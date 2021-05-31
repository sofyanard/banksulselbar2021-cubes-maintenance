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
	/// Summary description for CarInfoParam.
	/// </summary>
	public partial class CarInfoParam : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2= new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected string area;
		protected string city, plus = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetDBConn2();
			
			if(!IsPostBack)
			{
				area = (string) Session["AreaId"];
				dtCity();
				dtDealer();
				dtCarType();
				dtBrand();
				dtModel("");
				dtSeri("");
				viewExistingData();
				viewPendingData();
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
			this.DGExisting.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGExisting_PageIndexChanged);
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion
		
		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();
			
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}
		private void dtCity()
		{
			string myQuery;
			if (area.Trim()=="0000")
                myQuery="select city_id,city_name from city where area_id='"+area.Trim()+"'";
			else
				myQuery="select city_id,city_name from city";
			GlobalTools.fillRefList(this.DDL_CITY,myQuery,conn);
		}
		private void dtDealer()
		{
			string myQuery;
			if (area.Trim()=="0000")
				myQuery="select id_dealer,nm_dealer from dealer where city_id=(select city_id from city where area_id='"+area.Trim()+"')";
			else
				myQuery="select id_dealer,nm_dealer from dealer";
			GlobalTools.fillRefList(this.DDL_DEALER,myQuery,conn);
		}
		private void dtCarType()
		{
			string myQuery="select id_jns,nm_jenis from jenismobil";
			GlobalTools.fillRefList(this.DDL_CAR_TYPE,myQuery,conn);
		}
		private void dtBrand()
		{
			string myQuery="select id_merek,nm_merek from merek";
			GlobalTools.fillRefList(this.DDL_BRAND,myQuery,conn);
		}
		private void dtModel(string a)
		{
			string myQUERY="select ID_MODEL, NM_MODEL from MODEL where ID_MEREK ='"+DDL_BRAND.SelectedValue+"'";
			GlobalTools.fillRefList(DDL_MODEL,myQUERY,conn);
			DDL_MODEL.SelectedValue = a;
		}
		private void dtSeri(string a)
		{
			string myQUERY="select ID_TIPE, NM_TIPE from TIPE where ID_MODEL ='"+DDL_MODEL.SelectedValue+"'";
			GlobalTools.fillRefList(DDL_SERI,myQUERY,conn);
			DDL_SERI.SelectedValue = a;
		}
		
		private void viewPendingData()
		{
			conn.QueryString = "select ID_MOBILBARU,ID_TAHUN,ID_KOTA,ID_TIPE,ID_DEALER,a.ID_JNS,isnull(NM_JENIS,'') NM_JENIS,isnull(HARGAJUAL,0) HARGAJUAL,isnull(QTY,0) QTY,"+
				"JML_PINTU,TRANSMISSION,JNS_PRODUKSI,KETERANGAN,DIR_GAMBAR,ID_MEREK,ID_MODEL, YEAROFMADE,isnull(MIN_DP,0) MIN_DP,CH_STA, "+
				"case when CH_STA='0' then 'Update' when CH_STA='1' then 'Insert' when CH_STA='2' then 'Delete' else '' end CH_STA1 "+
				"from TMOBIL_BARU a left join jenismobil b on a.id_jns=b.id_jns";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
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
			if(TXT_YEAR.Text != "")
			{
				plus += "and ID_TAHUN = '"+TXT_YEAR.Text+"' ";
			}

			if(DDL_DEALER.SelectedValue != "")
			{
				plus += "and ID_DEALER = '"+DDL_DEALER.SelectedValue+"' ";
			}

			if(DDL_CAR_TYPE.SelectedValue != "")
			{
				plus += "and a.ID_JNS = '"+DDL_CAR_TYPE.SelectedValue+"' ";
			}

			if(DDL_BRAND.SelectedValue != "")
			{
				plus += "and ID_MEREK = '"+DDL_BRAND.SelectedValue+"' ";
			}

			conn.QueryString = "select ID_MOBILBARU,ID_TAHUN,ID_KOTA,ID_TIPE,ID_DEALER,a.ID_JNS,isnull(NM_JENIS,'') NM_JENIS,isnull(HARGAJUAL,0) HARGAJUAL,isnull(QTY,0) QTY,"+
				"JML_PINTU,TRANSMISSION,JNS_PRODUKSI,KETERANGAN,DIR_GAMBAR,ID_MEREK,ID_MODEL, YEAROFMADE,isnull(MIN_DP,0) MIN_DP "+
				"from MOBIL_BARU a left join jenismobil b on a.id_jns=b.id_jns where a.active='1' and id_kota='"+DDL_CITY.SelectedValue.Trim()+"' "+plus;
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
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
			DDL_CITY.SelectedIndex=0;
			DDL_DEALER.SelectedIndex=0;
			DDL_CAR_TYPE.SelectedIndex=0;
			DDL_BRAND.SelectedIndex=0;
			
			dtModel("");
			dtSeri("");
			
			TXT_CAR_CODE.Text="";
			TXT_CAR_CODE.ReadOnly=false;
			TXT_YEAR.Text="";
			TXT_PRICE.Text="0";
			TXT_MIN_PAYMENT.Text="0";
			TXT_QUANTITY.Text="0";
			TXT_MAN_YEAR.Text="";
			TXT_NUM_DOOR.Text="";
			TXT_TRANSMISSION.Text="";
			TXT_PRODUCTION_TYPE.Text="";
			TXT_DESC.Text="";
			TXT_PICTURE.Text="";
			LBL_SAVEMODE.Text="1";
			
		}
		private void executeMaker(string id,string city,string year,string dealer,string jns,string merk,string model,string type,string price,string dp,string qty,string yearmd,string pintu,string trans,string prod,string desc,string pic,string pendingStatus) 
		{
			string myQueryString="";
			conn.QueryString = "select * from TMOBIL_BARU where id_mobilbaru='"+id+"'";
			conn.ExecuteQuery();

			int jumlah = conn.GetRowCount();

			if (jumlah > 0) 
			{
				myQueryString = "UPDATE TMOBIL_BARU SET "+
					"ID_KOTA = '"+city+"',"+
					"CH_STA = '"+pendingStatus+"',"+
					"ID_TAHUN = '"+year+"',"+
					"ID_DEALER = '"+dealer+"',"+
					"ID_JENIS = '"+jns+"',"+
					"ID_MEREK = '"+merk+"',"+
					"ID_MODEL = '"+model+"',"+
					"ID_TIPE = '"+type+"',"+
					"YEAROFMADE = '"+yearmd+"',"+
					"MIN_DP = "+Convert.ToDouble(dp)+","+
					"QTY = "+Convert.ToInt32(qty)+","+
					"HARGAJUAL = "+Convert.ToInt32(price.Replace(".","").Trim())+","+
					"JML_PINTU = '"+pintu+"',"+
					"TRANSMISSION= '"+trans+"',"+
					"JNS_PRODUKSI = '"+prod+"',"+
					"KETERANGAN = '"+desc+"' "+
					"DIR_GAMBAR = '"+pic+"' "+
					"WHERE ID_MOBILBARU='" +id+ "'";
				conn.QueryString = myQueryString;
				
				try 
				{
					conn.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					if (ex.Message.ToString().IndexOf("truncate") > 0)
						Tools.popMessage(this, "Gagal melakukan update !");
				}
			}
			else 
			{
				if (LBL_ACTIVE.Text.Trim() == "1")
				{
					double dp1 = Convert.ToDouble(dp);
					int qty1 = Convert.ToInt32(qty);
					int price1 = Convert.ToInt32(price.Replace(".","").Trim());
					
					myQueryString="INSERT INTO TMOBIL_BARU VALUES('"+id+"','"+year+"','"+city+"','"+merk+"','"+model+"','"+type+"','"+dealer+"','"+jns+"',"+price1+","+qty1+",'"+pintu+"','"+trans+"','"+prod+"','"+desc+"','"+pic+"','"+year+"','"+pendingStatus+"',"+dp1+")";
					conn.QueryString = myQueryString;
				}
				else
				{
					double dp1 = Convert.ToDouble(dp);
					int qty1 = Convert.ToInt32(qty);
					int price1 = Convert.ToInt32(price.Replace(".","").Trim());
					
					myQueryString="INSERT INTO TMOBIL_BARU VALUES('"+id+"','"+year+"','"+city+"','"+merk+"','"+model+"','"+type+"','"+dealer+"','"+jns+"',"+price1+","+qty1+",'"+pintu+"','"+trans+"','"+prod+"','"+desc+"','"+pic+"','"+year+"','"+pendingStatus+"',"+dp1+")";
					conn.QueryString = myQueryString;
				}
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
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}
		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			clearControls();
			string myQUERY;
			
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					string id = CleansText(e.Item.Cells[0].Text);
					string city = CleansText(e.Item.Cells[2].Text);
					string year = CleansText(e.Item.Cells[3].Text);
					string dealer = CleansText(e.Item.Cells[4].Text);
					string jns = CleansText(e.Item.Cells[5].Text);
					string merk = CleansText(e.Item.Cells[6].Text);
					string model = CleansText(e.Item.Cells[7].Text);
					string type = CleansText(e.Item.Cells[8].Text);
					string price = CleansText(e.Item.Cells[9].Text);
					string dp = CleansText(e.Item.Cells[10].Text);
					string qty = CleansText(e.Item.Cells[11].Text);
					string yearmd = CleansText(e.Item.Cells[12].Text);
					string pintu = CleansText(e.Item.Cells[13].Text);
					string trans = CleansText(e.Item.Cells[14].Text);
					string prod = CleansText(e.Item.Cells[15].Text);
					string desc = CleansText(e.Item.Cells[16].Text);
					string pic = CleansText(e.Item.Cells[17].Text);
					
					LBL_SAVEMODE.Text = CleansText(e.Item.Cells[18].Text);
					TXT_CAR_CODE.ReadOnly=true;
					
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
		
					try
					{
						DDL_CITY.SelectedValue=city;
					}
					catch{ }


					TXT_CAR_CODE.Text=id;
					TXT_YEAR.Text=year;
					
					try
					{
						DDL_DEALER.SelectedValue=dealer;
					}
					catch{ }

					try
					{
						DDL_CAR_TYPE.SelectedValue=jns;
					}
					catch{ }

					try
					{
						DDL_BRAND.SelectedValue=merk;
					}
					catch{ }

					try
					{
						dtModel(model);
					}
					catch{ }

					try
					{
						dtSeri(type);
					}
					catch{ }

					TXT_PRICE.Text=price;
					TXT_MIN_PAYMENT.Text=dp;
					TXT_QUANTITY.Text=qty;
					TXT_MAN_YEAR.Text=yearmd;
					TXT_NUM_DOOR.Text=pintu;
					TXT_TRANSMISSION.Text=trans;
					TXT_PRODUCTION_TYPE.Text=prod;
					TXT_DESC.Text=desc;
					TXT_PICTURE.Text=pic;
					
					break;
				case "delete":
					string id1 = CleansText(e.Item.Cells[0].Text);
					myQUERY= "delete from TMOBIL_BARU where id_mobilbaru='"+id1+"'";
					
					conn.QueryString=myQUERY;
					conn.ExecuteQuery();
					viewPendingData();
					
					break;
				default : //do nothing
					break;
			}
		}
		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim()=="1")
			{
				conn.QueryString= "select * from mobil_baru where id_mobilbaru='"+TXT_CAR_CODE.Text.Trim()+"'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount()==0)
				{
					try 
					{
						executeMaker(TXT_CAR_CODE.Text.Trim(),DDL_CITY.SelectedValue,TXT_YEAR.Text.Trim(),
							DDL_DEALER.SelectedValue,DDL_CAR_TYPE.SelectedValue,DDL_BRAND.SelectedValue,
							DDL_MODEL.SelectedValue,DDL_SERI.SelectedValue,TXT_PRICE.Text.Trim(),
							TXT_MIN_PAYMENT.Text.Trim(),TXT_QUANTITY.Text.Trim(),TXT_MAN_YEAR.Text.Trim(),TXT_NUM_DOOR.Text.Trim(),
							TXT_TRANSMISSION.Text.Trim(),TXT_PRODUCTION_TYPE.Text.Trim(),TXT_DESC.Text.Trim(),
							TXT_PICTURE.Text.Trim(),LBL_SAVEMODE.Text.Trim());
					}
					catch {  }

					viewPendingData();
					clearControls();
				}
				else
				{
					GlobalTools.popMessage(this,"ID IS ALREADY IN THE TABLE, REQUEST CANCELLED");	
				}
			}
			else
			{
				try 
				{
					executeMaker(TXT_CAR_CODE.Text.Trim(),DDL_CITY.SelectedValue,TXT_YEAR.Text.Trim(),
						DDL_DEALER.SelectedValue,DDL_CAR_TYPE.SelectedValue,DDL_BRAND.SelectedValue,
						DDL_MODEL.SelectedValue,DDL_SERI.SelectedValue,TXT_PRICE.Text.Trim(),
						TXT_MIN_PAYMENT.Text.Trim(),TXT_QUANTITY.Text.Trim(),TXT_MAN_YEAR.Text.Trim(),TXT_NUM_DOOR.Text.Trim(),
						TXT_TRANSMISSION.Text.Trim(),TXT_PRODUCTION_TYPE.Text.Trim(),TXT_DESC.Text.Trim(),
						TXT_PICTURE.Text.Trim(),LBL_SAVEMODE.Text.Trim());
				}
				catch { GlobalTools.popMessage(this,"Salah 2"); }

				viewPendingData();
				clearControls();
			}
		}
		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{	
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					string id = CleansText(e.Item.Cells[0].Text);
					string city = CleansText(e.Item.Cells[2].Text);
					string year = CleansText(e.Item.Cells[3].Text);
					string dealer = CleansText(e.Item.Cells[4].Text);
					string jns = CleansText(e.Item.Cells[5].Text);
					string merk = CleansText(e.Item.Cells[6].Text);
					string model = CleansText(e.Item.Cells[7].Text);
					string type = CleansText(e.Item.Cells[8].Text);
					string price = CleansText(e.Item.Cells[9].Text);
					string dp = CleansText(e.Item.Cells[10].Text);
					string qty = CleansText(e.Item.Cells[11].Text);
					string yearmd = CleansText(e.Item.Cells[12].Text);
					string pintu = CleansText(e.Item.Cells[13].Text);
					string trans = CleansText(e.Item.Cells[14].Text);
					string prod = CleansText(e.Item.Cells[15].Text);
					string desc = CleansText(e.Item.Cells[16].Text);
					string pic = CleansText(e.Item.Cells[17].Text);
					
					TXT_CAR_CODE.ReadOnly=true;
					
					try
					{
						DDL_CITY.SelectedValue=city;
					}
					catch{ }

					TXT_CAR_CODE.Text=id;
					TXT_YEAR.Text=year;
					
					try
					{	
						DDL_DEALER.SelectedValue=dealer;
					}
					catch{ }

					try
					{
						DDL_CAR_TYPE.SelectedValue=jns;
					}
					catch{ }

					try
					{
						DDL_BRAND.SelectedValue=merk;
					}
					catch{ }

					try
					{
						dtModel(model);
					}
					catch{ }

					try
					{
						dtSeri(type);
					}
					catch{ }

					TXT_PRICE.Text=price;
					TXT_MIN_PAYMENT.Text=dp;
					TXT_QUANTITY.Text=qty;
					TXT_MAN_YEAR.Text=yearmd;
					TXT_NUM_DOOR.Text=pintu;
					TXT_TRANSMISSION.Text=trans;
					TXT_PRODUCTION_TYPE.Text=prod;
					TXT_DESC.Text=desc;
					TXT_PICTURE.Text=pic;
					
					break;

				case "delete":
					string id1 = CleansText(e.Item.Cells[0].Text);
					string city1 = CleansText(e.Item.Cells[2].Text);
					string year1 = CleansText(e.Item.Cells[3].Text);
					string dealer1 = CleansText(e.Item.Cells[4].Text);
					string jns1 = CleansText(e.Item.Cells[5].Text);
					string merk1 = CleansText(e.Item.Cells[6].Text);
					string model1 = CleansText(e.Item.Cells[7].Text);
					string type1 = CleansText(e.Item.Cells[8].Text);
					string price1 = CleansText(e.Item.Cells[9].Text);
					string dp1 = CleansText(e.Item.Cells[10].Text);
					string qty1 = CleansText(e.Item.Cells[11].Text);
					string yearmd1 = CleansText(e.Item.Cells[12].Text);
					string pintu1 = CleansText(e.Item.Cells[13].Text);
					string trans1 = CleansText(e.Item.Cells[14].Text);
					string prod1 = CleansText(e.Item.Cells[15].Text);
					string desc1 = CleansText(e.Item.Cells[16].Text);
					string pic1 = CleansText(e.Item.Cells[17].Text);
				
					executeMaker(id1,city1,year1,dealer1,jns1,merk1,model1,type1,price1,dp1,qty1,yearmd1,pintu1,trans1,prod1,desc1,pic1,"2");
					
					viewPendingData();	
					break;
				default : //do nothing
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamAll.aspx?moduleID=40");
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();
		}

		protected void DDL_BRAND_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			dtModel("");
		}

		protected void DDL_MODEL_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			dtSeri("");
		}

		protected void DDL_CITY_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			viewExistingData();
		}

		protected void BTN_FIND_Click(object sender, System.EventArgs e)
		{
			viewExistingData(); 
		}
		
	}
}
