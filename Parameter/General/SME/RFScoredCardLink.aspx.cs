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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFSCORECARDLINK.
	/// </summary>
	public partial class RFSCORECARDLINK : System.Web.UI.Page
	{
	
		private Connection conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if (!IsPostBack) 
			{
				fillDropDownList();
				viewDataPending();
			}

			BTN_SAVE.Attributes.Add("onclick", "if(!cek_mandatory(document.Form1)){return false;}");
		}

		private void fillDropDownList() 
		{
			/// program
			/// 
			GlobalTools.fillRefList(DDL_PROGRAMID, "select loanpurpid, loanpurpdesc from rfloanpurpose where active='1'  order by loanpurpid", true, conn);

			/// aspek
			/// 
			GlobalTools.fillRefList(DDL_CA_ASPEK, "select productid, productdesc from rfproduct where active='1' order by productid", true, conn);

			///rca
			///
			GlobalTools.fillRefList(DDL_RCA, "select SCORECARDID, SCORECARDDESC from VW_RFSCORECARDMODEL order by SCORECARDID", true, conn);

		}

		private void viewDataCurrent(string _programID,string _productID)//,string _program) 
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_CURRENT_RFSCORECARDLINK where loanpurpid = '" + _programID + "' and productid = '" + _productID + "' ";//and program = '" + _program + "' ";
			conn.ExecuteQuery();

			DGR_CURRENT.DataSource = conn.GetDataTable().DefaultView;
			try 
			{
				DGR_CURRENT.DataBind();
			}
			catch 
			{
				DGR_CURRENT.CurrentPageIndex = 0;
				DGR_CURRENT.DataBind();
			}
		}

		private void viewDataPending() 
		{			
			conn.QueryString = "select * from VW_PARAM_GENERAL_PENDING_RFSCORECARDLINK";
			conn.ExecuteQuery();

			DGR_PENDING.DataSource = conn.GetDataTable().DefaultView;
			try 
			{
				DGR_PENDING.DataBind();
			}
			catch 
			{
				DGR_PENDING.CurrentPageIndex = 0;
				DGR_PENDING.DataBind();
			}
		}

		private void resetStatus() 
		{
			DDL_PROGRAMID.SelectedValue = "";
			DDL_CA_ASPEK.SelectedValue = "";
			DDL_RCA.SelectedValue = "";
			DDL_PROGRAMID.Enabled = true;
			LBL_SAVEMODE.Text = "1";
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
			this.DGR_CURRENT.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_CURRENT_ItemCommand);
			this.DGR_CURRENT.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_CURRENT_PageIndexChanged);
			this.DGR_PENDING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_PENDING_ItemCommand);
			this.DGR_PENDING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_PENDING_PageIndexChanged);

		}
		#endregion

		protected void BTN_VIEW_Click(object sender, System.EventArgs e)
		{
			viewDataCurrent(DDL_PROGRAMID.SelectedValue,DDL_CA_ASPEK.SelectedValue);//,RDO_PROGRAMFLAG.SelectedValue.Trim());
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			resetStatus();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{

			conn.QueryString = "select * from RFSCORECARDLINK where  loanpurpid= '"+
				this.DDL_PROGRAMID.SelectedValue.ToString() +"' and productid='"+
				this.DDL_CA_ASPEK.SelectedValue.ToString() +"' ";
			conn.ExecuteNonQuery();

			//if (conn.GetRowCount()!=0)
			//{
			//	GlobalTools.popMessage(this,"Loan Purpose and Product Already Linked ");
			//}
			//else
			//{ 

				int res = 1;
				if (res <= this.DGR_PENDING.Items.Count)
				{
					res = this.DGR_PENDING.Items.Count + 1;
				}
				this.LBL_SEQ_ID.Text = res.ToString();
											
				conn.QueryString = "EXEC PARAM_GENERAL_RFSCORECARDLINK_MAKER '"+ LBL_SAVEMODE.Text.Trim() + "', '"+
					this.DDL_PROGRAMID.SelectedValue.ToString() +"','"+
					this.DDL_CA_ASPEK.SelectedValue.ToString() +"', '"+
					this.RDO_PROGRAMFLAG.SelectedValue.ToString() +"','"+
					this.DDL_RCA.SelectedValue.ToString() +"','"+this.LBL_SEQ_ID.Text+"' ";
				conn.ExecuteNonQuery();
			//}
			resetStatus();
			viewDataPending();
		}

		private void DGR_CURRENT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			resetStatus();

			switch (e.CommandName) 
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";

					DDL_PROGRAMID.Enabled = false;
					try { DDL_PROGRAMID.SelectedValue = e.Item.Cells[0].Text; } 
					catch { DDL_PROGRAMID.Enabled = true; }
					try { DDL_CA_ASPEK.SelectedValue = e.Item.Cells[2].Text; } 
					catch {}
					try { DDL_RCA.SelectedValue = e.Item.Cells[5].Text; } 
					catch {}
					try
					{
						if (e.Item.Cells[4].Text.Trim() == "Y")
							RDO_PROGRAMFLAG.SelectedValue = "1";
						else
							RDO_PROGRAMFLAG.SelectedValue = "0";
					} 
					catch {}


					break;

				case "delete":
					LBL_SAVEMODE.Text = "1";

					int res = 1;
					if (res <= this.DGR_PENDING.Items.Count)
					{
						res = this.DGR_PENDING.Items.Count + 1;
					}
					this.LBL_SEQ_ID.Text = res.ToString();

					conn.QueryString = "insert into PENDING_RFSCORECARDLINK (LOANPURPID, PRODUCTID, PROGRAM,scoreid,PENDINGSTATUS,seq) " + 
						" values ('" + e.Item.Cells[0].Text + "', '" + e.Item.Cells[2].Text + "', '" + e.Item.Cells[7].Text + "', '" + e.Item.Cells[5].Text + "', '2','" +LBL_SEQ_ID.Text+ "')";
					conn.ExecuteNonQuery();
					viewDataPending();
					break;

				default :
					// do nothing euy ....
					break;
			}
		}

		private void DGR_CURRENT_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try { DGR_CURRENT.CurrentPageIndex = e.NewPageIndex; } 
			catch { DGR_CURRENT.CurrentPageIndex = 0; }
			viewDataCurrent(DDL_PROGRAMID.SelectedValue,DDL_CA_ASPEK.SelectedValue);//,RDO_PROGRAMFLAG.SelectedValue.Trim());
		}

		private void DGR_PENDING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			resetStatus();

			switch (e.CommandName) 
			{
				case "edit" :
					LBL_SAVEMODE.Text = "0";

					DDL_PROGRAMID.Enabled = false;
					try { DDL_PROGRAMID.SelectedValue = e.Item.Cells[0].Text; } 
					catch { DDL_PROGRAMID.Enabled = true; }
					try { DDL_CA_ASPEK.SelectedValue = e.Item.Cells[2].Text; } 
					catch {}
					break;

				case "delete" :
					//LBL_SAVEMODE.Text = "2";

					conn.QueryString = "delete from PENDING_RFSCORECARDLINK where loanpurpid = '" + e.Item.Cells[0].Text + "' and productid = '" + e.Item.Cells[2].Text + "' and program = '" + e.Item.Cells[10].Text + "' and scoreid = '" + e.Item.Cells[5].Text + "'";
					conn.ExecuteNonQuery();
					viewDataPending();
					break;

				default :
					break;
			}
		}

		private void DGR_PENDING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try { DGR_PENDING.CurrentPageIndex = e.NewPageIndex; } 
			catch { DGR_PENDING.CurrentPageIndex = 0; }
			viewDataPending();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}
	}
}
