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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for JobTitleParam.
	/// </summary>
	public partial class JobTitleParam : System.Web.UI.Page
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
				setSeq();
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
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}
        
		private void BindData1()
		{
			conn.QueryString = "select JT_CODE,JT_DESC,JOB_TYPE_ID,CD_SIBS from RFJOBTITLE where active='1'";
			conn.ExecuteQuery();

			if (conn.GetRowCount() != 0)
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				DGEXISTING.DataSource = dt;

				try
				{
					DGEXISTING.DataBind();
				}
				catch 
				{
					DGEXISTING.CurrentPageIndex = DGEXISTING.PageCount - 1;
					DGEXISTING.DataBind();
				}				
			} 			

			conn.ClearData();
		}

		private void setSeq()
		{
		    conn.QueryString = "select count(*) jml from TRFJOBTITLE where ch_sta = '1'";
			conn.ExecuteQuery();

			if(conn.GetFieldValue("jml").Trim() == "0")
			{																						  
				conn.QueryString = "SELECT max(convert(int,JT_CODE))+1 maxSeq from RFJOBTITLE";
				conn.ExecuteQuery();
				LBL_JT_SEQ.Text = conn.GetFieldValue("maxSeq").Trim();
			}
			else
			{
				conn.QueryString = "select max(convert(int,JT_CODE))+1 maxSeq from TRFJOBTITLE where ch_sta = '1'";
				conn.ExecuteQuery();
				LBL_JT_SEQ.Text = conn.GetFieldValue("maxSeq").Trim();
			}			
			
		}
		private void BindData2()
		{
			conn.QueryString = "select JT_CODE,JT_DESC,JOB_TYPE_ID,CD_SIBS,CH_STA from TRFJOBTITLE";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGREQUEST.DataSource = dt;

			try
			{
				DGREQUEST.DataBind();
			}
			catch 
			{
				DGREQUEST.CurrentPageIndex = DGREQUEST.PageCount - 1;
				DGREQUEST.DataBind();
			}

			for (int i = 0; i < DGREQUEST.Items.Count; i++)
			{
				//DGREQUEST.Items[i].Cells[0].Text = (i+1+(DGREQUEST.CurrentPageIndex)*DGREQUEST.PageSize).ToString();
				if (DGREQUEST.Items[i].Cells[4].Text.Trim() == "1")
				{
					DGREQUEST.Items[i].Cells[3].Text = "INSERT";
				}
				else if (DGREQUEST.Items[i].Cells[4].Text.Trim() == "2")
				{
					DGREQUEST.Items[i].Cells[3].Text = "UPDATE";
				}
				else if (DGREQUEST.Items[i].Cells[4].Text.Trim() == "3")
				{
					DGREQUEST.Items[i].Cells[3].Text = "DELETE";
				}				
			} 
		}

		private void save()
		{
			if(LBL_SAVEMODE.Text.Trim() == "1")
			{
				conn.QueryString = "select count(*) jml from RFJOBTITLE where JT_CODE = '"+LBL_JT_SEQ.Text.Trim()+"'";
				conn.ExecuteQuery();

				if(conn.GetFieldValue("jml").Trim() == "0")
				{
					conn.QueryString = "insert into TRFJOBTITLE (JT_CODE,JT_DESC,JOB_TYPE_ID,CD_SIBS,CH_STA) "+
						"values ('"+LBL_JT_SEQ.Text+"','"+TXT_DESC.Text+"','"+TXT_ID.Text+"','"+TXT_CD_SIBS.Text+"','"+LBL_SAVEMODE.Text+"')";
					conn.ExecuteNonQuery();								
				}
				else 
				{
					GlobalTools.popMessage(this,"Duplicate Data");
				}
			}
			if(LBL_SAVEMODE.Text.Trim() == "2" || LBL_SAVEMODE.Text.Trim() == "3")
			{
				conn.QueryString = "select count(*) jml from TRFJOBTITLE where JT_CODE = '"+LBL_JT_SEQ.Text.Trim()+"' and JOB_TYPE_ID = '"+TXT_ID.Text+"'";
				conn.ExecuteQuery();

				if(conn.GetFieldValue("jml").Trim() == "0")
				{
					conn.QueryString = "select count(*) jml from RFJOBTITLE where JT_CODE = '"+LBL_JT_SEQ.Text.Trim()+"' and JOB_TYPE_ID = '"+TXT_ID.Text+"'";
					conn.ExecuteQuery();

					if(conn.GetFieldValue("jml").Trim() == "0" ||(conn.GetFieldValue("jml").Trim() != "0" && TXT_ID.Text.Trim() == LBL_CODE_LAMA.Text))
					{
						conn.QueryString = "insert into TRFJOBTITLE (JT_CODE,JT_DESC,JOB_TYPE_ID,CD_SIBS,CH_STA,JOB_ID_LAMA) "+
							"values ('"+LBL_JT_SEQ.Text+"','"+TXT_DESC.Text+"','"+TXT_ID.Text+"','"+TXT_CD_SIBS.Text+"','"+LBL_SAVEMODE.Text+"','"+LBL_CODE_LAMA.Text+"')";
						conn.ExecuteNonQuery();
					}
					else GlobalTools.popMessage(this,"Duplicate Data");
				}
				else 
				{
					conn.QueryString = "UPDATE TRFJOBTITLE set JT_CODE ='"+LBL_JT_SEQ.Text+"',JT_DESC = '"+TXT_DESC.Text+"',JOB_TYPE_ID ='"+TXT_ID.Text+"',CD_SIBS='"+TXT_CD_SIBS.Text+"',CH_STA = '"+LBL_SAVEMODE.Text+"',"+
						"JOB_ID_LAMA = '"+LBL_CODE_LAMA.Text+"'  where JT_CODE = '"+LBL_JT_SEQ.Text.Trim()+"' and JOB_TYPE_ID = '"+TXT_ID.Text+"'";
					conn.ExecuteNonQuery();
				}
			}
			LBL_CODE_LAMA.Text = "";
			TXT_ID.Text = "";
			TXT_DESC.Text = "";
			TXT_CD_SIBS.Text = "";
			LBL_JT_SEQ.Text = "";
			LBL_SAVEMODE.Text = "1";
			BindData1();
			BindData2();
			setSeq();
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
			this.DGEXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGEXISTING_ItemCommand);
			this.DGEXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGEXISTING_PageIndexChanged);
			this.DGREQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGREQUEST_PageIndexChanged);

		}
		#endregion

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../HostParam.aspx?mc=99020101"); 
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			save();
		}

		private void DGEXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_ID.Text = e.Item.Cells[0].Text.Trim();					
					LBL_CODE_LAMA.Text = e.Item.Cells[0].Text.Trim();					
					TXT_DESC.Text = e.Item.Cells[1].Text.Trim();
					TXT_CD_SIBS.Text = e.Item.Cells[2].Text.Trim();
					LBL_JT_SEQ.Text = e.Item.Cells[5].Text.Trim();
					LBL_SAVEMODE.Text = "2";
					break;
				case "delete":
					TXT_ID.Text = e.Item.Cells[0].Text.Trim();					
					LBL_CODE_LAMA.Text = e.Item.Cells[0].Text.Trim();					
					TXT_DESC.Text = e.Item.Cells[1].Text.Trim();
					TXT_CD_SIBS.Text = e.Item.Cells[2].Text.Trim();
					LBL_JT_SEQ.Text = e.Item.Cells[5].Text.Trim();
					LBL_SAVEMODE.Text = "3";
					save();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGEXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGEXISTING.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 
		}

		private void DGREQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGREQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 
		
		}
	}
}
