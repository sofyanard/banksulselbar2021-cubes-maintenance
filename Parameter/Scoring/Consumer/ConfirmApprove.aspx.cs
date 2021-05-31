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

namespace CuBES_Maintenance.Parameter.Scoring.Consumer
{
	/// <summary>
	/// Summary description for ConfirmApprove.
	/// </summary>
	public partial class ConfirmApprove : System.Web.UI.Page
	{
		protected Connection conn,conn2;
		protected string mid, scoflag, rngflag, lmtflag;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				//conn.QueryString="select * from TMANDIRI_DIMENSI_ITEM where DIMID='"+Request.QueryString["DIMID"].Trim()+"'";
				//conn.ExecuteQuery();

				//if (conn.GetFieldValue("SCOFLAG") == "1")
				//	scoflag = "mandiri_scoring_model ";
				//if (conn.GetFieldValue("RNGFLAG") == "1")
				//	rngflag = ", mandiri_range_model ";
				//if (conn.GetFieldValue("RNGFLAG") == "1")
				//	lmtflag = ", mandiri_limit_model ";

				//lbl_text.Text = "All of data in table " + scoflag + rngflag + lmtflag + " will be lost..!";
			}	
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from RFMODULE where MODULEID= '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
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
