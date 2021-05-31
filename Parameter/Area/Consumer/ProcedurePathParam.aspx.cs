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

namespace CuBES_Maintenance.Parameter.Area.Consumer
{
	/// <summary>
	/// Summary description for ProcedurePathParam.
	/// </summary>
	public partial class ProcedurePathParam : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);

			if(!IsPostBack)
			{
				ViewData(); 
			}
			else
			{
				InitialCon();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");	
			
		}

		private void ViewData()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select db_ip, db_nama from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_SAVEMODE.Text = "1"; 

			InitialCon(); 
			
			GlobalTools.fillRefList(DDL_AREA,"select AREA_ID, AREA_NAME from AREA",conn);

			GlobalTools.fillRefList(DDL_CUR_TRACK, "select TR_CODE, TR_DESC from RFTRACKLST", false, conn);
			GlobalTools.fillRefList(DDL_FAILTRACK, "select TR_CODE, TR_DESC from RFTRACKLST", false, conn);
			GlobalTools.fillRefList(DDL_NEXT_SUCTRACT, "select TR_CODE, TR_DESC from RFTRACKLST", false, conn);

			BindData1(); 
			BindData2();
		}

		private void InitialCon()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID='" + mid + "'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void BindData1()
		{
			conn.QueryString = "select * from RFPROC where AREA_ID = '"+DDL_AREA.SelectedValue+"'";  
			
			conn.ExecuteQuery();

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

			for (int i = 0; i < DGR_EXISTING.Items.Count; i++)
				DGR_EXISTING.Items[i].Cells[0].Text = (i+1+(DGR_EXISTING.CurrentPageIndex*DGR_EXISTING.PageSize)).ToString();

			conn.ClearData();
		}

		private void BindData2()
		{
			conn.QueryString = "select *, STATUS = case CH_STA when '1' then 'INSERT' "+
				"when '2' then 'UPDATE' when '3' then 'DELETE' end "+
				"from TRFPROC where AREA_ID = '"+DDL_AREA.SelectedValue+"'";
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

			for (int j = 0; j < DGR_REQUEST.Items.Count; j++)
				DGR_REQUEST.Items[j].Cells[0].Text = (j+1+(DGR_REQUEST.CurrentPageIndex*DGR_REQUEST.PageSize)).ToString();
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ClearEditBoxes()
		{
			DDL_AREA.Enabled = true;
			
			DDL_CUR_TRACK.ClearSelection();
			DDL_FAILTRACK.ClearSelection();
			DDL_NEXT_SUCTRACT.ClearSelection(); 
 
			LBL_SAVEMODE.Text = "1";
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleID=40");
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
		
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
		
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
 
			BindData2();
		
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;

			BindData2(); 
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			 
		}

		protected void DDL_AREA_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1();
			BindData2(); 
		}
	}
}
