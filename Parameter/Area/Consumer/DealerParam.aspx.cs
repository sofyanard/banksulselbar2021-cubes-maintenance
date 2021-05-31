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
	/// Summary description for DealerParam.
	/// </summary>
	public partial class DealerParam : System.Web.UI.Page
	{
		protected string mid, addr1, addr2, addr3, bid, type;
		protected string ph1, ph2, ph3, fx1, fx2, cty, dzipcode;
		protected string baddr1, baddr2, baddr3, bname, bcity, bzipcode;
		protected string dpremi, ddealer, src, acc_no;
		protected string insub, spv, manager, npwp;
		protected Connection conn;
		protected Connection conn2;
	
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
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			LBL_SAVEMODE.Text = "1"; 

			InitialCon(); 
			
			GlobalTools.fillRefList(DDL_CITY,"select CITY_ID, CITY_NAME from CITY",conn);
			GlobalTools.fillRefList(DDL_DEALER,"select DLI_CODE, DLI_DESC from DEALER_INDUK order by DLI_CODE",conn);
			GlobalTools.fillRefList(DDL_BANK_NAME,"select BANKID, BANKNAME from RFBANK where ACTIVE = '1'",conn);

			BindData1();
			BindData2();
			SetRadio();
		}

		private void SetRadio()
		{
			RDB_TYPE.Items.Add(new ListItem("Dealer","0"));
			RDB_TYPE.Items.Add(new ListItem("Showroom","1"));
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData1()
		{
			conn.QueryString = "select a.ID_DEALER, a.CITY_ID, a.DLI_CODE, a.NM_DEALER, isnull(a.DL_ADDR1,'')+' '+isnull(a.DL_ADDR2,'')+' '+isnull(a.DL_ADDR3,'') as ADDRESS, a.DL_SRCCODE, "+
				"(select DLI_DESC from  DEALER_INDUK where DEALER_INDUK.DLI_CODE = a.DLI_CODE) DLI_NAME from DEALER a "+
				"left join RFBANK b on a.DL_BANKNM = b.BANKID "+
				"where a.active='1' and a.CITY_ID = '"+DDL_CITY.SelectedValue+"' order by a.ID_DEALER";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG1.DataSource = dt;

			try
			{
				DG1.DataBind();
			}
			catch 
			{
				DG1.CurrentPageIndex = DG1.PageCount - 1;
				DG1.DataBind();
			}

			conn.ClearData();
		}

		private void BindData2()
		{
			conn.QueryString = "select a.ID_DEALER, a.CITY_ID, a.DLI_CODE, a.NM_DEALER, isnull(a.DL_ADDR1,'')+' '+isnull(a.DL_ADDR2,'')+' '+isnull(a.DL_ADDR3,'') as ADDRESS, a.DL_SRCCODE, "+
				"a.CH_STA, STATUS = case a.CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' when '3' then 'DELETE' end, "+
				"(select DLI_DESC from DEALER_INDUK where DEALER_INDUK.DLI_CODE = a.DLI_CODE) DLI_NAME "+
				"from TDEALER a left join RFBANK b on a.DL_BANKNM = b.BANKID"; 

			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG2.DataSource = dt;

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
			TXT_ACC_NO.Text = "";
			TXT_ADDR1.Text = "";
			TXT_ADDR2.Text = "";
			TXT_ADDR3.Text = "";
			TXT_BANK_ADDR1.Text = "";
			TXT_BANK_ADDR2.Text = "";
			TXT_BANK_ADDR3.Text = "";
			TXT_BANK_CITY.Text = ""; 
			TXT_BANK_CITY.Text = "";
			TXT_BANK_ZIPCODE.Text = "";
			TXT_CH_SRC.Text = "";
			TXT_CITY.Text = "";
			TXT_DISC_DEALER.Text = "0"; 
			TXT_DISC_PREMI.Text = "0";
			TXT_DLNAME.Text = "";
			TXT_FX1.Text = "";
			TXT_FX2.Text = "";
			TXT_INT_SUB.Text = "0";
			TXT_MANAGER.Text = "";
			TXT_NPWP.Text = ""; 
			TXT_PH1.Text = "";
			TXT_PH2.Text = "";
			TXT_PH3.Text = "";
			TXT_ZIPCODE.Text = ""; 
			DDL_CITY.Enabled = true;
 
			LBL_SAVEMODE.Text = "1";
			LBL_CODE.Text = "";
		}

		private string createseq(string nb)
		{
			string temp = "";

			if(nb.Length == 1)
				temp = "000" + nb;
			else if(nb.Length == 2)
				temp = "00" + nb;
			else if(nb.Length == 3)
				temp = "0" + nb;
			else temp = nb;

			return temp;
		}

		private string seq()
		{
			string seqid = "";
			int number = Int16.Parse(LBL_NB.Text);

			conn.QueryString = "select isnull(max(convert(int, ID_DEALER)),0)+ "+LBL_NB.Text+" as MAX from DEALER";
			
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
			Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleID=40");	
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string id = "", seqid = ""; 
			int hit = 0;

			conn.QueryString = "select * from TDEALER where CITY_ID = '"+DDL_CITY.SelectedValue+"' and ID_DEALER = '"+LBL_CODE.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "UPDATE TDEALER SET DLI_CODE = "+GlobalTools.ConvertNull(DDL_DEALER.SelectedValue)+", NM_DEALER = "+GlobalTools.ConvertNull(TXT_DLNAME.Text)+", "+ 
					"DL_ADDR1 = "+GlobalTools.ConvertNull(TXT_ADDR1.Text)+", DL_ADDR2 = "+GlobalTools.ConvertNull(TXT_ADDR2.Text)+", "+ 
					"DL_ADDR3 = "+GlobalTools.ConvertNull(TXT_ADDR3.Text)+", DL_CITY = "+GlobalTools.ConvertNull(TXT_CITY.Text)+", "+ 
					"DL_ZIPCODE = "+GlobalTools.ConvertNull(TXT_ZIPCODE.Text)+", DL_PHNAREA = "+GlobalTools.ConvertNull(TXT_PH1.Text)+", "+
					"DL_PHNNUM = "+GlobalTools.ConvertNull(TXT_PH2.Text)+", DL_PHNEXT = "+GlobalTools.ConvertNull(TXT_PH3.Text)+", "+
					"DL_FAXAREA = "+GlobalTools.ConvertNull(TXT_FX1.Text)+", DL_FAXNUM = "+GlobalTools.ConvertNull(TXT_FX2.Text)+", "+ 
					"DL_NPWP = "+GlobalTools.ConvertNull(TXT_NPWP.Text)+", DL_MANAGER = "+GlobalTools.ConvertNull(TXT_MANAGER.Text)+", "+
					"DL_SALSUPVIS = "+GlobalTools.ConvertNull(TXT_SPV.Text)+", DL_BANKNM = "+GlobalTools.ConvertNull(DDL_BANK_NAME.SelectedValue)+", "+ 
					"DL_BANKADDR = "+GlobalTools.ConvertNull(TXT_BANK_ADDR1.Text)+", DL_BANKADDR2 = "+GlobalTools.ConvertNull(TXT_BANK_ADDR2.Text)+", "+ 
					"DL_BANKADDR3 = "+GlobalTools.ConvertNull(TXT_BANK_ADDR3.Text)+", DL_BANKCITY = "+GlobalTools.ConvertNull(TXT_BANK_CITY.Text)+", "+
					"DL_BANKZIPCODE = "+GlobalTools.ConvertNull(TXT_BANK_ZIPCODE.Text)+", DL_ACCNUM = "+GlobalTools.ConvertNull(TXT_ACC_NO.Text)+", "+
					"DL_DISCPREMI = "+GlobalTools.ConvertFloat(TXT_DISC_PREMI.Text)+", DL_DISCDEALER = "+GlobalTools.ConvertFloat(TXT_DISC_DEALER.Text)+", "+
					"DL_INTSUB = "+GlobalTools.ConvertFloat(TXT_INT_SUB.Text)+", "+
					"DL_SRCCODE = "+GlobalTools.ConvertNull(TXT_CH_SRC.Text)+", DL_TYPE = "+GlobalTools.ConvertNull(RDB_TYPE.SelectedValue)+" "+
					"WHERE CITY_ID = '"+DDL_CITY.SelectedValue+"' and ID_DEALER = '"+LBL_CODE.Text+"'";
							
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "INSERT INTO TDEALER(ID_DEALER, CITY_ID, DLI_CODE, NM_DEALER, DL_ADDR1, DL_ADDR2, DL_ADDR3, DL_CITY, "+
					"DL_ZIPCODE, DL_PHNAREA, DL_PHNNUM, DL_PHNEXT, DL_FAXAREA, DL_FAXNUM, DL_NPWP, DL_MANAGER, "+ 
					"DL_SALSUPVIS, DL_BANKADDR, DL_BANKADDR2, DL_BANKADDR3, DL_BANKCITY, DL_BANKZIPCODE, DL_ACCNUM, "+
					"DL_DISCPREMI, DL_DISCDEALER, DL_INTSUB, CH_STA, DL_SRCCODE, DL_TYPE, DL_BANKNM) "+
					"VALUES('"+LBL_CODE.Text+"', '"+DDL_CITY.SelectedValue+"', '"+DDL_DEALER.SelectedValue+"',"+
					" "+GlobalTools.ConvertNull(TXT_DLNAME.Text)+", "+GlobalTools.ConvertNull(TXT_ADDR1.Text)+", "+
					" "+GlobalTools.ConvertNull(TXT_ADDR2.Text)+", "+GlobalTools.ConvertNull(TXT_ADDR3.Text)+", "+GlobalTools.ConvertNull(TXT_CITY.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_ZIPCODE.Text)+", "+GlobalTools.ConvertNull(TXT_PH1.Text)+", "+GlobalTools.ConvertNull(TXT_PH2.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_PH3.Text)+", "+GlobalTools.ConvertNull(TXT_FX1.Text)+", "+GlobalTools.ConvertNull(TXT_FX2.Text)+","+ 
					" "+GlobalTools.ConvertNull(TXT_NPWP.Text)+", "+GlobalTools.ConvertNull(TXT_MANAGER.Text)+", "+GlobalTools.ConvertNull(TXT_SPV.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_BANK_ADDR1.Text)+", "+GlobalTools.ConvertNull(TXT_BANK_ADDR2.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_BANK_ADDR3.Text)+", "+GlobalTools.ConvertNull(TXT_BANK_CITY.Text)+", "+GlobalTools.ConvertNull(TXT_BANK_ZIPCODE.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_ACC_NO.Text)+", "+GlobalTools.ConvertFloat(TXT_DISC_PREMI.Text)+","+GlobalTools.ConvertFloat(TXT_DISC_DEALER.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_INT_SUB.Text)+", 2, "+GlobalTools.ConvertNull(TXT_CH_SRC.Text)+","+GlobalTools.ConvertNull(RDB_TYPE.SelectedValue)+", "+GlobalTools.ConvertNull(DDL_BANK_NAME.SelectedValue)+")"; 

				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				id = seq();
				seqid = createseq(id); 

				conn.QueryString = "INSERT INTO TDEALER(ID_DEALER, CITY_ID, DLI_CODE, NM_DEALER, DL_ADDR1, DL_ADDR2, DL_ADDR3, DL_CITY, "+
					"DL_ZIPCODE, DL_PHNAREA, DL_PHNNUM, DL_PHNEXT, DL_FAXAREA, DL_FAXNUM, DL_NPWP, DL_MANAGER, "+ 
					"DL_SALSUPVIS, DL_BANKADDR, DL_BANKADDR2, DL_BANKADDR3, DL_BANKCITY, DL_BANKZIPCODE, DL_ACCNUM, "+
					"DL_DISCPREMI, DL_DISCDEALER, DL_INTSUB, CH_STA, DL_SRCCODE, DL_TYPE, DL_BANKNM) "+
					"VALUES('"+seqid+"', '"+DDL_CITY.SelectedValue+"', '"+DDL_DEALER.SelectedValue+"',"+
					" "+GlobalTools.ConvertNull(TXT_DLNAME.Text)+", "+GlobalTools.ConvertNull(TXT_ADDR1.Text)+", "+
					" "+GlobalTools.ConvertNull(TXT_ADDR2.Text)+", "+GlobalTools.ConvertNull(TXT_ADDR3.Text)+", "+GlobalTools.ConvertNull(TXT_CITY.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_ZIPCODE.Text)+", "+GlobalTools.ConvertNull(TXT_PH1.Text)+", "+GlobalTools.ConvertNull(TXT_PH2.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_PH3.Text)+", "+GlobalTools.ConvertNull(TXT_FX1.Text)+", "+GlobalTools.ConvertNull(TXT_FX2.Text)+","+ 
					" "+GlobalTools.ConvertNull(TXT_NPWP.Text)+", "+GlobalTools.ConvertNull(TXT_MANAGER.Text)+", "+GlobalTools.ConvertNull(TXT_SPV.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_BANK_ADDR1.Text)+", "+GlobalTools.ConvertNull(TXT_BANK_ADDR2.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_BANK_ADDR3.Text)+", "+GlobalTools.ConvertNull(TXT_BANK_CITY.Text)+", "+GlobalTools.ConvertNull(TXT_BANK_ZIPCODE.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_ACC_NO.Text)+", "+GlobalTools.ConvertFloat(TXT_DISC_PREMI.Text)+","+GlobalTools.ConvertFloat(TXT_DISC_DEALER.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_INT_SUB.Text)+", 1, "+GlobalTools.ConvertNull(TXT_CH_SRC.Text)+","+GlobalTools.ConvertNull(RDB_TYPE.SelectedValue)+", "+GlobalTools.ConvertNull(DDL_BANK_NAME.SelectedValue)+")"; 

				conn.ExecuteQuery();
 
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
		}

		private void DG1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG1.CurrentPageIndex = e.NewPageIndex;
 
			BindData1(); 
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		private void DG1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string id, cid, dlicode, dname; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					id = e.Item.Cells[0].Text.Trim();
					cid = e.Item.Cells[1].Text.Trim();
					dlicode = e.Item.Cells[2].Text.Trim();
					dname = cleansText(e.Item.Cells[4].Text);

					conn.QueryString = "select * from TDEALER where CITY_ID = '"+cid+"' and ID_DEALER = '"+id+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "select * from DEALER where CITY_ID = '"+cid+"' and ID_DEALER = '"+id+"'";
						conn.ExecuteQuery();
						
						if(conn.GetRowCount() != 0) 
						{
							addr1 = conn.GetFieldValue(0,"DL_ADDR1");
							addr2 = conn.GetFieldValue(0,"DL_ADDR2");
							addr3 = conn.GetFieldValue(0,"DL_ADDR3");
							acc_no =  conn.GetFieldValue(0,"DL_ACCNUM");
							baddr1 = conn.GetFieldValue(0,"DL_BANKADDR"); 
							baddr2 = conn.GetFieldValue(0,"DL_BANKADDR2"); 
							baddr3 = conn.GetFieldValue(0,"DL_BANKADDR3");
							bcity = conn.GetFieldValue(0,"DL_BANKCITY"); 
							dzipcode = conn.GetFieldValue(0,"DL_ZIPCODE");
							bzipcode = conn.GetFieldValue(0,"DL_BANKZIPCODE");
							cty = conn.GetFieldValue(0,"DL_CITY");
							ddealer = conn.GetFieldValue(0,"DL_DISCDEALER");
							dpremi = conn.GetFieldValue(0,"DL_DISCPREMI");
							fx1 = conn.GetFieldValue(0,"DL_FAXAREA");
							fx2 = conn.GetFieldValue(0,"DL_FAXNUM");
							manager = conn.GetFieldValue(0,"DL_MANAGER");
							npwp = conn.GetFieldValue(0,"DL_NPWP");
							ph1 = conn.GetFieldValue(0,"DL_PHNAREA");
							ph2 = conn.GetFieldValue(0,"DL_PHNNUM");
							ph3 = conn.GetFieldValue(0,"DL_PHNEXT");
							insub = conn.GetFieldValue(0,"DL_INTSUB");
							src = conn.GetFieldValue(0,"DL_SRCCODE");
							spv = conn.GetFieldValue(0,"DL_SALSUPVIS");
							bid = conn.GetFieldValue(0,"DL_BANKNM");
							type = conn.GetFieldValue(0,"DL_TYPE");

							try
							{
								conn.QueryString = "INSERT INTO TDEALER(ID_DEALER, CITY_ID, DLI_CODE, NM_DEALER, DL_ADDR1, DL_ADDR2, DL_ADDR3, DL_CITY, "+
									"DL_ZIPCODE, DL_PHNAREA, DL_PHNNUM, DL_PHNEXT, DL_FAXAREA, DL_FAXNUM, DL_NPWP, DL_MANAGER, "+ 
									"DL_SALSUPVIS, DL_BANKADDR, DL_BANKADDR2, DL_BANKADDR3, DL_BANKCITY, DL_BANKZIPCODE, DL_ACCNUM, "+
									"DL_DISCPREMI, DL_DISCDEALER, DL_INTSUB, CH_STA, DL_SRCCODE, DL_TYPE, DL_BANKNM) "+
									"VALUES('"+id+"', '"+cid+"', '"+dlicode+"', "+GlobalTools.ConvertNull(dname)+", "+GlobalTools.ConvertNull(addr1)+", "+
									" "+GlobalTools.ConvertNull(addr2)+", "+GlobalTools.ConvertNull(addr3)+", "+GlobalTools.ConvertNull(cty)+", "+
									" "+GlobalTools.ConvertNull(dzipcode)+", "+GlobalTools.ConvertNull(ph1)+", "+GlobalTools.ConvertNull(ph2)+", "+
									" "+GlobalTools.ConvertNull(ph3)+", "+GlobalTools.ConvertNull(fx1)+", "+GlobalTools.ConvertNull(fx2)+", "+ 
									" "+GlobalTools.ConvertNull(npwp)+", "+GlobalTools.ConvertNull(manager)+", "+GlobalTools.ConvertNull(spv)+", "+
									" "+GlobalTools.ConvertNull(baddr1)+", "+GlobalTools.ConvertNull(baddr2)+", "+
									" "+GlobalTools.ConvertNull(baddr3)+", "+GlobalTools.ConvertNull(bcity)+", "+GlobalTools.ConvertNull(bzipcode)+", "+
									" "+GlobalTools.ConvertNull(acc_no)+", "+GlobalTools.ConvertFloat(dpremi)+", "+GlobalTools.ConvertFloat(ddealer)+", "+
									" "+GlobalTools.ConvertFloat(insub)+", 3, "+GlobalTools.ConvertNull(src)+", "+GlobalTools.ConvertNull(type)+", "+GlobalTools.ConvertNull(bid)+")"; 

								conn.ExecuteQuery();
							}
							catch{ }
						}

						BindData2();
					}

					break;

				case "edit":
					id = e.Item.Cells[0].Text.Trim();
					cid = e.Item.Cells[1].Text.Trim();
					dlicode = e.Item.Cells[2].Text.Trim();
					LBL_CODE.Text = id;

					conn.QueryString = "select * from DEALER where CITY_ID = '"+cid+"' and ID_DEALER = '"+id+"'";
					conn.ExecuteQuery(); 
					
					if(conn.GetRowCount() != 0) 
					{
						TXT_ADDR1.Text = conn.GetFieldValue(0,"DL_ADDR1");
						TXT_ADDR2.Text = conn.GetFieldValue(0,"DL_ADDR2");
						TXT_ADDR3.Text = conn.GetFieldValue(0,"DL_ADDR3");
						TXT_ACC_NO.Text =  conn.GetFieldValue(0,"DL_ACCNUM");
						TXT_BANK_ADDR1.Text = conn.GetFieldValue(0,"DL_BANKADDR"); 
						TXT_BANK_ADDR2.Text = conn.GetFieldValue(0,"DL_BANKADDR2"); 
						TXT_BANK_ADDR3.Text = conn.GetFieldValue(0,"DL_BANKADDR3");
						TXT_BANK_CITY.Text = conn.GetFieldValue(0,"DL_BANKCITY"); 
						TXT_BANK_ZIPCODE.Text = conn.GetFieldValue(0,"DL_BANKZIPCODE");
						TXT_ZIPCODE.Text = conn.GetFieldValue(0,"DL_ZIPCODE");
						TXT_DLNAME.Text = conn.GetFieldValue(0,"NM_DEALER");
						TXT_CITY.Text = conn.GetFieldValue(0,"DL_CITY");
						TXT_DISC_DEALER.Text = conn.GetFieldValue(0,"DL_DISCDEALER");
						TXT_DISC_PREMI.Text = conn.GetFieldValue(0,"DL_DISCPREMI");
						TXT_FX1.Text = conn.GetFieldValue(0,"DL_FAXAREA");
						TXT_FX2.Text = conn.GetFieldValue(0,"DL_FAXNUM");
						TXT_MANAGER.Text = conn.GetFieldValue(0,"DL_MANAGER");
						TXT_NPWP.Text = conn.GetFieldValue(0,"DL_NPWP");
						TXT_PH1.Text = conn.GetFieldValue(0,"DL_PHNAREA");
						TXT_PH2.Text = conn.GetFieldValue(0,"DL_PHNNUM");
						TXT_PH3.Text = conn.GetFieldValue(0,"DL_PHNEXT");
						TXT_INT_SUB.Text = GlobalTools.MoneyFormat(conn.GetFieldValue(0,"DL_INTSUB"));
						TXT_CH_SRC.Text = conn.GetFieldValue(0,"DL_SRCCODE");
						TXT_SPV.Text = conn.GetFieldValue(0,"DL_SALSUPVIS");
						
						try
						{
							DDL_DEALER.SelectedValue = dlicode;
						}
						catch{ }

						try
						{
							DDL_CITY.SelectedValue = cid;
						}
						catch{ }

						try
						{
							DDL_BANK_NAME.SelectedValue = conn.GetFieldValue(0,"DL_BANKNM");
						}
						catch{ }

						if(conn.GetFieldValue(0,"DL_TYPE") == "1") 
							RDB_TYPE.SelectedValue = "1";
						else
							RDB_TYPE.SelectedValue = "0";
						
					}

					LBL_SAVEMODE.Text = "2";
					DDL_CITY.Enabled = false; 
					
					break;
			}
		}

		private void DG2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string id, cid, dlicode; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					id = e.Item.Cells[0].Text.Trim();
					cid = e.Item.Cells[1].Text.Trim();

					conn.QueryString = "DELETE FROM TDEALER where CITY_ID = '"+cid+"' and ID_DEALER = '"+id+"'";
					conn.ExecuteQuery();

					BindData2();
					break;
				case "edit":
					id = e.Item.Cells[0].Text.Trim();
					cid = e.Item.Cells[1].Text.Trim();
					dlicode = e.Item.Cells[2].Text.Trim();
					LBL_CODE.Text = id;

					if(e.Item.Cells[8].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						conn.QueryString = "select * from DEALER where CITY_ID = '"+cid+"' and ID_DEALER = '"+id+"'";
						conn.ExecuteQuery(); 
					
						if(conn.GetRowCount() != 0) 
						{
							TXT_ADDR1.Text = conn.GetFieldValue(0,"DL_ADDR1");
							TXT_ADDR2.Text = conn.GetFieldValue(0,"DL_ADDR2");
							TXT_ADDR3.Text = conn.GetFieldValue(0,"DL_ADDR3");
							TXT_ACC_NO.Text =  conn.GetFieldValue(0,"DL_ACCNUM");
							TXT_BANK_ADDR1.Text = conn.GetFieldValue(0,"DL_BANKADDR"); 
							TXT_BANK_ADDR2.Text = conn.GetFieldValue(0,"DL_BANKADDR2"); 
							TXT_BANK_ADDR3.Text = conn.GetFieldValue(0,"DL_BANKADDR3");
							TXT_BANK_CITY.Text = conn.GetFieldValue(0,"DL_BANKCITY"); 
							TXT_BANK_ZIPCODE.Text = conn.GetFieldValue(0,"DL_BANKZIPCODE");
							TXT_ZIPCODE.Text = conn.GetFieldValue(0,"DL_ZIPCODE");
							TXT_DLNAME.Text = conn.GetFieldValue(0,"NM_DEALER");
							TXT_CITY.Text = conn.GetFieldValue(0,"DL_CITY");
							TXT_DISC_DEALER.Text = conn.GetFieldValue(0,"DL_DISCDEALER");
							TXT_DISC_PREMI.Text = conn.GetFieldValue(0,"DL_DISCPREMI");
							TXT_FX1.Text = conn.GetFieldValue(0,"DL_FAXAREA");
							TXT_FX2.Text = conn.GetFieldValue(0,"DL_FAXNUM");
							TXT_MANAGER.Text = conn.GetFieldValue(0,"DL_MANAGER");
							TXT_NPWP.Text = conn.GetFieldValue(0,"DL_NPWP");
							TXT_PH1.Text = conn.GetFieldValue(0,"DL_PHNAREA");
							TXT_PH2.Text = conn.GetFieldValue(0,"DL_PHNNUM");
							TXT_PH3.Text = conn.GetFieldValue(0,"DL_PHNEXT");
							TXT_INT_SUB.Text = GlobalTools.MoneyFormat(conn.GetFieldValue(0,"DL_INTSUB"));
							TXT_CH_SRC.Text = conn.GetFieldValue(0,"DL_SRCCODE");
							TXT_SPV.Text = conn.GetFieldValue(0,"DL_SALSUPVIS");
						
							try
							{
								DDL_DEALER.SelectedValue = dlicode;
							}
							catch{ }

							try
							{
								DDL_CITY.SelectedValue = cid;
							}
							catch{ }

							try
							{
								DDL_BANK_NAME.SelectedValue = conn.GetFieldValue(0,"BANKID");
							}
							catch{ }
						}
						
						LBL_SAVEMODE.Text = "2";
						DDL_CITY.Enabled = false; 
					}

					break;
			}
		}

		private void DG2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG2.CurrentPageIndex = e.NewPageIndex;

			BindData2(); 
		
		}

		protected void DDL_CITY_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1(); 
		}
	}
}
