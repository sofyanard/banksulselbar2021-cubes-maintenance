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
	/// Summary description for HolidayParamAppr.
	/// </summary>
	public partial class HolidayParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid,DB_LOGINID,DB_LOGINPWD;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			conn.QueryString = "select convert(varchar,HL_DATE,103) HL_DATE, HL_DESC,CH_STA,HL_TYPE,HL_CODE from TRFHOLIDAY order by HL_TYPE";
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
				Label no = (Label)DGRequest.Items[i].Cells[0].FindControl("LBL_NO");
				no.Text = (i+1+(DGRequest.CurrentPageIndex)*DGRequest.PageSize).ToString();
				if (DGRequest.Items[i].Cells[4].Text.Trim() == "1")
				{
					DGRequest.Items[i].Cells[4].Text = "INSERT";
				}
				else if (DGRequest.Items[i].Cells[4].Text.Trim() == "2")
				{
					DGRequest.Items[i].Cells[4].Text = "UPDATE";
				}
				else if (DGRequest.Items[i].Cells[4].Text.Trim() == "3")
				{
					DGRequest.Items[i].Cells[4].Text = "DELETE";
				}
				if (DGRequest.Items[i].Cells[3].Text.Trim() == "01")
				{
					DGRequest.Items[i].Cells[3].Text = "Libur Nasional";
				}
				else if (DGRequest.Items[i].Cells[3].Text.Trim() == "02")
				{
					DGRequest.Items[i].Cells[3].Text = "Akhir Pekan";
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
			tablename = "RFHOLIDAY";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'HOLIDAY '";
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

				if(rbA.Checked == true)
				{
					//AuditTrail
					string userid = Session["UserID"].ToString();
					//=====

					if(DGRequest.Items[i].Cells[10].Text.Trim() == "1")
					{
						conn.QueryString = "insert into rfholiday (HL_DATE,HL_DESC,HL_TYPE,ACTIVE)"+
							"values ("+GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text)+",'"+
							DGRequest.Items[i].Cells[2].Text+"','"+DGRequest.Items[i].Cells[5].Text+"','1')";
						conn.ExecuteNonQuery();

						//AuditTrail
						conn.QueryString = "exec PARAM_AUDITTRAIL_RFHOLIDAY '"+DGRequest.Items[i].Cells[5].Text+"',NULL,'"+userid+"', " +GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text);
						conn.ExecuteNonQuery();	
						//=====

						conn.QueryString = "delete from trfholiday where HL_TYPE = '"+DGRequest.Items[i].Cells[5].Text+
								"' and HL_DESC = '"+ DGRequest.Items[i].Cells[2].Text.Trim()+"' and HL_DATE = "+GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text)+
								" and CH_STA = '"+DGRequest.Items[i].Cells[10].Text+"'";
						conn.ExecuteNonQuery();
						
					}
					else if(DGRequest.Items[i].Cells[10].Text.Trim() == "2")
					{	
						//AuditTrail
						conn.QueryString = "exec PARAM_AUDITTRAIL_RFHOLIDAY '"+DGRequest.Items[i].Cells[5].Text+"','"+DGRequest.Items[i].Cells[6].Text+"','"+userid+"'";
						conn.ExecuteNonQuery();	
						//=====

						conn.QueryString = "update rfholiday set HL_DATE = "+GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text)+","+
								"HL_DESC = '"+DGRequest.Items[i].Cells[2].Text+"',HL_TYPE='"+DGRequest.Items[i].Cells[5].Text+"' "+
								"where HL_DATE = "+GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text);//+" and HL_CODE = "+DGRequest.Items[i].Cells[6].Text;							
						conn.ExecuteNonQuery();

						conn.QueryString = "delete from trfholiday where HL_DATE = "+GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text);//+" and HL_CODE = "+DGRequest.Items[i].Cells[6].Text;
						conn.ExecuteNonQuery();
						
					}
					else if(DGRequest.Items[i].Cells[10].Text.Trim() == "3")
					{						
						//AuditTrail
						conn.QueryString = "exec PARAM_AUDITTRAIL_RFHOLIDAY '"+DGRequest.Items[i].Cells[5].Text+"','"+DGRequest.Items[i].Cells[6].Text+"','"+userid+"'";
						conn.ExecuteNonQuery();	
						//=====

						conn.QueryString = "update rfholiday set active='0' where HL_DATE = "+GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text);//+" and HL_CODE = "+DGRequest.Items[i].Cells[6].Text;
						conn.ExecuteNonQuery();

						conn.QueryString = "delete from trfholiday where HL_DATE = "+GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text);//+" and HL_CODE = "+DGRequest.Items[i].Cells[6].Text;
						conn.ExecuteNonQuery();
						
					}
				}
				else if(rbB.Checked == true)
				{
					conn.QueryString = "delete from trfholiday where HL_DATE = "+GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text);//+" and HL_CODE = "+DGRequest.Items[i].Cells[6].Text;
						conn.ExecuteNonQuery();					
				}
			}	
			BindData();																
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc=9902040102&moduleID=40");
		}

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			BindData();
		}
	}
}
