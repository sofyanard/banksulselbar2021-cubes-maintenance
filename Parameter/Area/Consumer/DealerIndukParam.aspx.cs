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

namespace CuBES_Maintenance.Parameter.Area.Consumer
{
	/// <summary>
	/// Summary description for DealerIndukParam.
	/// </summary>
	public partial class DealerIndukParam : System.Web.UI.Page
	{
		protected Connection conn ;//= new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			
			if (!IsPostBack)
			{
				LBL_ID.Text = "00000";
				viewPendingData();
				viewExistingData();
			}
			
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");	
			
			DGExisting.PageIndexChanged +=new DataGridPageChangedEventHandler(DGExisting_PageIndexChanged);
			DGRequest.PageIndexChanged +=new DataGridPageChangedEventHandler(DGRequest_PageIndexChanged);
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

		}
		#endregion
		
		private void SetDBConn2()
		{
			conn.QueryString = "select * from RFMODULE where MODULEID=40";
			conn.ExecuteQuery();
			
			conn2 = new Connection ("Data Source=" + conn.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn.GetFieldValue("DB_NAMA") + ";uid=" +conn.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private string codeGen()
		{
			string x,y;
			
			/* 12-08-2005
			 Hitung jumlah field di TDEALER_INDUK untuk penambahan di MAXSEQ  */

			conn2.QueryString = "select isnull(count(DLI_CODE),0)as MAXCOUNT from TDEALER_INDUK";
			conn2.ExecuteQuery();
			if(conn2.GetFieldValue("MAXCOUNT")!="0")
			{
				LBL_NB.Text = conn2.GetFieldValue("MAXCOUNT");
			}

			int number = Int16.Parse(LBL_NB.Text);
			
			conn2.QueryString = "select isnull(max(DLI_CODE),0)+ "+LBL_NB.Text+" as MAXSEQ from DEALER_INDUK";
			conn2.ExecuteQuery();
			
			x = conn2.GetFieldValue(0,0);
			y = x;
			
			for (int a=x.Length; a<5; a++)
			{
				y = "0"+y;
			}

			number++;

			LBL_NB.Text = number.ToString();
			
			return y;
		}
		private void viewPendingData()
		{
			conn2.QueryString = "select convert(int,dli_code) DLI_SEQ, *, case when dli_kerjasama='1' then 'Telah Kerjasama' else 'Belum Kerjasama' end ST1, "+
				"case when dli_blocked='1' then 'Blocked' else 'No' end ST2, "+
				"case when ch_sta='0' then 'UPDATE' when ch_sta='1' then 'INSERT' when ch_sta='2' then 'DELETE' else '' end STATUS from TDEALER_INDUK";
			conn2.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			DGRequest.DataSource = dt;
			
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
		private void viewExistingData()
		{
			conn2.QueryString = "select convert(int,dli_code) DLI_SEQ, *, case when dli_kerjasama='1' then 'Telah Kerjasama' else 'Belum Kerjasama' end ST1, "+
				"case when dli_blocked='1' then 'Blocked' else 'No' end ST2  from DEALER_INDUK where active='1' ";
			conn2.ExecuteQuery();
			
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			DGExisting.DataSource = dt;
			
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
		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}
		private void clearControls() 
		{
			TXT_DESC.Text="";
			
			CHB_BLOCK.Checked=false;
			CHB_KERJASAMA.Checked=false;

			LBL_SAVEMODE.Text="1";
			LBL_ID.Text = "00000";
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void executeMaker(string id, string desc, string kr, string lck, string pendingStatus) 
		{
			/* 12-08-2005
			 GlobalTools.ConvertNull untuk DLI_KERJASAMA & DLI_BLOCKED dihilangkan */
			
			string myQueryString = "", seqid = "";
			
			conn2.QueryString = "select * from TDEALER_INDUK where dli_code='"+id+"'";
			conn2.ExecuteQuery();

			int jumlah = conn2.GetRowCount();
			
			if (jumlah > 0) 
			{
				myQueryString = "UPDATE TDEALER_INDUK set dli_desc = "+GlobalTools.ConvertNull(desc)+", "+
					"dli_kerjasama = "+kr+", dli_blocked = "+lck+" where dli_code='"+id+"'";
				conn2.QueryString = myQueryString;
				
				try 
				{
					conn2.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					if (ex.Message.ToString().IndexOf("truncate") > 0)
						Tools.popMessage(this, "Gagal melakukan update !");
				}
			}
			else 
			{
				if(pendingStatus == "1")
				{
					seqid = codeGen();

					myQueryString="INSERT INTO TDEALER_INDUK VALUES ('"+seqid+"',"+GlobalTools.ConvertNull(desc)+","+kr+","+lck+",'"+pendingStatus+"')";
					conn2.QueryString = myQueryString;
				}
				else
				{
					myQueryString="INSERT INTO TDEALER_INDUK VALUES ('"+id+"',"+GlobalTools.ConvertNull(desc)+","+kr+","+lck+",'"+pendingStatus+"')";
					conn2.QueryString = myQueryString;
				}

				try 
				{
					conn2.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					if (ex.Message.ToString().IndexOf("truncate") > 0)
						Tools.popMessage(this, "Input melebihi batas !");
				}
			}
			
		}
		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			clearControls();
 
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					string id   = CleansText(e.Item.Cells[5].Text);
					string desc = CleansText(e.Item.Cells[1].Text);
					string kr   = CleansText(e.Item.Cells[6].Text);
					string lck  = CleansText(e.Item.Cells[7].Text);
					string sta  = CleansText(e.Item.Cells[4].Text);
					
					LBL_SAVEMODE.Text=sta;
					
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					
					LBL_ID.Text=id;
					
					TXT_DESC.Text=desc;
					
					if (kr == "1")
						CHB_KERJASAMA.Checked=true;
					else
						CHB_KERJASAMA.Checked=false;
					
					if (lck == "1")
						CHB_BLOCK.Checked=true;
					else
						CHB_BLOCK.Checked=false;
					break;

				case "delete":
					conn2.QueryString = "delete from TDEALER_INDUK WHERE dli_code='" + e.Item.Cells[5].Text.Trim() + "'";
					conn2.ExecuteQuery();

					viewPendingData();
					
					break;
				default : //do nothing
					break;
			}
		}
		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearControls();
			
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					string id   = CleansText(e.Item.Cells[4].Text);
					string desc = CleansText(e.Item.Cells[1].Text);
					string kr   = CleansText(e.Item.Cells[5].Text);
					string lck  = CleansText(e.Item.Cells[6].Text);
					
					LBL_ID.Text=id;
					
					TXT_DESC.Text=desc;
					
					if (kr == "1")
						CHB_KERJASAMA.Checked=true;
					else
						CHB_KERJASAMA.Checked=false;
					
					if (lck == "1")
						CHB_BLOCK.Checked=true;
					else
						CHB_BLOCK.Checked=false;

					LBL_SAVEMODE.Text = "0"; 
					break;

				case "delete":
					string id1   = CleansText(e.Item.Cells[4].Text);
					string desc1 = CleansText(e.Item.Cells[1].Text);
					string kr1   = CleansText(e.Item.Cells[5].Text);
					string lck1  = CleansText(e.Item.Cells[6].Text);
					
					executeMaker(id1,desc1,kr1,lck1,"2");
					
					viewPendingData();
		
					break;
				case "detail":
					Response.Write("<script language=JavaScript>window.open('DealerIndukParamDetail.aspx?dli_code="+e.Item.Cells[4].Text.Trim()+"');</script>");
					break;
				default : //do nothing
					break;
			}
		}
		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleID=40");
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn2.QueryString= "select * from TDEALER_INDUK where dli_code='"+LBL_ID.Text.Trim()+"'";
			conn2.ExecuteQuery();
			
			if (conn2.GetRowCount()== 0)
			{
				string a="0",b="0",c,d;
				
				c = TXT_DESC.Text.Trim();
				d = LBL_ID.Text.Trim();
				
				if (CHB_BLOCK.Checked==true)
					b="1";
				
				if (CHB_KERJASAMA.Checked==true)
					a="1";
				
				executeMaker(d,c,a,b,LBL_SAVEMODE.Text.Trim());
				
				viewPendingData();
				clearControls();
			}
			else
			{
				GlobalTools.popMessage(this,"ID is already in the table, request cancelled");	
			}				
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls(); 
		}
	}
}
