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
	/// Summary description for JabatanParamAppr.
	/// </summary>
	public partial class JabatanParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);

			if(!IsPostBack)
			{
				fillDDL();
				ViewData();
				BindData(DDL_MODULE.SelectedValue);
			}		
		}

		private void fillDDL()
		{
			/* if moduleid change, must fix this!!! */
			DDL_MODULE.Items.Add(new ListItem("01 - SME","01"));
			DDL_MODULE.Items.Add(new ListItem("40 - Consumer","40"));

			try { DDL_MODULE.SelectedValue = "01"; } 
			catch{}
		}

		private void ViewData()
		{
			conn2.QueryString = "select * from rfmodule where moduleid = '"+DDL_MODULE.SelectedValue+"'";
			conn2.ExecuteQuery();

			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			InitialCon(); 
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private string getPendingStatus(string saveMode) 
		{
			string status = "";			
			
			switch (saveMode)
			{
				case "1": case "4": 
					status = "INSERT";
					break;
				case "2": case "5": 
					status = "UPDATE";
					break;
				case "3": case "6": 
					status = "DELETE";
					break;
				default:
					status = "";
					break;
			}
			return status;
		}

		private void BindData(string mode) 
		{
			if(mode == "01")
			{
				conn2.QueryString = "select * from PENDING_RFJABATAN WHERE PENDINGSTATUS IN ('1','2','3')";
			}
			else if(mode == "40")
			{
				conn2.QueryString = "select * from PENDING_RFJABATAN WHERE PENDINGSTATUS IN ('4','5','6')";
			}
		
			conn2.ExecuteQuery();

			DataTable dt = new DataTable();			
		
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("STATUS"));
			dt.Columns.Add(new DataColumn("CH_STA"));			

			DataRow dr;
		
			for(int i = 0; i < conn2.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn2.GetFieldValue(i,0);
				dr[1] = conn2.GetFieldValue(i,1);
				dr[2] = getPendingStatus(conn2.GetFieldValue(i,"PENDINGSTATUS"));
				dr[3] = conn2.GetFieldValue(i,"PENDINGSTATUS");
				dt.Rows.Add(dr);
			}			

			DG_APPR.DataSource = new DataView(dt);
		
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
				
				conn2.QueryString = "DELETE FROM PENDING_RFJABATAN WHERE JB_CODE = '"+cd+"'";
				conn2.ExecuteQuery();

				conn2.ClearData(); 
			} 
			catch {}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "RFJABATAN";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'Jabatan Pejabat Bank'";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}

		private void performRequest(int row)
		{
			int rst = 0;
			
			string code = DG_APPR.Items[row].Cells[0].Text.Trim();
			string desc =  cleansText(DG_APPR.Items[row].Cells[1].Text);
			string status = DG_APPR.Items[row].Cells[3].Text.Trim();

			rst = row;

			conn.QueryString="select JB_CODE,JB_DESC from RFJABATAN where JB_CODE='"+code+"'";
			conn.ExecuteQuery();
			string code_old = conn.GetFieldValue("JB_CODE");
			string desc_old = conn.GetFieldValue("JB_DESC");

			if (status.Equals("1") || status.Equals("4"))
			{
				try
				{
					ExecSPAuditTrail(code,"JB_CODE","",code,"1");
					ExecSPAuditTrail(code,"JB_DESC","",desc,"1");

					conn.QueryString = "INSERT INTO RFJABATAN (JB_CODE, JB_DESC, ACTIVE) "+ 
						"VALUES('"+code+"', "+GlobalTools.ConvertNull(desc)+", '1')";
					conn.ExecuteQuery();			
				}
				catch
				{
					GlobalTools.popMessage(this,"Cannot approve, data with code: "+code+" already exist!");
				}
				finally
				{
					deleteData(rst);
				}
				
			}

			if (status.Equals("2") || status.Equals("5"))
			{
				try
				{
					if(code.Trim()!= code_old.Trim())
					{
						ExecSPAuditTrail(code,"JB_CODE",code_old,code,"0");
					}
					if(desc.Trim()!= desc_old.Trim())
					{
						ExecSPAuditTrail(code,"JB_DESC",desc_old,desc,"0");
					}

					conn.QueryString = "UPDATE RFJABATAN SET JB_DESC = "+GlobalTools.ConvertNull(desc)+" "+
						"where JB_CODE = '"+code+"'"; 
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

			if (status.Equals("3") || status.Equals("6"))
			{
				try
				{
					ExecSPAuditTrail(code,"JB_CODE",code,"","2");
					ExecSPAuditTrail(code,"JB_DESC",desc,"","2");

					conn.QueryString = "UPDATE RFJABATAN SET ACTIVE='0' WHERE JB_CODE = '"+code+"'";
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
			this.DG_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_APPR_PageIndexChanged);

		}
		#endregion

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			ViewData();
 
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
		
			BindData(DDL_MODULE.SelectedValue);					
		}

		private void DG_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_APPR.CurrentPageIndex = e.NewPageIndex;	

			BindData(DDL_MODULE.SelectedValue); 		
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../CommonParamApproval.aspx?mc=99020202");		
		}

		protected void DDL_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData(DDL_MODULE.SelectedValue); 
		}
	}
}
