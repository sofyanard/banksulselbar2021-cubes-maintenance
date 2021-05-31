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
	/// Summary description for CollateralBasedParam.
	/// </summary>
	public partial class CollateralBasedParam : System.Web.UI.Page
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

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
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
			
			InitialCon();
 
			conn.QueryString = "select PRODUCTID, PRODUCTNAME  from TPRODUCT";
			conn.ExecuteQuery();

			DDL_PROD_TYPE.Items.Clear();
			DDL_PROD_TYPE.Items.Add(new ListItem("- SELECT -", ""));
			
			for (int i=0; i < conn.GetRowCount(); i++)
				DDL_PROD_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
				
			conn.QueryString = "select RF_CODE,RF_DESC  from RFCOLATERAL";
			conn.ExecuteQuery();
			
			DDL_COLLATERAL.Items.Clear();
			DDL_COLLATERAL.Items.Add(new ListItem("- SELECT -", ""));
			
			for (int i=0; i<conn.GetRowCount(); i++)
				DDL_COLLATERAL.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

			LBL_SAVEMODE.Text = "1";
			
			bindData1();
			bindData2();
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
			conn.QueryString = "select SEQ_NO,PRODUCTID,COL_TYPE,(SELECT PRODUCTNAME FROM TPRODUCT T  WHERE T.PRODUCTID=RFCOLPROD.PRODUCTID) PRODUK ,(SELECT RF_DESC FROM RFCOLATERAL R  WHERE R.RF_CODE=RFCOLPROD.COL_TYPE) COLATERAL from RFCOLPROD where active='1'";
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
			conn.QueryString = "SELECT SEQ_NO,PRODUCTID,COL_TYPE,CH_STA,(SELECT PRODUCTNAME FROM TPRODUCT T  WHERE T.PRODUCTID=RF.PRODUCTID) PRODUK ,(SELECT RF_DESC FROM RFCOLATERAL R  WHERE R.RF_CODE=RF.COL_TYPE) COLATERAL FROM TRFCOLPROD RF";
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
				if (DataGrid2.Items[i].Cells[2].Text.Trim() == "0")
				{
					DataGrid2.Items[i].Cells[2].Text = "UPDATE";
				}
				else if (DataGrid2.Items[i].Cells[2].Text.Trim() == "1")
				{
					DataGrid2.Items[i].Cells[2].Text = "INSERT";
				}
				else if (DataGrid2.Items[i].Cells[2].Text.Trim() == "2")
				{
					DataGrid2.Items[i].Cells[2].Text = "DELETE";
				}
			} 
		}

		private void clearEditBoxes()
		{
			try
			{
				DDL_PROD_TYPE.SelectedIndex = 0;
				DDL_COLLATERAL.SelectedIndex = 0;
			} 
			catch {}
			
			LBL_SAVEMODE.Text = "1";
			DDL_PROD_TYPE.Enabled = true;
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
					try
					{
						DDL_PROD_TYPE.SelectedValue=e.Item.Cells[2].Text.Trim();
						DDL_COLLATERAL.SelectedValue = e.Item.Cells[3].Text.Trim();
					} 
					catch {}
					LBL_SAVEMODE.Text = "0";
					DDL_PROD_TYPE.Enabled = false;
				
					break;

				case "delete":
					LBL_SAVEMODE.Text = "2";
					
					
					conn.QueryString = "SELECT * FROM TRFCOLPROD WHERE PRODUCTID = '"+e.Item.Cells[2].Text.Trim()+"' "+
						"AND COL_TYPE = '"+e.Item.Cells[3].Text.Trim()+"'";

					conn.ExecuteQuery(); 

					if(conn.GetRowCount() == 0)
					{
						conn.QueryString = "EXEC PARAM_GENERAL_COLLPROD '1','','" +
							e.Item.Cells[2].Text.Trim()+ "', '" + e.Item.Cells[3].Text.Trim() + "','2' ,'','' " ;
						conn.ExecuteNonQuery();
					}
					else
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
						return;
					}

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
					try
					{
						DDL_PROD_TYPE.SelectedValue = e.Item.Cells[5].Text.Trim();
						DDL_COLLATERAL.SelectedValue = e.Item.Cells[6].Text.Trim();
					} 
					catch {}
					
					LBL_SAVEMODE.Text = "0";
					DDL_PROD_TYPE.Enabled = false;
					break;

				case "delete":
					conn.QueryString = "EXEC PARAM_GENERAL_COLLPROD '2','','" +
						e.Item.Cells[5].Text.Trim()+ "', '" + e.Item.Cells[6].Text.Trim() + "','2' ,'','' " ;
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
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select * from RFCOLPROD where PRODUCTID='" +DDL_PROD_TYPE.SelectedValue+ "' AND COL_TYPE='" +DDL_COLLATERAL.SelectedValue+ "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		 

			conn.QueryString = "EXEC PARAM_GENERAL_COLLPROD '1','','" +
				DDL_PROD_TYPE.SelectedValue+ "', '" + DDL_COLLATERAL.SelectedValue + "','"+LBL_SAVEMODE.Text+"' ,'','' " ;
			conn.ExecuteNonQuery();
			
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
			Response.Redirect("../../GeneralParamAll.aspx?ModuleId=40");
		}

	}
}

