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
	/// Summary description for DeveloperParam.
	/// </summary>
	public partial class DeveloperParam : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid, addr1, addr2, addr3;
		protected string city, zipcode, ph1, ph2, ph3;
		protected string fn1, fn2, cid, kerjasama, blocked;
		protected string grline, totalgr, intsub, srccode, cdsibs;
		protected string grpdev, pksdate, expdate, noakta, notaris;
	
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

			if(arid == "2000")
			{
				que = "select CITY_ID, CITY_NAME from CITY where AREA_ID = '"+arid+"'";
			}
			else
			{
				que = "select CITY_ID, CITY_NAME from CITY";
			}
			
			GlobalTools.fillRefList(DDL_CITY,que,false,conn);

			GlobalTools.fillRefList(DDL_GRPDEV,"select GDV_CODE, GDV_NAME from GROUPDEVELOPER",false,conn);

			GlobalTools.initDateForm(TXT_PKSDATE,DDL_PKSMONTH,TXT_PKSYEAR);
			GlobalTools.initDateForm(TXT_EXPDATE,DDL_EXPMONTH,TXT_EXPYEAR);

			BindData1();
			BindData2();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData1()
		{
			//20070712 remark by sofyan, for developer enhancement
			//conn.QueryString = "select distinct dv_code, dv_name, dv_srccode, case dv_kerjasama when '1' then 'Yes' when '0' then 'No' end DV_KERJASAMA, "+
			//	"case dv_blocked when '1' then 'Yes' else 'No' end DV_BLOCKED, "+
			//	"isnull(DV_GRLINE, 0) DV_GRLINE, (isnull(DV_GRLINE, 0) - isnull(DV_TOTGRLINE, 0)) as SISA, CD_SIBS "+
			//	"from developer " + //"left join proyek_housingloan on developer.dv_code = proyek_housingloan.developer_id "+
			//	"where developer.active='1' and developer.CITY_ID = '"+DDL_CITY.SelectedValue+"' order by dv_code"; 
			conn.QueryString = "exec PARAM_DEVELOPER_VIEWEXISTING '" + DDL_CITY.SelectedValue + "', '" + DDL_GRPDEV.SelectedValue + "'";
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
			
			for (int i = 0; i < DGR_EXISTING.Items.Count; i++)
				DGR_EXISTING.Items[i].Cells[0].Text = (i+1+(DGR_EXISTING.CurrentPageIndex*DGR_EXISTING.PageSize)).ToString();

			conn.ClearData();
		}

		private void BindData2()
		{
			//20070712 remark by sofyan, for developer enhancement
			//conn.QueryString = "select dv_code, dv_name, dv_srccode, "+
			//	"case dv_kerjasama when '1' then 'Yes' when '0' then 'No' end DV_KERJASAMA, "+ 
			//	"case dv_blocked when '1' then 'Yes' else 'No' end DV_BLOCKED, "+
			//	"isnull(DV_GRLINE, 0) DV_GRLINE, (isnull(DV_GRLINE, 0) - isnull(DV_TOTGRLINE, 0)) as SISA, "+
			//	"CH_STA, STATUS = case CH_STA when '1' then 'INSERT' when '2' then 'UPDATE' "+
			//	"when 3 then 'DELETE' end, CD_SIBS from tdeveloper "+ 
			//	//"left join proyek_housingloan on tdeveloper.dv_code = proyek_housingloan.developer_id "+ 
			//	"order by dv_code";
			conn.QueryString = "exec PARAM_DEVELOPER_VIEWREQUEST '" + DDL_CITY.SelectedValue + "', '" + DDL_GRPDEV.SelectedValue + "'";
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

			for (int i = 0; i < DGR_REQUEST.Items.Count; i++)
				DGR_REQUEST.Items[i].Cells[0].Text = (i+1+(DGR_REQUEST.CurrentPageIndex*DGR_REQUEST.PageSize)).ToString();
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ClearEditBoxes()
		{
			TXT_DEV_NAME.Text = "";
			TXT_GRLINE.Text = "0";
			TXT_INSUB.Text = "0";
			TXT_REMGRLINE.Text = "0";
			TXT_ZIPCODE.Text = "";
			TXT_ADDR1.Text = "";
			TXT_ADDR2.Text = "";
			TXT_ADDR3.Text = "";
			TXT_ADDR4.Text = "";
			
			TXT_FN1.Text = "";
			TXT_FN2.Text = "";
			
			TXT_PH1.Text = "";
			TXT_PH2.Text = "";
			TXT_PH3.Text = "";

			TXT_MKTSC.Text = "";

			TXT_CDSIBS.Text = "";
 
			CHK_BLOCK.Checked = false;
			//CHK_KERJASAMA.Checked = false;
			RDB_KERJASAMA.SelectedValue = "0";
 
			DDL_CITY.Enabled = true;
			
			LBL_SAVEMODE.Text = "1";
			LBL_DEV_CODE.Text = "";

			TXT_PKSDATE.Text = "";
			try {this.DDL_PKSMONTH.SelectedItem.Selected = false;} 
			catch {}
			TXT_PKSYEAR.Text = "";
			TXT_EXPDATE.Text = "";
			try {this.DDL_EXPMONTH.SelectedItem.Selected = false;} 
			catch {}
			TXT_EXPYEAR.Text = "";
			TXT_NOAKTA.Text = "";
			TXT_NOTARIS.Text = "";
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
			int number = Int16.Parse(LBL_NB.Text);

			conn.QueryString = "select isnull(max(convert(int, DV_CODE)), 0)+ "+LBL_NB.Text+" as MAXSEQ from DEVELOPER";
			
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0) 
				seqid = conn.GetFieldValue("MAXSEQ");
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
			this.DGR_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_ItemCommand);
			this.DGR_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string id = "", dvcode = "", ksm, blk; 
			int hit = 0;
			double togrline = 0;
			string grline = "", remgrline = "";

			//if(CHK_KERJASAMA.Checked)
			//	ksm = "1";
			//else
			//	ksm = "0";
			if(RDB_KERJASAMA.SelectedValue == "1")
				ksm = "1";
			else
				ksm = "0";

			if(CHK_BLOCK.Checked)
				blk = "1";
			else
				blk = "0";

			if(TXT_GRLINE.Text == "")
				grline = "0";

			if(TXT_REMGRLINE.Text == "")
				remgrline = "0";

			grline = TXT_GRLINE.Text.Replace(".","").Trim();
			remgrline = TXT_REMGRLINE.Text.Replace(".","").Trim();

			togrline = Double.Parse(grline.Replace(",00","").Trim()) - Double.Parse(remgrline.Replace(",00","").Trim());  

			conn.QueryString = "SELECT * FROM TDEVELOPER WHERE DV_CODE = '"+LBL_DEV_CODE.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				/*20070713 remark by sofyan for developer enhancement
				conn.QueryString = "UPDATE TDEVELOPER SET DV_NAME = "+GlobalTools.ConvertNull(TXT_DEV_NAME.Text)+", "+
					"DV_ADDR1 = "+GlobalTools.ConvertNull(TXT_ADDR1.Text)+", "+ 
					"DV_ADDR2 = "+GlobalTools.ConvertNull(TXT_ADDR2.Text)+", "+
					"DV_ADDR3 = "+GlobalTools.ConvertNull(TXT_ADDR3.Text)+", "+
					"DV_CITY = "+GlobalTools.ConvertNull(TXT_ADDR4.Text)+", "+ 
					"DV_ZIPCODE = "+GlobalTools.ConvertNull(TXT_ZIPCODE.Text)+", "+
					"DV_PHNAREA = "+GlobalTools.ConvertNull(TXT_PH1.Text)+", "+ 
					"DV_PHNNUM = "+GlobalTools.ConvertNull(TXT_PH2.Text)+", "+
					"DV_PHNEXT = "+GlobalTools.ConvertNull(TXT_PH3.Text)+", "+
					"DV_FAXAREA = "+GlobalTools.ConvertNull(TXT_FN1.Text)+", "+ 
					"DV_FAXNUM = "+GlobalTools.ConvertNull(TXT_FN2.Text)+", "+
					"CITY_ID = "+GlobalTools.ConvertNull(DDL_CITY.SelectedValue)+", "+
					"DV_GRLINE = "+GlobalTools.ConvertFloat(TXT_GRLINE.Text)+", "+
					"DV_TOTGRLINE = "+togrline+", "+
					"DV_INTSUB = "+GlobalTools.ConvertFloat(TXT_INSUB.Text)+", "+
					"DV_SRCCODE = "+GlobalTools.ConvertNull(TXT_MKTSC.Text)+", "+
					"DV_KERJASAMA = '"+ksm+"', "+
					"DV_BLOCKED = '"+blk+"', "+
					"CD_SIBS = '"+TXT_CDSIBS.Text+"' "+
					"WHERE DV_CODE = '"+LBL_DEV_CODE.Text+"'";
				*/	
				conn.QueryString = "exec PARAM_DEVELOPER_MAKERUPDATE 2, " +
					"'" + LBL_DEV_CODE.Text + "', " +
					GlobalTools.ConvertNull(TXT_DEV_NAME.Text) + ", " +
					GlobalTools.ConvertNull(TXT_ADDR1.Text) + ", " + 
					GlobalTools.ConvertNull(TXT_ADDR2.Text) + ", " +
					GlobalTools.ConvertNull(TXT_ADDR3.Text) + ", " +
					GlobalTools.ConvertNull(TXT_ADDR4.Text) + ", " +
					GlobalTools.ConvertNull(TXT_ZIPCODE.Text) + ", " +
					GlobalTools.ConvertNull(TXT_PH1.Text) + ", " + 
					GlobalTools.ConvertNull(TXT_PH2.Text) + ", " +
					GlobalTools.ConvertNull(TXT_PH3.Text) + ", " +
					GlobalTools.ConvertNull(TXT_FN1.Text) + ", " + 
					GlobalTools.ConvertNull(TXT_FN2.Text) + ", " +
					GlobalTools.ConvertNull(DDL_CITY.SelectedValue) + ", " +
					"'" + TXT_CDSIBS.Text + "', " +
					GlobalTools.ConvertFloat(TXT_GRLINE.Text) + ", " +
					togrline + ", " +
					GlobalTools.ConvertFloat(TXT_INSUB.Text) + ", " +
					"1, NULL, NULL, " +
					GlobalTools.ConvertNull(TXT_MKTSC.Text) + ", " +
					"'" + ksm + "', " +
					"'" + blk + "', " +
					GlobalTools.ConvertNull(DDL_GRPDEV.SelectedValue) + ", " +
					GlobalTools.ToSQLDate(TXT_PKSDATE.Text,DDL_PKSMONTH.SelectedValue,TXT_PKSYEAR.Text) + ", " +
					GlobalTools.ToSQLDate(TXT_EXPDATE.Text,DDL_EXPMONTH.SelectedValue,TXT_EXPYEAR.Text) + ", " +
					GlobalTools.ConvertNull(TXT_NOAKTA.Text) + ", " +
					GlobalTools.ConvertNull(TXT_NOTARIS.Text);
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				/*20070713 remark by sofyan for developer enhancement
				conn.QueryString = "insert into TDEVELOPER values "+
					"('"+LBL_DEV_CODE.Text+"',"+GlobalTools.ConvertNull(TXT_DEV_NAME.Text)+","+GlobalTools.ConvertNull(TXT_ADDR1.Text)+
					","+GlobalTools.ConvertNull(TXT_ADDR2.Text)+","+GlobalTools.ConvertNull(TXT_ADDR3.Text)+
					","+GlobalTools.ConvertNull(TXT_ADDR4.Text)+","+GlobalTools.ConvertNull(TXT_ZIPCODE.Text)+
					","+GlobalTools.ConvertNull(TXT_PH1.Text)+","+GlobalTools.ConvertNull(TXT_PH2.Text)+
					","+GlobalTools.ConvertNull(TXT_PH3.Text)+","+GlobalTools.ConvertNull(TXT_FN1.Text)+
					","+GlobalTools.ConvertNull(TXT_FN2.Text)+","+GlobalTools.ConvertNull(DDL_CITY.SelectedValue)+
					",'"+TXT_CDSIBS.Text+"',"+GlobalTools.ConvertFloat(TXT_GRLINE.Text)+","+togrline+
					","+GlobalTools.ConvertFloat(TXT_INSUB.Text)+", 2, null, null, "+GlobalTools.ConvertNull(TXT_MKTSC.Text)+
					","+GlobalTools.ConvertNull(ksm)+","+GlobalTools.ConvertNull(blk)+")";
				*/
				conn.QueryString = "exec PARAM_DEVELOPER_MAKERUPDATE 1, " +
					"'" + LBL_DEV_CODE.Text + "', " +
					GlobalTools.ConvertNull(TXT_DEV_NAME.Text) + ", " +
					GlobalTools.ConvertNull(TXT_ADDR1.Text) + ", " + 
					GlobalTools.ConvertNull(TXT_ADDR2.Text) + ", " +
					GlobalTools.ConvertNull(TXT_ADDR3.Text) + ", " +
					GlobalTools.ConvertNull(TXT_ADDR4.Text) + ", " +
					GlobalTools.ConvertNull(TXT_ZIPCODE.Text) + ", " +
					GlobalTools.ConvertNull(TXT_PH1.Text) + ", " + 
					GlobalTools.ConvertNull(TXT_PH2.Text) + ", " +
					GlobalTools.ConvertNull(TXT_PH3.Text) + ", " +
					GlobalTools.ConvertNull(TXT_FN1.Text) + ", " + 
					GlobalTools.ConvertNull(TXT_FN2.Text) + ", " +
					GlobalTools.ConvertNull(DDL_CITY.SelectedValue) + ", " +
					"'" + TXT_CDSIBS.Text + "', " +
					GlobalTools.ConvertFloat(TXT_GRLINE.Text) + ", " +
					togrline + ", " +
					GlobalTools.ConvertFloat(TXT_INSUB.Text) + ", " +
					"2, NULL, NULL, " +
					GlobalTools.ConvertNull(TXT_MKTSC.Text) + ", " +
					"'" + ksm + "', " +
					"'" + blk + "', " +
					GlobalTools.ConvertNull(DDL_GRPDEV.SelectedValue) + ", " +
					GlobalTools.ToSQLDate(TXT_PKSDATE.Text,DDL_PKSMONTH.SelectedValue,TXT_PKSYEAR.Text) + ", " +
					GlobalTools.ToSQLDate(TXT_EXPDATE.Text,DDL_EXPMONTH.SelectedValue,TXT_EXPYEAR.Text) + ", " +
					GlobalTools.ConvertNull(TXT_NOAKTA.Text) + ", " +
					GlobalTools.ConvertNull(TXT_NOTARIS.Text);
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				id = seq();
				dvcode = createseq(id); 

				/*20070713 remark by sofyan for developer enhancement
				conn.QueryString = "insert into TDEVELOPER values "+
					"('"+dvcode+"',"+GlobalTools.ConvertNull(TXT_DEV_NAME.Text)+","+GlobalTools.ConvertNull(TXT_ADDR1.Text)+
					","+GlobalTools.ConvertNull(TXT_ADDR2.Text)+","+GlobalTools.ConvertNull(TXT_ADDR3.Text)+
					","+GlobalTools.ConvertNull(TXT_ADDR4.Text)+","+GlobalTools.ConvertNull(TXT_ZIPCODE.Text)+
					","+GlobalTools.ConvertNull(TXT_PH1.Text)+","+GlobalTools.ConvertNull(TXT_PH2.Text)+
					","+GlobalTools.ConvertNull(TXT_PH3.Text)+","+GlobalTools.ConvertNull(TXT_FN1.Text)+
					","+GlobalTools.ConvertNull(TXT_FN2.Text)+","+GlobalTools.ConvertNull(DDL_CITY.SelectedValue)+
					",'"+TXT_CDSIBS.Text+"',"+GlobalTools.ConvertFloat(TXT_GRLINE.Text)+","+togrline+
					","+GlobalTools.ConvertFloat(TXT_INSUB.Text)+", 1, null, null, "+GlobalTools.ConvertNull(TXT_MKTSC.Text)+
					","+GlobalTools.ConvertNull(ksm)+","+GlobalTools.ConvertNull(blk)+")";
				*/
				conn.QueryString = "exec PARAM_DEVELOPER_MAKERUPDATE 1, " +
					"'" + dvcode + "', " +
					GlobalTools.ConvertNull(TXT_DEV_NAME.Text) + ", " +
					GlobalTools.ConvertNull(TXT_ADDR1.Text) + ", " + 
					GlobalTools.ConvertNull(TXT_ADDR2.Text) + ", " +
					GlobalTools.ConvertNull(TXT_ADDR3.Text) + ", " +
					GlobalTools.ConvertNull(TXT_ADDR4.Text) + ", " +
					GlobalTools.ConvertNull(TXT_ZIPCODE.Text) + ", " +
					GlobalTools.ConvertNull(TXT_PH1.Text) + ", " + 
					GlobalTools.ConvertNull(TXT_PH2.Text) + ", " +
					GlobalTools.ConvertNull(TXT_PH3.Text) + ", " +
					GlobalTools.ConvertNull(TXT_FN1.Text) + ", " + 
					GlobalTools.ConvertNull(TXT_FN2.Text) + ", " +
					GlobalTools.ConvertNull(DDL_CITY.SelectedValue) + ", " +
					"'" + TXT_CDSIBS.Text + "', " +
					GlobalTools.ConvertFloat(TXT_GRLINE.Text) + ", " +
					togrline + ", " +
					GlobalTools.ConvertFloat(TXT_INSUB.Text) + ", " +
					"1, NULL, NULL, " +
					GlobalTools.ConvertNull(TXT_MKTSC.Text) + ", " +
					"'" + ksm + "', " +
					"'" + blk + "', " +
					GlobalTools.ConvertNull(DDL_GRPDEV.SelectedValue) + ", " +
					GlobalTools.ToSQLDate(TXT_PKSDATE.Text,DDL_PKSMONTH.SelectedValue,TXT_PKSYEAR.Text) + ", " +
					GlobalTools.ToSQLDate(TXT_EXPDATE.Text,DDL_EXPMONTH.SelectedValue,TXT_EXPYEAR.Text) + ", " +
					GlobalTools.ConvertNull(TXT_NOAKTA.Text) + ", " +
					GlobalTools.ConvertNull(TXT_NOTARIS.Text);
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
			LBL_DEV_CODE.Text = "";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
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
			string code, name;
			double remain = 0;

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[1].Text.Trim();
					name = cleansText(e.Item.Cells[2].Text); 

					conn.QueryString = "SELECT * FROM TDEVELOPER WHERE DV_CODE = '"+code+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "SELECT * FROM DEVELOPER WHERE DV_CODE = '"+code+"'";
						conn.ExecuteQuery();
						
						if(conn.GetRowCount() != 0) 
						{
							addr1 = conn.GetFieldValue(0,"DV_ADDR1");
							addr2 = conn.GetFieldValue(0,"DV_ADDR2");
							addr3 = conn.GetFieldValue(0,"DV_ADDR3");	
							
							city =  conn.GetFieldValue(0,"DV_CITY");
							
							zipcode = conn.GetFieldValue(0,"DV_ZIPCODE"); 
							
							fn1 = conn.GetFieldValue(0,"DV_FAXAREA");
							fn2 = conn.GetFieldValue(0,"DV_FAXNUM"); 
							
							ph1 = conn.GetFieldValue(0,"DV_PHNAREA"); 
							ph2 = conn.GetFieldValue(0,"DV_PHNNUM"); 
							ph3 = conn.GetFieldValue(0,"DV_PHNEXT"); 
							
							cid = conn.GetFieldValue(0,"CITY_ID");
							intsub = conn.GetFieldValue(0,"DV_INTSUB");

							grline = conn.GetFieldValue(0,"DV_GRLINE");
							totalgr = conn.GetFieldValue(0,"DV_TOTGRLINE");
							srccode = conn.GetFieldValue(0,"DV_SRCCODE");
							kerjasama = conn.GetFieldValue(0,"DV_KERJASAMA");
							blocked = conn.GetFieldValue(0,"DV_BLOCKED"); 

							/*12-08-2005 tambahan untuk CD SIBS*/
							cdsibs = conn.GetFieldValue(0,"CD_SIBS");
							/******edit by feby*****************/

							grpdev = conn.GetFieldValue(0,"GDV_CODE");
							pksdate = conn.GetFieldValue(0,"DV_PKSDATE");
							expdate = conn.GetFieldValue(0,"DV_EXPDATE");
							noakta = conn.GetFieldValue(0,"DV_NOAKTA");
							notaris = conn.GetFieldValue(0,"DV_NOTARIS");

							try
							{
								/*20070713 remark by sofyan for developer enhancement
								conn.QueryString = "insert into TDEVELOPER values "+
									"('"+code+"',"+GlobalTools.ConvertNull(name)+","+GlobalTools.ConvertNull(addr1)+
									","+GlobalTools.ConvertNull(addr2)+","+GlobalTools.ConvertNull(addr3)+
									","+GlobalTools.ConvertNull(city)+","+GlobalTools.ConvertNull(zipcode)+
									","+GlobalTools.ConvertNull(ph1)+","+GlobalTools.ConvertNull(ph2)+
									","+GlobalTools.ConvertNull(ph3)+","+GlobalTools.ConvertNull(fn1)+
									","+GlobalTools.ConvertNull(fn2)+","+GlobalTools.ConvertNull(cid)+
									",'"+cdsibs+"',"+GlobalTools.ConvertFloat(grline)+","+GlobalTools.ConvertFloat(totalgr)+
									","+GlobalTools.ConvertFloat(intsub)+", 3, null, null, "+GlobalTools.ConvertNull(srccode)+
									","+GlobalTools.ConvertNull(kerjasama)+","+GlobalTools.ConvertNull(blocked)+")";
								*/
								conn.QueryString = "exec PARAM_DEVELOPER_MAKERUPDATE 1, " +
									"'" + code + "', " +
									GlobalTools.ConvertNull(name) + ", " +
									GlobalTools.ConvertNull(addr1) + ", " + 
									GlobalTools.ConvertNull(addr2) + ", " +
									GlobalTools.ConvertNull(addr3) + ", " +
									GlobalTools.ConvertNull(city) + ", " +
									GlobalTools.ConvertNull(zipcode) + ", " +
									GlobalTools.ConvertNull(ph1) + ", " + 
									GlobalTools.ConvertNull(ph2) + ", " +
									GlobalTools.ConvertNull(ph3) + ", " +
									GlobalTools.ConvertNull(fn1) + ", " + 
									GlobalTools.ConvertNull(fn2) + ", " +
									GlobalTools.ConvertNull(cid) + ", " +
									"'" + cdsibs + "', " +
									GlobalTools.ConvertFloat(grline) + ", " +
									GlobalTools.ConvertFloat(totalgr) + ", " +
									GlobalTools.ConvertFloat(intsub) + ", " +
									"3, NULL, NULL, " +
									GlobalTools.ConvertNull(srccode) + ", " +
									GlobalTools.ConvertNull(kerjasama) + ", " +
									GlobalTools.ConvertNull(blocked) + ", " +
									GlobalTools.ConvertNull(grpdev) + ", " +
									GlobalTools.ToSQLDate(pksdate) + ", " +
									GlobalTools.ToSQLDate(expdate) + ", " +
									GlobalTools.ConvertNull(noakta) + ", " +
									GlobalTools.ConvertNull(notaris);
								conn.ExecuteQuery();
							}
							catch{ }
						}

						BindData2();
					}
					break;

				case "edit":
					code = e.Item.Cells[1].Text.Trim();
					name = cleansText(e.Item.Cells[2].Text); 
					LBL_DEV_CODE.Text = e.Item.Cells[1].Text.Trim();  

					conn.QueryString = "SELECT * FROM DEVELOPER WHERE DV_CODE = '"+code+"'";
					conn.ExecuteQuery(); 
					
					if(conn.GetRowCount() != 0) 
					{
						TXT_ADDR1.Text = conn.GetFieldValue(0,"DV_ADDR1");
						TXT_ADDR2.Text = conn.GetFieldValue(0,"DV_ADDR2");
						TXT_ADDR3.Text = conn.GetFieldValue(0,"DV_ADDR3");	
							
						TXT_ADDR4.Text =  conn.GetFieldValue(0,"DV_CITY");
							
						TXT_ZIPCODE.Text = conn.GetFieldValue(0,"DV_ZIPCODE"); 
							
						TXT_FN1.Text = conn.GetFieldValue(0,"DV_FAXAREA");
						TXT_FN2.Text = conn.GetFieldValue(0,"DV_FAXNUM"); 
							
						TXT_PH1.Text = conn.GetFieldValue(0,"DV_PHNAREA"); 
						TXT_PH2.Text = conn.GetFieldValue(0,"DV_PHNNUM"); 
						TXT_PH3.Text = conn.GetFieldValue(0,"DV_PHNEXT"); 

						/****19 April 2005 edited by Rahma****/
						double dv_grline,dv_totgrline;
						try 
						{
							dv_grline = Double.Parse(conn.GetFieldValue(0,"DV_GRLINE"));
						} 
						catch { dv_grline=0;}
						try 
						{
							dv_totgrline = Double.Parse(conn.GetFieldValue(0,"DV_TOTGRLINE"));
						} 
						catch {dv_totgrline=0;}
											
						remain = dv_grline - dv_totgrline;  

						/*******end edited by Rahma***********/

						TXT_INSUB.Text = GlobalTools.MoneyFormat(conn.GetFieldValue(0,"DV_INTSUB"));
						TXT_GRLINE.Text = GlobalTools.MoneyFormat(conn.GetFieldValue(0,"DV_GRLINE"));
						TXT_REMGRLINE.Text = GlobalTools.MoneyFormat(remain.ToString());  
						
						TXT_MKTSC.Text = conn.GetFieldValue(0,"DV_SRCCODE");

						/*12-08-2005 tambahan untuk CD SIBS*/

						TXT_CDSIBS.Text = conn.GetFieldValue(0,"CD_SIBS");

						/******edited by feby*****************/
							
						//if(conn.GetFieldValue(0,"DV_KERJASAMA") == "1")
						//	CHK_KERJASAMA.Checked = true;
						//else
						//	CHK_KERJASAMA.Checked = false;
						RDB_KERJASAMA.SelectedValue = conn.GetFieldValue(0,"DV_KERJASAMA");

						if(conn.GetFieldValue(0,"DV_BLOCKED") == "1")
							CHK_BLOCK.Checked = true;
						else
							CHK_BLOCK.Checked = false;
 
						try
						{
							DDL_CITY.SelectedValue = conn.GetFieldValue(0,"CITY_ID");   
						}
						catch{ }

						try
						{
							DDL_GRPDEV.SelectedValue = conn.GetFieldValue(0,"GDV_CODE");   
						}
						catch{ }

						TXT_PKSDATE.Text = GlobalTools.FormatDate_Day(conn.GetFieldValue(0,"DV_PKSDATE"));
						try
						{
							DDL_PKSMONTH.SelectedValue = GlobalTools.FormatDate_Month(conn.GetFieldValue(0,"DV_PKSDATE"));
						}
						catch{ }
						TXT_PKSYEAR.Text = GlobalTools.FormatDate_Year(conn.GetFieldValue(0,"DV_PKSDATE"));

						TXT_EXPDATE.Text = GlobalTools.FormatDate_Day(conn.GetFieldValue(0,"DV_EXPDATE"));
						try
						{
							DDL_EXPMONTH.SelectedValue = GlobalTools.FormatDate_Month(conn.GetFieldValue(0,"DV_EXPDATE"));
						}
						catch{ }
						TXT_EXPYEAR.Text = GlobalTools.FormatDate_Year(conn.GetFieldValue(0,"DV_EXPDATE"));

						TXT_NOAKTA.Text = conn.GetFieldValue(0,"DV_NOAKTA");
						TXT_NOTARIS.Text = conn.GetFieldValue(0,"DV_NOTARIS");
					}

					TXT_DEV_NAME.Text = cleansText(name); 

					//DDL_CITY.Enabled = false;

					LBL_SAVEMODE.Text = "2";

					break;
				case "view":
					code = e.Item.Cells[1].Text.Trim();
					mid = Request.QueryString["ModuleId"];

					Response.Write("<script language='javascript'>window.open('DetailDeveloperParam.aspx?ModuleId="+mid+
						"&dvcode="+code+"&cityid="+DDL_CITY.SelectedValue+"','Detail','status=no,scrollbars=yes,width=800,height=400');</script>"); 
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code, name;
			double remain = 0;

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[1].Text.Trim();

					conn.QueryString = "DELETE FROM TDEVELOPER WHERE DV_CODE = '"+code+"'";
					conn.ExecuteQuery();

					BindData2();
					break;
				case "edit":
					code = e.Item.Cells[1].Text.Trim();
					name = cleansText(e.Item.Cells[2].Text);
					LBL_DEV_CODE.Text = e.Item.Cells[1].Text.Trim();

					if(e.Item.Cells[10].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						conn.QueryString = "SELECT * FROM TDEVELOPER WHERE DV_CODE = '"+code+"'";
						conn.ExecuteQuery(); 
					
						if(conn.GetRowCount() != 0) 
						{
							TXT_ADDR1.Text = conn.GetFieldValue(0,"DV_ADDR1");
							TXT_ADDR2.Text = conn.GetFieldValue(0,"DV_ADDR2");
							TXT_ADDR3.Text = conn.GetFieldValue(0,"DV_ADDR3");	
							
							TXT_ADDR4.Text =  conn.GetFieldValue(0,"DV_CITY");
							
							TXT_ZIPCODE.Text = conn.GetFieldValue(0,"DV_ZIPCODE"); 
							
							TXT_FN1.Text = conn.GetFieldValue(0,"DV_FAXAREA");
							TXT_FN2.Text = conn.GetFieldValue(0,"DV_FAXNUM"); 
							
							TXT_PH1.Text = conn.GetFieldValue(0,"DV_PHNAREA"); 
							TXT_PH2.Text = conn.GetFieldValue(0,"DV_PHNNUM"); 
							TXT_PH3.Text = conn.GetFieldValue(0,"DV_PHNEXT"); 
							
							//remain = Double.Parse(conn.GetFieldValue(0,"DV_GRLINE")) - Double.Parse(conn.GetFieldValue(0,"DV_TOTGRLINE"));  
							
							/****19 April 2005 edited by Rahma****/
							double dv_grline,dv_totgrline;
							try 
							{
								dv_grline = Double.Parse(conn.GetFieldValue(0,"DV_GRLINE"));
							} 
							catch { dv_grline=0;}
							try 
							{
								dv_totgrline = Double.Parse(conn.GetFieldValue(0,"DV_TOTGRLINE"));
							} 
							catch {dv_totgrline=0;}
											
							remain = dv_grline - dv_totgrline;  

							/*******end edited by Rahma***********/

							TXT_INSUB.Text = GlobalTools.MoneyFormat(conn.GetFieldValue(0,"DV_INTSUB"));
							TXT_GRLINE.Text = GlobalTools.MoneyFormat(conn.GetFieldValue(0,"DV_GRLINE"));
							TXT_REMGRLINE.Text = GlobalTools.MoneyFormat(remain.ToString());  
						
							TXT_MKTSC.Text = conn.GetFieldValue(0,"DV_SRCCODE");

							/*12-08-2005 tambahan untuk CD SIBS*/
							TXT_CDSIBS.Text = conn.GetFieldValue(0,"CD_SIBS");
							/******edit by feby*****************/
							
							//if(conn.GetFieldValue(0,"DV_KERJASAMA") == "1")
							//	CHK_KERJASAMA.Checked = true;
							//else
							//	CHK_KERJASAMA.Checked = false;
							RDB_KERJASAMA.SelectedValue = conn.GetFieldValue(0,"DV_KERJASAMA");

							if(conn.GetFieldValue(0,"DV_BLOCKED") == "1")
								CHK_BLOCK.Checked = true;
							else
								CHK_BLOCK.Checked = false;
	
							try
							{
								DDL_CITY.SelectedValue = conn.GetFieldValue(0,"CITY_ID");   
							}
							catch{ }

							try
							{
								DDL_GRPDEV.SelectedValue = conn.GetFieldValue(0,"GDV_CODE");   
							}
							catch{ }

							TXT_PKSDATE.Text = GlobalTools.FormatDate_Day(conn.GetFieldValue(0,"DV_PKSDATE"));
							try
							{
								DDL_PKSMONTH.SelectedValue = GlobalTools.FormatDate_Month(conn.GetFieldValue(0,"DV_PKSDATE"));
							}
							catch{ }
							TXT_PKSYEAR.Text = GlobalTools.FormatDate_Year(conn.GetFieldValue(0,"DV_PKSDATE"));

							TXT_EXPDATE.Text = GlobalTools.FormatDate_Day(conn.GetFieldValue(0,"DV_EXPDATE"));
							try
							{
								DDL_EXPMONTH.SelectedValue = GlobalTools.FormatDate_Month(conn.GetFieldValue(0,"DV_EXPDATE"));
							}
							catch{ }
							TXT_EXPYEAR.Text = GlobalTools.FormatDate_Year(conn.GetFieldValue(0,"DV_EXPDATE"));

							TXT_NOAKTA.Text = conn.GetFieldValue(0,"DV_NOAKTA");
							TXT_NOTARIS.Text = conn.GetFieldValue(0,"DV_NOTARIS");
						}

						TXT_DEV_NAME.Text = cleansText(name); 

						//DDL_CITY.Enabled = false;

						LBL_SAVEMODE.Text = "2";
					}

					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 	
		}

		protected void DDL_CITY_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1();
			BindData2();
		}

		protected void DDL_GRPDEV_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1();
			BindData2();
		}
	}
}
