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
	/// Summary description for JabatanParam.
	/// </summary>
	public partial class JabatanParam : System.Web.UI.Page
	{
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn2,conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			if (!IsPostBack) 
			{
				LBL_SAVEMODE.Text = "1"; 
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");	
			
		}

		private void SetDBConn2(string mid)
		{
			conn.QueryString = "select * from RFMODULE where MODULEID = '"+mid+"'";
			conn.ExecuteQuery();
			
			conn2 = new Connection("Data Source=" + conn.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn.GetFieldValue("DB_NAMA") + ";uid=" + conn.GetFieldValue("DB_LOGINID") + ";pwd=" + conn.GetFieldValue("DB_LOGINPWD") + ";Pooling=true");
		}

		private string getPendingStatus(string saveMode) 
		{
			string status = "";			
			switch (saveMode)
			{
				case "1": case "4": 
					status = "INSERT";
					break;
				case "2": case "5":
					status = "UPDATE";
					break;
				case "3": case "6":
					status = "DELETE";
					break;
				default:
					status = "";
					break;
			}
			return status;
		}

		private void BindData1()
		{
			conn2.QueryString = "select * from "+LBL_TABNAME.Text+" where ACTIVE = '1'";
			conn2.ExecuteQuery();

			DataTable dt = new DataTable();	
				
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			
			DataRow dr;
			
			for(int i = 0; i < conn2.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn2.GetFieldValue(i,0);
				dr[1] = conn2.GetFieldValue(i,1);
				dt.Rows.Add(dr);
			}

			DG1.DataSource = new DataView(dt);
			
			try 
			{
				DG1.DataBind();
			} 
			catch 
			{
				DG1.CurrentPageIndex = DG1.PageCount - 1;
				DG1.DataBind();
			}
		}

		private void BindData2(string mode) 
		{
			if(mode == "01")
			{
				conn.QueryString = "select * from PENDING_RFJABATAN WHERE PENDINGSTATUS IN ('1','2','3')";
			}
			else if(mode == "40")
			{
				conn.QueryString = "select * from PENDING_RFJABATAN WHERE PENDINGSTATUS IN ('4','5','6')";
			}
			
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("STATUS"));
			dt.Columns.Add(new DataColumn("CH_STA"));			

			DataRow dr;
			
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = getPendingStatus(conn.GetFieldValue(i,"PENDINGSTATUS"));
				dr[3] = conn.GetFieldValue(i,"PENDINGSTATUS");
				dt.Rows.Add(dr);
			}			

			DG2.DataSource = new DataView(dt);
			
			try 
			{
				DG2.DataBind();
			}
			catch 
			{
				DG2.CurrentPageIndex = DG2.PageCount - 1;
				DG2.DataBind();
			}
		}

		private void ClearEditBoxes()
		{
			TXT_CODE.Text = "";
			TXT_DESC.Text = "";
 
			TXT_CODE.Enabled = true;
			LBL_SAVEMODE.Text = "1";
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
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
			this.DG1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG1_ItemCommand);
			this.DG1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG1_PageIndexChanged);
			this.DG2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG2_ItemCommand);
			this.DG2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG2_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;
			string sta = "";

			SetDBConn2(LBL_STAT.Text); 

			if((LBL_STAT.Text == "01") && (LBL_SAVEMODE.Text == "2"))
				sta = "2";
			else if((LBL_STAT.Text == "40") && (LBL_SAVEMODE.Text == "2"))
				sta = "5";
			else if((LBL_STAT.Text == "01") && (LBL_SAVEMODE.Text == "1"))
				sta = "1";
			else if((LBL_STAT.Text == "40") && (LBL_SAVEMODE.Text == "1"))
				sta = "4";

			conn2.QueryString = "SELECT * FROM RFJABATAN WHERE JB_CODE = '"+TXT_CODE.Text+"'";
			conn2.ExecuteQuery();

			hit = conn2.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text == "3"))
			{
				conn.QueryString = "UPDATE PENDING_RFJABATAN SET JB_DESC = "+GlobalTools.ConvertNull(TXT_DESC.Text)+" "+
					"where JB_CODE = '"+TXT_CODE.Text+"'";  					
				conn.ExecuteQuery();	

				ClearEditBoxes();
			}
			else if((hit != 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "INSERT INTO PENDING_RFJABATAN VALUES('"+TXT_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_DESC.Text)+", '"+sta+"')";
				conn.ExecuteQuery();
 
				ClearEditBoxes();	
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1")) 
			{
				try
				{
					conn.QueryString = "INSERT INTO PENDING_RFJABATAN VALUES('"+TXT_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_DESC.Text)+", '"+sta+"')";
					conn.ExecuteQuery();
				}
				catch
				{
					GlobalTools.popMessage(this,"Cannot insert same code, request canceled!");  
				}
 
				ClearEditBoxes();
			}
			else if((hit != 0) && (LBL_SAVEMODE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
				return;
			}
 
			conn.ClearData();
 	
			BindData2(LBL_STAT.Text);

			LBL_SAVEMODE.Text = "1";			
		}

		protected void RDB_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			/* If moduleid & the table name changes, must fix this */
			if(RDB_MODULE.SelectedValue == "1")
			{
				LBL_TABNAME.Text = "RFJABATAN"; 
				LBL_STAT.Text = "01";
 
				BindData2("01");
			}
			else if(RDB_MODULE.SelectedValue == "2")
			{
				LBL_TABNAME.Text = "RFJABATAN";
				LBL_STAT.Text = "40";
 
				BindData2("40");
			}

			SetDBConn2(LBL_STAT.Text);

			BindData1();
		}

		private void DG1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG1.CurrentPageIndex = e.NewPageIndex;
 
			SetDBConn2(LBL_STAT.Text);
			BindData1();
		}

		private void DG1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string pend_sta = "";

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					TXT_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);

					conn.QueryString = "SELECT * FROM PENDING_RFJABATAN WHERE JB_CODE = '"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
						LBL_SAVEMODE.Text = "3";	
					else
						LBL_SAVEMODE.Text = "2";
	
					conn.ClearData(); 
 
					TXT_CODE.Enabled = false;
					break;

				case "delete":
					string code = e.Item.Cells[0].Text.Trim();
					string desc = cleansText(e.Item.Cells[1].Text);

					if(LBL_STAT.Text == "01")
						pend_sta = "3";
					else if(LBL_STAT.Text == "40")
						pend_sta = "6";

					conn.QueryString = "SELECT * FROM PENDING_RFJABATAN WHERE JB_CODE = '"+code+"' AND PENDINGSTATUS = '"+pend_sta+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "INSERT INTO PENDING_RFJABATAN VALUES('"+code+"', "+GlobalTools.ConvertNull(desc)+", '"+pend_sta+"')";
						conn.ExecuteQuery();
 
						BindData2(LBL_STAT.Text);
					}
					break;
				default :
					break;
			}	
		}

		private void DG2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG2.CurrentPageIndex = e.NewPageIndex;

			BindData2(LBL_STAT.Text);			
		}

		private void DG2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code, ch_sta = ""; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM PENDING_RFJABATAN WHERE JB_CODE = '"+code+"'";
					conn.ExecuteQuery();
					
					BindData2(LBL_STAT.Text);
					break;
				case "edit":
					ch_sta = e.Item.Cells[3].Text.Trim();

					if(ch_sta == "3" || ch_sta == "6")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						TXT_CODE.Text = e.Item.Cells[0].Text.Trim();
						TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
 
						TXT_CODE.Enabled = false;
						LBL_SAVEMODE.Text = "3";		
					}
					break;
			}						
		
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../CommonParam.aspx?mc=99020201"); 		
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}
	}
}
