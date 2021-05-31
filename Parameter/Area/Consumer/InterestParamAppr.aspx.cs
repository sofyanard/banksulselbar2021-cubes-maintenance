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
	/// Summary description for InterestParamAppr.
	/// </summary>
	public partial class InterestParamAppr : System.Web.UI.Page
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
			conn.QueryString = "select A.*, B.IN_DESC, TN.TN_CODE, TN.TN_DESC, "+
				"STATUS = case A.CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TINTEREST A join RFINTTYPE B on A.IN_TYPE = B.IN_TYPE "+ 
				"join RFCAWTENOR TN on TN.PRODUCTID =  A.PRODUCTID and TN.PR_CODE = A.PR_CODE "+
				"and TN.TN_SEQ = A.TN_SEQ";

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
				string seq = DG_APPR.Items[row].Cells[0].Text.Trim();
				string code = DG_APPR.Items[row].Cells[1].Text.Trim();
				string pid = DG_APPR.Items[row].Cells[2].Text.Trim();
				string intype = DG_APPR.Items[row].Cells[8].Text.Trim();

				conn.QueryString = "DELETE FROM TINTEREST WHERE PRODUCTID = '"+pid+"' "+
					"AND PR_CODE = '"+code+"' AND TN_SEQ = '"+seq+"' AND IN_TYPE = '"+intype+"'";

				conn.ExecuteQuery();
				 
			} 
			catch {}

			conn.ClearData();
		}

		private void performRequest(int row, string userid)
		{
			int rst = 0;
			
			string pid = DG_APPR.Items[row].Cells[2].Text.Trim();
			string seq = DG_APPR.Items[row].Cells[0].Text.Trim();
			string prcode = DG_APPR.Items[row].Cells[1].Text.Trim();
			string inval = cleansFloat(DG_APPR.Items[row].Cells[4].Text);
			string inttype = cleansText(DG_APPR.Items[row].Cells[8].Text);
			string status = DG_APPR.Items[row].Cells[7].Text.Trim();

			rst = row;

			/* coding for audittrial parameter */
            
			conn.QueryString = "EXEC PARAM_AREA_INTEREST_AUDIT '"+prcode+"','"+pid+"','"+seq+"','"+inttype+"','"+status+"','"+userid+"',"+GlobalTools.ConvertFloat(inval);
			conn.ExecuteNonQuery();
			
			/* end of coding */
			
			if (status.Equals("1"))
			{
				try
				{
					conn.QueryString = "insert into INTEREST values ('"+prcode+"', '"+pid+"', '"+seq+
						"', '"+inttype+"', null, "+GlobalTools.ConvertFloat(inval)+", 0, 0, 0, null,'1')";
					
					conn.ExecuteQuery();			
				}
				catch
				{
					GlobalTools.popMessage(this,"Cannot approve, data is already exist!");
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
					conn.QueryString = "update INTEREST set IN_VALUE = "+GlobalTools.ConvertFloat(inval)+", IN_TYPE = '"+inttype+"' "+  
						"where PR_CODE = '"+prcode+"' and TN_SEQ = '"+seq+"' and PRODUCTID = '"+pid+"' and IN_TYPE = '"+inttype+"'";

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
					conn.QueryString = "UPDATE INTEREST SET ACTIVE='0' WHERE PRODUCTID = '"+pid+"' "+
						"AND PR_CODE = '"+prcode+"' AND TN_SEQ = '"+seq+"' AND IN_TYPE = '"+inttype+"'";

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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamApprovalAll.aspx?mc=9902040202&moduleId=40");
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
	}
}
