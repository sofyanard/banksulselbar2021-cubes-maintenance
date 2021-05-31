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
	/// Summary description for Sandi BI
	/// </summary>
	public partial class RFBICodeParam : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.WebControls.Button updatestatus;
		protected Tools tool = new Tools();
		protected Connection conn;
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV;uid=sa;pwd=");
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

			//--- untuk mencegah oknum user, akses melalui url
			//if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
				//Response.Redirect("/SME/Restricted.aspx");
			//----------------------------------------------------------

			if (!IsPostBack) 
			{
				fillDDLBG_Group();
				LBL_SAVEMODE.Text = "1";
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			DGDocRequest.PageIndexChanged += new DataGridPageChangedEventHandler(DGDocRequest_PageIndexChanged);
			DGDocuments.PageIndexChanged +=new DataGridPageChangedEventHandler(DGDocuments_PageIndexChanged);
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
			this.DGDocuments.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGDocuments_ItemCommand);
			this.DGDocRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGDocRequest_ItemCommand);
			this.DGDocRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGDocRequest_PageIndexChanged);

		}
		#endregion

		private void fillDDLBG_Group() 
		{
			DDL_BG_GROUP.Items.Add(new ListItem("- PILIH -",""));

			conn.QueryString = "select * from RFBICODEGROUP order by BG_DESC";
			conn.ExecuteQuery();
			for(int i=0; i<conn.GetRowCount(); i++) 
			{
				DDL_BG_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i,"BG_DESC"), conn.GetFieldValue(i, "BG_ID")));
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

		private void viewPending(string BG_GROUP)
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_PENDING_RFBICODE where BG_GROUP = '"+BG_GROUP+"'";
			try 
			{
				conn.ExecuteQuery();
			} 
			catch {
				Tools.popMessage(this, "Error View !");
				return;
			}

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("BG_GROUP"));
			dt.Columns.Add(new DataColumn("BG_DESC"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i, "BI_SEQ");
				dr[1] = conn.GetFieldValue(i, "BI_DESC");
				dr[2] = conn.GetFieldValue(i, "BG_GROUP");
				dr[3] = conn.GetFieldValue(i, "BG_DESC");
				dr[4] = conn.GetFieldValue(i, "PENDINGSTATUS");				
				dr[5] = getPendingStatus(conn.GetFieldValue(i, "PENDINGSTATUS"));				
				dt.Rows.Add(dr);
			}			

			DGDocRequest.DataSource = new DataView(dt);
			try 
			{
				DGDocRequest.DataBind();
			}
			catch 
			{
				DGDocRequest.CurrentPageIndex = DGDocRequest.PageCount - 1;
				DGDocRequest.DataBind();
			}
		}

		private void viewExisting(string BG_GROUP)
		{
			conn.QueryString = "select * from VW_PARAM_GENERAL_RFBICODE where BG_GROUP = '"+BG_GROUP+"'";
			try 
			{
				conn.ExecuteQuery();			
			} 
			catch {
				Tools.popMessage(this, "Error Database !");
				return;
			}

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("BG_GROUP"));
			dt.Columns.Add(new DataColumn("BG_DESC"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i, "BI_SEQ");
				dr[1] = conn.GetFieldValue(i, "BI_DESC");
				dr[2] = conn.GetFieldValue(i, "BG_GROUP");
				dr[3] = conn.GetFieldValue(i, "BG_DESC");
				dt.Rows.Add(dr);
			}			

			DGDocuments.DataSource = new DataView(dt);
			try 
			{
				DGDocuments.DataBind();
			}
			catch  
			{
				DGDocuments.CurrentPageIndex = DGDocuments.PageCount - 1;
				DGDocuments.DataBind();
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "exec PARAM_GENERAL_RFBICODE_MAKER '"+
								LBL_SAVEMODE.Text+"','"+
								LBL_BI_SEQ.Text + "','" +
								TXT_BI_DESC.Text+"','"+
								DDL_BG_GROUP.SelectedValue+"'";
			try 
			{
				conn.ExecuteNonQuery();
			} 
			catch {
				//Tools.popMessage(this, "Error Stored Procedure !");
				//return;
			}
			
			viewPending(DDL_BG_GROUP.SelectedValue);

			clearControls();			
			LBL_SAVEMODE.Text = "1";
		}

		private void DGDocuments_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DGDocuments.CurrentPageIndex = e.NewPageIndex;
			viewExisting(DDL_BG_GROUP.SelectedValue);
		}

		private void DGDocuments_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":					
					LBL_SAVEMODE.Text = "0";
					LBL_BI_SEQ.Text = e.Item.Cells[0].Text;
					TXT_BI_DESC.Text = e.Item.Cells[1].Text;
					DDL_BG_GROUP.SelectedValue = e.Item.Cells[2].Text;
					activateControlKey(false);
					break;

				case "delete":					
					conn.QueryString = "exec PARAM_GENERAL_RFBICODE_MAKER '2','"+
										e.Item.Cells[0].Text+"','"+
										e.Item.Cells[1].Text+"','"+
										e.Item.Cells[2].Text+"'";
					try 
					{
						conn.ExecuteQuery();
					} 
					catch {
						Tools.popMessage(this, "Error Stored-Procedure !");
						return;
					}
					viewPending(DDL_BG_GROUP.SelectedValue);
					break;

				default:
					break;
			}
		}

		private void DGDocRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGDocRequest.CurrentPageIndex = e.NewPageIndex;
			viewPending(DDL_BG_GROUP.SelectedValue);
		}

		private void DGDocRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
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
					LBL_BI_SEQ.Text = e.Item.Cells[0].Text;
					TXT_BI_DESC.Text = e.Item.Cells[1].Text;
					DDL_BG_GROUP.SelectedValue = e.Item.Cells[2].Text;					
					activateControlKey(false);
					break;

				case "delete":
					string BI_SEQ = e.Item.Cells[0].Text;
					string BG_GROUP = e.Item.Cells[2].Text;

					conn.QueryString = "delete from PENDING_RFBICODE " + 
										"where BI_SEQ = '"+BI_SEQ+"' and BG_GROUP = '"+BG_GROUP+"'";	
					try 
					{
						conn.ExecuteQuery();
					} 
					catch {
						Tools.popMessage(this, "Error Stored-Procedure !");
						return;
					}
					viewPending(DDL_BG_GROUP.SelectedValue);
					break;

				default:
					break;
			}		
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../HostParam.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		protected void DDL_BG_GROUP_SelectedIndexChanged(object sender, System.EventArgs e)
		{			
			viewExisting(DDL_BG_GROUP.SelectedValue);
			viewPending(DDL_BG_GROUP.SelectedValue);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			LBL_SAVEMODE.Text = "1";
			clearControls();
		}

		private void activateControlKey(bool isEnabled) 
		{
			DDL_BG_GROUP.Enabled = isEnabled;
		}

		private void clearControls() 
		{
			LBL_BI_SEQ.Text = "";
			//DDL_BG_GROUP.SelectedValue = "";
			TXT_BI_DESC.Text = "";
			activateControlKey(true);
		}
	}		
}
