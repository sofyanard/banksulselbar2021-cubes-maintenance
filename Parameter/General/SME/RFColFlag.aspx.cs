using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter.General.SME
{
    public partial class RFColFlag : System.Web.UI.Page
    {
        protected Tools tool = new Tools();
        protected Connection conn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["connModuleSME"]);

        //load page
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LBL_SAVEMODE.Text = "1";
                FillDropdownList();
                ViewExistingData();
                ViewPendingData();
            }

            BTN_SAVE.Attributes.Add("onclick", "if(!cek_mandatory(document.Form1)){return false;};");

        }

        //page initiation
        private void FillDropdownList()
        {
            GlobalTools.fillRefList(DDL_COLFLAG, "select distinct COLLINKTABLE,COLLINKTABLE from RFCOLLATERALTYPE where ACTIVE = 1", false, conn);
        }

        private void ClearControls()
        {
            TXT_ID.Text = "";
            TXT_DESC.Text = "";
            //TXT_COLFLAG.Text = "";
            DDL_COLFLAG.SelectedValue = "";
            LBL_SAVEMODE.Text = "1";

            TXT_ID.ReadOnly = false;
        }

        //load grid
        private void ViewExistingData()
        {
            conn.QueryString = "select CERTTYPEID, CERTTYPEDESC, COLFLAG, ACTIVE from RFCERTTYPE";
            conn.ExecuteQuery();

            DataTable dt = new DataTable();
            //dt = conn.GetDataTable().Copy();
            //DG_EXISTING.DataSource = dt;
            dt.Columns.Add(new DataColumn("CerTypeID"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("ColFlag"));
            dt.Columns.Add(new DataColumn("ACTIVE"));
            DataRow dr;
            for (int i = 0; i < conn.GetDataTable().Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr[0] = conn.GetFieldValue(i, 0);
                dr[1] = conn.GetFieldValue(i, 1);
                dr[2] = conn.GetFieldValue(i, 2);
                dr[3] = conn.GetFieldValue(i, 3);
                dt.Rows.Add(dr);
            }
            DG_EXISTING.DataSource = new DataView(dt);


            try
            {
                DG_EXISTING.DataBind();
            }
            catch
            {
                DG_EXISTING.CurrentPageIndex = DG_EXISTING.PageCount - 1;
                DG_EXISTING.DataBind();
            }
        }

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

        //button
        protected void BTN_BACK_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string classid = "";
            try
            {
                classid = Request.QueryString["classid"].ToString();
            }
            catch { classid = ""; }

            if ((classid.Equals("01")) || (classid.ToString().Trim() == "01"))
                Response.Redirect("../../HostParam.aspx?mc=" + Request.QueryString["mc"] + "&moduleId=01&pg=" + Request.QueryString["pg"] + " ");
            else
                Response.Redirect("../../GeneralParamAll.aspx?mc=" + Request.QueryString["mc"] + "&moduleId=01&pg=" + Request.QueryString["pg"] + " ");
        }

        protected void BTN_SAVE_Click(object sender, EventArgs e)
        {

            try
            {
                conn.QueryString = "exec PARAM_RFCOLFLAG_MAKER '" +
                    TXT_ID.Text + "', '" +
                    TXT_DESC.Text + "', '" +
                    DDL_COLFLAG.SelectedValue + "', '" +
                    LBL_SAVEMODE.Text + "'";
                conn.ExecuteQuery();

                ViewPendingData();
                ClearControls();
            }
            catch (Exception ex)
            {
                Response.Write("<!--" + ex.Message + "-->");
                string errmsg = ex.Message.Replace("'", "");
                if (errmsg.IndexOf("Last Query:", System.StringComparison.Ordinal) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:", System.StringComparison.Ordinal));
                GlobalTools.popMessage(this, errmsg);
            }
        }

        protected void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        //itemdatabound
        protected void DG_EXISTING_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.AlternatingItem:
                case ListItemType.EditItem:
                case ListItemType.Item:
                case ListItemType.SelectedItem:
                    try
                    {
                        LinkButton lbedit = (LinkButton)e.Item.Cells[4].FindControl("lb_edit1");
                        LinkButton lbdel = (LinkButton)e.Item.Cells[4].FindControl("lb_delete1");
                        LinkButton lbundel = (LinkButton)e.Item.Cells[4].FindControl("lb_undelete1");
                        string status = e.Item.Cells[3].Text;
                        if (status == "1")
                        {
                            e.Item.Cells[0].ForeColor = System.Drawing.Color.Black;
                            e.Item.Cells[1].ForeColor = System.Drawing.Color.Black;
                            e.Item.Cells[2].ForeColor = System.Drawing.Color.Black;
                            lbedit.Visible = true;
                            lbdel.Visible = true;
                            lbundel.Visible = false;
                        }
                        else
                        {
                            e.Item.Cells[0].ForeColor = System.Drawing.Color.Red;
                            e.Item.Cells[1].ForeColor = System.Drawing.Color.Red;
                            e.Item.Cells[2].ForeColor = System.Drawing.Color.Red;
                            lbedit.Visible = false;
                            lbdel.Visible = false;
                            lbundel.Visible = true;
                        }
                    }
                    catch (Exception a)
                    {
                        string m = a.Message;
                    }
                    break;
                case ListItemType.Footer:
                    break;
                default:
                    break;
            }
        }

        protected void DG_PENDING_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.AlternatingItem:
                case ListItemType.EditItem:
                case ListItemType.Item:
                case ListItemType.SelectedItem:
                    try
                    {
                        LinkButton lbedit = (LinkButton)e.Item.Cells[5].FindControl("lb_edit2");

                        if ((e.Item.Cells[3].Text == "3") || (e.Item.Cells[3].Text == "4"))
                        {
                            lbedit.Visible = false;
                        }
                        else
                        {
                            lbedit.Visible = true;
                        }
                    }
                    catch
                    {
                    }
                    break;
                case ListItemType.Footer:
                    break;
                default:
                    break;
            }
        }

        //itemcommand
        protected void DG_EXISTING_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (((LinkButton)e.CommandSource).CommandName)
            {
                case "edit":
                    ClearControls();
                    TXT_ID.Text = e.Item.Cells[0].Text.Trim();
                    TXT_DESC.Text = e.Item.Cells[1].Text.Trim();

                    DDL_COLFLAG.SelectedValue = e.Item.Cells[2].Text.Trim() == "&nbsp;" ? "" : e.Item.Cells[2].Text.Trim();
                    LBL_SAVEMODE.Text = "2";
                    break;

                case "delete":
                    try
                    {
                        LBL_SAVEMODE.Text = "3";

                        conn.QueryString = "exec PARAM_RFCOLFLAG_MAKER '" +
                            e.Item.Cells[0].Text.Trim() + "', '" +
                            e.Item.Cells[1].Text.Trim() + "', '" +
                            e.Item.Cells[2].Text.Trim() + "', '" +
                            LBL_SAVEMODE.Text + "'";
                        conn.ExecuteQuery();

                        ViewPendingData();
                        ClearControls();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<!--" + ex.Message + "-->");
                        string errmsg = ex.Message.Replace("'", "");
                        if (errmsg.IndexOf("Last Query:", System.StringComparison.Ordinal) > 0)
                            errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:", System.StringComparison.Ordinal));
                        GlobalTools.popMessage(this, errmsg);
                    }
                    break;

                case "undelete":
                    try
                    {
                        LBL_SAVEMODE.Text = "4";

                        conn.QueryString = "exec PARAM_RFCOLFLAG_MAKER '" +
                            e.Item.Cells[0].Text.Trim() + "', '" +
                            e.Item.Cells[1].Text.Trim() + "', '" +
                            e.Item.Cells[2].Text.Trim() + "', '" +
                            LBL_SAVEMODE.Text + "'";
                        conn.ExecuteQuery();

                        ViewPendingData();
                        ClearControls();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<!--" + ex.Message + "-->");
                        string errmsg = ex.Message.Replace("'", "");
                        if (errmsg.IndexOf("Last Query:", System.StringComparison.Ordinal) > 0)
                            errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:", System.StringComparison.Ordinal));
                        GlobalTools.popMessage(this, errmsg);
                    }
                    break;

                default:
                    break;
            }
        }

        protected void DG_PENDING_ItemCommand(object source, DataGridCommandEventArgs e)
        {

            switch (((LinkButton)e.CommandSource).CommandName)
            {
                case "edit":

                    if ((e.Item.Cells[5].Text == "Delete") || (e.Item.Cells[5].Text == "UnDelete"))
                    {
                        return;
                    }
                    else
                    {
                        ClearControls();
                        TXT_ID.Text = e.Item.Cells[0].Text.Trim();
                        TXT_DESC.Text = e.Item.Cells[1].Text.Trim();

                        DDL_COLFLAG.SelectedValue = e.Item.Cells[2].Text.Trim() == "&nbsp;" ? "" : e.Item.Cells[2].Text.Trim();

                        LBL_SAVEMODE.Text = "2";

                        TXT_ID.ReadOnly = true;
                    }
                    break;
                case "delete":

                    LBL_SAVEMODE.Text = "5";
                    try
                    {
                        conn.QueryString = "exec PARAM_RFCOLFLAG_MAKER '" +
                                           e.Item.Cells[0].Text.Trim() + "', '" +
                                           e.Item.Cells[1].Text.Trim() + "', '" +
                                           e.Item.Cells[2].Text.Trim() + "', '" +
                                           LBL_SAVEMODE.Text + "'";
                        conn.ExecuteQuery();

                        ViewPendingData();
                        ClearControls();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<!--" + ex.Message + "-->");
                        string errmsg = ex.Message.Replace("'", "");
                        if (errmsg.IndexOf("Last Query:", System.StringComparison.Ordinal) > 0)
                            errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:", System.StringComparison.Ordinal));
                        GlobalTools.popMessage(this, errmsg);
                    }
                    break;
                default:
                    break;
            }
        }

        //pageindexchange
        protected void DG_EXISTING_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DG_EXISTING.CurrentPageIndex = e.NewPageIndex;
            ViewExistingData();
        }

        protected void DG_PENDING_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DG_PENDING.CurrentPageIndex = e.NewPageIndex;
            ViewPendingData();
        }





    }
}