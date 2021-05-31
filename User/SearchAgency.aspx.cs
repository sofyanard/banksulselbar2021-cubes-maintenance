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

namespace CuBES_Maintenance.User
{
	/// <summary>
	/// Summary description for SearchAgency.
	/// </summary>
	public partial class SearchAgency : System.Web.UI.Page
	{
		protected Connection conn;
		private string theForm = "", theObj = "", theObjDesc = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());
//sdfsdfdsf
			theForm = Request.QueryString["targetFormID"];
			theObj = Request.QueryString["targetObjectID"];
			theObjDesc = Request.QueryString["targetObjectDesc"];

			ok.Attributes.Add("onclick","pilih('" + theObj + "', '" + theObjDesc + "');");

			if (!IsPostBack)
			{
				DDL_CITY.Items.Add(new ListItem("- SELECT -", ""));
				conn.QueryString = "select cityid, cityname from rfcity";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_CITY.Items.Add(new ListItem(conn.GetFieldValue(i, "cityname"), conn.GetFieldValue(i, "cityid")));
			}
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
			LST_RESULT.Items.Clear();

			if ((DDL_CITY.SelectedValue != "") && (TXT_AGENCYNAME.Text.Trim() != ""))
				conn.QueryString = "select agencyid, agencyname from rfagency where cityid = '" + DDL_CITY.SelectedValue + "' and agencyname like '%" + TXT_AGENCYNAME.Text + "%'";
			else if ((DDL_CITY.SelectedValue != "") && (TXT_AGENCYNAME.Text.Trim() == ""))
				conn.QueryString = "select agencyid, agencyname from rfagency where cityid = '" + DDL_CITY.SelectedValue + "'";
			else if ((DDL_CITY.SelectedValue == "") && (TXT_AGENCYNAME.Text.Trim() != ""))
				conn.QueryString = "select agencyid, agencyname from rfagency where agencyname like '%" + TXT_AGENCYNAME.Text + "%'";
			else
				conn.QueryString = "select agencyid, agencyname from rfagency";
			conn.ExecuteQuery();

			for (int i = 0; i < conn.GetRowCount(); i++)
				LST_RESULT.Items.Add(new ListItem(conn.GetFieldValue(i, "agencyname"), conn.GetFieldValue(i, "agencyid")));
		}
	}
}
