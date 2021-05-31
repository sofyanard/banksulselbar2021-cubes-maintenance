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
	/// Summary description for CUBESFinalResultAppr.
	/// </summary>
	public partial class CUBESFinalResultAppr : System.Web.UI.Page
	{
		protected Connection conn2;
		protected Connection conn;
		string RESULT_ID, SEQ_ID, PRODUCTID, scid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon(); 
			if (!IsPostBack)
			{
				ViewPendingData();
			}
			DGR_APPR.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGR_APPR_PageIndexChanged);	
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

		public void ViewPendingData()
		{
			conn.QueryString = "SELECT A.RESULT_ID, A.SEQ_ID ,A.PRODUCTID, B.PRODUCTNAME AS PRODUCT_NAME, "+
				"A.RESULT_NAME, A.MAX_RANGE, A.MIN_RANGE, A.CH_STA, " +
				"case when A.CH_STA = '1' then 'INSERT' when A.CH_STA = '0' then 'UPDATE' "+
				"else 'DELETE' end CH_STA1 FROM TMANDIRI_RANGE_RESULT A "+
				"JOIN TPRODUCT B ON A.PRODUCTID = B.PRODUCTID ";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_APPR.DataSource = data;
			
			try
			{
				DGR_APPR.DataBind();
			} 
			catch 
			{
				
				DGR_APPR.CurrentPageIndex = DGR_APPR.PageCount - 1;
				DGR_APPR.DataBind();
			}
		}

		private void performRequestList(int row, char appr_sta, string userid)
		{
			try 
			{	
				RESULT_ID = DGR_APPR.Items[row].Cells[0].Text.Trim();
				SEQ_ID = DGR_APPR.Items[row].Cells[8].Text.Trim();
				PRODUCTID = DGR_APPR.Items[row].Cells[2].Text.Trim();

				//Audittrail
				conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_RANGE_RESULT_APPR '" + 
					RESULT_ID+ "',  '" +SEQ_ID+ "' , '" +PRODUCTID+ "' , '" +appr_sta+ "' , '" +userid+ "'";
				//*****

				//conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_RANGE_RESULT_APPR '" + 
				//	RESULT_ID+ "',  '" +SEQ_ID+ "' , '" +PRODUCTID+ "' , '" +appr_sta+ "'";

				conn.ExecuteNonQuery();
			} 
			catch 
			{
				GlobalTools.popMessage(this,"Error on Stored Procedure");
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
			this.DGR_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR_ItemCommand);
			this.DGR_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPR_PageIndexChanged);

		}
		#endregion

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			scid = (string) Session["UserID"];

			for (int i = 0; i < this.DGR_APPR.Items.Count; i++)
			{
				RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
					rbR = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject");
				if (rbA.Checked)
					performRequestList(i, '1', scid);
				else if (rbR.Checked)
					performRequestList(i, '0', scid);
			}

			ViewPendingData();
		}

		private void DGR_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_APPR.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Pending");
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
