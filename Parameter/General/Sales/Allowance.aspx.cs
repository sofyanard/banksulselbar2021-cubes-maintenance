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
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.Sales
{
	/// <summary>
	/// Summary description for Allowance.
	/// </summary>
	public partial class Allowance : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=SALESMANDIRI;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				DDL_AGENT_TYPE.Items.Add(new ListItem("--Select--",""));
				conncc.QueryString ="select * from AGENTTYPE";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_AGENT_TYPE.Items.Add(new ListItem(conncc.GetFieldValue(i,1),conncc.GetFieldValue(i,0)));

				}

				DDL_LEVEL.Items.Add(new ListItem("--Select--",""));
				conncc.QueryString="select  *, a.LEVEL_DESC+' '+b.AGENTYPE_DESC as LEVEL_AGENT "+
					"from LEVEL a left join AGENTTYPE b on a.AGENTYPE_ID = b.AGENTYPE_ID "+ 
					"order by a.AGENTYPE_ID, LEVEL_CODE";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_LEVEL.Items.Add(new ListItem(conncc.GetFieldValue(i,11),conncc.GetFieldValue(i,0)));
				}
				
				ViewExistingData();
				ViewPendingData();
			}
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
		}

		private void SetDBConn2()
		{
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			string DB_NAMA = conn.GetFieldValue("DB_NAMA");
			string DB_IP = conn.GetFieldValue("DB_IP");
			string DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncc = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
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
			this.DGR_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_ItemCommand);
			this.DGR_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

		}
		#endregion

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ViewExistingData()
		{
			conncc.QueryString="Select * from VW_PARAM_SALESCOM_AGENT_TARGET";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			DGR_EXISTING.DataSource = dt;
			try
			{
				this.DGR_EXISTING.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.CurrentPageIndex - 1;
					this.DGR_EXISTING.DataBind();
				}
				catch{}
			}
		}

		private void ViewPendingData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_AGENT_TARGET";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			DGR_REQUEST.DataSource = dt;
			try
			{
				this.DGR_REQUEST.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.CurrentPageIndex-1;
					this.DGR_REQUEST.DataBind();
				}
				catch{}
			}
		}

		private void BlankEntry()
		{
			DDL_AGENT_TYPE.SelectedValue="";
			DDL_AGENT_TYPE.Enabled=true;
			DDL_LEVEL.SelectedValue="";
			DDL_LEVEL.Enabled=true;
			TXT_ALLOWANCE.Text="";
			TXT_ALLOWANCE_TARGET.Text="";
			TXT_PHONE_ALLOWANCE.Text="";
			TXT_DISBURSE_TARGET.Text="";
			LBL_JENIS.Text="";
			LBL_ID.Text="";
			LBL_SEQ_ID.Text="";
			LBL_SAVE_MODE.Text="1";
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE.Text="0";
					DDL_AGENT_TYPE.SelectedValue = cleansText(e.Item.Cells[7].Text);
					DDL_AGENT_TYPE.Enabled=false;
					DDL_LEVEL.SelectedValue = cleansText(e.Item.Cells[8].Text);
					DDL_LEVEL.Enabled=false;
					TXT_ALLOWANCE.Text = cleansText(e.Item.Cells[2].Text);
					TXT_ALLOWANCE_TARGET.Text = cleansText(e.Item.Cells[3].Text);
					TXT_PHONE_ALLOWANCE.Text = cleansText(e.Item.Cells[4].Text);
					TXT_DISBURSE_TARGET.Text = cleansText(e.Item.Cells[5].Text);
					LBL_JENIS.Text = "edit";
					LBL_ID.Text = cleansText(e.Item.Cells[7].Text);
					LBL_CODE.Text = cleansText(e.Item.Cells[8].Text);
					LBL_SEQ_ID.Text="";
					break;
				case "delete":
					//LBL_SAVE_MODE.Text="2";
					//SMEDEV2
					//get seq_id
					conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_AGENT_TARGET";
					conncc.ExecuteQuery();
					string seq_id = conncc.GetFieldValue("SEQ_ID");
					
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_AGENT_TARGET where AGENT_TYPE='"+e.Item.Cells[7].Text.Trim()+"' and "+
									 "LEVEL_CODE='"+e.Item.Cells[8].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_AGENT_TARGET set STATUS='2' where AGENT_TYPE='"+e.Item.Cells[7].Text.Trim()+"' and "+
							"LEVEL_CODE='"+e.Item.Cells[8].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_AGENT_TARGET_MAKER '2','"+
							e.Item.Cells[7].Text +"','"+ e.Item.Cells[8].Text +"','"+
							e.Item.Cells[2].Text +"','"+ e.Item.Cells[3].Text +"','','','"+
							e.Item.Cells[4].Text +"','"+ e.Item.Cells[5].Text +"','1','"+seq_id+"'";
						try
						{
							conncc.ExecuteQuery();
						}
						catch { GlobalTools.popMessage (this,"Error...");}
					}
					ViewPendingData();
					BlankEntry();
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[10].Text;
					if(status=="2")
					{
						break;
					}
					DDL_AGENT_TYPE.SelectedValue = cleansText(e.Item.Cells[8].Text);
					DDL_AGENT_TYPE.Enabled=false;
					DDL_LEVEL.SelectedValue = cleansText(e.Item.Cells[9].Text);
					DDL_LEVEL.Enabled=false;
					TXT_ALLOWANCE.Text = cleansText(e.Item.Cells[2].Text);
					TXT_ALLOWANCE_TARGET.Text = cleansText(e.Item.Cells[3].Text);
					TXT_PHONE_ALLOWANCE.Text = cleansText(e.Item.Cells[4].Text);
					TXT_DISBURSE_TARGET.Text = cleansText(e.Item.Cells[5].Text);
					LBL_JENIS.Text = "edit";
					LBL_ID.Text = cleansText(e.Item.Cells[8].Text);
					LBL_CODE.Text = cleansText(e.Item.Cells[9].Text);
					LBL_SAVE_MODE.Text=cleansText(e.Item.Cells[10].Text);
					LBL_SEQ_ID.Text=cleansText(e.Item.Cells[11].Text);
					break;
				case "delete":
					//SMEDEV2
					conncc.QueryString="Delete from PENDING_SALESCOM_AGENT_TARGET where AGENT_TYPE='"+e.Item.Cells[8].Text+"' and LEVEL_CODE='"+
						e.Item.Cells[9].Text+"' and SEQ_ID='"+e.Item.Cells[11].Text+"'";
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					ViewPendingData();
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BlankEntry();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_JENIS.Text=="" && LBL_ID.Text == "" && LBL_CODE.Text =="") //input baru
			{
				//SALESMANDIRI--- Membandingkan ID baru dengan yang ada di Existing
				string agentype = DDL_AGENT_TYPE.SelectedValue.Trim();
				string level = DDL_LEVEL.SelectedValue.Trim();
				conncc.QueryString="select AGENT_TYPE,LEVEL_CODE from AGENT_TARGET";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					if(agentype == conncc.GetFieldValue(i,0).ToString().Trim() && level==conncc.GetFieldValue(i,1).ToString().Trim())
					{
						GlobalTools.popMessage(this,"Agent Type & Level is Existing...");
						return;
					}
				}

				//SMEDEV2---Membandingkan ID baru dengan yang ada di Pending
				conncc.QueryString="select AGENT_TYPE,LEVEL_CODE from PENDING_SALESCOM_AGENT_TARGET";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					if(agentype == conncc.GetFieldValue(i,0).ToString().Trim() && level==conncc.GetFieldValue(i,1).ToString().Trim())
					{
						GlobalTools.popMessage(this,"Agent Type & Level is Existing...");
						return;
					}
				}
				
				//get seq_id...
				conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_AGENT_TARGET";
				conncc.ExecuteQuery();
				LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");

				conncc.QueryString="PARAM_SALESCOM_AGENT_TARGET_MAKER '1','"+
					DDL_AGENT_TYPE.SelectedValue +"','"+ DDL_LEVEL.SelectedValue +"','"+
					TXT_ALLOWANCE.Text +"','"+ TXT_ALLOWANCE_TARGET.Text +"','','','"+
					TXT_PHONE_ALLOWANCE.Text +"','"+ TXT_DISBURSE_TARGET.Text +"','1','"+LBL_SEQ_ID.Text+"'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			else if (LBL_JENIS.Text=="edit" && LBL_ID.Text != "") //edit
			{
				if(LBL_SEQ_ID.Text!="")//edit dari Request
				{
					conncc.QueryString="PARAM_SALESCOM_AGENT_TARGET_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						DDL_AGENT_TYPE.SelectedValue +"','"+ DDL_LEVEL.SelectedValue +"','"+
						TXT_ALLOWANCE.Text +"','"+ TXT_ALLOWANCE_TARGET.Text +"','','','"+
						TXT_PHONE_ALLOWANCE.Text +"','"+ TXT_DISBURSE_TARGET.Text +"','1','"+LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
				else if(LBL_SEQ_ID.Text=="")//edit dari Existing
				{
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_AGENT_TARGET where AGENT_TYPE='"+DDL_AGENT_TYPE.SelectedValue+"' and LEVEL_CODE='"+
									  DDL_LEVEL.SelectedValue+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//get seq_id
						conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_AGENT_TARGET";
						conncc.ExecuteQuery();
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					else
					{
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					
					conncc.QueryString="PARAM_SALESCOM_AGENT_TARGET_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						DDL_AGENT_TYPE.SelectedValue +"','"+ DDL_LEVEL.SelectedValue +"','"+
						TXT_ALLOWANCE.Text +"','"+ TXT_ALLOWANCE_TARGET.Text +"','','','"+
						TXT_PHONE_ALLOWANCE.Text +"','"+ TXT_DISBURSE_TARGET.Text +"','1','"+LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
			}
			LBL_SAVE_MODE.Text="1";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../SalesParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

	}
}

