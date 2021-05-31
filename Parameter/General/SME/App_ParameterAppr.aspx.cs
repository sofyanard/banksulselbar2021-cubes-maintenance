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
	/// Summary description for App_ParameterAppr.
	/// </summary>
	public partial class App_ParameterAppr : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				bindData();
			}
		}

		private void bindData()
		{
			conn.QueryString = "select * from PENDING_APP_PARAMETER";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_APPR.DataSource = dt;
			try
			{
				this.DGR_APPR.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_APPR.CurrentPageIndex = DGR_APPR.CurrentPageIndex - 1;
					this.DGR_APPR.DataBind();
				}
				catch{}
			}

			for (int i = 0; i < DGR_APPR.Items.Count; i++)
			{
				if (DGR_APPR.Items[i].Cells[4].Text.Trim() == "0")
				{
					DGR_APPR.Items[i].Cells[4].Text = "UPDATE";
				}
				else if (DGR_APPR.Items[i].Cells[4].Text.Trim() == "1")
				{
					DGR_APPR.Items[i].Cells[4].Text = "INSERT";
				}
				else if (DGR_APPR.Items[i].Cells[4].Text.Trim() == "2")
				{
					DGR_APPR.Items[i].Cells[4].Text = "DELETE";
				}
			}
		}

		private void performRequest(int row)
		{
			try 
			{
				conn.QueryString = "exec PARAM_GENERAL_APP_PARAMETER_APPR '', '', '', '" + Session["UserID"].ToString() + "', '1', '" + Request.QueryString["pg"] + "'";
				conn.ExecuteNonQuery();

//				string seq = DGR_APPR.Items[row].Cells[0].Text.Trim();
//				string a = DGR_APPR.Items[row].Cells[1].Text.Trim();
//				string b = DGR_APPR.Items[row].Cells[2].Text.Trim();
//				string c = DGR_APPR.Items[row].Cells[3].Text.Trim();
//				string status=DGR_APPR.Items[row].Cells[4].Text.Trim();
								
//				if (status.Equals("UPDATE"))
//				{
//					conn.QueryString = "UPDATE APP_PARAMETER SET MAX_PS_COUNT='"+a+"',MAX_FS_COUNT='"+b+"',MAX_ACQ_COUNT='"+c+"' WHERE SEQ='"+seq+"'";
//					conn.ExecuteNonQuery();
//				}

//				if (status.Equals("INSERT"))
//				{
//					conn.QueryString = "INSERT INTO APP_PARAMETER (MAX_PS_COUNT,MAX_FS_COUNT,MAX_ACQ_COUNT)VALUES ('"+a+"','"+b+"', '"+c+"')";
//					conn.ExecuteNonQuery();
//				}
//				if (status.Equals("DELETE"))
//				{
//					conn.QueryString = "DELETE APP_PARAMETER WHERE SEQ='"+seq+"'";
//					conn.ExecuteNonQuery();
//				}

//				conn.QueryString = "DELETE PENDING_APP_PARAMETER WHERE SEQ='"+seq+"'";
//				conn.ExecuteNonQuery();

				
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				conn.QueryString = "exec PARAM_GENERAL_APP_PARAMETER_APPR '', '', '', '" + Session["UserID"].ToString() + "', '0', '" + Request.QueryString["pg"] + "'";
				conn.ExecuteNonQuery();

//				string a = DGR_APPR.Items[row].Cells[0].Text.Trim();
//		
//				conn.QueryString = "DELETE PENDING_APP_PARAMETER WHERE SEQ='"+a+"'";
//				conn.ExecuteNonQuery();
			} 
			catch {}
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
			this.DGR_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR_ItemCommand);

		}
		#endregion

		void Grid_Change(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DGR_APPR.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData();	
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGR_APPR.Items.Count; i++)
			{
				try
					
				{
					//DTG_APPR.Items[i].Cells[3].Text = "TEST";
					RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
						rbR = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject");
					if (rbA.Checked)
					{
						performRequest(i);
					}
					else if (rbR.Checked)
					{
						deleteData(i);
					}
				} 
				catch {}
			}
			bindData();
		}

		private void DGR_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Pending");
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}
	}
}
