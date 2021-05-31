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
	/// Summary description for BlacklistCustParam.
	/// </summary>
	public partial class BlacklistCustParam : System.Web.UI.Page
	{
		protected string mid, addr, cname1, cname2, cname3;
		protected string idnum, dob, npwp, crf, cif;
		protected Connection conn;
		protected Connection conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);

			if(!IsPostBack)
			{
				ViewData(); 
			}
			else
			{
				InitialCon();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");	
			
		}

		private void ViewData()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from rfmodule where moduleid = '"+mid+"'";
			conn2.ExecuteQuery();
			
			LBL_DB_IP.Text = conn2.GetFieldValue("db_ip");
			LBL_DB_NAME.Text = conn2.GetFieldValue("db_nama");
			LBL_LOG_ID.Text = conn2.GetFieldValue("db_loginid");
			LBL_LOG_PWD.Text = conn2.GetFieldValue("db_loginpwd");

			LBL_SAVEMODE.Text = "1";
			TXT_ID.Text = "";
			LBL_PS_FLAG.Text = "0"; 
			LBL_PS_SEQ.Text = "0";

			InitialCon();

			GlobalTools.initDateForm(TXT_DAY,DDL_MONTH,TXT_YEAR); 
			LBL_ID.Text = "CIF#"; 
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private string seq()
		{
			string seqid = "";
			int number = Int16.Parse(LBL_NB.Text);

			conn.QueryString = "select isnull(max(SEQ_ID),0)+ "+LBL_NB.Text+" as MAXSEQ from TPERSONAL";
			conn.ExecuteQuery();
 
			if(conn.GetRowCount() != 0) 
				seqid = conn.GetFieldValue("MAXSEQ");
			else
				seqid = "0"; 

			conn.ClearData();

			number++;

			LBL_NB.Text = number.ToString();  

			return seqid;
		}

		private string seqforps()
		{
			string psseq = "";
			int nr = Int16.Parse(LBL_NB2.Text);

			conn.QueryString = "select isnull(max(PS_SEQ),0)+ "+LBL_NB2.Text+" as MAX from PERSONAL where PS_FLAG = 2";
			conn.ExecuteQuery();
 
			if(conn.GetRowCount() != 0) 
				psseq = conn.GetFieldValue("MAX");
			else
				psseq = "0"; 

			conn.ClearData();

			nr++;

			LBL_NB2.Text = nr.ToString();  

			return psseq;
		}

		private string createseq(string nb)
		{
			string temp = "";

			if(nb.Length == 1)
				temp = "000" + nb;
			else if(nb.Length == 2)
				temp = "00" + nb;
			else if(nb.Length == 3)
				temp = "0" + nb;
			else temp = nb;

			return temp;
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void BindData1(string mode)
		{
			if(mode == "1")
			{
				conn.QueryString = "select PS_SEQ, PS_CIF, CU_REF, PS_FLAG, isnull(PS_NMFIRST,'')+' '+isnull(PS_NMMID,'')+' '+isnull(PS_NMLAST,'') as NAMA, "+
					"PS_KTPNO, PS_DOB, PS_NPWP, CU_HMADDR from PERSONAL where PS_FLAG = '1' order by PS_SEQ";

				DG1.Columns[3].Visible = true;
				DG1.Columns[2].Visible = false; 
				LBL_ID.Text = "Customer Ref";
 
				conn.ExecuteQuery();
			}
			else if(mode == "2")
			{
				conn.QueryString = "select PS_SEQ, PS_CIF, CU_REF, PS_FLAG, isnull(PS_NMFIRST,'')+' '+isnull(PS_NMMID,'')+' '+isnull(PS_NMLAST,'') as NAMA, "+
					"PS_KTPNO, PS_DOB, PS_NPWP, CU_HMADDR from PERSONAL where PS_FLAG = '2' order by PS_SEQ";

				DG1.Columns[3].Visible = false;
				DG1.Columns[2].Visible = true; 
				LBL_ID.Text = "CIF#";
 
				conn.ExecuteQuery();
			}

			
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG1.DataSource = dt;

			try
			{
				DG1.DataBind();
			}
			catch 
			{
					DG1.CurrentPageIndex = DG1.PageCount - 1;
					DG1.DataBind();
			}
 
			conn.ClearData(); 
 
		}

		private void BindData2(string mode)
		{
			if(mode == "1")
			{
				conn.QueryString = "select PS_SEQ, PS_CIF, CU_REF, PS_FLAG, isnull(PS_NMFIRST,'')+' '+isnull(PS_NMMID,'')+' '+isnull(PS_NMLAST,'') as NAMA, "+
					"PS_KTPNO, PS_DOB, PS_NPWP, CU_HMADDR, CH_STA, "+
					"STATUS = case CH_STA when '1' then 'INSERT' "+
					"when '2' then 'UPDATE' when '3' then 'DELETE' end "+
					"from TPERSONAL where PS_FLAG = '1' order by PS_SEQ";

				conn.ExecuteQuery();

				DG2.Columns[3].Visible = true;
				DG2.Columns[2].Visible = false; 
				LBL_ID.Text = "Customer Ref"; 
			}
			else if(mode == "2")
			{
				conn.QueryString = "select PS_SEQ, PS_CIF, CU_REF, PS_FLAG, isnull(PS_NMFIRST,'')+' '+isnull(PS_NMMID,'')+' '+isnull(PS_NMLAST,'') as NAMA, "+
					"PS_KTPNO, PS_DOB, PS_NPWP, CU_HMADDR, CH_STA, "+
					"STATUS = case CH_STA when '1' then 'INSERT' "+
					"when '2' then 'UPDATE' when '3' then 'DELETE' end "+
					"from TPERSONAL where PS_FLAG = '2' order by PS_SEQ";

				conn.ExecuteQuery();

				DG2.Columns[3].Visible = false;
				DG2.Columns[2].Visible = true; 
				LBL_ID.Text = "CIF#"; 
			}

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG2.DataSource = dt;

			try
			{
				DG2.DataBind();
			}
			catch 
			{
				DG2.CurrentPageIndex = DG2.PageCount - 1;
				DG2.DataBind();
			}
		}

		private void ClearEditBoxes()
		{
			TXT_ADDR.Text = "";
			TXT_ID.Text = "";
			TXT_NUMBER.Text = "";
			TXT_CUNAME1.Text = "";
			TXT_CUNAME2.Text = "";
			TXT_CUNAME3.Text = "";
			TXT_DAY.Text = "";
			TXT_NPWP.Text = "";
			TXT_YEAR.Text = "";
			DDL_MONTH.ClearSelection();

			TXT_ID.Enabled = true;
			TXT_ID.Text = "";
			LBL_PS_FLAG.Text = "0";
			LBL_PS_SEQ.Text = "0"; 
			LBL_SAVEMODE.Text = "1"; 
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
			this.DG1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG1_ItemCommand);
			this.DG1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG1_PageIndexChanged);
			this.DG2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG2_ItemCommand);
			this.DG2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG2_PageIndexChanged);

		}
		#endregion

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40"); 		
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string id = "", pseq = ""; 
			int hit = 0;

			if(RDB_PERSONAL.SelectedValue == "")
			{
				GlobalTools.popMessage(this,"Please select Personal Type first!");
				return;
			}

			if(TXT_DAY.Text != "" || DDL_MONTH.SelectedValue != "" || TXT_YEAR.Text != "")
			{
				if(!GlobalTools.isDateValid(this,TXT_DAY.Text, DDL_MONTH.SelectedValue, TXT_YEAR.Text))
				{
					return;
				}
			}

			conn.QueryString = "SELECT * FROM TPERSONAL WHERE PS_SEQ = '"+LBL_PS_SEQ.Text+"' AND PS_FLAG = '"+LBL_PS_FLAG.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "UPDATE TPERSONAL SET PS_DOB = "+GlobalTools.ToSQLDate(TXT_DAY.Text, DDL_MONTH.SelectedValue, TXT_YEAR.Text)+", "+
					"PS_NMFIRST = "+GlobalTools.ConvertNull(TXT_CUNAME1.Text)+", PS_NMMID = "+GlobalTools.ConvertNull(TXT_CUNAME2.Text)+", "+
					"PS_NMLAST = "+GlobalTools.ConvertNull(TXT_CUNAME3.Text)+", CU_HMADDR = "+GlobalTools.ConvertNull(TXT_ADDR.Text)+", "+
					"PS_KTPNO = "+GlobalTools.ConvertNull(TXT_NUMBER.Text)+", PS_NPWP = "+GlobalTools.ConvertNull(TXT_NPWP.Text)+" "+
					"WHERE PS_SEQ = '"+LBL_PS_SEQ.Text+"' AND PS_FLAG = '"+LBL_PS_FLAG.Text+"'";
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				id = createseq(seq()); 

				if(RDB_PERSONAL.SelectedValue == "1")
				{
					conn.QueryString = "insert into TPERSONAL (SEQ_ID, PS_SEQ, PS_FLAG, PS_DOB, PS_NMFIRST, PS_NMMID, PS_NMLAST, CU_REF, CU_HMADDR, PS_KTPNO, PS_NPWP, PS_CIF, CH_STA) "+
						"values ('"+id+"', '"+LBL_PS_SEQ.Text+"', 1, "+GlobalTools.ToSQLDate(TXT_DAY.Text, DDL_MONTH.SelectedValue, TXT_YEAR.Text)+", "+GlobalTools.ConvertNull(TXT_CUNAME1.Text)+","+
						" "+GlobalTools.ConvertNull(TXT_CUNAME2.Text)+", "+GlobalTools.ConvertNull(TXT_CUNAME3.Text)+","+
						" "+GlobalTools.ConvertNull(TXT_ID.Text)+", "+GlobalTools.ConvertNull(TXT_ADDR.Text)+", "+GlobalTools.ConvertNull(TXT_NUMBER.Text)+", "+GlobalTools.ConvertNull(TXT_NPWP.Text)+", NULL, '2')";

					conn.ExecuteQuery();
				}
				else if(RDB_PERSONAL.SelectedValue == "2")
				{
					conn.QueryString = "insert into TPERSONAL (SEQ_ID, PS_SEQ, PS_FLAG, PS_DOB, PS_NMFIRST, PS_NMMID, PS_NMLAST, CU_REF, CU_HMADDR, PS_KTPNO, PS_NPWP, PS_CIF, CH_STA) "+
						"values ('"+id+"', '"+LBL_PS_SEQ.Text+"', 2, "+GlobalTools.ToSQLDate(TXT_DAY.Text, DDL_MONTH.SelectedValue, TXT_YEAR.Text)+", "+GlobalTools.ConvertNull(TXT_CUNAME1.Text)+","+
						" "+GlobalTools.ConvertNull(TXT_CUNAME2.Text)+", "+GlobalTools.ConvertNull(TXT_CUNAME3.Text)+","+
						" null, "+GlobalTools.ConvertNull(TXT_ADDR.Text)+", "+GlobalTools.ConvertNull(TXT_NUMBER.Text)+", "+GlobalTools.ConvertNull(TXT_NPWP.Text)+", "+GlobalTools.ConvertNull(TXT_ID.Text)+", '2')";

					conn.ExecuteQuery();
				}
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				id = createseq(seq()); 

				if(RDB_PERSONAL.SelectedValue == "1")
				{
					conn.QueryString = "insert into TPERSONAL (SEQ_ID, PS_SEQ, PS_FLAG, PS_DOB, PS_NMFIRST, PS_NMMID, PS_NMLAST, CU_REF, CU_HMADDR, PS_KTPNO, PS_NPWP, PS_CIF, CH_STA) "+
						"values ('"+id+"', '"+TXT_ID.Text+"', 1, "+GlobalTools.ToSQLDate(TXT_DAY.Text, DDL_MONTH.SelectedValue, TXT_YEAR.Text)+", "+GlobalTools.ConvertNull(TXT_CUNAME1.Text)+","+
						" "+GlobalTools.ConvertNull(TXT_CUNAME2.Text)+", "+GlobalTools.ConvertNull(TXT_CUNAME3.Text)+","+
						" "+GlobalTools.ConvertNull(TXT_ID.Text)+", "+GlobalTools.ConvertNull(TXT_ADDR.Text)+", "+GlobalTools.ConvertNull(TXT_NUMBER.Text)+", "+GlobalTools.ConvertNull(TXT_NPWP.Text)+", NULL, '1')";

					conn.ExecuteQuery();
				}
				else if(RDB_PERSONAL.SelectedValue == "2")
				{
					pseq = seqforps();

					conn.QueryString = "insert into TPERSONAL (SEQ_ID, PS_SEQ, PS_FLAG, PS_DOB, PS_NMFIRST, PS_NMMID, PS_NMLAST, CU_REF, CU_HMADDR, PS_KTPNO, PS_NPWP, PS_CIF, CH_STA) "+
						"values ('"+id+"', '"+pseq+"', 2, "+GlobalTools.ToSQLDate(TXT_DAY.Text, DDL_MONTH.SelectedValue, TXT_YEAR.Text)+", "+GlobalTools.ConvertNull(TXT_CUNAME1.Text)+","+
						" "+GlobalTools.ConvertNull(TXT_CUNAME2.Text)+", "+GlobalTools.ConvertNull(TXT_CUNAME3.Text)+","+
						" null, "+GlobalTools.ConvertNull(TXT_ADDR.Text)+", "+GlobalTools.ConvertNull(TXT_NUMBER.Text)+", "+GlobalTools.ConvertNull(TXT_NPWP.Text)+", "+GlobalTools.ConvertNull(TXT_ID.Text)+", '1')";

					conn.ExecuteQuery();
				}

				
 
				ClearEditBoxes(); 
			}
			else if((hit != 0) && (LBL_SAVEMODE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
				return;
			}

			conn.ClearData();
 	
			BindData2(RDB_PERSONAL.SelectedValue);

			LBL_SAVEMODE.Text = "1";
		
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		private void DG1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG1.CurrentPageIndex = e.NewPageIndex;
			BindData1(RDB_PERSONAL.SelectedValue); 
		}

		private void DG1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string sq, flag, id; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					sq = e.Item.Cells[0].Text.Trim();
					flag = e.Item.Cells[1].Text.Trim();
					
					conn.QueryString = "SELECT * FROM TPERSONAL WHERE PS_SEQ = '"+sq+"' AND PS_FLAG = '"+flag+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						id = createseq(seq()); 

						conn.QueryString = "SELECT * FROM PERSONAL WHERE PS_SEQ = '"+sq+"' AND PS_FLAG = '"+flag+"'";
						conn.ExecuteQuery();

						if(conn.GetRowCount() != 0) 
						{
							addr = conn.GetFieldValue(0,"CU_HMADDR");
							dob = conn.GetFieldValue(0,"PS_DOB");  
							cname1 = conn.GetFieldValue(0,"PS_NMFIRST");
							cname2 = conn.GetFieldValue(0,"PS_NMMID");
							cname3 = conn.GetFieldValue(0,"PS_NMLAST");
							idnum = conn.GetFieldValue(0,"PS_KTPNO");
							npwp = conn.GetFieldValue(0,"PS_NPWP");
							crf = conn.GetFieldValue(0,"CU_REF");
							cif = conn.GetFieldValue(0,"PS_CIF");
							
							try
							{
								conn.QueryString = "insert into TPERSONAL (SEQ_ID, PS_SEQ, PS_FLAG, PS_DOB, PS_NMFIRST, PS_NMMID, PS_NMLAST, CU_REF, CU_HMADDR, PS_KTPNO, PS_NPWP, PS_CIF, CH_STA) "+
									"values ('"+id+"', '"+sq+"', "+flag+", "+GlobalTools.ToSQLDate(dob)+", "+GlobalTools.ConvertNull(cname1)+", "+GlobalTools.ConvertNull(cname2)+", "+GlobalTools.ConvertNull(cname3)+","+
									" "+GlobalTools.ConvertNull(crf)+", "+GlobalTools.ConvertNull(addr)+", "+GlobalTools.ConvertNull(idnum)+", "+GlobalTools.ConvertNull(npwp)+", "+GlobalTools.ConvertNull(cif)+", '3')";

								conn.ExecuteQuery();
							}
							catch{ }

						}

						BindData2(RDB_PERSONAL.SelectedValue);
					}
					break;
				case "edit":
					sq = e.Item.Cells[0].Text.Trim();
					flag = e.Item.Cells[1].Text.Trim();
					LBL_PS_FLAG.Text = flag; 
					LBL_PS_SEQ.Text = sq; 

					conn.QueryString = "SELECT * FROM PERSONAL WHERE PS_SEQ = '"+sq+"' AND PS_FLAG = '"+flag+"'";
					conn.ExecuteQuery(); 
					
					if(conn.GetRowCount() != 0) 
					{
						TXT_ADDR.Text = conn.GetFieldValue(0,"CU_HMADDR");
							
						TXT_DAY.Text = GlobalTools.FormatDate_Day(conn.GetFieldValue(0,"PS_DOB"));
						DDL_MONTH.SelectedValue = GlobalTools.FormatDate_Month(conn.GetFieldValue(0,"PS_DOB"));
						TXT_YEAR.Text = GlobalTools.FormatDate_Year(conn.GetFieldValue(0,"PS_DOB"));

						TXT_CUNAME1.Text = conn.GetFieldValue(0,"PS_NMFIRST");
						TXT_CUNAME2.Text = conn.GetFieldValue(0,"PS_NMMID");
						TXT_CUNAME3.Text = conn.GetFieldValue(0,"PS_NMLAST");
					}

					TXT_NPWP.Text = cleansText(e.Item.Cells[8].Text);
					TXT_NUMBER.Text = cleansText(e.Item.Cells[7].Text);

					if(RDB_PERSONAL.SelectedValue == "2")
						TXT_ID.Text = e.Item.Cells[2].Text.Trim();
					else if(RDB_PERSONAL.SelectedValue == "1")
						TXT_ID.Text = e.Item.Cells[3].Text.Trim();

					TXT_ID.Enabled = false;

					LBL_SAVEMODE.Text = "2";		
					break;
			}		
		
		}

		private void DG2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string seq, fg; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					seq = e.Item.Cells[0].Text.Trim();
					fg = e.Item.Cells[1].Text.Trim();

					conn.QueryString = "DELETE FROM TPERSONAL WHERE PS_SEQ = '"+seq+"' AND PS_FLAG = '"+fg+"'";
					conn.ExecuteQuery();
					
					BindData2(RDB_PERSONAL.SelectedValue);
					break;
				case "edit":
					seq = e.Item.Cells[0].Text.Trim();
					fg = e.Item.Cells[1].Text.Trim();
					LBL_PS_FLAG.Text = fg;
					LBL_PS_SEQ.Text = seq;

					if(e.Item.Cells[10].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						conn.QueryString = "SELECT * FROM TPERSONAL WHERE PS_SEQ = '"+seq+"' AND PS_FLAG = '"+fg+"'";
						conn.ExecuteQuery(); 
					
						if(conn.GetRowCount() != 0) 
						{
							TXT_ADDR.Text = conn.GetFieldValue(0,"CU_HMADDR");
							
							TXT_DAY.Text = GlobalTools.FormatDate_Day(conn.GetFieldValue(0,"PS_DOB"));
							DDL_MONTH.SelectedValue = GlobalTools.FormatDate_Month(conn.GetFieldValue(0,"PS_DOB"));
							TXT_YEAR.Text = GlobalTools.FormatDate_Year(conn.GetFieldValue(0,"PS_DOB"));

							TXT_CUNAME1.Text = conn.GetFieldValue(0,"PS_NMFIRST");
							TXT_CUNAME2.Text = conn.GetFieldValue(0,"PS_NMMID");
							TXT_CUNAME3.Text = conn.GetFieldValue(0,"PS_NMLAST");
						}

						TXT_NPWP.Text = cleansText(e.Item.Cells[8].Text);
						TXT_NUMBER.Text = cleansText(e.Item.Cells[7].Text);
						
						if(RDB_PERSONAL.SelectedValue == "2")
							TXT_ID.Text = e.Item.Cells[2].Text.Trim();
						else if(RDB_PERSONAL.SelectedValue == "1")
							TXT_ID.Text = e.Item.Cells[3].Text.Trim();
						
						TXT_ID.Enabled = false;
   
						LBL_SAVEMODE.Text = "2";		
					}
					break;
			}				
		}

		private void DG2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG2.CurrentPageIndex = e.NewPageIndex;
			BindData2(RDB_PERSONAL.SelectedValue); 		
		}

		protected void RDB_PERSONAL_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindData1(RDB_PERSONAL.SelectedValue);   
			BindData2(RDB_PERSONAL.SelectedValue);   
		}
	}
}
