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
using Microsoft.VisualBasic;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for Calendar.
	/// </summary>
	public partial class Calendar : System.Web.UI.Page
	{
		protected Connection conn;
		protected Tools tool = new Tools();

		string theForm="";
		string theObj="";
		string theObjDesc="";
		string tgl="";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

				theForm = Request.QueryString["theForm"];
				theObj = Request.QueryString["theObj"];
			try{theObjDesc = Request.QueryString["targetObjectDesc"].Trim();}
			catch{}

				//SAVE.Attributes.Add("onclick","pilih('" + theObj + "', '" + theObjDesc + "');");

			if(!IsPostBack)
			{
				conn.QueryString = "DELETE TEMP_CALENDAR";
				conn.ExecuteQuery();
				FillCalendar();
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
			this.DGR_CALENDAR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_CALENDAR_ItemCommand);

		}
		#endregion

		private void FillCalendar()
		{
			int tanggal;
			string senin=""; string selasa=""; string rabu=""; string kamis=""; string jumat="";
			string sabtu=""; string minggu="";

			try
			{
				for (tanggal = 1; tanggal <= 31; tanggal++)
				{	
					string tanggal2 = Request.QueryString["bulan"] + "/" + tanggal + "/" + Request.QueryString["tahun"];

					conn.QueryString = "SELECT DATEPART(dw, '" + tanggal2 + "')";
					conn.ExecuteQuery();

					string hari = conn.GetFieldValue(0,0);

					switch(conn.GetFieldValue(0,0))
					{
						case "2":
							senin = tanggal.ToString();
							break;
						case "3":
							selasa = tanggal.ToString();
							break;
						case "4":
							rabu = tanggal.ToString();
							break;
						case "5":
							kamis = tanggal.ToString();
							break;
						case "6":
							jumat = tanggal.ToString();
							break;
						case "7":
							sabtu = tanggal.ToString();
							break;
						case "1":
							minggu = tanggal.ToString();
							break;
						default:
							break;
					}
					
					if(conn.GetFieldValue(0,0) == "1")
					{
						conn.QueryString = "EXEC TEMP_CALENDAR_INSERT '" +
							senin + "', '" + selasa + "', '" + rabu + "', '" + kamis + "', '" +
							jumat + "', '" + sabtu + "', '" + minggu + "'";
						conn.ExecuteQuery();

						senin = ""; selasa="";
						rabu = ""; kamis="";
						jumat = ""; sabtu="";
						minggu = "";
					}
				}
				if(senin!="" || selasa!="" || rabu!="" || kamis!="" || jumat!="" || sabtu!="" || minggu!="")
				{
					conn.QueryString = "EXEC TEMP_CALENDAR_INSERT '" +
						senin + "', '" + selasa + "', '" + rabu + "', '" + kamis + "', '" +
						jumat + "', '" + sabtu + "', '" + minggu + "'";
					conn.ExecuteQuery();
				}
			}
			catch
			{
				if(senin!="" || selasa!="" || rabu!="" || kamis!="" || jumat!="" || sabtu!="" || minggu!="")
				{
					conn.QueryString = "EXEC TEMP_CALENDAR_INSERT '" +
						senin + "', '" + selasa + "', '" + rabu + "', '" + kamis + "', '" +
						jumat + "', '" + sabtu + "', '" + minggu + "'";
					conn.ExecuteQuery();
				}
			}

			conn.QueryString = "SELECT * FROM TEMP_CALENDAR";
			conn.ExecuteQuery();

			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			DGR_CALENDAR.DataSource = dt;
			try
			{
				DGR_CALENDAR.DataBind();
			}
			catch
			{
				DGR_CALENDAR.CurrentPageIndex = 0;
				DGR_CALENDAR.DataBind();
			}

			LinkButton lnk;

			for (int i = 0; i < DGR_CALENDAR.Items.Count; i++)
			{
				lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[1].FindControl("LNK_SENIN");
				lnk.Text = DGR_CALENDAR.Items[i].Cells[0].Text;
				if(lnk.Text =="&nbsp;")
					lnk.Visible = false;
				lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[3].FindControl("LNK_SELASA");
				lnk.Text = DGR_CALENDAR.Items[i].Cells[2].Text;
				if(lnk.Text =="&nbsp;")
					lnk.Visible = false;
				lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[5].FindControl("LNK_RABU");
				lnk.Text = DGR_CALENDAR.Items[i].Cells[4].Text;
				if(lnk.Text =="&nbsp;")
					lnk.Visible = false;
				lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[7].FindControl("LNK_KAMIS");
				lnk.Text = DGR_CALENDAR.Items[i].Cells[6].Text;
				if(lnk.Text =="&nbsp;")
					lnk.Visible = false;
				lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[9].FindControl("LNK_JUMAT");
				lnk.Text = DGR_CALENDAR.Items[i].Cells[8].Text;
				if(lnk.Text =="&nbsp;")
					lnk.Visible = false;
				lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[11].FindControl("LNK_SABTU");
				lnk.Text = DGR_CALENDAR.Items[i].Cells[10].Text;
				if(lnk.Text =="&nbsp;")
					lnk.Visible = false;
				lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[13].FindControl("LNK_MINGGU");
				lnk.Text = DGR_CALENDAR.Items[i].Cells[12].Text;
				if(lnk.Text =="&nbsp;")
					lnk.Visible = false;
			}
		}

		protected void CLOSE_ServerClick(object sender, System.EventArgs e)
		{
			Response.Write("<script language='JavaScript1.2'>window.close();</script>");
		}

		protected void SAVE_ServerClick(object sender, System.EventArgs e)
		{
			conn.QueryString = "delete pending_rf_libur2 where tahun='" + Request.QueryString["tahun"] + "' and bulan='" + Request.QueryString["bulan"] + "'";
			conn.ExecuteQuery();

			try
			{
				LinkButton lnk;
				for (int i = 0; i < DGR_CALENDAR.Items.Count; i++)
				{
					lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[1].FindControl("LNK_SENIN");
					if(lnk.ForeColor == System.Drawing.Color.Red && lnk.Text != "&nbsp;")
					{
						if(tgl=="")
							tgl = lnk.Text;
						else
							tgl = tgl + ", " + lnk.Text;

						conn.QueryString = "insert into pending_rf_libur2(tahun, bulan, tanggal) values('" + Request.QueryString["tahun"] + "', '" + Request.QueryString["bulan"] + "', '" + lnk.Text + "')";
						conn.ExecuteQuery();
					}
					lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[3].FindControl("LNK_SELASA");
					if(lnk.ForeColor == System.Drawing.Color.Red && lnk.Text != "&nbsp;")
					{
						if(tgl=="")
							tgl = lnk.Text;
						else
							tgl = tgl + ", " + lnk.Text;

						conn.QueryString = "insert into pending_rf_libur2(tahun, bulan, tanggal) values('" + Request.QueryString["tahun"] + "', '" + Request.QueryString["bulan"] + "', '" + lnk.Text + "')";
						conn.ExecuteQuery();
					}
					lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[5].FindControl("LNK_RABU");
					if(lnk.ForeColor == System.Drawing.Color.Red && lnk.Text != "&nbsp;")
					{
						if(tgl=="")
							tgl = lnk.Text;
						else
							tgl = tgl + ", " + lnk.Text;

						conn.QueryString = "insert into pending_rf_libur2(tahun, bulan, tanggal) values('" + Request.QueryString["tahun"] + "', '" + Request.QueryString["bulan"] + "', '" + lnk.Text + "')";
						conn.ExecuteQuery();
					}
					lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[7].FindControl("LNK_KAMIS");
					if(lnk.ForeColor == System.Drawing.Color.Red && lnk.Text != "&nbsp;")
					{
						if(tgl=="")
							tgl = lnk.Text;
						else
							tgl = tgl + ", " + lnk.Text;

						conn.QueryString = "insert into pending_rf_libur2(tahun, bulan, tanggal) values('" + Request.QueryString["tahun"] + "', '" + Request.QueryString["bulan"] + "', '" + lnk.Text + "')";
						conn.ExecuteQuery();
					}
					lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[9].FindControl("LNK_JUMAT");
					if(lnk.ForeColor == System.Drawing.Color.Red && lnk.Text != "&nbsp;")
					{
						if(tgl=="")
							tgl = lnk.Text;
						else
							tgl = tgl + ", " + lnk.Text;

						conn.QueryString = "insert into pending_rf_libur2(tahun, bulan, tanggal) values('" + Request.QueryString["tahun"] + "', '" + Request.QueryString["bulan"] + "', '" + lnk.Text + "')";
						conn.ExecuteQuery();
					}
					lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[11].FindControl("LNK_SABTU");
					if(lnk.ForeColor == System.Drawing.Color.Red && lnk.Text != "&nbsp;")
					{
						if(tgl=="")
							tgl = lnk.Text;
						else
							tgl = tgl + ", " + lnk.Text;

						conn.QueryString = "insert into pending_rf_libur2(tahun, bulan, tanggal) values('" + Request.QueryString["tahun"] + "', '" + Request.QueryString["bulan"] + "', '" + lnk.Text + "')";
						conn.ExecuteQuery();
					}
					lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[13].FindControl("LNK_MINGGU");
					if(lnk.ForeColor == System.Drawing.Color.Red && lnk.Text != "&nbsp;")
					{
						if(tgl=="")
							tgl = lnk.Text;
						else
							tgl = tgl + ", " + lnk.Text;

						conn.QueryString = "insert into pending_rf_libur2(tahun, bulan, tanggal) values('" + Request.QueryString["tahun"] + "', '" + Request.QueryString["bulan"] + "', '" + lnk.Text + "')";
						conn.ExecuteQuery();
					}
				}
				LST_RESULT.Text = tgl;

			}
			catch (Exception ex)
			{
				Response.Write("<!--" + ex.Message + "-->");
				return;
			}

			Response.Write("<script language='JavaScript1.2'>window.opener.document." + 
				theForm + "." + theObj + ".value='" + LST_RESULT.Text + "'; " +
				"window.opener.document." + theForm + ".submit(); </script>");
				
		}

		private void DGR_CALENDAR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			LinkButton lnk;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "senin":
					lnk = (LinkButton)e.Item.Cells[1].FindControl("LNK_SENIN");
					if(lnk.ForeColor != System.Drawing.Color.Red)
						lnk.ForeColor=System.Drawing.Color.Red;
					else
						lnk.ForeColor=System.Drawing.Color.Black;
					break;
				case "selasa":
					lnk = (LinkButton)e.Item.Cells[3].FindControl("LNK_SELASA");
					if(lnk.ForeColor != System.Drawing.Color.Red)
						lnk.ForeColor=System.Drawing.Color.Red;
					else
						lnk.ForeColor=System.Drawing.Color.Black;
					break;
				case "rabu":
					lnk = (LinkButton)e.Item.Cells[5].FindControl("LNK_RABU");
					if(lnk.ForeColor!=System.Drawing.Color.Red)
						lnk.ForeColor=System.Drawing.Color.Red;
					else
						lnk.ForeColor=System.Drawing.Color.Black;
					break;
				case "kamis":
					lnk = (LinkButton)e.Item.Cells[7].FindControl("LNK_KAMIS");
					if(lnk.ForeColor!=System.Drawing.Color.Red)
						lnk.ForeColor=System.Drawing.Color.Red;
					else
						lnk.ForeColor=System.Drawing.Color.Black;
					break;
				case "jumat":
					lnk = (LinkButton)e.Item.Cells[9].FindControl("LNK_JUMAT");
					if(lnk.ForeColor!=System.Drawing.Color.Red)

						lnk.ForeColor=System.Drawing.Color.Red;
					else
						lnk.ForeColor=System.Drawing.Color.Black;
					break;
				case "sabtu":
					
					break;
				case "minggu":
					
					break;
			}
		}

		protected void CLEAR_ServerClick(object sender, System.EventArgs e)
		{
			LinkButton lnk;
			for (int i = 0; i < DGR_CALENDAR.Items.Count; i++)
			{
				lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[1].FindControl("LNK_SENIN");
				lnk.ForeColor = System.Drawing.Color.Black;
				lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[3].FindControl("LNK_SELASA");
				lnk.ForeColor = System.Drawing.Color.Black;
				lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[5].FindControl("LNK_RABU");
				lnk.ForeColor = System.Drawing.Color.Black;
				lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[7].FindControl("LNK_KAMIS");
				lnk.ForeColor = System.Drawing.Color.Black;
				lnk = (LinkButton)DGR_CALENDAR.Items[i].Cells[9].FindControl("LNK_JUMAT");
				lnk.ForeColor = System.Drawing.Color.Black;
			}
		}
	}
}
