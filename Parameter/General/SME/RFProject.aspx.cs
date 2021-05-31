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
using Microsoft.VisualBasic;


namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFBMRatingIII.
	/// </summary>
	public partial class RFProject : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList DDL_A;
		protected System.Web.UI.WebControls.DropDownList DDL_B;
		protected Tools tool = new Tools();
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			if (!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1";
				DDL_EXPIRYDATE.Items.Add(new ListItem("- PILIH -", ""));

				for (int i = 1; i <= 12; i++)
				{
					DDL_EXPIRYDATE.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
				}

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
			/// data ditampilin semua
			//conn.QueryString = "select PRJ_CODE,PRJ_NAME,PRJ_DESC,PRJ_LIMIT,PRJ_LIMIT_REMAINING,PRJ_LIMIT_PENDING_APPRV, convert(varchar,PRJ_EXPIRY_DATE,103) PRJ_EXPIRY_DATE,ACTIVE from RFPROJECT where ACTIVE=1 ";

			conn.QueryString = "select PRJ_CODE,PRJ_NAME,PRJ_DESC,PRJ_LIMIT,PRJ_LIMIT_REMAINING,PRJ_LIMIT_PENDING_APPRV, convert(varchar,PRJ_EXPIRY_DATE,103) PRJ_EXPIRY_DATE,ACTIVE from RFPROJECT  ";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("A"));
			dt.Columns.Add(new DataColumn("B"));
			dt.Columns.Add(new DataColumn("C"));
			dt.Columns.Add(new DataColumn("D"));
			dt.Columns.Add(new DataColumn("E"));
			dt.Columns.Add(new DataColumn("F"));
			dt.Columns.Add(new DataColumn("G"));
			dt.Columns.Add(new DataColumn("ACTIVE"));
			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,2);
				dr[3] = conn.GetFieldValue(i,3);
				dr[4] = conn.GetFieldValue(i,4);
				dr[5] = conn.GetFieldValue(i,5);
				dr[6] = conn.GetFieldValue(i,6);
				dr[7] = conn.GetFieldValue(i,7);
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

			
			for (int i=0; i < Datagrid1.Items.Count; i++)
			{
				if (Datagrid1.Items[i].Cells[7].Text.Trim() =="0" )
				{

					LinkButton l_del = (LinkButton) Datagrid1.Items[i].FindControl("lnk_RfDelete1");
					l_del.CommandName = "Undelete";
					l_del.Text = "Undelete";

					LinkButton l_edit = (LinkButton) Datagrid1.Items[i].FindControl("lnk_RfEdit1");
					l_edit.Visible = false;
				}
			}
		}

	
		private void bindData2()
		{
		
			conn.QueryString = "select PRJ_CODE,PRJ_NAME,PRJ_DESC,PRJ_LIMIT,PRJ_LIMIT_REMAINING,PRJ_LIMIT_PENDING_APPRV,convert(varchar,PRJ_EXPIRY_DATE,103) PRJ_EXPIRY_DATE,PENDINGSTATUS from PENDING_RFPROJECT";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("A"));
			dt.Columns.Add(new DataColumn("B"));
			dt.Columns.Add(new DataColumn("C"));
			dt.Columns.Add(new DataColumn("D"));
			dt.Columns.Add(new DataColumn("E"));
			dt.Columns.Add(new DataColumn("F"));
			dt.Columns.Add(new DataColumn("G"));
			dt.Columns.Add(new DataColumn("H"));
			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
               	dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,2);
				dr[3] = conn.GetFieldValue(i,3);
				dr[4] = conn.GetFieldValue(i,4);
				dr[5] = conn.GetFieldValue(i,5);
				dr[6] = conn.GetFieldValue(i,6);
				dr[7] = conn.GetFieldValue(i,7);
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
				if (DataGrid2.Items[i].Cells[7].Text.Trim() == "0")
				{
					DataGrid2.Items[i].Cells[7].Text = "UPDATE";
				}
				else if (DataGrid2.Items[i].Cells[7].Text.Trim() == "1")
				{
					DataGrid2.Items[i].Cells[7].Text = "INSERT";
				}
				else if (DataGrid2.Items[i].Cells[7].Text.Trim() == "2")
				{
					DataGrid2.Items[i].Cells[7].Text = "DELETE";
				}
			} 
			
		}

		private void clearEditBoxes()
		{
			TXT_A.Text="";
			TXT_B.Text="";
			TXT_C.Text="";
			TXT_D.Text="";
			TXT_E.Text="";
			TXT_F.Text="";
			TXT_HARI.Text="";
			TXT_TAHUN.Text="";
			DDL_EXPIRYDATE.SelectedValue="";
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
			this.DataGrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_ItemCommand);
			this.Datagrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_ItemCommand);

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
						TXT_HARI.Text = GlobalTools.FormatDate_Day(e.Item.Cells[6].Text);
						DDL_EXPIRYDATE.SelectedValue = GlobalTools.FormatDate_Month(e.Item.Cells[6].Text);
						TXT_TAHUN.Text = GlobalTools.FormatDate_Year(e.Item.Cells[6].Text);
					}
					catch{}	

					TXT_A.Text =e.Item.Cells[0].Text.Trim();
					TXT_B.Text =e.Item.Cells[1].Text.Trim();
					TXT_C.Text =e.Item.Cells[2].Text.Trim();
					//TXT_D.Text =GlobalTools.MoneyFormat(e.Item.Cells[3].Text.Trim());
					//TXT_E.Text =GlobalTools.MoneyFormat(e.Item.Cells[4].Text.Trim());
					//TXT_F.Text =GlobalTools.MoneyFormat(e.Item.Cells[5].Text.Trim());
					TXT_D.Text =e.Item.Cells[3].Text.Trim();
					TXT_E.Text =e.Item.Cells[4].Text.Trim();
					TXT_F.Text =e.Item.Cells[5].Text.Trim();
					TXT_A.ReadOnly=true;
					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					cleansTextBox(TXT_A);
					cleansTextBox(TXT_B);
					cleansTextBox(TXT_C);
					cleansTextBox(TXT_D);
					cleansTextBox(TXT_E);
					cleansTextBox(TXT_F);
					cleansTextBox(TXT_HARI);
					cleansTextBox(TXT_TAHUN);
					break;
					
				case "delete":
					//LBL_SAVEMODE.Text = "2";
					//conn.QueryString = "INSERT INTO PENDING_RFPROJECT VALUES ('"+e.Item.Cells[0].Text.Trim()+"','"+e.Item.Cells[1].Text.Trim()+"','"+e.Item.Cells[2].Text.Trim()+"','"+e.Item.Cells[3].Text.Trim()+"','"+e.Item.Cells[4].Text.Trim()+"','"+e.Item.Cells[5].Text.Trim()+"','1','2')";
					conn.QueryString = "EXEC PARAM_GENERAL_RFPROJECT_PENDING 'INSERT','" + e.Item.Cells[0].Text.Trim()+"','"+e.Item.Cells[1].Text.Trim()+"','"+e.Item.Cells[2].Text.Trim()+"','"+e.Item.Cells[3].Text.Trim()+"','"+e.Item.Cells[4].Text.Trim()+"','"+e.Item.Cells[5].Text.Trim()+"','1','2','"+e.Item.Cells[6].Text.Trim()+"'";
					conn.ExecuteNonQuery();
					bindData2();
					break;
				
				case "Undelete":
					//LBL_SAVEMODE.Text = "0";
					//conn.QueryString = "INSERT INTO PENDING_RFPROJECT VALUES ('"+e.Item.Cells[0].Text.Trim()+"','"+e.Item.Cells[1].Text.Trim()+"','"+e.Item.Cells[2].Text.Trim()+"','"+e.Item.Cells[3].Text.Trim()+"','"+e.Item.Cells[4].Text.Trim()+"','"+e.Item.Cells[5].Text.Trim()+"','1','2')";
					conn.QueryString = "EXEC PARAM_GENERAL_RFPROJECT_PENDING 'INSERT','" + e.Item.Cells[0].Text.Trim()+"','"+e.Item.Cells[1].Text.Trim()+"','"+e.Item.Cells[2].Text.Trim()+"','"+e.Item.Cells[3].Text.Trim()+"','"+e.Item.Cells[4].Text.Trim()+"','"+e.Item.Cells[5].Text.Trim()+"','1','0','"+e.Item.Cells[6].Text.Trim()+"'";
					conn.ExecuteNonQuery();
					bindData2();
					break;

				default:
					// Do nothing.
					break;
			} 
			LBL_SAVEMODE.Text = "1";
					
		}

		private void DataGrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					
					try
					{
						TXT_HARI.Text = GlobalTools.FormatDate_Day(e.Item.Cells[6].Text);
						DDL_EXPIRYDATE.SelectedValue = GlobalTools.FormatDate_Month(e.Item.Cells[6].Text);
						TXT_TAHUN.Text = GlobalTools.FormatDate_Year(e.Item.Cells[6].Text);
					}
					catch{}	

					TXT_A.Text =e.Item.Cells[0].Text.Trim();
					TXT_B.Text =e.Item.Cells[1].Text.Trim();
					TXT_C.Text =e.Item.Cells[2].Text.Trim();
					//TXT_D.Text =GlobalTools.MoneyFormat(e.Item.Cells[3].Text.Trim());
					//TXT_E.Text =GlobalTools.MoneyFormat(e.Item.Cells[4].Text.Trim());
					//TXT_F.Text =GlobalTools.MoneyFormat(e.Item.Cells[5].Text.Trim());
					TXT_D.Text =e.Item.Cells[3].Text.Trim();
					TXT_E.Text =e.Item.Cells[4].Text.Trim();
					TXT_F.Text =e.Item.Cells[5].Text.Trim();
					TXT_A.ReadOnly=true;
					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					cleansTextBox(TXT_A);
					cleansTextBox(TXT_B);
					cleansTextBox(TXT_C);
					cleansTextBox(TXT_D);
					cleansTextBox(TXT_E);
					cleansTextBox(TXT_F);
					cleansTextBox(TXT_HARI);
					cleansTextBox(TXT_TAHUN);
					break;
					
				case "delete":
					LBL_SAVEMODE.Text = "2";
					//conn.QueryString = "DELETE PENDING_RFPROJECT WHERE PRJ_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.QueryString = "EXEC PARAM_GENERAL_RFPROJECT_PENDING 'DELETE','" + e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteNonQuery();
					bindData2();
					break;
				default:
					// Do nothing.
					break;
			}  
			LBL_SAVEMODE.Text = "1";
					
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select * from RFPROJECT WHERE PRJ_CODE='"+TXT_A.Text+"'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		 
			
			//conn.QueryString = "INSERT INTO PENDING_RFPROJECT VALUES ('"+TXT_A.Text+"','"+TXT_B.Text+"','"+TXT_C.Text+"','"+TXT_D.Text+"','"+TXT_E.Text+"','"+TXT_F.Text+"','1','"+LBL_SAVEMODE.Text.Trim()+"')";
			//conn.QueryString = "EXEC PARAM_GENERAL_RFPROJECT_PENDING 'INSERT','" + TXT_A.Text+"','"+TXT_B.Text+"','"+TXT_C.Text+"','"+TXT_D.Text+"','"+TXT_E.Text+"','"+TXT_F.Text+"','1','"+LBL_SAVEMODE.Text.Trim()+"',"+GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_EXPIRYDATE.SelectedValue,TXT_TAHUN.Text.Trim())+"";
			conn.QueryString = "EXEC PARAM_GENERAL_RFPROJECT_PENDING 'INSERT','" + TXT_A.Text+"','"+TXT_B.Text+"','"+TXT_C.Text+"',"+ tool.ConvertFloat(TXT_D.Text) +","+ tool.ConvertFloat(TXT_E.Text)+","+tool.ConvertFloat(TXT_F.Text)+",'1','"+LBL_SAVEMODE.Text.Trim()+"',"+GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_EXPIRYDATE.SelectedValue,TXT_TAHUN.Text.Trim())+"";
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
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]);
		}
	}
}
