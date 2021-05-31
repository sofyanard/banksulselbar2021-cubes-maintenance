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
	/// Summary description for InvestigatorAgencyParam.
	/// </summary>
	public partial class InvestigatorAgencyParam : System.Web.UI.Page
	{
		protected string mid, agname, addr1, addr2, addr3;
		protected string ph1, ph2, ph3, fn1, fn2, city;
		protected string cp, hp, zipcode, mktsrc, email;
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
				que = "select CITY_ID, CITY_NAME from CITY where AREA_ID = '"+arid+"'";
			}
			else
			{
				que = "select CITY_ID, CITY_NAME from CITY";
			}
			
			GlobalTools.fillRefList(DDL_CITY,que,false,conn);

			BindData1();
			BindData2();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData1()
		{
			conn.QueryString = "select AGENCYID, AGENCYTYPE, AGENCYNAME, CITY_ID, "+ 
				"isnull(ADDRESS1,'')+' '+isnull(ADDRESS2,'')+' '+isnull(ADDRESS3,'') ADDRESS "+   
				"from TAGENCY where active='1' and AGENCYTYPE = '3' "+ 
				"and CITY_ID = '"+DDL_CITY.SelectedValue+"'";
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
			conn.QueryString = "select AGENCYID, AGENCYTYPE, AGENCYNAME, CITY_ID, "+
				"isnull(ADDRESS1,'')+' '+isnull(ADDRESS2,'')+' '+isnull(ADDRESS3,'') ADDRESS, "+  
				"CH_STA, STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' "+ 
				"when '3' then 'DELETE' end from TTAGENCY where AGENCYTYPE = '3'";
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

		private void ClearEditBoxes()
		{
			TXT_ADDR1.Text = "";
			TXT_ADDR2.Text = "";
			TXT_ADDR3.Text = "";
			//TXT_AGENCY_CODE.Text = "";
			TXT_AGENCY_NAME.Text = "";
			TXT_CP.Text = "";
			TXT_EMAIL.Text = "";
			TXT_FN1.Text = "";
			TXT_FN2.Text = "";
			TXT_HP.Text = "";
			TXT_MKT_SOURCE_CODE.Text = "";
			TXT_ZIPCODE.Text = "";
			TXT_PN1.Text = "";
			TXT_PN2.Text = "";
			TXT_PN3.Text = ""; 
 
			DDL_CITY.Enabled = true;
			TXT_AGENCY_CODE.Enabled = true;
 
			LBL_SAVE.Text = "1";
		}

		private void seq(string mode)
		{
			string seqid = "";
			int number = Int16.Parse(LBL_NB.Text);

			if(mode == "1")
			{
				number++;
			}

			LBL_NB.Text = number.ToString();

			conn.QueryString = "select isnull(max(AGENCYID),0)+ "+LBL_NB.Text+" as MAXSEQ from TAGENCY where AGENCYTYPE = '3' and CITY_ID = '"+DDL_CITY.SelectedValue+"'";
			
			conn.ExecuteQuery();

			seqid = conn.GetFieldValue("MAXSEQ");
			
			if(seqid.Length == 1)
				seqid = "000" + seqid;
			else if(seqid.Length == 2)
				seqid = "00" + seqid;
			else if(seqid.Length == 3)
				seqid = "0" + seqid;

			TXT_AGENCY_CODE.Text = seqid;

			conn.ClearData();
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
			int hit = 0;

			conn.QueryString = "SELECT * FROM TTAGENCY WHERE AGENCYID = '"+TXT_AGENCY_CODE.Text+"' AND AGENCYTYPE = '3' AND CITY_ID = '"+DDL_CITY.SelectedValue+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVE.Text != "1"))
			{
				conn.QueryString = "UPDATE TTAGENCY SET AGENCYNAME = "+GlobalTools.ConvertNull(TXT_AGENCY_NAME.Text)+", "+
					"ADDRESS1 = "+GlobalTools.ConvertNull(TXT_ADDR1.Text)+", ADDRESS2 = "+GlobalTools.ConvertNull(TXT_ADDR2.Text)+", "+
					"ADDRESS3 = "+GlobalTools.ConvertNull(TXT_ADDR3.Text)+", CITY = "+GlobalTools.ConvertNull(DDL_CITY.SelectedItem.Text)+", "+ 
					"ZIPCODE = "+GlobalTools.ConvertNull(TXT_ZIPCODE.Text)+", EMAIL = "+GlobalTools.ConvertNull(TXT_EMAIL.Text)+", "+ 
					"CONTACTPERSON = "+GlobalTools.ConvertNull(TXT_CP.Text)+", CONTACTHP = "+GlobalTools.ConvertNull(TXT_HP.Text)+", "+
					"PHONEAREA = "+GlobalTools.ConvertNull(TXT_PN1.Text)+", PHONE = "+GlobalTools.ConvertNull(TXT_PN2.Text)+", "+ 
					"PHONEEXT = "+GlobalTools.ConvertNull(TXT_PN3.Text)+", FAXAREA = "+GlobalTools.ConvertNull(TXT_FN1.Text)+", "+ 
					"FAXNO = "+GlobalTools.ConvertNull(TXT_FN2.Text)+", AG_SRCCODE = "+GlobalTools.ConvertNull(TXT_MKT_SOURCE_CODE.Text)+" "+ 
					"WHERE AGENCYID = '"+TXT_AGENCY_CODE.Text+"' AND AGENCYTYPE = '3' AND CITY_ID = '"+DDL_CITY.SelectedValue+"'";
							
				conn.ExecuteQuery();	

				ClearEditBoxes(); 

				seq("0");
			}
			else if((hit == 0) && (LBL_SAVE.Text == "2"))
			{
				conn.QueryString = "insert into TTAGENCY(AGENCYID,AGENCYTYPE,AGENCYNAME,CITY_ID,ADDRESS1,ADDRESS2,ADDRESS3,CITY,ZIPCODE, "+                                                                                                
					"EMAIL,CONTACTPERSON,CONTACTHP,PHONEAREA,PHONE,PHONEEXT,FAXAREA,FAXNO,AG_SRCCODE,CH_STA) "+
					" values ('"+TXT_AGENCY_CODE.Text+"','3',"+GlobalTools.ConvertNull(TXT_AGENCY_NAME.Text)+",'"+DDL_CITY.SelectedValue+"',"+
					" "+GlobalTools.ConvertNull(TXT_ADDR1.Text)+","+GlobalTools.ConvertNull(TXT_ADDR2.Text)+","+GlobalTools.ConvertNull(TXT_ADDR3.Text)+","+
					" "+GlobalTools.ConvertNull(DDL_CITY.SelectedItem.Text)+","+GlobalTools.ConvertNull(TXT_ZIPCODE.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_EMAIL.Text)+","+GlobalTools.ConvertNull(TXT_CP.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_HP.Text)+","+GlobalTools.ConvertNull(TXT_PN1.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_PN2.Text)+","+GlobalTools.ConvertNull(TXT_PN3.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_FN1.Text)+","+GlobalTools.ConvertNull(TXT_FN2.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_MKT_SOURCE_CODE.Text)+", 2)";

				conn.ExecuteQuery();
 
				ClearEditBoxes(); 

				seq("0");
			}
			else if((hit == 0) && (LBL_SAVE.Text == "1"))
			{
				conn.QueryString = "insert into TTAGENCY(AGENCYID,AGENCYTYPE,AGENCYNAME,CITY_ID,ADDRESS1,ADDRESS2,ADDRESS3,CITY,ZIPCODE, "+                                                                                                
					"EMAIL,CONTACTPERSON,CONTACTHP,PHONEAREA,PHONE,PHONEEXT,FAXAREA,FAXNO,AG_SRCCODE,CH_STA) "+
					" values ('"+TXT_AGENCY_CODE.Text+"','3',"+GlobalTools.ConvertNull(TXT_AGENCY_NAME.Text)+",'"+DDL_CITY.SelectedValue+"',"+
					" "+GlobalTools.ConvertNull(TXT_ADDR1.Text)+","+GlobalTools.ConvertNull(TXT_ADDR2.Text)+","+GlobalTools.ConvertNull(TXT_ADDR3.Text)+","+
					" "+GlobalTools.ConvertNull(DDL_CITY.SelectedItem.Text)+","+GlobalTools.ConvertNull(TXT_ZIPCODE.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_EMAIL.Text)+","+GlobalTools.ConvertNull(TXT_CP.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_HP.Text)+","+GlobalTools.ConvertNull(TXT_PN1.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_PN2.Text)+","+GlobalTools.ConvertNull(TXT_PN3.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_FN1.Text)+","+GlobalTools.ConvertNull(TXT_FN2.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_MKT_SOURCE_CODE.Text)+", 1)";
				
				try
				{
					conn.ExecuteQuery();
					seq("1");
				}
				catch
				{
					GlobalTools.popMessage(this,"Cannot insert same code, request canceled!"); 
				}
				finally
				{
					ClearEditBoxes(); 
				}
			}
			else if((hit != 0) && (LBL_SAVE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
				return;
			}

			conn.ClearData();
 	
			BindData2();

			LBL_SAVE.Text = "1"; 
					
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleID=40");
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 			
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string id, type, cid, name; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					id = e.Item.Cells[0].Text.Trim();
					type = e.Item.Cells[1].Text.Trim();
					cid = e.Item.Cells[4].Text.Trim();
					name = cleansText(e.Item.Cells[2].Text); 

					conn.QueryString = "SELECT * FROM TTAGENCY WHERE AGENCYID = '"+id+"' AND AGENCYTYPE = '"+type+"' AND CITY_ID = '"+cid+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "SELECT * FROM TAGENCY WHERE AGENCYID = '"+id+"' AND AGENCYTYPE = '"+type+"' AND CITY_ID = '"+cid+"'";
						conn.ExecuteQuery();
						
						if(conn.GetRowCount() != 0) 
						{
							addr1 = conn.GetFieldValue(0,"ADDRESS1");
							addr2 = conn.GetFieldValue(0,"ADDRESS2");
							addr3 = conn.GetFieldValue(0,"ADDRESS3");	
							cp =  conn.GetFieldValue(0,"CONTACTPERSON");
							email = conn.GetFieldValue(0,"EMAIL"); 
							fn1 = conn.GetFieldValue(0,"FAXAREA");
							fn2 = conn.GetFieldValue(0,"FAXNO"); 
							ph1 = conn.GetFieldValue(0,"PHONEAREA"); 
							ph2 = conn.GetFieldValue(0,"PHONE"); 
							ph3 = conn.GetFieldValue(0,"PHONEEXT"); 
							hp = conn.GetFieldValue(0,"CONTACTHP");
							zipcode = conn.GetFieldValue(0,"ZIPCODE");
							mktsrc = conn.GetFieldValue(0,"AG_SRCCODE");
							city = conn.GetFieldValue(0,"CITY");

							try
							{
								conn.QueryString = "insert into TTAGENCY(AGENCYID,AGENCYTYPE,AGENCYNAME,CITY_ID,ADDRESS1,ADDRESS2,ADDRESS3,CITY,ZIPCODE,"+                                                                                                
									"EMAIL,CONTACTPERSON,CONTACTHP,PHONEAREA,PHONE,PHONEEXT,FAXAREA,FAXNO,AG_SRCCODE,CH_STA) "+
									"values ('"+id+"','"+type+"',"+GlobalTools.ConvertNull(name)+",'"+cid+"',"+
									" "+GlobalTools.ConvertNull(addr1)+","+GlobalTools.ConvertNull(addr2)+","+GlobalTools.ConvertNull(addr3)+","+
									" "+GlobalTools.ConvertNull(city)+","+GlobalTools.ConvertNull(zipcode)+","+
									" "+GlobalTools.ConvertNull(email)+","+GlobalTools.ConvertNull(cp)+","+
									" "+GlobalTools.ConvertNull(hp)+","+GlobalTools.ConvertNull(ph1)+","+
									" "+GlobalTools.ConvertNull(ph2.Replace(","," "))+","+GlobalTools.ConvertNull(ph3)+","+
									" "+GlobalTools.ConvertNull(fn1)+","+GlobalTools.ConvertNull(fn2)+","+
									" "+GlobalTools.ConvertNull(mktsrc)+", 3)";

								conn.ExecuteQuery();
							}
							catch{ }
						}

						BindData2();
					}
					break;

				case "edit":
					id = e.Item.Cells[0].Text.Trim();
					type = e.Item.Cells[1].Text.Trim();
					cid = e.Item.Cells[4].Text.Trim();

					conn.QueryString = "SELECT * FROM TAGENCY WHERE AGENCYID = '"+id+"' AND AGENCYTYPE = '"+type+"' AND CITY_ID = '"+cid+"'";
					conn.ExecuteQuery(); 
					
					if(conn.GetRowCount() != 0) 
					{
						TXT_ADDR1.Text = conn.GetFieldValue(0,"ADDRESS1");
						TXT_ADDR2.Text = conn.GetFieldValue(0,"ADDRESS2");
						TXT_ADDR3.Text = conn.GetFieldValue(0,"ADDRESS3");	
						TXT_CP.Text =  conn.GetFieldValue(0,"CONTACTPERSON");
						TXT_EMAIL.Text = conn.GetFieldValue(0,"EMAIL"); 
						TXT_FN1.Text = conn.GetFieldValue(0,"FAXAREA");
						TXT_FN2.Text = conn.GetFieldValue(0,"FAXNO"); 
						TXT_PN1.Text = conn.GetFieldValue(0,"PHONEAREA"); 
						TXT_PN2.Text = conn.GetFieldValue(0,"PHONE"); 
						TXT_PN3.Text = conn.GetFieldValue(0,"PHONEEXT"); 
						TXT_HP.Text = conn.GetFieldValue(0,"CONTACTHP");
						TXT_ZIPCODE.Text = conn.GetFieldValue(0,"ZIPCODE");
						TXT_MKT_SOURCE_CODE.Text = conn.GetFieldValue(0,"AG_SRCCODE"); 	
					}

					try
					{
						DDL_CITY.SelectedValue = e.Item.Cells[4].Text.Trim();   
					}
					catch{ }

					TXT_AGENCY_CODE.Text = e.Item.Cells[0].Text.Trim();  
					TXT_AGENCY_NAME.Text = cleansText(e.Item.Cells[2].Text); 

					DDL_CITY.Enabled = false;
					TXT_AGENCY_CODE.Enabled = false;

					LBL_SAVE.Text = "2";

					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 		
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string id, type, cid; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					id = e.Item.Cells[0].Text.Trim();
					type = e.Item.Cells[1].Text.Trim();
					cid = e.Item.Cells[4].Text.Trim();

					conn.QueryString = "DELETE FROM TTAGENCY WHERE AGENCYID = '"+id+"' AND AGENCYTYPE = '"+type+"' AND CITY_ID = '"+cid+"'";
					conn.ExecuteQuery();

					BindData2();
					break;
				case "edit":
					id = e.Item.Cells[0].Text.Trim();
					type = e.Item.Cells[1].Text.Trim();
					cid = e.Item.Cells[4].Text.Trim();

					if(e.Item.Cells[6].Text.Trim() == "3")
					{
						LBL_SAVE.Text = "1";
					}
					else
					{
						conn.QueryString = "SELECT * FROM TTAGENCY WHERE AGENCYID = '"+id+"' AND AGENCYTYPE = '"+type+"' AND CITY_ID = '"+cid+"'";
						conn.ExecuteQuery(); 
					
						if(conn.GetRowCount() != 0) 
						{
							TXT_ADDR1.Text = conn.GetFieldValue(0,"ADDRESS1");
							TXT_ADDR2.Text = conn.GetFieldValue(0,"ADDRESS2");
							TXT_ADDR3.Text = conn.GetFieldValue(0,"ADDRESS3");	
							TXT_CP.Text =  conn.GetFieldValue(0,"CONTACTPERSON");
							TXT_EMAIL.Text = conn.GetFieldValue(0,"EMAIL"); 
							TXT_FN1.Text = conn.GetFieldValue(0,"FAXAREA");
							TXT_FN2.Text = conn.GetFieldValue(0,"FAXNO"); 
							TXT_PN1.Text = conn.GetFieldValue(0,"PHONEAREA"); 
							TXT_PN2.Text = conn.GetFieldValue(0,"PHONE"); 
							TXT_PN3.Text = conn.GetFieldValue(0,"PHONEEXT"); 
							TXT_HP.Text = conn.GetFieldValue(0,"CONTACTHP");
							TXT_ZIPCODE.Text = conn.GetFieldValue(0,"ZIPCODE");
							TXT_MKT_SOURCE_CODE.Text = conn.GetFieldValue(0,"AG_SRCCODE"); 	
						}

						try
						{
							DDL_CITY.SelectedValue = e.Item.Cells[4].Text.Trim();   
						}
						catch{ }

						TXT_AGENCY_CODE.Text = e.Item.Cells[0].Text.Trim();
						TXT_AGENCY_NAME.Text = cleansText(e.Item.Cells[2].Text);    

						LBL_SAVE.Text = "2";
	
						DDL_CITY.Enabled = false;
						TXT_AGENCY_CODE.Enabled = false;
					}
					break;
			}
		}

		protected void DDL_CITY_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1(); 
			seq("0");
		}
	}
}
