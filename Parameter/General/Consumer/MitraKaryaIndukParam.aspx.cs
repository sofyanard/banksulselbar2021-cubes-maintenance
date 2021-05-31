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
using Excel;
using System.Threading;
using System.Globalization;

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for MitraKaryaIndukParam.
	/// </summary>
	public partial class MitraKaryaIndukParam : System.Web.UI.Page
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

				//add by sofyan 2007-05-28, LOS Consumer Enh 4
				GlobalTools.initDateForm(TXT_EXP_DAY,DDL_EXP_MONTH,TXT_EXP_YEAR);
				dtCpRating();
			}
			else
			{
				InitialCon();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
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
			CalcTotal();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private string createseq(string nb)
		{
			string temp = "";

			if(nb.Length == 1)
				temp = "0000" + nb;
			else if(nb.Length == 2)
				temp = "000" + nb;
			else if(nb.Length == 3)
				temp = "00" + nb;
			else if(nb.Length == 4)
				temp = "0" + nb;
			else temp = nb;

			return temp;
		}

		private void BindData1()
		{
			string plus = "";

			if(TXT_COMP_CODE.Text != "")
			{
				plus += " AND MKI_SRCCODE = ''"+TXT_COMP_CODE.Text+"'' ";
			}

			if(TXT_COMP_DESC.Text != "")
			{
				plus += " AND MKI_DESC LIKE ''%"+TXT_COMP_DESC.Text+"%''";
			}

			/*conn.QueryString = "select MKI_CODE, MKI_SRCCODE, MKI_DESC, "+
				"isnull(MKI_GRLINE,0) GRLINE, isnull(MKI_PLAFOND,0) PLAFOND, "+
				"(convert(float,isnull(MKI_GRLINE,0)) - convert(float,isnull(MKI_PLAFOND,0))) as SISA, MKI_BLOCKED, "+ 
				"BLOCKED = case MKI_BLOCKED when '0' then 'No' when '1' then 'Yes' end "+
				"from RFMITRAKARYA_INDUK where 1=1 "+plus+" order by MKI_SRCCODE";
			*/

			//changed by sofyan 2007-05-28, LOS Consumer Enh 4
			/*conn.QueryString = "select MKI_CODE, MKI_SRCCODE, MKI_DESC, "+
				"isnull(MKI_GRLINE,0) GRLINE, isnull((select sum(MTK_GRLINE) from rfmitrakarya where mki_code =RFMITRAKARYA_INDUK.mki_code),0) PLAFOND, "+
				"(convert(float,isnull(MKI_GRLINE,0)) - convert(float,isnull((select sum(MTK_GRLINE) from rfmitrakarya where mki_code =RFMITRAKARYA_INDUK.mki_code),0))) as SISA, MKI_BLOCKED, "+ 
				"BLOCKED = case MKI_BLOCKED when '0' then 'No' when '1' then 'Yes' end, "+
				"convert(varchar,MKI_EXPIREDATE,103) MKI_EXPIREDATE2, RT_DESC, convert(varchar,MKI_EXPIREDATE) MKI_EXPIREDATE, RFMITRAKARYA_INDUK.RT_CODE "+
				"from RFMITRAKARYA_INDUK left join RFRATING on RFMITRAKARYA_INDUK.RT_CODE = RFRATING.RT_CODE "+
				"where 1=1 and active='1' "+plus+" order by MKI_SRCCODE";
			*/
			
			conn.QueryString = "exec PARAM_MITRAKARYA_INDUK_VIEW_EXISTING '" + plus + "'";
			conn.ExecuteQuery();

			if (conn.GetRowCount() != 0)
			{
				System.Data.DataTable dt = new System.Data.DataTable ();
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
				
			} 

			conn.ClearData();
		}

		private void BindData2()
		{
			//changed by sofyan 2007-05-28, LOS Consumer Enh 4
			/*conn.QueryString = "select MKI_CODE, MKI_SRCCODE, MKI_DESC, isnull(MKI_GRLINE,0) GRLINE, "+ 
				"isnull((select sum(MTK_GRLINE) from rfmitrakarya where mki_code =TRFMITRAKARYA_INDUK.mki_code),0) PLAFOND, "+
				"(convert(float,isnull(MKI_GRLINE,0)) - convert(float,isnull((select sum(MTK_GRLINE) from rfmitrakarya where mki_code =TRFMITRAKARYA_INDUK.mki_code),0))) as SISA, "+
				"MKI_BLOCKED, CH_STA, "+
				"STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' when '3' then 'DELETE' end, "+
				"BLOCKED = case MKI_BLOCKED when '0' then 'No' when '1' then 'Yes' end ,"+
				"convert(varchar,MKI_EXPIREDATE,103) MKI_EXPIREDATE2, RT_DESC, convert(varchar,MKI_EXPIREDATE) MKI_EXPIREDATE, TRFMITRAKARYA_INDUK.RT_CODE "+
				"from TRFMITRAKARYA_INDUK left join RFRATING on TRFMITRAKARYA_INDUK.RT_CODE = RFRATING.RT_CODE "+
				"order by MKI_SRCCODE";
			*/

			conn.QueryString = "exec PARAM_MITRAKARYA_INDUK_VIEW_REQUEST";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
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

		private void CalcTotal()
		{	
			conn.QueryString = "select sum(isnull(MKI_GRLINE,0)) as GRLINE, sum(isnull(MKI_PLAFOND,0)) as PLAFOND, "+ 
				"sum((convert(float,isnull(MKI_GRLINE,0)) - convert(float,isnull(MKI_PLAFOND,0)))) as SISA "+ 
				"from RFMITRAKARYA_INDUK";

			conn.ExecuteQuery(); 

			LBL_TPLAFOND.Text = "Rp " +GlobalTools.MoneyFormat(conn.GetFieldValue("GRLINE"));
			LBL_TDISB.Text = "Rp " + GlobalTools.MoneyFormat(conn.GetFieldValue("PLAFOND")); 
			LBL_SISA.Text = "Rp "+ GlobalTools.MoneyFormat(conn.GetFieldValue("SISA"));
    		
			conn.ClearData(); 
		}

		private void ClearEditBoxes()
		{
			TXT_COMP_CODE.Text = "";
			TXT_COMP_DESC.Text = "";
			TXT_PLAFOND.Text = "";
			CHK_BLOCKED.Checked = false;

			TXT_COMP_CODE.Enabled = true;

			LBL_SAVEMODE.Text = "1"; 

			//add by sofyan 2007-05-28, LOS Consumer Enh 4
			TXT_EXP_DAY.Text = "";
			TXT_EXP_YEAR.Text = "";
			DDL_EXP_MONTH.ClearSelection();
			DDL_COMP_RATE.ClearSelection();
		}

		private string cleansFloat(string tn)
		{
			if (tn.Trim() == "&nbsp;")
				tn = "0";
			
			return tn;
		}

		private string seq()
		{
			string seqid = "";
			int number = Int16.Parse(LBL_NB.Text);

			conn.QueryString = "SELECT max(convert(int,MKI_CODE))+ "+LBL_NB.Text+" as MAX from RFMITRAKARYA_INDUK";
			
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0) 
				seqid = conn.GetFieldValue("MAX");
			else
				seqid = "0"; 

			conn.ClearData(); 

			number++;

			LBL_NB.Text = number.ToString();  
			return seqid;
		}

		private void dtCpRating()
		{
			GlobalTools.fillRefList(DDL_COMP_RATE,"select rt_code,rt_desc from RFRATING",conn);
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40");
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code, srccode, desc, grline, plafond; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[0].Text.Trim();
					srccode = e.Item.Cells[1].Text.Trim();
					desc = cleansText(e.Item.Cells[2].Text);  
					grline = GlobalTools.ConvertFloat(cleansFloat(e.Item.Cells[3].Text));
					plafond = GlobalTools.ConvertFloat(cleansFloat(e.Item.Cells[4].Text));

					conn.QueryString = "SELECT * FROM TRFMITRAKARYA_INDUK WHERE MKI_CODE = '"+code+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						/*conn.QueryString = "INSERT INTO TRFMITRAKARYA_INDUK (MKI_CODE, MKI_SRCCODE, "+
							"MKI_DESC, MKI_GRLINE, MKI_PLAFOND, MKI_EXPIREDATE, MKI_BLOCKED, CH_STA, RT_CODE) "+
							"VALUES('"+code+"', '"+srccode+"', '"+desc+
							"', "+grline+", "+plafond+", '"+cleansText(e.Item.Cells[11].Text)+"' "+
							", '"+cleansText(e.Item.Cells[10].Text)+"', '3', '"+cleansText(e.Item.Cells[12].Text)+"')";
						*/
						conn.QueryString = "exec PARAM_MITRAKARYA_INDUK_UPDATE_REQUEST " +
							"'2', " +
							"'" + code + "', " +
							"'" + srccode + "', " +
							"'" + desc + "', " +
							"" + grline + ", " +
							"" + plafond + ", " +
							"'" + cleansText(e.Item.Cells[11].Text) + "', " +
							"'" + cleansText(e.Item.Cells[10].Text) + "', " +
							"'3', " +
							"'" + cleansText(e.Item.Cells[12].Text) + "'";
						conn.ExecuteNonQuery();
						
						BindData2();
					}
					break;

				case "edit":
					TXT_COMP_CODE.Text = e.Item.Cells[1].Text.Trim();
					TXT_COMP_DESC.Text = cleansText(e.Item.Cells[2].Text);   
					TXT_PLAFOND.Text = cleansFloat(e.Item.Cells[3].Text.Replace(",00",""));	
					LBL_MKICODE.Text = e.Item.Cells[0].Text.Trim();

					if(e.Item.Cells[10].Text.Trim() == "0")
						CHK_BLOCKED.Checked = false;
					else
						CHK_BLOCKED.Checked = true;

					LBL_SAVEMODE.Text = "2";    
					TXT_COMP_CODE.Enabled = false; 

					//add by sofyan 2007-05-28, LOS Consumer Enh 4
					string mkicode = e.Item.Cells[0].Text.Trim();
					conn.QueryString = "SELECT * FROM RFMITRAKARYA_INDUK WHERE MKI_CODE = '"+mkicode+"'";
					conn.ExecuteQuery();
					TXT_EXP_DAY.Text = GlobalTools.FormatDate_Day(conn.GetFieldValue(0,"MKI_EXPIREDATE"));
					TXT_EXP_YEAR.Text = GlobalTools.FormatDate_Year(conn.GetFieldValue(0,"MKI_EXPIREDATE"));
					try
					{
						DDL_EXP_MONTH.SelectedValue = GlobalTools.FormatDate_Month(conn.GetFieldValue(0,"MKI_EXPIREDATE").Trim()); 
					}
					catch{ }
					try
					{
						DDL_COMP_RATE.SelectedValue = conn.GetFieldValue(0,"RT_CODE").Trim();
					}
					catch{ }

					break;
				case "view":
					code = e.Item.Cells[0].Text.Trim();
					mid = Request.QueryString["ModuleId"];

					Response.Write("<script language='javascript'>window.open('DetailMitrakaryaInduk.aspx?ModuleId="+mid+"&mkicode="+code+"','Detail','status=no,scrollbars=yes,width=800,height=500');</script>"); 
					break;
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string id = "", cek = "", codeseq = "";
			int hit = 0;
			
			if(TXT_COMP_CODE.Text.Length < 3)
			{
				GlobalTools.popMessage(this,"Company code must 3 digits in length!");
				return;
			}

			//add by sofyan 2007-05-28, LOS Consumer Enh 4
			if(TXT_EXP_DAY.Text != "" || DDL_EXP_MONTH.SelectedValue != "" || TXT_EXP_YEAR.Text != "")
			{
				if(!GlobalTools.isDateValid(this,TXT_EXP_DAY.Text,DDL_EXP_MONTH.SelectedValue,TXT_EXP_YEAR.Text))
					return;
			}

			if(CHK_BLOCKED.Checked)
				cek = "1";
			else
				cek = "0";

			conn.QueryString = "SELECT * FROM TRFMITRAKARYA_INDUK WHERE MKI_CODE = '"+LBL_MKICODE.Text+"' "+
				"AND MKI_SRCCODE = '"+TXT_COMP_CODE.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				//changed by sofyan 2007-05-28, LOS Consumer Enh 4
				/*conn.QueryString = "UPDATE TRFMITRAKARYA_INDUK SET "+
					"MKI_DESC = "+GlobalTools.ConvertNull(TXT_COMP_DESC.Text)+", "+
					"MKI_GRLINE = "+GlobalTools.ConvertFloat(TXT_PLAFOND.Text)+", "+
					"MKI_EXPIREDATE = "+GlobalTools.ToSQLDate(TXT_EXP_DAY.Text,DDL_EXP_MONTH.SelectedValue,TXT_EXP_YEAR.Text)+", "+
					"RT_CODE = "+GlobalTools.ConvertNull(DDL_COMP_RATE.SelectedValue)+" "+
					"WHERE MKI_CODE = '"+LBL_MKICODE.Text+"' AND MKI_SRCCODE = '"+TXT_COMP_CODE.Text+"'";
				*/
				conn.QueryString = "exec PARAM_MITRAKARYA_INDUK_UPDATE_REQUEST " +
					"'1', " +
					"'" + LBL_MKICODE.Text + "', " +
					"'" + TXT_COMP_CODE.Text + "', " +
					"" + GlobalTools.ConvertNull(TXT_COMP_DESC.Text) + ", " +
					"" + GlobalTools.ConvertFloat(TXT_PLAFOND.Text) + ", " +
					"0, " +
					"" + GlobalTools.ToSQLDate(TXT_EXP_DAY.Text,DDL_EXP_MONTH.SelectedValue,TXT_EXP_YEAR.Text) + ", " +
					"'" + cek + "', "+
					"'', " +
					"" + GlobalTools.ConvertNull(DDL_COMP_RATE.SelectedValue) + "";
				conn.ExecuteNonQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				//changed by sofyan 2007-05-28, LOS Consumer Enh 4
				/*conn.QueryString = "INSERT INTO TRFMITRAKARYA_INDUK (MKI_CODE, MKI_SRCCODE, MKI_DESC, "+
					"MKI_GRLINE, MKI_PLAFOND, MKI_EXPIREDATE, MKI_BLOCKED, CH_STA, RT_CODE) VALUES ("+
					"'"+LBL_MKICODE.Text+"', "+
					"'"+TXT_COMP_CODE.Text+"', "+
					GlobalTools.ConvertNull(TXT_COMP_DESC.Text)+", "+
					GlobalTools.ConvertFloat(TXT_PLAFOND.Text)+", "+
					"0, "+
					GlobalTools.ToSQLDate(TXT_EXP_DAY.Text,DDL_EXP_MONTH.SelectedValue,TXT_EXP_YEAR.Text)+", "+
					"'"+cek+"', "+
					"'2', "+
					GlobalTools.ConvertNull(DDL_COMP_RATE.SelectedValue)+")";    
				*/
				conn.QueryString = "exec PARAM_MITRAKARYA_INDUK_UPDATE_REQUEST " +
					"'2', " +
					"'" + LBL_MKICODE.Text + "', " +
					"'" + TXT_COMP_CODE.Text + "', " +
					"" + GlobalTools.ConvertNull(TXT_COMP_DESC.Text) + ", " +
					"" + GlobalTools.ConvertFloat(TXT_PLAFOND.Text) + ", " +
					"0, " +
					"" + GlobalTools.ToSQLDate(TXT_EXP_DAY.Text,DDL_EXP_MONTH.SelectedValue,TXT_EXP_YEAR.Text) + ", " +
					"'" + cek + "', "+
					"'2', " +
					"" + GlobalTools.ConvertNull(DDL_COMP_RATE.SelectedValue) + "";
				conn.ExecuteNonQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				id = seq();
				codeseq = createseq(id);
				
				//changed by sofyan 2007-05-28, LOS Consumer Enh 4
				/*conn.QueryString = "INSERT INTO TRFMITRAKARYA_INDUK (MKI_CODE, MKI_SRCCODE, MKI_DESC, "+
					"MKI_GRLINE, MKI_PLAFOND, MKI_EXPIREDATE, MKI_BLOCKED, CH_STA, RT_CODE) VALUES ("+
					"'"+codeseq+"', "+
					"'"+TXT_COMP_CODE.Text+"', "+
					GlobalTools.ConvertNull(TXT_COMP_DESC.Text)+", "+
					GlobalTools.ConvertFloat(TXT_PLAFOND.Text)+", "+
					"0, "+
					GlobalTools.ToSQLDate(TXT_EXP_DAY.Text,DDL_EXP_MONTH.SelectedValue,TXT_EXP_YEAR.Text)+", "+
					"'"+cek+"', "+
					"'1', "+
					GlobalTools.ConvertNull(DDL_COMP_RATE.SelectedValue)+")";  
				*/
				conn.QueryString = "exec PARAM_MITRAKARYA_INDUK_UPDATE_REQUEST " +
					"'2', " +
					"'" + codeseq + "', " +
					"'" + TXT_COMP_CODE.Text + "', " +
					"" + GlobalTools.ConvertNull(TXT_COMP_DESC.Text) + ", " +
					"" + GlobalTools.ConvertFloat(TXT_PLAFOND.Text) + ", " +
					"0, " +
					"" + GlobalTools.ToSQLDate(TXT_EXP_DAY.Text,DDL_EXP_MONTH.SelectedValue,TXT_EXP_YEAR.Text) + ", " +
					"'" + cek + "', "+
					"'1', " +
					"" + GlobalTools.ConvertNull(DDL_COMP_RATE.SelectedValue) + "";
				conn.ExecuteNonQuery();
				
				ClearEditBoxes();
			}
			else if((hit != 0) && (LBL_SAVEMODE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
				return;
			}

			conn.ClearData();
 	
			BindData2();

			LBL_SAVEMODE.Text = "1"; 
			LBL_MKICODE.Text = ""; 
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 		
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = cleansText(e.Item.Cells[0].Text);

					conn.QueryString = "DELETE FROM TRFMITRAKARYA_INDUK WHERE MKI_CODE = '"+code+"'";
					conn.ExecuteNonQuery();

					BindData2();
					break;
				case "edit":
					if(e.Item.Cells[7].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						TXT_COMP_CODE.Text = e.Item.Cells[1].Text.Trim();
						TXT_COMP_DESC.Text = cleansText(e.Item.Cells[2].Text);   
						TXT_PLAFOND.Text = cleansFloat(e.Item.Cells[3].Text.Replace(",00",""));
						LBL_MKICODE.Text = e.Item.Cells[0].Text.Trim();
				
						if(e.Item.Cells[12].Text.Trim() == "0")
							CHK_BLOCKED.Checked = false;
						else
							CHK_BLOCKED.Checked = true;

						TXT_COMP_CODE.Enabled = false; 
						
						LBL_SAVEMODE.Text = "2";

						//add by sofyan 2007-05-28, LOS Consumer Enh 4
						string mkicode = e.Item.Cells[0].Text.Trim();
						conn.QueryString = "SELECT * FROM TRFMITRAKARYA_INDUK WHERE MKI_CODE = '"+mkicode+"'";
						conn.ExecuteQuery(); 
						TXT_EXP_DAY.Text = GlobalTools.FormatDate_Day(conn.GetFieldValue(0,"MKI_EXPIREDATE"));
						TXT_EXP_YEAR.Text = GlobalTools.FormatDate_Year(conn.GetFieldValue(0,"MKI_EXPIREDATE"));
						try
						{
							DDL_EXP_MONTH.SelectedValue = GlobalTools.FormatDate_Month(conn.GetFieldValue(0,"MKI_EXPIREDATE").Trim()); 
						}
						catch{ }
						try
						{
							DDL_COMP_RATE.SelectedValue = conn.GetFieldValue(0,"RT_CODE").Trim();
						}
						catch{ }
					}
					break;
			}			
		}

		int Export_to_Excell()
		{
			string sql="";
			
			try
			{
				sql ="select MKI_SRCCODE, MKI_DESC,isnull(MKI_GRLINE,0) GRLINE, isnull(MKI_PLAFOND,0) PLAFOND"+
					",(convert(float,isnull(MKI_GRLINE,0)) - convert(float,isnull(MKI_PLAFOND,0))) as SISA"+
					",BLOCKED = case MKI_BLOCKED when '0' then 'No' when '1' then 'Yes' end "+
					"from RFMITRAKARYA_INDUK  order by MKI_CODE "; 

				conn.QueryString = sql;
				conn.ExecuteQuery();
				System.Data.DataTable dt1;
				dt1 = conn.GetDataTable().Copy();

				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
				Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
				
				string FileUpload = "";
				conn2.QueryString = "SELECT APP_ROOT FROM RFMODULE WHERE MODULEID='99'";
				conn2.ExecuteQuery();
				if (conn2.GetRowCount()>0)
				{FileUpload = conn2.GetFieldValue(0,"APP_ROOT")+"FileExcel";}
				else
				{FileUpload = @"C:\Inetpub\ftproot";}
                //FileUpload = @"C:\Inetpub\ftproot";
				string path = FileUpload + "\\TempalateMitraKaryaInduk.xls";

				Excel.Application excelApp = new Excel.ApplicationClass();
				excelApp.DisplayAlerts= false;

				Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(path ,
					0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t",
					false, false, 0, true) ;//,true,false);

				Excel.Sheets excelSheets = excelWorkbook.Worksheets;
				string currentSheet = "Sheet1";
				Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(currentSheet);
				//----------------- separator -----------------------------------------------------------------------------------------------------------------------------------------------------------------
				string[] LBL_Kolom = new string[26]; 
				LBL_Kolom[0] = "A";
				LBL_Kolom[1] = "B";
				LBL_Kolom[2] = "C";
				LBL_Kolom[3] = "D";
				LBL_Kolom[4] = "E";
				LBL_Kolom[5] = "F";
				LBL_Kolom[6] = "G";
				LBL_Kolom[7] = "H";
				LBL_Kolom[8] = "I";
				LBL_Kolom[9] = "J";
				LBL_Kolom[10] = "K";
				LBL_Kolom[11] = "L";
				LBL_Kolom[12] = "M";
				LBL_Kolom[13] = "N";
				LBL_Kolom[14] = "O";
				LBL_Kolom[15] = "P";
				LBL_Kolom[16] = "Q";
				LBL_Kolom[17]= "R";
				LBL_Kolom[18] = "S";
				LBL_Kolom[19] = "T";
				LBL_Kolom[20] = "U";
				LBL_Kolom[21] = "V";
				LBL_Kolom[22] = "W";
				LBL_Kolom[23] = "X";
				LBL_Kolom[24] = "Y";
				LBL_Kolom[25] = "Z";
			
				Excel.Range excelCell;
				for (int i=0;i<dt1.Rows.Count;i++)
				{
					excelCell = (Excel.Range)excelWorksheet.get_Range(LBL_Kolom[0] + (i+7) ,LBL_Kolom[0] + (i+7));
					excelCell.Value2 =i+1;
					for(int kolom=1;kolom<7;kolom++)
					{
						excelCell = (Excel.Range)excelWorksheet.get_Range(LBL_Kolom[kolom] + (i+7) ,LBL_Kolom[kolom] + (i+7));
						excelCell.Value2 = conn.GetFieldValue(dt1,i,kolom-1);
					}
				}
				//--------------------separator-------------------------------------------------------------------------------------------------------------------------------------------
								
				System.Random r = new System.Random();
				string temp = r.NextDouble().ToString();
				string workbookPath = FileUpload + @"\MitraKaryaInduk" +temp+ ".xls";
				BTN_DOWNLOAD.Visible=true;
				Label2.Text="MitraKaryaInduk" +temp+ ".xls";	
			
				excelWorkbook.SaveAs(workbookPath,Excel.XlFileFormat.xlWorkbookNormal,null,null,true,null,Excel.XlSaveAsAccessMode.xlNoChange,null,null,null,null); //,true); 
				excelWorkbook.Close(null,null,null);
				excelApp.Workbooks.Close();
				excelApp.Application.Quit();
				excelApp.Quit();
				System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheets); 
				System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook); 
				System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp); 
				excelSheets = null; 
				excelWorkbook = null; 
				excelApp = null;
				return 1;
			}
			catch(Exception E){return 0;}
		}



		protected void BTN_EXPORT_Click(object sender, System.EventArgs e)
		{
			if(Export_to_Excell()==1)
			{
				Response.Write("<Script language='javascript'>alert('Export report Succesed !')</Script>");
			}
			else
			{
				Response.Write("<Script language='javascript'>alert('Export report failed !')</Script>");
			}
		}

		protected void BTN_DOWNLOAD_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("../../../FileExcel/"+Label2.Text+"");
		}

		protected void BTN_FIND_Click(object sender, System.EventArgs e)
		{
			BindData1(); 
		}
	}
}
