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
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFCA_ASPEKPRGAppr.
	/// </summary>
	public partial class RFRCA_SCORECARDLINKAPR : System.Web.UI.Page
	{
	
		private Connection conn;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if (!IsPostBack) 
			{
				viewDataPending();
			}
		}

		private void viewDataPending() 
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_PENDING_RFRCALINK";
			conn.ExecuteQuery();

			DTG_APPR.DataSource = conn.GetDataTable().DefaultView;
			try { DTG_APPR.DataBind(); } 
			catch 
			{ 
				DTG_APPR.CurrentPageIndex = 0;
				DTG_APPR.DataBind();
			}
		}

		private void performRequest(int row, string _status)
		{
			try 
			{
				string _loanpurpid = DTG_APPR.Items[row].Cells[0].Text.Trim();
				string _productid = DTG_APPR.Items[row].Cells[2].Text.Trim();
				string _rcaid = DTG_APPR.Items[row].Cells[4].Text.Trim();
                string _deviasi_flag = DTG_APPR.Items[row].Cells[6].Text.Replace("&nbsp;", "").Trim();
                string _deviasi_action = DTG_APPR.Items[row].Cells[8].Text.Replace("&nbsp;", "").Trim();
                string _deviasi_route = DTG_APPR.Items[row].Cells[10].Text.Replace("&nbsp;", "").Trim();

				conn.QueryString = "exec PARAM_GENERAL_RFRCALINK_APPR '" +
                    _loanpurpid + "', '" + _productid + "', '" + _rcaid + "', '" + _deviasi_flag + "', '" + _deviasi_action + "', '" + _deviasi_route +  "', '" + _status + "', '" + Session["UserID"].ToString() + "'";
				conn.ExecuteNonQuery();				
			} 
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
			this.DTG_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DTG_APPR_ItemCommand);
			this.DTG_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DTG_APPR_PageIndexChanged);

		}
		#endregion

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DTG_APPR.Items.Count; i++)
			{
				try					
				{
					RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
								rbR = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2");
					if (rbA.Checked)
					{
						performRequest(i, "1");
					}
					else if (rbR.Checked)
					{
						performRequest(i, "0");
					}
				} 
				catch {}
			}
			viewDataPending();
		}

		private void DTG_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try { DTG_APPR.CurrentPageIndex = e.NewPageIndex; } 
			catch { DTG_APPR.CurrentPageIndex = 0; }
			viewDataPending();
		}

		private void DTG_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DTG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
								rbB = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2"),
								rbC = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton3");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DTG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
								rbB = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2"),
								rbC = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton3");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DTG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
								rbB = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2"),
								rbC = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton3");
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}
	}
}
