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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for App_Parameter.
	/// </summary>
	public partial class App_Parameter : System.Web.UI.Page
	{
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1";
				bindData1();
				bindData2();
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
			this.DGR_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_ItemCommand);
			this.DGR_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

		}
		#endregion

		private void bindData1()
		{
			conn.QueryString = "select * from APP_PARAMETER";
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
				try
				{
					this.DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.CurrentPageIndex - 1;
					this.DGR_EXISTING.DataBind();
				}
				catch{}
			}
		}

		private void bindData2()
		{
		
			conn.QueryString = "select * from PENDING_APP_PARAMETER";
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
				try
				{
					this.DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.CurrentPageIndex - 1;
					this.DGR_REQUEST.DataBind();
				}
				catch{}
			}
			
			for (int i = 0; i < DGR_REQUEST.Items.Count; i++)
			{
				if (DGR_REQUEST.Items[i].Cells[5].Text.Trim() == "0")
				{
					DGR_REQUEST.Items[i].Cells[5].Text = "UPDATE";
				}
				else if (DGR_REQUEST.Items[i].Cells[5].Text.Trim() == "1")
				{
					DGR_REQUEST.Items[i].Cells[5].Text = "INSERT";
				}
				else if (DGR_REQUEST.Items[i].Cells[5].Text.Trim() == "2")
				{
					DGR_REQUEST.Items[i].Cells[5].Text = "DELETE";
				}
			} 
		}

		private void clearEditBoxes()
		{
			TXT_PS.Text="";
			TXT_FS.Text="";
			TXT_ACQ.Text="";
			LBL_SAVEMODE.Text = "1";
			LBL_SEQ.Text="";
		}

		private void cleansTextBox (TextBox tb)
		{
			if (tb.Text.Trim() == "&nbsp;")
				tb.Text = "";
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			bindData1();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			bindData2();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_PS.Text=e.Item.Cells[2].Text;
					TXT_FS.Text=e.Item.Cells[3].Text;
					TXT_ACQ.Text=e.Item.Cells[4].Text;
					LBL_SAVEMODE.Text="1";
					LBL_SEQ.Text = e.Item.Cells[0].Text;
					cleansTextBox(TXT_PS);
					cleansTextBox(TXT_FS);
					cleansTextBox(TXT_ACQ);
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_PS.Text=e.Item.Cells[2].Text;
					TXT_FS.Text=e.Item.Cells[3].Text;
					TXT_ACQ.Text=e.Item.Cells[4].Text;
					LBL_SAVEMODE.Text="1";
					LBL_SEQ.Text = e.Item.Cells[0].Text;
					cleansTextBox(TXT_PS);
					cleansTextBox(TXT_FS);
					cleansTextBox(TXT_ACQ);
					break;
				case "delete":
					//SMEDEV2
					string seq=e.Item.Cells[0].Text;
					conn.QueryString="Delete from PENDING_APP_PARAMETER where SEQ='"+seq+"'"; 
					try
					{
						conn.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					bindData2();
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if(LBL_SEQ.Text=="")
			{
				GlobalTools.popMessage(this,"Edit Only....");
				return;
			}
			else
			{
				conn.QueryString="select * from PENDING_APP_PARAMETER where SEQ='"+LBL_SEQ.Text+"'";
				conn.ExecuteQuery();
				if(conn.GetRowCount()!=0)
				{
					conn.QueryString="update PENDING_APP_PARAMETER set MAX_PS_COUNT='"+TXT_PS.Text+"',MAX_FS_COUNT='"+
						TXT_FS.Text+"',MAX_ACQ_COUNT='"+TXT_ACQ.Text+"' where SEQ='"+LBL_SEQ.Text+"'";
					conn.ExecuteQuery();
				}
				else
				{
					conn.QueryString="insert into PENDING_APP_PARAMETER (SEQ,MAX_PS_COUNT,MAX_FS_COUNT,MAX_ACQ_COUNT,PENDINGSTATUS) values ('"+LBL_SEQ.Text+"','"+
						TXT_PS.Text+"','"+TXT_FS.Text+"','"+TXT_ACQ.Text+"','0')";
					conn.ExecuteQuery();
				}
				clearEditBoxes();
				bindData2();
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}

	}
}
