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
	/// Summary description for FormulaSetupParamAppr.
	/// </summary>
	public partial class FormulaSetupParamAppr : System.Web.UI.Page
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
				ViewPendingData();
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
			this.DGR_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR_ItemCommand);
			this.DGR_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPR_PageIndexChanged);

		}
		#endregion

		private void ViewPendingData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_CALCULATION_FORMULA order by SEQ_NO";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_APPR.DataSource = dt;
			this.DGR_APPR.DataBind();
			for (int i = 0; i < this.DGR_APPR.Items.Count; i++)
			{
				this.DGR_APPR.Items[i].Cells[1].Text = (i+1).ToString();
			}
		}

		private void DGR_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR.CurrentPageIndex=e.NewPageIndex;
			ViewPendingData();
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
			ViewPendingData();
		}

		private void performRequestList(int row, int appr_sta)
		{
			try 
			{	
				string userid = Session["UserID"].ToString();
				string groupid = Session["GroupID"].ToString();
				int seq_no=Convert.ToInt16(DGR_APPR.Items[row].Cells[10].Text.Trim());
				int seq_id = Convert.ToInt16(DGR_APPR.Items[row].Cells[0].Text.Trim());
				//SMEDEV2
				conncc.QueryString="select * from PENDING_SALESCOM_CALCULATION_FORMULA "+
					"where SEQ_NO='"+seq_no+"' and SEQ_ID='"+ seq_id +"'";
				conncc.ExecuteQuery();
				string status=conncc.GetFieldValue("STATUS");
				string CALCULATION_ID =conncc.GetFieldValue("CALCULATION_ID");
				string AGENTYPE_ID =conncc.GetFieldValue("AGENTYPE_ID");
				string FORMULA_NAME =conncc.GetFieldValue("FORMULA_NAME");
				string FORMULA =conncc.GetFieldValue("FORMULA");
				FORMULA = FORMULA.Replace("'","''");
				string FORMULA_TABLE =conncc.GetFieldValue("FORMULA_TABLE");
				string FORMULA_LINK =conncc.GetFieldValue("FORMULA_LINK");
				string FORMULA_ACTIVE =conncc.GetFieldValue("FORMULA_ACTIVE");
				string RESULT_TYPE =conncc.GetFieldValue("RESULT_TYPE");
				string FORMULA_GROUP =conncc.GetFieldValue("FORMULA_GROUP");
						
				//Salesmandiri
				conncc.QueryString = "PARAM_SALESCOM_CALCULATION_FORMULA_APPR '" + 
					appr_sta + "' , 'SMEDEV2','"+ status +"','"+
					seq_no +"','"+ CALCULATION_ID +"','"+ 
					AGENTYPE_ID +"','"+ FORMULA_NAME +"','"+ FORMULA +"','"+
					FORMULA_TABLE +"','"+ FORMULA_LINK +"','"+ FORMULA_ACTIVE +"','"+
					RESULT_TYPE +"','"+ FORMULA_GROUP +"','"+ seq_id +"','"+
					userid +"','"+ groupid +"'";
				conncc.ExecuteQuery();
			}
			catch{}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../SalesParamApproval.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

	}
}
