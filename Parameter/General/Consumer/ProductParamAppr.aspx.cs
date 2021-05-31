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
	/// Summary description for ProductParamAppr.
	/// Audittrail included, but still in comment
	/// </summary>
	public partial class ProductParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected Connection conn3;
		protected string mid;
		
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

			conn2.QueryString = "select * from rfmodule where moduleid = '50'";
			conn2.ExecuteQuery();

			LBL_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID2.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD2.Text = conn2.GetFieldValue("db_loginpwd");

			InitialCon(); 

			BindData();
			TR_KET.Visible = false;
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
			conn3 = new Connection("Data Source=" + LBL_IP.Text + ";Initial Catalog=" + LBL_DB.Text + ";uid=" + LBL_LOG_ID2.Text + ";pwd=" + LBL_LOG_PWD2.Text + ";Pooling=true");
		}

		private void BindData()
		{
			conn.QueryString = "select PRODUCTID, PRODUCTNAME, CH_STA, "+
				"STATUS = case CH_STA when '1' then 'INSERT' "+
				"when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TTPRODUCT ORDER BY PRODUCTID ASC";
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

		/* fungsi untuk mengecek parameter product di module SalesComm */
		private string CekSales(string proid)
		{
			string exist = "";

			conn3.QueryString = "SELECT * FROM PRODUCT WHERE PRODUCT_ID = '"+proid+"'";
			conn3.ExecuteQuery();
 
			if(conn3.GetRowCount() != 0)
				exist = "yes";
			else
				exist = "no";

			conn3.ClearData();

			return exist;
		}

		private void deleteData(int row)
		{
			try 
			{
				string cd = DG_APPR.Items[row].Cells[0].Text.Trim();
				conn.QueryString = "DELETE FROM TTPRODUCT WHERE PRODUCTID = '"+cd+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void performRequest(int row)
		{
			int rst = 0;
			string cek = "";
			
			string pid = DG_APPR.Items[row].Cells[0].Text.Trim();
			string pname = DG_APPR.Items[row].Cells[1].Text.Trim();
			string status = DG_APPR.Items[row].Cells[3].Text.Trim();
			string userid	= Session["UserID"].ToString();
			rst = row;

			cek = CekSales(pid);
			
			if (status.Equals("1"))
			{
				try
				{
					//audittrail
					conn.QueryString = "EXEC PARAM_GENERAL_TPRODUCT_APPR '"+pid+"', '"+status+"', '"+userid+"'";
					//conn.QueryString = "EXEC PARAM_GENERAL_TPRODUCT_APPR '"+pid+"', '"+status+"'";
					conn.ExecuteNonQuery();
					
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
				
				if(cek == "no")
				{
					try
					{
						conn3.QueryString = "INSERT INTO PRODUCT (PRODUCT_ID, PRODUCT_NAME, PRODTYPE_ID) "+
							"VALUES('"+pid+"', '"+pname+"', '1')";
 
						conn3.ExecuteQuery();
					}
					catch
					{
						GlobalTools.popMessage(this,"Cannot insert data for Sales Commission product parameter!"); 
						return;
					}
				}
				
			}

			if (status.Equals("2"))
			{
				try
				{
					//audittrail
					conn.QueryString = "EXEC PARAM_GENERAL_TPRODUCT_APPR '"+pid+"', '"+status+"', '"+userid+"'";
					//conn.QueryString = "EXEC PARAM_GENERAL_TPRODUCT_APPR '"+pid+"', '"+status+"'";
					conn.ExecuteNonQuery();
					//Response.Write(conn.QueryString);
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

				if(cek == "yes")
				{
					try
					{
						conn3.QueryString = "UPDATE PRODUCT SET PRODUCT_NAME = '"+pname+"' where PRODUCT_ID = '"+pid+"'";
 
						conn3.ExecuteQuery();
					}
					catch
					{
						GlobalTools.popMessage(this,"Cannot update data for Sales Commission product parameter!"); 
						return;
					}

				}
			}

			if (status.Equals("3"))
			{
				try
				{
					//audittrail
					conn.QueryString = "EXEC PARAM_GENERAL_TPRODUCT_APPR '"+pid+"', '"+status+"', '"+userid+"'";
					//conn.QueryString = "EXEC PARAM_GENERAL_TPRODUCT_APPR '"+pid+"', '"+status+"'";
					conn.ExecuteNonQuery();
					//Response.Write(conn.QueryString);
					//conn.QueryString = "DELETE FROM TPRODUCT WHERE PRODUCTID = '"+pid+"'"; 
					//conn.ExecuteQuery();
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

				if(cek == "yes")
				{
					try
					{
						conn3.QueryString = "DELETE from PRODUCT where PRODUCT_ID = '"+pid+"'";
 
						conn3.ExecuteQuery();
					}
					catch
					{
						GlobalTools.popMessage(this,"Cannot delete data for Sales Commission product parameter"); 
						return;
					}

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
				case "view":
					string pd = e.Item.Cells[0].Text.Trim();
					string stat = e.Item.Cells[3].Text.Trim();
					//ViewDetail(pd, stat);
					ViewDetail_2(pd, stat);
					BindData();	
					break;
				//default:
					// Do nothing.
					//break;
			}		
		}

		private void ViewDetail_2(string id, string st)
		{
			string mid = Request.QueryString["ModuleId"];
			//add for new page detail
			GlobalTools.NewWindow(this, "ProductDetail.aspx?productid="+id+"&ch_sta="+st+"&table=vw_param_tproduct_pending&ModuleId="+mid,"Pending_ProductDetail",false,true,900,600);
		}

		private void ViewDetail(string id, string st)
		{
			conn.QueryString = "select * from vw_param_tproduct_pending where productid = '"+id+"' and CH_STA = '"+st+"'";
			conn.ExecuteQuery();
 
			if(conn.GetRowCount() != 0)
			{
				TR_KET.Visible = true;

				if(st == "1")
					LBL_STATUS.Text = "INSERT";
				else if(st == "2")
					LBL_STATUS.Text = "UPDATE";
				else if(st == "3")
					LBL_STATUS.Text = "DELETE";
				
				LBL_PID.Text = conn.GetFieldValue(0,"PRODUCTID");
				LBL_PRNAME.Text = conn.GetFieldValue(0,"PRODUCTNAME");
				LBL_ADMIN.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"PR_ADMIN")); 

				LBL_AIP.Text = conn.GetFieldValue(0,"PR_AIPLIMITTIME");
				LBL_BEA_OTHER.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"PR_BEAOTH"));
				LBL_CEIL_LIM.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"CEILLING_LIMIT")); 
				LBL_DP.Text = conn.GetFieldValue(0,"DOWN_PAYMENT")+ " %";
				LBL_EMAS_CODE.Text = conn.GetFieldValue(0,"CD_SIBS");
				LBL_FIDU.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"PR_FIDUCIA")); 
				LBL_FIDU_LIM.Text = conn.GetFieldValue(0,"PR_LIMITFIDUCIA");
				LBL_FLOOR_LIM.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"FLOOR_LIMIT"));
				LBL_FLOOR_RATE.Text = conn.GetFieldValue(0,"FLOOR_RATE");
				LBL_GRP_NAME.Text = conn.GetFieldValue(0,"GROUP_NAME");
				LBL_MKSC.Text = conn.GetFieldValue(0,"PR_SRCCODE");
				LBL_NPWP.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"MIN_LIMIT_NPWP"));
				LBL_PROMO.Text = conn.GetFieldValue(0,"NAMA_PROMO"); 
				LBL_PROV.Text = "Rp. " + GlobalTools.MoneyFormat(conn.GetFieldValue(0,"PR_PROVISI")); 
				LBL_PROV_RATE.Text = conn.GetFieldValue(0,"PR_PROVPERCENT") + " %";
				LBL_SPPK.Text = conn.GetFieldValue(0,"PR_SPPKLMTTIME");
				LBL_TYPE.Text = conn.GetFieldValue(0,"PROD_TP"); 
				
				if(conn.GetFieldValue(0,"NL_CHECK") == "1")
					LBL_NEG_LIST.Text = "yes";
				else
					LBL_NEG_LIST.Text = "no";

				if(conn.GetFieldValue(0,"BLACKLIST_CHECK") == "1")
					LBL_BLACK.Text = "yes";
				else
					LBL_BLACK.Text = "no";

				if(conn.GetFieldValue(0,"PRESCRE_CHECK") == "1")
					LBL_PRES.Text = "yes";
				else
					LBL_PRES.Text = "no";

				if(conn.GetFieldValue(0,"DHBI_CHECK") == "1")
					LBL_DHBI.Text = "yes";
				else
					LBL_DHBI.Text = "no";

				if(conn.GetFieldValue(0,"PR_SPK") == "1")
					LBL_SPK.Text = "yes";
				else
					LBL_SPK.Text = "no";

				if(conn.GetFieldValue(0,"CUSTOMER_TYPE") == "0")
					LBL_CUST_TYPE.Text = "Personal";
				else if(conn.GetFieldValue(0,"CUSTOMER_TYPE") == "1")
					LBL_CUST_TYPE.Text = "Company";
				else if(conn.GetFieldValue(0,"CUSTOMER_TYPE") == "2")
					LBL_CUST_TYPE.Text = "Personal and Company";
				else
					LBL_CUST_TYPE.Text = "&nbsp;";

				if(conn.GetFieldValue(0,"ALLOWCARDBUNDLING") == "1")
					LBL_CARDBUNDLING.Text = "yes";
				else
					LBL_CARDBUNDLING.Text = "no";

				if(conn.GetFieldValue(0,"PR_MITRAKARYA") == "1")
					LBL_PR_MITRAKARYA.Text = "yes";
				else
					LBL_PR_MITRAKARYA.Text = "no";
			}
			else
			{
				TR_KET.Visible = false;
			}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DG_APPR.Items.Count; i++)
			{
				RadioButton rbA = (RadioButton)DG_APPR.Items[i].FindControl("Rdb1"),
					rbR = (RadioButton)DG_APPR.Items[i].FindControl("Rdb2");
				
				if(rbA.Checked)
				{
					performRequest(i);
				}
				else if(rbR.Checked)
				{
					deleteData(i);
				}
			}

			BindData();		
			TR_KET.Visible = false;
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc=9902040202&ModuleId=40");
		}
	}
}
