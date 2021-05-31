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
using Microsoft.VisualBasic;

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for HolidayParam.
	/// </summary>
	public partial class HolidayParam : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected Connection conn;
		protected Connection conn2;
		protected string mid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn2 = new Connection((string) Session["ConnString"]);

			if(!IsPostBack)
			{				
				ViewData(); 
				addDate();
			}
			else
			{
				InitialCon();
			}

			//BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
		}
		private void addDate()
		{
			/*DDL_LIBURMONTH.Items.Add(new ListItem("--Select--", ""));
			DDL_LIBURDATE.Items.Add(new ListItem("--Select--", ""));
			DDL_LIBURYEAR.Items.Add(new ListItem("--Select--", ""));
			DDL_PEKANYEAR.Items.Add(new ListItem("--Select--",""));*/

			conn.QueryString = "select getdate() year ";
			conn.ExecuteQuery();

			string year = GlobalTools.FormatDate_Year(conn.GetFieldValue("year"));

			int years = int.Parse(year);
			for(int yy = (years - 21); yy <= (years + 2); yy++)
			{
				DDL_LIBURYEAR.Items.Add(new ListItem(yy.ToString(), yy.ToString()));
				DDL_PEKANYEAR.Items.Add(new ListItem(yy.ToString(), yy.ToString()));
			}			

			for(int date = 1; date <= 31; date++)
			{
				DDL_LIBURDATE.Items.Add(new ListItem(date.ToString(), date.ToString()));
			}
			
			for (int i=1;i<=12;i++) 
			{  
				DDL_LIBURMONTH.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));				
			}

			try
			{
				DDL_LIBURDATE.SelectedValue = "1";
				DDL_LIBURMONTH.SelectedValue = "1";	
				DDL_LIBURYEAR.SelectedValue = year;
				DDL_PEKANYEAR.SelectedValue = year;
			}
			catch{}			
			
		}
		private void ViewData()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");
			LBL_SAVEMODE.Text = "1"; 

			InitialCon();

			BindData1();
			BindData2();
			
		}
        
		private void BindData1()
		{
			conn.QueryString = "select convert(varchar,HL_DATE,103) HL_DATE, HL_DESC,HL_TYPE,HL_CODE from RFHOLIDAY where active='1' order by Convert(datetime,HL_DATE) DESC";
			conn.ExecuteQuery();

			if (conn.GetRowCount() != 0)
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				DG_HOLIDAY.DataSource = dt;

				try
				{
					DG_HOLIDAY.DataBind();
				}
				catch 
				{
					DG_HOLIDAY.CurrentPageIndex = DG_HOLIDAY.PageCount - 1;
					DG_HOLIDAY.DataBind();
				}				
			} 

			for(int i = 0; i < DG_HOLIDAY.Items.Count; i++)
			{
				DG_HOLIDAY.Items[i].Cells[0].Text = (i+1+(DG_HOLIDAY.CurrentPageIndex)*DG_HOLIDAY.PageSize).ToString();
				System.Web.UI.WebControls.LinkButton lbEdit = (System.Web.UI.WebControls.LinkButton) DG_HOLIDAY.Items[i].Cells[3].FindControl("LB_EDIT");
				System.Web.UI.WebControls.LinkButton lbDelete = (System.Web.UI.WebControls.LinkButton) DG_HOLIDAY.Items[i].Cells[3].FindControl("LB_DELETE");				
				System.Web.UI.WebControls.LinkButton lbDrop = (System.Web.UI.WebControls.LinkButton) DG_HOLIDAY.Items[i].Cells[3].FindControl("LBL_DROP");
				
				if (DG_HOLIDAY.Items[i].Cells[3].Text.Trim() == "01")
				{
					DG_HOLIDAY.Items[i].Cells[3].Text = "Libur Nasional";
				}
				else if (DG_HOLIDAY.Items[i].Cells[3].Text.Trim() == "02")
				{
					DG_HOLIDAY.Items[i].Cells[3].Text = "Akhir Pekan";
				}

			}

			conn.ClearData();
		}

		private void BindData2()
		{
			conn.QueryString = "select convert(varchar,HL_DATE,103) HL_DATE, HL_DESC,CH_STA,HL_TYPE,HL_CODE from TRFHOLIDAY order by Convert(datetime,HL_DATE) DESC";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG_THOLIDAY.DataSource = dt;

			try
			{
				DG_THOLIDAY.DataBind();
			}
			catch 
			{
				DG_THOLIDAY.CurrentPageIndex = DG_THOLIDAY.PageCount - 1;
				DG_THOLIDAY.DataBind();
			}

			for (int i = 0; i < DG_THOLIDAY.Items.Count; i++)
			{
				DG_THOLIDAY.Items[i].Cells[0].Text = (i+1+(DG_THOLIDAY.CurrentPageIndex)*DG_THOLIDAY.PageSize).ToString();
				if (DG_THOLIDAY.Items[i].Cells[4].Text.Trim() == "1")
				{
					DG_THOLIDAY.Items[i].Cells[4].Text = "INSERT";
				}
				else if (DG_THOLIDAY.Items[i].Cells[4].Text.Trim() == "2")
				{
					DG_THOLIDAY.Items[i].Cells[4].Text = "UPDATE";
				}
				else if (DG_THOLIDAY.Items[i].Cells[4].Text.Trim() == "3")
				{
					DG_THOLIDAY.Items[i].Cells[4].Text = "DELETE";
				}
				if (DG_THOLIDAY.Items[i].Cells[3].Text.Trim() == "01")
				{
					DG_THOLIDAY.Items[i].Cells[3].Text = "Libur Nasional";
				}
				else if (DG_THOLIDAY.Items[i].Cells[3].Text.Trim() == "02")
				{
					DG_THOLIDAY.Items[i].Cells[3].Text = "Akhir Pekan";
				}
			} 
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void saveLiburNasional()
		{
			conn.QueryString = "select getdate() year ";
			conn.ExecuteQuery();
			
			if(TXT_LIBURDESC.Text == "")
			{
				GlobalTools.popMessage(this,"Deskripsi Libur cannot be empty ...!");
				return;
			}

			string years = GlobalTools.FormatDate_Year(conn.GetFieldValue("year"));

						
				
			if(LBL_SAVEMODE.Text.Trim() == "1")
			{
				conn.QueryString = "select count(*) jml from trfholiday  where HL_DATE="+GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue);
				conn.ExecuteQuery();

				if(conn.GetFieldValue("jml").Trim() == "0")
				{
					conn.QueryString ="select max(HL_CODE)+1 max_code from RFHOLIDAY";
					conn.ExecuteQuery();
					TXT_HL_CODE.Text = conn.GetFieldValue("max_code");
					//cek hl_code di TRFHOLIDAY
					conn.QueryString = "select HL_CODE from TRFHOLIDAY";
					conn.ExecuteQuery();
					for ( int i=0; i<conn.GetRowCount(); i++)
					{
						if(conn.GetFieldValue(i,"HL_CODE")==TXT_HL_CODE.Text)
						{
							int hlcode = int.Parse(conn.GetFieldValue(i,"HL_CODE"))+1;
							TXT_HL_CODE.Text =hlcode.ToString();
						}
					}
					int hl_code = int.Parse(TXT_HL_CODE.Text);

					conn.QueryString = "select count(*) jml from rfholiday  where HL_DATE="+GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue);
					conn.ExecuteQuery();

					if(conn.GetFieldValue("jml").Trim() == "0")
					{
						//Response.Write(GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue));
 						conn.QueryString = "insert into trfholiday (HL_DATE,HL_DESC,CH_STA,HL_TYPE,HL_CODE) values ("+
							GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue)+",'"+
							TXT_LIBURDESC.Text+"','"+LBL_SAVEMODE.Text+"','"+TXT_HL_TYPE.Text+"','"+hl_code+"')";
						conn.ExecuteNonQuery();
					}
					else GlobalTools.popMessage(this,"Duplicate Data");
				}
				else GlobalTools.popMessage(this,"Duplicate Data");
						
			}				
			else if(LBL_SAVEMODE.Text.Trim() == "2" || LBL_SAVEMODE.Text.Trim() == "3")
			{
				conn.QueryString = "select count(*) jml from trfholiday  where HL_DATE="+GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue);
				conn.ExecuteQuery();

				if(conn.GetFieldValue("jml").Trim() == "0")
				{
					/*conn.QueryString = "select count(*) jml from rfholiday  where HL_DATE="+GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue);
					conn.ExecuteQuery();

					if(conn.GetFieldValue("jml").Trim() == "0")
					{*/
						conn.QueryString = "insert into trfholiday (HL_DATE,HL_DESC,CH_STA,HL_TYPE,HL_CODE) values ("+
							GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue)+",'"+
							TXT_LIBURDESC.Text+"','"+LBL_SAVEMODE.Text+"','"+TXT_HL_TYPE.Text+"','"+TXT_HL_CODE.Text.Trim()+"')";
						conn.ExecuteNonQuery();
					/*}
					else GlobalTools.popMessage(this,"Duplicate Data");*/
				}
				else
				{
					/*conn.QueryString = "select count(*) jml from rfholiday where  HL_DATE="+GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue);
					conn.ExecuteQuery();

					if(conn.GetFieldValue("jml").Trim() == "0")
					{*/
						conn.QueryString = "update trfholiday set HL_DATE ="+GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue)+
							" ,HL_DESC ='"+TXT_LIBURDESC.Text+"' ,CH_STA = '"+LBL_SAVEMODE.Text+"' where  HL_TYPE = '"+TXT_HL_TYPE.Text.Trim()+"' and HL_CODE = '"+TXT_HL_CODE.Text.Trim()+"'";
						conn.ExecuteNonQuery();
					/*}
					else GlobalTools.popMessage(this,"Duplicate Data");*/
				}
			}
			/*else if(LBL_SAVEMODE.Text.Trim() == "3")
			{
				conn.QueryString = "select count(*) jml from trfholiday  where CH_STA = '3' and HL_DATE="+GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue);
				conn.ExecuteQuery();

				if(conn.GetFieldValue("jml").Trim() == "0")
				{
					conn.QueryString = "select count(*) jml from rfholiday  HL_DATE="+GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue);
					conn.ExecuteQuery();

					if(conn.GetFieldValue("jml").Trim() == "0")
					{
						conn.QueryString = "insert into trfholiday (HL_DATE,HL_DESC,CH_STA,HL_TYPE,HL_CODE) values ("+
							GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue)+",'"+
							TXT_LIBURDESC.Text+"','"+LBL_SAVEMODE.Text+"','"+TXT_HL_TYPE.Text+"','"+TXT_HL_CODE.Text.Trim()+"')";
						conn.ExecuteNonQuery();
					}
					else GlobalTools.popMessage(this,"Duplicate Data");
				}
				else
				{
					conn.QueryString = "select count(*) jml from rfholiday  where HL_DATE="+GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue);
					conn.ExecuteQuery();

					if(conn.GetFieldValue("jml").Trim() == "0")
					{
						conn.QueryString = "update trfholiday set HL_DATE ="+GlobalTools.ToSQLDate(DDL_LIBURDATE.SelectedValue,DDL_LIBURMONTH.SelectedValue,DDL_LIBURYEAR.SelectedValue)+
							" ,HL_DESC ='"+TXT_LIBURDESC.Text+"' ,CH_STA = '"+LBL_SAVEMODE.Text+"' where  (CH_STA = '2' or CH_STA = '3') and HL_TYPE = '"+TXT_HL_TYPE.Text.Trim()+"' and HL_CODE = '"+TXT_HL_CODE.Text.Trim()+"'";
						conn.ExecuteNonQuery();
					}
					else GlobalTools.popMessage(this,"Duplicate Data");
				}
			}*/

			try
			{
				DDL_LIBURDATE.SelectedValue = "1";
				DDL_LIBURMONTH.SelectedValue = "1";					
				DDL_LIBURYEAR.SelectedValue = years;			
			}
			catch{}					
		    TXT_LIBURDESC.Text = "";
			TXT_HL_CODE.Text = "";

			LBL_SAVEMODE.Text = "1";
			TXT_HL_TYPE.Text = "01";				
			BindData1();			
			BindData2();
		}
		private void saveAkhirPekan()
		{
			
			bool flags = true;
			//string action = "";

			conn.QueryString = "select getdate() year ";
			conn.ExecuteQuery();

			string years = GlobalTools.FormatDate_Year(conn.GetFieldValue("year"));

			string year = DDL_PEKANYEAR.SelectedValue.Trim();				
				

			if(flags == true)
			{
				for(int bulan = 1; bulan <= 12; bulan++)
				{
					for(int hari = 1; hari <= 31; hari++)
					{
						int tahun = int.Parse(DDL_PEKANYEAR.SelectedValue);						

						if(LBL_SAVEMODE.Text.Trim() == "3")
						{
							tahun = int.Parse(TXT_HL_DATE_LAMA.Text);
							year = TXT_HL_DATE_LAMA.Text;
						}
						

						try
						{
							string day = new DateTime(tahun,bulan,hari).DayOfWeek.ToString();																
							if((day.Trim() == "Saturday" && CB_HARI.SelectedValue.Trim() == "Sabtu") || 
								(day.Trim() == "Sunday" && CB_HARI.SelectedValue.Trim() == "Minggu"))
							{
								if(day.Trim() == "Saturday") day = "Sabtu";
								else if(day.Trim() == "Sunday") day = "Minggu";

								string days = Convert.ToString(hari);
								string month = Convert.ToString(bulan);

								if(LBL_SAVEMODE.Text.Trim() == "1")
								{
									conn.QueryString = "select count(*) jml from trfholiday  where HL_DATE="+GlobalTools.ToSQLDate(days,month,year);
									conn.ExecuteQuery();

									if(conn.GetFieldValue("jml").Trim() == "0")
									{
										conn.QueryString = "select count(*) jml from rfholiday  where HL_DATE="+GlobalTools.ToSQLDate(days,month,year);
										conn.ExecuteQuery();

										if(conn.GetFieldValue("jml").Trim() == "0")
										{
											conn.QueryString = "insert into trfholiday (HL_DATE,HL_DESC,CH_STA,HL_TYPE) values ("+
												GlobalTools.ToSQLDate(days,month,year)+",'"+day+"','"+LBL_SAVEMODE.Text+"','"+TXT_HL_TYPE.Text.Trim()+"')";
											conn.ExecuteNonQuery();	
										}

									}																										
								}								
							}
						}
						catch{continue;}
					}
				}
			}
			try
			{
				DDL_PEKANYEAR.SelectedValue = years;
			}
			catch{} 
   			TXT_HL_DATE_LAMA.Text = "";
																					
				 
			LBL_SAVEMODE.Text = "1";							
			BindData1();			
			BindData2();
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
			this.DG_HOLIDAY.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG_HOLIDAY_ItemCommand);
			this.DG_HOLIDAY.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_HOLIDAY_PageIndexChanged);
			this.DG_THOLIDAY.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG_THOLIDAY_ItemCommand);
			this.DG_THOLIDAY.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_THOLIDAY_PageIndexChanged);

		}
		#endregion

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "select getdate() year ";
			conn.ExecuteQuery();

			string year = GlobalTools.FormatDate_Year(conn.GetFieldValue("year"));

			if(RB_LIBURNAS.Checked == true)
			{
				try
				{
					DDL_LIBURDATE.SelectedValue = "1";
					DDL_LIBURMONTH.SelectedValue = "1";					
					DDL_LIBURYEAR.SelectedValue = year;
					TXT_LIBURDESC.Text = "";
				}
				catch{}
			}
			else if(RB_AHIRPEKAN.Checked == true)
			{
				try
				{
					DDL_PEKANYEAR.SelectedValue = year;
				}
				catch{}
			}
            LBL_SAVEMODE.Text = "1";  
			BindData1();
			BindData2();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if(RB_LIBURNAS.Checked == true)	saveLiburNasional();						
			else if(RB_AHIRPEKAN.Checked == true) saveAkhirPekan();
		}

		protected void RB_LIBURNAS_CheckedChanged(object sender, System.EventArgs e)
		{
			DDL_PEKANYEAR.Enabled = false;
			CB_HARI.Enabled = false;
			DDL_LIBURDATE.Enabled = true;
			DDL_LIBURMONTH.Enabled = true;
			DDL_LIBURYEAR.Enabled = true;
			TXT_LIBURDESC.Enabled = true;
			BindData1();
			BindData2();
			conn.QueryString = "select getdate() year ";
			conn.ExecuteQuery();

			string years = GlobalTools.FormatDate_Year(conn.GetFieldValue("year"));

			try
			{
				DDL_PEKANYEAR.SelectedValue = years;
			}
			catch{} 
			TXT_HL_DATE_LAMA.Text = "";
			TXT_HL_TYPE.Text = "01";
		}

		protected void RB_AHIRPEKAN_CheckedChanged(object sender, System.EventArgs e)
		{
			DDL_LIBURDATE.Enabled = false;
			DDL_LIBURMONTH.Enabled = false;
			DDL_LIBURYEAR.Enabled = false;
			TXT_LIBURDESC.Enabled = false;
			DDL_PEKANYEAR.Enabled = true;
			CB_HARI.Enabled = true;
			conn.QueryString = "select getdate() year ";
			conn.ExecuteQuery();

			string years = GlobalTools.FormatDate_Year(conn.GetFieldValue("year"));

			try
			{
				DDL_LIBURDATE.SelectedValue = "1";
				DDL_LIBURMONTH.SelectedValue = "1";					
				DDL_LIBURYEAR.SelectedValue = years;			
			}
			catch{}					
			TXT_LIBURDESC.Text = "";
			TXT_HL_CODE.Text = "";

			LBL_SAVEMODE.Text = "1";
			BindData1();
			BindData2();
			TXT_HL_TYPE.Text = "02";
		
		}

		private void DG_HOLIDAY_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_HOLIDAY.CurrentPageIndex = e.NewPageIndex;
			BindData1();
			BindData2();
		}

		private void DG_THOLIDAY_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_THOLIDAY.CurrentPageIndex = e.NewPageIndex;
			BindData1();
			BindData2();			
		}

		private void DG_HOLIDAY_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
						try
						{
							DDL_LIBURDATE.SelectedValue = GlobalTools.FormatDate_Day(e.Item.Cells[1].Text);
							DDL_LIBURMONTH.SelectedValue = GlobalTools.FormatDate_Month(e.Item.Cells[1].Text);
							DDL_LIBURYEAR.SelectedValue = GlobalTools.FormatDate_Year(e.Item.Cells[1].Text);
						}
						catch{}					
						TXT_LIBURDESC.Text = e.Item.Cells[2].Text;
						RB_LIBURNAS.Checked = true;
						RB_AHIRPEKAN.Checked = false;
						DDL_PEKANYEAR.Enabled = false;
						CB_HARI.Enabled = false;
						DDL_LIBURDATE.Enabled = true;
						DDL_LIBURMONTH.Enabled = true;
						DDL_LIBURYEAR.Enabled = true;
						TXT_LIBURDESC.Enabled = true;	
						TXT_HL_TYPE.Text = cleansText(e.Item.Cells[5].Text);
						TXT_HL_CODE.Text = cleansText(e.Item.Cells[6].Text.Trim());
						TXT_HL_DATE_LAMA.Text = cleansText(e.Item.Cells[1].Text.Trim());
					
					
					
					BindData1();
					BindData2();
					LBL_SAVEMODE.Text = "2";
					break;	
				
				case "delete":
					try
					{
						DDL_LIBURDATE.SelectedValue = GlobalTools.FormatDate_Day(e.Item.Cells[1].Text);
						DDL_LIBURMONTH.SelectedValue = GlobalTools.FormatDate_Month(e.Item.Cells[1].Text);
						DDL_LIBURYEAR.SelectedValue = GlobalTools.FormatDate_Year(e.Item.Cells[1].Text);
					}
					catch{}					
					TXT_LIBURDESC.Text = e.Item.Cells[2].Text;
					RB_LIBURNAS.Checked = true;
					RB_AHIRPEKAN.Checked = false;
					DDL_PEKANYEAR.Enabled = false;
					CB_HARI.Enabled = false;
					DDL_LIBURDATE.Enabled = true;
					DDL_LIBURMONTH.Enabled = true;
					DDL_LIBURYEAR.Enabled = true;
					TXT_LIBURDESC.Enabled = true;	
					TXT_HL_TYPE.Text = cleansText(e.Item.Cells[5].Text);
					TXT_HL_CODE.Text = cleansText(e.Item.Cells[6].Text.Trim());
					TXT_HL_DATE_LAMA.Text = cleansText(e.Item.Cells[1].Text.Trim());
					
					
					
					BindData1();
					BindData2();
					LBL_SAVEMODE.Text = "3";
					saveLiburNasional();
					conn.QueryString = "select getdate() year ";
					conn.ExecuteQuery();

					string years = GlobalTools.FormatDate_Year(conn.GetFieldValue("year"));
					try
					{
						DDL_LIBURDATE.SelectedValue = "1";
						DDL_LIBURMONTH.SelectedValue = "1";					
						DDL_LIBURYEAR.SelectedValue = years;			
					}
					catch{}					
					TXT_LIBURDESC.Text = "";
					TXT_HL_CODE.Text = "";

					LBL_SAVEMODE.Text = "1";							
					BindData1();			
					BindData2();
					break;	
				
			}
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void DG_THOLIDAY_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
		
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40"); 
		}
	}
}
