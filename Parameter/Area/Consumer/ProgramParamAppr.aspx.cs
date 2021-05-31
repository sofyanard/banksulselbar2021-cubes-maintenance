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

namespace CuBES_Maintenance.Parameter.Area.Consumer
{
	/// <summary>
	/// Summary description for ProgramParamAppr.
	/// </summary>
	public partial class ProgramParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid, scid;
	
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
			conn.QueryString = "select a.AREA_ID, a.PR_CODE, a.PR_DESC, a.MIN_PINJAM, "+ 
				"a.MAX_PINJAM, a.PR_EXPIREDATE, a.PR_SRCCODE, a.CMP_CODE, c.CMP_DESC, a.CH_STA, "+
				"STATUS = case a.CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TPROGRAM a left join AREA b on a.AREA_ID = b.AREA_ID "+
				"left join CAMPAIGN c on a.CMP_CODE = c.CMP_CODE";
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

		private string cleansFloat(string tn)
		{
			if (tn.Trim() == "&nbsp;")
				tn = "0";
			return tn;
		}

		private void deleteData(int row)
		{
			try 
			{
				string aid = DG_APPR.Items[row].Cells[12].Text.Trim();
				string code = DG_APPR.Items[row].Cells[0].Text.Trim();

				conn.QueryString = "DELETE FROM TPROGRAM WHERE AREA_ID = '"+aid+"' AND PR_CODE = '"+code+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void performRequest(int row, string userid)
		{
			int rst = 0;
			
			string aid = DG_APPR.Items[row].Cells[12].Text.Trim();
			string prcode = DG_APPR.Items[row].Cells[0].Text.Trim();
			string cmpcode = DG_APPR.Items[row].Cells[13].Text.Trim();
			string prdesc = cleansText(DG_APPR.Items[row].Cells[1].Text);
			string cmpdesc = cleansText(DG_APPR.Items[row].Cells[2].Text);
			string expdate = DG_APPR.Items[row].Cells[3].Text;
			string min = cleansFloat(DG_APPR.Items[row].Cells[4].Text);
			string max = cleansFloat(DG_APPR.Items[row].Cells[5].Text);
			string src = cleansText(DG_APPR.Items[row].Cells[6].Text);
			string status = DG_APPR.Items[row].Cells[8].Text.Trim();

			rst = row;

			/* coding for audittrial parameter */
		    
			conn.QueryString = "EXEC PARAM_AREA_PROGRAM_AUDIT '"+prcode+"','"+aid+"','"+status+"','"+userid+"',"+GlobalTools.ConvertNull(cmpcode)+","+
				GlobalTools.ConvertNull(prdesc)+","+GlobalTools.ConvertFloat(min)+","+GlobalTools.ConvertFloat(max)+","+
				GlobalTools.ToSQLDate(expdate)+","+GlobalTools.ConvertNull(src);
			
			conn.ExecuteNonQuery();
			
			/* end of coding */
			
			if (status.Equals("1"))
			{
				try
				{
					conn.QueryString = "INSERT INTO PROGRAM VALUES('"+prcode+"','"+cmpcode+
						"','"+aid+"',"+GlobalTools.ConvertNull(prdesc)+
						","+GlobalTools.ConvertFloat(min)+
						","+GlobalTools.ConvertFloat(max)+
						","+GlobalTools.ToSQLDate(expdate)+", NULL "+
						","+GlobalTools.ConvertNull(src)+",'1')";		
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
					conn.QueryString = "UPDATE PROGRAM SET CMP_CODE = '"+cmpcode+"', PR_DESC = '"+prdesc+
						"', MIN_PINJAM = "+GlobalTools.ConvertFloat(min)+", MAX_PINJAM = "+GlobalTools.ConvertFloat(max)+
						", PR_EXPIREDATE = "+GlobalTools.ToSQLDate(expdate)+
						", PR_SRCCODE = "+GlobalTools.ConvertNull(src)+" WHERE PR_CODE = '"+prcode+
						"' AND AREA_ID = '"+aid+"'";  	
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
					conn.QueryString = "UPDATE PROGRAM SET ACTIVE='0' WHERE AREA_ID = '"+aid+"' AND PR_CODE = '"+prcode+"'";
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
			Response.Redirect("../../AreaParamApprovalAll.aspx?mc=9902040202&moduleId=40");
		}
	}
}
