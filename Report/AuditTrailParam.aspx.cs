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
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Net;

namespace CuBES_Maintenance.Report
{
	/// <summary>
	/// Summary description for AuditTrailParam.
	/// </summary>
	public partial class AuditTrailParam : System.Web.UI.Page
	{
		protected Connection conn,conn2,connsme;
		protected string queryAll;
			
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);//conn MNT
			SetDBConn();
			if (!IsPostBack)
			{
				SetModuleName();
				FillDDL();
			}
			this.DGR_LIST.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_LIST_PageIndexChanged);
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
			this.DGR_LIST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_LIST_PageIndexChanged);

		}
		#endregion

		private void SetModuleName()
		{
			conn.QueryString = "select MODULENAME from RFMODULE WHERE MODULEID = '" + Request.QueryString["ModuleID"] + "'";
			conn.ExecuteQuery();
			this.LBL_MODULENAME.Text	= conn.GetFieldValue("MODULENAME");
		}

		private void SetDBConn()
		{
			string DB_NAMA,DB_NAMA_SME;
			string DB_IP,DB_IP_SME,DB_LOGINID_SME,DB_LOGINPWD_SME;
			string DB_LOGINID;
			string DB_LOGINPWD;
			conn.QueryString = "select * from VW_GETCONN where MODULEID='99'";
			conn.ExecuteQuery();
			string DB_NAMA_MNT = conn.GetFieldValue("DB_NAMA");
			string DB_IP_MNT	= conn.GetFieldValue("DB_IP");
			string DB_LOGINID_MNT	= conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD_MNT	= conn.GetFieldValue("DB_LOGINPWD");
			LBL_DB_IP_MNT.Text	= DB_IP_MNT;
			LBL_DB_NAMA_MNT.Text	= DB_NAMA_MNT;
			LBL_DB_LOGINID_MNT.Text	= DB_LOGINID_MNT;
			LBL_DB_LOGINPWD_MNT.Text	= DB_LOGINPWD_MNT;
			conn.ClearData();
			conn.QueryString = "select * from VW_GETCONN where MODULEID='01'";
			conn.ExecuteQuery();
			DB_NAMA_SME = conn.GetFieldValue("DB_NAMA");
			DB_IP_SME	= conn.GetFieldValue("DB_IP");
			DB_LOGINID_SME	= conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD_SME	= conn.GetFieldValue("DB_LOGINPWD");
			conn.ClearData();
			connsme = new Connection ("Data Source=" + DB_IP_SME + ";Initial Catalog=" + DB_NAMA_SME + ";uid=" + DB_LOGINID_SME + ";pwd=" + DB_LOGINPWD_SME + ";Pooling=true");
			this.LBL_DB_IP_SME.Text		=DB_IP_SME;
			this.LBL_DB_NAMA_SME.Text	=DB_NAMA_SME;
			this.LBL_DB_LOGINID_SME.Text	= DB_LOGINID_SME;
			this.LBL_DB_LOGINPWD_SME.Text	= DB_LOGINPWD_SME;
			
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["ModuleID"]+ "'";
			conn.ExecuteQuery();
			DB_NAMA = conn.GetFieldValue("DB_NAMA");
			DB_IP	= conn.GetFieldValue("DB_IP");
			DB_LOGINID	= conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conn.ClearData();
			this.LBL_DB_LOGINID.Text	= DB_LOGINID;
			conn2 = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void FillDDL()
		{
			string strBy = "FULLNAME";
			/*if (Request.QueryString["moduleID"] == "01")
				strBy = "SU_FULLNAME";
			else
				strBy = "SC_NAME";
				*/

			GlobalTools.initDateForm(TXT_AU_DATE_TO_D,DDL_AU_DATE_TO_M,TXT_AU_DATE_TO_Y);
			GlobalTools.initDateForm(TXT_AU_DATE_FROM_D,DDL_AU_DATE_FROM_M,TXT_AU_DATE_FROM_Y);
			/*
			GlobalTools.fillRefList(this.DDL_PARAMCLASS,"select * from VW_AUDIT_PARAMCLASS order by CLASSNAME",conn);
			GlobalTools.fillRefList(this.DDL_AU_BY,"select * from VW_AUDIT_BY order by " + strBy,conn2);
			GlobalTools.fillRefList(this.DDL_PARAMGROUP,"select distinct PG_ID,PG_NAME from VW_AUDIT_PARAMGROUP where MODULEID = '*' order by PG_NAME",conn);
			GlobalTools.fillRefList(this.DDL_AU_PARAMNAME,"select PARAMID,PARAMNAME from VW_AUDIT_DDL_PARAMNAME where moduleid='*' and CLASSID='' and PG_ID ='*' order by PARAMNAME",conn);	
			*/
			GlobalTools.fillRefList(this.DDL_PARAMCLASS,"select * from VW_AUDIT_PARAMCLASS order by CLASSNAME",connsme);
			GlobalTools.fillRefList(this.DDL_AU_BY,"select * from VW_AUDIT_BY order by " + strBy ,conn);
			GlobalTools.fillRefList(this.DDL_PARAMGROUP,"select distinct PG_ID,PG_NAME from VW_AUDIT_PARAMGROUP where MODULEID = '" + Request.QueryString["moduleID"] + "' order by PG_NAME",connsme);
			GlobalTools.fillRefList(this.DDL_AU_PARAMNAME,"select PARAMID,PARAMNAME from VW_AUDIT_DDL_PARAMNAME where moduleid='*' and CLASSID='' and PG_ID ='*' order by PARAMNAME",connsme);	
		}

		protected void DDL_PARAMCLASS_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.LBL_PARAMCLASS.Text	= DDL_PARAMCLASS.SelectedValue;
			LBL_PARAMGROUP.Text			= "";
			if (LBL_PARAMCLASS.Text == "00")
			{
				this.DDL_PARAMGROUP.Enabled		= true;
				this.DDL_AU_PARAMNAME.Enabled	= false;
				this.DDL_AU_PARAMNAME.SelectedValue	= "";
			} 
			else if (LBL_PARAMCLASS.Text == "01" || LBL_PARAMCLASS.Text == "02" )
			{
				this.DDL_AU_PARAMNAME.Enabled	= true;
				this.DDL_PARAMGROUP.Enabled		= false;
				GlobalTools.fillRefList(this.DDL_AU_PARAMNAME,"select DISTINCT PARAMID,PARAMNAME from VW_AUDIT_DDL_PARAMNAME where CLASSID = '" + this.LBL_PARAMCLASS.Text.Trim()+ "' order by PARAMNAME",connsme);	
			} 
			else
			{
				this.DDL_AU_PARAMNAME.Enabled	= false;
				this.DDL_PARAMGROUP.Enabled		= false;
			}
			GlobalTools.fillRefList(DDL_PARAMGROUP,"select distinct PG_ID,PG_NAME from VW_AUDIT_PARAMGROUP where MODULEID = '" + Request.QueryString["ModuleID"] + "' order by PG_NAME",connsme);
			
		}

		protected void DDL_PARAMGROUP_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.DDL_AU_PARAMNAME.Enabled	= true;
			LBL_PARAMGROUP.Text = this.DDL_PARAMGROUP.SelectedValue;
			GlobalTools.fillRefList(this.DDL_AU_PARAMNAME,"select PARAMID,PARAMNAME from VW_AUDIT_DDL_PARAMNAME where moduleid='" + Request.QueryString["ModuleID"] + "' and CLASSID='' and PG_ID ='" + this.LBL_PARAMGROUP.Text.Trim() + "' order by PARAMNAME",connsme);	
		}

		private void ClearBoxes()
		{
			this.TXT_AU_DATE_FROM_D.Text	= "";
			this.TXT_AU_DATE_FROM_Y.Text	= "";
			this.TXT_AU_DATE_TO_D.Text		= "";
			this.TXT_AU_DATE_TO_Y.Text		= "";
			this.DDL_AU_DATE_FROM_M.SelectedValue	= "";
			this.DDL_AU_DATE_TO_M.SelectedValue		= "";
			this.DDL_AU_BY.SelectedValue			= "";
			this.DDL_AU_PARAMNAME.SelectedValue		= "";
			this.LBL_PARAMCLASS.Text	= "";
			this.LBL_PARAMGROUP.Text	= "";
			this.DDL_PARAMCLASS.SelectedValue		= "";
			this.DDL_PARAMGROUP.SelectedValue		= "";
			this.Label1.Text						= "";
		}

		private void CreateSQLText()
		{
			string sql;
			string by,paramname,paramclass,cls,grp;
			sql = "";queryAll = "";	paramclass	= "";

			paramname   = this.DDL_AU_PARAMNAME.SelectedValue;
			by			= this.DDL_AU_BY.SelectedValue;
			cls			= this.LBL_PARAMCLASS.Text.Trim();
			grp			= this.LBL_PARAMGROUP.Text.Trim();

			if (cls == "01" || cls == "02")
				paramclass = cls;
			else if (this.LBL_PARAMCLASS.Text.Trim() == "00")
				paramclass = grp;
			//create sql teks...
			if (paramname!= "")
				sql = " AND AU_PARAMNAME = '" + paramname + "' ";
			if (cls == "00" && paramclass == "" && paramname == "")
				sql = " AND (AU_CLASS != '01' and AU_CLASS != '02')";
			else if (paramclass != "")
				sql += " AND AU_CLASS = '" + paramclass + "' ";
			
			if (by != "")
				sql += " and AU_BY = '" + by + "' ";
			if ((this.TXT_AU_DATE_TO_D.Text != "" && this.DDL_AU_DATE_TO_M.SelectedValue != "" &&  this.TXT_AU_DATE_TO_Y.Text != "" ) && (this.TXT_AU_DATE_FROM_D.Text != "" && this.DDL_AU_DATE_FROM_M.SelectedValue != "" & this.TXT_AU_DATE_FROM_Y.Text != "" ))
				sql += " AND convert(varchar,AU_DATE,112) between "+TXT_AU_DATE_FROM_Y.Text+int.Parse(DDL_AU_DATE_FROM_M.SelectedValue).ToString("00")+int.Parse(TXT_AU_DATE_FROM_D.Text).ToString("00")+" and "+TXT_AU_DATE_TO_Y.Text+int.Parse(DDL_AU_DATE_TO_M.SelectedValue).ToString("00")+int.Parse(TXT_AU_DATE_TO_D.Text).ToString("00");
			/***
			if (this.TXT_AU_DATE_TO_D.Text != "" && this.DDL_AU_DATE_TO_M.SelectedValue != "" &&  this.TXT_AU_DATE_TO_Y.Text != "" )
				sql += " AND AU_DATE < "+GlobalTools.ToSQLDate(TXT_AU_DATE_TO_D.Text,DDL_AU_DATE_TO_M.SelectedValue,TXT_AU_DATE_TO_Y.Text,"00","00") + " ";
			if (this.TXT_AU_DATE_FROM_D.Text != "" && this.DDL_AU_DATE_FROM_M.SelectedValue != "" & this.TXT_AU_DATE_FROM_Y.Text != "" )
				sql += " AND AU_DATE > "+GlobalTools.ToSQLDate(TXT_AU_DATE_FROM_D.Text,DDL_AU_DATE_FROM_M.SelectedValue,TXT_AU_DATE_FROM_Y.Text,"23","59") + " ";
			**/

			queryAll = "exec AUDITTRAIL_PARAM '" + this.LBL_DB_IP_MNT.Text.Trim() + "','" + this.LBL_DB_NAMA_MNT.Text + "','" +
				this.LBL_DB_LOGINID.Text.Trim() + "','" + this.LBL_DB_IP_MNT.Text.Trim() + "','" +
				this.LBL_DB_LOGINID_MNT.Text.Trim() + "','" + this.LBL_DB_LOGINPWD_MNT.Text.Trim() + "','"
				+ sql +" '";
			// Label1.Text	= queryAll;   // butuh where cluse aja
			Label1.Text = sql;			
		}

		private void ViewData()
		{
			CreateSQLText();
			//conn2.QueryString = Label1.Text;
			conn2.QueryString = queryAll;
			conn2.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			DGR_LIST.DataSource = dt;
			try
			{
				this.DGR_LIST.DataBind();
			}
			catch 
			{
				DGR_LIST.CurrentPageIndex = DGR_LIST.PageCount - 1;
				DGR_LIST.DataBind();
			}
			int No = (DGR_LIST.CurrentPageIndex * 10) + 1;
			for (int i=0; i< this.DGR_LIST.Items.Count; i ++)
			{
				/// format it at Design
				//this.DGR_LIST.Items[i].Cells[6].Text = GlobalTools.FormatDate(this.DGR_LIST.Items[i].Cells[6].Text);
				this.DGR_LIST.Items[i].Cells[0].Text = No.ToString();
				No++;
			}
		}

		private void CekValidDate()
		{
			try
			{
				string date_to = GlobalTools.ToSQLDate(TXT_AU_DATE_TO_D.Text,DDL_AU_DATE_TO_M.SelectedValue,TXT_AU_DATE_TO_Y.Text);
			} 
			catch 
			{
				GlobalTools.popMessage(this,"Date is not valid!");
				GlobalTools.SetFocus(this,this.TXT_AU_DATE_TO_D);
				return;
			}

			try
			{
				string date_from = GlobalTools.ToSQLDate(TXT_AU_DATE_FROM_D.Text,DDL_AU_DATE_FROM_M.SelectedValue,TXT_AU_DATE_FROM_Y.Text);
			} 
			catch 
			{
				GlobalTools.popMessage(this,"Date is not valid!");
				GlobalTools.SetFocus(this,this.TXT_AU_DATE_FROM_D);
				return;
			}
		}

		protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
		{
			ClearBoxes();
			this.DGR_LIST.Visible = false;
		}

		protected void BTN_SEARCH_Click(object sender, System.EventArgs e)
		{
			if (this.TXT_AU_DATE_TO_D.Text != "" || this.DDL_AU_DATE_TO_M.SelectedValue != "" ||  this.TXT_AU_DATE_TO_Y.Text != "" )
				if (!GlobalTools.isDateValid(TXT_AU_DATE_TO_D.Text,DDL_AU_DATE_TO_M.SelectedValue,TXT_AU_DATE_TO_Y.Text))
				{
					GlobalTools.popMessage(this,"Date is not valid!");
					GlobalTools.SetFocus(this,TXT_AU_DATE_TO_D);
					this.DGR_LIST.Visible = false;
					return;
				}	
			if (this.TXT_AU_DATE_FROM_D.Text != "" || this.DDL_AU_DATE_FROM_M.SelectedValue != "" || this.TXT_AU_DATE_FROM_Y.Text != "" )
				if (!GlobalTools.isDateValid(TXT_AU_DATE_FROM_D.Text,DDL_AU_DATE_FROM_M.SelectedValue,TXT_AU_DATE_FROM_Y.Text))
				{
					GlobalTools.popMessage(this,"Date is not valid!");
					GlobalTools.SetFocus(this,TXT_AU_DATE_FROM_D);
					this.DGR_LIST.Visible = false;
					return;
				}
			if (GlobalTools.compareDate(TXT_AU_DATE_TO_D.Text, DDL_AU_DATE_TO_M.SelectedValue, TXT_AU_DATE_TO_Y.Text, TXT_AU_DATE_FROM_D.Text, DDL_AU_DATE_FROM_M.SelectedValue, TXT_AU_DATE_FROM_Y.Text)< 0)
				{
					GlobalTools.popMessage(this,"Date1 must be less than Date2");
					GlobalTools.SetFocus(this,TXT_AU_DATE_FROM_D);
					this.DGR_LIST.Visible = false;
					return;
				}

			/**
			this.DGR_LIST.Visible			= true;
			this.DGR_LIST.CurrentPageIndex	= 0;
			ViewData();
			**/

			string sqlQuery, tanggal1, tanggal2, 
				paramclass, paramname, paramgroup, 
				modifyby, modulename;

			/// get query
			/// 
			CreateSQLText();
			sqlQuery = Label1.Text;

			if (DDL_AU_DATE_FROM_M.SelectedValue == "") tanggal1 = "";
			else tanggal1 = TXT_AU_DATE_FROM_D.Text.Trim() + "-" + DDL_AU_DATE_FROM_M.SelectedItem.Text + "-" + TXT_AU_DATE_FROM_Y.Text;
			if (DDL_AU_DATE_TO_M.SelectedValue == "") tanggal2="";
			else tanggal2 = TXT_AU_DATE_TO_D.Text + "-" + DDL_AU_DATE_TO_M.SelectedItem.Text + "-" + TXT_AU_DATE_TO_Y.Text;

			if (DDL_PARAMCLASS.SelectedValue == "")	paramclass ="";
			else paramclass = DDL_PARAMCLASS.SelectedItem.Text.Replace("'","");
			if (DDL_AU_PARAMNAME.SelectedValue =="") paramname ="";
			else paramname = DDL_AU_PARAMNAME.SelectedItem.Text.Replace("'","");
			if (DDL_PARAMGROUP.SelectedValue=="") paramgroup ="";
			else paramgroup = DDL_PARAMGROUP.SelectedItem.Text.Replace("'","");
			if (DDL_AU_BY.SelectedValue=="") modifyby = "";
			else modifyby = DDL_AU_BY.SelectedItem.Text.Replace("'","");		
			modulename = LBL_MODULENAME.Text.Replace("'","");	


			//// get CuBES Maintenance information module
			///

			conn.QueryString="select DB_IP, DB_IP DB_RMTNAMA,DB_NAMA,DB_LOGINID DB_RMTLOGINID,"+
							" DB_LOGINPWD DB_RMTLOGINPWD from VW_GETCONN where MODULEID='99'";
			conn.ExecuteQuery();

			string DB_IP = conn.GetFieldValue("DB_IP");
			string DB_RMTNAMA = conn.GetFieldValue("DB_RMTNAMA");
			string DB_NAMA = conn.GetFieldValue("DB_NAMA");
			string DB_RMTLOGINID = conn.GetFieldValue("DB_RMTLOGINID");
			string DB_RMTLOGINPWD = conn.GetFieldValue("DB_RMTLOGINPWD");


			conn.QueryString=" select DB_LOGINID,DB_IP ,DB_NAMA	from VW_GETCONN where MODULEID = '" + Request.QueryString["moduleid"] + "' ";
			conn.ExecuteQuery();
			string DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			string DB_IP_MOD = conn.GetFieldValue("DB_IP");
			string DB_NAMA_MOD = conn.GetFieldValue("DB_NAMA");

            /*
			string Report_IP="";
			conn.QueryString = "select report_ip from rfmodule where moduleid= '" + Request.QueryString["moduleid"] + "'";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				Report_IP = conn.GetFieldValue(0,"REPORT_IP").ToString();
			}
			else
			{
				Report_IP = "10.123.13.18";		// default value if not set
			}
			
			/// parameter di report : DB_IP, DB_NAMA,DB_LOGINID,DB_RMTNAMA,DB_RMTLOGINID,DB_RMTLOGINPWD,STR,Date1,Date2,ParamClass,ParamGroup,ParamName,ModifBy
			/// 
			ReportViewer1.ServerUrl = "http://" + Report_IP.ToString() + "/ReportServer";  
			ReportViewer1.ReportPath = "/CuBES Reports/RptMntParamAudit"+Request.QueryString["moduleid"]+"&STR="+sqlQuery+
				"&Date1="+tanggal1+"&Date2="+tanggal2+"&DB_IP="+DB_IP+"&DB_NAMA="+DB_NAMA+"&DB_LOGINID="+DB_LOGINID+
				"&DB_RMTNAMA="+DB_RMTNAMA+"&DB_RMTLOGINID="+DB_RMTLOGINID+"&DB_RMTLOGINPWD="+DB_RMTLOGINPWD+
				"&ParamClass="+paramclass+"&ParamName="+paramname+"&ParamGroup="+paramgroup+
				"&modulename="+modulename+"&ModifBy="+modifyby;
            */

            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportUser"].ToString(), WebConfigurationManager.AppSettings["ReportPassword"].ToString(), WebConfigurationManager.AppSettings["DomainName"].ToString());

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(WebConfigurationManager.AppSettings["ServerUrl"].ToString());
            ReportViewer1.ServerReport.ReportPath = "/CuBES Reports/RptMntParamAudit" + Request.QueryString["moduleid"];

            List<ReportParameter> paramList = new List<ReportParameter>();

            paramList.Add(new ReportParameter("STR", sqlQuery, false));
            paramList.Add(new ReportParameter("Date1", tanggal1, false));
            paramList.Add(new ReportParameter("Date2", tanggal2, false));
            paramList.Add(new ReportParameter("DB_IP", DB_IP, false));
            paramList.Add(new ReportParameter("DB_NAMA", DB_NAMA, false));
            paramList.Add(new ReportParameter("DB_LOGINID", DB_LOGINID, false));
            paramList.Add(new ReportParameter("DB_RMTNAMA", DB_RMTNAMA, false));
            paramList.Add(new ReportParameter("DB_RMTLOGINID", DB_RMTLOGINID, false));
            paramList.Add(new ReportParameter("DB_RMTLOGINPWD", DB_RMTLOGINPWD, false));
            paramList.Add(new ReportParameter("ParamClass", paramclass, false));
            paramList.Add(new ReportParameter("ParamName", paramname, false));
            paramList.Add(new ReportParameter("ParamGroup", paramgroup, false));
            //paramList.Add(new ReportParameter("modulename", modulename, false));
            paramList.Add(new ReportParameter("ModifBy", modifyby, false));

            ReportViewer1.ServerReport.SetParameters(paramList);
            ReportViewer1.ServerReport.Refresh();
        }

		private void DGR_LIST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_LIST.CurrentPageIndex = e.NewPageIndex;			
			ViewData();
		}

	}
}
