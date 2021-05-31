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

namespace CuBES_Maintenance.Parameter.General.JiwaService
{
	/// <summary>
	/// Summary description for CustomerParamApproveAll.
	/// </summary>
	public class CustomerParamApproveAll : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton BTN_BACK;
		protected System.Web.UI.WebControls.DataGrid DGR_QUESTAPPR;
		protected System.Web.UI.WebControls.Button BTN_SUB;
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				FillQuestAppr();
			}
		}

		private void FillQuestAppr()
		{
			conn.QueryString="select *, case status when '0' then 'Insert' end as STATUS_DESC from rf_customer where status='0'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_QUESTAPPR.DataSource = dt;
			try
			{
				DGR_QUESTAPPR.DataBind();
			}
			catch
			{
				DGR_QUESTAPPR.CurrentPageIndex=0;
				DGR_QUESTAPPR.DataBind();
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
			this.BTN_BACK.Click += new System.Web.UI.ImageClickEventHandler(this.BTN_BACK_Click);
			this.DGR_QUESTAPPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_QUESTAPPR_ItemCommand);
			this.DGR_QUESTAPPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_QUESTAPPR_PageIndexChanged);
			this.BTN_SUB.Click += new System.EventHandler(this.BTN_SUB_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DGR_QUESTAPPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_QUESTAPPR.CurrentPageIndex = e.NewPageIndex;
			FillQuestAppr();
		}

		private void DGR_QUESTAPPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAccept":
					for (i = 0; i < DGR_QUESTAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_QUESTAPPR.Items[i].FindControl("RDO_APPROVE"),
								rbP = (RadioButton) DGR_QUESTAPPR.Items[i].FindControl("RDO_PENDING"),
								rbR = (RadioButton) DGR_QUESTAPPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = true;
							rbP.Checked = false;
							rbR.Checked = false;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_QUESTAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_QUESTAPPR.Items[i].FindControl("RDO_APPROVE"),
								rbP = (RadioButton) DGR_QUESTAPPR.Items[i].FindControl("RDO_PENDING"),
								rbR = (RadioButton) DGR_QUESTAPPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = false;
							rbP.Checked = true;
							rbR.Checked = false;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_QUESTAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_QUESTAPPR.Items[i].FindControl("RDO_APPROVE"),
								rbP = (RadioButton) DGR_QUESTAPPR.Items[i].FindControl("RDO_PENDING"),
								rbR = (RadioButton) DGR_QUESTAPPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = false;
							rbP.Checked = false;
							rbR.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					break;
			}
		}

		private void BTN_SUB_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGR_QUESTAPPR.Items.Count; i++)
			{
				try	
				{
					RadioButton rbA = (RadioButton) DGR_QUESTAPPR.Items[i].FindControl("RDO_APPROVE"),
						rbR = (RadioButton) DGR_QUESTAPPR.Items[i].FindControl("RDO_REJECT");
					if (rbA.Checked)
					{
						performRequest(i);
					}
					else if (rbR.Checked)
					{
						deleteData(i);
					}
				} 
				catch {}
			}
			FillQuestAppr();
		}

		private void performRequest(int row)
		{
			try 
			{
				string nomor = DGR_QUESTAPPR.Items[row].Cells[0].Text.Trim();
				string groupcode = DGR_QUESTAPPR.Items[row].Cells[1].Text.Trim();
				string deptcode = DGR_QUESTAPPR.Items[row].Cells[3].Text.Trim();

				conn.QueryString = "update rf_customer set status='1' where [NO]='" + nomor + "' and g_code='"
					+ groupcode + "' and d_code='" + deptcode + "'";
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string nomor = DGR_QUESTAPPR.Items[row].Cells[0].Text.Trim();
				string groupcode = DGR_QUESTAPPR.Items[row].Cells[1].Text.Trim();
				string deptcode = DGR_QUESTAPPR.Items[row].Cells[3].Text.Trim();

				conn.QueryString = "delete rf_customer where [NO]='" + nomor + "' and g_code='"
					+ groupcode + "' and d_code='" + deptcode + "'";
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		private void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../JiwaServiceParam.aspx?mc="+Request.QueryString["mc"]+"&pg=9");
		}
	}
}
