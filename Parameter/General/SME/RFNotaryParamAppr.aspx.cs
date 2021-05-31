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
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for NasabahGroupInfo.
	/// </summary>
	public partial class RFNotaryParamAppr : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.WebControls.Button updatestatus;
		protected Tools tool = new Tools();		
		protected Connection conn;
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV;uid=sa;pwd=");
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

		//	if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
//				Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack) 
			{				
				viewPendingData();				
			}

			DGRequest.PageIndexChanged +=new DataGridPageChangedEventHandler(DGRequest_PageIndexChanged);
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

		}
		#endregion

		private void viewPendingData() 
		{		
			conn.QueryString = "select * from PENDING_RFNOTARY where ACTIVE = '1' order by NT_NAME";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("NTID"));
			dt.Columns.Add(new DataColumn("NT_NAME"));
			dt.Columns.Add(new DataColumn("NT_ADDR"));
			dt.Columns.Add(new DataColumn("NT_CITY"));
			dt.Columns.Add(new DataColumn("NT_ZIPCODE"));
			dt.Columns.Add(new DataColumn("NT_PHN"));
			dt.Columns.Add(new DataColumn("NT_FAX"));
			dt.Columns.Add(new DataColumn("NT_EMAIL"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,2) + " " + conn.GetFieldValue(i,3) + " " + conn.GetFieldValue(i,4);
				dr[3] = conn.GetFieldValue(i,5);
				dr[4] = conn.GetFieldValue(i,13);
				dr[5] = conn.GetFieldValue(i,7) + " " + conn.GetFieldValue(i,8) + " " + conn.GetFieldValue(i,9);
				dr[6] = conn.GetFieldValue(i,10) + " " + conn.GetFieldValue(i,11) + " " + conn.GetFieldValue(i,12);
				dr[7] = conn.GetFieldValue(i,6);
				dr[8] = conn.GetFieldValue(i,15);
				dr[9] = getPendingStatus(conn.GetFieldValue(i,15));
				dt.Rows.Add(dr);
			}			

			DGRequest.DataSource = new DataView(dt);
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
		
		private void DGRequest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;			
			viewPendingData();
		}

		private void performRequest(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				conn.QueryString = "exec PARAM_GENERAL_RFNOTARY_APPR '" + id + "', '1', '" + 
					Session["UserID"].ToString() + "'";
				conn.ExecuteQuery();
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				conn.QueryString = "exec PARAM_GENERAL_RFNOTARY_APPR '" + id + "', '0', '" + 
					Session["UserID"].ToString() + "'";
				conn.ExecuteQuery();
			} 
			catch {}
		}

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
			//Response.Redirect("../../HostParamApproval.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}
	}		
}
