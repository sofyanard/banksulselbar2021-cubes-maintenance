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

namespace CuBES_Maintenance.Parameter.General.CC
{
	/// <summary>
	/// Summary description for ParamFTPServer.
	/// </summary>
	public partial class ParamFTPServer : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection (System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;
		/* New Field Declare */
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				//ViewValue();
				ViewExistingData();
				ViewPendingData();
				GlobalTools.fillRefList(ddl_reject_track1,"select TR_CODE, TR_CODE+' - '+TR_DESC AS TR_DESC from rftracklst where ACTIVE='1'",conn);
				GlobalTools.fillRefList(ddl_reject_next1,"select SC_CODE, SC_CODE+' - '+SC_DESC AS SC_DESC from scgroup where ACTIVE='1'",conn);
				GlobalTools.fillRefList(ddl_reject_track2,"select TR_CODE, TR_CODE+' - '+TR_DESC AS TR_DESC from rftracklst where ACTIVE='1'",conn);
				GlobalTools.fillRefList(ddl_reject_next2,"select SC_CODE, SC_CODE+' - '+SC_DESC AS SC_DESC from scgroup where ACTIVE='1'",conn);
				GlobalTools.fillRefList(ddl_cap_track,"select TR_CODE, TR_CODE+' - '+TR_DESC AS TR_DESC from rftracklst where ACTIVE='1'",conn);
				GlobalTools.fillRefList(ddl_cap_approveby,"select SC_CODE, SC_CODE+' - '+SC_DESC AS SC_DESC from scgroup where ACTIVE='1'",conn);
			}
			
			ViewMenu();
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

		public void SetDBConn2()
		{
			conn2.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn2.ExecuteQuery();
			string DB_NAMA = conn2.GetFieldValue("DB_NAMA");
			string DB_IP = conn2.GetFieldValue("DB_IP");
			string DB_LOGINID = conn2.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn2.GetFieldValue("DB_LOGINPWD");
			conn = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void ViewMenu()
		{
			try 
			{
				conn.QueryString = "select * from SCREENMENU where menucode = '3301'";
				conn.ExecuteQuery();

				for (int i = 0; i < conn.GetRowCount(); i++)
				{
					HyperLink t = new HyperLink();
					t.Text = conn.GetFieldValue(i,2);
					t.Font.Bold = true;
					t.ForeColor = Color.Blue;
					string link = conn.GetFieldValue(i,3).Trim();
					if (link == "")
						t.ForeColor = Color.Red; 
					else
					{
						int insAt;
						string strtemp = "";
						if (link.IndexOf("javascript:") == 0)
						{
							insAt = link.IndexOf(".aspx?");
							if (insAt!=-1) insAt += 6;
						} 
						else insAt = link.Length;
						if (link.IndexOf("ParamFTPServer.aspx") != -1)
							t.ForeColor = Color.Red;
						if (link.IndexOf("mc=") >= 0)
							strtemp = "regno=" + Request.QueryString["regno"] + "&curef="+Request.QueryString["curef"]+"&tc="+Request.QueryString["tc"];
						else strtemp = "regno=" + Request.QueryString["regno"] + "&curef="+Request.QueryString["curef"]+"&mc="+Request.QueryString["mc"]+"&tc="+Request.QueryString["tc"];

						if (link.IndexOf("?de=") < 0 && link.IndexOf("&de=") < 0) 
							strtemp = strtemp + "&de=" + Request.QueryString["de"];	
						if (link.IndexOf("?par=") < 0 && link.IndexOf("&par=") < 0)  
							strtemp = strtemp + "&par=" + Request.QueryString["par"];
						if (insAt!=-1)	link = link.Insert(insAt, strtemp);
					}
					t.NavigateUrl = link;
					Menu.Controls.Add(t);
					Menu.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
				}
			} 
			catch (Exception ex) 
			{
				string temp = ex.ToString();
			}
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ClearBoxes()
		{
			this.TXT_IN_IDFTP.Text		= "";
			this.TXT_IN_IPSERVER.Text	= "";
			this.TXT_IN_PASSFTP.Text	= "";
			this.TXT_IN_PORTFTP.Text	= "";

			this.ddl_reject_next1.SelectedIndex = 0;
			this.ddl_reject_track1.SelectedIndex = 0;
			this.ddl_reject_next2.SelectedIndex = 0;
			this.ddl_reject_track2.SelectedIndex = 0;
			this.tx_cap.Text			= "";
			this.ddl_cap_approveby.SelectedIndex = 0;
			this.ddl_cap_track.SelectedIndex = 0;

			this.BTN_SAVE.Enabled		= false;
		}
		protected void ViewValue()
		{
			conn.QueryString = "select * from VW_INITIAL_FTP ";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				TXT_IN_IDFTP.Text = conn.GetFieldValue("IN_IDFTP");
				TXT_IN_IPSERVER.Text = conn.GetFieldValue("IN_IPSERVER");
				TXT_IN_PASSFTP.Text = conn.GetFieldValue("IN_PASSFTP");
				TXT_IN_PORTFTP.Text = conn.GetFieldValue("IN_PORTFTP");
				
				try
				{
					ddl_reject_next1.SelectedValue  = conn.GetFieldValue("reject_next1");
				}
				catch{}

				try
				{
					ddl_reject_track1.SelectedValue = conn.GetFieldValue("reject_track1");
				}
				catch{}

				try
				{
					ddl_reject_next2.SelectedValue = conn.GetFieldValue("reject_next2");
				}
				catch{}

				try
				{
					ddl_reject_track2.SelectedValue = conn.GetFieldValue("reject_track2");
				}
				catch{}

				try
				{
					ddl_cap_approveby.SelectedValue = conn.GetFieldValue("cap_approveby");
				}
				catch{}

				try
				{
					ddl_cap_track.SelectedValue = conn.GetFieldValue("cap_track");
				}
				catch{}

				tx_cap.Text  = conn.GetFieldValue("cap");

				/* New Field View */
			}
			conn.ClearData();
		}

		protected void ViewExistingData()
		{
			conn.QueryString = "select * from VW_INITIAL_FTP ";
			conn.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_EXISTING.DataSource = dt;
			try
			{
				this.DGR_EXISTING.DataBind();
			}
			catch
			{
				this.DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.PageCount - 1;
				this.DGR_EXISTING.DataBind();
			}

			
			/*
			conn.QueryString = "select * from VW_INITIAL_FTP ";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				TXT_IN_IDFTP1.Text = conn.GetFieldValue("IN_IDFTP");
				TXT_IN_IPSERVER1.Text = conn.GetFieldValue("IN_IPSERVER");
				TXT_IN_PASSFTP1.Text = conn.GetFieldValue("IN_PASSFTP");
				TXT_IN_PORTFTP1.Text = conn.GetFieldValue("IN_PORTFTP");

				ddl_reject_next1a.Text  = conn.GetFieldValue("reject_next1");
				ddl_reject_track1a.Text  = conn.GetFieldValue("reject_track1");
				ddl_reject_next2a.Text  = conn.GetFieldValue("reject_next2");
				ddl_reject_track2a.Text  = conn.GetFieldValue("reject_track2");

				ddl_cap_approvebya.Text  = conn.GetFieldValue("cap_approveby");
				ddl_cap_tracka.Text  = conn.GetFieldValue("cap_track");
				tx_capa.Text  = conn.GetFieldValue("cap");
			}
			conn.ClearData();
			*/
		}

		protected void ViewPendingData()
		{
			conn.QueryString = "select * from VW_PENDING_CC_INITIAL_FTP ";
			conn.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_REQUEST.DataSource = dt;
			try
			{
				this.DGR_REQUEST.DataBind();
			}
			catch
			{
				this.DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				this.DGR_REQUEST.DataBind();
			}

			/*
			conn.QueryString = "select * from VW_PENDING_CC_INITIAL_FTP ";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				TXT_IN_IDFTP2.Text = conn.GetFieldValue("IN_IDFTP");
				TXT_IN_IPSERVER2.Text = conn.GetFieldValue("IN_IPSERVER");
				TXT_IN_PASSFTP2.Text = conn.GetFieldValue("IN_PASSFTP");
				TXT_IN_PORTFTP2.Text = conn.GetFieldValue("IN_PORTFTP");

				ddl_reject_next1b.Text  = conn.GetFieldValue("reject_next1");
				ddl_reject_track1b.Text  = conn.GetFieldValue("reject_track1");
				ddl_reject_next2b.Text  = conn.GetFieldValue("reject_next2");
				ddl_reject_track2b.Text = conn.GetFieldValue("reject_track2");

				ddl_cap_approvebyb.Text = conn.GetFieldValue("cap_approveby");
				ddl_cap_trackb.Text= conn.GetFieldValue("cap_track");
				tx_capb.Text  = conn.GetFieldValue("cap");
			}
			conn.ClearData();
			*/
		}


		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			/*
			conn.QueryString = " update INITIAL set IN_IDFTP = '"+ TXT_IN_IDFTP.Text
				+"', IN_IPSERVER = '"+ TXT_IN_IPSERVER.Text
				+"', IN_PASSFTP = '"+ TXT_IN_PASSFTP.Text
				+"', IN_PORTFTP = '"+ TXT_IN_PORTFTP.Text + "'";
			conn.ExecuteNonQuery();
			*/
			//GlobalTools.popMessage(this,"Request has been saved");
	
			
			/*
			conn.QueryString = " update PENDING_CC_INITIAL set IN_IDFTP = '"+ TXT_IN_IDFTP.Text
				+"', IN_IPSERVER = '"+ TXT_IN_IPSERVER.Text
				+"', IN_PASSFTP = '"+ TXT_IN_PASSFTP.Text
				+"', IN_PORTFTP = '"+ TXT_IN_PORTFTP.Text 
				+"', reject_next1 = '"+ ddl_reject_next1.SelectedValue
				+"', reject_next2 = '"+ ddl_reject_next2.SelectedValue
				+"', reject_track1 = '"+ ddl_reject_track1.SelectedValue
				+"', reject_track2 = '"+ ddl_reject_track2.SelectedValue
				+"', cap_approveby = '"+ ddl_cap_approveby.SelectedValue
				+"', cap_track = '"+ ddl_cap_track.SelectedValue
				+"', cap = '"+ GlobalTools.ConvertFloat(tx_cap.Text)+"'";
				
			*/
					
			/*
							conn.QueryString = " insert into PENDING_CC_INITIAL "+
								" (IN_IDFTP,IN_IPSERVER,IN_PASSFTP,IN_PORTFTP,"+
								"  reject_next1, reject_next2, reject_track1, reject_track2, "+
								"  cap_approveby, cap_track, cap) values "+
								" ('"+ TXT_IN_IDFTP.Text
								+"','"+ TXT_IN_IPSERVER.Text
								+"','"+ TXT_IN_PASSFTP.Text
								+"','"+ TXT_IN_PORTFTP.Text 
								+"','"+ ddl_reject_next1.SelectedValue
								+"','"+ ddl_reject_next2.SelectedValue
								+"','"+ ddl_reject_track1.SelectedValue
								+"','"+ ddl_reject_track2.SelectedValue
								+"','"+ ddl_cap_approveby.SelectedValue
								+"','"+ ddl_cap_track.SelectedValue
								+"','"+ tx_cap.Text+"')";
			*/
			conn.QueryString = "select * from PENDING_CC_INITIAL where IN_SEQ = '"+LBL_IN_SEQ.Text+"'";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				conn.QueryString = " update PENDING_CC_INITIAL set IN_IDFTP = '"+TXT_IN_IDFTP.Text+"'"+
					", IN_IPSERVER = '"+TXT_IN_IPSERVER.Text+"', IN_PASSFTP = '"+TXT_IN_PASSFTP.Text+"'"+
					", IN_PORTFTP = '"+TXT_IN_PORTFTP.Text+"', REJECT_NEXT1 = '"+ddl_reject_next1.SelectedValue+"'"+
					", REJECT_NEXT2 = '"+ddl_reject_next2.SelectedValue+"', REJECT_TRACK1 = '"+ddl_reject_track1.SelectedValue+"'"+
					", REJECT_TRACK2 = '"+ddl_reject_track2.SelectedValue+"', CAP_APPROVEBY = '"+ddl_cap_approveby.SelectedValue+"'"+
					", CAP_TRACK = '"+ddl_cap_track.SelectedValue+"', CAP = '"+GlobalTools.ConvertFloat(tx_cap.Text)+"'"+
					" where IN_SEQ = '"+LBL_IN_SEQ.Text+"'";
			}
			else
			{
				conn.QueryString = " insert into PENDING_CC_INITIAL "+
					" (IN_IDFTP,IN_IPSERVER,IN_PASSFTP,IN_PORTFTP,"+
					"  reject_next1, reject_next2, reject_track1, reject_track2, "+
					"  cap_approveby, cap_track, cap) values "+
					" ('"+ TXT_IN_IDFTP.Text
					+"','"+ TXT_IN_IPSERVER.Text
					+"','"+ TXT_IN_PASSFTP.Text
					+"','"+ TXT_IN_PORTFTP.Text 
					+"','"+ ddl_reject_next1.SelectedValue
					+"','"+ ddl_reject_next2.SelectedValue
					+"','"+ ddl_reject_track1.SelectedValue
					+"','"+ ddl_reject_track2.SelectedValue
					+"','"+ ddl_cap_approveby.SelectedValue
					+"','"+ ddl_cap_track.SelectedValue
					+"','"+ GlobalTools.ConvertFloat(tx_cap.Text)+"')";
				conn.ExecuteNonQuery();
			}
			
			ClearBoxes();
			ViewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		private void LinkEdit_Click(object sender, System.EventArgs e)
		{
			this.BTN_SAVE.Enabled	= true;
			ViewValue();
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					this.BTN_SAVE.Enabled	= true;
					LBL_IN_SEQ.Text = cleansText(e.Item.Cells[12].Text);
					TXT_IN_IPSERVER.Text = cleansText(e.Item.Cells[0].Text);
					TXT_IN_PORTFTP.Text = cleansText(e.Item.Cells[1].Text);
					TXT_IN_IDFTP.Text = cleansText(e.Item.Cells[2].Text);			
					TXT_IN_PASSFTP.Text = cleansText(e.Item.Cells[3].Text);
				
					try
					{
						ddl_reject_next1.SelectedValue  = cleansText(e.Item.Cells[4].Text);
					}
					catch{}

					try
					{
						ddl_reject_track1.SelectedValue = cleansText(e.Item.Cells[5].Text);
					}
					catch{}

					try
					{
						ddl_reject_next2.SelectedValue = cleansText(e.Item.Cells[6].Text);
					}
					catch{}

					try
					{
						ddl_reject_track2.SelectedValue = cleansText(e.Item.Cells[7].Text);
					}
					catch{}

					try
					{
						ddl_cap_approveby.SelectedValue = cleansText(e.Item.Cells[8].Text);
					}
					catch{}

					try
					{
						ddl_cap_track.SelectedValue = cleansText(e.Item.Cells[9].Text);
					}
					catch{}

					tx_cap.Text  = cleansText(e.Item.Cells[10].Text).Replace(".",",");
				break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					this.BTN_SAVE.Enabled	= true;
					LBL_IN_SEQ.Text = cleansText(e.Item.Cells[12].Text);
					TXT_IN_IPSERVER.Text = cleansText(e.Item.Cells[0].Text);
					TXT_IN_PORTFTP.Text = cleansText(e.Item.Cells[1].Text);
					TXT_IN_IDFTP.Text = cleansText(e.Item.Cells[2].Text);			
					TXT_IN_PASSFTP.Text = cleansText(e.Item.Cells[3].Text);
				
					try
					{
						ddl_reject_next1.SelectedValue  = cleansText(e.Item.Cells[4].Text);
					}
					catch{}

					try
					{
						ddl_reject_track1.SelectedValue = cleansText(e.Item.Cells[5].Text);
					}
					catch{}

					try
					{
						ddl_reject_next2.SelectedValue = cleansText(e.Item.Cells[6].Text);
					}
					catch{}

					try
					{
						ddl_reject_track2.SelectedValue = cleansText(e.Item.Cells[7].Text);
					}
					catch{}

					try
					{
						ddl_cap_approveby.SelectedValue = cleansText(e.Item.Cells[8].Text);
					}
					catch{}

					try
					{
						ddl_cap_track.SelectedValue = cleansText(e.Item.Cells[9].Text);
					}
					catch{}

					tx_cap.Text  = cleansText(e.Item.Cells[10].Text).Replace(".",",");
					break;
				case "delete":		
					conn.QueryString = "delete from PENDING_CC_INITIAL ";
					try
					{
						conn.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					ClearBoxes();
					ViewPendingData();
					break;
			}
		}
	}
}
