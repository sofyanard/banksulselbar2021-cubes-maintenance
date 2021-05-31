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
	/// Summary description for TBORefParam.
	/// </summary>
	public partial class TBORefParam : System.Web.UI.Page
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

			BindData1();
			BindData2();
			CodeSeq("0");			
		}

		private void BindData1()
		{
			/*20070716 change by sofyan, for Consumer Enhancement
			conn.QueryString = "select DOC_ID, TBO_DESC, CD_SIBS, FLAG = case SIBS_FLAG when '1' then 'Yes' else 'No' end "+
				"from RFTBO where active='1' order by DOC_ID";
			*/
			conn.QueryString = "exec PARAM_TBOREF_VIEWEXISTING";	    
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
			/*20070716 change by sofyan, for Consumer Enhancement
			conn.QueryString = "select DOC_ID, TBO_DESC, CD_SIBS, FLAG = case SIBS_FLAG when '1' then 'Yes' else 'No' end, "+
				"CH_STA, STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' when '3' then 'DELETE' end from TRFTBO";
			*/
			conn.QueryString = "exec PARAM_TBOREF_VIEWREQUEST";
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
			//TXT_CODE.Text = "";
			TXT_DESC.Text = "";
			TXT_SBC.Text = "";
			RDB_UPLOAD.ClearSelection();
			TXT_EXPDURATION.Text = "";
			
			TXT_CODE.Enabled = true;

			LBL_SAVEMODE.Text  = "1";
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
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

			conn.QueryString = "select max(convert(int,isnull(DOC_ID,0)))+ "+LBL_NB.Text+" as MAXSEQ from RFTBO";
			conn.ExecuteQuery();

			seq = conn.GetFieldValue("MAXSEQ").Trim();  

			if(seq.Length == 1)
				seq = "000" + seq;
			else if(seq.Length == 2)
				seq = "00" + seq;
			else if(seq.Length == 3)
				seq = "0" + seq;

			TXT_CODE.Text = seq;  

			conn.ClearData(); 
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;
			string sb_flag = "";

			if(TXT_CODE.Text.Length != 4)
			{
				GlobalTools.popMessage(this,"Document ID must 4 digits in length!");  
				return;
			}

			if(RDB_UPLOAD.SelectedValue == "1")
				sb_flag = "1";
			else
				sb_flag = "0";

			conn.QueryString = "SELECT * FROM TRFTBO WHERE DOC_ID = '"+TXT_CODE.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				/*20070716 change by sofyan, for Consumer Enhancement
				conn.QueryString = "UPDATE TRFTBO SET TBO_DESC = "+GlobalTools.ConvertNull(TXT_DESC.Text)+", "+
					"CD_SIBS = "+GlobalTools.ConvertNull(TXT_SBC.Text)+", SIBS_FLAG = '"+sb_flag+"' "+
					"where DOC_ID = '"+TXT_CODE.Text+"'";
				*/
				conn.QueryString = "exec PARAM_TBOREF_MAKERUPDATE 2, '" + 
					TXT_CODE.Text + "', " +
					GlobalTools.ConvertNull(TXT_DESC.Text) + ", '" +
					sb_flag + "', 1, " +
					GlobalTools.ConvertNull(TXT_SBC.Text) + ", " +
					TXT_EXPDURATION.Text;
				conn.ExecuteQuery();	

				ClearEditBoxes(); 

				CodeSeq("0");
				 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				/*20070716 change by sofyan, for Consumer Enhancement
				conn.QueryString = "INSERT INTO TRFTBO VALUES('"+TXT_CODE.Text+"',"+GlobalTools.ConvertNull(TXT_DESC.Text)+", '"+sb_flag+"', 2, "+GlobalTools.ConvertNull(TXT_SBC.Text)+")";
				*/
				conn.QueryString = "exec PARAM_TBOREF_MAKERUPDATE 1, '" + 
					TXT_CODE.Text + "', " +
					GlobalTools.ConvertNull(TXT_DESC.Text) + ", '" +
					sb_flag + "', 2, " +
					GlobalTools.ConvertNull(TXT_SBC.Text) + ", " +
					TXT_EXPDURATION.Text;
				conn.ExecuteQuery();
 
				ClearEditBoxes();
 
				CodeSeq("0");
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				/*20070716 change by sofyan, for Consumer Enhancement
				conn.QueryString = "INSERT INTO TRFTBO VALUES('"+TXT_CODE.Text+"',"+GlobalTools.ConvertNull(TXT_DESC.Text)+", '"+sb_flag+"', 1, "+GlobalTools.ConvertNull(TXT_SBC.Text)+")";
				*/
				conn.QueryString = "exec PARAM_TBOREF_MAKERUPDATE 1, '" + 
					TXT_CODE.Text + "', " +
					GlobalTools.ConvertNull(TXT_DESC.Text) + ", '" +
					sb_flag + "', 1, " +
					GlobalTools.ConvertNull(TXT_SBC.Text) + ", " +
					TXT_EXPDURATION.Text;
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
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleID=40"); 		
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		private void DG1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG1.CurrentPageIndex = e.NewPageIndex;

			BindData1(); 	
		}

		private void DG1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code, desc, flag, sbc, expdur; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[0].Text.Trim();
					desc = cleansText(e.Item.Cells[1].Text);
					sbc = cleansText(e.Item.Cells[2].Text);
					expdur = cleansText(e.Item.Cells[4].Text);
					
					if(e.Item.Cells[3].Text.Trim() == "Yes")
						flag = "1";
					else 
						flag = "0";
					
					conn.QueryString = "SELECT * FROM TRFTBO WHERE DOC_ID = '"+code+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						/*20070716 change by sofyan, for Consumer Enhancement
						conn.QueryString = "INSERT INTO TRFTBO VALUES('"+code+"',"+GlobalTools.ConvertNull(desc)+", '"+flag+"', 3, "+GlobalTools.ConvertNull(sbc)+")";
						*/
						conn.QueryString = "exec PARAM_TBOREF_MAKERUPDATE 1, '" + 
							code + "', " +
							GlobalTools.ConvertNull(desc) + ", '" +
							flag + "', 3, " +
							GlobalTools.ConvertNull(sbc) + ", " +
							expdur;
						conn.ExecuteQuery();
						BindData2();
					}
					break;

				case "edit":
					TXT_CODE.Enabled = false;
					TXT_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_SBC.Text = cleansText(e.Item.Cells[2].Text);
					TXT_EXPDURATION.Text = cleansText(e.Item.Cells[4].Text);
 
					if(e.Item.Cells[3].Text.Trim() == "Yes")
						RDB_UPLOAD.SelectedValue = "1";
					else 
						RDB_UPLOAD.SelectedValue = "0";
					
					LBL_SAVEMODE.Text = "2";		
					break;
			}		
		}

		private void DG2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG2.CurrentPageIndex = e.NewPageIndex;

			BindData2(); 					
		}

		private void DG2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM TRFTBO WHERE DOC_ID = '"+code+"'";
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
						TXT_CODE.Enabled = false;
						TXT_CODE.Text = e.Item.Cells[0].Text.Trim();
						TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
						TXT_SBC.Text = cleansText(e.Item.Cells[2].Text);
						TXT_EXPDURATION.Text = cleansText(e.Item.Cells[4].Text);
 
						if(e.Item.Cells[3].Text.Trim() == "Yes")
							RDB_UPLOAD.SelectedValue = "1";
						else 
							RDB_UPLOAD.SelectedValue = "0";						
				
						LBL_SAVEMODE.Text = "2";
					}
					break;
			}
		}

		protected void RDB_UPLOAD_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(RDB_UPLOAD.SelectedValue == "1")
				TXT_SBC.CssClass = "mandatory";
			else
				TXT_SBC.CssClass = "";		
		}
	}
}
