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

namespace CuBES_Maintenance.User
{
	/// <summary>
	/// Summary description for SearchUnit.
	/// </summary>
	public partial class SearchUnit : System.Web.UI.Page
	{
		protected Connection conn;
		private string theForm = "", theObj = "", theObjDesc = "", mode = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());

			theForm = Request.QueryString["targetFormID"];
			theObj = Request.QueryString["targetObjectID"];
			theObjDesc = Request.QueryString["targetObjectDesc"];
			mode = Request.QueryString["unit"];

			ok.Attributes.Add("onclick","pilih('" + theObj + "', '" + theObjDesc + "');");
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

		protected void BTN_SEARCH_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "EXEC USERMAINTENANCE_SEARCH_UNIT '" + mode + "', '" + TXT_UNIT_CODE.Text + "', '" + TXT_UNIT_NAME.Text + "'";
			conn.ExecuteQuery();

			LST_RESULT.Items.Clear();
			for (int i = 0; i < conn.GetRowCount(); i++)
				LST_RESULT.Items.Add(new ListItem(conn.GetFieldValue(i,0) + " - " + conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
		}
	}
}
