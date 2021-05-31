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
	/// Summary description for RFChann_Company.
	/// </summary>
	public partial class RFChann_Company : System.Web.UI.Page
	{
		protected string var_comp, var_kriteria,var_value ;
		protected string fieldnya, tabelnya, value1;
		protected Connection conn;
			
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			conn = (Connection) Session["Connection"];
			
			if (!IsPostBack)
			{
				LBL_SAVEMODE.Text = "0";
				LBL_VALUECODE.Text="0";
				
				var_comp = "select cu_ref, cu_compname from cust_company "+
					"where cu_ref in (select cu_ref from customer where cu_channelcomp = '1') order by cu_compname asc";					
				GlobalTools.fillRefList(DDL_COMPNAME,var_comp, false, conn);


				var_kriteria = "select ch_prm_code, ch_prm_name from RFCHANN_PARAM_LIST where ch_prm_active='1' order by ch_prm_name asc";
				GlobalTools.fillRefList(DDL_KRITERIA,var_kriteria, false, conn);

												
				bindData1();
				bindData2();

			}
			//			LBL_CUREF.Text = DDL_COMPNAME.SelectedValue;
			//			conn.QueryString = "select cu_cif, cu_npwp from customer where cu_ref = '"+LBL_CUREF.Text.Trim()+"'";
			//			conn.ExecuteQuery();
			//			TXT_CIF.Text = conn.GetFieldValue("cu_cif");
			//			TXT_NPWP.Text = conn.GetFieldValue("cu_npwp");
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");		
			
		}

		
		
		private void bindData1()
		{ 
			conn.QueryString="select * from VW_CHANN_COMPANY1 ORDER BY CU_COMPNAME, CH_PRM_NAME,CH_VALUE1,CH_VALUE2 ASC";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_COMP1.DataSource = dt;
			try
			{
				DGR_COMP1.DataBind();
			}
			catch 
			{
				DGR_COMP1.CurrentPageIndex = DGR_COMP1.PageCount - 1;
				DGR_COMP1.DataBind();
			}				
			

			for(int i = 0; i < DGR_COMP1.Items.Count; i++)
			{
				//DGR_COMP1.Items[i].Cells[0].Text = (i+1+(DGR_COMP1.CurrentPageIndex)*DGR_COMP1.PageSize).ToString();
				System.Web.UI.WebControls.LinkButton lbEdit = (System.Web.UI.WebControls.LinkButton) DGR_COMP1.Items[i].Cells[11].FindControl("LB_EDIT");
				System.Web.UI.WebControls.LinkButton lbDelete = (System.Web.UI.WebControls.LinkButton) DGR_COMP1.Items[i].Cells[11].FindControl("LB_DELETE");				
				//System.Web.UI.WebControls.LinkButton lbDrop = (System.Web.UI.WebControls.LinkButton) DG_HOLIDAY.Items[i].Cells[3].FindControl("LBL_DROP");
								
			}
			conn.ClearData();
		
		}


		private void bindData2()
		{
			conn.QueryString="select * from VW_CHANN_COMPANY_PENDING ORDER BY CU_COMPNAME, CH_PRM_NAME,CH_VALUE1,CH_VALUE2 ASC";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_MAKER.DataSource = dt;

			try
			{
				DGR_MAKER.DataBind();
			}
			catch 
			{
				DGR_MAKER.CurrentPageIndex = DGR_MAKER.PageCount - 1;
				DGR_MAKER.DataBind();
			}

			for (int i = 0; i < DGR_MAKER.Items.Count; i++)
			{
				//DGR_MAKER.Items[i].Cells[0].Text = (i+1+(DGR_MAKER.CurrentPageIndex)*DGR_MAKER.PageSize).ToString();
				if (DGR_MAKER.Items[i].Cells[11].Text.Trim() == "0")
				{
					DGR_MAKER.Items[i].Cells[11].Text = "INSERT";
				}
				else if (DGR_MAKER.Items[i].Cells[11].Text.Trim() == "1")
				{
					DGR_MAKER.Items[i].Cells[11].Text = "UPDATE";
				}
				else if (DGR_MAKER.Items[i].Cells[11].Text.Trim() == "2")
				{
					DGR_MAKER.Items[i].Cells[11].Text = "DELETE";
				}
			}
			
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
			this.DGR_COMP1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_COMP1_ItemCommand);
			this.DGR_COMP1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_COMP1_PageIndexChanged);
			this.DGR_MAKER.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_MAKER_ItemCommand);
			this.DGR_MAKER.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_MAKER_PageIndexChanged);

		}
		#endregion

		private void DGR_COMP1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					
					try
					{
						DDL_COMPNAME.SelectedValue = e.Item.Cells[0].Text.Trim();
					}
					catch{}
					try
					{
						DDL_KRITERIA.SelectedValue = e.Item.Cells[4].Text.Trim();
						fillValue();
					}
					catch{}
					try
					{
						
						DDL_VALUE.SelectedValue = e.Item.Cells[9].Text.Trim();
					}
					catch {}

					TXT_CIF.Text = e.Item.Cells[2].Text.Trim();
					TXT_NPWP.Text = e.Item.Cells[3].Text.Trim();
					TXT_MINVALUE.Text = e.Item.Cells[7].Text.Trim();
					TXT_MAXVALUE.Text = e.Item.Cells[8].Text.Trim();
					TXT_SCORE.Text = e.Item.Cells[10].Text.Trim();
					LBL_VALUECODE.Text = e.Item.Cells[6].Text.Trim();
					LBL_SAVEMODE.Text = "1";
					//GlobalTools.popMessage(this,DDL_VALUE.SelectedValue);
					
					cleansTextBox(TXT_CIF);
					cleansTextBox(TXT_MAXVALUE);
					cleansTextBox(TXT_NPWP);
					cleansTextBox(TXT_MINVALUE);
					cleansTextBox(TXT_SCORE);

					DDL_COMPNAME.Enabled = false;
					TXT_CIF.Enabled= false;
					TXT_NPWP.Enabled=false;
					DDL_KRITERIA.Enabled= false;
					
					break;
					
				case "delete":
					LBL_SAVEMODE.Text = "2";
					////conn.QueryString = "EXEC CHANN_COMPANY_PENDING @mode ,@CH_PRM_CODE,@CH_BPR_CUREF,@CH_VALUE_CODE,@CH_VALUE1,@CH_VALUE2,@CH_VALUE3,@CH_SCORE,@PENDINGSTATUS,@MANDATORY)";
					//GlobalTools.popMessage(this,e.Item.Cells[4].Text.Trim());
					conn.QueryString = " EXEC CHANN_COMPANY_PENDING 'INSERT', '"+e.Item.Cells[4].Text.Trim()+"','"+e.Item.Cells[0].Text.Trim()+"','"+
						e.Item.Cells[6].Text.Trim()+"','"+e.Item.Cells[7].Text.Trim()+"','"+e.Item.Cells[8].Text.Trim()+"','"+e.Item.Cells[9].Text.Trim()+"','"+
						e.Item.Cells[10].Text.Trim()+"','"+LBL_SAVEMODE.Text.Trim()+"','1' ";
					conn.ExecuteNonQuery();


					bindData2();
					break;
				
				default:
					// Do nothing.
					break;
			} 		
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			simpan();
		
		}

		
				

		private void simpan()
		{
			//GlobalTools.popMessage(this,LBL_SAVEMODE.Text.Trim());
			conn.QueryString = " EXEC CHANN_COMPANY_PENDING 'INSERT', '"+
				DDL_KRITERIA.SelectedValue+"','"+
				DDL_COMPNAME.SelectedValue+"','"+
				LBL_VALUECODE.Text.Trim()+"','"+
				TXT_MINVALUE.Text.Trim()+"','"+
				TXT_MAXVALUE.Text.Trim()+"','"+
				DDL_VALUE.SelectedItem.Text+"','"+
				TXT_SCORE.Text.Trim()+"','"+
				LBL_SAVEMODE.Text.Trim()+"','1' ";
			conn.ExecuteNonQuery();
			bindData2();
			LBL_SAVEMODE.Text = "0";
			clearTextBoxes();
			
		}


		private void clearTextBoxes()
		{
			DDL_COMPNAME.SelectedValue="";
			TXT_CIF.Text="";
			TXT_NPWP.Text="";
			TXT_MAXVALUE.Text="";
			TXT_MINVALUE.Text="";
			TXT_SCORE.Text="";
			DDL_KRITERIA.SelectedValue="";
			DDL_VALUE.SelectedValue="";
			DDL_COMPNAME.Enabled = true;
			TXT_CIF.Enabled= true;
			TXT_NPWP.Enabled=true;
			DDL_KRITERIA.Enabled= true;

		}

		private void DGR_COMP1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			// Set CurrentPageIndex to the page the user clicked.
			DGR_COMP1.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData1();
		}

		private void DGR_MAKER_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			// Set CurrentPageIndex to the page the user clicked.
			DGR_MAKER.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData2();
		}

		private void DGR_MAKER_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":

					try
					{
						DDL_COMPNAME.SelectedValue = e.Item.Cells[0].Text.Trim();
					}
					catch{}
					try
					{
						DDL_KRITERIA.SelectedValue = e.Item.Cells[4].Text.Trim();
						fillValue();	
					}
					catch{}
					try
					{
						DDL_VALUE.SelectedValue = e.Item.Cells[9].Text.Trim();
					}
					catch {}

					TXT_CIF.Text = e.Item.Cells[2].Text.Trim();
					TXT_NPWP.Text = e.Item.Cells[3].Text.Trim();
					TXT_MINVALUE.Text = e.Item.Cells[7].Text.Trim();
					TXT_MAXVALUE.Text = e.Item.Cells[8].Text.Trim();
					TXT_SCORE.Text = e.Item.Cells[10].Text.Trim();
					LBL_VALUECODE.Text = e.Item.Cells[6].Text.Trim();
					LBL_SAVEMODE.Text = "1";

					cleansTextBox(TXT_CIF);
					cleansTextBox(TXT_MAXVALUE);
					cleansTextBox(TXT_NPWP);
					cleansTextBox(TXT_MINVALUE);
					cleansTextBox(TXT_SCORE);

					
					DDL_COMPNAME.Enabled = false;
					TXT_CIF.Enabled= false;
					TXT_NPWP.Enabled=false;
					DDL_KRITERIA.Enabled= false;
				
					
					break;
					
				case "delete":
					LBL_SAVEMODE.Text = "2";
					////conn.QueryString = "EXEC CHANN_COMPANY_PENDING @mode ,@CH_PRM_CODE,@CH_BPR_CUREF,@CH_VALUE_CODE,@CH_VALUE1,@CH_VALUE2,@CH_VALUE3,@CH_SCORE,@PENDINGSTATUS,@MANDATORY)";
					
					conn.QueryString = " EXEC CHANN_COMPANY_PENDING 'DELETE', '"+e.Item.Cells[4].Text.Trim()+"','"+e.Item.Cells[0].Text.Trim()+"','"+
						e.Item.Cells[6].Text.Trim()+"','"+e.Item.Cells[7].Text.Trim()+"','"+e.Item.Cells[8].Text.Trim()+"','"+e.Item.Cells[9].Text.Trim()+"','"+
						e.Item.Cells[10].Text.Trim()+"','"+LBL_SAVEMODE.Text.Trim()+"','1' ";
					conn.ExecuteNonQuery();
					bindData2();
					clearTextBoxes();
					break;
				
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearTextBoxes();
			LBL_SAVEMODE.Text = "0";
		}

		protected void DDL_COMPNAME_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillValue();
		}

		private void fillValue()
		{
			conn.QueryString = "select ch_prm_isparameter,CH_PRM_FIELD, CH_PRM_RFTABLE from RFCHANN_PARAM_LIST where ch_prm_code = '"+DDL_KRITERIA.SelectedValue+"'";
			try
			{
				conn.ExecuteQuery();
				LBL_VALUECODE.Text = conn.GetFieldValue("ch_prm_isparameter").Trim();
				fieldnya = conn.GetFieldValue("ch_prm_field").Trim();
				tabelnya = conn.GetFieldValue("ch_prm_rftable").Trim();
				
				DDL_VALUE.Items.Add(new ListItem("- PILIH -", ""));
				if (LBL_VALUECODE.Text.Trim()== "0")
				{
					DDL_VALUE.Enabled = false;
					DDL_VALUE.ClearSelection();
					TXT_MINVALUE.Enabled = true;
					TXT_MAXVALUE.Enabled = true;
				}
				else
				{
					DDL_VALUE.Enabled = true;
					
					//var_value = "select "+fieldnya+", "+fieldnya+" from "+tabelnya+" ";
					var_value = "select * from " + tabelnya;
					GlobalTools.fillRefList(DDL_VALUE,var_value, false, conn);
					
					//value1 = DDL_VALUE.SelectedValue.Trim();
					TXT_MINVALUE.Enabled = false;
					TXT_MAXVALUE.Text="";
					TXT_MINVALUE.Text="";
					TXT_MAXVALUE.Enabled = false;
				}
			}
			catch{}
		}

		protected void DDL_KRITERIA_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LBL_CUREF.Text = DDL_COMPNAME.SelectedValue;
			conn.QueryString = "select cu_cif, cu_npwp from customer where cu_ref = '"+LBL_CUREF.Text.Trim()+"'";
			conn.ExecuteQuery();
			TXT_CIF.Text = conn.GetFieldValue("cu_cif");
			TXT_NPWP.Text = conn.GetFieldValue("cu_npwp");
		}


	}
}
