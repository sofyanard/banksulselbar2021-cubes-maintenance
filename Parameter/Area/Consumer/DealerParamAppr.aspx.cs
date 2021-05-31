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
	/// Summary description for DealerParamAppr.
	/// </summary>
	public partial class DealerParamAppr : System.Web.UI.Page
	{
		protected string mid, addr1, addr2, addr3, bid, type, scid;
		protected string ph1, ph2, ph3, fx1, fx2, cty, dzipcode;
		protected string baddr1, baddr2, baddr3, bcity, bzipcode;
		protected string dpremi, ddealer, src, acc_no;
		protected string insub, spv, manager, npwp;
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
			conn.QueryString = "select a.ID_DEALER, a.CITY_ID, a.DLI_CODE, a.NM_DEALER, isnull(a.DL_ADDR1,'')+' '+isnull(a.DL_ADDR2,'')+' '+isnull(a.DL_ADDR3,'') as ADDRESS, a.DL_SRCCODE, "+
				"a.CH_STA, STATUS = case a.CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' when '3' then 'DELETE' end, "+
				"(select DLI_DESC from DEALER_INDUK where DEALER_INDUK.DLI_CODE = a.DLI_CODE) DLI_NAME "+
				"from TDEALER a left join RFBANK b on a.DL_BANKNM = b.BANKID"; 
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
				string did = DG_APPR.Items[row].Cells[0].Text.Trim();
				string cid = DG_APPR.Items[row].Cells[1].Text.Trim();
			
				conn.QueryString = "DELETE FROM TDEALER where CITY_ID = '"+cid+"' and ID_DEALER = '"+did+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void performRequest(int row, string userid)
		{
			int rst = 0;

			string did = DG_APPR.Items[row].Cells[0].Text.Trim();
			string cid = DG_APPR.Items[row].Cells[1].Text.Trim();
			string dlicode = DG_APPR.Items[row].Cells[2].Text.Trim();
			string dname = cleansText(DG_APPR.Items[row].Cells[4].Text);
			string status = DG_APPR.Items[row].Cells[8].Text.Trim();
			
			conn.QueryString = "select * from TDEALER where CITY_ID = '"+cid+"' and ID_DEALER = '"+did+"'";
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0)
			{
				addr1 = conn.GetFieldValue(0,"DL_ADDR1");
				addr2 = conn.GetFieldValue(0,"DL_ADDR2");
				addr3 = conn.GetFieldValue(0,"DL_ADDR3");
				acc_no =  conn.GetFieldValue(0,"DL_ACCNUM");
				baddr1 = conn.GetFieldValue(0,"DL_BANKADDR"); 
				baddr2 = conn.GetFieldValue(0,"DL_BANKADDR2"); 
				baddr3 = conn.GetFieldValue(0,"DL_BANKADDR3");
				bcity = conn.GetFieldValue(0,"DL_BANKCITY"); 
				dzipcode = conn.GetFieldValue(0,"DL_ZIPCODE");
				bzipcode = conn.GetFieldValue(0,"DL_BANKZIPCODE");
				cty = conn.GetFieldValue(0,"DL_CITY");
				ddealer = conn.GetFieldValue(0,"DL_DISCDEALER");
				dpremi = conn.GetFieldValue(0,"DL_DISCPREMI");
				fx1 = conn.GetFieldValue(0,"DL_FAXAREA");
				fx2 = conn.GetFieldValue(0,"DL_FAXNUM");
				manager = conn.GetFieldValue(0,"DL_MANAGER");
				npwp = conn.GetFieldValue(0,"DL_NPWP");
				ph1 = conn.GetFieldValue(0,"DL_PHNAREA");
				ph2 = conn.GetFieldValue(0,"DL_PHNNUM");
				ph3 = conn.GetFieldValue(0,"DL_PHNEXT");
				insub = conn.GetFieldValue(0,"DL_INTSUB");
				src = conn.GetFieldValue(0,"DL_SRCCODE");
				spv = conn.GetFieldValue(0,"DL_SALSUPVIS");
				bid = conn.GetFieldValue(0,"DL_BANKNM");
				type = conn.GetFieldValue(0,"DL_TYPE");
			}

			rst = row;

			conn.ClearData();

			/* coding for audittrial parameter */
			
			conn.QueryString = "EXEC PARAM_AREA_DEALER_AUDIT '"+did+"','"+cid+"','"+status+"','"+userid+"',"+GlobalTools.ConvertNull(dlicode)+","+
				GlobalTools.ConvertNull(dname)+","+GlobalTools.ConvertNull(addr1)+","+GlobalTools.ConvertNull(addr2)+","+
				GlobalTools.ConvertNull(addr3)+","+GlobalTools.ConvertNull(cty)+","+
				GlobalTools.ConvertNull(dzipcode)+","+GlobalTools.ConvertNull(ph1)+","+
				GlobalTools.ConvertNull(ph2)+","+GlobalTools.ConvertNull(ph3)+","+
				GlobalTools.ConvertNull(fx1)+","+GlobalTools.ConvertNull(fx2)+","+
				GlobalTools.ConvertNull(npwp)+","+GlobalTools.ConvertNull(manager)+","+
				GlobalTools.ConvertNull(spv)+","+GlobalTools.ConvertNull(bid)+","+
				GlobalTools.ConvertNull(baddr1)+","+GlobalTools.ConvertNull(baddr2)+","+
				GlobalTools.ConvertNull(baddr3)+","+GlobalTools.ConvertNull(bcity)+","+
				GlobalTools.ConvertNull(bzipcode)+","+GlobalTools.ConvertNull(acc_no)+","+
				GlobalTools.ConvertFloat(dpremi)+","+GlobalTools.ConvertFloat(ddealer)+","+
				GlobalTools.ConvertFloat(insub)+","+GlobalTools.ConvertNull(src)+","+GlobalTools.ConvertNull(type);
			conn.ExecuteNonQuery();
			
			/* end of coding */
			
			if (status.Equals("1"))
			{
				try
				{
					conn.QueryString = "INSERT INTO DEALER(ID_DEALER, CITY_ID, DLI_CODE, NM_DEALER, DL_ADDR1, DL_ADDR2, DL_ADDR3, DL_CITY, "+
						"DL_ZIPCODE, DL_PHNAREA, DL_PHNNUM, DL_PHNEXT, DL_FAXAREA, DL_FAXNUM, DL_NPWP, DL_MANAGER, "+ 
						"DL_SALSUPVIS, DL_BANKADDR, DL_BANKADDR2, DL_BANKADDR3, DL_BANKCITY, DL_BANKZIPCODE, DL_ACCNUM, "+
						"DL_DISCPREMI, DL_DISCDEALER, DL_INTSUB, DL_SRCCODE, DL_TYPE, DL_BANKNM,ACTIVE) "+
						"VALUES('"+did+"', '"+cid+"', '"+dlicode+"', "+GlobalTools.ConvertNull(dname)+", "+GlobalTools.ConvertNull(addr1)+", "+
						" "+GlobalTools.ConvertNull(addr2)+", "+GlobalTools.ConvertNull(addr3)+", "+GlobalTools.ConvertNull(cty)+", "+
						" "+GlobalTools.ConvertNull(dzipcode)+", "+GlobalTools.ConvertNull(ph1)+", "+GlobalTools.ConvertNull(ph2)+", "+
						" "+GlobalTools.ConvertNull(ph3)+", "+GlobalTools.ConvertNull(fx1)+", "+GlobalTools.ConvertNull(fx2)+", "+ 
						" "+GlobalTools.ConvertNull(npwp)+", "+GlobalTools.ConvertNull(manager)+", "+GlobalTools.ConvertNull(spv)+", "+
						" "+GlobalTools.ConvertNull(baddr1)+", "+GlobalTools.ConvertNull(baddr2)+", "+
						" "+GlobalTools.ConvertNull(baddr3)+", "+GlobalTools.ConvertNull(bcity)+", "+GlobalTools.ConvertNull(bzipcode)+", "+
						" "+GlobalTools.ConvertNull(acc_no)+", "+GlobalTools.ConvertFloat(dpremi)+", "+GlobalTools.ConvertFloat(ddealer)+", "+
						" "+GlobalTools.ConvertFloat(insub)+", "+GlobalTools.ConvertNull(src)+", "+GlobalTools.ConvertNull(type)+", "+GlobalTools.ConvertNull(bid)+",'1')"; 

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
					conn.QueryString = "UPDATE DEALER SET DLI_CODE = "+GlobalTools.ConvertNull(dlicode)+", NM_DEALER = "+GlobalTools.ConvertNull(dname)+", "+ 
						"DL_ADDR1 = "+GlobalTools.ConvertNull(addr1)+", DL_ADDR2 = "+GlobalTools.ConvertNull(addr2)+", "+ 
						"DL_ADDR3 = "+GlobalTools.ConvertNull(addr3)+", DL_CITY = "+GlobalTools.ConvertNull(cty)+", "+ 
						"DL_ZIPCODE = "+GlobalTools.ConvertNull(dzipcode)+", DL_PHNAREA = "+GlobalTools.ConvertNull(ph1)+", "+
						"DL_PHNNUM = "+GlobalTools.ConvertNull(ph2)+", DL_PHNEXT = "+GlobalTools.ConvertNull(ph3)+", "+
						"DL_FAXAREA = "+GlobalTools.ConvertNull(fx1)+", DL_FAXNUM = "+GlobalTools.ConvertNull(fx2)+", "+ 
						"DL_NPWP = "+GlobalTools.ConvertNull(npwp)+", DL_MANAGER = "+GlobalTools.ConvertNull(manager)+", "+
						"DL_SALSUPVIS = "+GlobalTools.ConvertNull(spv)+", DL_BANKNM = "+GlobalTools.ConvertNull(bid)+", "+ 
						"DL_BANKADDR = "+GlobalTools.ConvertNull(addr1)+", DL_BANKADDR2 = "+GlobalTools.ConvertNull(addr2)+", "+ 
						"DL_BANKADDR3 = "+GlobalTools.ConvertNull(addr3)+", DL_BANKCITY = "+GlobalTools.ConvertNull(bcity)+", "+
						"DL_BANKZIPCODE = "+GlobalTools.ConvertNull(bzipcode)+", DL_ACCNUM = "+GlobalTools.ConvertNull(acc_no)+", "+
						"DL_DISCPREMI = "+GlobalTools.ConvertFloat(dpremi)+", DL_DISCDEALER = "+GlobalTools.ConvertFloat(ddealer)+", "+
						"DL_INTSUB = "+GlobalTools.ConvertFloat(insub)+", "+
						"DL_SRCCODE = "+GlobalTools.ConvertNull(src)+", DL_TYPE = "+GlobalTools.ConvertNull(type)+" "+
						"WHERE CITY_ID = '"+cid+"' and ID_DEALER = '"+did+"'";
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
					conn.QueryString = "UPDATE DEALER SET ACTIVE='0' where CITY_ID = '"+cid+"' and ID_DEALER = '"+did+"'";
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
			this.DG_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DG_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

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

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_APPR.CurrentPageIndex = e.NewPageIndex;
			
			BindData(); 
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
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
