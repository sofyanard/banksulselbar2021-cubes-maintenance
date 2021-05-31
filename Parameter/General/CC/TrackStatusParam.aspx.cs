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

namespace CuBES_Maintenance.Parameter.General.CC
{
	/// <summary>
	/// Summary description for TrackStatusParam.
	/// </summary>
	public partial class TrackStatusParam : System.Web.UI.Page
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
			ViewPendingData();
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			this.DGR_REQUEST.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_REQUEST_PageIndexChanged);
			this.DGR_EXISTING.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_EXISTING_PageIndexChanged);
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

		private void FillDDL()
		{
			DDL_ACTIVE.Items.Add(new ListItem("- SELECT -",""));
			DDL_ACTIVE.Items.Add(new ListItem("Yes","1"));
			DDL_ACTIVE.Items.Add(new ListItem("No","0"));

			DDL_TR_FILTER.Items.Add(new ListItem("- SELECT -",""));
			DDL_TR_FILTER.Items.Add(new ListItem("Yes","1"));
			DDL_TR_FILTER.Items.Add(new ListItem("No","0"));
		}

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

		private void ClearControls()
		{
			this.TXT_TR_CODE.Text			= "";
			this.TXT_TR_DESC.Text			= "";
			this.TXT_MAXDAY.Text			= "";
			try {this.DDL_ACTIVE.SelectedValue   = "";}
			catch{}
			try {this.DDL_TR_FILTER.SelectedValue	= "";}
			catch{}
			this.activateControlKey(false);
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

		private string YesNoStatus(string saveMode) 
		{
			string status = "";			
			switch (saveMode)
			{
				case "0":
					status = "No";
					break;
				case "1":
					status = "Yes";
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

		private void activateControlKey(bool isReadOnly) 
		{
			this.TXT_TR_CODE.ReadOnly= isReadOnly;
		}

		private void ViewExistingData()
		{
			conn2.QueryString = "select * from RFTRACKLST";
			conn2.ExecuteQuery();
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
			for (int i=0; i< this.DGR_EXISTING.Items.Count; i++)
			{
				this.DGR_EXISTING.Items[i].Cells[5].Text = this.YesNoStatus(this.DGR_EXISTING.Items[i].Cells[5].Text.Trim());
				this.DGR_EXISTING.Items[i].Cells[6].Text = this.YesNoStatus(this.DGR_EXISTING.Items[i].Cells[6].Text.Trim());
			}
		}

		private void ViewPendingData()
		{
			conn2.QueryString = "select * from PENDING_CC_RFTRACKLST";
			conn2.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			this.DGR_REQUEST.DataSource = dt;
			try
			{
				this.DGR_REQUEST.DataBind();
			}
			catch
			{
				DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				DGR_REQUEST.DataBind();
			}
			for (int i=0; i< this.DGR_REQUEST.Items.Count; i++)
			{
				this.DGR_REQUEST.Items[i].Cells[8].Text = this.getPendingStatus(this.DGR_REQUEST.Items[i].Cells[8].Text.Trim());
				this.DGR_REQUEST.Items[i].Cells[7].Text = this.YesNoStatus(this.DGR_REQUEST.Items[i].Cells[7].Text.Trim());
				this.DGR_REQUEST.Items[i].Cells[6].Text = this.YesNoStatus(this.DGR_REQUEST.Items[i].Cells[6].Text.Trim());
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearControls();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn2.QueryString = "select * from RFTRACKLST  where TR_CODE ='"+ this.TXT_TR_CODE.Text.Trim() + "'";
				conn2.ExecuteQuery();
				int jml1 = conn2.GetRowCount();
				/*
				conn2.QueryString = "select * from PENDING_CC_RFTRACKLST where TR_CODE ='"+ this.TXT_TR_CODE.Text.Trim() + "'";
				conn2.ExecuteQuery();
				int jml2 = conn2.GetRowCount();
				*/
				if (jml1 > 0)
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		
			if (this.TXT_MAXDAY.Text == "") this.TXT_MAXDAY.Text = "0";
			conn2.QueryString = "exec PARAM_GENERAL_CC_RFTRACKLST_MAKER   '" +this.LBL_SAVEMODE.Text +"','" +
				this.TXT_TR_CODE.Text + "','" + this.TXT_TR_DESC.Text + "','" +
				this.TXT_MAXDAY.Text +"','" + this.DDL_ACTIVE.SelectedValue + "','" +
				this.DDL_TR_FILTER.SelectedValue + "'";
			try
			{
				conn2.ExecuteQuery();
			} 
			catch{}
			this.LBL_SAVEMODE.Text = "1";
			ViewPendingData();
			ClearControls();
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					this.TXT_TR_CODE.Text					= CleansText(e.Item.Cells[0].Text.Trim());
					this.TXT_TR_DESC.Text					= CleansText(e.Item.Cells[1].Text.Trim()); 
					this.TXT_MAXDAY.Text					= CleansText(e.Item.Cells[2].Text.Trim()); 
					try{this.DDL_ACTIVE.SelectedValue		= CleansText(e.Item.Cells[3].Text.Trim());} 
					catch{}
					try{this.DDL_TR_FILTER.SelectedValue	= CleansText(e.Item.Cells[4].Text.Trim());} 
					catch{}
					activateControlKey(true);
					break;

				case "delete":		
					conn2.QueryString = "exec PARAM_GENERAL_CC_RFTRACKLST_MAKER  '2','" +
						CleansText(e.Item.Cells[0].Text.Trim()) + "','" + CleansText(e.Item.Cells[1].Text.Trim()) + "','" +
						CleansText(e.Item.Cells[2].Text.Trim()) + "','" + CleansText(e.Item.Cells[3].Text.Trim()) + "','" + 
						CleansText(e.Item.Cells[4].Text.Trim()) + "'";
					try
					{
						conn2.ExecuteQuery();
					} 
					catch{}
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
					LBL_SAVEMODE.Text = CleansText(e.Item.Cells[5].Text.Trim());
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					this.TXT_TR_CODE.Text					= CleansText(e.Item.Cells[0].Text.Trim());
					this.TXT_TR_DESC.Text					= CleansText(e.Item.Cells[1].Text.Trim()); 
					this.TXT_MAXDAY.Text					= CleansText(e.Item.Cells[2].Text.Trim()); 
					try{this.DDL_ACTIVE.SelectedValue		= CleansText(e.Item.Cells[3].Text.Trim());} 
					catch{}
					try{this.DDL_TR_FILTER.SelectedValue	= CleansText(e.Item.Cells[4].Text.Trim());} 
					catch{}
					activateControlKey(true);
					break;

				case "delete":
					conn2.QueryString = " delete from PENDING_CC_RFTRACKLST where TR_CODE = '"+
						CleansText(e.Item.Cells[0].Text.Trim()) + "'";
                    conn2.ExecuteNonQuery();
					ViewPendingData();
					break;
				default :
					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}
	}
}

