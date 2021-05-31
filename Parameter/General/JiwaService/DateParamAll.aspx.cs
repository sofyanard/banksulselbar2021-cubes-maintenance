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
using System.Configuration;
using Microsoft.VisualBasic;

namespace CuBES_Maintenance.Parameter.General.JiwaService
{
	/// <summary>
	/// Summary description for DateParamAll.
	/// </summary>
	public class DateParamAll : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton BTN_BACK;
		protected System.Web.UI.WebControls.Button BTN_SAVE;
		protected System.Web.UI.WebControls.Button BTN_CLEAR;
		protected System.Web.UI.WebControls.DataGrid DGR_DATE;
		protected System.Web.UI.WebControls.DataGrid DGR_REQUESTDATE;
		protected System.Web.UI.WebControls.DropDownList DDL_FUNCTION;
		protected System.Web.UI.WebControls.Label LBL_NO;
		protected System.Web.UI.WebControls.Label LBL_ID;
		protected System.Web.UI.WebControls.TextBox TXT_TGL1;
		protected System.Web.UI.WebControls.DropDownList DDL_BLN1;
		protected System.Web.UI.WebControls.TextBox TXT_THN1;
		protected System.Web.UI.WebControls.TextBox TXT_TGL2;
		protected System.Web.UI.WebControls.DropDownList DDL_BLN2;
		protected System.Web.UI.WebControls.TextBox TXT_THN2;
		protected Tools tools = new Tools();
		protected Connection conn;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			if(!IsPostBack)
			{
				CekCode();
				FillDDLFunction();
				DDL_BLN1.Items.Add(new ListItem("-- Pilih --",""));
				DDL_BLN2.Items.Add(new ListItem("-- Pilih --",""));
				for (int i = 1; i <= 12; i++)
				{
					DDL_BLN1.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
					DDL_BLN2.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
				}
				FillGridCurr();
				FillGridReq();
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
			this.BTN_BACK.Click += new System.Web.UI.ImageClickEventHandler(this.BTN_BACK_Click);
			this.BTN_SAVE.Click += new System.EventHandler(this.BTN_SAVE_Click);
			this.BTN_CLEAR.Click += new System.EventHandler(this.BTN_CLEAR_Click);
			this.DGR_DATE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_DATE_ItemCommand);
			this.DGR_DATE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_DATE_PageIndexChanged);
			this.DGR_REQUESTDATE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUESTDATE_ItemCommand);
			this.DGR_REQUESTDATE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUESTDATE_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void CekCode()
		{
			conn.QueryString = "SELECT ISNULL(MAX(CONVERT(INT, SEQ)),0) AS SEQ FROM RF_DATE";
			conn.ExecuteQuery();
			LBL_NO.Text = conn.GetFieldValue("SEQ").ToString();

			conn.QueryString="EXEC PARAM_GENERAL_RFGROUP_GENERATE_CODE '" + LBL_NO.Text + "'";
			conn.ExecuteQuery();

			LBL_ID.Text = conn.GetFieldValue(0,0);
		}
		
		private void FillDDLFunction()
		{
			DDL_FUNCTION.Items.Clear();
			DDL_FUNCTION.Items.Add(new ListItem("--Pilih--", ""));

			conn.QueryString = "EXEC JWS_SERVICE_FUNCTION ''";
			conn.ExecuteQuery();
			for(int i=0; i < conn.GetRowCount(); i++)
			{
				DDL_FUNCTION.Items.Add(new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			}
		}

		private void FillGridCurr()
		{
			conn.QueryString = "SELECT * FROM RF_DATE WHERE STATUS='1' ORDER BY SEQ ASC";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_DATE.DataSource = dt;
			try
			{
				DGR_DATE.DataBind();
			}
			catch
			{
				DGR_DATE.CurrentPageIndex = 0;
				DGR_DATE.DataBind();
			}
		}

		private void FillGridReq()
		{
			conn.QueryString = "SELECT * , CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_DATE WHERE STATUS='0' ORDER BY SEQ ASC";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_REQUESTDATE.DataSource = dt;
			try
			{
				DGR_REQUESTDATE.DataBind();
			}
			catch
			{
				DGR_REQUESTDATE.CurrentPageIndex = 0;
				DGR_REQUESTDATE.DataBind();
			}
		}

		/*private void BTN_TANGGAL_Click(object sender, System.EventArgs e)
		{
			if(DDL_TAHUN.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Pilih tahun terlebih dahulu!");
				return;
			}
			if(DDL_BULAN.SelectedValue == "")
			{
				GlobalTools.popMessage(this, "Pilih bulan terlebih dahulu");
				return;
			}
			Response.Write("<script language='javascript'>window.open('Calendar.aspx?bulan=" + DDL_BULAN.SelectedValue + "&tahun=" + DDL_TAHUN.SelectedValue + "&theForm=Form1&theObj=TXT_TANGGAL','Calendar','status=no,scrollbars=no,width=380,height=250');</script>");
		}*/

		private void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string tanggal1 = "";
			string tanggal2 = "";

			if(DDL_FUNCTION.SelectedValue.ToString() == "" || TXT_TGL1.Text=="" && DDL_BLN1.SelectedValue=="" && TXT_THN1.Text=="" && TXT_TGL2.Text=="" && DDL_BLN2.SelectedValue=="" && TXT_THN2.Text=="")
			{
				GlobalTools.popMessage(this, "Check Field Mandatory!");
				return;
			}
			else
			{
				if (Tools.isDateValid(this,TXT_TGL1.Text,DDL_BLN1.SelectedValue,TXT_THN1.Text)&&Tools.isDateValid(this, TXT_TGL2.Text,DDL_BLN2.SelectedValue,TXT_THN2.Text))
				{
					tanggal1 = tools.ConvertDate(TXT_TGL1.Text,DDL_BLN1.SelectedValue,TXT_THN1.Text);
					tanggal2 = tools.ConvertDate(TXT_TGL2.Text,DDL_BLN2.SelectedValue,TXT_THN2.Text);

					if (!Information.IsDate(tanggal1) || !Information.IsDate(tanggal2))
					{
						Tools.popMessage(this,"Invalid date");
						if (!Information.IsDate(tanggal1))
							Tools.SetFocus(this,TXT_TGL1);
						else
							Tools.SetFocus(this,TXT_TGL2);
					}
					else
					{
						tanggal1		= tanggal1.Replace("'","");
						tanggal2		= tanggal2.Replace("'","");

						try
						{
							conn.QueryString = "EXEC PARAM_GENERAL_PENDING_RFDATE_INSERT '" + LBL_ID.Text + "','" + DDL_FUNCTION.SelectedValue + "','" +
								DDL_FUNCTION.SelectedItem+ "','" + tanggal1 + "','" + tanggal2 + "','" + Session["UserID"].ToString() + "'";
							conn.ExecuteQuery();
						}
						catch (Exception ex)
						{
							CekCode();
							ClearData();
							FillGridCurr();
							FillGridReq();

							string errmsg = ex.Message.Replace("'","");
							if (errmsg.IndexOf("Last Query:") > 0)
								errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
							GlobalTools.popMessage(this, errmsg);
							return;
						}
						CekCode();
						ClearData();
						FillGridCurr();
						FillGridReq();
					}
				}
				else
				{
					Response.Write("<script language='javascript'>alert('Invalid Date !')</script>");
				}
			}
		}

		private void BTN_CLEAR_Click(object sender, System.EventArgs e)
		{
			ClearData();
		}

		private void ClearData()
		{
			DDL_FUNCTION.SelectedValue = "";
			TXT_TGL1.Text="";
			DDL_BLN1.SelectedValue="";
			TXT_THN1.Text="";
			TXT_TGL2.Text="";
			DDL_BLN2.SelectedValue="";
			TXT_THN2.Text="";
			CekCode();
		}

		private void DGR_DATE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_DATE.CurrentPageIndex = e.NewPageIndex;
			FillGridCurr();
		}

		private void DGR_DATE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_ID.Text = e.Item.Cells[0].Text.Trim();
					DDL_FUNCTION.SelectedValue = e.Item.Cells[1].Text.Trim();
					break;
				case "delete":
					conn.QueryString = "UPDATE RF_DATE SET STATUS='2' WHERE SEQ='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					
					ClearData();
					FillGridCurr();
					break;
			}
		}

		private void DGR_REQUESTDATE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUESTDATE.CurrentPageIndex = e.NewPageIndex;
			FillGridReq();
		}

		private void DGR_REQUESTDATE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit_req":
					LBL_ID.Text = e.Item.Cells[0].Text.Trim();
					DDL_FUNCTION.SelectedValue = e.Item.Cells[1].Text.Trim();
					break;
				case "delete_req":
					conn.QueryString = "DELETE RF_DATE WHERE SEQ='" + e.Item.Cells[0].Text.Trim() + "'";
					conn.ExecuteNonQuery();
					
					ClearData();
					FillGridReq();
					break;
			}
		}

		private void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../JWSParamMaker.aspx?mc="+Request.QueryString["mc"]+"&pg=9");
		}
	}
}
