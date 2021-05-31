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
using System.Configuration;
using System.Web.Security;

namespace CuBES_Maintenance
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public partial class Login : System.Web.UI.Page
	{
		protected Connection conn;
		string connString;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			/*
			if (Request.QueryString.Count > 0)
				Label1.Text = "Session Lost... Please Login";
			*/

            BTN_SUBMIT.Click +=new EventHandler(BTN_SUBMIT_Click);
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

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			//connString = ConfigurationSettings.AppSettings["conn"] + TXT_USERNAME.Text + ";pwd=" + System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(TXT_PASSWORD.Text, "sha1") + ";Pooling=true";
			connString = ConfigurationSettings.AppSettings["conn"];
			//try
			//{
				conn = new Connection(connString);

				int flag = ValidateLogin(TXT_USERNAME.Text, System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(TXT_PASSWORD.Text, "sha1"), conn);
				if (flag == 0)
					Label1.Text = "Invalid Username/Password";
				else if (flag == 2)
					Label1.Text = "User is currently logged in!";
				else if (flag == 1 || flag == 2)
				{
					AddSession();
					//conn.QueryString = "update scalluser set su_logon='1' where userid='" + TXT_USERNAME.Text + "'";
					//conn.ExecuteNonQuery();

					FormsAuthentication.SetAuthCookie(TXT_USERNAME.Text, false);


					Response.Redirect("main.html");
					
				}
				else if (flag == 3)
					Label1.Text = "User: " + TXT_USERNAME.Text + " is locked!";
			//}
			//catch
			//{
				conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
				//int flag = ValidateLogin(TXT_USERNAME.Text, System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(TXT_PASSWORD.Text, "sha1"), conn);

				if (flag == 2) 
					Label1.Text = "User is currently logged in!";
				else if (flag == 3)
					Label1.Text = "User: " + TXT_USERNAME.Text + " is locked!";
				else
					Label1.Text = "Invalid Login!";
			//}
		}

		private void AddSession()
		{
			conn.QueryString = "SELECT * FROM VW_SESSIONLOS WHERE USERID='" + TXT_USERNAME.Text + "'";
			conn.ExecuteQuery();

			Session.Add("ConnString", connString);
			Session.Add("UserID", conn.GetFieldValue("USERID"));
			Session.Add("FullName", conn.GetFieldValue("SU_FULLNAME"));
			Session.Add("GroupID", conn.GetFieldValue("GROUPID"));
			Session.Add("BranchID", conn.GetFieldValue("SU_BRANCH"));
			Session.Add("AreaID", conn.GetFieldValue("AREAID"));
			Session.Add("LoginTime", System.DateTime.Now.ToString());
			Session.Add("CBC", conn.GetFieldValue("CBC_CODE"));
			Session.Add("PWD", System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(TXT_PASSWORD.Text, "sha1"));
		}

		private int ValidateLogin(string userName, string password, Connection conn)
		{
			int flag = 0, falsepwd = 0;

			conn.QueryString = "select SU_PWD, SU_LOGON, SU_REVOKE from vw_loginmaintenance where userid='" + userName + "' and moduleid = '99'"; //and su_pwd='" + password + "'";
			conn.ExecuteQuery();
			if (conn.GetRowCount() == 0)
				flag = 0;
			else if (conn.GetFieldValue("SU_LOGON") == "1")
				flag = 2;
			else
			{
				if (password.Equals(conn.GetFieldValue("SU_PWD")))
				{
					if (conn.GetFieldValue("SU_REVOKE") == "0")
						flag = 1;
					else
						flag = 3;
				}
				else 
				{
					falsepwd = 1;
					conn.QueryString = "update scalluser set su_falsepwdcount = su_falsepwdcount + 1 where userid = '" + userName + "'";
					conn.ExecuteNonQuery();
					flag = 0;
				}
			}
			if (falsepwd == 1)
			{
				conn.QueryString = "select isnull(loginmaxattempt,1) loginmaxattempt from app_parameter";
				conn.ExecuteQuery();
				int loginMaxAttempt = int.Parse(conn.GetFieldValue("loginmaxattempt"));

				conn.QueryString = "select isnull(su_falsepwdcount,0) su_falsepwdcount from scalluser where userid = '" + userName + "'";
				conn.ExecuteQuery();
				int falsePwdCount = int.Parse(conn.GetFieldValue("su_falsepwdcount"));

				if (falsePwdCount >= loginMaxAttempt)
					flag = 3;
			}
			return flag;
		}
	}
}
