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
	/// Summary description for ParamCalculation.
	/// </summary>
	public class ParamCalculation : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.HtmlControls.HtmlAnchor Back;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWOPRCODE1;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWOPRCODE2;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWOPRCODE3;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWOPRCODE5;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWOPRCODE6;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCCRCODE2;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCCRCODE3;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCCRCODE4;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCCRCODE5;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE1;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE11;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE2;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE3;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE4;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE5;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE6;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE12;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE7;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE8;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE9;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE10;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWCALCODE13;
		protected System.Web.UI.WebControls.DropDownList DDL_IN_CWOPRCODE4;
		protected System.Web.UI.WebControls.TextBox TXT_IN_LIMIT;
		protected System.Web.UI.WebControls.TextBox TXT_IN_TINCREG;
		protected System.Web.UI.WebControls.TextBox TXT_IN_TACCREG;
		protected System.Web.UI.WebControls.TextBox TXT_IN_SERVIND10;
		protected System.Web.UI.WebControls.TextBox TXT_IN_SERVIND12;
		protected System.Web.UI.WebControls.TextBox TXT_IN_MAXOPEN;
		protected System.Web.UI.WebControls.Button BTN_SAVE;
		//protected Connection conn2 = new Connection (System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected System.Web.UI.HtmlControls.HtmlAnchor back;
		protected System.Web.UI.WebControls.ImageButton BTN_BACK;
		protected Connection conn,conn2;
		/* New Field Declare */
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				/*
				ArrayList a = new ArrayList();
				a.Clear();
				a.Add(DDL_IN_CWOPRCODE1);
				a.Add(DDL_IN_CWOPRCODE2);
				a.Add(DDL_IN_CWOPRCODE3);
				a.Add(DDL_IN_CWOPRCODE5);
				a.Add(DDL_IN_CWOPRCODE6);
				a.Add(DDL_IN_CWCCRCODE2);
				a.Add(DDL_IN_CWCCRCODE3);
				a.Add(DDL_IN_CWCCRCODE4);
				a.Add(DDL_IN_CWCCRCODE5);
				a.Add(DDL_IN_CWCALCODE1);
				a.Add(DDL_IN_CWCALCODE11);
				a.Add(DDL_IN_CWCALCODE2);
				a.Add(DDL_IN_CWCALCODE3);
				a.Add(DDL_IN_CWCALCODE4);
				a.Add(DDL_IN_CWCALCODE5);
				a.Add(DDL_IN_CWCALCODE6);
				a.Add(DDL_IN_CWCALCODE12);
				a.Add(DDL_IN_CWCALCODE7);
				a.Add(DDL_IN_CWCALCODE8);
				a.Add(DDL_IN_CWCALCODE9);
				a.Add(DDL_IN_CWCALCODE10);
				a.Add(DDL_IN_CWCALCODE13);
				a.Add(DDL_IN_CWOPRCODE4);
				//GlobalTools.fillRefList(a,"select cw_code, cw_desc from rfcawlst order by cw_desc",conn);
				a.Clear();
				*/
				FillDDL();
				ViewValue();
			}
			//ViewMenu();
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
			this.BTN_SAVE.Click += new System.EventHandler(this.BTN_SAVE_Click);
			this.BTN_BACK.Click += new System.Web.UI.ImageClickEventHandler(this.BTN_BACK_Click);
			this.Load += new System.EventHandler(this.Page_Load);

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
			string strquery = "select cw_code, cw_desc from rfcawlst order by cw_desc";
			GlobalTools.fillRefList(DDL_IN_CWOPRCODE1,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWOPRCODE2,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWOPRCODE3,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWOPRCODE5,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWOPRCODE6,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCCRCODE2,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCCRCODE3,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCCRCODE4,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCCRCODE5,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE1,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE11,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE2,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE3,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE4,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE5,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE6,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE12,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE7,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE8,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE9,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE10,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE13,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWOPRCODE4,strquery,conn);
		}

		protected void ViewValue()
		{
			conn.QueryString = "select * from VW_INITIAL_CALCULTAION ";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				DDL_IN_CWOPRCODE1.SelectedValue = conn.GetFieldValue("IN_CWOPRCODE1");
				DDL_IN_CWOPRCODE2.SelectedValue = conn.GetFieldValue("IN_CWOPRCODE2");
				DDL_IN_CWOPRCODE3.SelectedValue = conn.GetFieldValue("IN_CWOPRCODE3");
				DDL_IN_CWOPRCODE5.SelectedValue = conn.GetFieldValue("IN_CWOPRCODE5");
				DDL_IN_CWOPRCODE6.SelectedValue = conn.GetFieldValue("IN_CWOPRCODE6");
				DDL_IN_CWCCRCODE2.SelectedValue = conn.GetFieldValue("IN_CWCCRCODE2");
				DDL_IN_CWCCRCODE3.SelectedValue = conn.GetFieldValue("IN_CWCCRCODE3");
				DDL_IN_CWCCRCODE4.SelectedValue = conn.GetFieldValue("IN_CWCCRCODE4");
				DDL_IN_CWCCRCODE5.SelectedValue = conn.GetFieldValue("IN_CWCCRCODE5");
				DDL_IN_CWCALCODE1.SelectedValue = conn.GetFieldValue("IN_CWCALCODE1");
				DDL_IN_CWCALCODE11.SelectedValue = conn.GetFieldValue("IN_CWCALCODE11");
				DDL_IN_CWCALCODE2.SelectedValue = conn.GetFieldValue("IN_CWCALCODE2");
				DDL_IN_CWCALCODE3.SelectedValue = conn.GetFieldValue("IN_CWCALCODE3");
				DDL_IN_CWCALCODE4.SelectedValue = conn.GetFieldValue("IN_CWCALCODE4");
				DDL_IN_CWCALCODE5.SelectedValue = conn.GetFieldValue("IN_CWCALCODE5");
				DDL_IN_CWCALCODE6.SelectedValue = conn.GetFieldValue("IN_CWCALCODE6");
				DDL_IN_CWCALCODE12.SelectedValue = conn.GetFieldValue("IN_CWCALCODE12");
				DDL_IN_CWCALCODE7.SelectedValue = conn.GetFieldValue("IN_CWCALCODE7");
				DDL_IN_CWCALCODE8.SelectedValue = conn.GetFieldValue("IN_CWCALCODE8");
				DDL_IN_CWCALCODE9.SelectedValue = conn.GetFieldValue("IN_CWCALCODE9");
				DDL_IN_CWCALCODE10.SelectedValue = conn.GetFieldValue("IN_CWCALCODE10");
				DDL_IN_CWCALCODE13.SelectedValue = conn.GetFieldValue("IN_CWCALCODE13");
				DDL_IN_CWOPRCODE4.SelectedValue = conn.GetFieldValue("IN_CWOPRCODE4");
				TXT_IN_LIMIT.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("IN_LIMIT"));
				TXT_IN_TINCREG.Text = conn.GetFieldValue("IN_TINCREG");
				TXT_IN_TACCREG.Text = conn.GetFieldValue("IN_TACCREG");
				TXT_IN_SERVIND10.Text = conn.GetFieldValue("IN_SERVIND10");
				TXT_IN_SERVIND12.Text = conn.GetFieldValue("IN_SERVIND12");
				TXT_IN_MAXOPEN.Text = conn.GetFieldValue("IN_MAXOPEN");
				/* New Field View */
			}
			conn.ClearData();
		}

		private void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = " update PENDING_CC_INITIAL SET IN_CWOPRCODE1 = "+ GlobalTools.ConvertNull(DDL_IN_CWOPRCODE1.SelectedValue)
				+",  IN_CWOPRCODE2 = "+ GlobalTools.ConvertNull(DDL_IN_CWOPRCODE2.SelectedValue)
				+",  IN_CWOPRCODE3 = "+ GlobalTools.ConvertNull(DDL_IN_CWOPRCODE3.SelectedValue)
				+",  IN_CWOPRCODE5 = "+ GlobalTools.ConvertNull(DDL_IN_CWOPRCODE5.SelectedValue)
				+",  IN_CWOPRCODE6 = "+ GlobalTools.ConvertNull(DDL_IN_CWOPRCODE6.SelectedValue)
				+",  IN_CWCCRCODE2 = "+ GlobalTools.ConvertNull(DDL_IN_CWCCRCODE2.SelectedValue)
				+",  IN_CWCCRCODE3 = "+ GlobalTools.ConvertNull(DDL_IN_CWCCRCODE3.SelectedValue)
				+",  IN_CWCCRCODE4 = "+ GlobalTools.ConvertNull(DDL_IN_CWCCRCODE4.SelectedValue)
				+",  IN_CWCCRCODE5 = "+ GlobalTools.ConvertNull(DDL_IN_CWCCRCODE5.SelectedValue)
				+",  IN_CWCALCODE1 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE1.SelectedValue)
				+",  IN_CWCALCODE11 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE11.SelectedValue)
				+",  IN_CWCALCODE2 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE2.SelectedValue)
				+",  IN_CWCALCODE3 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE3.SelectedValue)
				+",  IN_CWCALCODE4 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE4.SelectedValue)
				+",  IN_CWCALCODE5 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE5.SelectedValue)
				+",  IN_CWCALCODE6 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE6.SelectedValue)
				+",  IN_CWCALCODE12 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE12.SelectedValue)
				+",  IN_CWCALCODE7 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE7.SelectedValue)
				+",  IN_CWCALCODE8 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE8.SelectedValue)
				+",  IN_CWCALCODE9 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE9.SelectedValue)
				+",  IN_CWCALCODE10 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE10.SelectedValue)
				+",  IN_CWCALCODE13 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE13.SelectedValue)
				+",  IN_CWOPRCODE4 = "+ GlobalTools.ConvertNull(DDL_IN_CWOPRCODE4.SelectedValue)
				+",  IN_LIMIT = "+ GlobalTools.ConvertFloat(TXT_IN_LIMIT.Text)
				+",  IN_TINCREG = '"+ TXT_IN_TINCREG.Text
				+"',  IN_TACCREG = '"+ TXT_IN_TACCREG.Text
				+"',  IN_SERVIND10 = '"+ TXT_IN_SERVIND10.Text
				+"',  IN_SERVIND12 = '"+ TXT_IN_SERVIND12.Text
				+"',  IN_MAXOPEN = '"+ TXT_IN_MAXOPEN.Text + "'";
			conn.ExecuteNonQuery();
			GlobalTools.popMessage(this,"Request has been saved");
			ViewValue();
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
						if (link.IndexOf("ParamCalculation.aspx") != -1)
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

		private void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}
}

