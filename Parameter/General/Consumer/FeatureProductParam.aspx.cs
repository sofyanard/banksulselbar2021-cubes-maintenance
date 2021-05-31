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
	/// Summary description for FeatureProductParam.
	/// </summary>
	public partial class FeatureProductParam : System.Web.UI.Page
	{
		protected Connection conn,conn2;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				CodeGen();//memanggil code generator
				viewExistingData();
				viewPendingData();
				vwDtProg();
				vwDtProd();
				vwJobType();
				
				Datagrid1.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change1);
				Datagrid3.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change3);
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
			this.Datagrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGExisting_ItemCommand);
			this.Datagrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Grid_Change1);
			this.Datagrid3.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGRequest_ItemCommand);
			this.Datagrid3.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Grid_Change3);

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
			codes="";
			//menentukan code berikutnya
			//cek dulu apa sudah ada di pending, trus bisa ke master
			conn.QueryString = "select max(fid) " +
				"from 	( "+
				"	select isnull(max(convert(int,feature_id)),0) fid " +
				"	from feature " +
				"	union " +
				"	select isnull(max(convert(int,feature_id)),0) fid " +
				"	from tfeature " +
				"	) allfettbl ";
			conn.ExecuteQuery();
			zeros = conn.GetFieldValue(0,0);
			digits = zeros.Length;
			digits2 = Int32.Parse(zeros)+1;
			zeros2 = digits2.ToString();
			codes = digits2.ToString();
			for (digits3 = zeros2.Length;digits3<4;digits3++)
			{
				codes='0' + codes;
			}
			lbl_max.Text=codes;
		}
		private void vwDtProg()
		{
			if(!IsPostBack)
			{
				string myQUERY="select pr_code,pr_desc from program";
				GlobalTools.fillRefList(DDL_PROGRAM,myQUERY,conn);
			}
		}
		private void vwDtProd()
		{			
			if(!IsPostBack)
			{
				string myQUERY="select productid,productname from tproduct";
				GlobalTools.fillRefList(DDL_PRODUCT,myQUERY,conn);
			}
		}
		private void vwJobType()
		{
			if(!IsPostBack)
			{
				string myQUERY="select JOB_TYPE_ID,DES from JOB_TYPE";
				GlobalTools.fillRefList(DDL_JOB_TYPE,myQUERY,conn);
			}
		}
		private void viewExistingData()
		{
			conn.QueryString = "select FEATURE_ID,FEATURE_DESC,FORMULA,FEATURE_TABLE,FEATURE_LINK,"+
				"PR_DESC,PRODUCTNAME,FEATURE_JOBTYPE, FEATURE_FIELD, "+
				"case when FEATURE_ACTIVE='1' then 'Active' else '' end ACTIVE,"+
				"PRM_CODE,PR_CODE,PRODUCTID,JOB_TYPE_ID,FEATURE_ACTIVE,MIN_VALUE,MAX_VALUE,DESCRIPTION "+
				"from FEATURE left join TPRODUCT on FEATURE.PRODUCT = TPRODUCT.PRODUCTID "+
				"left join PROGRAM on FEATURE.PROGRAM = PROGRAM.PR_CODE "+
				"LEFT JOIN JOB_TYPE ON JOB_TYPE.JOB_TYPE_ID=FEATURE.FEATURE_JOBTYPE "+
				"where feature.active=1 ";

			if (this.DDL_PROGRAM.SelectedValue != "")
				conn.QueryString += "and (ISNULL(PROGRAM.PR_CODE ,'') = '' OR PROGRAM.PR_CODE = '" + this.DDL_PROGRAM.SelectedValue + "') ";
			if (this.DDL_PRODUCT.SelectedValue != "")
				conn.QueryString += "and (ISNULL(TPRODUCT.PRODUCTID ,'') = '' OR TPRODUCT.PRODUCTID = '" + this.DDL_PRODUCT.SelectedValue + "') ";
			if (this.DDL_JOB_TYPE.SelectedValue != "")
				conn.QueryString += "and (ISNULL(JOB_TYPE.JOB_TYPE_ID,'') = '' OR JOB_TYPE.JOB_TYPE_ID = '" + this.DDL_JOB_TYPE.SelectedValue + "') ";

			conn.ExecuteQuery();
			Datagrid1.DataSource = conn.GetDataTable().Copy();
			try 
			{
				Datagrid1.DataBind();
			}
			catch 
			{
				Datagrid1.CurrentPageIndex = Datagrid1.PageCount - 1;
				Datagrid1.DataBind();
			}
		}

		private void viewPendingData()
		{
			conn.QueryString = "select FEATURE_ID,FEATURE_DESC,FORMULA,FEATURE_TABLE,FEATURE_LINK,"+
				"PR_DESC,PRODUCTNAME,FEATURE_JOBTYPE, FEATURE_FIELD, "+
				"case when FEATURE_ACTIVE='1' then 'Active' else '' end ACTIVE,"+
				"PRM_CODE,PR_CODE,PRODUCTID,JOB_TYPE_ID,FEATURE_ACTIVE,MIN_VALUE,MAX_VALUE,CH_STA,"+
				"case when CH_STA='0' then 'Update' when CH_STA='1' then 'Insert' when CH_STA='2' then 'Delete' else '' end CH_STA1,DESCRIPTION "+
				"from TFEATURE a left join TPRODUCT b on a.PRODUCT = b.PRODUCTID "+
				"left join PROGRAM c on a.PROGRAM = c.PR_CODE "+
				"LEFT JOIN JOB_TYPE d ON d.JOB_TYPE_ID=a.FEATURE_JOBTYPE "+
				"WHERE 1 = 1 ";

			/*
			if (this.DDL_PROGRAM.SelectedValue != "")
				conn.QueryString += "and (ISNULL(c.PR_CODE ,'') = '' OR c.PR_CODE = '" + this.DDL_PROGRAM.SelectedValue + "') ";
			if (this.DDL_PRODUCT.SelectedValue != "")
				conn.QueryString += "and (ISNULL(b.PRODUCTID ,'') = '' OR b.PRODUCTID = '" + this.DDL_PRODUCT.SelectedValue + "') ";
			if (this.DDL_JOB_TYPE.SelectedValue != "")
				conn.QueryString += "and (ISNULL(d.JOB_TYPE_ID,'') = '' OR d.JOB_TYPE_ID = '" + this.DDL_JOB_TYPE.SelectedValue + "') ";
			*/

			conn.ExecuteQuery();
			Datagrid3.DataSource = conn.GetDataTable().Copy();
			try 
			{
				Datagrid3.DataBind();
			}
			catch 
			{
				Datagrid3.CurrentPageIndex = Datagrid3.PageCount - 1;
				Datagrid3.DataBind();
			}
		}

		private string CleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}
		
		private void clearControls()
		{
			TXT_ID.Text = "";
			TXT_FORMULA.Text = "";
			TXT_TABLE.Text = "";
			TXT_LINK.Text = "";
			TXT_DESC.Text = "";
			TXT_MIN.Text = "";
			TXT_MAX.Text = "";
			DDL_PROGRAM.SelectedValue = "";
			DDL_PRODUCT.SelectedValue = "";
			DDL_JOB_TYPE.SelectedValue = "";
			RDB_COL_TYPE.SelectedValue = "1";
			TXT_FIELD.Text = "";
			LBL_SAVEMODE.Text="1";		//default insert
			CodeGen();
		}

		private string checkApost(string str)
		{
			return str.Replace("'", "''").Trim();
		}

		private void executeMaker(string id,string desc,string form,string tbl,string lnk,string pr,string pd,string job,string actv,string min,string max,string prm,string desc1, string pendingStatus) 
		{

			conn.QueryString = "SELECT * FROM TFEATURE WHERE FEATURE_ID='" +id+ "'";
			conn.ExecuteQuery();

			int jumlah = conn.GetRowCount();
			
			if (jumlah != 0) 
			{				
				conn.QueryString = "UPDATE TFEATURE SET FEATURE_DESC='"+desc+"', "+
					"PRM_CODE='"+prm+"', FEATURE_TABLE='"+tbl+"', FEATURE_FIELD="+GlobalTools.ConvertNull(TXT_FIELD.Text)+","+
					"FEATURE_LINK='"+lnk+"', FEATURE_JOBTYPE='"+job+"', FORMULA='"+checkApost(form)+"',"+
					"PRODUCT='"+pd+"', PROGRAM='"+pr+"', FEATURE_ACTIVE='"+actv+"',"+
					"MIN_VALUE='"+min+"', MAX_VALUE='"+max+"', DESCRIPTION='"+desc1+"',"+
					"CH_STA='"+pendingStatus+"' WHERE FEATURE_ID='"+id+"'";
				
				conn.ExecuteQuery();
				
			}
			else 
			{
				conn.QueryString = "INSERT INTO TFEATURE VALUES "+
					"('"+lbl_max.Text+"','"+id+"', '"+desc+"', '"+prm+"', '"+tbl+"', "+GlobalTools.ConvertNull(TXT_FIELD.Text)+", "+
					"'"+lnk+"', '"+job+"', '"+form+"', '"+pd+"', '"+pr+"', '"+actv+"', "+
					"'', '"+min+"', '"+max+"', '"+desc1+"', '"+pendingStatus+"')";
				
				try 
				{
					conn.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					if (ex.Message.ToString().IndexOf("truncate") > 0)
						Tools.popMessage(this, "Input melebihi batas !");
				}
			}

			clearControls();
		}
	
		void Grid_Change1(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid1.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			viewExistingData();	
		}

		void Grid_Change3(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid3.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			viewPendingData();	
		}
		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = "0";

					lbl_max.Text=e.Item.Cells[0].Text.Trim();
					TXT_ID.Text = e.Item.Cells[1].Text.Trim();
					if (e.Item.Cells[1].Text.Trim()=="&nbsp;")
						TXT_ID.Text = "";
					TXT_FORMULA.Text = e.Item.Cells[2].Text.Trim();
					if (e.Item.Cells[2].Text.Trim()=="&nbsp;")
						TXT_FORMULA.Text = "";
					TXT_TABLE.Text = e.Item.Cells[3].Text.Trim();
					if (e.Item.Cells[3].Text.Trim()=="&nbsp;")
						TXT_TABLE.Text = "";
					TXT_LINK.Text = e.Item.Cells[4].Text.Trim();
					if (e.Item.Cells[4].Text.Trim()=="&nbsp;")
						TXT_LINK.Text = "";
					TXT_DESC.Text = e.Item.Cells[16].Text.Trim();
					if (e.Item.Cells[16].Text.Trim()=="&nbsp;")
						TXT_DESC.Text = "";
					TXT_MIN.Text = e.Item.Cells[14].Text.Trim();
					if (e.Item.Cells[14].Text.Trim()=="&nbsp;")
						TXT_MIN.Text = "";
					TXT_MAX.Text = e.Item.Cells[15].Text.Trim();
					if (e.Item.Cells[15].Text.Trim()=="&nbsp;")
						TXT_MAX.Text = "";
					TXT_FIELD.Text = e.Item.Cells[17].Text.Trim();
					if (e.Item.Cells[17].Text.Trim()=="&nbsp;")
						TXT_FIELD.Text = "";
					
					if (e.Item.Cells[10].Text.Trim()=="&nbsp;")
						DDL_PROGRAM.SelectedValue = "";
					else
						DDL_PROGRAM.SelectedValue = e.Item.Cells[10].Text;
					if (e.Item.Cells[11].Text.Trim()=="&nbsp;")
						DDL_PRODUCT.SelectedValue = "";
					else
						DDL_PRODUCT.SelectedValue = e.Item.Cells[11].Text;
					if (e.Item.Cells[12].Text.Trim()=="&nbsp;")
						DDL_JOB_TYPE.SelectedValue = "";
					else
						DDL_JOB_TYPE.SelectedValue = e.Item.Cells[12].Text;
					if (e.Item.Cells[13].Text.Trim()=="&nbsp;")
						RDB_COL_TYPE.SelectedValue = "0";
					else
						RDB_COL_TYPE.SelectedValue = e.Item.Cells[13].Text;

					break;

				case "delete":
					string id      = CleansText(e.Item.Cells[0].Text.Trim());
					string desc    = CleansText(e.Item.Cells[1].Text.Trim());
					string form	   = CleansText(e.Item.Cells[2].Text.Trim());
					string tbl     = CleansText(e.Item.Cells[3].Text.Trim());
					string lnk	   = CleansText(e.Item.Cells[4].Text.Trim());
					string pr      = CleansText(e.Item.Cells[10].Text.Trim());
					string pd	   = CleansText(e.Item.Cells[11].Text.Trim());
					string job	   = CleansText(e.Item.Cells[12].Text.Trim());
					string actv	   = CleansText(e.Item.Cells[13].Text.Trim());
					string min     = CleansText(e.Item.Cells[14].Text.Trim());
					string max	   = CleansText(e.Item.Cells[15].Text.Trim());
					string prm     = CleansText(e.Item.Cells[9].Text.Trim());
					string desc1   = CleansText(e.Item.Cells[16].Text.Trim());
					executeMaker(id,desc,form,tbl,lnk,pr,pd,job,actv,min,max,prm,desc1,"2");
					viewPendingData();
					break;

				default :
					break;
			}
		}
		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					LBL_SAVEMODE.Text = e.Item.Cells[16].Text;
					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
				
					lbl_max.Text=e.Item.Cells[0].Text.Trim();
					TXT_ID.Text = e.Item.Cells[1].Text.Trim();
					if (e.Item.Cells[1].Text.Trim()=="&nbsp;")
						TXT_ID.Text = "";
					TXT_FORMULA.Text = e.Item.Cells[2].Text.Trim();
					if (e.Item.Cells[2].Text.Trim()=="&nbsp;")
						TXT_FORMULA.Text = "";
					TXT_TABLE.Text = e.Item.Cells[3].Text.Trim();
					if (e.Item.Cells[3].Text.Trim()=="&nbsp;")
						TXT_TABLE.Text = "";
					TXT_LINK.Text = e.Item.Cells[4].Text.Trim();
					if (e.Item.Cells[4].Text.Trim()=="&nbsp;")
						TXT_LINK.Text = "";
					TXT_DESC.Text = e.Item.Cells[18].Text.Trim();
					if (e.Item.Cells[18].Text.Trim()=="&nbsp;")
						TXT_DESC.Text = "";
					TXT_MIN.Text = e.Item.Cells[14].Text.Trim();
					if (e.Item.Cells[14].Text.Trim()=="&nbsp;")
						TXT_MIN.Text = "";
					TXT_MAX.Text = e.Item.Cells[15].Text.Trim();
					if (e.Item.Cells[15].Text.Trim()=="&nbsp;")
						TXT_MAX.Text = "";
					TXT_FIELD.Text = e.Item.Cells[19].Text.Trim();
					if (e.Item.Cells[19].Text.Trim()=="&nbsp;")
						TXT_FIELD.Text = "";
					
					if (e.Item.Cells[10].Text.Trim()=="&nbsp;")
						DDL_PROGRAM.SelectedValue = "";
					else
						DDL_PROGRAM.SelectedValue = e.Item.Cells[10].Text;
					if (e.Item.Cells[11].Text.Trim()=="&nbsp;")
						DDL_PRODUCT.SelectedValue = "";
					else
						DDL_PRODUCT.SelectedValue = e.Item.Cells[11].Text;
					if (e.Item.Cells[12].Text.Trim()=="&nbsp;")
						DDL_JOB_TYPE.SelectedValue = "";
					else
						DDL_JOB_TYPE.SelectedValue = e.Item.Cells[12].Text;
					if (e.Item.Cells[13].Text.Trim()=="&nbsp;")
						RDB_COL_TYPE.SelectedValue = "0";
					else
						RDB_COL_TYPE.SelectedValue = e.Item.Cells[13].Text;

					break;

				case "delete":
					string id = e.Item.Cells[0].Text;
					
					conn.QueryString = "delete from TFEATURE WHERE FEATURE_ID='" + id + "'";
					conn.ExecuteQuery();
					viewPendingData();
					clearControls();
					break;
				default :
					break;
			}
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string a="";
			if (RDB_PROMPT.Checked==true)
				a="1";

			executeMaker(lbl_max.Text.Trim(),TXT_ID.Text.Trim(),TXT_FORMULA.Text.Trim(),
				TXT_TABLE.Text.Trim(),TXT_LINK.Text.Trim(),DDL_PROGRAM.SelectedValue.Trim(),
				DDL_PRODUCT.SelectedValue.Trim(),DDL_JOB_TYPE.SelectedValue.Trim(),
				RDB_COL_TYPE.SelectedValue.Trim(),TXT_MIN.Text.Trim(),TXT_MAX.Text.Trim(),
                a,TXT_DESC.Text.Trim(),LBL_SAVEMODE.Text.Trim());

			viewPendingData();
			
			clearControls();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleId=40");
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();
		}

		protected void DDL_PROGRAM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			viewExistingData();
			//viewPendingData();
		}

		protected void DDL_PRODUCT_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			viewExistingData();
			//viewPendingData();
		}

		protected void DDL_JOB_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			viewExistingData();
			//viewPendingData();
		}
		
	}
}
