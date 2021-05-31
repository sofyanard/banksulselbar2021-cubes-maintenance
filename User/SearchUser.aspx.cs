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
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Configuration;

namespace CuBES_Maintenance.User
{
	/// <summary>
	/// Summary description for SearchUser.
	/// </summary>
	public partial class SearchUser : System.Web.UI.Page
	{
		protected Connection conn;
		private string theForm = "", theObj = "", theObjDesc = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());

			if (!IsPostBack)
			{
				DDL_RFMODULE.Items.Add(new ListItem("- PILIH -", ""));
				DDL_RFAREA.Items.Add(new ListItem("- PILIH -", ""));
				DDL_RFBRANCH.Items.Add(new ListItem("- PILIH -", ""));
				DDL_RFGROUP.Items.Add(new ListItem("- PILIH -", ""));

				conn.QueryString = "select moduleid, modulename from rfmodule where active = '1'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_RFMODULE.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));

				conn.QueryString = "select areaid, areaname from rfarea where active = '1'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_RFAREA.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));

				conn.QueryString = "select branch_code, branch_name from rfbranch where active = '1' order by branch_code";
				conn.ExecuteQuery(); 
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_RFBRANCH.Items.Add(new ListItem(conn.GetFieldValue(i,0) + " - " + conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));

				//conn.QueryString = "select groupid, sg_grpname, modulename from vw_scallgroup order by modulename, sg_grpname";
				conn.QueryString = "select distinct groupid, sg_grpname from vw_scallgroup where (moduleid is not null and moduleid <> '') order by sg_grpname";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_RFGROUP.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
			}
			
			theForm = Request.QueryString["targetFormID"].Trim();
			theObj = Request.QueryString["targetObjectID"].Trim();
			theObjDesc = Request.QueryString["targetObjectDesc"].Trim();

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
			FillData();
		}

		private void FillData()
		{
			string sql = "WHERE", sjoin = "AND";
			if (DDL_RFMODULE.SelectedValue != "")
				sql = sql + " MODULEID = '" + DDL_RFMODULE.SelectedValue + "' " + sjoin;
			if (DDL_RFAREA.SelectedValue != "")
				sql = sql + " AREAID = '" + DDL_RFAREA.SelectedValue + "' " + sjoin;
			if (DDL_RFBRANCH.SelectedValue != "")
				sql = sql + " SU_BRANCH = '" + DDL_RFBRANCH.SelectedValue + "' " + sjoin;
			if (DDL_RFGROUP.SelectedValue != "")
				sql = sql + " GROUPID = '" + DDL_RFGROUP.SelectedValue + "' " + sjoin;
			if (TXT_SEARCH_USERNAME.Text.Trim() != "")
				sql = sql + " SU_FULLNAME like '%" + TXT_SEARCH_USERNAME.Text.Trim() + "%' " + sjoin;
			if (TXT_SEARCH_USERID.Text.Trim() != "")
				sql = sql + " USERID = '" + TXT_SEARCH_USERID.Text.Trim() + "' " + sjoin;
			if (TXT_SEARCH_OFFICERCODE.Text.Trim() != "")
				sql = sql + " OFFICER_CODE = '" + TXT_SEARCH_OFFICERCODE.Text.Trim() + "' " + sjoin;

			/*if (sql == "WHERE")
				sql = "";
			else
				sql = sql.Substring(0, sql.Length - sjoin.Length);
				*/
			sql += " SU_ACTIVE = '1' ";		//search only active users.... sjoin is always 'AND'

			LST_RESULT.Items.Clear();
			//LST_RESULT.Items.Add(new ListItem("- PILIH -", ""));
			conn.QueryString = "select distinct userid, su_fullname, groupid, sg_grpname, su_logon, su_revoke from vw_scalluser " + sql + " order by su_fullname";
			conn.ExecuteQuery();
			for (int i = 0; i < conn.GetRowCount(); i++)
				LST_RESULT.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
		}
	}
}
