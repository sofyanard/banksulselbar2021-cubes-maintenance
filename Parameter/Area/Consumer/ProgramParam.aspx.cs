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

namespace CuBES_Maintenance.Parameter.Area.Consumer
{
	/// <summary>
	/// Summary description for ProgramParam.
	/// </summary>
	public partial class ProgramParam : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);

			if(!IsPostBack)
			{
				ViewData(); 
			}
			else
			{
				InitialCon();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");	
			
		}

		private void ViewData()
		{
			string arid = (string) Session["AreaId"];
			string que = "";

			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			LBL_SAVE.Text = "1"; 

			InitialCon(); 

			if(arid == "2000")
			{
				que = "select AREA_ID, AREA_NAME from AREA where AREA_ID = '"+arid+"'";
			}
			else
			{
				que = "select AREA_ID, AREA_NAME from AREA";
			}
	
			GlobalTools.initDateForm(TXT_EXPDATE,DDL_EXPMONTH,TXT_EXPYEAR); 
			
			GlobalTools.fillRefList(DDL_BANK_PROGRAM,"select CMP_CODE, CMP_DESC from CAMPAIGN WHERE ACTIVE = '1'",false,conn);
			GlobalTools.fillRefList(DDL_AREA,que,false,conn);

			BindData1();
			BindData2();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");			
		}

		private void BindData1()
		{
			conn.QueryString = "select a.AREA_ID, a.PR_CODE, a.PR_DESC, a.MIN_PINJAM, "+ 
						"a.MAX_PINJAM, a.PR_EXPIREDATE, a.PR_SRCCODE, a.CMP_CODE, c.CMP_DESC "+
						"from PROGRAM a left join AREA b on a.AREA_ID = b.AREA_ID "+
						"left join CAMPAIGN c on a.CMP_CODE = c.CMP_CODE "+ 
						"where a.active='1' and  a.AREA_ID = '"+DDL_AREA.SelectedValue+"'";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_EXISTING.DataSource = dt;

			try
			{
				DGR_EXISTING.DataBind();
			}
			catch 
			{
				DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.PageCount - 1;
				DGR_EXISTING.DataBind();
			}

			conn.ClearData();
		}

		private void BindData2()
		{
			conn.QueryString = "select a.AREA_ID, a.PR_CODE, a.PR_DESC, a.MIN_PINJAM, "+ 
				"a.MAX_PINJAM, a.PR_EXPIREDATE, a.PR_SRCCODE, a.CMP_CODE, c.CMP_DESC, a.CH_STA, "+
				"STATUS = case a.CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TPROGRAM a left join AREA b on a.AREA_ID = b.AREA_ID "+
				"left join CAMPAIGN c on a.CMP_CODE = c.CMP_CODE";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_REQUEST.DataSource = dt;

			try
			{
				DGR_REQUEST.DataBind();
			}
			catch 
			{
				DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				DGR_REQUEST.DataBind();
			}
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private string cleansFloat(string tn)
		{
			if (tn.Trim() == "&nbsp;")
				tn = "0";
			return tn;
		}

		private void ClearEditBoxes()
		{
			DDL_BANK_PROGRAM.ClearSelection();  
			TXT_EXPDATE.Text = "";
			TXT_EXPYEAR.Text = "";
			TXT_MARK_SOURCE_CODE.Text = "";
			TXT_MAX_LOAN.Text = "";
			TXT_MIN_LOAN.Text = "";
			TXT_PROGRAM_NAME.Text = "";
 
			LBL_SAVE.Text = "1";
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
			this.DGR_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_ItemCommand);
			this.DGR_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string pcode = "";
			int hit = 0;

			conn.QueryString = "SELECT PR_CODE, AREA_ID FROM TPROGRAM WHERE AREA_ID = '"+DDL_AREA.SelectedValue+"' AND PR_CODE = '"+LBL_PRCODE.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVE.Text != "1"))
			{
				conn.QueryString = "UPDATE TPROGRAM SET CMP_CODE = '"+DDL_BANK_PROGRAM.SelectedValue+"', PR_DESC = "+GlobalTools.ConvertNull(TXT_PROGRAM_NAME.Text)+
							", MIN_PINJAM = "+GlobalTools.ConvertFloat(TXT_MIN_LOAN.Text)+", MAX_PINJAM = "+GlobalTools.ConvertFloat(TXT_MAX_LOAN.Text)+
							", PR_EXPIREDATE = "+GlobalTools.ToSQLDate(TXT_EXPDATE.Text,DDL_EXPMONTH.SelectedValue,TXT_EXPYEAR.Text)+
							", PR_SRCCODE = "+GlobalTools.ConvertNull(TXT_MARK_SOURCE_CODE.Text)+" WHERE PR_CODE = '"+LBL_PRCODE.Text+
							"' AND AREA_ID = '"+DDL_AREA.SelectedValue+"'";  					
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "2"))
			{
				conn.QueryString = "insert into TPROGRAM VALUES('"+LBL_PRCODE.Text+"','"+DDL_BANK_PROGRAM.SelectedValue+
					"','"+DDL_AREA.SelectedValue+"',"+GlobalTools.ConvertNull(TXT_PROGRAM_NAME.Text)+
					","+GlobalTools.ConvertFloat(TXT_MIN_LOAN.Text)+
					","+GlobalTools.ConvertFloat(TXT_MAX_LOAN.Text)+
					","+GlobalTools.ToSQLDate(TXT_EXPDATE.Text,DDL_EXPMONTH.SelectedValue,TXT_EXPYEAR.Text)+", NULL, 2, NULL, NULL"+ 
					","+GlobalTools.ConvertNull(TXT_MARK_SOURCE_CODE.Text)+")";						
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "1"))
			{
				conn.QueryString = "select isnull(max(convert(int,PR_CODE)),0)+1 MAXSEQ from PROGRAM";		
				conn.ExecuteQuery();

				pcode = conn.GetFieldValue("MAXSEQ"); 

				try
				{
					conn.QueryString = "insert into TPROGRAM VALUES('"+pcode+"','"+DDL_BANK_PROGRAM.SelectedValue+
						"','"+DDL_AREA.SelectedValue+"',"+GlobalTools.ConvertNull(TXT_PROGRAM_NAME.Text)+
						","+GlobalTools.ConvertFloat(TXT_MIN_LOAN.Text)+
						","+GlobalTools.ConvertFloat(TXT_MAX_LOAN.Text)+
						","+GlobalTools.ToSQLDate(TXT_EXPDATE.Text,DDL_EXPMONTH.SelectedValue,TXT_EXPYEAR.Text)+", NULL, 1, NULL, NULL"+ 
						","+GlobalTools.ConvertNull(TXT_MARK_SOURCE_CODE.Text)+")";						
					conn.ExecuteQuery();
				}
				catch
				{
					LBL_PRCODE.Text = "";
				}
 
				ClearEditBoxes();
			}
			else if((hit != 0) && (LBL_SAVE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
				return;
			}

			conn.ClearData();
 	
			BindData2();

			LBL_SAVE.Text = "1"; 
			LBL_PRCODE.Text = "";
		
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 	
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string aid, prcode; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					aid = e.Item.Cells[8].Text.Trim();
					prcode = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "SELECT PR_CODE, AREA_ID FROM TPROGRAM WHERE AREA_ID = '"+aid+"' AND PR_CODE = '"+prcode+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "insert into TPROGRAM VALUES('"+prcode+"','"+cleansText(e.Item.Cells[9].Text)+
                                           "','"+cleansText(e.Item.Cells[8].Text)+"','"+cleansText(e.Item.Cells[1].Text)+
										   "',"+GlobalTools.ConvertFloat(cleansFloat(e.Item.Cells[4].Text))+
										   ","+GlobalTools.ConvertFloat(cleansFloat(e.Item.Cells[5].Text))+
										   ","+GlobalTools.ToSQLDate(cleansText(e.Item.Cells[3].Text))+", NULL, 3, NULL, NULL"+ 
										   ",'"+cleansText(e.Item.Cells[6].Text)+"')";						
						conn.ExecuteQuery();
						
						BindData2();
					}
					break;

				case "edit":
					try
					{
						DDL_AREA.SelectedValue = e.Item.Cells[8].Text.Trim();   
					}
					catch{ }

					try
					{
						DDL_BANK_PROGRAM.SelectedValue = e.Item.Cells[9].Text.Trim();   
					}
					catch{ }

					TXT_MARK_SOURCE_CODE.Text = cleansText(e.Item.Cells[6].Text);
					TXT_MIN_LOAN.Text = cleansFloat(e.Item.Cells[4].Text);   
					TXT_MAX_LOAN.Text = cleansFloat(e.Item.Cells[5].Text);
					TXT_PROGRAM_NAME.Text = cleansText(e.Item.Cells[1].Text);

					TXT_EXPDATE.Text = GlobalTools.FormatDate_Day(cleansText(e.Item.Cells[3].Text));
					
					try
					{
						DDL_EXPMONTH.SelectedValue = GlobalTools.FormatDate_Month(cleansText(e.Item.Cells[3].Text));
					}
					catch{ }

					TXT_EXPYEAR.Text = GlobalTools.FormatDate_Year(cleansText(e.Item.Cells[3].Text));
					
					LBL_PRCODE.Text = e.Item.Cells[0].Text.Trim();   
					LBL_SAVE.Text = "2";		
					break;
			}
		
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code, aid; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					aid = cleansText(e.Item.Cells[10].Text);
					code = cleansText(e.Item.Cells[0].Text);

					conn.QueryString = "DELETE FROM TPROGRAM WHERE AREA_ID = '"+aid+"' AND PR_CODE = '"+code+"'";
					conn.ExecuteQuery();

					BindData2();
					break;
				case "edit":
					if(e.Item.Cells[8].Text.Trim() == "3")
					{
						LBL_SAVE.Text = "1";
					}
					else
					{
						try
						{
							DDL_AREA.SelectedValue = e.Item.Cells[10].Text.Trim();   
						}
						catch{ }

						try
						{
							DDL_BANK_PROGRAM.SelectedValue = e.Item.Cells[11].Text.Trim();   
						}
						catch{ }

						TXT_MARK_SOURCE_CODE.Text = cleansText(e.Item.Cells[6].Text);
						TXT_MIN_LOAN.Text = cleansFloat(e.Item.Cells[4].Text);   
						TXT_MAX_LOAN.Text = cleansFloat(e.Item.Cells[5].Text);
						TXT_PROGRAM_NAME.Text = cleansText(e.Item.Cells[1].Text);

						TXT_EXPDATE.Text = GlobalTools.FormatDate_Day(cleansText(e.Item.Cells[3].Text));
					
						try
						{
							DDL_EXPMONTH.SelectedValue = GlobalTools.FormatDate_Month(cleansText(e.Item.Cells[3].Text));
						}
						catch{ }

						TXT_EXPYEAR.Text = GlobalTools.FormatDate_Year(cleansText(e.Item.Cells[3].Text));
				
						LBL_PRCODE.Text = e.Item.Cells[0].Text.Trim();   
						LBL_SAVE.Text = "2";
					}
					break;
			}		
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		protected void DDL_AREA_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1(); 
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleID=40");
		}
	}
}
