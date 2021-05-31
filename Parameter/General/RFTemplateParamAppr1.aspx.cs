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


namespace CuBES_Maintenance.Parameter.General
{
	/// <summary>
	/// Create by    : Fajar
	/// Date		 : 07-02-2006
	/// Summary description for RFTemplateParamAppr.
	/// </summary>
	public partial class RFTemplateParamAppr1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.WebControls.Button updatestatus;
		protected Tools tool = new Tools();
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		protected Connection connMNT = new Connection(System.Configuration.ConfigurationSettings.AppSettings["conn"]);
		//protected Connection conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			if (!IsPostBack) 
			{				
			
				LBL_PARAMNAME.Text = Request.QueryString["name"];			
				LBL_ACTIVE.Text = Request.QueryString["active"];
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

				//set fields
				conn.QueryString = "select * from SYSCOLUMNS " + 
					"where ID in " + 
					"(select ID from SYSOBJECTS " + 
					"where NAME = '" + Request.QueryString["tablename"] + "')";
				conn.ExecuteQuery();

				try
				{
					LBL_ID.Text = conn.GetFieldValue(0,0);
					LBL_DESC.Text = conn.GetFieldValue(1,0);
				} 
				catch {}

				viewPendingData();
			}

			DGRequest.PageIndexChanged +=new DataGridPageChangedEventHandler(DGRequest_PageIndexChanged);
			//DGExisting.PageIndexChanged +=new DataGridPageChangedEventHandler(DGExisting_PageIndexChanged);
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

		private void viewPendingData() 
		{
			string tableName = Request.QueryString["tablename"];

			conn.QueryString = "select * from PENDING_" + tableName;
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				int pendcol = 2;
				if (LBL_ACTIVE.Text.Trim() == "1")
					pendcol = 3; 
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,pendcol);
				dr[3] = getPendingStatus(conn.GetFieldValue(i,pendcol));
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
				/*
				conn.QueryString = "PARAM_GENERAL_" + Request.QueryString["tablename"] + "_APPR " + id + ", '1'";
				conn.ExecuteQuery();
				*/
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
				/*
				conn.QueryString = "PARAM_GENERAL_" + Request.QueryString["tablename"] + "_APPR " + id + ", '0'";
				conn.ExecuteQuery();
				*/
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

			if (approvalFlag == "1")	// approved
			{
				/// ambil data yang di request
				/// 
				conn.QueryString = "SELECT * FROM PENDING_" +Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ " = '" +id+ "'";
				conn.ExecuteQuery();
				pendingStatus	= conn.GetFieldValue("PENDINGSTATUS");
				desc			= conn.GetFieldValue(0,1);
				conn.ClearData();

				/// audit perubahan parameter 
				/// 
				AuditTrail(id,desc,pendingStatus);

				/// memeriksa data yang existing
				/// 
				conn.QueryString = "SELECT COUNT(*) JUMLAH FROM " +Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ " = '"+id+"'";
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
						conn.QueryString = "UPDATE " +Request.QueryString["tablename"]+ " SET ["+
							getColumnDesc()+"] = '"+desc+"', ACTIVE = '"+LBL_ACTIVE.Text.Trim()+"' WHERE "+getColumnKey()+" = '"+id+"'";
						conn.ExecuteNonQuery();

						connMNT.QueryString = "UPDATE " +Request.QueryString["tablename"]+ " SET ["+
							getColumnDesc()+"] = '"+desc+"', ACTIVE = '"+LBL_ACTIVE.Text.Trim()+"' WHERE "+getColumnKey()+" = '"+id+"'";

//						Response.Write(connMNT.QueryString);
						connMNT.ExecuteNonQuery();
					}
					else 
					{
						/// Kalau belum ada, insert
						/// SME Department
						conn.QueryString = "select c.name from syscolumns c inner join sysobjects o on o.id = c.id " +
							"where o.name = '" + Request.QueryString["tablename"] + "' order by colorder ";
						conn.ExecuteQuery();

						if(LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "INSERT INTO " + Request.QueryString["tablename"] +
								" ( " + conn.GetFieldValue(0, 0) + " , " + conn.GetFieldValue(1, 0) + " , ACTIVE ) " +
								" VALUES ('"+id+"', '"+desc+"', '1')";
						else
							conn.QueryString = "INSERT INTO " +Request.QueryString["tablename"]+
								" ( " + conn.GetFieldValue(0, 0) + " , " + conn.GetFieldValue(1, 0) + " ) " +
								" VALUES ('"+id+"', '"+desc+"')";
						conn.ExecuteNonQuery();

						/// Kalau belum ada, insert
						/// MNT Department
						connMNT.QueryString = "select c.name from syscolumns c inner join sysobjects o on o.id = c.id " +
							"where o.name = '" + Request.QueryString["tablename"] + "' order by colorder ";
						connMNT.ExecuteQuery();

						if(LBL_ACTIVE.Text.Trim() == "1")
							connMNT.QueryString = "INSERT INTO " + Request.QueryString["tablename"] +
								" ( " + conn.GetFieldValue(0, 0) + " , " + conn.GetFieldValue(1, 0) + " , ACTIVE ) " +
								" VALUES ('"+id+"', '"+desc+"', '1')";
						else
							connMNT.QueryString = "INSERT INTO " +Request.QueryString["tablename"]+
								" ( " + conn.GetFieldValue(0, 0) + " , " + conn.GetFieldValue(1, 0) + " ) " +
								" VALUES ('"+id+"', '"+desc+"')";
						connMNT.ExecuteNonQuery();
					}
				}
				else if (pendingStatus == "2")	// delete
				{
					if (jumlah > 0) 
					{
						if (LBL_ACTIVE.Text.Trim() == "1")
						{
							conn.QueryString = "UPDATE "+Request.QueryString["tablename"]+" SET ACTIVE = '0' WHERE "+getColumnKey()+"= '"+id+"'";
							connMNT.QueryString = "UPDATE "+Request.QueryString["tablename"]+" SET ACTIVE = '0' WHERE "+getColumnKey()+"= '"+id+"'";
						}
						else
						{
							conn.QueryString = "DELETE FROM " +Request.QueryString["tablename"]+ " WHERE "+getColumnKey()+" = '"+id+"'";
							connMNT.QueryString = "DELETE FROM " +Request.QueryString["tablename"]+ " WHERE "+getColumnKey()+" = '"+id+"'";
						}
						conn.ExecuteNonQuery();
						connMNT.ExecuteNonQuery();
					}
				}
			}

			/// delete data dari pending
			/// 
			conn.QueryString = "DELETE FROM PENDING_" +Request.QueryString["tablename"]+ " WHERE "+getColumnKey()+" = '"+id+"'";
			conn.ExecuteNonQuery();			
		}

		private void AuditTrail(string id,string desc,string pending_status)
		{
			conn.ClearData();
			string desc_old = "";
			string action	= "";

			//Get Current Data
			conn.QueryString = "SELECT * FROM " + Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ " = '" +id+ "'";
			conn.ExecuteQuery();
			desc_old		= conn.GetFieldValue(getColumnDesc());
			action			= pending_status;
			conn.ClearData();

			if (action == "0")	// Update
			{
				if (desc != desc_old)
					ExecSPAuditTrail(id,getColumnDesc(),desc_old, desc, action);
			} 
			else if (action == "1")	// Insert
			{
				ExecSPAuditTrail(id,getColumnKey(), "", id, action);
				ExecSPAuditTrail(id,getColumnDesc(), "", desc, action);
				ExecSPAuditTrail(id,"ACTIVE", "", "1", action);
			} 
			else if (action == "2") // Delete
			{
				ExecSPAuditTrail(id,getColumnKey(), id, "", action);
				ExecSPAuditTrail(id,getColumnDesc(), desc_old, "", action);
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
				"" + "'," + "'01','" + this.LBL_PARAMNAME.Text.Trim() + "'";
			conn.ExecuteNonQuery();
			conn.ClearData();
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
			string classid="";
			try 
			{
				classid=Request.QueryString["classid"].ToString();
			}
			catch{ classid="";}
			
			if ((classid.Equals("01")) || (classid.ToString().Trim()=="01") )
					Response.Redirect("../HostParamApproval.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+" ");
				else
					Response.Redirect("../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}
	}		
}
