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
using System.Configuration;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for ApprovalCondParamAppr.
	/// </summary>
	public partial class ApprovalCondParamAppr : System.Web.UI.Page
	{
		protected Connection conn2;
		protected Connection conn;
		protected string scid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon();
			if(!IsPostBack)
			{
				ViewData(); 
			}
		}

		private void InitialCon()
		{
			conn2.QueryString = "select * from vw_getconn where moduleid = '"+Request.QueryString["ModuleId"]+"'";
			conn2.ExecuteQuery();
			string DB_NAMA = conn2.GetFieldValue("DB_NAMA");
			string DB_IP = conn2.GetFieldValue("DB_IP");
			string DB_LOGINID = conn2.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn2.GetFieldValue("DB_LOGINPWD");
			conn = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
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

		private void ViewData()
		{
			conn.QueryString = "select a.*,PR_DESC, PRODUCTNAME , "+  
				"STATUS = CASE a.PENDINGSTATUS WHEN '1' THEN 'INSERT' "+
				"WHEN '2' THEN 'UPDATE' WHEN '3' THEN 'DELETE' END "+
				"from PENDING_APPROVAL_CONDITION a "+
				"left join program b on a.PR_CODE=b.PR_CODE "+
				"left join tproduct c on a.PRODUCTID=c.PRODUCTID";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			this.DG_APPR.DataSource = dt;

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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleId=40");
		}

		private void deleteData(int row)
		{
			try 
			{
				string progid = DG_APPR.Items[row].Cells[14].Text.Trim();
				string prodid = DG_APPR.Items[row].Cells[15].Text.Trim();
				string code = DG_APPR.Items[row].Cells[2].Text.Trim();

				conn.QueryString = "delete from PENDING_APPROVAL_CONDITION where PR_CODE = '"+progid+"'"+
					"and PRODUCTID = '"+prodid+"' and CONDITION_CODE='"+code+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "APPROVAL_CONDITION";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'APPROVAL CONDITION'";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}

		private void performRequest(int row, string userid)
		{
			int rst = 0;
			
			string progid = DG_APPR.Items[row].Cells[14].Text.Trim();
			string prodid = DG_APPR.Items[row].Cells[15].Text.Trim();
			string code = DG_APPR.Items[row].Cells[2].Text.Trim();
			string min = DG_APPR.Items[row].Cells[3].Text.Trim(); 
			string max = DG_APPR.Items[row].Cells[4].Text.Trim(); 
			string score = DG_APPR.Items[row].Cells[5].Text.Trim(); 
			string field = DG_APPR.Items[row].Cells[6].Text.Trim(); 
			string table = DG_APPR.Items[row].Cells[7].Text.Trim(); 
			string valuenya = DG_APPR.Items[row].Cells[8].Text.Trim(); 
			string status = DG_APPR.Items[row].Cells[13].Text.Trim();

			string idnya = progid+"|"+prodid+"|"+code;

			rst = row;

			conn.QueryString = "select * from APPROVAL_CONDITION where PR_CODE = '"+progid+"' and PRODUCTID='"+prodid+"' and CONDITION_CODE='"+code+"'";
			conn.ExecuteQuery();
			string PR_CODE = conn.GetFieldValue("PR_CODE");
			string PRODUCTID = conn.GetFieldValue("PRODUCTID");
			string CONDITION_CODE = conn.GetFieldValue("CONDITION_CODE");
			string CONDITION_MIN_LIMIT = conn.GetFieldValue("CONDITION_MIN_LIMIT");
			string CONDITION_MAX_LIMIT = conn.GetFieldValue("CONDITION_MAX_LIMIT"); 
			string CONDITION_SCORE_RESULT = conn.GetFieldValue("CONDITION_SCORE_RESULT");
			string CONDITION_JENIS_FIELD = conn.GetFieldValue("CONDITION_JENIS_FIELD"); 
			string CONDITION_JENIS_TABLE = conn.GetFieldValue("CONDITION_JENIS_TABLE");
			string CONDITION_JENIS_value = conn.GetFieldValue("CONDITION_JENIS_value");
									
			if (status.Equals("1"))
			{
				try
				{
					/* coding for audittrial parameter */
			
					ExecSPAuditTrail(idnya,"PR_CODE","",progid,"1");
					ExecSPAuditTrail(idnya,"PRODUCTID","",prodid,"1");
					ExecSPAuditTrail(idnya,"CONDITION_CODE","",code,"1");
					ExecSPAuditTrail(idnya,"CONDITION_MIN_LIMIT","",min,"1");
					ExecSPAuditTrail(idnya,"CONDITION_MAX_LIMIT","",max,"1");
					ExecSPAuditTrail(idnya,"CONDITION_SCORE_RESULT","",score,"1");
					ExecSPAuditTrail(idnya,"CONDITION_JENIS_FIELD","",field,"1");
					ExecSPAuditTrail(idnya,"CONDITION_JENIS_TABLE","",table,"1");
					ExecSPAuditTrail(idnya,"CONDITION_JENIS_value","",valuenya,"1");
					/* end of coding */
					
					conn.QueryString = "insert into APPROVAL_CONDITION "+
						"values ('"+progid+"','"+prodid+"','"+code+"','"+min+"','"+max+"','"+score+"',"+
						"'"+field+"','"+table+"','"+valuenya+"')";
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
					if(progid.Trim()!= PR_CODE.Trim())
					{
						ExecSPAuditTrail(idnya,"PR_CODE",PR_CODE,progid,"0");
					}
					if(prodid.Trim()!= PRODUCTID.Trim())
					{
						ExecSPAuditTrail(idnya,"PRODUCTID",PRODUCTID,prodid,"0");
					}
					if(code.Trim()!= CONDITION_CODE.Trim())
					{
						ExecSPAuditTrail(idnya,"CONDITION_CODE",CONDITION_CODE,code,"0");
					}
					if(min.Trim()!= CONDITION_MIN_LIMIT.Trim())
					{
						ExecSPAuditTrail(idnya,"CONDITION_MIN_LIMIT",CONDITION_MIN_LIMIT,min,"0");
					}
					if(max.Trim()!= CONDITION_MAX_LIMIT.Trim())
					{
						ExecSPAuditTrail(idnya,"CONDITION_MAX_LIMIT",CONDITION_MAX_LIMIT,max,"0");
					}
					if(score.Trim()!= CONDITION_SCORE_RESULT.Trim())
					{
						ExecSPAuditTrail(idnya,"CONDITION_SCORE_RESULT",CONDITION_SCORE_RESULT,score,"0");
					}
					if(field.Trim()!= CONDITION_JENIS_FIELD.Trim())
					{
						ExecSPAuditTrail(idnya,"CONDITION_JENIS_FIELD",CONDITION_JENIS_FIELD,field,"0");
					}
					if(table.Trim()!= CONDITION_JENIS_TABLE.Trim())
					{
						ExecSPAuditTrail(idnya,"CONDITION_JENIS_TABLE",CONDITION_JENIS_TABLE,table,"0");
					}
					if(valuenya.Trim()!= CONDITION_JENIS_value.Trim())
					{
						ExecSPAuditTrail(idnya,"CONDITION_JENIS_value",CONDITION_JENIS_value,valuenya,"0");
					}

					conn.QueryString = "update APPROVAL_CONDITION set CONDITION_MIN_LIMIT = "+GlobalTools.ConvertFloat(min)+", "+
						"CONDITION_MAX_LIMIT="+GlobalTools.ConvertFloat(max)+", CONDITION_SCORE_RESULT='"+score+"' ,"+
						"CONDITION_JENIS_FIELD='"+field+"', CONDITION_JENIS_TABLE='"+table+"', CONDITION_JENIS_value='"+valuenya+"' "+
						"where PR_CODE = '"+progid+"' "+
						"and PRODUCTID = '"+prodid+"' and CONDITION_CODE='"+code+"'"; 	
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
					ExecSPAuditTrail(idnya,"PR_CODE",progid,"","2");
					ExecSPAuditTrail(idnya,"PRODUCTID",prodid,"","2");
					ExecSPAuditTrail(idnya,"CONDITION_CODE",code,"","2");
					ExecSPAuditTrail(idnya,"CONDITION_MIN_LIMIT",min,"","2");
					ExecSPAuditTrail(idnya,"CONDITION_MAX_LIMIT",max,"","2");
					ExecSPAuditTrail(idnya,"CONDITION_SCORE_RESULT",score,"","2");
					ExecSPAuditTrail(idnya,"CONDITION_JENIS_FIELD",field,"","2");
					ExecSPAuditTrail(idnya,"CONDITION_JENIS_TABLE",table,"","2");
					ExecSPAuditTrail(idnya,"CONDITION_JENIS_value",valuenya,"","2");

					conn.QueryString = "DELETE FROM APPROVAL_CONDITION where PR_CODE = '"+progid+"' "+
						"and PRODUCTID = '"+prodid+"' and CONDITION_CODE='"+code+"'"; 	
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

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			scid = (string) Session["UserID"];

			for (int i = 0; i < DG_APPR.Items.Count; i++)
			{
				RadioButton rbA = (RadioButton)DG_APPR.Items[i].FindControl("Rdb1"),
					rbR = (RadioButton)DG_APPR.Items[i].FindControl("Rdb2");
				
				if(rbA.Checked)
				{
					performRequest(i, scid);
				}
				else if(rbR.Checked)
				{
					deleteData(i);
				}
			}

			ViewData();	
		}

		private void DG_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_APPR.CurrentPageIndex = e.NewPageIndex;
			
			ViewData(); 
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
