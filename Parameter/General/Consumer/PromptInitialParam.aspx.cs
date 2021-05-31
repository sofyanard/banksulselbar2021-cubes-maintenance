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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for PromptInitialParam.
	/// </summary>
	public partial class PromptInitialParam : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				this.DDL_CHECK_TYPE.Items.Add(new ListItem("Basic","1"));
				this.DDL_CHECK_TYPE.Items.Add(new ListItem("Intermediate","2"));
				this.DDL_CHECK_TYPE.Items.Add(new ListItem("Advance","3"));
				fillGrid();
				fillGridPending();
			}
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");	
		}

		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void fillGrid()
		{
			conn.QueryString="select TYPE_CHECK,SEQ,BLACKLIST_RESULT,case when TYPE_CHECK='1' then 'Basic' when TYPE_CHECK='2' then 'Intermediate' "+
							 "when TYPE_CHECK='3' then 'Advance' end TYPE_CHECK1 from PRM_INIT order by SEQ";
			conn.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
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
		private void BlankEntry()
		{
			this.TXT_SEQ.Text = "";
			this.chk_ExistDB.Checked=false;
			this.chk_Deb_Macet.Checked=false;
			this.chk_IDI.Checked=false;
			this.chk_DHBI.Checked=false;
			this.chk_BL_CC.Checked=false;
			this.chk_Reject_List.Checked=false;
			this.chk_Other.Checked=false;
			this.TXT_RESULT_AGENCY.Text="";
			this.chk_Appl.Checked=false;
			TXT_SEQ.Enabled=true;
			LBL_SAVE.Text="1";
		}

		private void fillGridPending()
		{
			conn.QueryString="select SEQ, TYPE_CHECK, BLACKLIST_RESULT, "+
				"case when TYPE_CHECK='1' then 'Basic' when TYPE_CHECK='2' then 'Intermediate' "+
				"when TYPE_CHECK='3' then 'Advance' end TYPE_CHECK1, "+
				"CH_STA, STATUS = case CH_STA when '1' then 'INSERT' "+
				"when '2' then 'UPDATE' when '3' then 'DELETE' end "+
				"from TPRM_INIT order by SEQ";
			conn.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
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

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BlankEntry();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string seq = this.TXT_SEQ.Text;
			string TypeCheck =this.DDL_CHECK_TYPE.SelectedValue;
			string existdb="";
			if(this.chk_ExistDB.Checked==true)
			{
				existdb="1";
			}
			string debmacet="";
			if(this.chk_Deb_Macet.Checked==true)
			{
				debmacet="1";
			}
			string idi="";
			if(this.chk_IDI.Checked==true)
			{
				idi="1";
			}
			string dhbi="";
			if(this.chk_DHBI.Checked==true)
			{
				dhbi="1";
			}
			string bcc="";
			if(this.chk_BL_CC.Checked==true)
			{
				bcc="1";
			}
			string rl="";
			if(this.chk_Reject_List.Checked==true)
			{
				rl="1";
			}
			string other="";
			if(this.chk_Other.Checked==true)
			{
				other="1";
			}
			string result =this.TXT_RESULT_AGENCY.Text;
			string existapl="";
			if(this.chk_Appl.Checked==true)
			{
				existapl="1";
			}

			int hit = 0;

			if(TXT_SEQ.Text!="1")
			{
				TXT_SEQ.Text="1";
				seq="1";
			}

			conn.QueryString = "SELECT * FROM PRM_INIT WHERE SEQ = '"+TXT_SEQ.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if(hit != 0 )//&& (LBL_SAVE.Text == "2"))
			{
				conn.QueryString = "SELECT * FROM TPRM_INIT WHERE SEQ = '"+TXT_SEQ.Text+"'";
				conn.ExecuteQuery();
				hit = conn.GetRowCount();
				
				if(hit==0)
				{
					conn.QueryString = "insert into TPRM_INIT"+
						" (SEQ,TYPE_CHECK,EXISTDB_CHECK,DEBMACET_CHECK,IDI_CHECK,DHBI_CHECK,BCC_CHECK,RL_CHECK, "+
						" OTHER_CHECK,BLACKLIST_RESULT,EXISTAPL_CHECK, LASTSEQWARM, CH_STA, CD_TMP) values "+ 
						"('"+seq+"','"+TypeCheck+"','"+existdb+"','"+debmacet+"','"+idi+"','"+dhbi+"','"+
						""+bcc+"','"+rl+"','"+other+"','"+result+"','"+existapl+"','','2','')";
					conn.ExecuteQuery();
 
					BlankEntry(); 
				}
				else if(hit != 0)
				{
					conn.QueryString = "update TPRM_INIT set SEQ='"+seq+"', TYPE_CHECK='"+TypeCheck+"',EXISTDB_CHECK='"+existdb+"',"+
						"DEBMACET_CHECK='"+debmacet+"',IDI_CHECK='"+idi+"',DHBI_CHECK='"+dhbi+"',BCC_CHECK='"+bcc+"',"+
						"RL_CHECK='"+rl+"',OTHER_CHECK='"+other+"',BLACKLIST_RESULT='"+result+"',EXISTAPL_CHECK='"+existapl+"',"+
						"LASTSEQWARM='',CH_STA='2',CD_TMP=''";
					conn.ExecuteQuery();
				}
				
			}
			else if((hit == 0) )//&& (LBL_SAVE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Maaf, Anda Tidak Bisa Input Data Baru!!");
				BlankEntry(); 
				return;
			}
			conn.ClearData();
 	
			fillGrid();
			fillGridPending();
			LBL_SAVE.Text = "1"; 
			GlobalTools.popMessage(this,"Data is Requiring Approval");
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40");
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			fillGrid();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					conn.QueryString = "SELECT * FROM PRM_INIT WHERE SEQ = '"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery(); 

					if(conn.GetRowCount() != 0)
					{
						this.TXT_SEQ.Text = conn.GetFieldValue("SEQ");
						this.DDL_CHECK_TYPE.SelectedValue = conn.GetFieldValue("TYPE_CHECK");
						if(conn.GetFieldValue("EXISTDB_CHECK")=="1")
						{
							this.chk_ExistDB.Checked=true;
						}
						else
						{
							this.chk_ExistDB.Checked=false;
						}
						if(conn.GetFieldValue("DEBMACET_CHECK")=="1")
						{
							this.chk_Deb_Macet.Checked=true;
						}
						else
						{
							this.chk_Deb_Macet.Checked=false;
						}
						if(conn.GetFieldValue("IDI_CHECK")=="1")
						{
							this.chk_IDI.Checked=true;
						}
						else
						{
							this.chk_IDI.Checked=false;
						}
						if(conn.GetFieldValue("DHBI_CHECK")=="1")
						{
							this.chk_DHBI.Checked=true;
						}
						else
						{
							this.chk_DHBI.Checked=false;
						}
						if(conn.GetFieldValue("BCC_CHECK")=="1")
						{
							this.chk_BL_CC.Checked=true;
						}
						else
						{
							this.chk_BL_CC.Checked=false;
						}
						if(conn.GetFieldValue("RL_CHECK")=="1")
						{
							this.chk_Reject_List.Checked=true;
						}
						else
						{
							this.chk_Reject_List.Checked=false;
						}
						if(conn.GetFieldValue("OTHER_CHECK")=="1")
						{
							this.chk_Other.Checked=true;
						}
						else
						{
							this.chk_Other.Checked=false;
						}
						this.TXT_RESULT_AGENCY.Text=conn.GetFieldValue("BLACKLIST_RESULT");
						if(conn.GetFieldValue("EXISTAPL_CHECK")=="1")
						{
							this.chk_Appl.Checked=true;
						}
						else
						{
							this.chk_Appl.Checked=false;
						}
					}

					TXT_SEQ.Enabled = false;					
					LBL_SAVE.Text = "2";		
					break;

			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			fillGridPending();
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					string seq=e.Item.Cells[0].Text;
					conn.QueryString="Delete from TPRM_INIT where SEQ='"+seq+"'"; 
					try
					{
						conn.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					fillGridPending();
					break;
			}
		}

	}
}
