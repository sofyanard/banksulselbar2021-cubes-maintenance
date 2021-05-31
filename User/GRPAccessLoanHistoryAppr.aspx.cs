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

namespace CuBES_Maintenance.User
{
	/// <summary>
	/// Summary description for GRPAccessLoanHistoryAppr.
	/// </summary>
	public partial class GRPAccessLoanHistoryAppr : System.Web.UI.Page
	{

		private Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
			
			if (!IsPostBack) 
			{
				viewPendingData();
			}
		}

		/// <summary>
		/// Mengisi datagrid dengan pending data
		/// </summary>
		private void viewPendingData() 
		{
			conn.QueryString = "select * from VW_GRPACCESSLOANHISTORY_PENDING";
			conn.ExecuteQuery();

			DGRequest.DataSource = conn.GetDataTable().DefaultView;
			try 
			{
				DGRequest.DataBind();
			} 
			catch 
			{
				DGRequest.CurrentPageIndex = DGRequest.PageCount - 1;
				DGRequest.DataBind();
			}
		}

		/// <summary>
		/// Mengambil deskripsi pending status
		/// </summary>
		/// <param name="saveMode"></param>
		/// <returns></returns>
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


		/// <summary>
		/// Melakukan eksekusi pending status (insert, update, delete)
		/// </summary>
		/// <param name="row"></param>
		private void performRequest(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				conn.QueryString = "exec GRPACCESSLOANHISTORY_APPR '" + id + "', '1'";
				conn.ExecuteQuery();
			} 
			catch {}
		}

		/// <summary>
		/// Membatalkan eksekusi pending status (rollback)
		/// </summary>
		/// <param name="row"></param>
		private void deleteData(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				conn.QueryString = "exec SU_GRPACCESSLOANHISTORY_APPR '" + id + "', '0'";
				conn.ExecuteQuery();
			} 
			catch {}
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

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
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
		
		}

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;			
			viewPendingData();
		}
	}
}
