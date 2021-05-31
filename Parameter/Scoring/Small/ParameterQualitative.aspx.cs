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

namespace CuBES_Maintenance.Parameter.Scoring.Small
{
	/// <summary>
	/// Summary description for ParameterQualitative.
	/// </summary>
	public partial class ParameterQualitative : System.Web.UI.Page
	{
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			_btnFind.Click += new EventHandler(_btnFind_Click);
			_btnNew.Click += new EventHandler(_btnNew_Click);

			conn = new Connection(ConfigurationSettings.AppSettings["connME"]);

			if(!IsPostBack)
			{
				TR_EDIT_PARAMETER.Visible = false;
				TR_NEW_PARAMETER.Visible = false;

				FillDllOperator();

				conn.QueryString = "EXEC SCORING_BINDRULEREASONPARAMETER";		//do not auto load anything.. 
				conn.ExecuteQuery();

				BindData();

				TR_RULEREASON_TEMP.Visible = false;
			}

			BindData("DatGridRuleReason","EXEC SCORING_BINDTEMP 'RULEREASON'");
		}

		private void FillDllOperator()
		{
			for(int i=0; i<=5; i++)
			{
				if(i == 0)
				{
					_ddlEditedOperator.Items.Add(new ListItem("=",i.ToString()));
					_ddlNewOperator.Items.Add(new ListItem("=", i.ToString()));
					_ddlOperatorTemp.Items.Add(new ListItem("=", i.ToString()));
				}
				else if(i == 1)
				{
					_ddlEditedOperator.Items.Add(new ListItem("<",i.ToString()));
					_ddlNewOperator.Items.Add(new ListItem("<", i.ToString()));
					_ddlOperatorTemp.Items.Add(new ListItem("<", i.ToString()));
				}
				else if(i == 2)
				{
					_ddlEditedOperator.Items.Add(new ListItem(">",i.ToString()));
					_ddlNewOperator.Items.Add(new ListItem(">", i.ToString()));
					_ddlOperatorTemp.Items.Add(new ListItem(">", i.ToString()));
				}
				else if(i == 3)
				{
					_ddlEditedOperator.Items.Add(new ListItem(">=",i.ToString()));
					_ddlNewOperator.Items.Add(new ListItem(">=", i.ToString()));
					_ddlOperatorTemp.Items.Add(new ListItem(">=", i.ToString()));
				}
				else if(i == 4)
				{
					_ddlEditedOperator.Items.Add(new ListItem("<=",i.ToString()));
					_ddlNewOperator.Items.Add(new ListItem("<=", i.ToString()));
					_ddlOperatorTemp.Items.Add(new ListItem("<=", i.ToString()));
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
			this.DatGrd.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGrd_ItemCommand);
			this.DatGrd.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGrd_PageIndexChanged);
			this.DatGridRuleReason.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGridRuleReason_ItemCommand);

		}
		#endregion

		private void BindData()
		{
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DatGrd.DataSource = dt;				
			DatGrd.DataBind();
		}

		private void BindData1()
		{
			conn.QueryString = "EXEC SCORING_BINDRULEREASONPARAMETER";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DatGrd.DataSource = dt;

			try
			{
				DatGrd.DataBind();
			}
			catch 
			{
				DatGrd.CurrentPageIndex = DatGrd.PageCount - 1;
				DatGrd.DataBind();
			}

			conn.ClearData();
		}

		private void DatGrd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGrd.CurrentPageIndex >= 0 && DatGrd.CurrentPageIndex < DatGrd.PageCount)
			{
				DatGrd.CurrentPageIndex = e.NewPageIndex;
				BindData1();
			}
		}

		private void ClearData()
		{
			_txtEditedDesc.Text = "";
			_txtEditedID.Text = "";
			_txtEditedParamRequest.Text = "";
			_txtEditedQueryGetValue.Text = "";
			_txtEditedResult.Text = "";
			_txtEditedRRC.Text = "";

			_txtNewDesc.Text = "";
			_txtNewParamRequest.Text = "";
			_txtNewQueryGetValue.Text = "";
			_txtNewResult.Text = "";
			_txtNewRuleReasonCode.Text = "";
			
			_txtParamName.Text = "";
			_txtIDQualitative.Text = "";
		}

		private void TurnOnVisibleNewParameter(bool stat)
		{
			TR_EDIT_PARAMETER.Visible = !stat;
			TR_NEW_PARAMETER.Visible = stat;
		}

		private void TurnOnVisibleEditParameter(bool stat)
		{
			TR_EDIT_PARAMETER.Visible = stat;
			TR_NEW_PARAMETER.Visible = !stat;
		}

		private void _btnFind_Click(object sender, System.EventArgs e)
		{

			if(_txtIDQualitative.Text.ToString() == "" && _txtParamName.Text.ToString() == "")
			{
				conn.QueryString = "EXEC SCORING_BINDRULEREASONPARAMETER";
				conn.ExecuteQuery();
			}
			else if(_txtIDQualitative.Text.ToString() != "" && _txtParamName.Text.ToString() == "")
			{
				conn.QueryString = "EXEC SCORING_BINDRULEREASONPARAMETER '" + _txtIDQualitative.Text.ToString() + "',''";
				conn.ExecuteQuery();
			}
			else if(_txtIDQualitative.Text.ToString() == "" && _txtParamName.Text.ToString() != "")
			{
				conn.QueryString = "EXEC SCORING_BINDRULEREASONPARAMETER NULL,'" + _txtParamName.Text.ToString() + "'";
				conn.ExecuteQuery();
			}
			else if(_txtIDQualitative.Text.ToString() != "" && _txtParamName.Text.ToString() != "")
			{
				conn.QueryString = "EXEC SCORING_BINDRULEREASONPARAMETER '" + _txtIDQualitative.Text.ToString() + "','" + _txtParamName.Text.ToString() + "'";
				conn.ExecuteQuery();
			}

			BindData();
			ClearData();
		}

		private void _btnNew_Click(object sender, System.EventArgs e)
		{
			TurnOnVisibleNewParameter(true);
			ClearData();
		}

		private void cekEditedField(string id)
		{
			conn.QueryString = "SELECT * FROM RFSCORINGRULEREASON WHERE ID = '" + id + "'";
			conn.ExecuteQuery();

			_txtEditedDesc.Text = conn.GetFieldValue("DESCRIPTION");
			_txtEditedID.Text = conn.GetFieldValue("ID");
			_txtEditedParamRequest.Text = conn.GetFieldValue("COLUMNNAME");
			_txtEditedQueryGetValue.Text = conn.GetFieldValue("QUERYGETVALUE");
			_txtEditedResult.Text = conn.GetFieldValue("QUERYCOMPARATION");
			_txtEditedRRC.Text = conn.GetFieldValue("REASON_CODE");

			_ddlEditedOperator.SelectedValue = conn.GetFieldValue("ISQUERY");

			string isactive = conn.GetFieldValue("ISACTIVE");
			_rdBtnEditedStatus.SelectedValue = isactive;
		}

		private void DatGrd_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TurnOnVisibleEditParameter(true);
					_txtRRID.Text = e.Item.Cells[0].Text.ToString();
					cekEditedField(e.Item.Cells[0].Text.ToString());
					TR_RULEREASON_TEMP.Visible = false;
					break;
			}
		}

		protected void _btnEditedUpdate_Click(object sender, System.EventArgs e)
		{
			/*conn.QueryString = "EXEC SCORING_INSERTSCORINGRULEREASON '" +
				_txtNewDesc.Text.ToString() + "','" +
				_txtNewRuleReasonCode.Text.ToString() + "','" +
				_txtNewQueryGetValue.Text.ToString() + "','" +
				_txtNewParamRequest.Text.ToString() + "'," +
				Convert.ToInt32(_ddlNewOperator.SelectedValue.ToString()) + ",'" +
				_txtNewResult.Text.ToString() + "','" +
				_rdBtnNewStatus.SelectedValue.ToString() + "'";*/

			int success = 1;
			
			if(_txtEditedDesc.Text.ToString() == "")
			{
				Tools.popMessage(this, "Description cannot be empty !");
				success = 0;
			}
			if(_txtEditedRRC.Text.ToString() == "")
			{
				Tools.popMessage(this, "Rule Reason cannot be empty !");
				success = 0;
			}
			if(_txtEditedQueryGetValue.Text.ToString() == "")
			{
				Tools.popMessage(this, "Query Get Value cannot be empty !");
				success = 0;
			}
			if(_txtEditedParamRequest.Text.ToString() == "")
			{
				Tools.popMessage(this, "ParamRequest cannot be empty !");
				success = 0;
			}
			if(_txtEditedResult.Text.ToString() == "")
			{
				Tools.popMessage(this, "Result cannot be empty !");
				success = 0;
			}

			if(success == 1)
			{
				/*conn.QueryString = "UPDATE RFSCORINGRULEREASON SET DESCRIPTION = '" + _txtEditedDesc.Text.ToString() 
					+ "', REASON_CODE = '" + _txtEditedRRC.Text.ToString() 
					+ "', ISACTIVE = '" + _rdBtnEditedStatus.SelectedValue.ToString()
					+ "', QUERYGETVALUE = '" + _txtEditedQueryGetValue.Text.ToString()
					+ "', QUERYCOMPARATION = '" + _txtEditedResult.Text.ToString() 
					+ "', ISQUERY = '" + _ddlEditedOperator.SelectedValue.ToString()
					+ "', COLUMNNAME = '" + _txtEditedParamRequest.Text.ToString() 
					+ "' WHERE RFSCORINGRULEREASON.ID = '" + _txtEditedID.Text.ToString() + "'";
				conn.ExecuteNonQuery();*/

				conn.QueryString = "EXEC SCORING_INSERTSCORINGRULEREASONTEMP '" + 
					_txtEditedDesc.Text.ToString() + "','" +
					_txtEditedRRC.Text.ToString() + "','" +
					_rdBtnEditedStatus.SelectedValue.ToString() + "','" +
					_txtEditedQueryGetValue.Text.ToString() + "','" +
					_txtEditedResult.Text.ToString() + "','" +
					_ddlEditedOperator.SelectedValue.ToString() + "','" +
					_txtEditedParamRequest.Text.ToString() + "','" +
					"UPDATE','" + _txtRRID.Text + "'";

				/*
				 *	@DESCRIPTION VARCHAR(50),
					@REASON_CODE VARCHAR(10),
					@ISACTIVE VARCHAR(50),
					@QUERYGETVALUE VARCHAR(200),
					@QUERYCOMPARATION varchar(200),
					@ISQUERY VARCHAR(10),
					@COLUMNNAME VARCHAR(50),
					@STATUS VARCHAR(50),
					@IDPREV VARCHAR(50)
				 * */
				conn.ExecuteQuery();

				Tools.popMessage(this, "Request for Approval !");
				TR_EDIT_PARAMETER.Visible = false;
				BindData("DatGridRuleReason","EXEC SCORING_BINDTEMP 'RULEREASON'");
				BindData1();
			}
		}

		private void BindData(string dataGridName, string strconn)
		{
			if(strconn != "")
			{
				conn.QueryString = strconn;
				conn.ExecuteQuery();
			}

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();

			System.Web.UI.WebControls.DataGrid dg = (System.Web.UI.WebControls.DataGrid)Page.FindControl(dataGridName);

			dg.DataSource = dt;				

			try
			{
				dg.DataBind();
			}
			catch 
			{
				dg.CurrentPageIndex = dg.PageCount - 1;
				dg.DataBind();
			}

			conn.ClearData();
		}

		protected void _btnNewUpdate_Click(object sender, System.EventArgs e)
		{
			/*Tools.popMessage(this, _txtNewDesc.Text.ToString());
			Tools.popMessage(this, _txtNewRuleReasonCode.Text.ToString());
			Tools.popMessage(this, _txtNewQueryGetValue.Text.ToString());
			Tools.popMessage(this, _txtNewParamRequest.Text.ToString());
			Tools.popMessage(this, _ddlNewOperator.SelectedValue.ToString().ToString());
			Tools.popMessage(this, _txtNewResult.Text.ToString());
			Tools.popMessage(this, _rdBtnNewStatus.SelectedValue.ToString());*/

			int success = 1;
			
			if(_txtNewDesc.Text.ToString() == "")
			{
				Tools.popMessage(this, "Description cannot be empty !");
				success = 0;
			}
			if(_txtNewRuleReasonCode.ToString() == "")
			{
				Tools.popMessage(this, "Rule Reason cannot be empty !");
				success = 0;
			}
			if(_txtNewQueryGetValue.ToString() == "")
			{
				Tools.popMessage(this, "Query Get Value cannot be empty !");
				success = 0;
			}
			if(_txtNewParamRequest.ToString() == "")
			{
				Tools.popMessage(this, "ParamRequest cannot be empty !");
				success = 0;
			}
			if(_txtNewResult.Text.ToString() == "")
			{
				Tools.popMessage(this, "Result cannot be empty !");
				success = 0;
			}

			if(success == 1)
			{
				/*conn.QueryString = "EXEC SCORING_INSERTSCORINGRULEREASON '" +
					_txtNewDesc.Text.ToString() + "','" +
					_txtNewRuleReasonCode.Text.ToString() + "','" +
					_txtNewQueryGetValue.Text.ToString() + "','" +
					_txtNewParamRequest.Text.ToString() + "'," +
					Convert.ToInt32(_ddlNewOperator.SelectedValue.ToString()) + ",'" +
					_txtNewResult.Text.ToString() + "','" +
					_rdBtnNewStatus.SelectedValue.ToString() + "'";
				conn.ExecuteNonQuery();*/

				conn.QueryString = "EXEC SCORING_INSERTSCORINGRULEREASONTEMP '" + 
					_txtNewDesc.Text.ToString() + "','" +
					_txtNewRuleReasonCode.Text.ToString() + "','" +
					_rdBtnNewStatus.SelectedValue.ToString() + "','" +
					_txtNewQueryGetValue.Text.ToString() + "','" +
					_txtNewResult.Text.ToString() + "','" +
					_ddlNewOperator.SelectedValue.ToString() + "','" +
					_txtNewParamRequest.Text.ToString() + "','" +
					"INSERT','-1'";

				Tools.popMessage(this, "Request for aproval...");
				TR_NEW_PARAMETER.Visible = false;
				BindData1();
			}
		}

		protected void _ddlEditedOperator_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void DatGridRuleReason_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TR_RULEREASON_TEMP.Visible = true;
					//e.Item.Cells[0].Text.ToString();

					conn.QueryString = "SELECT * FROM RFSCORINGRULEREASONTEMP WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();

					_txtIDRRTemp.Text = conn.GetFieldValue("ID");
					_txtDescRRTemp.Text = conn.GetFieldValue("DESCRIPTION");
					_txtCodeRRTemp.Text = conn.GetFieldValue("REASON_CODE");
					_txtQueryRRTemp.Text = conn.GetFieldValue("QUERYGETVALUE");
					_txtParamRRTemp.Text = conn.GetFieldValue("COLUMNNAME");
					_ddlOperatorTemp.SelectedValue = conn.GetFieldValue("ISQUERY");
					_txtResultRRTemp.Text = conn.GetFieldValue("QUERYCOMPARATION");
					_rdoStatusRRTemp.SelectedValue = conn.GetFieldValue("ISACTIVE");

					break;
				case "Delete":
					conn.QueryString = "DELETE RFSCORINGRULEREASONTEMP WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();

					TR_RULEREASON_TEMP.Visible = false;
					BindData("DatGridRuleReason","EXEC SCORING_BINDTEMP 'RULEREASON'");
					Tools.popMessage(this, "Success !");
					break;
			}
		}

		protected void _btnSaveRRTemp_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "UPDATE RFSCORINGRULEREASONTEMP SET DESCRIPTION = '" + _txtDescRRTemp.Text + "'," +
				"REASON_CODE = '" + _txtCodeRRTemp.Text + "'," +
				"QUERYGETVALUE = '" + _txtQueryRRTemp.Text + "'," +
				"COLUMNNAME = '" + _txtParamRRTemp.Text + "'," +
				"ISQUERY = '" + _ddlOperatorTemp.SelectedValue + "'," +
				"QUERYCOMPARATION = '" + _txtResultRRTemp.Text + "'," +
				"ISACTIVE = '" + _rdoStatusRRTemp.SelectedValue + "' " +
				"WHERE [ID] = '" + _txtIDRRTemp.Text + "'";
			conn.ExecuteQuery();

			TR_RULEREASON_TEMP.Visible = false;
			BindData("DatGridRuleReason","EXEC SCORING_BINDTEMP 'RULEREASON'");
			Tools.popMessage(this, "Request for approval !");
		}
	}
}
