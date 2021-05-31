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

namespace CuBES_Maintenance.Parameter
{
	/// <summary>
	/// Summary description for LMSProcedurePath.
	/// </summary>
	public partial class LMSProcedurePath : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		int jml_row;
		string TRACKFROM, TRACKNEXT, TRACKBACK, TRACKFAIL;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IsiDDL();
				ViewExisting();
				ViewRequest();
			}
		}

		private void IsiDDL()
		{
			DDL_PP_TRACKFROM.Items.Add(new ListItem("-- Pilih --", ""));
			DDL_PP_TRACKNEXT.Items.Add(new ListItem("-- Pilih --", ""));
			DDL_PP_TRACKBACK.Items.Add(new ListItem("-- Pilih --", ""));
			DDL_PP_TRACKFAIL.Items.Add(new ListItem("-- Pilih --", ""));

			conn.QueryString = "select TRACKCODE, TRACKNAME from LMS_RFTRACK where ACTIVE = '1'";
			conn.ExecuteQuery();
			jml_row = conn.GetRowCount();
			for (int i=0; i<jml_row; i++)
			{
				TRACKFROM = conn.GetFieldValue(i, 0);
				DDL_PP_TRACKFROM.Items.Add(new ListItem(TRACKFROM +" - "+ conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
			}

			conn.QueryString = "select TRACKCODE, TRACKNAME from LMS_RFTRACK where ACTIVE = '1'";
			conn.ExecuteQuery();
			jml_row = conn.GetRowCount();
			for (int i=0; i<jml_row; i++)
			{
				TRACKFROM = conn.GetFieldValue(i, 0);
				DDL_PP_TRACKNEXT.Items.Add(new ListItem(TRACKFROM +" - "+ conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
			}

			conn.QueryString = "select TRACKCODE, TRACKNAME from LMS_RFTRACK where ACTIVE = '1'";
			conn.ExecuteQuery();
			jml_row = conn.GetRowCount();
			for (int i=0; i<jml_row; i++)
			{
				TRACKFROM = conn.GetFieldValue(i, 0);
				DDL_PP_TRACKBACK.Items.Add(new ListItem(TRACKFROM +" - "+ conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
			}

			conn.QueryString = "select TRACKCODE, TRACKNAME from LMS_RFTRACK where ACTIVE = '1'";
			conn.ExecuteQuery();
			jml_row = conn.GetRowCount();
			for (int i=0; i<jml_row; i++)
			{
				TRACKFROM = conn.GetFieldValue(i, 0);
				DDL_PP_TRACKFAIL.Items.Add(new ListItem(TRACKFROM +" - "+ conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
			}

		}

		private void ViewExisting()
		{
			conn.QueryString = "select * from VW_LMS_PROCEDUREPATH_VIEWEXISTING ";
			conn.ExecuteQuery();
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_CURRENT.DataSource = data;
			try 
			{
				DGR_CURRENT.DataBind();
			}
			catch 
			{
				DGR_CURRENT.CurrentPageIndex = DGR_CURRENT.PageCount - 1;
				DGR_CURRENT.DataBind();
			}
		}

		private void ViewRequest()
		{
			conn.QueryString = "select * from VW_LMS_PROCEDUREPATH_VIEWREQUEST ";
			conn.ExecuteQuery();
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_MAKER.DataSource = data;
			try 
			{
				DGR_MAKER.DataBind();
			}
			catch 
			{
				DGR_MAKER.CurrentPageIndex = DGR_MAKER.PageCount - 1;
				DGR_MAKER.DataBind();
			}
		}

		private void SaveMaker(string V_PP_TRACKFROM, string V_PP_TRACKNEXT, string V_PP_TRACKBACK, string V_PP_TRACKFAIL)
		{
			string V_TIPE = LBL_SAVEMODE.Text;
			string V_PENDINGSTATUS = LBL_PENDINGSTATUS.Text;

			switch (V_TIPE)
			{
				case "0" :
					// ASALNYA DARI CURRENT
					conn.QueryString = "exec LMS_PROCEDUREPATH_MAKER '" +
						V_PP_TRACKFROM + "', '" + 
						V_PP_TRACKNEXT + "', '" + 
						V_PP_TRACKBACK + "', '" + 
						V_PP_TRACKFAIL + "', '" + 
						V_PENDINGSTATUS +"' ";
					conn.ExecuteNonQuery();
					break;

				case "1" :
					// ASALNYA DARI MAKER
					if (DDL_PP_TRACKFROM.SelectedValue == LBL_PP_TRACKFROM_OLD.Text)
					{
						conn.QueryString = "update PENDING_LMS_PROCEDUREPATH set PP_TRACKNEXT = '" + DDL_PP_TRACKNEXT.SelectedValue + 
							"', PP_TRACKBACK = '" + DDL_PP_TRACKBACK.SelectedValue + 
							"', PP_TRACKFAIL = '" + DDL_PP_TRACKFAIL.SelectedValue + 
							"' where PP_TRACKFROM = '" + LBL_PP_TRACKFROM_OLD.Text + "' ";
						conn.ExecuteNonQuery();
					}
					else
					{
						conn.QueryString = "select PP_TRACKFROM from PENDING_LMS_PROCEDUREPATH where PP_TRACKFROM = '" +
							DDL_PP_TRACKFROM.SelectedValue + "' ";
						conn.ExecuteQuery();
						if (conn.GetRowCount() > 0)
							Tools.popMessage(this, "Procedure Path Exist...");
						else
						{
							conn.QueryString = "update PENDING_PROCEDUREPATH set PP_TRACKFROM = '"+ DDL_PP_TRACKFROM.SelectedValue + 
								"', PP_TRACKNEXT = '" + DDL_PP_TRACKNEXT.SelectedValue + 
								"', PP_TRACKBACK = '" + DDL_PP_TRACKBACK.SelectedValue + 
								"', PP_TRACKFAIL = '" + DDL_PP_TRACKFAIL.SelectedValue + 
								"' where PP_TRACKFROM = '" + LBL_PP_TRACKFROM_OLD.Text + "' ";
							conn.ExecuteNonQuery();
						}

					}
					break;
			}
			clearEditBoxes();
			ViewRequest();
		}

		private void clearEditBoxes()
		{
			activatePostBackControls(true);
			DDL_PP_TRACKFROM.SelectedValue = "";
			DDL_PP_TRACKNEXT.SelectedValue = "";
			DDL_PP_TRACKBACK.SelectedValue = "";
			DDL_PP_TRACKFAIL.SelectedValue = "";
			LBL_SAVEMODE.Text = "0";
			LBL_PENDINGSTATUS.Text = "1";
		}

		private void activatePostBackControls(bool mode)
		{
			DDL_PP_TRACKFROM.Enabled = mode;
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
			this.DGR_CURRENT.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_CURRENT_ItemCommand);
			this.DGR_CURRENT.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_CURRENT_PageIndexChanged);
			this.DGR_MAKER.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_MAKER_ItemCommand);
			this.DGR_MAKER.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_MAKER_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			SaveMaker(DDL_PP_TRACKFROM.SelectedValue, 
				DDL_PP_TRACKNEXT.SelectedValue,
				DDL_PP_TRACKBACK.SelectedValue,
				DDL_PP_TRACKFAIL.SelectedValue);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		private void DGR_CURRENT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			LBL_SAVEMODE.Text = "0";
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					try
					{
						DDL_PP_TRACKFROM.SelectedValue = e.Item.Cells[0].Text.Trim();
					} 
					catch{}
					TRACKNEXT = e.Item.Cells[2].Text.Trim();
					if (TRACKNEXT != "" && TRACKNEXT != "&nbsp;")
						DDL_PP_TRACKNEXT.SelectedValue = TRACKNEXT;
					else
						DDL_PP_TRACKNEXT.SelectedIndex = 0;
					TRACKBACK = e.Item.Cells[4].Text.Trim();
					if (TRACKBACK != "" && TRACKBACK != "&nbsp;")
						DDL_PP_TRACKBACK.SelectedValue = TRACKBACK;
					else
						DDL_PP_TRACKBACK.SelectedIndex = 0;
					TRACKFAIL = e.Item.Cells[6].Text.Trim();
					if (TRACKFAIL != "" && TRACKFAIL != "&nbsp;")
						DDL_PP_TRACKFAIL.SelectedValue = TRACKFAIL;
					else
						DDL_PP_TRACKFAIL.SelectedIndex = 0;
					LBL_PENDINGSTATUS.Text = "0";
					activatePostBackControls(false);
					break;

				case "delete":
					TRACKFROM = e.Item.Cells[0].Text.Trim();
					TRACKNEXT = e.Item.Cells[2].Text.Trim();
					TRACKBACK = e.Item.Cells[4].Text.Trim();
					TRACKFAIL = e.Item.Cells[6].Text.Trim();
					if (TRACKNEXT == "&nbsp;")
						TRACKNEXT = "";
					if (TRACKBACK == "&nbsp;")
						TRACKBACK = "";
					if (TRACKFAIL == "&nbsp;")
						TRACKFAIL = "";
					LBL_PENDINGSTATUS.Text = "2";
					SaveMaker(TRACKFROM, TRACKNEXT, TRACKBACK, TRACKFAIL);
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_MAKER_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TRACKFROM = e.Item.Cells[0].Text.Trim();
					TRACKNEXT = e.Item.Cells[2].Text.Trim();
					TRACKBACK = e.Item.Cells[4].Text.Trim();
					TRACKFAIL = e.Item.Cells[6].Text.Trim();
					LBL_PP_TRACKFROM_OLD.Text = TRACKFROM;
					LBL_PP_TRACKNEXT_OLD.Text = TRACKNEXT;
					LBL_PP_TRACKBACK_OLD.Text = TRACKBACK;
					LBL_PP_TRACKFAIL_OLD.Text = TRACKFAIL;
					try
					{
						DDL_PP_TRACKFROM.SelectedValue = TRACKFROM;
					} 
					catch{}
					if (TRACKNEXT != "" && TRACKNEXT != "&nbsp;")
						DDL_PP_TRACKNEXT.SelectedValue = TRACKNEXT;
					else
						DDL_PP_TRACKNEXT.SelectedIndex = 0;
					if (TRACKBACK != "" && TRACKBACK != "&nbsp;")
						DDL_PP_TRACKBACK.SelectedValue = TRACKBACK;
					else
						DDL_PP_TRACKBACK.SelectedIndex = 0;
					if (TRACKFAIL != "" && TRACKFAIL != "&nbsp;")
						DDL_PP_TRACKFAIL.SelectedValue = TRACKFAIL;
					else
						DDL_PP_TRACKFAIL.SelectedIndex = 0;
					activatePostBackControls(false);
					break;

				case "delete":
					TRACKFROM = e.Item.Cells[0].Text.Trim();
					TRACKNEXT = e.Item.Cells[2].Text.Trim();
					TRACKBACK = e.Item.Cells[4].Text.Trim();
					TRACKFAIL = e.Item.Cells[6].Text.Trim();
					conn.QueryString = "DELETE FROM PENDING_LMS_PROCEDUREPATH where  PP_TRACKFROM = '" + TRACKFROM + "' ";
					conn.ExecuteNonQuery();
					clearEditBoxes();
					ViewRequest();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_CURRENT_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_CURRENT.CurrentPageIndex = e.NewPageIndex;
			ViewExisting();
		}

		private void DGR_MAKER_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_MAKER.CurrentPageIndex = e.NewPageIndex;
			ViewRequest();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../CommonParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}
}
