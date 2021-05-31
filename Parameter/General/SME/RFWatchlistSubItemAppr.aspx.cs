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
	/// Summary description for RFWatchlistSubItemAppr.
	/// </summary>
	public partial class RFWatchlistSubItemAppr : System.Web.UI.Page
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
			conn.QueryString = "select * from VW_PARAM_GENERAL_RFSUBWATCHLIST_VIEWREQUEST";
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
				string xid = DGRequest.Items[row].Cells[2].Text.Trim();
				string id = DGRequest.Items[row].Cells[4].Text.Trim();
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
				string xid = DGRequest.Items[row].Cells[2].Text.Trim();
				string id = DGRequest.Items[row].Cells[4].Text.Trim();
				executeApproval(xid, id, "0");
			} 
			catch (Exception e)
			{
				Response.Write("<!-- " + e.Message + " --> ");
			}
		}

		private void executeApproval(string xid, string id, string approvalFlag) 
		{
			string desc, dispno, pendingStatus;
			int jumlah;

			if (approvalFlag == "1")	// approved
			{
				/// ambil data yang di request
				/// 
				conn.QueryString = "SELECT * FROM PENDING_RFWATCHLIST_SUBITEM WHERE WATCHID = '" + xid + "' AND SUBWATCHID ='" + id + "'";
				conn.ExecuteQuery();
				pendingStatus	= conn.GetFieldValue("CH_STA");
				desc			= conn.GetFieldValue("SUBWATCHDESC");
				dispno			= conn.GetFieldValue("DISPNO");
				conn.ClearData();

				if ((dispno == null) || (dispno == "") || (dispno == "&nbsp;")) {dispno = "";}
				desc = desc.Replace("'","''");

				/// audit perubahan parameter 
				/// 
				AuditTrail(xid, id, desc, dispno, pendingStatus);

				/// memeriksa data yang existing
				/// 
				conn.QueryString = "SELECT COUNT(*) JUMLAH FROM RFWATCHLIST_SUBITEM WHERE WATCHID = '" + xid + "' AND SUBWATCHID ='" + id + "'";
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
						conn.QueryString = "UPDATE RFWATCHLIST_SUBITEM SET SUBWATCHDESC = '" + desc + "', DISPNO = '" + dispno +
							"', ACTIVE = '"+LBL_ACTIVE.Text.Trim()+"' WHERE WATCHID = '" + xid + "' AND SUBWATCHID ='" + id + "'";
						conn.ExecuteNonQuery();
					}
					else 
					{
						/// Kalau belum ada, insert
						/// 
						if(LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "INSERT INTO RFWATCHLIST_SUBITEM " +
								"(WATCHID, SUBWATCHID, SUBWATCHDESC, ACTIVE, DISPNO) " +
								" VALUES ('" + xid + "', '" + id + "', '" + desc + "', '1', '" + dispno + "')";
						else
							conn.QueryString = "INSERT INTO RFWATCHLIST_SUBITEM " +
								"(WATCHID, SUBWATCHID, SUBWATCHDESC, DISPNO) " +
								" VALUES ('" + xid + "', '" + id + "', '" + desc + "', '" + dispno + "')";
						conn.ExecuteNonQuery();
					}
				}
				else if (pendingStatus == "2")	// delete
				{
					if (jumlah > 0) 
					{
						if (LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "UPDATE RFWATCHLIST_SUBITEM SET ACTIVE = '0' WHERE WATCHID = '" + xid + "' AND SUBWATCHID ='" + id + "'";
						else
							conn.QueryString = "DELETE FROM RFWATCHLIST_SUBITEM WHERE WATCHID = '" + xid + "' AND SUBWATCHID ='" + id + "'";
						conn.ExecuteNonQuery();
					}
				}
			}

			/// delete data dari pending
			/// 
			conn.QueryString = "DELETE FROM PENDING_RFWATCHLIST_SUBITEM WHERE WATCHID = '" + xid + "' AND SUBWATCHID ='" + id + "'";
			conn.ExecuteNonQuery();			
		}

		private void AuditTrail(string xid, string id, string desc, string dispno, string pending_status)
		{
			conn.ClearData();
			string desc_old = "";
			string dispno_old = "";
			string action	= "";

			//Get Current Data
			conn.QueryString = "SELECT * FROM RFWATCHLIST_SUBITEM WHERE WATCHID = '" + xid + "' AND SUBWATCHID ='" + id + "'";
			conn.ExecuteQuery();
			if (conn.GetRowCount() > 0)
			{
				desc_old		= conn.GetFieldValue("SUBWATCHDESC");
				dispno_old		= conn.GetFieldValue("DISPNO");
			}
			action			= pending_status;
			conn.ClearData();

			if ((dispno == null) || (dispno == "") || (dispno == "&nbsp;")) {dispno = "";}
			desc = desc.Replace("'","''");
			if ((dispno_old == null) || (dispno_old == "") || (dispno_old == "&nbsp;")) {dispno_old = "";}
			desc_old = desc.Replace("'","''");

			if (action == "0")	// Update
			{
				if (desc != desc_old)
				{
					ExecSPAuditTrail(id,"SUBWATCHDESC",desc_old, desc, action);
				}
				if (dispno != dispno_old)
				{
					ExecSPAuditTrail(id,"DISPNO",dispno_old, dispno, action);
				}
			} 
			else if (action == "1")	// Insert
			{
				ExecSPAuditTrail(id,"SUBWATCHID", "", id, action);
				ExecSPAuditTrail(id,"SUBWATCHDESC", "", desc, action);
				ExecSPAuditTrail(id,"DISPNO", "", dispno, action);
				ExecSPAuditTrail(id,"ACTIVE", "", "1", action);
			} 
			else if (action == "2") // Delete
			{
				ExecSPAuditTrail(id,"SUBWATCHID", id, "", action);
				ExecSPAuditTrail(id,"SUBWATCHDESC", desc_old, "", action);
				ExecSPAuditTrail(id,"DISPNO", dispno_old, "", action);
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
				"" + "'," + "'01','RFWATCHLIST_SUBITEM'";
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
