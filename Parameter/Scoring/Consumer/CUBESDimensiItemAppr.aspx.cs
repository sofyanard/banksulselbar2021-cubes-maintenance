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
	/// Summary description for CUBESDimensiItemAppr.
	/// </summary>
	public partial class CUBESDimensiItemAppr : System.Web.UI.Page
	{
		protected Connection conn,conn2;
		protected string mid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{		
				bindData1();

				string dimid = ""; 
				for (int i = 0; i < DGR_REQUEST.Items.Count; i++)
				{		
					if (DGR_REQUEST.Items[i].Cells[17].Text == "DELETE")
						dimid += DGR_REQUEST.Items[i].Cells[0].Text + ",";
				}

				if (dimid != "")
				{
					Response.Write("<script language='javascript'>window.showModalDialog('DeleteConfirmation.aspx?ModuleId=40','','dialogHeight:600px;dialogWidth:800px;scrollbar:no');</script>");
				}
			}	
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from RFMODULE where MODULEID= '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void bindData1()
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

		private void performRequest(int row)
		{
			try 
			{
				conn.QueryString="select * from VW_TMANDIRI_DIMESI_ITEM where DIMID='"+DGR_REQUEST.Items[row].Cells[0].Text.Trim()+"'";
				conn.ExecuteQuery();

				string userid = Session["UserID"].ToString(), groupid = Session["GroupID"].ToString(),
				dimid = conn.GetFieldValue("DIMID"), dimdesc = conn.GetFieldValue("DIMDESC"),
				reftable = GlobalTools.ConvertNull(conn.GetFieldValue("REFTABLE")), reffieldid = GlobalTools.ConvertNull(conn.GetFieldValue("REFFIELDID")),
				reffielddesc = GlobalTools.ConvertNull(conn.GetFieldValue("REFFIELDDESC")), cbstable = GlobalTools.ConvertNull(conn.GetFieldValue("CBSTABLE")),
				cbsfield = GlobalTools.ConvertNull(conn.GetFieldValue("CBSFIELD")), cbslink = GlobalTools.ConvertNull(conn.GetFieldValue("CBSLINK")),
				prmtable = GlobalTools.ConvertNull(conn.GetFieldValue("PRMTABLE")), prmfield = GlobalTools.ConvertNull(conn.GetFieldValue("PRMFIELD")),
				prmlink = GlobalTools.ConvertNull(conn.GetFieldValue("PRMLINK")), scoflag = conn.GetFieldValue("SCOFLAG"),
				rngflag = conn.GetFieldValue("RNGFLAG"), lmtflag = conn.GetFieldValue("LMTFLAG"),
				status = conn.GetFieldValue("CH_STA");
												
				if (status.Equals("0"))
				{
					conn.QueryString = "EXEC PARAM_MANDIRI_DIMENSI_ITEM_APPROVAL '3','"+dimid+"','"+dimdesc+"',"+
						reftable+", "+reffieldid+", "+reffielddesc+", "+cbstable+", "+cbsfield+", "+
						cbslink+", "+prmtable+", "+prmfield+", "+prmlink+", '"+scoflag+"', '"+
						rngflag+"', '"+lmtflag+"', '"+status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}

				if (status.Equals("1"))
				{
					conn.QueryString = "EXEC PARAM_MANDIRI_DIMENSI_ITEM_APPROVAL '4','"+dimid+"','"+dimdesc+"',"+
						reftable+", "+reffieldid+", "+reffielddesc+", "+cbstable+", "+cbsfield+", "+
						cbslink+", "+prmtable+", "+prmfield+", "+prmlink+", '"+scoflag+"', '"+
						rngflag+"', '"+lmtflag+"', '"+status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}
				if (status.Equals("2"))
				{
					conn.QueryString = "EXEC PARAM_MANDIRI_DIMENSI_ITEM_APPROVAL '5','"+dimid+"','"+dimdesc+"',"+
						reftable+", "+reffieldid+", "+reffielddesc+", "+cbstable+", "+cbsfield+", "+
						cbslink+", "+prmtable+", "+prmfield+", "+prmlink+", '"+scoflag+"', '"+
						rngflag+"', '"+lmtflag+"', '"+status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}

				conn.QueryString = "EXEC PARAM_MANDIRI_DIMENSI_ITEM_APPROVAL '2','"+dimid+"','"+dimdesc+"',"+
					reftable+", "+reffieldid+", "+reffielddesc+", "+cbstable+", "+cbsfield+", "+
					cbslink+", "+prmtable+", "+prmfield+", "+prmlink+", '"+scoflag+"', '"+
					rngflag+"', '"+lmtflag+"', '"+status+"','"+userid+"','"+groupid+"'" ;
				conn.ExecuteNonQuery();
				
			} 
			catch (Exception p)
			{
				GlobalTools.popMessage(this,p.Message);
			}


		}

		private void deleteData(int row)
		{
			try 
			{
				conn.QueryString = "EXEC PARAM_MANDIRI_DIMENSI_ITEM_APPROVAL '2','"+DGR_REQUEST.Items[row].Cells[0].Text.Trim()+"',"+
					"'', '', '', '', '', '', '', '', '', '', '', '', '', '','',''" ;
				conn.ExecuteNonQuery();
			} 
			catch {}
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
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

		}
		#endregion

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			bindData1();
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_REQUEST.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_REQUEST.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_REQUEST.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_REQUEST.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DGR_REQUEST.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_REQUEST.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_REQUEST.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_REQUEST.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_REQUEST.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_REQUEST.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_REQUEST.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_REQUEST.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbB.Checked = false;
							rbC.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_SUBMIT_TPL_Click(object sender, System.EventArgs e)
		{					
			for (int i = 0; i < DGR_REQUEST.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_REQUEST.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGR_REQUEST.Items[i].FindControl("rdo_Reject");
					if (rbA.Checked)
					{	
						performRequest(i);
					}
					else if (rbR.Checked)
					{						
						deleteData(i);
					}
				} 
				catch {}
			}
			bindData1();
		}

	}
}
