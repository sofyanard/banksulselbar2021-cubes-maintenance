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
	/// Summary description for CUBESTplScoreAppr.
	/// </summary>
	public partial class CUBESTplScoreAppr : System.Web.UI.Page
	{
		protected Connection conn,conn2;
		protected string mid;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn2 = new Connection((string) Session["ConnString"]);
			SetDBConn2();
			if (!IsPostBack)
			{
				bindData1();
				RBL_PARAM.SelectedValue = "0";
				RBL_PARAM_LOC.SelectedValue = "0";
				bindDataMix();
			}
		}

		private void SetDBConn2()
		{
			mid = Request.QueryString["ModuleId"];

			conn2.QueryString = "select * from RFMODULE where MODULEID= '"+mid+"'";
			conn2.ExecuteQuery();
			conn = new Connection ("Data Source=" + conn2.GetFieldValue("DB_IP") + ";Initial Catalog=" + conn2.GetFieldValue("DB_NAMA") + ";uid=" +conn2.GetFieldValue("DB_LOGINID")+ ";pwd=" +conn2.GetFieldValue("DB_LOGINPWD")+ ";Pooling=true");
		}

		private void bindData1()
		{
			conn.QueryString="SELECT * FROM VW_MANDIRI_SCORING_TTEMPLATE";
			conn.ExecuteQuery();
			this.DGR_APPR.DataSource = conn.GetDataTable().Copy();
			try 
			{
				this.DGR_APPR.DataBind();
			}
			catch 
			{
				this.DGR_APPR.CurrentPageIndex = this.DGR_APPR.PageCount - 1;
				this.DGR_APPR.DataBind();
			}
		}

		private void performRequest(int row)
		{
			try 
			{
				string userid = Session["UserID"].ToString();
				string groupid = Session["GroupID"].ToString();
				string TPLID = DGR_APPR.Items[row].Cells[0].Text.Trim();
				conn.QueryString="select * from VW_MANDIRI_SCORING_TTEMPLATE where SCOTPLID='"+TPLID+"'";
				conn.ExecuteQuery();
				string status = conn.GetFieldValue("CH_STA");
				string TPLDESC = conn.GetFieldValue("SCOTPLDESC");
								
				if (status.Equals("0"))
				{
					conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_APPROVAL '3','"+TPLID+"','" +
						TPLDESC+ "', '" +status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}

				if (status.Equals("1"))
				{
					conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_APPROVAL '4','"+TPLID+"','" +
						TPLDESC+ "', '" +status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}
				if (status.Equals("2"))
				{
					conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_APPROVAL '5','"+TPLID+"','" +
						TPLDESC+ "', '" +status+"','"+userid+"','"+groupid+"'" ;
					conn.ExecuteNonQuery();
				}

				conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_APPROVAL '2','"+
					TPLID+"', '', '', '', ''" ;
				conn.ExecuteNonQuery();
				
			} 
			catch (Exception p)
			{
				GlobalTools.popMessage(this,p.Message);
			}

		}

		private void deleteData(int row)
		{
			try 
			{
				string TPLID = DGR_APPR.Items[row].Cells[0].Text.Trim();

				conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_APPROVAL '2','"+
					TPLID+"', '', '', '', ''" ;
				conn.ExecuteNonQuery();
			} 
			catch {}
		}

		private string ExchgPoint(string str)
		{
			return str.Replace(".",",").Trim();
		}

		private void bindDataMix()
		{
			if (RBL_PARAM.SelectedValue != "1")
			{
				tr_grd_value.Visible = true;
				tr_grd_other.Visible = false;

				conn.QueryString="SELECT * FROM VW_TMANDIRI_PARAM_VALUE WHERE 1=1 ";

				if (RBL_PARAM_LOC.SelectedValue != "1")
					conn.QueryString += "AND PARAM_PRM = '" + RBL_PARAM_LOC.SelectedValue + "' ";
				else
					conn.QueryString += "AND PARAM_PRM  = '" + RBL_PARAM_LOC.SelectedValue + "' ";

				conn.QueryString += "ORDER BY SCOTPLID ASC";
				conn.ExecuteQuery();
				

				DataTable data = new DataTable();
				data = conn.GetDataTable().Copy();
				Datagrid1.DataSource = data;
		
				try
				{
					Datagrid1.DataBind();
				} 
				catch 
				{
					this.Datagrid1.CurrentPageIndex = Datagrid1.PageCount - 1;
					Datagrid1.DataBind();
				}

		
				for (int i=0; i < Datagrid1.Items.Count; i++)
				{
					Datagrid1.Items[i].Cells[5].Text	= ExchgPoint(Datagrid1.Items[i].Cells[5].Text);
					Datagrid1.Items[i].Cells[6].Text	= ExchgPoint(Datagrid1.Items[i].Cells[6].Text);
				}
			}
			else
			{
				tr_grd_other.Visible = true;
				tr_grd_value.Visible = false;

				conn.QueryString="SELECT * FROM VW_TMANDIRI_PARAM_OTHER WHERE 1=1 ";

				if (RBL_PARAM_LOC.SelectedValue != "1")
					conn.QueryString += "AND PARAM_PRM = '" + RBL_PARAM_LOC.SelectedValue + "' ";
				else
					conn.QueryString += "AND PARAM_PRM  = '" + RBL_PARAM_LOC.SelectedValue + "' ";


				conn.QueryString += "ORDER BY SCOTPLID ASC";
				conn.ExecuteQuery();

				

				DataTable data = new DataTable();
				data = conn.GetDataTable().Copy();
				Datagrid2.DataSource = data;
		
				try
				{
					Datagrid2.DataBind();
				} 
				catch 
				{
					this.Datagrid2.CurrentPageIndex = Datagrid2.PageCount - 1;
					Datagrid2.DataBind();
				}
			}
		}

		private void performRequestValue(int row, char appr_sta, string userid)
		{
			try 
			{
				string paramid = Datagrid1.Items[row].Cells[1].Text.Trim();
				string requestid = Datagrid1.Items[row].Cells[0].Text.Trim();
				string paramvalueid = Datagrid1.Items[row].Cells[2].Text.Trim();
				string scotplid = Datagrid1.Items[row].Cells[0].Text.Trim();
				string seqid = Datagrid1.Items[row].Cells[3].Text.Trim();
				string useridvalue = Session["UserID"].ToString();
							
				conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_VALUE_APPR '"+paramid+"', '"+
					requestid+"' , '"+paramvalueid+"' , '"+scotplid+"' , '"+seqid+"' , '"+appr_sta+"', '"+useridvalue+"'";

				conn.ExecuteNonQuery();
			} 
			catch (Exception ex)
			{
				Response.Write("<!-- " + ex.Message.Replace("'","") + " -->\n");
				GlobalTools.popMessage(this,"Error on Stored Procedure!");			
			}
		}

		private void performRequestOther(int row, char appr_sta, string userid)
		{
			try 
			{
				string paramid = Datagrid2.Items[row].Cells[1].Text.Trim();
				string requestid = Datagrid2.Items[row].Cells[0].Text.Trim();
				string paramotherid = Datagrid2.Items[row].Cells[2].Text.Trim();
				string scotplid = Datagrid2.Items[row].Cells[0].Text.Trim();
				string seqid = Datagrid2.Items[row].Cells[3].Text.Trim();
				string useridother = Session["UserID"].ToString();
							
				conn.QueryString = "EXEC PARAM_TEMPLATE_SCORING_OTHER_APPR '"+paramid+"', '"+
					requestid+"' , '"+paramotherid+"' , '"+scotplid+"' , '"+seqid+"' , '"+appr_sta+"', '"+useridother+"'";

				conn.ExecuteNonQuery();
			} 
			catch (Exception ex)
			{
				Response.Write("<!-- " + ex.Message.Replace("'","") + " -->\n");
				GlobalTools.popMessage(this,"Error on Stored Procedure!");			
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
			this.DGR_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DGR_APPR_ItemCommand);
			this.DGR_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGR_APPR_PageIndexChanged);
			this.Datagrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_ItemCommand);
			this.Datagrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid1_PageIndexChanged);
			this.Datagrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_ItemCommand);
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);

		}
		#endregion

		private void DGR_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGR_APPR.CurrentPageIndex = e.NewPageIndex;
			bindData1();
		}

		private void DGR_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Pending");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DGR_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
								rbB = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject"),
								rbC = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Pending");
							rbA.Checked = false;
							rbB.Checked = false;
							rbC.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					// Do nothing.
					break;
			}
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < DGR_APPR.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Approve"),
						rbR = (RadioButton) DGR_APPR.Items[i].FindControl("rdo_Reject");
					if (rbA.Checked)
					{
						performRequest(i);
					}
					else if (rbR.Checked)
					{
						deleteData(i);
					}
				} 
				catch {}
			}
			bindData1();
		}

		protected void RBL_PARAM_LOC_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			bindDataMix();
		}

		protected void RBL_PARAM_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			bindDataMix();
		}

		private void Datagrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.Datagrid1.CurrentPageIndex = e.NewPageIndex;
			bindDataMix();
		}

		private void Datagrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.Datagrid2.CurrentPageIndex = e.NewPageIndex;
			bindDataMix();
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			string scidvalue = Session["UserID"].ToString();

			for (int i = 0; i < Datagrid1.Items.Count; i++)
			{
				try
				{
					RadioButton rbA = (RadioButton) Datagrid1.Items[i].FindControl("rd_Approve1"),
						rbR = (RadioButton) Datagrid1.Items[i].FindControl("rd_Reject1");
					if (rbA.Checked)
						performRequestValue(i, '1', scidvalue);
					else if (rbR.Checked)
						performRequestValue(i, '0', scidvalue);
				} 
				catch {}
			}
			bindDataMix();
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			string scidother = Session["UserID"].ToString();

			for (int i = 0; i < Datagrid2.Items.Count; i++)
			{		
				
				RadioButton rbA = (RadioButton) Datagrid2.Items[i].FindControl("rd_Approve2"),
					rbR = (RadioButton) Datagrid2.Items[i].FindControl("rd_Reject2");
				if (rbA.Checked)
					performRequestOther(i, '1', scidother);
				else if (rbR.Checked)
					performRequestOther(i, '0', scidother);
			}

			bindDataMix();
		}

		private void Datagrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < Datagrid1.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) Datagrid1.Items[i].FindControl("rd_Approve1"),
								rbB = (RadioButton) Datagrid1.Items[i].FindControl("rd_Reject1"),
								rbC = (RadioButton) Datagrid1.Items[i].FindControl("rd_Pending1");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < Datagrid1.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) Datagrid1.Items[i].FindControl("rd_Approve1"),
								rbB = (RadioButton) Datagrid1.Items[i].FindControl("rd_Reject1"),
								rbC = (RadioButton) Datagrid1.Items[i].FindControl("rd_Pending1");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < Datagrid1.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) Datagrid1.Items[i].FindControl("rd_Approve1"),
								rbB = (RadioButton) Datagrid1.Items[i].FindControl("rd_Reject1"),
								rbC = (RadioButton) Datagrid1.Items[i].FindControl("rd_Pending1");
							rbA.Checked = false;
							rbB.Checked = false;
							rbC.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					// Do nothing.
					break;
			}
		}

		private void Datagrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < Datagrid2.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) Datagrid2.Items[i].FindControl("rd_Approve2"),
								rbB = (RadioButton) Datagrid2.Items[i].FindControl("rd_Reject2"),
								rbC = (RadioButton) Datagrid2.Items[i].FindControl("rd_Pending2");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allRejc":
					for (i = 0; i < Datagrid2.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) Datagrid2.Items[i].FindControl("rd_Approve2"),
								rbB = (RadioButton) Datagrid2.Items[i].FindControl("rd_Reject2"),
								rbC = (RadioButton) Datagrid2.Items[i].FindControl("rd_Pending2");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < Datagrid2.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) Datagrid2.Items[i].FindControl("rd_Approve2"),
								rbB = (RadioButton) Datagrid2.Items[i].FindControl("rd_Reject2"),
								rbC = (RadioButton) Datagrid2.Items[i].FindControl("rd_Pending2");
							rbA.Checked = false;
							rbB.Checked = false;
							rbC.Checked = true;
						} 
						catch {}
					}
					break;
				default:
					// Do nothing.
					break;
			}
		}
	}
}
