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
	/// Summary description for GroupParamApproveAll.
	/// </summary>
	public class GroupParamApproveAll : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton BTN_BACK;
		protected System.Web.UI.WebControls.DataGrid DGR_GROUPAPPR;
		protected System.Web.UI.WebControls.Button BTN_SUB;
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				FillGroupAppr();
			}
		}

		private void FillGroupAppr()
		{
			conn.QueryString="select CODE, DESC_CUR, case status when '0' then 'Insert' end as status from rf_group where status='0'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_GROUPAPPR.DataSource = dt;
			try
			{
				DGR_GROUPAPPR.DataBind();
			}
			catch
			{
				DGR_GROUPAPPR.CurrentPageIndex=0;
				DGR_GROUPAPPR.DataBind();
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
			this.BTN_SUB.Click += new System.EventHandler(this.BTN_SUB_Click);
			this.DGR_GROUPAPPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_GROUPAPPR_ItemCommand);
			this.DGR_GROUPAPPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_GROUPAPPR_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DGR_GROUPAPPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_GROUPAPPR.CurrentPageIndex = e.NewPageIndex;
			FillGroupAppr();
		}

		private void DGR_GROUPAPPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAccept":
					for (i = 0; i < DGR_GROUPAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_GROUPAPPR.Items[i].FindControl("RDO_APPROVE"),
								rbP = (RadioButton) DGR_GROUPAPPR.Items[i].FindControl("RDO_PENDING"),
								rbR = (RadioButton) DGR_GROUPAPPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = true;
							rbP.Checked = false;
							rbR.Checked = false;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_GROUPAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_GROUPAPPR.Items[i].FindControl("RDO_APPROVE"),
								rbP = (RadioButton) DGR_GROUPAPPR.Items[i].FindControl("RDO_PENDING"),
								rbR = (RadioButton) DGR_GROUPAPPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = false;
							rbP.Checked = true;
							rbR.Checked = false;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_GROUPAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_GROUPAPPR.Items[i].FindControl("RDO_APPROVE"),
								rbP = (RadioButton) DGR_GROUPAPPR.Items[i].FindControl("RDO_PENDING"),
								rbR = (RadioButton) DGR_GROUPAPPR.Items[i].FindControl("RDO_REJECT");
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
			for (int i = 0; i < DGR_GROUPAPPR.Items.Count; i++)
			{
				try	
				{
					RadioButton rbA = (RadioButton) DGR_GROUPAPPR.Items[i].FindControl("RDO_APPROVE"),
						rbR = (RadioButton) DGR_GROUPAPPR.Items[i].FindControl("RDO_REJECT");
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
			FillGroupAppr();
		}

		private void performRequest(int row)
		{
			try 
			{
				string code = DGR_GROUPAPPR.Items[row].Cells[0].Text.Trim();
				string name = DGR_GROUPAPPR.Items[row].Cells[1].Text.Trim();

				conn.QueryString = "update rf_group set status='1' where CODE='" + code + "' and DESC_CUR='" + name + "'";
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string code = DGR_GROUPAPPR.Items[row].Cells[0].Text.Trim();
				string name = DGR_GROUPAPPR.Items[row].Cells[1].Text.Trim();

				conn.QueryString = "delete rf_group where CODE='" + code + "' and DESC_CUR='" + name + "'";
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		private void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../JWSParamApproval.aspx?mc="+Request.QueryString["mc"]+"&pg=9");
		}
	}
}
