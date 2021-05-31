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
using System.Configuration;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.Area.Consumer
{
	/// <summary>
	/// Summary description for MRAgnecyOfficerParam.
	/// </summary>
	public partial class MRAgnecyOfficerParam : System.Web.UI.Page
	{
		protected string mid, name, pos, upline;
		protected Connection conn;
		protected Connection conn2; //= new Connection("Data Source=10.123.12.30 ;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp;Pooling=true");
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon();
			if(!IsPostBack)
			{
				TR_POSITION.Visible = false;
				TR_UPLINER.Visible = false;
				ViewData(); 
			}
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");
		}

		private void InitialCon()
		{
			conn2.QueryString = "select * from vw_getconn where moduleid = '"+Request.QueryString["ModuleId"]+"'";
			conn2.ExecuteQuery();
			string DB_NAMA = conn2.GetFieldValue("DB_NAMA");
			string DB_IP = conn2.GetFieldValue("DB_IP");
			string DB_LOGINID = conn2.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn2.GetFieldValue("DB_LOGINPWD");
			conn = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
			//conn = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=sa;pwd=dmscorp;Pooling=true");
		}

		private void ViewData()
		{
			string arid = (string) Session["AreaId"];
			string que = "";

			mid = Request.QueryString["ModuleId"];

			LBL_SAVE.Text = "1"; 

			que = "select CITY_ID, CITY_NAME from CITY";
			
			GlobalTools.fillRefList(DDL_CITY,que,false,conn);

			DDL_AGENCY.Items.Add(new ListItem("- SELECT -",""));
			DDL_POS.Items.Add(new ListItem("- SELECT -",""));
			DDL_UPLINER.Items.Add(new ListItem("- SELECT -",""));

			BindData1();
			BindData2();
		}

		private void BindData1()
		{
			conn.QueryString = "select a.*, b.agencyname, b.CITY_ID from RFAGENCYOFR a join TAGENCY b on a.AGENCYID = b.AGENCYID "+
				"where a.active='1' and a.AGENCYID = '"+DDL_AGENCY.SelectedValue+"' AND b.AGENCYTYPE = '7'";
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
			conn.QueryString = "select a.*, b.CITY_ID, b.agencyname,  "+ 
				"STATUS = CASE a.CH_STA WHEN '1' THEN 'INSERT' "+
				"WHEN '2' THEN 'UPDATE' WHEN '3' THEN 'DELETE' END "+
				"from TRFAGENCYOFR a join TAGENCY b on a.AGENCYID = b.AGENCYID AND b.AGENCYTYPE = '7' ";
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
			TXT_CODE.Text = "";
			TXT_NAME.Text = "";
		
			DDL_AGENCY.Enabled = true; 
			DDL_CITY.Enabled = true;
			TXT_CODE.Enabled = true;

			DDL_AGENCY.ClearSelection();
			DDL_UPLINER.ClearSelection(); 
			DDL_POS.ClearSelection();  
 
			LBL_SAVE.Text = "1";
		}

		private void fillAgen()
		{
			GlobalTools.fillRefList(DDL_AGENCY,"select AGENCYID, AGENCYNAME from TAGENCY where CITY_ID = '"+DDL_CITY.SelectedValue+"' and AGENCYTYPE = '7'",false,conn); 
		}

		private void fillPos()
		{
			DDL_POS.Items.Clear();
 
			DDL_POS.Items.Add(new ListItem("- SELECT -",""));   
			DDL_POS.Items.Add(new ListItem("Supervisor","Supervisor"));
			DDL_POS.Items.Add(new ListItem("Associate Sales Executive","Associate Sales Executive"));
			DDL_POS.Items.Add(new ListItem("Trainee","Trainee"));
			DDL_POS.Items.Add(new ListItem("Junior Sales Executive","Junior Sales Executive"));
			DDL_POS.Items.Add(new ListItem("Associate Sales Manager","Associate Sales Manager"));			
		}

		private void fillUpliner()
		{
			GlobalTools.fillRefList(DDL_UPLINER,"select AGOFR_CODE, AGOFR_DESC from RFAGENCYOFR where AGENCYID = '"+DDL_AGENCY.SelectedValue+"' and Position = 'Supervisor'",false,conn); 
		}

		private string createseq(string nb)
		{
			string temp = "";

			if(nb.Length == 1)
				temp = "000" + nb;
			else if(nb.Length == 2)
				temp = "00" + nb;
			else if(nb.Length == 3)
				temp = "0" + nb;
			else temp = nb;

			return temp;
		}

		private string seq()
		{
			string seqid = "";

			conn.QueryString = "select isnull(max(convert(int, AGOFR_CODE)), 0)+1 as MAX from RFAGENCYOFR "+
				"where AGENCYID = '"+DDL_AGENCY.SelectedValue+"'";
			
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0) 
				seqid = conn.GetFieldValue("MAX");
			else
				seqid = "0"; 

			conn.ClearData(); 
 
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
			this.DGR_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_ItemCommand);
			this.DGR_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

		}
		#endregion

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamAll.aspx?mc=9902040201&moduleID=40");
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string id = "", agenid = ""; 
			int hit = 0;

			conn.QueryString = "select * from TRFAGENCYOFR where AGENCYID = '"+DDL_AGENCY.SelectedValue+"'"+
				"and AGOFR_CODE = '"+TXT_CODE.Text+"'"; 
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVE.Text != "1"))
			{
				/*
				conn.QueryString = "update TRFAGENCYOFR set AGOFR_DESC = "+GlobalTools.ConvertNull(TXT_NAME.Text)+" "+
					"where AGENCYID = '"+DDL_AGENCY.SelectedValue+"' and AGOFR_CODE = '"+TXT_CODE.Text+"'"; 
				*/			
				conn.QueryString = "update TRFAGENCYOFR set AGOFR_DESC = "+GlobalTools.ConvertNull(TXT_NAME.Text)+",CH_STA ='2' "+
					"where AGENCYID = '"+DDL_AGENCY.SelectedValue+"' and AGOFR_CODE = '"+TXT_CODE.Text+"'"; 
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "2"))
			{
				/*
				conn.QueryString = "insert into TRFAGENCYOFR(AGENCYID, AGOFR_CODE, AGOFR_DESC, POSITION, UPLINER, CH_STA) "+ 
					"values ('"+DDL_AGENCY.SelectedValue+"', '"+TXT_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_NAME.Text)+","+
					" "+GlobalTools.ConvertNull(DDL_POS.SelectedValue)+", "+GlobalTools.ConvertNull(DDL_UPLINER.SelectedValue)+", 2)";
				*/
				conn.QueryString = "insert into TRFAGENCYOFR(AGENCYID, AGOFR_CODE, AGOFR_DESC, POSITION, UPLINER, CH_STA) "+ 
					"values ('"+DDL_AGENCY.SelectedValue+"', '"+TXT_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_NAME.Text)+","+
					" null, null, 2)";

				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "1"))
			{
				id = seq();
				agenid = createseq(id); 
				/*				
				conn.QueryString = "insert into TRFAGENCYOFR(AGENCYID, AGOFR_CODE, AGOFR_DESC, POSITION, UPLINER, CH_STA) "+ 
					"values ('"+DDL_AGENCY.SelectedValue+"', '"+TXT_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_NAME.Text)+","+
					" "+GlobalTools.ConvertNull(DDL_POS.SelectedValue)+", "+GlobalTools.ConvertNull(DDL_UPLINER.SelectedValue)+", 1)";
				*/
				conn.QueryString = "insert into TRFAGENCYOFR(AGENCYID, AGOFR_CODE, AGOFR_DESC, POSITION, UPLINER, CH_STA) "+ 
					"values ('"+DDL_AGENCY.SelectedValue+"', '"+TXT_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_NAME.Text)+","+
					"null,null,1)";

				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit != 0) && (LBL_SAVE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
				return;
			}

			conn.ClearData();
 	
			BindData2();

			LBL_SAVE.Text = "1"; 
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		protected void DDL_CITY_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillAgen();
		}

		protected void DDL_AGENCY_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillPos();
			fillUpliner();

			BindData1(); 
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 		
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string id, code, desc; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					id = e.Item.Cells[0].Text.Trim();
					code = e.Item.Cells[2].Text.Trim();
					desc = cleansText(e.Item.Cells[3].Text); 

					conn.QueryString = "select * from TRFAGENCYOFR where AGENCYID = '"+id+"'"+
						 "and AGOFR_CODE = '"+code+"'"; 
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "select * from RFAGENCYOFR where AGENCYID = '"+id+"' "+
							"and AGOFR_CODE = '"+code+"'"; 
						conn.ExecuteQuery();
						
						if(conn.GetRowCount() != 0) 
						{
							pos = conn.GetFieldValue(0,"POSITION"); 
							upline = conn.GetFieldValue(0,"UPLINER"); 

							try
							{
								/*
							    conn.QueryString = "insert into TRFAGENCYOFR(AGENCYID, AGOFR_CODE, AGOFR_DESC, POSITION, UPLINER, CH_STA) "+
									"values ('"+id+"', '"+code+"', "+GlobalTools.ConvertNull(desc)+", "+GlobalTools.ConvertNull(pos)+", "+GlobalTools.ConvertNull(upline)+", 3)";
								*/
								conn.QueryString = "insert into TRFAGENCYOFR(AGENCYID, AGOFR_CODE, AGOFR_DESC, POSITION, UPLINER, CH_STA) "+
									"values ('"+id+"', '"+code+"', "+GlobalTools.ConvertNull(desc)+",null,null, 3)";
								conn.ExecuteQuery();
							}
							catch{ }
						}

						BindData2();
					}
					break;

				case "edit":
					id = e.Item.Cells[0].Text.Trim();
					code = e.Item.Cells[2].Text.Trim();
					desc = cleansText(e.Item.Cells[3].Text); 

					conn.QueryString = "select a.*, b.CITY_ID from RFAGENCYOFR a join TAGENCY b on a.AGENCYID = b.AGENCYID "+
							"where a.AGENCYID = '"+id+"' and a.AGOFR_CODE = '"+code+"'"; 		
					
					conn.ExecuteQuery(); 
					
					if(conn.GetRowCount() != 0) 
					{
						pos = conn.GetFieldValue(0,"POSITION"); 
						upline = conn.GetFieldValue(0,"UPLINER"); 

						try
						{
							DDL_UPLINER.SelectedValue = upline.Trim();   
						}
						catch{ } 						

						try
						{
							DDL_POS.SelectedValue = pos.Trim();   
						}
						catch{ } 						
					}

					TXT_CODE.Text = code;
					TXT_NAME.Text = desc;

					try
					{
						DDL_CITY.SelectedValue = e.Item.Cells[7].Text.Trim();   
					}
					catch{ }

					try
					{
						DDL_AGENCY.SelectedValue = id;   
					}
					catch{ }
					
					DDL_CITY.Enabled = false;
					DDL_AGENCY.Enabled = false; 
					TXT_CODE.Enabled = false;

					LBL_SAVE.Text = "2";

					break;
			}		
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string id, code, desc; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":				
					id = e.Item.Cells[0].Text.Trim();
					code = e.Item.Cells[2].Text.Trim();

					conn.QueryString = "delete from TRFAGENCYOFR where AGENCYID = '"+id+"' "+
						"and AGOFR_CODE = '"+code+"'";
					
					conn.ExecuteQuery();

					BindData2();
					break;

				case "edit":
					id = e.Item.Cells[0].Text.Trim();
					code = e.Item.Cells[2].Text.Trim();
					desc = cleansText(e.Item.Cells[3].Text); 

					if(e.Item.Cells[7].Text.Trim() == "3")
					{
						LBL_SAVE.Text = "1";
					}
					else
					{
						conn.QueryString = "select a.*, b.CITY_ID from TRFAGENCYOFR a join TAGENCY b on a.AGENCYID = b.AGENCYID "+
							"where a.AGENCYID = '"+id+"' and a.AGOFR_CODE = '"+code+"'"; 
						conn.ExecuteQuery(); 
					
						if(conn.GetRowCount() != 0) 
						{
							pos = conn.GetFieldValue(0,"POSITION"); 
							upline = conn.GetFieldValue(0,"UPLINER");

							try
							{
								DDL_UPLINER.SelectedValue = upline.Trim();   
							}
							catch{ } 					
							
							try
							{
								DDL_POS.SelectedValue = pos.Trim();   
							}
							catch{ } 						
						}

						try
						{
							DDL_CITY.SelectedValue = e.Item.Cells[9].Text.Trim();   
						}
						catch{ }

						try
						{
							DDL_AGENCY.SelectedValue = id;   
						}
						catch{ }

						TXT_CODE.Text = code;
						TXT_NAME.Text = desc;

						DDL_CITY.Enabled = false;
						DDL_AGENCY.Enabled = false; 
						TXT_CODE.Enabled = false;
	
						LBL_SAVE.Text = "2";
					}
					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 			
		}
	}
}
