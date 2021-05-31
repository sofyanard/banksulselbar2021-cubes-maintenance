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
	/// Summary description for InsBasedOnProdParamAppr.
	/// </summary>
	public partial class InsBasedOnProdParamAppr : System.Web.UI.Page
	{
		protected Connection conn,conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
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

		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();
			
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		void DGRequest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}
		private void viewPendingData()
		{
			conn.QueryString = "select productname,ass_desc,a.productid,a.ass_code,ch_sta,case when ch_sta='0' then 'UPDATE' when ch_sta='1' then 'INSERT' when ch_sta='2' then 'DELETE' else '' end CH_STA1 from trfprodasur a left join tproduct b on a.productid=b.productid left join rfasstype c on a.ass_code=c.ass_code";
			conn.ExecuteQuery();

			DGRequest.DataSource = conn.GetDataTable().Copy();
			
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
				string asc = DGRequest.Items[row].Cells[3].Text.Trim();
				
				executeApproval(pr,asc,"1");
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
				string pr = DGRequest.Items[row].Cells[2].Text.Trim();
				string asc = DGRequest.Items[row].Cells[3].Text.Trim();
				
				executeApproval(pr,asc,"0");
			} 
			catch { GlobalTools.popMessage(this,"Data cannot be deleted into database"); }
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "RFPRODASUR";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'Insurance Based On Product'";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}

		private void executeApproval(string prod, string ass, string approvalFlag) 
		{
			int jumlah;
			string pendingStatus,myq;
			string userid = Session["UserID"].ToString();

			if (approvalFlag == "1") 
			{
				conn.QueryString = "SELECT * FROM TRFPRODASUR WHERE PRODUCTID = '" +prod+ "' and ASS_CODE='"+ass+"'";
				conn.ExecuteQuery();
				string PRODUCTID_OLD = conn.GetFieldValue("PRODUCTID");
				string ASS_CODE_OLD = conn.GetFieldValue("ASS_CODE");
				string code = PRODUCTID_OLD+"|"+ASS_CODE_OLD;
				

				pendingStatus	= conn.GetFieldValue("CH_STA");

				conn.QueryString ="exec PARAM_GENERAL_PRODUCT_INSURANCE '"+PRODUCTID_OLD+"','"+ASS_CODE_OLD+"','"+pendingStatus+"','"+userid+"'";
				conn.ExecuteNonQuery();

				conn.QueryString = "SELECT * FROM RFPRODASUR WHERE PRODUCTID = '" +prod+ "' and ASS_CODE='"+ass+"'";
				conn.ExecuteQuery();
				

				jumlah = conn.GetRowCount();

				if (pendingStatus == "0" || pendingStatus == "1") 
				{
					if (jumlah > 0) 
					{
//						if(prod.Trim()!= PRODUCTID_OLD.Trim())
//						{
//							ExecSPAuditTrail(code,"PRODUCTID",PRODUCTID_OLD,prod,"0");
//						}
//						if(ass.Trim()!= ASS_CODE_OLD.Trim())
//						{
//							ExecSPAuditTrail(code,"ASS_CODE",ASS_CODE_OLD,ass,"0");
//						}

						myq = "UPDATE RFPRODASUR SET ASS_CODE='"+ass+"' WHERE PRODUCTID = '" +prod+ "'";
						
						conn.QueryString=myq;
						conn.ExecuteQuery();
					}
					else 
					{
//						ExecSPAuditTrail(code,"PRODUCTID","",prod,"1");
//						ExecSPAuditTrail(code,"ASS_CODE","",ass,"1");

//						if(LBL_ACTIVE.Text.Trim() == "1")
//							myq = "INSERT INTO RFPRODASUR VALUES ('"+prod+"', '"+ass+"', '1')";
//						else
//							myq = "INSERT INTO RFPRODASUR VALUES ('"+prod+"', '"+ass+"',null)";

						myq = "INSERT INTO RFPRODASUR VALUES ('"+prod+"', '"+ass+"', '1')";
						conn.QueryString=myq;
						conn.ExecuteQuery();
					}
					
				}
				else if (pendingStatus == "2") 
				{
					if (jumlah > 0) 
					{
//						ExecSPAuditTrail(code,"PRODUCTID",prod,"","2");
//						ExecSPAuditTrail(code,"ASS_CODE",ass,"","2");

//						if (LBL_ACTIVE.Text.Trim() == "1")
//							myq = "DELETE FROM RFPRODASUR WHERE PRODUCTID = '" +prod+ "' and ASS_CODE='"+ass+"'";
//						else
//							myq = "DELETE FROM RFPRODASUR WHERE PRODUCTID = '" +prod+ "' and ASS_CODE='"+ass+"'";

						myq = "UPDATE RFPRODASUR SET ACTIVE='0' WHERE PRODUCTID = '" +prod+ "' and ASS_CODE='"+ass+"'";
						conn.QueryString = myq;
						conn.ExecuteQuery();
					}
				}
			}

			myq = "DELETE FROM TRFPRODASUR WHERE PRODUCTID = '" +prod+ "' and ASS_CODE = '"+ass+"'";
			
			conn.QueryString = myq;
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
