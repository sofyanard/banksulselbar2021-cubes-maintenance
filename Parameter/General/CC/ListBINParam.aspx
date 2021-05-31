<%@ Page language="c#" Codebehind="ListBINParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.ListBINParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>General Credit Card Parameter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table2">
						</TABLE>
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="400">
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													Credit Card General</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx">
										<IMG src="../../../Image/MainMenu.jpg"></A>&nbsp;<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A><A href="../../../Logout.aspx" target="_top"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;List BIN Issuer Maker</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" width="50%">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 202px; HEIGHT: 12px" width="202">Bank</TD>
								<TD style="WIDTH: 7px; HEIGHT: 12px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 12px">
									<asp:DropDownList id="DDL_BN_CODE" runat="server" CssClass="mandatory"></asp:DropDownList>
									<asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label>
									<asp:label id="LBL_SEQ" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 202px; HEIGHT: 22px">BIN Number</TD>
								<TD style="WIDTH: 7px; HEIGHT: 22px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 22px">
									<asp:textbox id="TXT_BIN_NUMB" onkeypress="return kutip_satu()" runat="server" Width="184px"
										MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 202px">ICA</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue">
									<asp:textbox id="TXT_ICA" onkeypress="return kutip_satu()" runat="server" Width="184px" MaxLength="20"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Existing&nbsp;Data</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
						<asp:datagrid id="DGR_EXISTING1" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="BINSEQ" HeaderText="Seq">
									<HeaderStyle Width="5%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="BN_CODE"></asp:BoundColumn>
								<asp:BoundColumn DataField="BANKNAME" HeaderText="Bank">
									<HeaderStyle Width="20%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BIN_NUMBER" HeaderText="BIN Issuer">
									<HeaderStyle Width="35%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ICA" HeaderText="ICA">
									<HeaderStyle Width="20%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete2" runat="server" CommandName="delete1">Delete</asp:LinkButton>
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
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="BINSEQ" HeaderText="Seq">
									<HeaderStyle Width="5%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="BN_CODE"></asp:BoundColumn>
								<asp:BoundColumn DataField="BANKNAME" HeaderText="Bank">
									<HeaderStyle Width="20%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BIN_NUMBER" HeaderText="BIN Issuer">
									<HeaderStyle Width="35%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ICA" HeaderText="ICA">
									<HeaderStyle Width="20%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Status">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PENDING_STATUS"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
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
