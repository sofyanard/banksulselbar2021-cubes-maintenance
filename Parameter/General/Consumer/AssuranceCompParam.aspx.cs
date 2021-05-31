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
	/// Summary description for AssuranceCompParam.
	/// </summary>
	public partial class AssuranceCompParam : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();

			if (!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1";
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
			conn.QueryString = "select * from RFASURANSI "+
				"where ACTIVE='1'";
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
		
			conn.QueryString = "select * from TRFASURANSI";
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
				if (this.DGR_REQUEST.Items[i].Cells[6].Text.Trim() == "0")
				{
					this.DGR_REQUEST.Items[i].Cells[6].Text = "UPDATE";
				}
				else if (this.DGR_REQUEST.Items[i].Cells[6].Text.Trim() == "1")
				{
					this.DGR_REQUEST.Items[i].Cells[6].Text = "INSERT";
				}
				else if (this.DGR_REQUEST.Items[i].Cells[6].Text.Trim() == "2")
				{
					this.DGR_REQUEST.Items[i].Cells[6].Text = "DELETE";
				}
			} 
		}

		private void clearEditBoxes()
		{
			TXT_CODE.Text="";
			TXT_CODE.Enabled=true;
			TXT_COMP_NAME.Text="";
			TXT_ADDRESS1.Text="";
			TXT_ADDRESS2.Text="";
			TXT_ADDRESS3.Text="";
			TXT_CITY.Text="";
			TXT_ZIPCODE.Text="";
			TXT_CDSIBS.Text="";
			LBL_SEQ_ID.Text="";
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
					TXT_CODE.Text=e.Item.Cells[0].Text.Trim();
					TXT_CODE.Enabled=false;
					TXT_COMP_NAME.Text=e.Item.Cells[1].Text.Trim();
					TXT_ADDRESS1.Text=e.Item.Cells[2].Text.Trim();
					TXT_ADDRESS2.Text="";
					TXT_ADDRESS3.Text="";
					TXT_CITY.Text=e.Item.Cells[3].Text.Trim();
					TXT_ZIPCODE.Text=e.Item.Cells[4].Text.Trim();
					TXT_CDSIBS.Text=e.Item.Cells[5].Text.Trim();
					LBL_SEQ_ID.Text="";
					LBL_SAVEMODE.Text = "0";
					
					cleansTextBox(TXT_CODE);
					cleansTextBox(TXT_COMP_NAME);
					cleansTextBox(TXT_ADDRESS1);
					cleansTextBox(TXT_ADDRESS2);
					cleansTextBox(TXT_ADDRESS3);
					cleansTextBox(TXT_CITY);
					cleansTextBox(TXT_ZIPCODE);
					cleansTextBox(TXT_CDSIBS);
					break;
				case "delete":

					//LBL_SAVEMODE.Text = "2";
					//get seq_id
					conn.QueryString="select isnull(max(SEQ_ID),0)+1 MAXSEQ from TRFASURANSI";
					conn.ExecuteQuery();
					string MAXSEQID = conn.GetFieldValue("MAXSEQ");//int.Parse(conn.GetFieldValue("MAXSEQ"));

					conn.QueryString="select * from TRFASURANSI where AS_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="update TRFASURANSI set CH_STA='2' where AS_CODE='"+e.Item.Cells[0].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_GENERAL_RFASURANSI '1','" +
							MAXSEQID+ "', '" + e.Item.Cells[0].Text.Trim() + "','" + e.Item.Cells[1].Text.Trim() + "', '" + 
							e.Item.Cells[5].Text.Trim() + "','" + e.Item.Cells[2].Text.Trim() + "','', '', '" + 
							e.Item.Cells[3].Text.Trim() + "','" + e.Item.Cells[4].Text.Trim() + "','2' ,'',''";
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
					string status = e.Item.Cells[6].Text;
					if(status=="DELETE")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					TXT_CODE.Text=e.Item.Cells[0].Text.Trim();
					TXT_CODE.Enabled=false;
					TXT_COMP_NAME.Text=e.Item.Cells[1].Text.Trim();
					TXT_ADDRESS1.Text=e.Item.Cells[2].Text.Trim();
					TXT_ADDRESS2.Text="";
					TXT_ADDRESS3.Text="";
					TXT_CITY.Text=e.Item.Cells[3].Text.Trim();
					TXT_ZIPCODE.Text=e.Item.Cells[4].Text.Trim();
					TXT_CDSIBS.Text=e.Item.Cells[5].Text.Trim();
					LBL_SEQ_ID.Text=e.Item.Cells[8].Text.Trim();
					if(e.Item.Cells[6].Text=="UPDATE")
					{
						LBL_SAVEMODE.Text = "0";
					}
					if(e.Item.Cells[6].Text=="INSERT")
					{
						LBL_SAVEMODE.Text = "1";
					}
					
					cleansTextBox(TXT_CODE);
					cleansTextBox(TXT_COMP_NAME);
					cleansTextBox(TXT_ADDRESS1);
					cleansTextBox(TXT_ADDRESS2);
					cleansTextBox(TXT_ADDRESS3);
					cleansTextBox(TXT_CITY);
					cleansTextBox(TXT_ZIPCODE);
					cleansTextBox(TXT_CDSIBS);
					break;
				case "delete":
					//string userid = Session["UserID"].ToString();
					//string groupid = Session["GruopID"].ToString();
					string SEQ_ID = e.Item.Cells[8].Text.Trim();
					string AS_CODE = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "EXEC PARAM_GENERAL_RFASURANSI '2','" +
						SEQ_ID+ "', '" + AS_CODE + "','', '','', '', '','','','' ,'',''";
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
				GlobalTools.popMessage(this,"Code Tidak Boleh Kosong!");
				return;
			}

			string AS_CODE=TXT_CODE.Text;
			//string tmp = LBL_SEQ_ID.Text;
			string MAXSEQID = LBL_SEQ_ID.Text;
			if (LBL_SEQ_ID.Text=="" && LBL_SAVEMODE.Text=="1" ) //input baru
			{
				conn.QueryString = "select * from RFASURANSI where AS_CODE='"+AS_CODE+ "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "Code has already been used! Request canceled!");
					return;
				}
				
				//get seq_id
				conn.QueryString="select isnull(max(SEQ_ID),0)+1 MAXSEQID from TRFASURANSI";
				conn.ExecuteQuery();
				MAXSEQID= conn.GetFieldValue("MAXSEQID");//int.Parse(conn.GetFieldValue("MAXSEQID"));

				//cek tabel pending
				conn.QueryString = "select SEQ_ID from TRFASURANSI ";
				conn.ExecuteQuery();
				for(int i=0; i<conn.GetRowCount(); i++)
				{
					if(conn.GetFieldValue(i,0)==MAXSEQID.ToString())
					{
						int MAXSEQNO1 =int.Parse(MAXSEQID) +1;
						MAXSEQID = MAXSEQNO1.ToString();
					}
				}
				
				conn.QueryString = "EXEC PARAM_GENERAL_RFASURANSI '1','" +
					MAXSEQID+ "', '" + AS_CODE + "','" + TXT_COMP_NAME.Text + "', '" + 
					TXT_CDSIBS.Text + "','"+TXT_ADDRESS1.Text+"', '"+TXT_ADDRESS2.Text+"','"+
					TXT_ADDRESS3.Text+"', '"+TXT_CITY.Text+"', '"+TXT_ZIPCODE.Text+"','"+LBL_SAVEMODE.Text+"','',''";
				conn.ExecuteQuery();

			}	
			else
			{
				if(LBL_SEQ_ID.Text!="")//edit dari Request
				{
					conn.QueryString = "EXEC PARAM_GENERAL_RFASURANSI '6','" +
						MAXSEQID+ "', '" + AS_CODE + "','" + TXT_COMP_NAME.Text + "', '" + 
						TXT_CDSIBS.Text + "','"+TXT_ADDRESS1.Text+"', '"+TXT_ADDRESS2.Text+"','"+
						TXT_ADDRESS3.Text+"', '"+TXT_CITY.Text+"', '"+TXT_ZIPCODE.Text+"','"+LBL_SAVEMODE.Text+"','',''";
					conn.ExecuteQuery();
				}
				else //edit dari existing
				{
					conn.QueryString="select * from TRFASURANSI where AS_CODE='"+AS_CODE+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						MAXSEQID = conn.GetFieldValue("SEQ_ID");//int.Parse(conn.GetFieldValue("SEQ_ID"));
						conn.QueryString = "EXEC PARAM_GENERAL_RFASURANSI '6','" +
							MAXSEQID+ "', '" + AS_CODE + "','" + TXT_COMP_NAME.Text + "', '" + 
							TXT_CDSIBS.Text + "','"+TXT_ADDRESS1.Text+"', '"+TXT_ADDRESS2.Text+"','"+
							TXT_ADDRESS3.Text+"', '"+TXT_CITY.Text+"', '"+TXT_ZIPCODE.Text+"','"+LBL_SAVEMODE.Text+"','',''";
						conn.ExecuteQuery();
					}
					else
					{
						//get seq_id
						conn.QueryString="select isnull(max(SEQ_ID),0)+1 MAXSEQID from TRFASURANSI";
						conn.ExecuteQuery();
						MAXSEQID= conn.GetFieldValue("MAXSEQID");//int.Parse(conn.GetFieldValue("MAXSEQID"));

						conn.QueryString = "EXEC PARAM_GENERAL_RFASURANSI '1','" +
							MAXSEQID+ "', '" + AS_CODE + "','" + TXT_COMP_NAME.Text + "', '" + 
							TXT_CDSIBS.Text + "','"+TXT_ADDRESS1.Text+"', '"+TXT_ADDRESS2.Text+"','"+
							TXT_ADDRESS3.Text+"', '"+TXT_CITY.Text+"', '"+TXT_ZIPCODE.Text+"','"+LBL_SAVEMODE.Text+"','',''";
						conn.ExecuteQuery();
					}
				}
			}
			bindData2();
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}

	}
}
