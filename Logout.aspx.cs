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
using System.Web.Security;
using DMS.DBConnection;

namespace CuBES_Maintenance
{
	/// <summary>
	/// Summary description for Logout.
	/// </summary>
	public partial class Logout : System.Web.UI.Page
	{
		protected Connection conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				conn = new Connection(Session["ConnString"].ToString());
	
				conn.QueryString = "exec SU_ALLUSERACTIVITY '" + Session["UserID"].ToString() + "', '" + 
					Session["GroupID"].ToString() + "', '0', '0', '0', '" + Request.UserHostAddress + "', '" + Request.UserHostName + "'";
				conn.ExecuteNonQuery();

                conn.QueryString = "UPDATE SCALLUSER SET SU_LOGON = '0' WHERE USERID = '" + Session["UserID"].ToString() + "'";
                conn.ExecuteQuery();
			}	
			catch { }

			Session.Clear();
			Session.Abandon();
			FormsAuthentication.SignOut();

			conn.QueryString = "select login_scr from rfmodule where moduleid = '99'";
			conn.ExecuteQuery();

			string lost = ((Request.QueryString.Keys.Count!=0 && Request.QueryString[0]=="login")?
				"?login":"");

			Response.Redirect(conn.GetFieldValue(0, "login_scr") + lost);
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
