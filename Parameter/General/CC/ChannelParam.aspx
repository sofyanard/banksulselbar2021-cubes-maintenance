<%@ Page language="c#" Codebehind="ChannelParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.OmsetParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>OmsetParam</title>
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
					<TD class="tdNoBorder" style="HEIGHT: 48px" colSpan="2">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD width="350">
									<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD class="tdBGColor2"><B>
													<asp:label id="LBL_LEFT_TITLE" runat="server">Parameter Credit Card Host - Maker</asp:label></B></TD>
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
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;
						<asp:label id="LBL_TITLE" runat="server"> Channels</asp:label>&nbsp;Maker</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR id="TR_MODULE" runat="server" visible>
								<TD class="TDBGColor1" width="150"><B>Module</B></TD>
								<TD width="15"></TD>
								<TD class="TDBGColorValue">
									<asp:radiobuttonlist id="RDB_MODULE" runat="server" Width="328px" RepeatDirection="Horizontal" AutoPostBack="True" onselectedindexchanged="RDB_MODULE_SelectedIndexChanged">
										<asp:ListItem Value="1">SME</asp:ListItem>
										<asp:ListItem Value="2">Consumer</asp:ListItem>
										<asp:ListItem Value="3" Selected="True">Credit Card</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="150">
									<asp:label id="LBL_CODE" runat="server"> ID</asp:label></TD>
								<TD width="15"></TD>
								<TD class="TDBGColorValue">
									<asp:textbox onkeypress="return kutip_satu()" id="TXT_CODE" runat="server" CssClass="mandatory"
										MaxLength="10" Width="102px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">
									<asp:label id="LBL_DESC" runat="server"> Description</asp:label></TD>
								<TD></TD>
								<TD class="TDBGColorValue">
									<asp:textbox onkeypress="return kutip_satu()" id="TXT_DESC" runat="server" MaxLength="30" Width="342px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">SICS Code</TD>
								<TD></TD>
								<TD class="TDBGColorValue">
									<asp:textbox onkeypress="return kutip_satu()" id="TXT_SICS" runat="server" MaxLength="15" Width="150px"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Runat="server" Text="Save" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Runat="server" Text="Cancel" onclick="BTN_CANCEL_Click"></asp:button>
						<asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label>
						<asp:label id="LBL_TABLENAME" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Existing&nbsp;Data</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_EXISTING1" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="CODE" HeaderText="ID">
									<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DES" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SICS_ID" HeaderText="SICS Code">
									<HeaderStyle Width="150px" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="150px" CssClass="tdSmallHeader"></HeaderStyle>
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
					<TD class="tdHeader1" colSpan="2">Maker Request</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="CODE" HeaderText="ID">
									<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DES" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SICS_ID" HeaderText="SICS Code">
									<HeaderStyle Width="150px" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="STATUS" HeaderText="Status">
									<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PENDING_STATUS"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="150px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="Linkbutton1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
