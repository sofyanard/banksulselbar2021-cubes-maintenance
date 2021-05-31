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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for RelationshipParam.
	/// </summary>
	public partial class RelationshipParam : System.Web.UI.Page
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

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");
			LBL_SAVEMODE.Text = "1"; 

			InitialCon();

			BindData1();
			BindData2();

		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData1()
		{
			conn.QueryString = "select * from RFRELEMG where ACTIVE = '1' order by RE_CODE";
			conn.ExecuteQuery();

			if (conn.GetRowCount() != 0)
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				DG1.DataSource = dt;

				try
				{
					DG1.DataBind();
				}
				catch 
				{
					DG1.CurrentPageIndex = DG1.PageCount - 1;
					DG1.DataBind();
				}	
			} 

			conn.ClearData();
 
		}

		private void BindData2()
		{
			conn.QueryString = "select RE_CODE, RE_DESC, CD_SIBS, CH_STA, "+
				"STATUS = case CH_STA when '1' then 'INSERT' " +
				"when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TRFRELEMG";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG2.DataSource = dt;

			try
			{
				DG2.DataBind();
			}
			catch 
			{
				DG2.CurrentPageIndex = DG2.PageCount - 1;
				DG2.DataBind();
			}
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ClearEditBoxes()
		{
			TXT_CODE.Text = "";
			TXT_DESC.Text = "";
			TXT_SIBS.Text = "";
			TXT_CODE.Enabled = true;

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
			this.DG1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG1_ItemCommand);
			this.DG1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG1_PageIndexChanged);
			this.DG2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG2_ItemCommand);
			this.DG2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG2_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;

			conn.QueryString = "SELECT RE_CODE FROM TRFRELEMG WHERE RE_CODE = '"+TXT_CODE.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "UPDATE TRFRELEMG SET RE_DESC = "+GlobalTools.ConvertNull(TXT_DESC.Text)+", CD_TMP = "+GlobalTools.ConvertNull(TXT_SIBS.Text)+
					" where RE_CODE = '"+TXT_CODE.Text+"'";  					
				conn.ExecuteQuery();	

				ClearEditBoxes();
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "INSERT INTO TRFRELEMG VALUES('"+TXT_CODE.Text+"',"+GlobalTools.ConvertNull(TXT_DESC.Text)+",NULL,"+GlobalTools.ConvertNull(TXT_SIBS.Text)+",'2',NULL)";
				conn.ExecuteQuery();
 
				ClearEditBoxes();	
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1")) 
			{
				conn.QueryString = "INSERT INTO TRFRELEMG VALUES('"+TXT_CODE.Text+"',"+GlobalTools.ConvertNull(TXT_DESC.Text)+",NULL,"+GlobalTools.ConvertNull(TXT_SIBS.Text)+",'1',NULL)";
				conn.ExecuteQuery();
 
				ClearEditBoxes();
			}
			else if((hit != 0) && (LBL_SAVEMODE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
				return;
			}
 
			conn.ClearData();
 	
			BindData2();

			LBL_SAVEMODE.Text = "1";
		
		}

		private void DG1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG1.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 
		}

		private void DG2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG2.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes();
		}


		private void DG1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code, desc, cds; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[0].Text.Trim();
					desc = cleansText(e.Item.Cells[1].Text);
					cds = e.Item.Cells[2].Text.Trim();
  
					conn.QueryString = "SELECT RE_CODE FROM TRFRELEMG WHERE RE_CODE = '"+code+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "INSERT INTO TRFRELEMG VALUES('"+code+"',"+GlobalTools.ConvertNull(desc)+",NULL,"+GlobalTools.ConvertNull(cds)+",'3',NULL)";
						conn.ExecuteQuery();
						BindData2();
					}
					break;
				case "edit":
					TXT_CODE.Enabled = false;
					TXT_CODE.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_SIBS.Text = cleansText(e.Item.Cells[2].Text);
   
					LBL_SAVEMODE.Text = "2";		
					break;
			}
		}

		private void DG2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM TRFRELEMG WHERE RE_CODE = '"+code+"'";
					conn.ExecuteQuery();
					BindData2();
					break;
				case "edit":
					if(e.Item.Cells[4].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						TXT_CODE.Enabled = false;
						TXT_CODE.Text = e.Item.Cells[0].Text;
						TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
						TXT_SIBS.Text = cleansText(e.Item.Cells[2].Text);
   
						LBL_SAVEMODE.Text = "2";		
					}
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40"); 
		}
	}
}
