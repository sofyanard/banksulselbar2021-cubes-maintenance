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
	/// Summary description for CityParam.
	/// </summary>
	public partial class CityParam : System.Web.UI.Page
	{
		protected string mid, addr;
		protected string ph1, ph2, ph3;
		protected string cp, sbc;
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
			
			GlobalTools.fillRefList(DDL_AREA,"select AREA_ID, AREA_NAME from AREA",conn);

			BindData1();
			BindData2();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData1()
		{
			conn.QueryString = "select *, isnull(PHONEAREA,'')+' '+isnull(PHONE,'')+' '+isnull(PHONEEXT,'') as PHONE "+  
				"from CITY where active='1' ORDER BY CITY_ID";
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
			conn.QueryString = "select *, isnull(PHONEAREA,'')+' '+isnull(PHONE,'')+' '+isnull(PHONEEXT,'') as PHONE, "+ 
				"STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end from TCITY ORDER BY CITY_ID";
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
			TXT_ADDR.Text = "";
			TXT_CP.Text = "";
			TXT_CITY.Text = ""; 
			TXT_SBC.Text = ""; 
			
			TXT_PH1.Text = "";
			TXT_PH2.Text = "";
			TXT_PH3.Text = "";
 
			DDL_AREA.Enabled = true;
 
			LBL_SAVEMODE.Text = "1";
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

			conn.QueryString = "select isnull(max(convert(int, CITY_ID)),0)+ "+LBL_NB.Text+" as MAX from CITY";
			
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

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string id = "", cty = ""; 
			int hit = 0;

			conn.QueryString = "select * from TCITY where CITY_ID = '"+LBL_CID.Text+"' and AREA_ID = '"+DDL_AREA.SelectedValue+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "UPDATE TCITY SET CITY_NAME = "+GlobalTools.ConvertNull(TXT_CITY.Text)+", PHONEAREA = "+GlobalTools.ConvertNull(TXT_PH1.Text)+", "+
					"PHONE = "+GlobalTools.ConvertNull(TXT_PH2.Text)+", PHONEEXT = "+GlobalTools.ConvertNull(TXT_PH3.Text)+", CONTACT_PERSON = "+GlobalTools.ConvertNull(TXT_CP.Text)+", "+
					"ADDRESS = "+GlobalTools.ConvertNull(TXT_ADDR.Text)+", SIBS_CODE = "+GlobalTools.ConvertNull(TXT_SBC.Text)+" WHERE  CITY_ID = '"+LBL_CID.Text+"' and AREA_ID = '"+DDL_AREA.SelectedValue+"'";
							
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "insert into TCITY (CITY_ID, AREA_ID, CITY_NAME, PHONEAREA, PHONE, PHONEEXT, CONTACT_PERSON, ADDRESS, CD_SIBS, CH_STA)"+
					" values ('"+LBL_CID.Text+"', '"+DDL_AREA.SelectedValue+"', "+GlobalTools.ConvertNull(TXT_CITY.Text)+", "+GlobalTools.ConvertNull(TXT_PH1.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_PH2.Text)+","+ 
					" "+GlobalTools.ConvertNull(TXT_PH3.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_CP.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_ADDR.Text)+", "+GlobalTools.ConvertNull(TXT_SBC.Text)+", 2)"; 

				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				id = seq();
				cty = createseq(id); 

				conn.QueryString = "insert into TCITY (CITY_ID, AREA_ID, CITY_NAME, PHONEAREA, PHONE, PHONEEXT, CONTACT_PERSON, ADDRESS, CD_SIBS, CH_STA)"+
					" values ('"+cty+"', '"+DDL_AREA.SelectedValue+"', "+GlobalTools.ConvertNull(TXT_CITY.Text)+", "+GlobalTools.ConvertNull(TXT_PH1.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_PH2.Text)+","+ 
					" "+GlobalTools.ConvertNull(TXT_PH3.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_CP.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_ADDR.Text)+", "+GlobalTools.ConvertNull(TXT_SBC.Text)+", 1)"; 

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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleID=40");
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		private void DG1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG1.CurrentPageIndex = e.NewPageIndex;
			
			BindData1(); 	
		}

		private void DG1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string cid, aid, cname; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					cid = e.Item.Cells[1].Text.Trim();
					aid = e.Item.Cells[0].Text.Trim();
					cname = cleansText(e.Item.Cells[2].Text); 

					conn.QueryString = "select * from TCITY where CITY_ID = '"+cid+"' and AREA_ID = '"+aid+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "select * from CITY where CITY_ID = '"+cid+"' and AREA_ID = '"+aid+"'";
						conn.ExecuteQuery();
						
						if(conn.GetRowCount() != 0) 
						{
							addr = conn.GetFieldValue(0,"ADDRESS");
							cp =  conn.GetFieldValue(0,"CONTACT_PERSON");
							ph1 = conn.GetFieldValue(0,"PHONEAREA"); 
							ph2 = conn.GetFieldValue(0,"PHONE"); 
							ph3 = conn.GetFieldValue(0,"PHONEEXT");
							sbc = conn.GetFieldValue(0,"CD_SIBS"); 						

							try
							{
								conn.QueryString = "insert into TCITY (CITY_ID, AREA_ID, CITY_NAME, PHONEAREA, PHONE, PHONEEXT, CONTACT_PERSON, ADDRESS, CD_SIBS, CH_STA) "+
									"values ('"+cid+"', '"+aid+"', "+GlobalTools.ConvertNull(cname)+", "+GlobalTools.ConvertNull(ph1)+", "+GlobalTools.ConvertNull(ph2)+","+ 
									" "+GlobalTools.ConvertNull(ph3)+", "+GlobalTools.ConvertNull(cp)+", "+GlobalTools.ConvertNull(addr)+", "+GlobalTools.ConvertNull(sbc)+", 3)"; 

								conn.ExecuteQuery();
							}
							catch{ }
						}

						BindData2();
					}

					break;

				case "edit":
					cid = e.Item.Cells[1].Text.Trim();
					aid = e.Item.Cells[0].Text.Trim();
					cname = cleansText(e.Item.Cells[2].Text); 

					conn.QueryString = "select * from CITY where CITY_ID = '"+cid+"' and AREA_ID =  '"+aid+"'";
					conn.ExecuteQuery(); 
					
					if(conn.GetRowCount() != 0) 
					{
						TXT_ADDR.Text = conn.GetFieldValue(0,"ADDRESS");
							
						TXT_CP.Text = conn.GetFieldValue(0,"CONTACT_PERSON");
							
						TXT_PH1.Text = conn.GetFieldValue(0,"PHONEAREA"); 
						TXT_PH2.Text = conn.GetFieldValue(0,"PHONE"); 
						TXT_PH3.Text = conn.GetFieldValue(0,"PHONEEXT");

						TXT_SBC.Text = conn.GetFieldValue(0,"CD_SIBS"); 							
					}

					TXT_CITY.Text = cname; 

					try
					{
						DDL_AREA.SelectedValue = aid;   
					}
					catch{ }

					DDL_AREA.Enabled = false;

					LBL_SAVEMODE.Text = "2";
					LBL_CID.Text = cid;
 
					break;
			}
		
		}

		private void DG2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG2.CurrentPageIndex = e.NewPageIndex;
			
			BindData2(); 		
		}

		private void DG2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string cid, aid, cname;  

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					cid = e.Item.Cells[1].Text.Trim();
					aid = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM TCITY WHERE CITY_ID = '"+cid+"' AND AREA_ID = '"+aid+"'";
					conn.ExecuteQuery();

					BindData2();
					break;
				case "edit":
					cid = e.Item.Cells[1].Text.Trim();
					aid = e.Item.Cells[0].Text.Trim();
					cname = cleansText(e.Item.Cells[2].Text); 

					if(e.Item.Cells[6].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						conn.QueryString = "select * from TCITY where CITY_ID = '"+cid+"' and AREA_ID =  '"+aid+"'";
						conn.ExecuteQuery(); 
					
						if(conn.GetRowCount() != 0) 
						{
							TXT_ADDR.Text = conn.GetFieldValue(0,"ADDRESS");
							
							TXT_CP.Text = conn.GetFieldValue(0,"CONTACT_PERSON");
							
							TXT_PH1.Text = conn.GetFieldValue(0,"PHONEAREA"); 
							TXT_PH2.Text = conn.GetFieldValue(0,"PHONE"); 
							TXT_PH3.Text = conn.GetFieldValue(0,"PHONEEXT");

							TXT_SBC.Text = conn.GetFieldValue(0,"CD_SIBS"); 						
						}

						TXT_CITY.Text = cname; 

						try
						{
							DDL_AREA.SelectedValue = aid;   
						}
						catch{ }
						
						LBL_SAVEMODE.Text = "2";
						LBL_CID.Text = cid;	
						DDL_AREA.Enabled = false;
					}

					break;
			}
		}
	}
}
