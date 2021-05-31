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
	/// Summary description for ParamCreditCard.
	/// </summary>
	public partial class ParamCreditCard : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox7;
		//protected Connection conn2 = new Connection (System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;
		/* New Field Declare */
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				ViewValue();
				ViewAgent();
				ViewStage();
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
			this.GRD_AGENT.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.GRD_AGENT_ItemCommand);
			this.GRD_AGENT.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.GRD_AGENT_PageIndexChanged);
			this.GRD_STAGE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.GRD_STAGE_ItemCommand);
			this.GRD_STAGE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.GRD_STAGE_PageIndexChanged);

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
						if (link.IndexOf("ParamCreditCard.aspx") != -1)
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

		protected void ViewValue()
		{
			conn.QueryString = "select * from VW_INITIAL_CREDITCARD";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				TXT_IN_GROUPADMIN.Text = conn.GetFieldValue("IN_GROUPADMIN");
				TXT_IN_GROUPANL.Text = conn.GetFieldValue("IN_GROUPANL");
				TXT_IN_GROUPVER.Text = conn.GetFieldValue("IN_GROUPVER");
				TXT_IN_MAXDAYAGENTJB.Text = conn.GetFieldValue("IN_MAXDAYAGENTJB");
				TXT_IN_MAXDAYAGENTLJB.Text = conn.GetFieldValue("IN_MAXDAYAGENTLJB");
				/* New Field View */
			}
			conn.ClearData();
		}

		protected void ViewStage()
		{
			conn.QueryString = "select tr_code, tr_desc, maxday from RFTRACKLST where ACTIVE='1' order by tr_code";
			conn.ExecuteQuery();
			try 
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				GRD_STAGE.DataSource = dt;
				GRD_STAGE.DataBind();
			} 
			catch 
			{
				GlobalTools.popMessage(this, "Error Grid !");
				return;
			}
		}

		private void GRD_AGENT_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			GlobalTools.SetFocus(this, (WebControl)source);
			GRD_AGENT.CurrentPageIndex = e.NewPageIndex;
			ViewAgent();
		}

		private void GRD_STAGE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			GlobalTools.SetFocus(this, (WebControl)source);
			GRD_STAGE.CurrentPageIndex = e.NewPageIndex;
			ViewStage();
		}

		private void GRD_STAGE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					ClearStage(e.Item.Cells[0].Text,e.Item.Cells[1].Text,e.Item.Cells[2].Text,true);
					break;
			}
		}

		protected void BTN_SAVESTAGE_Click(object sender, System.EventArgs e)
		{
			if (LBL_TR_CODE.Text.Trim() != "")
			{
				conn.QueryString = "update RFTRACKLST set maxday='"+TXT_MAXDAY.Text+"' where tr_code='"+LBL_TR_CODE.Text+"'";
				conn.ExecuteNonQuery();
				ClearStage("","","",false);
				ViewStage();
			}
			GlobalTools.popMessage(this,"Request has been saved");
		}

		protected void ClearStage(string code, string desc, string maxday, bool sta)
		{
			if (maxday.Trim() == "&nbsp;") maxday = "";
			LBL_TR_CODE.Text = code;
			LBL_TR_DESC.Text = desc;
			TXT_MAXDAY.Text = maxday;
			TXT_MAXDAY.Enabled = sta;
		}

		protected void BTN_SAVEOTHER_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "update INITIAL set IN_GROUPADMIN='"+TXT_IN_GROUPADMIN.Text
				+"', IN_GROUPANL='"+TXT_IN_GROUPANL.Text 
				+"', IN_GROUPVER='"+TXT_IN_GROUPVER.Text +"'";
			conn.ExecuteNonQuery();
			GlobalTools.popMessage(this,"Request has been saved");
		}

		protected void ViewAgent()
		{
			conn.QueryString = "select agencyid, agencyname, maxapp from TAGENCY where active='1' order by agencyname";
			conn.ExecuteQuery();
			try 
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				GRD_AGENT.DataSource = dt;
				GRD_AGENT.DataBind();
			} 
			catch 
			{
				GlobalTools.popMessage(this, "Error Grid !");
				return;
			}
		}

		private void GRD_AGENT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					ClearAgent(e.Item.Cells[0].Text,e.Item.Cells[1].Text,e.Item.Cells[2].Text,true);
					break;
			}
		}

		protected void ClearAgent(string code, string desc, string maxapp, bool sta)
		{
			if (maxapp.Trim() == "&nbsp;") maxapp = "";
			LBL_AGENTID.Text = code;
			LBL_AGENTNAME.Text = desc;
			TXT_MAXAPP.Text = maxapp;
			TXT_MAXAPP.Enabled = sta;
		}

		protected void BTN_AGENT_Click(object sender, System.EventArgs e)
		{
			if (LBL_AGENTID.Text.Trim() != "")
			{
				conn.QueryString = "update TAGENCY set maxapp='"+TXT_MAXAPP.Text+"' where agencyid='"+LBL_AGENTID.Text+"'";
				conn.ExecuteNonQuery();
				ClearAgent("","","",false);
				ViewAgent();
			}
			GlobalTools.popMessage(this,"Request has been saved");
		}

		protected void BTN_SAVEAGENT_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "update INITIAL set IN_MAXDAYAGENTJB='"+TXT_IN_MAXDAYAGENTJB.Text + "' " +
				", IN_MAXDAYAGENTLJB='"+TXT_IN_MAXDAYAGENTLJB.Text + "'"; 
			conn.ExecuteNonQuery();
			GlobalTools.popMessage(this,"Request has been saved");
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			//GlobalTools.NewWindow(this,"ParamSetupZipcode.aspx","SetupZipcodeTable",true,true,700,500);
			Response.Write("<script language='javascript'>window.open('ParamSetupZipcode.aspx?moduleid=" + Request.QueryString["moduleid"] +"&regno=" + Request.QueryString["regno"]+
				"','SetupZipcodeTable','status=no,scrollbars=no,width=700,height=500');</script>");
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}
}
