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
	/// Summary description for RFRatingQualitativeAppr.
	/// </summary>
	public partial class RFRatingQualitativeAppr : System.Web.UI.Page
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
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGQUAL_VIEWREQUEST ";
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
			string desc, pendingStatus;
			string haveparent, parent, havechild, qualtplid;
			int jumlah;

			if (approvalFlag == "1")	// approved
			{
				/// ambil data yang di request
				/// 
				conn.QueryString = "SELECT * FROM PENDING_RFRATING_QUALITATIVE WHERE QUALITATIVEID = '" +id+ "'";
				conn.ExecuteQuery();
				pendingStatus	= conn.GetFieldValue("CH_STA");
				desc			= conn.GetFieldValue("QUALITATIVEDESC");
				haveparent		= conn.GetFieldValue("HAVEPARENT");
				parent			= conn.GetFieldValue("PARENT");
				havechild		= conn.GetFieldValue("HAVECHILD");
				qualtplid		= conn.GetFieldValue("QUALTPL_ID");
				conn.ClearData();

				/// audit perubahan parameter 
				/// 
				AuditTrail(id,desc,haveparent,parent,havechild,pendingStatus,qualtplid);

				/// memeriksa data yang existing
				/// 
				conn.QueryString = "SELECT COUNT(*) JUMLAH FROM RFRATING_QUALITATIVE WHERE QUALITATIVEID = '"+id+"'";
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
						conn.QueryString = "UPDATE RFRATING_QUALITATIVE SET QUALITATIVEDESC = '" + desc + 
							"', HAVEPARENT = '" + haveparent + 
							"', PARENT = '" + parent +
							"', HAVECHILD = '" + havechild +
							"', ACTIVE = '" + LBL_ACTIVE.Text.Trim() +
							"', QUALTPL_ID = '" + qualtplid +
							"' WHERE QUALITATIVEID = '" + id + "'";
						conn.ExecuteNonQuery();
					}
					else 
					{
						/// Kalau belum ada, insert
						/// 
						if(LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "INSERT INTO RFRATING_QUALITATIVE " +
								"(QUALITATIVEID , QUALITATIVEDESC, HAVEPARENT, PARENT, HAVECHILD, ACTIVE, QUALTPL_ID) " +
								" VALUES ('"+id+"', '"+desc+"', '"+haveparent+"', '"+parent+"', '"+havechild+"', '1', '" + qualtplid + "')";
						else
							conn.QueryString = "INSERT INTO RFRATING_QUALITATIVE " +
								"(QUALITATIVEID , QUALITATIVEDESC, HAVEPARENT, PARENT, HAVECHILD, QUALTPL_ID) " +
								" VALUES ('"+id+"', '"+desc+"', '"+haveparent+"', '"+parent+"', '"+havechild+"', '" + qualtplid + "')";
						conn.ExecuteNonQuery();
					}
				}
				else if (pendingStatus == "2")	// delete
				{
					if (jumlah > 0) 
					{
						conn.QueryString = "DELETE FROM RFRATING_QUALITATIVE WHERE QUALITATIVEID = '"+id+"'";
						conn.ExecuteNonQuery();
					}
				}
			}

			/// delete data dari pending
			/// 
			conn.QueryString = "DELETE FROM PENDING_RFRATING_QUALITATIVE WHERE QUALITATIVEID = '"+id+"'";
			conn.ExecuteNonQuery();			
		}

		private void AuditTrail(string id, string desc, string haveparent, string parent, string havechild, string pending_status, string qualtplid)
		{
			conn.ClearData();
			string desc_old = "", haveparent_old = "", parent_old = "", havechild_old = "", qualtplid_old = "";
			string action	= "";

			//Get Current Data
			conn.QueryString = "SELECT * FROM RFRATING_QUALITATIVE WHERE QUALITATIVEID = '" +id+ "'";
			conn.ExecuteQuery();
			if (conn.GetRowCount() > 0)
			{
				desc_old		= conn.GetFieldValue("QUALITATIVEDESC");
				haveparent_old	= conn.GetFieldValue("HAVEPARENT");
				parent_old		= conn.GetFieldValue("PARENT");
				havechild_old	= conn.GetFieldValue("HAVECHILD");
				qualtplid_old	= conn.GetFieldValue("QUALTPL_ID");
			}
			action			= pending_status;
			conn.ClearData();

			if (action == "0")	// Update
			{
				if (desc != desc_old)
				{
					ExecSPAuditTrail(id,"QUALITATIVEDESC",desc_old, desc, action);
					ExecSPAuditTrail(id,"HAVEPARENT",haveparent_old, haveparent, action);
					ExecSPAuditTrail(id,"PARENT",parent_old, parent, action);
					ExecSPAuditTrail(id,"HAVECHILD",havechild_old, havechild, action);
					ExecSPAuditTrail(id,"QUALTPL_ID",qualtplid_old, qualtplid, action);
				}
			} 
			else if (action == "1")	// Insert
			{
				ExecSPAuditTrail(id,"QUALITATIVEID", "", id, action);
				ExecSPAuditTrail(id,"QUALITATIVEDESC", "", desc, action);
				ExecSPAuditTrail(id,"HAVEPARENT", "", haveparent, action);
				ExecSPAuditTrail(id,"PARENT", "", parent, action);
				ExecSPAuditTrail(id,"HAVECHILD", "", havechild, action);
				ExecSPAuditTrail(id,"ACTIVE", "", "1", action);
				ExecSPAuditTrail(id,"QUALTPL_ID", "", qualtplid, action);
			} 
			else if (action == "2") // Delete
			{
				ExecSPAuditTrail(id,"QUALITATIVEID", id, "", action);
				ExecSPAuditTrail(id,"QUALITATIVEDESC", desc_old, "", action);
				ExecSPAuditTrail(id,"HAVEPARENT", haveparent_old, "", action);
				ExecSPAuditTrail(id,"PARENT", parent_old, "", action);
				ExecSPAuditTrail(id,"HAVECHILD", havechild_old, "", action);
				if (LBL_ACTIVE.Text.Trim() == "1")
					ExecSPAuditTrail(id,"ACTIVE", "1", "0" , action);
				ExecSPAuditTrail(id,"QUALTPL_ID", qualtplid_old, "", action);
			}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = Request.QueryString["tablename"];
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','" + tablename + "','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "'01','RFRATING_QUALITAIVE'";
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
