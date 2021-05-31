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
	/// Summary description for NotaryDocParam.
	/// </summary>
	public partial class NotaryDocParam : System.Web.UI.Page
	{
		protected Connection conn2,conn;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = new Connection((string) Session["ConnString"]);
			SetDBConn2();

			if (!IsPostBack)
			{
				dtProgram();
				dtType();
				viewPendingData();
				viewExistingData();
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
			conn.QueryString = "select * from RFMODULE where MODULEID=40";
			conn.ExecuteQuery();
			
			conn2 = new Connection ("Data Source=" + conn.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn.GetFieldValue("DB_NAMA") + ";uid=" + conn.GetFieldValue("DB_LOGINID") + ";pwd=" + conn.GetFieldValue("DB_LOGINPWD") + ";Pooling=true");
		}

		private void dtProgram()
		{
			string myQUERY="select pr_code,pr_desc from program order by pr_code";
			GlobalTools.fillRefList(DDL_PROGRAM,myQUERY,conn2);
		}
		
		private void dtProduct(string a)
		{
			string myQUERY="select a.productid,c.productname from PROGRAMPRO a,PROGRAM b,TPRODUCT c where a.pr_code=b.pr_code and a.productid=c.productid and a.pr_code='"+DDL_PROGRAM.SelectedValue+"'";
			GlobalTools.fillRefList(DDL_PRODUCT,myQUERY,conn2);
			DDL_PRODUCT.SelectedValue = a;
		}
		
		private void dtType()
		{
			string myQUERY="select DNM_CODE, DNM_DESC from RFDOCNOTARY_MASTER ORDER BY DNM_CODE";
			GlobalTools.fillRefList(DDL_TYPE,myQUERY,conn2);
		}
		private void dtDocument(string a)
		{
			string myQUERY="select a.DND_CODE, DND_DESC from RFDOCNOTARY_DETAIL a join RFDOCNOTARY_MASTER b on a.DNM_CODE = b.DNM_CODE WHERE a.DNM_CODE='"+DDL_TYPE.SelectedValue+"'";
			GlobalTools.fillRefList(DDL_DOC,myQUERY,conn2);
			DDL_DOC.SelectedValue=a;
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string isChecked; //mandatory checkbox

			LBL_PROGRAM.Text=DDL_PROGRAM.SelectedValue;
			LBL_PRODUCT.Text=DDL_PRODUCT.SelectedValue;
			LBL_TYPE.Text=DDL_TYPE.SelectedValue;
			LBL_DOC.Text=DDL_DOC.SelectedValue;
			
			if (CXB_MANDATORY.Checked==true)
				isChecked="1";
			else
				isChecked="0";
			
			executeMaker(LBL_PROGRAM.Text,LBL_PRODUCT.Text,LBL_DOC.Text,isChecked,LBL_SAVEMODE.Text,LBL_TYPE.Text);
			
			viewExistingData();
			viewPendingData();
			clearControls();
		}

		protected void DDL_PROGRAM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			dtProduct("");

			viewExistingData();
		}

		protected void DDL_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			dtDocument("");
		}

		private void viewPendingData()
		{
			conn2.QueryString = "select b.pr_desc,c.productname,DNM_DESC,DND_DESC,MANDATORY1 = case DN_MANDATORY when '1' then 'Yes' else 'No' end, CH_STA1 = case CH_STA when '0' then 'Update' when '1' then 'Insert'when '2' then 'Delete' else '' end, CH_STA,b.pr_code,c.productid,a.DNM_CODE,a.DND_CODE,DN_MANDATORY from trfdocnotary a left join program b on a.pr_code=b.pr_code left join tproduct c on a.productid=c.productid left join RFDOCNOTARY_DETAIL DND on a.DND_CODE = DND.DND_CODE left join RFDOCNOTARY_MASTER DNM on a.DNM_CODE = DNM.DNM_CODE ";
			conn2.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			DGRequest.DataSource = dt;
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

		private void viewExistingData()
		{
			string plus = "";

			if(DDL_PROGRAM.SelectedValue != "")
			{
				if(plus != "")
					plus = plus + "AND PR_CODE = '"+DDL_PROGRAM.SelectedValue+"' "; 
				else
					plus = "AND PR_CODE = '"+DDL_PROGRAM.SelectedValue+"' "; 
			}

			if(DDL_PRODUCT.SelectedValue != "")
			{
				if(plus != "")
					plus = plus + "AND PRODUCTID = '"+DDL_PRODUCT.SelectedValue+"' "; 
				else
					plus = "AND PRODUCTID = '"+DDL_PRODUCT.SelectedValue+"' "; 
			}

			conn2.QueryString = "select * FROM VW_PRM_NOTARY_DOC where 1=1 " +plus;
			conn2.ExecuteQuery();
			
			DataTable dt = new DataTable();
			dt = conn2.GetDataTable().Copy();
			DGExisting.DataSource = dt;
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
		private void DGExisting_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{			
			DGExisting.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		}
		private void DGRequest_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGRequest.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}
		private void clearControls() 
		{
			DDL_PROGRAM.SelectedIndex=0;
			dtProduct("");
			DDL_TYPE.SelectedIndex=0;
			dtDocument("");
			CXB_MANDATORY.Checked=false;
			DDL_PROGRAM.Enabled=true;
			DDL_PRODUCT.Enabled=true;
			DDL_TYPE.Enabled=true;
			//DDL_DOC.Enabled=true;
			LBL_SAVEMODE.Text="1";
		}

		private void executeMaker(string pr, string pd, string doc, string md, string pendingStatus, string type) 
		{
			string myQueryString="";
			conn2.QueryString = "select * from trfdocnotary where pr_code='"+pr+"' "+ 
								"and productid='"+pd+"' and dnd_code='"+doc+"' and dnm_code='"+type+"'";
			conn2.ExecuteQuery();

			int jumlah = conn2.GetRowCount();
			
			if (jumlah > 0) 
			{
				myQueryString = "UPDATE trfdocnotary set dn_mandatory='"+md+"',ch_sta='"+pendingStatus+
					"' where pr_code='"+pr.Trim()+"' and productid='"+pd.Trim()+"' and dnd_code='"+doc.Trim()+"'";
				
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
			}
			else 
			{
				if (LBL_ACTIVE.Text.Trim() == "1")
				{
					myQueryString="INSERT INTO TRFDOCNOTARY VALUES ('','"+pr+"','"+pd+"','"+doc+"','"+md+"','"+pendingStatus+"','1','"+type+"')";
					conn2.QueryString = myQueryString;
				}
				else
				{
					myQueryString = "INSERT INTO tRFDOCNOTARY VALUES ('','"+pr+"','"+pd+"','"+doc+"','"+md+"','"+pendingStatus+"','','"+type+"')";
					conn2.QueryString = myQueryString;
				}
				try 
				{
					conn2.ExecuteQuery();
				} 
				catch (ApplicationException ex) 
				{
					if (ex.Message.ToString().IndexOf("truncate") > 0)
						Tools.popMessage(this, "Input melebihi batas !");
				}
			}
		}
		private void DGRequest_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{			
			clearControls();
			string myQUERY;
			
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					string pr = e.Item.Cells[7].Text;
					string pd = e.Item.Cells[8].Text;
					string tp = e.Item.Cells[9].Text;
					string dc = e.Item.Cells[10].Text;
					string md = e.Item.Cells[11].Text;
					LBL_SAVEMODE.Text = e.Item.Cells[6].Text;

					DDL_PROGRAM.Enabled=false;
					DDL_PRODUCT.Enabled=false;
					//DDL_DOC.Enabled = false;
					DDL_TYPE.Enabled = false;

					if (LBL_SAVEMODE.Text.Trim() == "2") 
					{
						// kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
						LBL_SAVEMODE.Text = "1";
						break;
					}
					//--
					if (pr!="&nbsp;")
						DDL_PROGRAM.SelectedValue = pr;
					if (tp!="&nbsp;")
						DDL_TYPE.SelectedValue = tp;
					if (pd!="&nbsp;")
						dtProduct(pd);
					else
						dtProduct("");
					if (dc!="&nbsp;")
						dtDocument(dc);
					else
						dtDocument("");
					
					//--
					if (md == "1")
						CXB_MANDATORY.Checked=true;
					else
						CXB_MANDATORY.Checked=false;
					break;

				case "delete":
					myQUERY= "delete from tRFDOCNOTARY WHERE PR_CODE='" + e.Item.Cells[7].Text + "'"+
							 " and PRODUCTID='"+e.Item.Cells[8].Text+"' and DND_CODE='"+e.Item.Cells[10].Text+"'";
					conn2.QueryString=myQUERY;
					conn2.ExecuteQuery();
					viewPendingData();
					
					break;
				default : //do nothing
					break;
			}
		}
		private void DGExisting_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//clearControls();
			
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "edit":
					string pr = e.Item.Cells[0].Text;
					string pd = e.Item.Cells[2].Text;
					string tp = e.Item.Cells[4].Text;
					string dc = e.Item.Cells[6].Text;
					string md = e.Item.Cells[8].Text;

					DDL_PROGRAM.Enabled=false;
					DDL_PRODUCT.Enabled=false;
					//DDL_DOC.Enabled = false;
					DDL_TYPE.Enabled = false;
					
					LBL_SAVEMODE.Text = "0";
					
					if (pr.Trim() !="&nbsp;" || pr.Trim() !="")
					{
						DDL_PROGRAM.SelectedValue = pr;
						
					}
					if (tp.Trim() !="&nbsp;" || tp.Trim() != "")
					{
						DDL_TYPE.SelectedValue = tp;
					}

					if (pd!="&nbsp;")
						dtProduct(pd);
					else
						dtProduct("");
					if (dc!="&nbsp;")
						dtDocument(dc);
					else
						dtDocument("");
					//--
					if (md == "1")
						CXB_MANDATORY.Checked=true;
					else
						CXB_MANDATORY.Checked=false;

					DDL_PROGRAM.AutoPostBack = true;
					DDL_TYPE.AutoPostBack = true;
					break;

				case "delete":
					executeMaker(e.Item.Cells[0].Text,e.Item.Cells[2].Text,e.Item.Cells[6].Text,e.Item.Cells[8].Text,"2",e.Item.Cells[4].Text);
					viewExistingData();
					viewPendingData();
					
					break;
				default : //do nothing
					break;
			}
		}
		
		

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearControls();
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleId=40");
		}

		protected void DDL_PRODUCT_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			viewExistingData();
		}

	}
}
