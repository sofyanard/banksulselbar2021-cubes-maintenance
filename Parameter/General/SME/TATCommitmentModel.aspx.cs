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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for TATCommitmentModel.
	/// </summary>
	public partial class TATCommitmentModel : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			initInputArea();
				
			bindData1();
			bindData2();
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

		private void initInputArea()
		{
			string dimQ, tplQ;
			
			dimQ = "SELECT DIMDESC, REFFIELDID, REFFIELDDESC, REFTABLE " +
				"FROM VW_RF_TAT_COMMIT ORDER BY DIMID ";
			tplQ = "SELECT DISTINCT FLOW_NAME, FLOW_NAME FROM RF_TAT ORDER BY FLOW_NAME";
			fillModelTable(dimQ, tplQ);		
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
				ddlquery = "SELECT DISTINCT " + conn.GetFieldValue(dtDim, row, 1) +
					", " + conn.GetFieldValue(dtDim, row, 2) +
					" FROM " + conn.GetFieldValue(dtDim, row, 3);
				GlobalTools.fillRefList(ddl, ddlquery, true, conn);
				TBL_MODEL.Rows.Add(new TableRow());
				TBL_MODEL.Rows[row].Cells.Add(new TableCell());	
				TBL_MODEL.Rows[row].Cells[0].CssClass = "TDBGColor1";
				TBL_MODEL.Rows[row].Cells[0].Style.Add("WIDTH", "300px");
				TBL_MODEL.Rows[row].Cells[0].Text = fieldtitle;
				TBL_MODEL.Rows[row].Cells.Add(new TableCell());	
				TBL_MODEL.Rows[row].Cells[1].Style.Add("WIDTH", "7px");
				TBL_MODEL.Rows[row].Cells.Add(new TableCell());	
				TBL_MODEL.Rows[row].Cells[2].CssClass = "TDBGColorValue";
				TBL_MODEL.Rows[row].Cells[2].Controls.Add(ddl);
			}

			//another row for the template id 
			ddl = new DropDownList();
			ddl.ID = "ddl" + row.ToString();
			ddl.CssClass = "mandatory";
			//GlobalTools.fillRefList(ddl, tplQ, true, conn);
			conn.QueryString = "SELECT DISTINCT FLOW_NAME FROM RF_TAT ORDER BY FLOW_NAME";
			conn.ExecuteQuery();
			for (int i=0; i < conn.GetRowCount(); i++)
			{
				ddl.Items.Add(new ListItem(conn.GetFieldValue(i,0),conn.GetFieldValue(i,0)));
			}

			TBL_MODEL.Rows.Add(new TableRow());
			TBL_MODEL.Rows[row].Cells.Add(new TableCell());	
			TBL_MODEL.Rows[row].Cells[0].CssClass = "TDBGColor1";
			TBL_MODEL.Rows[row].Cells[0].Style.Add("WIDTH", "300px");
			TBL_MODEL.Rows[row].Cells[0].Text = "TEMPLATE";
			TBL_MODEL.Rows[row].Cells.Add(new TableCell());	
			TBL_MODEL.Rows[row].Cells[1].Style.Add("WIDTH", "7px");
			TBL_MODEL.Rows[row].Cells.Add(new TableCell());	
			TBL_MODEL.Rows[row].Cells[2].CssClass = "TDBGColorValue";
			TBL_MODEL.Rows[row].Cells[2].Controls.Add(ddl);
		}

		private void bindData1()
		{
			bindData1(false);
		}

		private void bindData1(bool cleartable)
		{
			if (cleartable)
				TBL_EXISTING.Rows.Clear();

			int i, j, row;
			conn.QueryString = "EXEC PARAM_RF_TAT_COMMIT_MODEL";
			conn.ExecuteQuery(1000000);
			DataTable dt = conn.GetDataTable();

			//creating header row
			row = 0;
			TBL_EXISTING.Rows.Add(new TableRow());
			TBL_EXISTING.Rows[row].CssClass = "tdSmallHeader";
			for(j = 0; j < dt.Columns.Count; j++)
			{
				TBL_EXISTING.Rows[row].Cells.Add(new TableCell());
				TBL_EXISTING.Rows[row].Cells[j].Text = dt.Columns[j].ColumnName;
				if ((j % 2 == 1) || (j == 0)) 			//even columns contain id, odd columns contains desc			
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
					if ((j % 2 == 1) || (j == 0)) 			//even columns contain id, odd columns contains desc
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
				TBL_EXISTING.Rows[row].Cells[j].HorizontalAlign = HorizontalAlign.Center;
				TBL_EXISTING.Rows[row].Cells[j].Controls.Add(lbEdit);
				TBL_EXISTING.Rows[row].Cells[j].Controls.Add(lblSpace);
				TBL_EXISTING.Rows[row].Cells[j].Controls.Add(lbDel);
			}
		}

		private void bindData2()
		{
			bindData2(false);
		}

		private void bindData2(bool cleartable)
		{
			if (cleartable)
				TBL_REQUEST.Rows.Clear();

			int i, j, row;
			conn.QueryString = "EXEC PARAM_RF_TAT_COMMIT_PENDING_MODEL";
			conn.ExecuteQuery(1000000);
			DataTable dt = conn.GetDataTable();

			//creating header row
			row = 0;
			TBL_REQUEST.Rows.Add(new TableRow());
			TBL_REQUEST.Rows[row].CssClass = "tdSmallHeader";
			for(j = 0; j < dt.Columns.Count; j++)
			{
				TBL_REQUEST.Rows[row].Cells.Add(new TableCell());
				TBL_REQUEST.Rows[row].Cells[j].Text = dt.Columns[j].ColumnName;
				if ((j % 2 == 1) || (j == 0)) 	//even columns contain id, odd columns contains desc
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
					if (j == dt.Columns.Count-1)
						TBL_REQUEST.Rows[row].Cells[j].HorizontalAlign = HorizontalAlign.Center;
					TBL_REQUEST.Rows[row].Cells[j].Text = dt.Rows[i][j].ToString();
					if ((j % 2 == 1) || (j == 0)) 	//even columns contain id, odd columns contains desc
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
				TBL_REQUEST.Rows[row].Cells[j].HorizontalAlign = HorizontalAlign.Center;
				TBL_REQUEST.Rows[row].Cells[j].Controls.Add(lbEdit);
				TBL_REQUEST.Rows[row].Cells[j].Controls.Add(lblSpace);
				TBL_REQUEST.Rows[row].Cells[j].Controls.Add(lbDel);
			}
		}

		private void clearEditBoxes()
		{
			LBL_SAVEMODE.Text = "1";
			LBL_MODELSEQ.Text  = "NULL";
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

		protected void BTN_SAVE_VALUE_Click(object sender, System.EventArgs e)
		{
			string argStr;	// dynamic fields leave no choice but to pad the values under one string
			// this string is to be parsed back in the stored procedure that accept it
			DropDownList ddl;

			//start pengecekan
			argStr = "";
			for (int i = 0; i < TBL_MODEL.Rows.Count-1; i++)
			{
				try
				{
					ddl = (DropDownList) TBL_MODEL.Rows[i].Cells[2].FindControl("ddl" + i.ToString());
					if (ddl.SelectedValue == "")
						argStr += ";";
					else
						argStr += ddl.SelectedValue + ";";
				} 
				catch {}
			}
			Response.Write("<!-- pengecekan " + argStr + " --> ");
			if (argStr.Replace(";","") == "")
			{
				GlobalTools.popMessage(this, "One of key must be filled..!");
				return;
			}
			//end of pengecekan

			argStr = "";
			for (int i = 0; i < TBL_MODEL.Rows.Count; i++)
			{
				try
				{
					ddl = (DropDownList) TBL_MODEL.Rows[i].Cells[2].FindControl("ddl" + i.ToString());
					if (ddl.SelectedValue == "")
						argStr += "NULL;";
					else
						argStr += ddl.SelectedValue + ";";
				} 
				catch {}
			}
			Response.Write("<!-- masuk procedure " + argStr + " --> ");

			conn.QueryString = "EXEC PARAM_RF_TAT_COMMIT_MODEL_MAKER '"+ LBL_EDITMODE.Text + "', '" + LBL_SAVEMODE.Text +"', '"+ argStr + 
				"', '"+LBL_MODELSEQ.Text+"'"; 

			Response.Write("<!-- " + conn.QueryString + " --> ");
			//try
			//{
				conn.ExecuteNonQuery();
			//} 
			/*catch (Exception ex)
			{
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)		//method Connection.ExecuteNonQuery() add this msg on exception 
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this, errmsg);
			}*/
			
			bindData2(true);
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
								ddl = (DropDownList) TBL_MODEL.Rows[i].Cells[2].FindControl("ddl" + i.ToString());
								ddl.SelectedValue = TBL_EXISTING.Rows[row].Cells[(i * 2)+1].Text;
							}
							LBL_MODELSEQ.Text = TBL_EXISTING.Rows[row].Cells[0].Text;
							enableControls(true);
						}
						else if (btncode == "d")	//delete button
						{
							argStr = "";
							for (int i = 0; i < TBL_EXISTING.Rows[row].Cells.Count - 1; i++)
							{
								try
								{
									if (i % 2 == 1)
									{
										if ((TBL_EXISTING.Rows[row].Cells[i].Text == "") || (TBL_EXISTING.Rows[row].Cells[i].Text == "NULL"))
											argStr += "NULL;";
										else
											argStr += TBL_EXISTING.Rows[row].Cells[i].Text + ";";
									}
								} 
								catch {}
							}
							LBL_MODELSEQ.Text = TBL_EXISTING.Rows[row].Cells[0].Text;
							conn.QueryString = "EXEC PARAM_RF_TAT_COMMIT_MODEL_MAKER '"+ LBL_EDITMODE.Text + "', '2', '"+ argStr + 
								"', '"+LBL_MODELSEQ.Text+"'"; 

							Response.Write("<!-- "+conn.QueryString+" -->");
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
							LBL_MODELSEQ.Text = "NULL";
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
									ddl.SelectedValue = TBL_REQUEST.Rows[row].Cells[(i * 2)+1].Text;
								} 
								catch {}
							}
							LBL_MODELSEQ.Text = TBL_REQUEST.Rows[row].Cells[0].Text;
							enableControls(true);
						}
						else if (btncode == "d")	//delete button
						{
							argStr = "";
							for (int i = 0; i < TBL_REQUEST.Rows[row].Cells.Count - 3; i++)
							{
								try
								{
									if (i % 2 == 1)
									{
										if ((TBL_REQUEST.Rows[row].Cells[i].Text == "") || (TBL_REQUEST.Rows[row].Cells[i].Text == "NULL"))
											argStr += "NULL;";
										else
											argStr += TBL_REQUEST.Rows[row].Cells[i].Text + ";";
									}
								} 
								catch {}
							}
							LBL_MODELSEQ.Text = TBL_REQUEST.Rows[row].Cells[0].Text;
							
							conn.QueryString = "EXEC PARAM_RF_TAT_COMMIT_MODEL_MAKER '" + LBL_EDITMODE.Text + "', '9', '"+ argStr + 
								"', '"+LBL_MODELSEQ.Text+"'"; 
							Response.Write("<!--"+conn.QueryString+"-->");
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
							LBL_MODELSEQ.Text = "NULL";
						}
						break;

					default:
						break;
				}
			} 
			catch {}
			//GlobalTools.popMessage(this, lb.ID + "  " + row.ToString() + "  " + id);
		}
	}
}
