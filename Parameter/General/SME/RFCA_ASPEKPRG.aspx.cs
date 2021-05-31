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
	/// Summary description for RFCA_ASPEKPRG.
	/// </summary>
	public partial class RFCA_ASPEKPRG : System.Web.UI.Page
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

			BTN_SAVE.Attributes.Add("onclick", "if(!cek_mandatory(document.Form1)){return false;}");
		}

		private void fillDropDownList() 
		{
			/// program
			/// 
			GlobalTools.fillRefList(DDL_PROGRAMID, "select distinct programid, programdesc from rfprogram where active = '1'  order by programdesc", false, conn);

			/// aspek
			/// 
			GlobalTools.fillRefList(DDL_CA_ASPEK, "select aspekid, aspekdesc from rfca_aspek where active = '1' order by aspekdesc", false, conn);
		}

		private void viewDataCurrent(string _programID) 
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_RFCA_ASPEKPRG where programid = '" + _programID + "'";
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
			conn.QueryString = "select * from VW_PARAM_GENERAL_PENDING_RFCA_ASPEKPRG";
			conn.ExecuteQuery();

			DGR_PENDING.DataSource = conn.GetDataTable().DefaultView;
			try 
			{
				DGR_PENDING.DataBind();
			}
			catch 
			{
				DGR_PENDING.CurrentPageIndex = 0;
				DGR_PENDING.DataBind();
			}
		}

		private void resetStatus() 
		{
			DDL_PROGRAMID.SelectedValue = "";
			DDL_CA_ASPEK.SelectedValue = "";
			DDL_PROGRAMID.Enabled = true;
			LBL_SAVEMODE.Text = "1";
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
			this.DGR_PENDING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_PENDING_ItemCommand);
			this.DGR_PENDING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_PENDING_PageIndexChanged);

		}
		#endregion

		protected void BTN_VIEW_Click(object sender, System.EventArgs e)
		{
			viewDataCurrent(DDL_PROGRAMID.SelectedValue);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			resetStatus();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "exec PARAM_GENERAL_RFCA_ASPEKPRG_MAKER '" + 
				LBL_SAVEMODE.Text + "', '" + 
				DDL_PROGRAMID.SelectedValue + "', '" + 
				DDL_CA_ASPEK.SelectedValue + "'";
			conn.ExecuteNonQuery();

			resetStatus();
			viewDataPending();
		}

		private void DGR_CURRENT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			resetStatus();

			switch (e.CommandName) 
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";

					DDL_PROGRAMID.Enabled = false;
					try { DDL_PROGRAMID.SelectedValue = e.Item.Cells[0].Text; } 
					catch { DDL_PROGRAMID.Enabled = true; }
					try { DDL_CA_ASPEK.SelectedValue = e.Item.Cells[2].Text; } 
					catch {}
					break;

				case "delete":
					LBL_SAVEMODE.Text = "1";

					conn.QueryString = "insert into PENDING_RFCA_ASPEKPRG (programid, aspekid, pendingstatus) " + 
						" values ('" + e.Item.Cells[0].Text + "', '" + e.Item.Cells[2].Text + "', '2')";
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
			try { DGR_CURRENT.CurrentPageIndex = e.NewPageIndex; } 
			catch { DGR_CURRENT.CurrentPageIndex = 0; }
			viewDataCurrent(DDL_PROGRAMID.SelectedValue);
		}

		private void DGR_PENDING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			resetStatus();

			switch (e.CommandName) 
			{
				case "edit" :
					LBL_SAVEMODE.Text = "0";

					DDL_PROGRAMID.Enabled = false;
					try { DDL_PROGRAMID.SelectedValue = e.Item.Cells[0].Text; } 
					catch { DDL_PROGRAMID.Enabled = true; }
					try { DDL_CA_ASPEK.SelectedValue = e.Item.Cells[2].Text; } 
					catch {}
					break;

				case "delete" :
					//LBL_SAVEMODE.Text = "2";

					conn.QueryString = "delete from PENDING_RFCA_ASPEKPRG where programid = '" + e.Item.Cells[0].Text + "' and productid = '" + e.Item.Cells[2].Text + "'";
					conn.ExecuteNonQuery();
					viewDataPending();
					break;

				default :
					break;
			}
		}

		private void DGR_PENDING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try { DGR_PENDING.CurrentPageIndex = e.NewPageIndex; } 
			catch { DGR_PENDING.CurrentPageIndex = 0; }
			viewDataPending();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}
	}
}
