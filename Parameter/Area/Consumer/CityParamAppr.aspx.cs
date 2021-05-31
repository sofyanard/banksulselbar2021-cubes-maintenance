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
	/// Summary description for CityParamAppr.
	/// </summary>
	public partial class CityParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid, addr, scid;
		protected string ph1, ph2, ph3;
		protected string cp, sbc;
	
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
			conn.QueryString = "select *, isnull(PHONEAREA,'')+' '+isnull(PHONE,'')+' '+isnull(PHONEEXT,'') as PHONE, "+ 
				"STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end from TCITY ORDER BY CITY_ID";
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

		private void deleteData(int row)
		{
			try 
			{
				string aid = DG_APPR.Items[row].Cells[0].Text.Trim();
				string cid = DG_APPR.Items[row].Cells[1].Text.Trim();

				conn.QueryString = "DELETE FROM TCITY WHERE CITY_ID = '"+cid+"' AND AREA_ID = '"+aid+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void performRequest(int row, string userid)
		{
			int rst = 0;

			string aid = DG_APPR.Items[row].Cells[0].Text.Trim();
			string cid = DG_APPR.Items[row].Cells[1].Text.Trim();
			string name = cleansText(DG_APPR.Items[row].Cells[2].Text);
			string status = DG_APPR.Items[row].Cells[6].Text.Trim();

			conn.QueryString = "select * from TCITY where CITY_ID = '"+cid+"' and AREA_ID = '"+aid+"'";
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0)
			{
				addr = conn.GetFieldValue(0,"ADDRESS");
				cp =  conn.GetFieldValue(0,"CONTACT_PERSON");
				ph1 = conn.GetFieldValue(0,"PHONEAREA"); 
				ph2 = conn.GetFieldValue(0,"PHONE"); 
				ph3 = conn.GetFieldValue(0,"PHONEEXT");
				sbc = conn.GetFieldValue(0,"CD_SIBS"); 						
			}

			rst = row;

			conn.ClearData();

			/* coding for audittrial parameter */
			
			conn.QueryString = "EXEC PARAM_AREA_CITY_AUDIT '"+cid+"','"+aid+"','"+status+"','"+userid+"',"+GlobalTools.ConvertNull(ph3)+","+
				GlobalTools.ConvertNull(ph1)+","+GlobalTools.ConvertNull(ph2)+","+GlobalTools.ConvertNull(cp)+","+
				GlobalTools.ConvertNull(addr)+","+GlobalTools.ConvertNull(name)+","+
				GlobalTools.ConvertNull(sbc);
			conn.ExecuteNonQuery();
			
			/* end of coding */
			
			if (status.Equals("1"))
			{
				try
				{
					conn.QueryString = "insert into CITY (CITY_ID, AREA_ID, CITY_NAME, PHONEAREA, PHONE, PHONEEXT, CONTACT_PERSON, ADDRESS, CD_SIBS, ACTIVE) "+
						"values ('"+cid+"', '"+aid+"', "+GlobalTools.ConvertNull(name)+", "+GlobalTools.ConvertNull(ph1)+", "+GlobalTools.ConvertNull(ph2)+","+ 
						" "+GlobalTools.ConvertNull(ph3)+", "+GlobalTools.ConvertNull(cp)+", "+GlobalTools.ConvertNull(addr)+", "+GlobalTools.ConvertNull(sbc)+",'1')"; 
					
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
					conn.QueryString = "UPDATE CITY SET CITY_NAME = "+GlobalTools.ConvertNull(name)+", PHONEAREA = "+GlobalTools.ConvertNull(ph1)+", "+
						"PHONE = "+GlobalTools.ConvertNull(ph2)+", PHONEEXT = "+GlobalTools.ConvertNull(ph3)+", CONTACT_PERSON = "+GlobalTools.ConvertNull(cp)+", "+
						"ADDRESS = "+GlobalTools.ConvertNull(addr)+", CD_SIBS = "+GlobalTools.ConvertNull(sbc)+" WHERE  CITY_ID = '"+cid+"' and AREA_ID = '"+aid+"'";
					
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
					conn.QueryString = "UPDATE CITY SET ACTIVE='0' WHERE CITY_ID = '"+cid+"' AND AREA_ID = '"+aid+"'";
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
