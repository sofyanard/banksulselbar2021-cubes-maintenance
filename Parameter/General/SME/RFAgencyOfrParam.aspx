<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFAgencyOfrParam.aspx.cs" Inherits="CuBES_Maintenance.Parameter.General.SME.RFAgencyOfrParam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../../../style.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <table id="Table1" cellSpacing="2" cellPadding="2" width="100%">
                <tr>
					<td class="tdNoBorder">
						<table id="Table4">
							<tr>
								<td class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Setup</B></td>
							</tr>
						</table>
					</td>
					<td class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="/SME/image/Back.jpg"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A><A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A></td>
				</tr>
				<tr>
					<td class="tdNoBorder"></td>
					<td class="tdNoBorder" align="right"></td>
				</tr>
				<tr>
					<td class="tdHeader1" colSpan="2">Agency Officer</td>
				</tr>
				<tr>
                    <td class="td" valign="top" width="50%">
                        <table id="Table4" cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td class="TDBGColor1">Agency</td>
                                <td></td>
                                <td class="TDBGColorValue">
                                    <asp:DropDownList ID="DDL_AGENCY" runat="server" CssClass="mandatory"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="TDBGColor1" style="width: 129px">Officer ID</td>
                                <td style="width: 15px"></td>
                                <td class="TDBGColorValue">
                                    <asp:TextBox ID="TXT_OFFICER_ID" onkeypress="return kutip_satu()" runat="server" MaxLength="50"
                                        CssClass="mandatory"></asp:TextBox><asp:Label ID="LBL_SAVEMODE" runat="server" Visible="false">1</asp:Label></td>
                            </tr>
                            <tr>
                                <td class="TDBGColor1" style="width: 129px">Nama Officer</td>
                                <td style="width: 15px"></td>
                                <td class="TDBGColorValue">
                                    <asp:TextBox ID="TXT_OFFICER_NAME" onkeypress="return kutip_satu()" runat="server" MaxLength="50"
                                        CssClass="mandatory"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td class="td" valign="top" width="50%">
                        
                    </td>
                    <tr>
                        <td class="TDBGColor2" valign="top" align="center" width="50%" colspan="2">
                            <asp:Button ID="BTN_SAVE" runat="server" CssClass="Button1" Width="101" Text="Save" OnClick="BTN_SAVE_Click"></asp:Button>&nbsp;
							<asp:Button ID="BTN_CANCEL" CssClass="button1" Width="101px" Text="Cancel" runat="server" OnClick="BTN_CANCEL_Click"></asp:Button></td>
                    </tr>
                    <tr>
                        <td valign="top" align="center" width="50%" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="tdHeader1" valign="top" align="center" width="50%" colspan="2">Existing 
							Officer</td>
                    </tr>
                    <tr>
                        <td valign="top" align="center" width="50%" colspan="2">
                            <asp:DataGrid ID="DGR_OFFICER_EXIST" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
                                AllowPaging="True" OnItemCommand="DGR_OFFICER_EXIST_ItemCommand" OnPageIndexChanged="DGR_OFFICER_EXIST_PageIndexChanged">
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
                                    <asp:BoundColumn DataField="ACTIVE" Visible="False"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Function">
                                        <HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_RfEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton ID="lnk_RfDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid></td>
                    </tr>
                    <tr>
                        <td valign="top" align="center" width="50%" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="tdHeader1" style="height: 21px" valign="top" align="center" width="50%" colspan="2">Officer 
							Requested</td>
                    </tr>
                    <tr>
                        <td valign="top" align="center" width="50%" colspan="2">
                            <asp:DataGrid ID="DGR_OFFICER_REQ" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
                                AllowPaging="True" OnPageIndexChanged="DGR_OFFICER_REQ_PageIndexChanged" OnItemCommand="DGR_OFFICER_REQ_ItemCommand">
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
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn Text="Edit" CommandName="Edit">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                    </asp:ButtonColumn>
                                    <asp:ButtonColumn Text="Delete" CommandName="Delete">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:ButtonColumn>
                                </Columns>
                                <PagerStyle Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid></td>
                    </tr>
                </tr>
            </table>
        </center>
    </form>
</body>
</html>
