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
	/// Summary description for Base.
	/// </summary>
	public partial class Base : System.Web.UI.Page
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
				DDL_AGENT.Items.Add(new ListItem("--Select--",""));
				conncc.QueryString = " select AgenType_ID, AgenType_Desc from Agenttype";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_AGENT.Items.Add(new ListItem(conncc.GetFieldValue(i,1),conncc.GetFieldValue(i,0)));
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

		private void ViewExistingData()
		{
			//SALESMANDIRI
			conncc.QueryString="select * from VW_PARAM_SALESCOM_CALCULATION_BASE ";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_EXISTING.DataSource = dt;
			this.DGR_EXISTING.DataBind();
			for (int i = 0; i < this.DGR_EXISTING.Items.Count; i++)
			{
				this.DGR_EXISTING.Items[i].Cells[1].Text = (i+1).ToString();
			}
		}

		private void ViewPendingData()
		{
			conncc.QueryString = "Select * from VW_PARAM_SALESCOM_PENDING_CALCULATION_BASE ";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			DGR_REQUEST.DataSource = dt;
			this.DGR_REQUEST.DataBind();
			for (int i = 0; i < this.DGR_REQUEST.Items.Count; i++)
			{
				this.DGR_REQUEST.Items[i].Cells[1].Text = (i+1).ToString();
			}
		}

		private void BlankEntry()
		{
			DDL_AGENT.SelectedValue="";
			DDL_AGENT.Enabled = true;
			TXT_COEF.Text="";
			TXT_MAX_POINT.Text="";
			TXT_DISBURSE_POINT.Text="";
			TXT_MAX_INCENTIVE.Text="";
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
					DDL_AGENT.SelectedValue = e.Item.Cells[0].Text;
					DDL_AGENT.Enabled = false;
					TXT_COEF.Text = e.Item.Cells[3].Text;
					TXT_MAX_POINT.Text = e.Item.Cells[4].Text;
					TXT_DISBURSE_POINT.Text = e.Item.Cells[5].Text;
					TXT_MAX_INCENTIVE.Text = e.Item.Cells[6].Text;
					LBL_JENIS.Text = "edit";
					LBL_ID.Text=e.Item.Cells[0].Text;
					LBL_SEQ_ID.Text="";
					break;
				case "delete":
					//LBL_SAVE_MODE.Text="2";
					//get seq_id
					conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_CALCULATION_BASE";
					conncc.ExecuteQuery();
					string seq_id = conncc.GetFieldValue("SEQ_ID");
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_CALCULATION_BASE where AGENTYPE_ID='"+e.Item.Cells[0].Text.Trim()+"'"; 
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_CALCULATION_BASE set STATUS='2' where AGENTYPE_ID='"+e.Item.Cells[0].Text.Trim()+"'"; 
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_CALCULATION_BASE_MAKER '2','"+
							e.Item.Cells[0].Text +"','"+e.Item.Cells[3].Text+"','"+
							e.Item.Cells[4].Text +"','"+e.Item.Cells[5].Text+"','"+
							e.Item.Cells[6].Text +"','1','"+seq_id+"'";
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
					DDL_AGENT.SelectedValue = e.Item.Cells[0].Text;
					DDL_AGENT.Enabled = false;
					TXT_COEF.Text = e.Item.Cells[3].Text;
					TXT_MAX_POINT.Text = e.Item.Cells[4].Text;
					TXT_DISBURSE_POINT.Text = e.Item.Cells[5].Text;
					TXT_MAX_INCENTIVE.Text = e.Item.Cells[6].Text;
					LBL_JENIS.Text = "edit";
					LBL_ID.Text=e.Item.Cells[0].Text;
					LBL_SEQ_ID.Text=e.Item.Cells[9].Text;
					LBL_SAVE_MODE.Text=e.Item.Cells[10].Text;
					break;
				
				case "delete":
					//SMEDEV2
					string AGEN_ID = e.Item.Cells[0].Text;
					string seq_id = e.Item.Cells[9].Text;
					conncc.QueryString="Delete from PENDING_SALESCOM_CALCULATION_BASE "+
						"where AGENTYPE_ID='"+AGEN_ID+"'and SEQ_ID='"+seq_id+"'";
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
			if(TXT_COEF.Text=="" )
			{
				GlobalTools.popMessage(this,"Allowance Coeffient Tidak Boleh Kosong");
				return;
			}
			if(TXT_MAX_POINT.Text=="")
			{
				GlobalTools.popMessage(this,"Maximum Point Target Tidak Boleh Kosong");
				return;
			}
			if (LBL_JENIS.Text=="" && LBL_ID.Text=="") //input baru
			{
				//SalesMandiri-- Membandingkan Agentype_ID baru dengan yang ada di Tabel Existing
				conncc.QueryString="select AGENTYPE_ID from CALCULATION_BASE";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					if(DDL_AGENT.SelectedValue.ToString().Trim()==conncc.GetFieldValue(i,0).ToString().Trim())
					{
						GlobalTools.popMessage(this,"Agen is existing...");
						return;
					}
				}

				//SMEDEV2-- Membandingkan Agentype_ID dengan yang ada di Tabel Pending
				conncc.QueryString="select AGENTYPE_ID from PENDING_SALESCOM_CALCULATION_BASE";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					if(DDL_AGENT.SelectedValue.ToString().Trim()==conncc.GetFieldValue(i,0).ToString().Trim())
					{
						GlobalTools.popMessage(this,"Agen is existing...");
						return;
					}
				}

				//get seq_id
				conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_CALCULATION_BASE";
				conncc.ExecuteQuery();
				LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");

				conncc.QueryString="PARAM_SALESCOM_CALCULATION_BASE_MAKER '1','"+
					DDL_AGENT.SelectedValue +"','"+TXT_COEF.Text+"','"+
					TXT_MAX_POINT.Text+"','"+TXT_DISBURSE_POINT.Text+"','"+TXT_MAX_INCENTIVE.Text+"','1','"+
					LBL_SEQ_ID.Text+"'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			if (LBL_JENIS.Text=="edit" && LBL_ID.Text!="") //edit
			{
				if(LBL_SEQ_ID.Text!="")//edit dari Request
				{
					conncc.QueryString="PARAM_SALESCOM_CALCULATION_BASE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						DDL_AGENT.SelectedValue +"','"+TXT_COEF.Text+"','"+
						TXT_MAX_POINT.Text+"','"+TXT_DISBURSE_POINT.Text+"','"+TXT_MAX_INCENTIVE.Text+"','1','"+
						LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
				else if(LBL_SEQ_ID.Text=="")//edit dari Existing
				{
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_CALCULATION_BASE where AGENTYPE_ID='"+DDL_AGENT.SelectedValue+"'"; 
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//get seq_id
						conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_CALCULATION_BASE";
						conncc.ExecuteQuery();
						LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
					}
					else
					{
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					
					conncc.QueryString="PARAM_SALESCOM_CALCULATION_BASE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						DDL_AGENT.SelectedValue +"','"+TXT_COEF.Text+"','"+
						TXT_MAX_POINT.Text+"','"+TXT_DISBURSE_POINT.Text+"','"+TXT_MAX_INCENTIVE.Text+"','1','"+
						LBL_SEQ_ID.Text+"'";
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
