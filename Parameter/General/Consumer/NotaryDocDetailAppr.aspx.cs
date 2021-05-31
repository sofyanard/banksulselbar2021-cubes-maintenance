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
using DMS.DBConnection;
using DMS.CuBESCore;
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for NotaryDocDetailAppr.
	/// </summary>
	public partial class NotaryDocDetailAppr : System.Web.UI.Page
	{
		protected Connection conn,conn2;
		//protected Connection conn2= new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			
			if (!IsPostBack)
			{
				LBL_ACTIVE.Text = Request.QueryString["active"];
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

				viewPendingData();
			}
			
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
		
		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();
			
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" + conn2.GetFieldValue("DB_LOGINID") + ";pwd=" + conn2.GetFieldValue("DB_LOGINPWD") + ";Pooling=true");
		}

		private void viewPendingData() 
		{
			string tableName = Request.QueryString["tablename"];

			conn.QueryString = "select dnd_code,dnd_desc,a.dnm_code,dnm_desc,col_type,a.CH_STA,"+
				"case when col_type='H0' then 'Tanah/Bangunan/Tanah & Bangunan' "+
				"when col_type='C01' then 'Kendaraan' "+
				"else '' end COL from Trfdocnotary_detail a "+
				"left join rfdocnotary_master b on a.dnm_code=b.dnm_code";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("DND_CODE"));
			dt.Columns.Add(new DataColumn("DND_DESC"));
			dt.Columns.Add(new DataColumn("COL"));
			dt.Columns.Add(new DataColumn("DNM_CODE"));
			dt.Columns.Add(new DataColumn("COL_TYPE"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,"DND_CODE");
				dr[1] = conn.GetFieldValue(i,"DND_DESC");
				dr[2] = conn.GetFieldValue(i,"COL");
				dr[3] = conn.GetFieldValue(i,"DNM_CODE");
				dr[4] = conn.GetFieldValue(i,"COL_TYPE");
				dr[5] = conn.GetFieldValue(i,"CH_STA");
				dr[6] = getPendingStatus(conn.GetFieldValue(i,"CH_STA"));
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
		private void DGRequest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;			
			viewPendingData();
		}

		private void performRequest(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				string desc = DGRequest.Items[row].Cells[1].Text.Trim();
				string doc = DGRequest.Items[row].Cells[3].Text.Trim();
				string col = DGRequest.Items[row].Cells[4].Text.Trim();
				executeApproval(id,desc,doc,col, "1");
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				string desc = DGRequest.Items[row].Cells[1].Text.Trim();
				string doc = DGRequest.Items[row].Cells[3].Text.Trim();
				string col = DGRequest.Items[row].Cells[4].Text.Trim();
				executeApproval(id,desc,doc,col, "0");
			} 
			catch {}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "RFDOCNOTARY_MASTER";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'Notary Document Detail'";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}

		private void executeApproval(string id, string desc, string doc, string col, string approvalFlag) 
		{
			int jumlah;
			string pendingStatus;
			string desc_old,doc_old,col_old;

			if (approvalFlag == "1") 
			{
				conn.QueryString = "SELECT * FROM tRFDOCNOTARY_DETAIL WHERE DND_CODE = '" +id+ "'";
				conn.ExecuteQuery();
				pendingStatus	= conn.GetFieldValue("CH_STA");
				

				conn.QueryString = "SELECT * FROM RFDOCNOTARY_DETAIL WHERE DND_CODE = '" +id+ "'";
				conn.ExecuteQuery();

				jumlah = conn.GetRowCount();	
				desc_old = conn.GetFieldValue("DND_DESC");
				doc_old = conn.GetFieldValue("DNM_CODE");
				col_old = conn.GetFieldValue("COL_TYPE");

				if (pendingStatus == "0" || pendingStatus == "1") 
				{
					if (jumlah > 0) 
					{
						if(desc.Trim()!= desc_old.Trim())
						{
							ExecSPAuditTrail(id,"DND_DESC",desc_old,desc,"0");
						}
						if(doc.Trim()!= doc_old.Trim())
						{
							ExecSPAuditTrail(id,"DNM_CODE",doc_old,doc,"0");
						}
						if(col.Trim()!= col_old.Trim())
						{
							ExecSPAuditTrail(id,"COL_TYPE",col_old,col,"0");
						}

						conn.QueryString = "UPDATE RFDOCNOTARY_DETAIL SET DND_DESC = '"+desc+"' , DNM_CODE='"+doc+"',COL_TYPE='"+col+"' WHERE DND_CODE = '"+id+"'";
						conn.ExecuteNonQuery();
					}
					else 
					{	
						ExecSPAuditTrail(id,"DND_CODE","",id,"1");
						ExecSPAuditTrail(id,"DND_DESC","",desc,"1");
						ExecSPAuditTrail(id,"DNM_CODE","",doc,"1");
						ExecSPAuditTrail(id,"COL_TYPE","",col,"1");

						conn.QueryString = "INSERT INTO RFDOCNOTARY_DETAIL VALUES ('"+id+"', '"+desc+"','"+doc+"', '"+col+"', '1')";
						conn.ExecuteNonQuery();	
					}
				}
				else if (pendingStatus == "2") 
				{ 
					if (jumlah > 0) 
					{
						ExecSPAuditTrail(id,"DND_CODE",id,"","2");
						ExecSPAuditTrail(id,"DND_DESC",desc,"","2");
						ExecSPAuditTrail(id,"DNM_CODE",doc,"","2");
						ExecSPAuditTrail(id,"COL_TYPE",col,"","2");

					    conn.QueryString = "UPDATE RFDOCNOTARY_DETAIL SET ACTIVE='0' WHERE DND_CODE = '"+id+"'";
						conn.ExecuteNonQuery();
					}
				}
			}

			conn.QueryString = "DELETE FROM tRFDOCNOTARY_DETAIL WHERE DND_CODE = '" +id+ "'";
			conn.ExecuteNonQuery();
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleId=40");
		}
	}
}
