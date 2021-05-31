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
using System.Configuration;

namespace CuBES_Maintenance.User
{
	/// <summary>
	/// Summary description for GroupMaintenance.
	/// </summary>
	public partial class GroupMaintenance : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection connsme;
		DataTable dt = new DataTable();
		protected Tools tool = new Tools();
		protected string SqlStatement = "select distinct groupid, sg_grpname from vw_grpaccessmodule ";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());
			SetDBConnSME();

			if (!IsPostBack)
			{
				HyperLink1.NavigateUrl = "UserMaintenance.aspx?mc=" + Request.QueryString["mc"];
				HyperLink2.NavigateUrl = "GroupMaintenance.aspx?mc=" + Request.QueryString["mc"];

				DDL_MODULEID.Items.Add(new ListItem("- PILIH -", ""));
				DDL_MODULEID.Items.Add(new ListItem("- Unassigned -", "U"));
				DDL_SG_GRPLEVEL.Items.Add(new ListItem("- PILIH -", ""));

				DDL_SG_APRVTRACK_CC.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_APRVTRACK_CON.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_APRVTRACK_SME.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_LMSAPRVTRACK_SME.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_WTCAPRVTRACK_SME.Items.Add(new ListItem("- PILIH -", ""));

				DDL_SG_MITRARM_CC.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_MITRARM_CON.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_MITRARM_SME.Items.Add(new ListItem("- PILIH -", ""));

				DDL_SG_GRPUPLINER_SME.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_MDLUPLINER.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_CORUPLINER.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_CRGUPLINER.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_MCRUPLINER.Items.Add(new ListItem("- PILIH -", ""));

				DDL_SG_GRPUPLINER_CON.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_GRPUPLINER_CC.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_BUSSUNITID.Items.Add(new ListItem("- PILIH -", ""));
				DDL_SG_GRPSUPER.Items.Add(new ListItem("- PILIH -", ""));

				conn.QueryString = "select moduleid, modulename from rfmodule";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
				{
					DDL_MODULEID.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
					CHK_MODULEID.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
				}

				conn.QueryString = "select groupid, sg_grpname from vw_scallgroup where moduleid='01' order by sg_grpname";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
				{
					DDL_SG_GRPUPLINER_SME.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
					DDL_SG_MDLUPLINER.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
					DDL_SG_CORUPLINER.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
					DDL_SG_CRGUPLINER.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
					DDL_SG_MCRUPLINER.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
					DDL_SG_MITRARM_SME.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
					DDL_SG_GRPSUPER.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
				}

				conn.QueryString = "select groupid, sg_grpname, modulename from vw_scallgroup where moduleid='40' order by sg_grpname";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
				{
					DDL_SG_GRPUPLINER_CON.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
					DDL_SG_MITRARM_CON.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
				}

				conn.QueryString = "select groupid, sg_grpname, modulename from vw_scallgroup where moduleid='20' order by sg_grpname";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
				{
					DDL_SG_GRPUPLINER_CC.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
					DDL_SG_MITRARM_CC.Items.Add(new ListItem(conn.GetFieldValue(i, "sg_grpname"), conn.GetFieldValue(i, "groupid")));
				}

				conn.QueryString = "select trackcode, trackname, moduleid from rfalltrack where active = '1'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
				{
					if (conn.GetFieldValue(i, "moduleid") == "01")
						DDL_SG_APRVTRACK_SME.Items.Add(new ListItem(conn.GetFieldValue(i, "trackcode") + " - " + conn.GetFieldValue(i, "trackname"), conn.GetFieldValue(i, "trackcode")));
					else if (conn.GetFieldValue(i, "moduleid") == "40")
						DDL_SG_APRVTRACK_CON.Items.Add(new ListItem(conn.GetFieldValue(i, "trackcode") + " - " + conn.GetFieldValue(i, "trackname"), conn.GetFieldValue(i, "trackcode")));
					else if (conn.GetFieldValue(i, "moduleid") == "20")
						DDL_SG_APRVTRACK_CC.Items.Add(new ListItem(conn.GetFieldValue(i, "trackcode") + " - " + conn.GetFieldValue(i, "trackname"), conn.GetFieldValue(i, "trackcode")));
				}

				//20080313 add by sofyan, utk LMS
				connsme.QueryString = "select trackcode, trackname from lms_rftrack where active = '1'";
				connsme.ExecuteQuery();
				for (int i = 0; i < connsme.GetRowCount(); i++)
				{
					DDL_SG_LMSAPRVTRACK_SME.Items.Add(new ListItem(connsme.GetFieldValue(i, "trackcode") + " - " + connsme.GetFieldValue(i, "trackname"), connsme.GetFieldValue(i, "trackcode")));
				}
				connsme.QueryString = "select trackcode, trackname from lms_rftrack where active = '1'";
				connsme.ExecuteQuery();
				for (int i = 0; i < connsme.GetRowCount(); i++)
				{
					DDL_SG_WTCAPRVTRACK_SME.Items.Add(new ListItem(connsme.GetFieldValue(i, "trackcode") + " - " + connsme.GetFieldValue(i, "trackname"), connsme.GetFieldValue(i, "trackcode")));
				}

				conn.QueryString = "select grplevelid, grpleveldesc from rfgrouplevel where active = '1'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_SG_GRPLEVEL.Items.Add(new ListItem(conn.GetFieldValue(i, "grpleveldesc"), conn.GetFieldValue(i, "grplevelid")));

				conn.QueryString = "select bussunitid, bussunitdesc from rfbusinessunit where active='1'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_SG_BUSSUNITID.Items.Add(new ListItem(conn.GetFieldValue(i, "bussunitdesc"), conn.GetFieldValue(i, "bussunitid")));

				LBL_SqlStatement.Text = SqlStatement + "WHERE moduleid = ''";
				FillDataSet();

				SMEUpliner.Visible = false;
				CCUpliner.Visible = false;
				ConsumerUpliner.Visible = false;

				DisableFields();
			}


			SetVisible();
			LBL_RESULT.Text = "";
			BTN_SAVE.Attributes.Add("onclick", "if(!cek_mandatory(document.getElementById('Form1'))){return false;};");
		}

		//20080313 add by sofyan, utk keperluan LMS
		//di-hardcode utk SME aja ya...
		private void SetDBConnSME()
		{
			conn.QueryString = "SELECT * FROM RFMODULE WHERE MODULEID = '01'";
			conn.ExecuteQuery();
			connsme = new Connection ("Data Source=" + conn.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn.GetFieldValue("DB_NAMA") + ";uid=" +conn.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
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

		}
		#endregion

		private void DisableFields()
		{
			TXT_GROUPID.Enabled = false;
			TXT_SG_GRPNAME.Enabled = false;
			DDL_SG_GRPLEVEL.Enabled = false;
			CHK_MODULEID.Enabled = false;
			CHK_SG_APPRSTA.Enabled = false;

			SMEApproval.Visible = false;
			SMEUpliner.Visible = false;
			ConsumerApproval.Visible = false;
			ConsumerUpliner.Visible = false;
			CCApproval.Visible = false;
			CCUpliner.Visible = false;
		}

		private void EnableFields()
		{
			TXT_GROUPID.Enabled = true;
			TXT_SG_GRPNAME.Enabled = true;
			DDL_SG_GRPLEVEL.Enabled = true;
			CHK_MODULEID.Enabled = true;
			CHK_SG_APPRSTA.Enabled = true;
		}

		private void ClearEntries()
		{
			TXT_GROUPID.Text = "";
			TXT_SG_GRPNAME.Text = "";
			DDL_SG_GRPLEVEL.SelectedValue = "";

			for (int i = 0; i < CHK_MODULEID.Items.Count; i++)
				CHK_MODULEID.Items[i].Selected = false;

			DDL_SG_GRPUPLINER_SME.SelectedValue = "";
			DDL_SG_MDLUPLINER.SelectedValue = "";
			DDL_SG_CORUPLINER.SelectedValue = "";
			DDL_SG_CRGUPLINER.SelectedValue = "";
			DDL_SG_MCRUPLINER.SelectedValue = "";
			
			DDL_SG_GRPUPLINER_CON.SelectedValue = "";
			DDL_SG_GRPUPLINER_CC.SelectedValue = "";

			CHK_SG_APPRSTA.Checked = false;

			DDL_SG_MITRARM_SME.SelectedValue = "";
			DDL_SG_MITRARM_CON.SelectedValue = "";
			DDL_SG_MITRARM_CC.SelectedValue = "";

			DDL_SG_APRVTRACK_SME.SelectedValue = "";
			DDL_SG_LMSAPRVTRACK_SME.SelectedValue = "";
			DDL_SG_WTCAPRVTRACK_SME.SelectedValue = "";
			DDL_SG_APRVTRACK_CON.SelectedValue = "";
			DDL_SG_APRVTRACK_CC.SelectedValue = "";

			DDL_SG_GRPUNIT.SelectedValue = "";
			DDL_SG_BUSSUNITID.SelectedValue = "";
			DDL_SG_GRPSUPER.SelectedValue = "";
		}

		private void FillDataSet()
		{
			//			conn.QueryString = "select groupid, sg_grpname from vw_grpaccessmodule where moduleid = '" + DDL_MODULEID.SelectedValue + "'";
			conn.QueryString = LBL_SqlStatement.Text;
			conn.ExecuteQuery();
			dt = conn.GetDataTable().Copy();
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
		}

		protected void DDL_MODULEID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TXT_FINDGROUP.Text = "";
			TXT_FINDUPLINER.Text = "";
			RDL_UPLINER.SelectedIndex = -1;
			DatGrd.CurrentPageIndex = 0;
			FillGrid();
			FillDataSet();
		}

		private void DatGrd_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataTable dt = new DataTable();
			switch (((LinkButton) e.CommandSource).CommandName)
			{
				case "menuAccess":
					Response.Write("<script language='javascript'>window.open('GroupMenuAccess.aspx?GroupID=" + e.Item.Cells[0].Text + "','MenuAccess','status=no,scrollbars=yes,width=500,height=400');</script>");
					break;
				case "edit":
					EnableFields();
					BTN_NEW.Visible = false;
					BTN_CANCEL.Visible = true;
					BTN_SAVE.Visible = true;

					CHK_ISNEW.Checked = false;

					TXT_GROUPID.Text	= e.Item.Cells[0].Text;
					TXT_SG_GRPNAME.Text = e.Item.Cells[1].Text;
					conn.QueryString = "select moduleid from grpaccessmodule where groupid = '" + TXT_GROUPID.Text + "'";
					conn.ExecuteQuery();
					dt = conn.GetDataTable();
					for (int i = 0; i < conn.GetRowCount(); i++)
						try 
						{
							CHK_MODULEID.Items.FindByValue(conn.GetFieldValue(i, "moduleid")).Selected = true;
						}	
						catch { }

					conn.QueryString = "select sg_apprsta , sg_grplevel from scallgroup where groupid = '" + e.Item.Cells[0].Text + "'";
					conn.ExecuteQuery();
					if (conn.GetFieldValue("SG_APPRSTA") == "1")
						CHK_SG_APPRSTA.Checked = true;
					else
						CHK_SG_APPRSTA.Checked = false;

					try
					{
						DDL_SG_GRPLEVEL.SelectedValue = conn.GetFieldValue("SG_GRPLEVEL");
					}
					catch {}
					
					SetVisible();

					if (CHK_MODULEID_Selected("01") == true)
					{
						//conn.QueryString = "exec SCGROUP_EDIT '" + TXT_GROUPID.Text + "', 'VW_SCGROUP'";
						conn.QueryString = "select sg_grpupliner, sg_mdlupliner, sg_corupliner, sg_crgupliner, sg_mcrupliner, sg_aprvtrack, sg_aprvtrack_lms, sg_aprvtrack_wtc, " +
							"sg_mitrarm, sg_grpunit, sg_bussunitid, sg_grpsuper from vw_scallgroup " +
							"where groupid = '" + TXT_GROUPID.Text + "' and moduleid = '01' ";
						conn.ExecuteQuery();
						
						try { DDL_SG_GRPUPLINER_SME.SelectedValue = conn.GetFieldValue("SG_GRPUPLINER"); } 
						catch {}
						try { DDL_SG_MDLUPLINER.SelectedValue = conn.GetFieldValue("SG_MDLUPLINER"); } 
						catch {}
						try { DDL_SG_CORUPLINER.SelectedValue = conn.GetFieldValue("SG_CORUPLINER"); } 
						catch {}
						try { DDL_SG_CRGUPLINER.SelectedValue = conn.GetFieldValue("SG_CRGUPLINER"); } 
						catch {}
						try { DDL_SG_MCRUPLINER.SelectedValue = conn.GetFieldValue("SG_MCRUPLINER"); } 
						catch {}
						try { DDL_SG_GRPUNIT.SelectedValue = conn.GetFieldValue("SG_GRPUNIT"); } 
						catch {}
						try { DDL_SG_BUSSUNITID.SelectedValue = conn.GetFieldValue("SG_BUSSUNITID"); } 
						catch {}
						try { DDL_SG_GRPSUPER.SelectedValue = conn.GetFieldValue("SG_GRPSUPER"); } 
						catch {}
						DDL_SG_GRPUNIT.CssClass = "mandatory";

						if (CHK_SG_APPRSTA.Checked == true)
						{
							try { DDL_SG_APRVTRACK_SME.SelectedValue = conn.GetFieldValue("SG_APRVTRACK"); } 
							catch {}
							try { DDL_SG_LMSAPRVTRACK_SME.SelectedValue = conn.GetFieldValue("SG_APRVTRACK_LMS"); } 
							catch {}
							try { DDL_SG_WTCAPRVTRACK_SME.SelectedValue = conn.GetFieldValue("SG_APRVTRACK_WTC"); } 
							catch {}
							try { DDL_SG_MITRARM_SME.SelectedValue = conn.GetFieldValue("SG_MITRARM"); } 
							catch {}
							/*try { DDL_SG_GRPUNIT.SelectedValue = conn.GetFieldValue("SG_GRPUNIT"); } 
							catch {}
							try { DDL_SG_BUSSUNITID.SelectedValue = conn.GetFieldValue("SG_BUSSUNITID"); } 
							catch {}
							try { DDL_SG_GRPSUPER.SelectedValue = conn.GetFieldValue("SG_GRPSUPER"); } 
							catch {}*/
							DDL_SG_APRVTRACK_SME.CssClass = "mandatory";
						}
					}
					if (CHK_MODULEID_Selected("20") == true)
					{
						//conn.QueryString = "exec SCGROUP_EDIT '" + TXT_GROUPID.Text + "', 'VW_SCGROUP'";
						conn.QueryString = "select sg_grpupliner_cc, sg_aprvtrack_cc, sg_mitrarm_cc " +
							"from vw_scallgroup where groupid = '" + TXT_GROUPID.Text + "' and moduleid = '20' ";
						conn.ExecuteQuery();
						DDL_SG_GRPUPLINER_CC.SelectedValue = conn.GetFieldValue("SG_GRPUPLINER_CC");

						if (CHK_SG_APPRSTA.Checked == true)
						{
							DDL_SG_APRVTRACK_CC.SelectedValue = conn.GetFieldValue("SG_APRVTRACK_CC");
							DDL_SG_MITRARM_CC.SelectedValue = conn.GetFieldValue("SG_MITRARM_CC");
						}
					}
					if (CHK_MODULEID_Selected("40") == true)
					{
						//conn.QueryString = "exec SCGROUP_EDIT '" + TXT_GROUPID.Text + "', 'VW_SCGROUP'";
						conn.QueryString = "select sg_grpupliner_con, sg_aprvtrack_con, sg_mitrarm_con " +
							"from vw_scallgroup where groupid = '" + TXT_GROUPID.Text + "' and moduleid = '40' ";
						conn.ExecuteQuery();
						DDL_SG_GRPUPLINER_CON.SelectedValue = conn.GetFieldValue("SG_GRPUPLINER_CON");

						if (CHK_SG_APPRSTA.Checked == true)
						{
							DDL_SG_APRVTRACK_CON.SelectedValue = conn.GetFieldValue("SG_APRVTRACK_CON");
							DDL_SG_MITRARM_CON.SelectedValue = conn.GetFieldValue("SG_MITRARM_CON");
						}
					}
					GlobalTools.SetFocus(this, BTN_CANCEL);
					break;
				case "delete":
					conn.QueryString = "exec SU_SCGROUP_DELETE '" + e.Item.Cells[0].Text + "', '" + 
						e.Item.Cells[1].Text + "', '" + "2" + "', '" + "1" + "'";
					conn.ExecuteNonQuery();
					
					LBL_RESULT.Text = "Request Submitted! Awaiting Approval ... ";
					LBL_RESULT.ForeColor = System.Drawing.Color.Green;
					break;
			}
		}

		private bool CHK_MODULEID_Selected(string moduleid)
		{
			try
			{
				return CHK_MODULEID.Items.FindByValue(moduleid).Selected;
			}	
			catch { }
			return false;
		}

		private void SetVisible()
		{
			if (CHK_MODULEID_Selected("01") == true)
				SMEUpliner.Visible = true;
			else	SMEUpliner.Visible = false;
			if (CHK_MODULEID_Selected("20") == true)
				CCUpliner.Visible = true;
			else	CCUpliner.Visible = false;
			if (CHK_MODULEID_Selected("40") == true)
				ConsumerUpliner.Visible = true;
			else	ConsumerUpliner.Visible = false;
			
			if (CHK_SG_APPRSTA.Checked == true)
			{
				if (CHK_MODULEID_Selected("01") == true)
					SMEApproval.Visible = true;
				else	SMEApproval.Visible = false;
				if (CHK_MODULEID_Selected("20") == true)
					CCApproval.Visible = true;
				else	CCApproval.Visible = false;
				if (CHK_MODULEID_Selected("40") == true)
					ConsumerApproval.Visible = true;
				else	ConsumerApproval.Visible = false;
			}
			else
			{
				SMEApproval.Visible = false;
				CCApproval.Visible = false;
				ConsumerApproval.Visible = false;
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string approval = "0", flag = "0", moduleID = "";
		
			if (CHK_SG_APPRSTA.Checked == true)
				approval = "1";
			if (CHK_ISNEW.Checked == true)
				flag = "1";
			
			for (int i = 0; i < CHK_MODULEID.Items.Count; i++)
			{
				if (CHK_MODULEID.Items[i].Selected == true)
				{
					if (moduleID.Length != 0)
						moduleID = moduleID + CHK_MODULEID.Items[i].Value + "|";
					else
						moduleID = CHK_MODULEID.Items[i].Value + "|";
				}
			}

			conn.QueryString = "select count (*) from pending_scgroup where groupid='" + TXT_GROUPID.Text + "'";
			conn.ExecuteQuery();

			if (conn.GetFieldValue(0,0) == "0")
			{
				conn.QueryString = "exec SU_SCGROUP '" + TXT_GROUPID.Text.Trim() + "', '" + 
					TXT_SG_GRPNAME.Text.Trim() + "', " + GlobalTools.ConvertNull(DDL_SG_GRPUPLINER_SME.SelectedValue) + ", " + 
					GlobalTools.ConvertNull(DDL_SG_MDLUPLINER.SelectedValue) + ", " + 
					GlobalTools.ConvertNull(DDL_SG_CORUPLINER.SelectedValue) + ", " + 
					GlobalTools.ConvertNull(DDL_SG_CRGUPLINER.SelectedValue) + ", " + 
					GlobalTools.ConvertNull(DDL_SG_MCRUPLINER.SelectedValue) + ", '" + 
					approval + "', " + GlobalTools.ConvertNull(DDL_SG_APRVTRACK_SME.SelectedValue) + ", " +
					GlobalTools.ConvertNull(DDL_SG_LMSAPRVTRACK_SME.SelectedValue) + ", " +
					GlobalTools.ConvertNull(DDL_SG_WTCAPRVTRACK_SME.SelectedValue) + ", " +
					GlobalTools.ConvertNull(DDL_SG_APRVTRACK_CON.SelectedValue) + ", " + 
					GlobalTools.ConvertNull(DDL_SG_APRVTRACK_CC.SelectedValue) + ", " + 
					GlobalTools.ConvertNull(DDL_SG_MITRARM_SME.SelectedValue) + ", " + 
					GlobalTools.ConvertNull(DDL_SG_GRPLEVEL.SelectedValue) + ", '" + 
					flag + "', '1', " + GlobalTools.ConvertNull(DDL_SG_GRPUPLINER_CON.SelectedValue) + ", " + 
					GlobalTools.ConvertNull(DDL_SG_GRPUPLINER_CC.SelectedValue) + ", " + 
					GlobalTools.ConvertNull(DDL_SG_MITRARM_CON.SelectedValue) + ", " + 
					GlobalTools.ConvertNull(DDL_SG_MITRARM_CC.SelectedValue) + ", " +
					GlobalTools.ConvertNull(DDL_SG_GRPUNIT.SelectedValue) + ", " + 
					GlobalTools.ConvertNull(DDL_SG_GRPSUPER.SelectedValue) + ", " + 
					GlobalTools.ConvertNull(DDL_SG_BUSSUNITID.SelectedValue) + ", '" + 
					moduleID + "'";
				try
				{
					conn.ExecuteQuery();
				} 
				catch (Exception ex)
				{
					string errmsg = ex.Message.Replace("'","");
					if (errmsg.IndexOf("Last Query:") > 0)		//method Connection.ExecuteNonQuery() add this msg on exception 
						errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
					GlobalTools.popMessage(this, errmsg);
					return;
				}

				ClearEntries();
				DisableFields();
				BTN_SAVE.Visible = false;
				BTN_CANCEL.Visible = false;
				BTN_NEW.Visible = true;
				
				LBL_RESULT.Text = "Request Submitted! Awaiting Approval ... ";
				LBL_RESULT.ForeColor = System.Drawing.Color.Green;
			}
			else
			{
				ClearEntries();
				DisableFields();
				BTN_SAVE.Visible = false;
				BTN_CANCEL.Visible = false;
				BTN_NEW.Visible = true;
				
				LBL_RESULT.Text = "The GroupID is currently awaiting for approval... Request Rejected!";
				LBL_RESULT.ForeColor = System.Drawing.Color.Red;
			}
		}

		protected void CHK_MODULEID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			////////////////////////////////////////////////
			/// SME MODULE
			/// 
			if (CHK_MODULEID_Selected("01") == true)
			{
				SMEUpliner.Visible = true;
				DDL_SG_GRPUNIT.CssClass = "mandatory";
			}
			else 
			{
				SMEUpliner.Visible = false;
				DDL_SG_GRPUNIT.CssClass = "";
			}


			////////////////////////////////////////////////
			/// CREDIT CARD MODULE
			/// 
			if (CHK_MODULEID_Selected("20") == true)
				CCUpliner.Visible = true;
			else	
				CCUpliner.Visible = false;


			////////////////////////////////////////////////
			/// CONSUMER MODULE
			/// 
			if (CHK_MODULEID_Selected("40") == true)
				ConsumerUpliner.Visible = true;
			else	
				ConsumerUpliner.Visible = false;


			GlobalTools.SetFocus(this, BTN_CANCEL);
		}

		protected void BTN_NEW_Click(object sender, System.EventArgs e)
		{
			BTN_SAVE.Visible = true;
			BTN_CANCEL.Visible = true;
			BTN_NEW.Visible = false;
			EnableFields();

			CHK_ISNEW.Checked = true;
			GlobalTools.SetFocus(this, BTN_CANCEL);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BTN_SAVE.Visible = false;
			BTN_CANCEL.Visible = false;
			BTN_NEW.Visible = true;
			DisableFields();
			ClearEntries();
		}

		protected void CHK_SG_APPRSTA_CheckedChanged(object sender, System.EventArgs e)
		{
			if (CHK_SG_APPRSTA.Checked == true)
			{
				///////////////////////////////////////////////////////
				///	SME
				///	
				if (CHK_MODULEID_Selected("01") == true) 
				{
					/// If Group is an Approval Group, then Track for approval must be filled
					/// 
					SMEApproval.Visible = true;
					DDL_SG_APRVTRACK_SME.CssClass = "mandatory";
				}
				else	
					SMEApproval.Visible = false;


				///////////////////////////////////////////////////////
				///	CREDIT CARD
				///	
				if (CHK_MODULEID_Selected("20") == true)
					CCApproval.Visible = true;
				else	
					CCApproval.Visible = false;


				///////////////////////////////////////////////////////
				///	CONSUMER
				///	
				if (CHK_MODULEID_Selected("40") == true)
					ConsumerApproval.Visible = true;
				else	
					ConsumerApproval.Visible = false;
			}
			else
			{
				SMEApproval.Visible = false;
				DDL_SG_APRVTRACK_SME.CssClass = "";


				CCApproval.Visible = false;
				ConsumerApproval.Visible = false;
			}
		}

		private void DatGrd_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			string tempSql = "";
			/*
			LBL_SqlStatement.Text = SqlStatement + "where moduleid = '" + DDL_MODULEID.SelectedValue + "'" +
				" ORDER BY " + e.SortExpression;
			*/
			if (LBL_SqlStatement.Text.IndexOf("WHERE") >= 0)
			{
				if (LBL_SqlStatement.Text.IndexOf("ORDER") >= 0)
				{
					tempSql = LBL_SqlStatement.Text.Substring(0, LBL_SqlStatement.Text.IndexOf("ORDER"));
					LBL_SqlStatement.Text = tempSql + " ORDER BY " + e.SortExpression;
				}
				else
					LBL_SqlStatement.Text += " ORDER BY " + e.SortExpression;
			}
			else
				LBL_SqlStatement.Text = SqlStatement + "WHERE moduleid = '" + DDL_MODULEID.SelectedValue + "'" +
					" ORDER BY " + e.SortExpression;
			FillDataSet();
		}

		private void DatGrd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DatGrd.CurrentPageIndex = e.NewPageIndex;
			FillDataSet();
		}

		protected void BTN_GENERATE_Click(object sender, System.EventArgs e)
		{
			string path = "", menuDir = "", db_ip = "", db_nama = "", connString = "", dbLoginID = "", dbLoginPwd = "";

			//			Response.Write("<!-- \n");
			//conn.QueryString = "select moduleid, app_root, menudir from rfmodule where moduleid in ('20', '40', '50')";
			conn.QueryString = "select moduleid, app_root, menudir from rfmodule where moduleid = '" + DDL_MODULEID.SelectedValue + "'";
			conn.ExecuteQuery();
			//			Response.Write(conn.QueryString+"\n");
			DataTable dtModuleDir = new DataTable();
			dtModuleDir = conn.GetDataTable().Copy();

			for (int i = 0; i < dtModuleDir.Rows.Count; i++)
			{
				// Get path to create XML file
				path = dtModuleDir.Rows[i]["app_root"].ToString() + dtModuleDir.Rows[i]["menudir"].ToString();
				menuDir = dtModuleDir.Rows[i]["menudir"].ToString();

				// Get connection string credentials
				conn.QueryString = "select db_ip, db_nama, db_loginid, db_loginpwd from rfmodule where moduleid = '" + dtModuleDir.Rows[i]["moduleid"].ToString() + "'";
				conn.ExecuteQuery();
				db_ip = conn.GetFieldValue(0, "db_ip"); 
				db_nama = conn.GetFieldValue(0, "db_nama");
				dbLoginID = conn.GetFieldValue(0, "db_loginid");
				dbLoginPwd = conn.GetFieldValue(0, "db_loginpwd");
				connString = "Data Source=" + db_ip + ";Initial Catalog=" + db_nama + ";uid=" + dbLoginID + ";pwd=" + dbLoginPwd + ";Pooling=true";

				conn.QueryString = "select groupid, ga_status from grpaccessmodule where moduleid = '" + dtModuleDir.Rows[i]["moduleid"].ToString() + "'";
				conn.ExecuteQuery();
				//				Response.Write(conn.QueryString+"\n");

				if (dtModuleDir.Rows[i]["moduleid"].ToString() == "01")
				{
					Connection conn2 = new Connection (connString);
					for (int j = 0; j < conn.GetRowCount(); j++)
					{
						//tool.GenerateMenu(conn.GetFieldValue(j, "groupid").ToString(), path, conn, "01");
						//tool.GenerateMenu(conn.GetFieldValue(j, "groupid"), path, menuDir, conn, "01");
						//						Response.Write(conn.GetFieldValue(j, "groupid")+" xx \n");
						tool.GenerateMenu(conn.GetFieldValue(j, "groupid"), path, menuDir, conn2, "01");
					}
				}
				else if (dtModuleDir.Rows[i]["moduleid"].ToString() == "20")
				{
					CreditCard.Maintenance genMenu = new CreditCard.Maintenance();
					for (int j = 0; j < conn.GetRowCount(); j++)
						genMenu.GenerateMenu(conn.GetFieldValue(j, "groupid"), path, menuDir, connString, "20");
				}
				else if (dtModuleDir.Rows[i]["moduleid"].ToString() == "40")
				{
					Consumer.Maintenance genMenu = new Consumer.Maintenance();
					for (int j = 0; j < conn.GetRowCount(); j++)
						genMenu.GenerateMenu(conn.GetFieldValue(j, "groupid"), path, menuDir, connString, "40");
				}
				else if (dtModuleDir.Rows[i]["moduleid"].ToString() == "50")
				{
					SalesCom.Maintenance genMenu = new SalesCom.Maintenance();
					for (int j = 0; j < conn.GetRowCount(); j++)
						genMenu.GenerateMenu(conn.GetFieldValue(j, "groupid"), path, menuDir, connString, "50");
				}
				else if (dtModuleDir.Rows[i]["moduleid"].ToString() == "99")
				{
					Connection conn2 = new Connection (connString);
					for (int j = 0; j < conn.GetRowCount(); j++)
						tool.GenerateMenu(conn.GetFieldValue(j, "groupid"), path, menuDir, conn2, "99");
				}
			}

			//			Response.Write("-->\n");
			//Response.Write("<script language='javascript'>alert('Group Menu Access Updated!');</script>");
			GlobalTools.popMessage(this, "Group Menu Access Updated!");
		}

		private void FillGrid()
		{
			string sql = "WHERE", sjoin = "AND";

			if (DDL_MODULEID.SelectedValue != "")
				if (DDL_MODULEID.SelectedValue != "U")
					sql = sql + " MODULEID = '" + DDL_MODULEID.SelectedValue + "' " + sjoin;
				else sql = sql + " groupid not in (select distinct groupid from grpaccessmodule) " + sjoin;

			if (TXT_FINDGROUP.Text.Trim() != "")
				sql = sql + " sg_grpname like '%" + TXT_FINDGROUP.Text.Trim() + "%' " + sjoin;
			if (TXT_FINDUPLINER.Text.Trim() != "")
			{
				string field = RDL_UPLINER.SelectedValue;
				if (field == "")
					sql = sql + " (SG_GRPUNAME like '%" + TXT_FINDUPLINER.Text.Trim() +
						"%' or SG_GRPUNAME_CON like '%" + TXT_FINDUPLINER.Text.Trim() +
						"%' or SG_GRPUNAME_CC like '%" + TXT_FINDUPLINER.Text.Trim() + "%') " + sjoin;
				else
					sql = sql + " " + field + " like '%" + TXT_FINDUPLINER.Text.Trim() + "%' " + sjoin;
			}

			if (sql == "WHERE")
				sql = "";
			else
				sql = sql.Substring(0, sql.Length - sjoin.Length); // + " AND SG_ACTIVE = '1'";

			//			if (DDL_MODULEID.SelectedValue == "U")
			//				LBL_SqlStatement.Text = "select distinct groupid, sg_grpname from scallgroup " + sql;
			//			else
			LBL_SqlStatement.Text = SqlStatement + sql;
		}

		protected void BTN_SEARCH_Click(object sender, System.EventArgs e)
		{
			DatGrd.CurrentPageIndex = 0;
			FillGrid();
			FillDataSet();
		}
	}
}