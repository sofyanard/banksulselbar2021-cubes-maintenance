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
	/// Summary description for InterestParam.
	/// </summary>
	public partial class InterestParam : System.Web.UI.Page
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

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
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
			
			GlobalTools.fillRefList(DDL_INTEREST_RATE, "select IN_TYPE, IN_DESC from RFINTTYPE",false,conn);
			GlobalTools.fillRefList(DDL_PROGRAM,que,true,conn);
    
			fillProduct();

			BindData2();
		}

		private void fillProduct()
		{
			//GlobalTools.fillRefList(DDL_PRODUCT,"select PRODUCTID, PRODUCTNAME from TPRODUCT where GROUP_ID = '1'",true,conn);
			string q = "select T.PRODUCTID, T.PRODUCTNAME from PROGRAMPRO PP JOIN TPRODUCT T ON T.PRODUCTID = PP.PRODUCTID " +
				"WHERE PP.PR_CODE = '" + DDL_PROGRAM.SelectedValue + "'";
			GlobalTools.fillRefList(DDL_PRODUCT, q, true, conn);

			fillTenor();
		}

		private void fillTenor()
		{
			DDL_TENOR.Items.Clear();
			DDL_TENOR.Items.Add(new ListItem("- Select -",""));
			DDL_TENOR.Items.Add(new ListItem("- All Tenor -","All"));
  
			conn.QueryString = "select TN_SEQ, TN_CODE from RFCAWTENOR "+ 
				"where PR_CODE = '"+DDL_PROGRAM.SelectedValue+"' and PRODUCTID = '"+DDL_PRODUCT.SelectedValue+"'";
			conn.ExecuteQuery();

			for(int i=0; i < conn.GetRowCount(); i++) 
			{
				DDL_TENOR.Items.Add(new ListItem(conn.GetFieldValue(i,1)+" Bulan", conn.GetFieldValue(i,0)));
				
			}

			conn.ClearData(); 

			LBL_TENOR.Text = "";

			BindData1();
		}

		private void BindData1()
		{	
			if(DDL_TENOR.SelectedValue.ToString().Equals("All"))
			{
				conn.QueryString = "select DISTINCT A.IN_TYPE, 'All' AS TN_DESC,'' AS TN_CODE, '0000' AS TN_SEQ, "+ 
					"A.PR_CODE, A.PRODUCTID,  A.IN_VALUE, B.IN_DESC "+
					"from INTEREST A join RFINTTYPE B on A.IN_TYPE = B.IN_TYPE "+
					"where A.Active='1' and A.PR_CODE = '"+DDL_PROGRAM.SelectedValue+"' "+
					"and A.PRODUCTID = '"+DDL_PRODUCT.SelectedItem.Text+"'";
			}
			else
				if(DDL_TENOR.SelectedValue.ToString().Equals(""))
			{
				conn.QueryString = "select A.TN_SEQ, A.PR_CODE, A.PRODUCTID, A.IN_VALUE, A.IN_TYPE, B.IN_DESC, TN.TN_CODE, TN.TN_DESC "+
					"from INTEREST A join RFINTTYPE B on A.IN_TYPE = B.IN_TYPE "+ 
					"join RFCAWTENOR TN on TN.PRODUCTID =  A.PRODUCTID and TN.PR_CODE = A.PR_CODE "+
					"and TN.TN_SEQ = A.TN_SEQ where A.Active='1' and A.PR_CODE = '"+DDL_PROGRAM.SelectedValue+"' "+
					"and A.PRODUCTID = '"+DDL_PRODUCT.SelectedValue+"' ";
			}
			else
			{
				conn.QueryString = "select A.TN_SEQ, A.PR_CODE, A.PRODUCTID, A.IN_VALUE, A.IN_TYPE, B.IN_DESC, TN.TN_CODE, TN.TN_DESC "+
					"from INTEREST A join RFINTTYPE B on A.IN_TYPE = B.IN_TYPE "+ 
					"join RFCAWTENOR TN on TN.PRODUCTID =  A.PRODUCTID and TN.PR_CODE = A.PR_CODE "+
					"and TN.TN_SEQ = A.TN_SEQ where A.Active='1' and A.PR_CODE = '"+DDL_PROGRAM.SelectedValue+"' "+
					"and A.PRODUCTID = '"+DDL_PRODUCT.SelectedValue+"' and A.TN_SEQ = "+DDL_TENOR.SelectedValue;
			}
			
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
			conn.QueryString = "select A.*, B.IN_DESC, TN.TN_CODE, TN.TN_DESC, "+
				"STATUS = case A.CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TINTEREST A join RFINTTYPE B on A.IN_TYPE = B.IN_TYPE "+ 
				"join RFCAWTENOR TN on TN.PRODUCTID =  A.PRODUCTID and TN.PR_CODE = A.PR_CODE "+
				"and TN.TN_SEQ = A.TN_SEQ";
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
			DDL_PRODUCT.Enabled = true;
			DDL_PROGRAM.Enabled = true;
			DDL_TENOR.Enabled = true;
			DDL_INTEREST_RATE.Enabled = true;
			TXT_VALUE.Text = "";
 
			LBL_SAVEMODE.Text = "1";
		}

		private string cleansFloat(string tn)
		{
			if (tn.Trim() == "&nbsp;")
				tn = "0";
			return tn;
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

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{ 
			int hit = 0;
			
			if((DDL_TENOR.SelectedValue == "All") && (LBL_SAVEMODE.Text == "1"))
			{
				try
				{
					conn.QueryString = "EXEC PARAM_AREA_INTEREST_MAKER_INSALL '"+DDL_PROGRAM.SelectedValue+"','"+DDL_PRODUCT.SelectedValue+"',"+
						GlobalTools.ConvertFloat(TXT_VALUE.Text)+",'"+DDL_INTEREST_RATE.SelectedValue+"'";
					BindData2();
				}
				catch
				{
					GlobalTools.popMessage(this,"Cannot insert all tenor for request!");
				}
				return;
			}
 
			conn.QueryString = "SELECT PR_CODE, PRODUCTID, TN_SEQ FROM TINTEREST "+ 
				"WHERE PRODUCTID = '"+DDL_PRODUCT.SelectedValue+"' AND TN_SEQ = '"+DDL_TENOR.SelectedValue+"' "+
				"AND PR_CODE = '"+DDL_PROGRAM.SelectedValue+"' and IN_TYPE= '"+DDL_INTEREST_RATE.SelectedValue+"'";

			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				if(DDL_TENOR.SelectedValue != "All")
				{
					conn.QueryString = "EXEC PARAM_AREA_INTEREST_MAKER '0','"+DDL_PROGRAM.SelectedValue+"','"+DDL_PRODUCT.SelectedValue+"','"+DDL_TENOR.SelectedValue+
						"',"+GlobalTools.ConvertFloat(TXT_VALUE.Text)+",'"+DDL_INTEREST_RATE.SelectedValue+"'";
				}
				else
				{
					conn.QueryString = "EXEC PARAM_AREA_INTEREST_MAKER '6','"+DDL_PROGRAM.SelectedValue+"','"+DDL_PRODUCT.SelectedValue+"','"+DDL_TENOR.SelectedValue+
						"',"+GlobalTools.ConvertFloat(TXT_VALUE.Text)+",'"+DDL_INTEREST_RATE.SelectedValue+"'";
				}
				
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				if(DDL_TENOR.SelectedValue != "All")
				{
					conn.QueryString = "EXEC PARAM_AREA_INTEREST_MAKER '2','"+DDL_PROGRAM.SelectedValue+"','"+DDL_PRODUCT.SelectedValue+"','"+DDL_TENOR.SelectedValue+
						"',"+GlobalTools.ConvertFloat(TXT_VALUE.Text)+",'"+DDL_INTEREST_RATE.SelectedValue+"'";
				}
				else
				{
					conn.QueryString = "EXEC PARAM_AREA_INTEREST_MAKER '3','"+DDL_PROGRAM.SelectedValue+"','"+DDL_PRODUCT.SelectedValue+"','"+DDL_TENOR.SelectedValue+
						"',"+GlobalTools.ConvertFloat(TXT_VALUE.Text)+",'"+DDL_INTEREST_RATE.SelectedValue+"'";
				}

				conn.ExecuteNonQuery(); 
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				conn.QueryString = "EXEC PARAM_AREA_INTEREST_MAKER '1','"+DDL_PROGRAM.SelectedValue+"','"+DDL_PRODUCT.SelectedValue+"','"+DDL_TENOR.SelectedValue+
						"',"+GlobalTools.ConvertFloat(TXT_VALUE.Text)+",'"+DDL_INTEREST_RATE.SelectedValue+"'";				

				conn.ExecuteNonQuery(); 

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

		protected void DDL_PROGRAM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillProduct(); 
		}

		protected void DDL_PRODUCT_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillTenor();
		}

		protected void DDL_TENOR_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1(); 

			if(DDL_TENOR.SelectedValue == "All")
				LBL_TENOR.Text = "All";
			else
				if(DDL_TENOR.SelectedValue == "")
					LBL_TENOR.Text = "";
				else
					LBL_TENOR.Text = DDL_TENOR.SelectedItem.Text;

		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 	
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleID=40");
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string pid, prcode, seq, inval, desc, intype; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					pid = e.Item.Cells[2].Text.Trim();
					seq = e.Item.Cells[0].Text.Trim();
					prcode = e.Item.Cells[1].Text.Trim();
					inval = cleansFloat(e.Item.Cells[4].Text);
					desc = e.Item.Cells[3].Text.Trim();
					intype = e.Item.Cells[7].Text.Trim();
    	
					conn.QueryString = "SELECT PR_CODE, PRODUCTID, TN_SEQ FROM TINTEREST "+ 
							"WHERE PRODUCTID = '"+pid+"' AND TN_SEQ = '"+seq+"' AND PR_CODE = '"+prcode+"' AND IN_TYPE='"+intype+"'";
					conn.ExecuteQuery();
					
					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						if(LBL_TENOR.Text != "All")
						{
							conn.QueryString = "EXEC PARAM_AREA_INTEREST_MAKER '5','"+prcode+"','"+pid+"','"+seq+
									"',"+GlobalTools.ConvertFloat(inval)+",'"+cleansText(e.Item.Cells[7].Text)+"'";
								
							conn.ExecuteNonQuery(); 
						}
						else
						{
							conn.QueryString = "EXEC PARAM_AREA_INTEREST_MAKER '4','"+prcode+"','"+pid+"','"+seq+
								"',"+GlobalTools.ConvertFloat(inval)+",'"+cleansText(e.Item.Cells[7].Text)+"'";
								
							conn.ExecuteNonQuery(); 		
						}

						BindData2();
					}
					break;

				case "edit":
					try
					{
						DDL_INTEREST_RATE.SelectedValue = e.Item.Cells[7].Text.Trim();   
					}
					catch{ }

					try
					{	//if(DDL_TENOR.SelectedValue == "")
							DDL_TENOR.SelectedValue = e.Item.Cells[0].Text.Trim();   
					}
					catch{ }

					TXT_VALUE.Text = cleansText(e.Item.Cells[4].Text);    

					LBL_TENOR.Text = e.Item.Cells[3].Text.Trim(); 

					LBL_SAVEMODE.Text = "2";
					
					DDL_PRODUCT.Enabled = false;
					DDL_PROGRAM.Enabled = false;
					DDL_TENOR.Enabled = false; 
					DDL_INTEREST_RATE.Enabled = false;
					
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string pid, prcode, seq, intype; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					pid = e.Item.Cells[2].Text.Trim();
					prcode = e.Item.Cells[1].Text.Trim();
					seq = e.Item.Cells[0].Text.Trim();
					intype = e.Item.Cells[9].Text.Trim();

					conn.QueryString = "DELETE FROM TINTEREST WHERE PRODUCTID = '"+pid+"' "+
							"AND PR_CODE = '"+prcode+"' AND TN_SEQ = '"+seq+"' AND IN_TYPE = '"+intype+"'";
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
						try
						{
							DDL_INTEREST_RATE.SelectedValue = e.Item.Cells[9].Text.Trim();   
						}
						catch{ }

						try
						{	
							DDL_TENOR.SelectedValue = e.Item.Cells[0].Text.Trim();   
						}
						catch{ }

						TXT_VALUE.Text = cleansText(e.Item.Cells[4].Text);    

						LBL_TENOR.Text = e.Item.Cells[3].Text.Trim(); 

						LBL_SAVEMODE.Text = "2";
					
						DDL_PRODUCT.Enabled = false;
						DDL_PROGRAM.Enabled = false;
						DDL_TENOR.Enabled = false;
						DDL_INTEREST_RATE.Enabled = false;
					}
					break;
			}		
		}
	}
}
