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
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.CC
{
	/// <summary>
	/// Summary description for ProductListParamAppr.
	/// </summary>
	public partial class ProductListParamAppr : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=LOSCC2-DEV;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		string seq_id,prod_id,DB_NAMA ;
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				ViewPendingData();
			}
			DGR_APPR.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGR_APPR_PageIndexChanged);
		}

		private void SetDBConn2()
		{
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			DB_NAMA = conn.GetFieldValue("DB_NAMA");
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
			//SMEDEV2
			conncc.QueryString="select * from VW_PARAM_CC_PENDING_TPRODUCT";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			DGR_APPR.DataSource = dt;
			try
			{
				this.DGR_APPR.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_APPR.CurrentPageIndex = DGR_APPR.CurrentPageIndex - 1;
					this.DGR_APPR.DataBind();
				}
				catch{}
			}
		}

		private void DGR_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR.CurrentPageIndex = e.NewPageIndex;
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
				catch {}
			}
			ViewPendingData();
		}

		private void performRequestList(int row, int appr_sta)
		{
			try 
			{	
				string userid = Session["UserID"].ToString();
				string groupid = Session["GroupID"].ToString();
				seq_id = DGR_APPR.Items[row].Cells[31].Text.Trim();
				prod_id = DGR_APPR.Items[row].Cells[0].Text.Trim();
				//SMEDEV2
				conncc.QueryString="select * from VW_PARAM_CC_PENDING_TPRODUCT where seq_id='"+seq_id+"'and productid='"+prod_id+"'";
				conncc.ExecuteQuery();
				string status=conncc.GetFieldValue("STATUS");
				string PRODUCTNAME =conncc.GetFieldValue("PRODUCTNAME");
				string DUMMIES =conncc.GetFieldValue("DUMMIES");
				string PR_TYPE =conncc.GetFieldValue("PR_TYPE");
				string PR_CCY =conncc.GetFieldValue("PR_CCY");
				string PR_TENOR =conncc.GetFieldValue("PR_TENOR");
				string PR_AMOUNT =conncc.GetFieldValue("PR_AMOUNT");
				//string NA_CODE =conncc.GetFieldValue("NA_CODE");
				string PR_PROVISI =conncc.GetFieldValue("PR_PROVISI");
				string PR_RATEYY =conncc.GetFieldValue("PR_RATEYY");
				string PR_RATEMM =conncc.GetFieldValue("PR_RATEMM");
				string PR_CWTAX1 =conncc.GetFieldValue("PR_CWTAX1");
				string PR_CWTAX2 =conncc.GetFieldValue("PR_CWTAX2");
				string PR_CWTAX3 =conncc.GetFieldValue("PR_CWTAX3");
				string PR_CWTAX4 =conncc.GetFieldValue("PR_CWTAX4");
				string PR_CWTAXIDR1 =conncc.GetFieldValue("PR_CWTAXIDR1");
				string PR_CWTAXIDR2 =conncc.GetFieldValue("PR_CWTAXIDR2");
				string PMIN =conncc.GetFieldValue("PMIN");
				string PMAX =conncc.GetFieldValue("PMAX");
				string PR_CWCALLIMIT =conncc.GetFieldValue("PR_CWCALLIMIT");
				string PR_IURANCC =conncc.GetFieldValue("PR_IURANCC");
				string PR_IURANCCSP =conncc.GetFieldValue("PR_IURANCCSP");
				string FLOOR_LIMIT =conncc.GetFieldValue("FLOOR_LIMIT");
				string CEILING_LIMIT =conncc.GetFieldValue("CEILING_LIMIT");
				string CARD_TYPE =conncc.GetFieldValue("CARD_TYPE");
				string CD_SIBS =conncc.GetFieldValue("CD_SIBS");
				string PR_FLAG =conncc.GetFieldValue("PR_FLAG");
				string NETWORK_ID =conncc.GetFieldValue("NETWORK_ID");
				string CLASSIC_TYPE =conncc.GetFieldValue("CLASSIC_TYPE");
				//LOSCC2-DEV
				conncc.QueryString = "PARAM_CC_PRODUCTLIST_APPR '" + 
					seq_id + "' , '"+ appr_sta + "' , '"+DB_NAMA+"','"+ status +"','"+
					prod_id +"','"+ PRODUCTNAME +"','"+ DUMMIES +"','"+ PR_TYPE +"','"+
					PR_CCY +"','"+ PR_TENOR +"','"+ PR_AMOUNT +"','"+ PR_PROVISI +"','"+ //'"+ NA_CODE +"',
					PR_RATEYY +"','"+ PR_RATEMM +"','"+ PR_CWTAX1 +"','"+ PR_CWTAX2 +"','"+ PR_CWTAX3 +"','"+
					PR_CWTAX4 +"','"+ PR_CWTAXIDR1 +"','"+ PR_CWTAXIDR2 +"','"+ PMIN +"','"+ PMAX +"','"+
					PR_CWCALLIMIT +"','"+ PR_IURANCC +"','"+ PR_IURANCCSP +"','"+ FLOOR_LIMIT +"','"+
					CEILING_LIMIT +"','"+ CARD_TYPE +"','"+ CD_SIBS +"','"+ PR_FLAG +"','1','"+
					userid +"','"+ groupid +"', '"+NETWORK_ID+"', '"+CLASSIC_TYPE+"'";
				conncc.ExecuteQuery();
			}
			catch (Exception p)
			{
				GlobalTools.popMessage(this,p.Message);
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
			Response.Redirect("../../HostParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

	}
}
