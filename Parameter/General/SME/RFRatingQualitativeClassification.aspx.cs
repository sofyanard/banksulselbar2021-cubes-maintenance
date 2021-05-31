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
	/// Summary description for RFRatingQualitativeClassification.
	/// </summary>
	public partial class RFRatingQualitativeClassification : System.Web.UI.Page
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

				GlobalTools.fillRefList(DDL_QUALITATIVEID,"exec PARAM_GENERAL_RFRATINGQUALCLASSIFY_FILLDDLQUAL",false,conn);
				GlobalTools.fillRefList(DDL_DOWNGRADELEVEL,"exec PARAM_GENERAL_RFRATINGQUALCLASSIFY_FILLDDLDOWNGRADELEVEL",false,conn);
				viewExistingData();
				viewPendingData();
			}
		}

		private void viewExistingData()
		{
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGQUALCLASSIFY_VIEWEXIST '" + DDL_QUALITATIVEID.SelectedValue + "'";
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
			conn.QueryString = "exec PARAM_GENERAL_RFRATINGQUALCLASSIFY_VIEWREQUEST '" + DDL_QUALITATIVEID.SelectedValue + "'";
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
			TXT_SCORE.Text = "";
			TXT_MIN_RANGE.Text = "";
			TXT_MAX_RANGE.Text = "";
			DDL_QUALITATIVEID.SelectedValue = "";
			DDL_QUALITATIVEID.Enabled = true;
			TXT_CLASSIFICATION.Text = "";
			TXT_CLASSIFICATION.Enabled = true;
			CHK_FLAG.Checked = false;
			DDL_DOWNGRADELEVEL.SelectedValue = "";
			DDL_DOWNGRADELEVEL.Enabled = false;
			DDL_DOWNGRADELEVEL.CssClass = "";
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
			viewExistingData();
			viewPendingData();
		}

		private void DGExisting_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		}

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (DDL_QUALITATIVEID.SelectedValue.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Qualitative tidak boleh kosong!");
				GlobalTools.SetFocus(this,DDL_QUALITATIVEID);
				return;
			}
			else if (TXT_SCORE.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Score tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_SCORE);
				return;
			}
			else if (TXT_MIN_RANGE.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Min Range tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_MIN_RANGE);
				return;
			}
			else if (TXT_MAX_RANGE.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Max Range tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_MAX_RANGE);
				return;
			}
			else if (TXT_CLASSIFICATION.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Classification tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_CLASSIFICATION);
				return;
			}

			if (CHK_FLAG.Checked == true)
				if (DDL_DOWNGRADELEVEL.SelectedValue == "")
				{
					GlobalTools.popMessage(this,"Downgrade level tidak boleh kosong!");
					GlobalTools.SetFocus(this,DDL_DOWNGRADELEVEL);
					return;
				}

			string dg_flag;
			if (CHK_FLAG.Checked == true) { dg_flag = "1"; } else { dg_flag = "0"; }

			conn.QueryString = "exec PARAM_GENERAL_RFRATINGQUALCLASSIFY_UPDATEREQUEST '1', '" + 
				DDL_QUALITATIVEID.SelectedValue.Trim() + "','" + TXT_CLASSIFICATION.Text.Trim() + "', " + 
				double.Parse(TXT_SCORE.Text.Trim()).ToString().Replace(",",".") + ", " +
				double.Parse(TXT_MIN_RANGE.Text.Trim()).ToString().Replace(",",".") + ", " + double.Parse(TXT_MAX_RANGE.Text.Trim()).ToString().Replace(",",".") +
				", '" + dg_flag + "', '" + DDL_DOWNGRADELEVEL.SelectedValue + "'";
			conn.ExecuteQuery();
			viewPendingData();
			clearControls();
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();
		}

		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearControls();
			DDL_QUALITATIVEID.Enabled = false;
			TXT_CLASSIFICATION.Enabled = false;
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					try
					{
						DDL_QUALITATIVEID.SelectedValue = e.Item.Cells[0].Text;
					}
					catch(Exception ex){}
					TXT_CLASSIFICATION.Text = e.Item.Cells[2].Text;
					TXT_SCORE.Text = e.Item.Cells[3].Text;
					TXT_MIN_RANGE.Text = e.Item.Cells[4].Text;
					TXT_MAX_RANGE.Text = e.Item.Cells[5].Text;
					if (e.Item.Cells[6].Text == "1")
					{
						CHK_FLAG.Checked = true;
						try { DDL_DOWNGRADELEVEL.SelectedValue = e.Item.Cells[8].Text.Trim(); } 
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
					conn.QueryString = "exec PARAM_GENERAL_RFRATINGQUALCLASSIFY_UPDATEREQUEST '2', '" + 
						e.Item.Cells[0].Text.Trim() + "','" + e.Item.Cells[2].Text.Trim() + "', " + 
						e.Item.Cells[3].Text.Replace(",",".") + ", " + 
						e.Item.Cells[4].Text.Replace(",",".") + ", " + e.Item.Cells[5].Text.Replace(",",".") +
						", '" + e.Item.Cells[6].Text + "', '" + e.Item.Cells[8].Text + "'";
					conn.ExecuteQuery();
					viewPendingData();
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
				case "delete":
					conn.QueryString = "exec PARAM_GENERAL_RFRATINGQUALCLASSIFY_UPDATEREQUEST '3', '" + 
						e.Item.Cells[0].Text.Trim() + "','" + e.Item.Cells[2].Text.Trim() + "', " +
						e.Item.Cells[3].Text.Replace(",",".") + ", " + 
						e.Item.Cells[4].Text.Replace(",",".") + ", " + e.Item.Cells[5].Text.Replace(",",".") +
						", '" + e.Item.Cells[6].Text + "', '" + e.Item.Cells[8].Text + "'";
					conn.ExecuteQuery();
					viewPendingData();
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
