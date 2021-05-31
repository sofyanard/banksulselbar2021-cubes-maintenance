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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for CarSeriesParam.
	/// </summary>
	public partial class CarSeriesParam : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection (System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				FillDDL();
				ViewExistingData();
			}
			ViewPendingData();
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

		public void SetDBConn2()
		{
			conn2.QueryString = "select * from VW_GETCONN where MODULEID='40'";
			conn2.ExecuteQuery();

			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void FillDDL()
		{
			GlobalTools.fillRefList(DDL_MEREK,"select ID_MEREK, NM_MEREK from MEREK WHERE ACTIVE =1",conn);
			GlobalTools.fillRefList(DDL_MODEL,"select ID_MEREK, NM_MEREK from MEREK WHERE ACTIVE ='z'",conn);
		}

		private void FillDDLModel()
		{
			GlobalTools.fillRefList(DDL_MODEL,"select ID_MODEL, NM_MODEL from MODEL WHERE ACTIVE =1 and ID_MEREK ='" + this.DDL_MEREK.SelectedValue.Trim() + "'",conn);
		}
	
		private void ClearControls()
		{
			this.TXT_CODE.Text				= "";
			this.TXT_DESC.Text				= "";
			this.DDL_MEREK.SelectedValue	= "";
			this.DDL_MODEL.SelectedValue	= "";
			TXT_CODE.Enabled = true;
			DDL_MODEL.Enabled = true;
			DDL_MEREK.Enabled = true;
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
			conn.QueryString = "select * from VW_PARAM_CARSERIES_TIPE order by ID_TIPE,NM_MEREK,NM_MODEL,NM_TIPE";
			conn.ExecuteQuery();
			this.DGR_EXISTING.Visible = true;
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
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
			conn.QueryString = "select * from VW_PARAM_CARSERIES_TTIPE order by ID_TIPE,NM_MEREK,NM_MODEL,NM_TIPE";
			conn.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
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
			for (int i=0; i<this.DGR_REQUEST.Items.Count;i++)
			{
				this.DGR_REQUEST.Items[i].Cells[7].Text = getPendingStatus(this.DGR_REQUEST.Items[i].Cells[7].Text);
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

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "SELECT * FROM VW_PARAM_CARSERIES_TIPE where ID_TIPE ='" + 
					this.TXT_CODE.Text.Trim() +"' " +
					"and ID_MODEL = '" + this.DDL_MODEL.SelectedValue.Trim() + "' " ;
					//"and  NM_TIPE = '" + this.TXT_DESC.Text + "' ";
				conn.ExecuteQuery();
				int jml1 = conn.GetRowCount();
				if (jml1 > 0)
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}	
			conn.QueryString = "exec PARAM_GENERAL_TIPE_MAKER  '"+ this.LBL_SAVEMODE.Text +"','" +
				this.TXT_CODE.Text + "','" + this.DDL_MODEL.SelectedValue.Trim() + "','" +
				this.TXT_DESC.Text.Trim() + "' ";
			conn.ExecuteQuery();
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
					this.TXT_CODE.Text				= CleansText(e.Item.Cells[0].Text);
					this.DDL_MEREK.SelectedValue	= CleansText(e.Item.Cells[1].Text);
					FillDDLModel();
					this.DDL_MODEL.SelectedValue	= CleansText(e.Item.Cells[2].Text);
					this.TXT_DESC.Text				= CleansText(e.Item.Cells[5].Text);
					TXT_CODE.Enabled = false;
					DDL_MODEL.Enabled = false;
					DDL_MEREK.Enabled = false;
					break;

				case "delete":					
					conn.QueryString = "exec PARAM_GENERAL_TIPE_MAKER '2','" +
						CleansText(e.Item.Cells[0].Text) + "','" + CleansText(e.Item.Cells[2].Text) + "','" +
						CleansText(e.Item.Cells[5].Text)+"' ";
					conn.ExecuteQuery();
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
					LBL_SAVEMODE.Text = e.Item.Cells[6].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					this.TXT_CODE.Text				= CleansText(e.Item.Cells[0].Text.Trim());
					this.DDL_MEREK.SelectedValue	= CleansText(e.Item.Cells[1].Text.Trim());
					FillDDLModel();
					this.DDL_MODEL.SelectedValue	= CleansText(e.Item.Cells[2].Text.Trim());
					this.TXT_DESC.Text				= CleansText(e.Item.Cells[5].Text.Trim());
					TXT_CODE.Enabled = false;
					DDL_MODEL.Enabled = false;
					DDL_MEREK.Enabled = false;
					break;

				case "delete":
					string id_tipe				= CleansText(e.Item.Cells[0].Text);
					string id_model				= CleansText(e.Item.Cells[2].Text);
					string nm_tipe				= CleansText(e.Item.Cells[5].Text);
					conn.QueryString = "delete from TTIPE WHERE ID_TIPE='" + id_tipe + "' and " +
						"ID_MODEL = '" + id_model+ "' and NM_TIPE = '" + nm_tipe + "'";
					conn.ExecuteQuery();
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

		protected void DDL_MEREK_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			GlobalTools.fillRefList(DDL_MODEL,"select ID_MODEL, NM_MODEL from MODEL WHERE ACTIVE =1 and ID_MEREK ='" + this.DDL_MEREK.SelectedValue.Trim() + "'",conn);
		}
	}
}
