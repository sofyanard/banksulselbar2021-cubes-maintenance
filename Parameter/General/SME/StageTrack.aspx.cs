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
	/// Summary description for StageTrack.
	/// </summary>
	public partial class StageTrack : System.Web.UI.Page
	{
		protected Connection conn;
		protected Tools tool = new Tools();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
			
			if(!IsPostBack)
			{
				FillDDLStageName();
				FillDDLTrackCode();
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
			this.DGR_CURR_STAGETRACK.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_CURR_STAGETRACK_ItemCommand);
			this.DGR_CURR_STAGETRACK.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_CURR_STAGETRACK_PageIndexChanged);
			this.DGR_REQ_STAGETRACK.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQ_STAGETRACK_ItemCommand);
			this.DGR_REQ_STAGETRACK.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQ_STAGETRACK_PageIndexChanged);

		}
		#endregion

		private void FillDDLStageName()
		{
			//string stagename;
			DDL_STAGENAME.Items.Clear();
			DDL_STAGENAME.Items.Add(new ListItem("--Pilih--", ""));

			conn.QueryString = "SELECT * FROM RF_STAGE WHERE ACTIVE='1'";
			conn.ExecuteQuery();

			for (int i = 0; i < conn.GetRowCount(); i++)
			{
				//stagename = conn.GetFieldValue(i,0) + " - " + conn.GetFieldValue(i,1);
				DDL_STAGENAME.Items.Add(new ListItem(conn.GetFieldValue(i,0) + " - " + conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
			}
		}

		private void FillDDLTrackCode()
		{
			//string trackname;
			DDL_TRACKCODE.Items.Clear();
			DDL_TRACKCODE.Items.Add(new ListItem("--Pilih--", ""));

			//conn.QueryString = "SELECT * FROM RFTRACK WHERE TRACKCODE NOT LIKE '[ABCGT]%'";
            conn.QueryString = "SELECT * FROM RFTRACK WHERE ACTIVE = '1'";
			conn.ExecuteQuery();

			for (int i = 0; i < conn.GetRowCount(); i++)
			{
				//trackname = conn.GetFieldValue(i,0) + " " + conn.GetFieldValue(i,1);
				DDL_TRACKCODE.Items.Add(new ListItem(conn.GetFieldValue(i,0) + " " + conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
			}
		}

		private void FillGridCurr()
		{
			conn.QueryString = "SELECT * FROM VW_RF_STAGE_TRACK";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_CURR_STAGETRACK.DataSource = dt;
			try
			{
				DGR_CURR_STAGETRACK.DataBind();
			}
			catch
			{
				DGR_CURR_STAGETRACK.CurrentPageIndex = 0;
				DGR_CURR_STAGETRACK.DataBind();
			}

			LinkButton lnk;

			for (int i = 0; i < DGR_CURR_STAGETRACK.Items.Count; i++)
			{
				if (DGR_CURR_STAGETRACK.Items[i].Cells[6].Text == "0")	//active		-- param deleted
				{
					lnk = (LinkButton)DGR_CURR_STAGETRACK.Items[i].Cells[7].FindControl("LNK_DELETE");
					lnk.Visible = false;
					lnk = (LinkButton)DGR_CURR_STAGETRACK.Items[i].Cells[7].FindControl("LNK_EDIT");
					lnk.Visible = false;
					DGR_CURR_STAGETRACK.Items[i].Cells[1].ForeColor = Color.Gray;		//stage name
					DGR_CURR_STAGETRACK.Items[i].Cells[3].ForeColor = Color.Gray;		//track name
				}
				else
				{
					lnk = (LinkButton)DGR_CURR_STAGETRACK.Items[i].Cells[7].FindControl("LNK_UNDELETE");
					lnk.Visible = false;
				}
			}

			
			for (int i = 0; i < DGR_CURR_STAGETRACK.Items.Count; i++)
			{
				if(DGR_CURR_STAGETRACK.Items[i].Cells[8].Text == "1") //sedang di update/remove/undelete
				{
					lnk = (LinkButton) DGR_CURR_STAGETRACK.Items[i].Cells[7].FindControl("LNK_DELETE");
					lnk.Visible = false;
					lnk = (LinkButton) DGR_CURR_STAGETRACK.Items[i].Cells[7].FindControl("LNK_EDIT");
					lnk.Visible = false;
					lnk = (LinkButton) DGR_CURR_STAGETRACK.Items[i].Cells[7].FindControl("LNK_UNDELETE");
					lnk.Visible = false;
				}
			}
		}

		private void FillGridReq()
		{
			conn.QueryString = "SELECT * FROM VW_PENDING_RF_STAGE_TRACK";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_REQ_STAGETRACK.DataSource = dt;
			try
			{
				DGR_REQ_STAGETRACK.DataBind();
			}
			catch
			{
				DGR_REQ_STAGETRACK.CurrentPageIndex = 0;
				DGR_REQ_STAGETRACK.DataBind();
			}

			LinkButton lnk;

			for (int i = 0; i < DGR_REQ_STAGETRACK.Items.Count; i++)
			{
				if (DGR_REQ_STAGETRACK.Items[i].Cells[4].Text == "2" || DGR_REQ_STAGETRACK.Items[i].Cells[4].Text == "3")	
				{
					lnk = (LinkButton)DGR_REQ_STAGETRACK.Items[i].Cells[7].FindControl("LNK_EDIT_REQ");
					lnk.Visible = false;
				}
				else
				{
					lnk = (LinkButton)DGR_REQ_STAGETRACK.Items[i].Cells[7].FindControl("LNK_DELETE_REQ");
					lnk.Visible = true;
					lnk = (LinkButton)DGR_REQ_STAGETRACK.Items[i].Cells[7].FindControl("LNK_EDIT_REQ");
					lnk.Visible = true;
				}
			}
		}

		private void ClearData()
		{
			DDL_STAGENAME.SelectedValue = "";
			DDL_TRACKCODE.SelectedValue = "";
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if(DDL_STAGENAME.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Stage name tidak boleh kosong!");
				return;
			}
			if(DDL_TRACKCODE.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Track code tidak boleh kosong!");
				return;
			}

			try
			{
				conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RF_STAGETRACK_CHECK_DB '" +
					DDL_STAGENAME.SelectedValue + "', '" + DDL_TRACKCODE.SelectedValue + "'";
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

			/*conn.QueryString = "SELECT * FROM VW_PENDING_RF_STAGE_TRACK WHERE STAGE_CD='" + DDL_STAGENAME.SelectedValue + "' AND TRACK_CD='" + DDL_TRACKCODE.SelectedValue + "'";
			conn.ExecuteQuery();

			if(conn.GetRowCount() > 0)
			{
				GlobalTools.popMessage(this,"Kombinasi " + conn.GetFieldValue("STAGE_DESC") + " dan " + conn.GetFieldValue("TRACKNAME") + " sudah ada di daftar request stage-track");
				return;
			}

			conn.QueryString = "SELECT * FROM VW_RF_STAGE_TRACK WHERE STAGE_CD='" + DDL_STAGENAME.SelectedValue + "' AND TRACK_CD='" + DDL_TRACKCODE.SelectedValue + "'";
			conn.ExecuteQuery();

			if(conn.GetRowCount() > 0)
			{
				GlobalTools.popMessage(this,"Kombinasi " + conn.GetFieldValue("STAGE_DESC") + " dan " + conn.GetFieldValue("TRACKNAME") + " sudah ada di daftar current stage-track");
				return;
			}*/


			conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RFSTAGE_TRACK_INSERT '" +
				DDL_STAGENAME.SelectedValue + "', '" +
				DDL_TRACKCODE.SelectedValue + "', '" +
				LBL_SAVEMODE.Text + "', " +
				int.Parse(LBL_SEQ.Text) + ", " +
				int.Parse(LBL_SEQ_CURR.Text);
			conn.ExecuteNonQuery();

			if(LBL_EDIT.Text == "1")
			{
				conn.QueryString = "UPDATE RF_STAGE_TRACK SET EDIT_PARAM='1' WHERE SEQ='" + LBL_SEQ_CURR.Text + "'";
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

		private void DGR_CURR_STAGETRACK_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					try{DDL_STAGENAME.SelectedValue = e.Item.Cells[0].Text.Trim();}
					catch{DDL_STAGENAME.SelectedValue = "";}
					try{DDL_TRACKCODE.SelectedValue = e.Item.Cells[2].Text.Trim();}
					catch{DDL_TRACKCODE.SelectedValue = "";}
					LBL_SEQ_CURR.Text = e.Item.Cells[5].Text.Trim();
					LBL_SAVEMODE.Text = "0";
					break;
				case "delete":
					LBL_SAVEMODE.Text = "2";

					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RFSTAGE_TRACK_INSERT '" +
						e.Item.Cells[0].Text.Trim() + "', '" + 
						e.Item.Cells[2].Text.Trim() + "', '" + 
						LBL_SAVEMODE.Text + "', " +
						int.Parse(e.Item.Cells[5].Text.Trim()) + ", " +
						int.Parse(e.Item.Cells[5].Text.Trim());
					conn.ExecuteNonQuery();

					FillGridCurr();
					FillGridReq();
					LBL_SAVEMODE.Text = "1";
					break;
				case "undelete":
					LBL_SAVEMODE.Text = "3";

					conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RFSTAGE_TRACK_INSERT '" +
						e.Item.Cells[0].Text.Trim() + "', '" + 
						e.Item.Cells[2].Text.Trim() + "', '" + 
						LBL_SAVEMODE.Text + "', " +
						int.Parse(e.Item.Cells[5].Text.Trim()) + ", " +
						int.Parse(e.Item.Cells[5].Text.Trim());
					conn.ExecuteNonQuery();

					FillGridCurr();
					FillGridReq();
					LBL_SAVEMODE.Text = "1";
					break;
			}
			LBL_EDIT.Text = "1";
		}

		private void DGR_CURR_STAGETRACK_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_CURR_STAGETRACK.CurrentPageIndex = e.NewPageIndex;
			FillGridCurr();
		}

		private void DGR_REQ_STAGETRACK_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQ_STAGETRACK.CurrentPageIndex = e.NewPageIndex;
			FillGridReq();
		}

		private void DGR_REQ_STAGETRACK_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit_req":
					try{DDL_STAGENAME.SelectedValue = e.Item.Cells[0].Text.Trim();}
					catch{DDL_STAGENAME.SelectedValue = "";}
					try{DDL_TRACKCODE.SelectedValue = e.Item.Cells[2].Text.Trim();}
					catch{DDL_TRACKCODE.SelectedValue = "";}
					LBL_SEQ.Text = e.Item.Cells[6].Text.Trim();
					LBL_SAVEMODE.Text = e.Item.Cells[4].Text.Trim();
					LBL_SEQ_CURR.Text = e.Item.Cells[8].Text.Trim();
					LBL_EDIT.Text = "1";
					break;
				case "delete_req":
					conn.QueryString = "DELETE PENDING_RF_STAGE_TRACK WHERE SEQ ='" + e.Item.Cells[6].Text.Trim() + "'";
					conn.ExecuteQuery();
					conn.QueryString = "update rf_stage_track set edit_param='0' where seq='" + e.Item.Cells[8].Text.Trim() + "'";
					conn.ExecuteQuery();
					FillGridCurr();
					FillGridReq();
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}
	}
}
