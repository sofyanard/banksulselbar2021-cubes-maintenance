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

namespace CuBES_Maintenance.Parameter.General
{
	/// <summary>
	/// Summary description for RFTemplateParamCC.
	/// </summary>
	public partial class RFTemplateParamCC : System.Web.UI.Page
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
				LBL_SAVEMODE.Text = "1";
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
				HolidayControl();
				viewExistingData();
				viewPendingData();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			DGExisting.PageIndexChanged +=new DataGridPageChangedEventHandler(DGExisting_PageIndexChanged);
			this.DGRequest.PageIndexChanged +=new DataGridPageChangedEventHandler(DGRequest_PageIndexChanged);
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
			this.DGExisting.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGExisting_ItemCommand);
			this.DGExisting.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGExisting_PageIndexChanged);
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

		private void HolidayControl()
		{
			if (Request.QueryString["tablename"] == "LIBUR" || Request.QueryString["tablename"]== "LIBUR1")
				this.LBL_LIBUR.Visible	= true;
			else
				this.LBL_LIBUR.Visible = false;
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void viewPendingData() 
		{
			string tableName = Request.QueryString["tablename"];
			int pendCol = 2;

			conn2.QueryString = "select * from PENDING_CC_" + tableName;
			conn2.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS1"));			

			if (LBL_ACTIVE.Text.Trim() == "1")
				pendCol = 4;
			DataRow dr;
			for(int i = 0; i < conn2.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn2.GetFieldValue(i,0);
				dr[1] = conn2.GetFieldValue(i,1);
				dr[2] = conn2.GetFieldValue(i,pendCol);
				dr[3] = getPendingStatus(conn2.GetFieldValue(i,pendCol));
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

		private void viewExistingData() 
		{
			string tableName = Request.QueryString["tablename"];

			if (LBL_ACTIVE.Text.Trim() == "1")
				conn2.QueryString = "select * from " + tableName + " where ACTIVE = '1'";
			else
				conn2.QueryString = "select * from " + tableName ;
			conn2.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));

			DataRow dr;
			for(int i = 0; i < conn2.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn2.GetFieldValue(i,0);
				dr[1] = conn2.GetFieldValue(i,1);
				dt.Rows.Add(dr);
			}			

			DGExisting.DataSource = new DataView(dt);
			try 
			{
				DGExisting.DataBind();
			} 
			catch 
			{
				DGExisting.CurrentPageIndex = DGExisting.PageCount - 1;
				DGExisting.DataBind();
			}
		}

		private void DGExisting_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{			
			DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			//Validasi Existensi
			if (TXT_ID.Text.Trim() == "" || TXT_DESC.Text.Trim() == "") return;
			//Validasi ID = Tanggal : (DD/MM/YYYY)
			if (Request.QueryString["tablename"] == "LIBUR" || Request.QueryString["tablename"]== "LIBUR1")
			{
				try
				{
					string tgl = GlobalTools.ToSQLDate(this.TXT_ID.Text);
				} 
				catch 
				{
					GlobalTools.popMessage(this,"Date is not valid!");
					GlobalTools.SetFocus(this,this.TXT_ID);
					return;
				}
			}

			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				string id = "";string query = "";
				id = TXT_ID.Text.Trim();
				if (Request.QueryString["tablename"] == "LIBUR" || Request.QueryString["tablename"]== "LIBUR1")
					query = "select * from " + Request.QueryString["tablename"] + " WHERE ACTIVE ='1' and " + getColumnKey() + "=" + GlobalTools.ConvertDate(id);
				else
					query = "select * from " + Request.QueryString["tablename"] + " WHERE ACTIVE ='1' and " + getColumnKey() + "= '" + id + "'";
			    
				conn2.QueryString = query;
				conn2.ExecuteQuery();
				
				if (conn2.GetRowCount() > 0) 
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					GlobalTools.SetFocus(this,this.TXT_ID);
					return;
				}
			}		
	
			executeMaker(TXT_ID.Text.Trim(), TXT_DESC.Text.Trim(), LBL_SAVEMODE.Text.Trim());
			viewPendingData();
			clearControls();

			LBL_SAVEMODE.Text = "1";
		}

		private void clearControls() 
		{
			TXT_ID.Text   = "";
			TXT_DESC.Text = "";
			activateControlKey(false);
		}

		private void activateControlKey(bool isReadOnly) 
		{
			TXT_ID.ReadOnly = isReadOnly;
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{		
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[2].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					string idku = CleansText(e.Item.Cells[0].Text);
					if (Request.QueryString["tablename"] == "LIBUR" || Request.QueryString["tablename"]== "LIBUR1")
						TXT_ID.Text = idku.Substring(0,10);
					else
						TXT_ID.Text = idku;
					TXT_DESC.Text = CleansText(e.Item.Cells[1].Text);
					activateControlKey(true);
					break;

				case "delete":
					string id = CleansText(e.Item.Cells[0].Text);
					if (Request.QueryString["tablename"] == "LIBUR" || Request.QueryString["tablename"]== "LIBUR1")
					{
						id = GlobalTools.ToSQLDate(id);
						id = id.Replace("'","");
					}
					string colKey = "";
					colKey = getColumnKey();
					conn2.QueryString = "delete from PENDING_CC_" + Request.QueryString["tablename"] + " WHERE " + colKey + "='" + id + "'";
					conn2.ExecuteQuery();
					viewPendingData();
					break;
				default :
					break;
			}
		}

		private string getColumnKey() 
		{
			return LBL_ID.Text.Trim();
		}

		private string getColumnDesc()
		{
			return LBL_DESC.Text.Trim();
		}

		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_ID.Text = CleansText(e.Item.Cells[0].Text);
					TXT_DESC.Text = CleansText(e.Item.Cells[1].Text);
					activateControlKey(true);
					break;

				case "delete":					
					string id	= CleansText(e.Item.Cells[0].Text.Trim());
					string desc = CleansText(e.Item.Cells[1].Text.Trim());
					/*
					conn2.QueryString = "exec PARAM_GENERAL_CC_" + Request.QueryString["tablename"] + "_MAKER '2','" +
						id + "','" + e.Item.Cells[1].Text + "'";
					conn2.ExecuteQuery();
					*/
					//executeMaker(TXT_ID.Text.Trim(), TXT_DESC.Text.Trim(), LBL_SAVEMODE.Text.Trim());
					executeMaker(id,desc,"2");
					viewPendingData();
					break;

				default :
					break;
			}
		}

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		private void ExecuteSQLHandling()
		{
			try 
			{
				conn2.ExecuteQuery();
			} 
			catch (ApplicationException ex) 
			{
				if (ex.Message.ToString().IndexOf("truncate") > 0)
				{
					GlobalTools.popMessage(this, "Input melebihi batas !");
					GlobalTools.SetFocus(this,TXT_ID);
				}
				return;
			}
		}
		
		private void executeMaker(string id, string desc, string pendingStatus) 
		{
			if (Request.QueryString["tablename"] == "LIBUR" || Request.QueryString["tablename"]== "LIBUR1")
			{
				id = GlobalTools.ToSQLDate(id);
				id = id.Replace("'","");
			}
			conn2.QueryString = "SELECT COUNT(*) JUMLAH FROM PENDING_CC_" +Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ "='" +id+ "'";
			conn2.ExecuteQuery();
			int jumlah = Convert.ToInt16(conn2.GetFieldValue("JUMLAH"));
			if (jumlah > 0) 
			{				
				conn2.QueryString = "UPDATE PENDING_CC_" +Request.QueryString["tablename"]+ " SET " + getColumnDesc()+
					"= '" +desc+ "', PENDING_STATUS = '" +pendingStatus+ "' WHERE "+getColumnKey()+ "= '"+id+"'";
				ExecuteSQLHandling();
			}
			else 
			{
				if (LBL_ACTIVE.Text.Trim() == "1")
					conn2.QueryString = "INSERT INTO PENDING_CC_" + Request.QueryString["tablename"] +
						"("+ getColumnKey() + "," + getColumnDesc() + ",PENDING_STATUS,ACTIVE) VALUES ('"+
						id+"', '"+desc+"','" + pendingStatus + "','1')";
				else if (LBL_ACTIVE.Text.Trim() == "0")
					conn2.QueryString = "INSERT INTO PENDING_CC_" + Request.QueryString["tablename"] +
						"("+ getColumnKey() + "," + getColumnDesc() + ",PENDING_STATUS) VALUES ('"+
						id+"', '"+desc+"','" + pendingStatus + "')";
				ExecuteSQLHandling();
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();
		}
	}		
}
