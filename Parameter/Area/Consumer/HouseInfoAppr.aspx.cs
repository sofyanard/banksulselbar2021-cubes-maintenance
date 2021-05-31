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

namespace CuBES_Maintenance.Parameter.Area.Consumer
{
	/// <summary>
	/// Summary description for HouseInfoAppr.
	/// </summary>
	public partial class HouseInfoAppr : System.Web.UI.Page
	{
		protected Connection conn;
		protected Connection conn2;
		protected string mid, occ_type, bed, bath, desc, scid;
		protected string garage, land, build, phone, elec;
		protected string pam, year, bmarket, lmarket, bapp1, bapp2;
		protected string lapp1, lapp2, bforce1, bforce2;
		protected string lforce1, lforce2, remarks, currency, cid;

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

			InitialCon(); 

			BindData();		
		}

		private void InitialCon()
		{
			conn = new Connection("Data Source=" + LBL_DB_IP.Text + ";Initial Catalog=" + LBL_DB_NAME.Text + ";uid=" + LBL_LOG_ID.Text + ";pwd=" + LBL_LOG_PWD.Text + ";Pooling=true");
		}

		private void BindData()
		{	
			conn.QueryString = "select a.PROYEK_ID, b.PROYEK_DESCRIPTION as PROYEK_NAME, a.TYPE_CODE, c.TYPE_NAME as TIPE, "+
				"a.TYPE_DESCRIPTION , a.CH_STA, STATUS = case a.CH_STA when '1' then 'INSERT' "+
				"when '2' then 'UPDATE' when '3' then 'DELETE' end, b.ID_KOTA "+
				"from THOUSEINFORMATION a, proyek_housingloan b, HOUSE_TYPE c "+
				"where a.proyek_id = b.proyek_id and c.type_code = a.type_code"; 

			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG_APPR.DataSource = dt;

			try
			{
				DG_APPR.DataBind();
			}
			catch 
			{
				DG_APPR.CurrentPageIndex = DG_APPR.PageCount - 1;
				DG_APPR.DataBind();
			}
			
		}

		private string cleansText(string tb)
		{
			if (tb.Trim() == "&nbsp;")
				tb = "";
			return tb;
		}

		private void deleteData(int row)
		{
			try 
			{
				string proid = DG_APPR.Items[row].Cells[0].Text.Trim();
				string type = DG_APPR.Items[row].Cells[2].Text.Trim();

				conn.QueryString = "DELETE FROM THOUSEINFORMATION WHERE PROYEK_ID = '"+proid+"' AND TYPE_CODE = '"+type+"'"; 
				conn.ExecuteQuery();

				conn.ClearData(); 
			} 
			catch {}
		}

		private void performRequest(int row, string userid)
		{
			int rst = 0;

			string prid = DG_APPR.Items[row].Cells[0].Text.Trim();
			string tcode = DG_APPR.Items[row].Cells[2].Text.Trim();
			string status = DG_APPR.Items[row].Cells[6].Text.Trim();

			conn.QueryString = "SELECT * FROM THOUSEINFORMATION WHERE PROYEK_ID = '"+prid+"' AND TYPE_CODE = '"+tcode+"'"; 
			conn.ExecuteQuery(); 

			if(conn.GetRowCount() != 0)
			{
				desc = conn.GetFieldValue(0,"Type_Description");
				occ_type = conn.GetFieldValue(0,"Occupancy_Type");
				bed = conn.GetFieldValue(0,"Bedrooms");
				bath = conn.GetFieldValue(0,"Bathrooms");
				garage = conn.GetFieldValue(0,"Garage");
				land = conn.GetFieldValue(0,"Land_Area");
				build = conn.GetFieldValue(0,"Building_Area");
				phone = conn.GetFieldValue(0,"Phone");
				elec = conn.GetFieldValue(0,"Electricity");
				pam = conn.GetFieldValue(0,"PAM");
				year = conn.GetFieldValue(0,"Year_Built");
				bmarket = conn.GetFieldValue(0,"BMarket");
				lmarket = conn.GetFieldValue(0,"LMarket");
				bapp1 = conn.GetFieldValue(0,"BApp1");
				bapp2 = conn.GetFieldValue(0,"BApp2");
				lapp1 = conn.GetFieldValue(0,"LApp1");
				lapp2 = conn.GetFieldValue(0,"LApp2");
				bforce1 = conn.GetFieldValue(0,"BForced1");
				bforce2 = conn.GetFieldValue(0,"BForced2");
				lforce1 = conn.GetFieldValue(0,"LForced1");
				lforce2 = conn.GetFieldValue(0,"LForced2");
				remarks = conn.GetFieldValue(0,"Remarks");
				currency = conn.GetFieldValue(0,"Mata_uang");
				cid = conn.GetFieldValue(0,"CITY_ID");
			}

			rst = row;

			conn.ClearData();

			/* coding for audittrial parameter */
			
			conn.QueryString = "EXEC PARAM_AREA_HOUSEINFO_AUDIT '"+prid+"','"+tcode+"','"+status+"','"+userid+"',"+GlobalTools.ConvertNull(desc)+","+
				GlobalTools.ConvertNull(occ_type)+","+GlobalTools.ConvertFloat(bed)+","+GlobalTools.ConvertFloat(bath)+","+
				GlobalTools.ConvertFloat(garage)+","+GlobalTools.ConvertFloat(land)+","+
				GlobalTools.ConvertFloat(build)+","+GlobalTools.ConvertNull(phone)+","+
				GlobalTools.ConvertNull(elec)+","+GlobalTools.ConvertNull(pam)+","+
				GlobalTools.ConvertNull(year)+","+GlobalTools.ConvertFloat(bmarket)+","+
				GlobalTools.ConvertFloat(lmarket)+","+GlobalTools.ConvertFloat(bapp1)+","+
				GlobalTools.ConvertFloat(bapp2)+","+GlobalTools.ConvertFloat(lapp1)+","+
				GlobalTools.ConvertFloat(lapp2)+","+GlobalTools.ConvertFloat(bforce1)+","+
				GlobalTools.ConvertFloat(bforce2)+","+GlobalTools.ConvertFloat(lforce1)+","+
				GlobalTools.ConvertFloat(lforce2)+","+GlobalTools.ConvertNull(remarks)+","+
				GlobalTools.ConvertNull(currency)+","+GlobalTools.ConvertNull(cid);
			
			conn.ExecuteNonQuery();
			
			/* end of coding */
			
			if (status.Equals("1"))
			{
				try
				{
					conn.QueryString = "insert into HOUSEINFORMATION values"+
						"('"+prid+"', '"+tcode+"', "+GlobalTools.ConvertNull(desc)+", "+GlobalTools.ConvertNull(occ_type)+
						", "+GlobalTools.ConvertNull(bed)+", "+GlobalTools.ConvertNull(bath)+  
						", "+GlobalTools.ConvertNull(garage)+", "+GlobalTools.ConvertFloat(land)+
						", "+GlobalTools.ConvertFloat(build)+", "+GlobalTools.ConvertNull(phone)+ 
						", "+GlobalTools.ConvertNull(elec)+", "+GlobalTools.ConvertNull(pam)+ 
						", "+GlobalTools.ConvertNull(year)+", "+GlobalTools.ConvertFloat(bmarket)+  
						", "+GlobalTools.ConvertFloat(lmarket)+", "+GlobalTools.ConvertFloat(bapp1)+
						", "+GlobalTools.ConvertFloat(bapp2)+", "+GlobalTools.ConvertFloat(lapp1)+
						", "+GlobalTools.ConvertFloat(lapp2)+", "+GlobalTools.ConvertFloat(bforce1)+", "+GlobalTools.ConvertFloat(bforce2)+
						", "+GlobalTools.ConvertFloat(lforce1)+", "+GlobalTools.ConvertFloat(lforce2)+
						", "+GlobalTools.ConvertNull(remarks)+", null, "+GlobalTools.ConvertNull(currency)+
						", "+GlobalTools.ConvertNull(cid)+",'1')";
					
					conn.ExecuteQuery();			
				}
				catch
				{
					GlobalTools.popMessage(this,"Cannot approve, data is already exist!");
				}
				finally
				{
					deleteData(rst);
				}
			}

			if (status.Equals("2"))
			{
				try
				{
					conn.QueryString = "update HOUSEINFORMATION set Type_Description = "+GlobalTools.ConvertNull(desc)+", "+
						"Occupancy_Type = "+GlobalTools.ConvertNull(occ_type)+", Bedrooms = "+GlobalTools.ConvertNull(bed)+", "+
						"Bathrooms = "+GlobalTools.ConvertNull(bath)+", Garage = "+GlobalTools.ConvertNull(garage)+", "+
						"Land_Area = "+GlobalTools.ConvertFloat(land)+", Building_Area = "+GlobalTools.ConvertFloat(build)+", "+ 
						"Phone = "+GlobalTools.ConvertNull(phone)+", Electricity = "+GlobalTools.ConvertNull(elec)+", "+
						"PAM = "+GlobalTools.ConvertNull(pam)+", Year_Built = "+GlobalTools.ConvertNull(year)+", "+
						"BMarket = "+GlobalTools.ConvertFloat(bmarket)+", LMarket = "+GlobalTools.ConvertFloat(lmarket)+", "+
						"BApp1 = "+GlobalTools.ConvertFloat(bapp1)+", BApp2 = "+GlobalTools.ConvertFloat(bapp2)+", "+
						"LApp1 = "+GlobalTools.ConvertFloat(lapp1)+", LApp2 = "+GlobalTools.ConvertFloat(lapp2)+", "+
						"BForced1 = "+GlobalTools.ConvertFloat(bforce1)+", BForced2 = "+GlobalTools.ConvertFloat(bforce2)+", "+
						"LForced1 = "+GlobalTools.ConvertFloat(lforce1)+", LForced2 = "+GlobalTools.ConvertFloat(lforce2)+", "+
						"Remarks = "+GlobalTools.ConvertNull(remarks)+", Mata_uang = "+GlobalTools.ConvertNull(currency)+", "+
						"CITY_ID = "+GlobalTools.ConvertNull(cid)+" "+
						"where PROYEK_ID = '"+prid+"' and Type_Code = '"+tcode+"'";
					
					conn.ExecuteQuery();
				}
				catch(Exception Ex)
				{
					string errmsg = Ex.Message.Replace("'","");
					if (errmsg.IndexOf("Last Query:") > 0)		
						errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
					GlobalTools.popMessage(this, errmsg);
				}
				finally
				{
					deleteData(rst);
				}
			}

			if (status.Equals("3"))
			{
				try
				{
					conn.QueryString = "UPDATE HOUSEINFORMATION SET ACTIVE='0' WHERE PROYEK_ID = '"+prid+"' AND TYPE_CODE = '"+tcode+"'"; 

					conn.ExecuteQuery();
				}
				catch(Exception Ex)
				{
					string errmsg = Ex.Message.Replace("'","");
					if (errmsg.IndexOf("Last Query:") > 0)		
						errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
					GlobalTools.popMessage(this, errmsg);
				}
				finally
				{
					deleteData(rst);
				}
			}
			
			conn.ClearData(); 
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
			this.DG_APPR.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG_APPR_ItemCommand);
			this.DG_APPR.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_APPR_PageIndexChanged);

		}
		#endregion

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("../../AreaParamApprovalAll.aspx?mc=9902040202&moduleId=40");
		}

		protected void BTN_SUBMIT_Click(object sender, System.EventArgs e)
		{
			scid = (string) Session["UserID"];

			for (int i = 0; i < DG_APPR.Items.Count; i++)
			{
				RadioButton rbA = (RadioButton)DG_APPR.Items[i].FindControl("Rdb1"),
					rbR = (RadioButton)DG_APPR.Items[i].FindControl("Rdb2");
				
				if(rbA.Checked)
				{
					performRequest(i, scid);
				}
				else if(rbR.Checked)
				{
					deleteData(i);
				}
			}

			BindData();			
		}

		private void DG_APPR_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_APPR.CurrentPageIndex = e.NewPageIndex;
			
			BindData(); 			
		}

		private void DG_APPR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int i;
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "allAppr":
					for (i = 0; i < DG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DG_APPR.Items[i].FindControl("Rdb1"),
								rbB = (RadioButton) DG_APPR.Items[i].FindControl("Rdb2"),
								rbC = (RadioButton) DG_APPR.Items[i].FindControl("Rdb3");
							rbB.Checked = false;
							rbC.Checked = false;
							rbA.Checked = true;
						} 
						catch {}
					}
					break;
				case "allReject":
					for (i = 0; i < DG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DG_APPR.Items[i].FindControl("Rdb1"),
								rbB = (RadioButton) DG_APPR.Items[i].FindControl("Rdb2"),
								rbC = (RadioButton) DG_APPR.Items[i].FindControl("Rdb3");
							rbA.Checked = false;
							rbC.Checked = false;
							rbB.Checked = true;
						} 
						catch {}
					}
					break;
				case "allPend":
					for (i = 0; i < DG_APPR.PageSize; i++)
					{
						try
						{
							RadioButton rbA = (RadioButton) DG_APPR.Items[i].FindControl("Rdb1"),
								rbB = (RadioButton) DG_APPR.Items[i].FindControl("Rdb2"),
								rbC = (RadioButton) DG_APPR.Items[i].FindControl("Rdb3");
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
