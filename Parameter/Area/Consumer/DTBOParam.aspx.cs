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
	/// Summary description for DTBOParam.
	/// </summary>
	public partial class DTBOParam : System.Web.UI.Page
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

			LBL_SAVEMODE.Text = "1"; 

			InitialCon(); 

			if(arid == "2000")
			{
				que = "select PR_CODE, PR_DESC from PROGRAM where AREA_ID = '"+arid+"'";
			}
			else
			{
				que = "select PR_CODE, PR_DESC from PROGRAM";
			}
			
			GlobalTools.fillRefList(DDL_PROP_TYPE,que,true,conn);
			GlobalTools.fillRefList(DDL_PROD_TYPE,"select PRODUCTID, PRODUCTNAME from TPRODUCT where GROUP_ID = '1'",true,conn);
			GlobalTools.fillRefList(DDL_EMP_TYPE,"select JOB_TYPE_ID, DES from JOB_TYPE where ACTIVE = '1'",true,conn);   

			conn.QueryString = "select CW_CODE, CW_DESC from RFCAWLST where CW_GRP = '2'";
			conn.ExecuteQuery();

			for(int i=0; i < conn.GetRowCount(); i++) 
			{
				DDL_ITEM.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			}

			conn.ClearData(); 

			DDL_MANDATORY.Items.Add(new ListItem("Yes","0"));
			DDL_MANDATORY.Items.Add(new ListItem("No","1"));    
    
			BindData1();
			BindData2();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData1()
		{
			conn.QueryString = "select a.PRODUCTID, a.JOB_TYPE_ID, a.PR_CODE, a.CW_CODE, a.CW_MANDATORY, "+
					"CW_STATUS = case a.CW_MANDATORY when '0' then 'Yes' when '1' then 'No' end, "+
					"b.CW_DESC from RFCAWTBO a left join RFCAWLST b on a.CW_CODE = b.CW_CODE "+ 
					"where a.active='1' and a.PR_CODE = '"+DDL_PROP_TYPE.SelectedValue+"' and a.PRODUCTID = '"+DDL_PROD_TYPE.SelectedValue+"' "+
					"and a.JOB_TYPE_ID = '"+DDL_EMP_TYPE.SelectedValue+"'";
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
			conn.QueryString = "select a.PRODUCTID, a.JOB_TYPE_ID, a.PR_CODE, a.CW_CODE, a.CW_MANDATORY, "+
				"CW_STATUS = case a.CW_MANDATORY when '0' then 'Yes' when '1' then 'No' end, "+
				"b.CW_DESC, a.CH_STA, STATUS = case a.CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TRFCAWTBO a left join RFCAWLST b on a.CW_CODE = b.CW_CODE";
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

		private void ClearEditBoxes()
		{
			DDL_PROD_TYPE.Enabled = true;
			DDL_PROP_TYPE.Enabled = true;
			DDL_EMP_TYPE.Enabled = true; 

			DDL_ITEM.ClearSelection();
			DDL_MANDATORY.ClearSelection(); 
 
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

		protected void DDL_PROP_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1();  
		}

		protected void DDL_PROD_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1(); 
		}

		protected void DDL_EMP_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1(); 		
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string pid, prcode, jobid, cwcode; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					pid = e.Item.Cells[1].Text.Trim();
					jobid = e.Item.Cells[2].Text.Trim();
					prcode = e.Item.Cells[0].Text.Trim();
					cwcode = e.Item.Cells[7].Text.Trim();

					conn.QueryString = "SELECT * FROM TRFCAWTBO "+ 
						"WHERE PRODUCTID = '"+pid+"' AND JOB_TYPE_ID = '"+jobid+"' "+
						"AND PR_CODE = '"+prcode+"' AND CW_CODE = '"+cwcode+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						try
						{
							conn.QueryString = "INSERT INTO TRFCAWTBO VALUES('"+pid+"', '"+cleansText(e.Item.Cells[7].Text)+
								"','"+e.Item.Cells[2].Text.Trim()+"','"+prcode+"', 3, NULL, NULL, NULL, NULL, '"+cleansText(e.Item.Cells[6].Text)+"')";

							conn.ExecuteQuery();
						}
						catch{ }

						BindData2();
					}
				
					break;
				case "edit":
					try
					{
						DDL_PROD_TYPE.SelectedValue = e.Item.Cells[1].Text.Trim();   
					}
					catch{ }

					try
					{
						DDL_PROP_TYPE.SelectedValue = e.Item.Cells[0].Text.Trim();   
					}
					catch{ }

					try
					{
						DDL_EMP_TYPE.SelectedValue = e.Item.Cells[2].Text.Trim();   
					}
					catch{ }

					try
					{
						DDL_ITEM.SelectedValue = cleansText(e.Item.Cells[7].Text);   
					}
					catch{ }

					try
					{
						DDL_MANDATORY.SelectedValue = cleansText(e.Item.Cells[6].Text);   
					}
					catch{ }
					
					LBL_SAVEMODE.Text = "2";
		
					DDL_PROD_TYPE.Enabled = false;
					DDL_PROP_TYPE.Enabled = false;
					DDL_EMP_TYPE.Enabled = false; 
					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 	
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string id, code, job; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[0].Text.Trim();
					id = e.Item.Cells[1].Text.Trim();
					job = e.Item.Cells[2].Text.Trim();

					conn.QueryString = "DELETE FROM TRFCAWTBO WHERE PR_CODE = '"+code+"' AND PRODUCTID = '"+id+"' AND JOB_TYPE_ID = '"+job+"'";
					conn.ExecuteQuery();

					BindData2();
					break;
				case "edit":
					if(e.Item.Cells[6].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						try
						{
							DDL_PROD_TYPE.SelectedValue = e.Item.Cells[1].Text.Trim();   
						}
						catch{ }

						try
						{
							DDL_PROP_TYPE.SelectedValue = e.Item.Cells[0].Text.Trim();   
						}
						catch{ }

						try
						{
							DDL_EMP_TYPE.SelectedValue = e.Item.Cells[2].Text.Trim();   
						}
						catch{ }

						try
						{
							DDL_ITEM.SelectedValue = cleansText(e.Item.Cells[9].Text);   
						}
						catch{ }

						try
						{
							DDL_MANDATORY.SelectedValue = cleansText(e.Item.Cells[8].Text);   
						}
						catch{ }
					
						LBL_SAVEMODE.Text = "2";
		
						DDL_PROD_TYPE.Enabled = false;
						DDL_PROP_TYPE.Enabled = false;
						DDL_EMP_TYPE.Enabled = false; 
						
					}
					break;
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;

			conn.QueryString = "SELECT * FROM TRFCAWTBO "+ 
				"WHERE PRODUCTID = '"+DDL_PROD_TYPE.SelectedValue+"' "+ 
				"AND JOB_TYPE_ID = '"+DDL_EMP_TYPE.SelectedValue+"' "+
				"AND PR_CODE = '"+DDL_PROP_TYPE.SelectedValue+"' "+
				"AND CW_CODE = '"+DDL_ITEM.SelectedValue+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "UPDATE TRFCAWTBO SET CW_CODE = '"+DDL_ITEM.SelectedValue+"', CW_MANDATORY = '"+DDL_MANDATORY.SelectedValue+"' "+
					"WHERE PRODUCTID = '"+DDL_PROD_TYPE.SelectedValue+"' "+ 
					"AND JOB_TYPE_ID = '"+DDL_EMP_TYPE.SelectedValue+"' "+
					"AND PR_CODE = '"+DDL_PROP_TYPE.SelectedValue+"' "+
					"AND CW_CODE = '"+DDL_ITEM.SelectedValue+"'";
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "INSERT INTO TRFCAWTBO VALUES('"+DDL_PROD_TYPE.SelectedValue+"', '"+DDL_ITEM.SelectedValue+
					"','"+DDL_EMP_TYPE.SelectedValue+"','"+DDL_PROP_TYPE.SelectedValue+"', 2, NULL, NULL, NULL, NULL, '"+DDL_MANDATORY.SelectedValue+"')";
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				conn.QueryString = "INSERT INTO TRFCAWTBO VALUES('"+DDL_PROD_TYPE.SelectedValue+"', '"+DDL_ITEM.SelectedValue+
					"','"+DDL_EMP_TYPE.SelectedValue+"','"+DDL_PROP_TYPE.SelectedValue+"', 1, NULL, NULL, NULL, NULL, '"+DDL_MANDATORY.SelectedValue+"')";
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleID=40");
		}
	}
}
