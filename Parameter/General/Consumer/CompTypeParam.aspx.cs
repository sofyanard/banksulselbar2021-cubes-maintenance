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
	/// Summary description for CompTypeParam.
	/// </summary>
	public partial class CompTypeParam : System.Web.UI.Page
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
			CodeSeq("0"); 
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData1()
		{
			conn.QueryString = "select CT_CODE, CT_DESC, CD_SIBS from RFCOMPTYPE where active='1' order by CT_CODE";
			conn.ExecuteQuery();

			if (conn.GetRowCount() != 0)
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				DGEXISTING.DataSource = dt;

				try
				{
					DGEXISTING.DataBind();
				}
				catch 
				{
					DGEXISTING.CurrentPageIndex = DGEXISTING.PageCount - 1;
					DGEXISTING.DataBind();
				}
			} 

			conn.ClearData();
		}

		private void BindData2()
		{
			conn.QueryString = "select CT_CODE, CT_DESC, CD_SIBS, CH_STA, "+
				"STATUS = case CH_STA when '1' then 'INSERT' " +
				"when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TRFCOMPTYPE";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGREQUEST.DataSource = dt;

			try
			{
				DGREQUEST.DataBind();
			}
			catch 
			{
				DGREQUEST.CurrentPageIndex = DGREQUEST.PageCount - 1;
				DGREQUEST.DataBind();
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
			//TXT_ID.Text = "";
			TXT_DESC.Text = "";
			TXT_CD_SIBS.Text = "";
			TXT_ID.Enabled = true;

			LBL_SAVEMODE.Text = "1";
		}

		private void CodeSeq(string mode)
		{
			string seq = "";
			int number = Int16.Parse(LBL_NB.Text);

			if(mode == "1")
			{
				number++;
			}

			LBL_NB.Text = number.ToString();

			conn.QueryString = "select max(convert(int,isnull(CT_CODE,0)))+ "+LBL_NB.Text+" as MAXSEQ from RFCOMPTYPE";
			conn.ExecuteQuery();

			seq = conn.GetFieldValue("MAXSEQ").Trim();  

			if(seq.Length == 1)
				seq = "000" + seq;
			else if(seq.Length == 2)
				seq = "00" + seq;
			else if(seq.Length == 3)
				seq = "0" + seq;

			TXT_ID.Text = seq;  

			conn.ClearData(); 
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
			this.DGEXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGEXISTING_ItemCommand);
			this.DGEXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGEXISTING_PageIndexChanged);
			this.DGREQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGREQUEST_ItemCommand);
			this.DGREQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGREQUEST_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;

			conn.QueryString = "SELECT CT_CODE FROM TRFCOMPTYPE WHERE CT_CODE = '"+TXT_ID.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "UPDATE TRFCOMPTYPE SET CT_DESC = "+GlobalTools.ConvertNull(TXT_DESC.Text)+", CD_SIBS = "+GlobalTools.ConvertNull(TXT_CD_SIBS.Text)+" "+
					"where CT_CODE = '"+TXT_ID.Text+"'";  					
				conn.ExecuteQuery();	

				ClearEditBoxes();

				CodeSeq("0");
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "INSERT INTO TRFCOMPTYPE(CT_CODE, CT_DESC, CD_SIBS, CH_STA)"+ 
					"VALUES('"+TXT_ID.Text+"', "+GlobalTools.ConvertNull(TXT_DESC.Text)+", "+GlobalTools.ConvertNull(TXT_CD_SIBS.Text)+", 2)";
				conn.ExecuteQuery();
 
				ClearEditBoxes();
	
				CodeSeq("0");
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1")) 
			{
				conn.QueryString = "INSERT INTO TRFCOMPTYPE(CT_CODE, CT_DESC, CD_SIBS, CH_STA)"+ 
					"VALUES('"+TXT_ID.Text+"', "+GlobalTools.ConvertNull(TXT_DESC.Text)+", "+GlobalTools.ConvertNull(TXT_CD_SIBS.Text)+", 1)";
				
				try
				{
					conn.ExecuteQuery();
					CodeSeq("1");
				}
				catch
				{
					GlobalTools.popMessage(this,"Cannot insert same code, request canceled!"); 
				}
				finally
				{
					ClearEditBoxes(); 
				}				
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40"); 
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		private void DGEXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGEXISTING.CurrentPageIndex = e.NewPageIndex;

			BindData1(); 
		}

		private void DGREQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGREQUEST.CurrentPageIndex = e.NewPageIndex;
  
			BindData2(); 
		}

		private void DGREQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM TRFCOMPTYPE WHERE CT_CODE = '"+code+"'";
					conn.ExecuteQuery();
					
					BindData2();
					break;
				case "edit":
					if(e.Item.Cells[3].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						TXT_ID.Enabled = false;
						TXT_ID.Text = e.Item.Cells[0].Text.Trim();
						
						TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
						TXT_CD_SIBS.Text = cleansText(e.Item.Cells[2].Text);
   
						LBL_SAVEMODE.Text = "2";		
					}
					break;
			}				
		}

		private void DGEXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code, desc, sbc; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[0].Text.Trim();
					desc = cleansText(e.Item.Cells[1].Text);
					sbc = cleansText(e.Item.Cells[2].Text);
  
					conn.QueryString = "SELECT * FROM TRFCOMPTYPE WHERE CT_CODE = '"+code+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "INSERT INTO TRFCOMPTYPE(CT_CODE, CT_DESC, CD_SIBS, CH_STA)"+ 
							"VALUES('"+code+"', "+GlobalTools.ConvertNull(desc)+", "+GlobalTools.ConvertNull(sbc)+", 3)";
						conn.ExecuteQuery();

						BindData2();
					}
					break;
				case "edit":
					TXT_ID.Enabled = false;

					TXT_ID.Text = e.Item.Cells[0].Text.Trim();
					TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_CD_SIBS.Text = cleansText(e.Item.Cells[2].Text);
   
					LBL_SAVEMODE.Text = "2";		
					break;
			}		
		}
	}
}
