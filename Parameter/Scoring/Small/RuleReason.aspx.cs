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
	/// Summary description for RuleReason.
	/// </summary>
	public partial class RuleReason : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButton _rdBtnEditedEnable;
		protected System.Web.UI.WebControls.RadioButton _rdBtnEditedDisable;
		protected System.Web.UI.WebControls.RadioButtonList Radiobuttonlist2;
		protected System.Web.UI.WebControls.TextBox _txtNewValue;
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(ConfigurationSettings.AppSettings["connME"]);

			if(!IsPostBack)
			{
				TR_EDIT_PARAMETER.Visible = false;
				TR_NEW_PARAMETER.Visible = false;
				TR_ATTRIBUTE_RANGE.Visible = false;
				TR_ATTRIBUTE_NONRANGE.Visible = false;

				TR_ATTRIBUTE_TEMP.Visible = false;
				TR_ATTRANGE_TEMP.Visible = false;
				TR_ATTNONRANGE_TEMP.Visible = false;
			}

			//_btnNew.Click += new EventHandler(_btnNew_Click);

			conn.QueryString = "EXEC SCORING_BINDATTRIBUTE";		//do not auto load anything.. 
			conn.ExecuteQuery();

			BindData();
			BindData("DatGridAttributeReq","EXEC SCORING_BINDATTRIBUTETEMP");
			BindData("DatGridAttRangeReq","EXEC SCORING_BINDRANGEATTTEMP");
			BindData("DatGridAttNonRangeReq","EXEC SCORING_BINDNONRANGEATTTEMP");
		}

		private void BindData()
		{
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DatGrd.DataSource = dt;				
			DatGrd.DataBind();
		}

		protected void _btnUpdateAttNonRangeTemp_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "UPDATE RFSCORINGWEIGHTNONRANGETEMP SET VALUE = '" + _txtValueAttNonRangeTemp.Text + "'," +
				"QUERYTXT = '" + _txtDescAttNonRangeTemp.Text + "'," +
				"WEIGHT = '" + _txtWeightAttNonRangeTemp.Text + "' " +
				"WHERE [ID] = '" + _txtIdAttNonRangeTemp.Text + "'";
			conn.ExecuteQuery();
			BindData("DatGridAttNonRangeReq","EXEC SCORING_BINDNONRANGEATTTEMP");
			TR_ATTNONRANGE_TEMP.Visible = false;

			/*AttNonRangeTemp
			_txtDescAttNonRangeTemp		
			_txtValueAttNonRangeTemp
			_txtWeightAttNonRangeTemp*/
		}



		private void DatGridAttributeReq_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGridAttributeReq.CurrentPageIndex >= 0 && DatGridAttributeReq.CurrentPageIndex < DatGridAttributeReq.PageCount)
			{
				DatGridAttributeReq.CurrentPageIndex = e.NewPageIndex;
				BindData("DatGridAttributeReq","EXEC SCORING_BINDATTRIBUTETEMP");
			}
		}

		private void DatGridAttRangeReq_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGridAttRangeReq.CurrentPageIndex >= 0 && DatGridAttRangeReq.CurrentPageIndex < DatGridAttRangeReq.PageCount)
			{
				DatGridAttRangeReq.CurrentPageIndex = e.NewPageIndex;
				BindData("DatGridAttRangeReq","EXEC SCORING_BINDRANGEATTTEMP");
			}
		}

		private void DatGridAttNonRangeReq_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGridAttNonRangeReq.CurrentPageIndex >= 0 && DatGridAttNonRangeReq.CurrentPageIndex < DatGridAttNonRangeReq.PageCount)
			{
				DatGridAttNonRangeReq.CurrentPageIndex = e.NewPageIndex;
				BindData("DatGridAttNonRangeReq","EXEC SCORING_BINDNONRANGEATTTEMP");
			}
		}

		protected void _btnUpdateAttRangeTemp_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "UPDATE RFSCORINGWEIGHTRANGETEMP SET LOWESTSCORE = '" + _txtLowestAttRangeTemp.Text + "'," +
				"HIGHESTSCORE = '" + _txtHighestAttRangeTemp.Text + "'," +
				"WEIGHT = '" + _txtWeightAttRangeTemp.Text + "' " +
				"WHERE [ID] = '" + _txtIdAttRangeTemp.Text + "'";

			if(_rdoConditionAttRangeTemp.SelectedValue == "2")
			{
				conn.QueryString = "UPDATE RFSCORINGWEIGHTRANGETEMP SET LOWESTSCORE = '" + _txtLowestAttRangeTemp.Text + "'," +
					"HIGHESTSCORE = 'HIGH'," +
					"WEIGHT = '" + _txtWeightAttRangeTemp.Text + "' " +
					"WHERE [ID] = '" + _txtIdAttRangeTemp.Text + "'";
			}
			else if(_rdoConditionAttRangeTemp.SelectedValue == "3")
			{
				conn.QueryString = "UPDATE RFSCORINGWEIGHTRANGETEMP SET LOWESTSCORE = 'BELOW'," +
					"HIGHESTSCORE = '" + _txtHighestAttRangeTemp.Text + "'," +
					"WEIGHT = '" + _txtWeightAttRangeTemp.Text + "' " +
					"WHERE [ID] = '" + _txtIdAttRangeTemp.Text + "'";
			}
			else if(_rdoConditionAttRangeTemp.SelectedValue == "1")
			{
				conn.QueryString = "UPDATE RFSCORINGWEIGHTRANGETEMP SET LOWESTSCORE = 'NO INFORMATION'," +
					"HIGHESTSCORE = 'NO INFORMATION'," +
					"WEIGHT = '" + _txtWeightAttRangeTemp.Text + "' " +
					"WHERE [ID] = '" + _txtIdAttRangeTemp.Text + "'";
			}
			else if(_rdoConditionAttRangeTemp.SelectedValue == "0")
			{
				conn.QueryString = "UPDATE RFSCORINGWEIGHTRANGETEMP SET LOWESTSCORE = '" + _txtLowestAttRangeTemp.Text + "'," +
					"HIGHESTSCORE = '" + _txtHighestAttRangeTemp.Text + "'," +
					"WEIGHT = '" + _txtWeightAttRangeTemp.Text + "' " +
					"WHERE [ID] = '" + _txtIdAttRangeTemp.Text + "'";
			}
			conn.ExecuteQuery();

			BindData("DatGridAttRangeReq","EXEC SCORING_BINDRANGEATTTEMP");
			TR_ATTRANGE_TEMP.Visible = false;
		}

		protected void _btnUpdateAttribute_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "UPDATE RFSCORINGATTRIBUTETEMP SET DESCRIPT = '" + _txtDescAttTemp.Text + "'," +
				"COLUMNNAME = '" + _txtColumnAttTemp.Text + "'," +
				"QUERYTXT = '" + _txtQueryAttTemp.Text + "'," +
				"PARAMNAME = '" + _txtParameterAttTemp.Text + "'," +
				"ISRANGE = '" + _rdoTypeAttTemp.SelectedValue + "'," +
				"ISACTIVE = '" + _rdoStatusAttTemp.SelectedValue + "' " +
				"WHERE [ID] = '" + _txtIDAttributeTemp.Text + "'";
			conn.ExecuteQuery();

			BindData("DatGridAttributeReq","EXEC SCORING_BINDATTRIBUTETEMP");
			TR_ATTRIBUTE_TEMP.Visible = false;
		}

		private void DatGridAttributeReq_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
					//e.Item.Cells[0].Text.ToString()
				case "Edit":
					TR_ATTRIBUTE_TEMP.Visible = true;
					_txtIDAttributeTemp.Text = e.Item.Cells[0].Text.ToString();
					conn.QueryString = "SELECT DESCRIPT, COLUMNNAME, ISRANGE, ISACTIVE, " +
						"QUERYTXT, PARAMNAME, STATUS, IDPREV FROM RFSCORINGATTRIBUTETEMP " +
						"WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					
					_txtIDAttTemp.Text = e.Item.Cells[0].Text.ToString();
					_txtDescAttTemp.Text = conn.GetFieldValue("DESCRIPT");
					_txtQueryAttTemp.Text = conn.GetFieldValue("QUERYTXT");
					_txtParameterAttTemp.Text = conn.GetFieldValue("PARAMNAME");
					_txtColumnAttTemp.Text = conn.GetFieldValue("COLUMNNAME");

					if(conn.GetFieldValue("ISRANGE") == "1")
					{
						_rdoTypeAttTemp.SelectedValue = "1";
					}
					else
					{
						_rdoTypeAttTemp.SelectedValue = "0";
					}

					if(conn.GetFieldValue("STATUS") == "1")
					{
						_rdoStatusAttTemp.SelectedValue = "1";
					}
					else
					{
						_rdoStatusAttTemp.SelectedValue = "0";
					}
					BindData("DatGridAttributeReq","EXEC SCORING_BINDATTRIBUTETEMP");
					break;
				case "Delete":
					conn.QueryString = "DELETE RFSCORINGATTRIBUTETEMP WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					BindData("DatGridAttributeReq","EXEC SCORING_BINDATTRIBUTETEMP");
					TR_ATTRIBUTE_TEMP.Visible = false;
					break;

				
			}
		}

		private void DatGridAttRangeReq_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					conn.QueryString = "SELECT RFSCORINGWEIGHTRANGETEMP.[ID], IDATTRIBUTE, " +
						"LOWESTSCORE, HIGHESTSCORE, WEIGHT, STATUS, IDPREV " +
						"FROM RFSCORINGWEIGHTRANGETEMP " +
						"WHERE RFSCORINGWEIGHTRANGETEMP.[ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();

					_txtIdAttRangeTemp.Text = conn.GetFieldValue("ID");
					_txtLowestAttRangeTemp.Text = conn.GetFieldValue("LOWESTSCORE");
					_txtHighestAttRangeTemp.Text = conn.GetFieldValue("HIGHESTSCORE");
					_txtWeightAttRangeTemp.Text = conn.GetFieldValue("WEIGHT");
					
					if(conn.GetFieldValue("HIGHESTSCORE") == "HIGH")
					{
						_rdoConditionAttRangeTemp.SelectedValue = "2";
					}
					else if(conn.GetFieldValue("LOWESTSCORE") == "BELOW")
					{
						_rdoConditionAttRangeTemp.SelectedValue = "3";
					}
					else if(conn.GetFieldValue("LOWESTSCORE").Replace(" ","").ToString() == "NOINFORMATION" 
						&& conn.GetFieldValue("HIGHESTSCORE").Replace(" ","").ToString() == "NOINFORMATION")
					{
						_rdoConditionAttRangeTemp.SelectedValue = "1";
					}
					else
					{
						_rdoConditionAttRangeTemp.SelectedValue = "0";
					}

					BindData("DatGridAttRangeReq","EXEC SCORING_BINDRANGEATTTEMP");
					TR_ATTRANGE_TEMP.Visible = true;
					break;
				case "Delete":
					conn.QueryString = "DELETE RFSCORINGWEIGHTRANGETEMP WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					BindData("DatGridAttRangeReq","EXEC SCORING_BINDRANGEATTTEMP");
					TR_ATTRANGE_TEMP.Visible = false;
					break;
			}
		}

		private void DatGridAttNonRangeReq_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TR_ATTNONRANGE_TEMP.Visible = true;

					conn.QueryString = "SELECT RFSCORINGWEIGHTNONRANGETEMP.[ID], VALUE, QUERYTXT, WEIGHT " +
						"FROM RFSCORINGWEIGHTNONRANGETEMP " +
						"WHERE RFSCORINGWEIGHTNONRANGETEMP.[ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();

					_txtIdAttNonRangeTemp.Text = conn.GetFieldValue("ID");
					_txtDescAttNonRangeTemp.Text = conn.GetFieldValue("QUERYTXT");
					_txtValueAttNonRangeTemp.Text = conn.GetFieldValue("VALUE");
					_txtWeightAttNonRangeTemp.Text = conn.GetFieldValue("WEIGHT");

					BindData("DatGridAttNonRangeReq","EXEC SCORING_BINDNONRANGEATTTEMP");

					break;
				case "Delete":
					conn.QueryString = "DELETE RFSCORINGWEIGHTNONRANGETEMP WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					BindData("DatGridAttNonRangeReq","EXEC SCORING_BINDNONRANGEATTTEMP");
					TR_ATTNONRANGE_TEMP.Visible = false;
					break;
			}
		}

		/*private void ClearData()
		{
			_TxtIDParam.Text = "";
			_TxtRuleName.Text = "";

			_txtEditedID.Text = "";
			_txtEditedDesc.Text = "";
			_txtEditedQuery.Text = "";
			_rdoEditedStatus.SelectedValue = "0";
			_rdoEditedType.SelectedValue = "0";

			_txtNewDesc.Text = "";
			_txtNewValue.Text = "";
			_rdoNewStatus.SelectedValue = "0";
			_rdoNewType.SelectedValue = "0";

			_txtEditedAttributeNonRangeDesc.Text = "";
			_txtEditedAttributeNonRangeID.Text = "";
			_txtEditedAttributeNonRangeValue.Text = "";
			_txtEditedAttributeNonRangeWeight.Text = "";
			_txtEditedAttributeRangeHighest.Text = "";
			_txtEditedAttributeRangeID.Text = "";
			_txtEditedAttributeRangeLowest.Text = "";
			_txtEditedAttributeRangeWeight.Text = "";
		}*/

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
			this.DatGridAttributeRange.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGridAttributeRange_ItemCommand_1);
			this.DatGridAttributeRange.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGridAttributeRange_PageIndexChanged);
			this.DatGridAttributeNonRange.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGridAttributeNonRange_ItemCommand);
			this.DatGridAttributeNonRange.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGridAttributeNonRange_PageIndexChanged);
			this.DatGridAttributeReq.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGridAttributeReq_ItemCommand);
			this.DatGridAttributeReq.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGridAttributeReq_PageIndexChanged);
			this.DatGridAttRangeReq.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGridAttRangeReq_ItemCommand);
			this.DatGridAttRangeReq.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGridAttRangeReq_PageIndexChanged);
			this.DatGridAttNonRangeReq.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGridAttNonRangeReq_ItemCommand);
			this.DatGridAttNonRangeReq.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGridAttNonRangeReq_PageIndexChanged);

		}
		#endregion

		/*private void _btnNew_Click(object sender, EventArgs e)
		{
			TurnOnVisibleNewParameter(true);
		}*/

		private void TurnOnVisibleNewParameter(bool stat)
		{
			TR_EDIT_PARAMETER.Visible = !stat;
			TR_ATTRIBUTE_NONRANGE.Visible = !stat;
			TR_ATTRIBUTE_RANGE.Visible = !stat;
			TR_NEW_PARAMETER.Visible = stat;
		}

		private void TurnOnVisibleEditParameter(bool stat)
		{
			TR_EDIT_PARAMETER.Visible = stat;
			TR_NEW_PARAMETER.Visible = !stat;
			TR_ATTRIBUTE_NONRANGE.Visible = !stat;
			TR_ATTRIBUTE_RANGE.Visible = !stat;
		}

		private void DatGrd_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TurnOnVisibleEditParameter(true);
					_txtIDAttribute.Text = e.Item.Cells[0].Text.ToString();
					cekEditedField(e.Item.Cells[0].Text.ToString());
					break;
			}
		}

		private void cekEditedField(string id)
		{
			conn.QueryString = "SELECT * FROM RFSCORINGATTRIBUTE WHERE ID = '" + id + "'";		//do not auto load anything.. 
			conn.ExecuteQuery();

			_txtEditedID.Text = conn.GetFieldValue("ID").ToString();
			_txtEditedDesc.Text = conn.GetFieldValue("DESCRIPT").ToString();
			_txtEditedQuery.Text = conn.GetFieldValue("QUERYTXT").ToString();
			_txtEditedColumn.Text = conn.GetFieldValue("COLUMNNAME").ToString();
			_rdoEditedStatus.SelectedValue = conn.GetFieldValue("ISACTIVE").ToString();
			_txtEditedParameter.Text = conn.GetFieldValue("PARAMNAME").ToString();
			_rdoEditedType.SelectedValue = conn.GetFieldValue("ISRANGE").ToString();
			_txtIDStatus.Text = conn.GetFieldValue("STATUS").ToString();
		}

		protected void _btnUpdateRule_Click(object sender, EventArgs e)
		{
			/*conn.QueryString = "UPDATE RFSCORINGATTRIBUTE SET DESCRIPT = '" 
				+ _txtEditedDesc.Text.ToString() 
				+ "', ISACTIVE = '" 
				+ _rdoEditedStatus.SelectedValue 
				+ "' WHERE ID = '" + _txtEditedID.Text.ToString() + "'";
			conn.ExecuteNonQuery();*/

			conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTRIUBUTETEMP '" + 
				_txtEditedDesc.Text.ToString() + "','" +
				_txtEditedColumn.Text.ToString() + "'," +
				_rdoEditedType.SelectedValue.ToString() + "," +
				_rdoEditedStatus.SelectedValue.ToString() + ",'" +
				_txtEditedQuery.Text.ToString() + "','" +
				_txtEditedParameter.Text.ToString() + "','" +
				"2','" + 
				_txtEditedID.Text.ToString() + "'";
			conn.ExecuteQuery();

			Tools.popMessage(this,"Requesting Approval...");

			conn.QueryString = "EXEC SCORING_BINDATTRIBUTE";
			conn.ExecuteQuery();

			BindData();
			BindData("DatGridAttributeReq","EXEC SCORING_BINDATTRIBUTETEMP");
			//ClearData();
		}

		protected void _btnFind_Click(object sender, EventArgs e)
		{

			if(_TxtIDParam.Text.ToString() == "" && _TxtRuleName.Text.ToString() == "")
			{
				conn.QueryString = "EXEC SCORING_BINDATTRIBUTE ";
			}
			else if(_TxtIDParam.Text.ToString() == "" && _TxtRuleName.Text.ToString() != "")
			{
				conn.QueryString = "EXEC SCORING_BINDATTRIBUTE NULL,'" + _TxtRuleName.Text.ToString() + "'";
			}
			else if(_TxtIDParam.Text.ToString() != "" && _TxtRuleName.Text.ToString() == "")
			{
				conn.QueryString = "EXEC SCORING_BINDATTRIBUTE '" + _TxtIDParam.Text.ToString() + "'";
			}
			else if(_TxtIDParam.Text.ToString() != "" && _TxtRuleName.Text.ToString() != "")
			{
				conn.QueryString = "EXEC SCORING_BINDATTRIBUTE '" + _TxtIDParam.Text.ToString() + "','" + _TxtRuleName.Text.ToString() + "'";
			}
			conn.ExecuteQuery();
			BindData();
		}

		private void BindDataAtt(string range)
		{
			if(range == "RANGE")
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				DatGridAttributeRange.DataSource = dt;				
				DatGridAttributeRange.DataBind();
			}
			else if(range == "NONRANGE")
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				DatGridAttributeNonRange.DataSource = dt;				
				DatGridAttributeNonRange.DataBind();
			}
		}

		protected void _btnViewDetail_Click(object sender, EventArgs e)
		{
			conn.QueryString = "SELECT ID,ISRANGE FROM RFSCORINGATTRIBUTE WHERE ID = '" + _txtEditedID.Text.ToString() + "'";
			conn.ExecuteQuery();

			string id = conn.GetFieldValue("ID").ToString();

			if(conn.GetFieldValue("ISRANGE").ToString() == "0")
			{
				TR_ATTRIBUTE_RANGE.Visible = false;
				TR_ATTRIBUTE_NONRANGE.Visible = true;
				TR_NEW_ATTRIBUTE_NONRANGE.Visible = true;
				TR_EDIT_ATTRIBUTE_NONRANGE.Visible = false;
				TR_EDIT_ATTRIBUTE_RANGE.Visible = false;

				conn.QueryString = "EXEC SCORING_BINDNONRANGEATT '" + id + "'";
				conn.ExecuteQuery();
				BindDataAtt("NONRANGE");
			}
			else if(conn.GetFieldValue("ISRANGE").ToString() == "1")
			{
				TR_ATTRIBUTE_NONRANGE.Visible = false;
				TR_ATTRIBUTE_RANGE.Visible = true;
				TR_NEW_ATTRIBUTE_RANGE.Visible = true;
				TR_EDIT_ATTRIBUTE_NONRANGE.Visible = false;
				TR_EDIT_ATTRIBUTE_RANGE.Visible = false;

				conn.QueryString = "EXEC SCORING_BINDRANGEATT '" + id + "'";
				conn.ExecuteQuery();
				BindDataAtt("RANGE");
			}
		}

		private void cekEditedRange(string id)
		{
			conn.QueryString = "SELECT * FROM RFSCORINGWEIGHTRANGEATTRIBUTE WHERE ID = '" + id + "'";		//do not auto load anything.. 
			conn.ExecuteQuery();

			_txtEditedAttributeRangeID.Text = conn.GetFieldValue("ID").ToString();
			_txtEditedAttributeRangeHighest.Text = conn.GetFieldValue("HIGHESTSCORE").ToString();
			_txtEditedAttributeRangeLowest.Text = conn.GetFieldValue("LOWESTSCORE").ToString();
			_txtEditedAttributeRangeWeight.Text = conn.GetFieldValue("WEIGHT").ToString();

			if(conn.GetFieldValue("HIGHESTSCORE").ToString().Replace(" ","").ToString() == "HIGH")
			{
				_rdEditedStatus.SelectedValue = "2";
			}
			else if(conn.GetFieldValue("LOWESTSCORE").ToString().Replace(" ","").ToString() == "BELOW")
			{
				_rdEditedStatus.SelectedValue = "3";
			}
			else if(conn.GetFieldValue("LOWESTSCORE").ToString().Replace(" ","").ToString() == "NOINFORMATION" &&
				conn.GetFieldValue("HIGHESTSCORE").ToString().Replace(" ","").ToString() == "NOINFORMATION")
			{
				_rdEditedStatus.SelectedValue = "1";
			}
			else
			{
				_rdEditedStatus.SelectedValue = "0";
			}
		}

		private void cekEditedNonRange(string id)
		{
			conn.QueryString = "SELECT * FROM RFSCORINGWEIGHTNONRANGEATTRIBUTE WHERE ID = '" + id + "'";		//do not auto load anything.. 
			conn.ExecuteQuery();

			_txtEditedAttributeNonRangeID.Text = conn.GetFieldValue("ID").ToString();
			_txtEditedAttributeNonRangeDesc.Text = conn.GetFieldValue("QUERYTXT").ToString();
			_txtEditedAttributeNonRangeValue.Text = conn.GetFieldValue("VALUE").ToString();
			_txtEditedAttributeNonRangeWeight.Text = conn.GetFieldValue("WEIGHT").ToString();
		}

		private void DatGridAttributeRange_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TR_EDIT_ATTRIBUTE_RANGE.Visible = true;
					TR_NEW_ATTRIBUTE_RANGE.Visible = false;
					cekEditedRange(e.Item.Cells[0].Text.ToString());
					break;
				case "Delete":
					conn.QueryString = "DELETE RFSCORINGWEIGHTRANGEATTRIBUTE WHERE ID ='" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteNonQuery();
					conn.QueryString = "EXEC SCORING_BINDRANGEATT '" + _txtEditedID.Text + "'";
					conn.ExecuteQuery();
					BindDataAtt("RANGE");
					break;
			}
		}

		private void DatGridAttributeNonRange_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TR_EDIT_ATTRIBUTE_NONRANGE.Visible = true;
					TR_NEW_ATTRIBUTE_NONRANGE.Visible = false;
					cekEditedNonRange(e.Item.Cells[0].Text.ToString());
					break;
				case "Delete":
					conn.QueryString = "DELETE RFSCORINGWEIGHTNONRANGEATTRIBUTE WHERE ID ='" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteNonQuery();
					conn.QueryString = "EXEC SCORING_BINDNONRANGEATT '" + _txtEditedID.Text + "'";
					conn.ExecuteQuery();
					BindDataAtt("NONRANGE");
					break;
			}
		}

		protected void _btnEditedAttributeRangeWeight_Click(object sender, System.EventArgs e)
		{
			bool okay = false;

			if(_rdEditedStatus.SelectedValue == "2")
			{
				conn.QueryString = "SELECT COUNT(ID) as TOTAL FROM RFSCORINGWEIGHTRANGETEMP WHERE [IDATTRIBUTE] = '" + _txtIDAttRange.Text + "' AND HIGHESTSCORE = 'HIGH'";
				conn.ExecuteQuery();

				if(conn.GetFieldValue("TOTAL") == "0")
				{
					okay = true;
				}
				else
				{
					okay = false;
					Tools.popMessage(this, "Highestscore HIGH sudah ada !");
				}
			}
			else if(_rdEditedStatus.SelectedValue == "1")
			{
				conn.QueryString = "SELECT COUNT(ID) as TOTAL FROM RFSCORINGWEIGHTRANGETEMP WHERE [IDATTRIBUTE] = '" + _txtIDAttRange.Text + "' AND HIGHESTSCORE = 'NO INFORMATION' AND LOWESTSCORE = 'NO INFORMATION'";
				conn.ExecuteQuery();

				if(conn.GetFieldValue("TOTAL") == "0")
				{
					okay = true;
				}
				else
				{
					okay = false;
					Tools.popMessage(this, "No Information sudah ada !");
				}
			}
			else if(_rdEditedStatus.SelectedValue == "3")
			{
				conn.QueryString = "SELECT COUNT(ID) as TOTAL FROM RFSCORINGWEIGHTRANGETEMP WHERE [IDATTRIBUTE] = '" + _txtIDAttRange.Text + "' AND LOWESTSCORE = 'BELOW'";
				conn.ExecuteQuery();

				if(conn.GetFieldValue("TOTAL") == "0")
				{
					okay = true;
				}
				else
				{
					okay = false;
					Tools.popMessage(this, "Lowestscore BELOW sudah ada !");
				}
			}
			else if(_rdEditedStatus.SelectedValue == "0")
			{
				okay = true;
			}

			if(okay == true)
			{

				if(_rdEditedStatus.SelectedValue.ToString() == "0")
				{
					//sampai sini
					conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTRANGETEMP '" 
						+ _txtEditedID.Text.ToString() + "','"
						+ _txtEditedAttributeRangeLowest.Text.ToString() + "','"
						+ _txtEditedAttributeRangeHighest.Text.ToString() + "','"
						+ _txtEditedAttributeRangeWeight.Text.ToString() + "','"
						+ "UPDATE" + "','" 
						+ _txtEditedAttributeRangeID.Text.ToString() + "'";
					conn.ExecuteQuery();
				}
				else if(_rdEditedStatus.SelectedValue.ToString() == "1")
				{
					conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTRANGETEMP '" 
						+ _txtEditedID.Text.ToString() + "','"
						+ "NO INFORMATION" + "','"
						+ "NO INFORMATION" + "','"
						+ _txtEditedAttributeRangeWeight.Text.ToString() + "','"
						+ "UPDATE" + "','" 
						+ _txtEditedAttributeRangeID.Text.ToString() + "'";
					conn.ExecuteQuery();
				}
				else if(_rdEditedStatus.SelectedValue.ToString() == "2")
				{
					conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTRANGETEMP '" 
						+ _txtEditedID.Text.ToString() + "','"
						+ _txtEditedAttributeRangeLowest.Text.ToString() + "','"
						+ "HIGH" + "','"
						+ _txtEditedAttributeRangeWeight.Text.ToString() + "','"
						+ "UPDATE" + "','" 
						+ _txtEditedAttributeRangeID.Text.ToString() + "'";
					conn.ExecuteQuery();
				}
				else if(_rdEditedStatus.SelectedValue.ToString() == "3")
				{
					conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTRANGETEMP '" 
						+ _txtEditedID.Text.ToString() + "','"
						+ "BELOW" + "','"
						+ _txtEditedAttributeRangeHighest.Text.ToString() + "','"
						+ _txtEditedAttributeRangeWeight.Text.ToString() + "','"
						+ "UPDATE" + "','" 
						+ _txtEditedAttributeRangeID.Text.ToString() + "'";
					conn.ExecuteQuery();
				}

				conn.QueryString = "EXEC SCORING_BINDRANGEATT '" + _txtEditedID.Text.ToString() + "'";
				conn.ExecuteQuery();
				BindDataAtt("RANGE");
				//ClearData();
				BindData("DatGridAttRangeReq","EXEC SCORING_BINDRANGEATTTEMP");

				Tools.popMessage(this, "Request for approval...");
			}
			BindData("DatGridAttRangeReq","EXEC SCORING_BINDRANGEATTTEMP");
		}

		protected void _btnEditedAttributeNonRange_Click(object sender, System.EventArgs e)
		{
			/*conn.QueryString = "UPDATE RFSCORINGWEIGHTNONRANGEATTRIBUTE SET VALUE = '" 
				+ _txtEditedAttributeNonRangeValue.Text.ToString() 
				+ "', WEIGHT = '" 
				+ _txtEditedAttributeNonRangeWeight.Text.ToString()
				+ "', QUERYTXT = '" + _txtEditedAttributeNonRangeDesc.Text.ToString()
				+ "' WHERE ID = '" + _txtEditedAttributeNonRangeID.Text.ToString() + "'";
			conn.ExecuteNonQuery();

			conn.QueryString = "EXEC SCORING_BINDNONRANGEATT '" + _txtEditedID.Text + "'";
			conn.ExecuteQuery();
			BindDataAtt("NONRANGE");*/

			conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTNONRANGETEMP '" 
				+ _txtIDAttribute.Text.ToString() + "','"
				+ _txtEditedAttributeNonRangeValue.Text.ToString() + "','"
				+ _txtEditedAttributeNonRangeWeight.Text.ToString() + "','"
				+ _txtEditedAttributeNonRangeDesc.Text.ToString() + "','UPDATE','" + 
				_txtEditedAttributeNonRangeID.Text.ToString() + "'";
			conn.ExecuteQuery();

			BindData("DatGridAttNonRangeReq","EXEC SCORING_BINDNONRANGEATTTEMP");
			//ClearData();

			Tools.popMessage(this, "Requesting approval...");
		}

		protected void _btnNew_Click(object sender, System.EventArgs e)
		{
			TR_EDIT_PARAMETER.Visible = false;
			TR_NEW_PARAMETER.Visible = true;
			TR_ATTRIBUTE_RANGE.Visible = false;
			TR_ATTRIBUTE_NONRANGE.Visible = false;
		}

		protected void _btnInsertRule_Click(object sender, System.EventArgs e)
		{
			/*conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTRIUBUTE '" + _txtNewDesc.Text.ToString() + "','" +
				_txtNewColumn.Text.ToString() + "'," +
				_rdoEditedType.SelectedValue.ToString() + "," +
				_rdoEditedStatus.SelectedValue.ToString() + ",'" +
				_txtNewQuery.Text.ToString() + "','" +
				_txtNewParameter.Text.ToString() + "'";
			conn.ExecuteQuery();

			conn.QueryString = "EXEC SCORING_BINDATTRIBUTE";
			conn.ExecuteQuery();*/

			conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTRIUBUTETEMP '" + 
				_txtNewDesc.Text.ToString() + "','" +
				_txtNewColumn.Text.ToString() + "'," +
				_rdoNewType.SelectedValue.ToString() + "," +
				_rdoNewStatus.SelectedValue.ToString() + ",'" +
				_txtNewQuery.Text.ToString() + "','" +
				_txtNewParameter.Text.ToString() + "','" +
				"1','-1'";
			conn.ExecuteQuery();

			Tools.popMessage(this,"Requesting Approval...");

			conn.QueryString = "EXEC SCORING_BINDATTRIBUTE";
			conn.ExecuteQuery();

			BindData();
			BindData("DatGridAttributeReq","EXEC SCORING_BINDATTRIBUTETEMP");
		}

		protected void _btnUpdateAttributeRange_Click(object sender, System.EventArgs e)
		{
			bool okay = false;

			if(_rdNewStatus.SelectedValue == "2")
			{
				conn.QueryString = "SELECT COUNT(ID) as TOTAL FROM RFSCORINGWEIGHTRANGETEMP WHERE [IDATTRIBUTE] = '" + _txtIDAttribute.Text + "' AND HIGHESTSCORE = 'HIGH'";
				conn.ExecuteQuery();

				if(conn.GetFieldValue("TOTAL") == "0")
				{
					okay = true;
				}
				else
				{
					okay = false;
					Tools.popMessage(this, "Highestscore HIGH sudah ada !");
				}
			}
			else if(_rdNewStatus.SelectedValue == "1")
			{
				conn.QueryString = "SELECT COUNT(ID) as TOTAL FROM RFSCORINGWEIGHTRANGETEMP WHERE [IDATTRIBUTE] = '" + _txtIDAttribute.Text + "' AND HIGHESTSCORE = 'NO INFORMATION' AND LOWESTSCORE = 'NO INFORMATION'";
				conn.ExecuteQuery();

				if(conn.GetFieldValue("TOTAL") == "0")
				{
					okay = true;
				}
				else
				{
					okay = false;
					Tools.popMessage(this, "No Information sudah ada !");
				}
			}
			else if(_rdNewStatus.SelectedValue == "3")
			{
				conn.QueryString = "SELECT COUNT(ID) as TOTAL FROM RFSCORINGWEIGHTRANGETEMP WHERE [IDATTRIBUTE] = '" + _txtIDAttribute.Text + "' AND LOWESTSCORE = 'BELOW'";
				conn.ExecuteQuery();

				if(conn.GetFieldValue("TOTAL") == "0")
				{
					okay = true;
				}
				else
				{
					okay = false;
					Tools.popMessage(this, "Lowestscore BELOW sudah ada !");
				}
			}
			else if(_rdNewStatus.SelectedValue == "0")
			{
				okay = true;
			}

			if(okay == true)
			{
				if(_rdNewStatus.SelectedValue.ToString() == "0")
				{
					//sampai sini
					conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTRANGETEMP '" 
						+ _txtEditedID.Text.ToString() + "','"
						+ _txtNewRangeLowest.Text.ToString() + "','"
						+ _txtNewRangeHighest.Text.ToString() + "','"
						+ _txtNewRangeWeight.Text.ToString() + "','"
						+ "INSERT" + "','-1'";
					conn.ExecuteQuery();
				}
				else if(_rdNewStatus.SelectedValue.ToString() == "1")
				{
					conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTRANGETEMP '" 
						+ _txtEditedID.Text.ToString() + "','"
						+ "NO INFORMATION" + "','"
						+ "NO INFORMATION" + "','"
						+ _txtNewRangeWeight.Text.ToString() + "','"
						+ "INSERT" + "','-1'";
					conn.ExecuteQuery();
				}
				else if(_rdNewStatus.SelectedValue.ToString() == "2")
				{
					conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTRANGETEMP '" 
						+ _txtEditedID.Text.ToString() + "','"
						+ _txtNewRangeLowest.Text.ToString() + "','"
						+ "HIGH" + "','"
						+ _txtNewRangeWeight.Text.ToString() + "','"
						+ "INSERT" + "','-1'";
					conn.ExecuteQuery();
				}
				else if(_rdNewStatus.SelectedValue.ToString() == "3")
				{
					conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTRANGETEMP '" 
						+ _txtEditedID.Text.ToString() + "','"
						+ "BELOW" + "','"
						+ _txtNewRangeHighest.Text.ToString() + "','"
						+ _txtNewRangeWeight.Text.ToString() + "','"
						+ "INSERT" + "','-1'";
					conn.ExecuteQuery();
				}

				conn.QueryString = "EXEC SCORING_BINDRANGEATT '" + _txtEditedID.Text.ToString() + "'";
				conn.ExecuteQuery();
				BindDataAtt("RANGE");
				//ClearData();
				BindData("DatGridAttRangeReq","EXEC SCORING_BINDRANGEATTTEMP");

				Tools.popMessage(this, "Request for approval...");
			}
			BindData("DatGridAttRangeReq","EXEC SCORING_BINDRANGEATTTEMP");
		}

		protected void _btnNewAttributeNonRange_Click(object sender, System.EventArgs e)
		{
			/*conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTNONRANGE '" 
				+ _txtEditedID.Text.ToString() + "','"
				+ _txtNewAttributeNonRangeDesc.Text.ToString() + "','"
				+ _txtNewAttributeNonRangeValue.Text.ToString() + "','"
				+ _txtNewAttributeNonRangeWeight.Text.ToString() + "'";
			conn.ExecuteQuery();

			conn.QueryString = "EXEC SCORING_BINDNONRANGEATT '" + _txtEditedID.Text + "'";
			conn.ExecuteQuery();*/

			conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTNONRANGETEMP '" 
				+ _txtIDAttribute.Text.ToString() + "','"
				+ _txtNewAttributeNonRangeValue.Text.ToString() + "','"
				+ _txtNewAttributeNonRangeWeight.Text.ToString() + "','"
				+ _txtNewAttributeNonRangeDesc.Text.ToString() + "','INSERT','-1'";
			conn.ExecuteQuery();

			Tools.popMessage(this, "Requesting approval...");

			BindDataAtt("NONRANGE");
			BindData("DatGridAttNonRangeReq","EXEC SCORING_BINDNONRANGEATTTEMP");
		}

		/*conn.QueryString = "DELETE RFSCORINGWEIGHTRANGEATTRIBUTE WHERE ID ='" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteNonQuery();
					conn.QueryString = "EXEC SCORING_BINDRANGEATT '" + _txtEditedID.Text + "'";
					conn.ExecuteQuery();*/

		private void DatGridAttributeRange_ItemCommand_1(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Edits":
					conn.QueryString = "SELECT RFSCORINGWEIGHTRANGEATTRIBUTE.[IDATTRIBUTE], RFSCORINGWEIGHTRANGEATTRIBUTE.[LOWESTSCORE], RFSCORINGWEIGHTRANGEATTRIBUTE.[HIGHESTSCORE], RFSCORINGWEIGHTRANGEATTRIBUTE.[WEIGHT]," +
						"RFSCORINGATTRIBUTE.DESCRIPT " +
						"FROM RFSCORINGATTRIBUTE, RFSCORINGWEIGHTRANGEATTRIBUTE " +
						"WHERE RFSCORINGATTRIBUTE.[ID] = RFSCORINGWEIGHTRANGEATTRIBUTE.[IDATTRIBUTE] " +
						"AND RFSCORINGWEIGHTRANGEATTRIBUTE.[ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();

					conn.QueryString = "EXEC SCORING_INSERTRFSCORINGATTRANGETEMP '" 
						+ conn.GetFieldValue("IDATTRIBUTE") + "','"
						+ conn.GetFieldValue("LOWESTSCORE") + "','"
						+ conn.GetFieldValue("HIGHESTSCORE") + "','"
						+ conn.GetFieldValue("WEIGHT") + "','"
						+ "DELETE" + "','" 
						+ e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					Tools.popMessage(this, "Requesting approval...");
					BindDataAtt("RANGE");
					BindData("DatGridAttRangeReq","EXEC SCORING_BINDRANGEATTTEMP");
					break;
				case "Edit":
					TR_EDIT_ATTRIBUTE_RANGE.Visible = true;
					TR_NEW_ATTRIBUTE_RANGE.Visible = false;
					_txtIDAttRange.Text = e.Item.Cells[0].Text.ToString();
					cekEditedRange(e.Item.Cells[0].Text.ToString());
					break;
			}
		}

		private void DatGridAttributeNonRange_ItemCommand_1(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TR_EDIT_ATTRIBUTE_NONRANGE.Visible = true;
					TR_NEW_ATTRIBUTE_NONRANGE.Visible = false;
					_txtIDAttNonRange.Text = e.Item.Cells[0].Text.ToString();
					cekEditedNonRange(e.Item.Cells[0].Text.ToString());
					break;
				case "Delete":
					conn.QueryString = "DELETE RFSCORINGWEIGHTNONRANGEATTRIBUTE WHERE ID ='" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteNonQuery();
					conn.QueryString = "EXEC SCORING_BINDNONRANGEATT '" + _txtEditedID.Text + "'";
					conn.ExecuteQuery();
					BindDataAtt("NONRANGE");
					break;
			}
		}

		private void DatGrd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGrd.CurrentPageIndex >= 0 && DatGrd.CurrentPageIndex < DatGrd.PageCount)
			{
				DatGrd.CurrentPageIndex = e.NewPageIndex;
				conn.QueryString = "EXEC SCORING_BINDATTRIBUTE";		//do not auto load anything.. 
				conn.ExecuteQuery();
				BindData();
			}
		}

		private void DatGridAttributeRange_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGridAttributeRange.CurrentPageIndex >= 0 && DatGridAttributeRange.CurrentPageIndex < DatGridAttributeRange.PageCount)
			{
				DatGridAttributeRange.CurrentPageIndex = e.NewPageIndex;
				BindDataAtt("RANGE");
			}
		}

		private void DatGridAttributeNonRange_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGridAttributeNonRange.CurrentPageIndex >= 0 && DatGridAttributeNonRange.CurrentPageIndex < DatGridAttributeNonRange.PageCount)
			{
				DatGridAttributeNonRange.CurrentPageIndex = e.NewPageIndex;
				BindDataAtt("NONRANGE");
			}
		}

		/************************mulai dari sini ini buat maker checker**********************/
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

		/***********************************reset field***************************************/
		/*private void ResetTR_ATTRIBUTE_TEMP()
		{
			_txtIDAttTemp.Text = "";
			_txtDescAttTemp.Text = "";
			_txtQueryAttTemp.Text = "";
			_txtParameterAttTemp.Text = "";
			_txtColumnAttTemp.Text = "";
			_rdoTypeAttTemp.SelectedValue = "0";
			_rdoStatusAttTemp.SelectedValue = "0";

			TR_ATTRIBUTE_TEMP.Visible = false;
		}*/

	}
}
