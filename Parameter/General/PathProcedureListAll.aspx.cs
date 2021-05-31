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
	/// <summary>
	/// Summary description for PathProcedureListAll.
	/// </summary>
	public partial class PathProcedureListAll : System.Web.UI.Page
	{
		protected Connection conn;// = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conncc;
		protected Connection conncons;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn();
			if (!IsPostBack)
			{
				string module = Request.QueryString["moduleID"];
				if (module == "" || module == null)
				{
					this.LBL_STA.Text = "01";
				} 
				else
					this.LBL_STA.Text = module;
				try
				{
					this.RBL_MODULE.SelectedValue = this.LBL_STA.Text.Trim();
				} 
				catch {}
				FillDDL();
				ViewExistingData();
				ViewPendingData();
			}
			ControlInterface();
			/*if update and track="" ... tidak perlu cek mandatory
			if	(this.LBL_SAVEMODE.Text == "0" && this.DDL_TRACK_FROM.SelectedValue == "")
				BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return true;}");			
			else
				BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			*/
			this.DGR_REQUEST.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_REQUEST_PageIndexChanged);
			this.DGR_EXISTING.PageIndexChanged +=new DataGridPageChangedEventHandler(DGR_EXISTING_PageIndexChanged);
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

		private void SetDBConn()
		{
			string DB_NAMA;
			string DB_IP;
			string DB_LOGINID;
			string DB_LOGINPWD;
			//Credit Card Conn
			conn.QueryString = "select * from VW_GETCONN where MODULEID='20'";
			conn.ExecuteQuery();
			DB_NAMA = conn.GetFieldValue("DB_NAMA");
			DB_IP = conn.GetFieldValue("DB_IP");
			DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncc = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
			//Consumer
			conn.QueryString = "select * from VW_GETCONN where MODULEID='40'";
			conn.ExecuteQuery();
			DB_NAMA = conn.GetFieldValue("DB_NAMA");
			DB_IP = conn.GetFieldValue("DB_IP");
			DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conncons = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void ControlInterface()
		{
			if (this.LBL_STA.Text == "01")
			{
				Response.Redirect("../Area/SME/RFProcedurePath.aspx");
			}
		}

		private void ClearControls()
		{
			this.DDL_TRACK_SEQ.SelectedValue	= "";
			this.DDL_TRACK_BACK.SelectedValue	= "";
			this.DDL_TRACK_FAIL.SelectedValue	= "";
			this.DDL_TRACK_FROM.SelectedValue	= "";
			this.DDL_TRACK_NEXT.SelectedValue	= "";
			this.activateControlKey(true);
		}

		private void FillDDL()
		{
			if (this.LBL_STA.Text == "20")
			{
				GlobalTools.fillRefList(this.DDL_TRACK_SEQ,"SELECT * FROM VW_RFTRACKSEQ",conncc);
				GlobalTools.fillRefList(this.DDL_TRACK_SEQ_FILTER,"SELECT * FROM VW_RFTRACKSEQ",conncc);
				GlobalTools.fillRefList(this.DDL_TRACK_BACK,"SELECT * FROM VW_PARAM_RFPROCEDURE_TRACK",conncc);
				GlobalTools.fillRefList(this.DDL_TRACK_FAIL,"SELECT * FROM VW_PARAM_RFPROCEDURE_TRACK",conncc);
				GlobalTools.fillRefList(this.DDL_TRACK_FROM,"SELECT * FROM VW_PARAM_RFPROCEDURE_TRACK",conncc);
				GlobalTools.fillRefList(this.DDL_TRACK_NEXT,"SELECT * FROM VW_PARAM_RFPROCEDURE_TRACK",conncc);
			} 
			else if (this.LBL_STA.Text == "40")
			{
				GlobalTools.fillRefList(this.DDL_TRACK_SEQ,"SELECT * FROM VW_RFTRACKSEQ",conncons);
				GlobalTools.fillRefList(this.DDL_TRACK_SEQ_FILTER,"SELECT * FROM VW_RFTRACKSEQ",conncons);
				GlobalTools.fillRefList(this.DDL_TRACK_BACK,"SELECT * FROM VW_PARAM_RFPROCEDURE_TRACK",conncons);
				GlobalTools.fillRefList(this.DDL_TRACK_FAIL,"SELECT * FROM VW_PARAM_RFPROCEDURE_TRACK",conncons);
				GlobalTools.fillRefList(this.DDL_TRACK_FROM,"SELECT * FROM VW_PARAM_RFPROCEDURE_TRACK",conncons);
				GlobalTools.fillRefList(this.DDL_TRACK_NEXT,"SELECT * FROM VW_PARAM_RFPROCEDURE_TRACK",conncons);
			}
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

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void activateControlKey(bool isReadOnly) 
		{
			this.DDL_TRACK_SEQ.Enabled	= isReadOnly;
			this.DDL_TRACK_FROM.Enabled = isReadOnly;
		}

		private void ViewExistingData()
		{
			DataTable dt = new DataTable();
			if (this.LBL_STA.Text == "20")
			{
				if (this.DDL_TRACK_SEQ_FILTER.SelectedValue == "")
					conncc.QueryString = "select * from VW_PARAM_RFPROCEDURELIST";
				else
					conncc.QueryString = "select * from VW_PARAM_RFPROCEDURELIST where TRACK_SEQ ='" + this.DDL_TRACK_SEQ_FILTER.SelectedValue + "'";
				conncc.ExecuteQuery();
				dt = conncc.GetDataTable().Copy();
			} 
			else if (this.LBL_STA.Text == "40")
			{
				if (this.DDL_TRACK_SEQ_FILTER.SelectedValue == "")
					conncons.QueryString = "select * from VW_PARAM_RFPROCEDURELIST";
				else
					conncons.QueryString = "select * from VW_PARAM_RFPROCEDURELIST where TRACK_SEQ ='" + this.DDL_TRACK_SEQ_FILTER.SelectedValue + "'";
				conncons.ExecuteQuery();
				dt = conncons.GetDataTable().Copy();
			}

			DGR_EXISTING.DataSource = dt;
			try
			{
				DGR_EXISTING.DataBind();
			}
			catch
			{
				DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.PageCount - 1;
				DGR_EXISTING.DataBind();
			}
		}

		private void ViewPendingData()
		{
			DataTable dt = new DataTable();
			if (this.LBL_STA.Text == "20")
			{
				conncc.QueryString = "select * from VW_PARAM_PENDING_RFPROCEDURELIST";
				conncc.ExecuteQuery();
				dt = conncc.GetDataTable().Copy();
			} 
			else if (this.LBL_STA.Text == "40")
			{
				conncons.QueryString = "select * from VW_PARAM_PENDING_RFPROCEDURELIST";
				conncons.ExecuteQuery();
				dt = conncons.GetDataTable().Copy();
			} 

			this.DGR_REQUEST.DataSource = dt;
			try
			{
				this.DGR_REQUEST.DataBind();
			}
			catch
			{
				DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				DGR_REQUEST.DataBind();
			}
			for (int i=0; i< this.DGR_REQUEST.Items.Count; i++)
			{
				this.DGR_REQUEST.Items[i].Cells[11].Text = this.getPendingStatus(this.DGR_REQUEST.Items[i].Cells[11].Text);
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../CommonParam.aspx?ModuleID="+Request.QueryString["ModuleID"]);
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			/*********Mandatory Validation*******/
			if (this.LBL_STA.Text == "40")
			{
				if (this.DDL_TRACK_SEQ.SelectedValue == "")
				{
					GlobalTools.popMessage(this,"Track tidak boleh kosong!");
					GlobalTools.SetFocus(this,DDL_TRACK_SEQ);
					return;
				}
				if (this.DDL_TRACK_FROM.SelectedValue == "")
				{
					GlobalTools.popMessage(this,"Current Track tidak boleh kosong!");
					GlobalTools.SetFocus(this,DDL_TRACK_FROM);
					return;
				}
			}
			else if (this.LBL_STA.Text == "20")
			{
				if (this.DDL_TRACK_SEQ.SelectedValue == "")
				{
					GlobalTools.popMessage(this,"Track tidak boleh kosong!");
					GlobalTools.SetFocus(this,DDL_TRACK_SEQ);
					return;
				}
				if (this.LBL_SAVEMODE.Text != "0")
					if (this.DDL_TRACK_FROM.SelectedValue == "")
					{
						GlobalTools.popMessage(this,"Current Track tidak boleh kosong!");
						GlobalTools.SetFocus(this,DDL_TRACK_FROM);
						return;
					}
			} 
			/************************************/
			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				int jml = 0;
				if (this.LBL_STA.Text == "20")
				{
					conncc.QueryString = "select * from VW_PARAM_RFPROCEDURELIST where TRACK_SEQ ="+ this.DDL_TRACK_SEQ.SelectedValue +" and " +
						"TRACK_FROM = '" + this.DDL_TRACK_FROM.SelectedValue + "'";
					conncc.ExecuteQuery();
					jml = conncc.GetRowCount();
				} 
				else if (this.LBL_STA.Text == "40")
				{
					conncons.QueryString = "select * from VW_PARAM_RFPROCEDURELIST where TRACK_SEQ ="+ this.DDL_TRACK_SEQ.SelectedValue +" and " +
						"TRACK_FROM = '" + this.DDL_TRACK_FROM.SelectedValue + "'";
					conncons.ExecuteQuery();
					jml = conncons.GetRowCount();
				}
				
				if (jml > 0) 
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					return;
				}
			}		
			if (this.LBL_STA.Text == "20")
			{
				conncc.QueryString = "exec PARAM_GENERAL_CC_RFPROCEDURE_MAKER   '"+this.LBL_SAVEMODE.Text +"'," +
					this.DDL_TRACK_SEQ.SelectedValue + ",'" + this.DDL_TRACK_FROM.SelectedValue + "','" +
					this.DDL_TRACK_NEXT.SelectedValue +"','" + this.DDL_TRACK_BACK.SelectedValue + "','" +
					this.DDL_TRACK_FAIL.SelectedValue + "'";
				conncc.ExecuteNonQuery();
			} 
			else if (this.LBL_STA.Text == "40")
			{
				conncons.QueryString = "exec PARAM_GENERAL_RFPROCEDURE_MAKER   '"+this.LBL_SAVEMODE.Text +"'," +
					this.DDL_TRACK_SEQ.SelectedValue + ",'" + this.DDL_TRACK_FROM.SelectedValue + "','" +
					this.DDL_TRACK_NEXT.SelectedValue +"','" + this.DDL_TRACK_BACK.SelectedValue + "','" +
					this.DDL_TRACK_FAIL.SelectedValue + "'";
				conncons.ExecuteQuery();
			}
			this.LBL_SAVEMODE.Text = "1";
			ViewPendingData();
			ClearControls();
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearControls();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					FillDDL();
					LBL_SAVEMODE.Text = "0";
					try {this.DDL_TRACK_SEQ.SelectedValue		= CleansText(e.Item.Cells[0].Text);} 
					catch{}
					try {this.DDL_TRACK_FROM.SelectedValue	= CleansText(e.Item.Cells[2].Text);} 
					catch{}
					try{this.DDL_TRACK_NEXT.SelectedValue	= CleansText(e.Item.Cells[4].Text);} 
					catch{}
					try{this.DDL_TRACK_BACK.SelectedValue	= CleansText(e.Item.Cells[6].Text);} 
					catch{}
					try{this.DDL_TRACK_FAIL.SelectedValue	= CleansText(e.Item.Cells[8].Text);} 
					catch{}
					activateControlKey(false);
					break;

				case "delete":
					if (this.LBL_STA.Text == "20")
					{
						conncc.QueryString = "exec PARAM_GENERAL_CC_RFPROCEDURE_MAKER  '2'," +
							CleansText(e.Item.Cells[0].Text) + ",'" + CleansText(e.Item.Cells[2].Text) + "','" +
							CleansText(e.Item.Cells[4].Text) + "','" + CleansText(e.Item.Cells[6].Text) + "','" + 
							CleansText(e.Item.Cells[8].Text) + "'";
						conncc.ExecuteQuery();
					} 
					else if (this.LBL_STA.Text == "40")
					{
						conncons.QueryString = "exec PARAM_GENERAL_RFPROCEDURE_MAKER  '2'," +
							CleansText(e.Item.Cells[0].Text) + ",'" + CleansText(e.Item.Cells[2].Text) + "','" +
							CleansText(e.Item.Cells[4].Text) + "','" + CleansText(e.Item.Cells[6].Text) + "','" + 
							CleansText(e.Item.Cells[8].Text) + "'";
						conncons.ExecuteQuery();
					}
					ViewPendingData();
					break;

				default :
					break;
			}
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					FillDDL();
					LBL_SAVEMODE.Text = CleansText(e.Item.Cells[10].Text);
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
							LBL_SAVEMODE.Text = "1";
						break;
					}
					try {this.DDL_TRACK_SEQ.SelectedValue		= CleansText(e.Item.Cells[0].Text);} 
					catch{}
					try {this.DDL_TRACK_FROM.SelectedValue	= CleansText(e.Item.Cells[2].Text);} 
					catch{}
					try {this.DDL_TRACK_NEXT.SelectedValue	= CleansText(e.Item.Cells[4].Text);} 
					catch{}
					try	{this.DDL_TRACK_BACK.SelectedValue	= CleansText(e.Item.Cells[6].Text);} 
					catch{}
					try {this.DDL_TRACK_FAIL.SelectedValue	= CleansText(e.Item.Cells[8].Text);} 
					catch{}
					activateControlKey(false);
					break;

				case "delete":
					if (this.LBL_STA.Text == "20")
					{
						conncc.QueryString = " delete from PENDING_CC_RFPROCEDURE where TRACK_SEQ ="+
							CleansText(e.Item.Cells[0].Text) + " and TRACK_FROM ='" + CleansText(e.Item.Cells[2].Text) + "'";
						conncc.ExecuteNonQuery();
					}
					else if (this.LBL_STA.Text == "40")
					{
						conncons.QueryString = " delete from PENDING_RFPROCEDURE where TRACK_SEQ ="+
							CleansText(e.Item.Cells[0].Text) + " and TRACK_FROM ='" + CleansText(e.Item.Cells[2].Text) + "'";
						conncons.ExecuteNonQuery();
					}
					ViewPendingData();
					break;
				default :
					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		protected void RBL_MODULE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.LBL_STA.Text = this.RBL_MODULE.SelectedValue;
			ControlInterface();
			ViewExistingData();
			ViewPendingData();
			FillDDL();
		}

		protected void DDL_TRACK_SEQ_FILTER_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
		}
	}
}

