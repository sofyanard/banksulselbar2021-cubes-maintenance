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
	/// Summary description for CompTypeParamAppr.
	/// </summary>
	public partial class CompTypeParamAppr : System.Web.UI.Page
	{
		protected Connection conn,conn2;
		protected Connection connsme = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection ((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				fillDDL();	
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
			conn.QueryString = "select * from VW_GETCONN where MODULEID='20'";
			conn.ExecuteQuery();
			string DB_NAMA = conn.GetFieldValue("DB_NAMA");
			string DB_IP = conn.GetFieldValue("DB_IP");
			string DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conn2 = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void fillDDL()
		{
			/* if moduleid change, must fix this!!! */
			DDL_MODULE.Items.Add(new ListItem("SME","01"));
			DDL_MODULE.Items.Add(new ListItem("Credit Card","20"));
			DDL_MODULE.Items.Add(new ListItem("Consumer","40"));

			DDL_MODULE.SelectedValue = "20";
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

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		protected void DDL_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (DDL_MODULE.SelectedValue != "20")
				Response.Redirect("../Consumer/CompanyParamAppr.aspx?mod=" + DDL_MODULE.SelectedValue);
			else
				ViewPendingData();
		}

		private void ViewPendingData()
		{
			conn2.QueryString = "select * from VW_PENDING_CC_RFCOMPANY";
			conn2.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
            DGR_APPR.DataSource = dt;
			try
			{
				DGR_APPR.DataBind();
			} 
			catch 
			{
				DGR_APPR.CurrentPageIndex = DGR_APPR.PageCount - 1;
				DGR_APPR.DataBind();
			}
			for (int i=0;i< DGR_APPR.Items.Count; i++)
			{
				DGR_APPR.Items[i].Cells[4].Text = getPendingStatus(DGR_APPR.Items[i].Cells[4].Text);
			}
		}

		private void performRequestList(int row, char appr_sta)
		{
			
			string userid = Session["UserID"].ToString();
			string cp_code		= CleansText(this.DGR_APPR.Items[row].Cells[0].Text);
			
			conn2.QueryString = " exec PARAM_CC_RFCOMPANY_APPR '" + cp_code + "','" +
				appr_sta + "','" + userid + "'";
			conn2.ExecuteNonQuery();
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGR_APPR.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton)  DGR_APPR.Items[i].FindControl("Rdb1"),
						rbR = (RadioButton)  DGR_APPR.Items[i].FindControl("Rdb2");
					if (rbA.Checked)
						performRequestList(i, '1');
					else if (rbR.Checked)
						performRequestList(i, '0');
				} 
				catch {}
			}
			ViewPendingData();
		}

		private void DGR_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_APPR.CurrentPageIndex = e.NewPageIndex;
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
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("Rdb1"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("Rdb2"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("Rdb3");
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
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("Rdb1"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("Rdb2"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("Rdb3");
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
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("Rdb1"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("Rdb2"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("Rdb3");
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../CommonParamApproval.aspx?mc=99020202"); 	
		}
	}
}
