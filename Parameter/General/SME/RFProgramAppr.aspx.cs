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
	/// Summary description for RFProgramAppr.
	/// </summary>
	public partial class RFProgramAppr : System.Web.UI.Page
	{
	
		protected Connection conn;
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV;uid=sa;pwd=");

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

			//if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//	Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack)
			{
				bindData1();
				bindData2();
			}
			DTG_APPR.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change);
			DTG_APPR2.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change2);
		}

		private void bindData1()
		{
			conn.QueryString = "SELECT AREANAME, PROGRAMID, PROGRAMDESC, "+
				"case WITHFAIRISAAC when '1' then 'YES' when '0' then 'NO' end WITHFAIRISAAC, SCRDESC, " +
				"AREAID, SCRID, case APRVFOUREYES when '1' then 'YES' when '0' then 'NO' end APRVFOUREYES, " +
				"case WITHDRAWL when '1' then 'YES' when '0' then 'NO' end WITHDRAWL, BUSINESSUNIT, BUSSUNITDESC, " +
				"STATUSDESC, PENDINGSTATUS " +
				"FROM VW_PARAM_GENERAL_PENDING_RFPROGRAM ";
			conn.ExecuteQuery();
			DTG_APPR.DataSource = conn.GetDataTable().Copy();
			try 
			{
				DTG_APPR.DataBind();
			}
			catch 
			{
				DTG_APPR.CurrentPageIndex = DTG_APPR.PageCount - 1;
				DTG_APPR.DataBind();
			}

		}

		private void bindData2()
		{
			conn.QueryString = "SELECT PRODUCTID, PRODUCTDESC, PROGRAMID, PROGRAMDESC, "+
				"STATUSDESC, PENDINGSTATUS "+
				"FROM VW_PARAM_GENERAL_PENDING_PROG_PROD ";
			conn.ExecuteQuery();
			DTG_APPR2.DataSource = conn.GetDataTable().Copy();
			try 
			{
				DTG_APPR2.DataBind();
			}
			catch 
			{
				DTG_APPR2.CurrentPageIndex = DTG_APPR2.PageCount - 1;
				DTG_APPR2.DataBind();
			}
		}

		private void performRequest1(int row)
		{
			try 
			{
				string area = DTG_APPR.Items[row].Cells[7].Text.Trim(),
					prog = DTG_APPR.Items[row].Cells[2].Text.Trim();
				conn.QueryString = "PARAM_GENERAL_RFPROGRAM_APPR '" + area + "', '" + prog + "', '1', '" + 
					Session["UserID"].ToString() + "'";
				conn.ExecuteQuery();
			} 
			catch {}
		}

		private void deleteData1(int row)
		{
			try 
			{
				string area = DTG_APPR.Items[row].Cells[7].Text.Trim(),
					prog = DTG_APPR.Items[row].Cells[2].Text.Trim();
				conn.QueryString = "PARAM_GENERAL_RFPROGRAM_APPR '" + area + "', '" + prog + "', '0', '" + 
					Session["UserID"].ToString() + "'";
				conn.ExecuteQuery();
			} 
			catch {}
		}

		private void performRequest2(int row)
		{
			try 
			{
				string prog = DTG_APPR2.Items[row].Cells[3].Text.Trim(),
					prod = DTG_APPR2.Items[row].Cells[4].Text.Trim();
				conn.QueryString = "PARAM_GENERAL_PROG_PROD_APPR '" + prog + "', '" + prod + "', '1', '" + Session["UserID"] + "'";
				conn.ExecuteQuery();
			} 
			catch {}
		}

		private void deleteData2(int row)
		{
			try 
			{
				string prog = DTG_APPR2.Items[row].Cells[3].Text.Trim(),
					prod = DTG_APPR2.Items[row].Cells[4].Text.Trim();
				conn.QueryString = "PARAM_GENERAL_PROG_PROD_APPR '" + prog + "', '" + prod + "', '0', '" + Session["UserID"] + "'";
				conn.ExecuteQuery();
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
			this.DTG_APPR2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DTG_APPR2_ItemCommand);

		}
		#endregion

		void Grid_Change(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DTG_APPR.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData1();	
		}

		void Grid_Change2(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DTG_APPR2.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData2();	
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			int i;
			for (i = 0; i < DTG_APPR.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DTG_APPR.Items[i].FindControl("rdo_Reject");
					if (rbA.Checked)
					{
						performRequest1(i);
					}
					else if (rbR.Checked)
					{
						deleteData1(i);
					}
				} 
				catch {}
			}
			for (i = 0; i < DTG_APPR2.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DTG_APPR2.Items[i].FindControl("rdo_Approve2"),
						rbR = (RadioButton) DTG_APPR2.Items[i].FindControl("rdo_Reject2");
					if (rbA.Checked)
					{
						performRequest2(i);
					}
					else if (rbR.Checked)
					{
						deleteData2(i);
					}
				} 
				catch {}
			}
			bindData1();
			bindData2();
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
							RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DTG_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DTG_APPR.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DTG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DTG_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DTG_APPR.Items[i].FindControl("rdo_Pending");
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
							RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DTG_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DTG_APPR.Items[i].FindControl("rdo_Pending");
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

		private void DTG_APPR2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DTG_APPR2.Items.Count; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR2.Items[i].FindControl("rdo_Approve2"),
								rbB = (RadioButton) DTG_APPR2.Items[i].FindControl("rdo_Reject2"),
								rbC = (RadioButton) DTG_APPR2.Items[i].FindControl("rdo_Pending2");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DTG_APPR2.Items.Count; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR2.Items[i].FindControl("rdo_Approve2"),
								rbB = (RadioButton) DTG_APPR2.Items[i].FindControl("rdo_Reject2"),
								rbC = (RadioButton) DTG_APPR2.Items[i].FindControl("rdo_Pending2");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DTG_APPR2.Items.Count; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR2.Items[i].FindControl("rdo_Approve2"),
								rbB = (RadioButton) DTG_APPR2.Items[i].FindControl("rdo_Reject2"),
								rbC = (RadioButton) DTG_APPR2.Items[i].FindControl("rdo_Pending2");
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
				//Response.Redirect("../../AreaParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}
	}
}
