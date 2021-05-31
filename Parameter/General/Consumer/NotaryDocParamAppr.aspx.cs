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
	/// Summary description for NotaryDocParamAppr.
	/// </summary>
	public partial class NotaryDocParamAppr : System.Web.UI.Page
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
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion
		
		void DGRequest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		private void SetDBConn2()
		{
			conn.QueryString = "select * from RFMODULE where MODULEID=40";
			conn.ExecuteQuery();
			
			conn2 = new Connection ("Data Source=" + conn.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn.GetFieldValue("DB_NAMA") + ";uid=" + conn.GetFieldValue("DB_LOGINID") + ";pwd=" + conn.GetFieldValue("DB_LOGINPWD") + ";Pooling=true");
		}

		private void viewPendingData()
		{
			conn2.QueryString="select b.pr_desc,c.productname,DNM_DESC,DND_DESC,MANDATORY1 = case DN_MANDATORY when '1' then 'Yes' else 'No' end, CH_STA1 = case CH_STA when '0' then 'Update' when '1' then 'Insert'when '2' then 'Delete' else '' end, CH_STA,b.pr_code,c.productid,a.DNM_CODE,a.DND_CODE,DN_MANDATORY from trfdocnotary a left join program b on a.pr_code=b.pr_code left join tproduct c on a.productid=c.productid left join RFDOCNOTARY_DETAIL DND on a.DND_CODE = DND.DND_CODE left join RFDOCNOTARY_MASTER DNM on a.DNM_CODE = DNM.DNM_CODE";
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
				string pr   = DGRequest.Items[row].Cells[7].Text.Trim();
				string pd   = DGRequest.Items[row].Cells[8].Text.Trim();
				string md   = DGRequest.Items[row].Cells[11].Text.Trim();
				string doc  = DGRequest.Items[row].Cells[10].Text.Trim();

				executeApproval(pr,pd,md,doc,"1");
			}
			catch 
			{
				GlobalTools.popMessage(this,"Data cannot be entered into database");
			}
		}

		private void deleteData(int row)
		{
			try 
			{
				string pr   = DGRequest.Items[row].Cells[7].Text.Trim();
				string pd   = DGRequest.Items[row].Cells[8].Text.Trim();
				string md   = DGRequest.Items[row].Cells[11].Text.Trim();
				string doc  = DGRequest.Items[row].Cells[10].Text.Trim();
				
				executeApproval(pr,pd,md,doc,"0");
			} 
			catch { GlobalTools.popMessage(this,"Data cannot be deleted into database"); }
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "RFDOCNOTARY";
			conn2.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'Notary Document'";
			conn2.ExecuteNonQuery();
			conn2.ClearData();
		}

		private void executeApproval(string pr, string pd, string md, string doc,string approvalFlag) 
		{
			int jumlah;
			string pendingStatus,myq;

			if (approvalFlag == "1") 
			{
				conn2.QueryString = "SELECT * FROM trfdocnotary where pr_code='"+pr+"' "+ 
					"and productid='"+pd+"' and dnd_code='"+doc+"'";
				conn2.ExecuteQuery();
				
				pendingStatus	= conn2.GetFieldValue("CH_STA");

				conn2.QueryString = "SELECT * FROM rfdocnotary where pr_code='"+pr+"' "+ 
					"and productid='"+pd+"' and dnd_code='"+doc+"'";
				conn2.ExecuteQuery();

				jumlah = conn2.GetRowCount();
				string md_old = conn2.GetFieldValue("dn_mandatory");
				string code = pr+"|"+pd+"|"+doc;

				if (pendingStatus == "0" || pendingStatus == "1") 
				{
					if (jumlah > 0) 
					{
						if(md.Trim()!= md_old.Trim())
						{
							ExecSPAuditTrail(code,"DN_MANDATORY",md_old,md,"0");
						}

						myq = "UPDATE rfdocnotary SET dn_mandatory='"+md+"' where pr_code='"+pr+"' and active='1' "+ 
							"and productid='"+pd+"' and dnd_code='"+doc+"'";
						conn2.QueryString=myq;
						conn2.ExecuteQuery();
					}
					else 
					{
						ExecSPAuditTrail(code,"PR_CODE","",pr,"1");
						ExecSPAuditTrail(code,"PRODUCTID","",pd,"1");
						ExecSPAuditTrail(code,"DND_CODE","",doc,"1");
						ExecSPAuditTrail(code,"DN_MANDATORY","",md,"1");

						if(LBL_ACTIVE.Text.Trim() == "1")
							myq = "INSERT INTO rfdocnotary VALUES ('"+pr+"', '"+pd+"','"+doc+"','"+md+"','1')";
						else
							myq = "INSERT INTO rfdocnotary VALUES ('"+pr+"', '"+pd+"','"+doc+"','"+md+"','')";
                        
						conn2.QueryString=myq;
						conn2.ExecuteQuery();
					}
				}
				else if (pendingStatus == "2") 
				{
					if (jumlah > 0) 
					{
						ExecSPAuditTrail(code,"PR_CODE",pr,"","2");
						ExecSPAuditTrail(code,"PRODUCTID",pd,"","2");
						ExecSPAuditTrail(code,"DND_CODE",doc,"","2");
						ExecSPAuditTrail(code,"DN_MANDATORY",md,"","2");

						myq = "UPDATE rfdocnotary SET ACTIVE='0' where pr_code='"+pr+"' "+ 
								"and productid='"+pd+"' and dnd_code='"+doc+"'";
						
						conn2.QueryString=myq;
						conn2.ExecuteQuery();
					}
				}
			}			

			myq = "DELETE FROM trfdocnotary where pr_code='"+pr+"' "+ 
				"and productid='"+pd+"' and dnd_code='"+doc+"'";
			conn2.QueryString=myq;
			conn2.ExecuteQuery();
			
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
