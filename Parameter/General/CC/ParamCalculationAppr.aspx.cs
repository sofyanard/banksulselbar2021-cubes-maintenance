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

namespace CuBES_Maintenance.Parameter.General.CC
{
	/// <summary>
	/// Summary description for ParamCalculationAppr.
	/// </summary>
	public partial class ParamCalculationAppr : System.Web.UI.Page
	{
		protected Connection conn,conn2;
		protected Connection connsme = new Connection (System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		string IN_CWOPRCODE1_OLD ,IN_CWOPRCODE2_OLD ,IN_CWOPRCODE3_OLD ,IN_CWOPRCODE5_OLD ,IN_CWOPRCODE6_OLD;
		string IN_CWCCRCODE2_OLD ,IN_CWCCRCODE3_OLD ,IN_CWCCRCODE4_OLD ,IN_CWCCRCODE5_OLD ,IN_CWCALCODE1_OLD;
		string IN_CWCALCODE11_OLD ,IN_CWCALCODE2_OLD ,IN_CWCALCODE3_OLD ,IN_CWCALCODE4_OLD ,IN_CWCALCODE5_OLD;
		string IN_CWCALCODE6_OLD ,IN_CWCALCODE12_OLD ,IN_CWCALCODE7_OLD ,IN_CWCALCODE8_OLD ,IN_CWCALCODE9_OLD;
		string IN_CWCALCODE10_OLD ,IN_CWCALCODE13_OLD ,IN_CWOPRCODE4_OLD ,IN_LIMIT_OLD ,IN_TINCREG_OLD;
		string IN_TACCREG_OLD ,IN_SERVIND10_OLD ,IN_SERVIND12_OLD,IN_MAXOPEN_OLD;
		string IN_CWOPRCODE1,IN_CWOPRCODE2,IN_CWOPRCODE3,IN_CWOPRCODE5,IN_CWOPRCODE6;
		string IN_CWCCRCODE2,IN_CWCCRCODE3,IN_CWCCRCODE4,IN_CWCCRCODE5,IN_CWCALCODE1;
		string IN_CWCALCODE11,IN_CWCALCODE2,IN_CWCALCODE3,IN_CWCALCODE4,IN_CWCALCODE5;
		string IN_CWCALCODE6,IN_CWCALCODE12,IN_CWCALCODE7,IN_CWCALCODE8,IN_CWCALCODE9;
		string IN_CWCALCODE10,IN_CWCALCODE13,IN_CWOPRCODE4,IN_LIMIT,IN_TINCREG;
		string IN_TACCREG,IN_SERVIND10,IN_SERVIND12,IN_MAXOPEN;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				FillDDL();
				ViewPendingData();
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

		public void SetDBConn2()
		{
			conn2.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn2.ExecuteQuery();
			string DB_NAMA = conn2.GetFieldValue("DB_NAMA");
			string DB_IP = conn2.GetFieldValue("DB_IP");
			string DB_LOGINID = conn2.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn2.GetFieldValue("DB_LOGINPWD");
			conn = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void FillDDL()
		{
			string strquery = "select cw_code, cw_desc from rfcawlst order by cw_desc";
			GlobalTools.fillRefList(DDL_IN_CWOPRCODE1,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWOPRCODE2,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWOPRCODE3,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWOPRCODE5,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWOPRCODE6,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCCRCODE2,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCCRCODE3,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCCRCODE4,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCCRCODE5,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE1,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE11,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE2,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE3,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE4,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE5,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE6,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE12,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE7,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE8,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE9,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE10,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWCALCODE13,strquery,conn);
			GlobalTools.fillRefList(DDL_IN_CWOPRCODE4,strquery,conn);
		}

		private void ClearControl()
		{
			DDL_IN_CWOPRCODE1.SelectedValue = "";
			DDL_IN_CWOPRCODE2.SelectedValue = "";
			DDL_IN_CWOPRCODE3.SelectedValue = "";
			DDL_IN_CWOPRCODE5.SelectedValue = "";
			DDL_IN_CWOPRCODE6.SelectedValue = "";
			DDL_IN_CWCCRCODE2.SelectedValue = "";
			DDL_IN_CWCCRCODE3.SelectedValue = "";
			DDL_IN_CWCCRCODE4.SelectedValue = "";
			DDL_IN_CWCCRCODE5.SelectedValue = "";
			DDL_IN_CWCALCODE1.SelectedValue = "";
			DDL_IN_CWCALCODE11.SelectedValue = "";
			DDL_IN_CWCALCODE2.SelectedValue = "";
			DDL_IN_CWCALCODE3.SelectedValue = "";
			DDL_IN_CWCALCODE4.SelectedValue = "";
			DDL_IN_CWCALCODE5.SelectedValue = "";
			DDL_IN_CWCALCODE6.SelectedValue = "";
			DDL_IN_CWCALCODE12.SelectedValue = "";
			DDL_IN_CWCALCODE7.SelectedValue = "";
			DDL_IN_CWCALCODE8.SelectedValue = "";
			DDL_IN_CWCALCODE9.SelectedValue = "";
			DDL_IN_CWCALCODE10.SelectedValue = "";
			DDL_IN_CWCALCODE13.SelectedValue = "";
			DDL_IN_CWOPRCODE4.SelectedValue = "";
			TXT_IN_LIMIT.Text = "";
			TXT_IN_TINCREG.Text = "";
			TXT_IN_TACCREG.Text = "";
			TXT_IN_SERVIND10.Text = "";
			TXT_IN_SERVIND12.Text = "";
			TXT_IN_MAXOPEN.Text = "";
			this.BTN_SAVE.Enabled = false;
		}

		protected void ViewPendingData()
		{
			conn.QueryString = "select * from VW_PENDING_CC_INITIAL_CALCULATION ";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				DDL_IN_CWOPRCODE1.SelectedValue = conn.GetFieldValue("IN_CWOPRCODE1");
				DDL_IN_CWOPRCODE2.SelectedValue = conn.GetFieldValue("IN_CWOPRCODE2");
				DDL_IN_CWOPRCODE3.SelectedValue = conn.GetFieldValue("IN_CWOPRCODE3");
				DDL_IN_CWOPRCODE5.SelectedValue = conn.GetFieldValue("IN_CWOPRCODE5");
				DDL_IN_CWOPRCODE6.SelectedValue = conn.GetFieldValue("IN_CWOPRCODE6");
				DDL_IN_CWCCRCODE2.SelectedValue = conn.GetFieldValue("IN_CWCCRCODE2");
				DDL_IN_CWCCRCODE3.SelectedValue = conn.GetFieldValue("IN_CWCCRCODE3");
				DDL_IN_CWCCRCODE4.SelectedValue = conn.GetFieldValue("IN_CWCCRCODE4");
				DDL_IN_CWCCRCODE5.SelectedValue = conn.GetFieldValue("IN_CWCCRCODE5");
				DDL_IN_CWCALCODE1.SelectedValue = conn.GetFieldValue("IN_CWCALCODE1");
				DDL_IN_CWCALCODE11.SelectedValue = conn.GetFieldValue("IN_CWCALCODE11");
				DDL_IN_CWCALCODE2.SelectedValue = conn.GetFieldValue("IN_CWCALCODE2");
				DDL_IN_CWCALCODE3.SelectedValue = conn.GetFieldValue("IN_CWCALCODE3");
				DDL_IN_CWCALCODE4.SelectedValue = conn.GetFieldValue("IN_CWCALCODE4");
				DDL_IN_CWCALCODE5.SelectedValue = conn.GetFieldValue("IN_CWCALCODE5");
				DDL_IN_CWCALCODE6.SelectedValue = conn.GetFieldValue("IN_CWCALCODE6");
				DDL_IN_CWCALCODE12.SelectedValue = conn.GetFieldValue("IN_CWCALCODE12");
				DDL_IN_CWCALCODE7.SelectedValue = conn.GetFieldValue("IN_CWCALCODE7");
				DDL_IN_CWCALCODE8.SelectedValue = conn.GetFieldValue("IN_CWCALCODE8");
				DDL_IN_CWCALCODE9.SelectedValue = conn.GetFieldValue("IN_CWCALCODE9");
				DDL_IN_CWCALCODE10.SelectedValue = conn.GetFieldValue("IN_CWCALCODE10");
				DDL_IN_CWCALCODE13.SelectedValue = conn.GetFieldValue("IN_CWCALCODE13");
				DDL_IN_CWOPRCODE4.SelectedValue = conn.GetFieldValue("IN_CWOPRCODE4");
				TXT_IN_LIMIT.Text = GlobalTools.MoneyFormat(conn.GetFieldValue("IN_LIMIT"));
				TXT_IN_TINCREG.Text = conn.GetFieldValue("IN_TINCREG");
				TXT_IN_TACCREG.Text = conn.GetFieldValue("IN_TACCREG");
				TXT_IN_SERVIND10.Text = conn.GetFieldValue("IN_SERVIND10");
				TXT_IN_SERVIND12.Text = conn.GetFieldValue("IN_SERVIND12");
				TXT_IN_MAXOPEN.Text = conn.GetFieldValue("IN_MAXOPEN");

				if (conn.GetFieldValue("IN_CWOPRCODE1") == ""
					&& conn.GetFieldValue("IN_CWOPRCODE2") == ""
					&& conn.GetFieldValue("IN_CWOPRCODE3") == ""
					&& conn.GetFieldValue("IN_CWOPRCODE5") == ""
					&& conn.GetFieldValue("IN_CWOPRCODE6") == ""
					&& conn.GetFieldValue("IN_CWCCRCODE2") == ""
					&& conn.GetFieldValue("IN_CWCCRCODE3") == ""
					&& conn.GetFieldValue("IN_CWCCRCODE4") == ""
					&& conn.GetFieldValue("IN_CWCCRCODE5") == ""
					&& conn.GetFieldValue("IN_CWCALCODE1") == ""
					&& conn.GetFieldValue("IN_CWCALCODE11") == ""
					&& conn.GetFieldValue("IN_CWCALCODE2") == ""
					&& conn.GetFieldValue("IN_CWCALCODE3") == ""
					&& conn.GetFieldValue("IN_CWCALCODE4") == ""
					&& conn.GetFieldValue("IN_CWCALCODE5") == ""
					&& conn.GetFieldValue("IN_CWCALCODE6") == ""
					&& conn.GetFieldValue("IN_CWCALCODE12") == ""
					&& conn.GetFieldValue("IN_CWCALCODE7") == ""
					&& conn.GetFieldValue("IN_CWCALCODE8") == ""
					&& conn.GetFieldValue("IN_CWCALCODE9") == ""
					&& conn.GetFieldValue("IN_CWCALCODE10") == ""
					&& conn.GetFieldValue("IN_CWCALCODE13") == ""
					&& conn.GetFieldValue("IN_CWOPRCODE4") == ""
					&& (conn.GetFieldValue("IN_LIMIT") == "" || conn.GetFieldValue("IN_LIMIT") == "0") 
					&& (conn.GetFieldValue("IN_TINCREG") == "" || conn.GetFieldValue("IN_TINCREG") == "0")
					&& (conn.GetFieldValue("IN_TACCREG") == "" || conn.GetFieldValue("IN_TACCREG") == "0")
					&& (conn.GetFieldValue("IN_SERVIND10") == "" || conn.GetFieldValue("IN_SERVIND10") == "0")
					&& (conn.GetFieldValue("IN_SERVIND12") == "" || conn.GetFieldValue("IN_SERVIND12") == "0")
					&& (conn.GetFieldValue("IN_MAXOPEN") == "" || conn.GetFieldValue("IN_MAXOPEN") == "0"))
				{
					this.BTN_SAVE.Enabled = false;
				}
					
			}
			conn.ClearData();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			AuditTrail();
			conn.QueryString = " update INITIAL SET IN_CWOPRCODE1 = "+ GlobalTools.ConvertNull(DDL_IN_CWOPRCODE1.SelectedValue)
				+",  IN_CWOPRCODE2 = "+ GlobalTools.ConvertNull(DDL_IN_CWOPRCODE2.SelectedValue)
				+",  IN_CWOPRCODE3 = "+ GlobalTools.ConvertNull(DDL_IN_CWOPRCODE3.SelectedValue)
				+",  IN_CWOPRCODE5 = "+ GlobalTools.ConvertNull(DDL_IN_CWOPRCODE5.SelectedValue)
				+",  IN_CWOPRCODE6 = "+ GlobalTools.ConvertNull(DDL_IN_CWOPRCODE6.SelectedValue)
				+",  IN_CWCCRCODE2 = "+ GlobalTools.ConvertNull(DDL_IN_CWCCRCODE2.SelectedValue)
				+",  IN_CWCCRCODE3 = "+ GlobalTools.ConvertNull(DDL_IN_CWCCRCODE3.SelectedValue)
				+",  IN_CWCCRCODE4 = "+ GlobalTools.ConvertNull(DDL_IN_CWCCRCODE4.SelectedValue)
				+",  IN_CWCCRCODE5 = "+ GlobalTools.ConvertNull(DDL_IN_CWCCRCODE5.SelectedValue)
				+",  IN_CWCALCODE1 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE1.SelectedValue)
				+",  IN_CWCALCODE11 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE11.SelectedValue)
				+",  IN_CWCALCODE2 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE2.SelectedValue)
				+",  IN_CWCALCODE3 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE3.SelectedValue)
				+",  IN_CWCALCODE4 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE4.SelectedValue)
				+",  IN_CWCALCODE5 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE5.SelectedValue)
				+",  IN_CWCALCODE6 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE6.SelectedValue)
				+",  IN_CWCALCODE12 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE12.SelectedValue)
				+",  IN_CWCALCODE7 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE7.SelectedValue)
				+",  IN_CWCALCODE8 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE8.SelectedValue)
				+",  IN_CWCALCODE9 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE9.SelectedValue)
				+",  IN_CWCALCODE10 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE10.SelectedValue)
				+",  IN_CWCALCODE13 = "+ GlobalTools.ConvertNull(DDL_IN_CWCALCODE13.SelectedValue)
				+",  IN_CWOPRCODE4 = "+ GlobalTools.ConvertNull(DDL_IN_CWOPRCODE4.SelectedValue)
				+",  IN_LIMIT = "+ GlobalTools.ConvertFloat(TXT_IN_LIMIT.Text)
				+",  IN_TINCREG = '"+ TXT_IN_TINCREG.Text
				+"',  IN_TACCREG = '"+ TXT_IN_TACCREG.Text
				+"',  IN_SERVIND10 = '"+ TXT_IN_SERVIND10.Text
				+"',  IN_SERVIND12 = '"+ TXT_IN_SERVIND12.Text
				+"',  IN_MAXOPEN = '"+ TXT_IN_MAXOPEN.Text + "'";
			conn.ExecuteNonQuery();
			DeletePendingData();
			ClearControl();
		}

		private void AuditTrail()
		{
			//Get Current data
			conn.QueryString = "select * from VW_INITIAL_CALCULTAION ";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				IN_CWOPRCODE1_OLD = conn.GetFieldValue("IN_CWOPRCODE1");
				IN_CWOPRCODE2_OLD = conn.GetFieldValue("IN_CWOPRCODE2");
				IN_CWOPRCODE3_OLD = conn.GetFieldValue("IN_CWOPRCODE3");
				IN_CWOPRCODE5_OLD = conn.GetFieldValue("IN_CWOPRCODE5");
				IN_CWOPRCODE6_OLD = conn.GetFieldValue("IN_CWOPRCODE6");
				IN_CWCCRCODE2_OLD = conn.GetFieldValue("IN_CWCCRCODE2");
				IN_CWCCRCODE3_OLD = conn.GetFieldValue("IN_CWCCRCODE3");
				IN_CWCCRCODE4_OLD = conn.GetFieldValue("IN_CWCCRCODE4");
				IN_CWCCRCODE5_OLD = conn.GetFieldValue("IN_CWCCRCODE5");
				IN_CWCALCODE1_OLD = conn.GetFieldValue("IN_CWCALCODE1");
				IN_CWCALCODE11_OLD = conn.GetFieldValue("IN_CWCALCODE11");
				IN_CWCALCODE2_OLD = conn.GetFieldValue("IN_CWCALCODE2");
				IN_CWCALCODE3_OLD = conn.GetFieldValue("IN_CWCALCODE3");
				IN_CWCALCODE4_OLD = conn.GetFieldValue("IN_CWCALCODE4");
				IN_CWCALCODE5_OLD = conn.GetFieldValue("IN_CWCALCODE5");
				IN_CWCALCODE6_OLD = conn.GetFieldValue("IN_CWCALCODE6");
				IN_CWCALCODE12_OLD = conn.GetFieldValue("IN_CWCALCODE12");
				IN_CWCALCODE7_OLD = conn.GetFieldValue("IN_CWCALCODE7");
				IN_CWCALCODE8_OLD = conn.GetFieldValue("IN_CWCALCODE8");
				IN_CWCALCODE9_OLD = conn.GetFieldValue("IN_CWCALCODE9");
				IN_CWCALCODE10_OLD = conn.GetFieldValue("IN_CWCALCODE10");
				IN_CWCALCODE13_OLD = conn.GetFieldValue("IN_CWCALCODE13");
				IN_CWOPRCODE4_OLD = conn.GetFieldValue("IN_CWOPRCODE4");
				IN_LIMIT_OLD = conn.GetFieldValue("IN_LIMIT");
				IN_TINCREG_OLD = conn.GetFieldValue("IN_TINCREG");
				IN_TACCREG_OLD = conn.GetFieldValue("IN_TACCREG");
				IN_SERVIND10_OLD = conn.GetFieldValue("IN_SERVIND10");
				IN_SERVIND12_OLD = conn.GetFieldValue("IN_SERVIND12");
				IN_MAXOPEN_OLD = conn.GetFieldValue("IN_MAXOPEN");
			}
			conn.ClearData();
			//GET PENDING DATA
			conn.QueryString = "select IN_LIMIT from VW_PENDING_CC_INITIAL_CALCULATION ";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				IN_CWOPRCODE1 = DDL_IN_CWOPRCODE1.SelectedValue;
				IN_CWOPRCODE2 = DDL_IN_CWOPRCODE2.SelectedValue;
				IN_CWOPRCODE3 = DDL_IN_CWOPRCODE3.SelectedValue;
				IN_CWOPRCODE5 = DDL_IN_CWOPRCODE5.SelectedValue;
				IN_CWOPRCODE6 = DDL_IN_CWOPRCODE6.SelectedValue;
				IN_CWCCRCODE2 = DDL_IN_CWCCRCODE2.SelectedValue;
				IN_CWCCRCODE3 = DDL_IN_CWCCRCODE3.SelectedValue;
				IN_CWCCRCODE4 = DDL_IN_CWCCRCODE4.SelectedValue;
				IN_CWCCRCODE5 = DDL_IN_CWCCRCODE5.SelectedValue;
				IN_CWCALCODE1 = DDL_IN_CWCALCODE1.SelectedValue;
				IN_CWCALCODE11 = DDL_IN_CWCALCODE11.SelectedValue;
				IN_CWCALCODE2 = DDL_IN_CWCALCODE2.SelectedValue;
				IN_CWCALCODE3 = DDL_IN_CWCALCODE3.SelectedValue;
				IN_CWCALCODE4 = DDL_IN_CWCALCODE4.SelectedValue;
				IN_CWCALCODE5 = DDL_IN_CWCALCODE5.SelectedValue;
				IN_CWCALCODE6 = DDL_IN_CWCALCODE6.SelectedValue;
				IN_CWCALCODE12 = DDL_IN_CWCALCODE12.SelectedValue;
				IN_CWCALCODE7 = DDL_IN_CWCALCODE7.SelectedValue;
				IN_CWCALCODE8 = DDL_IN_CWCALCODE8.SelectedValue;
				IN_CWCALCODE9 = DDL_IN_CWCALCODE9.SelectedValue;
				IN_CWCALCODE10 = DDL_IN_CWCALCODE10.SelectedValue;
				IN_CWCALCODE13 = DDL_IN_CWCALCODE13.SelectedValue;
				IN_CWOPRCODE4 = DDL_IN_CWOPRCODE4.SelectedValue;
				IN_LIMIT = conn.GetFieldValue("IN_LIMIT");
				IN_TINCREG = TXT_IN_TINCREG.Text;
				IN_TACCREG = TXT_IN_TACCREG.Text;
				IN_SERVIND10 = TXT_IN_SERVIND10.Text;
				IN_SERVIND12 = TXT_IN_SERVIND12.Text;
				IN_MAXOPEN = TXT_IN_MAXOPEN.Text;
			}
			conn.ClearData();
			//*******
			if (IN_CWOPRCODE1_OLD != IN_CWOPRCODE1)
				ExecSPAuditTrail("IN_CWOPRCODE1",IN_CWOPRCODE1_OLD,IN_CWOPRCODE1);								
			if (IN_CWOPRCODE2_OLD != IN_CWOPRCODE2)
				ExecSPAuditTrail("IN_CWOPRCODE2",IN_CWOPRCODE2_OLD,IN_CWOPRCODE2);
			if (IN_CWOPRCODE3_OLD != IN_CWOPRCODE3)
				ExecSPAuditTrail("IN_CWOPRCODE3",IN_CWOPRCODE3_OLD,IN_CWOPRCODE3);
			if (IN_CWOPRCODE5_OLD != IN_CWOPRCODE5)
				ExecSPAuditTrail("IN_CWOPRCODE5",IN_CWOPRCODE5_OLD,IN_CWOPRCODE5);
			if (IN_CWOPRCODE6_OLD != IN_CWOPRCODE6)
				ExecSPAuditTrail("IN_CWOPRCODE6",IN_CWOPRCODE6_OLD,IN_CWOPRCODE6);
			if (IN_CWCCRCODE2_OLD != IN_CWCCRCODE2)
				ExecSPAuditTrail("IN_CWCCRCODE2",IN_CWCCRCODE2_OLD,IN_CWCCRCODE2);
			if (IN_CWCCRCODE3_OLD != IN_CWCCRCODE3)
				ExecSPAuditTrail("IN_CWCCRCODE3",IN_CWCCRCODE3_OLD,IN_CWCCRCODE3);
			if (IN_CWCCRCODE4_OLD != IN_CWCCRCODE4)
				ExecSPAuditTrail("IN_CWCCRCODE4",IN_CWCCRCODE4_OLD,IN_CWCCRCODE4);
			if (IN_CWCCRCODE5_OLD != IN_CWCCRCODE5)
				ExecSPAuditTrail("IN_CWCCRCODE5",IN_CWCCRCODE5_OLD,IN_CWCCRCODE5);
			if (IN_CWCALCODE1_OLD != IN_CWCALCODE1)
				ExecSPAuditTrail("IN_CWCALCODE1",IN_CWCALCODE1_OLD,IN_CWCALCODE1);
			if (IN_CWCALCODE11_OLD != IN_CWCALCODE11)
				ExecSPAuditTrail("IN_CWCALCODE11",IN_CWCALCODE11_OLD,IN_CWCALCODE11);
			if (IN_CWCALCODE2_OLD != IN_CWCALCODE2)
				ExecSPAuditTrail("IN_CWCALCODE2",IN_CWCALCODE2_OLD,IN_CWCALCODE2);
			if (IN_CWCALCODE3_OLD != IN_CWCALCODE3)
				ExecSPAuditTrail("IN_CWCALCODE3",IN_CWCALCODE3_OLD,IN_CWCALCODE3);
			if (IN_CWCALCODE4_OLD != IN_CWCALCODE4)
				ExecSPAuditTrail("IN_CWCALCODE4",IN_CWCALCODE4_OLD,IN_CWCALCODE4);
			if (IN_CWCALCODE5_OLD != IN_CWCALCODE5)
				ExecSPAuditTrail("IN_CWCALCODE5",IN_CWCALCODE5_OLD,IN_CWCALCODE5);
			if (IN_CWCALCODE6_OLD != IN_CWCALCODE6)
				ExecSPAuditTrail("IN_CWCALCODE6",IN_CWCALCODE6_OLD,IN_CWCALCODE6);
			if (IN_CWCALCODE12_OLD != IN_CWCALCODE12)
				ExecSPAuditTrail("IN_CWCALCODE12",IN_CWCALCODE12_OLD,IN_CWCALCODE12);
			if (IN_CWCALCODE7_OLD != IN_CWCALCODE7)
				ExecSPAuditTrail("IN_CWCALCODE7",IN_CWCALCODE7_OLD,IN_CWCALCODE7);
			if (IN_CWCALCODE8_OLD != IN_CWCALCODE8)
				ExecSPAuditTrail("IN_CWCALCODE8",IN_CWCALCODE8_OLD,IN_CWCALCODE8);
			if (IN_CWCALCODE9_OLD != IN_CWCALCODE9)
				ExecSPAuditTrail("IN_CWCALCODE9",IN_CWCALCODE9_OLD,IN_CWCALCODE9);
			if (IN_CWCALCODE10_OLD != IN_CWCALCODE10)
				ExecSPAuditTrail("IN_CWCALCODE10",IN_CWCALCODE10_OLD,IN_CWCALCODE10);
			if (IN_CWCALCODE13_OLD != IN_CWCALCODE13)
				ExecSPAuditTrail("IN_CWCALCODE13",IN_CWCALCODE13_OLD,IN_CWCALCODE13);
			if (IN_CWOPRCODE4_OLD != IN_CWOPRCODE4)
				ExecSPAuditTrail("IN_CWOPRCODE4",IN_CWOPRCODE4_OLD,IN_CWOPRCODE4);
			if (IN_LIMIT_OLD != IN_LIMIT)
				ExecSPAuditTrail("IN_LIMIT",IN_LIMIT_OLD,IN_LIMIT);
			if (IN_TINCREG_OLD != IN_TINCREG)
				ExecSPAuditTrail("IN_TINCREG",IN_TINCREG_OLD,IN_TINCREG);
			if (IN_TACCREG_OLD != IN_TACCREG)
				ExecSPAuditTrail("IN_TACCREG",IN_TACCREG_OLD,IN_TACCREG);
			if (IN_SERVIND10_OLD != IN_SERVIND10)
				ExecSPAuditTrail("IN_SERVIND10",IN_SERVIND10_OLD,IN_SERVIND10);
			if (IN_SERVIND12_OLD != IN_SERVIND12)
				ExecSPAuditTrail("IN_SERVIND12",IN_SERVIND12_OLD,IN_SERVIND12);
			if (IN_MAXOPEN_OLD != IN_MAXOPEN)
				ExecSPAuditTrail("IN_MAXOPEN",IN_MAXOPEN_OLD,IN_MAXOPEN);

		}

		private void ExecSPAuditTrail(string field,string from, string to)
		{
			string userid	= Session["UserID"].ToString();
			string paramid	= "36";
			string tablename = "INITIAL";

			connsme.QueryString = "select PARAMNAME,PG_ID from RFPARAMETERALL where paramid='" + paramid + "' and moduleid='20' and classid='' and ismaker=0";
			connsme.ExecuteQuery();
			string paramname	= connsme.GetFieldValue("PARAMNAME");
			string paramclass	= connsme.GetFieldValue("PG_ID");
			connsme.ClearData();
			
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + "1" + "','" + tablename + "','" +
				field + "','" + from + "','" + to + "','" + "0" + "','" + userid + "','" + 
				"" + "','" + paramclass + "','" + paramname  + "'";
			conn.ExecuteNonQuery();
		}

		private void DeletePendingData()
		{
			conn.QueryString = " update PENDING_CC_INITIAL SET IN_CWOPRCODE1 = NULL"
				+",  IN_CWOPRCODE2 = NULL"
				+",  IN_CWOPRCODE3 = NULL"
				+",  IN_CWOPRCODE5 = NULL"
				+",  IN_CWOPRCODE6 = NULL"
				+",  IN_CWCCRCODE2 = NULL"
				+",  IN_CWCCRCODE3 = NULL"
				+",  IN_CWCCRCODE4 = NULL"
				+",  IN_CWCCRCODE5 = NULL"
				+",  IN_CWCALCODE1 = NULL"
				+",  IN_CWCALCODE11 = NULL"
				+",  IN_CWCALCODE2 = NULL"
				+",  IN_CWCALCODE3 = NULL"
				+",  IN_CWCALCODE4 = NULL"
				+",  IN_CWCALCODE5 = NULL"
				+",  IN_CWCALCODE6 = NULL"
				+",  IN_CWCALCODE12 = NULL"
				+",  IN_CWCALCODE7 = NULL"
				+",  IN_CWCALCODE8 = NULL"
				+",  IN_CWCALCODE9 = NULL"
				+",  IN_CWCALCODE10 = NULL"
				+",  IN_CWCALCODE13 = NULL"
				+",  IN_CWOPRCODE4 = NULL"
				+",  IN_LIMIT = NULL"
				+",  IN_TINCREG = NULL"
				+",  IN_TACCREG = NULL"
				+",  IN_SERVIND10 = NULL"
				+",  IN_SERVIND12 = NULL"
				+",  IN_MAXOPEN = NULL";
			conn.ExecuteNonQuery();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}
}
