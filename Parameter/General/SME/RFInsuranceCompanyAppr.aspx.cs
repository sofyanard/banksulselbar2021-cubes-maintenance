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
	/// Summary description for RFInsuranceCompanyAppr.
	/// </summary>
	public partial class RFInsuranceCompanyAppr : System.Web.UI.Page
	{
		string IC_ID;
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

		//	if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//	Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack)
			{
				ViewAppr();
			}
			DGR_APPR.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change);
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
			this.DGR_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR_ItemCommand);

		}
		#endregion

		private void ViewAppr()
		{
			conn.QueryString = "select IC_ID, IC_DESC "+
				", isnull(IC_ADDR1, '') +' '+ isnull(IC_ADDR2, '') +' '+ isnull(IC_ADDR3, '') IC_ADDR "+
				", IC_CITY, IC_ZIPCODE, IC_CONTACT "+
				", isnull(ACTIVE, '0') ACTIVE, PENDINGSTATUS "+
				", case when PENDINGSTATUS = '0' then 'Update' when PENDINGSTATUS = '2' then 'Delete' "+
				" else 'Insert' end PENDINGDESC "+
				"FROM PENDING_RFINSURANCECOMPANY ";
			conn.ExecuteQuery();
			DGR_APPR.DataSource = conn.GetDataTable().Copy();
			try 
			{
				DGR_APPR.DataBind();
			}
			catch 
			{
				DGR_APPR.CurrentPageIndex = DGR_APPR.PageCount - 1;
				DGR_APPR.DataBind();
			}
		}

		void Grid_Change(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DGR_APPR.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			ViewAppr();	
		}
		
		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGR_APPR.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject");
					if (rbA.Checked)
						performRequest(i, '1');
					else if (rbR.Checked)
						performRequest(i, '0');
				} 
				catch {}
			}
			ViewAppr();
		}

		private void performRequest(int row, char appr_sta)
		{
			try 
			{
				IC_ID = DGR_APPR.Items[row].Cells[1].Text.Trim();
				conn.QueryString = "PARAM_GENERAL_RFINSURANCECOMPANY_APPR '" + IC_ID + "', '"+ appr_sta +"', '" + 
					Session["UserID"].ToString() + "'";
				conn.ExecuteQuery();
			} 
			catch {}
		}

		private void DGR_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Pending");
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

		protected void DGR_APPR_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
