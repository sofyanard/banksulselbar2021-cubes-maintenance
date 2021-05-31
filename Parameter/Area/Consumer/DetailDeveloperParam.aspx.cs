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

namespace CuBES_Maintenance.Parameter.Area.Consumer
{
	/// <summary>
	/// Summary description for DetailDeveloperParam.
	/// </summary>
	public partial class DetailDeveloperParam : System.Web.UI.Page
	{
		protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn;		
		protected string mid, code, cid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
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
			code = Request.QueryString["dvcode"];

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

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void ViewDetail()
		{
			conn.QueryString = "select DV_NAME from DEVELOPER where DV_CODE = '"+code+"'";			
			conn.ExecuteQuery();
			
			if(conn.GetRowCount() != 0)
			{
				LBL_DEV_NAME.Text = conn.GetFieldValue(0,"DV_NAME");
			}
			else
			{
				LBL_DEV_NAME.Text = "&nbsp;";
			}

			conn.ClearData(); 
		}

		private void BindData()
		{
			cid = Request.QueryString["cityid"];

			conn.QueryString = "select PROYEK_ID h0, PROYEK_DESCRIPTION h1, LK.LOKASI h2, "+ 
				"CT.CITY_NAME h3, isnull(PH_GRLINE, 0) h4, isnull(PH_PLAFOND, 0) h5, "+
				"PH_SRCCODE h6, RUMAH_SAKIT h7, SEKOLAH h8, PUSAT_BELANJA h9, DANAU h10, TAMAN h11, "+
				"OLAH_RAGA h12, REMARK h13 from PROYEK_HOUSINGLOAN PH "+ 
				"join LOKASI_PERUMAHAN LK on PH.ID_LOKASI = LK.ID_LOKASI and PH.ID_KOTA = LK.ID_KOTA "+ 
				"join CITY CT on CT.CITY_ID = PH.ID_KOTA "+
				"where DEVELOPER_ID = '"+code+"' and PH.ID_LOKASI = '"+cid+"'";
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
