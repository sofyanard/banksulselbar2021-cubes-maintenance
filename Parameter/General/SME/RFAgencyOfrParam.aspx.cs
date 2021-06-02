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
    public partial class RFAgencyOfrParam : System.Web.UI.Page
    {
        protected Connection conn;
        protected Tools tool = new Tools();

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

            if (!IsPostBack)
            {
                fillAgency();

                viewExisting();
                viewRequest();
            }
        }

        private void fillAgency()
        {
            conn.QueryString = "select AGENCYID, AGENCYNAME from RFAGENCY"; // where ACTIVE = '1'";
            conn.ExecuteQuery();

            DDL_AGENCY.Items.Add(new ListItem("- PILIH -", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_AGENCY.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
            }
        }

        private void viewExisting()
        {
            conn.QueryString = "select o.AGOFR_CODE, o.AGOFR_DESC, o.AGENCYID, a.AGENCYNAME, o.ACTIVE " +
                "from RFAGENCYOFR o " +
                "join RFAGENCY a on o.AGENCYID = a.AGENCYID ";
            conn.ExecuteQuery();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AGOFR_CODE"));
            dt.Columns.Add(new DataColumn("AGOFR_DESC"));
            dt.Columns.Add(new DataColumn("AGENCYID"));
            dt.Columns.Add(new DataColumn("AGENCYNAME"));
            dt.Columns.Add(new DataColumn("ACTIVE"));

            DataRow dr;
            for (int i = 0; i < conn.GetDataTable().Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr[0] = conn.GetFieldValue(i, "AGOFR_CODE");
                dr[1] = conn.GetFieldValue(i, "AGOFR_DESC");
                dr[2] = conn.GetFieldValue(i, "AGENCYID");
                dr[3] = conn.GetFieldValue(i, "AGENCYNAME");
                dr[4] = conn.GetFieldValue(i, "ACTIVE");

                dt.Rows.Add(dr);
            }

            DGR_OFFICER_EXIST.DataSource = new DataView(dt);
            try
            {
                DGR_OFFICER_EXIST.DataBind();
            }
            catch
            {
                DGR_OFFICER_EXIST.CurrentPageIndex = DGR_OFFICER_EXIST.PageCount - 1;
                DGR_OFFICER_EXIST.DataBind();
            }

            for (int i = 0; i < DGR_OFFICER_EXIST.Items.Count; i++)
            {
                if (DGR_OFFICER_EXIST.Items[i].Cells[4].Text.Trim() == "0")
                {
                    LinkButton l_del = (LinkButton)DGR_OFFICER_EXIST.Items[i].FindControl("lnk_RfDelete");
                    l_del.CommandName = "Undelete";
                    l_del.Text = "Undelete";

                    LinkButton l_edit = (LinkButton)DGR_OFFICER_EXIST.Items[i].FindControl("lnk_RfEdit");
                    l_edit.Visible = false;
                }
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

        private void viewRequest()
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

            DGR_OFFICER_REQ.DataSource = new DataView(dt);
            try
            {
                DGR_OFFICER_REQ.DataBind();
            }
            catch
            {
                DGR_OFFICER_REQ.CurrentPageIndex = DGR_OFFICER_REQ.PageCount - 1;
                DGR_OFFICER_REQ.DataBind();
            }
        }

        protected void DGR_OFFICER_EXIST_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_OFFICER_EXIST.CurrentPageIndex = e.NewPageIndex;
            viewExisting();
        }

        protected void DGR_OFFICER_REQ_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_OFFICER_REQ.CurrentPageIndex = e.NewPageIndex;
            viewRequest();
        }

        protected void BTN_SAVE_Click(object sender, EventArgs e)
        {
            string active = "0";

            if (LBL_SAVEMODE.Text.Trim() == "1")
            {
                conn.QueryString = "select ACTIVE from RFAGENCYOFR where AGOFR_CODE='" + TXT_OFFICER_ID.Text.Trim() + "'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    active = conn.GetFieldValue("ACTIVE");
                    if (active == "1")
                    {
                        Tools.popMessage(this, "ID has already been used! Request canceled!");
                        return;
                    }
                    else
                    {
                        LBL_SAVEMODE.Text = "0";
                    }
                }
            }
            
            try
            {
                conn.QueryString = "exec PARAM_GENERAL_RFAGENCYOFR_MAKER '" + LBL_SAVEMODE.Text +
                                    "', '" + DDL_AGENCY.SelectedValue +
                                    "', '" + TXT_OFFICER_ID.Text +
                                    "', '" + TXT_OFFICER_NAME.Text + "'";
                conn.ExecuteNonQuery();
            }
            catch
            {
                Tools.popMessage(this, "Input tidak valid !");
                return;
            }

            viewRequest();
            clearControls();

            LBL_SAVEMODE.Text = "1";
        }

        private void clearControls()
        {
            // LBL_SAVEMODE.Text = "1";

            DDL_AGENCY.SelectedValue = "";
            TXT_OFFICER_ID.Text = "";
            TXT_OFFICER_NAME.Text = "";

            activateControlKey(false);
        }

        private void activateControlKey(bool isReadOnly)
        {
            TXT_OFFICER_ID.ReadOnly = isReadOnly;
        }

        protected void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            clearControls();
            LBL_SAVEMODE.Text = "1";
        }

        protected void DGR_OFFICER_EXIST_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string ID, CITYID, AGENCYTYPEID, NAME, ADDR1, ADDR2, ADDR3, ZIPCODE, PHNAREA;
            string PHNNUM, PHNEXT, FAXAREA, FAXNUM, FAXEXT, EMAIL;

            clearControls();
            switch (((LinkButton)e.CommandSource).CommandName.ToLower())
            {
                case "edit":
                    LBL_SAVEMODE.Text = "0";
                    TXT_OFFICER_ID.Text = e.Item.Cells[0].Text;
                    TXT_OFFICER_NAME.Text = e.Item.Cells[1].Text;
                    try
                    {
                        DDL_AGENCY.SelectedValue = e.Item.Cells[2].Text;
                    }
                    catch (Exception ex) { }
                    
                    activateControlKey(true);
                    break;

                case "delete":
                    conn.QueryString = "exec PARAM_GENERAL_RFAGENCYOFR_MAKER '2', '" + 
                        e.Item.Cells[2].Text.Trim() + "', '" + 
                        e.Item.Cells[0].Text.Trim() + "', '" + 
                        e.Item.Cells[0].Text.Trim() + "'";
                    conn.ExecuteQuery();
                    viewRequest();
                    break;

                case "undelete":
                    conn.QueryString = "exec PARAM_GENERAL_RFAGENCYOFR_MAKER '0', '" +
                        e.Item.Cells[2].Text.Trim() + "', '" +
                        e.Item.Cells[0].Text.Trim() + "', '" +
                        e.Item.Cells[0].Text.Trim() + "'";
                    conn.ExecuteQuery();
                    viewRequest();
                    break;

                default:
                    break;
            }
        }

        protected void DGR_OFFICER_REQ_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            clearControls();
            switch (((LinkButton)e.CommandSource).CommandName.ToLower())
            {
                case "edit":
                    LBL_SAVEMODE.Text = e.Item.Cells[4].Text;
                    if (LBL_SAVEMODE.Text.Trim() == "2")
                    {
                        // kalau ingin EDIT, yang status_pendingnya DELETE, ignore ....
                        LBL_SAVEMODE.Text = "1";
                        break;
                    }

                    LBL_SAVEMODE.Text = "0";
                    TXT_OFFICER_ID.Text = e.Item.Cells[0].Text;
                    TXT_OFFICER_NAME.Text = e.Item.Cells[1].Text;
                    try
                    {
                        DDL_AGENCY.SelectedValue = e.Item.Cells[2].Text;
                    }
                    catch (Exception ex) { }

                    activateControlKey(true);
                    break;

                case "delete":
                    string id = e.Item.Cells[0].Text;

                    conn.QueryString = "delete from PENDING_RFAGENCYOFR WHERE AGOFR_CODE = '" + id + "'";
                    conn.ExecuteQuery();
                    viewRequest();
                    break;
                default:
                    break;
            }
        }
    }
}