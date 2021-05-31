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
	/// Summary description for RFWatchlistSubSubItemAppr.
	/// </summary>
	public partial class RFWatchlistSubSubItemAppr : System.Web.UI.Page
	{
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
			conn.QueryString = "select * from VW_PARAM_GENERAL_RFSUBSUBWATCHLIST_VIEWREQUEST";
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
				string xid = DGRequest.Items[row].Cells[4].Text.Trim();
				string id = DGRequest.Items[row].Cells[6].Text.Trim();
				executeApproval(xid, id, "1");
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
				string xid = DGRequest.Items[row].Cells[4].Text.Trim();
				string id = DGRequest.Items[row].Cells[6].Text.Trim();
				executeApproval(xid, id, "0");
			} 
			catch (Exception e)
			{
				Response.Write("<!-- " + e.Message + " --> ");
			}
		}

		private void executeApproval(string xid, string id, string approvalFlag) 
		{
			string desc, weight, ismandatory, rvwkolbi, pilarbi, isignored, pendingStatus;
			int jumlah;

			if (approvalFlag == "1")	// approved
			{
				/// ambil data yang di request
				/// 
				conn.QueryString = "SELECT * FROM PENDING_RFWATCHLIST_SUBSUBITEM WHERE SUBWATCHID = '" + xid + "' AND SUBSUBWATCHID ='" + id + "'";
				conn.ExecuteQuery();
				pendingStatus	= conn.GetFieldValue("CH_STA");
				desc			= conn.GetFieldValue("SUBSUBWATCHDESC");
				weight			= conn.GetFieldValue("WEIGHT");
				ismandatory		= conn.GetFieldValue("ISMANDATORY");
				rvwkolbi		= conn.GetFieldValue("RVWKOLBI");
				pilarbi			= conn.GetFieldValue("PILARBI");
				isignored		= conn.GetFieldValue("ISIGNORED");
				conn.ClearData();

				/// audit perubahan parameter 
				/// 
				AuditTrail(xid, id, desc, weight, ismandatory, rvwkolbi, pilarbi, isignored, pendingStatus);

				/// memeriksa data yang existing
				/// 
				conn.QueryString = "SELECT COUNT(*) JUMLAH FROM RFWATCHLIST_SUBSUBITEM WHERE SUBWATCHID = '" + xid + "' AND SUBSUBWATCHID ='" + id + "'";
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
						conn.QueryString = "UPDATE RFWATCHLIST_SUBSUBITEM SET SUBSUBWATCHDESC = '" + desc + 
							"', WEIGHT = '" + weight + "', ISMANDATORY = '" + ismandatory + "', RVWKOLBI = '" + rvwkolbi + "', PILARBI = '" + pilarbi + "', ISIGNORED = '" + isignored + 
							"', ACTIVE = '"+LBL_ACTIVE.Text.Trim()+"' WHERE SUBWATCHID = '" + xid + "' AND SUBSUBWATCHID ='" + id + "'";
						conn.ExecuteNonQuery();
					}
					else 
					{
						/// Kalau belum ada, insert
						/// 
						if(LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "INSERT INTO RFWATCHLIST_SUBSUBITEM " +
								"(SUBWATCHID, SUBSUBWATCHID, SUBSUBWATCHDESC, WEIGHT, ISMANDATORY, ISIGNORED, ACTIVE, RVWKOLBI, PILARBI) " +
								" VALUES ('" + xid + "', '" + id + "', '" + desc + "', '" + weight + "', '" + ismandatory + "', '" + isignored + "', '1', '" + rvwkolbi + "', '" + pilarbi + "')";
						else
							conn.QueryString = "INSERT INTO RFWATCHLIST_SUBSUBITEM " +
								"(SUBWATCHID, SUBSUBWATCHID, SUBSUBWATCHDESC, WEIGHT, ISMANDATORY, ISIGNORED, RVWKOLBI, PILARBI) " +
								" VALUES ('" + xid + "', '" + id + "', '" + desc + "', '" + weight + "', '" + ismandatory + "', '" + isignored + "', '" + rvwkolbi + "', '" + pilarbi + "')";
						conn.ExecuteNonQuery();
					}
				}
				else if (pendingStatus == "2")	// delete
				{
					if (jumlah > 0) 
					{
						if (LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "UPDATE RFWATCHLIST_SUBSUBITEM SET ACTIVE = '0' WHERE SUBWATCHID = '" + xid + "' AND SUBSUBWATCHID ='" + id + "'";
						else
							conn.QueryString = "DELETE FROM RFWATCHLIST_SUBSUBITEM WHERE SUBWATCHID = '" + xid + "' AND SUBSUBWATCHID ='" + id + "'";
						conn.ExecuteNonQuery();
					}
				}
			}

			/// delete data dari pending
			/// 
			conn.QueryString = "DELETE FROM PENDING_RFWATCHLIST_SUBSUBITEM WHERE SUBWATCHID = '" + xid + "' AND SUBSUBWATCHID ='" + id + "'";
			conn.ExecuteNonQuery();			
		}

		private void AuditTrail(string xid, string id, string desc, string weight, string ismandatory, string rvwkolbi, string pilarbi, string isignored, string pending_status)
		{
			conn.ClearData();
			string desc_old = "";
			string weight_old = "";
			string ismandatory_old = "";
			string rvwkolbi_old = "";
			string pilarbi_old = "";
			string isignored_old = "";
			string action	= "";

			//Get Current Data
			conn.QueryString = "SELECT * FROM RFWATCHLIST_SUBSUBITEM WHERE SUBWATCHID = '" + xid + "' AND SUBSUBWATCHID ='" + id + "'";
			conn.ExecuteQuery();
			if (conn.GetRowCount() > 0)
			{
				desc_old		= conn.GetFieldValue("SUBSUBWATCHDESC");
				weight_old		= conn.GetFieldValue("WEIGHT");
				ismandatory_old	= conn.GetFieldValue("ISMANDATORY");
				rvwkolbi_old	= conn.GetFieldValue("RVWKOLBI");
				pilarbi_old		= conn.GetFieldValue("PILARBI");
				isignored_old	= conn.GetFieldValue("ISIGNORED");
			}
			action			= pending_status;
			conn.ClearData();

			if (action == "0")	// Update
			{
				if (desc != desc_old)
				{
					ExecSPAuditTrail(id, "SUBSUBWATCHDESC", desc_old, desc, action);
				}
				if (weight != weight_old)
				{
					ExecSPAuditTrail(id, "WEIGHT", weight_old, weight, action);
				}
				if (ismandatory != ismandatory_old)
				{
					ExecSPAuditTrail(id, "ISMANDATORY", ismandatory_old, ismandatory, action);
				}
				if (ismandatory != ismandatory_old)
				{
					ExecSPAuditTrail(id, "RVWKOLBI", rvwkolbi_old, rvwkolbi, action);
				}
				if (ismandatory != ismandatory_old)
				{
					ExecSPAuditTrail(id, "PILARBI", pilarbi_old, pilarbi, action);
				}
				if (isignored != isignored_old)
				{
					ExecSPAuditTrail(id, "ISIGNORED", isignored_old, isignored, action);
				}
			} 
			else if (action == "1")	// Insert
			{
				ExecSPAuditTrail(id, "SUBSUBWATCHID", "", id, action);
				ExecSPAuditTrail(id, "SUBSUBWATCHDESC", "", desc, action);
				ExecSPAuditTrail(id, "WEIGHT", "", weight, action);
				ExecSPAuditTrail(id, "ISMANDATORY", "", ismandatory, action);
				ExecSPAuditTrail(id, "RVWKOLBI", "", rvwkolbi, action);
				ExecSPAuditTrail(id, "PILARBI", "", pilarbi, action);
				ExecSPAuditTrail(id, "ISIGNORED", "", isignored, action);
				ExecSPAuditTrail(id, "ACTIVE", "", "1", action);
			} 
			else if (action == "2") // Delete
			{
				ExecSPAuditTrail(id, "SUBSUBWATCHID", id, "", action);
				ExecSPAuditTrail(id, "SUBSUBWATCHDESC", desc_old, "", action);
				ExecSPAuditTrail(id, "WEIGHT", weight_old, "", action);
				ExecSPAuditTrail(id, "ISMANDATORY", ismandatory_old, "", action);
				ExecSPAuditTrail(id, "RVWKOLBI", rvwkolbi_old, "", action);
				ExecSPAuditTrail(id, "PILARBI", pilarbi_old, "", action);
				ExecSPAuditTrail(id, "ISIGNORED", isignored_old, "", action);
				if (LBL_ACTIVE.Text.Trim() == "1")
					ExecSPAuditTrail(id,"ACTIVE", "1", "0" , action);
			}
		}

		private void ExecSPAuditTrail(string id, string field, string from, string to, string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = Request.QueryString["tablename"];
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','" + tablename + "','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "'01','RFWATCHLIST_SUBSUBITEM'";
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
