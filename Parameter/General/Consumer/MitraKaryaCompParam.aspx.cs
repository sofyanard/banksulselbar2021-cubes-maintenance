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
	/// Summary description for MitraKaryaCompParam.
	/// </summary>
	public partial class MitraKaryaCompParam : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;
		protected string potgaji, block, tht, expdate, mtksrc;
		protected string src, plafond, limit, tenor, mtkdesc;
		protected string insub, gline, remain, rtcode, bcode;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			setDbConn();

			if(!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1"; 
				GlobalTools.initDateForm(TXT_EXP_DAY,DDL_EXP_MONTH,TXT_EXP_YEAR);
				
				dtCpInduk();
				dtCpRating();
				dtCab();
				
				viewExistingData();
				viewPendingData();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");	
			
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
			this.DGExisting.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGExisting_ItemCommand);
			this.DGExisting.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGExisting_PageIndexChanged);
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion
		
		private void setDbConn()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();
			
			conn = new Connection("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void dtCpInduk()
		{
			//GlobalTools.fillRefList(DDL_COMP_INDUK,"select mki_code,mki_code+' - '+mki_srccode + ' - '+ mki_desc from rfmitrakarya_induk",true,conn);
			GlobalTools.fillRefList(DDL_COMP_INDUK,"select mki_code,mki_code+' - '+mki_srccode + ' - '+ mki_desc from rfmitrakarya_induk",conn);
		}

		private void dtCab()
		{
			GlobalTools.fillRefList(DDL_BRANCH,"select branch_code, branch_name from VW_RFBRANCH2",conn);
		}

		private void dtCpRating()
		{
			GlobalTools.fillRefList(DDL_COMP_RATE,"select rt_code,rt_desc from RFRATING",conn);
		}

		private void mrcGen(string id)
		{
			string a,b,c;
			
			string myQuery = "select MKI.MKI_CODE, MKI.MKI_DESC, MKI.MKI_SRCCODE, isnull(right(max(MTK.MTK_SRCCODE), 3), 0) + 1 as JUM "+
				"from RFMITRAKARYA_INDUK MKI left join RFMITRAKARYA MTK on MKI.MKI_CODE = MTK.MKI_CODE where MKI.MKI_CODE ='"+id+"' "+ 
				"group by MKI.MKI_CODE, MKI.MKI_DESC, MKI.MKI_SRCCODE order by MKI.MKI_CODE";
			
			conn.QueryString = myQuery;
			conn.ExecuteQuery();
			
			a = conn.GetFieldValue(0,2);
			b = conn.GetFieldValue(0,3);
			c=b;
			
			for (int x = b.Length; x<3; x++)
			{
				c = "0"+c;
			}
			
			TXT_COMP_CODE.Text = a+c;
		}

		private string codeGen()
		{
			string b,c;
			//string myQuery = "select isnull(max(MTK_CODE),0)+1 MAXSEQ from RFMITRAKARYA";
			string myQuery = "select max (maxseq) from " +
				"(select isnull(max(MTK_CODE),0)+1 MAXSEQ from RFMITRAKARYA " +
				"union " +
				"select isnull(max(MTK_CODE),0)+1 MAXSEQ from TRFMITRAKARYA " +
				") mtkcode ";
			
			conn.QueryString = myQuery;
			conn.ExecuteQuery();
			
			b = conn.GetFieldValue(0,0);
			c=b;
			
			for (int x=b.Length;x<5;x++)
			{
				c = "0"+c;
			}
			
			return c;
		}

		private void viewExistingData()
		{
			string plus = "";

			if(DDL_COMP_INDUK.SelectedValue != "")
			{
				plus += " AND a.MKI_CODE = '"+DDL_COMP_INDUK.SelectedValue+"' ";
			}

			if(DDL_BRANCH.SelectedValue != "")
			{
				plus += " AND a.BRANCH_CODE = '"+DDL_BRANCH.SelectedValue+"' ";
			}

			if(TXT_NAME.Text != "")
			{
				plus += " AND a.MTK_DESC LIKE '"+TXT_NAME.Text+"%'";
			}
			
			conn.QueryString = "select a.MTK_CODE, a.MTK_SRCCODE, a.MKI_CODE, b.MKI_DESC, "+
				"a.MTK_DESC, a.MTK_SUBINTEREST, isnull(a.MTK_GRLINE,0) MTK_GRLINE, isnull(a.MTK_PLAFOND,0) MTK_PLAFOND, "+
				"(convert(float,isnull(a.MTK_GRLINE,0))- convert(float,isnull(a.MTK_PLAFOND,0))) as REMAIN "+ 
				"from RFMITRAKARYA a left join RFMITRAKARYA_INDUK b on a.MKI_CODE = b.MKI_CODE where 1 = 1" +plus;
			
			conn.ExecuteQuery();
			
			DGExisting.DataSource = conn.GetDataTable().Copy();
			
			try 
			{
				DGExisting.DataBind();
			}
			catch 
			{
				DGExisting.CurrentPageIndex = DGExisting.PageCount - 1;
				DGExisting.DataBind();
			}
		}

		private void viewPendingData()
		{
			conn.QueryString = "select a.MTK_CODE, a.MTK_SRCCODE, a.MKI_CODE, b.MKI_DESC, "+
				"a.MTK_DESC, a.MTK_SUBINTEREST, isnull(a.MTK_GRLINE,0) MTK_GRLINE, isnull(a.MTK_PLAFOND,0) MTK_PLAFOND, "+
				"(convert(float,isnull(a.MTK_GRLINE,0))- convert(float,isnull(a.MTK_PLAFOND,0))) as REMAIN, a.CH_STA, "+
				"STATUS = case a.CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' when '3' then 'DELETE' end "+
				"from TRFMITRAKARYA a left join RFMITRAKARYA_INDUK b on a.MKI_CODE = b.MKI_CODE";
			conn.ExecuteQuery();

			DGRequest.DataSource = conn.GetDataTable().Copy();
			
			try 
			{
				DGRequest.DataBind();
			}
			catch 
			{
				DGRequest.CurrentPageIndex = DGRequest.PageCount - 1;
				DGRequest.DataBind();
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleId=40");
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string blk = "", tht = "", id = "";
			int hit = 0;
			
			if (CHK_BLOCKED.Checked==true)
				blk = "1";
			else
				blk = "0";
			
			if (CHK_COVER_THT.Checked==true)
				tht = "1";
			else
				tht = "0";

			if(TXT_EXP_DAY.Text != "" || DDL_EXP_MONTH.SelectedValue != "" || TXT_EXP_YEAR.Text != "")
			{
				if(!GlobalTools.isDateValid(this,TXT_EXP_DAY.Text,DDL_EXP_MONTH.SelectedValue,TXT_EXP_YEAR.Text))
					return;
			}
			
			conn.QueryString = "SELECT * FROM TRFMITRAKARYA WHERE MTK_CODE = '"+LBL_MTKCODE.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "UPDATE TRFMITRAKARYA SET "+
					"BRANCH_CODE = "+GlobalTools.ConvertNull(DDL_BRANCH.SelectedValue)+", "+
					"MKI_CODE = "+GlobalTools.ConvertNull(LBL_MKICODE.Text)+","+
					"MKT_PESATENAM = 0.0, "+
					"MKT_PESENAM = 0.0, "+
					"MKT_POTGAJI = "+GlobalTools.ConvertFloat(TXT_PCT_SALARY.Text)+", "+
					"MTK_BLOCKED = '"+blk+"', "+
					"MTK_COVERTHT = '"+tht+"', "+
					"MTK_DESC = "+GlobalTools.ConvertNull(TXT_NAME.Text)+", "+
					"MTK_EXPIREDATE = "+GlobalTools.ToSQLDate(TXT_EXP_DAY.Text,DDL_EXP_MONTH.SelectedValue,TXT_EXP_YEAR.Text)+", "+
					"MTK_GRLINE = "+GlobalTools.ConvertFloat(TXT_GRLINE.Text)+", "+
					"MTK_INDLIMIT = "+GlobalTools.ConvertFloat(TXT_LIMIT.Text)+", "+
					"MTK_INDTENOR = "+GlobalTools.ConvertFloat(TXT_TENOR.Text)+", "+
					"MTK_PLAFOND = "+GlobalTools.ConvertFloat(TXT_REAL.Text)+", "+
					"MTK_SRCCODE = "+GlobalTools.ConvertNull(TXT_COMP_CODE.Text)+", "+
					"MTK_SUBINTEREST = "+GlobalTools.ConvertFloat(TXT_SUB_INT.Text)+", "+
					"RT_CODE = "+GlobalTools.ConvertNull(DDL_COMP_RATE.SelectedValue)+" "+
					"WHERE MTK_CODE = '"+LBL_MTKCODE.Text+"'";
										
				conn.ExecuteQuery();	

				clearControl(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "INSERT INTO TRFMITRAKARYA (MTK_CODE, MKI_CODE, MTK_SRCCODE, MTK_DESC, MTK_SUBINTEREST,"+
					" MTK_GRLINE, MTK_PLAFOND, MTK_EXPIREDATE, CH_STA, RT_CODE, MKT_POTGAJI,"+ 
					" MKT_PESENAM, MKT_PESATENAM, MTK_BLOCKED, MTK_INDLIMIT, MTK_INDTENOR, MTK_COVERTHT, BRANCH_CODE) values "+
					"('"+LBL_MTKCODE.Text+"', "+GlobalTools.ConvertNull(LBL_MKICODE.Text)+", "+GlobalTools.ConvertNull(TXT_COMP_CODE.Text)+", "+GlobalTools.ConvertNull(TXT_NAME.Text)+","+ 
					" "+GlobalTools.ConvertFloat(TXT_SUB_INT.Text)+", "+GlobalTools.ConvertFloat(TXT_GRLINE.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_REAL.Text)+", "+GlobalTools.ToSQLDate(TXT_EXP_DAY.Text,DDL_EXP_MONTH.SelectedValue,TXT_EXP_YEAR.Text)+","+ 
					" '2', "+GlobalTools.ConvertNull(DDL_COMP_RATE.SelectedValue)+","+
					" "+GlobalTools.ConvertFloat(TXT_PCT_SALARY.Text)+", 0, 0, "+GlobalTools.ConvertNull(blk)+", "+GlobalTools.ConvertFloat(TXT_LIMIT.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_TENOR.Text)+", "+GlobalTools.ConvertNull(tht)+", "+GlobalTools.ConvertNull(DDL_BRANCH.SelectedValue)+")";
					
				conn.ExecuteQuery();
 
				clearControl(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				id = codeGen();

				//======18may05--cek MTK_SRCCODE
				conn.QueryString = " select * from TRFMITRAKARYA where MKI_CODE='"+DDL_COMP_INDUK.SelectedValue+"'";
				conn.ExecuteQuery();
				if (conn.GetRowCount()!=0)
				{
					conn.QueryString = "exec MK_GENNEWSRCCODE '"+DDL_COMP_INDUK.SelectedValue+"'";
					conn.ExecuteQuery();
					TXT_COMP_CODE.Text = conn.GetFieldValue("newsrccode");
				}
				
				//=======

				conn.QueryString = "INSERT INTO TRFMITRAKARYA (MTK_CODE, MKI_CODE, MTK_SRCCODE, MTK_DESC, MTK_SUBINTEREST,"+
					" MTK_GRLINE, MTK_PLAFOND, MTK_EXPIREDATE, CH_STA, RT_CODE, MKT_POTGAJI,"+ 
					" MKT_PESENAM, MKT_PESATENAM, MTK_BLOCKED, MTK_INDLIMIT, MTK_INDTENOR, MTK_COVERTHT, BRANCH_CODE) values "+
					"('"+id+"', "+GlobalTools.ConvertNull(DDL_COMP_INDUK.SelectedValue)+", "+GlobalTools.ConvertNull(TXT_COMP_CODE.Text)+", "+GlobalTools.ConvertNull(TXT_NAME.Text)+","+ 
					" "+GlobalTools.ConvertFloat(TXT_SUB_INT.Text)+", "+GlobalTools.ConvertFloat(TXT_GRLINE.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_REAL.Text)+", "+GlobalTools.ToSQLDate(TXT_EXP_DAY.Text,DDL_EXP_MONTH.SelectedValue,TXT_EXP_YEAR.Text)+","+ 
					" '1', "+GlobalTools.ConvertNull(DDL_COMP_RATE.SelectedValue)+","+
					" "+GlobalTools.ConvertFloat(TXT_PCT_SALARY.Text)+", 0, 0, "+GlobalTools.ConvertNull(blk)+", "+GlobalTools.ConvertFloat(TXT_LIMIT.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_TENOR.Text)+", "+GlobalTools.ConvertNull(tht)+", "+GlobalTools.ConvertNull(DDL_BRANCH.SelectedValue)+")";

				conn.ExecuteQuery();
				
				clearControl();
				
			}
			else if((hit != 0) && (LBL_SAVEMODE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
				return;
			}

			conn.ClearData();
 	
			viewPendingData();

			LBL_SAVEMODE.Text = "1"; 
			LBL_MTKCODE.Text = ""; 
			LBL_MKICODE.Text = "";
		
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControl();
		}

		private void DGRequest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		private void DGExisting_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{			
			DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}
		
		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string mtkcode = "", mkicode = ""; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					mkicode = CleansText(e.Item.Cells[0].Text);
					mtkcode = e.Item.Cells[1].Text.Trim();

					conn.QueryString = "SELECT *, (convert(float,isnull(MTK_GRLINE,0))- convert(float,isnull(MTK_PLAFOND,0))) as REMAIN "+
						"FROM RFMITRAKARYA WHERE MTK_CODE = '"+mtkcode+"'";
					conn.ExecuteQuery();
 
					if(conn.GetRowCount() !=0)
					{
						TXT_COMP_CODE.Text = conn.GetFieldValue(0,"MTK_SRCCODE"); 
						TXT_NAME.Text = conn.GetFieldValue(0,"MTK_DESC");
						TXT_SUB_INT.Text = conn.GetFieldValue(0,"MTK_SUBINTEREST");
						TXT_GRLINE.Text = (GlobalTools.MoneyFormat(changeToZero(conn.GetFieldValue(0,"MTK_GRLINE")))).Replace(",00","");
						TXT_REAL.Text = (GlobalTools.MoneyFormat(changeToZero(conn.GetFieldValue(0,"MTK_PLAFOND")))).Replace(",00","");
						TXT_REMAIN_GR.Text = (GlobalTools.MoneyFormat(conn.GetFieldValue(0,"REMAIN"))).Replace(",00","");
						
						TXT_PCT_SALARY.Text = conn.GetFieldValue(0,"MKT_POTGAJI");
						
						TXT_LIMIT.Text = (GlobalTools.MoneyFormat(changeToZero(conn.GetFieldValue(0,"MTK_INDLIMIT")))).Replace(",00","");
						TXT_TENOR.Text = conn.GetFieldValue(0,"MTK_INDTENOR");
						
						if(conn.GetFieldValue(0,"MTK_BLOCKED").Trim() == "1")
							CHK_BLOCKED.Checked = true;
						else
							CHK_BLOCKED.Checked = false;

						if(conn.GetFieldValue(0,"MTK_COVERTHT").Trim() == "1")
							CHK_COVER_THT.Checked = true;
						else
							CHK_COVER_THT.Checked = false;
						
						TXT_EXP_DAY.Text = GlobalTools.FormatDate_Day(conn.GetFieldValue(0,"MTK_EXPIREDATE"));
						TXT_EXP_YEAR.Text = GlobalTools.FormatDate_Year(conn.GetFieldValue(0,"MTK_EXPIREDATE"));

						try
						{
							DDL_EXP_MONTH.SelectedValue = GlobalTools.FormatDate_Month(conn.GetFieldValue(0,"MTK_EXPIREDATE").Trim()); 
						}
						catch{ }

						try
						{
							DDL_BRANCH.SelectedValue = conn.GetFieldValue(0,"BRANCH_CODE").Trim();
						}
						catch{ }

						try
						{
							DDL_COMP_RATE.SelectedValue = conn.GetFieldValue(0,"RT_CODE").Trim();
						}
						catch{ }

					}

					try
					{
						DDL_COMP_INDUK.SelectedValue = mkicode;  

					}
					catch{ }

					LBL_MTKCODE.Text = mtkcode;
					LBL_MKICODE.Text = mkicode; 
					LBL_SAVEMODE.Text = "2"; 

					break;

				case "delete":
					mkicode = e.Item.Cells[0].Text.Trim();
					mtkcode = e.Item.Cells[1].Text.Trim();

					conn.QueryString = "SELECT * FROM TRFMITRAKARYA WHERE MTK_CODE = '"+mtkcode+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "SELECT * FROM RFMITRAKARYA WHERE MTK_CODE = '"+mtkcode+"'";
						conn.ExecuteQuery();

						if(conn.GetRowCount() != 0) 
						{
							mtksrc = conn.GetFieldValue(0,"MTK_SRCCODE"); 
							mtkdesc = conn.GetFieldValue(0,"MTK_DESC");
							insub = conn.GetFieldValue(0,"MTK_SUBINTEREST");
							gline = changeToZero(conn.GetFieldValue(0,"MTK_GRLINE"));
							plafond = changeToZero(conn.GetFieldValue(0,"MTK_PLAFOND"));
							potgaji = conn.GetFieldValue(0,"MKT_POTGAJI");
							limit = conn.GetFieldValue(0,"MTK_INDLIMIT");
							tenor = conn.GetFieldValue(0,"MTK_INDTENOR");
							block = conn.GetFieldValue(0,"MTK_BLOCKED");
							tht = conn.GetFieldValue(0,"MTK_COVERTHT");
							expdate = conn.GetFieldValue(0,"MTK_EXPIREDATE");
							bcode = conn.GetFieldValue(0,"BRANCH_CODE");
							rtcode = conn.GetFieldValue(0,"RT_CODE");
							
							try
							{
								conn.QueryString = "INSERT INTO TRFMITRAKARYA (MTK_CODE, MKI_CODE, MTK_SRCCODE, MTK_DESC, MTK_SUBINTEREST,"+
									" MTK_GRLINE, MTK_PLAFOND, MTK_EXPIREDATE, CH_STA, RT_CODE, MKT_POTGAJI,"+ 
									" MKT_PESENAM, MKT_PESATENAM, MTK_BLOCKED, MTK_INDLIMIT, MTK_INDTENOR, MTK_COVERTHT, BRANCH_CODE) values "+
									"('"+mtkcode+"', '"+mkicode+"', "+GlobalTools.ConvertNull(mtksrc)+", "+GlobalTools.ConvertNull(mtkdesc)+","+ 
									" "+GlobalTools.ConvertFloat(insub)+", "+GlobalTools.ConvertFloat(gline)+","+
									" "+GlobalTools.ConvertFloat(plafond)+", "+GlobalTools.ToSQLDate(expdate)+", '3', "+GlobalTools.ConvertNull(rtcode)+","+
									" "+GlobalTools.ConvertFloat(potgaji)+", 0, 0, "+GlobalTools.ConvertNull(block)+", "+GlobalTools.ConvertFloat(limit)+","+
									" "+GlobalTools.ConvertFloat(tenor)+", "+GlobalTools.ConvertNull(tht)+", "+GlobalTools.ConvertNull(bcode)+")";

								conn.ExecuteQuery();
							}
							catch
							{
								GlobalTools.popMessage(this, "Cannot delete mitrakarya company data!");  
							}
						}

						viewPendingData();
					}
					
					break;

				case "view":
					mtkcode = e.Item.Cells[1].Text.Trim();

					Response.Write("<script language='javascript'>window.open('DetailMitrakaryaComp.aspx?sta=1&mtkcode="+mtkcode+"','Detail','status=no,scrollbars=yes,width=620,height=425');</script>"); 
					break;

				default :
					break;
			}
		}
		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			string mtkcode = "", mkicode = ""; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					mkicode = CleansText(e.Item.Cells[0].Text);
					mtkcode = e.Item.Cells[1].Text.Trim();

					if(e.Item.Cells[10].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						conn.QueryString = "SELECT *, (convert(float,isnull(MTK_GRLINE,0))- convert(float,isnull(MTK_PLAFOND,0))) as REMAIN "+
							"FROM TRFMITRAKARYA WHERE MTK_CODE = '"+mtkcode+"'";

						conn.ExecuteQuery(); 
					
						if(conn.GetRowCount() != 0) 
						{
							TXT_COMP_CODE.Text = conn.GetFieldValue(0,"MTK_SRCCODE"); 
							TXT_NAME.Text = conn.GetFieldValue(0,"MTK_DESC");
							TXT_SUB_INT.Text = conn.GetFieldValue(0,"MTK_SUBINTEREST");
							TXT_GRLINE.Text = (GlobalTools.MoneyFormat(changeToZero(conn.GetFieldValue(0,"MTK_GRLINE")))).Replace(",00","");
							TXT_REAL.Text = (GlobalTools.MoneyFormat(changeToZero(conn.GetFieldValue(0,"MTK_PLAFOND")))).Replace(",00","");
							TXT_REMAIN_GR.Text = (GlobalTools.MoneyFormat(conn.GetFieldValue(0,"REMAIN"))).Replace(",00","");
						
							TXT_PCT_SALARY.Text = conn.GetFieldValue(0,"MKT_POTGAJI");
						
							TXT_LIMIT.Text = (GlobalTools.MoneyFormat(changeToZero(conn.GetFieldValue(0,"MTK_INDLIMIT")))).Replace(",00","");
							TXT_TENOR.Text = conn.GetFieldValue(0,"MTK_INDTENOR");
						
							if(conn.GetFieldValue(0,"MTK_BLOCKED").Trim() == "1")
								CHK_BLOCKED.Checked = true;
							else
								CHK_BLOCKED.Checked = false;

							if(conn.GetFieldValue(0,"MTK_COVERTHT").Trim() == "1")
								CHK_COVER_THT.Checked = true;
							else
								CHK_COVER_THT.Checked = false;
						
							TXT_EXP_DAY.Text = GlobalTools.FormatDate_Day(conn.GetFieldValue(0,"MTK_EXPIREDATE"));
							TXT_EXP_YEAR.Text = GlobalTools.FormatDate_Year(conn.GetFieldValue(0,"MTK_EXPIREDATE"));

							try
							{
								DDL_EXP_MONTH.SelectedValue = GlobalTools.FormatDate_Month(conn.GetFieldValue(0,"MTK_EXPIREDATE").Trim()); 
							}
							catch{ }

							try
							{
								DDL_BRANCH.SelectedValue = conn.GetFieldValue(0,"BRANCH_CODE").Trim();
							}
							catch{ }

							try
							{
								DDL_COMP_RATE.SelectedValue = conn.GetFieldValue(0,"RT_CODE").Trim();
							}
							catch{ }
							
						}

						try
						{
							DDL_COMP_INDUK.SelectedValue = mkicode;  

						}
						catch{ }

						LBL_MTKCODE.Text = mtkcode;
						LBL_MKICODE.Text = mkicode;
						LBL_SAVEMODE.Text = "2"; 
					}

					break;

				case "delete":
					string code = e.Item.Cells[1].Text;
					
					conn.QueryString = "delete from TRFMITRAKARYA WHERE MTK_CODE = '"+code+"'";
					conn.ExecuteQuery();

					viewPendingData();
					
					break;
				default :
					break;
			}
		}
		private void clearControl()
		{
			DDL_BRANCH.ClearSelection();
			DDL_COMP_INDUK.ClearSelection();
			DDL_COMP_RATE.ClearSelection(); 
			DDL_EXP_MONTH.ClearSelection();

			TXT_COMP_CODE.Text = "";
			TXT_EXP_DAY.Text = "";
			TXT_EXP_YEAR.Text = ""; 
			TXT_GRLINE.Text = "0";
			TXT_LIMIT.Text = "0";
			TXT_NAME.Text = "";
			TXT_PCT_SALARY.Text = "0";
			TXT_REAL.Text = "0";
			TXT_REMAIN_GR.Text = "0";
			TXT_SUB_INT.Text = "0";
			TXT_TENOR.Text = "0";
 
			CHK_BLOCKED.Checked = false;
			CHK_COVER_THT.Checked = false; 
			
			LBL_SAVEMODE.Text ="1";
			LBL_MTKCODE.Text = "";  
			LBL_MKICODE.Text = "";
			
		}

		private string changeToZero(string a)
		{
			if(a.Trim()=="" || a.Trim()==null)
				a="0";

			return a;
		}

		protected void DDL_COMP_INDUK_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (DDL_COMP_INDUK.SelectedValue.Trim()!="")
				mrcGen(DDL_COMP_INDUK.SelectedValue.Trim());
			else
				TXT_COMP_CODE.Text="";
		}

		protected void BTN_FIND_Click(object sender, System.EventArgs e)
		{
			viewExistingData(); 		
		}

		int Export_to_Excell()
		{
			string sql="";
			try
			{
				sql ="select (select top 1 REGIONAL_NAME from rfregional where c.REGIONAL_ID=REGIONAL_ID)regional,BRANCH_NAME, "+
					"b.MKI_DESC,MTK_CODE,MTK_DESC,MTK_EXPIREDATE,MTK_GRLINE,MTK_PLAFOND,(MTK_GRLINE-MTK_PLAFOND)remaining, "+
					"(select top 1 RT_DESC from rfrating where rt_code=a.rt_code)RT_DESC,(CASE WHEN MTK_COVERTHT=1 THEN 'Yes' ELSE 'No' end) THT, "+
					"(CASE WHEN mtk_blocked=1 THEN 'Yes' ELSE 'No' end)block  from RFMITRAKARYA a  "+
					"left join RFMITRAKARYA_INDUK b on a.MKI_CODE = b.MKI_CODE "+
					"left join RFBRANCH c on a.BRANCH_CODE = c.BRANCH_CODE "+
					"order by regional_id,branch_name";

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
				string path = FileUpload + "\\TempalateMitraKaryaCompany.xls";

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
					for(int kolom=1;kolom<13;kolom++)
					{
						excelCell = (Excel.Range)excelWorksheet.get_Range(LBL_Kolom[kolom] + (i+7) ,LBL_Kolom[kolom] + (i+7));
						excelCell.Value2 = conn.GetFieldValue(dt1,i,kolom-1);
					}
				}
				//--------------------separator-------------------------------------------------------------------------------------------------------------------------------------------
								
				System.Random r = new System.Random();
				string temp = r.NextDouble().ToString();
				string workbookPath = FileUpload + @"\TempalateMitraKaryaCompany" +temp+ ".xls";
				BTN_DOWNLOAD.Visible=true;
				Label2.Text="TempalateMitraKaryaCompany" +temp+ ".xls";	
			
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

		protected void BTN_DOWNLOAD_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("../../../FileExcel/"+Label2.Text+"");
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

	}
}
