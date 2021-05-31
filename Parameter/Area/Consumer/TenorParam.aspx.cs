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
	/// Summary description for TenorParam.
	/// </summary>
	public partial class TenorParam : System.Web.UI.Page
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
			
			GlobalTools.fillRefList(DDL_PROGRAM_TYPE,que,true,conn);
			fillProduct();

			BindData1();
			BindData2();
		}

		private void fillProduct()
		{
			//GlobalTools.fillRefList(DDL_PRODUCT_TYPE,"select PRODUCTID, PRODUCTNAME from TPRODUCT where GROUP_ID = '1'",true,conn);
			string q = "select T.PRODUCTID, T.PRODUCTNAME from PROGRAMPRO PP JOIN TPRODUCT T ON T.PRODUCTID = PP.PRODUCTID " +
				"WHERE PP.PR_CODE = '" + DDL_PROGRAM_TYPE.SelectedValue + "'";
			GlobalTools.fillRefList(DDL_PRODUCT_TYPE, q, true, conn);
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData1()
		{
			conn.QueryString = "select PRODUCTID, TN_SEQ, PR_CODE, TN_CODE, TN_DESC, "+ 
				"isnull(ADMIN_FEE, 0) ADMIN_FEE from RFCAWTENOR "+ 
				"where active='1' and PR_CODE = '"+DDL_PROGRAM_TYPE.SelectedValue+"' "+
				"and PRODUCTID = '"+DDL_PRODUCT_TYPE.SelectedValue+"'";
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
			conn.QueryString = "select SEQ_ID, PRODUCTID, TN_SEQ, PR_CODE, TN_CODE, TN_DESC, "+ 
				"isnull(ADMIN_FEE, 0) ADMIN_FEE, CH_STA, "+ 
				"STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TRFCAWTENOR";
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

		private string cleansFloat(string tn)
		{
			if (tn.Trim() == "&nbsp;")
				tn = "0";
			return tn;
		}

		private void ClearEditBoxes()
		{
			TXT_CODE.Text = "";
			TXT_DESC.Text = "";
			TXT_ADM_FEE.Text = "";

			DDL_PRODUCT_TYPE.Enabled = true;
			DDL_PROGRAM_TYPE.Enabled = true;
			TXT_CODE.Enabled = true;
 
			LBL_SAVE.Text = "1";
		}

		private string getSeqId()
		{
			string mseq = "";

			conn.QueryString = "select isnull(max(convert(int,SEQ_ID)),0)+1 MAXSEQ from TRFCAWTENOR";
			conn.ExecuteQuery();
 
			mseq = conn.GetFieldValue("MAXSEQ");

			conn.ClearData(); 
 
			return mseq;
		}

		private string newtnseq(string pd, string pcode)
		{
			string seq;
			conn.QueryString = "select (select 1 + isnull(max(TN_SEQ),0) from RFCAWTENOR " +
				"WHERE PRODUCTID = '"+pd+"' AND PR_CODE = '"+pcode+"') a, " +
				"(select 1 + isnull(max(TN_SEQ),0) from TRFCAWTENOR " +
				"WHERE PRODUCTID = '"+pd+"' AND PR_CODE = '"+pcode+"') b";
			conn.ExecuteQuery();

			seq = conn.GetFieldValue("a").Trim();
			if (int.Parse(conn.GetFieldValue("a").Trim()) < int.Parse(conn.GetFieldValue("b").Trim()))
				seq = conn.GetFieldValue("b").Trim();

			return seq;
		}

		private string newtnseq_old(string pd, string pcode)
		{
			string tseq = "";
			int number = Int16.Parse(LBL_NB.Text);

			conn.QueryString = "select isnull(max(TN_SEQ),0)+ "+LBL_NB.Text+" as MAXSEQ from RFCAWTENOR WHERE "+
				"PRODUCTID = '"+pd+"' AND PR_CODE = '"+pcode+"'";
			conn.ExecuteQuery();
 
			if(conn.GetRowCount() != 0) 
				tseq = conn.GetFieldValue("MAXSEQ");
			else
				tseq = "0";

			conn.ClearData(); 

			number++;

			LBL_NB.Text = number.ToString();
 
			return tseq;
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
			string id = "", tnseq = ""; 
			int hit = 0;

			if(LBL_TNSEQ.Text == "")
				LBL_TNSEQ.Text = "0"; 

			conn.QueryString = "SELECT PR_CODE, PRODUCTID, TN_SEQ FROM TRFCAWTENOR "+ 
				"WHERE PRODUCTID = '"+DDL_PRODUCT_TYPE.SelectedValue+"' AND TN_SEQ = "+LBL_TNSEQ.Text+" "+
				"AND PR_CODE = '"+DDL_PROGRAM_TYPE.SelectedValue+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVE.Text != "1"))
			{
				conn.QueryString = "UPDATE TRFCAWTENOR SET TN_CODE = '"+TXT_CODE.Text+"', TN_DESC = '"+TXT_DESC.Text+"', "+
					"ADMIN_FEE = "+GlobalTools.ConvertFloat(TXT_ADM_FEE.Text)+
					"WHERE PRODUCTID = '"+DDL_PRODUCT_TYPE.SelectedValue+"' AND TN_SEQ = "+LBL_TNSEQ.Text+" "+
					"AND PR_CODE = '"+DDL_PROGRAM_TYPE.SelectedValue+"'";
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "2"))
			{
				id = getSeqId();

				conn.QueryString = "INSERT INTO TRFCAWTENOR VALUES('"+id+
					"', '"+DDL_PRODUCT_TYPE.SelectedValue+
					"', "+LBL_TNSEQ.Text+", '"+DDL_PROGRAM_TYPE.SelectedValue+
					"', '"+TXT_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_DESC.Text)+
					", "+GlobalTools.ConvertFloat(TXT_ADM_FEE.Text)+
					", 2, NULL, NULL, NULL)";
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "1"))
			{
				id = getSeqId();
				tnseq = newtnseq(DDL_PRODUCT_TYPE.SelectedValue,DDL_PROGRAM_TYPE.SelectedValue);

				conn.QueryString = "INSERT INTO TRFCAWTENOR VALUES('"+id+
					"', '"+DDL_PRODUCT_TYPE.SelectedValue+
					"', "+tnseq+", '"+DDL_PROGRAM_TYPE.SelectedValue+
					"', '"+TXT_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_DESC.Text)+
					", "+GlobalTools.ConvertFloat(TXT_ADM_FEE.Text)+
					", 1, NULL, NULL, NULL)";
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
			LBL_TNSEQ.Text = "";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
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
			string pid, prcode, seq, id = ""; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					pid = e.Item.Cells[0].Text.Trim();
					seq = e.Item.Cells[1].Text.Trim();
					prcode = e.Item.Cells[2].Text.Trim();

					conn.QueryString = "SELECT PR_CODE, PRODUCTID, TN_SEQ FROM TRFCAWTENOR "+ 
							"WHERE PRODUCTID = '"+pid+"' AND TN_SEQ = "+seq+" AND PR_CODE = '"+prcode+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						id = getSeqId();

						try
						{
							conn.QueryString = "INSERT INTO TRFCAWTENOR VALUES('"+id+"', '"+pid+"', "+seq+", '"+prcode+"', '"+e.Item.Cells[3].Text.Trim()+
								"', '"+cleansText(e.Item.Cells[4].Text)+"', "+GlobalTools.ConvertFloat(cleansFloat(e.Item.Cells[5].Text))+
								", 3, NULL, NULL, NULL)";

							conn.ExecuteQuery();
						}
						catch{ }

						BindData2();
					}
					break;

				case "edit":
					try
					{
						DDL_PROGRAM_TYPE.SelectedValue = e.Item.Cells[2].Text.Trim();   
					}
					catch{ }

					try
					{
						DDL_PRODUCT_TYPE.SelectedValue = e.Item.Cells[0].Text.Trim();   
					}
					catch{ }

					TXT_ADM_FEE.Text = cleansFloat(e.Item.Cells[5].Text);
					TXT_CODE.Text = cleansText(e.Item.Cells[3].Text);
					TXT_DESC.Text = cleansText(e.Item.Cells[4].Text);
    
					LBL_TNSEQ.Text = cleansText(e.Item.Cells[1].Text);    
					LBL_SAVE.Text = "2";
		
					DDL_PRODUCT_TYPE.Enabled = false;
					DDL_PROGRAM_TYPE.Enabled = false;
					TXT_CODE.Enabled = false;
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string id, pid; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					pid = e.Item.Cells[1].Text.Trim();
					id = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM TRFCAWTENOR WHERE SEQ_ID = '"+id+"' AND PRODUCTID = '"+pid+"'";
					conn.ExecuteQuery();

					BindData2();
					break;
				case "edit":
					if(e.Item.Cells[8].Text.Trim() == "3")
					{
						LBL_SAVE.Text = "1";
					}
					else
					{
						try
						{
							DDL_PROGRAM_TYPE.SelectedValue = e.Item.Cells[3].Text.Trim();   
						}
						catch{ }

						try
						{
							DDL_PRODUCT_TYPE.SelectedValue = e.Item.Cells[1].Text.Trim();   
						}
						catch{ }

						TXT_ADM_FEE.Text = cleansFloat(e.Item.Cells[6].Text);
						TXT_CODE.Text = cleansText(e.Item.Cells[4].Text);
						TXT_DESC.Text = cleansText(e.Item.Cells[5].Text);
    
						LBL_TNSEQ.Text = cleansText(e.Item.Cells[2].Text);    
						LBL_SAVE.Text = "2";

						DDL_PRODUCT_TYPE.Enabled = false;
						DDL_PROGRAM_TYPE.Enabled = false;
						TXT_CODE.Enabled = false;
					}
					break;
			}
		}

		protected void DDL_PROGRAM_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillProduct();
			BindData1(); 
		}

		protected void DDL_PRODUCT_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1(); 
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleID=40");
		}
	}
}
