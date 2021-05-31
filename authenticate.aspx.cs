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
using System.Configuration;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Web.Security;

namespace CuBES_Maintenance
{
	/// <summary>
	/// Summary description for authenticate.
	/// </summary>
	public partial class authenticate : System.Web.UI.Page
	{
		protected Connection connESecurity = new Connection(ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn;
		protected string token;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string connectionString;

			connESecurity.QueryString = "select * from VW_ES_APPTOKEN where token = '" + Request.QueryString["tkn"] + "'";
			connESecurity.ExecuteQuery();
			if (connESecurity.GetRowCount() > 0)
			{
				string uid = connESecurity.GetFieldValue(0, "userid"), 
					pwd = connESecurity.GetFieldValue(0, "pwd");
				FormsAuthentication.SignOut();
				FormsAuthentication.SetAuthCookie(uid, false);

				connESecurity.QueryString = "select db_ip, db_nama, db_loginid, db_loginpwd from rfmodule where moduleid = '99'";
				connESecurity.ExecuteQuery();

				connectionString = "Data Source=" + connESecurity.GetFieldValue(0, "db_ip") + ";Initial Catalog=" + connESecurity.GetFieldValue(0, "db_nama") + ";uid=" + connESecurity.GetFieldValue(0, "db_loginid") + ";pwd=" + connESecurity.GetFieldValue(0, "db_loginpwd") + ";Pooling=true";
				Session.Add("ConnString", connectionString);

				connESecurity.QueryString = "exec ES_APPTOKEN_DELETE '" + Request.QueryString["tkn"] + "'";
				connESecurity.ExecuteNonQuery();

				/* Re-organize Sessions: Set branch level or area level user 
				 * if area level set branchid session to null */
				conn = new Connection(connectionString);

				AddSession(uid, pwd);

				Response.Redirect("main.html");
			}
			else
			{
				connESecurity.QueryString = "select top 1 login_scr from rfmodule";
				connESecurity.ExecuteQuery();

                conn = new Connection(ConfigurationSettings.AppSettings["conn"]);

                try
                {
                    conn.QueryString = "UPDATE SCALLUSER SET SU_LOGON = '0' WHERE USERID = '" + Session["UserID"] + "'";
                    conn.ExecuteQuery();
                }
                catch(Exception o)
                {
                    string mess = o.Message;
                    string tes = "";
                }

				Response.Redirect(connESecurity.GetFieldValue(0, "login_scr"));
			}

		}

		private void AddSession(string uid, string pwd)
		{
			conn.QueryString = "SELECT * FROM VW_SESSIONLOS WHERE USERID='" + uid + "'";
			conn.ExecuteQuery();

			Session.Add("UserID", conn.GetFieldValue("USERID"));
			Session.Add("FullName", conn.GetFieldValue("SU_FULLNAME"));
			Session.Add("GroupID", conn.GetFieldValue("GROUPID"));
			Session.Add("BranchID", conn.GetFieldValue("SU_BRANCH"));
			Session.Add("AreaID", conn.GetFieldValue("AREAID"));
			Session.Add("LoginTime", System.DateTime.Now.ToString());
			Session.Add("CBC", conn.GetFieldValue("CBC_CODE"));
			Session.Add("PWD", pwd);
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
