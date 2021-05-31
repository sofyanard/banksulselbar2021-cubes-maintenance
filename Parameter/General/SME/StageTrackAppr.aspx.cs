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
	/// Summary description for StageTrackAppr.
	/// </summary>
	public partial class StageTrackAppr : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				FillStaTrackAppr();
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
			this.DGR_STAGETRACKAPPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_STAGETRACKAPPR_ItemCommand);
			this.DGR_STAGETRACKAPPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_STAGETRACKAPPR_PageIndexChanged);

		}
		#endregion

		private void FillStaTrackAppr()
		{
			conn.QueryString = "SELECT * FROM VW_PENDING_RF_STAGE_TRACK";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_STAGETRACKAPPR.DataSource = dt;
			try
			{
				DGR_STAGETRACKAPPR.DataBind();
			}
			catch
			{
				DGR_STAGETRACKAPPR.CurrentPageIndex = 0;
				DGR_STAGETRACKAPPR.DataBind();
			}
		}


		private void DGR_STAGETRACKAPPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_STAGETRACKAPPR.CurrentPageIndex = e.NewPageIndex;
			FillStaTrackAppr();
		}

		private void DGR_STAGETRACKAPPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAccept":
					for (i = 0; i < DGR_STAGETRACKAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_STAGETRACKAPPR.Items[i].FindControl("RDO_ACCEPT"),
								rbB = (RadioButton) DGR_STAGETRACKAPPR.Items[i].FindControl("RDO_PENDING"),
								rbC = (RadioButton) DGR_STAGETRACKAPPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = true;
							rbB.Checked = false;
							rbC.Checked = false;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_STAGETRACKAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_STAGETRACKAPPR.Items[i].FindControl("RDO_ACCEPT"),
								rbB = (RadioButton) DGR_STAGETRACKAPPR.Items[i].FindControl("RDO_PENDING"),
								rbC = (RadioButton) DGR_STAGETRACKAPPR.Items[i].FindControl("RDO_REJECT");
							rbA.Checked = false;
							rbB.Checked = true;
							rbC.Checked = false;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_STAGETRACKAPPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_STAGETRACKAPPR.Items[i].FindControl("RDO_ACCEPT"),
								rbB = (RadioButton) DGR_STAGETRACKAPPR.Items[i].FindControl("RDO_PENDING"),
								rbC = (RadioButton) DGR_STAGETRACKAPPR.Items[i].FindControl("RDO_REJECT");
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
			for (int i = 0; i < DGR_STAGETRACKAPPR.Items.Count; i++)
			{
				try	
				{
					RadioButton rbA = (RadioButton) DGR_STAGETRACKAPPR.Items[i].FindControl("RDO_ACCEPT"),
						rbR = (RadioButton) DGR_STAGETRACKAPPR.Items[i].FindControl("RDO_REJECT");
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
			FillStaTrackAppr();
		}

		private void performRequest(int row)
		{
			try 
			{
				string active = "1";
				string stagecode = DGR_STAGETRACKAPPR.Items[row].Cells[1].Text.Trim();
				string trackcode = DGR_STAGETRACKAPPR.Items[row].Cells[3].Text.Trim();
				string status = DGR_STAGETRACKAPPR.Items[row].Cells[5].Text.Trim();
				string seq = DGR_STAGETRACKAPPR.Items[row].Cells[0].Text.Trim();
				string seq_curr = DGR_STAGETRACKAPPR.Items[row].Cells[10].Text.Trim();

				/*if(DGR_STAGETRACKAPPR.Items[row].Cells[5].Text.Trim()=="2") //Remove Stage
					active = "0";
				else
					active = "1";*/

				conn.QueryString = "exec PARAM_GENERAL_RFSTAGE_TRACK_INSERT '" + 
					stagecode + "', '" + trackcode + "', '" + active + "', '" + status + "', " + 
					int.Parse(seq) + ", " + int.Parse(seq_curr);
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string stagecode = DGR_STAGETRACKAPPR.Items[row].Cells[1].Text.Trim();
				string trackcode = DGR_STAGETRACKAPPR.Items[row].Cells[3].Text.Trim();
				//string status = DGR_STAGETRACKAPPR.Items[row].Cells[5].Text.Trim();
				string seq = DGR_STAGETRACKAPPR.Items[row].Cells[0].Text.Trim();
				string seq_curr = DGR_STAGETRACKAPPR.Items[row].Cells[10].Text.Trim();

				conn.QueryString = "exec PARAM_GENERAL_RFSTAGE_TRACK_INSERT '" + 
					stagecode + "', '" + trackcode + "', '0', '" + 4 + "', " + 
					int.Parse(seq) + ", " + int.Parse(seq_curr);
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}


	}
}
