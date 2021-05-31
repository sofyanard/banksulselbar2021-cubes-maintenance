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
	/// Summary description for HousingLoanParamAppr.
	/// </summary>
	public partial class HousingLoanParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid, desc, nb_param, hospital, school;
		protected string shop, lake, park, sport, remark, scid;
		protected string srccode, grline, plafond, cid, locid, cdsibs;
	
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
			/* 20070717 change by sofyan for kpr developer enhancement
			conn.QueryString = "select PROYEK_ID, DEVELOPER_ID, ID_KOTA, ID_LOKASI, PROYEK_DESCRIPTION, "+
				"(select lokasi from lokasi_perumahan "+ 
				"where lokasi_perumahan.ID_LOKASI=TPROYEK_HOUSINGLOAN.ID_LOKASI) LOKASI, "+ 
				"(select DV_NAME  from developer where developer.DV_CODE=TPROYEK_HOUSINGLOAN.Developer_ID) DEVELOPER, "+
				"PH_GRLINE, PH_PLAFOND, PH_SRCCODE, CD_SIBS, CH_STA, STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' when '3' "+
				"then 'DELETE' end from TPROYEK_HOUSINGLOAN";
			*/
			conn.QueryString = "exec PARAM_HOUSINGLOAN_VIEWREQUEST ''";
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
				string proid = DG_APPR.Items[row].Cells[0].Text.Trim();
				string dvid = DG_APPR.Items[row].Cells[1].Text.Trim();

				conn.QueryString = "DELETE FROM TPROYEK_HOUSINGLOAN WHERE PROYEK_ID = '"+proid+"' AND DEVELOPER_ID = '"+dvid+"'"; 
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void performRequest(int row, string userid)
		{
			int rst = 0;

			string pid = DG_APPR.Items[row].Cells[0].Text.Trim();
			string devid = DG_APPR.Items[row].Cells[1].Text.Trim();
			string status = DG_APPR.Items[row].Cells[12].Text.Trim();

			//20070717 remark by sofyan, for kpr developer enhancement
			/* move into procedure
			conn.QueryString = "SELECT * FROM TPROYEK_HOUSINGLOAN WHERE PROYEK_ID = '"+pid+"' AND DEVELOPER_ID = '"+devid+"'"; 
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0)
			{
				cid = conn.GetFieldValue(0,"ID_KOTA");
				locid = conn.GetFieldValue(0,"ID_LOKASI");
				desc = conn.GetFieldValue(0,"PROYEK_DESCRIPTION");
				nb_param = conn.GetFieldValue(0,"NUMBER_PARAM");
				hospital = conn.GetFieldValue(0,"RUMAH_SAKIT");
				school = conn.GetFieldValue(0,"SEKOLAH");
				shop = conn.GetFieldValue(0,"PUSAT_BELANJA");
				lake = conn.GetFieldValue(0,"DANAU");
				park = conn.GetFieldValue(0,"TAMAN");
				sport = conn.GetFieldValue(0,"OLAH_RAGA");
				remark = conn.GetFieldValue(0,"REMARK");
				srccode = conn.GetFieldValue(0,"PH_SRCCODE");
				grline = conn.GetFieldValue(0,"PH_GRLINE");
				plafond = conn.GetFieldValue(0,"PH_PLAFOND");
				cdsibs = conn.GetFieldValue(0,"CD_SIBS");
			}
			*/

			rst = row;

			//20070717 remark by sofyan, for kpr developer enhancement
			/* move into procedure
			conn.ClearData();

			

			coding for audittrial parameter
			
			conn.QueryString = "EXEC PARAM_AREA_PROYEK_HOUSINGLOAN_AUDIT '"+pid+"','"+devid+"','"+status+"','"+userid+"',"+
				GlobalTools.ConvertNull(cid)+","+GlobalTools.ConvertNull(locid)+","+
				GlobalTools.ConvertNull(desc)+","+GlobalTools.ConvertFloat(nb_param)+","+
				GlobalTools.ConvertNull(hospital)+","+GlobalTools.ConvertNull(school)+","+
				GlobalTools.ConvertNull(shop)+","+GlobalTools.ConvertNull(lake)+","+
				GlobalTools.ConvertNull(park)+","+GlobalTools.ConvertNull(sport)+","+
				GlobalTools.ConvertNull(remark)+","+GlobalTools.ConvertNull(srccode)+","+
				GlobalTools.ConvertFloat(grline)+","+GlobalTools.ConvertFloat(plafond)+","+
				GlobalTools.ConvertNull(cdsibs);

			conn.ExecuteNonQuery();
			
			end of coding 
			*/
			
			if (status.Equals("1"))
			{
				try
				{
					/*20070717 remark by sofyan for kpr developer enhancement
					conn.QueryString = "INSERT INTO PROYEK_HOUSINGLOAN VALUES('"+pid+"','"+devid+"',"+GlobalTools.ConvertNull(cid)+
						", "+GlobalTools.ConvertNull(locid)+", "+GlobalTools.ConvertNull(desc)+","+GlobalTools.ConvertNull(nb_param)+
						", "+GlobalTools.ConvertNull(hospital)+", "+GlobalTools.ConvertNull(school)+", "+GlobalTools.ConvertNull(shop)+
						", "+GlobalTools.ConvertNull(lake)+", "+GlobalTools.ConvertNull(park)+", "+GlobalTools.ConvertNull(sport)+
						", "+GlobalTools.ConvertNull(remark)+", "+GlobalTools.ConvertNull(srccode)+", "+GlobalTools.ConvertFloat(grline)+
						", "+GlobalTools.ConvertFloat(plafond)+",'1', "+GlobalTools.ConvertNull(cdsibs)+")";
					*/
					conn.QueryString = "exec PARAM_HOUSINGLOAN_APPRUPDATE 1, '" + pid + "', '" + devid + "', '" + userid + "'";
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
					/*20070717 remark by sofyan for kpr developer enhancement
					conn.QueryString = "UPDATE PROYEK_HOUSINGLOAN SET ID_KOTA = "+GlobalTools.ConvertNull(cid)+", "+ 
						"ID_LOKASI = "+GlobalTools.ConvertNull(locid)+", "+ 
						"PROYEK_DESCRIPTION = "+GlobalTools.ConvertNull(desc)+", NUMBER_PARAM = "+GlobalTools.ConvertNull(nb_param)+", "+
						"RUMAH_SAKIT = "+GlobalTools.ConvertNull(hospital)+", SEKOLAH = "+GlobalTools.ConvertNull(school)+", PUSAT_BELANJA = "+GlobalTools.ConvertNull(shop)+", "+
						"DANAU = "+GlobalTools.ConvertNull(lake)+", TAMAN = "+GlobalTools.ConvertNull(park)+", OLAH_RAGA = "+GlobalTools.ConvertNull(sport)+", REMARK = "+GlobalTools.ConvertNull(remark)+", "+ 
						"PH_SRCCODE = "+GlobalTools.ConvertNull(srccode)+", PH_GRLINE = "+GlobalTools.ConvertFloat(grline)+", "+
						"PH_PLAFOND = "+GlobalTools.ConvertFloat(plafond)+", CD_SIBS = "+GlobalTools.ConvertNull(cdsibs)+" "+ 
						"WHERE PROYEK_ID = '"+pid+"' "+
						"AND DEVELOPER_ID = '"+devid+"'";
					*/
					conn.QueryString = "exec PARAM_HOUSINGLOAN_APPRUPDATE 2, '" + pid + "', '" + devid + "', '" + userid + "'";
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
					/*20070717 remark by sofyan for kpr developer enhancement
					conn.QueryString = "UPDATE PROYEK_HOUSINGLOAN SET ACTIVE='0' WHERE PROYEK_ID = '"+pid+"' AND DEVELOPER_ID = '"+devid+"'"; 
					*/
					conn.QueryString = "exec PARAM_HOUSINGLOAN_APPRUPDATE 3, '" + pid + "', '" + devid + "', '" + userid + "'";
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
