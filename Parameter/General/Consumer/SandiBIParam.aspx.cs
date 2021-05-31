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
	/// Summary description for SandiBIParam.
	/// </summary>
	public partial class SandiBIParam : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);

			if(!IsPostBack)
			{
				ViewData(); 
			}
			else
			{
				InitialCon();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");	
		}

		private void ViewData()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");
			LBL_SAVEMODE.Text = "1"; 

			InitialCon();

			BindData1();
			BindData2();

			FillGroup();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void FillGroup()
		{
			DDL_TYPE.Items.Add(new ListItem("Sifat Kredit","1"));
			DDL_TYPE.Items.Add(new ListItem("Jenis Penggunaan","2"));
			DDL_TYPE.Items.Add(new ListItem("Sektor Ekonomi","3"));
			DDL_TYPE.Items.Add(new ListItem("Lokasi Proyek","4"));
			DDL_TYPE.Items.Add(new ListItem("Jenis Kredit","5"));
			DDL_TYPE.Items.Add(new ListItem("Orientasi Penggunaan","6"));
			DDL_TYPE.Items.Add(new ListItem("Fasilitas Penyediaan Dana","7"));
		}

		private void BindData1()
		{
			string grp = "";

			conn.QueryString = "select convert(int,BI_SEQ) as BI_SEQ, BI_DESC, BI_GROUP, CD_SIBS from RFBICODE where ACTIVE = '1' order by BI_SEQ";
			conn.ExecuteQuery();

			if (conn.GetRowCount() != 0)
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				DG1.DataSource = dt;

				try
				{
					DG1.DataBind();
				}
				catch 
				{
					DG1.CurrentPageIndex = DG1.PageCount - 1;
					DG1.DataBind();
				}

				for (int i = 0; i < DG1.Items.Count; i++)
				{
					grp = DG1.Items[i].Cells[2].Text;

					switch(grp)
					{
						case "1" :
							DG1.Items[i].Cells[3].Text = "Sifat Kredit";
							break;
						case "2" :
							DG1.Items[i].Cells[3].Text = "Jenis Penggunaan";
							break;
						case "3" :
							DG1.Items[i].Cells[3].Text = "Sektor Ekonomi";
							break;
						case "4" :
							DG1.Items[i].Cells[3].Text = "Lokasi Proyek";
							break;
						case "5" :
							DG1.Items[i].Cells[3].Text = "Jenis Kredit";
							break;
						case "6" :
							DG1.Items[i].Cells[3].Text = "Orientasi Penggunaan";
							break;
						case "7" :
							DG1.Items[i].Cells[3].Text = "Fasilitas Penyediaan Dana";
							break;
					}
				}
			} 

			conn.ClearData();
		}

		private void BindData2()
		{
			string gp = "";

			conn.QueryString = "select convert(int,BI_SEQ) as BI_SEQ, BI_DESC, BI_GROUP, CD_SIBS, CH_STA, "+
				"STATUS = case CH_STA when '1' then 'INSERT' " +
				"when '2' then 'UPDATE' "+
				"when '3' then 'DELETE' end "+
				"from TRFBICODE order by BI_SEQ";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG2.DataSource = dt;

			try
			{
				DG2.DataBind();
			}
			catch 
			{
				DG2.CurrentPageIndex = DG2.PageCount - 1;
				DG2.DataBind();
			}

			for (int i = 0; i < DG2.Items.Count; i++)
			{
				gp = DG2.Items[i].Cells[2].Text;

				switch(gp)
				{
					case "1" :
						DG2.Items[i].Cells[3].Text = "Sifat Kredit";
						break;
					case "2" :
						DG2.Items[i].Cells[3].Text = "Jenis Penggunaan";
						break;
					case "3" :
						DG2.Items[i].Cells[3].Text = "Sektor Ekonomi";
						break;
					case "4" :
						DG2.Items[i].Cells[3].Text = "Lokasi Proyek";
						break;
					case "5" :
						DG2.Items[i].Cells[3].Text = "Jenis Kredit";
						break;
					case "6" :
						DG2.Items[i].Cells[3].Text = "Orientasi Penggunaan";
						break;
					case "7" :
						DG2.Items[i].Cells[3].Text = "Fasilitas Penyediaan Dana";
						break;
				}
			}
		}

		private void ClearEditBoxes()
		{
			TXT_DESC.Text = "";
			TXT_SIBS.Text = "";
			DDL_TYPE.ClearSelection();
	
			LBL_SEQ.Text = ""; 
			LBL_SAVEMODE.Text = "1"; 
		}

		private string seq()
		{
			string seqid = "";
			int number = Int16.Parse(LBL_NB.Text);

			conn.QueryString = "SELECT max(convert(int,BI_SEQ))+ "+LBL_NB.Text+" as MAX from RFBICODE";
			
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0) 
				seqid = conn.GetFieldValue("MAX");
			else
				seqid = "0"; 

			conn.ClearData(); 

			number++;

			LBL_NB.Text = number.ToString();  
			return seqid;
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
			this.DG1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG1_ItemCommand);
			this.DG1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG1_PageIndexChanged);
			this.DG2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG2_ItemCommand);
			this.DG2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG2_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;
			string id = "";

			conn.QueryString = "SELECT BI_SEQ FROM TRFBICODE WHERE BI_SEQ = '"+LBL_SEQ.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "UPDATE TRFBICODE SET BI_DESC = "+GlobalTools.ConvertNull(TXT_DESC.Text)+", BI_GROUP = "+GlobalTools.ConvertNull(DDL_TYPE.SelectedValue)+", CD_SIBS = "+GlobalTools.ConvertNull(TXT_SIBS.Text)+" where BI_SEQ = '"+LBL_SEQ.Text+"'";  					
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "INSERT INTO TRFBICODE VALUES('"+LBL_SEQ.Text+"',"+GlobalTools.ConvertNull(TXT_DESC.Text)+","+GlobalTools.ConvertNull(DDL_TYPE.SelectedValue)+","+GlobalTools.ConvertNull(TXT_SIBS.Text)+",'2')";
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1")) 
			{
				id = seq();
 
				conn.QueryString = "INSERT INTO TRFBICODE VALUES('"+id+"',"+GlobalTools.ConvertNull(TXT_DESC.Text)+","+GlobalTools.ConvertNull(DDL_TYPE.SelectedValue)+","+GlobalTools.ConvertNull(TXT_SIBS.Text)+",'1')";
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit != 0) && (LBL_SAVEMODE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
				return;
			}
 
			conn.ClearData();
 	
			BindData2();

			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		private void DG1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG1.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 
		}

		private void DG1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string bicode, bidesc, bigrp, cds; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					bicode = e.Item.Cells[0].Text.Trim();
					bidesc = e.Item.Cells[1].Text.Trim();
					bigrp = e.Item.Cells[2].Text.Trim();
					cds = e.Item.Cells[4].Text.Trim();
 
					conn.QueryString = "SELECT BI_SEQ FROM TRFBICODE WHERE BI_SEQ = '"+bicode+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "INSERT INTO TRFBICODE VALUES('"+bicode+"',"+GlobalTools.ConvertNull(bidesc)+","+GlobalTools.ConvertNull(bigrp)+","+GlobalTools.ConvertNull(cds)+",'3')";
						conn.ExecuteQuery();
						BindData2();
					}
					break;
				case "edit":
					LBL_SEQ.Text = e.Item.Cells[0].Text.Trim();
					TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_SIBS.Text = cleansText(e.Item.Cells[4].Text);

					try
					{
						DDL_TYPE.SelectedValue = e.Item.Cells[2].Text.Trim();
					}
					catch{ }
   
					LBL_SAVEMODE.Text = "2";		
					break;
			}
		
		}

		private void DG2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM TRFBICODE WHERE BI_SEQ = '"+code+"'";
					conn.ExecuteQuery();
					BindData2();
					break;
				case "edit":
					if(e.Item.Cells[6].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						LBL_SEQ.Text = e.Item.Cells[0].Text.Trim();  
						TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
						TXT_SIBS.Text = cleansText(e.Item.Cells[4].Text);

						try
						{
							DDL_TYPE.SelectedValue = e.Item.Cells[2].Text.Trim();
						}
						catch{ }
   
						LBL_SAVEMODE.Text = "2";
					}
					break;
			}		
		}

		private void DG2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG2.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40"); 
		}
	}
}
