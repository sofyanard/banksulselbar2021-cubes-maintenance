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
	/// Summary description for RFRatingQualitative.
	/// </summary>
	public partial class RFRatingQualitative : System.Web.UI.Page
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

				GlobalTools.fillRefList(DDL_PARENT,"exec PARAM_GENERAL_RFRATINGQUAL_FILLDDLPARENT",false,conn);
				GlobalTools.fillRefList(DDL_QUALTPL_ID, "exec PARAM_GENERAL_RFRATINGQUAL_FILLDDLTEMPLATE",false,conn);

				viewExistingData();
				viewPendingData();
				nextID();
			}
		}

		private void nextID()
		{
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGQUAL_NEXTID ";
			conn.ExecuteQuery();
			TXT_ID.Text = conn.GetFieldValue("NEXTID");
		}

		private void viewExistingData()
		{
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGQUAL_VIEWEXIST ";
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
				if (DGExisting.Items[i].Cells[8].Text.Trim() =="0" ) 
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
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGQUAL_VIEWREQUEST ";
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
			CHK_HAVEPARENT.Checked = false;
			CHK_HAVECHILD.Checked = false;
			try { DDL_PARENT.SelectedValue = ""; } 
			catch {}
			DDL_PARENT.Enabled = false;
			DDL_PARENT.CssClass = "";
			try {DDL_QUALTPL_ID.SelectedValue = "";}
			catch {}
		}

		private void executeMaker(string id, string desc, string haveparent, string parent, string havechild, string pendingStatus, string qualtplid) 
		{
			conn.QueryString = "SELECT COUNT(*) JUMLAH FROM PENDING_RFRATING_QUALITATIVE WHERE QUALITATIVEID ='" + id + "'";
			conn.ExecuteQuery();

			int jumlah = Convert.ToInt16(conn.GetFieldValue("JUMLAH"));
			
			if (jumlah > 0) 
			{				
				conn.QueryString = "UPDATE PENDING_RFRATING_QUALITATIVE SET QUALITATIVEDESC = '" + desc + "', " +
					"HAVEPARENT = '" + haveparent + "', " +
					"PARENT = '" + parent + "', " +
					"HAVECHILD = '" + havechild + "', " +
					"CH_STA = '" + pendingStatus + "', " +
					"QUALTPL_ID = '" + qualtplid + "' " +
					"WHERE QUALITATIVEID = '" + id + "'";
				conn.ExecuteQuery();
			}
			else 
			{
				if (LBL_ACTIVE.Text.Trim() == "1")
					conn.QueryString = "INSERT INTO PENDING_RFRATING_QUALITATIVE (QUALITATIVEID, QUALITATIVEDESC, HAVEPARENT, PARENT, HAVECHILD, ACTIVE, CH_STA, QUALTPL_ID) " +
						"VALUES ('" + id + "', '" + desc + "', '" + haveparent + "', '" + parent + "', '" + havechild + "', '1', '"+pendingStatus+"', '" + qualtplid + "')";
				else if (LBL_ACTIVE.Text.Trim() == "0")
					conn.QueryString = "INSERT INTO PENDING_RFRATING_QUALITATIVE (QUALITATIVEID, QUALITATIVEDESC, HAVEPARENT, PARENT, HAVECHILD, ACTIVE, CH_STA, QUALTPL_ID) " +
						"VALUES ('" + id + "', '" + desc + "', '" + haveparent + "', '" + parent + "', '" + havechild + "', '0', '"+pendingStatus+"', '" + qualtplid + "')";
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
			if (TXT_ID.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"ID tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_ID);
				return;
			}
			if (TXT_DESC.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Description tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_DESC);
				return;
			}
			if (CHK_HAVEPARENT.Checked == true)
			{
				if (DDL_PARENT.SelectedValue == "")
				{
					GlobalTools.popMessage(this,"Parent tidak boleh kosong!");
					GlobalTools.SetFocus(this,DDL_PARENT);
					return;
				}
			}
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select active from rfrating_qualitative where qualitativeid ='" + TXT_ID.Text.Trim() + "'";
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
			string haveparent, havechild;
			if (CHK_HAVEPARENT.Checked == true) { haveparent = "1"; }
			else { haveparent = "0"; }
			if (CHK_HAVECHILD.Checked == true) { havechild = "1"; }
				else { havechild = "0"; }
			executeMaker(TXT_ID.Text.Trim(), TXT_DESC.Text.Trim(), haveparent, DDL_PARENT.SelectedValue.Trim(), havechild, LBL_SAVEMODE.Text.Trim(), DDL_QUALTPL_ID.SelectedValue.Trim());
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
			string id;
			string haveparent, havechild;

			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[1].Text;
					haveparent = e.Item.Cells[2].Text;
					if (haveparent == "1")
					{
						CHK_HAVEPARENT.Checked = true;
						try { DDL_PARENT.SelectedValue = e.Item.Cells[4].Text.Trim(); } 
						catch{}
						DDL_PARENT.Enabled = true;
						DDL_PARENT.CssClass = "mandatory";
					}
					else
					{
						CHK_HAVEPARENT.Checked = false;
						try { DDL_PARENT.SelectedValue = ""; } 
						catch{}
						DDL_PARENT.Enabled = false;
						DDL_PARENT.CssClass = "";
					}
					havechild = e.Item.Cells[6].Text;
					if (havechild == "1")
					{
						CHK_HAVECHILD.Checked = true;
					}
					else
					{
						CHK_HAVECHILD.Checked = false;
					}
					try {DDL_QUALTPL_ID.SelectedValue = e.Item.Cells[8].Text.Trim();}
					catch {}
					break;

				case "delete":					
					id = e.Item.Cells[0].Text.Trim();
					
					executeMaker(id, e.Item.Cells[1].Text, e.Item.Cells[2].Text, e.Item.Cells[4].Text, e.Item.Cells[6].Text, "2", e.Item.Cells[8].Text);
					viewPendingData();
					nextID();
					break;

				case "undelete":					
					id = e.Item.Cells[0].Text.Trim();
					
					executeMaker(id, e.Item.Cells[1].Text, e.Item.Cells[2].Text, e.Item.Cells[4].Text, e.Item.Cells[6].Text, "0", e.Item.Cells[8].Text);
					viewPendingData();
					nextID();
					break;

				default :
					break;
			}
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string haveparent, havechild;
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[9].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[1].Text;
					haveparent = e.Item.Cells[2].Text;
					if (haveparent == "1")
					{
						CHK_HAVEPARENT.Checked = true;
						try { DDL_PARENT.SelectedValue = e.Item.Cells[4].Text.Trim(); } 
						catch{}
						DDL_PARENT.Enabled = true;
						DDL_PARENT.CssClass = "mandatory";
					}
					else
					{
						CHK_HAVEPARENT.Checked = false;
						try { DDL_PARENT.SelectedValue = ""; } 
						catch{}
						DDL_PARENT.Enabled = false;
						DDL_PARENT.CssClass = "";
					}
					havechild = e.Item.Cells[6].Text;
					if (havechild == "1")
					{
						CHK_HAVECHILD.Checked = true;
					}
					else
					{
						CHK_HAVECHILD.Checked = false;
					}
					try {DDL_QUALTPL_ID.SelectedValue = e.Item.Cells[8].Text.Trim();}
					catch {}
					break;

				case "delete":
					string id = e.Item.Cells[0].Text;
					conn.QueryString = "delete from PENDING_RFRATING_QUALITATIVE WHERE QUALITATIVEID = '" + id + "'";
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

		protected void CHK_HAVEPARENT_CheckedChanged(object sender, System.EventArgs e)
		{
			if (CHK_HAVEPARENT.Checked == true)
			{
				DDL_PARENT.Enabled = true;
				DDL_PARENT.CssClass = "mandatory";
			}
			else
			{
				try { DDL_PARENT.SelectedValue = ""; } 
				catch{}
				DDL_PARENT.Enabled = false;
				DDL_PARENT.CssClass = "";
			}
		}

	}
}
