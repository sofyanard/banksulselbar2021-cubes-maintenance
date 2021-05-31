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
	/// Summary description for ParamValueAppr.
	/// </summary>
	public partial class ParamValueAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		string PARAM_ID, REQUEST_ID, PARAM_VALUE_ID, SEQ, scid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon();
			
			if (!IsPostBack)
			{
				ViewValueAppr();
				fillDDL();
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

		private void fillDDL()
		{
			DDL_PRM.Items.Add(new ListItem("- SELECT -",""));  
			DDL_PRM.Items.Add(new ListItem("PROMPT","1"));
			DDL_PRM.Items.Add(new ListItem("CUBES","0"));
   
			GlobalTools.fillRefList(DDL_PID,"SELECT convert(int,PRODUCTID) PID, PRODUCTNAME from TPRODUCT order by PID",false,conn); 
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
			string plus = "";

			if(DDL_PRM.SelectedValue != "")
			{
				if(plus != "")
					plus = plus + " AND c.PARAM_PRM = '"+DDL_PRM.SelectedValue+"'"; 
				else
					plus = " AND c.PARAM_PRM = '"+DDL_PRM.SelectedValue+"'"; 
			}
			
			if(DDL_PID.SelectedValue != "")
			{
				if(plus != "")
					plus = plus + " AND b.PRODUCTID = '"+DDL_PID.SelectedValue+"'";
				else
					plus = " AND b.PRODUCTID = '"+DDL_PID.SelectedValue+"'";
			}

			conn.QueryString = "select b.PRODUCTID, c.PARAM_PRM, a.PARAM_ID, a.REQUEST_ID, a.PARAM_VALUE_ID, a.SEQ_ID, "+
				"a.PARAM_VALUE_NAME, a.MIN_VALUE, a.MAX_VALUE, a.PARAM_SCORE, a.CH_STA, "+
				"case when a.CH_STA = '0' then 'UPDATE' when a.CH_STA = '1' then 'INSERT' "+
				"else 'DELETE' end CH_STA1 from TMANDIRI_PARAM_VALUE a "+
				"left join MANDIRI_PARAM_REQUEST b on a.REQUEST_ID = b.REQUEST_ID "+
				"left join MANDIRI_PARAM_LIST c on a.PARAM_ID = c.PARAM_ID where 1=1 " +plus;

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
		
		private void performRequestValue(int row, char appr_sta, string userid)
		{
			try 
			{
				PARAM_ID = DGR_APPR_VALUE.Items[row].Cells[1].Text.Trim();
				REQUEST_ID = DGR_APPR_VALUE.Items[row].Cells[2].Text.Trim();
				PARAM_VALUE_ID = DGR_APPR_VALUE.Items[row].Cells[3].Text.Trim();
				SEQ = DGR_APPR_VALUE.Items[row].Cells[4].Text.Trim();
				
				
				conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAMVALUE_APPR '"+PARAM_ID+"', '"+
					REQUEST_ID+"' , '"+PARAM_VALUE_ID+"' , '"+SEQ+"' , '"+appr_sta+"', '"+userid+"'";
				

				/*conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAMVALUE_APPR '"+PARAM_ID+"', '"+
					REQUEST_ID+"' , '"+PARAM_VALUE_ID+"' , '"+SEQ+"' , '"+appr_sta+"'";
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
				try
				{
					RadioButton rbA = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Approve"),
						rbR = (RadioButton) DGR_APPR_VALUE.Items[i].FindControl("rd_Reject");
					if (rbA.Checked)
						performRequestValue(i, '1', scid);
					else if (rbR.Checked)
						performRequestValue(i, '0', scid);
				} 
				catch {}
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

		protected void DDL_PID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewValueAppr();		
		}

		protected void DDL_PRM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewValueAppr();		
		}
	}
}
