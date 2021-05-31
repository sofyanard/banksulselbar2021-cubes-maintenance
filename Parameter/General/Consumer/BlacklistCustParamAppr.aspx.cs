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

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for BlacklistCustParamAppr.
	/// </summary>
	public partial class BlacklistCustParamAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid, addr, dob;
		protected string cnm1, cnm2, cnm3;
		protected string idnum, npwp, curef;
		protected string mid_old, addr_old, dob_old;
		protected string cnm1_old, cnm2_old, cnm3_old;
		protected string idnum_old, npwp_old, curef_old,cif_old;

	
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

			BindData();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData()
		{
			conn.QueryString = "select PS_SEQ, PS_CIF, CU_REF, PS_FLAG, isnull(PS_NMFIRST,'')+' '+isnull(PS_NMMID,'')+' '+isnull(PS_NMLAST,'') as NAMA, "+
				"PS_KTPNO, PS_DOB, PS_NPWP, CU_HMADDR, CH_STA, "+
				"STATUS = case CH_STA when '1' then 'INSERT' "+
				"when '2' then 'UPDATE' when '3' then 'DELETE' end "+
				"from TPERSONAL order by PS_SEQ";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG_APPR.DataSource = dt;

			try
			{
				DG_APPR.DataBind();
			}
			catch 
			{
				DG_APPR.CurrentPageIndex = DG_APPR.PageCount - 1;
				DG_APPR.DataBind();
			}
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}


		private void deleteData(int row)
		{
			try 
			{
				string sq = DG_APPR.Items[row].Cells[0].Text.Trim();
				string fq = DG_APPR.Items[row].Cells[1].Text.Trim();

				conn.QueryString = "DELETE FROM TPERSONAL WHERE PS_SEQ = '"+sq+"' AND PS_FLAG = '"+fq+"'";
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void ExecSPAuditTrail(string id,string field,string from, string to,string action)
		{
			string tablename;
			string userid = Session["UserID"].ToString();
			tablename = "PERSONAL";
			conn.QueryString = "EXEC PARAM_AUDITTRAIL_INNER '" + id + "','"+tablename+"','" +
				field + "','" + from + "','" + to + "','" + action + "','" + userid + "','" + 
				"" + "'," + "1,'Blacklist Customer'";
			conn.ExecuteNonQuery();
			conn.ClearData();
		}

		private void performRequest(int row)
		{
			int rst = 0;
			
			string seq = DG_APPR.Items[row].Cells[0].Text.Trim();
			string flag = DG_APPR.Items[row].Cells[1].Text.Trim();
			string cif =  cleansText(DG_APPR.Items[row].Cells[2].Text);
			string status = DG_APPR.Items[row].Cells[9].Text.Trim();

			conn.QueryString = "SELECT * FROM TPERSONAL WHERE PS_SEQ = '"+seq+"' AND PS_FLAG = '"+flag+"'";
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0)
			{
				addr = conn.GetFieldValue(0,"CU_HMADDR");
				dob = conn.GetFieldValue(0,"PS_DOB");  
				cnm1 = conn.GetFieldValue(0,"PS_NMFIRST");
				cnm2 = conn.GetFieldValue(0,"PS_NMMID");
				cnm3 = conn.GetFieldValue(0,"PS_NMLAST");
				idnum = conn.GetFieldValue(0,"PS_KTPNO");
				npwp = conn.GetFieldValue(0,"PS_NPWP");	
				curef = conn.GetFieldValue(0,"CU_REF");
			}

			rst = row;

			conn.QueryString = "SELECT * FROM PERSONAL WHERE PS_SEQ = '"+seq+"' AND PS_FLAG = '"+flag+"'";
			conn.ExecuteQuery();

			if(conn.GetRowCount() != 0)
			{
				addr_old = conn.GetFieldValue(0,"CU_HMADDR");
				dob_old = conn.GetFieldValue(0,"PS_DOB");  
				cnm1_old = conn.GetFieldValue(0,"PS_NMFIRST");
				cnm2_old = conn.GetFieldValue(0,"PS_NMMID");
				cnm3_old = conn.GetFieldValue(0,"PS_NMLAST");
				idnum_old = conn.GetFieldValue(0,"PS_KTPNO");
				npwp_old = conn.GetFieldValue(0,"PS_NPWP");	
				curef_old = conn.GetFieldValue(0,"CU_REF");
				cif_old = conn.GetFieldValue(0,"PS_CIF");
			}
			
			if (status.Equals("1"))
			{
				try
				{
					ExecSPAuditTrail(seq,"PS_SEQ","",seq,"1");
					ExecSPAuditTrail(seq,"PS_FLAG","",flag,"1");
					ExecSPAuditTrail(seq,"PS_KTPNO","",idnum,"1");
					ExecSPAuditTrail(seq,"PS_NMFIRST","",cnm1,"1");
					ExecSPAuditTrail(seq,"PS_NMMID","",cnm2,"1");
					ExecSPAuditTrail(seq,"PS_NMLAST","",cnm3,"1");
					ExecSPAuditTrail(seq,"PS_DOB","",dob,"1");
					ExecSPAuditTrail(seq,"PS_NPWP","",npwp,"1");
					ExecSPAuditTrail(seq,"CU_HMADDR","",addr,"1");
					ExecSPAuditTrail(seq,"CU_REF","",curef,"1");
					ExecSPAuditTrail(seq,"PS_CIF","",cif,"1");

					conn.QueryString = "insert into PERSONAL (PS_SEQ, PS_FLAG, PS_DOB, PS_NMFIRST, PS_NMMID, PS_NMLAST, CU_REF, CU_HMADDR, PS_KTPNO, PS_NPWP, PS_CIF) "+
						"values ('"+seq+"', '"+flag+"', "+GlobalTools.ToSQLDate(dob)+", "+GlobalTools.ConvertNull(cnm1)+", "+GlobalTools.ConvertNull(cnm2)+", "+GlobalTools.ConvertNull(cnm3)+","+
						" "+GlobalTools.ConvertNull(curef)+", "+GlobalTools.ConvertNull(addr)+", "+GlobalTools.ConvertNull(idnum)+", "+GlobalTools.ConvertNull(npwp)+", "+GlobalTools.ConvertNull(cif)+")";
					conn.ExecuteQuery();			
				}
				catch
				{
					GlobalTools.popMessage(this, "Cannot insert, data already exists in the table!");
				}
				finally
				{
					deleteData(rst);
				}
				
			}

			if (status.Equals("2"))
			{
				try
				{
					if(idnum.Trim()!= idnum_old.Trim())
					{
						ExecSPAuditTrail(seq,"PS_KTPNO",idnum_old,idnum,"0");
					}
					if(cnm1.Trim()!= cnm1_old.Trim())
					{
						ExecSPAuditTrail(seq,"PS_NMFIRST",cnm1_old,cnm1,"0");
					}
					if(cnm2.Trim()!= cnm2_old.Trim())
					{
						ExecSPAuditTrail(seq,"PS_NMMID",cnm2_old,cnm2,"0");
					}
					if(cnm3.Trim()!= cnm3_old.Trim())
					{
						ExecSPAuditTrail(seq,"PS_NMLAST",cnm3_old,cnm3,"0");
					}
					if(dob.Trim()!= dob_old.Trim())
					{
						ExecSPAuditTrail(seq,"PS_DOB",dob_old,dob,"0");
					}
					if(npwp.Trim()!= npwp_old.Trim())
					{
						ExecSPAuditTrail(seq,"PS_NPWP",npwp_old,npwp,"0");
					}
					if(addr.Trim()!= addr_old.Trim())
					{
						ExecSPAuditTrail(seq,"CU_HMADDR",addr_old,addr,"0");
					}
					if(curef.Trim()!= curef_old.Trim())
					{
						ExecSPAuditTrail(seq,"CU_REF",curef_old,curef,"0");
					}
					if(cif.Trim()!= cif_old.Trim())
					{
						ExecSPAuditTrail(seq,"PS_CIF",cif_old,cif,"0");
					}
					 		
					conn.QueryString = "UPDATE PERSONAL SET PS_DOB = "+GlobalTools.ToSQLDate(dob)+", "+
						"PS_NMFIRST = "+GlobalTools.ConvertNull(cnm1)+", PS_NMMID = "+GlobalTools.ConvertNull(cnm2)+", "+
						"PS_NMLAST = "+GlobalTools.ConvertNull(cnm3)+", CU_HMADDR = "+GlobalTools.ConvertNull(addr)+", "+
						"PS_KTPNO = "+GlobalTools.ConvertNull(idnum)+", PS_NPWP = "+GlobalTools.ConvertNull(npwp)+" "+
						"WHERE PS_SEQ = '"+seq+"' AND PS_FLAG = '"+flag+"'";
					conn.ExecuteQuery();
				}
				catch(Exception Ex)
				{
					string errmsg = Ex.Message.Replace("'","");
					if (errmsg.IndexOf("Last Query:") > 0)		
						errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
					GlobalTools.popMessage(this, errmsg);
				}
				finally
				{
					deleteData(rst);
				}
			}

			if (status.Equals("3"))
			{
				try
				{
					ExecSPAuditTrail(seq,"PS_SEQ",seq,"","2");
					ExecSPAuditTrail(seq,"PS_FLAG",flag,"","2");
					ExecSPAuditTrail(seq,"PS_KTPNO",idnum,"","2");
					ExecSPAuditTrail(seq,"PS_NMFIRST",cnm1,"","2");
					ExecSPAuditTrail(seq,"PS_NMMID",cnm2,"","2");
					ExecSPAuditTrail(seq,"PS_NMLAST",cnm3,"","2");
					ExecSPAuditTrail(seq,"PS_DOB",dob,"","2");
					ExecSPAuditTrail(seq,"PS_NPWP",npwp,"","2");
					ExecSPAuditTrail(seq,"CU_HMADDR",addr,"","2");
					ExecSPAuditTrail(seq,"CU_REF",curef,"","2");
					ExecSPAuditTrail(seq,"PS_CIF",cif,"","2");

					conn.QueryString = "DELETE FROM PERSONAL WHERE PS_SEQ = '"+seq+"' AND PS_FLAG = '"+flag+"'";
					conn.ExecuteQuery();
				}
				catch(Exception Ex)
				{
					string errmsg = Ex.Message.Replace("'","");
					if (errmsg.IndexOf("Last Query:") > 0)		
						errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
					GlobalTools.popMessage(this, errmsg);
				}
				finally
				{
					deleteData(rst);
				}
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
			this.DG_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG_APPR_ItemCommand);
			this.DG_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_APPR_PageIndexChanged);

		}
		#endregion

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DG_APPR.Items.Count; i++)
			{
				RadioButton rbA = (RadioButton)DG_APPR.Items[i].FindControl("Rdb1"),
					rbR = (RadioButton)DG_APPR.Items[i].FindControl("Rdb2");
				
				if(rbA.Checked)
				{
					performRequest(i);
				}
				else if(rbR.Checked)
				{
					deleteData(i);
				}
			}

			BindData();		
		}

		private void DG_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_APPR.CurrentPageIndex = e.NewPageIndex;
 
			BindData(); 
		}

		private void DG_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DG_APPR.Items[i].FindControl("Rdb1"),
								rbB = (RadioButton) DG_APPR.Items[i].FindControl("Rdb2"),
								rbC = (RadioButton) DG_APPR.Items[i].FindControl("Rdb3");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DG_APPR.Items[i].FindControl("Rdb1"),
								rbB = (RadioButton) DG_APPR.Items[i].FindControl("Rdb2"),
								rbC = (RadioButton) DG_APPR.Items[i].FindControl("Rdb3");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DG_APPR.Items[i].FindControl("Rdb1"),
								rbB = (RadioButton) DG_APPR.Items[i].FindControl("Rdb2"),
								rbC = (RadioButton) DG_APPR.Items[i].FindControl("Rdb3");
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
