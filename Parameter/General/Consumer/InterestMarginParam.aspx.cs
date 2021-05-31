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
	/// Summary description for InterestMarginParam.
	/// </summary>
	public partial class InterestMarginParam : System.Web.UI.Page
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
			conn.QueryString = "select IM_SEQ, MIN_LIMIT , MAX_LIMIT, IM_VALUE, convert(varchar,IM_VALUE)+'%' IM_VALUE1 from INTEREST_MARGIN where active='1'"+ 
							   "order by MIN_LIMIT";
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
			for (int i = 0; i < this.DGR_EXISTING.Items.Count; i++)
			{
				this.DGR_EXISTING.Items[i].Cells[1].Text = this.DGR_EXISTING.Items[i].Cells[1].Text.Trim();
			} 
			for (int i = 0; i < this.DGR_EXISTING.Items.Count; i++)
			{
				this.DGR_EXISTING.Items[i].Cells[2].Text = this.DGR_EXISTING.Items[i].Cells[2].Text.Trim();
			} 
		}

		private void bindData2()
		{
		
			conn.QueryString = "select IM_SEQ, MIN_LIMIT, MAX_LIMIT, convert(varchar,IM_VALUE)+'%' IM_VALUE1, IM_VALUE, CH_STA from TINTEREST_MARGIN "+
							   "order by MIN_LIMIT";
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
				this.DGR_REQUEST.Items[i].Cells[1].Text = this.DGR_REQUEST.Items[i].Cells[1].Text.Trim();
			} 
			for (int i = 0; i < this.DGR_REQUEST.Items.Count; i++)
			{
				this.DGR_REQUEST.Items[i].Cells[2].Text = this.DGR_REQUEST.Items[i].Cells[2].Text.Trim();
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
			TXT_MIN.Text="";
			TXT_MAX.Text="";
			TXT_INT.Text="";
			LBL_SAVEMODE.Text = "1";
			LBL_SEQ.Text="";
			//activatePostBackControls(true);
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
					TXT_MIN.Text=e.Item.Cells[1].Text.Trim();
					TXT_MAX.Text=e.Item.Cells[2].Text.Trim();
					TXT_INT.Text =e.Item.Cells[5].Text.Trim();
					LBL_SAVEMODE.Text = "0";
					LBL_SEQ.Text = e.Item.Cells[0].Text.Trim();
					
					cleansTextBox(TXT_MIN);
					cleansTextBox(TXT_MAX);
					cleansTextBox(TXT_INT);
					break;
				case "delete":
					conn.QueryString="select * from TINTEREST_MARGIN where IM_SEQ='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="update TINTEREST_MARGIN set CH_STA='2' where IM_SEQ='"+e.Item.Cells[0].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						string Min_limit = GlobalTools.ConvertFloat(e.Item.Cells[1].Text.Trim());
						string Max_limit = GlobalTools.ConvertFloat(e.Item.Cells[2].Text.Trim());
						string Im_value = GlobalTools.ConvertFloat(e.Item.Cells[5].Text.Trim());
						conn.QueryString = "EXEC PARAM_GENERAL_INTEREST_MARGIN '1','" +
							e.Item.Cells[0].Text.Trim()+ "', '" + Min_limit + "','" + Max_limit + "', '" + 
							Im_value+"', '2' ,'','' " ;
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
					TXT_MIN.Text=e.Item.Cells[1].Text.Trim();
					TXT_MAX.Text=e.Item.Cells[2].Text.Trim();
					TXT_INT.Text =e.Item.Cells[6].Text.Trim();
					LBL_SEQ.Text = e.Item.Cells[0].Text.Trim();
					if(e.Item.Cells[4].Text=="UPDATE")
					{
						LBL_SAVEMODE.Text = "0";
					}
					if(e.Item.Cells[4].Text=="INSERT")
					{
						LBL_SAVEMODE.Text = "1";
					}
					
					cleansTextBox(TXT_MIN);
					cleansTextBox(TXT_MAX);
					cleansTextBox(TXT_INT);
					break;
				case "delete":
					int IM_SEQ = int.Parse(e.Item.Cells[0].Text.Trim());
					conn.QueryString = "EXEC PARAM_GENERAL_INTEREST_MARGIN '2','" +
						IM_SEQ+ "', '','', '','','','' " ;
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
			if(TXT_MIN.Text=="")
			{
				GlobalTools.popMessage(this,"Please input Minimum Limit...");
				return;
			}
			if(TXT_MAX.Text=="")
			{
				GlobalTools.popMessage(this,"Please input Maximum Limit...");
				return;
			}
			if(TXT_INT.Text=="")
			{
				GlobalTools.popMessage(this,"Please input Interest Rate...");
				return;
			}

			if (LBL_SEQ.Text == "") //(LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select isnull(max(convert(int, IM_SEQ)), 0)+1 IM_SEQ from INTEREST_MARGIN";
				conn.ExecuteQuery();
				LBL_SEQ.Text = conn.GetFieldValue("IM_SEQ");

				//cek maxSeq di TINTEREST_MARGIN
				conn.QueryString = "select IM_SEQ from TINTEREST_MARGIN ";
				conn.ExecuteQuery();
				for(int i=0; i<conn.GetRowCount(); i++)
				{
					if(LBL_SEQ.Text.Trim()==conn.GetFieldValue(i,0).Trim())
					{
						int imSeq = int.Parse(LBL_SEQ.Text)+1;
						LBL_SEQ.Text = imSeq.ToString();
					}
				}
			}		

			string minLimit = GlobalTools.ConvertFloat(TXT_MIN.Text);
			string maxLimit = GlobalTools.ConvertFloat(TXT_MAX.Text);
			string imValue = GlobalTools.ConvertFloat(TXT_INT.Text);

			conn.QueryString="select * from TINTEREST_MARGIN where IM_SEQ='"+LBL_SEQ.Text+"'";
			conn.ExecuteQuery();
			if(conn.GetRowCount()!=0)
			{
				conn.QueryString = "exec PARAM_GENERAL_INTEREST_MARGIN '6','"+ LBL_SEQ.Text +"','" +
					minLimit + "','" + maxLimit + "','" +
					imValue +"','"+LBL_SAVEMODE.Text+"','',''";
				conn.ExecuteQuery();
			}
			else
			{
				conn.QueryString = "exec PARAM_GENERAL_INTEREST_MARGIN '1','"+ LBL_SEQ.Text +"','" +
					minLimit + "','" + maxLimit + "','" +
					imValue +"','"+LBL_SAVEMODE.Text+"','',''";
				conn.ExecuteQuery();
			}
			bindData2();
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}
	}
}
