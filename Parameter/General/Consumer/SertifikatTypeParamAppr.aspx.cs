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
	/// Summary description for SertifikatTypeParamAppr.
	/// </summary>
	public partial class SertifikatTypeParamAppr : System.Web.UI.Page
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
			conn.QueryString = "select ST_CODE, ST_DESC, CD_SIBS, ST_EXPDATEFLAG, "+
				"STATUS = case CH_STA when '1' then 'INSERT' "+
				"when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end, CH_STA "+
				"from TRFSERTYPE ORDER BY ST_CODE ASC";
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

		private void deleteData(int row)
		{
			try 
			{
				string cd = DG_APPR.Items[row].Cells[0].Text.Trim();
				conn.QueryString = "DELETE FROM TRFSERTYPE WHERE ST_CODE = '"+cd+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "RFSERTYPE";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'Certificate Type' ";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}

		private void performRequest(int row)
		{
			int rst = 0;
			
			string code = DG_APPR.Items[row].Cells[0].Text.Trim();
			string desc = DG_APPR.Items[row].Cells[1].Text.Trim();
			string sbc = DG_APPR.Items[row].Cells[2].Text.Trim();
			string flag = DG_APPR.Items[row].Cells[3].Text.Trim();
			string status = DG_APPR.Items[row].Cells[5].Text.Trim();

			rst = row;

			conn.QueryString = "select * from RFSERTYPE where ST_CODE = '"+code+"'";
			conn.ExecuteQuery();
			string ST_CODE = conn.GetFieldValue("ST_CODE");
			string ST_DESC= conn.GetFieldValue("ST_DESC");
			string CD_SIBS = conn.GetFieldValue("CD_SIBS");
			string ST_EXPDATEFLAG = conn.GetFieldValue("ST_EXPDATEFLAG");
			
			if (status.Equals("1"))
			{
				try
				{
					conn.QueryString = "INSERT INTO RFSERTYPE VALUES('"+code+"','"+desc+"','"+sbc+"', NULL,'"+flag+"', '1')";
					conn.ExecuteQuery();			
					// update first, then audit..... coz, sometimes the auditrail procs throw exception 
					ExecSPAuditTrail(code,"ST_CODE","",code,"1");
					ExecSPAuditTrail(code,"ST_DESC","",desc,"1");
					ExecSPAuditTrail(code,"CD_SIBS","",sbc,"1");
					ExecSPAuditTrail(code,"ST_EXPDATEFLAG","",flag,"1");
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
					conn.QueryString = "UPDATE RFSERTYPE SET ST_DESC = '"+desc+"', CD_SIBS = '"+sbc+"', ST_EXPDATEFLAG = '"+flag+"' WHERE ST_CODE = '"+code+"'";
					conn.ExecuteQuery();
					// update first, then audit..... coz, sometimes the auditrail procs throw exception 
					if(code.Trim()!= ST_CODE.Trim())
					{
						ExecSPAuditTrail(code,"ST_CODE",ST_CODE,code,"0");
					}
					if(desc.Trim()!= ST_DESC.Trim())
					{
						ExecSPAuditTrail(code,"ST_DESC",ST_DESC,desc,"0");
					}
					if(sbc.Trim()!= CD_SIBS.Trim())
					{
						ExecSPAuditTrail(code,"CD_SIBS",CD_SIBS,sbc,"0");
					}
					if(flag.Trim()!= ST_EXPDATEFLAG.Trim())
					{
						ExecSPAuditTrail(code,"ST_EXPDATEFLAG",ST_EXPDATEFLAG,flag,"0");
					}
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
					//conn.QueryString = "DELETE FROM RFSERTYPE WHERE ST_CODE = '"+code+"'"; 
					conn.QueryString = "UPDATE RFSERTYPE SET ACTIVE='0' WHERE ST_CODE = '"+code+"'"; 
					conn.ExecuteQuery();
					// update first, then audit..... coz, sometimes the auditrail procs throw exception 
					ExecSPAuditTrail(code,"ST_CODE",code,"","2");
					ExecSPAuditTrail(code,"ST_DESC",desc,"","2");
					ExecSPAuditTrail(code,"CD_SIBS",sbc,"","2");
					ExecSPAuditTrail(code,"ST_EXPDATEFLAG",flag,"","2");
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

		private void DG_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_APPR.CurrentPageIndex = e.NewPageIndex;
			
			BindData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc=9902040102&moduleID=40"); 
		}
	}
}
