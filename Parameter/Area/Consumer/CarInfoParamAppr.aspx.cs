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
using System.Configuration;

namespace CuBES_Maintenance.Parameter.Area.Consumer
{
	/// <summary>
	/// Summary description for CarInfoParamAppr.
	/// </summary>
	public partial class CarInfoParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected string scid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetDBConn2();
			
			if (!IsPostBack)
			{
				LBL_ACTIVE.Text = Request.QueryString["active"];
				
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

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

		private void viewPendingData() 
		{
			string tableName = Request.QueryString["tablename"];

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
		
		private void DGRequest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;			
			viewPendingData();
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void performRequest(int row, string userid)
		{
			string id1 = CleansText(DGRequest.Items[row].Cells[0].Text);
			string city1 = CleansText(DGRequest.Items[row].Cells[2].Text);
			string year1 = CleansText(DGRequest.Items[row].Cells[3].Text);
			string dealer1 = CleansText(DGRequest.Items[row].Cells[4].Text);
			string jns1 = CleansText(DGRequest.Items[row].Cells[5].Text);
			string merk1 = CleansText(DGRequest.Items[row].Cells[6].Text);
			string model1 = CleansText(DGRequest.Items[row].Cells[7].Text);
			string type1 = CleansText(DGRequest.Items[row].Cells[8].Text);
			string price1 = CleansText(DGRequest.Items[row].Cells[9].Text);
			string dp1 = CleansText(DGRequest.Items[row].Cells[10].Text);
			string qty1 = CleansText(DGRequest.Items[row].Cells[11].Text);
			string yearmd1 = CleansText(DGRequest.Items[row].Cells[12].Text);
			string pintu1 = CleansText(DGRequest.Items[row].Cells[13].Text);
			string trans1 = CleansText(DGRequest.Items[row].Cells[14].Text);
			string prod1 = CleansText(DGRequest.Items[row].Cells[15].Text);
			string desc1 = CleansText(DGRequest.Items[row].Cells[16].Text);
			string pic1 = CleansText(DGRequest.Items[row].Cells[17].Text);
			
			executeApproval(id1,city1,year1,dealer1,jns1,merk1,model1,type1,price1,dp1,qty1,yearmd1,pintu1,trans1,prod1,desc1,pic1,"1",userid);
			
		}

		private void deleteData(int row)
		{
			try 
			{
				string id1 = CleansText(DGRequest.Items[row].Cells[0].Text);
				string city1 = CleansText(DGRequest.Items[row].Cells[2].Text);
				string year1 = CleansText(DGRequest.Items[row].Cells[3].Text);
				string dealer1 = CleansText(DGRequest.Items[row].Cells[4].Text);
				string jns1 = CleansText(DGRequest.Items[row].Cells[5].Text);
				string merk1 = CleansText(DGRequest.Items[row].Cells[6].Text);
				string model1 = CleansText(DGRequest.Items[row].Cells[7].Text);
				string type1 = CleansText(DGRequest.Items[row].Cells[8].Text);
				string price1 = CleansText(DGRequest.Items[row].Cells[9].Text);
				string dp1 = CleansText(DGRequest.Items[row].Cells[10].Text);
				string qty1 = CleansText(DGRequest.Items[row].Cells[11].Text);
				string yearmd1 = CleansText(DGRequest.Items[row].Cells[12].Text);
				string pintu1 = CleansText(DGRequest.Items[row].Cells[13].Text);
				string trans1 = CleansText(DGRequest.Items[row].Cells[14].Text);
				string prod1 = CleansText(DGRequest.Items[row].Cells[15].Text);
				string desc1 = CleansText(DGRequest.Items[row].Cells[16].Text);
				string pic1 = CleansText(DGRequest.Items[row].Cells[17].Text);
				
				executeApproval(id1,city1,year1,dealer1,jns1,merk1,model1,type1,price1,dp1,qty1,yearmd1,pintu1,trans1,prod1,desc1,pic1,"0","");
				
			} 
			catch {}
		}

		private void executeApproval(string id,string city,string year,string dealer,string jns,string merk,string model,string type,string price,string dp,string qty,string yearmd,string pintu,string trans,string prod,string desc,string pic,string approvalFlag,string uid) 
		{
			string pendingStatus = "", query;
			int jumlah;

			if (approvalFlag == "1") 
			{
				conn.QueryString = "SELECT * FROM TMOBIL_BARU where id_mobilbaru='"+id+"'";
				conn.ExecuteQuery();

				pendingStatus	= conn.GetFieldValue("CH_STA");
				desc			= conn.GetFieldValue(0,1);

				conn.QueryString = "SELECT * FROM MOBIL_BARU where id_mobilbaru='"+id+"'";
				conn.ExecuteQuery();

				jumlah = conn.GetRowCount();

				if (pendingStatus == "0" || pendingStatus == "1") 
				{
					if (jumlah > 0) 
					{
						conn.QueryString = "UPDATE MOBIL_BARU SET "+
							"ID_KOTA = '"+city+"',"+							
							"ID_TAHUN = '"+year+"',"+
							"ID_DEALER = '"+dealer+"',"+ 
							"ID_JNS = '"+jns+"',"+
							"ID_MEREK = '"+merk+"',"+
							"ID_MODEL = '"+model+"',"+
							"ID_TIPE = '"+type+"',"+
							"YEAROFMADE = '"+yearmd+"',"+
							"MIN_DP = "+Convert.ToDouble(dp)+","+
							"QTY = "+Convert.ToInt32(qty)+","+
							"HARGAJUAL = "+Convert.ToInt32(price)+","+
							"JML_PINTU = '"+pintu+"',"+
							"TRANSMISSION= '"+trans+"',"+
							"JNS_PRODUKSI = '"+prod+"',"+
							"KETERANGAN = '"+desc+"', "+
							"DIR_GAMBAR = '"+pic+"' "+
							"WHERE ID_MOBILBARU='" +id+ "'";
						conn.ExecuteQuery();
					}
					else 
					{
						if(LBL_ACTIVE.Text.Trim() == "1")
						{
							double dp1 = Convert.ToDouble(dp);
							int qty1 = Convert.ToInt32(qty);
							int price1 = Convert.ToInt32(price);
							
							conn.QueryString = "INSERT INTO MOBIL_BARU VALUES('"+id+"','"+year+"','"+city+"','"+merk+"','"+model+"','"+type+"','"+dealer+"','"+jns+"',"+price1+","+qty1+",'"+pintu+"','"+trans+"','"+prod+"','"+desc+"','"+pic+"','"+year+"',"+dp1+",'1')";
						}
						else
						{
							double dp1 = Convert.ToDouble(dp);
							int qty1 = Convert.ToInt32(qty);
							int price1 = Convert.ToInt32(price);
							
							conn.QueryString = "INSERT INTO MOBIL_BARU VALUES('"+id+"','"+year+"','"+city+"','"+merk+"','"+model+"','"+type+"','"+dealer+"','"+jns+"',"+price1+","+qty1+",'"+pintu+"','"+trans+"','"+prod+"','"+desc+"','"+pic+"','"+year+"',"+dp1+",'1')";
						}

						conn.ExecuteQuery();
					}
				}
				else if (pendingStatus == "2") 
				{
					if (jumlah > 0) 
					{
						conn.QueryString = "UPDATE MOBIL_BARU SET ACTIVE='0' where id_mobilbaru='"+id+"'";
						conn.ExecuteQuery();
					}
				}
			}

			query = "DELETE FROM TMOBIL_BARU where id_mobilbaru='"+id+"'";
			
			conn.QueryString=query;
			conn.ExecuteQuery();

			conn.ClearData();


			/* coding for audittrial parameter */
			
			conn.QueryString = "EXEC PARAM_AREA_CAR_INFO_AUDIT '"+id+"','"+city+"','"+pendingStatus+"','"+uid+"',"+GlobalTools.ConvertNull(year)+","+
				GlobalTools.ConvertNull(merk)+","+GlobalTools.ConvertNull(model)+","+GlobalTools.ConvertNull(type)+","+
				GlobalTools.ConvertNull(dealer)+","+GlobalTools.ConvertNull(jns)+","+
				GlobalTools.ConvertFloat(price)+","+GlobalTools.ConvertFloat(qty)+","+
				GlobalTools.ConvertNull(pintu)+","+GlobalTools.ConvertNull(trans)+","+
				GlobalTools.ConvertNull(prod)+","+GlobalTools.ConvertNull(desc)+","+GlobalTools.ConvertNull(pic)+","+
				GlobalTools.ConvertNull(yearmd)+","+GlobalTools.ConvertFloat(dp);
			conn.ExecuteNonQuery();
			
			/* end of coding */
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbB.Checked = false;
							rbC.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			scid = (string) Session["UserID"];

			for (int i = 0; i < DGRequest.Items.Count; i++)
			{
				RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject");
				
				if (rbA.Checked)
				{
					performRequest(i,scid);
				}
				else if (rbR.Checked)
				{
						deleteData(i);
				}
			}

			viewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamApprovalAll.aspx?mc=9902040202&ModuleId=40");
		}
	}
}
