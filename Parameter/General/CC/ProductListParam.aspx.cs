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

namespace CuBES_Maintenance.Parameter.General.CC
{
	/// <summary>
	/// Summary description for ProductListParam.
	/// </summary>
	public partial class ProductListParam : System.Web.UI.Page
	{
		//protected Connection conn = new Connection(ConfigurationSettings.AppSettings["conn"]);
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
				TR_SICSID.Visible=false;
				ViewExistingData();
				ViewPendingData();
				GlobalTools.fillRefList(DDL_TYPE,"select SEQ, CARD_TYPE from RFCARDTYPE where ACTIVE = '1'", true,conncc);
				GlobalTools.fillRefList(DDL_NETWORK_ID,"select NETWORKID, NETWORKDESC from RFNETWORK where ACTIVE = '1'", true,conncc);
				GlobalTools.fillRefList(DDL_CLASSIC_TYPE,"select CLASSICID, CLASSICDESC from RFCLASSICTYPE where ACTIVE = '1'", true,conncc);
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

		private void ViewExistingData()
		{
			conncc.QueryString="select * from VW_CC_TPRODUCT ";
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
			conncc.QueryString="select * from VW_PARAM_CC_PENDING_TPRODUCT";
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
				this.DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				this.DGR_REQUEST.DataBind();
			}
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

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void cleardata()
		{
			TXT_ID.Text="";
			TXT_ID.Enabled=true;
			TXT_NAME.Text="";
			//TXT_TYPE.Text="";
			DDL_NETWORK_ID.SelectedIndex = 0;
			DDL_CLASSIC_TYPE.SelectedIndex = 0;
			DDL_TYPE.SelectedIndex = 0;
			try {RBL_Flag.SelectedItem.Selected = false;}
			catch{}
			TXT_SICS_CD.Text="";
			LBL_SEQ_ID.Text="";
			LBL_CODE.Text="";
			//LBL_SAVE_MODE.Text="";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			cleardata();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE.Text="0";
					TXT_ID.Text= cleansText(e.Item.Cells[0].Text);
					TXT_NAME.Text= cleansText(e.Item.Cells[3].Text);
					//TXT_TYPE.Text= cleansText(e.Item.Cells[2].Text);
					try
					{
						DDL_TYPE.SelectedValue = cleansText(e.Item.Cells[4].Text);
					}
					catch{}
					try
					{
						DDL_NETWORK_ID.SelectedValue = cleansText(e.Item.Cells[9].Text);
					}
					catch{}
					try
					{
						DDL_CLASSIC_TYPE.SelectedValue = cleansText(e.Item.Cells[10].Text);
					}
					catch{}
					LBL_CODE.Text= cleansText(e.Item.Cells[0].Text);
					TXT_SICS_CD.Text= cleansText(e.Item.Cells[5].Text);
					try {RBL_Flag.SelectedValue = cleansText(e.Item.Cells[7].Text);}
					catch{}
					LBL_SEQ_ID.Text="";
					TXT_ID.Enabled=false;
					break;
				case "delete":
					//SMEDEV2
					LBL_SAVE_MODE.Text="2";
					conncc.QueryString="select isnull(max(cast(SEQ_ID as integer)),0)+1 SeqId from PENDING_CC_PRODUCT";
					conncc.ExecuteQuery();
					this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SeqId");
					
					//cek tabel pending
					conncc.QueryString="select * from PENDING_CC_PRODUCT where PRODUCTID='"+e.Item.Cells[0].Text.Trim()+"'";
					//"SEQ_ID='"+LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update Pending_CC_PRODUCT set STATUS='"+LBL_SAVE_MODE.Text+"' where PRODUCTID='"+e.Item.Cells[0].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_GENERAL_TPRODUCTLIST_MAKER '"+LBL_SAVE_MODE.Text+"','"+
							e.Item.Cells[0].Text +"','"+ e.Item.Cells[3].Text +"','"+
							e.Item.Cells[5].Text +"','','','','','','','','', "+
							"'','','','','','','','','','','','','','','','1','"+ e.Item.Cells[7].Text +"','"+ LBL_SEQ_ID.Text +"', '"+
							e.Item.Cells[9].Text +"','"+e.Item.Cells[10].Text+"'";
						try
						{
							conncc.ExecuteQuery();
						}
						catch { GlobalTools.popMessage (this,"Error...");}
					}
					ViewPendingData();
					LBL_SAVE_MODE.Text="1";
					break;
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SEQ_ID.Text == "" && LBL_CODE.Text =="") //input baru
			{
				//int seqid=0;
				conncc.QueryString="select isnull(max(cast(SEQ_ID as integer)),0)+1 SeqId from PENDING_CC_PRODUCT";
				conncc.ExecuteQuery();
				this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SeqId");
				//if (seqid <= this.DGR_REQUEST.Items.Count)
				//{
				//	seqid=DGR_REQUEST.Items.Count + 1;
				//}
				//this.LBL_SEQ_ID.Text = seqid.ToString();
							

				conncc.QueryString="PARAM_GENERAL_TPRODUCTLIST_MAKER '1','"+
					TXT_ID.Text +"','"+ TXT_NAME.Text +"','"+
					DDL_TYPE.SelectedValue +"','','','','','','','','', "+
					"'','','','','','','','','','','','','','','','','"+ RBL_Flag.SelectedValue.ToString() +"','"+ LBL_SEQ_ID.Text +"','"+
					DDL_NETWORK_ID.SelectedValue+"', '"+DDL_CLASSIC_TYPE.SelectedValue+"'";

				conncc.ExecuteQuery();
				cleardata();
				ViewPendingData();
				
			}
			if (LBL_CODE.Text!= "" || LBL_SEQ_ID.Text != "") //edit
			{
				if (LBL_SEQ_ID.Text != "")//Edit dari DGR_REQUEST
				{
					conncc.QueryString="PARAM_GENERAL_TPRODUCTLIST_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						TXT_ID.Text +"','"+ TXT_NAME.Text +"','"+
						DDL_TYPE.SelectedValue +"','','','','','','','','', "+
						"'','','','','','','','','','','','','','','','','"+ RBL_Flag.SelectedValue.ToString() +"','"+ LBL_SEQ_ID.Text +"','"+
						DDL_NETWORK_ID.SelectedValue+"', '"+DDL_CLASSIC_TYPE.SelectedValue+"'";

					conncc.ExecuteQuery();
					cleardata();
					ViewPendingData();
					TXT_ID.Enabled=true;
				}
				else if (LBL_SEQ_ID.Text == "") //Edit dari DGR_EXISTING
				{
					conncc.QueryString="select * from PENDING_CC_PRODUCT where PRODUCTID='"+TXT_ID.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)
					{
						conncc.QueryString="select isnull(max(cast(SEQ_ID as integer)),0)+1 SeqId from PENDING_CC_PRODUCT";
						conncc.ExecuteQuery();
						this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SeqId").ToString();
					}
					else
					{
						this.LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
					}
					conncc.QueryString="exec PARAM_GENERAL_TPRODUCTLIST_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						TXT_ID.Text +"','"+ TXT_NAME.Text +"','"+
						DDL_TYPE.SelectedValue +"','','','','','','','','', "+
						"'','','','','','','','','','','','','','','','','"+ RBL_Flag.SelectedValue.ToString() +"','"+ LBL_SEQ_ID.Text +"','"+
						DDL_NETWORK_ID.SelectedValue+"', '"+DDL_CLASSIC_TYPE.SelectedValue+"'";

					conncc.ExecuteQuery();
					cleardata();
					ViewPendingData();
					TXT_ID.Enabled=true;
				}
			}
			LBL_SAVE_MODE.Text="1";
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[33].Text;
					if (status == "2")
					{
						//TXT_ID.Text= cleansText(e.Item.Cells[0].Text);
						break;
					}
					TXT_ID.Text= cleansText(e.Item.Cells[0].Text);
					TXT_NAME.Text= cleansText(e.Item.Cells[3].Text);
					//TXT_TYPE.Text= cleansText(e.Item.Cells[2].Text);
					try
					{
						DDL_TYPE.SelectedValue= cleansText(e.Item.Cells[4].Text);
					}
					catch{}
					try
					{
						DDL_NETWORK_ID.SelectedValue= cleansText(e.Item.Cells[34].Text);
					}
					catch{}
					try
					{
						DDL_CLASSIC_TYPE.SelectedValue= cleansText(e.Item.Cells[35].Text);
					}
					catch{}
					TXT_SICS_CD.Text= cleansText(e.Item.Cells[5].Text);
					try {RBL_Flag.SelectedValue = cleansText(e.Item.Cells[32].Text);}
					catch{}
					LBL_CODE.Text= e.Item.Cells[0].Text;
					LBL_SEQ_ID.Text=e.Item.Cells[31].Text;
					LBL_SAVE_MODE.Text=e.Item.Cells[33].Text;
					TXT_ID.Enabled=false;
					break;
				case "delete":
					//SMEDEV2
					//string prodid=e.Item.Cells[0].Text;
					string seqid=e.Item.Cells[31].Text;
					//string code=e.Item.Cells[0].Text;
					conncc.QueryString="Delete from PENDING_CC_PRODUCT where SEQ_ID='"+ seqid +"'";
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					
					conncc.QueryString="Delete from PENDING_CC_PRODUCT_MAPPING where SEQ_ID='"+ seqid +"'";
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					ViewPendingData();
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
			Response.Redirect("../../HostParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}
}
