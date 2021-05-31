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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for DTBOItemsParamAppr.
	/// </summary>
	public partial class DTBOItemsParamAppr : System.Web.UI.Page
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

			DTG_APPR.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change);
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

			InitialCon(); 

			bindData();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void bindData()
		{
			conn.QueryString = "SELECT CW_CODE,CW_DESC,SIBS_FLAG,CW_PRINT,CD_SIBS,CH_STA,(case when SIBS_FLAG=1 then 'Yes' else 'No' End) FLAG,(case when CW_PRINT=1 then 'Yes' else 'No' End) PRINTT FROM TRFCAWLST";
			conn.ExecuteQuery();

			DTG_APPR.DataSource = conn.GetDataTable().Copy();
			
			try 
			{
				DTG_APPR.DataBind();
			}
			catch 
			{
				DTG_APPR.CurrentPageIndex = DTG_APPR.PageCount - 1;
				DTG_APPR.DataBind();
			}

			for (int i = 0; i < DTG_APPR.Items.Count; i++)
			{
				if (DTG_APPR.Items[i].Cells[7].Text.Trim() == "0")
				{
					DTG_APPR.Items[i].Cells[7].Text = "UPDATE";
				}
				else if (DTG_APPR.Items[i].Cells[7].Text.Trim() == "1")
				{
					DTG_APPR.Items[i].Cells[7].Text = "INSERT";
				}
				else if (DTG_APPR.Items[i].Cells[7].Text.Trim() == "2")
				{
					DTG_APPR.Items[i].Cells[7].Text = "DELETE";
				}
			}
		}

		private void performRequest(int row)
		{
			string userid = Session["UserID"].ToString();
			string groupid = Session["GroupID"].ToString();
			string status = DTG_APPR.Items[row].Cells[7].Text.Trim();
			string kode = DTG_APPR.Items[row].Cells[2].Text.Trim();
			string desc = DTG_APPR.Items[row].Cells[3].Text.Trim();
			string upload = DTG_APPR.Items[row].Cells[0].Text.Trim();
			string print = DTG_APPR.Items[row].Cells[1].Text.Trim();
			string sbc = DTG_APPR.Items[row].Cells[6].Text.Trim();

			try 
			{
				if (status.Equals("UPDATE"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_DTBOITEM '3','" +
						kode+"', '"+desc+"', '"+ 
						upload+"', '"+print+"', '1', "+GlobalTools.ConvertNull(sbc)+" ,'"+userid+"','"+groupid+"'";
					conn.ExecuteNonQuery();
				}

				if (status.Equals("INSERT"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_DTBOITEM '4','" +
						kode+"', '"+desc+"', '"+ 
						upload+"', '"+print+"', '1', "+GlobalTools.ConvertNull(sbc)+" ,'"+userid+"','"+groupid+"'";
					conn.ExecuteNonQuery();
				}
				
				if (status.Equals("DELETE"))
				{
					conn.QueryString = "EXEC PARAM_GENERAL_DTBOITEM '5','" +
						kode+ "', '"+desc+"', '"+ 
						upload + "', '"+print+"', '1', '' ,'"+userid+"','"+groupid+"'";
					conn.ExecuteNonQuery();
				}

				kode = DTG_APPR.Items[row].Cells[2].Text.Trim();
				
				conn.QueryString = "EXEC PARAM_GENERAL_DTBOITEM '2','"+kode+"', '','','','','' ,'"+userid+"','"+groupid+"'";
				conn.ExecuteNonQuery();
				
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string kode = DTG_APPR.Items[row].Cells[2].Text.Trim();
				
				conn.QueryString = "EXEC PARAM_GENERAL_DTBOITEM '2','"+kode+"','','','','','' ,'',''" ;
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
			this.DTG_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DTG_APPR_ItemCommand);

		}
		#endregion

		void Grid_Change(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DTG_APPR.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData();	
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc=9902040102&ModuleId=40");
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DTG_APPR.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
						rbR = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2");
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
			bindData();
		}

		private void DTG_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DTG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
								rbB = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2"),
								rbC = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton3");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DTG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
								rbB = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2"),
								rbC = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton3");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DTG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
								rbB = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2"),
								rbC = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton3");
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
	}
}
