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
	/// Summary description for CompanyType.
	/// </summary>
	public partial class CompanyType : System.Web.UI.Page
	{
		protected Connection conn,conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection ((string) Session["ConnString"]);
			SetDBConn2();
			//conn = new Connection("Data Source=10.123.13.18;Initial Catalog=LOSSME-OLD;uid=sa;pwd=dmscorp;Pooling=true");
			if (!IsPostBack)
			{
				ViewExistingData();
			}
			ViewPendingData();
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");
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
			conn.QueryString = "select * from VW_GETCONN where moduleID = '20'";
			conn.ExecuteQuery();
			conn2 = new Connection("Data Source=" + conn.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn.GetFieldValue("DB_NAMA") + ";uid=" +conn.GetFieldValue("DB_LOGINID")+ ";pwd=" + conn.GetFieldValue("DB_LOGINPWD") + ";Pooling=true");
		}

		private void ClearBoxes()
		{
			TXT_CP_CODE.Text	= "";
			TXT_CP_DESC.Text	= "";
			TXT_SBC.Text		= "";
			ActivateControlKey(true);
		}

		private void ViewExistingData()
		{
			conn2.QueryString = "select * from VW_PARAM_CC_RFCOMPANY";
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
			conn2.QueryString = "select * from VW_PENDING_CC_RFCOMPANY";
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
			for (int i=0;i<DGR_REQUEST.Items.Count; i++)
			{
				DGR_REQUEST.Items[i].Cells[4].Text = getPendingStatus(DGR_REQUEST.Items[i].Cells[4].Text);
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearBoxes();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn2.QueryString = "select * from VW_PARAM_CC_RFCOMPANY where CP_CODE ='"+ TXT_CP_CODE.Text.Trim() + "'";
				conn2.ExecuteQuery();
				int jml = conn2.GetRowCount();
				if (jml > 0)
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}	
			conn2.QueryString = "exec PARAM_CC_RFCOMPANY_MAKER  '" + TXT_CP_CODE.Text.Trim() + "'," +
				GlobalTools.ConvertNull(TXT_CP_DESC.Text.Trim()) + "," + GlobalTools.ConvertNull(TXT_SBC.Text.Trim()) + ",'" +
				LBL_SAVEMODE.Text.Trim() + "'";
			conn2.ExecuteQuery();
			LBL_SAVEMODE.Text = "1";
			ViewPendingData();
			ClearBoxes();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../CommonParam.aspx?mc=99020201");
		}

		protected void RDB_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string str = RDB_MODULE.SelectedValue;
			if (str == "1" || str == "2")
				Response.Redirect("../Consumer/CompanyParam.aspx?mod="+ str); 
		}

		private string CleansText(string str)
		{
			if (str.Trim() == "&nbsp;")
				str = "";
			return str;
		}

		private void ActivateControlKey(bool sta)
		{
			TXT_CP_CODE.Enabled = sta;
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

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string str = ((LinkButton)e.CommandSource).CommandName.ToLower();
			switch (str)
			{
				case "edit":
					LBL_SAVEMODE.Text	= "0";
					TXT_CP_CODE.Text	= CleansText(e.Item.Cells[0].Text); 
					TXT_CP_DESC.Text	= CleansText(e.Item.Cells[1].Text);
					TXT_SBC.Text		= CleansText(e.Item.Cells[2].Text);
					ActivateControlKey(false);
					break;
				case "delete":
					string code = CleansText(e.Item.Cells[0].Text);
					string desc = CleansText(e.Item.Cells[1].Text);
					string sics = CleansText(e.Item.Cells[2].Text);
					conn2.QueryString = "exec PARAM_CC_RFCOMPANY_MAKER '" + code + "'," +
						GlobalTools.ConvertNull(desc) + "," + GlobalTools.ConvertNull(sics) +",'2'";
					conn2.ExecuteNonQuery();
					ViewPendingData();
					break;
				default:
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string str = ((LinkButton)e.CommandSource).CommandName.ToLower();
			switch (str)
			{
				case "edit":
					LBL_SAVEMODE.Text	= CleansText(e.Item.Cells[3].Text); 
					if (LBL_SAVEMODE.Text.Trim() == "2") return;
					TXT_CP_CODE.Text	= CleansText(e.Item.Cells[0].Text); 
					TXT_CP_DESC.Text	= CleansText(e.Item.Cells[1].Text);
					TXT_SBC.Text		= CleansText(e.Item.Cells[2].Text);
					ActivateControlKey(false);
					break;
				case "delete":
					string code = CleansText(e.Item.Cells[0].Text);
					conn2.QueryString = "delete from PENDING_CC_RFCOMPANY where CP_CODE = '" + code + "'";
					conn2.ExecuteNonQuery();
					ViewPendingData();
					break;
				default:
					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}
	}
}
