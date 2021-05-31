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
	/// Summary description for ChannBPRRulesAppr.
	/// </summary>
	public partial class ChannBPRRulesAppr : System.Web.UI.Page
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
				bindData2();
			}
			DTG_APPR.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change);
		}

		private void bindData()
		{
			conn.QueryString = "SELECT A.CH_PRM_CODE,(select top 1 CU_COMPNAME from cust_company c where c.cu_ref=a.CH_BPR_CUREF)KANTOR,B.CH_PRM_NAME,A.CH_BPR_CUREF,A.STATUS  FROM PENDING_CHANN_BPR_RULES A JOIN RFCHANN_PARAM_LIST B ON A.CH_PRM_CODE=B.CH_PRM_CODE ORDER BY CH_BPR_CUREF";
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

		private void bindData2()
		{
			conn.QueryString = "select A.CH_PRM_CODE,A.CH_BPR_CU_REF,CH_VALUE_CODE,CH_VALUE1,CH_VALUE2,CH_VALUE3,CH_SCORE "+
				",(select top 1 CU_COMPNAME from cust_company c "+
				"where c.cu_ref=a.CH_BPR_CU_REF)KANTOR,B.CH_PRM_NAME,PENDINGSTATUS "+
				"from PENDING_CHANN_BPR_RULEVALUE A "+
				"JOIN RFCHANN_PARAM_LIST B ON A.CH_PRM_CODE=B.CH_PRM_CODE ";
			conn.ExecuteQuery();
			DtGrd_DetailMaker.DataSource = conn.GetDataTable().Copy();
			try 
			{
				DtGrd_DetailMaker.DataBind();
			}
			catch 
			{
				DtGrd_DetailMaker.CurrentPageIndex = DtGrd_DetailMaker.PageCount - 1;
				DtGrd_DetailMaker.DataBind();
			}
			for (int i = 0; i < DtGrd_DetailMaker.Items.Count; i++)
			{
				if (DtGrd_DetailMaker.Items[i].Cells[13].Text.Trim() == "0")
				{
					DtGrd_DetailMaker.Items[i].Cells[6].Text = "UPDATE";
				}
				else if (DtGrd_DetailMaker.Items[i].Cells[13].Text.Trim() == "1")
				{
					DtGrd_DetailMaker.Items[i].Cells[6].Text = "INSERT";
				}
				else if (DtGrd_DetailMaker.Items[i].Cells[13].Text.Trim() == "2")
				{
					DtGrd_DetailMaker.Items[i].Cells[6].Text = "DELETE";
				}
			}
		}

		private void performRequest(int row)
		{
			try 
			{
				string curef = DTG_APPR.Items[row].Cells[0].Text.Trim();
				string rules = DTG_APPR.Items[row].Cells[2].Text.Trim();

				conn.QueryString = "exec PARAM_GENERAL_CHANNBPRRULES_APPR '" + curef + "', '" + rules + "', '1', '" + Session["UserID"].ToString() + "'";
				conn.ExecuteNonQuery();

				/***
				string kantor = DTG_APPR.Items[row].Cells[1].Text.Trim();
				string status =DTG_APPR.Items[row].Cells[3].Text.Trim();
								
				if (status.Equals("UPDATE"))
				{	conn.QueryString = "UPDATE CHANN_BPR_RULES SET CH_PRM_CODE='" +
						rules+ "',CH_BPR_CUREF='"+curef+"',MANDATORY='1' WHERE CH_PRM_CODE='" +
						rules+ "' AND CH_BPR_CUREF='"+curef+"' " ;
					conn.ExecuteNonQuery();
				}

				if (status.Equals("INSERT"))
				{
					conn.QueryString = "INSERT INTO CHANN_BPR_RULES VALUES('" +
						rules+ "', '"+curef+"','1')" ;
					conn.ExecuteNonQuery();
				}
				if (status.Equals("DELETE"))
				{
					conn.QueryString = "DELETE FROM  CHANN_BPR_RULES WHERE CH_PRM_CODE='" +
						rules+ "' AND CH_BPR_CUREF='"+curef+"' " ;
					conn.ExecuteNonQuery();
				}

				conn.QueryString = "DELETE FROM  PENDING_CHANN_BPR_RULES WHERE CH_PRM_CODE='" +
					rules+ "' AND CH_BPR_CUREF='"+curef+"' " ;
				conn.ExecuteNonQuery();
				****/
				
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string curef = DTG_APPR.Items[row].Cells[0].Text.Trim();
				string rules = DTG_APPR.Items[row].Cells[2].Text.Trim();

				conn.QueryString = "exec PARAM_GENERAL_CHANNBPRRULES_APPR '" + curef + "', '" + rules + "', '0', '" + Session["UserID"].ToString() + "'";
				conn.ExecuteNonQuery();

				/***
				conn.QueryString = "DELETE FROM  PENDING_CHANN_BPR_RULES WHERE CH_PRM_CODE='" +
					rules+ "'AND CH_BPR_CUREF='"+curef+"' " ;
				conn.ExecuteNonQuery();
				***/
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
			this.DtGrd_DetailMaker.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DtGrd_DetailMaker_ItemCommand);
			this.DtGrd_DetailMaker.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DtGrd_DetailMaker_PageIndexChanged);

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

		private void DtGrd_DetailMaker_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DtGrd_DetailMaker.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DtGrd_DetailMaker.Items[i].FindControl("Radiobutton4"),
								rbB = (RadioButton) DtGrd_DetailMaker.Items[i].FindControl("Radiobutton5"),
								rbC = (RadioButton) DtGrd_DetailMaker.Items[i].FindControl("Radiobutton6");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DtGrd_DetailMaker.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DtGrd_DetailMaker.Items[i].FindControl("Radiobutton4"),
								rbB = (RadioButton) DtGrd_DetailMaker.Items[i].FindControl("Radiobutton5"),
								rbC = (RadioButton) DtGrd_DetailMaker.Items[i].FindControl("Radiobutton6");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DtGrd_DetailMaker.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DtGrd_DetailMaker.Items[i].FindControl("Radiobutton4"),
								rbB = (RadioButton) DtGrd_DetailMaker.Items[i].FindControl("Radiobutton5"),
								rbC = (RadioButton) DtGrd_DetailMaker.Items[i].FindControl("Radiobutton6");
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

		protected void btn_submit2_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DtGrd_DetailMaker.Items.Count; i++)
			{
				try
					
				{
					RadioButton rbA = (RadioButton) DtGrd_DetailMaker.Items[i].FindControl("Radiobutton4"),
						rbR = (RadioButton) DtGrd_DetailMaker.Items[i].FindControl("Radiobutton5");
					if (rbA.Checked)
					{
						performRequest2(i);
					}
					else if (rbR.Checked)
					{
						deleteData2(i);
					}
				} 
				catch {}
			}
			bindData2();
		}

		private void performRequest2(int row)
		{
			try 
			{
				string curef = DtGrd_DetailMaker.Items[row].Cells[10].Text.Trim();
				string rules = DtGrd_DetailMaker.Items[row].Cells[11].Text.Trim();
				string valuecode = DtGrd_DetailMaker.Items[row].Cells[12].Text.Trim();
				conn.QueryString = "exec PARAM_GENERAL_CHANNBPRRULESVALUE_APPR '" + curef + "', '" + rules + "', '"+valuecode+"','1','TEST' ";
				conn.ExecuteNonQuery();

			} 
			catch (Exception p)
			{
				GlobalTools.popMessage(this,p.Message);
			}

		}

		private void deleteData2(int row)
		{
			try 
			{
				string curef = DtGrd_DetailMaker.Items[row].Cells[10].Text.Trim();
				string rules = DtGrd_DetailMaker.Items[row].Cells[11].Text.Trim();
				string valuecode = DtGrd_DetailMaker.Items[row].Cells[12].Text.Trim();//'" + Session["UserID"].ToString() + "'

				conn.QueryString = "exec PARAM_GENERAL_CHANNBPRRULESVALUE_APPR '" + curef + "', '" + rules + "', '"+valuecode+"','0','" + Session["UserID"].ToString() + "' ";
				conn.ExecuteNonQuery();

			} 
			catch {}
		}

		private void DtGrd_DetailMaker_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DtGrd_DetailMaker.CurrentPageIndex = e.NewPageIndex;
			bindData2();
		}

	
	}
}
