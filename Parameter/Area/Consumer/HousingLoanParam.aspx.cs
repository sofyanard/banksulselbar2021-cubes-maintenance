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
	/// Summary description for HousingLoanParam.
	/// </summary>
	public partial class HousingLoanParam : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid, desc, nb_param, hospital, school;
		protected string shop, lake, park, sport, remark;
		protected string srccode, grline, plafond, cdsibs;

		/* 20070717 add by sofyan for kpr developer enhancement
		 * Type bangunan (TYPE_BANG) :	"1" = Rumah / Ruko
		 *								"2" = Apartemen / Kios / Mall
		 * Utk Rumah / Ruko :	Bangunan indent - Sertifikat induk		BANG_BLM_SERT_BLM = "1"
		 *						Bangunan jadi - Sertifikat induk		BANG_JADI_SERT_BLM = "1"
		 *						Bangunan indent - Sertifikat pecah		BANG_BLM_SERT_JADI = "1"
		 *						Bangunan jadi - Sertifikat pecah		BANG_JADI_SERT_JADI = "1"
		 * Utk Apartemen	:	Bangunan belum jadi - Strata Title (SHMSRS) belum terbit	BANG_BLM_SERT_BLM = "1"
		 *						Bangunan jadi - Strata Title (SHMSRS) belum terbit			BANG_JADI_SERT_BLM = "1"
		 *						Bangunan jadi - Strata Title (SHMSRS) sudah terbit			BANG_JADI_SERT_JADI = "1"
		 * Jika dipilih Strata Title (SHMSRS) belum terbit, muncul pilihan :
		 *		SHGB tanah bersama dalam proses panggabungan	SHGB_GABUNG = "0"
		 *		SHGB tanah bersama telah digabung				SHGB_GABUNG = "1"
		 */
	
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

			fillDev(); 
			fillLoc();

			BindData1();
			BindData2();

			//20070717 add by sofyan for kpr developer enhancement
			TR_SHGB.Visible = false;
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData1()
		{	
			/* 20070717 change by sofyan for kpr developer enhancement
			conn.QueryString = "select PROYEK_ID, DEVELOPER_ID, ID_KOTA, ID_LOKASI, PROYEK_DESCRIPTION, (select lokasi from lokasi_perumahan "+
					"where lokasi_perumahan.ID_LOKASI=PROYEK_HOUSINGLOAN.ID_LOKASI) LOKASI, "+ 
					"(select DV_NAME  from developer where developer.DV_CODE=PROYEK_HOUSINGLOAN.DEVELOPER_ID) DEVELOPER, "+
					"PH_GRLINE, PH_PLAFOND, PH_SRCCODE, CD_SIBS from PROYEK_HOUSINGLOAN "+
					"where active='1' and ID_KOTA = '"+DDL_CITY.SelectedValue+"'";
			*/
			conn.QueryString = "exec PARAM_HOUSINGLOAN_VIEWEXISTING '"+DDL_CITY.SelectedValue+"'";
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
			/* 20070717 change by sofyan for kpr developer enhancement
			conn.QueryString = "select PROYEK_ID, DEVELOPER_ID, ID_KOTA, ID_LOKASI, PROYEK_DESCRIPTION, "+
				"(select lokasi from lokasi_perumahan "+ 
				"where lokasi_perumahan.ID_LOKASI=TPROYEK_HOUSINGLOAN.ID_LOKASI) LOKASI, "+ 
				"(select DV_NAME  from developer where developer.DV_CODE=TPROYEK_HOUSINGLOAN.Developer_ID) DEVELOPER, "+
				"PH_GRLINE, PH_PLAFOND, PH_SRCCODE, CD_SIBS, CH_STA, STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' when '3' "+
				"then 'DELETE' end from TPROYEK_HOUSINGLOAN";
			*/
			conn.QueryString = "exec PARAM_HOUSINGLOAN_VIEWREQUEST '"+DDL_CITY.SelectedValue+"'";
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

		private void fillDev()
		{
			GlobalTools.fillRefList(DDL_DEVELOPER,"select DV_CODE, DV_NAME from DEVELOPER where CITY_ID = '"+DDL_CITY.SelectedValue+"'",false,conn);		
		}

		private void fillLoc()
		{
			GlobalTools.fillRefList(DDL_LOCATION,"select ID_LOKASI, LOKASI from LOKASI_PERUMAHAN where ID_KOTA = '"+DDL_CITY.SelectedValue+"'",false,conn);				
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ClearEditBoxes()
		{
			DDL_CITY.Enabled = true;
			TXT_PROJECT_CODE.Enabled = true; 

			TXT_MKT_SOURCE_CODE.Text = "";
			TXT_GUARANTOR_LINE.Text = "0";
			TXT_PARAM_NUMBER.Text = "0";
			TXT_PROJECT_CODE.Text = ""; 
			TXT_PROJECT_NAME.Text = "";
			TXT_REALISASI.Text = "0";
			TXT_REMARK.Text = "";
			
			CB_HOSPITAL.Checked = false;
			CB_LAKE.Checked = false;
			CB_MARKETPLACE.Checked = false;
			CB_PARK.Checked = false;
			CB_SCHOOL.Checked = false;
			CB_SPORTCENTER.Checked = false; 
				 
			LBL_SAVEMODE.Text = "1";

			//20070717 add by sofyan for kpr developer enhancement
			RBL_TYPE_BANG.SelectedValue = "1";

			CB_BANG_BLM_SERT_BLM.Checked = false;
			CB_BANG_BLM_SERT_BLM.Text = "Bangunan indent - Sertifikat induk";
			CB_BANG_JADI_SERT_BLM.Checked = false;
			CB_BANG_JADI_SERT_BLM.Text = "Bangunan jadi - Sertifikat induk";
			CB_BANG_BLM_SERT_JADI.Visible = true;
			CB_BANG_BLM_SERT_JADI.Checked = false;
			CB_BANG_BLM_SERT_JADI.Text = "Bangunan indent - Sertifikat pecah";
			CB_BANG_JADI_SERT_JADI.Checked = false;
			CB_BANG_JADI_SERT_JADI.Text = "Bangunan jadi - Sertifikat pecah";

			TR_SHGB.Visible = false;
			RBL_SHGB_GABUNG.SelectedValue = "0";
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

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string h1 = "", h2 = "", h3 = "", h4 = "", h5 = "", h6 = "", gl = "", pl = "";
			int hit = 0;

			if(CB_HOSPITAL.Checked)
				h1 = "Ada";
			else
				h1 = "Tidak Ada";

			if(CB_LAKE.Checked)
				h2 = "Ada";
			else
				h2 = "Tidak Ada";

			if(CB_MARKETPLACE.Checked)
				h3 = "Ada";
			else
				h3 = "Tidak Ada";

			if(CB_PARK.Checked)
				h4 = "Ada";
			else
				h4 = "Tidak Ada";

			if(CB_SCHOOL.Checked)
				h5 = "Ada";
			else
				h5 = "Tidak Ada";

			if(CB_SPORTCENTER.Checked)
				h6 = "Ada";
			else
				h6 = "Tidak Ada";

			if(TXT_REALISASI.Text == "")
				pl = "0";
			else
				pl = TXT_REALISASI.Text; 

			if(TXT_GUARANTOR_LINE.Text == "")
				gl = "0";
			else
				gl = TXT_GUARANTOR_LINE.Text; 

			//20070717 add by sofyan for kpr developer enhancement
			string type_bang, bank_blm_sert_blm, bang_jadi_sert_blm, bang_blm_sert_jadi, bang_jadi_sert_jadi, shgb_gabung;

			if (RBL_TYPE_BANG.SelectedValue == "1") //Rumah Ruko
				type_bang = "1";
			else if (RBL_TYPE_BANG.SelectedValue == "2") //Apartemen Kios Mall
				type_bang = "2";
			else
				type_bang = "";

			if (CB_BANG_BLM_SERT_BLM.Checked == true)
				bank_blm_sert_blm = "1";
			else
				bank_blm_sert_blm = "0";

			if (CB_BANG_JADI_SERT_BLM.Checked == true)
				bang_jadi_sert_blm = "1";
			else
				bang_jadi_sert_blm = "0";

			if (CB_BANG_BLM_SERT_JADI.Checked == true)
				bang_blm_sert_jadi = "1";
			else
				bang_blm_sert_jadi = "0";

			if (CB_BANG_JADI_SERT_JADI.Checked == true)
				bang_jadi_sert_jadi = "1";
			else
				bang_jadi_sert_jadi = "0";

			if (RBL_SHGB_GABUNG.SelectedValue == "1")
				shgb_gabung = "1";
			else
				shgb_gabung = "0";


			conn.QueryString = "SELECT * FROM TPROYEK_HOUSINGLOAN WHERE PROYEK_ID = '"+TXT_PROJECT_CODE.Text+
						"' AND DEVELOPER_ID = '"+DDL_DEVELOPER.SelectedValue+"'"; 
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				/* 20070717 change by sofyan for kpr developer enhancement
				conn.QueryString = "UPDATE TPROYEK_HOUSINGLOAN SET ID_KOTA = "+GlobalTools.ConvertNull(DDL_CITY.SelectedValue)+", "+ 
					"ID_LOKASI = "+GlobalTools.ConvertNull(DDL_LOCATION.SelectedValue)+", "+ 
					"PROYEK_DESCRIPTION = "+GlobalTools.ConvertNull(TXT_PROJECT_NAME.Text)+", NUMBER_PARAM = "+GlobalTools.ConvertNull(TXT_PARAM_NUMBER.Text)+", "+
					"RUMAH_SAKIT = "+GlobalTools.ConvertNull(h1)+", SEKOLAH = "+GlobalTools.ConvertNull(h5)+", PUSAT_BELANJA = "+GlobalTools.ConvertNull(h3)+", "+
					"DANAU = "+GlobalTools.ConvertNull(h2)+", TAMAN = "+GlobalTools.ConvertNull(h4)+", OLAH_RAGA = "+GlobalTools.ConvertNull(h6)+", REMARK = "+GlobalTools.ConvertNull(TXT_REMARK.Text)+", "+ 
					"PH_SRCCODE = "+GlobalTools.ConvertNull(TXT_MKT_SOURCE_CODE.Text)+", PH_GRLINE = "+GlobalTools.ConvertFloat(gl)+", "+
					"PH_PLAFOND = "+GlobalTools.ConvertFloat(pl)+" "+ "CD_SIBS = "+GlobalTools.ConvertNull(TXT_EMAS_CODE.Text)+
					"WHERE PROYEK_ID = '"+TXT_PROJECT_CODE.Text+"' "+
					"AND DEVELOPER_ID = '"+DDL_DEVELOPER.SelectedValue+"'";
				*/
				conn.QueryString = "exec PARAM_HOUSINGLOAN_MAKERUPDATE 2, '" +
					TXT_PROJECT_CODE.Text + "', '" +
					DDL_DEVELOPER.SelectedValue + "', " +
					GlobalTools.ConvertNull(DDL_CITY.SelectedValue) + ", " +
					GlobalTools.ConvertNull(DDL_LOCATION.SelectedValue) + ", " +
					GlobalTools.ConvertNull(TXT_PROJECT_NAME.Text) + ", " +
					GlobalTools.ConvertNull(TXT_PARAM_NUMBER.Text) + ", " +
					GlobalTools.ConvertNull(h1) + ", " +
					GlobalTools.ConvertNull(h5) + ", " +
					GlobalTools.ConvertNull(h3) + ", " +
					GlobalTools.ConvertNull(h2) + ", " +
					GlobalTools.ConvertNull(h4) + ", " +
					GlobalTools.ConvertNull(h6) + ", " +
					GlobalTools.ConvertNull(TXT_REMARK.Text) + ", " +
					GlobalTools.ConvertNull(TXT_MKT_SOURCE_CODE.Text) + ", " +
					GlobalTools.ConvertFloat(gl) + ", " +
					GlobalTools.ConvertFloat(pl) + ", " +
					"'1', " +
					GlobalTools.ConvertNull(TXT_EMAS_CODE.Text) + ", " +
					GlobalTools.ConvertFloat(type_bang) + ", " +
					GlobalTools.ConvertFloat(bang_jadi_sert_jadi) + ", " +
					GlobalTools.ConvertFloat(bang_blm_sert_jadi) + ", " +
					GlobalTools.ConvertFloat(bang_jadi_sert_blm) + ", " +
					GlobalTools.ConvertFloat(bank_blm_sert_blm) + ", " +
					GlobalTools.ConvertFloat(shgb_gabung);
				conn.ExecuteQuery();

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				/* 20070717 change by sofyan for kpr developer enhancement
				conn.QueryString = "INSERT INTO TPROYEK_HOUSINGLOAN (PROYEK_ID,DEVELOPER_ID,ID_KOTA,ID_LOKASI,PROYEK_DESCRIPTION,NUMBER_PARAM,RUMAH_SAKIT,SEKOLAH,PUSAT_BELANJA,DANAU,TAMAN,OLAH_RAGA,REMARK,PH_SRCCODE,PH_GRLINE,PH_PLAFOND,CH_STA,CD_SIBS ) VALUES('"+TXT_PROJECT_CODE.Text+"','"+DDL_DEVELOPER.SelectedValue+"',"+GlobalTools.ConvertNull(DDL_CITY.SelectedValue)+
					", "+GlobalTools.ConvertNull(DDL_LOCATION.SelectedValue)+", "+GlobalTools.ConvertNull(TXT_PROJECT_NAME.Text)+","+GlobalTools.ConvertNull(TXT_PARAM_NUMBER.Text)+
					", "+GlobalTools.ConvertNull(h1)+", "+GlobalTools.ConvertNull(h5)+", "+GlobalTools.ConvertNull(h3)+
					", "+GlobalTools.ConvertNull(h2)+", "+GlobalTools.ConvertNull(h4)+", "+GlobalTools.ConvertNull(h6)+
					", "+GlobalTools.ConvertNull(TXT_REMARK.Text)+", "+GlobalTools.ConvertNull(TXT_MKT_SOURCE_CODE.Text)+", "+GlobalTools.ConvertFloat(gl)+
					", "+GlobalTools.ConvertFloat(pl)+", '2', " + GlobalTools.ConvertNull(TXT_EMAS_CODE.Text)+")";
				*/
				conn.QueryString = "exec PARAM_HOUSINGLOAN_MAKERUPDATE 1, '" +
					TXT_PROJECT_CODE.Text + "', '" +
					DDL_DEVELOPER.SelectedValue + "', " +
					GlobalTools.ConvertNull(DDL_CITY.SelectedValue) + ", " +
					GlobalTools.ConvertNull(DDL_LOCATION.SelectedValue) + ", " +
					GlobalTools.ConvertNull(TXT_PROJECT_NAME.Text) + ", " +
					GlobalTools.ConvertNull(TXT_PARAM_NUMBER.Text) + ", " +
					GlobalTools.ConvertNull(h1) + ", " +
					GlobalTools.ConvertNull(h5) + ", " +
					GlobalTools.ConvertNull(h3) + ", " +
					GlobalTools.ConvertNull(h2) + ", " +
					GlobalTools.ConvertNull(h4) + ", " +
					GlobalTools.ConvertNull(h6) + ", " +
					GlobalTools.ConvertNull(TXT_REMARK.Text) + ", " +
					GlobalTools.ConvertNull(TXT_MKT_SOURCE_CODE.Text) + ", " +
					GlobalTools.ConvertFloat(gl) + ", " +
					GlobalTools.ConvertFloat(pl) + ", " +
					"'2', " +
					GlobalTools.ConvertNull(TXT_EMAS_CODE.Text) + ", " +
					GlobalTools.ConvertFloat(type_bang) + ", " +
					GlobalTools.ConvertFloat(bang_jadi_sert_jadi) + ", " +
					GlobalTools.ConvertFloat(bang_blm_sert_jadi) + ", " +
					GlobalTools.ConvertFloat(bang_jadi_sert_blm) + ", " +
					GlobalTools.ConvertFloat(bank_blm_sert_blm) + ", " +
					GlobalTools.ConvertFloat(shgb_gabung);
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				/* 20070717 change by sofyan for kpr developer enhancement
				conn.QueryString = "INSERT INTO TPROYEK_HOUSINGLOAN (PROYEK_ID,DEVELOPER_ID,ID_KOTA,ID_LOKASI,PROYEK_DESCRIPTION,NUMBER_PARAM,RUMAH_SAKIT,SEKOLAH,PUSAT_BELANJA,DANAU,TAMAN,OLAH_RAGA,REMARK,PH_SRCCODE,PH_GRLINE,PH_PLAFOND,CH_STA,CD_SIBS ) VALUES('"+TXT_PROJECT_CODE.Text+"','"+DDL_DEVELOPER.SelectedValue+"',"+GlobalTools.ConvertNull(DDL_CITY.SelectedValue)+
					", "+GlobalTools.ConvertNull(DDL_LOCATION.SelectedValue)+", "+GlobalTools.ConvertNull(TXT_PROJECT_NAME.Text)+","+GlobalTools.ConvertNull(TXT_PARAM_NUMBER.Text)+
					", "+GlobalTools.ConvertNull(h1)+", "+GlobalTools.ConvertNull(h5)+", "+GlobalTools.ConvertNull(h3)+
					", "+GlobalTools.ConvertNull(h2)+", "+GlobalTools.ConvertNull(h4)+", "+GlobalTools.ConvertNull(h6)+
					", "+GlobalTools.ConvertNull(TXT_REMARK.Text)+", "+GlobalTools.ConvertNull(TXT_MKT_SOURCE_CODE.Text)+", "+GlobalTools.ConvertFloat(gl)+
					", "+GlobalTools.ConvertFloat(pl)+", '1', " + GlobalTools.ConvertNull(TXT_EMAS_CODE.Text)+")";
				*/
				conn.QueryString = "exec PARAM_HOUSINGLOAN_MAKERUPDATE 1, '" +
					TXT_PROJECT_CODE.Text + "', '" +
					DDL_DEVELOPER.SelectedValue + "', " +
					GlobalTools.ConvertNull(DDL_CITY.SelectedValue) + ", " +
					GlobalTools.ConvertNull(DDL_LOCATION.SelectedValue) + ", " +
					GlobalTools.ConvertNull(TXT_PROJECT_NAME.Text) + ", " +
					GlobalTools.ConvertNull(TXT_PARAM_NUMBER.Text) + ", " +
					GlobalTools.ConvertNull(h1) + ", " +
					GlobalTools.ConvertNull(h5) + ", " +
					GlobalTools.ConvertNull(h3) + ", " +
					GlobalTools.ConvertNull(h2) + ", " +
					GlobalTools.ConvertNull(h4) + ", " +
					GlobalTools.ConvertNull(h6) + ", " +
					GlobalTools.ConvertNull(TXT_REMARK.Text) + ", " +
					GlobalTools.ConvertNull(TXT_MKT_SOURCE_CODE.Text) + ", " +
					GlobalTools.ConvertFloat(gl) + ", " +
					GlobalTools.ConvertFloat(pl) + ", " +
					"'1', " +
					GlobalTools.ConvertNull(TXT_EMAS_CODE.Text) + ", " +
					GlobalTools.ConvertFloat(type_bang) + ", " +
					GlobalTools.ConvertFloat(bang_jadi_sert_jadi) + ", " +
					GlobalTools.ConvertFloat(bang_blm_sert_jadi) + ", " +
					GlobalTools.ConvertFloat(bang_jadi_sert_blm) + ", " +
					GlobalTools.ConvertFloat(bank_blm_sert_blm) + ", " +
					GlobalTools.ConvertFloat(shgb_gabung);
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

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 			
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string proid, devid, cid, locid; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					proid = e.Item.Cells[0].Text.Trim();
					devid = e.Item.Cells[1].Text.Trim();
					cid = cleansText(e.Item.Cells[3].Text);
					locid = cleansText(e.Item.Cells[4].Text);

					conn.QueryString = "SELECT * FROM TPROYEK_HOUSINGLOAN WHERE PROYEK_ID = '"+proid+"' AND DEVELOPER_ID = '"+devid+"'"; 
						
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "SELECT * FROM PROYEK_HOUSINGLOAN WHERE PROYEK_ID = '"+proid+"' AND DEVELOPER_ID = '"+devid+"'"; 
						conn.ExecuteQuery(); 

						desc = conn.GetFieldValue(0,"PROYEK_DESCRIPTION");
						nb_param = conn.GetFieldValue(0,"NUMBER_PARAM");
						hospital = conn.GetFieldValue(0,"RUMAH_SAKIT");
						school = conn.GetFieldValue(0,"SEKOLAH");
						shop = conn.GetFieldValue(0,"PUSAT_BELANJA");
						lake = conn.GetFieldValue(0,"DANAU");
						park = conn.GetFieldValue(0,"TAMAN");
						sport = conn.GetFieldValue(0,"OLAH_RAGA");
						remark = conn.GetFieldValue(0,"REMARK");
						srccode = conn.GetFieldValue(0,"PH_SRCCODE");
						grline = conn.GetFieldValue(0,"PH_GRLINE");
						plafond = conn.GetFieldValue(0,"PH_PLAFOND");
						cdsibs = conn.GetFieldValue(0,"CD_SIBS");

						//20070717 add by sofyan for kpr developer enhancement
						string type_bang, bank_blm_sert_blm, bang_jadi_sert_blm, bang_blm_sert_jadi, bang_jadi_sert_jadi, shgb_gabung;
						type_bang = conn.GetFieldValue(0,"TYPE_BANG");
						bank_blm_sert_blm = conn.GetFieldValue(0,"BANG_BLM_SERT_BLM");
						bang_jadi_sert_blm = conn.GetFieldValue(0,"BANG_JADI_SERT_BLM");
						bang_blm_sert_jadi = conn.GetFieldValue(0,"BANG_BLM_SERT_JADI");
						bang_jadi_sert_jadi = conn.GetFieldValue(0,"BANG_JADI_SERT_JADI");
						shgb_gabung = conn.GetFieldValue(0,"SHGB_GABUNG");

						try
						{
							/* 20070717 change by sofyan for kpr developer enhancement
							conn.QueryString = "INSERT INTO TPROYEK_HOUSINGLOAN (PROYEK_ID,DEVELOPER_ID,ID_KOTA,ID_LOKASI,PROYEK_DESCRIPTION,NUMBER_PARAM,RUMAH_SAKIT,SEKOLAH,PUSAT_BELANJA,DANAU,TAMAN,OLAH_RAGA,REMARK,PH_SRCCODE,PH_GRLINE,PH_PLAFOND,CH_STA,CD_SIBS ) VALUES('"+proid+"','"+devid+"',"+GlobalTools.ConvertNull(cid)+
								", "+GlobalTools.ConvertNull(locid)+", "+GlobalTools.ConvertNull(desc)+","+GlobalTools.ConvertNull(nb_param)+
								", "+GlobalTools.ConvertNull(hospital)+", "+GlobalTools.ConvertNull(school)+", "+GlobalTools.ConvertNull(shop)+
								", "+GlobalTools.ConvertNull(lake)+", "+GlobalTools.ConvertNull(park)+", "+GlobalTools.ConvertNull(sport)+
								", "+GlobalTools.ConvertNull(remark)+", "+GlobalTools.ConvertNull(srccode)+", "+GlobalTools.ConvertFloat(grline)+
								", "+GlobalTools.ConvertFloat(plafond)+", '3', " + GlobalTools.ConvertNull(cdsibs)+")";
							*/
							conn.QueryString = "exec PARAM_HOUSINGLOAN_MAKERUPDATE 1, '" +
								proid + "', '" +
								devid + "', " +
								GlobalTools.ConvertNull(cid) + ", " +
								GlobalTools.ConvertNull(locid) + ", " +
								GlobalTools.ConvertNull(desc) + ", " +
								GlobalTools.ConvertNull(nb_param) + ", " +
								GlobalTools.ConvertNull(hospital) + ", " +
								GlobalTools.ConvertNull(school) + ", " +
								GlobalTools.ConvertNull(shop) + ", " +
								GlobalTools.ConvertNull(lake) + ", " +
								GlobalTools.ConvertNull(park) + ", " +
								GlobalTools.ConvertNull(sport) + ", " +
								GlobalTools.ConvertNull(remark) + ", " +
								GlobalTools.ConvertNull(srccode) + ", " +
								GlobalTools.ConvertFloat(grline) + ", " +
								GlobalTools.ConvertFloat(plafond) + ", " +
								"'3', " +
								GlobalTools.ConvertNull(cdsibs) + ", " +
								GlobalTools.ConvertFloat(type_bang) + ", " +
								GlobalTools.ConvertFloat(bang_jadi_sert_jadi) + ", " +
								GlobalTools.ConvertFloat(bang_blm_sert_jadi) + ", " +
								GlobalTools.ConvertFloat(bang_jadi_sert_blm) + ", " +
								GlobalTools.ConvertFloat(bank_blm_sert_blm) + ", " +
								GlobalTools.ConvertFloat(shgb_gabung);
							conn.ExecuteQuery();
						}
						catch{ }

						BindData2();
					}
					break;

				case "edit":
					ClearEditBoxes(); 		
					proid = e.Item.Cells[0].Text.Trim();
					devid = e.Item.Cells[1].Text.Trim();
					
					conn.QueryString = "SELECT * FROM PROYEK_HOUSINGLOAN WHERE PROYEK_ID = '"+proid+"' AND DEVELOPER_ID = '"+devid+"'"; 
					conn.ExecuteQuery(); 
						
					try
					{
						DDL_CITY.SelectedValue = e.Item.Cells[3].Text.Trim();   
					}
					catch{ }

					try
					{
						DDL_DEVELOPER.SelectedValue = e.Item.Cells[1].Text.Trim();   
					}
					catch{ }

					try
					{
						DDL_LOCATION.SelectedValue = e.Item.Cells[4].Text.Trim();   
					}
					catch{ }

					TXT_PROJECT_CODE.Text = proid;

					if(conn.GetRowCount() != 0) 
					{
						TXT_PROJECT_NAME.Text = conn.GetFieldValue(0,"PROYEK_DESCRIPTION");
						TXT_PARAM_NUMBER.Text  = conn.GetFieldValue(0,"NUMBER_PARAM");
						
						TXT_REMARK.Text = conn.GetFieldValue(0,"REMARK");
						TXT_MKT_SOURCE_CODE.Text = conn.GetFieldValue(0,"PH_SRCCODE");
						TXT_GUARANTOR_LINE.Text = GlobalTools.MoneyFormat(conn.GetFieldValue(0,"PH_GRLINE"));
						TXT_REALISASI.Text = GlobalTools.MoneyFormat(conn.GetFieldValue(0,"PH_PLAFOND"));
						TXT_EMAS_CODE.Text = conn.GetFieldValue(0,"CD_SIBS");
		
						if(conn.GetFieldValue("RUMAH_SAKIT") == "Ada") 
							CB_HOSPITAL.Checked = true;
						else
							CB_HOSPITAL.Checked = false;

						if(conn.GetFieldValue("SEKOLAH") == "Ada") 
							CB_SCHOOL.Checked = true;
						else
							CB_SCHOOL.Checked = false;

						if(conn.GetFieldValue("PUSAT_BELANJA") == "Ada") 
							CB_MARKETPLACE.Checked = true;
						else
							CB_MARKETPLACE.Checked = false;

						if(conn.GetFieldValue("DANAU") == "Ada") 
							CB_LAKE.Checked = true;
						else
							CB_LAKE.Checked = false;

						if(conn.GetFieldValue("TAMAN") == "Ada") 
							CB_PARK.Checked = true;
						else
							CB_PARK.Checked = false;

						if(conn.GetFieldValue("OLAH_RAGA") == "Ada") 
							CB_SPORTCENTER.Checked = true;
						else
							CB_SPORTCENTER.Checked = false;

						//20070717 add by sofyan for kpr developer enhancement
						try
						{
							RBL_TYPE_BANG.SelectedValue = conn.GetFieldValue("TYPE_BANG");
						}
						catch{ }

						if(conn.GetFieldValue("BANG_JADI_SERT_JADI") == "1") 
							CB_BANG_JADI_SERT_JADI.Checked = true;
						else
							CB_BANG_JADI_SERT_JADI.Checked = false;

						if(conn.GetFieldValue("BANG_BLM_SERT_JADI") == "1") 
							CB_BANG_BLM_SERT_JADI.Checked = true;
						else
							CB_BANG_BLM_SERT_JADI.Checked = false;

						if(conn.GetFieldValue("BANG_JADI_SERT_BLM") == "1") 
							CB_BANG_JADI_SERT_BLM.Checked = true;
						else
							CB_BANG_JADI_SERT_BLM.Checked = false;

						if(conn.GetFieldValue("BANG_BLM_SERT_BLM") == "1") 
							CB_BANG_BLM_SERT_BLM.Checked = true;
						else
							CB_BANG_BLM_SERT_BLM.Checked = false;

						try
						{
							RBL_SHGB_GABUNG.SelectedValue = conn.GetFieldValue("SHGB_GABUNG");
						}
						catch{ }

						if (RBL_TYPE_BANG.SelectedValue == "1") //Rumah Ruko
						{
							CB_BANG_BLM_SERT_BLM.Text = "Bangunan indent - Sertifikat induk";
							CB_BANG_JADI_SERT_BLM.Text = "Bangunan jadi - Sertifikat induk";
							CB_BANG_BLM_SERT_JADI.Visible = true;
							CB_BANG_BLM_SERT_JADI.Text = "Bangunan indent - Sertifikat pecah";
							CB_BANG_JADI_SERT_JADI.Text = "Bangunan jadi - Sertifikat pecah";
						}
						else if (RBL_TYPE_BANG.SelectedValue == "2") //Apartemen Kios Mall
						{
							CB_BANG_BLM_SERT_BLM.Text = "Bangunan belum jadi - Strata Title (SHMSRS) belum terbit";
							CB_BANG_JADI_SERT_BLM.Text = "Bangunan jadi - Strata Title (SHMSRS) belum terbit";
							CB_BANG_BLM_SERT_JADI.Visible = false;
							CB_BANG_BLM_SERT_JADI.Checked = false;
							CB_BANG_BLM_SERT_JADI.Text = "";
							CB_BANG_JADI_SERT_JADI.Text = "Bangunan jadi - Strata Title (SHMSRS) sudah terbit";
						}

						if (((CB_BANG_BLM_SERT_BLM.Checked == true) || (CB_BANG_JADI_SERT_BLM.Checked == true)) && (RBL_TYPE_BANG.SelectedValue == "2"))
						{
							TR_SHGB.Visible = true;
						}
						else
						{
							TR_SHGB.Visible = false;
							RBL_SHGB_GABUNG.SelectedValue = "0";
						}
					}
					
					LBL_SAVEMODE.Text = "2";
		
					DDL_CITY.Enabled = false;
					TXT_PROJECT_CODE.Enabled = false;
					
					break;
			}
		}

		protected void DDL_CITY_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillDev();
			fillLoc(); 
			BindData1(); 
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 		
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 		
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string proid, devid, cid, locid; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					proid = e.Item.Cells[0].Text.Trim();
					devid = e.Item.Cells[1].Text.Trim();
					cid = cleansText(e.Item.Cells[3].Text);
					locid = cleansText(e.Item.Cells[4].Text);
					
					conn.QueryString = "DELETE FROM TPROYEK_HOUSINGLOAN WHERE PROYEK_ID = '"+proid+"' AND DEVELOPER_ID = '"+devid+"'"; 
					conn.ExecuteQuery();

					BindData2();
					break;
				case "edit":
					ClearEditBoxes(); 		
					proid = e.Item.Cells[0].Text.Trim();
					devid = e.Item.Cells[1].Text.Trim();

					if(e.Item.Cells[11].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						conn.QueryString = "SELECT * FROM TPROYEK_HOUSINGLOAN WHERE PROYEK_ID = '"+proid+"' AND DEVELOPER_ID = '"+devid+"'"; 
						conn.ExecuteQuery(); 

						try
						{
							DDL_CITY.SelectedValue = e.Item.Cells[3].Text.Trim();   
						}
						catch{ }

						try
						{
							DDL_DEVELOPER.SelectedValue = e.Item.Cells[1].Text.Trim();   
						}
						catch{ }

						try
						{
							DDL_LOCATION.SelectedValue = e.Item.Cells[4].Text.Trim();   
						}
						catch{ }

						TXT_PROJECT_CODE.Text = proid; 

						if(conn.GetRowCount() != 0) 
						{
							TXT_PROJECT_NAME.Text = conn.GetFieldValue(0,"PROYEK_DESCRIPTION");
							TXT_PARAM_NUMBER.Text  = conn.GetFieldValue(0,"NUMBER_PARAM");
						
							TXT_REMARK.Text = conn.GetFieldValue(0,"REMARK");
							TXT_MKT_SOURCE_CODE.Text = conn.GetFieldValue(0,"PH_SRCCODE");
							TXT_GUARANTOR_LINE.Text = GlobalTools.MoneyFormat(conn.GetFieldValue(0,"PH_GRLINE"));
							TXT_REALISASI.Text = GlobalTools.MoneyFormat(conn.GetFieldValue(0,"PH_PLAFOND"));
							TXT_EMAS_CODE.Text = conn.GetFieldValue(0,"CD_SIBS");
		
							if(conn.GetFieldValue("RUMAH_SAKIT") == "Ada") 
								CB_HOSPITAL.Checked = true;
							else
								CB_HOSPITAL.Checked = false;

							if(conn.GetFieldValue("SEKOLAH") == "Ada") 
								CB_SCHOOL.Checked = true;
							else
								CB_SCHOOL.Checked = false;

							if(conn.GetFieldValue("PUSAT_BELANJA") == "Ada") 
								CB_MARKETPLACE.Checked = true;
							else
								CB_MARKETPLACE.Checked = false;

							if(conn.GetFieldValue("DANAU") == "Ada") 
								CB_LAKE.Checked = true;
							else
								CB_LAKE.Checked = false;

							if(conn.GetFieldValue("TAMAN") == "Ada") 
								CB_PARK.Checked = true;
							else
								CB_PARK.Checked = false;

							if(conn.GetFieldValue("OLAH_RAGA") == "Ada") 
								CB_SPORTCENTER.Checked = true;
							else
								CB_SPORTCENTER.Checked = false;

							//20070717 add by sofyan for kpr developer enhancement
							try
							{
								RBL_TYPE_BANG.SelectedValue = conn.GetFieldValue("TYPE_BANG");
							}
							catch{ }

							if(conn.GetFieldValue("BANG_JADI_SERT_JADI") == "1") 
								CB_BANG_JADI_SERT_JADI.Checked = true;
							else
								CB_BANG_JADI_SERT_JADI.Checked = false;

							if(conn.GetFieldValue("BANG_BLM_SERT_JADI") == "1") 
								CB_BANG_BLM_SERT_JADI.Checked = true;
							else
								CB_BANG_BLM_SERT_JADI.Checked = false;

							if(conn.GetFieldValue("BANG_JADI_SERT_BLM") == "1") 
								CB_BANG_JADI_SERT_BLM.Checked = true;
							else
								CB_BANG_JADI_SERT_BLM.Checked = false;

							if(conn.GetFieldValue("BANG_BLM_SERT_BLM") == "1") 
								CB_BANG_BLM_SERT_BLM.Checked = true;
							else
								CB_BANG_BLM_SERT_BLM.Checked = false;

							try
							{
								RBL_SHGB_GABUNG.SelectedValue = conn.GetFieldValue("SHGB_GABUNG");
							}
							catch{ }

							if (RBL_TYPE_BANG.SelectedValue == "1") //Rumah Ruko
							{
								CB_BANG_BLM_SERT_BLM.Text = "Bangunan indent - Sertifikat induk";
								CB_BANG_JADI_SERT_BLM.Text = "Bangunan jadi - Sertifikat induk";
								CB_BANG_BLM_SERT_JADI.Visible = true;
								CB_BANG_BLM_SERT_JADI.Text = "Bangunan indent - Sertifikat pecah";
								CB_BANG_JADI_SERT_JADI.Text = "Bangunan jadi - Sertifikat pecah";
							}
							else if (RBL_TYPE_BANG.SelectedValue == "2") //Apartemen Kios Mall
							{
								CB_BANG_BLM_SERT_BLM.Text = "Bangunan belum jadi - Strata Title (SHMSRS) belum terbit";
								CB_BANG_JADI_SERT_BLM.Text = "Bangunan jadi - Strata Title (SHMSRS) belum terbit";
								CB_BANG_BLM_SERT_JADI.Visible = false;
								CB_BANG_BLM_SERT_JADI.Checked = false;
								CB_BANG_BLM_SERT_JADI.Text = "";
								CB_BANG_JADI_SERT_JADI.Text = "Bangunan jadi - Strata Title (SHMSRS) sudah terbit";
							}

							if (((CB_BANG_BLM_SERT_BLM.Checked == true) || (CB_BANG_JADI_SERT_BLM.Checked == true)) && (RBL_TYPE_BANG.SelectedValue == "2"))
							{
								TR_SHGB.Visible = true;
							}
							else
							{
								TR_SHGB.Visible = false;
								RBL_SHGB_GABUNG.SelectedValue = "0";
							}
						}
											
						LBL_SAVEMODE.Text = "2";
	
						DDL_CITY.Enabled = false;
						TXT_PROJECT_CODE.Enabled = false;
					}

					break;
			}
		}

		protected void RBL_TYPE_BANG_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (RBL_TYPE_BANG.SelectedValue == "1") //Rumah Ruko
			{
				CB_BANG_BLM_SERT_BLM.Text = "Bangunan indent - Sertifikat induk";
				CB_BANG_JADI_SERT_BLM.Text = "Bangunan jadi - Sertifikat induk";
				CB_BANG_BLM_SERT_JADI.Visible = true;
				CB_BANG_BLM_SERT_JADI.Text = "Bangunan indent - Sertifikat pecah";
				CB_BANG_JADI_SERT_JADI.Text = "Bangunan jadi - Sertifikat pecah";
			}
			else if (RBL_TYPE_BANG.SelectedValue == "2") //Apartemen Kios Mall
			{
				CB_BANG_BLM_SERT_BLM.Text = "Bangunan belum jadi - Strata Title (SHMSRS) belum terbit";
				CB_BANG_JADI_SERT_BLM.Text = "Bangunan jadi - Strata Title (SHMSRS) belum terbit";
				CB_BANG_BLM_SERT_JADI.Visible = false;
				CB_BANG_BLM_SERT_JADI.Checked = false;
				CB_BANG_BLM_SERT_JADI.Text = "";
				CB_BANG_JADI_SERT_JADI.Text = "Bangunan jadi - Strata Title (SHMSRS) sudah terbit";
			}

			if (((CB_BANG_BLM_SERT_BLM.Checked == true) || (CB_BANG_JADI_SERT_BLM.Checked == true)) && (RBL_TYPE_BANG.SelectedValue == "2"))
			{
				TR_SHGB.Visible = true;
			}
			else
			{
				TR_SHGB.Visible = false;
				RBL_SHGB_GABUNG.SelectedValue = "0";
			}
		}

		protected void CB_BANG_BLM_SERT_BLM_CheckedChanged(object sender, System.EventArgs e)
		{
			if (((CB_BANG_BLM_SERT_BLM.Checked == true) || (CB_BANG_JADI_SERT_BLM.Checked == true)) && (RBL_TYPE_BANG.SelectedValue == "2"))
			{
				TR_SHGB.Visible = true;
			}
			else
			{
				TR_SHGB.Visible = false;
				RBL_SHGB_GABUNG.SelectedValue = "0";
			}
		}

		protected void CB_BANG_JADI_SERT_BLM_CheckedChanged(object sender, System.EventArgs e)
		{
			if (((CB_BANG_BLM_SERT_BLM.Checked == true) || (CB_BANG_JADI_SERT_BLM.Checked == true)) && (RBL_TYPE_BANG.SelectedValue == "2"))
			{
				TR_SHGB.Visible = true;
			}
			else
			{
				TR_SHGB.Visible = false;
				RBL_SHGB_GABUNG.SelectedValue = "0";
			}
		}
	}
}
