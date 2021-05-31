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
	/// Summary description for ListBINParam.
	/// </summary>
	public partial class ListBINParam : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TXT_BANK;
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn2,conn;
		int maxint1, maxint2,max;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				GlobalTools.fillRefList(DDL_BN_CODE,"select * from VW_RFBANK order by BANKNAME",conn2);	
				ViewExistingData();
			}
			ViewPendingData();
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			this.DGR_REQUEST.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_REQUEST_PageIndexChanged);
			this.DGR_EXISTING1.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_EXISTING1_PageIndexChanged);
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
			this.DGR_EXISTING1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING1_ItemCommand);
			this.DGR_EXISTING1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING1_PageIndexChanged);
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
			this.DDL_BN_CODE.Enabled = isReadOnly;
		}

		private void ClearBoxes()
		{
			this.DDL_BN_CODE.SelectedValue	= "";
			this.TXT_BIN_NUMB.Text	= "";
			this.TXT_ICA.Text		= "";
			this.LBL_SEQ.Text		= "";
			this.activateControlKey(true);
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
			conn2.QueryString = "SELECT * FROM VW_PARAM_CCBIN_MAKER where ACTIVE ='1' Order BY BANKNAME,BINSEQ";
			conn2.ExecuteQuery();
			this.DGR_EXISTING1.Visible = true;
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			DGR_EXISTING1.DataSource = dt;
			try
			{
				DGR_EXISTING1.DataBind();
			}
			catch
			{
				DGR_EXISTING1.CurrentPageIndex = DGR_EXISTING1.PageCount - 1;
				DGR_EXISTING1.DataBind();
			}
		}

		private void ViewPendingData()
		{
			conn2.QueryString = "select * from PENDING_CC_CCBIN";
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
				this.DGR_REQUEST.Items[i].Cells[5].Text = getPendingStatus(DGR_REQUEST.Items[i].Cells[5].Text) ;
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
				conn2.QueryString = "select MAX(BINSEQ)as MAX1 from CCBIN where BN_CODE ='"+ this.DDL_BN_CODE.SelectedValue +"'";
				conn2.ExecuteQuery();
				string max1 = conn2.GetFieldValue("MAX1");
				conn2.QueryString = "select MAX(BINSEQ)as MAX2 from PENDING_CC_CCBIN where BN_CODE ='"+ this.DDL_BN_CODE.SelectedValue +"'";
				conn2.ExecuteQuery();
				string max2 = conn2.GetFieldValue("MAX2");
				if (max1 != "" )
					maxint1 = int.Parse(max1) + 1;
				if (max2!="")
					maxint2 = int.Parse(max2) + 1;
				if (max1 != "" && max2 != "")
				{
					if (maxint2>maxint1)
						max = maxint2;
					else
						max=maxint1;
				} 
				else if (max1 != "" && max2 == "")
				{
					max = maxint1;
				} 
				else if (max1 == "" && max2 != "")
				{
					max = maxint2;
				}
				else if (max1 == "" && max2 == "")
					max = 1;

				this.LBL_SEQ.Text = max.ToString();
			}
									
			conn2.QueryString = "exec PARAM_GENERAL_CC_CCBIN_MAKER '"+this.LBL_SAVEMODE.Text +"'," +
				this.LBL_SEQ.Text + ",'" + this.DDL_BN_CODE.SelectedValue + "','" + this.TXT_BIN_NUMB.Text + "','" +
				this.TXT_ICA.Text +"','" + this.DDL_BN_CODE.SelectedItem.Text + "'";
			conn2.ExecuteQuery();
			this.LBL_SAVEMODE.Text = "1";
			ViewPendingData();
			ClearBoxes();
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes();
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
					this.LBL_SEQ.Text					= CleansText(e.Item.Cells[0].Text);
					this.DDL_BN_CODE.SelectedValue		= CleansText(e.Item.Cells[1].Text);
					this.TXT_BIN_NUMB.Text				= CleansText(e.Item.Cells[3].Text);
					this.TXT_ICA.Text					= CleansText(e.Item.Cells[4].Text);
					activateControlKey(false);
					break;

				case "delete":
					string binseq = e.Item.Cells[0].Text;
					string bn_code = e.Item.Cells[1].Text;
					conn2.QueryString = "delete from PENDING_CC_CCBIN WHERE BN_CODE='" + bn_code + "'" +
						"and BINSEQ=" + binseq ;
					conn2.ExecuteQuery();
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

		private void DGR_EXISTING1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING1.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_EXISTING1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete1":					
					conn2.QueryString = "exec PARAM_GENERAL_CC_CCBIN_MAKER '2'," + CleansText(e.Item.Cells[0].Text) + ",'" +
						CleansText(e.Item.Cells[1].Text) + "','" + CleansText(e.Item.Cells[3].Text) + "','" +
						CleansText(e.Item.Cells[4].Text)+"','" + CleansText(e.Item.Cells[2].Text) + "'";
					conn2.ExecuteQuery();
					ViewPendingData();
					break;
				case "edit":
					LBL_SAVEMODE.Text = "0";
					this.LBL_SEQ.Text					= CleansText(e.Item.Cells[0].Text);
					this.DDL_BN_CODE.SelectedValue		= CleansText(e.Item.Cells[1].Text);
					this.TXT_BIN_NUMB.Text				= CleansText(e.Item.Cells[3].Text);
					this.TXT_ICA.Text					= CleansText(e.Item.Cells[4].Text);
					activateControlKey(false);
					break;
				default :
					break;
			}
		}
	}
}
