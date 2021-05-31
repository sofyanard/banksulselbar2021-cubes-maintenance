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
	/// Summary description for HouseInfo.
	/// </summary>
	public partial class HouseInfo : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid, occ_type, bed, bath;
		protected string garage, land, build, phone, elec;
		protected string pam, year, bmarket, lmarket, bapp1, bapp2;
		protected string lapp1, lapp2, bforce1, bforce2;
		protected string lforce1, lforce2, remarks, currency;
	
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
			string que = "";
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			LBL_SAVEMODE.Text = "1"; 

			InitialCon(); 
			
			que = "select CITY_ID, CITY_NAME from CITY";
			
			GlobalTools.fillRefList(DDL_CITY,que,false,conn);

			fillProject(); 
			fillHouse();

			BindData1();
			BindData2();
		}

		private void fillProject()
		{
			DDL_PROJECT_CODE.Items.Clear();

			DDL_PROJECT_CODE.Items.Add(new ListItem("- SELECT -",""));
  
			conn.QueryString = "select PROYEK_ID, PROYEK_DESCRIPTION from PROYEK_HOUSINGLOAN "+ 
					"where ID_KOTA = '"+DDL_CITY.SelectedValue+"'";
			conn.ExecuteQuery();

			for(int i=0; i < conn.GetRowCount(); i++) 
			{
				DDL_PROJECT_CODE.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			}

			conn.ClearData(); 
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData1()
		{	
			conn.QueryString = "select a.PROYEK_ID, b.PROYEK_DESCRIPTION as PROYEK_NAME, a.TYPE_CODE, c.TYPE_NAME as TIPE, "+
				"a.TYPE_DESCRIPTION, b.ID_KOTA from HOUSEINFORMATION a, proyek_housingloan b, HOUSE_TYPE c "+
				"where a.active='1' and a.proyek_id = b.proyek_id and c.type_code = a.type_code "+
				"and b.ID_KOTA = '"+DDL_CITY.SelectedValue+"' and a.PROYEK_ID = '"+DDL_PROJECT_CODE.SelectedValue+"'";

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

			for (int i = 0; i < DGR_EXISTING.Items.Count; i++)
				DGR_EXISTING.Items[i].Cells[0].Text = (i+1+(DGR_EXISTING.CurrentPageIndex*DGR_EXISTING.PageSize)).ToString();

			conn.ClearData();
		}

		private void BindData2()
		{
			conn.QueryString = "select a.PROYEK_ID, b.PROYEK_DESCRIPTION as PROYEK_NAME, a.TYPE_CODE, c.TYPE_NAME as TIPE, "+
					"a.TYPE_DESCRIPTION , a.CH_STA, STATUS = case a.CH_STA when '1' then 'INSERT' "+
					"when '2' then 'UPDATE' when '3' then 'DELETE' end, b.ID_KOTA "+
					"from THOUSEINFORMATION a, proyek_housingloan b, HOUSE_TYPE c "+
					"where a.proyek_id = b.proyek_id and c.type_code = a.type_code"; 

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

			for (int i = 0; i < DGR_REQUEST.Items.Count; i++)
				DGR_REQUEST.Items[i].Cells[0].Text = (i+1+(DGR_REQUEST.CurrentPageIndex*DGR_REQUEST.PageSize)).ToString();
		}

		private void fillHouse()
		{
			DDL_HOUSE_TYPE.Items.Clear();
  
			conn.QueryString = "select type_code, type_name from HOUSE_TYPE where ACTIVE = '1'";
			conn.ExecuteQuery();

			for(int i=0; i < conn.GetRowCount(); i++) 
			{
				DDL_HOUSE_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			}

			conn.ClearData(); 
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ClearEditBoxes()
		{
			DDL_PROJECT_CODE.Enabled = true;
			DDL_CITY.Enabled = true;
			DDL_HOUSE_TYPE.Enabled = true;
 
			TXT_BATH.Text = "";
			TXT_BED.Text = "";
			TXT_BUILD_APPR_1.Text = "0";
			TXT_BUILD_APPR_2.Text = "0";
			
			TXT_BUILD_FORCE_1.Text = "0";
			TXT_BUILD_FORCE_2.Text = "0";
			
			TXT_LAND_APPR_1.Text = "0";
			TXT_LAND_APPR_2.Text = "0";
			
			TXT_LAND_FORCE_1.Text = "0";
			TXT_LAND_FORCE_2.Text = "0"; 
			
			TXT_BUILDING_AREA.Text = "0";
			TXT_LAND_AREA.Text = "0";

			TXT_LAND_MARKET.Text = "0";
			TXT_MARKET_PRICE.Text = "0";
			TXT_MATAUANG.Text = "";
			TXT_OCCUPANCY.Text = "";
			TXT_REMARKS.Text = "";
			TXT_TYPENAME.Text = "";
			TXT_YEAR_BUILD.Text = ""; 
			TXT_ELEC.Text = ""; 

			CB_GARAGE.Checked = false;
			CB_PAM.Checked = false;
			CB_PHONE.Checked = false; 
			 
			LBL_SAVEMODE.Text = "1";
		}

		private string cleansFloat(string tn)
		{
			if (tn.Trim() == "&nbsp;")
				tn = "0";
			return tn;
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
			Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleID=40");	
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string grg = "", ph = "", pm = "";
			int hit = 0;

			if(CB_GARAGE.Checked)
				grg = "1";
			else
				grg = "2";

			if(CB_PAM.Checked)
				pm = "Ada";
			else
				pm = "Tidak Ada";

			if(CB_PHONE.Checked)
				ph = "Ada";
			else
				ph = "Tidak Ada";

			conn.QueryString = "SELECT PROYEK_ID, TYPE_CODE FROM THOUSEINFORMATION WHERE PROYEK_ID = '"+DDL_PROJECT_CODE.SelectedValue+
				"' AND TYPE_CODE = '"+DDL_HOUSE_TYPE.SelectedValue+"'"; 
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "EXEC PARAM_AREA_HOUSEINFO_MAKER '4', '"+DDL_PROJECT_CODE.SelectedValue+"' "+
					",'"+DDL_HOUSE_TYPE.SelectedValue+"',"+GlobalTools.ConvertNull(TXT_TYPENAME.Text)+" "+
					","+GlobalTools.ConvertNull(TXT_OCCUPANCY.Text)+", "+GlobalTools.ConvertNull(TXT_BED.Text)+" "+
					","+GlobalTools.ConvertNull(TXT_BATH.Text)+", "+GlobalTools.ConvertNull(grg)+" "+
					","+GlobalTools.ConvertNull(TXT_LAND_AREA.Text)+", "+GlobalTools.ConvertNull(TXT_BUILDING_AREA.Text)+" "+ 
					","+GlobalTools.ConvertNull(ph)+", "+GlobalTools.ConvertNull(TXT_ELEC.Text)+" "+
					","+GlobalTools.ConvertNull(pm)+", "+GlobalTools.ConvertNull(TXT_YEAR_BUILD.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_MARKET_PRICE.Text)+", "+GlobalTools.ConvertFloat(TXT_LAND_MARKET.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_BUILD_APPR_1.Text)+", "+GlobalTools.ConvertFloat(TXT_BUILD_APPR_2.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_LAND_APPR_1.Text)+", "+GlobalTools.ConvertFloat(TXT_LAND_APPR_2.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_BUILD_FORCE_1.Text)+", "+GlobalTools.ConvertFloat(TXT_BUILD_FORCE_2.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_LAND_FORCE_1.Text)+", "+GlobalTools.ConvertFloat(TXT_LAND_FORCE_2.Text)+" "+
					","+GlobalTools.ConvertNull(TXT_REMARKS.Text)+", "+GlobalTools.ConvertNull(TXT_MATAUANG.Text)+" "+
					","+GlobalTools.ConvertNull(DDL_CITY.SelectedValue);  

				conn.ExecuteNonQuery();

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "EXEC PARAM_AREA_HOUSEINFO_MAKER '2', '"+DDL_PROJECT_CODE.SelectedValue+"' "+
					",'"+DDL_HOUSE_TYPE.SelectedValue+"',"+GlobalTools.ConvertNull(TXT_TYPENAME.Text)+" "+
					","+GlobalTools.ConvertNull(TXT_OCCUPANCY.Text)+", "+GlobalTools.ConvertNull(TXT_BED.Text)+" "+
					","+GlobalTools.ConvertNull(TXT_BATH.Text)+", "+GlobalTools.ConvertNull(grg)+" "+
					","+GlobalTools.ConvertNull(TXT_LAND_AREA.Text)+", "+GlobalTools.ConvertNull(TXT_BUILDING_AREA.Text)+" "+ 
					","+GlobalTools.ConvertNull(ph)+", "+GlobalTools.ConvertNull(TXT_ELEC.Text)+" "+
					","+GlobalTools.ConvertNull(pm)+", "+GlobalTools.ConvertNull(TXT_YEAR_BUILD.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_MARKET_PRICE.Text)+", "+GlobalTools.ConvertFloat(TXT_LAND_MARKET.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_BUILD_APPR_1.Text)+", "+GlobalTools.ConvertFloat(TXT_BUILD_APPR_2.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_LAND_APPR_1.Text)+", "+GlobalTools.ConvertFloat(TXT_LAND_APPR_2.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_BUILD_FORCE_1.Text)+", "+GlobalTools.ConvertFloat(TXT_BUILD_FORCE_2.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_LAND_FORCE_1.Text)+", "+GlobalTools.ConvertFloat(TXT_LAND_FORCE_2.Text)+" "+
					","+GlobalTools.ConvertNull(TXT_REMARKS.Text)+", "+GlobalTools.ConvertNull(TXT_MATAUANG.Text)+" "+
					","+GlobalTools.ConvertNull(DDL_CITY.SelectedValue);  

				conn.ExecuteNonQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				conn.QueryString = "EXEC PARAM_AREA_HOUSEINFO_MAKER '1', '"+DDL_PROJECT_CODE.SelectedValue+"' "+
					",'"+DDL_HOUSE_TYPE.SelectedValue+"',"+GlobalTools.ConvertNull(TXT_TYPENAME.Text)+" "+
					","+GlobalTools.ConvertNull(TXT_OCCUPANCY.Text)+", "+GlobalTools.ConvertNull(TXT_BED.Text)+" "+
					","+GlobalTools.ConvertNull(TXT_BATH.Text)+", "+GlobalTools.ConvertNull(grg)+" "+
					","+GlobalTools.ConvertNull(TXT_LAND_AREA.Text)+", "+GlobalTools.ConvertNull(TXT_BUILDING_AREA.Text)+" "+ 
					","+GlobalTools.ConvertNull(ph)+", "+GlobalTools.ConvertNull(TXT_ELEC.Text)+" "+
					","+GlobalTools.ConvertNull(pm)+", "+GlobalTools.ConvertNull(TXT_YEAR_BUILD.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_MARKET_PRICE.Text)+", "+GlobalTools.ConvertFloat(TXT_LAND_MARKET.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_BUILD_APPR_1.Text)+", "+GlobalTools.ConvertFloat(TXT_BUILD_APPR_2.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_LAND_APPR_1.Text)+", "+GlobalTools.ConvertFloat(TXT_LAND_APPR_2.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_BUILD_FORCE_1.Text)+", "+GlobalTools.ConvertFloat(TXT_BUILD_FORCE_2.Text)+" "+
					","+GlobalTools.ConvertFloat(TXT_LAND_FORCE_1.Text)+", "+GlobalTools.ConvertFloat(TXT_LAND_FORCE_2.Text)+" "+
					","+GlobalTools.ConvertNull(TXT_REMARKS.Text)+", "+GlobalTools.ConvertNull(TXT_MATAUANG.Text)+" "+
					","+GlobalTools.ConvertNull(DDL_CITY.SelectedValue);  
				
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
		
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		protected void DDL_CITY_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillProject(); 
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string prid, tcode, cid, desc; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					prid = e.Item.Cells[1].Text.Trim();
					tcode = e.Item.Cells[3].Text.Trim();
					cid = cleansText(e.Item.Cells[6].Text);
					desc = cleansText(e.Item.Cells[5].Text);

					conn.QueryString = "SELECT PROYEK_ID, TYPE_CODE FROM THOUSEINFORMATION WHERE PROYEK_ID = '"+prid+"' AND TYPE_CODE = '"+tcode+"'"; 
						
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "SELECT * FROM HOUSEINFORMATION WHERE PROYEK_ID = '"+prid+"' AND TYPE_CODE = '"+tcode+"'"; 
						conn.ExecuteQuery(); 

						occ_type = conn.GetFieldValue(0,"Occupancy_Type");
						bed = conn.GetFieldValue(0,"Bedrooms");
						bath = conn.GetFieldValue(0,"Bathrooms");
						garage = conn.GetFieldValue(0,"Garage");
						land = conn.GetFieldValue(0,"Land_Area");
						build = conn.GetFieldValue(0,"Building_Area");
						phone = conn.GetFieldValue(0,"Phone");
						elec = conn.GetFieldValue(0,"Electricity");
						pam = conn.GetFieldValue(0,"PAM");
						year = conn.GetFieldValue(0,"Year_Built");
						bmarket = conn.GetFieldValue(0,"BMarket");
						lmarket = conn.GetFieldValue(0,"LMarket");
						bapp1 = conn.GetFieldValue(0,"BApp1");
						bapp2 = conn.GetFieldValue(0,"BApp2");
						lapp1 = conn.GetFieldValue(0,"LApp1");
						lapp2 = conn.GetFieldValue(0,"LApp2");
						bforce1 = conn.GetFieldValue(0,"BForced1");
						bforce2 = conn.GetFieldValue(0,"BForced2");
						lforce1 = conn.GetFieldValue(0,"LForced1");
						lforce2 = conn.GetFieldValue(0,"LForced2");
						remarks = conn.GetFieldValue(0,"Remarks");
						currency = conn.GetFieldValue(0,"Mata_uang");

						try
						{
							conn.QueryString = "EXEC PARAM_AREA_HOUSEINFO_MAKER '3', '"+prid+"','"+tcode+"',"+GlobalTools.ConvertNull(desc)+" "+
								","+GlobalTools.ConvertNull(occ_type)+", "+GlobalTools.ConvertNull(bed)+" "+
								","+GlobalTools.ConvertNull(bath)+", "+GlobalTools.ConvertNull(garage)+" "+
								","+GlobalTools.ConvertNull(land)+", "+GlobalTools.ConvertNull(build)+" "+ 
								","+GlobalTools.ConvertNull(phone)+", "+GlobalTools.ConvertNull(elec)+" "+
								","+GlobalTools.ConvertNull(pam)+", "+GlobalTools.ConvertNull(year)+" "+
								","+GlobalTools.ConvertFloat(bmarket)+", "+GlobalTools.ConvertFloat(lmarket)+" "+
								","+GlobalTools.ConvertFloat(bapp1)+", "+GlobalTools.ConvertFloat(bapp2)+" "+
								","+GlobalTools.ConvertFloat(lapp1)+", "+GlobalTools.ConvertFloat(lapp2)+" "+
								","+GlobalTools.ConvertFloat(bforce1)+", "+GlobalTools.ConvertFloat(bforce2)+" "+
								","+GlobalTools.ConvertFloat(lforce1)+", "+GlobalTools.ConvertFloat(lforce2)+" "+
								","+GlobalTools.ConvertNull(remarks)+", "+GlobalTools.ConvertNull(currency)+" "+
								","+GlobalTools.ConvertNull(cid);  

							conn.ExecuteNonQuery();
						}
						catch{ }

						BindData2();
					}
					break;

				case "edit":
					prid = e.Item.Cells[1].Text.Trim();
					tcode = e.Item.Cells[3].Text.Trim();
					desc = cleansText(e.Item.Cells[5].Text);

					conn.QueryString = "SELECT * FROM HOUSEINFORMATION WHERE PROYEK_ID = '"+prid+"' AND TYPE_CODE = '"+tcode+"'"; 
					conn.ExecuteQuery(); 
						
					try
					{
						DDL_CITY.SelectedValue = e.Item.Cells[6].Text.Trim();   
					}
					catch{ }

					try
					{
						DDL_HOUSE_TYPE.SelectedValue = e.Item.Cells[3].Text.Trim();   
					}
					catch{ }

					if(conn.GetRowCount() != 0) 
					{
						TXT_BATH.Text = conn.GetFieldValue("BATHROOMS");
						TXT_BED.Text = conn.GetFieldValue("BEDROOMS");
						TXT_BUILD_APPR_1.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("BApp1"));
						TXT_BUILD_APPR_2.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("BApp2"));
			
						TXT_BUILD_FORCE_1.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("BForced1"));
						TXT_BUILD_FORCE_2.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("BForced2"));
			
						TXT_LAND_APPR_1.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("LApp1"));
						TXT_LAND_APPR_2.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("LApp2"));
			
						TXT_LAND_FORCE_1.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("LForced1"));
						TXT_LAND_FORCE_2.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("LForced2")); 
			
						TXT_BUILDING_AREA.Text = conn.GetFieldValue("Building_Area"); 
						TXT_LAND_AREA.Text = conn.GetFieldValue("Land_Area"); 

						TXT_LAND_MARKET.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("LMarket")); 
						TXT_MARKET_PRICE.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("BMarket")); 
						TXT_MATAUANG.Text = conn.GetFieldValue("Mata_uang"); 
						TXT_OCCUPANCY.Text = conn.GetFieldValue("Occupancy_Type");
						TXT_REMARKS.Text = conn.GetFieldValue("Remarks"); 
						TXT_TYPENAME.Text = desc; 
						TXT_YEAR_BUILD.Text = conn.GetFieldValue("Year_Built"); 
						TXT_ELEC.Text = conn.GetFieldValue("Electricity"); 

						if(conn.GetFieldValue("Garage") == "1") 
							CB_GARAGE.Checked = true;
						else
							CB_GARAGE.Checked = false;

						if(conn.GetFieldValue("PAM") == "Ada") 
							CB_PAM.Checked = true;
						else
							CB_PAM.Checked = false;

						if(conn.GetFieldValue("Phone") == "Ada") 
							CB_PHONE.Checked = true;
						else
							CB_PHONE.Checked = false;
					}
					
					LBL_SAVEMODE.Text = "2";
		
					DDL_CITY.Enabled = false;
					DDL_PROJECT_CODE.Enabled = false;
					DDL_HOUSE_TYPE.Enabled = false;
					
					break;
			}
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

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string prid, tcode, desc; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					prid = e.Item.Cells[1].Text.Trim();
					tcode = e.Item.Cells[3].Text.Trim();
					
					conn.QueryString = "DELETE FROM THOUSEINFORMATION WHERE PROYEK_ID = '"+prid+"' AND TYPE_CODE = '"+tcode+"'"; 
					conn.ExecuteQuery();

					BindData2();
					break;
				case "edit":
					prid = e.Item.Cells[1].Text.Trim();
					tcode = e.Item.Cells[3].Text.Trim();
					desc = cleansText(e.Item.Cells[5].Text);

					if(e.Item.Cells[7].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						conn.QueryString = "SELECT * FROM THOUSEINFORMATION WHERE PROYEK_ID = '"+prid+"' AND TYPE_CODE = '"+tcode+"'"; 
						conn.ExecuteQuery(); 
						
						try
						{
							DDL_CITY.SelectedValue = e.Item.Cells[8].Text.Trim();   
						}
						catch{ }

						try
						{
							DDL_HOUSE_TYPE.SelectedValue = e.Item.Cells[3].Text.Trim();   
						}
						catch{ }
					
						if(conn.GetRowCount() != 0) 
						{
							TXT_BATH.Text = conn.GetFieldValue("BATHROOMS");
							TXT_BED.Text = conn.GetFieldValue("BEDROOMS");
							TXT_BUILD_APPR_1.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("BApp1"));
							TXT_BUILD_APPR_2.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("BApp2"));
			
							TXT_BUILD_FORCE_1.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("BForced1"));
							TXT_BUILD_FORCE_2.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("BForced2"));
			
							TXT_LAND_APPR_1.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("LApp1"));
							TXT_LAND_APPR_2.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("LApp2"));
			
							TXT_LAND_FORCE_1.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("LForced1"));
							TXT_LAND_FORCE_2.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("LForced2")); 
			
							TXT_BUILDING_AREA.Text = conn.GetFieldValue("Building_Area"); 
							TXT_LAND_AREA.Text = conn.GetFieldValue("Land_Area"); 

							TXT_LAND_MARKET.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("LMarket")); 
							TXT_MARKET_PRICE.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("BMarket")); 
							TXT_MATAUANG.Text = conn.GetFieldValue("Mata_uang"); 
							TXT_OCCUPANCY.Text = conn.GetFieldValue("Occupancy_Type");
							TXT_REMARKS.Text = conn.GetFieldValue("Remarks"); 
							TXT_TYPENAME.Text = desc; 
							TXT_YEAR_BUILD.Text = conn.GetFieldValue("Year_Built"); 
							TXT_ELEC.Text = conn.GetFieldValue("Electricity"); 

							if(conn.GetFieldValue("Garage") == "1") 
								CB_GARAGE.Checked = true;
							else
								CB_GARAGE.Checked = false;

							if(conn.GetFieldValue("PAM") == "Ada") 
								CB_PAM.Checked = true;
							else
								CB_PAM.Checked = false;

							if(conn.GetFieldValue("Phone") == "Ada") 
								CB_PHONE.Checked = true;
							else
								CB_PHONE.Checked = false;
							
						}

						LBL_SAVEMODE.Text = "2";
	
						DDL_CITY.Enabled = false;
						DDL_PROJECT_CODE.Enabled = false;
						DDL_HOUSE_TYPE.Enabled = false;
					}

					break;
			}
		}

		protected void DDL_PROJECT_CODE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1();
		}
	}
}
