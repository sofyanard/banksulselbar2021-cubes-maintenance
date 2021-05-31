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
	/// Summary description for EvaluationDetail.
	/// </summary>
	public partial class EvaluationDetail : System.Web.UI.Page
	{
		//protected Connection conncc = new Connection("Data Source=10.123.12.30;Initial Catalog=SALESMANDIRI;uid=sa;pwd=dmscorp");
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		protected string dbConsumer = ConfigurationSettings.AppSettings["dbConsumer"];
		//protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conn;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				DDL_EVAL.Items.Add(new ListItem("-- Select --",""));
				conncc.QueryString="select EVALUATION_ID, EVALUATION_DESC from RFEVALUATION";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					DDL_EVAL.Items.Add(new ListItem(conncc.GetFieldValue(i,1),conncc.GetFieldValue(i,0)));
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

		private void ViewExistingData()
		{
			//SALESMANDIRI
			conncc.QueryString="select * from VW_PARAM_SALESCOM_EVALUATION_TYPE";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			this.DGR_EXISTING.DataSource = dt;
			this.DGR_EXISTING.DataBind();
			for (int i = 0; i < this.DGR_EXISTING.Items.Count; i++)
			{
				this.DGR_EXISTING.Items[i].Cells[1].Text = (i+1).ToString();
			}
		}

		private void ViewPendingData()
		{
			//SMEDEV2
			conncc.QueryString="select * from VW_PARAM_SALESCOM_PENDING_EVALUATION_TYPE";
			conncc.ExecuteQuery();
			DataTable dt = new DataTable();
			dt = conncc.GetDataTable().Copy();
			DGR_REQUEST.DataSource = dt;
			this.DGR_REQUEST.DataBind();
			for (int i = 0; i < this.DGR_REQUEST.Items.Count; i++)
				this.DGR_REQUEST.Items[i].Cells[1].Text = (i+1).ToString();
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

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVE_MODE.Text="0";
					DDL_EVAL.SelectedValue = e.Item.Cells[7].Text;
					TXT_EVAL_DESC.Text = e.Item.Cells[3].Text;
					TXT_EVAL_MAX.Text = e.Item.Cells[4].Text;
					LBL_JENIS.Text = "edit";
					LBL_TYPEID.Text=e.Item.Cells[6].Text;
					LBL_SEQ_ID.Text="";
					break;
				case "delete":
					//LBL_SAVE_MODE.Text="2";
					//SMEDEV2
					//get seq_id
					conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from pending_salescom_evaluation_type";
					conncc.ExecuteQuery();
					string seq_id = conncc.GetFieldValue("SEQ_ID");
					
					//cek tabel pending
					conncc.QueryString="select * from pending_salescom_evaluation_type where EVALUATION_TYPEID='"+e.Item.Cells[6].Text.Trim()+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()!= 0)
					{
						conncc.QueryString="update pending_salescom_evaluation_type set STATUS='2' where EVALUATION_TYPEID='"+e.Item.Cells[6].Text.Trim()+"'";
						conncc.ExecuteQuery();
					}
					else
					{
						conncc.QueryString="PARAM_SALESCOM_EVALUATIONTYPE_MAKER '2','"+
							e.Item.Cells[6].Text +"','','"+e.Item.Cells[7].Text +"','"+ 
							e.Item.Cells[3].Text +"','"+
							e.Item.Cells[4].Text+"','1','"+seq_id+"'";
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
					string status = e.Item.Cells[10].Text;
					if(status=="2")
					{
						break;
					}
					DDL_EVAL.SelectedValue = e.Item.Cells[8].Text;
					TXT_EVAL_DESC.Text = e.Item.Cells[3].Text;
					TXT_EVAL_MAX.Text = e.Item.Cells[4].Text;
					LBL_JENIS.Text = "edit";
					LBL_TYPEID.Text=e.Item.Cells[7].Text;
					LBL_SEQ_ID.Text=e.Item.Cells[9].Text;
					LBL_SAVE_MODE.Text=e.Item.Cells[10].Text;
					break;
				case "delete":
					//SMEDEV2
					string typeid=e.Item.Cells[7].Text;
					string seq_id = e.Item.Cells[9].Text;
					conncc.QueryString="Delete from PENDING_SALESCOM_EVALUATION_TYPE "+
						"where Evaluation_Typeid='"+typeid+"' and SEQ_ID='"+seq_id+"'"; 
					try
					{
						conncc.ExecuteQuery();
					}
					catch { GlobalTools.popMessage (this,"Error...");}
					ViewPendingData();
					break;
			}
		}

		private void BlankEntry()
		{
			DDL_EVAL.SelectedValue="";
			TXT_EVAL_DESC.Text="";
			TXT_EVAL_MAX.Text="";
			LBL_JENIS.Text="";
			LBL_TYPEID.Text="";
			LBL_SEQ_ID.Text="";
			LBL_SAVE_MODE.Text="1";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			BlankEntry();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_JENIS.Text=="" && LBL_TYPEID.Text=="") //input baru
			{
				//SalesMandiri---input typeId
				conncc.QueryString="select isnull(max(cast(Evaluation_TypeId as integer)),0)+1 TypeID from Evaluation_type";
				conncc.ExecuteQuery();
				string TypeID=conncc.GetFieldValue("TypeID");
				LBL_TYPEID.Text = TypeID;
				//SMEDEV2--- Membandingkan Award_ID baru dengan yang ada di Tabel Pending
				conncc.QueryString="Select Evaluation_TypeId from PENDING_SALESCOM_EVALUATION_TYPE";
				conncc.ExecuteQuery();
				for (int i=0; i<conncc.GetRowCount(); i++)
				{
					if(LBL_TYPEID.Text.ToString().Trim() == conncc.GetFieldValue(i,0).ToString().Trim())
					{
						int ID = int.Parse(LBL_TYPEID.Text.ToString().Trim())+1;
						LBL_TYPEID.Text=ID.ToString();
					}
				}

				//get seq_id
				conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from pending_salescom_evaluation_type";
				conncc.ExecuteQuery();
				LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");

				conncc.QueryString="PARAM_SALESCOM_EVALUATIONTYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
					LBL_TYPEID.Text +"','','"+DDL_EVAL.SelectedValue+"','"+ 
					TXT_EVAL_DESC.Text +"','"+TXT_EVAL_MAX.Text+"','1','"+LBL_SEQ_ID.Text+"'";
				conncc.ExecuteQuery();
				BlankEntry();
				ViewPendingData();
			}
			if (LBL_JENIS.Text=="edit" && LBL_TYPEID.Text!="") //edit
			{
				if(LBL_SEQ_ID.Text!="")//edit dari Request
				{
					conncc.QueryString="PARAM_SALESCOM_EVALUATIONTYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						LBL_TYPEID.Text +"','','"+DDL_EVAL.SelectedValue+"','"+ 
						TXT_EVAL_DESC.Text +"','"+TXT_EVAL_MAX.Text+"','1','"+LBL_SEQ_ID.Text+"'";
					conncc.ExecuteQuery();
					BlankEntry();
					ViewPendingData();
				}
				else if(LBL_SEQ_ID.Text=="")//edit dari Existing
				{
					//cek tabel pending
					conncc.QueryString="select * from pending_salescom_evaluation_type where EVALUATION_TYPEID='"+LBL_TYPEID.Text+"'";
					conncc.ExecuteQuery();
					if(conncc.GetRowCount()==0)//ga ada di tabel pending
					{
						//get seq_id
						conncc.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQ_ID from pending_salescom_evaluation_type";
						conncc.ExecuteQuery();
						LBL_SEQ_ID.Text = conncc.GetFieldValue("SEQ_ID");
					}
					else
					{
						LBL_SEQ_ID.Text=conncc.GetFieldValue("SEQ_ID");
					}
					conncc.QueryString="PARAM_SALESCOM_EVALUATIONTYPE_MAKER '"+LBL_SAVE_MODE.Text+"','"+
						LBL_TYPEID.Text +"','','"+DDL_EVAL.SelectedValue+"','"+ 
						TXT_EVAL_DESC.Text +"','"+TXT_EVAL_MAX.Text+"','1','"+LBL_SEQ_ID.Text+"'";
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

