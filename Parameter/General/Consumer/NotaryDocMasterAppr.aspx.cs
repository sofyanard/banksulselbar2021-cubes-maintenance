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
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for NotaryDocMasterAppr.
	/// </summary>
	public partial class NotaryDocMasterAppr : System.Web.UI.Page
	{
		protected Connection conn,conn2;
		//protected Connection conn2= new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected int digits;
		protected int digits2;
		protected int digits3;
		protected string zeros;
		protected string zeros2;
		protected string codes;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				LBL_ACTIVE.Text = Request.QueryString["active"];
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

				viewPendingData();
				//CodeGen();
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
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion
		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();

			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" + conn2.GetFieldValue("DB_LOGINID") + ";pwd=" + conn2.GetFieldValue("DB_LOGINPWD") + ";Pooling=true");
		}
		private void CodeGen()
		{
			//menentukan code berikutnya
			//cek dulu apa sudah ada di pending, trus bisa ke master

			conn.QueryString="select convert(int,isnull(max(dnm_code),0)) from tRFDOCNOTARY_MASTER";
			conn.ExecuteQuery();
			
			zeros = conn.GetFieldValue(0,0);
			if (zeros=="0")
			{
				conn.QueryString="select convert(int,isnull(max(dnm_code),0)) from RFDOCNOTARY_MASTER where active=1";
				conn.ExecuteQuery();
				zeros = conn.GetFieldValue(0,0);
			}

			/*conn.QueryString="select convert(int,isnull(max(dnm_code),0)) from RFDOCNOTARY_MASTER where active=1";
			conn.ExecuteQuery();
			zeros = conn2.GetFieldValue(0,0);*/
				
			digits = zeros.Length;
			digits2 = Int32.Parse(zeros)+1;
			zeros2 = digits2.ToString();
			codes = digits2.ToString();
			for (digits3 = zeros2.Length;digits3<5;digits3++)
			{
				codes='0' + codes;
			}
			lbl_max.Text=codes;
		}
		private void viewPendingData() 
		{
			string tableName = Request.QueryString["tablename"];

			conn.QueryString = "select * from tRFDOCNOTARY_MASTER";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("DNM_CODE"));
			dt.Columns.Add(new DataColumn("DNM_DESC"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,"DNM_CODE");
				dr[1] = conn.GetFieldValue(i,"DNM_DESC");
				dr[2] = conn.GetFieldValue(i,"CH_STA");
				dr[3] = getPendingStatus(conn.GetFieldValue(i,"CH_STA"));
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
				string id =  DGRequest.Items[row].Cells[0].Text.Trim();				
				executeApproval(id, "1");
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string id = DGRequest.Items[row].Cells[0].Text.Trim();
				executeApproval(id, "0");
			} 
			catch {}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "RFDOCNOTARY_MASTER";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'Notary Document Master'";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}

		private void executeApproval(string id, string approvalFlag) 
		{
			string desc, pendingStatus;
			string desc_old;
			int jumlah;

			if (approvalFlag == "1") 
			{
				conn.QueryString = "SELECT * FROM tRFDOCNOTARY_MASTER WHERE DNM_CODE = '" +id+ "'";
				conn.ExecuteQuery();
				pendingStatus	= conn.GetFieldValue("CH_STA");
				desc			= conn.GetFieldValue(0,1);

				conn.QueryString = "SELECT * FROM RFDOCNOTARY_MASTER WHERE DNM_CODE = '" +id+ "'";
				conn.ExecuteQuery();

				jumlah = conn.GetRowCount();	
		
				conn.QueryString = "SELECT DNM_DESC FROM RFDOCNOTARY_MASTER WHERE DNM_CODE = '" +id+ "'";
				conn.ExecuteQuery();
				desc_old = conn.GetFieldValue("DNM_DESC");

				if (pendingStatus == "0" || pendingStatus == "1") 
				{
					if (jumlah > 0) 
					{
						if(desc.Trim()!= desc_old.Trim())
						{
							ExecSPAuditTrail(id,"DNM_DESC",desc_old,desc,"0");
						}

						conn.QueryString = "UPDATE RFDOCNOTARY_MASTER SET DNM_DESC = '"+desc+"' WHERE DNM_CODE = '"+id+"'";
						conn.ExecuteQuery();
					}
					else 
					{
						ExecSPAuditTrail(id,"DNM_CODE","",id,"1");
						ExecSPAuditTrail(id,"DNM_DESC","",desc,"1");

						//id = lbl_max.Text.Trim(); //id baru dari codegen
						if(LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "INSERT INTO RFDOCNOTARY_MASTER VALUES ('"+id+"', '"+desc+"', '1')";
						else
							conn.QueryString = "INSERT INTO RFDOCNOTARY_MASTER VALUES ('"+id+"', '"+desc+"', '')";
						conn.ExecuteQuery();
						//CodeGen();
					}

					conn.QueryString = "DELETE FROM tRFDOCNOTARY_MASTER WHERE DNM_CODE = '" +id+ "'";
					conn.ExecuteQuery();
				}
				else if (pendingStatus == "2") 
				{
					if (jumlah > 0) 
					{
						ExecSPAuditTrail(id,"DNM_CODE",id,"","2");
						ExecSPAuditTrail(id,"DNM_DESC",desc,"","2");

						/*if (LBL_ACTIVE.Text.Trim() == "1")
							conn.QueryString = "DELETE FROM RFDOCNOTARY_MASTER WHERE DNM_CODE= '"+id+"'";
						else*/
							conn.QueryString = "UPDATE RFDOCNOTARY_MASTER SET ACTIVE = '0' WHERE DNM_CODE = '"+id+"'";
						conn.ExecuteQuery();
						//CodeGen();
					}
				}
			}

			conn.QueryString = "DELETE FROM tRFDOCNOTARY_MASTER WHERE DNM_CODE = '" +id+ "'";
			conn.ExecuteQuery();
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

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGRequest.Items.Count; i++)
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
			viewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleId=40");
		}
	}
}
