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
using System.Configuration;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.Consumer
{
	/// <summary>
	/// Summary description for ApprovalRoutingParam.
	/// </summary>
	public partial class ApprovalRoutingParam : System.Web.UI.Page
	{
		protected Connection conn,conn2;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			InitialCon();
			if(!IsPostBack)
			{
				DDL_Ccode.Items.Add(new ListItem("--SELECT--",""));
				conn.QueryString="select condition_code from approval_condition ";
				conn.ExecuteQuery();
				for (int i=0; i<conn.GetRowCount(); i++)
				{
					DDL_Ccode.Items.Add(new ListItem(conn.GetFieldValue(i,0),conn.GetFieldValue(i,0)));
				}
				GlobalTools.fillRefList(DDL_Group,"select groupid,sg_grpname from scgroup where sg_aprvtrack is not null and sg_aprvtrack<>''",false,conn);
				BindData1();
				BindData2();
				TXT_SEQ.ReadOnly = true;
			}
			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;}");
		}

		private void InitialCon()
		{
			conn2.QueryString = "select * from vw_getconn where moduleid = '"+Request.QueryString["ModuleId"]+"'";
			conn2.ExecuteQuery();
			string DB_NAMA = conn2.GetFieldValue("DB_NAMA");
			string DB_IP = conn2.GetFieldValue("DB_IP");
			string DB_LOGINID = conn2.GetFieldValue("DB_LOGINID");
			string DB_LOGINPWD = conn2.GetFieldValue("DB_LOGINPWD");
			conn = new Connection ("Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAMA + ";uid=" + DB_LOGINID + ";pwd=" + DB_LOGINPWD + ";Pooling=true");
		}

		private void BindData1()
		{
			string chk1;
			CheckBox chk;

			conn.QueryString="select *,sg_grpname from APPROVAL_ROUTING a "+
				"left join scgroup b on a.routing_group=b.groupid order by condition_code,formula_code,routing_seq";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_EXISTING.DataSource = dt;

			try
			{
				DGR_EXISTING.DataBind();
				for (int i=0; i<DGR_EXISTING.Items.Count; i++)
				{
					chk1 = DGR_EXISTING.Items[i].Cells[6].Text;
					chk = (CheckBox)DGR_EXISTING.Items[i].Cells[4].FindControl("chk_RFoureyes");
					if (chk1 =="1")
						chk.Checked = true;
					else
						chk.Checked = false;
				}
			}
			catch 
			{
				DGR_EXISTING.CurrentPageIndex = DGR_EXISTING.PageCount - 1;
				DGR_EXISTING.DataBind();
			}

			conn.ClearData();
		}

		private void BindData2()
		{
			string chk1;
			CheckBox chk;
			 
			conn.QueryString="select *,sg_grpname, "+  
				"STATUS = CASE PENDINGSTATUS WHEN '1' THEN 'INSERT' "+
				"WHEN '2' THEN 'UPDATE' WHEN '3' THEN 'DELETE' END "+
				" from PENDING_APPROVAL_ROUTING a "+
				"left join scgroup b on a.routing_group=b.groupid";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DGR_REQUEST.DataSource = dt;
			
			try
			{
				DGR_REQUEST.DataBind();
				for (int i=0; i<DGR_REQUEST.Items.Count; i++)
				{
					chk1 = DGR_REQUEST.Items[i].Cells[7].Text;
					chk = (CheckBox)DGR_REQUEST.Items[i].Cells[4].FindControl("chk_RFoureyes");
					if (chk1 =="1")
						chk.Checked = true;
					else
						chk.Checked = false;
				}
			}
			catch 
			{
				DGR_REQUEST.CurrentPageIndex = DGR_REQUEST.PageCount - 1;
				DGR_REQUEST.DataBind();
			}

			conn.ClearData();
		}

		private void ClearEditBoxes()
		{
			DDL_Ccode.Enabled = true; 
			DDL_Fcode.Enabled = true;
			TXT_SEQ.Enabled   = true;
			DDL_Ccode.ClearSelection();
			DDL_Fcode.ClearSelection();
			DDL_Group.ClearSelection();
			TXT_SEQ.Text="";
			chk_foureyes.Checked=false;
 
			LBL_SAVE.Text = "1";
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

		protected void DDL_Ccode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TXT_SEQ.Text="";
			string ccode = DDL_Ccode.SelectedValue;
			fillFcode(ccode);
		}

		private void fillFcode(string ccode)
		{
			DDL_Fcode.Items.Clear();
			DDL_Fcode.Items.Add(new ListItem("--SELECT--",""));
			conn.QueryString = " select FORMULA_CODE from APPROVAL_FORMULA "+
				"where CONDITION_CODE ='"+ccode+"'";
			conn.ExecuteQuery();
			for (int i=0; i<conn.GetRowCount(); i++)
			{
				DDL_Fcode.Items.Add(new ListItem(conn.GetFieldValue(i,0),conn.GetFieldValue(i,0)));
			}
		}

		protected void DDL_Fcode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string Ccode = DDL_Ccode.SelectedValue;
			string Fcode = DDL_Fcode.SelectedValue;
			conn.QueryString = "select ROUTING_SEQ from APPROVAL_ROUTING "+
				"where CONDITION_CODE='"+Ccode+"' and FORMULA_CODE='"+Fcode+"'";
			conn.ExecuteQuery();
			if(conn.GetRowCount()!=0)
			{
				conn.QueryString = "select max(ROUTING_SEQ)+1 ROUTING_SEQ from APPROVAL_ROUTING "+
					"where condition_code='"+Ccode+"' and formula_code='"+Fcode+"'";
				conn.ExecuteQuery();
				TXT_SEQ.Text = conn.GetFieldValue("ROUTING_SEQ");
			}
			else if (conn.GetRowCount()==0)
			{
				TXT_SEQ.Text = "1";
			}
		}

		private void DGR_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_EXISTING.CurrentPageIndex = e.NewPageIndex;
			BindData1(); 
		}

		private void DGR_REQUEST_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DGR_REQUEST.CurrentPageIndex = e.NewPageIndex;
			BindData2();
		}

		private void DGR_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string Ccode,Fcode,Seq,Group,Foureyes; 
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":
					LBL_SAVE.Text="3";
					Ccode = e.Item.Cells[0].Text.Trim();
					Fcode = e.Item.Cells[1].Text.Trim();
					Seq = e.Item.Cells[2].Text; 
					Group = e.Item.Cells[7].Text; 
					Foureyes = e.Item.Cells[6].Text;
					
					conn.QueryString = "select * from PENDING_APPROVAL_ROUTING where CONDITION_CODE = '"+Ccode+"'"+
						"and FORMULA_CODE = '"+Fcode+"' and ROUTING_SEQ='"+Seq+"'"; 
					conn.ExecuteQuery();

					if(conn.GetRowCount() != 0)
					{
						conn.QueryString="update PENDING_APPROVAL_ROUTING set PENDINGSTATUS ='3' where CONDITION_CODE = '"+Ccode+"'"+
							"and FORMULA_CODE = '"+Fcode+"' and ROUTING_SEQ='"+Seq+"'"; 
						conn.ExecuteQuery();
						BindData2();
					}
					else
					{
						try
						{
							conn.QueryString = "insert into PENDING_APPROVAL_ROUTING "+
								"values ('"+Ccode+"','"+Fcode+"','"+Seq+"','"+Group+"','"+Foureyes+"','"+LBL_SAVE.Text+"')";
							conn.ExecuteQuery();
						}
						catch{ }
		
						BindData2();
					}
					break;

				case "edit":
					LBL_SAVE.Text="2";
					Ccode = e.Item.Cells[0].Text.Trim();
					Fcode = e.Item.Cells[1].Text.Trim();
					Seq = e.Item.Cells[2].Text; 
					Group = e.Item.Cells[7].Text; 
					Foureyes = e.Item.Cells[6].Text;

					try
					{
						DDL_Ccode.SelectedValue = Ccode;
						fillFcode(Ccode);
					}
					catch{ } 	
					try
					{
						DDL_Fcode.SelectedValue = Fcode;
					}
					catch{ } 
					try
					{
						DDL_Group.SelectedValue = Group;
					}
					catch{ } 

					TXT_SEQ.Text = Seq;
					if (Foureyes == "1")
						chk_foureyes.Checked = true;
					else if (Foureyes == "0")
						chk_foureyes.Checked = false;

					DDL_Ccode.Enabled = false;
					DDL_Fcode.Enabled = false;
					TXT_SEQ.Enabled = false;
					
					break;
			}		
		}

		private void DGR_REQUEST_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string Ccode,Fcode,Seq,Group,Foureyes; 
			switch(((LinkButton)e.CommandSource).CommandName.ToLower())
			{
				case "delete":				
					Ccode = e.Item.Cells[0].Text.Trim();
					Fcode = e.Item.Cells[1].Text.Trim();
					Seq = e.Item.Cells[2].Text; 
					Group = e.Item.Cells[9].Text; 
					Foureyes = e.Item.Cells[7].Text; 

					conn.QueryString = "delete from PENDING_APPROVAL_ROUTING where CONDITION_CODE = '"+Ccode+"'"+
						"and FORMULA_CODE = '"+Fcode+"' and ROUTING_SEQ='"+Seq+"'";  
					conn.ExecuteQuery();

					BindData2();
					break;

				case "edit":
					Ccode = e.Item.Cells[0].Text.Trim();
					Fcode = e.Item.Cells[1].Text.Trim();
					Seq = e.Item.Cells[2].Text; 
					Group = e.Item.Cells[9].Text; 
					Foureyes = e.Item.Cells[7].Text; 

					if(e.Item.Cells[8].Text.Trim() == "3")
					{
						LBL_SAVE.Text = "1";
					}
					else
					{
						LBL_SAVE.Text="2";

						try
						{
							DDL_Ccode.SelectedValue = Ccode;
						}
						catch{ } 	
						try
						{
							DDL_Fcode.SelectedValue = Fcode;
						}
						catch{ } 
						try
						{
							DDL_Group.SelectedValue = Group;
						}
						catch{ } 						

						TXT_SEQ.Text = Seq;
						if (Foureyes == "1")
							chk_foureyes.Checked = true;
						else if (Foureyes == "0")
							chk_foureyes.Checked = false;

						DDL_Ccode.Enabled = false;
						DDL_Fcode.Enabled = false;
						TXT_SEQ.Enabled = false;
					}
					break;
			}
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearEditBoxes();
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			int hit = 0;
			string chk;
			if (chk_foureyes.Checked==true)
				chk = "1";
			else
				chk = "0";

			conn.QueryString = "select * from PENDING_APPROVAL_ROUTING where CONDITION_CODE = '"+DDL_Ccode.SelectedValue+"'"+
				"and FORMULA_CODE = '"+DDL_Fcode.SelectedValue+"' and ROUTING_SEQ='"+TXT_SEQ.Text+"'";  
			conn.ExecuteQuery();

			hit = conn.GetRowCount();

			if((hit != 0) && (LBL_SAVE.Text != "1"))//update
			{
				conn.QueryString = "update PENDING_APPROVAL_ROUTING set ROUTING_GROUP = '"+DDL_Group.SelectedValue+"',PENDINGSTATUS ='2', "+
					"ROUTING_FOUREYES='"+chk+"' "+
					"where CONDITION_CODE = '"+DDL_Ccode.SelectedValue+"'"+
					"and FORMULA_CODE = '"+DDL_Fcode.SelectedValue+"' and ROUTING_SEQ='"+TXT_SEQ.Text+"'";   
				conn.ExecuteQuery();	

				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "2"))//update
			{
				conn.QueryString = "insert into PENDING_APPROVAL_ROUTING "+
					"values ('"+DDL_Ccode.SelectedValue+"','"+DDL_Fcode.SelectedValue+"','"+TXT_SEQ.Text+"',"+
					"'"+DDL_Group.SelectedValue+"','"+chk+"','2')";
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit == 0) && (LBL_SAVE.Text == "1"))//insert
			{
				conn.QueryString = "insert into PENDING_APPROVAL_ROUTING "+
					"values ('"+DDL_Ccode.SelectedValue+"','"+DDL_Fcode.SelectedValue+"','"+TXT_SEQ.Text+"',"+
					"'"+DDL_Group.SelectedValue+"','"+chk+"','1')";
				conn.ExecuteQuery();
 
				ClearEditBoxes(); 
			}
			else if((hit != 0) && (LBL_SAVE.Text == "1"))
			{
				GlobalTools.popMessage(this,"Duplikasi data !");
				return;
			}

			conn.ClearData();
 	
			BindData2();

			LBL_SAVE.Text = "1"; 
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../GeneralParamAll.aspx?ModuleId=40");
		}


	}
}
