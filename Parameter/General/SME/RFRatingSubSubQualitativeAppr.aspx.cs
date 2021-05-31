using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFRatingSubSubQualitativeAppr.
	/// </summary>
	public partial class RFRatingSubSubQualitativeAppr : System.Web.UI.Page
	{
		protected Tools tool = new Tools();
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack) 
			{
				LBL_ACTIVE.Text = Request.QueryString["active"];
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition
				
				viewPendingData();
			}
		}

		private void viewPendingData()
		{
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGSUBSUBQUAL_VIEWREQUEST '', ''";
			conn.ExecuteQuery();
			DGRequest.DataSource = conn.GetDataTable().Copy();
			try 
			{
				DGRequest.DataBind();
			}
			catch 
			{
				DGRequest.CurrentPageIndex = DGRequest.PageCount - 1;
				DGRequest.DataBind();
			}
		}

		private void performRequest(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				executeApproval(id, "1");
			} 
			catch (Exception e)
			{
				Response.Write("<!-- " + e.Message + " --> ");
			}
		}

		private void deleteData(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				executeApproval(id, "0");
			} 
			catch (Exception e)
			{
				Response.Write("<!-- " + e.Message + " --> ");
			}
		}

		private void executeApproval(string id, string approvalFlag) 
		{
			string desc, xid, pendingStatus, dg_flag, dg_level;
			double score;
			int jumlah;

			if (approvalFlag == "1")	// approved
			{
				/// ambil data yang di request
				/// 
				conn.QueryString = "SELECT SUBSUBQUALITATIVEID, SUBQUALITATIVEID, SUBSUBQUALITATIVEDESC, SCORE, DOWNGRADE_FLAG, DOWNGRADE_LEVEL, ACTIVE, CH_STA " +
 					" FROM PENDING_RFRATING_SUBSUBQUALITATIVE WHERE SUBSUBQUALITATIVEID = '" +id+ "'";
				conn.ExecuteQuery();
				pendingStatus	= conn.GetFieldValue("CH_STA");
				xid				= conn.GetFieldValue(0,"SUBQUALITATIVEID");
				desc			= conn.GetFieldValue(0,"SUBSUBQUALITATIVEDESC");
				score			= double.Parse(conn.GetFieldValue(0,"SCORE"));
				dg_flag			= conn.GetFieldValue(0,"DOWNGRADE_FLAG");
				dg_level		= conn.GetFieldValue(0,"DOWNGRADE_LEVEL");
				conn.ClearData();

				/// audit perubahan parameter 
				/// 
				AuditTrail(id, desc, score, dg_flag, dg_level, pendingStatus);

				/// memeriksa data yang existing
				/// 
				conn.QueryString = "SELECT COUNT(*) JUMLAH FROM RFRATING_SUBSUBQUALITATIVE WHERE SUBSUBQUALITATIVEID = '"+id+"'";
				conn.ExecuteQuery();
				jumlah = Convert.ToInt16(conn.GetFieldValue("JUMLAH"));

				/// pendingstatus
				/// 0 : update
				/// 1 : insert
				/// 
				if (pendingStatus == "0" || pendingStatus == "1") 
				{
					/// Kalau sudah ada, update aja
					/// 
					if (jumlah > 0) 
					{
						conn.QueryString = "UPDATE RFRATING_SUBSUBQUALITATIVE SET SUBSUBQUALITATIVEDESC = '" + desc + 
							"', SCORE = " + score.ToString().Replace(",",".") + ", DOWNGRADE_FLAG = '" + dg_flag + "', DOWNGRADE_LEVEL = '" + dg_level +
							"', ACTIVE = '"+LBL_ACTIVE.Text.Trim()+"' WHERE SUBSUBQUALITATIVEID = '"+id+"'";
						conn.ExecuteNonQuery();
					}
					else 
					{
						/// Kalau belum ada, insert
						/// 
						if(LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "INSERT INTO RFRATING_SUBSUBQUALITATIVE " +
								"(SUBSUBQUALITATIVEID, SUBQUALITATIVEID, SUBSUBQUALITATIVEDESC, SCORE, DOWNGRADE_FLAG, DOWNGRADE_LEVEL, ACTIVE) " +
								" VALUES ('"+id+"', '"+xid+"', '"+desc+"', "+score.ToString().Replace(",",".")+", '"+dg_flag+"', '"+dg_level+"', '1')";
						else
							conn.QueryString = "INSERT INTO RFRATING_SUBSUBQUALITATIVE " +
								"(SUBSUBQUALITATIVEID, SUBQUALITATIVEID, SUBSUBQUALITATIVEDESC, SCORE, DOWNGRADE_FLAG, DOWNGRADE_LEVEL) " +
								" VALUES ('"+id+"', '"+xid+"', '"+desc+"', "+score.ToString().Replace(",",".")+", '"+dg_flag+"', '"+dg_level+"')";
						conn.ExecuteNonQuery();
					}
				}
				else if (pendingStatus == "2")	// delete
				{
					if (jumlah > 0) 
					{
						if (LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "UPDATE RFRATING_SUBSUBQUALITATIVE SET ACTIVE = '0' WHERE SUBSUBQUALITATIVEID = '"+id+"'";
						else
							conn.QueryString = "DELETE FROM RFRATING_SUBSUBQUALITATIVE WHERE SUBSUBQUALITATIVEID = '"+id+"'";
						conn.ExecuteNonQuery();
					}
				}
			}

			/// delete data dari pending
			/// 
			conn.QueryString = "DELETE FROM PENDING_RFRATING_SUBSUBQUALITATIVE WHERE SUBSUBQUALITATIVEID = '"+id+"'";
			conn.ExecuteNonQuery();			
		}

		private void AuditTrail(string id, string desc, double score, string dg_flag, string dg_level, string pending_status)
		{
			conn.ClearData();
			string desc_old = "";
			double score_old = 0.0;
			string dg_flag_old = "";
			string dg_level_old = "";
			string action	= "";

			//Get Current Data
			conn.QueryString = "SELECT * FROM RFRATING_SUBSUBQUALITATIVE WHERE SUBSUBQUALITATIVEID = '" +id+ "'";
			conn.ExecuteQuery();
			if (conn.GetRowCount() > 0)
			{
				desc_old		= conn.GetFieldValue("SUBSUBQUALITATIVEDESC");
				score_old		= double.Parse(conn.GetFieldValue("SCORE"));
				dg_flag_old		= conn.GetFieldValue("DOWNGRADE_FLAG");
				dg_level_old	= conn.GetFieldValue("DOWNGRADE_LEVEL");
			}
			action			= pending_status;
			conn.ClearData();

			if (action == "0")	// Update
			{
				if (desc != desc_old)
					ExecSPAuditTrail(id,"SUBSUBQUALITATIVEDESC",desc_old, desc, action);
				if (score != score_old)
					ExecSPAuditTrail(id,"SCORE",score_old.ToString(), score.ToString(), action);
				if (dg_flag != dg_flag_old)
					ExecSPAuditTrail(id,"DOWNGRADE_FLAG",dg_flag_old, dg_flag, action);
				if (dg_level != dg_level_old)
					ExecSPAuditTrail(id,"DOWNGRADE_LEVEL",dg_level_old, dg_level, action);
			} 
			else if (action == "1")	// Insert
			{
				ExecSPAuditTrail(id,"SUBSUBQUALITATIVEID", "", id, action);
				ExecSPAuditTrail(id,"SUBSUBQUALITATIVEDESC", "", desc, action);
				ExecSPAuditTrail(id,"SCORE", "", score.ToString(), action);
				ExecSPAuditTrail(id,"DOWNGRADE_FLAG", "", dg_flag, action);
				ExecSPAuditTrail(id,"DOWNGRADE_LEVEL", "", dg_level, action);
				ExecSPAuditTrail(id,"ACTIVE", "", "1", action);
			} 
			else if (action == "2") // Delete
			{
				ExecSPAuditTrail(id,"SUBSUBQUALITATIVEID", id, "", action);
				ExecSPAuditTrail(id,"SUBSUBQUALITATIVEDESC", desc_old, "", action);
				if (LBL_ACTIVE.Text.Trim() == "1")
					ExecSPAuditTrail(id,"ACTIVE", "1", "0" , action);
			}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = Request.QueryString["tablename"];
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','" + tablename + "','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "'01','RFRATING_SUBSUBQUALITAIVE'";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;			
			viewPendingData();
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGRequest.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject");
					if (rbA.Checked)
					{
						performRequest(i);
					}
					else if (rbR.Checked)
					{
						deleteData(i);
					}
				} 
				catch {}
			}
			viewPendingData();
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbB.Checked = false;
							rbC.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}
	}
}