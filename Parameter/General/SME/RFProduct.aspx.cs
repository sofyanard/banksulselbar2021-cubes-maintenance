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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFProduct.
	/// </summary>
	public partial class RFProduct : System.Web.UI.Page
	{

		protected Tools tool = new Tools();
		protected Connection conn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);//(Connection) Session["Connection"];
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

			//if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
			//	Response.Redirect("/SME/Restricted.aspx");

			if (!IsPostBack)
			{
				//INTEREST TYPE
				conn.QueryString = "SELECT * FROM RFINTERESTTYPE ";
				conn.ExecuteQuery();
				DDL_INTERESTTYPE.Items.Clear();
				DDL_INTERESTTYPE.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_INTERESTTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				//INTSTALLMENT TYPE
				conn.QueryString = "SELECT * FROM RFINSTALLMENTTYPE WHERE ACTIVE = '1' ";
				conn.ExecuteQuery();
				DDL_INSTALLMENTTYPE.Items.Clear();
				DDL_INSTALLMENTTYPE.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_INSTALLMENTTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				//SIBS CURRENCY
				conn.QueryString = "select CURRENCYID, CURRENCYDESC from RFCURRENCY WHERE ACTIVE = '1' ";
				conn.ExecuteQuery();
				DDL_CURRENCY.Items.Clear();
				DDL_CURRENCY.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_CURRENCY.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				//CALCULATION METHOD
				conn.QueryString = "select CALCMETHODDESC, CALCMETHODDESC from RFCALCMETHOD ";
				conn.ExecuteQuery();
				DDL_CALCMETHOD.Items.Clear();
				DDL_CALCMETHOD.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_CALCMETHOD.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				//PRODUCT TYPE
				conn.QueryString = "select JENISID, JENISDESC from RFJENISPRODUCT WHERE ACTIVE = '1' ";
				conn.ExecuteQuery();
				DDL_JNSPRODUCT.Items.Clear();
				DDL_JNSPRODUCT.Items.Add(new ListItem("-- Pilih --", ""));
				for (int i=0; i<conn.GetRowCount(); i++)
					DDL_JNSPRODUCT.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));


				fillRateNumber();
				bindData1();
				bindData2();
			}
			Datagrid1.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change1);
			DataGrid2.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change2);
		}

		private void fillRateNumber()
		{
			DDL_RATENO.Items.Clear();
			DDL_RATE.Items.Clear();
			TXT_RATE.Text = "";

			DDL_RATENO.Items.Add(new ListItem("-- Pilih --", ""));
			DDL_RATE.Items.Add(new ListItem("-- Pilih --", ""));

			if (DDL_CURRENCY.SelectedValue == "")//||(TXT_SIBS_PRODCODE.Text.Trim() == ""))
				return;

			/*conn.QueryString = "select RATENO, RATE, CURRENCYID, CURRENCYDESC from rfsibsprodid " +
				"left join rfratenumber on rfratenumber.sibs_prodcode = rfsibsprodid.sibsproddesc " +
				"and rfratenumber.currency = rfsibsprodid.currency " +
				"left join rfcurrency on rfcurrency.currencyid = rfsibsprodid.currency " +
				"where sibsprodid = '" + DDL_SIBS_PRODCODE.SelectedValue.Trim() + "' ";
				*/
			conn.QueryString = "select RATENO, RATE * 100 from rfratenumber " +
				"where CURRENCY = '" + DDL_CURRENCY.SelectedValue.Trim() +
				//"' AND SIBS_PRODCODE = '" + TXT_SIBS_PRODCODE.Text.Trim() +
				"' AND ACTIVE = '1' ";
			conn.ExecuteQuery();
			for (int i=0; i<conn.GetRowCount(); i++)
			{
				DDL_RATENO.Items.Add(new ListItem(conn.GetFieldValue(i, 0), conn.GetFieldValue(i, 0)));
				DDL_RATE.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
			}
		}

		private void bindData1()
		{
			DataTable dt = new DataTable();
			DataRow dr;
			conn.QueryString = "SELECT * FROM VW_PARAM_GENERAL_RFPRODUCT ";
			conn.ExecuteQuery();

			dt.Columns.Add(new DataColumn("PRODUCTID"));
			dt.Columns.Add(new DataColumn("PRODUCTDESC"));
			dt.Columns.Add(new DataColumn("SIBS_PRODCODE"));
			dt.Columns.Add(new DataColumn("SIBS_PRODID"));
			dt.Columns.Add(new DataColumn("CURRENCY"));
			dt.Columns.Add(new DataColumn("CURRENCYDESC"));
			dt.Columns.Add(new DataColumn("INTERESTREST"));
			dt.Columns.Add(new DataColumn("CALCMETHOD"));
			dt.Columns.Add(new DataColumn("CALCMETHODDESC"));
			dt.Columns.Add(new DataColumn("ISCASHLOAN"));
			dt.Columns.Add(new DataColumn("ISCASHLOANDESC"));
			dt.Columns.Add(new DataColumn("REVOLVING"));
			dt.Columns.Add(new DataColumn("REVOLVINGDESC"));
			dt.Columns.Add(new DataColumn("INTERESTTYPE"));
			dt.Columns.Add(new DataColumn("INTERESTTYPEDESC"));
			dt.Columns.Add(new DataColumn("INTERESTTYPERATE"));
			dt.Columns.Add(new DataColumn("RATENO"));
			dt.Columns.Add(new DataColumn("RATE"));
			dt.Columns.Add(new DataColumn("VARCODE"));
			dt.Columns.Add(new DataColumn("VARIANCE"));
			dt.Columns.Add(new DataColumn("SPK"));
			dt.Columns.Add(new DataColumn("SPKDESC"));
			dt.Columns.Add(new DataColumn("ISINSTALLMENT"));
			dt.Columns.Add(new DataColumn("ISINSTALLMENTDESC"));
			dt.Columns.Add(new DataColumn("INSTALMENTTYPE"));
			dt.Columns.Add(new DataColumn("INSTALMENTTYPEDESC"));
			dt.Columns.Add(new DataColumn("CONFIRMKORAN"));
			dt.Columns.Add(new DataColumn("CONFIRMKORANDESC"));
			dt.Columns.Add(new DataColumn("ISUPLOADEMAS"));
			dt.Columns.Add(new DataColumn("ISUPLOADEMASDESC"));
			dt.Columns.Add(new DataColumn("IDCFLAG"));
			dt.Columns.Add(new DataColumn("FIRSTINSTALLDATE"));
			dt.Columns.Add(new DataColumn("JNSPRODUCT"));
			dt.Columns.Add(new DataColumn("SUPPORTSUBAPP"));
			dt.Columns.Add(new DataColumn("SUPPORTEBIZ"));
			dt.Columns.Add(new DataColumn("ISSUBAPPPROD"));
			dt.Columns.Add(new DataColumn("INTERESTMODE"));
			dt.Columns.Add(new DataColumn("ISCHANGEINTERESTALLOW"));
			dt.Columns.Add(new DataColumn("ISNEGORATE"));
			dt.Columns.Add(new DataColumn("ALTRATECALC"));
			dt.Columns.Add(new DataColumn("NCL_CODE"));
			dt.Columns.Add(new DataColumn("ACTIVE")); //col 41
            dt.Columns.Add(new DataColumn("IS_AGUNAN"));
            dt.Columns.Add(new DataColumn("IS_AGUNANDESC"));
            dt.Columns.Add(new DataColumn("LIMIT_ATAS"));
            dt.Columns.Add(new DataColumn("LIMIT_BAWAH"));
            dt.Columns.Add(new DataColumn("IS_PRK"));
            dt.Columns.Add(new DataColumn("IS_PRKDESC"));

			for (int i = 0; i < conn.GetRowCount(); i++) 
			{
				dr = dt.NewRow();
				for (int j = 0; j < conn.GetColumnCount(); j++)
				{
					dr[j] = conn.GetFieldValue(i,j);
				}
				dt.Rows.Add(dr);
			}
			Datagrid1.DataSource = new DataView(dt);
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
				if (Datagrid1.Items[i].Cells[41].Text.Trim() =="0" )
				{

					LinkButton l_del = (LinkButton) Datagrid1.Items[i].FindControl("lnk_RfDelete");
					l_del.CommandName = "Undelete";
					l_del.Text = "Undelete";

					LinkButton l_edit = (LinkButton) Datagrid1.Items[i].FindControl("lnk_RfEdit");
					l_edit.Visible = false;
				}
			}
		}

		private void bindData2()
		{
			DataTable dt = new DataTable();
			DataRow dr;
			conn.QueryString = "SELECT * FROM VW_PARAM_GENERAL_PENDING_RFPRODUCT ";
			conn.ExecuteQuery();

			dt.Columns.Add(new DataColumn("PRODUCTID"));
			dt.Columns.Add(new DataColumn("PRODUCTDESC"));
			dt.Columns.Add(new DataColumn("SIBS_PRODCODE"));
			dt.Columns.Add(new DataColumn("SIBS_PRODID"));
			dt.Columns.Add(new DataColumn("CURRENCY"));
			dt.Columns.Add(new DataColumn("CURRENCYDESC"));
			dt.Columns.Add(new DataColumn("INTERESTREST"));
			dt.Columns.Add(new DataColumn("CALCMETHOD"));
			dt.Columns.Add(new DataColumn("CALCMETHODDESC"));
			dt.Columns.Add(new DataColumn("ISCASHLOAN"));
			dt.Columns.Add(new DataColumn("ISCASHLOANDESC"));
			dt.Columns.Add(new DataColumn("REVOLVING"));
			dt.Columns.Add(new DataColumn("REVOLVINGDESC"));
			dt.Columns.Add(new DataColumn("INTERESTTYPE"));
			dt.Columns.Add(new DataColumn("INTERESTTYPEDESC"));
			dt.Columns.Add(new DataColumn("INTERESTTYPERATE"));
			dt.Columns.Add(new DataColumn("RATENO"));
			dt.Columns.Add(new DataColumn("RATE"));
			dt.Columns.Add(new DataColumn("VARCODE"));
			dt.Columns.Add(new DataColumn("VARIANCE"));
			dt.Columns.Add(new DataColumn("SPK"));
			dt.Columns.Add(new DataColumn("SPKDESC"));
			dt.Columns.Add(new DataColumn("ISINSTALLMENT"));
			dt.Columns.Add(new DataColumn("ISINSTALLMENTDESC"));
			dt.Columns.Add(new DataColumn("INSTALMENTTYPE"));
			dt.Columns.Add(new DataColumn("INSTALMENTTYPEDESC"));
			dt.Columns.Add(new DataColumn("CONFIRMKORAN"));
			dt.Columns.Add(new DataColumn("CONFIRMKORANDESC"));
			dt.Columns.Add(new DataColumn("ISUPLOADEMAS"));
			dt.Columns.Add(new DataColumn("ISUPLOADEMASDESC"));
			dt.Columns.Add(new DataColumn("IDCFLAG"));
			dt.Columns.Add(new DataColumn("FIRSTINSTALLDATE"));
			dt.Columns.Add(new DataColumn("JNSPRODUCT"));
			dt.Columns.Add(new DataColumn("SUPPORTSUBAPP"));
			dt.Columns.Add(new DataColumn("SUPPORTEBIZ"));
			dt.Columns.Add(new DataColumn("ISSUBAPPPROD"));
			dt.Columns.Add(new DataColumn("INTERESTMODE"));
			dt.Columns.Add(new DataColumn("ISCHANGEINTERESTALLOW"));
			dt.Columns.Add(new DataColumn("ISNEGORATE"));
			dt.Columns.Add(new DataColumn("ALTRATECALC"));
			dt.Columns.Add(new DataColumn("NCL_CODE"));
			dt.Columns.Add(new DataColumn("ACTIVE"));

            dt.Columns.Add(new DataColumn("IS_AGUNAN"));
            dt.Columns.Add(new DataColumn("IS_AGUNANDESC"));
            dt.Columns.Add(new DataColumn("LIMIT_ATAS"));
            dt.Columns.Add(new DataColumn("LIMIT_BAWAH"));
            dt.Columns.Add(new DataColumn("IS_PRK"));
            dt.Columns.Add(new DataColumn("IS_PRKDESC"));

			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("STATUSDESC"));

			for (int i = 0; i < conn.GetRowCount(); i++) 
			{
				dr = dt.NewRow();
				for (int j = 0; j < conn.GetColumnCount(); j++)
				{
					dr[j] = conn.GetFieldValue(i,j);
				}
				dt.Rows.Add(dr);
			}
			DataGrid2.DataSource = new DataView(dt);
			try 
			{
				DataGrid2.DataBind();
			}
			catch 
			{
				DataGrid2.CurrentPageIndex = DataGrid2.PageCount - 1;
				DataGrid2.DataBind();
			}
		}

		private void clearEditBoxes()
		{
			TXT_PRODUCTID.Text = "";
			TXT_PRODUCTDESC.Text = "";
			TXT_SIBS_PRODCODE.Text = "";
			TXT_RATE.Text = "";
			TXT_VARIANCE.Text = "";
			TXT_INTERESTTYPERATE.Text = "";
			TXT_SIBS_PRODID.Text = "";
			try 
			{
				DDL_CURRENCY.SelectedIndex = 0;
			} 
			catch {}
			try 
			{
				DDL_CALCMETHOD.SelectedIndex = 0;
			} 
			catch {}
			try 
			{
				DDL_INTERESTREST.SelectedIndex = 0;
			} 
			catch {}
			fillRateNumber();
			try
			{
				RDO_REVOLVING.SelectedIndex = 0;
			} 
			catch {}
			try 
			{
				RDO_ISCASHLOAN.SelectedIndex = 0;
			} 
			catch {}
			try
			{
				RDO_VARCODE.SelectedIndex = 0; 
			} 
			catch {}
			try 
			{
				RDO_SPK.SelectedIndex = 0;
			} 
			catch {}
			try 
			{
				RDO_ISINSTALLMENT.SelectedIndex = 0;
			} 
			catch {}
			try 
			{
				DDL_INTERESTTYPE.SelectedIndex = 0;
			} 
			catch {}
			try 
			{
				DDL_INSTALLMENTTYPE.SelectedIndex = 0;
			} 
			catch {}
			try 
			{
				RDO_CONFIRMKORAN.SelectedIndex = 0;
			} 
			catch {}
			try 
			{
				RDO_ISUPLOADEMAS.SelectedIndex = 0;
			} 
			catch {}
			TXT_NCL_CODE.Text = "";

            try
            {
                RDO_AGUNAN.SelectedIndex = 0;
            }
            catch
            {
            }

            try
            {
                RDO_PRK.SelectedIndex = 0;
            }
            catch { }

            TXT_LIMIT_ATAS.Text = "";
            TXT_LIMIT_BAWAH.Text = "";

			LBL_SAVEMODE.Text = "1";
			activatePostBackControls(true);
		}

		private void activatePostBackControls(bool mode)
		{
			TXT_PRODUCTID.Enabled = mode;
		}

		private void cleansTextBox (TextBox tb)
		{
			if (tb.Text.Trim() == "&nbsp;")
				tb.Text = "";
		}

		private void enInterestVal(string intmode)
		{
			switch(intmode)
			{
				case "01" :
					TXT_INTERESTTYPERATE.Enabled = false;
					TXT_INTERESTTYPERATE.Text = "";
					DDL_RATENO.Enabled = true;
					RDO_VARCODE.Enabled = true;
					TXT_VARIANCE.Enabled = true;
					break;
				case "02" : // Fixed
					TXT_INTERESTTYPERATE.Enabled = true;
					TXT_INTERESTTYPERATE.Text = "";
					DDL_RATENO.Enabled = false;
					RDO_VARCODE.Enabled = false;
					TXT_VARIANCE.Enabled = false;
					TXT_VARIANCE.Text = "";
					break;
				default : // Alternate Rate
					TXT_INTERESTTYPERATE.Enabled = false;
					DDL_RATENO.Enabled = false;
					RDO_VARCODE.Enabled = false;
					TXT_VARIANCE.Enabled = false;
					try
					{
						DDL_RATENO.SelectedIndex = 0;
					} 
					catch {}
					try
					{
						RDO_VARCODE.SelectedIndex = 0;
					} 
					catch {}
					TXT_VARIANCE.Text = "";
					TXT_RATE.Text = "";
					break;
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
			this.Datagrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_ItemCommand);
			this.DataGrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_ItemCommand);

		}
		#endregion

		void Grid_Change1(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid1.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData1();	
		}

		void Grid_Change2(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DataGrid2.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData2();	
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
            if (DDL_CURRENCY.SelectedIndex != 0 ||
                DDL_CALCMETHOD.SelectedIndex != 0 ||
                DDL_INTERESTREST.SelectedIndex != 0)
            {
                string VARIANCE = TXT_VARIANCE.Text.Trim(),
                    INTERESTTYPERATE = TXT_INTERESTTYPERATE.Text.Trim(),
                    varcode = RDO_VARCODE.SelectedValue.Trim(),
                    rateno = DDL_RATENO.SelectedValue.Trim();
                string active = "0";

                if (TXT_PRODUCTID.Text.Trim() == "")
                {
                    Tools.popMessage(this, "Product Code has not been set!");
                    return;
                }
                if (LBL_SAVEMODE.Text.Trim() == "1")
                {
                    conn.QueryString = "SELECT active FROM RFPRODUCT WHERE PRODUCTID = '" +
                        TXT_PRODUCTID.Text.Trim() + "' ";
                    conn.ExecuteQuery();
                    if (conn.GetRowCount() > 0)
                    {
                        active = conn.GetFieldValue("active");
                        if (active == "1")
                        {
                            Tools.popMessage(this, "Product Code has already been used! Request canceled!");
                            return;
                        }
                        else
                        {
                            LBL_SAVEMODE.Text = "0";

                        }
                    }
                }
                if (DDL_INTERESTTYPE.SelectedValue.Trim() == "01")	//floating
                {
                    rateno = "'" + rateno + "'";
                    varcode = "'" + varcode + "'";
                    VARIANCE = tool.ConvertFloat(VARIANCE);
                    INTERESTTYPERATE = "NULL";
                }
                else
                {
                    rateno = "NULL";
                    varcode = "NULL";
                    VARIANCE = "NULL";
                    INTERESTTYPERATE = tool.ConvertFloat(INTERESTTYPERATE);
                }

                conn.QueryString = "PARAM_GENERAL_RFPRODUCT_MAKER '" +
                    LBL_SAVEMODE.Text.Trim() + "', '" +
                    TXT_PRODUCTID.Text.Trim() + "', '" +
                    TXT_PRODUCTDESC.Text.Trim() + "', '" +
                    DDL_CURRENCY.SelectedValue.Trim() + "', '" +
                    DDL_CALCMETHOD.SelectedValue.Trim() + "', " +
                    "NULL, '" +
                    RDO_REVOLVING.SelectedValue.Trim() + "', '" +
                    RDO_ISCASHLOAN.SelectedValue.Trim() + "', " +
                    "NULL, '" +
                    TXT_SIBS_PRODCODE.Text.Trim() + "', '" +
                    "1', " +
                    rateno + ", " +
                    varcode + ", " +
                    VARIANCE + ", '" +
                    RDO_SPK.SelectedValue.Trim() + "', '" +
                    DDL_INTERESTREST.SelectedValue.Trim() + "', '" +
                    RDO_ISINSTALLMENT.SelectedValue.Trim() + "', '" +
                    DDL_INTERESTTYPE.SelectedValue.Trim() + "', " +
                    "NULL, " +
                    INTERESTTYPERATE + ", '" +
                    TXT_SIBS_PRODID.Text.Trim() + "', '" +
                    DDL_INSTALLMENTTYPE.SelectedValue.Trim() + "', '" +
                    RDO_CONFIRMKORAN.SelectedValue.Trim() + "', '" +
                    DDL_JNSPRODUCT.SelectedValue.Trim() + "', " +
                    "NULL, " +
                    "NULL, " +
                    "NULL, " +
                    "NULL, " +
                    "NULL, " +
                    "NULL, '" +
                    RDO_ISUPLOADEMAS.SelectedValue.Trim() + "', '" +
                    TXT_NCL_CODE.Text.Trim() + "', '" +
                    //crpundi
                    RDO_AGUNAN.SelectedValue.Trim() + "', '" +    //IS_AGUNAN
                    TXT_LIMIT_ATAS.Text.Trim() + "', '" +         //LIMIT_ATAS
                    TXT_LIMIT_BAWAH.Text.Trim() + "', '" +        //LIMIT_BAWAH
                    RDO_PRK.SelectedValue.Trim() + "'";
                conn.QueryString = conn.QueryString.Replace("'&nbsp;'", "NULL");
                conn.ExecuteQuery();

                // menghapus data preset_rate jika ada dan jika interesttype != alternate rate
                if ((DDL_INTERESTTYPE.SelectedValue != "03") || (RBL_NEGO.SelectedValue != "0"))
                {
                    clearAlternateRate();
                }

                bindData2();
                clearEditBoxes();
                //Tools.popMessage(this, "Data added for insertion/modification!");
            }
            else
            {
                Tools.popMessage(this, "Currency, Calculation Method dan Interest Rate Mohon diisi !");
            }
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearAlternateRate();
			clearEditBoxes();
		}

		private void clearAlternateRate()
		{
			try
			{
				//hapus preset alternaterate jika ada
				conn.QueryString  = "delete * from RFPRODUCT_PRESETRATE ";
				conn.QueryString += "where PRODUCTID = '" + TXT_PRODUCTID.Text.Trim() + "'";
				conn.ExecuteQuery();
			}
			catch(Exception)
			{
				//GlobalTools.popMessage(this, "Server Error when it's trying to delete record from Preset Alternate Rate");
			}
		}

		private void Datagrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string VARIANCE, INTERESTTYPERATE, varcode,REVOLVING, rateno;
			string ISCASHLOAN,SPK,ISINSTALLMENT,CONFIRMKORAN,ISUPLOADEMAS;
					

			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					TXT_PRODUCTID.Text = e.Item.Cells[0].Text.Trim();
					TXT_PRODUCTDESC.Text = e.Item.Cells[1].Text.Trim();
					TXT_SIBS_PRODCODE.Text = e.Item.Cells[2].Text.Trim();
					TXT_SIBS_PRODID.Text = e.Item.Cells[3].Text.Trim();
					try
					{
						DDL_CURRENCY.SelectedValue = e.Item.Cells[4].Text.Trim();
					}
					catch{}
					try
					{
						DDL_CALCMETHOD.SelectedValue = e.Item.Cells[7].Text.Trim();
					}
					catch{}
					fillRateNumber();
					try
					{
						DDL_RATENO.SelectedValue = e.Item.Cells[16].Text.Trim();
					} 
					catch {}
					try
					{
						DDL_RATE.SelectedValue = e.Item.Cells[16].Text.Trim();
						TXT_RATE.Text = DDL_RATE.SelectedItem.Text;
					} 
					catch {}
					try
					{
						DDL_INTERESTREST.SelectedValue = e.Item.Cells[6].Text.Trim();
					} 
					catch {}
					try
					{
						RDO_REVOLVING.SelectedValue = e.Item.Cells[11].Text.Trim();
					} 
					catch {}
					try
					{
						RDO_ISCASHLOAN.SelectedValue = e.Item.Cells[9].Text.Trim();
					} 
					catch {}
					try
					{
						RDO_VARCODE.SelectedValue = e.Item.Cells[18].Text.Trim();
					} 
					catch {}
					TXT_VARIANCE.Text = e.Item.Cells[19].Text.Trim();
					try
					{
						RDO_SPK.SelectedValue = e.Item.Cells[20].Text.Trim();
					} 
					catch {}
					try
					{
						RDO_ISINSTALLMENT.SelectedValue = e.Item.Cells[22].Text.Trim();
					} 
					catch {}
					try 
					{
						DDL_INTERESTTYPE.SelectedValue = e.Item.Cells[13].Text.Trim();
					} 
					catch {}
					try 
					{
						DDL_INSTALLMENTTYPE.SelectedValue = e.Item.Cells[24].Text.Trim();
					} 
					catch {}
					try
					{
						RDO_CONFIRMKORAN.SelectedValue = e.Item.Cells[26].Text.Trim();
					} 
					catch {}
					try
					{
						RDO_ISUPLOADEMAS.SelectedValue = e.Item.Cells[29].Text.Trim();
					}
					catch {}
					
					TXT_INTERESTTYPERATE.Text = e.Item.Cells[15].Text.Trim();
					TXT_NCL_CODE.Text = e.Item.Cells[40].Text.Trim();

                    //[-- crpundi
                    try
                    {
                        RDO_AGUNAN.SelectedValue = e.Item.Cells[42].Text.Trim();
                    }
                    catch { }
                    TXT_LIMIT_ATAS.Text = e.Item.Cells[44].Text.Trim();
                    TXT_LIMIT_BAWAH.Text = e.Item.Cells[45].Text.Trim();

                    try
                    {
                        RDO_PRK.SelectedValue = e.Item.Cells[46].Text.Trim();
                    }
                    catch { }
                    //--]

					cleansTextBox(TXT_PRODUCTID);
					cleansTextBox(TXT_PRODUCTDESC);
					cleansTextBox(TXT_SIBS_PRODCODE);
					cleansTextBox(TXT_SIBS_PRODID);
					cleansTextBox(TXT_RATE);
					cleansTextBox(TXT_VARIANCE);
					cleansTextBox(TXT_INTERESTTYPERATE);
					cleansTextBox(TXT_NCL_CODE);
                    //[-- crpundi
                    cleansTextBox(TXT_LIMIT_ATAS);
                    cleansTextBox(TXT_LIMIT_BAWAH);
                    //--]
					activatePostBackControls(false);
					enInterestVal(DDL_INTERESTTYPE.SelectedValue.Trim());
					break;
				case "delete":
					conn.QueryString = "PARAM_GENERAL_RFPRODUCT_MAKER '2', '" +
						e.Item.Cells[0].Text.Trim() + "', '" + //@PRODUCTID
						e.Item.Cells[1].Text.Trim() + "', '" + //@PRODUCTDESC
						e.Item.Cells[4].Text.Trim() + "', '" + //@CURRENCY
						e.Item.Cells[7].Text.Trim() + "', '" + //@CALCMETHOD
						e.Item.Cells[36].Text.Trim() + "', '" + //@INTERESTMODE
						e.Item.Cells[11].Text.Trim() + "', '" + //@REVOLVING
						e.Item.Cells[9].Text.Trim() + "', '" + //@ISCASHLOAN
						e.Item.Cells[30].Text.Trim() + "', '" + //@IDCFLAG
						e.Item.Cells[2].Text.Trim() + "', '" + //@SIBS_PRODCODE
						e.Item.Cells[41].Text.Trim() + "', '" + //@ACTIVE
						e.Item.Cells[16].Text.Trim() + "', '" + //@RATENO
						e.Item.Cells[18].Text.Trim() + "', '" + //@VARCODE
						e.Item.Cells[19].Text.Trim() + "', '" + //@VARIANCE
						e.Item.Cells[20].Text.Trim() + "', '" + //@SPK
						e.Item.Cells[6].Text.Trim() + "', '" + //@INTERESTREST
						e.Item.Cells[22].Text.Trim() + "', '" + //@ISINSTALLMENT
						e.Item.Cells[13].Text.Trim() + "', '" + //@INTERESTTYPE
						e.Item.Cells[31].Text.Trim() + "', '" + //@FIRSTINSTALLDATE
						e.Item.Cells[15].Text.Trim() + "', '" + //@INTERESTTYPERATE
						e.Item.Cells[3].Text.Trim() + "', '" + //@SIBS_PRODID
						e.Item.Cells[24].Text.Trim() + "', '" + //@INSTALMENTTYPE
						e.Item.Cells[26].Text.Trim() + "', '" + //@CONFIRMKORAN
						e.Item.Cells[32].Text.Trim() + "', '" + //@JNSPRODUCT
						e.Item.Cells[33].Text.Trim() + "', '" + //@SUPPORTSUBAPP
						e.Item.Cells[34].Text.Trim() + "', '" + //@SUPPORTEBIZ
						e.Item.Cells[35].Text.Trim() + "', '" + //@ISSUBAPPPROD
						e.Item.Cells[37].Text.Trim() + "', '" + //@ISCHANGEINTERESTALLOW
						e.Item.Cells[38].Text.Trim() + "', '" + //@ISNEGORATE
						e.Item.Cells[39].Text.Trim() + "', '" + //@ALTRATECALC
						e.Item.Cells[28].Text.Trim() + "', '" + //@ISUPLOADEMAS
                        e.Item.Cells[40].Text.Trim() + "', '" + //@NCL_CODE
                        //crpundi
                        e.Item.Cells[42].Text.Trim() + "', '" +   //@IS_AGUNAN
                        e.Item.Cells[44].Text.Trim() + "', '" +   //@LIMIT_ATAS
                        e.Item.Cells[45].Text.Trim() + "', '" +  //@LIMIT_BAWAH
                        e.Item.Cells[46].Text.Trim() + "'";      //@IS_PRK
                    conn.QueryString = conn.QueryString.Replace("'&nbsp;'", "NULL");
					conn.ExecuteQuery();
					bindData2();
					//Tools.popMessage(this, "Data added for deletion!");
					break;

				case "Undelete":
					conn.QueryString = "PARAM_GENERAL_RFPRODUCT_MAKER '0', '" +
						e.Item.Cells[0].Text.Trim() + "', '" + //@PRODUCTID
						e.Item.Cells[1].Text.Trim() + "', '" + //@PRODUCTDESC
						e.Item.Cells[4].Text.Trim() + "', '" + //@CURRENCY
						e.Item.Cells[7].Text.Trim() + "', '" + //@CALCMETHOD
						e.Item.Cells[36].Text.Trim() + "', '" + //@INTERESTMODE
						e.Item.Cells[11].Text.Trim() + "', '" + //@REVOLVING
						e.Item.Cells[9].Text.Trim() + "', '" + //@ISCASHLOAN
						e.Item.Cells[30].Text.Trim() + "', '" + //@IDCFLAG
						e.Item.Cells[2].Text.Trim() + "', '" + //@SIBS_PRODCODE
						e.Item.Cells[41].Text.Trim() + "', '" + //@ACTIVE
						e.Item.Cells[16].Text.Trim() + "', '" + //@RATENO
						e.Item.Cells[18].Text.Trim() + "', '" + //@VARCODE
						e.Item.Cells[19].Text.Trim() + "', '" + //@VARIANCE
						e.Item.Cells[20].Text.Trim() + "', '" + //@SPK
						e.Item.Cells[6].Text.Trim() + "', '" + //@INTERESTREST
						e.Item.Cells[22].Text.Trim() + "', '" + //@ISINSTALLMENT
						e.Item.Cells[13].Text.Trim() + "', '" + //@INTERESTTYPE
						e.Item.Cells[31].Text.Trim() + "', '" + //@FIRSTINSTALLDATE
						e.Item.Cells[15].Text.Trim() + "', '" + //@INTERESTTYPERATE
						e.Item.Cells[3].Text.Trim() + "', '" + //@SIBS_PRODID
						e.Item.Cells[24].Text.Trim() + "', '" + //@INSTALMENTTYPE
						e.Item.Cells[26].Text.Trim() + "', '" + //@CONFIRMKORAN
						e.Item.Cells[32].Text.Trim() + "', '" + //@JNSPRODUCT
						e.Item.Cells[33].Text.Trim() + "', '" + //@SUPPORTSUBAPP
						e.Item.Cells[34].Text.Trim() + "', '" + //@SUPPORTEBIZ
						e.Item.Cells[35].Text.Trim() + "', '" + //@ISSUBAPPPROD
						e.Item.Cells[37].Text.Trim() + "', '" + //@ISCHANGEINTERESTALLOW
						e.Item.Cells[38].Text.Trim() + "', '" + //@ISNEGORATE
						e.Item.Cells[39].Text.Trim() + "', '" + //@ALTRATECALC
						e.Item.Cells[28].Text.Trim() + "', '" + //@ISUPLOADEMAS
						e.Item.Cells[40].Text.Trim() + "', '" + //@NCL_CODE
                        //crpundi
                        e.Item.Cells[42].Text.Trim() + "', '" +   //@IS_AGUNAN
                        e.Item.Cells[44].Text.Trim() + "', '" +   //@LIMIT_ATAS
                        e.Item.Cells[45].Text.Trim() + "', '" +  //@LIMIT_BAWAH
                        e.Item.Cells[46].Text.Trim() + "'";      //@IS_PRK
                    conn.QueryString = conn.QueryString.Replace("'&nbsp;'", "NULL");
					conn.ExecuteQuery();
					bindData2();
					//Tools.popMessage(this, "Data added for deletion!");
					break;
				
				default:
					// Do nothing.
					break;
			}
		}

		private void DataGrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
                    //LBL_SAVEMODE.Text = e.Item.Cells[25].Text.Trim();
                    LBL_SAVEMODE.Text = e.Item.Cells[46].Text.Trim();
                    //LBL_SAVEMODE.Text = "0";
                    if (LBL_SAVEMODE.Text.Trim() == "2")
                    {
                        LBL_SAVEMODE.Text = "1";
                        break;
                    }
					TXT_PRODUCTID.Text = e.Item.Cells[0].Text.Trim();
					TXT_PRODUCTDESC.Text = e.Item.Cells[1].Text.Trim();
					TXT_SIBS_PRODID.Text = e.Item.Cells[3].Text.Trim();
					TXT_SIBS_PRODCODE.Text = e.Item.Cells[2].Text.Trim();
					try
					{
						DDL_CURRENCY.SelectedValue = e.Item.Cells[4].Text.Trim();
					}
					catch{}
					fillRateNumber();
					try
					{
						DDL_RATENO.SelectedValue = e.Item.Cells[16].Text.Trim();
					} 
					catch {}
					try
					{
						DDL_RATE.SelectedValue = e.Item.Cells[16].Text.Trim();
						TXT_RATE.Text = DDL_RATE.SelectedItem.Text;
					} 
					catch {}
					try
					{
						DDL_INTERESTREST.SelectedValue = e.Item.Cells[6].Text.Trim();
					} 
					catch {}
					try
					{
						RDO_REVOLVING.SelectedValue = e.Item.Cells[11].Text.Trim();
					} 
					catch {}
					try
					{
						RDO_ISCASHLOAN.SelectedValue = e.Item.Cells[9].Text.Trim();
					} 
					catch {}
					try
					{
						RDO_VARCODE.SelectedValue = e.Item.Cells[18].Text.Trim();
					} 
					catch {}
					TXT_VARIANCE.Text = e.Item.Cells[19].Text.Trim();
					try
					{
						RDO_SPK.SelectedValue = e.Item.Cells[20].Text.Trim();
					} 
					catch {}
					try
					{
						RDO_ISINSTALLMENT.SelectedValue = e.Item.Cells[22].Text.Trim();
					} 
					catch {}
					try 
					{
						DDL_INTERESTTYPE.SelectedValue = e.Item.Cells[13].Text.Trim();
					} 
					catch {}
					try
					{
						RDO_CONFIRMKORAN.SelectedValue = e.Item.Cells[26].Text.Trim();
					} 
					catch {}
					try
					{
						RDO_ISUPLOADEMAS.SelectedValue = e.Item.Cells[28].Text.Trim();
					} 
					catch {}
					TXT_INTERESTTYPERATE.Text = e.Item.Cells[15].Text.Trim();
					TXT_NCL_CODE.Text = e.Item.Cells[40].Text.Trim();

                    //[-- crpundi
                    try
                    {
                        RDO_AGUNAN.SelectedValue = e.Item.Cells[42].Text.Trim();
                    }
                    catch { }
                    TXT_LIMIT_ATAS.Text = e.Item.Cells[44].Text.Trim();
                    TXT_LIMIT_BAWAH.Text = e.Item.Cells[45].Text.Trim();
                    try
                    {
                        RDO_PRK.SelectedValue = e.Item.Cells[46].Text.Trim();
                    }
                    catch { }
                    //--]

					cleansTextBox(TXT_PRODUCTID);
					cleansTextBox(TXT_PRODUCTDESC);
					cleansTextBox(TXT_SIBS_PRODCODE);
					cleansTextBox(TXT_SIBS_PRODID);
					cleansTextBox(TXT_RATE);
					cleansTextBox(TXT_VARIANCE);
					cleansTextBox(TXT_INTERESTTYPERATE);
					cleansTextBox(TXT_NCL_CODE);
                    //[-- crpundi
                    cleansTextBox(TXT_LIMIT_ATAS);
                    cleansTextBox(TXT_LIMIT_BAWAH);
                    //--]

					activatePostBackControls(false);
					enInterestVal(DDL_INTERESTTYPE.SelectedValue.Trim());
					break;
				case "delete":
					string PRODUCTID = e.Item.Cells[0].Text.Trim();
					conn.QueryString = "DELETE FROM PENDING_RFPRODUCT WHERE PRODUCTID = '"+ PRODUCTID + "' ";
					conn.ExecuteQuery();
					bindData2();
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void DDL_RATENO_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DDL_RATE.SelectedIndex = DDL_RATENO.SelectedIndex;
			TXT_RATE.Text = DDL_RATE.SelectedItem.Text;
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../GeneralParam.aspx?mc=" + Request.QueryString["mc"]);
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01");
		}

		protected void DDL_INTERESTTYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (DDL_INTERESTTYPE.SelectedValue.Trim() == "03")
			{
				//GlobalTools.popMessage(this, "OK");
				RBL_NEGO.Enabled = true;
				if ((TXT_PRODUCTID.Text.Trim() != "") && (RBL_NEGO.SelectedValue == "0") && (DDL_CURRENCY.SelectedIndex != 0))
					BTN_ALTERNATE_RATE.Visible = true;
			} 
			else
			{
				RBL_NEGO.SelectedValue = "0";
				RBL_NEGO.Enabled = false;
			}
			//GlobalTools.popMessage(this,"Luar");
			enInterestVal(DDL_INTERESTTYPE.SelectedValue.Trim());
		}

		protected void DDL_CURRENCY_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillRateNumber();
			if ( (DDL_INTERESTTYPE.SelectedValue.Trim() == "03") && (RBL_NEGO.SelectedValue == "0") && 
				(TXT_PRODUCTID.Text.Trim() != "") && (DDL_CURRENCY.SelectedIndex != 0))
			{ // jika productid sudah diisi, diset NEGO, currency sudah dipilih, tipenya alt rate
				BTN_ALTERNATE_RATE.Visible = true;
			}
			else
			{
				BTN_ALTERNATE_RATE.Visible = false;
			}
		}

		protected void RBL_NEGO_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( (DDL_INTERESTTYPE.SelectedValue.Trim() == "03") && (RBL_NEGO.SelectedValue == "0") && 
				(TXT_PRODUCTID.Text.Trim() != "") && (DDL_CURRENCY.SelectedIndex != 0))
			{ // jika productid sudah diisi, diset NEGO, currency sudah dipilih, tipenya alt rate
				BTN_ALTERNATE_RATE.Visible = true;
			}
			else
			{
				RBL_NEGO.SelectedValue = "1";
				BTN_ALTERNATE_RATE.Visible = false;
			}
		}

		protected void BTN_ALTERNATE_RATE_Click(object sender, System.EventArgs e)
		{
			/*// Pastikan produk yang akan diberi preset rate HARUS sudah disimpan terlebih dahulu
			// Pastikan produkID ada di basis data
			conn.QueryString = "select PRODUCTID, RATENO, CURRENCY from RFPRODUCT where PRODUCTID = '" + TXT_PRODUCTID.Text.Trim();
			conn.ExecuteQuery();
			if (conn.GetRowCount() == 1)
			{ // produk ada di basis data
				// ambil rateno dan currency, jadikan param bersama productid
				//Response.Write("<script language='Javascript'>PopupPage('AlternateRate.aspx?productid=" + TXT_PRODUCTID.Text.Trim() + "','900','350'));</script>"); 
				string prodid = conn.GetFieldValue("PRODUCTID");
				string rateno = conn.GetFieldValue("RATENO");
				string currency = conn.GetFieldValue("CURRENCY");
				Response.Write("<script language='javascript'>window.open('AlternateRate.aspx?productid=" + prodid + "&rateno=" + rateno + "&currency=" + currency + "','PresetAlternateRate','status=no,scrollbars=no,width=800,height=350');</script>");
			}*/
			if (TXT_PRODUCTID.Text.Trim() != "")
			{
				Response.Write("<script language='javascript'>window.open('AlternateRate.aspx?productid=" + TXT_PRODUCTID.Text.Trim() + "&rateno=" + DDL_RATENO.SelectedValue + "&currency=" + DDL_CURRENCY.SelectedValue + "','PresetAlternateRate','status=no,scrollbars=no,width=1000,height=350');</script>");
			}
			else
			{ // tampilkan pesan bahwa data harus di-save terlebih dahulu
				//GlobalTools.popMessage(this, "Data produk harap disimpan terlebih dahulu!");
				GlobalTools.popMessage(this, "PRODUCTID tidak boleh kosong!");
			}
		}
	}
}
