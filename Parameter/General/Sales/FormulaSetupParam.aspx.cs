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
	/// Summary description for FormulaSetupParam.
	/// </summary>
	public partial class FormulaSetupParam : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=SALESMANDIRI;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				DDL_CALC_TYPE.Items.Add(new ListItem("--Choose--",""));
				conncc.QueryString="select CALCULATION_ID, CALCULATION_DESC, isnull(CAL_TRACK,'') CAL_TRACK, "+
					"isnull(CAL_FIELDDATE,'') CAL_FIELDDATE, isnull(CAL_FIELDTRACK,'') CAL_FIELDTRACK "+
					"from CALCULATION_TYPE";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_CALC_TYPE.Items.Add(new ListItem(conncc.GetFieldValue(i,1),conncc.GetFieldValue(i,0)));
				}

				DDL_AGENT_TYPE.Items.Add(new ListItem("--Choose--",""));
				conncc.QueryString="select AGENTYPE_ID, AGENTYPE_DESC from AGENTTYPE ";
				conncc.ExecuteQuery();
				for(int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_AGENT_TYPE.Items.Add(new ListItem(conncc.GetFieldValue(i,1),conncc.GetFieldValue(i,0)));
				}
				
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
			if (tb.Trim() == "&nbsp;" || tb.Trim() == "&nbsp" )
				tb = "";
			return tb;
		}

		private void ViewExistingData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_CALCULATION_FORMULA order by SEQ_NO";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
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

		private void ViewPendingData()
		{
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_CALCULATION_FORMULA order by SEQ_NO";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			DGR_REQUEST.DataSource = dt;
			this.DGR_REQUEST.DataBind();
			for (int i = 0; i < this.DGR_REQUEST.Items.Count; i++)
			{
				this.DGR_REQUEST.Items[i].Cells[1].Text = (i+1).ToString();
			}
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex=e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex=e.NewPageIndex;
			ViewPendingData();
		}

		private void BlankEntry()
		{
			TXT_NUM.Text="";
			TXT_NUM.Enabled=true;
			DDL_CALC_TYPE.SelectedValue="";
			DDL_AGENT_TYPE.SelectedValue="";
			TXT_CALC_NAME.Text="";
			TXT_CALC_FORMULA.Text="";
			TXT_CALC_TABLE.Text="";
			TXT_CALC_LINK.Text="";
			TXT_CALC_GROUP.Text="";
			LBL_JENIS.Text="";
			LBL_SEQ_NO.Text="";
			LBL_SEQ_ID.Text="";
			try {RBL_RESULT_TYPE.SelectedItem.Selected = false;}
			catch{}
			try {RBL_ACTIVE.SelectedItem.Selected = false;}
			catch{}
			LBL_SAVE_MODE.Text="1";
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE.Text="0";
					string seqno=e.Item.Cells[0].Text;
					conncc.QueryString="select * from CALCULATION_FORMULA where seq_no='"+seqno+"'";
					conncc.ExecuteQuery();
					TXT_NUM.Text=cleansText(conncc.GetFieldValue("SEQ_NO"));
					TXT_NUM.Enabled=false;
					DDL_CALC_TYPE.SelectedValue=cleansText(conncc.GetFieldValue("CALCULATION_ID"));
					DDL_AGENT_TYPE.SelectedValue=cleansText(conncc.GetFieldValue("AGENTYPE_ID"));
					TXT_CALC_NAME.Text=cleansText(conncc.GetFieldValue("FORMULA_NAME"));
					TXT_CALC_FORMULA.Text=cleansText(conncc.GetFieldValue("FORMULA"));
					TXT_CALC_TABLE.Text=cleansText(conncc.GetFieldValue("FORMULA_TABLE"));
					TXT_CALC_LINK.Text=cleansText(conncc.GetFieldValue("FORMULA_LINK"));
					TXT_CALC_GROUP.Text=cleansText(conncc.GetFieldValue("FORMULA_GROUP"));
					try {RBL_RESULT_TYPE.SelectedValue = cleansText(conncc.GetFieldValue("RESULT_TYPE"));}
					catch{}
					try {RBL_ACTIVE.SelectedValue = cleansText(conncc.GetFieldValue("FORMULA_ACTIVE"));}
					catch{}
					LBL_JENIS.Text="edit";
					LBL_SEQ_NO.Text=cleansText(conncc.GetFieldValue("SEQ_NO"));
					LBL_SEQ_ID.Text="";//edit dari Existing seq_id kosong....
					break;
				case "delete":
					//get seq_id
					//LBL_SAVE_MODE.Text="2";
					conncc.QueryString="select isnull(max(seq_id),0)+1 no from PENDING_SALESCOM_CALCULATION_FORMULA";
					conncc.ExecuteQuery();
					int seq_id = int.Parse(conncc.GetFieldValue("no"));
					//SMEDEV2..
					//cek tabel pending
					conncc.QueryString="select * from PENDING_SALESCOM_CALCULATION_FORMULA where SEQ_NO='"+e.Item.Cells[0].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update PENDING_SALESCOM_CALCULATION_FORMULA set STATUS='2' where SEQ_NO='"+e.Item.Cells[0].Text.Trim()+"'";
						conncc.ExecuteQuery();
						//GlobalTools.popMessage(this,"SEQ_ID is Existing...");
						//return;
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_CALCULATION_FORMULA_MAKER '2','"+
							e.Item.Cells[0].Text +"','"+ e.Item.Cells[6].Text +"','','','','','"+
							e.Item.Cells[7].Text +"','"+ e.Item.Cells[2].Text +"','"+ e.Item.Cells[10].Text +"','"+
							e.Item.Cells[11].Text +"','"+ e.Item.Cells[12].Text +"','"+ e.Item.Cells[8].Text +"','"+
							e.Item.Cells[9].Text +"','"+ e.Item.Cells[13].Text +"','"+ seq_id +"'";
						try
						{
							conncc.ExecuteQuery();
						}
						catch { GlobalTools.popMessage (this,"Error...");}
					}
					ViewPendingData();
					break;
			}
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[13].Text;
					if(status=="2")
					{
						break;
					}
					string seq_no=e.Item.Cells[11].Text;
					string seq_id = e.Item.Cells[0].Text;
					conncc.QueryString="select * from PENDING_SALESCOM_CALCULATION_FORMULA "+
						"where SEQ_NO='"+seq_no+"' and SEQ_ID='"+ seq_id +"'";
					conncc.ExecuteQuery();
					TXT_NUM.Text=cleansText(conncc.GetFieldValue("SEQ_NO"));
					TXT_NUM.Enabled=false;
					DDL_CALC_TYPE.SelectedValue=cleansText(conncc.GetFieldValue("CALCULATION_ID"));
					DDL_AGENT_TYPE.SelectedValue=cleansText(conncc.GetFieldValue("AGENTYPE_ID"));
					TXT_CALC_NAME.Text=cleansText(conncc.GetFieldValue("FORMULA_NAME"));
					TXT_CALC_FORMULA.Text=cleansText(conncc.GetFieldValue("FORMULA"));
					TXT_CALC_TABLE.Text=cleansText(conncc.GetFieldValue("FORMULA_TABLE"));
					TXT_CALC_LINK.Text=cleansText(conncc.GetFieldValue("FORMULA_LINK"));
					TXT_CALC_GROUP.Text=cleansText(conncc.GetFieldValue("FORMULA_GROUP"));
					try {RBL_RESULT_TYPE.SelectedValue = cleansText(conncc.GetFieldValue("RESULT_TYPE"));}
					catch{}
					try {RBL_ACTIVE.SelectedValue = cleansText(conncc.GetFieldValue("FORMULA_ACTIVE"));}
					catch{}
					LBL_JENIS.Text="edit";
					LBL_SEQ_NO.Text=cleansText(conncc.GetFieldValue("SEQ_NO"));
					LBL_SEQ_ID.Text=cleansText(conncc.GetFieldValue("SEQ_ID"));
					LBL_SAVE_MODE.Text=cleansText(conncc.GetFieldValue("STATUS"));
					break;
				case "delete":
					//SMEDEV2
					int seqno=int.Parse(e.Item.Cells[11].Text);
					int seqid = int.Parse(e.Item.Cells[0].Text);
					conncc.QueryString="Delete from PENDING_SALESCOM_CALCULATION_FORMULA "+
						"where SEQ_NO='"+seqno+"' and SEQ_ID='"+ seqid +"'";
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					ViewPendingData();
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BlankEntry();
		}

		protected void BTN_NEW_Click(object sender, System.EventArgs e)
		{
			BlankEntry();
			conncc.QueryString="select isnull(max(seq_no),0)+1 no from CALCULATION_FORMULA";
			conncc.ExecuteQuery();
			TXT_NUM.Text=conncc.GetFieldValue("no");
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_JENIS.Text=="" && LBL_SEQ_ID.Text=="" && LBL_SEQ_NO.Text=="") //input baru
			{
				//SalesMandiri--Cek tabel Existing
				conncc.QueryString="select * from CALCULATION_FORMULA where SEQ_NO='"+TXT_NUM.Text+"'";
				conncc.ExecuteQuery();
				if(conncc.GetRowCount()!=0)
				{
					GlobalTools.popMessage(this,"CALCULATION ID is Existing..");
					return;
				}
				//get seq_id
				conncc.QueryString="select isnull(max(seq_id),0)+1 no from PENDING_SALESCOM_CALCULATION_FORMULA";
				conncc.ExecuteQuery();
				int seq_id = int.Parse(conncc.GetFieldValue("no"));
				
				//SMEDEV2---Jika SEQ_NO baru sudah ada di tabel pending
				string SEQ_NO="";
				conncc.QueryString="select * from PENDING_SALESCOM_CALCULATION_FORMULA where SEQ_NO='"+TXT_NUM.Text+"'";
				conncc.ExecuteQuery();
				if(conncc.GetRowCount()!=0)
				{
					conncc.QueryString="select isnull(max(SEQ_NO), 0)+1 SEQ_NO from PENDING_SALESCOM_CALCULATION_FORMULA";
					conncc.ExecuteQuery();
					SEQ_NO=conncc.GetFieldValue("SEQ_NO");
					TXT_NUM.Text = SEQ_NO;
				}
				
				conncc.QueryString="PARAM_SALESCOM_CALCULATION_FORMULA_MAKER '1','"+
					TXT_NUM.Text +"','"+ DDL_CALC_TYPE.SelectedValue +"','','','','','"+
					DDL_AGENT_TYPE.SelectedValue +"','"+ TXT_CALC_NAME.Text +"','"+ TXT_CALC_FORMULA.Text +"','"+
					TXT_CALC_TABLE.Text +"','"+ TXT_CALC_LINK.Text +"','"+ RBL_ACTIVE.SelectedValue.ToString() +"','"+
					RBL_RESULT_TYPE.SelectedValue.ToString() +"','"+ TXT_CALC_GROUP.Text +"','"+ seq_id +"'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			if (LBL_JENIS.Text=="edit") //edit
			{
				if (LBL_SEQ_ID.Text!="")//Edit dari DGR_REQUEST
				{
					//SALESMANDIRI
					conncc.QueryString="PARAM_SALESCOM_CALCULATION_FORMULA_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						TXT_NUM.Text +"','"+ DDL_CALC_TYPE.SelectedValue +"','','','','','"+
						DDL_AGENT_TYPE.SelectedValue +"','"+ TXT_CALC_NAME.Text +"','"+ TXT_CALC_FORMULA.Text +"','"+
						TXT_CALC_TABLE.Text +"','"+ TXT_CALC_LINK.Text +"','"+ RBL_ACTIVE.SelectedValue.ToString() +"','"+
						RBL_RESULT_TYPE.SelectedValue.ToString() +"','"+ TXT_CALC_GROUP.Text +"','"+ LBL_SEQ_ID.Text +"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
				else if(LBL_SEQ_ID.Text=="")//edit dari Existing
				{
					int seq_id=0;
					conncc.QueryString="select * from PENDING_SALESCOM_CALCULATION_FORMULA where SEQ_NO='"+LBL_SEQ_NO.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//get seq_id
						conncc.QueryString="select isnull(max(seq_id),0)+1 no from PENDING_SALESCOM_CALCULATION_FORMULA";
						conncc.ExecuteQuery();
						seq_id = int.Parse(conncc.GetFieldValue("no"));
					}
					else
					{
						seq_id = int.Parse(conncc.GetFieldValue("SEQ_ID"));
					}
					
					conncc.QueryString="PARAM_SALESCOM_CALCULATION_FORMULA_MAKER '0','"+
						TXT_NUM.Text +"','"+ DDL_CALC_TYPE.SelectedValue +"','','','','','"+
						DDL_AGENT_TYPE.SelectedValue +"','"+ TXT_CALC_NAME.Text +"','"+ TXT_CALC_FORMULA.Text +"','"+
						TXT_CALC_TABLE.Text +"','"+ TXT_CALC_LINK.Text +"','"+ RBL_ACTIVE.SelectedValue.ToString() +"','"+
						RBL_RESULT_TYPE.SelectedValue.ToString() +"','"+ TXT_CALC_GROUP.Text +"','"+ seq_id +"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
			}
			LBL_SAVE_MODE.Text="1";
		}

		protected void Button9_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>window.open('SelectField.aspx?ModuleID="+Request.QueryString["ModuleID"]+"','CheckingResult','width=750,height=150');</script>");
			//Response.Write("<script language='javascript'>window.open('SelectField.aspx?','CheckingResult','status=no,scrollbars=no,width=750,height=150');</script>");
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../SalesParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void BTN_TEST_Click(object sender, System.EventArgs e)
		{
			Session["Formula"] = TXT_CALC_FORMULA.Text;
			Session["Table"] = TXT_CALC_TABLE.Text;
			Session["Link"] = TXT_CALC_LINK.Text;
			Session["Group"] = TXT_CALC_GROUP.Text;
			Response.Write("<script language='javascript'>window.open('CekFormula.aspx?cal_type="+DDL_CALC_TYPE.SelectedValue+"&AgenType="+DDL_AGENT_TYPE.SelectedValue+"&Seq_no="+LBL_SEQ_NO.Text+"&ModuleID="+Request.QueryString["ModuleID"]+"','CheckingResult','width=900,height=350');</script>");
		}
		
		protected void BTN_KALI2_Click(object sender, System.EventArgs e)
		{
			this.TXT_CALC_FORMULA.Text = this.TXT_CALC_FORMULA.Text + "*";
		}

		protected void BTN_BAGI2_Click(object sender, System.EventArgs e)
		{
			this.TXT_CALC_FORMULA.Text = this.TXT_CALC_FORMULA.Text + "/";
		}

		protected void BTN_TAMBAH2_Click(object sender, System.EventArgs e)
		{
			this.TXT_CALC_FORMULA.Text = this.TXT_CALC_FORMULA.Text + "+";
		}

		protected void BTN_KURANG2_Click(object sender, System.EventArgs e)
		{
			this.TXT_CALC_FORMULA.Text = this.TXT_CALC_FORMULA.Text + "-";
		}

		protected void BTN_KURBUKA_Click(object sender, System.EventArgs e)
		{
			this.TXT_CALC_FORMULA.Text = this.TXT_CALC_FORMULA.Text + "(";
		}

		protected void BTN_KURTUTUP_Click(object sender, System.EventArgs e)
		{
			this.TXT_CALC_FORMULA.Text = this.TXT_CALC_FORMULA.Text + ")";
		}

		protected void BTN_KURANG1_Click(object sender, System.EventArgs e)
		{
			if(this.TXT_NUM.Enabled==true)
			{
				if(this.TXT_NUM.Text!="")// && int.Parse(this.TXT_NUM.Text) > 0)
				{
					int kurangNo = int.Parse(this.TXT_NUM.Text)-1;
					this.TXT_NUM.Text = kurangNo.ToString();
				}
			}
		}

		protected void BTN_TAMBAH1_Click(object sender, System.EventArgs e)
		{
			if(this.TXT_NUM.Enabled==true)
			{
				if(this.TXT_NUM.Text!="")
				{
					int tambahNo = int.Parse(this.TXT_NUM.Text)+1;
					this.TXT_NUM.Text = tambahNo.ToString();
				}
			}
		}

	}
}
