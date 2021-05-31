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

namespace CuBES_Maintenance.Parameter.Area.Consumer
{
	/// <summary>
	/// Summary description for MRAgnecyOfficerParamAppr.
	/// </summary>
	public partial class MRAgnecyOfficerParamAppr : System.Web.UI.Page
	{
		protected Connection conn2;// = new Connection(ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn;
		protected string mid, scid;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon();
			if(!IsPostBack)
			{
				ViewData(); 
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
		
		private void ViewData()
		{
			conn.QueryString = "select a.*, b.CITY_ID, b.agencyname,  "+ 
				"STATUS = CASE a.CH_STA WHEN '1' THEN 'INSERT' "+
				"WHEN '2' THEN 'UPDATE' WHEN '3' THEN 'DELETE' END "+
				"from TRFAGENCYOFR a join TAGENCY b on a.AGENCYID = b.AGENCYID AND b.AGENCYTYPE = '7' ";
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
			Response.Redirect("../../AreaParamApprovalAll.aspx?mc=9902040202&moduleId=40");
		}

		private void deleteData(int row)
		{
			try 
			{
				string agencyid = DG_APPR.Items[row].Cells[0].Text.Trim();
				string id = DG_APPR.Items[row].Cells[2].Text.Trim();

				conn.QueryString = "DELETE FROM TRFAGENCYOFR WHERE AGENCYID = '"+agencyid+"' AND AGOFR_CODE = '"+id+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void performRequest(int row, string userid)
		{
			int rst = 0;
			
			string agencyid = DG_APPR.Items[row].Cells[0].Text.Trim();
			string id = DG_APPR.Items[row].Cells[2].Text.Trim();
			string name = DG_APPR.Items[row].Cells[3].Text.Trim();
			string status = DG_APPR.Items[row].Cells[7].Text.Trim();
			
			rst = row;

			/* coding for audittrial parameter */
			
			conn.QueryString = "EXEC PARAM_AREA_AGENCYOFFICER_AUDIT '"+agencyid+"','"+id+"','"+name+"','"+status+"','"+userid+"'";
			
			conn.ExecuteNonQuery();
			
			/* end of coding */
			
			if (status.Equals("1"))
			{
				try
				{
					conn.QueryString = "INSERT INTO RFAGENCYOFR VALUES('"+agencyid+"','"+id+
						"','"+name+"',NULL,NULL,NULL,NULL,NULL,'1')";
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
					conn.QueryString = "UPDATE RFAGENCYOFR SET AGOFR_DESC = '"+name+"' where AGENCYID = '"+agencyid+"' and "+
						"AGOFR_CODE = '"+id+"'";  	
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
					conn.QueryString = "UPDATE RFAGENCYOFR SET ACTIVE='0' WHERE AGENCYID = '"+agencyid+"' and AGOFR_CODE = '"+id+"'";
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
