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
	/// Summary description for ScoringItemAppr.
	/// </summary>
	public partial class ScoringItemAppr : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				ViewPendingParameterListData();
			}
		}

		public void ViewPendingParameterListData()
		{
			conn.QueryString = "SELECT * FROM VW_PRMSCORING_SCORINGITEM_VIEWPENDING ORDER BY PARAM_ID";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_APPR_LIST.DataSource = data;
			
			try
			{
				DGR_APPR_LIST.DataBind();
			} 
			catch 
			{
				this.DGR_APPR_LIST.CurrentPageIndex = DGR_APPR_LIST.PageCount - 1;
				DGR_APPR_LIST.DataBind();
			}
		}

		private void performRequestList(int row, char appr_sta, string userid)
		{
			string paramid = DGR_APPR_LIST.Items[row].Cells[0].Text.Trim();
			
			try 
			{
				conn.QueryString = "EXEC PRMSCORING_SCORINGITEM_APPR '" + paramid + "', '" + appr_sta + "', '" + userid + "'";
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
			this.DGR_APPR_LIST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR_LIST_ItemCommand);
			this.DGR_APPR_LIST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPR_LIST_PageIndexChanged);

		}
		#endregion

		protected void BTN_SUBMIT_LIST_Click(object sender, System.EventArgs e)
		{
			string scid = (string)Session["UserID"];

			for (int i = 0; i < DGR_APPR_LIST.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Approve"),
						rbR = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Reject");
					if (rbA.Checked)
						performRequestList(i, '1', scid);
					else if (rbR.Checked)
						performRequestList(i, '0', scid);
				} 
				catch {}
			}
			ViewPendingParameterListData();
		}

		private void DGR_APPR_LIST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR_LIST.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_APPR_LIST.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR_LIST.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Pending");
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

		private void DGR_APPR_LIST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_APPR_LIST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingParameterListData();
		}
	}
}
