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
using DMS.CuBESCore.Maintenance;

namespace CuBES_Maintenance.Parameter.Area
{
	/// <summary>
	/// Summary description for BranchApproval.
	/// </summary>
	public partial class BranchApproval : System.Web.UI.Page
	{
		protected Connection conn;
		protected DMS.CuBESCore.Maintenance.ParameterMaintenance pm = new DMS.CuBESCore.Maintenance.ParameterMaintenance();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());
			if (!IsPostBack)
				ViewData();
		}

		private void ViewData()
		{
			conn.QueryString = "select * from vw_pending_rfbranch";
			conn.ExecuteQuery();
			DataTable dt = conn.GetDataTable().Copy();
			DataGrid1.DataSource = dt;
			DataGrid1.DataBind();
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

		}
		#endregion

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			int success = 1;

			for (int i = 0; i < DataGrid1.Items.Count; i++)
			{
				RadioButton rdA = (RadioButton) DataGrid1.Items[i].FindControl("RDO_APPROVE"),
					rdR = (RadioButton) DataGrid1.Items[i].FindControl("RDO_REJECT");
				if (rdA.Checked == true)
				{
					if (DataGrid1.Items[i].Cells[2].Text == "2")
					{
					}
					else if (DataGrid1.Items[i].Cells[2].Text == "0" || DataGrid1.Items[i].Cells[2].Text == "1")
					{
						success = pm.ApproveBranch(DataGrid1.Items[i].Cells[0].Text, conn);
						if (success != 1)
							break;
						conn.QueryString = "delete from pending_rfbranch where branch_code = '" + DataGrid1.Items[i].Cells[0].Text + "'";
						conn.ExecuteNonQuery();
					}
				}
				else if (rdR.Checked == true)
				{
					conn.QueryString = "delete from pending_rfbranch where branch_code='" + DataGrid1.Items[i].Cells[0].Text + "'";
					conn.ExecuteNonQuery();
				}
			}
			if (success == 1)
				Response.Write("<script language='javascript'>alert('Update Complete...');</script>");
			else 
				Response.Write("<script language='javascript'>alert('Update Failed...');</script>");
			ViewData();
		}

	}
}
