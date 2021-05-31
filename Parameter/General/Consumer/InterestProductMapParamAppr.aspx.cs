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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for InterestProductMapParamAppr.
	/// </summary>
	public partial class InterestProductMapParamAppr : System.Web.UI.Page
	{
		protected Connection conn2,conn;
		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			
			if (!IsPostBack)
			{
				LBL_ACTIVE.Text = "1";
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
		
		private void SetDBConn2()
		{
			conn.QueryString = "select * from RFMODULE where MODULEID=40";
			conn.ExecuteQuery();

			conn2 = new Connection ("Data Source=" + conn.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn.GetFieldValue("DB_NAMA") + ";uid=" +conn.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		void DGRequest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		private void viewPendingData()
		{
			conn2.QueryString="select productname,in_desc,a.productid,a.in_type,a.cd_sibs,ch_sta,case when ch_sta='0' then 'Edit' when ch_sta='1' then 'Insert' when ch_sta='2' then 'Delete' else '' end CH_STA1 from trfintprodmap a left join tproduct b on a.productid=b.productid left join rfinttype c on a.in_type=c.in_type";
			conn2.ExecuteQuery();
			DGRequest.DataSource = conn2.GetDataTable().Copy();
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
		private void performRequest(int row)
		{
			try 
			{
				string pr = DGRequest.Items[row].Cells[2].Text.Trim();
				string ins = DGRequest.Items[row].Cells[3].Text.Trim();
				string sibs = DGRequest.Items[row].Cells[4].Text.Trim();
				executeApproval(pr,ins,sibs,"1");
			}
			catch {}
				
		}

		private void deleteData(int row)
		{
			try 
			{
				string pr = DGRequest.Items[row].Cells[2].Text.Trim();
				string ins = DGRequest.Items[row].Cells[3].Text.Trim();
				string sibs = DGRequest.Items[row].Cells[4].Text.Trim();
				executeApproval(pr,ins,sibs,"0");
			}  
			catch {}
			
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "RFINTPRODMAP";
			conn2.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "01,'Interest Product Mapping'";
			conn2.ExecuteNonQuery();
			conn2.ClearData();
		}

		private void executeApproval(string prod, string ass, string sibs,string approvalFlag) 
		{
			int jumlah;
			string pendingStatus,myq;

			if (approvalFlag == "1") 
			{
				conn2.QueryString = "SELECT * FROM trfintprodmap WHERE PRODUCTID = '" +prod+ "' and IN_TYPE='"+ass+"'";
				conn2.ExecuteQuery();
				pendingStatus	= conn2.GetFieldValue("CH_STA");
				
				
				conn2.QueryString = "SELECT * FROM rfintprodmap WHERE PRODUCTID = '" +prod+ "' and IN_TYPE='"+ass+"'";
				conn2.ExecuteQuery();
				string PRODUCTID_OLD = conn2.GetFieldValue("PRODUCTID");
				string IN_TYPE_OLD = conn2.GetFieldValue("IN_TYPE");
				string CD_SIBS_OLD = conn2.GetFieldValue("CD_SIBS");
				string code = PRODUCTID_OLD+"|"+IN_TYPE_OLD;
								
				jumlah = conn2.GetRowCount();

				if (pendingStatus == "0" || pendingStatus == "1") 
				{
					if (jumlah>0)
					{
						if(prod.Trim()!= PRODUCTID_OLD.Trim())
						{
							ExecSPAuditTrail(code,"PRODUCTID",PRODUCTID_OLD,prod,"0");
						}
						if(ass.Trim()!= IN_TYPE_OLD.Trim())
						{
							ExecSPAuditTrail(code,"IN_TYPE",IN_TYPE_OLD,ass,"0");
						}
						if(sibs.Trim()!= CD_SIBS_OLD.Trim())
						{
							ExecSPAuditTrail(code,"CD_SIBS",CD_SIBS_OLD,sibs,"0");
						}

						myq = "update rfintprodmap set cd_sibs='"+sibs+"' where productid='"+prod+"' and IN_TYPE='"+ass+"'";
					}
					else
					{
						code = prod+"|"+ass;
						ExecSPAuditTrail(code,"PRODUCTID","",prod,"1");
						ExecSPAuditTrail(code,"IN_TYPE","",ass,"1");
						ExecSPAuditTrail(code,"CD_SIBS","",sibs,"1");

						myq = "INSERT INTO rfintprodmap VALUES ('"+prod+"', '"+ass+"','"+sibs+"','1')";
//						if(LBL_ACTIVE.Text.Trim() == "1")
//							myq = "INSERT INTO rfintprodmap VALUES ('"+prod+"', '"+ass+"','"+sibs+"')";
//						else
//							myq = "INSERT INTO rfintprodmap VALUES ('"+prod+"', '"+ass+"','"+sibs+"')";
					}
				
					conn2.QueryString=myq;
					conn2.ExecuteNonQuery();
				
				}
				else if (pendingStatus == "2") 
				{
					if (jumlah > 0) 
					{
						ExecSPAuditTrail(code,"PRODUCTID",prod,"","2");
						ExecSPAuditTrail(code,"IN_TYPE",ass,"","2");
						ExecSPAuditTrail(code,"CD_SIBS",sibs,"","2");

						myq = "UPDATE rfintprodmap SET ACTIVE='0' WHERE PRODUCTID = '" +prod+ "' and IN_TYPE='"+ass+"'";
//						if (LBL_ACTIVE.Text.Trim() == "1")
//							myq = "DELETE FROM rfintprodmap WHERE PRODUCTID = '" +prod+ "' and IN_TYPE='"+ass+"'";
//						else
//							myq = "DELETE FROM rfintprodmap WHERE PRODUCTID = '" +prod+ "' and IN_TYPE='"+ass+"'";
						
						conn2.QueryString=myq;
						conn2.ExecuteNonQuery();
					}
				}
			}

			myq = "DELETE FROM Trfintprodmap WHERE PRODUCTID = '" +prod+ "' and IN_TYPE='"+ass+"'";
			conn2.QueryString=myq;
			conn2.ExecuteNonQuery();
			
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
			this.viewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../HostParamApproval.aspx?ModuleID=40");
		}
	}
}
