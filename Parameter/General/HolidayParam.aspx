<%@ Page Language="c#" CodeBehind="HolidayParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.HolidayParam" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>HolidayParam</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../../style.css" type="text/css" rel="stylesheet">
    <!-- #include file="../../include/cek_entries.html" -->
    <script language="javascript">
        function ch_jenis(vstatus1, vstatus2) {
            frm = document.Form1;
            frm.DDL_LIBURDATE.disabled = vstatus1;
            frm.DDL_LIBURMONTH.disabled = vstatus1;
            frm.DDL_LIBURYEAR.disabled = vstatus1;
            frm.TXT_LIBURDESC.readOnly = vstatus1;
            frm.DDL_PEKANYEAR.disabled = vstatus2;
            frm.CB_HARI.disabled = vstatus2;
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <center>
        <table id="Table1" cellspacing="2" cellpadding="2" width="100%">
            <tr>
                <td class="tdNoBorder">
                    <table id="Table2" style="width: 408px; height: 17px" cellspacing="0" cellpadding="0"
                        width="408">
                        <tr>
                            <td class="tdBGColor2" style="width: 400px" align="center">
                                <b>Parameter Maintenance : General Maker</b>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdNoBorder" align="right">
                    <asp:ImageButton ID="BTN_BACK" runat="server" ImageUrl="../../Image/back.jpg"></asp:ImageButton><a
                        href="../../Body.aspx"><img src="../../Image/MainMenu.jpg"></a> <a href="../../Logout.aspx"
                            target="_top">
                            <img src="../../Image/Logout.jpg"></a>
                </td>
            </tr>
            <tr>
                <td class="tdNoBorder" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="tdHeader1" valign="top" align="center" width="50%" colspan="2">
                    Parameter Holiday Maker
                </td>
            </tr>
            <tr>
                <td class="td" valign="top" width="50%" colspan="2">
                    <table id="Table3" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td class="TDBGColor1" style="width: 128px; height: 16px">
                                Tanggal
                            </td>
                            <td style="width: 9px; height: 16px">
                                :
                            </td>
                            <td class="TDBGColorValue" style="height: 16px">
                                <asp:TextBox onkeypress="return numbersonly()" ID="TXT_HARI" runat="server" Width="41px"
                                    MaxLength="2"></asp:TextBox><asp:DropDownList ID="DDL_LIBURMONTH" runat="server">
                                    </asp:DropDownList>
                                <asp:TextBox onkeypress="return numbersonly()" ID="TXT_TAHUN" runat="server" Width="72px"
                                    MaxLength="4"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor1" style="width: 128px">
                                Deskripsi Libur
                            </td>
                            <td style="width: 9px">
                                :
                            </td>
                            <td class="TDBGColorValue">
                                <asp:TextBox ID="TXT_LIBURDESC" runat="server" Width="248px"></asp:TextBox><asp:CheckBox
                                    ID="CheckDes" runat="server" Text="Libur Nasional" AutoPostBack="True" 
                                    oncheckedchanged="CheckDes_CheckedChanged"></asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor1" style="width: 128px">
                                Kategori
                            </td>
                            <td style="width: 8px">
                                :
                            </td>
                            <td class="TDBGColorValue">
                                <asp:RadioButtonList ID="CB_HARI" runat="server" RepeatDirection="Horizontal" 
                                    Enabled="true" AutoPostBack="True" 
                                    onselectedindexchanged="CB_HARI_SelectedIndexChanged">
                                    <asp:ListItem Value="Sabtu">Sabtu</asp:ListItem>
                                    <asp:ListItem Value="Minggu">Minggu</asp:ListItem>
                                    <asp:ListItem Value="Lain-lain" Selected="True">Lain- lain</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:Label ID="LBL_LOG_PWD" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LBL_LOG_ID" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LBL_DB_NAME" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LBL_DB_IP" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%-- <tr>
            <table id="Table4" cellspacing="0" cellpadding="0" width="100%">
               
            </table>
        </tr>--%>
            <tr>
                <td class="TDBGColor2" colspan="2" valign="top">
                    <%--width="50%" align="left"--%>
                    <asp:Button ID="BTN_SAVE" Text="Save" CssClass="button1" runat="server" OnClick="BTN_SAVE_Click">
                    </asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BTN_CANCEL" Text="Cancel" CssClass="button1" runat="server" OnClick="BTN_CANCEL_Click">
                    </asp:Button>
                    <asp:Label ID="LBL_SAVEMODE" runat="server" Visible="False">1</asp:Label>
                    <asp:TextBox ID="TXT_HL_TYPE" runat="server" Visible="False">01</asp:TextBox>
                    <asp:TextBox ID="TXT_HL_CODE" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TXT_HL_DATE_LAMA" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="LBL_EDIT" Style="z-index: 101; left: 552px; position: absolute; top: 216px"
                        runat="server" Visible="true">1</asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" width="50%" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="tdHeader1" colspan="2" valign="top" width="50%">
                    Current Holiday Table
                </td>
            </tr>
            <tr>
                <td class="td" colspan="2" width="100%" align="center" valign="top">
                    <asp:DataGrid ID="DG_HOLIDAY" runat="server" Width="100%" AutoGenerateColumns="False"
                        PageSize="15" AllowPaging="True">
                        <AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LBL_NO" runat="server"></asp:Label>&nbsp;
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="HL_DATE1" HeaderText="Tanggal Libur">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="HL_DESC" HeaderText="Deskripsi Libur">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Function">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_EDIT" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="LB_DELETE" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
                <td class="tdHeader1" colspan="2">
                    Maker Request
                </td>
            </tr>
            <tr>
                <td class="td" colspan="2" width="50%" align="center" valign="top">
                    <asp:DataGrid ID="DG_THOLIDAY" runat="server" Width="100%" AutoGenerateColumns="False"
                        PageSize="15" AllowPaging="True" onitemcommand="DG_THOLIDAY_ItemCommand" 
                        onitemdatabound="DG_THOLIDAY_ItemDataBound">
                        <AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="No">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="LBL_TNO" runat="server"></asp:Label>&nbsp;
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="HL_DATE1" HeaderText="Tanggal Libur">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="HL_DESC" HeaderText="Deskripsi Libur">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CH_STA" HeaderText="Status">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Function">
                                <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_EDIT2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="LB_DELETE2" runat="server" CommandName="delete">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
            <%-- <tr>
            <td class="TDBGColor2" colspan="2" width="50%" align="center" valign="top">
                &nbsp;
            </td>
        </tr>--%>
        </table>
    </center>
    </form>
</body>
</html>
