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
	/// Summary description for QualitativeTemplateAppr.
	/// </summary>
	public partial class QualitativeTemplateAppr : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				ViewPendingDataTpl();
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

		private void performRequestTpl(int row, char appr_sta, string userid)
		{
			string qualtplid = DGR_REQUEST_TPL.Items[row].Cells[0].Text.Trim();
			
			try 
			{
				conn.QueryString = "EXEC PRMRATINGQUALTEMPLATE_APPR '" + qualtplid + "', '" + appr_sta + "', '" + userid + "'";
				conn.ExecuteNonQuery();
			} 
			catch (Exception ex)
			{
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this, errmsg);
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
			this.DGR_REQUEST_TPL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_TPL_ItemCommand);
			this.DGR_REQUEST_TPL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_TPL_PageIndexChanged);

		}
		#endregion

		protected void BTN_SUBMIT_TPL_Click(object sender, System.EventArgs e)
		{
			string scid = (string)Session["UserID"];

			for (int i = 0; i < DGR_REQUEST_TPL.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_REQUEST_TPL.Items[i].FindControl("rd_Approve"),
						rbR = (RadioButton) DGR_REQUEST_TPL.Items[i].FindControl("rd_Reject");
					if (rbA.Checked)
						performRequestTpl(i, '1', scid);
					else if (rbR.Checked)
						performRequestTpl(i, '0', scid);
				} 
				catch {}
			}
			ViewPendingDataTpl();
		}

		private void DGR_REQUEST_TPL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_REQUEST_TPL.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_REQUEST_TPL.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_REQUEST_TPL.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_REQUEST_TPL.Items[i].FindControl("rd_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DGR_REQUEST_TPL.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_REQUEST_TPL.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_REQUEST_TPL.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_REQUEST_TPL.Items[i].FindControl("rd_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_REQUEST_TPL.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_REQUEST_TPL.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_REQUEST_TPL.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_REQUEST_TPL.Items[i].FindControl("rd_Pending");
							rbA.Checked = false;
							rbB.Checked = false;
							rbC.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_REQUEST_TPL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST_TPL.CurrentPageIndex = e.NewPageIndex;
			ViewPendingDataTpl();
		}
	}
}
