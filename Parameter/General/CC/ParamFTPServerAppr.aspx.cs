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
	/// Summary description for ParamFTPServerAppr.
	/// </summary>
	public partial class ParamFTPServerAppr : System.Web.UI.Page
	{
		protected Connection connsme = new Connection (System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		protected Connection conn,conn2;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
				ViewPendingData();
			//ClearComponents();
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
			this.DGR_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR_ItemCommand);
			this.DGR_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPR_PageIndexChanged);

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

		protected void ViewPendingData()
		{
			/*
			conn.QueryString = "select * from VW_PENDING_CC_INITIAL_FTP ";
			conn.ExecuteQuery();
			if (conn.GetRowCount()>0)
			{
				TXT_IN_IDFTP2.Text = conn.GetFieldValue("IN_IDFTP");
				TXT_IN_IPSERVER2.Text = conn.GetFieldValue("IN_IPSERVER");
				TXT_IN_PASSFTP2.Text = conn.GetFieldValue("IN_PASSFTP");
				LBL_IN_PASSFTP2.Text = conn.GetFieldValue("IN_PASSFTP");
				TXT_IN_PORTFTP2.Text = conn.GetFieldValue("IN_PORTFTP");
				ddl_reject_next1.Text  = conn.GetFieldValue("reject_next1");
				ddl_reject_track1.Text  = conn.GetFieldValue("reject_track1");
				ddl_reject_next2.Text  = conn.GetFieldValue("reject_next2");
				ddl_reject_track2.Text  = conn.GetFieldValue("reject_track2");

				ddl_cap_approveby.Text  = conn.GetFieldValue("cap_approveby");
				ddl_cap_track.Text  = conn.GetFieldValue("cap_track");
				tx_cap.Text  = conn.GetFieldValue("cap");

				New Field View
			}
			conn.ClearData();
			ClearComponents();
			*/

			conn.QueryString = "select * from VW_PENDING_CC_INITIAL_FTP ";
			conn.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_APPR.DataSource = dt;
			try
			{
				this.DGR_APPR.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_APPR.CurrentPageIndex = DGR_APPR.CurrentPageIndex - 1;
					this.DGR_APPR.DataBind();
				}
				catch{}
			}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < this.DGR_APPR.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
						rbR = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject");
					if (rbA.Checked)
					{
						AuditTrail();
				
						string IN_IDFTP,IN_IPSERVER,IN_PASSFTP,IN_PORTFTP, REJECT_NEXT1, REJECT_NEXT2, REJECT_TRACK1, REJECT_TRACK2, CAP_APPROVEBY, CAP_TRACK, CAP;

						conn.QueryString = "select * from VW_PENDING_CC_INITIAL_FTP ";
						conn.ExecuteQuery();
						IN_IDFTP = conn.GetFieldValue("IN_IDFTP");
						IN_IPSERVER = conn.GetFieldValue("IN_IPSERVER");
						IN_PASSFTP = conn.GetFieldValue("IN_PASSFTP");
						IN_PORTFTP = conn.GetFieldValue("IN_PORTFTP");
						REJECT_NEXT1 = conn.GetFieldValue("REJECT_NEXT1");
						REJECT_NEXT2 = conn.GetFieldValue("REJECT_NEXT2");
						REJECT_TRACK1 = conn.GetFieldValue("REJECT_TRACK1");
						REJECT_TRACK2 = conn.GetFieldValue("REJECT_TRACK2");
						CAP_APPROVEBY = conn.GetFieldValue("CAP_APPROVEBY");
						CAP_TRACK = conn.GetFieldValue("CAP_TRACK");
						CAP = GlobalTools.ConvertFloat(conn.GetFieldValue("CAP"));

						conn.QueryString = " update INITIAL set IN_IDFTP = '"+ IN_IDFTP
							+"', IN_IPSERVER = '"+ IN_IPSERVER
							+"', IN_PASSFTP = '"+ IN_PASSFTP
							+"', IN_PORTFTP = '"+ IN_PORTFTP
							+"', REJECT_NEXT1 = '"+ REJECT_NEXT1
							+"', REJECT_NEXT2 = '"+ REJECT_NEXT2
							+"', REJECT_TRACK1 = '"+ REJECT_TRACK1
							+"', REJECT_TRACK2 = '"+ REJECT_TRACK2
							+"', CAP_APPROVEBY = '"+ CAP_APPROVEBY
							+"', CAP_TRACK = '"+ CAP_TRACK
							+"', CAP = "+ CAP +"";
						conn.ExecuteNonQuery();
						DeletePendingData();
					}
					else if (rbR.Checked)
					{
						DeletePendingData();
					}
				} 
				catch {}
			}					
			ViewPendingData();
		}

		private void AuditTrail()
		{
			//EXISTING DATA
			string IN_IDFTP_OLD,IN_IPSERVER_OLD,IN_PASSFTP_OLD,IN_PORTFTP_OLD, REJECT_NEXT1_OLD, REJECT_NEXT2_OLD, REJECT_TRACK1_OLD, REJECT_TRACK2_OLD, CAP_APPROVEBY_OLD, CAP_TRACK_OLD, CAP_OLD;
			string IN_IDFTP,IN_IPSERVER,IN_PASSFTP,IN_PORTFTP, REJECT_NEXT1, REJECT_NEXT2, REJECT_TRACK1, REJECT_TRACK2, CAP_APPROVEBY, CAP_TRACK, CAP;
			
			conn.QueryString = "select * from VW_INITIAL_FTP ";
			conn.ExecuteQuery();
			IN_IDFTP_OLD = conn.GetFieldValue("IN_IDFTP");
			IN_IPSERVER_OLD = conn.GetFieldValue("IN_IPSERVER");
			IN_PASSFTP_OLD = conn.GetFieldValue("IN_PASSFTP");
			IN_PORTFTP_OLD = conn.GetFieldValue("IN_PORTFTP");
			REJECT_NEXT1_OLD = conn.GetFieldValue("REJECT_NEXT1");
			REJECT_NEXT2_OLD = conn.GetFieldValue("REJECT_NEXT2");
			REJECT_TRACK1_OLD = conn.GetFieldValue("REJECT_TRACK1");
			REJECT_TRACK2_OLD = conn.GetFieldValue("REJECT_TRACK2");
			CAP_APPROVEBY_OLD = conn.GetFieldValue("CAP_APPROVEBY");
			CAP_TRACK_OLD = conn.GetFieldValue("CAP_TRACK");
			CAP_OLD = GlobalTools.ConvertFloat(conn.GetFieldValue("CAP"));

			//PENDING DATA
			conn.QueryString = "select * from VW_PENDING_CC_INITIAL_FTP ";
			conn.ExecuteQuery();
			IN_IDFTP = conn.GetFieldValue("IN_IDFTP");
			IN_IPSERVER = conn.GetFieldValue("IN_IPSERVER");
			IN_PASSFTP = conn.GetFieldValue("IN_PASSFTP");
			IN_PORTFTP = conn.GetFieldValue("IN_PORTFTP");
			REJECT_NEXT1 = conn.GetFieldValue("REJECT_NEXT1");
			REJECT_NEXT2 = conn.GetFieldValue("REJECT_NEXT2");
			REJECT_TRACK1 = conn.GetFieldValue("REJECT_TRACK1");
			REJECT_TRACK2 = conn.GetFieldValue("REJECT_TRACK2");
			CAP_APPROVEBY = conn.GetFieldValue("CAP_APPROVEBY");
			CAP_TRACK = conn.GetFieldValue("CAP_TRACK");
			CAP = GlobalTools.ConvertFloat(conn.GetFieldValue("CAP"));

			if (IN_IDFTP_OLD != IN_IDFTP)
				ExecSPAuditTrail("IN_IDFTP",IN_IDFTP_OLD,IN_IDFTP);
			if (IN_IPSERVER_OLD != IN_IPSERVER)
				ExecSPAuditTrail("IN_IPSERVER",IN_IPSERVER_OLD,IN_IPSERVER);
			if (IN_PASSFTP_OLD != IN_PASSFTP)
				ExecSPAuditTrail("IN_PASSFTP",IN_PASSFTP_OLD,IN_PASSFTP);
			if (IN_PORTFTP_OLD != IN_PORTFTP)
				ExecSPAuditTrail("IN_PORTFTP",IN_PORTFTP_OLD,IN_PORTFTP);
			if (REJECT_NEXT1_OLD != REJECT_NEXT1)
				ExecSPAuditTrail("IN_PORTFTP",REJECT_NEXT1_OLD,REJECT_NEXT1);
			if (REJECT_NEXT2_OLD != REJECT_NEXT2)
				ExecSPAuditTrail("IN_PORTFTP",REJECT_NEXT2_OLD,REJECT_NEXT2);
			if (REJECT_TRACK1_OLD != REJECT_TRACK1)
				ExecSPAuditTrail("IN_PORTFTP",REJECT_TRACK1_OLD,REJECT_TRACK1);
			if (REJECT_TRACK2_OLD != REJECT_TRACK2)
				ExecSPAuditTrail("IN_PORTFTP",REJECT_TRACK2_OLD,REJECT_TRACK2);
			if (CAP_APPROVEBY_OLD != CAP_APPROVEBY)
				ExecSPAuditTrail("IN_PORTFTP",CAP_APPROVEBY_OLD,CAP_APPROVEBY);
			if (CAP_APPROVEBY_OLD != CAP_APPROVEBY)
				ExecSPAuditTrail("IN_PORTFTP",CAP_APPROVEBY_OLD,CAP_APPROVEBY);
			if (CAP_TRACK_OLD != CAP_TRACK)
				ExecSPAuditTrail("IN_PORTFTP",CAP_TRACK_OLD,CAP_TRACK);
			if (CAP_OLD != CAP)
				ExecSPAuditTrail("IN_PORTFTP",CAP_OLD,CAP);
		}

		private void ExecSPAuditTrail(string field,string from, string to)
		{
			string userid = Session["UserID"].ToString();
			string tablename = "INITIAL";
			string paramid = "66";
			
			connsme.QueryString = "select PARAMNAME,PG_ID from RFPARAMETERALL where paramid='" + paramid + "' and moduleid='20' and classid='' and ismaker=0";
			connsme.ExecuteQuery();
			string paramname	= connsme.GetFieldValue("PARAMNAME");
			string paramclass	= connsme.GetFieldValue("PG_ID");
			connsme.ClearData();
			
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + "1" + "','" + tablename + "','" +
				field + "','" + from + "','" + to + "','" + "0" + "','" + userid + "','" + 
				"" + "','" + paramclass +"','" + paramname + "'";
			conn.ExecuteNonQuery();
		}

		private void DeletePendingData()
		{
			conn.QueryString = " delete from PENDING_CC_INITIAL ";
			conn.ExecuteNonQuery();
		}

		/*
		private void ClearComponents()
		{
			if (this.TXT_IN_IDFTP2.Text	== "" && this.TXT_IN_IPSERVER2.Text	== "" &&
				this.TXT_IN_PASSFTP2.Text	== "" && this.TXT_IN_PORTFTP2.Text	== "")
			{
				this.rdo_Approve.Visible	= false;
				this.rdo_Pending.Visible	= false;
				this.rdo_Reject.Visible		= false;
				this.BTN_SUBMIT.Enabled		= false;
			}
		}
		*/

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		private void DGR_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rd_Pending");
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
	}
}
