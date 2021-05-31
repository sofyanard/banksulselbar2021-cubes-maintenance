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
using System.Configuration;

namespace CuBES_Maintenance.Parameter.Scoring.Small
{
	/// <summary>
	/// Summary description for ScoringHistory.
	/// </summary>
	public partial class ScoringHistory : System.Web.UI.Page
	{
		protected Connection conn;

		private string regno = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(ConfigurationSettings.AppSettings["connME"]);

			if(!IsPostBack)
			{
				TR_RULE.Visible = false;
				TR_COMPOSITION.Visible = false;
			}
			
			conn.QueryString = "SELECT GRID_COMPOST_DETAIL_RULE_PORM, GRID_COMPOST_DETAIL_CURRENT_PORM FROM  APP_PARAMETER";
			conn.ExecuteQuery();

			/*BindData();*/

			DatGrd.PageSize = int.Parse(conn.GetFieldValue(0,0));
			DatGrdComposition.PageSize = int.Parse(conn.GetFieldValue(0,1));

			// Put user code to initialize the page here
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
			this.DatGrd.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGrd_ItemCommand);
			this.DatGrd.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGrd_PageIndexChanged);
			this.DatGrdRule.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGrdRule_PageIndexChanged);
			this.DatGrdComposition.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGrdComposition_PageIndexChanged);

		}
		#endregion

		private void BindResumeData()
		{
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DatGrd.DataSource = dt;				
			DatGrd.DataBind();
		}

		private void BindResumeDataComposition()
		{
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DatGrdComposition.DataSource = dt;				
			DatGrdComposition.DataBind();
		}

		private void BindResumeDataRule()
		{
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DatGrdRule.DataSource = dt;				
			DatGrdRule.DataBind();
		}

		private void DatGrd_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Composition":
					TR_COMPOSITION.Visible = true;
					conn.QueryString = "EXEC SCORING_BINDCOMPOSTDETAILHISTORY '" + e.Item.Cells[0].Text.ToString() + "','" + e.Item.Cells[2].Text.ToString() + "'";
					conn.ExecuteQuery();
					_txtSeqComposition.Text =  e.Item.Cells[2].Text.ToString();
					BindResumeDataComposition();
					break;
				case "Rule":
					TR_RULE.Visible = true;
					conn.QueryString = "EXEC SCORING_BINDRULEHISTORY '" + _txtId.Text.ToString() + "','" + e.Item.Cells[2].Text.ToString() + "'";
					conn.ExecuteQuery();
					_txtSeqRuleReason.Text = e.Item.Cells[2].Text.ToString();
					BindResumeDataRule();
					break;
			}
		}

		private void DatGrd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGrd.CurrentPageIndex >= 0 && DatGrd.CurrentPageIndex < DatGrd.PageCount)
			{
				DatGrd.CurrentPageIndex = e.NewPageIndex;
				conn.QueryString = "EXEC SCORING_BINDHISTORY '" + _txtApRegnoHidden.Text.ToString() + "',''";
				conn.ExecuteQuery();
				BindResumeData();
			}
		}

		protected void _btnFind_Click(object sender, System.EventArgs e)
		{
			TR_RULE.Visible = false;
			TR_COMPOSITION.Visible = false;
			regno = _txtApREGNO.Text.ToString();
			_txtId.Text = _txtApREGNO.Text.ToString();
			_txtApRegnoHidden.Text = _txtApREGNO.Text.ToString();
			conn.QueryString = "EXEC SCORING_BINDHISTORY '" + _txtApREGNO.Text.ToString() + "',''";

			DatGrd.CurrentPageIndex = 0;
			DatGrdRule.CurrentPageIndex = 0;
			DatGrdComposition.CurrentPageIndex = 0;

			conn.ExecuteQuery();
			BindResumeData();
		}

		private void DatGrdRule_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGrdRule.CurrentPageIndex >= 0 && DatGrdRule.CurrentPageIndex < DatGrdRule.PageCount)
			{
				DatGrdRule.CurrentPageIndex = e.NewPageIndex;
				conn.QueryString = "EXEC SCORING_BINDRULEHISTORY '" + _txtId.Text.ToString() + "','" + _txtSeqRuleReason.Text + "'";
				conn.ExecuteQuery();
				BindResumeDataRule();
			}
		}

		private void DatGrdComposition_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGrdComposition.CurrentPageIndex >= 0 && DatGrdComposition.CurrentPageIndex < DatGrdComposition.PageCount)
			{
				DatGrdComposition.CurrentPageIndex = e.NewPageIndex;
				conn.QueryString = "EXEC SCORING_BINDCOMPOSTDETAILHISTORY '" + _txtId.Text.ToString() + "','" + _txtSeqComposition.Text + "'";
				conn.ExecuteQuery();
				BindResumeDataComposition();
			}
		}
	}
}
