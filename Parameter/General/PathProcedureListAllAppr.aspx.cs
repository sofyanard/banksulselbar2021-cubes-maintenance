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

namespace CuBES_Maintenance.Parameter.General
{
	/// <summary>
	/// Summary description for PathProcedureListAllAppr.
	/// </summary>
	public partial class PathProcedureListAllAppr : System.Web.UI.Page
	{
		protected Connection conn;// = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc;
		protected Connection conncons; 
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn();
			if (!IsPostBack)
			{
				string module = Request.QueryString["moduleID"];
				if (module == "" || module == null)
					this.LBL_STA.Text = "01";
				else
					this.LBL_STA.Text = module;
				try
				{
					this.RBL_MODULE.SelectedValue = this.LBL_STA.Text.Trim();
				} 
				catch {}
				ControlInterface();
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

		private void SetDBConn()
		{
			string DB_NAMA;
			string DB_IP;
			string DB_LOGINID;
			string DB_LOGINPWD;
			//Credit Card Conn
			conn.QueryString = "select * from VW_GETCONN where MODULEID='20'";
			conn.ExecuteQuery();
			DB_NAMA = conn.GetFieldValue("DB_NAMA");
			DB_IP = conn.GetFieldValue("DB_IP");
			DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncc = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
			//Consumer
			conn.QueryString = "select * from VW_GETCONN where MODULEID='40'";
			conn.ExecuteQuery();
			DB_NAMA = conn.GetFieldValue("DB_NAMA");
			DB_IP = conn.GetFieldValue("DB_IP");
			DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncons = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void ControlInterface()
		{
			if (this.LBL_STA.Text == "01")
				Response.Redirect("../Area/SME/RFProcedurePathAppr.aspx");
			else
				ViewPendingData();
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

		private string getPendingStatus(string saveMode) 
		{
			string status = "";			
			switch (saveMode)
			{
				case "0":
					status = "Update";
					break;
				case "1":
					status = "Insert";
					break;
				case "2":
					status = "Delete";
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
			DataTable dt = new DataTable();
			if (this.LBL_STA.Text == "20")
			{
				conncc.QueryString = "select * from VW_PARAM_PENDING_RFPROCEDURELIST";
				conncc.ExecuteQuery();
				dt = conncc.GetDataTable().Copy();
			} 
			else if (this.LBL_STA.Text == "40")
			{
				conncons.QueryString = "select * from VW_PARAM_PENDING_RFPROCEDURELIST";
				conncons.ExecuteQuery();
				dt = conncons.GetDataTable().Copy();
			} 

			this.DGR_APPR.DataSource = dt;
			try
			{
				this.DGR_APPR.DataBind();
			}
			catch
			{
				DGR_APPR.CurrentPageIndex = DGR_APPR.PageCount - 1;
				DGR_APPR.DataBind();
			}
			for (int i=0; i< this.DGR_APPR.Items.Count; i++)
			{
				this.DGR_APPR.Items[i].Cells[11].Text = this.getPendingStatus(this.DGR_APPR.Items[i].Cells[11].Text);
			}
		}
		

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../CommonParamApproval.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		private void performRequestList(int row, char appr_sta)
		{
			string userid = Session["UserID"].ToString();
			string track_seq= CleansText(DGR_APPR.Items[row].Cells[0].Text.Trim());
			string track_from =  CleansText(DGR_APPR.Items[row].Cells[2].Text.Trim());	
			if (this.LBL_STA.Text == "20")
			{
				conncc.QueryString = " exec PARAM_CC_RFPROCEDURE_APPR '" + 
					appr_sta + "',  " + track_seq + " , '" + track_from + "','" +
					userid + "'";
				conncc.ExecuteNonQuery();
				//Response.Write(conncc.QueryString);
			} 
			else if (this.LBL_STA.Text == "40")
			{
				conncons.QueryString = "exec PARAM_RFPROCEDURE_APPR '" +
					appr_sta + "', " + track_seq + " , '" + track_from + "'";
				/*
				conncons.QueryString = "exec PARAM_RFPROCEDURE_APPR '" +
					appr_sta + "', " + track_seq + " , '" + track_from + "','" +
					userid + "'";
				*/	
				conncons.ExecuteNonQuery();
			}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGR_APPR.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton)  DGR_APPR.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton)  DGR_APPR.Items[i].FindControl("rdo_Reject");
					if (rbA.Checked)
						performRequestList(i, '1');
					else if (rbR.Checked)
						performRequestList(i, '0');
				} 
				catch {}
			}
			ViewPendingData();
		}

		private void DGR_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();		
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

		protected void RBL_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.LBL_STA.Text = this.RBL_MODULE.SelectedValue;
			ControlInterface();
		}
	}
}
