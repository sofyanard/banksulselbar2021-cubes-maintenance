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

namespace CuBES_Maintenance.Parameter.General.JiwaService
{
	/// <summary>
	/// Summary description for AssessmentParamAll.
	/// </summary>
	public class AssessmentParamAll : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton BTN_BACK;
		protected System.Web.UI.WebControls.DropDownList DDL_GRPTYPEID;
		protected System.Web.UI.WebControls.TextBox TEXT_LANGKAH;
		protected System.Web.UI.WebControls.Button BTN_SAVE;
		protected System.Web.UI.WebControls.Button BTN_CLEAR;
		protected System.Web.UI.WebControls.DataGrid DGR_LANGKAH;
		protected System.Web.UI.WebControls.DataGrid DGR_SELF;
		protected System.Web.UI.WebControls.DataGrid DGR_REQUESTSELF;
		protected Tools tool = new Tools();
		protected System.Web.UI.WebControls.TextBox TXT_ACTION;
		protected System.Web.UI.WebControls.TextBox TXT_UKURAN;
		protected System.Web.UI.WebControls.TextBox TXT_PIC_UNIT;
		protected System.Web.UI.WebControls.RadioButtonList RDO_SERVICE;
		protected System.Web.UI.WebControls.Button BTN_INSERT;
		protected System.Web.UI.WebControls.Label TXT_NO;
		protected System.Web.UI.WebControls.Label LBL_NO;
		protected System.Web.UI.WebControls.Label LBL_ID;
		protected System.Web.UI.WebControls.Label TXT_ID;
		protected System.Web.UI.WebControls.DropDownList DDL_DEPTTYPEID;
		protected Connection conn;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if (!IsPostBack) 
			{
				CekKode();
				FillDDLType();
				ViewData();
				FillDDLDeptType();
				FillGridCurr();
				FillGridReq();
			}
		}
		
		private void CekKode()
		{
			conn.QueryString = "SELECT ISNULL(MAX(CONVERT(INT, SEQ)),0) AS SEQ FROM RF_SELF";
			conn.ExecuteQuery();
			LBL_ID.Text = conn.GetFieldValue("SEQ").ToString();

			conn.QueryString="EXEC PARAM_GENERAL_RFGROUP_GENERATE_CODE '" + LBL_ID.Text + "'";
			conn.ExecuteQuery();

			TXT_ID.Text = conn.GetFieldValue(0,0).ToString();
		}

		private void FillDDLType()
		{
			DDL_GRPTYPEID.Items.Clear();
			DDL_GRPTYPEID.Items.Add(new ListItem("--Pilih--", ""));

			conn.QueryString = "SELECT BRANCH_CODE, BRANCH_NAME FROM RFBRANCH WHERE ACTIVE='1'";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_GRPTYPEID.Items.Add(new ListItem(conn.GetFieldValue(i, "BRANCH_NAME"), conn.GetFieldValue(i, "BRANCH_CODE")));
			}
		}

		private void DDL_GRPTYPEID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillDDLDeptType();
			FillGridSelf();
			CekCode();
		}

		private void FillDDLDeptType()
		{
			DDL_DEPTTYPEID.Items.Clear();
			DDL_DEPTTYPEID.Items.Add(new ListItem("--Pilih--", ""));

			conn.QueryString = "SELECT D_CODE, D_DESCNEW FROM RF_DEPT WHERE G_CODE='" + DDL_GRPTYPEID.SelectedValue + "' AND STATUS='1'";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_DEPTTYPEID.Items.Add(new ListItem(conn.GetFieldValue(i, "D_DESCNEW"), conn.GetFieldValue(i, "D_CODE")));
			}
		}

		private void ViewData()
		{
			conn.QueryString = "SELECT * FROM RF_STEP WHERE SEQ_ID='" + TXT_ID.Text + "' AND STATUS='1'";
			conn.ExecuteQuery();

			if(conn.GetRowCount() > 0)
			{
				DDL_GRPTYPEID.SelectedValue = conn.GetFieldValue(0,2);
				TXT_ACTION.Text = conn.GetFieldValue(0,3);
				TXT_UKURAN.Text = conn.GetFieldValue(0,4);
				FillGridSelf();
				CekCode();
			}
			else
			{
				FillGridSelf();
				CekCode();
			}
		}

		private void FillGridSelf()
		{
			conn.QueryString = "SELECT * FROM RF_STEP WHERE STATUS='1' ORDER BY SEQ ASC";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_LANGKAH.DataSource = dt;
			try
			{
				DGR_LANGKAH.DataBind();
			}
			catch
			{
				DGR_LANGKAH.CurrentPageIndex = 0;
				DGR_LANGKAH.DataBind();
			}
		}

		private void FillGridCurr()
		{
			conn.QueryString = "SELECT * FROM RF_SELF WHERE STATUS='1'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_SELF.DataSource = dt;
			try
			{
				DGR_SELF.DataBind();
			}
			catch
			{
				DGR_SELF.CurrentPageIndex = 0;
				DGR_SELF.DataBind();
			}
		}
		private void FillGridReq()
		{
			conn.QueryString = "SELECT *, CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_SELF WHERE STATUS='0'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_REQUESTSELF.DataSource = dt;
			try
			{
				DGR_REQUESTSELF.DataBind();
			}
			catch
			{
				DGR_REQUESTSELF.CurrentPageIndex = 0;
				DGR_REQUESTSELF.DataBind();
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
			this.DDL_GRPTYPEID.SelectedIndexChanged += new System.EventHandler(this.DDL_GRPTYPEID_SelectedIndexChanged);
			this.DGR_LANGKAH.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_LANGKAH_ItemCommand);
			this.DGR_LANGKAH.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_LANGKAH_PageIndexChanged);
			this.BTN_INSERT.Click += new System.EventHandler(this.BTN_INSERT_Click);
			this.BTN_SAVE.Click += new System.EventHandler(this.BTN_SAVE_Click);
			this.BTN_CLEAR.Click += new System.EventHandler(this.BTN_CLEAR_Click);
			this.DGR_SELF.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_SELF_ItemCommand);
			this.DGR_SELF.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_SELF_PageIndexChanged);
			this.DGR_REQUESTSELF.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUESTSELF_ItemCommand);
			this.DGR_REQUESTSELF.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUESTSELF_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BTN_INSERT_Click(object sender, System.EventArgs e)
		{
			if(DDL_GRPTYPEID.SelectedValue.ToString() == "" || TXT_ACTION.Text == "" || TXT_UKURAN.Text == "")
			{
				GlobalTools.popMessage(this, "Check Field Mandatory!");
				return; 
			}
			else
			{
				try
				{
					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RFSTEP_INSERT '" + TXT_ID.Text + "','" + TXT_NO.Text + "','" + DDL_GRPTYPEID.SelectedValue + "','" +
						TXT_ACTION.Text + "','" + TXT_UKURAN.Text + "','" + TEXT_LANGKAH.Text + "'";
					conn.ExecuteQuery();
				}
				catch (Exception ex)
				{
					TEXT_LANGKAH.Text = "";
					ViewData();

					string errmsg = ex.Message.Replace("'","");
					if (errmsg.IndexOf("Last Query:") > 0)
						errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
					GlobalTools.popMessage(this, errmsg);
					return;
				}
				
				TEXT_LANGKAH.Text = "";
				ViewData();
			}
		}

		private void CekCode()
		{
			conn.QueryString = "SELECT ISNULL(MAX(CONVERT(INT, SEQ)),0) AS SEQ FROM RF_STEP WHERE G_CODE='" + DDL_GRPTYPEID.SelectedValue + "' AND STATUS='1'";
			conn.ExecuteQuery();
			LBL_NO.Text = conn.GetFieldValue("SEQ").ToString();

			conn.QueryString="EXEC PARAM_GENERAL_RFGROUP_GENERATE_CODE '" + LBL_NO.Text + "'";
			conn.ExecuteQuery();

			TXT_NO.Text = conn.GetFieldValue(0,0).ToString();
		}

		private void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "SELECT * FROM RF_STEP WHERE SEQ_ID='" + TXT_ID.Text + "' AND STATUS='1'";
			conn.ExecuteQuery();
			
			int a = conn.GetRowCount();
			string b = "";

			for(int i=0; i<a; i++)
			{
				try
				{
					conn.QueryString = "SELECT TOP 1 * FROM RF_STEP WHERE SEQ_ID='" + TXT_ID.Text + "' AND STATUS='1'";
					conn.ExecuteQuery();

					b = conn.GetFieldValue(0,1).ToString();

					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RFSELF_INSERT '" + TXT_ID.Text + "','" + 
						DDL_GRPTYPEID.SelectedValue + "','" + DDL_DEPTTYPEID.SelectedValue + "','" +
						TXT_ACTION.Text + "','" + TXT_UKURAN.Text + "','" + TXT_PIC_UNIT.Text + "','" +
						RDO_SERVICE.SelectedValue + "','" + b + "','" + Session["UserID"].ToString() + "'";
					conn.ExecuteQuery();
				}
				catch (Exception ex)
				{
					CekKode();
					CekCode();
					ClearData();
					FillGridCurr();
					FillGridReq();

					string errmsg = ex.Message.Replace("'","");
					if (errmsg.IndexOf("Last Query:") > 0)
						errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
					GlobalTools.popMessage(this, errmsg);
					return;
				}
			}
			
			CekKode();
			CekCode();
			ClearData();
			FillGridCurr();
			FillGridReq();
		}

		private void BTN_CLEAR_Click(object sender, System.EventArgs e)
		{
			ClearData();
		}

		private void ClearData()
		{
			FillDDLType();
			FillDDLDeptType();
			FillGridSelf();
			TEXT_LANGKAH.Text = "";
			TXT_ACTION.Text = "";
			TXT_UKURAN.Text = "";
			TXT_PIC_UNIT.Text = "";
			RDO_SERVICE.SelectedValue = null;
		}

		private void DGR_LANGKAH_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_SELF.CurrentPageIndex = e.NewPageIndex;
			FillGridSelf();
		}

		private void DGR_LANGKAH_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit_langkah":
					TXT_ID.Text = e.Item.Cells[0].Text.Trim();
					TXT_NO.Text = e.Item.Cells[1].Text.Trim();
					DDL_GRPTYPEID.SelectedValue = e.Item.Cells[2].Text.Trim();
					TXT_ACTION.Text = e.Item.Cells[3].Text.Trim();
					TXT_UKURAN.Text = e.Item.Cells[4].Text.Trim();
					TEXT_LANGKAH.Text = e.Item.Cells[5].Text.Trim();
					break;
				case "delete_langkah":
					conn.QueryString = "DELETE RF_STEP WHERE SEQ_ID='" + e.Item.Cells[0].Text.Trim() + "'AND SEQ='" + e.Item.Cells[1].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					
					CekCode();
					FillGridSelf();
					break;
			}
		}

		private void DGR_SELF_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_SELF.CurrentPageIndex = e.NewPageIndex;
			FillGridCurr();
		}

		private void DGR_SELF_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_ID.Text = e.Item.Cells[0].Text.Trim();
					TXT_NO.Text = e.Item.Cells[7].Text.Trim();
					DDL_GRPTYPEID.SelectedValue = e.Item.Cells[1].Text.Trim();
					FillDDLDeptType();
					DDL_DEPTTYPEID.SelectedValue = e.Item.Cells[3].Text.Trim();
					TXT_ACTION.Text = e.Item.Cells[5].Text.Trim();
					TXT_UKURAN.Text = e.Item.Cells[6].Text.Trim();
					TEXT_LANGKAH.Text = e.Item.Cells[8].Text.Trim();
					RDO_SERVICE.SelectedValue = e.Item.Cells[9].Text.Trim();
					TXT_PIC_UNIT.Text = e.Item.Cells[10].Text.Trim();
					break;
				case "delete":
					conn.QueryString = "UPDATE RF_SELF SET STATUS='2' WHERE SEQ='" + e.Item.Cells[0].Text.Trim() + "' AND LANGKAH_SEQ='" + e.Item.Cells[7].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					conn.QueryString = "UPDATE RFSELF_HISTORY SET STATUS='0' WHERE SEQ='" + e.Item.Cells[0].Text.Trim() + "' AND LANGKAH_SEQ='" + e.Item.Cells[7].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					
					FillGridCurr();
					break;
			}
		}

		private void DGR_REQUESTSELF_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUESTSELF.CurrentPageIndex = e.NewPageIndex;
			FillGridReq();
		}

		private void DGR_REQUESTSELF_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit_req":
					TXT_ID.Text = e.Item.Cells[0].Text.Trim();
					TXT_NO.Text = e.Item.Cells[7].Text.Trim();
					DDL_GRPTYPEID.SelectedValue = e.Item.Cells[1].Text.Trim();
					FillDDLDeptType();
					DDL_DEPTTYPEID.SelectedValue = e.Item.Cells[3].Text.Trim();
					TXT_ACTION.Text = e.Item.Cells[5].Text.Trim();
					TXT_UKURAN.Text = e.Item.Cells[6].Text.Trim();
					TEXT_LANGKAH.Text = e.Item.Cells[8].Text.Trim();
					RDO_SERVICE.SelectedValue = e.Item.Cells[9].Text.Trim();
					TXT_PIC_UNIT.Text = e.Item.Cells[10].Text.Trim();

					break;
				case "delete_req":
					conn.QueryString = "DELETE RF_SELF WHERE SEQ='" + e.Item.Cells[0].Text.Trim() + "' AND LANGKAH_SEQ='" + e.Item.Cells[7].Text.Trim() + "'";
					conn.ExecuteNonQuery();

					conn.QueryString = "UPDATE RFSELF_HISTORY SET STATUS='0' WHERE SEQ='" + e.Item.Cells[0].Text.Trim() + "' AND LANGKAH_SEQ='" + e.Item.Cells[7].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					
					FillGridReq();
					break;
			}
		}

		private void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../JWSParamMaker.aspx?mc="+Request.QueryString["mc"]+"&pg=9");
		}
	}
}
