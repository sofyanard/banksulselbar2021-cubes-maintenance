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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for Nota_Analisa.
	/// </summary>
	public partial class Nota_Analisa : System.Web.UI.Page
	{

		private Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if (!IsPostBack) 
			{
				LBL_SAVEMODE.Text = "1";
				
				fillDropDownList();
				bindDataCurrent();
				bindDataPending();
			}
		}

		private void fillDropDownList() 
		{
			/// program
			/// 
			GlobalTools.fillRefList(DDL_PROGRAMID,"SELECT PROGRAMID,PROGRAMDESC FROM RFPROGRAM WHERE AREAID = '0000' AND ACTIVE = '1'", false, conn);			

			/// business unit
			/// 
			GlobalTools.fillRefList(DDL_B_UNIT, "select bussunit,bussunitdesc from rfbusinessunit where active ='1'", false, conn);
		}

		private void bindDataCurrent() 
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_NOTA_ANALISA";
			conn.ExecuteQuery();

			DGR_CURRENT.DataSource = conn.GetDataTable().DefaultView;
			try 
			{ 
				DGR_CURRENT.DataBind(); 
			} 
			catch 
			{
				DGR_CURRENT.CurrentPageIndex = 0;
				DGR_CURRENT.DataBind();
			}
		}

		private void bindDataPending() 
		{
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
	}
}
