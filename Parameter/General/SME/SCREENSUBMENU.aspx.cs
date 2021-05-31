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
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for SCREENSUBMENU.
	/// </summary>
	public partial class SCREENSUBMENU : System.Web.UI.Page
	{
	
		private Connection conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if (!IsPostBack) 
			{
				fillDropDownList();
				viewDataPending();
			}
		}

		private void viewDataCurrent(string _programID) 
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_SCREENSUBMENU where PROGRAMID = '" + _programID + "'";
			conn.ExecuteQuery();

			DGR_CURRENT.DataSource = conn.GetDataTable().DefaultView;
			try  
			{ 
				DGR_CURRENT.DataBind();
			} 
			catch 
			{
				DGR_CURRENT.CurrentPageIndex = 0;
				DGR_CURRENT.DataBind();
			}
		}

		private void viewDataPending() 
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_PENDING_SCREENSUBMENU";
			conn.ExecuteQuery();

			DGR_PENDING.DataSource = conn.GetDataTable().DefaultView;
			try { DGR_PENDING.DataBind(); } 
			catch 
			{
				DGR_PENDING.CurrentPageIndex = 0;
				DGR_PENDING.DataBind();
			}
		}

		private void resetStatus() 
		{
			DDL_MENUCODE.SelectedValue = "";
			DDL_MENUCODE.Enabled = true;

			DDL_PROGRAMID.SelectedValue = "";
			DDL_PROGRAMID.Enabled = true;

			LBL_SAVEMODE.Text = "1";
		}

		private void fillDropDownList() 
		{
			/// program
			/// 
			GlobalTools.fillRefList(DDL_PROGRAMID, "select programid, programdesc from rfprogram where areaid = '0000' and active = '1'  order by programdesc", false, conn);

			/// Menu
			/// 
			GlobalTools.fillRefList(DDL_MENUCODE, "select * from vw_rfmenu", false, conn);
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
			this.DGR_PENDING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_ItemCommand);
			this.DGR_PENDING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_PENDING_PageIndexChanged);

		}
		#endregion

		protected void BTN_VIEW_Click(object sender, System.EventArgs e)
		{
			viewDataCurrent(DDL_PROGRAMID.SelectedValue);
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string _isprogbpr = "";
			_isprogbpr = (CHK_PROG_BPR.Checked) ? "true" : "false";

			conn.QueryString = "exec PARAM_GENERAL_SCREENSUBMENU_PENDING 'insert', '" + 
				DDL_PROGRAMID.SelectedValue + "', '" + 
				DDL_MENUCODE.SelectedValue + "', '" + 
				_isprogbpr + "', '1'";
			conn.ExecuteNonQuery();

			resetStatus();
			viewDataPending();
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			resetStatus();
		}

		private void DGR_CURRENT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			resetStatus();

			switch (e.CommandName) 
			{
				/*
				case "edit":
					LBL_SAVEMODE.Text = "0";

					DDL_PROGRAMID.Enabled = false;
					try { DDL_PROGRAMID.SelectedValue = e.Item.Cells[0].Text; } 
					catch { DDL_PROGRAMID.Enabled = true; }
					try { DDL_FAIRISAACID.SelectedValue = e.Item.Cells[2].Text; } 
					catch { DDL_FAIRISAACID.Enabled = true; }
					try { RDO_CONSTRAINT.SelectedValue = e.Item.Cells[4].Text; } 
					catch {}
					break;
				*/

				case "delete":
					LBL_SAVEMODE.Text = "1";
					string _isprogbpr = (CHK_PROG_BPR.Checked) ? "true" : "false";

					conn.QueryString = "PARAM_GENERAL_SCREENSUBMENU_PENDING 'insert', '" + 
						e.Item.Cells[2].Text + "', '" + 
						e.Item.Cells[0].Text + "', '" + 
						_isprogbpr + "', '2'";
					conn.ExecuteNonQuery();
					viewDataPending();
					break;

				default :
					// do nothing euy ....
					break;
			}
		}

		private void DGR_CURRENT_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_CURRENT.CurrentPageIndex = e.NewPageIndex;
			viewDataCurrent(DDL_PROGRAMID.SelectedValue);
		}

		private void Datagrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			resetStatus();

			switch (e.CommandName) 
			{
				/*
				case "edit" :
					LBL_SAVEMODE.Text = "0";

					DDL_PROGRAMID.Enabled = false;
					try { DDL_PROGRAMID.SelectedValue = e.Item.Cells[0].Text; } 
					catch { DDL_PROGRAMID.Enabled = true; }
					try { DDL_FAIRISAACID.SelectedValue = e.Item.Cells[2].Text; } 
					catch { DDL_FAIRISAACID.Enabled = true; }
					try { RDO_CONSTRAINT.SelectedValue = e.Item.Cells[4].Text; } 
					catch {}
					break;
				*/

				case "delete" :
					//LBL_SAVEMODE.Text = "2";
					string _isprogbpr = (CHK_PROG_BPR.Checked) ? "true" : "false";

					conn.QueryString = "PARAM_GENERAL_SCREENSUBMENU_PENDING 'delete', '" + 
						e.Item.Cells[2].Text + "', '" + 
						e.Item.Cells[0].Text + "', '" + 
						_isprogbpr + "', '2'";
					conn.ExecuteNonQuery();
					viewDataPending();
					break;

				default :
					break;
			}
		}

		private void DGR_PENDING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_PENDING.CurrentPageIndex = e.NewPageIndex;
			viewDataPending();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}
	}
}
