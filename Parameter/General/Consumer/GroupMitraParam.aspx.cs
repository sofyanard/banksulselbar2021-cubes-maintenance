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
	/// Summary description for GroupMitraParam.
	/// </summary>
	public partial class GroupMitraParam : System.Web.UI.Page
	{
		protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetDBConn2();
			if (!IsPostBack)
			{
				LBL_SAVEMODE.Text = "1";
				DDL_GRP_NAME.Items.Add(new ListItem("--Select--",""));
				conn.QueryString="select GroupID, SG_GRPNAME from SCGROUP";
				conn.ExecuteQuery();
				for (int i=0; i<conn.GetRowCount(); i++)
				{
					DDL_GRP_NAME.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
				}
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
			conn.QueryString = "select a.SEQ_NO, a.SG_CODE, a.MP_RATETYPE, a.MP_LIMIT1, a.MP_LIMIT2, b.SG_GRPNAME, "+
				"case when a.MP_RATETYPE='1' then 'Red' when a.MP_RATETYPE='2' then 'Green' when a.MP_RATETYPE='3' then 'Yellow' else 'No Data' end MP_RATETYPE1 "+
				"from SCMITRAPLAFOND a left join SCGROUP b on a.SG_CODE = b.GROUPID "+
				"where a.ACTIVE='1'";
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
		
			conn.QueryString = "select a.SEQ_ID, a.SEQ_NO,a.SG_CODE, a.MP_RATETYPE, a.MP_LIMIT1, "+
				"a.MP_LIMIT2, a.CH_STA, b.SG_GRPNAME, "+
				"case when a.MP_RATETYPE='1' then 'Red' when a.MP_RATETYPE='2' then 'Green' when a.MP_RATETYPE='3' then 'Yellow' else 'No Data' end MP_RATETYPE1 "+
				"from TSCMITRAPLAFOND a left join SCGROUP b on a.SG_CODE = b.GROUPID";
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
			try {RB_PROD_TYPE.SelectedItem.Selected = false;}
			catch{}
			DDL_GRP_NAME.SelectedValue="";
			RB_PROD_TYPE.Enabled=true;
			DDL_GRP_NAME.Enabled=true;
			TXT_LIMIT1.Text="";
			TXT_LIMIT2.Text="";
			LBL_SEQ_ID.Text="";
			LBL_SEQ_NO.Text="";
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
					try {RB_PROD_TYPE.SelectedValue = e.Item.Cells[7].Text.Trim();}
					catch{}
					DDL_GRP_NAME.SelectedValue=e.Item.Cells[6].Text.Trim();
					RB_PROD_TYPE.Enabled=false;
					DDL_GRP_NAME.Enabled=false;
					TXT_LIMIT1.Text=e.Item.Cells[2].Text.Trim();
					TXT_LIMIT2.Text=e.Item.Cells[3].Text.Trim();
					LBL_SEQ_ID.Text="";
					LBL_SEQ_NO.Text = e.Item.Cells[5].Text.Trim();
					LBL_SAVEMODE.Text = "0";
					
					cleansTextBox(TXT_LIMIT1);
					cleansTextBox(TXT_LIMIT2);
					break;
				case "delete":

					//LBL_SAVEMODE.Text = "2";
					//get seq_id
					conn.QueryString="select isnull(max(SEQ_ID),0)+1 MAXSEQ from TSCMITRAPLAFOND";
					conn.ExecuteQuery();
					string MAXSEQID = conn.GetFieldValue("MAXSEQ");

					conn.QueryString="select * from TSCMITRAPLAFOND where SEQ_NO='"+e.Item.Cells[5].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						string SEQ_ID=conn.GetFieldValue("SEQ_ID");
						conn.QueryString="update TSCMITRAPLAFOND set CH_STA='2' where SEQ_NO='"+e.Item.Cells[5].Text.Trim()+"' and "+
										 "SEQ_ID="+SEQ_ID+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_GENERAL_SCMITRAPLAFOND '1','" +
							MAXSEQID+ "', '" + e.Item.Cells[5].Text.Trim() + "','" + e.Item.Cells[6].Text.Trim() + "','" + 
							e.Item.Cells[7].Text.Trim() + "','" + e.Item.Cells[2].Text.Trim() + "','" + 
							e.Item.Cells[3].Text.Trim() + "','2' " ;
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
					try {RB_PROD_TYPE.SelectedValue = e.Item.Cells[9].Text.Trim();}
					catch{}
					DDL_GRP_NAME.SelectedValue=e.Item.Cells[8].Text.Trim();
					RB_PROD_TYPE.Enabled=false;
					DDL_GRP_NAME.Enabled=false;
					TXT_LIMIT1.Text=e.Item.Cells[2].Text.Trim();
					TXT_LIMIT2.Text=e.Item.Cells[3].Text.Trim();
					LBL_SEQ_ID.Text=e.Item.Cells[7].Text.Trim();
					LBL_SEQ_NO.Text=e.Item.Cells[6].Text.Trim();
					if(e.Item.Cells[4].Text=="UPDATE")
					{
						LBL_SAVEMODE.Text = "0";
					}
					if(e.Item.Cells[4].Text=="INSERT")
					{
						LBL_SAVEMODE.Text = "1";
					}
					
					cleansTextBox(TXT_LIMIT1);
					cleansTextBox(TXT_LIMIT2);
					break;
				case "delete":
					string SEQ_ID = e.Item.Cells[7].Text.Trim();
					string SEQ_NO = e.Item.Cells[6].Text.Trim();

					conn.QueryString = "EXEC PARAM_GENERAL_RFASURANSI '2','" +
						SEQ_ID+ "', '" + SEQ_NO + "','', '','', '', '' " ;
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
			if(DDL_GRP_NAME.SelectedValue=="" && RB_PROD_TYPE.SelectedValue=="")
			{
				GlobalTools.popMessage(this,"Rating Type & Group Name Tidak Boleh Kosong!");
				return;
			}

			string MAXSEQID = LBL_SEQ_ID.Text;
			string SEQ_NO = LBL_SEQ_NO.Text;
			if (LBL_SEQ_ID.Text=="" && LBL_SAVEMODE.Text=="1" ) //input baru
			{
				//get seq_no
				conn.QueryString="select isnull(max(SEQ_NO),0)+1 MAXSEQ from SCMITRAPLAFOND";
				conn.ExecuteQuery();
				SEQ_NO=conn.GetFieldValue("MAXSEQ");

				conn.QueryString = "select * from SCMITRAPLAFOND where SEQ_NO='"+SEQ_NO+ "' and "+
								   "SG_CODE='"+DDL_GRP_NAME.SelectedValue+"' and "+
								   "MP_RATETYPE='"+RB_PROD_TYPE.SelectedValue+"'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "Rating Type & Group Name has already been used! Request canceled!");
					return;
				}
				
				//get seq_id
				conn.QueryString="select isnull(max(SEQ_ID),0)+1 MAXSEQ from TSCMITRAPLAFOND";
				conn.ExecuteQuery();
				MAXSEQID= conn.GetFieldValue("MAXSEQ");

				//cek SEQ_NO ditabel pending
				conn.QueryString = "select SEQ_NO from TSCMITRAPLAFOND ";
				conn.ExecuteQuery();
				for(int i=0; i<conn.GetRowCount(); i++)
				{
					if(conn.GetFieldValue(i,0)==SEQ_NO.ToString())
					{
						int MAXSEQNO1 =int.Parse(SEQ_NO) +1;
						SEQ_NO = MAXSEQNO1.ToString();
					}
				}
				
				conn.QueryString = "EXEC PARAM_GENERAL_SCMITRAPLAFOND '1','" +
					MAXSEQID+ "', '" + SEQ_NO + "','" + DDL_GRP_NAME.SelectedValue + "','" + 
					RB_PROD_TYPE.SelectedValue.ToString() + "','" + GlobalTools.ConvertFloat(TXT_LIMIT1.Text) + "','" + 
					GlobalTools.ConvertFloat(TXT_LIMIT2.Text) + "','"+LBL_SAVEMODE.Text+"' " ;
				conn.ExecuteQuery();

			}	
			else
			{
				if(LBL_SEQ_ID.Text!="")//edit dari Request
				{
					conn.QueryString = "EXEC PARAM_GENERAL_SCMITRAPLAFOND '6','" +
						MAXSEQID+ "', '" + SEQ_NO + "','" + DDL_GRP_NAME.SelectedValue + "','" + 
						RB_PROD_TYPE.SelectedValue.ToString() + "','" + GlobalTools.ConvertFloat(TXT_LIMIT1.Text) + "','" + 
						GlobalTools.ConvertFloat(TXT_LIMIT2.Text) + "','"+LBL_SAVEMODE.Text+"' " ;
					conn.ExecuteQuery();
				}
				else //edit dari existing
				{
					conn.QueryString="select * from TSCMITRAPLAFOND where SEQ_NO='"+SEQ_NO+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						MAXSEQID = conn.GetFieldValue("SEQ_ID");
						conn.QueryString = "EXEC PARAM_GENERAL_SCMITRAPLAFOND '6','" +
							MAXSEQID+ "', '" + SEQ_NO + "','" + DDL_GRP_NAME.SelectedValue + "','" + 
							RB_PROD_TYPE.SelectedValue.ToString() + "','" + GlobalTools.ConvertFloat(TXT_LIMIT1.Text) + "','" + 
							GlobalTools.ConvertFloat(TXT_LIMIT2.Text) + "','"+LBL_SAVEMODE.Text+"' " ;
						conn.ExecuteQuery();
					}
					else
					{
						//get seq_id
						conn.QueryString="select isnull(max(SEQ_ID),0)+1 MAXSEQ from TSCMITRAPLAFOND";
						conn.ExecuteQuery();
						MAXSEQID= conn.GetFieldValue("MAXSEQ");

						conn.QueryString = "EXEC PARAM_GENERAL_RFASURANSI '1','" +
							MAXSEQID+ "', '" + SEQ_NO + "','" + DDL_GRP_NAME.SelectedValue + "','" + 
							RB_PROD_TYPE.SelectedValue.ToString() + "','" + GlobalTools.ConvertFloat(TXT_LIMIT1.Text) + "','" + 
							GlobalTools.ConvertFloat(TXT_LIMIT2.Text) + "','"+LBL_SAVEMODE.Text+"' " ;
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
