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
	/// Summary description for ParamListOtherAppr.
	/// </summary>
	public partial class ParamListOtherAppr : System.Web.UI.Page
	{
		protected Connection conn2;
		protected Connection conn;
		string PARAM_ID, SEQ, scid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon(); 
			if (!IsPostBack)
			{
				ViewListAppr();
			} 
			else
			{
				AddSeqList();
			}
			DGR_APPR_LIST.PageIndexChanged += new DataGridPageChangedEventHandler(this.GridList_Change);
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

		private void AddSeqList()
		{
			for (int i=0; i < DGR_APPR_LIST.Items.Count; i++)
			{
				Label LBL_NUMBLIST = new Label();
				LBL_NUMBLIST.ID = "LBL_NUMBLIST_" + i.ToString();
				LBL_NUMBLIST.Text = (i+1).ToString();
				DGR_APPR_LIST.Items[i].Cells[0].Controls.Add(LBL_NUMBLIST);
			}
		}
		
		private void ViewListAppr()
		{
			
			conn.QueryString = "select PARAM_ID,SEQ_ID,PARAM_NAME,PARAM_FORMULA,PARAM_FIELD,PARAM_TABLE, " +
				"PARAM_TABLE_CHILD,PARAM_LINK,PARAM_PRM,PARAM_ACTIVE,CH_STA, " +
				"case when CH_STA = '1' then 'INSERT' when CH_STA = '0' then 'UPDATE' " +
				"else 'DELETE' end CH_STA1 from TMANDIRI_PARAM_LIST " +
				"where PARAM_OTHER = '1'";
			conn.ExecuteQuery();

			DGR_APPR_LIST.DataSource = conn.GetDataTable().Copy();
			
			try 
			{
				DGR_APPR_LIST.DataBind();
				AddSeqList();
			}
			catch 
			{
				DGR_APPR_LIST.CurrentPageIndex = DGR_APPR_LIST.PageCount - 1;
				DGR_APPR_LIST.DataBind();
				AddSeqList();
			}
		}

		private void performRequestList(int row, char appr_sta, string userid)
		{
			try 
			{
				PARAM_ID = DGR_APPR_LIST.Items[row].Cells[1].Text.Trim();
				SEQ = DGR_APPR_LIST.Items[row].Cells[2].Text.Trim();
				
				
				conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAMLIST_APPR '"+PARAM_ID+"', '"+SEQ+"' , '"+appr_sta+"', '"+userid+"'";
				conn.ExecuteNonQuery();

				//conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAMLIST_APPR '"+PARAM_ID+"', '"+SEQ+"' , '"+appr_sta+"'";
				
			} 
			catch {}
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
				catch 
				{
					GlobalTools.popMessage(this,"Error on Stored Procedure!");				
				}
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
