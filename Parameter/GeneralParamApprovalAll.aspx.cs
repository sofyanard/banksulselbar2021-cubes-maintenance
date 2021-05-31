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

namespace CuBES_Maintenance.Parameter
{
	/// <summary>
	/// Summary description for GeneralParamApprovalAll.
	/// </summary>
	public partial class GeneralParamApprovalAll : System.Web.UI.Page
	{
		protected Connection connMnt;
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			connMnt = new Connection((string) Session["ConnString"]);
					
			int divider = 0, counter = 0, column = 0;

			conn.QueryString = "SELECT PARAMNAME,PARAMLINK,MODULEID FROM RFPARAMETERALL " +
				"WHERE ISMAKER='0' AND PG_ID='1' AND CLASSID='' AND MODULEID='" + Request.QueryString["ModuleID"]+ "' " +
				"ORDER BY PARAMNAME";
			
			if(Request.QueryString["ModuleID"].Equals("01"))
			{
				try
				{
					if ((Request.QueryString["pg"]=="7") || (Request.QueryString["pg"]=="8") )
						conn.QueryString = "SELECT PARAMNAME,PARAMLINK,MODULEID FROM RFPARAMETERALL " +
							"WHERE ISMAKER='0' AND PG_ID='" + Request.QueryString["pg"]+ "' AND CLASSID='' AND MODULEID='" + Request.QueryString["ModuleID"]+ "' " +
							"ORDER BY PARAMNAME";
                    //Bank Papua
                    if ((Request.QueryString["pg"] == "11") || (Request.QueryString["pg"] == "12") || (Request.QueryString["pg"] == "13") || (Request.QueryString["pg"] == "14") || (Request.QueryString["pg"] == "15"))
                        conn.QueryString = "SELECT PARAMNAME,PARAMLINK,MODULEID FROM RFPARAMETERALL " +
                            "WHERE ISMAKER='0' AND PG_ID='" + Request.QueryString["pg"] + "' AND CLASSID='' AND MODULEID='" + Request.QueryString["ModuleID"] + "' " +
                            "ORDER BY PARAMNAME";
				} 
				catch {}
			}

			conn.ExecuteQuery();

			divider = conn.GetRowCount() / 3;
			for (int j = 0; j <= divider; j++)
				Table2.Rows.Add(new TableRow());

			for (int i = 0; i < conn.GetRowCount(); i++)
			{
				if (counter > divider)
				{
					counter = 0;
					if (column == 2)
						column = 0;
					else
						column++;
				}

				string navURL = conn.GetFieldValue(i,1);
				/*
				if(conn.GetFieldValue(i,1) != "" )
				{ navURL+="&moduleID="+conn.GetFieldValue(i,2); }
			
				if(conn.GetFieldValue(i,2) == "01")
					link_proj = "SME - "; 
				else if(conn.GetFieldValue(i,2) == "20")
					link_proj = "CC - "; 
				else if(conn.GetFieldValue(i,2) == "40")
					link_proj = "CONSUMER - "; 
					*/
				HyperLink link = new HyperLink();
				//link.Text = link_proj + conn.GetFieldValue(i,0);
				link.Text = conn.GetFieldValue(i,0);
				link.NavigateUrl = navURL;
					
				Table2.Rows[counter].Cells.Add(new TableCell());
				if (i % 2 == 1)
					Table2.Rows[counter].Cells[column].CssClass = "TblAlternating";
				Table2.Rows[counter].Cells[column].Controls.Add(link);

				counter++;
			}
			conn.ClearData();
			if (!IsPostBack)
			{
				SetModuleName();
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

		private void SetModuleName()
		{
			connMnt.QueryString = "select MODULENAME from RFMODULE where MODULEID = '" + Request.QueryString["ModuleID"]+ "'";
			connMnt.ExecuteQuery();
			this.LBL_MODULENAME.Text = connMnt.GetFieldValue("MODULENAME");
		}
	}
}
