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
	/// Summary description for ProductParam.
	/// </summary>
	public partial class ProductParam : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid;
	
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

			InitialCon();

			BindData1();
			BindData2();
			
			GlobalTools.fillRefList(DDL_GID,"select GROUP_ID, GROUP_NAME from GROUP_PRODUCT", false,conn);
			GlobalTools.fillRefList(DDL_PROMO,"select GROUP_PROMO, NAMA_PROMO from GROUP_PROMO",false,conn);
			GlobalTools.fillRefList(DDL_CHILDPRODUCT,"select PRODUCTID, PRODUCTNAME from TPRODUCT",true,conn);
			
 
			DDL_CUST_TYPE.Items.Add(new ListItem("Personal","0"));
			DDL_CUST_TYPE.Items.Add(new ListItem("Company","1"));
			DDL_CUST_TYPE.Items.Add(new ListItem("Personal and Company","2"));

			DDL_TYPE.Items.Add(new ListItem("Secured Loan","SL"));
			DDL_TYPE.Items.Add(new ListItem("UnSecured Loan","US"));

		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void BindData1()
		{
			string c1 = "", c2 = "", c3 = "", c4 = "", c5 = "", c6 = "";
			CheckBox chk1, chk2, chk3, chk4, chk5, chk6;

			conn.QueryString = "select * from VW_PARAM_TPRODUCT_MAKER order by cast(PRODUCTID as int) asc";
			conn.ExecuteQuery();

			if (conn.GetRowCount() != 0)
			{
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

				for (int i = 0; i < DG1.Items.Count; i++)
				{
					c1 = DG1.Items[i].Cells[28].Text.Trim();
					c2 = DG1.Items[i].Cells[29].Text.Trim();
					c3 = DG1.Items[i].Cells[30].Text.Trim();
					c4 = DG1.Items[i].Cells[31].Text.Trim();
					c5 = DG1.Items[i].Cells[32].Text.Trim();
					c6 = DG1.Items[i].Cells[35].Text.Trim();

					chk1 = (CheckBox)DG1.Items[i].Cells[4].FindControl("CHK_NG1");
					chk2 = (CheckBox)DG1.Items[i].Cells[5].FindControl("CHK_BL1");
					chk3 = (CheckBox)DG1.Items[i].Cells[6].FindControl("CHK_PRE1");
					chk4 = (CheckBox)DG1.Items[i].Cells[7].FindControl("CHK_DHBI1");
					chk5 = (CheckBox)DG1.Items[i].Cells[24].FindControl("CHK_SPK1");
					chk6 = (CheckBox)DG1.Items[i].Cells[25].FindControl("CHK_ALLOWCARDBUNDLING1");

					if (c1 == "1")
						chk1.Checked = true;
					else
						chk1.Checked = false;

					if (c2 == "1")
						chk2.Checked = true;
					else
						chk2.Checked = false;

					if (c3 == "1")
						chk3.Checked = true;
					else
						chk3.Checked = false;

					if (c4 == "1")
						chk4.Checked = true;
					else
						chk4.Checked = false;

					if (c5 == "1")
						chk5.Checked = true;
					else
						chk5.Checked = false;

					if (c6 == "1")
						chk6.Checked = true;
					else
						chk6.Checked = false;
					
					chk1.Enabled = false;
					chk2.Enabled = false;
					chk3.Enabled = false;
					chk4.Enabled = false;
					chk5.Enabled = false;
					chk6.Enabled = false;
				}
			} 

			conn.ClearData();
		}

		private void BindData2()
		{
			//Response.Write(e.Item.Cells[23].Text.Trim());
			//Response.End();

			string c1 = "", c2 = "", c3 = "", c4 = "", c5 = "", c6 = "";
			CheckBox ck1, ck2, ck3, ck4, ck5, ck6;

			conn.QueryString = "select * from VW_PARAM_TPRODUCT_PENDING order by cast(PRODUCTID as int) asc";
			conn.ExecuteQuery();

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

			for (int i = 0; i < DG2.Items.Count; i++)
			{
				c1 = DG2.Items[i].Cells[29].Text.Trim();
				c2 = DG2.Items[i].Cells[30].Text.Trim();
				c3 = DG2.Items[i].Cells[31].Text.Trim();
				c4 = DG2.Items[i].Cells[32].Text.Trim();
				c5 = DG2.Items[i].Cells[33].Text.Trim();
				c6 = DG2.Items[i].Cells[36].Text.Trim();

				ck1 = (CheckBox)DG2.Items[i].Cells[4].FindControl("CHK_NG2");
				ck2 = (CheckBox)DG2.Items[i].Cells[5].FindControl("CHK_BL2");
				ck3 = (CheckBox)DG2.Items[i].Cells[6].FindControl("CHK_PRE2");
				ck4 = (CheckBox)DG2.Items[i].Cells[7].FindControl("CHK_DHBI2");
				ck5 = (CheckBox)DG2.Items[i].Cells[24].FindControl("CHK_SPK2");
				ck6 = (CheckBox)DG2.Items[i].Cells[25].FindControl("CHK_ALLOWCARDBUNDLING2");

				if (c1 == "1")
					ck1.Checked = true;
				else
					ck1.Checked = false;

				if (c2 == "1")
					ck2.Checked = true;
				else
					ck2.Checked = false;

				if (c3 == "1")
					ck3.Checked = true;
				else
					ck3.Checked = false;

				if (c4 == "1")
					ck4.Checked = true;
				else
					ck4.Checked = false;

				if (c5 == "1")
					ck5.Checked = true;
				else
					ck5.Checked = false;

				if (c6 == "1")
					ck6.Checked = true;
				else
					ck6.Checked = false;
					
				ck1.Enabled = false;
				ck2.Enabled = false;
				ck3.Enabled = false;
				ck4.Enabled = false;
				ck5.Enabled = false;
				ck6.Enabled = false;

				if (DG2.Items[i].Cells[27].Text.Trim() == "2")
				{
					DG2.Items[i].Cells[27].Text = "UPDATE";
				}
				else if (DG2.Items[i].Cells[27].Text.Trim() == "1")
				{
					DG2.Items[i].Cells[27].Text = "INSERT";
				}
				else if (DG2.Items[i].Cells[27].Text.Trim() == "3")
				{
					DG2.Items[i].Cells[27].Text = "DELETE";
				}
			}
		}

		private void ClearEditBoxes()
		{
			TXT_ADMIN_FEE.Text = "0";
			TXT_AIP_LM.Text = "0";
			TXT_BEA_OTHER.Text = "0";
			TXT_PR_CODE.Text = "";
			TXT_CEIL_LIMIT.Text = "0";
			TXT_DP.Text = "0";
			TXT_EMAS_CODE.Text = "";
			TXT_FIDUCIA.Text = "0";
			TXT_FIDUCIA_LIM.Text = "0";
			TXT_FLOOR_LIMIT.Text = "0";
			TXT_FLOOR_RATE.Text = "0";
			TXT_MARKET.Text = "";
			TXT_NPWP.Text = "";
			TXT_PRNAME.Text = "";
			TXT_PROV_RATE.Text = "0";
			TXT_PROVISI.Text = "0";
			TXT_SPPK_LM.Text = "0";
			CHK_BLACK.Checked = false;
			CHK_DHBI.Checked = false;
			CHK_NEGLST.Checked = false;
			CHK_PRES.Checked = false;
			CHK_SPK.Checked = false;
			CHK_PR_KENDARA.Checked = false;
			CHK_CARDBUNDLING.Checked = false;
			CHK_PR_MITRAKARYA.Checked = false;

			//For Child Product
			DDL_CHILDPRODUCT.SelectedIndex = 0;
			TXT_CHILDMINTENOR.Text = "";
			TXT_CHILDMAXTENOR.Text = "";
			TXT_CHILDDEFTENOR.Text = "";
			TXT_CHILDMINRATIO.Text = "";
			TXT_CHILDMAXRATIO.Text = "";
			TXT_CHILDDEFRATIO.Text = "";
			TXT_CHILDMININTEREST.Text = "";
			TXT_CHILDMAXINTEREST.Text = "";
			TXT_CHILDDEFINTEREST.Text = "";
			TXT_CHILDMINLIMIT.Text = "0";
			TXT_CHILDMAXLIMIT.Text = "0";
			CHK_REVOLVING.Checked = false;
			CHK_ROUND_APPROVAL.Checked = false;
			CHK_ACTIVE.Checked = false;
			CHK_DOC_RAB.Checked = false;
			TXT_PR_CODE.Enabled = true;

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

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string k1, k2, k3, k4, k5, k6, k7, k8, k9, k10, k11, k12;
			int hit = 0;

			if(CHK_NEGLST.Checked)
				k1 = "1";
			else
				k1 = "0";

			if(CHK_BLACK.Checked)
				k2 = "1";
			else
				k2 = "0";

			if(CHK_PRES.Checked)
				k3 = "1";
			else
				k3 = "0";

			if(CHK_DHBI.Checked)
				k4 = "1";
			else
				k4 = "0";

			if(CHK_SPK.Checked)
				k5 = "1";
			else
				k5 = "0";

			if(CHK_PR_KENDARA.Checked)
				k6 = "1";
			else
				k6 = "0";

			if(CHK_REVOLVING.Checked)
				k7 = "1";
			else
				k7 = "0";

			if(CHK_ROUND_APPROVAL.Checked)
				k8 = "1";
			else
				k8 = "0";

			if(CHK_ACTIVE.Checked)
				k9 = "1";
			else
				k9 = "0";

			if(CHK_DOC_RAB.Checked)
				k10 = "1";
			else
				k10 = "0";

			if(CHK_CARDBUNDLING.Checked)
				k11 = "1";
			else
				k11 = "0";

			if(CHK_PR_MITRAKARYA.Checked)
				k12 = "1";
			else
				k12 = "0";

			conn.QueryString = "SELECT PRODUCTID FROM VW_PARAM_TPRODUCT_PENDING WHERE PRODUCTID = '"+TXT_PR_CODE.Text+"' and CH_STA <> '3'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "EXEC PARAM_GENERAL_TPRODUCT_MAKER '"+TXT_PR_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_PRNAME.Text)+
					", '"+DDL_CUST_TYPE.SelectedValue+"', "+GlobalTools.ConvertFloat(TXT_NPWP.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_ADMIN_FEE.Text)+", "+GlobalTools.ConvertFloat(TXT_DP.Text)+", "+GlobalTools.ConvertFloat(TXT_SPPK_LM.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_AIP_LM.Text)+", "+GlobalTools.ConvertFloat(TXT_FLOOR_RATE.Text)+", "+GlobalTools.ConvertFloat(TXT_FLOOR_LIMIT.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_CEIL_LIMIT.Text)+", "+GlobalTools.ConvertFloat(TXT_PROVISI.Text)+", "+GlobalTools.ConvertFloat(TXT_PROV_RATE.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_FIDUCIA.Text)+", "+GlobalTools.ConvertFloat(TXT_FIDUCIA_LIM.Text)+", "+GlobalTools.ConvertFloat(TXT_BEA_OTHER.Text)+
					" , "+GlobalTools.ConvertNull(TXT_EMAS_CODE.Text)+", "+GlobalTools.ConvertNull(TXT_MARKET.Text)+", '"+k1+
					"', '"+k2+"', '"+k3+"', '"+k4+
					"', '"+k5+"', '"+DDL_GID.SelectedValue+"', '"+DDL_PROMO.SelectedValue+"', '4', '"+DDL_TYPE.SelectedValue+
					"', '"+k6+"', '"+k11+"', "+GlobalTools.ConvertNull(DDL_CHILDPRODUCT.SelectedValue)+", "+GlobalTools.ConvertFloat(TXT_CHILDMINTENOR.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMAXTENOR.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDDEFTENOR.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_CHILDMINRATIO.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMAXRATIO.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDDEFRATIO.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDDEFINTEREST.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMININTEREST.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_CHILDMAXINTEREST.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMINLIMIT.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMAXLIMIT.Text)+
					" , '"+k7+"', '"+k8+"', '"+k9+"', '"+k10+"', '"+k12+"'";
								
				conn.ExecuteNonQuery();
				
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "EXEC PARAM_GENERAL_TPRODUCT_MAKER '"+TXT_PR_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_PRNAME.Text)+
					", '"+DDL_CUST_TYPE.SelectedValue+"', "+GlobalTools.ConvertFloat(TXT_NPWP.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_ADMIN_FEE.Text)+", "+GlobalTools.ConvertFloat(TXT_DP.Text)+", "+GlobalTools.ConvertFloat(TXT_SPPK_LM.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_AIP_LM.Text)+", "+GlobalTools.ConvertFloat(TXT_FLOOR_RATE.Text)+", "+GlobalTools.ConvertFloat(TXT_FLOOR_LIMIT.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_CEIL_LIMIT.Text)+", "+GlobalTools.ConvertFloat(TXT_PROVISI.Text)+", "+GlobalTools.ConvertFloat(TXT_PROV_RATE.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_FIDUCIA.Text)+", "+GlobalTools.ConvertFloat(TXT_FIDUCIA_LIM.Text)+", "+GlobalTools.ConvertFloat(TXT_BEA_OTHER.Text)+
					" , "+GlobalTools.ConvertNull(TXT_EMAS_CODE.Text)+", "+GlobalTools.ConvertNull(TXT_MARKET.Text)+", '"+k1+
					"', '"+k2+"', '"+k3+"', '"+k4+
					"', '"+k5+"', '"+DDL_GID.SelectedValue+"', '"+DDL_PROMO.SelectedValue+"', '2', '"+DDL_TYPE.SelectedValue+
					"', '"+k6+"', '"+k11+"', "+GlobalTools.ConvertNull(DDL_CHILDPRODUCT.SelectedValue)+", "+GlobalTools.ConvertFloat(TXT_CHILDMINTENOR.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMAXTENOR.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDDEFTENOR.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_CHILDMINRATIO.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMAXRATIO.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDDEFRATIO.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDDEFINTEREST.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMININTEREST.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_CHILDMAXINTEREST.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMINLIMIT.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMAXLIMIT.Text)+
					" , '"+k7+"', '"+k8+"', '"+k9+"', '"+k10+"', '"+k12+"'";

				conn.ExecuteNonQuery();
				
				ClearEditBoxes();
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				conn.QueryString = "EXEC PARAM_GENERAL_TPRODUCT_MAKER '"+TXT_PR_CODE.Text+"', "+GlobalTools.ConvertNull(TXT_PRNAME.Text)+
					", '"+DDL_CUST_TYPE.SelectedValue+"', "+GlobalTools.ConvertFloat(TXT_NPWP.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_ADMIN_FEE.Text)+", "+GlobalTools.ConvertFloat(TXT_DP.Text)+", "+GlobalTools.ConvertFloat(TXT_SPPK_LM.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_AIP_LM.Text)+", "+GlobalTools.ConvertFloat(TXT_FLOOR_RATE.Text)+", "+GlobalTools.ConvertFloat(TXT_FLOOR_LIMIT.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_CEIL_LIMIT.Text)+", "+GlobalTools.ConvertFloat(TXT_PROVISI.Text)+", "+GlobalTools.ConvertFloat(TXT_PROV_RATE.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_FIDUCIA.Text)+", "+GlobalTools.ConvertFloat(TXT_FIDUCIA_LIM.Text)+", "+GlobalTools.ConvertFloat(TXT_BEA_OTHER.Text)+
					" , "+GlobalTools.ConvertNull(TXT_EMAS_CODE.Text)+", "+GlobalTools.ConvertNull(TXT_MARKET.Text)+", '"+k1+
					"', '"+k2+"', '"+k3+"', '"+k4+
					"', '"+k5+"', '"+DDL_GID.SelectedValue+"', '"+DDL_PROMO.SelectedValue+"', '1', '"+DDL_TYPE.SelectedValue+
					"', '"+k6+"', '"+k11+"', "+GlobalTools.ConvertNull(DDL_CHILDPRODUCT.SelectedValue)+", "+GlobalTools.ConvertFloat(TXT_CHILDMINTENOR.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMAXTENOR.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDDEFTENOR.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_CHILDMINRATIO.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMAXRATIO.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDDEFRATIO.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDDEFINTEREST.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMININTEREST.Text)+
					" , "+GlobalTools.ConvertFloat(TXT_CHILDMAXINTEREST.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMINLIMIT.Text)+", "+GlobalTools.ConvertFloat(TXT_CHILDMAXLIMIT.Text)+
					" , '"+k7+"', '"+k8+"', '"+k9+"', '"+k10+"', '"+k12+"'";
								
				conn.ExecuteNonQuery();

				ClearEditBoxes();
			}
			else if((hit != 0) && (LBL_SAVEMODE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
				return;
			}

			conn.ClearData();
 	
			BindData2();

			LBL_SAVEMODE.Text = "1"; 
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes();
		}

		private void DG1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG1.CurrentPageIndex = e.NewPageIndex;
			BindData1();
		}

		private void DG2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG2.CurrentPageIndex = e.NewPageIndex;
			BindData2();
		}

		private void DG1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string pid, gid; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					pid = e.Item.Cells[0].Text.Trim();
					gid = e.Item.Cells[33].Text.Trim();

					//Response.Write(e.Item.Cells[25].Text.Trim());
					//Response.End();

					conn.QueryString = "SELECT PRODUCTID FROM VW_PARAM_TPRODUCT_PENDING WHERE PRODUCTID = '"+pid+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "EXEC PARAM_GENERAL_TPRODUCT_MAKER '"+pid+"', '"+cleansText(e.Item.Cells[1].Text.Trim())+
								"', '"+cleansText(e.Item.Cells[3].Text.Trim())+"', "+GlobalTools.ConvertFloat(e.Item.Cells[10].Text.Trim())+
								" , "+GlobalTools.ConvertFloat(e.Item.Cells[11].Text.Trim())+", "+GlobalTools.ConvertFloat(e.Item.Cells[12].Text.Trim())+", "+e.Item.Cells[13].Text.Trim()+
								" , "+e.Item.Cells[14].Text.Trim()+", "+GlobalTools.ConvertFloat(e.Item.Cells[15].Text.Trim())+", "+GlobalTools.ConvertFloat(e.Item.Cells[16].Text.Trim())+
								" , "+GlobalTools.ConvertFloat(e.Item.Cells[17].Text.Trim())+", "+GlobalTools.ConvertFloat(e.Item.Cells[18].Text.Trim())+", "+GlobalTools.ConvertFloat(e.Item.Cells[19].Text.Trim())+
								" , "+GlobalTools.ConvertFloat(e.Item.Cells[20].Text.Trim())+", "+GlobalTools.ConvertFloat(e.Item.Cells[21].Text.Trim())+", "+GlobalTools.ConvertFloat(e.Item.Cells[22].Text.Trim())+
								" , "+GlobalTools.ConvertNull(e.Item.Cells[23].Text.Trim())+", "+GlobalTools.ConvertNull(e.Item.Cells[22].Text.Trim())+", '"+cleansText(e.Item.Cells[28].Text.Trim())+
								"', '"+cleansText(e.Item.Cells[29].Text.Trim())+"', '"+cleansText(e.Item.Cells[30].Text.Trim())+"', '"+cleansText(e.Item.Cells[31].Text.Trim())+
								"', '"+cleansText(e.Item.Cells[32].Text.Trim())+"', '"+cleansText(gid)+"', '"+cleansText(e.Item.Cells[34].Text.Trim())+"', '3"+
								"', '"+cleansText(e.Item.Cells[36].Text.Trim())+"', '"+cleansText(e.Item.Cells[37].Text.Trim())+"', '"+cleansText(e.Item.Cells[35].Text.Trim())+
								"', '"+cleansText(e.Item.Cells[38].Text.Trim())+"', '"+GlobalTools.ConvertFloat(e.Item.Cells[39].Text.Trim())+"', '"+GlobalTools.ConvertFloat(e.Item.Cells[40].Text.Trim())+
								"', '"+GlobalTools.ConvertFloat(e.Item.Cells[41].Text.Trim())+"', '"+GlobalTools.ConvertFloat(e.Item.Cells[42].Text.Trim())+"', '"+GlobalTools.ConvertFloat(e.Item.Cells[43].Text.Trim())+
								"', '"+GlobalTools.ConvertFloat(e.Item.Cells[44].Text.Trim())+"', '"+GlobalTools.ConvertFloat(e.Item.Cells[45].Text.Trim())+"', '"+GlobalTools.ConvertFloat(e.Item.Cells[46].Text.Trim())+
								"', '"+GlobalTools.ConvertFloat(e.Item.Cells[47].Text.Trim())+"', '"+GlobalTools.ConvertFloat(e.Item.Cells[48].Text.Trim())+"', '"+GlobalTools.ConvertFloat(e.Item.Cells[49].Text.Trim())+
								"', '"+cleansText(e.Item.Cells[50].Text.Trim())+"', '"+cleansText(e.Item.Cells[51].Text.Trim())+"', '"+cleansText(e.Item.Cells[52].Text.Trim())+"', '"+cleansText(e.Item.Cells[53].Text.Trim())+"'";
								
						conn.ExecuteNonQuery();

						BindData2();
					}
					break;

				case "edit":
					pid = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "SELECT * FROM TPRODUCT where PRODUCTID = '"+pid+"'";
					conn.ExecuteQuery();

					TXT_PR_CODE.Enabled = false;
					TXT_PR_CODE.Text = conn.GetFieldValue("PRODUCTID");				
					TXT_PRNAME.Text = conn.GetFieldValue("PRODUCTNAME");
					
					try
					{
						DDL_GID.SelectedValue = conn.GetFieldValue("GROUP_ID");
					}
					catch{ }

					try
					{
						DDL_CUST_TYPE.SelectedValue = conn.GetFieldValue("CUSTOMER_TYPE");
					}
					catch{ }

					try
					{
						DDL_PROMO.SelectedValue = conn.GetFieldValue("GROUP_PROMO"); 
					}
					catch{ }

					if(conn.GetFieldValue("NL_CHECK") == "1")    
						CHK_NEGLST.Checked = true;
					else
						CHK_NEGLST.Checked = false; 

					if(conn.GetFieldValue("BLACKLIST_CHECK") == "1")    
						CHK_BLACK.Checked = true;
					else
						CHK_BLACK.Checked = false; 

					if(conn.GetFieldValue("PRESCRE_CHECK") == "1")    
						CHK_PRES.Checked = true;
					else
						CHK_PRES.Checked = false; 

					if(conn.GetFieldValue("DHBI_CHECK") == "1")    
						CHK_DHBI.Checked = true;
					else
						CHK_DHBI.Checked = false; 

					if(conn.GetFieldValue("PR_SPK") == "1")    
						CHK_SPK.Checked = true;
					else
						CHK_SPK.Checked = false; 

					if(conn.GetFieldValue("PR_KENDARA") == "1")    
						CHK_PR_KENDARA.Checked = true;
					else
						CHK_PR_KENDARA.Checked = false; 

					if(conn.GetFieldValue("REVOLVING") == "1")    
						CHK_REVOLVING.Checked = true;
					else
						CHK_REVOLVING.Checked = false; 

					if(conn.GetFieldValue("ROUND_APPROVAL") == "1")    
						CHK_ROUND_APPROVAL.Checked = true;
					else
						CHK_ROUND_APPROVAL.Checked = false; 

					if(conn.GetFieldValue("ACTIVE") == "1")    
						CHK_ACTIVE.Checked = true;
					else
						CHK_ACTIVE.Checked = false; 

					if(conn.GetFieldValue("DOC_RAB") == "1")    
						CHK_DOC_RAB.Checked = true;
					else
						CHK_DOC_RAB.Checked = false;
 
					if(conn.GetFieldValue("ALLOWCARDBUNDLING") == "1")    
						CHK_CARDBUNDLING.Checked = true;
					else
						CHK_CARDBUNDLING.Checked = false; 

					if(conn.GetFieldValue("PR_MITRAKARYA") == "1")    
						CHK_PR_MITRAKARYA.Checked = true;
					else
						CHK_PR_MITRAKARYA.Checked = false; 

					TXT_NPWP.Text = conn.GetFieldValue("MIN_LIMIT_NPWP");
					//TXT_DP.Text = cleansText(e.Item.Cells[12].Text.Replace(",","."));
					TXT_DP.Text = conn.GetFieldValue("DOWN_PAYMENT");
					TXT_SPPK_LM.Text = conn.GetFieldValue("PR_SPPKLMTTIME");
					TXT_AIP_LM.Text = conn.GetFieldValue("PR_AIPLIMITTIME");
					TXT_ADMIN_FEE.Text = conn.GetFieldValue("PR_ADMIN");
					//TXT_FLOOR_RATE.Text = cleansText(e.Item.Cells[15].Text.Replace(",","."));
					TXT_FLOOR_RATE.Text = conn.GetFieldValue("FLOOR_RATE");
					TXT_FLOOR_LIMIT.Text = conn.GetFieldValue("FLOOR_LIMIT");
					TXT_CEIL_LIMIT.Text = conn.GetFieldValue("CEILLING_LIMIT");
					TXT_PROVISI.Text = conn.GetFieldValue("PR_PROVISI");
					//TXT_PROV_RATE.Text = cleansText(e.Item.Cells[19].Text.Replace(",","."));
					TXT_PROV_RATE.Text = conn.GetFieldValue("PR_PROVPERCENT");
					TXT_FIDUCIA.Text = conn.GetFieldValue("PR_FIDUCIA");
					TXT_FIDUCIA_LIM.Text = conn.GetFieldValue("PR_LIMITFIDUCIA");
					TXT_BEA_OTHER.Text = conn.GetFieldValue("PR_BEAOTH");
					TXT_EMAS_CODE.Text = conn.GetFieldValue("CD_SIBS");
					TXT_MARKET.Text = conn.GetFieldValue("PR_SRCCODE");
						
					//FOR CHILDPRODUCT
					try
					{
						DDL_CHILDPRODUCT.SelectedValue = conn.GetFieldValue("CHILDPRODUCT");
					} 
					catch{}
					TXT_CHILDMINTENOR.Text = conn.GetFieldValue("CHILDMINTENOR");
					TXT_CHILDMAXTENOR.Text = conn.GetFieldValue("CHILDMAXTENOR");
					TXT_CHILDDEFTENOR.Text = conn.GetFieldValue("CHILDDEFTENOR");
					TXT_CHILDMINRATIO.Text = conn.GetFieldValue("CHILDMINRATIO");
					TXT_CHILDMAXRATIO.Text = conn.GetFieldValue("CHILDMAXRATIO");
					TXT_CHILDDEFRATIO.Text = conn.GetFieldValue("CHILDDEFRATIO");
					TXT_CHILDMININTEREST.Text = conn.GetFieldValue("CHILDMININTEREST");
					TXT_CHILDMAXINTEREST.Text = conn.GetFieldValue("CHILDMAXINTEREST");
					TXT_CHILDDEFINTEREST.Text = conn.GetFieldValue("CHILDDEFINTEREST");
					TXT_CHILDMINLIMIT.Text = conn.GetFieldValue("CHILDMINLIMIT");
					TXT_CHILDMAXLIMIT.Text = conn.GetFieldValue("CHILDMAXLIMIT");
				
					LBL_SAVEMODE.Text = "2";
					break;

				case "detail":
					string id = e.Item.Cells[0].Text.Trim();
					string st = "";

					string mid = Request.QueryString["ModuleId"];
					//add for new page detail
					GlobalTools.NewWindow(this, "ProductDetail.aspx?productid="+id+"&ch_sta="+st+"&table=VW_PARAM_TPRODUCT_MAKER&ModuleId="+mid,"Current_ProductDetail",false,true,900,600);
					break;
			}
		}

		private void DG2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string pid; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					pid = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM TTPRODUCT where PRODUCTID = '"+pid+"'";
					conn.ExecuteQuery();
					BindData2();
					break;

				case "edit":
					pid = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "SELECT * FROM TTPRODUCT where PRODUCTID = '"+pid+"'";
					conn.ExecuteQuery();

					if(e.Item.Cells[26].Text.Trim() == "DELETE")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						TXT_PR_CODE.Enabled = false;
						TXT_PR_CODE.Text = conn.GetFieldValue("PRODUCTID");				
						TXT_PRNAME.Text = conn.GetFieldValue("PRODUCTNAME");
					
						try
						{
							DDL_GID.SelectedValue = conn.GetFieldValue("GROUP_ID");
						}
						catch{ }

						try
						{
							DDL_CUST_TYPE.SelectedValue = conn.GetFieldValue("CUSTOMER_TYPE");
						}
						catch{ }

						try
						{
							DDL_PROMO.SelectedValue = conn.GetFieldValue("GROUP_PROMO"); 
						}
						catch{ }

						if(conn.GetFieldValue("NL_CHECK") == "1")    
							CHK_NEGLST.Checked = true;
						else
							CHK_NEGLST.Checked = false; 

						if(conn.GetFieldValue("BLACKLIST_CHECK") == "1")    
							CHK_BLACK.Checked = true;
						else
							CHK_BLACK.Checked = false; 

						if(conn.GetFieldValue("PRESCRE_CHECK") == "1")    
							CHK_PRES.Checked = true;
						else
							CHK_PRES.Checked = false; 

						if(conn.GetFieldValue("DHBI_CHECK") == "1")    
							CHK_DHBI.Checked = true;
						else
							CHK_DHBI.Checked = false; 

						if(conn.GetFieldValue("PR_SPK") == "1")    
							CHK_SPK.Checked = true;
						else
							CHK_SPK.Checked = false; 

						if(conn.GetFieldValue("PR_KENDARA") == "1")    
							CHK_PR_KENDARA.Checked = true;
						else
							CHK_PR_KENDARA.Checked = false; 

						if(conn.GetFieldValue("REVOLVING") == "1")    
							CHK_REVOLVING.Checked = true;
						else
							CHK_REVOLVING.Checked = false; 

						if(conn.GetFieldValue("ROUND_APPROVAL") == "1")    
							CHK_ROUND_APPROVAL.Checked = true;
						else
							CHK_ROUND_APPROVAL.Checked = false; 

						if(conn.GetFieldValue("ACTIVE") == "1")    
							CHK_ACTIVE.Checked = true;
						else
							CHK_ACTIVE.Checked = false; 

						if(conn.GetFieldValue("DOC_RAB") == "1")    
							CHK_DOC_RAB.Checked = true;
						else
							CHK_DOC_RAB.Checked = false;
 
						if(conn.GetFieldValue("ALLOWCARDBUNDLING") == "1")    
							CHK_CARDBUNDLING.Checked = true;
						else
							CHK_CARDBUNDLING.Checked = false; 

						if(conn.GetFieldValue("PR_MITRAKARYA") == "1")    
							CHK_PR_MITRAKARYA.Checked = true;
						else
							CHK_PR_MITRAKARYA.Checked = false; 

						TXT_NPWP.Text = conn.GetFieldValue("MIN_LIMIT_NPWP");
						//TXT_DP.Text = cleansText(e.Item.Cells[12].Text.Replace(",","."));
						TXT_DP.Text = conn.GetFieldValue("DOWN_PAYMENT");
						TXT_SPPK_LM.Text = conn.GetFieldValue("PR_SPPKLMTTIME");
						TXT_AIP_LM.Text = conn.GetFieldValue("PR_AIPLIMITTIME");
						TXT_ADMIN_FEE.Text = conn.GetFieldValue("PR_ADMIN");
						//TXT_FLOOR_RATE.Text = cleansText(e.Item.Cells[15].Text.Replace(",","."));
						TXT_FLOOR_RATE.Text = conn.GetFieldValue("FLOOR_RATE");
						TXT_FLOOR_LIMIT.Text = conn.GetFieldValue("FLOOR_LIMIT");
						TXT_CEIL_LIMIT.Text = conn.GetFieldValue("CEILLING_LIMIT");
						TXT_PROVISI.Text = conn.GetFieldValue("PR_PROVISI");
						//TXT_PROV_RATE.Text = cleansText(e.Item.Cells[19].Text.Replace(",","."));
						TXT_PROV_RATE.Text = conn.GetFieldValue("PR_PROVPERCENT");
						TXT_FIDUCIA.Text = conn.GetFieldValue("PR_FIDUCIA");
						TXT_FIDUCIA_LIM.Text = conn.GetFieldValue("PR_LIMITFIDUCIA");
						TXT_BEA_OTHER.Text = conn.GetFieldValue("PR_BEAOTH");
						TXT_EMAS_CODE.Text = conn.GetFieldValue("CD_SIBS");
						TXT_MARKET.Text = conn.GetFieldValue("PR_SRCCODE");
						
						//FOR CHILDPRODUCT
						try
						{
							DDL_CHILDPRODUCT.SelectedValue = conn.GetFieldValue("CHILDPRODUCT");
						} 
						catch{}
						TXT_CHILDMINTENOR.Text = conn.GetFieldValue("CHILDMINTENOR");
						TXT_CHILDMAXTENOR.Text = conn.GetFieldValue("CHILDMAXTENOR");
						TXT_CHILDDEFTENOR.Text = conn.GetFieldValue("CHILDDEFTENOR");
						TXT_CHILDMINRATIO.Text = conn.GetFieldValue("CHILDMINRATIO");
						TXT_CHILDMAXRATIO.Text = conn.GetFieldValue("CHILDMAXRATIO");
						TXT_CHILDDEFRATIO.Text = conn.GetFieldValue("CHILDDEFRATIO");
						TXT_CHILDMININTEREST.Text = conn.GetFieldValue("CHILDMININTEREST");
						TXT_CHILDMAXINTEREST.Text = conn.GetFieldValue("CHILDMAXINTEREST");
						TXT_CHILDDEFINTEREST.Text = conn.GetFieldValue("CHILDDEFINTEREST");
						TXT_CHILDMINLIMIT.Text = conn.GetFieldValue("CHILDMINLIMIT");
						TXT_CHILDMAXLIMIT.Text = conn.GetFieldValue("CHILDMAXLIMIT");
						
						LBL_SAVEMODE.Text = "2";
					}
					break;

				case "detail":
					string id = e.Item.Cells[0].Text.Trim();
					string st = e.Item.Cells[27].Text.Trim();

					string mid = Request.QueryString["ModuleId"];
					//add for new page detail
					GlobalTools.NewWindow(this, "ProductDetail.aspx?productid="+id+"&ch_sta="+st+"&table=VW_PARAM_TPRODUCT_PENDING&ModuleId="+mid,"Pending_ProductDetail",false,true,900,600);
					break;
			}		
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40"); 
		}

		
	}
}
