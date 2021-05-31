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
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.CC
{
	/// <summary>
	/// Summary description for CustCategoryAppr.
	/// </summary>
	public partial class RFTemplateParamCCICSAppr : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.WebControls.Button updatestatus;
		protected Connection conn;// = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack) 
			{				
				LBL_PARAMNAME.Text = Request.QueryString["name"];			
				LBL_ACTIVE.Text = Request.QueryString["active"];
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

				//set fields
				conn2.QueryString = "select * from SYSCOLUMNS " + 
					"where ID in " + 
					"(select ID from SYSOBJECTS " + 
					"where NAME = '" + Request.QueryString["tablename"] + "')";
				conn2.ExecuteQuery();

				try
				{
					LBL_ID.Text = conn2.GetFieldValue(0,0);
					LBL_DESC.Text = conn2.GetFieldValue(1,0);
					//15-08-2005
					this.LBL_SICS.Text = conn2.GetFieldValue(2,0);
					//this.LBL_SICS.Text = "SICS_ID";
				} 
				catch {}

				viewPendingData();
			}

			DGRequest.PageIndexChanged +=new DataGridPageChangedEventHandler(DGRequest_PageIndexChanged);
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
			string DB_NAMA;
			string DB_IP;
			string DB_LOGINID;
			string DB_LOGINPWD;
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			DB_NAMA = conn.GetFieldValue("DB_NAMA");
			DB_IP = conn.GetFieldValue("DB_IP");
			DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conn2 = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void viewPendingData() 
		{
			string tableName = Request.QueryString["tablename"];

			conn2.QueryString = "select * from PENDING_CC_" + tableName;
			conn2.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("SICS_ID"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS1"));			

			DataRow dr;
			for(int i = 0; i < conn2.GetDataTable().Rows.Count; i++) 
			{
				int pendcol = 3;
				if (LBL_ACTIVE.Text.Trim() == "1")
					pendcol = 4; 
				dr = dt.NewRow();
				dr[0] = conn2.GetFieldValue(i,0);
				dr[1] = conn2.GetFieldValue(i,1);
				dr[2] = conn2.GetFieldValue(i,2);
				dr[3] = conn2.GetFieldValue(i,pendcol);
				dr[4] = getPendingStatus(conn2.GetFieldValue(i,pendcol));
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

		private string CheckApost(string str)
		{
			return str.Replace("'", "''").Trim();
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

		private string getColumnKey() 
		{
			return LBL_ID.Text.Trim();
		}
		
		private string getColumnDesc1() 
		{
			return this.LBL_DESC.Text.Trim();
		}

		private string getColumnDesc2() 
		{
			return this.LBL_SICS.Text.Trim();
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
				executeApproval(id, "1");
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				executeApproval(id, "0");
			} 
			catch {}
		}

		private void AuditTrail(string id,string desc, string sics, string pending_status)
		{
			string desc_old = "";
			string sics_old = "";
			string action	= "";
			//Get Current Data
			conn2.QueryString = "SELECT * FROM " + Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ " = '" +id+ "'";
			conn2.ExecuteQuery();
			desc_old		= conn2.GetFieldValue(getColumnDesc1());
			sics_old		= conn2.GetFieldValue(getColumnDesc2());
			action			= pending_status;
			conn2.ClearData();

			if (action == "0")
			{
				if (desc != desc_old)
					ExecSPAuditTrail(id,getColumnDesc1(),desc_old, desc, action);
				if (sics != sics_old)
					ExecSPAuditTrail(id,getColumnDesc2(),sics_old, sics, action);
			} 
			else if (action == "1")
			{
				ExecSPAuditTrail(id,getColumnKey(), "", id, action);
				ExecSPAuditTrail(id,getColumnDesc1(), "", desc, action);
				ExecSPAuditTrail(id,getColumnDesc2(), "", sics, action);
				ExecSPAuditTrail(id,"ACTIVE", "", "1", action);
			} 
			else if (action == "2")
			{
				ExecSPAuditTrail(id,getColumnKey(), id, "", action);
				ExecSPAuditTrail(id,getColumnDesc1(), desc_old, "", action);
				ExecSPAuditTrail(id,getColumnDesc2(), sics_old, "", action);
				if (LBL_ACTIVE.Text.Trim() == "1")
					ExecSPAuditTrail(id,"ACTIVE", "1", "0" , action);
			}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = Request.QueryString["tablename"];
			conn2.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','" + tablename + "','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "'01','" + this.LBL_PARAMNAME.Text.Trim() + "'";
			//Response.Write(conn2.QueryString);
			conn2.ExecuteNonQuery();
			conn2.ClearData();
		}


		private void executeApproval(string id, string approvalFlag) 
		{
			string desc,sics, pendingStatus;
			string query = "";
			int jumlah;

			if (approvalFlag == "1") 
			{
				conn2.QueryString = "SELECT * FROM PENDING_CC_" +Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ " = '" +id+ "'";
				conn2.ExecuteQuery();
				pendingStatus	= conn2.GetFieldValue("PENDING_STATUS");
				desc			= conn2.GetFieldValue(0,1);
				sics			= conn2.GetFieldValue(0,2);
				conn2.ClearData();

				AuditTrail(id,desc,sics,pendingStatus);

				conn2.QueryString = "SELECT count(*) JUMLAH FROM " +Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ " = '"+id+"'";
				conn2.ExecuteQuery();
				try   {jumlah = Convert.ToInt16(conn2.GetFieldValue("JUMLAH"));} 
				catch {jumlah =0;}
				conn2.ClearData();

				if (pendingStatus == "0" || pendingStatus == "1") //update/insert
				{
					if (jumlah > 0) 
					{
						query = "UPDATE " +Request.QueryString["tablename"]+ " SET "+
							getColumnDesc1()+" = '"+CheckApost(desc)+ "'," + getColumnDesc2()+" = '"+sics+
							"', ACTIVE='1' WHERE "+getColumnKey()+" = '"+id+"'";
					}
					else //insert
					{
						if(LBL_ACTIVE.Text.Trim() == "1")
							query = "INSERT INTO " +Request.QueryString["tablename"]+
								"(" + getColumnKey() +"," + getColumnDesc1() +","+ getColumnDesc2() +",ACTIVE) VALUES ('"+
								id + "', '" + CheckApost(desc) + "','" + sics + "', '1')";
						else
							query = "INSERT INTO " +Request.QueryString["tablename"]+
								"("+ getColumnKey() + "," + getColumnDesc1()+"," + getColumnDesc2() +
								") VALUES ('"+id+"', '"+CheckApost(desc)+ "','" + sics +"',)";
						
					}
				}
				else if (pendingStatus == "2") 
				{
					if (LBL_ACTIVE.Text.Trim() == "1")
						query = "UPDATE "+Request.QueryString["tablename"]+" SET ACTIVE = '0' WHERE "+getColumnKey()+"= '"+id+"'";
					else
						query = "DELETE FROM " +Request.QueryString["tablename"]+ " WHERE "+getColumnKey()+" = '"+id+"'";
				}

				conn2.QueryString = query;
				conn2.ExecuteQuery();
				conn2.ClearData();
			}
			
			conn2.QueryString = "DELETE FROM PENDING_CC_" +Request.QueryString["tablename"]+ " WHERE "+getColumnKey()+" = '"+id+"'";
			conn2.ExecuteQuery();
			conn2.ClearData();
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
			Response.Redirect("../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}		
}
