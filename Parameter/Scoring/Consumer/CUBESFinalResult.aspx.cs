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
	/// Summary description for CUBESFinalResult.
	/// </summary>
	public partial class CUBESFinalResult : System.Web.UI.Page
	{
		protected Connection conn2;
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon();
			if(!IsPostBack)
			{
				FillProductType();
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


		public void FillProductType()
		{
			DDL_PRODUCT_TYPE.Items.Add(new ListItem("- PILIH -", ""));
			
			conn.QueryString = "SELECT DISTINCT PRODUCTID,PRODUCTNAME FROM TPRODUCT ";
			conn.ExecuteQuery();
			
			for (int i=0; i <conn.GetRowCount(); i++)
			{
				this.DDL_PRODUCT_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
			}
		}
		
		private void clearEditBoxes()
		{
			try
			{
				DDL_RESULT_NAME.SelectedValue = "";
			} 
			catch {}

			TXT_MIN_RANGE.Text = "";
			TXT_MAX_RANGE.Text = "";
			
			try
			{
				this.DDL_PRODUCT_TYPE.SelectedItem.Selected = false;
			} 
			catch {}
			
			LBL_SEQ.Text= "";

			LBL_SAVEMODE.Text = "1";
		}
				
		public void ViewExistingData()
		{
			conn.QueryString = "SELECT A.RESULT_ID, A.PRODUCT_ID, B.PRODUCTNAME AS PRODUCT_NAME, " +
				"A.RESULT_NAME, A.MAX_RANGE, A.MIN_RANGE FROM MANDIRI_RANGE_RESULT A " +
				"JOIN TPRODUCT B ON A.PRODUCT_ID = B.PRODUCTID " + 
				"WHERE 1 = 1 ";

			if (this.DDL_RESULT_NAME.SelectedValue != "")
				conn.QueryString += "and A.RESULT_ID = '" + this.DDL_RESULT_NAME.SelectedValue + "' ";
			if (this.DDL_PRODUCT_TYPE.SelectedValue != "")
				conn.QueryString += "and B.PRODUCTID = '" + this.DDL_PRODUCT_TYPE.SelectedValue + "' ";

			conn.ExecuteQuery();
			
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_EXISTING.DataSource = data;
			
			try
			{
				DGR_EXISTING.DataBind();
			} 
			catch 
			{
				
				DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.PageCount - 1;
				DGR_EXISTING.DataBind();
			}
		}

		public void ViewPendingData()
		{
			conn.QueryString = "SELECT A.RESULT_ID, A.SEQ_ID ,A.PRODUCTID, B.PRODUCTNAME AS PRODUCT_NAME, "+
				"A.RESULT_NAME, A.MAX_RANGE, A.MIN_RANGE, A.CH_STA, "+
				" case when A.CH_STA = '1' then 'INSERT' when A.CH_STA = '0' then 'UPDATE' "+
				" else 'DELETE' end CH_STA1 FROM TMANDIRI_RANGE_RESULT A " +
				"JOIN TPRODUCT B ON A.PRODUCTID = B.PRODUCTID " +
				"WHERE 1 = 1 ";

			if (this.DDL_RESULT_NAME.SelectedValue != "")
				conn.QueryString += "and A.RESULT_ID = '" + this.DDL_RESULT_NAME.SelectedValue + "' ";
			if (this.DDL_PRODUCT_TYPE.SelectedValue != "")
				conn.QueryString += "and B.PRODUCTID = '" + this.DDL_PRODUCT_TYPE.SelectedValue + "' ";

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

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
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
			if (this.DDL_RESULT_NAME.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Result Name is required !");
				return;
			} 
			else if (this.DDL_PRODUCT_TYPE.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Product Type is required !");
				return;
			}
			else
			{
				/*
				  if (this.LBL_SAVEMODE.Text == "0" && (this.LBL_SEQ.Text == "")) //Bila update dari existing data
				  {
					  conn.QueryString= "SELECT * FROM TMANDIRI_RANGE_RESULT";
					  conn.ExecuteQuery();
					  int seq = conn.GetRowCount() + 1;
					  this.LBL_SEQ.Text = seq.ToString();
				  } 
				  else if (this.LBL_SAVEMODE.Text == "1") //bila insert data baru
				  {
					  conn.QueryString= "SELECT * FROM TMANDIRI_RANGE_RESULT";
					  conn.ExecuteQuery();
					  int seq = conn.GetRowCount() + 1;
					  this.LBL_SEQ.Text = seq.ToString();
				  } 
				  else  if (this.LBL_SAVEMODE.Text == "0" && (this.LBL_SEQ.Text != "")) //Bila update dari request data
				  {
					  //nilai seq sudah di set di DGR_EXISTING item command
				  } */

				if (this.LBL_SAVEMODE.Text == "1") //bila insert data baru
				{
					conn.QueryString= "SELECT * FROM TMANDIRI_RANGE_RESULT";
					conn.ExecuteQuery();
					
					int seq = conn.GetRowCount() + 1;
					
					LBL_SEQ.Text = seq.ToString();
				} 

//				if (this.LBL_RESULT_ID.Text == "" ) //kalau insert (data baru, nilai Result ID belum ada)
//				{
//					int res = DGR_REQUEST.Items.Count + 1;
//					LBL_RESULT_ID.Text = res.ToString();
//				}
				
				conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_RANGE_RESULT_MAKER '"+ LBL_SAVEMODE.Text.Trim() +"', '"+
					this.DDL_RESULT_NAME.SelectedValue +"', '"+ this.DDL_PRODUCT_TYPE.SelectedValue.ToString() +"', '"+
					this.DDL_RESULT_NAME.SelectedItem.Text.Trim() +"', "+ this.checkComma(TXT_MIN_RANGE.Text) +", "+
					this.checkComma(TXT_MAX_RANGE.Text) +", '"+ this.LBL_SEQ.Text.Trim() +"'";
				
				try
				{
					conn.ExecuteNonQuery();
				} 
				catch 
				{
					GlobalTools.popMessage(this, "Input data is not valid!");
				}

				ViewPendingData();
				clearEditBoxes();
				LBL_SAVEMODE.Text = "1";
			}
		}
		private string cleansFloat(string tn)
		{
			if (tn.Trim() == "&nbsp;")
				tn = "0";
			return tn;
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = "0"; //update
					//TXT_RESULT_NAME.Text = cleansText(e.Item.Cells[1].Text);
					TXT_MIN_RANGE.Text = cleansText(e.Item.Cells[4].Text);
					TXT_MAX_RANGE.Text = cleansText(e.Item.Cells[5].Text);
					
					try
					{
						this.DDL_PRODUCT_TYPE.SelectedValue = e.Item.Cells[2].Text;
					} 
					catch {}
					
					try
					{
						DDL_RESULT_NAME.SelectedValue = cleansText(e.Item.Cells[0].Text);
					} 
					catch {}
					LBL_PRODUCT_ID.Text = cleansText(e.Item.Cells[2].Text);

					conn.QueryString= "SELECT * FROM TMANDIRI_RANGE_RESULT";
					conn.ExecuteQuery();
					int seq = conn.GetRowCount() + 1;
					LBL_SEQ.Text = seq.ToString();

					break;
				case "delete": 
					conn.QueryString= "SELECT * FROM TMANDIRI_RANGE_RESULT";
					conn.ExecuteQuery();

					int seqc = conn.GetRowCount() + 1; // set sequence untuk DGR_REQ ...
					
					LBL_SEQ.Text = seqc.ToString();
					
					conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_RANGE_RESULT_MAKER '2', '"+ 
						cleansText(e.Item.Cells[0].Text) +"', '"+ cleansText(e.Item.Cells[2].Text) +"', '"+
						cleansText(e.Item.Cells[1].Text) +"', "+ checkComma(cleansFloat(e.Item.Cells[4].Text)) +", "+
						checkComma(cleansFloat(e.Item.Cells[5].Text)) +", '"+ LBL_SEQ.Text.Trim() + "'";
					conn.ExecuteNonQuery();

					ViewPendingData();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[6].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					
					//TXT_RESULT_NAME.Text = cleansText(e.Item.Cells[1].Text);
					TXT_MIN_RANGE.Text = cleansText(e.Item.Cells[4].Text);
					TXT_MAX_RANGE.Text = cleansText(e.Item.Cells[5].Text);
					
					try
					{
						DDL_PRODUCT_TYPE.SelectedValue = e.Item.Cells[2].Text;
					} 
					catch {}
					
					try
					{
						DDL_RESULT_NAME.SelectedValue = cleansText(e.Item.Cells[0].Text);
					} 
					catch {}
					LBL_SEQ.Text = cleansText(e.Item.Cells[8].Text);
					LBL_PRODUCT_ID.Text = cleansText(e.Item.Cells[2].Text);
					break;
				
				case "delete":
					string resid = cleansText(e.Item.Cells[0].Text);
					string prodid = cleansText(e.Item.Cells[2].Text);
					string seqid = cleansText(e.Item.Cells[8].Text);

					conn.QueryString = "DELETE FROM TMANDIRI_RANGE_RESULT WHERE RESULT_ID = '"+ resid+ "' " +
						"and PRODUCTID = '" +prodid+ "' and SEQ_ID = '"  +seqid+ "'" ;
					conn.ExecuteQuery();
					
					ViewPendingData();
					break;
				default:
					// do nothing
					break;
			}
		}

		protected void DDL_PRODUCT_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
			ViewPendingData();
		}

		protected void DDL_RESULT_NAME_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
			ViewPendingData();
		}
	}
}
