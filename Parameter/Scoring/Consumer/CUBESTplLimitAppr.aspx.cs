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
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.Scoring.Consumer
{
	/// <summary>
	/// Summary description for CUBESTplLimitAppr.
	/// </summary>
	public partial class CUBESTplLimitAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid;
		string Loan_Code, Seq_Id, Seq_No, scid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				ViewPendingDataVal();
				ViewPendingDataTpl();
			}

			DGR_APPR_VAL.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGR_APPR_VAL_PageIndexChanged);
			DGR_APPR_TPL.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGR_APPR_VAL_PageIndexChanged);
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "SELECT * FROM RFMODULE WHERE MODULEID = '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void ViewPendingDataVal()
		{
			conn.QueryString = "select * from VW_PARAM_TEMPLATE_LIMIT_MAKER_PENDING where LMTTPLID is not null ";
			conn.ExecuteQuery();
			
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_APPR_VAL.DataSource = data;
			
			try
			{
				this.DGR_APPR_VAL.DataBind();
			} 
			catch 
			{
				this.DGR_APPR_VAL.CurrentPageIndex = DGR_APPR_VAL.PageCount - 1;
				this.DGR_APPR_VAL.DataBind();	
			}
		}

		private void performRequestListVal(int row, char appr_sta, string userid)
		{
			try 
			{	
				Loan_Code = DGR_APPR_VAL.Items[row].Cells[12].Text.Trim();
				Seq_No = DGR_APPR_VAL.Items[row].Cells[1].Text.Trim();
				Seq_Id = DGR_APPR_VAL.Items[row].Cells[11].Text.Trim();					

				//Audittrail
				conn.QueryString = "EXEC PARAM_TEMPLATE_LIMIT_LIST_APPR '" + 
					Loan_Code + "', " + Seq_No + " , '"+ Seq_Id + "' , '" + appr_sta +"', '" +userid+ "'";
				//***********

				//conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_LIMIT_LIST_APPR '" + 
				//	Loan_Code + "', " + Seq_No + " , '"+ Seq_Id + "' , '" + appr_sta + "'";
				conn.ExecuteNonQuery();
			} 
			catch 
			{
				GlobalTools.popMessage(this,"Error on Stored Procedure");
			}
		}

		private void ViewPendingDataTpl()
		{
			conn.QueryString="SELECT * FROM VW_MANDIRI_LIMIT_TTEMPLATE";
			conn.ExecuteQuery();
			this.DGR_APPR_TPL.DataSource = conn.GetDataTable().Copy();
			try 
			{
				this.DGR_APPR_TPL.DataBind();
			}
			catch 
			{
				this.DGR_APPR_TPL.CurrentPageIndex = this.DGR_APPR_TPL.PageCount - 1;
				this.DGR_APPR_TPL.DataBind();
			}
		}

		private void performRequestTpl(int row)
		{
			try 
			{
				string userid = Session["UserID"].ToString();
				string groupid = Session["GroupID"].ToString();
				string TPLID = DGR_APPR_TPL.Items[row].Cells[0].Text.Trim();
				conn.QueryString="select * from VW_MANDIRI_LIMIT_TTEMPLATE where LMTTPLID='"+TPLID+"'";
				conn.ExecuteQuery();
				string status = conn.GetFieldValue("CH_STA");
				string TPLDESC = conn.GetFieldValue("LMTTPLDESC");

				if (status.Equals("0"))
				{
					conn.QueryString = "EXEC PARAM_TEMPLATE_LIMIT_APPROVAL '3','"+TPLID+"','" +
						TPLDESC+ "', '" +status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}

				if (status.Equals("1"))
				{
					conn.QueryString = "EXEC PARAM_TEMPLATE_LIMIT_APPROVAL '4','"+TPLID+"','" +
						TPLDESC+ "', '" +status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}
				if (status.Equals("2"))
				{
					conn.QueryString = "EXEC PARAM_TEMPLATE_LIMIT_APPROVAL '5','"+TPLID+"','" +
						TPLDESC+ "', '" +status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}

				conn.QueryString = "EXEC PARAM_TEMPLATE_LIMIT_APPROVAL '2','"+
					TPLID+"', '', '', '', ''" ;
				conn.ExecuteNonQuery();
				
			} 
			catch (Exception p)
			{
				GlobalTools.popMessage(this,p.Message);
			}
		}

		private void deleteDataTpl(int row)
		{
			try 
			{
				string TPLID = DGR_APPR_TPL.Items[row].Cells[0].Text.Trim();

				conn.QueryString = "EXEC PARAM_TEMPLATE_LIMIT_APPROVAL '2','"+
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
			this.DGR_APPR_TPL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR_TPL_ItemCommand);
			this.DGR_APPR_TPL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPR_TPL_PageIndexChanged);
			this.DGR_APPR_VAL.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR_VAL_ItemCommand);
			this.DGR_APPR_VAL.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPR_VAL_PageIndexChanged);

		}
		#endregion

		protected void BTN_SUBMIT_VAL_Click(object sender, System.EventArgs e)
		{
			scid = (string) Session["UserID"];

			for (int i = 0; i < this.DGR_APPR_VAL.Items.Count; i++)
			{
				RadioButton rbA = (RadioButton) DGR_APPR_VAL.Items[i].FindControl("rd_Approve"),
					rbR = (RadioButton) DGR_APPR_VAL.Items[i].FindControl("rd_Reject");
				
				if (rbA.Checked)
					performRequestListVal(i, '1', scid);
				else if (rbR.Checked)
					performRequestListVal(i, '0', scid);
			}

			ViewPendingDataVal();
		}

		private void DGR_APPR_VAL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_APPR_VAL.CurrentPageIndex = e.NewPageIndex;
			ViewPendingDataVal();
		}

		private void DGR_APPR_VAL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR_VAL.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_VAL.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR_VAL.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR_VAL.Items[i].FindControl("rd_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_APPR_VAL.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_VAL.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR_VAL.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR_VAL.Items[i].FindControl("rd_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR_VAL.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_VAL.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR_VAL.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR_VAL.Items[i].FindControl("rd_Pending");
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

		protected void BTN_SUBMIT_TPL_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGR_APPR_TPL.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_APPR_TPL.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGR_APPR_TPL.Items[i].FindControl("rdo_Reject");
					if (rbA.Checked)
					{
						performRequestTpl(i);
					}
					else if (rbR.Checked)
					{
						deleteDataTpl(i);
					}
				} 
				catch {}
			}
			ViewPendingDataTpl();
		}

		private void DGR_APPR_TPL_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR_TPL.CurrentPageIndex = e.NewPageIndex;
			ViewPendingDataTpl();
		}

		private void DGR_APPR_TPL_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR_TPL.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_TPL.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR_TPL.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR_TPL.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DGR_APPR_TPL.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_TPL.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR_TPL.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR_TPL.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR_TPL.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR_TPL.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR_TPL.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR_TPL.Items[i].FindControl("rdo_Pending");
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
