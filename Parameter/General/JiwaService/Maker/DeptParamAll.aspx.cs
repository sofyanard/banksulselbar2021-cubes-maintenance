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
	/// Summary description for DeptParamAll.
	/// </summary>
	public partial class DeptParamAll : System.Web.UI.Page
	{
		protected Tools tool = new Tools();
		protected Connection conn;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if (!IsPostBack) 
			{
				CekCode();
				FillDDLGroupType();
				FillGridCurr();
				FillGridReq();
			}
		}
		
		private void CekCode()
		{
			conn.QueryString = "SELECT ISNULL(MAX(CONVERT(INT, D_CODE)),0) AS DEPTID FROM RF_DEPT";
			conn.ExecuteQuery();
			LBL_DEPTID.Text = conn.GetFieldValue("DEPTID").ToString();

			conn.QueryString="EXEC PARAM_GENERAL_RFGROUP_GENERATE_CODE '" + LBL_DEPTID.Text + "'";
			conn.ExecuteQuery();

			TXT_DEPTCODE.Text = conn.GetFieldValue(0,0);
		}

		private void FillDDLGroupType()
		{
			DDL_GRPTYPEID.Items.Clear();

			conn.QueryString = "SELECT BRANCH_CODE, BRANCH_NAME, BRANCH_CODE + '-' + BRANCH_NAME AS [GROUP] FROM RFBRANCH WHERE ACTIVE='1'";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_GRPTYPEID.Items.Add(new ListItem(conn.GetFieldValue(i, "GROUP"), conn.GetFieldValue(i, "BRANCH_CODE")));
			}
		}

		private void FillGridCurr()
		{
			conn.QueryString = "SELECT G_CODE + '-' + G_DESC AS [GROUP], * FROM RF_DEPT WHERE STATUS='1'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_DEPT.DataSource = dt;
			try
			{
				DGR_DEPT.DataBind();
			}
			catch
			{
				DGR_DEPT.CurrentPageIndex = 0;
				DGR_DEPT.DataBind();
			}
		}

		private void FillGridReq()
		{
			conn.QueryString = "SELECT G_CODE + '-' + G_DESC AS [GROUP], *, CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_DEPT WHERE STATUS='0'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_REQUESTDEPT.DataSource = dt;
			try
			{
				DGR_REQUESTDEPT.DataBind();
			}
			catch
			{
				DGR_REQUESTDEPT.CurrentPageIndex = 0;
				DGR_REQUESTDEPT.DataBind();
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
			this.DGR_DEPT.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_DEPT_ItemCommand);
			this.DGR_DEPT.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_DEPT_PageIndexChanged);
			this.DGR_REQUESTDEPT.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUESTDEPT_ItemCommand);
			this.DGR_REQUESTDEPT.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUESTDEPT_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if(DDL_GRPTYPEID.SelectedValue.ToString() == "" || TXT_DEPTNAME.Text == "")
			{
				GlobalTools.popMessage(this, "Check Field Mandatory!");
				return; 
			}
			else
			{
				try
				{
					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RFDEPT_INSERT '" +
						DDL_GRPTYPEID.SelectedValue + "','" + TXT_DEPTCODE.Text + "','" + 
						TXT_DEPTNAME.Text + "','" + Session["UserID"].ToString() + "'";
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

		protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
		{
			ClearData();
		}

		private void ClearData()
		{
			TXT_DEPTNAME.Text = "";
			CekCode();
		}
		
		private void DGR_DEPT_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_DEPT.CurrentPageIndex = e.NewPageIndex;
			FillGridCurr();
		}

		private void DGR_DEPT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					DDL_GRPTYPEID.SelectedValue = e.Item.Cells[0].Text.Trim();
					TXT_DEPTCODE.Text = e.Item.Cells[2].Text.Trim();
					TXT_DEPTNAME.Text = e.Item.Cells[3].Text.Trim();
					break;
				case "delete":
					conn.QueryString = "EXEC JWS_DELETE_DEPT '" + e.Item.Cells[0].Text.Trim() + "','" + e.Item.Cells[2].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					FillGridCurr();
					CekCode();
					break;
			}
		}		

		private void DGR_REQUESTDEPT_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUESTDEPT.CurrentPageIndex = e.NewPageIndex;
			FillGridReq();
		}

		private void DGR_REQUESTDEPT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit_req":
					DDL_GRPTYPEID.SelectedValue = e.Item.Cells[0].Text.Trim();
					TXT_DEPTCODE.Text = e.Item.Cells[2].Text.Trim();
					TXT_DEPTNAME.Text = e.Item.Cells[3].Text.Trim();
					break;
				case "delete_req":
					conn.QueryString = "DELETE RF_DEPT WHERE D_CODE='" + e.Item.Cells[2].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					conn.QueryString = "DELETE RFDEPT_HISTORY WHERE D_CODE='" + e.Item.Cells[2].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					FillGridReq();
					CekCode();
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../../JiwaServiceParam.aspx?mc=" + Request.QueryString["mc"] + "&pg=9");
		}
	}
}
