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
	/// Summary description for MitraKaryaIndukParamAppr.
	/// Audittrail included, but still in comment!!
	/// </summary>
	public partial class MitraKaryaIndukParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
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

			InitialCon(); 

			BindData();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData()
		{
			/*conn.QueryString = "select MKI_CODE, MKI_SRCCODE, MKI_DESC, isnull(MKI_GRLINE,0) GRLINE, "+ 
				"isnull(MKI_PLAFOND,0) PLAFOND, "+
				"(convert(float,isnull(MKI_GRLINE,0)) - convert(float,isnull(MKI_PLAFOND,0))) as SISA, "+
				"MKI_BLOCKED, CH_STA, "+
				"STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' when '3' then 'DELETE' end, "+
				"BLOCKED = case MKI_BLOCKED when '0' then 'No' when '1' then 'Yes' end, "+
				"convert(varchar,MKI_EXPIREDATE,103) MKI_EXPIREDATE2, RT_DESC, convert(varchar,MKI_EXPIREDATE) MKI_EXPIREDATE, TRFMITRAKARYA_INDUK.RT_CODE "+
				"from TRFMITRAKARYA_INDUK left join RFRATING on TRFMITRAKARYA_INDUK.RT_CODE = RFRATING.RT_CODE order by MKI_CODE ";
			*/

			conn.QueryString = "exec PARAM_MITRAKARYA_INDUK_APPR_VIEW_REQUEST";
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
				string code = DG_APPR.Items[row].Cells[0].Text.Trim();
				
				conn.QueryString = "DELETE FROM TRFMITRAKARYA_INDUK WHERE MKI_CODE = '"+code+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void performRequest(int row)
		{
			int rst = 0;
			
			string code = DG_APPR.Items[row].Cells[0].Text.Trim();
			string srccode = DG_APPR.Items[row].Cells[1].Text.Trim();
			string desc = cleansText(DG_APPR.Items[row].Cells[2].Text);
			string block = cleansText(DG_APPR.Items[row].Cells[7].Text.Trim());
			string grline = cleansFloat(DG_APPR.Items[row].Cells[3].Text.Trim());
			string status = DG_APPR.Items[row].Cells[12].Text.Trim();
			string expdate = DG_APPR.Items[row].Cells[8].Text.Trim();
			string rt_code = DG_APPR.Items[row].Cells[10].Text.Trim();

			rst = row;
			
			//Audittrail
			
			conn.QueryString = "exec PARAM_AUDITTRAIL_RFMITRAKARYA_INDUK '" + code + "','" + Session["UserID"].ToString() + "'";
			conn.ExecuteNonQuery();
			//****

			if (status.Equals("1"))
			{
				try
				{
					//changed by sofyan 2007-05-28, LOS Consumer Enh 4
					/*conn.QueryString = "INSERT INTO RFMITRAKARYA_INDUK (MKI_CODE, MKI_SRCCODE, MKI_DESC, "+
						"MKI_GRLINE, MKI_PLAFOND, MKI_EXPIREDATE, MKI_BLOCKED, ACTIVE, RT_CODE) VALUES('"+
						code+"', '"+
						srccode+"', "+
						GlobalTools.ConvertNull(desc)+", "+
						GlobalTools.ConvertFloat(grline)+", "+
						"0, '"+
						expdate+"', '"+
						block+"', '"+
						"1', '"+
						rt_code+"')";
					*/
					conn.QueryString = "exec PARAM_MITRAKARYA_INDUK_APPR_UPDATE_REQUEST " +
						"'2', " +
						"'" + code + "', " +
						"'" + srccode + "'";
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
					//deleteData(rst);
				}
			}

			if (status.Equals("2"))
			{
				try
				{
					//changed by sofyan 2007-05-28, LOS Consumer Enh 4
					/*conn.QueryString = "UPDATE RFMITRAKARYA_INDUK SET "+
						"MKI_DESC = "+GlobalTools.ConvertNull(desc)+", "+
						"MKI_GRLINE = "+GlobalTools.ConvertFloat(grline)+", "+
						"MKI_EXPIREDATE = '"+expdate+"', "+
						"RT_CODE = '"+rt_code+"' "+
						"WHERE MKI_CODE = '"+code+"' AND "+
						"MKI_SRCCODE = '"+srccode+"'";
					*/
					conn.QueryString = "exec PARAM_MITRAKARYA_INDUK_APPR_UPDATE_REQUEST " +
						"'1', " +
						"'" + code + "', " +
						"'" + srccode + "'";
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
					//deleteData(rst);
				}
			}

			if (status.Equals("3"))
			{
				try
				{
					//changed by sofyan 2007-05-28, LOS Consumer Enh 4
					/*conn.QueryString = "UPDATE RFMITRAKARYA_INDUK SET ACTIVE='0' WHERE MKI_CODE = '"+code+"'";
					 */
					conn.QueryString = "exec PARAM_MITRAKARYA_INDUK_APPR_UPDATE_REQUEST " +
						"'3', " +
						"'" + code + "', " +
						"'" + srccode + "'";
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
					//deleteData(rst);
				}
			}
			
			//conn.ClearData(); 
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
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc=9902040202&ModuleId=40");
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
		}
	}
}
