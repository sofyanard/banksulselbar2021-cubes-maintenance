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
	/// Summary description for DTBOListParam.
	/// </summary>
	public partial class DTBOListParam : System.Web.UI.Page
	{
		protected Connection conn,conn2;
		protected Connection connsme = new Connection (System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection ((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				ViewExistingData();
			}
			ViewPendingData();
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			DGR_EXISTING.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_EXISTING_PageIndexChanged);
			DGR_REQUEST.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_REQUEST_PageIndexChanged);
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

		private void ClearBoxes()
		{
			TXT_CW_CODE.Text	= "";
			TXT_CW_DESC.Text	= "";
			ActivateControlKeys(true);
		}

		private void ActivateControlKeys(bool sta)
		{
			TXT_CW_CODE.Enabled = sta;
		}

		private string CleansText(string str)
		{
			if (str.Trim() == "&nbsp;")
				str = "";
			return str;
		}

		private string getPendingStatus(string str)
		{
			string ret = "";
			switch (str)
			{
				case "0":
					ret = "Update";
					break;
				case "1":
					ret = "Insert";
					break;
				case "2":
					ret = "Delete";
					break;
				default:
					break;
			}
			return ret;
		}

		private void ViewExistingData()
		{
			conn2.QueryString = "select * from VW_PARAM_CC_RFCAWLST";
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
			conn2.QueryString = "select * from VW_PENDING_CC_RFCAWLST";
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

			for (int i=0; i<DGR_REQUEST.Items.Count; i++)
			{
				DGR_REQUEST.Items[i].Cells[3].Text = getPendingStatus(CleansText(DGR_REQUEST.Items[i].Cells[3].Text));
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
				conn2.QueryString = "select * from VW_PARAM_CC_RFCAWLST where CW_CODE ='"+ 
					TXT_CW_CODE.Text.Trim() + "' and CW_GRP ='12'";
				conn2.ExecuteQuery();
				int jml1 = conn2.GetRowCount();
				if (jml1 > 0) 
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					GlobalTools.SetFocus(this,TXT_CW_CODE);
					return;
				}
			}		
		
			conn2.QueryString = "exec PARAM_CC_RFCAWLST_MAKER  '"+this.LBL_SAVEMODE.Text +"','" +
				TXT_CW_CODE.Text.Trim() + "'," + GlobalTools.ConvertNull(TXT_CW_DESC.Text.Trim());
			conn2.ExecuteNonQuery();
			LBL_SAVEMODE.Text = "1";
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
					TXT_CW_CODE.Text		= CleansText(e.Item.Cells[0].Text);
					TXT_CW_DESC.Text		= CleansText(e.Item.Cells[1].Text);
					ActivateControlKeys(false);
					break;
				case "delete":	
					string code = CleansText(e.Item.Cells[0].Text);
					string desc = CleansText(e.Item.Cells[1].Text);
					conn2.QueryString = "exec PARAM_CC_RFCAWLST_MAKER '2','" + 
						code + "'," + GlobalTools.ConvertNull(desc);
					conn2.ExecuteNonQuery();
					ViewPendingData();
					break;
				default :
					break;
			}
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[2].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_CW_CODE.Text		= CleansText(e.Item.Cells[0].Text);
					TXT_CW_DESC.Text		= CleansText(e.Item.Cells[1].Text);
					ActivateControlKeys(false);
					break;

				case "delete":
					string code = CleansText(e.Item.Cells[0].Text);
					conn2.QueryString = "delete from PENDING_CC_RFCAWLST WHERE CW_CODE='" + code + "' " +
						"and CW_GRP = '12'";
					conn2.ExecuteNonQuery();
					ViewPendingData();
					break;
				default :
					break;
			}
		}

	}
}
