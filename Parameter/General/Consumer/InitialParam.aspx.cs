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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for InitialParam.
	/// </summary>
	public partial class InitialParam : System.Web.UI.Page
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
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");
			
			LBL_SAVE.Text = "1";
 
			ClearEditBoxes(); 

			InitialCon();
 
			BindData1();
 			BindData2(); 
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void ClearEditBoxes()
		{
			TXT_SEQ_NO.Enabled = true;
 
			TXT_ADDR1.Text = "";
			TXT_ADDR2.Text = "";
			TXT_ADDR3.Text = "";
			TXT_BANK_NAME.Text = "";
			TXT_BEA_ADM_LUNAS1.Text = "0";
			TXT_BEA_ADM_LUNAS2.Text = "0";
			TXT_BEA_MATERAI.Text = "0";
			TXT_BUILD_NAME.Text = "";
			TXT_CAW_GR1.Text = "0";
			TXT_CAW_TAX1.Text = "0";
			TXT_CAW_TAX2.Text = "0";
			TXT_CAW_TAX3.Text = "0";
			TXT_CAW_TAX4.Text = "0";
			TXT_CAWIDR_TAX1.Text = "0";
			TXT_CAWIDR_TAX2.Text = "0";
			TXT_CCRA_AGREE.Text = "";
			TXT_CCRA_APPR.Text = "";
			TXT_CITY.Text = "";
			TXT_COST_LIVE.Text = "0";
			TXT_DENDA.Text = "0";
			TXT_EXP_DTBO.Text = "0";
			TXT_FG_IM.Text = "0";
			TXT_LIM_GREEN.Text = "0";
			TXT_LIM_RED.Text = "0";
			TXT_LIM_YELLOW.Text = "0";
			TXT_MAX_AGING.Text = "0";
			TXT_MAX_COST.Text = "0";
			TXT_MAX_CTAG.Text = "0";
			TXT_MAX_DTBO.Text = "0";
			TXT_MG_RATE1.Text = "0";
			TXT_MG_RATE2.Text = "0";
			TXT_MIN_DENDA.Text = "0";
			TXT_MINBEA_ADM1.Text = "0";
			TXT_MINBEA_ADM2.Text = "0";
			TXT_NILAI_JAM.Text = "0";
			TXT_PASS_LIFE.Text = "0";
			TXT_PASS_CHANGE.Text = "0";
			TXT_PASS_DIGIT.Text = "0";
			TXT_PASS_REVOKE.Text = "0";
			TXT_PASS_UNIQ.Text = "0";
			TXT_PASS_WARNING.Text = "0";
			TXT_PH_VERLESS.Text = "0";
			TXT_PH_VERLIM.Text = "0";
			TXT_PH_VERMORE.Text = "0";
			TXT_PHONE.Text = "";
			TXT_PY_VERLESS.Text = "0";
			TXT_PY_VERLIM.Text = "0";
			TXT_PY_VERMORE.Text = "0";
			TXT_SEQ_NO.Text = "";
			TXT_WAA.Text = "0";
			TXT_ZIPCODE.Text = "";

			LBL_SAVE.Text = "1";
		}

		private void BindData1()
		{
			conn.QueryString = "SELECT IN_SEQ, IN_SCPWDLIVE, BANK_NAME FROM INITIAL";
			conn.ExecuteQuery(); 

			if(conn.GetRowCount() != 0)
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				DG.DataSource = dt;

				try
				{
					DG.DataBind();
				}
				catch 
				{
					DG.CurrentPageIndex = DG.PageCount - 1;
					DG.DataBind();
				}
			} 
 
			conn.ClearData();
		}

		private void BindData2()
		{
			conn.QueryString = "SELECT IN_SEQ, IN_SCPWDLIVE, BANK_NAME, CH_STA, "+
				"STATUS = case CH_STA when '1' then 'INSERT' " +
				"when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TINITIAL";

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
			this.DG.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG_ItemCommand);
			this.DG2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG2_ItemCommand);

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

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;

			conn.QueryString = "SELECT * FROM INITIAL WHERE IN_SEQ = '"+TXT_SEQ_NO.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVE.Text == "2"))
			{
				conn.QueryString = "insert into TINITIAL"+
					" (IN_SEQ, IN_SCPWDLIVE, IN_SCPWDWARN, IN_SCPWDREV, IN_SCPWDUNIQ, IN_SCPWDDIGIT, IN_SCPWDCHG,"+
					" IN_VERPHNLIMIT, IN_VERPHNMORE, IN_VERPHNLESS, IN_VERSITLIMIT, IN_VERSITMORE, IN_VERSITLESS,"+ 
					" CW_COSTLIVING, CW_COSTLIVINGMAX, CW_GRS1, PR_CWTAXIDR1, PR_CWTAXIDR2, PR_CWTAX1, PR_CWTAX2, PR_CWTAX3, PR_CWTAX4,"+ 
					" PK_TLANG, PK_TLANGPAR, PK_LNPINJSEN, PK_LNPINJAMOUNT, PK_LNSEBSEN, PK_LNSEBAMOUNT, FD_COLTSEN,"+
					" BANK_NAME, IN_LTPLACE, IN_LTADDR1, IN_LTADDR2, IN_LTADDR3, IN_LTCITY, IN_LTZIPCODE,"+
					" IN_LTPHONE, CCRA_APPROVED, CCRA_DISETUJUI, IN_APPAGE, IN_BEAMATERAI,"+ 
					" IN_LIMITRED, IN_LIMITGREEN, IN_LIMITYELLOW, AGING_MAX, AGING_MAXCOUNT,"+
					" MAX_DTBO, EXP_DTBO, DC_MARGINRATE, SC_MARGINRATE, FC_INTRMARGIN, CH_STA) values"+
					" ('"+TXT_SEQ_NO.Text+"',"+GlobalTools.ConvertNull(TXT_PASS_LIFE.Text)+", "+GlobalTools.ConvertFloat(TXT_PASS_WARNING.Text)+", "+GlobalTools.ConvertFloat(TXT_PASS_REVOKE.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_PASS_UNIQ.Text)+", "+GlobalTools.ConvertFloat(TXT_PASS_DIGIT.Text)+", "+GlobalTools.ConvertFloat(TXT_PASS_CHANGE.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_PH_VERLIM.Text)+", "+GlobalTools.ConvertFloat(TXT_PH_VERMORE.Text)+", "+GlobalTools.ConvertFloat(TXT_PH_VERLESS.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_PY_VERLIM.Text)+", "+GlobalTools.ConvertFloat(TXT_PY_VERMORE.Text)+", "+GlobalTools.ConvertFloat(TXT_PY_VERLESS.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_COST_LIVE.Text)+", "+GlobalTools.ConvertFloat(TXT_MAX_COST.Text)+", "+GlobalTools.ConvertFloat(TXT_CAW_GR1.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_CAWIDR_TAX1.Text)+", "+GlobalTools.ConvertFloat(TXT_CAWIDR_TAX2.Text)+", "+GlobalTools.ConvertFloat(TXT_CAW_TAX1.Text)+", "+GlobalTools.ConvertFloat(TXT_CAW_TAX2.Text)+", "+GlobalTools.ConvertFloat(TXT_CAW_TAX3.Text)+", "+GlobalTools.ConvertFloat(TXT_CAW_TAX4.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_DENDA.Text)+", "+GlobalTools.ConvertFloat(TXT_MIN_DENDA.Text)+", "+GlobalTools.ConvertFloat(TXT_BEA_ADM_LUNAS1.Text)+", "+GlobalTools.ConvertFloat(TXT_MINBEA_ADM1.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_BEA_ADM_LUNAS2.Text)+", "+GlobalTools.ConvertFloat(TXT_MINBEA_ADM2.Text)+", "+GlobalTools.ConvertFloat(TXT_NILAI_JAM.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_BANK_NAME.Text)+", "+GlobalTools.ConvertNull(TXT_BUILD_NAME.Text)+", "+GlobalTools.ConvertNull(TXT_ADDR1.Text)+", "+GlobalTools.ConvertNull(TXT_ADDR2.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_ADDR3.Text)+", "+GlobalTools.ConvertNull(TXT_CITY.Text)+", "+GlobalTools.ConvertNull(TXT_ZIPCODE.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_PHONE.Text)+", "+GlobalTools.ConvertNull(TXT_CCRA_APPR.Text)+", "+GlobalTools.ConvertNull(TXT_CCRA_AGREE.Text)+","+
					" "+GlobalTools.ConvertNull(TXT_WAA.Text)+", "+GlobalTools.ConvertFloat(TXT_BEA_MATERAI.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_LIM_RED.Text)+", "+GlobalTools.ConvertFloat(TXT_LIM_GREEN.Text)+", "+GlobalTools.ConvertFloat(TXT_LIM_YELLOW.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_MAX_AGING.Text)+", "+GlobalTools.ConvertNull(TXT_MAX_CTAG.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_MAX_DTBO.Text)+", "+GlobalTools.ConvertFloat(TXT_EXP_DTBO.Text)+","+
					" "+GlobalTools.ConvertFloat(TXT_MG_RATE1.Text)+", "+GlobalTools.ConvertFloat(TXT_MG_RATE2.Text)+", "+GlobalTools.ConvertFloat(TXT_FG_IM.Text)+", '2')";
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit != 0) && (LBL_SAVE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Cannot add data for sequence number " +TXT_SEQ_NO.Text.Trim()+" !");
				return;
			}

			conn.ClearData();
 	
			BindData2();

			LBL_SAVE.Text = "1"; 
		}

		private void DG_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					conn.QueryString = "SELECT * FROM INITIAL WHERE IN_SEQ = '"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery(); 

					if(conn.GetRowCount() != 0)
					{
						TXT_ADDR1.Text = conn.GetFieldValue(0,"IN_LTADDR1");
						TXT_ADDR2.Text = conn.GetFieldValue(0,"IN_LTADDR2");
						TXT_ADDR3.Text = conn.GetFieldValue(0,"IN_LTADDR3");
						TXT_BANK_NAME.Text = conn.GetFieldValue(0,"BANK_NAME");
						TXT_BEA_ADM_LUNAS1.Text = conn.GetFieldValue(0,"PK_LNPINJSEN");
						TXT_BEA_ADM_LUNAS2.Text = conn.GetFieldValue(0,"PK_LNSEBSEN");
						TXT_BEA_MATERAI.Text = conn.GetFieldValue(0,"IN_BEAMATERAI");
						TXT_BUILD_NAME.Text = conn.GetFieldValue(0,"IN_LTPLACE");
						TXT_CAW_GR1.Text = conn.GetFieldValue(0,"CW_GRS1");
						TXT_CAW_TAX1.Text = conn.GetFieldValue(0,"PR_CWTAX1");
						TXT_CAW_TAX2.Text = conn.GetFieldValue(0,"PR_CWTAX2");
						TXT_CAW_TAX3.Text = conn.GetFieldValue(0,"PR_CWTAX3");
						TXT_CAW_TAX4.Text = conn.GetFieldValue(0,"PR_CWTAX4");
						TXT_CAWIDR_TAX1.Text = conn.GetFieldValue(0,"PR_CWTAXIDR1");
						TXT_CAWIDR_TAX2.Text = conn.GetFieldValue(0,"PR_CWTAXIDR2");
						TXT_CCRA_AGREE.Text = conn.GetFieldValue(0,"CCRA_DISETUJUI");
						TXT_CCRA_APPR.Text = conn.GetFieldValue(0,"CCRA_APPROVED");
						TXT_CITY.Text = conn.GetFieldValue(0,"IN_LTCITY");
						TXT_COST_LIVE.Text = conn.GetFieldValue(0,"CW_COSTLIVING");
						TXT_DENDA.Text = conn.GetFieldValue(0,"PK_TLANG");
						TXT_EXP_DTBO.Text = conn.GetFieldValue(0,"EXP_DTBO");
						TXT_FG_IM.Text = conn.GetFieldValue(0,"FC_INTRMARGIN");
						TXT_LIM_GREEN.Text = conn.GetFieldValue(0,"IN_LIMITGREEN");
						TXT_LIM_RED.Text = conn.GetFieldValue(0,"IN_LIMITRED");
						TXT_LIM_YELLOW.Text = conn.GetFieldValue(0,"IN_LIMITYELLOW");
						TXT_MAX_AGING.Text = conn.GetFieldValue(0,"AGING_MAX");
						TXT_MAX_COST.Text = conn.GetFieldValue(0,"CW_COSTLIVINGMAX");
						TXT_MAX_CTAG.Text = conn.GetFieldValue(0,"AGING_MAXCOUNT");
						TXT_MAX_DTBO.Text = conn.GetFieldValue(0,"MAX_DTBO");
						TXT_MG_RATE1.Text = conn.GetFieldValue(0,"DC_MARGINRATE");
						TXT_MG_RATE2.Text = conn.GetFieldValue(0,"SC_MARGINRATE");
						TXT_MIN_DENDA.Text = conn.GetFieldValue(0,"PK_TLANGPAR");
						TXT_MINBEA_ADM1.Text = conn.GetFieldValue(0,"PK_LNPINJAMOUNT");
						TXT_MINBEA_ADM2.Text = conn.GetFieldValue(0,"PK_LNSEBAMOUNT");
						TXT_NILAI_JAM.Text = conn.GetFieldValue(0,"FD_COLTSEN");
						TXT_PASS_LIFE.Text = conn.GetFieldValue(0,"IN_SCPWDLIVE");
						TXT_PASS_CHANGE.Text = conn.GetFieldValue(0,"IN_SCPWDCHG");
						TXT_PASS_DIGIT.Text = conn.GetFieldValue(0,"IN_SCPWDDIGIT");
						TXT_PASS_REVOKE.Text = conn.GetFieldValue(0,"IN_SCPWDREV");
						TXT_PASS_UNIQ.Text = conn.GetFieldValue(0,"IN_SCPWDUNIQ");
						TXT_PASS_WARNING.Text = conn.GetFieldValue(0,"IN_SCPWDWARN");
						TXT_PH_VERLESS.Text = conn.GetFieldValue(0,"IN_VERPHNLESS");
						TXT_PH_VERLIM.Text = conn.GetFieldValue(0,"IN_VERPHNLIMIT");
						TXT_PH_VERMORE.Text = conn.GetFieldValue(0,"IN_VERPHNMORE");
						TXT_PHONE.Text = conn.GetFieldValue(0,"IN_LTPHONE");
						TXT_PY_VERLESS.Text = conn.GetFieldValue(0,"IN_VERSITLESS");
						TXT_PY_VERLIM.Text = conn.GetFieldValue(0,"IN_VERSITLIMIT");
						TXT_PY_VERMORE.Text = conn.GetFieldValue(0,"IN_VERSITMORE");
						TXT_SEQ_NO.Text = conn.GetFieldValue(0,"IN_SEQ");
						TXT_WAA.Text = conn.GetFieldValue(0,"IN_APPAGE");
						TXT_ZIPCODE.Text = conn.GetFieldValue(0,"IN_LTZIPCODE");
					}

					TXT_SEQ_NO.Enabled = false;					
					LBL_SAVE.Text = "2";		
					break;
			}
		
		}

		private void DG2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string seq; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					seq = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM TINITIAL WHERE IN_SEQ = '"+seq+"'";
					conn.ExecuteQuery();
					
					BindData2();
					break;
				
			}				
		}
	}
}
