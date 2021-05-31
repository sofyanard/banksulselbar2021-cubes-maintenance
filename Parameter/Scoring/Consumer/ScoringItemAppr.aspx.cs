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
	/// Summary description for ScoringItemAppr.
	/// </summary>
	public partial class ScoringItemAppr : System.Web.UI.Page
	{
		protected Connection conn, conn2;
		string PARAM_ID, SEQ , scid, mid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2(); 

			if (!IsPostBack)
			{
				ViewListAppr();	
			} 

			DGR_APPR_LIST.PageIndexChanged += new DataGridPageChangedEventHandler(this.GridList_Change);
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "SELECT * FROM RFMODULE WHERE MODULEID = '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void ViewListAppr()
		{
			conn.QueryString = "SELECT * FROM VW_TMANDIRI_PARAM_LIST WHERE PARAM_OTHER = '0' ";
			conn.ExecuteQuery();
			
			DGR_APPR_LIST.DataSource = conn.GetDataTable().Copy();
			
			try 
			{
				DGR_APPR_LIST.DataBind();
			}
			catch 
			{
				DGR_APPR_LIST.CurrentPageIndex = DGR_APPR_LIST.PageCount - 1;
				DGR_APPR_LIST.DataBind();
			}
		}

		private void performRequestList(int row, char appr_sta, string userid)
		{
			try 
			{
				PARAM_ID = DGR_APPR_LIST.Items[row].Cells[0].Text.Trim();
				SEQ = DGR_APPR_LIST.Items[row].Cells[1].Text.Trim();
				
				conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAMLIST_APPR '"+PARAM_ID+"', '"+SEQ+"', '"+appr_sta+"', '"+userid+"'";
				
				conn.ExecuteNonQuery();
			} 
			catch 
			{
				GlobalTools.popMessage(this,"Error on Stored Procedure!");
			}
		}

		void GridList_Change(Object sender, DataGridPageChangedEventArgs e) 
		{
			DGR_APPR_LIST.CurrentPageIndex = e.NewPageIndex;
			ViewListAppr();	
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
			this.DGR_APPR_LIST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR_LIST_ItemCommand);

		}
		#endregion

		protected void BTN_SUBMIT_LIST_Click(object sender, System.EventArgs e)
		{
			scid = (string) Session["UserID"];

			for (int i = 0; i < DGR_APPR_LIST.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Approve"),
						rbR = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Reject");
					if (rbA.Checked)
						performRequestList(i, '1', scid);
					else if (rbR.Checked)
						performRequestList(i, '0', scid);
				} 
				catch {}
			}
			ViewListAppr();
		}

		private void DGR_APPR_LIST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR_LIST.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_APPR_LIST.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR_LIST.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR_LIST.Items[i].FindControl("rd_Pending");
							rbA.Checked = false;
							rbB.Checked = false;
							rbC.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					// Do nothing.
					break;
			}

		}
	}
}
