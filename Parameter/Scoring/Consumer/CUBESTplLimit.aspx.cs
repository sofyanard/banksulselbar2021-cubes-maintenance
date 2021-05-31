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

namespace CuBES_Maintenance.Parameter.Scoring.Consumer
{
	/// <summary>
	/// Summary description for CUBESTplLimit.
	/// </summary>
	public partial class CUBESTplLimit : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				filltemplateddl();
				ViewPendingDataVal();
				ViewExistingDataVal();

				RBL_PRM.SelectedValue = "0";
				RBL_LIMIT_ACTIVE.SelectedValue = "1";
				ViewExistingDataTpl();
				ViewPendingDataTpl();
			}
			BTN_SAVE_TPL.Attributes.Add("onclick","if(!cek_mandatory2(document.Form1)){return false;};");
			BTN_SAVE_VAL.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "SELECT * FROM RFMODULE WHERE MODULEID = '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
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

		private void clearEditBoxesVal()
		{
			this.TXT_LIMIT_FORMULA.Text = "";
			this.TXT_LIMIT_LINK.Text = "";
			this.TXT_LIMIT_NAME.Text = "";
			this.TXT_LIMIT_TABLE.Text = "";
			
			try {this.DDL_TPLID.SelectedItem.Selected = false;} 
			catch {}
			
			try {this.RBL_LIMIT_ACTIVE.SelectedItem.Selected = false;} 
			catch {}
	
			this.LBL_SEQ_ID.Text = "";
			this.LBL_SEQ_NO.Text = "";
 
			this.LBL_SAVEMODE_VAL.Text = "1"; 
		}

		public void filltemplateddl()
		{
			GlobalTools.fillRefList(DDL_TPLID, "SELECT LMTTPLID, LMTTPLDESC FROM MANDIRI_LIMIT_TEMPLATE WHERE ACTIVE = '1'", conn);
		}

		public void ViewExistingDataVal()
		{
			conn.QueryString = "select * from VW_PARAM_TEMPLATE_LIMIT_MAKER_EXSISTING where LOAN_CODE is not null ";
			
			if (this.RBL_PRM.SelectedValue != "")
				conn.QueryString += "and PRM_CODE = '" + this.RBL_PRM.SelectedValue + "' ";
			if (this.DDL_TPLID.SelectedValue != "")
				conn.QueryString += "and LMTTPLID = '" + this.DDL_TPLID.SelectedValue + "' ";
			if (this.RBL_LIMIT_ACTIVE.SelectedValue != "")
				conn.QueryString += "and LIMIT_ACTIVE = '" + this.RBL_LIMIT_ACTIVE.SelectedValue + "' ";

			LBL_PRM.Text = this.RBL_PRM.SelectedValue;
			conn.ExecuteQuery();
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_EXISTING_VAL.DataSource = data;
			try
			{
				this.DGR_EXISTING_VAL.DataBind();
			} 
			catch 
			{
				
				this.DGR_EXISTING_VAL.CurrentPageIndex = DGR_EXISTING_VAL.PageCount - 1;
				this.DGR_EXISTING_VAL.DataBind();
			}
		}

		public void ViewPendingDataVal()
		{
			conn.QueryString = "select * from VW_PARAM_TEMPLATE_LIMIT_MAKER_PENDING where LOAN_CODE is not null ";

			if (this.RBL_PRM.SelectedValue != "")
				conn.QueryString += "and PRM_CODE = '" + this.RBL_PRM.SelectedValue + "' ";
			if (this.DDL_TPLID.SelectedValue != "")
				conn.QueryString += "and LMTTPLID = '" + this.DDL_TPLID.SelectedValue + "' ";
			if (this.RBL_LIMIT_ACTIVE.SelectedValue != "")
				conn.QueryString += "and LIMIT_ACTIVE = '" + this.RBL_LIMIT_ACTIVE.SelectedValue + "' ";
			
			conn.ExecuteQuery();
			
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_REQUEST_VAL.DataSource = data;
			
			try
			{
				this.DGR_REQUEST_VAL.DataBind();
			} 
			catch 
			{
				this.DGR_REQUEST_VAL.CurrentPageIndex = DGR_REQUEST_VAL.PageCount - 1;
				this.DGR_REQUEST_VAL.DataBind();
			}
		}

		private void clearEditBoxesTpl()
		{
			TXT_TPLID.Enabled = true;
			TXT_TPLID.Text = "";
			TXT_TPLDESC.Text= "";
			LBL_SAVEMODE_TPL.Text = "1";
		}

		private void ViewExistingDataTpl()
		{
			conn.QueryString = "SELECT * FROM VW_MANDIRI_LIMIT_TEMPLATE";
			conn.ExecuteQuery();
			this.DGR_EXISTING_TPL.DataSource = conn.GetDataTable().Copy();
			try 
			{
				this.DGR_EXISTING_TPL.DataBind();
			}
			catch 
			{
				this.DGR_EXISTING_TPL.CurrentPageIndex = this.DGR_EXISTING_TPL.PageCount - 1;
				this.DGR_EXISTING_TPL.DataBind();
			}

			LinkButton lnkEdit, lnkDel;
			Label lblSta;
			for (int i = 0; i < this.DGR_EXISTING_TPL.Items.Count; i++)
			{
				if (DGR_EXISTING_TPL.Items[i].Cells[3].Text == "0")
				{
					
					lnkEdit = (LinkButton)DGR_EXISTING_TPL.Items[i].Cells[2].FindControl("lnk_RfEdit3");
					lnkDel = (LinkButton)DGR_EXISTING_TPL.Items[i].Cells[2].FindControl("lnk_RfDelete3");
					lblSta = (Label)DGR_EXISTING_TPL.Items[i].Cells[2].FindControl("lbl_Status");

					lnkEdit.Visible = false;
					lnkDel.Visible = false;
					lblSta.ForeColor = System.Drawing.Color.Red;
					lblSta.Text = "DELETED";
				}
			}
		}

		private void ViewPendingDataTpl()
		{
		
			conn.QueryString = "select * from VW_MANDIRI_LIMIT_TTEMPLATE ";
			conn.ExecuteQuery();
			this.DGR_REQUEST_TPL.DataSource = conn.GetDataTable().Copy();
			try 
			{
				this.DGR_REQUEST_TPL.DataBind();
			}
			catch 
			{
				this.DGR_REQUEST_TPL.CurrentPageIndex = this.DGR_REQUEST_TPL.PageCount - 1;
				this.DGR_REQUEST_TPL.DataBind();
			}
			
			for (int i = 0; i < this.DGR_REQUEST_TPL.Items.Count; i++)
			{
				if (this.DGR_REQUEST_TPL.Items[i].Cells[2].Text.Trim() == "0")
				{
					this.DGR_REQUEST_TPL.Items[i].Cells[2].Text = "UPDATE";
				}
				else if (this.DGR_REQUEST_TPL.Items[i].Cells[2].Text.Trim() == "1")
				{
					this.DGR_REQUEST_TPL.Items[i].Cells[2].Text = "INSERT";
				}
				else if (this.DGR_REQUEST_TPL.Items[i].Cells[2].Text.Trim() == "2")
				{
					this.DGR_REQUEST_TPL.Items[i].Cells[2].Text = "DELETE";
				}
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
			this.DGR_EXISTING_TPL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_TPL_ItemCommand);
			this.DGR_EXISTING_TPL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_TPL_PageIndexChanged);
			this.DGR_REQUEST_TPL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_TPL_ItemCommand);
			this.DGR_REQUEST_TPL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_TPL_PageIndexChanged);
			this.DGR_EXISTING_VAL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_VAL_ItemCommand);
			this.DGR_EXISTING_VAL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_VAL_PageIndexChanged);
			this.DGR_REQUEST_VAL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_VAL_ItemCommand);
			this.DGR_REQUEST_VAL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_VAL_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_VAL_Click(object sender, System.EventArgs e)
		{
			if (this.LBL_SAVEMODE_VAL.Text == "1" && this.LBL_SEQ_NO.Text == "") //bila insert data baru
			{
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
			if (res <= this.DGR_REQUEST_VAL.Items.Count)
			{
				res = this.DGR_REQUEST_VAL.Items.Count + 1;
			}
			this.LBL_SEQ_ID.Text = res.ToString();
											
			conn.QueryString = "EXEC PARAM_TEMPLATE_LIMIT_LIST_MAKER '"+ LBL_SAVEMODE_VAL.Text.Trim() + "', "+
				this.LBL_SEQ_NO.Text.Trim() +", '"+
				this.TXT_LIMIT_NAME.Text.Trim() +"', '"+ checkApost(this.TXT_LIMIT_FORMULA.Text) +"', '"+
				this.TXT_LIMIT_TABLE.Text.Trim() +"', '"+ checkApost(this.TXT_LIMIT_LINK.Text) +"', '"+ 
				this.DDL_TPLID.SelectedValue +"', '"+ this.RBL_LIMIT_ACTIVE.SelectedValue +"', '"+ 
				this.RBL_PRM.SelectedValue.ToString() +"', '"+ this.LBL_SEQ_ID.Text.Trim() + "'";
			try
			{
				conn.ExecuteNonQuery();
			} 
			catch (Exception ex)
			{
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)		//method Connection.ExecuteNonQuery() add this msg on exception 
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this, errmsg);
			}


			ViewPendingDataVal();
			clearEditBoxesVal();
			this.LBL_SAVEMODE_VAL.Text = "1";
		}

		protected void BTN_CANCEL_VAL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxesVal();
		}

		private void DGR_EXISTING_VAL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING_VAL.CurrentPageIndex = e.NewPageIndex;
			ViewExistingDataVal();
		}

		private void DGR_EXISTING_VAL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					clearEditBoxesVal();
					this.LBL_SAVEMODE_VAL.Text = "0"; //update
					this.LBL_SEQ_NO.Text = cleansText(e.Item.Cells[1].Text);
					this.TXT_LIMIT_NAME.Text = cleansText(e.Item.Cells[3].Text);
					this.TXT_LIMIT_FORMULA.Text = cleansText(e.Item.Cells[4].Text);
					this.TXT_LIMIT_TABLE.Text = cleansText(e.Item.Cells[5].Text);
					this.TXT_LIMIT_LINK.Text = cleansText(e.Item.Cells[6].Text);

					try 
					{this.DDL_TPLID.SelectedValue = cleansText(e.Item.Cells[0].Text);} 
					catch {}
					try {this.RBL_LIMIT_ACTIVE.SelectedValue = cleansText(e.Item.Cells[7].Text);} 
					catch {}
					try {this.RBL_PRM.SelectedValue = cleansText(e.Item.Cells[9].Text);}
					catch {}
					this.LBL_SEQ_ID.Text = ""; //update data dari DGR_EXISTING tidak ada nilai SEQ ID-nya!!
					
					break;
				case "delete": 
					int res = 1;

					if (res <= this.DGR_REQUEST_VAL.Items.Count)
					{
						res = this.DGR_REQUEST_VAL.Items.Count + 1;
					}

					this.LBL_SEQ_ID.Text = res.ToString();

					conn.QueryString = "EXEC PARAM_TEMPLATE_LIMIT_LIST_MAKER '2', "+ 
						cleansText(e.Item.Cells[1].Text) +", '"+
						cleansText(e.Item.Cells[3].Text) +"', '"+ cleansText(e.Item.Cells[4].Text) +"', '"+
						cleansText(e.Item.Cells[5].Text) +"', '"+ cleansText(e.Item.Cells[6].Text) +"', '"+ 
						cleansText(e.Item.Cells[0].Text) +"', '"+ cleansText(e.Item.Cells[7].Text) +"', '"+ 
						cleansText(e.Item.Cells[9].Text) +"', '"+ this.LBL_SEQ_ID.Text.Trim() + "'";

					try
					{
						conn.ExecuteNonQuery();
					} 
					catch { GlobalTools.popMessage (this,"Error on stored procedure!");}
					ViewPendingDataVal();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_REQUEST_VAL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST_VAL.CurrentPageIndex = e.NewPageIndex;
			ViewPendingDataVal();
		}

		private void DGR_REQUEST_VAL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					clearEditBoxesVal();
					this.LBL_SAVEMODE_VAL.Text = e.Item.Cells[10].Text.Trim();

					if (LBL_SAVEMODE_VAL.Text.Trim() == "2")
					{
						LBL_SAVEMODE_VAL.Text = "1";
						break;
					}
					this.LBL_SEQ_NO.Text = cleansText(e.Item.Cells[1].Text);
					this.TXT_LIMIT_NAME.Text = cleansText(e.Item.Cells[3].Text);
					this.TXT_LIMIT_FORMULA.Text = cleansText(e.Item.Cells[4].Text);
					this.TXT_LIMIT_LINK.Text = cleansText(e.Item.Cells[5].Text);
					this.TXT_LIMIT_TABLE.Text = cleansText(e.Item.Cells[6].Text);
					try 
					{this.DDL_TPLID.SelectedValue = cleansText(e.Item.Cells[0].Text);} 
					catch {}
					try {this.RBL_LIMIT_ACTIVE.SelectedValue = cleansText(e.Item.Cells[7].Text);} 
					catch {}
					try {this.RBL_PRM.SelectedValue = cleansText(e.Item.Cells[9].Text);}
					catch {}
					this.LBL_SEQ_ID.Text = cleansText(e.Item.Cells[12].Text);
					
					break;
				case "delete":
					
					string seq_id = cleansText(e.Item.Cells[12].Text);
					string seq_no = cleansText(e.Item.Cells[1].Text);
					
					conn.QueryString = "DELETE FROM TMANDIRI_LIMIT_LIST WHERE SEQ_ID = '"+ seq_id+ "' " +
						"and SEQ_NO = '" + seq_no + "'" ;
					conn.ExecuteQuery();			
					ViewPendingDataVal();
					break;
				default:
					// do nothing
					break;
			}
		}

		protected void RBL_PRM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingDataVal();
			ViewPendingDataVal();
		}

		protected void DDL_TPLID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingDataVal();
			ViewPendingDataVal();
		}

		protected void BTN_SAVE_TPL_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "EXEC PARAM_TEMPLATE_LIMIT_MAKER '"+TXT_TPLID.Text+"', '"+TXT_TPLDESC.Text+"'" +
				", '"+LBL_SAVEMODE_TPL.Text+"'";
			try
			{
				conn.ExecuteNonQuery();
			} 
			catch (Exception ex)
			{
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)		//method Connection.ExecuteNonQuery() add this msg on exception 
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this, errmsg);
			}

			clearEditBoxesTpl();
			ViewPendingDataTpl();
		}

		protected void BTN_CANCEL_TPL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxesTpl();
		}

		private void DGR_EXISTING_TPL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxesTpl();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TXT_TPLID.Enabled = false;
					TXT_TPLID.Text = e.Item.Cells[0].Text.Trim();
					conn.QueryString="SELECT * FROM VW_MANDIRI_LIMIT_TEMPLATE WHERE LMTTPLID='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					
					TXT_TPLDESC.Text = conn.GetFieldValue("LMTTPLDESC");
					LBL_SAVEMODE_TPL.Text = "0";
										
					break;
				case "Delete":
					conn.QueryString="SELECT * FROM VW_MANDIRI_LIMIT_TTEMPLATE WHERE LMTTPLID='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="UPDATE MANDIRI_LIMIT_TTEMPLATE SET CH_STA='2' WHERE LMTTPLID='"+e.Item.Cells[0].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_TEMPLATE_LIMIT_MAKER '" +
							e.Item.Cells[0].Text.Trim()+ "','" + e.Item.Cells[1].Text.Trim() + "','2'" ;
						conn.ExecuteQuery();
					}
					ViewPendingDataTpl();
					break;
				default:
					
					break;
			}
		}

		private void DGR_EXISTING_TPL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING_TPL.CurrentPageIndex = e.NewPageIndex;
			ViewExistingDataTpl();
		}

		private void DGR_REQUEST_TPL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxesTpl();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":

					conn.QueryString="SELECT * FROM VW_MANDIRI_LIMIT_TTEMPLATE WHERE LMTTPLID='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();

					string status = e.Item.Cells[2].Text;
					if(status=="DELETE")
					{
						LBL_SAVEMODE_TPL.Text = "1";
					}
					else
					{
						TXT_TPLID.Enabled = false;
						TXT_TPLID.Text = conn.GetFieldValue("LMTTPLID");
						TXT_TPLDESC.Text = conn.GetFieldValue("LMTTPLDESC");
						
						if(e.Item.Cells[2].Text=="UPDATE")
						{
							LBL_SAVEMODE_TPL.Text = "0";
						}
						if(e.Item.Cells[2].Text=="INSERT")
						{
							LBL_SAVEMODE_TPL.Text = "1";
						}
					}
					break;
				case "Delete":
					string CODE = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM MANDIRI_LIMIT_TTEMPLATE WHERE LMTTPLID = '"+CODE+"'";
					conn.ExecuteQuery();
					ViewPendingDataTpl();
					filltemplateddl();
					break;
				default:
					break;			
			}
		}

		private void DGR_REQUEST_TPL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST_TPL.CurrentPageIndex = e.NewPageIndex;
			ViewPendingDataTpl();
		}

		protected void RBL_LIMIT_ACTIVE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingDataVal();
			ViewPendingDataVal();
		}

	}
}
