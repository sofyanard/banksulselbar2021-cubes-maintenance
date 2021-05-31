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
	/// Summary description for RFProgram.
	/// </summary>
	public partial class RFProgram : System.Web.UI.Page
	{
	
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

		//	if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//	Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack)
			{
				//RFAREA
				conn.QueryString = "SELECT * FROM RFAREA ";
				conn.ExecuteQuery();
				LIST_AREA.Items.Clear();
				for (int i=0; i<conn.GetRowCount(); i++)
					LIST_AREA.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				//RFPRODUCT
				//--- Modified by Yudi ---
				//conn.QueryString = "select productid , '(' + sibs_prodid + ')' + SPACE(4 - LEN(sibs_prodid)) + productdesc from rfproduct WHERE ACTIVE = '1' ";
				conn.QueryString = "select productid , '(' + sibs_prodid + ')(' + sibs_prodcode + ') ' + productdesc as productdesc from rfproduct WHERE ACTIVE = '1' order by PRODUCTDESC";
				//------------------------
				conn.ExecuteQuery();
				ddl_PRODUCTID.Items.Clear();
				ddl_PRODUCTID.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					ddl_PRODUCTID.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				//RFSCORINGTYPE
				conn.QueryString = "select scrid, scrdesc from rfscoringtype order by SCRDESC ";
				conn.ExecuteQuery();
				DDL_SCRID.Items.Clear();
				DDL_SCRID.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_SCRID.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
				
				//RFBUSINESSUNIT
				conn.QueryString = "select * from rfbusinessunit where active = '1' order by BUSSUNITDESC";
				conn.ExecuteQuery();
				DDL_BUSINESSUNIT.Items.Clear();
				DDL_BUSINESSUNIT.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_BUSINESSUNIT.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				fillProgram();
				bindData2();
				bindData3();
			}
			Datagrid2.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change2);
			Datagrid3.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change3);
		}

		private void fillProgram()
		{
			//PROGRAM 
			LIST_PROGRAM.Items.Clear();
			if (LIST_AREA.SelectedValue.Trim() == "")
				return;
			conn.QueryString = "SELECT PROGRAMID, PROGRAMDESC " +
				"FROM RFPROGRAM WHERE ACTIVE = '1' AND AREAID = '" + LIST_AREA.SelectedValue.Trim() + "' order by PROGRAMDESC";
			conn.ExecuteQuery();
			for (int i=0; i<conn.GetRowCount(); i++)
			{
				LIST_PROGRAM.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
			}
			fillProduct();
		}

		private void fillProduct()
		{
			//PRODUCT 
			LIST_PRODUCT.Items.Clear();
			if (LIST_PROGRAM.SelectedValue.Trim() == "")
				return;
			conn.QueryString = "SELECT prog_prod.PRODUCTID, '(' + SIBS_PRODID + ')(' + SIBS_PRODCODE + ') ' + PRODUCTDESC FROM RFPRODUCT " +
				"inner join prog_prod on prog_prod.PRODUCTID = RFPRODUCT.PRODUCTID " +
				"WHERE PROGRAMID = '" + LIST_PROGRAM.SelectedValue.Trim() + "' order by PRODUCTDESC";
			conn.ExecuteQuery();
			for (int i=0; i<conn.GetRowCount(); i++)
				LIST_PRODUCT.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
		}

		private void bindData2()
		{
			DataTable dt = new DataTable();
			DataRow dr;
			conn.QueryString = "SELECT AREANAME, PROGRAMID, PROGRAMDESC, "+
				"case WITHFAIRISAAC when '1' then 'YES' when '0' then 'NO' end WITHFAIRISAAC, SCRDESC, " +
				"AREAID, SCRID, case APRVFOUREYES when '1' then 'YES' when '0' then 'NO' end APRVFOUREYES, " +
				"case WITHDRAWL when '1' then 'YES' when '0' then 'NO' end WITHDRAWL, BUSINESSUNIT, BUSSUNITDESC, " +
				"STATUSDESC, PENDINGSTATUS " +
				"FROM VW_PARAM_GENERAL_PENDING_RFPROGRAM ";
			conn.ExecuteQuery();
			dt.Columns.Add(new DataColumn("AREANAME"));
			dt.Columns.Add(new DataColumn("PROGRAMID"));
			dt.Columns.Add(new DataColumn("PROGRAMDESC"));
			dt.Columns.Add(new DataColumn("WITHFAIRISAAC"));
			dt.Columns.Add(new DataColumn("SCRDESC"));
			dt.Columns.Add(new DataColumn("AREAID"));
			dt.Columns.Add(new DataColumn("SCRID"));
			dt.Columns.Add(new DataColumn("APRVFOUREYES"));
			dt.Columns.Add(new DataColumn("WITHDRAWL"));
			dt.Columns.Add(new DataColumn("BUSINESSUNIT"));
			dt.Columns.Add(new DataColumn("BUSSUNITDESC"));
			dt.Columns.Add(new DataColumn("STATUSDESC"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));

			for (int i = 0; i < conn.GetRowCount(); i++) 
			{
				dr = dt.NewRow();
				for (int j = 0; j < conn.GetColumnCount(); j++)
				{
					dr[j] = conn.GetFieldValue(i,j);
				}
				dt.Rows.Add(dr);
			}
			Datagrid2.DataSource = new DataView(dt);
			try 
			{
				Datagrid2.DataBind();
			}
			catch 
			{
				Datagrid2.CurrentPageIndex = Datagrid2.PageCount - 1;
				Datagrid2.DataBind();
			}
		}

		private void bindData3()
		{
			DataTable dt = new DataTable();
			DataRow dr;
			conn.QueryString = "SELECT PROGRAMID, PROGRAMDESC, PRODUCTID, PRODUCTDESC, "+
				"STATUSDESC, PENDINGSTATUS "+
				"FROM VW_PARAM_GENERAL_PENDING_PROG_PROD ";
			conn.ExecuteQuery();
			dt.Columns.Add(new DataColumn("PROGRAMID"));
			dt.Columns.Add(new DataColumn("PROGRAMDESC"));
			dt.Columns.Add(new DataColumn("PRODUCTID"));
			dt.Columns.Add(new DataColumn("PRODUCTDESC"));
			dt.Columns.Add(new DataColumn("STATUSDESC"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));

			for (int i = 0; i < conn.GetRowCount(); i++) 
			{
				dr = dt.NewRow();
				for (int j = 0; j < conn.GetColumnCount(); j++)
				{
					dr[j] = conn.GetFieldValue(i,j);
				}
				dt.Rows.Add(dr);
			}
			Datagrid3.DataSource = new DataView(dt);
			try 
			{
				Datagrid3.DataBind();
			}
			catch 
			{
				Datagrid3.CurrentPageIndex = Datagrid3.PageCount - 1;
				Datagrid3.DataBind();
			}
		}

		private void clearEditBoxes()
		{
			TXT_PROGRAMID.Text = "";
			TXT_PROGRAMDESC.Text = "";
			try 
			{
				RDO_WITHFAIRISAAC.SelectedValue = "0";
			}
			catch {}
			try
			{
				DDL_SCRID.SelectedIndex = 0;
			} 
			catch {}
			try
			{
				RDO_APRVFOUREYES.SelectedValue = "0";
			} 
			catch {}
			try
			{
				DDL_BUSINESSUNIT.SelectedIndex = 0;
			} 
			catch {}
			try
			{
				RDO_WITHDRAWL.SelectedValue = "0";
			} 
			catch {}
			LBL_SAVEMODE.Text = "1";
			activatePostBackControls(true);
		}

		private void activatePostBackControls(bool mode)
		{
			LIST_AREA.AutoPostBack = mode;
			LIST_PROGRAM.Enabled = mode;
		}

		private void cleansTextBox (TextBox tb)
		{
			if (tb.Text.Trim() == "&nbsp;")
				tb.Text = "";
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
			this.Datagrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_ItemCommand);
			this.Datagrid3.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid3_ItemCommand);

		}
		#endregion

		void Grid_Change2(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid2.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData2();	
		}

		void Grid_Change3(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid3.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData3();	
		}

		protected void LIST_AREA_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int count = 0;
			for (int i = 0; i < LIST_AREA.Items.Count; i++)
				if (LIST_AREA.Items[i].Selected) count++;
			if (count == LIST_AREA.Items.Count)
				CHK_CheckAll.Checked = true;
			else
				CHK_CheckAll.Checked = false;
			if (count == 1)
				fillProgram();
			else
			{
				LIST_PROGRAM.Items.Clear();
				LIST_PRODUCT.Items.Clear();
			}
		}

		protected void LIST_PROGRAM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillProduct();
		}

		private void Datagrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[12].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_PROGRAMID.Text = e.Item.Cells[1].Text.Trim();
					TXT_PROGRAMDESC.Text = e.Item.Cells[2].Text.Trim();
					try
					{
						LIST_AREA.SelectedValue = e.Item.Cells[5].Text.Trim();
					} 
					catch {}
					fillProgram();
					try
					{
						if (e.Item.Cells[3].Text.Trim() == "YES")
							RDO_WITHFAIRISAAC.SelectedValue = "1";
						else
							RDO_WITHFAIRISAAC.SelectedValue = "0";
					} 
					catch {}
					try
					{
						DDL_SCRID.SelectedValue = e.Item.Cells[6].Text.Trim();
					} 
					catch {}
					try
					{
						if (e.Item.Cells[7].Text.Trim() == "YES")
							RDO_APRVFOUREYES.SelectedValue = "1";
						else
							RDO_APRVFOUREYES.SelectedValue = "0";
					} 
					catch {}
					try
					{
						if (e.Item.Cells[8].Text.Trim() == "YES")
							RDO_WITHDRAWL.SelectedValue = "1";
						else
							RDO_WITHDRAWL.SelectedValue = "0";
					} 
					catch {}
					try
					{
						DDL_BUSINESSUNIT.SelectedValue = e.Item.Cells[10].Text.Trim();
					} 
					catch {}
					cleansTextBox(TXT_PROGRAMID);
					cleansTextBox(TXT_PROGRAMDESC);
					activatePostBackControls(false);
					break;
				case "delete":
					string AREAID = e.Item.Cells[5].Text.Trim(), 
						PROGRAMID = e.Item.Cells[1].Text.Trim();
					conn.QueryString = "DELETE FROM PENDING_RFPROGRAM WHERE PROGRAMID = '"+ PROGRAMID +
						"' AND AREAID = '" + AREAID + "' ";
					conn.ExecuteQuery();
					bindData2();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void Datagrid3_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "delete":
					string PROGRAMID = e.Item.Cells[2].Text.Trim(), 
						PRODUCTID = e.Item.Cells[3].Text.Trim();
					conn.QueryString = "DELETE FROM PENDING_PROG_PROD WHERE PROGRAMID = '"+ PROGRAMID +
						"' AND PRODUCTID = '" + PRODUCTID + "' ";
					conn.ExecuteQuery();
					bindData3();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		protected void BTN_EDITLISTPROGRAM_Click(object sender, System.EventArgs e)
		{
			LBL_SAVEMODE.Text = "0";
			TXT_PROGRAMID.Text = LIST_PROGRAM.SelectedValue.Trim();
			TXT_PROGRAMDESC.Text = LIST_PROGRAM.SelectedItem.Text.Trim();
			conn.QueryString = "SELECT WITHFAIRISAAC, SCRID, APRVFOUREYES, BUSINESSUNIT, WITHDRAWL " +
				"FROM RFPROGRAM WHERE ACTIVE = '1' AND AREAID = '" + LIST_AREA.SelectedValue.Trim() +
				"' AND PROGRAMID = '" + TXT_PROGRAMID.Text.Trim() + "' ";
			conn.ExecuteQuery();
			try
			{
				RDO_WITHFAIRISAAC.SelectedValue = conn.GetFieldValue(0,0).Trim();
			} 
			catch {}
			try
			{
				DDL_SCRID.SelectedValue = conn.GetFieldValue(0,1).Trim();
			} 
			catch {}
			try
			{
				RDO_APRVFOUREYES.SelectedValue = conn.GetFieldValue(0,2).Trim();
			} 
			catch {}
			try
			{
				DDL_BUSINESSUNIT.SelectedValue = conn.GetFieldValue(0,3).Trim();
			} 
			catch {}
			try
			{
				RDO_WITHDRAWL.SelectedValue = conn.GetFieldValue(0,4).Trim();
			} 
			catch {}
			activatePostBackControls(false);
		}

		protected void BTN_DELETELISTPROGRAM_Click(object sender, System.EventArgs e)
		{
			string AREAID = LIST_AREA.SelectedValue.Trim(), 
				PROGRAMID = LIST_PROGRAM.SelectedValue.Trim();
			if ((PROGRAMID == "") || (AREAID == "")) 
				return;
			conn.QueryString = "PARAM_GENERAL_RFPROGRAM_MAKER '2', '"+ AREAID + "', '" + PROGRAMID + "' ";
			conn.ExecuteQuery();
			bindData2();
		}

		protected void BTN_ADDPROD_Click(object sender, System.EventArgs e)
		{
			string PROGRAMID = LIST_PROGRAM.SelectedValue.Trim(),
				PRODUCTID = ddl_PRODUCTID.SelectedValue.Trim();
			bool found = false;
			if ((PROGRAMID == "") || (PRODUCTID == "")) 
				return;
			for (int i = 0; i < LIST_PRODUCT.Items.Count; i++)
				if (PRODUCTID.Trim() == LIST_PRODUCT.Items[i].Value.Trim())
				{
					found = true;
					break;
				}
			if (found)
			{
				Tools.popMessage(this, "The Product has already been used! Request canceled!");
				return;
			}
			conn.QueryString = "PARAM_GENERAL_PROG_PROD_MAKER '1', '"+ PROGRAMID + "', '" + PRODUCTID + "' ";
			conn.ExecuteQuery();
			bindData3();
		}

		protected void BTN_DELETELISTPRODUCT_Click(object sender, System.EventArgs e)
		{
			string PROGRAMID = LIST_PROGRAM.SelectedValue.Trim(),
				PRODUCTID = LIST_PRODUCT.SelectedValue.Trim();
			if ((PROGRAMID == "") || (PRODUCTID == "")) 
				return;
			conn.QueryString = "PARAM_GENERAL_PROG_PROD_MAKER '2', '"+ PROGRAMID + "', '" + PRODUCTID + "' ";
			conn.ExecuteQuery();
			bindData3();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			bool found = false;
			if (TXT_PROGRAMID.Text.Trim() == "")
			{
				Tools.popMessage(this, "Program Code harus diisi! ");
				return;
			}
			if (LBL_SAVEMODE.Text.Trim() == "1")
			{
				conn.QueryString = "SELECT PROGRAMID FROM RFPROGRAM WHERE PROGRAMID = '"+
					TXT_PROGRAMID.Text.Trim() + "' ";
				conn.ExecuteQuery();
				if (conn.GetRowCount() > 0)
				{
					Tools.popMessage(this, "Program Code has already been used! Request canceled!");
					return;
				}
			}

			for (int i = 0; i < LIST_AREA.Items.Count; i++)
			{
				found = true;
				if (!LIST_AREA.Items[i].Selected) continue;
				conn.QueryString = "PARAM_GENERAL_RFPROGRAM_MAKER '"+ LBL_SAVEMODE.Text.Trim() + "', '" +
					LIST_AREA.Items[i].Value.Trim() + "', '" + TXT_PROGRAMID.Text.Trim() + "', '" +
					TXT_PROGRAMDESC.Text.Trim() + "', '" + RDO_WITHFAIRISAAC.SelectedValue.Trim() + "', '" +
					DDL_SCRID.SelectedValue.Trim() + "', '" + RDO_APRVFOUREYES.SelectedValue.Trim() + "', '" +
					DDL_BUSINESSUNIT.SelectedValue.Trim() + "', '" + RDO_WITHDRAWL.SelectedValue.Trim() + "' ";
				conn.ExecuteQuery();
			}
			if (!found)
			{
				Tools.popMessage(this, "Area harus dipilih! ");
				return;
			}
			bindData2();
			clearEditBoxes();
			//Tools.popMessage(this, "Data added for insertion/modification!");
		}

		protected void CHK_CheckAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if (CHK_CheckAll.Checked)
			{
				for(int i = 0; i < LIST_AREA.Items.Count; i++)
					LIST_AREA.Items[i].Selected = true;
				LIST_AREA.Enabled = false;
				LIST_PROGRAM.Items.Clear();
				LIST_PRODUCT.Items.Clear();
			}
			else
				LIST_AREA.Enabled = true;
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../AreaParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		protected void Datagrid3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

	}
}
