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
using DMS.DBConnection;
using DMS.CuBESCore;
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for ScoringTypeAppr.
	/// </summary>
	public partial class ScoringTypeAppr : System.Web.UI.Page
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
			conn.QueryString = "SELECT SCRID,SCRDESC,SCR_LINK,PENDINGSTATUS  FROM PENDING_RFSCORINGTYPE";
			conn.ExecuteQuery();
			DTG_APPR.DataSource = conn.GetDataTable().Copy();
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
				if (DTG_APPR.Items[i].Cells[3].Text.Trim() == "0")
				{
					DTG_APPR.Items[i].Cells[3].Text = "UPDATE";
				}
				else if (DTG_APPR.Items[i].Cells[3].Text.Trim() == "1")
				{
					DTG_APPR.Items[i].Cells[3].Text = "INSERT";
				}
				else if (DTG_APPR.Items[i].Cells[3].Text.Trim() == "2")
				{
					DTG_APPR.Items[i].Cells[3].Text = "DELETE";
				}
			}
		}

		private void performRequest(int row)
		{
			try 
			{
				string a = DTG_APPR.Items[row].Cells[0].Text.Trim();
				string b = DTG_APPR.Items[row].Cells[1].Text.Trim();
				string c =DTG_APPR.Items[row].Cells[2].Text.Trim();
				string status=DTG_APPR.Items[row].Cells[3].Text.Trim();
								
				if (status.Equals("UPDATE"))
				{
					conn.QueryString = "UPDATE RFSCORINGTYPE SET SCRID='"+a+"',SCRDESC='"+b+"',SCR_LINK='"+c+"',ACTIVE='1'  WHERE SCRID='"+a+"' ";
					conn.ExecuteNonQuery();
				}

				if (status.Equals("INSERT"))
				{
					conn.QueryString = "INSERT INTO RFSCORINGTYPE(SCRID,SCRDESC,SCR_LINK,ACTIVE) VALUES ('"+a+"', '"+b+"','"+c+"','1')";
					conn.ExecuteNonQuery();
				}
				if (status.Equals("DELETE"))
				{
					conn.QueryString = "DELETE RFSCORINGTYPE WHERE SCRID='"+a+"' ";
					conn.ExecuteNonQuery();
				}

				conn.QueryString = "DELETE PENDING_RFSCORINGTYPE WHERE SCRID='"+a+"' ";
				conn.ExecuteNonQuery();

				
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string a = DTG_APPR.Items[row].Cells[0].Text.Trim();
				conn.QueryString = "DELETE PENDING_RFSCORINGTYPE WHERE SCRID='"+a+"' ";
				conn.ExecuteNonQuery();
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
				Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
			
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
