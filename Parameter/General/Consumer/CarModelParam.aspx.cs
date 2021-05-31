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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for CarModelParam.
	/// </summary>
	public partial class CarModelParam : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);

			if(!IsPostBack)
			{
				ViewData(); 
			}
			else
			{
				InitialCon();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");	
			
			Datagrid1.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change1);
			DataGrid2.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change2);
		}

		private void ViewData()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			LBL_SAVEMODE.Text = "1";

			InitialCon();
 
			GlobalTools.fillRefList(DDL_MEREK,"select ID_MEREK, NM_MEREK from MEREK",false,conn);
  
			bindData1();
			bindData2();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void bindData1()
		{
			conn.QueryString = "select ID_MODEL, ID_MEREK, NM_MODEL  from MODEL where active='1' ORDER BY ID_MEREK ASC";
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
			conn.QueryString = "select ID_MODEL, ID_MEREK, NM_MODEL, CH_STA  from TMODEL";
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
				if (DataGrid2.Items[i].Cells[3].Text.Trim() == "0")
				{
					DataGrid2.Items[i].Cells[3].Text = "UPDATE";
				}
				else if (DataGrid2.Items[i].Cells[3].Text.Trim() == "1")
				{
					DataGrid2.Items[i].Cells[3].Text = "INSERT";
				}
				else if (DataGrid2.Items[i].Cells[3].Text.Trim() == "2")
				{
					DataGrid2.Items[i].Cells[3].Text = "DELETE";
				}
			} 
		}

		private void clearEditBoxes()
		{
			TXT_CODE.Text = "";
			TXT_DESC.Text = "";
			
			try
			{
				DDL_MEREK.SelectedIndex = 0;
			} 
			catch {}
			
			LBL_SAVEMODE.Text = "1";
			activatePostBackControls(true);
		}

		private void activatePostBackControls(bool mode)
		{
			TXT_CODE.Enabled = mode;
			DDL_MEREK.Enabled = mode;
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
					
					TXT_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_DESC.Text = e.Item.Cells[2].Text.Trim();
								
					try
					{
						DDL_MEREK.SelectedValue=e.Item.Cells[1].Text.Trim();
					} 
					catch {}
					
					LBL_SAVEMODE.Text = "0";
					
					cleansTextBox(TXT_CODE);
					cleansTextBox(TXT_DESC);
					
					activatePostBackControls(false);
					
					break;
				case "delete":
					LBL_SAVEMODE.Text = "2";
					
					conn.QueryString = "EXEC PARAM_GENERAL_CARMODEL '1','" +
						e.Item.Cells[0].Text.Trim()+ "', '" + e.Item.Cells[1].Text.Trim() + "', '" + 
						e.Item.Cells[2].Text.Trim() + "', '2' ,'','' " ;
					conn.ExecuteQuery();
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
					
					TXT_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_DESC.Text = e.Item.Cells[2].Text.Trim();
								
					try
					{
						DDL_MEREK.SelectedValue=e.Item.Cells[1].Text.Trim();
					} 
					catch {}
					LBL_SAVEMODE.Text = "0";
					cleansTextBox(TXT_CODE);
					cleansTextBox(TXT_DESC);
					activatePostBackControls(false);
					break;
				
				case "delete":
					conn.QueryString = "EXEC PARAM_GENERAL_CARMODEL '2','" +
						e.Item.Cells[0].Text.Trim()+ "', '" + e.Item.Cells[1].Text.Trim() + "', '" + 
						e.Item.Cells[2].Text.Trim() + "', '2' ,'','' " ;
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
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select * from MODEL where  ID_MEREK='" +DDL_MEREK.SelectedValue+ "' AND ID_MODEL='" +TXT_CODE+ "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		 

			conn.QueryString = "EXEC PARAM_GENERAL_CARMODEL '1','" +
				TXT_CODE.Text+ "', '"+DDL_MEREK.SelectedValue+"', '" + 
				TXT_DESC.Text + "','"+LBL_SAVEMODE.Text+"' ,'','' " ;
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
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40");
		}

	}
}
