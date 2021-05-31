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
	/// Summary description for ProblemApproval.
	/// </summary>
	public partial class ProblemApproval : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				FillProbAppr();
			}
		}

		private void FillProbAppr()
		{
			conn.QueryString = "SELECT * FROM VW_PENDING_RFPROBLEM";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_PROBAPPR.DataSource = dt;
			try
			{
				DGR_PROBAPPR.DataBind();
			}
			catch
			{
				DGR_PROBAPPR.CurrentPageIndex = 0;
				DGR_PROBAPPR.DataBind();
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
			this.DGR_PROBAPPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_PROBAPPR_ItemCommand);
			this.DGR_PROBAPPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_PROBAPPR_PageIndexChanged);

		}
		#endregion

		protected void BTN_SUB_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGR_PROBAPPR.Items.Count; i++)
			{
				try	
				{
					RadioButton rbA = (RadioButton) DGR_PROBAPPR.Items[i].FindControl("RDO_ACCEPT"),
						rbR = (RadioButton) DGR_PROBAPPR.Items[i].FindControl("RDO_REJECT");
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
			FillProbAppr();
		}

		private void performRequest(int row)
		{
			try 
			{
				string active;
				string code = DGR_PROBAPPR.Items[row].Cells[0].Text.Trim();
				string name = DGR_PROBAPPR.Items[row].Cells[1].Text.Trim();

				if(DGR_PROBAPPR.Items[row].Cells[3].Text.Trim()=="2") //Remove Stage
					active = "0";
				else
					active = "1";

				conn.QueryString = "exec PARAM_GENERAL_RFPROBLEM_INSERT '" + 
					code + "', '" + name + "', '" + active + "', '1'";
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string code = DGR_PROBAPPR.Items[row].Cells[0].Text.Trim();
				string name = DGR_PROBAPPR.Items[row].Cells[1].Text.Trim();

				conn.QueryString = "exec PARAM_GENERAL_RFPROBLEM_INSERT '" + 
					code + "', '" + name + "', '0', '2'";
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		private void DGR_PROBAPPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_PROBAPPR.CurrentPageIndex = e.NewPageIndex;
			FillProbAppr();
		}

		private void DGR_PROBAPPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAccept":
					for (i = 0; i < DGR_PROBAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_PROBAPPR.Items[i].FindControl("RDO_ACCEPT"),
								rbB = (RadioButton) DGR_PROBAPPR.Items[i].FindControl("RDO_PENDING"),
								rbC = (RadioButton) DGR_PROBAPPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = true;
							rbB.Checked = false;
							rbC.Checked = false;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_PROBAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_PROBAPPR.Items[i].FindControl("RDO_ACCEPT"),
								rbB = (RadioButton) DGR_PROBAPPR.Items[i].FindControl("RDO_PENDING"),
								rbC = (RadioButton) DGR_PROBAPPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = false;
							rbB.Checked = true;
							rbC.Checked = false;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_PROBAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_PROBAPPR.Items[i].FindControl("RDO_ACCEPT"),
								rbB = (RadioButton) DGR_PROBAPPR.Items[i].FindControl("RDO_PENDING"),
								rbC = (RadioButton) DGR_PROBAPPR.Items[i].FindControl("RDO_REJECT");
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
	}
}
