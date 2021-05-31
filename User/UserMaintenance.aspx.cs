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
using System.Web.Security;
using DMS.DBConnection;
using System.Configuration;

namespace CuBES_Maintenance.User
{
	/// <summary>
	/// Summary description for UserMaintenance.
	/// </summary>
	public partial class UserMaintenance : System.Web.UI.Page
	{
		protected Connection conn;
		protected string SqlStatement = "select userid, su_fullname, groupid, sg_grpname, su_logon, su_revoke, modulename, moduleid, su_active, active from vw_scalluser ";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());

			if (!IsPostBack)
			{
				DDL_RFMODULE.Items.Add(new ListItem("- PILIH -", ""));
				DDL_RFAREA.Items.Add(new ListItem("- PILIH -", ""));
				DDL_RFGROUP.Items.Add(new ListItem("- PILIH -", ""));
				DDL_GROUPID.Items.Add(new ListItem("- PILIH -", ""));
				DDL_JB_CODE.Items.Add(new ListItem("- PILIH -", ""));
				DDL_DEPT_CODE.Items.Add(new ListItem("- PILIH -", ""));
				DDL_RFBRANCH.Items.Add(new ListItem("- PILIH -", ""));

				HyperLink1.NavigateUrl = "UserMaintenance.aspx?mc=" + Request.QueryString["mc"];
				HyperLink2.NavigateUrl = "GroupMaintenance.aspx?mc=" + Request.QueryString["mc"];

				conn.QueryString = "select moduleid, modulename from rfmodule where active = '1'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_RFMODULE.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));

				conn.QueryString = "select areaid, areaname from rfarea where active = '1'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_RFAREA.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));

				conn.QueryString = "select branch_code, branch_code + ' - ' + branch_name from rfbranch where active = '1' order by branch_code";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_RFBRANCH.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));

				conn.QueryString = "select jb_code, jb_desc from rfjabatan where active='1'  order by jb_desc";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_JB_CODE.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));

				conn.QueryString = "select dept_code, dept_desc from rfdepartmentcode where active='1' order by dept_desc";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_DEPT_CODE.Items.Add(new ListItem(conn.GetFieldValue(i,0) + " - " + conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));

				conn.QueryString = "select distinct groupid, sg_grpname from vw_scallgroup where (moduleid is not null and moduleid <> '') order by sg_grpname";// order by modulename, sg_grpname"; where moduleid = '" + DDL_MODULEID.SelectedValue + "'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
				{
					DDL_RFGROUP.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
					DDL_GROUPID.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
				}

				// Set all module specific setting to invisible
				TableSME.Visible		= false;
				TableCC.Visible			= false;
				TableConsumer.Visible	= false;
				TableSales.Visible		= false;

				conn.QueryString = "select userid, su_fullname, groupid, sg_grpname, su_logon, su_revoke from vw_scalluser where su_active='3'";
				conn.ExecuteQuery();
				DataTable dt = conn.GetDataTable().Copy();
				DatGrd.DataSource = dt;
				DatGrd.DataBind();

				SetDisable();
			}

			BTN_SAVE.Attributes.Add("onclick", "if(!cek_mandatory(document.getElementById('Form1'))){return false;};");
			//LBL_RESULT.Text = "";
		}

		private void FillGrid()
		{
			string sql = "WHERE", sjoin = "AND";
			if (DDL_RFMODULE.SelectedValue != "")
				sql = sql + " MODULEID = '" + DDL_RFMODULE.SelectedValue + "' " + sjoin;
			if (DDL_RFAREA.SelectedValue != "")
				sql = sql + " AREAID = '" + DDL_RFAREA.SelectedValue + "' " + sjoin;
			if (DDL_RFBRANCH.SelectedValue != "")
				sql = sql + " SU_BRANCH = '" + DDL_RFBRANCH.SelectedValue + "' " + sjoin;
			//sql = sql + " BRANCH like '%" + TXT_RFBRANCH.Text + "%' " + sjoin;
			if (DDL_RFGROUP.SelectedValue != "")
				sql = sql + " GROUPID = '" + DDL_RFGROUP.SelectedValue + "' " + sjoin;
			if (TXT_SEARCH_USERID.Text.Trim() != "")
				sql = sql + " USERID like '%" + TXT_SEARCH_USERID.Text.Trim() + "%' " + sjoin;
			if (TXT_SEARCH_USERNAME.Text.Trim() != "")
				sql = sql + " SU_FULLNAME like '%" + TXT_SEARCH_USERNAME.Text.Trim() + "%' " + sjoin;
			if (TXT_SEARCH_UPLINER.Text.Trim() != "")
			{
				string field = RDL_UPLINER.SelectedValue;
				if (field == "")
					/*sql = sql + " (SU_UPLINER+' '+SU_UNAME like '%" + TXT_SEARCH_UPLINER.Text.Trim() +
					"%' or SU_UPLINER_CON+' '+SU_UNAME_CON like '%" + TXT_SEARCH_UPLINER.Text.Trim() +
					"%' or SU_UPLINER_CC+' '+SU_UNAME_CC like '%" + TXT_SEARCH_UPLINER.Text.Trim() + "%') " + sjoin;
					*/
					sql = sql + " (ISNULL(SU_TEAMLEADER ,'') + ' ' + ISNULL(SU_UPLINER,'') + ' ' + ISNULL(SU_MDLUPLINER,'') + ' ' " +
						" + ISNULL(SU_CORUPLINER,'') + ' ' + ISNULL(SU_CRGUPLINER,'') + ' ' + ISNULL(SU_MCRUPLINER,'') + ' ' + ISNULL(SU_MITRARM,'') + ' ' + ISNULL(SU_UPLINER_CON,'') + ' ' " +
						" + ISNULL(SU_MITRARM_CON,'') + ' ' + ISNULL(SU_UPLINER_CC,'') + ' ' + ISNULL(SU_MITRARM_CC,'') " +
						" LIKE '%" + TXT_SEARCH_UPLINER.Text.Trim() + "%') " + sjoin; //search all users related to this *userid*
				else
					sql = sql + " " + field + " like '%" + TXT_SEARCH_UPLINER.Text.Trim() + "%' " + sjoin;
			}
			if (TXT_SEARCH_OFFICERCODE.Text.Trim() != "")
				sql = sql + " OFFICER_CODE = '" + TXT_SEARCH_OFFICERCODE.Text.Trim() + "' " + sjoin;

			if (sql == "WHERE")
				sql = "";
			else
				sql = sql.Substring(0, sql.Length - sjoin.Length) ; //+ " AND ACTIVE = '1'";

			LBL_SqlStatement.Text = SqlStatement + sql;
		}

		private void BindData()
		{
			//conn.QueryString = "select userid, su_fullname, groupid, sg_grpname, su_logon, su_revoke, modulename from vw_scalluser " + sql + " order by su_fullname";
			conn.QueryString = LBL_SqlStatement.Text;
			conn.ExecuteQuery();
			DataTable dt = conn.GetDataTable().Copy();
			DatGrd.DataSource = dt;
			try
			{
				DatGrd.DataBind();
			} 
			catch
			{
				DatGrd.CurrentPageIndex = 0;
				DatGrd.DataBind();
			}

			CheckBox cb;
			LinkButton lnk;

			for (int i = 0; i < DatGrd.Items.Count; i++)
			{
				if (DatGrd.Items[i].Cells[6].Text == "1")	//su_logon
				{
					cb = (CheckBox)DatGrd.Items[i].Cells[9].FindControl("CheckBox1");
					cb.Checked = true;
				}
				if (DatGrd.Items[i].Cells[7].Text == "1")	//su_revoke
				{
					cb = (CheckBox)DatGrd.Items[i].Cells[10].FindControl("CheckBox2");
					cb.Checked = true;
				}
				if (DatGrd.Items[i].Cells[8].Text == "1")	//su_active		-- user active
				{
					cb = (CheckBox)DatGrd.Items[i].Cells[11].FindControl("CheckBox3");
					cb.Checked = true;
				}
				else
				{
					DatGrd.Items[i].Cells[2].ForeColor = Color.Green;		//userid
					DatGrd.Items[i].Cells[3].ForeColor = Color.Green;		//fullname
				}
				if (DatGrd.Items[i].Cells[13].Text == "0")	//active		-- user deleted
				{
					lnk = (LinkButton)DatGrd.Items[i].Cells[12].FindControl("lnkDelete");
					lnk.Visible = false;
					lnk = (LinkButton)DatGrd.Items[i].Cells[12].FindControl("lnkEdit");
					lnk.Visible = false;
					DatGrd.Items[i].Cells[2].ForeColor = Color.Gray;		//userid
					DatGrd.Items[i].Cells[3].ForeColor = Color.Gray;		//fullname
				}
				else
				{
					lnk = (LinkButton)DatGrd.Items[i].Cells[12].FindControl("lnkUndelete");
					lnk.Visible = false;
				}
			}

            for (int i = 1; i < DatGrd.Items.Count; i++)
            {
                if (DatGrd.Items[i - 1].Cells[2].Text == DatGrd.Items[i].Cells[2].Text)
                {
                    if (DatGrd.Items[i - 1].Cells[2].RowSpan == 0)
                    {
                        DatGrd.Items[i - 1].Cells[2].RowSpan += 2;
                        DatGrd.Items[i].Cells[2].Visible = false;

                        DatGrd.Items[i - 1].Cells[3].RowSpan += 2;
                        DatGrd.Items[i].Cells[3].Visible = false;

                        DatGrd.Items[i - 1].Cells[5].RowSpan += 2;
                        DatGrd.Items[i].Cells[5].Visible = false;
                    }
                }
            }
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			base.OnInit(e);
            if (!this.DesignMode)
            {
                InitializeComponent();
            }
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.DatGrd.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGrd_ItemCommand);
			this.DatGrd.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGrd_PageIndexChanged);
			this.DatGrd.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DatGrd_SortCommand);
			this.PreRender += new System.EventHandler(this.UserMaintenance_PreRender);

		}
		#endregion

		protected void BTN_SEARCH_Click(object sender, System.EventArgs e)
		{
			DatGrd.CurrentPageIndex = 0;
			FillGrid();
			BindData();
		}

		private void DatGrd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DatGrd.CurrentPageIndex = e.NewPageIndex;
			FillGrid();
			BindData();
		}

		private void BTN_SEARCH_TL_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>window.open('SearchUser.aspx?targetFormID=Form1&targetObjectID=TXT_SU_TEAMLEADER','SearchUser','status=no,scrollbars=no,width=450,height=275');</script>");
		}

		protected void TXT_SU_TEAMLEADER_TextChanged(object sender, System.EventArgs e)
		{
			conn.QueryString = "select su_fullname from scalluser where userid = '" + TXT_SU_TEAMLEADER.Text + "'";
			conn.ExecuteQuery();
			try
			{
				TXT_TEAMLEADER.Text = conn.GetFieldValue(0,0);
			}
			catch {}
		}

		protected void TXT_SU_MIDUPLINER_TextChanged(object sender, System.EventArgs e)
		{
			conn.QueryString = "select su_fullname from scalluser where userid = '" + TXT_SU_MIDUPLINER.Text + "'";
			conn.ExecuteQuery();
			try
			{
				TXT_MIDUPLINER.Text = conn.GetFieldValue(0,0);
			}
			catch {}
		}

		protected void TXT_SU_UPLINER_TextChanged(object sender, System.EventArgs e)
		{
			conn.QueryString = "select su_fullname from scalluser where userid = '" + TXT_SU_UPLINER.Text + "'";
			conn.ExecuteQuery();
			try
			{
				TXT_UPLINER.Text = conn.GetFieldValue(0,0);
			}
			catch {}
		}

		protected void TXT_SU_MITRARM_TextChanged(object sender, System.EventArgs e)
		{
			conn.QueryString = "select su_fullname from scalluser where userid = '" + TXT_SU_MITRARM.Text + "'";
			conn.ExecuteQuery();
			try
			{
				TXT_MITRARM.Text = conn.GetFieldValue(0,0);
			}
			catch {}
		}

		private void DatGrd_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			FillGrid();
			LBL_SqlStatement.Text = /* SqlStatement */ LBL_SqlStatement.Text + " ORDER BY " + e.SortExpression;
			BindData();
		}

		private void DatGrd_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					BTN_SAVE.Visible = true;
					BTN_CANCEL.Visible = true;
					BTN_NEW.Visible = false;
					CHK_ISNEW.Checked = false;
					
					//conn.QueryString = "exec SCUSER_EDIT '" + e.Item.Cells[2].Text + "', 'VW_SCUSER'";
					conn.QueryString = "select * from vw_scuser_edit where userid = '" + e.Item.Cells[2].Text + "'";
					conn.ExecuteQuery();

					if (conn.GetFieldValue("SU_REVOKE") == "1")
					{
						cb_revoke.Checked = true;
						cb_revoke.Text = "(clear to reset)";
					}
					else cb_revoke.Checked = false;

					if (conn.GetFieldValue("SU_LOGON") == "1")
						cb_logon.Checked = true;
					else
						cb_logon.Checked = false;

					SetEnable();		//set cb_logon before calling this function 
					TXT_USERID.ReadOnly = true;

					TXT_USERID.Text				= conn.GetFieldValue("USERID");
					TXT_SU_FULLNAME.Text		= conn.GetFieldValue("SU_FULLNAME");
					TXT_SU_NIP.Text				= conn.GetFieldValue("SU_NIP");
					TXT_SU_HPNUM.Text			= conn.GetFieldValue("SU_HPNUM");
					TXT_SU_EMAIL.Text			= conn.GetFieldValue("SU_EMAIL");
					TXT_OFFICER_CODE.Text		= conn.GetFieldValue("OFFICER_CODE");
					DDL_JB_CODE.SelectedValue	= conn.GetFieldValue("JB_CODE");
					DDL_GROUPID.SelectedValue	= conn.GetFieldValue("GROUPID");
					TXT_SU_BRANCH.Text			= conn.GetFieldValue("SU_BRANCH");
					TXT_BRANCH.Text				= conn.GetFieldValue("BRANCH_NAME");
					TXT_TEAMLEADER.Text			= conn.GetFieldValue("TEAMLEADER");
					TXT_SU_TEAMLEADER.Text		= conn.GetFieldValue("SU_TEAMLEADER");
					TXT_UPLINER.Text			= conn.GetFieldValue("UPLINER");
					TXT_SU_UPLINER.Text			= conn.GetFieldValue("SU_UPLINER");
					TXT_MIDUPLINER.Text			= conn.GetFieldValue("MIDUPLINER");
					TXT_CORUPLINER.Text			= conn.GetFieldValue("CORUPLINER");
					TXT_CRGUPLINER.Text			= conn.GetFieldValue("CRGUPLINER");
					TXT_MCRUPLINER.Text			= conn.GetFieldValue("MCRUPLINER");
					TXT_SU_MIDUPLINER.Text		= conn.GetFieldValue("SU_MIDUPLINER");
					TXT_SU_CORUPLINER.Text		= conn.GetFieldValue("SU_CORUPLINER");
					TXT_SU_CRGUPLINER.Text		= conn.GetFieldValue("SU_CRGUPLINER");
					TXT_SU_MCRUPLINER.Text		= conn.GetFieldValue("SU_MCRUPLINER");
					TXT_MITRARM.Text			= conn.GetFieldValue("MITRARM");
					DDL_DEPT_CODE.SelectedValue	= conn.GetFieldValue("SU_DEPTCODE");
					TXT_SU_MITRARM.Text			= conn.GetFieldValue("SU_MITRARM");
					TXT_SU_APRVLIMIT.Text		= GlobalTools.MoneyFormat(conn.GetFieldValue("SU_APRVLIMIT"));
					TXT_SU_EMASLIMIT.Text		= GlobalTools.MoneyFormat(conn.GetFieldValue("SU_EMASLIMIT"));
					TXT_UPLINER_CON.Text		= conn.GetFieldValue("UPLINER_CON");
					TXT_SU_UPLINER_CON.Text		= conn.GetFieldValue("SU_UPLINER_CON");
					TXT_MITRARM_CON.Text		= conn.GetFieldValue("MITRARM_CON");
					TXT_SU_MITRARM_CON.Text		= conn.GetFieldValue("SU_MITRARM_CON");
					TXT_UPLINER_CC.Text			= conn.GetFieldValue("UPLINER_CC");
					TXT_SU_UPLINER_CC.Text		= conn.GetFieldValue("SU_UPLINER_CC");
					TXT_MITRARM_CC.Text			= conn.GetFieldValue("MITRARM_CC");
					TXT_SU_MITRARM_CC.Text		= conn.GetFieldValue("SU_MITRARM_CC");
					TXT_AGENTID.Text			= conn.GetFieldValue("AGENTID");
					TXT_AGENTNAME.Text			= conn.GetFieldValue("AGENTNAME");
					TXT_SALES_ID.Text			= conn.GetFieldValue("SALES_ID");
					TXT_SALES_NAME.Text			= conn.GetFieldValue("SALES_NAME");
					TXT_SU_OWNUNIT.Text			= conn.GetFieldValue("SU_OWNUNIT");
					TXT_OWNUNIT.Text			= conn.GetFieldValue("OWNUNIT_NAME");
					TXT_SU_SYSUNIT.Text			= conn.GetFieldValue("SU_SYSUNIT");
					TXT_SYSUNIT.Text			= conn.GetFieldValue("SYSUNIT_NAME");

					if (e.Item.Cells[8].Text == "1")
						CHK_SU_ACTIVE.Checked = true;
					else 
						CHK_SU_ACTIVE.Checked = false;

					TableSME.Visible = false;
					TableCC.Visible = false;
					TableConsumer.Visible = false;
					TableSales.Visible = false;

					conn.QueryString = "select moduleid, sg_apprsta from vw_grpaccessmodule where groupid = '" + e.Item.Cells[4].Text + "'";
					conn.ExecuteQuery();
					DataTable dtTemporary = conn.GetDataTable().Copy();

					for (int i = 0; i < dtTemporary.Rows.Count; i++)
					{
						/// SME
						/// 
						if (dtTemporary.Rows[i]["moduleid"].ToString() == "01") 
						{
							TableSME.Visible = true;

							TXT_SU_APRVLIMIT.CssClass = "";
							TXT_SU_EMASLIMIT.CssClass = "";

							/// If Group chosen is an Approval Group, then approval limit field and 
							/// emas limit must be filled
							if (conn.GetFieldValue(i, "sg_apprsta") == "1") 
							{
								TXT_SU_APRVLIMIT.CssClass = "mandatory";
								TXT_SU_EMASLIMIT.CssClass = "mandatory";
							}
						}
						if (dtTemporary.Rows[i]["moduleid"].ToString() == "20")
						{
							TableCC.Visible = true;
							conn.QueryString = "select rfcaregion.sc_id, rfarea.areaname from rfcaregion inner join rfarea on rfcaregion.region_id = rfarea.areaid where rfcaregion.sc_id = '" + TXT_USERID.Text + "'";
							conn.ExecuteQuery();
							for (int j = 0; j < conn.GetRowCount(); j++)
							{
								if (TXT_CC_AREA.Text.Trim() == "")
									TXT_CC_AREA.Text += conn.GetFieldValue(j, "areaname");
								else
									TXT_CC_AREA.Text += ", " + conn.GetFieldValue(j, "areaname");
							}
						}
						if (dtTemporary.Rows[i]["moduleid"].ToString() == "40")
							TableConsumer.Visible = true;
						if (dtTemporary.Rows[i]["moduleid"].ToString() == "50")
							TableSales.Visible = true;
					}

					/*
										for (int i = 0; i < conn.GetRowCount(); i++)
										{
											if (conn.GetFieldValue(i, "moduleid") == "01")
												TableSME.Visible = true;
											if (conn.GetFieldValue(i, "moduleid") == "20")
											{
												TableCC.Visible = true;
												conn.QueryString = "select rfcaregion.sc_id, rfarea.areaname from rfcaregion inner join rfarea on rfcaregion.region_id = rfarea.areaid where rfcaregion.sc_id = '" + TXT_USERID.Text + "'";
												conn.ExecuteQuery();
												for (int j = 0; j < conn.GetRowCount(); j++)
												{
													if (TXT_CC_AREA.Text.Trim() == "")
														TXT_CC_AREA.Text += conn.GetFieldValue(j, "areaname");
													else
														TXT_CC_AREA.Text += ", " + conn.GetFieldValue(j, "areaname");
												}
											}
											if (conn.GetFieldValue(i, "moduleid") == "40")
												TableConsumer.Visible = true;
											if (conn.GetFieldValue(i, "moduleid") == "50")
												TableSales.Visible = true;
										}
					*/
					pwdmsg.Value = "Leave password blank to use old password!";
					GlobalTools.SetFocus(this, BTN_CANCEL);
					break;
				case "delete":
					conn.QueryString = "exec SU_SCUSER_DELETE '" + 
						e.Item.Cells[2].Text + "', '" + 
						e.Item.Cells[4].Text + "', '" + 
						"2" + "', '" + "1" + "', '" + 
						e.Item.Cells[3].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					LBL_RESULT.Text = "Request Submitted! Awaiting Approval ... ";
					LBL_RESULT.ForeColor = System.Drawing.Color.Green;
					//Response.Write("<script language='javascript'>alert('Request Submitted! Awaiting approval...');</script>");
					break;
				case "undelete":
					conn.QueryString = "exec SU_SCUSER_UNDELETE '" + 
						e.Item.Cells[2].Text + "', '" + 
						e.Item.Cells[4].Text + "', '" + 
						"3" + "', '" + "1" + "', '" + 
						e.Item.Cells[3].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					LBL_RESULT.Text = "Request Submitted! Awaiting Approval ... ";
					LBL_RESULT.ForeColor = System.Drawing.Color.Green;
					//Response.Write("<script language='javascript'>alert('Request Submitted! Awaiting approval...');</script>");
					break;

			}
		}

		private void SetEnable()
		{
			TXT_USERID.Enabled		= true;
			TXT_SU_PWD.Enabled		= true;
			TXT_SU_NIP.Enabled		= true;
			TXT_SU_HPNUM.Enabled	= true;
			TXT_SU_EMAIL.Enabled	= true;
			TXT_OFFICER_CODE.Enabled= true;
			DDL_JB_CODE.Enabled		= true;
			DDL_GROUPID.Enabled		= true;
			TXT_SU_BRANCH.Enabled	= true;
			TXT_BRANCH.Enabled		= true;
			TXT_TEAMLEADER.Enabled	= true;
			TXT_UPLINER.Enabled		= true;
			TXT_MIDUPLINER.Enabled	= true;
			TXT_CORUPLINER.Enabled	= true;
			TXT_CRGUPLINER.Enabled	= true;
			TXT_MCRUPLINER.Enabled	= true;
			TXT_MITRARM.Enabled		= true;
			DDL_DEPT_CODE.Enabled	= true;
			TXT_SU_APRVLIMIT.Enabled= true;
			TXT_SU_EMASLIMIT.Enabled= true;
			TXT_SU_FULLNAME.Enabled = true;
			cb_revoke.Enabled		= true;
			//cb_logon.Enabled		= cb_logon.Checked;
			cb_logon.Enabled		= true;
			cb_resetpwd.Enabled		= true;
			CHK_SU_ACTIVE.Enabled	= true;
			TXT_SU_OWNUNIT.Enabled	= true;
			TXT_OWNUNIT.Enabled		= true;
			TXT_SU_SYSUNIT.Enabled	= true;
			TXT_SYSUNIT.Enabled		= true;

			BTN_SEARCH_TL.Disabled	= false;
			BTN_SEARCH_PAIR.Disabled= false;
			BTN_SEARCH_UPMIDDLE.Disabled = false;
			BTN_SEARCH_BRANCH.Disabled = false;
			BTN_SEARCH_UPSMALL.Disabled = false;
			BTN_SEARCH_UPCORP.Disabled = false;
			BTN_SEARCH_UPCRG.Disabled = false;
			BTN_SEARCH_UPMCR.Disabled = false;
			BTN_SEARCH_OWNUNIT.Disabled = false;
			BTN_SEARCH_SYSUNIT.Disabled = false;

			BTN_SRCH_CON_UPLINER.Disabled = false;
			BTN_SRCH_CON_USRPAIR.Disabled = false;
			BTN_CON_APRVLIMIT.Disabled = false;

			BTN_SRCH_CC_UPLINER.Disabled = false;
			BTN_SRCH_CC_USRPAIR.Disabled = false;
			BTN_CC_APRVLIMIT.Disabled = false;

			BTN_SRCH_SC_AGENCY.Disabled = false;
			BTN_SRCH_SC_SAGENT.Disabled = false;
		}

		private void SetDisable()
		{
			TXT_USERID.Enabled		= false;
			TXT_SU_FULLNAME.Enabled = false;
			TXT_SU_PWD.Enabled		= false;
			TXT_SU_NIP.Enabled		= false;
			TXT_SU_HPNUM.Enabled	= false;
			TXT_SU_EMAIL.Enabled	= false;
			TXT_OFFICER_CODE.Enabled= false;
			DDL_JB_CODE.Enabled		= false;
			DDL_GROUPID.Enabled		= false;
			TXT_SU_BRANCH.Enabled	= false;
			TXT_BRANCH.Enabled		= false;
			TXT_TEAMLEADER.Enabled	= false;
			TXT_UPLINER.Enabled		= false;
			TXT_MIDUPLINER.Enabled	= false;
			TXT_CORUPLINER.Enabled	= false;
			TXT_CRGUPLINER.Enabled	= false;
			TXT_MCRUPLINER.Enabled	= false;
			TXT_MITRARM.Enabled		= false;
			DDL_DEPT_CODE.Enabled	= false;
			TXT_SU_APRVLIMIT.Enabled= false;
			TXT_SU_EMASLIMIT.Enabled= false;
			cb_revoke.Enabled		= false;
			cb_logon.Enabled		= false;
			cb_resetpwd.Enabled		= false;
			CHK_SU_ACTIVE.Enabled	= false;
			TXT_SU_OWNUNIT.Enabled	= false;
			TXT_OWNUNIT.Enabled		= false;
			TXT_SU_SYSUNIT.Enabled	= false;
			TXT_SYSUNIT.Enabled		= false;

			BTN_SEARCH_TL.Disabled	= true;
			BTN_SEARCH_PAIR.Disabled	= true;
			BTN_SEARCH_UPMIDDLE.Disabled = true;
			BTN_SEARCH_BRANCH.Disabled = true;
			BTN_SEARCH_UPSMALL.Disabled = true;
			BTN_SEARCH_UPCORP.Disabled = true;
			BTN_SEARCH_UPCRG.Disabled = true;
			BTN_SEARCH_UPMCR.Disabled = true;
			BTN_SEARCH_OWNUNIT.Disabled = true;
			BTN_SEARCH_SYSUNIT.Disabled = true;

			BTN_SRCH_CON_UPLINER.Disabled = true;
			BTN_SRCH_CON_USRPAIR.Disabled = true;
			BTN_CON_APRVLIMIT.Disabled = true;

			BTN_SRCH_CC_UPLINER.Disabled = true;
			BTN_SRCH_CC_USRPAIR.Disabled = true;
			BTN_CC_APRVLIMIT.Disabled = true;

			BTN_SRCH_SC_AGENCY.Disabled = true;
			BTN_SRCH_SC_SAGENT.Disabled = true;
		}

		private void ClearEntries()
		{
			TXT_USERID.Text				= "";
			TXT_SU_PWD.Text				= "";
			TXT_SU_NIP.Text				= "";
			TXT_SU_HPNUM.Text			= "";
			TXT_SU_EMAIL.Text			= "";
			TXT_OFFICER_CODE.Text		= "";
			DDL_JB_CODE.SelectedValue	= "";
			DDL_GROUPID.SelectedValue	= "";
			TXT_SU_BRANCH.Text			= "";
			TXT_BRANCH.Text				= "";
			TXT_TEAMLEADER.Text			= "";
			TXT_SU_TEAMLEADER.Text		= "";
			TXT_UPLINER.Text			= "";
			TXT_SU_UPLINER.Text			= "";
			TXT_MIDUPLINER.Text			= "";
			TXT_CORUPLINER.Text			= "";
			TXT_CRGUPLINER.Text			= "";
			TXT_MCRUPLINER.Text			= "";
			TXT_SU_MIDUPLINER.Text		= "";
			TXT_SU_CORUPLINER.Text		= "";
			TXT_SU_CRGUPLINER.Text		= "";
			TXT_SU_MCRUPLINER.Text		= "";
			TXT_MITRARM.Text			= "";
			DDL_DEPT_CODE.SelectedValue = "";
			TXT_SU_MITRARM.Text			= "";
			TXT_UPLINER_CON.Text		= "";
			TXT_SU_UPLINER_CON.Text		= "";
			TXT_MITRARM_CON.Text		= "";
			TXT_SU_MITRARM_CON.Text		= "";
			TXT_UPLINER_CC.Text			= "";
			TXT_SU_UPLINER_CC.Text		= "";
			TXT_MITRARM_CC.Text			= "";
			TXT_SU_MITRARM_CC.Text		= "";
			TXT_SU_APRVLIMIT.Text		= "";
			TXT_SU_EMASLIMIT.Text		= "";
			TXT_SU_FULLNAME.Text		= "";
			TXT_AGENTID.Text			= "";
			TXT_AGENTNAME.Text			= "";
			TXT_SALES_ID.Text			= "";
			TXT_SALES_NAME.Text			= "";
			TXT_CC_AREA.Text			= "";
			cb_revoke.Text				= "(check for yes)";
			cb_revoke.Checked			= false;
			cb_logon.Checked			= false;
			cb_resetpwd.Checked			= false;
			CHK_SU_ACTIVE.Checked		= true;
			LBL_AREANAME.Text			= "";
			TXT_SU_OWNUNIT.Text			= "";
			TXT_OWNUNIT.Text			= "";
			TXT_SU_SYSUNIT.Text			= "";
			TXT_SYSUNIT.Text			= "";
		}

		private void clearSearch()
		{
			try
			{
				DDL_RFMODULE.SelectedValue = "";
			} 
			catch {}
			try
			{
				DDL_RFAREA.SelectedValue = "";
			} 
			catch {}
			try
			{
				DDL_RFBRANCH.SelectedValue = "";
			} 
			catch {}
			try
			{
				DDL_RFGROUP.SelectedValue = "";
			} 
			catch {}
			TXT_SEARCH_USERID.Text = "";
			TXT_SEARCH_USERNAME.Text = "";
			TXT_SEARCH_UPLINER.Text = "";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BTN_SAVE.Visible = false;
			BTN_NEW.Visible = true;
			BTN_CANCEL.Visible = false;

			TableSME.Visible		= false;
			TableConsumer.Visible	= false;
			TableCC.Visible			= false;
			TableSales.Visible		= false;

			ClearEntries();
			SetDisable();
		}

		protected void BTN_NEW_Click(object sender, System.EventArgs e)
		{
			BTN_NEW.Visible = false;
			BTN_SAVE.Visible = true;
			BTN_CANCEL.Visible = true;
			CHK_ISNEW.Checked = true;
			CHK_SU_ACTIVE.Checked = true;

			SetEnable();
			TXT_USERID.ReadOnly = false;

			pwdmsg.Value = "Leave password blank to use default password!";
			GlobalTools.SetFocus(this, BTN_CANCEL);
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string password = "";

			conn.QueryString = "select isnull(minpwdlength,0) minpwdlength from app_parameter";
			conn.ExecuteQuery();
			if ((TXT_SU_PWD.Text.Trim().Length >= int.Parse(conn.GetFieldValue("minpwdlength"))) || (TXT_SU_PWD.Text.Trim().Length == 0))
			{
				if (TXT_SU_PWD.Text.Trim() == TXT_VERIFYPWD.Text.Trim())
				{
					BTN_SAVE.Visible = false;
					BTN_CANCEL.Visible = false;
					BTN_NEW.Visible = true;
					string flag = "0";
					string revoke = "0";
					string reset_logon = "0";
					string suActive = "1";

					if (CHK_ISNEW.Checked == true)
						flag = "1";

					if (cb_revoke.Checked == true)
						revoke = "1";
					else
						revoke = "0";
				
					if (cb_logon.Enabled && !cb_logon.Checked)
						reset_logon = "1";
					else
						reset_logon = "0";

					if (CHK_SU_ACTIVE.Checked == true)
						suActive = "1";
					else
						suActive = "0";

					if (cb_resetpwd.Checked)
					{
						conn.QueryString = "select def_pwd from app_parameter";
						conn.ExecuteQuery();
						password = conn.GetFieldValue(0,0);
					}	
					else password = TXT_SU_PWD.Text.Trim();

					if ((password == "") && (flag == "1"))
					{
						conn.QueryString = "select in_defaultpwd from rfinitial";
						conn.ExecuteQuery();
						password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(conn.GetFieldValue(0,0), "sha1");
					}
					else if ((password == "") && (flag == "0"))
						password = "";
					else
						password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");


					conn.QueryString = "select (select count(*) from pending_scuser where userid = '" + TXT_USERID.Text + "'), " +
						" (select count(*) from scalluser where userid = '" + TXT_USERID.Text + "') ";
					conn.ExecuteQuery();

					if (conn.GetFieldValue(0,0) != "0")
					{
						ClearEntries();
						SetDisable();
						LBL_RESULT.Text = "The UserID is currently awaiting for approval... Request Rejected!";
						LBL_RESULT.ForeColor = System.Drawing.Color.Red;
						//Response.Write("<script language='javascript'>alert('The UserID is currently awaiting for approval... Request Rejected!');</script>");
					}
					else if (CHK_ISNEW.Checked && conn.GetFieldValue(0,1) != "0")		//new userid, but userid already exist 
					{
						ClearEntries();
						SetDisable();
						LBL_RESULT.Text = "The UserID has already existed... Request Rejected!";
						LBL_RESULT.ForeColor = System.Drawing.Color.Red;
					}
					else
					{
						conn.QueryString = "exec su_scuser_new '" + 
							TXT_USERID.Text + "', '" + 
							DDL_GROUPID.SelectedValue + "', '" +
							TXT_SU_FULLNAME.Text + "', '" + 
							//							System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(TXT_SU_PWD.Text, "sha1")+ "', " +
							password + "', " +
							GlobalTools.ConvertNull(TXT_SU_HPNUM.Text) + ", " + 
							GlobalTools.ConvertNull(TXT_SU_EMAIL.Text) + ", " + 
							GlobalTools.ConvertNull(TXT_SU_BRANCH.Text)+ ", " + 
							GlobalTools.ConvertNull(TXT_SU_OWNUNIT.Text)+ ", " + 
							GlobalTools.ConvertNull(TXT_SU_SYSUNIT.Text)+ ", " + 
							GlobalTools.ConvertNull(TXT_SU_TEAMLEADER.Text) + ", " + 
							GlobalTools.ConvertNull(TXT_SU_UPLINER.Text) + ", " + 
							GlobalTools.ConvertNull(TXT_SU_MIDUPLINER.Text) + ", " + 
							GlobalTools.ConvertNull(TXT_SU_CORUPLINER.Text) + ", " + 
							GlobalTools.ConvertNull(TXT_SU_CRGUPLINER.Text) + ", " + 
							GlobalTools.ConvertNull(TXT_SU_MCRUPLINER.Text) + ", " + 
							GlobalTools.ConvertNull(TXT_SU_MITRARM.Text) + ", " + 
							GlobalTools.ConvertFloat(TXT_SU_APRVLIMIT.Text) + ", " +
							GlobalTools.ConvertFloat(TXT_SU_EMASLIMIT.Text) + ", " + 
							GlobalTools.ConvertNull(TXT_SU_UPLINER_CON.Text) + ", " + 
							GlobalTools.ConvertNull(TXT_SU_MITRARM_CON.Text) + ", " + 
							GlobalTools.ConvertNull(TXT_SU_UPLINER_CC.Text) + ", " + 
							GlobalTools.ConvertNull(TXT_SU_MITRARM_CC.Text) + ", '" + 
							flag + "', '" + TXT_SU_NIP.Text + "', '" + 
							TXT_OFFICER_CODE.Text + "', " + 
							GlobalTools.ConvertNull(DDL_JB_CODE.SelectedValue) + ", " +
							GlobalTools.ConvertNull(DDL_DEPT_CODE.SelectedValue) +", '" + 
							Session["UserID"].ToString() + "', '" + revoke + "', '" +
							suActive + "', '" +
							TXT_AGENTID.Text + "', '" + 
							TXT_SALES_ID.Text + "', '" + reset_logon + "'";
						conn.ExecuteNonQuery();

						CheckApproval();
						ClearEntries();
						SetDisable();

						LBL_RESULT.Text = "Request Submitted! Awaiting Approval ... ";
						LBL_RESULT.ForeColor = System.Drawing.Color.Green;
						//Response.Write("<script language='javascript'>alert('Request Submitted! Awaiting approval...');</script>");
					}
				}
				else
				{
					GlobalTools.popMessage(this, "Password Mismatch!");
					//Response.Write("<script language='javascript'>alert('Password Mismatch!');</script>");
				}
			}
			else
			{
				GlobalTools.popMessage(this, "Password must be at least " + conn.GetFieldValue("minpwdlength") + " characters!");
				//Response.Write("<script language='javascript'>alert('Password must be at least " + conn.GetFieldValue("minpwdlength") + " characters!');</script>");
			}
		}

		protected void DDL_GROUPID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TableSME.Visible = false;
			TableCC.Visible = false;
			TableConsumer.Visible = false;
			TableSales.Visible = false;

			conn.QueryString = "select moduleid, sg_apprsta from vw_grpaccessmodule where groupid = '" + DDL_GROUPID.SelectedValue + "'";
			conn.ExecuteQuery();
			for (int i = 0; i < conn.GetRowCount(); i++)
			{
				/// SME
				/// 
				if (conn.GetFieldValue(i, "moduleid") == "01") 
				{
					TableSME.Visible = true;

					TXT_SU_APRVLIMIT.CssClass = "";
					TXT_SU_EMASLIMIT.CssClass = "";

					/// If Group chosen is an Approval Group, then approval limit field and 
					/// emas limit must be filled
					if (conn.GetFieldValue(i, "sg_apprsta") == "1") 
					{
						TXT_SU_APRVLIMIT.CssClass = "mandatory";
						TXT_SU_EMASLIMIT.CssClass = "mandatory";
					}
				}


				if (conn.GetFieldValue(i, "moduleid") == "20")
					TableCC.Visible = true;
				if (conn.GetFieldValue(i, "moduleid") == "40")
					TableConsumer.Visible = true;
				if (conn.GetFieldValue(i, "moduleid") == "50")
					TableSales.Visible = true;
			}
			GlobalTools.SetFocus(this, BTN_CANCEL);
		}

		private void CheckApproval()
		{
			DataTable dtTemp = (DataTable) Session["dtRequest"];
			DataTable dtCCTemp = (DataTable) Session["dtCCRequest"];
			ArrayList listArea = (ArrayList) Session["CAareaList"];

			try
			{
				if (dtTemp.Rows.Count > 0)
				{
					for (int i = 0; i < dtTemp.Rows.Count; i++)
					{
						conn.QueryString = "exec su_sccreditlimit '" + 
							TXT_USERID.Text + "', '" + 
							dtTemp.Rows[i]["PRODUCTID"].ToString() + "', '" + 
							dtTemp.Rows[i]["CL_ISMITRA"].ToString() + "', " + 
							GlobalTools.ConvertFloat(dtTemp.Rows[i]["ACC_LIMIT"].ToString()) + ", " + 
							GlobalTools.ConvertFloat(dtTemp.Rows[i]["RJ_LIMIT"].ToString()) + ", " + 
							GlobalTools.ConvertFloat(dtTemp.Rows[i]["GRY_LIMIT"].ToString()) + ", '" + 
							dtTemp.Rows[i]["STATUS"].ToString() + "'";
						conn.ExecuteNonQuery();
					}
				}
				Session.Remove("dtRequest");
			}
			catch
			{
			}

			try
			{
				if (dtCCTemp.Rows.Count > 0)
				{
					for (int i = 0; i < dtCCTemp.Rows.Count; i++)
					{
						conn.QueryString = "exec su_sc_cc_creditlimit '" + 
							TXT_USERID.Text + "', '" + 
							dtCCTemp.Rows[i]["PRODUCTID"].ToString() + "', " + 
							GlobalTools.ConvertFloat(dtCCTemp.Rows[i]["ACC_LIMIT"].ToString()) + ", '" +
							dtCCTemp.Rows[i]["STATUS"].ToString() + "'";
						conn.ExecuteNonQuery();
					}
				}
				Session.Remove("dtCCRequest");
			}
			catch
			{
			}

			try
			{
				for (int i = 0; i < listArea.Count; i++)
				{
					conn.QueryString = "insert into pending_caregion (sc_id, region_id) " +
						"values ('" + TXT_USERID.Text.Trim() + "', '" + listArea[i].ToString() + "')";
					conn.ExecuteNonQuery();
				}
				Session.Remove("CAareaList");
			}
			catch
			{
			}
		}

		protected void DatGrd_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void UserMaintenance_PreRender(object sender, EventArgs e)
		{
			if (TableConsumer.Visible)
			{
				conn.QueryString = "select areaname from rfbranch b " +
					"left join rfarea a on a.areaid = b.areaid " +
					"where branch_code = '" + TXT_SU_BRANCH.Text + "' ";
				conn.ExecuteQuery();
				try
				{
					LBL_AREANAME.Text = conn.GetFieldValue(0,0);
				} 
				catch {}
			}
		}

		protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
		{
			clearSearch();
		}

		protected void cb_logon_CheckedChanged(object sender, System.EventArgs e)
		{
			if (CHK_ISNEW.Checked == false)
            {
                conn.QueryString = "UPDATE SCALLUSER SET SU_LOGON = 0 WHERE USERID = '" + TXT_USERID.Text + "' ";
			    conn.ExecuteNonQuery();
			    GlobalTools.popMessage(this, "Logon flag resetted!");
			    BTN_CANCEL_Click(sender, e);
			    BindData();
            }
		}

		protected void TXT_SU_CORUPLINER_TextChanged(object sender, System.EventArgs e)
		{
			conn.QueryString = "select su_fullname from scalluser where userid = '" + TXT_SU_CORUPLINER.Text + "'";
			conn.ExecuteQuery();
			try
			{
				TXT_CORUPLINER.Text = conn.GetFieldValue(0,0);
			}
			catch {}
		}

		protected void TXT_SU_CRGUPLINER_TextChanged(object sender, System.EventArgs e)
		{
			conn.QueryString = "select su_fullname from scalluser where userid = '" + TXT_SU_CRGUPLINER.Text + "'";
			conn.ExecuteQuery();
			try
			{
				TXT_CRGUPLINER.Text = conn.GetFieldValue(0,0);
			}
			catch {}
		}

		protected void TXT_SU_MCRUPLINER_TextChanged(object sender, System.EventArgs e)
		{
			conn.QueryString = "select su_fullname from scalluser where userid = '" + TXT_SU_MCRUPLINER.Text + "'";
			conn.ExecuteQuery();
			try
			{
				TXT_MCRUPLINER.Text = conn.GetFieldValue(0,0);
			}
			catch {}
		}

        protected void DatGrd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
        }

	}
}