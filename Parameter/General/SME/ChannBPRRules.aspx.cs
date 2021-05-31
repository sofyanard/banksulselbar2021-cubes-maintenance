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
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for ChannBPRRules.
	/// </summary>
	public partial class ChannBPRRules : System.Web.UI.Page
	{
	
		//protected Connection conn;
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			if (!IsPostBack)
			{
				//CUREF
				/*
				conn.QueryString = "SELECT CH_BPR_CUREF FROM chann_BPR_RULES GROUP BY CH_BPR_CUREF ORDER BY CH_BPR_CUREF";
				conn.ExecuteQuery();
				DDL_CUREF.Items.Clear();
				DDL_CUREF.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_CUREF.Items.Add(new ListItem(conn.GetFieldValue(i,0), conn.GetFieldValue(i, 0)));
				*/

				//RULES
				conn.QueryString = "SELECT CH_PRM_CODE,CH_PRM_NAME  FROM RFCHANN_PARAM_LIST WHERE CH_PRM_ACTIVE=1";
				conn.ExecuteQuery();
				DDL_RULES.Items.Clear();
				DDL_RULES.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_RULES.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				//Company
				//conn.QueryString = "select distinct CU_COMPNAME from cust_company";
				conn.QueryString = "select cc.cu_compname,c.cu_ref from customer c "+
					"left join CUST_COMPANY cc on c.cu_ref=cc.cu_ref "+
					"where cu_channelcomp = '1'";
				conn.ExecuteQuery();
				DDL_COMP.Items.Clear();
				DDL_COMP.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_COMP.Items.Add(new ListItem(conn.GetFieldValue(i,0), conn.GetFieldValue(i, 1)));

				LBL_SAVEMODE.Text = "1";
				bindData1();
				bindData2();
			}
			
			btn_saveDetail.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			Datagrid1.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change1);
			DataGrid2.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change2);
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
//			conn.QueryString = "SELECT A.CH_PRM_CODE,(select top 1 CU_COMPNAME from cust_company c where c.cu_ref=a.CH_BPR_CUREF)KANTOR, " +
//				"B.CH_PRM_NAME, A.CH_BPR_CUREF " +
//				"FROM chann_BPR_RULES A JOIN RFCHANN_PARAM_LIST B ON A.CH_PRM_CODE = B.CH_PRM_CODE " +
//				"where ACTIVE='1' ORDER BY CH_BPR_CUREF ";
			conn.QueryString = "select r.ch_prm_code, c.cu_compname kantor, l.ch_prm_name, r.ch_bpr_curef " +
				"from chann_bpr_rules r " +
				"left join cust_company c on c.cu_ref = r.ch_bpr_curef " +
				"join rfchann_param_list l on l.ch_prm_code = r.ch_prm_code " +
				"where r.active = '1' ";
			if (DDL_COMP.SelectedValue != "")
				conn.QueryString += "and r.ch_bpr_curef = '" + DDL_COMP.SelectedValue + "' ";
			conn.QueryString += "order by c.cu_compname ";
			conn.ExecuteQuery();
			Datagrid1.DataSource = conn.GetDataTable().Copy();
			try 
			{
				Datagrid1.DataBind();
			}
			catch 
			{
				Datagrid1.CurrentPageIndex = Datagrid1.PageCount - 1;
				Datagrid1.DataBind();
			}
		}

		private void bindData2()
		{
		
			//conn.QueryString = "SELECT A.CH_PRM_CODE,(select top 1 CU_COMPNAME from cust_company c where c.cu_ref=a.CH_BPR_CUREF)KANTOR,B.CH_PRM_NAME,A.CH_BPR_CUREF,A.STATUS  FROM PENDING_CHANN_BPR_RULES A JOIN RFCHANN_PARAM_LIST B ON A.CH_PRM_CODE=B.CH_PRM_CODE ORDER BY CH_BPR_CUREF";
			conn.QueryString = "select r.ch_prm_code, c.cu_compname kantor, l.ch_prm_name, r.ch_bpr_curef " +
				"from pending_chann_bpr_rules r " +
				"left join cust_company c on c.cu_ref = r.ch_bpr_curef " +
				"join rfchann_param_list l on l.ch_prm_code = r.ch_prm_code " +
				"order by c.cu_compname ";
			conn.ExecuteQuery();
			DataGrid2.DataSource = conn.GetDataTable().Copy();
			try 
			{
				DataGrid2.DataBind();
			}
			catch 
			{
				DataGrid2.CurrentPageIndex = DataGrid2.PageCount - 1;
				DataGrid2.DataBind();
			}
			
			for (int i = 0; i < DataGrid2.Items.Count; i++)
			{
				if (DataGrid2.Items[i].Cells[4].Text.Trim() == "0")
				{
					DataGrid2.Items[i].Cells[4].Text = "UPDATE";
				}
				else if (DataGrid2.Items[i].Cells[4].Text.Trim() == "1")
				{
					DataGrid2.Items[i].Cells[4].Text = "INSERT";
				}
				else if (DataGrid2.Items[i].Cells[4].Text.Trim() == "2")
				{
					DataGrid2.Items[i].Cells[4].Text = "DELETE";
				}
			} 
		}

		private void clearEditBoxes()
		{
			//DDL_CUREF.SelectedIndex = 0;
			DDL_RULES.SelectedIndex = 0;
			DDL_COMP.SelectedIndex = 0;
					
			LBL_SAVEMODE.Text = "1";
			activatePostBackControls(true);
		}

		private void clearEditBoxes2()
		{
			txt_value1.Text = "";
			txt_value2.Text = "";
			txt_value3.Text = "";
			txt_score.Text = "";
					
			lbl_savemodeDetail.Text = "1";
			//value code
			conn.QueryString = "select max(CH_VALUE_CODE)+1 NEXTVALUECODE from CHANN_BPR_RULEVALUE "+
				"where CH_BPR_CU_REF='"+LBL_CUREF.Text+"' and CH_PRM_CODE='"+LBL_RULES.Text+"'";
			conn.ExecuteQuery();
			LBL_VALUE_CODE.Text = conn.GetFieldValue("NEXTVALUECODE");
			//activatePostBackControls(true);
		}

		private void activatePostBackControls(bool mode)
		{
			//TXT_BRANCH_CODE.Enabled = mode;
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
			this.Datagrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_ItemCommand);
			this.DataGrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_ItemCommand);
			this.DtGrd_Detail.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DtGrd_Detail_ItemCommand);
			this.DtGrd_Detail.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DtGrd_Detail_PageIndexChanged);
			this.DtGrd_DetailMaker.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DtGrd_DetailMaker_ItemCommand);
			this.DtGrd_DetailMaker.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DtGrd_DetailMaker_PageIndexChanged);

		}
		#endregion

		void Grid_Change1(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid1.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData1();	
		}

		void Grid_Change2(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DataGrid2.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData2();	
		}

		private void Datagrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					//DDL_CUREF.SelectedValue=e.Item.Cells[0].Text.Trim();
					DDL_RULES.SelectedValue = e.Item.Cells[2].Text.Trim();
					DDL_COMP.SelectedValue = e.Item.Cells[1].Text.Trim();
					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					break;
					
				case "delete":
					GlobalTools.popMessage(this,"Data Chann BPR Rules Detail Value akan Terdelete");
					LBL_SAVEMODE.Text = "2";
					conn.QueryString = "INSERT INTO PENDING_CHANN_BPR_RULES VALUES ('" +
						e.Item.Cells[2].Text.Trim()+ "', '" + e.Item.Cells[0].Text.Trim() + "','','2') " ;			
					conn.ExecuteQuery();
					bindData2();
					break;

				case "detail":
					LBL_CUREFGRD.Text = "";
					LBL_RULESGRD.Text = "";
					LBL_VALUE_CODE.Text = "";
					LBL_CUREF.Text = e.Item.Cells[0].Text.Trim();
					LBL_RULES.Text = e.Item.Cells[2].Text.Trim();
					viewDetailExisting(LBL_CUREF.Text,LBL_RULES.Text);
					viewDetailPending(LBL_CUREF.Text,LBL_RULES.Text);
					break;
				
				default:
					// Do nothing.
					break;
			} 
		}

		private void DataGrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					//DDL_CUREF.SelectedValue=e.Item.Cells[0].Text.Trim();
					DDL_RULES.SelectedValue = e.Item.Cells[2].Text.Trim();
					DDL_COMP.SelectedValue = e.Item.Cells[1].Text.Trim();
					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					break;

				case "delete":
					conn.QueryString = "DELETE FROM PENDING_CHANN_BPR_RULES  WHERE CH_PRM_CODE='"+e.Item.Cells[2].Text.Trim()+ "' AND CH_BPR_CUREF='"+e.Item.Cells[0].Text.Trim() + "' " ;
					conn.ExecuteQuery();
					bindData2();
					break;

				default:
					// Do nothing.
					break;
			}  
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select * from chann_BPR_RULES where CH_BPR_CUREF='" +DDL_COMP.SelectedValue+
					"' AND CH_PRM_CODE='" +DDL_RULES.SelectedValue+ "' AND ACTIVE = 1";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					Tools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		 

			conn.QueryString = "INSERT INTO PENDING_CHANN_BPR_RULES VALUES( '" +
				DDL_RULES.SelectedValue+ "', '" + DDL_COMP.SelectedValue + "','','"+LBL_SAVEMODE.Text.Trim()+"') " ;			
			conn.ExecuteQuery();
			bindData2();
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]);
		}

		private void viewDetailExisting(string curef, string cdrules)
		{
			conn.QueryString = "select CH_PRM_ISPARAMETER from RFCHANN_PARAM_LIST " +
				"where CH_PRM_CODE='"+cdrules+"' ";
			conn.ExecuteQuery();
			if(conn.GetFieldValue(0, "CH_PRM_ISPARAMETER") == "0")
			{
				txt_value1.CssClass = "mandatory";
				txt_value2.CssClass = "mandatory";
				txt_value3.CssClass = "";
			}
			else
			{
				txt_value1.CssClass = "";
				txt_value2.CssClass = "";
				txt_value3.CssClass = "mandatory";
			}
			conn.QueryString = "select A.CH_PRM_CODE,A.CH_BPR_CU_REF,CH_VALUE_CODE,CH_VALUE1,CH_VALUE2,CH_VALUE3,CH_SCORE "+
				",(select top 1 CU_COMPNAME from cust_company c "+
				"where c.cu_ref=a.CH_BPR_CU_REF)KANTOR,B.CH_PRM_NAME, CH_PRM_ISPARAMETER "+
				"from CHANN_BPR_RULEVALUE A "+
				"JOIN RFCHANN_PARAM_LIST B ON A.CH_PRM_CODE=B.CH_PRM_CODE "+
				"where A.CH_BPR_CU_REF='"+curef+"' and A.CH_PRM_CODE='"+cdrules+"' and active='1'";
			conn.ExecuteQuery();
			DtGrd_Detail.DataSource = conn.GetDataTable().Copy();
			try 
			{
				DtGrd_Detail.DataBind();
			}
			catch 
			{
				DtGrd_Detail.CurrentPageIndex = DtGrd_Detail.PageCount - 1;
				DtGrd_Detail.DataBind();
			}

			//get new value code
			conn.QueryString = "select max(CH_VALUE_CODE)+1 NEXTVALUECODE from CHANN_BPR_RULEVALUE "+
				"where CH_BPR_CU_REF='"+curef+"' and CH_PRM_CODE='"+cdrules+"'";
			conn.ExecuteQuery();
			LBL_VALUE_CODE.Text = conn.GetFieldValue("NEXTVALUECODE");
		}

		private void viewDetailPending(string curef, string cdrules)
		{
			conn.QueryString = "select A.CH_PRM_CODE,A.CH_BPR_CU_REF,CH_VALUE_CODE,CH_VALUE1,CH_VALUE2,CH_VALUE3,CH_SCORE "+
				",(select top 1 CU_COMPNAME from cust_company c "+
				"where c.cu_ref=a.CH_BPR_CU_REF)KANTOR,B.CH_PRM_NAME,PENDINGSTATUS "+
				"from PENDING_CHANN_BPR_RULEVALUE A "+
				"JOIN RFCHANN_PARAM_LIST B ON A.CH_PRM_CODE=B.CH_PRM_CODE "+
				"where A.CH_BPR_CU_REF='"+curef+"' and A.CH_PRM_CODE='"+cdrules+"'";
			conn.ExecuteQuery();
			DtGrd_DetailMaker.DataSource = conn.GetDataTable().Copy();
			try 
			{
				DtGrd_DetailMaker.DataBind();
				for (int i = 0; i < DtGrd_DetailMaker.Items.Count; i++)
				{
					if (DtGrd_DetailMaker.Items[i].Cells[11].Text.Trim() == "0")
					{
						DtGrd_DetailMaker.Items[i].Cells[6].Text = "UPDATE";
					}
					else if (DtGrd_DetailMaker.Items[i].Cells[11].Text.Trim() == "1")
					{
						DtGrd_DetailMaker.Items[i].Cells[6].Text = "INSERT";
					}
					else if (DtGrd_DetailMaker.Items[i].Cells[11].Text.Trim() == "2")
					{
						DtGrd_DetailMaker.Items[i].Cells[6].Text = "DELETE";
					}
				} 
			}
			catch 
			{
				DtGrd_DetailMaker.CurrentPageIndex = DtGrd_Detail.PageCount - 1;
				DtGrd_DetailMaker.DataBind();
			}
			
		}

		private void DtGrd_Detail_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes2();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					 
					txt_value1.Text = e.Item.Cells[2].Text.Trim();
					txt_value2.Text = e.Item.Cells[3].Text.Trim();
					txt_value3.Text = e.Item.Cells[4].Text.Trim();
					txt_score.Text	= e.Item.Cells[5].Text.Trim();
					cleansTextBox(txt_value1);
					cleansTextBox(txt_value2);
					cleansTextBox(txt_value3);
					cleansTextBox(txt_score);
					LBL_CUREFGRD.Text = e.Item.Cells[7].Text.Trim();
					LBL_RULESGRD.Text = e.Item.Cells[8].Text.Trim();
					LBL_VALUE_CODE.Text = e.Item.Cells[9].Text.Trim();

					lbl_savemodeDetail.Text = "0";
					//activatePostBackControls(false);
					break;
					
				case "delete":
					lbl_savemodeDetail.Text = "2";

					//cek tabel pending
					conn.QueryString="select * from PENDING_CHANN_BPR_RULEVALUE "+
						"where CH_BPR_CU_REF='"+e.Item.Cells[7].Text.Trim()+"' "+
						"and CH_PRM_CODE='"+e.Item.Cells[8].Text.Trim()+"' "+
						"and CH_VALUE_CODE='"+e.Item.Cells[9].Text.Trim()+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount()!= 0)
					{
						try
						{
							conn.QueryString ="update PENDING_CHANN_BPR_RULEVALUE set PENDINGSTATUS ='2'"+
								"where CH_BPR_CU_REF='"+e.Item.Cells[7].Text.Trim()+"' "+
								"and CH_PRM_CODE='"+e.Item.Cells[8].Text.Trim()+"' "+
								"and CH_VALUE_CODE='"+e.Item.Cells[9].Text.Trim()+"'";
							conn.ExecuteQuery();
						}
						catch{}
						viewDetailPending(e.Item.Cells[7].Text.Trim(),e.Item.Cells[8].Text.Trim());
					}
					else
					{
						try
						{
							conn.QueryString = "INSERT INTO PENDING_CHANN_BPR_RULEVALUE " +
								"(CH_PRM_CODE, CH_BPR_CU_REF, CH_VALUE_CODE, PENDINGSTATUS) " + 
								"VALUES ('" +
								e.Item.Cells[8].Text.Trim()+ "', '" + e.Item.Cells[7].Text.Trim() + "','"+
								e.Item.Cells[9].Text.Trim()+ "', '2') " ;
							conn.ExecuteQuery();
						}
						catch{}
						viewDetailPending(e.Item.Cells[7].Text.Trim(),e.Item.Cells[8].Text.Trim());
					}
					break;
				
				default:
					// Do nothing.
					break;
			} 
		}

		private void DtGrd_DetailMaker_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes2();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					if (e.Item.Cells[11].Text.Trim()=="2")
					{
						break;
					}
					else
					{
						txt_value1.Text = e.Item.Cells[2].Text.Trim();
						txt_value2.Text = e.Item.Cells[3].Text.Trim();
						txt_value3.Text = e.Item.Cells[4].Text.Trim();
						txt_score.Text	= e.Item.Cells[5].Text.Trim();
						cleansTextBox(txt_value1);
						cleansTextBox(txt_value2);
						cleansTextBox(txt_value3);
						cleansTextBox(txt_score);
						LBL_CUREFGRD.Text = e.Item.Cells[8].Text.Trim();
						LBL_RULESGRD.Text = e.Item.Cells[9].Text.Trim();
						LBL_VALUE_CODE.Text = e.Item.Cells[10].Text.Trim();

						lbl_savemodeDetail.Text = "0";
					}
					//activatePostBackControls(false);
					break;
					
				case "delete":
					conn.QueryString ="delete PENDING_CHANN_BPR_RULEVALUE "+
						"where CH_BPR_CU_REF='"+e.Item.Cells[8].Text.Trim()+"' "+
						"and CH_PRM_CODE='"+e.Item.Cells[9].Text.Trim()+"' "+
						"and CH_VALUE_CODE='"+e.Item.Cells[10].Text.Trim()+"'";
					conn.ExecuteQuery();

					viewDetailPending(e.Item.Cells[8].Text.Trim(),e.Item.Cells[9].Text.Trim());
					
					break;
				
				default:
					// Do nothing.
					break;
			} 
		}

		protected void btn_cancelDetail_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes2();
			lbl_savemodeDetail.Text = "1";
		}

		protected void btn_saveDetail_Click(object sender, System.EventArgs e)
		{
			string val1, val2, val3;
			
			if (txt_value1.Text == "")
				val1 = "NULL";
			else
				val1 = txt_value1.Text;

			if (txt_value2.Text == "")
				val2 = "NULL";
			else
				val2 = txt_value2.Text;

			if (txt_value3.Text == "")
				val3 = "NULL";
			else
				val3 = "'" + txt_value3.Text + "'";

			int hit = 0;
			conn.QueryString="select * from PENDING_CHANN_BPR_RULEVALUE "+
				"where CH_BPR_CU_REF='"+LBL_CUREF.Text+"' "+
				"and CH_PRM_CODE='"+LBL_RULES.Text+"' "+
				"and CH_VALUE_CODE='"+LBL_VALUE_CODE.Text+"'";
			conn.ExecuteQuery();
			hit = conn.GetRowCount() ;
			if (hit == 0 && lbl_savemodeDetail.Text == "1")//insert
			{
				try
				{
					conn.QueryString="insert into PENDING_CHANN_BPR_RULEVALUE values('"+
						LBL_RULES.Text+ "', '" + LBL_CUREF.Text + "','"+
						LBL_VALUE_CODE.Text+ "', " + val1 + ", " + val2 + ", " + val3 + ", '"+
						txt_score.Text+ "', '1' )";
					conn.ExecuteQuery();
				}
				catch{}
			}
			else if (hit != 0 && lbl_savemodeDetail.Text == "1")//insert, tp value code udah ada
			{
				int valuetmp ;
				valuetmp = int.Parse(LBL_VALUE_CODE.Text)+1;
				LBL_VALUE_CODE.Text = valuetmp.ToString();
				try
				{
					conn.QueryString="insert into PENDING_CHANN_BPR_RULEVALUE values('"+
						LBL_RULES.Text+ "', '" + LBL_CUREF.Text + "','"+
						LBL_VALUE_CODE.Text+ "',  " + val1 + ", " + val2 + ", " + val3 + ",'"+
						txt_score.Text+ "', '1' )";
					conn.ExecuteQuery();
				}
				catch{}
			}
			else if (hit == 0 && lbl_savemodeDetail.Text == "0")//update,belum ada di tabel pending
			{
				try
				{
					conn.QueryString="insert into PENDING_CHANN_BPR_RULEVALUE values ('"+
						LBL_RULESGRD.Text+ "', '" + LBL_CUREFGRD.Text + "','"+
						LBL_VALUE_CODE.Text+ "',  " + val1 + ", " + val2 + ", " + val3 + ",'"+
						txt_score.Text+ "', '0' )";
					conn.ExecuteQuery();
				}
				catch{}
			}
			else if (hit != 0 && lbl_savemodeDetail.Text == "0")//update,sudah ada di tabel pending
			{
				try
				{
					conn.QueryString="update PENDING_CHANN_BPR_RULEVALUE set "+
						"CH_VALUE1 = " + val1 + ", CH_VALUE2 = " + val2 + ", CH_VALUE3 = " + val3 + ", " +
						"CH_SCORE = '" + txt_score.Text + "', PENDINGSTATUS = '0' " +
						"where CH_PRM_CODE = '"+LBL_RULESGRD.Text+ "' and CH_BPR_CU_REF = '" + LBL_CUREFGRD.Text +
						"' and CH_VALUE_CODE ='"+LBL_VALUE_CODE.Text+ "' ";
					conn.ExecuteQuery();
				}
				catch{}
			}
			lbl_savemodeDetail.Text = "1";
			clearEditBoxes2();
			viewDetailPending(LBL_CUREF.Text,LBL_RULES.Text);
		}

		private void DtGrd_Detail_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DtGrd_Detail.CurrentPageIndex = e.NewPageIndex;
			viewDetailExisting(LBL_CUREF.Text,LBL_RULES.Text);
		}

		private void DtGrd_DetailMaker_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DtGrd_DetailMaker.CurrentPageIndex = e.NewPageIndex;
			viewDetailPending(LBL_CUREF.Text,LBL_RULES.Text);
		}

		protected void DDL_COMP_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Datagrid1.CurrentPageIndex = 0;
			bindData1();	
		}

	}
}
