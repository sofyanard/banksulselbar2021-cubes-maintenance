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
	/// Summary description for DealerIndukParamDetail.
	/// </summary>
	public partial class DealerIndukParamDetail : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetDBConn2();
			
			if(!IsPostBack)
			{
				dtDetail();
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
			this.DGExisting.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGExisting_PageIndexChanged);

		}
		#endregion
		
		private void SetDBConn2()
		{
			conn.QueryString = "select * from RFMODULE where MODULEID=40";
			conn.ExecuteQuery();

			conn2 = new Connection ("Data Source=" + conn.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn.GetFieldValue("DB_NAMA") + ";uid=" +conn.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}
		
		private void dtDetail()
		{
			conn2.QueryString = "select * from dealer where dli_code = '"+Request.QueryString["dli_code"]+"'";
			conn2.ExecuteQuery();
			
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			DGExisting.DataSource = dt;
			
			try 
			{
				DGExisting.DataBind();
			}
			catch 
			{
				DGExisting.CurrentPageIndex = DGExisting.PageCount - 1;
				DGExisting.DataBind();
			}
		}

		protected void BTN_PRINT_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language=JavaScript>window.print();</script>");
		}

		protected void BTN_CLOSE_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language=JavaScript>window.close();</script>");
		}

		private void DGExisting_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGExisting.CurrentPageIndex = e.NewPageIndex;

			dtDetail(); 
		}
	}
}
