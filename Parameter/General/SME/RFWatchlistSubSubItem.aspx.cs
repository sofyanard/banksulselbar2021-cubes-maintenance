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
	/// Summary description for RFWatchlistSubSubItem.
	/// </summary>
	public partial class RFWatchlistSubSubItem : System.Web.UI.Page
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
				GlobalTools.fillRefList(DDL_PILARBI, "SELECT * FROM VW_RFPILARBI WHERE ACTIVE='1'", true, conn);
				FillDDLWatch();
				FillDDLSubWatch();

				viewExistingData();
				viewPendingData();
				nextID();
			}
		}

		private void FillDDLWatch()
		{
			string qry = "EXEC PARAM_GENERAL_RFSUBSUBWATCHLIST_FILLDDLWATCH '" + DDL_BUSSUNIT.SelectedValue.Trim() + "'";
			GlobalTools.fillRefList(DDL_WATCHID,qry,false,conn);
		}

		private void FillDDLSubWatch()
		{
			string qry = "EXEC PARAM_GENERAL_RFSUBSUBWATCHLIST_FILLDDLSUBWATCH '" + DDL_WATCHID.SelectedValue.Trim() + "'";
			GlobalTools.fillRefList(DDL_SUBWATCHID,qry,false,conn);
		}

		private void nextID()
		{
			conn.QueryString = "exec PARAM_GENERAL_RFSUBSUBWATCHLIST_NEXTID '" + DDL_SUBWATCHID.SelectedValue + "'";
			conn.ExecuteQuery();
			TXT_ID.Text = conn.GetFieldValue("NEXTID");
		}

		private void viewExistingData()
		{
			string qry = "select * from VW_PARAM_GENERAL_RFSUBSUBWATCHLIST_VIEWEXIST where 1=1 ";
			if (DDL_BUSSUNIT.SelectedValue != "")
				qry = qry + "and BUSSUNITID = '" + DDL_BUSSUNIT.SelectedValue.Trim() + "' ";
			if (DDL_WATCHID.SelectedValue != "")
				qry = qry + "and WATCHID = '" + DDL_WATCHID.SelectedValue.Trim() + "' ";
			if (DDL_SUBWATCHID.SelectedValue != "")
				qry = qry + "and SUBWATCHID = '" + DDL_SUBWATCHID.SelectedValue.Trim() + "' ";
			conn.QueryString = qry;
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
				if (DGExisting.Items[i].Cells[17].Text.Trim() != "1" ) 
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
			string qry = "select * from VW_PARAM_GENERAL_RFSUBSUBWATCHLIST_VIEWREQUEST where 1=1 ";
			if (DDL_BUSSUNIT.SelectedValue != "")
				qry = qry + "and BUSSUNITID = '" + DDL_BUSSUNIT.SelectedValue.Trim() + "' ";
			if (DDL_WATCHID.SelectedValue != "")
				qry = qry + "and WATCHID = '" + DDL_WATCHID.SelectedValue.Trim() + "' ";
			if (DDL_SUBWATCHID.SelectedValue != "")
				qry = qry + "and SUBWATCHID = '" + DDL_SUBWATCHID.SelectedValue.Trim() + "' ";
			conn.QueryString = qry;
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
			try {DDL_BUSSUNIT.SelectedValue = "";}
			catch {}
			DDL_BUSSUNIT.Enabled = true;
			try {DDL_WATCHID.SelectedValue = "";}
			catch {}
			DDL_WATCHID.Enabled = true;
			try {DDL_SUBWATCHID.SelectedValue = "";}
			catch {}
			DDL_SUBWATCHID.Enabled = true;
			TXT_WEIGHT.Text = "";
			CHK_ISMANDATORY.Checked = false;
			CHK_RVWKOLBI.Checked = false;
			try {DDL_PILARBI.SelectedValue = "";}
			catch {}
			CHK_ISIGNORED.Checked = false;
		}

		private void executeMaker(string xid, string id, string desc, string weight, string ismandatory, string rvwkolbi, string pilarbi, string isignored, string pendingStatus)
		{
			conn.QueryString = "SELECT COUNT(*) JUMLAH FROM PENDING_RFWATCHLIST_SUBSUBITEM WHERE SUBWATCHID = '" + xid + "' AND SUBSUBWATCHID ='" + id + "'";
			conn.ExecuteQuery();

			int jumlah = Convert.ToInt16(conn.GetFieldValue("JUMLAH"));
			
			if (jumlah > 0) 
			{				
				conn.QueryString = "UPDATE PENDING_RFWATCHLIST_SUBSUBITEM SET SUBSUBWATCHDESC = '" + desc + "', " +
					"WEIGHT = '" + weight + "', ISMANDATORY = '" + ismandatory + "', RVWKOLBI = '" + rvwkolbi + "', PILARBI = '" + pilarbi + "', ISIGNORED = '" + isignored + "', " +
					"CH_STA = '" + pendingStatus + "' WHERE SUBWATCHID = '" + xid + "' AND SUBSUBWATCHID ='" + id + "'";
				conn.ExecuteQuery();
			}
			else 
			{
				if (LBL_ACTIVE.Text.Trim() == "1")
					conn.QueryString = "INSERT INTO PENDING_RFWATCHLIST_SUBSUBITEM (SUBWATCHID, SUBSUBWATCHID, SUBSUBWATCHDESC, WEIGHT, ISMANDATORY, ISIGNORED, ACTIVE, CH_STA, RVWKOLBI, PILARBI) " +
						"VALUES ('" + xid + "', '" + id + "', '" + desc + "', '" + weight + "', '" + ismandatory + "', '" + isignored + "', '1', '" + pendingStatus + "', '" + rvwkolbi + "', '" + pilarbi + "')";
				else if (LBL_ACTIVE.Text.Trim() == "0")
					conn.QueryString = "INSERT INTO PENDING_RFWATCHLIST_SUBSUBITEM (SUBWATCHID, SUBSUBWATCHID, SUBSUBWATCHDESC, WEIGHT, ISMANDATORY, ISIGNORED, ACTIVE, CH_STA, RVWKOLBI, PILARBI) " +
						"VALUES ('" + xid + "', '" + id + "', '" + desc + "', '" + weight + "', '" + ismandatory + "', '" + isignored + "', '0', '" + pendingStatus + "', '" + rvwkolbi + "', '" + pilarbi + "')";
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
			if (DDL_SUBWATCHID.SelectedValue.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Watchlist Sub Item tidak boleh kosong!");
				GlobalTools.SetFocus(this,DDL_SUBWATCHID);
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
				conn.QueryString = "select ACTIVE from RFWATCHLIST_SUBSUBITEM where SUBWATCHID = '" + DDL_SUBWATCHID.SelectedValue.Trim() + "' and SUBSUBWATCHID = '" + TXT_ID.Text.Trim() + "'";
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

			executeMaker(DDL_SUBWATCHID.SelectedValue.Trim(), 
				TXT_ID.Text.Trim(), 
				TXT_DESC.Text.Trim(), 
				TXT_WEIGHT.Text.Trim(),
				(CHK_ISMANDATORY.Checked==true?"1":"0"), 
				(CHK_RVWKOLBI.Checked==true?"1":"0"), 
				DDL_PILARBI.SelectedValue.Trim(), 
				(CHK_ISIGNORED.Checked==true?"1":"0"), 
				LBL_SAVEMODE.Text.Trim());
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
			string xid, id, desc, weight, ismandatory, isignored, rvwkolbi, pilarbi;
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					try {DDL_BUSSUNIT.SelectedValue = e.Item.Cells[0].Text;}
					catch {}
					DDL_BUSSUNIT.Enabled = false;
					try {DDL_WATCHID.SelectedValue = e.Item.Cells[2].Text;}
					catch {}
					DDL_WATCHID.Enabled = false;
					try {DDL_SUBWATCHID.SelectedValue = e.Item.Cells[4].Text;}
					catch {}
					DDL_SUBWATCHID.Enabled = false;
					TXT_ID.Text = e.Item.Cells[6].Text;
					TXT_DESC.Text = e.Item.Cells[7].Text;
					TXT_WEIGHT.Text = e.Item.Cells[8].Text;
					CHK_ISMANDATORY.Checked = (e.Item.Cells[9].Text.Trim()=="1"?true:false);
					CHK_RVWKOLBI.Checked = (e.Item.Cells[11].Text.Trim()=="1"?true:false);
					try {DDL_PILARBI.SelectedValue = e.Item.Cells[13].Text;}
					catch {}
					CHK_ISIGNORED.Checked = (e.Item.Cells[15].Text.Trim()=="1"?true:false);
					break;

				case "delete":
					xid = e.Item.Cells[4].Text.Trim();
					id = e.Item.Cells[6].Text.Trim();
					desc = e.Item.Cells[7].Text.Trim();
					weight = e.Item.Cells[8].Text.Trim();
					ismandatory = e.Item.Cells[9].Text.Trim();
					rvwkolbi = e.Item.Cells[11].Text.Trim();
					pilarbi = e.Item.Cells[13].Text.Trim();
					isignored = e.Item.Cells[15].Text.Trim();
					
					executeMaker(xid, id, desc, weight, ismandatory, rvwkolbi, pilarbi, isignored, "2");
					viewPendingData();
					nextID();
					break;

				case "undelete":					
					xid = e.Item.Cells[4].Text.Trim();
					id = e.Item.Cells[6].Text.Trim();
					desc = e.Item.Cells[7].Text.Trim();
					weight = e.Item.Cells[8].Text.Trim();
					ismandatory = e.Item.Cells[9].Text.Trim();
					rvwkolbi = e.Item.Cells[11].Text.Trim();
					pilarbi = e.Item.Cells[13].Text.Trim();
					isignored = e.Item.Cells[15].Text.Trim();
					
					executeMaker(xid, id, desc, weight, ismandatory, rvwkolbi, pilarbi, isignored, "0");
					viewPendingData();
					nextID();
					break;

				default :
					break;
			}
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string xid, id, desc, weight, ismandatory, isignored;
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[18].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					LBL_SAVEMODE.Text = "0";
					try {DDL_BUSSUNIT.SelectedValue = e.Item.Cells[0].Text;}
					catch {}
					DDL_BUSSUNIT.Enabled = false;
					try {DDL_WATCHID.SelectedValue = e.Item.Cells[2].Text;}
					catch {}
					DDL_WATCHID.Enabled = false;
					try {DDL_SUBWATCHID.SelectedValue = e.Item.Cells[4].Text;}
					catch {}
					DDL_SUBWATCHID.Enabled = false;
					TXT_ID.Text = e.Item.Cells[6].Text;
					TXT_DESC.Text = e.Item.Cells[7].Text;
					TXT_WEIGHT.Text = e.Item.Cells[8].Text;
					CHK_ISMANDATORY.Checked = (e.Item.Cells[9].Text.Trim()=="1"?true:false);
					CHK_RVWKOLBI.Checked = (e.Item.Cells[11].Text.Trim()=="1"?true:false);
					try {DDL_PILARBI.SelectedValue = e.Item.Cells[13].Text;}
					catch {}
					CHK_ISIGNORED.Checked = (e.Item.Cells[15].Text.Trim()=="1"?true:false);
					break;

				case "delete":
					xid = e.Item.Cells[4].Text.Trim();
					id = e.Item.Cells[6].Text.Trim();

					conn.QueryString = "delete from PENDING_RFWATCHLIST_SUBSUBITEM where SUBWATCHID = '" + xid + "' and SUBSUBWATCHID = '" + id + "'";
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
			FillDDLWatch();
			FillDDLSubWatch();
			viewExistingData();
			viewPendingData();
			nextID();
		}

		protected void DDL_WATCHID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillDDLSubWatch();
			viewExistingData();
			viewPendingData();
			nextID();
		}

		protected void DDL_SUBWATCHID_SelectedIndexChanged(object sender, System.EventArgs e)
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
