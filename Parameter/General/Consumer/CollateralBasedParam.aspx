<%@ Page language="c#" Codebehind="CollateralBasedParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.CollateralBasedParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFBRANCH</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
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
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Maintenance : 
													General&nbsp;Maker</B></TD>
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
					<TD class="tdHeader1" colSpan="2">Parameter Collateral Based On Product Maker</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" width="50%" colspan="2">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="200">Product Type</TD>
								<TD style="WIDTH: 5px">:</TD>
								<TD class="TDBGColorValue">
									<asp:DropDownList id="DDL_PROD_TYPE" runat="server" CssClass="mandatory"></asp:DropDownList>
									<asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="200">Collateral&nbsp;Type</TD>
								<TD style="WIDTH: 5px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 15px">
									<asp:DropDownList id="DDL_COLLATERAL" runat="server" CssClass="mandatory"></asp:DropDownList></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Current&nbsp;Collateral Based On Product Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2">
						<asp:datagrid id="Datagrid1" Width="100%" runat="server" AllowPaging="True" PageSize="20" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="PRODUK" HeaderText="Product Type">
									<HeaderStyle Width="30%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="COLATERAL" HeaderText="Collateral Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRODUCTID" HeaderText="P"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="COL_TYPE" HeaderText="C"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="SEQ_NO" HeaderText="No"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Maker Request</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DataGrid2" Width="100%" runat="server" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="PRODUK" HeaderText="Product Type">
									<HeaderStyle Width="30%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="COLATERAL" HeaderText="Collateral Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA" HeaderText="Status">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="SEQ_NO" HeaderText="No"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRODUCTID" HeaderText="P"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="COL_TYPE" HeaderText="C"></asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2">&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
