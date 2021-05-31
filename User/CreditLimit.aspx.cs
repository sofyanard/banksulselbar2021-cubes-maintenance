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
	/// Summary description for CreditLimit.
	/// </summary>
	public partial class CreditLimit : System.Web.UI.Page
	{
		protected Connection conn, connConsumer;
		DataTable dtRequest = new DataTable();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(Session["ConnString"].ToString());
			
			conn.QueryString = "select db_ip, db_nama, db_loginid, db_loginpwd from rfmodule where moduleid = '" + "40" + "'";
			conn.ExecuteQuery();
			string db_ip	= conn.GetFieldValue(0, "db_ip"),
				db_nama		= conn.GetFieldValue(0, "db_nama"),
				db_loginid	= conn.GetFieldValue(0, "db_loginid"),
				db_loginpwd	= conn.GetFieldValue(0, "db_loginpwd");

			connConsumer = new Connection ("Data Source=" + db_ip + ";Initial Catalog=" + db_nama + ";uid=" + db_loginid + ";pwd=" + db_loginpwd + ";Pooling=true");

			if (!IsPostBack)
			{
				TXT_USERID.Text = Request.QueryString["userid"];

				FillGrid();

				DDL_PRODUCTID.Items.Add(new ListItem("- SELECT -", ""));
				connConsumer.QueryString = "select productid, productname from tproduct";
				connConsumer.ExecuteQuery();
				for (int i = 0; i < connConsumer.GetRowCount(); i++)
					DDL_PRODUCTID.Items.Add(new ListItem(connConsumer.GetFieldValue(i, "productname"), connConsumer.GetFieldValue(i, "productid")));

				dtRequest.Columns.Add(new DataColumn("PRODUCTID"));
				dtRequest.Columns.Add(new DataColumn("PRODUCTNAME"));
				dtRequest.Columns.Add(new DataColumn("CL_ISMITRA"));
				dtRequest.Columns.Add(new DataColumn("CL_ISMITRA_STATUS"));
				dtRequest.Columns.Add(new DataColumn("ACC_LIMIT"));
				dtRequest.Columns.Add(new DataColumn("RJ_LIMIT"));
				dtRequest.Columns.Add(new DataColumn("GRY_LIMIT"));
				dtRequest.Columns.Add(new DataColumn("STATUSDESC"));
				dtRequest.Columns.Add(new DataColumn("SEQ"));
				dtRequest.Columns.Add(new DataColumn("STATUS"));

				Session.Add("dtRequest", dtRequest);
			}
			dtRequest = (DataTable) Session["dtRequest"];
		}

		private void FillGrid() 
		{
			connConsumer.QueryString = "select SC_ID, a.PRODUCTID, b.PRODUCTNAME, CL_ISMITRA, case cl_ismitra when '1' then 'YES' when '0' then 'NO' END CL_ISMITRA_STATUS, RJ_LIMIT, ACC_LIMIT, GRY_LIMIT from SCCREDITLIMIT a inner join tproduct b on a.productid = b.productid " + 
				"where sc_id = '" + Request.QueryString["userid"] + "'";
			connConsumer.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = connConsumer.GetDataTable().Copy();

			DataGrid1.DataSource = dt;
			DataGrid1.DataBind();

			for (int i = 0; i < DataGrid1.Items.Count; i++)
			{
				DataGrid1.Items[i].Cells[4].Text = GlobalTools.MoneyFormat(DataGrid1.Items[i].Cells[4].Text);
				DataGrid1.Items[i].Cells[5].Text = GlobalTools.MoneyFormat(DataGrid1.Items[i].Cells[5].Text);
				DataGrid1.Items[i].Cells[6].Text = GlobalTools.MoneyFormat(DataGrid1.Items[i].Cells[6].Text);
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
					Label1.Text = "0";
					EnableEntries();

					DDL_PRODUCTID.SelectedValue	= e.Item.Cells[0].Text;
					TXT_ACC_LIMIT.Text	= e.Item.Cells[4].Text;
					TXT_RJ_LIMIT.Text	= e.Item.Cells[5].Text;
					TXT_GRY_LIMIT.Text	= e.Item.Cells[6].Text;

					if (e.Item.Cells[2].Text.Trim() == "1")
						CHK_CL_ISMITRA.Checked = true;
					else
						CHK_CL_ISMITRA.Checked = false;
					break;
				case "delete":
					break;
			}
		}

		protected void BTN_NEW_Click(object sender, System.EventArgs e)
		{
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

			string isMitra = "0", isMitraDesc = "NO", status = "", statusDesc = "";
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

			if (CHK_CL_ISMITRA.Checked == true)
			{
				isMitra = "1";
				isMitraDesc = "YES";
			}
			
			dr = dtRequest.NewRow();
			dr[0] = DDL_PRODUCTID.SelectedValue;
			dr[1] = DDL_PRODUCTID.SelectedItem.Text;
			dr[2] = isMitra;
			dr[3] = isMitraDesc;
			dr[4] = TXT_ACC_LIMIT.Text;
			dr[5] = TXT_RJ_LIMIT.Text;
			dr[6] = TXT_GRY_LIMIT.Text;
			dr[7] = statusDesc;
			dr[8] = dtRequest.Rows.Count + 1;
			dr[9] = status;
			dtRequest.Rows.Add(dr);

			Datagrid2.DataSource = dtRequest;
			Session.Remove("dtRequest");
			Session.Add("dtRequest", dtRequest);
			Datagrid2.DataBind();
			
			/*
			conn.QueryString = "exec su_sccreditlimit '" + TXT_USERID.Text + "', " + 
				GlobalTools.ConvertNull(DDL_PRODUCTID.SelectedValue) + ", '" + 
				isMitra + "', " + GlobalTools.ConvertFloat(TXT_ACC_LIMIT.Text) + ", " + 
				GlobalTools.ConvertFloat(TXT_RJ_LIMIT.Text) + ", " + 
				GlobalTools.ConvertFloat(TXT_GRY_LIMIT.Text);
			conn.ExecuteNonQuery();
			*/

			DDL_PRODUCTID.SelectedValue		= "";
			CHK_CL_ISMITRA.Checked			= false;
			TXT_ACC_LIMIT.Text				= "";
			TXT_RJ_LIMIT.Text				= "";
			TXT_GRY_LIMIT.Text				= "";

			DisableEntries();
			Label1.Text = "1";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BTN_NEW.Visible		= true;
			BTN_SUBMIT.Visible	= false;
			BTN_CANCEL.Visible	= false;
			Button1.Visible		= true;

			DDL_PRODUCTID.SelectedValue		= "";
			CHK_CL_ISMITRA.Checked			= false;
			TXT_ACC_LIMIT.Text				= "";
			TXT_RJ_LIMIT.Text				= "";
			TXT_GRY_LIMIT.Text				= "";

			DisableEntries();
			Label1.Text = "1";
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			FillGrid();
		}

		private void DisableEntries()
		{
			DDL_PRODUCTID.Enabled		= false;
			TXT_ACC_LIMIT.Enabled		= false;
			TXT_RJ_LIMIT.Enabled		= false;
			TXT_GRY_LIMIT.Enabled		= false;
			CHK_CL_ISMITRA.Enabled		= false;
		}

		private void EnableEntries()
		{
			DDL_PRODUCTID.Enabled		= true;
			TXT_ACC_LIMIT.Enabled		= true;
			TXT_RJ_LIMIT.Enabled		= true;
			TXT_GRY_LIMIT.Enabled		= true;
			CHK_CL_ISMITRA.Enabled		= true;
		}

		private void Datagrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton) e.CommandSource).CommandName)
			{
				case "delete":
					for (int i = 0; i < dtRequest.Rows.Count; i++)
					{
						if (dtRequest.Rows[i]["SEQ"].ToString() == e.Item.Cells[8].Text)
						{
							dtRequest.Rows[i].Delete();
							break;
						}
					}
					Session.Remove("dtRequest");
					Session.Add("dtRequest", dtRequest);
					Datagrid2.DataSource = dtRequest;
					Datagrid2.DataBind();
					break;
			}
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>window.close();</script>");
		}
	}
}
