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
	/// Summary description for ApprovalRoutingParamAppr.
	/// </summary>
	public partial class ApprovalRoutingParamAppr : System.Web.UI.Page
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
			string chk1;
			CheckBox chk;

			conn.QueryString = "select *,sg_grpname, "+  
				"STATUS = CASE PENDINGSTATUS WHEN '1' THEN 'INSERT' "+
				"WHEN '2' THEN 'UPDATE' WHEN '3' THEN 'DELETE' END "+
				" from PENDING_APPROVAL_ROUTING a "+
				"left join scgroup b on a.routing_group=b.groupid";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			this.DG_APPR.DataSource = dt;

			try
			{
				DG_APPR.DataBind();
				for (int i=0; i<DG_APPR.Items.Count; i++)
				{
					chk1 = DG_APPR.Items[i].Cells[9].Text;
					chk = (CheckBox)DG_APPR.Items[i].Cells[4].FindControl("chk_RFoureyes");
					if (chk1 =="1")
						chk.Checked = true;
					else
						chk.Checked = false;
				}
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
				string Ccode = DG_APPR.Items[row].Cells[0].Text.Trim();
				string Fcode = DG_APPR.Items[row].Cells[1].Text.Trim();
				string Seq = DG_APPR.Items[row].Cells[2].Text.Trim();

				conn.QueryString = "delete from PENDING_APPROVAL_ROUTING where CONDITION_CODE = '"+Ccode+"'"+
					"and FORMULA_CODE = '"+Fcode+"' and ROUTING_SEQ = '"+Seq+"'";
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
			
			string Ccode = DG_APPR.Items[row].Cells[0].Text.Trim();
			string Fcode = DG_APPR.Items[row].Cells[1].Text.Trim();
			string Seq = DG_APPR.Items[row].Cells[2].Text.Trim();
			string Group = DG_APPR.Items[row].Cells[11].Text.Trim(); 
			string Foureyes = DG_APPR.Items[row].Cells[9].Text.Trim(); 
			string status = DG_APPR.Items[row].Cells[10].Text.Trim();

			rst = row;

			string idnya = Ccode+"|"+Fcode+"|"+Seq;

			conn.QueryString = "select * from APPROVAL_ROUTING where CONDITION_CODE = '"+Ccode+"' and FORMULA_CODE='"+Fcode+"' and ROUTING_SEQ='"+Seq+"'";
			conn.ExecuteQuery();
			string CONDITION_CODE = conn.GetFieldValue("CONDITION_CODE");
			string FORMULA_CODE = conn.GetFieldValue("FORMULA_CODE");
			string ROUTING_SEQ = conn.GetFieldValue("ROUTING_SEQ");
			string ROUTING_GROUP = conn.GetFieldValue("ROUTING_GROUP");
			string ROUTING_FOUREYES = conn.GetFieldValue("ROUTING_FOUREYES"); 
		
			if (status.Equals("1"))
			{
				try
				{
					/* coding for audittrial parameter */
					ExecSPAuditTrail(idnya,"CONDITION_CODE","",Ccode,"1");
					ExecSPAuditTrail(idnya,"FORMULA_CODE","",Fcode,"1");
					ExecSPAuditTrail(idnya,"ROUTING_SEQ","",Seq,"1");
					ExecSPAuditTrail(idnya,"ROUTING_GROUP","",Group,"1");
					ExecSPAuditTrail(idnya,"ROUTING_FOUREYES","",Foureyes,"1");
					/* end of coding */
					conn.QueryString = "insert into APPROVAL_ROUTING "+
						"values ('"+Ccode+"','"+Fcode+"','"+Seq+"','"+Group+"','"+Foureyes+"')";
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
					if(Ccode.Trim()!= CONDITION_CODE.Trim())
					{
						ExecSPAuditTrail(idnya,"CONDITION_CODE",CONDITION_CODE,Ccode,"0");
					}
					if(Fcode.Trim()!= FORMULA_CODE.Trim())
					{
						ExecSPAuditTrail(idnya,"FORMULA_CODE",FORMULA_CODE,Fcode,"0");
					}
					if(Seq.Trim()!= ROUTING_SEQ.Trim())
					{
						ExecSPAuditTrail(idnya,"ROUTING_SEQ",ROUTING_SEQ,Seq,"0");
					}
					if(Group.Trim()!= ROUTING_GROUP.Trim())
					{
						ExecSPAuditTrail(idnya,"ROUTING_GROUP",ROUTING_GROUP,Group,"0");
					}
					if(Foureyes.Trim()!= ROUTING_FOUREYES.Trim())
					{
						ExecSPAuditTrail(idnya,"ROUTING_FOUREYES",ROUTING_FOUREYES,Foureyes,"0");
					}

					conn.QueryString = "update APPROVAL_ROUTING set ROUTING_GROUP = '"+Group+"', "+
						"ROUTING_FOUREYES='"+Foureyes+"' "+
						"where CONDITION_CODE = '"+Ccode+"'"+
						"and FORMULA_CODE = '"+Fcode+"' and ROUTING_SEQ = '"+Seq+"'";	
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
					ExecSPAuditTrail(idnya,"CONDITION_CODE",Ccode,"","2");
					ExecSPAuditTrail(idnya,"FORMULA_CODE",Fcode,"","2");
					ExecSPAuditTrail(idnya,"ROUTING_SEQ",Seq,"","2");
					ExecSPAuditTrail(idnya,"ROUTING_GROUP",Group,"","2");
					ExecSPAuditTrail(idnya,"ROUTING_FOUREYES",Foureyes,"","2");

					conn.QueryString = "DELETE FROM APPROVAL_ROUTING where CONDITION_CODE = '"+Ccode+"'"+
						"and FORMULA_CODE = '"+Fcode+"' and ROUTING_SEQ = '"+Seq+"'";	
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
			
			ViewData(); 
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

	}
}
