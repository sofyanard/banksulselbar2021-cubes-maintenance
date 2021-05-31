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
using System.Configuration;
namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFPDIndustryClass.
	/// </summary>
	public partial class RFPDIndustryClass : System.Web.UI.Page
	{
	
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			if (!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1";
				conn.QueryString = "select PD_INDUSTRY_CLASSCD, PD_INDUSTRY_CLASS from PD_RF_INDUSTRY_CLASS_CAP where active='1'";
				conn.ExecuteQuery();
				DDL_INDUSTRY_CLASS.Items.Clear();
				DDL_INDUSTRY_CLASS.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_INDUSTRY_CLASS.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i, 0)));

				bindData1();
				bindData2();
			}
			
			Datagrid1.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change1);
			DataGrid2.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change2);
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


	
		private void bindData1()
		{
			conn.QueryString = "select PD_INDUSTRY_NAMECD, PD_INDUSTRY_NAME, PD_INDUSTRY_CLASSCD, (select PD_INDUSTRY_CLASS from PD_RF_INDUSTRY_CLASS_CAP B where B.PD_INDUSTRY_CLASSCD = A.PD_INDUSTRY_CLASSCD)PD_INDUSTRY_CLASS from PD_RF_INDUSTRY_CLASS A where active='1'";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			Datagrid1.DataSource = dt;
			try
			{
				this.Datagrid1.DataBind();
			}
			catch
			{
				try
				{
					this.Datagrid1.CurrentPageIndex = Datagrid1.CurrentPageIndex - 1;
					this.Datagrid1.DataBind();
				}
				catch{}
			}
		}

	
		private void bindData2()
		{
		
			conn.QueryString = "select PD_INDUSTRY_NAMECD, PD_INDUSTRY_NAME, PD_INDUSTRY_CLASSCD, PENDINGSTATUS, (select PD_INDUSTRY_CLASS from PD_RF_INDUSTRY_CLASS_CAP B where B.PD_INDUSTRY_CLASSCD = A.PD_INDUSTRY_CLASSCD)PD_INDUSTRY_CLASS from PD_PENDING_RF_INDUSTRY_CLASS A";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DataGrid2.DataSource = dt;
			try
			{
				this.DataGrid2.DataBind();
			}
			catch
			{
				try
				{
					this.DataGrid2.CurrentPageIndex = DataGrid2.CurrentPageIndex - 1;
					this.DataGrid2.DataBind();
				}
				catch{}
			}
			
			for (int i = 0; i < DataGrid2.Items.Count; i++)
			{
				if (DataGrid2.Items[i].Cells[4].Text.Trim() == "0")
				{
					DataGrid2.Items[i].Cells[4].Text = "UPDATE";
				}
				else if (DataGrid2.Items[i].Cells[4].Text.Trim() == "1")
				{
					DataGrid2.Items[i].Cells[4].Text = "INSERT";
				}
				else if (DataGrid2.Items[i].Cells[4].Text.Trim() == "2")
				{
					DataGrid2.Items[i].Cells[4].Text = "DELETE";
				}
			} 
		}

		private void clearEditBoxes()
		{
			TXT_INDUSTRY_NAME.Text="";
			TXT_RATIO.Text="";
			TXT_RATIO.ReadOnly=true;
			TXT_INDUSTRY_NAME.ReadOnly=false;
			DDL_INDUSTRY_CLASS.SelectedValue="";
			DDL_INDUSTRY_CLASS.Enabled=true;
					
			LBL_SAVEMODE.Text = "1";
			activatePostBackControls(true);
		}

		private void activatePostBackControls(bool mode)
		{
			//TXT_BRANCH_CODE.Enabled = mode;
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
					TXT_INDUSTRY_NAME.Text=e.Item.Cells[1].Text.Trim();
					TXT_RATIO.Text = e.Item.Cells[3].Text.Trim();
					DDL_INDUSTRY_CLASS.SelectedValue=e.Item.Cells[2].Text.Trim();
					DDL_INDUSTRY_CLASS.Enabled=false;
					TXT_RATIO.ReadOnly=true;
					LBL_SAVEMODE.Text = "0";
					LBL_INDUSTRYCD.Text=e.Item.Cells[0].Text.Trim();
					LBL_INDUSTRYCD.Visible=false;
					activatePostBackControls(false);
					cleansTextBox(TXT_INDUSTRY_NAME);
					cleansTextBox(TXT_RATIO);
					break;
					
				case "delete":
					//LBL_SAVEMODE.Text = "2";
					conn.QueryString = "INSERT INTO PD_PENDING_RF_INDUSTRY_CLASS values ('"+e.Item.Cells[0].Text.Trim()+"','"+e.Item.Cells[1].Text.Trim()+"', '"+e.Item.Cells[2].Text.Trim()+"','1','2')";
					conn.ExecuteNonQuery();

					bindData2();
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
					TXT_INDUSTRY_NAME.Text=e.Item.Cells[1].Text.Trim();
					TXT_RATIO.Text = e.Item.Cells[3].Text.Trim();
					DDL_INDUSTRY_CLASS.SelectedValue=e.Item.Cells[2].Text.Trim();
					DDL_INDUSTRY_CLASS.Enabled=false;
					TXT_RATIO.ReadOnly=true;
					LBL_SAVEMODE.Text = "0";
					LBL_INDUSTRYCD.Text=e.Item.Cells[0].Text.Trim();
					LBL_INDUSTRYCD.Visible=false;
					activatePostBackControls(false);
					cleansTextBox(TXT_INDUSTRY_NAME);
					cleansTextBox(TXT_RATIO);
					break;
					
				case "delete":
					//LBL_SAVEMODE.Text = "2";
					conn.QueryString = "delete PD_PENDING_RF_INDUSTRY_CLASS where PD_INDUSTRY_NAMECD='"+e.Item.Cells[0].Text.Trim()+"'  and PD_INDUSTRY_CLASSCD='"+e.Item.Cells[2].Text.Trim()+"'";
					conn.ExecuteNonQuery();
					bindData2();
					break;
				default:
					// Do nothing.
					break;
			}  
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "INSERT INTO PD_PENDING_RF_INDUSTRY_CLASS VALUES ('"+LBL_INDUSTRYCD.Text+"','"+TXT_INDUSTRY_NAME.Text+"','"+DDL_INDUSTRY_CLASS.SelectedValue+"','1','"+LBL_SAVEMODE.Text+"')";
			conn.ExecuteQuery();
			bindData2();
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../PDParam.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]);
		}

	}
}
