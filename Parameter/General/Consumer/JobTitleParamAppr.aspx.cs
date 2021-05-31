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
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for JobTitleParamAppr.
	/// </summary>
	public partial class JobTitleParamAppr : System.Web.UI.Page
	{
		protected string mid;
		protected Connection conn;
		protected Connection conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);

			if(!IsPostBack)
			{								
				ViewData(); 				
			}
			else
			{
				InitialCon();
			}

			//BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
		}
		private void ViewData()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			InitialCon();

			BindData();			
			
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData()
		{
			conn.QueryString = "select JT_CODE,JT_DESC,JOB_TYPE_ID,CD_SIBS,CH_STA,JOB_ID_LAMA from TRFJOBTITLE";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG_APPR.DataSource = dt;

			try
			{
				DG_APPR.DataBind();
			}
			catch 
			{
				DG_APPR.CurrentPageIndex = DG_APPR.PageCount - 1;
				DG_APPR.DataBind();
			}

			for (int i = 0; i < DG_APPR.Items.Count; i++)
			{
				//DG_APPR.Items[i].Cells[0].Text = (i+1+(DG_APPR.CurrentPageIndex)*DG_APPR.PageSize).ToString();
				if (DG_APPR.Items[i].Cells[4].Text.Trim() == "1")
				{
					DG_APPR.Items[i].Cells[3].Text = "INSERT";
				}
				else if (DG_APPR.Items[i].Cells[4].Text.Trim() == "2")
				{
					DG_APPR.Items[i].Cells[3].Text = "UPDATE";
				}
				else if (DG_APPR.Items[i].Cells[4].Text.Trim() == "3")
				{
					DG_APPR.Items[i].Cells[3].Text = "DELETE";
				}				
			} 
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
			this.DG_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG_APPR_ItemCommand);
			this.DG_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_APPR_PageIndexChanged);

		}
		#endregion

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "RFJOBTITLE";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "01,'Job Title'";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}
		
		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DG_APPR.Items.Count; i++)
			{
				
				RadioButton rbA = (RadioButton) DG_APPR.Items[i].Cells[7].FindControl("rdo_Approve"),
					rbB = (RadioButton) DG_APPR.Items[i].Cells[8].FindControl("rdo_Reject"),
					rbC = (RadioButton) DG_APPR.Items[i].Cells[9].FindControl("rdo_Pending");					
				
				conn.QueryString = "select * from RFJOBTITLE where JT_CODE = '"+DG_APPR.Items[i].Cells[5].Text+"'";
				conn.ExecuteQuery();
				string JT_DESC_OLD = conn.GetFieldValue("JT_DESC");
				string JOB_TYPE_ID_OLD = conn.GetFieldValue("JOB_TYPE_ID");
				string CD_SIBS_OLD = conn.GetFieldValue("CD_SIBS");
				
				if(rbA.Checked == true)
				{
					if(DG_APPR.Items[i].Cells[4].Text.Trim() == "1")
					{
						ExecSPAuditTrail(DG_APPR.Items[i].Cells[5].Text,"JT_DESC","",DG_APPR.Items[i].Cells[1].Text,"1");
						ExecSPAuditTrail(DG_APPR.Items[i].Cells[5].Text,"JOB_TYPE_ID","",DG_APPR.Items[i].Cells[0].Text,"1");
						ExecSPAuditTrail(DG_APPR.Items[i].Cells[5].Text,"CD_SIBS","",DG_APPR.Items[i].Cells[2].Text,"1");

						conn.QueryString = "insert into RFJOBTITLE (JT_CODE,JT_DESC,JOB_TYPE_ID,CD_SIBS,CUST_TYPE,ACTIVE) "+
							"values ('"+DG_APPR.Items[i].Cells[5].Text+"','"+DG_APPR.Items[i].Cells[1].Text+"','"+DG_APPR.Items[i].Cells[0].Text+"','"+DG_APPR.Items[i].Cells[2].Text+"','A','1')";
						conn.ExecuteNonQuery();		

						conn.QueryString = "delete from TRFJOBTITLE where JT_CODE = '"+DG_APPR.Items[i].Cells[5].Text+"'";
						conn.ExecuteNonQuery();
						
					}
					else if(DG_APPR.Items[i].Cells[4].Text.Trim() == "2")
					{	
						if(DG_APPR.Items[i].Cells[1].Text!= JT_DESC_OLD.Trim())
						{
							ExecSPAuditTrail(DG_APPR.Items[i].Cells[5].Text,"JT_DESC",JT_DESC_OLD,DG_APPR.Items[i].Cells[1].Text,"0");
						}
						if(DG_APPR.Items[i].Cells[0].Text!= JOB_TYPE_ID_OLD.Trim())
						{
							ExecSPAuditTrail(DG_APPR.Items[i].Cells[5].Text,"JOB_TYPE_ID",JOB_TYPE_ID_OLD,DG_APPR.Items[i].Cells[0].Text,"0");
						}
						if(DG_APPR.Items[i].Cells[2].Text!= CD_SIBS_OLD.Trim())
						{
							ExecSPAuditTrail(DG_APPR.Items[i].Cells[5].Text,"CD_SIBS",CD_SIBS_OLD,DG_APPR.Items[i].Cells[2].Text,"0");
						}

						conn.QueryString = "UPDATE RFJOBTITLE set JT_CODE='"+DG_APPR.Items[i].Cells[5].Text+"',JT_DESC='"+DG_APPR.Items[i].Cells[1].Text+"',JOB_TYPE_ID='"+DG_APPR.Items[i].Cells[0].Text+"',"+
							"CD_SIBS = '"+DG_APPR.Items[i].Cells[2].Text+"',CUST_TYPE='A',ACTIVE='1' "+
							"where JT_CODE = '"+DG_APPR.Items[i].Cells[5].Text+"' and JOB_TYPE_ID = '"+DG_APPR.Items[i].Cells[6].Text+"'";
						conn.ExecuteNonQuery();		

						conn.QueryString = "delete from TRFJOBTITLE where JT_CODE = '"+DG_APPR.Items[i].Cells[5].Text+"'";
						conn.ExecuteNonQuery();
						
					}
					else if(DG_APPR.Items[i].Cells[4].Text.Trim() == "3")
					{						
						ExecSPAuditTrail(DG_APPR.Items[i].Cells[5].Text,"JT_DESC",DG_APPR.Items[i].Cells[1].Text,"","2");
						ExecSPAuditTrail(DG_APPR.Items[i].Cells[5].Text,"JOB_TYPE_ID",DG_APPR.Items[i].Cells[0].Text,"","2");
						ExecSPAuditTrail(DG_APPR.Items[i].Cells[5].Text,"CD_SIBS",DG_APPR.Items[i].Cells[2].Text,"","2");
						
						conn.QueryString = "Update RFJOBTITLE set ACTIVE='0' where JT_CODE = '"+DG_APPR.Items[i].Cells[5].Text+"'";
						conn.ExecuteNonQuery();
						conn.QueryString = "delete from TRFJOBTITLE where JT_CODE = '"+DG_APPR.Items[i].Cells[5].Text+"'";
						conn.ExecuteNonQuery();
						
					}
				}
				else if(rbB.Checked == true)
				{
					conn.QueryString = "delete from TRFJOBTITLE where JT_CODE = '"+DG_APPR.Items[i].Cells[5].Text+"'";
					conn.ExecuteNonQuery();
				}
			}	
			BindData();		
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc=9902040102&moduleID=40"); 
		}

		private void DG_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DG_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DG_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DG_APPR.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DG_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DG_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DG_APPR.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DG_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DG_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DG_APPR.Items[i].FindControl("rdo_Pending");
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

		private void DG_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_APPR.CurrentPageIndex = e.NewPageIndex;
			BindData();
		}
	}
}
