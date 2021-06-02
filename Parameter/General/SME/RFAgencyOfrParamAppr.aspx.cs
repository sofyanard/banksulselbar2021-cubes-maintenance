using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.SME
{
    public partial class RFAgencyOfrParamAppr : System.Web.UI.Page
    {
        protected Connection conn;
        protected Tools tool = new Tools();

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

            if (!IsPostBack)
            {
                viewPendingData();
            }
        }

		private void viewPendingData()
		{
            conn.QueryString = "select o.AGOFR_CODE, o.AGOFR_DESC, o.AGENCYID, a.AGENCYNAME, o.ACTIVE, o.PENDINGSTATUS " +
                "from PENDING_RFAGENCYOFR o " +
                "join RFAGENCY a on o.AGENCYID = a.AGENCYID ";
            conn.ExecuteQuery();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AGOFR_CODE"));
            dt.Columns.Add(new DataColumn("AGOFR_DESC"));
            dt.Columns.Add(new DataColumn("AGENCYID"));
            dt.Columns.Add(new DataColumn("AGENCYNAME"));
            dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
            dt.Columns.Add(new DataColumn("PENDING_STATUS"));


            DataRow dr;
            for (int i = 0; i < conn.GetDataTable().Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr[0] = conn.GetFieldValue(i, "AGOFR_CODE");
                dr[1] = conn.GetFieldValue(i, "AGOFR_DESC");
                dr[2] = conn.GetFieldValue(i, "AGENCYNAME");
                dr[3] = conn.GetFieldValue(i, "AGENCYNAME");
                dr[4] = conn.GetFieldValue(i, "PENDINGSTATUS");
                dr[5] = getPendingStatus(conn.GetFieldValue(i, "PENDINGSTATUS").ToString());

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

        protected void DGRequest_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGRequest.CurrentPageIndex = e.NewPageIndex;
            viewPendingData();
        }

        private void performRequest(int row)
        {
            try
            {
                string id = DGRequest.Items[row].Cells[0].Text.Trim();
                conn.QueryString = "exec PARAM_GENERAL_RFAGENCYOFR_APPR '" + id + "', '1', '" +
                    Session["UserID"].ToString() + "'";
                conn.ExecuteQuery();
            }
            catch { }
        }

        private void deleteData(int row)
        {
            try
            {
                string id = DGRequest.Items[row].Cells[0].Text.Trim();
                conn.QueryString = "exec PARAM_GENERAL_RFAGENCYOFR_APPR '" + id + "', '0', '" +
                    Session["UserID"].ToString() + "'";
                conn.ExecuteQuery();
            }
            catch { }
        }

        protected void BTN_SUBMIT_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGRequest.Items.Count; i++)
            {
                try
                {
                    RadioButton rbA = (RadioButton)DGRequest.Items[i].FindControl("rdo_Approve"),
                                rbR = (RadioButton)DGRequest.Items[i].FindControl("rdo_Reject");
                    if (rbA.Checked)
                    {
                        performRequest(i);
                    }
                    else if (rbR.Checked)
                    {
                        deleteData(i);
                    }
                }
                catch { }
            }
            viewPendingData();
        }

        protected void DGRequest_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            int i;
            switch (((LinkButton)e.CommandSource).CommandName)
            {
                case "allAppr":
                    for (i = 0; i < DGRequest.PageSize; i++)
                    {
                        try
                        {
                            RadioButton rbA = (RadioButton)DGRequest.Items[i].FindControl("rdo_Approve"),
                                rbB = (RadioButton)DGRequest.Items[i].FindControl("rdo_Reject"),
                                rbC = (RadioButton)DGRequest.Items[i].FindControl("rdo_Pending");
                            rbB.Checked = false;
                            rbC.Checked = false;
                            rbA.Checked = true;
                        }
                        catch { }
                    }
                    break;
                case "allRejc":
                    for (i = 0; i < DGRequest.PageSize; i++)
                    {
                        try
                        {
                            RadioButton rbA = (RadioButton)DGRequest.Items[i].FindControl("rdo_Approve"),
                                rbB = (RadioButton)DGRequest.Items[i].FindControl("rdo_Reject"),
                                rbC = (RadioButton)DGRequest.Items[i].FindControl("rdo_Pending");
                            rbA.Checked = false;
                            rbC.Checked = false;
                            rbB.Checked = true;
                        }
                        catch { }
                    }
                    break;
                case "allPend":
                    for (i = 0; i < DGRequest.PageSize; i++)
                    {
                        try
                        {
                            RadioButton rbA = (RadioButton)DGRequest.Items[i].FindControl("rdo_Approve"),
                                rbB = (RadioButton)DGRequest.Items[i].FindControl("rdo_Reject"),
                                rbC = (RadioButton)DGRequest.Items[i].FindControl("rdo_Pending");
                            rbA.Checked = false;
                            rbB.Checked = false;
                            rbC.Checked = true;
                        }
                        catch { }
                    }
                    break;
                default:
                    // Do nothing.
                    break;
            }
        }
    }
}