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
using Microsoft.VisualBasic;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFChann_CompanyAppr.
	/// </summary>
	public partial class RFChann_CompanyAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected int jml;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			// Put user code to initialize the page here
			if (!IsPostBack)
			{
				bindData();
			}
		}

		private void bindData()
		{
			conn.QueryString="select * from VW_CHANN_COMPANY_PENDING";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DTG_APPR.DataSource = dt;

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
				//DGR_MAKER.Items[i].Cells[0].Text = (i+1+(DGR_MAKER.CurrentPageIndex)*DGR_MAKER.PageSize).ToString();
				if (DTG_APPR.Items[i].Cells[11].Text.Trim() == "0")
				{
					DTG_APPR.Items[i].Cells[11].Text = "INSERT";
				}
				else if (DTG_APPR.Items[i].Cells[11].Text.Trim() == "1")
				{
					DTG_APPR.Items[i].Cells[11].Text = "UPDATE";
				}
				else if (DTG_APPR.Items[i].Cells[11].Text.Trim() == "2")
				{
					DTG_APPR.Items[i].Cells[11].Text = "DELETE";
				}
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
			this.DTG_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DTG_APPR_ItemCommand);
			this.DTG_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DTG_APPR_PageIndexChanged);

		}
		#endregion

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


		private void deleteData(int row)
		{
			try 
			{
									
				//conn.QueryString = "DELETE PENDING_RFPROJECT WHERE PRJ_CODE="+a+"";
				conn.QueryString = " EXEC CHANN_COMPANY_PENDING 'DELETE', '"+DTG_APPR.Items[row].Cells[4].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[0].Text.Trim()+"','"+
					DTG_APPR.Items[row].Cells[6].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[7].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[8].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[9].Text.Trim()+"','"+
					DTG_APPR.Items[row].Cells[10].Text.Trim()+"','','' ";
				conn.ExecuteNonQuery();
			} 
			catch {}
		}


		private void performRequest(int row)
		{
			string status=DTG_APPR.Items[row].Cells[11].Text.Trim(); //status 

			try 
			{
				conn.QueryString = " EXEC CHANN_COMPANY_PENDING 'DELETE', '"+DTG_APPR.Items[row].Cells[4].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[0].Text.Trim()+"','"+
					DTG_APPR.Items[row].Cells[6].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[7].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[8].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[9].Text.Trim()+"','"+
					DTG_APPR.Items[row].Cells[10].Text.Trim()+"','','' ";
				conn.ExecuteNonQuery();		
			
				if (status.Equals("UPDATE"))
				{

					conn.QueryString = "exec CHANN_COMPANY_APPR 'INSERT','"+DTG_APPR.Items[row].Cells[4].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[0].Text.Trim()+"','"+
						DTG_APPR.Items[row].Cells[6].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[7].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[8].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[9].Text.Trim()+"','"+
						DTG_APPR.Items[row].Cells[10].Text.Trim()+"','1' ";
					conn.ExecuteNonQuery();

				}

				if (status.Equals("INSERT"))
				{
					
					conn.QueryString = "exec CHANN_COMPANY_APPR 'INSERT','"+DTG_APPR.Items[row].Cells[4].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[0].Text.Trim()+"','"+
						DTG_APPR.Items[row].Cells[6].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[7].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[8].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[9].Text.Trim()+"','"+
						DTG_APPR.Items[row].Cells[10].Text.Trim()+"','1' ";
					conn.ExecuteNonQuery();
				}
				if (status.Equals("DELETE"))
				{
					
					conn.QueryString = "exec CHANN_COMPANY_APPR 'DELETE','"+DTG_APPR.Items[row].Cells[4].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[0].Text.Trim()+"','"+
						DTG_APPR.Items[row].Cells[6].Text.Trim()+"'";//,'"+DTG_APPR.Items[row].Cells[7].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[8].Text.Trim()+"','"+DTG_APPR.Items[row].Cells[9].Text.Trim()+"','"+
					//	DTG_APPR.Items[row].Cells[10].Text.Trim()+"','' ";
					conn.ExecuteNonQuery();
				}
				
				

				
			} 
			catch 
			{
		
			}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DTG_APPR.Items.Count; i++)
			{
				try
					
				{
					RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
						rbR = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2");
					if (rbA.Checked == true)
					{	
						performRequest(i);
					}
					else if (rbR.Checked == true)
					{  
						deleteData(i);
					}
				} 
				catch {}
			}
			bindData();

		}

		private void DTG_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			// Set CurrentPageIndex to the page the user clicked.
			DTG_APPR.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData();
		}


	}
}
