using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.User
{
	/// <summary>
	/// Summary description for ChangePassword.
	/// </summary>
	public partial class ChangePassword : System.Web.UI.Page
	{
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
			GlobalTools.SetFocus(this, txt_OldPwd);
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

		protected void btn_Change_Click(object sender, System.EventArgs e)
		{
			string newPassword = "", oldPassword = "";

			oldPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txt_OldPwd.Text.Trim(), "sha1");
			conn.QueryString = "select su_pwd from scalluser where userid = '" + Session["UserID"].ToString() + "'";
			conn.ExecuteQuery();

			if (oldPassword == conn.GetFieldValue(0, "su_pwd"))
			{
				conn.QueryString = "select isnull(minpwdlength,0) minpwdlength from app_parameter";
				conn.ExecuteQuery();
				if (txt_NewPwd.Text.Length >= int.Parse(conn.GetFieldValue("minpwdlength")))
				{
					if (txt_NewPwd.Text.Trim() == txt_ConfirmPwd.Text.Trim())
					{
						newPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txt_NewPwd.Text, "sha1");
						conn.QueryString = "exec SU_SCALLUSERPASSWORD '" + Session["UserID"] + "', '" + newPassword + "'";
						conn.ExecuteNonQuery();

						conn.QueryString = "select db_ip, db_nama, db_loginid, db_loginpwd from rfmodule ";
						conn.ExecuteQuery();
						DataTable dtModule = conn.GetDataTable().Copy();

						for (int i = 0; i < conn.GetRowCount(); i++)
						{
							try
							{
								string connectionString = "Data Source=" + conn.GetFieldValue(dtModule, i, "db_ip") +
									";Initial Catalog=" + conn.GetFieldValue(dtModule, i, "db_nama") +
									";uid=" + conn.GetFieldValue(dtModule, i, "db_loginid") +
									";pwd=" + conn.GetFieldValue(dtModule, i, "db_loginpwd") + ";Pooling=true";
								Connection lclConn = new Connection(connectionString);
								lclConn.QueryString = "exec SU_SCUSERPASSWORD '" + Session["UserID"] + "', '" + newPassword + "'";
								lclConn.ExecuteNonQuery();
							}
							catch {}
						}

						Message.Text = "";
						Clear();
						Response.Write("<script for=window event=onload language=javascript>\n"+
							"alert('Password Updated!');</script>");
					}
					else 
					{
						Message.Text = "Password mismatch!";
						Clear();
					}
				}	
				else 
				{
					Message.Text = "Password length at least " + conn.GetFieldValue("minpwdlength") + " characters.";
					Clear();
				}
			}
			else
			{
				Message.Text = "Old Password invalid!";
				Clear();
			}
		}

		private void Clear()
		{
			txt_OldPwd.Text = "";
			txt_NewPwd.Text = "";
			txt_ConfirmPwd.Text = "";
		}
	}
}
