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

namespace CuBES_Maintenance.Parameter.Area.SME
{
	/// <summary>
	/// Summary description for RFProcedurePath.
	/// </summary>
	public partial class RFProcedurePath : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		int jml_row;
		string AREAID, PROGRAMID, PRODUCTID, TRACKFROM, TRACKNEXT, TRACKBACK, TRACKFAIL;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//	Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack)
			{
				IsiDDL();
				ViewCurrent();
				ViewMaker();
			}
			DGR_CURRENT.PageIndexChanged += new DataGridPageChangedEventHandler(this.CH_DGR_CURRENT);
			DGR_MAKER.PageIndexChanged += new DataGridPageChangedEventHandler(this.CH_DGR_MAKER);
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
			this.DGR_MAKER.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_MAKER_ItemCommand);

		}
		#endregion

		private void IsiDDL()
		{
			DDL_PP_TRACKNEXT.Items.Add(new ListItem("-- Pilih --", ""));
			DDL_PP_TRACKBACK.Items.Add(new ListItem("-- Pilih --", ""));
			DDL_PP_TRACKFAIL.Items.Add(new ListItem("-- Pilih --", ""));

			conn.QueryString = "select AREAID, AREANAME from RFAREA ";
			conn.ExecuteQuery();
			jml_row = conn.GetRowCount();
			//string AREAID1, PROGRAMID1, PRODUCTID1;
			for (int i=0; i<jml_row; i++)
				DDL_AREAID.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

			ViewDDLProg();

			conn.QueryString = "select TRACKCODE, TRACKNAME from RFTRACK where ACTIVE = '1'";
			conn.ExecuteQuery();
			jml_row = conn.GetRowCount();
			for (int i=0; i<jml_row; i++)
			{
				TRACKFROM = conn.GetFieldValue(i, 0);
				DDL_PP_TRACKNEXT.Items.Add(new ListItem(TRACKFROM +" - "+ conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
			}

			conn.QueryString = "select TRACKCODE, TRACKNAME from RFTRACK where ACTIVE = '1'";
			conn.ExecuteQuery();
			jml_row = conn.GetRowCount();
			for (int i=0; i<jml_row; i++)
			{
				TRACKFROM = conn.GetFieldValue(i, 0);
				DDL_PP_TRACKBACK.Items.Add(new ListItem(TRACKFROM +" - "+ conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
			}

			conn.QueryString = "select TRACKCODE, TRACKNAME from RFTRACK where ACTIVE = '1'";
			conn.ExecuteQuery();
			jml_row = conn.GetRowCount();
			for (int i=0; i<jml_row; i++)
			{
				TRACKFROM = conn.GetFieldValue(i, 0);
				DDL_PP_TRACKFAIL.Items.Add(new ListItem(TRACKFROM +" - "+ conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
			}

		}

		private void ViewCurrent()
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_PROCEDUREPATH ";
			conn.ExecuteQuery();
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_CURRENT.DataSource = data;
			DGR_CURRENT.DataBind();
		}

		void CH_DGR_CURRENT(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DGR_CURRENT.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			ViewCurrent();
		}
		
		private void ViewMaker()
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_PROCEDUREPATHMAKER "+
				"order by PENDINGSTATUS ";
			conn.ExecuteQuery();
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_MAKER.DataSource = data;
			DGR_MAKER.DataBind();
		}
		
		void CH_DGR_MAKER(Object sender, DataGridPageChangedEventArgs e) 
		{
			DGR_MAKER.CurrentPageIndex = e.NewPageIndex;
			ViewMaker();
		}

		private void ViewDDLProg()
		{

			DDL_PROGRAMID.Items.Clear();
			conn.QueryString = "select PROGRAMID, PROGRAMDESC from RFPROGRAM "+
				"where ACTIVE = '1' and AREAID = '"+ DDL_AREAID.SelectedValue +"' "+
				"order by PROGRAMID ";
			conn.ExecuteQuery();
			jml_row = conn.GetRowCount();
			for (int i=0; i<jml_row; i++)
			{
				PROGRAMID = conn.GetFieldValue(i, 0);
				DDL_PROGRAMID.Items.Add(new ListItem(PROGRAMID +" - "+ conn.GetFieldValue(i, 1), PROGRAMID));
			}
			ViewDDLProd();
		}

		private void ViewDDLProd()
		{
			DDL_PRODUCTID.Items.Clear();
			conn.QueryString = "select PD.PRODUCTID, PD.PRODUCTDESC "+
				"from RFPRODUCT PD "+
				"join PROG_PROD PP on PD.PRODUCTID = PP.PRODUCTID "+
				"where ACTIVE = '1' and PP.PROGRAMID = '"+ DDL_PROGRAMID.SelectedValue +"' "+
				"order by PD.PRODUCTID ";
			conn.ExecuteQuery();
			jml_row = conn.GetRowCount();
			for (int i=0; i<jml_row; i++)
			{
				PRODUCTID = conn.GetFieldValue(i, 0);
				DDL_PRODUCTID.Items.Add(new ListItem(PRODUCTID +" - "+ conn.GetFieldValue(i, 1), PRODUCTID));
			}
			ViewDDLTrackFrom();
		}

		private void ViewDDLTrackFrom()
		{
			DDL_PP_TRACKFROM.Items.Clear();
			conn.QueryString = "exec PARAM_GENERAL_PP_TRACKFROM '"+ DDL_AREAID.SelectedValue +"', '"+ DDL_PROGRAMID.SelectedValue +"', '"+ DDL_PRODUCTID.SelectedValue +"' ";
			conn.ExecuteQuery();
			jml_row = conn.GetRowCount();
			for (int i=0; i<jml_row; i++)
			{
				TRACKFROM = conn.GetFieldValue(i, 0);
				DDL_PP_TRACKFROM.Items.Add(new ListItem(TRACKFROM +" - "+ conn.GetFieldValue(i, 1), TRACKFROM));
			}

		}

		protected void DDL_AREAID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewDDLProg();
		}

		protected void DDL_PROGRAMID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewDDLProd();
		}

		protected void DDL_PRODUCTID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewDDLTrackFrom();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			SaveMaker(DDL_AREAID.SelectedValue, DDL_PROGRAMID.SelectedValue,
				DDL_PRODUCTID.SelectedValue, DDL_PP_TRACKFROM.SelectedValue, DDL_PP_TRACKNEXT.SelectedValue,
				DDL_PP_TRACKBACK.SelectedValue, DDL_PP_TRACKFAIL.SelectedValue);
		}

		private void SaveMaker(string V_AREAID, string V_PROGRAMID, string V_PRODUCTID, string V_PP_TRACKFROM, string V_PP_TRACKNEXT, string V_PP_TRACKBACK, string V_PP_TRACKFAIL)
		{
			string V_TIPE = LBL_SAVEMODE.Text;
			string V_PENDINGSTATUS = LBL_PENDINGSTATUS.Text;
			//Response.Write("V_TIPE = "+ V_TIPE +"<br>");
			//Response.Write("V_PENDINGSTATUS = "+ V_PENDINGSTATUS +"<br>");
			switch (V_TIPE)
			{
				case "0" :
					// ASALNYA DARI CURRENT
					conn.QueryString = "exec PARAM_GENERAL_PROCEDUREPATH_MAKER '"+
						V_AREAID +"', '"+ V_PROGRAMID +"', '"+ V_PRODUCTID +"', '"+ V_PP_TRACKFROM +"', '"+
						V_PP_TRACKNEXT +"', '"+ V_PP_TRACKBACK +"', '"+ V_PP_TRACKFAIL + "', '"+ V_PENDINGSTATUS +"' ";
					conn.ExecuteNonQuery();
					//Response.Write(conn.QueryString);
					break;
				case "1" :
					// ASALNYA DARI MAKER
					if (DDL_AREAID.SelectedValue == LBL_AREAID_OLD.Text && DDL_PROGRAMID.SelectedValue == LBL_PROGRAMID_OLD.Text && DDL_PRODUCTID.SelectedValue == LBL_PRODUCTID_OLD.Text && DDL_PP_TRACKFROM.SelectedValue == LBL_PP_TRACKFROM_OLD.Text)
					{
						conn.QueryString = "update PENDING_PROCEDUREPATH set PP_TRACKNEXT = '"+
							DDL_PP_TRACKNEXT.SelectedValue +"', PP_TRACKBACK = '"+ DDL_PP_TRACKBACK.SelectedValue +" "+
							"', PP_TRACKFAIL = '"+ DDL_PP_TRACKFAIL.SelectedValue +"' "+
							"where AREAID = '"+ LBL_AREAID_OLD.Text +"' and PROGRAMID = '"+ LBL_PROGRAMID_OLD.Text +"' "+
							"and PRODUCTID = '"+ LBL_PRODUCTID_OLD.Text +"' and PP_TRACKFROM = '"+ LBL_PP_TRACKFROM_OLD.Text +"' ";
						conn.ExecuteNonQuery();
					}
					else
					{
						conn.QueryString = "select PP_TRACKFROM from PENDING_PROCEDUREPATH where AREAID = '"+
							DDL_AREAID.SelectedValue +"' and PROGRAMID = '"+ DDL_PROGRAMID.SelectedValue +"' "+
							"and PRODUCTID = '"+ DDL_PRODUCTID.SelectedValue +"' and PP_TRACKFROM = '"+
							DDL_PP_TRACKFROM.SelectedValue +"' ";
						//Response.Write(conn.QueryString);
						conn.ExecuteQuery();
						if (conn.GetRowCount() > 0)
							Tools.popMessage(this, "Procedure Path Exist...");
						else
						{
							conn.QueryString = "update PENDING_PROCEDUREPATH set AREAID = '"+ DDL_AREAID.SelectedValue +"', "+
								"PROGRAMID = '"+ DDL_PROGRAMID.SelectedValue +"', PRODUCTID = '"+ DDL_PRODUCTID.SelectedValue +"', "+
								"PP_TRACKFROM = '"+ DDL_PP_TRACKFROM.SelectedValue +"', PP_TRACKNEXT = '"+ DDL_PP_TRACKNEXT.SelectedValue +"', "+
								"PP_TRACKBACK = '"+ DDL_PP_TRACKBACK.SelectedValue +"', PP_TRACKFAIL = '"+ DDL_PP_TRACKFAIL.SelectedValue +"' "+
								"where AREAID = '"+ LBL_AREAID_OLD.Text +"' and PROGRAMID = '"+ LBL_PROGRAMID_OLD.Text +"' "+
								"and PRODUCTID = '"+ LBL_PRODUCTID_OLD.Text +"' and PP_TRACKFROM = '"+ LBL_PP_TRACKFROM_OLD.Text +"' ";
							conn.ExecuteNonQuery();
						}

					}
					break;
			}
			clearEditBoxes();
			ViewMaker();
		}


		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		private void clearEditBoxes()
		{
			activatePostBackControls(true);
			DDL_AREAID.SelectedIndex = 0;
			DDL_PP_TRACKNEXT.SelectedValue = "";
			DDL_PP_TRACKBACK.SelectedValue = "";
			DDL_PP_TRACKFAIL.SelectedValue = "";
			LBL_SAVEMODE.Text = "0";
			LBL_PENDINGSTATUS.Text = "1";
			ViewDDLProg();
		}

		private void activatePostBackControls(bool mode)
		{
			
			DDL_AREAID.Enabled = mode;
			DDL_PROGRAMID.Enabled = mode;
			DDL_PRODUCTID.Enabled = mode;
			DDL_PP_TRACKFROM.Enabled = mode;
		}

		private void DGR_CURRENT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			LBL_SAVEMODE.Text = "0";
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					try
					{
						DDL_AREAID.SelectedValue = e.Item.Cells[0].Text.Trim();
					} 
					catch{}
					ViewDDLProg();
					try 
					{
						DDL_PROGRAMID.SelectedValue = e.Item.Cells[2].Text.Trim();
					} 
					catch{}
					ViewDDLProd();
					try 
					{
						DDL_PRODUCTID.SelectedValue = e.Item.Cells[4].Text.Trim();
					} 
					catch{}
					try
					{
						DDL_PP_TRACKFROM.SelectedValue = e.Item.Cells[6].Text.Trim();
					} 
					catch{}
					TRACKNEXT = e.Item.Cells[8].Text.Trim();
					if (TRACKNEXT != "" && TRACKNEXT != "&nbsp;")
						DDL_PP_TRACKNEXT.SelectedValue = TRACKNEXT;
					else
						DDL_PP_TRACKNEXT.SelectedIndex = 0;
					TRACKBACK = e.Item.Cells[10].Text.Trim();
					if (TRACKBACK != "" && TRACKBACK != "&nbsp;")
						DDL_PP_TRACKBACK.SelectedValue = TRACKBACK;
					else
						DDL_PP_TRACKBACK.SelectedIndex = 0;
					TRACKFAIL = e.Item.Cells[12].Text.Trim();
					if (TRACKFAIL != "" && TRACKFAIL != "&nbsp;")
						DDL_PP_TRACKFAIL.SelectedValue = TRACKFAIL;
					else
						DDL_PP_TRACKFAIL.SelectedIndex = 0;
					LBL_PENDINGSTATUS.Text = "0";
					activatePostBackControls(false);
					break;
				case "delete":
					AREAID = e.Item.Cells[0].Text.Trim();
					PROGRAMID = e.Item.Cells[2].Text.Trim();
					PRODUCTID = e.Item.Cells[4].Text.Trim();
					TRACKFROM = e.Item.Cells[6].Text.Trim();
					TRACKNEXT = e.Item.Cells[8].Text.Trim();
					TRACKBACK = e.Item.Cells[10].Text.Trim();
					TRACKFAIL = e.Item.Cells[12].Text.Trim();
					if (TRACKNEXT == "&nbsp;")
						TRACKNEXT = "";
					if (TRACKBACK == "&nbsp;")
						TRACKBACK = "";
					if (TRACKFAIL == "&nbsp;")
						TRACKFAIL = "";
					LBL_PENDINGSTATUS.Text = "2";
					SaveMaker(AREAID, PROGRAMID, PRODUCTID, TRACKFROM, TRACKNEXT, TRACKBACK, TRACKFAIL);
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_MAKER_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					AREAID = e.Item.Cells[0].Text.Trim();
					PROGRAMID = e.Item.Cells[2].Text.Trim();
					PRODUCTID = e.Item.Cells[4].Text.Trim();
					TRACKFROM = e.Item.Cells[6].Text.Trim();
					TRACKNEXT = e.Item.Cells[8].Text.Trim();
					TRACKBACK = e.Item.Cells[10].Text.Trim();
					TRACKFAIL = e.Item.Cells[12].Text.Trim();
					LBL_AREAID_OLD.Text = AREAID;
					LBL_PROGRAMID_OLD.Text = PROGRAMID;
					LBL_PRODUCTID_OLD.Text = PRODUCTID;
					LBL_PP_TRACKFROM_OLD.Text = TRACKFROM;
					LBL_PP_TRACKNEXT_OLD.Text = TRACKNEXT;
					LBL_PP_TRACKBACK_OLD.Text = TRACKBACK;
					LBL_PP_TRACKFAIL_OLD.Text = TRACKFAIL;
					try
					{
						DDL_AREAID.SelectedValue = AREAID;
					} 
					catch{}
					try
					{
						DDL_PROGRAMID.SelectedValue = PROGRAMID;
					} 
					catch{}
					try
					{
						DDL_PRODUCTID.SelectedValue = PRODUCTID;
					} 
					catch{}
					try
					{
						DDL_PP_TRACKFROM.SelectedValue = TRACKFROM;
					} 
					catch{}
					if (TRACKNEXT != "" && TRACKNEXT != "&nbsp;")
						DDL_PP_TRACKNEXT.SelectedValue = TRACKNEXT;
					else
						DDL_PP_TRACKNEXT.SelectedIndex = 0;
					if (TRACKBACK != "" && TRACKBACK != "&nbsp;")
						DDL_PP_TRACKBACK.SelectedValue = TRACKBACK;
					else
						DDL_PP_TRACKBACK.SelectedIndex = 0;
					if (TRACKFAIL != "" && TRACKFAIL != "&nbsp;")
						DDL_PP_TRACKFAIL.SelectedValue = TRACKFAIL;
					else
						DDL_PP_TRACKFAIL.SelectedIndex = 0;
					activatePostBackControls(false);
					break;
				case "delete":
					AREAID = e.Item.Cells[0].Text.Trim();
					PROGRAMID = e.Item.Cells[2].Text.Trim();
					PRODUCTID = e.Item.Cells[4].Text.Trim();
					TRACKFROM = e.Item.Cells[6].Text.Trim();
					TRACKNEXT = e.Item.Cells[8].Text.Trim();
					TRACKBACK = e.Item.Cells[10].Text.Trim();
					TRACKFAIL = e.Item.Cells[12].Text.Trim();
					conn.QueryString = "DELETE FROM PENDING_PROCEDUREPATH "+
						"where AREAID = '"+ AREAID +"' and PROGRAMID = '"+ PROGRAMID +"' "+
						"and PRODUCTID = '"+ PRODUCTID + "' and PP_TRACKFROM = '"+ TRACKFROM +"' ";
					conn.ExecuteNonQuery();
					clearEditBoxes();
					ViewMaker();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../GeneralParam.aspx");
			//Response.Redirect("../../AreaParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleID=01");
			Response.Redirect("../../CommonParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void RBL_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.RBL_MODULE.SelectedValue == "20" || this.RBL_MODULE.SelectedValue == "40")
				Response.Redirect("../../General/PathProcedureListAll.aspx?moduleID=" + 
					RBL_MODULE.SelectedValue);
			this.Label1.Text = this.RBL_MODULE.SelectedValue;
		}
	}
}
