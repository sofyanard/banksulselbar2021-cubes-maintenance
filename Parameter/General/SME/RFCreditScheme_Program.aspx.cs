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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFCreditScheme_Program.
	/// </summary>
	public partial class RFCreditScheme_Program : System.Web.UI.Page
	{
		protected Connection conn, con1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
			
			TXT_SEQ.ReadOnly = true;

			if (!IsPostBack)
			{
				fillDDL();
				//BindData1();
				BindData2();

			}

			Datagrid1.PageIndexChanged += new DataGridPageChangedEventHandler(this.Datagrid1_PageIndexChanged);
			Datagrid2.PageIndexChanged += new DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);
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
			this.Datagrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_ItemCommand);
			this.Datagrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid1_PageIndexChanged);
			this.Datagrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_ItemCommand);
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);

		}
		#endregion

		private void fillDDL()
		{
			GlobalTools.fillRefList(DDL_PROGRAMID,"SELECT distinct PROGRAMID,PROGRAMDESC FROM RFPROGRAM WHERE ACTIVE = '1' order by programdesc desc",false,conn);
			GlobalTools.fillRefList(DDL_CREDITSCHEMEID,"select CreditSchemeID, CreditScheme from RFCREDITSCHEMESCORING where active ='1' order by CreditScheme desc",true, conn);		

		}

		private void BindData1()
		{
			conn.QueryString = "SELECT * FROM VW_PARAM_GENERAL_RFCREDITSCHEME_PROGRAM WHERE PROGRAMID ='"+DDL_PROGRAMID.SelectedValue+"' "+
								" ORDER BY PROGRAMDESC, CREDITSCHEME  ";
			conn.ExecuteQuery();
			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			Datagrid1.DataSource = dt;
			try
			{
				Datagrid1.DataBind();
			}
			catch
			{
				Datagrid1.CurrentPageIndex = 0;
				Datagrid1.DataBind();
			}

		}

		private void BindData2()
		{
			conn.QueryString = "SELECT * FROM VW_PARAM_GENERAL_PENDING_RFCREDITSCHEME_PROGRAM "+
				" ORDER BY PROGRAMDESC, CREDITSCHEME  ";			
			conn.ExecuteQuery();
			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();

			Datagrid2.DataSource = dt;
			try
			{
				Datagrid2.DataBind();
			}
			catch
			{
				Datagrid2.CurrentPageIndex = 0;
				Datagrid2.DataBind();
			}

		}


		protected void BTN_NEW_Click(object sender, System.EventArgs e)
		{
			int seqmax= 0 ;
			con1 = conn;

			conn.QueryString = "select isnull(max(convert(int,seq)),0) seqmax from PENDING_RFCREDITSCHEME_PROGRAM ";
			conn.ExecuteQuery();
			seqmax =int.Parse(conn.GetFieldValue("seqmax"));
		
			if (seqmax == 0)
			{		
				con1.QueryString = "select isnull(max(convert(int,seq)),0) seqmax from RFCREDITSCHEME_PROGRAM ";
				con1.ExecuteQuery();
		
				seqmax =int.Parse(con1.GetFieldValue("seqmax"));
			}

			seqmax = seqmax +1;

			TXT_SEQ.Text = seqmax.ToString() ;
			clearEditBoxes();
		}

		private void clearEditBoxes()
		{
			DDL_CREDITSCHEMEID.SelectedValue ="";
			DDL_PROGRAMID.SelectedValue = "";
			LBL_SAVEMODE.Text = "1";
			BindData1();
			BindData2();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			con1 = conn;

			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select * from RFCREDITSCHEME_PROGRAM WHERE "+
					"PROGRAMID = '"+DDL_PROGRAMID.SelectedValue+"' AND CREDITSCHEMEID = '"+DDL_CREDITSCHEMEID.SelectedValue+"' ";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
				else
				{
					con1.QueryString = "select * from PENDING_RFCREDITSCHEME_PROGRAM WHERE "+
						"PROGRAMID = '"+DDL_PROGRAMID.SelectedValue+"' AND CREDITSCHEMEID = '"+DDL_CREDITSCHEMEID.SelectedValue+"' ";
					con1.ExecuteQuery();
				
					if (con1.GetRowCount() > 0) 
					{
						Tools.popMessage(this, "ID has already been used! Request canceled!");
						return;
					}
				}

			}		 

			// ubah jadi sp
			conn.QueryString = "EXEC PARAM_GENERAL_RFCREDITSCHEME_PROGRAM_MAKER '"+TXT_SEQ.Text + "','"
								+ DDL_PROGRAMID.SelectedValue + "','" + DDL_CREDITSCHEMEID.SelectedValue +"','"
								+ LBL_SAVEMODE.Text + "' ";
			conn.ExecuteQuery();


			BindData2();
			TXT_SEQ.Text = "";
			clearEditBoxes();
			
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
			TXT_SEQ.Text ="";
		}

		private void enable_kan(bool x)
		{
			TXT_SEQ.Enabled = !x;
			DDL_PROGRAMID.Enabled = !x;
			DDL_CREDITSCHEMEID.Enabled = !x;
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		private void Datagrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_SEQ.Text = e.Item.Cells[0].Text.Trim();
					try { DDL_PROGRAMID.SelectedValue = e.Item.Cells[1].Text.Trim(); } 
					catch {}
					try { DDL_CREDITSCHEMEID.SelectedValue = e.Item.Cells[2].Text.Trim(); } 
					catch {}
					enable_kan(false);

					LBL_SAVEMODE.Text = "0";
					break;
					
				case "delete":
					LBL_SAVEMODE.Text = "2";
					conn.QueryString = "INSERT INTO PENDING_RFCREDITSCHEME_PROGRAM (SEQ,PROGRAMID,CREDITSCHEMEID,PENDINGSTATUS) VALUES ('"+
						e.Item.Cells[0].Text.Trim()+"','"+
						e.Item.Cells[1].Text.Trim()+"','"+
						e.Item.Cells[2].Text.Trim()+"','2')";
					conn.ExecuteNonQuery();
					BindData2();
					LBL_SAVEMODE.Text = "1";
					break;
				
				default:
					// Do nothing.
					break;
			} 
		
		}

		private void Datagrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			Datagrid1.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			BindData1();
		
		}

		private void Datagrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			Datagrid2.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			BindData2();

		
		}

		private void Datagrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					try 
					{
						TXT_SEQ.Text = e.Item.Cells[0].Text.Trim();  
						try {DDL_PROGRAMID.SelectedValue = e.Item.Cells[1].Text.Trim();}
						catch{}
						try {DDL_CREDITSCHEMEID.SelectedValue = e.Item.Cells[2].Text.Trim();}
						catch{}
						enable_kan(false);
					} 
					catch{}

					LBL_SAVEMODE.Text = "0";
					break;
					
				case "delete":
					conn.QueryString = "DELETE PENDING_RFCREDITSCHEME_PROGRAM WHERE SEQ='"+e.Item.Cells[0].Text.Trim()+
						"' AND PROGRAMID='"+e.Item.Cells[1].Text.Trim()+
						"' AND CREDITSCHEMEID='"+e.Item.Cells[2].Text.Trim()+
						"' AND PENDINGSTATUS='"+e.Item.Cells[3].Text.Trim()+"'";
					conn.ExecuteNonQuery();
					BindData2();
					LBL_SAVEMODE.Text = "1";
					break;
				default:
					// Do nothing.
					break;
			}
		
		}

		protected void BTN_VIEW_Click(object sender, System.EventArgs e)
		{
			BindData1();
			BindData2();
		}


	}
}
