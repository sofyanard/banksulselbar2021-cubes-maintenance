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
	/// Summary description for RFTemplateParam2.
	/// </summary>
	public partial class RFTemplateParam2 : System.Web.UI.Page
	{
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
					LBL_ACTIVE.Text = "1";	//default condition

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

			//BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			DGExisting.PageIndexChanged +=new DataGridPageChangedEventHandler(DGExisting_PageIndexChanged);
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

			//			if (LBL_ACTIVE.Text.Trim() == "1") -- data ditampilin semua (dewi)
			//				conn.QueryString = "select * from " + tableName + " where ACTIVE = '1'";
			//			else
			
			conn.QueryString = "select * from " + tableName ;
			conn.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("ACTIVE"));

			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,2);
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

			for (int i =0; i < DGExisting.Items.Count; i++)
			{			

				if (DGExisting.Items[i].Cells[2].Text.Trim() =="0" ) 
				{		// active = 0
					LinkButton l_del = (LinkButton) DGExisting.Items[i].FindControl("lnk_RfDelete");
					l_del.CommandName = "Undelete";
					l_del.Text = "Undelete";

					LinkButton l_edit = (LinkButton) DGExisting.Items[i].FindControl("lnk_RfEdit");
					l_edit.Visible = false;
				}
			}
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

		private string getColumnDesc()
		{
			return LBL_DESC.Text.Trim();
		}

		private void executeMaker(string id, string desc, string pendingStatus) 
		{
			conn.QueryString = "SELECT COUNT(*) JUMLAH FROM PENDING_" +Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ "='" +id+ "'";
			conn.ExecuteQuery();

			int jumlah = Convert.ToInt16(conn.GetFieldValue("JUMLAH"));
			
			if (jumlah > 0) 
			{				
				conn.QueryString = "UPDATE PENDING_" +Request.QueryString["tablename"]+ " SET ["+getColumnDesc()+
					"]= '" +desc+ "', PENDINGSTATUS = '" +pendingStatus+ "' WHERE "+getColumnKey()+ "= '"+id+"'";
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

		private void DGExisting_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string active="0";
			//if (TXT_ID.Text.Trim() == "" || TXT_DESC.Text.Trim() == "") return;
			if (TXT_ID.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"ID tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_ID);
				return;
			}
			else if (TXT_DESC.Text.Trim() == "") 
			{
				GlobalTools.popMessage(this,"Description tidak boleh kosong!");
				GlobalTools.SetFocus(this,TXT_DESC);
				return;
			}

			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select active from " + Request.QueryString["tablename"] + " WHERE " + getColumnKey() + "='" + TXT_ID.Text.Trim() + "'";
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

		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string id;

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
					id = e.Item.Cells[0].Text.Trim();
					
					executeMaker(id, e.Item.Cells[1].Text, "2");
					viewPendingData();
					break;

				case "undelete":					
					id = e.Item.Cells[0].Text.Trim();
					
					executeMaker(id, e.Item.Cells[1].Text, "0");
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			/*
            string classid="";
			try 
			{
				classid=Request.QueryString["classid"].ToString();
			}
			catch{ classid="";}
			
			if ((classid.Equals("01")) || (classid.ToString().Trim()=="01") )
				Response.Redirect("../HostParam.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+" ");
			else
				Response.Redirect("../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+" ");
            */
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();
		}
	}
}
