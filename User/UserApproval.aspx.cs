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
using DMS.CuBESCore.Maintenance;
using System.Configuration;

namespace CuBES_Maintenance.User
{
	/// <summary>
	/// Summary description for UserApproval.
	/// </summary>
	public partial class UserApproval : System.Web.UI.Page
	{
		protected Connection conn;
		protected DataTable dt = new DataTable();
		protected DMS.CuBESCore.Maintenance.UserMaintenance um = new DMS.CuBESCore.Maintenance.UserMaintenance();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			conn = new Connection(Session["ConnString"].ToString());

			if (!IsPostBack)
			{
				HyperLink1.NavigateUrl = "UserApproval.aspx?mc=" + Request.QueryString["mc"];
				HyperLink2.NavigateUrl = "GroupApproval.aspx?mc=" + Request.QueryString["mc"];
				ViewData();
			}
		}

		private void ViewData()
		{
			conn.QueryString = "select * from vw_pending_scuser";
            conn.ExecuteQuery();
			dt = conn.GetDataTable().Copy();
			DataGrid1.DataSource = dt;
			DataGrid1.DataBind();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			base.OnInit(e);
            if (!this.DesignMode)
            {
                InitializeComponent();
            }
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void ProcessPending()
		{
			for (int i = 0; i < DataGrid1.Items.Count; i++)
			{
				RadioButton rdA = (RadioButton) DataGrid1.Items[i].FindControl("RDO_APPROVE"),
					rdR = (RadioButton) DataGrid1.Items[i].FindControl("RDO_REJECT");
				if (rdA.Checked == true)
				{
					if (DataGrid1.Items[i].Cells[3].Text == "2")
					{
						conn.QueryString = "exec SU_SCUSER_DELETE '" + DataGrid1.Items[i].Cells[0].Text + "', '" + 
							"', '', '0', '', '" + Session["UserID"].ToString() + "'";
						conn.ExecuteNonQuery();
					}
					if (DataGrid1.Items[i].Cells[3].Text == "3")
					{
						conn.QueryString = "exec SU_SCUSER_UNDELETE '" + DataGrid1.Items[i].Cells[0].Text + "', '" + 
							"', '', '0', '', '" + Session["UserID"].ToString() + "'";
						conn.ExecuteNonQuery();
					}
					else if (DataGrid1.Items[i].Cells[3].Text == "0" || DataGrid1.Items[i].Cells[3].Text == "1")
						um.ApproveUser(DataGrid1.Items[i].Cells[0].Text, DataGrid1.Items[i].Cells[8].Text, conn, Session["UserID"].ToString(), Session["PWD"].ToString());	
				}
				else if (rdR.Checked == true)
				{
					conn.QueryString = "delete from pending_scuser where userid='" + DataGrid1.Items[i].Cells[0].Text + "'";
					conn.ExecuteNonQuery();
				}
			}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			try
			{
				ProcessPending();
				GlobalTools.popMessage(this, "Update Complete...");
			} 
			catch (Exception ex)
			{
				Response.Write("<!-- " + ex.Message + " -->\n");
				GlobalTools.popMessage(this, "Update Failed...");
			}
			ViewData();
		}

	}
}
