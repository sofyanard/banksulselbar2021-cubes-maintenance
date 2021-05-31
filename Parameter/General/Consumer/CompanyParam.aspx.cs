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
	/// Summary description for CompanyParam.
	/// </summary>
	public partial class CompanyParam : System.Web.UI.Page
	{
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn2,conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			if (!IsPostBack) 
			{
				LBL_SAVEMODE.Text = "1"; 
				if (Request.QueryString["mod"] != null )
				{
					try
					{
						RDB_MODULE.SelectedValue = Request.QueryString["mod"];
					} 
					catch 
					{
						RDB_MODULE.SelectedValue = "1";
					}
				} 
				else
					RDB_MODULE.SelectedValue = "1";
				ViewData();
			}
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");		
			
		}

		private void SetDBConn2(string mid)
		{
			conn.QueryString = "select * from RFMODULE where MODULEID = '"+mid+"'";
			conn.ExecuteQuery();
			
			conn2 = new Connection("Data Source=" + conn.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn.GetFieldValue("DB_NAMA") + ";uid=" +conn.GetFieldValue("DB_LOGINID")+ ";pwd=" + conn.GetFieldValue("DB_LOGINPWD") + ";Pooling=true");
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
				
			dt.Columns.Add(new DataColumn("CODE"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("CD_SIBS"));
			
			DataRow dr;
			
			if(LBL_TABNAME.Text == "CHANNELS")
			{
				for(int i = 0; i < conn2.GetDataTable().Rows.Count; i++) 
				{
					dr = dt.NewRow();
					dr[0] = conn2.GetFieldValue(i,0);
					dr[1] = conn2.GetFieldValue(i,1);
					dr[2] = conn2.GetFieldValue(i,"CD_SIBS");
					dt.Rows.Add(dr);
				}		
			}
			else
			{
				for(int i = 0; i < conn2.GetDataTable().Rows.Count; i++) 
				{
					dr = dt.NewRow();
					dr[0] = conn2.GetFieldValue(i,0);
					dr[1] = conn2.GetFieldValue(i,1);
					dr[2] = "&nbsp;";
					dt.Rows.Add(dr);
				}
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
				conn.QueryString = "select * from pending_rfcomptype WHERE PENDINGSTATUS in ('1','2','3')";
			}
			else if(mode == "40")
			{
				conn.QueryString = "select * from pending_rfcomptype WHERE PENDINGSTATUS in ('4','5','6')";
			}
			
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			
			dt.Columns.Add(new DataColumn("CODE"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("CD_SIBS"));
			dt.Columns.Add(new DataColumn("STATUS"));
			dt.Columns.Add(new DataColumn("CH_STA"));			

			DataRow dr;
			
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,"CD_SIBS");
				dr[3] = getPendingStatus(conn.GetFieldValue(i,"PENDINGSTATUS"));
				dr[4] = conn.GetFieldValue(i,"PENDINGSTATUS");
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

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ClearEditBoxes()
		{
			TXT_CP_CODE.Text = "";
			TXT_CP_DESC.Text = "";
			
			if(LBL_STAT.Text == "40")
			{
				TXT_SBC.Text = "";
			}

			TXT_CP_CODE.Enabled = true;

			LBL_SAVEMODE.Text = "1";
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../CommonParam.aspx?mc=99020201");		
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;
			string sta = "";

			if((LBL_STAT.Text == "01") && (TXT_CP_CODE.Text.Length != 2))
			{
				GlobalTools.popMessage(this,"Code must 2 digits for Module SME!");
				return;
			}

			SetDBConn2(LBL_STAT.Text);  

			if((LBL_STAT.Text == "01") && (LBL_SAVEMODE.Text == "2"))
				sta = "2";
			else if((LBL_STAT.Text == "40") && (LBL_SAVEMODE.Text == "2"))
				sta = "5";
			else if((LBL_STAT.Text == "01") && (LBL_SAVEMODE.Text == "1"))
				sta = "1";
			else if((LBL_STAT.Text == "40") && (LBL_SAVEMODE.Text == "1"))
				sta = "4";

			conn2.QueryString = "SELECT * FROM "+LBL_TABNAME.Text+" WHERE "+LBL_CODE.Text+" = '"+TXT_CP_CODE.Text+"'";
			conn2.ExecuteQuery();

			hit = conn2.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text == "3"))
			{
				conn.QueryString = "UPDATE pending_rfcomptype SET COMPTYPEDESC = "+GlobalTools.ConvertNull(TXT_CP_DESC.Text)+", CD_SIBS = "+GlobalTools.ConvertNull(TXT_SBC.Text)+" "+
					"WHERE COMPTYPEID = '"+TXT_CP_CODE.Text+"' AND PENDINGSTATUS = '"+sta+"'"; 		
				conn.ExecuteQuery();	

				ClearEditBoxes();
			}
			else if((hit != 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "INSERT INTO pending_rfcomptype (COMPTYPEID, COMPTYPEDESC, CD_SIBS, ACTIVE, PENDINGSTATUS) "+ 
					"VALUES('"+TXT_CP_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_CP_DESC.Text)+", "+GlobalTools.ConvertNull(TXT_SBC.Text)+", '1', '"+sta+"')";
				conn.ExecuteQuery();
 
				ClearEditBoxes();	
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1")) 
			{
				try
				{
					conn.QueryString = "INSERT INTO pending_rfcomptype (COMPTYPEID, COMPTYPEDESC, CD_SIBS, ACTIVE, PENDINGSTATUS) "+ 
						"VALUES('"+TXT_CP_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_CP_DESC.Text)+", "+GlobalTools.ConvertNull(TXT_SBC.Text)+", '1', '"+sta+"')";
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

		private void ViewData()
		{
			/* If moduleid & the table name changes, must fix this */
			if(RDB_MODULE.SelectedValue == "1")
			{
				LBL_TABNAME.Text = "RFCOMPTYPE"; 
				LBL_CODE.Text = "COMPTYPEID"; 
				LBL_STAT.Text = "01";

				TR_SIBS.Visible = false;
				
				DG1.Columns[2].Visible = false;  
				DG2.Columns[2].Visible = false;  
 
				ClearEditBoxes(); 
				BindData2("01");
			}
			else if(RDB_MODULE.SelectedValue == "2")
			{
				LBL_TABNAME.Text = "RFCOMPANY";
				LBL_CODE.Text = "CP_CODE"; 
				LBL_STAT.Text = "40";

				TR_SIBS.Visible = true;

				DG1.Columns[2].Visible = true;  
				DG2.Columns[2].Visible = true;  
  
				ClearEditBoxes();
				BindData2("40");
			} 
			else if (RDB_MODULE.SelectedValue == "3")
			{
				Response.Redirect("../CC/CompTypeParam.aspx");
			}

			SetDBConn2(LBL_STAT.Text);

			BindData1();			
		
		}

		protected void RDB_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewData();
		}

		private void DG1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG1.CurrentPageIndex = e.NewPageIndex;

			SetDBConn2(LBL_STAT.Text);
			BindData1(); 	
		}

		private void DG1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code, desc, sbc; 
			string pend_sta = "";

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[0].Text.Trim();
					desc = cleansText(e.Item.Cells[1].Text);
					sbc = cleansText(e.Item.Cells[2].Text.Trim());

					if(LBL_STAT.Text == "01")
						pend_sta = "3";
					else if(LBL_STAT.Text == "40")
						pend_sta = "6";
  
					conn.QueryString = "SELECT * FROM pending_rfcomptype WHERE COMPTYPEID = '"+code+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						
						conn.QueryString = "INSERT INTO pending_rfcomptype (COMPTYPEID, COMPTYPEDESC, CD_SIBS, ACTIVE, PENDINGSTATUS) "+ 
							"VALUES('"+code+"', "+GlobalTools.ConvertNull(desc)+", "+GlobalTools.ConvertNull(sbc)+", '1', '"+pend_sta+"')";

						conn.ExecuteQuery();
						
						BindData2(LBL_STAT.Text);
					}

					BindData2(LBL_STAT.Text);
					break;
				case "edit":
					TXT_CP_CODE.Enabled = false;
					TXT_CP_CODE.Text = e.Item.Cells[0].Text;
					TXT_CP_DESC.Text = cleansText(e.Item.Cells[1].Text);

					if(LBL_STAT.Text == "40")
					{
						TXT_SBC.Text = cleansText(e.Item.Cells[2].Text);
					}

					conn.QueryString = "SELECT * FROM pending_rfcomptype WHERE COMPTYPEID = '"+e.Item.Cells[0].Text+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0) 
						LBL_SAVEMODE.Text = "3";
					else
						LBL_SAVEMODE.Text = "2";

					conn.ClearData();
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

					conn.QueryString = "DELETE FROM pending_rfcomptype WHERE COMPTYPEID = '"+code+"'";
					conn.ExecuteQuery();
					
					BindData2(LBL_STAT.Text);
					break;
				case "edit":
					ch_sta = e.Item.Cells[4].Text.Trim();

					if(ch_sta == "3" || ch_sta == "6")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						TXT_CP_CODE.Enabled = false;
						TXT_CP_CODE.Text = e.Item.Cells[0].Text;
						TXT_CP_DESC.Text = cleansText(e.Item.Cells[1].Text);
						
						if(LBL_STAT.Text == "40")
						{
							TXT_SBC.Text = cleansText(e.Item.Cells[2].Text);
						}
   
						LBL_SAVEMODE.Text = "3";		
					}
					break;
			}	
		}
	}
}
