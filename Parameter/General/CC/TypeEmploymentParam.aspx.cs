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
	/// Summary description for TypeEmploymentParam.
	/// </summary>
	public partial class TypeEmploymentParam : System.Web.UI.Page
	{
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn2,conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				GlobalTools.fillRefList(DDL_GR_CODE,"select * from VW_PARAM_JOB_TYPE_GROUPEMP",conn2);
				ViewExistingData();
			}
			ViewPendingData();
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			this.DGR_EXISTING.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_REQUEST_PageIndexChanged);
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
			this.DGR_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_ItemCommand);
			this.DGR_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

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

		private void activateControlKey(bool isReadOnly) 
		{
			this.TXT_JOB_TYPE_ID.ReadOnly = isReadOnly;
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

		private void ClearControls() 
		{
			this.TXT_JOB_TYPE_ID.Text		= "";
			this.TXT_DESC.Text				= "";
			this.DDL_GR_CODE.SelectedValue	= "";
			activateControlKey(false);
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ViewExistingData()
		{
			conn2.QueryString = "select * from VW_PARAM_JOB_TYPE where ACTIVE='1'";
			conn2.ExecuteQuery();
			this.DGR_EXISTING.Visible = true;
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			DGR_EXISTING.DataSource = dt;
			try
			{
				DGR_EXISTING.DataBind();
			}
			catch
			{
				DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.PageCount - 1;
				DGR_EXISTING.DataBind();
			}
		}

		private void ViewPendingData()
		{
			conn2.QueryString = "select * from PENDING_CC_JOB_TYPE";
			conn2.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			DGR_REQUEST.DataSource = dt;
			try
			{
				DGR_REQUEST.DataBind();
			}
			catch
			{
				DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				DGR_REQUEST.DataBind();
			}
			for (int i=0;i<DGR_REQUEST.Items.Count;i++)
			{
				this.DGR_REQUEST.Items[i].Cells[3].Text = getPendingStatus(DGR_REQUEST.Items[i].Cells[3].Text) ;
			}			
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn2.QueryString = "select * from VW_PARAM_JOB_TYPE where ACTIVE ='1' and JOB_TYPE_ID ='"+ this.TXT_JOB_TYPE_ID.Text +"'";
				conn2.ExecuteQuery();
				int jml1 = conn2.GetRowCount();
				/*
				conn2.QueryString = "select * from PENDING_CC_JOB_TYPE where JOB_TYPE_ID ='"+ this.TXT_JOB_TYPE_ID.Text +"'";
				conn2.ExecuteQuery();
				int jml2 = conn2.GetRowCount();
				*/
				if (jml1 > 0)
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		
			conn2.QueryString = "exec PARAM_GENERAL_CC_JOB_TYPE_MAKER '"+this.LBL_SAVEMODE.Text +"','" +
				this.TXT_JOB_TYPE_ID.Text + "','" + this.DDL_GR_CODE.SelectedValue + "','" +
				this.TXT_DESC.Text +"'";
			conn2.ExecuteQuery();
			this.LBL_SAVEMODE.Text = "1";
			ViewPendingData();
			ClearControls();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					this.TXT_JOB_TYPE_ID.Text = CleansText(e.Item.Cells[0].Text);
					this.TXT_DESC.Text = CleansText(e.Item.Cells[1].Text);
					try
					{
						this.DDL_GR_CODE.SelectedValue = CleansText(e.Item.Cells[2].Text);
					}
					catch{}
					activateControlKey(true);
					break;

				case "delete":					
					conn2.QueryString = "exec PARAM_GENERAL_CC_JOB_TYPE_MAKER '2','" +
						CleansText(e.Item.Cells[0].Text) + "','" + CleansText(e.Item.Cells[2].Text) + "','" +
						CleansText(e.Item.Cells[1].Text)+"'";
					conn2.ExecuteQuery();
					ViewPendingData();
					break;

				default :
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[4].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					this.TXT_JOB_TYPE_ID.Text = CleansText(e.Item.Cells[0].Text);
					this.TXT_DESC.Text = CleansText(e.Item.Cells[1].Text);
					try
					{
						this.DDL_GR_CODE.SelectedValue =  CleansText(e.Item.Cells[2].Text);
					} catch{}
					activateControlKey(true);
					break;

				case "delete":
					string id = e.Item.Cells[0].Text;
					conn2.QueryString = "delete from PENDING_CC_JOB_TYPE WHERE JOB_TYPE_ID='" + id + "'";
					conn2.ExecuteQuery();
					ViewPendingData();
					break;
				default :
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
			Response.Redirect("../../HostParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearControls();
			LBL_SAVEMODE.Text = "1";
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}
	}
}
