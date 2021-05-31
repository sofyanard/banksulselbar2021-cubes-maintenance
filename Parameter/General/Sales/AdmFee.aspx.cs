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
	/// Summary description for AdmFee.
	/// </summary>
	public partial class AdmFee : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=SALESMANDIRI;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		//protected string dbConsumer = ConfigurationSettings.AppSettings["dbConsumer"];
		protected Tools tools = new Tools();
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				DDL_AREA.Items.Add(new ListItem("--select--", ""));
				conncc.QueryString="select AREA_ID, AREA_NAME from Area";// where ACTIVE='1'";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_AREA.Items.Add(new ListItem(conncc.GetFieldValue(i,1),conncc.GetFieldValue(i,0)));
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
			this.LBL_DB_NAMA.Text = conn.GetFieldValue("DB_NAMA");
			this.LBL_DB_IP.Text   = conn.GetFieldValue("DB_IP");
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
			conncc.QueryString="select Min_Val, Max_Val, Currency, _Value, AF_CODE, a.Area_ID, AREA_NAME "+
				"from Administration_fee a "+
				"left join AREA b on a.AREA_ID=b.AREA_ID where a.ACTIVE='1'";
				//"left join "+dbConsumer+".dbo.AREA b on a.AREA_ID=b.AREA_ID where a.ACTIVE='1'";
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
			//SMEDEV2
			conncc.QueryString="select Min_Val, Max_Val, Currency, _Value, AF_CODE, a.Area_ID, AREA_NAME, SEQ_ID, "+
				"STATUS,case when STATUS = '1' then 'Insert' when STATUS = '0' then 'Update' "+
				"else 'Delete' end STATUS1 "+
				"from PENDING_SALESCOM_ADMFEE a "+
				"left join AREA b on a.AREA_ID=b.AREA_ID ";
				//"left join "+dbConsumer+".dbo.AREA b on a.AREA_ID=b.AREA_ID ";
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

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex=e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex=e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE.Text="1";
					DDL_AREA.SelectedValue = e.Item.Cells[0].Text;
					TXT_CODE1.Text = e.Item.Cells[2].Text;
					TXT_START_NUM.Text = e.Item.Cells[3].Text;
					TXT_END_NUM.Text = e.Item.Cells[4].Text;
					TXT_CURRENCY.Text = e.Item.Cells[5].Text;
					TXT_VALUE.Text = e.Item.Cells[6].Text;
					LBL_JENIS.Text = "edit";
					LBL_CODE.Text = e.Item.Cells[2].Text;
					LBL_SEQ_ID.Text=""; //update dari Existing Ga ada SeqId-nya...
					break;
				case "delete":
					//SMEDEV2
					//LBL_SAVE_MODE.Text="2";
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_ADMFEE where AF_Code='"+e.Item.Cells[2].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_ADMFEE set STATUS='2' where AF_Code='"+e.Item.Cells[2].Text.Trim()+"'";
						conncc.ExecuteQuery();
						//GlobalTools.popMessage(this,"SEQ_ID is Existing...");
						//return;
					}
					else
					{
						conncc.QueryString="select isnull(max(cast(SEQ_ID as integer)),0)+1 SeqId from PENDING_SALESCOM_ADMFEE";
						conncc.ExecuteQuery();
						this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SeqId");
						float valuenya = float.Parse(e.Item.Cells[6].Text.ToString());
																				
						conncc.QueryString="PARAM_SALESCOM_ADMFEE_MAKER '2','"+
							LBL_SEQ_ID.Text +"','"+ e.Item.Cells[3].Text +"','"+
							e.Item.Cells[4].Text +"','"+ e.Item.Cells[5].Text +"','"+
							valuenya +"','"+e.Item.Cells[0].Text+"','"+
							e.Item.Cells[2].Text+"','1'";
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
					DDL_AREA.SelectedValue = e.Item.Cells[0].Text;
					TXT_CODE1.Text = e.Item.Cells[3].Text;
					TXT_START_NUM.Text = e.Item.Cells[4].Text;
					TXT_END_NUM.Text = e.Item.Cells[5].Text;
					TXT_CURRENCY.Text = e.Item.Cells[6].Text;
					TXT_VALUE.Text = e.Item.Cells[7].Text;
					LBL_JENIS.Text = "edit";
					LBL_CODE.Text = e.Item.Cells[3].Text;
					LBL_SEQ_ID.Text=e.Item.Cells[1].Text;
					LBL_SAVE_MODE.Text=e.Item.Cells[10].Text;
					break;
				case "delete":
					//SMEDEV2
					string seqid=e.Item.Cells[1].Text;
					string code=e.Item.Cells[3].Text;
					conncc.QueryString="Delete from PENDING_SALESCOM_ADMFEE where SEQ_ID='"+seqid+"' and AF_CODE='"+code+"'"; 
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
			DDL_AREA.SelectedValue = "";
			TXT_CODE1.Text = "";
			TXT_START_NUM.Text = "";
			TXT_END_NUM.Text = "";
			TXT_CURRENCY.Text = "";
			TXT_VALUE.Text = "";
			LBL_JENIS.Text = "";
			LBL_CODE.Text = "";
			LBL_SEQ_ID.Text="";
			LBL_SAVE_MODE.Text="1";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BlankEntry();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int nilai1=int.Parse(TXT_START_NUM.Text);
			int nilai2=int.Parse(TXT_END_NUM.Text);
			if (nilai1 > nilai2)
			{
				GlobalTools.popMessage(this,"Start Number must less than End Number...");
				return;
			}
			if (LBL_SEQ_ID.Text == "" && LBL_CODE.Text=="") //input baru
			{
				//SALESMANDIRI
				conncc.QueryString="select isnull(max(cast(AF_CODE as integer)),0)+1 AfCode from Administration_Fee";
				conncc.ExecuteQuery();
				this.LBL_SEQ_ID.Text = conncc.GetFieldValue("AfCode");
				string mCode = kar(4,LBL_SEQ_ID.Text.ToString());
				float valuenya = float.Parse(TXT_VALUE.Text);

				conncc.QueryString="PARAM_SALESCOM_ADMFEE_MAKER '1','"+ 
					LBL_SEQ_ID.Text +"','"+ TXT_START_NUM.Text +"','"+
					TXT_END_NUM.Text +"','"+ TXT_CURRENCY.Text +"','"+
					valuenya +"','"+ DDL_AREA.SelectedValue +"','"+
					mCode+"','1'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			if (LBL_JENIS.Text=="edit") //edit
			{
				if (LBL_SEQ_ID.Text != "")//Edit dari DGR_REQUEST
				{
					float valuenya = float.Parse(TXT_VALUE.Text);
					conncc.QueryString="PARAM_SALESCOM_ADMFEE_MAKER '"+LBL_SAVE_MODE.Text+"','"+ 
						LBL_SEQ_ID.Text +"','"+ TXT_START_NUM.Text +"','"+
						TXT_END_NUM.Text +"','"+ TXT_CURRENCY.Text +"','"+
						valuenya +"','"+ DDL_AREA.SelectedValue +"','"+
						TXT_CODE1.Text+"','1'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
				else if (LBL_SEQ_ID.Text == "") //Edit dari DGR_EXISTING
				{
					conncc.QueryString="select * from PENDING_SALESCOM_ADMFEE where AF_Code='"+LBL_CODE.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//get seq_id
						conncc.QueryString="select isnull(max(cast(SEQ_ID as integer)),0)+1 SeqId from PENDING_SALESCOM_ADMFEE";
						conncc.ExecuteQuery();
						this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SeqId");
					}
					else
					{
						this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
					}
					float valuenya=float.Parse(TXT_VALUE.Text);

					conncc.QueryString="PARAM_SALESCOM_ADMFEE_MAKER '0','"+ 
						LBL_SEQ_ID.Text +"','"+ TXT_START_NUM.Text +"','"+
						TXT_END_NUM.Text +"','"+ TXT_CURRENCY.Text +"','"+
						valuenya +"','"+ DDL_AREA.SelectedValue +"','"+
						TXT_CODE1.Text+"','1'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
			}
			LBL_SAVE_MODE.Text="1";
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../SalesParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	
	}
}
