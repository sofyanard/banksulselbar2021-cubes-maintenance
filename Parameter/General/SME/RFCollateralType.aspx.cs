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
using System.Configuration;

namespace CuBES_Maintenance.Parameter.General.SME
{
	/// <summary>
	/// Summary description for RFBMRatingII.
	/// </summary>
	public partial class RFCollateralType : System.Web.UI.Page
	{
	
		protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				FillDropDownList();
				LBL_SAVEMODE.Text = "1";
				bindData1();
				bindData2();
			/*	TXT_A.ReadOnly=true;
				TXT_B.ReadOnly=true; */
			}
			Datagrid1.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change1);
			DataGrid2.PageIndexChanged += new DataGridPageChangedEventHandler(this.Grid_Change2);
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
        
		private void FillDropDownList()
		{
            GlobalTools.fillRefList(RDO_COLTYPE, "select distinct COLLINKTABLE,COLLINKTABLE from RFCOLLATERALTYPE", false, conn);
		}
	
		private void bindData1()
		{
			conn.QueryString = "select " +
			                   "COLTYPESEQ," +
			                   "COLTYPEID," +
			                   "COLTYPEDESC," +
                               "COLLINKTABLE," +
			                   "RATINGCODE," +
			                   "COLLTYPECODESIBS" +
			                   " from RFCOLLATERALTYPE WHERE ACTIVE=1";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("A"));
			dt.Columns.Add(new DataColumn("B"));
			dt.Columns.Add(new DataColumn("C"));
			dt.Columns.Add(new DataColumn("D"));
			dt.Columns.Add(new DataColumn("E"));
            dt.Columns.Add(new DataColumn("F"));
			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,2);
				dr[3] = conn.GetFieldValue(i,3);
				dr[4] = conn.GetFieldValue(i,4);
                dr[5] = conn.GetFieldValue(i,5);
				dt.Rows.Add(dr);
			}
			Datagrid1.DataSource = new DataView(dt);
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

		private void bindData2()
		{
		/**/
			conn.QueryString = "select " +
			                   "COLTYPESEQ," +
			                   "COLTYPEID," +
			                   "COLTYPEDESC," +
                               "COLLINKTABLE," +
			                   "RATINGCODE," +
			                   "COLLTYPECODESIBS," +
			                   "PENDINGSTATUS " +
			                   "from PENDING_RFCOLLATERALTYPE ";
			conn.ExecuteQuery();
			
			DataTable dt = new DataTable();			
			dt.Columns.Add(new DataColumn("A"));
			dt.Columns.Add(new DataColumn("B"));
			dt.Columns.Add(new DataColumn("C"));
			dt.Columns.Add(new DataColumn("D"));
			dt.Columns.Add(new DataColumn("E"));
			dt.Columns.Add(new DataColumn("F"));
            dt.Columns.Add(new DataColumn("G"));
            dt.Columns.Add(new DataColumn("H"));
			DataRow dr;
			for(int i = 0; i < conn.GetDataTable().Rows.Count; i++) 
			{
				dr = dt.NewRow();
				dr[0] = conn.GetFieldValue(i,0);
				dr[1] = conn.GetFieldValue(i,1);
				dr[2] = conn.GetFieldValue(i,2);
				dr[3] = conn.GetFieldValue(i,3);
				dr[4] = conn.GetFieldValue(i,4);
				dr[5] = conn.GetFieldValue(i,5);
                dr[6] = getPendingStatus(conn.GetFieldValue(i,6));
                dr[7] = conn.GetFieldValue(i,6);
				dt.Rows.Add(dr);
			}
			DataGrid2.DataSource = new DataView(dt);
			try 
			{
				DataGrid2.DataBind();
			}
			catch 
			{
				DataGrid2.CurrentPageIndex = DataGrid2.PageCount - 1;
				DataGrid2.DataBind();
			}
			/*
			for (int i = 0; i < DataGrid2.Items.Count; i++)
			{
				if (DataGrid2.Items[i].Cells[6].Text.Trim() == "0")
				{
					DataGrid2.Items[i].Cells[6].Text = "Update";
				}
				else if (DataGrid2.Items[i].Cells[6].Text.Trim() == "1")
				{
					DataGrid2.Items[i].Cells[6].Text = "Insert";
				}
				else if (DataGrid2.Items[i].Cells[6].Text.Trim() == "2")
				{
					DataGrid2.Items[i].Cells[6].Text = "Delete";
				}
			}*/ 
		}

		private void clearEditBoxes()
		{
		    Label1.Text = "";

			TXT_A.Text="";
			TXT_B.Text="";
			TXT_C.Text="";
            TXT_CODESIBS.Text = "";
            
            RDO_COLTYPE.Enabled = true;
		    //TXT_A.Enabled = true;
            //TXT_B.Enabled = true;
            //TXT_C.Enabled = true;
            FillDropDownList();
		    
			TXT_A.ReadOnly=false;
			TXT_B.ReadOnly=false;
					
			LBL_SAVEMODE.Text = "1";
			activatePostBackControls(true);
		}

		private void activatePostBackControls(bool mode)
		{
			//TXT_BRANCH_CODE.Enabled = mode;
		}

		private void cleansTextBox (TextBox tb)
		{
			if (tb.Text.Trim() == "&nbsp;")
				tb.Text = "";
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
			this.Datagrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_ItemCommand);
			this.DataGrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_ItemCommand);

		}
		#endregion

		void Grid_Change1(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			Datagrid1.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData1();	
		}

		void Grid_Change2(Object sender, DataGridPageChangedEventArgs e) 
		{
			// Set CurrentPageIndex to the page the user clicked.
			DataGrid2.CurrentPageIndex = e.NewPageIndex;
			// Re-bind the data to refresh the DataGrid control. 
			bindData2();	
		}

		private void Datagrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					try
					{
						RDO_COLTYPE.SelectedValue=e.Item.Cells[3].Text.Trim();
					}
					catch{};

					Label1.Text=e.Item.Cells[0].Text.Trim(); //seq
					TXT_A.Text= e.Item.Cells[1].Text.Trim(); //id
					TXT_B.Text = e.Item.Cells[2].Text.Trim(); //desc
					TXT_C.Text = e.Item.Cells[4].Text.Trim(); //rating code
					TXT_CODESIBS.Text = e.Item.Cells[5].Text.Trim(); //sibs

                    //COLTYPESEQ, 
                    //COLTYPEID,
                    //COLTYPEDESC,
                    //COLLINKTABLE,
                    //RATINGCODE,
                    //COLLTYPECODESIBS

					TXT_A.ReadOnly=true;
					TXT_B.ReadOnly=true;
					RDO_COLTYPE.Enabled = true;
					LBL_SAVEMODE.Text = "0";
					activatePostBackControls(false);
					cleansTextBox(TXT_A);
					cleansTextBox(TXT_B);
					cleansTextBox(TXT_C);
					cleansTextBox(TXT_CODESIBS);
					break;
				
							
				default:
					// Do nothing.
					break;
			} 
		}

		private void DataGrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			clearEditBoxes();
			switch(((LinkButton)e.CommandSource).CommandName)
			{
				case "edit":
					/*try
					{
						RDO_COLTYPE.SelectedValue=e.Item.Cells[3].Text.Trim();
					}
					catch{};*/
                    //----
                    LBL_SAVEMODE.Text = e.Item.Cells[7].Text.Trim(); //pending status
			      /*  if (LBL_SAVEMODE.Text.Trim() == "2")
			        {
			            LBL_SAVEMODE.Text = "1";
			        } */

			        //----
                    Label1.Text=e.Item.Cells[0].Text.Trim(); //seq
					TXT_A.Text=e.Item.Cells[1].Text.Trim(); //id
					TXT_B.Text = e.Item.Cells[2].Text.Trim(); //desc

                    if (e.Item.Cells[3].Text.Trim() == "&nbsp;")
			        {
			            RDO_COLTYPE.SelectedValue = "" ;
                    }else{
                    RDO_COLTYPE.SelectedValue = e.Item.Cells[3].Text.Trim(); //collateral group
                    }
                    TXT_C.Text = e.Item.Cells[4].Text.Trim(); //rating code
					TXT_CODESIBS.Text = e.Item.Cells[5].Text.Trim(); //sibs

                    //"COLTYPESEQ," +
                    //"COLTYPEID," +
                    //"COLTYPEDESC," +
                    //"COLLINKTABLE" +
                    //"RATINGCODE," +
                    //"COLLTYPECODESIBS," +
                    //"PENDINGSTATUS "

			        status_temp.Text = "pending_edit";

					TXT_A.ReadOnly=true;
					TXT_B.ReadOnly=true;
					//LBL_SAVEMODE.Text = "0";
					RDO_COLTYPE.Enabled = true;
					activatePostBackControls(false);
					cleansTextBox(TXT_A);
					cleansTextBox(TXT_B);
					cleansTextBox(TXT_C);
					cleansTextBox(TXT_CODESIBS);
					break;
				
				case "delete":
					//LBL_SAVEMODE.Text = "2";
					conn.QueryString = "DELETE PENDING_RFCOLLATERALTYPE WHERE COLTYPESEQ='"+e.Item.Cells[0].Text.Trim()+"' ";
					conn.ExecuteNonQuery();
					bindData2();
					break;
				default:
					// Do nothing.
					break;
			}  
		}

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
		    
            

		    if (TXT_A.Text.Trim() == "" || TXT_B.Text.Trim() == "")
		    {
                Tools.popMessage(this, "ID and Description cannot empty!");
                return;
		    }
			
            //jika status insert baru
		    if (LBL_SAVEMODE.Text.Trim() == "1")
		    {
		        //cek apakah ID dan DESC dan RATINGCODE sudah ada sebelumnya di RFCOLLATERALTYPE
		        conn.QueryString = "select * from RFCollateralType where COLTYPEID ='" + TXT_A.Text.Trim() + "' ";// +
		                          // "and COLTYPEDESC = '" + TXT_B.Text.TrimStart().TrimEnd() + "' and RATINGCODE = '" +
		                         //  TXT_C.Text.Trim() + "'";
		        conn.ExecuteQuery();

		        if (conn.GetRowCount() > 0)
		        {
		            Tools.popMessage(this, "ID has already been used! Request canceled!");
		            return;
		        }
		        else
		        {
		            if (status_temp.Text == "")
		            {
		                Label1.Text = GetLatestSeq("PENDING_RFCOLLATERALTYPE", "COLTYPESEQ");
		            }
		        }
		    }


		    ExecuteMaker(Label1.Text, TXT_A.Text, TXT_B.Text, TXT_CODESIBS.Text, TXT_C.Text, RDO_COLTYPE.SelectedValue, LBL_SAVEMODE.Text, status_temp.Text);      
		    bindData2();
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		    status_temp.Text = "";

		}

	    protected void ExecuteMaker(string seq, string id, string desc, string sibscode, string ratingcode, string colgroup, string pendingStatus, string statusTemp)
	    {
            if (status_temp.Text == "pending_edit")
            {
                //cek jika sudah ada di PENDING_RFCOLLATERALTYPE | link edit dari pending grid (pending edit)
                conn.QueryString = "SELECT * FROM PENDING_RFCOLLATERALTYPE " +
                                   "WHERE COLTYPESEQ =" + seq;
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    //ada di PENDING_RFCOLLATERALTYPE status apa aja | link edit dari pending grid (pending edit)
                    conn.QueryString = "Update PENDING_RFCOLLATERALTYPE " +
                                       "Set " +
                                       "COLTYPEID = '" + id + "'," +
                                       "COLTYPEDESC = '" + desc + "'," +
                                       "COLLINKTABLE = '" + colgroup + "'," +
                                       "COLLTYPECODESIBS = '" + sibscode + "'," +
                                       "RATINGCODE = '" + ratingcode + "'," +
                                       "PENDINGSTATUS = '" + pendingStatus + "'" +
                                       "Where COLTYPESEQ =" + seq;
                    conn.ExecuteQuery();
                }
            }
            else
            {
                conn.QueryString = "INSERT INTO PENDING_RFCOLLATERALTYPE(" +
                                           "COLTYPESEQ,"+
                                           "COLTYPEID," +
                                           "COLTYPEDESC," +
                                           "COLLINKTABLE," +
                                           "COLLTYPECODESIBS," +
                                           "ACTIVE," +
                                           "REQAPPRAISAL," +
                                           "RATINGCODE," +
                                           "PENDINGSTATUS" +
                                           ") VALUES ('" +
                                           seq + "','" +
                                           id + "','" + //id
                                           desc + "','" + //desc
                                           colgroup + "','" + //dropdown collateral group => collinktable
                                           sibscode + "','" + //sibs code
                                           "1','" + //active
                                           "','" + //reqappraisal
                                           ratingcode + "','" + //rating
                                           pendingStatus + "')"; //pending status

                conn.ExecuteQuery();
            }
	    }

	    protected string GetLatestSeq(string tablename, string field)
	    {
	        string seq = "";
            conn.QueryString = "select top 1 " +
                               field +
                               " from " +
                               tablename +
                               " where " +
                               "PENDINGSTATUS = '1'" +
                               " order by " +
                               field +
                               " desc";
            conn.ExecuteQuery();

            seq = conn.GetRowCount() <= 0 ? "0" : conn.GetFieldValue(0,0);
	        
	        int a = Convert.ToInt32(seq);
	        a++;
	        seq = Convert.ToString(a);

	        return seq;
	    }

	    protected void BTN_CANCEL_Click(object sender, System.EventArgs e)
		{
			clearEditBoxes();
			LBL_SAVEMODE.Text = "1";
		    status_temp.Text = "";
		}

		protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]+"&moduleId=01&pg="+Request.QueryString["pg"]+"");
			//Response.Redirect("../../GeneralParamAll.aspx?mc="+Request.QueryString["mc"]);
		}
	}
}
