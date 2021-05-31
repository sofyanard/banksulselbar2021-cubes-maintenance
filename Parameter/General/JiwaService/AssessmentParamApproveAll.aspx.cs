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
	/// Summary description for AssessmentParamApproveAll.
	/// </summary>
	public class AssessmentParamApproveAll : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton BTN_BACK;
		protected System.Web.UI.WebControls.DataGrid DGR_SELFAPPR;
		protected System.Web.UI.WebControls.Button BTN_SUB;
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				FillSelfAppr();
			}
		}

		private void FillSelfAppr()
		{
			conn.QueryString="SELECT *, CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_SELF WHERE STATUS='0'";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_SELFAPPR.DataSource = dt;
			try
			{
				DGR_SELFAPPR.DataBind();
			}
			catch
			{
				DGR_SELFAPPR.CurrentPageIndex=0;
				DGR_SELFAPPR.DataBind();
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
			this.DGR_SELFAPPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_SELFAPPR_ItemCommand);
			this.DGR_SELFAPPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_SELFAPPR_PageIndexChanged);
			this.BTN_SUB.Click += new System.EventHandler(this.BTN_SUB_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DGR_SELFAPPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_SELFAPPR.CurrentPageIndex = e.NewPageIndex;
			FillSelfAppr();
		}

		private void DGR_SELFAPPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAccept":
					for (i = 0; i < DGR_SELFAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_SELFAPPR.Items[i].FindControl("RDO_APPROVE"),
								rbP = (RadioButton) DGR_SELFAPPR.Items[i].FindControl("RDO_PENDING"),
								rbR = (RadioButton) DGR_SELFAPPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = true;
							rbP.Checked = false;
							rbR.Checked = false;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_SELFAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_SELFAPPR.Items[i].FindControl("RDO_APPROVE"),
								rbP = (RadioButton) DGR_SELFAPPR.Items[i].FindControl("RDO_PENDING"),
								rbR = (RadioButton) DGR_SELFAPPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = false;
							rbP.Checked = true;
							rbR.Checked = false;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_SELFAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_SELFAPPR.Items[i].FindControl("RDO_APPROVE"),
								rbP = (RadioButton) DGR_SELFAPPR.Items[i].FindControl("RDO_PENDING"),
								rbR = (RadioButton) DGR_SELFAPPR.Items[i].FindControl("RDO_REJECT");
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
			for (int i = 0; i < DGR_SELFAPPR.Items.Count; i++)
			{
				try	
				{
					RadioButton rbA = (RadioButton) DGR_SELFAPPR.Items[i].FindControl("RDO_APPROVE"),
						rbR = (RadioButton) DGR_SELFAPPR.Items[i].FindControl("RDO_REJECT");
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
			FillSelfAppr();
		}

		private void performRequest(int row)
		{
			try 
			{
				string seqid = DGR_SELFAPPR.Items[row].Cells[0].Text.Trim();
				string seqstep = DGR_SELFAPPR.Items[row].Cells[7].Text.Trim();

				conn.QueryString = "UPDATE RF_SELF SET STATUS='1' WHERE SEQ='" + seqid + "' AND LANGKAH_SEQ='" + seqstep + "'";
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string seqid = DGR_SELFAPPR.Items[row].Cells[0].Text.Trim();
				string seqstep = DGR_SELFAPPR.Items[row].Cells[7].Text.Trim();

				conn.QueryString = "DELETE RF_SELF WHERE SEQ='" + seqid + "' AND LANGKAH_SEQ='" + seqstep + "'";
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
