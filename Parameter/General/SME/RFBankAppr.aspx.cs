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
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFBankAppr.
	/// </summary>
	public partial class RFBankAppr : System.Web.UI.Page
	{
		protected Tools tool = new Tools();
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				viewPendingData();
			}
		}

		private void viewPendingData() 
		{
			conn.QueryString = "select * from VW_PARAM_RFBANK_VIEWPENDING";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG_PENDING.DataSource = dt;
			try 
			{
				DG_PENDING.DataBind();
			} 
			catch 
			{
				DG_PENDING.CurrentPageIndex = 0;
				DG_PENDING.DataBind();
			}
		}

		private void PerformRequest(int row)
		{
			try 
			{
				string id = DG_PENDING.Items[row].Cells[0].Text.Trim();
				conn.QueryString = "exec PARAM_RFBANK_APPROVAL '" + id + "', '1'";
				conn.ExecuteQuery();
			} 
			catch (Exception ex)
			{
				Response.Write("<!--" + ex.Message + "-->");
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)		
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this,errmsg);
			}
		}

		private void DeleteData(int row)
		{
			try 
			{
				string id = DG_PENDING.Items[row].Cells[0].Text.Trim();
				conn.QueryString = "exec PARAM_RFBANK_APPROVAL '" + id + "', '0'";
				conn.ExecuteQuery();
			} 
			catch (Exception ex)
			{
				Response.Write("<!--" + ex.Message + "-->");
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)		
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this,errmsg);
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
			this.DG_PENDING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG_PENDING_ItemCommand);
			this.DG_PENDING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_PENDING_PageIndexChanged);

		}
		#endregion

		private void DG_PENDING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DG_PENDING.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DG_PENDING.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DG_PENDING.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DG_PENDING.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DG_PENDING.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DG_PENDING.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DG_PENDING.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DG_PENDING.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DG_PENDING.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DG_PENDING.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DG_PENDING.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DG_PENDING.Items[i].FindControl("rdo_Pending");
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

		private void DG_PENDING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_PENDING.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DG_PENDING.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DG_PENDING.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DG_PENDING.Items[i].FindControl("rdo_Reject");
					if (rbA.Checked)
					{
						PerformRequest(i);
					}
					else if (rbR.Checked)
					{
						DeleteData(i);
					}
				} 
				catch {}
			}
			viewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			/*
            string classid="";
			try 
			{
				classid=Request.QueryString["classid"].ToString();
			}
			catch{ classid="";}
			
			if ((classid.Equals("01")) || (classid.ToString().Trim()=="01") )
				Response.Redirect("../../HostParamApproval.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+" ");
			else
				Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
            */
		}
	}
}
