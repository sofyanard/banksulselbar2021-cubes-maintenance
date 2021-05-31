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
	/// Summary description for Level.
	/// </summary>
	public partial class Level : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=SALESMANDIRI;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				DDL_TYPEID.Items.Add(new ListItem("--Select--",""));
				conncc.QueryString="select Agentype_ID,Agentype_desc from AgentType";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_TYPEID.Items.Add(new ListItem(conncc.GetFieldValue(i,1),conncc.GetFieldValue(i,0)));
				}

				ViewExistingData();
				ViewPendingData();

				conncc.QueryString="select isnull(max(cast(LEVEL_CODE as integer)),0)+1 code from LEVEL";
				conncc.ExecuteQuery();
				string code=conncc.GetFieldValue("code");
				TXT_LEVEL_CODE.Text = code;
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

		private void ViewExistingData()
		{
			//SalesMandiri
			conncc.QueryString="select * from VW_PARAM_SALESCOM_LEVEL order by LEVEL_CODE";
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
			//SMEDEV2
			conncc.QueryString="select *,convert(int,LEVEL_CODE) LEVEL_CODE from VW_PARAM_SALESCOM_PENDING_LEVEL order by LEVEL_CODE";
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

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex= e.NewPageIndex;
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
					DDL_TYPEID.SelectedValue = e.Item.Cells[1].Text;
					DDL_TYPEID.Enabled=false;
					TXT_DESC.Text = e.Item.Cells[2].Text;
					TXT_LEVEL_CODE.Text = e.Item.Cells[0].Text;
					TXT_LEVEL_CODE.Enabled=false;
					LBL_JENIS.Text = "edit";
					LBL_CODE.Text=e.Item.Cells[0].Text;
					LBL_SEQ_ID.Text="";
					break;
				case "delete":
					//LBL_SAVE_MODE.Text="2";
					//SMEDEV2
					//get seq_id
					conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_LEVEL";
					conncc.ExecuteQuery();
					string seq_id = conncc.GetFieldValue("SEQ_ID");
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_LEVEL where AGENTYPE_ID='"+e.Item.Cells[1].Text.Trim()+"' and "+
						"LEVEL_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_LEVEL set STATUS='2' where AGENTYPE_ID='"+e.Item.Cells[1].Text.Trim()+"' and "+
							"LEVEL_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_LEVEL_MAKER '2','"+
							e.Item.Cells[0].Text +"','"+e.Item.Cells[1].Text+"','"+
							e.Item.Cells[2].Text +"','','1','"+seq_id+"'";
						try
						{
							conncc.ExecuteQuery();
						}
						catch { GlobalTools.popMessage (this,"Error...");}
					}
					ViewPendingData();
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[6].Text;
					if(status=="2")
					{
						break;
					}
					DDL_TYPEID.SelectedValue = e.Item.Cells[1].Text;
					DDL_TYPEID.Enabled=false;
					TXT_DESC.Text = e.Item.Cells[2].Text;
					TXT_LEVEL_CODE.Text = e.Item.Cells[0].Text;
					TXT_LEVEL_CODE.Enabled=false;
					LBL_JENIS.Text = "edit";
					LBL_CODE.Text=e.Item.Cells[0].Text;
					LBL_SEQ_ID.Text=e.Item.Cells[5].Text;
					LBL_SAVE_MODE.Text=e.Item.Cells[6].Text;
					break;
				case "delete":
					//SMEDEV2
					string code = e.Item.Cells[0].Text;
					string agentype = e.Item.Cells[1].Text;
					string seq_id = e.Item.Cells[5].Text;
					conncc.QueryString="Delete from PENDING_SALESCOM_LEVEL "+
						"where LEVEL_CODE='"+code+"' and AGENTYPE_ID='"+agentype+"' and "+
						"SEQ_ID='"+seq_id+"'"; 
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					ViewPendingData();
					break;
			}
		}

		private void BlankEntry()
		{
			conncc.QueryString="select isnull(max(cast(LEVEL_CODE as integer)),0)+1 code from LEVEL";
			conncc.ExecuteQuery();
			string code=conncc.GetFieldValue("code");
			TXT_LEVEL_CODE.Text = code;
			DDL_TYPEID.SelectedValue ="";
			DDL_TYPEID.Enabled=true;
			TXT_LEVEL_CODE.Enabled=true;
			TXT_DESC.Text = "";
			//TXT_LEVEL_CODE.Text = "";
			LBL_JENIS.Text = "";
			LBL_SEQ_ID.Text="";
			LBL_SAVE_MODE.Text="1";
			LBL_CODE.Text="";
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_JENIS.Text=="" && LBL_CODE.Text=="") //input baru
			{
				//cek tabel existing
				conncc.QueryString="select * from LEVEL where AGENTYPE_ID='"+DDL_TYPEID.SelectedValue+"' and "+
					"LEVEL_DESC='"+TXT_DESC.Text.ToString().Trim()+"'";
				conncc.ExecuteQuery();
				if(conncc.GetRowCount()!=0)
				{
					GlobalTools.popMessage(this,"Agent Type ID & Level Description is Existing..");
					return;
				}

				//SMEDEV2--- Membandingkan LEVEL_CODE baru dengan yang ada di Tabel Pending
				conncc.QueryString="Select LEVEL_CODE from PENDING_SALESCOM_LEVEL";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					if(TXT_LEVEL_CODE.Text.ToString().Trim() == conncc.GetFieldValue(i,0).ToString().Trim())
					{
						int ID = int.Parse(TXT_LEVEL_CODE.Text.ToString().Trim())+1;
						TXT_LEVEL_CODE.Text=ID.ToString();
					}
				}

				//get seq_id
				conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_LEVEL";
				conncc.ExecuteQuery();
				LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");

				conncc.QueryString="PARAM_SALESCOM_LEVEL_MAKER '"+LBL_SAVE_MODE.Text+"','"+
					TXT_LEVEL_CODE.Text +"','"+DDL_TYPEID.SelectedValue+"','"+TXT_DESC.Text+"','','1','"+LBL_SEQ_ID.Text+"'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			else if (LBL_JENIS.Text=="edit" && LBL_CODE.Text!="") //edit
			{
				if(LBL_SEQ_ID.Text!="")//edit dari Request
				{
					conncc.QueryString="PARAM_SALESCOM_LEVEL_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						LBL_CODE.Text +"','"+DDL_TYPEID.SelectedValue+"','"+TXT_DESC.Text+"','','1','"+LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
				else if(LBL_SEQ_ID.Text=="")//edit dari Existing
				{
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_LEVEL where LEVEL_CODE='"+TXT_LEVEL_CODE.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//get seq_id
						conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_LEVEL";
						conncc.ExecuteQuery();
						LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
					}
					else
					{
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					
					conncc.QueryString="PARAM_SALESCOM_LEVEL_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						LBL_CODE.Text +"','"+DDL_TYPEID.SelectedValue+"','"+TXT_DESC.Text+"','','1','"+LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
			}
			LBL_SAVE_MODE.Text="1";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BlankEntry();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../SalesParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}
}

