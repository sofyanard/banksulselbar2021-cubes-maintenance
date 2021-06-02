<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFAgencyOfrParamAppr.aspx.cs" Inherits="CuBES_Maintenance.Parameter.General.SME.RFAgencyOfrParamAppr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
	<!-- #include file="../../../include/cek_all.html" -->
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <table id="Table1" cellspacing="2" cellpadding="2" width="100%">
                <tr>
                    <td class="tdNoBorder">
                        <!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
                        <table id="Table6">
                            <tr>
                                <td class="tdBGColor2" style="width: 400px" align="center"><b>Parameter Approval</b></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdNoBorder" align="right">
                        <asp:ImageButton ID="BTN_BACK" runat="server" ImageUrl="/SME/image/Back.jpg"></asp:ImageButton><a href="../../../Body.aspx"><img src="/SME/Image/MainMenu.jpg"></a><a href="../../../Logout.aspx" target="_top"><img src="/SME/Image/Logout.jpg"></a></td>
                </tr>
                <tr>
                    <td class="tdNoBorder" colspan="2"></td>
                </tr>
                <tr>
                    <td class="tdHeader1" valign="top" width="50%" colspan="2" align="center">Agency Officer Parameter</td>
                </tr>
                <tr>
                    <td valign="top" width="50%" colspan="2">
                        <asp:DataGrid ID="DGRequest" runat="server" Width="100%" AllowPaging="True" CellPadding="1" AutoGenerateColumns="False"
                            ShowFooter="True" OnItemCommand="DGRequest_ItemCommand" OnPageIndexChanged="DGRequest_PageIndexChanged">
                            <AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
                            <Columns>
                                <asp:BoundColumn DataField="AGOFR_CODE" HeaderText="Officer ID">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="AGOFR_DESC" HeaderText="Officer Name">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="AGENCYID" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="AGENCYNAME" HeaderText="Agency Name">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    </asp:BoundColumn>
                                <asp:BoundColumn Visible="False" DataField="PENDINGSTATUS"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Pending Status">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Approve">
                                    <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rdo_Approve" runat="server" GroupName="rdg_Decision"></asp:RadioButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="BTN_All_Approve" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Reject">
                                    <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rdo_Reject" runat="server" GroupName="rdg_Decision"></asp:RadioButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="BTN_All_Reject" runat="server" CommandName="allRejc">Select All</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Pending">
                                    <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rdo_Pending" runat="server" GroupName="rdg_Decision" Checked="True"></asp:RadioButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="BTN_All_Pending" runat="server" CommandName="allPend">Select All</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid></td>
                </tr>
                <tr>
                    <td class="TDBGColor2" valign="top" width="50%" colspan="2">
                        <asp:Button ID="BTN_SUBMIT" runat="server" CssClass="Button1" Width="100px" Text="Submit" OnClick="BTN_SUBMIT_Click"></asp:Button></td>
                </tr>
            </table>
		</center>
    </form>
</body>
</html>
