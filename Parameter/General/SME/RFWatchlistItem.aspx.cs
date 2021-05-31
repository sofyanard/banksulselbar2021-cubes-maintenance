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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFWatchlistItem.
	/// </summary>
	public partial class RFWatchlistItem : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack) 
			{
				LBL_SAVEMODE.Text = "1";
				LBL_ACTIVE.Text = Request.QueryString["active"];
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

				GlobalTools.fillRefList(DDL_BUSSUNIT, "SELECT * FROM RFBUSINESSUNIT WHERE ACTIVE='1'", true, conn);

				viewExistingData();
				viewPendingData();
				nextID();
			}
		}

		private void nextID()
		{
			conn.QueryString = "exec PARAM_GENERAL_RFWATCHLIST_NEXTID '" + DDL_BUSSUNIT.SelectedValue.Trim() + "'";
			conn.ExecuteQuery();
			TXT_ID.Text = conn.GetFieldValue("NEXTID");
		}

		private void viewExistingData()
		{
			if (DDL_BUSSUNIT.SelectedValue != "")
				conn.QueryString = "select * from VW_PARAM_GENERAL_RFWATCHLIST_VIEWEXIST where BUSSUNITID = '" + DDL_BUSSUNIT.SelectedValue.Trim() + "'";
			else
				conn.QueryString = "select * from VW_PARAM_GENERAL_RFWATCHLIST_VIEWEXIST ";
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

			for (int i =0; i < DGExisting.Items.Count; i++)
			{			
				if (DGExisting.Items[i].Cells[4].Text.Trim() != "1" ) 
				{		// active = 0
					LinkButton l_del = (LinkButton) DGExisting.Items[i].FindControl("lnk_RfDelete");
					l_del.CommandName = "Undelete";
					l_del.Text = "Undelete";

					LinkButton l_edit = (LinkButton) DGExisting.Items[i].FindControl("lnk_RfEdit");
					l_edit.Visible = false;
				}
			}
		}

		private void viewPendingData()
		{
			if (DDL_BUSSUNIT.SelectedValue != "")
				conn.QueryString = "select * from VW_PARAM_GENERAL_RFWATCHLIST_VIEWREQUEST where BUSSUNITID = '" + DDL_BUSSUNIT.SelectedValue.Trim() + "'";
			else
				conn.QueryString = "select * from VW_PARAM_GENERAL_RFWATCHLIST_VIEWREQUEST ";
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

		private void clearControls() 
		{
			TXT_ID.Text   = "";
			TXT_DESC.Text = "";
			try { DDL_BUSSUNIT.SelectedValue = ""; } 
			catch {}
			DDL_BUSSUNIT.Enabled = true;
		}

		private void executeMaker(string bu, string id, string desc, string pendingStatus)
		{
			conn.QueryString = "SELECT COUNT(*) JUMLAH FROM PENDING_RFWATCHLIST_ITEM WHERE BUSSUNITID = '" + bu + "' AND WATCHID ='" + id + "'";
			conn.ExecuteQuery();

			int jumlah = Convert.ToInt16(conn.GetFieldValue("JUMLAH"));
			
			if (jumlah > 0) 
			{				
				conn.QueryString = "UPDATE PENDING_RFWATCHLIST_ITEM SET WATCHDESC = '" + desc + "', " +
					"CH_STA = '" + pendingStatus + "' WHERE BUSSUNITID = '" + bu + "' AND WATCHID ='" + id + "'";
				conn.ExecuteQuery();
			}
			else 
			{
				if (LBL_ACTIVE.Text.Trim() == "1")
					conn.QueryString = "INSERT INTO PENDING_RFWATCHLIST_ITEM (BUSSUNITID, WATCHID, WATCHDESC, ACTIVE, CH_STA) " +
						"VALUES ('" + bu + "', '" + id + "', '" + desc + "', '1', '" + pendingStatus + "')";
				else if (LBL_ACTIVE.Text.Trim() == "0")
					conn.QueryString = "INSERT INTO PENDING_RFWATCHLIST_ITEM (BUSSUNITID, WATCHID, WATCHDESC, ACTIVE, CH_STA) " +
						"VALUES ('" + bu + "', '" + id + "', '" + desc + "', '0', '" + pendingStatus + "')";
				try 
				{
					conn.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					Response.Write("<!--"+ex.ToString()+"-->");
				}
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
			this.DGExisting.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGExisting_ItemCommand);
			this.DGExisting.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGExisting_PageIndexChanged);
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion

		private void DGExisting_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
			nextID();
		}

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
			nextID();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string active="0";
			if (DDL_BUSSUNIT.SelectedValue.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Business Unit tidak boleh kosong!");
				GlobalTools.SetFocus(this,DDL_BUSSUNIT);
				return;
			}
			if (TXT_ID.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Code tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_ID);
				return;
			}
			if (TXT_DESC.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Desc tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_DESC);
				return;
			}

			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select ACTIVE from RFWATCHLIST_ITEM where BUSSUNITID = '" + DDL_BUSSUNIT.SelectedValue.Trim() + "' and WATCHID = '" + TXT_ID.Text.Trim() + "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					active = conn.GetFieldValue("ACTIVE");
					if (active == "1")
					{
						//Tools.popMessage(this, "ID has already been used! Request canceled!");
						return;
					}
					else
					{
						LBL_SAVEMODE.Text ="0";
					}
				}
			}

			executeMaker(DDL_BUSSUNIT.SelectedValue.Trim(), TXT_ID.Text.Trim(), TXT_DESC.Text.Trim(), LBL_SAVEMODE.Text.Trim());
			viewPendingData();
			clearControls();
			nextID();
			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();
			nextID();
		}

		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string bu, id, desc;
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					try {DDL_BUSSUNIT.SelectedValue = e.Item.Cells[0].Text;}
					catch {}
					DDL_BUSSUNIT.Enabled = false;
					TXT_ID.Text = e.Item.Cells[2].Text;
					TXT_DESC.Text = e.Item.Cells[3].Text;
					break;

				case "delete":
					bu = e.Item.Cells[0].Text.Trim();
					id = e.Item.Cells[2].Text.Trim();
					desc = e.Item.Cells[3].Text.Trim();
					
					executeMaker(bu, id, desc, "2");
					viewPendingData();
					nextID();
					break;

				case "undelete":					
					bu = e.Item.Cells[0].Text.Trim();
					id = e.Item.Cells[2].Text.Trim();
					desc = e.Item.Cells[3].Text.Trim();
					
					executeMaker(bu, id, desc, "0");
					viewPendingData();
					nextID();
					break;

				default :
					break;
			}
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string bu, id, desc;
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[5].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					try {DDL_BUSSUNIT.SelectedValue = e.Item.Cells[0].Text;}
					catch {}
					DDL_BUSSUNIT.Enabled = false;
					TXT_ID.Text = e.Item.Cells[2].Text;
					TXT_DESC.Text = e.Item.Cells[3].Text;
					break;

				case "delete":
					bu = e.Item.Cells[0].Text.Trim();
					id = e.Item.Cells[2].Text.Trim();

					conn.QueryString = "delete from PENDING_RFWATCHLIST_ITEM where BUSSUNITID = '" + bu + "' and WATCHID = '" + id + "'";
					conn.ExecuteQuery();
					viewPendingData();
					nextID();
					break;

				default :
					break;
			}
		}

		protected void DDL_BUSSUNIT_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			viewExistingData();
			viewPendingData();
			nextID();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}
	}
}
