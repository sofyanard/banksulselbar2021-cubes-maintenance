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
using System.Web.Security;
using DMS.DBConnection;
using System.Configuration;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Net;
using CuBES_Maintenance.Report;

namespace CuBES_Maintenance.User
{
	/// <summary>
	/// Summary description for ReportUserMaintenance.
	/// </summary>
	public partial class ReportUserMaintenance : System.Web.UI.Page
	{
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(Session["ConnString"].ToString());

			if (!IsPostBack)
            {
				HyperLink1.NavigateUrl = "ReportUserMaintenance.aspx?mc=" + Request.QueryString["mc"];
				HyperLink2.NavigateUrl = "ReportGroupMaintenance.aspx?mc=" + Request.QueryString["mc"];

				DDL_RFMODULE.Items.Add(new ListItem("- PILIH -", ""));
				DDL_RFAREA.Items.Add(new ListItem("- PILIH -", ""));
				DDL_RFGROUP.Items.Add(new ListItem("- PILIH -", ""));
				DDL_RFBRANCH.Items.Add(new ListItem("- PILIH -", ""));
				conn.QueryString = "select moduleid, modulename from rfmodule where active = '1'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_RFMODULE.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));

				conn.QueryString = "select areaid, areaname from rfarea where active = '1'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_RFAREA.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));

				/* // branch terisi setelah area terisi
				conn.QueryString = "select branch_code, branch_code + ' - ' + branch_name from rfbranch where active = '1' order by branch_code";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_RFBRANCH.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
				*/

				conn.QueryString = "select distinct groupid, sg_grpname from vw_scallgroup where (moduleid is not null and moduleid <> '') order by sg_grpname";// order by modulename, sg_grpname"; where moduleid = '" + DDL_MODULEID.SelectedValue + "'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
				{ 
					DDL_RFGROUP.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
					//DDL_GROUPID.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
				}

				// Set all module specific setting to invisible
				
			}
		}

		private void FillGrid()
		{
			string sql = "WHERE", sjoin = "AND",module,area,branch,group;
			if (DDL_RFMODULE.SelectedValue != "")
			{
				sql = sql + " MODULEID = '" + DDL_RFMODULE.SelectedValue + "' " + sjoin;
				module=DDL_RFMODULE.SelectedItem.ToString();
			}
			else
				module="All Module";

			if (DDL_RFAREA.SelectedValue != "")
			{
				sql = sql + " AREAID = '" + DDL_RFAREA.SelectedValue + "' " + sjoin;
				area=DDL_RFAREA.SelectedItem.ToString();
			}
			else
				area= "All Area";
			
			//if (TXT_RFBRANCH.Text.Trim() != "")
			//	sql = sql + " BRANCH like '%" + TXT_RFBRANCH.Text + "%' " + sjoin;
			if (DDL_RFBRANCH.SelectedValue != "")
			{
				sql = sql + " SU_BRANCH = '" + DDL_RFBRANCH.SelectedValue + "' " + sjoin;
				branch=DDL_RFBRANCH.SelectedItem.ToString();
			}
			else
				branch="All Branch";
		
			if (DDL_RFGROUP.SelectedValue != "")
			{
				sql = sql + " GROUPID = '" + DDL_RFGROUP.SelectedValue + "' " + sjoin;
				group=DDL_RFGROUP.SelectedItem.ToString();
			}
			else
				group="All Group";

			if (TXT_SEARCH_USERID.Text.Trim() != "")
				sql = sql + " USERID like '%" + TXT_SEARCH_USERID.Text.Trim() + "%' " + sjoin;
			if (TXT_SEARCH_USERNAME.Text.Trim() != "")
				sql = sql + " SU_FULLNAME like '%" + TXT_SEARCH_USERNAME.Text.Trim() + "%' " + sjoin;
			if (TXT_SEARCH_OFFICERCODE.Text.Trim() != "")
				sql = sql + " OFFICER_CODE = '" + TXT_SEARCH_OFFICERCODE.Text.Trim() + "' " + sjoin;

			if (sql == "WHERE")
				sql = "";
			else
				sql = sql.Substring(0, sql.Length - sjoin.Length) + " AND ACTIVE = '1'";

			//LBL_SqlStatement.Text = SqlStatement + sql;

			/*
            string Report_IP="";
			conn.QueryString = "select report_ip from rfmodule where moduleid=01";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				Report_IP = conn.GetFieldValue(0,"REPORT_IP").ToString();
			}
			else
			{
				Report_IP = "10.123.13.18";
			}
			//Report_IP = "10.123.12.30";
			ReportViewer1.ServerUrl = "http://" + Report_IP.ToString() + "/ReportServer";
			ReportViewer1.ReportPath = "/MaintenanceReport/RptUserMaintenance&sql_kondisi="+sql+"&module="+module+"&branch="+branch+ "&userid="+TXT_SEARCH_USERNAME.Text+"&officercode="+TXT_SEARCH_USERID.Text+"&username="+TXT_SEARCH_USERNAME.Text+"&group="+group+"&area="+area+" ";
            */

            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportUser"].ToString(), WebConfigurationManager.AppSettings["ReportPassword"].ToString(), WebConfigurationManager.AppSettings["DomainName"].ToString());

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(WebConfigurationManager.AppSettings["ServerUrl"].ToString());
            ReportViewer1.ServerReport.ReportPath = "/CuBES Reports/RptUserMaintenance";

            List<ReportParameter> paramList = new List<ReportParameter>();

            paramList.Add(new ReportParameter("sql_kondisi", sql, false));
            paramList.Add(new ReportParameter("module", module, false));
            paramList.Add(new ReportParameter("branch", branch, false));
            paramList.Add(new ReportParameter("userid", TXT_SEARCH_USERNAME.Text, false));
            paramList.Add(new ReportParameter("officercode", TXT_SEARCH_USERID.Text, false));
            paramList.Add(new ReportParameter("username", TXT_SEARCH_USERNAME.Text, false));
            paramList.Add(new ReportParameter("group", group, false));
            paramList.Add(new ReportParameter("area", area, false));

            ReportViewer1.ServerReport.SetParameters(paramList);
            ReportViewer1.ServerReport.Refresh();
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

		}
		#endregion

		private void DDL_RFMODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

	
		private void BTN_SAVE_Click(object sender, System.EventArgs e)
		{FillGrid();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
		FillGrid();
		}

		private void TXT_SU_EMAIL_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void BTN_SEARCH_Click(object sender, System.EventArgs e)
		{
			FillGrid();
		}

		protected void DDL_RFAREA_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			GlobalTools.fillRefList( DDL_RFBRANCH, "select branch_code, branch_name from rfbranch where active = '1' and areaid = '" + DDL_RFAREA.SelectedValue + "' order by branch_code", true, conn );		
		}
	}
}
