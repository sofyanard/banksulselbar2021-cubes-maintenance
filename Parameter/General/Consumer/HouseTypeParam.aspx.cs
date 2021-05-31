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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for HouseTypeParam.
	/// </summary>
	public partial class HouseTypeParam : System.Web.UI.Page
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
		
		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
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

		private void BindData1()
		{
			conn.QueryString = "select type_code,type_name,description,active from HOUSE_TYPE where active='1' order by type_code";
			conn.ExecuteQuery();

			if (conn.GetRowCount() != 0)
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				DG_HOUSE.DataSource = dt;

				try
				{
					DG_HOUSE.DataBind();
				}
				catch 
				{
					DG_HOUSE.CurrentPageIndex = DG_HOUSE.PageCount - 1;
					DG_HOUSE.DataBind();
				}				
			} 
			conn.ClearData();
		}

		private void BindData2()
		{
			conn.QueryString = "select type_code,type_name,description,ch_sta from THOUSE_TYPE order by type_code";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG_THOUSE.DataSource = dt;

			try
			{
				DG_THOUSE.DataBind();
			}
			catch 
			{
				DG_THOUSE.CurrentPageIndex = DG_THOUSE.PageCount - 1;
				DG_THOUSE.DataBind();
			}

			for (int i = 0; i < DG_THOUSE.Items.Count; i++)
			{
				Label status = (Label)DG_THOUSE.Items[i].Cells[3].FindControl("LBL_STATUS");
				//no.Text = (i+1+(DGRequest.CurrentPageIndex)*DGRequest.PageSize).ToString();
				if (DG_THOUSE.Items[i].Cells[4].Text.Trim() == "1")
				{
					status.Text = "INSERT";
				}
				else if (DG_THOUSE.Items[i].Cells[4].Text.Trim() == "2")
				{
					status.Text = "UPDATE";
				}
				else if (DG_THOUSE.Items[i].Cells[4].Text.Trim() == "3")
				{
					status.Text = "DELETE";
				}				
			} 
		}

		private void save()
		{
			if(LBL_SAVEMODE.Text.Trim() == "1")
			{
				conn.QueryString = "select count(*) jml from HOUSE_TYPE where TYPE_CODE = '"+TXT_TYPE_CODE.Text.Trim()+"'";
				conn.ExecuteQuery();

				if(conn.GetFieldValue("jml").Trim() == "0")
				{
					conn.QueryString = "insert into THOUSE_TYPE (TYPE_CODE,TYPE_NAME,DESCRIPTION,CH_STA) "+
						"values ('"+TXT_TYPE_CODE.Text+"','"+TXT_TYPE_NAME.Text+"','"+TXT_DESC.Text+"','"+LBL_SAVEMODE.Text+"')";
					conn.ExecuteNonQuery();
				}
				else 
				{
					GlobalTools.popMessage(this,"Duplicate Data");
				}
			}
			if(LBL_SAVEMODE.Text.Trim() == "2" || LBL_SAVEMODE.Text.Trim() == "3")
			{
				conn.QueryString = "select count(*) jml from THOUSE_TYPE where TYPE_CODE = '"+TXT_TYPE_CODE.Text.Trim()+"'";
				conn.ExecuteQuery();

				if(conn.GetFieldValue("jml").Trim() == "0")
				{
					conn.QueryString = "select count(*) jml from HOUSE_TYPE where TYPE_CODE = '"+TXT_TYPE_CODE.Text.Trim()+"'";
					conn.ExecuteQuery();

					if(conn.GetFieldValue("jml").Trim() == "0" ||(conn.GetFieldValue("jml").Trim() != "0" && TXT_TYPE_CODE.Text.Trim() == LBL_CODE_LAMA.Text))
					{
						conn.QueryString = "insert into THOUSE_TYPE (TYPE_CODE,TYPE_NAME,DESCRIPTION,CH_STA,TYPE_CODE_LAMA) "+
							"values ('"+TXT_TYPE_CODE.Text+"','"+TXT_TYPE_NAME.Text+"','"+TXT_DESC.Text+"','"+LBL_SAVEMODE.Text+"','"+LBL_CODE_LAMA.Text+"')";
						conn.ExecuteNonQuery();
					}
					else GlobalTools.popMessage(this,"Duplicate Data");
				}
				else 
				{
					conn.QueryString = "UPDATE THOUSE_TYPE set TYPE_CODE = '"+TXT_TYPE_CODE.Text+"',TYPE_NAME = '"+TXT_TYPE_NAME.Text+"',DESCRIPTION = '"+TXT_DESC.Text+"',CH_STA = '"+LBL_SAVEMODE.Text+"',TYPE_CODE_LAMA = '"+LBL_CODE_LAMA.Text+"' "+
						"where TYPE_CODE= '"+LBL_CODE_LAMA.Text+"'";					                 	
					conn.ExecuteNonQuery();
				}
			}
			LBL_CODE_LAMA.Text = "";
			TXT_TYPE_CODE.Text = "";
			TXT_DESC.Text = "";
			TXT_TYPE_NAME.Text = "";
			LBL_SAVEMODE.Text = "1";
			BindData1();
			BindData2();
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
			this.DG_HOUSE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG_HOUSE_ItemCommand);
			this.DG_HOUSE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_HOUSE_PageIndexChanged);
			this.DG_THOUSE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_THOUSE_PageIndexChanged);

		}
		#endregion

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40");	
		}

		private void DG_HOUSE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_HOUSE.CurrentPageIndex = e.NewPageIndex;
			BindData1();
		}

		private void DG_THOUSE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_THOUSE.CurrentPageIndex = e.NewPageIndex;
			BindData2();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{			
			save();
		}

		private void DG_HOUSE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_TYPE_CODE.Text = e.Item.Cells[0].Text.Trim();
					LBL_CODE_LAMA.Text = e.Item.Cells[0].Text.Trim();
					TXT_DESC.Text = e.Item.Cells[2].Text.Trim();
					TXT_TYPE_NAME.Text = e.Item.Cells[1].Text.Trim();
					LBL_SAVEMODE.Text = "2";
					break;
				case "delete":
					TXT_TYPE_CODE.Text = e.Item.Cells[0].Text.Trim();
					LBL_CODE_LAMA.Text = e.Item.Cells[0].Text.Trim();
					TXT_DESC.Text = e.Item.Cells[2].Text.Trim();
					TXT_TYPE_NAME.Text = e.Item.Cells[1].Text.Trim();
					LBL_SAVEMODE.Text = "3";
					save();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			LBL_CODE_LAMA.Text = "";
			TXT_TYPE_CODE.Text = "";
			TXT_DESC.Text = "";
			TXT_TYPE_NAME.Text = "";
			LBL_SAVEMODE.Text = "1";
			LBL_ID.Text = "";
		}
	}
}
