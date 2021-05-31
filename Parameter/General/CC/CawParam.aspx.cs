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
	/// Summary description for CawParam.
	/// </summary>
	public partial class CawParam : System.Web.UI.Page
	{
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn2,conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				FillDDL();
				ViewExistingData();
			}
			this.DDL_CW_TYPE.Enabled = false;
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

		private void FillDDL()
		{
			GlobalTools.fillRefList(this.DDL_CW_CODE,"SELECT * FROM PARAM_CC_DDL_RFCAWCAL",conn2);
			//GlobalTools.fillRefList(this.DDL_CW_TYPE,"",conn2);
			GlobalTools.fillRefList(this.DDL_JOB_TYPE_ID,"SELECT * FROM VW_PARAM_DDL_JOB_TYPE",conn2);
			GlobalTools.fillRefList(this.DDL_PRODUCTID,"SELECT * FROM VW_PARAM_CC_DDLPRODUCT",conn2);
		}

		private void ClearBoxes()
		{
			try { this.DDL_CW_CODE.SelectedValue		= ""; } 
			catch{}
			try { this.DDL_CW_TYPE.SelectedValue		= ""; }
			catch{}
			try { this.DDL_JOB_TYPE_ID.SelectedValue	= ""; }
			catch{}
			try { this.DDL_PRODUCTID.SelectedValue	= ""; }
			catch{}
			this.TXT_CW_MAX.Text				= "";
			this.TXT_CW_MIN.Text				= "";
			activateControlKey(true);
		}

		private void activateControlKey(bool isReadOnly) 
		{
			this.DDL_CW_CODE.Enabled		= isReadOnly;
			//this.DDL_CW_TYPE.Enabled		= isReadOnly;
			this.DDL_JOB_TYPE_ID.Enabled	= isReadOnly;
			this.DDL_PRODUCTID.Enabled		= isReadOnly;
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

		private void ViewExistingData()
		{
			conn2.QueryString = "select * from VW_PARAM_CC_RFCAWCAL";
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
			for (int i=0;i<DGR_EXISTING.Items.Count;i++)
			{
				this.DGR_EXISTING.Items[i].Cells[9].Text = GlobalTools.MoneyFormat(DGR_EXISTING.Items[i].Cells[9].Text) ;
				this.DGR_EXISTING.Items[i].Cells[10].Text = GlobalTools.MoneyFormat(DGR_EXISTING.Items[i].Cells[10].Text) ;
			}	
		}

		private void ViewPendingData()
		{
			//conn2.QueryString = "select * from PENDING_CC_RFCAWCAL";
			conn2.QueryString = "select * from VW_PENDING_CC_RFCAWCAL";
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
				this.DGR_REQUEST.Items[i].Cells[10].Text = GlobalTools.MoneyFormat(DGR_REQUEST.Items[i].Cells[10].Text) ;
				this.DGR_REQUEST.Items[i].Cells[11].Text = GlobalTools.MoneyFormat(DGR_REQUEST.Items[i].Cells[11].Text) ;
				this.DGR_REQUEST.Items[i].Cells[12].Text = getPendingStatus(DGR_REQUEST.Items[i].Cells[12].Text) ;
			}			
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearBoxes();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn2.QueryString = "select * from VW_PARAM_CC_RFCAWCAL where CW_CODE ='"+ this.DDL_CW_CODE.SelectedValue.Trim() +"' " +
					"and  CW_TYPE = '" + this.DDL_CW_TYPE.SelectedValue.Trim() + "' " +
					"and  JOB_TYPE_ID = '" + this.DDL_JOB_TYPE_ID.SelectedValue.Trim() + "' " +
					"and  PRODUCTID = '" + this.DDL_PRODUCTID.SelectedValue.Trim() + "' ";
				conn2.ExecuteQuery();
				int jml1 = conn2.GetRowCount();
			
				if (jml1 > 0)
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}	
			conn2.QueryString = "exec PARAM_GENERAL_CC_RFCAWCAL_MAKER  '"+this.LBL_SAVEMODE.Text +"','" +
				this.DDL_CW_CODE.SelectedValue + "','" + this.DDL_CW_TYPE.SelectedValue + "','" +
				this.DDL_JOB_TYPE_ID.SelectedValue + "','" + this.DDL_PRODUCTID.SelectedValue + "'," +
				GlobalTools.ConvertFloat(this.TXT_CW_MIN.Text) + "," + GlobalTools.ConvertFloat(this.TXT_CW_MAX.Text);
			conn2.ExecuteQuery();
			this.LBL_SAVEMODE.Text = "1";
			ViewPendingData();
			ClearBoxes();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					try {this.DDL_CW_CODE.SelectedValue	 = CleansText(e.Item.Cells[0].Text);} 
					catch {}
					try {this.DDL_CW_TYPE.SelectedValue	 = CleansText(e.Item.Cells[1].Text);} 
					catch {}
					try {this.DDL_JOB_TYPE_ID.SelectedValue	 = CleansText(e.Item.Cells[2].Text);} 
					catch {}
					try {this.DDL_PRODUCTID.SelectedValue	 = CleansText(e.Item.Cells[3].Text);} 
					catch {}
					this.TXT_CW_MIN.Text	 = CleansText(e.Item.Cells[4].Text);
					this.TXT_CW_MAX.Text	 = CleansText(e.Item.Cells[5].Text);
					activateControlKey(false);
					break;

				case "delete":					
					string cw_code		= CleansText(e.Item.Cells[0].Text);
					string cw_type		= CleansText(e.Item.Cells[1].Text);
					string job_type_id	= CleansText(e.Item.Cells[2].Text);
					string productid	= CleansText(e.Item.Cells[3].Text);
					conn2.QueryString = "exec PARAM_GENERAL_CC_RFCAWCAL_MAKER  '2','" +
						cw_code + "','" + cw_type + "','" + job_type_id + "','" + productid + "'," +
						GlobalTools.ConvertFloat(CleansText(e.Item.Cells[4].Text)) + "," +
						GlobalTools.ConvertFloat(CleansText(e.Item.Cells[5].Text));
					conn2.ExecuteQuery();
					ViewPendingData();
					break;

				default :
					break;
			}
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

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[12].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					try {this.DDL_CW_CODE.SelectedValue	 = CleansText(e.Item.Cells[0].Text);} 
					catch {}
					try {this.DDL_CW_TYPE.SelectedValue	 = CleansText(e.Item.Cells[1].Text);} 
					catch {}
					try {this.DDL_JOB_TYPE_ID.SelectedValue	 = CleansText(e.Item.Cells[2].Text);} 
					catch {}
					try {this.DDL_PRODUCTID.SelectedValue	 = CleansText(e.Item.Cells[3].Text);} 
					catch {}
					this.TXT_CW_MIN.Text	 = CleansText(e.Item.Cells[4].Text);
					this.TXT_CW_MAX.Text	 = CleansText(e.Item.Cells[5].Text);
					activateControlKey(false);
					break;

				case "delete":
					string cw_code		= CleansText(e.Item.Cells[0].Text);
					string cw_type		= CleansText(e.Item.Cells[1].Text);
					string job_type_id	= CleansText(e.Item.Cells[2].Text);
					string productid	= CleansText(e.Item.Cells[3].Text);
					conn2.QueryString = "delete from PENDING_CC_RFCAWCAL where CW_CODE ='"+ cw_code +"' " +
						"and  CW_TYPE = '" + cw_type + "' " +
						"and  JOB_TYPE_ID = '" + job_type_id + "' " +
						"and  PRODUCTID = '" + productid + "' ";
					conn2.ExecuteNonQuery();
					ViewPendingData();
					break;
				default :
					break;
			}
		}

	}
}
