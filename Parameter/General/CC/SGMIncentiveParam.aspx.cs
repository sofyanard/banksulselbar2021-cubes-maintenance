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
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.CC
{
	/// <summary>
	/// Summary description for SGMIncentiveParam.
	/// </summary>
	public partial class SGMIncentiveParam : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=LOSCC2-DEV;uid=sa;pwd=dmscorp");
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
			//BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
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
			conncc.QueryString="select * from VW_CC_SGMINCENTIVE ";
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
			conncc.QueryString="select * from VW_PARAM_CC_PENDING_SGMINCENTIVE";
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

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BlankEntry();
		}

		private void BlankEntry()
		{
			TXT_MIN_APPR.Text="";
			TXT_MIN_APPR.Enabled=true;
			TXT_MAX_APPR.Text="";
			TXT_INCENTIVE.Text="";
			TXT_REMARK.Text="";
			LBL_MIN.Text="";
			LBL_SEQ_ID.Text="";
			//LBL_SAVE_MODE.Text="";
								
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
					LBL_SAVE_MODE.Text="0";
					TXT_MIN_APPR.Text= cleansText(e.Item.Cells[0].Text);
					TXT_MAX_APPR.Text= cleansText(e.Item.Cells[1].Text);
					TXT_INCENTIVE.Text= cleansText(e.Item.Cells[2].Text);
					TXT_REMARK.Text=cleansText(e.Item.Cells[3].Text);
					LBL_MIN.Text= cleansText(e.Item.Cells[0].Text);
					LBL_SEQ_ID.Text=""; //update dari Existing Ga ada SeqId-nya...
					TXT_MIN_APPR.Enabled=false;
					break;
				case "delete":
					//cek min appr di Pending
					LBL_SAVE_MODE.Text="2";
					conncc.QueryString="select * from PENDING_CC_SGMINCENTIVE where MIN_APPR='"+cleansText(e.Item.Cells[0].Text)+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_CC_SGMINCENTIVE set STATUS='2' where MIN_APPR='"+e.Item.Cells[0].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="select isnull(max(cast(SEQ_ID as integer)),0)+1 SeqId from PENDING_CC_SGMINCENTIVE";
						conncc.ExecuteQuery();
						this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SeqId");
					
						conncc.QueryString="PARAM_CC_SGMINCENTIVE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
							LBL_SEQ_ID.Text+"','"+ e.Item.Cells[0].Text +"','"+ e.Item.Cells[1].Text +"','"+
							e.Item.Cells[2].Text +"','"+ e.Item.Cells[3].Text +"'";
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
					string status = e.Item.Cells[7].Text;
					if (status == "2")
					{
						break;
					}
					TXT_MIN_APPR.Text= cleansText(e.Item.Cells[0].Text);
					TXT_MAX_APPR.Text= cleansText(e.Item.Cells[1].Text);
					TXT_INCENTIVE.Text= cleansText(e.Item.Cells[2].Text);
					TXT_REMARK.Text=cleansText(e.Item.Cells[3].Text);
					LBL_MIN.Text= cleansText(e.Item.Cells[0].Text);
					LBL_SEQ_ID.Text=cleansText(e.Item.Cells[5].Text);
					LBL_SAVE_MODE.Text=cleansText(e.Item.Cells[7].Text);
					TXT_MIN_APPR.Enabled=false;
					break;
				case "delete":
					//SMEDEV2
					string seqid=e.Item.Cells[5].Text;
					conncc.QueryString="Delete from PENDING_CC_SGMINCENTIVE where SEQ_ID='"+ seqid +"'"; 
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					ViewPendingData();
					break;
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (this.TXT_MIN_APPR.Text == "")
			{
				GlobalTools.popMessage(this,"Min. Approval tidak boleh kosong!");
				return;
			}
			//cek MIN APPR harus < MAX APPR
			//if(int.Parse(TXT_MIN_APPR.Text) > int.Parse(TXT_MAX_APPR.Text))
			//{
			//	GlobalTools.popMessage(this,"MIN APPR Harus Lebih Kecil dari MAX APPR");
			//	return;
			//}

			if (LBL_SEQ_ID.Text == "" && LBL_MIN.Text=="") //input baru
			{
				//cek min appr di Existing
				conncc.QueryString="select * from SGMINCENTIVE where MIN_APPR='"+TXT_MIN_APPR.Text+"'";
				conncc.ExecuteQuery();
				//cek min appr di Pending
				//conncc.QueryString="select MIN_APPR from PENDING_CC_SGMINCENTIVE where MIN_APPR='"+TXT_MIN_APPR.Text+"'";
				//conncc.ExecuteQuery();
				//if (conncc.GetRowCount() > 0 || conncc.GetRowCount() > 0) 
				if (conncc.GetRowCount() > 0) 
				{
					GlobalTools.popMessage(this,"Min APPR has already been used! Request canceled!");
						return;
				}

				conncc.QueryString="select isnull(max(cast(SEQ_ID as integer)),0)+1 SeqId from PENDING_CC_SGMINCENTIVE";
				conncc.ExecuteQuery();
				this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SeqId");

				conncc.QueryString="PARAM_CC_SGMINCENTIVE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
					LBL_SEQ_ID.Text+"','"+ TXT_MIN_APPR.Text +"','"+ TXT_MAX_APPR.Text +"','"+
					TXT_INCENTIVE.Text +"','"+ TXT_REMARK.Text +"'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			else if (LBL_SEQ_ID.Text!= "" || LBL_MIN.Text!= "") //edit
			{
				if (LBL_SEQ_ID.Text != "")//Edit dari DGR_REQUEST
				{
					conncc.QueryString="PARAM_CC_SGMINCENTIVE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						LBL_SEQ_ID.Text+"','"+ TXT_MIN_APPR.Text +"','"+ TXT_MAX_APPR.Text +"','"+
						TXT_INCENTIVE.Text +"','"+ TXT_REMARK.Text +"'";
					conncc.ExecuteQuery();
					ViewPendingData();
					BlankEntry();
				}
				else if (LBL_SEQ_ID.Text == "") //Edit dari DGR_EXISTING
				{
					conncc.QueryString="select * from PENDING_CC_SGMINCENTIVE where MIN_APPR='"+TXT_MIN_APPR.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)
					{
						conncc.QueryString="select isnull(max(cast(SEQ_ID as integer)),0)+1 SeqId from PENDING_CC_SGMINCENTIVE";
						conncc.ExecuteQuery();
						this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SeqId").ToString();
					}
					else
					{
						this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
					}

					conncc.QueryString="PARAM_CC_SGMINCENTIVE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						LBL_SEQ_ID.Text+"','"+ TXT_MIN_APPR.Text +"','"+ TXT_MAX_APPR.Text +"','"+
						TXT_INCENTIVE.Text +"','"+ TXT_REMARK.Text +"'";
					conncc.ExecuteQuery();
					ViewPendingData();
					BlankEntry();
				}
			}
			LBL_SAVE_MODE.Text="1";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}
}
