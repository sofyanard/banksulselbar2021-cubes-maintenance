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

namespace SME.Maintenance.Parameters.General
{
	/// <summary>
	/// Summary description for RFBRANCH.
	/// </summary>
	public partial class RFBRANCH : System.Web.UI.Page
	{
	
		protected Connection conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);//(Connection) Session["Connection"];
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			//if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//	Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack)
			{
				//CITY 
				conn.QueryString = "select cityid, cityname from rfcity where active = '1' ";
				conn.ExecuteQuery();
				DDL_CITYID.Items.Clear();
				DDL_CITYID.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_CITYID.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				bindData1();
				bindData2();
			}
			Datagrid1.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change1);
			DataGrid2.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change2);
		}

		private void bindData1()
		{
			conn.QueryString = "SELECT * FROM RFBRANCH LEFT JOIN RFCITY ON RFCITY.CITYID = RFBRANCH.CITYID ";
			conn.ExecuteQuery();
			Datagrid1.DataSource = conn.GetDataTable().Copy();
			try 
			{
				Datagrid1.DataBind();
			}
			catch 
			{
				Datagrid1.CurrentPageIndex = Datagrid1.PageCount - 1;
				Datagrid1.DataBind();
			}
		}

		private void bindData2()
		{
			conn.QueryString = "SELECT * , 'UNDEFINED' STATUSDESC FROM PENDING_RFBRANCH LEFT JOIN RFCITY " +
				"ON RFCITY.CITYID = PENDING_RFBRANCH.CITYID ";
			conn.ExecuteQuery();
			DataGrid2.DataSource = conn.GetDataTable().Copy();
			try 
			{
				DataGrid2.DataBind();
			}
			catch 
			{
				DataGrid2.CurrentPageIndex = DataGrid2.PageCount - 1;
				DataGrid2.DataBind();
			}
			for (int i = 0; i < DataGrid2.Items.Count; i++)
			{
				if (DataGrid2.Items[i].Cells[11].Text.Trim() == "0")
				{
					DataGrid2.Items[i].Cells[10].Text = "UPDATE";
				}
				else if (DataGrid2.Items[i].Cells[11].Text.Trim() == "1")
				{
					DataGrid2.Items[i].Cells[10].Text = "INSERT";
				}
				else if (DataGrid2.Items[i].Cells[11].Text.Trim() == "2")
				{
					DataGrid2.Items[i].Cells[10].Text = "DELETE";
				}
			}
		}

		private void clearEditBoxes()
		{
			TXT_BRANCH_CODE.Text = "";
			TXT_BRANCH_NAME.Text = "";
			TXT_CBC_CODE.Text = "";
			try
			{
				DDL_CITYID.SelectedIndex = 0;
			} 
			catch {}
			TXT_BR_ZIPCODE.Text = "";
			TXT_BR_ADDR.Text = "";
			TXT_BR_BRANCHAREA.Text = "";
			TXT_BR_PHNFAX.Text = "";
			TXT_BR_CCOBRANCH.Text = "";
			LBL_SAVEMODE.Text = "1";
			activatePostBackControls(true);
		}

		private void activatePostBackControls(bool mode)
		{
			TXT_BRANCH_CODE.Enabled = mode;
		}

		private void cleansTextBox (TextBox tb)
		{
			if (tb.Text.Trim() == "&nbsp;")
				tb.Text = "";
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
			this.Datagrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_ItemCommand);
			this.DataGrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_ItemCommand);

		}
		#endregion

		void Grid_Change1(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid1.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData1();	
		}

		void Grid_Change2(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DataGrid2.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData2();	
		}

		private void Datagrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_BRANCH_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_BRANCH_NAME.Text = e.Item.Cells[1].Text.Trim();
					TXT_CBC_CODE.Text = e.Item.Cells[2].Text.Trim();
					try
					{
						DDL_CITYID.SelectedValue = e.Item.Cells[9].Text.Trim();
					} 
					catch {}
					TXT_BR_ZIPCODE.Text = e.Item.Cells[4].Text.Trim();
					TXT_BR_ADDR.Text = e.Item.Cells[5].Text.Trim();
					TXT_BR_BRANCHAREA.Text = e.Item.Cells[6].Text.Trim();
					TXT_BR_PHNFAX.Text = e.Item.Cells[7].Text.Trim();
					TXT_BR_CCOBRANCH.Text = e.Item.Cells[8].Text.Trim();
					cleansTextBox(TXT_BRANCH_CODE);
					cleansTextBox(TXT_BRANCH_NAME);
					cleansTextBox(TXT_CBC_CODE);
					cleansTextBox(TXT_BR_ZIPCODE);
					cleansTextBox(TXT_BR_ADDR);
					cleansTextBox(TXT_BR_BRANCHAREA);
					cleansTextBox(TXT_BR_PHNFAX);
					cleansTextBox(TXT_BR_CCOBRANCH);
					activatePostBackControls(false);
					break;
				case "delete":
					string id = e.Item.Cells[0].Text.Trim();
					LBL_SAVEMODE.Text = "2";
					conn.QueryString = "PARAM_GENERAL_RFBRANCH_MAKER '"+ LBL_SAVEMODE.Text.Trim() + "', '" +
						e.Item.Cells[0].Text.Trim() + "', '" + e.Item.Cells[1].Text.Trim() + "', '" + 
						e.Item.Cells[2].Text.Trim() + "', '" + e.Item.Cells[9].Text.Trim() +
						"', '" + e.Item.Cells[4].Text.Trim() + "', '" + e.Item.Cells[5].Text.Trim() + "', '" +
						e.Item.Cells[6].Text.Trim() + "', '" + e.Item.Cells[7].Text.Trim() +
						"', null, null, null, null, '1', '" + e.Item.Cells[8].Text.Trim() + "' ";
					//conn.QueryString = "PARAM_GENERAL_RFBRANCH_MAKER '2', '"+ id + "' ";
					conn.ExecuteQuery();
					bindData2();
					//Tools.popMessage(this, "Data added for deletion!");
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DataGrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[11].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_BRANCH_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_BRANCH_NAME.Text = e.Item.Cells[1].Text.Trim();
					TXT_CBC_CODE.Text = e.Item.Cells[2].Text.Trim();
					try
					{
						DDL_CITYID.SelectedValue = e.Item.Cells[9].Text.Trim();
					} 
					catch {}
					TXT_BR_ZIPCODE.Text = e.Item.Cells[4].Text.Trim();
					TXT_BR_ADDR.Text = e.Item.Cells[5].Text.Trim();
					TXT_BR_BRANCHAREA.Text = e.Item.Cells[6].Text.Trim();
					TXT_BR_PHNFAX.Text = e.Item.Cells[7].Text.Trim();
					TXT_BR_CCOBRANCH.Text = e.Item.Cells[8].Text.Trim();
					cleansTextBox(TXT_BRANCH_CODE);
					cleansTextBox(TXT_BRANCH_NAME);
					cleansTextBox(TXT_CBC_CODE);
					cleansTextBox(TXT_BR_ZIPCODE);
					cleansTextBox(TXT_BR_ADDR);
					cleansTextBox(TXT_BR_BRANCHAREA);
					cleansTextBox(TXT_BR_PHNFAX);
					cleansTextBox(TXT_BR_CCOBRANCH);
					activatePostBackControls(false);
					break;
				case "delete":
					string BRANCH_CODE = e.Item.Cells[0].Text.Trim();
					conn.QueryString = "DELETE FROM PENDING_RFBRANCH WHERE BRANCH_CODE = '"+ BRANCH_CODE + "' ";
					conn.ExecuteQuery();
					bindData2();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (TXT_BRANCH_CODE.Text.Trim() == "")
			{
				Tools.popMessage(this, "Branch Code has not been set!");
				return;
			}
			if (LBL_SAVEMODE.Text.Trim() == "1")
			{
				conn.QueryString = "SELECT BRANCH_CODE FROM RFBRANCH WHERE BRANCH_CODE = '"+
					TXT_BRANCH_CODE.Text.Trim() + "' ";
				conn.ExecuteQuery();
				if (conn.GetRowCount() > 0)
				{
					Tools.popMessage(this, "Branch Code has already been used! Request canceled!");
					return;
				}
			}

			conn.QueryString = "PARAM_GENERAL_RFBRANCH_MAKER '"+ LBL_SAVEMODE.Text.Trim() + "', '" +
				TXT_BRANCH_CODE.Text.Trim() + "', '" + TXT_BRANCH_NAME.Text.Trim() + "', '" + 
				TXT_CBC_CODE.Text.Trim() + "', '" + DDL_CITYID.SelectedValue.Trim() +
				"', '" + TXT_BR_ZIPCODE.Text.Trim() + "', '" + TXT_BR_ADDR.Text.Trim() + "', '" +
				TXT_BR_BRANCHAREA.Text.Trim() + "', '" + TXT_BR_PHNFAX.Text.Trim() +
				"', null, null, null, null, '1', '" + TXT_BR_CCOBRANCH.Text.Trim() + "' ";
			conn.ExecuteQuery();
			bindData2();
			clearEditBoxes();
			//Tools.popMessage(this, "Data added for insertion/modification!");
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

	}
}
