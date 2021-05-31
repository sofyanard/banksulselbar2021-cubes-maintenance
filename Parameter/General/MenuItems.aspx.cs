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

namespace CuBES_Maintenance.Parameter.General
{
	/// <summary>
	/// Summary description for MenuItems.
	/// </summary>
	public partial class MenuItems : System.Web.UI.Page
	{
		protected Connection conn;
		string sqlQuery = "select * from vw_param_rfmenu ";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());

			if (!IsPostBack)
			{
				DDL_MODULEID.Items.Add(new ListItem("- SELECT -", ""));
				DDL_SUBMODULEID.Items.Add(new ListItem("- SELECT -", ""));
				DDL_SUBMODULEID_ADD.Items.Add(new ListItem("- SELECT -", ""));
				DDL_MENUPARENTCODE.Items.Add(new ListItem("- SELECT -", ""));
				DDL_TRACKCODE.Items.Add(new ListItem("- SELECT -", ""));

				conn.QueryString = "select moduleid, modulename from rfmodule";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++)
					DDL_MODULEID.Items.Add(new ListItem(conn.GetFieldValue(i, "modulename"), conn.GetFieldValue(i, "moduleid")));

				LBL_SqlQuery.Text = sqlQuery + "where submoduleid = '" + DDL_SUBMODULEID.SelectedValue + "'";
				BindData();
			}
		}

		private void BindData()
		{
			conn.QueryString = LBL_SqlQuery.Text;
			conn.ExecuteQuery();
			DataTable dt = conn.GetDataTable().Copy();
			DatGrd.DataSource = GenerateDataTable(dt);
			DatGrd.DataBind();
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

		protected void DDL_MODULEID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DDL_SUBMODULEID.Items.Clear();
			DDL_SUBMODULEID_ADD.Items.Clear();
			DDL_TRACKCODE.Items.Clear();

			DDL_SUBMODULEID.Items.Add(new ListItem("- SELECT -", ""));
			DDL_SUBMODULEID_ADD.Items.Add(new ListItem("- SELECT -", ""));
			DDL_TRACKCODE.Items.Add(new ListItem("- SELECT -", ""));

			conn.QueryString = "select submoduleid, submoduledisplay from rfsubmodule where moduleid = '" + DDL_MODULEID.SelectedValue + "'";
			conn.ExecuteQuery();
			for (int i = 0; i < conn.GetRowCount(); i++)
			{
				DDL_SUBMODULEID.Items.Add(new ListItem(conn.GetFieldValue(i, "submoduledisplay"), conn.GetFieldValue(i, "submoduleid")));
				DDL_SUBMODULEID_ADD.Items.Add(new ListItem(conn.GetFieldValue(i, "submoduledisplay"), conn.GetFieldValue(i, "submoduleid")));
			}

			conn.QueryString = "select trackcode, trackname from rfalltrack where moduleid = '" + DDL_MODULEID.SelectedValue + "'";
			conn.ExecuteQuery();
			for (int i = 0; i < conn.GetRowCount(); i++)
				DDL_TRACKCODE.Items.Add(new ListItem(conn.GetFieldValue(i, "trackname"), conn.GetFieldValue(i, "trackcode")));

			LBL_SqlQuery.Text = sqlQuery + "where moduleid = '" + DDL_MODULEID.SelectedValue + "' order by submoduledisplay";
			DatGrd.CurrentPageIndex = 0;
			BindData();
		}

		protected void DDL_SUBMODULEID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (DDL_SUBMODULEID.SelectedValue != "")
				LBL_SqlQuery.Text = sqlQuery + "where submoduleid = '" + DDL_SUBMODULEID.SelectedValue + "' and moduleid = '" + DDL_MODULEID.SelectedValue + "' order by submoduledisplay";
			else
				LBL_SqlQuery.Text = sqlQuery;
			
			DatGrd.CurrentPageIndex = 0;
			BindData();
		}

		private DataTable GenerateDataTable(DataTable dt)
		{
			DataTable dtToBeUsed = new DataTable();
			dtToBeUsed.Columns.Add(new DataColumn("MENUCODE"));
			dtToBeUsed.Columns.Add(new DataColumn("SUBMODULEDISPLAY"));
			dtToBeUsed.Columns.Add(new DataColumn("MENUPARENTDISPLAY"));
			dtToBeUsed.Columns.Add(new DataColumn("MENUDISPLAY"));
			dtToBeUsed.Columns.Add(new DataColumn("MENUHEADER4"));
			dtToBeUsed.Columns.Add(new DataColumn("TRACKCODE"));
			dtToBeUsed.Columns.Add(new DataColumn("TM_PARSINGPARAM"));
			dtToBeUsed.Columns.Add(new DataColumn("SUBMODULEID"));

			DataRow dr; 
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				dr =  dtToBeUsed.NewRow();
				dr[0] = dt.Rows[i]["MENUCODE"].ToString();
				dr[1] = dt.Rows[i]["SUBMODULEDISPLAY"].ToString();
				if (dt.Rows[i]["MENULEVEL"].ToString() == "0")
					dr[2] = dt.Rows[i]["MENUDISPLAY"].ToString();
				else if (dt.Rows[i]["MENULEVEL"].ToString() == "1")
				{
					dr[2] = dt.Rows[i]["MENUPARENTDISPLAY"].ToString();
					dr[3] = dt.Rows[i]["MENUDISPLAY"].ToString();
				}
				else if (dt.Rows[i]["MENULEVEL"].ToString() == "2")
				{
					for (int j = 0; j < dt.Rows.Count; j++)
					{
						if (dt.Rows[j]["MENUCODE"].ToString().Trim() == dt.Rows[i]["MENUPARENTID"].ToString().Trim())
						{
							dr[2] = dt.Rows[j]["MENUPARENTDISPLAY"].ToString();
							break;
						}
					}
					dr[3] = dt.Rows[i]["MENUPARENTDISPLAY"].ToString();
					dr[4] = dt.Rows[i]["MENUDISPLAY"].ToString();
				}
				dr[5] = dt.Rows[i]["TRACKCODE"].ToString();
				dr[6] = dt.Rows[i]["TM_PARSINGPARAM"].ToString();
				dr[7] = dt.Rows[i]["SUBMODULEID"].ToString();
				dtToBeUsed.Rows.Add(dr);
			}

			return dtToBeUsed;
		}

		private void DatGrd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DatGrd.CurrentPageIndex = e.NewPageIndex;
			BindData();
		}

		private void DatGrd_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton) e.CommandSource).CommandName)
			{
				case "edit":
					conn.QueryString = "select menucode, menudisplay from rfmenu where submoduleid = '" + e.Item.Cells[8].Text + "'";
					conn.ExecuteQuery();

					DDL_MENUPARENTCODE.Items.Clear();
					DDL_MENUPARENTCODE.Items.Add(new ListItem("- SELECT -", ""));
					for (int i = 0; i < conn.GetRowCount(); i++)
						DDL_MENUPARENTCODE.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));

					conn.QueryString = "select menuparentid, menucode, tm_linkname, trackcode, menudisplay from vw_param_rfmenu where menucode = '" + e.Item.Cells[0].Text + "'";
					conn.ExecuteQuery();
					if (conn.GetRowCount() > 0)
					{
						DDL_SUBMODULEID_ADD.SelectedValue	= e.Item.Cells[8].Text;//conn.GetFieldValue(0, "submoduleid");
						DDL_MENUPARENTCODE.SelectedValue	= conn.GetFieldValue(0, "menuparentid");
						TXT_MENUCODE.Text					= conn.GetFieldValue(0, "menucode");
						TXT_MENUDISPLAY.Text				= conn.GetFieldValue(0, "menudisplay");
						DDL_TRACKCODE.SelectedValue			= conn.GetFieldValue(0, "trackcode");
						TXT_TM_LINKNAME.Text				= conn.GetFieldValue(0, "tm_linkname");
						TXT_TM_PARSINGPARAM.Text			= e.Item.Cells[6].Text;
					}
					EnableInput();
					BTN_NEW.Visible = false;
					Button2.Visible = true;
					Button3.Visible = true;
					break;
			}
		}

		protected void BTN_NEW_Click(object sender, System.EventArgs e)
		{
			EnableInput();
			
			BTN_NEW.Visible = false;
			Button2.Visible = true;
			Button3.Visible = true;
		}

		private void EnableInput()
		{
			DDL_SUBMODULEID_ADD.Enabled	= true;
			DDL_MENUPARENTCODE.Enabled	= true;
			TXT_MENUDISPLAY.Enabled		= true;
			DDL_TRACKCODE.Enabled		= true;
			TXT_TM_LINKNAME.Enabled		= true;
			TXT_TM_PARSINGPARAM.Enabled	= true;
			TXT_MENUCODE.Enabled		= true;
		}

		private void DisableInput()
		{
			DDL_SUBMODULEID_ADD.Enabled		= false;
			DDL_MENUPARENTCODE.Enabled		= false;
			TXT_MENUDISPLAY.Enabled			= false;
			DDL_TRACKCODE.Enabled			= false;
			TXT_TM_LINKNAME.Enabled			= false;
			TXT_TM_PARSINGPARAM.Enabled		= false;
			TXT_MENUCODE.Enabled			= false;
		}

		private void ClearInputs()
		{
			DDL_SUBMODULEID_ADD.SelectedValue	= "";
			DDL_MENUPARENTCODE.SelectedValue	= "";
			TXT_MENUDISPLAY.Text				= "";
			DDL_TRACKCODE.SelectedValue			= "";
			TXT_TM_LINKNAME.Text				= "";
			TXT_TM_PARSINGPARAM.Text			= "";
			TXT_MENUCODE.Text					= "";
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			ClearInputs();
			DisableInput();

			BTN_NEW.Visible = true;
			Button2.Visible = false;
			Button3.Visible = false;
		}

		protected void Button3_Click(object sender, System.EventArgs e)
		{
			ClearInputs();
			DisableInput();

			BTN_NEW.Visible = true;
			Button2.Visible = false;
			Button3.Visible = false;
		}

		protected void DDL_SUBMODULEID_ADD_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			conn.QueryString = "select menucode, menudisplay from rfmenu where submoduleid = '" + DDL_SUBMODULEID_ADD.SelectedValue + "'";
			conn.ExecuteQuery();
			DDL_MENUPARENTCODE.Items.Clear();
			DDL_MENUPARENTCODE.Items.Add(new ListItem("- SELECT -", ""));
			for (int i = 0; i < conn.GetRowCount(); i++)
				DDL_MENUPARENTCODE.Items.Add(new ListItem(conn.GetFieldValue(i, "menudisplay"), conn.GetFieldValue(i, "menucode")));

		}
	}
}
