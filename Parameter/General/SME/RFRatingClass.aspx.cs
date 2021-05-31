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
	/// Summary description for RFRatingClass.
	/// </summary>
	public partial class RFRatingClass : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TXT_SEQ;
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack) 
			{
				LBL_SAVEMODE.Text = "1";
				LBL_ACTIVE.Text = Request.QueryString["active"];
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

				GlobalTools.fillRefList(DDL_SCOTPL_ID, "SELECT SCOTPL_ID, SCOTPL_DESC FROM PRMSCORING_TEMPLATE", false, conn);

				viewExistingData();
				viewPendingData();
			}
		}

		private void viewExistingData() 
		{
			conn.QueryString = "select * from VW_PARAM_RFRATINGCLASS order by SCOTPL_ID, RATEID";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("SCOTPL_ID"));
			dt.Columns.Add(new DataColumn("SCOTPL_DESC"));
			dt.Columns.Add(new DataColumn("RATEID"));
			dt.Columns.Add(new DataColumn("RATEDESC"));
			dt.Columns.Add(new DataColumn("MINPD"));
			dt.Columns.Add(new DataColumn("MAXPD"));
			dt.Columns.Add(new DataColumn("PD_REMARK"));
			dt.Columns.Add(new DataColumn("MINSCORE"));
			dt.Columns.Add(new DataColumn("MAXSCORE"));
			dt.Columns.Add(new DataColumn("SCORE_REMARK"));
			dt.Columns.Add(new DataColumn("ACTIVE"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,"SCOTPL_ID");
				dr[1] = conn.GetFieldValue(i,"SCOTPL_DESC");
				dr[2] = conn.GetFieldValue(i,"RATEID");
				dr[3] = conn.GetFieldValue(i,"RATEDESC");
				dr[4] = conn.GetFieldValue(i,"MINPD");
				dr[5] = conn.GetFieldValue(i,"MAXPD");
				dr[6] = conn.GetFieldValue(i,"PD_REMARK");
				dr[7] = conn.GetFieldValue(i,"MINSCORE");
				dr[8] = conn.GetFieldValue(i,"MAXSCORE");
				dr[9] = conn.GetFieldValue(i,"SCORE_REMARK");
				dr[10] = conn.GetFieldValue(i,"ACTIVE");
				dt.Rows.Add(dr);
			}			

			DGExisting.DataSource = new DataView(dt);
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

				if (DGExisting.Items[i].Cells[10].Text.Trim() =="0" ) 
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
			string pendCol = "ACTIVE";

			conn.QueryString = "select * from VW_PARAM_PENDING_RFRATINGCLASS order by SCOTPL_ID, RATEID";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("SCOTPL_ID"));
			dt.Columns.Add(new DataColumn("SCOTPL_DESC"));
			dt.Columns.Add(new DataColumn("RATEID"));
			dt.Columns.Add(new DataColumn("RATEDESC"));
			dt.Columns.Add(new DataColumn("MINPD"));
			dt.Columns.Add(new DataColumn("MAXPD"));
			dt.Columns.Add(new DataColumn("PD_REMARK"));
			dt.Columns.Add(new DataColumn("MINSCORE"));
			dt.Columns.Add(new DataColumn("MAXSCORE"));
			dt.Columns.Add(new DataColumn("SCORE_REMARK"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			if (LBL_ACTIVE.Text.Trim() == "1")
				pendCol = "PENDINGSTATUS";
			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,"SCOTPL_ID");
				dr[1] = conn.GetFieldValue(i,"SCOTPL_DESC");
				dr[2] = conn.GetFieldValue(i,"RATEID");
				dr[3] = conn.GetFieldValue(i,"RATEDESC");
				dr[4] = conn.GetFieldValue(i,"MINPD");
				dr[5] = conn.GetFieldValue(i,"MAXPD");
				dr[6] = conn.GetFieldValue(i,"PD_REMARK");
				dr[7] = conn.GetFieldValue(i,"MINSCORE");
				dr[8] = conn.GetFieldValue(i,"MAXSCORE");
				dr[9] = conn.GetFieldValue(i,"SCORE_REMARK");
				dr[10] = conn.GetFieldValue(i,pendCol);
				dr[11] = getPendingStatus(conn.GetFieldValue(i,pendCol));
				dt.Rows.Add(dr);
			}			

			DGRequest.DataSource = new DataView(dt);
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

		private string getPendingStatus(string saveMode) 
		{
			string status = "";			
			switch (saveMode)
			{
				case "0":
					status = "Update";
					break;
				case "1":
					status = "Insert";
					break;
				case "2":
					status = "Delete";
					break;
				default:
					status = "";
					break;
			}
			return status;
		}

		private void clearControls() 
		{
			DDL_SCOTPL_ID.SelectedValue = "";
			TXT_ID.Text = "";
			TXT_DESC.Text = "";
			TXT_MINPD.Text = "";
			TXT_MAXPD.Text = "";
			TXT_PD_REMARK.Text = "";
			TXT_MINSCORE.Text = "";
			TXT_MAXSCORE.Text = "";
			TXT_SCORE_REMARK.Text = "";
			activateControlKey(false);
		}

		private void activateControlKey(bool isReadOnly) 
		{
			DDL_SCOTPL_ID.Enabled = !isReadOnly;
			TXT_ID.ReadOnly = isReadOnly;
			TXT_DESC.ReadOnly = isReadOnly;
		}

		private void executeMaker(string scotpl_id, string id, string desc, string minpd, string maxpd, string pdremark, string minscore, string maxscore, string scoreremark, string pendingStatus)
		{
			conn.QueryString = "SELECT COUNT(*) JUMLAH FROM PENDING_RFRATINGCLASS WHERE SCOTPL_ID = '" + scotpl_id + "' AND RATEID ='" +id+ "'";
			conn.ExecuteQuery();

			int jumlah = Convert.ToInt16(conn.GetFieldValue("JUMLAH"));
			
			if (jumlah > 0) 
			{				
				conn.QueryString = "UPDATE PENDING_RFRATINGCLASS SET RATEDESC = '" + desc + 
					"', MINPD = " + double.Parse(minpd.Trim()).ToString().Replace(",",".") +
					", MAXPD = " + double.Parse(maxpd.Trim()).ToString().Replace(",",".") +
					", PD_REMARK = '" + pdremark + "'" +
					", MINSCORE = " + double.Parse(minscore.Trim()).ToString().Replace(",",".") +
					", MAXSCORE = " + double.Parse(maxscore.Trim()).ToString().Replace(",",".") +
					", SCORE_REMARK = '" + scoreremark + "'" +
					", PENDINGSTATUS = '" + pendingStatus + 
					"' WHERE SCOTPL_ID = '" + scotpl_id + "' AND RATEID = '" + id + "'";
				conn.ExecuteQuery();
			}
			else 
			{
				if (LBL_ACTIVE.Text.Trim() == "1")
					conn.QueryString = "INSERT INTO PENDING_RFRATINGCLASS " +
						"(SCOTPL_ID, RATEID, RATEDESC, MINPD, MAXPD, PD_REMARK, MINSCORE, MAXSCORE, SCORE_REMARK, ACTIVE, PENDINGSTATUS) VALUES ('" +
						scotpl_id + "', '" + id + "', '" + desc + "', " + 
						double.Parse(minpd.Trim()).ToString().Replace(",",".") + ", " + 
						double.Parse(maxpd.Trim()).ToString().Replace(",",".") + ", " + 
						"'" + pdremark + "', " +
						double.Parse(minscore.Trim()).ToString().Replace(",",".") + ", " + 
						double.Parse(maxscore.Trim()).ToString().Replace(",",".") + ", " + 
						"'" + scoreremark + "' " +
						", '1', '" + pendingStatus + "')";
				else if (LBL_ACTIVE.Text.Trim() == "0")
					conn.QueryString = "INSERT INTO PENDING_RFRATINGCLASS " +
						"(SCOTPL_ID, RATEID, RATEDESC, MINPD, MAXPD, MINSCORE, MAXSCORE, ACTIVE, PENDINGSTATUS) VALUES ('" +
						scotpl_id + "', '" + id + "', '" + desc + "', " + 
						double.Parse(minpd.Trim()).ToString().Replace(",",".") + ", " + 
						double.Parse(maxpd.Trim()).ToString().Replace(",",".") + ", " + 
						"'" + pdremark + "', " +
						double.Parse(minscore.Trim()).ToString().Replace(",",".") + ", " + 
						double.Parse(maxscore.Trim()).ToString().Replace(",",".") + ", " + 
						"'" + scoreremark + "' " +
						", '0', '" + pendingStatus + "')";
				try 
				{
					conn.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					Tools.popMessage(this, "Input error !");
					Response.Write("<!--" + ex.ToString() + "-->");
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

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string active="0";

			if (DDL_SCOTPL_ID.SelectedValue.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Rating Template tidak boleh kosong!");
				GlobalTools.SetFocus(this,DDL_SCOTPL_ID);
				return;
			}
			else if (TXT_ID.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Seq tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_ID);
				return;
			}
			else if (TXT_DESC.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Rating Class tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_DESC);
				return;
			}
			else if (TXT_MINPD.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Min PD tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_MINPD);
				return;
			}
			else if (TXT_MAXPD.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Max PD tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_MAXPD);
				return;
			}
			else if (TXT_MINSCORE.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Min Score tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_MINSCORE);
				return;
			}
			else if (TXT_MAXSCORE.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Max Score tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_MAXSCORE);
				return;
			}

			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select active from PENDING_RFRATINGCLASS WHERE SCOTPL_ID = '" + DDL_SCOTPL_ID.SelectedValue.Trim() + "' AND RATEID ='" + TXT_ID.Text.Trim() + "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					active = conn.GetFieldValue("active");
					if (active == "1")
					{
						Tools.popMessage(this, "Seq has already been used! Request canceled!");
						return;
					}
					else
					{
						LBL_SAVEMODE.Text ="0";
					}
				}
			}		
				
			executeMaker(DDL_SCOTPL_ID.SelectedValue.Trim(), TXT_ID.Text.Trim(), TXT_DESC.Text.Trim(), TXT_MINPD.Text.Trim(), TXT_MAXPD.Text.Trim(), TXT_PD_REMARK.Text.Trim(), TXT_MINSCORE.Text.Trim(), TXT_MAXSCORE.Text.Trim(), TXT_SCORE_REMARK.Text.Trim(), LBL_SAVEMODE.Text.Trim());
			viewPendingData();
			clearControls();

			LBL_SAVEMODE.Text = "1";
		}

		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string scotpl_id, id;

			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					try {DDL_SCOTPL_ID.SelectedValue = e.Item.Cells[0].Text;} 
					catch {}
					TXT_ID.Text = e.Item.Cells[2].Text;
					TXT_DESC.Text = e.Item.Cells[3].Text;
					TXT_MINPD.Text = e.Item.Cells[4].Text;
					TXT_MAXPD.Text = e.Item.Cells[5].Text;
					TXT_PD_REMARK.Text = e.Item.Cells[6].Text;
					TXT_MINSCORE.Text = e.Item.Cells[7].Text;
					TXT_MAXSCORE.Text = e.Item.Cells[8].Text;
					TXT_SCORE_REMARK.Text = e.Item.Cells[9].Text;
					activateControlKey(true);
					break;

				case "delete":					
					scotpl_id = e.Item.Cells[0].Text.Trim();
					id = e.Item.Cells[2].Text.Trim();
					
					executeMaker(scotpl_id, id, e.Item.Cells[3].Text, e.Item.Cells[4].Text, e.Item.Cells[5].Text, e.Item.Cells[6].Text, e.Item.Cells[7].Text, e.Item.Cells[8].Text, e.Item.Cells[9].Text, "2");
					viewPendingData();
					break;

				case "undelete":					
					scotpl_id = e.Item.Cells[0].Text.Trim();
					id = e.Item.Cells[2].Text.Trim();
					
					executeMaker(scotpl_id, id, e.Item.Cells[3].Text, e.Item.Cells[4].Text, e.Item.Cells[5].Text, e.Item.Cells[6].Text, e.Item.Cells[7].Text, e.Item.Cells[8].Text, e.Item.Cells[9].Text, "0");
					viewPendingData();
					break;

				default :
					break;
			}
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[10].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					try {DDL_SCOTPL_ID.SelectedValue = e.Item.Cells[0].Text;}
					catch {}
					TXT_ID.Text = e.Item.Cells[2].Text;
					TXT_DESC.Text = e.Item.Cells[3].Text;
					TXT_MINPD.Text = e.Item.Cells[4].Text;
					TXT_MAXPD.Text = e.Item.Cells[5].Text;
					TXT_PD_REMARK.Text = e.Item.Cells[6].Text;
					TXT_MINSCORE.Text = e.Item.Cells[7].Text;
					TXT_MAXSCORE.Text = e.Item.Cells[8].Text;
					TXT_SCORE_REMARK.Text = e.Item.Cells[9].Text;
					activateControlKey(true);
					break;

				case "delete":
					string scotpl_id = e.Item.Cells[0].Text, id = e.Item.Cells[2].Text;

					conn.QueryString = "delete from PENDING_RFRATINGCLASS WHERE SCOTPL_ID = '" + scotpl_id + "' AND RATEID ='" + id + "'";
					conn.ExecuteQuery();
					viewPendingData();
					break;
				default :
					break;
			}
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();
		}
	}
}