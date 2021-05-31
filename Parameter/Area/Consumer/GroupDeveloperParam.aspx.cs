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
	/// Summary description for GroupDeveloperParam.
	/// </summary>
	public partial class GroupDeveloperParam : System.Web.UI.Page
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

			BindData1();
			BindData2();
		}

		private void BindData1()
		{
			conn.QueryString = "select * from VW_PARAM_GROUPDEVELOPER_VIEWEXISTING order by gdv_name"; 
			
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
			conn.QueryString = "select * from VW_PARAM_GROUPDEVELOPER_VIEWREQUEST order by gdv_name";

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
			
			LBL_SAVEMODE.Text = "1";
			LBL_DEV_CODE.Text = "";
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

			conn.QueryString = "select isnull(max(convert(int, GDV_CODE)), 0)+ "+LBL_NB.Text+" as MAXSEQ from GROUPDEVELOPER";
			
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
			string id = "", dvcode = ""; 
			int hit = 0;

			conn.QueryString = "SELECT * FROM TGROUPDEVELOPER WHERE GDV_CODE = '"+LBL_DEV_CODE.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "UPDATE TGROUPDEVELOPER SET GDV_NAME = "+GlobalTools.ConvertNull(TXT_DEV_NAME.Text)+
					" WHERE GDV_CODE = '"+LBL_DEV_CODE.Text+"'";			
				
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "insert into TGROUPDEVELOPER values "+
					"('"+LBL_DEV_CODE.Text+"',"+GlobalTools.ConvertNull(TXT_DEV_NAME.Text)+", 2)";

				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				id = seq();
				dvcode = createseq(id); 

				conn.QueryString = "insert into TGROUPDEVELOPER values "+
					"('"+dvcode+"',"+GlobalTools.ConvertNull(TXT_DEV_NAME.Text)+", 1)";

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

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[1].Text.Trim();
					name = cleansText(e.Item.Cells[2].Text); 

					conn.QueryString = "SELECT * FROM TGROUPDEVELOPER WHERE GDV_CODE = '"+code+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "SELECT * FROM GROUPDEVELOPER WHERE GDV_CODE = '"+code+"'";
						conn.ExecuteQuery();
						
						if(conn.GetRowCount() != 0) 
						{
							try
							{
								conn.QueryString = "insert into TGROUPDEVELOPER values "+
									"('"+code+"',"+GlobalTools.ConvertNull(name)+", 3)";

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

					TXT_DEV_NAME.Text = cleansText(name); 

					LBL_SAVEMODE.Text = "2";

					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2();
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code, name;

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[1].Text.Trim();

					conn.QueryString = "DELETE FROM TGROUPDEVELOPER WHERE GDV_CODE = '"+code+"'";
					conn.ExecuteQuery();

					BindData2();
					break;
				case "edit":
					code = e.Item.Cells[1].Text.Trim();
					name = cleansText(e.Item.Cells[2].Text);
					LBL_DEV_CODE.Text = e.Item.Cells[1].Text.Trim();

					if(e.Item.Cells[4].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						TXT_DEV_NAME.Text = cleansText(name); 

						LBL_SAVEMODE.Text = "2";
					}

					break;
			}
		}
	}
}
