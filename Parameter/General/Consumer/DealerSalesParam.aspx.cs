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
	/// Summary description for DealerSalesParam.
	/// </summary>
	public partial class DealerSalesParam : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				bindData1();
				bindData2();
				LBL_SAVEMODE.Text="1";
				
				conn.QueryString="select isnull(max(DEALER_ID),0)+1 DEALER_ID from RFSALES_DEALER";
				conn.ExecuteQuery();
				TXT_ID.Text = kar(4,conn.GetFieldValue("DEALER_ID"));

				conn.QueryString="select isnull(max(SALES_CODE),0)+1 SALES_CODE from RFSALES_DEALER";
				conn.ExecuteQuery();
				TXT_CODE.Text = conn.GetFieldValue("SALES_CODE");
			}
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
		}

		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();

			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void bindData1()
		{
			conn.QueryString = "select * from RFSALES_DEALER where Active='1'";
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
		
			conn.QueryString = "select * from TRFSALES_DEALER";
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
				if (this.DGR_REQUEST.Items[i].Cells[3].Text.Trim() == "0")
				{
					this.DGR_REQUEST.Items[i].Cells[3].Text = "UPDATE";
				}
				else if (this.DGR_REQUEST.Items[i].Cells[3].Text.Trim() == "1")
				{
					this.DGR_REQUEST.Items[i].Cells[3].Text = "INSERT";
				}
				else if (this.DGR_REQUEST.Items[i].Cells[3].Text.Trim() == "2")
				{
					this.DGR_REQUEST.Items[i].Cells[3].Text = "DELETE";
				}
			} 
		}

		private void clearEditBoxes()
		{
			conn.QueryString="select isnull(max(DEALER_ID),0)+1 DEALER_ID from RFSALES_DEALER";
			conn.ExecuteQuery();
			TXT_ID.Text = kar(4,conn.GetFieldValue("DEALER_ID"));

			conn.QueryString="select isnull(max(SALES_CODE),0)+1 SALES_CODE from RFSALES_DEALER";
			conn.ExecuteQuery();
			TXT_CODE.Text = conn.GetFieldValue("SALES_CODE");
			TXT_ID.Enabled=true;
			TXT_CODE.Enabled=true;
			TXT_DESC.Text="";
			LBL_ID.Text="";
			LBL_SAVEMODE.Text = "1";
		}

		private void cleansTextBox (TextBox tb)
		{
			if (tb.Text.Trim() == "&nbsp;")
				tb.Text = "";
		}

		string kar(int pjg,string str)
		{
			string tmpkar="";
			try
			{
				if (pjg >=str.Length )
				{
					int panjang = pjg -str.Length ;
					string tstr = str.ToString().Trim();
					for (int i=0; i<panjang; i++)
					{
						tstr = "0"+tstr;
					}
					tmpkar=tstr;
				}
				else 
				{
					tmpkar ="Wrong...!";
				}
				return tmpkar;
			}
			catch(Exception e){return "Wrong...!";};
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
					TXT_ID.Text = e.Item.Cells[0].Text.Trim();
					TXT_ID.Enabled=false;
					TXT_CODE.Text = e.Item.Cells[1].Text.Trim();
					TXT_CODE.Enabled = false;
					TXT_DESC.Text = e.Item.Cells[2].Text.Trim();
					LBL_ID.Text = e.Item.Cells[0].Text.Trim();
					LBL_SAVEMODE.Text = "0";
					
					cleansTextBox(TXT_ID);
					cleansTextBox(TXT_DESC);
					cleansTextBox(TXT_CODE);
					break;
				case "delete":
					conn.QueryString="select * from TRFSALES_DEALER where DEALER_ID='"+e.Item.Cells[0].Text.Trim()+"' and "+
									 "SALES_CODE ='"+e.Item.Cells[1].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="update TRFSALES_DEALER set CH_STA='2' where DEALER_ID='"+e.Item.Cells[0].Text.Trim()+"' and "+
							"SALES_CODE ='"+e.Item.Cells[1].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_GENERAL_RFSALES_DEALER '1','" +
							e.Item.Cells[0].Text.Trim()+ "', '" + e.Item.Cells[1].Text.Trim() + "','" + 
							e.Item.Cells[2].Text.Trim()+"', '','2','',''  " ;
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
					string status = e.Item.Cells[3].Text;
					if(status=="DELETE")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_ID.Text = e.Item.Cells[0].Text.Trim();
					TXT_ID.Enabled=false;
					TXT_CODE.Text = e.Item.Cells[1].Text.Trim();
					TXT_CODE.Enabled = false;
					TXT_DESC.Text = e.Item.Cells[2].Text.Trim();
					LBL_ID.Text = e.Item.Cells[0].Text.Trim();

					if(e.Item.Cells[3].Text=="UPDATE")
					{
						LBL_SAVEMODE.Text = "0";
					}
					if(e.Item.Cells[3].Text=="INSERT")
					{
						LBL_SAVEMODE.Text = "1";
					}
					
					cleansTextBox(TXT_ID);
					cleansTextBox(TXT_DESC);
					cleansTextBox(TXT_CODE);
					break;
				case "delete":
					conn.QueryString = "EXEC PARAM_GENERAL_RFSALES_DEALER '2','" +
						e.Item.Cells[0].Text.Trim()+ "', '"+e.Item.Cells[1].Text.Trim()+"','', '','','','' " ;
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
			if (LBL_SAVEMODE.Text.Trim() == "1" && LBL_ID.Text=="") //input baru
			{
				conn.QueryString = "select * from RFSALES_DEALER where DEALER_ID='"+TXT_ID.Text+"' and "+
					"SALES_CODE ='"+TXT_CODE.Text+"'";
				conn.ExecuteQuery();
				if(conn.GetRowCount() > 0)
				{
					GlobalTools.popMessage(this,"ID & Code has already been used!");
					return;
				}

				conn.QueryString = "select DEALER_ID, SALES_CODE from TRFSALES_DEALER ";
				conn.ExecuteQuery();
				for(int i=0; i<conn.GetRowCount(); i++)
				{
					if(TXT_ID.Text==conn.GetFieldValue(i,0) && TXT_CODE.Text==conn.GetFieldValue(i,1))
					{
						int IDnya = int.Parse(TXT_ID.Text)+1;
						TXT_ID.Text = kar(4,IDnya.ToString());
						int CODEnya = int.Parse(TXT_CODE.Text)+1;
						TXT_CODE.Text = CODEnya.ToString();
					}
				}

				conn.QueryString = "EXEC PARAM_GENERAL_RFSALES_DEALER '1','" +
					TXT_ID.Text+ "', '" + TXT_CODE.Text + "','" + 
					TXT_DESC.Text+"','','"+LBL_SAVEMODE.Text+"','','' " ;
				conn.ExecuteQuery();
			}		
			else if(LBL_ID.Text!="")
			{
				conn.QueryString="select * from TRFSALES_DEALER where DEALER_ID='"+TXT_ID.Text+"' and "+
					"SALES_CODE ='"+TXT_CODE.Text+"'";
				conn.ExecuteQuery();
				if(conn.GetRowCount()!=0)
				{
					conn.QueryString = "exec PARAM_GENERAL_RFSALES_DEALER '6','"+
						TXT_ID.Text+ "', '" + TXT_CODE.Text + "','" + 
						TXT_DESC.Text+"','','"+LBL_SAVEMODE.Text+"','','' " ;
					conn.ExecuteQuery();
				}
				else
				{
					conn.QueryString = "exec PARAM_GENERAL_RFSALES_DEALER '1','"+
						TXT_ID.Text+ "', '" + TXT_CODE.Text + "','" + 
						TXT_DESC.Text+"','','"+LBL_SAVEMODE.Text+"','','' " ;
					conn.ExecuteQuery();
				}
			}
			bindData2();
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}
	}
}
