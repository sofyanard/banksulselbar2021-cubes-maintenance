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
	/// Summary description for ApprovalCondParam.
	/// </summary>
	public partial class ApprovalCondParam : System.Web.UI.Page
	{
		protected Connection conn,conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon();
			if(!IsPostBack)
			{
				GlobalTools.fillRefList(DDL_PROGRAM,"select PR_CODE,PR_DESC from program",false,conn);
				GlobalTools.fillRefList(DDL_PRODUCT,"select PRODUCTID,PRODUCTNAME from tproduct",false,conn);
				BindData1();
				BindData2();
				TXT_CODE.ReadOnly = true;
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
			conn.QueryString="select a.*,PR_DESC, PRODUCTNAME from APPROVAL_CONDITION a "+
				"left join program b on a.PR_CODE=b.PR_CODE "+
				"left join tproduct c on a.PRODUCTID=c.PRODUCTID";
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
			conn.QueryString="select a.*,PR_DESC, PRODUCTNAME , "+  
				"STATUS = CASE a.PENDINGSTATUS WHEN '1' THEN 'INSERT' "+
				"WHEN '2' THEN 'UPDATE' WHEN '3' THEN 'DELETE' END "+
				"from PENDING_APPROVAL_CONDITION a "+
				"left join program b on a.PR_CODE=b.PR_CODE "+
				"left join tproduct c on a.PRODUCTID=c.PRODUCTID";
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
			TXT_CODE.Text = "";
			TXT_MAX.Text = "";
			TXT_MIN.Text = "";
			TXT_SCORE.Text = "";
			TXT_FIELD.Text = "";
			TXT_TABLE.Text = "";
			TXT_VALUE.Text = "";
		
			DDL_PRODUCT.Enabled = true; 
			DDL_PROGRAM.Enabled = true;
			TXT_CODE.Enabled = true;

			DDL_PROGRAM.ClearSelection();
			DDL_PRODUCT.ClearSelection(); 
 
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

		protected void DDL_PRODUCT_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string progid = DDL_PROGRAM.SelectedValue.ToString();
			string prodid = DDL_PRODUCT.SelectedValue.ToString();
			//buat condition code
			conn.QueryString="select CONDITION_CODE from APPROVAL_CONDITION where "+
				"PR_CODE='"+progid+"' and PRODUCTID='"+prodid+"'";
			conn.ExecuteQuery();
			if(conn.GetRowCount()!=0)
			{
				conn.QueryString="select max(CONDITION_CODE)+1 CONDITION_CODE from APPROVAL_CONDITION "+
					"where PR_CODE='"+progid+"' and PRODUCTID='"+prodid+"'";
				conn.ExecuteQuery();
				TXT_CODE.Text=conn.GetFieldValue("CONDITION_CODE");
			}
			else //belum ada code
			{
				if (progid.Length == 1)
				{
					progid = "0"+progid;
				}
				if (prodid.Length == 1)
				{
					prodid = "0"+prodid;
				}
				TXT_CODE.Text = progid.Trim()+prodid.Trim()+"01";
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
			string progid, prodid, code,min,max,score,field,table,valuenya; 
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					LBL_SAVE.Text="3";
					progid = e.Item.Cells[10].Text.Trim();
					prodid = e.Item.Cells[11].Text.Trim();
					code = e.Item.Cells[2].Text; 
					min = GlobalTools.ConvertFloat(e.Item.Cells[3].Text); 
					max = GlobalTools.ConvertFloat(e.Item.Cells[4].Text); 
					score = cleansText(e.Item.Cells[5].Text); 
					field = cleansText(e.Item.Cells[6].Text); 
					table = cleansText(e.Item.Cells[7].Text); 
					valuenya = cleansText(e.Item.Cells[8].Text); 

					conn.QueryString = "select * from PENDING_APPROVAL_CONDITION where PR_CODE = '"+progid+"'"+
						"and PRODUCTID = '"+prodid+"' and CONDITION_CODE='"+code+"'"; 
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						conn.QueryString="update PENDING_APPROVAL_CONDITION set PENDINGSTATUS ='3' where PR_CODE = '"+progid+"'"+
							"and PRODUCTID = '"+prodid+"' and CONDITION_CODE='"+code+"'"; 
						conn.ExecuteQuery();
						BindData2();
					}
					else
					{
						try
						{
							conn.QueryString = "insert into PENDING_APPROVAL_CONDITION "+
								"values ('"+progid+"','"+prodid+"','"+code+"','"+min+"','"+max+"','"+score+"','"+field+"','"+table+"','"+valuenya+"','"+LBL_SAVE.Text+"')";
							conn.ExecuteQuery();
						}
						catch{ }
		
						BindData2();
					}
					break;

				case "edit":
					LBL_SAVE.Text="2";
					progid = e.Item.Cells[10].Text.Trim();
					prodid = e.Item.Cells[11].Text.Trim();
					code = e.Item.Cells[2].Text; 

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

					TXT_CODE.Text = code;
					TXT_MAX.Text = GlobalTools.ConvertFloat(e.Item.Cells[4].Text);
					TXT_MIN.Text = GlobalTools.ConvertFloat(e.Item.Cells[3].Text);
					TXT_SCORE.Text = cleansText(e.Item.Cells[5].Text);
					TXT_FIELD.Text = cleansText(e.Item.Cells[6].Text); 
					TXT_TABLE.Text = cleansText(e.Item.Cells[7].Text); 
					TXT_VALUE.Text = cleansText(e.Item.Cells[8].Text); 

					DDL_PROGRAM.Enabled = false;
					DDL_PRODUCT.Enabled = false; 
					TXT_CODE.Enabled = false;
					
					break;
			}		
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string progid, prodid, code; 
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":				
					progid = e.Item.Cells[12].Text.Trim();
					prodid = e.Item.Cells[13].Text.Trim();
					code = e.Item.Cells[2].Text; 

					conn.QueryString = "delete from PENDING_APPROVAL_CONDITION where PR_CODE = '"+progid+"'"+
						"and PRODUCTID = '"+prodid+"' and CONDITION_CODE='"+code+"'"; 
					conn.ExecuteQuery();

					BindData2();
					break;

				case "edit":
					progid = e.Item.Cells[12].Text.Trim();
					prodid = e.Item.Cells[13].Text.Trim();
					code = e.Item.Cells[2].Text; 

					if(e.Item.Cells[11].Text.Trim() == "3")
					{
						LBL_SAVE.Text = "1";
					}
					else
					{
						LBL_SAVE.Text="2";
						progid = e.Item.Cells[12].Text.Trim();
						prodid = e.Item.Cells[13].Text.Trim();
						code = e.Item.Cells[2].Text; 

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

						TXT_CODE.Text = code;
						TXT_MAX.Text = GlobalTools.ConvertFloat(e.Item.Cells[4].Text);
						TXT_MIN.Text = GlobalTools.ConvertFloat(e.Item.Cells[3].Text);
						TXT_SCORE.Text = cleansText(e.Item.Cells[5].Text);
						TXT_FIELD.Text = cleansText(e.Item.Cells[6].Text); 
						TXT_TABLE.Text = cleansText(e.Item.Cells[7].Text); 
						TXT_VALUE.Text = cleansText(e.Item.Cells[8].Text); 

						DDL_PROGRAM.Enabled = false;
						DDL_PRODUCT.Enabled = false; 
						TXT_CODE.Enabled = false;
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
				"and PRODUCTID = '"+DDL_PRODUCT.SelectedValue+"' and CONDITION_CODE='"+TXT_CODE.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVE.Text != "1"))//update
			{
				conn.QueryString = "update PENDING_APPROVAL_CONDITION set CONDITION_MIN_LIMIT = "+GlobalTools.ConvertFloat(TXT_MIN.Text)+",PENDINGSTATUS ='2', "+
					"CONDITION_MAX_LIMIT="+GlobalTools.ConvertFloat(TXT_MAX.Text)+", CONDITION_SCORE_RESULT='"+TXT_SCORE.Text+"' ,"+
					"CONDITION_JENIS_FIELD='"+TXT_FIELD.Text+"', CONDITION_JENIS_TABLE='"+TXT_TABLE.Text+"', CONDITION_JENIS_value='"+TXT_VALUE.Text+"' "+
					"where PR_CODE = '"+DDL_PROGRAM.SelectedValue+"' "+
					"and PRODUCTID = '"+DDL_PRODUCT.SelectedValue+"' and CONDITION_CODE='"+TXT_CODE.Text+"'"; 
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "2"))//update
			{
				conn.QueryString = "insert into PENDING_APPROVAL_CONDITION "+
					"values ('"+DDL_PROGRAM.SelectedValue+"','"+DDL_PRODUCT.SelectedValue+"','"+TXT_CODE.Text+"','"+GlobalTools.ConvertFloat(TXT_MIN.Text)+"',"+
					"'"+GlobalTools.ConvertFloat(TXT_MAX.Text)+"','"+TXT_SCORE.Text+"','"+TXT_FIELD.Text+"','"+TXT_TABLE.Text+"','"+TXT_VALUE.Text+"','2')";
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "1"))//insert
			{
				conn.QueryString = "insert into PENDING_APPROVAL_CONDITION "+
					"values ('"+DDL_PROGRAM.SelectedValue+"','"+DDL_PRODUCT.SelectedValue+"','"+TXT_CODE.Text+"','"+GlobalTools.ConvertFloat(TXT_MIN.Text)+"',"+
					"'"+GlobalTools.ConvertFloat(TXT_MAX.Text)+"','"+TXT_SCORE.Text+"','"+TXT_FIELD.Text+"','"+TXT_TABLE.Text+"','"+TXT_VALUE.Text+"','1')";
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
