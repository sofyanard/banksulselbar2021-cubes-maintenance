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
	/// Summary description for ParameterScoring.
	/// </summary>
	public partial class ParameterScoring : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button _btnDeleteItem;
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection(ConfigurationSettings.AppSettings["connME"]);

			if(!IsPostBack)
			{
				TR_EDIT_PARAMETER.Visible = false;
				TR_NEW_PARAMETER.Visible = false;
				TR_TEMPLATE.Visible = false;
				TR_ITEM.Visible = false;
				TR_ADD_NEW_ITEM.Visible = false;
				
				ClearData();
				fillDDLItem();
			}

			/*request*/
			TR_EDIT_REQUEST_ITEM.Visible = false;
			BindData("DatGrdItemTemplate","EXEC SCORING_BINDTEMP 'ITEM'");
		}

		private void fillDDLItem()
		{
			conn.QueryString = "SELECT [ID], DESCRIPTION FROM RFSCORINGCUTOFFPERITEM";
			conn.ExecuteQuery();

			_ddlItemCutOff.Items.Clear();

			_ddlItemCutOff.Items.Add(new ListItem("-", "-1"));

			if(conn.GetRowCount() > 0)
			{
				for(int i=0; i<conn.GetRowCount(); i++)
				{
					_ddlItemCutOff.Items.Add(new ListItem(conn.GetFieldValue(i,"DESCRIPTION"), conn.GetFieldValue(i,"ID")));
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
			this.DatGrdItemTemplate.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DatGrdItemTemplate_ItemCommand);

		}
		#endregion

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

		private void ClearData()
		{
			_txtEditedDesc.Text = "";
			_txtEditedHighestScr.Text = "";
			_txtEditedID.Text = "";
			_txtEditedLowestScr.Text = "";
			_txtEditedProp.Text = "";
			_rdEditedLine.SelectedIndex = 1;
			_rdEditedStatus.SelectedIndex = 0;

			_txtNewDesc.Text = "";
			_txtNewHighestScr.Text = "";
			_txtNewLowestScr.Text = "";
			_txtNewProp.Text = "";
			_rdNewLine.SelectedIndex = 1;
			_rdNewStatus.SelectedIndex = 0;
		}

		private void cekEditedField(string id)
		{
			conn.QueryString = "SELECT * FROM RFSCORINGCUTOFFSCORE WHERE ID = '" + id + "';";		//do not auto load anything.. 
			conn.ExecuteQuery();
			_txtEditedID.Text = conn.GetFieldValue("ID").ToString();
			_txtEditedDesc.Text = conn.GetFieldValue("SCORERESULT").ToString();
			_txtEditedProp.Text = conn.GetFieldValue("PROPORSIACCOUNT").ToString(); 
			_txtEditedLowestScr.Text = conn.GetFieldValue("LOWESTSCORE").ToString();
			_txtEditedHighestScr.Text = conn.GetFieldValue("HIGHESTSCORE").ToString();
					
			if(conn.GetFieldValue("ISHIGHEST") == "1")
			{
				_rdEditedLine.SelectedValue = "0";
			}
			else if(conn.GetFieldValue("ISLOWEST") == "1")
			{
				_rdEditedLine.SelectedValue = "2";
			}
			else
			{
				_rdEditedLine.SelectedValue = "1";
			}

			if(conn.GetFieldValue("ISACTIVE") == "0")
			{
				_rdEditedStatus.SelectedValue = "0";
			}
			else
			{
				_rdEditedStatus.SelectedValue = "1";
			}
		}

		private void DatGrd_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TurnOnVisibleEditParameter(true);
					//Tools.popMessage(this, e.Item.Cells[0].Text.ToString());
					cekEditedField(e.Item.Cells[0].Text.ToString());
					break;
				case "Delete":
					conn.QueryString ="DELETE RFSCORINGCUTOFFSCORE WHERE [ID] = '" + e.Item.Cells[0].Text.ToString() + "'";
					conn.ExecuteQuery();
					BindData("DatGrd","EXEC SCORING_BINDCUTOFFPARAMETER '" + _ddlItemCutOff.SelectedValue.ToString() + "'");
					break;
			}
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

		protected void _btnEditedUpdate_Click(object sender, System.EventArgs e) //updateparameter
		{
			int ISHIGHEST = 0;
			int ISLOWEST = 0;
			string val =  "";
			int isfailed = 0;

			//sebelumnya cek dulu lowest ama uppestnya ada apa g ?
			if(_rdEditedLine.SelectedValue == "0")
			{
				conn.QueryString = "SELECT * FROM RFSCORINGCUTOFFSCORE WHERE ISHIGHEST = '" + 1 + "' AND IDITEM = '" + _ddlItemCutOff.SelectedValue.ToString() + "'";
				conn.ExecuteQuery();
				
				if(conn.GetRowCount() != 0)
				{
					Tools.popMessage(this, "Highest line has been set to another parameter !");	
					isfailed = 1;
				}
				else
				{
					ISHIGHEST = 1;
					isfailed = 0;
				}
			}
			else if(_rdEditedLine.SelectedValue == "2")
			{
				conn.QueryString = "SELECT * FROM RFSCORINGCUTOFFSCORE WHERE ISLOWEST = '" + 1 + "' AND IDITEM = '" + _ddlItemCutOff.SelectedValue.ToString() + "'";
				conn.ExecuteQuery();

				if(conn.GetRowCount() != 0)
				{
					Tools.popMessage(this, "Lowest line has been set to another parameter !");	
					isfailed = 1;
				}
				else
				{
					ISLOWEST = 1;
					isfailed = 0;
				}
			}

			//cek total proporsi lebih dari 100% atau tidak

			if(isfailed == 0)
			{
				//val =  CekProportion(_txtEditedID.Text.ToString(), _txtEditedProp.Text.ToString(), out isfailed);
				//RFSCORINGCUTOFFSCORETEMP
				conn.QueryString = "UPDATE RFSCORINGCUTOFFSCORE SET SCORERESULT = '" + _txtEditedDesc.Text.ToString() 
					+ "', PROPORSIACCOUNT = '" + _txtEditedProp.Text.ToString()
					+ "', LOWESTSCORE = '" + _txtEditedLowestScr.Text.ToString()
					+ "', HIGHESTSCORE = '" + _txtEditedHighestScr.Text.ToString()
					+ "', ISACTIVE = '" + _rdEditedStatus.SelectedValue.ToString()
					+ "', ISHIGHEST = '" + ISHIGHEST
					+ "', ISLOWEST = '" + ISLOWEST
					+ "' WHERE ID = '" + _txtEditedID.Text.ToString()
					+ "' AND IDITEM = '" + _ddlItemCutOff.SelectedValue.ToString() + "'";
				conn.ExecuteQuery();

				BindData("DatGrd","EXEC SCORING_BINDCUTOFFPARAMETER '" + _ddlItemCutOff.SelectedValue.ToString() + "'");

				Tools.popMessage(this, "Data has been updated !");	
			}

			cekEditedField(_txtEditedID.Text.ToString());
		}

		private string CekProportion(string id, string requestedvalue, out int isfailed)
		{
			string result = "";

			if(requestedvalue != "")
			{
				SqlDataReader reader;
				MyConnection con = new MyConnection();
				con.OpenConnection();

				if(id != "")
				{
					reader = con.Query("SELECT PROPORSIACCOUNT FROM RFSCORINGCUTOFFSCORE WHERE ISACTIVE = '1' AND ID <> '" + id + "'");
				}
				else
				{
					reader = con.Query("SELECT PROPORSIACCOUNT FROM RFSCORINGCUTOFFSCORE WHERE ISACTIVE = '1'");
				}

				int total = Convert.ToInt32(requestedvalue.Replace("%","").Replace(" ",""));
				int temp = 0;
				int temp2 = 0;

				if(id != "")
				{
					conn.QueryString = "SELECT * FROM RFSCORINGCUTOFFSCORE WHERE ID = '" + id + "'";
					conn.ExecuteQuery();
					temp2 = Convert.ToInt32(requestedvalue.Replace("%",""));

					if(conn.GetFieldValue("PROPORSIACCOUNT").Replace("%","").Replace(" ","") != "")
					{
						temp = Convert.ToInt32(conn.GetFieldValue("PROPORSIACCOUNT").Replace("%","").Replace(" ",""));
					}
					else
					{
						temp = 0;
					}
					/*int temp = Convert.ToInt32(conn.GetFieldValue("PROPORSIACCOUNT").Replace("%","").Replace(" ",""));
					int temp2 = Convert.ToInt32(requestedvalue.Replace("%",""));*/
				}

				if(reader.HasRows)
				{
					while(reader.Read())
					{
						if(reader[0].ToString().Replace(" ","").Replace("%","") != "")
						{
							total += Convert.ToInt32(reader[0].ToString().Replace(" ","").Replace("%",""));
						}
					}
				}

				if(total > 100)
				{
					isfailed = 1;
					Tools.popMessage(this, "Total proportion is exceed 100 ! Proportion cannot be updated !");
					if(id != "")
					{
						total = temp;
					}
				}
				else
				{
					isfailed = 0;
					if(id != "")
					{
						total = temp2;
					}
				}
				con.CloseConnection();

				result = total.ToString();
				return result;
			}

			isfailed = 0;
			return result;
		}

		protected void _btnNewUpdate_Click(object sender, System.EventArgs e) //insert new parameter
		{
			int ISHIGHEST = 0;
			int ISLOWEST = 0;
			int ISACTIVE = 0;
			int CHANGELINE = 0;

			//sebelumnya cek dulu lowest ama uppestnya ada apa g ?
			if(_rdNewLine.SelectedValue == "0")
			{
				conn.QueryString = "SELECT * FROM RFSCORINGCUTOFFSCORE WHERE ISHIGHEST = '" + 1 + "' and IDITEM = '" + _ddlItemCutOff.SelectedValue.ToString() + "'";
				conn.ExecuteQuery();
				
				if(conn.GetRowCount() != 0)
				{
					Tools.popMessage(this, "Highest line has been set to another parameter !");	
					CHANGELINE = 1;
				}
				else
				{
					ISHIGHEST = 1;
				}
			}
			else if(_rdNewLine.SelectedValue == "2")
			{
				conn.QueryString = "SELECT * FROM RFSCORINGCUTOFFSCORE WHERE ISLOWEST = '" + 1 + "' and IDITEM = '" + _ddlItemCutOff.SelectedValue.ToString() + "'";
				conn.ExecuteQuery();

				if(conn.GetRowCount() != 0)
				{
					Tools.popMessage(this, "Lowest line has been set to another parameter !");
					CHANGELINE = 1;
				}
				else
				{
					ISLOWEST = 1;
				}
			}

			if(CHANGELINE != 1)
			{
				conn.QueryString = "EXEC SCORING_INSERTSCORINGCUTOFFSCORE '" + _txtNewDesc.Text.ToString() + "','" +
					_txtNewProp.Text.ToString() + "'," +
					_txtNewLowestScr.Text.ToString() + "," +
					_txtNewHighestScr.Text.ToString() + "," +
					ISHIGHEST + "," +
					ISLOWEST + "," +
					_rdNewStatus.SelectedValue.ToString() + "," +
					_ddlItemCutOff.SelectedValue.ToString();
				conn.ExecuteNonQuery();
			}

			BindData("DatGrd","EXEC SCORING_BINDCUTOFFPARAMETER '" + _ddlItemCutOff.SelectedValue.ToString() + "'");
		}

		private void _btnNew_Click(object sender, System.EventArgs e)
		{
			ClearData();
			TurnOnVisibleNewParameter(true);
		}

		private void fillListBoxTemplate()
		{
			_listBoxAppliedTemplate.Items.Clear();
			_listBoxAvailableTemplate.Items.Clear();

			conn.QueryString = "SELECT RFSCORINGCUTOFFITEMPERTEMPLATE.[ID], RFSCORINGTEMPLATE.[DESC]  as [DESCRIPTION] " +
				"FROM RFSCORINGCUTOFFITEMPERTEMPLATE, RFSCORINGCUTOFFPERITEM, RFSCORINGTEMPLATE " +
				"WHERE RFSCORINGCUTOFFPERITEM.[ID] = '" + _ddlItemCutOff.SelectedValue.ToString() + "' " +
				"AND RFSCORINGCUTOFFITEMPERTEMPLATE.[IDITEM] = RFSCORINGCUTOFFPERITEM.[ID] " +
				"AND RFSCORINGTEMPLATE.[ID] = RFSCORINGCUTOFFITEMPERTEMPLATE.IDTEMPLATE";
			conn.ExecuteQuery();

			for(int i =0; i<conn.GetRowCount(); i++)
			{
				_listBoxAppliedTemplate.Items.Add(new ListItem(conn.GetFieldValue(i,"DESCRIPTION"), conn.GetFieldValue(i,"ID")));		
			}

			conn.QueryString = "SELECT RFSCORINGTEMPLATE.[ID], RFSCORINGTEMPLATE.[DESC] as [DESCRIPTION] " +
				"FROM RFSCORINGTEMPLATE WHERE RFSCORINGTEMPLATE.[ID] not in " +
				"(SELECT RFSCORINGTEMPLATE.[ID] FROM RFSCORINGCUTOFFITEMPERTEMPLATE, RFSCORINGCUTOFFPERITEM, RFSCORINGTEMPLATE " +
				"WHERE RFSCORINGCUTOFFPERITEM.[ID] = '" + _ddlItemCutOff.SelectedValue.ToString() + "' " +
				"AND RFSCORINGCUTOFFITEMPERTEMPLATE.[IDITEM] = RFSCORINGCUTOFFPERITEM.[ID] " +
				"AND RFSCORINGTEMPLATE.[ID] = RFSCORINGCUTOFFITEMPERTEMPLATE.IDTEMPLATE)";
			conn.ExecuteQuery();

			for(int i =0; i<conn.GetRowCount(); i++)
			{
				_listBoxAvailableTemplate.Items.Add(new ListItem(conn.GetFieldValue(i,"DESCRIPTION"), conn.GetFieldValue(i,"ID")));
			}
		}

		protected void _btnTemplate_Click(object sender, System.EventArgs e)
		{
			//sampai sini di bind
			if(_ddlItemCutOff.SelectedValue.ToString() != "-1")
			{
				TR_TEMPLATE.Visible = true;
				TR_NEW_PARAMETER.Visible = false;
				TR_EDIT_PARAMETER.Visible = false;
				TR_ITEM.Visible = false;
				TR_ADD_NEW_ITEM.Visible = false;
				fillListBoxTemplate();
			}
		}

		protected void _btnItem_Click(object sender, System.EventArgs e)
		{
			if(_ddlItemCutOff.SelectedValue.ToString() != "-1")
			{
				TR_TEMPLATE.Visible = false;
				TR_NEW_PARAMETER.Visible = true;
				TR_EDIT_PARAMETER.Visible = false;
				TR_ITEM.Visible = true;
				TR_ADD_NEW_ITEM.Visible = false;
				//sampai sini di bind

				_txtNewDesc.Text = "";
				_txtNewProp.Text = "";
				_txtNewLowestScr.Text = "";
				_txtNewHighestScr.Text = "";
				_rdNewLine.SelectedValue = "0";
				_rdNewStatus.SelectedValue = "1";

				BindData("DatGrd","EXEC SCORING_BINDCUTOFFPARAMETER '" + _ddlItemCutOff.SelectedValue.ToString() + "'");
			}
		}

		protected void _btnAddTemplate_Click(object sender, System.EventArgs e)
		{
			if(_listBoxAvailableTemplate.SelectedValue.ToString() != "")
			{
				conn.QueryString = "EXEC SCORING_INSERTRFSCORINGCUTOFFITEMPERTEMPLATE " + _ddlItemCutOff.SelectedValue.ToString() + "," +
					_listBoxAvailableTemplate.SelectedValue.ToString() + ",'','',''";
				conn.ExecuteQuery();

				fillListBoxTemplate();

				Tools.popMessage(this, "Success !");
			}
			else
			{
				Tools.popMessage(this, "Please select the available template first !");
			}
		}

		protected void _btnRemoveTemplate_Click(object sender, System.EventArgs e)
		{
			if(_listBoxAppliedTemplate.SelectedValue.ToString() != "")
			{
				conn.QueryString = "DELETE RFSCORINGCUTOFFITEMPERTEMPLATE WHERE [ID] = '" + _listBoxAppliedTemplate.SelectedValue.ToString() + "'";
				conn.ExecuteQuery();

				fillListBoxTemplate();

				Tools.popMessage(this, "Success !");
			}
			else
			{
				Tools.popMessage(this, "Please select the applied template first !");
			}
		}

		protected void _addNewItem_Click(object sender, System.EventArgs e)
		{
			_labelAddItem.Text = "Add New Item";
			_btnAddNewItem.Text = "Add";
			
			_txtItemName.Text = "";

			TR_ADD_NEW_ITEM.Visible = true;
			TR_EDIT_PARAMETER.Visible = false;
			TR_NEW_PARAMETER.Visible = false;
			TR_TEMPLATE.Visible = false;
			TR_ITEM.Visible = false;
		}

		protected void _btnAddNewItem_Click(object sender, System.EventArgs e)
		{
			if(_btnAddNewItem.Text == "Add")
			{
				conn.QueryString = "SELECT * FROM RFSCORINGCUTOFFPERITEM WHERE [DESCRIPTION] like '%" + _txtItemName.Text + "%'";
				conn.ExecuteQuery();

				if(conn.GetRowCount() > 0)
				{
					Tools.popMessage(this, "Nama terlalu umum atau sudah dipakai oleh item lain !");
				}
				else
				{
					/*conn.QueryString = "SCORING_INSERTRFSCORINGCUTOFFPERITEM '" + _txtItemName.Text + "'";
					conn.ExecuteQuery();*/

					conn.QueryString = "EXEC SCORING_INSERTITEMTEMP '" + _txtItemName.Text + "','-1','INSERT','1'";
					conn.ExecuteQuery();

					Tools.popMessage(this, "Requesting approval...");
				}
			}
			else if(_btnAddNewItem.Text == "Update")
			{
				TR_ADD_NEW_ITEM.Visible = false;

				conn.QueryString = "SELECT * FROM RFSCORINGCUTOFFPERITEM WHERE [DESCRIPTION] like '%" + _txtItemName.Text + "%'";
				conn.ExecuteQuery();

				if(conn.GetRowCount() > 0)
				{
					Tools.popMessage(this, "Nama terlalu umum atau sudah dipakai oleh item lain !");
				}
				else
				{
					/*conn.QueryString = "SCORING_INSERTRFSCORINGCUTOFFPERITEM '" + _txtItemName.Text + "'";
					conn.ExecuteQuery();*/

					conn.QueryString = "EXEC SCORING_INSERTITEMTEMP '" + _txtItemName.Text + "','" + _ddlItemCutOff.SelectedValue + "','UPDATE','1'";
					conn.ExecuteQuery();

					Tools.popMessage(this, "Requesting approval...");
				}
			}

			fillDDLItem();

			TR_EDIT_PARAMETER.Visible = false;
			TR_NEW_PARAMETER.Visible = false;
			TR_TEMPLATE.Visible = false;
			TR_ITEM.Visible = false;
			TR_ADD_NEW_ITEM.Visible = false;

			BindData("DatGrdItemTemplate","EXEC SCORING_BINDTEMP 'ITEM'");
		}

		private void DatGrd_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(DatGrd.CurrentPageIndex >= 0 && DatGrd.CurrentPageIndex < DatGrd.PageCount)
			{
				DatGrd.CurrentPageIndex = e.NewPageIndex;
				BindData("DatGrd","EXEC SCORING_BINDCUTOFFPARAMETER '" + _ddlItemCutOff.SelectedValue.ToString() + "'");
			}
		}

		protected void _ddlItemCutOff_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TR_EDIT_PARAMETER.Visible = false;
			TR_NEW_PARAMETER.Visible = false;
			TR_TEMPLATE.Visible = false;
			TR_ITEM.Visible = false;
			TR_ADD_NEW_ITEM.Visible = false;
		}

		protected void _btnEditItem_Click(object sender, System.EventArgs e)
		{
			_labelAddItem.Text = "Edit Item";
			_btnAddNewItem.Text = "Update";
			TR_ADD_NEW_ITEM.Visible = true;
			
			conn.QueryString = "SELECT [DESCRIPTION] FROM RFSCORINGCUTOFFPERITEM WHERE [ID] = '" + _ddlItemCutOff.SelectedValue.ToString() + "'";
			conn.ExecuteQuery();

			_txtItemName.Text = conn.GetFieldValue("DESCRIPTION");
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			TR_EDIT_REQUEST_ITEM.Visible = false;
		
			conn.QueryString = "UPDATE RFSCORINGCUTOFFPERITEMTEMPLATE SET DESCRIPTION = '" + _txtItemTemplate.Text + "' WHERE [ID] = '" + _lblIdItemTemplate.Text + "'";
			conn.ExecuteQuery();

			BindData("DatGrdItemTemplate","EXEC SCORING_BINDTEMP 'ITEM'");
		}

		private void DatGrdItemTemplate_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					TR_EDIT_REQUEST_ITEM.Visible = true;
					_lblIDDatGrdItemTemplate.Text = e.Item.Cells[0].Text.ToString();

					conn.QueryString = "SELECT DESCRIPTION FROM RFSCORINGCUTOFFPERITEMTEMPLATE WHERE [ID] = '" + _lblIDDatGrdItemTemplate.Text + "'";
					conn.ExecuteQuery();
					_txtItemTemplate.Text = conn.GetFieldValue("DESCRIPTION");

					break;
				case "Delete":
					
					conn.QueryString = "DELETE RFSCORINGCUTOFFPERITEMTEMPLATE WHERE [ID] = '" + _lblIDDatGrdItemTemplate.Text + "'";
					conn.ExecuteQuery();
					BindData("DatGrdItemTemplate","EXEC SCORING_BINDTEMP 'ITEM'");

					break;
			}
		}

	}
}
