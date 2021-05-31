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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for ProblemMaker.
	/// </summary>
	public partial class ProblemMaker : System.Web.UI.Page
	{
		protected Connection conn, con1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if (!IsPostBack)
			{
				FillGridCurr();
				FillGridReq();
				CekCode();
			}
		}

		private void FillGridCurr()
		{
			conn.QueryString = "SELECT * FROM RFPROBLEM";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			Dgr_CurrStage.DataSource = dt;
			try
			{
				Dgr_CurrStage.DataBind();
			}
			catch
			{
				Dgr_CurrStage.CurrentPageIndex = 0;
				Dgr_CurrStage.DataBind();
			}

			LinkButton lnk;

			for (int i = 0; i < Dgr_CurrStage.Items.Count; i++)
			{
				if (Dgr_CurrStage.Items[i].Cells[2].Text == "0")	//active		-- user deleted
				{
					lnk = (LinkButton)Dgr_CurrStage.Items[i].Cells[3].FindControl("LNK_DELETE");
					lnk.Visible = false;
					lnk = (LinkButton)Dgr_CurrStage.Items[i].Cells[3].FindControl("LNK_EDIT");
					lnk.Visible = false;
					Dgr_CurrStage.Items[i].Cells[0].ForeColor = Color.Gray;		//userid
					Dgr_CurrStage.Items[i].Cells[1].ForeColor = Color.Gray;		//fullname
				}
				else
				{
					lnk = (LinkButton)Dgr_CurrStage.Items[i].Cells[3].FindControl("LNK_UNDELETE");
					lnk.Visible = false;
				}
			}
		}

		private void FillGridReq()
		{
			conn.QueryString = "SELECT * FROM VW_PENDING_RFPROBLEM";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			Dgr_RequestStage.DataSource = dt;
			try
			{
				Dgr_RequestStage.DataBind();
			}
			catch
			{
				Dgr_RequestStage.CurrentPageIndex = 0;
				Dgr_RequestStage.DataBind();
			}
		}

		private void CekCode()
		{
			conn.QueryString = "PARAM_GENERAL_RFPROBLEM_GENERATE_CODE";
			conn.ExecuteQuery();

			TXT_CODE.Text = conn.GetFieldValue(0,0);
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
			this.Dgr_CurrStage.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Dgr_CurrStage_ItemCommand);
			this.Dgr_CurrStage.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Dgr_CurrStage_PageIndexChanged);
			this.Dgr_RequestStage.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Dgr_RequestStage_ItemCommand);
			this.Dgr_RequestStage.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Dgr_RequestStage_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "PARAM_GENERAL_PENDING_RFPROBLEM_INSERT '" +
				TXT_CODE.Text + "', '" + TXT_PROBNAME.Text + "', '" + LBL_SAVEMODE.Text + "'";
			conn.ExecuteNonQuery();
			
			CekCode();
			ClearData();

			LBL_SAVEMODE.Text = "1";
			FillGridReq();
			FillGridCurr();
		}

		protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
		{
			ClearData();
		}

		private void ClearData()
		{
			TXT_PROBNAME.Text = "";
			CekCode();
		}

		private void Dgr_CurrStage_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_PROBNAME.Text = e.Item.Cells[1].Text.Trim();
					LBL_SAVEMODE.Text = "0";
					break;
				case "delete":
					LBL_SAVEMODE.Text = "2";

					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RFPROBLEM_INSERT '" +
						e.Item.Cells[0].Text.Trim() + "', '" + 
						e.Item.Cells[1].Text.Trim() + "', '" + 
						LBL_SAVEMODE.Text + "'";
					conn.ExecuteNonQuery();

					FillGridCurr();
					FillGridReq();
					CekCode();
					LBL_SAVEMODE.Text = "1";
					break;
				case "undelete":
					LBL_SAVEMODE.Text = "3";

					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RFPROBLEM_INSERT '" +
						e.Item.Cells[0].Text.Trim() + "', '" + 
						e.Item.Cells[1].Text.Trim() + "', '" + 
						LBL_SAVEMODE.Text + "'";
					conn.ExecuteNonQuery();

					FillGridCurr();
					FillGridReq();
					CekCode();
					LBL_SAVEMODE.Text = "1";
					break;
			}
		}

		private void Dgr_CurrStage_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			Dgr_CurrStage.CurrentPageIndex = e.NewPageIndex;
			FillGridCurr();
		}

		private void Dgr_RequestStage_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			Dgr_RequestStage.CurrentPageIndex = e.NewPageIndex;
			FillGridReq();
		}

		private void Dgr_RequestStage_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit_req":
					TXT_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_PROBNAME.Text = e.Item.Cells[1].Text.Trim();
					LBL_SAVEMODE.Text = e.Item.Cells[2].Text.Trim();
					break;
				case "delete_req":
					conn.QueryString = "delete pending_RFPROBLEM where prob_cd='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteQuery();
					FillGridCurr();
					FillGridReq();
					CekCode();
					break;
			}
		}
	}
}
