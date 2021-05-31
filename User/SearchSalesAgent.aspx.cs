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
	/// Summary description for SearchSalesAgent.
	/// </summary>
	public partial class SearchSalesAgent : System.Web.UI.Page
	{
		protected Connection conn;
		private string theForm = "", theObj = "", theObjDesc = "", agentid = "";
		protected System.Web.UI.WebControls.DropDownList DDL_CITY;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());

			theForm = Request.QueryString["targetFormID"];
			theObj = Request.QueryString["targetObjectID"];
			theObjDesc = Request.QueryString["targetObjectDesc"];
			agentid = Request.QueryString["agentid"];

			if (!IsPostBack)
				LBL_AGENTNAME.Text = Request.QueryString["agentname"];

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
			LST_RESULT.Items.Clear();

			conn.QueryString = "select agofr_code, agofr_desc from rfagencyofr where agencyid = '" + agentid + "'";
			if (TXT_AGENTNAME.Text.Trim() != "")
				conn.QueryString = "select agofr_code, agofr_desc from rfagencyofr where agencyid = '" + agentid + "' and agofr_desc like '%" + TXT_AGENTNAME.Text + "%'";
			conn.ExecuteQuery();

			for (int i = 0; i < conn.GetRowCount(); i++)
				LST_RESULT.Items.Add(new ListItem(conn.GetFieldValue(i, "agofr_desc"), conn.GetFieldValue(i, "agofr_code")));
		}
	}
}
