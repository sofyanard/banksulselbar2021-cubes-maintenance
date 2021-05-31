<%@ Page language="c#" Codebehind="HouseInfo.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Area.Consumer.HouseInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>House Information Parameter</title>
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
						<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table8" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
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
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;House Information&nbsp;Maker</TD>
				</TR>
				<TR>
					<TD class="td">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD width="50%">
									<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%">
										<TR>
											<TD class="TDBGColor1" width="200">City</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CITY" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_CITY_SelectedIndexChanged"></asp:dropdownlist></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" width="200">Project Code</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PROJECT_CODE" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_PROJECT_CODE_SelectedIndexChanged"></asp:dropdownlist></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="HEIGHT: 18px">House Type</TD>
											<TD style="WIDTH: 5px; HEIGHT: 18px">:</TD>
											<TD class="TDBGColorValue" style="HEIGHT: 18px"><asp:dropdownlist id="DDL_HOUSE_TYPE" runat="server"></asp:dropdownlist></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Occupancy Type</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_OCCUPANCY" runat="server" Width="48px"
													MaxLength="1"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="HEIGHT: 22px">Type Name</TD>
											<TD style="WIDTH: 5px; HEIGHT: 22px">:</TD>
											<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_TYPENAME" runat="server" Width="99%" MaxLength="30"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="HEIGHT: 20px">Land&nbsp;Area</TD>
											<TD style="WIDTH: 5px; HEIGHT: 20px">:</TD>
											<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return numbersonly()" id="TXT_LAND_AREA" runat="server" Columns="6"
													MaxLength="10">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Building&nbsp;Area</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_BUILDING_AREA" runat="server" Columns="6"
													MaxLength="10">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Year Build</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_YEAR_BUILD" runat="server" MaxLength="4"
													Columns="6"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Building Market Price</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_MARKET_PRICE" onblur="FormatCurrency(this)" runat="server" Columns="30"
													MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Building Appraisal Price 1</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_BUILD_APPR_1" onblur="FormatCurrency(this)" runat="server" ToolTip="0" Columns="30"
													MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Land Appraisal Price 1</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_LAND_APPR_1" onblur="FormatCurrency(this)" runat="server" Columns="30" MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Building Force Price 1</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_BUILD_FORCE_1" onblur="FormatCurrency(this)" runat="server" Columns="30"
													MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Land Force Price 1</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_LAND_FORCE_1" onblur="FormatCurrency(this)" runat="server" Columns="30"
													MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Remarks</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_REMARKS" runat="server" Width="99%" MaxLength="30"></asp:textbox></TD>
										</TR>
									</TABLE>
								</TD>
								<TD>
									<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
										<TR>
											<TD class="TDBGColorValue" style="WIDTH: 175px" width="175">&nbsp;</TD>
											<TD class="TDBGColorValue" style="WIDTH: 5px">&nbsp;</TD>
											<TD class="TDBGColorValue">&nbsp;
												<asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label></TD>
										</TR>
										<TR>
											<TD class="TDBGColorValue" style="WIDTH: 175px" width="175">&nbsp;</TD>
											<TD class="TDBGColorValue" style="WIDTH: 5px">&nbsp;</TD>
											<TD class="TDBGColorValue">&nbsp;
												<asp:label id="LBL_SAVEMODE" runat="server" Visible="False"></asp:label></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 175px">Bed Rooms</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_BED" runat="server" Width="64px" MaxLength="5"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 175px">Bath Rooms</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_BATH" runat="server" Width="63px" MaxLength="5"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 175px">Garage</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:checkbox id="CB_GARAGE" runat="server"></asp:checkbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 175px; HEIGHT: 12px">Phone</TD>
											<TD style="WIDTH: 5px; HEIGHT: 12px">:</TD>
											<TD class="TDBGColorValue" style="HEIGHT: 12px"><asp:checkbox id="CB_PHONE" runat="server"></asp:checkbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 175px; HEIGHT: 21px">PAM</TD>
											<TD style="WIDTH: 5px; HEIGHT: 21px">:</TD>
											<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:checkbox id="CB_PAM" runat="server"></asp:checkbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 175px; HEIGHT: 23px">Electricity</TD>
											<TD style="WIDTH: 5px; HEIGHT: 23px">:</TD>
											<TD class="TDBGColorValue" style="HEIGHT: 23px">
												<asp:textbox id="TXT_ELEC" runat="server" Width="96px" MaxLength="20"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 175px; HEIGHT: 24px">Land Market Price</TD>
											<TD style="WIDTH: 5px; HEIGHT: 24px">:</TD>
											<TD class="TDBGColorValue" style="HEIGHT: 24px"><asp:textbox id="TXT_LAND_MARKET" onblur="FormatCurrency(this)" runat="server" Columns="30" MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 175px">Building Appraisal Price 2</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_BUILD_APPR_2" onblur="FormatCurrency(this)" runat="server" Columns="30"
													MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 175px">Land Appraisal Price 2</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_LAND_APPR_2" onblur="FormatCurrency(this)" runat="server" Columns="30" MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 175px">Building Force Price 2</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_BUILD_FORCE_2" onblur="FormatCurrency(this)" runat="server" Columns="30"
													MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 175px">Land Force Price 2</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_LAND_FORCE_2" onblur="FormatCurrency(this)" runat="server" Columns="30"
													MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 175px">Mata Uang</TD>
											<TD style="WIDTH: 5px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_MATAUANG" runat="server" Width="96px" MaxLength="20"></asp:textbox></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button>
						<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Current&nbsp;House Information&nbsp;Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2" style="HEIGHT: 66px"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn HeaderText="No">
									<HeaderStyle Width="5%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PROYEK_ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PROYEK_NAME" HeaderText="Project  Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="TYPE_CODE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TIPE" HeaderText="House Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TYPE_DESCRIPTION" HeaderText="Type Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ID_KOTA">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
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
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn HeaderText="No">
									<HeaderStyle Width="5%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PROYEK_ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PROYEK_NAME" HeaderText="Project  Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="TYPE_CODE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TIPE" HeaderText="House Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TYPE_DESCRIPTION" HeaderText="Type Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="STATUS" HeaderText="Status">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ID_KOTA">
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
					<TD class="TDBGColor2" colSpan="2">&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
