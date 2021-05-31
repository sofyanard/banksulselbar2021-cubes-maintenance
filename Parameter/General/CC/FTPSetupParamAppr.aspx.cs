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

namespace CuBES_Maintenance.Parameter.General.CC
{
	/// <summary>
	/// Summary description for FTPSetupParamAppr.
	/// </summary>
	public partial class FTPSetupParamAppr : System.Web.UI.Page
	{
		protected Connection conn2,conn;
		protected Connection connsme = new Connection (System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				ViewPendingData();
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

		private void SetDBConn2()
		{
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			string DB_NAMA = conn.GetFieldValue("DB_NAMA");
			string DB_IP = conn.GetFieldValue("DB_IP");
			string DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conn2 = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void ViewPendingData()
		{
			conn2.QueryString = "select * from PENDING_CC_RFFTP";
			conn2.ExecuteQuery();
			this.LBL_REQ_F_IP.Text				= conn2.GetFieldValue("F_IP");
			this.LBL_REQ_F_USER.Text			= conn2.GetFieldValue("F_USER");
			this.LBL_REQ_F_PASS.Text			= conn2.GetFieldValue("F_PASS");
			this.LBL_REQ_F_SCDL.Text			= GlobalTools.FormatDate_GetTime(conn2.GetFieldValue("F_SCDL"));
			this.LBL_REQ_F_MTNT.Text			= GlobalTools.FormatDate_GetTime(conn2.GetFieldValue("F_MTNT"));
			this.LBL_REQ_F_HOURS.Text			= conn2.GetFieldValue("F_HOURS");
			this.LBL_REQ_F_BI.Text				= GlobalTools.FormatDate_GetTime(conn2.GetFieldValue("F_BI"));
			this.LBL_REQ_FBI_IP.Text			= conn2.GetFieldValue("FBI_IP");
			this.LBL_REQ_FBI_PASS.Text			= conn2.GetFieldValue("FBI_PASS");
			this.LBL_REQ_FBI_USER.Text			= conn2.GetFieldValue("FBI_USER");
			this.LBL_REQ_FBI_DIR.Text			= conn2.GetFieldValue("FBI_DIR");
			this.LBL_REQ_CIF_SHEC.Text			= GlobalTools.FormatDate_GetTime(conn2.GetFieldValue("CIF_SHEC"));			
			this.LBL_REQ_SERV_SHEC.Text			= GlobalTools.FormatDate_GetTime(conn2.GetFieldValue("SERV_SHEC"));
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleID=" + Request.QueryString["ModuleID"]);
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			PerformRequest();
			this.PNL_REQUEST.Visible = false;
		}

		private void PerformRequest()
		{
			string paramid	= "22";
			connsme.QueryString = "select PARAMNAME,PG_ID from RFPARAMETERALL where paramid='" + paramid + "' and moduleid='20' and classid='' and ismaker=0";
			connsme.ExecuteQuery();
			string paramname	= connsme.GetFieldValue("PARAMNAME");
			string paramclass	= connsme.GetFieldValue("PG_ID");
			connsme.ClearData();

			string userid = Session["UserID"].ToString();
			conn2.QueryString = "exec PARAM_CC_RFFTP_APPR '" +  this.LBL_REQ_F_IP.Text.Trim() + "','" +
				userid+ "','" + paramname + "','" + paramclass + "'";
			conn2.ExecuteNonQuery();
		}
	
	}
}
