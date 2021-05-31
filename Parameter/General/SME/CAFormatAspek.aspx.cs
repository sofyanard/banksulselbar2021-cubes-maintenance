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
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for CAFormatAspek.
	/// </summary>
	public partial class CAFormatAspek : System.Web.UI.Page
	{
	
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			if (!IsPostBack)
			{
				//FORMAT
				conn.QueryString = "select * from RFCA_FORMAT WHERE ACTIVE =1";
				conn.ExecuteQuery();
				DDL_FORMAT.Items.Clear();
				DDL_FORMAT.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_FORMAT.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i, 0)));
				//ASPEK
				conn.QueryString = "select * from RFCA_ASPEK WHERE ACTIVE =1";
				conn.ExecuteQuery();
				DDL_AP.Items.Clear();
				DDL_AP.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_AP.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

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
			conn.QueryString = "SELECT A.FORMATID,(select TOP 1 FORMATDESC from RFCA_FORMAT B WHERE A.FORMATID=B.FORMATID ) FORMATDESC, A.ASPEKID "+
                                " ,(select TOP 1 ASPEKDESC from RFCA_ASPEK B WHERE A.ASPEKID=B.ASPEKID )ASPEKDESC "+
								"	FROM RFCA_FORMATASPEK A WHERE ACTIVE =1";
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
		
			conn.QueryString = "SELECT A.FORMATID,(select TOP 1 FORMATDESC from RFCA_FORMAT B WHERE A.FORMATID=B.FORMATID ) FORMATDESC, A.ASPEKID "+
				",(select TOP 1 ASPEKDESC from RFCA_ASPEK B WHERE A.ASPEKID=B.ASPEKID )ASPEKDESC,PENDINGSTATUS "+
				"	FROM PENDING_RFCA_FORMATASPEK A ";
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
			DDL_FORMAT.SelectedIndex = 0;
			DDL_AP.SelectedIndex = 0;
							
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
					
					DDL_FORMAT.SelectedValue=e.Item.Cells[0].Text.Trim();
					DDL_AP.SelectedValue = e.Item.Cells[2].Text.Trim();
					
					
					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					break;
					
				case "delete":
					LBL_SAVEMODE.Text = "2";
					/*
					conn.QueryString = "INSERT INTO PENDING_RFCA_FORMATASPEK(FORMATID,ASPEKID,PENDINGSTATUS ) VALUES ('" +
						e.Item.Cells[0].Text.Trim()+ "', '" + e.Item.Cells[2].Text.Trim() + "','2') " ;			
						*/
					conn.QueryString = "EXEC PARAM_GENERAL_RFCAFORMATASPEK_PENDING 'INSERT','" + e.Item.Cells[0].Text.Trim() +"','" + e.Item.Cells[2].Text.Trim() + "','2'";
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
					DDL_FORMAT.SelectedValue=e.Item.Cells[0].Text.Trim();
					DDL_AP.SelectedValue = e.Item.Cells[2].Text.Trim();
					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					break;
				case "delete":
					//conn.QueryString = "DELETE FROM PENDING_RFCA_FORMATASPEK  WHERE FORMATID='"+e.Item.Cells[0].Text.Trim()+ "' AND ASPEKID='"+e.Item.Cells[2].Text.Trim() + "' " ;
					conn.QueryString = "EXEC PARAM_GENERAL_RFCAFORMATASPEK_PENDING 'DELETE','" + e.Item.Cells[0].Text.Trim() +"','" + e.Item.Cells[2].Text.Trim() + "',''";
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
				conn.QueryString = "select * from RFCA_FORMATASPEK where FORMATID='" +DDL_FORMAT.SelectedValue+ "' AND ASPEKID='" +DDL_AP.SelectedValue+ "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
				conn.QueryString = "select * from PENDING_RFCA_FORMATASPEK where FORMATID='" +DDL_FORMAT.SelectedValue+ "' AND ASPEKID='" +DDL_AP.SelectedValue+ "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		 
			
			/*
			conn.QueryString = "INSERT INTO PENDING_RFCA_FORMATASPEK(FORMATID,ASPEKID,PENDINGSTATUS ) VALUES ('" +
						DDL_FORMAT.SelectedValue+ "', '" + DDL_AP.SelectedValue + "','"+LBL_SAVEMODE.Text.Trim()+"') " ;			
			*/
			conn.QueryString = "EXEC PARAM_GENERAL_RFCAFORMATASPEK_PENDING 'INSERT','" + DDL_FORMAT.SelectedValue +"','" + DDL_AP.SelectedValue + "','" + LBL_SAVEMODE.Text.Trim() + "'";
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
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]);
		}

	}
}
