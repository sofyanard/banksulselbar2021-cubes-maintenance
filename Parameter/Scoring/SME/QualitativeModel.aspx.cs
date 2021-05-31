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
	/// Summary description for QualitativeModel.
	/// </summary>
	public partial class QualitativeModel : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack) 
			{
				LBL_SAVEMODE.Text = "1";

				FillDDL();

				ViewExistingData();
				ViewPendingData();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory2(document.Form1)){return false;};");
		}

		private void FillDDL()
		{
			GlobalTools.fillRefList(DDL_PROGRAM, "SELECT DISTINCT PROGRAMID, PROGRAMDESC FROM RFPROGRAM", conn);
			GlobalTools.fillRefList(DDL_TEMPLATE, "SELECT QUALTPL_ID, QUALTPL_DESC FROM PRMRATINGQUAL_TEMPLATE", conn);
		}

		private void ViewExistingData()
		{
			conn.QueryString = "SELECT * FROM VW_PRMRATINGQUALMODEL_VIEWEXISTING";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_EXISTING.DataSource = data;
			
			try
			{
				DGR_EXISTING.DataBind();
			} 
			catch 
			{
				this.DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.PageCount - 1;
				DGR_EXISTING.DataBind();
			}
		}

		private void ViewPendingData()
		{
			conn.QueryString = "SELECT * FROM VW_PRMRATINGQUALMODEL_VIEWPENDING";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_REQUEST.DataSource = data;
			
			try
			{
				DGR_REQUEST.DataBind();
			} 
			catch 
			{
				this.DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				DGR_REQUEST.DataBind();
			}
		}

		private void clearEntry()
		{
			try {DDL_PROGRAM.SelectedValue = ""; }
			catch {}
			try {DDL_TEMPLATE.SelectedValue = ""; }
			catch {}
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
			this.DGR_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_ItemCommand);
			this.DGR_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

		}
		#endregion

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEntry();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			try
			{
				conn.QueryString = "EXEC PRMRATINGQUALMODEL_MAKER '" + 
					DDL_PROGRAM.SelectedValue.Trim() + "', '" + 
					DDL_TEMPLATE.SelectedValue.Trim() + "', '" + 
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
			clearEntry();
			ViewPendingData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "delete":
					try
					{
						conn.QueryString = "EXEC PRMRATINGQUALMODEL_MAKER '" + 
							e.Item.Cells[0].Text.Trim() + "', '" + 
							e.Item.Cells[2].Text.Trim() + "', '" + 
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
					ViewPendingData();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "delete":
					try
					{
						conn.QueryString = "DELETE FROM PENDING_PRMRATINGQUAL_MODEL WHERE PROGRAMID = '"+ e.Item.Cells[0].Text + "' AND QUALTPL_ID = '" + e.Item.Cells[2].Text + "'";
						conn.ExecuteQuery();
					}
					catch (Exception ex)
					{
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this, errmsg);
					}
					ViewPendingData();
					break;
				default:
					// do nothing
					break;
			}
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}
	}
}
