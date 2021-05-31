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
using System.Configuration;

namespace CuBES_Maintenance.Parameter.Area.Consumer
{
	/// <summary>
	/// Summary description for DealerIndukParamAppr.
	/// </summary>
	public partial class DealerIndukParamAppr : System.Web.UI.Page
	{
		protected Connection conn2;
		protected Connection conn ;//= new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected string scid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			
			if (!IsPostBack)
			{
				LBL_ACTIVE.Text = Request.QueryString["active"];
				
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

				viewPendingData();
				
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
		
		private void SetDBConn2()
		{
			conn.QueryString = "select * from RFMODULE where MODULEID=40";
			conn.ExecuteQuery();

			conn2 = new Connection ("Data Source=" + conn.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn.GetFieldValue("DB_NAMA") + ";uid=" +conn.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void viewPendingData() 
		{
			string tableName = Request.QueryString["tablename"];

			conn2.QueryString = "select convert(int,dli_code) DLI_SEQ,*,case when dli_kerjasama='1' then 'Telah Kerjasama' else 'Belum Kerjasama' end ST1,case when dli_blocked='1' then 'Blocked' else 'No' end ST2,case when ch_sta='0' then 'UPDATE' when ch_sta='1' then 'INSERT' when ch_sta='2' then 'DELETE' else '' end STATUS from tdealer_induk";
			conn2.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
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
		}
		
		private void DGRequest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;			
			viewPendingData();
		}
		
		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}
		
		private void performRequest(int row, string userid)
		{
			int rst = 0;

			string id   = DGRequest.Items[row].Cells[5].Text.Trim();
			string desc = CleansText(DGRequest.Items[row].Cells[1].Text);
			string kr   = CleansText(DGRequest.Items[row].Cells[6].Text);
			string blk  = CleansText(DGRequest.Items[row].Cells[7].Text);
			string status = DGRequest.Items[row].Cells[4].Text.Trim();
			string status1 = DGRequest.Items[row].Cells[11].Text.Trim();
			
			rst = row;

			/* 12-08-2005
			  GlobalTools.ConvertNull untuk DLI_KERJASAMA & DLI_BLOCKED dihilangkan */
			
			/* coding for audittrial parameter */
		    
			conn2.QueryString = "EXEC PARAM_AREA_DEALER_INDUK_AUDIT '"+id+"','"+status1+"','"+userid+"',"+
				GlobalTools.ConvertNull(desc)+",'"+kr+"','"+blk+"'";
			conn2.ExecuteNonQuery();
			
			/* end of coding */
			
			if (status.Equals("INSERT"))
			{
				try
				{
					conn2.QueryString = "INSERT INTO DEALER_INDUK VALUES ('"+id+"',"+GlobalTools.ConvertNull(desc)+", "+kr+", "+blk+",'1')";
					
					conn2.ExecuteQuery();			
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

			if (status.Equals("UPDATE"))
			{
				try
				{
					conn2.QueryString = "UPDATE DEALER_INDUK set DLI_DESC = "+GlobalTools.ConvertNull(desc)+", "+
						"DLI_KERJASAMA = "+kr+", DLI_BLOCKED = "+blk+" where DLI_CODE = '"+id+"'";
					
					conn2.ExecuteQuery();
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

			if (status.Equals("DELETE"))
			{
				try
				{
					conn2.QueryString = "UPDATE DEALER_INDUK SET ACTIVE='0' where DLI_CODE = '"+id+"'";
				
					conn2.ExecuteQuery();
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
			
			conn2.ClearData(); 
		}

		private void deleteData(int row)
		{
			string id   = DGRequest.Items[row].Cells[5].Text.Trim();

			try 
			{	
				conn2.QueryString = "DELETE FROM TDEALER_INDUK where DLI_CODE = '"+id+"'";
				conn2.ExecuteQuery();

				conn2.ClearData(); 
			} 
			catch { }
		}

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
				case "allRejc":
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

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			scid = (string) Session["UserID"];

			for (int i = 0; i < DGRequest.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject");
					
					if (rbA.Checked)
					{
						performRequest(i, scid);
					}
					else if (rbR.Checked)
					{
						deleteData(i);
					}
				} 
				catch {}
			}

			viewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamApprovalAll.aspx?mc=9902040202&ModuleId=40");
		}
	}
}
