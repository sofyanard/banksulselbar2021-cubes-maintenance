<%@ Page language="c#" Codebehind="AssuranceCompParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.AssuranceCompParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AssuranceCompParam</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_entries.html" -->
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table6" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													General Maker</B></TD>
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
					<TD class="tdHeader1" colSpan="2" align="center">
						Parameter Assurance Company Maker</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" width="50%" colspan="2">
						<TABLE id="Table20" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="128" style="WIDTH: 128px; HEIGHT: 25px">Code</TD>
								<TD style="WIDTH: 8px; HEIGHT: 25px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 25px">
									<asp:textbox id="TXT_CODE" runat="server" MaxLength="10" Width="60px" CssClass="mandatory" onkeypress="return numbersonly()"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 128px; HEIGHT: 20px" vAlign="top">Company Name</TD>
								<TD style="WIDTH: 8px; HEIGHT: 20px" vAlign="top"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px">
									<asp:textbox id="TXT_COMP_NAME" runat="server" MaxLength="50" Width="200px" onkeypress="return kutip_satu()"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 128px; HEIGHT: 22px" vAlign="top">Address</TD>
								<TD style="WIDTH: 8px; HEIGHT: 22px" vAlign="top"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 22px">
									<asp:textbox id="TXT_ADDRESS1" runat="server" MaxLength="50" Width="200px" onkeypress="return kutip_satu()"></asp:textbox>
									<asp:textbox id="TXT_ADDRESS2" runat="server" Width="200px" MaxLength="50" onkeypress="return kutip_satu()"></asp:textbox>
									<asp:textbox id="TXT_ADDRESS3" runat="server" Width="200px" MaxLength="50" onkeypress="return kutip_satu()"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 128px; HEIGHT: 20px" vAlign="top">City</TD>
								<TD style="WIDTH: 8px; HEIGHT: 20px" vAlign="top"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px">
									<asp:textbox id="TXT_CITY" runat="server" MaxLength="30" Width="200px" onkeypress="return kutip_satu()"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 128px; HEIGHT: 20px" vAlign="top">Zip Code</TD>
								<TD style="WIDTH: 8px; HEIGHT: 20px" vAlign="top"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px">
									<asp:textbox onkeypress="return numbersonly()" id="TXT_ZIPCODE" runat="server" MaxLength="6"
										Width="80px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 128px; HEIGHT: 20px" vAlign="top">SIBS Code</TD>
								<TD style="WIDTH: 8px; HEIGHT: 20px" vAlign="top"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px">
									<asp:textbox id="TXT_CDSIBS" runat="server" MaxLength="10" Width="80px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 128px; HEIGHT: 20px" vAlign="top">Records 
									per&nbsp;Page</TD>
								<TD style="WIDTH: 8px; HEIGHT: 20px" vAlign="top"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px">
									<asp:textbox id="TXT_PAGE" runat="server" Width="32px" MaxLength="10" Enabled="False"></asp:textbox></TD>
							</TR> <!-- Additional Field : Right --></TABLE>
						<asp:Label id="LBL_SEQ_ID" runat="server" Visible="False"></asp:Label>
						<asp:Label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:Label>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" width="50%" colspan="2">
						<asp:button id="BTN_SAVE" runat="server" CssClass="Button1" Text="Save" Width="100px" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL" runat="server" CssClass="Button1" Width="100px" Text="Cancel" onclick="BTN_CANCEL_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Existing Data</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="20"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="AS_CODE" HeaderText="Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AS_DESC" HeaderText="Company Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AS_ADDR1" HeaderText="Address">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AS_CITY" HeaderText="City">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AS_ZIPCODE" HeaderText="Zipcode">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
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
					<TD class="tdHeader1" colSpan="2">Requested Data</TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="AS_CODE" HeaderText="Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AS_DESC" HeaderText="Company Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AS_ADDR1" HeaderText="Address">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AS_CITY" HeaderText="City">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AS_ZIPCODE" HeaderText="Zipcode">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
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
										<asp:LinkButton id="Linkbutton1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="SEQ_ID" HeaderText="SEQ_ID"></asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2">&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
