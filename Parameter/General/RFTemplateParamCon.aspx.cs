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
	/// Summary description for RFTemplateParamCon.
	/// </summary>
	public partial class RFTemplateParamCon : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder Menu;
		protected System.Web.UI.WebControls.Button updatestatus;
		protected Tools tool = new Tools();
		protected Connection conn;// = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
		protected Connection conn2;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack) 
			{
				//LBL_PARAMNAME.Text = Request.QueryString["tablename"];
				LBL_PRM_NM.Text = Request.QueryString["name"];
				LBL_PARAMNAME.Text = Request.QueryString["name"];
				LBL_SAVEMODE.Text = "1";
				LBL_ACTIVE.Text = Request.QueryString["active"];
				LBL_AUTO.Text = "2";
				LBL_AUTO.Text = Request.QueryString["digits"];
 
				if (LBL_ACTIVE.Text.Trim() != "0")
					LBL_ACTIVE.Text = "1";	//default condition

				//set fields
				conn2.QueryString = "select [name] from SYSCOLUMNS " + 
					"where ID in " + 
					"(select ID from SYSOBJECTS " + 
					"where NAME = '" + Request.QueryString["tablename"] + "') order by colid";
				conn2.ExecuteQuery();

				try
				{
					LBL_ID.Text = conn2.GetFieldValue(0,0);
					LBL_DESC.Text = conn2.GetFieldValue(1,0);
					TR_CD_SIBS.Visible = false;
					if (Request.QueryString["cd_sibs"] == "1")
						for (int i = 2; i < conn2.GetRowCount(); i++)
							if (conn2.GetFieldValue(i,0) == "CD_SIBS")
							{
								TR_CD_SIBS.Visible = true;
								DGExisting.Columns[2].Visible = true;
								DGRequest.Columns[4].Visible = true;
							}
				}
				catch {}

				viewExistingData();
				viewPendingData();
				clearControls();
				setDescription();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");			
			DGExisting.PageIndexChanged +=new DataGridPageChangedEventHandler(DGExisting_PageIndexChanged);
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
			this.DGExisting.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGExisting_ItemCommand);
			this.DGExisting.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGExisting_PageIndexChanged);
			this.DGRequest.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.DGRequest.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGRequest_PageIndexChanged);

		}
		#endregion
		
		private void SetDBConn2()
		{
			string DB_NAMA;
			string DB_IP;
			string DB_LOGINID;
			string DB_LOGINPWD;
			conn.QueryString = "select DB_NAMA, DB_IP, DB_LOGINID, DB_LOGINPWD from VW_GETCONN where MODULEID='" + Request.QueryString["MODULEID"]+ "'";
			conn.ExecuteQuery();
			DB_NAMA = conn.GetFieldValue("DB_NAMA");
			DB_IP = conn.GetFieldValue("DB_IP");
			DB_LOGINID = conn.GetFieldValue("DB_LOGINID");
			DB_LOGINPWD = conn.GetFieldValue("DB_LOGINPWD");
			conn2 = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void setDescription() 
		{
			switch (Request.QueryString["tablename"]) 
			{
				case "RFTBODOC":
					TXT_DESC.TextMode = TextBoxMode.MultiLine;
					break;				
				default:
					TXT_DESC.TextMode = TextBoxMode.SingleLine;
					break;
			}
		}

		private void viewPendingData() 
		{
			string tableName = Request.QueryString["tablename"];

			conn2.QueryString = "select * from T" + tableName;
			conn2.ExecuteQuery();

			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("PENDING_STATUS"));
			dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
			dt.Columns.Add(new DataColumn("CD_SIBS"));

			DataRow dr;
			for(int i = 0; i < conn2.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn2.GetFieldValue(i,0);
				dr[1] = conn2.GetFieldValue(i,1);
				dr[2] = conn2.GetFieldValue(i,"CH_STA");
				dr[3] = getPendingStatus(conn2.GetFieldValue(i,"CH_STA"));
				dr[4] = hasCD_SIBS() ? conn2.GetFieldValue(i,"CD_SIBS") : "";
				dt.Rows.Add(dr);
			}			

			DGRequest.DataSource = new DataView(dt);
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

		private void viewExistingData() 
		{
			string tableName = Request.QueryString["tablename"];

			if (LBL_ACTIVE.Text.Trim() == "1")
				conn2.QueryString = "select * from " + tableName + " where ACTIVE = '1'";
			else
				conn2.QueryString = "select * from " + tableName ;
			conn2.ExecuteQuery();

			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("ID"));
			dt.Columns.Add(new DataColumn("DESC"));
			dt.Columns.Add(new DataColumn("CD_SIBS"));

			DataRow dr;
			for(int i = 0; i < conn2.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn2.GetFieldValue(i,0);
				dr[1] = conn2.GetFieldValue(i,1);
				dr[2] = hasCD_SIBS() ? conn2.GetFieldValue(i,"CD_SIBS") : "";
				dt.Rows.Add(dr);
			}			

			DGExisting.DataSource = new DataView(dt);
			try 
			{
				DGExisting.DataBind();
			} 
			catch 
			{
				DGExisting.CurrentPageIndex = DGExisting.PageCount - 1;
				DGExisting.DataBind();
			}
		}

		private void CodeSeq_old(string mode)
		{
			string seq = "", bseq = "";
			int dg = Int16.Parse(LBL_AUTO.Text); 
			int number = Int16.Parse(LBL_NB.Text);

			if(mode == "1")
			{
				number++;
			}

			LBL_NB.Text = number.ToString();

			try
			{
				conn2.QueryString = "select max(convert(int,isnull("+LBL_ID.Text.Trim()+",0)))+ "+LBL_NB.Text+" as MAXSEQ from "+Request.QueryString["tablename"]+" ";
				conn2.ExecuteQuery();

				seq = conn2.GetFieldValue("MAXSEQ").Trim();

				for(int i = seq.Length; i < dg; i++)
				{
					bseq = "0" +bseq; 
				}

				TXT_ID.Text = bseq+""+seq;
			}
			catch
			{
				GlobalTools.popMessage(this,"Error, this parameter have invalid id!"); 
				return;
			}
		}

		private void CodeSeq()
		{
			string seq = "", bseq = "";
			int dg = Int16.Parse(LBL_AUTO.Text);
			try
			{
				conn2.QueryString = "select (select isnull(max("+LBL_ID.Text.Trim()+"),0) + 1 from "+Request.QueryString["tablename"]+") a, " +
					"(select isnull(max("+LBL_ID.Text.Trim()+"),0) + 1 from T"+Request.QueryString["tablename"]+") b";
				conn2.ExecuteQuery();

				seq = conn2.GetFieldValue("a").Trim();
				if (int.Parse(conn2.GetFieldValue("a").Trim()) < int.Parse(conn2.GetFieldValue("b").Trim()))
					seq = conn2.GetFieldValue("b").Trim();

				for(int i = seq.Length; i < dg; i++)
				{
					bseq = "0" +bseq; 
				}

				TXT_ID.Text = bseq+""+seq;
			}
			catch
			{
				GlobalTools.popMessage(this,"Error, this parameter have invalid id!"); 
				return;
			}
		}

		private void DGExisting_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{			
			DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			if (TXT_ID.Text.Trim() == "" || TXT_DESC.Text.Trim() == "") return;

			if (LBL_SAVEMODE.Text.Trim() == "1") 
			{
				//conn2.QueryString = "select * from " + Request.QueryString["tablename"] + " WHERE ACTIVE='1' and " + getColumnKey() + "='" + TXT_ID.Text.Trim() + "'";
				conn2.QueryString = "select * from " + Request.QueryString["tablename"] + " WHERE " + getColumnKey() + "='" + TXT_ID.Text.Trim() + "'";
				conn2.ExecuteQuery();
				int jmlexisting = conn2.GetRowCount();

				if (jmlexisting > 0) 
				{
					GlobalTools.popMessage(this, "ID has already been used! Request canceled!");
					GlobalTools.SetFocus(this,TXT_ID);
					return;
				}
			}		
			
			executeMaker(TXT_ID.Text.Trim(), TXT_DESC.Text.Trim(), TXT_CD_SIBS.Text.Trim(), LBL_SAVEMODE.Text.Trim());
			viewPendingData();
			clearControls();

			LBL_SAVEMODE.Text = "1";
		}

		private void clearControls() 
		{
			if(LBL_AUTO.Text.Trim() == "0")
				TXT_ID.Text   = "";
			else
				CodeSeq();

			TXT_DESC.Text = "";
			TXT_CD_SIBS.Text = "";
			activateControlKey(true);
			LBL_SAVEMODE.Text = "1";
		}

		private void activateControlKey(bool mode) 
		{
			//TXT_ID.ReadOnly = LBL_AUTO.Text.Trim() != "0";
		}

		private string cleanText(string str)
		{
			if (str.Trim() == "&nbsp;")
				str = "";
			return str;
		}

		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			clearControls();
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[2].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[1].Text;
					TXT_CD_SIBS.Text = cleanText(e.Item.Cells[4].Text);
					activateControlKey(false);
					break;

				case "delete":
					string id = e.Item.Cells[0].Text;
					string colKey = "";

					colKey = getColumnKey();
					conn2.QueryString = "delete from T" + Request.QueryString["tablename"] + " WHERE " + colKey + "='" + id + "'";
					conn2.ExecuteQuery();
					viewPendingData();
					break;
				default :
					break;
			}
		}

		private string getColumnKey() 
		{
			return LBL_ID.Text.Trim();
		}

		private string getColumnDesc()
		{
			return LBL_DESC.Text.Trim();
		}

		private bool hasCD_SIBS()
		{
			return TR_CD_SIBS.Visible;
		}

		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";
					
					TXT_ID.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = e.Item.Cells[1].Text;
					TXT_CD_SIBS.Text = cleanText(e.Item.Cells[2].Text);
					activateControlKey(false);
					break;

				case "delete":
					string id	= e.Item.Cells[0].Text.Trim();
					string desc = e.Item.Cells[1].Text.Trim();
					string cdsibs = e.Item.Cells[2].Text.Trim();
					executeMaker(id,desc,cdsibs,"2");
					viewPendingData();
					break;

				default :
					break;
			}
		}

		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		private void executeMaker(string id, string desc, string cdsibs, string pendingStatus) 
		{
			string myQueryString="";

			conn2.QueryString = "SELECT * FROM T" +Request.QueryString["tablename"]+ " WHERE " +getColumnKey()+ "='" +id+ "'";
			conn2.ExecuteQuery();

			int jumlah = conn2.GetRowCount();
			
            if (jumlah > 0) 
			{				
				myQueryString = "UPDATE T" +Request.QueryString["tablename"]+ " SET " + getColumnDesc()+
								  "= '" +desc+ "', CH_STA = '" +pendingStatus+ "' WHERE "+getColumnKey()+ "= '"+id+"'";
				conn2.QueryString = myQueryString;
				
				try 
				{
					conn2.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					if (ex.Message.ToString().IndexOf("truncate") > 0)
						Tools.popMessage(this, "Input melebihi batas !");
				}
				if (hasCD_SIBS())
				{
					try
					{
						conn2.QueryString = "UPDATE T" +Request.QueryString["tablename"]+ " SET CD_SIBS = '" +cdsibs+ "' WHERE "+getColumnKey()+ "= '"+id+"'";
						conn2.ExecuteNonQuery();
					} 
					catch {}
				}
			}
			else 
			{
				myQueryString="INSERT INTO T" +Request.QueryString["tablename"]+
					"("+ getColumnKey() + "," + getColumnDesc() + ",CH_STA) VALUES ('"+id+"', '"+desc+"', '"+pendingStatus+"')";
				
				conn2.QueryString = myQueryString;
				
				try 
				{
					conn2.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					if (ex.Message.ToString().IndexOf("truncate") > 0)
						Tools.popMessage(this, "Input melebihi batas !");
				}
				
				if (hasCD_SIBS())
				{
					try
					{
						conn2.QueryString = "UPDATE T" +Request.QueryString["tablename"]+ " SET CD_SIBS = '" +cdsibs+ "' WHERE "+getColumnKey()+ "= '"+id+"'";
						conn2.ExecuteNonQuery();
					} 
					catch {}
				}
			}
			
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../GeneralParamAll.aspx?ModuleID=40");
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls(); 
		}
	}		
}
