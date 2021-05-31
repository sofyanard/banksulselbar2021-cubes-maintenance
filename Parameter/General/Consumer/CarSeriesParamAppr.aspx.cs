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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for CarSeriesParamAppr.
	/// </summary>
	public partial class CarSeriesParamAppr : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection (System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
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

		public void SetDBConn2()
		{
			conn2.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["ModuleID"] + "'";
			conn2.ExecuteQuery();

			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
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

		private void ViewPendingData()
		{
			conn.QueryString = "select * from VW_PARAM_CARSERIES_TTIPE order by ID_TIPE,NM_MEREK,NM_MODEL,NM_TIPE";
			conn.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
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
			for (int i=0; i<this.DGR_APPR.Items.Count;i++)
			{
				this.DGR_APPR.Items[i].Cells[7].Text = getPendingStatus(this.DGR_APPR.Items[i].Cells[7].Text);
			}
		}
		
		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		private void performRequestList(int row, char appr_sta)
		{
			string userid	 = Session["UserID"].ToString();
			string groupid	 = Session["GroupID"].ToString();
			string id_tipe	 = this.DGR_APPR.Items[row].Cells[0].Text.Trim();
			string id_model	 = this.DGR_APPR.Items[row].Cells[2].Text.Trim();
			string nm_tipe	 = this.DGR_APPR.Items[row].Cells[5].Text.Trim();
			conn.QueryString = " exec PARAM_CC_TIPE_APPR '" + appr_sta + "','" + id_tipe + "','" +
				id_model + "','" + nm_tipe + "' ,'"+userid+"','"+groupid+"'";
			conn.ExecuteNonQuery();
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
	}
}
