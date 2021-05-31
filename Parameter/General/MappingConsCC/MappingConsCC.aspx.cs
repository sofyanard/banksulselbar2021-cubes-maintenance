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

namespace CuBES_Maintenance.Parameter.General.MappingConsCC
{
	/// <summary>
	/// Summary description for MappingConcCC.
	/// </summary>
	public partial class MappingConcCC : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string cclink;
		protected string paramname, constablename, cctablename, consid, consdesc, consccmapid, ccid, ccdesc;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();

			paramname		= Request.QueryString["tb1"];
			constablename	= Request.QueryString["tb1"];
			cctablename		= Request.QueryString["tb2"];
			consid			= Request.QueryString["f11"];
			consdesc		= Request.QueryString["f12"];
			consccmapid		= Request.QueryString["f13"];
			ccid			= Request.QueryString["f21"];
			ccdesc			= Request.QueryString["f22"];

			

			if (!IsPostBack) 
			{

				conn.QueryString = "SELECT CCLINK FROM APP_PARAMETER";
				try
				{
					conn.ExecuteQuery();
					cclink = conn.GetFieldValue(0,0);
				}
				catch 
				{
					cclink = "LNK_LOSCC.LOSCCENH.DBO.";
				}
				ViewState["cclink"] = cclink;

				GlobalTools.fillRefList(DDL_CCMAP,"select " + ccid + ", " + ccdesc + " from " + cclink + cctablename ,true,conn);
			}
			else
			{
				cclink = (string)ViewState["cclink"];
			}

			LBL_PARAMNAME.Text = paramname;

			viewExistingData();
			viewRequestData();
		}

		private void SetDBConn2()
		{
			string DB_NAMA;
			string DB_IP;
			string DB_LOGINID;
			string DB_LOGINPWD;
			conn.QueryString = "select DB_NAMA, DB_IP, DB_LOGINID, DB_LOGINPWD from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			DB_NAMA = conn.GetFieldValue("DB_NAMA");
			DB_IP = conn.GetFieldValue("DB_IP");
			DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conn2 = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
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
			this.DGExisting.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGExisting_ItemCommand);
			this.DGExisting.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGExisting_PageIndexChanged);
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion

		private void viewExistingData() 
		{
			conn2.QueryString = "select cons." + consid + " ConsID, cons." + consdesc + " ConsDESC" +
				", cons." + consccmapid + " ConsCCMAPID, cc." + ccid + " CCID ,cc." + ccdesc + " CCDESC" +
				" from " + constablename + " cons " +
				" left join " + cclink + cctablename + " cc " +
				" on cons." + consccmapid + " = cc." + ccid + " order by ConsID";
			conn2.ExecuteQuery();

			DGExisting.DataSource = conn2.GetDataTable().Copy();
			try 
			{
				DGExisting.DataBind();
			}
			catch 
			{
				DGExisting.CurrentPageIndex = DGExisting.PageCount - 1;
				DGExisting.DataBind();
			}

		}

		private void viewRequestData()
		{
			conn2.QueryString = "select map.considvalue ConsID, cons." + consdesc + " ConsDESC" +
				", map.consccmapvalue ConsCCMAPID, cc." + ccdesc + " CCDESC" +
				" from TMAPPINGCONSCC map " +
				" left join " + constablename + " cons on map.considvalue = cons." + consid +
				" left join " + cclink + cctablename + " cc on map.consccmapvalue = cc." + ccid +
				" where constable = '" + constablename + "'" + 
				" and cctable = '" + cctablename + "' " +
				" order by ConsID";
			conn2.ExecuteQuery();

			DGRequest.DataSource = conn2.GetDataTable().Copy();
			try 
			{
				DGRequest.DataBind();
			}
			catch 
			{
				DGRequest.CurrentPageIndex = DGRequest.PageCount - 1;
				DGRequest.DataBind();
			}
		}

		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					conn2.QueryString = "select cons." + consid + " ConsID, cons." + consdesc + " ConsDESC" +
						", cons." + consccmapid + " ConsCCMAPID, cc." + ccid + " CCID ,cc." + ccdesc + " CCDESC" +
						" from " + constablename + " cons " +
						" left join " + cclink + cctablename + " cc " +
						" on cons." + consccmapid + " = cc." + ccid +
						" where cons." + consid + " = '" + e.Item.Cells[0].Text.Trim() + "'";
					conn2.ExecuteQuery();

					TXT_ConsID.Text = conn2.GetFieldValue("ConsID");
					TXT_ConsDESC.Text = conn2.GetFieldValue("ConsDESC");
					try
					{
						DDL_CCMAP.SelectedValue = conn2.GetFieldValue("ConsCCMAPID");
					}
					catch{}
					break;

			}
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					conn2.QueryString = "select map.considvalue ConsID, cons." + consdesc + " ConsDESC" +
						", map.consccmapvalue ConsCCMAPID, cc." + ccdesc + " CCDESC" +
						" from TMAPPINGCONSCC map " +
						" left join " + constablename + " cons on map.considvalue = cons." + consid +
						" left join " + cclink + cctablename + " cc on map.consccmapvalue = cc." + ccid +
						" where constable = '" + constablename + "' " +
						" and cctable = '" + cctablename + "' " +
						" and considvalue = '" + e.Item.Cells[0].Text.Trim() + "'";
					conn2.ExecuteQuery();

					TXT_ConsID.Text = conn2.GetFieldValue("ConsID");
					TXT_ConsDESC.Text = conn2.GetFieldValue("ConsDESC");
					try
					{
						DDL_CCMAP.SelectedValue = conn2.GetFieldValue("ConsCCMAPID");
					}
					catch{}
					break;
				case "delete":
					conn2.QueryString = "delete from TMAPPINGCONSCC where constable = '" + constablename + "' " +
						" and cctable = '" + cctablename + "' " +
						" and considvalue = '" + e.Item.Cells[0].Text.Trim() + "'";
					conn2.ExecuteQuery();
					viewRequestData();
					break;
				default:
					break;			
			}  
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		private void clearEditBoxes()
		{
			TXT_ConsID.Text = "";
			TXT_ConsDESC.Text = "";
			DDL_CCMAP.SelectedIndex = 0;
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (TXT_ConsID.Text != "")
			{
				conn2.QueryString = "EXEC PARAM_GENERAL_MAPPINGCONSCC '" + constablename + "', '" +
					consid + "', '" + consccmapid + "', '" + cctablename + "', '" + ccid + "', '" +
					TXT_ConsID.Text + "', '" + DDL_CCMAP.SelectedValue + "'";
				conn2.ExecuteNonQuery();

				conn2.ClearData();
				viewRequestData();
				clearEditBoxes();
			}
			else
			{
				GlobalTools.popMessage(this,"Select one from Existing Data!");
			}
		}

		private void DGExisting_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		}

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewRequestData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../CreditCardParam.aspx?mc=9902040501&moduleId=40");
		}


	}
}
