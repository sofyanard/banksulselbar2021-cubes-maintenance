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
	/// Summary description for RFCHANN_PARAM_LIST.
	/// </summary>
	public partial class RFCHANN_PARAM_LIST : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1";
				Bind_Data1();
				Bind_Data2();
				Bind_Data3();
			}
			Datagrid1.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change1);
			Datagrid2.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change2);
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
			this.Datagrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Grid_Change1);
			this.Datagrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_ItemCommand);
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Grid_Change2);

		}
		#endregion
		void Grid_Change1(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid1.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			Bind_Data2();	
		}
		
		void Grid_Change2(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid2.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			Bind_Data3();	
		}

		private void Bind_Data1()
		{
			TXT_CH_PRM_CODE.Text = "";
			TXT_CH_PRM_NAME.Text = "";
			TXT_CH_PRM_REJECTDESC.Text = "";
			TXT_CH_VALUE1.Text = "";
			TXT_CH_VALUE2.Text = "";
			TXT_CH_VALUE3.Text = "";
		}

		private void Bind_Data2()
		{
			conn.QueryString = "select CHANN.CH_PRM_CODE,CHANN.CH_PRM_NAME,CHANN.CH_PRM_REJECTDESC," +
								"CHANNV.CH_VALUE1,CHANNV.CH_VALUE2,CHANNV.CH_VALUE3 " +
								"from dbo.RFCHANN_PARAM_LIST CHANN INNER JOIN RFCHANN_PARAM_VALUE CHANNV " +
								"ON CHANN.CH_PRM_CODE = CHANNV.CH_PRM_CODE";
			conn.ExecuteQuery();
			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			Datagrid1.DataSource = dt;
			Datagrid1.DataBind();
			
			for (int i = 0; i < Datagrid1.Items.Count; i++)
			{
				Datagrid1.Items[i].Cells[3].Text = GlobalTools.MoneyFormat(Datagrid1.Items[i].Cells[3].Text);
				Datagrid1.Items[i].Cells[4].Text = GlobalTools.MoneyFormat(Datagrid1.Items[i].Cells[4].Text);
			}

		}

		private void Bind_Data3()
		{
			conn.QueryString = "select DISTINCT CHANN.CH_PRM_CODE,CHANN.CH_PRM_NAME,CHANN.CH_PRM_REJECTDESC," +
				"CHANNV.CH_VALUE1,CHANNV.CH_VALUE2,CHANNV.CH_VALUE3,CHANN.PENDINGSTATUS " +
				"from dbo.PENDING_RFCHANN_PARAM_LIST CHANN INNER JOIN PENDING_RFCHANN_PARAM_VALUE CHANNV " +
				"ON CHANN.CH_PRM_CODE = CHANNV.CH_PRM_CODE";
			conn.ExecuteQuery();
			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			Datagrid2.DataSource = dt;
			Datagrid2.DataBind();

			for (int i = 0; i < Datagrid2.Items.Count; i++)
			{
				if (Datagrid2.Items[i].Cells[6].Text.Trim() == "0")
				{
					Datagrid2.Items[i].Cells[6].Text = "UPDATE";
				}
				else if (Datagrid2.Items[i].Cells[6].Text.Trim() == "1")
				{
					Datagrid2.Items[i].Cells[6].Text = "INSERT";
				}
				else if (Datagrid2.Items[i].Cells[6].Text.Trim() == "2")
				{
					Datagrid2.Items[i].Cells[6].Text = "DELETE";
				}
			} 
		}

		private void enable_kan(bool x)
		{
			TXT_CH_PRM_CODE.Enabled = x;
			//DDL_CH_PRM_NAME.Enabled = x;
			//DDL_AREAID.Enabled = x;
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			
			if (LBL_SAVEMODE.Text.Trim() == "0") 
			{
				conn.QueryString = "select * from PENDING_RFCHANN_PARAM_LIST" +
					" where CH_PRM_CODE = '" + TXT_CH_PRM_CODE.Text + "' AND PENDINGSTATUS = '0'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
				 
			
				conn.QueryString = "INSERT INTO PENDING_RFCHANN_PARAM_LIST (" +
					"CH_PRM_CODE," +
					"CH_PRM_NAME," +
					"CH_PRM_REJECTDESC," +
					"PENDINGSTATUS" +
					") VALUES ('"+
					TXT_CH_PRM_CODE.Text +"','"+
					TXT_CH_PRM_NAME.Text +"','"+
					TXT_CH_PRM_REJECTDESC.Text +"','" + LBL_SAVEMODE.Text +"')";
				conn.ExecuteNonQuery();

				conn.QueryString = "INSERT INTO PENDING_RFCHANN_PARAM_VALUE (" +
					"CH_PRM_CODE," +
					"CH_VALUE1," +
					"CH_VALUE2," +
					"CH_VALUE3," +
					"PENDINGSTATUS" +
					") VALUES ('"+
					TXT_CH_PRM_CODE.Text +"',"+
					GlobalTools.ConvertFloat(TXT_CH_VALUE1.Text) +","+
					GlobalTools.ConvertFloat(TXT_CH_VALUE2.Text) +",'"+
					GlobalTools.ConvertFloat(TXT_CH_VALUE3.Text) +"','" + LBL_SAVEMODE.Text + "')";
				conn.ExecuteNonQuery();

				Bind_Data3();
				clearEditBoxes();
				LBL_SAVEMODE.Text = "1";
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		private void clearEditBoxes()
		{
			TXT_CH_PRM_CODE.Text = "";
			TXT_CH_PRM_NAME.Text = "";
			TXT_CH_PRM_REJECTDESC.Text = "";
			TXT_CH_VALUE1.Text = "";
			TXT_CH_VALUE2.Text = "";
			TXT_CH_VALUE3.Text = "";		
			LBL_SAVEMODE.Text = "1";
		}

		private void Datagrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					try 
					{
						TXT_CH_PRM_CODE.Text = e.Item.Cells[0].Text.Trim().Replace("&nbsp;","");
						TXT_CH_PRM_NAME.Text = e.Item.Cells[1].Text.Trim().Replace("&nbsp;","");
						TXT_CH_PRM_REJECTDESC.Text = e.Item.Cells[2].Text.Trim().Replace("&nbsp;","");
						TXT_CH_VALUE1.Text = e.Item.Cells[3].Text.Trim().Replace("&nbsp;","");
						TXT_CH_VALUE2.Text = e.Item.Cells[4].Text.Trim().Replace("&nbsp;","");
						TXT_CH_VALUE3.Text = e.Item.Cells[5].Text.Trim().Replace("&nbsp;","");
						enable_kan(false);
					} 
					catch{}

					LBL_SAVEMODE.Text = "0";
					break;
					
				case "delete":
					LBL_SAVEMODE.Text = "2";
					
					conn.QueryString = "select * from PENDING_RFCHANN_PARAM_LIST" +
						" where CH_PRM_CODE = '" + e.Item.Cells[0].Text.Trim().Replace("&nbsp;","") + "' AND PENDINGSTATUS = '" + LBL_SAVEMODE.Text + "'";
					conn.ExecuteQuery();
				
					if (conn.GetRowCount() > 0) 
					{
						Tools.popMessage(this, "ID has already been used! Request canceled!");
						return;
					}

					conn.QueryString = "INSERT INTO PENDING_RFCHANN_PARAM_LIST (" +
						"CH_PRM_CODE," +
						"CH_PRM_NAME," +
						"CH_PRM_REJECTDESC," +
						"PENDINGSTATUS" +
						") VALUES ('"+
						e.Item.Cells[0].Text.Trim().Replace("&nbsp;","")+"','"+
						e.Item.Cells[1].Text.Trim().Replace("&nbsp;","")+"','"+
						e.Item.Cells[2].Text.Trim().Replace("&nbsp;","")+"','2')";
					conn.ExecuteNonQuery();

					conn.QueryString = "INSERT INTO PENDING_RFCHANN_PARAM_VALUE (" +
						"CH_PRM_CODE," +
						"CH_VALUE1," +
						"CH_VALUE2," +
						"CH_VALUE3," +
						"PENDINGSTATUS" +
						") VALUES ('"+
						e.Item.Cells[0].Text.Trim().Replace("&nbsp;","")+"',"+
						GlobalTools.ConvertFloat(e.Item.Cells[3].Text.Trim().Replace("&nbsp;",""))+","+
						GlobalTools.ConvertFloat(e.Item.Cells[4].Text.Trim().Replace("&nbsp;",""))+",'"+
						GlobalTools.ConvertFloat(e.Item.Cells[5].Text.Trim().Replace("&nbsp;",""))+"','2')";
					conn.ExecuteNonQuery();

					Bind_Data3();
					LBL_SAVEMODE.Text = "1";
					break;
				
				default:
					// Do nothing.
					break;
			} 	
		}

		private void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		private void Datagrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					try 
					{
						TXT_CH_PRM_CODE.Text = e.Item.Cells[0].Text.Trim().Replace("&nbsp;","");
						TXT_CH_PRM_NAME.Text = e.Item.Cells[1].Text.Trim().Replace("&nbsp;","");
						TXT_CH_PRM_REJECTDESC.Text = e.Item.Cells[2].Text.Trim().Replace("&nbsp;","");
						TXT_CH_VALUE1.Text = e.Item.Cells[3].Text.Trim().Replace("&nbsp;","");
						TXT_CH_VALUE2.Text = e.Item.Cells[4].Text.Trim().Replace("&nbsp;","");
						TXT_CH_VALUE3.Text = e.Item.Cells[5].Text.Trim().Replace("&nbsp;","");
						enable_kan(false);
					} 
					catch{}

					LBL_SAVEMODE.Text = "0";
					break;
					
				case "delete":
					conn.QueryString = "DELETE PENDING_RFCHANN_PARAM_LIST WHERE CH_PRM_CODE='"+e.Item.Cells[0].Text.Trim()+
						"' AND PENDINGSTATUS='"+e.Item.Cells[7].Text.Trim()+"'";
					conn.ExecuteNonQuery();

					conn.QueryString = "DELETE PENDING_RFCHANN_PARAM_VALUE WHERE CH_PRM_CODE='"+e.Item.Cells[0].Text.Trim()+
						"' AND PENDINGSTATUS='"+e.Item.Cells[7].Text.Trim()+"'";
					conn.ExecuteNonQuery();
					
					Bind_Data3();
					LBL_SAVEMODE.Text = "1";
					break;
				default:
					// Do nothing.
					break;
			}  
		}

		
	}
}
