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
	/// Summary description for Syarat-syarat.
	/// </summary>
	public partial class SyaratParamAppr : System.Web.UI.Page
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
		//		Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack) 
			{				
				fillDocumentType();
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

		private void fillDocumentType() 
		{
			DDL_DOCTYPEID.Items.Add(new ListItem("- PILIH -", ""));

			conn.QueryString = "select * from VW_PARAM_GENERAL_DOCTYPE_SYARAT";			
			try 
			{
				conn.ExecuteQuery();
			} 
			catch (ApplicationException) 
			{
				Tools.popMessage(this, "View Error !");
				return;
			}
			catch (NullReferenceException) 
			{
				Tools.popMessage(this, "Server Error !");
				return;
			}

			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_DOCTYPEID.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			}
		}

		private string getStatusMandatory(string val) 
		{
			string mandatory = "";
			switch (val.ToLower()) 
			{
				case "0":
					mandatory = "No";
					break;
				case "1":
					mandatory = "Yes";
					break;
				case "yes":
					mandatory = "1";
					break;
				case "no":
					mandatory = "0";
					break;
				default:
					break;
			}
			return mandatory;
		}		

		private void viewPendingDocuments(string viewName) 
		{
			conn.QueryString = "select * from " + viewName;
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("MANDATORY"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,4);
				dr[1] = conn.GetFieldValue(i,7);
				dr[2] = getStatusMandatory(conn.GetFieldValue(i,5).ToString());
				dr[3] = conn.GetFieldValue(i,6);
				dr[4] = getPendingStatus(conn.GetFieldValue(i,6));				
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
			viewPendingDocuments("VW_PARAM_GENERAL_PENDING_SYARAT");
		}

		private void performRequest(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				string mandatory = getStatusMandatory(DGRequest.Items[row].Cells[2].Text.Trim());

				procSyaratAppr(id, "1");
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				string mandatory = getStatusMandatory(DGRequest.Items[row].Cells[2].Text.Trim());
				
				procSyaratAppr(id, "0");
			} 
			catch {}
		}

		private void procSyaratAppr(string id, string mode) 
		{
			//********************
			// mode :
			//		0 --> Reject
			//		1 --> Approve
			//*********************
			conn.QueryString = "exec PARAM_GENERAL_TBOLIST_APPR '" + DDL_DOCTYPEID.SelectedValue + 
				"', null, null, '" + id + "',null,'" + mode + "', '" + Session["UserID"].ToString() + "','1'";
			try 
			{			
				conn.ExecuteQuery();
			} 
			catch (ApplicationException) 
			{
				Tools.popMessage(this, "Stored-Procedure Error !");
				return;
			}
			catch (NullReferenceException) 
			{
				Tools.popMessage(this, "Server Error !");
				return;
			}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGRequest.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("RD_APPROVE"),
						        rbR = (RadioButton) DGRequest.Items[i].FindControl("RD_REJECT");
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

			viewPendingDocuments("VW_PARAM_GENERAL_PENDING_SYARAT");
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
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("RD_APPROVE"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("RD_REJECT"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("RD_PENDING");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("RD_APPROVE"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("RD_REJECT"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("RD_PENDING");
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
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("RD_APPROVE"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("RD_REJECT"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("RD_PENDING");
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

		protected void DDL_DOCTYPEID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string doctypeid = DDL_DOCTYPEID.SelectedValue.ToString();

			viewPendingDocuments("VW_PARAM_GENERAL_PENDING_SYARAT");
		}

		private void DDL_DOCTYPEDESC_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			viewPendingDocuments("VW_PARAM_GENERAL_PENDING_SYARAT");
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		    //Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}
	}		
}
