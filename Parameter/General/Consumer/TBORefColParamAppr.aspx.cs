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
	/// Summary description for TBORefColParamAppr.
	/// </summary>
	public partial class TBORefColParamAppr : System.Web.UI.Page
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
			conn.QueryString = "select DOC_SEQ, COLTY = case COL_TYPE when 'C01' then 'Mobil' "+
				"when 'D01' then 'Deposito' when 'H01' then 'Tanah' "+
				"when 'H02' then 'Bangunan' when 'H03' then 'Tanah dan Bangunan' end, "+
				"FG = case FLAG when '1' then 'BARU' ELSE 'LAMA' end, "+
				"STATUS = case CH_STA when '1' then 'INSERT' "+
				"when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end, COL_TYPE, FLAG, DOC_ID, CH_STA "+
				"from TRFTBOCOLATERAL ORDER BY DOC_SEQ ASC";
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

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			InitialCon();
 
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

		private void deleteData(int row)
		{
			try 
			{
				string seq = DG_APPR.Items[row].Cells[0].Text.Trim();
				conn.QueryString = "DELETE FROM TRFTBOCOLATERAL WHERE DOC_SEQ = "+seq;
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "RFTBOCOLATERAL";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'TBO Reference Collateral'";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}

		private void performRequest(int row)
		{
			int rst = 0;
			
			string seq = DG_APPR.Items[row].Cells[0].Text.Trim();
			string docid = DG_APPR.Items[row].Cells[3].Text.Trim();
			string status = DG_APPR.Items[row].Cells[5].Text.Trim();
			string cty = DG_APPR.Items[row].Cells[6].Text.Trim();
			string flag = DG_APPR.Items[row].Cells[7].Text.Trim();

			rst = row;

			conn.QueryString = "select * from RFTBOCOLATERAL where DOC_SEQ = '"+seq+"'";
			conn.ExecuteQuery();
			string DOC_SEQ_OLD = conn.GetFieldValue("DOC_SEQ");
			string COL_TYPE_OLD = conn.GetFieldValue("COL_TYPE");
			string FLAG_OLD = conn.GetFieldValue("FLAG");
			string DOC_ID_OLD = conn.GetFieldValue("DOC_ID");
						
			if (status.Equals("1"))
			{
				try
				{
					ExecSPAuditTrail(seq,"DOC_SEQ","",seq,"1");
					ExecSPAuditTrail(seq,"COL_TYPE","",cty,"1");
					ExecSPAuditTrail(seq,"FLAG","",flag,"1");
					ExecSPAuditTrail(seq,"DOC_ID","",docid,"1");

					conn.QueryString = "INSERT INTO RFTBOCOLATERAL (DOC_SEQ,COL_TYPE,FLAG,DOC_ID) VALUES("+seq+",'"+cty+"','"+flag+"','"+docid+"')";
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
					if(seq.Trim()!= DOC_SEQ_OLD.Trim())
					{
						ExecSPAuditTrail(seq,"DOC_SEQ",DOC_SEQ_OLD,seq,"0");
					}
					if(cty.Trim()!= COL_TYPE_OLD.Trim())
					{
						ExecSPAuditTrail(seq,"COL_TYPE",COL_TYPE_OLD,cty,"0");
					}
					if(flag.Trim()!= FLAG_OLD.Trim())
					{
						ExecSPAuditTrail(seq,"FLAG",FLAG_OLD,flag,"0");
					}
					if(docid.Trim()!= DOC_ID_OLD.Trim())
					{
						ExecSPAuditTrail(seq,"DOC_ID",DOC_ID_OLD,docid,"0");
					}

					conn.QueryString = "UPDATE RFTBOCOLATERAL SET COL_TYPE = '"+cty+"', FLAG = '"+flag+"', DOC_ID = '"+docid+"' WHERE DOC_SEQ = "+seq;
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
					ExecSPAuditTrail(seq,"DOC_SEQ",seq,"","2");
					ExecSPAuditTrail(seq,"COL_TYPE",cty,"","2");
					ExecSPAuditTrail(seq,"FLAG",flag,"","2");
					ExecSPAuditTrail(seq,"DOC_ID",docid,"","2");

					conn.QueryString = "UPDATE RFTBOCOLATERAL SET ACTIVE='0' WHERE DOC_SEQ = "+seq; 
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc=9902040102&moduleID=40"); 
		}
	}
}
