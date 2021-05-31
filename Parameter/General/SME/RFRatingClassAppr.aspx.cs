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
	/// Summary description for RFRatingClassAppr.
	/// </summary>
	public partial class RFRatingClassAppr : System.Web.UI.Page
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
			conn.QueryString = "select * from VW_PARAM_PENDING_RFRATINGCLASS order by SCOTPL_ID, RATEID";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("SCOTPL_ID"));
			dt.Columns.Add(new DataColumn("SCOTPL_DESC"));
			dt.Columns.Add(new DataColumn("RATEID"));
			dt.Columns.Add(new DataColumn("RATEDESC"));
			dt.Columns.Add(new DataColumn("MINPD"));
			dt.Columns.Add(new DataColumn("MAXPD"));
			dt.Columns.Add(new DataColumn("PD_REMARK"));
			dt.Columns.Add(new DataColumn("MINSCORE"));
			dt.Columns.Add(new DataColumn("MAXSCORE"));
			dt.Columns.Add(new DataColumn("SCORE_REMARK"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				string pendcol = "ACTIVE";
				if (LBL_ACTIVE.Text.Trim() == "1")
					pendcol = "PENDINGSTATUS"; 
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,"SCOTPL_ID");
				dr[1] = conn.GetFieldValue(i,"SCOTPL_DESC");
				dr[2] = conn.GetFieldValue(i,"RATEID");
				dr[3] = conn.GetFieldValue(i,"RATEDESC");
				dr[4] = conn.GetFieldValue(i,"MINPD");
				dr[5] = conn.GetFieldValue(i,"MAXPD");
				dr[6] = conn.GetFieldValue(i,"PD_REMARK");
				dr[7] = conn.GetFieldValue(i,"MINSCORE");
				dr[8] = conn.GetFieldValue(i,"MAXSCORE");
				dr[9] = conn.GetFieldValue(i,"SCORE_REMARK");
				dr[10] = conn.GetFieldValue(i,pendcol);
				dr[11] = getPendingStatus(conn.GetFieldValue(i,pendcol));
				dt.Rows.Add(dr);
			}

			DGRequest.DataSource = new DataView(dt);
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

		private string getPendingStatus(string saveMode) 
		{
			string status = "";			
			switch (saveMode)
			{
				case "0":
					status = "Update";
					break;
				case "1":
					status = "Insert";
					break;
				case "2":
					status = "Delete";
					break;
				default:
					status = "";
					break;
			}
			return status;
		}

		private void performRequest(int row)
		{
			try 
			{
				string scotpl_id = DGRequest.Items[row].Cells[0].Text.Trim(),
					id = DGRequest.Items[row].Cells[2].Text.Trim();
				executeApproval(scotpl_id, id, "1");
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
				string scotpl_id = DGRequest.Items[row].Cells[0].Text.Trim(),
					id = DGRequest.Items[row].Cells[2].Text.Trim();
				executeApproval(scotpl_id, id, "0");
			} 
			catch (Exception e)
			{
				Response.Write("<!-- " + e.Message + " --> ");
			}
		}

		private void executeApproval(string scotpl_id, string id, string approvalFlag) 
		{
			string pendingStatus, desc, pdremark, scoreremark;
			int jumlah;
			double minpd, maxpd, minscore, maxscore;

			if (approvalFlag == "1")	// approved
			{
				/// ambil data yang di request
				/// 
				conn.QueryString = "SELECT * FROM PENDING_RFRATINGCLASS WHERE SCOTPL_ID = '" + scotpl_id + "' AND RATEID = '" +id+ "'";
				conn.ExecuteQuery();
				pendingStatus	= conn.GetFieldValue("PENDINGSTATUS");
				desc			= conn.GetFieldValue("RATEDESC");
				minpd			= double.Parse(conn.GetFieldValue("MINPD"));
				maxpd			= double.Parse(conn.GetFieldValue("MAXPD"));
				pdremark			= conn.GetFieldValue("PD_REMARK");
				minscore		= double.Parse(conn.GetFieldValue("MINSCORE"));
				maxscore		= double.Parse(conn.GetFieldValue("MAXSCORE"));
				scoreremark			= conn.GetFieldValue("SCORE_REMARK");
				conn.ClearData();

				/// audit perubahan parameter 
				/// 
				AuditTrail(scotpl_id, id, desc, minpd.ToString().Replace(",","."), maxpd.ToString().Replace(",","."), pdremark, 
					minscore.ToString().Replace(",","."), maxscore.ToString().Replace(",","."), scoreremark, pendingStatus);

				/// memeriksa data yang existing
				/// 
				conn.QueryString = "SELECT COUNT(*) JUMLAH FROM RFRATINGCLASS WHERE SCOTPL_ID = '" + scotpl_id + "' AND RATEID = '"+id+"'";
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
						conn.QueryString = "UPDATE RFRATINGCLASS SET RATEDESC = '" + desc + 
							"', MINPD = " + minpd.ToString().Replace(",",".") +
							", MAXPD = " + maxpd.ToString().Replace(",",".") +
							", PD_REMARK = '" + pdremark + "'" +
							", MINSCORE = " + minscore.ToString().Replace(",",".") +
							", MAXSCORE = " + maxscore.ToString().Replace(",",".") +
							", SCORE_REMARK = '" + scoreremark + "'" +
							", ACTIVE = '" + LBL_ACTIVE.Text.Trim() + 
							"' WHERE SCOTPL_ID = '" + scotpl_id + "' AND RATEID = '" + id + "'";
						conn.ExecuteNonQuery();
					}
					else 
					{
						/// Kalau belum ada, insert
						/// 
						if(LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "INSERT INTO RFRATINGCLASS " +
                                " (SCOTPL_ID, RATEID, RATEDESC, MINPD, MAXPD, PD_REMARK, MINSCORE, MAXSCORE, ACTIVE) VALUES ('" +
								scotpl_id + "', '" + id + "', '" + desc + "', " + 
								minpd.ToString().Replace(",",".") + ", " + 
								maxpd.ToString().Replace(",",".") + ", " + 
								"'" + pdremark + "', " +
								minscore.ToString().Replace(",",".") + ", " +
                                maxscore.ToString().Replace(",", ".") +
								", '1')";
						else
							conn.QueryString = "INSERT INTO RFRATINGCLASS " +
                                " (SCOTPL_ID, RATEID, RATEDESC, MINPD, MAXPD, PD_REMARK, MINSCORE, MAXSCORE) VALUES ('" +
								scotpl_id + "', '" + id + "', '" + desc + "', " + 
								minpd.ToString().Replace(",",".") + ", " + 
								maxpd.ToString().Replace(",",".") + ", " + 
								"'" + pdremark + "', " +
								minscore.ToString().Replace(",",".") + ", " +
                                maxscore.ToString().Replace(",", ".") +
								")";
						conn.ExecuteNonQuery();
					}
				}
				else if (pendingStatus == "2")	// delete
				{
					if (jumlah > 0) 
					{
						if (LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "UPDATE RFRATINGCLASS SET ACTIVE = '0' WHERE SCOTPL_ID = '" + scotpl_id + "' AND RATEID = '" + id + "'";
						else
							conn.QueryString = "DELETE FROM RFRATINGCLASS WHERE SCOTPL_ID = '" + scotpl_id + "' AND RATEID = '" + id + "'";
						conn.ExecuteNonQuery();
					}
				}
			}

			/// delete data dari pending
			/// 
			conn.QueryString = "DELETE FROM PENDING_RFRATINGCLASS WHERE SCOTPL_ID = '" + scotpl_id + "' AND RATEID = '" + id + "'";
			conn.ExecuteNonQuery();			
		}

		private void AuditTrail(string scotpl_id, string id, string desc, string minpd, string maxpd, string pdremark, 
			string minscore, string maxscore, string scoreremark, string pending_status)
		{
			conn.ClearData();
			string desc_old = "";
			string minpd_old = "";
			string maxpd_old = "";
			string pdremark_old = "";
			string minscore_old = "";
			string maxscore_old = "";
			string scoreremark_old = "";
			string action	= "";

			//Get Current Data
			conn.QueryString = "SELECT * FROM RFRATINGCLASS WHERE SCOTPL_ID = '" + scotpl_id + "' AND RATEID = '" +id+ "'";
			conn.ExecuteQuery();
			desc_old		= conn.GetFieldValue("RATEDESC").ToString();
			minpd_old		= conn.GetFieldValue("MINPD").ToString();
			maxpd_old		= conn.GetFieldValue("MAXPD").ToString();
			pdremark_old	= conn.GetFieldValue("PD_REMARK").ToString();
			minscore_old	= conn.GetFieldValue("MINSCORE").ToString();
			maxscore_old	= conn.GetFieldValue("MAXSCORE").ToString();
			scoreremark_old	= conn.GetFieldValue("SCORE_REMARK").ToString();
			action			= pending_status;
			conn.ClearData();

			if (action == "0")	// Update
			{
				if (desc != desc_old)
					ExecSPAuditTrail(id, "RATEDESC", desc_old, desc, action);
				if (minpd != minpd_old)
					ExecSPAuditTrail(id, "MINPD", minpd_old, minpd, action);
				if (maxpd != maxpd_old)
					ExecSPAuditTrail(id, "MAXPD", maxpd_old, maxpd, action);
				if (pdremark != pdremark_old)
					ExecSPAuditTrail(id, "PD_REMARK", pdremark_old, pdremark, action);
				if (minscore != minscore_old)
					ExecSPAuditTrail(id, "MINSCORE", minscore_old, minscore, action);
				if (maxscore != maxscore_old)
					ExecSPAuditTrail(id, "MAXSCORE", maxscore_old, maxscore, action);
				if (scoreremark != scoreremark_old)
					ExecSPAuditTrail(id, "SCORE_REMARK", scoreremark_old, scoreremark, action);
			} 
			else if (action == "1")	// Insert
			{
				ExecSPAuditTrail(id, "SCOTPL_ID", "", id, action);
				ExecSPAuditTrail(id, "RATEID", "", id, action);
				ExecSPAuditTrail(id, "RATEDESC", "", desc, action);
				ExecSPAuditTrail(id, "MINPD", "", minpd, action);
				ExecSPAuditTrail(id, "MAXPD", "", maxpd, action);
				ExecSPAuditTrail(id, "PD_REMARK", "", pdremark, action);
				ExecSPAuditTrail(id, "MINSCORE", "", minscore, action);
				ExecSPAuditTrail(id, "MAXSCORE", "", maxscore, action);
				ExecSPAuditTrail(id, "SCORE_REMARK", "", scoreremark, action);
				ExecSPAuditTrail(id, "ACTIVE", "", "1", action);
			} 
			else if (action == "2") // Delete
			{
				ExecSPAuditTrail(id, "SCOTPL_ID", id, "", action);
				ExecSPAuditTrail(id, "RATEID", id, "", action);
				ExecSPAuditTrail(id, "RATEDESC", desc_old, "", action);
				ExecSPAuditTrail(id, "MINPD", minpd_old, "", action);
				ExecSPAuditTrail(id, "MAXPD", maxpd_old, "", action);
				ExecSPAuditTrail(id, "PD_REMARK", pdremark_old, "", action);
				ExecSPAuditTrail(id, "MINSCORE", minscore_old, "", action);
				ExecSPAuditTrail(id, "MAXSCORE", maxscore_old, "", action);
				ExecSPAuditTrail(id, "SCORE_REMARK", scoreremark_old, "", action);
				if (LBL_ACTIVE.Text.Trim() == "1")
					ExecSPAuditTrail(id, "ACTIVE", "1", "0" , action);
			}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = Request.QueryString["tablename"];
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','" + tablename + "','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "'01','Rating - Class'";
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

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}
	}
}