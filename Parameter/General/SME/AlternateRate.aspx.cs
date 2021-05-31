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
	/// Summary description for arAlternateRate.
	/// </summary>
	public partial class AlternateRate : System.Web.UI.Page
	{
	
		#region " Variables "

		private Connection conn;
		private Tools tool = new Tools();
		private string PRODUCTID, RATENO, CURRENCY, EDIT;

		#endregion

		protected System.Web.UI.WebControls.Button BTN_SAVE;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);//(Connection) Session["Connection"];

			PRODUCTID	= Request.QueryString["productid"];
			RATENO		= Request.QueryString["rateno"];
			CURRENCY	= Request.QueryString["currency"];
			EDIT		= Request.QueryString["edit"];

			if (!IsPostBack) 
			{
				// isi ddl_rateno
				string q  = "select RATENO, RATENO AS RATENO2 from RFRATENUMBER ";
				q += "where CURRENCY = '" + CURRENCY + "' and ACTIVE = '1' order by RATENO";
				
				// load data untuk ddl rateno 
				GlobalTools.fillRefList(DDL_RATENO, q, false, conn);
				
				/*if ((RATENO != "") && (RATENO != null))
				{ //pilih rateno yang sudah diisi dari rfproduct
					DDL_RATENO.SelectedValue = RATENO;
					conn.QueryString = "select (RATE*100) as RATE from RFRATENUMBER ";
					conn.QueryString += "where RATENO = '" + RATENO + "' and CURRENCY = '" + CURRENCY + "'";
					conn.ExecuteQuery(); // mengambil nilai rate
					TXT_FLOATINGRATE.Text = conn.GetFieldValue("RATE");
				}
				else
				{
					//DDL_RATENO.SelectedItem = "-- Pilih --";
					DDL_RATENO.SelectedValue = "";
					TXT_FLOATINGRATE.Text = "";
				}*/
				setScreen();
				viewData();
			}
			//this.DDL_RATENO.SelectedIndexChanged += new System.EventHandler(this.DDL_RATENO_SelectedIndexChanged);
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
			this.DGR_ALTERNATERATE.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_ALTERNATERATE_ItemCommand);
			this.DGR_ALTERNATERATE.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.DGR_ALTERNATERATE_SortCommand);

		}
		#endregion		

		private void viewData() 
		{
			if (EDIT == "no")
			{
				DGR_ALTERNATERATE.Columns[6].Visible = false;
				DGR_ALTERNATERATE.Columns[7].Visible = false;
			}
			else
			{
				DGR_ALTERNATERATE.Columns[6].Visible = true;
				DGR_ALTERNATERATE.Columns[7].Visible = true;
			}
			DataTable dt = new DataTable();
			try 
			{
				conn.QueryString  = "select SEQUENCE, TENOR, FIXEDRATE, RATE AS FLOATINGRATE, VARCODE, VARIANCE ";
				conn.QueryString +=	"from VW_PARAM_GENERAL_RFPRODUCT_PRESETRATE ";
				conn.QueryString += "where PRODUCTID = '" + PRODUCTID + "'";
				conn.ExecuteQuery();
			} 
			catch (NullReferenceException) 
			{
				GlobalTools.popMessage(this, "Connection Error !");
				return;
			}

			DGR_ALTERNATERATE.DataSource = conn.GetDataTable().DefaultView;
			try 
			{
				DGR_ALTERNATERATE.DataBind();
			}
			catch 
			{
				DGR_ALTERNATERATE.CurrentPageIndex = 0;
				DGR_ALTERNATERATE.DataBind();
			}
		}

		private bool isInputValid() 
		{
			bool isValid = true;
			if (TXT_SEQ.Text == "") 
			{
				Response.Write("<script language='javascript'>alert('Kolom Sequence tidak boleh kosong!');</script>");
				isValid = false;
				return isValid;
			}
			// checking sequence input
			try 
			{
				conn.QueryString = "select * from rfproduct_preset_rate " + 
					"where PRODUCTID = '" + PRODUCTID + 
					"' and SEQUENCE = '" + TXT_SEQ.Text.Trim() + "'";
				conn.ExecuteQuery();

				if (conn.GetRowCount() > 0) 
				{
					Response.Write("<script language='javascript'>alert('Sequence sudah ada!');</script>");
					isValid = false;
				}
			} 
			catch (NullReferenceException) 
			{
				GlobalTools.popMessage(this, "Connection Error !");
				isValid = false;
			}

			return isValid;
		}

		private void clearInput() 
		{
			TXT_SEQ.Text			= "";
			TXT_SEQ.BackColor		= Color.White;
			TXT_SEQ.ReadOnly		= false;
			TXT_TENOR.Text			= "";
			TXT_FIXEDRATE.Text		= "";
			TXT_FLOATINGRATE.Text	= "";
			TXT_FLOATINGRATE.ReadOnly = true;
			TXT_FLOATINGRATE.BackColor = Color.Gainsboro;
			DDL_CODE.SelectedValue	= " ";
			DDL_RATENO.SelectedValue= "";
			TXT_VARIANCE.Text		= "";
		}

		private void setScreen()
		{
			if (EDIT == "no")
			{
				TXT_SEQ.Enabled = false;
				TXT_TENOR.Enabled = false;
				TXT_FIXEDRATE.Enabled = false;
				TXT_FLOATINGRATE.Enabled = false;
				TXT_VARIANCE.Enabled = false;
				DDL_CODE.Enabled = false;
				DDL_RATENO.Enabled = false; 
				BTN_INSERT.Enabled = false;
				TXT_SEQ.BackColor = Color.Gainsboro;
				TXT_TENOR.BackColor = Color.Gainsboro;
				TXT_FIXEDRATE.BackColor = Color.Gainsboro;
				TXT_FLOATINGRATE.BackColor = Color.Gainsboro;
				TXT_VARIANCE.BackColor = Color.Gainsboro;
				DDL_CODE.BackColor = Color.Gainsboro;
				DDL_RATENO.BackColor = Color.Gainsboro;
			}
			else
			{
				TXT_SEQ.Enabled = true;
				TXT_TENOR.Enabled = true;
				TXT_FIXEDRATE.Enabled = true;
				TXT_FLOATINGRATE.Enabled = true;
				TXT_FLOATINGRATE.ReadOnly = true;
				TXT_VARIANCE.Enabled = true;
				DDL_CODE.Enabled = true;
				DDL_RATENO.Enabled = true;
				BTN_INSERT.Enabled = true;
				TXT_SEQ.BackColor = Color.White;
				TXT_TENOR.BackColor = Color.White;
				TXT_FIXEDRATE.BackColor = Color.White;
				TXT_FLOATINGRATE.BackColor = Color.Gainsboro;
				TXT_VARIANCE.BackColor = Color.White;
				DDL_CODE.BackColor = Color.White;
				DDL_RATENO.BackColor = Color.Gainsboro;
			}
		}

		private string toStrBiasa(string s)
		{
			s = s.Replace("%","");
			s = s.Replace(",00","");
			s = s.Replace(".","");
			s = s.Replace(",",".");
			return s;
		}
		
		private string toStrCurrency(string s)
		{ // masih salah
			string r = "";
			int pjstr = s.Length;
			while (pjstr > 3)
			{
				r = "." + s.Substring(pjstr-3, pjstr-1);
				pjstr -= 3;
				s = s.Substring(0,pjstr-1);
			}
			r = s + r + ",00";
			return r;
		}


		protected void BTN_INSERT_Click(object sender, System.EventArgs e)
		{
			if (!isInputValid()) return; // input tidak valid

			string rateno = "";
			string code = " ";
			if (TXT_FLOATINGRATE.Text.Trim() != "") rateno = DDL_RATENO.SelectedValue;
			if (DDL_RATENO.SelectedIndex != 0) code = DDL_CODE.SelectedValue;

			try 
			{
				conn.QueryString = "exec PARAM_GENERAL_RFPRODUCT_PRESET_RATE " +
					"'" + PRODUCTID + "', " + TXT_SEQ.Text.Trim() +
					", " + TXT_TENOR.Text.Trim() + 
					", " + tool.ConvertFloat(TXT_FIXEDRATE.Text.Trim()) + 
					", '" + rateno + 
					"', '" + code + 
					"', " + tool.ConvertFloat(TXT_VARIANCE.Text.Trim()) + 
					", '1'";
				conn.ExecuteNonQuery();

				LBL_SERBA.Text = conn.QueryString;
				viewData();
				clearInput();
			}
			catch (NullReferenceException) 
			{
				GlobalTools.popMessage(this, "Connection Error !");
				return;
			}
		}

		private void DGR_ALTERNATERATE_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			LBL_SORT_EXP.Text = e.SortExpression;
			if (LBL_SORT_TYPE.Text == "ASC") LBL_SORT_TYPE.Text = "DESC";
			else LBL_SORT_TYPE.Text = "ASC";

			viewData();
		}

		private void DGR_ALTERNATERATE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (e.CommandName.ToString()) 
			{
				case "Delete":
					try 
					{
						conn.QueryString = "exec PARAM_GENERAL_RFPRODUCT_PRESET_RATE '";
						conn.QueryString += PRODUCTID + "', " + e.Item.Cells[0].Text + ", 0, 0, ' ', ' ', 0, '3'";
						conn.ExecuteNonQuery();

						LBL_SERBA.Text = conn.QueryString;
						viewData();
						clearInput();
					} 
					catch (NullReferenceException) 
					{
						GlobalTools.popMessage(this, "Connection Error !");
						return;
					}
					BTN_INSERT.Visible = true;
					BTN_UPDATE.Visible = false;
					BTN_CANCEL.Visible = false;
					break;

				case "Edit":
					BTN_INSERT.Visible = false;
					BTN_UPDATE.Visible = true;
					BTN_CANCEL.Visible = true;

					TXT_SEQ.Text = e.Item.Cells[0].Text.Trim();
					TXT_SEQ.ReadOnly = true;
					TXT_SEQ.BackColor = Color.Gainsboro;
					
					TXT_TENOR.Text = e.Item.Cells[1].Text.Trim();
					
					string temp = toStrBiasa(e.Item.Cells[2].Text);
					LBL_SERBA.Text += " - " + temp;
					float x = 0;
					try { x = Convert.ToSingle(temp); }
					catch (Exception) { x = 0; }

					if ( x != 0 )
						TXT_FIXEDRATE.Text = temp;
					else TXT_FIXEDRATE.Text = "";

					temp = toStrBiasa(e.Item.Cells[3].Text);
					LBL_SERBA.Text += " -float " + temp;
					// yang terambil baru rate, pastikan ddl-nya ter-select pada index yang bersesuaian
					// ambil dari basis data kuncinya: seq + productid
					x = 0;
					try { x = Convert.ToSingle(temp); }
					catch (Exception) { x = 0; }

					if ( x != 0 )
					{
						conn.QueryString  = "select RATENUMBER from RFPRODUCT_PRESET_RATE where ";
						conn.QueryString += "PRODUCTID = '" + PRODUCTID + "' and ";
						conn.QueryString += "SEQUENCE = " + TXT_SEQ.Text + "";
						//LBL_SERBA.Text += " - "+conn.QueryString;
						conn.ExecuteQuery();
						try
						{
							DDL_RATENO.SelectedValue = conn.GetFieldValue("RATENUMBER");
						} 
						catch(Exception) {}
						TXT_FLOATINGRATE.Text = temp;
					}
					else 
					{
						DDL_RATENO.SelectedValue = "";
						TXT_FLOATINGRATE.Text = "";
					}
					
					string code = e.Item.Cells[4].Text.Trim();
					if (code == "+") 
					{
						DDL_CODE.SelectedValue = code;
					} 
					else if (code == "-")
					{
						DDL_CODE.SelectedValue = code;
					} else DDL_CODE.SelectedValue = " ";

					temp = toStrBiasa(e.Item.Cells[5].Text.Trim());
					x = 0;
					try { x = Convert.ToSingle(temp); }
					catch (Exception) { x = 0; }

					if ( x != 0 )
					{
						TXT_VARIANCE.Text = temp;
					}
					else TXT_VARIANCE.Text = "";

						break;

				case "Cancel":
					DGR_ALTERNATERATE.EditItemIndex = -1;
					viewData();
					break;
			}			
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearInput();
			TXT_SEQ.Text = "";
			TXT_SEQ.ReadOnly = false;

			BTN_INSERT.Visible = true;
			BTN_UPDATE.Visible = false;
			BTN_CANCEL.Visible = false;
		}

		protected void BTN_UPDATE_Click(object sender, System.EventArgs e)
		{
			string rateno = "";
			string code = " ";
			if (TXT_FLOATINGRATE.Text.Trim() != "") rateno = DDL_RATENO.SelectedValue;
			if (DDL_RATENO.SelectedIndex != 0) code = DDL_CODE.SelectedValue;

			try 
			{
				conn.QueryString = "exec PARAM_GENERAL_RFPRODUCT_PRESET_RATE " +
					"'" + PRODUCTID + "', " + TXT_SEQ.Text.Trim() +
					", " + TXT_TENOR.Text.Trim() + 
					", " + tool.ConvertFloat(TXT_FIXEDRATE.Text.Trim()) + 
					", '" + DDL_RATENO.SelectedValue + 
					"', '" + DDL_CODE.SelectedValue + 
					"', " + tool.ConvertFloat(TXT_VARIANCE.Text.Trim()) + 
					", '2'";
				conn.ExecuteNonQuery();
				LBL_SERBA.Text = conn.QueryString;
				viewData();
				clearInput();
			}
			catch (NullReferenceException) 
			{
				GlobalTools.popMessage(this, "Connection Error !");
				return;
			}

			BTN_INSERT.Visible = true;
			BTN_UPDATE.Visible = false;
			BTN_CANCEL.Visible = false;
		}

		protected void DDL_RATENO_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// ketika terjadi perubahan pilihan, load nilai ratenumber yang baru ke teksboks floatingrate
			conn.QueryString = "select (RATE*100) as RATE from RFRATENUMBER ";
			conn.QueryString += "where RATENO = '" + DDL_RATENO.SelectedValue + "' and CURRENCY = '" + CURRENCY + "'";
			conn.ExecuteQuery(); // mengambil nilai rate
			TXT_FLOATINGRATE.Text = conn.GetFieldValue("RATE");
			TXT_FLOATINGRATE.ReadOnly = true;
			TXT_FLOATINGRATE.BackColor = Color.Gainsboro;
			TXT_FIXEDRATE.Text = "";
			TXT_VARIANCE.ReadOnly = false;
		}
	}
}
