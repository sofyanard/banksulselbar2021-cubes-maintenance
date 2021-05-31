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
	/// Summary description for DeleteConfirmation.
	/// </summary>
	public partial class DeleteConfirmation : System.Web.UI.Page
	{
		protected Connection conn,conn2;
		protected string mid, scoflag, rngflag, lmtflag, dimid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				tr_modelscoring.Visible = false;
				tr_modelrange.Visible = false;
				tr_modellimit.Visible = false;
				viewdata();
			}	
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "SELECT * FROM RFMODULE WHERE MODULEID= '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}


		private void viewdata()
		{			
			conn.QueryString="SELECT * FROM VW_TMANDIRI_DIMESI_ITEM WHERE CH_STA1 = 'DELETE'";
			conn.ExecuteQuery();
			DataTable dt_dim = conn.GetDataTable();

			for(int k = 0; k < dt_dim.Rows.Count; k++)
			{		
				if (conn.GetFieldValue(dt_dim,k,"SCOFLAG") == "1")
				{
					int i, j, row;

					tr_modelscoring.Visible = true;					
					conn.QueryString = "EXEC PARAM_DELETE_DIMESI_ITEM '1', '"+conn.GetFieldValue(dt_dim,k,"REFFIELDID")+"'";
					conn.ExecuteQuery();
					DataTable dt = conn.GetDataTable();
					
					//creating header row
					row = TBL_SCORING.Rows.Count;
					TBL_SCORING.Rows.Add(new TableRow());
					TBL_SCORING.Rows[row].CssClass = "tdSmallHeader";
					for(j = 0; j < dt.Columns.Count; j++)
					{
						TBL_SCORING.Rows[row].Cells.Add(new TableCell());
						TBL_SCORING.Rows[row].Cells[j].Text = dt.Columns[j].ColumnName;
						if (j % 2 == 0)			//even columns contain id, odd columns contains desc
							TBL_SCORING.Rows[row].Cells[j].Visible = false;
					}
				
					//creating data rows 
					for(i = 0; i < dt.Rows.Count; i++)
					{
						row = TBL_SCORING.Rows.Count;
						TBL_SCORING.Rows.Add(new TableRow());
						if (i % 2 == 1)
							TBL_SCORING.Rows[row].CssClass = "tblalternating";
						for(j = 0; j < dt.Columns.Count; j++)
						{
							TBL_SCORING.Rows[row].Cells.Add(new TableCell());
							TBL_SCORING.Rows[row].Cells[j].Text = dt.Rows[i][j].ToString();
							if (j % 2 == 0)			//even columns contain id, odd columns contains desc
								TBL_SCORING.Rows[row].Cells[j].Visible = false;
						}
					}
				}

				if (conn.GetFieldValue(dt_dim,k,"RNGFLAG") == "1")
				{
					int i, j, row;

					tr_modelrange.Visible = true;					
					conn.QueryString = "EXEC PARAM_DELETE_DIMESI_ITEM '2', '"+conn.GetFieldValue(dt_dim,k,"REFFIELDID")+"'";
					conn.ExecuteQuery();
					DataTable dt = conn.GetDataTable();
					
					//creating header row
					row = TBL_RANGE.Rows.Count;
					TBL_RANGE.Rows.Add(new TableRow());
					TBL_RANGE.Rows[row].CssClass = "tdSmallHeader";
					for(j = 0; j < dt.Columns.Count; j++)
					{
						TBL_RANGE.Rows[row].Cells.Add(new TableCell());
						TBL_RANGE.Rows[row].Cells[j].Text = dt.Columns[j].ColumnName;
						if (j % 2 == 0)			//even columns contain id, odd columns contains desc
							TBL_RANGE.Rows[row].Cells[j].Visible = false;
					}
				
					//creating data rows 
					for(i = 0; i < dt.Rows.Count; i++)
					{
						row = TBL_RANGE.Rows.Count;
						TBL_RANGE.Rows.Add(new TableRow());
						if (i % 2 == 1)
							TBL_RANGE.Rows[row].CssClass = "tblalternating";
						for(j = 0; j < dt.Columns.Count; j++)
						{
							TBL_RANGE.Rows[row].Cells.Add(new TableCell());
							TBL_RANGE.Rows[row].Cells[j].Text = dt.Rows[i][j].ToString();
							if (j % 2 == 0)			//even columns contain id, odd columns contains desc
								TBL_RANGE.Rows[row].Cells[j].Visible = false;
						}
					}
				}

				if (conn.GetFieldValue(dt_dim,k,"LMTFLAG") == "1")
				{
					int i, j, row;

					tr_modellimit.Visible = true;					
					conn.QueryString = "EXEC PARAM_DELETE_DIMESI_ITEM '3', '"+conn.GetFieldValue(dt_dim,k,"REFFIELDID")+"'";
					conn.ExecuteQuery();
					DataTable dt = conn.GetDataTable();
					
					//creating header row
					row = TBL_LIMIT.Rows.Count;
					TBL_LIMIT.Rows.Add(new TableRow());
					TBL_LIMIT.Rows[row].CssClass = "tdSmallHeader";
					for(j = 0; j < dt.Columns.Count; j++)
					{
						TBL_LIMIT.Rows[row].Cells.Add(new TableCell());
						TBL_LIMIT.Rows[row].Cells[j].Text = dt.Columns[j].ColumnName;
						if (j % 2 == 0)			//even columns contain id, odd columns contains desc
							TBL_LIMIT.Rows[row].Cells[j].Visible = false;
					}
				
					//creating data rows 
					for(i = 0; i < dt.Rows.Count; i++)
					{
						row = TBL_LIMIT.Rows.Count;
						TBL_LIMIT.Rows.Add(new TableRow());
						if (i % 2 == 1)
							TBL_LIMIT.Rows[row].CssClass = "tblalternating";
						for(j = 0; j < dt.Columns.Count; j++)
						{
							TBL_LIMIT.Rows[row].Cells.Add(new TableCell());
							TBL_LIMIT.Rows[row].Cells[j].Text = dt.Rows[i][j].ToString();
							if (j % 2 == 0)			//even columns contain id, odd columns contains desc
								TBL_LIMIT.Rows[row].Cells[j].Visible = false;
						}
					}
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

		}
		#endregion

	}
}
