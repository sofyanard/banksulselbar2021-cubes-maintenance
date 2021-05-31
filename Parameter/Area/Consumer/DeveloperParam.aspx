<%@ Page language="c#" Codebehind="DeveloperParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Area.Consumer.DeveloperParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Developer Parameter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_entries.html" -->
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
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													Area Maker</B></TD>
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
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Developer Maker
						<asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label><asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label><asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" width="50%">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="200">City</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CITY" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_CITY_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="200">Group Developer</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_GRPDEV" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_GRPDEV_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="200">Developer Name</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_DEV_NAME" runat="server" CssClass="mandatory"
										Width="344px" MaxLength="70"></asp:textbox><asp:label id="LBL_DEV_CODE" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Address</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ADDR1" runat="server" Width="344px" MaxLength="30"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">&nbsp;</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ADDR2" runat="server" Width="344px" MaxLength="30"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">&nbsp;</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ADDR3" runat="server" Width="344px" MaxLength="30"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">&nbsp;</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ADDR4" runat="server" Width="160px" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Zipcode</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_ZIPCODE" runat="server" Width="72px" MaxLength="5"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Phone Number</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PH1" runat="server" MaxLength="3" Columns="4"></asp:textbox>-
									<asp:textbox onkeypress="return numbersonly()" id="TXT_PH2" runat="server" Width="128px" MaxLength="10"></asp:textbox>ext
									<asp:textbox onkeypress="return numbersonly()" id="TXT_PH3" runat="server" MaxLength="5" Columns="6"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Fax Number</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_FN1" runat="server" MaxLength="4" Columns="4"></asp:textbox>-
									<asp:textbox onkeypress="return numbersonly()" id="TXT_FN2" runat="server" Width="128px" MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Guarantor Line</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_GRLINE" onblur="FormatCurrency(this)" runat="server" Width="176px" MaxLength="15">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Interest Subsidy</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_INSUB" onblur="FormatCurrency(this)" runat="server" Width="176px" MaxLength="15">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Remaining Guarantor's Line</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_REMGRLINE" onblur="FormatCurrency(this)" runat="server" Width="176px" MaxLength="15">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Marketing Sources Code</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_MKTSC" runat="server" Width="80px" MaxLength="10"></asp:textbox><asp:label id="LBL_NB" runat="server" Visible="False">1</asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">SIBS Code</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_CDSIBS" runat="server" MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Status Kerjasama</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDB_KERJASAMA" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
										<asp:ListItem Value="1">PKS</asp:ListItem>
										<asp:ListItem Value="0" Selected="True">Non PKS</asp:ListItem>
									</asp:radiobuttonlist><asp:checkbox id="CHK_KERJASAMA" runat="server" Visible="False"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Tanggal PKS</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PKSDATE" runat="server" MaxLength="2"
										Columns="3"></asp:textbox><asp:dropdownlist id="DDL_PKSMONTH" runat="server"></asp:dropdownlist><asp:textbox onkeypress="return numbersonly()" id="TXT_PKSYEAR" runat="server" MaxLength="4"
										Columns="4"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Tanggal Jatuh Tempo</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_EXPDATE" runat="server" MaxLength="2"
										Columns="3"></asp:textbox><asp:dropdownlist id="DDL_EXPMONTH" runat="server"></asp:dropdownlist><asp:textbox onkeypress="return numbersonly()" id="TXT_EXPYEAR" runat="server" MaxLength="4"
										Columns="4"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">No. Akta Perjanjian</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_NOAKTA" runat="server" Width="344px" MaxLength="30"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Nama Notaris</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_NOTARIS" runat="server" Width="344px" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Blocked</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_BLOCK" runat="server"></asp:checkbox></TD>
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
						<P>Current&nbsp;Developer Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="30"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn HeaderText="No">
									<HeaderStyle Width="4%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DV_CODE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DV_NAME" HeaderText="Developer Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DV_KERJASAMA" HeaderText="Kerjasama">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DV_BLOCKED" HeaderText="Blocked">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DV_GRLINE" HeaderText="Guarantor Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SISA" HeaderText="Remaining Guarantor Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DV_SRCCODE" HeaderText="Mkt Source Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="LinkDetail" runat="server" CommandName="view">Detail</asp:LinkButton>&nbsp;
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
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn HeaderText="No">
									<HeaderStyle Width="4%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DV_CODE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DV_NAME" HeaderText="Developer Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DV_KERJASAMA" HeaderText="Kerjasama">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DV_BLOCKED" HeaderText="Blocked">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DV_GRLINE" HeaderText="Guarantor Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SISA" HeaderText="Remaining Guarantor Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DV_SRCCODE" HeaderText="Mkt Source Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="STATUS" HeaderText="Status">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
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
