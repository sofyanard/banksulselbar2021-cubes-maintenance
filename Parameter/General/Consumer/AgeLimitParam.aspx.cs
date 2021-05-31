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

namespace SME.MaintainanceAll.ParametersAll.GeneralAll.Consumer
{
	/// <summary>
	/// Summary description for AgeLimitParam.
	/// </summary>
	public partial class AgeLimitParam : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox3;
		protected System.Web.UI.WebControls.TextBox TextBox4;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.DropDownList DropDownList2;
		protected Connection conn,conn2; 
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				setddl();

				LBL_SAVEMODE.Text = "1";
				bindData1();
				bindData2();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");
			
			Datagrid1.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change1);
			DataGrid2.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change2);
		}


		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();

			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}
		private void setddl()
		{
			//PRODUCT
			GlobalTools.fillRefList(DDL_PROD_TYPE,"select PRODUCTID, PRODUCTNAME   from TPRODUCT  where GROUP_ID = '1'",conn);
			//EMPLOYEE
			GlobalTools.fillRefList(DDL_EMPL_TYPE,"select JOB_TYPE_ID, DES from JOB_TYPE WHERE ACTIVE='1'",conn);
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
			conn.QueryString = "SELECT b.productname,c.des,MIN_AGE,MAX_AGE,a.PRODUCT_ID,a.JOB_TYPE_ID "+
				"FROM PRODUCT_JOBTYPE  a left join tproduct b on a.product_id=b.productid "+
				"left join job_type c on a.job_type_id=c.job_type_id where a.active='1'";
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
		
			conn.QueryString = "select b.productname,c.des,a.min_age,a.max_age,a.product_id,a.job_type_id,ch_sta, "+
				"case when ch_sta='0' then 'Update' when ch_sta='1' then 'Insert' when ch_sta='2' then 'Delete' else '' end CH_STA1 "+
				",seq_id from TPRODUCT_JOBTYPE a left join tproduct b on a.product_id=b.productid "+
				"left join job_type c on a.job_type_id=c.job_type_id ";
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
			
		  
		}

		private void clearEditBoxes()
		{
			TXT_MIN_AGE.Text = "";
			TXT_MAX_AGE.Text = "";
			try
			{
				DDL_PROD_TYPE.SelectedIndex = 0;
				DDL_EMPL_TYPE.SelectedIndex = 0;
			} 
			catch {}
			
			LBL_SAVEMODE.Text = "1";
			activatePostBackControls(true);
			DDL_PROD_TYPE.Enabled = true;
			DDL_EMPL_TYPE.Enabled = true;
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
					
					TXT_MIN_AGE.Text = e.Item.Cells[2].Text.Trim();
					TXT_MAX_AGE.Text = e.Item.Cells[3].Text.Trim();
					try
					{
						DDL_PROD_TYPE.SelectedValue=e.Item.Cells[4].Text.Trim();
						DDL_EMPL_TYPE.SelectedValue = e.Item.Cells[5].Text.Trim();
					} 
					catch {}
					LBL_SAVEMODE.Text = "0";
					cleansTextBox(TXT_MIN_AGE);
					cleansTextBox(TXT_MAX_AGE);
					DDL_PROD_TYPE.Enabled = false;
					DDL_EMPL_TYPE.Enabled = false;
					activatePostBackControls(false);
					break;
				case "delete":
					LBL_SAVEMODE.Text = "2";
					
					conn.QueryString = "EXEC PARAM_GENERAL_AGELIMIT '1','1','" +
						e.Item.Cells[4].Text.Trim()+ "', '" + e.Item.Cells[5].Text.Trim() + "', '" + 
						e.Item.Cells[2].Text.Trim() + "', '"+e.Item.Cells[3].Text.Trim()+"', '2' ,'','' " ;
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

					LBL_SAVEMODE.Text = e.Item.Cells[6].Text.Trim();
					if (LBL_SAVEMODE.Text=="2")
					{
						LBL_SAVEMODE.Text="1"; // lock control when pending status is delete
						break;
					}					
					TXT_MIN_AGE.Text = e.Item.Cells[2].Text.Trim();
					TXT_MAX_AGE.Text = e.Item.Cells[3].Text.Trim();
					try
					{
						DDL_PROD_TYPE.SelectedValue=e.Item.Cells[4].Text.Trim();
						DDL_EMPL_TYPE.SelectedValue = e.Item.Cells[5].Text.Trim();
					} 
					catch {}
					LBL_SAVEMODE.Text = "0";
					cleansTextBox(TXT_MIN_AGE);
					cleansTextBox(TXT_MAX_AGE);
					DDL_PROD_TYPE.Enabled = false;
					DDL_EMPL_TYPE.Enabled = false;
					activatePostBackControls(false);
					break;
				case "delete":
					string seq = e.Item.Cells[8].Text.Trim();
					conn.QueryString = "exec PARAM_GENERAL_AGELIMIT '2','"+seq+"','','','','','' , '',''";
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
				conn.QueryString = "select * from PRODUCT_JOBTYPE where PRODUCT_ID = '" +DDL_PROD_TYPE.SelectedValue+ "' AND JOB_TYPE_ID = '" +DDL_EMPL_TYPE.SelectedValue+ "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		 

			conn.QueryString = "EXEC PARAM_GENERAL_AGELIMIT '1','1','" +
			DDL_PROD_TYPE.SelectedValue + "', '" + DDL_EMPL_TYPE.SelectedValue + "', '" + 
			TXT_MIN_AGE.Text + "', '"+TXT_MAX_AGE.Text +"','"+LBL_SAVEMODE.Text+"' ,'','' " ;
			conn.ExecuteQuery();
			//conn.ExecuteNonQuery();
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
