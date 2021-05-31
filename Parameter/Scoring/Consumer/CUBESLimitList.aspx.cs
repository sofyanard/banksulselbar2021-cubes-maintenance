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
	/// Summary description for CUBESLimitList.
	/// </summary>
	public partial class CUBESLimitList : System.Web.UI.Page
	{
		protected Connection conn2;
		protected Connection conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon(); 
			if (!IsPostBack)
			{
				FillProductType();
				ViewPendingData();
				ViewExistingData();
			}
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

		private void clearEditBoxes()
		{
			this.TXT_LIMIT_FORMULA.Text = "";
			this.TXT_LIMIT_LINK.Text = "";
			this.TXT_LIMIT_NAME.Text = "";
			this.TXT_LIMIT_TABLE.Text = "";
			
			try {this.DDL_PRODUCT_TYPE.SelectedItem.Selected = false;} 
			catch {}
			
			try {this.RBL_LIMIT_ACTIVE.SelectedItem.Selected = false;} 
			catch {}
	
			this.LBL_SEQ_ID.Text = "";
			this.LBL_SEQ_NO.Text = "";
 
			this.LBL_SAVEMODE.Text = "1"; 
		}

		public void FillProductType()
		{
			this.DDL_PRODUCT_TYPE.Items.Clear();
			this.DDL_PRODUCT_TYPE.Items.Add(new ListItem("- PILIH -", ""));
			
			conn.QueryString = "SELECT DISTINCT PRODUCTID,PRODUCTNAME FROM TPRODUCT ";
			if (this.RBL_LOAN_CODE.SelectedValue != "")
				conn.QueryString += "WHERE PRODUCT_TYPE = '" + this.RBL_LOAN_CODE.SelectedValue + "' ";
			conn.ExecuteQuery();
			
			for (int i=0; i <conn.GetRowCount(); i++)
			{
				this.DDL_PRODUCT_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
			}
		}

		public void ViewExistingData()
		{
			conn.QueryString = "select * from VW_PARAM_LIMITLIST_EXISTING where 1 = 1 ";
			
			if (this.RBL_PRM.SelectedValue != "")
				conn.QueryString += "and PRM_CODE = '" + this.RBL_PRM.SelectedValue + "' ";
			if (this.RBL_LOAN_CODE.SelectedValue != "")
				conn.QueryString += "and LOAN_CODE = '" + this.RBL_LOAN_CODE.SelectedValue + "' ";
			if (this.DDL_PRODUCT_TYPE.SelectedValue != "")
				conn.QueryString += "and (ISNULL(PRODUCTID,'') = '' OR PRODUCTID = '" + this.DDL_PRODUCT_TYPE.SelectedValue + "') ";
			
			LBL_LOAN_CODE.Text = this.RBL_LOAN_CODE.SelectedValue;
			LBL_PRM.Text = this.RBL_PRM.SelectedValue;
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
			conn.QueryString = "select * from VW_PARAM_LIMITLIST_PENDING where 1 = 1 ";

			if (this.RBL_PRM.SelectedValue != "")
				conn.QueryString += "and PRM_CODE = '" + this.RBL_PRM.SelectedValue + "' ";
			if (this.RBL_LOAN_CODE.SelectedValue != "")
				conn.QueryString += "and LOAN_CODE = '" + this.RBL_LOAN_CODE.SelectedValue + "' ";
			if (this.DDL_PRODUCT_TYPE.SelectedValue != "")
				conn.QueryString += "and (ISNULL(PRODUCTID,'') = '' OR PRODUCTID = '" + this.DDL_PRODUCT_TYPE.SelectedValue + "') ";
			
			conn.ExecuteQuery();
			
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_REQUEST.DataSource = data;
			
			try
			{
				this.DGR_REQUEST.DataBind();
			} 
			catch 
			{
				this.DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				this.DGR_REQUEST.DataBind();
			}
		}
		
		private string checkApost(string str)
		{
			return str.Replace("'", "''").Trim();
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
			if (this.LBL_SAVEMODE.Text == "1" && this.LBL_SEQ_NO.Text == "") //bila insert data baru
			{
//				conn.QueryString= "SELECT * FROM MANDIRI_LIMIT_LIST";
//				conn.ExecuteQuery();
//				int seq1 = conn.GetRowCount();
//				
//				conn.QueryString = "select * from TMANDIRI_LIMIT_LIST where CH_STA = '1'";
//				conn.ExecuteQuery();
//				int seq2 = conn.GetRowCount();
//				int seq = seq1 + seq2 + 1;
//				this.LBL_SEQ_NO.Text = seq.ToString();
				conn.QueryString = "select max(seq_no) + 1 "+
					"from 	( "+
					"	select isnull(max(convert(int,seq_no)),0) seq_no "+
					"	from mandiri_limit_list "+
					"	union "+
					"	select isnull(max(convert(int,seq_no)),0) seq_no "+
					"	from tmandiri_limit_list "+
					"	) alltbl ";
				conn.ExecuteQuery();
				this.LBL_SEQ_NO.Text = conn.GetFieldValue(0,0);
			} 
			int res = 1;
			if (res <= this.DGR_REQUEST.Items.Count)
			{
				res = this.DGR_REQUEST.Items.Count + 1;
			}
			this.LBL_SEQ_ID.Text = res.ToString();
											
			conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_LIMIT_LIST_MAKER '"+ LBL_SAVEMODE.Text.Trim() + "', "+
				this.LBL_SEQ_NO.Text.Trim() +", '"+ this.RBL_LOAN_CODE.SelectedValue.ToString() +"', '"+
				this.TXT_LIMIT_NAME.Text.Trim() +"', '"+ checkApost(this.TXT_LIMIT_FORMULA.Text) +"', '"+
				this.TXT_LIMIT_TABLE.Text.Trim() +"', '"+ checkApost(this.TXT_LIMIT_LINK.Text) +"', '"+ 
				this.DDL_PRODUCT_TYPE.SelectedValue +"', '"+ this.RBL_LIMIT_ACTIVE.SelectedValue +"', '"+ 
				this.RBL_PRM.SelectedValue.ToString() +"', '"+ this.LBL_SEQ_ID.Text.Trim() + "'";
			conn.ExecuteNonQuery();

			ViewPendingData();
			clearEditBoxes();
			this.LBL_SAVEMODE.Text = "1";
		
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					clearEditBoxes();
					this.LBL_SAVEMODE.Text = "0"; //update
					this.LBL_SEQ_NO.Text = cleansText(e.Item.Cells[0].Text);
					this.TXT_LIMIT_NAME.Text = cleansText(e.Item.Cells[2].Text);
					this.TXT_LIMIT_FORMULA.Text = cleansText(e.Item.Cells[3].Text);
					this.TXT_LIMIT_TABLE.Text = cleansText(e.Item.Cells[4].Text);
					this.TXT_LIMIT_LINK.Text = cleansText(e.Item.Cells[5].Text);

					try 
					{this.DDL_PRODUCT_TYPE.SelectedValue = cleansText(e.Item.Cells[6].Text);} 
					catch {}
					try {this.RBL_LIMIT_ACTIVE.SelectedValue = cleansText(e.Item.Cells[8].Text);} 
					catch {}
					try {this.RBL_LOAN_CODE.SelectedValue = cleansText(e.Item.Cells[11].Text);} 
					catch {}
					try {this.RBL_PRM.SelectedValue = cleansText(e.Item.Cells[10].Text);}
					catch {}
					this.LBL_LOAN_CODE.Text = cleansText(e.Item.Cells[11].Text);
					this.LBL_SEQ_ID.Text = ""; //update data dari DGR_EXISTING tidak ada nilai SEQ ID-nya!!
					
					break;
				case "delete": 
					int res = 1;

					if (res <= this.DGR_REQUEST.Items.Count)
					{
						res = this.DGR_REQUEST.Items.Count + 1;
					}

					this.LBL_SEQ_ID.Text = res.ToString();

					conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_LIMIT_LIST_MAKER '2', "+ 
						cleansText(e.Item.Cells[0].Text) +", "+ cleansText(e.Item.Cells[11].Text) +", '"+
						cleansText(e.Item.Cells[2].Text) +"', '"+ cleansText(e.Item.Cells[3].Text) +"', '"+
						cleansText(e.Item.Cells[4].Text) +"', '"+ cleansText(e.Item.Cells[5].Text) +"', '"+ 
						cleansText(e.Item.Cells[6].Text) +"', '"+ cleansText(e.Item.Cells[8].Text) +"', '"+ 
						cleansText(e.Item.Cells[10].Text) +"', '"+ this.LBL_SEQ_ID.Text.Trim() + "'";
					
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
					clearEditBoxes();
					this.LBL_SAVEMODE.Text = e.Item.Cells[11].Text.Trim();

					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					this.LBL_SEQ_NO.Text = cleansText(e.Item.Cells[0].Text);
					this.TXT_LIMIT_NAME.Text = cleansText(e.Item.Cells[2].Text);
					this.TXT_LIMIT_FORMULA.Text = cleansText(e.Item.Cells[3].Text);
					this.TXT_LIMIT_LINK.Text = cleansText(e.Item.Cells[5].Text);
					this.TXT_LIMIT_TABLE.Text = cleansText(e.Item.Cells[4].Text);
					try 
					{this.DDL_PRODUCT_TYPE.SelectedValue = cleansText(e.Item.Cells[6].Text);} 
					catch {}
					try {this.RBL_LIMIT_ACTIVE.SelectedValue = cleansText(e.Item.Cells[8].Text);} 
					catch {}
					try {this.RBL_LOAN_CODE.SelectedValue = cleansText(e.Item.Cells[14].Text);} 
					catch {}
					try {this.RBL_PRM.SelectedValue = cleansText(e.Item.Cells[10].Text);}
					catch {}
					this.LBL_SEQ_ID.Text = cleansText(e.Item.Cells[13].Text);
					
					break;
				case "delete":
					
					string seq_id = cleansText(e.Item.Cells[13].Text);
					string seq_no = cleansText(e.Item.Cells[0].Text);
					string loan_code = cleansText(e.Item.Cells[14].Text);
					
					conn.QueryString = "DELETE FROM TMANDIRI_LIMIT_LIST WHERE SEQ_ID = '"+ seq_id+ "' " +
						"and SEQ_NO = '" + seq_no + "' and LOAN_CODE = '"  + loan_code+ "'" ;
					conn.ExecuteQuery();			
					ViewPendingData();
					break;
				default:
					// do nothing
					break;
			}
		}

		protected void RBL_PRM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
			ViewPendingData();
		}

		protected void RBL_LOAN_CODE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillProductType();
			ViewExistingData();
			ViewPendingData();
		}

		protected void DDL_PRODUCT_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
			ViewPendingData();
		}
	}
}
