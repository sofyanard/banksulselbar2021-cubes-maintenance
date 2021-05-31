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
	/// Summary description for CUBESTplScore.
	/// </summary>
	public partial class CUBESTplScore : System.Web.UI.Page
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
				LBL_SAVEMODE3.Text = "1";
				LBL_PRMVALNAME.Text = "";
				LBL_SEQ.Text = "";
				RBL_PARAM.SelectedValue = "0";
				RBL_PARAM_LOC.SelectedValue = "0";

				bindData1();
				bindData2();
				filltemplate();
				fillparam();							
				ViewExistingParameterValueData();
				ViewPendingParameterValueData();
			}
			

			BTN_SAVE_VALUE.Attributes.Add("onclick","if(!cek_mandatory2(document.Form1)){return false;};");
			BTN_SAVE_LIST.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
			
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "SELECT * FROM RFMODULE WHERE MODULEID = '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		
		private void bindData1()
		{
			conn.QueryString = "SELECT * FROM VW_MANDIRI_SCORING_TEMPLATE";
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
			for (int i = 0; i < this.DGR_EXISTING_VALUE.Items.Count; i++)
			{
				if (DGR_EXISTING_VALUE.Items[i].Cells[3].Text == "0")
				{
					
					lnkEdit = (LinkButton)DGR_EXISTING_VALUE.Items[i].Cells[2].FindControl("lnk_RfEdit3");
					lnkDel = (LinkButton)DGR_EXISTING_VALUE.Items[i].Cells[2].FindControl("lnk_RfDelete3");
				}
			}
		}

		private void bindData2()
		{
		
			conn.QueryString = "select * from VW_MANDIRI_SCORING_TTEMPLATE ";
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

		
		private void clearEditBoxes()
		{
			TXT_TPLID.Enabled = true;
			TXT_TPLID.Text="";
			TXT_TPLDESC.Text="";
			LBL_SAVEMODE.Text = "1";
		}

		private void cleansTextBox (TextBox tb)
		{
			if (tb.Text.Trim() == "&nbsp;")
				tb.Text = "";
		}

		public void filltemplate()
		{
			GlobalTools.fillRefList(DDL_TPLID, "SELECT SCOTPLID, SCOTPLDESC FROM MANDIRI_SCORING_TEMPLATE WHERE ACTIVE = '1'", conn);
		}

		public void fillparam()
		{	
			GlobalTools.fillRefList(DDL_PARAMID, "SELECT PARAM_ID,PARAM_NAME FROM MANDIRI_PARAM_LIST " +
				"WHERE PARAM_OTHER = '"+ RBL_PARAM.SelectedValue +"' AND PARAM_PRM = '" + RBL_PARAM_LOC.SelectedValue + "' " +
				"AND PARAM_ACTIVE = '1'", conn);	
	
			fillvalue();
		}

		private void fillvalue()
		{
			conn.QueryString = "SELECT PARAM_TABLE,PARAM_TABLE_CHILD FROM VW_MANDIRI_PARAM_LIST " +
				"WHERE PARAM_ID = '" + DDL_PARAMID.SelectedValue + "' ";
			conn.ExecuteQuery();

			
			string TABLE,TABLE_CHILD;
			TABLE = conn.GetFieldValue("PARAM_TABLE");
			TABLE_CHILD = conn.GetFieldValue("PARAM_TABLE_CHILD");
			string Comm;
			
			this.DDL_VALUE.Items.Clear();
			
			if (TABLE.Trim() == TABLE_CHILD.Trim())
			{
				TR_MAX_VALUE.Visible = true;
				TR_MIN_VALUE.Visible = true; 
				TR_DDL_VALUE.Visible = false; 	
				
				if (RBL_PARAM.SelectedValue == "1")
				{
					LBL_PARAM_SCORE.Text = "Parameter Other Score";
					TR_MAX_VALUE.Visible = false;
					TR_MIN_VALUE.Visible = false; 
				}

			} 
			else if(TABLE.Trim() != TABLE_CHILD.Trim())
			{
				TR_MAX_VALUE.Visible = false;
				TR_MIN_VALUE.Visible = false; 
				
				TR_DDL_VALUE.Visible = true; 	

				this.DDL_VALUE.Items.Add(new ListItem("- PILIH -",""));
				
				if(TABLE.Trim() == "RFBUSINESTYPE")
				{
					Comm = "select distinct(BT_GROUP) from " + TABLE;
					conn.QueryString = Comm;
					conn.ExecuteQuery();

					for (int i=0; i< conn.GetRowCount(); i++)
					{
						this.DDL_VALUE.Items.Add(new ListItem(conn.GetFieldValue(i,0),conn.GetFieldValue(i,0)));
					}
				} 
				else if(TABLE.Trim() == "RFBRANCH")
				{
					Comm = "select AREA_ID, AREA_NAME from AREA";
					conn.QueryString = Comm;
					conn.ExecuteQuery();
					
					for (int i=0; i< conn.GetRowCount(); i++)
					{
						this.DDL_VALUE.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
					}
				} 
				else
				{
					Comm = "select * from " + TABLE;
					conn.QueryString = Comm;

					try
					{
						conn.ExecuteQuery();
					
						for (int i=0; i< conn.GetRowCount(); i++)
						{
							this.DDL_VALUE.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
						}
					}
					catch { }
			
				}
			}
		}

		private void clearEditParamValueBoxes()
		{
			this.TXT_MAX_VALUE.Text = "";
			this.TXT_MIN_VALUE.Text = "";
			this.TXT_PARAM_SCORE.Text = "";
			this.TXT_PARAM_VALUE_ID.Text = "";
			this.TXT_REMARKS.Text = "";
			RBL_PARAM.Enabled = true;
			RBL_PARAM_LOC.Enabled = true;
			DDL_TPLID.Enabled = true;
			DDL_PARAMID.Enabled = true;
			DDL_VALUE.Enabled = true;
			TXT_PARAM_VALUE_ID.Enabled = true;
			TXT_MIN_VALUE.Enabled = true;
			TXT_MAX_VALUE.Enabled = true;
			TXT_PARAM_SCORE.Enabled = true;
			TXT_REMARKS.Enabled = true;
			
			try 
			{
				this.DDL_TPLID.SelectedItem.Selected = false;
				this.DDL_PARAMID.SelectedItem.Selected = false;
				this.DDL_VALUE.SelectedItem.Selected = false;
			}
			catch {}

		}

		private string ExchgPoint(string str)
		{
			return str.Replace(".",",").Trim();
		}

		private string checkComma(string str)
		{
			return str.Replace(",", ".").Trim();
		}

		private void ViewExistingParameterValueData()
		{
			if (RBL_PARAM.SelectedValue != "1")
			{
				tr_grd_value_exist.Visible = true;
				tr_grd_other_exitst.Visible = false;
				TR_REMARKS.Visible = true;

				LBL_PARAMVALID.Text = "Parameter Value ID";
				
				conn.QueryString = "SELECT * FROM VW_MANDIRI_PARAM_VALUE WHERE PARAM_PRM = '"+ RBL_PARAM_LOC.SelectedValue +"' ";

				if (DDL_TPLID.SelectedValue != "")
					conn.QueryString += "AND SCOTPLID = '" + DDL_TPLID.SelectedValue + "' ";
				if (DDL_PARAMID.SelectedValue != "")
					conn.QueryString += "AND PARAM_ID = '" + DDL_PARAMID.SelectedValue + "' ";
				 
				conn.QueryString += "ORDER BY SCOTPLID ASC";
				conn.ExecuteQuery();
				


				DataTable data = new DataTable();
				data = conn.GetDataTable().Copy();
				Datagrid1.DataSource = data;
		
				try
				{
					Datagrid1.DataBind();
				} 
				catch 
				{
					this.Datagrid1.CurrentPageIndex = Datagrid1.PageCount - 1;
					Datagrid1.DataBind();
				}

		
				for (int i=0; i < Datagrid1.Items.Count; i++)
				{
					Datagrid1.Items[i].Cells[4].Text	= ExchgPoint(Datagrid1.Items[i].Cells[4].Text);
					Datagrid1.Items[i].Cells[5].Text	= ExchgPoint(Datagrid1.Items[i].Cells[5].Text);
				}
			}
			else
			{
				tr_grd_other_exitst.Visible = true;
				tr_grd_value_exist.Visible = false;
				TR_REMARKS.Visible = false;

				LBL_PARAMVALID.Text = "Parameter Other ID";

				conn.QueryString = "SELECT * FROM VW_MANDIRI_PARAM_OTHER WHERE PARAM_PRM = '"+ RBL_PARAM_LOC.SelectedValue +"' ";

				if (DDL_TPLID.SelectedValue != "")
					conn.QueryString += "AND SCOTPLID = '" + DDL_TPLID.SelectedValue + "' ";
				if (DDL_PARAMID.SelectedValue != "")
					conn.QueryString += "AND PARAM_ID = '" + DDL_PARAMID.SelectedValue + "' ";

				conn.QueryString += "ORDER BY SCOTPLID ASC";
				conn.ExecuteQuery();

				DataTable data = new DataTable();
				data = conn.GetDataTable().Copy();
				Datagrid4.DataSource = data;
		
				try
				{
					Datagrid4.DataBind();
				} 
				catch 
				{
					this.Datagrid4.CurrentPageIndex = Datagrid4.PageCount - 1;
					Datagrid4.DataBind();
				}
			}
		}



		private void ViewPendingParameterValueData()
		{
			if (RBL_PARAM.SelectedValue != "1")
			{
				tr_grd_value.Visible = true;
				tr_grd_other.Visible = false;

				LBL_PARAMVALID.Text = "Parameter Value ID";

				conn.QueryString = "SELECT * FROM VW_TMANDIRI_PARAM_VALUE WHERE PARAM_PRM = '"+ RBL_PARAM_LOC.SelectedValue +"' ";

				if (DDL_TPLID.SelectedValue != "")
					conn.QueryString += "AND SCOTPLID = '" + DDL_TPLID.SelectedValue + "' ";
				if (DDL_PARAMID.SelectedValue != "")
					conn.QueryString += "AND PARAM_ID = '" + DDL_PARAMID.SelectedValue + "' ";

				conn.QueryString += "ORDER BY SCOTPLID ASC";
				conn.ExecuteQuery();
				
				DataTable data = new DataTable();
				data = conn.GetDataTable().Copy();
				Datagrid2.DataSource = data;
		
				try
				{
					Datagrid2.DataBind();
				} 
				catch 
				{
					this.Datagrid2.CurrentPageIndex = Datagrid2.PageCount - 1;
					Datagrid2.DataBind();
				}

		
				for (int i=0; i < Datagrid2.Items.Count; i++)
				{
					Datagrid2.Items[i].Cells[4].Text	= ExchgPoint(Datagrid2.Items[i].Cells[4].Text);
					Datagrid2.Items[i].Cells[5].Text	= ExchgPoint(Datagrid2.Items[i].Cells[5].Text);
				}
			}
			else
			{
				tr_grd_other.Visible = true;
				tr_grd_value.Visible = false;

				LBL_PARAMVALID.Text = "Parameter Other ID";

				conn.QueryString = "SELECT * FROM VW_TMANDIRI_PARAM_OTHER WHERE PARAM_PRM = '"+ RBL_PARAM_LOC.SelectedValue +"' ";

				if (DDL_TPLID.SelectedValue != "")
					conn.QueryString += "AND SCOTPLID = '" + DDL_TPLID.SelectedValue + "' ";
				if (DDL_PARAMID.SelectedValue != "")
					conn.QueryString += "AND PARAM_ID = '" + DDL_PARAMID.SelectedValue + "' ";

				conn.QueryString += "ORDER BY SCOTPLID ASC";
				conn.ExecuteQuery();

				DataTable data = new DataTable();
				data = conn.GetDataTable().Copy();
				Datagrid3.DataSource = data;
		
				try
				{
					Datagrid3.DataBind();
				} 
				catch 
				{
					this.Datagrid3.CurrentPageIndex = Datagrid3.PageCount - 1;
					Datagrid3.DataBind();
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
			this.DGR_EXISTING_VALUE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_VALUE_ItemCommand);
			this.DGR_EXISTING_VALUE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_VALUE_PageIndexChanged);
			this.DGR_REQUEST_VALUE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_VALUE_ItemCommand);
			this.DGR_REQUEST_VALUE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_VALUE_PageIndexChanged);
			this.Datagrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_ItemCommand);
			this.Datagrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid1_PageIndexChanged);
			this.Datagrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_ItemCommand);
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);
			this.Datagrid4.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid4_ItemCommand);
			this.Datagrid4.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid4_PageIndexChanged);
			this.Datagrid3.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid3_ItemCommand);
			this.Datagrid3.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid3_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_VALUE_Click(object sender, System.EventArgs e)
		{
			
			conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_MAKER '"+TXT_TPLID.Text+"', '"+TXT_TPLDESC.Text+"', " +
				"'"+LBL_SAVEMODE.Text+"'";
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
			bindData2();
		}

		private void DGR_EXISTING_VALUE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TXT_TPLID.Enabled = false;
					TXT_TPLID.Text = e.Item.Cells[0].Text.Trim();
					conn.QueryString="SELECT * FROM MANDIRI_SCORING_TEMPLATE WHERE SCOTPLID='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					
					TXT_TPLDESC.Text = conn.GetFieldValue("SCOTPLDESC");
					LBL_SAVEMODE.Text = "0";
										
					break;
				case "Delete":
					conn.QueryString="SELECT * FROM MANDIRI_SCORING_TTEMPLATE WHERE SCOTPLID='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="UPDATE MANDIRI_SCORING_TTEMPLATE SET CH_STA='2' WHERE SCOTPLID='"+e.Item.Cells[0].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_MAKER '" +
							e.Item.Cells[0].Text.Trim()+ "','" + e.Item.Cells[1].Text.Trim() + "','2'" ;
						conn.ExecuteQuery();
					}
					bindData2();
					break;
				default:
					
					break;
			} 
		}

		private void DGR_EXISTING_VALUE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING_VALUE.CurrentPageIndex = e.NewPageIndex;
			bindData1();
		}

		private void DGR_REQUEST_VALUE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST_VALUE.CurrentPageIndex = e.NewPageIndex;
			bindData2();
		}

		private void DGR_REQUEST_VALUE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":

					conn.QueryString="SELECT * FROM MANDIRI_SCORING_TTEMPLATE WHERE SCOTPLID='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();

					string status = e.Item.Cells[2].Text;
					if(status=="DELETE")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						TXT_TPLID.Enabled = false;
						TXT_TPLID.Text = conn.GetFieldValue("SCOTPLID");
						TXT_TPLDESC.Text = conn.GetFieldValue("SCOTPLDESC");
						
						if(e.Item.Cells[2].Text=="UPDATE")
						{
							LBL_SAVEMODE.Text = "0";
						}
						if(e.Item.Cells[2].Text=="INSERT")
						{
							LBL_SAVEMODE.Text = "1";
						}
					}
					break;
				case "Delete":
					string CODE = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM MANDIRI_SCORING_TTEMPLATE WHERE SCOTPLID = '"+CODE+"'";
					conn.ExecuteQuery();
					bindData2();
					break;
				default:
					break;			
			}  

		}

		protected void BTN_CANCEL_VALUE_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		protected void DDL_PARAMID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LBL_PRMVALNAME.Text = DDL_PARAMID.SelectedItem.ToString();
			fillvalue();
			ViewExistingParameterValueData();
			ViewPendingParameterValueData();
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			clearEditParamValueBoxes();
			ViewExistingParameterValueData();
			ViewPendingParameterValueData();
			GlobalTools.SetFocus(this,DDL_TPLID);
		}

		protected void BTN_SAVE_LIST_Click(object sender, System.EventArgs e)
		{

			if (RBL_PARAM.SelectedValue != "1")
			{
				int rowDGR = Datagrid2.Items.Count + 1 ; 
				LBL_SEQ.Text = rowDGR.ToString();
			}
			else
			{
				int rowDGR = Datagrid3.Items.Count + 1 ; 
				LBL_SEQ.Text = rowDGR.ToString();
			}

			
			if (this.TR_DDL_VALUE.Visible == false)
			{
				this.LBL_MIN.Text = this.TXT_MIN_VALUE.Text.Trim();
				this.LBL_MAX.Text = this.TXT_MAX_VALUE.Text.Trim();
			} 
			else if ((this.TR_DDL_VALUE.Visible == true) && (RBL_PARAM.SelectedValue != "1"))
			{
				this.LBL_MIN.Text = this.DDL_VALUE.SelectedValue.ToString();
				this.LBL_MAX.Text = this.DDL_VALUE.SelectedItem.ToString();
			}

			if (RBL_PARAM.SelectedValue != "1")
			{
				LBL_PRMVALNAME.Text = DDL_PARAMID.SelectedItem.ToString();

				conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_VALUE_MAKER '"+ LBL_SAVEMODE2.Text.Trim() +"', '"+
					DDL_PARAMID.SelectedValue +"', '" + DDL_TPLID.SelectedValue + "', '" + TXT_PARAM_VALUE_ID.Text  +"', '"+
					this.LBL_PRMVALNAME.Text +"', '"+  this.checkComma(this.LBL_MIN.Text) +"', '"+ 
					this.checkComma(this.LBL_MAX.Text) +"', '"+ this.checkComma(this.TXT_PARAM_SCORE.Text) +"', '"+
					this.LBL_SEQ.Text.Trim() +"', '" + TXT_REMARKS.Text + "'"; 
				
			}
			else
			{
				LBL_PRMVALNAME.Text = DDL_PARAMID.SelectedItem.ToString();

				conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_OTHER_MAKER '"+ LBL_SAVEMODE3.Text.Trim() +"', '"+
					DDL_PARAMID.SelectedValue +"', '" + DDL_TPLID.SelectedValue + "', '" + TXT_PARAM_VALUE_ID.Text  +"', '"+
					this.LBL_PRMVALNAME.Text +"', '" + this.checkComma(this.TXT_PARAM_SCORE.Text) +"', '"+
					this.LBL_SEQ.Text.Trim() +"'"; 
			}
			
			try
			{
				conn.ExecuteNonQuery();
				if (RBL_PARAM.SelectedValue != "1")
					LBL_SAVEMODE2.Text = "1";
				else
					LBL_SAVEMODE3.Text = "1";
			} 
			catch (Exception ex)
			{
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)		//method Connection.ExecuteNonQuery() add this msg on exception 
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this, errmsg);
			}
			
			clearEditParamValueBoxes();
			ViewPendingParameterValueData();
			GlobalTools.SetFocus(this,DDL_TPLID);
			
			
		}

		protected void DDL_TPLID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingParameterValueData();
			ViewPendingParameterValueData();
		}

		protected void RBL_PARAM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillparam();
			ViewExistingParameterValueData();
			ViewPendingParameterValueData();
		}

		private void Datagrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			Datagrid2.CurrentPageIndex = e.NewPageIndex;
			ViewPendingParameterValueData();
		}

		protected void RBL_PARAM_LOC_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillparam();
			ViewExistingParameterValueData();
			ViewPendingParameterValueData();	
		}

		private void Datagrid3_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			Datagrid3.CurrentPageIndex = e.NewPageIndex;
			ViewPendingParameterValueData();
		}

		private void Datagrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					

					conn.QueryString = "SELECT * from TMANDIRI_PARAM_VALUE " + 
						"WHERE PARAM_VALUE_ID = '"+e.Item.Cells[2].Text.Trim()+"' " +
						"AND PARAM_ID = '"+e.Item.Cells[1].Text.Trim()+"' and SCOTPLID = '"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();

					string scotlpid = conn.GetFieldValue("SCOTPLID"), paramid = conn.GetFieldValue("PARAM_ID"),
					paramvaluename = conn.GetFieldValue("MIN_VALUE"), paramvalueid = conn.GetFieldValue("PARAM_VALUE_ID"),
					minvalue = conn.GetFieldValue("MIN_VALUE"), maxvalue = conn.GetFieldValue("MAX_VALUE"),
					paramscore = conn.GetFieldValue("PARAM_SCORE"), remarks = conn.GetFieldValue("REMARKS"),
					status = e.Item.Cells[10].Text;

					if(status=="DELETE")
					{
						LBL_SAVEMODE2.Text = "1";
					}
					else
					{

						DDL_TPLID.SelectedValue = scotlpid;
						DDL_PARAMID.SelectedValue = paramid;

						fillvalue();

						DDL_VALUE.SelectedValue = paramvaluename;
						TXT_PARAM_VALUE_ID.Text = paramvalueid;
						TXT_MIN_VALUE.Text = minvalue;
						TXT_MAX_VALUE.Text = maxvalue;
						TXT_PARAM_SCORE.Text = paramscore;
						TXT_REMARKS.Text = remarks;
						
						if(e.Item.Cells[10].Text=="UPDATE")
						{
							LBL_SAVEMODE2.Text = "0";
						}
						if(e.Item.Cells[10].Text=="INSERT")
						{
							LBL_SAVEMODE2.Text = "1";
						}

						DDL_TPLID.Enabled = false;
						DDL_PARAMID.Enabled = false;
						DDL_VALUE.Enabled = false;
						TXT_PARAM_VALUE_ID.Enabled = false;
						RBL_PARAM.Enabled = false;
						RBL_PARAM_LOC.Enabled = false;					
					}
					break;
				case "delete":
					
					conn.QueryString = "DELETE FROM TMANDIRI_PARAM_VALUE " +
					"WHERE PARAM_VALUE_ID = '"+e.Item.Cells[2].Text.Trim()+"' " +
					"AND PARAM_ID = '"+e.Item.Cells[1].Text.Trim()+"' and SCOTPLID = '"+e.Item.Cells[0].Text.Trim()+"'";

					conn.ExecuteQuery();

					ViewPendingParameterValueData();
					break;
				default:
					break;			
			}  
		}

		private void Datagrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			Datagrid1.CurrentPageIndex = e.NewPageIndex;
			ViewExistingParameterValueData();
		}

		private void Datagrid4_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			Datagrid4.CurrentPageIndex = e.NewPageIndex;
			ViewExistingParameterValueData();
		}

		private void Datagrid3_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					

					conn.QueryString = "SELECT * from TMANDIRI_PARAM_OTHER " + 
						"WHERE PARAM_OTHER_ID = '"+e.Item.Cells[2].Text.Trim()+"' " +
						"AND PARAM_ID = '"+e.Item.Cells[1].Text.Trim()+"' and SCOTPLID = '"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();

					string scotlpid = conn.GetFieldValue("SCOTPLID"), paramid = conn.GetFieldValue("PARAM_ID"),
						paramvalueid = conn.GetFieldValue("PARAM_OTHER_ID"),
						paramothervalue = conn.GetFieldValue("PARAM_OTHER_VALUE"), status = e.Item.Cells[7].Text;

					if(status=="DELETE")
					{
						LBL_SAVEMODE3.Text = "1";
					}
					else
					{

						DDL_TPLID.SelectedValue = scotlpid;
						DDL_PARAMID.SelectedValue = paramid;

						fillvalue();

						TXT_PARAM_VALUE_ID.Text = paramvalueid;
						TXT_PARAM_SCORE.Text = paramothervalue;

						
						if(e.Item.Cells[7].Text=="UPDATE")
						{
							LBL_SAVEMODE3.Text = "0";
						}
						if(e.Item.Cells[7].Text=="INSERT")
						{
							LBL_SAVEMODE3.Text = "1";
						}

						DDL_TPLID.Enabled = false;
						DDL_PARAMID.Enabled = false;
						TXT_PARAM_VALUE_ID.Enabled = false;
						RBL_PARAM.Enabled = false;
						RBL_PARAM_LOC.Enabled = false;					
					}
					break;
				case "delete":
					
					conn.QueryString = "DELETE FROM TMANDIRI_PARAM_OTHER " +
						"WHERE PARAM_OTHER_ID = '"+e.Item.Cells[2].Text.Trim()+"' " +
						"AND PARAM_ID = '"+e.Item.Cells[1].Text.Trim()+"' and SCOTPLID = '"+e.Item.Cells[0].Text.Trim()+"'";

					conn.ExecuteQuery();
					
					ViewPendingParameterValueData();
					break;
				default:
					break;			
			}  

		}

		private void Datagrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					
					conn.QueryString = "SELECT * from MANDIRI_PARAM_VALUE " + 
						"WHERE PARAM_VALUE_ID = '"+e.Item.Cells[2].Text.Trim()+"' " +
						"AND PARAM_ID = '"+e.Item.Cells[1].Text.Trim()+"' and SCOTPLID = '"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();

					string scotlpid = conn.GetFieldValue("SCOTPLID"), paramid = conn.GetFieldValue("PARAM_ID"),
						paramvaluename = conn.GetFieldValue("MIN_VALUE"), paramvalueid = conn.GetFieldValue("PARAM_VALUE_ID"),
						minvalue = conn.GetFieldValue("MIN_VALUE"), maxvalue = conn.GetFieldValue("MAX_VALUE"),
						paramscore = conn.GetFieldValue("PARAM_SCORE"), remarks = conn.GetFieldValue("REMARKS");

					DDL_TPLID.SelectedValue = scotlpid;
					DDL_PARAMID.SelectedValue = paramid;

					fillvalue();

					DDL_VALUE.SelectedValue = paramvaluename;
					TXT_PARAM_VALUE_ID.Text = paramvalueid;
					TXT_MIN_VALUE.Text = minvalue;
					TXT_MAX_VALUE.Text = maxvalue;
					TXT_PARAM_SCORE.Text = paramscore;
					TXT_REMARKS.Text = remarks;
					LBL_SAVEMODE2.Text = "0";

					DDL_TPLID.Enabled = false;
					DDL_PARAMID.Enabled = false;
					DDL_VALUE.Enabled = false;
					TXT_PARAM_VALUE_ID.Enabled = false;
					RBL_PARAM.Enabled = false;
					RBL_PARAM_LOC.Enabled = false;					
					
					break;
				case "delete":
					
					conn.QueryString = "SELECT * from TMANDIRI_PARAM_VALUE " + 
						"WHERE PARAM_VALUE_ID = '"+e.Item.Cells[2].Text.Trim()+"' " +
						"AND PARAM_ID = '"+e.Item.Cells[1].Text.Trim()+"' and SCOTPLID = '"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="UPDATE TMANDIRI_PARAM_VALUE SET CH_STA='2' " +
							"WHERE PARAM_VALUE_ID = '"+e.Item.Cells[2].Text.Trim()+"' " +
							"AND PARAM_ID = '"+e.Item.Cells[1].Text.Trim()+"' and SCOTPLID = '"+e.Item.Cells[0].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_VALUE_MAKER '2', '"+
							e.Item.Cells[1].Text.Trim() +"', '" + e.Item.Cells[0].Text.Trim() + "', '" + e.Item.Cells[2].Text.Trim() +"', '"+
							e.Item.Cells[3].Text.Trim() +"', '"+  this.checkComma(e.Item.Cells[4].Text.Trim()) +"', '"+ 
							this.checkComma(e.Item.Cells[5].Text.Trim()) +"', '"+ this.checkComma(e.Item.Cells[6].Text.Trim()) +"', '"+
							this.LBL_SEQ.Text.Trim() +"', '"+e.Item.Cells[7].Text.Trim()+"'"; 
						conn.ExecuteQuery();
					}

					ViewPendingParameterValueData();
					break;
				default:
					break;			
			}  
		}

		private void Datagrid4_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					
					conn.QueryString = "SELECT * from MANDIRI_PARAM_OTHER " + 
						"WHERE PARAM_OTHER_ID = '"+e.Item.Cells[2].Text.Trim()+"' " +
						"AND PARAM_ID = '"+e.Item.Cells[1].Text.Trim()+"' and SCOTPLID = '"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();

					string scotlpid = conn.GetFieldValue("SCOTPLID"), paramid = conn.GetFieldValue("PARAM_ID"),
						paramvalueid = conn.GetFieldValue("PARAM_OTHER_ID"),
						paramothervalue = conn.GetFieldValue("PARAM_OTHER_VALUE");

					DDL_TPLID.SelectedValue = scotlpid;
					DDL_PARAMID.SelectedValue = paramid;			

					fillvalue();

					LBL_SAVEMODE3.Text = "0";
					TXT_PARAM_VALUE_ID.Text = paramvalueid;
					TXT_PARAM_SCORE.Text = paramothervalue;

					DDL_TPLID.Enabled = false;
					DDL_PARAMID.Enabled = false;
					TXT_PARAM_VALUE_ID.Enabled = false;
					RBL_PARAM.Enabled = false;
					RBL_PARAM_LOC.Enabled = false;					
					
					break;
				case "delete":
					
					conn.QueryString = "SELECT * from TMANDIRI_PARAM_OTHER " + 
						"WHERE PARAM_OTHER_ID = '"+e.Item.Cells[2].Text.Trim()+"' " +
						"AND PARAM_ID = '"+e.Item.Cells[1].Text.Trim()+"' and SCOTPLID = '"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="UPDATE TMANDIRI_PARAM_OTHER SET CH_STA='2' " +
							"WHERE PARAM_OTHER_ID = '"+e.Item.Cells[2].Text.Trim()+"' " +
							"AND PARAM_ID = '"+e.Item.Cells[1].Text.Trim()+"' and SCOTPLID = '"+e.Item.Cells[0].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_OTHER_MAKER '2', '"+
							e.Item.Cells[1].Text.Trim() +"', '" + e.Item.Cells[0].Text.Trim() + "', '" + e.Item.Cells[2].Text.Trim() +"', '"+
							e.Item.Cells[3].Text.Trim() +"', '" + this.checkComma(e.Item.Cells[4].Text.Trim()) +"', '"+
							this.LBL_SEQ.Text.Trim() +"'"; 
						conn.ExecuteQuery();
					}

					ViewPendingParameterValueData();
					break;
				default:
					break;			
			}  

		}

	
	}
}
