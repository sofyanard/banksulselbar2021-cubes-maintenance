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
	/// Summary description for RFRatingTotalAdjustment.
	/// </summary>
	public partial class RFRatingTotalAdjustment : System.Web.UI.Page
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

				viewExistingData();
				viewPendingData();
			}
		}

		private void viewExistingData() 
		{
			conn.QueryString = "select * from RFSCOREBCGTOTALADJUSTMENT ";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("MIN_RANGE"));
			dt.Columns.Add(new DataColumn("MAX_RANGE"));
			dt.Columns.Add(new DataColumn("ACTIVE"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,"CODE");
				dr[1] = conn.GetFieldValue(i,"DESC");
				dr[2] = conn.GetFieldValue(i,"MIN_RANGE");
				dr[3] = conn.GetFieldValue(i,"MAX_RANGE");
				dr[4] = conn.GetFieldValue(i,"ACTIVE");
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

				if (DGExisting.Items[i].Cells[4].Text.Trim() =="0" ) 
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
			//int pendCol = 2;
			string pendCol = "ACTIVE";

			conn.QueryString = "select * from PENDING_RFSCOREBCGTOTALADJUSTMENT";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("MIN_RANGE"));
			dt.Columns.Add(new DataColumn("MAX_RANGE"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			if (LBL_ACTIVE.Text.Trim() == "1")
				pendCol = "PENDINGSTATUS";
			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,"CODE");
				dr[1] = conn.GetFieldValue(i,"DESC");
				dr[2] = conn.GetFieldValue(i,"MIN_RANGE");
				dr[3] = conn.GetFieldValue(i,"MAX_RANGE");
				dr[4] = conn.GetFieldValue(i,pendCol);
				dr[5] = getPendingStatus(conn.GetFieldValue(i,pendCol));
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
			TXT_ID.Text = "";
			TXT_DESC.Text = "";
			TXT_MIN_RANGE.Text = "";
			TXT_MAX_RANGE.Text = "";
			activateControlKey(false);
		}

		private void activateControlKey(bool isReadOnly) 
		{
			TXT_ID.ReadOnly = isReadOnly;
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

			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select active from PENDING_RFSCOREBCGTOTALADJUSTMENT WHERE [CODE] ='" + TXT_ID.Text.Trim() + "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					active = conn.GetFieldValue("active");
					if (active == "1")
					{
						Tools.popMessage(this, "ID has already been used! Request canceled!");
						return;
					}
					else
					{
						LBL_SAVEMODE.Text ="0";
					}
				}
			}		
				
			executeMaker(TXT_ID.Text.Trim(), TXT_DESC.Text.Trim(), TXT_MIN_RANGE.Text.Trim(), TXT_MAX_RANGE.Text.Trim(), LBL_SAVEMODE.Text.Trim());
			viewPendingData();
			clearControls();

			LBL_SAVEMODE.Text = "1";
		}

		private void executeMaker(string id, string desc, string min_range, string max_range, string pendingStatus) 
		{
			conn.QueryString = "SELECT COUNT(*) JUMLAH FROM PENDING_RFSCOREBCGTOTALADJUSTMENT WHERE [CODE] ='" +id+ "'";
			conn.ExecuteQuery();

			int jumlah = Convert.ToInt16(conn.GetFieldValue("JUMLAH"));
			
			if (jumlah > 0) 
			{				
				conn.QueryString = "UPDATE PENDING_RFSCOREBCGTOTALADJUSTMENT SET [DESC]= '" + desc + 
					"', MIN_RANGE = " + double.Parse(min_range) +
					", MAX_RANGE = " + double.Parse(min_range) +
					", PENDINGSTATUS = '" +pendingStatus+ "' WHERE [CODE] = '" + id + "'";
				conn.ExecuteQuery();
			}
			else 
			{
				if (LBL_ACTIVE.Text.Trim() == "1")
					conn.QueryString = "INSERT INTO PENDING_RFSCOREBCGTOTALADJUSTMENT " +
						"([CODE], [DESC], [MIN_RANGE], [MAX_RANGE], [ACTIVE], [PENDINGSTATUS]) VALUES ('" +
						id + "', '" + desc + "', " + double.Parse(min_range) + ", " + double.Parse(max_range) + ", '1', '" + pendingStatus + "')";
				else if (LBL_ACTIVE.Text.Trim() == "0")
					conn.QueryString = "INSERT INTO PENDING_RFSCOREBCGTOTALADJUSTMENT " +
						"([CODE], [DESC], [MIN_RANGE], [MAX_RANGE], [ACTIVE], [PENDINGSTATUS]) VALUES ('" +
						id + "', '" + desc + "', " + double.Parse(min_range) + ", " + double.Parse(max_range) + ", '0', '" + pendingStatus + "')";
				try 
				{
					conn.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					if (ex.Message.ToString().IndexOf("truncate") > 0)
						Tools.popMessage(this, "Input melebihi batas !");
				}
			}
		}

		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string id;

			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[1].Text;
					TXT_MIN_RANGE.Text = e.Item.Cells[2].Text;
					TXT_MAX_RANGE.Text = e.Item.Cells[3].Text;
					activateControlKey(true);
					break;

				case "delete":					
					id = e.Item.Cells[0].Text.Trim();
					
					executeMaker(id, e.Item.Cells[1].Text, e.Item.Cells[2].Text, e.Item.Cells[3].Text, "2");
					viewPendingData();
					break;

				case "undelete":					
					id = e.Item.Cells[0].Text.Trim();
					
					executeMaker(id, e.Item.Cells[1].Text, e.Item.Cells[2].Text, e.Item.Cells[3].Text, "0");
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
					LBL_SAVEMODE.Text = e.Item.Cells[4].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[1].Text;
					TXT_MIN_RANGE.Text = e.Item.Cells[2].Text;
					TXT_MAX_RANGE.Text = e.Item.Cells[3].Text;
					activateControlKey(true);
					break;

				case "delete":
					string id = e.Item.Cells[0].Text;

					conn.QueryString = "delete from PENDING_RFSCOREBCGTOTALADJUSTMENT WHERE [CODE] ='" + id + "'";
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
