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

namespace CuBES_Maintenance.Parameter.Area
{
	/// <summary>
	/// Summary description for Branch.
	/// </summary>
	public partial class Branch : System.Web.UI.Page
	{
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);

			if (!IsPostBack)
			{
				DDL_AREAID.Items.Add(new ListItem("- SELECT -", ""));
				DDL_CITYID.Items.Add(new ListItem("- SELECT -", ""));
				DDL_BRANCH_TYPE.Items.Add(new ListItem("- SELECT -", ""));
				DDL_BRANCH_TYPE.Items.Add(new ListItem("Community Branch", "0"));
				DDL_BRANCH_TYPE.Items.Add(new ListItem("Scoring Branch", "1"));

				conn.QueryString = "select areaid, areaname from rfarea where active = '1'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_AREAID.Items.Add(new ListItem(conn.GetFieldValue(i, "areaname"), conn.GetFieldValue(i, "areaid")));

				DisableAll();
				LoadGrid();
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
			this.DatGrd.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGrd_ItemCommand);
			this.DatGrd.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGrd_PageIndexChanged);

		}
		#endregion

		private void LoadGrid()
		{
			conn.QueryString = "select branch_code, branch_name, cityname, areaname, regiondesc from vw_maintenance_branch";
			conn.ExecuteQuery();
			DataTable dt = conn.GetDataTable().Copy();
			DatGrd.DataSource = dt;
			DatGrd.DataBind();
		}

		protected void DDL_AREAID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DDL_CITYID.Items.Clear();
			DDL_CITYID.Items.Add(new ListItem("- SELECT -", ""));

			conn.QueryString = "select cityid, cityname from rfcity where areaid = '" + DDL_AREAID.SelectedValue + "' and active = '1'";
			conn.ExecuteQuery();
			for (int i = 0; i < conn.GetRowCount(); i++)
				DDL_CITYID.Items.Add(new ListItem(conn.GetFieldValue(i, "cityname"), conn.GetFieldValue(i, "cityid")));
		}

		private void DatGrd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DatGrd.CurrentPageIndex = e.NewPageIndex;
			LoadGrid();
		}

		protected void BTN_NEW_Click(object sender, System.EventArgs e)
		{
			CHK_ISNEW.Checked = true;
			EnableAll();
			BTN_SUBMIT.Visible = true;
			BTN_CANCEL.Visible = true;
			BTN_NEW.Visible = false;
		}

		private void EnableAll()
		{
			TXT_BRANCH_CODE.Enabled		= true;
			TXT_BRANCH_CODE.ReadOnly	= false;
			TXT_BRANCH_NAME.Enabled		= true;
			TXT_BR_ADDR.Enabled			= true;
			TXT_BR_BRANCHAREA.Enabled	= true;
			DDL_AREAID.Enabled			= true;
			DDL_CITYID.Enabled			= true;
			CHK_BOOKINGBRANCH.Enabled	= true;
			CHK_ISBRANCH.Enabled		= true;
			TXT_CBC.Enabled				= true;
			TXT_CCOBRANCH.Enabled		= true;
			BTN_SEARCH_CBC.Disabled		= false;
			BTN_SEARCH_CCOBRANCH.Disabled = false;
		}

		private void DisableAll()
		{
			TXT_BRANCH_CODE.Enabled		= false;
			TXT_BRANCH_CODE.ReadOnly	= true;
			TXT_BRANCH_NAME.Enabled		= false;
			TXT_BR_ADDR.Enabled			= false;
			TXT_BR_BRANCHAREA.Enabled	= false;
			DDL_AREAID.Enabled			= false;
			DDL_CITYID.Enabled			= false;
			CHK_BOOKINGBRANCH.Enabled	= false;
			CHK_ISBRANCH.Enabled		= false;
			TXT_CBC.Enabled				= false;
			TXT_CCOBRANCH.Enabled		= false;
			BTN_SEARCH_CBC.Disabled		= true;
			BTN_SEARCH_CCOBRANCH.Disabled = true;
		}

		private void DatGrd_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataTable dtTemp;

			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					BTN_NEW.Visible = false;
					BTN_SUBMIT.Visible = true;
					BTN_CANCEL.Visible = true;
					CHK_ISNEW.Checked = false;

					conn.QueryString = "select * from vw_maintenance_branch where branch_code = '" + e.Item.Cells[0].Text + "'";
					conn.ExecuteQuery();
					dtTemp = conn.GetDataTable().Copy();

					if (conn.GetRowCount() > 0)
					{
						EnableAll();
						TXT_BRANCH_CODE.ReadOnly = true;
						TXT_BRANCH_CODE.Text	= dtTemp.Rows[0]["branch_code"].ToString();
						TXT_BRANCH_NAME.Text	= dtTemp.Rows[0]["branch_name"].ToString();
						TXT_BR_ADDR.Text		= dtTemp.Rows[0]["br_addr"].ToString();
						TXT_BR_BRANCHAREA.Text	= dtTemp.Rows[0]["br_brancharea"].ToString();
						DDL_AREAID.SelectedValue = dtTemp.Rows[0]["areaid"].ToString();

						DDL_CITYID.Items.Clear();
						DDL_CITYID.Items.Add(new ListItem("- SELECT -", ""));

						conn.QueryString = "select cityid, cityname from rfcity where areaid = '" + dtTemp.Rows[0]["areaid"].ToString() + "' and active = '1'";
						conn.ExecuteQuery();
						for (int i = 0; i < conn.GetRowCount(); i++)
							DDL_CITYID.Items.Add(new ListItem(conn.GetFieldValue(i, "cityname"), conn.GetFieldValue(i, "cityid")));

						try
						{
							DDL_CITYID.SelectedValue = dtTemp.Rows[0]["cityid"].ToString();
						}
						catch {}

						if (dtTemp.Rows[0]["br_isbranch"].ToString() == "1")
							CHK_ISBRANCH.Checked = true;
						if (dtTemp.Rows[0]["br_isbookingbranch"].ToString() == "1")
							CHK_BOOKINGBRANCH.Checked = true;

						TXT_CBC.Text	= dtTemp.Rows[0]["cbc_name"].ToString();
						TXT_CBC_CODE.Text	= dtTemp.Rows[0]["cbc_code"].ToString();
						TXT_CCOBRANCH.Text	= dtTemp.Rows[0]["ccobranch_name"].ToString();
						TXT_BR_CCOBRANCH.Text	= dtTemp.Rows[0]["br_ccobranch"].ToString();
					}
					break;
				case "delete":
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BTN_NEW.Visible = true;
			BTN_SUBMIT.Visible = false;
			BTN_CANCEL.Visible = false;

			ClearAll();
			DisableAll();
		}

		private void ClearAll()
		{
			TXT_BRANCH_CODE.Text		= "";
			TXT_BRANCH_NAME.Text		= "";
			TXT_BR_ADDR.Text			= "";
			TXT_BR_BRANCHAREA.Text		= "";
			DDL_AREAID.SelectedValue	= "";
			DDL_CITYID.SelectedValue	= "";
			CHK_BOOKINGBRANCH.Checked	= false;
			CHK_ISBRANCH.Checked		= false;
			TXT_CBC.Text				= "";
			TXT_CBC_CODE.Text			= "";
			TXT_CCOBRANCH.Text			= "";
			TXT_BR_CCOBRANCH.Text		= "";
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			string isBranch = "0", isBookingBranch = "0", pendingStatus = "0", regionID = "";

			if (CHK_BOOKINGBRANCH.Checked == true)
				isBookingBranch = "1";
			if (CHK_ISBRANCH.Checked == true)
				isBranch = "1";
			if (CHK_ISNEW.Checked == true)
				pendingStatus = "1";

			conn.QueryString = "select regionid from map_arearegion where areaid = '" + DDL_AREAID.SelectedValue + "'";
			conn.ExecuteQuery();
			regionID = conn.GetFieldValue("regionid");

			conn.QueryString = "select count(*) from pending_rfbranch where branch_code = '" + TXT_BRANCH_CODE.Text + "'";
			conn.ExecuteQuery();
			if (conn.GetFieldValue(0,0) != "0")
			{
				Response.Write("<script language='javascript'>alert('Branch Code: " + TXT_BRANCH_CODE.Text + " is currently awaiting approval! Request Denied!');</script>");
			}
			else
			{
				conn.QueryString = "insert into pending_rfbranch (branch_code, branch_name, br_addr, br_brancharea, areaid, cityid, br_isbranch, br_isbookingbranch, cbc_code, br_ccobranch, pendingstatus, regionid) " + 
					"values ('" + TXT_BRANCH_CODE.Text + "', '" + TXT_BRANCH_NAME.Text + "', '" + TXT_BR_ADDR.Text + "', '" + TXT_BR_BRANCHAREA.Text + "', " + GlobalTools.ConvertNull(DDL_AREAID.SelectedValue) + ", " + GlobalTools.ConvertNull(DDL_CITYID.SelectedValue) + ", '" + 
					isBranch + "', '" + isBookingBranch + "', " + GlobalTools.ConvertNull(TXT_CBC_CODE.Text) + ", " + GlobalTools.ConvertNull(TXT_BR_CCOBRANCH.Text) + ", '" + pendingStatus + "', " + GlobalTools.ConvertNull(regionID) + ")";
				conn.ExecuteQuery();
			}

			ClearAll();
			DisableAll();

			BTN_NEW.Visible = true;
			BTN_SUBMIT.Visible = false;
			BTN_CANCEL.Visible = false;
		}
	}
}
