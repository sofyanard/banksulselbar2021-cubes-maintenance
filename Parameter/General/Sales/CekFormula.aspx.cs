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
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.Sales
{
	/// <summary>
	/// Summary description for CekFormula.
	/// </summary>
	public partial class CekFormula : System.Web.UI.Page
	{
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				GetData();
			}
		}

		private void SetDBConn2()
		{
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			string DB_NAMA = conn.GetFieldValue("DB_NAMA");
			string DB_IP = conn.GetFieldValue("DB_IP");
			string DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncc = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
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

		protected void BTN_TEST_Click(object sender, System.EventArgs e)
		{
			TXT_DESC.Text="";
			try
			{
				conncc.QueryString=TXT_FORMULA.Text;
				conncc.ExecuteQuery();
				TXT_RESULT.Text="Complete";
			}
			catch (Exception p)
			{
				TXT_DESC.Text = p.Message;
				TXT_RESULT.Text="Error";
			}
		}

		private void GetData()
		{

			string where1="";
			string wherenya="";
			string group1="";
			string groupnya="";
			string CAL_TRACK="";
			string CAL_FIELDDATE="";
			string CAL_FIELDTRACK="";
			string SqlFormula="";
			string selectnya="";
			string Seq_no = Request.QueryString["Seq_no"];
			
			conncc.QueryString="select * from VW_PARAM_SALESCOM_CALCULATION_FORMULA where SEQ_NO='"+Seq_no+"'";
			conncc.ExecuteQuery();
			string PR_CODE = conncc.GetFieldValue("PR_CODE");
			string AGENTYPE_ID = conncc.GetFieldValue("AGENTYPE_ID");
			string LEVEL_CODE = conncc.GetFieldValue("LEVEL_CODE");
			string PRODUCT_ID = conncc.GetFieldValue("PRODUCT_ID");

			string CalType = Request.QueryString["cal_type"];
			if(CalType != "")
			{
				conncc.QueryString="select CALCULATION_ID, CALCULATION_DESC, isnull(CAL_TRACK,'') CAL_TRACK, "+
					"isnull(CAL_FIELDDATE,'') CAL_FIELDDATE, isnull(CAL_FIELDTRACK,'') CAL_FIELDTRACK "+ 
					"from CALCULATION_TYPE where CALCULATION_ID='"+CalType+"'";
				conncc.ExecuteQuery();
				//string [] arrCalcul = CalType.Split("#".ToCharArray());
				CAL_TRACK     = conncc.GetFieldValue("CAL_TRACK");
				string [] FieldDate = conncc.GetFieldValue("CAL_FIELDDATE").Split(".".ToCharArray());
				CAL_FIELDDATE = FieldDate[1].Trim();
				string [] FieldTrack = conncc.GetFieldValue("CAL_FIELDTRACK").Split(".".ToCharArray());
				CAL_FIELDTRACK = FieldTrack[1];
			}
			string AgenType = Request.QueryString["AgenType"];
			string Formula = (string)Session["Formula"]; //obj.TXT_CALC_FORMULA.value;
			string Table = (string)Session["Table"]; //obj.TXT_CALC_TABLE.value;
			string Link = (string)Session["Link"]; //obj.TXT_CALC_LINK.value;
			string Group = (string)Session["Group"]; //obj.TXT_CALC_GROUP.value;

			string [] ArrSelect = Formula.Split(",".ToCharArray());
			int JmlSelect = ArrSelect.Length;
			selectnya = "";
			for (int i=0; i<JmlSelect;i++)
			{
				if (selectnya.Trim()=="")
				{
					selectnya = ArrSelect[i];
				} 
				else {selectnya = selectnya + ", "  + ArrSelect[i];}
			}
			SqlFormula = "Select "  + selectnya + " From AGENT ";
			
			//dari tabel mana
			string adatemp = "0";
			//string adaperiode = "0";
			string adacalresult = "0";
			string adaevaluation = "0";
			string from1 = "";

			if(PR_CODE.Trim()!="" && PR_CODE.Trim()!=null)
			{
				adatemp ="1";
				from1 = from1 +", TEMP_CUBES ";
			}

			if(Table.Trim() != "")
			{
				string [] ArrTable = Table.Split(",".ToCharArray());
				int JmlTable = ArrTable.Length;
				for (int i=0;i<JmlTable;i++)
				{
					if (ArrTable[i].Trim()!="AGENT")
					{
						if (ArrTable[i].Trim()=="TEMP_CUBES" || ArrTable[i].Trim()=="PERIODE")
						{
							if (adatemp=="0")
							{
								from1 = from1 +", "+ArrTable[i];
							}
							if (ArrTable[i].Trim() == "TEMP_CUBES"){adatemp = "1";}
						}
						else
						{
							from1 = from1 + ", "+ ArrTable[i];						
							if (ArrTable[i].Trim()=="CALCULATION_RESULT")
							{adacalresult = "1";}

							if (ArrTable[i].Trim()=="EVALUATION") {adaevaluation = "1";}
						}
					}
				}
			}
			SqlFormula = SqlFormula + from1;
			//end dari tabel mana

			//wherenya apa
			where1 = "";
			if(AGENTYPE_ID.Trim()!="" && AGENTYPE_ID.Trim()!=null)
			{
				if (where1 == "" )
				{
					where1 = where1 + " AGENT.AGENTYPE_ID = '"+AGENTYPE_ID.Trim()+"' ";
				}
				else
				{
					where1 = where1 + " and AGENT.AGENTYPE_ID = '"+AGENTYPE_ID.Trim()+"' ";
				}
			}	 
			if(LEVEL_CODE.Trim()!="" && LEVEL_CODE.Trim()!=null)
			{
				if (where1 == "" )
				{
					where1 = where1 + " AGENT.LEVEL_CODE = '"+LEVEL_CODE.Trim()+"' ";
				}
				else
				{
					where1 = where1 + " and AGENT.LEVEL_CODE = '"+LEVEL_CODE.Trim()+"' ";
				}
			}
			if(PR_CODE.Trim()!="" && PR_CODE.Trim()!=null) 
			{
				if (where1 == "" )
				{
					where1 = where1 + " TEMP_CUBES.PR_CODE = '"+PR_CODE.Trim()+"' ";
				}
				else
				{
					where1 = where1 + " and TEMP_CUBES.PR_CODE = '"+PR_CODE.Trim()+"' ";
				}
			}	 
			if(PRODUCT_ID.Trim()!="" && PRODUCT_ID.Trim()!=null) 
			{
				if (where1 == "" )
				{
					where1 = where1 + " TEMP_CUBES.PRODUCTID = '"+LEVEL_CODE.Trim()+"' ";
				}
				else
				{
					where1 = where1 + " and TEMP_CUBES.PRODUCTID = '"+LEVEL_CODE.Trim()+"' ";
				}
			}
			if (adatemp == "1" )
			{
				if (where1 =="" )
				{
					where1 = where1 + " TEMP_CUBES.AP_AGENOFRCODE = AGENT.AGEN_ID ";
				}
				else
				{
					where1 = where1 + " and TEMP_CUBES.AP_AGENOFRCODE = AGENT.AGEN_ID ";
				}
				if (CAL_FIELDDATE.Trim() != "" )
				{
					where1 = where1 + " and month(TEMP_CUBES."+CAL_FIELDDATE.Trim()+") = month(getdate()) ";
					where1 = where1 + " and year(TEMP_CUBES."+CAL_FIELDDATE.Trim()+") = year(getdate()) ";
				}
				if (CAL_TRACK.Trim() != "" && CAL_FIELDTRACK.Trim() != "" )
				{
					where1 = where1 + " and TEMP_CUBES."+CAL_FIELDTRACK.Trim()+"= '"+CAL_TRACK+"'";
				}
			}
			//end wherenya apa

			//link
			if (Link.Trim() != "" )
			{
				string [] ForLink = Link.Split(",".ToCharArray());
				int jmlcallink = ForLink.Length;
				for (int i = 0; i<jmlcallink; i++)
				{
					wherenya = "";
					if (ForLink[i].Trim() == "TEMP_CUBES.AP_AGENOFRCODE = AGENT.AGEN_ID" || ForLink[i].Trim() == "TEMP_CUBES.AP_AGENOFRCODE=AGENT.AGEN_ID" )
					{
						if (adatemp == "0" )
						{
							wherenya = ForLink[i].Trim();
						}
					}
					else
					{
						wherenya = ForLink[i].Trim();
					}
					if (where1 == "" && wherenya != "" )
					{
						where1 = where1 + wherenya ;
					}
					else if (where1 != "" && wherenya != "" )
					{
						where1 = where1 + " and "+ wherenya;
					}
				}
			}

			if (adacalresult == "1" )
			{
				if (where1 == "" )
				{
					where1 = where1 + " CALCULATION_RESULT.CAL_TYPE = ''";
				}
				else if (where1 != "" && wherenya != "" )
				{
					where1 = where1 + " and CALCULATION_RESULT.CAL_TYPE = ''";
				}
				where1 = where1 + "and CALCULATION_RESULT.CAL_MONTH = month(getdate()) and CALCULATION_RESULT.CAL_YEAR = year(getdate()) ";
			}
			if (adaevaluation == "1" )
			{
				if (where1 != "" )
				{
					where1 = where1 + " and " ;
					where1 = where1 + " CALCULATION_RESULT.CAL_MONTH = EVALUATION.EVALUATION_MONTH "+
						" and CALCULATION_RESULT.CAL_YEAR = EVALUATION.EVALUATION_YEAR ";
				}
			}
			
			if (where1.Trim() != "" )
			{
				SqlFormula = SqlFormula +" where "+ where1 ;
			}

			//group by
			if (Group.Trim() != "" )
			{
				string [] ForGroup = Group.Split(",".ToCharArray());
				int jmlcalGroup = ForGroup.Length;
				group1 = "";
				for (int i = 0; i<jmlcalGroup; i++)
				{
					if (group1.Trim() == "" )
					{
						group1 = ForGroup[i].Trim();
					}
					else
					{
						group1 = group1 + ", "+ForGroup[i].Trim();
					}
				}
				groupnya = " group by "+group1;
			}
			
			SqlFormula = SqlFormula + groupnya;
			SqlFormula = SqlFormula.Replace("~","'");
			TXT_FORMULA.Text = SqlFormula;
			//------akhir--------
		}

	}
}
