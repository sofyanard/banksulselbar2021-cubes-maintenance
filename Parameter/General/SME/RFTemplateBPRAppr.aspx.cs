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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFTemplateBPRAppr.
	/// </summary>
	public partial class RFTemplateBPRAppr : System.Web.UI.Page
	{
		protected Connection conn, conn2;
		private string thirdfld = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			if (!IsPostBack) 
			{
				string DB_NAMA, DB_IP, DB_LOGINID, DB_LOGINPWD, conn2str;
				conn.QueryString = "select DB_NAMA, DB_IP, DB_LOGINID, DB_LOGINPWD from RFMODULE " +
					"where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
				conn.ExecuteQuery();
				DB_NAMA = conn.GetFieldValue("DB_NAMA");
				DB_IP = conn.GetFieldValue("DB_IP");
				DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
				DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
				conn2str = "Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + 
					DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true";
				ViewState["conn2str"] = conn2str;
				conn2 = new Connection (conn2str);

				LBL_PARAMNAME.Text = Request.QueryString["name"];			
				LBL_ACTIVE.Text = Request.QueryString["active"];
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

				//set fields
				conn2.QueryString = "select [name] from SYSCOLUMNS " + 
					"where ID in " + 
					"(select ID from SYSOBJECTS " + 
					"where NAME = '" + Request.QueryString["tablename"] + "') order by colid";
				conn2.ExecuteQuery();

				try
				{
					LBL_ID.Text = conn2.GetFieldValue(0,0);
					LBL_DESC.Text = conn2.GetFieldValue(1,0);
					if (Request.QueryString["thirdfld"] != null)
					{
						thirdfld = Request.QueryString["thirdfld"];
						for (int i = 2; i < conn2.GetRowCount(); i++)
							if (conn2.GetFieldValue(i,0) == thirdfld)
							{
								DGRequest.Columns[2].Visible = true;
								if (Request.QueryString["thirdhdr"] != null)
									DGRequest.Columns[2].HeaderText = Request.QueryString["thirdhdr"];
								else
									DGRequest.Columns[2].HeaderText = thirdfld;
							}
					}
					ViewState["thirdfld"] = thirdfld;
				}
				catch {}

				viewPendingData();
			}
			else
			{
				conn2 = new Connection ((string)ViewState["conn2str"]);
				thirdfld = (string) ViewState["thirdfld"];
			}
		}

		private bool hasThirdField()
		{
			return thirdfld != "";
		}

		private void viewPendingData() 
		{
			string tableName = Request.QueryString["tablename"];
			//int pendcol=0;
			conn2.QueryString = "select * from PENDING_" + tableName;
			conn2.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			
			dt.Columns.Add(new DataColumn("THIRDFIELD"));

			DataRow dr;
			for(int i = 0; i < conn2.GetDataTable().Rows.Count; i++) 
			{
				/*pendcol = 2;
				if (LBL_ACTIVE.Text.Trim() == "1")
					pendcol = 4; */
				dr = dt.NewRow();
				dr[0] = conn2.GetFieldValue(i,0);
				dr[1] = conn2.GetFieldValue(i,1);
				dr[2] = conn2.GetFieldValue(i,"PENDINGSTATUS");
				dr[3] = getPendingStatus(conn2.GetFieldValue(i,"PENDINGSTATUS"));
				dr[4] = hasThirdField() ? conn2.GetFieldValue(i,thirdfld) : "";
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

		private void executeApproval(string id, string approvalFlag) 
		{
			string desc, pendingStatus, cdsibs;
			int jumlah;

			if (approvalFlag == "1") 
			{
				conn2.QueryString = "SELECT * FROM PENDING_" +Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ " = '" +id+ "'";
				conn2.ExecuteQuery();
				pendingStatus	= conn2.GetFieldValue("PENDINGSTATUS");
				desc			= conn2.GetFieldValue(0,1);
				cdsibs			= hasThirdField() ? conn2.GetFieldValue(0,thirdfld) : "";
				conn2.ClearData();

				AuditTrail(id,desc,cdsibs,pendingStatus);

				conn2.QueryString = "SELECT COUNT(*) JUMLAH FROM " +Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ " = '"+id+"'";
				conn2.ExecuteQuery();
				try
				{jumlah = Convert.ToInt16(conn2.GetFieldValue("JUMLAH"));} 
				catch {jumlah =0;}
				conn2.ClearData();
				
				if (pendingStatus == "0" || pendingStatus == "1") 
				{
					if (jumlah > 0) 
					{
						if(LBL_ACTIVE.Text.Trim() == "1")
							conn2.QueryString = "UPDATE " +Request.QueryString["tablename"]+ " SET "+
                                getColumnDesc()+" = '"+desc+"',ACTIVE='1' WHERE "+getColumnKey()+" = '"+id+"'";
						else
							conn2.QueryString = "UPDATE " +Request.QueryString["tablename"]+ " SET "+
								getColumnDesc()+" = '"+desc+"' WHERE "+getColumnKey()+" = '"+id+"'";
						conn2.ExecuteNonQuery();
					}
					else 
					{
						if(LBL_ACTIVE.Text.Trim() == "1")
							conn2.QueryString = "INSERT INTO " +Request.QueryString["tablename"]+
								"("+getColumnKey()+","+getColumnDesc()+",active) VALUES ('"+id+"', '"+desc+"','1')";
						else
							conn2.QueryString = "INSERT INTO " +Request.QueryString["tablename"]+
								"("+getColumnKey()+","+getColumnDesc()+") VALUES ('"+id+"', '"+desc+"')";
						conn2.ExecuteNonQuery();
					}
					if (hasThirdField())
					{
						try
						{
							conn2.QueryString = "UPDATE " +Request.QueryString["tablename"] +
								" SET " + thirdfld + " = '" + cdsibs + "' WHERE " + getColumnKey() + "= '" + id + "'";
							conn2.ExecuteNonQuery();
						} 
						catch {}
					}
				}
				else if (pendingStatus == "2") 
				{
					if (jumlah > 0) 
					{
						if (LBL_ACTIVE.Text.Trim() == "1")
							conn2.QueryString = "UPDATE "+Request.QueryString["tablename"]+" SET ACTIVE = '0' WHERE "+getColumnKey()+"= '"+id+"'";
						else
							conn2.QueryString = "DELETE FROM " +Request.QueryString["tablename"]+ " WHERE "+getColumnKey()+" = '"+id+"'";
						conn2.ExecuteNonQuery();
					}
				}
			}
			conn2.QueryString = "DELETE FROM PENDING_" +Request.QueryString["tablename"]+ " WHERE "+getColumnKey()+" = '"+id+"'";
			conn2.ExecuteNonQuery();
		}

		private void AuditTrail(string id,string desc,string cdsibs,string pending_status)
		{
			conn2.ClearData();
			string desc_old = "";
			string cdsibs_old = "";
			string action	= "";
			//Get Current Data
			conn2.QueryString = "SELECT * FROM " + Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ " = '" +id+ "'";
			conn2.ExecuteQuery();
			desc_old		= conn2.GetFieldValue(getColumnDesc());
			if (hasThirdField())
				cdsibs_old = conn2.GetFieldValue(thirdfld);
			action			= pending_status;
			conn2.ClearData();

			if (action == "0")
			{
				if (desc != desc_old)
					ExecSPAuditTrail(id, getColumnDesc(), desc_old, desc, action);
				if (cdsibs != cdsibs_old)
					ExecSPAuditTrail(id, thirdfld, cdsibs_old, cdsibs, action);
			} 
			else if (action == "1")
			{
				ExecSPAuditTrail(id,getColumnKey(), "", id, action);
				ExecSPAuditTrail(id,getColumnDesc(), "", desc, action);
				if (hasThirdField())
					ExecSPAuditTrail(id, thirdfld, "", cdsibs, action);
				ExecSPAuditTrail(id,"ACTIVE", "", "1", action);
			} 
			else if (action == "2")
			{
				ExecSPAuditTrail(id,getColumnKey(), id, "", action);
				ExecSPAuditTrail(id,getColumnDesc(), desc_old, "", action);
				if (hasThirdField())
					ExecSPAuditTrail(id, thirdfld, "", cdsibs, action);
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
				"" + "'," + "'1','" + this.LBL_PARAMNAME.Text.Trim() + "'";
			conn2.ExecuteNonQuery();
			conn2.ClearData();
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string bcklnk = "../../GeneralParamApprovalAll.aspx?a=1";
			if (Request.QueryString["mc"] != null)
				bcklnk += "&mc=" + Request.QueryString["mc"];
			if (Request.QueryString["moduleID"] != null)
				bcklnk += "&moduleID=" + Request.QueryString["moduleID"];
			if (Request.QueryString["pg"] != null)
				bcklnk += "&pg=" + Request.QueryString["pg"];
			Response.Redirect(bcklnk);
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

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;			
			viewPendingData();
		}
	}
}
