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
	/// Summary description for SearchBranch.
	/// </summary>
	public partial class SearchBranch : System.Web.UI.Page
	{
		protected Connection conn;
		private string theForm = "", theObj = "", theObjDesc = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());

			theForm = Request.QueryString["targetFormID"];
			theObj = Request.QueryString["targetObjectID"];
			theObjDesc = Request.QueryString["targetObjectDesc"];

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
			if ((TXT_BRANCH_CODE.Text.Trim() != "") && (TXT_BRANCH_NAME.Text.Trim() == ""))
			{
				conn.QueryString = "select branch_code, branch_name from rfbranch where branch_code like '%" + TXT_BRANCH_CODE.Text.Trim() + "%' and active = '1'";
				conn.ExecuteQuery();
			}
			else if ((TXT_BRANCH_NAME.Text.Trim() != "") && (TXT_BRANCH_CODE.Text.Trim() == ""))
			{
				conn.QueryString = "select branch_code, branch_name from rfbranch where branch_name like '%" + TXT_BRANCH_NAME.Text.Trim() + "%'  and active = '1'";
				conn.ExecuteQuery();
			}
			else if ((TXT_BRANCH_CODE.Text.Trim() != "") && (TXT_BRANCH_NAME.Text.Trim() != ""))
			{
				conn.QueryString = "select branch_code, branch_name from rfbranch where branch_code like '%" + TXT_BRANCH_CODE.Text.Trim() + "%' and branch_name like '%" + TXT_BRANCH_NAME.Text.Trim() + "%'  and active = '1'";
				conn.ExecuteQuery();
			}
			else if ((TXT_BRANCH_CODE.Text.Trim() == "") && (TXT_BRANCH_NAME.Text.Trim() == ""))
			{
				conn.QueryString = "select branch_code, branch_name from rfbranch where branch_code = '" + TXT_BRANCH_CODE.Text + "'  and active = '1'";
				conn.ExecuteQuery();
			}

			LST_RESULT.Items.Clear();
			for (int i = 0; i < conn.GetRowCount(); i++)
				LST_RESULT.Items.Add(new ListItem(conn.GetFieldValue(i,0) + " - " + conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
		}
	}
}
