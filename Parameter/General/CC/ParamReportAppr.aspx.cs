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
	/// Summary description for ParamReportAppr.
	/// </summary>
	public partial class ParamReportAppr : System.Web.UI.Page
	{
		protected Connection connsme = new Connection (System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		protected Connection conn,conn2;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				ViewPendingData();
			}
			ClearComponents();
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

		public void SetDBConn2()
		{
			conn2.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn2.ExecuteQuery();
			string DB_NAMA = conn2.GetFieldValue("DB_NAMA");
			string DB_IP = conn2.GetFieldValue("DB_IP");
			string DB_LOGINID = conn2.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn2.GetFieldValue("DB_LOGINPWD");
			conn = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void ViewPendingData()
		{
			conn.QueryString = "select * from VW_PENDING_CC_INITIAL_REPORT";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				TXT_IN_FEERECEIVED1.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("IN_FEERECEIVED"));
				TXT_IN_POINTREWARD1.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("IN_POINTREWARD"));
				TXT_IN_TAX1.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("IN_TAX"));
			}
			conn.ClearData();
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			if (this.rdo_Approve.Checked == true)
			{
				AuditTrail();
				conn.QueryString = " update INITIAL set IN_FEERECEIVED = "+ GlobalTools.ConvertFloat(TXT_IN_FEERECEIVED1.Text)
					+", IN_POINTREWARD = "+ GlobalTools.ConvertFloat(TXT_IN_POINTREWARD1.Text)
					+", IN_TAX = "+ GlobalTools.ConvertFloat(TXT_IN_TAX1.Text);
				conn.ExecuteNonQuery();
				DeletePendingData();
			} 
			else if (this.rdo_Reject.Checked == true)
			{
				DeletePendingData();
			}
			ViewPendingData();
			ClearComponents();
		}

		private void AuditTrail()
		{
			//EXISTING DATA
			string IN_FEERECEIVED,IN_POINTREWARD,IN_TAX;
			string IN_FEERECEIVED_OLD,IN_POINTREWARD_OLD,IN_TAX_OLD;
			conn.QueryString = "select * from VW_INITIAL_REPORT ";
			conn.ExecuteQuery();
			IN_FEERECEIVED_OLD	= conn.GetFieldValue("IN_FEERECEIVED");
			IN_POINTREWARD_OLD	= conn.GetFieldValue("IN_POINTREWARD");
			IN_TAX_OLD			= conn.GetFieldValue("IN_TAX");
			conn.ClearData();
			//PENDING DATA
			conn.QueryString = "select * from VW_PENDING_CC_INITIAL_REPORT";
			conn.ExecuteQuery();
			IN_FEERECEIVED	= conn.GetFieldValue("IN_FEERECEIVED");
			IN_POINTREWARD	= conn.GetFieldValue("IN_POINTREWARD");
			IN_TAX			= conn.GetFieldValue("IN_TAX");
			conn.ClearData();
			if (IN_FEERECEIVED_OLD != IN_FEERECEIVED)
				ExecSPAuditTrail("IN_FEERECEIVED",IN_FEERECEIVED_OLD,IN_FEERECEIVED);
			if (IN_POINTREWARD_OLD != IN_POINTREWARD)
				ExecSPAuditTrail("IN_POINTREWARD",IN_POINTREWARD_OLD,IN_POINTREWARD);
			if (IN_TAX_OLD != IN_TAX)
				ExecSPAuditTrail("IN_TAX",IN_TAX_OLD,IN_TAX);
		}

		private void ExecSPAuditTrail(string field,string from, string to)
		{
			string userid = Session["UserID"].ToString();
			string tablename = "INITIAL";
			string paramid = "63";
			
			connsme.QueryString = "select PARAMNAME,PG_ID from RFPARAMETERALL where paramid='" + paramid + "' and moduleid='20' and classid='' and ismaker=0";
			connsme.ExecuteQuery();
			string paramname	= connsme.GetFieldValue("PARAMNAME");
			string paramclass	= connsme.GetFieldValue("PG_ID");
			connsme.ClearData();

			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + "1" + "','" + tablename + "','" +
				field + "','" + from + "','" + to + "','" + "0" + "','" + userid + "','" + 
				"" + "','" + paramclass +"','" + paramname + "'";
			conn.ExecuteNonQuery();
		}

		private void DeletePendingData()
		{
			conn.QueryString = " update PENDING_CC_INITIAL set IN_FEERECEIVED = NULL " +
				", IN_POINTREWARD = NULL " +
				", IN_TAX = NULL";
			conn.ExecuteNonQuery();
		}

		private void ClearComponents()
		{
			if (TXT_IN_FEERECEIVED1.Text == "0,00"  && TXT_IN_POINTREWARD1.Text == "0,00" && TXT_IN_TAX1.Text == "0,00" )
			{
				this.TXT_IN_FEERECEIVED1.Text	= "";
				this.TXT_IN_POINTREWARD1.Text	= "";
				this.TXT_IN_TAX1.Text			= "";
				this.rdo_Approve.Visible		= false;
				this.rdo_Pending.Visible		= false;
				this.rdo_Reject.Visible			= false;
				this.BTN_SUBMIT.Enabled			= false;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}
}
