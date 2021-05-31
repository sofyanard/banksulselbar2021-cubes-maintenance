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
	/// Summary description for Area
	/// 
	/// TODO : Validasi untuk SEMUA screen parameters !!!
	/// 
	/// </summary>
	public partial class RFAreaParam : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.WebControls.Button updatestatus;
		protected Tools tool = new Tools();
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);//(Connection) Session["Connection"];
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			//if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//	Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack) 
			{
				LBL_SAVEMODE.Text = "1";
				LBL_ACTIVE.Text = Request.QueryString["active"];
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

				/***
				LBL_ID.Text = "CERTTYPEID";
				LBL_DESC.Text = "CERTTYPEDESC";
				***/

				viewExistingData();
				viewPendingData();
				setDescription();
				fillDDLType();
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

		private void fillDDLType() 
		{
			conn.QueryString = "select * from VW_SCUSER_IDONLY";
			conn.ExecuteQuery();

			DDL_COLTYPEID.Items.Add(new ListItem("- PILIH -",""));
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_COLTYPEID.Items.Add(new ListItem(conn.GetFieldValue(i, "SU_FULLNAME"), conn.GetFieldValue(i, "USERID")));
			}
		}

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

			conn.QueryString = "select * from VW_PARAM_GENERAL_PENDING_RFAREA";
			try 
			{
				conn.ExecuteQuery();
			} 
			catch (ApplicationException) 
			{
				Tools.popMessage(this, "Application Error !");
				return;
			}
			catch 
			{
				Tools.popMessage(this, "View [VW_PARAM_GENERAL_PENDING_RFAREA] Error !");
				return;
			}

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("AREAREGMANAGER"));
			dt.Columns.Add(new DataColumn("SU_FULLNAME"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			if (LBL_ACTIVE.Text.Trim() == "1")
				pendCol = "PENDINGSTATUS";
			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i, "AREAID");
				dr[1] = conn.GetFieldValue(i, "AREANAME");
				dr[2] = conn.GetFieldValue(i, "AREAREGMANAGER");
				dr[3] = conn.GetFieldValue(i, "SU_FULLNAME");
				dr[4] = conn.GetFieldValue(i,pendCol);
				dr[5] = getPendingStatus(conn.GetFieldValue(i,pendCol));
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

		private void viewExistingData() 
		{
			/// data ditampilin semua
			/// 
//			if (LBL_ACTIVE.Text.Trim() == "1")
//				conn.QueryString = "select * from VW_PARAM_GENERAL_RFAREA where ACTIVE = '1'";
//			else
				conn.QueryString = "select * from VW_PARAM_GENERAL_RFAREA";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("AREAREGMANAGER"));
			dt.Columns.Add(new DataColumn("SU_FULLNAME"));
			dt.Columns.Add(new DataColumn("ACTIVE"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i, "AREAID");
				dr[1] = conn.GetFieldValue(i, "AREANAME");
				dr[2] = conn.GetFieldValue(i, "AREAREGMANAGER");
				dr[3] = conn.GetFieldValue(i, "SU_FULLNAME");
				dr[4] = conn.GetFieldValue(i, "ACTIVE");
				dt.Rows.Add(dr);
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

			for (int i=0; i < DGExisting.Items.Count; i++)
			{
				if (DGExisting.Items[i].Cells[4].Text.Trim() =="0" )
				{

					LinkButton l_del = (LinkButton) DGExisting.Items[i].FindControl("lnk_RfDelete");
					l_del.CommandName = "Undelete";
					l_del.Text = "Undelete";

					LinkButton l_edit = (LinkButton) DGExisting.Items[i].FindControl("lnk_RfEdit");
					l_edit.Visible = false;
				}
			}
		}

		private void DGExisting_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{			
			DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		} 

		

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string active ="0";
			if (TXT_ID.Text.Trim() == "" || TXT_DESC.Text.Trim() == "") return;

			if (LBL_SAVEMODE.Text.Trim() == "1") //--- Status INSERT
			{
				conn.QueryString = "select active from RFAREA WHERE AREAID ='" + TXT_ID.Text.Trim() + "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					active = conn.GetFieldValue("active");
					if (active == "1")
					{
						Tools.popMessage(this, "ID has already been used! Request canceled!");
						return;
					}
					else
					{
						LBL_SAVEMODE.Text ="0";
					}
				}

			}	
	
			executeMaker(TXT_ID.Text.Trim(), TXT_DESC.Text.Trim(), DDL_COLTYPEID.SelectedValue, LBL_SAVEMODE.Text.Trim());
			viewPendingData();
			clearControls();

			LBL_SAVEMODE.Text = "1";
		}

		private void clearControls() 
		{
			TXT_ID.Text   = "";
			TXT_DESC.Text = "";
			DDL_COLTYPEID.SelectedValue = "";
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
					LBL_SAVEMODE.Text = e.Item.Cells[4].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[1].Text;
					DDL_COLTYPEID.SelectedValue = e.Item.Cells[2].Text;
					activateControlKey(true);
					break;

				case "delete":
					string id = e.Item.Cells[0].Text;
					conn.QueryString = "delete from PENDING_RFAREA WHERE AREAID ='" + id + "'";
					conn.ExecuteQuery();
					viewPendingData();
					break;

				default :
					break;
			}
		}

        private string getColumnDesc()
		{
			return LBL_DESC.Text.Trim();
		}

		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string id,coltypeid;

			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[1].Text;
					try {DDL_COLTYPEID.SelectedValue = e.Item.Cells[2].Text;} 
					catch {}
					activateControlKey(true);
					break;

				case "delete":					
					 id = e.Item.Cells[0].Text.Trim();
					 coltypeid = e.Item.Cells[2].Text;
					executeMaker(id, e.Item.Cells[1].Text, coltypeid, "2");
					viewPendingData();
					break;

				case "undelete":					
					 id = e.Item.Cells[0].Text.Trim();
					 coltypeid = e.Item.Cells[2].Text;
					executeMaker(id, e.Item.Cells[1].Text, coltypeid, "0");
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

		private void executeMaker(string id, string desc, string typeid, string pendingStatus) 
		{
			conn.QueryString = "exec PARAM_GENERAL_RFAREA_MAKER '" + pendingStatus + 
								"','" + id + 
								"','" + desc + 
								"','" + typeid + "'";
			try 
			{
				conn.ExecuteNonQuery();
			} 
			catch (ApplicationException) 
			{
				Tools.popMessage(this, "Application Error !");
				return;
			}
			catch 
			{
				Tools.popMessage(this, "Input tidak valid !");
				return;
			}

			viewPendingData();
			clearControls();

			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();
		}
	}		
}
