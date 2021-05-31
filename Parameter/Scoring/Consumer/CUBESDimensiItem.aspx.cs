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

namespace CuBES_Maintenance.Parameter.Scoring.Consumer
{
	/// <summary>
	/// Summary description for CUBESDimensiItem.
	/// </summary>
	public partial class CUBESDimensiItem : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected System.Web.UI.WebControls.CheckBox CHK_SCO;
		protected string mid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				bindData1();
				bindData2();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory2(document.Form1)){return false;};");
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "SELECT * FROM RFMODULE WHERE MODULEID = '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void bindData1()
		{
			string csco = "", crng = "", clmt = "";
			CheckBox chksco, chkrng, chklmt;

			conn.QueryString = "SELECT * FROM VW_MANDIRI_DIMESI_ITEM";
			conn.ExecuteQuery();

		
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_EXISTING.DataSource = dt;

			try
			{
				DGR_EXISTING.DataBind();
			}
			catch 
			{
				DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.PageCount - 1;
				DGR_EXISTING.DataBind();
			}

			for (int i = 0; i < DGR_EXISTING.Items.Count; i++)
			{
				csco = DGR_EXISTING.Items[i].Cells[11].Text.Trim();
				crng = DGR_EXISTING.Items[i].Cells[12].Text.Trim();
				clmt = DGR_EXISTING.Items[i].Cells[13].Text.Trim();
				
				chksco = (CheckBox)DGR_EXISTING.Items[i].Cells[14].FindControl("CHK_SCO1");
				chkrng = (CheckBox)DGR_EXISTING.Items[i].Cells[15].FindControl("CHK_RNG1");
				chklmt = (CheckBox)DGR_EXISTING.Items[i].Cells[16].FindControl("CHK_LMT1");
				
				if (csco == "1")
					chksco.Checked = true;
				else
					chksco.Checked = false;

				if (crng == "1")
					chkrng.Checked = true;
				else
					chkrng.Checked = false;

				if (clmt == "1")
					chklmt.Checked = true;
				else
					chklmt.Checked = false;

				
				chksco.Enabled = false;
				chkrng.Enabled = false;
				chklmt.Enabled = false;
			}
			
			conn.ClearData();
		}

		private void bindData2()
		{
			string csco = "", crng = "", clmt = "";
			CheckBox chksco, chkrng, chklmt;

			conn.QueryString = "SELECT * FROM VW_TMANDIRI_DIMESI_ITEM";
			conn.ExecuteQuery();

			
			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_REQUEST.DataSource = dt;

			try
			{
				DGR_REQUEST.DataBind();
			}
			catch 
			{
				DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				DGR_REQUEST.DataBind();
			}

			for (int i = 0; i < DGR_REQUEST.Items.Count; i++)
			{
				csco = DGR_REQUEST.Items[i].Cells[11].Text.Trim();
				crng = DGR_REQUEST.Items[i].Cells[12].Text.Trim();
				clmt = DGR_REQUEST.Items[i].Cells[13].Text.Trim();
				
				chksco = (CheckBox)DGR_REQUEST.Items[i].Cells[14].FindControl("CHK_SCO2");
				chkrng = (CheckBox)DGR_REQUEST.Items[i].Cells[15].FindControl("CHK_RNG2");
				chklmt = (CheckBox)DGR_REQUEST.Items[i].Cells[16].FindControl("CHK_LMT2");
				
				if (csco == "1")
					chksco.Checked = true;
				else
					chksco.Checked = false;

				if (crng == "1")
					chkrng.Checked = true;
				else
					chkrng.Checked = false;

				if (clmt == "1")
					chklmt.Checked = true;
				else
					chklmt.Checked = false;

				
				chksco.Enabled = false;
				chkrng.Enabled = false;
				chklmt.Enabled = false;

			}

			conn.ClearData();
		}

		private void clearEditBoxes()
		{
			TXT_DIMID.ReadOnly = false;
			TXT_DIMID.Text = "";
			TXT_DIMDESC.Text = "";
			TXT_REFTABLE.Text = "";
			TXT_REFFIELDID.Text = "";
			TXT_REFFIELDDESC.Text = "";
			TXT_CBSTABLE.Text = "";
			TXT_CBSFIELD.Text = "";
			TXT_CBSLINK.Text = "";
			TXT_PRMTABLE.Text = "";
			TXT_PRMFIELD.Text = "";
			TXT_PRMLINK.Text = "";
			CHK_SCOFLAG.Checked = false;
			CHK_RNGFLAG.Checked = false;
			CHK_LMTFLAG.Checked = false;
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
			this.DGR_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_ItemCommand);
			this.DGR_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

		}
		#endregion

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			bindData1();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			bindData2();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					
					conn.QueryString="SELECT * FROM VW_MANDIRI_DIMESI_ITEM WHERE DIMID='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();

					TXT_DIMID.ReadOnly = true;
					TXT_DIMID.Text = conn.GetFieldValue("DIMID");
					TXT_DIMDESC.Text = conn.GetFieldValue("DIMDESC");
					TXT_REFTABLE.Text = conn.GetFieldValue("REFTABLE");
					TXT_REFFIELDID.Text = conn.GetFieldValue("REFFIELDID");
					TXT_REFFIELDDESC.Text = conn.GetFieldValue("REFFIELDDESC");
					TXT_CBSTABLE.Text = conn.GetFieldValue("CBSTABLE");
					TXT_CBSFIELD.Text = conn.GetFieldValue("CBSFIELD");
					TXT_CBSLINK.Text = conn.GetFieldValue("CBSLINK");
					TXT_PRMTABLE.Text = conn.GetFieldValue("PRMTABLE");
					TXT_PRMFIELD.Text = conn.GetFieldValue("PRMFIELD");
					TXT_PRMLINK.Text = conn.GetFieldValue("PRMLINK");
				
					if (conn.GetFieldValue("SCOFLAG") == "1")
						CHK_SCOFLAG.Checked = true;
					else
						CHK_SCOFLAG.Checked = false;

					if (conn.GetFieldValue("RNGFLAG") == "1")
						CHK_RNGFLAG.Checked = true;
					else
						CHK_RNGFLAG.Checked = false;

					if (conn.GetFieldValue("LMTFLAG") == "1")
						CHK_LMTFLAG.Checked = true;
					else
						CHK_LMTFLAG.Checked = false;
					
					LBL_SAVEMODE.Text = "0";
										
					break;
				case "Delete":
					conn.QueryString="SELECT * FROM VW_TMANDIRI_DIMESI_ITEM WHERE DIMID='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="UPDATE TMANDIRI_DIMENSI_ITEM SET CH_STA='2' WHERE DIMID='"+e.Item.Cells[0].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_MANDIRI_DIMENSI_ITEM_MAKER '" +
							e.Item.Cells[0].Text.Trim()+ "','2', '"+e.Item.Cells[1].Text.Trim()+"','"+e.Item.Cells[2].Text.Trim()+"','"+
							e.Item.Cells[3].Text.Trim()+"','"+e.Item.Cells[4].Text.Trim()+"','"+e.Item.Cells[5].Text.Trim()+"','"+e.Item.Cells[6].Text.Trim()+"','"+
							e.Item.Cells[7].Text.Trim()+"','"+e.Item.Cells[8].Text.Trim()+"','"+e.Item.Cells[9].Text.Trim()+"','"+e.Item.Cells[10].Text.Trim()+"','"+ 
							e.Item.Cells[11].Text.Trim()+"','"+e.Item.Cells[12].Text.Trim()+"','"+e.Item.Cells[13].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					bindData2();
					break;
				default:
					
					break;
			} 
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":

					conn.QueryString="SELECT * FROM VW_TMANDIRI_DIMESI_ITEM WHERE DIMID='"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();

					string status = e.Item.Cells[17].Text;
					if(status=="DELETE")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						TXT_DIMID.ReadOnly = true;
						TXT_DIMID.Text = conn.GetFieldValue("DIMID");
						TXT_DIMDESC.Text = conn.GetFieldValue("DIMDESC");
						TXT_REFTABLE.Text = conn.GetFieldValue("REFTABLE");
						TXT_REFFIELDID.Text = conn.GetFieldValue("REFFIELDID");
						TXT_REFFIELDDESC.Text = conn.GetFieldValue("REFFIELDDESC");
						TXT_CBSTABLE.Text = conn.GetFieldValue("CBSTABLE");
						TXT_CBSFIELD.Text = conn.GetFieldValue("CBSFIELD");
						TXT_CBSLINK.Text = conn.GetFieldValue("CBSLINK");
						TXT_PRMTABLE.Text = conn.GetFieldValue("PRMTABLE");
						TXT_PRMFIELD.Text = conn.GetFieldValue("PRMFIELD");
						TXT_PRMLINK.Text = conn.GetFieldValue("PRMLINK");

						if (conn.GetFieldValue("SCOFLAG") == "1")
							CHK_SCOFLAG.Checked = true;
						else
							CHK_SCOFLAG.Checked = false;

						if (conn.GetFieldValue("RNGFLAG") == "1")
							CHK_RNGFLAG.Checked = true;
						else
							CHK_RNGFLAG.Checked = false;

						if (conn.GetFieldValue("LMTFLAG") == "1")
							CHK_LMTFLAG.Checked = true;
						else
							CHK_LMTFLAG.Checked = false;
					
						
						if(e.Item.Cells[17].Text=="UPDATE")
						{
							LBL_SAVEMODE.Text = "0";
						}
						if(e.Item.Cells[17].Text=="INSERT")
						{
							LBL_SAVEMODE.Text = "1";
						}
					}
					break;
				case "Delete":
					
					conn.QueryString = "DELETE FROM TMANDIRI_DIMENSI_ITEM WHERE DIMID = '"+e.Item.Cells[0].Text.Trim()+"'";
					conn.ExecuteQuery();
					bindData2();
					break;
				default:
					break;			
			}  
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string chksco, chkrng, chklmt;
			
			if (CHK_SCOFLAG.Checked == true)
				chksco = "1";
			else
				chksco = "0";

			if (CHK_RNGFLAG.Checked == true)
				chkrng = "1";
			else
				chkrng = "0";

			if (CHK_LMTFLAG.Checked == true)
				chklmt = "1";
			else
				chklmt = "0";

			conn.QueryString = "EXEC PARAM_MANDIRI_DIMENSI_ITEM_MAKER '" +
				TXT_DIMID.Text+ "','"+LBL_SAVEMODE.Text+"', '"+TXT_DIMDESC.Text+"',"+GlobalTools.ConvertNull(TXT_REFTABLE.Text)+","+
				GlobalTools.ConvertNull(TXT_REFFIELDID.Text)+","+GlobalTools.ConvertNull(TXT_REFFIELDDESC.Text)+","+GlobalTools.ConvertNull(TXT_CBSTABLE.Text)+","+GlobalTools.ConvertNull(TXT_CBSFIELD.Text)+","+
				GlobalTools.ConvertNull(TXT_CBSLINK.Text)+","+GlobalTools.ConvertNull(TXT_PRMTABLE.Text)+","+GlobalTools.ConvertNull(TXT_PRMFIELD.Text)+","+GlobalTools.ConvertNull(TXT_PRMLINK.Text)+",'"+ 
				chksco+"','"+chkrng+"','"+chklmt+"'";
			
			Response.Write("<!-- "+conn.QueryString+" -->");
			try
			{
				conn.ExecuteNonQuery();
			} 
			catch (Exception ex)
			{
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)		//method Connection.ExecuteNonQuery() add this msg on exception 
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this, errmsg);
			}

			clearEditBoxes();
			bindData2();
		}


	}
}
