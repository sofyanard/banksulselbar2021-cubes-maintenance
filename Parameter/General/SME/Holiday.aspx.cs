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
using System.Configuration;
using Microsoft.VisualBasic;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for Holiday.
	/// </summary>
	public partial class Holiday : System.Web.UI.Page
	{
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if(!IsPostBack)
			{
				FillDDLTahun();
				FillDDLBulan();
				FillGridCurr();
				FillGridReq();
				LBL_SEQ.Text = "0";
				LBL_SEQ_CURR.Text = "0";
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
			this.DGR_CURR_LIBUR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_CURR_LIBUR_ItemCommand);
			this.DGR_CURR_LIBUR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_CURR_LIBUR_PageIndexChanged);
			this.DGR_REQ_LIBUR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQ_LIBUR_ItemCommand);
			this.DGR_REQ_LIBUR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQ_LIBUR_PageIndexChanged);

		}
		#endregion

		private void FillDDLTahun()
		{
			DDL_TAHUN.Items.Clear();
			DDL_TAHUN.Items.Add(new ListItem("--Pilih--", ""));
			for (int i = 2011; i <= 3011; i++)
			{
				DDL_TAHUN.Items.Add(new ListItem(i.ToString(), i.ToString()));
			}
		}

		private void FillDDLBulan()
		{
			DDL_BULAN.Items.Clear();
			DDL_BULAN.Items.Add(new ListItem("--Pilih--", ""));
			for(int i = 1; i <= 12; i++)
			{
				DDL_BULAN.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
			}
		}

		private void FillGridCurr()
		{
			conn.QueryString = "SELECT * FROM VW_RF_LIBUR ORDER BY CONVERT(INT, TAHUN), CONVERT(INT, BULAN)";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_CURR_LIBUR.DataSource = dt;
			try
			{
				DGR_CURR_LIBUR.DataBind();
			}
			catch
			{
				DGR_CURR_LIBUR.CurrentPageIndex = 0;
				DGR_CURR_LIBUR.DataBind();
			}

			LinkButton lnk;

			for (int i = 0; i < DGR_CURR_LIBUR.Items.Count; i++)
			{
				if (DGR_CURR_LIBUR.Items[i].Cells[5].Text == "0")	//active		-- param deleted
				{
					lnk = (LinkButton)DGR_CURR_LIBUR.Items[i].Cells[7].FindControl("LNK_DELETE");
					lnk.Visible = false;
					lnk = (LinkButton)DGR_CURR_LIBUR.Items[i].Cells[7].FindControl("LNK_EDIT");
					lnk.Visible = false;
					DGR_CURR_LIBUR.Items[i].Cells[1].ForeColor = Color.Gray;		
					DGR_CURR_LIBUR.Items[i].Cells[3].ForeColor = Color.Gray;
					DGR_CURR_LIBUR.Items[i].Cells[4].ForeColor = Color.Gray;
				}
				else
				{
					lnk = (LinkButton)DGR_CURR_LIBUR.Items[i].Cells[7].FindControl("LNK_UNDELETE");
					lnk.Visible = false;
				}
			}

			
			for (int i = 0; i < DGR_CURR_LIBUR.Items.Count; i++)
			{
				if(DGR_CURR_LIBUR.Items[i].Cells[6].Text == "1") //sedang di update/remove/undelete
				{
					lnk = (LinkButton) DGR_CURR_LIBUR.Items[i].Cells[7].FindControl("LNK_DELETE");
					lnk.Visible = false;
					lnk = (LinkButton) DGR_CURR_LIBUR.Items[i].Cells[7].FindControl("LNK_EDIT");
					lnk.Visible = false;
					lnk = (LinkButton) DGR_CURR_LIBUR.Items[i].Cells[7].FindControl("LNK_UNDELETE");
					lnk.Visible = false;
				}
			}
		}

		private void FillGridReq()
		{
			conn.QueryString = "SELECT * FROM VW_PENDING_RF_LIBUR";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_REQ_LIBUR.DataSource = dt;
			try
			{
				DGR_REQ_LIBUR.DataBind();
			}
			catch
			{
				DGR_REQ_LIBUR.CurrentPageIndex = 0;
				DGR_REQ_LIBUR.DataBind();
			}

			LinkButton lnk;

			for (int i = 0; i < DGR_REQ_LIBUR.Items.Count; i++)
			{
				if (DGR_REQ_LIBUR.Items[i].Cells[6].Text == "2" || DGR_REQ_LIBUR.Items[i].Cells[6].Text == "3")	
				{
					lnk = (LinkButton)DGR_REQ_LIBUR.Items[i].Cells[8].FindControl("LNK_EDIT_REQ");
					lnk.Visible = false;
				}
				else
				{
					lnk = (LinkButton)DGR_REQ_LIBUR.Items[i].Cells[8].FindControl("LNK_DELETE_REQ");
					lnk.Visible = true;
					lnk = (LinkButton)DGR_REQ_LIBUR.Items[i].Cells[8].FindControl("LNK_EDIT_REQ");
					lnk.Visible = true;
				}
			}
		}

		private void ClearData()
		{
			DDL_TAHUN.SelectedValue = "";
			DDL_BULAN.SelectedValue = "";
			TXT_TGL_LIBUR.Text = "";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}

		protected void BTN_TANGGAL_Click(object sender, System.EventArgs e)
		{
			if(DDL_TAHUN.SelectedValue == "")
			{
				GlobalTools.popMessage(this,"Pilih tahun terlebih dahulu!");
				return;
			}

			if(DDL_BULAN.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Pilih bulan terlebih dahulu!");
				return;
			}

			Response.Write("<script language='javascript'>window.open('Calendar.aspx?bulan=" + DDL_BULAN.SelectedValue + "&tahun=" + DDL_TAHUN.SelectedValue + "&theForm=Form1&theObj=TXT_TGL_LIBUR','Calendar','status=no,scrollbars=no,width=380,height=250');</script>");
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if(DDL_BULAN.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Nama bulan tidak boleh kosong!");
				return;
			}
			if(DDL_TAHUN.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Tahun tidak boleh kosong!");
				return;
			}
			if(TXT_TGL_LIBUR.Text == "")
			{
				GlobalTools.popMessage(this, "Tanggal libur tidak boleh kosong!");
				return;
			}

			try
			{
				conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RF_LIBUR_CHECK_DB '" +
					DDL_BULAN.SelectedValue + "', '" + DDL_TAHUN.SelectedValue + "', '" +
					TXT_TGL_LIBUR.Text + "'";
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

			conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RF_LIBUR_INSERT '" +
				DDL_TAHUN.SelectedValue + "', '" +
				DDL_BULAN.SelectedValue + "', '" +
				TXT_TGL_LIBUR.Text + "', '" +
				LBL_SAVEMODE.Text + "', " +
				int.Parse(LBL_SEQ.Text) + ", " +
				int.Parse(LBL_SEQ_CURR.Text);
			conn.ExecuteNonQuery();

			if(LBL_EDIT.Text == "1")
			{
				conn.QueryString = "UPDATE RF_LIBUR SET EDIT_PARAM='1' WHERE SEQ='" + LBL_SEQ_CURR.Text + "'";
				conn.ExecuteQuery();

				LBL_EDIT.Text="0";
			}

			ClearData();
			LBL_SAVEMODE.Text = "1";
			LBL_SEQ_CURR.Text = "0";
			LBL_SEQ.Text = "0";
			FillGridCurr();
			FillGridReq();
		}

		protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
		{
			ClearData();
		}

		private void DGR_CURR_LIBUR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					try{DDL_TAHUN.SelectedValue = e.Item.Cells[1].Text.Trim();}
					catch{DDL_TAHUN.SelectedValue = "";}
					try{DDL_BULAN.SelectedValue = e.Item.Cells[2].Text.Trim();}
					catch{DDL_BULAN.SelectedValue = "";}
					TXT_TGL_LIBUR.Text = e.Item.Cells[4].Text.Trim();
					LBL_SEQ_CURR.Text = e.Item.Cells[0].Text.Trim();
					LBL_SAVEMODE.Text = "0";
					break;
				case "delete":
					LBL_SAVEMODE.Text = "2";

					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RF_LIBUR_INSERT '" +
						e.Item.Cells[1].Text.Trim() + "', '" + 
						e.Item.Cells[2].Text.Trim() + "', '" + 
						e.Item.Cells[4].Text.Trim() + "', '" +
						LBL_SAVEMODE.Text + "', " +
						int.Parse(e.Item.Cells[5].Text.Trim()) + ", " +
						int.Parse(e.Item.Cells[0].Text.Trim());
					conn.ExecuteNonQuery();

					FillGridCurr();
					FillGridReq();
					LBL_SAVEMODE.Text = "1";
					break;
				case "undelete":
					LBL_SAVEMODE.Text = "3";

					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RF_LIBUR_INSERT '" +
						e.Item.Cells[1].Text.Trim() + "', '" + 
						e.Item.Cells[2].Text.Trim() + "', '" + 
						e.Item.Cells[4].Text.Trim() + "', '" +
						LBL_SAVEMODE.Text + "', " +
						int.Parse(e.Item.Cells[5].Text.Trim()) + ", " +
						int.Parse(e.Item.Cells[0].Text.Trim());
					conn.ExecuteNonQuery();

					FillGridCurr();
					FillGridReq();
					LBL_SAVEMODE.Text = "1";
					break;
			}
			LBL_EDIT.Text = "1";
		}

		private void DGR_CURR_LIBUR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_CURR_LIBUR.CurrentPageIndex = e.NewPageIndex;
			FillGridCurr();
		}

		private void DGR_REQ_LIBUR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit_req":
					try{DDL_TAHUN.SelectedValue = e.Item.Cells[2].Text.Trim();}
					catch{DDL_TAHUN.SelectedValue = "";}
					try{DDL_BULAN.SelectedValue = e.Item.Cells[3].Text.Trim();}
					catch{DDL_BULAN.SelectedValue = "";}
					LBL_SEQ.Text = e.Item.Cells[0].Text.Trim();
					LBL_SAVEMODE.Text = e.Item.Cells[6].Text.Trim();
					LBL_SEQ_CURR.Text = e.Item.Cells[1].Text.Trim();
					LBL_EDIT.Text = "1";
					break;
				case "delete_req":
					conn.QueryString = "DELETE PENDING_RF_LIBUR WHERE SEQ ='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteQuery();
					conn.QueryString = "update rf_libur set edit_param='0' where seq='" + e.Item.Cells[1].Text.Trim() + "'";
					conn.ExecuteQuery();
					FillGridCurr();
					FillGridReq();
					break;
			}
		}

		private void DGR_REQ_LIBUR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQ_LIBUR.CurrentPageIndex = e.NewPageIndex;
			FillGridReq();
		}
	}
}
