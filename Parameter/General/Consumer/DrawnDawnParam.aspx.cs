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
	/// Summary description for DrawnDawnParam.
	/// </summary>
	public partial class DrawnDawnParam : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TXT_MAXTIME;
		protected Connection conn;
		protected Connection conn2;
	
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
			conn.QueryString = "select * from RFDRAWDAWN";
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

			LinkButton lnkEdit, lnkDel;
			Label lblSta;
			for (int i = 0; i < this.DGR_EXISTING.Items.Count; i++)
			{
				if (DGR_EXISTING.Items[i].Cells[4].Text == "0")
				{
					lnkEdit = (LinkButton)DGR_EXISTING.Items[i].Cells[3].FindControl("lnk_RfEdit");
					lnkDel = (LinkButton)DGR_EXISTING.Items[i].Cells[3].FindControl("lnk_RfDelete");
					lblSta = (Label)DGR_EXISTING.Items[i].Cells[3].FindControl("lbl_Status");

					lnkEdit.Visible = false;
					lnkDel.Visible = false;
					lblSta.ForeColor = System.Drawing.Color.Red;
					lblSta.Text = "DELETED";
				}
			}
		}

		private void bindData2()
		{
		
			conn.QueryString = "select * from TRFDRAWDAWN ";
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
			
			TXT_DD_CODE.Text="";
			TXT_DD_CODE.Enabled=true;
			TXT_DD_DESC.Text="";
			TXT_PERCENTAGE.Text="";
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
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);

		}
		#endregion

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


		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40");
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_DD_CODE.Text = e.Item.Cells[0].Text.Trim();
					TXT_DD_CODE.Enabled=false;
					conn.QueryString="select * from RFDRAWDAWN where DD_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					
					TXT_DD_CODE.Enabled = false;
					TXT_DD_DESC.Text = conn.GetFieldValue("DD_DESC");
					TXT_PERCENTAGE.Text = conn.GetFieldValue("PERCENTAGE");					
					LBL_SAVEMODE.Text = "0";
										
					break;
				case "delete":
					conn.QueryString="select * from TRFDRAWDAWN where DD_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="update TRFDRAWDAWN set CH_STA='2' where DD_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_GENERAL_RFDRAWDAWN_MAKER '" +
							e.Item.Cells[0].Text.Trim()+ "','" + e.Item.Cells[1].Text.Trim() + "','" + e.Item.Cells[2].Text.Trim() + "','2'" ;
						conn.ExecuteQuery();
					}
					bindData2();
					break;
				default:
					
					break;
			} 
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":

					conn.QueryString="select * from TRFDRAWDAWN where DD_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();

					string status = e.Item.Cells[3].Text;
					if(status=="DELETE")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						TXT_DD_CODE.Text = conn.GetFieldValue("DD_CODE");
						TXT_DD_CODE.Enabled=false;
						TXT_DD_DESC.Text = conn.GetFieldValue("DD_DESC");
						TXT_PERCENTAGE.Text = conn.GetFieldValue("PERCENTAGE");

						if(e.Item.Cells[3].Text=="UPDATE")
						{
							LBL_SAVEMODE.Text = "0";
						}
						if(e.Item.Cells[3].Text=="INSERT")
						{
							LBL_SAVEMODE.Text = "1";
						}
					}
						break;
				case "delete":
						string CODE = e.Item.Cells[0].Text.Trim();

						conn.QueryString = "DELETE FROM TRFDRAWDAWN WHERE DD_CODE = '"+CODE+"'";
						conn.ExecuteQuery();
						bindData2();
						break;
				default:
						break;			
			}  
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;
			int hit2 = 0;

			conn.QueryString = "SELECT DD_CODE FROM RFDRAWDAWN WHERE DD_CODE = '"+TXT_DD_CODE.Text+"'";
			conn.ExecuteQuery();
			hit2 = conn.GetRowCount();

			conn.QueryString = "SELECT DD_CODE FROM TRFDRAWDAWN WHERE DD_CODE = '"+TXT_DD_CODE.Text+"' and CH_STA <> '3'";
			conn.ExecuteQuery();
			hit = conn.GetRowCount();

				if ((hit != 0) && (LBL_SAVEMODE.Text != "1"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_RFDRAWDAWN_MAKER '"+TXT_DD_CODE.Text+"', '"+TXT_DD_DESC.Text+"', "+GlobalTools.ConvertFloat(TXT_PERCENTAGE.Text)+
						", '0'";
					conn.ExecuteNonQuery();
					clearEditBoxes();
				}
				else if ((hit == 0) && (LBL_SAVEMODE.Text == "0"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_RFDRAWDAWN_MAKER '"+TXT_DD_CODE.Text+"', '"+TXT_DD_DESC.Text+"', "+GlobalTools.ConvertFloat(TXT_PERCENTAGE.Text)+
						", '0'";
					conn.ExecuteNonQuery();
					clearEditBoxes();
				}
				else if ((hit == 0) && (LBL_SAVEMODE.Text == "1") && (hit2 == 0))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_RFDRAWDAWN_MAKER '"+TXT_DD_CODE.Text+"', '"+TXT_DD_DESC.Text+"', "+GlobalTools.ConvertFloat(TXT_PERCENTAGE.Text)+
						", '1'";
					conn.ExecuteNonQuery();
					clearEditBoxes();
				}
				else if ((hit == 0) && (LBL_SAVEMODE.Text == "1") && (hit2 != 0))
				{
					GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
					return;
				}
				else if((hit != 0) && (LBL_SAVEMODE.Text == "1") && (TXT_DD_CODE.Enabled == false))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_RFDRAWDAWN_MAKER '"+TXT_DD_CODE.Text+"', '"+TXT_DD_DESC.Text+"', "+GlobalTools.ConvertFloat(TXT_PERCENTAGE.Text)+
						", '1'";
					conn.ExecuteNonQuery();
					clearEditBoxes();
				}
				else if((hit != 0) && (LBL_SAVEMODE.Text == "1") && (TXT_DD_CODE.Enabled == true))
				{
					GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
					return;
				}
			conn.ClearData();
			bindData2();
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

	}
}
