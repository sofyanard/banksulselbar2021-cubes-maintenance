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
	/// Summary description for CalculationType.
	/// </summary>
	public partial class CalculationType : System.Web.UI.Page
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
			conncc.QueryString = "select * from VW_PARAM_SALESCOM_CALCULATION_TYPE ";
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
			conncc.QueryString= "Select * from VW_PARAM_SALESCOM_PENDING_CALCULATION_TYPE";
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
			TXT_DESC.Text = "";
			TXT_TRACK.Text = "";
			TXT_DATEFIELD.Text = "";
			TXT_TRACKFIELD.Text = "";
			try {rblType.SelectedItem.Selected = false;}
			catch{}
			LBL_JENIS.Text = "";
			LBL_ID.Text = "";
			LBL_SEQ_ID.Text="";
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

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE.Text="0";
					TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_TRACK.Text = cleansText(e.Item.Cells[2].Text);
					TXT_DATEFIELD.Text = cleansText(e.Item.Cells[3].Text);
					TXT_TRACKFIELD.Text = cleansText(e.Item.Cells[4].Text);
					try {rblType.SelectedValue = cleansText(e.Item.Cells[7].Text);}
					catch{}
					LBL_JENIS.Text = "edit";
					LBL_ID.Text = cleansText(e.Item.Cells[0].Text);
					LBL_SEQ_ID.Text="";
					break;
				case "delete":
					//LBL_SAVE_MODE.Text="2";
					//SMEDEV2
					//get seq_id
					conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_CALCULATION_TYPE";
					conncc.ExecuteQuery();
					string seq_id = conncc.GetFieldValue("SEQ_ID");
					
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_CALCULATION_TYPE where CALCULATION_ID='"+e.Item.Cells[0].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_CALCULATION_TYPE set STATUS='2' where CALCULATION_ID='"+e.Item.Cells[0].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_CALCULATION_TYPE_MAKER '2','"+
							e.Item.Cells[0].Text +"','"+ e.Item.Cells[1].Text +"','"+
							e.Item.Cells[2].Text +"','"+ e.Item.Cells[3].Text +"','"+
							e.Item.Cells[4].Text +"','"+ e.Item.Cells[7].Text +"','','1','"+seq_id+"'";
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
					string status = e.Item.Cells[10].Text;
					if(status=="2")
					{
						break;
					}
					TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_TRACK.Text = cleansText(e.Item.Cells[2].Text);
					TXT_DATEFIELD.Text = cleansText(e.Item.Cells[3].Text);
					TXT_TRACKFIELD.Text = cleansText(e.Item.Cells[4].Text);
					try {rblType.SelectedValue = cleansText(e.Item.Cells[8].Text);}
					catch{}
					LBL_JENIS.Text = "edit";
					LBL_ID.Text = cleansText(e.Item.Cells[0].Text);
					LBL_SEQ_ID.Text = cleansText(e.Item.Cells[9].Text);
					LBL_SAVE_MODE.Text = cleansText(e.Item.Cells[10].Text);
					break;
				case "delete":
					//SMEDEV2
					conncc.QueryString="Delete from PENDING_SALESCOM_CALCULATION_TYPE where CALCULATION_ID='"+e.Item.Cells[0].Text+"' and "+
									 "SEQ_ID='"+e.Item.Cells[9].Text+"'";
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
			if (LBL_JENIS.Text=="" && LBL_ID.Text == "") //input baru
			{
				//SALESMANDIRI--- Membuat ID baru
				conncc.QueryString="select isnull(max(CALCULATION_ID),0)+1 IDnya from CALCULATION_TYPE";
				conncc.ExecuteQuery();
				string ID = kar(3,conncc.GetFieldValue("IDnya"));
				LBL_ID.Text = ID;

				//SMEDEV2---Membandingkan ID baru dengan yang ada di Tabel Pending
				conncc.QueryString="select CALCULATION_ID from PENDING_SALESCOM_CALCULATION_TYPE";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					if(LBL_ID.Text.ToString().Trim() == conncc.GetFieldValue(i,0).ToString().Trim())
					{
						int ID2 = int.Parse(LBL_ID.Text.ToString().Trim())+1;
						string IDnya = kar(3,ID2.ToString());
						LBL_ID.Text=IDnya;
					}
				}

				//get seq_id
				conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_CALCULATION_TYPE";
				conncc.ExecuteQuery();
				LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
								
				conncc.QueryString="PARAM_SALESCOM_CALCULATION_TYPE_MAKER '1','"+
					LBL_ID.Text +"','"+ TXT_DESC.Text +"','"+
					TXT_TRACK.Text +"','"+ TXT_DATEFIELD.Text +"','"+
					TXT_TRACKFIELD.Text +"','"+ rblType.SelectedValue.ToString() +"','','1','"+LBL_SEQ_ID.Text+"'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			if (LBL_JENIS.Text=="edit" && LBL_ID.Text != "") //edit
			{
				if(LBL_SEQ_ID.Text!="")//edit dari Request
				{
					conncc.QueryString="PARAM_SALESCOM_CALCULATION_TYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						LBL_ID.Text +"','"+ TXT_DESC.Text +"','"+
						TXT_TRACK.Text +"','"+ TXT_DATEFIELD.Text +"','"+
						TXT_TRACKFIELD.Text +"','"+ rblType.SelectedValue.ToString() +"','','1','"+LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
				else if(LBL_SEQ_ID.Text=="")//edit dari Existing
				{
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_CALCULATION_TYPE where CALCULATION_ID='"+LBL_ID.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//get seq_id
						conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_CALCULATION_TYPE";
						conncc.ExecuteQuery();
						LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
					}
					else
					{
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					
					conncc.QueryString="PARAM_SALESCOM_CALCULATION_TYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						LBL_ID.Text +"','"+ TXT_DESC.Text +"','"+
						TXT_TRACK.Text +"','"+ TXT_DATEFIELD.Text +"','"+
						TXT_TRACKFIELD.Text +"','"+ rblType.SelectedValue.ToString() +"','','1','"+LBL_SEQ_ID.Text+"'";
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

