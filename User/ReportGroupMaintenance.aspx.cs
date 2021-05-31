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
using System.Configuration;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Net;
using CuBES_Maintenance.Report;

namespace CuBES_Maintenance.User
{
	/// <summary>
	/// Summary description for ReportGroupMaintenance.
	/// </summary>
	public partial class ReportGroupMaintenance : System.Web.UI.Page
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

				DDL_MODULEID.Items.Add(new ListItem("- PILIH -", ""));
				DDL_MODULEID.Items.Add(new ListItem("- Unassigned -", "U"));
				
				conn.QueryString = "select moduleid, modulename from rfmodule";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
				{
					DDL_MODULEID.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			
				}
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

		}
		#endregion

		protected void BTN_SEARCH_Click(object sender, System.EventArgs e)
		
		{	string sql="",group,module="All Module";
			if (!TXT_FINDGROUP.Text.Equals(""))
			{
				sql="where sg_grpname like '%" + TXT_FINDGROUP.Text + "%'";
				group=TXT_FINDGROUP.Text;
			}
			else
				group="All Group";

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
				Report_IP = "10.123.12.30";
			}
			//Report_IP = "10.123.12.30";
			ReportViewer1.ServerUrl = "http://" + Report_IP.ToString() + "/ReportServer";
			ReportViewer1.ReportPath = "/MaintenanceReport/RptGroupMaintenance&sql_kondisi="+sql+"&module="+module+"&group="+group+"";
            */

            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportUser"].ToString(), WebConfigurationManager.AppSettings["ReportPassword"].ToString(), WebConfigurationManager.AppSettings["DomainName"].ToString());

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(WebConfigurationManager.AppSettings["ServerUrl"].ToString());
            ReportViewer1.ServerReport.ReportPath = "/CuBES Reports/RptGroupMaintenance";

            List<ReportParameter> paramList = new List<ReportParameter>();

            paramList.Add(new ReportParameter("sql_kondisi", sql, false));
            paramList.Add(new ReportParameter("module", module, false));
            paramList.Add(new ReportParameter("group", group, false));

            ReportViewer1.ServerReport.SetParameters(paramList);
            ReportViewer1.ServerReport.Refresh();
		}
	


		protected void DDL_MODULEID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			string sql="where 1=1 ",module="All Module",group="All Group";
			if (DDL_MODULEID.SelectedValue == "U")
			{
				sql += "and groupid not in (select distinct groupid from grpaccessmodule)";
				module=DDL_MODULEID.SelectedItem.ToString();
			}
			else
			{
				sql +="and moduleid = '" + DDL_MODULEID.SelectedValue + "' ";
				module=DDL_MODULEID.SelectedItem.ToString();
			}

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
				Report_IP = "10.123.12.30";
			}
//			Report_IP = "10.123.12.30";
			ReportViewer1.ServerUrl = "http://" + Report_IP.ToString() + "/ReportServer";
			ReportViewer1.ReportPath = "/MaintenanceReport/RptGroupMaintenance&sql_kondisi="+sql+"&module="+module+"&group="+group+"";
            */

            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportUser"].ToString(), WebConfigurationManager.AppSettings["ReportPassword"].ToString(), WebConfigurationManager.AppSettings["DomainName"].ToString());

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(WebConfigurationManager.AppSettings["ServerUrl"].ToString());
            ReportViewer1.ServerReport.ReportPath = "/CuBES Reports/RptGroupMaintenance";

            List<ReportParameter> paramList = new List<ReportParameter>();

            paramList.Add(new ReportParameter("sql_kondisi", sql, false));
            paramList.Add(new ReportParameter("module", module, false));
            paramList.Add(new ReportParameter("group", group, false));

            ReportViewer1.ServerReport.SetParameters(paramList);
            ReportViewer1.ServerReport.Refresh();
		}
	}
}
