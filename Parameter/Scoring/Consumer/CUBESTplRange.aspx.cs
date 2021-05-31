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
	/// Summary description for CUBESTplRange.
	/// </summary>
	public partial class CUBESTplRange : System.Web.UI.Page
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
				LBL_SAVEMODE2.Text = "1";
				LBL_SEQ.Text = "";
				
				filltemplateddl();
				ViewExistingData();
				ViewPendingData();
				ViewExistingData1();
				ViewPendingData1();
			}
			DGR_EXISTING.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			DGR_REQUEST.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

			BTN_SAVE_VALUE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
			BTN_SAVE2.Attributes.Add("onclick","if(!cek_mandatoryString(document.Form1)){return false;};");
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "SELECT * FROM RFMODULE WHERE MODULEID = '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		public void filltemplateddl()
		{
			GlobalTools.fillRefList(DDL_TPLID, "SELECT RNGTPLID, RNGTPLDESC FROM MANDIRI_RANGE_TEMPLATE WHERE ACTIVE = '1'", conn);
		}

		private void clearEditBoxes()
		{
			TXT_TPLID.Enabled = true;
			TXT_TPLID.Text = "";
			TXT_TPLDESC.Text= "";
			LBL_SAVEMODE1.Text = "1";
		}

		private void clearEditParamValueBoxes()
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
				this.DDL_TPLID.SelectedItem.Selected = false;
				this.DDL_RESULT_NAME.SelectedItem.Selected = false;
			} 
			catch {}
			
			LBL_SEQ.Text= "";
			LBL_SAVEMODE2.Text = "1";
			DDL_TPLID.Enabled = true;
			DDL_RESULT_NAME.Enabled = true;
		}

		private void ViewExistingData1()
		{
			conn.QueryString = "SELECT * FROM VW_MANDIRI_RANGE_TEMPLATE ";
			conn.ExecuteQuery();
			this.DGR_EXISTING_VALUE.DataSource = conn.GetDataTable().Copy();
			try 
			{
				this.DGR_EXISTING_VALUE.DataBind();
			}
			catch 
			{
				this.DGR_EXISTING_VALUE.CurrentPageIndex = this.DGR_EXISTING_VALUE.PageCount - 1;
				this.DGR_EXISTING_VALUE.DataBind();
			}

			LinkButton lnkEdit, lnkDel;
			Label lblSta;
			for (int i = 0; i < this.DGR_EXISTING_VALUE.Items.Count; i++)
			{
				if (DGR_EXISTING_VALUE.Items[i].Cells[3].Text == "0")
				{
					
					lnkEdit = (LinkButton)DGR_EXISTING_VALUE.Items[i].Cells[2].FindControl("lnk_RfEdit3");
					lnkDel = (LinkButton)DGR_EXISTING_VALUE.Items[i].Cells[2].FindControl("lnk_RfDelete3");
					lblSta = (Label)DGR_EXISTING_VALUE.Items[i].Cells[2].FindControl("lbl_Status");

					lnkEdit.Visible = false;
					lnkDel.Visible = false;
					lblSta.ForeColor = System.Drawing.Color.Red;
					lblSta.Text = "DELETED";
				}
			}
		}

		private void ViewPendingData1()
		{
		
			conn.QueryString = "select * from VW_MANDIRI_RANGE_TTEMPLATE ";
			conn.ExecuteQuery();
			this.DGR_REQUEST_VALUE.DataSource = conn.GetDataTable().Copy();
			try 
			{
				this.DGR_REQUEST_VALUE.DataBind();
			}
			catch 
			{
				this.DGR_REQUEST_VALUE.CurrentPageIndex = this.DGR_REQUEST_VALUE.PageCount - 1;
				this.DGR_REQUEST_VALUE.DataBind();
			}
			
			for (int i = 0; i < this.DGR_REQUEST_VALUE.Items.Count; i++)
			{
				if (this.DGR_REQUEST_VALUE.Items[i].Cells[2].Text.Trim() == "0")
				{
					this.DGR_REQUEST_VALUE.Items[i].Cells[2].Text = "UPDATE";
				}
				else if (this.DGR_REQUEST_VALUE.Items[i].Cells[2].Text.Trim() == "1")
				{
					this.DGR_REQUEST_VALUE.Items[i].Cells[2].Text = "INSERT";
				}
				else if (this.DGR_REQUEST_VALUE.Items[i].Cells[2].Text.Trim() == "2")
				{
					this.DGR_REQUEST_VALUE.Items[i].Cells[2].Text = "DELETE";
				}
			} 
		}
		
		public void ViewExistingData()
		{
			conn.QueryString = "SELECT * FROM VW_MANDIRI_RANGE_RESULT WHERE 1 = 1 ";

			if (this.DDL_RESULT_NAME.SelectedValue != "")
				conn.QueryString += "and RESULT_ID = '" + this.DDL_RESULT_NAME.SelectedValue + "' ";
			if (this.DDL_TPLID.SelectedValue != "")
				conn.QueryString += "and RNGTPLID = '" + this.DDL_TPLID.SelectedValue + "' ";

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
			conn.QueryString = "SELECT * FROM VW_TMANDIRI_RANGE_RESULT WHERE 1 = 1 ";

			if (this.DDL_RESULT_NAME.SelectedValue != "")
				conn.QueryString += "and RESULT_ID = '" + this.DDL_RESULT_NAME.SelectedValue + "' ";
			if (this.DDL_TPLID.SelectedValue != "")
				conn.QueryString += "and RNGTPLID = '" + this.DDL_TPLID.SelectedValue + "' ";

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

		private string cleansFloat(string tn)
		{
			if (tn.Trim() == "&nbsp;")
				tn = "0";
			return tn;
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
			this.DGR_EXISTING_VALUE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_VALUE_ItemCommand);
			this.DGR_EXISTING_VALUE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_VALUE_PageIndexChanged);
			this.DGR_REQUEST_VALUE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_VALUE_ItemCommand);
			this.DGR_REQUEST_VALUE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_VALUE_PageIndexChanged);
			this.DGR_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_ItemCommand);
			this.DGR_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE2_Click(object sender, System.EventArgs e)
		{
			if (this.DDL_RESULT_NAME.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Result Name is required !");
				return;
			} 
			else if (this.DDL_TPLID.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Template ID is required !");
				return;
			}
			else
			{
				if (this.LBL_SAVEMODE2.Text == "1") //bila insert data baru
				{
					conn.QueryString= "SELECT * FROM TMANDIRI_RANGE_RESULT";
					conn.ExecuteQuery();
					
					int seq = conn.GetRowCount() + 1;
					
					LBL_SEQ.Text = seq.ToString();
				} 

				conn.QueryString = "EXEC PARAM_TEMPLATE_RANGE_RESULT_MAKER '"+ LBL_SAVEMODE2.Text.Trim() +"', '"+
					this.DDL_RESULT_NAME.SelectedValue +"', '"+ this.DDL_TPLID.SelectedValue.ToString() +"', '"+
					this.DDL_RESULT_NAME.SelectedItem.Text.Trim() +"', "+ this.checkComma(TXT_MIN_RANGE.Text) +", "+
					this.checkComma(TXT_MAX_RANGE.Text) +", '"+ this.LBL_SEQ.Text.Trim() +"'";
				
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

				clearEditParamValueBoxes();
				ViewPendingData();
				LBL_SAVEMODE2.Text = "1";
				GlobalTools.SetFocus(this,DDL_TPLID);
			}
		}

		protected void BTN_CANCEL2_Click(object sender, System.EventArgs e)
		{
			clearEditParamValueBoxes();
			GlobalTools.SetFocus(this,DDL_TPLID);
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
					LBL_SAVEMODE2.Text = "0"; //update
					//TXT_RESULT_NAME.Text = cleansText(e.Item.Cells[1].Text);
					TXT_MIN_RANGE.Text = cleansText(e.Item.Cells[3].Text);
					TXT_MAX_RANGE.Text = cleansText(e.Item.Cells[4].Text);
					
					try
					{
						this.DDL_TPLID.SelectedValue = e.Item.Cells[1].Text;
					} 
					catch {}
					
					try
					{
						DDL_RESULT_NAME.SelectedValue = cleansText(e.Item.Cells[0].Text);
					} 
					catch {}
					LBL_TPLID.Text = cleansText(e.Item.Cells[1].Text);

					DDL_TPLID.Enabled = false;
					DDL_RESULT_NAME.Enabled = false;

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
					
					conn.QueryString = "EXEC PARAM_TEMPLATE_RANGE_RESULT_MAKER '2', '"+ 
						cleansText(e.Item.Cells[0].Text) +"', '"+ cleansText(e.Item.Cells[1].Text) +"', '"+
						cleansText(e.Item.Cells[2].Text) +"', "+ checkComma(cleansFloat(e.Item.Cells[3].Text)) +", "+
						checkComma(cleansFloat(e.Item.Cells[4].Text)) +", '"+ LBL_SEQ.Text.Trim() + "'";
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
					LBL_SAVEMODE2.Text = e.Item.Cells[5].Text.Trim();
					if (LBL_SAVEMODE2.Text.Trim() == "2")
					{
						LBL_SAVEMODE2.Text = "1";
						break;
					}
					
					//TXT_RESULT_NAME.Text = cleansText(e.Item.Cells[1].Text);
					TXT_MIN_RANGE.Text = cleansText(e.Item.Cells[3].Text);
					TXT_MAX_RANGE.Text = cleansText(e.Item.Cells[4].Text);
					
					try
					{
						DDL_TPLID.SelectedValue = e.Item.Cells[1].Text;
					} 
					catch {}
					
					try
					{
						DDL_RESULT_NAME.SelectedValue = cleansText(e.Item.Cells[0].Text);
					} 
					catch {}
					LBL_SEQ.Text = cleansText(e.Item.Cells[7].Text);
					LBL_TPLID.Text = cleansText(e.Item.Cells[1].Text);
					DDL_TPLID.Enabled = false;
					DDL_RESULT_NAME.Enabled = false;

					break;
				
				case "delete":
					string resid = cleansText(e.Item.Cells[0].Text);
					string tplid = cleansText(e.Item.Cells[1].Text);
					string seqid = cleansText(e.Item.Cells[7].Text);

					conn.QueryString = "DELETE FROM TMANDIRI_RANGE_RESULT WHERE RESULT_ID = '"+ resid+ "' " +
						"and RNGTPLID = '" +tplid+ "' and SEQ_ID = '"  +seqid+ "'" ;
					conn.ExecuteQuery();
					
					ViewPendingData();
					break;
				default:
					// do nothing
					break;
			}
		}

		protected void DDL_TPLID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
			ViewPendingData();
		}

		protected void DDL_RESULT_NAME_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
			ViewPendingData();
		}

		protected void BTN_SAVE_VALUE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "EXEC PARAM_TEMPLATE_RANGE_MAKER '"+TXT_TPLID.Text+"', '"+TXT_TPLDESC.Text+"'" +
				", '"+LBL_SAVEMODE1.Text+"'";
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
			
			clearEditBoxes();
			ViewPendingData1();
		}

		protected void BTN_CANCEL_VALUE_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		private void DGR_EXISTING_VALUE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TXT_TPLID.Enabled = false;
					TXT_TPLID.Text = e.Item.Cells[0].Text.Trim();
					conn.QueryString="SELECT * FROM MANDIRI_RANGE_TEMPLATE WHERE RNGTPLID='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					
					TXT_TPLDESC.Text = conn.GetFieldValue("RNGTPLDESC");
					LBL_SAVEMODE1.Text = "0";
										
					break;
				case "Delete":
					conn.QueryString="SELECT * FROM MANDIRI_RANGE_TTEMPLATE WHERE RNGTPLID='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="UPDATE MANDIRI_RANGE_TTEMPLATE SET CH_STA='2' WHERE RNGTPLID='"+e.Item.Cells[0].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_TEMPLATE_RANGE_MAKER '" +
							e.Item.Cells[0].Text.Trim()+ "','" + e.Item.Cells[1].Text.Trim() + "','2'" ;
						conn.ExecuteQuery();
					}
					ViewPendingData1();
					break;
				default:
					
					break;
			}
		}

		private void DGR_EXISTING_VALUE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING_VALUE.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData1();
		}

		private void DGR_REQUEST_VALUE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":

					conn.QueryString="SELECT * FROM MANDIRI_RANGE_TTEMPLATE WHERE RNGTPLID='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();

					string status = e.Item.Cells[2].Text;
					if(status=="DELETE")
					{
						LBL_SAVEMODE1.Text = "1";
					}
					else
					{	
						TXT_TPLID.Enabled = false;
						TXT_TPLID.Text = conn.GetFieldValue("RNGTPLID");
						TXT_TPLDESC.Text = conn.GetFieldValue("RNGTPLDESC");
						
						if(e.Item.Cells[2].Text=="UPDATE")
						{
							LBL_SAVEMODE1.Text = "0";
						}
						if(e.Item.Cells[2].Text=="INSERT")
						{
							LBL_SAVEMODE1.Text = "1";
						}
					}
					break;
				case "Delete":
					string CODE = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM MANDIRI_RANGE_TTEMPLATE WHERE RNGTPLID = '"+CODE+"'";
					conn.ExecuteQuery();
					ViewPendingData1();
					filltemplateddl();
					break;
				default:
					break;			
			}
		}

		private void DGR_REQUEST_VALUE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST_VALUE.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData1();
		}

	}
}
