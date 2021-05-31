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
	public partial class TBOListParamAppr : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.WebControls.Button updatestatus;
		protected Tools tool = new Tools();
		protected Connection conn;
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV;uid=sa;pwd=");
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

			//if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack) 
			{				
				fillDocumentType();
				//fillFacilityList();		
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

			conn.QueryString = "select DOCTYPEID, DOCTYPEDESC from RFDOCTYPE where DOCTYPEID <= 3"; // where ACTIVE = '1'";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_DOCTYPEID.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			}
		}

		private void fillDocumentType(string tableName, string columnId, string columnDesc) 
		{
			DDL_DOCTYPEDESC.Items.Clear();
			DDL_DOCTYPEDESC.Items.Add(new ListItem("- PILIH -", ""));

			conn.QueryString = "select " + columnId + ", " + columnDesc + 
				" from " + tableName + " where ACTIVE = '1'";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_DOCTYPEDESC.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			}
		}

		private void fillCollateralList() 
		{
			//fillDocumentType("RFCOLLATERALTYPE", "COLTYPESEQ", "COLTYPEDESC");
			//fillDocumentType("RFCOLLATERALTYPE", "COLTYPEID", "COLTYPEDESC");
			fillDocumentType("VW_PARAM_GENERAL_TBO_COLL", "COLTYPEID", "COLTYPEDESC");
		}

		private void fillFacilityList() 
		{
			//fillDocumentType("RFPRODUCT", "PRODUCTID", "PRODUCTDESC");
			fillDocumentType("VW_PARAM_GENERAL_TBO_FAC", "PRODUCTID", "PRODUCTDESC");
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

		private void viewPendingDocuments(string viewName, string columnId, string docTypeId) 
		{
			conn.QueryString = "select * from " + viewName + 
				" where " + 
				"DOCTYPEID = '" + docTypeId + "' and " + 
				columnId + " = '" + DDL_DOCTYPEDESC.SelectedValue + "'";
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
		
		/*
		private void viewPendingData() 
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_PENDING_TBOBYFAC " + 
				"where " + 
				"DOCTYPEID = '2' and " + 
				"PRODUCTID = '" + DDL_PRODUCT.SelectedValue + "'";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,3);
				dr[1] = conn.GetFieldValue(i,6);
				dr[2] = conn.GetFieldValue(i,5);
				dr[3] = getPendingStatus(conn.GetFieldValue(i,5));				
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
		*/

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
			if (DDL_DOCTYPEID.SelectedValue.ToString() == "1") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYUMUM", "''", "1");
			}
			else if(DDL_DOCTYPEID.SelectedValue.ToString() == "2") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYFAC", "PRODUCTID", "2");
			}
			else if (DDL_DOCTYPEID.SelectedValue.ToString() == "3") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYCOLL", "COLTYPEID", "3");
			}
		}

		private void performRequest(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				string mandatory = getStatusMandatory(DGRequest.Items[row].Cells[2].Text.Trim());

				if (DDL_DOCTYPEID.SelectedValue.ToString() == "1") 
				{
					conn.QueryString = "exec PARAM_GENERAL_TBOLIST_APPR '" + DDL_DOCTYPEID.SelectedValue + 
						"', null, null, '" + id + "','" + mandatory + "','1', '" + 
						Session["UserID"].ToString() + "','0' ";
				}
				else if(DDL_DOCTYPEID.SelectedValue.ToString() == "2") 
				{
					conn.QueryString = "exec PARAM_GENERAL_TBOLIST_APPR '" + DDL_DOCTYPEID.SelectedValue + 
						"', '" + DDL_DOCTYPEDESC.SelectedValue + "', null, '" + id + "','" + mandatory + "','1', '" + 
						Session["UserID"].ToString() + "','0' ";
				}
				else if (DDL_DOCTYPEID.SelectedValue.ToString() == "3") 
				{
					conn.QueryString = "exec PARAM_GENERAL_TBOLIST_APPR '" + DDL_DOCTYPEID.SelectedValue + 
						"', null, '" + DDL_DOCTYPEDESC.SelectedValue + "', '" + id + "','" + mandatory + "','1', '" + 
						Session["UserID"].ToString() + "','0'";
				}

				//conn.QueryString = "exec PARAM_GENERAL_TBOLIST_APPR '" + DDL_DOCTYPEID.SelectedValue + "','" + + "','','','','1'";
				conn.ExecuteQuery();
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				string mandatory = getStatusMandatory(DGRequest.Items[row].Cells[2].Text.Trim());

				if (DDL_DOCTYPEID.SelectedValue.ToString() == "1") 
				{
					conn.QueryString = "exec PARAM_GENERAL_TBOLIST_APPR '" + DDL_DOCTYPEID.SelectedValue + 
						"', null, null, '" + id + "','" + mandatory + "','0', '" + 
						Session["UserID"].ToString() + "','0' ";
				}
				else if(DDL_DOCTYPEID.SelectedValue.ToString() == "2") 
				{
					conn.QueryString = "exec PARAM_GENERAL_TBOLIST_APPR '" + DDL_DOCTYPEID.SelectedValue + 
						"', '" + DDL_DOCTYPEDESC.SelectedValue + "', null, '" + id + "','" + mandatory + "','0', '" + 
						Session["UserID"].ToString() + "','0' ";
				}
				else if (DDL_DOCTYPEID.SelectedValue.ToString() == "3") 
				{
					conn.QueryString = "exec PARAM_GENERAL_TBOLIST_APPR '" + DDL_DOCTYPEID.SelectedValue + 
						"', null, '" + DDL_DOCTYPEDESC.SelectedValue + "', '" + id + "','" + mandatory + "','0', '" + 
						Session["UserID"].ToString() + "','0' ";
				}

				/*
				conn.QueryString = "exec PARAM_GENERAL_TBOLIST_APPR '" + 
									DDL_PRODUCT.SelectedValue + "','" + 
									id + "', '0'";
				*/
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

			if (DDL_DOCTYPEID.SelectedValue.ToString() == "1") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYUMUM", "''", "1");
			}
			else if(DDL_DOCTYPEID.SelectedValue.ToString() == "2") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYFAC", "PRODUCTID", "2");
			}
			else if (DDL_DOCTYPEID.SelectedValue.ToString() == "3") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYCOLL", "COLTYPEID", "3");
			}		
			//viewPendingData();
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

		private void setDocumentType(bool isVisible) 
		{
			//LBL_DOCTYPEDESC.Visible = isVisible;
			DDL_DOCTYPEDESC.Visible = isVisible;
		}

		protected void DDL_DOCTYPEID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string doctypeid = DDL_DOCTYPEID.SelectedValue.ToString();
			DDL_DOCTYPEDESC.SelectedValue = "";

			switch (doctypeid) 
			{
				case "1":
					LBL_DOCTYPEDESC.Text = " ";
					setDocumentType(false);
					viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYUMUM", "''", "1");
					break;
				case "2":
					LBL_DOCTYPEDESC.Text = "Facility";
					setDocumentType(true);
					fillFacilityList();
					viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYFAC", "PRODUCTID", "2");
					break;
				case "3":
					LBL_DOCTYPEDESC.Text = "Collateral";
					setDocumentType(true);
					fillCollateralList();
					viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYCOLL", "COLTYPEID", "3");
					break;
				default:
					setDocumentType(false);					
					break;
			}
		}

		protected void DDL_DOCTYPEDESC_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (DDL_DOCTYPEID.SelectedValue.ToString() == "1") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYUMUM", "''", "1");
			}
			else if(DDL_DOCTYPEID.SelectedValue.ToString() == "2") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYFAC", "PRODUCTID", "2");
			}
			else if (DDL_DOCTYPEID.SelectedValue.ToString() == "3") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYCOLL", "COLTYPEID", "3");
			}			
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}
	}		
}
