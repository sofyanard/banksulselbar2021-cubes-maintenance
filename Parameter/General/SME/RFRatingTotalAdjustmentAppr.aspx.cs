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
	/// Summary description for RFRatingTotalAdjustmentAppr.
	/// </summary>
	public partial class RFRatingTotalAdjustmentAppr : System.Web.UI.Page
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
			conn.QueryString = "select * from PENDING_RFSCOREBCGTOTALADJUSTMENT ";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("MIN_RANGE"));
			dt.Columns.Add(new DataColumn("MAX_RANGE"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				//int pendcol = 2;
				string pendcol = "ACTIVE";
				if (LBL_ACTIVE.Text.Trim() == "1")
					pendcol = "PENDINGSTATUS"; 
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,"CODE");
				dr[1] = conn.GetFieldValue(i,"DESC");
				dr[2] = conn.GetFieldValue(i,"MIN_RANGE");
				dr[3] = conn.GetFieldValue(i,"MAX_RANGE");
				dr[4] = conn.GetFieldValue(i,pendcol);
				dr[5] = getPendingStatus(conn.GetFieldValue(i,pendcol));
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
			int jumlah;
			double min_range, max_range;

			if (approvalFlag == "1")	// approved
			{
				/// ambil data yang di request
				/// 
				conn.QueryString = "SELECT * FROM PENDING_RFSCOREBCGTOTALADJUSTMENT WHERE [CODE] = '" +id+ "'";
				conn.ExecuteQuery();
				pendingStatus	= conn.GetFieldValue("PENDINGSTATUS");
				desc			= conn.GetFieldValue("DESC");
				min_range			= double.Parse(conn.GetFieldValue("MIN_RANGE"));
				max_range			= double.Parse(conn.GetFieldValue("MAX_RANGE"));
				conn.ClearData();

				/// audit perubahan parameter 
				/// 
				AuditTrail(id, desc, min_range.ToString(), max_range.ToString(), pendingStatus);

				/// memeriksa data yang existing
				/// 
				conn.QueryString = "SELECT COUNT(*) JUMLAH FROM RFSCOREBCGTOTALADJUSTMENT WHERE [CODE] = '"+id+"'";
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
						conn.QueryString = "UPDATE RFSCOREBCGTOTALADJUSTMENT SET [DESC] = '" + desc +
							"', MIN_RANGE = " + min_range + ", MAX_RANGE = " + max_range + 
							", ACTIVE = '" + LBL_ACTIVE.Text.Trim() + "' WHERE [CODE] = '" + id + "'";
						conn.ExecuteNonQuery();
					}
					else 
					{
						/// Kalau belum ada, insert
						/// 
						conn.QueryString = "select c.name from syscolumns c inner join sysobjects o on o.id = c.id " +
							"where o.name = '" + Request.QueryString["tablename"] + "' order by colorder ";
						conn.ExecuteQuery();

						if(LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "INSERT INTO RFSCOREBCGTOTALADJUSTMENT " +
								" ([CODE], [DESC], [MIN_RANGE], [MAX_RANGE], [ACTIVE]) VALUES ('" +
								id + "', '" + desc + "', " + min_range + ", " + max_range + ", '1')";
						else
							conn.QueryString = "INSERT INTO RFSCOREBCGTOTALADJUSTMENT " +
								" ([CODE], [DESC], [MIN_RANGE], [MAX_RANGE]) VALUES ('" +
								id + "', '" + desc + "', " + min_range + ", " + max_range + ")";
						conn.ExecuteNonQuery();
					}
				}
				else if (pendingStatus == "2")	// delete
				{
					if (jumlah > 0) 
					{
						if (LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "UPDATE RFSCOREBCGTOTALADJUSTMENT SET ACTIVE = '0' WHERE [CODE] = '" + id + "'";
						else
							conn.QueryString = "DELETE FROM RFSCOREBCGTOTALADJUSTMENT WHERE [CODE] = '" + id + "'";
						conn.ExecuteNonQuery();
					}
				}
			}

			/// delete data dari pending
			/// 
			conn.QueryString = "DELETE FROM PENDING_RFSCOREBCGTOTALADJUSTMENT WHERE [CODE] = '" + id + "'";
			conn.ExecuteNonQuery();			
		}

		private void AuditTrail(string id, string desc, string min_range, string max_range, string pending_status)
		{
			conn.ClearData();
			string desc_old = "";
			string min_range_old = "";
			string max_range_old = "";
			string action	= "";

			//Get Current Data
			conn.QueryString = "SELECT * FROM RFSCOREBCGTOTALADJUSTMENT WHERE [CODE] = '" +id+ "'";
			conn.ExecuteQuery();
			desc_old		= conn.GetFieldValue("DESC");
			min_range_old	= conn.GetFieldValue("MIN_RANGE").ToString();
			max_range_old	= conn.GetFieldValue("MAX_RANGE").ToString();
			action			= pending_status;
			conn.ClearData();

			if (action == "0")	// Update
			{
				if (desc != desc_old)
					ExecSPAuditTrail(id, "[DESC]", desc_old, desc, action);
				if (min_range != min_range_old)
					ExecSPAuditTrail(id, "MIN_RANGE", min_range_old, min_range, action);
				if (max_range != max_range_old)
					ExecSPAuditTrail(id, "MAX_RANGE", max_range_old, max_range, action);
			} 
			else if (action == "1")	// Insert
			{
				ExecSPAuditTrail(id, "CODE", "", id, action);
				ExecSPAuditTrail(id, "[DESC]", "", desc, action);
				ExecSPAuditTrail(id, "MIN-RANGE", "", min_range, action);
				ExecSPAuditTrail(id, "MAX_RANGE", "", max_range, action);
				ExecSPAuditTrail(id,"ACTIVE", "", "1", action);
			} 
			else if (action == "2") // Delete
			{
				ExecSPAuditTrail(id, "CODE", id, "", action);
				ExecSPAuditTrail(id, "[DESC]", desc_old, "", action);
				ExecSPAuditTrail(id, "MIN_RANGE", min_range_old, "", action);
				ExecSPAuditTrail(id, "MAX_RANGE", max_range_old, "", action);
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
				"" + "'," + "'01','Rating - Total Adjustment'";
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
