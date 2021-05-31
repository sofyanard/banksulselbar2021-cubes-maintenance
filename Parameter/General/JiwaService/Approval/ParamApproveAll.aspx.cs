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
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.JiwaService.Approval
{
	/// <summary>
	/// Summary description for ParamApproveAll.
	/// </summary>
	public partial class ParamApproveAll : System.Web.UI.Page
	{



		protected Tools tools = new Tools();
		protected Connection conn = new Connection(ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				TR_DEPT.Visible = false;
				TR_SELF.Visible = false;
				TR_CUST.Visible = false;
				TR_LINK.Visible = false;
				TR_SCORE.Visible = false;
				TR_DATE.Visible = false;
				ViewData();
			}
		}

		private void ViewData()
		{
			//choose parameter type
			if(Request.QueryString["pg"] == "01")			// Department Parameter
			{
				LBL_PARAM_TITLE.Text = "DEPARTMENT PROVIDER";
				TR_DEPT.Visible = true;
				conn.QueryString = "SELECT *, G_CODE + '-' + G_DESC AS [GROUP], D_DESCNEW, CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_DEPT WHERE STATUS='0'";
				BindData(DGR_DEPTAPPR.ID.ToString(), conn.QueryString);
			}
			else if(Request.QueryString["pg"] == "02")		// Self Assessment Parameter
			{
				LBL_PARAM_TITLE.Text = "SELF ASSESSMENT";
				TR_SELF.Visible = true;
				conn.QueryString = "SELECT *, CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_SELF WHERE STATUS='0'";
				BindData(DGR_SELFAPPR.ID.ToString(), conn.QueryString);
			}
			else if(Request.QueryString["pg"] == "03")		// Customer Rule Parameter
			{
				LBL_PARAM_TITLE.Text = "PERTANYAAN INTERNAL CUSTOMER";
				TR_CUST.Visible = true;
				conn.QueryString = "SELECT *, CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_CUSTOMER WHERE STATUS='0'";
				BindData(DGR_CUSTAPPR.ID.ToString(), conn.QueryString);
			}
			else if(Request.QueryString["pg"] == "04")		// Link Provider Parameter
			{
				LBL_PARAM_TITLE.Text = "CUSTOMER - PROVIDER LINK";
				TR_LINK.Visible = true;
				conn.QueryString = "SELECT CODE+'-'+DESCRIPTION AS BRANCH, * , CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_LINK WHERE STATUS='0'";
				BindData(DGR_LINKAPPR.ID.ToString(), conn.QueryString);
			}
			else if(Request.QueryString["pg"] == "05")		// Score Rule Parameter
			{
				LBL_PARAM_TITLE.Text = "SCORE RULE";
				TR_SCORE.Visible = true;
				conn.QueryString = "SELECT *, CONVERT(VARCHAR, BOBOT)+'%' AS BOBOT_DESC, CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_SCORE WHERE STATUS='0'";
				BindData(DGR_SCOREAPPR.ID.ToString(), conn.QueryString);
			}
			else if(Request.QueryString["pg"] == "06")		// Cut Date Parameter
			{
				LBL_PARAM_TITLE.Text = "CUT OFF DATE";
				TR_DATE.Visible = true;
				conn.QueryString = "SELECT * , CASE STATUS WHEN '0' THEN 'Insert' END AS STATUS_DESC FROM RF_DATE WHERE STATUS='0' ORDER BY SEQ ASC";
				BindData(DGR_DATEAPPR.ID.ToString(), conn.QueryString);
			}
		}

		private void BindData(string dataGridName, string strconn)
		{
			if(strconn != "")
			{
				conn.QueryString = strconn;
				conn.ExecuteQuery();
			}

			System.Data.DataTable dt = new System.Data.DataTable();
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
			this.DGR_DEPTAPPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPROVE_ItemCommand);
			this.DGR_DEPTAPPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPROVE_PageIndexChanged);
			this.DGR_SELFAPPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPROVE_ItemCommand);
			this.DGR_SELFAPPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPROVE_PageIndexChanged);
			this.DGR_CUSTAPPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPROVE_ItemCommand);
			this.DGR_CUSTAPPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPROVE_PageIndexChanged);
			this.DGR_LINKAPPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPROVE_ItemCommand);
			this.DGR_LINKAPPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPROVE_PageIndexChanged);
			this.DGR_SCOREAPPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPROVE_ItemCommand);
			this.DGR_SCOREAPPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPROVE_PageIndexChanged);
			this.DGR_DATEAPPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPROVE_ItemCommand);
			this.DGR_DATEAPPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPROVE_PageIndexChanged);

		}
		#endregion

		private void DGR_APPROVE_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			if(((DataGrid)source).ID == "DGR_DEPTAPPR")
			{
				DGR_DEPTAPPR.CurrentPageIndex = e.NewPageIndex;
			}
			else if(((DataGrid)source).ID == "DGR_SELFAPPR")
			{
				DGR_SELFAPPR.CurrentPageIndex = e.NewPageIndex;
			}
			else if(((DataGrid)source).ID == "DGR_CUSTAPPR")
			{
				DGR_CUSTAPPR.CurrentPageIndex = e.NewPageIndex;
			}
			else if(((DataGrid)source).ID == "DGR_LINKAPPR")
			{
				DGR_LINKAPPR.CurrentPageIndex = e.NewPageIndex;
			}
			else if(((DataGrid)source).ID == "DGR_SCOREAPPR")
			{
				DGR_SCOREAPPR.CurrentPageIndex = e.NewPageIndex;
			}
			else if(((DataGrid)source).ID == "DGR_DATEAPPR")
			{
				DGR_DATEAPPR.CurrentPageIndex = e.NewPageIndex;
			}
			
			ViewData();
		}

		private void DGR_APPROVE_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string rdo1 = "", rdo2 = "", rdo3 = "";

			if(((DataGrid)source).ID == "DGR_DEPTAPPR")
			{
				rdo1 = "RDO_APPROVE1"; rdo2 = "RDO_PENDING1"; rdo3 = "RDO_REJECT1";
			}
			else if(((DataGrid)source).ID == "DGR_SELFAPPR")
			{
				rdo1 = "RDO_APPROVE2"; rdo2 = "RDO_PENDING2"; rdo3 = "RDO_REJECT2";
			}
			else if(((DataGrid)source).ID == "DGR_CUSTAPPR")
			{
				rdo1 = "RDO_APPROVE3"; rdo2 = "RDO_PENDING3"; rdo3 = "RDO_REJECT3";
			}
			else if(((DataGrid)source).ID == "DGR_LINKAPPR")
			{
				rdo1 = "RDO_APPROVE4"; rdo2 = "RDO_PENDING4"; rdo3 = "RDO_REJECT4";
			}
			else if(((DataGrid)source).ID == "DGR_SCOREAPPR")
			{
				rdo1 = "RDO_APPROVE5"; rdo2 = "RDO_PENDING5"; rdo3 = "RDO_REJECT5";
			}
			else if(((DataGrid)source).ID == "DGR_DATEAPPR")
			{
				rdo1 = "RDO_APPROVE6"; rdo2 = "RDO_PENDING6"; rdo3 = "RDO_REJECT6";
			}

			DataGrid dataGridName = (DataGrid)source;

			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAccept":
					for (int i = 0; i < dataGridName.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) dataGridName.Items[i].FindControl(rdo1),
								rbP = (RadioButton) dataGridName.Items[i].FindControl(rdo2),
								rbR = (RadioButton) dataGridName.Items[i].FindControl(rdo3);
							rbA.Checked = true;
							rbP.Checked = false;
							rbR.Checked = false;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (int i = 0; i < dataGridName.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) dataGridName.Items[i].FindControl(rdo1),
								rbP = (RadioButton) dataGridName.Items[i].FindControl(rdo2),
								rbR = (RadioButton) dataGridName.Items[i].FindControl(rdo3);
							rbA.Checked = false;
							rbP.Checked = true;
							rbR.Checked = false;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (int i = 0; i < dataGridName.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) dataGridName.Items[i].FindControl(rdo1),
								rbP = (RadioButton) dataGridName.Items[i].FindControl(rdo2),
								rbR = (RadioButton) dataGridName.Items[i].FindControl(rdo3);
							rbA.Checked = false;
							rbP.Checked = false;
							rbR.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					break;
			}
		}

		protected void BTN_SUB_Click(object sender, System.EventArgs e)
		{
			string gridname = "", rdo1 = "", rdo2 = "";

			if(Request.QueryString["pg"] == "01")		// Department Parameter
			{
				gridname = "DGR_DEPTAPPR";
				rdo1 = "RDO_APPROVE1"; rdo2 = "RDO_REJECT1";
			}
			else if(Request.QueryString["pg"] == "02")	// Self Assessment Parameter
			{
				gridname = "DGR_SELFAPPR";
				rdo1 = "RDO_APPROVE2"; rdo2 = "RDO_REJECT2";
			}
			else if(Request.QueryString["pg"] == "03")	// Customer Rule Parameter
			{
				gridname = "DGR_CUSTAPPR";
				rdo1 = "RDO_APPROVE3"; rdo2 = "RDO_REJECT3";
			}
			else if(Request.QueryString["pg"] == "04")	// Link Provider Parameter
			{
				gridname = "DGR_LINKAPPR";
				rdo1 = "RDO_APPROVE4"; rdo2 = "RDO_REJECT4";
			}
			else if(Request.QueryString["pg"] == "05")	// Score Rule Parameter
			{
				gridname = "DGR_SCOREAPPR";
				rdo1 = "RDO_APPROVE5"; rdo2 = "RDO_REJECT5";
			}
			else if(Request.QueryString["pg"] == "06")	// Cut Date Parameter
			{
				gridname = "DGR_DATEAPPR";
				rdo1 = "RDO_APPROVE6"; rdo2 = "RDO_REJECT6";
			}

			DataGrid dataGridName =  (System.Web.UI.WebControls.DataGrid)Page.FindControl(gridname);

			for (int i = 0; i < dataGridName.Items.Count; i++)
			{
				try	
				{
					RadioButton rbA = (RadioButton) dataGridName.Items[i].FindControl(rdo1),
						rbR = (RadioButton) dataGridName.Items[i].FindControl(rdo2);
					if (rbA.Checked)
					{
						performRequest(i, dataGridName);
					}
					else if (rbR.Checked)
					{
						deleteData(i, dataGridName);
					}
				} 
				catch {}
			}

			ViewData();
		}

		private void performRequest(int row, DataGrid dataGridName)
		{
			try 
			{
				if(Request.QueryString["pg"] == "01")		// Department Parameter
				{
					string code = dataGridName.Items[row].Cells[2].Text.Trim();
					string name = dataGridName.Items[row].Cells[3].Text.Trim();

					conn.QueryString = "UPDATE RF_DEPT SET STATUS='1' WHERE D_CODE='" + code + "' AND D_DESCNEW='" + name + "'";
					conn.ExecuteNonQuery();
				}
				else if(Request.QueryString["pg"] == "02")	// Self Assessment Parameter
				{
					string seqid = dataGridName.Items[row].Cells[0].Text.Trim();
					string seqstep = dataGridName.Items[row].Cells[7].Text.Trim();

					conn.QueryString = "UPDATE RF_SELF SET STATUS='1' WHERE SEQ='" + seqid + "' AND LANGKAH_SEQ='" + seqstep + "'";
					conn.ExecuteNonQuery();
				}
				else if(Request.QueryString["pg"] == "03")	// Customer Rule Parameter
				{
					string nomor = dataGridName.Items[row].Cells[0].Text.Trim();
					string groupcode = dataGridName.Items[row].Cells[1].Text.Trim();
					string deptcode = dataGridName.Items[row].Cells[3].Text.Trim();

					conn.QueryString = "UPDATE RF_CUSTOMER SET STATUS='1' WHERE [NO]='" + nomor + "' AND G_CODE='" + groupcode + "' AND D_CODE='" + deptcode + "'";
					conn.ExecuteNonQuery();
				}
				else if(Request.QueryString["pg"] == "04")	// Link Provider Parameter
				{
					string nomor = dataGridName.Items[row].Cells[0].Text.Trim();

					conn.QueryString = "UPDATE RF_LINK SET STATUS='1' WHERE SEQ='" + nomor + "'";
					conn.ExecuteNonQuery();
				}
				else if(Request.QueryString["pg"] == "05")	// Score Rule Parameter
				{
					string code = dataGridName.Items[row].Cells[0].Text.Trim();

					conn.QueryString = "UPDATE RF_SCORE SET STATUS='1' WHERE CODE='" + code + "'";
					conn.ExecuteNonQuery();
				}
				else if(Request.QueryString["pg"] == "06")	// Cut Date Parameter
				{
					string nomor = dataGridName.Items[row].Cells[0].Text.Trim();

					conn.QueryString = "UPDATE RF_DATE SET STATUS='1' WHERE SEQ='" + nomor + "'";
					conn.ExecuteNonQuery();
				}
			} 
			catch {}
		}

		private void deleteData(int row, DataGrid dataGridName)
		{
			try 
			{
				if(Request.QueryString["pg"] == "01")		// Department Parameter
				{
					string code = dataGridName.Items[row].Cells[2].Text.Trim();
					string name = dataGridName.Items[row].Cells[3].Text.Trim();

					conn.QueryString = "DELETE RF_DEPT WHERE D_CODE='" + code + "' AND D_DESCNEW='" + name + "'";
					conn.ExecuteNonQuery();
				}
				else if(Request.QueryString["pg"] == "02")	// Self Assessment Parameter
				{
					string seqid = dataGridName.Items[row].Cells[0].Text.Trim();
					string seqstep = dataGridName.Items[row].Cells[7].Text.Trim();

					conn.QueryString = "DELETE RF_SELF WHERE SEQ='" + seqid + "' AND LANGKAH_SEQ='" + seqstep + "'";
					conn.ExecuteNonQuery();
				}
				else if(Request.QueryString["pg"] == "03")	// Customer Rule Parameter
				{
					string nomor = dataGridName.Items[row].Cells[0].Text.Trim();
					string groupcode = dataGridName.Items[row].Cells[1].Text.Trim();
					string deptcode = dataGridName.Items[row].Cells[3].Text.Trim();

					conn.QueryString = "DELETE RF_CUSTOMER WHERE [NO]='" + nomor + "' AND G_CODE='" + groupcode + "' AND D_CODE='" + deptcode + "'";
					conn.ExecuteNonQuery();
				}
				else if(Request.QueryString["pg"] == "04")	// Link Provider Parameter
				{
					string nomor = dataGridName.Items[row].Cells[0].Text.Trim();

					conn.QueryString = "DELETE RF_LINK WHERE SEQ='" + nomor + "'";
					conn.ExecuteNonQuery();
				}
				else if(Request.QueryString["pg"] == "05")	// Score Rule Parameter
				{
					string code = dataGridName.Items[row].Cells[0].Text.Trim();

					conn.QueryString = "DELETE RF_SCORE WHERE CODE='" + code + "'";
					conn.ExecuteNonQuery();
				}
				else if(Request.QueryString["pg"] == "06")	// Cut Date Parameter
				{
					string nomor = dataGridName.Items[row].Cells[0].Text.Trim();

					conn.QueryString = "DELETE RF_DATE WHERE SEQ='" + nomor + "'";
					conn.ExecuteNonQuery();
				}
			} 
			catch {}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../../JiwaServiceParam.aspx?mc=" + Request.QueryString["mc"] + "&pg=9");
		}
	}
}
