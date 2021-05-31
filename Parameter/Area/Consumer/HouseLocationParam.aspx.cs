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

namespace CuBES_Maintenance.Parameter.Area.Consumer
{
	/// <summary>
	/// Summary description for HouseLocationParam.
	/// </summary>
	public partial class HouseLocationParam : System.Web.UI.Page
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
			string arid = (string) Session["AreaId"];
			string que = "";
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			LBL_SAVEMODE.Text = "1"; 

			InitialCon(); 

			que = "select CITY_ID, CITY_NAME from CITY";
			
			GlobalTools.fillRefList(DDL_CITY,que,false,conn);

			BindData1();
			BindData2();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void CodeSeq(string mode)
		{
			string seq = "";
			int number = Int16.Parse(LBL_NB.Text);

			LBL_NB.Text = number.ToString();

			if(mode == "1")
			{
				number++;
			}

			LBL_NB.Text = number.ToString();

			conn.QueryString = "select isnull(max(convert(int, ID_LOKASI)), 0)+ "+LBL_NB.Text+" as MAXSEQ from LOKASI_PERUMAHAN";
			conn.ExecuteQuery();

			seq = conn.GetFieldValue("MAXSEQ").Trim();  

			if(seq.Length == 1)
				seq = "000" + seq;
			else if(seq.Length == 2)
				seq = "00" + seq;
			else if(seq.Length == 3)
				seq = "0" + seq;

			TXT_CODE.Text = seq;  

			conn.ClearData(); 

		}

		private void BindData1()
		{	
			conn.QueryString = "select ID_LOKASI, ID_KOTA, CITY_NAME, LOKASI from LOKASI_PERUMAHAN "+
				"left join CITY on LOKASI_PERUMAHAN.ID_KOTA = CITY.CITY_ID "+
				"where LOKASI_PERUMAHAN.active='1' and ID_KOTA = '"+DDL_CITY.SelectedValue+"'";

			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_EXISTING.DataSource = dt;

			try
			{
				DGR_EXISTING.DataBind();
			}
			catch 
			{
				DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.PageCount - 1;
				DGR_EXISTING.DataBind();
			}

			conn.ClearData();
		}

		private void BindData2()
		{
			conn.QueryString = "select ID_LOKASI, ID_KOTA, CITY_NAME, LOKASI, CH_STA, "+
				"STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' when '3' "+
				"then 'DELETE' end from TLOKASI_PERUMAHAN "+
				"left join CITY on TLOKASI_PERUMAHAN.ID_KOTA = CITY.CITY_ID ";

			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_REQUEST.DataSource = dt;

			try
			{
				DGR_REQUEST.DataBind();
			}
			catch 
			{
				DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				DGR_REQUEST.DataBind();
			}
		}
		
		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ClearEditBoxes()
		{
			DDL_CITY.Enabled = true;
			TXT_CODE.Enabled = true; 

			//TXT_CODE.Text = ""; 
			TXT_LOCATION.Text = "";
				 
			LBL_SAVEMODE.Text = "1";
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
			this.DGR_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_ItemCommand);
			this.DGR_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;

			if(TXT_CODE.Text.Length != 4)
			{
				GlobalTools.popMessage(this,"Location Code must 4 digits in length!");
				return;
			}

			conn.QueryString = "SELECT * FROM TLOKASI_PERUMAHAN WHERE ID_LOKASI = '"+TXT_CODE.Text+"' "+
					"AND ID_KOTA = '"+DDL_CITY.SelectedValue+"'"; 
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "UPDATE TLOKASI_PERUMAHAN SET LOKASI = "+GlobalTools.ConvertNull(TXT_LOCATION.Text);

				conn.ExecuteQuery();

				ClearEditBoxes(); 

				CodeSeq("0"); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "INSERT INTO TLOKASI_PERUMAHAN VALUES('"+TXT_CODE.Text+"','"+DDL_CITY.SelectedValue+"',"+GlobalTools.ConvertNull(TXT_LOCATION.Text)+",'2')";

				conn.ExecuteQuery();
				
				ClearEditBoxes(); 

				CodeSeq("0");
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				conn.QueryString = "INSERT INTO TLOKASI_PERUMAHAN VALUES('"+TXT_CODE.Text+"','"+DDL_CITY.SelectedValue+"',"+GlobalTools.ConvertNull(TXT_LOCATION.Text)+",'1')";

				try
				{
					conn.ExecuteQuery();
					CodeSeq("1");
				}
				catch
				{
					GlobalTools.popMessage(this,"Cannot insert same code, request canceled!"); 
				}
				finally
				{
					ClearEditBoxes(); 
				}
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleID=40");	
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 	
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string locid, cid, lokasi; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					locid = e.Item.Cells[0].Text.Trim();
					cid = e.Item.Cells[1].Text.Trim();
					lokasi = cleansText(e.Item.Cells[3].Text);

					conn.QueryString = "SELECT * FROM TLOKASI_PERUMAHAN WHERE ID_LOKASI = '"+locid+"' AND ID_KOTA = '"+cid+"'"; 
						
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						try
						{
							conn.QueryString = "INSERT INTO TLOKASI_PERUMAHAN VALUES('"+locid+"','"+cid+"',"+GlobalTools.ConvertNull(lokasi)+",'3')";
								
							conn.ExecuteQuery();
						}
						catch{ }

						BindData2();
					}
					break;

				case "edit":
					locid = e.Item.Cells[0].Text.Trim();
					cid = e.Item.Cells[1].Text.Trim();
					lokasi = cleansText(e.Item.Cells[3].Text);
						
					try
					{
						DDL_CITY.SelectedValue = cid;   
					}
					catch{ }

					TXT_CODE.Text = locid;
					TXT_LOCATION.Text = lokasi; 

					LBL_SAVEMODE.Text = "2";
		
					DDL_CITY.Enabled = false;
					TXT_CODE.Enabled = false;
					
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string locid, cid, lokasi;  

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					locid = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM TLOKASI_PERUMAHAN WHERE ID_LOKASI = '"+locid+"'";
					
					conn.ExecuteQuery();

					BindData2();
					break;
				case "edit":
					locid = e.Item.Cells[0].Text.Trim();
					cid = e.Item.Cells[1].Text.Trim();
					lokasi = cleansText(e.Item.Cells[3].Text);

					if(e.Item.Cells[5].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						try
						{
							DDL_CITY.SelectedValue = cid;   
						}
						catch{ }

						TXT_CODE.Text = locid;
						TXT_LOCATION.Text = lokasi; 

						LBL_SAVEMODE.Text = "2";
		
						DDL_CITY.Enabled = false;
						TXT_CODE.Enabled = false;						
					}

					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 			
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		protected void DDL_CITY_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1(); 

			CodeSeq("0"); 
		}
	}
}
