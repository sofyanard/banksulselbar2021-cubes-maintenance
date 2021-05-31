using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.SME
{
    public partial class RFColFlagAppr : System.Web.UI.Page
    {
        protected Tools tool = new Tools();
        protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewPendingData();
            }
        }

        protected void BTN_BACK_Click(object sender, ImageClickEventArgs e)
        {
            string classid = "";
            try
            {
                classid = Request.QueryString["classid"].ToString();
            }
            catch { classid = ""; }

            if ((classid.Equals("01")) || (classid.ToString().Trim() == "01"))
                Response.Redirect("../../HostParamApproval.aspx?mc=" + Request.QueryString["mc"] + "&moduleId=01&pg=" + Request.QueryString["pg"] + " ");
            else
                Response.Redirect("../../GeneralParamApprovalAll.aspx?mc=" + Request.QueryString["mc"] + "&moduleId=01&pg=" + Request.QueryString["pg"] + "");
        }

        protected void BTN_SUBMIT_Click(object sender, EventArgs e)
        {
            int count = DG_PENDING.Items.Count;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    RadioButton rbA = (RadioButton)DG_PENDING.Items[i].FindControl("rdo_Approve"),
                                rbR = (RadioButton)DG_PENDING.Items[i].FindControl("rdo_Reject");
                    if (rbA.Checked)
                    {
                        PerformRequest(i);
                    }
                    else if (rbR.Checked)
                    {
                        DeleteData(i);
                    }
                }
                catch { }
            }
            ViewPendingData();
        }


        //bind data
        private void ViewPendingData()
        {
            conn.QueryString = "select CERTTYPEID, CERTTYPEDESC, COLFLAG, PENDINGSTATUS from PENDING_RFCERTTYPE";
            conn.ExecuteQuery();

            DataTable dt = new DataTable();
            //dt = conn.GetDataTable().Copy();
            //DG_EXISTING.DataSource = dt;
            dt.Columns.Add(new DataColumn("CerTypeID"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("ColFlag"));
            dt.Columns.Add(new DataColumn("PENDINGSTATUS"));
            dt.Columns.Add(new DataColumn("PENDINGSTATUSDESC"));
            DataRow dr;
            for (int i = 0; i < conn.GetDataTable().Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr[0] = conn.GetFieldValue(i, 0);
                dr[1] = conn.GetFieldValue(i, 1);
                dr[2] = conn.GetFieldValue(i, 2);
                dr[3] = conn.GetFieldValue(i, 3);
                dr[4] = getPendingStatus(conn.GetFieldValue(i, 3));

                dt.Rows.Add(dr);
            }
            DG_PENDING.DataSource = new DataView(dt);


            try
            {
                DG_PENDING.DataBind();
            }
            catch
            {
                DG_PENDING.CurrentPageIndex = DG_PENDING.PageCount - 1;
                DG_PENDING.DataBind();
            }
        }

        //pending status desc
        private string getPendingStatus(string saveMode)
        {
            string status = "";
            switch (saveMode)
            {
                case "1":
                    status = "Insert";
                    break;
                case "2":
                    status = "Update";
                    break;
                case "3":
                    status = "Delete";
                    break;
                case "4":
                    status = "Undelete";
                    break;
                default:
                    status = "";
                    break;
            }
            return status;
        }

        //
        private void PerformRequest(int row)
        {
            try
            {
                string id = DG_PENDING.Items[row].Cells[0].Text.Trim();
                string userid = Session["UserID"].ToString();
                conn.QueryString = "exec PARAM_RFCOLFLAG_APPROVAL '" + id + "','1','" + userid + "'";
                conn.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Response.Write("<!--" + ex.Message + "-->");
                string errmsg = ex.Message.Replace("'", "");
                if (errmsg.IndexOf("Last Query:") > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
                GlobalTools.popMessage(this, errmsg);
            }
        }

        private void DeleteData(int row)
        {
            try
            {
                string id = DG_PENDING.Items[row].Cells[0].Text.Trim();
                conn.QueryString = "exec PARAM_RFCOLFLAG_APPROVAL '" + id + "', '0'";
                conn.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Response.Write("<!--" + ex.Message + "-->");
                string errmsg = ex.Message.Replace("'", "");
                if (errmsg.IndexOf("Last Query:") > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
                GlobalTools.popMessage(this, errmsg);
            }
        }

        //itemCommand
        protected void DG_PENDING_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            int i;
            switch (((LinkButton)e.CommandSource).CommandName)
            {
                case "allAppr":
                    for (i = 0; i < DG_PENDING.PageSize; i++)
                    {
                        try
                        {
                            RadioButton rbA = (RadioButton)DG_PENDING.Items[i].FindControl("rdo_Approve"),
                                rbB = (RadioButton)DG_PENDING.Items[i].FindControl("rdo_Reject"),
                                rbC = (RadioButton)DG_PENDING.Items[i].FindControl("rdo_Pending");
                            rbB.Checked = false;
                            rbC.Checked = false;
                            rbA.Checked = true;
                        }
                        catch { }
                    }
                    break;
                case "allRejc":
                    for (i = 0; i < DG_PENDING.PageSize; i++)
                    {
                        try
                        {
                            RadioButton rbA = (RadioButton)DG_PENDING.Items[i].FindControl("rdo_Approve"),
                                rbB = (RadioButton)DG_PENDING.Items[i].FindControl("rdo_Reject"),
                                rbC = (RadioButton)DG_PENDING.Items[i].FindControl("rdo_Pending");
                            rbA.Checked = false;
                            rbC.Checked = false;
                            rbB.Checked = true;
                        }
                        catch { }
                    }
                    break;
                case "allPend":
                    for (i = 0; i < DG_PENDING.PageSize; i++)
                    {
                        try
                        {
                            RadioButton rbA = (RadioButton)DG_PENDING.Items[i].FindControl("rdo_Approve"),
                                rbB = (RadioButton)DG_PENDING.Items[i].FindControl("rdo_Reject"),
                                rbC = (RadioButton)DG_PENDING.Items[i].FindControl("rdo_Pending");
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

        //pageIndexChange
        protected void DG_PENDING_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            DG_PENDING.CurrentPageIndex = e.NewPageIndex;
            ViewPendingData();
        }


    }
}