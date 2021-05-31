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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFCAFinstatementAppr.
	/// </summary>
	public partial class RFCAFinstatementAppr : System.Web.UI.Page
	{

		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//conn = new Connection ("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp;Pooling=true");			
			if (!IsPostBack)
			{
				bindData();
			}
			DTG_APPR.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change);
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
		private void bindData()
		{
			conn.QueryString = "SELECT * FROM VW_PARAM_GENERAL_PENDING_RFCAFINSTATEMENT " +
				"order by PROGRAMDESC, areaname, nasabahdesc";
			conn.ExecuteQuery();
			System.Data.DataTable dt = new System.Data.DataTable();
			dt = conn.GetDataTable().Copy();
		
			DTG_APPR.DataSource = dt;
			try
			{
				DTG_APPR.DataBind();
			}
			catch
			{
				DTG_APPR.CurrentPageIndex =0;
				DTG_APPR.DataBind();
			}
		}


		private void deleteData(int row)
		{
//			try 
//			{
				string a = DTG_APPR.Items[row].Cells[0].Text.Trim();
				string b = DTG_APPR.Items[row].Cells[1].Text.Trim();
				string c = DTG_APPR.Items[row].Cells[2].Text.Trim();
				string d = DTG_APPR.Items[row].Cells[3].Text.Trim();
				string e = DTG_APPR.Items[row].Cells[4].Text.Trim();
		
				//conn.QueryString = "exec PARAM_GENERAL_RFCAFINSTATEMENT_APPR '" + a + "', '" + b + "', '" + c + "', '" + d +"', '0', '" + Session["UserID"].ToString() + "'";
				/// kayaknya ga masuk audittrail 
//				conn.QueryString = "exec PARAM_GENERAL_RFCAFINSTATEMENT_APPR '" + a + "', '" + b + "', '" + c + "', '" + d +"', '0', 'ADMIN'";
//				conn.ExecuteNonQuery();

			
				conn.QueryString = "DELETE FROM PENDING_RFCAFINSTATEMENT WHERE SEQ = '" + a +
									"' AND PROGRAMID = '" + b +
									"' AND AREAID = '" + c + 
									"' AND NASABAHID = '" + d + 
									"' AND LG_CODE = '" + e.Trim().Replace("&nbsp;","") + "'";
				conn.ExecuteNonQuery();
				
//			} 
//			catch {}
		}

		void Grid_Change(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DTG_APPR.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
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


		private void performRequest(int row)
		{
//			try 
//			{
				string a = DTG_APPR.Items[row].Cells[0].Text.Trim();
				string b = DTG_APPR.Items[row].Cells[1].Text.Trim();
				string c = DTG_APPR.Items[row].Cells[2].Text.Trim();
				string d = DTG_APPR.Items[row].Cells[3].Text.Trim();

				conn.QueryString = "exec PARAM_GENERAL_RFCAFINSTATEMENT_APPR '" + a + "', '" + b + "', '" + c + "', '" + d +"', '1', '"+ Session["UserID"].ToString() + "'";
				conn.ExecuteNonQuery();

				/**
				string e = DTG_APPR.Items[row].Cells[4].Text.Trim();
				string status=DTG_APPR.Items[row].Cells[5].Text.Trim();
								
				//if (status.Equals("UPDATE"))
				if (status=="0")
				{
					//conn.QueryString = "UPDATE RFBMRATING_III SET BMR_CODE='"+a+"',BMR2_CODE="+b+",BMR3_CODE='"+c+"',BMR3_DESC='"+d+"',BMR3_ACTIVE='1'  WHERE BMR_CODE='"+a+"' AND BMR2_CODE="+b+" AND BMR3_CODE='"+c+"' ";
					conn.QueryString = "UPDATE RFCAFINSTATEMENT SET LG_CODE='"+e+"' WHERE SEQ='"+a+"' AND PROGRAMID='"+b+"' AND AREAID='"+c+"' AND NASABAHID ='"+d+"'";
					conn.ExecuteNonQuery();
				}

				//if (status.Equals("INSERT"))
				if (status=="1")
				{
					conn.QueryString = "INSERT INTO RFCAFINSTATEMENT (SEQ,ProgramID,AreaID,NasabahID,LG_CODE) VALUES ('"+a+"',"+b+",'"+c+"', '"+d+"','"+e+"')";
					conn.ExecuteNonQuery();
				}
				
				//if (status.Equals("DELETE"))
				if (status=="2")
				{
					conn.QueryString = "DELETE RFCAFINSTATEMENT WHERE SEQ='"+a+"' AND PROGRAMID="+b+" AND AREAID='"+c+"' AND NASABAHID ='" + d + "' AND LG_CODE = '" + e + "'";
					conn.ExecuteNonQuery();
				}

				conn.QueryString = "DELETE PENDING_RFCAFINSTATEMENT WHERE SEQ='"+a+"' AND PROGRAMID="+b+" AND AREAID='"+c+"' AND NASABAHID ='" + d + "' AND LG_CODE = '" + e + "'";
				conn.ExecuteNonQuery();
				**/
				
//			} 
//			catch {}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DTG_APPR.Items.Count; i++)
			{
//				try
//					
//				{
//					//DTG_APPR.Items[i].Cells[3].Text = "TEST";
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
//				} 
//				catch {}
			}
			bindData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApproveAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
			
		}
	}
}
