using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Remoting;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DMS.DBConnection;
using DMS.CuBESCore;
using Excel;
using Microsoft.VisualBasic;
using DataTable = System.Data.DataTable;

namespace CuBES_Maintenance.Parameter.General
{
	/// <summary>
	/// Summary description for HolidayParam.
	/// hardcode: dayCountry = 'IDR'
	/// </summary>
	public partial class HolidayParam : Page
	{
		protected Connection conn, connsme, conncc, conncons;
		protected string mid;
	
		protected void Page_Load(object sender, EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection((string) Session["ConnString"]);

			//GlobalTools.popMessage(this,"tes");

			InitialCon();
			
			if(!IsPostBack)
			{	
				
				ViewData(); 
				//addDate();
				DDL_LIBURMONTH.Items.Add(new ListItem("- PILIH -", ""));

				for (int i = 1; i <= 12; i++)
				{
					DDL_LIBURMONTH.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
				}

			}

		}

        private void InitialCon()
        {
            //			connsme = new Connection ("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp;Pooling=true");
            //			conncc = new Connection ("Data Source=10.123.13.18;Initial Catalog=LOSCC2;uid=sa;pwd=dmscorp;Pooling=true");
            //			conncons = new Connection ("Data Source=10.123.13.18;Initial Catalog=LOSCONSUMER-UAT;uid=sa;pwd=dmscorp;Pooling=true");


            string DB_NAMA, DB_IP, DB_LOGINID, DB_LOGINPWD;
            //SME Conn
            conn.QueryString = "select * from RFMODULE where MODULEID='01'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                DB_NAMA = conn.GetFieldValue("DB_NAMA");
                DB_IP = conn.GetFieldValue("DB_IP");
                DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
                DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
                connsme = new Connection("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
            }
            else connsme = null;
            conn.ClearData();

            //Credit Card Conn
            conn.QueryString = "select * from RFMODULE where MODULEID='20'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                DB_NAMA = conn.GetFieldValue("DB_NAMA");
                DB_IP = conn.GetFieldValue("DB_IP");
                DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
                DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
                conncc = new Connection("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
            }
            else conncc = null;
            conn.ClearData();

            //Consumer
            conn.QueryString = "select * from RFMODULE where MODULEID='40'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                DB_NAMA = conn.GetFieldValue("DB_NAMA");
                DB_IP = conn.GetFieldValue("DB_IP");
                DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
                DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
                conncons = new Connection("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
            }
            else conncons = null;
            conn.ClearData();
        }

		private void ViewData()
		{
			BindData1();
			BindData2();
		}		

        //load grid
		private void BindData1()
		{
           /* // CONS
            if (conncons != null)
            {
                conncons.QueryString = "select convert(varchar,HL_DATE,103) HL_DATE1, HL_DESC from RFHOLIDAY ORDER BY HL_DATE DESC ";
                try
                {	//GlobalTools.popMessage(this,"Masuk ke CONS1");
                    conncons.ExecuteQuery();

                    DataTable dt = new DataTable();
                    dt = conncons.GetDataTable().Copy();
                    DG_HOLIDAY.DataSource = dt;
                    conncons.ClearData();
                }
                catch
                {

                    //SME
                    if (connsme != null)
                    {
                        connsme.QueryString = "select convert(varchar,DAY_DATE,103) HL_DATE1, DAY_DESC HL_DESC from RFHOLIDAY ORDER BY DAY_DATE DESC";
                        try
                        {	//GlobalTools.popMessage(this,"Masuk ke SME1");
                            connsme.ExecuteQuery();

                            DataTable dt = new DataTable();
                            dt = connsme.GetDataTable().Copy();
                            DG_HOLIDAY.DataSource = dt;
                            connsme.ClearData();
                        }
                        catch
                        {
                            //CC
                            if (conncc != null)
                            {
                                conncc.QueryString = "select convert(varchar,LB_DATE,103) HL_DATE1,LB_REMARK HL_DESC from LIBUR ORDER BY LB_DATE DESC ";
                                try
                                {	//GlobalTools.popMessage(this,"Masuk ke CC1");
                                    conncc.ExecuteQuery();

                                    DataTable dt = new DataTable();
                                    dt = conncc.GetDataTable().Copy();
                                    DG_HOLIDAY.DataSource = dt;
                                    conncc.ClearData();
                                }
                                catch
                                {
                                    GlobalTools.popMessage(this, "Tidak Ada Koneksi");
                                }
                            }
                        }
                    }
                }
            } */

            //if (connsme != null)
            //{
            //    connsme.QueryString =
            //        "select convert(varchar,DAY_DATE,103) HL_DATE1, DAY_DESC HL_DESC from RFHOLIDAY ORDER BY DAY_DATE DESC";
            //    try
            //    {
            //        //GlobalTools.popMessage(this,"Masuk ke SME1");
            //        connsme.ExecuteQuery();

            //        DataTable dt = new DataTable();
            //        dt = connsme.GetDataTable().Copy();
            //        DG_HOLIDAY.DataSource = dt;
            //        connsme.ClearData();
            //    }
            //    catch (Exception m)
            //    {
            //    }
            //}

            // CONS
            if (conncons != null)
            {
                conncons.QueryString = "select convert(varchar,HL_DATE,103) HL_DATE1, HL_DESC from RFHOLIDAY ORDER BY HL_DATE DESC ";
                try
                {	//GlobalTools.popMessage(this,"Masuk ke CONS1");
                    conncons.ExecuteQuery();

                    DataTable dt = new DataTable();
                    dt = conncons.GetDataTable().Copy();
                    DG_HOLIDAY.DataSource = dt;
                    conncons.ClearData();
                }
                catch(Exception m)
                {
                    GlobalTools.popMessage(this, "Tidak Ada Koneksi CONS");
                }
            }

            //SME
            if (connsme != null)
            {
                connsme.QueryString = "select convert(varchar,DAY_DATE,103) HL_DATE1, DAY_DESC HL_DESC from RFHOLIDAY ORDER BY DAY_DATE DESC";
                try
                {	//GlobalTools.popMessage(this,"Masuk ke SME1");
                    connsme.ExecuteQuery();

                    DataTable dt = new DataTable();
                    dt = connsme.GetDataTable().Copy();
                    DG_HOLIDAY.DataSource = dt;
                    connsme.ClearData();
                }
                catch (Exception m)
                {
                    GlobalTools.popMessage(this, "Tidak Ada Koneksi SME");
                }
            }

            //CC
            if (conncc != null)
            {
                conncc.QueryString = "select convert(varchar,LB_DATE,103) HL_DATE1,LB_REMARK HL_DESC from LIBUR ORDER BY LB_DATE DESC ";
                try
                {	//GlobalTools.popMessage(this,"Masuk ke CC1");
                    conncc.ExecuteQuery();

                    DataTable dt = new DataTable();
                    dt = conncc.GetDataTable().Copy();
                    DG_HOLIDAY.DataSource = dt;
                    conncc.ClearData();
                }
                catch(Exception m)
                {
                   GlobalTools.popMessage(this, "Tidak Ada Koneksi CC");
                }
            }

		    try
			{
				DG_HOLIDAY.DataBind();
			}
			catch 
			{
				DG_HOLIDAY.CurrentPageIndex = DG_HOLIDAY.PageCount - 1;
				DG_HOLIDAY.DataBind();
			}				
			

			for(int i = 0; i < DG_HOLIDAY.Items.Count; i++)
			{
				DG_HOLIDAY.Items[i].Cells[0].Text = (i+1+(DG_HOLIDAY.CurrentPageIndex)*DG_HOLIDAY.PageSize).ToString();
				LinkButton lbEdit = (LinkButton) DG_HOLIDAY.Items[i].Cells[3].FindControl("LB_EDIT");
				LinkButton lbDelete = (LinkButton) DG_HOLIDAY.Items[i].Cells[3].FindControl("LB_DELETE");				
				//System.Web.UI.WebControls.LinkButton lbDrop = (System.Web.UI.WebControls.LinkButton) DG_HOLIDAY.Items[i].Cells[3].FindControl("LBL_DROP");
								
			}
			
						

		}

		private void BindData2()
		{

            ////CONS
            //if (conncons != null)
            //{
            //    conncons.QueryString = "select convert(varchar,HL_DATE,103) HL_DATE1, HL_DESC,CH_STA from TRFHOLIDAY ORDER BY HL_DATE DESC ";
            //    try
            //    {
            //        conncons.ExecuteQuery();

            //        DataTable dt = new DataTable();
            //        dt = conncons.GetDataTable().Copy();
            //        DG_THOLIDAY.DataSource = dt;
            //    }
            //    catch
            //    {
            //        //SME
            //        if (connsme != null)
            //        {
            //            connsme.QueryString = "select convert(varchar,DAY_DATE,103) HL_DATE1, DAY_DESC HL_DESC, PENDINGSTATUS CH_STA from PENDING_RFHOLIDAY ORDER BY DAY_DATE DESC";
            //            try
            //            {
            //                connsme.ExecuteQuery();

            //                DataTable dt = new DataTable();
            //                dt = connsme.GetDataTable().Copy();
            //                DG_THOLIDAY.DataSource = dt;
            //            }
            //            catch
            //            {
            //                //CC
            //                if (conncc != null)
            //                {
            //                    conncc.QueryString = "select convert(varchar,LB_DATE,103) HL_DATE1,LB_REMARK HL_DESC,PENDING_STATUS CH_STA from PENDING_CC_LIBUR ORDER BY LB_DATE DESC";
            //                    try
            //                    {
            //                        conncc.ExecuteQuery();

            //                        DataTable dt = new DataTable();
            //                        dt = conncc.GetDataTable().Copy();
            //                        DG_THOLIDAY.DataSource = dt;
            //                    }
            //                    catch
            //                    {
            //                        GlobalTools.popMessage(this,"Tidak Ada Koneksi");
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //CONS
            if (conncons != null)
            {
                conncons.QueryString = "select convert(varchar,HL_DATE,103) HL_DATE1, HL_DESC,CH_STA from TRFHOLIDAY ORDER BY HL_DATE DESC ";
                try
                {
                    conncons.ExecuteQuery();

                    DataTable dt = new DataTable();
                    dt = conncons.GetDataTable().Copy();
                    DG_THOLIDAY.DataSource = dt;
                }
                catch
                {
                }
            }

            //SME
            if (connsme != null)
            {
                connsme.QueryString = "select convert(varchar,DAY_DATE,103) HL_DATE1, DAY_DESC HL_DESC, PENDINGSTATUS CH_STA from PENDING_RFHOLIDAY ORDER BY DAY_DATE DESC";
                try
                {
                    connsme.ExecuteQuery();

                    DataTable dt = new DataTable();
                    dt = connsme.GetDataTable().Copy();
                    DG_THOLIDAY.DataSource = dt;
                }
                catch
                {
                }
            }

            //CC
            if (conncc != null)
            {
                conncc.QueryString = "select convert(varchar,LB_DATE,103) HL_DATE1,LB_REMARK HL_DESC,PENDING_STATUS CH_STA from PENDING_CC_LIBUR ORDER BY LB_DATE DESC";
                try
                {
                    conncc.ExecuteQuery();

                    DataTable dt = new DataTable();
                    dt = conncc.GetDataTable().Copy();
                    DG_THOLIDAY.DataSource = dt;
                }
                catch
                {
                    GlobalTools.popMessage(this, "Tidak Ada Koneksi");
                }
            }

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
				if (DG_THOLIDAY.Items[i].Cells[3].Text.Trim() == "1")
				{
					DG_THOLIDAY.Items[i].Cells[3].Text = "INSERT";
				}
				else if (DG_THOLIDAY.Items[i].Cells[3].Text.Trim() == "2")
				{
					DG_THOLIDAY.Items[i].Cells[3].Text = "UPDATE";
				}
				else if (DG_THOLIDAY.Items[i].Cells[3].Text.Trim() == "3")
				{
					DG_THOLIDAY.Items[i].Cells[3].Text = "DELETE";
				}
			}
				
			
		}

        //save
		private void saveLiburNasional()
		{ 
			TXT_HL_TYPE.Text = "01"; //libur nasional
			conn.QueryString = "select getdate() year ";
			conn.ExecuteQuery();

			string years = GlobalTools.FormatDate_Year(conn.GetFieldValue("year"));
				
			if(LBL_SAVEMODE.Text.Trim() == "1") //insert
			{
				// save buat Consumer
				if (conncons != null)
				{
					conncons.QueryString = "select count(*) jml from trfholiday  where HL_DATE="+GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_LIBURMONTH.SelectedValue,TXT_TAHUN.Text.Trim());
					try
					{
						conncons.ExecuteQuery();

						if(conncons.GetFieldValue("jml").Trim() == "0")
						{
							conncons.QueryString = "select count(*) jml from rfholiday  where HL_DATE="+GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_LIBURMONTH.SelectedValue,TXT_TAHUN.Text.Trim());
							conncons.ExecuteQuery();

							if(conncons.GetFieldValue("jml").Trim() == "0")
							{
                                //string dayDesc = Encode(TXT_LIBURDESC.Text);
								conncons.QueryString = "EXEC PARAM_GENERAL_RFHOLIDAY_MAKER '1', "+
									GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_LIBURMONTH.SelectedValue,TXT_TAHUN.Text.Trim())+",'"+
									TXT_LIBURDESC.Text+"','"+LBL_SAVEMODE.Text+"','"+TXT_HL_TYPE.Text+"',''";
								conncons.ExecuteNonQuery();
							}
							else GlobalTools.popMessage(this,"Duplicate Data");
						}
						else GlobalTools.popMessage(this,"Duplicate Data");
					}
					catch {}
				}

				// save buat CC
				if (conncc != null)
				{
					conncc.QueryString = "select count(*) jml from PENDING_CC_LIBUR where LB_DATE="+GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_LIBURMONTH.SelectedValue,TXT_TAHUN.Text.Trim());
					try
					{
						conncc.ExecuteQuery();

						if(conncc.GetFieldValue("jml").Trim() == "0")
						{
							conncc.QueryString = "select count(*) jml from libur where LB_DATE="+GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_LIBURMONTH.SelectedValue,TXT_TAHUN.Text.Trim());
							conncc.ExecuteQuery();

							if(conncc.GetFieldValue("jml").Trim() == "0")
							{
                                //string dayDesc = Encode(TXT_LIBURDESC.Text);
								conncc.QueryString = "EXEC PARAM_GENERAL_LIBUR_MAKER '1', "+
									GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_LIBURMONTH.SelectedValue,TXT_TAHUN.Text.Trim())+",'"+
									TXT_LIBURDESC.Text+"','1','','"+LBL_SAVEMODE.Text+"'";
								conncc.ExecuteNonQuery();
							}
							else GlobalTools.popMessage(this,"Duplicate Data");
						}
						else GlobalTools.popMessage(this,"Duplicate Data");
					}
					catch {}
				}
		
				// save buat SME
				if (connsme != null)
				{
					connsme.QueryString = "select count(*) jml from pending_rfholiday  where DAY_DATE="+GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_LIBURMONTH.SelectedValue,TXT_TAHUN.Text.Trim());
					try
					{
						connsme.ExecuteQuery();

						if(connsme.GetFieldValue("jml").Trim() == "0")
						{
							connsme.QueryString = "select count(*) jml from rfholiday  where DAY_DATE="+GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_LIBURMONTH.SelectedValue,TXT_TAHUN.Text.Trim());
							connsme.ExecuteQuery();

							if(connsme.GetFieldValue("jml").Trim() == "0")
							{
                                string dayDesc = EncodingDayDesc();
                                
								connsme.QueryString =   "EXEC PARAM_GENERAL_RFHOLIDAY_MAKER '" +
								                        "1', "+ //mode
									                    GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_LIBURMONTH.SelectedValue,TXT_TAHUN.Text.Trim())+",'"+ //day_date
                                                        dayDesc + "','" + //day_desc
								                        "1','" + //active
								                        "IDR','" + //day_country
                                                        LBL_SAVEMODE.Text + "' "; //pending status
								connsme.ExecuteNonQuery();
							}
							else GlobalTools.popMessage(this,"Duplicate Data in ");
						}
						else GlobalTools.popMessage(this,"Duplicate Data");
					}
					catch {}
				}
						
			}	
			
			else if(LBL_SAVEMODE.Text.Trim() == "2" || LBL_SAVEMODE.Text.Trim() == "3") //update || delete
			{
				// insert buat consumer
				if (conncons != null)
				{
					conncons.QueryString = "EXEC PARAM_GENERAL_RFHOLIDAY_MAKER '', "+
						GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_LIBURMONTH.SelectedValue,TXT_TAHUN.Text.Trim())+",'"+
						TXT_LIBURDESC.Text+"','"+LBL_SAVEMODE.Text+"','"+TXT_HL_TYPE.Text+"','"+TXT_HL_CODE.Text.Trim()+"' ";
					try
					{
						conncons.ExecuteNonQuery();
					}
					catch {}
				}

				// insert buat cc
				if (conncc != null)
				{
					conncc.QueryString = "EXEC PARAM_GENERAL_LIBUR_MAKER '', "+
						GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_LIBURMONTH.SelectedValue,TXT_TAHUN.Text.Trim())+",'"+
						TXT_LIBURDESC.Text+"','1','','"+LBL_SAVEMODE.Text+"'";
					try
					{
						conncc.ExecuteNonQuery();
					}
					catch {}
				}

				// insert buat sme
				if (connsme != null)
				{
                    string dayDesc = EncodingDayDesc();
					connsme.QueryString =   "EXEC PARAM_GENERAL_RFHOLIDAY_MAKER '" +
					                        LBL_SAVEMODE.Text+"', "+
						                    GlobalTools.ToSQLDate(TXT_HARI.Text.Trim(),DDL_LIBURMONTH.SelectedValue,TXT_TAHUN.Text.Trim())+",'"+
						                    dayDesc+"','1','IDR','"+LBL_SAVEMODE.Text+"' ";
					try
					{
						connsme.ExecuteNonQuery();
					}
					catch {}
				}			
				
			}

			
			TXT_HL_CODE.Text = "";

			LBL_SAVEMODE.Text = "1";
			//TXT_HL_TYPE.Text ="01";
			BindData1();			
			BindData2();
			
		}

		private void saveAkhirPekan() //otomatis setahun save sabtu minggu + tanggalya, g ngaruh klo d input tanggalnya
		{  //GlobalTools.popMessage(this,"save akhir pekan");
			
			TXT_HL_TYPE.Text = "02"; //akhir pekan
			const bool flags = true;
			string action = "";

			conn.QueryString = "select getdate() year ";
			conn.ExecuteQuery();

			string years = GlobalTools.FormatDate_Year(conn.GetFieldValue("year"));

			string year = TXT_TAHUN.Text.Trim();				
				
			if(flags == true)
			{
				for(int bulan = 1; bulan <= 12; bulan++)
				{
					for(int hari = 1; hari <= 31; hari++)
					{
						int tahun = int.Parse(TXT_TAHUN.Text);						

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
								switch (day.Trim())
								{
								    case "Saturday":
								        day = "Sabtu";
								        day = Encode("", day, "");
								        break;
								    case "Sunday":
								        day = "Minggu";
                                        day = Encode("", day, "");
								        break;
								}

                                //daydesc = Encode(day);

								string days = Convert.ToString(hari);
								string month = Convert.ToString(bulan);

								if(LBL_SAVEMODE.Text.Trim() == "1")
								{
									// buat consumer
									if (conncons != null)
									{
										conncons.QueryString = "select count(*) jml from trfholiday  where HL_DATE="+GlobalTools.ToSQLDate(days,month,year);
										try
										{
											conncons.ExecuteQuery();

											if(conncons.GetFieldValue("jml").Trim() == "0")
											{	
												conncons.QueryString = "select count(*) jml from rfholiday  where HL_DATE="+GlobalTools.ToSQLDate(days,month,year);
												conncons.ExecuteQuery();

												if(conncons.GetFieldValue("jml").Trim() == "0")
												{
													conncons.QueryString = "EXEC PARAM_GENERAL_RFHOLIDAY_MAKER '1', "+
														GlobalTools.ToSQLDate(days,month,year)+",'"+day+"','"+LBL_SAVEMODE.Text+"','"+TXT_HL_TYPE.Text.Trim()+"',''";
													conncons.ExecuteNonQuery();	
												}
											}
										}
										catch {}
									}

									// buat cc
									if (conncc != null)
									{
										conncc.QueryString = "select count(*) jml from pending_cc_libur where LB_DATE="+GlobalTools.ToSQLDate(days,month,year);
										try
										{
											conncc.ExecuteQuery();

											if (conncc.GetFieldValue("jml").Trim() == "0")
											{
												conncc.QueryString = "select count(*) jml from libur where LB_DATE="+GlobalTools.ToSQLDate(days,month,year);
												conncc.ExecuteQuery();

												if(conncc.GetFieldValue("jml").Trim() == "0")
												{
													conncc.QueryString = "EXEC PARAM_GENERAL_LIBUR_MAKER '1', "+
														GlobalTools.ToSQLDate(days,month,year)+",'"+day+"','1','','"+LBL_SAVEMODE.Text+"' ";
													conncc.ExecuteNonQuery();	
												}
											}
										}
										catch {}
									}

									// buat sme
									if(connsme != null)
									{
										connsme.QueryString = "select count(*) jml from pending_rfholiday  where DAY_DATE="+GlobalTools.ToSQLDate(days,month,year);
										try
										{
											connsme.ExecuteQuery();

											if(connsme.GetFieldValue("jml").Trim() == "0")
											{
												connsme.QueryString = "select count(*) jml from rfholiday  where DAY_DATE="+GlobalTools.ToSQLDate(days,month,year);
												connsme.ExecuteQuery();

												if(connsme.GetFieldValue("jml").Trim() == "0")
												{
													connsme.QueryString = "EXEC PARAM_GENERAL_RFHOLIDAY_MAKER '1', "+
														GlobalTools.ToSQLDate(days,month,year)+",'"+day+"','1','IDR','"+LBL_SAVEMODE.Text+"'";
													connsme.ExecuteNonQuery();	
												}
											}
										}
										catch {}
									
									}																										
								}								
							}
						}
						catch{continue;}
					}
				}
			}
			
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
			this.DG_THOLIDAY.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_THOLIDAY_PageIndexChanged);

		}
		#endregion

		
        //button
        protected void BTN_SAVE_Click(object sender, EventArgs e)
		{
			//________________________________________________
			//	VALIDASI TANGGAL APLIKASI
			//________________________________________________

			if (!GlobalTools.isDateValid(TXT_HARI.Text, DDL_LIBURMONTH.SelectedValue, TXT_TAHUN.Text)) 
			{
				GlobalTools.popMessage(this, "Tanggal tidak valid!");
				return;
			}
				
			if( CheckDes.Checked || CB_HARI.SelectedValue.Trim()== "Lain-lain" || LBL_EDIT.Text == "2" )	
			{
				saveLiburNasional();						
			}
			else  saveAkhirPekan();

			ClearControls();
			
		}

		protected void BTN_CANCEL_Click(object sender, EventArgs e)
		{
            ////string daydesc = "libur lah (sabtu - libur nasional)";
            //string daydesc = "libur lah (sabtu)", day = "", flag = "";
            ////Decode(daydesc);

            //daydesc = TXT_LIBURDESC.Text.Trim();
            //switch (CB_HARI.SelectedValue.ToLower().Trim())
            //{
            //    case "sabtu":
            //        day = "Sabtu";
            //        break;
            //    case "minggu":
            //        day = "Minggu";
            //        break;
            //    default:
            //        day = "Other";
            //        break;
            //}

            //flag = CheckDes.Checked ? "Libur Nasional" : "";

            //GlobalTools.popMessage(this, EncodingDayDesc());
            //Decode(Encode(daydesc, day, flag));

            //daydesc = "PUBLIC HOLIDAY";
            //var d = Decode(daydesc);
            //string m = "cancel_click desc: [" + d.Item1 + "] day: [" + d.Item2 + "] flag: [" + d.Item3 + "]";
            //GlobalTools.popMessage(this, m);

			conn.QueryString = "select getdate() year ";
			conn.ExecuteQuery();

			string year = GlobalTools.FormatDate_Year(conn.GetFieldValue("year"));

			ClearControls();
			BindData1();
			BindData2();
		}

        //other
	    protected void ClearControls()
	    {
            TXT_HARI.Text = "";
            DDL_LIBURMONTH.SelectedValue = "";
            TXT_TAHUN.Text = "";
            TXT_LIBURDESC.Text = "";
	        CB_HARI.SelectedValue = "Lain-lain";
            CB_HARI.Enabled = true;
	        CheckDes.Checked = false;
            CheckDes.Enabled = true;
	        TXT_LIBURDESC.Enabled = true;
            LBL_SAVEMODE.Text = "1";
            LBL_EDIT.Text = "1";
	    }

	    protected string EncodingDayDesc()
	    {
            string daydesc = "", day = "", flag = "";
            
            daydesc = TXT_LIBURDESC.Text.Trim();
            switch (CB_HARI.SelectedValue.ToLower().Trim())
            {
                case "sabtu":
                    day = "Sabtu";
                    break;
                case "minggu":
                    day = "Minggu";
                    break;
                default:
                    day = "Other";
                    break;
            }

            flag = CheckDes.Checked ? "Libur Nasional" : "";

            return Encode(daydesc, day, flag);
	    }

	    protected string Encode(string daydesc,string day,string flag)
	    {
	        string desc = "", temp="";
	        if (day.ToLower() == "other")
	        {
	            day = "Other";
	        }

	        if (flag!="")
	        {
	            desc = daydesc.Trim() + " (" + day + " - " + flag + ")";
	        }
	        else
	        {
	            desc = daydesc.Trim() + " (" + day + ")";
	        }
            //string m = "Result: [" + desc + "]";
            //GlobalTools.popMessage(this, m);
	        return desc;
	    }

	    protected Tuple<string, string, string> Decode(string daydesc)
	    {
	        if (daydesc == "&nbsp;")
	        {
	            daydesc = "";
	        }

	        string desc, day, flag = "no";

	        string[] temp = Regex.Split(daydesc, "\\(");

            //klo g ada '(' kosong gmn
            if (temp.Length < 2)
            {
                desc = temp[0];
                day = "Other";
            }
            else
            {

	            desc = temp[0].Trim();
	            string temp1 = temp[1];

	            temp = Regex.Split(temp1, "-");

	            if (temp.Length < 2)
	            {
	                //klog bs d split
	                day = Regex.Replace(temp[0], "\\)", "").Trim();
	            }
	            else
	            {
	                day = temp[0].Trim();
	                temp1 = temp[1];

	                flag = Regex.Replace(temp1, "\\)", "").Trim();
	            }

            }

            //string m = "desc: [" + desc + "] day: [" + day + "] flag: [" + flag + "]";
            //GlobalTools.popMessage(this,m);

            var tuple = new Tuple<string, string, string>(desc,day,flag);
            return tuple;
	    }

	    protected string ConvertStatus(string s)
	    {
	        string stat = "";
	        switch (s.ToLower())
	        {
	            case "insert":
	                stat = "1";
                    break;
                case "update":
                    stat = "2";
	                break;
                case "delete":
                    stat = "3";
                    break;
                case "undelete":
                    stat = "4";
	                break;
                default:

                    break;
	        }
	        return stat;
	    } //untuk pending status
	    //itemCommand
		private void DG_HOLIDAY_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
			        try
			        {
			            TXT_HARI.Text = GlobalTools.FormatDate_Day(e.Item.Cells[1].Text);
			            DDL_LIBURMONTH.SelectedValue = GlobalTools.FormatDate_Month(e.Item.Cells[1].Text);
			            TXT_TAHUN.Text = GlobalTools.FormatDate_Year(e.Item.Cells[1].Text);

			        }
			        catch (Exception m)
			        {
			            string ex = m.Message;
			            GlobalTools.popMessage(this,ex);
			        }
                    //------------------------------------
                    var d = Decode(e.Item.Cells[2].Text);
					TXT_LIBURDESC.Text = d.Item1; //daydesc

			        switch (d.Item2.ToLower())
			        {
			            case "sabtu":
                            CB_HARI.SelectedValue = "Sabtu";
                            break;
                        case "minggu":
                            CB_HARI.SelectedValue = "Minggu";
			                break;
			            default:
			                CB_HARI.SelectedValue = "Lain-lain";
			                break;
			        }
			        
					CB_HARI.Enabled = false;
					//CheckDes.Checked = true;
			        CheckDes.Checked = d.Item3 != "no";
			        CheckDes.Enabled = false;
                    //-----------------------------------
					
                    LBL_EDIT.Text = "2";
                    TXT_HARI.Enabled = true;        //tgl
					DDL_LIBURMONTH.Enabled = true;  //tgl
					TXT_TAHUN.Enabled = true;       //tgl
					//TXT_HL_TYPE.Text = e.Item.Cells[5].Text;
					//TXT_HL_CODE.Text = e.Item.Cells[6].Text.Trim();
					TXT_HL_DATE_LAMA.Text = e.Item.Cells[1].Text.Trim();
												
					BindData1();
					BindData2();
					LBL_SAVEMODE.Text = "2";
					break;	
				
				case "delete":
					try
					{
						TXT_HARI.Text = GlobalTools.FormatDate_Day(e.Item.Cells[1].Text);
						DDL_LIBURMONTH.SelectedValue = GlobalTools.FormatDate_Month(e.Item.Cells[1].Text);
						TXT_TAHUN.Text = GlobalTools.FormatDate_Year(e.Item.Cells[1].Text);
					}
                    catch (Exception m)
                    {
                        string ex = m.Message;
                        GlobalTools.popMessage(this, ex);
                    }		
		
					
					TXT_LIBURDESC.Text = e.Item.Cells[2].Text;
					CB_HARI.Enabled = true;
					TXT_HARI.Enabled = true;
					DDL_LIBURMONTH.Enabled = true;
					TXT_TAHUN.Enabled = true;
					TXT_LIBURDESC.Enabled = true;	
					CheckDes.Enabled = true;
					//TXT_HL_TYPE.Text = e.Item.Cells[5].Text;
					//TXT_HL_CODE.Text = e.Item.Cells[6].Text.Trim();
					TXT_HL_DATE_LAMA.Text = e.Item.Cells[1].Text.Trim();
															
					BindData1();
					BindData2();
					LBL_SAVEMODE.Text = "3";
					saveLiburNasional();
					conn.QueryString = "select getdate() year ";
					conn.ExecuteQuery();

					string years = GlobalTools.FormatDate_Year(conn.GetFieldValue("year"));
					try
					{
						TXT_HARI.Text="";
						DDL_LIBURMONTH.SelectedValue="";
						TXT_TAHUN.Text="";
						
					}
                    catch (Exception m)
                    {
                        string ex = m.Message;
                        GlobalTools.popMessage(this, ex);
                    }					
					TXT_LIBURDESC.Text = "";
					TXT_HL_CODE.Text = "";

					LBL_SAVEMODE.Text = "1";							
					BindData1();			
					BindData2();
					break;	
				
			}

		}

        protected void DG_THOLIDAY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (((LinkButton)e.CommandSource).CommandName)
            {
                case "edit":
                    try
			        {
			            TXT_HARI.Text = GlobalTools.FormatDate_Day(e.Item.Cells[1].Text);
			            DDL_LIBURMONTH.SelectedValue = GlobalTools.FormatDate_Month(e.Item.Cells[1].Text);
			            TXT_TAHUN.Text = GlobalTools.FormatDate_Year(e.Item.Cells[1].Text);

			        }
			        catch (Exception m)
			        {
			            string ex = m.Message;
			            GlobalTools.popMessage(this,ex);
			        }
                    //------------------------------------
                    var d = Decode(e.Item.Cells[2].Text);
					TXT_LIBURDESC.Text = d.Item1; //daydesc

			        switch (d.Item2.ToLower())
			        {
			            case "sabtu":
                            CB_HARI.SelectedValue = "Sabtu";
                            break;
                        case "minggu":
                            CB_HARI.SelectedValue = "Minggu";
			                break;
			            default:
			                CB_HARI.SelectedValue = "Lain-lain";
			                break;
			        }
			        
					CB_HARI.Enabled = true;
			        CheckDes.Checked = d.Item3 != "no";
			        CheckDes.Enabled = true;
                    //-----------------------------------
					
                    LBL_EDIT.Text = "2";
                    TXT_HARI.Enabled = true;        //tgl
					DDL_LIBURMONTH.Enabled = true;  //tgl
					TXT_TAHUN.Enabled = true;       //tgl
					TXT_HL_DATE_LAMA.Text = e.Item.Cells[1].Text.Trim();
												
					BindData1();
					BindData2();
					LBL_SAVEMODE.Text = "2";
                    break;
                case "delete":
                    if (connsme != null)
                    {
                        try
                        {
                            LBL_SAVEMODE.Text = "5";


                            connsme.QueryString = "exec PARAM_GENERAL_RFHOLIDAY_MAKER '" +
                                               LBL_SAVEMODE.Text.Trim() + "', '" + //mode
                                               GlobalTools.FormatDate(e.Item.Cells[1].Text) + "', '" + //day_date
                                               e.Item.Cells[2].Text.Trim() + "', '" + //day desc
                                               "1','" + //active
                                               "IDR','" + //day_country
                                               ConvertStatus(e.Item.Cells[3].Text.Trim()) + "'"; //pending_status
                            connsme.ExecuteQuery();

                            BindData2();
                            ClearControls();
                            //ReadOnlyControl(false);
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<!--" + ex.Message + "-->");
                            string errmsg = ex.Message.Replace("'", "");
                            if (errmsg.IndexOf("Last Query:") > 0)
                                errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
                            GlobalTools.popMessage(this, errmsg);
                        }
                    }
                    break;
            }
        }
       
        //pageIndexChanged
		private void DG_THOLIDAY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DG_THOLIDAY.CurrentPageIndex = e.NewPageIndex;
			BindData1();
			BindData2();		
		}

		private void DG_HOLIDAY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DG_HOLIDAY.CurrentPageIndex = e.NewPageIndex;
			BindData1();
			BindData2();
		}

        //itemDataBound
        protected void DG_THOLIDAY_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.AlternatingItem:
                case ListItemType.EditItem:
                case ListItemType.Item:
                case ListItemType.SelectedItem:
                    try
                    {
                        LinkButton lbedit = (LinkButton)e.Item.Cells[4].FindControl("LB_EDIT2");

                        if ((e.Item.Cells[3].Text == "3") || (e.Item.Cells[3].Text == "4"))
                        {
                            lbedit.Visible = false;
                        }
                        else
                        {
                            lbedit.Visible = true;
                        }
                    }
                    catch
                    {
                    }
                    break;
                case ListItemType.Footer:
                    break;
                default:
                    break;
            }
        }

        //autopostback
        protected void CB_HARI_SelectedIndexChanged(object sender, EventArgs e)
        {
            string index = CB_HARI.SelectedValue;
            if(LBL_EDIT.Text != "2"){
                switch (index.ToLower())
                {
                    case "sabtu":
                        TXT_LIBURDESC.Enabled = false;
                        break;
                    case "minggu":
                        TXT_LIBURDESC.Enabled = false;
                        break;
                    default:
                        TXT_LIBURDESC.Enabled = true;
                        break;
                }
            }
        }
        
        protected void CheckDes_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckDes.Checked)
            {
                TXT_LIBURDESC.Enabled = true;
            }
            else
            {
                TXT_LIBURDESC.Enabled = false;
            }

        }


	}
}
