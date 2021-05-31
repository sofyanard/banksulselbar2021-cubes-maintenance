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
	/// Summary description for TrackListParam.
	/// </summary>
	public partial class TrackListParam : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				DDL_STATUS.Items.Add(new ListItem("--Pilih--",""));
				DDL_STATUS.Items.Add(new ListItem("No","0"));
				DDL_STATUS.Items.Add(new ListItem("Yes","1"));
				bindData1();
				bindData2();
				Remark.Visible=false;
			}
			
		}

		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void bindData1()
		{
			conn.QueryString = "select * from RFTRACKLST where active='1'";
			conn.ExecuteQuery();
			this.DGR_EXISTING.DataSource = conn.GetDataTable().Copy();
			try 
			{
				this.DGR_EXISTING.DataBind();
			}
			catch 
			{
				this.DGR_EXISTING.CurrentPageIndex = this.DGR_EXISTING.PageCount - 1;
				this.DGR_EXISTING.DataBind();
			}
		}

		private void bindData2()
		{
		
			conn.QueryString = "select * from TRFTRACKLST";
			conn.ExecuteQuery();
			this.DGR_REQUEST.DataSource = conn.GetDataTable().Copy();
			try 
			{
				this.DGR_REQUEST.DataBind();
			}
			catch 
			{
				this.DGR_REQUEST.CurrentPageIndex = this.DGR_REQUEST.PageCount - 1;
				this.DGR_REQUEST.DataBind();
			}
			
			for (int i = 0; i < this.DGR_REQUEST.Items.Count; i++)
			{
				if (this.DGR_REQUEST.Items[i].Cells[4].Text.Trim() == "0")
				{
					this.DGR_REQUEST.Items[i].Cells[4].Text = "UPDATE";
				}
				else if (this.DGR_REQUEST.Items[i].Cells[4].Text.Trim() == "1")
				{
					this.DGR_REQUEST.Items[i].Cells[4].Text = "INSERT";
				}
				else if (this.DGR_REQUEST.Items[i].Cells[4].Text.Trim() == "2")
				{
					this.DGR_REQUEST.Items[i].Cells[4].Text = "DELETE";
				}
			} 
		}

		private void clearEditBoxes()
		{
			DDL_STATUS.SelectedValue="";
			TXT_CODE.Text="";
			TXT_CODE.Enabled=true;
			TXT_DESC.Text="";
			TXT_TARGET.Text="";
			TXT_MAXTIME.Text="";
			TXT_REMARK.Text="";
			LBL_SAVEMODE.Text = "1";
		}

		private void cleansTextBox (TextBox tb)
		{
			if (tb.Text.Trim() == "&nbsp;")
				tb.Text = "";
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40");
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
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_CODE.Enabled=false;
					conn.QueryString="select * from RFTRACKLST where TR_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					DDL_STATUS.SelectedValue = conn.GetFieldValue("SEND_STATUS");
					TXT_REMARK.Text = conn.GetFieldValue("REMARK");
					TXT_DESC.Text = e.Item.Cells[1].Text.Trim();
					TXT_TARGET.Text = e.Item.Cells[2].Text.Trim();
					TXT_MAXTIME.Text = e.Item.Cells[3].Text.Trim();
					
					LBL_SAVEMODE.Text = "0";
					
					cleansTextBox(TXT_CODE);
					cleansTextBox(TXT_DESC);
					cleansTextBox(TXT_TARGET);
					cleansTextBox(TXT_MAXTIME);
					cleansTextBox(TXT_REMARK);
					//activatePostBackControls(false);
					break;
				case "delete":
					conn.QueryString="select * from TRFTRACKLST where TR_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="update TRFTRACKLST set CH_STA='2' where TR_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						int target=int.Parse(e.Item.Cells[2].Text.Trim());
						int maxtime = int.Parse(e.Item.Cells[3].Text.Trim());
						conn.QueryString = "EXEC PARAM_GENERAL_RFTRACKLST '1','" +
							e.Item.Cells[0].Text.Trim()+ "', '" + e.Item.Cells[1].Text.Trim() + "','" + target + "', '" + 
							maxtime+"', '"+DDL_STATUS.SelectedValue+"','"+TXT_REMARK.Text+"','2' ,'','' " ;
						conn.ExecuteQuery();
					}
					bindData2();
					break;
				default:
					// Do nothing.
					break;
			} 
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[4].Text;
					if(status=="DELETE")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_CODE.Enabled=false;
					conn.QueryString="select * from TRFTRACKLST where TR_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					DDL_STATUS.SelectedValue = conn.GetFieldValue("SEND_STATUS");
					TXT_REMARK.Text = conn.GetFieldValue("REMARK");
					TXT_DESC.Text = e.Item.Cells[1].Text.Trim();
					TXT_TARGET.Text = e.Item.Cells[2].Text.Trim();
					TXT_MAXTIME.Text = e.Item.Cells[3].Text.Trim();

					if(e.Item.Cells[4].Text=="UPDATE")
					{
						LBL_SAVEMODE.Text = "0";
					}
					if(e.Item.Cells[4].Text=="INSERT")
					{
						LBL_SAVEMODE.Text = "1";
					}
					
					cleansTextBox(TXT_CODE);
					cleansTextBox(TXT_DESC);
					cleansTextBox(TXT_TARGET);
					cleansTextBox(TXT_MAXTIME);
					cleansTextBox(TXT_REMARK);
					break;
				case "delete":
					string CODE = e.Item.Cells[0].Text.Trim();
					conn.QueryString = "EXEC PARAM_GENERAL_RFTRACKLST '2','" +
						CODE+ "', '','', '','','','' , '',''" ;
					conn.ExecuteQuery();
					bindData2();
					break;
				default:
					// Do nothing.
					break;
			}  
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if(TXT_CODE.Text=="")
			{
				GlobalTools.popMessage(this,"Code Tidak Boleh Kosong!!");
				return;
			}
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select * from RFTRACKLST where TR_CODE='"+TXT_CODE.Text+"'";
				conn.ExecuteQuery();
				if(conn.GetRowCount() > 0)
				{
					GlobalTools.popMessage(this,"Code has already been used!");
					return;
				}
				conn.QueryString = "select * from TRFTRACKLST where TR_CODE='"+TXT_CODE.Text+"'";
				conn.ExecuteQuery();
				if(conn.GetRowCount() > 0)
				{
					GlobalTools.popMessage(this,"Code has already been used!");
					return;
				}
			}		
			int target=int.Parse(TXT_TARGET.Text);
			int maxtime = int.Parse(TXT_MAXTIME.Text);
			conn.QueryString="select * from TRFTRACKLST where TR_CODE='"+TXT_CODE.Text+"'";
			conn.ExecuteQuery();
			if(conn.GetRowCount()!=0)
			{
				conn.QueryString = "exec PARAM_GENERAL_RFTRACKLST '6','"+
					TXT_CODE.Text+ "', '" + TXT_DESC.Text + "','" + target + "', '" + 
					maxtime+"', '"+DDL_STATUS.SelectedValue+"','"+TXT_REMARK.Text+"','"+LBL_SAVEMODE.Text+"','',''";
				conn.ExecuteQuery();
			}
			else
			{
				conn.QueryString = "exec PARAM_GENERAL_RFTRACKLST '1','"+
					TXT_CODE.Text+ "', '" + TXT_DESC.Text + "','" + target + "', '" + 
					maxtime+"', '"+DDL_STATUS.SelectedValue+"','"+TXT_REMARK.Text+"','"+LBL_SAVEMODE.Text+"','',''";
				conn.ExecuteQuery();
			}
			bindData2();
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}

		protected void LNK_REMARK_Click(object sender, System.EventArgs e)
		{
			if(Remark.Visible==true)
			{
				Remark.Visible=false;
			}
			else
			{
				Remark.Visible=true;
			}
		}

	}
}
