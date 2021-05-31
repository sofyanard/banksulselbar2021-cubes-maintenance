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

namespace CuBES_Maintenance.Parameter.General.JiwaService
{
	/// <summary>
	/// Summary description for GroupParamAll.
	/// </summary>
	public class GroupParamAll : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton BTN_BACK;
		protected System.Web.UI.WebControls.Button BTN_SAVE;
		protected System.Web.UI.WebControls.Button BTN_CLEAR;
		protected System.Web.UI.HtmlControls.HtmlTableRow TR_GROUP;
		protected System.Web.UI.WebControls.TextBox TXT_GRPCODE;
		protected System.Web.UI.WebControls.TextBox TXT_GRPNAME;
		protected System.Web.UI.WebControls.DataGrid DGR_REQUESTGROUP;
		protected System.Web.UI.WebControls.DataGrid DGR_GROUP;
		protected System.Web.UI.WebControls.Label LBL_GROUPID;
		protected Connection conn;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if(!IsPostBack)
			{
				CekCode();
				FillGridCurr();
				FillGridReq();
			}
		}

		private void CekCode()
		{
			conn.QueryString = "SELECT ISNULL(MAX(CONVERT(INT, CODE)),0) AS GROUPID FROM RF_GROUP";
			conn.ExecuteQuery();
			LBL_GROUPID.Text = conn.GetFieldValue("GROUPID").ToString();

			conn.QueryString = "exec PARAM_GENERAL_RFGROUP_GENERATE_CODE '" + LBL_GROUPID.Text + "'";
			conn.ExecuteQuery();

			TXT_GRPCODE.Text = conn.GetFieldValue(0,0);
		}

		private void FillGridCurr()
		{
			conn.QueryString = "select * from rf_group where status='1'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_GROUP.DataSource = dt;
			try
			{
				DGR_GROUP.DataBind();
			}
			catch
			{
				DGR_GROUP.CurrentPageIndex = 0;
				DGR_GROUP.DataBind();
			}
		}

		private void FillGridReq()
		{
			conn.QueryString = "select * from rf_group where status='0'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_REQUESTGROUP.DataSource = dt;
			try
			{
				DGR_REQUESTGROUP.DataBind();
			}
			catch
			{
				DGR_REQUESTGROUP.CurrentPageIndex = 0;
				DGR_REQUESTGROUP.DataBind();
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
			this.BTN_BACK.Click += new System.Web.UI.ImageClickEventHandler(this.BTN_BACK_Click);
			this.BTN_SAVE.Click += new System.EventHandler(this.BTN_SAVE_Click);
			this.BTN_CLEAR.Click += new System.EventHandler(this.BTN_CLEAR_Click);
			this.DGR_GROUP.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_GROUP_ItemCommand);
			this.DGR_GROUP.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_GROUP_PageIndexChanged);
			this.DGR_REQUESTGROUP.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUESTGROUP_ItemCommand);
			this.DGR_REQUESTGROUP.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUESTGROUP_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "exec PARAM_GENERAL_PENDING_RFGROUP_INSERT '" + TXT_GRPCODE.Text + "', '" + TXT_GRPNAME.Text + "', '" + Session["UserID"].ToString() + "'";
			conn.ExecuteQuery();
			
			CekCode();
			ClearData();
			FillGridCurr();
			FillGridReq();
		}

		private void BTN_CLEAR_Click(object sender, System.EventArgs e)
		{
			ClearData();
		}

		private void ClearData()
		{
			TXT_GRPNAME.Text = "";
			CekCode();
		}

		private void DGR_GROUP_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_GROUP.CurrentPageIndex = e.NewPageIndex;
			FillGridCurr();
		}

		private void DGR_GROUP_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_GRPCODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_GRPNAME.Text = e.Item.Cells[1].Text.Trim();
					break;
				case "delete":
					conn.QueryString = "update rf_group set status='2' where code='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					FillGridCurr();
					CekCode();
					break;
			}
		}

		private void DGR_REQUESTGROUP_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUESTGROUP.CurrentPageIndex = e.NewPageIndex;
			FillGridReq();
		}

		private void DGR_REQUESTGROUP_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit_req":
					TXT_GRPCODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_GRPNAME.Text = e.Item.Cells[1].Text.Trim();
					break;
				case "delete_req":
					conn.QueryString = "delete rf_group where code='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteQuery();
					FillGridReq();
					CekCode();
					break;
			}
		}

		private void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../JWSParamMaker.aspx?mc="+Request.QueryString["mc"]+"&pg=9");
		}
	}
}
