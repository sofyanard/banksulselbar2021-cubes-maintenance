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
	/// Summary description for ReviewScoreCust.
	/// </summary>
	public partial class ReviewScoreCust : System.Web.UI.Page
	{
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(ConfigurationSettings.AppSettings["connME"]);

			if(!IsPostBack)
			{
				TR_COMPOSITION.Visible = false;
				TR_COMPOSITIONHIST.Visible = false;
			}

			/*conn.QueryString = "EXEC SCORING_BINDCOMPOST";		//do not auto load anything.. 
			conn.ExecuteQuery();*/
			
			conn.QueryString = "SELECT GRID_COMPOST_DETAIL_HISTORY_PORM, GRID_COMPOST_DETAIL_CURRENT_PORM FROM  APP_PARAMETER";
			conn.ExecuteQuery();

			/*BindData();*/

			DatGrdHistory.PageSize = int.Parse(conn.GetFieldValue(0,0));
			DatGrdComposition.PageSize = int.Parse(conn.GetFieldValue(0,1));
		}

		private void BindData()
		{
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DatGrd.DataSource = dt;				
			DatGrd.DataBind();
		}

		private void BindDataDetail()
		{
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DatGrdComposition.DataSource = dt;				
			DatGrdComposition.DataBind();
		}

		private void BindDataDetailHistory()
		{
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DatGrdHistory.DataSource = dt;				
			DatGrdHistory.DataBind();
		}

		private void BindData1()
		{
			conn.QueryString = "EXEC SCORING_BINDCOMPOST '" + _txtApREGNO.Text.ToString() + "'";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DatGrd.DataSource = dt;

			try
			{
				DatGrd.DataBind();
			}
			catch 
			{
				DatGrd.CurrentPageIndex = DatGrd.PageCount - 1;
				DatGrd.DataBind();
			}

			conn.ClearData();
		}

		private void BindDataComposition1()
		{
			conn.QueryString = "EXEC SCORING_BINDCOMPOSTDETAIL '" +  _txtId.Text.ToString() + "'";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DatGrdComposition.DataSource = dt;

			try
			{
				DatGrdComposition.DataBind();
			}
			catch 
			{
				DatGrdComposition.CurrentPageIndex = DatGrdComposition.PageCount - 1;
				DatGrdComposition.DataBind();
			}

			conn.ClearData();
		}

		private void BindDataDetailHistory1()
		{
			conn.QueryString = "EXEC SCORING_BINDCOMPOSTDETAILHISTORY '" + _txtId.Text.ToString() + "'";
			conn.ExecuteQuery();
	
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DatGrdHistory.DataSource = dt;

			try
			{
				DatGrdHistory.DataBind();
			}
			catch 
			{
				DatGrdHistory.CurrentPageIndex = DatGrdHistory.PageCount - 1;
				DatGrdHistory.DataBind();
			}

			conn.ClearData();
		}

		private void DatGrd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGrd.CurrentPageIndex >= 0 && DatGrd.CurrentPageIndex < DatGrd.PageCount)
			{
				DatGrd.CurrentPageIndex = e.NewPageIndex;
				BindData1();
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
			this.DatGrd.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGrd_ItemCommand);
			this.DatGrd.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGrd_PageIndexChanged);
			this.DatGrdComposition.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGrdComposition_PageIndexChanged);
			this.DatGrdHistory.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGrdHistory_PageIndexChanged);

		}
		#endregion


		protected void _btnFind_Click(object sender, System.EventArgs e)
		{
			TR_COMPOSITION.Visible = false;

			DatGrd.CurrentPageIndex = 0;
			DatGrdComposition.CurrentPageIndex = 0;
			DatGrdHistory.CurrentPageIndex = 0;

			conn.QueryString = "EXEC SCORING_BINDCOMPOST '" + _txtApREGNO.Text.ToString() + "'";

			conn.ExecuteQuery();
			BindData();
		}

		private void DatGrd_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "View":
					TR_COMPOSITION.Visible = true;
					TR_COMPOSITIONHIST.Visible = false;
					_txtId.Text = e.Item.Cells[0].Text.ToString();
					conn.QueryString = "EXEC SCORING_BINDCOMPOSTDETAIL '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					BindDataDetail();
					break;
				case "History":
					TR_COMPOSITIONHIST.Visible = true;
					TR_COMPOSITION.Visible = false;
					_txtId.Text = e.Item.Cells[0].Text.ToString();
					conn.QueryString = "EXEC SCORING_BINDCOMPOSTDETAILHISTORY '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					BindDataDetailHistory();
					break;
			}
		}

		private void DatGrdHistory_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGrdHistory.CurrentPageIndex >= 0 && DatGrdHistory.CurrentPageIndex < DatGrdHistory.PageCount)
			{
				DatGrdHistory.CurrentPageIndex = e.NewPageIndex;
				BindDataDetailHistory1();
			}
		}

		private void DatGrdComposition_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGrdComposition.CurrentPageIndex >= 0 && DatGrdComposition.CurrentPageIndex < DatGrdComposition.PageCount)
			{
				DatGrdComposition.CurrentPageIndex = e.NewPageIndex;
				BindDataComposition1();
			}
		}
	}
}
