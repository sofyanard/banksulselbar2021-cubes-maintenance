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

namespace CuBES_Maintenance.Parameter.Scoring.Consumer
{
	/// <summary>
	/// Summary description for CUBESCalculation.
	/// </summary>
	public partial class CUBESCalculation : System.Web.UI.Page
	{
		protected Connection conn2;
		protected Connection conn;
		protected string Id,Request_Id;
		string PARAM_ID,REQUEST_ID;
		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon(); 
			
			if(!IsPostBack)
			{
				PARAM_ID=""; 
				LBL_SAVEMODE.Text = "1";
				LBL_SAVEMODE2.Text = "1";
				LBL_REQUEST.Text = "";
				TXT_ID1.Text = "";
				TXT_ID2.Text = "";
				LBL_SEQ.Text = "";
				Label1.Text = "0";
				Label5.Text = "0";
				BTN_SAVE_VALUE.Enabled = false;
				TR_DDL_VALUE.Visible = false; 
			
				FillProgramType();
				FillProductType();
				FillEmployeeType();
				ViewExistingParameterListData();
				ViewPendingParameterListData();
				ViewExistingParameterValueData();
				ViewPendingParameterValueData();
			}

			refreshRadioButton();
			BTN_SAVE_LIST.Attributes.Add("onclick","if(!cek_mandatory(document.form1)){return false;};");
			BTN_SAVE_VALUE.Attributes.Add("onclick","if(!cek_mandatory2(document.form1)){return false;};");
		}

		private void InitialCon()
		{
			conn2.QueryString = "select * from VW_GETCONN where moduleid = '40'";
			conn2.ExecuteQuery();
			string DB_NAMA			= conn2.GetFieldValue("DB_NAMA");
			string DB_IP			= conn2.GetFieldValue("DB_IP");
			string DB_LOGINID		= conn2.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD		= conn2.GetFieldValue("DB_LOGINPWD");
			conn = new Connection("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void activatePostBackControlsList(bool modeList)
		{
			TXT_ID1.Enabled = modeList;
		}

		private void activatePostBackControlsValue(bool modeValue)
		{
			TXT_ID2.Enabled = modeValue;
		}

		private void refreshRadioButton()
		{
			//*********** TEMPLATE DATAGRID DGR_EXISTING_LIST
			
			for (int i=0; i<DGR_EXISTING_LIST.Items.Count; i++)
			{
				RadioButton rbt = new RadioButton();
				rbt.Checked = false;
				rbt.CssClass = "mandatory";
				rbt.ID = "RBT_" + i.ToString();
				rbt.AutoPostBack = true;
				DGR_EXISTING_LIST.Items[i].Cells[0].Controls.Add(rbt);

				rbt.CheckedChanged += new System.EventHandler(this.EventCbx);
			}		

			//***********************************************
		}

		private void EventCbx(object sender, System.EventArgs e)
		{
			RadioButton rb = (RadioButton) sender, oldchecked;
			
			int oldrow,nowrow;
			string RBT_ID = rb.ID;
			
			try
			{
				oldrow = int.Parse(Label3.Text.Substring(4));
				oldchecked = (RadioButton) DGR_EXISTING_LIST.Items[oldrow].Cells[0].FindControl(Label3.Text);
				Label2.Text = oldchecked.ID;

				if(Label1.Text == "1" && Label5.Text == "1")
					oldchecked.Checked = false;
			}
			catch { }
			
			Label3.Text = RBT_ID;
			nowrow = int.Parse(Label3.Text.Substring(4));
			PARAM_ID = DGR_EXISTING_LIST.Items[nowrow].Cells[1].Text.Trim();
			Label4.Text = PARAM_ID;
			BTN_SAVE_VALUE.Enabled = true;

			Label1.Text = "1";
			Label5.Text = "1";
			ParameterValueDDL();
		}

		public void FillProgramType()
		{
			
			/* ################ source aslinya #####################
			 *	if (session("SG_CODE") = "00") or (session("area_id") = "0000") then
				sqlprog = "select * from PROGRAM "
				else
				sqlprog = "select * from PROGRAM " &_
			 "	where AREA_ID = '"&session("area_id")&"'" 
			####################################################### 
			*/
			this.DDL_PROGRAM_TYPE.Items.Add(new ListItem("- PILIH -", ""));
			/*
			if (session("SG_CODE")="00" || session("area_id") = "0000") -- menunggu perkembangan lebih lanjut
			{
			*/
			conn.QueryString ="SELECT PR_CODE,PR_DESC FROM PROGRAM";
			conn.ExecuteQuery();
			/*
			} 
			else
			{	
				conn.QueryString = "SELECT PR_CODE,PR_DESC FROM PROGRAM" +
					"WHERE AREA_ID = " + session("area_id");
				conn.ExecuteQuery();
			}
			*/
			for(int i = 0; i < conn.GetRowCount(); i++)
			{
				this.DDL_PROGRAM_TYPE.Items.Add (new ListItem(conn.GetFieldValue(i,1), conn.GetFieldValue(i,0)));
			}
		
		}
		public void FillProductType()
		{
			/*########### SOURCE ASLINYA ###############
			 *   sqlprod = "select * from TPRODUCT "&_
				" where GROUP_ID = '1'" 
				set prodres = koneksi.execute(sqlprod)   
			 ##########################################
			 * */
			this.DDL_PRODUCT_TYPE.Items.Clear();
			this.DDL_PRODUCT_TYPE.Items.Add(new ListItem("- PILIH -", ""));
			conn.QueryString = "SELECT TPRODUCT.PRODUCTID, PRODUCTNAME FROM TPRODUCT " +
				"INNER JOIN PROGRAMPRO ON PROGRAMPRO.PRODUCTID = TPRODUCT.PRODUCTID " +
				"WHERE PR_CODE = '" + DDL_PROGRAM_TYPE.SelectedValue + "'";
			conn.ExecuteQuery();
			for (int i=0; i <conn.GetRowCount(); i++)
			{
				this.DDL_PRODUCT_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
			}
		}

		public void FillEmployeeType()
		{
			this.DDL_EMPLOYEE_TYPE.Items.Add(new ListItem("- PILIH -",""));
			conn.QueryString ="SELECT * FROM JOB_TYPE";
			conn.ExecuteQuery();
			for (int i = 0; i < conn.GetRowCount(); i++)
			{
				this.DDL_EMPLOYEE_TYPE.Items.Add (new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
			}
		}

		private string checkApost(string str)
		{
			return str.Replace("'", "''").Trim();
		}

		private void clearEditParamListBoxes()
		{
			this.TXT_ID1.Text = "";
			this.TXT_PARAM_NAME.Text = "";
			this.TXT_PARAM_FORMULA.Text = "";
			this.TXT_PARAM_LINK.Text = "";
			this.TXT_PARAM_FIELD.Text = "";
			this.TXT_PARAM_TABLE.Text = "";
			this.TXT_PARAM_TABLE_CHILD.Text = "";
			
			try 
			{
				this.RBL_PARAM_PRM.SelectedItem.Selected = false;
				this.RBL_PARAM_ACTIVE.SelectedItem.Selected = false;
			}
			catch {}
			
			activatePostBackControlsList(true);
		}
		
		public void ViewExistingParameterListData()
		{ 
			string plus = "";

			if(LBL_PRM.Text != "" || LBL_PRM.Text != null)
			{
				if(LBL_PRM.Text == "0")
					plus = "and PARAM_PRM = '0'";
				else if(LBL_PRM.Text == "1")
					plus = "and PARAM_PRM = '1'";
				else plus = "";
			}

			conn.QueryString = "select PARAM_ID,PARAM_NAME,PARAM_FORMULA,PARAM_FIELD,PARAM_TABLE,PARAM_TABLE_CHILD," +
				"PARAM_LINK,PARAM_PRM,PARAM_ACTIVE from MANDIRI_PARAM_LIST " +
				"where PARAM_OTHER = '0' " +plus;
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_EXISTING_LIST.DataSource = data;
			
			try
			{
				DGR_EXISTING_LIST.DataBind();
			} 
			catch 
			{
				
				this.DGR_EXISTING_LIST.CurrentPageIndex = DGR_EXISTING_LIST.PageCount - 1;
				DGR_EXISTING_LIST.DataBind();
			}

			try
			{
				RBL_PARAM_PRM.SelectedValue = LBL_PRM.Text.Trim(); 
			}
			catch{ }
		}

		public void ViewPendingParameterListData()
		{
			conn.QueryString = "select PARAM_ID,PARAM_NAME,PARAM_FORMULA,PARAM_FIELD,PARAM_TABLE, " +
				"PARAM_TABLE_CHILD,PARAM_LINK,PARAM_PRM,PARAM_ACTIVE,CH_STA, " +
				"case when CH_STA = '1' then 'INSERT' when CH_STA = '0' then 'UPDATE' " +
				"else 'DELETE' end CH_STA1 from TMANDIRI_PARAM_LIST " +
				"where PARAM_OTHER = '0'";
			conn.ExecuteQuery();

			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_REQUEST_LIST.DataSource = data;
			
			try
			{
				DGR_REQUEST_LIST.DataBind();
			} 
			catch 
			{
				this.DGR_REQUEST_LIST.CurrentPageIndex = DGR_REQUEST_LIST.PageCount - 1;
				DGR_REQUEST_LIST.DataBind();
			}
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private string cleansFloat(string tn)
		{
			if (tn.Trim() == "&nbsp;")
				tn = "0";
			return tn;
		}

		private void clearEditParamValueBoxes()
		{
			this.TXT_ID2.Text = "";
			this.TXT_PARAM_NAME2.Text = "";
			this.TXT_MAX_VALUE.Text = "";
			this.TXT_MIN_VALUE.Text = "";
			this.TXT_PARAM_SCORE.Text = "";
			
			try 
			{
				this.DDL_VALUE.SelectedItem.Selected = false;
			}
			catch {}

			activatePostBackControlsValue(true);
		}

		private string checkComma(string str)
		{
			return str.Replace(",", ".").Trim();
		}

		private void GetRequest()
		{
			this.LBL_REQUEST.Text = "";
			
			if ((this.DDL_PRODUCT_TYPE.SelectedValue  != "") && (this.DDL_PROGRAM_TYPE.SelectedValue != "") && (this.DDL_EMPLOYEE_TYPE.SelectedValue != ""))
			{
				conn.QueryString = "select REQUEST_ID, PR_CODE, JOB_TYPE_ID, PRODUCTID from MANDIRI_PARAM_REQUEST "+
					"where PR_CODE = '"+this.DDL_PROGRAM_TYPE.SelectedValue.Trim()+"' "+
					"and  PRODUCTID = '"+this.DDL_PRODUCT_TYPE.SelectedValue.Trim()+"' "+
					"and JOB_TYPE_ID = '"+this.DDL_EMPLOYEE_TYPE.SelectedValue.Trim()+"'";
				conn.ExecuteQuery();

				if(conn.GetRowCount() != 0)
				{
					REQUEST_ID = conn.GetFieldValue("REQUEST_ID");	 
					LBL_REQUEST.Text = REQUEST_ID.Trim();
				}
				else
				{
					REQUEST_ID = DDL_PROGRAM_TYPE.SelectedValue+""+DDL_PRODUCT_TYPE.SelectedValue+""+DDL_EMPLOYEE_TYPE.SelectedValue;
					LBL_REQUEST.Text = REQUEST_ID.Trim();

					try
					{
						conn.QueryString = "INSERT INTO MANDIRI_PARAM_REQUEST (REQUEST_ID, PRODUCTID, JOB_TYPE_ID, PR_CODE) "+
							"VALUES('"+REQUEST_ID.Trim()+"','"+DDL_PRODUCT_TYPE.SelectedValue+"','"+DDL_EMPLOYEE_TYPE.SelectedValue+"','"+DDL_PROGRAM_TYPE.SelectedValue+"')";
						conn.ExecuteQuery();
					}
					catch{ }
				}
			}
		}

		private string ExchgPoint(string str)
		{
			return str.Replace(".",",").Trim();
		}

		private void ViewExistingParameterValueData()
		{
			if ((this.DDL_PRODUCT_TYPE.SelectedValue  != "") && (this.DDL_PROGRAM_TYPE.SelectedValue != "") && ( this.DDL_EMPLOYEE_TYPE.SelectedValue != "") )
			{
				conn.QueryString = "select PARAM_VALUE_ID, PARAM_VALUE_NAME, MIN_VALUE, MAX_VALUE, PARAM_SCORE " +
					"from MANDIRI_PARAM_VALUE " +
					"where PARAM_ID = '" + this.Label4.Text.Trim() + "' and REQUEST_ID = '" + LBL_REQUEST.Text.Trim() + "' ";
				conn.ExecuteQuery();
			
				DataTable data1 = new DataTable();
				data1 = conn.GetDataTable().Copy();
				DGR_EXISTING_VALUE.DataSource = data1;
				
				try
				{
					DGR_EXISTING_VALUE.DataBind();
				} 
				catch 
				{
					this.DGR_EXISTING_VALUE.CurrentPageIndex = DGR_EXISTING_VALUE.PageCount - 1;
					DGR_EXISTING_VALUE.DataBind();
				}

				for (int i=0; i < DGR_EXISTING_VALUE.Items.Count; i++)
				{
					DGR_EXISTING_VALUE.Items[i].Cells[2].Text	= ExchgPoint(DGR_EXISTING_VALUE.Items[i].Cells[2].Text);
					DGR_EXISTING_VALUE.Items[i].Cells[3].Text	= ExchgPoint(DGR_EXISTING_VALUE.Items[i].Cells[3].Text);
				}

				LBL_PARAM_VALUE_ID.Text = conn.GetFieldValue("PARAM_VALUE_ID");
			}
		}
		
		private void ViewPendingParameterValueData()
		{
			conn.QueryString = "select PARAM_VALUE_ID, PARAM_VALUE_NAME, MIN_VALUE, MAX_VALUE, PARAM_SCORE, " +
				"CH_STA, SEQ_ID,case when CH_STA = '1' then 'INSERT' when CH_STA = '0' then 'UPDATE' " +
				"else 'DELETE' end CH_STA1  from TMANDIRI_PARAM_VALUE " +
				"where PARAM_ID = '" + this.Label4.Text.Trim() + "' and REQUEST_ID = '" + LBL_REQUEST.Text.Trim() + "' ";
			conn.ExecuteQuery();
			
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_REQUEST_VALUE.DataSource = data;
			
			try
			{
				DGR_REQUEST_VALUE.DataBind();
			} 
			catch 
			{
				this.DGR_REQUEST_VALUE.CurrentPageIndex = DGR_REQUEST_VALUE.PageCount - 1;
				DGR_REQUEST_VALUE.DataBind();
			}

			
			for (int i=0; i < DGR_REQUEST_VALUE.Items.Count; i++)
			{
				DGR_REQUEST_VALUE.Items[i].Cells[2].Text	= ExchgPoint(DGR_REQUEST_VALUE.Items[i].Cells[2].Text);
				DGR_REQUEST_VALUE.Items[i].Cells[3].Text	= ExchgPoint(DGR_REQUEST_VALUE.Items[i].Cells[3].Text);
			}
		}

		private void ParameterValueDDL()
		{
			conn.QueryString = "select PARAM_TABLE,PARAM_TABLE_CHILD from MANDIRI_PARAM_LIST " +
				"where PARAM_ID = '" + this.Label4.Text.Trim() + "' ";
			conn.ExecuteQuery();

			string TABLE,TABLE_CHILD;
			TABLE = conn.GetFieldValue("PARAM_TABLE");
			TABLE_CHILD = conn.GetFieldValue("PARAM_TABLE_CHILD");
			string Comm;
			
			this.DDL_VALUE.Items.Clear();
			
			if (TABLE.Trim() == TABLE_CHILD.Trim())
			{
				TR_MAX_VALUE.Visible = true;
				TR_MIN_VALUE.Visible = true; 
				
				TR_DDL_VALUE.Visible = false; 	
			} 
			else if(TABLE.Trim() != TABLE_CHILD.Trim())
			{
				TR_MAX_VALUE.Visible = false;
				TR_MIN_VALUE.Visible = false; 
				
				TR_DDL_VALUE.Visible = true; 	

				this.DDL_VALUE.Items.Add(new ListItem("- PILIH -",""));
				
				if(TABLE.Trim() == "RFBUSINESTYPE")
				{
					Comm = "select distinct(BT_GROUP) from " + TABLE;
					conn.QueryString = Comm;
					conn.ExecuteQuery();

					for (int i=0; i< conn.GetRowCount(); i++)
					{
						this.DDL_VALUE.Items.Add(new ListItem(conn.GetFieldValue(i,0),conn.GetFieldValue(i,0)));
					}
				} 
				else if(TABLE.Trim() == "RFBRANCH")
				{
					Comm = "select AREA_ID, AREA_NAME from AREA";
					conn.QueryString = Comm;
					conn.ExecuteQuery();
					
					for (int i=0; i< conn.GetRowCount(); i++)
					{
						this.DDL_VALUE.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
					}
				} 
				else
				{
					Comm = "select * from " + TABLE;
					conn.QueryString = Comm;

					try
					{
						conn.ExecuteQuery();
					
						for (int i=0; i< conn.GetRowCount(); i++)
						{
							this.DDL_VALUE.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
						}
					}
					catch { }
			
				}
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
			this.DGR_EXISTING_LIST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_LIST_ItemCommand);
			this.DGR_EXISTING_LIST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_LIST_PageIndexChanged);
			this.DGR_REQUEST_LIST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_LIST_ItemCommand);
			this.DGR_REQUEST_LIST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_LIST_PageIndexChanged);
			this.DGR_EXISTING_VALUE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_VALUE_ItemCommand);
			this.DGR_EXISTING_VALUE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_VALUE_PageIndexChanged);
			this.DGR_REQUEST_VALUE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_VALUE_ItemCommand);
			this.DGR_REQUEST_VALUE.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_VALUE_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_LIST_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select * from MANDIRI_PARAM_LIST WHERE PARAM_ID ='" + TXT_ID1.Text.Trim() + "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					GlobalTools.popMessage(this,"ID has already been used! Request canceled!");
					return;
				}
			}
		
			if (this.TXT_ID1.Text.Trim() == "" || this.TXT_PARAM_NAME.Text.Trim() == "" ) 
			{
				if (this.TXT_ID1.Text.Trim() == "")
				{
					GlobalTools.popMessage(this, "ID is required !");
					return;
				} 
				else 
				{
					GlobalTools.popMessage(this, "Parameter Name is required !");
					return;
				}
			} 
			else
			{
				int SEQID = DGR_REQUEST_LIST.Items.Count + 1;

				conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAMLIST_MAKER '"+ LBL_SAVEMODE.Text.Trim() + "', '" +
					this.TXT_ID1.Text.Trim() + "', '" + TXT_PARAM_NAME.Text.Trim() + "', '" + checkApost(TXT_PARAM_FORMULA.Text.Trim()) + "', '" +
					TXT_PARAM_FIELD.Text.Trim() + "', '" + TXT_PARAM_TABLE.Text.Trim() + "', '"+ TXT_PARAM_TABLE_CHILD.Text.Trim() + "', '"+
					checkApost(TXT_PARAM_LINK.Text.Trim()) + "', '" + this.RBL_PARAM_PRM.SelectedValue.ToString() + "', '" +
					this.RBL_PARAM_ACTIVE.SelectedValue.ToString() + "', '" + SEQID  + "'";
				
				try
				{
					conn.ExecuteQuery();
				} 
				catch 
				{
					GlobalTools.popMessage(this, "Error on stored procedure!");
				}

				ViewPendingParameterListData();
				clearEditParamListBoxes();
				this.LBL_SAVEMODE.Text = "1";
			}
		}

		protected void BTN_CANCEL_LIST_Click(object sender, System.EventArgs e)
		{
			clearEditParamListBoxes();
		}

		protected void BtnViewValue_Click(object sender, System.EventArgs e)
		{
			if (this.DDL_PROGRAM_TYPE.SelectedValue.ToString() == "")
			{	GlobalTools.popMessage(this, " Program Type is required!"); 
				GlobalTools.SetFocus(this,DDL_PROGRAM_TYPE);
				return;}
			else if (this.DDL_PRODUCT_TYPE.SelectedValue.ToString() == "")
			{ GlobalTools.popMessage(this, " Product Type is required!"); 
				GlobalTools.SetFocus(this,DDL_PRODUCT_TYPE);
				return;}
			else if (this.DDL_EMPLOYEE_TYPE.SelectedValue.ToString() == "")
			{ GlobalTools.popMessage(this, " Employee Type is required!");
				GlobalTools.SetFocus(this,DDL_EMPLOYEE_TYPE);
				return;}
			else if (this.Label4.Text.Trim() == "")
			{ GlobalTools.popMessage(this, " Parameter List ID has not been choose yet!"); return;}
	
			if(this.DDL_PROGRAM_TYPE.SelectedValue !="" || this.DDL_PRODUCT_TYPE.SelectedValue != "" || this.DDL_EMPLOYEE_TYPE.SelectedValue != "")
			{
				GetRequest();
				ViewExistingParameterValueData();
				ViewPendingParameterValueData();
				GlobalTools.SetFocus(this,TXT_ID2);
			}		
		}

		protected void BTN_SAVE_VALUE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE2.Text.Trim() == "1") 
			{ 
				conn.QueryString = "select * from MANDIRI_PARAM_VALUE " + 
					"WHERE PARAM_VALUE_ID ='" + this.TXT_ID2.Text.Trim() + "' " +
					"and PARAM_ID = '" + this.Label4.Text.Trim() + "' and REQUEST_ID = '" + LBL_REQUEST.Text.Trim() + "'";
				conn.ExecuteQuery();
				
				if (conn.GetRowCount() > 0) 
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}
			// end check ID
			if (this.TXT_ID2.Text.Trim() == "" || this.TXT_PARAM_NAME2.Text.Trim() == "" ) 
			{
				if (this.TXT_ID2.Text.Trim() == "")
				{
					GlobalTools.popMessage(this, "Parameter Value ID is required !");
					return;
				} 
				else 
				{
					GlobalTools.popMessage(this, "Parameter Value Name is required !");
					return;
				}
			} 
			else
			{
				int rowDGR = DGR_REQUEST_VALUE.Items.Count + 1 ; 
				
				LBL_SEQ.Text = rowDGR.ToString();
				
				
				if (this.TR_DDL_VALUE.Visible == false)
				{
					this.LBL_MIN.Text = this.TXT_MIN_VALUE.Text.Trim();
					this.LBL_MAX.Text = this.TXT_MAX_VALUE.Text.Trim();
				} 
				else if (this.TR_DDL_VALUE.Visible == true)
				{
					this.LBL_MIN.Text = this.DDL_VALUE.SelectedValue.ToString();
					this.LBL_MAX.Text = this.DDL_VALUE.SelectedItem.ToString();
				}

				conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAM_VALUE_MAKER '"+ LBL_SAVEMODE2.Text.Trim() +"', '"+
					this.Label4.Text.Trim() +"', '" + this.LBL_REQUEST.Text.Trim() + "', '" + this.TXT_ID2.Text.Trim() +"', '"+
					this.TXT_PARAM_NAME2.Text.Trim() +"', '"+ this.checkComma(this.LBL_MIN.Text) +"', '"+ 
					this.checkComma(this.LBL_MAX.Text) +"', "+ this.checkComma(this.TXT_PARAM_SCORE.Text) +", '"+
					this.LBL_SEQ.Text.Trim() +"'"; 
				
				try
				{
					conn.ExecuteNonQuery();
				} 
				catch 
				{
					GlobalTools.popMessage(this, "The input data is not valid!");
				}

				ViewPendingParameterValueData();
				clearEditParamValueBoxes();
				LBL_SAVEMODE2.Text = "1";
				GlobalTools.SetFocus(this,TXT_ID2);
			}
		}

		protected void BTN_CANCEL_VALUE_Click(object sender, System.EventArgs e)
		{
			clearEditParamValueBoxes();
			GlobalTools.SetFocus(this,TXT_ID2);
		}

		private void DGR_EXISTING_VALUE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING_VALUE.CurrentPageIndex = e.NewPageIndex;
			ViewExistingParameterValueData();		
		}

		private void DGR_REQUEST_VALUE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST_VALUE.CurrentPageIndex = e.NewPageIndex;
			ViewPendingParameterValueData();
		}

		private void DGR_EXISTING_VALUE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditParamValueBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE2.Text = "0";
					this.TXT_ID2.Text = cleansText(e.Item.Cells[0].Text);
					this.TXT_PARAM_NAME2.Text = cleansText(e.Item.Cells[1].Text);
					this.TXT_MIN_VALUE.Text = cleansText(e.Item.Cells[2].Text);
					this.TXT_MAX_VALUE.Text = cleansText(e.Item.Cells[3].Text);
					this.TXT_PARAM_SCORE.Text = cleansText(e.Item.Cells[4].Text);
					
					if (this.TR_DDL_VALUE.Visible == true)
					{
						try 
						{
							this.DDL_VALUE.SelectedValue = e.Item.Cells[2].Text;
						} 
						catch{}
					}
					activatePostBackControlsValue(false);
					GlobalTools.SetFocus(this,TXT_PARAM_NAME2); 
					break;	
				case "delete": 
					int seq = this.DGR_REQUEST_VALUE.Items.Count + 1;//conn.GetRowCount() + 1;
					Id = cleansText(e.Item.Cells[0].Text);

					conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAM_VALUE_MAKER '2', '"+ Label4.Text.Trim() +"', '"+
						LBL_REQUEST.Text.Trim() +"', '"+ cleansText(e.Item.Cells[0].Text) +"', '"+
						cleansText(e.Item.Cells[1].Text) +"', '"+ checkComma(cleansText(e.Item.Cells[2].Text.Trim())) +"', '"+
						checkComma(cleansText(e.Item.Cells[3].Text.Trim())) +"', "+ checkComma(cleansFloat(e.Item.Cells[4].Text)) +", '"+
						seq.ToString() + "' " ;
					conn.ExecuteNonQuery();

					activatePostBackControlsValue(true);
					ViewPendingParameterValueData();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_REQUEST_VALUE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditParamValueBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE2.Text = e.Item.Cells[5].Text.Trim();
					if (LBL_SAVEMODE2.Text.Trim() == "2")
					{
						LBL_SAVEMODE2.Text = "1";
						break;
					}
					
					this.TXT_ID2.Text = cleansText(e.Item.Cells[0].Text);
					this.TXT_PARAM_NAME2.Text = cleansText(e.Item.Cells[1].Text);
					this.TXT_MIN_VALUE.Text = cleansText(e.Item.Cells[2].Text);
					this.TXT_MAX_VALUE.Text = cleansText(e.Item.Cells[3].Text);
					this.TXT_PARAM_SCORE.Text = cleansText(e.Item.Cells[4].Text);
					
					if (this.TR_DDL_VALUE.Visible == true)
					{
						try 
						{	
							this.DDL_VALUE.SelectedValue = e.Item.Cells[2].Text;
						}
						catch {}
					}
					
					activatePostBackControlsValue(false);
					GlobalTools.SetFocus(this,TXT_PARAM_NAME2);
					break;
				case "delete":
					Id = cleansText(e.Item.Cells[0].Text);
					
					conn.QueryString = "DELETE FROM TMANDIRI_PARAM_VALUE " + 
						"WHERE PARAM_ID = '"+ Label4.Text + "' and REQUEST_ID = '" + LBL_REQUEST.Text+ "' " +
						"and PARAM_VALUE_ID = '" + cleansText(e.Item.Cells[0].Text)+ "' " +
						"and SEQ_ID = '" + cleansText(e.Item.Cells[6].Text) + "' ";
					conn.ExecuteQuery();
					
					activatePostBackControlsValue(true);
					ViewPendingParameterValueData();
					break;
				default:
					// do nothing
					break;
			}
		}


		private void DGR_REQUEST_LIST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditParamListBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[11].Text.Trim();
					if (LBL_SAVEMODE.Text.Trim() == "2")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					this.TXT_ID1.Text = cleansText(e.Item.Cells[1].Text);;
					this.TXT_PARAM_NAME.Text = cleansText(e.Item.Cells[2].Text);
					this.TXT_PARAM_FORMULA.Text =cleansText(e.Item.Cells[3].Text);
					this.TXT_PARAM_FIELD.Text = cleansText(e.Item.Cells[4].Text);
					this.TXT_PARAM_TABLE.Text = cleansText(e.Item.Cells[5].Text);
					this.TXT_PARAM_TABLE_CHILD.Text = cleansText(e.Item.Cells[6].Text);
					this.TXT_PARAM_LINK.Text = cleansText(e.Item.Cells[7].Text);
					try 
					{	
						this.RBL_PARAM_PRM.SelectedValue =e.Item.Cells[8].Text;
					}
					catch {}
					try 
					{
						this.RBL_PARAM_ACTIVE.SelectedValue = e.Item.Cells[9].Text;
					} 
					catch{}
					activatePostBackControlsList(false);
					break;
				case "delete":
					Id = cleansText(e.Item.Cells[1].Text);
					
					conn.QueryString = "DELETE FROM TMANDIRI_PARAM_LIST WHERE PARAM_ID = '"+ Id + "' ";
					conn.ExecuteQuery();
					
					activatePostBackControlsList(true);
					ViewPendingParameterListData();
					break;
				default:
					// do nothing
					break;
			}
			
		}

		private void DGR_EXISTING_LIST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditParamListBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					this.TXT_ID1.Text = cleansText(e.Item.Cells[1].Text);
					this.TXT_PARAM_NAME.Text = cleansText(e.Item.Cells[2].Text);
					this.TXT_PARAM_FORMULA.Text =cleansText(e.Item.Cells[3].Text);
					this.TXT_PARAM_FIELD.Text = cleansText(e.Item.Cells[4].Text);
					this.TXT_PARAM_TABLE.Text = cleansText(e.Item.Cells[5].Text);
					this.TXT_PARAM_TABLE_CHILD.Text = cleansText(e.Item.Cells[6].Text);
					this.TXT_PARAM_LINK.Text = cleansText(e.Item.Cells[7].Text);
					try 
					{	
						this.RBL_PARAM_PRM.SelectedValue =e.Item.Cells[8].Text;
					}
					catch {}
					try 
					{
						this.RBL_PARAM_ACTIVE.SelectedValue = e.Item.Cells[9].Text;
					} 
					catch{}
					activatePostBackControlsList(false);
					break;
				case "delete":
					conn.QueryString= "SELECT * FROM TMANDIRI_PARAM_LIST";
					conn.ExecuteQuery();
					int seq = conn.GetRowCount() + 1;
					Id = cleansText(e.Item.Cells[1].Text);
					
					conn.QueryString = "EXEC PARAM_SCORING_MANDIRI_PARAMLIST_MAKER '2', '"+ Id + "', '"+
						cleansText(e.Item.Cells[2].Text) +"', '"+ cleansText(e.Item.Cells[3].Text) +"', '"+
						cleansText(e.Item.Cells[4].Text) +"', '"+ cleansText(e.Item.Cells[5].Text) +"', '"+
						cleansText(e.Item.Cells[6].Text) +"', '"+ cleansText(e.Item.Cells[7].Text) +"', '"+
						cleansText(e.Item.Cells[8].Text) +"', '"+ cleansText(e.Item.Cells[9].Text) +"', '"+
						seq.ToString() + "' ";
					conn.ExecuteQuery();

					activatePostBackControlsList(true);
					ViewPendingParameterListData();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void DGR_REQUEST_LIST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST_LIST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingParameterListData();
		}

		private void DGR_EXISTING_LIST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING_LIST.CurrentPageIndex = e.NewPageIndex;
			ViewExistingParameterListData();
			refreshRadioButton();
		}

		protected void RBL_PARAM_PRM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Label1.Text = "1";
			Label5.Text = "0";
			LBL_PRM.Text = RBL_PARAM_PRM.SelectedValue;  
			ViewExistingParameterListData(); 
			refreshRadioButton();
		}

		protected void DDL_PROGRAM_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillProductType();
		}
		
	}
}
