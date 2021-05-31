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
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for GpmCoefficient.
	/// </summary>
	public partial class GpmCoefficient : System.Web.UI.Page
	{
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=CUBESDEVNET;uid=sa;pwd=");
		//protected Connection conn2 = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV2;uid=sa;pwd=dmscorp");
		protected Connection conn,conn2;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if(!IsPostBack)
			{
				bindData1();
				bindData2();
				LBL_SAVEMODE.Text="1";

				conn.QueryString = "select PR_CODE,PR_DESC from program";
				conn.ExecuteQuery();
				DDL_PROGRAM.Items.Add(new ListItem("--select--",""));
				for (int i=0; i<conn.GetRowCount(); i++)
				{
					DDL_PROGRAM.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
				}

				/*conn.QueryString = "select productid,productname from tproduct";
				conn.ExecuteQuery();
				DDL_PRODID.Items.Add(new ListItem("--select--",""));
				for (int i=0; i<conn.GetRowCount(); i++)
				{
					DDL_PRODID.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
				}*/
				DDL_PRODID.Items.Add(new ListItem("--select--",""));
				DDL_TNSEQ.Items.Add(new ListItem("--select--",""));

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
			conn.QueryString = "select a.productid,a.tn_seq,c.tn_desc,a.yearseq,a.coeff_value, b.productname,c.pr_code,d.pr_desc "+
						"from gpm_coefficient a left join tproduct b on a.productid=b.productid "+
						"left join rfcawtenor c on a.tn_seq=c.tn_seq and a.productid = c.productid "+
						"left join program d on c.pr_code=d.pr_code where a.active='1'";
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
		
			conn.QueryString = "select seq_id,a.productid,a.tn_seq,c.tn_desc,a.yearseq,a.coeff_value,a.ch_sta, b.productname,c.pr_code,d.pr_desc "+
						"from tgpm_coefficient a inner join tproduct b on a.productid=b.productid "+
						"left join rfcawtenor c on a.tn_seq=c.tn_seq and a.productid = c.productid "+
						"left join program d on c.pr_code=d.pr_code";
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
				if (this.DGR_REQUEST.Items[i].Cells[6].Text.Trim() == "0")
				{
					this.DGR_REQUEST.Items[i].Cells[6].Text = "UPDATE";
				}
				else if (this.DGR_REQUEST.Items[i].Cells[6].Text.Trim() == "1")
				{
					this.DGR_REQUEST.Items[i].Cells[6].Text = "INSERT";
				}
				else if (this.DGR_REQUEST.Items[i].Cells[6].Text.Trim() == "2")
				{
					this.DGR_REQUEST.Items[i].Cells[6].Text = "DELETE";
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
			this.DGR_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_EXISTING_ItemCommand);
			this.DGR_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_EXISTING_PageIndexChanged);
			this.DGR_REQUEST.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_REQUEST_ItemCommand);
			this.DGR_REQUEST.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_REQUEST_PageIndexChanged);

		}
		#endregion

		private void GetTn(string prodid,string progid)
		{
			conn.QueryString = "select tn_seq,tn_desc from rfcawtenor "+
							   "where productid = '"+prodid+"' and pr_code='"+progid+"'";
			conn.ExecuteQuery();
			DDL_TNSEQ.Items.Clear();
			DDL_TNSEQ.Items.Add(new ListItem("--select--",""));
			for (int i=0; i<conn.GetRowCount(); i++)
			{
				DDL_TNSEQ.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
			}
		}

		private void GetProd(string progid)
		{
			conn.QueryString = "select a.productid,b.productname "+ 
				"from programpro a left join tproduct b on a.productid=b.productid "+
				"where a.pr_code='"+progid+"'";
			conn.ExecuteQuery();
			DDL_PRODID.Items.Clear();
			DDL_PRODID.Items.Add(new ListItem("--select--",""));
			for (int i=0; i<conn.GetRowCount(); i++)
			{
				DDL_PRODID.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
			}
		}

		protected void DDL_PRODID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			string prodid = DDL_PRODID.SelectedValue;
			string progid = DDL_PROGRAM.SelectedValue;
			GetTn(prodid,progid);
			/*
			conn.QueryString = "select tn_seq,tn_desc from rfcawtenor "+
				"where productid = '"+prodid+"' and pr_code='"+progid+"'";
			conn.ExecuteQuery();
			DDL_TNSEQ.Items.Clear();
			DDL_TNSEQ.Items.Add(new ListItem("--select--",""));
			for (int i=0; i<conn.GetRowCount(); i++)
			{
				DDL_TNSEQ.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
			}
			*/
		}

		private void ClearEditBoxes()
		{
			TXT_COEFF.Text = "";
			TXT_YEARSEQ.Text = "";
			LBL_ID.Text = "";
			LBL_SAVEMODE.Text = "1";
			try
			{
				DDL_PRODID.SelectedValue = "";
				DDL_TNSEQ.SelectedValue = "";
				DDL_PROGRAM.SelectedValue ="";
			}
			catch{}
			DDL_PRODID.Enabled = true;
			DDL_TNSEQ.Enabled = true;
			DDL_PROGRAM.Enabled = true;
			TXT_YEARSEQ.ReadOnly = false;
		}

		private void cleansTextBox (TextBox tb)
		{
			if (tb.Text.Trim() == "&nbsp;")
				tb.Text = "";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?mc=9902040201&moduleId=40");
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			bindData1();
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			bindData2();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			ClearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					try
					{
						string progid = e.Item.Cells[7].Text.Trim();
						string prodid = e.Item.Cells[6].Text.Trim();
						string tnseq1 = e.Item.Cells[8].Text.Trim();

						DDL_PROGRAM.SelectedValue = progid;
						GetProd(progid);
						DDL_PRODID.SelectedValue = prodid;
						GetTn(prodid,progid);
						DDL_TNSEQ.SelectedValue = tnseq1;

						/*
						DDL_PROGRAM.SelectedValue = e.Item.Cells[7].Text.Trim();
						//DDL_PRODID.SelectedValue = e.Item.Cells[6].Text.Trim();
						//DDL_TNSEQ.SelectedValue = e.Item.Cells[1].Text.Trim();
						conn.QueryString = "select a.pr_code,a.productid,b.productname "+ 
							"from programpro a left join tproduct b on a.productid=b.productid "+
							"where a.pr_code='"+e.Item.Cells[7].Text.Trim()+"'";
						conn.ExecuteQuery();
						for (int i=0; i<conn.GetRowCount(); i++)
						{
							this.GetProd(e.Item.Cells[7].Text.Trim());
							DDL_PRODID.SelectedValue = conn.GetFieldValue("productid");
						}

						conn.QueryString = "select tn_seq from rfcawtenor where productid='"+e.Item.Cells[6].Text.Trim()+"' and "+
											"pr_code='"+e.Item.Cells[7].Text.Trim()+"'";
						conn.ExecuteQuery();
						for (int i=0; i<conn.GetRowCount(); i++)
						{
							this.GetTn(e.Item.Cells[6].Text.Trim(),e.Item.Cells[7].Text.Trim());
							DDL_TNSEQ.SelectedValue = conn.GetFieldValue("tn_seq");
						}
						*/
					}
					catch{}
					TXT_COEFF.Text = e.Item.Cells[4].Text.Trim();
					TXT_YEARSEQ.Text = e.Item.Cells[3].Text.Trim();
					DDL_PRODID.Enabled = false;
					DDL_TNSEQ.Enabled = false;
					DDL_PROGRAM.Enabled =false;
					TXT_YEARSEQ.ReadOnly = true;
					LBL_SAVEMODE.Text = "0";
					LBL_ID.Text="";
					
					cleansTextBox(TXT_COEFF);
					cleansTextBox(TXT_YEARSEQ);
					break;
				case "delete":
					conn.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQID from TGPM_COEFFICIENT";
					conn.ExecuteQuery();
					string seqid = conn.GetFieldValue("SEQID");
					int tnseq = int.Parse(e.Item.Cells[8].Text.Trim());
					int yearseq = int.Parse(e.Item.Cells[3].Text.Trim());
					
					conn.QueryString="select * from TGPM_COEFFICIENT where PRODUCTID='"+e.Item.Cells[6].Text.Trim()+"' and "+
						"TN_SEQ ='"+e.Item.Cells[8].Text.Trim()+"' and YEARSEQ ='"+e.Item.Cells[3].Text.Trim()+"'";
					conn.ExecuteQuery();
					if(conn.GetRowCount()!=0)
					{
						conn.QueryString="update TGPM_COEFFICIENT set CH_STA='2' where PRODUCTID='"+e.Item.Cells[6].Text.Trim()+"' and "+
							"TN_SEQ ='"+e.Item.Cells[8].Text.Trim()+"' and YEARSEQ ='"+e.Item.Cells[3].Text.Trim()+"'";
						conn.ExecuteQuery();
					}
					else
					{
						conn.QueryString = "EXEC PARAM_GENERAL_GPM_COEFFICIENT '1','" + seqid +"','"+
							e.Item.Cells[6].Text.Trim()+ "', '" + int.Parse(e.Item.Cells[8].Text.Trim()) + "','" + 
							int.Parse(e.Item.Cells[3].Text.Trim())+"','"+GlobalTools.ConvertFloat(e.Item.Cells[4].Text.Trim())+"','2','',''  " ;
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
			ClearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					string status = e.Item.Cells[6].Text;
					string progid = e.Item.Cells[9].Text.Trim();
					string prodid = e.Item.Cells[8].Text.Trim();
					string tnseq1 = e.Item.Cells[10].Text.Trim();

					
					if(status=="DELETE")
					{
						LBL_SAVEMODE.Text = "1";
						break;
					}
					try
					{
						DDL_PROGRAM.SelectedValue = progid;
						GetProd(progid);
						DDL_PRODID.SelectedValue = prodid;
						GetTn(prodid,progid);
						DDL_TNSEQ.SelectedValue = tnseq1;
						/*
						DDL_PRODID.SelectedValue = e.Item.Cells[8].Text.Trim();
						DDL_TNSEQ.SelectedValue = e.Item.Cells[10].Text.Trim();
						DDL_PROGRAM.SelectedValue = e.Item.Cells[9].Text.Trim();
						*/
					}
					catch{}
					TXT_COEFF.Text = e.Item.Cells[5].Text.Trim();
					TXT_YEARSEQ.Text = e.Item.Cells[4].Text.Trim();
					DDL_PRODID.Enabled = false;
					DDL_TNSEQ.Enabled = false;
					DDL_PROGRAM.Enabled = false;
					TXT_YEARSEQ.ReadOnly = true;
					LBL_ID.Text = e.Item.Cells[1].Text.Trim();

					if(e.Item.Cells[6].Text=="UPDATE")
					{
						LBL_SAVEMODE.Text = "0";
					}
					if(e.Item.Cells[6].Text=="INSERT")
					{
						LBL_SAVEMODE.Text = "1";
					}
					
					cleansTextBox(TXT_COEFF);
					cleansTextBox(TXT_YEARSEQ);
					break;
				case "delete":
					conn.QueryString = "EXEC PARAM_GENERAL_GPM_COEFFICIENT '2','" + e.Item.Cells[1].Text.Trim() +"','', '','','','','',''  " ;
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
			ClearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			string seqid; 
			if (LBL_SAVEMODE.Text.Trim() == "1" && LBL_ID.Text=="") //input baru
			{
				conn.QueryString = "select * from GPM_COEFFICIENT where PRODUCTID='"+DDL_PRODID.SelectedValue+"' and "+
					"TN_SEQ ='"+DDL_TNSEQ.SelectedValue+"' and YEARSEQ ='"+TXT_YEARSEQ.Text+"'";
				conn.ExecuteQuery();
				if(conn.GetRowCount() > 0)
				{
					GlobalTools.popMessage(this,"GPM Coefficient has already been used!");
					return;
				}

				//get seqid
				conn.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQID from TGPM_COEFFICIENT";
				conn.ExecuteQuery();
				seqid = conn.GetFieldValue("SEQID");
				
				conn.QueryString = "EXEC PARAM_GENERAL_GPM_COEFFICIENT '1','" + seqid +"','"+
					DDL_PRODID.SelectedValue+ "', '" + int.Parse(DDL_TNSEQ.SelectedValue) + "','" + 
					int.Parse(TXT_YEARSEQ.Text)+"','"+GlobalTools.ConvertFloat(TXT_COEFF.Text)+"','"+LBL_SAVEMODE.Text+"','','' " ;
				conn.ExecuteQuery();
			}		
			else if(LBL_ID.Text!="")
			{
				//conn.QueryString="select * from TGPM_COEFFICIENT where PRODUCTID='"+DDL_PRODID.SelectedValue+"' and "+
				//	"TN_SEQ ='"+DDL_TNSEQ.SelectedValue+"' and YEARSEQ ='"+TXT_YEARSEQ.Text+"'";
				//conn.ExecuteQuery();
				//if(conn.GetRowCount()!=0)
				
					conn.QueryString = "EXEC PARAM_GENERAL_GPM_COEFFICIENT '6','" + LBL_ID.Text +"','"+
						DDL_PRODID.SelectedValue+ "', '" + int.Parse(DDL_TNSEQ.SelectedValue) + "','" + 
						int.Parse(TXT_YEARSEQ.Text)+"','"+GlobalTools.ConvertFloat(TXT_COEFF.Text)+"','"+LBL_SAVEMODE.Text+"','','' " ;
					conn.ExecuteQuery();
				
			}
			else if(LBL_ID.Text == "")
			{
				conn.QueryString="select * from TGPM_COEFFICIENT where PRODUCTID='"+DDL_PRODID.SelectedValue+"' and "+
					"TN_SEQ ='"+DDL_TNSEQ.SelectedValue+"' and YEARSEQ ='"+TXT_YEARSEQ.Text+"'";
				conn.ExecuteQuery();
				if(conn.GetRowCount()==0)
				{
					conn.QueryString="select isnull(max(convert(int, SEQ_ID)), 0)+1 SEQID from TGPM_COEFFICIENT";
					conn.ExecuteQuery();
					LBL_ID.Text = conn.GetFieldValue("SEQID");

					conn.QueryString = "EXEC PARAM_GENERAL_GPM_COEFFICIENT '1','" + LBL_ID.Text +"','"+
						DDL_PRODID.SelectedValue+ "', '" + int.Parse(DDL_TNSEQ.SelectedValue) + "','" + 
						int.Parse(TXT_YEARSEQ.Text)+"','"+GlobalTools.ConvertFloat(TXT_COEFF.Text)+"','"+LBL_SAVEMODE.Text+"','','' " ;
					conn.ExecuteQuery();
				}
				else
				{
					LBL_ID.Text = conn.GetFieldValue("SEQ_ID");

					conn.QueryString = "EXEC PARAM_GENERAL_GPM_COEFFICIENT '6','" + LBL_ID.Text +"','"+
						DDL_PRODID.SelectedValue+ "', '" + int.Parse(DDL_TNSEQ.SelectedValue) + "','" + 
						int.Parse(TXT_YEARSEQ.Text)+"','"+GlobalTools.ConvertFloat(TXT_COEFF.Text)+"','"+LBL_SAVEMODE.Text+"','','' " ;
					conn.ExecuteQuery();
				}
				
			}
			
			bindData2();
			ClearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		}

		protected void DDL_PROGRAM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string progid = DDL_PROGRAM.SelectedValue;
			GetProd(progid);
			/*conn.QueryString = "select a.productid,b.productname "+ 
					"from programpro a left join tproduct b on a.productid=b.productid "+
					"where a.pr_code='"+progid+"'";
			conn.ExecuteQuery();
			DDL_PRODID.Items.Clear();
			DDL_PRODID.Items.Add(new ListItem("--select--",""));
			for (int i=0; i<conn.GetRowCount(); i++)
			{
				DDL_PRODID.Items.Add(new ListItem(conn.GetFieldValue(i,1),conn.GetFieldValue(i,0)));
			}*/
		}

	}
}
