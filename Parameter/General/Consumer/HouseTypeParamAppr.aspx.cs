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
	/// Summary description for HouseTypeParamAppr.
	/// </summary>
	public partial class HouseTypeParamAppr : System.Web.UI.Page
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
			conn.QueryString = "select type_code,type_name,description,ch_sta,TYPE_CODE_LAMA from THOUSE_TYPE order by type_code";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGRequest.DataSource = dt;

			try
			{
				DGRequest.DataBind();
			}
			catch 
			{
				DGRequest.CurrentPageIndex = DGRequest.PageCount - 1;
				DGRequest.DataBind();
			}

			for (int i = 0; i < DGRequest.Items.Count; i++)
			{
				Label status = (Label)DGRequest.Items[i].Cells[3].FindControl("LBL_STATUS");
				//no.Text = (i+1+(DGRequest.CurrentPageIndex)*DGRequest.PageSize).ToString();
				if (DGRequest.Items[i].Cells[4].Text.Trim() == "1")
				{
					status.Text = "INSERT";
				}
				else if (DGRequest.Items[i].Cells[4].Text.Trim() == "2")
				{
					status.Text = "UPDATE";
				}
				else if (DGRequest.Items[i].Cells[4].Text.Trim() == "3")
				{
					status.Text = "DELETE";
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
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc=9902040102&moduleID=40");
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
				case "allReject":
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

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "HOUSE_TYPE";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'House Type'";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGRequest.Items.Count; i++)
			{
				
				RadioButton rbA = (RadioButton) DGRequest.Items[i].Cells[7].FindControl("rdo_Approve"),
					rbB = (RadioButton) DGRequest.Items[i].Cells[8].FindControl("rdo_Reject"),
					rbC = (RadioButton) DGRequest.Items[i].Cells[9].FindControl("rdo_Pending");					

				conn.QueryString = "select * from HOUSE_TYPE where TYPE_CODE = '"+DGRequest.Items[i].Cells[0].Text+"'";
				conn.ExecuteQuery();
				string TYPE_CODE_OLD = conn.GetFieldValue("TYPE_CODE");
				string TYPE_NAME_OLD = conn.GetFieldValue("TYPE_NAME");
				string DESCRIPTION_OLD = conn.GetFieldValue("DESCRIPTION");
				
				if(rbA.Checked == true)
				{
					if(DGRequest.Items[i].Cells[4].Text.Trim() == "1")
					{
						try
						{
							ExecSPAuditTrail(DGRequest.Items[i].Cells[0].Text,"TYPE_CODE","",DGRequest.Items[i].Cells[0].Text,"1");
							ExecSPAuditTrail(DGRequest.Items[i].Cells[0].Text,"TYPE_NAME","",DGRequest.Items[i].Cells[1].Text,"1");
							ExecSPAuditTrail(DGRequest.Items[i].Cells[0].Text,"DESCRIPTION","",DGRequest.Items[i].Cells[2].Text,"1");

							try
							{
								conn.QueryString = "insert into HOUSE_TYPE (TYPE_CODE,TYPE_NAME,DESCRIPTION,ACTIVE) "+
									"values ('"+DGRequest.Items[i].Cells[0].Text+"','"+DGRequest.Items[i].Cells[1].Text+
									"','"+DGRequest.Items[i].Cells[2].Text+"','1')";
								conn.ExecuteNonQuery();
							}
							catch(Exception Ex)
							{
								string errmsg = Ex.Message.Replace("'","");
								if (errmsg.IndexOf("Last Query:") > 0)		
									errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
								GlobalTools.popMessage(this, errmsg);
							}

							conn.QueryString = "delete from THOUSE_TYPE where TYPE_CODE = '"+DGRequest.Items[i].Cells[0].Text+"'";
							conn.ExecuteNonQuery();
						}
						catch(Exception Ex)
						{
							string errmsg = Ex.Message.Replace("'","");
							if (errmsg.IndexOf("Last Query:") > 0)		
								errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
							GlobalTools.popMessage(this, errmsg);
						}
						
					}
					else if(DGRequest.Items[i].Cells[4].Text.Trim() == "2")
					{		
						if(DGRequest.Items[i].Cells[0].Text!= TYPE_CODE_OLD.Trim())
						{
							ExecSPAuditTrail(DGRequest.Items[i].Cells[0].Text,"TYPE_CODE",TYPE_CODE_OLD,DGRequest.Items[i].Cells[0].Text,"0");
						}
						if(DGRequest.Items[i].Cells[1].Text!= TYPE_NAME_OLD.Trim())
						{
							ExecSPAuditTrail(DGRequest.Items[i].Cells[0].Text,"TYPE_NAME",TYPE_NAME_OLD,DGRequest.Items[i].Cells[1].Text,"0");
						}
						if(DGRequest.Items[i].Cells[2].Text!= DESCRIPTION_OLD.Trim())
						{
							ExecSPAuditTrail(DGRequest.Items[i].Cells[0].Text,"DESCRIPTION",DESCRIPTION_OLD,DGRequest.Items[i].Cells[2].Text,"0");
						}

						conn.QueryString = "UPDATE HOUSE_TYPE set TYPE_CODE = '"+DGRequest.Items[i].Cells[0].Text+"',TYPE_NAME = '"+DGRequest.Items[i].Cells[1].Text+"',DESCRIPTION = '"+DGRequest.Items[i].Cells[2].Text+"' "+
							"where TYPE_CODE= '"+DGRequest.Items[i].Cells[5].Text+"'";	
						conn.ExecuteNonQuery();

						conn.QueryString = "delete from tHOUSE_TYPE where TYPE_CODE_LAMA= '"+DGRequest.Items[i].Cells[5].Text+"'";	
						conn.ExecuteNonQuery();
						
					}
					else if(DGRequest.Items[i].Cells[4].Text.Trim() == "3")
					{			
						ExecSPAuditTrail(DGRequest.Items[i].Cells[0].Text,"TYPE_CODE",DGRequest.Items[i].Cells[0].Text,"","2");
						ExecSPAuditTrail(DGRequest.Items[i].Cells[0].Text,"TYPE_NAME",DGRequest.Items[i].Cells[1].Text,"","2");
						ExecSPAuditTrail(DGRequest.Items[i].Cells[0].Text,"DESCRIPTION",DGRequest.Items[i].Cells[2].Text,"","2");
						
						conn.QueryString = "Update HOUSE_TYPE set Active='0' where TYPE_CODE = '"+DGRequest.Items[i].Cells[5].Text+"'";	
						conn.ExecuteNonQuery();
						conn.QueryString = "delete from tHOUSE_TYPE where TYPE_CODE_LAMA= '"+DGRequest.Items[i].Cells[5].Text+"'";	
						conn.ExecuteNonQuery();
						
					}
				}
				else if(rbB.Checked == true)
				{
					conn.QueryString = "delete from tHOUSE_TYPE where TYPE_CODE_LAMA= '"+DGRequest.Items[i].Cells[0].Text+"'";	
					conn.ExecuteNonQuery();					
				}
			}	
			BindData();	
		}

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			BindData();
		}
	}
}
