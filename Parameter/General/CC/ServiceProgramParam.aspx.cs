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
	/// Summary description for ServiceProgramParam.
	/// </summary>
	public partial class ServiceProgramParam : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=LOSCC2-DEV;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				DDL_PROD_ID.Items.Add(new ListItem("--select--", ""));
				conncc.QueryString ="select PRODUCTID from TPRODUCT";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_PROD_ID.Items.Add(new ListItem(conncc.GetFieldValue(i,0))); 
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
			//LOSCC2-DEV
			conncc.QueryString="select * from VW_CC_SERVICE_PROGRAM";
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
				this.DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.PageCount - 1;
				this.DGR_EXISTING.DataBind();
			}
		}

		private void ViewPendingData()
		{
			//SMEDEV2
			conncc.QueryString="select * from VW_PARAM_CC_PENDING_CCTYPE";
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
				this.DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount-1;
				this.DGR_REQUEST.DataBind();
			}
		}
	
		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex= e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[17].Text;
					if (status == "2")
					{
						break;
					}
					TXT_TYPE.Text= cleansText(e.Item.Cells[2].Text);
					TXT_DEF_LIMIT.Text= cleansText(e.Item.Cells[3].Text);
					TXT_SICS_ID.Text= cleansText(e.Item.Cells[4].Text);
					TXT_MAX_LIMIT.Text=cleansText(e.Item.Cells[5].Text);
					LBL_SEQ.Text= cleansText(e.Item.Cells[0].Text);
					LBL_SEQ_ID.Text=cleansText(e.Item.Cells[16].Text);
					LBL_SAVE_MODE.Text=cleansText(e.Item.Cells[17].Text);
					DDL_PROD_ID.SelectedValue=cleansText(e.Item.Cells[1].Text);
					DDL_PROD_ID.Enabled=false;
					break;
				case "delete":
					//SMEDEV2
					string seq=e.Item.Cells[0].Text.Trim();
					string seqid=e.Item.Cells[16].Text.Trim();
					string type=e.Item.Cells[2].Text.Trim();
					conncc.QueryString="Delete from PENDING_CC_CCTYPE where SEQ='"+
						seq +"' and SEQ_ID='"+seqid+"' and TYPE_DESC='"+ type +"'"; 
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					ViewPendingData();
					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex= e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE.Text="0";
					TXT_TYPE.Text= cleansText(e.Item.Cells[2].Text);
					TXT_DEF_LIMIT.Text= cleansText(e.Item.Cells[3].Text);
					TXT_SICS_ID.Text= cleansText(e.Item.Cells[4].Text);
					TXT_MAX_LIMIT.Text=cleansText(e.Item.Cells[5].Text);
					LBL_SEQ.Text= cleansText(e.Item.Cells[0].Text);
					DDL_PROD_ID.SelectedValue=cleansText(e.Item.Cells[1].Text);
					DDL_PROD_ID.Enabled=false;
					LBL_SEQ_ID.Text=""; //update dari Existing Ga ada SeqId-nya...
					break;
				case "delete":
					LBL_SAVE_MODE.Text="2";
					int seqid=1;
					if (seqid <= this.DGR_REQUEST.Items.Count)
					{
						seqid = this.DGR_REQUEST.Items.Count + 1;
					}
					this.LBL_SEQ_ID.Text= seqid.ToString();
					
					//cek tabel pending
					conncc.QueryString="select * from PENDING_CC_CCTYPE where SEQ='"+e.Item.Cells[0].Text.Trim()+"'";
									 //"SEQ_ID='"+LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update Pending_CC_CCTYPE set STATUS='2' where SEQ='"+e.Item.Cells[0].Text.Trim()+"'";
						conncc.ExecuteQuery();
						//GlobalTools.popMessage(this,"SEQ_ID is Existing...");
						//return;
					}
					else
					{
						conncc.QueryString="PARAM_GENERAL_CCTYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
							e.Item.Cells[0].Text.Trim() +"','"+ e.Item.Cells[2].Text.Trim() +"','"+
							e.Item.Cells[3].Text.Trim() +"','"+ e.Item.Cells[4].Text.Trim() +"','"+
							e.Item.Cells[5].Text.Trim() +"','','','','','','','','','"+ e.Item.Cells[1].Text.Trim() +"','1','"+
							LBL_SEQ_ID.Text.Trim()+"'";
						//try
						//{
						conncc.ExecuteQuery();
						//}
						//catch { GlobalTools.popMessage (this,"Error...");}
					}
					ViewPendingData();
					break;
			}
		}

		private void cleardata()
		{
			TXT_TYPE.Text="";
			TXT_DEF_LIMIT.Text="";
			TXT_SICS_ID.Text="";
			TXT_MAX_LIMIT.Text="";
			LBL_SEQ.Text="";
			LBL_SEQ_ID.Text="";
			DDL_PROD_ID.SelectedValue="";
			DDL_PROD_ID.Enabled=true;
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			cleardata();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SEQ_ID.Text == "" && LBL_SEQ.Text=="") //input baru
			{
				//conncc.QueryString="select * from PENDING_CC_CCTYPE where 
				conncc.QueryString="select isnull(max(cast(SEQ as integer)),0)+1 Seq from CCTYPE";
				conncc.ExecuteQuery();
				//int seq1 = conncc.GetRowCount()+1;
				this.LBL_SEQ.Text = conncc.GetFieldValue("Seq").ToString();
				
				conncc.QueryString="select SEQ from PENDING_CC_CCTYPE ";//where SEQ='"+LBL_SEQ.Text+"'";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					if(conncc.GetFieldValue(i,0).ToString()==LBL_SEQ.Text)
					{
						int seq = int.Parse(LBL_SEQ.Text)+1;
						this.LBL_SEQ.Text = seq.ToString();
					}
				}

				//int seqid=0;
				conncc.QueryString="select isnull(max(cast(SEQ_ID as integer)),0)+1 SeqId from PENDING_CC_CCTYPE";
				conncc.ExecuteQuery();
				//if (seqid <= this.DGR_REQUEST.Items.Count)
				//{
				//	seqid=this.DGR_REQUEST.Items.Count + 1;
				//}
				this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SeqId").ToString();

				conncc.QueryString="PARAM_GENERAL_CCTYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+ 
					LBL_SEQ.Text.Trim() +"','"+ TXT_TYPE.Text.Trim() +"','"+
					TXT_DEF_LIMIT.Text.Trim() +"','"+ TXT_SICS_ID.Text.Trim() +"','"+
					TXT_MAX_LIMIT.Text.Trim() +"','','','','','','','','','"+ DDL_PROD_ID.SelectedValue.ToString().Trim() +"','1','"+
					LBL_SEQ_ID.Text.Trim()+"'";
				conncc.ExecuteQuery();
				cleardata();
				ViewPendingData();
			}
			else if (LBL_SEQ.Text != "") //edit
			{
				if (LBL_SEQ_ID.Text !="") //edit dari Pending
				{
					conncc.QueryString="PARAM_GENERAL_CCTYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+ 
						LBL_SEQ.Text.Trim() +"','"+ TXT_TYPE.Text.Trim() +"','"+
						TXT_DEF_LIMIT.Text.Trim() +"','"+ TXT_SICS_ID.Text.Trim() +"','"+
						TXT_MAX_LIMIT.Text.Trim() +"','','','','','','','','','"+ DDL_PROD_ID.SelectedValue.ToString().Trim() +"','1','"+
						LBL_SEQ_ID.Text.Trim()+"'";
					conncc.ExecuteQuery();
					cleardata();
					ViewPendingData();
				}
				else if (LBL_SEQ_ID.Text == "")//edit dari Existing
				{
					conncc.QueryString="select * from PENDING_CC_CCTYPE where SEQ='"+LBL_SEQ.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						conncc.QueryString="select isnull(max(cast(SEQ_ID as integer)),0)+1 SeqId from PENDING_CC_CCTYPE";
						conncc.ExecuteQuery();
						this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SeqId").ToString();
					}
					else
					{
						this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
					}
					conncc.QueryString="PARAM_GENERAL_CCTYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+ 
						LBL_SEQ.Text.Trim() +"','"+ TXT_TYPE.Text.Trim() +"','"+
						TXT_DEF_LIMIT.Text.Trim() +"','"+ TXT_SICS_ID.Text.Trim() +"','"+
						TXT_MAX_LIMIT.Text.Trim() +"','','','','','','','','','"+ DDL_PROD_ID.SelectedValue.ToString().Trim() +"','1','"+
						LBL_SEQ_ID.Text.Trim()+"'";
					conncc.ExecuteQuery();
					cleardata();
					ViewPendingData();
				}
			}
			LBL_SAVE_MODE.Text="1";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../HostParam.aspx?mc=99020101");
		}
	}
}
