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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for DetailMitrakaryaComp.
	/// </summary>
	public partial class DetailMitrakaryaComp : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;
		protected string code, bcode, mkicode, status;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			if(!IsPostBack)
			{
				ViewData(); 
			}
			else
			{
				InitialCon();
			}
		}

		private void ViewData()
		{
			conn2.QueryString = "select * from rfmodule where moduleid = '40'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			InitialCon();

			ViewDetail();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void ViewDetail()
		{
			code = Request.QueryString["mtkcode"];
			status = Request.QueryString["sta"];

			if(status == "1")
			{
				conn.QueryString = "select * from VW_PARAM_RFMITRAKARYA_INDUK where MTK_CODE = '"+code+"'";			
			}
			else
			{
				conn.QueryString = "select * from VW_PARAM_RFMITRAKARYA_INDUK_APPR where MTK_CODE = '"+code+"'";			
			}
				
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0)
			{
				LBL_COMP_CODE.Text = conn.GetFieldValue(0,"MTK_SRCCODE"); 
				LBL_COMP_NAME.Text = conn.GetFieldValue(0,"MTK_DESC");
				LBL_SUB_IN.Text  = conn.GetFieldValue(0,"MTK_SUBINTEREST") + " %";
				LBL_GLINE.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"MTK_GRLINE"));
				LBL_REAL.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"MTK_PLAFOND"));
				LBL_REMAIN.Text =  "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"SISA"));
				LBL_POT_GAJI.Text = conn.GetFieldValue(0,"MKT_POTGAJI") + " %";
				LBL_LIMIT.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"MTK_INDLIMIT"));
				LBL_TENOR.Text = conn.GetFieldValue(0,"MTK_INDTENOR") + " bulan";
				LBL_BLOCK.Text = conn.GetFieldValue(0,"BLOCK");
				LBL_THT.Text = conn.GetFieldValue(0,"COVER");
				LBL_EXPDATE.Text = conn.GetFieldValue(0,"MTK_EXPIREDATE");
				LBL_COMP_RATE.Text = conn.GetFieldValue(0,"RT_DESC");
				LBL_PES1.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"MKT_PESENAM"));
				LBL_PES2.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"MKT_PESATENAM"));

				bcode = conn.GetFieldValue(0,"BRANCH_CODE");
				mkicode = conn.GetFieldValue(0,"MKI_CODE");
			}

			conn.ClearData(); 


			if(bcode != "" || bcode != null)
			{
				conn.QueryString = "select BRANCH_NAME from RFBRANCH where BRANCH_CODE = '"+bcode+"'";
				conn.ExecuteQuery(); 

				if(conn.GetRowCount() != 0)
				{
					LBL_BRANCH.Text = conn.GetFieldValue(0,0);  
				}

				conn.ClearData(); 
			}
			else
			{
				LBL_BRANCH.Text = "&nbsp;";

			}

			if(mkicode != "" || mkicode != null)
			{
				conn.QueryString = "select MKI_DESC from RFMITRAKARYA_INDUK where MKI_CODE = '"+mkicode+"'";
				conn.ExecuteQuery();

				if(conn.GetRowCount() != 0)
				{
					LBL_COMP_INDUK.Text =  conn.GetFieldValue(0,0);  
				}

				conn.ClearData(); 
			}
			else
			{
				LBL_COMP_INDUK.Text = "&nbsp;"; 

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

		protected void BTN_CLOSE_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>window.close();</script>");		
		}
	}
}
