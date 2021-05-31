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
	/// Summary description for CreditAnalystArea.
	/// </summary>
	public partial class CreditAnalystArea : System.Web.UI.Page
	{
		protected Connection conn;
		private string theForm = "", theObjDesc = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);

			theForm = Request.QueryString["targetFormID"];
			theObjDesc = Request.QueryString["targetObjectDesc"];

			if (!IsPostBack)
			{
				TXT_USERID.Text = Request.QueryString["uid"];

				conn.QueryString = "select areaid, areaname from rfarea where active = '1'";
				conn.ExecuteQuery();

				for (int i = 0; i < conn.GetRowCount(); i++)
					LST_AREA.Items.Add(new ListItem(conn.GetFieldValue(i, "areaname"), conn.GetFieldValue(i, "areaid")));

				conn.QueryString = "select rfcaregion.sc_id, rfarea.areaname from rfcaregion inner join rfarea on rfcaregion.region_id = rfarea.areaid where rfcaregion.sc_id = '" + Request.QueryString["uid"] + "'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
				{
					if (TXT_CC_AREA.Text.Trim() == "")
						TXT_CC_AREA.Text += conn.GetFieldValue(i, "areaname");
					else
						TXT_CC_AREA.Text += ", " + conn.GetFieldValue(i, "areaname");
				}
			}

			ok.Attributes.Add("onclick","pilih('" + theObjDesc + "');");

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
			ArrayList areaList = new ArrayList();

			foreach (ListItem items in LST_AREA.Items)
			{
				if (items.Selected == true)
				{
					if (TXT_AREA_REQUEsT.Text == "")
						TXT_AREA_REQUEsT.Text += items.Text;
					else
						TXT_AREA_REQUEsT.Text += ", " + items.Text;
					areaList.Add(items.Value);
				}
			}
			Session.Add("CAareaList", areaList);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			Session.Remove("CAareaList");
			Response.Write("<script language='javascript'>window.close();</script>");
		}
	}
}
