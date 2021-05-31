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
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.Sales
{
	/// <summary>
	/// Summary description for PTKP.
	/// </summary>
	public partial class PTKP : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=SALESMANDIRI;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;
		protected Connection conncons;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				DDL_MARITAL.Items.Add(new ListItem("--Select--",""));
				conncons.QueryString="select MARITALID,MARITALDESC from rfmarital";
				conncons.ExecuteQuery();
				for(int i=0; i<conncons.GetRowCount(); i++)
				{
					DDL_MARITAL.Items.Add(new ListItem(conncons.GetFieldValue(i,1),conncons.GetFieldValue(i,0)));
				}

				DDL_SPOUSE_WORKED.Items.Add(new ListItem("Worked","1"));
				DDL_SPOUSE_WORKED.Items.Add(new ListItem("none","0"));
				ViewExistingData();
				ViewPendingData();
			}
			
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
		}
		
		private void SetDBConn2()
		{
			conn.QueryString = "select * from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			string DB_NAMA = conn.GetFieldValue("DB_NAMA");
			string DB_IP = conn.GetFieldValue("DB_IP");
			string DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncc = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
			//connection untuk Consumer
			conn.QueryString = "select * from VW_GETCONN where MODULEID='40'";
			conn.ExecuteQuery();
			string DB_NAMA2 = conn.GetFieldValue("DB_NAMA");
			string DB_IP2 = conn.GetFieldValue("DB_IP");
			string DB_LOGINID2 = conn.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD2 = conn.GetFieldValue("DB_LOGINPWD");
			conncons = new Connection ("Data Source=" + DB_IP2 + ";Initial Catalog=" + DB_NAMA2 + ";uid=" + DB_LOGINID2 + ";pwd=" + DB_LOGINPWD2 + ";Pooling=true");
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

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void ViewExistingData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_RFPTKP";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_EXISTING.DataSource = dt;
			try
			{
				this.DGR_EXISTING.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.CurrentPageIndex-1;
					this.DGR_EXISTING.DataBind();
				}
				catch{}
			}
		}

		private void ViewPendingData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_RFPTKP";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_REQUEST.DataSource = dt;
			try
			{
				this.DGR_REQUEST.DataBind();
			}
			catch
			{
				try
				{
					this.DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.CurrentPageIndex-1;
					this.DGR_REQUEST.DataBind();
				}
				catch{}
			}
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE.Text="0";
					DDL_MARITAL.SelectedValue = cleansText(e.Item.Cells[6].Text);
					DDL_MARITAL.Enabled=false;
					TXT_CHILD_NUMB.Text = cleansText(e.Item.Cells[1].Text);
					TXT_CHILD_NUMB.Enabled=false;
					DDL_SPOUSE_WORKED.SelectedValue = cleansText(e.Item.Cells[7].Text);
					DDL_SPOUSE_WORKED.Enabled=false;
					TXT_PTK_VALUE.Text = cleansText(e.Item.Cells[8].Text);
					TXT_TAX_CODE.Text = cleansText(e.Item.Cells[4].Text);
					LBL_JENIS.Text = "edit";
					LBL_SEQ_ID.Text="";
					break;
				
				case "delete":
					//LBL_SAVE_MODE.Text="2";
					//SMEDEV2
					//get seq_id
					conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_RFPTKP";
					conncc.ExecuteQuery();
					string seq_id = conncc.GetFieldValue("SEQ_ID");
					
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_RFPTKP where AGEN_MARITAL='"+e.Item.Cells[6].Text.Trim()+"' and "+
									 " AGEN_CHILDREN='"+e.Item.Cells[1].Text+"' and SPOUSE_STATUS='"+e.Item.Cells[7].Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_RFPTKP set STATUS='2' where AGEN_MARITAL='"+e.Item.Cells[6].Text.Trim()+"' and "+
							" AGEN_CHILDREN='"+e.Item.Cells[1].Text+"' and SPOUSE_STATUS='"+e.Item.Cells[7].Text+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_RFPTKP_MAKER '2','"+
							cleansText(e.Item.Cells[6].Text) +"','"+cleansText(e.Item.Cells[1].Text)+"','"+
							cleansText(e.Item.Cells[7].Text) +"','"+cleansText(e.Item.Cells[8].Text)+"','"+
							cleansText(e.Item.Cells[4].Text) +"','1','"+seq_id+"'";
						try
						{
							conncc.ExecuteQuery();
						}
						catch { GlobalTools.popMessage (this,"Error...");}
					}
					ViewPendingData();
					BlankEntry();
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[11].Text;
					if(status=="2")
					{
						break;
					}
					DDL_MARITAL.SelectedValue = cleansText(e.Item.Cells[7].Text);
					DDL_MARITAL.Enabled=false;
					TXT_CHILD_NUMB.Text = cleansText(e.Item.Cells[1].Text);
					TXT_CHILD_NUMB.Enabled=false;
					DDL_SPOUSE_WORKED.SelectedValue = cleansText(e.Item.Cells[8].Text);
					DDL_SPOUSE_WORKED.Enabled=false;
					TXT_PTK_VALUE.Text = cleansText(e.Item.Cells[9].Text);
					TXT_TAX_CODE.Text = cleansText(e.Item.Cells[4].Text);
					LBL_JENIS.Text = "edit";
					LBL_SEQ_ID.Text = cleansText(e.Item.Cells[10].Text);
					LBL_SAVE_MODE.Text = cleansText(e.Item.Cells[11].Text);
					break;
				
				case "delete":
					//SMEDEV2
					string marital_status = cleansText(e.Item.Cells[7].Text);
					string child_numb = cleansText(e.Item.Cells[1].Text);
					string spouse_status = cleansText(e.Item.Cells[8].Text);
					string seq_id = cleansText(e.Item.Cells[10].Text);
					conncc.QueryString="Delete from PENDING_SALESCOM_RFPTKP "+
						"where AGEN_MARITAL='"+marital_status+"' and AGEN_CHILDREN='"+child_numb+"' and "+
						"SPOUSE_STATUS='"+spouse_status+"' and SEQ_ID='"+seq_id+"'"; 
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					ViewPendingData();
					BlankEntry();
					break;
			}
		}

		private void BlankEntry()
		{
			DDL_MARITAL.SelectedValue = "";
			DDL_MARITAL.Enabled=true;
			TXT_CHILD_NUMB.Text = "";
			TXT_CHILD_NUMB.Enabled=true;
			DDL_SPOUSE_WORKED.SelectedValue = "1";
			DDL_SPOUSE_WORKED.Enabled=true;
			TXT_PTK_VALUE.Text = "";
			TXT_TAX_CODE.Text = "";
			LBL_JENIS.Text = "";
			LBL_SEQ_ID.Text="";
			LBL_SAVE_MODE.Text="1";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BlankEntry();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if(DDL_SPOUSE_WORKED.SelectedValue=="")
			{
				GlobalTools.popMessage(this,"Spouse Worked Tidak Boleh Kosong");
				return;
			}
			if(TXT_CHILD_NUMB.Text=="")
			{
				GlobalTools.popMessage(this,"Number Of Children Tidak Boleh Kosong");
				return;
			}
			if(TXT_PTK_VALUE.Text=="")
			{
				GlobalTools.popMessage(this,"PTK Value Tidak Boleh Kosong");
				return;
			}

			if (LBL_JENIS.Text=="" && LBL_SEQ_ID.Text=="") //input baru
			{
				//SalesMandiri--- Membandingkan PPH_CODE baru dengan yang ada di Tabel RFPERSONAL_PPH
				conncc.QueryString="Select AGEN_MARITAL,AGEN_CHILDREN,SPOUSE_STATUS from RFPTKP";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					if(DDL_MARITAL.SelectedValue.ToString().Trim() == conncc.GetFieldValue(i,0).ToString().Trim()&& TXT_CHILD_NUMB.Text.ToString().Trim()==conncc.GetFieldValue(i,1).ToString().Trim() && DDL_SPOUSE_WORKED.SelectedValue.ToString().Trim()==conncc.GetFieldValue(i,2).ToString().Trim())
					{
						GlobalTools.popMessage(this,"AGEN_MARITAL,AGEN_CHILDREN,SPOUSE_STATUS is Existing...");
						return;
					}
				}
				//SMEDEV2--- Membandingkan PPH_CODE baru dengan yang ada di Tabel Pending
				conncc.QueryString="Select AGEN_MARITAL,AGEN_CHILDREN,SPOUSE_STATUS from PENDING_SALESCOM_RFPTKP";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					if(DDL_MARITAL.SelectedValue.ToString().Trim() == conncc.GetFieldValue(i,0).ToString().Trim()&& TXT_CHILD_NUMB.Text.ToString().Trim()==conncc.GetFieldValue(i,1).ToString().Trim() && DDL_SPOUSE_WORKED.SelectedValue.ToString().Trim()==conncc.GetFieldValue(i,2).ToString().Trim())
					{
						GlobalTools.popMessage(this,"AGEN_MARITAL,AGEN_CHILDREN,SPOUSE_STATUS is Existing...");
						return;
					}
				}

				//get seq_id
				conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_RFPTKP";
				conncc.ExecuteQuery();
				LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");

				conncc.QueryString="PARAM_SALESCOM_RFPTKP_MAKER '1','"+
					DDL_MARITAL.SelectedValue +"','"+TXT_CHILD_NUMB.Text+"','"+
					DDL_SPOUSE_WORKED.SelectedValue+"','"+cleansText(TXT_PTK_VALUE.Text)+"','"+cleansText(TXT_TAX_CODE.Text)+"','1','"+
					LBL_SEQ_ID.Text+"'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			else if (LBL_JENIS.Text=="edit") //edit
			{
				if(LBL_SEQ_ID.Text!="")//edit dari Request
				{
					conncc.QueryString="PARAM_SALESCOM_RFPTKP_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						DDL_MARITAL.SelectedValue +"','"+TXT_CHILD_NUMB.Text+"','"+
						DDL_SPOUSE_WORKED.SelectedValue+"','"+cleansText(TXT_PTK_VALUE.Text)+"','"+cleansText(TXT_TAX_CODE.Text)+"','1','"+
						LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
				else if(LBL_SEQ_ID.Text=="")//edit dari Existing
				{
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_RFPTKP where AGEN_MARITAL='"+DDL_MARITAL.SelectedValue+"' and "+
						" AGEN_CHILDREN='"+TXT_CHILD_NUMB.Text+"' and SPOUSE_STATUS='"+DDL_SPOUSE_WORKED.SelectedValue+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//get seq_id
						conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from PENDING_SALESCOM_RFPTKP";
						conncc.ExecuteQuery();
						LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
					}
					else
					{
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					
					conncc.QueryString="PARAM_SALESCOM_RFPTKP_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						DDL_MARITAL.SelectedValue +"','"+TXT_CHILD_NUMB.Text+"','"+
						DDL_SPOUSE_WORKED.SelectedValue+"','"+cleansText(TXT_PTK_VALUE.Text)+"','"+
						cleansText(TXT_TAX_CODE.Text)+"','1','"+LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
			}
			LBL_SAVE_MODE.Text="1";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../SalesParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

	}
}

