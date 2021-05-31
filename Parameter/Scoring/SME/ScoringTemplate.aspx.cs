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

namespace CuBES_Maintenance.Parameter.Scoring.SME
{
	/// <summary>
	/// Summary description for ScoringTemplate.
	/// </summary>
	public partial class ScoringTemplate : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack) 
			{
				LBL_SAVEMODE.Text = "1";
				LBL_SAVEMODE2.Text = "1";

				ViewExistingDataTpl();
				ViewPendingDataTpl();

				//ViewExistingDataVal();
				//ViewPendingDataVal();

				FillTemplate();
				FillParam();
			}

			BTN_SAVE_TPL.Attributes.Add("onclick","if(!cek_mandatory2(document.Form1)){return false;};");
			BTN_SAVE_VAL.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
		}

		private void ViewExistingDataTpl()
		{ 
			conn.QueryString = "SELECT * FROM VW_PRMSCORING_SCORINGTEMPLATE_VIEWEXISTINGTPL ORDER BY SCOTPL_ID";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_EXISTING_TPL.DataSource = data;
			
			try
			{
				DGR_EXISTING_TPL.DataBind();
			} 
			catch 
			{
				this.DGR_EXISTING_TPL.CurrentPageIndex = DGR_EXISTING_TPL.PageCount - 1;
				DGR_EXISTING_TPL.DataBind();
			}
		}

		private void ViewPendingDataTpl()
		{
			conn.QueryString = "SELECT * FROM VW_PRMSCORING_SCORINGTEMPLATE_VIEWPENDINGTPL ORDER BY SCOTPL_ID";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_REQUEST_TPL.DataSource = data;
			
			try
			{
				DGR_REQUEST_TPL.DataBind();
			} 
			catch 
			{
				this.DGR_REQUEST_TPL.CurrentPageIndex = DGR_REQUEST_TPL.PageCount - 1;
				DGR_REQUEST_TPL.DataBind();
			}
		}

		private void clearEditBoxesTpl()
		{
			TXT_TPLID.Enabled = true;
			TXT_TPLID.Text = "";
			TXT_TPLDESC.Text = "";
			TXT_TPLFORMULA.Text = "";
			LBL_SAVEMODE.Text = "1";
		}

		private void ViewExistingDataVal()
		{
			string qry = "SELECT * FROM VW_PRMSCORING_SCORINGTEMPLATE_VIEWEXISTINGVAL WHERE 1=1 ";

			if (DDL_SCOTPL_ID.SelectedValue != "")
				qry = qry + " AND SCOTPL_ID = '" + DDL_SCOTPL_ID.SelectedValue.Trim() + "' ";
			if (DDL_PARAM_ID.SelectedValue != "")
				qry = qry + " AND PARAM_ID = '" + DDL_PARAM_ID.SelectedValue.Trim() + "' ";

			qry = qry + " ORDER BY SCOTPL_ID, PARAM_ID, PARAM_VALUE_ID";
			
			conn.QueryString = qry;
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_EXISTING_VAL.DataSource = data;
			
			try
			{
				DGR_EXISTING_VAL.DataBind();
			} 
			catch 
			{
				this.DGR_EXISTING_VAL.CurrentPageIndex = DGR_EXISTING_VAL.PageCount - 1;
				DGR_EXISTING_VAL.DataBind();
			}
		}

		private void ViewPendingDataVal()
		{
			string qry = "SELECT * FROM VW_PRMSCORING_SCORINGTEMPLATE_VIEWPENDINGVAL WHERE 1=1 ";

			if (DDL_SCOTPL_ID.SelectedValue != "")
				qry = qry + " AND SCOTPL_ID = '" + DDL_SCOTPL_ID.SelectedValue.Trim() + "' ";
			if (DDL_PARAM_ID.SelectedValue != "")
				qry = qry + " AND PARAM_ID = '" + DDL_PARAM_ID.SelectedValue.Trim() + "' ";

			qry = qry + " ORDER BY SCOTPL_ID, PARAM_ID, PARAM_VALUE_ID";
			
			conn.QueryString = qry;
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_REQUEST_VAL.DataSource = data;
			
			try
			{
				DGR_REQUEST_VAL.DataBind();
			} 
			catch 
			{
				this.DGR_REQUEST_VAL.CurrentPageIndex = DGR_REQUEST_VAL.PageCount - 1;
				DGR_REQUEST_VAL.DataBind();
			}
		}

		private void clearEditBoxesVal()
		{
			try {DDL_SCOTPL_ID.SelectedValue = "";}
			catch {}
			try {DDL_PARAM_ID.SelectedValue = "";}
			catch {}
			TXT_PARAM_VALUE_ID.Text = "";
			CHK_DIRECT_FLAG.Checked = false;
			try {DDL_REF_VALUE.SelectedValue = "";}
			catch {}
			TXT_MIN_VALUE.Text = "";
			TXT_MAX_VALUE.Text = "";
			TXT_PARAM_SCORE.Text = "";
			TXT_PARAM_WEIGHT.Text = "";
			TXT_REMARKS.Text = "";
			TR_PARAM.Visible = true;
			TR_VALUE.Visible = true;
			TR_SCORE.Visible = true;
			DDL_SCOTPL_ID.Enabled = true;
			DDL_PARAM_ID.Enabled = true;
			TXT_PARAM_VALUE_ID.Enabled = true;
			DDL_REF_VALUE.Enabled = true;
			LBL_SAVEMODE2.Text = "1";
		}

		private void FillTemplate()
		{
			GlobalTools.fillRefList(DDL_SCOTPL_ID, "SELECT SCOTPL_ID, SCOTPL_DESC FROM VW_PRMSCORING_SCORINGTEMPLATE_FILLTEMPLATE WHERE ACTIVE = '1' ORDER BY SCOTPL_ID", conn);
		}

		private void FillParam()
		{	
			GlobalTools.fillRefList(DDL_PARAM_ID, "SELECT PARAM_ID, PARAM_NAME FROM VW_PRMSCORING_SCORINGTEMPLATE_FILLPARAM WHERE ACTIVE = '1' ORDER BY PARAM_ID", conn);
			FillParamDetail();
		}

		private void FillParamDetail()
		{
			conn.QueryString = "SELECT PARAM_TABLE, PARAM_TABLE_CHILD FROM VW_PRMSCORING_SCORINGTEMPLATE_FILLPARAMDETAIL WHERE PARAM_ID = '" + DDL_PARAM_ID.SelectedValue + "'";
			conn.ExecuteQuery();

			string TABLE, TABLE_CHILD;
			TABLE = conn.GetFieldValue("PARAM_TABLE");
			TABLE_CHILD = conn.GetFieldValue("PARAM_TABLE_CHILD");
			
			this.DDL_REF_VALUE.Items.Clear();
			
			if (TABLE.Trim() == TABLE_CHILD.Trim())
			{
				TR_VALUE.Visible = true;
				TR_PARAM.Visible = false; 	
			} 
			else if(TABLE.Trim() != TABLE_CHILD.Trim())
			{
				TR_VALUE.Visible = false;
				TR_PARAM.Visible = true;

				try
				{
					GlobalTools.fillRefList(DDL_REF_VALUE, "SELECT * FROM " + TABLE, conn);
				}
				catch {}
			}
		}

		private string checkComma(string str)
		{
			return str.Replace(",", ".").Trim();
		}

		private string checkApost(string str)
		{
			return str.Replace("'", "''").Trim();
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
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
			this.DGR_EXISTING_TPL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_TPL_ItemCommand);
			this.DGR_EXISTING_TPL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_TPL_PageIndexChanged);
			this.DGR_REQUEST_TPL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_TPL_ItemCommand);
			this.DGR_REQUEST_TPL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_TPL_PageIndexChanged);
			this.DGR_EXISTING_VAL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_VAL_ItemCommand);
			this.DGR_EXISTING_VAL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_VAL_PageIndexChanged);
			this.DGR_REQUEST_VAL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_VAL_ItemCommand);
			this.DGR_REQUEST_VAL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_VAL_PageIndexChanged);

		}
		#endregion

		protected void BTN_CANCEL_TPL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxesTpl();
		}

		protected void BTN_SAVE_TPL_Click(object sender, System.EventArgs e)
		{
			try
			{
				conn.QueryString = "EXEC PRMSCORING_SCORINGTEMPLATE_TPLMAKER '" + 
					TXT_TPLID.Text.Trim() + "', '" + 
					TXT_TPLDESC.Text.Trim() + "', '" + 
					TXT_TPLFORMULA.Text.Trim() + "', '" + 
					LBL_SAVEMODE.Text.Trim() + "'";
				conn.ExecuteQuery();
			} 
			catch (Exception ex)
			{
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this, errmsg);
			}
			clearEditBoxesTpl();
			ViewPendingDataTpl();
		}

		private void DGR_EXISTING_TPL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_TPLID.Text = cleansText(e.Item.Cells[0].Text);
					TXT_TPLDESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_TPLFORMULA.Text = cleansText(e.Item.Cells[2].Text);
					TXT_TPLID.Enabled = false;
					break;
				case "delete":
					try
					{
						conn.QueryString = "EXEC PRMSCORING_SCORINGTEMPLATE_TPLMAKER '" + 
							cleansText(e.Item.Cells[0].Text.Trim()) + "', '" + 
							cleansText(e.Item.Cells[1].Text.Trim()) + "', '" + 
							cleansText(e.Item.Cells[2].Text.Trim()) + "', '" + 
							"2'";
						conn.ExecuteQuery();
					}
					catch (Exception ex)
					{
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this, errmsg);
					}
					ViewPendingDataTpl();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_REQUEST_TPL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[4].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						break;
					}
					TXT_TPLID.Text = cleansText(e.Item.Cells[0].Text);
					TXT_TPLDESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_TPLFORMULA.Text = cleansText(e.Item.Cells[2].Text);
					TXT_TPLID.Enabled = false;
					break;
				case "delete":
					try
					{
						conn.QueryString = "DELETE FROM PENDING_PRMSCORING_TEMPLATE WHERE SCOTPL_ID = '"+ cleansText(e.Item.Cells[0].Text) + "' ";
						conn.ExecuteQuery();
					}
					catch (Exception ex)
					{
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this, errmsg);
					}
					ViewPendingDataTpl();
					break;
				default:
					// do nothing
					break;
			}
		}

		private void DGR_EXISTING_TPL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING_TPL.CurrentPageIndex = e.NewPageIndex;
			ViewExistingDataTpl();
		}

		private void DGR_REQUEST_TPL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST_TPL.CurrentPageIndex = e.NewPageIndex;
			ViewPendingDataTpl();
		}

		protected void BTN_CANCEL_VAL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxesVal();
		}

		protected void BTN_FIND_VAL_Click(object sender, System.EventArgs e)
		{
			ViewExistingDataVal();
			ViewPendingDataVal();
		}

		private void DGR_EXISTING_VAL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING_VAL.CurrentPageIndex = e.NewPageIndex;
			ViewExistingDataVal();
		}

		private void DGR_REQUEST_VAL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST_VAL.CurrentPageIndex = e.NewPageIndex;
			ViewPendingDataVal();
		}

		protected void CHK_DIRECT_FLAG_CheckedChanged(object sender, System.EventArgs e)
		{
			if (CHK_DIRECT_FLAG.Checked)
			{
				try {DDL_REF_VALUE.SelectedValue = "";}
				catch {}
				TXT_MIN_VALUE.Text = "";
				TXT_MAX_VALUE.Text = "";
				TXT_PARAM_SCORE.Text = "";
				
				TR_PARAM.Visible = false;
				TR_VALUE.Visible = false;
				TR_SCORE.Visible = false;
			}
			else
			{
				TR_PARAM.Visible = true;
				TR_VALUE.Visible = true;
				TR_SCORE.Visible = true;
			}
		}

		protected void DDL_PARAM_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillParamDetail();
		}

		protected void BTN_SAVE_VAL_Click(object sender, System.EventArgs e)
		{
			if (TR_PARAM.Visible == false)
			{
				LBL_MIN.Text = TXT_MIN_VALUE.Text.Trim();
				LBL_MAX.Text = TXT_MAX_VALUE.Text.Trim();
			} 
			else if (this.TR_PARAM.Visible == true)
			{
				LBL_MIN.Text = DDL_REF_VALUE.SelectedValue.ToString().Trim();
				LBL_MAX.Text = DDL_REF_VALUE.SelectedItem.ToString().Trim();
			}
			
			try
			{
				conn.QueryString = "EXEC PRMSCORING_SCORINGTEMPLATE_VALMAKER '" + 
					DDL_SCOTPL_ID.SelectedValue.Trim() + "', '" + 
					DDL_PARAM_ID.SelectedValue.Trim() + "', '" + 
					TXT_PARAM_VALUE_ID.Text.Trim() + "', '" + 
					(CHK_DIRECT_FLAG.Checked == true ? "1" : "0") + "', '" + 
					checkComma(LBL_MIN.Text) + "', '" + 
					checkComma(LBL_MAX.Text) + "', '" + 
					checkComma(TXT_PARAM_SCORE.Text) + "', '" + 
					checkComma(TXT_PARAM_WEIGHT.Text) + "', '" + 
					TXT_REMARKS.Text.Trim() + "', '" + 
					LBL_SAVEMODE2.Text.Trim() + "'";
				conn.ExecuteQuery();
			} 
			catch (Exception ex)
			{
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this, errmsg);
			}
			clearEditBoxesVal();
			ViewPendingDataVal();
		}

		private void DGR_EXISTING_VAL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE2.Text = "0";
					try {DDL_SCOTPL_ID.SelectedValue = e.Item.Cells[0].Text.Trim();} 
					catch{}
					try {DDL_PARAM_ID.SelectedValue = e.Item.Cells[1].Text.Trim();} 
					catch{}
					TXT_PARAM_VALUE_ID.Text = e.Item.Cells[3].Text.Trim();
					CHK_DIRECT_FLAG.Checked = (e.Item.Cells[4].Text.Trim() == "1" ? true : false);
					FillParamDetail();
					try {DDL_REF_VALUE.SelectedValue = e.Item.Cells[5].Text.Trim();} 
					catch{}
					TXT_MIN_VALUE.Text = e.Item.Cells[5].Text.Trim();
					TXT_MAX_VALUE.Text = e.Item.Cells[6].Text.Trim();
					TXT_PARAM_SCORE.Text = e.Item.Cells[7].Text.Trim();
					TXT_PARAM_WEIGHT.Text = e.Item.Cells[8].Text.Trim();
					TXT_REMARKS.Text = e.Item.Cells[9].Text.Trim();

					DDL_SCOTPL_ID.Enabled = false;
					DDL_PARAM_ID.Enabled = false;
					DDL_REF_VALUE.Enabled = false;
					TXT_PARAM_VALUE_ID.Enabled = false;
					break;
				case "delete":
					try
					{
						conn.QueryString = "EXEC PRMSCORING_SCORINGTEMPLATE_VALMAKER '" + 
							e.Item.Cells[0].Text.Trim() + "', '" + 
							e.Item.Cells[1].Text.Trim() + "', '" + 
							e.Item.Cells[3].Text.Trim() + "', '" + 
							(CHK_DIRECT_FLAG.Checked == true ? "1" : "0") + "', '" + 
							checkComma(e.Item.Cells[5].Text.Trim()) + "', '" + 
							checkComma(e.Item.Cells[6].Text.Trim()) + "', '" + 
							checkComma(e.Item.Cells[7].Text.Trim()) + "', '" + 
							checkComma(e.Item.Cells[8].Text.Trim()) + "', '" + 
							e.Item.Cells[9].Text.Trim() + "', '" + 
							"2'";
						conn.ExecuteQuery();
					} 
					catch (Exception ex)
					{
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this, errmsg);
					}
					clearEditBoxesVal();
					ViewPendingDataVal();
					break;
				default:
					break;			
			}
		}

		private void DGR_REQUEST_VAL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE2.Text = e.Item.Cells[10].Text.Trim();
					if (LBL_SAVEMODE2.Text.Trim() == "2")
					{
						break;
					}
					try {DDL_SCOTPL_ID.SelectedValue = e.Item.Cells[0].Text.Trim();} 
					catch{}
					try {DDL_PARAM_ID.SelectedValue = e.Item.Cells[1].Text.Trim();} 
					catch{}
					TXT_PARAM_VALUE_ID.Text = e.Item.Cells[3].Text.Trim();
					CHK_DIRECT_FLAG.Checked = (e.Item.Cells[4].Text.Trim() == "1" ? true : false);
					FillParamDetail();
					try {DDL_REF_VALUE.SelectedValue = e.Item.Cells[5].Text.Trim();} 
					catch{}
					TXT_MIN_VALUE.Text = e.Item.Cells[5].Text.Trim();
					TXT_MAX_VALUE.Text = e.Item.Cells[6].Text.Trim();
					TXT_PARAM_SCORE.Text = e.Item.Cells[7].Text.Trim();
					TXT_PARAM_SCORE.Text = e.Item.Cells[8].Text.Trim();
					TXT_REMARKS.Text = e.Item.Cells[9].Text.Trim();

					DDL_SCOTPL_ID.Enabled = false;
					DDL_PARAM_ID.Enabled = false;
					DDL_REF_VALUE.Enabled = false;
					TXT_PARAM_VALUE_ID.Enabled = false;
					break;
				case "delete":
					try
					{
						conn.QueryString = "DELETE FROM PENDING_PRMSCORING_PARAM_VALUE WHERE SCOTPL_ID = '"+ e.Item.Cells[0].Text.Trim() + 
							"' AND PARAM_ID = '" + e.Item.Cells[1].Text.Trim() +
							"' AND PARAM_VALUE_ID = '" + e.Item.Cells[3].Text.Trim() + "'";
						conn.ExecuteQuery();
					}
					catch (Exception ex)
					{
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this, errmsg);
					}
					ViewPendingDataVal();
					break;
				default:
					break;
			}
		}
	}
}
