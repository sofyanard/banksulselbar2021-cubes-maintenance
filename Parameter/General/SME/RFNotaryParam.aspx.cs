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
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for GeneralInfo.
	/// </summary>
	public partial class RFNotaryParam : System.Web.UI.Page
	{
		protected Connection conn;
		protected Tools tool = new Tools();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

		//	if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//	Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack)
			{
				viewExisting();
				viewRequest();
			}

			BTN_SAVE.Attributes.Add("onclick", "if(!cek_mandatory(document.Form1)) { return false; };");
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
			this.DGR_NOTARY_EXIST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_NOTARY_EXIST_ItemCommand);
			this.DGR_NOTARY_EXIST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_NOTARY_EXIST_PageIndexChanged);
			this.DGR_NOTARY_REQ.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_NOTARY_REQ_ItemCommand);
			this.DGR_NOTARY_REQ.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_NOTARY_REQ_PageIndexChanged);

		}
		#endregion

		private void viewExisting() 
		{
            conn.QueryString = "select * from RFNOTARY where ACTIVE = '1' order by NTID";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("NTID"));
			dt.Columns.Add(new DataColumn("NT_NAME"));
			dt.Columns.Add(new DataColumn("NT_ADDR"));
			dt.Columns.Add(new DataColumn("NT_CITY"));
			dt.Columns.Add(new DataColumn("NT_ZIPCODE"));
			dt.Columns.Add(new DataColumn("NT_PHN"));
			dt.Columns.Add(new DataColumn("NT_FAX"));
			dt.Columns.Add(new DataColumn("NT_EMAIL"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,2) + " " + conn.GetFieldValue(i,3) + " " + conn.GetFieldValue(i,4);
				dr[3] = conn.GetFieldValue(i,5);
				dr[4] = conn.GetFieldValue(i,13);
				dr[5] = conn.GetFieldValue(i,7) + " " + conn.GetFieldValue(i,8) + " " + conn.GetFieldValue(i,9);
				dr[6] = conn.GetFieldValue(i,10) + " " + conn.GetFieldValue(i,11) + " " + conn.GetFieldValue(i,12);
				dr[7] = conn.GetFieldValue(i,6);
				dt.Rows.Add(dr);
			}			

			DGR_NOTARY_EXIST.DataSource = new DataView(dt);
			try 
			{
				DGR_NOTARY_EXIST.DataBind();
			} 
			catch 
			{
				DGR_NOTARY_EXIST.CurrentPageIndex = DGR_NOTARY_EXIST.PageCount - 1;
				DGR_NOTARY_EXIST.DataBind();
			}
		}

		private string getPendingStatus(string saveMode) 
		{
			string status = "";			
			switch (saveMode)
			{
				case "0":
					status = "Update";
					break;
				case "1":
					status = "Insert";
					break;
				case "2":
					status = "Delete";
					break;
				default:
					status = "";
					break;
			}
			return status;
		}

		private void viewRequest() 
		{
            conn.QueryString = "select * from PENDING_RFNOTARY where ACTIVE = '1' order by NTID";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("NTID"));
			dt.Columns.Add(new DataColumn("NT_NAME"));
			dt.Columns.Add(new DataColumn("NT_ADDR"));
			dt.Columns.Add(new DataColumn("NT_CITY"));
			dt.Columns.Add(new DataColumn("NT_ZIPCODE"));
			dt.Columns.Add(new DataColumn("NT_PHN"));
			dt.Columns.Add(new DataColumn("NT_FAX"));
			dt.Columns.Add(new DataColumn("NT_EMAIL"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,2) + " " + conn.GetFieldValue(i,3) + " " + conn.GetFieldValue(i,4);
				dr[3] = conn.GetFieldValue(i,5);
				dr[4] = conn.GetFieldValue(i,13);
				dr[5] = conn.GetFieldValue(i,7) + " " + conn.GetFieldValue(i,8) + " " + conn.GetFieldValue(i,9);
				dr[6] = conn.GetFieldValue(i,10) + " " + conn.GetFieldValue(i,11) + " " + conn.GetFieldValue(i,12);
				dr[7] = conn.GetFieldValue(i,6);
				dr[8] = conn.GetFieldValue(i,15);
				dr[9] = getPendingStatus(conn.GetFieldValue(i,15).ToString());				
				dt.Rows.Add(dr);
			}			

			DGR_NOTARY_REQ.DataSource = new DataView(dt);
			try 
			{
				DGR_NOTARY_REQ.DataBind();
			} 
			catch 
			{
				DGR_NOTARY_REQ.CurrentPageIndex = DGR_NOTARY_REQ.PageCount - 1;
				DGR_NOTARY_REQ.DataBind();
			}
		}

		private void DGR_NOTARY_EXIST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_NOTARY_EXIST.CurrentPageIndex = e.NewPageIndex;
			viewExisting();
		}

		private void DGR_NOTARY_REQ_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_NOTARY_REQ.CurrentPageIndex = e.NewPageIndex;
			viewRequest();
		}

		protected void BTN_SEARCHCOMP_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>window.open('SearchZipcode.aspx?targetFormID=Form1&targetObjectID=TXT_NT_ZIPCODE','SearchZipcode','status=no,scrollbars=no,width=420,height=200');</script>");
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
                conn.QueryString = "select * from RFNOTARY where NTID='" + TXT_NT_ID.Text.Trim() + "' order by NTID";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}				

			conn.QueryString = "exec PARAM_GENERAL_RFNOTARY_MAKER '" + LBL_SAVEMODE.Text + 
									"', '" + TXT_NT_ID.Text + 
									"', '" + TXT_NT_NAME.Text + 
									"', '" + TXT_NT_ADDR1.Text + 
									"', '" + TXT_NT_ADDR2.Text + 
									"', '" + TXT_NT_ADDR3.Text + 
									"', '" + TXT_NT_CITY.Text + 
									"', '" + TXT_NT_EMAIL.Text + 
									"', '" + TXT_NT_PHNAREA.Text + 
									"', '" + TXT_NT_PHNNUM.Text + 
									"', '" + TXT_NT_PHNEXT.Text + 
									"', '" + TXT_NT_FAXAREA.Text + 
									"', '" + TXT_NT_FAXNUM.Text + 
									"', '" + TXT_NT_FAXEXT.Text+ 
									"', '" + TXT_NT_ZIPCODE.Text + "'";
			conn.ExecuteNonQuery();

			viewRequest();
			clearControls();

			LBL_SAVEMODE.Text = "1";
		}

		private void clearControls() 
		{
//			LBL_SAVEMODE.Text = "1";

			TXT_NT_ID.Text		= "";
			TXT_NT_NAME.Text	= "";
			TXT_NT_ADDR1.Text	= "";
			TXT_NT_ADDR2.Text	= "";
			TXT_NT_ADDR3.Text	= "";
			TXT_NT_CITY.Text	= "";
			TXT_NT_ZIPCODE.Text = "";
			TXT_NT_PHNAREA.Text = "";
			TXT_NT_PHNNUM.Text	= "";
			TXT_NT_PHNEXT.Text	= "";
			TXT_NT_FAXAREA.Text = "";
			TXT_NT_FAXNUM.Text	= "";
			TXT_NT_FAXEXT.Text	= "";
			TXT_NT_EMAIL.Text	= "";
			activateControlKey(false);
		}

		protected void TXT_NT_ZIPCODE_TextChanged(object sender, System.EventArgs e)
		{
			conn.QueryString = "select cityid, cityname, description from vw_zipcodecity where rtrim(ltrim(zipcode)) = '" + 
				TXT_NT_ZIPCODE.Text.Trim() + "' ";
			conn.ExecuteQuery();
			try
			{
				LBL_NT_CITY.Text = conn.GetFieldValue(0,0);
				TXT_NT_CITY.Text = conn.GetFieldValue(0,2);
			}
			catch
			{
				TXT_NT_ZIPCODE.Text = "";
				TXT_NT_CITY.Text = "";
				Response.Write("<script language='javascript'>alert('Invalid Zipcode!');</script>");
			}
		}

		private void activateControlKey(bool isReadOnly) 
		{
			TXT_NT_ID.ReadOnly = isReadOnly;
		}

		private void DGR_NOTARY_REQ_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[8].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_NT_ID.Text		= e.Item.Cells[0].Text;
                    conn.QueryString = "select * from PENDING_RFNOTARY where ACTIVE = '1' and NTID = '" + TXT_NT_ID.Text + "' order by NTID";
					conn.ExecuteQuery();
					TXT_NT_NAME.Text	= conn.GetFieldValue("NT_NAME");
					TXT_NT_ADDR1.Text	= conn.GetFieldValue("NT_ADDR1");
					TXT_NT_ADDR2.Text	= conn.GetFieldValue("NT_ADDR2");
					TXT_NT_ADDR3.Text	= conn.GetFieldValue("NT_ADDR3");
					TXT_NT_CITY.Text	= conn.GetFieldValue("NT_CITY");
					TXT_NT_ZIPCODE.Text = conn.GetFieldValue("NT_ZIPCODE");
					TXT_NT_PHNAREA.Text = conn.GetFieldValue("NT_PHNAREA");
					TXT_NT_PHNNUM.Text	= conn.GetFieldValue("NT_PHNNUM");
					TXT_NT_PHNEXT.Text	= conn.GetFieldValue("NT_PHNEXT");
					TXT_NT_FAXAREA.Text = conn.GetFieldValue("NT_FAXAREA");
					TXT_NT_FAXNUM.Text	= conn.GetFieldValue("NT_FAXNUM");
					TXT_NT_FAXEXT.Text	= conn.GetFieldValue("NT_FAXEXT");
					TXT_NT_EMAIL.Text	= conn.GetFieldValue("NT_EMAIL");
					
					activateControlKey(true);
					break;

				case "delete":
					string id = e.Item.Cells[0].Text;					
				
					conn.QueryString = "delete from PENDING_RFNOTARY WHERE ACTIVE = '1' and NTID = '" + id + "'";
					conn.ExecuteQuery();
					viewRequest();
					break;
				default :
					break;
			}
		}

		private void DGR_NOTARY_EXIST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":					
					LBL_SAVEMODE.Text = "0";
					TXT_NT_ID.Text = e.Item.Cells[0].Text;
                    conn.QueryString = "select * from RFNOTARY where NTID = '" + TXT_NT_ID.Text + "' order by NTID";
					conn.ExecuteQuery();
					TXT_NT_NAME.Text	= conn.GetFieldValue("NT_NAME");
					TXT_NT_ADDR1.Text	= conn.GetFieldValue("NT_ADDR1");
					TXT_NT_ADDR2.Text	= conn.GetFieldValue("NT_ADDR2");
					TXT_NT_ADDR3.Text	= conn.GetFieldValue("NT_ADDR3");
					TXT_NT_CITY.Text	= conn.GetFieldValue("NT_CITY");
					TXT_NT_ZIPCODE.Text = conn.GetFieldValue("NT_ZIPCODE");
					TXT_NT_PHNAREA.Text = conn.GetFieldValue("NT_PHNAREA");
					TXT_NT_PHNNUM.Text	= conn.GetFieldValue("NT_PHNNUM");
					TXT_NT_PHNEXT.Text	= conn.GetFieldValue("NT_PHNEXT");
					TXT_NT_FAXAREA.Text = conn.GetFieldValue("NT_FAXAREA");
					TXT_NT_FAXNUM.Text	= conn.GetFieldValue("NT_FAXNUM");
					TXT_NT_FAXEXT.Text	= conn.GetFieldValue("NT_FAXEXT");
					TXT_NT_EMAIL.Text	= conn.GetFieldValue("NT_EMAIL");
					activateControlKey(true);
					break;

				case "delete":					
					string ID = e.Item.Cells[0].Text.Trim();
                    conn.QueryString = "select * from RFNOTARY where NTID = '" + ID + "' order by NTID";
					conn.ExecuteQuery();
					string NAME		= conn.GetFieldValue("NT_NAME");
					string ADDR1	= conn.GetFieldValue("NT_ADDR1");
					string ADDR2	= conn.GetFieldValue("NT_ADDR2");
					string ADDR3	= conn.GetFieldValue("NT_ADDR3");
					string CITY		= conn.GetFieldValue("NT_CITY");
					string ZIPCODE	= conn.GetFieldValue("NT_ZIPCODE");
					string PHNAREA	= conn.GetFieldValue("NT_PHNAREA");
					string PHNNUM	= conn.GetFieldValue("NT_PHNNUM");
					string PHNEXT	= conn.GetFieldValue("NT_PHNEXT");
					string FAXAREA	= conn.GetFieldValue("NT_FAXAREA");
					string FAXNUM	= conn.GetFieldValue("NT_FAXNUM");
					string FAXEXT	= conn.GetFieldValue("NT_FAXEXT");
					string EMAIL	= conn.GetFieldValue("NT_EMAIL");

					conn.QueryString = "exec PARAM_GENERAL_RFNOTARY_MAKER '2', '" + ID + 
						"', '" + NAME + 
						"', '" + ADDR1 + 
						"', '" + ADDR2 + 
						"', '" + ADDR3 + 
						"', '" + CITY + 
						"', '" + EMAIL + 
						"', '" + PHNAREA + 
						"', '" + PHNNUM + 
						"', '" + PHNEXT + 
						"', '" + FAXAREA + 
						"', '" + FAXNUM + 
						"', '" + FAXEXT+ 
						"', '" + ZIPCODE + "'";
					conn.ExecuteQuery();
					viewRequest();
					break;

				default :
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../HostParam.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}
	}
}
