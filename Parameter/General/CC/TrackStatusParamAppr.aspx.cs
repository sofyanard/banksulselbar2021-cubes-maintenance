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
	/// Summary description for TrackStatusParamAppr.
	/// </summary>
	public partial class TrackStatusParamAppr : System.Web.UI.Page
	{
		protected Connection connsme = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		protected Connection conn2,conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				ViewPendingData();
			}
			this.DGR_APPR.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_APPR_PageIndexChanged);
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

		private void SetDBConn2()
		{
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			string DB_NAMA = conn.GetFieldValue("DB_NAMA");
			string DB_IP = conn.GetFieldValue("DB_IP");
			string DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conn2 = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private string getPendingStatus(string saveMode) 
		{
			string status = "";			
			switch (saveMode)
			{
				case "0":
					status = "Update";
					break;
				case "1":
					status = "Insert";
					break;
				case "2":
					status = "Delete";
					break;
				default:
					status = "";
					break;
			}
			return status;
		}

		private string YesNoStatus(string saveMode) 
		{
			string status = "";			
			switch (saveMode)
			{
				case "0":
					status = "No";
					break;
				case "1":
					status = "Yes";
					break;
				default:
					status = "";
					break;
			}
			return status;
		}

		private void ViewPendingData()
		{
			conn2.QueryString = "select * from PENDING_CC_RFTRACKLST";
			conn2.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			this.DGR_APPR.DataSource = dt;
			try
			{
				DGR_APPR.DataBind();
			}
			catch
			{
				DGR_APPR.CurrentPageIndex = DGR_APPR.PageCount - 1;
				DGR_APPR.DataBind();
			}
			for (int i=0; i< this.DGR_APPR.Items.Count; i++)
			{
				this.DGR_APPR.Items[i].Cells[8].Text = this.getPendingStatus(this.DGR_APPR.Items[i].Cells[8].Text.Trim());
				this.DGR_APPR.Items[i].Cells[7].Text = this.YesNoStatus(this.DGR_APPR.Items[i].Cells[7].Text.Trim());
				this.DGR_APPR.Items[i].Cells[6].Text = this.YesNoStatus(this.DGR_APPR.Items[i].Cells[6].Text.Trim());
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
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Pending");
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
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Pending");
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
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Pending");
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		private void performRequestList(int row, char appr_sta)
		{
			string paramid	= "107";
			connsme.QueryString = "select PARAMNAME,PG_ID from RFPARAMETERALL where paramid='" + paramid + "' and moduleid='20' and classid='' and ismaker=0";
			connsme.ExecuteQuery();
			string paramname	= connsme.GetFieldValue("PARAMNAME");
			string paramclass	= connsme.GetFieldValue("PG_ID");
			connsme.ClearData();

			string userid = Session["UserID"].ToString();
			string tr_code= this.DGR_APPR.Items[row].Cells[0].Text.Trim();
			conn2.QueryString = " exec PARAM_CC_RFTRACKLST_APPR '" + 
				appr_sta + "',  '" + tr_code + "','" + userid+ "','" + 
				paramname + "','" + paramclass + "'";
			conn2.ExecuteQuery();
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGR_APPR.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton)  DGR_APPR.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton)  DGR_APPR.Items[i].FindControl("rdo_Reject");
					if (rbA.Checked)
						performRequestList(i, '1');
					else if (rbR.Checked)
						performRequestList(i, '0');
				} 
				catch {}
			}
			ViewPendingData();
		}
	}
}
