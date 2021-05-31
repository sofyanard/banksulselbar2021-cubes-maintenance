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

namespace CuBES_Maintenance.Parameter.General.CC
{
	/// <summary>
	/// Summary description for ServiceProgramParamAppr.
	/// </summary>
	public partial class ServiceProgramParamAppr : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=LOSCC2;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		string seq_no,seq_id,DB_NAMA;
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
			conncc.QueryString="select * from VW_PARAM_CC_PENDING_CCTYPE ";
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
					this.DGR_APPR.CurrentPageIndex = DGR_APPR.CurrentPageIndex-1;
					this.DGR_APPR.DataBind();
				}
				catch{}
			}
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

		private void DGR_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
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
		
		//private void performRequestList(int row, char appr_sta)
		private void performRequestList(int row, int appr_sta)
		{
			try 
			{	
				seq_no = DGR_APPR.Items[row].Cells[0].Text.Trim();
				seq_id = DGR_APPR.Items[row].Cells[15].Text.Trim();
				string userid = Session["UserID"].ToString();
				string groupid = Session["GroupID"].ToString();
				//SMEDEV2
				conncc.QueryString="select * from PENDING_CC_CCTYPE where seq_id='"+seq_id+"'and seq='"+seq_no+"'";
				conncc.ExecuteQuery();
				string status=conncc.GetFieldValue("STATUS");
				string TYPE_DESC =conncc.GetFieldValue("TYPE_DESC");
				string DEFAULT_LIMIT =conncc.GetFieldValue("DEFAULT_LIMIT");
				string SICS_ID =conncc.GetFieldValue("SICS_ID");
				string MAX_LIMIT =conncc.GetFieldValue("MAX_LIMIT");
				string TYPE_CODE =conncc.GetFieldValue("TYPE_CODE");
				string ORG =conncc.GetFieldValue("ORG");
				string TYPE_EMBOSS =conncc.GetFieldValue("TYPE_EMBOSS");
				string CCNUM =conncc.GetFieldValue("CCNUM");
				string CCIURANYY =conncc.GetFieldValue("CCIURANYY");
				string CCIURANYYSP =conncc.GetFieldValue("CCIURANYYSP");
				string REMARK =conncc.GetFieldValue("REMARK");
				string EXPIRE_DATE =conncc.GetFieldValue("EXPIRE_DATE");
				string PRODUCTID =conncc.GetFieldValue("PRODUCTID");
				string ACTIVE =conncc.GetFieldValue("ACTIVE");
				//LOSCC2
				conncc.QueryString = "PARAM_CC_SERVICE_PROGRAM_APPR '" + 
					seq_no + "' , '"+ seq_id + "' , '" + appr_sta +"','"+DB_NAMA+"','"+ status +"','"+
					TYPE_DESC +"','"+ DEFAULT_LIMIT +"','"+ SICS_ID +"','"+ MAX_LIMIT +"','"+
					TYPE_CODE +"','"+ ORG +"','"+ TYPE_EMBOSS +"','"+ CCNUM +"','"+
					CCIURANYY +"','"+ CCIURANYYSP +"','"+ REMARK +"','"+ EXPIRE_DATE +"','"+
					PRODUCTID +"','"+ ACTIVE +"','"+ userid +"','"+ groupid +"'";
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
			Response.Redirect("../../HostParam.aspx?mc=99020101");
		}

	
	}
}
