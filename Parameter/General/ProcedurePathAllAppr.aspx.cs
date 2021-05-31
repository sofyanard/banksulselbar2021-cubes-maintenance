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

namespace CuBES_Maintenance.Parameter.General
{
	/// <summary>
	/// Summary description for ProcedurePathAllAppr.
	/// </summary>
	public partial class ProcedurePathAllAppr : System.Web.UI.Page
	{
		protected Connection connsme,conncc,conncons,conn;// = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		int jmlpar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			connsme = new Connection((string) Session["ConnString"]);
			SetDBConn();
			if (!IsPostBack)
			{
				string module = Request.QueryString["moduleID"];
				if (module == "" || module == null)
					this.LBL_STA.Text = "20";
				else
					this.LBL_STA.Text = module;
				try
				{
					this.RBL_MODULE.SelectedValue = this.LBL_STA.Text.Trim();
				} 
				catch {}
				ControlSME();
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

	
		protected void RBL_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.LBL_STA.Text = this.RBL_MODULE.SelectedValue;
			ControlSME();
		}

		private void ControlSME()
		{
			if (this.LBL_STA.Text == "01")
			{}//Response.Redirect("/CuBES-Maintenance/Parameter/Area/SME/RFProcedurePathAppr.aspx");
			else
				ViewPendingData();
		}

		private void SetDBConn()
		{
			string DB_NAMA,DB_IP,DB_LOGINID,DB_LOGINPWD;
			//SME Conn
			conn.QueryString = "select * from VW_GETCONN where MODULEID='01'";
			conn.ExecuteQuery();
			DB_NAMA		= conn.GetFieldValue("DB_NAMA");
			DB_IP		= conn.GetFieldValue("DB_IP");
			DB_LOGINID	= conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			connsme = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
			conn.ClearData();
			//Credit Card Conn
			conn.QueryString = "select * from VW_GETCONN where MODULEID='20'";
			conn.ExecuteQuery();
			DB_NAMA		= conn.GetFieldValue("DB_NAMA");
			DB_IP		= conn.GetFieldValue("DB_IP");
			DB_LOGINID	= conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncc = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
			conn.ClearData();
			//Consumer
			conn.QueryString = "select * from VW_GETCONN where MODULEID='40'";
			conn.ExecuteQuery();
			DB_NAMA		= conn.GetFieldValue("DB_NAMA");
			DB_IP		= conn.GetFieldValue("DB_IP");
			DB_LOGINID	= conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncons = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
			conn.ClearData();
		}

		private string GetBackPendingStatus(string saveMode) 
		{
			string status = "";			
			switch (saveMode.ToLower())
			{
				case "update":
					status = "0";
					break;
				case "insert":
					status = "1";
					break;
				case "delete":
					status = "2";
					break;
				default:
					status = "";
					break;
			}
			return status;
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}
		
		private void ViewPendingData()
		{
			string query = "select * from VW_PENDING_PATHPROCEDURE";
			if (this.LBL_STA.Text == "40")
			{
				conncons.QueryString = query;
				conncons.ExecuteQuery();
				DataTable dt = new DataTable();
				dt = conncons.GetDataTable().Copy();
				DGR_APPR.DataSource = dt;
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
			else if (this.LBL_STA.Text == "20")
			{
				conncc.QueryString = query;
				conncc.ExecuteQuery();
				DataTable dt = new DataTable();
				dt = conncc.GetDataTable().Copy();
				DGR_APPR.DataSource = dt;
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
			this.DGR_APPR.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
		}

		
		public void GetPath()
		{
			string type,par;
			if (this.LBL_STA.Text == "01")
				type = "01";
			else if (this.LBL_STA.Text == "20")
				type = "02";
			else 
				type = "03";
           
			connsme.QueryString = "select PARAMETER from RFPATHPARAMETER where TYPEID ='" + type+ "'";
			connsme.ExecuteQuery();
			par = connsme.GetFieldValue("PARAMETER");
			par = par.Replace('|','.');
			//split data
			int n = 0;
			if (par.Length > 0)
			{
				char[] sep = new char[]{'.'};
				foreach (string ss in par.Split(sep))
					n++;	
			}
			jmlpar = n-1;
		}

		private void performRequestList(int row, char appr_sta)
		{
			string userid = Session["UserID"].ToString();
			this.LBL_KEY.Text = "";
			for (int i=0; i < jmlpar; i++)
			{
				this.LBL_KEY.Text = ",'" + CleansText(DGR_APPR.Items[row].Cells[2*i+3].Text) + "'";
			}
			if (this.LBL_STA.Text == "20")
			{
				conncc.QueryString = " exec PARAM_PATHPROCEDURE_APPR '" + appr_sta + "'" + this.LBL_KEY.Text.Trim() + ",'" +
					userid + "'";
				conncc.ExecuteNonQuery();
			} 
			else if (this.LBL_STA.Text == "40")
			{
				conncons.QueryString = " exec PARAM_PATHPROCEDURE_APPR '" + appr_sta + "'" + this.LBL_KEY.Text.Trim()+ ",'" +
					userid + "'";
				conncons.ExecuteNonQuery();
				//Response.Write(conncons.QueryString);
			}
	
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			GetPath();
			for (int j = 0; j < DGR_APPR.Items.Count; j++)
			{
				try
				{
					RadioButton rbA = (RadioButton)  DGR_APPR.Items[j].FindControl("rdo_Approve"),
						rbR = (RadioButton)  DGR_APPR.Items[j].FindControl("rdo_Reject");
					if (rbA.Checked)
					{
						performRequestList(j, '1');
					}
					else if (rbR.Checked)
						performRequestList(j, '0');
				} 
				catch {}
			}
			ViewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../CommonParamApproval.aspx?ModuleID="+Request.QueryString["ModuleID"]);
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
				case "allReject":
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

		private void DGR_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
		
			this.DGR_APPR.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}
	}
}
