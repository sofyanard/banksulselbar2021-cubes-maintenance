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
	/// Summary description for TBORefParamAppr.
	/// </summary>
	public partial class TBORefParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid;
	
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
		}

		private void ViewData()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select db_ip, db_nama,DB_LOGINID,DB_LOGINPWD from rfmodule where moduleid = '"+mid+"'";
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
			/*20070716 change by sofyan, for Consumer Enhancement
			conn.QueryString = "select DOC_ID, TBO_DESC, CD_SIBS, FLAG = case SIBS_FLAG when '1' then 'Yes' else 'No' end, "+
				"CH_STA, STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' when '3' then 'DELETE' end from TRFTBO";
			*/
			conn.QueryString = "exec PARAM_TBOREF_VIEWREQUEST";
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

		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void deleteData(int row)
		{
			try 
			{
				string cd = DG_APPR.Items[row].Cells[0].Text.Trim();
				
				conn.QueryString = "DELETE FROM TRFTBO WHERE DOC_ID = '"+cd+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "RFTBO";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'TBO Referensi' ";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}

		private void performRequest(int row)
		{
			int rst = 0;
			string flag = "";
			
			string code = DG_APPR.Items[row].Cells[0].Text.Trim();
			string desc =  cleansText(DG_APPR.Items[row].Cells[1].Text);
			string sbc =  cleansText(DG_APPR.Items[row].Cells[2].Text);
			string expdur =  cleansText(DG_APPR.Items[row].Cells[4].Text);
			string status = DG_APPR.Items[row].Cells[6].Text.Trim();

			if(DG_APPR.Items[row].Cells[3].Text.Trim() == "Yes")
				flag = "1";
			else
				flag = "0";

			rst = row;

			conn.QueryString = "select * from RFTBO where DOC_ID = '"+code+"'";
			conn.ExecuteQuery();
			string DOC_ID = conn.GetFieldValue("DOC_ID");
			string TBO_DESC= conn.GetFieldValue("TBO_DESC");
			string CD_SIBS = conn.GetFieldValue("CD_SIBS");
			string SIBS_FLAG = conn.GetFieldValue("SIBS_FLAG");
			string EXPDURATION = conn.GetFieldValue("EXPDURATION");
			
			if (status.Equals("1"))
			{
				try
				{
					ExecSPAuditTrail(code,"DOC_ID","",code,"1");
					ExecSPAuditTrail(code,"TBO_DESC","",desc,"1");
					ExecSPAuditTrail(code,"CD_SIBS","",sbc,"1");
					ExecSPAuditTrail(code,"SIBS_FLAG","",flag,"1");
					ExecSPAuditTrail(code,"EXPDURATION","",expdur,"1");

					/*20070716 change by sofyan, for Consumer Enhancement
					conn.QueryString = "INSERT INTO RFTBO VALUES('"+code+"', "+GlobalTools.ConvertNull(desc)+", "+GlobalTools.ConvertNull(sbc)+", 1, '"+flag+"')";
					*/
					conn.QueryString = "exec PARAM_TBOREF_APPRUPDATE 1, '" + code+"'";
					conn.ExecuteQuery();			
				}
				catch(Exception Ex)
				{
					string errmsg = Ex.Message.Replace("'","");
					if (errmsg.IndexOf("Last Query:") > 0)		
						errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
					GlobalTools.popMessage(this, errmsg);
				}
				finally
				{
					deleteData(rst);
				}
				
			}

			if (status.Equals("2"))
			{
				try
				{
					if(code.Trim()!= DOC_ID.Trim())
					{
						ExecSPAuditTrail(code,"DOC_ID",DOC_ID,code,"0");
					}
					if(desc.Trim()!= TBO_DESC.Trim())
					{
						ExecSPAuditTrail(code,"TBO_DESC",TBO_DESC,desc,"0");
					}
					if(sbc.Trim()!= CD_SIBS.Trim())
					{
						ExecSPAuditTrail(code,"CD_SIBS",CD_SIBS,sbc,"0");
					}
					if(flag.Trim()!= SIBS_FLAG.Trim())
					{
						ExecSPAuditTrail(code,"SIBS_FLAG",SIBS_FLAG,flag,"0");
					}

					/*20070716 change by sofyan, for Consumer Enhancement
					conn.QueryString = "UPDATE RFTBO SET TBO_DESC = "+GlobalTools.ConvertNull(desc)+", "+
						"CD_SIBS = "+GlobalTools.ConvertNull(sbc)+", SIBS_FLAG = '"+flag+"' "+
						"where DOC_ID = '"+code+"'";
					*/
					conn.QueryString = "exec PARAM_TBOREF_APPRUPDATE 2, '" + code+"'";
					conn.ExecuteQuery();
				}
				catch(Exception Ex)
				{
					string errmsg = Ex.Message.Replace("'","");
					if (errmsg.IndexOf("Last Query:") > 0)		
						errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
					GlobalTools.popMessage(this, errmsg);
				}
				finally
				{
					deleteData(rst);
				}
			}

			if (status.Equals("3"))
			{
				try
				{
					ExecSPAuditTrail(code,"DOC_ID",code,"","2");
					ExecSPAuditTrail(code,"TBO_DESC",desc,"","2");
					ExecSPAuditTrail(code,"CD_SIBS",sbc,"","2");
					ExecSPAuditTrail(code,"SIBS_FLAG",flag,"","2");

					/*20070716 change by sofyan, for Consumer Enhancement
					conn.QueryString = "UPDATE RFTBO SET ACTIVE='0' WHERE DOC_ID = '"+code+"'";
					*/
					conn.QueryString = "exec PARAM_TBOREF_APPRUPDATE 3, '" + code+"'";
					conn.ExecuteQuery();
				}
				catch(Exception Ex)
				{
					string errmsg = Ex.Message.Replace("'","");
					if (errmsg.IndexOf("Last Query:") > 0)		
						errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
					GlobalTools.popMessage(this, errmsg);
				}
				finally
				{
					deleteData(rst);
				}
			}
			
			conn.ClearData(); 
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

		}
		#endregion

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DG_APPR.Items.Count; i++)
			{
				RadioButton rbA = (RadioButton)DG_APPR.Items[i].FindControl("Rdb1"),
					rbR = (RadioButton)DG_APPR.Items[i].FindControl("Rdb2");
				
				if(rbA.Checked)
				{
					performRequest(i);
				}
				else if(rbR.Checked)
				{
					deleteData(i);
				}
			}

			BindData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc=9902040202&moduleID=40");
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
							RadioButton rbA = (RadioButton) DG_APPR.Items[i].FindControl("Rdb1"),
								rbB = (RadioButton) DG_APPR.Items[i].FindControl("Rdb2"),
								rbC = (RadioButton) DG_APPR.Items[i].FindControl("Rdb3");
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
							RadioButton rbA = (RadioButton) DG_APPR.Items[i].FindControl("Rdb1"),
								rbB = (RadioButton) DG_APPR.Items[i].FindControl("Rdb2"),
								rbC = (RadioButton) DG_APPR.Items[i].FindControl("Rdb3");
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
							RadioButton rbA = (RadioButton) DG_APPR.Items[i].FindControl("Rdb1"),
								rbB = (RadioButton) DG_APPR.Items[i].FindControl("Rdb2"),
								rbC = (RadioButton) DG_APPR.Items[i].FindControl("Rdb3");
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
	}
}
