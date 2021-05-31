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
	/// Summary description for Sandi BI
	/// </summary>
	public partial class RFBICodeParamAppr : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.WebControls.Button updatestatus;
		protected Tools tool = new Tools();
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

			//if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
				//Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack) 
			{				
				fillDDLBG_Group();
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

		private void fillDDLBG_Group() 
		{
			DDL_BG_GROUP.Items.Add(new ListItem("- PILIH -", ""));

			conn.QueryString = "select * from RFBICODEGROUP order by BG_DESC";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_BG_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, "BG_DESC"), conn.GetFieldValue(i,"BG_ID")));
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

		private void viewPending(string BG_GROUP) 
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_PENDING_RFBICODE where BG_GROUP = '"+BG_GROUP+"'";
			try 
			{
				conn.ExecuteQuery();
			} 
			catch {
				Tools.popMessage(this, "Error View !");
				return;
			}

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("BG_GROUP"));
			dt.Columns.Add(new DataColumn("BG_DESC"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i, "BI_SEQ");
				dr[1] = conn.GetFieldValue(i, "BI_DESC");
				dr[2] = conn.GetFieldValue(i, "BG_GROUP");
				dr[3] = conn.GetFieldValue(i, "BG_DESC");
				dr[4] = conn.GetFieldValue(i, "PENDINGSTATUS");
				dr[5] = getPendingStatus(conn.GetFieldValue(i,"PENDINGSTATUS"));				
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
			viewPending(DDL_BG_GROUP.SelectedValue);
		}

		private void performRequest(int row)
		{
			try 
			{
				string seq = DGRequest.Items[row].Cells[0].Text.Trim();
				string BG_GROUP = DGRequest.Items[row].Cells[2].Text;

				conn.QueryString = "exec PARAM_GENERAL_RFBICODE_APPR '"+seq+"','"+BG_GROUP+"','1', '" + Session["UserID"].ToString() + "'";
				try 
				{
					conn.ExecuteQuery();
				} 
				catch {
					Tools.popMessage(this, "Error Stored-Procedure !");
					return;
				}
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string SEQ = DGRequest.Items[row].Cells[0].Text.Trim();
				string BG_GROUP = DGRequest.Items[row].Cells[2].Text;

				conn.QueryString = "exec PARAM_GENERAL_RFBICODE_APPR '"+SEQ+"','"+BG_GROUP+"','0', '" + Session["UserID"].ToString() + "'";

				try 
				{
					conn.ExecuteQuery();
				} 
				catch {
					Tools.popMessage(this, "Error Stored-Procedure !");
					return;
				}
			} 
			catch {}
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

			viewPending(DDL_BG_GROUP.SelectedValue);
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../HostParamApproval.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		protected void DDL_BG_GROUP_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			viewPending(DDL_BG_GROUP.SelectedValue);
		}

		
	}		
}
