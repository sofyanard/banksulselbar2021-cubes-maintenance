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
	/// Summary description for RFBMRatingIIIAppr.
	/// </summary>
	public partial class prog_prodAppr : System.Web.UI.Page
	{
	
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				bindData();
			}
			DTG_APPR.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change);
		}

		private void bindData()
		{
			conn.QueryString = "select PROGRAMID,PRODUCTID,(SELECT TOP 1 PROGRAMDESC FROM rfprogram WHERE rfprogram.PROGRAMID=pending_prog_prod.PROGRAMID) program,(SELECT TOP 1 PRODUCTDESC FROM rfproduct WHERE rfproduct.PRODUCTID=pending_prog_prod.PRODUCTID) product,pendingstatus  from pending_prog_prod";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("A"));
			dt.Columns.Add(new DataColumn("B"));
			dt.Columns.Add(new DataColumn("C"));
			dt.Columns.Add(new DataColumn("D"));
			dt.Columns.Add(new DataColumn("E"));
			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,2);
				dr[3] = conn.GetFieldValue(i,3);
				dr[4] = conn.GetFieldValue(i,4);
				dt.Rows.Add(dr);
			}
			DTG_APPR.DataSource = new DataView(dt);
			try 
			{
				DTG_APPR.DataBind();
			}
			catch 
			{
				DTG_APPR.CurrentPageIndex = DTG_APPR.PageCount - 1;
				DTG_APPR.DataBind();
			}
			for (int i = 0; i < DTG_APPR.Items.Count; i++)
			{
				if (DTG_APPR.Items[i].Cells[4].Text.Trim() == "0")
				{
					DTG_APPR.Items[i].Cells[4].Text = "UPDATE";
				}
				else if (DTG_APPR.Items[i].Cells[4].Text.Trim() == "1")
				{
					DTG_APPR.Items[i].Cells[4].Text = "INSERT";
				}
				else if (DTG_APPR.Items[i].Cells[4].Text.Trim() == "2")
				{
					DTG_APPR.Items[i].Cells[4].Text = "DELETE";
				}
			}
		}

		private void executeApproval(string id1, string id2, string approvalFlag) 
		{
			conn.QueryString = "exec PARAM_GENERAL_PROG_PROD_APPR '" + id1 + "', '" + id2 + "', '" + 
				approvalFlag + "', '" + 
				Session["UserID"].ToString() + "'";
			conn.ExecuteNonQuery();
		}
		
		private void performRequest(int row)
		{
			try 
			{
				string a = DTG_APPR.Items[row].Cells[0].Text.Trim();
				string b = DTG_APPR.Items[row].Cells[1].Text.Trim();

				executeApproval(a, b, "1");

				/***
				string status=DTG_APPR.Items[row].Cells[4].Text.Trim();
								
				//if (status.Equals("UPDATE"))
				//{
				//	conn.QueryString = "UPDATE prog_prod SET PROGRAMID='"+a+"',PRODUCTID="+b+"  WHERE PROGRAMID='"+a+"' AND PRODUCTID='"+b+"' ";
				//	conn.ExecuteNonQuery();
				//}

				if (status.Equals("INSERT"))
				{
					conn.QueryString = "INSERT INTO prog_prod VALUES ('"+a+"',"+b+")";
					conn.ExecuteNonQuery();
				}
				if (status.Equals("DELETE"))
				{
					conn.QueryString = "DELETE prog_prod WHERE PROGRAMID='"+a+"' AND PRODUCTID='"+b+"' ";
					conn.ExecuteNonQuery();
				}

				conn.QueryString = "DELETE pending_prog_prod WHERE PROGRAMID='"+a+"' AND PRODUCTID='"+b+"' ";
				conn.ExecuteNonQuery();
				***/				
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string a = DTG_APPR.Items[row].Cells[0].Text.Trim();
				string b = DTG_APPR.Items[row].Cells[1].Text.Trim();

				executeApproval(a, b, "0");

				/***		
				conn.QueryString = "DELETE pending_prog_prod WHERE PROGRAMID='"+a+"' AND PRODUCTID='"+b+"' ";
				conn.ExecuteNonQuery();
				****/
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
			this.DTG_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DTG_APPR_ItemCommand);

		}
		#endregion

		void Grid_Change(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DTG_APPR.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData();	
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
			
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DTG_APPR.Items.Count; i++)
			{
				try
					
				{
					//DTG_APPR.Items[i].Cells[3].Text = "TEST";
					RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
						rbR = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2");
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

		private void DTG_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DTG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
								rbB = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2"),
								rbC = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton3");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DTG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
								rbB = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2"),
								rbC = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton3");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DTG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
								rbB = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2"),
								rbC = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton3");
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
