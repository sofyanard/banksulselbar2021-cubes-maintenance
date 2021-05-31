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
using System.Configuration;

namespace CuBES_Maintenance.User
{
	/// <summary>
	/// Summary description for GroupMenuAccess.
	/// </summary>
	public partial class GroupMenuAccess : System.Web.UI.Page
	{
		protected DataTable dtSubModule = new DataTable();
		protected Tools tool = new Tools();
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());
			DataTable dtMenuCode = new DataTable();

			int rowNumber = 0;
			
			conn.QueryString = "select moduleid, modulename from vw_grpaccessmodule where groupid = '" + Request.QueryString["GroupID"] + "'";
			conn.ExecuteQuery();
			for (int i = 0; i < conn.GetRowCount(); i++)
			{
				HyperLink t = new HyperLink();
				t.Text = conn.GetFieldValue(i, "modulename");
				t.Font.Bold = true;
				t.NavigateUrl = "GroupMenuAccess.aspx?GroupID=" + Request.QueryString["GroupID"] + "&ModuleID=" + conn.GetFieldValue(i, "moduleid") + "&ModuleName=" + conn.GetFieldValue(i, "modulename");
				PlaceHolder1.Controls.Add(t);
				PlaceHolder1.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
			}

			TBL_MENU.Rows.Add(new TableRow());
			TBL_MENU.Rows[rowNumber].Cells.Add(new TableCell());
			try
			{
				if (Request.QueryString["ModuleName"].Length != 0)
					TBL_MENU.Rows[rowNumber].Cells[0].Text = Request.QueryString["ModuleName"];
			}
			catch
			{
				TBL_MENU.Rows[rowNumber].Cells[0].Text = "- CHOOSE A MODULE -";
			}
			
			TBL_MENU.Rows[rowNumber].Cells[0].CssClass = "tdHeader1";
			rowNumber++;

			conn.QueryString = "select submoduleid, submoduledisplay from rfsubmodule where moduleid='" + Request.QueryString["ModuleID"] + "'";
			conn.ExecuteQuery();
			dtSubModule = conn.GetDataTable();

			for (int i = 0; i < dtSubModule.Rows.Count; i++)
			{
				TBL_MENU.Rows.Add(new TableRow());
				TBL_MENU.Rows[rowNumber].Cells.Add(new TableCell());
				TBL_MENU.Rows[rowNumber].Cells[0].Text = dtSubModule.Rows[i]["SUBMODULEDISPLAY"].ToString();
				TBL_MENU.Rows[rowNumber].Cells[0].Font.Bold = true;
				CheckBoxList chkList = new CheckBoxList();
				chkList.ID = "chkList_" + i.ToString();
				rowNumber++;

				TBL_MENU.Rows.Add(new TableRow());
				TBL_MENU.Rows[rowNumber].Cells.Add(new TableCell());
				TBL_MENU.Rows[rowNumber].Cells[0].Controls.Add(chkList);

				rowNumber++;

				if (!IsPostBack)
				{
					DataColumn[] dtMenuCodeKey = new DataColumn[1];
					DataRow[] drMenuCode = new DataRow[3];

					conn.QueryString = "select menucode, menudisplay, menulevel, menuparentid from rfmenu where submoduleid='" + dtSubModule.Rows[i]["SUBMODULEID"].ToString() + "' and moduleid='" + Request.QueryString["ModuleID"] + "'";
					conn.ExecuteQuery();
					dtMenuCode = conn.GetDataTable().Copy();

					dtMenuCodeKey[0] = dtMenuCode.Columns["menucode"];
					dtMenuCode.PrimaryKey = dtMenuCodeKey;

					for (int j = 0; j < dtMenuCode.Rows.Count; j++)
					{
						CheckBoxList chkListTemp = (CheckBoxList) Page.FindControl("chkList_" + i.ToString());
						if (dtMenuCode.Rows[j]["menulevel"].ToString() != "0")
						{
							drMenuCode[0] = dtMenuCode.Rows.Find(dtMenuCode.Rows[j]["menuparentid"].ToString());
							if (drMenuCode[0]["menulevel"].ToString() != "0")
							{
								drMenuCode[1] = dtMenuCode.Rows.Find(drMenuCode[0]["menuparentid"].ToString());
								chkListTemp.Items.Add(new ListItem(drMenuCode[1]["menudisplay"].ToString() + " > " + drMenuCode[0]["menudisplay"].ToString() + " > " + dtMenuCode.Rows[j]["menudisplay"].ToString(), dtMenuCode.Rows[j]["menucode"].ToString()));
							}
							else
								chkListTemp.Items.Add(new ListItem(drMenuCode[0]["menudisplay"].ToString() + " > " + dtMenuCode.Rows[j]["menudisplay"].ToString(), dtMenuCode.Rows[j]["menucode"].ToString()));
						}
						else
							chkListTemp.Items.Add(new ListItem(dtMenuCode.Rows[j]["menudisplay"].ToString(), dtMenuCode.Rows[j]["menucode"].ToString()));
					}
				}
			}
			Label1.Text = dtSubModule.Rows.Count.ToString();
			ViewData();
		}

		private void ViewData()
		{
			conn.QueryString = "select menucode from grpaccessmenuall where groupid='" + Request.QueryString["GroupID"] + "' and active='1' and moduleid = '" + Request.QueryString["ModuleID"] + "'";
			conn.ExecuteQuery();
			for (int i = 0; i < conn.GetRowCount(); i++)
			{
				for (int k = 0; k < dtSubModule.Rows.Count; k++)
				{
					CheckBoxList temp = (CheckBoxList) Page.FindControl("chkList_" + k.ToString());
					for (int l = 0; l < temp.Items.Count; l++)
					{
						if (temp.Items[l].Value == conn.GetFieldValue(i,0))
							temp.Items[l].Selected = true;
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

		private void UpdateMenuAccess()
		{
			// find connection string to specified module
			conn.QueryString = "select DB_IP,DB_NAMA,DB_LOGINID,DB_LOGINPWD from RFMODULE where moduleid = '" + Request.QueryString["ModuleID"] + "'";
			conn.ExecuteQuery();
			string db_ip = conn.GetFieldValue(0, "db_ip"), db_nama = conn.GetFieldValue(0, "db_nama");
			string connString = "Data Source=" + db_ip + ";Initial Catalog=" + db_nama + ";uid=" + conn.GetFieldValue(0, "db_loginid") + ";pwd=" + conn.GetFieldValue(0, "db_loginpwd") + ";Pooling=true";

			Connection conn2 = new Connection (connString);

			conn2.QueryString = "delete from grpaccessmenu where groupid = '" + Request.QueryString["GroupID"] + "'";
			conn2.ExecuteNonQuery();

			conn.QueryString = "delete from grpaccessmenuall where groupid = '" + Request.QueryString["GroupID"] + "' and moduleID = '" + Request.QueryString["ModuleID"] + "'";
			conn.ExecuteNonQuery();

			// insert new selected ones
			for (int i = 0; i < dtSubModule.Rows.Count; i++)
			{
				CheckBoxList cbTemp = (CheckBoxList) Page.FindControl("chkList_" + i.ToString());
				
				for (int j = 0; j < cbTemp.Items.Count; j++)
				{
					if (cbTemp.Items[j].Selected)
					{
						conn2.QueryString = "insert into grpaccessmenu (groupid, menucode, active) values ('" + Request.QueryString["GroupID"] + "', '" + cbTemp.Items[j].Value + "', '1')";
						conn2.ExecuteNonQuery();

						conn.QueryString = "exec SU_GRPACCESSMENUALLSAVE '" + 
							Request.QueryString["groupid"] + "', '" + 
							cbTemp.Items[j].Value + "', '" + 
							Request.QueryString["ModuleID"] + "', '" + 
							Session["UserID"].ToString() + "'";
						conn.ExecuteNonQuery();

						/**
						conn.QueryString = "insert into grpaccessmenuall (groupid, menucode, moduleid, active) values ('" + Request.QueryString["GroupID"] + "', '" + cbTemp.Items[j].Value + "', '" + Request.QueryString["ModuleID"] + "', '1')";
						conn.ExecuteQuery();
						**/
					}
				}
			}

			conn.QueryString = "select app_root, menudir from rfmodule where moduleid = '" + Request.QueryString["ModuleID"] + "'";
			conn.ExecuteQuery();
			string path = conn.GetFieldValue(0, "app_root") + conn.GetFieldValue(0, "menudir");
			string menuDir = conn.GetFieldValue(0, "menudir");

			if (Request.QueryString["ModuleID"] == "01")
				tool.GenerateMenu(Request.QueryString["GroupID"], path, menuDir, conn2, Request.QueryString["ModuleID"]);

			else if (Request.QueryString["ModuleID"] == "40")
			{
				try 
				{
					Consumer.Maintenance genMenu = new Consumer.Maintenance();
					genMenu.GenerateMenu(Request.QueryString["GroupID"], path, menuDir, connString, Request.QueryString["ModuleID"]);
				} 
				catch (Exception ex)
				{
					Response.Write("<!-- menuDir: " + menuDir + " -->");
					Response.Write("<!-- path: " + path + " -->");
					throw ex;
				}
			}
			else if (Request.QueryString["ModuleID"] == "20")
			{
				CreditCard.Maintenance genMenu = new CreditCard.Maintenance();
				genMenu.GenerateMenu(Request.QueryString["GroupID"], path, menuDir, connString, Request.QueryString["ModuleID"]);
			}
			else if (Request.QueryString["ModuleID"] == "50")
			{
				SalesCom.Maintenance genMenu = new SalesCom.Maintenance();
				genMenu.GenerateMenu(Request.QueryString["GroupID"], path, menuDir, connString, Request.QueryString["ModuleID"]);
			}
			else if (Request.QueryString["ModuleID"] == "99")
			{
				tool.GenerateMenu(Request.QueryString["GroupID"], path, menuDir, conn2, Request.QueryString["ModuleID"]);
			}
			else		//use module id from querystring
			{
				tool.GenerateMenu(Request.QueryString["GroupID"], path, menuDir, conn2, Request.QueryString["ModuleID"]);
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			try
			{

				UpdateMenuAccess();
				GlobalTools.popMessage(this, "Group Menu Access Updated!");				

			} 
			catch (Exception ex)
			{
				Response.Write("<!-- " + ex.Message.Replace("'", "").Replace("-->", ": ") + " -->\n");
				GlobalTools.popMessage(this, "Update Failed...");
			}
		}

	}
}
