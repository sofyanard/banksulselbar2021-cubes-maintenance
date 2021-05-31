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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for NasabahGroupInfo.
	/// </summary>
	public partial class RFRateNumberParam : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.WebControls.Button updatestatus;
		protected Tools tool = new Tools();		
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

		//	if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//	Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack) 
			{
				fillCurrency();
				//fillProductID();
				viewExistingData();
				viewPendingData();				
				LBL_SAVEMODE.Text = "1";				

				DDL_CURRENCY.Items.Add(new ListItem("- PILIH -", ""));
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			DGExisting.PageIndexChanged +=new DataGridPageChangedEventHandler(DGExisting_PageIndexChanged);
			
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
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion
		
		private void fillCurrency() 
		{
			conn.QueryString = "select * from RFCURRENCY where ACTIVE = '1'";
			conn.ExecuteQuery();

			DDL_CURRENCY.Items.Add(new ListItem("- PILIH -",""));
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_CURRENCY.Items.Add(new ListItem(conn.GetFieldValue(i,0) + " - " + conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			}
		}		

		private void fillProductID() 
		{
			conn.QueryString = "select * from RFSIBSPRODUCTCODE where ACTIVE = '1'";
			conn.ExecuteQuery();

			DDL_SIBS_PRODCODE.Items.Add(new ListItem("- PILIH -",""));
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_SIBS_PRODCODE.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0).Trim()));
			}
		}

		private void viewPendingData() 
		{
			string tableName = Request.QueryString["tablename"];

			conn.QueryString = "select PENDING_RFRATENUMBER.*, CURRENCYDESC from PENDING_RFRATENUMBER left join RFCURRENCY on CURRENCY = CURRENCYID";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("RATENO"));
			dt.Columns.Add(new DataColumn("CURRENCY"));
			dt.Columns.Add(new DataColumn("CURRENCYDESC"));			
			dt.Columns.Add(new DataColumn("SIBS_PRODCODE"));
			dt.Columns.Add(new DataColumn("RATE"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,6);
				dr[3] = conn.GetFieldValue(i,2);
				dr[4] = tool.ConvertFloat(conn.GetFieldValue(i,3));
				dr[5] = conn.GetFieldValue(i,5);
				dr[6] = getPendingStatus(conn.GetFieldValue(i,5));				
				dt.Rows.Add(dr);
			}			

			DGRequest.DataSource = new DataView(dt);
			try 
			{
				DGRequest.DataBind();
			}
			catch {
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

		private void viewExistingData() 
		{		
			conn.QueryString = "select RFRATENUMBER.*, CURRENCYDESC from RFRATENUMBER left join RFCURRENCY on CURRENCY = CURRENCYID where RFRATENUMBER.ACTIVE = '1'";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("RATENO"));
			dt.Columns.Add(new DataColumn("CURRENCY"));
			dt.Columns.Add(new DataColumn("CURRENCYDESC"));
			dt.Columns.Add(new DataColumn("SIBS_PRODCODE"));
			dt.Columns.Add(new DataColumn("RATE"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,5);
				dr[3] = conn.GetFieldValue(i,2);
				dr[4] = tool.ConvertFloat(conn.GetFieldValue(i,3));
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
		}

		private void DGExisting_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{			
			DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			//if (TXT_RATENO.Text.Trim() == "" || TXT_RATE.Text == "") return;	//--- ver 1.0
			if (TXT_RATENO.Text.Trim() == "" || 
				TXT_RATE.Text == "" || 
				TXT_SIBS_PRODCODE.Text == "") return;
			
			//--- Periksa RATE masukan
			//    Rate harus < 1
			if (Convert.ToDouble(TXT_RATE.Text.Replace(".",",")) >= 1)
			{
				Tools.popMessage(this, "Rate harus lebih kecil dari 1 !");
				return;
			}


			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				//--- ver 1.0
                conn.QueryString = "select * from RFRATENUMBER where RATENO = '" + TXT_RATENO.Text.Trim() + "' AND CURRENCY = '" + DDL_CURRENCY.SelectedValue + "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		
			
			try 
			{
				//TXT_RATE.Text = Convert.ToDecimal(TXT_RATE.Text).ToString();
				TXT_RATE.Text = tool.ConvertFloat(TXT_RATE.Text.Replace(".",","));
			}
			catch 
			{
				Tools.popMessage(this, "Rate must be floating-number!");
				return;
			}				

			/*** ver 1.0
			conn.QueryString = "exec PARAM_GENERAL_RFRATENUMBER_MAKER " +
								"'" + LBL_SAVEMODE.Text.Trim() + "', " +
								"'" + TXT_RATENO.Text.Trim() + "', " +
								"'" + DDL_CURRENCY.SelectedValue + "', " +
								"'" + DDL_SIBS_PRODCODE.SelectedItem.Text + "', " +
								"'" + TXT_RATE.Text + "'";
			****/
			conn.QueryString = "exec PARAM_GENERAL_RFRATENUMBER_MAKER " +
				"'" + LBL_SAVEMODE.Text.Trim() + "', " +
				"'" + TXT_RATENO.Text.Trim() + "', " +
				"'" + DDL_CURRENCY.SelectedValue + "', " +
				"'" + TXT_SIBS_PRODCODE.Text.Trim() + "', " +
				"'" + TXT_RATE.Text + "'";
			conn.ExecuteNonQuery();
			viewPendingData();
			clearControls();

			LBL_SAVEMODE.Text = "1";
		}

		private void clearControls() 
		{
			TXT_RATENO.Text   = "";
			TXT_RATE.Text = "";			
			DDL_CURRENCY.SelectedValue = "";
			DDL_SIBS_PRODCODE.SelectedValue = "";
			TXT_SIBS_PRODCODE.Text = "";
			activateControlKey(false);
		}

		private void activateControlKey(bool isReadOnly) 
		{
			TXT_RATENO.ReadOnly = isReadOnly;
			DDL_CURRENCY.Enabled = !isReadOnly;
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[5].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_RATENO.Text = e.Item.Cells[0].Text;
					DDL_CURRENCY.SelectedValue = e.Item.Cells[1].Text;					
					TXT_RATE.Text = e.Item.Cells[4].Text;
					conn.QueryString = "select * from RFSIBSPRODUCTCODE where ACTIVE = '1' and SIBS_PRODCODE = '" +e.Item.Cells[3].Text+ "'";
					conn.ExecuteQuery();
					//DDL_SIBS_PRODCODE.SelectedValue = conn.GetFieldValue("SEQ");
					TXT_SIBS_PRODCODE.Text = conn.GetFieldValue("SIBS_PRODCODE");
					activateControlKey(true);
					break;

				case "delete":					
					string rateno, currency;
					rateno = e.Item.Cells[0].Text;
					currency = e.Item.Cells[1].Text;
					conn.QueryString = "delete from PENDING_RFRATENUMBER where RATENO = '" + rateno + "' and CURRENCY = '" + currency + "'";
					conn.ExecuteQuery();
					viewPendingData();
					break;
				default :
					break;
			}
		}

		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_RATENO.Text = e.Item.Cells[0].Text;
					DDL_CURRENCY.SelectedValue = e.Item.Cells[1].Text;					
					TXT_RATE.Text = e.Item.Cells[4].Text;
					conn.QueryString = "select * from RFSIBSPRODUCTCODE where ACTIVE = '1' and SIBS_PRODCODE = '" +e.Item.Cells[3].Text+ "'";
					conn.ExecuteQuery();
					//DDL_SIBS_PRODCODE.SelectedValue = conn.GetFieldValue("SEQ");
					TXT_SIBS_PRODCODE.Text = conn.GetFieldValue("SIBS_PRODCODE");
					activateControlKey(true);
					break;

				case "delete":					
					string rateno = e.Item.Cells[0].Text.Trim();
					string currency = e.Item.Cells[1].Text;
					string sibs_prodcode = e.Item.Cells[3].Text;
					string rate = e.Item.Cells[4].Text;

					conn.QueryString = "exec PARAM_GENERAL_RFRATENUMBER_MAKER " + 
										"'2'," + 
										"'" + rateno + "'," + 
										"'" + currency + "'," + 
										"'" + sibs_prodcode + "'," + 
										"'" + rate + "'";
					conn.ExecuteQuery();
					viewPendingData();
					break;

				default :
					break;
			}
		}

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		protected void TXT_RATE_TextChanged(object sender, System.EventArgs e)
		{
			//TXT_RATE.Text = tool.ConvertNum(TXT_RATE.Text);
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../HostParam.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		protected void DDL_SIBS_PRODCODE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (DDL_SIBS_PRODCODE.SelectedValue != "") 
			{
				DDL_CURRENCY.Enabled = true;
				conn.QueryString = "select distinct SIBSPRODDESC, CURRENCY, CURRENCYDESC " + 
					"from RFSIBSPRODID left join RFCURRENCY on CURRENCYID = CURRENCY " + 
					"where SIBSPRODDESC = '"+DDL_SIBS_PRODCODE.SelectedItem.Text.Trim()+"' " + 
					"order by SIBSPRODDESC";
				conn.ExecuteQuery();
				
				DDL_CURRENCY.Items.Clear();
				DDL_CURRENCY.Items.Add(new ListItem("- PILIH -", ""));
				for(int i=0; i<conn.GetRowCount(); i++) 
				{
					//new ListItem(conn.GetFieldValue(i,0) + " - " + conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
					//DDL_CURRENCY.Items.Add(new ListItem(conn.GetFieldValue(i,2), conn.GetFieldValue(i,1)));
					DDL_CURRENCY.Items.Add(new ListItem(conn.GetFieldValue(i,1) + " - " + conn.GetFieldValue(i,2), conn.GetFieldValue(i,1)));
				}
			}
			else 
			{
				DDL_CURRENCY.Items.Clear();
				DDL_CURRENCY.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CURRENCY.Enabled = false;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();
			LBL_SAVEMODE.Text = "1";
		}
	}		
}
