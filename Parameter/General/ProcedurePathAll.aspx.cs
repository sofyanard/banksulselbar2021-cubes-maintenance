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

namespace CuBES_Maintenance.Parameter.General
{
	/// <summary>
	/// Summary description for ProcedurePathAll.
	/// </summary>
	public partial class ProcedurePathAll : System.Web.UI.Page
	{
		protected Connection conn;// = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc,conncons,connsme;
				
		string [] parameter = new string[10];//!!!!!!!!!
		string [] desc = new string[10];//!!!!!!!!!
		string [] tablename,ddlval,strtemp;
		DropDownList []	ddl;
		Label [] lbl,lbl_val;
		int jk,jmlpar;
		int exist;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn();
			if (!IsPostBack)
			{
				this.LBL_SAVEMODE.Text = "1";
                string module = Request.QueryString["moduleID"];
				if (module == "" || module == null)
					this.LBL_STA.Text = "20";
				else
					this.LBL_STA.Text = module;
				
				try
				{
					this.RBL_MODULE.SelectedValue = this.LBL_STA.Text.Trim();
				} 
				catch {}
				ControlSME();
				jk=0;jmlpar=0;exist=0;
				ControlDDLSEQ(true);
				ViewExistingData();
			} 
			GetPath("0");
			ViewPendingData();
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			this.DGR_EXISTING_PATH.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_EXISTING_PATH_PageIndexChanged);
		}

/*
		private void ProcedurePathAll_PreRender(object sender, EventArgs e)
		{
			GetPath("0");
		}
*/
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
			this.DGR_EXISTING_PATH.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_PATH_ItemCommand);
			this.DGR_EXISTING_PATH.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PATH_PageIndexChanged);
			this.DGR_REQUEST_PATH.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_PATH_ItemCommand);
			this.DGR_REQUEST_PATH.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PATH_PageIndexChanged);

		}
		#endregion

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

		private void SetInterface()
		{
			if (this.LBL_STA.Text	== "01")
			{
				Response.Redirect("../Area/SME/RFProcedurePath.aspx");
			}
			else
			{
				ViewExistingData();
				ViewPendingData();
			}
			ClearBoxes();
		}

		private void ControlSME()
		{
			if (this.LBL_STA.Text == "01")
			{
				Response.Redirect("../Area/SME/RFProcedurePath.aspx");
				this.TR_TRACK_SEQ.Visible = false;
				this.DGR_EXISTING_PATH.Visible = false;
			} 
			else
			{
				this.TR_TRACK_SEQ.Visible = true;
				this.DGR_EXISTING_PATH.Visible = true;
			}
		}

		private void ViewExistingData()
		{
			ControlGridPath(LBL_FIELDS.Text.Trim());
		}

		private void ViewPendingData()
		{
			ControlGridPathPending(LBL_FIELDS.Text.Trim());
		}

		private string GetBackPendingStatus(string saveMode) 
		{
			string status = "";			
			switch (saveMode.ToLower())
			{
				case "update":
					status = "0";
					break;
				case "insert":
					status = "1";
					break;
				case "delete":
					status = "2";
					break;
				default:
					status = "";
					break;
			}
			return status;
		}

		private void ControlDDLSEQ(bool sta)
		{
			if (sta == true)
			{
				this.TR_TRACK_SEQ.Visible = true;
				FillDDLTrackSeq();
			}
			else
				this.TR_TRACK_SEQ.Visible = false;
		}

		private void FillDDLTrackSeq()
		{
			if (this.LBL_STA.Text == "20")
				GlobalTools.fillRefList(this.DDL_TRACK_SEQ,"select * from VW_RFTRACKSEQ",conncc);
			else if (this.LBL_STA.Text == "40")
				GlobalTools.fillRefList(this.DDL_TRACK_SEQ,"select * from VW_RFTRACKSEQ",conncons);
			else
				this.TR_TRACK_SEQ.Visible = false;
			
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		public void GetPath(string st)
		{
			jk=0;
			string type = "",par=""; string querytemp="";
			if (this.LBL_STA.Text == "01")
				type = "01";
			else if (this.LBL_STA.Text == "20")
				type = "02";
			else 
				type = "03";
           
			connsme.QueryString = "select PARAMETER from RFPATHPARAMETER where TYPEID ='" + type+ "'";
			connsme.ExecuteQuery();
			par = connsme.GetFieldValue("PARAMETER");
			par = par.Replace('|','.');
			//split data
			int n = 0;
			if (par.Length > 0)
			{
				char[] sep = new char[]{'.'};
				foreach (string ss in par.Split(sep))
				{
					parameter[n]= ss;
					connsme.QueryString = "select [DESC]from RFPATHPARAMETERDESC where TYPEID ='" + 
						type+ "' and PARAMETER = '" + parameter[n] + "'";
					connsme.ExecuteQuery();
					desc[n]		= connsme.GetFieldValue("DESC");
					n++;	
				}
			}
			try
			{
				jmlpar = n-1;			
			} 
			catch {jmlpar=0;}

			this.LBL_JMLPAR.Text	= jmlpar.ToString();
		    ddl= new DropDownList[jmlpar];
			lbl = new Label[jmlpar];
			lbl_val = new Label[jmlpar];
			desc = new string[jmlpar];
			tablename = new string[jmlpar];
			ddlval = new string[jmlpar];
			strtemp = new string[jmlpar];

			for (int j =0;j<jmlpar; j++)
			{
				ControlInterface(type,parameter[j],j,st);
				querytemp += parameter[j] + " as [" + desc[j] + "] , ";
			}
		}

		private void ControlInterface(string type,string paramname,int j,string st)
		{
			if (paramname != "" && paramname != null)
			{
				int row = jk;
				string desc			= "";
				string tablename	= "";
				connsme.QueryString = "select [DESC],TABLENAME from RFPATHPARAMETERDESC where TYPEID ='" + 
					type+ "' and PARAMETER = '" + paramname + "'";
				connsme.ExecuteQuery();
				desc		= connsme.GetFieldValue("DESC");
				tablename	= connsme.GetFieldValue("TABLENAME");
				
				this.LBL_NUM.Text	= j.ToString();
				Label lbl			= new Label();
				lbl.ID				= "LBL_" + j;
				lbl.Text			= paramname;
				lbl.Visible			= false;
				ddl[j]				= new DropDownList();
				ddl[j].CssClass		= "mandatory";
				ddl[j].EnableViewState	= true;
				//ddl[j].AutoPostBack		= true;
				//ddl[j].SelectedIndexChanged +=new EventHandler(ddl_SelectedIndexChanged);
			
				if (tablename != "")
				{
					
					if (st == "0")
					{
						if (this.LBL_STA.Text == "01") 
							GlobalTools.fillRefList(ddl[j],"select * from " + tablename,connsme);
						else if (this.LBL_STA.Text == "20")
							GlobalTools.fillRefList(ddl[j],"select * from " + tablename,conncc);
						else if (this.LBL_STA.Text == "40") 
							GlobalTools.fillRefList(ddl[j],"select * from " + tablename,conncons);
					}
					
									
				}
				else
				{
					ddl[j].Items.Add(new ListItem ("-SELECT-",""));
				}
			
				tbl.Rows.Add(new TableRow());
				//pasti 3 cell
				tbl.Rows[row].Cells.Add(new TableCell());
				tbl.Rows[row].Cells.Add(new TableCell());
				tbl.Rows[row].Cells.Add(new TableCell());
				tbl.Rows[row].Cells[0].Controls.Add(lbl);
				tbl.Rows[row].Cells[0].Text		= desc;
				tbl.Rows[row].Cells[0].CssClass = "TDBGColor1";
				tbl.Rows[row].Cells[0].Width    = 200;
				tbl.Rows[row].Cells[1].Width    = 5;
				tbl.Rows[row].Cells[2].Controls.Add(ddl[j]);
				tbl.Rows[row].Cells[2].CssClass = "TDBGColorValue";
				tbl.Rows[row].Cells[2].Width	= 795;

				jk++;
			}
		}

        private void ControlGridPath(string str)
		{
			string query = "SELECT * FROM VW_PATHPROCEDURE";
			if (this.LBL_STA.Text == "40")
			{
				conncons.QueryString = query;
				conncons.ExecuteQuery();
				DataTable dt = new DataTable();
				dt = conncons.GetDataTable().Copy();
				DGR_EXISTING_PATH.DataSource = dt;
				try
				{
					DGR_EXISTING_PATH.DataBind();
				}
				catch
				{
					DGR_EXISTING_PATH.CurrentPageIndex = DGR_EXISTING_PATH.PageCount - 1;
					DGR_EXISTING_PATH.DataBind();
				}
			} 
			else if (this.LBL_STA.Text == "20")
			{
				conncc.QueryString = query;
				conncc.ExecuteQuery();
				DataTable dt = new DataTable();
				dt = conncc.GetDataTable().Copy();
				DGR_EXISTING_PATH.DataSource = dt;
				try
				{
					DGR_EXISTING_PATH.DataBind();
				}
				catch
				{
					DGR_EXISTING_PATH.CurrentPageIndex = DGR_EXISTING_PATH.PageCount - 1;
					DGR_EXISTING_PATH.DataBind();
				}
			}
			try
			{
				DGR_EXISTING_PATH.Items[DGR_EXISTING_PATH.Items.Count -1].Width = 10;
			} 
			catch{}
		}

		private void ControlGridPathPending(string str)
		{
			string query = "select * from VW_PENDING_PATHPROCEDURE";
			if (this.LBL_STA.Text == "40")
			{
				conncons.QueryString = query;
				conncons.ExecuteQuery();
				DataTable dt = new DataTable();
				dt = conncons.GetDataTable().Copy();
				DGR_REQUEST_PATH.DataSource = dt;
				try
				{
					DGR_REQUEST_PATH.DataBind();
				}
				catch
				{
					DGR_REQUEST_PATH.CurrentPageIndex = DGR_REQUEST_PATH.PageCount - 1;
					DGR_REQUEST_PATH.DataBind();
				}
			} 
			else if (this.LBL_STA.Text == "20")
			{
				conncc.QueryString = query;
				conncc.ExecuteQuery();
				DataTable dt = new DataTable();
				dt = conncc.GetDataTable().Copy();
				DGR_REQUEST_PATH.DataSource = dt;
				try
				{
					DGR_REQUEST_PATH.DataBind();
				}
				catch
				{
					DGR_REQUEST_PATH.CurrentPageIndex = DGR_REQUEST_PATH.PageCount - 1;
					DGR_REQUEST_PATH.DataBind();
				}
			}
			//Declare Columns
			//TemplateColumn tcl			= new TemplateColumn();
			//tcl.HeaderStyle.CssClass	= "TDHeader1";
			//tcl.ItemTemplate			= new CreateItemTemplateDDL("LinkButtonZ1","Edit","edit");
			//tcl.ItemTemplate			= new CreateItemTemplateDDL("LinkButtonZ2","Delete","delete");
			
		}

		public class CreateItemTemplateDDL : ITemplate
		{
			string LBTID;
			string strText;
			string strCommandName;
			
			public CreateItemTemplateDDL(string LBTID, 
				string strText, string strCommandName)
			{
				LinkButton lbt	= new LinkButton();
				lbt.ID			= LBTID;
				lbt.CommandName	= strCommandName;
				lbt.Text		= strText;		

			}

			public void InstantiateIn(Control objContainer)
			{
				LinkButton lbt	= new LinkButton();
				lbt.Click +=new EventHandler(lbt_Click);
				objContainer.Controls.Add(lbt);
			}

			private void lbt_Click(object sender, EventArgs e)
			{
				LinkButton lbt	= new LinkButton();
				lbt.ID			= LBTID;
				lbt.CommandName	= strCommandName;
				lbt.Text		= strText;		
			}

		}

		private void ClearBoxes()
		{
			for (int i=0;i<jmlpar;i++)
			{
				ddl[i].SelectedValue = "";
			}
			this.DDL_TRACK_SEQ.SelectedValue = "";
			ActivateControlKey(true);
			this.LBL_SAVEMODE.Text = "1";
		}

		private void ActivateControlKey(bool isEnable)
		{
			for (int i=0;i<jmlpar;i++)
			{
				ddl[i].Enabled = isEnable;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../CommonParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearBoxes();
		}

		private void GetVal(string str)
		{
			int n = 0;
			if (str.Length > 0)
			{
				char[] sep = new char[]{'*'};
				foreach (string ss in str.Split(sep))
				{
					strtemp[n]= ss;
					n++;	
				}
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string track_seq = this.DDL_TRACK_SEQ.SelectedValue;
			string query	= "";
			string query2	= "";
					
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				for (int i=0;i<Convert.ToInt16(this.LBL_JMLPAR.Text.Trim());i++)
				{
					string val = "";
					val = ddl[i].SelectedValue;

					if (i == 0)
						query += parameter[i] + " = '" +  val + "' ";
					else
						query += " and " + parameter[i] + " = '" +  val + "' ";
				}
				query = "select * from PATHPROCEDURE where " + query;
				if (this.LBL_STA.Text == "20")
				{
					conncc.QueryString = query;
					conncc.ExecuteQuery();
					exist = conncc.GetRowCount();
				} 
				else if (this.LBL_STA.Text == "40")
				{
					conncons.QueryString = query;
					conncons.ExecuteQuery();
					exist = conncons.GetRowCount();
				} 
			
				if (exist > 0) 
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		
					
			query2 += "," + track_seq + "";
			//Response.Write("jmlpar: " + jmlpar);

			for (int i=0;i<jmlpar;i++)
			{
				string val = "";
				val = ddl[i].SelectedValue;

				//Response.Write("val[" + i + "] : " + val);

				query2 += ",'" + val + "'";
			}
			query2 = "exec PARAM_GENERAL_PATHPROCEDURE_MAKER '" + this.LBL_SAVEMODE.Text + "'" + query2;
			if (this.LBL_STA.Text == "20")
			{
				conncc.QueryString = query2;
				conncc.ExecuteQuery();
			} 
			else if (this.LBL_STA.Text == "40")
			{
				conncons.QueryString = query2;
				conncons.ExecuteQuery();
			}
			
			this.LBL_SAVEMODE.Text = "1";
			ViewPendingData();
			ClearBoxes();		
		}

		protected void RBL_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.LBL_STA.Text		= this.RBL_MODULE.SelectedValue;
			if (this.LBL_STA.Text == "40")
				Response.Redirect("ProcedurePathAll.aspx?moduleID=40");
			if (this.LBL_STA.Text == "20")
				Response.Redirect("ProcedurePathAll.aspx?moduleID=20");
			//ControlDDLSEQ(true);
			//SetInterface();
		}

		private void DGR_EXISTING_PATH_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING_PATH.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_EXISTING_PATH_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					ControlDDLSEQ(true);
					for (int i=0;i<jmlpar;i++)
					{
						ddl[i].SelectedValue	= CleansText(e.Item.Cells[2*i+1].Text);
					}
					this.DDL_TRACK_SEQ.SelectedValue = CleansText(e.Item.Cells[2*jmlpar+1].Text);					
					ActivateControlKey(false);
					break;
				case "delete":
					string query = "";
					query += "," + CleansText(e.Item.Cells[2*jmlpar+1].Text) + "";//track seq 
					for (int i=0;i<jmlpar;i++)
					{
						query += ",'" + CleansText(e.Item.Cells[2*i+1].Text) + "'";
					}
					if (this.LBL_STA.Text == "20")
					{
						conncc.QueryString = "exec PARAM_GENERAL_PATHPROCEDURE_MAKER '2'" + query;
						conncc.ExecuteQuery();
					} 
					else if (this.LBL_STA.Text == "40")
					{
						conncons.QueryString = "exec PARAM_GENERAL_PATHPROCEDURE_MAKER '2'" + query;
						conncons.ExecuteQuery();
					}
					ViewPendingData();
					break;
				default :
					break;
			}
		}

		private void DGR_REQUEST_PATH_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST_PATH.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_REQUEST_PATH_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearBoxes();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					this.LBL_SAVEMODE.Text = GetBackPendingStatus(CleansText(e.Item.Cells[2*jmlpar+2].Text).ToLower());
					ControlDDLSEQ(true);
					if (this.LBL_SAVEMODE.Text.Trim() == "2")
					{
						this.LBL_SAVEMODE.Text = "1";
						break;
					}
					for (int i=0;i<jmlpar;i++)
					{
						ddl[i].SelectedValue  = CleansText(e.Item.Cells[2*i+1].Text);
					}
					this.DDL_TRACK_SEQ.SelectedValue = CleansText(e.Item.Cells[2*jmlpar+1].Text);
					ActivateControlKey(false);
					break;
				case "delete":
					string query = "";
					for (int i=0;i<jmlpar;i++)
					{
						query += parameter[i] + " = '" +  CleansText(e.Item.Cells[2*i+1].Text) + "' and ";
					}
					query += " TRACK_SEQ = " + CleansText(e.Item.Cells[2*jmlpar+1].Text); 
					if (this.LBL_STA.Text == "20")
					{
						conncc.QueryString = "delete from PENDING_PATHPROCEDURE where " + query;
						conncc.ExecuteNonQuery();
					} 
					else if (this.LBL_STA.Text == "40")
					{
						conncons.QueryString = "delete from PENDING_PATHPROCEDURE where " + query;
						conncons.ExecuteNonQuery();
					}
					ViewPendingData();
					break;
				default :
					break;
			}
		}

		private void ddl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int j = Convert.ToInt16(this.LBL_NUM.Text.Trim());
			this.LBL_NILTEMP.Text	+= ddl[j].SelectedValue + "*";
		}
	}
}
