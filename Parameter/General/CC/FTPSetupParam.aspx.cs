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

namespace CuBES_Maintenance.Parameter.General.CC
{
	/// <summary>
	/// Summary description for FTPSetupParam.
	/// </summary>
	public partial class FTPSetupParam : System.Web.UI.Page
	{
		//protected Connection conn = new Connection (System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn2,conn;
		string scdl,mtnt,bi,cif,serv;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				ViewExistingData();
			}
			ViewPendingData();
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
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

		private void ClearBoxes()
		{
			this.TXT_CIF_SHEC_YY.Text		= "";
			this.TXT_F_BI_YY.Text			= "";
			this.TXT_F_HOURS.Text			= "";
			this.TXT_F_IP.Text				= "";
			this.TXT_F_MTNT_YY.Text			= "";
			this.TXT_F_PASS.Text			= "";
			this.TXT_F_SCDL_YY.Text			= "";
			this.TXT_F_USER.Text			= "";
			this.TXT_FBI_DIR.Text			= "";
			this.TXT_FBI_IP.Text			= "";
			this.TXT_FBI_PASS.Text			= "";
			this.TXT_FBI_USER.Text			= "";
			this.TXT_SERV_SHEC_YY.Text		= "";
		}

		private void ViewExistingData()
		{
			scdl=mtnt=bi=cif=serv="";
			conn2.QueryString = "select * from RFFTP";
			conn2.ExecuteQuery();
			this.LBL_F_IP.Text				= conn2.GetFieldValue("F_IP");
			this.LBL_F_USER.Text			= conn2.GetFieldValue("F_USER");
			this.LBL_F_PASS.Text			= conn2.GetFieldValue("F_PASS");
			this.LBL_F_SCDL.Text			= GlobalTools.FormatDate_GetTime(conn2.GetFieldValue("F_SCDL"));
			this.LBL_F_MTNT.Text			= GlobalTools.FormatDate_GetTime(conn2.GetFieldValue("F_MTNT"));
			this.LBL_F_HOURS.Text			= conn2.GetFieldValue("F_HOURS");
			this.LBL_F_BI.Text				= GlobalTools.FormatDate_GetTime(conn2.GetFieldValue("F_BI"));
			this.LBL_FBI_IP.Text			= conn2.GetFieldValue("FBI_IP");
			this.LBL_FBI_PASS.Text			= conn2.GetFieldValue("FBI_PASS");
			this.LBL_FBI_USER.Text			= conn2.GetFieldValue("FBI_USER");
			this.LBL_FBI_DIR.Text			= conn2.GetFieldValue("FBI_DIR");
			this.LBL_CIF_SHEC.Text			= GlobalTools.FormatDate_GetTime(conn2.GetFieldValue("CIF_SHEC"));			
			this.LBL_SERV_SHEC.Text			= GlobalTools.FormatDate_GetTime(conn2.GetFieldValue("SERV_SHEC"));
		}

		private void ViewPendingData()
		{
			conn2.QueryString = "select * from PENDING_CC_RFFTP";
			conn2.ExecuteQuery();
			if (conn2.GetRowCount() == 0)
				this.PNP_REQUEST.Visible = false;
			else
				this.PNP_REQUEST.Visible = true;
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
			Response.Redirect("../../GeneralParamAll.aspx?ModuleID=" + Request.QueryString["ModuleID"]);
		}

		protected void BTN_EDIT_Click(object sender, System.EventArgs e)
		{
			this.LBL_SAVEMODE.Text = "0";
			this.PNL_SETUP.Visible = true;

			this.TXT_F_IP.Text					= this.LBL_F_IP.Text;
			this.TXT_F_USER.Text				= this.LBL_F_USER.Text;
			this.TXT_F_PASS.Text				= this.LBL_F_PASS.Text;
			this.TXT_F_SCDL_YY.Text				= this.LBL_F_SCDL.Text;	
			this.TXT_F_MTNT_YY.Text				= this.LBL_F_MTNT.Text;
			this.TXT_F_HOURS.Text				= this.LBL_F_HOURS.Text;
			this.TXT_F_BI_YY.Text				= this.LBL_F_BI.Text;
			this.TXT_FBI_IP.Text				= this.LBL_FBI_IP.Text;
			this.TXT_FBI_PASS.Text				= this.LBL_FBI_PASS.Text;
			this.TXT_FBI_USER.Text				= this.LBL_FBI_USER.Text;
			this.TXT_FBI_DIR.Text				= this.LBL_FBI_DIR.Text;
			this.TXT_CIF_SHEC_YY.Text			= this.LBL_CIF_SHEC.Text;
			this.TXT_SERV_SHEC_YY.Text			= this.LBL_SERV_SHEC.Text;
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearBoxes();
		}
		private bool TimeValidation(string tm)
		{
			bool retval = false;
			string hrStr,minStr = "";
			int hrInt,minInt = 0;
			char[] cr = new char[]{':'};
			string [] temp = new string[4];
			int n=0;
			foreach (string splitstr in tm.Split(cr))
			{
				temp[n] = splitstr;
				n++;
			}
			hrStr = temp[0];minStr = temp[1];
			try
			{
				hrInt = Convert.ToInt16(hrStr);
				minInt = Convert.ToInt16(minStr);
			
				if (( 23 < hrInt ||hrInt < 0) || (59 < minInt || minInt < 0 ))
					retval = false;
				else
					retval = true;
			} 
			catch{}
			return retval;
		}

		private void ErrorMessage(TextBox txt)
		{
			GlobalTools.popMessage(this, "Time is not valid!");
			GlobalTools.SetFocus(this,txt);
			return;
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (TimeValidation(this.TXT_F_SCDL_YY.Text) == false)
			{
				GlobalTools.popMessage(this, "Time is not valid!");
				GlobalTools.SetFocus(this,TXT_F_SCDL_YY);
				return;
			}
			if (TimeValidation(this.TXT_F_MTNT_YY.Text) == false)
			{
				GlobalTools.popMessage(this, "Time is not valid!");
				GlobalTools.SetFocus(this,TXT_F_MTNT_YY);
				return;
			}
			if (TimeValidation(this.TXT_F_BI_YY.Text ) == false)
			{
				GlobalTools.popMessage(this, "Time is not valid!");
				GlobalTools.SetFocus(this,TXT_F_BI_YY);
				return;
			}
			if (TimeValidation(this.TXT_CIF_SHEC_YY.Text) == false)
			{
				GlobalTools.popMessage(this, "Time is not valid!");
				GlobalTools.SetFocus(this,TXT_CIF_SHEC_YY);
				return;
			}
			if (TimeValidation(this.TXT_SERV_SHEC_YY.Text) == false)
			{
				GlobalTools.popMessage(this, "Time is not valid!");
				GlobalTools.SetFocus(this,TXT_SERV_SHEC_YY);
				return;
			}
			scdl = "2000-01-01 " + this.TXT_F_SCDL_YY.Text + ":00.000";
			mtnt = "2000-01-01 " + this.TXT_F_MTNT_YY.Text + ":00.000";
			bi   = "2000-01-01 " + this.TXT_F_BI_YY.Text + ":00.000";
			cif	 = "2000-01-01 " + this.TXT_CIF_SHEC_YY.Text + ":00.000";
			serv = "2000-01-01 " + this.TXT_SERV_SHEC_YY.Text + ":00.000";
			

			conn2.QueryString = "exec PARAM_GENERAL_CC_RFFTP_MAKER '" + this.LBL_SAVEMODE.Text + "','" +
				this.TXT_F_IP.Text +"','"+ this.TXT_F_USER.Text +"','"+ this.TXT_F_PASS.Text + "','" +
				scdl + "','" + mtnt + "','" + this.TXT_F_HOURS.Text+"','"+ bi +"','"+
				this.TXT_FBI_IP.Text + "','" + this.TXT_FBI_USER.Text +"','" + this.TXT_FBI_PASS.Text + "','"+
				this.TXT_FBI_DIR.Text +"','"+ cif + "','" + serv + "'";
			conn2.ExecuteQuery();
			this.LBL_SAVEMODE.Text = "1";
			this.PNL_SETUP.Visible = false;
			ViewPendingData();
			
		}
	}
}
