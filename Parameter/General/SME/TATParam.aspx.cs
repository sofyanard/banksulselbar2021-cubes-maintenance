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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for TATParam.
	/// </summary>
	public partial class TATParam : System.Web.UI.Page
	{
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if(!IsPostBack)
			{
				FillDDLSEGMENT();
				FillDDLSLA();
				CekCode();
				FillGridCurr();
				FillGridReq();
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
			this.Dgr_CurrTAT.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Dgr_CurrTAT_ItemCommand);
			this.Dgr_CurrTAT.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Dgr_CurrTAT_PageIndexChanged);
			this.Dgr_ReqTAT.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Dgr_ReqTAT_ItemCommand);
			this.Dgr_ReqTAT.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Dgr_ReqTAT_PageIndexChanged);

		}
		#endregion

		private void FillDDLSLA()
		{
			DDL_SLA.Items.Clear();
			DDL_SLA.Items.Add(new ListItem("--Pilih--", ""));

			conn.QueryString = "SELECT DISTINCT STAGE_CD, STAGE_DESC FROM VW_RF_STAGE_TRACK WHERE ACTIVE='1'";
			conn.ExecuteQuery();

			for (int i = 0; i < conn.GetRowCount(); i++)
			{
				DDL_SLA.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
			}
		}

		private void FillDDLSEGMENT()
		{
			DDL_SEGMENT.Items.Clear();
			DDL_SEGMENT.Items.Add(new ListItem("--Pilih--", ""));

			conn.QueryString = "SELECT BUSSUNITID, BUSSUNITDESC FROM RFBUSINESSUNIT WHERE ACTIVE='1'";
			conn.ExecuteQuery();

			for (int i = 0; i < conn.GetRowCount(); i++)
			{
				DDL_SEGMENT.Items.Add(new ListItem(conn.GetFieldValue(i,0) + " - " + conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			}
		}

		private void FillGridCurr()
		{
			conn.QueryString = "SELECT * FROM VW_RF_TAT";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			Dgr_CurrTAT.DataSource = dt;
			try
			{
				Dgr_CurrTAT.DataBind();
			}
			catch
			{
				Dgr_CurrTAT.CurrentPageIndex = 0;
				Dgr_CurrTAT.DataBind();
			}

			LinkButton lnk;

			for (int i = 0; i < Dgr_CurrTAT.Items.Count; i++)
			{
				if (Dgr_CurrTAT.Items[i].Cells[7].Text == "0")	//active		-- user deleted
				{
					lnk = (LinkButton)Dgr_CurrTAT.Items[i].Cells[8].FindControl("LNK_DELETE");
					lnk.Visible = false;
					lnk = (LinkButton)Dgr_CurrTAT.Items[i].Cells[8].FindControl("LNK_EDIT");
					lnk.Visible = false;
					Dgr_CurrTAT.Items[i].Cells[0].ForeColor = Color.Gray;	
					Dgr_CurrTAT.Items[i].Cells[1].ForeColor = Color.Gray;		
					Dgr_CurrTAT.Items[i].Cells[3].ForeColor = Color.Gray;		
					Dgr_CurrTAT.Items[i].Cells[4].ForeColor = Color.Gray;		
					Dgr_CurrTAT.Items[i].Cells[6].ForeColor = Color.Gray;		
				}
				else
				{
					lnk = (LinkButton)Dgr_CurrTAT.Items[i].Cells[8].FindControl("LNK_UNDELETE");
					lnk.Visible = false;
				}
			}

			for (int i = 0; i < Dgr_CurrTAT.Items.Count; i++)
			{
				if(Dgr_CurrTAT.Items[i].Cells[9].Text == "1") //sedang di update/remove/undelete
				{
					lnk = (LinkButton) Dgr_CurrTAT.Items[i].Cells[8].FindControl("LNK_DELETE");
					lnk.Visible = false;
					lnk = (LinkButton) Dgr_CurrTAT.Items[i].Cells[8].FindControl("LNK_EDIT");
					lnk.Visible = false;
					lnk = (LinkButton) Dgr_CurrTAT.Items[i].Cells[8].FindControl("LNK_UNDELETE");
					lnk.Visible = false;
				}
			}
		}

		private void FillGridReq()
		{
			conn.QueryString = "SELECT * FROM VW_PENDING_RF_TAT";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			Dgr_ReqTAT.DataSource = dt;
			try
			{
				Dgr_ReqTAT.DataBind();
			}
			catch
			{
				Dgr_ReqTAT.CurrentPageIndex = 0;
				Dgr_ReqTAT.DataBind();
			}

			LinkButton lnk;

			for (int i = 0; i < Dgr_ReqTAT.Items.Count; i++)
			{
				if (Dgr_ReqTAT.Items[i].Cells[7].Text == "2" || Dgr_ReqTAT.Items[i].Cells[7].Text == "3")	
				{
					lnk = (LinkButton)Dgr_ReqTAT.Items[i].Cells[9].FindControl("LNK_EDIT_REQ");
					lnk.Visible = false;
				}
				else
				{
					lnk = (LinkButton)Dgr_ReqTAT.Items[i].Cells[9].FindControl("LNK_DELETE_REQ");
					lnk.Visible = true;
					lnk = (LinkButton)Dgr_ReqTAT.Items[i].Cells[9].FindControl("LNK_EDIT_REQ");
					lnk.Visible = true;
				}
			}
		} 

		private void CekCode()
		{
			//int codemax = 0;

			conn.QueryString = "EXEC PARAM_GENERAL_RFTAT_GENERATE_CODE";
			conn.ExecuteQuery();

			TXT_CODE.Text = conn.GetFieldValue(0,0);
		}

		private void ClearData()
		{
			TXT_FLOWNAME.Text = "";
			DDL_SEGMENT.SelectedValue = "";
			DDL_SLA.SelectedValue = "";
			TXT_DAY.Text = "";
			CekCode();
			LBL_EDIT.Text = "0";
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if(TXT_FLOWNAME.Text == "")
			{
				GlobalTools.popMessage(this, "Flow Name tidak boleh kosong!");
				return;
			}
			if(DDL_SLA.SelectedValue =="")
			{
				GlobalTools.popMessage(this, "SLA tidak boleh kosong!");
				return;
			}
			if(DDL_SEGMENT.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Segment tidak boleh kosong!");
				return;
			}
			if(TXT_DAY.Text == "")
			{
				GlobalTools.popMessage(this, "Jumlah hari tidak boleh kosong!");
				return;
			}
			try
			{
				conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RF_TAT_CHECK_DB '" +
					TXT_FLOWNAME.Text + "', '" + DDL_SLA.SelectedValue + "', " +
					int.Parse(TXT_DAY.Text) + ", '" + DDL_SEGMENT.SelectedValue + "'";
				conn.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this, errmsg);
				return;
			}

			/*if(conn.GetFieldValue(0,0)!="")
			{
				GlobalTools.popMessage(this, conn.GetFieldValue(0,0));
				return;
			}*/

			conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RF_TAT_INSERT '" +
				TXT_CODE.Text + "', '" + TXT_FLOWNAME.Text + "', '" + 
				DDL_SLA.SelectedValue + "', " + int.Parse(TXT_DAY.Text) + ", '" +
				DDL_SEGMENT.SelectedValue + "', '" + LBL_SAVEMODE.Text + "'";
			conn.ExecuteNonQuery();

			if(LBL_EDIT.Text == "1")
			{
				conn.QueryString = "UPDATE RF_TAT SET EDIT_PARAM='1' WHERE CODE='" + TXT_CODE.Text + "'";
				conn.ExecuteQuery();

				LBL_EDIT.Text="0";
			}
			
			CekCode();
			ClearData();

			LBL_SAVEMODE.Text = "1";
			FillGridReq();
			FillGridCurr();
		}

		protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
		{
			ClearData();
		}

		private void Dgr_CurrTAT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_FLOWNAME.Text = e.Item.Cells[1].Text.Trim();
					try{DDL_SLA.SelectedValue = e.Item.Cells[2].Text.Trim();}
					catch{DDL_SLA.SelectedValue = "";}
					try{DDL_SEGMENT.SelectedValue = e.Item.Cells[5].Text.Trim();}
					catch{DDL_SEGMENT.SelectedValue = "";}
					TXT_DAY.Text = e.Item.Cells[4].Text.Trim();
					LBL_SAVEMODE.Text = "0";
					break;
				case "delete":
					LBL_SAVEMODE.Text = "2";

					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RF_TAT_INSERT '" +
						e.Item.Cells[0].Text.Trim() + "', '" + e.Item.Cells[1].Text.Trim() + "', '" + 
						e.Item.Cells[2].Text.Trim() + "', " + int.Parse(e.Item.Cells[4].Text.Trim()) + ", '" +
						e.Item.Cells[5].Text.Trim() + "', '" + LBL_SAVEMODE.Text + "'";
					conn.ExecuteNonQuery();

					FillGridCurr();
					FillGridReq();
					CekCode();
					LBL_SAVEMODE.Text = "1";
					break;
				case "undelete":
					LBL_SAVEMODE.Text = "3";

					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RF_TAT_INSERT '" +
						e.Item.Cells[0].Text.Trim() + "', '" + e.Item.Cells[1].Text.Trim() + "', '" + 
						e.Item.Cells[2].Text.Trim() + "', " + int.Parse(e.Item.Cells[4].Text.Trim()) + ", '" +
						e.Item.Cells[5].Text.Trim() + "', '" + LBL_SAVEMODE.Text + "'";
					conn.ExecuteNonQuery();

					FillGridCurr();
					FillGridReq();
					CekCode();
					LBL_SAVEMODE.Text = "1";
					break;
			}
			LBL_EDIT.Text = "1";
			
		}

		private void Dgr_CurrTAT_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			Dgr_CurrTAT.CurrentPageIndex = e.NewPageIndex;
			FillGridCurr();
		}	

		private void Dgr_ReqTAT_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			Dgr_ReqTAT.CurrentPageIndex = e.NewPageIndex;
			FillGridReq();
		}

		private void Dgr_ReqTAT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit_req":
					TXT_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_FLOWNAME.Text = e.Item.Cells[1].Text.Trim();
					try{DDL_SLA.SelectedValue = e.Item.Cells[2].Text.Trim();}
					catch{DDL_SLA.SelectedValue = "";}
					try{DDL_SEGMENT.SelectedValue = e.Item.Cells[5].Text.Trim();}
					catch{DDL_SEGMENT.SelectedValue = "";}
					TXT_DAY.Text = e.Item.Cells[4].Text.Trim();
					LBL_SAVEMODE.Text = e.Item.Cells[7].Text.Trim();
					LBL_EDIT.Text = "1";
					break;
				case "delete_req":
					conn.QueryString = "delete pending_rf_tat where code='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteQuery();
					conn.QueryString = "update rf_tat set edit_param='0' where code='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteQuery();
					FillGridCurr();
					FillGridReq();
					CekCode();
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}
	}
}
