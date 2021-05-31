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
	/// Summary description for CUBESTplRangeAppr.
	/// </summary>
	public partial class CUBESTplRangeAppr : System.Web.UI.Page
	{
		protected Connection conn2;
		protected Connection conn;
		protected string mid;
		string RESULT_ID, SEQ_ID, TPLID, scid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2(); 
			if (!IsPostBack)
			{
				ViewPendingData();
				ViewPendingData2();
			}
			DGR_APPR.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGR_APPR_PageIndexChanged);	
			DGR_APPR2.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGR_APPR2_PageIndexChanged);
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "SELECT * FROM RFMODULE WHERE MODULEID = '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		public void ViewPendingData()
		{
			conn.QueryString = "SELECT * FROM VW_TMANDIRI_RANGE_RESULT ";
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
				TPLID = DGR_APPR.Items[row].Cells[2].Text.Trim();

				//Audittrail
				conn.QueryString = "EXEC PARAM_TEMPLATE_RANGE_RESULT_APPR '" + 
					RESULT_ID+ "',  '" +SEQ_ID+ "' , '" +TPLID+ "' , '" +appr_sta+ "' , '" +userid+ "'";
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

		private void ViewPendingData2()
		{
			conn.QueryString="SELECT * FROM VW_MANDIRI_RANGE_TTEMPLATE";
			conn.ExecuteQuery();
			this.DGR_APPR2.DataSource = conn.GetDataTable().Copy();
			try 
			{
				this.DGR_APPR2.DataBind();
			}
			catch 
			{
				this.DGR_APPR2.CurrentPageIndex = this.DGR_APPR2.PageCount - 1;
				this.DGR_APPR2.DataBind();
			}
		}

		private void performRequest2(int row)
		{
			try 
			{
				string userid = Session["UserID"].ToString();
				string groupid = Session["GroupID"].ToString();
				string TPLID = DGR_APPR2.Items[row].Cells[0].Text.Trim();
				conn.QueryString="select * from VW_MANDIRI_RANGE_TTEMPLATE where RNGTPLID='"+TPLID+"'";
				conn.ExecuteQuery();
				string status = conn.GetFieldValue("CH_STA");
				string TPLDESC = conn.GetFieldValue("RNGTPLDESC");

				if (status.Equals("0"))
				{
					conn.QueryString = "EXEC PARAM_TEMPLATE_RANGE_APPROVAL '3','"+TPLID+"','" +
						TPLDESC+ "', '" +status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}

				if (status.Equals("1"))
				{
					conn.QueryString = "EXEC PARAM_TEMPLATE_RANGE_APPROVAL '4','"+TPLID+"','" +
						TPLDESC+ "', '" +status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}
				if (status.Equals("2"))
				{
					conn.QueryString = "EXEC PARAM_TEMPLATE_RANGE_APPROVAL '5','"+TPLID+"','" +
						TPLDESC+ "', '" +status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}

				conn.QueryString = "EXEC PARAM_TEMPLATE_RANGE_APPROVAL '2','"+
					TPLID+"', '', '', '', ''" ;
				conn.ExecuteNonQuery();
				
			} 
			catch (Exception p)
			{
				GlobalTools.popMessage(this,p.Message);
			}
		}

		private void deleteData2(int row)
		{
			try 
			{
				string TPLID = DGR_APPR2.Items[row].Cells[0].Text.Trim();

				conn.QueryString = "EXEC PARAM_TEMPLATE_RANGE_APPROVAL '2','"+
					TPLID+"', '', '', '', ''" ;
				conn.ExecuteNonQuery();
			} 
			catch {}
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
			this.DGR_APPR2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR2_ItemCommand);
			this.DGR_APPR2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPR2_PageIndexChanged);

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

		private void DGR_APPR2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR2.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData2();
		}

		private void DGR_APPR2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR2.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR2.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR2.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR2.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DGR_APPR2.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR2.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR2.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR2.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR2.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR2.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR2.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR2.Items[i].FindControl("rdo_Pending");
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

		protected void BTN_SUBMIT2_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGR_APPR2.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_APPR2.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGR_APPR2.Items[i].FindControl("rdo_Reject");
					if (rbA.Checked)
					{
						performRequest2(i);
					}
					else if (rbR.Checked)
					{
						deleteData2(i);
					}
				} 
				catch {}
			}
			ViewPendingData2();
		}
	}
}
