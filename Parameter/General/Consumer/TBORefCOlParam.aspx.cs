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
	/// Summary description for TBORefCOlParam.
	/// </summary>
	public partial class TBORefCOlParam : System.Web.UI.Page
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

			LBL_SAVE.Text = "1";
			LBL_SEQ.Text = "0";

			InitialCon(); 
	
			fillDocumentType("RFTBO","DOC_ID","TBO_DESC", DDL_DOC);
			fillDocumentType("RFCOLATERAL","RF_CODE","RF_DESC", DDL_COL_TYPE);
			fillFlag();

			BindData1();
			BindData2();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData1()
		{
			conn.QueryString = "select c.RF_DESC, case a.FLAG when '1' then 'BARU' ELSE 'LAMA' END as FLAG_STAT, "+
							   "b.TBO_DESC, a.FLAG, a.DOC_ID, a.DOC_SEQ, a.COL_TYPE from RFTBOCOLATERAL a, RFTBO b, RFCOLATERAL c "+
							   "where a.DOC_ID = b.DOC_ID and a.COL_TYPE = c.RF_CODE and a.active='1' order by a.DOC_SEQ ASC";
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0)
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
			conn.QueryString = "select COLTY = case a.COL_TYPE when 'C01' then 'Mobil' "+
						"when 'D01' then 'Deposito' when 'H01' then 'Tanah' "+
						"when 'H02' then 'Bangunan' when 'H03' then 'Tanah dan Bangunan' end, "+
						"FG = case a.FLAG when '1' then 'BARU' ELSE 'LAMA' END, "+
				        "STATUS = case a.CH_STA when '1' then 'INSERT' " +
				        "when '2' then 'UPDATE' "+
					    "when '3' then 'DELETE' end, a.DOC_ID, a.DOC_SEQ, a.COL_TYPE, a.FLAG, a.CH_STA, b.TBO_DESC "+
						"from TRFTBOCOLATERAL a join RFTBO b on a.DOC_ID = b.DOC_ID ORDER BY a.DOC_SEQ ASC";
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

		private void fillFlag()
		{
			DDL_FLAG.Items.Add(new  ListItem("Baru", "1"));
			DDL_FLAG.Items.Add(new  ListItem("Lama", "2"));
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

		private string seq()
		{
			string seqid = "";
			int number = Int16.Parse(LBL_NB.Text);

			conn.QueryString = "SELECT max(convert(int,DOC_SEQ))+ "+LBL_NB.Text+" as MAX from RFTBOCOLATERAL";
			
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0) 
				seqid = conn.GetFieldValue("MAX");
			else
				seqid = "0"; 

			conn.ClearData(); 

			number++;

			LBL_NB.Text = number.ToString();  
			return seqid;
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
			string id = "";

			conn.QueryString = "SELECT DOC_SEQ FROM TRFTBOCOLATERAL WHERE DOC_SEQ = "+LBL_SEQ.Text;
			conn.ExecuteQuery();

			hit = conn.GetRowCount(); 
 
			if((hit != 0) && (LBL_SAVE.Text != "1"))
			{
				conn.QueryString = "UPDATE TRFTBOCOLATERAL SET FLAG = '"+DDL_FLAG.SelectedValue+"', COL_TYPE = '"+DDL_COL_TYPE.SelectedValue+"', DOC_ID = '"+DDL_DOC.SelectedValue+"' "+
								   "where DOC_SEQ = "+LBL_SEQ.Text;
				conn.ExecuteQuery();

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "2"))
			{
				conn.QueryString = "INSERT INTO TRFTBOCOLATERAL VALUES("+LBL_SEQ.Text+",'"+DDL_COL_TYPE.SelectedValue+"','"+DDL_FLAG.SelectedValue+"','"+DDL_DOC.SelectedValue+"','2')";
				conn.ExecuteQuery();

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "1")) 
			{
				id = seq();

				conn.QueryString = "INSERT INTO TRFTBOCOLATERAL VALUES("+id+",'"+DDL_COL_TYPE.SelectedValue+"','"+DDL_FLAG.SelectedValue+"','"+DDL_DOC.SelectedValue+"','1')";
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

		private void DG1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG1.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 
		}

		private void DG1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string docseq, coltype, flag, docid; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					docseq = e.Item.Cells[0].Text.Trim();
					flag = e.Item.Cells[5].Text.Trim();
					docid = e.Item.Cells[6].Text.Trim();
					coltype = e.Item.Cells[7].Text.Trim();

					conn.QueryString = "SELECT DOC_SEQ FROM TRFTBOCOLATERAL WHERE DOC_SEQ = "+docseq;
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "INSERT INTO TRFTBOCOLATERAL VALUES("+docseq+",'"+coltype+"','"+flag+"','"+docid+"','3')";
						conn.ExecuteQuery();
						BindData2();
					}
					break;

				case "edit":
					docseq = e.Item.Cells[0].Text;
					flag = e.Item.Cells[5].Text;
					docid = e.Item.Cells[6].Text;
					coltype = e.Item.Cells[7].Text;

					try
					{
						DDL_COL_TYPE.SelectedValue =  coltype.Trim(); 
					}
					catch{ }

					try
					{
						DDL_FLAG.SelectedValue =  flag.Trim(); 
					}
					catch{ }

					try
					{
						DDL_DOC.SelectedValue =  docid.Trim(); 
					}
					catch{ }

					LBL_SAVE.Text = "2";
					
					LBL_SEQ.Text = docseq; 			
					break;
			}
		}

		private void ClearEditBoxes()
		{
			DDL_COL_TYPE.SelectedIndex = 0;
			DDL_DOC.SelectedIndex = 0;
			DDL_FLAG.SelectedIndex = 0;
 
			LBL_SAVE.Text  = "1"; 
			
			LBL_SEQ.Text = "0";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes();
		}

		private void DG2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string docseq, docid; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					docid = e.Item.Cells[3].Text.Trim();
					docseq = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM TRFTBOCOLATERAL where DOC_SEQ = "+docseq;
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
							DDL_COL_TYPE.SelectedValue =  e.Item.Cells[7].Text.Trim(); 
						}
						catch{ }

						try
						{
							DDL_FLAG.SelectedValue =  e.Item.Cells[6].Text.Trim(); 
						}
						catch{ }

						try
						{
							DDL_DOC.SelectedValue =  e.Item.Cells[9].Text.Trim(); 
						}
						catch{ }
						
						LBL_SEQ.Text = e.Item.Cells[0].Text.Trim();
						LBL_SAVE.Text = "2";		
					}
					break;
			}		
		}

		private void DG2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG2.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40"); 
		}
	}
}
