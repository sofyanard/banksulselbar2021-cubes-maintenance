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
using System.Configuration;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for ApprovalFormulaParam.
	/// </summary>
	public partial class ApprovalFormulaParam : System.Web.UI.Page
	{
		protected Connection conn,conn2;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon();
			if(!IsPostBack)
			{
				DDL_Code.Items.Add(new ListItem("--selesct--",""));
				conn.QueryString="select condition_code from approval_condition ";
				conn.ExecuteQuery();
				for (int i=0; i<conn.GetRowCount(); i++)
				{
					DDL_Code.Items.Add(new ListItem(conn.GetFieldValue(i,0),conn.GetFieldValue(i,0)));
				}
				BindData1();
				BindData2();
				txt_code.ReadOnly = true;
			}
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");
		}

		private void InitialCon()
		{
			conn2.QueryString = "select * from vw_getconn where moduleid = '"+Request.QueryString["ModuleId"]+"'";
			conn2.ExecuteQuery();
			string DB_NAMA = conn2.GetFieldValue("DB_NAMA");
			string DB_IP = conn2.GetFieldValue("DB_IP");
			string DB_LOGINID = conn2.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn2.GetFieldValue("DB_LOGINPWD");
			conn = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void BindData1()
		{
			conn.QueryString="select * from APPROVAL_FORMULA";
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
			conn.QueryString="select *, "+  
				"STATUS = CASE PENDINGSTATUS WHEN '1' THEN 'INSERT' "+
				"WHEN '2' THEN 'UPDATE' WHEN '3' THEN 'DELETE' END "+
				" from PENDING_APPROVAL_FORMULA ";
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

			conn.ClearData();
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ClearEditBoxes()
		{
			txt_code.Text = "";
			txta_routing.Value = "";
			txta_table.Value = "";
			DDL_Code.Enabled = true; 
			txt_code.Enabled = true;
			DDL_Code.ClearSelection();
 
			LBL_SAVE.Text = "1";
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

		protected void DDL_Code_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			conn.QueryString="select CONDITION_CODE,FORMULA_CODE from APPROVAL_FORMULA "+
				"where CONDITION_CODE='"+DDL_Code.SelectedValue+"'";
			conn.ExecuteQuery();
			if(conn.GetRowCount()!=0)
			{
				conn.QueryString="select max(FORMULA_CODE)+1 FORMULA_CODE from APPROVAL_FORMULA "+
					"where condition_code='"+DDL_Code.SelectedValue+"'";
				conn.ExecuteQuery();
				txt_code.Text=conn.GetFieldValue("FORMULA_CODE");
			}
			else if(conn.GetRowCount()==0)
			{
				txt_code.Text="1";
			}
		}

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
			string Ccode, Ftable, Fcode,Frouting; 
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					LBL_SAVE.Text="3";
					Ccode = e.Item.Cells[0].Text.Trim();
					Fcode = e.Item.Cells[1].Text.Trim();
					Ftable = cleansText(e.Item.Cells[2].Text); 
					Frouting = cleansText(e.Item.Cells[3].Text); 
					
					conn.QueryString = "select * from PENDING_APPROVAL_FORMULA where CONDITION_CODE = '"+Ccode+"'"+
						"and FORMULA_CODE = '"+Fcode+"'"; 
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						conn.QueryString="update PENDING_APPROVAL_FORMULA set PENDINGSTATUS ='3' where CONDITION_CODE = '"+Ccode+"'"+
							"and FORMULA_CODE = '"+Fcode+"'"; 
						conn.ExecuteQuery();
						BindData2();
					}
					else
					{
						try
						{
							conn.QueryString = "insert into PENDING_APPROVAL_FORMULA "+
								"values ('"+Ccode+"','"+Fcode+"','"+Ftable+"','"+Frouting+"','"+LBL_SAVE.Text+"')";
							conn.ExecuteQuery();
						}
						catch{ }
		
						BindData2();
					}
					break;

				case "edit":
					LBL_SAVE.Text="2";
					Ccode = e.Item.Cells[0].Text.Trim();
					Fcode = e.Item.Cells[1].Text.Trim();
					Ftable = cleansText(e.Item.Cells[2].Text); 
					Frouting = cleansText(e.Item.Cells[3].Text);  

					try
					{
						DDL_Code.SelectedValue = Ccode;
					}
					catch{ } 						

					txt_code.Text = Fcode;
					txta_routing.Value = Frouting;
					txta_table.Value = Ftable;

					DDL_Code.Enabled = false;
					txt_code.Enabled = false;
					
					break;
			}		
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string Ccode, Ftable, Fcode,Frouting;
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":				
					Ccode = e.Item.Cells[0].Text.Trim();
					Fcode = e.Item.Cells[1].Text.Trim();
					Ftable = cleansText(e.Item.Cells[2].Text); 
					Frouting = cleansText(e.Item.Cells[3].Text); 

					conn.QueryString = "delete from PENDING_APPROVAL_FORMULA where CONDITION_CODE = '"+Ccode+"'"+
						"and FORMULA_CODE = '"+Fcode+"'";  
					conn.ExecuteQuery();

					BindData2();
					break;

				case "edit":
					Ccode = e.Item.Cells[0].Text.Trim();
					Fcode = e.Item.Cells[1].Text.Trim();
					Ftable = cleansText(e.Item.Cells[2].Text); 
					Frouting = cleansText(e.Item.Cells[3].Text);  

					if(e.Item.Cells[6].Text.Trim() == "3")
					{
						LBL_SAVE.Text = "1";
					}
					else
					{
						LBL_SAVE.Text="2";

						try
						{
							DDL_Code.SelectedValue = Ccode;
						}
						catch{ } 						

						txt_code.Text = Fcode;
						txta_routing.Value = Frouting;
						txta_table.Value = Ftable;

						DDL_Code.Enabled = false; 
						txt_code.Enabled = false;
					}
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;

			conn.QueryString = "select * from PENDING_APPROVAL_FORMULA where CONDITION_CODE = '"+DDL_Code.SelectedValue+"'"+
				"and FORMULA_CODE = '"+txt_code.Text+"'";  
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVE.Text != "1"))//update
			{
				conn.QueryString = "update PENDING_APPROVAL_FORMULA set FORMULA_TABLE = '"+txta_table.Value+"',PENDINGSTATUS ='2', "+
					"FORMULA_ROUTING='"+txta_routing.Value+"' "+
					"where CONDITION_CODE = '"+DDL_Code.SelectedValue+"'"+
					"and FORMULA_CODE = '"+txt_code.Text+"'";   
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "2"))//update
			{
				conn.QueryString = "insert into PENDING_APPROVAL_FORMULA "+
					"values ('"+DDL_Code.SelectedValue+"','"+txt_code.Text+"','"+txta_table.Value+"','"+txta_routing.Value+"','2')";
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "1"))//insert
			{
				conn.QueryString = "insert into PENDING_APPROVAL_FORMULA "+
					"values ('"+DDL_Code.SelectedValue+"','"+txt_code.Text+"','"+txta_table.Value+"','"+txta_routing.Value+"','1')";
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit != 0) && (LBL_SAVE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data !");
				return;
			}

			conn.ClearData();
 	
			BindData2();

			LBL_SAVE.Text = "1"; 
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleId=40");
		}


	}
}
