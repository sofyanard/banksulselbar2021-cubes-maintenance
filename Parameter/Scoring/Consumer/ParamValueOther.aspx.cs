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
	/// Summary description for ParamValueOther.
	/// </summary>
	public partial class ParamValueOther : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		string PARAM_ID, REQUEST_ID, PARAM_OTHER_ID, SEQ, scid;
		private string sqlKondisi;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon();		
			if (!IsPostBack)
			{
				FillProductType();
				sqlKondisi = "";
				ViewValueAppr();				
			}
			else
			{
				AddSeqValue();
			}
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

		private void AddSeqValue()
		{
			for (int i=0; i < DGR_APPR_VALUE.Items.Count; i++)
			{
				Label LBL_NUMBValue = new Label();
				LBL_NUMBValue.ID = "LBL_NUMBValue_" + i.ToString();
				LBL_NUMBValue.Text = (i+1).ToString();
				DGR_APPR_VALUE.Items[i].Cells[0].Controls.Add(LBL_NUMBValue);
			}
		}
		
		private void ViewValueAppr()
		{	
			conn.QueryString = "select t.PARAM_ID, t.REQUEST_ID, t.PARAM_OTHER_ID, t.PARAM_OTHER_NAME, t.PARAM_OTHER_VALUE, "+
				"t.SEQ_ID, t.CH_STA, case when t.CH_STA = '1' then 'INSERT' when t.CH_STA = '0' then 'UPDATE' "+
				"else 'DELETE' end CH_STA1 from TMANDIRI_PARAM_OTHER t left join "+
                "MANDIRI_PARAM_REQUEST m on t.REQUEST_ID = m.REQUEST_ID left join "+
				"MANDIRI_PARAM_LIST ml on ml.param_id = t.param_id where 1=1 " + sqlKondisi;

				
			conn.ExecuteQuery();

			DGR_APPR_VALUE.DataSource = conn.GetDataTable().Copy();
			
			try 
			{
				DGR_APPR_VALUE.DataBind();
				AddSeqValue();
			}
			catch 
			{
				DGR_APPR_VALUE.CurrentPageIndex = DGR_APPR_VALUE.PageCount - 1;
				DGR_APPR_VALUE.DataBind();
				AddSeqValue();
			}
		}

		private void setSQLKondisi()
		{
			if(DDL_PARAMETER_PRM.SelectedValue != "" && DDL_PRODUCT_TYPE.SelectedValue == "")
				sqlKondisi = " AND ml.param_prm = '"+ DDL_PARAMETER_PRM.SelectedValue+"'";

			if(DDL_PRODUCT_TYPE.SelectedValue != "" && DDL_PARAMETER_PRM.SelectedValue == "")
				sqlKondisi = " AND m.productid = '"+ DDL_PRODUCT_TYPE.SelectedValue +"'";
			else sqlKondisi = " AND m.productid = '"+ DDL_PRODUCT_TYPE.SelectedValue +"'";

			ViewValueAppr();
		}

		public void FillProductType()
		{
			this.DDL_PRODUCT_TYPE.Items.Add(new ListItem("- SELECT -", ""));
			
			conn.QueryString = "SELECT PRODUCTID,PRODUCTNAME FROM TPRODUCT " +
				"WHERE GROUP_ID = '1'";
			conn.ExecuteQuery();
			
			for (int i=0; i <conn.GetRowCount(); i++)
			{
				this.DDL_PRODUCT_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
			}
		}
		
		private void performRequestValue(int row, char appr_sta, string userid)
		{
			try 
			{
				PARAM_ID = DGR_APPR_VALUE.Items[row].Cells[1].Text.Trim();
				REQUEST_ID = DGR_APPR_VALUE.Items[row].Cells[2].Text.Trim();
				PARAM_OTHER_ID = DGR_APPR_VALUE.Items[row].Cells[3].Text.Trim();
				SEQ = DGR_APPR_VALUE.Items[row].Cells[4].Text.Trim();
				
				
				conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAMOTHVALUE_APPR '"+PARAM_ID+"', '"+
					REQUEST_ID+"' , '"+PARAM_OTHER_ID+"' , '"+SEQ+"' , '"+appr_sta+"', '"+userid+"'";
				
				/*
				conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAMOTHVALUE_APPR '"+PARAM_ID+"', '"+
					REQUEST_ID+"', '"+PARAM_OTHER_ID+"', '"+SEQ+"', '"+appr_sta+"'";
				*/
				conn.ExecuteNonQuery();
			} 
			catch 
			{
				GlobalTools.popMessage(this,"Error on Stored Procedure!");			
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
			this.DGR_APPR_VALUE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR_VALUE_ItemCommand);
			this.DGR_APPR_VALUE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPR_VALUE_PageIndexChanged);

		}
		#endregion

		protected void BTN_SUBMIT_VALUE_Click(object sender, System.EventArgs e)
		{
			scid = (string) Session["UserID"];

			for (int i = 0; i < DGR_APPR_VALUE.Items.Count; i++)
			{		
				
				RadioButton rbA = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Approve"),
						rbR = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Reject");
				if (rbA.Checked)
					performRequestValue(i, '1', scid);
				else if (rbR.Checked)
					performRequestValue(i, '0', scid);
			}

			ViewValueAppr();
		}

		private void DGR_APPR_VALUE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allApprove":
					for (i = 0; i < DGR_APPR_VALUE.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_APPR_VALUE.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPending":
					for (i = 0; i < DGR_APPR_VALUE.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Pending");
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

		private void DGR_APPR_VALUE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_APPR_VALUE.CurrentPageIndex = e.NewPageIndex;
			
			ViewValueAppr();		
		}

		protected void DDL_PARAMETER_PRM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			setSQLKondisi();		
		}

		protected void DDL_PRODUCT_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			setSQLKondisi();		
		}
	}
}
