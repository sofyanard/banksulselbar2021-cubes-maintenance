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
	/// Summary description for MediaName.
	/// </summary>
	public partial class MediaName : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList DDL_SOURCE;
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
				DDL_MEDIA.Items.Add(new ListItem("--Select--",""));
				conncc.QueryString="select MD_ID,MD_DESC from RFMEDIA ";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_MEDIA.Items.Add(new ListItem(conncc.GetFieldValue(i,1),conncc.GetFieldValue(i,0)));
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
			conncc.QueryString="select * from VW_PARAM_SALESCOM_RFMEDIANAME";
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
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_RFMEDIANAME";
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

		private void BlankEntry()
		{
			DDL_MEDIA.SelectedValue="";
			TXT_MEDIA_ID.Text="Auto";
			TXT_DESC.Text="";
			LBL_JENIS.Text="";
			LBL_ID.Text="";
			LBL_SEQ_ID.Text="";
			LBL_SAVE_MODE.Text="1";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BlankEntry();
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex=e.NewPageIndex;
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
					DDL_MEDIA.SelectedValue = e.Item.Cells[4].Text;
					TXT_MEDIA_ID.Text = e.Item.Cells[1].Text;
					TXT_DESC.Text = e.Item.Cells[2].Text;
					LBL_JENIS.Text = "edit";
					LBL_ID.Text = e.Item.Cells[1].Text;
					LBL_SEQ_ID.Text="";
					break;
				
				case "delete":
					//LBL_SAVE_MODE.Text="2";
					//SMEDEV2
					//get seq_id
					conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_RFMEDIANAME";
					conncc.ExecuteQuery();
					string seq_id = conncc.GetFieldValue("SEQ_ID");
					
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_RFMEDIANAME where MN_ID='"+e.Item.Cells[1].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_RFMEDIANAME set STATUS='2' where MN_ID='"+e.Item.Cells[1].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_RFMEDIANAME_MAKER '2','"+
							e.Item.Cells[1].Text +"','"+e.Item.Cells[2].Text+"','"+
							e.Item.Cells[4].Text +"','1','"+seq_id+"'";
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
					DDL_MEDIA.SelectedValue = e.Item.Cells[5].Text;
					TXT_MEDIA_ID.Text = e.Item.Cells[1].Text;
					TXT_DESC.Text = e.Item.Cells[2].Text;
					LBL_JENIS.Text = "edit";
					LBL_ID.Text = e.Item.Cells[1].Text;
					LBL_SEQ_ID.Text = e.Item.Cells[6].Text;
					LBL_SAVE_MODE.Text = e.Item.Cells[7].Text;
					break;
				
				case "delete":
					//SMEDEV2
					string MD_ID = e.Item.Cells[5].Text;
					string MN_ID = e.Item.Cells[1].Text;
					string MN_DESC = e.Item.Cells[2].Text;
					string SEQ_ID = e.Item.Cells[6].Text;
					conncc.QueryString="Delete from PENDING_SALESCOM_RFMEDIANAME "+
						"where MN_ID='"+MN_ID+"' and MD_ID='"+MD_ID+"' and "+
						"MN_DESC='"+MN_DESC+"' and SEQ_ID='"+SEQ_ID+"'";
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

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_JENIS.Text=="" && LBL_ID.Text=="") //input baru
			{
				//SalesMandiri--- Input MD_ID
				conncc.QueryString="select isnull(max(MN_ID),0)+1 ID from RFMEDIANAME";
				conncc.ExecuteQuery();
				string MN_ID=conncc.GetFieldValue("ID");

				//SMEDEV2-- Membandingkan MN_ID baru dengan yang ada di Tabel Pending
				conncc.QueryString="select MN_ID from PENDING_SALESCOM_RFMEDIANAME";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					if(MN_ID.ToString().Trim()==conncc.GetFieldValue(i,0).ToString().Trim())
					{
						int IDnya=int.Parse(MN_ID.ToString().Trim())+1;
						MN_ID = IDnya.ToString().Trim();
					}
				}

				//get seq_id
				conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_RFMEDIANAME";
				conncc.ExecuteQuery();
				LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
				
				conncc.QueryString="PARAM_SALESCOM_RFMEDIANAME_MAKER '"+LBL_SAVE_MODE.Text+"','"+
					MN_ID +"','"+TXT_DESC.Text+"','"+
					DDL_MEDIA.SelectedValue+"','1','"+LBL_SEQ_ID.Text+"'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			if (LBL_JENIS.Text=="edit" && LBL_ID.Text!="") //edit
			{
				if(LBL_SEQ_ID.Text!="")//edit dari Request
				{
					conncc.QueryString="PARAM_SALESCOM_RFMEDIANAME_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						TXT_MEDIA_ID.Text +"','"+TXT_DESC.Text+"','"+
						DDL_MEDIA.SelectedValue+"','1','"+LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
				else if(LBL_SEQ_ID.Text=="")//edit dari Existing
				{
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_RFMEDIANAME where MN_ID='"+TXT_MEDIA_ID.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//get seq_id
						conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_RFMEDIANAME";
						conncc.ExecuteQuery();
						LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
					}
					else
					{
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					
					conncc.QueryString="PARAM_SALESCOM_RFMEDIANAME_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						TXT_MEDIA_ID.Text +"','"+TXT_DESC.Text+"','"+
						DDL_MEDIA.SelectedValue+"','1','"+LBL_SEQ_ID.Text+"'";
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

