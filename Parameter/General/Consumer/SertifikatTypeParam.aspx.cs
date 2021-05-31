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
	/// Summary description for SertifikatTypeParam.
	/// </summary>
	public partial class SertifikatTypeParam : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid;
	
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

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");	
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

			LBL_SAVEMODE.Text = "1"; 

			InitialCon();

			CodeSeq();
			BindData1();
			BindData2();
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void BindData1()
		{
			string exp = "";
			CheckBox chk;

			conn.QueryString = "select ST_CODE, ST_DESC, ST_EXPDATEFLAG, CD_SIBS from RFSERTYPE where ACTIVE = '1'";
			conn.ExecuteQuery();

			if (conn.GetRowCount() != 0)
			{
				DataTable dt = new DataTable();
				dt = conn.GetDataTable().Copy();
				DG1.DataSource = dt;

				try
				{
					DG1.DataBind();
				}
				catch 
				{
					DG1.CurrentPageIndex = DG1.PageCount - 1;
					DG1.DataBind();
				}

				for (int i = 0; i < DG1.Items.Count; i++)
				{
					exp = DG1.Items[i].Cells[2].Text;
					chk = (CheckBox)DG1.Items[i].Cells[3].FindControl("CHK_EXD1");

					if (exp == "1")
						chk.Checked = true;
					else
						chk.Checked = false;
					
					chk.Enabled = false;
				}
			} 

			conn.ClearData();
		}

		private void BindData2()
		{
			string expd = "";
			CheckBox chk;

			conn.QueryString = "select ST_CODE, ST_DESC, ST_EXPDATEFLAG, CD_SIBS, CH_STA, "+
							   "STATUS = case CH_STA when '1' then 'INSERT' " +
							   "when '2' then 'UPDATE' "+
						 	   "when '3' then 'DELETE' end "+
							   "from TRFSERTYPE ORDER BY ST_CODE";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG2.DataSource = dt;

			try
			{
				DG2.DataBind();
			}
			catch 
			{
				DG2.CurrentPageIndex = DG2.PageCount - 1;
				DG2.DataBind();
			}

			for (int i = 0; i < DG2.Items.Count; i++)
			{
				expd = DG2.Items[i].Cells[2].Text;
				chk = (CheckBox)DG2.Items[i].Cells[3].FindControl("CHK_EXD2");

				if (expd == "1")
					chk.Checked = true;
				else
					chk.Checked = false;
					
				chk.Enabled = false;
			}
		}

		private void ClearEditBoxes()
		{
			//TXT_CODE.Text = "";
			TXT_DESC.Text = "";
			TXT_SBCODE.Text = "";
			CHK_EXDATE.Checked = false;

			TXT_CODE.Enabled = true;

			LBL_SAVEMODE.Text  = "1";
		}

		private void CodeSeq_old(string mode)
		{
			string seq = "";
			int number = Int16.Parse(LBL_NB.Text);

			if(mode == "1")
			{
				number++;
			}

			LBL_NB.Text = number.ToString();

			conn.QueryString = "select max(convert(int,isnull(ST_CODE,0)))+ "+LBL_NB.Text+" as MAXSEQ from RFSERTYPE";
			conn.ExecuteQuery();

			seq = conn.GetFieldValue("MAXSEQ").Trim();  

			if(seq.Length == 1)
				seq = "000" + seq;
			else if(seq.Length == 2)
				seq = "00" + seq;
			else if(seq.Length == 3)
				seq = "0" + seq;

			TXT_CODE.Text = seq;  

			conn.ClearData(); 
		}

		private void CodeSeq()
		{
			string seq = "", bseq = "";
			int dg = 4;
			try
			{
				conn.QueryString = "select (select isnull(max(ST_CODE),0) + 1 from RFSERTYPE) a, " +
					"(select isnull(max(ST_CODE),0) + 1 from TRFSERTYPE) b";
				conn.ExecuteQuery();

				seq = conn.GetFieldValue("a").Trim();
				if (int.Parse(conn.GetFieldValue("a").Trim()) < int.Parse(conn.GetFieldValue("b").Trim()))
					seq = conn.GetFieldValue("b").Trim();

				for(int i = seq.Length; i < dg; i++)
				{
					bseq = "0" +bseq; 
				}

				TXT_CODE.Text = bseq+""+seq;
			}
			catch
			{
				GlobalTools.popMessage(this,"Error, this parameter have invalid id!"); 
				return;
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
			this.DG1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG1_ItemCommand);
			this.DG1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG1_PageIndexChanged);
			this.DG2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG2_ItemCommand);
			this.DG2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG2_PageIndexChanged);

		}
		#endregion

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string cek;
			int hit = 0;

			if(CHK_EXDATE.Checked)
				cek = "1";
			else
				cek = "0";

			if(TXT_CODE.Text.Length != 4)
			{
				GlobalTools.popMessage(this,"Code must 4 digits in lentgh!");  
				return;
			}

			conn.QueryString = "SELECT ST_CODE FROM TRFSERTYPE WHERE ST_CODE = '"+TXT_CODE.Text+"'";
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVEMODE.Text != "1"))
			{
				conn.QueryString = "UPDATE TRFSERTYPE SET ST_DESC = '"+TXT_DESC.Text+"', ST_EXPDATEFLAG = '"+cek+"', CD_SIBS = '"+TXT_SBCODE.Text+"' where ST_CODE = '"+TXT_CODE.Text+"'";  					
				conn.ExecuteQuery();	

				ClearEditBoxes(); 

				CodeSeq();
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "2"))
			{
				conn.QueryString = "INSERT INTO TRFSERTYPE VALUES('"+TXT_CODE.Text+"','"+TXT_DESC.Text+"','"+TXT_SBCODE.Text+"','2',"+GlobalTools.ConvertNull(cek)+")";
				
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
				
				CodeSeq();	
			}
			else if((hit == 0) && (LBL_SAVEMODE.Text == "1"))
			{
				conn.QueryString = "INSERT INTO TRFSERTYPE VALUES('"+TXT_CODE.Text+"','"+TXT_DESC.Text+"','"+TXT_SBCODE.Text+"','1',"+GlobalTools.ConvertNull(cek)+")";
				
				try
				{
					conn.ExecuteQuery();
					CodeSeq();
				}
				catch
				{
					GlobalTools.popMessage(this,"Cannot insert same code, request canceled!"); 
				}
				finally
				{
					ClearEditBoxes(); 
				}
			}
			else if((hit != 0) && (LBL_SAVEMODE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data untuk Approve!");
				return;
			}

			conn.ClearData();
 	
			BindData2();

			LBL_SAVEMODE.Text = "1"; 
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes(); 
		}

		private void DG1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG1.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 
		}

		private void DG1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string stcode, stdesc, exflag, sbc; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					stcode = e.Item.Cells[0].Text.Trim();
					stdesc = e.Item.Cells[1].Text.Trim();
					exflag = e.Item.Cells[2].Text.Trim();
					sbc = cleansText(e.Item.Cells[4].Text);   

					conn.QueryString = "SELECT ST_CODE FROM TRFSERTYPE WHERE ST_CODE = '"+stcode+"'";
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						GlobalTools.popMessage(this,"Cannot request to delete this row!");
					}
					else
					{
						conn.QueryString = "INSERT INTO TRFSERTYPE VALUES('"+stcode+"',"+GlobalTools.ConvertNull(stdesc)+","+GlobalTools.ConvertNull(sbc)+",'3','"+exflag+"')";
						conn.ExecuteQuery();
						BindData2();
					}
					break;

				case "edit":
					TXT_CODE.Enabled = false;
					TXT_CODE.Text = e.Item.Cells[0].Text;
					TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
					TXT_SBCODE.Text = cleansText(e.Item.Cells[4].Text); 
   
					if(e.Item.Cells[2].Text == "1")
						CHK_EXDATE.Checked = true;
					else
						CHK_EXDATE.Checked = false;

					LBL_SAVEMODE.Text = "2";		
					break;
			}
		}

		private void DG2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG2.CurrentPageIndex = e.NewPageIndex;
			BindData2(); 
		}

		private void DG2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string code; 

			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					code = e.Item.Cells[0].Text.Trim();

					conn.QueryString = "DELETE FROM TRFSERTYPE WHERE ST_CODE = '"+code+"'";
					conn.ExecuteQuery();
					BindData2();
					break;
				case "edit":
					if(e.Item.Cells[7].Text.Trim() == "3")
					{
						LBL_SAVEMODE.Text = "1";
					}
					else
					{
						TXT_CODE.Enabled = false;
						TXT_CODE.Text = e.Item.Cells[0].Text;
						TXT_DESC.Text = cleansText(e.Item.Cells[1].Text);
						TXT_SBCODE.Text = cleansText(e.Item.Cells[4].Text);
   
						if(e.Item.Cells[2].Text.Trim() == "1")
							CHK_EXDATE.Checked = true;
						else
							CHK_EXDATE.Checked = false;
				
						LBL_SAVEMODE.Text = "2";
					}
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40");
		}
	}
}
