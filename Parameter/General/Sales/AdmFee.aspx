<%@ Page language="c#" Codebehind="AdmFee.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Sales.AdmFee" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AdmFee</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<!-- #include file="../../../include/cek_mandatory.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
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
														Sales Commission</B></TD>
											</TR>
										</TABLE>
									</TD>
									<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click">
										</asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>&nbsp;
										<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
										<A href="../../../Logout.aspx" target="_top"></A>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Administration Fee Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD width="50%">
										<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 90px; HEIGHT: 20px" width="90">Area</TD>
												<TD style="WIDTH: 11px; HEIGHT: 20px">:</TD>
												<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:dropdownlist id="DDL_AREA" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 90px">Code</TD>
												<TD style="WIDTH: 11px; HEIGHT: 22px">:</TD>
												<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox id="TXT_CODE1" runat="server" Enabled="False" Width="40px"></asp:textbox>&nbsp;
													<asp:textbox id="TXT_CODE2" runat="server" Enabled="False" Width="80px">Auto</asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 90px; HEIGHT: 22px">Start Number</TD>
												<TD style="WIDTH: 11px; HEIGHT: 22px">:</TD>
												<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox onkeypress="return numbersonly()" id="TXT_START_NUM" runat="server" CssClass="mandatory"
														Width="128px" MaxLength="10"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 90px; HEIGHT: 22px">End Number</TD>
												<TD style="WIDTH: 11px; HEIGHT: 22px">:</TD>
												<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox onkeypress="return numbersonly()" id="TXT_END_NUM" runat="server" CssClass="mandatory"
														Width="128px" MaxLength="10"></asp:textbox></TD>
											</TR>
										</TABLE>
									</TD>
									<TD width="50%">
										<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
											<TR>
												<TD style="WIDTH: 118px; HEIGHT: 20px" width="118"></TD>
												<TD style="WIDTH: 4px; HEIGHT: 20px"></TD>
												<TD style="HEIGHT: 20px"></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 118px">Currency</TD>
												<TD style="WIDTH: 4px; HEIGHT: 22px">:</TD>
												<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox id="TXT_CURRENCY" runat="server" CssClass="mandatory" Width="144px"></asp:textbox><asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAMA" runat="server" Visible="False"></asp:label></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 118px; HEIGHT: 19px">Value</TD>
												<TD style="WIDTH: 4px; HEIGHT: 19px">:</TD>
												<TD class="TDBGColorValue" style="HEIGHT: 19px"><asp:textbox onkeypress="return numbersonly()" id="TXT_VALUE" runat="server" CssClass="mandatory"
														Width="144px" MaxLength="12"></asp:textbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 118px; HEIGHT: 22px"></TD>
												<TD style="WIDTH: 4px; HEIGHT: 22px"></TD>
												<TD style="HEIGHT: 22px"><asp:label id="LBL_JENIS" runat="server" Visible="False"></asp:label><asp:label id="LBL_CODE" runat="server" Visible="False"></asp:label><asp:label id="LBL_SEQ_ID" runat="server" Visible="False"></asp:label><asp:label id="LBL_SAVE_MODE" runat="server" Visible="False">1</asp:label></TD>
											</TR>
										</TABLE>
									</TD>
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
							<P>Existing Data</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="Area_ID" HeaderText="AreaID"></asp:BoundColumn>
									<asp:BoundColumn DataField="AREA_NAME" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AF_CODE" HeaderText="Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Min_Val" HeaderText="Start Number">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Max_Val" HeaderText="End Number">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Currency" HeaderText="Currency">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="_Value" HeaderText="Value">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete1" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
									<asp:BoundColumn Visible="False" DataField="Area_ID" HeaderText="AreaID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SEQ_ID" HeaderText="SEQ_ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="AREA_NAME" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AF_CODE" HeaderText="Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Min_Val" HeaderText="Start Number">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Max_Val" HeaderText="End Number">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Currency" HeaderText="Currency">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="_Value" HeaderText="Value">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS1" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="Linkbutton1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="STATUS" HeaderText="STATUS"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2">&nbsp;</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
