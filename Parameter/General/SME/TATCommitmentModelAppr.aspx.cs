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
	/// Summary description for TATCommitmentModelAppr.
	/// </summary>
	public partial class TATCommitmentModelAppr : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			bindData2();
		}

		private void bindData2()
		{
			bindData2(false);
		}

		private void bindData2(bool cleartable)
		{
			if (cleartable)
				TBL_APPR.Rows.Clear();

			int i, j, row;
			conn.QueryString = "EXEC PARAM_RF_TAT_COMMIT_PENDING_MODEL";
			conn.ExecuteQuery();
			DataTable dt = conn.GetDataTable();

			//creating header row
			row = 0;
			TBL_APPR.Rows.Add(new TableRow());
			TBL_APPR.Rows[row].CssClass = "tdSmallHeader";
			for(j = 0; j < dt.Columns.Count; j++)
			{
				TBL_APPR.Rows[row].Cells.Add(new TableCell());
				TBL_APPR.Rows[row].Cells[j].Text = dt.Columns[j].ColumnName;
				if ((j % 2 == 1) || (j == 0))		//even columns contain id, odd columns contains desc
					TBL_APPR.Rows[row].Cells[j].Visible = false;
			}
			TBL_APPR.Rows[row].Cells.Add(new TableCell());
			TBL_APPR.Rows[row].Cells[j].Text = "Approve";
			TBL_APPR.Rows[row].Cells.Add(new TableCell());
			TBL_APPR.Rows[row].Cells[j+1].Text = "Reject";
			TBL_APPR.Rows[row].Cells.Add(new TableCell());
			TBL_APPR.Rows[row].Cells[j+2].Text = "Pending";


			//creating data rows 
			for(i = 0; i < dt.Rows.Count; i++)
			{
				row = i + 1;
				TBL_APPR.Rows.Add(new TableRow());
				if (i % 2 == 1)
					TBL_APPR.Rows[row].CssClass = "tblalternating";
				for(j = 0; j < dt.Columns.Count; j++)
				{
					TBL_APPR.Rows[row].Cells.Add(new TableCell());
					if (j == dt.Columns.Count-1)
						TBL_APPR.Rows[row].Cells[j].HorizontalAlign = HorizontalAlign.Center;
					TBL_APPR.Rows[row].Cells[j].Text = dt.Rows[i][j].ToString();
					if ((j % 2 == 1) || (j == 0))		//even columns contain id, odd columns contains desc
						TBL_APPR.Rows[row].Cells[j].Visible = false;
				}
				//function radiobutton
				RadioButton rdA = new RadioButton();
				rdA.ID = "rda" + row.ToString();
							
				RadioButton rdR = new RadioButton();
				rdR.ID = "rdr" + row.ToString();
						
				RadioButton rdP = new RadioButton();
				rdP.ID = "rdp" + row.ToString();
								
				TBL_APPR.Rows[row].Cells.Add(new TableCell());
				TBL_APPR.Rows[row].Cells[j].HorizontalAlign = HorizontalAlign.Center;
				TBL_APPR.Rows[row].Cells[j].Controls.Add(rdA);
				rdA.GroupName = "rd_approval" + row.ToString();
				
				TBL_APPR.Rows[row].Cells.Add(new TableCell());
				TBL_APPR.Rows[row].Cells[j+1].HorizontalAlign  = HorizontalAlign.Center;
				TBL_APPR.Rows[row].Cells[j+1].Controls.Add(rdR);
				rdR.GroupName = "rd_approval" + row.ToString();

				TBL_APPR.Rows[row].Cells.Add(new TableCell());
				TBL_APPR.Rows[row].Cells[j+2].HorizontalAlign = HorizontalAlign.Center;
				TBL_APPR.Rows[row].Cells[j+2].Controls.Add(rdP);
				rdP.GroupName = "rd_approval" + row.ToString();
				
				rdP.Checked = true;
			}									

						
			//function linkbuttton select all
			LinkButton lnkA = new LinkButton();
			lnkA.ID = "lnkA";
			lnkA.Text = "Select All";
			lnkA.Click += new EventHandler(lnk_Click);

			LinkButton lnkR = new LinkButton();
			lnkR.ID = "lnkR";
			lnkR.Text = "Select All";
			lnkR.Click += new EventHandler(lnk_Click);

			LinkButton lnkP = new LinkButton();
			lnkP.ID = "lnkP";
			lnkP.Text = "Select All";
			lnkP.Click += new EventHandler(lnk_Click);

			TBL_APPR.Rows.Add(new TableRow());
			for(j = 0; j < dt.Columns.Count; j++)
			{
				TBL_APPR.Rows[row+1].Cells.Add(new TableCell());
				if ((j % 2 == 1) || (j == 0)) 	
					TBL_APPR.Rows[row+1].Cells[j].Visible = false;
			}

			TBL_APPR.Rows[row+1].Cells.Add(new TableCell());
			TBL_APPR.Rows[row+1].Cells[j].HorizontalAlign = HorizontalAlign.Center;
			TBL_APPR.Rows[row+1].Cells[j].Controls.Add(lnkA);

			TBL_APPR.Rows[row+1].Cells.Add(new TableCell());
			TBL_APPR.Rows[row+1].Cells[j+1].HorizontalAlign = HorizontalAlign.Center;
			TBL_APPR.Rows[row+1].Cells[j+1].Controls.Add(lnkR);

			TBL_APPR.Rows[row+1].Cells.Add(new TableCell());
			TBL_APPR.Rows[row+1].Cells[j+2].HorizontalAlign = HorizontalAlign.Center;
			TBL_APPR.Rows[row+1].Cells[j+2].Controls.Add(lnkP);

		}

		private void performRequest(int row, string apprflag)
		{
			string argStr = "", userid = Session["UserID"].ToString();

			conn.QueryString = "EXEC PARAM_RF_TAT_COMMIT_PENDING_MODEL";
			conn.ExecuteQuery();
			DataTable dt = conn.GetDataTable();

			for(int j = 0; j < dt.Columns.Count; j++)
			{
				try
				{
					if (j % 2 == 1) 	
					{
						if (TBL_APPR.Rows[row].Cells[j].Text == "")
							argStr += "NULL;";
						else
							argStr += TBL_APPR.Rows[row].Cells[j].Text + ";";
					}
				} 
				catch {}
			}

			LBL_MODELSEQ.Text = TBL_APPR.Rows[row].Cells[0].Text;
			if (apprflag == "1")
			{
				conn.QueryString = "EXEC PARAM_RF_TAT_COMMIT_MODEL_APPR '1','"+ argStr + 
					"','"+userid+"','1','"+LBL_MODELSEQ.Text+"'";			

				Response.Write("<!-- approve baris ke-" + row +" "+ conn.QueryString + " --> ");
			}
			else
			{
				conn.QueryString = "EXEC PARAM_RF_TAT_COMMIT_MODEL_APPR '1','"+ argStr + 
					"','"+userid+"','0','"+LBL_MODELSEQ.Text+"'";

				Response.Write("<!-- reject baris ke-" + row +" "+ conn.QueryString + " --> ");
			}

			
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

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 1; i <= TBL_APPR.Rows.Count-1; i++)
			{		
				try
				{
					RadioButton rbA = (RadioButton) TBL_APPR.Rows[i].FindControl("rda" + i.ToString()),
						rbR = (RadioButton) TBL_APPR.Rows[i].FindControl("rdr" + i.ToString());

					if (rbA.Checked)
					{
						performRequest(i, "1");
					}
					else if (rbR.Checked)
					{
						performRequest(i, "0");
					}
				}
				catch{}
			}
			bindData2(true);
		}

		private void lnk_Click(object sender, EventArgs e)
		{
			LinkButton lb = (LinkButton) sender;
			string selectall = lb.ID;

			try
			{
				switch (selectall)
				{
					case "lnkA":					
						for (int i = 1; i <= TBL_APPR.Rows.Count-1; i++)
						{
							try
							{
								RadioButton rbA = (RadioButton) TBL_APPR.Rows[i].FindControl("rda" + i.ToString()),
									rbR = (RadioButton) TBL_APPR.Rows[i].FindControl("rdr" + i.ToString()),
									rbP = (RadioButton) TBL_APPR.Rows[i].FindControl("rdp" + i.ToString());
								rbP.Checked = false;
								rbR.Checked = false;
								rbA.Checked = true;
							} 
							catch {}
						}
						break;

					case "lnkR":						
						for (int i = 1; i <= TBL_APPR.Rows.Count-1; i++)
						{
							try
							{
								RadioButton rbA = (RadioButton) TBL_APPR.Rows[i].FindControl("rda" + i.ToString()),
									rbR = (RadioButton) TBL_APPR.Rows[i].FindControl("rdr" + i.ToString()),
									rbP = (RadioButton) TBL_APPR.Rows[i].FindControl("rdp" + i.ToString());
								rbP.Checked = false;
								rbA.Checked = false;
								rbR.Checked = true;
							} 
							catch {}
						}
						break;

					case "lnkP":						
						for (int i = 1; i <= TBL_APPR.Rows.Count-1; i++)
						{
							try
							{
								RadioButton rbA = (RadioButton) TBL_APPR.Rows[i].FindControl("rda" + i.ToString()),
									rbR = (RadioButton) TBL_APPR.Rows[i].FindControl("rdr" + i.ToString()),
									rbP = (RadioButton) TBL_APPR.Rows[i].FindControl("rdp" + i.ToString());
								rbA.Checked = false;
								rbR.Checked = false;
								rbP.Checked = true;
							} 
							catch {}
						}
						break;

					default:
						break;
				}
			} 
			catch {}
		}
	}
}
