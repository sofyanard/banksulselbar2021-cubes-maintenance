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


namespace CUBESPARAMETER
{
	/// <summary>
	/// Summary description for AgeLimitParamAppr.
	/// </summary>
	public partial class AgeLimitParamAppr : System.Web.UI.Page
	{
		protected Connection conn,conn2;
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			
			if (!IsPostBack)
			{
				bindData();
			}
			DTG_APPR.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change);
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

		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID = 40";
			conn2.ExecuteQuery();

			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		void Grid_Change(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DTG_APPR.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData();	
		}
		private void bindData()
		{
			conn.QueryString ="select b.productname,c.des,a.min_age,a.max_age,a.product_id,a.job_type_id,ch_sta, "+
				"case when ch_sta='0' then 'Update' when ch_sta='1' then 'Insert' when ch_sta='2' then 'Delete' else '' end CH_STA1 "+
				",seq_id from TPRODUCT_JOBTYPE a left join tproduct b on a.product_id=b.productid "+
				"left join job_type c on a.job_type_id=c.job_type_id ";
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
			
		}

		private void performRequest(int row)
		{
			try 
			{
				string userid = Session["UserID"].ToString();
				string groupid = Session["GroupID"].ToString();
				string status = DTG_APPR.Items[row].Cells[6].Text.Trim();
				
				string productid = DTG_APPR.Items[row].Cells[4].Text.Trim();
				string jobsid = DTG_APPR.Items[row].Cells[5].Text.Trim();
				string min=DTG_APPR.Items[row].Cells[2].Text.Trim();
				string max=DTG_APPR.Items[row].Cells[3].Text.Trim();
				
				if (status.Equals("0"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_AGELIMIT '3','1','" +
						productid+ "', '" +jobsid+ "', '" + 
						min+ "', '"+max+"', '1' ,'"+userid+"','"+groupid+"' " ;
					conn.ExecuteNonQuery();
				}

				if (status.Equals("1"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_AGELIMIT '4','1','" +
						productid+ "', '" +jobsid+ "', '" + 
						min+ "', '"+max+"', '1' ,'"+userid+"','"+groupid+"' " ;
					conn.ExecuteNonQuery();
				}
				if (status.Equals("2"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_AGELIMIT '5','1','" +
						productid+ "', '" +jobsid+ "', '" + 
						min+ "', '"+max+"', '1' ,'"+userid+"','"+groupid+"' " ;
					conn.ExecuteNonQuery();
				}

				string seq = DTG_APPR.Items[row].Cells[8].Text.Trim();
				conn.QueryString = "EXEC PARAM_GENERAL_AGELIMIT '2','"+seq+"',' ','','',' ','' , '','' " ;
				conn.ExecuteNonQuery();

				
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string seq = DTG_APPR.Items[row].Cells[8].Text.Trim();
				conn.QueryString = "EXEC PARAM_GENERAL_AGELIMIT '2','"+seq+"',' ','','',' ','' , '',''" ;
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleId=40");
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DTG_APPR.Items.Count; i++)
			{
				try
				{
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
				case "allRejc":
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
