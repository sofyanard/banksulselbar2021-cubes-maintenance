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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for TATParamAppr.
	/// </summary>
	public partial class TATParamAppr : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				FillGRD_TAT();
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
			this.DGR_TATPARAM_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_TATPARAM_APPR_ItemCommand);
			this.DGR_TATPARAM_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_TATPARAM_APPR_PageIndexChanged);

		}
		#endregion

		private void FillGRD_TAT()
		{
			conn.QueryString = "SELECT * FROM VW_PENDING_RF_TAT";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_TATPARAM_APPR.DataSource = dt;
			try
			{
				DGR_TATPARAM_APPR.DataBind();
			}
			catch
			{
				DGR_TATPARAM_APPR.CurrentPageIndex = 0;
				DGR_TATPARAM_APPR.DataBind();
			}
		}


		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGR_TATPARAM_APPR.Items.Count; i++)
			{
				try	
				{
					RadioButton rbA = (RadioButton) DGR_TATPARAM_APPR.Items[i].FindControl("RDO_ACCEPT"),
						rbR = (RadioButton) DGR_TATPARAM_APPR.Items[i].FindControl("RDO_REJECT");
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
			FillGRD_TAT();
		}

		private void performRequest(int row)
		{
			try 
			{
				string active = "1";
				string code = DGR_TATPARAM_APPR.Items[row].Cells[0].Text.Trim();
				string flowname = DGR_TATPARAM_APPR.Items[row].Cells[1].Text.Trim();
				string slacode = DGR_TATPARAM_APPR.Items[row].Cells[2].Text.Trim();
				string sladay = DGR_TATPARAM_APPR.Items[row].Cells[4].Text.Trim();
				string segment = DGR_TATPARAM_APPR.Items[row].Cells[5].Text.Trim();
				string status = DGR_TATPARAM_APPR.Items[row].Cells[7].Text.Trim();

				/*if(DGR_STAGEAPPR.Items[row].Cells[3].Text.Trim()=="2") //Remove Stage
					active = "0";
				else
					active = "1";*/

				conn.QueryString = "exec PARAM_GENERAL_RF_TAT_INSERT '" + 
					code + "', '" + flowname + "', '" + slacode + "', " + int.Parse(sladay) + ", '" +
					segment + "', '" + active + "', '" + status + "'";
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string active = "0";
				string code = DGR_TATPARAM_APPR.Items[row].Cells[0].Text.Trim();
				string flowname = DGR_TATPARAM_APPR.Items[row].Cells[1].Text.Trim();
				string slacode = DGR_TATPARAM_APPR.Items[row].Cells[2].Text.Trim();
				string sladay = DGR_TATPARAM_APPR.Items[row].Cells[4].Text.Trim();
				string segment = DGR_TATPARAM_APPR.Items[row].Cells[5].Text.Trim();
				string status = "4";

				conn.QueryString = "exec PARAM_GENERAL_RF_TAT_INSERT '" + 
					code + "', '" + flowname + "', '" + slacode + "', " + int.Parse(sladay) + ", '" +
					segment + "', '" + active + "', '" + status + "'";
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		private void DGR_TATPARAM_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAccept":
					for (i = 0; i < DGR_TATPARAM_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_TATPARAM_APPR.Items[i].FindControl("RDO_ACCEPT"),
								rbB = (RadioButton) DGR_TATPARAM_APPR.Items[i].FindControl("RDO_PENDING"),
								rbC = (RadioButton) DGR_TATPARAM_APPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = true;
							rbB.Checked = false;
							rbC.Checked = false;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_TATPARAM_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_TATPARAM_APPR.Items[i].FindControl("RDO_ACCEPT"),
								rbB = (RadioButton) DGR_TATPARAM_APPR.Items[i].FindControl("RDO_PENDING"),
								rbC = (RadioButton) DGR_TATPARAM_APPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = false;
							rbB.Checked = true;
							rbC.Checked = false;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_TATPARAM_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_TATPARAM_APPR.Items[i].FindControl("RDO_ACCEPT"),
								rbB = (RadioButton) DGR_TATPARAM_APPR.Items[i].FindControl("RDO_PENDING"),
								rbC = (RadioButton) DGR_TATPARAM_APPR.Items[i].FindControl("RDO_REJECT");
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

		private void DGR_TATPARAM_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_TATPARAM_APPR.CurrentPageIndex = e.NewPageIndex;
			FillGRD_TAT();
		}
	}
}
