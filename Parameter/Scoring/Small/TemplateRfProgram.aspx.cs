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
	/// Summary description for TemplateRfProgram.
	/// </summary>
	public partial class TemplateRfProgram : System.Web.UI.Page
	{
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);//(Connection) Session["Connection"];

			//	if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//	Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack)
			{
				fillDdlOperator();

				//RFPROGRAM
				conn.QueryString = "select distinct(PROGRAMID) as PROGRAMID, PROGRAMDESC from rfprogram, rfscoringscrid " +
									"where rfprogram.scrid = rfscoringscrid.scrid";
				conn.ExecuteQuery();
				LIST_PROGRAM.Items.Clear();
				for (int i=0; i<conn.GetRowCount(); i++)
					LIST_PROGRAM.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				//RFTEMPLATE
				conn.QueryString = "SELECT [ID], [DESC] FROM RFSCORINGTEMPLATE WHERE ISACTIVE = '1' ";
				conn.ExecuteQuery();
				ddl_TEMPLATEID.Items.Clear();
				for (int i=0; i<conn.GetRowCount(); i++)
					ddl_TEMPLATEID.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

			}

			cekSelectedTemplate();
		}

		private void fillDdlOperator()
		{
			_ddlOperatorItem.Items.Clear();
			_ddlOperatorItem.Items.Add(new ListItem("none",""));
			_ddlOperatorItem.Items.Add(new ListItem("=","="));
			_ddlOperatorItem.Items.Add(new ListItem(">",">"));
			_ddlOperatorItem.Items.Add(new ListItem("<","<"));
			_ddlOperatorItem.Items.Add(new ListItem(">=",">="));
			_ddlOperatorItem.Items.Add(new ListItem("<=","<="));
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

		}
		#endregion

		protected void LIST_PROGRAM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LIST_CONDITION.Items.Clear();
			LIST_TEMPLATE.Items.Clear();
			TXT_CONDITION.Text = "";
			BTN_SAVE.Text = "Add Condition";
			TXT_PARAMETER.Text = "";
			TXT_COLUMN.Text = "";
			TXT_RESULT.Text = "";
			_ddlOperatorItem.SelectedValue = "";

			int count = 0;
			for (int i = 0; i < LIST_PROGRAM.Items.Count; i++)
				if (LIST_PROGRAM.Items[i].Selected) count++;

			if (count == 1)
			{
				fillCondition();
			}
		}

		private void fillCondition()
		{
			if (LIST_PROGRAM.SelectedValue.Trim() == "")
				return;
			conn.QueryString = "SELECT [ID], IDPROGRAM, CONDITION, IDTEMPLATE " +
								"FROM RFSCORINGPROGTOCONDITION " +
								"WHERE IDPROGRAM = '" + LIST_PROGRAM.SelectedValue + "'";
			conn.ExecuteQuery();
			for (int i=0; i<conn.GetRowCount(); i++)
			{
				LIST_CONDITION.Items.Add(new ListItem(conn.GetFieldValue(i, 2), conn.GetFieldValue(i, 0)));
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int count = 0;
			for (int i = 0; i < LIST_PROGRAM.Items.Count; i++)
				if (LIST_PROGRAM.Items[i].Selected) count++;

			if (count == 1)
			{
				//Tools.popMessage(this,LIST_PROGRAM.SelectedValue.ToString());
				if(LIST_CONDITION.SelectedValue.ToString().Trim() == "")
				{	
					conn.QueryString = "EXEC SCORING_INSERTRFSCORINGPROGTOCONDITION '" + LIST_PROGRAM.SelectedValue.ToString() + 
						"','" + TXT_CONDITION.Text.ToString() + "','" +
						TXT_PARAMETER.Text.ToString() + "','" +
						TXT_COLUMN.Text.ToString() + "','" +
						TXT_RESULT.Text.ToString() + "','" +
						_ddlOperatorItem.SelectedValue.ToString() + "',''";
					conn.ExecuteNonQuery();

					Tools.popMessage(this, "The condition has been inserted !");

					LIST_CONDITION.Items.Clear();
					fillCondition();
				}
				else
				{
					conn.QueryString = "EXEC SCORING_INSERTRFSCORINGPROGTOCONDITION '" + LIST_PROGRAM.SelectedValue.ToString() + "','" + TXT_CONDITION.Text.ToString() + "','" + 
						TXT_PARAMETER.Text.ToString() + "','" +
						TXT_COLUMN.Text.ToString() + "','" +
						TXT_RESULT.Text.ToString() + "','" +
						_ddlOperatorItem.SelectedValue.ToString() + "','" +
						LIST_CONDITION.SelectedValue.ToString() + "'";
					conn.ExecuteNonQuery();

					Tools.popMessage(this, "The condition has been updated !");

					LIST_CONDITION.Items.Clear();
					fillCondition();
					TXT_CONDITION.Text = "";
					TXT_PARAMETER.Text = "";
					TXT_COLUMN.Text = "";
					TXT_RESULT.Text = "";
					_ddlOperatorItem.SelectedValue = "";
				}
			}
			else
			{
				Tools.popMessage(this, "Please select program first !");
			}
		}

		protected void BTN_ADDPROD_Click(object sender, System.EventArgs e)
		{
			if(cekCheckedProgramAndCondition() == true)
			{
				conn.QueryString = "EXEC SCORING_INSERTRFSCORINGPROGTOCONDITION '" + LIST_PROGRAM.SelectedValue.ToString() + "','" + 
					TXT_CONDITION.Text.ToString() + "','" + 
					"','','','','" +
					LIST_CONDITION.SelectedValue.ToString() + "','" + 
					ddl_TEMPLATEID.SelectedValue.ToString() + "'";
				conn.ExecuteNonQuery();

				fillListTemplate();
			}
			else
			{
				Tools.popMessage(this,"Please select program or condition first !");
			}
		}

		private void fillListTemplate()
		{
			//LIST_TEMPLATE
			LIST_TEMPLATE.Items.Clear();
	
			conn.QueryString = "SELECT RFSCORINGTEMPLATE.[ID], RFSCORINGTEMPLATE.[DESC] FROM RFSCORINGPROGTOCONDITION, RFSCORINGTEMPLATE " +
			"WHERE RFSCORINGPROGTOCONDITION.IDTEMPLATE = RFSCORINGTEMPLATE.[ID] " +
			"AND RFSCORINGPROGTOCONDITION.[ID] = '" + LIST_CONDITION.SelectedValue.ToString() + "'";
			conn.ExecuteQuery();

			for (int i=0; i<conn.GetRowCount(); i++)
			{
				LIST_TEMPLATE.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
			}
		}

		protected void LIST_CONDITION_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillListTemplate();
			BTN_SAVE.Text = "Update Condition";
			TXT_CONDITION.Text = LIST_CONDITION.SelectedItem.Text.ToString();
			conn.QueryString = "SELECT PARAMETER,[COLUMN],[RESULT],[OPERATOR] FROM RFSCORINGPROGTOCONDITION WHERE [ID] = '" + LIST_CONDITION.SelectedValue.ToString() + "'";
			conn.ExecuteQuery();
			TXT_PARAMETER.Text = conn.GetFieldValue("PARAMETER");
			TXT_COLUMN.Text = conn.GetFieldValue("COLUMN");
			TXT_RESULT.Text = conn.GetFieldValue("RESULT");
			_ddlOperatorItem.SelectedValue = conn.GetFieldValue("OPERATOR");
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			TXT_CONDITION.Text = "";
			LIST_CONDITION.SelectedIndex = -1;
			BTN_SAVE.Text = "Add Condition";
			LIST_TEMPLATE.Items.Clear();
		}

		private void BTN_EDIT_CONDITION_Click(object sender, System.EventArgs e)
		{
			LIST_CONDITION.SelectedValue = "";
		}

		private bool cekCheckedProgramAndCondition()
		{
			int count = 0;
			for (int i = 0; i < LIST_CONDITION.Items.Count; i++)
				if (LIST_CONDITION.Items[i].Selected) count++;

			int count2 = 0;
			for (int i = 0; i < LIST_PROGRAM.Items.Count; i++)
				if (LIST_PROGRAM.Items[i].Selected) count2++;


			if (count == 1 && count2 == 1)
			{
				return true;
			}

			return false;
		}

		protected void BTN_DELETELISTCONDITION_Click(object sender, System.EventArgs e)
		{
			string id = LIST_CONDITION.SelectedValue.ToString();

			if(id.Trim() == "")
			{
				Tools.popMessage(this,"Please select the condition first !");
			}
			else
			{
				bool cek = cekCheckedProgramAndCondition();

				if(cek == true)
				{
					conn.QueryString = "DELETE RFSCORINGPROGTOCONDITION WHERE [ID] = '" + LIST_CONDITION.SelectedValue.ToString() + "'";
					conn.ExecuteNonQuery();

					Tools.popMessage(this, "Delete is succeed !");

					LIST_CONDITION.Items.Clear();
					fillCondition();
					TXT_CONDITION.Text = "";
				}
				else 
				{
					Tools.popMessage(this,"Please select the program first !");
				}
			}
		}

		private bool cekSelectedTemplate()
		{
			int count = 0;
			for (int i = 0; i < LIST_TEMPLATE.Items.Count; i++)
				if (LIST_TEMPLATE.Items[i].Selected) count++;

			if(count == 1)
				return true;
			else
				return false;
		}

		protected void LIST_TEMPLATE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			bool selected = cekSelectedTemplate();
		}

		protected void BTN_DELETELISTPRODUCT_Click(object sender, System.EventArgs e)
		{
			bool selected = cekSelectedTemplate();

			if(selected == true)
			{
				conn.QueryString = "EXEC SCORING_INSERTRFSCORINGPROGTOCONDITION '" + LIST_PROGRAM.SelectedValue.ToString() + "','" + 
					TXT_CONDITION.Text.ToString() + "','" + "','','','','" +
					LIST_CONDITION.SelectedValue.ToString() + "','-=DEL=-'";
				conn.ExecuteNonQuery();

				Tools.popMessage(this, "The templete has been removed from condition !");

				fillListTemplate();
			}
			else if(selected == false)
			{
				Tools.popMessage(this, "Please select the template first !");
			}
		}

	}
}
