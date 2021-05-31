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
using Microsoft.VisualBasic;

namespace CuBES_Maintenance.Parameter.General
{
	/// <summary>
	/// Summary description for HolidayParamAppr.
	/// </summary>
	public partial class HolidayParamAppr : System.Web.UI.Page
	{
		protected Connection conn, connsme, conncc, conncons;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			conn = new Connection((string) Session["ConnString"]);

			InitialCon();

			if(!IsPostBack)
			{				
				BindData();
			}
		}


		private void InitialCon()
		{
//			connsme = new Connection ("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp;Pooling=true");
//			conncc = new Connection ("Data Source=10.123.13.18;Initial Catalog=LOSCC2;uid=sa;pwd=dmscorp;Pooling=true");
//			conncons = new Connection ("Data Source=10.123.13.18;Initial Catalog=LOSCONSUMER-UAT;uid=sa;pwd=dmscorp;Pooling=true");
        
			string DB_NAMA,DB_IP,DB_LOGINID,DB_LOGINPWD;
			//SME Conn
			try
			{
				conn.QueryString = "select * from RFMODULE where MODULEID='01'";
				conn.ExecuteQuery();
				if (conn.GetRowCount() > 0)
				{
					DB_NAMA		= conn.GetFieldValue("DB_NAMA");
					DB_IP		= conn.GetFieldValue("DB_IP");
					DB_LOGINID	= conn.GetFieldValue("DB_LOGINID");
					DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
					connsme = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
				}
				else connsme= null;
				conn.ClearData();
			}
			catch{}
			
			//Credit Card Conn
			
			conn.QueryString = "select * from RFMODULE where MODULEID='20'";
			conn.ExecuteQuery();
			if (conn.GetRowCount() > 0)
			{
				DB_NAMA		= conn.GetFieldValue("DB_NAMA");
				DB_IP		= conn.GetFieldValue("DB_IP");
				DB_LOGINID	= conn.GetFieldValue("DB_LOGINID");
				DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
					
				conncc = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
					
			}
			else conncc= null;
			conn.ClearData();
			

			//Consumer
			
			conn.QueryString = "select * from RFMODULE where MODULEID='40'";
			conn.ExecuteQuery();
			
			if (conn.GetRowCount() > 0 ) 
			{
				DB_NAMA		= conn.GetFieldValue("DB_NAMA");
				DB_IP		= conn.GetFieldValue("DB_IP");
				DB_LOGINID	= conn.GetFieldValue("DB_LOGINID");
				DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
				conncons = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
			}
			else conncons= null;
			conn.ClearData();
			
		}


		private void BindData()
		{
            if (conncc != null)
            {
                conncc.QueryString = "select convert(varchar,LB_DATE,103) HL_DATE1,LB_REMARK HL_DESC,PENDING_STATUS CH_STA from PENDING_CC_LIBUR ORDER BY LB_DATE DESC";
                try
                {	//GlobalTools.popMessage(this,"Masuk ke CC");
                    conncc.ExecuteQuery();

                    DataTable dt = new DataTable();
                    dt = conncc.GetDataTable().Copy();
                    DGRequest.DataSource = dt;
                }
                catch
                {
                    GlobalTools.popMessage(this, "Tidak Ada Koneksi");
                }

            }

			if (conncons != null)
			{
				conncons.QueryString = "select convert(varchar,HL_DATE,103) HL_DATE1, HL_DESC,CH_STA from TRFHOLIDAY ORDER BY HL_DATE DESC";
				try
				{  // GlobalTools.popMessage(this,"Masuk ke CONS");
					conncons.ExecuteQuery();

					if (conncons.GetRowCount() >0 )
					{
						DataTable dt = new DataTable();
						dt = conncons.GetDataTable().Copy();
						DGRequest.DataSource = dt;
					}
				}
				catch
				{
					
				}
			}

            if (connsme != null)
            {
                connsme.QueryString = "select convert(varchar,DAY_DATE,103) HL_DATE1, DAY_DESC HL_DESC, PENDINGSTATUS CH_STA from PENDING_RFHOLIDAY ORDER BY DAY_DATE DESC";
                try
                {
                    //GlobalTools.popMessage(this,"Masuk ke SME");
                    connsme.ExecuteQuery();

                    DataTable dt = new DataTable();
                    dt = connsme.GetDataTable().Copy();
                    DGRequest.DataSource = dt;
                }
                catch
                {

                }
            }

			try
			{
				DGRequest.DataBind();
			}
			catch 
			{
				DGRequest.CurrentPageIndex = DGRequest.PageCount - 1;
				DGRequest.DataBind();
			}

			for (int i = 0; i < DGRequest.Items.Count; i++)
			{
				Label no = (Label)DGRequest.Items[i].Cells[0].FindControl("LBL_NO");
				no.Text = (i+1+(DGRequest.CurrentPageIndex)*DGRequest.PageSize).ToString();
				//DGRequest.Items[i].Cells[7].Text = DGRequest.Items[i].Cells[4].Text;
				if (DGRequest.Items[i].Cells[3].Text.Trim() == "1")
				{
					DGRequest.Items[i].Cells[3].Text = "INSERT";
				}
				else if (DGRequest.Items[i].Cells[3].Text.Trim() == "2")
				{
					DGRequest.Items[i].Cells[3].Text = "UPDATE";
				}
				else if (DGRequest.Items[i].Cells[3].Text.Trim() == "3")
				{
					DGRequest.Items[i].Cells[3].Text = "DELETE";
				}
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
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion

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
				case "allReject":
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

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			BindData();
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGRequest.Items.Count; i++)
			{
				
				RadioButton rbA = (RadioButton) DGRequest.Items[i].Cells[4].FindControl("rdo_Approve"),
					rbB = (RadioButton) DGRequest.Items[i].Cells[5].FindControl("rdo_Reject"),
					rbC = (RadioButton) DGRequest.Items[i].Cells[6].FindControl("rdo_Pending");					

				
				if(rbA.Checked == true)
				{
					if(DGRequest.Items[i].Cells[7].Text.Trim() == "1")
					{
						// CONS
						if (conncons != null)
						{
							conncons.QueryString = "EXEC PARAM_GENERAL_RFHOLIDAY_APPR 'INSERT', "+
								GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text)+",'"+
								DGRequest.Items[i].Cells[2].Text.Trim()+"','"+DGRequest.Items[i].Cells[7].Text+"' ";
							//							try
							//							{
							conncons.ExecuteNonQuery();
							//							}
							//							catch {}
							
						}

						// CC
						if (conncc != null)
						{
							conncc.QueryString = "EXEC PARAM_GENERAL_LIBUR_APPR 'INSERT', "+
								GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text)+",'"+
								DGRequest.Items[i].Cells[2].Text.Trim()+"','1','','"+DGRequest.Items[i].Cells[7].Text+"' ";
							try
							{
								conncc.ExecuteNonQuery();
							}
							catch {}
						}

						//SME
						if (connsme != null)
						{
							connsme.QueryString = "EXEC PARAM_GENERAL_RFHOLIDAY_APPR 'INSERT', "+
								GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text)+",'"+
								DGRequest.Items[i].Cells[2].Text.Trim()+"','1','IDR','"+DGRequest.Items[i].Cells[7].Text+"' ";
							try
							{
								connsme.ExecuteNonQuery();
							}
							catch {}
						}						
					}
					else if(DGRequest.Items[i].Cells[7].Text.Trim() == "2")
					{		
						// CONS
						if (conncons != null)
						{
							conncons.QueryString = "EXEC PARAM_GENERAL_RFHOLIDAY_APPR 'UPDATE', "+
								GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text)+",'"+
								DGRequest.Items[i].Cells[2].Text.Trim()+"','"+DGRequest.Items[i].Cells[7].Text+"' ";
							try
							{
								conncons.ExecuteNonQuery();
							}
							catch {}
						}
						
						// CC
						if (conncc != null)
						{
							conncc.QueryString = "EXEC PARAM_GENERAL_LIBUR_APPR 'UPDATE', "+
								GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text)+",'"+
								DGRequest.Items[i].Cells[2].Text.Trim()+"','1','','"+DGRequest.Items[i].Cells[7].Text+"' ";
							try
							{
								conncc.ExecuteNonQuery();
							}
							catch {}
						}

						//SME
						if (connsme != null)
						{
							connsme.QueryString = "EXEC PARAM_GENERAL_RFHOLIDAY_APPR 'UPDATE', "+
								GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text)+",'"+
								DGRequest.Items[i].Cells[2].Text.Trim()+"','1','IDR','"+DGRequest.Items[i].Cells[7].Text+"' ";
							try
							{
								connsme.ExecuteNonQuery();
							}
							catch {}
						}						
					}
					else if(DGRequest.Items[i].Cells[7].Text.Trim() == "3")
					{						
						// CONS
						if (conncons != null)
						{
							conncons.QueryString = "EXEC PARAM_GENERAL_RFHOLIDAY_APPR 'DELETE', "+
								GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text)+",'"+
								DGRequest.Items[i].Cells[2].Text.Trim()+"','"+DGRequest.Items[i].Cells[7].Text+"' ";
							try
							{
								conncons.ExecuteNonQuery();
							}
							catch {}
						}

						// CC
						if (conncc != null)
						{
							conncc.QueryString = "EXEC PARAM_GENERAL_LIBUR_APPR 'DELETE', "+
								GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text)+",'"+
								DGRequest.Items[i].Cells[2].Text.Trim()+"','1','','"+DGRequest.Items[i].Cells[7].Text+"' ";
							try 
							{
								conncc.ExecuteNonQuery();
							}
							catch {}
						}

						//SME
						if (connsme != null)
						{
							connsme.QueryString = "EXEC PARAM_GENERAL_RFHOLIDAY_APPR 'DELETE', "+
								GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text)+",'"+
								DGRequest.Items[i].Cells[2].Text.Trim()+"','1','IDR','"+DGRequest.Items[i].Cells[7].Text+"' ";
							try 
							{
								connsme.ExecuteNonQuery();
							}
							catch {}
						}
					}
				}
				else if(rbB.Checked == true)
				{
					// CONS
					if (conncons != null)
					{
						conncons.QueryString = "delete from trfholiday where HL_DATE = "+GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text);//+" and HL_CODE = "+DGRequest.Items[i].Cells[6].Text;
						try 
						{
							conncons.ExecuteNonQuery();					
						}
						catch {}
					}

					// CC
					if (conncc != null)
					{
						conncc.QueryString = "delete from pending_cc_libur where LB_DATE = "+GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text);
						try
						{
							conncc.ExecuteNonQuery();					
						}
						catch {}
					}

					// SME
					if (connsme != null)
					{
						connsme.QueryString = "delete from pending_rfholiday where DAY_DATE = "+GlobalTools.ToSQLDate(DGRequest.Items[i].Cells[1].Text);
						try 
						{
							connsme.ExecuteNonQuery();					
						}
						catch {}
					}
				}
			}	
			BindData();																
		}
	}
}
