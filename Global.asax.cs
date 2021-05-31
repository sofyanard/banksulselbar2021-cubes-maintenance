using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using DMS.DBConnection;
using System.Configuration;

namespace CuBES_Maintenance 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		protected Connection conn = new Connection(ConfigurationSettings.AppSettings["eSecurityConnectString"]);

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{

		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{
			// if session is lost
            
			if (Session["UserID"]==null)
			{
				Server.ClearError();

                try
                {
                    conn.QueryString = "UPDATE SCALLUSER SET SU_LOGON = '0' WHERE USERID = '" + Session["UserID"] + "'";
                    conn.ExecuteQuery();
                }
                catch
                {

                }
				Response.Redirect(Request.ApplicationPath + "/Logout.aspx?login", true);
			}
            
		}

		protected void Session_End(Object sender, EventArgs e)
		{
//			conn.QueryString = "exec SU_USERACTIVITY '" + Session["UserID"].ToString() + "', '" + 
//				Session["GroupID"].ToString() + "', '0', '0', '0'";
//			conn.ExecuteNonQuery();
            /*
			conn.QueryString = "exec SU_ALLUSERACTIVITY '" + Session["UserID"].ToString() + "', '" + 
				Session["GroupID"].ToString() + "', '0', '0', '0', '0'";
			conn.ExecuteNonQuery();
            */

            conn.QueryString = "UPDATE SCALLUSER SET SU_LOGON = '0' WHERE USERID = '" + Session["UserID"] + "'";
            conn.ExecuteQuery();
		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

