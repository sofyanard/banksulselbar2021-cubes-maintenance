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
	/// Summary description for RFRatingSubQualitative.
	/// </summary>
	public partial class RFRatingSubQualitative : System.Web.UI.Page
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

				GlobalTools.fillRefList(DDL_QUALITATIVEID,"exec PARAM_GENERAL_RFRATINGSUBQUAL_FILLDDLQUAL",false,conn);

				viewExistingData();
				viewPendingData();
				nextID();
			}
		}

		private void nextID()
		{
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGSUBQUAL_NEXTID '" + DDL_QUALITATIVEID.SelectedValue + "'";
			conn.ExecuteQuery();
			TXT_ID.Text = conn.GetFieldValue("NEXTID");
		}

		private void viewExistingData()
		{
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGSUBQUAL_VIEWEXIST '" + DDL_QUALITATIVEID.SelectedValue + "'";
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
				if (DGExisting.Items[i].Cells[5].Text.Trim() =="0" ) 
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
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGSUBQUAL_VIEWREQUEST '" + DDL_QUALITATIVEID.SelectedValue + "'";
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
			DDL_QUALITATIVEID.SelectedValue = "";
			DDL_QUALITATIVEID.Enabled = true;
		}

		private void executeMaker(string id, string xid, string desc, string pendingStatus) 
		{
			conn.QueryString = "SELECT COUNT(*) JUMLAH FROM PENDING_RFRATING_SUBQUALITATIVE WHERE SUBQUALITATIVEID ='" + id + "'";
			conn.ExecuteQuery();

			int jumlah = Convert.ToInt16(conn.GetFieldValue("JUMLAH"));
			
			if (jumlah > 0) 
			{				
				conn.QueryString = "UPDATE PENDING_RFRATING_SUBQUALITATIVE SET SUBQUALITATIVEDESC = '" + desc + "', " +
					"CH_STA = '" + pendingStatus + "' WHERE SUBQUALITATIVEID = '" + id + "'";
				conn.ExecuteQuery();
			}
			else 
			{
				if (LBL_ACTIVE.Text.Trim() == "1")
					conn.QueryString = "INSERT INTO PENDING_RFRATING_SUBQUALITATIVE " +
						"VALUES ('"+id+"', '"+xid+"', '"+desc+"', '1', '"+pendingStatus+"')";
				else if (LBL_ACTIVE.Text.Trim() == "0")
					conn.QueryString = "INSERT INTO PENDING_RFRATING_SUBQUALITATIVE " +
						"VALUES ('"+id+"', '"+xid+"', '"+desc+"', '0', '"+pendingStatus+"')";
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

		protected void DDL_QUALITATIVEID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			nextID();
			viewExistingData();
			viewPendingData();
		}

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
			if (TXT_ID.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"ID tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_ID);
				return;
			}
			else if (TXT_DESC.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Description tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_DESC);
				return;
			}
			else if (DDL_QUALITATIVEID.SelectedValue.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Qualitative tidak boleh kosong!");
				GlobalTools.SetFocus(this,DDL_QUALITATIVEID);
				return;
			}
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select active from rfrating_subqualitative where subqualitativeid ='" + TXT_ID.Text.Trim() + "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					active = conn.GetFieldValue("active");
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
			executeMaker(TXT_ID.Text.Trim(), DDL_QUALITATIVEID.SelectedValue.Trim(), TXT_DESC.Text.Trim(), LBL_SAVEMODE.Text.Trim());
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
			string id, xid;

			clearControls();
			DDL_QUALITATIVEID.Enabled = false;
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[4].Text;
					try
					{
						DDL_QUALITATIVEID.SelectedValue = e.Item.Cells[1].Text;
					}
					catch(Exception ex){}
					break;

				case "delete":					
					id = e.Item.Cells[0].Text.Trim();
					xid = e.Item.Cells[1].Text.Trim();
					
					executeMaker(id, xid, e.Item.Cells[4].Text, "2");
					viewPendingData();
					nextID();
					break;

				case "undelete":					
					id = e.Item.Cells[0].Text.Trim();
					xid = e.Item.Cells[1].Text.Trim();
					
					executeMaker(id, xid, e.Item.Cells[4].Text, "0");
					viewPendingData();
					nextID();
					break;

				default :
					break;
			}
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearControls();
			DDL_QUALITATIVEID.Enabled = false;
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[6].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[4].Text;
					try
					{
						DDL_QUALITATIVEID.SelectedValue = e.Item.Cells[1].Text;
					}
					catch(Exception ex){}
					break;

				case "delete":
					string id = e.Item.Cells[0].Text;
					conn.QueryString = "delete from PENDING_RFRATING_SUBQUALITATIVE WHERE SUBQUALITATIVEID = '" + id + "'";
					conn.ExecuteQuery();
					viewPendingData();
					nextID();
					break;

				default :
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}
	}
}
