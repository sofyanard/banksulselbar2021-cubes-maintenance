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
using DMS.CuBESCore;

namespace CuBES_Maintenance.Parameter.General.CC
{
	/// <summary>
	/// Summary description for ParamSetupZipcode.
	/// </summary>
	public partial class ParamSetupZipcode : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection (System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;
		/* New Field Declare */
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				GlobalTools.fillRefList(DDL_AGENCY,"select agencyid, agencyname from TAGENCY where active='1' order by agencyname",conn);
				ViewZipcode();
			}
			BTN_AGENT.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
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
			this.GRD_ZIPCODE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.GRD_ZIPCODE_ItemCommand);
			this.GRD_ZIPCODE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.GRD_ZIPCODE_PageIndexChanged);

		}
		#endregion

		public void SetDBConn2()
		{
			conn2.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn2.ExecuteQuery();
			string DB_NAMA = conn2.GetFieldValue("DB_NAMA");
			string DB_IP = conn2.GetFieldValue("DB_IP");
			string DB_LOGINID = conn2.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn2.GetFieldValue("DB_LOGINPWD");
			conn = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		protected void ViewZipcode()
		{
			conn.QueryString = "select * from VW_INITIAL_ZIPCODE order by agencyname";
			conn.ExecuteQuery();
			try 
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				GRD_ZIPCODE.DataSource = dt;
				GRD_ZIPCODE.DataBind();
			} 
			catch 
			{
				GlobalTools.popMessage(this, "Error Grid !");
				return;
			}
		}

		private void GRD_ZIPCODE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			GlobalTools.SetFocus(this, (WebControl)source);
			GRD_ZIPCODE.CurrentPageIndex = e.NewPageIndex;
			ViewZipcode();
		}

		protected void BTN_AGENT_Click(object sender, System.EventArgs e)
		{
			if (DDL_AGENCY.Enabled  == true)
			{
				conn.QueryString = "exec INITIAL_ZIPCODE "+GlobalTools.ConvertNull(DDL_AGENCY.SelectedValue)+", '"+TXT_STARTZIP.Text
					+"', '"+TXT_ENDZIP.Text+"', 0, '0'";
				conn.ExecuteNonQuery();
			}
			else
			{
				conn.QueryString = "exec INITIAL_ZIPCODE "+GlobalTools.ConvertNull(DDL_AGENCY.SelectedValue)+", '"+TXT_STARTZIP.Text
					+"', '"+TXT_ENDZIP.Text+"', '"+LBL_SEQ.Text+"', '1'";
				conn.ExecuteNonQuery();
			}
			ClearZipCode("","","","",true);
			ViewZipcode();
			GlobalTools.popMessage(this,"Request has been saved");
		}

		private void GRD_ZIPCODE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					ClearZipCode(e.Item.Cells[0].Text,e.Item.Cells[3].Text,e.Item.Cells[4].Text,e.Item.Cells[1].Text,false);
					break;
				case "delete":
					conn.QueryString = "exec INITIAL_ZIPCODE "+GlobalTools.ConvertNull(e.Item.Cells[0].Text)+", '"+e.Item.Cells[3].Text
						+"', '"+e.Item.Cells[4].Text+"', '"+e.Item.Cells[1].Text+"', '2'";
					conn.ExecuteNonQuery();
					GlobalTools.popMessage(this,"Request has been deleted");
					int index = GRD_ZIPCODE.Items.Count;			
					int jml = (index % 10)-1;
					if (jml == 0)
						GRD_ZIPCODE.CurrentPageIndex = index/10;
					ViewZipcode();
					break;
			}
		}

		protected void ClearZipCode(string agency, string start, string end, string seq, bool sta)
		{
			DDL_AGENCY.SelectedValue = agency;
			TXT_ENDZIP.Text = end;
			TXT_STARTZIP.Text = start;
			LBL_SEQ.Text = seq;
			DDL_AGENCY.Enabled = sta;
		}
	}
}








