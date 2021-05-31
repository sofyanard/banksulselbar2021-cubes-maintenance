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

namespace CuBES_Maintenance.Parameter.Scoring.Consumer
{
	/// <summary>
	/// Summary description for CUBESLimitScore.
	/// </summary>
	public partial class CUBESLimitScore : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton BTN_BACK;
		protected Connection conn2;
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon();
			if (!IsPostBack)
			{
				ViewExistingData();
				ViewPendingData();
			}
			DGR_EXISTING.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			DGR_REQUEST.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);
		}

		private void InitialCon()
		{
			conn2.QueryString = "select * from VW_GETCONN where moduleid = '40'";
			conn2.ExecuteQuery();
			string DB_NAMA			= conn2.GetFieldValue("DB_NAMA");
			string DB_IP			= conn2.GetFieldValue("DB_IP");
			string DB_LOGINID		= conn2.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD		= conn2.GetFieldValue("DB_LOGINPWD");
			conn = new Connection("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private string checkApost(string str)
		{
			return str.Replace("'", "''").Trim();
		}

		private void clearEditBoxes()
		{
			this.LBL_SEQ_NO.Text = "";
			this.TXT_LIMIT_FORMULA.Text = "";
			this.TXT_LIMIT_TABLE.Text = "";
			this.TXT_LINK.Text = "";
			this.TXT_MAX_SCORE.Text = "";
			this.TXT_MIN_SCORE.Text = "";

			this.LBL_SAVEMODE.Text = "1";
		}
				
		private void ViewExistingData()
		{
			conn.QueryString = "select * from VW_PARAM_LIMITSCORE_EXISTING";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_EXISTING.DataSource = data;
			
			try
			{
				this.DGR_EXISTING.DataBind();
			} 
			catch 
			{
				this.DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.PageCount - 1;
				this.DGR_EXISTING.DataBind();
			}
		}
		private void ViewPendingData()
		{
			conn.QueryString = "select * FROM VW_PARAM_LIMITSCORE_PENDING";
			conn.ExecuteQuery();
			
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_REQUEST.DataSource = data;
			
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

		private string checkComma(string str)
		{
			return str.Replace(",", ".").Trim();
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
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);

		}
		#endregion

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (this.LBL_SAVEMODE.Text == "1") //bila insert data baru
			{
//				conn.QueryString= "SELECT * FROM MANDIRI_LIMIT_SCORE";
//				conn.ExecuteQuery();
//				int seq1 = conn.GetRowCount();
//				if (this.LBL_SEQ_NO.Text == "")
//				{
//					conn.QueryString = "select * from TMANDIRI_LIMIT_SCORE where CH_STA = '1'";
//					conn.ExecuteQuery();
//					int seq2 = conn.GetRowCount();
//					int seq = seq1 + seq2 + 1;
//					this.LBL_SEQ_NO.Text = seq.ToString();
//				}
				conn.QueryString = "select max(seq_no) + 1 from (select seq_no from MANDIRI_LIMIT_SCORE " +
					"union select seq_no from TMANDIRI_LIMIT_SCORE) unied ";
				conn.ExecuteQuery();
				this.LBL_SEQ_NO.Text = conn.GetFieldValue(0,0);
			} 
			int res = 1;
			if (res <= this.DGR_REQUEST.Items.Count)
			{
				res = this.DGR_REQUEST.Items.Count + 1;
			}
			this.LBL_SEQ_ID.Text = res.ToString();
							
			conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_LIMIT_SCORE_MAKER '"+ LBL_SAVEMODE.Text.Trim() + "', '"+
				this.LBL_SEQ_ID.Text.Trim() +"', "+ this.LBL_SEQ_NO.Text.Trim() +", '"+
				this.RBL_PRM.SelectedValue.ToString() +"', "+ checkComma(this.TXT_MIN_SCORE.Text) +", "+
				checkComma(this.TXT_MAX_SCORE.Text) +", '"+ checkApost(this.TXT_LIMIT_FORMULA.Text) +"', '"+ 
				this.TXT_LIMIT_TABLE.Text.Trim() +"', '"+ checkApost(this.TXT_LINK.Text) +"', '"+ 
				this.RBL_LIMIT_ACTIVE.SelectedValue.ToString() + "'";
			try
			{
				conn.ExecuteNonQuery();
			} 
			catch 
			{
				GlobalTools.popMessage(this, "The input data is not valid!");
			}
			ViewPendingData();
			clearEditBoxes();
			this.LBL_SAVEMODE.Text = "1";
		
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = "0"; //update
					this.LBL_SEQ_NO.Text = cleansText(e.Item.Cells[0].Text);
					try
					{
						this.RBL_PRM.SelectedValue = cleansText(e.Item.Cells[1].Text);
					}
					catch{}
					this.TXT_MAX_SCORE.Text = cleansText(e.Item.Cells[3].Text);
					this.TXT_MIN_SCORE.Text = cleansText(e.Item.Cells[2].Text);
					this.TXT_LIMIT_FORMULA.Text = cleansText(e.Item.Cells[4].Text);
					this.TXT_LIMIT_TABLE.Text = cleansText(e.Item.Cells[5].Text);
					this.TXT_LINK.Text = cleansText(e.Item.Cells[6].Text);
				
					try
					{
						this.RBL_LIMIT_ACTIVE.SelectedValue = cleansText(e.Item.Cells[7].Text);
					}
					catch{}
					break;
				case "delete": 
					int res = 1;
					if (res <= this.DGR_REQUEST.Items.Count)
					{
						res = this.DGR_REQUEST.Items.Count + 1;
					}
					this.LBL_SEQ_ID.Text = res.ToString();
					
					conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_LIMIT_SCORE_MAKER '2', '" + 
						this.LBL_SEQ_ID.Text.Trim() +"', "+ cleansText(e.Item.Cells[0].Text) +", '"+
						cleansText(e.Item.Cells[1].Text) +"', "+ GlobalTools.ConvertFloat(cleansText(e.Item.Cells[2].Text)) +", "+
						GlobalTools.ConvertFloat(cleansText(e.Item.Cells[3].Text)) +", '"+ cleansText(e.Item.Cells[4].Text) +"', '"+ 
						cleansText(e.Item.Cells[5].Text) +"', '"+ cleansText(e.Item.Cells[6].Text) +"', '"+ 
						cleansText(e.Item.Cells[7].Text) +"'";
					try
					{
						conn.ExecuteNonQuery();
					} 
					catch {GlobalTools.popMessage(this,"Error on stored procedure!");}
					ViewPendingData();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[9].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
							
					this.LBL_SEQ_NO.Text = cleansText(e.Item.Cells[0].Text);
					
					try
					{
						this.RBL_PRM.SelectedValue = cleansText(e.Item.Cells[1].Text);
					}
					catch{}
					
					this.TXT_MAX_SCORE.Text = cleansText(e.Item.Cells[3].Text);
					this.TXT_MIN_SCORE.Text = cleansText(e.Item.Cells[2].Text);
					this.TXT_LIMIT_FORMULA.Text = cleansText(e.Item.Cells[4].Text);
					this.TXT_LIMIT_TABLE.Text = cleansText(e.Item.Cells[5].Text);
					this.TXT_LINK.Text = cleansText(e.Item.Cells[6].Text);
				
					try
					{
						this.RBL_LIMIT_ACTIVE.SelectedValue = cleansText(e.Item.Cells[7].Text);
					}
					catch{}
					break;
				case "delete":
					string Seq_No = cleansText(e.Item.Cells[0].Text);
					string Seq_Id = cleansText(e.Item.Cells[11].Text);

					conn.QueryString = "DELETE FROM TMANDIRI_LIMIT_SCORE WHERE SEQ_ID = '"+ Seq_Id+ "' " +
						"and SEQ_NO = '" + Seq_No + "'" ;
					conn.ExecuteQuery();
					
					ViewPendingData();
					break;
				default:
					// do nothing
					break;
			}
		}
	}
}
