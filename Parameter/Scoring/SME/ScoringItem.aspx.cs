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
using System.Configuration;

namespace CuBES_Maintenance.Parameter.Scoring.SME
{
	/// <summary>
	/// Summary description for ScoringItem.
	/// </summary>
	public partial class ScoringItem : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack) 
			{
				LBL_SAVEMODE.Text = "1";

				ViewExistingParameterListData();
				ViewPendingParameterListData();
			}

			BTN_SAVE_LIST.Attributes.Add("onclick","if(!cek_mandatory(document.form1)){return false;};");
		}

		public void ViewExistingParameterListData()
		{ 
			conn.QueryString = "SELECT * FROM VW_PRMSCORING_SCORINGITEM_VIEWEXISTING ORDER BY PARAM_ID";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_EXISTING_LIST.DataSource = data;
			
			try
			{
				DGR_EXISTING_LIST.DataBind();
			} 
			catch 
			{
				this.DGR_EXISTING_LIST.CurrentPageIndex = DGR_EXISTING_LIST.PageCount - 1;
				DGR_EXISTING_LIST.DataBind();
			}
		}

		public void ViewPendingParameterListData()
		{
			conn.QueryString = "SELECT * FROM VW_PRMSCORING_SCORINGITEM_VIEWPENDING ORDER BY PARAM_ID";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_REQUEST_LIST.DataSource = data;
			
			try
			{
				DGR_REQUEST_LIST.DataBind();
			} 
			catch 
			{
				this.DGR_REQUEST_LIST.CurrentPageIndex = DGR_REQUEST_LIST.PageCount - 1;
				DGR_REQUEST_LIST.DataBind();
			}
		}

		private void clearEditParamListBoxes()
		{
			TXT_PARAM_ID.Enabled = true;
			TXT_PARAM_ID.Text = "";
			TXT_PARAM_NAME.Text = "";
			TXT_PARAM_FORMULA.Text = "";
			TXT_PARAM_LINK.Text = "";
			TXT_PARAM_FIELD.Text = "";
			TXT_PARAM_TABLE.Text = "";
			TXT_PARAM_TABLE_CHILD.Text = "";
			LBL_SAVEMODE.Text = "1";
		}

		private string checkApost(string str)
		{
			return str.Replace("'", "''").Trim();
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
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
			this.DGR_EXISTING_LIST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_LIST_ItemCommand);
			this.DGR_EXISTING_LIST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_LIST_PageIndexChanged);
			this.DGR_REQUEST_LIST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_LIST_ItemCommand);
			this.DGR_REQUEST_LIST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_LIST_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_LIST_Click(object sender, System.EventArgs e)
		{
			try
			{
				conn.QueryString = "EXEC PRMSCORING_SCORINGITEM_MAKER '" + 
					TXT_PARAM_ID.Text.Trim() + "', '" + 
					TXT_PARAM_NAME.Text.Trim() + "', '" + 
					checkApost(TXT_PARAM_FORMULA.Text.Trim()) + "', '" + 
					TXT_PARAM_FIELD.Text.Trim() + "', '" + 
					TXT_PARAM_TABLE.Text.Trim() + "', '" + 
					TXT_PARAM_TABLE_CHILD.Text.Trim() + "', '" +
					checkApost(TXT_PARAM_LINK.Text.Trim()) + "', '" + 
					LBL_SAVEMODE.Text.Trim() + "'";
				conn.ExecuteQuery();
			} 
			catch (Exception ex)
			{
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this, errmsg);
			}
			clearEditParamListBoxes();
			ViewPendingParameterListData();
		}

		protected void BTN_CANCEL_LIST_Click(object sender, System.EventArgs e)
		{
			clearEditParamListBoxes();
		}

		private void DGR_EXISTING_LIST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_PARAM_ID.Text = cleansText(e.Item.Cells[0].Text);
					TXT_PARAM_NAME.Text = cleansText(e.Item.Cells[1].Text);
					TXT_PARAM_FORMULA.Text =cleansText(e.Item.Cells[2].Text);
					TXT_PARAM_FIELD.Text = cleansText(e.Item.Cells[3].Text);
					TXT_PARAM_TABLE.Text = cleansText(e.Item.Cells[4].Text);
					TXT_PARAM_TABLE_CHILD.Text = cleansText(e.Item.Cells[5].Text);
					TXT_PARAM_LINK.Text = cleansText(e.Item.Cells[6].Text);
					TXT_PARAM_ID.Enabled = false;
					break;
				case "delete":
					try
					{
						conn.QueryString = "EXEC PRMSCORING_SCORINGITEM_MAKER '" + 
							cleansText(e.Item.Cells[0].Text.Trim()) + "', '" + 
							cleansText(e.Item.Cells[1].Text.Trim()) + "', '" + 
							cleansText(e.Item.Cells[2].Text.Trim()) + "', '" + 
							cleansText(e.Item.Cells[3].Text.Trim()) + "', '" + 
							cleansText(e.Item.Cells[4].Text.Trim()) + "', '" + 
							cleansText(e.Item.Cells[5].Text.Trim()) + "', '" + 
							cleansText(e.Item.Cells[6].Text.Trim()) + "', '" + 
							"2'";
						conn.ExecuteQuery();
					}
					catch (Exception ex)
					{
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this, errmsg);
					}
					ViewPendingParameterListData();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_REQUEST_LIST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[8].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						break;
					}
					TXT_PARAM_ID.Text = cleansText(e.Item.Cells[0].Text);
					TXT_PARAM_NAME.Text = cleansText(e.Item.Cells[1].Text);
					TXT_PARAM_FORMULA.Text =cleansText(e.Item.Cells[2].Text);
					TXT_PARAM_FIELD.Text = cleansText(e.Item.Cells[3].Text);
					TXT_PARAM_TABLE.Text = cleansText(e.Item.Cells[4].Text);
					TXT_PARAM_TABLE_CHILD.Text = cleansText(e.Item.Cells[5].Text);
					TXT_PARAM_LINK.Text = cleansText(e.Item.Cells[6].Text);
					TXT_PARAM_ID.Enabled = false;
					break;
				case "delete":
					try
					{
						conn.QueryString = "DELETE FROM PENDING_PRMSCORING_PARAM WHERE PARAM_ID = '"+ cleansText(e.Item.Cells[0].Text) + "' ";
						conn.ExecuteQuery();
					}
					catch (Exception ex)
					{
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this, errmsg);
					}
					ViewPendingParameterListData();
					break;
				default:
					// do nothing
					break;
			}
		}

		private void DGR_EXISTING_LIST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING_LIST.CurrentPageIndex = e.NewPageIndex;
			ViewExistingParameterListData();
		}

		private void DGR_REQUEST_LIST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST_LIST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingParameterListData();
		}
	}
}
