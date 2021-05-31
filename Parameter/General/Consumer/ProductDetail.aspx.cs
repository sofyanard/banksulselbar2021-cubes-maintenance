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


namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for ProductDetail.
	/// </summary>
	public partial class ProductDetail : System.Web.UI.Page
	{
		protected Connection conn, conn2;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string mid = Request.QueryString["ModuleId"],
				table = Request.QueryString["table"],
				ch_sta = Request.QueryString["ch_sta"],
				id = Request.QueryString["productid"];

			conn2 = new Connection((string) Session["ConnString"]);
			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();

			string connstr = "Data Source=" + conn2.GetFieldValue("db_ip") +
				";Initial Catalog=" + conn2.GetFieldValue("db_nama") +
				";uid=" + conn2.GetFieldValue("db_loginid") +
				";pwd=" + conn2.GetFieldValue("db_loginpwd") +
				";Pooling=true";
			conn = new Connection(connstr);

			conn.QueryString = "select * from " + table + " where productid = '"+id+"'";
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0)
			{
								
				LBL_PID.Text = conn.GetFieldValue(0,"PRODUCTID");
				LBL_PRNAME.Text = conn.GetFieldValue(0,"PRODUCTNAME");
				LBL_ADMIN.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"PR_ADMIN")); 

				LBL_AIP.Text = conn.GetFieldValue(0,"PR_AIPLIMITTIME");
				LBL_BEA_OTHER.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"PR_BEAOTH"));
				LBL_CEIL_LIM.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"CEILLING_LIMIT")); 
				LBL_DP.Text = conn.GetFieldValue(0,"DOWN_PAYMENT")+ " %";
				LBL_EMAS_CODE.Text = conn.GetFieldValue(0,"CD_SIBS");
				LBL_FIDU.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"PR_FIDUCIA")); 
				LBL_FIDU_LIM.Text = conn.GetFieldValue(0,"PR_LIMITFIDUCIA");
				LBL_FLOOR_LIM.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"FLOOR_LIMIT"));
				LBL_FLOOR_RATE.Text = conn.GetFieldValue(0,"FLOOR_RATE");
				LBL_GRP_NAME.Text = conn.GetFieldValue(0,"GROUP_NAME");
				LBL_MKSC.Text = conn.GetFieldValue(0,"PR_SRCCODE");
				LBL_NPWP.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"MIN_LIMIT_NPWP"));
				LBL_PROMO.Text = conn.GetFieldValue(0,"NAMA_PROMO"); 
				LBL_PROV.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"PR_PROVISI")); 
				LBL_PROV_RATE.Text = conn.GetFieldValue(0,"PR_PROVPERCENT") + " %";
				LBL_SPPK.Text = conn.GetFieldValue(0,"PR_SPPKLMTTIME");
				LBL_TYPE.Text = conn.GetFieldValue(0,"PROD_TP"); 
				
				//FOR CHILDPRODUCT
				LBL_CHILDPRODUCT.Text = conn.GetFieldValue(0,"CHILDPRODUCTNAME");
				LBL_CHILDPRODUCTID.Text = conn.GetFieldValue(0,"CHILDPRODUCT");
				LBL_CHILDMINTENOR.Text = conn.GetFieldValue(0,"CHILDMINTENOR");
				LBL_CHILDMAXTENOR.Text = conn.GetFieldValue(0,"CHILDMAXTENOR"); 
				LBL_CHILDDEFTENOR.Text = conn.GetFieldValue(0,"CHILDDEFTENOR"); 
				LBL_CHILDMINRATIO.Text = conn.GetFieldValue(0,"CHILDMINRATIO");
				LBL_CHILDMAXRATIO.Text = conn.GetFieldValue(0,"CHILDMAXRATIO"); 
				LBL_CHILDDEFRATIO.Text = conn.GetFieldValue(0,"CHILDDEFRATIO"); 
				LBL_CHILDMININTEREST.Text = conn.GetFieldValue(0,"CHILDMININTEREST");
				LBL_CHILDMAXINTEREST.Text = conn.GetFieldValue(0,"CHILDMAXINTEREST"); 
				LBL_CHILDDEFINTEREST.Text = conn.GetFieldValue(0,"CHILDDEFINTEREST");
				LBL_CHILDMINLIMIT.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"CHILDMINLIMIT"));
				LBL_CHILDMAXLIMIT.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"CHILDMAXLIMIT"));			


				if(conn.GetFieldValue(0,"NL_CHECK") == "1")
					LBL_NEG_LIST.Text = "yes";
				else
					LBL_NEG_LIST.Text = "no";

				if(conn.GetFieldValue(0,"BLACKLIST_CHECK") == "1")
					LBL_BLACK.Text = "yes";
				else
					LBL_BLACK.Text = "no";

				if(conn.GetFieldValue(0,"PRESCRE_CHECK") == "1")
					LBL_PRES.Text = "yes";
				else
					LBL_PRES.Text = "no";

				if(conn.GetFieldValue(0,"DHBI_CHECK") == "1")
					LBL_DHBI.Text = "yes";
				else
					LBL_DHBI.Text = "no";

				if(conn.GetFieldValue(0,"PR_SPK") == "1")
					LBL_SPK.Text = "yes";
				else
					LBL_SPK.Text = "no";

				if(conn.GetFieldValue(0,"PR_KENDARA") == "1")
					LBL_PR_KENDARA.Text = "yes";
				else
					LBL_PR_KENDARA.Text = "no";

				if(conn.GetFieldValue(0,"REVOLVING") == "1")
					LBL_REVOLVING.Text = "yes";
				else
					LBL_REVOLVING.Text = "no";

				if(conn.GetFieldValue(0,"ROUND_APPROVAL") == "1")
					LBL_ROUND_APPROVAL.Text = "yes";
				else
					LBL_ROUND_APPROVAL.Text = "no";

				if(conn.GetFieldValue(0,"ACTIVE") == "1")
					LBL_ACTIVE.Text = "yes";
				else
					LBL_ACTIVE.Text = "no";

				if(conn.GetFieldValue(0,"DOC_RAB") == "1")
					LBL_DOC_RAB.Text = "yes";
				else
					LBL_DOC_RAB.Text = "no";

				if(conn.GetFieldValue(0,"CUSTOMER_TYPE") == "0")
					LBL_CUST_TYPE.Text = "Personal";
				else if(conn.GetFieldValue(0,"CUSTOMER_TYPE") == "1")
					LBL_CUST_TYPE.Text = "Company";
				else if(conn.GetFieldValue(0,"CUSTOMER_TYPE") == "2")
					LBL_CUST_TYPE.Text = "Personal and Company";
				else
					LBL_CUST_TYPE.Text = "&nbsp;";

				if(conn.GetFieldValue(0,"ALLOWCARDBUNDLING") == "1")
					LBL_CARDBUNDLING.Text = "yes";
				else
					LBL_CARDBUNDLING.Text = "no";

				if(conn.GetFieldValue(0,"PR_MITRAKARYA") == "1")
					LBL_PR_MITRAKARYA.Text = "yes";
				else
					LBL_PR_MITRAKARYA.Text = "no";
			}
			else
			{
				
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

		}
		#endregion
	}
}
