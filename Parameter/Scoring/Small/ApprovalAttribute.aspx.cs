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

namespace CuBES_Maintenance.Parameter.Scoring.Small
{
	/// <summary>
	/// Summary description for ApprovalAttribute.
	/// </summary>
	public partial class ApprovalAttribute : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				viewPendingData();
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
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGRequest.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject");

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
			viewPendingData();
		}

		private void performRequest(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				executeApproval(id, "1");
			} 
			catch (Exception e)
			{
				Response.Write("<!-- " + e.Message + " --> ");
			}
		}

		private void deleteData(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				executeApproval(id, "0");
			} 
			catch (Exception e)
			{
				Response.Write("<!-- " + e.Message + " --> ");
			}
		}

		private void executeApproval(string id, string approvalFlag) 
		{
			conn.QueryString = "EXEC SCORING_APPROVEATTRIBUTE '" + id + "','" + approvalFlag + "'";
			conn.ExecuteNonQuery();
			conn.ClearData();

			viewPendingData();
		}

		private void viewPendingData() 
		{
			conn.QueryString = "EXEC SCORING_BINDTEMP 'ATTRIBUTE'";
			conn.ExecuteQuery();

			BindData();
		}

		private void BindData()
		{
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGRequest.DataSource = dt;				
			DGRequest.DataBind();

			conn.ClearData();
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (int i = 0; i < DGRequest.Items.Count; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbR = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject");

							rbA.Checked = true;
							rbR.Checked = false;
						}
						catch{}
					}
					break;
				case "allRejc":
					for (int i = 0; i < DGRequest.Items.Count; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbR = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject");

							rbA.Checked = false;
							rbR.Checked = true;
						}
						catch{}
					}
					break;
			}
		}

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DGRequest.CurrentPageIndex >= 0 && DGRequest.CurrentPageIndex < DGRequest.PageCount)
			{
				DGRequest.CurrentPageIndex = e.NewPageIndex;
				viewPendingData();
			}
		}
	}
}
