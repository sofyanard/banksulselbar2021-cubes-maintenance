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

namespace CuBES_Maintenance.Parameter.Area.SME
{
	/// <summary>
	/// Summary description for RFProcedurePathAppr.
	/// </summary>
	public partial class RFProcedurePathAppr : System.Web.UI.Page
	{
		string AREAID, PROGRAMID, PRODUCTID, TRACKFROM;
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//conn = (Connection) Session["Connection"];

			//if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//	Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack)
			{
				ViewAppr();
			}
			DGR_APPR.PageIndexChanged += new DataGridPageChangedEventHandler(this.CH_DGR_APPR);
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
			conn.QueryString = "SELECT * FROM VW_PARAM_GENERAL_PROCEDUREPATHMAKER ";
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
		
		void CH_DGR_APPR(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DGR_APPR.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			ViewAppr();
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
			ViewAppr();
		}

		private void performRequest(int row, string tipe)
		{
			string PENDINGSTATUS;
			try 
			{
				PENDINGSTATUS = DGR_APPR.Items[row].Cells[0].Text.Trim();
				AREAID = DGR_APPR.Items[row].Cells[2].Text.Trim();
				PROGRAMID = DGR_APPR.Items[row].Cells[4].Text.Trim();
				PRODUCTID = DGR_APPR.Items[row].Cells[6].Text.Trim();
				TRACKFROM = DGR_APPR.Items[row].Cells[8].Text.Trim();
				
				conn.QueryString = "exec PARAM_GENERAL_PROCEDUREPATH_APPR '"+ tipe +"', '" + AREAID + "', '"+
					PROGRAMID +"', '"+ PRODUCTID +"', '"+ TRACKFROM +"', '"+ PENDINGSTATUS +"' ";
				conn.ExecuteNonQuery();
			} 
			catch {}
			//ViewAppr();
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../AreaParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleID=01");
			Response.Redirect("../../CommonParamApproval.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void RBL_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.RBL_MODULE.SelectedValue == "20" || this.RBL_MODULE.SelectedValue == "40")
				Response.Redirect("../../General/PathProcedureListAllAppr.aspx?moduleID=" + 
					RBL_MODULE.SelectedValue);
		}
	}
}
