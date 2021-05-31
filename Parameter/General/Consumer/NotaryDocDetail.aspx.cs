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
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for NotaryDocDetail.
	/// </summary>
	public partial class NotaryDocDetail : System.Web.UI.Page
	{
		protected Connection conn,conn2;
		protected int digits;
		protected int digits2;
		protected int digits3;
		protected string zeros;
		protected string zeros2;
		protected string codes;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			
			SetDBconn();
			
			if (!IsPostBack)
			{
				setMax();
				dtDoc();
				viewExistingData();
				viewPendingData();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");	
			
			this.DG_EXISTING.PageIndexChanged +=new DataGridPageChangedEventHandler(DGExisting_PageIndexChanged);
			this.DG_REQUEST.PageIndexChanged +=new DataGridPageChangedEventHandler(DGRequest_PageIndexChanged);
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
			this.DG_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGExisting_ItemCommand);
			this.DG_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGExisting_PageIndexChanged);
			this.DG_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DG_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion

		private void setMax()
		{
			conn.QueryString = "select * from TRFDOCNOTARY_DETAIL";
			conn.ExecuteQuery();

			if(conn.GetRowCount() == 0)
			{
				conn.QueryString="select max(dnd_code) from RFDOCNOTARY_DETAIL";
				conn.ExecuteQuery();
				zeros = conn.GetFieldValue(0,0);
				digits = zeros.Length;
				digits2 = Int32.Parse(zeros)+1;
				zeros2 = digits2.ToString();
				codes = digits2.ToString();
				for (digits3 = zeros2.Length;digits3<digits;digits3++)
				{
					codes='0' + codes;
				
				}
				lbl_max.Text=codes;
			}
			else
			{
				conn.QueryString="select max(dnd_code) from TRFDOCNOTARY_DETAIL";
				conn.ExecuteQuery();
				zeros = conn.GetFieldValue(0,0);
				digits = zeros.Length;
				digits2 = Int32.Parse(zeros)+1;
				zeros2 = digits2.ToString();
				codes = digits2.ToString();
				for (digits3 = zeros2.Length;digits3<digits;digits3++)
				{
					codes='0' + codes;
				
				}
				lbl_max.Text=codes;

			}
		}

		private void SetDBconn()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" + conn2.GetFieldValue("DB_LOGINID") + ";pwd=" + conn2.GetFieldValue("DB_LOGINPWD") + ";Pooling=true");
		}
		private void dtDoc()
		{
			string myQuery="select dnm_code,dnm_desc from rfdocnotary_master";
			GlobalTools.fillRefList(this.DDL_DOC_TYPE,myQuery,conn);
		
		}
		void DGRequest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DG_REQUEST.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}
		void DGExisting_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{			
			DG_EXISTING.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		}
		private void viewExistingData()
		{
			conn.QueryString="select dnd_code,dnd_desc,a.dnm_code,dnm_desc,col_type,a.active,"+
				"case when col_type='H0' then 'Tanah/Bangunan/Tanah & Bangunan' "+
				"when col_type='C01' then 'Kendaraan' "+
				"else '' end COL from rfdocnotary_detail a "+
				"left join rfdocnotary_master b on a.dnm_code=b.dnm_code where a.active='1'";
			conn.ExecuteQuery();
			DG_EXISTING.DataSource = conn.GetDataTable().Copy();
			try 
			{
				DG_EXISTING.DataBind();
			}
			catch 
			{
				DG_EXISTING.CurrentPageIndex = DG_EXISTING.PageCount - 1;
				DG_EXISTING.DataBind();
			}
		}
		private void viewPendingData()
		{
			conn.QueryString="select dnd_code,dnd_desc,a.dnm_code,dnm_desc,col_type,a.CH_STA,"+
				"case when col_type='H0' then 'Tanah/Bangunan/Tanah & Bangunan' "+
				"when col_type='C01' then 'Kendaraan' "+
				"else '' end COL from trfdocnotary_detail a "+
				"left join rfdocnotary_master b on a.dnm_code=b.dnm_code";
			conn.ExecuteQuery();
			DG_REQUEST.DataSource = conn.GetDataTable().Copy();
			try 
			{
				DG_REQUEST.DataBind();
			}
			catch 
			{
				DG_REQUEST.CurrentPageIndex = DG_REQUEST.PageCount - 1;
				DG_REQUEST.DataBind();
			}

			for(int i = 0 ; i < DG_REQUEST.Items.Count; i++)
			{
				System.Web.UI.WebControls.Label lbl = (System.Web.UI.WebControls.Label)DG_REQUEST.Items[i].Cells[7].FindControl("LBL_STA");
				lbl.Text = getPendingStatus(DG_REQUEST.Items[i].Cells[3].Text.Trim());
			}
		}
		private void executeMaker(string id, string desc, string doc, string col, string pendingStatus) 
		{
			if (col=="&nbsp;")
				col="";
			if (desc=="&nbsp;")
				desc="";
			string myQueryString="";

			

			conn.QueryString = "SELECT * FROM TRFDOCNOTARY_DETAIL WHERE DND_CODE='" +id+ "'";
			conn.ExecuteQuery();

			int jumlah = conn.GetRowCount();
			
			if (jumlah > 0) 
			{				
				myQueryString = "UPDATE TRFDOCNOTARY_DETAIL SET DND_DESC= '" +desc+ "',DNM_CODE='"+doc+"',COL_TYPE='"+col+"', CH_STA = '" +pendingStatus+ "' WHERE DND_CODE= '"+id+"'";
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
				myQueryString="INSERT INTO tRFDOCNOTARY_DETAIL VALUES ('"+id+"', '"+desc+"','"+doc+"','"+col+"','"+pendingStatus+"','')";
				conn.QueryString = myQueryString;
				
				
				try 
				{
					conn.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					Response.Write("CEK LAGI, ADA YANG SALAH!!");
					if (ex.Message.ToString().IndexOf("truncate") > 0)
						Tools.popMessage(this, "Input melebihi batas !");
				}
			}
			
		}
		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			DDL_COL.SelectedValue="";
			DDL_DOC_TYPE.SelectedValue="";
			TXT_DESC.Text="";
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
		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//DDL_COL.SelectedValue="";
			//DDL_DOC_TYPE.SelectedValue="";
			//TXT_DESC.Text="";
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					LBL_ID.Text = e.Item.Cells[4].Text.Trim();
					if (e.Item.Cells[3].Text.Trim()=="&nbsp;")
						DDL_COL.SelectedValue = "";
					else
						DDL_COL.SelectedValue = e.Item.Cells[3].Text.Trim();
					if (e.Item.Cells[1].Text.Trim()=="&nbsp;")
						TXT_DESC.Text = "";
					else
						TXT_DESC.Text = e.Item.Cells[1].Text.Trim();
					if (e.Item.Cells[5].Text.Trim()=="&nbsp;")
						DDL_DOC_TYPE.SelectedValue = "";
					else
						DDL_DOC_TYPE.SelectedValue = e.Item.Cells[5].Text.Trim();
					break;

				case "delete":
					string id	= e.Item.Cells[4].Text.Trim();
					string desc = e.Item.Cells[1].Text.Trim();
					string doc  = e.Item.Cells[5].Text.Trim();
					string col  = e.Item.Cells[3].Text.Trim();
					//GlobalTools.popMessage(this,e.Item.Cells[3].Text);
					//Response.Write(id+"<br>"+desc+"<br>"+doc+"<br>"+col+"<br>");
					executeMaker(id,desc,doc,col,"2");
					viewPendingData();
					break;

				default :
					break;
			}
		}
		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			//DDL_COL.SelectedValue="";
			//DDL_DOC_TYPE.SelectedValue="";
			//TXT_DESC.Text="";
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[3].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					if (e.Item.Cells[6].Text.Trim()=="&nbsp;")
						DDL_COL.SelectedValue = "";
					else
						DDL_COL.SelectedValue = e.Item.Cells[6].Text.Trim();
					if (e.Item.Cells[1].Text.Trim()=="&nbsp;")
						TXT_DESC.Text = "";
					else
						TXT_DESC.Text = e.Item.Cells[1].Text.Trim();
					if (e.Item.Cells[5].Text.Trim()=="&nbsp;")
						DDL_DOC_TYPE.SelectedValue = "";
					else
						DDL_DOC_TYPE.SelectedValue = e.Item.Cells[5].Text.Trim();
				break;

				case "delete":
					string id = e.Item.Cells[4].Text;
					conn.QueryString = "delete from TRFDOCNOTARY_DETAIL WHERE DND_CODE='" + id + "'";
					conn.ExecuteQuery();
					viewPendingData();
					break;
				default :
					break;
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if(LBL_SAVEMODE.Text.Trim()=="1")
				executeMaker(lbl_max.Text.Trim(), TXT_DESC.Text.Trim(), DDL_DOC_TYPE.SelectedValue, DDL_COL.SelectedValue, LBL_SAVEMODE.Text.Trim());
			else
				executeMaker(LBL_ID.Text.Trim(), TXT_DESC.Text.Trim(), DDL_DOC_TYPE.SelectedValue, DDL_COL.SelectedValue, LBL_SAVEMODE.Text.Trim());

			viewPendingData();
			TXT_DESC.Text="";
			setMax();

			LBL_SAVEMODE.Text = "1";
			LBL_ID.Text="";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleId=40");
		}
	}
}
