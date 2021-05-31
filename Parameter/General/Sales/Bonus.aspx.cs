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
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.Sales
{
	/// <summary>
	/// Summary description for Bonus.
	/// </summary>
	public partial class Bonus : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=SALESMANDIRI;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				DDL_AGENT_TYPE.Items.Add(new ListItem("--Select--",""));
				conncc.QueryString="select * from AGENTTYPE";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_AGENT_TYPE.Items.Add(new ListItem(conncc.GetFieldValue(i,1),conncc.GetFieldValue(i,0)));
				}

				DDL_LEVEL.Items.Add(new ListItem("--Select--",""));
				conncc.QueryString="select  *, a.LEVEL_DESC+' '+b.AGENTYPE_DESC as LEVEL_AGENT from "+
					"LEVEL a left join AGENTTYPE b on a.AGENTYPE_ID = b.AGENTYPE_ID "+
					"order by a.AGENTYPE_ID, LEVEL_CODE";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_LEVEL.Items.Add(new ListItem(conncc.GetFieldValue(i,11),conncc.GetFieldValue(i,0)));
				}

				ViewExistingData();
				ViewPendingData();
			}
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
		}

		private void SetDBConn2()
		{
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			string DB_NAMA = conn.GetFieldValue("DB_NAMA");
			string DB_IP = conn.GetFieldValue("DB_IP");
			string DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncc = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
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

		private void ViewExistingData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_AGENT_BONUSPOINT";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_EXISTING.DataSource = dt;
			try
			{ 
				this.DGR_EXISTING.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_EXISTING.CurrentPageIndex = this.DGR_EXISTING.CurrentPageIndex-1;
					this.DGR_EXISTING.DataBind();
				}
				catch{}
			}
		}
	
		private void ViewPendingData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_AGENT_BONUSPOINT";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			DGR_REQUEST.DataSource = dt;
			try
			{
				this.DGR_REQUEST.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.CurrentPageIndex-1;
					this.DGR_REQUEST.DataBind();
				}
				catch{}
			}
		}

		private void BlankEntry()
		{
			DDL_AGENT_TYPE.SelectedValue="";
			DDL_AGENT_TYPE.Enabled=true;
			DDL_LEVEL.SelectedValue="";
			DDL_LEVEL.Enabled=true;
			TXT_MIN_POINT.Text="";
			TXT_MAX_POINT.Text="";
			TXT_BONUS.Text="";
			LBL_JENIS.Text="";
			LBL_SEQ_ID.Text="";
			LBL_SEQ_NO.Text="";
			LBL_SAVE_MODE.Text="1";
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex=e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex=e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE.Text="0";
					DDL_AGENT_TYPE.SelectedValue =(e.Item.Cells[6].Text);
					DDL_AGENT_TYPE.Enabled=false;
					DDL_LEVEL.SelectedValue = (e.Item.Cells[7].Text);
					DDL_LEVEL.Enabled=false;
					TXT_MIN_POINT.Text=(e.Item.Cells[2].Text);
					TXT_MAX_POINT.Text=(e.Item.Cells[3].Text);
					TXT_BONUS.Text=(e.Item.Cells[4].Text);
					LBL_JENIS.Text="edit";
					LBL_SEQ_ID.Text=""; //Edit dari Existing, ga ada Seq_Id..
					LBL_SEQ_NO.Text=(e.Item.Cells[8].Text);
					break;
				case "delete":
					//SMEDEV2
					//LBL_SAVE_MODE.Text="2";
					string min = GlobalTools.ConvertFloat(e.Item.Cells[2].Text);
					string max = GlobalTools.ConvertFloat(e.Item.Cells[3].Text);
					string bonus = GlobalTools.ConvertFloat(e.Item.Cells[4].Text);
					
					//Get Seq_id....
					conncc.QueryString="select isnull(max(SEQ_ID), 0)+1 SEQ_ID from PENDING_SALESCOM_AGENT_BONUSPOINT";
					conncc.ExecuteQuery();
					string seq_id=conncc.GetFieldValue("SEQ_ID");
					
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_AGENT_BONUSPOINT where "+
						"AGENTYPE_ID='"+e.Item.Cells[6].Text+"' and LEVEL_CODE='"+
						e.Item.Cells[7].Text+"' and SEQ_NO='"+e.Item.Cells[8].Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_AGENT_BONUSPOINT set STATUS='2' where "+
							"AGENTYPE_ID='"+e.Item.Cells[6].Text+"' and LEVEL_CODE='"+
							e.Item.Cells[7].Text+"' and SEQ_NO='"+e.Item.Cells[8].Text+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_AGENT_BONUSPOINT_MAKER '2','"+
							e.Item.Cells[6].Text +"','"+ e.Item.Cells[7].Text +"','"+
							e.Item.Cells[8].Text +"','"+ min +"','"+
							max +"','"+ bonus +"','"+ seq_id +"','1'";
						try
						{
							conncc.ExecuteQuery();
						}
						catch { GlobalTools.popMessage (this,"Error StoreProcedure...");}
					}
					ViewPendingData();
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[11].Text;
					if(status=="2")
					{
						break;
					}
					DDL_AGENT_TYPE.SelectedValue =(e.Item.Cells[7].Text);
					DDL_AGENT_TYPE.Enabled=false;
					DDL_LEVEL.SelectedValue = (e.Item.Cells[8].Text);
					DDL_LEVEL.Enabled=false;
					TXT_MIN_POINT.Text=(e.Item.Cells[2].Text);
					TXT_MAX_POINT.Text=(e.Item.Cells[3].Text);
					TXT_BONUS.Text=(e.Item.Cells[4].Text);
					LBL_JENIS.Text="edit";
					LBL_SEQ_ID.Text=(e.Item.Cells[10].Text);
					LBL_SEQ_NO.Text=(e.Item.Cells[9].Text);
					LBL_SAVE_MODE.Text=(e.Item.Cells[11].Text);
					break;
				case "delete":
					//SMEDEV2
					conncc.QueryString="Delete from PENDING_SALESCOM_AGENT_BONUSPOINT where "+
						"AGENTYPE_ID='"+e.Item.Cells[7].Text+"' and LEVEL_CODE='"+
						e.Item.Cells[8].Text+"' and SEQ_NO='"+e.Item.Cells[9].Text+"' and "+
						"SEQ_ID='"+ e.Item.Cells[10].Text +"'";
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error Delete...");}
					ViewPendingData();
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BlankEntry();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_JENIS.Text=="" && LBL_SEQ_ID.Text == "" && LBL_SEQ_NO.Text == "") //input baru
			{
				//Get Seq_id....
				conncc.QueryString="select isnull(max(SEQ_ID), 0)+1 SEQ_ID from PENDING_SALESCOM_AGENT_BONUSPOINT";
				conncc.ExecuteQuery();
				string seq_id=conncc.GetFieldValue("SEQ_ID");

				string agentype = DDL_AGENT_TYPE.SelectedValue.Trim();
				string level = DDL_LEVEL.SelectedValue.Trim();
				//get SEQ_NO
				conncc.QueryString="select isnull(max(SEQ_NO), 0)+1 SEQ_NO from AGENT_BONUSPOINT "+
					"where AGENTYPE_ID = '"+agentype+"' and LEVEL_CODE = '"+level+"'";
				conncc.ExecuteQuery();
				string seq_no = conncc.GetFieldValue("SEQ_NO");
				
				//membandingkan dengan existing...
				conncc.QueryString="select * from AGENT_BONUSPOINT where AGENTYPE_ID = '"+agentype+"'"+
					" and LEVEL_CODE = '"+level+"' and SEQ_NO = '"+seq_no+"'";
				conncc.ExecuteQuery();
				if (conncc.GetRowCount()!=0)
				{
					GlobalTools.popMessage(this,"Commision is Existing...");
					return;
				}

				//SMEDEV2---Membandingkan dengan Pending..
				conncc.QueryString="select * from PENDING_SALESCOM_AGENT_BONUSPOINT where AGENTYPE_ID = '"+agentype+"'"+
					" and LEVEL_CODE = '"+level+"' and SEQ_NO = '"+seq_no+"' and SEQ_ID='"+seq_id+"'";
				conncc.ExecuteQuery();
				if (conncc.GetRowCount()!=0)
				{
					GlobalTools.popMessage(this,"Commision is Existing...");
					return;
				}
				
				string min = GlobalTools.ConvertFloat(TXT_MIN_POINT.Text);
				string max = GlobalTools.ConvertFloat(TXT_MAX_POINT.Text);
				string bonus = GlobalTools.ConvertFloat(TXT_BONUS.Text);
				conncc.QueryString="PARAM_SALESCOM_AGENT_BONUSPOINT_MAKER '1','"+
					DDL_AGENT_TYPE.SelectedValue +"','"+ DDL_LEVEL.SelectedValue +"','"+
					seq_no +"','"+ min +"','"+ max +"','"+ bonus +"','"+ seq_id +"','1'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			if (LBL_JENIS.Text=="edit") //edit
			{
				if (LBL_SEQ_ID.Text != "")//Edit dari DGR_REQUEST
				{
					string min = GlobalTools.ConvertFloat(TXT_MIN_POINT.Text);
					string max = GlobalTools.ConvertFloat(TXT_MAX_POINT.Text);
					string bonus = GlobalTools.ConvertFloat(TXT_BONUS.Text);
					conncc.QueryString="PARAM_SALESCOM_AGENT_BONUSPOINT_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						DDL_AGENT_TYPE.SelectedValue +"','"+ DDL_LEVEL.SelectedValue +"','"+
						LBL_SEQ_NO.Text +"','"+ min +"','"+ max +"','"+ bonus +"','"+ LBL_SEQ_ID.Text +"','1'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
					
				}
				else if (LBL_SEQ_ID.Text == "") //Edit dari DGR_EXISTING
				{
					string min = GlobalTools.ConvertFloat(TXT_MIN_POINT.Text);
					string max = GlobalTools.ConvertFloat(TXT_MAX_POINT.Text);
					string bonus = GlobalTools.ConvertFloat(TXT_BONUS.Text);
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_AGENT_BONUSPOINT where AGENTYPE_ID = '"+DDL_AGENT_TYPE.SelectedValue+"'"+
								     " and LEVEL_CODE = '"+DDL_LEVEL.SelectedValue+"' and SEQ_NO = '"+LBL_SEQ_NO.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//Get Seq_id....
						conncc.QueryString="select isnull(max(SEQ_ID), 0)+1 SEQ_ID from PENDING_SALESCOM_AGENT_BONUSPOINT";
						conncc.ExecuteQuery();
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					else
					{
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					

					conncc.QueryString="PARAM_SALESCOM_AGENT_BONUSPOINT_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						DDL_AGENT_TYPE.SelectedValue +"','"+ DDL_LEVEL.SelectedValue +"','"+
						LBL_SEQ_NO.Text +"','"+ min +"','"+ max +"','"+ bonus +"','"+ LBL_SEQ_ID.Text +"','1'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
					
				}
			}
			LBL_SAVE_MODE.Text="1";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../SalesParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}
}

