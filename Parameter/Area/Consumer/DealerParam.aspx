<%@ Page language="c#" Codebehind="DealerParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Area.Consumer.DealerParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DealerParam</title>
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
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table6" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
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
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Dealer Maker</TD>
				</TR>
				<TR>
					<TD class="td">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="WIDTH: 500px">
									<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">City</TD>
											<TD style="WIDTH: 10px">:</TD>
											<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CITY" runat="server" AutoPostBack="True" CssClass="mandatory" onselectedindexchanged="DDL_CITY_SelectedIndexChanged"></asp:dropdownlist></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Dealer Induk</TD>
											<TD style="WIDTH: 10px"></TD>
											<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_DEALER" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Dealer Name</TD>
											<TD style="HEIGHT: 10px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_DLNAME" runat="server" MaxLength="35" Columns="35"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Address</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ADDR1" runat="server" MaxLength="35" Columns="35"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">&nbsp;</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ADDR2" runat="server" MaxLength="35" Columns="35"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">&nbsp;</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ADDR3" runat="server" MaxLength="35" Columns="35"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">City</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_CITY" runat="server" Width="224px"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Zipcode</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_ZIPCODE" runat="server" MaxLength="5"
													Columns="7"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Phone Number</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PH1" runat="server" MaxLength="3" Columns="4"></asp:textbox>-
												<asp:textbox onkeypress="return numbersonly()" id="TXT_PH2" runat="server" MaxLength="15" Columns="20"></asp:textbox>ext
												<asp:textbox onkeypress="return numbersonly()" id="TXT_PH3" runat="server" MaxLength="4" Columns="5"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Fax Number</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_FX1" runat="server" MaxLength="3" Columns="4"></asp:textbox>-
												<asp:textbox onkeypress="return numbersonly()" id="TXT_FX2" runat="server" MaxLength="15" Columns="20"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">NPWP</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_NPWP" runat="server" MaxLength="35" Columns="35"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Manager</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_MANAGER" runat="server" MaxLength="35"
													Columns="35"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Sales Supervisor</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_SPV" runat="server" MaxLength="35" Columns="35"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Type</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDB_TYPE" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"></asp:radiobuttonlist></TD>
										</TR>
									</TABLE>
								</TD>
								<TD vAlign="top">
									<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%">
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">&nbsp;</TD>
											<TD style="WIDTH: 10px"></TD>
											<TD class="TDBGColorValue">
												<asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Bank Name</TD>
											<TD style="WIDTH: 10px">:</TD>
											<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_BANK_NAME" runat="server"></asp:dropdownlist></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Bank Address</TD>
											<TD style="WIDTH: 10px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_BANK_ADDR1" runat="server" MaxLength="35"
													Columns="35"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">&nbsp;</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_BANK_ADDR2" runat="server" MaxLength="35"
													Columns="35"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">&nbsp;</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_BANK_ADDR3" runat="server" MaxLength="35"
													Columns="35"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Bank City</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_BANK_CITY" runat="server" MaxLength="35" Columns="35"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Bank Zipcode</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_BANK_ZIPCODE" runat="server" MaxLength="5"
													Columns="7" Width="62px"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Account Number</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_ACC_NO" runat="server" MaxLength="35" Columns="35"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Discount Premi</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_DISC_PREMI" runat="server" MaxLength="3"
													Columns="5">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px; HEIGHT: 21px">Discount Dealer</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_DISC_DEALER" runat="server" MaxLength="3"
													Columns="5">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Interest Subsidy</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onblur="FormatCurrency(this)" id="TXT_INT_SUB" runat="server" MaxLength="15" Columns="20">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 150px">Marketing Source Code</TD>
											<TD>:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_CH_SRC" runat="server" MaxLength="10" Columns="10"></asp:textbox><asp:label id="LBL_NB" runat="server" Visible="False">1</asp:label>
												<asp:label id="LBL_CODE" runat="server" Visible="False"></asp:label></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2">
						<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label><asp:button id="BTN_SAVE" CssClass="button1" Width="70px" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Current&nbsp;Dealer Table
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DG1" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="20">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID_DEALER">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CITY_ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DLI_CODE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DLI_NAME" HeaderText="Dealer Induk">
									<HeaderStyle Width="20%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NM_DEALER" HeaderText="Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ADDRESS" HeaderText="Address">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DL_SRCCODE" HeaderText="Marketing Source Code">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
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
					<TD class="td" colSpan="2"><asp:datagrid id="DG2" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID_DEALER">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CITY_ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DLI_CODE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Dealer Induk" DataField="DLI_NAME">
									<HeaderStyle CssClass="tdSmallHeader" Width="20%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Name" DataField="NM_DEALER">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Address" DataField="ADDRESS">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn HeaderText="Marketing Source Code" DataField="DL_SRCCODE">
									<HeaderStyle CssClass="tdSmallHeader" Width="12%"></HeaderStyle>
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
									<HeaderStyle CssClass="tdSmallHeader" Width="10%"></HeaderStyle>
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
