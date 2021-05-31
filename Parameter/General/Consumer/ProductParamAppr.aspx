<%@ Page language="c#" Codebehind="ProductParamAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.ProductParamAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Product Parameter Approval</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													General Approval</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
									<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2"><asp:label id="LBL_LOG_ID2" runat="server" Visible="False"></asp:label><asp:label id="LBL_LOG_PWD2" runat="server" Visible="False"></asp:label>Parameter&nbsp;Product 
						Approval
						<asp:label id="LBL_DB" runat="server" Visible="False"></asp:label><asp:label id="LBL_IP" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="2"><ASP:DATAGRID id="DG_APPR" runat="server" ShowFooter="True" Width="100%" AllowPaging="True" CellPadding="1"
							AutoGenerateColumns="False" PageSize="5">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="PRODUCTID" HeaderText="Code">
									<HeaderStyle Width="14%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Product Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="STATUS" HeaderText="Status">
									<HeaderStyle Width="16%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="14%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="link_view" runat="server" CommandName="view">Detail</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Approve">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="Rdb1" runat="server" GroupName="approval_status"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="Lb1" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Reject">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="Rdb2" runat="server" GroupName="approval_status"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="Lb2" runat="server" CommandName="allReject">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pending">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="Rdb3" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="Lb3" runat="server" CommandName="allPend">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Visible="False" Mode="NumericPages"></PagerStyle>
						</ASP:DATAGRID></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label><asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label><asp:button id="BTN_SUBMIT" Runat="server" Text="Submit" CssClass="button1" onclick="BTN_SUBMIT_Click"></asp:button><asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR id="TR_KET" runat="server">
					<TD align="center" colSpan="2">
						<table cellSpacing="0" cellPadding="0" width="80%" border="1">
							<tr>
								<td class="tdHeader1" style="FONT-SIZE: 12px" colSpan="6">Product
									<asp:label id="LBL_PRNAME" Runat="server"></asp:label>&nbsp; (<asp:label id="LBL_PID" Runat="server"></asp:label>)
								</td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">Status</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue" width="25%"><asp:label id="LBL_STATUS" Runat="server"></asp:label></td>
								<td class="TDBGColor1" width="16%">Group Name</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue" width="25%"><asp:label id="LBL_GRP_NAME" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">Customer Type</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_CUST_TYPE" Runat="server"></asp:label></td>
								<td class="TDBGColor1" width="16%">Negative List</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_NEG_LIST" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" style="HEIGHT: 21px" width="16%">Blacklist</td>
								<td style="HEIGHT: 21px" align="center" width="1%">:</td>
								<td class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_BLACK" Runat="server"></asp:label></td>
								<td class="TDBGColor1" style="HEIGHT: 21px" width="16%">Presrceneer</td>
								<td style="HEIGHT: 21px" align="center" width="1%">:</td>
								<td class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_PRES" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">DHBI</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_DHBI" Runat="server"></asp:label></td>
								<td class="TDBGColor1" width="16%">Promo Group</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_PROMO" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">Type</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_TYPE" Runat="server"></asp:label></td>
								<td class="TDBGColor1" width="16%">NPWP Limit</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_NPWP" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">Down Payment</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_DP" Runat="server"></asp:label></td>
								<td class="TDBGColor1" width="16%">SPPK Limit Time</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_SPPK" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">AIP Limit Time</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_AIP" Runat="server"></asp:label></td>
								<td class="TDBGColor1" width="16%">Admin Fee</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_ADMIN" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">Floor Rate</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_FLOOR_RATE" Runat="server"></asp:label></td>
								<td class="TDBGColor1" width="16%">Floor Limit</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_FLOOR_LIM" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">Celing Limit</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_CEIL_LIM" Runat="server"></asp:label></td>
								<td class="TDBGColor1" width="16%">Provisi</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_PROV" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">Provition Rate</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_PROV_RATE" Runat="server"></asp:label></td>
								<td class="TDBGColor1" width="16%">Fiducia</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_FIDU" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">Fiducia Limit</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_FIDU_LIM" Runat="server"></asp:label></td>
								<td class="TDBGColor1" width="16%">Bea Other</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_BEA_OTHER" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">eMAS Code</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_EMAS_CODE" Runat="server"></asp:label></td>
								<td class="TDBGColor1" width="16%">SPK</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_SPK" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">Marketing Source Code</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_MKSC" Runat="server"></asp:label>&nbsp;</td>
								<td class="TDBGColor1" width="16%">Kendara</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_PR_KENDARA" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="TDBGColor1" width="16%">Allow Cardbundling</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_CARDBUNDLING" Runat="server"></asp:label>&nbsp;</td>
								<td class="TDBGColor1" width="16%">Mitrakarya</td>
								<td align="center" width="1%">:</td>
								<td class="TDBGColorValue"><asp:label id="LBL_PR_MITRAKARYA" Runat="server"></asp:label></td>
							</tr>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
