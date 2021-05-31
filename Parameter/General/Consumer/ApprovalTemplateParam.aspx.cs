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
	/// Summary description for ApprovalTemplateParam.
	/// </summary>
	public partial class ApprovalTemplateParam : System.Web.UI.Page
	{
		protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitialCon();
			if(!IsPostBack)
			{
				DDL_PROGRAM.Items.Add(new ListItem("--Select--",""));
				GlobalTools.fillRefList(DDL_PROGRAM,"select PR_CODE,PR_DESC from program",false,conn);
				//GlobalTools.fillRefList(DDL_PRODUCT,"select PRODUCTID,PRODUCTNAME from tproduct",false,conn);
				DDL_TEMPLATE.Items.Add(new ListItem("--Select--",""));
				GlobalTools.fillRefList(DDL_TEMPLATE,"select TEMPLATEID,TEMPLATEDESC from approval_template",false,conn);
				BindData1();
				BindData2();
			}
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");
		}

		private void InitialCon()
		{
			conn2.QueryString = "select * from vw_getconn where moduleid = '40'";
			conn2.ExecuteQuery();
			string DB_NAMA = conn2.GetFieldValue("DB_NAMA");
			string DB_IP = conn2.GetFieldValue("DB_IP");
			//string DB_LOGINID = conn2.GetFieldValue("DB_LOGINID");
			//string DB_LOGINPWD = conn2.GetFieldValue("DB_LOGINPWD");
			conn = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=sa;pwd=dmscorp;Pooling=true");
		}

		private void FillProduct(string prog)
		{
			DDL_PRODUCT.Items.Clear();
			DDL_PRODUCT.Items.Add(new ListItem("--Select--",""));
			string query = "select p.pr_code,p.productid,productname from programpro p "+
				"left join tproduct t on p.productid=t.productid "+
				"where pr_code='"+prog+"'";
			conn.QueryString = query ;
			conn.ExecuteQuery();
			for (int i=0; i< conn.GetRowCount(); i++)
			{
				DDL_PRODUCT.Items.Add(new ListItem(conn.GetFieldValue(i,2),conn.GetFieldValue(i,1)));
			}
		}

		private void BindData1()
		{
			conn.QueryString="select * from VW_PARAM_APPROVAL_TEMPLATE";
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
			conn.QueryString="select * from VW_PARAM_PENDING_APPROVAL_TEMPLATE";
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

		private void ClearEditBoxes()
		{
			DDL_PRODUCT.Enabled = true; 
			DDL_PROGRAM.Enabled = true;
			try
			{
				DDL_PROGRAM.SelectedValue="";
				DDL_PRODUCT.SelectedValue=""; 
				DDL_TEMPLATE.SelectedValue="";
			}
			catch{}
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
			string progid, prodid, templateid; 
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					LBL_SAVE.Text="3";
					progid = e.Item.Cells[4].Text.Trim();
					prodid = e.Item.Cells[5].Text.Trim();
					templateid = e.Item.Cells[6].Text.Trim(); 

					conn.QueryString = "select * from PENDING_APPROVAL_CONDITION where PR_CODE = '"+progid+"'"+
						"and PRODUCTID = '"+prodid+"' and TEMPLATEID='"+templateid+"'"; 
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						conn.QueryString="update PENDING_APPROVAL_CONDITION set PENDINGSTATUS ='3' where PR_CODE = '"+progid+"'"+
							"and PRODUCTID = '"+prodid+"' and TEMPLATEID='"+templateid+"'"; 
						conn.ExecuteQuery();
						BindData2();
					}
					else
					{
						try
						{
							conn.QueryString = "EXEC PARAM_GENERAL_APPROVAL_CONDITION '1','" +
								e.Item.Cells[4].Text.Trim()+ "', '" + e.Item.Cells[5].Text.Trim() + "','" + 
								e.Item.Cells[6].Text.Trim()+"', '3','',''  " ;
							conn.ExecuteQuery();
						}
						catch{ }
		
						BindData2();
					}
					break;

				case "edit":
					LBL_SAVE.Text="2";
					progid = e.Item.Cells[4].Text.Trim();
					FillProduct(progid);
					prodid = e.Item.Cells[5].Text.Trim();
					templateid = e.Item.Cells[6].Text.Trim(); 

					try
					{
						DDL_PROGRAM.SelectedValue = progid;
					}
					catch{ } 						

					try
					{
						DDL_PRODUCT.SelectedValue = prodid;  
					}
					catch{ } 	
					try
					{
						DDL_TEMPLATE.SelectedValue = templateid;  
					}
					catch{ } 	

					DDL_PROGRAM.Enabled = false;
					DDL_PRODUCT.Enabled = false; 
					break;
			}		
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string progid, prodid, templateid; 
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":				
					progid = e.Item.Cells[6].Text.Trim();
					prodid = e.Item.Cells[7].Text.Trim();
					templateid = e.Item.Cells[8].Text; 

					conn.QueryString = "EXEC PARAM_GENERAL_APPROVAL_CONDITION '2','" +
						e.Item.Cells[6].Text.Trim()+ "', '" + e.Item.Cells[7].Text.Trim() + "','" + 
						e.Item.Cells[8].Text.Trim()+"', '','',''  " ;
					conn.ExecuteQuery();

					BindData2();
					break;

				case "edit":
					progid = e.Item.Cells[6].Text.Trim();
					FillProduct(progid);
					prodid = e.Item.Cells[7].Text.Trim();
					templateid = e.Item.Cells[8].Text; 

					if(e.Item.Cells[5].Text.Trim() == "3")
					{
						LBL_SAVE.Text = "1";
					}
					else
					{
						LBL_SAVE.Text="2";

						try
						{
							DDL_PROGRAM.SelectedValue = progid;
						}
						catch{ } 						

						try
						{
							DDL_PRODUCT.SelectedValue = prodid;  
						}
						catch{ } 	
						try
						{
							DDL_TEMPLATE.SelectedValue = templateid;  
						}
						catch{ } 

						DDL_PROGRAM.Enabled = false;
						DDL_PRODUCT.Enabled = false; 
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

			conn.QueryString = "select * from PENDING_APPROVAL_CONDITION where PR_CODE = '"+DDL_PROGRAM.SelectedValue+"'"+
				"and PRODUCTID = '"+DDL_PRODUCT.SelectedValue+"' and TEMPLATEID='"+DDL_TEMPLATE.SelectedValue+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVE.Text != "1"))//update
			{
				conn.QueryString = "EXEC PARAM_GENERAL_APPROVAL_CONDITION '6','" +
					DDL_PROGRAM.SelectedValue+ "', '" + DDL_PRODUCT.SelectedValue + "','" + 
					DDL_TEMPLATE.SelectedValue+"', '2','',''  " ;
				conn.ExecuteQuery();

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "2"))//insert, stlh edit disave
			{
				conn.QueryString = "EXEC PARAM_GENERAL_APPROVAL_CONDITION '1','" +
					DDL_PROGRAM.SelectedValue+ "', '" + DDL_PRODUCT.SelectedValue + "','" + 
					DDL_TEMPLATE.SelectedValue+"', '2','',''  " ;
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "1"))//insert baru
			{
				conn.QueryString = "EXEC PARAM_GENERAL_APPROVAL_CONDITION '1','" +
					DDL_PROGRAM.SelectedValue+ "', '" + DDL_PRODUCT.SelectedValue + "','" + 
					DDL_TEMPLATE.SelectedValue+"', '1','',''  " ;
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

		protected void DDL_PROGRAM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillProduct(DDL_PROGRAM.SelectedValue);
		}

		

	}
}
