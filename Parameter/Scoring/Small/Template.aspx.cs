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
using System.Data.SqlClient;

namespace CuBES_Maintenance.Parameter.Scoring.Small
{
	/// <summary>
	/// Summary description for Template.
	/// </summary>
	public partial class Template : System.Web.UI.Page
	{
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(ConfigurationSettings.AppSettings["connME"]);

			_btnFindTemplate.Click += new EventHandler(_btnFindTemplate_Click);
			_btnNewTemplate.Click += new EventHandler(_btnNewTemplate_Click);

			if(!IsPostBack)
			{
				TR_NEW_TEMPLATE.Visible = false;
				TR_EDIT_TEMPLATE.Visible = false;
				TR_GRID_ATTRIBUTE.Visible = false;
				TR_ADD_ATTRIBUTE.Visible = false;
				TR_GRID_RULEREASON.Visible = false;
				TR_ADD_RULE_REASON.Visible = false;
				TR_EDIT_CUTOFF_SCORE.Visible = false;
				TR_CUTOFF.Visible = false;
				TR_DDL_ITEMPERTEMPLATE.Visible = false;

				fillDdlOperator();
			}

			BindData("DatGrd","EXEC SCORING_BINDTEMPLATE");

			TR_EDIT_RULE_REASON.Visible = false;
			TR_ATTRIBUTE_NONRANGE.Visible = false;
			TR_ATTRIBUTE_RANGE.Visible = false;
			TR_TEMPLATE_TEMP.Visible = false;

			BindData("DataGridTemplateTemp","EXEC SCORING_BINDTEMP 'TEMPLATE'");
		}

		private void fillDdlOperator()
		{
			_ddlOperatorItem.Items.Clear();

			_ddlOperatorItem.Items.Add(new ListItem("=","="));
			_ddlOperatorItem.Items.Add(new ListItem("<","<"));
			_ddlOperatorItem.Items.Add(new ListItem(">",">"));
			_ddlOperatorItem.Items.Add(new ListItem(">=",">="));
			_ddlOperatorItem.Items.Add(new ListItem("<=","<="));
			_ddlOperatorItem.Items.Add(new ListItem("none",""));
		}

//DDL_ITEMPERTEMPLATE
		private void fillDDLAttribute(string idTemplate)
		{
			DDL_ATTRIBUTE.Items.Clear();

			//yang bisa diselect cuman yang ada atributnya
			conn.QueryString =	"SELECT * FROM RFSCORINGATTRIBUTE where [ID] not in " +
			"(SELECT DISTINCT(RFSCORINGWEIGHTNONRANGEATTRIBUTE.IDATTRIBUTE) as [ID] " +
			"FROM RFSCORINGWEIGHTNONRANGEATTRIBUTE, RFSCORINGTEMPATT " +
			"WHERE RFSCORINGWEIGHTNONRANGEATTRIBUTE.IDATTRIBUTE = RFSCORINGTEMPATT.IDATTRIBUTE " +
			"AND RFSCORINGTEMPATT.IDTEMPLATE = '" + idTemplate + "'" + 
			"union " +
			"SELECT DISTINCT(RFSCORINGWEIGHTRANGEATTRIBUTE.IDATTRIBUTE) as [ID] " +
			"FROM RFSCORINGWEIGHTRANGEATTRIBUTE, RFSCORINGTEMPATT " +
			"WHERE RFSCORINGWEIGHTRANGEATTRIBUTE.IDATTRIBUTE = RFSCORINGTEMPATT.IDATTRIBUTE " +
			"AND RFSCORINGTEMPATT.IDTEMPLATE = '" + idTemplate + "') " + 
			"AND [ID] in ( " +
			"SELECT RFSCORINGATTRIBUTE.[ID] FROM RFSCORINGWEIGHTNONRANGEATTRIBUTE,RFSCORINGATTRIBUTE " +
			"WHERE RFSCORINGATTRIBUTE.[ID] = RFSCORINGWEIGHTNONRANGEATTRIBUTE.[IDATTRIBUTE] union " +
			"SELECT RFSCORINGATTRIBUTE.[ID] FROM RFSCORINGWEIGHTRANGEATTRIBUTE, RFSCORINGATTRIBUTE " +
			"WHERE RFSCORINGATTRIBUTE.[ID] = RFSCORINGWEIGHTRANGEATTRIBUTE.[IDATTRIBUTE]) " +
			"and STATUS = '1'";
			conn.ExecuteQuery();

			for(int i =0; i<conn.GetRowCount(); i++)
			{
				DDL_ATTRIBUTE.Items.Add(new ListItem(conn.GetFieldValue(i,"DESCRIPT"), conn.GetFieldValue(i, "ID")));
			}
			
			conn.QueryString = "SELECT [DESC] FROM RFSCORINGTEMPLATE WHERE [ID] = '" + idTemplate + "'";
			conn.ExecuteQuery();

			_txtAddAttributeTemplate.Text = conn.GetFieldValue("DESC");
		}

		private void fillDDLRuleReason(string idTemplate)
		{
			DDL_RULE_REASON.Items.Clear();

			conn.QueryString = "SELECT RFSCORINGRULEREASON.[ID], ('(' + REASON_CODE + ')' + ' - ' + [DESCRIPTION]) as [DESCRIPTION] " +
								"FROM RFSCORINGRULEREASON " +
								"WHERE ISACTIVE = '1' AND RFSCORINGRULEREASON.[ID] not in " +
								"(SELECT RFSCORINGRULEREASONPERTEMPLATE.IDRFSCORINGRULEREASON as [ID] " +
								"FROM RFSCORINGRULEREASONPERTEMPLATE " +
								"WHERE IDTEMPLATE = '" + idTemplate + "')";
			conn.ExecuteQuery();

			for(int i =0; i<conn.GetRowCount(); i++)
			{
				DDL_RULE_REASON.Items.Add(new ListItem(conn.GetFieldValue(i,"DESCRIPTION"), conn.GetFieldValue(i, "ID")));
			}

			conn.QueryString = "SELECT [DESC] FROM RFSCORINGTEMPLATE WHERE [ID] = '" + idTemplate + "'";
			conn.ExecuteQuery();

			TXT_TEMPLATE_R.Text = conn.GetFieldValue("DESC");
		}

		private void fillNonRangeDDL()
		{
			DDL_EDITATTNONRANGE.Items.Clear();

			conn.QueryString =	"SELECT RFSCORING_NONRANGEATTPERTEMPLATE.[ID], " + 
				"RFSCORING_NONRANGEATTPERTEMPLATE.WEIGHT,  RFSCORINGATTRIBUTE.DESCRIPT, " +
				"RFSCORINGWEIGHTNONRANGEATTRIBUTE.QUERYTXT " +
				"FROM RFSCORINGWEIGHTNONRANGEATTRIBUTE, RFSCORING_NONRANGEATTPERTEMPLATE, " +
				"RFSCORINGATTRIBUTE " + 
				"WHERE RFSCORINGATTRIBUTE.[ID] = RFSCORINGWEIGHTNONRANGEATTRIBUTE.[IDATTRIBUTE] " +
				"AND RFSCORING_NONRANGEATTPERTEMPLATE.IDATTRIBUTENONRANGE = RFSCORINGWEIGHTNONRANGEATTRIBUTE.[ID] " +
				"AND RFSCORING_NONRANGEATTPERTEMPLATE.IDTEMPLATE = '" + TXT_ID_TEMPLATE.Text.ToString() + "'" +
				"AND RFSCORING_NONRANGEATTPERTEMPLATE.IDATTRIBUTENONRANGE in " + 
				"(SELECT [ID] FROM RFSCORINGWEIGHTNONRANGEATTRIBUTE " +
				"WHERE IDATTRIBUTE = '" + idAttributeNonRange.Text.ToString() + "')";
			conn.ExecuteQuery();

			for(int i = 0; i <conn.GetRowCount(); i++)
			{
				if(i == 0)
				{
					_txtEditedAttributeNonRangeID.Text = conn.GetFieldValue(i,"DESCRIPT");
					_txtNewAttributeNonRangeWeight.Text = conn.GetFieldValue(i,"WEIGHT");
				}

				DDL_EDITATTNONRANGE.Items.Add(new ListItem(conn.GetFieldValue(i,"QUERYTXT"), conn.GetFieldValue(i,"ID")));
			}
		}

		private void fillRangeDDL()
		{
			DDL_EDITATTRANGE.Items.Clear();

			conn.QueryString = "SELECT RFSCORING_RANGEATTPERTEMPLATE.[ID], RFSCORINGATTRIBUTE.[DESCRIPT], " +
				"RFSCORING_RANGEATTPERTEMPLATE.[LOWESTSCORE], RFSCORING_RANGEATTPERTEMPLATE.[HIGHESTSCORE], " +
				"RFSCORING_RANGEATTPERTEMPLATE.[WEIGHT] " +
				"FROM RFSCORINGTEMPLATE, RFSCORING_RANGEATTPERTEMPLATE, " +
				"RFSCORINGTEMPATT, RFSCORINGATTRIBUTE " +
				"WHERE RFSCORINGTEMPLATE.[ID] = '" + TXT_ID_TEMPLATE.Text.ToString() + "' AND RFSCORINGTEMPLATE.[ID] = RFSCORINGTEMPATT.[IDTEMPLATE] " +
				"AND RFSCORINGTEMPATT.[IDATTRIBUTE] = '" + idAttributeRange.Text.ToString() + "' " +
				"AND RFSCORING_RANGEATTPERTEMPLATE.[IDATTRIBUTERANGE] = RFSCORINGTEMPATT.[IDATTRIBUTE] " +
				"AND RFSCORING_RANGEATTPERTEMPLATE.[IDATTRIBUTERANGE] = RFSCORINGATTRIBUTE.[ID] " +
				"AND RFSCORING_RANGEATTPERTEMPLATE.[IDTEMPLATE] = RFSCORINGTEMPATT.[IDTEMPLATE]";
			conn.ExecuteQuery();

			for(int i = 0; i <conn.GetRowCount(); i++)
			{
				if(i == 0)
				{
					TXT_EditedAttributeRangeID.Text = conn.GetFieldValue(i,"DESCRIPT");
					TXT_EditedAttributeRangeWeight.Text = conn.GetFieldValue(i,"WEIGHT");
					TXT_EditedAttributeRangeLowest.Text = conn.GetFieldValue(i,"LOWESTSCORE");
					TXT_EditedAttributeRangeHighest.Text = conn.GetFieldValue(i,"HIGHESTSCORE");

					if(conn.GetFieldValue(i,"LOWESTSCORE").ToString().ToUpper().Trim() == "BELOW")
					{
						RDO_EditedStatus.SelectedValue = "3";
					}
					else if(conn.GetFieldValue(i,"HIGHESTSCORE").ToString().ToUpper().Trim() == "HIGH")
					{
						RDO_EditedStatus.SelectedValue = "2";
					}
					else if(conn.GetFieldValue(i,"HIGHESTSCORE").ToString().ToUpper().Trim() == "NOINFORMATION")
					{
						RDO_EditedStatus.SelectedValue = "1";
					}
					else
					{
						RDO_EditedStatus.SelectedValue = "0";
					}
				}

				DDL_EDITATTRANGE.Items.Add(new ListItem(conn.GetFieldValue(i,"LOWESTSCORE") + "-" + conn.GetFieldValue(i,"HIGHESTSCORE") , conn.GetFieldValue(i,"ID")));
			}
		}

		private void fillTR_DDLITEM()
		{
			DDL_ITEMPERTEMPLATE.Items.Clear();

			conn.QueryString = "SELECT RFSCORINGCUTOFFPERITEM.[ID], RFSCORINGCUTOFFPERITEM.[DESCRIPTION] " + 
				"FROM RFSCORINGCUTOFFITEMPERTEMPLATE, RFSCORINGCUTOFFPERITEM " +
				"WHERE RFSCORINGCUTOFFITEMPERTEMPLATE.[IDTEMPLATE] = '" + TXT_ID_TEMPLATE.Text + "'" +
				"AND RFSCORINGCUTOFFPERITEM.[ID] = RFSCORINGCUTOFFITEMPERTEMPLATE.[IDITEM]";
			conn.ExecuteQuery();

			for(int i=0; i<conn.GetRowCount(); i++)
			{
				DDL_ITEMPERTEMPLATE.Items.Add(new ListItem(conn.GetFieldValue(i, "DESCRIPTION"),conn.GetFieldValue(i, "ID"))); 
			}
		}

		private void fillTR_EDIT_CUTOFF_SCORE()
		{
			TR_DDL_ITEMPERTEMPLATE.Visible = true;
			
			conn.QueryString = "SELECT [CONDITION],[COLUMNNAME],[PARAMETER],[OPERATOR],[RESULT] FROM RFSCORINGCUTOFFITEMPERTEMPLATE WHERE  [IDTEMPLATE] = '" + TXT_ID_TEMPLATE.Text + "' AND " +
				"[IDITEM] = '" + DDL_ITEMPERTEMPLATE.SelectedValue.ToString() + "'";
			conn.ExecuteQuery();
			_txtCondition.Text = conn.GetFieldValue("CONDITION");
			_txtColumnName.Text = conn.GetFieldValue("COLUMNNAME");
			_txtParameter.Text = conn.GetFieldValue("PARAMETER");
			_txtResult.Text = conn.GetFieldValue("RESULT");
			_ddlOperatorItem.SelectedValue = conn.GetFieldValue("OPERATOR");
			BTN_EDIT_CUTOFF.Text = "Add Parameter";
			fillTR_DDLITEM();
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
			this.DatGrdAttribute.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGrdComposition_ItemCommand);
			this.DatGrdAttribute.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGrdAttribute_PageIndexChanged);
			this.DatGridRuleReason.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGridRuleReason_ItemCommand);
			this.DatGridRuleReason.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGridRuleReason_PageIndexChanged);
			this.DatGridCutOff.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGridCutOff_ItemCommand);
			this.DatGridCutOff.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DatGridCutOff_PageIndexChanged);
			this.DataGridTemplateTemp.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGridTemplateTemp_ItemCommand);

		}
		#endregion

		private void _btnFindTemplate_Click(object sender, EventArgs e)
		{

			if(_txtName.Text.ToString() == "" && _txtID.Text.ToString() == "")
			{
				conn.QueryString = "EXEC SCORING_BINDTEMPLATE";
			}
			else if(_txtName.Text.ToString() == "" && _txtID.Text.ToString() != "")
			{
				conn.QueryString = "EXEC SCORING_BINDTEMPLATE '" + _txtID.Text.ToString() + "'";
			}
			else if(_txtName.Text.ToString() != "" && _txtID.Text.ToString() == "")
			{
				conn.QueryString = "EXEC SCORING_BINDTEMPLATE NULL, '" + _txtName.Text.ToString() + "'";
			}
			else if(_txtName.Text.ToString() != "" && _txtID.Text.ToString() != "")
			{
				conn.QueryString = "EXEC SCORING_BINDTEMPLATE '" + _txtID.Text.ToString() + "','" + _txtName.Text.ToString() + "'";
			}

			conn.ExecuteQuery();
			BindData("DatGrd",conn.QueryString);
		}

		protected void _btnNewTemplate_Click(object sender, EventArgs e)
		{
			TR_NEW_TEMPLATE.Visible = true;
			TR_EDIT_TEMPLATE.Visible = false;
			TR_GRID_ATTRIBUTE.Visible = false;
			TR_ADD_ATTRIBUTE.Visible = false;
			TR_GRID_RULEREASON.Visible = false;
			TR_ADD_RULE_REASON.Visible = false;
			TR_EDIT_CUTOFF_SCORE.Visible = false;
			TR_CUTOFF.Visible = false;
			TR_DDL_ITEMPERTEMPLATE.Visible = false;
		}

		protected void _btnAddAttribute_Click(object sender, EventArgs e)
		{
			/*Tools.popMessage(this, _ddlAddAttribute.SelectedItem.Text.ToString());
			Tools.popMessage(this, _ddlAddAttribute.SelectedValue.ToString());*/
			
			/*conn.QueryString = "EXEC SCORING_INSERTRFSCORINGTEMPATT '" + _txtEditTempID.Text.ToString() + "','" + _ddlAddAttribute.SelectedValue.ToString() + "'";		//do not auto load anything.. 
			conn.ExecuteQuery();
			
			conn.QueryString = "EXEC SCORING_BINDTEMPATT '" + _txtEditTempID.Text.ToString() + "'";		//do not auto load anything.. 
			conn.ExecuteQuery();*/

			conn.QueryString = "EXEC SCORING_INSERTRFSCORINGTEMPATT '" + TXT_ID_TEMPLATE.Text.ToString() + "','" + DDL_ATTRIBUTE.SelectedValue.ToString() + "'";		//do not auto load anything.. 
			conn.ExecuteQuery();

			BindData("DatGrdAttribute", "EXEC SCORING_BINDTEMPATT '" + TXT_ID_TEMPLATE.Text.ToString() + "'");

			fillDDLAttribute(TXT_ID_TEMPLATE.Text.ToString());
		}

		protected void _btnEditedTemplateSave_Click(object sender, EventArgs e)
		{
			if(_txtEditTempDesc.Text == "")
			{
				Tools.popMessage(this, "Field cannot be empty");
			}
			else
			{
				/*conn.QueryString = "UPDATE RFSCORINGTEMPLATE SET [DESC] = '" + _txtEditTempDesc.Text.ToString() + "', ISACTIVE = '" + _rdoEditTempStats.SelectedValue.ToString() + "' WHERE ID = '" + _txtEditTempID.Text.ToString() + "'";
				conn.ExecuteNonQuery();

				BindData("DatGrd","EXEC SCORING_BINDTEMPLATE");*/

				conn.QueryString = "SCORING_INSERTTEMPLATETEMP '" + _txtEditTempDesc.Text.ToString() + "','1','UPDATE','" + _txtEditTempID.Text.ToString() + "'";
				conn.ExecuteQuery();

				Tools.popMessage(this, "Requesting approval...");
				BindData("DataGridTemplateTemp","EXEC SCORING_BINDTEMP 'TEMPLATE'");
				TR_EDIT_TEMPLATE.Visible = false;
			}
		}

		protected void _btnNewTemplateSave_Click(object sender, EventArgs e)
		{
			if(_txtNewTemplateDesc.Text == "")
			{
				Tools.popMessage(this, "Field cannot be empty !");
			}
			else
			{
				/*conn.QueryString = "EXEC SCORING_INSERTTEMPLATE '" + _txtNewTemplateDesc.Text.ToString() + "'";
				conn.ExecuteQuery();
			
				BindData("DatGrd", "EXEC SCORING_BINDTEMPLATE");*/

				conn.QueryString = "SCORING_INSERTTEMPLATETEMP '" + _txtNewTemplateDesc.Text.ToString() + "','1','INSERT','-1'";
				conn.ExecuteQuery();
				
				BindData("DataGridTemplateTemp","EXEC SCORING_BINDTEMP 'TEMPLATE'");
				Tools.popMessage(this, "Requesting approval...");
			}
		}


		private void DatGrd_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "ViewAttribute":
					BindData("DatGrdAttribute", "EXEC SCORING_BINDTEMPATT '" + e.Item.Cells[0].Text.ToString() + "'");
					TR_GRID_ATTRIBUTE.Visible = true;
					TR_ADD_ATTRIBUTE.Visible = true;
					TR_GRID_RULEREASON.Visible = false;
					TR_ADD_RULE_REASON.Visible = false;
					TR_DDL_ITEMPERTEMPLATE.Visible = false;
					TR_CUTOFF.Visible = false;
					TR_EDIT_CUTOFF_SCORE.Visible = false;
					TR_EDIT_TEMPLATE.Visible = false;
					TR_NEW_TEMPLATE.Visible = false;

					TXT_ID_TEMPLATE.Text = e.Item.Cells[0].Text.ToString();
					fillDDLAttribute(e.Item.Cells[0].Text.ToString());
					break;
				case "ViewRuleReason":
					BindData("DatGridRuleReason", "EXEC SCORING_BINDTEMPRULE '" + e.Item.Cells[0].Text.ToString() + "'");
					TR_GRID_RULEREASON.Visible = true;
					TR_ADD_RULE_REASON.Visible = true;
					TR_GRID_ATTRIBUTE.Visible = false;
					TR_ADD_ATTRIBUTE.Visible = false;
					TR_DDL_ITEMPERTEMPLATE.Visible = false;
					TR_CUTOFF.Visible = false;
					TR_EDIT_CUTOFF_SCORE.Visible = false;
					TR_EDIT_TEMPLATE.Visible = false;
					TR_NEW_TEMPLATE.Visible = false;

					TXT_ID_TEMPLATE.Text = e.Item.Cells[0].Text.ToString();
					fillDDLRuleReason(e.Item.Cells[0].Text.ToString());
					break;
				case "ViewCutOff" :
					TXT_ID_TEMPLATE.Text = e.Item.Cells[0].Text.ToString();
					fillTR_DDLITEM();
					fillTR_EDIT_CUTOFF_SCORE();
					
					TR_DDL_ITEMPERTEMPLATE.Visible = true;
					TR_CUTOFF.Visible = false;
					TR_GRID_ATTRIBUTE.Visible = false;
					TR_ADD_ATTRIBUTE.Visible = false;
					TR_GRID_RULEREASON.Visible = false;
					TR_ADD_RULE_REASON.Visible = false;
					TR_EDIT_CUTOFF_SCORE.Visible = false;
					TR_EDIT_TEMPLATE.Visible = false;
					TR_NEW_TEMPLATE.Visible = false;
					break;
				case "Enable":
					conn.QueryString = "UPDATE RFSCORINGTEMPLATE SET ISACTIVE = '1' WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteNonQuery();
					TXT_ID_TEMPLATE.Text = e.Item.Cells[0].Text.ToString();
					BindData("DatGrd","EXEC SCORING_BINDTEMPLATE");
					break;
				case "Disable":
					conn.QueryString = "UPDATE RFSCORINGTEMPLATE SET ISACTIVE = '0' WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteNonQuery();
					TXT_ID_TEMPLATE.Text = e.Item.Cells[0].Text.ToString();
					BindData("DatGrd","EXEC SCORING_BINDTEMPLATE");
					break;
				case "Edit" :
					TR_EDIT_TEMPLATE.Visible = true;
					TR_NEW_TEMPLATE.Visible = false;
					TR_GRID_ATTRIBUTE.Visible = false;
					TR_ADD_ATTRIBUTE.Visible = false;
					TR_GRID_RULEREASON.Visible = false;
					TR_ADD_RULE_REASON.Visible = false;
					TR_EDIT_CUTOFF_SCORE.Visible = false;
					TR_CUTOFF.Visible = false;
					TR_DDL_ITEMPERTEMPLATE.Visible = false;


					conn.QueryString = "SELECT [ID], [DESC], [ISACTIVE] FROM RFSCORINGTEMPLATE WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					_txtEditTempID.Text = conn.GetFieldValue("ID");
					_txtEditTempDesc.Text = conn.GetFieldValue("DESC");
					_rdoEditTempStats.SelectedValue = conn.GetFieldValue("ISACTIVE");

					break;
			}
		}

		
		private void DatGrdComposition_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Delete":
					conn.QueryString = "DELETE RFSCORING_NONRANGEATTPERTEMPLATE WHERE [ID] in " +
						"(SELECT RFSCORING_NONRANGEATTPERTEMPLATE.[ID] " +
						"FROM RFSCORINGTEMPATT, RFSCORING_NONRANGEATTPERTEMPLATE, " +
						"RFSCORINGWEIGHTNONRANGEATTRIBUTE, RFSCORINGATTRIBUTE " +
						"WHERE RFSCORINGTEMPATT.[ID] = '" + e.Item.Cells[0].Text.ToString() + "' " +
						"AND RFSCORINGTEMPATT.IDTEMPLATE = '" + TXT_ID_TEMPLATE.Text.ToString() + "' " +
						"AND RFSCORINGTEMPATT.IDATTRIBUTE = RFSCORINGATTRIBUTE.[ID] " + 
						"AND RFSCORINGTEMPATT.IDTEMPLATE = RFSCORING_NONRANGEATTPERTEMPLATE.IDTEMPLATE " + 
						"AND RFSCORINGWEIGHTNONRANGEATTRIBUTE.IDATTRIBUTE = RFSCORINGATTRIBUTE.[ID] " +
						"AND RFSCORING_NONRANGEATTPERTEMPLATE.IDATTRIBUTENONRANGE = RFSCORINGWEIGHTNONRANGEATTRIBUTE.[ID])";
					conn.ExecuteNonQuery();

					conn.QueryString = "DELETE RFSCORING_RANGEATTPERTEMPLATE WHERE [ID] in " +
						"(SELECT RFSCORING_RANGEATTPERTEMPLATE.[ID] " +
						"FROM RFSCORINGTEMPATT, RFSCORING_RANGEATTPERTEMPLATE, " +
						"RFSCORINGWEIGHTRANGEATTRIBUTE, RFSCORINGATTRIBUTE " +
						"WHERE RFSCORINGTEMPATT.[ID] = '" + e.Item.Cells[0].Text.ToString() + "' " +
						"AND RFSCORINGTEMPATT.IDTEMPLATE = '" + TXT_ID_TEMPLATE.Text.ToString() + "' " + 
						"AND RFSCORINGTEMPATT.IDATTRIBUTE = RFSCORINGATTRIBUTE.[ID] " +
						"AND RFSCORINGTEMPATT.IDTEMPLATE = RFSCORING_RANGEATTPERTEMPLATE.IDTEMPLATE " + 
						"AND RFSCORINGWEIGHTRANGEATTRIBUTE.IDATTRIBUTE = RFSCORINGATTRIBUTE.[ID] " +
						"AND RFSCORING_RANGEATTPERTEMPLATE.IDATTRIBUTERANGE = RFSCORINGWEIGHTRANGEATTRIBUTE.[ID])"; 
					conn.ExecuteNonQuery();

					conn.QueryString = "SELECT IDATTRIBUTE FROM RFSCORINGTEMPATT WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					string temp = conn.GetFieldValue("IDATTRIBUTE").ToString();

					conn.QueryString = "DELETE RFSCORING_NONRANGEATTPERTEMPLATE WHERE IDTEMPLATE = '" + TXT_ID_TEMPLATE.Text.ToString() + "' AND " +
						"IDATTRIBUTENONRANGE = '" + temp + "'";
					conn.ExecuteQuery();

					conn.QueryString = "DELETE RFSCORING_RANGEATTPERTEMPLATE WHERE IDTEMPLATE = '" + TXT_ID_TEMPLATE.Text.ToString() + "' AND " +
						"IDATTRIBUTERANGE = '" + temp + "'";
					conn.ExecuteQuery();

					conn.QueryString = "DELETE RFSCORINGTEMPATT WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteNonQuery();
					//conn.QueryString = "DELETE RFSCORING_NONRANGEATTPERTEMPLATE WHERE [ID] = '";
					fillDDLAttribute(TXT_ID_TEMPLATE.Text.ToString());
					BindData("DatGrdAttribute","EXEC SCORING_BINDTEMPATT '" + TXT_ID_TEMPLATE.Text.ToString() + "'");
					break;
				case "EditValue":
					//cek jenis range atau non range
					conn.QueryString =	"SELECT RFSCORINGATTRIBUTE.ISRANGE, RFSCORINGATTRIBUTE.[ID] " +
										"FROM RFSCORINGTEMPATT, RFSCORINGATTRIBUTE " +
										"WHERE RFSCORINGTEMPATT.IDATTRIBUTE = RFSCORINGATTRIBUTE.[ID] " +
										"AND RFSCORINGTEMPATT.IDTEMPLATE = '" + TXT_ID_TEMPLATE.Text.ToString() + "' " +
										"AND RFSCORINGTEMPATT.[ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					
					if(conn.GetFieldValue("ISRANGE") == "0")
					{
						idAttributeNonRange.Text = conn.GetFieldValue("ID");
						TR_ATTRIBUTE_NONRANGE.Visible = true;
						TR_ATTRIBUTE_RANGE.Visible = false;
						fillNonRangeDDL();
					}
					else if(conn.GetFieldValue("ISRANGE") == "1")
					{
						idAttributeRange.Text = conn.GetFieldValue("ID");
						TR_ATTRIBUTE_RANGE.Visible = true;
						TR_ATTRIBUTE_NONRANGE.Visible = false;
						fillRangeDDL();
					}

					break;
			}
		}

		protected void BTN_ADD_RULEREASON_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "EXEC SCORING_INSERTRFSCORINGRULEREASONPERTEMPLATE '" + TXT_ID_TEMPLATE.Text + "','" + DDL_RULE_REASON.SelectedValue.ToString() + "','0'";
			conn.ExecuteQuery();
			fillDDLRuleReason(TXT_ID_TEMPLATE.Text);
			BindData("DatGridRuleReason","EXEC SCORING_BINDTEMPRULE '" + TXT_ID_TEMPLATE.Text + "'");
		}

		private void DatGrd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGrd.CurrentPageIndex >= 0 && DatGrd.CurrentPageIndex < DatGrd.PageCount)
			{
				DatGrd.CurrentPageIndex = e.NewPageIndex;
				BindData("DatGrd","EXEC SCORING_BINDTEMPLATE");
			}
		}

		private void DatGrdAttribute_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGrdAttribute.CurrentPageIndex >= 0 && DatGrdAttribute.CurrentPageIndex < DatGrdAttribute.PageCount)
			{
				DatGrdAttribute.CurrentPageIndex = e.NewPageIndex;
				BindData("DatGrdAttribute", "EXEC SCORING_BINDTEMPATT '" + TXT_ID_TEMPLATE.Text.ToString() + "'");
			}
		}

		private void DatGridRuleReason_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGridRuleReason.CurrentPageIndex >= 0 && DatGridRuleReason.CurrentPageIndex < DatGridRuleReason.PageCount)
			{
				DatGridRuleReason.CurrentPageIndex = e.NewPageIndex;
				BindData("DatGridRuleReason", "EXEC SCORING_BINDTEMPRULE '" + TXT_ID_TEMPLATE.Text.ToString() + "'");
			}
		}

		private void DatGridRuleReason_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Delete":
					conn.QueryString = "DELETE RFSCORINGRULEREASONPERTEMPLATE WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteNonQuery();
					fillDDLRuleReason(TXT_ID_TEMPLATE.Text.ToString());
					BindData("DatGridRuleReason","EXEC SCORING_BINDTEMPRULE '" + TXT_ID_TEMPLATE.Text.ToString() + "'");
					break; 
				case "Edit":
					TR_EDIT_RULE_REASON.Visible = true;
					//id rfscoringrulereasonpertemplate
					idRuleReason.Text = e.Item.Cells[0].Text.ToString();
					conn.QueryString = "SELECT RFSCORINGRULEREASON.[DESCRIPTION],RFSCORINGRULEREASON.[REASON_CODE]," +
						"RFSCORINGRULEREASONPERTEMPLATE.[QUERYCOMPARATION] " +
						"FROM RFSCORINGRULEREASONPERTEMPLATE, RFSCORINGRULEREASON " +
						"WHERE RFSCORINGRULEREASONPERTEMPLATE.[IDRFSCORINGRULEREASON] = RFSCORINGRULEREASON.[ID] " +
						"AND RFSCORINGRULEREASONPERTEMPLATE.[ID] = '" + idRuleReason.Text.ToString() + "'";
					conn.ExecuteQuery();

					_txtEditedRRDesc.Text = conn.GetFieldValue("DESCRIPTION");
					_txtEditedRRCode.Text = conn.GetFieldValue("REASON_CODE");
					_txtEditedRRResult.Text = conn.GetFieldValue("QUERYCOMPARATION");
					break;
			}
		}

		protected void _btnEditedUpdate_Click(object sender, System.EventArgs e)
		{
			if(_txtEditedRRResult.Text == "")
			{
				Tools.popMessage(this, "Field cannot be empty !");
			}
			else
			{
				conn.QueryString = "UPDATE RFSCORINGRULEREASONPERTEMPLATE SET QUERYCOMPARATION = '" + _txtEditedRRResult.Text.ToString() + "' WHERE [ID] = '" + idRuleReason.Text.ToString() + "'";
				conn.ExecuteQuery();

				BindData("DatGridRuleReason","EXEC SCORING_BINDTEMPRULE '" + TXT_ID_TEMPLATE.Text.ToString() + "'");
			}
		}

		protected void DDL_EDITATTNONRANGE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TR_ATTRIBUTE_NONRANGE.Visible = true;

			conn.QueryString = "SELECT [WEIGHT] FROM RFSCORING_NONRANGEATTPERTEMPLATE WHERE [ID] = '" + DDL_EDITATTNONRANGE.SelectedValue.ToString() + "'";
			conn.ExecuteQuery();

			_txtNewAttributeNonRangeWeight.Text = conn.GetFieldValue("WEIGHT");
		}

		protected void _btnNewAttributeNonRange_Click(object sender, System.EventArgs e)
		{
			if(_txtNewAttributeNonRangeWeight.Text == "")
			{
				Tools.popMessage(this, "Field cannot be empty !");
			}
			else
			{
				//Tools.popMessage(this,DDL_EDITATTNONRANGE.SelectedValue.ToString());
				//------------> sampai sini
				TR_ATTRIBUTE_NONRANGE.Visible = true;
				conn.QueryString = "UPDATE RFSCORING_NONRANGEATTPERTEMPLATE SET WEIGHT = '" + _txtNewAttributeNonRangeWeight.Text.ToString() + "' WHERE [ID] = '" + DDL_EDITATTNONRANGE.SelectedValue.ToString() + "'";
				conn.ExecuteQuery();

				conn.QueryString = "SELECT [WEIGHT] FROM RFSCORING_NONRANGEATTPERTEMPLATE WHERE [ID] = '" + DDL_EDITATTNONRANGE.SelectedValue.ToString() + "'";
				conn.ExecuteQuery();

				_txtNewAttributeNonRangeWeight.Text = conn.GetFieldValue("WEIGHT");
			}
		}

		protected void _btnEditedAttributeRangeWeight_Click(object sender, System.EventArgs e)
		{
			if(TXT_EditedAttributeRangeLowest.Text == "" || TXT_EditedAttributeRangeHighest.Text == "" ||
				TXT_EditedAttributeRangeWeight.Text == "")
			{
				Tools.popMessage(this, "Field cannot be empty !");
			}
			else
			{
				TR_ATTRIBUTE_RANGE.Visible = true;

				if(_btnEditedAttributeRangeWeight.Text.ToString().ToUpper().Replace(" ","") == "UPDATEATTRIBUTE")
				{
					conn.QueryString = "UPDATE RFSCORING_RANGEATTPERTEMPLATE SET LOWESTSCORE = '" + TXT_EditedAttributeRangeLowest.Text + 
						"', HIGHESTSCORE = '" + TXT_EditedAttributeRangeHighest.Text + "'," +
						"WEIGHT = '" + TXT_EditedAttributeRangeWeight.Text + "' " +
						"WHERE [ID] = '" + DDL_EDITATTRANGE.SelectedValue.ToString() + "'";
					conn.ExecuteQuery();
				
				}
				else if(_btnEditedAttributeRangeWeight.Text.ToString().ToUpper().Replace(" ","") == "ADDATTRIBUTE")
				{
					if(RDO_EditedStatus.SelectedValue.ToString() == "0")
					{
						//normal
						conn.QueryString = "EXEC SCORING_INSERTRFSCORING_RANGEATTPERTEMPLATE '" 
							+ idAttributeRange.Text.ToString() + "','"
							+ TXT_EditedAttributeRangeLowest.Text.ToString() + "','"
							+ TXT_EditedAttributeRangeHighest.Text.ToString() + "','"
							+ TXT_EditedAttributeRangeWeight.Text.ToString() + "','"
							+ TXT_ID_TEMPLATE.Text.ToString() + "'";
						conn.ExecuteQuery();
					}
					else if(RDO_EditedStatus.SelectedValue.ToString() == "1")
					{
						//no information
						conn.QueryString = "SELECT * FROM RFSCORING_RANGEATTPERTEMPLATE " +
							"WHERE LOWESTSCORE = 'NO INFORMATION' AND HIGHESTSCORE = 'NO INFORMATION' " + 
							"AND IDTEMPLATE = '" + TXT_ID_TEMPLATE.Text + "' " + 
							"AND IDATTRIBUTERANGE = '" + idAttributeRange.Text + "'";
						conn.ExecuteQuery();

						if(conn.GetRowCount() <= 0)
						{
							conn.QueryString = "EXEC SCORING_INSERTRFSCORING_RANGEATTPERTEMPLATE '" 
								+ idAttributeRange.Text.ToString() + "','"
								+ "NO INFORMATION" + "','"
								+ "NO INFORMATION" + "','"
								+ TXT_EditedAttributeRangeWeight.Text.ToString() + "','"
								+ TXT_ID_TEMPLATE.Text.ToString() + "'";
							conn.ExecuteQuery();
						}
						else
						{
							Tools.popMessage(this,"The NO INFORMATION value is already exist !");
						}
					}
					else if(RDO_EditedStatus.SelectedValue.ToString() == "2")
					{
						//high

						conn.QueryString = "SELECT * FROM RFSCORING_RANGEATTPERTEMPLATE " +
							"WHERE HIGHESTSCORE = 'HIGH' " + 
							"AND IDTEMPLATE = '" + TXT_ID_TEMPLATE.Text + "' " + 
							"AND IDATTRIBUTERANGE = '" + idAttributeRange.Text + "'";
						conn.ExecuteQuery();

						if(conn.GetRowCount() <= 0)
						{
							conn.QueryString = "EXEC SCORING_INSERTRFSCORING_RANGEATTPERTEMPLATE '" 
								+ idAttributeRange.Text.ToString() + "','"
								+ TXT_EditedAttributeRangeLowest.Text.ToString() + "','"
								+ "HIGH" + "','"
								+ TXT_EditedAttributeRangeWeight.Text.ToString() + "','"
								+ TXT_ID_TEMPLATE.Text.ToString() + "'";
							conn.ExecuteQuery();
						}
						else
						{
							Tools.popMessage(this,"The HIGH value is already exist !");
						}
					}
					else if(RDO_EditedStatus.SelectedValue.ToString() == "3")
					{
						//below

						conn.QueryString = "SELECT * FROM RFSCORING_RANGEATTPERTEMPLATE " +
							"WHERE LOWESTSCORE = 'BELOW' " + 
							"AND IDTEMPLATE = '" + TXT_ID_TEMPLATE.Text + "' " + 
							"AND IDATTRIBUTERANGE = '" + idAttributeRange.Text + "'";
						conn.ExecuteQuery();

						if(conn.GetRowCount() <= 0)
						{
							conn.QueryString = "EXEC SCORING_INSERTRFSCORING_RANGEATTPERTEMPLATE '" 
								+ idAttributeRange.Text.ToString() + "','"
								+ "BELOW" + "','"
								+ TXT_EditedAttributeRangeHighest.Text.ToString() + "','"
								+ TXT_EditedAttributeRangeWeight.Text.ToString() + "','"
								+ TXT_ID_TEMPLATE.Text.ToString() + "'";
							conn.ExecuteQuery();
						}
						else
						{
							Tools.popMessage(this,"The BELOW value is already exist !");
						}
					}
				}
				else if(_btnEditedAttributeRangeWeight.Text.ToString().ToUpper().Replace(" ","") == "DELETEATTRIBUTE")
				{
					conn.QueryString = "DELETE RFSCORING_RANGEATTPERTEMPLATE WHERE [ID] = '" + DDL_EDITATTRANGE.SelectedValue.ToString() + "'";
					conn.ExecuteQuery();

					//DDL_EDITATTRANGE.SelectedValue = "0";
				}

				BindTR_ATTRIBUTE_RANGE();
			}
		}

		private void BindTR_ATTRIBUTE_RANGE()
		{
			string selectedvalue = DDL_EDITATTRANGE.SelectedValue.ToString();

			fillRangeDDL();

			if(_btnEditedAttributeRangeWeight.Text.ToString().ToUpper().Replace(" ","") == "DELETEATTRIBUTE")
			{
				DDL_EDITATTRANGE.SelectedIndex = 0;
			}
			else
			{
				DDL_EDITATTRANGE.SelectedValue = selectedvalue;
			}

			TR_ATTRIBUTE_RANGE.Visible = true;

				conn.QueryString = "SELECT DISTINCT(RFSCORINGATTRIBUTE.[DESCRIPT]) as DESCRIPT, RFSCORING_RANGEATTPERTEMPLATE.[LOWESTSCORE], " +
				"RFSCORING_RANGEATTPERTEMPLATE.[HIGHESTSCORE], RFSCORING_RANGEATTPERTEMPLATE.[WEIGHT] " +
				"FROM RFSCORING_RANGEATTPERTEMPLATE, RFSCORINGWEIGHTRANGEATTRIBUTE, " + 
				"RFSCORINGATTRIBUTE WHERE RFSCORINGATTRIBUTE.[ID] = RFSCORINGWEIGHTRANGEATTRIBUTE.IDATTRIBUTE " +
				"AND RFSCORINGWEIGHTRANGEATTRIBUTE.IDATTRIBUTE = RFSCORING_RANGEATTPERTEMPLATE.IDATTRIBUTERANGE " +
				"AND RFSCORING_RANGEATTPERTEMPLATE.[ID] = '" + DDL_EDITATTRANGE.SelectedValue.ToString() + "'";
				conn.ExecuteQuery();

				TXT_EditedAttributeRangeID.Text = conn.GetFieldValue("DESCRIPT");
				TXT_EditedAttributeRangeWeight.Text = conn.GetFieldValue("WEIGHT");
				TXT_EditedAttributeRangeLowest.Text = conn.GetFieldValue("LOWESTSCORE");
				TXT_EditedAttributeRangeHighest.Text = conn.GetFieldValue("HIGHESTSCORE");

				if(conn.GetFieldValue("LOWESTSCORE").ToString().ToUpper().Trim() == "BELOW")
				{
					RDO_EditedStatus.SelectedValue = "3";
				}
				else if(conn.GetFieldValue("HIGHESTSCORE").ToString().ToUpper().Trim() == "HIGH")
				{
					RDO_EditedStatus.SelectedValue = "2";
				}
				else if(conn.GetFieldValue("HIGHESTSCORE").ToString().ToUpper().Replace(" ","") == "NOINFORMATION" && 
					conn.GetFieldValue("LOWESTSCORE").ToString().ToUpper().Replace(" ","") == "NOINFORMATION")
				{
					RDO_EditedStatus.SelectedValue = "1";
				}
				else
				{
					RDO_EditedStatus.SelectedValue = "0";
				}
		}

		protected void DDL_EDITATTRANGE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindTR_ATTRIBUTE_RANGE();
		}

		protected void RDO_Action_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TR_ATTRIBUTE_RANGE.Visible = true;

			RDO_EditedStatus.SelectedValue = "3";
			TXT_EditedAttributeRangeLowest.Text = "BELOW";

			if(RDO_Action.SelectedValue == "1")
			{
				//add
				DDL_EDITATTRANGE.Enabled = true;
				RDO_EditedStatus.Enabled = false;
				_btnEditedAttributeRangeWeight.Text = "Update Attribute";
				DDL_EDITATTRANGE.SelectedIndex = 0;
				TXT_EditedAttributeRangeLowest.Enabled = true;
				TXT_EditedAttributeRangeHighest.Enabled = true;
			}
			else if(RDO_Action.SelectedValue == "0")
			{
				//update
				DDL_EDITATTRANGE.Enabled = false;
				RDO_EditedStatus.Enabled = true;
				_btnEditedAttributeRangeWeight.Text = "Add Attribute";
				DDL_EDITATTRANGE.SelectedIndex = 0;
				TXT_EditedAttributeRangeLowest.Enabled = true;
				TXT_EditedAttributeRangeHighest.Enabled = true;
			}
			else if(RDO_Action.SelectedValue == "2")
			{
				DDL_EDITATTRANGE.Enabled = true;
				RDO_EditedStatus.Enabled = false;
				_btnEditedAttributeRangeWeight.Text = "Delete Attribute";
				TXT_EditedAttributeRangeLowest.Enabled = false;
				TXT_EditedAttributeRangeHighest.Enabled = true;
			}
		}

		protected void RDO_EditedStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TR_ATTRIBUTE_RANGE.Visible = true;

			if(RDO_EditedStatus.SelectedValue.ToString() == "0")
			{
				TXT_EditedAttributeRangeLowest.Text = "";
				TXT_EditedAttributeRangeLowest.Enabled = true;
				TXT_EditedAttributeRangeHighest.Text = "";
				TXT_EditedAttributeRangeHighest.Enabled = true;
			}
			else if(RDO_EditedStatus.SelectedValue.ToString() == "1")
			{
				TXT_EditedAttributeRangeLowest.Text = "NO INFORMATION";
				TXT_EditedAttributeRangeLowest.Enabled = false;
				TXT_EditedAttributeRangeHighest.Text = "NO INFORMATION";
				TXT_EditedAttributeRangeHighest.Enabled = false;
			}
			else if(RDO_EditedStatus.SelectedValue.ToString() == "2")
			{
				//high
				TXT_EditedAttributeRangeHighest.Text = "HIGH";
				TXT_EditedAttributeRangeHighest.Enabled = false;
				TXT_EditedAttributeRangeLowest.Enabled = true;
			}
			else if(RDO_EditedStatus.SelectedValue.ToString() == "3")
			{
				//below
				TXT_EditedAttributeRangeLowest.Text = "BELOW";
				TXT_EditedAttributeRangeLowest.Enabled = false;
				TXT_EditedAttributeRangeHighest.Enabled = true;
			}
		}

		private void BindToTR_EDIT_CUTOFF_SCORE()
		{
			conn.QueryString = "SELECT [ID], SCORERESULT, PROPORSIACCOUNT, LOWESTSCORE, HIGHESTSCORE, ISHIGHEST, ISLOWEST FROM " +
				"RFSCORINGCUTOFFSCORE WHERE [ID] = '" + idAttributeCutOff.Text + "'";
			conn.ExecuteQuery();

			TXT_DESC_SCORE_CUTOFF_EDIT.Text = conn.GetFieldValue("SCORERESULT");
			TXT_PROPORSI_CUTOFF_EDIT.Text = conn.GetFieldValue("PROPORSIACCOUNT");
			TXT_LOWESTSCORE_CUTOFFEDIT.Text = conn.GetFieldValue("LOWESTSCORE");
			TXT_HIGHESTSCORE_CUTOFFEDIT.Text = conn.GetFieldValue("HIGHESTSCORE");

			if(conn.GetFieldValue("ISHIGHEST") == "1")
			{
				RDO_POSITION_CUTOFF_EDIT.SelectedValue = "0";
			}
			else if(conn.GetFieldValue("ISLOWEST") == "1")
			{
				RDO_POSITION_CUTOFF_EDIT.SelectedValue = "2";
			}
			else
			{
				RDO_POSITION_CUTOFF_EDIT.SelectedValue = "1";
			}
		}

		
		private void DatGridCutOff_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "EditValue" :
					TR_EDIT_CUTOFF_SCORE.Visible = true;
					idAttributeCutOff.Text = e.Item.Cells[0].Text.ToString();
					BTN_EDIT_CUTOFF.Text = "Update Parameter";
					BindToTR_EDIT_CUTOFF_SCORE();
					break;
				case "Delete" :
					conn.QueryString = "DELETE RFSCORINGCUTOFFSCORE WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					BindData("DatGridCutOff","EXEC SCORING_BINDSCORECUTOFFPERTEMPLATE '" + TXT_ID_TEMPLATE.Text.ToString() + "','" + DDL_ITEMPERTEMPLATE.SelectedValue.ToString() + "'");
					break;
			}
		}

		protected void BTN_EDIT_CUTOFF_Click(object sender, System.EventArgs e)
		{
			if(TXT_DESC_SCORE_CUTOFF_EDIT.Text == "" || TXT_LOWESTSCORE_CUTOFFEDIT.Text == "" ||
				TXT_HIGHESTSCORE_CUTOFFEDIT.Text == "")
			{
				Tools.popMessage(this, "Field cannot be empty !");
			}
			else
			{
				if(BTN_EDIT_CUTOFF.Text.ToString().ToUpper().Replace(" ", "") == "ADDPARAMETER")
				{
					//sampai sini
					//create add to cut off ya :)
					//kasih feature seperti yang diminta buat cut off (Baru dimention di 8 Maret ! Buset !) :) 
					//klo perlu bikin juga fitur ini buat attribute ama rule reason
					//ama maker checker, bikin cukup 1 hari ya :D
					
					string connString = "EXEC SCORING_INSERTSCORINGRFSCORINGCUTOFFSCORE '" + TXT_DESC_SCORE_CUTOFF_EDIT.Text + "','" +
						TXT_PROPORSI_CUTOFF_EDIT.Text + "','";

					if(RDO_POSITION_CUTOFF_EDIT.SelectedValue == "0")
					{
						connString += TXT_LOWESTSCORE_CUTOFFEDIT.Text + "','9999999999','";
						connString += "1','0','1',";
					}
					else if(RDO_POSITION_CUTOFF_EDIT.SelectedValue == "1")
					{
						connString += TXT_LOWESTSCORE_CUTOFFEDIT.Text + "','" +
							TXT_HIGHESTSCORE_CUTOFFEDIT.Text + "','";
						connString += "0','0','1',";
					}
					else if(RDO_POSITION_CUTOFF_EDIT.SelectedValue == "2")
					{
						connString += "-9999999999','" +
							TXT_HIGHESTSCORE_CUTOFFEDIT.Text + "','";
						connString += "0','1','1',";
					}

					string iditem = DDL_ITEMPERTEMPLATE.SelectedValue.ToString();

					if(iditem != "")
					{
						connString += "'" + iditem + "'";
						conn.QueryString = connString;
						conn.ExecuteQuery();
					}
					else
					{
						Tools.popMessage(this, "Pilih item terlebih dahulu !");
					}

					BindData("DatGridCutOff","EXEC SCORING_BINDSCORECUTOFFPERTEMPLATE '" + TXT_ID_TEMPLATE.Text.ToString() + "','" + DDL_ITEMPERTEMPLATE.SelectedValue.ToString() + "'");
				}
				else
				{
				
					string connString = "UPDATE RFSCORINGCUTOFFSCORE SET SCORERESULT = '" + TXT_DESC_SCORE_CUTOFF_EDIT.Text + 
						"', PROPORSIACCOUNT = '" + TXT_PROPORSI_CUTOFF_EDIT.Text + "', ";

					if(RDO_POSITION_CUTOFF_EDIT.SelectedValue == "0")
					{
						connString += "ISHIGHEST = '1', ISLOWEST = '0', HIGHESTSCORE = '9999999999', " +
							" LOWESTSCORE = '" + TXT_LOWESTSCORE_CUTOFFEDIT.Text.ToString();
					}
					else if(RDO_POSITION_CUTOFF_EDIT.SelectedValue == "1")
					{
						connString += "ISHIGHEST = '0', ISLOWEST = '0',HIGHESTSCORE = '" + TXT_HIGHESTSCORE_CUTOFFEDIT.Text.ToString() + "', " +
							" LOWESTSCORE = '" + TXT_LOWESTSCORE_CUTOFFEDIT.Text.ToString();
					}
					else if(RDO_POSITION_CUTOFF_EDIT.SelectedValue == "2")
					{
						connString += "ISHIGHEST = '0', ISLOWEST = '1',HIGHESTSCORE = '" + TXT_HIGHESTSCORE_CUTOFFEDIT.Text.ToString() + "', " +
							" LOWESTSCORE = '-9999999999'";
					}
				
					connString += "' WHERE [ID] = '" + idAttributeCutOff.Text + "'";

					conn.QueryString = connString;
					conn.ExecuteQuery();

					BindToTR_EDIT_CUTOFF_SCORE();
					BindData("DatGridCutOff","EXEC SCORING_BINDSCORECUTOFFPERTEMPLATE '" + TXT_ID_TEMPLATE.Text.ToString() + "','" + DDL_ITEMPERTEMPLATE.SelectedValue.ToString() + "'");
				}
			}
		}

		private void BTN_ADD_CUTOFF_Click(object sender, System.EventArgs e)
		{
		
		}

		protected void _btnUpdateCondition_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "UPDATE RFSCORINGCUTOFFITEMPERTEMPLATE SET CONDITION = '" + _txtCondition.Text.ToString() + 
				"', [PARAMETER] = '" + _txtParameter.Text + 
				"', [COLUMNNAME] = '" + _txtColumnName.Text + 
				"', [RESULT] = '" + _txtResult.Text +
				"', [OPERATOR] = '" + _ddlOperatorItem.SelectedValue.ToString() +
				"' WHERE [IDTEMPLATE] = '" + TXT_ID_TEMPLATE.Text + "' AND " +
				"[IDITEM] = '" + DDL_ITEMPERTEMPLATE.SelectedValue.ToString() + "'";
			//_txtCondition
			conn.ExecuteQuery();

			fillTR_EDIT_CUTOFF_SCORE();
		}

		private void DatGridCutOff_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGridCutOff.CurrentPageIndex >= 0 && DatGridCutOff.CurrentPageIndex < DatGridCutOff.PageCount)
			{
				DatGridCutOff.CurrentPageIndex = e.NewPageIndex;
				BindData("DatGridCutOff","EXEC SCORING_BINDSCORECUTOFFPERTEMPLATE '" + TXT_ID_TEMPLATE.Text.ToString() + "','" + DDL_ITEMPERTEMPLATE.SelectedValue.ToString() + "'");
			}
		}

		protected void _btnViewItem_Click(object sender, System.EventArgs e)
		{
			TR_CUTOFF.Visible = true;
			TR_EDIT_CUTOFF_SCORE.Visible = true;
			BindData("DatGridCutOff","EXEC SCORING_BINDSCORECUTOFFPERTEMPLATE '" + TXT_ID_TEMPLATE.Text.ToString() + "','" + DDL_ITEMPERTEMPLATE.SelectedValue.ToString() + "'");
		}

		protected void DDL_ITEMPERTEMPLATE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			conn.QueryString = "SELECT [CONDITION],[COLUMNNAME],[PARAMETER],[OPERATOR],[RESULT] FROM RFSCORINGCUTOFFITEMPERTEMPLATE WHERE  [IDTEMPLATE] = '" + TXT_ID_TEMPLATE.Text + "' AND " +
				"[IDITEM] = '" + DDL_ITEMPERTEMPLATE.SelectedValue.ToString() + "'";
			conn.ExecuteQuery();
			_txtCondition.Text = conn.GetFieldValue("CONDITION");
			_txtColumnName.Text = conn.GetFieldValue("COLUMNNAME");
			_txtParameter.Text = conn.GetFieldValue("PARAMETER");
			_txtResult.Text = conn.GetFieldValue("RESULT");
			_ddlOperatorItem.SelectedValue = conn.GetFieldValue("OPERATOR");
			BindData("DatGridCutOff","EXEC SCORING_BINDSCORECUTOFFPERTEMPLATE '" + TXT_ID_TEMPLATE.Text.ToString() + "','" + DDL_ITEMPERTEMPLATE.SelectedValue.ToString() + "'");
		}

		private void DataGridTemplateTemp_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TR_TEMPLATE_TEMP.Visible = true;

					conn.QueryString = "SELECT [ID], [DESC], [ISACTIVE] FROM RFSCORINGTEMPLATETEMP WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();

					_txtIdTemplateTemp.Text = conn.GetFieldValue("ID");
					_txtIdDescTemplateTemp.Text = conn.GetFieldValue("DESC");
					_rdoStatusTemplateTemp.SelectedValue = conn.GetFieldValue("ISACTIVE");

					BindData("DataGridTemplateTemp","EXEC SCORING_BINDTEMP 'TEMPLATE'");
					break;
				case "Delete":
					conn.QueryString = "DELETE RFSCORINGTEMPLATETEMP WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					BindData("DataGridTemplateTemp","EXEC SCORING_BINDTEMP 'TEMPLATE'");
					Tools.popMessage(this, "Requesting approval...");
					break;
			}
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			TR_TEMPLATE_TEMP.Visible = false;

			conn.QueryString = "UPDATE RFSCORINGTEMPLATETEMP SET [DESC] = '" + _txtIdDescTemplateTemp.Text + "'," +
				"ISACTIVE = '" + _rdoStatusTemplateTemp.SelectedValue + "' " +
				"WHERE [ID] = '" + _txtIdTemplateTemp.Text + "'";
			conn.ExecuteQuery();

			Tools.popMessage(this, "Requesting approval...");

			BindData("DataGridTemplateTemp","EXEC SCORING_BINDTEMP 'TEMPLATE'");
		}	

		/*template approval mulai dibawah*/

	}
}
