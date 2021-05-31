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
	/// Summary description for RFBMRatingI.
	/// </summary>
	public partial class RFBMRatingI : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			if (!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1";
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
			conn.QueryString = "SELECT *  FROM rfbmrating_i where BMR_ACTIVE=1 ";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("A"));
			dt.Columns.Add(new DataColumn("B"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dt.Rows.Add(dr);
			}
			Datagrid1.DataSource = new DataView(dt);
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
		
			conn.QueryString = "SELECT *  FROM pending_rfbmrating_i where BMR_ACTIVE=1 ";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("A"));
			dt.Columns.Add(new DataColumn("B"));
			dt.Columns.Add(new DataColumn("C"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,3);
				dt.Rows.Add(dr);
			}
			DataGrid2.DataSource = new DataView(dt);
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
			TXT_A.Text="";
			TXT_B.Text="";
			TXT_A.ReadOnly=false;
					
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
					TXT_A.Text=e.Item.Cells[0].Text.Trim();
					TXT_B.Text = e.Item.Cells[1].Text.Trim();
					TXT_A.ReadOnly=true;
					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					cleansTextBox(TXT_A);
					cleansTextBox(TXT_B);
					break;
					
				case "delete":
					LBL_SAVEMODE.Text = "2";
					conn.QueryString = "INSERT INTO PENDING_RFBMRATING_I VALUES ('"+e.Item.Cells[0].Text.Trim()+"', '"+e.Item.Cells[1].Text.Trim()+"','1','2')";
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
					TXT_A.Text=e.Item.Cells[0].Text.Trim();
					TXT_B.Text = e.Item.Cells[1].Text.Trim();
					LBL_SAVEMODE.Text = "0";
					TXT_A.ReadOnly=true;
					activatePostBackControls(false);
					cleansTextBox(TXT_A);
					cleansTextBox(TXT_B);
					break;
					
				case "delete":
					LBL_SAVEMODE.Text = "2";
					conn.QueryString = "DELETE PENDING_RFBMRATING_I WHERE BMR_CODE='"+e.Item.Cells[0].Text.Trim()+"' ";
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
				conn.QueryString = "select * from RFBMRATING_I where PENDING_RFBMRATING_I='"+TXT_A.Text+ "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		 

			conn.QueryString = "INSERT INTO PENDING_RFBMRATING_I VALUES ('"+TXT_A.Text+"', '"+TXT_B.Text+"','1','"+LBL_SAVEMODE.Text+"')";
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
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]);
		}

	}
}
