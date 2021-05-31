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
	/// Summary description for DetailMitrakaryaInduk.
	/// </summary>
	public partial class DetailMitrakaryaInduk : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;		
		protected string mid, code;
	
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
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			InitialCon();

			ViewDetail();
			BindData();
		}

		private void ViewDetail()
		{
			code = Request.QueryString["mkicode"];

			//changed by sofyan 2007-05-28, LOS Consumer Enh 4
			conn.QueryString = "select MKI_DESC, MKI_SRCCODE, " +
				"convert(varchar,MKI_EXPIREDATE,103) MKI_EXPIREDATE, RT_DESC " + 
				"from RFMITRAKARYA_INDUK join RFRATING on RFMITRAKARYA_INDUK.RT_CODE = RFRATING.RT_CODE " + 
				"where MKI_CODE = '"+code+"'";
			conn.ExecuteQuery();
			
			if(conn.GetRowCount() != 0)
			{
				LBL_MKI_CODE.Text = code;
				LBL_MKI_SRCCODE.Text = conn.GetFieldValue(0,1);
				LBL_COMP_NAME.Text = conn.GetFieldValue(0,0);
				LBL_EXP_DATE.Text = conn.GetFieldValue(0,2);
				LBL_RT_CODE.Text = conn.GetFieldValue(0,3);
			}
			else
			{
				LBL_MKI_CODE.Text = "&nbsp;";
				LBL_MKI_SRCCODE.Text = "&nbsp;";
				LBL_COMP_NAME.Text = "&nbsp;";
				LBL_EXP_DATE.Text = "&nbsp;";
				LBL_RT_CODE.Text = "&nbsp;";
			}

			conn.ClearData(); 
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData()
		{
			conn.QueryString = "select * from VW_PARAM_RFMITRAKARYA_INDUK where MKI_CODE = '"+code+"' order by MTK_CODE";			
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_DETAIL.DataSource = dt;

			try
			{
				DGR_DETAIL.DataBind();
			}
			catch 
			{
				DGR_DETAIL.CurrentPageIndex = DGR_DETAIL.PageCount - 1;
				DGR_DETAIL.DataBind();
			} 

			conn.ClearData();
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

		protected void BTN_PRINT_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>window.print();</script>");
		}

		protected void BTN_CLOSE_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>window.close();</script>");
		}
	}
}
