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
	/// Summary description for QualitativeTemplate.
	/// </summary>
	public partial class QualitativeTemplate : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack) 
			{
				LBL_SAVEMODE.Text = "1";

				ViewExistingDataTpl();
				ViewPendingDataTpl();
			}

			BTN_SAVE_TPL.Attributes.Add("onclick","if(!cek_mandatory2(document.Form1)){return false;};");
		}

		private void ViewExistingDataTpl()
		{ 
			conn.QueryString = "SELECT * FROM VW_PRMRATINGQUALTEMPLATE_VIEWEXISTING";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_EXISTING_TPL.DataSource = data;
			
			try
			{
				DGR_EXISTING_TPL.DataBind();
			} 
			catch 
			{
				this.DGR_EXISTING_TPL.CurrentPageIndex = DGR_EXISTING_TPL.PageCount - 1;
				DGR_EXISTING_TPL.DataBind();
			}
		}

		private void ViewPendingDataTpl()
		{
			conn.QueryString = "SELECT * FROM VW_PRMRATINGQUALTEMPLATE_VIEWPENDING";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_REQUEST_TPL.DataSource = data;
			
			try
			{
				DGR_REQUEST_TPL.DataBind();
			} 
			catch 
			{
				this.DGR_REQUEST_TPL.CurrentPageIndex = DGR_REQUEST_TPL.PageCount - 1;
				DGR_REQUEST_TPL.DataBind();
			}
		}

		private void clearEditBoxesTpl()
		{
			TXT_TPLID.Enabled = true;
			TXT_TPLID.Text = "";
			TXT_TPLDESC.Text = "";
			LBL_SAVEMODE.Text = "1";
		}

		private string checkComma(string str)
		{
			return str.Replace(",", ".").Trim();
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
			this.DGR_EXISTING_TPL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_TPL_ItemCommand);
			this.DGR_EXISTING_TPL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_TPL_PageIndexChanged);
			this.DGR_REQUEST_TPL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_TPL_ItemCommand);
			this.DGR_REQUEST_TPL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_TPL_PageIndexChanged);

		}
		#endregion

		protected void BTN_CANCEL_TPL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxesTpl();
		}

		protected void BTN_SAVE_TPL_Click(object sender, System.EventArgs e)
		{
			try
			{
				conn.QueryString = "EXEC PRMRATINGQUALTEMPLATE_MAKER '" + 
					TXT_TPLID.Text.Trim() + "', '" + 
					TXT_TPLDESC.Text.Trim() + "', '" + 
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
			clearEditBoxesTpl();
			ViewPendingDataTpl();
		}

		private void DGR_EXISTING_TPL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_TPLID.Text = cleansText(e.Item.Cells[0].Text);
					TXT_TPLDESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_TPLID.Enabled = false;
					break;
				case "delete":
					try
					{
						conn.QueryString = "EXEC PRMRATINGQUALTEMPLATE_MAKER '" + 
							cleansText(e.Item.Cells[0].Text.Trim()) + "', '" + 
							cleansText(e.Item.Cells[1].Text.Trim()) + "', '" + 
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
					ViewPendingDataTpl();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_REQUEST_TPL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[4].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						break;
					}
					TXT_TPLID.Text = cleansText(e.Item.Cells[0].Text);
					TXT_TPLDESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_TPLID.Enabled = false;
					break;
				case "delete":
					try
					{
						conn.QueryString = "DELETE FROM PENDING_PRMRATINGQUAL_TEMPLATE WHERE QUALTPL_ID = '"+ cleansText(e.Item.Cells[0].Text) + "' ";
						conn.ExecuteQuery();
					}
					catch (Exception ex)
					{
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this, errmsg);
					}
					ViewPendingDataTpl();
					break;
				default:
					// do nothing
					break;
			}
		}

		private void DGR_EXISTING_TPL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING_TPL.CurrentPageIndex = e.NewPageIndex;
			ViewExistingDataTpl();
		}

		private void DGR_REQUEST_TPL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST_TPL.CurrentPageIndex = e.NewPageIndex;
			ViewPendingDataTpl();
		}
	}
}
