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
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General
{
	/// <summary>
	/// Summary description for RFTemplateParam.
	/// </summary>
	public partial class RFLiquidationRate : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.WebControls.Button updatestatus;
		protected Tools tool = new Tools();
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
			
		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			if (!IsPostBack) 
			{
		
				LBL_PARAMNAME.Text = Request.QueryString["name"];
				LBL_SAVEMODE.Text = "1";
				LBL_ACTIVE.Text = Request.QueryString["active"];
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition //tes

				//set fields
				conn.QueryString = "select * from SYSCOLUMNS " + 
					"where ID in " + 
					"(select ID from SYSOBJECTS " + 
					"where NAME = '" + Request.QueryString["tablename"] + "')";
				conn.ExecuteQuery();

				try
				{
					LBL_ID.Text = conn.GetFieldValue(0,0);
					LBL_DESC.Text = conn.GetFieldValue(1,0);
				} 
				catch {}

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
			string tableName = Request.QueryString["tablename"];
			int pendCol = 2;

			conn.QueryString = "select * from PENDING_" + tableName;
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));			

			if (LBL_ACTIVE.Text.Trim() == "1")
				pendCol = 3;
			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,pendCol);
				dr[3] = getPendingStatus(conn.GetFieldValue(i,pendCol));
				dt.Rows.Add(dr);
			}			

			DGRequest.DataSource = new DataView(dt);
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
			string tableName = Request.QueryString["tablename"];

			if (LBL_ACTIVE.Text.Trim() == "1")
				conn.QueryString = "select * from " + tableName + " where ACTIVE = '1'";
			else
				conn.QueryString = "select * from " + tableName ;
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
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
		}

		private void DGExisting_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{			
			DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (TXT_ID.Text.Trim() == "" || TXT_DESC.Text.Trim() == "") return;

			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select * from " + Request.QueryString["tablename"] + " WHERE " + getColumnKey() + "='" + TXT_ID.Text.Trim() + "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		
				

			/*
			conn.QueryString = "exec PARAM_GENERAL_" + Request.QueryString["tablename"] + "_MAKER " +
								"'" + this.TXT_ID.Text.Trim() + "', " +
								"'" + this.TXT_DESC.Text.Trim() + "', " +
								"'" + this.LBL_SAVEMODE.Text.Trim() + "'";
			conn.ExecuteNonQuery();
			*/
			
			executeMaker(TXT_ID.Text.Trim(), TXT_DESC.Text.Trim(), LBL_SAVEMODE.Text.Trim());
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
					LBL_SAVEMODE.Text = e.Item.Cells[2].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[1].Text;
					activateControlKey(true);
					break;

				case "delete":
					string id = e.Item.Cells[0].Text;
					string colKey = "";

					colKey = getColumnKey();
					conn.QueryString = "delete from PENDING_" + Request.QueryString["tablename"] + " WHERE " + colKey + "='" + id + "'";
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
					activateControlKey(true);
					break;

				case "delete":					
					string id = e.Item.Cells[0].Text.Trim();
					/*
					conn.QueryString = "exec PARAM_GENERAL_" + Request.QueryString["tablename"] + "_MAKER '" + id + "','" + e.Item.Cells[1].Text + "','2'";
					conn.ExecuteQuery();
					*/
					executeMaker(id, e.Item.Cells[1].Text, "2");
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

		private void executeMaker(string id, string desc, string pendingStatus) 
		{
			conn.QueryString = "SELECT COUNT(*) JUMLAH FROM PENDING_" +Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ "='" +id+ "'";
			conn.ExecuteQuery();

			int jumlah = Convert.ToInt16(conn.GetFieldValue("JUMLAH"));
			
			if (jumlah > 0) 
			{				
				conn.QueryString = "UPDATE PENDING_" +Request.QueryString["tablename"]+ " SET " + getColumnDesc()+
					"= '" +desc+ "', PENDINGSTATUS = '" +pendingStatus+ "' WHERE "+getColumnKey()+ "= '"+id+"'";
				conn.ExecuteQuery();
			}
			else 
			{
				if (LBL_ACTIVE.Text.Trim() == "1")
					conn.QueryString = "INSERT INTO PENDING_" +Request.QueryString["tablename"]+
						" VALUES ('"+id+"', '"+desc+"', '1', '"+pendingStatus+"')";
				else if (LBL_ACTIVE.Text.Trim() == "0")
					conn.QueryString = "INSERT INTO PENDING_" +Request.QueryString["tablename"]+
						" VALUES ('"+id+"', '"+desc+"', '"+pendingStatus+"')";
				try 
				{
					conn.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					if (ex.Message.ToString().IndexOf("truncate") > 0)
						Tools.popMessage(this, "Input melebihi batas !");
				}
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg=8");
			
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();
		}

		protected void TXT_ID_TextChanged(object sender, System.EventArgs e)
		{
		
		}
	}		
}
