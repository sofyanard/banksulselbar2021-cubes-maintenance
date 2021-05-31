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
	/// Summary description for ScoreParamAll.
	/// </summary>
	public partial class ScoreParamAll : System.Web.UI.Page
	{
		protected Tools tool = new Tools();
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if(!IsPostBack)
			{
				CekCode();
				FillDDLVariable();
				FillGridCurr();
				FillGridReq();
			}
		}

		private void CekCode()
		{
			conn.QueryString = "SELECT ISNULL(MAX(CONVERT(INT, CODE)),0) AS SCOREID FROM RF_SCORE";
			conn.ExecuteQuery();
			LBL_CODE.Text = conn.GetFieldValue("SCOREID").ToString();

			conn.QueryString = "EXEC PARAM_GENERAL_RFGROUP_GENERATE_CODE '" + LBL_CODE.Text + "'";
			conn.ExecuteQuery();

			LBL_SCOREID.Text = conn.GetFieldValue(0,0);
		}

		private void FillDDLVariable()
		{
			DDL_VARIABLE.Items.Clear();
			DDL_VARIABLE.Items.Add(new ListItem("--Pilih--", ""));

			conn.QueryString = "EXEC JWS_SCORE_VARIABLE ''";
			conn.ExecuteQuery();
			for(int i=0; i < conn.GetRowCount(); i++)
			{
				DDL_VARIABLE.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			}
		}

		private void FillGridCurr()
		{
			conn.QueryString = "SELECT *, CONVERT(VARCHAR, BOBOT)+'%' AS BOBOT_DESC FROM RF_SCORE WHERE STATUS='1'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_SCORE.DataSource = dt;
			try
			{
				DGR_SCORE.DataBind();
			}
			catch
			{
				DGR_SCORE.CurrentPageIndex = 0;
				DGR_SCORE.DataBind();
			}
		}

		private void FillGridReq()
		{
			conn.QueryString = "SELECT *, CONVERT(VARCHAR, BOBOT)+'%' AS BOBOT_DESC, CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_SCORE WHERE STATUS='0'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_REQUESTSCORE.DataSource = dt;
			try
			{
				DGR_REQUESTSCORE.DataBind();
			}
			catch
			{
				DGR_REQUESTSCORE.CurrentPageIndex = 0;
				DGR_REQUESTSCORE.DataBind();
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
			this.DGR_SCORE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_SCORE_ItemCommand);
			this.DGR_SCORE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_SCORE_PageIndexChanged);
			this.DGR_REQUESTSCORE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUESTSCORE_ItemCommand);
			this.DGR_REQUESTSCORE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUESTSCORE_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if(DDL_VARIABLE.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Check Field Mandatory!");
				return; 
			}
			else
			{
				try
				{
					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RFSCORE_INSERT '" + LBL_SCOREID.Text + "','" + DDL_VARIABLE.SelectedValue + "','"
						+ DDL_VARIABLE.SelectedItem + "','" + TXT_BOBOT.Text + "','" + Session["UserID"].ToString() + "'";
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
			DDL_VARIABLE.SelectedValue = "";
			TXT_BOBOT.Text = "";
			CekCode();
		}

		private void DGR_SCORE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_SCORE.CurrentPageIndex = e.NewPageIndex;
			FillGridCurr();
		}

		private void DGR_SCORE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SCOREID.Text = e.Item.Cells[0].Text.Trim();
					DDL_VARIABLE.SelectedValue = e.Item.Cells[1].Text.Trim();
					TXT_BOBOT.Text = e.Item.Cells[3].Text.Trim();
					break;
				case "delete":
					conn.QueryString = "UPDATE RF_SCORE SET STATUS='2' WHERE CODE='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					conn.QueryString = "UPDATE RFSCORE_HISTORY SET STATUS='0' WHERE CODE='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					ClearData();
					FillGridCurr();
					break;
			}
		}

		private void DGR_REQUESTSCORE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUESTSCORE.CurrentPageIndex = e.NewPageIndex;
			FillGridReq();
		}

		private void DGR_REQUESTSCORE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit_req":
					LBL_SCOREID.Text = e.Item.Cells[0].Text.Trim();
					DDL_VARIABLE.SelectedValue = e.Item.Cells[1].Text.Trim();
					TXT_BOBOT.Text = e.Item.Cells[3].Text.Trim();
					break;
				case "delete_req":
					conn.QueryString = "DELETE RF_SCORE WHERE CODE='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					conn.QueryString = "DELETE RFSCORE_HISTORY WHERE CODE='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					
					ClearData();
					FillGridReq();
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../../JiwaServiceParam.aspx?mc=" + Request.QueryString["mc"] + "&pg=9");
		}
	}
}
