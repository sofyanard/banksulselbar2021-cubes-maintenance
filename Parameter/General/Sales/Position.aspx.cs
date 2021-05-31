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
	/// Summary description for Position.
	/// </summary>
	public partial class Position : System.Web.UI.Page
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
				string agenID="";
				conncc.QueryString="select isnull(max(cast(Agentype_ID as integer)),0)+1 agentID from agenttype";
				conncc.ExecuteQuery();
				agenID=conncc.GetFieldValue("agentID");
				if(conncc.GetFieldValue("agentID").Length == 1)
				{
					agenID=kar(2,conncc.GetFieldValue("agentID"));
				}
				TXT_CODE.Text = agenID;
				
				DDL_UPLINER.Items.Add(new ListItem("--Select--",""));
				conncc.QueryString = "select Agentype_ID,Agentype_desc from agenttype";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_UPLINER.Items.Add(new ListItem(conncc.GetFieldValue(i,1),conncc.GetFieldValue(i,0)));
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
			conncc.QueryString="select * from VW_PARAM_SALESCOM_AGENTTYPE";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_EXISTING.DataSource = dt;
			try
			{
				this.DGR_EXISTING.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.CurrentPageIndex-1;
					this.DGR_EXISTING.DataBind();
				}
				catch{}
			}
		}

		private void ViewPendingData()
		{
			conncc.QueryString="Select * from VW_PARAM_SALESCOM_PENDING_AGENTTYPE";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_REQUEST.DataSource = dt;
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

		string kar(int pjg,string str)
		{
			string tmpkar="";
			try
			{
				if (pjg >=str.Length )
				{
					int panjang = pjg -str.Length ;
					string tstr = str.ToString().Trim();
					for (int i=0; i<panjang; i++)
					{
						tstr = "0"+tstr;
					}
					tmpkar=tstr;
				}
				else 
				{
					tmpkar ="Wrong...!";
				}
				return tmpkar;
			}
			catch(Exception e){return "Wrong...!";};
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

		private void BlankEntry()
		{
			string agenID="";
			conncc.QueryString="select isnull(max(cast(Agentype_ID as integer)),0)+1 agentID from agenttype";
			conncc.ExecuteQuery();
			agenID=conncc.GetFieldValue("agentID");
			if(conncc.GetFieldValue("agentID").Length == 1)
			{
				agenID=kar(2,conncc.GetFieldValue("agentID"));
			}
			TXT_CODE.Text = agenID;
			//TXT_CODE.Enabled=true;
			TXT_DESC.Text = "";
			DDL_UPLINER.SelectedValue = "";
			TXT_PERIODE_PROMO.Text = "";
			LBL_JENIS.Text = "";
			LBL_ID.Text="";
			LBL_SAVE_MODE.Text="1";
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_CODE.Text = cleansText(e.Item.Cells[0].Text);
					TXT_CODE.Enabled=false;
					TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					try
					{
						DDL_UPLINER.SelectedValue = cleansText(e.Item.Cells[2].Text);
						TXT_PERIODE_PROMO.Text = cleansText(e.Item.Cells[3].Text);
					}
					catch{}
					
					LBL_JENIS.Text = "edit";
					LBL_ID.Text = cleansText(e.Item.Cells[0].Text);
					LBL_SAVE_MODE.Text="0";
					LBL_SEQ_ID.Text="";
					break;
				
				case "delete":
					//LBL_SAVE_MODE.Text="2";
					//SMEDEV2
					//get seq_id
					conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_AGENTTYPE";
					conncc.ExecuteQuery();
					string seq_id = conncc.GetFieldValue("SEQ_ID");
					
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_AGENTTYPE where AGENTYPE_ID='"+e.Item.Cells[0].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_AGENTTYPE set STATUS='2' where AGENTYPE_ID='"+e.Item.Cells[0].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_AGENTTYPE_MAKER '2','"+
							cleansText(e.Item.Cells[0].Text) +"','"+cleansText(e.Item.Cells[1].Text)+"','"+
							cleansText(e.Item.Cells[2].Text) +"','','"+cleansText(e.Item.Cells[3].Text)+"','1','"+seq_id+"'";
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
					string status = e.Item.Cells[7].Text;
					if(status=="2")
					{
						break;
					}
					TXT_CODE.Text = cleansText(e.Item.Cells[0].Text);
					TXT_CODE.Enabled = false;
					TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					try
					{
						DDL_UPLINER.SelectedValue = cleansText(e.Item.Cells[2].Text);
						TXT_PERIODE_PROMO.Text = cleansText(e.Item.Cells[3].Text);
					}
					catch{}
					
					LBL_JENIS.Text = "edit";
					LBL_ID.Text = cleansText(e.Item.Cells[0].Text);
					LBL_SEQ_ID.Text = cleansText(e.Item.Cells[6].Text);
					LBL_SAVE_MODE.Text = cleansText(e.Item.Cells[7].Text);
					break;
				
				case "delete":
					//SMEDEV2
					string AGEN_ID = e.Item.Cells[0].Text;
					string seq_id = e.Item.Cells[6].Text;
					conncc.QueryString="Delete from PENDING_SALESCOM_AGENTTYPE "+
						"where AGENTYPE_ID='"+AGEN_ID+"' and SEQ_ID='"+seq_id+"'";
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					ViewPendingData();
					BlankEntry();
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BlankEntry();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_JENIS.Text=="" && LBL_ID.Text=="") //input baru
			{
				//SMEDEV2-- Membandingkan MN_ID baru dengan yang ada di Tabel Pending
				conncc.QueryString="select AGENTYPE_ID from PENDING_SALESCOM_AGENTTYPE";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					if(TXT_CODE.Text.ToString().Trim()==conncc.GetFieldValue(i,0).ToString().Trim())
					{
						int IDnya=int.Parse(TXT_CODE.Text.ToString().Trim())+1;
						TXT_CODE.Text = kar(2,IDnya.ToString().Trim());
					}
				}
				string periode="";
				if(TXT_PERIODE_PROMO.Text!="")
				{
					periode=TXT_PERIODE_PROMO.Text;
				}
				else
				{
					periode="";
				}

				//get seq_id
				conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_AGENTTYPE";
				conncc.ExecuteQuery();
				LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");

				conncc.QueryString="PARAM_SALESCOM_AGENTTYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
					TXT_CODE.Text +"','"+TXT_DESC.Text+"','"+
					DDL_UPLINER.SelectedValue+"','','"+periode+"','1','"+LBL_SEQ_ID.Text+"'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			if (LBL_JENIS.Text=="edit" && LBL_ID.Text!="") //edit
			{
				string periode="";
				if(TXT_PERIODE_PROMO.Text!="")
				{
					periode=TXT_PERIODE_PROMO.Text;
				}
				else
				{
					periode="";
				}

				if(LBL_SEQ_ID.Text!="")//edit dari Request
				{
					conncc.QueryString="PARAM_SALESCOM_AGENTTYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						TXT_CODE.Text +"','"+TXT_DESC.Text+"','"+
						cleansText(DDL_UPLINER.SelectedValue)+"','','"+periode+"','1','"+LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
				else if(LBL_SEQ_ID.Text=="")//edit dari Existing
				{
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_AGENTTYPE where AGENTYPE_ID='"+TXT_CODE.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//get seq_id
						conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_AGENTTYPE";
						conncc.ExecuteQuery();
						LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
					}
					else
					{
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					
					conncc.QueryString="PARAM_SALESCOM_AGENTTYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						TXT_CODE.Text +"','"+TXT_DESC.Text+"','"+
						cleansText(DDL_UPLINER.SelectedValue)+"','','"+periode+"','1','"+LBL_SEQ_ID.Text+"'";
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
