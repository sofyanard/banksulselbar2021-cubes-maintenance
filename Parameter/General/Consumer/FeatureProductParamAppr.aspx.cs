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
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for FeatureProductParamAppr.
	/// AuditTrail included but, still in comment!!
	/// </summary>
	public partial class FeatureProductParamAppr : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn,conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			
			if (!IsPostBack)
			{
				LBL_ACTIVE.Text = Request.QueryString["active"];
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

				viewPendingData();
				//CodeGen();	-- not needed
			}
			
			DGRequest.PageIndexChanged +=new DataGridPageChangedEventHandler(DGRequest_PageIndexChanged);
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
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion
		
		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();

			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}
		
		private void CodeGen()
		{
			int digits,digits2,digits3;
			string zeros,zeros2,codes;
			
			//menentukan code berikutnya
			//cek dulu apa sudah ada di pending, trus bisa ke master
			conn.QueryString="select convert(int,isnull(max(feature_id),0)) from FEATURE where active=1";
			conn.ExecuteQuery();
			zeros = conn.GetFieldValue(0,0);
				
			digits = zeros.Length;
			digits2 = Int32.Parse(zeros)+1;
			zeros2 = digits2.ToString();
			codes = digits2.ToString();
			for (digits3 = zeros2.Length;digits3<2;digits3++)
			{
				codes='0' + codes;
			}
			lbl_max.Text=codes;
		}
		private void viewPendingData()
		{
			conn.QueryString = "select FEATURE_ID,FEATURE_DESC,FORMULA,FEATURE_TABLE,FEATURE_LINK,"+
				"PR_DESC,PRODUCTNAME,FEATURE_JOBTYPE, "+
				"case when FEATURE_ACTIVE='1' then 'Active' else '' end ACTIVE,"+
				"PRM_CODE,PR_CODE,PRODUCTID,JOB_TYPE_ID,FEATURE_ACTIVE,MIN_VALUE,MAX_VALUE,CH_STA,"+
				"case when CH_STA='0' then 'Update' when CH_STA='1' then 'Insert' when CH_STA='2' then 'Delete' else '' end CH_STA1,DESCRIPTION,FEATURE_FIELD "+
				"from TFEATURE a left join TPRODUCT b on a.PRODUCT = b.PRODUCTID "+
				"left join PROGRAM c on a.PROGRAM = c.PR_CODE "+
				"LEFT JOIN JOB_TYPE d ON d.JOB_TYPE_ID=a.FEATURE_JOBTYPE";
			conn.ExecuteQuery();

			DGRequest.DataSource = conn.GetDataTable().Copy();
			try 
			{
				DGRequest.DataBind();
			}
			catch 
			{
				DGRequest.CurrentPageIndex = DGRequest.PageCount - 1;
				DGRequest.DataBind();
			}
		}
		private void DGRequest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;			
			viewPendingData();
		}
		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void performRequest(int row)
		{
			try 
			{
				string id      = CleansText(DGRequest.Items[row].Cells[0].Text.Trim());
				string desc    = CleansText(DGRequest.Items[row].Cells[1].Text.Trim());
				string form	   = CleansText(DGRequest.Items[row].Cells[2].Text.Trim());
				string tbl     = CleansText(DGRequest.Items[row].Cells[3].Text.Trim());
				string lnk	   = CleansText(DGRequest.Items[row].Cells[4].Text.Trim());
				string pr      = CleansText(DGRequest.Items[row].Cells[10].Text.Trim());
				string pd	   = CleansText(DGRequest.Items[row].Cells[11].Text.Trim());
				string job	   = CleansText(DGRequest.Items[row].Cells[12].Text.Trim());
				string actv	   = CleansText(DGRequest.Items[row].Cells[13].Text.Trim());
				string min     = CleansText(DGRequest.Items[row].Cells[14].Text.Trim());
				string max	   = CleansText(DGRequest.Items[row].Cells[15].Text.Trim());
				string prm     = CleansText(DGRequest.Items[row].Cells[9].Text.Trim());
				string desc1   = CleansText(DGRequest.Items[row].Cells[18].Text.Trim());
				string field   = CleansText(DGRequest.Items[row].Cells[19].Text.Trim());
				executeApproval(id,desc,form,tbl,lnk,pr,pd,job,actv,min,max,prm,desc1, "1", field);
			} 
			catch {}
		}

		private void deleteData(int row)
		{
			try 
			{
				string id      = CleansText(DGRequest.Items[row].Cells[0].Text.Trim());
				string desc    = CleansText(DGRequest.Items[row].Cells[1].Text.Trim());
				string form	   = CleansText(DGRequest.Items[row].Cells[2].Text.Trim());
				string tbl     = CleansText(DGRequest.Items[row].Cells[3].Text.Trim());
				string lnk	   = CleansText(DGRequest.Items[row].Cells[4].Text.Trim());
				string pr      = CleansText(DGRequest.Items[row].Cells[10].Text.Trim());
				string pd	   = CleansText(DGRequest.Items[row].Cells[11].Text.Trim());
				string job	   = CleansText(DGRequest.Items[row].Cells[12].Text.Trim());
				string actv	   = CleansText(DGRequest.Items[row].Cells[13].Text.Trim());
				string min     = CleansText(DGRequest.Items[row].Cells[14].Text.Trim());
				string max	   = CleansText(DGRequest.Items[row].Cells[15].Text.Trim());
				string prm     = CleansText(DGRequest.Items[row].Cells[9].Text.Trim());
				string desc1   = CleansText(DGRequest.Items[row].Cells[18].Text.Trim());
				string field   = CleansText(DGRequest.Items[row].Cells[19].Text.Trim());
				executeApproval(id,desc,form,tbl,lnk,pr,pd,job,actv,min,max,prm,desc1, "0", field);
			} 
			catch (Exception p)
			{
				GlobalTools.popMessage(this,p.Message);
			}

		}

		private void executeApproval(string id,string desc,string form,string tbl,string lnk,string pr,string pd,string job,string actv,string min,string max,string prm,string desc1, string approvalFlag, string field) 
		{
			//AuditTrail
			
			string userid	= Session["UserID"].ToString();
			conn.QueryString = "exec PARAM_AUDITTRAIL_FEATURE '" + id + "','" + userid  +"'";
			conn.ExecuteNonQuery();
			//*****
			
			string myq="";
			int jumlah;
			string pendingStatus;

			if (approvalFlag == "1") 
			{
				conn.QueryString = "SELECT * FROM TFEATURE WHERE FEATURE_ID = '" +id+ "'";
				conn.ExecuteQuery();
				pendingStatus	= conn.GetFieldValue("CH_STA");
				
				conn.QueryString = "SELECT * FROM FEATURE WHERE FEATURE_ID = '" +id+ "'";
				conn.ExecuteQuery();

				jumlah = conn.GetRowCount();

				if (pendingStatus == "0" || pendingStatus == "1") 
				{
					if (jumlah > 0) 
					{
						myq = "UPDATE FEATURE SET FEATURE_DESC='"+desc+"', "+
							"PRM_CODE='"+prm+"', FEATURE_TABLE='"+tbl+"', FEATURE_FIELD='"+field+"', "+
							"FEATURE_LINK='"+lnk+"', FEATURE_JOBTYPE='"+job+"', FORMULA='"+form+"', "+
							"PRODUCT='"+pd+"',PROGRAM='"+pr+"',FEATURE_ACTIVE='"+actv+"',"+
							"MIN_VALUE='"+min+"',MAX_VALUE='"+max+"',DESCRIPTION='"+desc1+"' "+
							"WHERE FEATURE_ID='"+id+"'";
						try{conn.QueryString=myq;
							conn.ExecuteQuery();}
						catch (Exception p)
						{
							GlobalTools.popMessage(this,p.Message);
						}

					}
					else 
					{
						//id = lbl_max.Text.Trim(); //id baru dari codegen		--commented by nyoman. already done in maker. code in this line are due to the id is not found in feature as indicated by the variable jumlah. 
						myq = "INSERT INTO FEATURE VALUES ('"+id+"', '"+desc+"', '"+prm+"', '"+tbl+"', '"+field+"', '"+lnk+"', '"+job+"', '"+form+"', '"+pd+"','"+pr+"','"+actv+"','','"+min+"','"+max+"','"+desc1+"','1')";
						//if(LBL_ACTIVE.Text.Trim() == "1")
						//	myq = "INSERT INTO FEATURE VALUES ('"+id+"', '"+desc+"', '"+prm+"', '"+tbl+"', '', '"+lnk+"', '"+job+"', '"+form+"', '"+pd+"','"+pr+"','"+actv+"','','"+min+"','"+max+"','"+desc1+"','1')";
						//else
						//	myq = "INSERT INTO FEATURE VALUES ('"+id+"', '"+desc+"', '"+prm+"', '"+tbl+"', '', '"+lnk+"', '"+job+"', '"+form+"', '"+pd+"','"+pr+"','"+actv+"','','"+min+"','"+max+"','"+desc1+"','')";
						
						try{conn.QueryString=myq;
							conn.ExecuteQuery();}
						catch (Exception p)
						{
							GlobalTools.popMessage(this,p.Message);
						}

						//CodeGen();	-- not needed
					}

				}
				else if (pendingStatus == "2") 
				{
					if (jumlah > 0) 
					{
						if (LBL_ACTIVE.Text.Trim() == "1")
							myq = "UPDATE FEATURE SET ACTIVE = '0' WHERE FEATURE_ID = '" +id+ "'";
						else
							myq = "DELETE FROM FEATURE WHERE FEATURE_ID = '" +id+ "'";
						
						conn.QueryString=myq;
						conn.ExecuteNonQuery();
						//CodeGen();	-- not needed
					}
				}
			}
			
			conn.QueryString = "DELETE FROM TFEATURE WHERE FEATURE_ID = '" +id+ "'";
			conn.ExecuteNonQuery();
		}
		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGRequest.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGRequest.Items[i].FindControl("rdo_Pending");
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

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGRequest.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGRequest.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGRequest.Items[i].FindControl("rdo_Reject");
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
			viewPendingData();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamApprovalAll.aspx?ModuleId=40");
		}
	}
}
