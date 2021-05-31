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

namespace CuBES_Maintenance.Parameter.Scoring.Consumer
{
	/// <summary>
	/// Summary description for CUBESSimModel.
	/// </summary>
	public partial class CUBESSimModel : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				fillprogramddl();
				fillproductddl();
				filljobtypeddl();
				filltemplateddl();
				ViewExistingData();
				ViewPendingData();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "SELECT * FROM RFMODULE WHERE MODULEID = '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		public void fillprogramddl()
		{
			GlobalTools.fillRefList(DDL_PROGRAM, "SELECT PR_CODE, PR_DESC FROM PROGRAM ", conn);
		}

		public void fillproductddl()
		{
			GlobalTools.fillRefList(DDL_PRODUCT, "SELECT PRODUCTID, PRODUCTNAME FROM TPRODUCT ", conn);
		}

		public void filljobtypeddl()
		{
			GlobalTools.fillRefList(DDL_JOBTYPE, "SELECT JOB_TYPE_ID, DES FROM JOB_TYPE ", conn);
		}

		public void filltemplateddl()
		{
			GlobalTools.fillRefList(DDL_TPLID, "SELECT SIMTPLID, SIMTPLDESC FROM MANDIRI_SIM_LIMIT_TEMPLATE ", conn);
		}

		public void ViewExistingData()
		{
			conn.QueryString = "select * from VW_PARAM_SIM_MODEL_MAKER_EXSISTING where 1=1 ";
			
			if (this.DDL_PROGRAM.SelectedValue != "")
				conn.QueryString += "and PROGRAMID = '" + this.DDL_PROGRAM.SelectedValue + "' ";
			if (this.DDL_PRODUCT.SelectedValue != "")
				conn.QueryString += "and PRODUCTID = '" + this.DDL_PRODUCT.SelectedValue + "' ";
			if (this.DDL_JOBTYPE.SelectedValue != "")
				conn.QueryString += "and JOBTYPEID = '" + this.DDL_JOBTYPE.SelectedValue + "' ";
			if (this.DDL_TPLID.SelectedValue != "")
				conn.QueryString += "and TEMPLATEID = '" + this.DDL_TPLID.SelectedValue + "' ";

			conn.ExecuteQuery();
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_EXISTING.DataSource = data;
			try
			{
				this.DGR_EXISTING.DataBind();
			} 
			catch 
			{
				
				this.DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.PageCount - 1;
				this.DGR_EXISTING.DataBind();
			}
		}

		public void ViewPendingData()
		{
			conn.QueryString = "select * from VW_PARAM_SIM_MODEL_MAKER_PENDING where 1=1 ";
			
			if (this.DDL_PROGRAM.SelectedValue != "")
				conn.QueryString += "and PROGRAMID = '" + this.DDL_PROGRAM.SelectedValue + "' ";
			if (this.DDL_PRODUCT.SelectedValue != "")
				conn.QueryString += "and PRODUCTID = '" + this.DDL_PRODUCT.SelectedValue + "' ";
			if (this.DDL_JOBTYPE.SelectedValue != "")
				conn.QueryString += "and JOBTYPEID = '" + this.DDL_JOBTYPE.SelectedValue + "' ";
			if (this.DDL_TPLID.SelectedValue != "")
				conn.QueryString += "and TEMPLATEID = '" + this.DDL_TPLID.SelectedValue + "' ";

			conn.ExecuteQuery();
			DataTable data = new DataTable();
			data = conn.GetDataTable().Copy();
			DGR_REQUEST.DataSource = data;
			try
			{
				this.DGR_REQUEST.DataBind();
			} 
			catch 
			{
				
				this.DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				this.DGR_REQUEST.DataBind();
			}
		}

		private void clearEditBoxes()
		{
			try {this.DDL_PROGRAM.SelectedItem.Selected = false;} 
			catch {}
			try {this.DDL_PRODUCT.SelectedItem.Selected = false;} 
			catch {}
			try {this.DDL_JOBTYPE.SelectedItem.Selected = false;} 
			catch {}
			try {this.DDL_TPLID.SelectedItem.Selected = false;} 
			catch {}

			DDL_PROGRAM.Enabled = true;
			DDL_PRODUCT.Enabled = true;
			DDL_JOBTYPE.Enabled = true;
			
			this.LBL_SAVEMODE.Text = "1"; 
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

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			conn.QueryString = "EXEC PARAM_SIM_MODEL_MAKER '"+DDL_PROGRAM.SelectedValue+"', '"+
				DDL_PRODUCT.SelectedValue+"', '"+
				DDL_JOBTYPE.SelectedValue+"', '"+
				DDL_TPLID.SelectedValue+"', '"+
				LBL_SAVEMODE.Text+"'";

			try
			{
				conn.ExecuteNonQuery();
			} 
			catch (Exception ex)
			{
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)		//method Connection.ExecuteNonQuery() add this msg on exception 
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this, errmsg);
			}

			clearEditBoxes();
			ViewExistingData();
			ViewPendingData();
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
			ViewExistingData();
			ViewPendingData();
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			ViewExistingData();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":
					try {this.DDL_PROGRAM.SelectedValue = cleansText(e.Item.Cells[0].Text);}
					catch {}
					try {this.DDL_PRODUCT.SelectedValue = cleansText(e.Item.Cells[2].Text);}
					catch {}
					try {this.DDL_JOBTYPE.SelectedValue = cleansText(e.Item.Cells[4].Text);}
					catch {}
					try {this.DDL_TPLID.SelectedValue = cleansText(e.Item.Cells[6].Text);}
					catch {}

					DDL_PROGRAM.Enabled = false;
					DDL_PRODUCT.Enabled = false;
					DDL_JOBTYPE.Enabled = false;

					LBL_SAVEMODE.Text = "0";
										
					break;
				case "Delete":
					conn.QueryString="SELECT * FROM TMANDIRI_SIM_LIMIT_MODEL WHERE PR_CODE='"+cleansText(e.Item.Cells[0].Text)+"'"+
						" AND PRODUCTID='"+cleansText(e.Item.Cells[2].Text)+"'"+
						" AND JOB_TYPE_ID='"+cleansText(e.Item.Cells[4].Text)+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="UPDATE TMANDIRI_SIM_LIMIT_MODEL SET CH_STA='2' WHERE PR_CODE='"+cleansText(e.Item.Cells[0].Text)+"'"+
							" AND PRODUCTID='"+cleansText(e.Item.Cells[2].Text)+"'"+
							" AND JOB_TYPE_ID='"+cleansText(e.Item.Cells[4].Text)+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_SIM_MODEL_MAKER '"+cleansText(e.Item.Cells[0].Text)+"', '"+
							cleansText(e.Item.Cells[2].Text)+"', '"+
							cleansText(e.Item.Cells[4].Text)+"', '"+
							cleansText(e.Item.Cells[6].Text)+"', '2'";
						conn.ExecuteQuery();
					}
					ViewPendingData();
					clearEditBoxes();
					break;
				default:
					
					break;
			}
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			ViewPendingData();
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "Edit":

					conn.QueryString="SELECT * FROM TMANDIRI_SIM_LIMIT_MODEL WHERE PR_CODE='"+cleansText(e.Item.Cells[0].Text)+"'"+
						" AND PRODUCTID='"+cleansText(e.Item.Cells[2].Text)+"'"+
						" AND JOB_TYPE_ID='"+cleansText(e.Item.Cells[4].Text)+"'";
					conn.ExecuteQuery();

					string status = e.Item.Cells[8].Text;
					if(status=="DELETE")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						try {this.DDL_PROGRAM.SelectedValue = cleansText(e.Item.Cells[0].Text);}
						catch {}
						try {this.DDL_PRODUCT.SelectedValue = cleansText(e.Item.Cells[2].Text);}
						catch {}
						try {this.DDL_JOBTYPE.SelectedValue = cleansText(e.Item.Cells[4].Text);}
						catch {}
						try {this.DDL_TPLID.SelectedValue = cleansText(e.Item.Cells[6].Text);}
						catch {}

						DDL_PROGRAM.Enabled = false;
						DDL_PRODUCT.Enabled = false;
						DDL_JOBTYPE.Enabled = false;
						
						if(e.Item.Cells[8].Text=="UPDATE")
						{
							LBL_SAVEMODE.Text = "0";
						}
						if(e.Item.Cells[8].Text=="INSERT")
						{
							LBL_SAVEMODE.Text = "1";
						}
					}
					break;
				case "Delete":
					conn.QueryString = "DELETE FROM TMANDIRI_SIM_LIMIT_MODEL WHERE PR_CODE='"+cleansText(e.Item.Cells[0].Text)+"'"+
						" AND PRODUCTID='"+cleansText(e.Item.Cells[2].Text)+"'"+
						" AND JOB_TYPE_ID='"+cleansText(e.Item.Cells[4].Text)+"'";

					conn.ExecuteQuery();
					ViewPendingData();
					clearEditBoxes();
					break;
				default:
					break;			
			}
		}

		protected void DDL_PROGRAM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
			ViewPendingData();
		}

		protected void DDL_PRODUCT_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
			ViewPendingData();
		}

		protected void DDL_JOBTYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
			ViewPendingData();
		}

		protected void DDL_TPLID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ViewExistingData();
			ViewPendingData();
		}

	}
}
