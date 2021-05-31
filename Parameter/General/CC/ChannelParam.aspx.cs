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
	/// Summary description for OmsetParam.
	/// </summary>
	public partial class OmsetParam : System.Web.UI.Page
	{
		protected Connection conn2,conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				LBL_TABLENAME.Text = Request.QueryString["tablename"];
				TR_MODULE.Visible			= false;
				/*
				if (LBL_TABLENAME.Text	== "RFCHANNEL")
				{
					LBL_LEFT_TITLE.Text	= "Parameter Maintenance : Common Maker";
					TR_MODULE.Visible			= true;
					
				} 
				else
				{
					TR_MODULE.Visible			= false;
				}
				*/
				LBL_TITLE.Text = Request.QueryString["title"];
				string tablename = Request.QueryString["tablename"];
				if (tablename.Trim() == "RFSTATUSEMP")
				{
					LBL_CODE.Text = "Status ID";
					LBL_DESC.Text = "Status Description";
					DGR_EXISTING1.Columns[0].HeaderText = "Status ID";
					DGR_EXISTING1.Columns[1].HeaderText = "Status Description";
					DGR_REQUEST.Columns[0].HeaderText = "Status ID";
					DGR_REQUEST.Columns[1].HeaderText = "Status Description";
				}
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

		private void ClearBoxes(string code, string desc, string sics, bool sta)
		{
			TXT_CODE.Text		= code;
			TXT_DESC.Text		= desc;
			TXT_SICS.Text		= sics;
			TXT_CODE.Enabled	= sta;
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
			/*if (LBL_TABLENAME.Text == "RFCHANNEL")
				Response.Redirect("../../CommonParam.aspx?mc=99020201"); 
			else
			*/
				Response.Redirect("../../HostParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearBoxes("","","",true);
			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			bool sta = true;
			if (TXT_CODE.Enabled == false) sta = false;

			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn2.QueryString = "select code from VW_PARAM_"+LBL_TABLENAME.Text+" where CODE ='"+ TXT_CODE.Text +"'";
				conn2.ExecuteQuery();
				if (conn2.GetRowCount()>0)
				{
					GlobalTools.popMessage(this, "This Data Allready exist");
					return;
				}
				conn2.ClearData();
				if (sta)
				{
					conn2.QueryString = "select code from VW_PENDING_CC_"+LBL_TABLENAME.Text+" where CODE ='"+ TXT_CODE.Text +"'";
					conn2.ExecuteQuery();
					if (conn2.GetRowCount()>0)
					{
						GlobalTools.popMessage(this, "This Data Allready exist");
						return;
					}
					conn2.ClearData();
				}
			}

			conn2.QueryString = "exec PARAM_GENERAL_CC_RFCHANNEL '"+LBL_SAVEMODE.Text+"', '"+TXT_CODE.Text
				+"', '"+TXT_DESC.Text+"', 'PENDING_CC_"+LBL_TABLENAME.Text+"', '1', '"+TXT_SICS.Text+"' ";
			conn2.ExecuteNonQuery();
			LBL_SAVEMODE.Text = "1";
			ViewPendingData();
			ClearBoxes("","","",true);
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes("","","",true);
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[4].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
					else
						ClearBoxes(CleansText(e.Item.Cells[0].Text),CleansText(e.Item.Cells[1].Text), CleansText(e.Item.Cells[2].Text),false);
					break;

				case "delete":
					conn2.QueryString = "exec PARAM_GENERAL_CC_RFCHANNEL '"+e.Item.Cells[4].Text+"', '"+e.Item.Cells[0].Text
						+"', '"+e.Item.Cells[2].Text+"', 'PENDING_CC_"+LBL_TABLENAME.Text+"', '0', '"+e.Item.Cells[2].Text+"' ";
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
			ClearBoxes("","","",true);
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":					
					conn2.QueryString = "exec PARAM_GENERAL_CC_RFCHANNEL '2', '"+e.Item.Cells[0].Text
						+"', '"+e.Item.Cells[1].Text+"', 'PENDING_CC_"+LBL_TABLENAME.Text+"', '1', '"+e.Item.Cells[2].Text+"'";
					conn2.ExecuteNonQuery();
					ViewPendingData();
					break;
				case "edit":
					LBL_SAVEMODE.Text = "0";
					ClearBoxes(CleansText(e.Item.Cells[0].Text),CleansText(e.Item.Cells[1].Text), CleansText(e.Item.Cells[2].Text),false);
					break;
				default :
					break;
			}
		}

		protected void RDB_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.RDB_MODULE.SelectedValue == "1" || this.RDB_MODULE.SelectedValue == "2")
				Response.Redirect("../../General/Consumer/ChannelsParam.aspx?ModuleId=40&mod="+RDB_MODULE.SelectedValue);
		}
	}
}
