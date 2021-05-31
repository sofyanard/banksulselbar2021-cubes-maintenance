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
	/// Summary description for Jenis Pengikatan
	/// </summary>
	public partial class RFIkatParam : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.WebControls.Button updatestatus;
		protected Tools tool = new Tools();
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

			//if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//	Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack) 
			{
				LBL_SAVEMODE.Text = "1";
				LBL_ACTIVE.Text = Request.QueryString["active"];
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

				LBL_ID.Text = "IKATID";
				LBL_DESC.Text = "IKATDESC";

				viewExistingData();
				viewPendingData();
				setDescription();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			DGExisting.PageIndexChanged +=new DataGridPageChangedEventHandler(DGExisting_PageIndexChanged);
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
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion

		private void setDescription() 
		{
			switch (Request.QueryString["tablename"]) 
			{
				case "RFTBODOC":
					TXT_DESC.TextMode = TextBoxMode.MultiLine;
					break;				
				default:
					TXT_DESC.TextMode = TextBoxMode.SingleLine;
					break;
			}
		}

		private void viewPendingData() 
		{
			string pendCol = "";

			conn.QueryString = "select * from PENDING_RFIKAT";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("DEFAULT"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			if (LBL_ACTIVE.Text.Trim() == "1")
				pendCol = "PENDINGSTATUS";
			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i, "IKATID");
				dr[1] = conn.GetFieldValue(i, "IKATDESC");
				dr[2] = (conn.GetFieldValue(i, "DEFAULT")=="1")?"Yes":"No";
				dr[3] = conn.GetFieldValue(i,pendCol);
				dr[4] = getPendingStatus(conn.GetFieldValue(i,pendCol));
				dt.Rows.Add(dr);
			}			

			DGRequest.DataSource = new DataView(dt);
			try 
			{
				DGRequest.DataBind();
			}
			catch {
				DGRequest.CurrentPageIndex = DGRequest.PageCount - 1;
				DGRequest.DataBind();
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

		private void cekAndSetDefaultIkat(string IKATID, string DEFAULT) 
		{
			if (LBL_DEFAULT_IKAT.Text == "") 
			{
				if (DEFAULT == "1") 
				{
					LBL_DEFAULT_IKAT.Text = IKATID;				
					//DDL_DEFAULT.Enabled = false;
					//DDL_DEFAULT.SelectedValue = "0";
				}
			}
		}

		private void viewExistingData() 
		{
			if (LBL_ACTIVE.Text.Trim() == "1")
				conn.QueryString = "select * from RFIKAT where ACTIVE = '1'";
			else
				conn.QueryString = "select * from RFIKAT";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("DEFAULT"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i, "IKATID");
				dr[1] = conn.GetFieldValue(i, "IKATDESC");
				dr[2] = (conn.GetFieldValue(i, "DEFAULT")=="1")?"Yes":"No";
				dt.Rows.Add(dr);

				cekAndSetDefaultIkat(conn.GetFieldValue(i, "IKATID"), conn.GetFieldValue(i, "DEFAULT"));
			}			

			DGExisting.DataSource = new DataView(dt);
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

		private void DGExisting_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{			
			DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		} 

		/*
		private void cekDefaultIkat(string IKATID, string DEFAULT, string SAVEMODE) 
		{
			if (SAVEMODE == "0") //---  UPDATE
			{
				if (DEFAULT == "1") 
				{
					if (LBL_DEFAULT_IKAT.Text != "" && LBL_DEFAULT_IKAT.Text != IKATID) 
					{
						Tools.popMessage(this, "Pengikatan Default already exist! Request canceled!");
						return;
					}
					else 
					{
						LBL_DEFAULT_IKAT.Text = IKATID;
					}
				}
				else 
				{
					LBL_DEFAULT_IKAT.Text = IKATID;
				}			
			}
			else if (SAVEMODE == "1") //--- INSERT
			{
				if (DEFAULT == "1") 
				{
					if (LBL_DEFAULT_IKAT.Text != "") 
					{
						Tools.popMessage(this, "Pengikatan Default already exist! Request canceled!");
						return;
					}
				}
				else 
				{
					LBL_DEFAULT_IKAT.Text = IKATID;
				}
			}
			else if (SAVEMODE == "2") //--- DELETE
			{
				if (DEFAULT == "1")
					LBL_DEFAULT_IKAT.Text = "";				
			}
		}
		*/

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (TXT_ID.Text.Trim() == "" || TXT_DESC.Text.Trim() == "") return;

			if (LBL_SAVEMODE.Text.Trim() == "1") //--- Status INSERT
			{
				conn.QueryString = "select * from RFIKAT WHERE IKATID ='" + TXT_ID.Text.Trim() + "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}

			}	
	
			executeMaker(TXT_ID.Text.Trim(), TXT_DESC.Text.Trim(), DDL_DEFAULT.SelectedValue, LBL_SAVEMODE.Text.Trim());
			viewPendingData();
			clearControls();

			LBL_SAVEMODE.Text = "1";
		}

		private void clearControls() 
		{
			TXT_ID.Text   = "";
			TXT_DESC.Text = "";
			activateControlKey(false);
		}

		private void activateControlKey(bool isReadOnly) 
		{
			TXT_ID.ReadOnly = isReadOnly;
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					//LBL_SAVEMODE.Text = e.Item.Cells[2].Text;
					LBL_SAVEMODE.Text = e.Item.Cells[3].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[1].Text;
					DDL_DEFAULT.SelectedValue = e.Item.Cells[2].Text;
					activateControlKey(true);
					break;

				case "delete":
					string id = e.Item.Cells[0].Text;
					conn.QueryString = "delete from PENDING_RFIKAT WHERE IKATID='" + id + "'";
					conn.ExecuteQuery();
					viewPendingData();
					break;

				default :
					break;
			}
		}

		private string getColumnKey() 
		{
			/*string colKey = "";
			//---  mendapatkan field table yang PK -----------------------
			conn.QueryString = "select * from SYSCOLUMNS " + 
				"where ID in " + 
				"(select ID from SYSOBJECTS " + 
				"where NAME = '" + Request.QueryString["tablename"] + "')";
			conn.ExecuteQuery();
			colKey = conn.GetFieldValue(0, 0);*/

			return LBL_ID.Text.Trim();
			//-------------------------------------------------------------
		}

		/*private string[] getColumnNonKey()
		{
			string[] col = new string[2];
			//---  mendapatkan field table yang PK -----------------------
			conn.QueryString = "select * from SYSCOLUMNS " + 
				"where ID in " + 
				"(select ID from SYSOBJECTS " + 
				"where NAME = '" + Request.QueryString["tablename"] + "')";
			conn.ExecuteQuery();

			col[0] = conn.GetFieldValue(1,0);
			//col[1] = conn.GetFieldValue(2,0);

			return col;
			//-------------------------------------------------------------			
		}*/

        private string getColumnDesc()
		{
			return LBL_DESC.Text.Trim();
		}

		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[1].Text;
					DDL_DEFAULT.SelectedValue = (e.Item.Cells[2].Text.ToUpper()=="YES")?"1":"0";
					activateControlKey(true);
					break;

				case "delete":					
					string id = e.Item.Cells[0].Text.Trim();
					string defaultVal = (e.Item.Cells[2].Text.ToUpper()=="YES")?"1":"0";
					executeMaker(id, e.Item.Cells[1].Text, defaultVal, "2");
					viewPendingData();
					break;

				default :
					break;
			}
		}

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		private void executeMaker(string id, string desc, string defaultval, string pendingStatus) 
		{
			conn.QueryString = "exec PARAM_GENERAL_RFIKAT_MAKER '" + pendingStatus + 
								"','" + id + 
								"','" + desc + 
								"','" + defaultval + "'";
			try 
			{
				conn.ExecuteNonQuery();
			} 
			catch {
				Tools.popMessage(this, "Input tidak valid !");
				return;
			}

			viewPendingData();
			clearControls();

			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../HostParam.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{			
			clearControls();
		}
	}		
}
