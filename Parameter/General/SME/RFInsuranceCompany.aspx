<%@ Page language="c#" Codebehind="RFInsuranceCompany.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFInsuranceCompany" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Perusahaan Asuransi</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include  file="../../../include/cek_all.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="Table2">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center">
										<B>Parameter Maintenance : General</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right">
							<asp:ImageButton id="BTN_BACK" runat="server" ImageUrl="/SME/Image/back.jpg" onclick="BTN_BACK_Click"></asp:ImageButton>
							<A href="../../../Body.aspx"><IMG src="/SME/Image/MainMenu.jpg"></A> <A href="../../../Logout.aspx" target="_top">
								<IMG src="/SME/Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter Insurance Company Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="200">Company Code</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_IC_ID" runat="server" onKeyup="return kutip_satu()" MaxLength="10" Columns="10"
											CssClass="mandatory"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Company Name</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_IC_DESC" runat="server" onKeyup="return kutip_satu()" MaxLength="50" Columns="30"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Address</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_IC_ADDR1" runat="server" onKeyup="return kutip_satu()" MaxLength="40" Columns="30"></asp:textbox><br>
										<asp:textbox id="TXT_IC_ADDR2" runat="server" onKeyup="return kutip_satu()" MaxLength="30" Columns="30"></asp:textbox><br>
										<asp:textbox id="TXT_IC_ADDR3" runat="server" onKeyup="return kutip_satu()" MaxLength="30" Columns="30"></asp:textbox>
									</TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="200">City</TD>
									<TD width="15"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_IC_CITY" runat="server" onKeyup="return kutip_satu()" MaxLength="30" Columns="30"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Zipcode</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_IC_ZIPCODE" runat="server" onKeyup="return kutip_satu()" MaxLength="7" Columns="10"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Contact</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_IC_CONTACT" runat="server" onKeyup="return kutip_satu()" MaxLength="50"
											Columns="30"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Current Insurance Company Table</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
							<asp:datagrid id="DGR_CURRENT" runat="server" AllowPaging="True" PageSize="5" AutoGenerateColumns="False"
								Width="100%">
								<Columns>
									<asp:BoundColumn DataField="IC_ID" HeaderText="Company Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IC_DESC" HeaderText="Company Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IC_ADDR" HeaderText="Address">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IC_ADDR1" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="IC_ADDR2" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="IC_ADDR3" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="IC_CITY" HeaderText="City">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IC_ZIPCODE" HeaderText="Zipcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IC_CONTACT" HeaderText="Contact">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE" HeaderText="ACTIVE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
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
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
							<asp:datagrid id="DGR_MAKER" runat="server" AllowPaging="True" PageSize="5" AutoGenerateColumns="False"
								Width="100%">
								<Columns>
									<asp:BoundColumn DataField="IC_ID" HeaderText="Company Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IC_DESC" HeaderText="Company Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IC_ADDR" HeaderText="Address">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IC_ADDR1" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="IC_ADDR2" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="IC_ADDR3" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="IC_CITY" HeaderText="City">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IC_ZIPCODE" HeaderText="Zipcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IC_CONTACT" HeaderText="Contact">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PENDINGSTATUS" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDINGDESC" HeaderText="Request Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RqEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RqDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
			</center>
		</form>
	</body>
</HTML>
