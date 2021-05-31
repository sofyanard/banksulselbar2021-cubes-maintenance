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
	/// Summary description for CreditLimitCC.
	/// </summary>
	public partial class CreditLimitCC : System.Web.UI.Page
	{
		protected Connection conn, connCC;
		DataTable dtCCRequest = new DataTable();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());
			
			conn.QueryString = "select db_ip, db_nama, db_loginid, db_loginpwd from rfmodule where moduleid = '" + "20" + "'";
			conn.ExecuteQuery();
			string db_ip	= conn.GetFieldValue(0, "db_ip"),
				db_nama		= conn.GetFieldValue(0, "db_nama"),
				db_loginid	= conn.GetFieldValue(0, "db_loginid"),
				db_loginpwd	= conn.GetFieldValue(0, "db_loginpwd");

			connCC = new Connection ("Data Source=" + db_ip + ";Initial Catalog=" + db_nama + ";uid=" + db_loginid + ";pwd=" + db_loginpwd + ";Pooling=true");

			if (!IsPostBack)
			{
				TXT_USERID.Text = Request.QueryString["userid"];

				FillGrid();

				DDL_PRODUCTID.Items.Add(new ListItem("- SELECT -", ""));
				connCC.QueryString = "select productid, productname from tproduct";
				connCC.ExecuteQuery();
				for (int i = 0; i < connCC.GetRowCount(); i++)
					DDL_PRODUCTID.Items.Add(new ListItem(connCC.GetFieldValue(i, "productname"), connCC.GetFieldValue(i, "productid")));

				dtCCRequest.Columns.Add(new DataColumn("PRODUCTID"));
				dtCCRequest.Columns.Add(new DataColumn("PRODUCTNAME"));
				dtCCRequest.Columns.Add(new DataColumn("ACC_LIMIT"));
				dtCCRequest.Columns.Add(new DataColumn("STATUSDESC"));
				dtCCRequest.Columns.Add(new DataColumn("SEQ"));
				dtCCRequest.Columns.Add(new DataColumn("STATUS"));

				Session.Add("dtCCRequest", dtCCRequest);
			}
			dtCCRequest = (DataTable) Session["dtCCRequest"];
		}

		private void FillGrid()
		{
			connCC.QueryString = "select * from VW_USERPRODLIMIT where sc_id = '" + Request.QueryString["userid"] + "'";
			connCC.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = connCC.GetDataTable().Copy();

			DataGrid1.DataSource = dt;
			DataGrid1.DataBind();

			for (int i = 0; i < DataGrid1.Items.Count; i++)
				DataGrid1.Items[i].Cells[2].Text = GlobalTools.MoneyFormat(DataGrid1.Items[i].Cells[2].Text);
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

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>window.close();</script>");
		}

		protected void BTN_NEW_Click(object sender, System.EventArgs e)
		{
			BTN_NEW.Visible		= false;
			BTN_SUBMIT.Visible	= true;
			BTN_CANCEL.Visible	= true;
			Button1.Visible		= false;

			EnableEntries();
		}

		private void EnableEntries()
		{
			DDL_PRODUCTID.Enabled		= true;
			TXT_ACC_LIMIT.Enabled		= true;
		}

		private void DisableEntries()
		{
			DDL_PRODUCTID.Enabled		= false;
			TXT_ACC_LIMIT.Enabled		= false;
		}

		private void Datagrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton) e.CommandSource).CommandName)
			{
				case "delete":
					for (int i = 0; i < dtCCRequest.Rows.Count; i++)
					{
						if (dtCCRequest.Rows[i]["SEQ"].ToString() == e.Item.Cells[5].Text)
						{
							dtCCRequest.Rows[i].Delete();
							break;
						}
					}
					Session.Remove("dtCCRequest");
					Session.Add("dtCCRequest", dtCCRequest);
					Datagrid2.DataSource = dtCCRequest;
					Datagrid2.DataBind();
					break;
			}
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			FillGrid();
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BTN_NEW.Visible		= true;
			BTN_SUBMIT.Visible	= false;
			BTN_CANCEL.Visible	= false;
			Button1.Visible		= true;

			DDL_PRODUCTID.SelectedValue		= "";
			TXT_ACC_LIMIT.Text				= "";

			DisableEntries();
			Label1.Text = "1";
		}

		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton) e.CommandSource).CommandName)
			{
				case "edit":
					BTN_NEW.Visible		= false;
					BTN_SUBMIT.Visible	= true;
					BTN_CANCEL.Visible	= true;
					Button1.Visible		= false;
					Label1.Text = "0";
					EnableEntries();

					DDL_PRODUCTID.SelectedValue	= e.Item.Cells[0].Text;
					TXT_ACC_LIMIT.Text	= e.Item.Cells[2].Text;

					break;
				case "delete":
					break;
			}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			connCC.QueryString = "select count(*) from VW_USERPRODLIMIT where sc_id = '" + Request.QueryString["userid"] + "' and productid = '" + DDL_PRODUCTID.SelectedValue + "'";
			connCC.ExecuteQuery();

			if ((Label1.Text == "1") && (connCC.GetFieldValue(0,0) != "0"))
			{
				Response.Write("<script language='javascript'>alert('User already have approval limit for " + DDL_PRODUCTID.SelectedItem.Text + "!');</script>");
				return;
			}

			BTN_NEW.Visible		= true;
			BTN_SUBMIT.Visible	= false;
			BTN_CANCEL.Visible	= false;
			Button1.Visible		= true;

			string status = "", statusDesc = "";
			DataRow dr;

			status = Label1.Text;
			
			if (status == "1")
				statusDesc = "NEW";
			else if (status == "0")
				statusDesc = "EDIT";
			else if (status == "2")
				statusDesc = "DELETE";
			else 
				statusDesc = "";

			dr = dtCCRequest.NewRow();
			dr[0] = DDL_PRODUCTID.SelectedValue;
			dr[1] = DDL_PRODUCTID.SelectedItem.Text;
			dr[2] = TXT_ACC_LIMIT.Text;
			dr[3] = statusDesc;
			dr[4] = dtCCRequest.Rows.Count + 1;
			dr[5] = status;
			dtCCRequest.Rows.Add(dr);

			Datagrid2.DataSource = dtCCRequest;
			Session.Remove("dtCCRequest");
			Session.Add("dtCCRequest", dtCCRequest);
			Datagrid2.DataBind();
			
			DDL_PRODUCTID.SelectedValue		= "";
			TXT_ACC_LIMIT.Text				= "";

			DisableEntries();
			Label1.Text = "1";
		}
	}
}
