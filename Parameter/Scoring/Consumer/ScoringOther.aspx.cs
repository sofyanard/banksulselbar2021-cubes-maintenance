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
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.Scoring.Consumer
{
	/// <summary>
	/// Summary description for ScoringOther.
	/// </summary>
	public partial class ScoringOther : System.Web.UI.Page
	{
		protected Connection conn, conn2;
		protected string Id, mid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2(); 
			
			if(!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1";
														
				ViewExistingParameterListData();
				ViewPendingParameterListData();
			}

			BTN_SAVE_LIST.Attributes.Add("onclick","if(!cek_mandatory(document.form1)){return false;};");
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "SELECT * FROM RFMODULE WHERE MODULEID = '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}
		
		public void ViewExistingParameterListData()
		{ 
			conn.QueryString = "SELECT * FROM VW_MANDIRI_PARAM_LIST WHERE PARAM_OTHER = '1' "; 

			if (RBL_PARAM_PRM.SelectedValue != "")
				conn.QueryString += "AND PARAM_PRM = '" + RBL_PARAM_PRM.SelectedValue + "' ";
			if (RBL_PARAM_ACTIVE.SelectedValue != "")
				conn.QueryString += "AND PARAM_ACTIVE = '" + RBL_PARAM_ACTIVE.SelectedValue + "' ";
				 
			conn.QueryString += "ORDER BY PARAM_ID ASC";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_EXISTING_LIST.DataSource = data;
			
			try
			{
				DGR_EXISTING_LIST.DataBind();
			} 
			catch 
			{
				
				this.DGR_EXISTING_LIST.CurrentPageIndex = DGR_EXISTING_LIST.PageCount - 1;
				DGR_EXISTING_LIST.DataBind();
			}

		}

		public void ViewPendingParameterListData()
		{
			conn.QueryString = "SELECT * FROM VW_TMANDIRI_PARAM_LIST WHERE PARAM_OTHER = '1' ";
			
			if (RBL_PARAM_PRM.SelectedValue != "")
				conn.QueryString += "AND PARAM_PRM = '" + RBL_PARAM_PRM.SelectedValue + "' ";
			if (RBL_PARAM_ACTIVE.SelectedValue != "")
				conn.QueryString += "AND PARAM_ACTIVE = '" + RBL_PARAM_ACTIVE.SelectedValue + "' ";

			conn.QueryString += "ORDER BY PARAM_ID ASC";
			conn.ExecuteQuery();
				 

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_REQUEST_LIST.DataSource = data;
			
			try
			{
				DGR_REQUEST_LIST.DataBind();
			} 
			catch 
			{
				this.DGR_REQUEST_LIST.CurrentPageIndex = DGR_REQUEST_LIST.PageCount - 1;
				DGR_REQUEST_LIST.DataBind();
			}
		}

		private void clearEditParamListBoxes()
		{
			TXT_ID1.Enabled = true;
			this.TXT_ID1.Text = "";
			this.TXT_PARAM_NAME.Text = "";
			this.TXT_PARAM_FORMULA.Text = "";
			this.TXT_PARAM_LINK.Text = "";
			this.TXT_PARAM_FIELD.Text = "";
			this.TXT_PARAM_TABLE.Text = "";
			this.TXT_PARAM_TABLE_CHILD.Text = "";
			
			try 
			{
				this.RBL_PARAM_PRM.SelectedItem.Selected = false;
				this.RBL_PARAM_ACTIVE.SelectedItem.Selected = false;
			}
			catch {}			
		}

		private string checkApost(string str)
		{
			return str.Replace("'", "''").Trim();
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
			this.DGR_EXISTING_LIST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_LIST_ItemCommand);
			this.DGR_EXISTING_LIST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_LIST_PageIndexChanged);
			this.DGR_REQUEST_LIST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_LIST_ItemCommand);
			this.DGR_REQUEST_LIST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_LIST_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_LIST_Click(object sender, System.EventArgs e)
		{
			int SEQID = DGR_REQUEST_LIST.Items.Count + 1;

			conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAMLIST_OTHER_MAKER '"+ LBL_SAVEMODE.Text.Trim() + "', '" +
				this.TXT_ID1.Text.Trim() + "', '" + TXT_PARAM_NAME.Text.Trim() + "', '" + checkApost(TXT_PARAM_FORMULA.Text.Trim()) + "', '" +
				TXT_PARAM_FIELD.Text.Trim() + "', '" + TXT_PARAM_TABLE.Text.Trim() + "', '"+ TXT_PARAM_TABLE_CHILD.Text.Trim() + "', '"+
				checkApost(TXT_PARAM_LINK.Text.Trim()) + "', '" + this.RBL_PARAM_PRM.SelectedValue.ToString() + "', '" +
				this.RBL_PARAM_ACTIVE.SelectedValue.ToString() + "', '" + SEQID  + "'";
			
			try
			{
				conn.ExecuteQuery();
			} 
			catch (Exception ex)
			{
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)		//method Connection.ExecuteNonQuery() add this msg on exception 
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this, errmsg);
			}
			clearEditParamListBoxes();
			ViewPendingParameterListData();			
		}

		protected void BTN_CANCEL_LIST_Click(object sender, System.EventArgs e)
		{
			clearEditParamListBoxes();
		}

		private void DGR_REQUEST_LIST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditParamListBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[9].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					this.TXT_ID1.Text = cleansText(e.Item.Cells[0].Text);;
					this.TXT_PARAM_NAME.Text = cleansText(e.Item.Cells[1].Text);
					this.TXT_PARAM_FORMULA.Text =cleansText(e.Item.Cells[2].Text);
					this.TXT_PARAM_FIELD.Text = cleansText(e.Item.Cells[3].Text);
					this.TXT_PARAM_TABLE.Text = cleansText(e.Item.Cells[4].Text);
					this.TXT_PARAM_TABLE_CHILD.Text = cleansText(e.Item.Cells[5].Text);
					this.TXT_PARAM_LINK.Text = cleansText(e.Item.Cells[6].Text);
					try 
					{	
						this.RBL_PARAM_PRM.SelectedValue =e.Item.Cells[7].Text;
					}
					catch {}
					try 
					{
						this.RBL_PARAM_ACTIVE.SelectedValue = e.Item.Cells[8].Text;
					} 
					catch{}
					TXT_ID1.Enabled = false;
					break;
				case "delete":
					Id = cleansText(e.Item.Cells[0].Text);
					
					conn.QueryString = "DELETE FROM TMANDIRI_PARAM_LIST WHERE PARAM_ID = '"+ Id + "' ";
					conn.ExecuteQuery();
					
					TXT_ID1.Enabled = true;
					ViewPendingParameterListData();
					break;
				default:
					// do nothing
					break;
			}
		}

		private void DGR_EXISTING_LIST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditParamListBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					this.TXT_ID1.Text = cleansText(e.Item.Cells[0].Text);
					this.TXT_PARAM_NAME.Text = cleansText(e.Item.Cells[1].Text);
					this.TXT_PARAM_FORMULA.Text =cleansText(e.Item.Cells[2].Text);
					this.TXT_PARAM_FIELD.Text = cleansText(e.Item.Cells[3].Text);
					this.TXT_PARAM_TABLE.Text = cleansText(e.Item.Cells[4].Text);
					this.TXT_PARAM_TABLE_CHILD.Text = cleansText(e.Item.Cells[5].Text);
					this.TXT_PARAM_LINK.Text = cleansText(e.Item.Cells[6].Text);
					try 
					{	
						this.RBL_PARAM_PRM.SelectedValue =e.Item.Cells[7].Text;
					}
					catch {}
					try 
					{
						this.RBL_PARAM_ACTIVE.SelectedValue = e.Item.Cells[8].Text;
					} 
					catch{}
					TXT_ID1.Enabled = false;
					break;
				case "delete":
					conn.QueryString= "SELECT * FROM TMANDIRI_PARAM_LIST";
					conn.ExecuteQuery();
					int seq = conn.GetRowCount() + 1;
					Id = cleansText(e.Item.Cells[0].Text);
					
					conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAMLIST_OTHER_MAKER '2', '"+ Id + "', '"+
						cleansText(e.Item.Cells[1].Text) +"', '"+ cleansText(e.Item.Cells[2].Text) +"', '"+
						cleansText(e.Item.Cells[3].Text) +"', '"+ cleansText(e.Item.Cells[4].Text) +"', '"+
						cleansText(e.Item.Cells[5].Text) +"', '"+ cleansText(e.Item.Cells[6].Text) +"', '"+
						cleansText(e.Item.Cells[7].Text) +"', '"+ cleansText(e.Item.Cells[8].Text) +"', '"+
						seq.ToString() + "' ";
					conn.ExecuteQuery();

					TXT_ID1.Enabled = true;
					ViewPendingParameterListData();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_EXISTING_LIST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING_LIST.CurrentPageIndex = e.NewPageIndex;
			ViewExistingParameterListData();
		}

		private void DGR_REQUEST_LIST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST_LIST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingParameterListData();
		}

		protected void RBL_PARAM_PRM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingParameterListData(); 
			ViewPendingParameterListData();
		}

		protected void RBL_PARAM_ACTIVE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingParameterListData(); 
			ViewPendingParameterListData();
		}
	}
}
