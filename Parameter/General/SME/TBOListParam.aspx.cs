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
	/// Summary description for TBOListParam.
	/// </summary>
	public partial class TBOListParam : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.WebControls.Button updatestatus;
		protected Tools tool = new Tools();
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);//(Connection) Session["Connection"];
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

		//if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
		//		Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack) 
			{
				fillDocumentType();
				//fillFacilityList();
				fillDocumentList();
				LBL_SAVEMODE.Text = "1";
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			DGDocRequest.PageIndexChanged += new DataGridPageChangedEventHandler(DGDocRequest_PageIndexChanged);
			DGDocuments.PageIndexChanged +=new DataGridPageChangedEventHandler(DGDocuments_PageIndexChanged);
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
			this.DGDocuments.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGDocuments_ItemCommand);
			this.DGDocRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGDocRequest_ItemCommand);
			this.DGDocRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGDocRequest_PageIndexChanged);

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
			//fillDocumentType("RFCOLLATERALTYPE", "COLTYPEID", "COLTYPEDESC");
			fillDocumentType("VW_PARAM_GENERAL_TBO_COLL", "COLTYPEID", "COLTYPEDESC");
		}

		private void fillFacilityList() 
		{
			//fillDocumentType("RFPRODUCT", "PRODUCTID", "PRODUCTDESC");
			fillDocumentType("VW_PARAM_GENERAL_TBO_FAC", "PRODUCTID", "PRODUCTDESC");
		}

		private void fillDocumentList() 
		{
			DDL_DOCUMENT.Items.Add(new ListItem("- PILIH -", ""));

			//conn.QueryString = "select DOCID, DOCDESC from RFTBODOC where ACTIVE = '1'";
			conn.QueryString = "select DOCID, DOCDESC from VW_PARAM_GENERAL_TBO_DOC where ACTIVE = '1'";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_DOCUMENT.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
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

		private string getStatusMandatory(string val) 
		{
			string mandatory = "";
			switch (val) 
			{
				case "0":
					mandatory = "No";
					break;
				case "1":
					mandatory = "Yes";
					break;
				default:
					break;
			}
			return mandatory;
		}		

		private void viewPendingDocuments(string viewName, string columnId, string docTypeId) 
		{
			/*
			conn.QueryString = "select * from VW_PARAM_GENERAL_PENDING_TBOBYFAC " + 
								"where " + 
									"DOCTYPEID = '2' and " + 
									"PRODUCTID = '" + DDL_PRODUCT.SelectedValue + "'";
			*/


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

			DGDocRequest.DataSource = new DataView(dt);
			try 
			{
				DGDocRequest.DataBind();
			}
			catch 
			{
				DGDocRequest.CurrentPageIndex = DGDocRequest.PageCount - 1;
				DGDocRequest.DataBind();
			}
		}

		private void viewDocuments() 
		{
			//conn.QueryString = "select * from TBOLIST where DOCTYPEID = '2' and PRODUCTID = '" + DDL_PRODUCT.SelectedValue + "'";
			//conn.QueryString = "select * from VW_PARAM_GENERAL_TBOBYFAC where productid = '" + DDL_PRODUCT.SelectedValue + "'";
			if (DDL_DOCTYPEID.SelectedValue.ToString() == "1") 
			{
				conn.QueryString = "select * from VW_PARAM_GENERAL_TBOBYUMUM";
			}
			else if(DDL_DOCTYPEID.SelectedValue.ToString() == "2") 
			{
				conn.QueryString = "select * from VW_PARAM_GENERAL_TBOBYFAC where productid = '" + DDL_DOCTYPEDESC.SelectedValue + "'";
			}
			else if (DDL_DOCTYPEID.SelectedValue.ToString() == "3") 
			{
				conn.QueryString = "select * from VW_PARAM_GENERAL_TBOBYCOLL where COLTYPEID = '" + DDL_DOCTYPEDESC.SelectedValue + "'";
			}

			conn.ExecuteQuery();			

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("MANDATORY"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,5);
				dr[1] = conn.GetFieldValue(i,7);
				dr[2] = getStatusMandatory(conn.GetFieldValue(i, 6).ToString());
				dt.Rows.Add(dr);
			}			

			DGDocuments.DataSource = new DataView(dt);
			try 
			{
				DGDocuments.DataBind();
			}
			catch  
			{
				DGDocuments.CurrentPageIndex = DGDocuments.PageCount - 1;
				DGDocuments.DataBind();
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			//-------- mencari apakah dokumen yang ingin ditambah sudah ada -----------------
			for(int i=0; i<DGDocuments.Items.Count; i++) 
			{
				if (DGDocuments.Items[i].Cells[0].Text == DDL_DOCUMENT.SelectedValue) 
				{
					Tools.popMessage(this, "Document sudah ada");
					return;
				}
			}
			//--------------------------------------------------------------------------------
			
			/*
			conn.QueryString = "exec PARAM_GENERAL_TBOLIST_MAKER '" + 
									DDL_PRODUCT.SelectedValue + "','" + 
									DDL_DOCUMENT.SelectedValue + "','" + 
									LBL_SAVEMODE.Text.Trim() + "'";
			*/
			string mandatory = "";
			if (RDO_YES.Checked) mandatory = "1";
			if (RDO_NO.Checked) mandatory = "0";

			if (DDL_DOCTYPEID.SelectedValue.ToString() == "1") 
			{
				conn.QueryString = "exec PARAM_GENERAL_TBOLIST_MAKER '" + DDL_DOCTYPEID.SelectedValue + 
										"', null, null, '" + DDL_DOCUMENT.SelectedValue + "','" + mandatory + "','" + LBL_SAVEMODE.Text + "'";
			}
			else if(DDL_DOCTYPEID.SelectedValue.ToString() == "2") 
			{
				conn.QueryString = "exec PARAM_GENERAL_TBOLIST_MAKER '" + DDL_DOCTYPEID.SelectedValue + 
					"', '" + DDL_DOCTYPEDESC.SelectedValue + "', null, '" + DDL_DOCUMENT.SelectedValue + "','" + mandatory + "','" + LBL_SAVEMODE.Text + "'";
			}
			else if (DDL_DOCTYPEID.SelectedValue.ToString() == "3") 
			{
				conn.QueryString = "exec PARAM_GENERAL_TBOLIST_MAKER '" + DDL_DOCTYPEID.SelectedValue + 
					"', null, '" + DDL_DOCTYPEDESC.SelectedValue + "', '" + DDL_DOCUMENT.SelectedValue + "','" + mandatory + "','" + LBL_SAVEMODE.Text + "'";
			}

			conn.ExecuteNonQuery();
			if (DDL_DOCTYPEID.SelectedValue.ToString() == "1") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYUMUM", "''", "1");
				DDL_DOCTYPEDESC.SelectedValue = "";
			}
			else if(DDL_DOCTYPEID.SelectedValue.ToString() == "2") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYFAC", "PRODUCTID", "2");
			}
			else if (DDL_DOCTYPEID.SelectedValue.ToString() == "3") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYCOLL", "COLTYPEID", "3");
			}

			//viewPendingDocuments();			

			LBL_SAVEMODE.Text = "1";
		}

		private void DGDocuments_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DGDocuments.CurrentPageIndex = e.NewPageIndex;
			viewDocuments();
		}

		private void DGDocuments_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":					
					/*
					conn.QueryString = "exec PARAM_GENERAL_TBOLIST_MAKER '" + 
										DDL_PRODUCT.SelectedValue + "','" + 
										e.Item.Cells[0].Text + "','2'";
					*/
					string mandatory = "";
					if (e.Item.Cells[2].Text.ToUpper() == "YES") mandatory = "1";
					else mandatory = "0";

					if (DDL_DOCTYPEID.SelectedValue.ToString() == "1") 
					{
						conn.QueryString = "exec PARAM_GENERAL_TBOLIST_MAKER '" + DDL_DOCTYPEID.SelectedValue + 
							"', null, null, '" + e.Item.Cells[0].Text + "','" + mandatory + "','2'";
					}
					else if(DDL_DOCTYPEID.SelectedValue.ToString() == "2") 
					{
						conn.QueryString = "exec PARAM_GENERAL_TBOLIST_MAKER '" + DDL_DOCTYPEID.SelectedValue + 
							"', '" + DDL_DOCTYPEDESC.SelectedValue + "', null, '" + e.Item.Cells[0].Text + "','" + mandatory + "','2'";
					}
					else if (DDL_DOCTYPEID.SelectedValue.ToString() == "3") 
					{
						conn.QueryString = "exec PARAM_GENERAL_TBOLIST_MAKER '" + DDL_DOCTYPEID.SelectedValue + 
							"', null, '" + DDL_DOCTYPEDESC.SelectedValue + "', '" + e.Item.Cells[0].Text + "','" + mandatory + "','2'";
					}

					conn.ExecuteQuery();
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
					break;
				default:
					break;
			}
		}

		private void DGDocRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGDocRequest.CurrentPageIndex = e.NewPageIndex;

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

		private void DGDocRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					/*
					conn.QueryString = "delete from PENDING_TBOLIST " + 
										"where " + 
											"DOCTYPEID = '2' AND " + 
											"PRODUCTID = '" + DDL_PRODUCT.SelectedValue +
											"' and DOCID = '" + e.Item.Cells[0].Text + "'";
					*/

					if (DDL_DOCTYPEID.SelectedValue.ToString() == "1") 
					{
						conn.QueryString = "delete from PENDING_TBOLIST " + 
											"where " + 
											"DOCTYPEID = '1' AND " + 
											"PRODUCTID IS NULL AND " + 
											"COLTYPEID IS NULL AND " +
											"DOCID = '" + e.Item.Cells[0].Text + "'";
					}
					else if(DDL_DOCTYPEID.SelectedValue.ToString() == "2") 
					{
						conn.QueryString = "delete from PENDING_TBOLIST " + 
											"where " + 
											"DOCTYPEID = '2' AND " + 
											"COLTYPEID IS NULL AND " +
											"PRODUCTID = '" + DDL_DOCTYPEDESC.SelectedValue +
											"' and DOCID = '" + e.Item.Cells[0].Text + "'";
					}
					else if (DDL_DOCTYPEID.SelectedValue.ToString() == "3") 
					{
						conn.QueryString = "delete from PENDING_TBOLIST " + 
											"where " + 
											"DOCTYPEID = '3' AND " + 
											"PRODUCTID IS NULL AND " +
											"COLTYPEID = '" + DDL_DOCTYPEDESC.SelectedValue +
											"' and DOCID = '" + e.Item.Cells[0].Text + "'";
					}
					conn.ExecuteQuery();

					if (DDL_DOCTYPEID.SelectedValue.ToString() == "1") 
					{
						viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYUMUM", "1", "1");
					}
					else if(DDL_DOCTYPEID.SelectedValue.ToString() == "2") 
					{
						viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYFAC", "PRODUCTID", "2");
					}
					else if (DDL_DOCTYPEID.SelectedValue.ToString() == "3") 
					{
						viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYCOLL", "COLTYPEID", "3");
					}

					break;
				default:
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

			if (DDL_DOCTYPEID.SelectedValue == "")
			return;

			DDL_DOCUMENT.Enabled = true;
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
					DDL_DOCUMENT.Enabled = false;
					break;
			}
			viewDocuments();

		}

		protected void DDL_DOCTYPEDESC_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (DDL_DOCTYPEID.SelectedValue.ToString() == "1") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYUMUM", "1", "1");
			}
			else if(DDL_DOCTYPEID.SelectedValue.ToString() == "2") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYFAC", "PRODUCTID", "2");
			}
			else if (DDL_DOCTYPEID.SelectedValue.ToString() == "3") 
			{
				viewPendingDocuments("VW_PARAM_GENERAL_PENDING_TBOBYCOLL", "COLTYPEID", "3");
			}

			viewDocuments();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		protected void DGDocRequest_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}		
}
