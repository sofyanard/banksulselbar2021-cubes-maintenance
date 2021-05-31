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
	/// Summary description for CAFormatAspekAppr.
	/// </summary>
	public partial class CAFormatAspekAppr : System.Web.UI.Page
	{
	
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");

		protected void Page_Load(object sender, System.EventArgs e)
		{
			/*	conn = (Connection) Session["Connection"];

				if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
					Response.Redirect("/SME/Restricted.aspx");
	*/
			if (!IsPostBack)
			{
				bindData();
			}
			DTG_APPR.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change);
		}

		private void bindData()
		{
			conn.QueryString = "SELECT A.FORMATID,(select TOP 1 FORMATDESC from RFCA_FORMAT B WHERE A.FORMATID=B.FORMATID ) FORMATDESC, A.ASPEKID "+
				",(select TOP 1 ASPEKDESC from RFCA_ASPEK B WHERE A.ASPEKID=B.ASPEKID )ASPEKDESC,PENDINGSTATUS "+
				"	FROM PENDING_RFCA_FORMATASPEK A ";
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

		private void performRequest(int row)
		{
			try 
			{
				string format = DTG_APPR.Items[row].Cells[0].Text.Trim();
				string aspek = DTG_APPR.Items[row].Cells[2].Text.Trim();				

				conn.QueryString = "exec PARAM_GENERAL_RFCAFORMATASPEKAPPR '" + format + "', '" + aspek + "', '1', '" + Session["UserID"].ToString() + "'";
				conn.ExecuteNonQuery();

				/****
				string status =DTG_APPR.Items[row].Cells[4].Text.Trim();
								
				if (status.Equals("UPDATE"))
				{					
					conn.QueryString = "EXEC PARAM_GENERAL_RFCAFORMATASPEKAPPR 'UPDATE','" + format+ "','" + aspek + "','1'" ;
					conn.ExecuteNonQuery();
				}

				if (status.Equals("INSERT"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_RFCAFORMATASPEKAPPR 'INSERT','" + format+ "','" + aspek + "','1'" ;
					conn.ExecuteNonQuery();
				}
				if (status.Equals("DELETE"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_RFCAFORMATASPEKAPPR 'DELETE','" + format+ "','" + aspek + "',''" ;
					conn.ExecuteNonQuery();
				}
				
				conn.QueryString = "EXEC PARAM_GENERAL_RFCAFORMATASPEK_PENDING 'DELETE','" + format +"','" + aspek + "',''";
				conn.ExecuteNonQuery();
				***/
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string format = DTG_APPR.Items[row].Cells[0].Text.Trim();
				string aspek = DTG_APPR.Items[row].Cells[2].Text.Trim();

				conn.QueryString = "exec PARAM_GENERAL_RFCAFORMATASPEKAPPR '" + format + "', '" + aspek + "', '0', '" + Session["UserID"].ToString() + "'";
				conn.ExecuteNonQuery();

				/*
				conn.QueryString = "EXEC PARAM_GENERAL_RFCAFORMATASPEK_PENDING 'DELETE','" + format +"','" + aspek + "',''";
				conn.ExecuteNonQuery();
				*/
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
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
			
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
