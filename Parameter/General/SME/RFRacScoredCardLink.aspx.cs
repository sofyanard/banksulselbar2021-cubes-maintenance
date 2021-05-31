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
	/// Summary description for RFRacScoredCardLink.
	/// </summary>
	public partial class RFRacScoredCardLink : System.Web.UI.Page
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
			GlobalTools.fillRefList(DDL_PROGRAMID, "select loanpurpid, loanpurpdesc from rfloanpurpose where active='1'  order by loanpurpid", true, conn);

			/// aspek
			/// 
			GlobalTools.fillRefList(DDL_CA_ASPEK, "select productid, productdesc from rfproduct where active='1' order by productid", true, conn);

			///rca
			///
			GlobalTools.fillRefList(DDL_RCA, "select rcaid, rcadesc from rfrca where active='1' order by rcaid", true, conn);

            ///deviasi action
            ///
            GlobalTools.fillRefList(DDL_DEVIASI_ACTION, "SELECT * FROM VW_RFDEVIASI_ACTION", true, conn);

            ///deviasi route
            ///
            GlobalTools.fillRefList(DDL_DEVIASI_ROUTE, "SELECT * FROM VW_RFDEVIASI_ROUTE", true, conn);
		}

		private void viewDataCurrent(string _programID,string _productID) 
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_CURRENT_RFRCALINK where loanpurpid = '" + _programID + "' and productid = '" + _productID + "' ";
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
			conn.QueryString = "select * from VW_PARAM_GENERAL_PENDING_RFRCALINK";
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
			DDL_RCA.SelectedValue = "";
			DDL_PROGRAMID.Enabled = true;
			LBL_SAVEMODE.Text = "1";
            CHK_DEVIASI_FLAG.Checked = false;
            DDL_DEVIASI_ACTION.Visible = true;
            DDL_DEVIASI_ACTION.SelectedValue = "";
            DDL_DEVIASI_ROUTE.Enabled = true;
            DDL_DEVIASI_ROUTE.SelectedValue = "";
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
			viewDataCurrent(DDL_PROGRAMID.SelectedValue,DDL_CA_ASPEK.SelectedValue);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			resetStatus();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{

			int res = 1;
			if (res <= this.DGR_PENDING.Items.Count)
			{
				res = this.DGR_PENDING.Items.Count + 1;
			}
			this.LBL_SEQ_ID.Text = res.ToString();

            string deviasi_flag;
            if (CHK_DEVIASI_FLAG.Checked == true)
            {
                deviasi_flag = "1";
            }
            else
            {
                deviasi_flag = "";
            }
											
			conn.QueryString = "EXEC PARAM_GENERAL_RFRCALINK_LINK_MAKER '" +
                LBL_SAVEMODE.Text.Trim() + "', '" +
				this.DDL_PROGRAMID.SelectedValue.ToString() + "', '" +
				this.DDL_CA_ASPEK.SelectedValue.ToString() + "', '" +
				this.DDL_RCA.SelectedValue.ToString() + "', '" +
                this.LBL_SEQ_ID.Text + "', '" +
                deviasi_flag + "', '" +
                this.DDL_DEVIASI_ACTION.SelectedValue.ToString() + "','" +
                this.DDL_DEVIASI_ROUTE.SelectedValue.ToString() + "'";
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

                    conn.QueryString = "insert into PENDING_RFRCALINK (LOANPURPID, PRODUCTID, RCAID, PENDINGSTATUS, DEVIASI_FLAG, DEVIASI_ACTION, DEVIASI_ROUTE) " + 
						" values ('" + e.Item.Cells[0].Text + "', '" + e.Item.Cells[2].Text + "', '" + e.Item.Cells[4].Text + "', '2', '" + e.Item.Cells[6].Text + "', '" + e.Item.Cells[8].Text + "', '" + e.Item.Cells[10].Text + "')";
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
			viewDataCurrent(DDL_PROGRAMID.SelectedValue,DDL_CA_ASPEK.SelectedValue);
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

					conn.QueryString = "delete from PENDING_RFRCALINK where loanpurpid = '" + e.Item.Cells[0].Text + 
						"' and productid = '" + e.Item.Cells[2].Text + "' and rcaid = '" + e.Item.Cells[4].Text + "'";
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

        protected void CHK_DEVIASI_FLAG_CheckedChanged(object sender, System.EventArgs e)
        {
            if (CHK_DEVIASI_FLAG.Checked == true)
            {
                DDL_DEVIASI_ACTION.Visible = true;
                DDL_DEVIASI_ACTION.CssClass = "mandatory";
                DDL_DEVIASI_ACTION.SelectedValue = "";

                DDL_DEVIASI_ROUTE.Visible = false;
                DDL_DEVIASI_ROUTE.CssClass = "";
                DDL_DEVIASI_ROUTE.SelectedValue = "";
            }
            else
            {
                DDL_DEVIASI_ACTION.Visible = false;
                DDL_DEVIASI_ACTION.CssClass = "";
                DDL_DEVIASI_ACTION.SelectedValue = "";

                DDL_DEVIASI_ROUTE.Visible = false;
                DDL_DEVIASI_ROUTE.CssClass = "";
                DDL_DEVIASI_ROUTE.SelectedValue = "";
            }
        }

        protected void DDL_DEVIASI_ACTION_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (DDL_DEVIASI_ACTION.SelectedValue == "DEV01") //Reject
            {
                DDL_DEVIASI_ROUTE.Visible = false;
                DDL_DEVIASI_ROUTE.CssClass = "";
                DDL_DEVIASI_ROUTE.SelectedValue = "";
            }
            else if (DDL_DEVIASI_ACTION.SelectedValue == "DEV02") //OLA
            {
                DDL_DEVIASI_ROUTE.Visible = false;
                DDL_DEVIASI_ROUTE.CssClass = "";
                DDL_DEVIASI_ROUTE.SelectedValue = "";
            }
            else if (DDL_DEVIASI_ACTION.SelectedValue == "DEV03") //Proceed To
            {
                DDL_DEVIASI_ROUTE.Visible = true;
                DDL_DEVIASI_ROUTE.CssClass = "mandatory";
                DDL_DEVIASI_ROUTE.SelectedValue = "";
            }
            else
            {
                DDL_DEVIASI_ROUTE.Visible = false;
                DDL_DEVIASI_ROUTE.CssClass = "";
                DDL_DEVIASI_ROUTE.SelectedValue = "";
            }
        }
	}
}
