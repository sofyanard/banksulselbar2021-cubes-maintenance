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
	/// Summary description for CUBESScoreNumber.
	/// </summary>
	public partial class CUBESScoreNumber : System.Web.UI.Page
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
				FillProduct();
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

		private string cleansFloat(string tn)
		{
			if (tn.Trim() == "&nbsp;")
				tn = "0";
			return tn;
		}

		private void clearEditBoxes()
		{
			this.TXT_MAX_SCORE.Text = "";
			this.TXT_MIN_SCORE.Text = "";
			this.TXT_POPULATION_VAL.Text = "";
			this.LBL_SEQ.Text = "";
			this.DDL_PRODUCT.SelectedValue = "";
			this.DDL_PRODUCT.Enabled = true;

			this.LBL_SAVEMODE.Text = "1";
		}

		public void ViewExistingData()
		{
			string qrystr = "select * from VW_SCORENUMBER_MAKER_EXISTING";
			if (DDL_PRODUCT.SelectedValue != "")
				qrystr = qrystr + " where PRODUCTID = '" + DDL_PRODUCT.SelectedValue + "'";
			conn.QueryString = qrystr;
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

		public void ViewPendingData()
		{
			string qrystr = "select * from VW_SCORENUMBER_MAKER_PENDING";
			if (DDL_PRODUCT.SelectedValue != "")
				qrystr = qrystr + " where PRODUCTID = '" + DDL_PRODUCT.SelectedValue + "'";
			conn.QueryString = qrystr;
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

		public void FillProduct()
		{
			DDL_PRODUCT.Items.Clear();
			DDL_PRODUCT.Items.Add(new ListItem("-- PILIH --",""));
			conn.QueryString = "select productname,productid from tproduct order by productname ";
			conn.ExecuteQuery();
			for (int i = 0; i < conn.GetRowCount(); i++)
			{
				DDL_PRODUCT.Items.Add(new ListItem(conn.GetFieldValue(i,0),conn.GetFieldValue(i,1)));
			}
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

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = "0"; //update
					this.TXT_MIN_SCORE.Text = cleansText(e.Item.Cells[1].Text);
					this.TXT_MAX_SCORE.Text = cleansText(e.Item.Cells[2].Text);
					this.TXT_POPULATION_VAL.Text = cleansText(e.Item.Cells[3].Text);
					this.DDL_PRODUCT.SelectedValue = cleansText(e.Item.Cells[4].Text);
					this.DDL_PRODUCT.Enabled = false;
					this.LBL_SEQ.Text = cleansText(e.Item.Cells[0].Text);
					break;
				case "delete": 
					int res = 1;
					if (res <= this.DGR_REQUEST.Items.Count)
					{
						res = this.DGR_REQUEST.Items.Count + 1;
					}
					this.LBL_SEQ.Text = res.ToString();
	
					conn.QueryString = "EXEC PARAM_SCORING_SCORE_NUMBER_MAKER '2', "+ 
						cleansText(e.Item.Cells[0].Text) +", "+ checkComma(cleansFloat(e.Item.Cells[1].Text)) +", "+
						checkComma(cleansFloat(e.Item.Cells[2].Text)) +", "+ cleansText(e.Item.Cells[3].Text) +
						", "+ cleansText(e.Item.Cells[4].Text);
					
					try
					{
						conn.ExecuteNonQuery();
					} 
					catch { GlobalTools.popMessage (this,"Error on stored procedure!");}
					
					ViewPendingData();

					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[3].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					this.LBL_SEQ.Text = cleansText(e.Item.Cells[0].Text);
					this.TXT_MIN_SCORE.Text = cleansText(e.Item.Cells[1].Text);
					this.TXT_MAX_SCORE.Text = cleansText(e.Item.Cells[2].Text);
					this.TXT_POPULATION_VAL.Text = cleansText(e.Item.Cells[3].Text);
					this.DDL_PRODUCT.SelectedValue = cleansText(e.Item.Cells[4].Text);
					this.DDL_PRODUCT.Enabled = false;

					break;
				case "delete":
					string Seq_No = cleansText(e.Item.Cells[0].Text);
					conn.QueryString = "DELETE FROM TSCORE_NUMBER WHERE SEQ = '"+ Seq_No+ "'";
					conn.ExecuteQuery();
					ViewPendingData();
					break;
				default:
					// do nothing
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			this.clearEditBoxes();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (this.LBL_SAVEMODE.Text == "1") //bila insert data baru
			{
				conn.QueryString= "SELECT * FROM SCORE_NUMBER";
				conn.ExecuteQuery();
				int seq1 = conn.GetRowCount();
				int seq,seq2;
				
				if (this.LBL_SEQ.Text == "")
				{
					conn.QueryString = "select * from TSCORE_NUMBER where CH_STA = '1'";
					conn.ExecuteQuery();
					seq2 = conn.GetRowCount();
					seq = seq1 + seq2 + 1;
					this.LBL_SEQ.Text = seq.ToString();
				} 
				
			} 
			conn.QueryString = "exec PARAM_SCORING_SCORE_NUMBER_MAKER '"+ LBL_SAVEMODE.Text.Trim() + "', "+
				this.LBL_SEQ.Text.Trim() +", "+ checkComma(this.TXT_MIN_SCORE.Text) +", "+ 
				checkComma(this.TXT_MAX_SCORE.Text) +", "+ this.TXT_POPULATION_VAL.Text.Trim()+
				", "+ this.DDL_PRODUCT.SelectedValue.Trim();

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
			LBL_SAVEMODE.Text = "1";
		}

		protected void DDL_PRODUCT_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
			ViewPendingData();
		}
	}
}
