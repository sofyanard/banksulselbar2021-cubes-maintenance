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
	/// Summary description for DeveloperParamAppr.
	/// </summary>
	public partial class DeveloperParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid, addr1, addr2, addr3, scid;
		protected string city, zipcode, ph1, ph2, ph3;
		protected string fn1, fn2, cid, kerjasama, blocked;
		protected string grline, totalgr, intsub, srccode,cdsibs;
	
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
			conn.QueryString = "select dv_code, dv_name, dv_srccode, "+
				"case dv_kerjasama when '1' then 'Yes' when '0' then 'No' end DV_KERJASAMA, "+ 
				"case dv_blocked when '1' then 'Yes' else 'No' end DV_BLOCKED, "+
				"isnull(DV_GRLINE, 0) DV_GRLINE, (isnull(DV_GRLINE, 0) - isnull(DV_TOTGRLINE, 0)) as SISA, "+
				"CH_STA, STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' "+
				"when 3 then 'DELETE' end from tdeveloper "+ 
				//"left join proyek_housingloan on tdeveloper.dv_code = proyek_housingloan.developer_id "+ 
				"order by dv_code";

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

			for (int i = 0; i < DG_APPR.Items.Count; i++)
				DG_APPR.Items[i].Cells[0].Text = (i+1+(DG_APPR.CurrentPageIndex*DG_APPR.PageSize)).ToString();
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
				string code = DG_APPR.Items[row].Cells[1].Text.Trim();

				conn.QueryString = "DELETE FROM TDEVELOPER WHERE DV_CODE = '"+code+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void performRequest(int row, string userid)
		{
			int rst = 0;

			string code = DG_APPR.Items[row].Cells[1].Text.Trim();
			string name = cleansText(DG_APPR.Items[row].Cells[2].Text);
			string status = DG_APPR.Items[row].Cells[9].Text.Trim();

			//20070713 remark by sofyan, for developer enhancement
			/* move into procedure
			conn.QueryString = "SELECT * FROM TDEVELOPER WHERE DV_CODE = '"+code+"'";
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0)
			{
				addr1 = conn.GetFieldValue(0,"DV_ADDR1");
				addr2 = conn.GetFieldValue(0,"DV_ADDR2");
				addr3 = conn.GetFieldValue(0,"DV_ADDR3");	
				
				fn1 = conn.GetFieldValue(0,"DV_FAXAREA");
				fn2 = conn.GetFieldValue(0,"DV_FAXNUM"); 
				
				ph1 = conn.GetFieldValue(0,"DV_PHNAREA"); 
				ph2 = conn.GetFieldValue(0,"DV_PHNNUM"); 
				ph3 = conn.GetFieldValue(0,"DV_PHNEXT");
 
				city = conn.GetFieldValue(0,"DV_CITY");

				zipcode = conn.GetFieldValue(0,"DV_ZIPCODE");
				srccode = conn.GetFieldValue(0,"DV_SRCCODE");
				cid = conn.GetFieldValue(0,"CITY_ID");

				grline = conn.GetFieldValue(0,"DV_GRLINE");
				intsub = conn.GetFieldValue(0,"DV_INTSUB"); 
				totalgr = conn.GetFieldValue(0,"DV_TOTGRLINE"); 
				kerjasama = conn.GetFieldValue(0,"DV_KERJASAMA");
				blocked = conn.GetFieldValue(0,"DV_BLOCKED");
				cdsibs = conn.GetFieldValue(0,"CD_SIBS");
			}
			*/

			rst = row;

			//20070713 remark by sofyan, for developer enhancement
			/* move into procedure
			conn.ClearData();

			coding for audittrial parameter
			
			conn.QueryString = "EXEC PARAM_AREA_DEVELOPER_AUDIT '"+code+"','"+status+"','"+userid+"',"+GlobalTools.ConvertNull(name)+","+
				GlobalTools.ConvertNull(addr1)+","+GlobalTools.ConvertNull(addr2)+","+
				GlobalTools.ConvertNull(addr3)+","+GlobalTools.ConvertNull(city)+","+
				GlobalTools.ConvertNull(zipcode)+","+GlobalTools.ConvertNull(ph1)+","+
				GlobalTools.ConvertNull(ph2)+","+GlobalTools.ConvertNull(ph3)+","+
				GlobalTools.ConvertNull(fn1)+","+GlobalTools.ConvertNull(fn2)+","+
				GlobalTools.ConvertNull(cid)+","+GlobalTools.ConvertFloat(grline)+","+
				GlobalTools.ConvertFloat(totalgr)+","+GlobalTools.ConvertFloat(intsub)+","+
				GlobalTools.ConvertNull(srccode)+","+GlobalTools.ConvertNull(kerjasama)+","+
				GlobalTools.ConvertFloat(blocked)+",'"+cdsibs+"'";
			conn.ExecuteNonQuery();
		    
			end of coding
			*/
			
			if (status.Equals("1"))
			{
				try
				{
					/*20070713 remark by sofyan for developer enhancement
					conn.QueryString = "insert into DEVELOPER values "+
						"('"+code+"',"+GlobalTools.ConvertNull(name)+","+GlobalTools.ConvertNull(addr1)+
						","+GlobalTools.ConvertNull(addr2)+","+GlobalTools.ConvertNull(addr3)+
						","+GlobalTools.ConvertNull(city)+","+GlobalTools.ConvertNull(zipcode)+
						","+GlobalTools.ConvertNull(ph1)+","+GlobalTools.ConvertNull(ph2)+
						","+GlobalTools.ConvertNull(ph3)+","+GlobalTools.ConvertNull(fn1)+
						","+GlobalTools.ConvertNull(fn2)+","+GlobalTools.ConvertNull(cid)+
						",'"+cdsibs+"',"+GlobalTools.ConvertFloat(grline)+","+GlobalTools.ConvertFloat(totalgr)+
						","+GlobalTools.ConvertFloat(intsub)+", null, "+GlobalTools.ConvertNull(srccode)+
						","+GlobalTools.ConvertNull(kerjasama)+","+GlobalTools.ConvertNull(blocked)+",'1')";
					*/
					conn.QueryString = "exec PARAM_DEVELOPER_APPRUPDATE 1, '" + code + "', '" + userid + "'";
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
					/*20070713 remark by sofyan for developer enhancement
					conn.QueryString = "UPDATE DEVELOPER SET DV_NAME = "+GlobalTools.ConvertNull(name)+", "+
						"DV_ADDR1 = "+GlobalTools.ConvertNull(addr1)+", "+ 
						"DV_ADDR2 = "+GlobalTools.ConvertNull(addr2)+", "+
						"DV_ADDR3 = "+GlobalTools.ConvertNull(addr3)+", "+
						"DV_CITY = "+GlobalTools.ConvertNull(city)+", "+ 
						"DV_ZIPCODE = "+GlobalTools.ConvertNull(zipcode)+", "+
						"DV_PHNAREA = "+GlobalTools.ConvertNull(ph1)+", "+ 
						"DV_PHNNUM = "+GlobalTools.ConvertNull(ph2)+", "+
						"DV_PHNEXT = "+GlobalTools.ConvertNull(ph3)+", "+
						"DV_FAXAREA = "+GlobalTools.ConvertNull(fn1)+", "+ 
						"DV_FAXNUM = "+GlobalTools.ConvertNull(fn2)+", "+
						"CITY_ID = "+GlobalTools.ConvertNull(cid)+", "+
						"DV_GRLINE = "+GlobalTools.ConvertFloat(grline)+", "+
						"DV_TOTGRLINE = "+GlobalTools.ConvertFloat(totalgr)+", "+
						"DV_INTSUB = "+GlobalTools.ConvertFloat(intsub)+", "+
						"DV_SRCCODE = "+srccode+", "+
						"DV_KERJASAMA = '"+kerjasama+"', "+
						"DV_BLOCKED = '"+blocked+"', "+
						"CD_SIBS = '"+cdsibs+"' "+
						"WHERE DV_CODE = '"+code+"'";
					*/
					conn.QueryString = "exec PARAM_DEVELOPER_APPRUPDATE 2, '" + code + "', '" + userid + "'";
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
					/*20070713 remark by sofyan for developer enhancement
					conn.QueryString = "UPDATE DEVELOPER SET ACTIVE='0' WHERE DV_CODE = '"+code+"'";
					*/
					conn.QueryString = "exec PARAM_DEVELOPER_APPRUPDATE 3, '" + code + "', '" + userid + "'";
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamApprovalAll.aspx?mc=9902040202&moduleId=40");
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
			
			BindData(); 	
		}
	}
}
