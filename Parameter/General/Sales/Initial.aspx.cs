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
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.Sales
{
	/// <summary>
	/// Summary description for Initial.
	/// </summary>
	public partial class Initial : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=SALESMANDIRI;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				ViewExistingData();
				ViewPendingData();
				ViewData();
			}
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
		}

		private void SetDBConn2()
		{
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			string DB_NAMA = conn.GetFieldValue("DB_NAMA");
			string DB_IP = conn.GetFieldValue("DB_IP");
			string DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncc = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
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

		private void ViewExistingData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_INITIAL";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_EXISTING.DataSource = dt;
			try
			{
				this.DGR_EXISTING.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.CurrentPageIndex-1;
					this.DGR_EXISTING.DataBind();
				}
				catch{}
			}
		}

		private void ViewPendingData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_INITIAL";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_REQUEST.DataSource = dt;
			try
			{
				this.DGR_REQUEST.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.CurrentPageIndex-1;
					this.DGR_REQUEST.DataBind();
				}
				catch{}
			}
		}

		private void ViewData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_INITIAL";
			conncc.ExecuteQuery();
			if(conncc.GetRowCount()!=0)
			{
				TXT_PHOTO_DIR.Text=conncc.GetFieldValue("IN_PHOTODIRECTORY");
				TXT_MAX_CHILD_TAX.Text=conncc.GetFieldValue("IN_MAXTANGGUNGAN");
				TXT_PPH.Text=conncc.GetFieldValue("IN_PPHPS21");
				LBL_IN_SEQ.Text=conncc.GetFieldValue("IN_SEQ");
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ViewData();
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex=e.NewPageIndex;
			ViewExistingData();
			ViewData();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex=e.NewPageIndex;
			ViewPendingData();
			ViewData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_PHOTO_DIR.Text=e.Item.Cells[1].Text;
					TXT_MAX_CHILD_TAX.Text=e.Item.Cells[2].Text;
					TXT_PPH.Text=e.Item.Cells[3].Text;
					LBL_IN_SEQ.Text=e.Item.Cells[0].Text;
					LBL_JENIS.Text="edit";
					break;
				
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_PHOTO_DIR.Text=e.Item.Cells[1].Text;
					TXT_MAX_CHILD_TAX.Text=e.Item.Cells[2].Text;
					TXT_PPH.Text=e.Item.Cells[3].Text;
					LBL_IN_SEQ.Text=e.Item.Cells[0].Text;
					LBL_JENIS.Text="edit";
					break;
				case "delete":
					//SMEDEV2
					string seq=e.Item.Cells[0].Text;
					conncc.QueryString="Delete from PENDING_SALESCOM_INITIAL where IN_SEQ='"+seq+"'"; 
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					ViewPendingData();
					break;
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conncc.QueryString="PARAM_SALESCOM_INITIAL_MAKER '0','"+ 
				LBL_IN_SEQ.Text +"','"+ TXT_PHOTO_DIR.Text +"','"+ TXT_MAX_CHILD_TAX.Text +"','"+
				TXT_PPH.Text+"','1'";
			conncc.ExecuteQuery();
			ViewData();
			ViewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../SalesParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

	}
}
