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
	/// Summary description for LinkParamAll.
	/// </summary>
	public class LinkParamAll : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton BTN_BACK;
		protected System.Web.UI.WebControls.DropDownList DDL_BCHTYPEID;
		protected System.Web.UI.WebControls.DropDownList DDL_GRPTYPEID;
		protected System.Web.UI.WebControls.DropDownList DDL_DEPTTYPEID;
		protected System.Web.UI.WebControls.Button BTN_SAVE;
		protected System.Web.UI.WebControls.Button BTN_CLEAR;
		protected System.Web.UI.WebControls.DataGrid DGR_LINK;
		protected System.Web.UI.WebControls.DataGrid DGR_REQUESTLINK;
		protected System.Web.UI.HtmlControls.HtmlTableRow TR_GROUP;
		protected Tools tool = new Tools();
		protected System.Web.UI.WebControls.Label LBL_NO;
		protected System.Web.UI.WebControls.TextBox TXT_NO;
		protected Connection conn;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if (!IsPostBack) 
			{
				CekCode();
				FillDDLType();
				DDL_DEPTTYPEID.Items.Add(new ListItem("", ""));
				FillGridCurr();
				FillGridReq();
			}
		}

		private void CekCode()
		{
			conn.QueryString = "SELECT ISNULL(MAX(CONVERT(INT, SEQ)),0) AS SEQ FROM RF_LINK";
			conn.ExecuteQuery();
			LBL_NO.Text = conn.GetFieldValue("SEQ").ToString();

			conn.QueryString="EXEC PARAM_GENERAL_RFGROUP_GENERATE_CODE '" + LBL_NO.Text + "'";
			conn.ExecuteQuery();

			TXT_NO.Text = conn.GetFieldValue(0,0);
		}

		private void FillDDLType()
		{
			DDL_BCHTYPEID.Items.Clear();
			DDL_GRPTYPEID.Items.Clear();
			DDL_BCHTYPEID.Items.Add(new ListItem("--Pilih--", ""));
			DDL_GRPTYPEID.Items.Add(new ListItem("--Pilih--", ""));

			conn.QueryString = "SELECT BRANCH_CODE, BRANCH_NAME, BRANCH_CODE + '-' + BRANCH_NAME AS [GROUP] FROM RFBRANCH WHERE ACTIVE='1'";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_BCHTYPEID.Items.Add(new ListItem(conn.GetFieldValue(i, "GROUP"), conn.GetFieldValue(i, "BRANCH_CODE")));
			}

			conn.QueryString = "SELECT BRANCH_CODE, BRANCH_NAME FROM RFBRANCH WHERE ACTIVE='1'";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_GRPTYPEID.Items.Add(new ListItem(conn.GetFieldValue(i, "BRANCH_NAME"), conn.GetFieldValue(i, "BRANCH_CODE")));
			}
		}

		private void FillDDLDeptType()
		{
			DDL_DEPTTYPEID.Items.Clear();

			conn.QueryString = "SELECT D_CODE, D_DESCNEW FROM RF_DEPT WHERE G_CODE='" + DDL_GRPTYPEID.SelectedValue + "' AND STATUS='1'";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_DEPTTYPEID.Items.Add(new ListItem(conn.GetFieldValue(i, "D_DESCNEW"), conn.GetFieldValue(i, "D_CODE")));
			}
		}

		private void FillGridCurr()
		{
			conn.QueryString = "SELECT CODE + '-' + DESCRIPTION AS BRANCH, * FROM RF_LINK WHERE STATUS='1'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_LINK.DataSource = dt;
			try
			{
				DGR_LINK.DataBind();
			}
			catch
			{
				DGR_LINK.CurrentPageIndex = 0;
				DGR_LINK.DataBind();
			}
		}

		private void FillGridReq()
		{
			conn.QueryString = "SELECT CODE + '-' + DESCRIPTION AS BRANCH, * , CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_LINK WHERE STATUS='0'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_REQUESTLINK.DataSource = dt;
			try
			{
				DGR_REQUESTLINK.DataBind();
			}
			catch
			{
				DGR_REQUESTLINK.CurrentPageIndex = 0;
				DGR_REQUESTLINK.DataBind();
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
			this.DDL_GRPTYPEID.SelectedIndexChanged += new System.EventHandler(this.DDL_GRPTYPEID_SelectedIndexChanged);
			this.BTN_SAVE.Click += new System.EventHandler(this.BTN_SAVE_Click);
			this.BTN_CLEAR.Click += new System.EventHandler(this.BTN_CLEAR_Click);
			this.DGR_LINK.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_LINK_ItemCommand);
			this.DGR_LINK.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_LINK_PageIndexChanged);
			this.DGR_REQUESTLINK.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUESTLINK_ItemCommand);
			this.DGR_REQUESTLINK.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUESTLINK_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DDL_GRPTYPEID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillDDLDeptType();
		}

		private void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if(DDL_BCHTYPEID.SelectedValue.ToString() == "" || DDL_GRPTYPEID.SelectedValue.ToString() == "")
			{
				GlobalTools.popMessage(this, "Check Field Mandatory!");
				return; 
			}
			else
			{
				try
				{
					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RFLINK_INSERT '" + TXT_NO.Text + "','" + DDL_BCHTYPEID.SelectedValue + "','"
						+ DDL_GRPTYPEID.SelectedValue + "','" + DDL_DEPTTYPEID.SelectedValue + "','" + Session["UserID"].ToString() + "'";
					conn.ExecuteQuery();
				}
				catch (Exception ex)
				{
					ClearData();
					CekCode();
					FillGridCurr();
					FillGridReq();

					string errmsg = ex.Message.Replace("'","");
					if (errmsg.IndexOf("Last Query:") > 0)
						errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
					GlobalTools.popMessage(this, errmsg);
					return;
				}
				ClearData();
				CekCode();
				FillGridCurr();
				FillGridReq();
			}
		}

		private void BTN_CLEAR_Click(object sender, System.EventArgs e)
		{
			ClearData();
		}

		private void ClearData()
		{
			FillDDLType();
			DDL_DEPTTYPEID.Items.Clear();
			DDL_DEPTTYPEID.Items.Add(new ListItem("", ""));
		}

		private void DGR_LINK_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_LINK.CurrentPageIndex = e.NewPageIndex;
			FillGridCurr();
		}

		private void DGR_LINK_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_NO.Text = e.Item.Cells[0].Text.Trim();
					DDL_BCHTYPEID.SelectedValue = e.Item.Cells[1].Text.Trim();
					DDL_GRPTYPEID.SelectedValue = e.Item.Cells[3].Text.Trim();
					FillDDLDeptType();
					DDL_DEPTTYPEID.SelectedValue = e.Item.Cells[5].Text.Trim();
					break;
				case "delete":
					conn.QueryString = "UPDATE RF_LINK SET STATUS='2' WHERE SEQ='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					conn.QueryString = "UPDATE RFLINK_HISTORY SET STATUS='0' WHERE SEQ='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					
					CekCode();
					FillGridCurr();
					break;
			}
		}

		private void DGR_REQUESTLINK_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUESTLINK.CurrentPageIndex = e.NewPageIndex;
			FillGridReq();
		}

		private void DGR_REQUESTLINK_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit_req":
					TXT_NO.Text = e.Item.Cells[0].Text.Trim();
					DDL_BCHTYPEID.SelectedValue = e.Item.Cells[1].Text.Trim();
					DDL_GRPTYPEID.SelectedValue = e.Item.Cells[3].Text.Trim();
					FillDDLDeptType();
					DDL_DEPTTYPEID.SelectedValue = e.Item.Cells[5].Text.Trim();
					break;
				case "delete_req":
					conn.QueryString = "DELETE RF_LINK WHERE SEQ='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					conn.QueryString = "UPDATE RFLINK_HISTORY SET STATUS='0' WHERE SEQ='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					
					CekCode();
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
