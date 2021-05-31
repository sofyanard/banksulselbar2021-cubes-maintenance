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

namespace CuBES_Maintenance.User
{
	/// <summary>
	/// Summary description for CreditLimitSME.
	/// </summary>
	public partial class CreditLimitSME : System.Web.UI.Page
	{
		protected Connection conn, connSME;
		DataTable dtRequest = new DataTable();
		protected Tools tool = new Tools();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());
			
			conn.QueryString = "select db_ip, db_nama, db_loginid, db_loginpwd from rfmodule where moduleid = '01'";
			conn.ExecuteQuery();
			string db_ip	= conn.GetFieldValue(0, "db_ip"),
				db_nama		= conn.GetFieldValue(0, "db_nama"),
				db_loginid	= conn.GetFieldValue(0, "db_loginid"),
				db_loginpwd	= conn.GetFieldValue(0, "db_loginpwd");

			connSME = new Connection ("Data Source=" + db_ip + ";Initial Catalog=" + db_nama + ";uid=" + db_loginid + ";pwd=" + db_loginpwd + ";Pooling=true");

			if (!IsPostBack)
			{
				TXT_USERID.Text = Request.QueryString["userid"];

				ViewExisting();
				ViewPending();

				GlobalTools.fillRefList(DDL_PROGRAM, "SELECT * FROM VW_PARAM_SMECREDITLIMIT_RFPROGRAM", connSME);
				GlobalTools.fillRefList(DDL_APPTYPE, "SELECT * FROM VW_PARAM_SMECREDITLIMIT_RFAPPLICATIONTYPE", connSME);
			}
		}

		private void ViewExisting() 
		{
			connSME.QueryString = "SELECT * FROM VW_PARAM_SMECREDITLIMIT_VIEWEXIXSTING WHERE USERID = '" + Request.QueryString["userid"] + "'";
			connSME.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = connSME.GetDataTable().Copy();

			DataGrid1.DataSource = dt;
			DataGrid1.DataBind();

			/*for (int i = 0; i < DataGrid1.Items.Count; i++)
			{
				DataGrid1.Items[i].Cells[4].Text = GlobalTools.MoneyFormat(DataGrid1.Items[i].Cells[4].Text);
				DataGrid1.Items[i].Cells[5].Text = GlobalTools.MoneyFormat(DataGrid1.Items[i].Cells[5].Text);
			}*/
		}

		private void ViewPending()
		{
			connSME.QueryString = "SELECT * FROM VW_PARAM_SMECREDITLIMIT_VIEWPENDING WHERE USERID = '" + Request.QueryString["userid"] + "'";
			connSME.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = connSME.GetDataTable().Copy();

			Datagrid2.DataSource = dt;
			Datagrid2.DataBind();

			/*for (int i = 0; i < DataGrid1.Items.Count; i++)
			{
				DataGrid1.Items[i].Cells[4].Text = GlobalTools.MoneyFormat(DataGrid1.Items[i].Cells[4].Text);
				DataGrid1.Items[i].Cells[5].Text = GlobalTools.MoneyFormat(DataGrid1.Items[i].Cells[5].Text);
			}*/
		}

		private void DisableEntries()
		{
			DDL_PROGRAM.Enabled		= false;
			DDL_APPTYPE.Enabled		= false;
			TXT_LIMIT1.Enabled		= false;
			TXT_LIMIT2.Enabled		= false;
		}

		private void EnableEntries()
		{
			DDL_PROGRAM.Enabled		= true;
			DDL_APPTYPE.Enabled		= true;
			TXT_LIMIT1.Enabled		= true;
			TXT_LIMIT2.Enabled		= true;
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
			this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.Datagrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_ItemCommand);

		}
		#endregion

		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton) e.CommandSource).CommandName)
			{
				case "edit":
					BTN_NEW.Visible		= false;
					BTN_SUBMIT.Visible	= true;
					BTN_CANCEL.Visible	= true;
					Button1.Visible		= false;
					EnableEntries();

					Label1.Text = "2";
					try { DDL_PROGRAM.SelectedValue	= e.Item.Cells[0].Text; }
					catch {}
					try { DDL_APPTYPE.SelectedValue	= e.Item.Cells[2].Text; }
					catch {}
					TXT_LIMIT1.Text	= e.Item.Cells[4].Text;
					TXT_LIMIT2.Text	= e.Item.Cells[5].Text;

					break;
				case "delete":
					try
					{
						connSME.QueryString = "exec PARAM_SMECREDITLIMIT_MAKER '2', '" + 
							TXT_USERID.Text.Trim() + "','" + 
							e.Item.Cells[0].Text.Trim() + "', '" + 
							e.Item.Cells[2].Text.Trim() + "', " + 
							tool.ConvertFloat(e.Item.Cells[4].Text) + ", " + 
							tool.ConvertFloat(e.Item.Cells[5].Text) + ", '" + 
							Label1.Text.Trim() + "'";
						connSME.ExecuteQuery();

						ViewPending();
						Label1.Text = "1";
					}
					catch (Exception ex)
					{
						Response.Write("<!--" + ex.Message + "-->");
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)		
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this,errmsg);
					}
					break;
			}
		}

		protected void BTN_NEW_Click(object sender, System.EventArgs e)
		{
			Label1.Text = "1";

			BTN_NEW.Visible		= false;
			BTN_SUBMIT.Visible	= true;
			BTN_CANCEL.Visible	= true;
			Button1.Visible		= false;

			EnableEntries();
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			BTN_NEW.Visible		= true;
			BTN_SUBMIT.Visible	= false;
			BTN_CANCEL.Visible	= false;
			Button1.Visible		= true;

			try
			{
				connSME.QueryString = "exec PARAM_SMECREDITLIMIT_MAKER '1', '" + 
					TXT_USERID.Text.Trim() + "','" + 
					DDL_PROGRAM.SelectedValue.Trim() + "', '" + 
					DDL_APPTYPE.SelectedValue.Trim() + "', " + 
					tool.ConvertFloat(TXT_LIMIT1.Text.Trim()) + ", " + 
					tool.ConvertFloat(TXT_LIMIT2.Text.Trim()) + ", '" + 
					Label1.Text.Trim() + "'";
				connSME.ExecuteQuery();

				ViewPending();
			}
			catch (Exception ex)
			{
				Response.Write("<!--" + ex.Message + "-->");
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)		
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this,errmsg);
			}

			/*
			string status = "", statusDesc = "";
			status = Label1.Text;
			
			if (status == "1")
				statusDesc = "INSERT";
			else if (status == "2")
				statusDesc = "UPDATE";
			else if (status == "3")
				statusDesc = "DELETE";
			else 
				statusDesc = "";

			DataRow dr;

			dr = dtRequest.NewRow();
			dr[0] = DDL_PROGRAM.SelectedValue;
			dr[1] = DDL_PROGRAM.SelectedItem.Text;
			dr[2] = DDL_APPTYPE.SelectedValue;
			dr[3] = DDL_APPTYPE.SelectedItem.Text;
			dr[4] = TXT_LIMIT1.Text;
			dr[5] = TXT_LIMIT2.Text;
			dr[6] = status;
			dr[7] = statusDesc;
			dr[8] = dtRequest.Rows.Count + 1;

			dtRequest.Rows.Add(dr);

			Datagrid2.DataSource = dtRequest;
			Session.Remove("dtRequest");
			Session.Add("dtRequest", dtRequest);
			Datagrid2.DataBind();
			*/
			
			DDL_PROGRAM.SelectedValue	= "";
			DDL_APPTYPE.SelectedValue	= "";
			TXT_LIMIT1.Text				= "";
			TXT_LIMIT2.Text				= "";

			DisableEntries();
			Label1.Text = "1";
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			ViewExisting();
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BTN_NEW.Visible		= true;
			BTN_SUBMIT.Visible	= false;
			BTN_CANCEL.Visible	= false;
			Button1.Visible		= true;

			DDL_PROGRAM.SelectedValue	= "";
			DDL_APPTYPE.SelectedValue	= "";
			TXT_LIMIT1.Text				= "";
			TXT_LIMIT2.Text				= "";

			DisableEntries();
			Label1.Text = "1";
		}

		private void Datagrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			Label1.Text = "3";
			
			switch (((LinkButton) e.CommandSource).CommandName)
			{
				case "delete":
					try
					{
						connSME.QueryString = "exec PARAM_SMECREDITLIMIT_MAKER '3', '" + 
							TXT_USERID.Text.Trim() + "','" + 
							e.Item.Cells[0].Text.Trim() + "', '" + 
							e.Item.Cells[2].Text.Trim() + "', " + 
							tool.ConvertFloat(e.Item.Cells[4].Text) + ", " + 
							tool.ConvertFloat(e.Item.Cells[5].Text) + ", '" + 
							Label1.Text.Trim() + "'";
						connSME.ExecuteQuery();

						ViewPending();
					}
					catch (Exception ex)
					{
						Response.Write("<!--" + ex.Message + "-->");
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)		
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this,errmsg);
					}
					break;
			}

			Label1.Text = "1";
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>window.close();</script>");
		}
	}
}
