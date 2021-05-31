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
	/// Summary description for InterestProductMapParam.
	/// </summary>
	public partial class InterestProductMapParam : System.Web.UI.Page
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
			this.DGExisting.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGExisting_PageIndexChanged);
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

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
			string myQuery="select productid,productname from tproduct";
			GlobalTools.fillRefList(this.DDL_PROD,myQuery,conn);
		
		}
		private void dtAssc()
		{
			string myQuery="select in_type,in_desc from rfinttype";
			GlobalTools.fillRefList(this.DDL_ASSC,myQuery,conn);
		
		}
		private void viewExistingData()
		{
			conn.QueryString="select productname,in_desc,a.productid,a.in_type,a.cd_sibs from rfintprodmap a left join tproduct b on a.productid=b.productid left join rfinttype c on a.in_type=c.in_type where a.active='1'";
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
			conn.QueryString="select productname,in_desc,a.productid,a.in_type,a.cd_sibs,ch_sta,case when ch_sta='0' then 'Edit' when ch_sta='1' then 'Insert' when ch_sta='2' then 'Delete' else '' end CH_STA1 from trfintprodmap a left join tproduct b on a.productid=b.productid left join rfinttype c on a.in_type=c.in_type";
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

		private string getPendingStatus(string saveMode) 
		{
			string status = "";			
			switch (saveMode)
			{
				case "0":
					status = "Update";
					break;
				case "1":
					status = "Insert";
					break;
				case "2":
					status = "Delete";
					break;
				default:
					status = "";
					break;
			}
			return status;
		}

		private string cleansText (string a)
		{
			if ((a.Trim()=="")||(a.Trim()=="nbsp;"))
				a="";
			return a;
		}

		private void executeMaker(string id, string asc, string sibs,string pendingStatus) 
		{
			string myQueryString="";

			conn.QueryString = "SELECT * FROM trfintprodmap WHERE PRODUCTID = '" +id+ "' and IN_TYPE='"+asc+"'";
			conn.ExecuteQuery();

			int jumlah = conn.GetRowCount();
			
			if (jumlah > 0) 
			{				
				myQueryString = "UPDATE trfintprodmap SET cd_sibs='"+sibs+"' WHERE PRODUCTID= '" +id+ "' and IN_TYPE='"+asc+"'";
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
				myQueryString="INSERT INTO trfintprodmap (productid,in_type,ch_sta,cd_sibs) VALUES ('"+id+"', '"+asc+"','"+pendingStatus+"','"+sibs+"')";
				conn.QueryString = myQueryString;
				
				try 
				{
					conn.ExecuteQuery();
				} 
				catch
				{
					Tools.popMessage(this, "Input melebihi batas !");
				}
			}
			
			LBL_SAVEMODE.Text="1";
			
			LBL_AS.Text=""; LBL_PR.Text="";
		}
		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearControls();
			
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					string id	= cleansText(e.Item.Cells[2].Text.Trim());
					string desc = cleansText(e.Item.Cells[3].Text.Trim());
					string sibs = cleansText(e.Item.Cells[4].Text.Trim());
					DDL_ASSC.SelectedValue = desc;
					DDL_PROD.SelectedValue = id;
					TXT_SIBS.Text = sibs;
					DDL_ASSC.Enabled=false;
					DDL_PROD.Enabled=false;
					break;
				case "delete":
					string id1	= cleansText(e.Item.Cells[2].Text.Trim());
					string desc1 = cleansText(e.Item.Cells[3].Text.Trim());
					string sibs1 = cleansText(e.Item.Cells[4].Text.Trim());
					
					executeMaker(id1,desc1,sibs1,"2");
					viewPendingData();
					break;

				default :
					break;
			}
		}
		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			clearControls();
			
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = cleansText(e.Item.Cells[5].Text.Trim());
					string id	= cleansText(e.Item.Cells[2].Text.Trim());
					string desc = cleansText(e.Item.Cells[3].Text.Trim());
					string sibs = cleansText(e.Item.Cells[4].Text.Trim());
					if (LBL_SAVEMODE.Text.Trim()=="2")
					{
						LBL_SAVEMODE.Text="0";
						break;
					}
					DDL_ASSC.SelectedValue = desc;
					DDL_PROD.SelectedValue = id;
					TXT_SIBS.Text = sibs;
					DDL_ASSC.Enabled=false;
					DDL_PROD.Enabled=false;
					break;
				case "delete":
					string id2 = e.Item.Cells[2].Text;
					string id1 = e.Item.Cells[3].Text;
					conn.QueryString = "DELETE FROM Trfintprodmap WHERE PRODUCTID = '" +id2+ "' and IN_TYPE='"+id1+"'";
					conn.ExecuteQuery();
					viewPendingData();
					break;
				default :
					break;
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if ((DDL_ASSC.SelectedValue=="")||(DDL_PROD.SelectedValue=="")||(TXT_SIBS.Text.Trim()==""))
				return;

			conn.QueryString="select * from trfintprodmap where productid='"+DDL_PROD.SelectedValue.Trim()+"' and in_type='"+DDL_ASSC.SelectedValue.Trim()+"'";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
				GlobalTools.popMessage(this,"ID IS ALREADY IN DATABASE, REQUEST CANCELLED");
			else
				executeMaker(DDL_PROD.SelectedValue, DDL_ASSC.SelectedValue,TXT_SIBS.Text.Trim(),LBL_SAVEMODE.Text.Trim());

			viewPendingData();
			DDL_ASSC.Enabled=true;
			DDL_PROD.Enabled=true;			
			LBL_SAVEMODE.Text = "1";
			LBL_PR.Text="";
			LBL_AS.Text="";
			TXT_SIBS.Text="";
			DDL_ASSC.SelectedValue="";
			DDL_PROD.SelectedValue="";
		}
		private void clearControls()
		{
			TXT_SIBS.Text="";
			DDL_ASSC.SelectedValue="";
			DDL_PROD.SelectedValue="";
			LBL_SAVEMODE.Text="1";
			DDL_ASSC.Enabled=true;
			DDL_PROD.Enabled=true;
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../HostParam.aspx?ModuleId=40");
		}

	}
}
