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
	/// Summary description for ZipcodeParam.
	/// </summary>
	public partial class ZipcodeParam : System.Web.UI.Page
	{
		protected Connection conn2;
		protected Connection conn;
		protected string mid;
		protected string plus = "";

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
			conn2.QueryString = "select * from rfmodule where moduleid = '40'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			LBL_SAVEMODE.Text = "1"; 

			InitialCon();

			fillDocumentType("CITY","CITY_ID","CITY_NAME", DDL_CITY);
			fillDocumentType("AREA","AREA_ID","AREA_NAME", DDL_AREA);

			BindData1(false);
			BindData2();
			
		}

		private void fillDocumentType(string tableName, string columnId, string columnDesc, System.Web.UI.WebControls.DropDownList ddl) 
		{
			conn.QueryString = "select " + columnId + ", " + columnDesc + " from " + tableName;
			conn.ExecuteQuery();

			for(int i=0; i < conn.GetRowCount(); i++) 
			{
				ddl.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			}

			conn.ClearData(); 
		}

		private void BindData1(bool find)
		{
			if(find)
				plus = "where b.CITY_ID = '"+DDL_CITY.SelectedValue+"' and a.active='1' ";
			else
				plus = " where a.active='1' ";

			conn.QueryString = "select a.*, c.AREA_NAME, b.CITY_NAME from RFZIPCODECITY a "+
					"left join CITY b on a.CITY_ID = b.CITY_ID "+
					"left join AREA c on b.AREA_ID = c.AREA_ID "+plus+" order by a.ZC_ZIPCODE";
				    
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
			conn.QueryString = "select a.*, c.AREA_NAME, b.CITY_NAME, a.CH_STA, "+
				"STATUS = case a.CH_STA when '1' then 'INSERT' "+
				"when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TRFZIPCODECITY a "+
				"left join CITY b on a.CITY_ID = b.CITY_ID "+
				"left join AREA c on b.AREA_ID = c.AREA_ID order by a.ZC_ZIPCODE";
			
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

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void ClearEditBoxes()
		{
			TXT_ZIPCODE.Text = "";
			TXT_DESC.Text = "";
			DDL_AREA.ClearSelection();
			DDL_CITY.ClearSelection(); 

			TXT_ZIPCODE.Enabled = true;

			LBL_SAVEMODE.Text  = "1";
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
			this.DG1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG1_ItemCommand);
			this.DG1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG1_PageIndexChanged);
			this.DG2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG2_ItemCommand);
			this.DG2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG2_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;

			conn.QueryString = "SELECT ZC_ZIPCODE FROM TRFZIPCODECITY WHERE ZC_ZIPCODE = '"+TXT_ZIPCODE.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "UPDATE TRFZIPCODECITY SET ZC_DESC = "+GlobalTools.ConvertNull(TXT_DESC.Text)+", "+
						"AREA_ID = '"+DDL_AREA.SelectedValue+"', CITY_ID = '"+DDL_CITY.SelectedValue+"' "+
						"where ZC_ZIPCODE = '"+TXT_ZIPCODE.Text+"'";  					
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
				 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "INSERT INTO TRFZIPCODECITY VALUES('"+TXT_ZIPCODE.Text+"','"+DDL_CITY.SelectedValue+"','"+DDL_AREA.SelectedValue+"',"+GlobalTools.ConvertNull(TXT_DESC.Text)+",'2', NULL)";
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				conn.QueryString = "INSERT INTO TRFZIPCODECITY VALUES('"+TXT_ZIPCODE.Text+"','"+DDL_CITY.SelectedValue+"','"+DDL_AREA.SelectedValue+"',"+GlobalTools.ConvertNull(TXT_DESC.Text)+",'1', NULL)";
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

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		private void DG1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG1.CurrentPageIndex = e.NewPageIndex;	
			BindData1(true); 
		}

		private void DG2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG2.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 
		}

		protected void DDL_CITY_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1(true);
		}

		private void DG1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string zipcd, zcdesc, cid, arid; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					zipcd = e.Item.Cells[0].Text.Trim();
					zcdesc = cleansText(e.Item.Cells[3].Text);
					arid = e.Item.Cells[4].Text.Trim();
					cid = e.Item.Cells[5].Text.Trim();

					conn.QueryString = "SELECT ZC_ZIPCODE FROM TRFZIPCODECITY WHERE ZC_ZIPCODE = '"+zipcd+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "INSERT INTO TRFZIPCODECITY VALUES('"+zipcd+"','"+cid+"','"+arid+"',"+GlobalTools.ConvertNull(zcdesc)+",'3', NULL)";
						conn.ExecuteQuery();
						BindData2();
					}
					break;

				case "edit":
					TXT_ZIPCODE.Enabled = false;
					TXT_ZIPCODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_DESC.Text = cleansText(e.Item.Cells[3].Text);

					try
					{
						DDL_AREA.SelectedValue = e.Item.Cells[4].Text.Trim(); 
					}
					catch
					{ }

					try
					{
						DDL_CITY.SelectedValue = e.Item.Cells[5].Text.Trim(); 
					}
					catch
					{ }
					
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
					code = e.Item.Cells[0].Text;

					conn.QueryString = "DELETE FROM TRFZIPCODECITY WHERE ZC_ZIPCODE = '"+code+"'";
					conn.ExecuteQuery();
					BindData2();
					break;
				case "edit":
					if(e.Item.Cells[7].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1"; 
					}
					else
					{
						TXT_ZIPCODE.Enabled = false;
						TXT_ZIPCODE.Text = e.Item.Cells[0].Text.Trim();
						TXT_DESC.Text = cleansText(e.Item.Cells[3].Text);

						try
						{
							DDL_AREA.SelectedValue = e.Item.Cells[4].Text.Trim(); 
						}
						catch
						{ }

						try
						{
							DDL_CITY.SelectedValue = e.Item.Cells[5].Text.Trim(); 
						}
						catch
						{ }
				
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
