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
	/// Summary description for RFCAFinstatement.
	/// </summary>
	public partial class RFCAFinstatement : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		protected string userid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
			userid = Session["UserID"].ToString();

		//conn = new Connection ("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp;Pooling=true");			

			if (!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1";
				Bind_Data1();
				//Bind_Data2();
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

		}
		#endregion
		
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

		private void Bind_Data1()
		{
			GlobalTools.fillRefList(DDL_PROGRAMID,"SELECT PROGRAMID,PROGRAMDESC FROM RFPROGRAM WHERE AREAID = '0000' AND ACTIVE = '1' ORDER BY PROGRAMDESC",false,conn);

			GlobalTools.fillRefList(DDL_AREAID,"SELECT distinct pr.areaid, ar.areaname FROM RFPROGRAM pr " +
													"left join rfarea ar on pr.areaid = ar.areaid " +
													"where ar.active = '1' ORDER BY ar.areaname ",false,conn);

			GlobalTools.fillRefList(DDL_NASABAHID,"select nasabahid,nasabahdesc from rfjenisnasabah ORDER BY nasabahdesc",false,conn);

			GlobalTools.fillRefList(DDL_LG_CODE,"SELECT LG_CODE,LG_DESC FROM RFCAFINLEGEND where active = 1 ORDER BY LG_DESC",false,conn);
		}
		
		private void Bind_Data2()
		{
			//conn.QueryString = "SELECT SEQ,PROGRAMID,AREAID,NASABAHID,LG_CODE FROM RFCAFINSTATEMENT order by programid, areaid,nasabahid";
			conn.QueryString = "SELECT * FROM VW_PARAM_GENERAL_RFCAFINSTATEMENT WHERE PROGRAMID = '"+DDL_PROGRAMID.SelectedValue+"' " +
								"order by PROGRAMDESC, areaname, nasabahdesc";
			conn.ExecuteQuery();
			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			Datagrid1.DataSource = dt;
			try
			{
				Datagrid1.DataBind();
			}
			catch
			{
				Datagrid1.CurrentPageIndex = 0;
				Datagrid1.DataBind();
			}
		}

		void Grid_Change1(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid1.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			Bind_Data2();	
		}
	
		private void Bind_Data3()
		{
			//conn.QueryString = "SELECT SEQ,PROGRAMID,AREAID,NASABAHID,LG_CODE,PENDINGSTATUS FROM PENDING_RFCAFINSTATEMENT order by programid, areaid,nasabahid";
			conn.QueryString = "SELECT * FROM VW_PARAM_GENERAL_PENDING_RFCAFINSTATEMENT " +
								"order by PROGRAMDESC, areaname, nasabahdesc";
			conn.ExecuteQuery();
			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			Datagrid2.DataSource = dt;
			try
			{
				Datagrid2.DataBind();
			}
			catch
			{
				Datagrid2.CurrentPageIndex = 0;
				Datagrid2.DataBind();
			}

//			for (int i = 0; i < Datagrid2.Items.Count; i++)
//			{
//				if (Datagrid2.Items[i].Cells[10].Text.Trim() == "0")
//				{
//					Datagrid2.Items[i].Cells[10].Text = "UPDATE";
//				}
//				else if (Datagrid2.Items[i].Cells[10].Text.Trim() == "1")
//				{
//					Datagrid2.Items[i].Cells[10].Text = "INSERT";
//				}
//				else if (Datagrid2.Items[i].Cells[10].Text.Trim() == "2")
//				{
//					Datagrid2.Items[i].Cells[10].Text = "DELETE";
//				}
//			} 
		}

		void Grid_Change2(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid2.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			Bind_Data3();	
		}
		
		private void clearEditBoxes()
		{
			DDL_PROGRAMID.SelectedValue = "";
			DDL_AREAID.SelectedValue = "";
			DDL_NASABAHID.SelectedValue = "";
			DDL_LG_CODE.SelectedValue = "";
					
			LBL_SAVEMODE.Text = "1";
			activatePostBackControls(true);
			Bind_Data2();
			Bind_Data3();
		}

		private void activatePostBackControls(bool mode)
		{
			//TXT_BRANCH_CODE.Enabled = mode;
		}

		private void enable_kan(bool x)
		{
			//TXT_SEQ.Enabled = x;
			DDL_PROGRAMID.Enabled = x;
			DDL_AREAID.Enabled = x;
			DDL_NASABAHID.Enabled = x;
		}
		
		private void Datagrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					
						TXT_SEQ.Text = e.Item.Cells[0].Text.Trim();
						try{DDL_PROGRAMID.SelectedValue = e.Item.Cells[1].Text.Trim();}
						catch{}
						try{DDL_AREAID.SelectedValue = e.Item.Cells[2].Text.Trim();}
						catch{}
						try{DDL_NASABAHID.SelectedValue = e.Item.Cells[3].Text.Trim();}
						catch{}
						try{DDL_LG_CODE.SelectedValue = e.Item.Cells[4].Text.Trim();}
						catch{}
						enable_kan(false);
					

					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					break;
					
				case "delete":
					LBL_SAVEMODE.Text = "2";
					conn.QueryString = "EXEC PARAM_GENERAL_RFCAFINSTATEMENT_MAKER 'INSERT','"+
										e.Item.Cells[0].Text.Trim()+"','"+
										e.Item.Cells[1].Text.Trim()+"','"+
										e.Item.Cells[2].Text.Trim()+"','"+
										e.Item.Cells[3].Text.Trim()+"','"+
										e.Item.Cells[4].Text.Trim().Replace("&nbsp;","")+"','2','"+userid+"' ";
					conn.ExecuteNonQuery();
					Bind_Data3();
					LBL_SAVEMODE.Text = "1";
					break;
				
				default:
					// Do nothing.
					break;
			} 
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
			enable_kan(true);
			TXT_SEQ.Text="";
		}
		
		private void cleansTextBox (TextBox tb)
		{
			if (tb.Text.Trim() == "&nbsp;")
				tb.Text = "";
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			// insert / edit harus dicek sdh ada ato belum
			
				conn.QueryString = "select * from RFCAFINSTATEMENT" +
					" where PROGRAMID = '" + DDL_PROGRAMID.SelectedValue + "' AND " +
					"AREAID = '" + DDL_AREAID.SelectedValue + "' AND NASABAHID = '" + DDL_NASABAHID.SelectedValue + "' AND " +
					"LG_CODE = '" + DDL_LG_CODE.SelectedValue + "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
				else
				{
					conn.QueryString = "select * from PENDING_RFCAFINSTATEMENT" +
						" where PROGRAMID = '" + DDL_PROGRAMID.SelectedValue + "' AND " +
						"AREAID = '" + DDL_AREAID.SelectedValue + "' AND NASABAHID = '" + DDL_NASABAHID.SelectedValue + "' AND " +
						"LG_CODE = '" + DDL_LG_CODE.SelectedValue + "'";
					conn.ExecuteQuery();
				
					if (conn.GetRowCount() > 0) 
					{
						Tools.popMessage(this, "ID has already been used! Request canceled!");
						return;
					}
				}	
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{	 

				conn.QueryString = "EXEC PARAM_GENERAL_RFCAFINSTATEMENT_MAKER 'INSERT',"+
					" '"+TXT_SEQ.Text + "','" + DDL_PROGRAMID.SelectedValue + "','" + DDL_AREAID.SelectedValue + "','" + DDL_NASABAHID.SelectedValue + 
					"','" + DDL_LG_CODE.SelectedValue + "','" + LBL_SAVEMODE.Text + "','"+userid+"' ";
				conn.ExecuteQuery();
			}
			else if (LBL_SAVEMODE.Text.Trim() == "0") 
			{
				conn.QueryString = "EXEC PARAM_GENERAL_RFCAFINSTATEMENT_MAKER 'UPDATE',"+
					" '"+TXT_SEQ.Text + "','" + DDL_PROGRAMID.SelectedValue + "','" + DDL_AREAID.SelectedValue + "','" + DDL_NASABAHID.SelectedValue + 
					"','" + DDL_LG_CODE.SelectedValue + "','" + LBL_SAVEMODE.Text + "','"+userid+"' ";
				conn.ExecuteQuery();
			}

			clearEditBoxes();
			//Bind_Data3();
			enable_kan(true);
			TXT_SEQ.Text="";
		}

		private void Datagrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					
						TXT_SEQ.Text = e.Item.Cells[0].Text.Trim();
						try{DDL_PROGRAMID.SelectedValue = e.Item.Cells[1].Text.Trim();}
						catch{}
						try{DDL_AREAID.SelectedValue = e.Item.Cells[2].Text.Trim();}
						catch{}
						try{DDL_NASABAHID.SelectedValue = e.Item.Cells[3].Text.Trim();}
						catch{}
						try{DDL_LG_CODE.SelectedValue = e.Item.Cells[4].Text.Trim();}
						catch{}
						enable_kan(false);
					

					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					break;
					
				case "delete":
					conn.QueryString = "EXEC PARAM_GENERAL_RFCAFINSTATEMENT_MAKER 'DELETE','"+e.Item.Cells[0].Text.Trim()+"', "+
										"'"+e.Item.Cells[1].Text.Trim()+"', "+
										"'"+e.Item.Cells[2].Text.Trim()+"', "+
										"'"+e.Item.Cells[3].Text.Trim()+"', "+
										"'"+e.Item.Cells[4].Text.Trim().Replace("&nbsp;","") +"', "+
										"'"+e.Item.Cells[5].Text.Trim()+"', '"+userid+"' ";
					conn.ExecuteNonQuery();
					Bind_Data3();
					LBL_SAVEMODE.Text = "1";
					break;
				default:
					// Do nothing.
					break;
			}  
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		protected void BTN_NEW_Click(object sender, System.EventArgs e)
		{
			int seqmax= 0 ;
			
			conn.QueryString = "select isnull(max(convert(int,seq)),0) seqmax from PENDING_RFCAFINSTATEMENT ";
			conn.ExecuteQuery();
			seqmax =int.Parse(conn.GetFieldValue("seqmax"));
		
			if (seqmax == 0)
			{		
				conn.QueryString = "select isnull(max(convert(int,seq)),0) seqmax from RFCAFINSTATEMENT ";
				conn.ExecuteQuery();
		
				seqmax =int.Parse(conn.GetFieldValue("seqmax"));
			}

			seqmax = seqmax +1;

			TXT_SEQ.Text = seqmax.ToString() ;
			clearEditBoxes();
			enable_kan(true);
		}

		protected void BTN_VIEW_Click(object sender, System.EventArgs e)
		{
			Bind_Data2();
			Bind_Data3();
		}
	}
}
