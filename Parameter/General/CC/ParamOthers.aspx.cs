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
	/// Summary description for ParamOthers.
	/// </summary>
	public partial class Others : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.TextBox TextBox3;
		//protected Connection conn2 = new Connection (System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;
		/* New Field Declare */
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				/*
				ArrayList a = new ArrayList();
				a.Clear();
				a.Add(DDL_IN_JTEMPLOY);
				a.Add(DDL_IN_JTPROF);
				a.Add(DDL_IN_JTSELF);
				GlobalTools.fillRefList(a,"select job_type_id, des from job_type where active='1' order by des",conn);
				a.Clear();
				*/
				FillDDL();
				ViewValue();
			}
			ViewMenu();
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

		private void FillDDL()
		{
			string strquery = "select job_type_id, des from job_type where active='1' order by des";
			GlobalTools.fillRefList(DDL_IN_JTEMPLOY,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_JTPROF,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_JTSELF,strquery,conn);
		}

		private void ViewMenu()
		{
			try 
			{
				conn.QueryString = "select * from SCREENMENU where menucode = '3301'";
				conn.ExecuteQuery();

				for (int i = 0; i < conn.GetRowCount(); i++)
				{
					HyperLink t = new HyperLink();
					t.Text = conn.GetFieldValue(i,2);
					t.Font.Bold = true;
					t.ForeColor = Color.Blue;
					string link = conn.GetFieldValue(i,3).Trim();
					if (link == "")
						t.ForeColor = Color.Red; 
					else
					{
						int insAt;
						string strtemp = "";
						if (link.IndexOf("javascript:") == 0)
						{
							insAt = link.IndexOf(".aspx?");
							if (insAt!=-1) insAt += 6;
						} 
						else insAt = link.Length;
						if (link.IndexOf("ParamOthers.aspx") != -1)
							t.ForeColor = Color.Red;
						if (link.IndexOf("mc=") >= 0)
							strtemp = "regno=" + Request.QueryString["regno"] + "&curef="+Request.QueryString["curef"]+"&tc="+Request.QueryString["tc"];
						else strtemp = "regno=" + Request.QueryString["regno"] + "&curef="+Request.QueryString["curef"]+"&mc="+Request.QueryString["mc"]+"&tc="+Request.QueryString["tc"];

						if (link.IndexOf("?de=") < 0 && link.IndexOf("&de=") < 0) 
							strtemp = strtemp + "&de=" + Request.QueryString["de"];	
						if (link.IndexOf("?par=") < 0 && link.IndexOf("&par=") < 0)  
							strtemp = strtemp + "&par=" + Request.QueryString["par"];
						if (insAt!=-1)	link = link.Insert(insAt, strtemp);
					}
					t.NavigateUrl = link;
					Menu.Controls.Add(t);
					Menu.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
				}
			} 
			catch (Exception ex) 
			{
				string temp = ex.ToString();
			}
		}

		private void ViewValue()
		{
			conn.QueryString = "select * from VW_INITIAL_OTHERS";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				DDL_IN_JTEMPLOY.SelectedValue = conn.GetFieldValue("IN_JTEMPLOY");
				DDL_IN_JTSELF.SelectedValue = conn.GetFieldValue("IN_JTSELF");
				DDL_IN_JTPROF.SelectedValue = conn.GetFieldValue("IN_JTPROF");
				TXT_IN_VERPHNLIMIT.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("IN_VERPHNLIMIT"));
				TXT_IN_VERPHNLESS.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("IN_VERPHNLESS"));
				TXT_IN_VERPHNMORE.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("IN_VERPHNMORE"));
				TXT_IN_VERSITLIMIT.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("IN_VERSITLIMIT"));
				TXT_IN_VERSITLESS.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("IN_VERSITLESS"));
				TXT_IN_VERSITMORE.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("IN_VERSITMORE"));
				/* New Field View */
			}
			conn.ClearData();

		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = " update INITIAL set IN_JTEMPLOY = "+ GlobalTools.ConvertNull(DDL_IN_JTEMPLOY.SelectedValue)
				+", IN_JTSELF = "+ GlobalTools.ConvertNull(DDL_IN_JTSELF.SelectedValue)
				+", IN_JTPROF = "+ GlobalTools.ConvertNull(DDL_IN_JTPROF.SelectedValue)
				+", IN_VERPHNLIMIT = "+ GlobalTools.ConvertFloat(TXT_IN_VERPHNLIMIT.Text)
				+", IN_VERPHNLESS = "+ GlobalTools.ConvertFloat(TXT_IN_VERPHNLESS.Text)
				+", IN_VERPHNMORE = "+ GlobalTools.ConvertFloat(TXT_IN_VERPHNMORE.Text)
				+", IN_VERSITLIMIT = "+ GlobalTools.ConvertFloat(TXT_IN_VERSITLIMIT.Text)
				+", IN_VERSITLESS = "+ GlobalTools.ConvertFloat(TXT_IN_VERSITLESS.Text)
				+", IN_VERSITMORE = "+ GlobalTools.ConvertFloat(TXT_IN_VERSITMORE.Text);
			conn.ExecuteNonQuery();
			GlobalTools.popMessage(this,"Request has been saved");
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}
}

