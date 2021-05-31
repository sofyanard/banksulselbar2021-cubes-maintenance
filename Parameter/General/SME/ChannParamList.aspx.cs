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
	/// Summary description for ChannParamList.
	/// </summary>
	public partial class ChannParamList : System.Web.UI.Page
	{
		//////////////////////////////////////////////////////////////////////////
		/// view & stored procedure used in this modul:
		/// VW_CHANN_TABLE
		/// VW_CHANN_FIELD
		/// VW_CHANN_TABLEPARAM
		/// VW_CHANN_RFCHANN_PARAM_LIST
		/// VW_PENDING_RFCHANN_PARAM_LIST
		/// CHANN_RFCHANN_PARAM_LIST_MAKER
		

		protected System.Web.UI.WebControls.DataGrid Datagrid1;

		#region my variables
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		#endregion
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				fillDDL();

//				bindData1();
				bindData2();

				lbl_SAVEMODE.Text = "1";	// default value : Insert Mode
			}

			bindData1();
		}

		#region my methods

		private void fillDDL()
		{
			GlobalTools.fillRefList(ddl_CH_PRM_TABLE,"SELECT * FROM VW_CHANN_TABLE", false, conn);

			GlobalTools.fillRefList(ddl_CH_PRM_RFTABLE,"SELECT * FROM VW_CHANN_TABLEPARAM", false, conn);
		}

		private void bindData1()
		{
			conn.QueryString = "SELECT * FROM VW_CHANN_RFCHANN_PARAM_LIST ";
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

			
			for (int i=0; i < Datagrid1.Items.Count; i++)
			{
				/**************************************************************
				 * Asumsi : (TODO ...)
				 * di datagrid ada 3 linkbutton : LNK_EDIT, LNK_DEL, LNK_UNDEL
				 * 
				 * saat looping di sini ...
				 * 
				 * if (Datagrid1.Items[i].Cells[7].Text.Trim() =="1" ) {		// active = 1
				 *      LinkButton l_undel = (LinkButton) Datagrid1.Items[i].FindControl("LNK_UNDEL");
				 *      l_undel.Visible = false;
				 * }
				 * else {
				 *		LinkButton l_edit = (LinkButton) Datagrid1.Items[i].FindControl("LNK_EDIT");
				 *		LinkButton l_del = (LinkButton) Datagrid1.Items[i].FindControl("LNK_DEL");
				 *		l_edit.Visible = false;
				 *		l_del.Visible = false;
				 * }
				 ***************************************************************/

				  if (Datagrid1.Items[i].Cells[7].Text.Trim() =="0" )
				  {		// active = 0

					  LinkButton l_del = (LinkButton) Datagrid1.Items[i].FindControl("lnk_RfDelete");
					  l_del.CommandName = "Undelete";
					  l_del.Text = "Undelete";

					  LinkButton l_edit = (LinkButton) Datagrid1.Items[i].FindControl("lnk_RfEdit");
					  l_edit.Visible = false;
				  }

//				if (Datagrid1.Items[i].Cells[7].Text.Trim() =="1" )		// active = 1
//				{
//					LinkButton lnk = new LinkButton();
//					lnk.Text = "Edit";
//					lnk.ID = "lnk" + Datagrid1.Items[i].Cells[0].Text.Trim() ;
//					lnk.CommandName = Datagrid1.Items[i].Cells[1].Text.Trim() ;
//					lnk.Click += new System.EventHandler(this.LNK_EDIT_Click);
//
//					LinkButton del = new LinkButton();
//					del.Text = "    Delete";
//					del.ID = "del" + Datagrid1.Items[i].Cells[0].Text.Trim() ;
//					del.CommandName = Datagrid1.Items[i].Cells[1].Text.Trim() ;
//					del.Click += new System.EventHandler(this.LNK_DELETE_Click);
//					
//					Datagrid1.Items[i].Cells[8].Controls.Add(lnk);
//					Datagrid1.Items[i].Cells[8].Controls.Add(new LiteralControl(" "));
//					Datagrid1.Items[i].Cells[8].Controls.Add(del);
//			
//				}
//				else	// active = 0
//				{
//					LinkButton undel = new LinkButton();
//					undel.Text = "Undelete";
//					undel.ID = "del" + Datagrid1.Items[i].Cells[0].Text.Trim() ;
//					undel.CommandName = "undelete";
//					undel.Click += new System.EventHandler(this.LNK_UNDELETE_Click);
//					
//					Datagrid1.Items[i].Cells[8].Controls.Add(undel);
//				}								
			}
		}

		private void LNK_UNDELETE_Click(object sender, System.EventArgs e)
		{
			string id = ((LinkButton) sender).ID;
			lbl_CH_PRM_CODE.Text = id.Substring(3);
			lbl_SAVEMODE.Text = "0";	// update mode
			

			conn.QueryString = "SELECT * FROM VW_CHANN_RFCHANN_PARAM_LIST where CH_PRM_CODE = '"+lbl_CH_PRM_CODE.Text.Trim()+"' ";
			conn.ExecuteQuery();
			string name = conn.GetFieldValue("CH_PRM_NAME");
			string table = conn.GetFieldValue("CH_PRM_TABLE");
			string field = conn.GetFieldValue("CH_PRM_FIELD");
			string reject = conn.GetFieldValue("CH_PRM_REJECTDESC");
			string isparam = conn.GetFieldValue("CH_PRM_ISPARAMETER");
			string rftable = conn.GetFieldValue("CH_PRM_RFTABLE");
			
			
			conn.QueryString = "CHANN_RFCHANN_PARAM_LIST_MAKER '1','" +
				lbl_CH_PRM_CODE.Text.Trim() + "','" + 
				name + "','" + 
				(isparam == "No" ? 0 : 1) + "','','" + 						
				field + "','" + 
				table + "','" + 
				rftable + "','','" + 
				reject + "','','1','" + 
				lbl_SAVEMODE.Text +"'"; 
					
			conn.ExecuteQuery();
			bindData2();
			clearEditBox();
			lbl_CH_PRM_CODE.Text = "";
		}


		private void LNK_DELETE_Click(object sender, System.EventArgs e)
		{
			string id = ((LinkButton) sender).ID;
			lbl_CH_PRM_CODE.Text = id.Substring(3);
			lbl_SAVEMODE.Text = "2";
			

			conn.QueryString = "SELECT * FROM VW_CHANN_RFCHANN_PARAM_LIST where CH_PRM_CODE = '"+lbl_CH_PRM_CODE.Text.Trim()+"' ";
			conn.ExecuteQuery();
			string name = conn.GetFieldValue("CH_PRM_NAME");
			string table = conn.GetFieldValue("CH_PRM_TABLE");
			string field = conn.GetFieldValue("CH_PRM_FIELD");
			string reject = conn.GetFieldValue("CH_PRM_REJECTDESC");
			string isparam = conn.GetFieldValue("CH_PRM_ISPARAMETER");
			string rftable = conn.GetFieldValue("CH_PRM_RFTABLE");
			
			
			conn.QueryString = "CHANN_RFCHANN_PARAM_LIST_MAKER '1','" +
				lbl_CH_PRM_CODE.Text.Trim() + "','" + 
				name + "','" + 
				(isparam == "No" ? 0 : 1) + "','','" + 						
				field + "','" + 
				table + "','" + 
				rftable + "','','" + 
				reject + "','','0','" + 
				lbl_SAVEMODE.Text +"'"; 
					
			conn.ExecuteQuery();
			bindData2();
			clearEditBox();
			lbl_CH_PRM_CODE.Text = "";
		}

		private void LNK_EDIT_Click(object sender, System.EventArgs e)
		{
			string id = ((LinkButton) sender).ID;
			
			//GlobalTools.popMessage(this,"edit euy");
//			TXT_ID.Text = id.Substring(3);
//			TXT_DESC.Text = ((LinkButton) sender).CommandName;
//			activateControlKey(true);
			

			lbl_SAVEMODE.Text = "0";
			lbl_CH_PRM_CODE.Text = id.Substring(3);

			/// ambil data dr tabel bdasarkan CH_PRM_CODE
			/// 
			/// CH_PRM_CODE,CH_PRM_NAME,CH_PRM_ISPARAMETER, CH_PRM_FORMULA, CH_PRM_FIELD,CH_PRM_TABLE,
			/// CH_PRM_RFTABLE,CH_PRM_KONDISI,CH_PRM_REJECTDESC,CH_PRM_TERSEDIADATA, CH_PRM_ACTIVE ,
			/// 
			conn.QueryString = "SELECT * FROM VW_CHANN_RFCHANN_PARAM_LIST where CH_PRM_CODE = '"+lbl_CH_PRM_CODE.Text.Trim()+"' ";
			conn.ExecuteQuery();

			string table = conn.GetFieldValue("CH_PRM_TABLE");
			string field = conn.GetFieldValue("CH_PRM_FIELD");
			string rftable = conn.GetFieldValue("CH_PRM_RFTABLE");

			txt_CH_PRM_NAME.Text = conn.GetFieldValue("CH_PRM_NAME");
			txt_CH_PRM_REJECTDESC.Text = conn.GetFieldValue("CH_PRM_REJECTDESC");
			rbl_CH_PRM_ISPARAMETER.SelectedValue = conn.GetFieldValue("CH_PRM_ISPARAMETER") == "No" ? "0":"1";
			//rbl_CH_PRM_ISPARAMETER.SelectedValue = e.Item.Cells[5].Text.Trim() == "No" ? "0":"1";


			/// isi table -- CH_PRM_TABLE
			///
			//string table = e.Item.Cells[2].Text.Trim() == "&nbsp;" ? "":e.Item.Cells[2].Text.Trim();
			
			conn.QueryString = "SELECT [ID] FROM VW_CHANN_TABLE WHERE [NAME] = '" + table.Trim() + "'";
			conn.ExecuteQuery();
			table = conn.GetFieldValue("ID");
					
			try { ddl_CH_PRM_TABLE.SelectedValue = table; } 
			catch {}

			/// isi field -- CH_PRM_FIELD
			/// 
			GlobalTools.fillRefList(ddl_CH_PRM_FIELD,"SELECT * FROM VW_CHANN_FIELD WHERE [ID] = '" + ddl_CH_PRM_TABLE.SelectedValue +"'" , false, conn);
			//string field = e.Item.Cells[3].Text.Trim() == "&nbsp;" ? "":e.Item.Cells[3].Text.Trim();
			
			conn.QueryString = "SELECT [CODE] FROM VW_CHANN_FIELD WHERE [NAME] = '" + field.Trim() + "'";
			conn.ExecuteQuery();
			field = conn.GetFieldValue("CODE");

			try { ddl_CH_PRM_FIELD.SelectedValue = field; } 
			catch {}

			/// isi table parameter -- CH_PRM_RFTABLE
			/// 
			//string rftable = e.Item.Cells[6].Text.Trim() == "&nbsp;" ? "":e.Item.Cells[6].Text.Trim();
			
			conn.QueryString = "SELECT [ID] FROM VW_CHANN_TABLEPARAM WHERE [NAME] = '" + rftable.Trim() + "'";
			conn.ExecuteQuery();
			rftable = conn.GetFieldValue("ID");
			try { ddl_CH_PRM_RFTABLE.SelectedValue = rftable; } 
			catch {}
		}

		private void bindData2()
		{
		
			conn.QueryString = "SELECT * FROM VW_PENDING_RFCHANN_PARAM_LIST";
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
				if (DataGrid2.Items[i].Cells[7].Text.Trim() == "0")
				{
					DataGrid2.Items[i].Cells[7].Text = "UPDATE";
				}
				else if (DataGrid2.Items[i].Cells[7].Text.Trim() == "1")
				{
					DataGrid2.Items[i].Cells[7].Text = "INSERT";
				}
				else if (DataGrid2.Items[i].Cells[7].Text.Trim() == "2")
				{
					DataGrid2.Items[i].Cells[7].Text = "DELETE";
				}
			} 
		}

		private void clearEditBox()
		{
			fillDDL();
			
			ddl_CH_PRM_FIELD.Items.Clear();

			txt_CH_PRM_NAME.Text = "";
			txt_CH_PRM_REJECTDESC.Text = "";
			

			lbl_CH_PRM_CODE.Text = "";
			lbl_SAVEMODE.Text = "1";
		}

		#endregion

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
			this.Datagrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid1_PageIndexChanged);
			this.DataGrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_ItemCommand);
			this.DataGrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid2_PageIndexChanged);

		}
		#endregion

		protected void btn_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
		}

		protected void rbl_CH_PRM_ISPARAMETER_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( rbl_CH_PRM_ISPARAMETER.SelectedValue == "1" )
				ddl_CH_PRM_RFTABLE.Enabled = true;
			else
				ddl_CH_PRM_RFTABLE.Enabled = false;
		}

		protected void ddl_CH_PRM_TABLE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			GlobalTools.fillRefList(ddl_CH_PRM_FIELD,"SELECT * FROM VW_CHANN_FIELD WHERE [ID] = '" +ddl_CH_PRM_TABLE.SelectedValue +"'" , false, conn);
		}

		protected void btn_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "CHANN_RFCHANN_PARAM_LIST_MAKER '1','" + 
				lbl_CH_PRM_CODE.Text + "','" +
				txt_CH_PRM_NAME.Text + "','" +
				(rbl_CH_PRM_ISPARAMETER.SelectedItem.Text == "No" ? "0" : "1") + "','','" +
				(ddl_CH_PRM_FIELD.SelectedIndex == 0 ? "" : ddl_CH_PRM_FIELD.SelectedItem.Text) + "','" + 
				(ddl_CH_PRM_TABLE.SelectedIndex == 0 ? "" : ddl_CH_PRM_TABLE.SelectedItem.Text) + "','" +
				(ddl_CH_PRM_RFTABLE.SelectedIndex == 0 ? "" : ddl_CH_PRM_RFTABLE.SelectedItem.Text) + "','','" +
				txt_CH_PRM_REJECTDESC.Text + "','','1','" + 

				lbl_SAVEMODE.Text + "'";
			conn.ExecuteQuery();
			bindData2();

			clearEditBox();
		}

		protected void btn_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBox();
		}

		private void Datagrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					lbl_SAVEMODE.Text = "0";
					lbl_CH_PRM_CODE.Text = e.Item.Cells[0].Text.Trim();

					txt_CH_PRM_NAME.Text = e.Item.Cells[1].Text.Trim();
					txt_CH_PRM_REJECTDESC.Text = e.Item.Cells[4].Text.Trim();
					rbl_CH_PRM_ISPARAMETER.SelectedValue = e.Item.Cells[5].Text.Trim() == "No" ? "0":"1";


					/// isi table
					///
					string table = e.Item.Cells[2].Text.Trim() == "&nbsp;" ? "":e.Item.Cells[2].Text.Trim();
					conn.QueryString = "SELECT [ID] FROM VW_CHANN_TABLE WHERE [NAME] = '" + table.Trim() + "'";
					conn.ExecuteQuery();
					table = conn.GetFieldValue("ID");
					
					ddl_CH_PRM_TABLE.SelectedValue = table;

					/// isi field
					/// 
					GlobalTools.fillRefList(ddl_CH_PRM_FIELD,"SELECT * FROM VW_CHANN_FIELD WHERE [ID] = '" + ddl_CH_PRM_TABLE.SelectedValue +"'" , false, conn);
					string field = e.Item.Cells[3].Text.Trim() == "&nbsp;" ? "":e.Item.Cells[3].Text.Trim();
					conn.QueryString = "SELECT [CODE] FROM VW_CHANN_FIELD WHERE [NAME] = '" + field.Trim() + "'";
					conn.ExecuteQuery();
					field = conn.GetFieldValue("CODE");

					ddl_CH_PRM_FIELD.SelectedValue = field;

					/// isi table parameter
					/// 
					string rftable = e.Item.Cells[6].Text.Trim() == "&nbsp;" ? "":e.Item.Cells[6].Text.Trim();
					conn.QueryString = "SELECT [ID] FROM VW_CHANN_TABLEPARAM WHERE [NAME] = '" + rftable.Trim() + "'";
					conn.ExecuteQuery();
					rftable = conn.GetFieldValue("ID");
					ddl_CH_PRM_RFTABLE.SelectedValue = rftable;

					break;
					
				case "delete":

					lbl_SAVEMODE.Text = "2";
					lbl_CH_PRM_CODE.Text = "";

					conn.QueryString = "CHANN_RFCHANN_PARAM_LIST_MAKER '1','" +
                        e.Item.Cells[0].Text.Trim() + "','" + 
						e.Item.Cells[1].Text.Trim() + "','" + 
						(e.Item.Cells[5].Text.Trim() == "No" ? 0 : 1) + "','','" + 
						
						e.Item.Cells[3].Text.Trim() + "','" + 
						e.Item.Cells[2].Text.Trim() + "','" + 
						e.Item.Cells[6].Text.Trim() + "','','" + 

						e.Item.Cells[4].Text.Trim() + "','','0','" + 
						lbl_SAVEMODE.Text +"'"; 
					
					conn.ExecuteQuery();
					bindData2();
					clearEditBox();

					break;

				 // TODO : Undelete function
				case "undelete":

					lbl_SAVEMODE.Text = "0";
					lbl_CH_PRM_CODE.Text = "";

					conn.QueryString = "CHANN_RFCHANN_PARAM_LIST_MAKER '1','" +
						e.Item.Cells[0].Text.Trim() + "','" + 
						e.Item.Cells[1].Text.Trim() + "','" + 
						(e.Item.Cells[5].Text.Trim() == "No" ? 0 : 1) + "','','" + 
						
						e.Item.Cells[3].Text.Trim() + "','" + 
						e.Item.Cells[2].Text.Trim() + "','" + 
						e.Item.Cells[6].Text.Trim() + "','','" + 

						e.Item.Cells[4].Text.Trim() + "','','0','" + 
						lbl_SAVEMODE.Text +"'"; 
					
					conn.ExecuteQuery();
					bindData2();
					clearEditBox();

					break;
				
			} 

		}

		private void DataGrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					lbl_SAVEMODE.Text = "0";
					lbl_CH_PRM_CODE.Text = e.Item.Cells[0].Text.Trim();

					txt_CH_PRM_NAME.Text = e.Item.Cells[1].Text.Trim();
					txt_CH_PRM_REJECTDESC.Text = e.Item.Cells[4].Text.Trim();
					rbl_CH_PRM_ISPARAMETER.SelectedValue = e.Item.Cells[5].Text.Trim() == "No" ? "0":"1";


					/// isi table
					///
					string table = e.Item.Cells[2].Text.Trim() == "&nbsp;" ? "":e.Item.Cells[2].Text.Trim();
					conn.QueryString = "SELECT [ID] FROM VW_CHANN_TABLE WHERE [NAME] = '" + table.Trim() + "'";
					conn.ExecuteQuery();
					table = conn.GetFieldValue("ID");
					
					try { ddl_CH_PRM_TABLE.SelectedValue = table; } 
					catch {}

					/// isi field
					/// 
					GlobalTools.fillRefList(ddl_CH_PRM_FIELD,"SELECT * FROM VW_CHANN_FIELD WHERE [ID] = '" + ddl_CH_PRM_TABLE.SelectedValue +"'" , false, conn);
					string field = e.Item.Cells[3].Text.Trim() == "&nbsp;" ? "":e.Item.Cells[3].Text.Trim();
					conn.QueryString = "SELECT [CODE] FROM VW_CHANN_FIELD WHERE [NAME] = '" + field.Trim() + "'";
					conn.ExecuteQuery();
					field = conn.GetFieldValue("CODE");

					try { ddl_CH_PRM_FIELD.SelectedValue = field; } 
					catch {}

					/// isi table parameter
					/// 
					string rftable = e.Item.Cells[6].Text.Trim() == "&nbsp;" ? "":e.Item.Cells[6].Text.Trim();
					conn.QueryString = "SELECT [ID] FROM VW_CHANN_TABLEPARAM WHERE [NAME] = '" + rftable.Trim() + "'";
					conn.ExecuteQuery();
					rftable = conn.GetFieldValue("ID");
					try { ddl_CH_PRM_RFTABLE.SelectedValue = rftable; } 
					catch {}

					break;
					
				case "delete":
					lbl_CH_PRM_CODE.Text = "";

					conn.QueryString = "CHANN_RFCHANN_PARAM_LIST_MAKER '2','" + e.Item.Cells[0].Text.Trim() + "','','','','','','','','','','',''";
					conn.ExecuteQuery();
					
					bindData2();
					clearEditBox();

					break;
			}  		
		}

		private void Datagrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid1.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData1();	
		
		}

		private void DataGrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			// Set CurrentPageIndex to the page the user clicked.
			DataGrid2.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData2();	
		}
	}
}
