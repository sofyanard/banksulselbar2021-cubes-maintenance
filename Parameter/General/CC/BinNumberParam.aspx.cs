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
	/// Summary description for BinNumberParam.
	/// </summary>
	public partial class BinNumberParam : System.Web.UI.Page
	{
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn2,conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				LBL_TABLENAME.Text = Request.QueryString["tablename"];
				LBL_TITLE.Text = Request.QueryString["title"];
				string tablename = Request.QueryString["tablename"];
				if (tablename.Trim() == "RFOTHERSOURCEOFINCOME" || tablename.Trim() == "RFPAIDUPCAPITALCODE" || tablename.Trim() == "RFFREKRELOAD")
				{
					TXT_DESC.CssClass = "";
					DDL_BN_CODE.Visible = false;
					TXT_CODE.Visible = true;
					TXT_DESC.Width = 250;
					LBL_CODE.Text = "Code";
					LBL_DESC.Text = "Description";
					DGR_EXISTING1.Columns[1].HeaderText = "Code";
					DGR_EXISTING1.Columns[2].HeaderText = "Description";
					DGR_REQUEST.Columns[1].HeaderText = "code";
					DGR_REQUEST.Columns[2].HeaderText = "Description";
					if (tablename.Trim() == "RFFREKRELOAD")
					{
						TXT_DESC.MaxLength = 30;
						TXT_CODE.MaxLength = 10;
						TXT_CODE.Width = 100;
					}
					else
						TXT_DESC.MaxLength = 50;
				}
				else
					GlobalTools.fillRefList(DDL_BN_CODE,"select * from VW_RFBANK order by BANKNAME",conn2);	
				ViewExistingData();
			}
			ViewPendingData();
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			this.DGR_REQUEST.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_REQUEST_PageIndexChanged);
			this.DGR_EXISTING1.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_EXISTING1_PageIndexChanged);
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
			this.DGR_EXISTING1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING1_ItemCommand);
			this.DGR_EXISTING1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING1_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

		}
		#endregion

		private void SetDBConn2()
		{
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			string DB_NAMA = conn.GetFieldValue("DB_NAMA");
			string DB_IP = conn.GetFieldValue("DB_IP");
			string DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conn2 = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void ClearBoxes(string code, string desc, bool sta)
		{
			if (DDL_BN_CODE.Visible == true)
			{
				DDL_BN_CODE.SelectedValue	= code;
				DDL_BN_CODE.Enabled			= sta;
			}
			else
			{
				TXT_CODE.Text		= code;
				TXT_CODE.Enabled	= sta;
			}
			TXT_DESC.Text = desc;
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ViewExistingData()
		{
			conn2.QueryString = "select * from VW_PARAM_"+LBL_TABLENAME.Text.Trim()+" where ACTIVE ='1' Order BY CODE, DES";
			conn2.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			DGR_EXISTING1.DataSource = dt;
			try
			{
				DGR_EXISTING1.DataBind();
			}
			catch
			{
				DGR_EXISTING1.CurrentPageIndex = DGR_EXISTING1.PageCount - 1;
				DGR_EXISTING1.DataBind();
			}
		}

		private void ViewPendingData()
		{
			conn2.QueryString = "select * from VW_PENDING_CC_"+LBL_TABLENAME.Text;
			conn2.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			DGR_REQUEST.DataSource = dt;
			try
			{
				DGR_REQUEST.DataBind();
			}
			catch
			{
				DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				DGR_REQUEST.DataBind();
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
			Response.Redirect("../../HostParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearBoxes("","",true);
			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{ 
			string code = "", sql = "", des = "", flag = "1";
			bool sta = true;
			if (DDL_BN_CODE.Visible == true)
			{
				code = DDL_BN_CODE.SelectedValue;
				if (DDL_BN_CODE.Enabled == false) sta = false;
			}
			else
			{
				code = TXT_CODE.Text;
				if (TXT_CODE.Enabled == false) sta = false;
			}

			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				if (LBL_TABLENAME.Text.Trim() == "BIN_NUMBER")
				{
					if (TXT_DESC.Text.Trim() != LBL_BIN.Text.Trim() && DDL_BN_CODE.Visible == true && DDL_BN_CODE.Enabled == false && LBL_BIN.Text.Trim() != "")
					{
						des = LBL_BIN.Text;
						flag = "2";
					}
					else
						des = TXT_DESC.Text;
					sql = " and DES='"+des+"'";
				}
				conn2.QueryString = "select code from VW_PARAM_"+LBL_TABLENAME.Text+" where CODE ='"+ code +"'"+sql;
				conn2.ExecuteQuery();
				if (conn2.GetRowCount()>0)
				{
					GlobalTools.popMessage(this, "This Data Already exist");
					return;
				}
				conn2.ClearData();
				if (sta)
				{
					conn2.QueryString = "select code from VW_PENDING_CC_"+LBL_TABLENAME.Text+" where CODE ='"+ code +"'"+sql;
					conn2.ExecuteQuery();
					if (conn2.GetRowCount()>0)
					{
						GlobalTools.popMessage(this, "This Data Already exist");
						return;
					}
					conn2.ClearData();
				}
			}
			else if (LBL_SAVEMODE.Text.Trim() == "0" && LBL_TABLENAME.Text.Trim() == "BIN_NUMBER")
			{
				conn2.QueryString = "select code from VW_PARAM_"+LBL_TABLENAME.Text+" where CODE ='"+ code +"' and des='"+TXT_DESC.Text+"'";
				conn2.ExecuteQuery();
				if (conn2.GetRowCount()>0)
				{
					GlobalTools.popMessage(this, "This Data Allready exist");
					return;
				}
				conn2.ClearData();

				conn2.QueryString = "select code from VW_PENDING_CC_"+LBL_TABLENAME.Text+" where CODE ='"+ code +"' and des='"+TXT_DESC.Text+"'";
				conn2.ExecuteQuery();
				if (conn2.GetRowCount()>0)
				{
					GlobalTools.popMessage(this, "This Data Allready exist");
					return;
				}
				conn2.ClearData();
			}
			conn2.QueryString = "exec PARAM_GENERAL_CC_BIN_NUMBER '"+LBL_SAVEMODE.Text+"', '"+code
				+"', '"+TXT_DESC.Text+"', 'PENDING_CC_"+LBL_TABLENAME.Text+"', '"+flag+"', '"+LBL_BIN.Text+"' ";
			conn2.ExecuteNonQuery();
			LBL_SAVEMODE.Text = "1";
			ViewPendingData();
			ClearBoxes("","",true);
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes("","",true);
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[4].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
					else
					{
						ClearBoxes(CleansText(e.Item.Cells[0].Text),CleansText(e.Item.Cells[2].Text),false);
						LBL_BIN.Text = CleansText(e.Item.Cells[2].Text);
					}
					break;

				case "delete":
					conn2.QueryString = "exec PARAM_GENERAL_CC_BIN_NUMBER '"+e.Item.Cells[4].Text+"', '"+e.Item.Cells[0].Text
						+"', '"+e.Item.Cells[2].Text+"', 'PENDING_CC_"+LBL_TABLENAME.Text+"', '0', '"+LBL_BIN.Text+"' ";
					conn2.ExecuteNonQuery();
					ViewPendingData();
					break;
				default :
					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_EXISTING1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING1.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_EXISTING1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes("","",true);
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":					
					conn2.QueryString = "exec PARAM_GENERAL_CC_BIN_NUMBER '2', '"+e.Item.Cells[0].Text
						+"', '"+e.Item.Cells[2].Text+"', 'PENDING_CC_"+LBL_TABLENAME.Text+"', '1', '"+LBL_BIN.Text+"'";
					conn2.ExecuteNonQuery();
					ViewPendingData();
					break;
				case "edit":
					LBL_SAVEMODE.Text = "0";
					ClearBoxes(CleansText(e.Item.Cells[0].Text),CleansText(e.Item.Cells[2].Text),false);
					LBL_BIN.Text = CleansText(e.Item.Cells[2].Text);
					break;
				default :
					break;
			}
		}
	}
}
