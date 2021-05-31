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
	/// Summary description for ProductBasedOnProgram.
	/// </summary>
	public partial class ProductBasedOnProgram : System.Web.UI.Page
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
			string arid = (string) Session["AreaId"];
			string que = "";

			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			LBL_SAVE.Text = "1"; 

			InitialCon(); 

			if(arid == "2000")
			{
				que = "select PR_CODE, PR_DESC from PROGRAM where AREA_ID = '"+arid+"'";
			}
			else
			{
				que = "select PR_CODE, PR_DESC from PROGRAM";
			}
			
			GlobalTools.fillRefList(DDL_PRODUCT,"select PRODUCTID, PRODUCTNAME from TPRODUCT where GROUP_ID = '1'",false,conn);
			GlobalTools.fillRefList(DDL_PROGRAM,que,true,conn);

			BindData1();
			BindData2();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData1()
		{
			conn.QueryString = "select t.PRODUCTID, t.PRODUCTNAME, p.NUMBER_PARAM, p.PR_CODE "+ 
				"from PROGRAMPRO p join TPRODUCT t on p.PRODUCTID = t.PRODUCTID "+
				"where p.active='1' and PR_CODE = '"+DDL_PROGRAM.SelectedValue+"'";
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

			conn.ClearData();
		}

		private void BindData2()
		{
			conn.QueryString = "select t.PRODUCTID, t.PRODUCTNAME, p.NUMBER_PARAM, p.CH_STA, p.PR_CODE, "+ 
					"STATUS = case p.CH_STA when '1' then 'INSERT' "+ 
					"when '2' then 'UPDATE' when '3' then 'DELETE' end "+ 
					"from TPROGRAMPRO p join TPRODUCT t on p.PRODUCTID = t.PRODUCTID"; 
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
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
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

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{		
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 	
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string pid, prcode; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					pid = e.Item.Cells[1].Text.Trim();
					prcode = e.Item.Cells[0].Text.Trim();
					

					conn.QueryString = "SELECT PR_CODE, PRODUCTID FROM TPROGRAMPRO WHERE PR_CODE = '"+prcode+"' AND PRODUCTID = '"+pid+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "insert into TPROGRAMPRO(PR_CODE,PRODUCTID,NUMBER_PARAM,CH_STA) "+
							"values('"+prcode+"','"+pid+"',NULL, 3)";
						conn.ExecuteQuery();
						
						BindData2();
					}
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code, pid; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					pid = e.Item.Cells[1].Text.Trim();
					code = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM TPROGRAMPRO WHERE PR_CODE = '"+code+"' AND PRODUCTID = '"+pid+"'";
					conn.ExecuteQuery();

					BindData2();
					break;
			}
		}

		protected void DDL_PROGRAM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1();
		}

		private void ClearEditBoxes()
		{
			DDL_PRODUCT.Enabled = true;
			DDL_PROGRAM.Enabled = true;
 
			LBL_SAVE.Text = "1";
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;

			conn.QueryString = "SELECT PR_CODE, PRODUCTID FROM TPROGRAMPRO WHERE PR_CODE = '"+DDL_PROGRAM.SelectedValue+"' AND PRODUCTID = '"+DDL_PRODUCT.SelectedValue+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVE.Text != "1"))
			{
				conn.QueryString = "UPDATE TPROGRAMPRO SET NUMBER_PARAM = NULL WHERE PR_CODE = '"+DDL_PROGRAM.SelectedValue+"' "+ 
								   "AND PRODUCTID = '"+DDL_PRODUCT.SelectedValue+"'";  					
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "2"))
			{
				conn.QueryString = "insert into TPROGRAMPRO(PR_CODE,PRODUCTID,NUMBER_PARAM,CH_STA) "+
					"values('"+DDL_PROGRAM.SelectedValue+"','"+DDL_PRODUCT.SelectedValue+"', NULL, 2)";
				conn.ExecuteQuery();
 
				ClearEditBoxes();
			}
			else if((hit == 0) && (LBL_SAVE.Text == "1"))
			{
				conn.QueryString = "insert into TPROGRAMPRO(PR_CODE,PRODUCTID,NUMBER_PARAM,CH_STA) "+
					"values('"+DDL_PROGRAM.SelectedValue+"','"+DDL_PRODUCT.SelectedValue+"', NULL, 1)";
				conn.ExecuteQuery();
 
				ClearEditBoxes();
			}
			else if((hit != 0) && (LBL_SAVE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
				return;
			}

			conn.ClearData();
 	
			BindData2();

			LBL_SAVE.Text = "1"; 
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		  Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleId=40"); 
		}
	}
}
