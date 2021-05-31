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
	/// Summary description for PathProcedureParam.
	/// </summary>
	public partial class PathProcedureParam : System.Web.UI.Page
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
			this.DDL_APPTYPE.SelectedValue	= "";
			this.DDL_TRACK_BACK.SelectedValue = "";
			this.DDL_TRACK_FAIL.SelectedValue = "";
			this.DDL_TRACK_FROM.SelectedValue = "";
			this.DDL_TRACK_NEXT.SelectedValue	= "";
			this.activateControlKey(true);
		}

		private void FillDDL()
		{
			GlobalTools.fillRefList(this.DDL_APPTYPE,"SELECT * FROM VW_PARAM_CC_RFPROCEDURE_APPTYPE",conn2);
			GlobalTools.fillRefList(this.DDL_APPTYPE1,"SELECT * FROM VW_PARAM_CC_RFPROCEDURE_APPTYPE",conn2);
			GlobalTools.fillRefList(this.DDL_TRACK_BACK,"SELECT * FROM VW_PARAM_CC_RFPROCEDURE_TRACK",conn2);
			GlobalTools.fillRefList(this.DDL_TRACK_FAIL,"SELECT * FROM VW_PARAM_CC_RFPROCEDURE_TRACK",conn2);
			GlobalTools.fillRefList(this.DDL_TRACK_FROM,"SELECT * FROM VW_PARAM_CC_RFPROCEDURE_TRACK",conn2);
			GlobalTools.fillRefList(this.DDL_TRACK_NEXT,"SELECT * FROM VW_PARAM_CC_RFPROCEDURE_TRACK",conn2);
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

		private void activateControlKey(bool isReadOnly) 
		{
			this.DDL_APPTYPE.Enabled	= isReadOnly;
			this.DDL_TRACK_FROM.Enabled = isReadOnly;
		}

		private void ViewExistingData()
		{   if (this.DDL_APPTYPE1.SelectedValue == "")
				conn2.QueryString = "select * from VW_PARAM_CC_RFPROCEDURE";
			else
				conn2.QueryString = "select * from VW_PARAM_CC_RFPROCEDURE where TRACK_SEQ =" + this.DDL_APPTYPE1.SelectedValue;
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
		}

		private void ViewPendingData()
		{
			conn2.QueryString = "select * from VW_PARAM_CC_PENDING_RFPROCEDURE";
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
				this.DGR_REQUEST.Items[i].Cells[11].Text = this.getPendingStatus(this.DGR_REQUEST.Items[i].Cells[11].Text);
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn2.QueryString = "select * from VW_PARAM_CC_RFPROCEDURE where TRACK_SEQ ="+ this.DDL_APPTYPE.SelectedValue +" and " +
					"TRACK_FROM = '" + this.DDL_TRACK_FROM.SelectedValue + "'";
				conn2.ExecuteQuery();
				int jml1 = conn2.GetRowCount();
				/*
				conn2.QueryString = "select * from PENDING_CC_RFPROCEDURE where TRACK_SEQ ="+ this.DDL_APPTYPE.SelectedValue +" and " +
					"TRACK_FROM = '" + this.DDL_TRACK_FROM.SelectedValue + "'";
				conn2.ExecuteQuery();
				int jml2 = conn2.GetRowCount();
				
				if (jml1 > 0 || jml2 > 0) 
				*/
				if (jml1 > 0) 
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		
			conn2.QueryString = "exec PARAM_GENERAL_CC_RFPROCEDURE_MAKER   '"+this.LBL_SAVEMODE.Text +"'," +
				this.DDL_APPTYPE.SelectedValue + ",'" + this.DDL_TRACK_FROM.SelectedValue + "','" +
				this.DDL_TRACK_NEXT.SelectedValue +"','" + this.DDL_TRACK_BACK.SelectedValue + "','" +
				this.DDL_TRACK_FAIL.SelectedValue + "'";
			conn2.ExecuteQuery();
			this.LBL_SAVEMODE.Text = "1";
			ViewPendingData();
			ClearControls();
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearControls();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					try {this.DDL_APPTYPE.SelectedValue		= CleansText(e.Item.Cells[1].Text);} catch{}
					try {this.DDL_TRACK_FROM.SelectedValue	= CleansText(e.Item.Cells[2].Text);} catch{}
					try{this.DDL_TRACK_NEXT.SelectedValue	= CleansText(e.Item.Cells[4].Text);} catch{}
					try{this.DDL_TRACK_BACK.SelectedValue	= CleansText(e.Item.Cells[6].Text);} catch{}
					try{this.DDL_TRACK_FAIL.SelectedValue	= CleansText(e.Item.Cells[8].Text);} catch{}
					activateControlKey(false);
					break;

				case "delete":		
					conn2.QueryString = "exec PARAM_GENERAL_CC_RFPROCEDURE_MAKER  '2'," +
						CleansText(e.Item.Cells[1].Text) + ",'" + CleansText(e.Item.Cells[2].Text) + "','" +
						CleansText(e.Item.Cells[4].Text) + "','" + CleansText(e.Item.Cells[6].Text) + "','" + 
						CleansText(e.Item.Cells[8].Text) + "'";
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

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = CleansText(e.Item.Cells[10].Text);
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					try {this.DDL_APPTYPE.SelectedValue		= CleansText(e.Item.Cells[1].Text);} catch{}
					try {this.DDL_TRACK_FROM.SelectedValue	= CleansText(e.Item.Cells[2].Text);} catch{}
					try {this.DDL_TRACK_NEXT.SelectedValue	= CleansText(e.Item.Cells[4].Text);} catch{}
					try	{this.DDL_TRACK_BACK.SelectedValue	= CleansText(e.Item.Cells[6].Text);} catch{}
					try {this.DDL_TRACK_FAIL.SelectedValue	= CleansText(e.Item.Cells[8].Text);} catch{}
					activateControlKey(false);
					break;

				case "delete":
					conn2.QueryString = " delete from PENDING_CC_RFPROCEDURE where TRACK_SEQ ="+
						CleansText(e.Item.Cells[1].Text) + " and TRACK_FROM ='" + CleansText(e.Item.Cells[2].Text) + "'";
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

		protected void DDL_APPTYPE1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
		}
	}
}
