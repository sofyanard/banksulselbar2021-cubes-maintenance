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
	/// Summary description for RFBMRatingIII.
	/// </summary>
	public partial class RFBMRatingIII : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			if (!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1";
				conn.QueryString = "select BMR_CODE,BMR_DESC from rfbmrating_i";
				conn.ExecuteQuery();
				DDL_A.Items.Clear();
				DDL_A.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_A.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i, 0)));
						
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
			conn.QueryString = "select (select B.BMR_DESC from rfbmrating_i B where B.BMR_CODE=A.BMR_CODE)RATE1,BMR_CODE,(select B.BMR2_DESC from rfbmrating_ii B where B.BMR2_CODE=A.BMR2_CODE AND B.BMR_CODE=A.BMR_CODE )RATE2,BMR2_CODE,BMR3_CODE,BMR3_DESC,BMR3_ACTIVE from rfbmrating_iii A where a.BMR3_ACTIVE=1";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("A"));
			dt.Columns.Add(new DataColumn("B"));
			dt.Columns.Add(new DataColumn("C"));
			dt.Columns.Add(new DataColumn("D"));
			dt.Columns.Add(new DataColumn("E"));
			dt.Columns.Add(new DataColumn("F"));
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
		
			conn.QueryString = "select (select B.BMR_DESC from rfbmrating_i B where B.BMR_CODE=A.BMR_CODE)RATE1,BMR_CODE,(select B.BMR2_DESC "+
				 " from rfbmrating_ii B where B.BMR2_CODE=A.BMR2_CODE AND B.BMR_CODE=A.BMR_CODE )RATE2,BMR2_CODE,BMR3_CODE,BMR3_DESC,pendingstatus from PENDING_RFBMRATING_III A ";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("A"));
			dt.Columns.Add(new DataColumn("B"));
			dt.Columns.Add(new DataColumn("C"));
			dt.Columns.Add(new DataColumn("D"));
			dt.Columns.Add(new DataColumn("E"));
			dt.Columns.Add(new DataColumn("F"));
			dt.Columns.Add(new DataColumn("G"));
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
				if (DataGrid2.Items[i].Cells[6].Text.Trim() == "0")
				{
					DataGrid2.Items[i].Cells[6].Text = "UPDATE";
				}
				else if (DataGrid2.Items[i].Cells[6].Text.Trim() == "1")
				{
					DataGrid2.Items[i].Cells[6].Text = "INSERT";
				}
				else if (DataGrid2.Items[i].Cells[6].Text.Trim() == "2")
				{
					DataGrid2.Items[i].Cells[6].Text = "DELETE";
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
					TXT_A.Text=e.Item.Cells[4].Text.Trim();
					TXT_B.Text = e.Item.Cells[5].Text.Trim();
					TXT_A.ReadOnly=true;
					try 
					{
						DDL_A.SelectedValue=e.Item.Cells[1].Text.Trim();
						DDL_B.SelectedValue=e.Item.Cells[3].Text.Trim();
					} 
					catch{}

					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					cleansTextBox(TXT_A);
					cleansTextBox(TXT_B);
					break;
					
				case "delete":
					LBL_SAVEMODE.Text = "2";
					conn.QueryString = "INSERT INTO PENDING_RFBMRATING_III VALUES ('"+e.Item.Cells[1].Text.Trim()+"','"+e.Item.Cells[3].Text.Trim()+"','"+e.Item.Cells[4].Text.Trim()+"', '"+e.Item.Cells[5].Text.Trim()+"','1','2')";
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
					TXT_A.Text=e.Item.Cells[4].Text.Trim();
					TXT_B.Text = e.Item.Cells[5].Text.Trim();
					TXT_A.ReadOnly=true;
					try 
					{
						DDL_A.SelectedValue=e.Item.Cells[1].Text.Trim();
						DDL_B.SelectedValue=e.Item.Cells[3].Text.Trim();
					} 
					catch{}

					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					cleansTextBox(TXT_A);
					cleansTextBox(TXT_B);
					break;
					
				case "delete":
					LBL_SAVEMODE.Text = "2";
					conn.QueryString = "DELETE PENDING_RFBMRATING_III WHERE BMR_CODE='"+e.Item.Cells[1].Text.Trim()+"' AND BMR2_CODE='"+e.Item.Cells[3].Text.Trim()+"' AND BMR3_CODE='"+e.Item.Cells[4].Text.Trim()+"' ";
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
				conn.QueryString = "select * from RFBMRATING_II where BMR_CODE='"+DDL_A.SelectedValue+"' AND BMR2_CODE='"+TXT_A.Text+"' ";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		 

			conn.QueryString = "INSERT INTO PENDING_RFBMRATING_III VALUES ('"+DDL_A.SelectedValue+"','"+DDL_B.SelectedValue+"','"+TXT_A.Text+"', '"+TXT_B.Text+"','1','"+LBL_SAVEMODE.Text+"')";
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

		protected void DDL_A_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(!DDL_A.SelectedValue.Equals(""))
			{
				conn.QueryString = "select BMR2_CODE,BMR2_DESC from rfbmrating_ii WHERE BMR_CODE='"+DDL_A.SelectedValue+"'";
				conn.ExecuteQuery();
				DDL_B.Items.Clear();
				DDL_B.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_B.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i, 0)));
			}
		
		}

	}
}
