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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for NotaryDocMaster.
	/// </summary>
	public partial class NotaryDocMaster : System.Web.UI.Page
	{
		protected int digits;
		protected int digits2;
		protected int digits3;
		protected string zeros;
		protected string zeros2;
		protected string codes;
		protected Connection conn;
		protected System.Web.UI.WebControls.DataGrid DG_EXISTING;
		protected Connection conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			
			SetDBconn();
			
			if(!IsPostBack)
			{
				CodeGen();
				LBL_SAVEMODE.Text="1";
				viewExistingData();
				viewPendingData();
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
			this.Datagrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_ItemCommand);
			this.Datagrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid1_PageIndexChanged);
			this.Datagrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_ItemCommand);
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);

		}
		#endregion
		
		private void SetDBconn()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" + conn2.GetFieldValue("DB_LOGINID") + ";pwd=" + conn2.GetFieldValue("DB_LOGINPWD") + ";Pooling=true");
		}

		private void CodeGen()
		{
			conn.QueryString="select convert(int,isnull(max(dnm_code),0)) from tRFDOCNOTARY_MASTER";
			conn.ExecuteQuery();
			
			zeros = conn.GetFieldValue(0,0);
			
			if (zeros=="0")
			{
				conn.QueryString="select convert(int,isnull(max(dnm_code),0)) from RFDOCNOTARY_MASTER where active=1";
				conn.ExecuteQuery();
				zeros = conn.GetFieldValue(0,0);
			}
				
			digits = zeros.Length;
			digits2 = Int32.Parse(zeros)+1;
			zeros2 = digits2.ToString();
			codes = digits2.ToString();
			
			for (digits3 = zeros2.Length;digits3<5;digits3++)
			{
				codes='0' + codes;
			}
			lbl_max.Text=codes;
		}
		
		private void viewExistingData()
		{
			conn.QueryString="select * from RFDOCNOTARY_MASTER where active='1'";
			conn.ExecuteQuery();
			Datagrid1.DataSource = conn.GetDataTable().Copy();
			try 
			{
				Datagrid1.DataBind();
			}
			catch 
			{
				Datagrid1.CurrentPageIndex = Datagrid1.PageCount - 1;
				Datagrid1.DataBind();
			}
		}
		
		private void viewPendingData()
		{
			conn.QueryString="select * from tRFDOCNOTARY_master";
			conn.ExecuteQuery();
			Datagrid2.DataSource = conn.GetDataTable().Copy();
			try 
			{
				Datagrid2.DataBind();
			}
			catch 
			{
				Datagrid2.CurrentPageIndex = Datagrid1.PageCount - 1;
				Datagrid2.DataBind(); 
			}
			for(int i = 0 ; i < Datagrid2.Items.Count; i++)
			{
				System.Web.UI.WebControls.Label lbl = (System.Web.UI.WebControls.Label)Datagrid2.Items[i].Cells[3].FindControl("LBL_STA");
				lbl.Text = getPendingStatus(Datagrid2.Items[i].Cells[2].Text.Trim());
			}
		}		
		private void executeMaker(string id, string desc, string pendingStatus) 
		{
			string myQueryString="";

			conn.QueryString = "SELECT * FROM tRFDOCNOTARY_master WHERE DNM_CODE='" +id+ "'";
			conn.ExecuteQuery();

			int jumlah = conn.GetRowCount();
			
			if (jumlah > 0) 
			{				
				myQueryString = "UPDATE tRFDOCNOTARY_master SET DNM_DESC= '" +desc+ "', CH_STA = '" +pendingStatus+ "' WHERE DNM_CODE= '"+id+"'";
				conn.QueryString = myQueryString;
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
			else 
			{
				myQueryString="INSERT INTO tRFDOCNOTARY_master VALUES ('"+id+"', '"+desc+"','"+pendingStatus+"','1')";
				conn.QueryString = myQueryString;
				
				try 
				{
					conn.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					Response.Write("CEK LAGI, ADA YANG SALAH!! " + myQueryString);
					if (ex.Message.ToString().IndexOf("truncate") > 0)
						Tools.popMessage(this, "Input melebihi batas !");
				}
			}
			
			CodeGen();
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
				

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			TXT_DESC.Text="";
			LBL_SAVEMODE.Text="1";
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text == "1")
				executeMaker(lbl_max.Text, TXT_DESC.Text.Trim(), LBL_SAVEMODE.Text.Trim());
			else
				executeMaker(LBL_ID.Text.Trim(), TXT_DESC.Text.Trim(), LBL_SAVEMODE.Text.Trim());

			viewPendingData();
			TXT_DESC.Text="";
			CodeGen();

			LBL_SAVEMODE.Text = "1";
			LBL_ID.Text="";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleId=40");
		}

		private void Datagrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			Datagrid1.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		
		}

		private void Datagrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			Datagrid2.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		private void Datagrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			TXT_DESC.Text = "";
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					LBL_ID.Text=e.Item.Cells[0].Text.Trim();
					
					if (e.Item.Cells[1].Text.Trim()=="&nbsp;")
						TXT_DESC.Text = "";
					else
						TXT_DESC.Text = e.Item.Cells[1].Text.Trim();
					break;

				case "delete":
					string id	= e.Item.Cells[0].Text.Trim();
					string desc = e.Item.Cells[1].Text.Trim();
					executeMaker(id,desc,"2");
					viewPendingData();
					CodeGen();
					break;

				default :
					break;
			}
		}

		private void Datagrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			TXT_DESC.Text="";
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[2].Text;
					LBL_ID.Text=e.Item.Cells[0].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_DESC.Text = e.Item.Cells[1].Text.Trim();
					if (e.Item.Cells[1].Text.Trim()=="&nbsp;")
						TXT_DESC.Text = "";
					break;

				case "delete":
					string id = e.Item.Cells[0].Text;
					
					conn.QueryString = "delete from tRFDOCNOTARY_master WHERE dnm_code='" + id + "'";
					conn.ExecuteQuery();
					viewPendingData();
					CodeGen();
					break;
				default :
					break;
			}
		
		}
	}
}
