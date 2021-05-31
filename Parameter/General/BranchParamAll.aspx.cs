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

namespace CuBES_Maintenance.Parameter.General
{
	/// <summary>
	/// Summary description for BranchParamAll.
	/// </summary>
	public partial class BranchParamAll : System.Web.UI.Page
	{
		protected Connection conn,connsme,conncons,conncc;
			
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn();
			InitialControls();
			if (!IsPostBack)
			{
				FillFixedDDL();
				FillDDL();
				ViewExistingData();
			}
			ViewPendingData();
			this.DGR_EXISTING_CONNCC.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_EXISTING_CONNCC_PageIndexChanged);
			this.DGR_EXISTING_SME.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_EXISTING_SME_PageIndexChanged);
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			
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
			this.DGR_EXISTING_CONNCC.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_CONNCC_ItemCommand);
			this.DGR_EXISTING_CONNCC.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_CONNCC_PageIndexChanged);
			this.DGR_EXISTING_SME.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_SME_ItemCommand);
			this.DGR_EXISTING_SME.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_SME_PageIndexChanged);
			this.DGR_REQUEST_CONNCC.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_CONNCC_ItemCommand);
			this.DGR_REQUEST_CONNCC.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_CONNCC_PageIndexChanged);
			this.DGR_REQUEST_SME.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_SME_ItemCommand);
			this.DGR_REQUEST_SME.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_SME_PageIndexChanged);

		}
		#endregion

		private void FillFixedDDL()
		{
			DDL_REGIONAL_ID.Items.Clear();
			DDL_REGIONAL_ID.Items.Add(new ListItem("- SELECT -",""));
			DDL_REGIONAL_ID.Items.Add(new ListItem("Yes","1"));
			DDL_REGIONAL_ID.Items.Add(new ListItem("No","0"));
			DDL_ISBOOKINGBRANCH.Items.Clear();
			DDL_ISBOOKINGBRANCH.Items.Add(new ListItem("- SELECT -",""));
			DDL_ISBOOKINGBRANCH.Items.Add(new ListItem("Yes","1"));
			DDL_ISBOOKINGBRANCH.Items.Add(new ListItem("No","0"));
			DDL_BR_ISBRANCH.Items.Clear();
			DDL_BR_ISBRANCH.Items.Add(new ListItem("- SELECT -",""));
			DDL_BR_ISBRANCH.Items.Add(new ListItem("Branch","1"));
			DDL_BR_ISBRANCH.Items.Add(new ListItem("Not Branch","0"));
		}

		private void SetDBConn()
		{
			string DB_NAMA,DB_IP,DB_LOGINID,DB_LOGINPWD;
			//SME Conn
			conn.QueryString = "select * from VW_GETCONN where MODULEID='01'";
			conn.ExecuteQuery();
			DB_NAMA		= conn.GetFieldValue("DB_NAMA");
			DB_IP		= conn.GetFieldValue("DB_IP");
			DB_LOGINID	= conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			connsme = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
			conn.ClearData();
			//Credit Card Conn
			conn.QueryString = "select * from VW_GETCONN where MODULEID='20'";
			conn.ExecuteQuery();
			DB_NAMA		= conn.GetFieldValue("DB_NAMA");
			DB_IP		= conn.GetFieldValue("DB_IP");
			DB_LOGINID	= conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncc = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
			conn.ClearData();
			//Consumer
			conn.QueryString = "select * from VW_GETCONN where MODULEID='40'";
			conn.ExecuteQuery();
			DB_NAMA		= conn.GetFieldValue("DB_NAMA");
			DB_IP		= conn.GetFieldValue("DB_IP");
			DB_LOGINID	= conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncons = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
			conn.ClearData();
		}

		private void FillDDL()
		{
			//march 4th 2005
			//GlobalTools.fillRefList(this.DDL_CITYID,"select cityid, cityname from rfcity where active = '1' ",connsme);
				
			if (this.LBL_STA.Text == "0") // SME
			{
				try
				{
					GlobalTools.fillRefList(this.DDL_CITYID,"select cityid, cityname from rfcity where active = '1' ",connsme);
					/*
					GlobalTools.fillRefList(this.DDL_CBC_CODE, "select BRANCH_CODE,BRANCH_CODE + ' - ' + BRANCH_NAME AS BRANCH_NAME from RFBranch where BRANCH_TYPE = '3'", connsme);
					GlobalTools.fillRefList(this.DDL_BR_CCOBRANCH_SME, "select BRANCH_CODE,BRANCH_CODE + ' - ' + BRANCH_NAME AS BRANCH_NAME from RFBranch where BRANCH_TYPE = '3' or BRANCH_TYPE = '4'", connsme);
					*/
					GlobalTools.fillRefList(this.DDL_CBC_CODE, "exec PARAM_GENERAL_RFBRANCH_GETBRANCH 'CBC'", connsme);
					GlobalTools.fillRefList(this.DDL_BR_CCOBRANCH_SME, "exec PARAM_GENERAL_RFBRANCH_GETBRANCH 'CCO'", connsme);
					GlobalTools.fillRefList(this.DDL_REGIONAL_ID,"select AREAID, AREANAME from RFAREA ",connsme);
				} 
				catch{}
			}
			else 
			{
				try 
				{
					GlobalTools.fillRefList(this.DDL_CITYID,"select city_id,city_name from city ",conncons);
					//Di Consumer tidak ada CBC dan CCO, yang ada Comm. Branch
					GlobalTools.fillRefList(this.DDL_BR_CCOBRANCH_CONCC,"select BRANCH_CODE,BRANCH_CODE + ' - ' + BRANCH_NAME AS BRANCH_NAME from RFBranch where BRANCH_TYPE = '0'",conncons);
					GlobalTools.fillRefList(this.DDL_REGIONAL_ID,"select REGIONAL_ID, REGIONAL_NAME from RFREGIONAL ",conncons);
				} 
				catch {}
			}
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
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

		private string getYesNoStatus(string YesMode) 
		{
			string status = "";			
			switch (YesMode)
			{
				case "1":
					status = "Yes";
					break;
				case "0":
					status = "No";
					break;
				default:
					status = "No";
					break;
			}
			return status;
		}

		private void GetConsumerArea()
		{
			conncons.QueryString = "select * from VW_PARAM_RFBRANCHall_CITY where CITY_ID='" + this.DDL_CITYID.SelectedValue.Trim()+ "'";
			try 
			{
				conncons.ExecuteQuery();
				this.TXT_AREAID.Text = conncons.GetFieldValue("AREA_NAME");
				this.LBL_CONSAREA_ID.Text = conncons.GetFieldValue("AREA_ID");
			} 
			catch {}
		}

		private void ViewExistingConCC()
		{
			conncons.QueryString = "select * from VW_PARAM_RFBRANCHall_CONCC";
			try
			{
				conncons.ExecuteQuery();
			} 
			catch 
			{
				GlobalTools.popMessage(this,"Please attach Consumer/Credit Card Database");
				return;
			}
			this.DGR_EXISTING_CONNCC.Visible = true;
			DataTable dt = new DataTable();
			dt = conncons.GetDataTable().Copy();
			DGR_EXISTING_CONNCC.DataSource = dt;
			try
			{
				DGR_EXISTING_CONNCC.DataBind();
			}
			catch
			{
				DGR_EXISTING_CONNCC.CurrentPageIndex = DGR_EXISTING_CONNCC.PageCount - 1;
				DGR_EXISTING_CONNCC.DataBind();
			}
		}

		private void ViewExistingSME()
		{
			connsme.QueryString = "select * from VW_PARAM_RFBRANCHall_SME";
			try
			{
				connsme.ExecuteQuery();
			} 
			catch 
			{
				GlobalTools.popMessage(this,"Please attach SME database.");
				return;
			}
			this.DGR_EXISTING_SME.Visible = true;
			DataTable dt = new DataTable();
			dt = connsme.GetDataTable().Copy();
			DGR_EXISTING_SME.DataSource = dt;
			try
			{
				DGR_EXISTING_SME.DataBind();
			}
			catch
			{
				DGR_EXISTING_SME.CurrentPageIndex = DGR_EXISTING_SME.PageCount - 1;
				DGR_EXISTING_SME.DataBind();
			}

			for (int i=0;i<this.DGR_EXISTING_SME.Items.Count;i++)
			{
				this.DGR_EXISTING_SME.Items[i].Cells[18].Text = getYesNoStatus(this.DGR_EXISTING_SME.Items[i].Cells[18].Text.Trim());
			}
		}

		private void ViewPendingConsumerData()
		{
			conncons.QueryString = "select * from VW_PARAM_PENDING_RFBRANCHall_CONCC";
			try
			{
				conncons.ExecuteQuery();
			} 
			catch 
			{
				GlobalTools.popMessage(this,"Please attach Consumer/Credit Card atabase");
				return;
			}
			this.DGR_REQUEST_CONNCC.Visible = true;
			DataTable dt = new DataTable();
			dt = conncons.GetDataTable().Copy();
			this.DGR_REQUEST_CONNCC.DataSource = dt;
			try
			{
				this.DGR_REQUEST_CONNCC.DataBind();
			}
			catch
			{
				this.DGR_REQUEST_CONNCC.CurrentPageIndex = this.DGR_REQUEST_CONNCC.PageCount - 1;
				this.DGR_REQUEST_CONNCC.DataBind();
			}

			
			for (int i=0;i<this.DGR_REQUEST_CONNCC.Items.Count;i++)
			{
				this.DGR_REQUEST_CONNCC.Items[i].Cells[21].Text =this.getPendingStatus(this.DGR_REQUEST_CONNCC.Items[i].Cells[21].Text.Trim());
			}
		}

		private void ViewPendingSMEData()
		{
			connsme.QueryString = "select * from VW_PARAM_PENDING_RFBRANCHall_SME";
			try
			{
				connsme.ExecuteQuery();
			} 
			catch
			{
				GlobalTools.popMessage(this,"Please attach SME database");
				return;
			}
			this.DGR_REQUEST_SME.Visible = true;
			DataTable dt = new DataTable();
			dt = connsme.GetDataTable().Copy();
			this.DGR_REQUEST_SME.DataSource = dt;
			try
			{
				this.DGR_REQUEST_SME.DataBind();
			}
			catch
			{
				this.DGR_REQUEST_SME.CurrentPageIndex = this.DGR_REQUEST_SME.PageCount - 1;
				this.DGR_REQUEST_SME.DataBind();
			}

			for (int i=0;i<this.DGR_REQUEST_SME.Items.Count;i++)
			{
				this.DGR_REQUEST_SME.Items[i].Cells[18].Text =this.getYesNoStatus(this.DGR_REQUEST_SME.Items[i].Cells[18].Text.Trim());
				this.DGR_REQUEST_SME.Items[i].Cells[20].Text =this.getPendingStatus(this.DGR_REQUEST_SME.Items[i].Cells[20].Text.Trim());
			}
		}

		private void ViewExistingData()
		{
			if (this.LBL_STA.Text !="0")//cons/loscc
				ViewExistingConCC();
			else
				ViewExistingSME();
		}

		private void ViewPendingData()
		{
			if (this.LBL_STA.Text !="0")//cons/loscc
				ViewPendingConsumerData();
			else
				ViewPendingSMEData();
		}

		private void InitialControls()
		{
			this.LBL_STA.Text	= this.RBL_MODULE.SelectedValue.Trim();
			if (this.RBL_MODULE.SelectedValue == "0")//SME
			{
				this.RBL_BRANCH_TYPE_SME.Visible	= true;
				this.RBL_BRANCH_TYPE_CONCC.Visible	= false;
				this.LBL_FAX.Visible				= true;
				this.LBL_CCOBRANCH.Visible			= true;
				this.LBL_COMBRANCH.Visible			= false;
				this.LBL_REGION_CONS.Visible		= false;
				this.LBL_AREA_SME.Visible			= true;
				this.TXT_BR_PHNFAX.Visible			= true;
				this.TXT_PHONEAREA.Visible			= false;
				this.TXT_PHONE.Visible				= false;
				this.TXT_PHONEEXT.Visible			= false;
				this.TR_AREA.Visible				= false;
				this.TR_ISBOOKINGBRANCH.Visible		= true;
				this.TR_CBC.Visible					= true;
				this.TR_BRANCHAREA.Visible			= true;
				this.DGR_EXISTING_SME.Visible		= true;
				this.DGR_EXISTING_CONNCC.Visible	= false;
				this.DGR_REQUEST_SME.Visible		= true;
				this.DGR_REQUEST_CONNCC.Visible		= false;
				this.DDL_BR_CCOBRANCH_CONCC.Visible	= false;
				this.DDL_BR_CCOBRANCH_SME.Visible	= true;
				TXT_CD_SIBS.ReadOnly = true;
			} 
			else if (this.RBL_MODULE.SelectedValue == "1")
			{
				this.RBL_BRANCH_TYPE_SME.Visible	= false;
				this.RBL_BRANCH_TYPE_CONCC.Visible	= true;
				this.LBL_FAX.Visible				= false;
				this.LBL_CCOBRANCH.Visible			= false;
				this.LBL_COMBRANCH.Visible			= true;
				this.LBL_REGION_CONS.Visible		= true;
				this.LBL_AREA_SME.Visible			= false;
				this.TXT_BR_PHNFAX.Visible			= false;
				this.TXT_PHONEAREA.Visible			= true;
				this.TXT_PHONE.Visible				= true;
				this.TXT_PHONEEXT.Visible			= true;
				this.TR_AREA.Visible				= true;
				this.TR_ISBOOKINGBRANCH.Visible		= false;
				this.TR_CBC.Visible					= false;
				this.TR_BRANCHAREA.Visible			= false;
				this.DGR_EXISTING_SME.Visible		= false;
				this.DGR_EXISTING_CONNCC.Visible	= true;
				this.DGR_REQUEST_SME.Visible		= false;
				this.DGR_REQUEST_CONNCC.Visible		= true;
				this.DDL_BR_CCOBRANCH_CONCC.Visible	= true;
				this.DDL_BR_CCOBRANCH_SME.Visible	= false;
				TXT_CD_SIBS.ReadOnly = false;
			}
		}

		protected void RBL_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillDDL();
			InitialControls();
			ClearBoxes();
			ViewExistingData();
		}

		private void activateControlKey(bool isReadOnly) 
		{
			this.TXT_BRANCH_CODE.ReadOnly = isReadOnly;
		}

		private void ClearBoxes()
		{
			this.TXT_BRANCH_CODE.Text			= "";
			this.TXT_BR_ADDR.Text				= "";
			this.TXT_AREAID.Text				= "";
			this.TXT_BR_PHNFAX.Text				= "";
			this.TXT_BR_ZIPCODE.Text			= "";
			this.TXT_BRANCH_NAME.Text			= "";
			this.TXT_CD_SIBS.Text				= "";
			this.TXT_PHONE.Text					= "";
			this.TXT_PHONEAREA.Text				= "";
			this.TXT_PHONEEXT.Text				= "";
			this.TXT_BR_BRANCH_AREA.Text		= "";
			try{this.DDL_BR_CCOBRANCH_SME.SelectedValue = "";} 
			catch{}
			try{this.DDL_BR_CCOBRANCH_CONCC.SelectedValue = "";} 
			catch{}
			try{this.DDL_BR_ISBRANCH.SelectedValue	= "";}
			catch{}
			try{this.DDL_CBC_CODE.SelectedValue		= "";}
			catch{}
			try{this.DDL_ISBOOKINGBRANCH.SelectedValue	= "";}
			catch{}
			try{this.DDL_CITYID.SelectedValue			= "";}
			catch{}
			try{this.DDL_REGIONAL_ID.SelectedValue		= "";}
			catch{}
			try{this.RBL_BRANCH_TYPE_CONCC.SelectedValue	= "2";}
			catch{}
			try{this.RBL_BRANCH_TYPE_SME.SelectedValue		= "5";}
			catch{}
			activateControlKey(false);
		}

		private void GetDataOtherModule(string module, string branch_code)
		{
			if (module=="01")
			{
				connsme.QueryString="select BRANCH_TYPE,BR_PHNFAX, BR_BRANCHAREA,BR_ISBOOKINGBRANCH,BR_CCOBRANCH from RFBRANCH where BRANCH_CODE='" + branch_code + "'";
				try 
				{
					connsme.ExecuteQuery();
				} 
				catch 
				{
					GlobalTools.popMessage(this,"Please attach SME database.");
					return;
				}
				this.LBL_BRANCH_TYPE_SME.Text			= connsme.GetFieldValue("BRANCH_TYPE");
				this.LBL_BR_CCOBRANCH_SME.Text			= connsme.GetFieldValue("BR_CCOBRANCH");
				this.LBL_BR_PHNFAX.Text					= connsme.GetFieldValue("BR_PHNFAX");
				this.LBL_BR_ISBOOKINGBRANCH.Text		= connsme.GetFieldValue("BR_ISBOOKINGBRANCH");
				this.LBL_BR_BRANCHAREA.Text				= connsme.GetFieldValue("BR_BRANCHAREA");
				
			} 
			else
			{
				conncons.QueryString="select BRANCH_TYPE,BR_CCOBRANCH,PHONEAREA,PHONE,PHONEEXT,AREA_ID from RFBRANCH where BRANCH_CODE='" + branch_code + "'";
				try
				{
					conncons.ExecuteQuery();
				} 
				catch
				{
					GlobalTools.popMessage(this,"Please attach Consumer/Credit Card database");
					return;
				}
				this.LBL_BRANCH_TYPE_CONCC.Text		= conncons.GetFieldValue("BRANCH_TYPE");
				this.LBL_BR_CCOBRANCH_CONCC.Text	= conncons.GetFieldValue("BR_CCOBRANCH");
				this.LBL_PHONE1.Text				= conncons.GetFieldValue("PHONE");
				this.LBL_PHONEAREA.Text				= conncons.GetFieldValue("PHONEAREA");
				this.LBL_PHONEEXT.Text				= conncons.GetFieldValue("PHONEEXT");
				this.LBL_AREAID.Text				= conncons.GetFieldValue("AREA_ID");
			}
			
		}

		private void DGR_EXISTING_CONNCC_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					FillDDL();
					this.TXT_BRANCH_CODE.Text			= CleansText(e.Item.Cells[0].Text.Trim());
					this.TXT_CD_SIBS.Text				= CleansText(e.Item.Cells[1].Text.Trim());
					this.TXT_BRANCH_NAME.Text			= CleansText(e.Item.Cells[2].Text.Trim());
					try{this.DDL_CITYID.SelectedValue		= CleansText(e.Item.Cells[3].Text.Trim());}
					catch{}
					//this.TXT_AREAID.Text					= CleansText(e.Item.Cells[5].Text.Trim());
					try{this.DDL_REGIONAL_ID.SelectedValue	= CleansText(e.Item.Cells[7].Text.Trim());}
					catch{}
					this.TXT_BR_ADDR.Text				= CleansText(e.Item.Cells[9].Text.Trim());
					this.TXT_BR_ZIPCODE.Text			= CleansText(e.Item.Cells[10].Text.Trim());
					this.TXT_PHONEAREA.Text				= CleansText(e.Item.Cells[11].Text.Trim());				
					this.TXT_PHONE.Text					= CleansText(e.Item.Cells[12].Text.Trim());
					this.TXT_PHONEEXT.Text				= CleansText(e.Item.Cells[13].Text.Trim());
					try{this.DDL_BR_ISBRANCH.SelectedValue	= CleansText(e.Item.Cells[14].Text.Trim());}
					catch{}
					try{this.RBL_BRANCH_TYPE_CONCC.SelectedValue	= CleansText(e.Item.Cells[16].Text.Trim());}
					catch{}
					try{this.DDL_CBC_CODE.SelectedValue				= CleansText(e.Item.Cells[18].Text.Trim());}
					catch{}
					try{this.DDL_BR_CCOBRANCH_CONCC.SelectedValue	= CleansText(e.Item.Cells[19].Text.Trim());}
					catch{}
					/*************/
					GetDataOtherModule("01",TXT_BRANCH_CODE.Text.Trim());

					try{this.RBL_BRANCH_TYPE_SME.SelectedValue	= this.LBL_BRANCH_TYPE_SME.Text.Trim();}
					catch{}
					if (RBL_BRANCH_TYPE_SME.SelectedValue == "3") DDL_CBC_CODE.Enabled = false;

					try{this.DDL_ISBOOKINGBRANCH.SelectedValue	= this.LBL_BR_ISBOOKINGBRANCH.Text.Trim();}
					catch{}

					this.TXT_BR_PHNFAX.Text					= this.LBL_BR_PHNFAX.Text.Trim();
					this.TXT_BR_BRANCH_AREA.Text			= this.LBL_BR_BRANCHAREA.Text.Trim();
					
					try{this.DDL_BR_CCOBRANCH_SME.SelectedValue	= this.LBL_BR_CCOBRANCH_SME.Text.Trim();}
					catch{}

					/*************/
					GetConsumerArea();
					activateControlKey(true);
					break;
				case "delete":
				
					string CD_SIBS ,BRANCH_CODE ,BRANCH_NAME;
					string BRANCH_TYPE_SME, BRANCH_TYPE_CONCC /*,BR_APREGNO */,BR_ADDR /*,BR_APREGNODATE*/;
					string BR_BRANCHAREA ,BR_CCOBRANCH_CONCC,BR_CCOBRANCH_SME /*,BR_CUREF,BR_CUREFDATE*/;
					string BR_ZIPCODE ,CBC_CODE ,AREAID,CITYID;
					string /*HUB_CODE ,*/PHONE,PHONEAREA ,PHONEEXT;
					string REGIONAL_ID /*,ACTIVE */,BR_ISBRANCH;
					string BR_PHNFAX,BR_ISBOOKINGBRANCH;
			
					BRANCH_CODE			= CleansText(e.Item.Cells[0].Text.Trim());
					CD_SIBS				= CleansText(e.Item.Cells[1].Text.Trim());
					BRANCH_NAME			= CleansText(e.Item.Cells[2].Text.Trim());
					CITYID				= CleansText(e.Item.Cells[3].Text.Trim());
					AREAID				= CleansText(e.Item.Cells[5].Text.Trim());
					REGIONAL_ID			= CleansText(e.Item.Cells[7].Text.Trim());
					//BR_BRANCHAREA		= CleansText(e.Item.Cells[7].Text.Trim());
					BR_ADDR				= CleansText(e.Item.Cells[9].Text.Trim());
					BR_ZIPCODE			= CleansText(e.Item.Cells[10].Text.Trim());
					//BR_PHNFAX			= CleansText(e.Item.Cells[0].Text.Trim());
					PHONEAREA			= CleansText(e.Item.Cells[11].Text.Trim());				
					PHONE				= CleansText(e.Item.Cells[12].Text.Trim());
					PHONEEXT			= CleansText(e.Item.Cells[13].Text.Trim());
					BR_ISBRANCH			= CleansText(e.Item.Cells[14].Text.Trim());
					BRANCH_TYPE_CONCC	= CleansText(e.Item.Cells[16].Text.Trim());
					CBC_CODE				= CleansText(e.Item.Cells[18].Text.Trim());
					BR_CCOBRANCH_CONCC		= CleansText(e.Item.Cells[19].Text.Trim());
					//ISBOOKINGBRANCH		= CleansText(e.Item.Cells[0].Text.Text.Trim());
					/**********/
					GetDataOtherModule("01",BRANCH_CODE);
					BRANCH_TYPE_SME			= LBL_BRANCH_TYPE_SME.Text.Trim();
					BR_BRANCHAREA			= LBL_BR_BRANCHAREA.Text.Trim();	
					BR_PHNFAX				= LBL_BR_PHNFAX.Text.Trim();
					BR_ISBOOKINGBRANCH			= LBL_BR_ISBOOKINGBRANCH.Text.Trim();
					BR_CCOBRANCH_SME		= LBL_BR_CCOBRANCH_SME.Text.Trim();	
							
					/**********/
		
					conncons.QueryString = "exec PARAM_GENERAL_RFBRANCH_ALL_MAKER  '2','" +
						CD_SIBS + "','" +BRANCH_CODE + "','" +BRANCH_NAME + "','" +
						BRANCH_TYPE_CONCC + "',null,'" +BR_ADDR + "',null,'" +
						BR_BRANCHAREA + "','" +BR_CCOBRANCH_CONCC + "',null,null,'" +
						BR_ZIPCODE + "','" +CBC_CODE + "','" +AREAID+ "','" +CITYID + "'," +
						"null,'" +PHONE+ "','" +PHONEAREA + "','" +PHONEEXT + "','" +
						REGIONAL_ID + "','1','" +BR_ISBRANCH + "','"+BR_ISBOOKINGBRANCH+"','"+BR_PHNFAX+"'";
					try
					{
						conncons.ExecuteNonQuery();
					} 
					catch{}

					conncc.QueryString = "exec PARAM_GENERAL_RFBRANCH_ALL_MAKER  '2','" +
						CD_SIBS + "','" +BRANCH_CODE + "','" +BRANCH_NAME + "','" +
						BRANCH_TYPE_CONCC + "',null,'" +BR_ADDR + "',null,'" +
						BR_BRANCHAREA + "','" +BR_CCOBRANCH_CONCC + "',null,null,'" +
						BR_ZIPCODE + "','" +CBC_CODE + "','" +AREAID+ "','" +CITYID + "'," +
						"null,'" +PHONE+ "','" +PHONEAREA + "','" +PHONEEXT + "','" +
						REGIONAL_ID + "','1','" +BR_ISBRANCH + "','"+BR_ISBOOKINGBRANCH+"','"+BR_PHNFAX+"'";
					try
					{
						conncc.ExecuteNonQuery();
					} 
					catch{}

					//SME.area=Cons.Region
					connsme.QueryString = "exec PARAM_GENERAL_RFBRANCH_ALL_MAKER  '2','" +
						CD_SIBS + "','" +BRANCH_CODE + "','" +BRANCH_NAME + "','" +
						BRANCH_TYPE_SME + "',null,'" +BR_ADDR + "',null,'" +
						BR_BRANCHAREA + "','" +BR_CCOBRANCH_SME + "',null,null,'" +
						BR_ZIPCODE + "','" +CBC_CODE + "','" +AREAID+ "','" +CITYID + "'," +
						"null,'" +PHONE+ "','" +PHONEAREA + "','" +PHONEEXT + "','" +
						REGIONAL_ID + "','1','" +BR_ISBRANCH + "','"+BR_ISBOOKINGBRANCH+"','"+BR_PHNFAX+"'";
					try
					{
						connsme.ExecuteNonQuery();
					} 
					catch{}

					
					//CuBES-Maintenance
					conn.QueryString = "exec PARAM_GENERAL_RFBRANCH_ALL_MAKER  '2','" +
						CD_SIBS + "','" +BRANCH_CODE + "','" +BRANCH_NAME + "','" +
						BRANCH_TYPE_SME + "',null,'" +BR_ADDR + "',null,'" +
						BR_BRANCHAREA + "','" +BR_CCOBRANCH_SME + "',null,null,'" +
						BR_ZIPCODE + "','" +CBC_CODE + "','" +AREAID+ "','" +CITYID + "'," +
						"null,'" +PHONE+ "','" +PHONEAREA + "','" +PHONEEXT + "','" +
						REGIONAL_ID + "','1','" +BR_ISBRANCH + "','"+BR_ISBOOKINGBRANCH+"','"+BR_PHNFAX+"'";
					try
					{
						conn.ExecuteNonQuery();
					} 
					catch {}
						ViewPendingData();
					
					break;
				default :
					break;
			}
		}

		private void DGR_EXISTING_CONNCC_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING_CONNCC.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_EXISTING_SME_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					FillDDL();
					this.TXT_BRANCH_CODE.Text				= CleansText(e.Item.Cells[0].Text.Trim());
					this.TXT_CD_SIBS.Text					= CleansText(e.Item.Cells[1].Text.Trim());
					this.TXT_BRANCH_NAME.Text				= CleansText(e.Item.Cells[2].Text.Trim());
					try{this.DDL_CITYID.SelectedValue		= CleansText(e.Item.Cells[3].Text.Trim());}
					catch{}
					//this.TXT_AREAID.Text				= CleansText(e.Item.Cells[5].Text.Trim());
					try{this.DDL_REGIONAL_ID.SelectedValue	= CleansText(e.Item.Cells[5].Text.Trim());}
					catch{}
					this.TXT_BR_ADDR.Text					= CleansText(e.Item.Cells[7].Text.Trim());
					this.TXT_BR_BRANCH_AREA.Text			= CleansText(e.Item.Cells[8].Text.Trim());
					this.TXT_BR_ZIPCODE.Text				= CleansText(e.Item.Cells[9].Text.Trim());
					this.TXT_BR_PHNFAX.Text					= CleansText(e.Item.Cells[10].Text.Trim());
					//this.TXT_PHONEAREA.Text				= CleansText(e.Item.Cells[11].Text.Trim());				
					//this.TXT_PHONE.Text					= CleansText(e.Item.Cells[12].Text.Trim());
					//this.TXT_PHONEEXT.Text				= CleansText(e.Item.Cells[13].Text.Trim());
					try{this.DDL_BR_ISBRANCH.SelectedValue	= CleansText(e.Item.Cells[11].Text.Trim());}
					catch{}
					try{this.RBL_BRANCH_TYPE_SME.SelectedValue		= CleansText(e.Item.Cells[13].Text.Trim());}
					catch{}
					try{this.DDL_CBC_CODE.SelectedValue				= CleansText(e.Item.Cells[15].Text.Trim());}
					catch{}
					try{this.DDL_BR_CCOBRANCH_SME.SelectedValue			= CleansText(e.Item.Cells[16].Text.Trim());}
					catch{}
					try {this.DDL_ISBOOKINGBRANCH.SelectedValue			= CleansText(e.Item.Cells[17].Text.Trim());}
					catch{}
					/**********/
					GetDataOtherModule("40",TXT_BRANCH_CODE.Text.Trim());
					try
					{
						this.RBL_BRANCH_TYPE_CONCC.SelectedValue    = this.LBL_BRANCH_TYPE_CONCC.Text;
					} 
					catch {}
					try
					{
						this.DDL_BR_CCOBRANCH_CONCC.SelectedValue	= this.LBL_BR_CCOBRANCH_CONCC.Text;
					} 
					catch{}
					//this.TXT_AREAID.Text						= this.LBL_AREAID.Text;
					this.TXT_PHONE.Text							= this.LBL_PHONE1.Text;
					this.TXT_PHONEAREA.Text						= this.LBL_PHONEAREA.Text;
					this.TXT_PHONEEXT.Text						= this.LBL_PHONEEXT.Text;
					GetConsumerArea();
					/**********/
					activateControlKey(true);
					break;
				case "delete":
					
					string CD_SIBS ,BRANCH_CODE ,BRANCH_NAME;
					string BRANCH_TYPE_SME,BRANCH_TYPE_CONCC /*,BR_APREGNO */,BR_ADDR /*,BR_APREGNODATE*/;
					string BR_BRANCHAREA ,BR_CCOBRANCH_CONCC,BR_CCOBRANCH_SME /*,BR_CUREF,BR_CUREFDATE*/;
					string BR_ZIPCODE ,CBC_CODE ,AREAID,CITYID;
					string /*HUB_CODE ,*/PHONE,PHONEAREA ,PHONEEXT;
					string REGIONAL_ID /*,ACTIVE */,BR_ISBRANCH;
					string BR_PHNFAX,BR_ISBOOKINGBRANCH;
			
					BRANCH_CODE				= CleansText(e.Item.Cells[0].Text.Trim());
					CD_SIBS					= CleansText(e.Item.Cells[1].Text.Trim());
					BRANCH_NAME				= CleansText(e.Item.Cells[2].Text.Trim());
					CITYID					= CleansText(e.Item.Cells[3].Text.Trim());
					//AREAID					= "";
					REGIONAL_ID				= CleansText(e.Item.Cells[5].Text.Trim());
					BR_ADDR					= CleansText(e.Item.Cells[7].Text.Trim());
					BR_BRANCHAREA			= CleansText(e.Item.Cells[8].Text.Trim());
					BR_ZIPCODE				= CleansText(e.Item.Cells[9].Text.Trim());
					BR_PHNFAX				= CleansText(e.Item.Cells[10].Text.Trim());
					//PHONEAREA				= "";
					//PHONE					= "";
					//PHONEEXT				= "";
					BR_ISBRANCH				= CleansText(e.Item.Cells[11].Text.Trim());
					BRANCH_TYPE_SME			= CleansText(e.Item.Cells[13].Text.Trim());
					CBC_CODE				= CleansText(e.Item.Cells[15].Text.Trim());
					BR_CCOBRANCH_SME		= CleansText(e.Item.Cells[16].Text.Trim());
					BR_ISBOOKINGBRANCH		= CleansText(e.Item.Cells[17].Text.Trim());
					/**********/
					GetDataOtherModule("40",BRANCH_CODE);
					BRANCH_TYPE_CONCC		= this.LBL_BRANCH_TYPE_CONCC.Text;
					BR_CCOBRANCH_CONCC		= this.LBL_BR_CCOBRANCH_CONCC.Text;
					AREAID					= this.LBL_AREAID.Text;
					PHONE					= this.LBL_PHONE1.Text;
					PHONEAREA				= this.LBL_PHONEAREA.Text;
					PHONEEXT	     		= this.LBL_PHONEEXT.Text;
					/**********/

					conncons.QueryString = "exec PARAM_GENERAL_RFBRANCH_ALL_MAKER  '2','" +
						CD_SIBS + "','" +BRANCH_CODE + "','" +BRANCH_NAME + "','" +
						BRANCH_TYPE_CONCC + "',null,'" +BR_ADDR + "',null,'" +
						BR_BRANCHAREA + "','" +BR_CCOBRANCH_CONCC + "',null,null,'" +
						BR_ZIPCODE + "','" +CBC_CODE + "','" +AREAID+ "','" +CITYID + "'," +
						"null,'" +PHONE+ "','" +PHONEAREA + "','" +PHONEEXT + "','" +
						REGIONAL_ID + "','1','" +
						BR_ISBRANCH + "','"+ BR_ISBOOKINGBRANCH +"','"+ BR_PHNFAX +"'";
					try
					{
						conncons.ExecuteNonQuery();
					} 
					catch {}

					conncc.QueryString = "exec PARAM_GENERAL_RFBRANCH_ALL_MAKER  '2','" +
						CD_SIBS + "','" +BRANCH_CODE + "','" +BRANCH_NAME + "','" +
						BRANCH_TYPE_CONCC + "',null,'" +BR_ADDR + "',null,'" +
						BR_BRANCHAREA + "','" +BR_CCOBRANCH_CONCC + "',null,null,'" +
						BR_ZIPCODE + "','" +CBC_CODE + "','" +AREAID+ "','" +CITYID + "'," +
						"null,'" +PHONE+ "','" +PHONEAREA + "','" +PHONEEXT + "','" +
						REGIONAL_ID + "','1','" +
						BR_ISBRANCH + "','"+ BR_ISBOOKINGBRANCH +"','"+ BR_PHNFAX +"'";
					try
					{
						conncc.ExecuteNonQuery();
					} 
					catch {}

					//SME.area=Cons.Region
					connsme.QueryString = "exec PARAM_GENERAL_RFBRANCH_ALL_MAKER  '2','" +
						CD_SIBS + "','" +BRANCH_CODE + "','" +BRANCH_NAME + "','" +
						BRANCH_TYPE_SME + "',null,'" +BR_ADDR + "',null,'" +
						BR_BRANCHAREA + "','" +BR_CCOBRANCH_SME + "',null,null,'" +
						BR_ZIPCODE + "','" +CBC_CODE + "','" +REGIONAL_ID+ "','" +CITYID + "'," +
						"null,'" +PHONE+ "','" +PHONEAREA + "','" +PHONEEXT + "','" +
						AREAID + "','1','" +
						BR_ISBRANCH + "','"+ BR_ISBOOKINGBRANCH +"','"+ BR_PHNFAX +"'";
					try 
					{
						connsme.ExecuteNonQuery();
					} 
					catch {}

					//CuBES-Maintenance
					conn.QueryString = "exec PARAM_GENERAL_RFBRANCH_ALL_MAKER  '2','" +
						CD_SIBS + "','" +BRANCH_CODE + "','" +BRANCH_NAME + "','" +
						BRANCH_TYPE_SME + "',null,'" +BR_ADDR + "',null,'" +
						BR_BRANCHAREA + "','" +BR_CCOBRANCH_SME + "',null,null,'" +
						BR_ZIPCODE + "','" +CBC_CODE + "','" +REGIONAL_ID+ "','" +CITYID + "'," +
						"null,'" +PHONE+ "','" +PHONEAREA + "','" +PHONEEXT + "','" +
						AREAID + "','1','" +
						BR_ISBRANCH + "','"+ BR_ISBOOKINGBRANCH +"','"+ BR_PHNFAX +"'";
					try 
					{
						conn.ExecuteNonQuery();
					} 
					catch{}

					ViewPendingData();

					break;
				default :
					break;
			}
		}

		private void DGR_EXISTING_SME_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING_SME.CurrentPageIndex = e.NewPageIndex;
			this.ViewExistingData();
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearBoxes();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../HostParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void DDL_CITYID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			GetConsumerArea();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (this.LBL_SAVEMODE.Text == "1")
			{
				connsme.QueryString = "select * from RFBRANCH where BRANCH_CODE = '" + this.TXT_BRANCH_CODE.Text.Trim() + "'";
				connsme.ExecuteQuery();
				if (connsme.GetRowCount()>0)
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					GlobalTools.SetFocus(this,TXT_BRANCH_CODE);
					return;
				}

			}

			string CD_SIBS ,BRANCH_CODE ,BRANCH_NAME;
			string BRANCH_TYPE_CC,BRANCH_TYPE_SME /*,BR_APREGNO */,BR_ADDR /*,BR_APREGNODATE*/;
			string BR_BRANCHAREA ,BR_CCOBRANCH_SME,BR_CCOBRANCH_CONCC /*,BR_CUREF,BR_CUREFDATE*/;
			string BR_ZIPCODE ,CBC_CODE ,AREAID,CITYID;
			string /*HUB_CODE ,*/PHONE,PHONEAREA ,PHONEEXT;
			string REGIONAL_ID /*,ACTIVE */,BR_ISBRANCH;
			string BR_PHNFAX,BR_ISBOOKINGBRANCH;

			if (this.LBL_STA.Text == "0")//SME
			{
				BRANCH_CODE			= this.TXT_BRANCH_CODE.Text.Trim();
				CD_SIBS				= this.TXT_CD_SIBS.Text;
				BRANCH_NAME			= this.TXT_BRANCH_NAME.Text;
				CITYID				= this.DDL_CITYID.SelectedValue;
				//AREAID				= this.TXT_AREAID.Text;
				AREAID				= this.LBL_CONSAREA_ID.Text;
				REGIONAL_ID			= this.DDL_REGIONAL_ID.SelectedValue;
				BR_BRANCHAREA		= this.TXT_BR_BRANCH_AREA.Text;
				BR_ADDR				= this.TXT_BR_ADDR.Text;
				BR_ZIPCODE			= this.TXT_BR_ZIPCODE.Text;
				BR_PHNFAX			= this.TXT_BR_PHNFAX.Text;
				PHONEAREA			= this.TXT_PHONEAREA.Text;				
				PHONE				= this.TXT_PHONE.Text;
				PHONEEXT			= this.TXT_PHONEEXT.Text;
				BR_ISBRANCH			= this.DDL_BR_ISBRANCH.SelectedValue;
				BRANCH_TYPE_CC		= this.RBL_BRANCH_TYPE_CONCC.SelectedValue;
				BRANCH_TYPE_SME		= this.RBL_BRANCH_TYPE_SME.SelectedValue;
				CBC_CODE			= this.DDL_CBC_CODE.SelectedValue;
				BR_CCOBRANCH_SME	= this.DDL_BR_CCOBRANCH_SME.SelectedValue;
				BR_CCOBRANCH_CONCC	= this.DDL_BR_CCOBRANCH_CONCC.SelectedValue;
				BR_ISBOOKINGBRANCH	= this.DDL_ISBOOKINGBRANCH.SelectedValue;
			} 
			else//CONCC
			{
				BRANCH_CODE			= this.TXT_BRANCH_CODE.Text.Trim();
				CD_SIBS				= this.TXT_CD_SIBS.Text;
				BRANCH_NAME			= this.TXT_BRANCH_NAME.Text;
				CITYID				= this.DDL_CITYID.SelectedValue;
				AREAID				= this.LBL_CONSAREA_ID.Text;
				REGIONAL_ID			= this.DDL_REGIONAL_ID.SelectedValue;
				//BR_BRANCHAREA		= "";
				BR_BRANCHAREA		= this.TXT_BR_BRANCH_AREA.Text;
				BR_ADDR				= this.TXT_BR_ADDR.Text;
				BR_ZIPCODE			= this.TXT_BR_ZIPCODE.Text;
				//BR_PHNFAX			= "";
				BR_PHNFAX			= this.TXT_BR_PHNFAX.Text;
				PHONEAREA			= this.TXT_PHONEAREA.Text;
				PHONE				= this.TXT_PHONE.Text;
				PHONEEXT			= this.TXT_PHONEEXT.Text;
				BR_ISBRANCH			= this.DDL_BR_ISBRANCH.SelectedValue;
				BRANCH_TYPE_CC		= this.RBL_BRANCH_TYPE_CONCC.SelectedValue;
				BRANCH_TYPE_SME		= this.RBL_BRANCH_TYPE_SME.SelectedValue;
				//CBC_CODE			= "";
				CBC_CODE			= this.DDL_CBC_CODE.SelectedValue;
				BR_CCOBRANCH_SME	= this.DDL_BR_CCOBRANCH_SME.SelectedValue;
				BR_CCOBRANCH_CONCC	= this.DDL_BR_CCOBRANCH_CONCC.SelectedValue;//Comm. Branch di Consumer
				BR_ISBOOKINGBRANCH	= this.DDL_ISBOOKINGBRANCH.SelectedValue;
			}
			
			conncons.QueryString = "exec PARAM_GENERAL_RFBRANCH_ALL_MAKER  '"+ this.LBL_SAVEMODE.Text.Trim()+"','" +
				CD_SIBS + "','" +BRANCH_CODE + "','" +BRANCH_NAME + "','" +
				BRANCH_TYPE_CC + "',null,'" +BR_ADDR + "',null,'" +
				BR_BRANCHAREA + "','" +BR_CCOBRANCH_CONCC + "',null,null,'" +
				BR_ZIPCODE + "','" +CBC_CODE + "','" +AREAID+ "','" +CITYID + "'," +
				"null,'" +PHONE+ "','" +PHONEAREA + "','" +PHONEEXT + "','" +
				REGIONAL_ID + "','1','" +BR_ISBRANCH + "','"+BR_ISBOOKINGBRANCH+"','"+BR_PHNFAX+"'";
			try
			{
				conncons.ExecuteQuery();
			} 
			catch {}

			conncc.QueryString = "exec PARAM_GENERAL_RFBRANCH_ALL_MAKER  '"+ this.LBL_SAVEMODE.Text.Trim()+"','" +
				CD_SIBS + "','" +BRANCH_CODE + "','" +BRANCH_NAME + "','" +
				BRANCH_TYPE_CC + "',null,'" +BR_ADDR + "',null,'" +
				BR_BRANCHAREA + "','" +BR_CCOBRANCH_CONCC + "',null,null,'" +
				BR_ZIPCODE + "','" +CBC_CODE + "','" +AREAID+ "','" +CITYID + "'," +
				"null,'" +PHONE+ "','" +PHONEAREA + "','" +PHONEEXT + "','" +
				REGIONAL_ID + "','1','" +BR_ISBRANCH + "','"+BR_ISBOOKINGBRANCH+"','"+BR_PHNFAX+"'";
			try
			{
				conncc.ExecuteQuery();
			} 
			catch{}

			//SME.areaid=Cons.Regional_id
			connsme.QueryString = "exec PARAM_GENERAL_RFBRANCH_ALL_MAKER  '"+ this.LBL_SAVEMODE.Text.Trim()+"','" +
				CD_SIBS + "','" +BRANCH_CODE + "','" +BRANCH_NAME + "','" +
				BRANCH_TYPE_SME + "',null,'" +BR_ADDR + "',null,'" +
				BR_BRANCHAREA + "','" +BR_CCOBRANCH_SME + "',null,null,'" +
				BR_ZIPCODE + "','" + CBC_CODE + "','" +REGIONAL_ID+ "','" +CITYID + "'," +
				"null" +",'" +PHONE+ "','" + PHONEAREA + "','" +PHONEEXT + "', '" +
				AREAID + "','1','" + BR_ISBRANCH + "','" + BR_ISBOOKINGBRANCH +"','"+ BR_PHNFAX +"' ";
			try
			{
				connsme.ExecuteQuery();
			} 
			catch{}

			//CuBES-Maintenance DB ... same with SME!...
			conn.QueryString = "exec PARAM_GENERAL_RFBRANCH_ALL_MAKER  '"+ this.LBL_SAVEMODE.Text.Trim()+"','" +
				CD_SIBS + "','" +BRANCH_CODE + "','" +BRANCH_NAME + "','" +
				BRANCH_TYPE_SME + "',null,'" +BR_ADDR + "',null,'" +
				BR_BRANCHAREA + "','" +BR_CCOBRANCH_SME + "',null,null,'" +
				BR_ZIPCODE + "','" + CBC_CODE + "','" +REGIONAL_ID+ "','" +CITYID + "'," +
				"null" +",'" +PHONE+ "','" + PHONEAREA + "','" +PHONEEXT + "', '" +
				AREAID + "','1','" + BR_ISBRANCH + "','" + BR_ISBOOKINGBRANCH +"','"+ BR_PHNFAX +"' ";
			try 
			{
				conn.ExecuteQuery();
			} 
			catch{}
			
		
			ViewPendingData();

			this.LBL_SAVEMODE.Text = "1";
			this.ClearBoxes();
			
		}

		private void GetDataOtherModule(string module, string branch_code,string pending)
		{
			if (module=="01")
			{
				connsme.QueryString="select BRANCH_TYPE,BR_PHNFAX, BR_BRANCHAREA,BR_ISBOOKINGBRANCH,BR_CCOBRANCH from PENDING_RFBRANCH_ALL where BRANCH_CODE='" + branch_code + "'";
				connsme.ExecuteQuery();
				this.LBL_BRANCH_TYPE_SME.Text			= connsme.GetFieldValue("BRANCH_TYPE");
				this.LBL_BR_CCOBRANCH_SME.Text			= connsme.GetFieldValue("BR_CCOBRANCH");
				this.LBL_BR_PHNFAX.Text					= connsme.GetFieldValue("BR_PHNFAX");
				this.LBL_BR_ISBOOKINGBRANCH.Text		= connsme.GetFieldValue("BR_ISBOOKINGBRANCH");
				this.LBL_BR_BRANCHAREA.Text				= connsme.GetFieldValue("BR_BRANCHAREA");
				
			} 
			else
			{
				conncons.QueryString="select BRANCH_TYPE,BR_CCOBRANCH,PHONEAREA,PHONE,PHONEEXT,AREA_ID from PENDING_RFBRANCH_ALL where BRANCH_CODE='" + branch_code + "'";
				try 
				{
					conncons.ExecuteQuery();
				}
				catch { return; }
				this.LBL_BRANCH_TYPE_CONCC.Text		= conncons.GetFieldValue("BRANCH_TYPE");
				this.LBL_BR_CCOBRANCH_CONCC.Text	= conncons.GetFieldValue("BR_CCOBRANCH");
				this.LBL_PHONE1.Text				= conncons.GetFieldValue("PHONE");
				this.LBL_PHONEAREA.Text				= conncons.GetFieldValue("PHONEAREA");
				this.LBL_PHONEEXT.Text				= conncons.GetFieldValue("PHONEEXT");
				this.LBL_AREAID.Text				= conncons.GetFieldValue("AREA_ID");
			}
			
		}
		

		private void DGR_REQUEST_CONNCC_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[20].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					FillDDL();
					this.TXT_BRANCH_CODE.Text			= CleansText(e.Item.Cells[0].Text.Trim());
					this.TXT_CD_SIBS.Text				= CleansText(e.Item.Cells[1].Text.Trim());
					this.TXT_BRANCH_NAME.Text			= CleansText(e.Item.Cells[2].Text.Trim());
					try{this.DDL_CITYID.SelectedValue	= CleansText(e.Item.Cells[3].Text.Trim());}
					catch{}
					//this.TXT_AREAID.Text				= CleansText(e.Item.Cells[5].Text.Trim());
					try{this.DDL_REGIONAL_ID.SelectedValue	= CleansText(e.Item.Cells[7].Text.Trim());}
					catch{}
					this.TXT_BR_ADDR.Text				= CleansText(e.Item.Cells[9].Text.Trim());
					this.TXT_BR_ZIPCODE.Text			= CleansText(e.Item.Cells[10].Text.Trim());
					/*************/
					this.TXT_BR_PHNFAX.Text				= CleansText(e.Item.Cells[22].Text.Trim());
					this.TXT_BR_BRANCH_AREA.Text		= CleansText(e.Item.Cells[24].Text.Trim());
					/*************/
					this.TXT_PHONEAREA.Text				= CleansText(e.Item.Cells[11].Text.Trim());				
					this.TXT_PHONE.Text					= CleansText(e.Item.Cells[12].Text.Trim());
					this.TXT_PHONEEXT.Text				= CleansText(e.Item.Cells[13].Text.Trim());
					try{this.DDL_BR_ISBRANCH.SelectedValue			= CleansText(e.Item.Cells[14].Text.Trim());}
					catch{}
					try{this.RBL_BRANCH_TYPE_CONCC.SelectedValue	= CleansText(e.Item.Cells[16].Text.Trim());}
					catch{}
					try{this.DDL_CBC_CODE.SelectedValue				= CleansText(e.Item.Cells[18].Text.Trim());}
					catch{}
					try{this.DDL_BR_CCOBRANCH_CONCC.SelectedValue	= CleansText(e.Item.Cells[19].Text.Trim());}
					catch{}
					GetConsumerArea();
					/*************/
					GetDataOtherModule("01",TXT_BRANCH_CODE.Text.Trim(),"1");
					try
					{
						this.DDL_ISBOOKINGBRANCH.SelectedValue	= CleansText(e.Item.Cells[23].Text.Trim());
					}
					catch {}
					try
					{
						this.RBL_BRANCH_TYPE_SME.SelectedValue	= this.LBL_BRANCH_TYPE_SME.Text.Trim();
					} 
					catch{}
					try
					{
						this.DDL_BR_CCOBRANCH_SME.SelectedValue	= this.LBL_BR_CCOBRANCH_SME.Text.Trim();
					} 
					catch{}
					/*************/
					activateControlKey(true);
					break;
				case "delete":
					string BRANCH_CODE;
					BRANCH_CODE				= CleansText(e.Item.Cells[0].Text.Trim());
		
					conncons.QueryString = "delete from PENDING_RFBRANCH_ALL where BRANCH_CODE ='" +
					BRANCH_CODE +"'";
					try 
					{
						conncons.ExecuteNonQuery();
					} 
					catch {}

					conncc.QueryString = "delete from PENDING_RFBRANCH_ALL where BRANCH_CODE ='" +
						BRANCH_CODE +"'";
						try
						{
							conncc.ExecuteNonQuery();
						} 
						catch {}

					connsme.QueryString = "delete from PENDING_RFBRANCH_ALL where BRANCH_CODE ='" +
						BRANCH_CODE +"'";
					try 
					{
						connsme.ExecuteNonQuery();
					}
					catch {}

					//CuBES-Maintenance DB
					conn.QueryString = "delete from PENDING_RFBRANCH_ALL where BRANCH_CODE ='" +
						BRANCH_CODE +"'";
					try
					{
						conn.ExecuteNonQuery();
					} 
					catch {}

					ViewPendingData();
					break;
				default :
					break;
			}
		}

		private void DGR_REQUEST_CONNCC_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST_CONNCC.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_REQUEST_SME_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST_SME.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}
		/****** rEQUEST!!!!!*/
		private void DGR_REQUEST_SME_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[19].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					FillDDL();
					this.TXT_BRANCH_CODE.Text				= CleansText(e.Item.Cells[0].Text.Trim());
					this.TXT_CD_SIBS.Text					= CleansText(e.Item.Cells[1].Text.Trim());
					this.TXT_BRANCH_NAME.Text				= CleansText(e.Item.Cells[2].Text.Trim());
					try{this.DDL_CITYID.SelectedValue		= CleansText(e.Item.Cells[3].Text.Trim());}
					catch{}
					/**********/
					//this.TXT_AREAID.Text					= CleansText(e.Item.Cells[24].Text.Trim());
					GetConsumerArea();
					/**********/
					try{this.DDL_REGIONAL_ID.SelectedValue	= CleansText(e.Item.Cells[5].Text.Trim());}
					catch{}
					this.TXT_BR_ADDR.Text					= CleansText(e.Item.Cells[7].Text.Trim());
					this.TXT_BR_BRANCH_AREA.Text			= CleansText(e.Item.Cells[8].Text.Trim());
					this.TXT_BR_ZIPCODE.Text				= CleansText(e.Item.Cells[9].Text.Trim());
					this.TXT_BR_PHNFAX.Text					= CleansText(e.Item.Cells[10].Text.Trim());
					/*************/
					this.TXT_PHONEAREA.Text				= CleansText(e.Item.Cells[21].Text.Trim());				
					this.TXT_PHONE.Text					= CleansText(e.Item.Cells[22].Text.Trim());
					this.TXT_PHONEEXT.Text				= CleansText(e.Item.Cells[23].Text.Trim());
					/*************/
					try{this.DDL_BR_ISBRANCH.SelectedValue	= CleansText(e.Item.Cells[11].Text.Trim());}
					catch{}
					try{this.RBL_BRANCH_TYPE_SME.SelectedValue		= CleansText(e.Item.Cells[13].Text.Trim());}
					catch{}
					try{this.DDL_CBC_CODE.SelectedValue				= CleansText(e.Item.Cells[15].Text.Trim());}
					catch{}
					try{this.DDL_BR_CCOBRANCH_SME.SelectedValue			= CleansText(e.Item.Cells[16].Text.Trim());}
					catch{}
					try {this.DDL_ISBOOKINGBRANCH.SelectedValue			= CleansText(e.Item.Cells[17].Text.Trim());}
					catch{}
					GetDataOtherModule("40",TXT_BRANCH_CODE.Text.Trim(),"1");
					try{this.RBL_BRANCH_TYPE_CONCC.SelectedValue	= this.LBL_BRANCH_TYPE_CONCC.Text;}
					catch{}
					activateControlKey(true);
					break;
				case "delete":
					string BRANCH_CODE;
					BRANCH_CODE				= CleansText(e.Item.Cells[0].Text.Trim());
		
					conncons.QueryString = "delete from PENDING_RFBRANCH_ALL where BRANCH_CODE ='" +
						BRANCH_CODE +"'";
					try
					{
						conncons.ExecuteNonQuery();
					} 
					catch {}

					conncc.QueryString = "delete from PENDING_RFBRANCH_ALL where BRANCH_CODE ='" +
						BRANCH_CODE +"'";
					try
					{
						conncc.ExecuteNonQuery();
					} catch {}

					connsme.QueryString = "delete from PENDING_RFBRANCH_ALL where BRANCH_CODE ='" +
						BRANCH_CODE +"'";
					try 
					{
						connsme.ExecuteNonQuery();
					}catch {}
					
					//CuBES-Maintenance DB
					conn.QueryString = "delete from PENDING_RFBRANCH_ALL where BRANCH_CODE ='" +
						BRANCH_CODE +"'";
					try 
					{
						conn.ExecuteNonQuery();
					} catch {}

					ViewPendingData();
					break;
				default :
					break;
			}
		}

		protected void RBL_BRANCH_TYPE_SME_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( RBL_BRANCH_TYPE_SME.SelectedValue == "3" ) 
				DDL_CBC_CODE.Enabled = false;
			else 
				DDL_CBC_CODE.Enabled = true;
		}
	}
}
