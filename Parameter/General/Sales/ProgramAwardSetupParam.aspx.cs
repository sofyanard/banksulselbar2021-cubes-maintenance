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
	/// Summary description for ProgramAwardSetupParam.
	/// </summary>
	public partial class ProgramAwardSetupParam : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=SALESMANDIRI;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		string ID1,ID2;
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				DDL_MONTH_START.Items.Add(new ListItem("--Select--",""));
				DDL_MONTH_END.Items.Add(new ListItem("--Select--",""));
				for (int i=1; i<=12; i++)
				{
					DDL_MONTH_START.Items.Add(new ListItem(DateAndTime.MonthName(i,false), i.ToString()));
					DDL_MONTH_END.Items.Add(new ListItem(DateAndTime.MonthName(i,false), i.ToString()));
				}

				DDL_SALES_TYPE.Items.Add(new ListItem("--Select--",""));
				conncc.QueryString="select AGENTYPE_ID, AGENTYPE_DESC from AGENTTYPE";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_SALES_TYPE.Items.Add(new ListItem(conncc.GetFieldValue(i,1), conncc.GetFieldValue(i,0)));
				}

				DDL_LEVEL.Items.Add(new ListItem("--All--",""));
				conncc.QueryString="select LEVEL_CODE, AGENTYPE_DESC, LEVEL_DESC, AGENTYPE_DESC+' '+LEVEL_DESC as LEVELDesc from LEVEL lv "+
					"left join AGENTTYPE agt on lv.AGENTYPE_ID = agt.AGENTYPE_ID "+
					"order by lv.AGENTYPE_ID, LEVEL_CODE";
				conncc.ExecuteQuery();
				for(int j=0; j<conncc.GetRowCount(); j++)
				{
					DDL_LEVEL.Items.Add(new ListItem(conncc.GetFieldValue(j,3),conncc.GetFieldValue(j,0)));
				}

				DDL_WILAYAH_TYPE.Items.Add(new ListItem("--Select--",""));
				DDL_WILAYAH_TYPE.Items.Add(new ListItem("Regional","0"));
				DDL_WILAYAH_TYPE.Items.Add(new ListItem("Nasional","1"));

				DDL_PERIOD_TYPE.Items.Add(new ListItem("--Select--",""));
				DDL_PERIOD_TYPE.Items.Add(new ListItem("Week","0"));
				DDL_PERIOD_TYPE.Items.Add(new ListItem("Month","1"));
				DDL_PERIOD_TYPE.Items.Add(new ListItem("Year","2"));

				DDL_CRITERIA_TYPE.Items.Add(new ListItem("--Select--",""));
				conncc.QueryString="select CRITERIA_ID, CRITERIA_DESC from RFCRITERIA_TYPE";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_CRITERIA_TYPE.Items.Add(new ListItem(conncc.GetFieldValue(i,1), conncc.GetFieldValue(i,0)));
				}

				DDL_CRITERIASUB.Items.Add(new ListItem("1 Criteria","0"));
				DDL_CRITERIASUB.Items.Add(new ListItem("Criteria With Formula","1"));

				ViewExistingAwardPro();
				ViewPendingAwardPro();
			}
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
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
			this.DGR_AWARDPRO_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_AWARDPRO_EXISTING_ItemCommand);
			this.DGR_AWARDPRO_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_AWARDPRO_EXISTING_PageIndexChanged);
			this.DGR_AWARDPRO_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_AWARDPRO_REQUEST_ItemCommand);
			this.DGR_AWARDPRO_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_AWARDPRO_REQUEST_PageIndexChanged);
			this.DGR_SUB_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_SUB_EXISTING_ItemCommand);
			this.DGR_SUB_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_SUB_EXISTING_PageIndexChanged);
			this.DGR_SUBAWARD_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_SUBAWARD_REQUEST_ItemCommand);
			this.DGR_SUBAWARD_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_SUBAWARD_REQUEST_PageIndexChanged);
			this.DGR_AWARD_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_AWARD_EXISTING_ItemCommand);
			this.DGR_AWARD_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_AWARD_EXISTING_PageIndexChanged);
			this.DGR_AWARD_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_AWARD_REQUEST_ItemCommand);
			this.DGR_AWARD_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_AWARD_REQUEST_PageIndexChanged);

		}
		#endregion

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;" || tb.Trim() == "&nbsp")
				tb = "";
			return tb;
		}

		private void ViewExistingAwardPro()
		{
			conncc.QueryString="select  PA_ID, PA_DESC, convert(varchar,PA_STARTDATE,101) PA_STARTDATE, "+
				"convert(varchar,PA_ENDDATE,101) PA_ENDDATE from PROGRAM_AWARD where ACTIVE='1'";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_AWARDPRO_EXISTING.DataSource = dt;
			try
			{ 
				this.DGR_AWARDPRO_EXISTING.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_AWARDPRO_EXISTING.CurrentPageIndex = this.DGR_AWARDPRO_EXISTING.CurrentPageIndex-1;
					this.DGR_AWARDPRO_EXISTING.DataBind();
				}
				catch{}
			}
		}

		private void ViewPendingAwardPro()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_PROGRAM_AWARD";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_AWARDPRO_REQUEST.DataSource = dt;
			try
			{ 
				this.DGR_AWARDPRO_REQUEST.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_AWARDPRO_REQUEST.CurrentPageIndex = this.DGR_AWARDPRO_REQUEST.CurrentPageIndex-1;
					this.DGR_AWARDPRO_REQUEST.DataBind();
				}
				catch{}
			}
		}

		private void BlankEntryAwardPro()
		{
			TXT_AWARD.Text="";
			TXT_DATE_START.Text="";
			DDL_MONTH_START.SelectedValue="";
			TXT_YEAR_START.Text="";
			TXT_DATE_END.Text="";
			DDL_MONTH_END.SelectedValue="";
			TXT_YEAR_END.Text="";
			LBL_JENIS_PA.Text="";
			LBL_PA_ID.Text="";
			LBL_SEQ_ID.Text="";
			LBL_SAVE_MODE1.Text="1";
		}

		private void DGR_AWARDPRO_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_AWARDPRO_EXISTING.CurrentPageIndex=e.NewPageIndex;
			ViewExistingAwardPro();
		}

		private void DGR_AWARDPRO_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_AWARDPRO_REQUEST.CurrentPageIndex=e.NewPageIndex;
			ViewPendingAwardPro();
		}

		private void DGR_AWARDPRO_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE1.Text="0";
					conncc.QueryString="select PA_ID, PA_DESC,convert(varchar,PA_STARTDATE,106) PA_STARTDATE ,DAY(PA_STARTDATE) AS tglStart, "+
						"MONTH(PA_STARTDATE) AS blnStart, YEAR(PA_STARTDATE) AS thnStart, "+
						"convert(varchar,PA_ENDDATE,106) PA_ENDDATE ,DAY(PA_ENDDATE) AS tglEnd,"+
						"MONTH(PA_ENDDATE) AS blnEnd,YEAR(PA_ENDDATE) AS thnEnd "+
						"from Program_Award where PA_ID='"+ e.Item.Cells[1].Text +"'";
					conncc.ExecuteQuery();
					TXT_AWARD.Text=cleansText(conncc.GetFieldValue("PA_DESC"));
					TXT_DATE_START.Text=cleansText(conncc.GetFieldValue("tglStart"));
					DDL_MONTH_START.SelectedValue=cleansText(conncc.GetFieldValue("blnStart"));
					TXT_YEAR_START.Text=cleansText(conncc.GetFieldValue("thnStart"));
					TXT_DATE_END.Text=cleansText(conncc.GetFieldValue("tglEnd"));
					DDL_MONTH_END.SelectedValue=cleansText(conncc.GetFieldValue("blnEnd"));
					TXT_YEAR_END.Text=cleansText(conncc.GetFieldValue("thnEnd"));
					LBL_JENIS_PA.Text="edit";
					LBL_PA_ID.Text=e.Item.Cells[1].Text;
					LBL_SEQ_ID.Text="";
					break;
				case "delete":
					//SMEDEV2
					//LBL_SAVE_MODE1.Text="2";
					//get Seq_id.........
					conncc.QueryString="select isnull(max(SEQ_ID), 0)+1 SEQ_ID from PENDING_SALESCOM_PROGRAM_AWARD";
					conncc.ExecuteQuery();
					string seq_id=conncc.GetFieldValue("SEQ_ID");
					string pa_id=e.Item.Cells[1].Text.Trim();

					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_PROGRAM_AWARD where PA_ID='"+e.Item.Cells[1].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_PROGRAM_AWARD set STATUS='2' where PA_ID='"+pa_id+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_PROGRAM_AWARD_MAKER '2','"+
							e.Item.Cells[1].Text +"','"+ e.Item.Cells[2].Text +"','"+
							e.Item.Cells[3].Text +"','"+ e.Item.Cells[4].Text +"','"+ seq_id +"','1'";
						try
						{
							conncc.ExecuteQuery();
						}
						catch { GlobalTools.popMessage (this,"Error...");}
					}
					BlankEntryAwardPro();
					ViewPendingAwardPro();
					break;
				case "detail":
					//SalesMandiri
					LBL_PA_ID.Text=e.Item.Cells[1].Text;
					ID1=LBL_PA_ID.Text;
					ViewExistingSubAward(ID1);
					ViewPendingSubAward(ID1);
					ID2="";
					ViewExistingAwardCup(ID1, ID2);
					ViewPendingAwardCup(ID1, ID2);
					break;
			}
		}
		
		private void ViewExistingSubAward(string IDsubE)
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PROGRAM_SUBAWARD where PA_ID='"+IDsubE+"'";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_SUB_EXISTING.DataSource = dt;
			try
			{ 
				this.DGR_SUB_EXISTING.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_SUB_EXISTING.CurrentPageIndex = this.DGR_SUB_EXISTING.CurrentPageIndex-1;
					this.DGR_SUB_EXISTING.DataBind();
				}
				catch{}
			}
		}

		private void ViewPendingSubAward(string IDsubP)
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_PROGRAM_SUBAWARD where PA_ID= '"+IDsubP+"'";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_SUBAWARD_REQUEST.DataSource = dt;
			try
			{ 
				this.DGR_SUBAWARD_REQUEST.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_SUBAWARD_REQUEST.CurrentPageIndex = this.DGR_SUBAWARD_REQUEST.CurrentPageIndex-1;
					this.DGR_SUBAWARD_REQUEST.DataBind();
				}
				catch{}
			}
		}

		private void DGR_AWARDPRO_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[8].Text;
					if(status=="2")
					{
						break;
					}
					conncc.QueryString="select PA_ID, SEQ_ID, PA_DESC,convert(varchar,PA_STARTDATE,106) PA_STARTDATE ,DAY(PA_STARTDATE) AS tglStart, "+
						"MONTH(PA_STARTDATE) AS blnStart, YEAR(PA_STARTDATE) AS thnStart, "+
						"convert(varchar,PA_ENDDATE,106) PA_ENDDATE ,DAY(PA_ENDDATE) AS tglEnd,"+
						"MONTH(PA_ENDDATE) AS blnEnd,YEAR(PA_ENDDATE) AS thnEnd "+
						"from PENDING_SALESCOM_PROGRAM_AWARD where PA_ID='"+ e.Item.Cells[1].Text +"' and SEQ_ID='"+ e.Item.Cells[2].Text +"'";
					conncc.ExecuteQuery();
					TXT_AWARD.Text=conncc.GetFieldValue("PA_DESC");
					TXT_DATE_START.Text=conncc.GetFieldValue("tglStart");
					DDL_MONTH_START.SelectedValue=conncc.GetFieldValue("blnStart");
					TXT_YEAR_START.Text=conncc.GetFieldValue("thnStart");
					TXT_DATE_END.Text=conncc.GetFieldValue("tglEnd");
					DDL_MONTH_END.SelectedValue=conncc.GetFieldValue("blnEnd");
					TXT_YEAR_END.Text=conncc.GetFieldValue("thnEnd");
					LBL_JENIS_PA.Text="edit";
					LBL_PA_ID.Text=e.Item.Cells[1].Text;
					LBL_SEQ_ID.Text=e.Item.Cells[2].Text;
					LBL_SAVE_MODE1.Text=e.Item.Cells[8].Text;
					break;
				case "delete":
					//SMEDEV2
					conncc.QueryString="Delete from PENDING_SALESCOM_PROGRAM_AWARD "+
						"where PA_ID='"+ e.Item.Cells[1].Text +"' and "+
						"SEQ_ID='"+ e.Item.Cells[2].Text +"'";
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					BlankEntryAwardPro();
					ViewPendingAwardPro();
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BlankEntryAwardPro();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (!GlobalTools.isDateValid(this,TXT_DATE_START.Text,DDL_MONTH_START.SelectedValue,TXT_YEAR_START.Text) || !GlobalTools.isDateValid(this,TXT_DATE_END.Text,DDL_MONTH_END.SelectedValue,TXT_YEAR_END.Text))
			{
				return;
			}
			if (int.Parse(TXT_YEAR_START.Text)<1900 || int.Parse(TXT_YEAR_END.Text)<1900)
			{
				GlobalTools.popMessage(this,"Invalid Year.");
				return;
			}
			if (LBL_JENIS_PA.Text=="" && LBL_PA_ID.Text=="") //input baru
			{
				//Get Seq_id....
				conncc.QueryString="select isnull(max(SEQ_ID), 0)+1 SEQ_ID from PENDING_SALESCOM_PROGRAM_AWARD";
				conncc.ExecuteQuery();
				string seq_id=conncc.GetFieldValue("SEQ_ID");
				//SalesMandiri
				//get PA_ID
				conncc.QueryString="select isnull(max(PA_ID), 0)+1 PA_ID from PROGRAM_AWARD";
				conncc.ExecuteQuery();
				string PA_ID = kar(5,conncc.GetFieldValue("PA_ID"));
				LBL_PA_ID.Text=PA_ID.ToString().Trim();
								
				conncc.QueryString="select PA_ID from PENDING_SALESCOM_PROGRAM_AWARD ";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					if(LBL_PA_ID.Text.Trim()==conncc.GetFieldValue(i,0))
					{
						int ID = int.Parse(LBL_PA_ID.Text)+1;
						LBL_PA_ID.Text = kar(5,ID.ToString().Trim());
					}
				}
				conncc.QueryString="PARAM_SALESCOM_PROGRAM_AWARD_MAKER '"+LBL_SAVE_MODE1.Text+"','"+
					LBL_PA_ID.Text +"','"+ TXT_AWARD.Text +"','"+
					DDL_MONTH_START.SelectedValue + "/" + TXT_DATE_START.Text + "/" + TXT_YEAR_START.Text + "','"+
					DDL_MONTH_END.SelectedValue + "/" + TXT_DATE_END.Text + "/" + TXT_YEAR_END.Text + "','"+
					seq_id +"','1'";
				conncc.ExecuteQuery();
				BlankEntryAwardPro();
				ViewPendingAwardPro();
			}
			else if (LBL_JENIS_PA.Text=="edit" && LBL_PA_ID.Text!="") //edit
			{
				if (LBL_SEQ_ID.Text!="")//Edit dari DGR_REQUEST
				{
					//SALESMANDIRI
					conncc.QueryString="PARAM_SALESCOM_PROGRAM_AWARD_MAKER '"+LBL_SAVE_MODE1.Text+"','"+
						LBL_PA_ID.Text +"','"+ TXT_AWARD.Text +"','"+
						DDL_MONTH_START.SelectedValue + "/" + TXT_DATE_START.Text + "/" + TXT_YEAR_START.Text + "','"+
						DDL_MONTH_END.SelectedValue + "/" + TXT_DATE_END.Text + "/" + TXT_YEAR_END.Text + "','"+
						LBL_SEQ_ID.Text +"','1'";
					conncc.ExecuteQuery();
					BlankEntryAwardPro();
					ViewPendingAwardPro();
				}
				else if(LBL_SEQ_ID.Text=="")//edit dari Existing
				{
					

					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_PROGRAM_AWARD where PA_ID='"+LBL_PA_ID.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//Get Seq_id....
						conncc.QueryString="select isnull(max(SEQ_ID), 0)+1 SEQ_ID from PENDING_SALESCOM_PROGRAM_AWARD";
						conncc.ExecuteQuery();
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					else
					{
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}

					conncc.QueryString="PARAM_SALESCOM_PROGRAM_AWARD_MAKER '"+LBL_SAVE_MODE1.Text+"','"+
						LBL_PA_ID.Text +"','"+ TXT_AWARD.Text +"','"+
						DDL_MONTH_START.SelectedValue + "/" + TXT_DATE_START.Text + "/" + TXT_YEAR_START.Text + "','"+
						DDL_MONTH_END.SelectedValue + "/" + TXT_DATE_END.Text + "/" + TXT_YEAR_END.Text + "','"+
						LBL_SEQ_ID.Text +"','1'";
					conncc.ExecuteQuery();
					BlankEntryAwardPro();
					ViewPendingAwardPro();
				}
			}
			LBL_SAVE_MODE1.Text="1";
		}

		string kar(int pjg,string str)
		{
			string tmpkar="";
			try
			{
				if (pjg >=str.Length )
				{
					int panjang = pjg -str.Length ;
					string tstr = str.ToString().Trim();
					for (int i=0; i<panjang; i++)
					{
						tstr = "0"+tstr;
					}
					tmpkar=tstr;
				}
				else 
				{
					tmpkar ="Wrong...!";
				}
				return tmpkar;
			}
			catch(Exception e){return "Wrong...!";};
		}

		private void DGR_SUB_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_SUB_EXISTING.CurrentPageIndex=e.NewPageIndex;
			ViewExistingSubAward(LBL_PA_ID.Text);
		}

		private void DGR_SUBAWARD_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_SUBAWARD_REQUEST.CurrentPageIndex=e.NewPageIndex;
			ViewPendingSubAward(LBL_PA_ID.Text);
		}

		private void DGR_SUB_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE2.Text="0";
					conncc.QueryString="select * from PROGRAM_SUBAWARD "+
						"where PA_ID='"+ e.Item.Cells[1].Text +"' and "+
						"PA_SUBID='"+ e.Item.Cells[2].Text +"'";
					conncc.ExecuteQuery();
					TXT_AWARD_SUB.Text=cleansText(conncc.GetFieldValue("PA_SUBDESC"));
					DDL_SALES_TYPE.SelectedValue=cleansText(conncc.GetFieldValue("AGENTYPE_ID"));
					DDL_LEVEL.SelectedValue=cleansText(conncc.GetFieldValue("LEVEL_CODE"));
					DDL_WILAYAH_TYPE.SelectedValue=cleansText(conncc.GetFieldValue("PA_SUBAREATYPE"));
					TXT_PERIOD.Text=cleansText(conncc.GetFieldValue("PA_SUBPERIOD"));
					DDL_PERIOD_TYPE.SelectedValue=cleansText(conncc.GetFieldValue("PA_SUBTYPEPERIOD"));
					TXT_WINNER.Text=cleansText(conncc.GetFieldValue("PA_SUBWINNER"));
					DDL_CRITERIA_TYPE.SelectedValue=cleansText(conncc.GetFieldValue("PA_FORMULATYPE"));
					DDL_CRITERIASUB.SelectedValue=cleansText(conncc.GetFieldValue("PA_SUBCRITERIATYPE"));
					if(DDL_CRITERIASUB.SelectedValue =="0" || DDL_CRITERIASUB.SelectedValue.ToString().Trim() ==" ")
					{
						TXT_FORMULA.Enabled=false;
						TXT_JABOTABEK.Enabled=true;
						TXT_NON_JABOTABEK.Enabled=true;
					}
					else
					{
						TXT_JABOTABEK.Enabled=false;
						TXT_NON_JABOTABEK.Enabled=false;
						TXT_FORMULA.Enabled=true;
					}
					TXT_JABOTABEK.Text=cleansText(conncc.GetFieldValue("PA_MINCRITERIA"));
					TXT_NON_JABOTABEK.Text=cleansText(conncc.GetFieldValue("PA_MINCRITERIA1"));
					TXT_FORMULA.Text=cleansText(conncc.GetFieldValue("PA_FORMULA"));
					TXT_CRITERIA_DESC.Text=cleansText(conncc.GetFieldValue("PA_SUBCRITEDESC"));
					LBL_JENIS_SUB.Text="edit";
					LBL_SUBID.Text=e.Item.Cells[2].Text;
					LBL_SEQID_SUB.Text="";
					break;
				case "delete":
					//SMEDEV2
					//LBL_SAVE_MODE2.Text="2";
					//get Seq_id.........
					conncc.QueryString="select isnull(max(SEQ_ID), 0)+1 SEQ_ID from PENDING_SALESCOM_PROGRAM_SUBAWARD";
					conncc.ExecuteQuery();
					string seq_id=conncc.GetFieldValue("SEQ_ID");
					
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_PROGRAM_SUBAWARD where PA_ID='"+e.Item.Cells[1].Text.Trim()+"' and "+
									 "PA_SUBID='"+e.Item.Cells[2].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_PROGRAM_SUBAWARD set STATUS='2' where PA_ID='"+e.Item.Cells[1].Text.Trim()+"' and "+
							"PA_SUBID='"+e.Item.Cells[2].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_PROGRAM_SUBAWARD_MAKER '2','"+
							seq_id+"','"+ e.Item.Cells[1].Text +"','"+ e.Item.Cells[2].Text +"','"+ e.Item.Cells[3].Text +"','"+
							e.Item.Cells[20].Text +"','"+ e.Item.Cells[21].Text +"','"+
							e.Item.Cells[10].Text +"','"+ e.Item.Cells[11].Text +"','"+ e.Item.Cells[12].Text +"','"+
							e.Item.Cells[13].Text +"','"+ e.Item.Cells[14].Text +"','"+ e.Item.Cells[15].Text +"','"+
							e.Item.Cells[16].Text +"','"+ e.Item.Cells[17].Text +"','"+ e.Item.Cells[18].Text +"','"+
							e.Item.Cells[19].Text +"','1'";
						try
						{
							conncc.ExecuteQuery();
						}
						catch { GlobalTools.popMessage (this,"Error...");}
					}
					BlankEntrySub();
					ViewPendingSubAward(LBL_PA_ID.Text);
					break;
				case "detail":
					//SalesMandiri
					LBL_SUBID.Text=e.Item.Cells[2].Text;
					ID1=LBL_PA_ID.Text;
					ID2=LBL_SUBID.Text;
					ViewExistingAwardCup(ID1,ID2);
					ViewPendingAwardCup(ID1,ID2);
					conncc.QueryString="select isnull(max(SEQ_ID), 0)+1 SEQ_ID from PROGRAM_AWARDCUP "+
						"where PA_ID='"+ ID1 +"' and PA_SUBID='"+ ID2 +"'";
					conncc.ExecuteQuery();
					TXT_POSITION.Text=conncc.GetFieldValue("SEQ_ID");
					break;
			}
		}

		private void DGR_SUBAWARD_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[24].Text;
					if(status=="2")
					{
						break;
					}
					LBL_SAVE_MODE2.Text=e.Item.Cells[24].Text;
					conncc.QueryString="select * from PENDING_SALESCOM_PROGRAM_SUBAWARD "+
						"where PA_ID='"+ e.Item.Cells[1].Text +"' and "+
						"PA_SUBID='"+ e.Item.Cells[2].Text +"' and "+
						"SEQ_ID='"+ e.Item.Cells[3].Text +"'";
					conncc.ExecuteQuery();
					TXT_AWARD_SUB.Text=cleansText(conncc.GetFieldValue("PA_SUBDESC"));
					DDL_SALES_TYPE.SelectedValue=cleansText(conncc.GetFieldValue("AGENTYPE_ID"));
					try
					{
						DDL_LEVEL.SelectedValue = cleansText(conncc.GetFieldValue("LEVEL_CODE"));
					}
					catch (Exception p)
					{
						GlobalTools.popMessage(this,p.Message);
					}
					//DDL_LEVEL.SelectedValue=cleansText(conncc.GetFieldValue("LEVEL_CODE"));
					DDL_WILAYAH_TYPE.SelectedValue=cleansText(conncc.GetFieldValue("PA_SUBAREATYPE"));
					TXT_PERIOD.Text=cleansText(conncc.GetFieldValue("PA_SUBPERIOD"));
					DDL_PERIOD_TYPE.SelectedValue=cleansText(conncc.GetFieldValue("PA_SUBTYPEPERIOD"));
					TXT_WINNER.Text=cleansText(conncc.GetFieldValue("PA_SUBWINNER"));
					DDL_CRITERIA_TYPE.SelectedValue=cleansText(conncc.GetFieldValue("PA_FORMULATYPE"));
					DDL_CRITERIASUB.SelectedValue=cleansText(conncc.GetFieldValue("PA_SUBCRITERIATYPE"));
					if(DDL_CRITERIASUB.SelectedValue =="0" || DDL_CRITERIASUB.SelectedValue.ToString().Trim() ==" ")
					{
						TXT_FORMULA.Enabled=false;
						TXT_JABOTABEK.Enabled=true;
						TXT_NON_JABOTABEK.Enabled=true;
					}
					else
					{
						TXT_JABOTABEK.Enabled=false;
						TXT_NON_JABOTABEK.Enabled=false;
						TXT_FORMULA.Enabled=true;
					}
					TXT_JABOTABEK.Text=cleansText(conncc.GetFieldValue("PA_MINCRITERIA"));
					TXT_NON_JABOTABEK.Text=cleansText(conncc.GetFieldValue("PA_MINCRITERIA1"));
					TXT_FORMULA.Text=cleansText(conncc.GetFieldValue("PA_FORMULA"));
					TXT_CRITERIA_DESC.Text=cleansText(conncc.GetFieldValue("PA_SUBCRITEDESC"));
					LBL_JENIS_SUB.Text="edit";
					LBL_SUBID.Text=e.Item.Cells[2].Text;
					LBL_SEQID_SUB.Text=e.Item.Cells[3].Text;
					break;
				case "delete":
					//SMEDEV2
					conncc.QueryString="Delete from PENDING_SALESCOM_PROGRAM_SUBAWARD "+
						"where PA_ID='"+ e.Item.Cells[1].Text +"' and "+
						"PA_SUBID='"+ e.Item.Cells[2].Text +"' and "+
						"SEQ_ID='"+ e.Item.Cells[3].Text +"'";
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					BlankEntrySub();
					ViewPendingSubAward(LBL_PA_ID.Text);
					break;
			}
		}

		private void BlankEntrySub()
		{
			TXT_AWARD_SUB.Text="";
			DDL_SALES_TYPE.SelectedValue="";
			DDL_LEVEL.SelectedValue="";
			DDL_WILAYAH_TYPE.SelectedValue="";
			TXT_PERIOD.Text="";
			DDL_PERIOD_TYPE.SelectedValue="";
			TXT_WINNER.Text="";
			DDL_CRITERIA_TYPE.SelectedValue="";
			DDL_CRITERIASUB.SelectedValue="0";
			TXT_JABOTABEK.Text="";
			TXT_JABOTABEK.Enabled=true;
			TXT_NON_JABOTABEK.Text="";
			TXT_NON_JABOTABEK.Enabled=true;
			TXT_FORMULA.Text="";
			TXT_FORMULA.Enabled=false;
			TXT_CRITERIA_DESC.Text="";
			LBL_JENIS_SUB.Text="";
			LBL_SUBID.Text="";
			LBL_SEQID_SUB.Text="";
			LBL_SAVE_MODE2.Text="1";
		}

		protected void BTN_CANCEL_SUB_Click(object sender, System.EventArgs e)
		{
			BlankEntrySub();
		}

		protected void DDL_CRITERIASUB_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(DDL_CRITERIASUB.SelectedValue =="0" || DDL_CRITERIASUB.SelectedValue.ToString().Trim() ==" ")
			{
				TXT_FORMULA.Enabled=false;
				TXT_JABOTABEK.Enabled=true;
				TXT_NON_JABOTABEK.Enabled=true;
			}
			else
			{
				TXT_JABOTABEK.Enabled=false;
				TXT_NON_JABOTABEK.Enabled=false;
				TXT_FORMULA.Enabled=true;
			}
		}

		protected void BTN_SAVE_SUB_Click(object sender, System.EventArgs e)
		{
			if(LBL_PA_ID.Text=="")
			{
				GlobalTools.popMessage(this,"Pilih Award Program Dulu..");
				return;
			}
			if (LBL_JENIS_SUB.Text=="" ) //input baru
			{
				//Get Seq_id....
				conncc.QueryString="select isnull(max(SEQ_ID), 0)+1 SEQ_ID from PENDING_SALESCOM_PROGRAM_SUBAWARD";
				conncc.ExecuteQuery();
				string seq_id=conncc.GetFieldValue("SEQ_ID");
				//SalesMandiri
				//get SUB_ID
				conncc.QueryString="select isnull(max(PA_SUBID), 0)+1 PA_SUBID from PROGRAM_SUBAWARD"+
					" where PA_ID='"+ LBL_PA_ID.Text +"'";
				conncc.ExecuteQuery();
				string PA_SUBID = kar(3,conncc.GetFieldValue("PA_SUBID"));
				LBL_SUBID.Text=PA_SUBID.ToString().Trim();
								
				conncc.QueryString="select PA_ID,PA_SUBID from PENDING_SALESCOM_PROGRAM_SUBAWARD ";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					if(LBL_SUBID.Text.Trim()==conncc.GetFieldValue(i,1).Trim() && LBL_PA_ID.Text.Trim()==conncc.GetFieldValue(i,0).Trim())
					{
						int ID = int.Parse(LBL_SUBID.Text)+1;
						LBL_SUBID.Text = kar(3,ID.ToString().Trim());
					}
				}

				conncc.QueryString="PARAM_SALESCOM_PROGRAM_SUBAWARD_MAKER '"+LBL_SAVE_MODE2.Text+"','"+
					seq_id+"','"+ LBL_PA_ID.Text +"','"+ LBL_SUBID.Text +"','"+ TXT_AWARD_SUB.Text +"','"+
					DDL_SALES_TYPE.SelectedValue +"','"+ DDL_LEVEL.SelectedValue +"','"+
					TXT_PERIOD.Text +"','"+ DDL_PERIOD_TYPE.SelectedValue +"','"+ DDL_WILAYAH_TYPE.SelectedValue +"','"+
					TXT_WINNER.Text +"','"+ TXT_FORMULA.Text +"','"+ DDL_CRITERIA_TYPE.SelectedValue +"','"+
					DDL_CRITERIASUB.SelectedValue +"','"+ TXT_JABOTABEK.Text +"','"+ TXT_NON_JABOTABEK.Text +"','"+
					TXT_CRITERIA_DESC.Text +"','1'";
				conncc.ExecuteQuery();
				BlankEntrySub();
				ViewPendingSubAward(LBL_PA_ID.Text);
			}
			else if (LBL_JENIS_SUB.Text=="edit" && LBL_SUBID.Text!="") //edit
			{
				if (LBL_SEQID_SUB.Text!="")//Edit dari DGR_REQUEST
				{
					//SALESMANDIRI
					conncc.QueryString="PARAM_SALESCOM_PROGRAM_SUBAWARD_MAKER '"+LBL_SAVE_MODE2.Text+"','"+
						LBL_SEQID_SUB.Text+"','"+ LBL_PA_ID.Text +"','"+ LBL_SUBID.Text +"','"+ TXT_AWARD_SUB.Text +"','"+
						DDL_SALES_TYPE.SelectedValue +"','"+ DDL_LEVEL.SelectedValue +"','"+
						TXT_PERIOD.Text +"','"+ DDL_PERIOD_TYPE.SelectedValue +"','"+ DDL_WILAYAH_TYPE.SelectedValue +"','"+
						TXT_WINNER.Text +"','"+ TXT_FORMULA +"','"+ DDL_CRITERIA_TYPE.SelectedValue +"','"+
						DDL_CRITERIASUB.SelectedValue +"','"+ TXT_JABOTABEK.Text +"','"+ TXT_NON_JABOTABEK.Text +"','"+
						TXT_CRITERIA_DESC.Text +"','1'";
					conncc.ExecuteQuery();
					BlankEntrySub();
					ViewPendingSubAward(LBL_PA_ID.Text);
				}
				else if(LBL_SEQID_SUB.Text=="")//edit dari Existing
				{
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_PROGRAM_SUBAWARD where "+
									 "PA_ID='"+LBL_PA_ID.Text+"' and PA_SUBID='"+LBL_SUBID.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0) //ga ada di table pending
					{//Get Seq_id....
						conncc.QueryString="select isnull(max(SEQ_ID), 0)+1 SEQ_ID from PENDING_SALESCOM_PROGRAM_SUBAWARD";
						conncc.ExecuteQuery();
						LBL_SEQID_SUB.Text=conncc.GetFieldValue("SEQ_ID");
					}
					else
					{
						LBL_SEQID_SUB.Text=conncc.GetFieldValue("SEQ_ID");
					}
					
					conncc.QueryString="PARAM_SALESCOM_PROGRAM_SUBAWARD_MAKER '"+LBL_SAVE_MODE2.Text+"','"+
						LBL_SEQID_SUB.Text+"','"+ LBL_PA_ID.Text +"','"+ LBL_SUBID.Text +"','"+ TXT_AWARD_SUB.Text +"','"+
						DDL_SALES_TYPE.SelectedValue +"','"+ DDL_LEVEL.SelectedValue +"','"+
						TXT_PERIOD.Text +"','"+ DDL_PERIOD_TYPE.SelectedValue +"','"+ DDL_WILAYAH_TYPE.SelectedValue +"','"+
						TXT_WINNER.Text +"','"+ TXT_FORMULA +"','"+ DDL_CRITERIA_TYPE.SelectedValue +"','"+
						DDL_CRITERIASUB.SelectedValue +"','"+ TXT_JABOTABEK.Text +"','"+ TXT_NON_JABOTABEK.Text +"','"+
						TXT_CRITERIA_DESC.Text +"','1'";
					conncc.ExecuteQuery();
					BlankEntrySub();
					ViewPendingSubAward(LBL_PA_ID.Text);
				}
			}
			LBL_SAVE_MODE2.Text="1";
		}

		private void ViewExistingAwardCup(string IDPA, string IDSub)
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PROGRAM_AWARDCUP where PA_ID='"+ IDPA +"' and "+
				" PA_SUBID ='"+ IDSub +"'";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_AWARD_EXISTING.DataSource = dt;
			try
			{ 
				this.DGR_AWARD_EXISTING.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_AWARD_EXISTING.CurrentPageIndex = this.DGR_AWARD_EXISTING.CurrentPageIndex-1;
					this.DGR_AWARD_EXISTING.DataBind();
				}
				catch{}
			}
		}

		private void ViewPendingAwardCup(string IDPA, string IDSub)
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_PROGRAM_AWARDCUP where "+
				"PA_ID='"+ IDPA +"' and PA_SUBID ='"+ IDSub +"'";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_AWARD_REQUEST.DataSource = dt;
			try
			{ 
				this.DGR_AWARD_REQUEST.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_AWARD_REQUEST.CurrentPageIndex = this.DGR_AWARD_REQUEST.CurrentPageIndex-1;
					this.DGR_AWARD_REQUEST.DataBind();
				}
				catch{}
			}
		}

		private void DGR_AWARD_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_AWARD_EXISTING.CurrentPageIndex=e.NewPageIndex;
			ViewExistingAwardCup(LBL_PA_ID.Text,LBL_SUBID.Text);
		}

		private void DGR_AWARD_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_AWARD_REQUEST.CurrentPageIndex=e.NewPageIndex;
			ViewPendingAwardCup(LBL_PA_ID.Text,LBL_SUBID.Text);
		}

		private void BlankEntryCup()
		{
			TXT_POSITION.Text="";
			TXT_POSITION.Enabled=true;
			TXT_DESC.Text="";
			TXT_WIL_JBTK.Text="";
			TXT_WIL_NONJBTK.Text="";
			TXT_REMARK.Text="";
			LBL_JENIS_CUP.Text="";
			LBL_SEQ_NO.Text="";
			LBL_CUPID.Text="";
			LBL_SAVE_MODE3.Text="1";
		}

		private void DGR_AWARD_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE3.Text="0";
					TXT_POSITION.Text = cleansText(e.Item.Cells[0].Text);
					TXT_POSITION.Enabled=false;
					TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_WIL_JBTK.Text = cleansText(e.Item.Cells[2].Text);
					TXT_WIL_NONJBTK.Text= cleansText(e.Item.Cells[3].Text);
					TXT_REMARK.Text= cleansText(e.Item.Cells[4].Text);
					LBL_JENIS_CUP.Text = "edit";
					LBL_SEQ_NO.Text = "";
					LBL_CUPID.Text = cleansText(e.Item.Cells[0].Text);
					break;
				case "delete":
					//LBL_SAVE_MODE3.Text="2";
					//get seq-no...
					conncc.QueryString="select isnull(max(SEQ_NO), 0)+1 SEQ_NO from PENDING_SALESCOM_PROGRAM_AWARDCUP";
					conncc.ExecuteQuery();
					int seqno=int.Parse(conncc.GetFieldValue("SEQ_NO"));
					
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_PROGRAM_AWARDCUP where PA_ID='"+e.Item.Cells[6].Text.Trim()+"' and "+
						"PA_SUBID='"+e.Item.Cells[7].Text.Trim()+"' and SEQ_ID='"+e.Item.Cells[0].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_PROGRAM_AWARDCUP set STATUS='2' where PA_ID='"+e.Item.Cells[6].Text.Trim()+"' and "+
							"PA_SUBID='"+e.Item.Cells[7].Text.Trim()+"' and SEQ_ID='"+e.Item.Cells[0].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						//SMEDEV2
						conncc.QueryString="PARAM_SALESCOM_PROGRAM_AWARDCUP_MAKER '2','"+
							cleansText(e.Item.Cells[6].Text) +"','"+ cleansText(e.Item.Cells[7].Text) +"','"+
							cleansText(e.Item.Cells[0].Text) +"','"+ cleansText(e.Item.Cells[1].Text) +"','"+
							cleansText(e.Item.Cells[2].Text) +"','"+ cleansText(e.Item.Cells[3].Text) +"','"+
							cleansText(e.Item.Cells[4].Text) +"','"+ seqno +"','1'";
						try
						{
							conncc.ExecuteQuery();
						}
						catch { GlobalTools.popMessage (this,"Error...");}
					}
					ViewPendingAwardCup(LBL_PA_ID.Text,LBL_SUBID.Text);
					BlankEntryCup();
					break;
			}
		}

		private void DGR_AWARD_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[10].Text;
					if(status=="2")
					{
						break;
					}
					LBL_SAVE_MODE3.Text = cleansText(e.Item.Cells[10].Text);
					TXT_POSITION.Text = cleansText(e.Item.Cells[0].Text);
					TXT_POSITION.Enabled=false;
					TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_WIL_JBTK.Text = cleansText(e.Item.Cells[2].Text);
					TXT_WIL_NONJBTK.Text= cleansText(e.Item.Cells[3].Text);
					TXT_REMARK.Text= cleansText(e.Item.Cells[4].Text);
					LBL_JENIS_CUP.Text = "edit";
					LBL_SEQ_NO.Text = cleansText(e.Item.Cells[9].Text);
					LBL_CUPID.Text = cleansText(e.Item.Cells[0].Text);
					break;
				case "delete":
					conncc.QueryString="delete from PENDING_SALESCOM_PROGRAM_AWARDCUP "+
						"where PA_ID='"+cleansText(e.Item.Cells[7].Text)+"' and "+
						"PA_SUBID='"+cleansText(e.Item.Cells[8].Text)+"' and "+
						"SEQ_ID='"+cleansText(e.Item.Cells[0].Text)+"' and "+
						"SEQ_NO='"+cleansText(e.Item.Cells[9].Text)+"'";
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error in Delete...");}
					ViewPendingAwardCup(LBL_PA_ID.Text,LBL_SUBID.Text);
					BlankEntryCup();
					break;
			}
		}

		protected void BTN_CANCEL_AWARD_Click(object sender, System.EventArgs e)
		{
			BlankEntryCup();
		}

		protected void BTN_SAVE_AWARD_Click(object sender, System.EventArgs e)
		{
			if(LBL_PA_ID.Text=="" && LBL_SUBID.Text=="")
			{
				GlobalTools.popMessage(this,"Pilih Award Program & Sub Award Dulu..");
				return;
			}
			if (LBL_JENIS_CUP.Text=="" && LBL_CUPID.Text=="") //input baru
			{
				//get SEQ_NO....
				conncc.QueryString="select isnull(max(SEQ_NO), 0)+1 SEQ_NO from PENDING_SALESCOM_PROGRAM_AWARDCUP";
				conncc.ExecuteQuery();
				string SEQ_NO = conncc.GetFieldValue("SEQ_NO");
				LBL_SEQ_NO.Text=SEQ_NO.ToString().Trim();
				
				//cek SEQ_ID di tabel Existing
				conncc.QueryString="select * from PROGRAM_AWARDCUP where PA_ID='"+ LBL_PA_ID.Text +"' and "+
					"PA_SUBID='"+LBL_SUBID.Text+"'and SEQ_ID='"+TXT_POSITION.Text+"'";
				conncc.ExecuteQuery();
				if(conncc.GetRowCount()!=0)
				{
					int ID = int.Parse(TXT_POSITION.Text)+1;
					LBL_CUPID.Text = ID.ToString().Trim();
				}
				else
				{
					LBL_CUPID.Text=TXT_POSITION.Text;
				}

				//cek SEQ_ID di tabel Pending
				conncc.QueryString="select SEQ_ID from PENDING_SALESCOM_PROGRAM_AWARDCUP";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					if(LBL_CUPID.Text.Trim()==conncc.GetFieldValue(i,0).Trim())
					{
						int CUPID = int.Parse(LBL_CUPID.Text)+1;
						LBL_CUPID.Text = CUPID.ToString().Trim();
					}
				}

				conncc.QueryString="PARAM_SALESCOM_PROGRAM_AWARDCUP_MAKER '"+LBL_SAVE_MODE3.Text+"','"+
					LBL_PA_ID.Text +"','"+ LBL_SUBID.Text +"','"+
					LBL_CUPID.Text +"','"+ TXT_DESC.Text +"','"+
					TXT_WIL_JBTK.Text +"','"+ TXT_WIL_NONJBTK.Text +"','"+
					TXT_REMARK.Text +"','"+ LBL_SEQ_NO.Text +"','1'";
				conncc.ExecuteQuery();
				BlankEntryCup();
				ViewPendingAwardCup(LBL_PA_ID.Text,LBL_SUBID.Text);
			}
			if (LBL_JENIS_CUP.Text=="edit" && LBL_SUBID.Text!="") //edit
			{
				if (LBL_SEQ_NO.Text!="")//Edit dari DGR_REQUEST
				{
					//SALESMANDIRI
					conncc.QueryString="PARAM_SALESCOM_PROGRAM_AWARDCUP_MAKER '"+LBL_SAVE_MODE3.Text+"','"+
						LBL_PA_ID.Text +"','"+ LBL_SUBID.Text +"','"+
						LBL_CUPID.Text +"','"+ TXT_DESC.Text +"','"+
						TXT_WIL_JBTK.Text +"','"+ TXT_WIL_NONJBTK.Text +"','"+
						TXT_REMARK.Text +"','"+ LBL_SEQ_NO.Text +"','1'";
					try
					{
						conncc.ExecuteQuery();
					}
					catch (Exception p)
					{
						GlobalTools.popMessage(this,p.Message);
					}
					BlankEntryCup();
					ViewPendingAwardCup(LBL_PA_ID.Text,LBL_SUBID.Text);
				}
				else if(LBL_SEQ_NO.Text=="")//edit dari Existing
				{
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_PROGRAM_AWARDCUP where PA_ID='"+ LBL_PA_ID.Text +"' and "+
									 "PA_SUBID='"+LBL_SUBID.Text+"'and SEQ_ID='"+TXT_POSITION.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//Get Seq_no....
						conncc.QueryString="select isnull(max(SEQ_NO), 0)+1 SEQ_NO from PENDING_SALESCOM_PROGRAM_AWARDCUP";
						conncc.ExecuteQuery();
						LBL_SEQ_NO.Text = conncc.GetFieldValue("SEQ_NO");
					}
					else
					{
						LBL_SEQ_NO.Text=conncc.GetFieldValue("SEQ_NO");
					}
					
					conncc.QueryString="PARAM_SALESCOM_PROGRAM_AWARDCUP_MAKER '"+LBL_SAVE_MODE3.Text+"','"+
						LBL_PA_ID.Text +"','"+ LBL_SUBID.Text +"','"+
						LBL_CUPID.Text +"','"+ TXT_DESC.Text +"','"+
						TXT_WIL_JBTK.Text +"','"+ TXT_WIL_NONJBTK.Text +"','"+
						TXT_REMARK.Text +"','"+ LBL_SEQ_NO.Text +"','1'";
					try
					{
						conncc.ExecuteQuery();
					}
					catch (Exception p)
					{
						GlobalTools.popMessage(this,p.Message);
					}

					BlankEntryCup();
					ViewPendingAwardCup(LBL_PA_ID.Text,LBL_SUBID.Text);
				}
			}
			LBL_SAVE_MODE3.Text="1";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../SalesParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

	}
}

