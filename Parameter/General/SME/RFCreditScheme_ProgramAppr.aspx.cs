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
	/// Summary description for RFCreditScheme_ProgramAppr.
	/// </summary>
	public partial class RFCreditScheme_ProgramAppr : System.Web.UI.Page
	{
		protected Connection conn, con1;
		protected string userid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
			userid = Session["UserID"].ToString();

			if (!IsPostBack)
			{
				bindData();
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
			this.DTG_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DTG_APPR_ItemCommand);
			this.DTG_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DTG_APPR_PageIndexChanged);

		}
		#endregion

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DTG_APPR.Items.Count; i++)
			{
				try
					
				{
					//DTG_APPR.Items[i].Cells[3].Text = "TEST";
					RadioButton rbA = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton1"),
						rbR = (RadioButton) DTG_APPR.Items[i].FindControl("Radiobutton2");
					if (rbA.Checked)
					{
						performRequest(i, "1");
					}
					else if (rbR.Checked)
					{
						//deleteData(i);
						performRequest(i, "0");
					}
				} 
				catch {}
			}
			bindData();
		
		}

		private void bindData()
		{
			// todo : jadikan view
			conn.QueryString = "SELECT * FROM VW_PARAM_GENERAL_PENDING_RFCREDITSCHEME_PROGRAM "+
				" ORDER BY PROGRAMDESC, CREDITSCHEME  ";
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
				DTG_APPR.CurrentPageIndex = 0;
				DTG_APPR.DataBind();
			}

		}

//		private void deleteData(int row)
//		{
//			try 
//			{
//				string a = DTG_APPR.Items[row].Cells[0].Text.Trim();
//				string b = DTG_APPR.Items[row].Cells[1].Text.Trim();
//				string c = DTG_APPR.Items[row].Cells[2].Text.Trim();
//
//				// todo : jadikan sp		
////				conn.QueryString = "DELETE FROM PENDING_RFCREDITSCHEME_PROGRAM WHERE SEQ = '" + a +
////					"' AND PROGRAMID = '" + b +
////					"' AND CREDITSCHEMEID = '" + c + "' ";
////				//	"' AND LG_CODE = '" + e.Trim().Replace("&nbsp;","") + "'";
//				conn.ExecuteNonQuery();
//
//			} 
//			catch {}
//		}


		private void performRequest(int row, string apprFlag)
		{
			/// _apprflag :
			/// 1 : approve
			/// 0 : reject
			/// 

			try 
			{
			
				string a = DTG_APPR.Items[row].Cells[0].Text.Trim();
				string b = DTG_APPR.Items[row].Cells[1].Text.Trim();
				string c = DTG_APPR.Items[row].Cells[2].Text.Trim();
				string status=DTG_APPR.Items[row].Cells[3].Text.Trim();

				conn.QueryString = "EXEC PARAM_GENERAL_RFCREDITSCHEME_PROGRAM_APPR "+
						" '"+b+"','"+c+"','"+apprFlag+"','"+userid+"' ";
				conn.ExecuteQuery();
				

				// todo : jadikan sp
				
//				//if (status.Equals("UPDATE"))
//				if (status=="0")
//				{
//					//conn.QueryString = "UPDATE RFBMRATING_III SET BMR_CODE='"+a+"',BMR2_CODE="+b+",BMR3_CODE='"+c+"',BMR3_DESC='"+d+"',BMR3_ACTIVE='1'  WHERE BMR_CODE='"+a+"' AND BMR2_CODE="+b+" AND BMR3_CODE='"+c+"' ";
//					conn.QueryString = "UPDATE RFCREDITSCHEME_PROGRAM SET PROGRAMID='"+b+"', CREDITSCHEMEID='"+c+"' WHERE SEQ='"+a+"' ";
//					conn.ExecuteNonQuery();
//				}
//
//				//if (status.Equals("INSERT"))
//				if (status=="1")
//				{
//					conn.QueryString = "INSERT INTO RFCREDITSCHEME_PROGRAM (SEQ, PROGRAMID, CREDITSCHEMEID) VALUES ('"+a+"',"+b+",'"+c+"')";
//					conn.ExecuteNonQuery();
//				}
//				
//				//if (status.Equals("DELETE"))
//				if (status=="2")
//				{
//					conn.QueryString = "DELETE RFCREDITSCHEME_PROGRAM WHERE SEQ='"+a+"' AND PROGRAMID="+b+" AND CREDITSCHEMEID='"+c+"' ";
//					conn.ExecuteNonQuery();
//				}
//
//				conn.QueryString = "DELETE PENDING_RFCREDITSCHEME_PROGRAM WHERE SEQ='"+a+"' AND PROGRAMID="+b+" AND CREDITSCHEMEID='"+c+"' ";
//				conn.ExecuteNonQuery();

				
			} 
			catch {}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
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

		private void DTG_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DTG_APPR.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData();
		}


	}
}
