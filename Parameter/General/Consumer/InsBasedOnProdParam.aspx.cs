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
using DMS.DBConnection;
using DMS.CuBESCore;

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for InsBasedOnProdParam.
	/// </summary>
	public partial class InsBasedOnProdParam : System.Web.UI.Page
	{
		protected Connection conn,conn2;		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			
			if (!IsPostBack)
			{
				
				dtProd();
				dtAssc();
				viewExistingData();
				viewPendingData();
			}

			this.DGExisting.PageIndexChanged +=new DataGridPageChangedEventHandler(DGExisting_PageIndexChanged);
			this.DGRequest.PageIndexChanged +=new DataGridPageChangedEventHandler(DGRequest_PageIndexChanged);

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");
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
			this.DGExisting.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGExisting_ItemCommand);
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);

		}
		#endregion
		
		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();
			
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		void DGRequest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		void DGExisting_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{			
			DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		}

		private void dtProd()
		{
			string myQuery = "select productid,productname from tproduct";
			
			GlobalTools.fillRefList(this.DDL_PROD,myQuery,conn);
		
		}
		private void dtAssc()
		{
			string myQuery = "select ass_code,ass_desc from rfasstype";

			GlobalTools.fillRefList(this.DDL_ASSC,myQuery,conn);
		
		}
		private void viewExistingData()
		{
			conn.QueryString = "select productname,ass_desc,a.productid,a.ass_code from rfprodasur a left join tproduct b on a.productid=b.productid left join rfasstype c on a.ass_code=c.ass_code where a.active='1'";
			conn.ExecuteQuery();

			DGExisting.DataSource = conn.GetDataTable().Copy();
			
			try 
			{
				DGExisting.DataBind();
			}
			catch 
			{
				DGExisting.CurrentPageIndex = DGExisting.PageCount - 1;
				DGExisting.DataBind();
			}
		}
		private void viewPendingData()
		{
			conn.QueryString = "select productname,ass_desc,a.productid,a.ass_code,ch_sta,case when ch_sta='0' then 'UPDATE' when ch_sta='1' then 'INSERT' when ch_sta='2' then 'DELETE' else '' end CH_STA1 from trfprodasur a left join tproduct b on a.productid=b.productid left join rfasstype c on a.ass_code=c.ass_code";
			conn.ExecuteQuery();
			
			DGRequest.DataSource = conn.GetDataTable().Copy();
			
			try 
			{
				DGRequest.DataBind();
			}
			catch 
			{
				DGRequest.CurrentPageIndex = DGRequest.PageCount - 1;
				DGRequest.DataBind();
			}
		}
		
		private void executeMaker(string id, string asc, string pendingStatus) 
		{
			string myQueryString="";

			conn.QueryString = "SELECT * FROM trfprodasur WHERE PRODUCTID='" +id+ "' and ASS_CODE='"+asc+"'";
			conn.ExecuteQuery();

			int jumlah = conn.GetRowCount();
			
			if (jumlah > 0) 
			{				
				myQueryString = "UPDATE trfprodasur SET ASS_CODE = '"+asc+"' WHERE PRODUCTID = '" +LBL_PR.Text.Trim()+ "' and ASS_CODE='"+LBL_AS.Text.Trim()+"'";
				conn.QueryString = myQueryString;
				
				try 
				{
					conn.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					GlobalTools.popMessage(this,"data already in pending queue");
					if (ex.Message.ToString().IndexOf("truncate") > 0)
						Tools.popMessage(this, "Input melebihi batas !");
				}
			}
			else 
			{
				myQueryString = "INSERT INTO trfprodasur(productid,ass_code,ch_sta) VALUES ('"+id+"', '"+asc+"','"+pendingStatus+"')";
				
				conn.QueryString = myQueryString;
				
				conn.ExecuteQuery();
			
			}

			LBL_SAVEMODE.Text="1";
			
			LBL_PENDING.Text = pendingStatus;
			
			LBL_AS.Text=""; LBL_PR.Text="";
		}
		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					LBL_AS.Text = e.Item.Cells[3].Text;
					LBL_PR.Text = e.Item.Cells[2].Text;
					
					if (e.Item.Cells[2].Text.Trim()=="&nbsp;")
						DDL_PROD.SelectedValue = "";
					else
						DDL_PROD.SelectedValue = e.Item.Cells[2].Text.Trim();
					
					if (e.Item.Cells[3].Text.Trim()=="&nbsp;")
						DDL_ASSC.SelectedValue= "";
					else
						DDL_ASSC.SelectedValue= e.Item.Cells[3].Text.Trim();
					break;

				case "delete":
					string id	= e.Item.Cells[2].Text.Trim();
					string desc = e.Item.Cells[3].Text.Trim();
					
					executeMaker(id,desc,"2");
					viewPendingData();
					
					break;

				default :
					break;
			}
		}
		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[4].Text;
					LBL_AS.Text = e.Item.Cells[3].Text;
					LBL_PR.Text = e.Item.Cells[2].Text;
					
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					
					LBL_ID.Text = e.Item.Cells[4].Text.Trim();
					
					if (e.Item.Cells[2].Text.Trim()=="&nbsp;")
						DDL_PROD.SelectedValue = "";
					else
						DDL_PROD.SelectedValue = e.Item.Cells[2].Text.Trim();
					
					if (e.Item.Cells[3].Text.Trim()=="&nbsp;")
						DDL_ASSC.SelectedValue= "";
					else
						DDL_ASSC.SelectedValue= e.Item.Cells[3].Text.Trim();
					
					break;
				case "delete":
					string pid = e.Item.Cells[2].Text.Trim();
					string asscode = e.Item.Cells[3].Text.Trim();
					
					conn.QueryString = "delete from TRFPRODASUR WHERE PRODUCTID = '"+pid+"' and ASS_CODE = '"+asscode+"'";
					conn.ExecuteQuery();
					viewPendingData();
					
					break;
				default :
					break;
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			executeMaker(DDL_PROD.SelectedValue, DDL_ASSC.SelectedValue,LBL_SAVEMODE.Text.Trim());

			viewPendingData();
			
			LBL_SAVEMODE.Text = "1";
			
			LBL_PR.Text="";
			LBL_AS.Text="";
			
			DDL_ASSC.ClearSelection();
			DDL_PROD.ClearSelection();
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			DDL_ASSC.ClearSelection();

			DDL_PROD.ClearSelection();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleId=40");
		}

	}
}
