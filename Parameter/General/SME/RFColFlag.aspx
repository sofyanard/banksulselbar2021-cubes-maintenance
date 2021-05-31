<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFColFlag.aspx.cs" Inherits="CuBES_Maintenance.Parameter.General.SME.RFColFlag" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Bukti Pemilikan Hak</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../../Style.css" type="text/css" rel="stylesheet">
    <!-- #include file="../../../include/cek_mandatoryOnly.html" -->
    <!-- #include file="../../../include/cek_entries.html" -->
</head>
<body leftmargin="0" topmargin="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <center>
        <table id="Table1" cellspacing="2" cellpadding="2" width="100%">
            <tr>
                <td class="tdNoBorder">
                    <!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
                    <table id="Table6">
                        <tr>
                            <td class="tdBGColor2" style="width: 400px" align="center">
                                <b>Parameter Setup</b>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdNoBorder" align="right">
                    <a href="ListCustomer.aspx?si="></a>
                    <asp:ImageButton ID="BTN_BACK" runat="server" ImageUrl="../../../image/Back.jpg"
                        OnClick="BTN_BACK_Click"></asp:ImageButton><a href="../../../Body.aspx"><img src="../../../Image/MainMenu.jpg"></a>
                    <a href="../../../Logout.aspx" target="_top">
                        <img src="../../../Image/Logout.jpg"></a>
                </td>
            </tr>
            <tr>
                <td class="tdNoBorder" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="tdHeader1" valign="top" align="center" width="50%" colspan="2">
                    Bukti Pemilikan Hak
                </td>
            </tr>
            <!--<asp:label id="lbl_CU_CUSTTYPEID" runat="server" Visible="False"></asp:label>-->
            <tr id="TR_PERSONAL" runat="server">
                <td class="td" valign="top" width="50%" colspan="2">
                    <table id="Table20" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td class="TDBGColor1">
                                Certificate Type ID
                            </td>
                            <td style="width: 15px">
                                :
                            </td>
                            <td class="TDBGColorValue">
                                <asp:TextBox onkeypress="return kutip_satu()" ID="TXT_ID" runat="server" CssClass="mandatory"
                                    MaxLength="50"></asp:TextBox><asp:Label ID="LBL_SAVEMODE" runat="server" Visible="False">1</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor1">
                                Description
                            </td>
                            <td style="width: 15px">
                                :
                            </td>
                            <td class="TDBGColorValue">
                                <asp:TextBox onkeypress="return kutip_satu()" ID="TXT_DESC" runat="server" CssClass="mandatory"
                                    MaxLength="500" Width="700px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor1">
                                Collateral Flag
                            </td>
                            <td style="width: 15px">
                                :
                            </td>
                            <td class="TDBGColorValue">
                                <%--<asp:TextBox onkeypress="return kutip_satu()" ID="TXT_COLFLAG" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="DDL_COLFLAG" runat="server" Width="168px"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor2" valign="top" width="50%" colspan="2">
                    <asp:Button ID="BTN_SAVE" runat="server" CssClass="Button1" Width="100px" Text="Save"
                        OnClick="BTN_SAVE_Click"></asp:Button>&nbsp;
                    <asp:Button ID="BTN_CANCEL" runat="server" CssClass="Button1" Width="100px" Text="Cancel"
                        OnClick="BTN_CANCEL_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td valign="top" width="50%" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="tdHeader1" valign="top" width="50%" colspan="2">
                    Existing Data
                </td>
            </tr>
            <tr>
                <td valign="top" width="50%" colspan="2">
                    <asp:DataGrid ID="DG_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False"
                        CellPadding="1" AllowPaging="True" 
                        onitemcommand="DG_EXISTING_ItemCommand" 
                        onpageindexchanged="DG_EXISTING_PageIndexChanged" 
                        onitemdatabound="DG_EXISTING_ItemDataBound">
                        <AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
                        <Columns>
                            <asp:BoundColumn DataField="CerTypeID" HeaderText="Certificate Type ID">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Description" HeaderText="Description">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ColFlag" HeaderText="Collateral Flag">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="ACTIVE"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Function">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lb_edit1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="lb_delete1" runat="server" CommandName="delete">Delete</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="lb_undelete1" runat="server" CommandName="undelete" Visible="False">UnDelete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td valign="top" width="50%" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="tdHeader1" valign="top" width="50%" colspan="2">
                    Requested Data
                </td>
            </tr>
            <tr>
                <td valign="top" width="50%" colspan="2">
                    <asp:DataGrid ID="DG_PENDING" runat="server" Width="100%" AutoGenerateColumns="False"
                        CellPadding="1" AllowPaging="True" 
                        onpageindexchanged="DG_PENDING_PageIndexChanged" 
                        onitemcommand="DG_PENDING_ItemCommand" 
                        onitemdatabound="DG_PENDING_ItemDataBound">
                        <AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
                        <Columns>
                            <asp:BoundColumn DataField="CerTypeID" HeaderText="Certificate Type ID">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Description" HeaderText="Description">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ColFlag" HeaderText="Collateral Flag">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="PENDINGSTATUS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PENDINGSTATUSDESC" HeaderText="Pending Status">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Function">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lb_edit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="lb_delete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
