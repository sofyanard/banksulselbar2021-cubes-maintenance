using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Configuration;

namespace clsSetConn
{
	public class DBConn
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		
		public string getConnection(string str)
		{
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + str+ "'";
			conn.ExecuteQuery();
			string DB_NAMA		= conn.GetFieldValue("DB_NAMA");
			string DB_IP		= conn.GetFieldValue("DB_IP");
			string DB_LOGINID	= conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD	= conn.GetFieldValue("DB_LOGINPWD");
			
			return "Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true";
			
		}
	}
}
