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
	/// Summary description for RFInsuranceCompany.
	/// </summary>
	public partial class RFInsuranceCompany : System.Web.UI.Page
	{
		
		protected Connection conn;
		string IC_ID;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

			
			if (!IsPostBack)
			{
				ViewCurrent();
				ViewMaker();
			}
			DGR_CURRENT.PageIndexChanged += new DataGridPageChangedEventHandler(this.CH_DGR_CURRENT);
			DGR_MAKER.PageIndexChanged += new DataGridPageChangedEventHandler(this.CH_DGR_MAKER);
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
			this.DGR_CURRENT.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.CH_DGR_CURRENT);
			this.DGR_MAKER.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_MAKER_ItemCommand);
			this.DGR_MAKER.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.CH_DGR_MAKER);

		}
		#endregion

		private void ViewCurrent()
		{
			conn.QueryString = "select IC_ID, IC_DESC "+
				", isnull(IC_ADDR1, '') +' '+ isnull(IC_ADDR2, '') +' '+ isnull(IC_ADDR3, '') IC_ADDR "+
				", IC_ADDR1, IC_ADDR2, IC_ADDR3, IC_CITY, IC_ZIPCODE, IC_CONTACT, isnull(ACTIVE, '0') ACTIVE "+				
				"from RFINSURANCECOMPANY " ;
				//"where ACTIVE = '1' ";  -- semua data ditampilin
			conn.ExecuteQuery();
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_CURRENT.DataSource = data;
			DGR_CURRENT.DataBind();

			
			for (int i=0; i< DGR_CURRENT.Items.Count;i++)
			{

				if (DGR_CURRENT.Items[i].Cells[9].Text.Trim() =="0" ) 
				{		// active = 0					
					LinkButton l_del = (LinkButton) DGR_CURRENT.Items[i].FindControl("lnk_RfDelete");
					l_del.CommandName = "Undelete";
					l_del.Text = "Undelete";

					LinkButton l_edit = (LinkButton) DGR_CURRENT.Items[i].FindControl("lnk_RfEdit");
					l_edit.Visible = false;
				}
			}


		}

		private void ViewMaker()
		{
			conn.QueryString = "select IC_ID, IC_DESC "+
				", isnull(IC_ADDR1, '') +' '+ isnull(IC_ADDR2, '') +' '+ isnull(IC_ADDR3, '') IC_ADDR "+
				", IC_ADDR1, IC_ADDR2, IC_ADDR3, IC_CITY, IC_ZIPCODE, IC_CONTACT "+
				", isnull(ACTIVE, '0') ACTIVE, PENDINGSTATUS "+
				", case when PENDINGSTATUS = '0' then 'Update' when PENDINGSTATUS = '2' then 'Delete' "+
				" else 'Insert' end PENDINGDESC  "+
				"from PENDING_RFINSURANCECOMPANY "+
				"order by PENDINGSTATUS ";
			conn.ExecuteQuery();
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_MAKER.DataSource = data;
			DGR_MAKER.DataBind();
		}
		
		private void CH_DGR_CURRENT(Object sender, DataGridPageChangedEventArgs e) 
		{
			DGR_CURRENT.CurrentPageIndex = e.NewPageIndex;
			ViewCurrent();
		}

		private void CH_DGR_MAKER(Object sender, DataGridPageChangedEventArgs e) 
		{
			DGR_MAKER.CurrentPageIndex = e.NewPageIndex;
			ViewMaker();
		}

		private void clearEditBoxes()
		{
			TXT_IC_ID.Text = "";
			TXT_IC_DESC.Text = "";
			TXT_IC_ADDR1.Text = "";
			TXT_IC_ADDR2.Text = "";
			TXT_IC_ADDR3.Text = "";
			TXT_IC_CITY.Text = "";
			TXT_IC_ZIPCODE.Text = "";
			TXT_IC_CONTACT.Text = "";
		}

		private void activatePostBackControls(bool mode)
		{
			TXT_IC_ID.Enabled = mode;
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "exec PARAM_GENERAL_RFINSURANCECOMPANY_MAKER '"+ LBL_SAVEMODE.Text.Trim() + "', '" +
				TXT_IC_ID.Text.Trim() + "', '" + TXT_IC_DESC.Text.Trim() + "', '" + 
				TXT_IC_ADDR1.Text.Trim() + "', '" + TXT_IC_ADDR2.Text.Trim() + "', '" +
				TXT_IC_ADDR3.Text.Trim() + "', '" + TXT_IC_CITY.Text.Trim() + "', '" + TXT_IC_ZIPCODE.Text.Trim() + "', '" +
				TXT_IC_CONTACT.Text.Trim() + "' ";
			conn.ExecuteQuery();
			ViewMaker();
			clearEditBoxes();
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			activatePostBackControls(true);
			clearEditBoxes();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void DGR_CURRENT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//int i;
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_IC_ID.Text = cleansText(e.Item.Cells[0].Text);
					TXT_IC_DESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_IC_ADDR1.Text = cleansText(e.Item.Cells[3].Text);
					TXT_IC_ADDR2.Text = cleansText(e.Item.Cells[4].Text);
					TXT_IC_ADDR3.Text = cleansText(e.Item.Cells[5].Text);
					TXT_IC_CITY.Text = cleansText(e.Item.Cells[6].Text);
					TXT_IC_ZIPCODE.Text = cleansText(e.Item.Cells[7].Text);
					TXT_IC_CONTACT.Text = cleansText(e.Item.Cells[8].Text);
					activatePostBackControls(false);
					break;

				case "delete":
					IC_ID = cleansText(e.Item.Cells[0].Text);
					conn.QueryString = "PARAM_GENERAL_RFINSURANCECOMPANY_MAKER '2', '"+ IC_ID + "', '"+
						cleansText(e.Item.Cells[1].Text) +"', '"+ cleansText(e.Item.Cells[3].Text) +"', '"+
						cleansText(e.Item.Cells[4].Text) +"', '"+ cleansText(e.Item.Cells[5].Text) +"', '"+
						cleansText(e.Item.Cells[6].Text) +"', '"+ cleansText(e.Item.Cells[7].Text) +"', '"+
						cleansText(e.Item.Cells[8].Text) +"', null ";
					conn.ExecuteQuery();
					activatePostBackControls(true);
					ViewMaker();
					break;

				case "Undelete":
					IC_ID = cleansText(e.Item.Cells[0].Text);
					conn.QueryString = "PARAM_GENERAL_RFINSURANCECOMPANY_MAKER '0', '"+ IC_ID + "', '"+
						cleansText(e.Item.Cells[1].Text) +"', '"+ cleansText(e.Item.Cells[3].Text) +"', '"+
						cleansText(e.Item.Cells[4].Text) +"', '"+ cleansText(e.Item.Cells[5].Text) +"', '"+
						cleansText(e.Item.Cells[6].Text) +"', '"+ cleansText(e.Item.Cells[7].Text) +"', '"+
						cleansText(e.Item.Cells[8].Text) +"', '1' ";
					conn.ExecuteQuery();
					activatePostBackControls(true);
					ViewMaker();
					break;

				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_MAKER_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//int i;
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[9].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_IC_ID.Text = cleansText(e.Item.Cells[0].Text);
					TXT_IC_DESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_IC_ADDR1.Text = cleansText(e.Item.Cells[3].Text);
					TXT_IC_ADDR2.Text = cleansText(e.Item.Cells[4].Text);
					TXT_IC_ADDR3.Text = cleansText(e.Item.Cells[5].Text);
					TXT_IC_CITY.Text = cleansText(e.Item.Cells[6].Text);
					TXT_IC_ZIPCODE.Text = cleansText(e.Item.Cells[7].Text);
					TXT_IC_CONTACT.Text = cleansText(e.Item.Cells[8].Text);
					activatePostBackControls(false);
					break;
				case "delete":
					IC_ID = e.Item.Cells[0].Text.Trim();
					conn.QueryString = "DELETE FROM PENDING_RFINSURANCECOMPANY WHERE IC_ID = '"+ IC_ID + "' ";
					conn.ExecuteQuery();
					activatePostBackControls(true);
					ViewMaker();
					break;
				default:
					// Do nothing.
					break;
			}
		}
	}
}
