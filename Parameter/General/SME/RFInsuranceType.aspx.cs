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
	/// Summary description for RFInsuranceType.
	/// </summary>
	public partial class RFInsuranceType : System.Web.UI.Page
	{
		protected Connection conn;
		string IT_ID;
		int jml_row;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

		//	if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
		//		Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack)
			{
				conn.QueryString = "select GRIT_ID, GRIT_DESC from RFGROUPINSURANCETYPE ";
				conn.ExecuteQuery();
				jml_row = conn.GetRowCount();
				for (int i=0; i<jml_row; i++)
					DDL_GRIT_ID.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
				ViewCurrent();
				ViewMaker();
			}
			DGR_CURRENT.PageIndexChanged += new DataGridPageChangedEventHandler(this.CH_DGR_CURRENT);
			DGR_MAKER.PageIndexChanged += new DataGridPageChangedEventHandler(this.CH_DGR_MAKER);
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
			this.DGR_CURRENT.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_CURRENT_ItemCommand);
			this.DGR_MAKER.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_MAKER_ItemCommand);

		}
		#endregion

		private void ViewCurrent()
		{ 
			conn.QueryString = "select * from VW_PARAM_GENERAL_RFINSURANCETYPE ";
			conn.ExecuteQuery();
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_CURRENT.DataSource = data;
			DGR_CURRENT.DataBind();
		}

		private void ViewMaker()
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_RFINSURANCETYPE_MAKER ";
			conn.ExecuteQuery();
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_MAKER.DataSource = data;
			DGR_MAKER.DataBind();
		}
		
		private void CH_DGR_CURRENT(Object sender, DataGridPageChangedEventArgs e) 
		{
			DGR_CURRENT.CurrentPageIndex = e.NewPageIndex;
			ViewCurrent();
		}

		private void CH_DGR_MAKER(Object sender, DataGridPageChangedEventArgs e) 
		{
			DGR_MAKER.CurrentPageIndex = e.NewPageIndex;
			ViewMaker();
		}

		private void clearEditBoxes()
		{
			TXT_IT_ID.Text = "";
			TXT_IT_DESC.Text = "";
			try
			{ DDL_GRIT_ID.SelectedIndex = 0; }
			catch{}
		}

		private void activatePostBackControls(bool mode)
		{
			TXT_IT_ID.Enabled = mode;
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "exec PARAM_GENERAL_RFINSURANCETYPE_MAKER '"+ LBL_SAVEMODE.Text.Trim() + "', '" +
				TXT_IT_ID.Text.Trim() + "', '" + TXT_IT_DESC.Text.Trim() + "', '" + 
				DDL_GRIT_ID.SelectedValue.Trim() + "' ";
			conn.ExecuteQuery();
			ViewMaker();
			clearEditBoxes();
			activatePostBackControls(true);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			activatePostBackControls(true);
			clearEditBoxes();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../HostParam.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void DGR_CURRENT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//int i;
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_IT_ID.Text = cleansText(e.Item.Cells[0].Text);
					TXT_IT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					try
					{ DDL_GRIT_ID.SelectedValue = cleansText(e.Item.Cells[2].Text); }
					catch{}
					
					activatePostBackControls(false);
					break;
				case "delete":
					IT_ID = cleansText(e.Item.Cells[0].Text);
					conn.QueryString = "PARAM_GENERAL_RFINSURANCETYPE_MAKER '2', '"+ IT_ID + "', '"+
						cleansText(e.Item.Cells[1].Text) +"', '"+ cleansText(e.Item.Cells[2].Text) +"' ";
					conn.ExecuteQuery();
					activatePostBackControls(true);
					ViewMaker();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_MAKER_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//int i;
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[4].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_IT_ID.Text = cleansText(e.Item.Cells[0].Text);
					TXT_IT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					try
					{ DDL_GRIT_ID.SelectedValue = cleansText(e.Item.Cells[2].Text); }
					catch{}
					activatePostBackControls(false);
					break;
				case "delete":
					IT_ID = e.Item.Cells[0].Text.Trim();
					conn.QueryString = "DELETE FROM PENDING_RFINSURANCETYPE WHERE IT_ID = '"+ IT_ID + "' ";
					conn.ExecuteQuery();
					activatePostBackControls(true);
					ViewMaker();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void DGR_MAKER_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
