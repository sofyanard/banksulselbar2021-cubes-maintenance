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
	/// Summary description for ZipcodeParamAppr.
	/// </summary>
	public partial class ZipcodeParamAppr : System.Web.UI.Page
	{
		protected Connection conn2;
		protected Connection conn;
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
			conn.QueryString = "select a.*, c.AREA_NAME, b.CITY_NAME, a.CH_STA, "+
				"STATUS = case a.CH_STA when '1' then 'INSERT' "+
				"when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TRFZIPCODECITY a "+
				"left join CITY b on a.CITY_ID = b.CITY_ID "+
				"left join AREA c on b.AREA_ID = c.AREA_ID order by a.ZC_ZIPCODE";
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
				
				conn.QueryString = "DELETE FROM TRFZIPCODECITY WHERE ZC_ZIPCODE = '"+cd+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "RFZIPCODECITY";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'Zipcode City'";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}

		private void performRequest(int row)
		{
			int rst = 0;
			
			string code = DG_APPR.Items[row].Cells[0].Text.Trim();
			string arid = DG_APPR.Items[row].Cells[3].Text.Trim();
			string cid = DG_APPR.Items[row].Cells[4].Text.Trim();
			string desc =  cleansText(DG_APPR.Items[row].Cells[5].Text);
			string status = DG_APPR.Items[row].Cells[7].Text.Trim();

			rst = row;

			conn.QueryString = "select * from RFZIPCODECITY where ZC_ZIPCODE = '"+code+"'";
			conn.ExecuteQuery();
			string ZC_ZIPCODE = conn.GetFieldValue("ZC_ZIPCODE");
			string CITY_ID = conn.GetFieldValue("CITY_ID");
			string AREA_ID = conn.GetFieldValue("AREA_ID");
			string ZC_DESC = conn.GetFieldValue("ZC_DESC");
			
			if (status.Equals("1"))
			{
				try
				{
					ExecSPAuditTrail(code,"ZC_ZIPCODE","",code,"1");
					ExecSPAuditTrail(code,"CITY_ID","",cid,"1");
					ExecSPAuditTrail(code,"AREA_ID","",arid,"1");
					ExecSPAuditTrail(code,"ZC_DESC","",desc,"1");

					conn.QueryString = "INSERT INTO RFZIPCODECITY VALUES('"+code+"','"+cid+"','"+arid+"','"+desc+"')";
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
					if(code.Trim()!= ZC_ZIPCODE.Trim())
					{
						ExecSPAuditTrail(code,"ZC_ZIPCODE",ZC_ZIPCODE,code,"0");
					}
					if(cid.Trim()!= CITY_ID.Trim())
					{
						ExecSPAuditTrail(code,"CITY_ID",CITY_ID,cid,"0");
					}
					if(arid.Trim()!= AREA_ID.Trim())
					{
						ExecSPAuditTrail(code,"AREA_ID",AREA_ID,arid,"0");
					}
					if(desc.Trim()!= ZC_DESC.Trim())
					{
						ExecSPAuditTrail(code,"ZC_DESC",ZC_DESC,desc,"0");
					}

					conn.QueryString = "UPDATE RFZIPCODECITY SET AREA_ID = '"+arid+"', CITY_ID = '"+cid+"', ZC_DESC = "+GlobalTools.ConvertNull(desc)+" WHERE ZC_ZIPCODE = '"+code+"'";
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
					ExecSPAuditTrail(code,"ZC_ZIPCODE",code,"","2");
					ExecSPAuditTrail(code,"CITY_ID",cid,"","2");
					ExecSPAuditTrail(code,"AREA_ID",arid,"","2");
					ExecSPAuditTrail(code,"ZC_DESC",desc,"","2");

					conn.QueryString = "UPDATE RFZIPCODECITY SET ACTIVE='0' WHERE ZC_ZIPCODE = '"+code+"'"; 
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

		private void DG_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_APPR.CurrentPageIndex = e.NewPageIndex;
			
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc=9902040102&moduleId=40"); 
		}
	}
}
