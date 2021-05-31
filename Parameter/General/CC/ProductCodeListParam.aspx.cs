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
	public class ProductCodeListParam : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton BTN_BACK;
		protected System.Web.UI.WebControls.Button BTN_SAVE;
		protected System.Web.UI.WebControls.Button BTN_CANCEL;
		protected System.Web.UI.WebControls.DataGrid DGR_EXISTING;
		protected System.Web.UI.WebControls.TextBox TXT_NAME;
		protected System.Web.UI.WebControls.TextBox TXT_ID;
		protected System.Web.UI.WebControls.DataGrid DGR_REQUEST;
		protected System.Web.UI.WebControls.Label LBL_SAVE_MODE;
		protected Connection conncc,conn;
		protected System.Web.UI.WebControls.Label LBL_CODE;		

		private void Page_Load(object sender, System.EventArgs e)
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

		private void ViewExistingData()
		{
			conncc.QueryString="select * from VW_CC_RFPRODUCTCODE ";
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
			conncc.QueryString="select * from VW_PARAM_CC_PENDING_RFPRODUCTCODE";
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
			this.BTN_BACK.Click += new System.Web.UI.ImageClickEventHandler(this.BTN_BACK_Click);
			this.BTN_SAVE.Click += new System.EventHandler(this.BTN_SAVE_Click);
			this.BTN_CANCEL.Click += new System.EventHandler(this.BTN_CANCEL_Click);
			this.DGR_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_ItemCommand);
			this.DGR_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

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
			LBL_SAVE_MODE.Text="";
		}

		private void BTN_CANCEL_Click(object sender, System.EventArgs e)
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
					TXT_NAME.Text= cleansText(e.Item.Cells[1].Text);
					LBL_CODE.Text= cleansText(e.Item.Cells[0].Text);
					TXT_ID.Enabled=false;
					break;
				case "delete":
					LBL_SAVE_MODE.Text="2";
					
					//cek tabel pending
					conncc.QueryString="select * from PENDING_CC_RFPRODUCTCODE where IDPRODUCT='"+e.Item.Cells[0].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update Pending_CC_RFPRODUCTCODE set PENDING_STATUS='"+LBL_SAVE_MODE.Text+"' where IDPRODUCT='"+e.Item.Cells[0].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
//						conncc.QueryString="PARAM_GENERAL_RFPRODUCTCODELIST_MAKER '"+LBL_SAVE_MODE.Text+"','"+
//							e.Item.Cells[0].Text +"','"+ e.Item.Cells[1].Text +"','"+
//							e.Item.Cells[2].Text +"','','','','','','','','', "+
//							"'','','','','','','','','','','','','','','','1','"+ e.Item.Cells[5].Text +"','"+ LBL_SEQ_ID.Text +"'";
						conncc.QueryString="PARAM_GENERAL_RFPRODUCTCODELIST_MAKER '"+LBL_SAVE_MODE.Text+"','"+
							e.Item.Cells[0].Text +"','"+ e.Item.Cells[1].Text +"','1','"+ e.Item.Cells[0].Text +"'";
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

		private void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_CODE.Text =="") //input baru
			{
//				conncc.QueryString="PARAM_GENERAL_RFPRODUCTCODELIST_MAKER '1','"+
//					TXT_ID.Text +"','"+ TXT_NAME.Text +"','"+
//					TXT_TYPE.Text +"','','','','','','','','', "+
//					"'','','','','','','','','','','','','','','','','"+ RBL_Flag.SelectedValue.ToString() +"','"+ LBL_SEQ_ID.Text +"'";
				conncc.QueryString="PARAM_GENERAL_RFPRODUCTCODELIST_MAKER '1','"+
					TXT_ID.Text +"','"+ TXT_NAME.Text +"','','"+ TXT_ID.Text +"'";

				conncc.ExecuteQuery();
				cleardata();
				ViewPendingData();
				
			}
			if (LBL_CODE.Text!= "") //edit
			{
//				if (LBL_SEQ_ID.Text != "")//Edit dari DGR_REQUEST
//				{
//					conncc.QueryString="PARAM_GENERAL_RFPRODUCTCODELIST_MAKER '"+LBL_SAVE_MODE.Text+"','"+
//						TXT_ID.Text +"','"+ TXT_NAME.Text +"','"+
//						TXT_TYPE.Text +"','','','','','','','','', "+
//						"'','','','','','','','','','','','','','','','','"+ RBL_Flag.SelectedValue.ToString() +"','"+ LBL_SEQ_ID.Text +"'";
//					conncc.QueryString="PARAM_GENERAL_RFPRODUCTCODELIST_MAKER '"+LBL_SAVE_MODE.Text+"','"+
//						e.Item.Cells[0].Text +"','"+ e.Item.Cells[1].Text +"','1','"+ e.Item.Cells[0].Text +"'";
//					conncc.ExecuteQuery();
//					cleardata();
//					ViewPendingData();
//					TXT_ID.Enabled=true;
//				}
//				else if (LBL_SEQ_ID.Text == "") //Edit dari DGR_EXISTING
//				{
//					conncc.QueryString="exec PARAM_GENERAL_RFPRODUCTCODELIST_MAKER '"+LBL_SAVE_MODE.Text+"','"+
//						TXT_ID.Text +"','"+ TXT_NAME.Text +"','"+
//						TXT_TYPE.Text +"','','','','','','','','', "+
//						"'','','','','','','','','','','','','','','','','"+ RBL_Flag.SelectedValue.ToString() +"','"+ LBL_SEQ_ID.Text +"'";
				conncc.QueryString="PARAM_GENERAL_RFPRODUCTCODELIST_MAKER '"+LBL_SAVE_MODE.Text+"','"+
					TXT_ID.Text +"','"+ TXT_NAME.Text +"','','"+ TXT_ID.Text +"'";
				conncc.ExecuteQuery();
				cleardata();
				ViewPendingData();
				TXT_ID.Enabled=true;
//				}
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
					string status = e.Item.Cells[31].Text;
					if (status == "2")
					{
						//TXT_ID.Text= cleansText(e.Item.Cells[0].Text);
						break;
					}
					TXT_ID.Text= cleansText(e.Item.Cells[0].Text);
					TXT_NAME.Text= cleansText(e.Item.Cells[1].Text);
					LBL_SAVE_MODE.Text=e.Item.Cells[31].Text;
					TXT_ID.Enabled=false;
					break;
				case "delete":
					string seqid=e.Item.Cells[29].Text;
					
					conncc.QueryString="Delete from PENDING_CC_RFPRODUCTCODE where IDPRODUCT = '"+ seqid +"'";
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					ViewPendingData();
					break;
			}
		}

		private void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}
}
