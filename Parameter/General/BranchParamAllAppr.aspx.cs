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
	public partial class BranchParamAllAppr : System.Web.UI.Page
	{
		protected Connection conn,connsme,conncons,conncc;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn();
			InitialControls();
			if (!IsPostBack)
			{
				ViewPendingData();
			}
			this.DGR_REQUEST_CONNCC.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_REQUEST_CONNCC_PageIndexChanged);
			this.DGR_REQUEST_SME.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_REQUEST_SME_PageIndexChanged);
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

		private void InitialControls()
		{
			this.LBL_STA.Text	= this.RBL_MODULE.SelectedValue;
		}

		private void ViewPendingData()
		{
			if (this.LBL_STA.Text.Trim() =="0")//SME
				ViewPendingSME();
			else
				ViewPendingConsumer();
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

		private void ViewPendingSME()
		{
			connsme.QueryString = "select * from VW_PARAM_PENDING_RFBRANCHall_SME";
			try 
			{
				connsme.ExecuteQuery();
			} 
			catch {}
			this.DGR_REQUEST_CONNCC.Visible = false;
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

		private void ViewPendingConsumer()
		{
			conncons.QueryString = "select * from VW_PARAM_PENDING_RFBRANCHall_CONCC";
			try 
			{
				conncons.ExecuteQuery();
			} catch {}
			this.DGR_REQUEST_CONNCC.Visible = true;
			this.DGR_REQUEST_SME.Visible = false;
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
			this.DGR_REQUEST_SME.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_SME_ItemCommand);
			this.DGR_REQUEST_SME.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_SME_PageIndexChanged);
			this.DGR_REQUEST_CONNCC.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_CONNCC_ItemCommand);
			this.DGR_REQUEST_CONNCC.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_CONNCC_PageIndexChanged);

		}
		#endregion

		protected void RBL_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.LBL_STA.Text	= this.RBL_MODULE.SelectedValue;
			ViewPendingData();
		}

		private void DGR_REQUEST_CONNCC_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr1":
					for (i = 0; i < DGR_REQUEST_CONNCC.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_REQUEST_CONNCC.Items[i].FindControl("rdo_Approve1"),
								rbB = (RadioButton) DGR_REQUEST_CONNCC.Items[i].FindControl("rdo_Reject1"),
								rbC = (RadioButton) DGR_REQUEST_CONNCC.Items[i].FindControl("rdo_Pending1");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject1":
					for (i = 0; i < DGR_REQUEST_CONNCC.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_REQUEST_CONNCC.Items[i].FindControl("rdo_Approve1"),
								rbB = (RadioButton) DGR_REQUEST_CONNCC.Items[i].FindControl("rdo_Reject1"),
								rbC = (RadioButton) DGR_REQUEST_CONNCC.Items[i].FindControl("rdo_Pending1");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend1":
					for (i = 0; i < DGR_REQUEST_CONNCC.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_REQUEST_CONNCC.Items[i].FindControl("rdo_Approve1"),
								rbB = (RadioButton) DGR_REQUEST_CONNCC.Items[i].FindControl("rdo_Reject1"),
								rbC = (RadioButton) DGR_REQUEST_CONNCC.Items[i].FindControl("rdo_Pending1");
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

		private void DGR_REQUEST_CONNCC_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST_CONNCC.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_REQUEST_SME_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST_SME.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_REQUEST_SME_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr2":
					for (i = 0; i < DGR_REQUEST_SME.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_REQUEST_SME.Items[i].FindControl("rdo_Approve2"),
								rbB = (RadioButton) DGR_REQUEST_SME.Items[i].FindControl("rdo_Reject2"),
								rbC = (RadioButton) DGR_REQUEST_SME.Items[i].FindControl("rdo_Pending2");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject2":
					for (i = 0; i < DGR_REQUEST_SME.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_REQUEST_SME.Items[i].FindControl("rdo_Approve2"),
								rbB = (RadioButton) DGR_REQUEST_SME.Items[i].FindControl("rdo_Reject2"),
								rbC = (RadioButton) DGR_REQUEST_SME.Items[i].FindControl("rdo_Pending2");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend2":
					for (i = 0; i < DGR_REQUEST_SME.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_REQUEST_SME.Items[i].FindControl("rdo_Approve2"),
								rbB = (RadioButton) DGR_REQUEST_SME.Items[i].FindControl("rdo_Reject2"),
								rbC = (RadioButton) DGR_REQUEST_SME.Items[i].FindControl("rdo_Pending2");
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

		private void performRequestList(int row, char appr_sta)
		{
			string userid	= Session["UserID"].ToString();
			string id		= "";string query	= "";

			if (this.DGR_REQUEST_SME.Visible == true)
				id		= this.DGR_REQUEST_SME.Items[row].Cells[0].Text.Trim();
			else
				id		= this.DGR_REQUEST_CONNCC.Items[row].Cells[0].Text.Trim();
			
			query	= " exec PARAM_GENERAL_RFBRANCH_ALL_APPR '" +  id + "','" + appr_sta + "'";
			/* AUDITTRAIL
				query	= " exec PARAM_GENERAL_RFBRANCH_ALL_APPR '" +  id + "','" + appr_sta + "','" +
								userid + "'";
								*/
			conn.QueryString = query;
			try 
			{
				conn.ExecuteNonQuery();
			} catch (Exception ex1) {
				Response.Write("<!-- conn: " + ex1.ToString() + " -->");
			}
			connsme.QueryString = query;
			try 
			{
				connsme.ExecuteNonQuery();
			} 
			catch (Exception ex1) 
			{
				Response.Write("<!-- connsme: " + ex1.ToString() + " -->");
			}
			conncons.QueryString = query;
			try 
			{
				conncons.ExecuteNonQuery();
			} 
			catch (Exception ex1) 
			{
				Response.Write("<!-- conncons: " + ex1.ToString() + " -->");
			}
			conncc.QueryString = query;
			try 
			{
				conncc.ExecuteNonQuery();
			} 
			catch (Exception ex1) 
			{
				Response.Write("<!-- conncc: " + ex1.ToString() + " -->");
			}
			//Response.Write(query);
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			if (this.LBL_STA.Text == "0")//SME
			{
				for (int i = 0; i < DGR_REQUEST_SME.Items.Count; i++)
				{
					try
					{
						RadioButton rbA1 = (RadioButton)  DGR_REQUEST_SME.Items[i].FindControl("rdo_Approve2"),
							rbR1 = (RadioButton)  DGR_REQUEST_SME.Items[i].FindControl("rdo_Reject2");
						if (rbA1.Checked)
							performRequestList(i, '1');
						else if (rbR1.Checked)
							performRequestList(i, '0');
					} 
					catch //(Exception p)
					{
						//GlobalTools.popMessage(this,p.Message);
					}

				}
			}
			else
			{
				for (int i = 0; i < DGR_REQUEST_CONNCC.Items.Count; i++)
				{
					try
					{
						RadioButton rbA = (RadioButton)  DGR_REQUEST_CONNCC.Items[i].FindControl("rdo_Approve1"),
							rbR = (RadioButton)  DGR_REQUEST_CONNCC.Items[i].FindControl("rdo_Reject1");
						if (rbA.Checked)
							performRequestList(i, '1');
						else if (rbR.Checked)
							performRequestList(i, '0');
					} 
					catch //(Exception p)
					{
						//GlobalTools.popMessage(this,p.Message);
					}

				}
			}
			ViewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../HostParamApproval.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}
	}
}
