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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for ChannParamListAppr.
	/// </summary>
	public partial class ChannParamListAppr : System.Web.UI.Page
	{
		//////////////////////////////////////////////////////////////////////////
		/// view & stored procedure used in this modul:
		/// VW_PENDING_RFCHANN_PARAM_LIST
		/// CHANN_RFCHANN_PARAM_LIST_MAKER
		/// CHANN_RFCHANN_PARAM_LIST_APPR

		protected System.Web.UI.WebControls.DataGrid dtg_CHANN;

		#region my variabels
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		#endregion
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				bindData();
			}
			dtg_CHANN.PageIndexChanged += new DataGridPageChangedEventHandler(this.gridChange);
		}
		
		#region my methods

		private void bindData()
		{
			conn.QueryString = "SELECT * FROM VW_PENDING_RFCHANN_PARAM_LIST";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("A")); //0
			dt.Columns.Add(new DataColumn("B")); //1
			dt.Columns.Add(new DataColumn("C")); //2
			dt.Columns.Add(new DataColumn("D")); //3
			dt.Columns.Add(new DataColumn("E")); //4
			dt.Columns.Add(new DataColumn("F")); //5
			dt.Columns.Add(new DataColumn("G")); //6
			dt.Columns.Add(new DataColumn("H")); //7
			dt.Columns.Add(new DataColumn("I")); //8
			dt.Columns.Add(new DataColumn("J")); //9
			dt.Columns.Add(new DataColumn("K")); //10
			dt.Columns.Add(new DataColumn("L")); //11
			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,2);
				dr[3] = conn.GetFieldValue(i,3);
				dr[4] = conn.GetFieldValue(i,4);
				dr[5] = conn.GetFieldValue(i,5);
				dr[6] = conn.GetFieldValue(i,6);
				dr[7] = conn.GetFieldValue(i,7);
				dr[8] = conn.GetFieldValue(i,8);
				dr[9] = conn.GetFieldValue(i,9);
				dr[10] = conn.GetFieldValue(i,10);
				dr[11] = conn.GetFieldValue(i,11);
				dt.Rows.Add(dr);
			}
			dtg_CHANN.DataSource = new DataView(dt);
			try 
			{
				dtg_CHANN.DataBind();
			}
			catch 
			{
				dtg_CHANN.CurrentPageIndex = dtg_CHANN.PageCount - 1;
				dtg_CHANN.DataBind();
			}
			for (int i = 0; i < dtg_CHANN.Items.Count; i++)
			{
				if (dtg_CHANN.Items[i].Cells[7].Text.Trim() == "0")
				{
					dtg_CHANN.Items[i].Cells[7].Text = "UPDATE";
				}
				else if (dtg_CHANN.Items[i].Cells[7].Text.Trim() == "1")
				{
					dtg_CHANN.Items[i].Cells[7].Text = "INSERT";
				}
				else if (dtg_CHANN.Items[i].Cells[7].Text.Trim() == "2")
				{
					dtg_CHANN.Items[i].Cells[7].Text = "DELETE";
				}
			}
		}

		private void performRequest(int row)
		{
			try 
			{
				string CH_PRM_CODE = dtg_CHANN.Items[row].Cells[0].Text.Trim();

				conn.QueryString = "exec PARAM_GENERAL_RFCHANN_PARAM_LIST_APPR '" + CH_PRM_CODE + "', '1', '" + Session["UserID"].ToString() + "'";
				conn.ExecuteNonQuery();

				/***
				string CH_PRM_NAME = dtg_CHANN.Items[row].Cells[1].Text.Trim();
				string CH_PRM_TABLE = dtg_CHANN.Items[row].Cells[2].Text.Trim();
				string CH_PRM_FIELD = dtg_CHANN.Items[row].Cells[3].Text.Trim();
				string CH_PRM_REJECTDESC = dtg_CHANN.Items[row].Cells[4].Text.Trim();
				string CH_PRM_ISPARAMETER = dtg_CHANN.Items[row].Cells[5].Text.Trim() == "No" ? "0" : "1";
				string CH_PRM_RFTABLE = dtg_CHANN.Items[row].Cells[6].Text.Trim();


				string status = dtg_CHANN.Items[row].Cells[7].Text.Trim();
								

				if (status.Equals("INSERT"))
				{
					conn.QueryString = "CHANN_RFCHANN_PARAM_LIST_APPR '1','"+ 
						CH_PRM_CODE.Trim() + "','" +
						CH_PRM_NAME.Trim() + "','" +
						CH_PRM_ISPARAMETER.Trim() + "','','" +
						CH_PRM_FIELD.Trim() + "','" +
						CH_PRM_TABLE.Trim() + "','" +
						CH_PRM_RFTABLE.Trim() + "','','" +
						CH_PRM_REJECTDESC.Trim() + "','','1'";
						
					conn.ExecuteNonQuery();

				}
				if (status.Equals("DELETE"))
				{
					conn.QueryString = "CHANN_RFCHANN_PARAM_LIST_APPR '2','"+ CH_PRM_CODE.Trim() + "','','','','','','','','','','',''";
					conn.ExecuteNonQuery();
				}

				conn.QueryString = "CHANN_RFCHANN_PARAM_LIST_MAKER '2','"+ CH_PRM_CODE.Trim() + "','','','','','','','','','','',''";
				conn.ExecuteNonQuery();
				***/
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string CH_PRM_CODE = dtg_CHANN.Items[row].Cells[0].Text.Trim();
				
				conn.QueryString = "exec PARAM_GENERAL_RFCHANN_PARAM_LIST_APPR '" + CH_PRM_CODE + "', '0', '" + Session["UserID"].ToString() + "'";
				conn.ExecuteNonQuery();

				/**
				conn.QueryString = "CHANN_RFCHANN_PARAM_LIST_MAKER '2','"+ CH_PRM_CODE.Trim() + "','','','','','','','','','','',''";
				conn.ExecuteNonQuery();
				**/
			} 
			catch {}
		}

		#endregion

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
			this.dtg_CHANN.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtg_CHANN_ItemCommand);

		}
		#endregion

		private void gridChange(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			dtg_CHANN.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData();	
		}

		protected void btn_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < dtg_CHANN.Items.Count; i++)
			{
				try
					
				{
					RadioButton rdb_A = (RadioButton) dtg_CHANN.Items[i].FindControl("rdb_Approve");
					RadioButton	rdb_R = (RadioButton) dtg_CHANN.Items[i].FindControl("rdb_Reject");
					if (rdb_A.Checked)
					{
						performRequest(i);
					}
					else if (rdb_R.Checked)
					{
						deleteData(i);
					}
				} 
				catch {}
			}
			bindData();
		}

		protected void btn_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		private void dtg_CHANN_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "approve":
					for (i = 0; i < dtg_CHANN.PageSize; i++)
					{
						try
						{
							RadioButton rdb_A = (RadioButton) dtg_CHANN.Items[i].FindControl("rdb_Approve");
							RadioButton	rdb_R = (RadioButton) dtg_CHANN.Items[i].FindControl("rdb_Reject");
							RadioButton	rdb_P = (RadioButton) dtg_CHANN.Items[i].FindControl("rdb_Pending");
							rdb_A.Checked = true;
							rdb_R.Checked = false;
							rdb_P.Checked = false;
						} 
						catch {}
					}
					break;
				case "reject":
					for (i = 0; i < dtg_CHANN.PageSize; i++)
					{
						try
						{
							RadioButton rdb_A = (RadioButton) dtg_CHANN.Items[i].FindControl("rdb_Approve");
							RadioButton	rdb_R = (RadioButton) dtg_CHANN.Items[i].FindControl("rdb_Reject");
							RadioButton	rdb_P = (RadioButton) dtg_CHANN.Items[i].FindControl("rdb_Pending");
							rdb_A.Checked = false;
							rdb_R.Checked = true;
							rdb_P.Checked = false;
						} 
						catch {}
					}
					break;
				case "pending":
					for (i = 0; i < dtg_CHANN.PageSize; i++)
					{
						try
						{
							RadioButton rdb_A = (RadioButton) dtg_CHANN.Items[i].FindControl("rdb_Approve");
							RadioButton	rdb_R = (RadioButton) dtg_CHANN.Items[i].FindControl("rdb_Reject");
							RadioButton	rdb_P = (RadioButton) dtg_CHANN.Items[i].FindControl("rdb_Pending");
							rdb_A.Checked = false;
							rdb_R.Checked = false;
							rdb_P.Checked = true;
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
