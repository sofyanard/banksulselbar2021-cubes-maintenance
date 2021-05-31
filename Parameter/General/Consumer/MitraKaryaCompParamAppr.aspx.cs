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
	/// Summary description for MitraKaryaCompParamAppr.
	/// Audittrail included, but still in comment!!...
	/// </summary>
	public partial class MitraKaryaCompParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string potgaji, block, tht, expdate, mtksrc;
		protected string src, plafond, limit, tenor, mtkdesc;
		protected string insub, gline, remain, rtcode, bcode;
	
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
			
			DGRequest.PageIndexChanged +=new DataGridPageChangedEventHandler(DGRequest_PageIndexChanged);
		}

		private void ViewData()
		{
			conn2.QueryString = "select * from rfmodule where moduleid = '40'";
			conn2.ExecuteQuery();

			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			InitialCon(); 

			viewPendingData();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
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

		}
		#endregion
		
		private void viewPendingData()
		{
			conn.QueryString = "select a.MTK_CODE, a.MTK_SRCCODE, a.MKI_CODE, a.MTK_DESC, b.MKI_DESC, a.CH_STA, "+
				"STATUS = case a.CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' when '3' then 'DELETE' end "+
				"from TRFMITRAKARYA a left join RFMITRAKARYA_INDUK b on a.MKI_CODE = b.MKI_CODE";

			conn.ExecuteQuery(); 

			DGRequest.DataSource = conn.GetDataTable().Copy();
			
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

		private string changeToZero(string a)
		{
			if(a.Trim()=="" || a.Trim()==null)
				a="0";

			return a;
		}

		private void deleteData(int row)
		{
			try 
			{
				string cd = DGRequest.Items[row].Cells[1].Text.Trim();

				conn.QueryString = "delete from TRFMITRAKARYA WHERE MTK_CODE = '"+cd+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void performRequest(int row)
		{
			int rst = 0;

			string mkicode   = DGRequest.Items[row].Cells[0].Text.Trim();
			string mtkcode   = DGRequest.Items[row].Cells[1].Text.Trim();
			string status = DGRequest.Items[row].Cells[6].Text.Trim();
			
			rst = row;

			//CHECK PLAFOND INDUK
			double plafond_induk=0, distribusi_cabang=0, distribusi_request=0;
			conn.QueryString = "SELECT MKI_GRLINE FROM RFMITRAKARYA_INDUK WHERE MKI_CODE = '"+mkicode+"'";
			conn.ExecuteQuery();
			if(conn.GetRowCount() != 0)
			{
				plafond_induk = double.Parse(conn.GetFieldValue(0,0));
			}
			conn.QueryString = "SELECT ISNULL(SUM(MTK_GRLINE),0) FROM RFMITRAKARYA WHERE MKI_CODE = '"+mkicode+"' AND MTK_CODE <> '"+mtkcode+"'";
			conn.ExecuteQuery();
			if(conn.GetRowCount() != 0)
			{
				distribusi_cabang = double.Parse(conn.GetFieldValue(0,0));
			}
			conn.QueryString = "SELECT * FROM TRFMITRAKARYA WHERE MTK_CODE = '"+mtkcode+"'";
			conn.ExecuteQuery();
			if(conn.GetRowCount() != 0)
			{
				distribusi_request = double.Parse(conn.GetFieldValue(0,"MTK_GRLINE"));
			}
			if (plafond_induk < (distribusi_cabang + distribusi_request))
			{
				string errmsg = "Plafond Induk Tidak Mencukupi!";
				GlobalTools.popMessage(this, errmsg);
				return;
			}


			//AUDITTRAIL
			
			conn.QueryString = " exec PARAM_AUDITTRAIL_RFMITRAKARYA '" + mtkcode + "','" + Session["UserID"].ToString ()+ "'";
			conn.ExecuteNonQuery();
			//****

			conn.QueryString = "SELECT * FROM TRFMITRAKARYA WHERE MTK_CODE = '"+mtkcode+"'";
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0)
			{
				mtksrc = conn.GetFieldValue(0,"MTK_SRCCODE"); 
				mtkdesc = conn.GetFieldValue(0,"MTK_DESC");
				insub = conn.GetFieldValue(0,"MTK_SUBINTEREST");
				gline = changeToZero(conn.GetFieldValue(0,"MTK_GRLINE"));
				plafond = changeToZero(conn.GetFieldValue(0,"MTK_PLAFOND"));
				potgaji = conn.GetFieldValue(0,"MKT_POTGAJI");
				limit = conn.GetFieldValue(0,"MTK_INDLIMIT");
				tenor = conn.GetFieldValue(0,"MTK_INDTENOR");
				block = conn.GetFieldValue(0,"MTK_BLOCKED");
				tht = conn.GetFieldValue(0,"MTK_COVERTHT");
				expdate = conn.GetFieldValue(0,"MTK_EXPIREDATE");
				bcode = conn.GetFieldValue(0,"BRANCH_CODE");
				rtcode = conn.GetFieldValue(0,"RT_CODE");
			}
			
			if (status.Equals("1"))
			{
				try
				{
					conn.QueryString = "INSERT INTO RFMITRAKARYA (MTK_CODE, MKI_CODE, MTK_SRCCODE, MTK_DESC, MTK_SUBINTEREST,"+
						" MTK_GRLINE, MTK_PLAFOND, MTK_EXPIREDATE, RT_CODE, MKT_POTGAJI,"+ 
						" MKT_PESENAM, MKT_PESATENAM, MTK_BLOCKED, MTK_INDLIMIT, MTK_INDTENOR, MTK_COVERTHT, BRANCH_CODE) values "+
						"('"+mtkcode+"', '"+mkicode+"', "+GlobalTools.ConvertNull(mtksrc)+", "+GlobalTools.ConvertNull(mtkdesc)+","+ 
						" "+GlobalTools.ConvertFloat(insub)+", "+GlobalTools.ConvertFloat(gline)+","+
						" "+GlobalTools.ConvertFloat(plafond)+", "+GlobalTools.ToSQLDate(expdate)+", "+GlobalTools.ConvertNull(rtcode)+","+
						" "+GlobalTools.ConvertFloat(potgaji)+", 0, 0, "+GlobalTools.ConvertNull(block)+", "+GlobalTools.ConvertFloat(limit)+","+
						" "+GlobalTools.ConvertFloat(tenor)+", "+GlobalTools.ConvertNull(tht)+", "+GlobalTools.ConvertNull(bcode)+")";
					
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
					conn.QueryString = "UPDATE RFMITRAKARYA SET "+
						"BRANCH_CODE = "+GlobalTools.ConvertNull(bcode)+", "+
						"MKI_CODE = "+GlobalTools.ConvertNull(mkicode)+","+
						"MKT_PESATENAM = 0, "+
						"MKT_PESENAM = 0, "+
						"MKT_POTGAJI = "+GlobalTools.ConvertFloat(potgaji)+", "+
						"MTK_BLOCKED = '"+block+"', "+
						"MTK_COVERTHT = '"+tht+"', "+
						"MTK_DESC = "+GlobalTools.ConvertNull(mtkdesc)+", "+
						"MTK_EXPIREDATE = "+GlobalTools.ToSQLDate(expdate)+", "+
						"MTK_GRLINE = "+GlobalTools.ConvertFloat(gline)+", "+
						"MTK_INDLIMIT = "+GlobalTools.ConvertFloat(limit)+", "+
						"MTK_INDTENOR = "+GlobalTools.ConvertFloat(tenor)+", "+
						"MTK_PLAFOND = "+GlobalTools.ConvertFloat(plafond)+", "+
						"MTK_SRCCODE = "+GlobalTools.ConvertNull(mtksrc)+", "+
						"MTK_SUBINTEREST = "+GlobalTools.ConvertFloat(insub)+", "+
						"RT_CODE = "+GlobalTools.ConvertNull(rtcode)+" "+
						"WHERE MTK_CODE = '"+mtkcode+"'";		
					
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
					conn.QueryString = "delete from RFMITRAKARYA WHERE MTK_CODE = '"+mtkcode+"'";
				
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
				case "view":
					string code = e.Item.Cells[1].Text.Trim();
					
					Response.Write("<script language='javascript'>window.open('DetailMitrakaryaComp.aspx?sta=0&mtkcode="+code+"','Detail','status=no,scrollbars=yes,width=620,height=425');</script>"); 						
					break;

				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGRequest.Items.Count; i++)
			{
				RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject");
				
				if (rbA.Checked)
				{
					performRequest(i);
				}
				else if (rbR.Checked)
				{
					deleteData(i);
				}
			}

			viewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleId=40");
		}
	}
}
