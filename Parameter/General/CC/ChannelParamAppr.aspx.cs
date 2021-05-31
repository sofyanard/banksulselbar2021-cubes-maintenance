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

namespace CuBES_Maintenance.Parameter.General.CC
{
	/// <summary>
	/// Summary description for OmsetParamAppr.
	/// </summary>
	public partial class OmsetParamAppr : System.Web.UI.Page
	{
		protected Connection conn2,conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				fillDDL();
				string tablename = Request.QueryString["tablename"];
				LBL_TITLE.Text = Request.QueryString["title"];
				LBL_TABLENAME.Text = tablename;
				/*
				if (LBL_TABLENAME.Text == "RFCHANNEL")
				{
					TR_MODULE.Visible			= true;
					LBL_LEFT_TITLE.Text			= "Parameter Maintenance : Common Approval";
					DDL_MODULE.SelectedValue	= "20";
				}
				else 
					TR_MODULE.Visible	= false;
				*/
				TR_MODULE.Visible			= false;
				
				if (tablename.Trim() == "RFSTATUSEMP")
				{
					DGR_APPR.Columns[0].HeaderText = "Code";
					DGR_APPR.Columns[1].HeaderText = "Description";
				}
				ViewPendingData();
			}
			this.DGR_APPR.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_APPR_PageIndexChanged);
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

		private void SetDBConn2()
		{
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			string DB_NAMA = conn.GetFieldValue("DB_NAMA");
			string DB_IP = conn.GetFieldValue("DB_IP");
			string DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conn2 = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void fillDDL()
		{
			/* if moduleid change, must fix this!!! */
			DDL_MODULE.Items.Add(new ListItem("SME","01"));
			DDL_MODULE.Items.Add(new ListItem("Consumer","40"));
			DDL_MODULE.Items.Add(new ListItem("Credit Card","20"));

			//try { DDL_MODULE.SelectedValue = "01"; } 
			//catch{}
		}

		private void ViewPendingData()
		{
			conn2.QueryString = "select * from VW_PENDING_CC_"+LBL_TABLENAME.Text;
			conn2.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			this.DGR_APPR.DataSource = dt;
			try
			{
				DGR_APPR.DataBind();
			}
			catch
			{
				DGR_APPR.CurrentPageIndex = DGR_APPR.PageCount - 1;
				DGR_APPR.DataBind();
			}
		}

		private void DGR_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (int i = 0; i < DGR_APPR.PageSize; i++)
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
				case "allReject":
					for (int i = 0; i < DGR_APPR.PageSize; i++)
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
					for (int i = 0; i < DGR_APPR.PageSize; i++)
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

		private void PerformRequest(string code, string desc, string sics, string status)
		{
			try 
			{
				string userid = Session["UserID"].ToString();
				conn2.QueryString = "exec PARAM_CC_RFCHANNEL_APPR '" + status + "','"+ code+"','" +
					desc+"','" +LBL_TABLENAME.Text+ "','" + sics+ "','" +
					userid + "'";
				conn2.ExecuteNonQuery();
			} 
			catch {}
		}

		private void DeleteData(string code, string desc, string status)
		{
			try 
			{
				conn2.QueryString = "exec PARAM_GENERAL_CC_RFCHANNEL '"+status+"', '"+code
					+"', '"+desc+"', 'PENDING_CC_"+LBL_TABLENAME.Text+"', '0', '' ";
				conn2.ExecuteNonQuery();
			} 
			catch {}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGR_APPR.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton)  DGR_APPR.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton)  DGR_APPR.Items[i].FindControl("rdo_Reject");
					string code = CleansText(DGR_APPR.Items[i].Cells[0].Text);
					string desc = CleansText(DGR_APPR.Items[i].Cells[1].Text);
					string sics = CleansText(DGR_APPR.Items[i].Cells[2].Text);
					string status = CleansText(DGR_APPR.Items[i].Cells[4].Text);
					if (rbA.Checked)
					{
						PerformRequest(code,desc,sics,status);
						DeleteData(code,desc,status);
					}
					else if (rbR.Checked)
						DeleteData(code,desc,status);
				} 
				catch {}
			}
			ViewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			/*if (LBL_TABLENAME.Text == "RFCHANNEL")
				Response.Redirect("../../CommonParamApproval.aspx?mc=99020202"); 	
			else
			*/
				Response.Redirect("../../HostParamApproval.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		protected void DDL_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (DDL_MODULE.SelectedValue == "01" || DDL_MODULE.SelectedValue == "40")
				Response.Redirect("../../General/Consumer/ChannelsParamAppr.aspx?ModuleId=40&mod=" + DDL_MODULE.SelectedValue);
		}
	}
}
