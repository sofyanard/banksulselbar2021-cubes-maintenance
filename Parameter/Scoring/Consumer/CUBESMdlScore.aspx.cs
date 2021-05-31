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
	/// Summary description for CUBESMdlScore.
	/// </summary>
	public partial class CUBESMdlScore : System.Web.UI.Page
	{
		private Connection conn, conn2;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1";
				LBL_EDITMODE.Text = Request.QueryString["editmode"];
				if (LBL_EDITMODE.Text == "1")
					LBL_TITLE.Text = "Model Scoring Maker";
				else if (LBL_EDITMODE.Text == "2")
					LBL_TITLE.Text = "Model Range Maker";
				else
					LBL_TITLE.Text = "Model Limit Maker";
			}
			initInputArea();
				
			bindData1();
			bindData2();
			
			BTN_SAVE_VALUE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
		}

		private void SetDBConn2()
		{
			conn2.QueryString = "SELECT * FROM RFMODULE WHERE MODULEID = '" + Request.QueryString["ModuleId"] + "'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void fillModelTable(string dimQ, string tplQ)
		{
			DataTable dtDim;
			DropDownList ddl;
			string fieldtitle, ddlquery;
			int row;

			conn.QueryString = dimQ;
			conn.ExecuteQuery();
			dtDim = conn.GetDataTable();
					
			// fill the table! 
			for (row = 0; row < dtDim.Rows.Count; row++)
			{
				fieldtitle = conn.GetFieldValue(dtDim, row, 0);	//DIMDESC 
				ddl = new DropDownList();
				ddl.ID = "ddl" + row.ToString();
				ddl.CssClass = "mandatory";
				ddlquery = "SELECT " + conn.GetFieldValue(dtDim, row, 1) +
					", " + conn.GetFieldValue(dtDim, row, 2) +
					" FROM " + conn.GetFieldValue(dtDim, row, 3);
				GlobalTools.fillRefList(ddl, ddlquery, true, conn);
				TBL_MODEL.Rows.Add(new TableRow());
				//filling content for <TD class="TDBGColor1" style="WIDTH: 300px">[FieldTitle]</TD>
				TBL_MODEL.Rows[row].Cells.Add(new TableCell());	
				TBL_MODEL.Rows[row].Cells[0].CssClass = "TDBGColor1";
				TBL_MODEL.Rows[row].Cells[0].Style.Add("WIDTH", "300px");
				TBL_MODEL.Rows[row].Cells[0].Text = fieldtitle;
				//filling content for <TD style="WIDTH: 7px"></TD>
				TBL_MODEL.Rows[row].Cells.Add(new TableCell());	
				TBL_MODEL.Rows[row].Cells[1].Style.Add("WIDTH", "7px");
				//filling content for <TD class="TDBGColorValue">[FieldControl]</TD>
				TBL_MODEL.Rows[row].Cells.Add(new TableCell());	
				TBL_MODEL.Rows[row].Cells[2].CssClass = "TDBGColorValue";
				TBL_MODEL.Rows[row].Cells[2].Controls.Add(ddl);
			}
			//another row for the template id 
			ddl = new DropDownList();
			ddl.ID = "ddl" + row.ToString();
			ddl.CssClass = "mandatory";
			GlobalTools.fillRefList(ddl, tplQ, true, conn);
			TBL_MODEL.Rows.Add(new TableRow());
			//filling content for <TD class="TDBGColor1" style="WIDTH: 300px">[FieldTitle]</TD>
			TBL_MODEL.Rows[row].Cells.Add(new TableCell());	
			TBL_MODEL.Rows[row].Cells[0].CssClass = "TDBGColor1";
			TBL_MODEL.Rows[row].Cells[0].Style.Add("WIDTH", "300px");
			TBL_MODEL.Rows[row].Cells[0].Text = "Template ID";
			//filling content for <TD style="WIDTH: 7px"></TD>
			TBL_MODEL.Rows[row].Cells.Add(new TableCell());	
			TBL_MODEL.Rows[row].Cells[1].Style.Add("WIDTH", "7px");
			//filling content for <TD class="TDBGColorValue">[FieldControl]</TD>
			TBL_MODEL.Rows[row].Cells.Add(new TableCell());	
			TBL_MODEL.Rows[row].Cells[2].CssClass = "TDBGColorValue";
			TBL_MODEL.Rows[row].Cells[2].Controls.Add(ddl);
		}

		/// <summary>
		/// This function generate runtime input fields using table mandiri_dimensi_item 
		/// </summary>
		/// <param name="mode">1: scoring_model, 2: range_model, 3: limit_model </param>
		private void initInputArea()
		{
			string dimQ, tplQ;
			switch(LBL_EDITMODE.Text)
			{
				case "1":
					dimQ = "SELECT DIMDESC, REFFIELDID, REFFIELDDESC, REFTABLE " +
						"FROM VW_MANDIRI_DIMENSI_ITEM WHERE SCOFLAG = '1' ORDER BY DIMID ";
					tplQ = "SELECT SCOTPLID, SCOTPLDESC FROM VW_MANDIRI_SCORING_TEMPLATE ";
					fillModelTable(dimQ,tplQ);
					break;
				case "2":
					dimQ = "SELECT DIMDESC, REFFIELDID, REFFIELDDESC, REFTABLE " +
						"FROM VW_MANDIRI_DIMENSI_ITEM WHERE RNGFLAG = '1' ORDER BY DIMID ";
					tplQ = "SELECT RNGTPLID, RNGTPLDESC FROM VW_MANDIRI_RANGE_TEMPLATE ";
					fillModelTable(dimQ,tplQ);
					break;
				case "3":
					dimQ = "SELECT DIMDESC, REFFIELDID, REFFIELDDESC, REFTABLE " +
						"FROM VW_MANDIRI_DIMENSI_ITEM WHERE LMTFLAG = '1' ORDER BY DIMID ";
					tplQ = "SELECT LMTTPLID, LMTTPLDESC FROM VW_MANDIRI_LIMIT_TEMPLATE ";
					fillModelTable(dimQ,tplQ);
					break;
				default:
					break;
			}
		}
		

		/// <summary>
		/// This function binds existing data to the grid with columns define runtime 
		/// The columns generated by stored procedure with the following rule: 
		///			field01_id, field01_desc, field02_id, field02_desc, ..., tplid, tpldesc
		/// </summary>
		/// <param name="mode">1: scoring_model, 2: range_model, 3: limit_model </param>
		private void bindData1()
		{
			bindData1(false);
		}
		private void bindData1(bool cleartable)
		{
			if (cleartable)
				TBL_EXISTING.Rows.Clear();

			int i, j, row;
			conn.QueryString = "EXEC PARAM_SCORING_LIST_MANDIRI_MODEL '" + LBL_EDITMODE.Text + "'";
			conn.ExecuteQuery();
			DataTable dt = conn.GetDataTable();

			//creating header row
			row = 0;
			TBL_EXISTING.Rows.Add(new TableRow());
			TBL_EXISTING.Rows[row].CssClass = "tdSmallHeader";
			for(j = 0; j < dt.Columns.Count; j++)
			{
				TBL_EXISTING.Rows[row].Cells.Add(new TableCell());
				TBL_EXISTING.Rows[row].Cells[j].Text = dt.Columns[j].ColumnName;
				if (j % 2 == 0)			//even columns contain id, odd columns contains desc
					TBL_EXISTING.Rows[row].Cells[j].Visible = false;
			}
			TBL_EXISTING.Rows[row].Cells.Add(new TableCell());
			TBL_EXISTING.Rows[row].Cells[j].Text = "Function";

			//creating data rows 
			for(i = 0; i < dt.Rows.Count; i++)
			{
				row = i + 1;
				TBL_EXISTING.Rows.Add(new TableRow());
				if (i % 2 == 1)
					TBL_EXISTING.Rows[row].CssClass = "tblalternating";
				for(j = 0; j < dt.Columns.Count; j++)
				{
					TBL_EXISTING.Rows[row].Cells.Add(new TableCell());
					TBL_EXISTING.Rows[row].Cells[j].Text = dt.Rows[i][j].ToString();
					if (j % 2 == 0)			//even columns contain id, odd columns contains desc
						TBL_EXISTING.Rows[row].Cells[j].Visible = false;
				}
				//function link
				LinkButton lbEdit = new LinkButton();
				lbEdit.ID = "xe" + row.ToString();
				lbEdit.Text = "Edit";
				lbEdit.Click +=new EventHandler(lb_Click);
				Label lblSpace = new Label();
				lblSpace.Text = "  ";
				LinkButton lbDel = new LinkButton();
				lbDel.ID = "xd" + row.ToString();
				lbDel.Text = "Delete";
				lbDel.Click +=new EventHandler(lb_Click);
				TBL_EXISTING.Rows[row].Cells.Add(new TableCell());
				TBL_EXISTING.Rows[row].Cells[j].Controls.Add(lbEdit);
				TBL_EXISTING.Rows[row].Cells[j].Controls.Add(lblSpace);
				TBL_EXISTING.Rows[row].Cells[j].Controls.Add(lbDel);
			}
		}

		/// <summary>
		/// This function binds requested data to the grid with columns define runtime 
		/// The columns generated by stored procedure with the following rule: 
		///			field01_id, field01_desc, field02_id, field02_desc, ..., tplid, tpldesc, ch_sta, status_desc
		/// </summary>
		/// <param name="mode">1: scoring_model, 2: range_model, 3: limit_model </param>
		private void bindData2()
		{
			bindData2(false);
		}
		private void bindData2(bool cleartable)
		{
			if (cleartable)
				TBL_REQUEST.Rows.Clear();

			int i, j, row;
			conn.QueryString = "EXEC PARAM_SCORING_LIST_TMANDIRI_MODEL '" + LBL_EDITMODE.Text + "'";
			conn.ExecuteQuery();
			DataTable dt = conn.GetDataTable();

			//creating header row
			row = 0;
			TBL_REQUEST.Rows.Add(new TableRow());
			TBL_REQUEST.Rows[row].CssClass = "tdSmallHeader";
			for(j = 0; j < dt.Columns.Count; j++)
			{
				TBL_REQUEST.Rows[row].Cells.Add(new TableCell());
				TBL_REQUEST.Rows[row].Cells[j].Text = dt.Columns[j].ColumnName;
				if (j % 2 == 0)			//even columns contain id, odd columns contains desc
					TBL_REQUEST.Rows[row].Cells[j].Visible = false;
			}
			TBL_REQUEST.Rows[row].Cells.Add(new TableCell());
			TBL_REQUEST.Rows[row].Cells[j].Text = "Function";

			//creating data rows 
			for(i = 0; i < dt.Rows.Count; i++)
			{
				row = i + 1;
				TBL_REQUEST.Rows.Add(new TableRow());
				if (i % 2 == 1)
					TBL_REQUEST.Rows[row].CssClass = "tblalternating";
				for(j = 0; j < dt.Columns.Count; j++)
				{
					TBL_REQUEST.Rows[row].Cells.Add(new TableCell());
					TBL_REQUEST.Rows[row].Cells[j].Text = dt.Rows[i][j].ToString();
					if (j % 2 == 0)			//even columns contain id, odd columns contains desc
						TBL_REQUEST.Rows[row].Cells[j].Visible = false;
				}
				//function link
				LinkButton lbEdit = new LinkButton();
				lbEdit.ID = "re" + row.ToString();
				lbEdit.Text = "Edit";
				lbEdit.Click +=new EventHandler(lb_Click);
				Label lblSpace = new Label();
				lblSpace.Text = "  ";
				LinkButton lbDel = new LinkButton();
				lbDel.ID = "rd" + row.ToString();
				lbDel.Text = "Delete";
				lbDel.Click +=new EventHandler(lb_Click);
				TBL_REQUEST.Rows[row].Cells.Add(new TableCell());
				TBL_REQUEST.Rows[row].Cells[j].Controls.Add(lbEdit);
				TBL_REQUEST.Rows[row].Cells[j].Controls.Add(lblSpace);
				TBL_REQUEST.Rows[row].Cells[j].Controls.Add(lbDel);
			}
		}

		private void clearEditBoxes()
		{
			LBL_SAVEMODE.Text = "1";
			DropDownList ddl;
			for (int i = 0; i < TBL_MODEL.Rows.Count; i++)
			{
				try
				{
					ddl = (DropDownList) TBL_MODEL.Rows[i].Cells[2].FindControl("ddl" + i.ToString());
					ddl.SelectedIndex = 0;
				} 
				catch {}
			}
			enableControls(true);
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void enableControls(bool mode)
		{
			DropDownList ddl;
			for (int i = 0; i < TBL_MODEL.Rows.Count - 1; i++)		//last control is always modifiable 
			{
				try
				{
					ddl = (DropDownList) TBL_MODEL.Rows[i].Cells[2].FindControl("ddl" + i.ToString());
					ddl.Enabled = mode;
				} 
				catch {}
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

		}
		#endregion

		protected void BTN_SAVE_VALUE_Click(object sender, System.EventArgs e)
		{
			string argStr = "";		// dynamic fields leave no choice but to pad the values under one string
									// this string is to be parsed back in the stored procedure that accept it
			DropDownList ddl;
			for (int i = 0; i < TBL_MODEL.Rows.Count; i++)
			{
				try
				{
					ddl = (DropDownList) TBL_MODEL.Rows[i].Cells[2].FindControl("ddl" + i.ToString());
					argStr += ddl.SelectedValue + ";";
				} 
				catch {}
			}
			Response.Write("<!-- " + argStr + " --> ");
			conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_MODEL_MAKER '"+ LBL_EDITMODE.Text + "', '" + LBL_SAVEMODE.Text.Trim() +"', '"+ argStr + "'"; 
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
		}

		protected void BTN_CANCEL_VALUE_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		private void lb_Click(object sender, EventArgs e)
		{
			//event vars
			LinkButton lb = (LinkButton) sender;
			string tabcode = lb.ID.Substring(0,1), btncode = lb.ID.Substring(1,1), id = "<null>";
			int row = int.Parse(lb.ID.Substring(2));

			//command vars
			DropDownList ddl;
			string argStr;
			clearEditBoxes();

			try
			{
				switch (tabcode)
				{
					case "x":						//button from Existing Table
						id = TBL_EXISTING.Rows[row].Cells[0].Text;
						if (btncode == "e")			//edit button
						{
							LBL_SAVEMODE.Text = "0";
							for (int i = 0; i < TBL_MODEL.Rows.Count; i++)
							{
								//try
								//{
								ddl = (DropDownList) TBL_MODEL.Rows[i].Cells[2].FindControl("ddl" + i.ToString());
								//0:1 1:3 2:5 3:7
								ddl.SelectedValue = TBL_EXISTING.Rows[row].Cells[i * 2].Text;
								//} 
								//catch {}
							}
							enableControls(false);
						}
						else if (btncode == "d")	//delete button
						{
							argStr = "";
							for (int i = 0; i < TBL_EXISTING.Rows[row].Cells.Count - 1; i += 2)
							{
								try
								{
									argStr += TBL_EXISTING.Rows[row].Cells[i].Text + ";";
								} 
								catch {}
							}
							conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_MODEL_MAKER '"+ LBL_EDITMODE.Text + "', '2', '"+ argStr + "'"; 
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
							bindData2(true);
						}
						break;

					case "r":						//button from Request Table
						id = TBL_REQUEST.Rows[row].Cells[0].Text;
						if (btncode == "e")			//edit button
						{
							LBL_SAVEMODE.Text = TBL_REQUEST.Rows[row].Cells[TBL_REQUEST.Rows[row].Cells.Count-3].Text.Trim();
							if (LBL_SAVEMODE.Text.Trim() == "2")
							{
								LBL_SAVEMODE.Text = "1";
								break;
							}
					
							for (int i = 0; i < TBL_MODEL.Rows.Count; i++)
							{
								try
								{
									ddl = (DropDownList) TBL_MODEL.Rows[i].Cells[2].FindControl("ddl" + i.ToString());
									//0:1 1:3 2:5 3:7
									ddl.SelectedValue = TBL_REQUEST.Rows[row].Cells[i * 2].Text;
								} 
								catch {}
							}
							enableControls(false);
						}
						else if (btncode == "d")	//delete button
						{
							argStr = "";
							for (int i = 0; i < TBL_REQUEST.Rows[row].Cells.Count - 4; i += 2)
							{
								try
								{
									argStr += TBL_REQUEST.Rows[row].Cells[i].Text + ";";
								} 
								catch {}
							}
							conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_MODEL_MAKER '" + LBL_EDITMODE.Text + "', '9', '"+ argStr + "'"; 
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
							bindData2(true);
						}
						break;

					default:
						break;
				}
			} 
			catch {}
			GlobalTools.popMessage(this, lb.ID + "  " + row.ToString() + "  " + id);
		}

	}
}
