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
using Microsoft.VisualBasic;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Net;

namespace CuBES_Maintenance.Report
{
	/// <summary>
	/// Summary description for UserMaintenance.
	/// </summary>
	public partial class UserMaintenance : System.Web.UI.Page
	{
		protected Connection conn;
		protected string sqlQuery = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());

			if (!IsPostBack)
			{
				DDL_AREAID.Items.Add(new ListItem("- ALL AREA -", ""));
				DDL_AU_DATE_FROM_M.Items.Add(new ListItem("- SELECT -", ""));
				DDL_AU_DATE_TO_M.Items.Add(new ListItem("- SELECT -", ""));
				//// branch depend on area
				//DDL_BRANCH_CODE.Items.Add(new ListItem("- ALL BRANCH -", ""));
				DDL_GROUPID.Items.Add(new ListItem("- ALL GROUP -", ""));

				for (int i = 1; i <= 12; i++)
				{
					DDL_AU_DATE_FROM_M.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
					DDL_AU_DATE_TO_M.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
				}

				conn.QueryString = "select areaid, areaname from rfarea where active = '1'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_AREAID.Items.Add(new ListItem(conn.GetFieldValue(i, "areaname"), conn.GetFieldValue(i, "areaid")));

//				conn.QueryString = "select branch_code, branch_name from rfbranch where active = '1'";
//				conn.ExecuteQuery();
//				for (int i = 0; i < conn.GetRowCount(); i++)
//					DDL_BRANCH_CODE.Items.Add(new ListItem(conn.GetFieldValue(i, "branch_code") + " - " + conn.GetFieldValue(i, "branch_name"), conn.GetFieldValue(i, "branch_code")));

				conn.QueryString = "select distinct groupid, sg_grpname from vw_scallgroup where (moduleid is not null and moduleid <> '') order by sg_grpname";// order by modulename, sg_grpname"; where moduleid = '" + DDL_MODULEID.SelectedValue + "'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_GROUPID.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));


//				conn.QueryString = sqlQuery + "and au_field = 'askldfj'";
//				conn.ExecuteQuery();
//				DataTable dt = new DataTable();
//				dt = conn.GetDataTable().Copy();
//				DatGrd.DataSource = dt;
//				DatGrd.DataBind();
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
			this.DatGrd.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGrd_PageIndexChanged);

		}
		#endregion

		protected void BTN_SEARCH_Click(object sender, System.EventArgs e)
		{
			string tanggal1="",tanggal2="",area="", branch="", group="", userid="" ;

			if (!GlobalTools.isDateValid(TXT_AU_DATE_TO_D.Text,DDL_AU_DATE_TO_M.SelectedValue,TXT_AU_DATE_TO_Y.Text))
			{
				GlobalTools.popMessage(this,"Date is not valid!");
				GlobalTools.SetFocus(this,TXT_AU_DATE_TO_D);
				return;
			}
			if (!GlobalTools.isDateValid(TXT_AU_DATE_FROM_D.Text,DDL_AU_DATE_FROM_M.SelectedValue,TXT_AU_DATE_FROM_Y.Text))
			{
				GlobalTools.popMessage(this,"Date is not valid!");
				GlobalTools.SetFocus(this,TXT_AU_DATE_FROM_D);
				return;
			}
			if (GlobalTools.compareDate(TXT_AU_DATE_TO_D.Text, DDL_AU_DATE_TO_M.SelectedValue, TXT_AU_DATE_TO_Y.Text, TXT_AU_DATE_FROM_D.Text, DDL_AU_DATE_FROM_M.SelectedValue, TXT_AU_DATE_FROM_Y.Text)< 0)
			{
				GlobalTools.popMessage(this,"Date1 must be less than Date2");
				GlobalTools.SetFocus(this,TXT_AU_DATE_FROM_D);
				return;
			}
			if (Chk_AuditTrail.Checked == true)
			{
				if (DDL_GROUPID.SelectedValue == "")
				{
					GlobalTools.popMessage(this,"Group Tidak Boleh Kosong");
					return;
				}
				else
				{
					sqlQuery += "/* viewgrpaccessmenu */";

					sqlQuery += "and GROUPID = '" + DDL_GROUPID.SelectedValue + "'";
					group = DDL_GROUPID.SelectedItem.ToString();

					if (((TXT_AU_DATE_FROM_D.Text.Trim() != "") && (DDL_AU_DATE_FROM_M.SelectedValue != "") && (TXT_AU_DATE_FROM_Y.Text.Trim() != "")) && ((TXT_AU_DATE_TO_D.Text.Trim() != "") && (DDL_AU_DATE_TO_M.SelectedValue != "") && (TXT_AU_DATE_TO_Y.Text.Trim() != "")))
					{
						sqlQuery += " AND convert(varchar,au_date,112) between "+TXT_AU_DATE_FROM_Y.Text+int.Parse(DDL_AU_DATE_FROM_M.SelectedValue).ToString("00")+int.Parse(TXT_AU_DATE_FROM_D.Text).ToString("00")+" and "+TXT_AU_DATE_TO_Y.Text+int.Parse(DDL_AU_DATE_TO_M.SelectedValue).ToString("00")+int.Parse(TXT_AU_DATE_TO_D.Text).ToString("00");
						tanggal1 = TXT_AU_DATE_FROM_D.Text.Trim() +"-" + DDL_AU_DATE_FROM_M.SelectedItem.ToString()+"-"+TXT_AU_DATE_FROM_Y.Text.Trim() ;
						tanggal2 = TXT_AU_DATE_TO_D.Text.Trim() + "-" +DDL_AU_DATE_TO_M.SelectedItem.ToString()+ "-" +TXT_AU_DATE_TO_Y.Text.Trim();
						/**sqlQuery += "and au_date > " + GlobalTools.ToSQLDate(TXT_AU_DATE_FROM_D.Text, DDL_AU_DATE_FROM_M.SelectedValue, TXT_AU_DATE_FROM_Y.Text) + " ";
						// tanggal1 = GlobalTools.ConvertDate(TXT_AU_DATE_FROM_D.Text, DDL_AU_DATE_FROM_M.SelectedItem.ToString(),TXT_AU_DATE_FROM_Y.Text);
						tanggal1 = TXT_AU_DATE_FROM_D.Text.Trim() +"-" + DDL_AU_DATE_FROM_M.SelectedItem.ToString()+"-"+TXT_AU_DATE_FROM_Y.Text.Trim() ;
						**/
					}

					/**if ((TXT_AU_DATE_TO_D.Text.Trim() != "") && (DDL_AU_DATE_TO_M.SelectedValue != "") && (TXT_AU_DATE_TO_Y.Text.Trim() != ""))
					{
						sqlQuery += "and au_date < " + GlobalTools.ToSQLDate(TXT_AU_DATE_TO_D.Text, DDL_AU_DATE_TO_M.SelectedValue, TXT_AU_DATE_TO_Y.Text) + " ";
						// tanggal2 = GlobalTools.ConvertDate(TXT_AU_DATE_TO_D.Text, DDL_AU_DATE_TO_M.SelectedItem.ToString(),TXT_AU_DATE_TO_Y.Text);
						tanggal2 = TXT_AU_DATE_TO_D.Text.Trim() + "-" +DDL_AU_DATE_TO_M.SelectedItem.ToString()+ "-" +TXT_AU_DATE_TO_Y.Text.Trim();
					}**/
					Label1.Text = "select * from vw_rpt_usrmaintenance where au_table = 'GRPACCESSMENUALL' " + sqlQuery;

				}
			}
			else if(Chk_AuditTrail.Checked == false)
			{

				if (((TXT_AU_DATE_FROM_D.Text.Trim() != "") && (DDL_AU_DATE_FROM_M.SelectedValue != "") && (TXT_AU_DATE_FROM_Y.Text.Trim() != "")) && ((TXT_AU_DATE_TO_D.Text.Trim() != "") && (DDL_AU_DATE_TO_M.SelectedValue != "") && (TXT_AU_DATE_TO_Y.Text.Trim() != "")))
				{
					sqlQuery += " AND convert(varchar,au_date,112) between "+TXT_AU_DATE_FROM_Y.Text+int.Parse(DDL_AU_DATE_FROM_M.SelectedValue).ToString("00")+int.Parse(TXT_AU_DATE_FROM_D.Text).ToString("00")+" and "+TXT_AU_DATE_TO_Y.Text+int.Parse(DDL_AU_DATE_TO_M.SelectedValue).ToString("00")+int.Parse(TXT_AU_DATE_TO_D.Text).ToString("00");
					tanggal1 = TXT_AU_DATE_FROM_D.Text.Trim() +"-" + DDL_AU_DATE_FROM_M.SelectedItem.ToString()+"-"+TXT_AU_DATE_FROM_Y.Text.Trim() ;
					tanggal2 = TXT_AU_DATE_TO_D.Text.Trim() + "-" +DDL_AU_DATE_TO_M.SelectedItem.ToString()+ "-" +TXT_AU_DATE_TO_Y.Text.Trim();
				}

				/**if ((TXT_AU_DATE_FROM_D.Text.Trim() != "") && (DDL_AU_DATE_FROM_M.SelectedValue != "") && (TXT_AU_DATE_FROM_Y.Text.Trim() != ""))
				{
					sqlQuery += "and au_date > " + GlobalTools.ToSQLDate(TXT_AU_DATE_FROM_D.Text, DDL_AU_DATE_FROM_M.SelectedValue, TXT_AU_DATE_FROM_Y.Text) + " ";
					// tanggal1 = GlobalTools.ConvertDate(TXT_AU_DATE_FROM_D.Text, DDL_AU_DATE_FROM_M.SelectedItem.ToString(),TXT_AU_DATE_FROM_Y.Text);
					tanggal1 = TXT_AU_DATE_FROM_D.Text.Trim() +"-" + DDL_AU_DATE_FROM_M.SelectedItem.ToString()+"-"+TXT_AU_DATE_FROM_Y.Text.Trim() ;
				}

				if ((TXT_AU_DATE_TO_D.Text.Trim() != "") && (DDL_AU_DATE_TO_M.SelectedValue != "") && (TXT_AU_DATE_TO_Y.Text.Trim() != ""))
				{
					sqlQuery += "and au_date < " + GlobalTools.ToSQLDate(TXT_AU_DATE_TO_D.Text, DDL_AU_DATE_TO_M.SelectedValue, TXT_AU_DATE_TO_Y.Text) + " ";
					// tanggal2 = GlobalTools.ConvertDate(TXT_AU_DATE_TO_D.Text, DDL_AU_DATE_TO_M.SelectedItem.ToString(),TXT_AU_DATE_TO_Y.Text);
					tanggal2 = TXT_AU_DATE_TO_D.Text.Trim() + "-" +DDL_AU_DATE_TO_M.SelectedItem.ToString()+ "-" +TXT_AU_DATE_TO_Y.Text.Trim();
				}**/

				if (DDL_AREAID.SelectedValue != "")
				{
					sqlQuery += "and areaid = '" + DDL_AREAID.SelectedValue + "' ";
					area = DDL_AREAID.SelectedItem.ToString();
				}
				if (DDL_BRANCH_CODE.SelectedValue != "")
				{
					sqlQuery += "and su_branch = '" + DDL_BRANCH_CODE.SelectedValue + "'";
					branch = DDL_BRANCH_CODE.SelectedItem.ToString();
				}
				if (DDL_GROUPID.SelectedValue != "")
				{
					sqlQuery += "and GROUPID = '" + DDL_GROUPID.SelectedValue + "'";
					group = DDL_GROUPID.SelectedItem.ToString();
				}
				if (TXT_USERID.Text.Trim() != "")
				{
					sqlQuery += "and au_id = '" + TXT_USERID.Text.Trim() + "'";
					userid = TXT_USERID.Text.Trim();
				}
				Label1.Text = "select * from vw_rpt_usrmaintenance where au_table = 'SCALLUSER' " + sqlQuery;

			}
			
			
			DatGrd.CurrentPageIndex = 0;
			IsiDataGrid();

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
			ReportViewer1.ServerUrl = "http://" + Report_IP.ToString() + "/ReportServer";  // tanggal1="",tanggal2="",area="", branch="", group="", userid=""
			ReportViewer1.ReportPath = "/CuBES Reports/RptMntUserAudit&sql_kondisi="+sqlQuery+"&tanggal1="+tanggal1+"&tanggal2="+tanggal2+ "&area="+area+"&branch="+branch+"&group="+group+"&userid="+userid+" ";
            */
           
            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportUser"].ToString(), WebConfigurationManager.AppSettings["ReportPassword"].ToString(), WebConfigurationManager.AppSettings["DomainName"].ToString());

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(WebConfigurationManager.AppSettings["ServerUrl"].ToString());
            ReportViewer1.ServerReport.ReportPath = "/CuBES Reports/RptMntUserAudit";

            List<ReportParameter> paramList = new List<ReportParameter>();

            paramList.Add(new ReportParameter("sql_kondisi", sqlQuery, false));
            paramList.Add(new ReportParameter("tanggal1", tanggal1, false));
            paramList.Add(new ReportParameter("tanggal2", tanggal2, false));
            paramList.Add(new ReportParameter("area", area, false));
            paramList.Add(new ReportParameter("branch", branch, false));
            paramList.Add(new ReportParameter("group", group, false));
            paramList.Add(new ReportParameter("userid", userid, false));

            ReportViewer1.ServerReport.SetParameters(paramList);
            ReportViewer1.ServerReport.Refresh();

		}

		private void IsiDataGrid()
		{
			conn.QueryString = Label1.Text;
			conn.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			
			DatGrd.DataSource = dt;
			DatGrd.DataBind();

			int startNo = (DatGrd.CurrentPageIndex * 10) + 1;
			for (int i = 0; i < DatGrd.Items.Count; i++)
			{
				DatGrd.Items[i].Cells[0].Text = startNo.ToString();
				startNo++;
			}
		}

		private void DatGrd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DatGrd.CurrentPageIndex = e.NewPageIndex;
			IsiDataGrid();
		}

		protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
		{
			TXT_AU_DATE_FROM_D.Text		= "";
			TXT_AU_DATE_FROM_Y.Text		= "";
			TXT_AU_DATE_TO_D.Text		= "";
			TXT_AU_DATE_TO_Y.Text		= "";
			TXT_USERID.Text				= "";
			DDL_AREAID.SelectedValue	= "";
			DDL_AU_DATE_FROM_M.SelectedValue	= "";
			DDL_AU_DATE_TO_M.SelectedValue		= "";
			DDL_BRANCH_CODE.SelectedValue		= "";
			DDL_GROUPID.SelectedValue			= "";
			Chk_AuditTrail.Checked				= false;
		}

		protected void DDL_AREAID_SelectedIndexChanged(object sender, System.EventArgs e)
		{

//			conn.QueryString = "select branch_code, branch_name from rfbranch where active = '1'";
//			conn.ExecuteQuery();
//			for (int i = 0; i < conn.GetRowCount(); i++)
//				DDL_BRANCH_CODE.Items.Add(new ListItem(conn.GetFieldValue(i, "branch_code") + " - " + conn.GetFieldValue(i, "branch_name"), conn.GetFieldValue(i, "branch_code")));

			GlobalTools.fillRefList(DDL_BRANCH_CODE,"select branch_code, branch_name from rfbranch where active = '1' and areaid ='"+DDL_AREAID.SelectedValue+"' ",conn);

			
		
		}
	}
}
