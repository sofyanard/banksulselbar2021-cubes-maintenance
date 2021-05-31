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
	/// Summary description for RFRatingSubSubQualitative.
	/// </summary>
	public partial class RFRatingSubSubQualitative : System.Web.UI.Page
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

				GlobalTools.fillRefList(DDL_QUALITATIVEID,"exec PARAM_GENERAL_RFRATINGSUBSUBQUAL_FILLDDLQUAL",false,conn);
				GlobalTools.fillRefList(DDL_SUBQUALITATIVEID,"exec PARAM_GENERAL_RFRATINGSUBSUBQUAL_FILLDDLSUBQUAL '" + DDL_QUALITATIVEID.SelectedValue + "'",false,conn);
				GlobalTools.fillRefList(DDL_DOWNGRADELEVEL,"exec PARAM_GENERAL_RFRATINGSUBSUBQUAL_FILLDDLDOWNGRADELEVEL",false,conn);

				viewExistingData();
				viewPendingData();
				nextID();
			}
		}

		private void nextID()
		{
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGSUBSUBQUAL_NEXTID '" + DDL_SUBQUALITATIVEID.SelectedValue + "'";
			conn.ExecuteQuery();
			TXT_ID.Text = conn.GetFieldValue("NEXTID");
		}

		private void viewExistingData()
		{
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGSUBSUBQUAL_VIEWEXIST '" + 
				DDL_QUALITATIVEID.SelectedValue + "', '" + DDL_SUBQUALITATIVEID.SelectedValue + "'";
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
				if (DGExisting.Items[i].Cells[12].Text.Trim() =="0" ) 
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
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGSUBSUBQUAL_VIEWREQUEST '" + 
				DDL_QUALITATIVEID.SelectedValue + "', '" + DDL_SUBQUALITATIVEID.SelectedValue + "'";
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
			TXT_SCORE.Text = "";
			DDL_QUALITATIVEID.SelectedValue = "";
			DDL_SUBQUALITATIVEID.SelectedValue = "";
			DDL_QUALITATIVEID.Enabled = true;
			DDL_SUBQUALITATIVEID.Enabled = true;
			CHK_FLAG.Checked = false;
			DDL_DOWNGRADELEVEL.SelectedValue = "";
			DDL_DOWNGRADELEVEL.Enabled = false;
			DDL_DOWNGRADELEVEL.CssClass = "";
		}

		private void executeMaker(string id, string xid, string desc, double score, string dg_flag, string dg_level, string pendingStatus) 
		{
			conn.QueryString = "SELECT COUNT(*) JUMLAH FROM PENDING_RFRATING_SUBSUBQUALITATIVE WHERE SUBSUBQUALITATIVEID ='" + id + "'";
			conn.ExecuteQuery();

			int jumlah = Convert.ToInt16(conn.GetFieldValue("JUMLAH"));
			
			if (jumlah > 0) 
			{				
				conn.QueryString = "UPDATE PENDING_RFRATING_SUBSUBQUALITATIVE SET SUBSUBQUALITATIVEDESC = '" + desc + "', " +
					"CH_STA = '" + pendingStatus + "' WHERE SUBSUBQUALITATIVEID = '" + id + "'";
				conn.ExecuteQuery();
			}
			else 
			{
				if (LBL_ACTIVE.Text.Trim() == "1")
					conn.QueryString = "INSERT INTO PENDING_RFRATING_SUBSUBQUALITATIVE " +
						"(SUBSUBQUALITATIVEID, SUBQUALITATIVEID, SUBSUBQUALITATIVEDESC, SCORE, DOWNGRADE_FLAG, DOWNGRADE_LEVEL, ACTIVE, CH_STA) " +
						"VALUES ('"+id+"', '"+xid+"', '"+desc+"', "+score.ToString().Replace(",",".")+", '" + dg_flag + "', '" + dg_level + "', '1', '"+pendingStatus+"')";
				else if (LBL_ACTIVE.Text.Trim() == "0")
					conn.QueryString = "INSERT INTO PENDING_RFRATING_SUBSUBQUALITATIVE " +
						"(SUBSUBQUALITATIVEID, SUBQUALITATIVEID, SUBSUBQUALITATIVEDESC, SCORE, DOWNGRADE_FLAG, DOWNGRADE_LEVEL, ACTIVE, CH_STA) " +
						"VALUES ('"+id+"', '"+xid+"', '"+desc+"', "+score.ToString().Replace(",",".")+", '" + dg_flag + "', '" + dg_level + "', '0', '"+pendingStatus+"')";
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
			GlobalTools.fillRefList(DDL_SUBQUALITATIVEID,"exec PARAM_GENERAL_RFRATINGSUBSUBQUAL_FILLDDLSUBQUAL '" + DDL_QUALITATIVEID.SelectedValue + "'",false,conn);
			nextID();
			viewExistingData();
			viewPendingData();
		}

		protected void DDL_SUBQUALITATIVEID_SelectedIndexChanged(object sender, System.EventArgs e)
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
			else if (DDL_SUBQUALITATIVEID.SelectedValue.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Sub Qualitative tidak boleh kosong!");
				GlobalTools.SetFocus(this,DDL_SUBQUALITATIVEID);
				return;
			}

			if (CHK_FLAG.Checked == true)
				if (DDL_DOWNGRADELEVEL.SelectedValue == "")
				{
					GlobalTools.popMessage(this,"Downgrade level tidak boleh kosong!");
					GlobalTools.SetFocus(this,DDL_DOWNGRADELEVEL);
					return;
				}

			if (TXT_SCORE.Text.Trim() == "") TXT_SCORE.Text = "0";
			string dg_flag;
			if (CHK_FLAG.Checked == true) { dg_flag = "1"; } 
			else { dg_flag = "0"; }
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select active from rfrating_subsubqualitative where subsubqualitativeid ='" + TXT_ID.Text.Trim() + "'";
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
			executeMaker(TXT_ID.Text.Trim(), DDL_SUBQUALITATIVEID.SelectedValue.Trim(), TXT_DESC.Text.Trim(), double.Parse(TXT_SCORE.Text.Trim()), dg_flag.Trim(), DDL_DOWNGRADELEVEL.SelectedValue.Trim(), LBL_SAVEMODE.Text.Trim());
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
			string id, xid, dg_flag;

			clearControls();
			DDL_QUALITATIVEID.Enabled = false;
			DDL_SUBQUALITATIVEID.Enabled = false;
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[6].Text;
					TXT_SCORE.Text = e.Item.Cells[7].Text;
					try
					{
						DDL_QUALITATIVEID.SelectedValue = e.Item.Cells[1].Text;
					}
					catch(Exception ex){}
					GlobalTools.fillRefList(DDL_SUBQUALITATIVEID,"exec PARAM_GENERAL_RFRATINGSUBSUBQUAL_FILLDDLSUBQUAL '" + DDL_QUALITATIVEID.SelectedValue + "'",false,conn);
					try
					{
						DDL_SUBQUALITATIVEID.SelectedValue = e.Item.Cells[3].Text;
					}
					catch(Exception ex){}
					if (e.Item.Cells[8].Text == "1")
					{
						CHK_FLAG.Checked = true;
						try { DDL_DOWNGRADELEVEL.SelectedValue = e.Item.Cells[10].Text.Trim(); } 
						catch {}
						DDL_DOWNGRADELEVEL.Enabled = true;
						DDL_DOWNGRADELEVEL.CssClass = "mandatory";
					} 
					else
					{
						CHK_FLAG.Checked = false;
						DDL_DOWNGRADELEVEL.SelectedValue = "";
						DDL_DOWNGRADELEVEL.Enabled = false;
						DDL_DOWNGRADELEVEL.CssClass = "";
					}
					
					break;

				case "delete":					
					id = e.Item.Cells[0].Text.Trim();
					xid = e.Item.Cells[3].Text.Trim();
					if (e.Item.Cells[8].Text == "1") { dg_flag = "1"; } 
					else { dg_flag = "0"; }
					
					executeMaker(id, xid, e.Item.Cells[6].Text, double.Parse(e.Item.Cells[7].Text), dg_flag, e.Item.Cells[10].Text, "2");
					viewPendingData();
					nextID();
					break;

				case "undelete":					
					id = e.Item.Cells[0].Text.Trim();
					xid = e.Item.Cells[3].Text.Trim();
					if (e.Item.Cells[8].Text == "1") { dg_flag = "1"; } 
					else { dg_flag = "0"; }
					
					executeMaker(id, xid, e.Item.Cells[6].Text, double.Parse(e.Item.Cells[7].Text), dg_flag, e.Item.Cells[10].Text, "0");
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
			DDL_SUBQUALITATIVEID.Enabled = false;
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[13].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[6].Text;
					TXT_SCORE.Text = e.Item.Cells[7].Text;
					try
					{
						DDL_QUALITATIVEID.SelectedValue = e.Item.Cells[1].Text;
					}
					catch(Exception ex){}
					GlobalTools.fillRefList(DDL_SUBQUALITATIVEID,"exec PARAM_GENERAL_RFRATINGSUBSUBQUAL_FILLDDLSUBQUAL '" + DDL_QUALITATIVEID.SelectedValue + "'",false,conn);
					try
					{
						DDL_SUBQUALITATIVEID.SelectedValue = e.Item.Cells[3].Text;
					}
					catch(Exception ex){}
					if (e.Item.Cells[8].Text == "1")
					{
						CHK_FLAG.Checked = true;
						try { DDL_DOWNGRADELEVEL.SelectedValue = e.Item.Cells[10].Text.Trim(); } 
						catch {}
						DDL_DOWNGRADELEVEL.Enabled = true;
						DDL_DOWNGRADELEVEL.CssClass = "mandatory";
					} 
					else
					{
						CHK_FLAG.Checked = false;
						DDL_DOWNGRADELEVEL.SelectedValue = "";
						DDL_DOWNGRADELEVEL.Enabled = false;
						DDL_DOWNGRADELEVEL.CssClass = "";
					}
					break;

				case "delete":
					string id = e.Item.Cells[0].Text;
					conn.QueryString = "delete from PENDING_RFRATING_SUBSUBQUALITATIVE WHERE SUBSUBQUALITATIVEID = '" + id + "'";
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

		protected void CHK_FLAG_CheckedChanged(object sender, System.EventArgs e)
		{
			if (CHK_FLAG.Checked == true)
			{
				DDL_DOWNGRADELEVEL.Enabled = true;
				DDL_DOWNGRADELEVEL.CssClass = "mandatory";
				DDL_DOWNGRADELEVEL.SelectedValue = "";
			}
			else
			{
				DDL_DOWNGRADELEVEL.Enabled = false;
				DDL_DOWNGRADELEVEL.CssClass = "";
				DDL_DOWNGRADELEVEL.SelectedValue = "";
			}
		}
	}
}