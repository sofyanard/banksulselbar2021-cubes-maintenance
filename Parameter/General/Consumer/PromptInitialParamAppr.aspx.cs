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
	/// Summary description for PromptInitialParamAppr.
	/// </summary>
	public partial class PromptInitialParamAppr : System.Web.UI.Page
	{
		protected Connection conn2;
		protected Connection conn;
		protected string mid,DB_LOGINID,DB_LOGINPWD;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			ViewData();
			if(!IsPostBack)
			{
				//ViewData(); 
				BindData();
			}
//			else
//			{
//				InitialCon();
//			}	
		}

		private void ViewData()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select db_ip, db_nama,DB_LOGINID,DB_LOGINPWD from rfmodule where moduleid = '40'";
			conn2.ExecuteQuery();

			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			DB_LOGINID = conn2.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn2.GetFieldValue("DB_LOGINPWD");

			InitialCon(); 
			//BindData();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void BindData()
		{
			conn.QueryString = "select SEQ, TYPE_CHECK, BLACKLIST_RESULT, "+
						"case when TYPE_CHECK='1' then 'Basic' when TYPE_CHECK='2' then 'Intermediate' "+
						"when TYPE_CHECK='3' then 'Advance' end TYPE_CHECK1, "+
						"CH_STA, STATUS = case CH_STA when '1' then 'INSERT' "+
						"when '2' then 'UPDATE' when '3' then 'DELETE' end "+
						"from TPRM_INIT order by SEQ";
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

		private void deleteData(int row)
		{
			try 
			{
				string seq = DG_APPR.Items[row].Cells[0].Text.Trim();
				
				conn.QueryString = "DELETE FROM TPRM_INIT WHERE SEQ = '"+seq+"'";
				conn.ExecuteQuery();
				
				conn.ClearData();
			} 
			catch {}
		}

		private void performRequest(int row)
		{
			int rst = 0;
			string userid = Session["UserID"].ToString();
			string groupid = Session["GroupID"].ToString();
			string seq = DG_APPR.Items[row].Cells[0].Text.Trim();
			string status = DG_APPR.Items[row].Cells[5].Text.Trim();

			rst = row;
			if (status.Equals("1"))
			{
				try
				{
					conn.QueryString = "EXEC PARAM_GENERAL_PROMPT_INITIAL '1', '"+seq+"' ,'"+userid+"','"+groupid+"'";
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
					conn.QueryString = "EXEC PARAM_GENERAL_PROMPT_INITIAL '2', '"+seq+"' , '"+userid+"','"+groupid+"'";
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
					conn.QueryString = "DELETE FROM PRM_INIT WHERE SEQ = '"+seq+"'"; 
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
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc=9902040102&moduleID=40"); 
		}

		private void DG_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DG_APPR.CurrentPageIndex = e.NewPageIndex;
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
	}
}
