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

namespace CuBES_Maintenance.Report
{
	/// <summary>
	/// Summary description for CreateUserLoginID.
	/// </summary>
	public partial class CreateUserLoginID : System.Web.UI.Page
	{
		protected Connection conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			/*
			conn = new Connection ("Data Source=" + db_ip + ";Initial Catalog=" + db_nama + ";uid=" + uid + ";pwd=" + pwd + ";Pooling=true");

			conn.QueryString = "exec SU_CREATEDBLOGIN '" + userid + "', '" + 
				dtPendingUser.Rows[0]["su_pwd"].ToString() + "', '" + 
				db_nama + "', '" + dtPendingUser.Rows[0]["status"].ToString() + "'";
			conn.ExecuteNonQuery();
			*/
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
