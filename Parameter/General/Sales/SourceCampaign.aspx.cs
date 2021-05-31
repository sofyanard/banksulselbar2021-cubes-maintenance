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
	/// Summary description for SourceCampaign.
	/// </summary>
	public partial class SourceCampaign : System.Web.UI.Page
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
				conncc.QueryString="select isnull(max(SC_ID), 0)+1 SC_ID from RFSOURCE_CAMPAIGN";
				conncc.ExecuteQuery();
				string IDnya=conncc.GetFieldValue("SC_ID");
				TXT_ID.Text = kar(2,IDnya);
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
			conncc.QueryString="select * from VW_PARAM_SALESCOM_RFSOURCE_CAMPAIGN";
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
			conncc.QueryString="Select * from VW_PARAM_SALESCOM_PENDING_RFSOURCE_CAMPAIGN";
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

		private void BlankEntry()
		{
			TXT_DESC.Text="";
			//if(LBL_JENIS.Text=="")
			//{
			conncc.QueryString="select isnull(max(SC_ID), 0)+1 SC_ID from RFSOURCE_CAMPAIGN";
			conncc.ExecuteQuery();
			string IDnya=conncc.GetFieldValue("SC_ID");
			TXT_ID.Text = kar(2,IDnya);
			//}
			//TXT_ID.Enabled=true;
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
					TXT_ID.Text=e.Item.Cells[2].Text;
					TXT_ID.Enabled=false;
					TXT_DESC.Text=e.Item.Cells[3].Text;
					LBL_JENIS.Text="edit";
					LBL_ID.Text=e.Item.Cells[2].Text;
					LBL_SEQ_ID.Text="";
					break;
				case "delete":
					//LBL_SAVE_MODE.Text="2";
					//SMEDEV2
					//Get Seq_id....
					conncc.QueryString="select isnull(max(Seq_ID), 0)+1 SEQ_ID from PENDING_SALESCOM_RFSOURCE_CAMPAIGN";
					conncc.ExecuteQuery();
					string seq_id=conncc.GetFieldValue("SEQ_ID");

					conncc.QueryString="select * from PENDING_SALESCOM_RFSOURCE_CAMPAIGN "+
						"where SC_ID='"+e.Item.Cells[2].Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!=0)
					{
						conncc.QueryString="update PENDING_SALESCOM_RFSOURCE_CAMPAIGN set STATUS='2' where SC_ID='"+e.Item.Cells[2].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_RFSOURCE_CAMPAIGN_MAKER '2','"+
							e.Item.Cells[2].Text +"','"+ e.Item.Cells[3].Text +"','"+seq_id+"','1'";
						try
						{
							conncc.ExecuteQuery();
						}
						catch { GlobalTools.popMessage (this,"Error on Store Procedure...");}
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
					string status = e.Item.Cells[6].Text;
					if(status=="2")
					{
						break;
					}
					LBL_SAVE_MODE.Text=e.Item.Cells[6].Text;
					TXT_ID.Text=e.Item.Cells[2].Text;
					TXT_ID.Enabled=false;
					TXT_DESC.Text=e.Item.Cells[3].Text;
					LBL_JENIS.Text="edit";
					LBL_ID.Text=e.Item.Cells[2].Text;
					LBL_SEQ_ID.Text=e.Item.Cells[0].Text;
					break;
				case "delete":
					//SMEDEV2
					string id=e.Item.Cells[2].Text;
					string seq_id = e.Item.Cells[0].Text;
					conncc.QueryString="Delete from PENDING_SALESCOM_RFSOURCE_CAMPAIGN where SC_ID='"+id+"'and SEQ_ID='"+seq_id+"'"; 
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
			if (LBL_ID.Text == "" && LBL_JENIS.Text=="" && LBL_SEQ_ID.Text=="") //input baru
			{
				//Get Seq_id....
				conncc.QueryString="select isnull(max(Seq_ID), 0)+1 SEQ_ID from PENDING_SALESCOM_RFSOURCE_CAMPAIGN";
				conncc.ExecuteQuery();
				LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");

				//SalesMandiri
				conncc.QueryString="select * from RFSOURCE_CAMPAIGN where SC_ID='"+TXT_ID.Text+"'";
				conncc.ExecuteQuery();
				if(conncc.GetRowCount()!=0)
				{
					GlobalTools.popMessage(this,"Source ID is Existing..");
					return;
				}
				//SMEDEV2
				conncc.QueryString="select SC_ID from PENDING_SALESCOM_RFSOURCE_CAMPAIGN ";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					if(TXT_ID.Text.Trim()==conncc.GetFieldValue(i,0).Trim())
					{
						int IDnya=int.Parse(TXT_ID.Text.Trim())+1;
						TXT_ID.Text = kar(2,IDnya.ToString().Trim());
					}
				}
				
				conncc.QueryString="PARAM_SALESCOM_RFSOURCE_CAMPAIGN_MAKER '"+LBL_SAVE_MODE.Text+"','"+ 
					TXT_ID.Text +"','"+ TXT_DESC.Text +"','"+LBL_SEQ_ID.Text+"','1'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			else if (LBL_JENIS.Text=="edit" && LBL_ID.Text!="") //edit
			{
				if (LBL_SEQ_ID.Text!="")//Edit dari DGR_REQUEST
				{
					//SALESMANDIRI
					conncc.QueryString="PARAM_SALESCOM_RFSOURCE_CAMPAIGN_MAKER '"+LBL_SAVE_MODE.Text+"','"+ 
						TXT_ID.Text +"','"+ TXT_DESC.Text +"','"+LBL_SEQ_ID.Text+"','1'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
				else if(LBL_SEQ_ID.Text=="")//edit dari Existing
				{
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_RFSOURCE_CAMPAIGN where SC_ID='"+TXT_ID.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//Get Seq_id....
						conncc.QueryString="select isnull(max(Seq_ID), 0)+1 SEQ_ID from PENDING_SALESCOM_RFSOURCE_CAMPAIGN";
						conncc.ExecuteQuery();
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					else
					{
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}

					conncc.QueryString="PARAM_SALESCOM_RFSOURCE_CAMPAIGN_MAKER '"+LBL_SAVE_MODE.Text+"','"+ 
						TXT_ID.Text +"','"+ TXT_DESC.Text +"','"+LBL_SEQ_ID.Text+"','1'";
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
