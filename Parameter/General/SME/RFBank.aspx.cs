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

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFBank.
	/// </summary>
	public partial class RFBank : System.Web.UI.Page
	{
		protected Tools tool = new Tools();
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
		private string scoreruleflag;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack) 
			{
				LBL_SAVEMODE.Text = "1";

				viewExistingData();
				viewPendingData();
			}

			BTN_SAVE.Attributes.Add("onclick","if(!cek_mandatory(document.Form1)){return false;};");
		}

		private void viewExistingData() 
		{
			conn.QueryString = "select * from VW_PARAM_RFBANK_VIEWEXISTING";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG_EXISTING.DataSource = dt;
			try 
			{
				DG_EXISTING.DataBind();
			} 
			catch 
			{
				DG_EXISTING.CurrentPageIndex = 0;
				DG_EXISTING.DataBind();
			}
		}

		private void viewPendingData() 
		{
			conn.QueryString = "select * from VW_PARAM_RFBANK_VIEWPENDING";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG_PENDING.DataSource = dt;
			try 
			{
				DG_PENDING.DataBind();
			} 
			catch 
			{
				DG_PENDING.CurrentPageIndex = 0;
				DG_PENDING.DataBind();
			}
		}

		private void ClearControls()
		{
			TXT_ID.Text = "";
			TXT_DESC.Text = "";
			CHK_SCORERULEFLAG.Checked = false;
			LBL_SAVEMODE.Text = "1";
		}

		private void ReadOnlyControl(bool isReadOnly) 
		{
			TXT_ID.ReadOnly = isReadOnly;
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
			this.DG_EXISTING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG_EXISTING_ItemCommand);
			this.DG_EXISTING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_EXISTING_PageIndexChanged);
			this.DG_EXISTING.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DG_EXISTING_ItemDataBound);
			this.DG_PENDING.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG_PENDING_ItemCommand);
			this.DG_PENDING.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_PENDING_PageIndexChanged);
			this.DG_PENDING.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DG_PENDING_ItemDataBound);

		}
		#endregion

		private void DG_EXISTING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_EXISTING.CurrentPageIndex = e.NewPageIndex;
			viewExistingData();
		}

		private void DG_PENDING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_PENDING.CurrentPageIndex = e.NewPageIndex;
			viewPendingData();
		}

		protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			ClearControls();
			ReadOnlyControl(false);
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (CHK_SCORERULEFLAG.Checked == true)
					scoreruleflag = "1";
				else
					scoreruleflag = "0";

				conn.QueryString = "exec PARAM_RFBANK_MAKER '" + 
					TXT_ID.Text + "', '" +
					TXT_DESC.Text + "', '" +
					scoreruleflag + "', '" +
					LBL_SAVEMODE.Text + "'";
				conn.ExecuteQuery();

				viewPendingData();
				ClearControls();
				ReadOnlyControl(false);
			}
			catch (Exception ex)
			{
				Response.Write("<!--" + ex.Message + "-->");
				string errmsg = ex.Message.Replace("'","");
				if (errmsg.IndexOf("Last Query:") > 0)		
					errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
				GlobalTools.popMessage(this,errmsg);
			}
		}

		private void DG_EXISTING_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.AlternatingItem:
				case ListItemType.EditItem:
				case ListItemType.Item:
				case ListItemType.SelectedItem:
					try
					{
						LinkButton lbedit = (LinkButton)e.Item.Cells[5].FindControl("lb_edit1");
						LinkButton lbdel = (LinkButton)e.Item.Cells[5].FindControl("lb_delete1");
						LinkButton lbundel = (LinkButton)e.Item.Cells[5].FindControl("lb_undelete1");

						if (e.Item.Cells[4].Text == "1")
						{
							e.Item.Cells[0].ForeColor = System.Drawing.Color.Black;
							e.Item.Cells[1].ForeColor = System.Drawing.Color.Black;
							e.Item.Cells[3].ForeColor = System.Drawing.Color.Black;
							lbedit.Visible = true;
							lbdel.Visible = true;
							lbundel.Visible = false;
						}
						else
						{
							e.Item.Cells[0].ForeColor = System.Drawing.Color.Red;
							e.Item.Cells[1].ForeColor = System.Drawing.Color.Red;
							e.Item.Cells[3].ForeColor = System.Drawing.Color.Red;
							lbedit.Visible = false;
							lbdel.Visible = false;
							lbundel.Visible = true;
						}
					} 
					catch {}
					break;
				case ListItemType.Footer:
					break;
				default:
					break;
			}
		}

		private void DG_PENDING_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.AlternatingItem:
				case ListItemType.EditItem:
				case ListItemType.Item:
				case ListItemType.SelectedItem:
					try
					{
						LinkButton lbedit = (LinkButton)e.Item.Cells[6].FindControl("lb_edit2");

						if ((e.Item.Cells[4].Text == "3") || (e.Item.Cells[4].Text == "4"))
						{
							lbedit.Visible = false;
						}
						else
						{
							lbedit.Visible = true;
						}
					} 
					catch {}
					break;
				case ListItemType.Footer:
					break;
				default:
					break;
			}
		}

		private void DG_EXISTING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					ClearControls();
					ReadOnlyControl(true);
					TXT_ID.Text = e.Item.Cells[0].Text.Trim();
					TXT_DESC.Text = e.Item.Cells[1].Text.Trim();
					scoreruleflag = e.Item.Cells[2].Text.Trim();
					if (scoreruleflag == "1")
						CHK_SCORERULEFLAG.Checked = true;
					else
						CHK_SCORERULEFLAG.Checked = false;
					LBL_SAVEMODE.Text = "2";
					break;

				case "delete":
					try
					{
						LBL_SAVEMODE.Text = "3";
						
						conn.QueryString = "exec PARAM_RFBANK_MAKER '" + 
							e.Item.Cells[0].Text.Trim() + "', '" +
							e.Item.Cells[1].Text.Trim() + "', '" +
							e.Item.Cells[2].Text.Trim() + "', '" +
							LBL_SAVEMODE.Text + "'";
						conn.ExecuteQuery();

						viewPendingData();
						ClearControls();
						ReadOnlyControl(false);
					}
					catch (Exception ex)
					{
						Response.Write("<!--" + ex.Message + "-->");
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)		
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this,errmsg);
					}
					break;

				case "undelete":
					try
					{
						LBL_SAVEMODE.Text = "4";

						conn.QueryString = "exec PARAM_RFBANK_MAKER '" + 
							e.Item.Cells[0].Text.Trim() + "', '" +
							e.Item.Cells[1].Text.Trim() + "', '" +
							e.Item.Cells[2].Text.Trim() + "', '" +
							LBL_SAVEMODE.Text + "'";
						conn.ExecuteQuery();

						viewPendingData();
						ClearControls();
						ReadOnlyControl(false);
					}
					catch (Exception ex)
					{
						Response.Write("<!--" + ex.Message + "-->");
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)		
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this,errmsg);
					}
					break;

				default:
					break;
			}
		}

		private void DG_PENDING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					if ((e.Item.Cells[5].Text == "Delete") || (e.Item.Cells[5].Text == "UnDelete"))
					{
						return;
					}
					else
					{
						ClearControls();
						ReadOnlyControl(true);
						TXT_ID.Text = e.Item.Cells[0].Text.Trim();
						TXT_DESC.Text = e.Item.Cells[1].Text.Trim();
						scoreruleflag = e.Item.Cells[2].Text.Trim();
						if (scoreruleflag == "1")
							CHK_SCORERULEFLAG.Checked = true;
						else
							CHK_SCORERULEFLAG.Checked = false;
						LBL_SAVEMODE.Text = "2";
					}
					break;

				case "delete":
					try
					{
						LBL_SAVEMODE.Text = "5";

						conn.QueryString = "exec PARAM_RFBANK_MAKER '" + 
							e.Item.Cells[0].Text.Trim() + "', '" +
							e.Item.Cells[1].Text.Trim() + "', '" +
							e.Item.Cells[2].Text.Trim() + "', '" +
							LBL_SAVEMODE.Text + "'";
						conn.ExecuteQuery();

						viewPendingData();
						ClearControls();
						ReadOnlyControl(false);
					}
					catch (Exception ex)
					{
						Response.Write("<!--" + ex.Message + "-->");
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)		
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this,errmsg);
					}
					break;

				default:
					break;
			}
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			/*
            string classid="";
			try 
			{
				classid=Request.QueryString["classid"].ToString();
			}
			catch{ classid="";}
			
			if ((classid.Equals("01")) || (classid.ToString().Trim()=="01") )
				Response.Redirect("../../HostParam.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+" ");
			else
				Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+" ");
            */
		}

	}
}
