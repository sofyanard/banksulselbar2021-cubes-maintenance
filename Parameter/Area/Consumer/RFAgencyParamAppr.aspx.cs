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
	/// Summary description for RFAgencyParamAppr.
	/// </summary>
	public partial class RFAgencyParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid, agname, addr1, addr2, addr3, scid;
		protected string ph1, ph2, ph3, fn1, fn2, city;
		protected string cp, hp, zipcode, mktsrc, email, cdsibs;
	
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
			LBL_TIPE.Text = Request.QueryString["tagen"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();

			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			InitialCon(); 

			BindData();

			switch(LBL_TIPE.Text.Trim())
			{
				case "1":
					LBL_PARAMNAME.Text = "Verificator Agency"; 
					break;
				case "2":
					LBL_PARAMNAME.Text = "Appraisal Agency"; 
					break;
				case "3":
					LBL_PARAMNAME.Text = "Investigator Agency"; 
					break;
				case "4":
					LBL_PARAMNAME.Text = "Notaris Agency"; 
					break;
				case "7":
					LBL_PARAMNAME.Text = "MR Agency"; 
					break;
			}
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData()
		{
			conn.QueryString = "select AGENCYID, AGENCYTYPE, AGENCYNAME, CITY_ID, "+
				"isnull(ADDRESS1,'')+' '+isnull(ADDRESS2,'')+' '+isnull(ADDRESS3,'') ADDRESS, "+  
				"CH_STA, STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' "+ 
				"when '3' then 'DELETE' end from TTAGENCY where AGENCYTYPE = '"+LBL_TIPE.Text+"'";
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
				string id = DG_APPR.Items[row].Cells[0].Text.Trim();
				string type = DG_APPR.Items[row].Cells[1].Text.Trim();
				string cid = DG_APPR.Items[row].Cells[4].Text.Trim();

				conn.QueryString = "DELETE FROM TTAGENCY WHERE AGENCYID = '"+id+"' AND AGENCYTYPE = '"+type+
						"' AND CITY_ID = '"+cid+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void performRequest(int row, string userid)
		{
			int rst = 0;

			string id = DG_APPR.Items[row].Cells[0].Text.Trim();
			string type = DG_APPR.Items[row].Cells[1].Text.Trim();
			string cid = DG_APPR.Items[row].Cells[4].Text.Trim();
			string name = cleansText(DG_APPR.Items[row].Cells[2].Text);
			string status = DG_APPR.Items[row].Cells[6].Text.Trim();

			conn.QueryString = "SELECT * FROM TTAGENCY WHERE AGENCYID = '"+id+"' AND AGENCYTYPE = '"+type+"' AND CITY_ID = '"+cid+"'";
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0)
			{
				addr1 = conn.GetFieldValue(0,"ADDRESS1");
				addr2 = conn.GetFieldValue(0,"ADDRESS2");
				addr3 = conn.GetFieldValue(0,"ADDRESS3");	
				cp =  conn.GetFieldValue(0,"CONTACTPERSON");
				email = conn.GetFieldValue(0,"EMAIL"); 
				fn1 = conn.GetFieldValue(0,"FAXAREA");
				fn2 = conn.GetFieldValue(0,"FAXNO"); 
				ph1 = conn.GetFieldValue(0,"PHONEAREA"); 
				ph2 = conn.GetFieldValue(0,"PHONE"); 
				ph3 = conn.GetFieldValue(0,"PHONEEXT"); 
				hp = conn.GetFieldValue(0,"CONTACTHP");
				zipcode = conn.GetFieldValue(0,"ZIPCODE");
				mktsrc = conn.GetFieldValue(0,"AG_SRCCODE");
				city = conn.GetFieldValue(0,"CITY");
				cdsibs = conn.GetFieldValue(0,"CD_SIBS");
			}

			rst = row;

			conn.ClearData();

			/* coding for audittrial parameter */
			
			conn.QueryString = "EXEC PARAM_AREA_AGENCY_AUDIT '"+id+"','"+type+"','"+cid+"','"+status+"','"+userid+"',"+GlobalTools.ConvertNull(name)+","+
				GlobalTools.ConvertNull(addr1)+","+GlobalTools.ConvertNull(addr2)+","+GlobalTools.ConvertNull(addr3)+","+
				GlobalTools.ConvertNull(city)+","+GlobalTools.ConvertNull(zipcode)+","+
				GlobalTools.ConvertNull(email)+","+GlobalTools.ConvertNull(cp)+","+
				GlobalTools.ConvertNull(hp)+","+GlobalTools.ConvertNull(ph1)+","+
				GlobalTools.ConvertNull(ph2)+","+GlobalTools.ConvertNull(ph3)+","+
				GlobalTools.ConvertNull(fn1)+","+GlobalTools.ConvertNull(fn2)+","+
				GlobalTools.ConvertNull(mktsrc);
			conn.ExecuteNonQuery();
			
			/* end of coding */
			
			if (status.Equals("1"))
			{
				try
				{
					conn.QueryString = "insert into TAGENCY(AGENCYID,AGENCYTYPE,AGENCYNAME,CITY_ID,ADDRESS1,ADDRESS2,ADDRESS3,CITY,ZIPCODE, "+                                                                                                
						"EMAIL,CONTACTPERSON,CONTACTHP,PHONEAREA,PHONE,PHONEEXT,FAXAREA,FAXNO,AG_SRCCODE,CD_SIBS,ACTIVE) "+
						" values ('"+id+"','"+type+"',"+GlobalTools.ConvertNull(name)+",'"+cid+"',"+
						" "+GlobalTools.ConvertNull(addr1)+","+GlobalTools.ConvertNull(addr2)+","+GlobalTools.ConvertNull(addr3)+","+
						" "+GlobalTools.ConvertNull(city)+","+GlobalTools.ConvertNull(zipcode)+","+
						" "+GlobalTools.ConvertNull(email)+","+GlobalTools.ConvertNull(cp)+","+
						" "+GlobalTools.ConvertNull(hp)+","+GlobalTools.ConvertNull(ph1)+","+
						" "+GlobalTools.ConvertNull(ph2)+","+GlobalTools.ConvertNull(ph3)+","+
						" "+GlobalTools.ConvertNull(fn1)+","+GlobalTools.ConvertNull(fn2)+","+
						" "+GlobalTools.ConvertNull(mktsrc)+","+GlobalTools.ConvertNull(cdsibs)+",'1')";
					
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
					conn.QueryString = "UPDATE TAGENCY SET AGENCYNAME = "+GlobalTools.ConvertNull(name)+", "+
						"ADDRESS1 = "+GlobalTools.ConvertNull(addr1)+", ADDRESS2 = "+GlobalTools.ConvertNull(addr2)+", "+
						"ADDRESS3 = "+GlobalTools.ConvertNull(addr3)+", CITY = "+GlobalTools.ConvertNull(cid)+", "+ 
						"ZIPCODE = "+GlobalTools.ConvertNull(zipcode)+", EMAIL = "+GlobalTools.ConvertNull(email)+", "+ 
						"CONTACTPERSON = "+GlobalTools.ConvertNull(cp)+", CONTACTHP = "+GlobalTools.ConvertNull(hp)+", "+
						"PHONEAREA = "+GlobalTools.ConvertNull(ph1)+", PHONE = "+GlobalTools.ConvertNull(ph2)+", "+ 
						"PHONEEXT = "+GlobalTools.ConvertNull(ph3)+", FAXAREA = "+GlobalTools.ConvertNull(fn1)+", "+ 
						"FAXNO = "+GlobalTools.ConvertNull(fn2)+", AG_SRCCODE = "+GlobalTools.ConvertNull(mktsrc)+", "+ 
						"CD_SIBS = "+GlobalTools.ConvertNull(cdsibs)+" "+
						"WHERE AGENCYID = '"+id+"' AND AGENCYTYPE = '"+type+"' AND CITY_ID = '"+cid+"'";
					
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
					conn.QueryString = "UPDATE TAGENCY SET ACTIVE='0' WHERE AGENCYID = '"+id+"' AND AGENCYTYPE = '"+type+
						"' AND CITY_ID = '"+cid+"'";
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
	}
}
