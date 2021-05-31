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
	/// Summary description for GRPAccessLoanHistory.
	/// </summary>
	public partial class GRPAccessLoanHistory : System.Web.UI.Page
	{

		private Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if (!IsPostBack) 
			{
				fillDropDowns();	
				viewExisting();
				viewRequest();				
			}

			BTN_SAVE.Attributes.Add("onclick", "if(!cek_mandatory(document.Form1)) { return false; };");
		}

		/// <summary>
		/// Mengisi DropDown
		/// </summary>
		private void fillDropDowns() 
		{
			/// Mengisi GroupID
			/// 
			GlobalTools.fillRefList(DDL_GROUPID, "select groupid, sg_grpname from scgroup where sg_active = 1 order by sg_grpname", false, conn);

			/// Mengisi BusinessUnitID
			/// 
			GlobalTools.fillRefList(DDL_BUSSUNITID, "select bussunitid, bussunitdesc from rfbusinessunit where active = 1", false, conn);

			/// Mengisi Filter
			/// 
			GlobalTools.fillRefList(DDL_FILTERID, "select filterid, filterdesc from rfloanhisfilter", false, conn);
		}


		/// <summary>
		/// Membersihkan kembali field entries
		/// </summary>
		private void clearControls() 
		{
			DDL_GROUPID.SelectedValue = "";
			DDL_BUSSUNITID.SelectedValue = "";
			DDL_FILTERID.SelectedValue = "";
		}

		/// <summary>
		/// Menampilkan data pending pada datagrid
		/// </summary>
		private void viewRequest() 
		{
			conn.QueryString = "select * from VW_PENDING_GRPACCESSLOANHISTORY order by GROUPID";
			conn.ExecuteQuery();

			DGR_PENDING.DataSource = conn.GetDataTable().DefaultView;
			try 
			{
				DGR_PENDING.DataBind();
			} 
			catch 
			{
				DGR_PENDING.CurrentPageIndex = DGR_PENDING.PageCount - 1;
				DGR_PENDING.DataBind();
			}
		}

		/// <summary>
		/// Menampilkan data existing pada datagrid
		/// </summary>
		private void viewExisting() 
		{
			conn.QueryString = "select * from VW_GRPACCESSLOANHISTORY order by GROUPID";
			conn.ExecuteQuery();

			DGR_EXIST.DataSource = conn.GetDataTable().DefaultView;
			try 
			{
				DGR_EXIST.DataBind();
			} 
			catch 
			{
				DGR_EXIST.CurrentPageIndex = DGR_EXIST.PageCount - 1;
				DGR_EXIST.DataBind();
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

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();

			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim() == "1")	// mode INSERT
			{
				conn.QueryString = "select GROUPID from GRPACCESSLOANHISTORY " + 
					" where GROUPID = '" + DDL_GROUPID.SelectedValue + 
					"' and FILTERID = '" + DDL_FILTERID.SelectedValue + 
					"' AND BUSSUNITID = '" + DDL_BUSSUNITID.SelectedValue + "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					GlobalTools.popMessage(this, "Group Acccess already exist! Request canceled!");
					return;
				}
			}				

			conn.QueryString = "exec SU_GRPACCESSLOANHISTORY_MAKER '" + LBL_SAVEMODE.Text + 
				"', '" + DDL_GROUPID.SelectedValue + 
				"', '" + DDL_FILTERID.SelectedValue + 
				"', '" + DDL_BUSSUNITID.SelectedValue + "'";
			conn.ExecuteNonQuery();

			viewRequest();
			clearControls();

			LBL_SAVEMODE.Text = "1";
		}
	}
}
