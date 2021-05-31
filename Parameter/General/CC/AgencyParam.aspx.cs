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
	/// Summary description for AgencyParam.
	/// </summary>
	public partial class AgencyParam : System.Web.UI.Page
	{
		protected Connection conn2,conn;
	
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

		private void ClearBoxes()
		{
			this.TXT_AGENCYID.Text		= "";
			this.TXT_AGENCYNAME.Text	= "";
			this.TXT_ADDRESS1.Text		= "";
			this.TXT_ADDRESS2.Text		= "";
			this.TXT_ADDRESS3.Text		= "";
			this.TXT_CITY.Text			= "";
			this.TXT_AGENCY_PUSAT.Text	= "";
			try 
			{
				this.DDL_AGENCYTYPE.SelectedValue	= "";}
			catch{}
			this.activateControlKey(false);
		}

		private void activateControlKey(bool isReadOnly) 
		{
			this.TXT_AGENCYID.ReadOnly = isReadOnly;
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
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

		private void ViewExistingData()
		{
			conn2.QueryString = "SELECT * FROM TAGENCY where ACTIVE ='1'";
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
			conn2.QueryString = "select * from PENDING_CC_TAGENCY";
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
				this.DGR_REQUEST.Items[i].Cells[9].Text = getPendingStatus(this.DGR_REQUEST.Items[i].Cells[9].Text);
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
				conn2.QueryString = "select * from TAGENCY where ACTIVE ='1' AND AGENCYID ='"+ this.TXT_AGENCYID.Text +"'";
				conn2.ExecuteQuery();
				int jml1 = conn2.GetRowCount();
				if (jml1 > 0) 
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					GlobalTools.SetFocus(this,this.TXT_AGENCYID);
					return;
				}
			}		

			conn2.QueryString = "exec PARAM_GENERAL_CC_TAGENCY_MAKER  '"+this.LBL_SAVEMODE.Text +"','" +
				this.TXT_AGENCYID.Text + "','" + this.TXT_AGENCYNAME.Text + "','" +
				this.TXT_ADDRESS1.Text + "','" + this.TXT_ADDRESS2.Text + "','" +
				this.TXT_ADDRESS3.Text + "','" + this.TXT_CITY.Text + "','" + 
				this.TXT_AGENCY_PUSAT.Text + "','" + this.DDL_AGENCYTYPE.SelectedValue + "'"; 
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
					this.TXT_AGENCYID.Text			= CleansText(e.Item.Cells[0].Text);
					this.TXT_AGENCYNAME.Text		= CleansText(e.Item.Cells[1].Text);
					this.TXT_ADDRESS1.Text			= CleansText(e.Item.Cells[2].Text);
					this.TXT_ADDRESS2.Text			= CleansText(e.Item.Cells[3].Text);
					this.TXT_ADDRESS3.Text			= CleansText(e.Item.Cells[4].Text);
					this.TXT_CITY.Text				= CleansText(e.Item.Cells[5].Text);
					this.TXT_AGENCY_PUSAT.Text		= CleansText(e.Item.Cells[6].Text);
					try
					{
						this.DDL_AGENCYTYPE.SelectedValue = CleansText(e.Item.Cells[7].Text);
					} 
					catch{}
					activateControlKey(true);
					break;
				case "delete":	
					conn2.QueryString = "exec PARAM_GENERAL_CC_TAGENCY_MAKER  '2','" +
						CleansText(e.Item.Cells[0].Text) + "','" + CleansText(e.Item.Cells[1].Text) + "','" +
						CleansText(e.Item.Cells[2].Text) + "','" + CleansText(e.Item.Cells[3].Text) + "','" +
						CleansText(e.Item.Cells[4].Text) + "','" + CleansText(e.Item.Cells[5].Text) + "','" + 
						CleansText(e.Item.Cells[6].Text) + "','" + CleansText(e.Item.Cells[7].Text) + "'"; 
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
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[8].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					this.TXT_AGENCYID.Text			= CleansText(e.Item.Cells[0].Text);
					this.TXT_AGENCYNAME.Text		= CleansText(e.Item.Cells[1].Text);
					this.TXT_ADDRESS1.Text			= CleansText(e.Item.Cells[2].Text);
					this.TXT_ADDRESS2.Text			= CleansText(e.Item.Cells[3].Text);
					this.TXT_ADDRESS3.Text			= CleansText(e.Item.Cells[4].Text);
					this.TXT_CITY.Text				= CleansText(e.Item.Cells[5].Text);
					this.TXT_AGENCY_PUSAT.Text		= CleansText(e.Item.Cells[6].Text);
					try
					{
						this.DDL_AGENCYTYPE.SelectedValue = CleansText(e.Item.Cells[7].Text);
					} 
					catch{}
					activateControlKey(true);
					break;

				case "delete":
					string id = e.Item.Cells[0].Text.Trim();
					conn2.QueryString = "delete from PENDING_CC_TAGENCY WHERE AGENCYID='" + id + "'";
					conn2.ExecuteQuery();
					ViewPendingData();
					break;
				default :
					break;
			}
		}
	}
}
