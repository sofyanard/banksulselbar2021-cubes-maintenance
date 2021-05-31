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
	/// Summary description for CustomerParamAll.
	/// </summary>
	public class CustomerParamAll : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton BTN_BACK;
		protected System.Web.UI.WebControls.DropDownList DDL_GRPTYPEID;
		protected System.Web.UI.WebControls.DropDownList DDL_DEPTTYPEID;
		protected System.Web.UI.WebControls.Button BTN_SAVE;
		protected System.Web.UI.WebControls.Button BTN_CLEAR;
		protected System.Web.UI.WebControls.TextBox TEXT_QUESTION;
		protected System.Web.UI.WebControls.DataGrid DGR_QUEST;
		protected System.Web.UI.HtmlControls.HtmlTableRow TR_GROUP;
		protected System.Web.UI.WebControls.DataGrid DGR_REQUESTQUEST;
		protected Tools tool = new Tools();
		protected System.Web.UI.WebControls.Label LBL_NO;
		protected System.Web.UI.WebControls.TextBox TXT_NO;
		protected Connection conn;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if(!IsPostBack)
			{
				CekCode();
				DDL_DEPTTYPEID.Items.Add(new ListItem("--Select--", ""));
				FillDDLGroupType();
				FillGridCurr();
				FillGridReq();
			}
		}

		private void CekCode()
		{
			conn.QueryString = "SELECT ISNULL(MAX(CONVERT(INT, [NO])),0) AS NO_ID FROM RF_CUSTOMER";
			conn.ExecuteQuery();
			LBL_NO.Text = conn.GetFieldValue("NO_ID").ToString();

			conn.QueryString="EXEC PARAM_GENERAL_RFGROUP_GENERATE_CODE '" + LBL_NO.Text + "'";
			conn.ExecuteQuery();

			TXT_NO.Text = conn.GetFieldValue(0,0);
		}

		private void FillDDLGroupType()
		{
			DDL_GRPTYPEID.Items.Clear();
			DDL_GRPTYPEID.Items.Add(new ListItem("--Select--", ""));

			conn.QueryString = "SELECT BRANCH_CODE, BRANCH_NAME FROM RFBRANCH WHERE ACTIVE='1'";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_GRPTYPEID.Items.Add(new ListItem(conn.GetFieldValue(i, "BRANCH_NAME"), conn.GetFieldValue(i, "BRANCH_CODE")));
			}
		}

		private void DDL_GRPTYPEID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillDDLDeptType();
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
			conn.QueryString = "SELECT * FROM RF_CUSTOMER WHERE STATUS='1'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_QUEST.DataSource = dt;
			try
			{
				DGR_QUEST.DataBind();
			}
			catch
			{
				DGR_QUEST.CurrentPageIndex = 0;
				DGR_QUEST.DataBind();
			}
		}

		private void FillGridReq()
		{
			conn.QueryString = "SELECT *, CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_CUSTOMER WHERE STATUS='0'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_REQUESTQUEST.DataSource = dt;
			try
			{
				DGR_REQUESTQUEST.DataBind();
			}
			catch
			{
				DGR_REQUESTQUEST.CurrentPageIndex = 0;
				DGR_REQUESTQUEST.DataBind();
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
			this.DGR_QUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_QUEST_ItemCommand);
			this.DGR_QUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_QUEST_PageIndexChanged);
			this.DGR_REQUESTQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUESTQUEST_ItemCommand);
			this.DGR_REQUESTQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUESTQUEST_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if(DDL_GRPTYPEID.SelectedValue.ToString() == "" || TEXT_QUESTION.Text == "")
			{
				GlobalTools.popMessage(this, "Check Field Mandatory!");
				return; 
			}
			else
			{
				try
				{
					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RFQUEST_INSERT '" + TXT_NO.Text + "','" + DDL_GRPTYPEID.SelectedValue + "','"
						+ DDL_GRPTYPEID.SelectedItem + "','" + DDL_DEPTTYPEID.SelectedValue + "','" + DDL_DEPTTYPEID.SelectedItem + "','"
						+ TEXT_QUESTION.Text + "','" + Session["UserID"].ToString() + "'";
					conn.ExecuteQuery();
				}
				catch (Exception ex)
				{
					CekCode();
					ClearData();
					FillGridCurr();
					FillGridReq();

					string errmsg = ex.Message.Replace("'","");
					if (errmsg.IndexOf("Last Query:") > 0)
						errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
					GlobalTools.popMessage(this, errmsg);
					return;
				}
				CekCode();
				ClearData();
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
			DDL_DEPTTYPEID.Items.Clear();
			DDL_DEPTTYPEID.Items.Add(new ListItem("--Select--", ""));
			FillDDLGroupType();
			TEXT_QUESTION.Text = "";
		}

		private void DGR_QUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_QUEST.CurrentPageIndex = e.NewPageIndex;
			FillGridCurr();
		}

		private void DGR_QUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_NO.Text = e.Item.Cells[0].Text.Trim();
					FillDDLGroupType();
					DDL_GRPTYPEID.SelectedValue = e.Item.Cells[1].Text.Trim();
					FillDDLDeptType();
					DDL_DEPTTYPEID.SelectedValue = e.Item.Cells[3].Text.Trim();
					TEXT_QUESTION.Text = e.Item.Cells[5].Text.Trim();
					break;
				case "delete":
					conn.QueryString = "UPDATE RF_CUSTOMER SET STATUS='2' WHERE [NO]='" + e.Item.Cells[0].Text.Trim() + "' AND G_CODE='"
						+ e.Item.Cells[1].Text.Trim() + "' AND D_CODE='" + e.Item.Cells[3].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					conn.QueryString = "UPDATE RFCUSTOMER_HISTORY SET STATUS='0' WHERE [NO]='" + e.Item.Cells[0].Text.Trim() + "' AND G_CODE='"
						+ e.Item.Cells[1].Text.Trim() + "' AND D_CODE='" + e.Item.Cells[3].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					
					CekCode();
					FillGridCurr();
					break;
			}
		}

		private void DGR_REQUESTQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUESTQUEST.CurrentPageIndex = e.NewPageIndex;
			FillGridReq();
		}

		private void DGR_REQUESTQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit_req":
					TXT_NO.Text = e.Item.Cells[0].Text.Trim();
					FillDDLGroupType();
					DDL_GRPTYPEID.SelectedValue = e.Item.Cells[1].Text.Trim();
					FillDDLDeptType();
					DDL_DEPTTYPEID.SelectedValue = e.Item.Cells[3].Text.Trim();
					TEXT_QUESTION.Text = e.Item.Cells[5].Text.Trim();
					break;
				case "delete_req":
					conn.QueryString = "DELETE RF_CUSTOMER WHERE [NO]='" + e.Item.Cells[0].Text.Trim() + "' AND G_CODE='"
						+ e.Item.Cells[1].Text.Trim() + "' AND D_CODE='" + e.Item.Cells[3].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					conn.QueryString = "UPDATE RFCUSTOMER_HISTORY SET STATUS='0' WHERE [NO]='" + e.Item.Cells[0].Text.Trim() + "' AND G_CODE='"
						+ e.Item.Cells[1].Text.Trim() + "' AND D_CODE='" + e.Item.Cells[3].Text.Trim() + "'";
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
