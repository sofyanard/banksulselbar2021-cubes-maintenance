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
	/// Summary description for MappingConsCCAppr.
	/// </summary>
	public partial class MappingConsCCAppr : System.Web.UI.Page
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

				viewRequestData();
			}
			else
			{
				cclink = (string)ViewState["cclink"];
			}

			LBL_PARAMNAME.Text = paramname;

			
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
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion

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

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbB.Checked = false;
							rbC.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGRequest.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject");
					if (rbA.Checked)
					{
						performRequest(i);
					}
					else if (rbR.Checked)
					{
						deleteData(i);
					}
				} 
				catch (Exception ex )
				{
					Response.Write("<!-- " + ex.Message + " -->");
				}
			}
			viewRequestData();
		}

		private void performRequest(int row)
		{
			try 
			{
				conn2.QueryString = "SELECT * from TMAPPINGCONSCC where constable = '" + constablename + "' " +
					" and cctable = '" + cctablename + "' " +
					" and considvalue = '" + DGRequest.Items[row].Cells[0].Text.Trim() + "'";
				conn2.ExecuteQuery();

				string iconstable		= conn2.GetFieldValue("CONSTABLE");
				string iconsfldid		= conn2.GetFieldValue("CONSFLDID");
				string iconsfldccmap	= conn2.GetFieldValue("CONSFLDCCMAP");
				string iconsidvalue		= conn2.GetFieldValue("CONSIDVALUE");
				string iconsccmapvalue	= conn2.GetFieldValue("CONSCCMAPVALUE");

				conn2.QueryString = "EXEC PARAM_GENERAL_MAPPINGCONSCC_APPR 'UPD', '" +
					iconstable + "', '" + iconsfldid + "', '" + iconsfldccmap + "', '" +
					iconsidvalue + "', '" + iconsccmapvalue + "'";
				conn2.ExecuteNonQuery();

				conn2.QueryString = "EXEC PARAM_GENERAL_MAPPINGCONSCC_APPR 'DEL', '" +
					iconstable + "', '" + iconsfldid + "', '" + iconsfldccmap + "', '" +
					iconsidvalue + "', '" + iconsccmapvalue + "'";
				conn2.ExecuteNonQuery();
			} 
			catch (Exception p)
			{
				GlobalTools.popMessage(this,p.Message);
			}
		}

		private void deleteData(int row)
		{
			try 
			{
				conn2.QueryString = "SELECT * from TMAPPINGCONSCC where constable = '" + constablename + "' " +
					" and cctable = '" + cctablename + "' " +
					" and considvalue = '" + DGRequest.Items[row].Cells[0].Text.Trim() + "'";
				conn2.ExecuteQuery();

				string iconstable		= conn2.GetFieldValue("CONSTABLE");
				string iconsfldid		= conn2.GetFieldValue("CONSFLDID");
				string iconsfldccmap	= conn2.GetFieldValue("CONSFLDCCMAP");
				string iconsidvalue		= conn2.GetFieldValue("CONSIDVALUE");
				string iconsccmapvalue	= conn2.GetFieldValue("CONSCCMAPVALUE");

				conn2.QueryString = "EXEC PARAM_GENERAL_MAPPINGCONSCC_APPR 'DEL', '" +
					iconstable + "', '" + iconsfldid + "', '" + iconsfldccmap + "', '" +
					iconsidvalue + "', '" + iconsccmapvalue + "'";
				conn2.ExecuteNonQuery();
			} 
			catch {}
		}

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewRequestData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../CreditCardParamApproval.aspx?mc=9902040202&ModuleId=40");
		}

	}
}
