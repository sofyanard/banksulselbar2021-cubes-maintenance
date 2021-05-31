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
	/// Summary description for CriteriaType.
	/// </summary>
	public partial class CriteriaType : System.Web.UI.Page
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
			if (tb.Trim() == "&nbsp;" || tb.Trim() == "&nbsp" )
				tb = "";
			return tb;
		}

		private void ViewExistingData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_RFCRITERIA_TYPE";
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

		private void  ViewPendingData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_RFCRITERIA_TYPE";
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
					this.DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.CurrentPageIndex - 1;
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

		private void BlankEntry()
		{
			TXT_DESC.Text="";
			TXT_TRACK.Text="";
			TXT_FIELDDATE.Text="";
			TXT_FIELDTRACK.Text="";
			TXT_FIELDSELECT.Text="";
			TXT_FIELDGROUP.Text="";
			try {RBL_TYPE.SelectedItem.Selected = false;}
			catch{}
			LBL_JENIS.Text="";
			LBL_ID.Text="";
			LBL_SEQ_ID.Text="";
			LBL_SAVE_MODE.Text="1";
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE.Text="0";
					TXT_DESC.Text=cleansText(e.Item.Cells[1].Text);
					TXT_TRACK.Text=cleansText(e.Item.Cells[2].Text);
					TXT_FIELDDATE.Text=cleansText(e.Item.Cells[3].Text);
					TXT_FIELDTRACK.Text=cleansText(e.Item.Cells[4].Text);
					TXT_FIELDSELECT.Text=cleansText(e.Item.Cells[5].Text);
					TXT_FIELDGROUP.Text=cleansText(e.Item.Cells[6].Text);
					try {RBL_TYPE.SelectedValue = cleansText(e.Item.Cells[9].Text);}
					catch{}
					LBL_JENIS.Text="edit";
					LBL_ID.Text=cleansText(e.Item.Cells[0].Text);
					LBL_SEQ_ID.Text="";//edit dari Existing seq_id kosong....
					break;
				case "delete":
					//LBL_SAVE_MODE.Text="2";
					//get seq_id
					conncc.QueryString="select isnull(max(seq_id),0)+1 no from PENDING_SALESCOM_RFCRITERIA_TYPE";
					conncc.ExecuteQuery();
					int seq_id = int.Parse(conncc.GetFieldValue("no"));
					//SMEDEV2..
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_RFCRITERIA_TYPE where CRITERIA_ID='"+e.Item.Cells[0].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_RFCRITERIA_TYPE set STATUS='2' where CRITERIA_ID='"+e.Item.Cells[0].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_RFCRITERIA_TYPE_MAKER '2','"+
							e.Item.Cells[0].Text +"','"+ e.Item.Cells[1].Text +"','"+
							e.Item.Cells[2].Text +"','"+ e.Item.Cells[3].Text +"','"+ e.Item.Cells[4].Text +"','"+
							e.Item.Cells[5].Text +"','"+ e.Item.Cells[6].Text +"','"+ e.Item.Cells[9].Text +"','"+
							seq_id +"','1'";
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
					TXT_DESC.Text=cleansText(e.Item.Cells[1].Text);
					TXT_TRACK.Text=cleansText(e.Item.Cells[2].Text);
					TXT_FIELDDATE.Text=cleansText(e.Item.Cells[3].Text);
					TXT_FIELDTRACK.Text=cleansText(e.Item.Cells[4].Text);
					TXT_FIELDSELECT.Text=cleansText(e.Item.Cells[5].Text);
					TXT_FIELDGROUP.Text=cleansText(e.Item.Cells[6].Text);
					try {RBL_TYPE.SelectedValue = cleansText(e.Item.Cells[12].Text);}
					catch{}
					LBL_JENIS.Text="edit";
					LBL_ID.Text=cleansText(e.Item.Cells[0].Text);
					LBL_SEQ_ID.Text=cleansText(e.Item.Cells[11].Text);
					LBL_SAVE_MODE.Text=cleansText(e.Item.Cells[10].Text);
					break;
				case "delete":
					//SMEDEV2
					string CriteriaId=e.Item.Cells[0].Text;
					int seqid = int.Parse(e.Item.Cells[11].Text);
					conncc.QueryString="Delete from PENDING_SALESCOM_RFCRITERIA_TYPE "+
						"where CRITERIA_ID='"+CriteriaId+"' and SEQ_ID='"+ seqid +"'";
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
			if (LBL_JENIS.Text=="" && LBL_SEQ_ID.Text=="" && LBL_ID.Text=="") //input baru
			{
				//SalesMandiri--Cek tabel Existing
				conncc.QueryString="select isnull(max(convert(int, CRITERIA_ID)), 0)+1 ID from RFCRITERIA_TYPE";
				conncc.ExecuteQuery();
				LBL_ID.Text = kar(3,conncc.GetFieldValue("ID"));

				//get seq_id
				conncc.QueryString="select isnull(max(seq_id),0)+1 seqid from PENDING_SALESCOM_RFCRITERIA_TYPE";
				conncc.ExecuteQuery();
				int seq_id = int.Parse(conncc.GetFieldValue("seqid"));
				
				//SMEDEV2---Jika CRITERIA_ID baru sudah ada di tabel pending
				conncc.QueryString="select CRITERIA_ID from PENDING_SALESCOM_RFCRITERIA_TYPE ";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					if(conncc.GetFieldValue(i,0).ToString()==LBL_ID.Text)
					{
						int CRITERIA_ID = int.Parse(LBL_ID.Text)+1;
						this.LBL_ID.Text = kar(3,CRITERIA_ID.ToString());
					}
				}
				
				conncc.QueryString="PARAM_SALESCOM_RFCRITERIA_TYPE_MAKER '1','"+
					LBL_ID.Text +"','"+ TXT_DESC.Text +"','"+
					TXT_TRACK.Text +"','"+ TXT_FIELDDATE.Text +"','"+ TXT_FIELDTRACK.Text +"','"+
					TXT_FIELDSELECT.Text +"','"+ TXT_FIELDGROUP.Text +"','"+ RBL_TYPE.SelectedValue.ToString() +"','"+
					seq_id +"','1'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			if (LBL_JENIS.Text=="edit") //edit
			{
				if (LBL_SEQ_ID.Text!="")//Edit dari DGR_REQUEST
				{
					//SALESMANDIRI
					conncc.QueryString="PARAM_SALESCOM_RFCRITERIA_TYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						LBL_ID.Text +"','"+ TXT_DESC.Text +"','"+
						TXT_TRACK.Text +"','"+ TXT_FIELDDATE.Text +"','"+ TXT_FIELDTRACK.Text +"','"+
						TXT_FIELDSELECT.Text +"','"+ TXT_FIELDGROUP.Text +"','"+ RBL_TYPE.SelectedValue.ToString() +"','"+
						LBL_SEQ_ID.Text +"','1'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
				else if(LBL_SEQ_ID.Text=="")//edit dari Existing
				{
					int seq_id=0;
					conncc.QueryString="select * from PENDING_SALESCOM_RFCRITERIA_TYPE where CRITERIA_ID='"+LBL_ID.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//get seq_id
						conncc.QueryString="select isnull(max(seq_id),0)+1 seq_id from PENDING_SALESCOM_RFCRITERIA_TYPE";
						conncc.ExecuteQuery();
						seq_id = int.Parse(conncc.GetFieldValue("seq_id"));
					}
					else
					{
						seq_id = int.Parse(conncc.GetFieldValue("SEQ_ID"));
					}
					
					conncc.QueryString="PARAM_SALESCOM_RFCRITERIA_TYPE_MAKER '0','"+
						LBL_ID.Text +"','"+ TXT_DESC.Text +"','"+
						TXT_TRACK.Text +"','"+ TXT_FIELDDATE.Text +"','"+ TXT_FIELDTRACK.Text +"','"+
						TXT_FIELDSELECT.Text +"','"+ TXT_FIELDGROUP.Text +"','"+ RBL_TYPE.SelectedValue.ToString() +"','"+
						seq_id +"','1'";
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
