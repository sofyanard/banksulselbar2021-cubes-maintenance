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
	/// Summary description for RFBMRatingII.
	/// </summary>
	public partial class RFBmSubSubSektorEkonomi : System.Web.UI.Page
	{
	
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			if (!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1";
				conn.QueryString = "SELECT BMSUB_CODE, BMSUB_DESC FROM VW_RFBMSUBSEKTOREKONOMI";
				conn.ExecuteQuery();
				DDL_A.Items.Clear();
				DDL_A.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_A.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i, 0)));
		
				conn.QueryString = "SELECT BI_SEQ, BI_DESC FROM VW_RFBICODE WHERE BG_GROUP = '3'";
				conn.ExecuteQuery();
				DDL_B.Items.Clear();
				DDL_B.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_B.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i, 0)));

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
			conn.QueryString = "select BMSUBSUB_CODE,BMSUBSUB_DESC,BMSUB_CODE,BI_SEQ,BG_GROUP,(select B.BMSUB_DESC from rfbmsubsektorekonomi B where B.BMSUB_CODE=A.BMSUB_CODE)BMSUB_DESC from rfbmsubsubsektorekonomi A WHERE ACTIVE=1";
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
		
			conn.QueryString = "select BMSUBSUB_CODE,BMSUBSUB_DESC,BMSUB_CODE,BI_SEQ,BG_GROUP,PENDINGSTATUS,(select B.BMSUB_DESC from rfbmsubsektorekonomi B where B.BMSUB_CODE=A.BMSUB_CODE)BMSUB_DESC from pending_rfbmsubsubsektorekonomi A ";
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
			TXT_A.Text="";
			//TXT_A.Enabled=true;
			TXT_B.Text="";
			TXT_A.ReadOnly=false;
			DDL_A.SelectedValue="";
			DDL_A.Enabled=true;
			DDL_B.SelectedValue="";
			DDL_B.Enabled=true;
					
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
					TXT_A.Text=e.Item.Cells[2].Text.Trim();
					TXT_B.Text = e.Item.Cells[3].Text.Trim();
					DDL_A.SelectedValue=e.Item.Cells[5].Text.Trim();
					DDL_B.SelectedValue=e.Item.Cells[0].Text.Trim();
					//TXT_A.Enabled=false;
					DDL_A.Enabled=false;
					DDL_B.Enabled=false;
					TXT_A.ReadOnly=true;
					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					cleansTextBox(TXT_A);
					cleansTextBox(TXT_B);
					break;
					
				case "delete":
					//LBL_SAVEMODE.Text = "2";
					conn.QueryString = "INSERT INTO pending_rfbmsubsubsektorekonomi VALUES ('"+e.Item.Cells[2].Text.Trim()+"','"+e.Item.Cells[3].Text.Trim()+"', '"+e.Item.Cells[5].Text.Trim()+"', '"+e.Item.Cells[0].Text.Trim()+"','3','1','2')";
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
					TXT_A.Text=e.Item.Cells[2].Text.Trim();
					TXT_B.Text = e.Item.Cells[3].Text.Trim();
					TXT_A.ReadOnly=true;
					DDL_A.SelectedValue=e.Item.Cells[6].Text.Trim();
					DDL_B.SelectedValue=e.Item.Cells[0].Text.Trim();
					//TXT_A.Enabled=false;
					DDL_A.Enabled=false;
					DDL_B.Enabled=false;
					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					cleansTextBox(TXT_A);
					cleansTextBox(TXT_B);
					break;
					
				case "delete":
					//LBL_SAVEMODE.Text = "2";
					conn.QueryString = "DELETE pending_rfbmsubsubsektorekonomi WHERE BMSUB_CODE='"+e.Item.Cells[6].Text.Trim()+"' AND BMSUBSUB_CODE='"+e.Item.Cells[2].Text.Trim()+"' and BI_SEQ='"+e.Item.Cells[0].Text.Trim()+"'";
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
				conn.QueryString = "select * from rfbmsubsubsektorekonomi where BMSUBSUB_CODE='"+TXT_A.Text+"'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		 

			conn.QueryString = "INSERT INTO pending_rfbmsubsubsektorekonomi VALUES ('"+TXT_A.Text+"','"+TXT_B.Text+"','"+DDL_A.SelectedValue+"','"+DDL_B.SelectedValue+"','3','1','"+LBL_SAVEMODE.Text+"')";
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
			//Response.Redirect("../../HostParam.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]);
		}

	}
}
