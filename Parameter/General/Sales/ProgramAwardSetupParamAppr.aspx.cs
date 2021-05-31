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
	/// Summary description for ProgramAwardSetupParamAppr.
	/// </summary>
	public partial class ProgramAwardSetupParamAppr : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=SALESMANDIRI;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				ViewPendingAwardPro();
				ViewPendingSubAward();
				ViewPendingAwardCup();
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
			this.DGR_APPR1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR1_ItemCommand);
			this.DGR_APPR1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPR1_PageIndexChanged);
			this.DGR_APPR2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR2_ItemCommand);
			this.DGR_APPR2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPR2_PageIndexChanged);

		}
		#endregion

		private void ViewPendingAwardPro()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_PROGRAM_AWARD";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_APPR.DataSource = dt;
			try
			{ 
				this.DGR_APPR.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_APPR.CurrentPageIndex = this.DGR_APPR.CurrentPageIndex-1;
					this.DGR_APPR.DataBind();
				}
				catch{}
			}
		}

		private void DGR_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR.CurrentPageIndex=e.NewPageIndex;
			ViewPendingAwardPro();
		}

		private void DGR_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Pending");
							rbA.Checked = false;
							rbB.Checked = false;
							rbC.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < this.DGR_APPR.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
						rbR = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject");
					if (rbA.Checked)
						performRequestList(i,1);
					else if (rbR.Checked)
						performRequestList(i,0);
				} 
				catch (Exception p)
				{
					GlobalTools.popMessage(this,p.Message);
				}

			}
			ViewPendingAwardPro();
		}

		private void performRequestList(int row, int appr_sta)
		{
			try 
			{	
				string userid = Session["UserID"].ToString();
				string groupid = Session["GroupID"].ToString();
				string PA_ID = DGR_APPR.Items[row].Cells[1].Text.Trim();
				string SEQ_ID = DGR_APPR.Items[row].Cells[0].Text.Trim();
				//SMEDEV2
				conncc.QueryString="select * from PENDING_SALESCOM_PROGRAM_AWARD where PA_ID='"+PA_ID+"'and "+
					"SEQ_ID='"+SEQ_ID+"'";
				conncc.ExecuteQuery();
				string status=conncc.GetFieldValue("STATUS");
				string PA_DESC = conncc.GetFieldValue("PA_DESC");
				string tgl1 = GlobalTools.FormatDate_Day(conncc.GetFieldValue("PA_STARTDATE"));
				string bln1 = GlobalTools.FormatDate_Month(conncc.GetFieldValue("PA_STARTDATE"));
				string thn1 = GlobalTools.FormatDate_Year(conncc.GetFieldValue("PA_STARTDATE"));
				string PA_STARTDATE = GlobalTools.ToSQLDate(tgl1,bln1,thn1);
				string tgl2 = GlobalTools.FormatDate_Day(conncc.GetFieldValue("PA_ENDDATE"));
				string bln2 = GlobalTools.FormatDate_Month(conncc.GetFieldValue("PA_ENDDATE"));
				string thn2 = GlobalTools.FormatDate_Year(conncc.GetFieldValue("PA_ENDDATE"));
				string PA_ENDDATE = GlobalTools.ToSQLDate(tgl2,bln2,thn2);
				string ACTIVE = conncc.GetFieldValue("ACTIVE");

				//Salesmandiri
				conncc.QueryString = "PARAM_SALESCOM_PROGRAM_AWARD_APPR '" + 
					SEQ_ID + "','"+ appr_sta + "' , 'SMEDEV2','"+ status +"','"+
					PA_ID +"','"+ PA_DESC +"',"+ PA_STARTDATE +","+ PA_ENDDATE +",'"+ ACTIVE +"','"+
					userid +"','"+ groupid +"'";
				conncc.ExecuteQuery();
			}
			catch{}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../SalesParamApproval.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		private void ViewPendingSubAward()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_PROGRAM_SUBAWARD";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_APPR1.DataSource = dt;
			try
			{ 
				this.DGR_APPR1.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_APPR1.CurrentPageIndex = this.DGR_APPR1.CurrentPageIndex-1;
					this.DGR_APPR1.DataBind();
				}
				catch{}
			}
		}

		private void DGR_APPR1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR1.CurrentPageIndex=e.NewPageIndex;
			ViewPendingSubAward();
		}

		private void DGR_APPR1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR1.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR1.Items[i].FindControl("rd_Approve1"),
								rbB = (RadioButton) DGR_APPR1.Items[i].FindControl("rd_Reject1"),
								rbC = (RadioButton) DGR_APPR1.Items[i].FindControl("rd_Pending1");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_APPR1.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR1.Items[i].FindControl("rd_Approve1"),
								rbB = (RadioButton) DGR_APPR1.Items[i].FindControl("rd_Reject1"),
								rbC = (RadioButton) DGR_APPR1.Items[i].FindControl("rd_Pending1");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR1.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR1.Items[i].FindControl("rd_Approve1"),
								rbB = (RadioButton) DGR_APPR1.Items[i].FindControl("rd_Reject1"),
								rbC = (RadioButton) DGR_APPR1.Items[i].FindControl("rd_Pending1");
							rbA.Checked = false;
							rbB.Checked = false;
							rbC.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_SUBMIT1_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < this.DGR_APPR1.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_APPR1.Items[i].FindControl("rd_Approve1"),
						rbR = (RadioButton) DGR_APPR1.Items[i].FindControl("rd_Reject1");
					if (rbA.Checked)
						performRequestList1(i,1);
					else if (rbR.Checked)
						performRequestList1(i,0);
				} 
				catch (Exception p)
				{
					GlobalTools.popMessage(this,p.Message);
				}

			}
			ViewPendingSubAward();
		}

		private void performRequestList1(int row, int appr_sta)
		{
			try 
			{	
				string userid = Session["UserID"].ToString();
				string groupid = Session["GroupID"].ToString();
				string PA_ID = DGR_APPR1.Items[row].Cells[0].Text.Trim();
				string PA_SUBID = DGR_APPR1.Items[row].Cells[1].Text.Trim();
				string SEQ_ID = DGR_APPR1.Items[row].Cells[2].Text.Trim();
				//SMEDEV2
				conncc.QueryString="select * from PENDING_SALESCOM_PROGRAM_SUBAWARD where PA_ID='"+PA_ID+"'and "+
					"SEQ_ID='"+SEQ_ID+"' and PA_SUBID='"+PA_SUBID+"'";
				conncc.ExecuteQuery();
				string status =conncc.GetFieldValue("STATUS");
				string PA_SUBDESC =conncc.GetFieldValue("PA_SUBDESC");
				string AGENTYPE_ID =conncc.GetFieldValue("AGENTYPE_ID");
				string LEVEL_CODE =conncc.GetFieldValue("LEVEL_CODE");
				string PA_SUBPERIOD =conncc.GetFieldValue("PA_SUBPERIOD");
				string PA_SUBTYPEPERIOD =conncc.GetFieldValue("PA_SUBTYPEPERIOD");
				string PA_SUBAREATYPE =conncc.GetFieldValue("PA_SUBAREATYPE");
				string PA_SUBWINNER =conncc.GetFieldValue("PA_SUBWINNER");
				string PA_FORMULA =conncc.GetFieldValue("PA_FORMULA");
				string PA_FORMULATYPE =conncc.GetFieldValue("PA_FORMULATYPE");
				string PA_SUBCRITERIATYPE =conncc.GetFieldValue("PA_SUBCRITERIATYPE");
				string PA_MINCRITERIA =conncc.GetFieldValue("PA_MINCRITERIA");
				string PA_MINCRITERIA1 =conncc.GetFieldValue("PA_MINCRITERIA1");
				string PA_SUBCRITEDESC =conncc.GetFieldValue("PA_SUBCRITEDESC");
				string ACTIVE = conncc.GetFieldValue("ACTIVE");

				//Salesmandiri
				conncc.QueryString = "PARAM_SALESCOM_PROGRAM_SUBAWARD_APPR '" + 
					SEQ_ID+"','"+ appr_sta +"','SMEDEV2','"+ status +"','"+
					PA_ID +"','"+ PA_SUBID +"','"+
					PA_SUBDESC +"','"+ AGENTYPE_ID +"','"+ LEVEL_CODE +"','"+
					PA_SUBPERIOD +"','"+ PA_SUBTYPEPERIOD +"','"+ PA_SUBAREATYPE +"','"+
					PA_SUBWINNER +"','"+ PA_FORMULA +"','"+ PA_FORMULATYPE +"','"+
					PA_SUBCRITERIATYPE +"','"+ PA_MINCRITERIA +"','"+ PA_MINCRITERIA1 +"','"+
					PA_SUBCRITEDESC +"','1','"+ userid +"','"+ groupid +"'";
				conncc.ExecuteQuery();
			}
			catch{}
		}

		private void ViewPendingAwardCup()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_PROGRAM_AWARDCUP ";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_APPR2.DataSource = dt;
			try
			{ 
				this.DGR_APPR2.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_APPR2.CurrentPageIndex = this.DGR_APPR2.CurrentPageIndex-1;
					this.DGR_APPR2.DataBind();
				}
				catch{}
			}
		}

		private void DGR_APPR2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR2.CurrentPageIndex=e.NewPageIndex;
			ViewPendingAwardCup();
		}

		private void DGR_APPR2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR2.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR2.Items[i].FindControl("rd_Approve2"),
								rbB = (RadioButton) DGR_APPR2.Items[i].FindControl("rd_Reject2"),
								rbC = (RadioButton) DGR_APPR2.Items[i].FindControl("rd_Pending2");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_APPR2.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR2.Items[i].FindControl("rd_Approve2"),
								rbB = (RadioButton) DGR_APPR2.Items[i].FindControl("rd_Reject2"),
								rbC = (RadioButton) DGR_APPR2.Items[i].FindControl("rd_Pending2");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR1.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR2.Items[i].FindControl("rd_Approve2"),
								rbB = (RadioButton) DGR_APPR2.Items[i].FindControl("rd_Reject2"),
								rbC = (RadioButton) DGR_APPR2.Items[i].FindControl("rd_Pending2");
							rbA.Checked = false;
							rbB.Checked = false;
							rbC.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_SUBMIT2_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < this.DGR_APPR2.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_APPR2.Items[i].FindControl("rd_Approve2"),
						rbR = (RadioButton) DGR_APPR2.Items[i].FindControl("rd_Reject2");
					if (rbA.Checked)
						performRequestList2(i,1);
					else if (rbR.Checked)
						performRequestList2(i,0);
				} 
				catch (Exception p)
				{
					GlobalTools.popMessage(this,p.Message);
				}

			}
			ViewPendingAwardCup();
		}

		private void performRequestList2(int row, int appr_sta)
		{
			try 
			{	
				string userid = Session["UserID"].ToString();
				string groupid = Session["GroupID"].ToString();
				int SEQ_NO = int.Parse(DGR_APPR2.Items[row].Cells[0].Text.Trim());
				string PA_ID = DGR_APPR2.Items[row].Cells[1].Text.Trim();
				string PA_SUBID = DGR_APPR2.Items[row].Cells[2].Text.Trim();
				string SEQ_ID = DGR_APPR2.Items[row].Cells[3].Text.Trim();
				//SMEDEV2
				conncc.QueryString="select * from PENDING_SALESCOM_PROGRAM_AWARDCUP where PA_ID='"+PA_ID+"'and "+
					"PA_SUBID='"+PA_SUBID+"' and SEQ_ID='"+SEQ_ID+"' and SEQ_NO='"+SEQ_NO+"'";
				conncc.ExecuteQuery();
				string status=conncc.GetFieldValue("STATUS");
				string DESCRIPTION = conncc.GetFieldValue("DESCRIPTION");
				string AWARD_VALUE = conncc.GetFieldValue("AWARD_VALUE");
				string AWARD_VALUE1 = conncc.GetFieldValue("AWARD_VALUE1");
				string AWARD_MEMO = conncc.GetFieldValue("AWARD_MEMO");
				string ACTIVE = conncc.GetFieldValue("ACTIVE");

				//Salesmandiri
				conncc.QueryString = "PARAM_SALESCOM_PROGRAM_AWARDCUP_APPR '" + 
					SEQ_ID + "','"+ appr_sta + "' , 'SMEDEV2','"+ status +"','"+
					PA_ID +"','"+ PA_SUBID +"','"+ DESCRIPTION +"','"+ AWARD_VALUE +"','"+ AWARD_VALUE1 +"','"+
					AWARD_MEMO +"','"+ SEQ_NO +"','"+ ACTIVE +"','"+
					userid +"','"+ groupid +"'";
				conncc.ExecuteQuery();
			}
			catch{}
		}

	}
}

