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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for InterestMarginParamAppr.
	/// </summary>
	public partial class InterestMarginParamAppr : System.Web.UI.Page
	{
		protected Connection conn,conn2;
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				bindData1();
			}
		}

		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void bindData1()
		{
			conn.QueryString="select  * ,case when CH_STA='0' then 'Update' when CH_STA='1' then 'Insert' "+
				"when CH_STA='2' then 'Delete' else '' end CH_STA1 "+
				"from TINTEREST_MARGIN order by MIN_LIMIT";
			conn.ExecuteQuery();
			this.DGR_APPR.DataSource = conn.GetDataTable().Copy();
			try 
			{
				this.DGR_APPR.DataBind();
			}
			catch 
			{
				this.DGR_APPR.CurrentPageIndex = this.DGR_APPR.PageCount - 1;
				this.DGR_APPR.DataBind();
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
			this.DGR_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR_ItemCommand);
			this.DGR_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPR_PageIndexChanged);

		}
		#endregion

		private void DGR_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR.CurrentPageIndex = e.NewPageIndex;
			bindData1();
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
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Pending");
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
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Pending");
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
			for (int i = 0; i < DGR_APPR.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject");
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
			bindData1();
		}

		private void performRequest(int row)
		{
			try 
			{
				string userid = Session["UserID"].ToString();
				string groupid = Session["GroupID"].ToString();
				string IM_SEQ = DGR_APPR.Items[row].Cells[7].Text.Trim();
				conn.QueryString="select * from TINTEREST_MARGIN where IM_SEQ='"+IM_SEQ+"'";
				conn.ExecuteQuery();
				string status = conn.GetFieldValue("CH_STA");
				string MIN_LIMIT = GlobalTools.ConvertFloat(conn.GetFieldValue("MIN_LIMIT"));
				string MAX_LIMIT = GlobalTools.ConvertFloat(conn.GetFieldValue("MAX_LIMIT"));
				string IM_VALUE = GlobalTools.ConvertFloat(conn.GetFieldValue("IM_VALUE"));
				
				if (status.Equals("0"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_INTEREST_MARGIN '3','"+IM_SEQ+"','" +
						MIN_LIMIT+ "','" +MAX_LIMIT+ "','" + IM_VALUE +"','"+status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}

				if (status.Equals("1"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_INTEREST_MARGIN '4','"+IM_SEQ+"','" +
						MIN_LIMIT+ "','" +MAX_LIMIT+ "','" + IM_VALUE +"','"+status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}
				if (status.Equals("2"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_INTEREST_MARGIN '5','"+IM_SEQ+"','" +
						MIN_LIMIT+ "','" +MAX_LIMIT+ "','" + IM_VALUE +"','"+status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}

				conn.QueryString = "EXEC PARAM_GENERAL_INTEREST_MARGIN '2','"+IM_SEQ+"','','','','' , '','' " ;
				conn.ExecuteNonQuery();
				
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string IM_SEQ = DGR_APPR.Items[row].Cells[7].Text.Trim();
				conn.QueryString = "EXEC PARAM_GENERAL_INTEREST_MARGIN '2','"+IM_SEQ+"','','','','' , '',''" ;
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleId=40");
		}

	}
}
