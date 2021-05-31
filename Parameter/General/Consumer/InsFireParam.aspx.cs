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
	/// Summary description for InsFireParam.
	/// </summary>
	public partial class InsFireParam : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2; // = new Connection("Data Source=10.123.12.30;Initial Catalog=CUBESDEVNET;uid=sa;pwd=dmscorp");

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
			this.DGR_EXISTING.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_REQUEST_PageIndexChanged);
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
		}

		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private string getPendingStatus(string saveMode) 
		{
			string status = "";			
			switch (saveMode)
			{
				case "0":
					status = "Update";
					break;
				case "1":
					status = "Insert";
					break;
				case "2":
					status = "Delete";
					break;
				default:
					status = "";
					break;
			}
			return status;
		}

		private void bindData1()
		{
			conn.QueryString = "select SEQ_NO,RATE_YEAR,RATE_CLASS,RATE_VALUE,TIPE from  MANDIRI_FIRE_RATE "+
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
		
			conn.QueryString = "select * from TMANDIRI_FIRE_RATE";
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
			TXT_RATE_YEAR.Text="";
			TXT_RATE_CLASS.Text="";
			TXT_RATE_VALUE.Text="";
			LBL_SAVEMODE.Text = "1";
			//activatePostBackControls(true);
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
			this.DGR_EXISTING.CurrentPageIndex=e.NewPageIndex;
			bindData1();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex=e.NewPageIndex;
			bindData2();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					TXT_RATE_YEAR.Text=e.Item.Cells[0].Text.Trim();
					TXT_RATE_CLASS.Text=e.Item.Cells[1].Text.Trim();
					TXT_RATE_VALUE.Text=e.Item.Cells[2].Text.Trim();
					LBL_SEQ_NO.Text = e.Item.Cells[4].Text.Trim();
					LBL_SEQ_ID.Text="";
					LBL_SAVEMODE.Text = "0";
					
					cleansTextBox(TXT_RATE_YEAR);
					cleansTextBox(TXT_RATE_CLASS);
					cleansTextBox(TXT_RATE_VALUE);
					//activatePostBackControls(false);
					break;
				case "delete":

					//LBL_SAVEMODE.Text = "2";
					//get seq_id
					conn.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 MAXSEQ from TMANDIRI_FIRE_RATE";
					conn.ExecuteQuery();
					string MAXSEQID = kar(4,conn.GetFieldValue("MAXSEQ"));
					string Rclass = GlobalTools.ConvertFloat(e.Item.Cells[1].Text.Trim());
					string Rvalue = GlobalTools.ConvertFloat(e.Item.Cells[2].Text.Trim());
					conn.QueryString="select * from TMANDIRI_FIRE_RATE where SEQ_NO='"+e.Item.Cells[4].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="update TMANDIRI_FIRE_RATE set CH_STA='2' where SEQ_NO='"+e.Item.Cells[4].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_GENERAL_MANDIRI_FIRE_RATE '1','" +
							MAXSEQID+ "', '" + e.Item.Cells[4].Text.Trim() + "','" + e.Item.Cells[0].Text.Trim() + "', '" + 
							Rclass + "','"+Rvalue+"', '"+e.Item.Cells[5].Text.Trim()+"', '2' ,'','' " ;
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
					TXT_RATE_YEAR.Text=e.Item.Cells[0].Text.Trim();
					TXT_RATE_CLASS.Text=e.Item.Cells[1].Text.Trim();
					TXT_RATE_VALUE.Text=e.Item.Cells[2].Text.Trim();
					LBL_SEQ_NO.Text = e.Item.Cells[6].Text.Trim();
					LBL_SEQ_ID.Text=e.Item.Cells[5].Text.Trim();
					if(e.Item.Cells[3].Text=="UPDATE")
					{
						LBL_SAVEMODE.Text = "0";
					}
					if(e.Item.Cells[3].Text=="INSERT")
					{
						LBL_SAVEMODE.Text = "1";
					}
					
					cleansTextBox(TXT_RATE_YEAR);
					cleansTextBox(TXT_RATE_CLASS);
					cleansTextBox(TXT_RATE_VALUE);
					//activatePostBackControls(false);
					break;
				case "delete":
					string SEQ_ID = e.Item.Cells[5].Text.Trim();
					string SEQ_NO = e.Item.Cells[6].Text.Trim();
					string Ryear = e.Item.Cells[0].Text.Trim();
					string Rclass = e.Item.Cells[1].Text.Trim();

					conn.QueryString = "EXEC PARAM_GENERAL_MANDIRI_FIRE_RATE '2','" +
						SEQ_ID+ "', '" + SEQ_NO + "','" + Ryear + "', '" + 
						Rclass + "','', '', '' ,'','' " ;
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
			if(TXT_RATE_YEAR.Text=="" || TXT_RATE_CLASS.Text=="" || TXT_RATE_CLASS.Text=="")
			{
				GlobalTools.popMessage(this,"Data Tidak Boleh Kosong!");
				return;
			}

			string MAXSEQNO=LBL_SEQ_NO.Text;
			string MAXSEQID=LBL_SEQ_ID.Text;
			if (LBL_SEQ_ID.Text=="" && LBL_SEQ_NO.Text=="") 
			{
				//get seq_no
				conn.QueryString="select isnull(max(convert(int, SEQ_NO)), 0)+1 MAXSEQNO from MANDIRI_FIRE_RATE";
				conn.ExecuteQuery();
				MAXSEQNO= kar(4,conn.GetFieldValue("MAXSEQNO"));

				conn.QueryString = "select * from MANDIRI_FIRE_RATE where SEQ_NO='"+MAXSEQNO+ "' and "+
					"RATE_YEAR='"+TXT_RATE_YEAR.Text+"' and RATE_CLASS='"+TXT_RATE_CLASS.Text+"'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "Rate Year & Rate Class has already been used! Request canceled!");
					return;
				}
				
				//get seq_id
				conn.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 MAXSEQID from TMANDIRI_FIRE_RATE";
				conn.ExecuteQuery();
				MAXSEQID= kar(4,conn.GetFieldValue("MAXSEQID"));

				//cek tabel pending
				conn.QueryString = "select SEQ_NO from TMANDIRI_FIRE_RATE ";
				conn.ExecuteQuery();
				for(int i=0; i<conn.GetRowCount(); i++)
				{
					if(conn.GetFieldValue(i,0)==MAXSEQNO)
					{
						int MAXSEQNO1 = int.Parse(MAXSEQNO)+1;
						MAXSEQNO = kar(4,MAXSEQNO1.ToString());
					}
				}

				string Rclass = GlobalTools.ConvertFloat(TXT_RATE_CLASS.Text);
				string Rvalue = GlobalTools.ConvertFloat(TXT_RATE_VALUE.Text); 
				conn.QueryString = "EXEC PARAM_GENERAL_MANDIRI_FIRE_RATE '1','" +
					MAXSEQID+ "', '" + MAXSEQNO + "','" + TXT_RATE_YEAR.Text + "', '" + 
					Rclass + "','"+Rvalue+"', '', '"+LBL_SAVEMODE.Text+"' ,'','' " ;
				conn.ExecuteQuery();

			}	
			else
			{
				if(LBL_SEQ_ID.Text!="")//edit dari Request
				{
					string Rclass = GlobalTools.ConvertFloat(TXT_RATE_CLASS.Text);
					string Rvalue = GlobalTools.ConvertFloat(TXT_RATE_VALUE.Text); 
					conn.QueryString = "EXEC PARAM_GENERAL_MANDIRI_FIRE_RATE '6','" +
						MAXSEQID+ "', '" + MAXSEQNO + "','" + TXT_RATE_YEAR.Text + "', '" + 
						Rclass + "','"+Rvalue+"', '', '"+LBL_SAVEMODE.Text+"' ,'','' " ;
					conn.ExecuteQuery();
				}
				else //edit dari existing
				{
					conn.QueryString="select * from TMANDIRI_FIRE_RATE where SEQ_NO='"+MAXSEQNO+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						MAXSEQID = conn.GetFieldValue("SEQ_ID");
						string Rclass = GlobalTools.ConvertFloat(TXT_RATE_CLASS.Text);
						string Rvalue = GlobalTools.ConvertFloat(TXT_RATE_VALUE.Text); 
						conn.QueryString = "EXEC PARAM_GENERAL_MANDIRI_FIRE_RATE '6','" +
							MAXSEQID+ "', '" + MAXSEQNO + "','" + TXT_RATE_YEAR.Text + "', '" + 
							Rclass + "','"+Rvalue+"', '', '"+LBL_SAVEMODE.Text+"' ,'','' " ;
						conn.ExecuteQuery();
					}
					else
					{
						//get seq_id
						conn.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 MAXSEQID from TMANDIRI_FIRE_RATE";
						conn.ExecuteQuery();
						MAXSEQID= kar(4,conn.GetFieldValue("MAXSEQID"));

						string Rclass = GlobalTools.ConvertFloat(TXT_RATE_CLASS.Text);
						string Rvalue = GlobalTools.ConvertFloat(TXT_RATE_VALUE.Text); 
						conn.QueryString = "EXEC PARAM_GENERAL_MANDIRI_FIRE_RATE '1','" +
							MAXSEQID+ "', '" + MAXSEQNO + "','" + TXT_RATE_YEAR.Text + "', '" + 
							Rclass + "','"+Rvalue+"', '', '"+LBL_SAVEMODE.Text+"' ,'','' " ;
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
