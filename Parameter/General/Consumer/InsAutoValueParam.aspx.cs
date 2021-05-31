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
	/// Summary description for InsAutoValueParam.
	/// </summary>
	public partial class InsAutoValueParam : System.Web.UI.Page
	{
		//protected Connection conn2 = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=CUBESDEVNET;uid=sa;pwd=dmscorp");
		protected Connection conn,conn2;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				DDL_INS_TYPE.Items.Add(new ListItem("--Select--",""));
				conn.QueryString="select ASS_CODE, ASS_DESC from RFASSTYPE";
				conn.ExecuteQuery();
				for(int i=0; i<conn.GetRowCount(); i++)
				{
					DDL_INS_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
				}
				bindData1();
				bindData2();
				LBL_SAVEMODE.Text="1";
			}
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
		}

		private void SetDBConn2()
		{
			conn2.QueryString = "select * from RFMODULE where MODULEID=40";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void bindData1()
		{
			conn.QueryString = "select AI.ASS_CODE, AT.ASS_DESC, AI.ASS_TENOR, AI.ASS_CASHPERCENT, AI.ASS_CREDITPERCENT "+
							   "from RFASSINTR AI "+
							   "join RFASSTYPE AT on AI.ASS_CODE = AT.ASS_CODE where AI.ACTIVE='1' ";
			conn.ExecuteQuery();
			this.DGR_EXISTING.DataSource = conn.GetDataTable().Copy();
			try 
			{
				this.DGR_EXISTING.DataBind();
			}
			catch 
			{
				this.DGR_EXISTING.CurrentPageIndex = this.DGR_EXISTING.PageCount - 1;
				this.DGR_EXISTING.DataBind();
			}
		}

		private void bindData2()
		{
		
			conn.QueryString = "select AI.ASS_CODE, AT.ASS_DESC, AI.ASS_TENOR, AI.ASS_CASHPERCENT, AI.ASS_CREDITPERCENT ,AI.CH_STA "+
							   "from TRFASSINTR AI "+
							   "join RFASSTYPE AT on AI.ASS_CODE = AT.ASS_CODE";
			conn.ExecuteQuery();
			this.DGR_REQUEST.DataSource = conn.GetDataTable().Copy();
			try 
			{
				this.DGR_REQUEST.DataBind();
			}
			catch 
			{
				this.DGR_REQUEST.CurrentPageIndex = this.DGR_REQUEST.PageCount - 1;
				this.DGR_REQUEST.DataBind();
			}
			
			for (int i = 0; i < this.DGR_REQUEST.Items.Count; i++)
			{
				if (this.DGR_REQUEST.Items[i].Cells[4].Text.Trim() == "0")
				{
					this.DGR_REQUEST.Items[i].Cells[4].Text = "UPDATE";
				}
				else if (this.DGR_REQUEST.Items[i].Cells[4].Text.Trim() == "1")
				{
					this.DGR_REQUEST.Items[i].Cells[4].Text = "INSERT";
				}
				else if (this.DGR_REQUEST.Items[i].Cells[4].Text.Trim() == "2")
				{
					this.DGR_REQUEST.Items[i].Cells[4].Text = "DELETE";
				}
			} 
		}

		private void clearEditBoxes()
		{
			DDL_INS_TYPE.SelectedValue="";
			DDL_INS_TYPE.Enabled=true;
			TXT_TENOR.Text="";
			TXT_TENOR.Enabled=true;
			TXT_CASH.Text="";
			TXT_CASH_PCT.Text="";
			TXT_CREDIT.Text="";
			TXT_CREDIT_PCT.Text="";
			LBL_SAVEMODE.Text = "1";
			//activatePostBackControls(true);
		}

		private void cleansTextBox (TextBox tb)
		{
			if (tb.Text.Trim() == "&nbsp;")
				tb.Text = "";
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

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40");
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex=e.NewPageIndex;
			bindData1();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex=e.NewPageIndex;
			bindData2();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					DDL_INS_TYPE.SelectedValue=e.Item.Cells[5].Text.Trim();
					DDL_INS_TYPE.Enabled=false;
					TXT_TENOR.Text=e.Item.Cells[1].Text.Trim();
					TXT_TENOR.Enabled=false;
					string cashnya =e.Item.Cells[2].Text.Trim();
					if(cashnya!="")
					{
						string [] Cash = cashnya.Trim().Split(",".ToCharArray());
						int jmlcash = Cash.Length;
						if (jmlcash>1)
						{
							TXT_CASH.Text=Cash[0].Trim();
							TXT_CASH_PCT.Text=Cash[1].Trim();
						}
						else
						{
							TXT_CASH.Text=Cash[0].Trim();
							TXT_CASH_PCT.Text="0";
						}
					}
					else
					{
						TXT_CASH.Text="0";
						TXT_CASH_PCT.Text="0";
					}

					string creditnya =e.Item.Cells[3].Text.Trim();
					if(creditnya!="")
					{
						string [] Credit = creditnya.Trim().Split(",".ToCharArray());
						int jmlcredit = Credit.Length;
						if (jmlcredit>1)
						{
							TXT_CREDIT.Text=Credit[0].Trim();
							TXT_CREDIT_PCT.Text=Credit[1].Trim();
						}
						else
						{
							TXT_CREDIT.Text=Credit[0].Trim();
							TXT_CREDIT_PCT.Text="0";
						}
					}
					else
					{
						TXT_CREDIT.Text="0";
						TXT_CREDIT_PCT.Text="0";
					}

					LBL_CODE.Text = e.Item.Cells[5].Text.Trim();
					LBL_SAVEMODE.Text = "0";
					
					cleansTextBox(TXT_TENOR);
					cleansTextBox(TXT_CASH);
					cleansTextBox(TXT_CASH_PCT);
					cleansTextBox(TXT_CREDIT);
					cleansTextBox(TXT_CREDIT_PCT);
					//activatePostBackControls(false);
					break;
				case "delete":
					conn.QueryString="select * from TRFASSINTR where ASS_CODE='"+e.Item.Cells[5].Text.Trim()+"' and "+
									 "ASS_TENOR='"+e.Item.Cells[1].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="update TRFASSINTR set CH_STA='2' where ASS_CODE='"+e.Item.Cells[5].Text.Trim()+"' and "+
							"ASS_TENOR='"+e.Item.Cells[1].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_GENERAL_RFASSINTR '1','" +
							e.Item.Cells[5].Text.Trim()+ "', '" + e.Item.Cells[1].Text.Trim() + "','" + GlobalTools.ConvertFloat(e.Item.Cells[2].Text.Trim()) + "', '" + 
							GlobalTools.ConvertFloat(e.Item.Cells[3].Text.Trim())+"', '2' ,'','' " ;
						conn.ExecuteQuery();
					}
					bindData2();
					break;
				
				default:
					// Do nothing.
					break;
			} 
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[4].Text;
					if(status=="DELETE")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					DDL_INS_TYPE.SelectedValue=e.Item.Cells[6].Text.Trim();
					DDL_INS_TYPE.Enabled=false;
					TXT_TENOR.Text=e.Item.Cells[1].Text.Trim();
					TXT_TENOR.Enabled=false;
					string cashnya =e.Item.Cells[2].Text.Trim();
					if(cashnya!="")
					{
						string [] Cash = cashnya.Trim().Split(",".ToCharArray());
						if(Cash.Length > 1)
						{
							TXT_CASH.Text=Cash[0].Trim();
							TXT_CASH_PCT.Text=Cash[1].Trim();
						}
						else
						{
							TXT_CASH.Text=Cash[0].Trim();
							TXT_CASH_PCT.Text="0";
						}
					}
					else
					{
						TXT_CASH.Text="0";
						TXT_CASH_PCT.Text="0";
					}

					string creditnya =e.Item.Cells[3].Text.Trim();
					if(creditnya!="")
					{
						string [] Credit = creditnya.Trim().Split(",".ToCharArray());
						if(Credit.Length > 1)
						{
							TXT_CREDIT.Text=Credit[0].Trim();
							TXT_CREDIT_PCT.Text=Credit[1].Trim();
						}
						else
						{
							TXT_CREDIT.Text=Credit[0].Trim();
							TXT_CREDIT_PCT.Text="0";
						}
					}
					else
					{
						TXT_CREDIT.Text="0";
						TXT_CREDIT_PCT.Text="0";
					}

					LBL_CODE.Text = e.Item.Cells[6].Text.Trim();
					if(e.Item.Cells[4].Text=="UPDATE")
					{
						LBL_SAVEMODE.Text = "0";
					}
					if(e.Item.Cells[4].Text=="INSERT")
					{
						LBL_SAVEMODE.Text = "1";
					}
					
					cleansTextBox(TXT_TENOR);
					cleansTextBox(TXT_CASH);
					cleansTextBox(TXT_CASH_PCT);
					cleansTextBox(TXT_CREDIT);
					cleansTextBox(TXT_CREDIT_PCT);
					//activatePostBackControls(false);
					break;
				case "delete":
					string CODE = e.Item.Cells[6].Text.Trim();
					string TENOR = e.Item.Cells[1].Text.Trim();
					conn.QueryString = "EXEC PARAM_GENERAL_RFASSINTR '2','" +
						CODE+ "', '" + TENOR + "','', '','','','' " ;
					conn.ExecuteQuery();
					bindData2();
					break;
				default:
					// Do nothing.
					break;
			}  
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				conn.QueryString = "select * from RFASSINTR where ASS_CODE='"+DDL_INS_TYPE.SelectedValue+"' and "+
					"ASS_TENOR='"+TXT_TENOR.Text+"'";
				conn.ExecuteQuery();
				if(conn.GetRowCount() > 0)
				{
					GlobalTools.popMessage(this,"Insurance Type & Tenor has already been used!");
					return;
				}
				conn.QueryString = "select * from TRFASSINTR where ASS_CODE='"+DDL_INS_TYPE.SelectedValue+"' and "+
					"ASS_TENOR='"+TXT_TENOR.Text+"'";
				conn.ExecuteQuery();
				if(conn.GetRowCount() > 0)
				{
					GlobalTools.popMessage(this,"Insurance Type & Tenor has already been used!");
					return;
				}
			}		
			string cash1 = GlobalTools.ConvertFloat(TXT_CASH.Text+','+TXT_CASH_PCT.Text);
			string credit1 = GlobalTools.ConvertFloat(TXT_CREDIT.Text+','+TXT_CREDIT_PCT.Text);
			conn.QueryString="select * from TRFASSINTR where ASS_CODE='"+DDL_INS_TYPE.SelectedValue+"' and "+
				"ASS_TENOR='"+TXT_TENOR.Text+"'";
			conn.ExecuteQuery();
			if(conn.GetRowCount()!=0)
			{
				conn.QueryString = "exec PARAM_GENERAL_RFASSINTR '6','"+this.DDL_INS_TYPE.SelectedValue +"','" +
					this.TXT_TENOR.Text + "','" + cash1 + "','" +
					credit1 +"','"+LBL_SAVEMODE.Text+"','',''";
				conn.ExecuteQuery();
			}
			else
			{
				conn.QueryString = "exec PARAM_GENERAL_RFASSINTR '1','"+this.DDL_INS_TYPE.SelectedValue +"','" +
					this.TXT_TENOR.Text + "','" + cash1 + "','" +
					credit1 +"','"+LBL_SAVEMODE.Text+"','',''";
				conn.ExecuteQuery();
			}
			bindData2();
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}
	}
}
