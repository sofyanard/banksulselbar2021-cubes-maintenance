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

namespace CuBES_Maintenance.Parameter.General
{
	/// <summary>
	/// Summary description for RFTemplateParamConCSAppr.
	/// </summary>
	public partial class RFTemplateParamConCSAppr : System.Web.UI.Page
	{
		protected Tools tool = new Tools();
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

			conn2.QueryString = "select * from T" + tableName;
			conn2.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("CD_SIBS"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("CH_STA"));			

			DataRow dr;
			for(int i = 0; i < conn2.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn2.GetFieldValue(i,0);
				dr[1] = conn2.GetFieldValue(i,1);
				dr[2] = conn2.GetFieldValue(i,"CD_SIBS");
				dr[3] = conn2.GetFieldValue(i,"CH_STA");
				dr[4] = getPendingStatus(conn2.GetFieldValue(i,"CH_STA"));
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

		private string getColumnKey() 
		{
			return LBL_ID.Text.Trim();
		}
		
		private string getColumnDesc()
		{
			return LBL_DESC.Text.Trim();
		}

		private string getColumnDesc2()
		{
			return "CD_SIBS";
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
			catch 
			{
				Response.Write("Data cannot be entered into database");
			}
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

		private void AuditTrail(string id,string desc, string sibs, string pending_status)
		{
			string desc_old = "";
			string sibs_old = "";
			string action	= "";
			//Get Current Data
			conn2.QueryString = "SELECT * FROM " + Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ " = '" +id+ "'";
			conn2.ExecuteQuery();
			desc_old		= conn2.GetFieldValue(getColumnDesc());
			sibs_old		= conn2.GetFieldValue("CD_SIBS");
			action			= pending_status;
			conn2.ClearData();

			if (action == "0")
			{
				if (desc != desc_old)
					ExecSPAuditTrail(id,getColumnDesc(),desc_old, desc, action);
				if (sibs != sibs_old)
					ExecSPAuditTrail(id,getColumnDesc2(),sibs_old, sibs, action);
			} 
			else if (action == "1")
			{
				ExecSPAuditTrail(id,getColumnKey(), "", id, action);
				ExecSPAuditTrail(id,getColumnDesc(), "", desc, action);
				ExecSPAuditTrail(id,getColumnDesc2(), "", sibs, action);
				ExecSPAuditTrail(id,"ACTIVE", "", "1", action);
			} 
			else if (action == "2")
			{
				ExecSPAuditTrail(id,getColumnKey(), id, "", action);
				ExecSPAuditTrail(id,getColumnDesc(), desc_old, "", action);
				ExecSPAuditTrail(id,getColumnDesc2(), sibs_old, "", action);
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
			conn2.ExecuteNonQuery();
			conn2.ClearData();
		}


		private void executeApproval(string id, string approvalFlag) 
		{
			string desc, pendingStatus, cd_sibs,query;
			int jumlah2;
			query=""; desc=""; pendingStatus=""; cd_sibs="";

			if (approvalFlag == "1") 
			{
				conn2.QueryString = "SELECT * FROM T" +Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ " = '" +id+ "'";
				conn2.ExecuteQuery();
				pendingStatus	= conn2.GetFieldValue("CH_STA");
				desc			= conn2.GetFieldValue(0,1);
				cd_sibs			= conn2.GetFieldValue("CD_SIBS");
				conn2.ClearData();

				AuditTrail(id,desc,cd_sibs,pendingStatus);
				
				conn2.QueryString = "select * from "+Request.QueryString["tablename"]+" where " +getColumnKey()+ " = '" +id+ "'";
				conn2.ExecuteQuery();
				jumlah2=conn2.GetRowCount();
				conn2.ClearData();

				if (pendingStatus == "0" || pendingStatus == "1") 
				{
					if (jumlah2 > 0) 
					{
						query = "UPDATE " +Request.QueryString["tablename"]+ " SET "+
							getColumnDesc()+" = '"+desc+",CD_SIBS='"+cd_sibs+"',ACTIVE='1' WHERE "+getColumnKey()+" = '"+id+"'";
					}
					else 
					{
						if(LBL_ACTIVE.Text.Trim() == "1")
						{
							query = "INSERT INTO " +Request.QueryString["tablename"]+
								" ("+getColumnKey()+","+getColumnDesc()+",cd_sibs,active) VALUES ('"+id+"', '"+desc+"', '"+cd_sibs+"' , '1')";
						}
						else
						{
							query = "INSERT INTO " +Request.QueryString["tablename"]+
								" ("+getColumnKey()+","+getColumnDesc()+",cd_sibs) VALUES ('"+id+"', '"+desc+"', '"+cd_sibs+"')";
						}
					}
	
					try
					{
						conn2.QueryString=query;
						conn2.ExecuteNonQuery();;
					}
					catch (ApplicationException ex)
					{
						if (ex.Message.ToString().IndexOf("truncate") > 0)
							Tools.popMessage(this, "Input melebihi batas !");
					}
				}
				else if (pendingStatus == "2") 
				{
					if (jumlah2 > 0) 
					{
						if (LBL_ACTIVE.Text.Trim() == "1")
							conn2.QueryString = "UPDATE "+Request.QueryString["tablename"]+" SET ACTIVE = '0' WHERE "+getColumnKey()+"= '"+id+"'";
						else
							conn2.QueryString = "DELETE FROM " +Request.QueryString["tablename"]+ " WHERE "+getColumnKey()+" = '"+id+"'";
						conn2.ExecuteNonQuery();
					}
				}
			}

			conn2.QueryString = "DELETE FROM T" +Request.QueryString["tablename"]+ " WHERE "+getColumnKey()+" = '"+id+"'";
			conn2.ExecuteNonQuery();
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
			Response.Redirect("../HostParamApproval.aspx?mc=99020101");
		}
	}
}
