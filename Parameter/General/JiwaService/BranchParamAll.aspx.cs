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

namespace CuBES_Maintenance.Parameter.General.JiwaService
{
	/// <summary>
	/// Summary description for BranchParamAll.
	/// </summary>
	public class BranchParamAll : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton BTN_BACK;
		protected System.Web.UI.WebControls.DropDownList DDL_BCHTYPEID;
		protected System.Web.UI.WebControls.DropDownList DDL_GRPTYPEID;
		protected System.Web.UI.WebControls.Button BTN_SAVE;
		protected System.Web.UI.WebControls.Button BTN_CLEAR;
		protected System.Web.UI.HtmlControls.HtmlTableRow TR_GROUP;
		protected System.Web.UI.WebControls.DataGrid DGR_BRANCH;
		protected System.Web.UI.WebControls.DataGrid DGR_REQUESTBRANCH;
		protected Tools tool = new Tools();
		protected Connection conn;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if (!IsPostBack) 
			{
				FillDDLType();
				FillGridCurr();
				FillGridReq();
			}
		}

		private void FillDDLType()
		{
			DDL_BCHTYPEID.Items.Clear();
			DDL_GRPTYPEID.Items.Clear();
			DDL_BCHTYPEID.Items.Add(new ListItem("--Pilih--", ""));
			DDL_GRPTYPEID.Items.Add(new ListItem("--Pilih--", ""));

			conn.QueryString = "select BRANCH_CODE, branch_code + '-' + branch_name as BRANCH from rfbranch where active='1'";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_BCHTYPEID.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
			}

			conn.QueryString = "select CODE, code + '-' + desc_cur as [GROUP] from rf_group where status='1'";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_GRPTYPEID.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
			}


		}

		private void FillGridCurr()
		{
			conn.QueryString = "select *, B_CODE+'-'+B_DESC as BRANCH, G_CODE+'-'+G_DESC as [GROUP] from VW_JWS_BRANCH where status='1'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_BRANCH.DataSource = dt;
			try
			{
				DGR_BRANCH.DataBind();
			}
			catch
			{
				DGR_BRANCH.CurrentPageIndex = 0;
				DGR_BRANCH.DataBind();
			}
		}

		private void FillGridReq()
		{
			conn.QueryString = "select *, B_CODE+'-'+B_DESC as BRANCH, G_CODE+'-'+G_DESC as [GROUP] from VW_JWS_BRANCH where status='0'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_REQUESTBRANCH.DataSource = dt;
			try
			{
				DGR_REQUESTBRANCH.DataBind();
			}
			catch
			{
				DGR_REQUESTBRANCH.CurrentPageIndex = 0;
				DGR_REQUESTBRANCH.DataBind();
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
			this.DGR_BRANCH.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_BRANCH_ItemCommand);
			this.DGR_BRANCH.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_BRANCH_PageIndexChanged);
			this.DGR_REQUESTBRANCH.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUESTBRANCH_ItemCommand);
			this.DGR_REQUESTBRANCH.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUESTBRANCH_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "exec PARAM_GENERAL_PENDING_RFBRANCH_INSERT '" + DDL_BCHTYPEID.SelectedValue + "','" + DDL_GRPTYPEID.SelectedValue + "','" + Session["UserID"].ToString() + "'";
			conn.ExecuteQuery();

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
			FillDDLType();
		}
		
		private void DGR_BRANCH_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_BRANCH.CurrentPageIndex = e.NewPageIndex;
			FillGridCurr();
		}

		private void DGR_BRANCH_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				/*case "edit":
					DDL_BCHTYPEID.SelectedValue = e.Item.Cells[0].Text.Trim();
					DDL_GRPTYPEID.SelectedValue = e.Item.Cells[2].Text.Trim();
					break;*/
				case "delete":
					conn.QueryString = "update rf_branch set status='2' where b_code='" + e.Item.Cells[0].Text.Trim() + "' and g_code='" + e.Item.Cells[2].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					FillGridCurr();
					break;
			}
		}

		private void DGR_REQUESTBRANCH_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUESTBRANCH.CurrentPageIndex = e.NewPageIndex;
			FillGridReq();
		}

		private void DGR_REQUESTBRANCH_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				/*case "edit_req":
					DDL_BCHTYPEID.SelectedValue = e.Item.Cells[0].Text.Trim();
					DDL_GRPTYPEID.SelectedValue = e.Item.Cells[2].Text.Trim();
					break;*/
				case "delete_req":
					conn.QueryString = "delete rf_branch where b_code='" + e.Item.Cells[0].Text.Trim() + "' and g_code='" + e.Item.Cells[2].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					FillGridReq();
					break;
			}
		}

		private void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../JWSParamMaker.aspx?mc="+Request.QueryString["mc"]+"&pg=9");
		}
	}
}
