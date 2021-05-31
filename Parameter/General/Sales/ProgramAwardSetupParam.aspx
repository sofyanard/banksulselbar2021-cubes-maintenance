<%@ Page language="c#" Codebehind="ProgramAwardSetupParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Sales.ProgramAwardSetupParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProgramAwardSetupParam</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!-- #include file="../../../include/cek_entries.html" -->
		<!-- #include file="../../../include/cek_mandatory.html" -->
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
							<TABLE id="Table2">
							</TABLE>
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD style="HEIGHT: 41px" width="400">
										<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
											border="0">
											<TR>
												<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Maintenance : 
														Sales Commission</B></TD>
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
						<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Program Award&nbsp;Setup Maker</TD>
					</TR>
					<TR>
						<TD class="tdSmallHeader" colSpan="2">Award Program</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 20px" width="227">Award Program</TD>
									<TD style="WIDTH: 6px; HEIGHT: 20px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox id="TXT_AWARD" runat="server" Width="144px" CssClass="mandatory" MaxLength="50"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px">Start Date</TD>
									<TD style="WIDTH: 6px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox onkeypress="return numbersonly()" id="TXT_DATE_START" runat="server" Width="32px"
											MaxLength="2"></asp:textbox>&nbsp;
										<asp:dropdownlist id="DDL_MONTH_START" runat="server"></asp:dropdownlist>&nbsp;
										<asp:textbox onkeypress="return numbersonly()" id="TXT_YEAR_START" runat="server" Width="64px"
											MaxLength="4"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 22px">End Date</TD>
									<TD style="WIDTH: 6px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox onkeypress="return numbersonly()" id="TXT_DATE_END" runat="server" Width="32px"
											MaxLength="2"></asp:textbox>&nbsp;
										<asp:dropdownlist id="DDL_MONTH_END" runat="server"></asp:dropdownlist>&nbsp;
										<asp:textbox onkeypress="return numbersonly()" id="TXT_YEAR_END" runat="server" Width="64px"
											MaxLength="4"></asp:textbox></TD>
								</TR>
							</TABLE>
							<asp:label id="LBL_JENIS_PA" runat="server" Visible="False"></asp:label><asp:label id="LBL_PA_ID" runat="server" Visible="False"></asp:label><asp:label id="LBL_SEQ_ID" runat="server" Visible="False"></asp:label>
							<asp:Label id="LBL_SAVE_MODE1" runat="server" Visible="False">1</asp:Label></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD class="tdSmallHeader" vAlign="top" align="left" width="50%" colSpan="2">
							<P>Existing Award Program Data</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_AWARDPRO_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False"
								PageSize="5" AllowPaging="True">
								<Columns>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RadioButton12" runat="server"></asp:RadioButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="PA_ID" HeaderText="PA_ID">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PA_DESC" HeaderText="Award Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PA_STARTDATE" HeaderText="Start Date">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PA_ENDDATE" HeaderText="End Date">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit112" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete112" runat="server" CommandName="delete">Delete</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDetail112" runat="server" CommandName="detail">Detail</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="tdSmallHeader" colSpan="2">Maker Award Program Request</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_AWARDPRO_REQUEST" runat="server" Width="100%" AutoGenerateColumns="False"
								PageSize="5" AllowPaging="True">
								<Columns>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RadioButton11" runat="server"></asp:RadioButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="PA_ID" HeaderText="PA_ID">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SEQ_ID" HeaderText="SEQ_ID">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PA_DESC" HeaderText="Award Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PA_STARTDATE" HeaderText="Start Date">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PA_ENDDATE" HeaderText="End Date">
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
											<asp:LinkButton id="lnk_RfEdit11" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete11" runat="server" CommandName="delete">Delete</asp:LinkButton>&nbsp;
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="STATUS" HeaderText="STATUS"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdSmallHeader" vAlign="top" align="center" width="50%" colSpan="2">&nbsp;Sub 
							Award Program</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 20px" width="227">Sub Award 
										Program</TD>
									<TD style="WIDTH: 6px; HEIGHT: 20px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox id="TXT_AWARD_SUB" runat="server" Width="100%" MaxLength="50"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px">Sales Person Type</TD>
									<TD style="WIDTH: 6px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:dropdownlist id="DDL_SALES_TYPE" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px">Level</TD>
									<TD style="WIDTH: 6px; HEIGHT: 22px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:dropdownlist id="DDL_LEVEL" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 18px">Wilayah Type</TD>
									<TD style="WIDTH: 6px; HEIGHT: 18px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 18px"><asp:dropdownlist id="DDL_WILAYAH_TYPE" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 20px">Period Type</TD>
									<TD style="WIDTH: 6px; HEIGHT: 20px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return numbersonly()" id="TXT_PERIOD" runat="server" Width="64px"></asp:textbox>&nbsp;
										<asp:dropdownlist id="DDL_PERIOD_TYPE" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 22px">Number of WInner</TD>
									<TD style="WIDTH: 6px; HEIGHT: 22px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox onkeypress="return numbersonly()" id="TXT_WINNER" runat="server" Width="64px" MaxLength="2"></asp:textbox>&nbsp; 
										Person (0:all)</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px">Criteria Type</TD>
									<TD style="WIDTH: 6px; HEIGHT: 22px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:dropdownlist id="DDL_CRITERIA_TYPE" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 16px">Criteria #</TD>
									<TD style="WIDTH: 6px; HEIGHT: 16px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 16px"><asp:dropdownlist id="DDL_CRITERIASUB" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_CRITERIASUB_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 22px">- 1 Criteria</TD>
									<TD style="WIDTH: 6px; HEIGHT: 22px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px">JABOTABEK:&nbsp;
										<asp:textbox onkeypress="return numbersonly()" id="TXT_JABOTABEK" runat="server" Width="136px"
											Enabled="False" MaxLength="12">0</asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										Non JABOTABEK:
										<asp:textbox onkeypress="return numbersonly()" id="TXT_NON_JABOTABEK" runat="server" Width="136px"
											Enabled="False" MaxLength="12">0</asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 80px">Criteria With Formula</TD>
									<TD style="WIDTH: 6px; HEIGHT: 80px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 80px"><asp:textbox id="TXT_FORMULA" runat="server" Width="100%" Enabled="False" TextMode="MultiLine"
											Height="86px" MaxLength="500"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 22px">Criteria Description</TD>
									<TD style="WIDTH: 6px; HEIGHT: 22px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox id="TXT_CRITERIA_DESC" runat="server" Width="100%" TextMode="MultiLine" Height="86px"
											MaxLength="400"></asp:textbox></TD>
								</TR>
							</TABLE>
							<asp:label id="LBL_SEQID_SUB" runat="server" Visible="False"></asp:label><asp:label id="LBL_SUBID" runat="server" Visible="False"></asp:label><asp:label id="LBL_JENIS_SUB" runat="server" Visible="False"></asp:label>
							<asp:Label id="LBL_SAVE_MODE2" runat="server" Visible="False">1</asp:Label></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2"><asp:button id="BTN_SAVE_SUB" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_SUB_Click"></asp:button><asp:button id="BTN_CANCEL_SUB" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_SUB_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD class="tdSmallHeader" vAlign="top" align="center" width="50%" colSpan="2">Existing 
							Sub Award Program Data</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_SUB_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<Columns>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:RadioButton id="RadioButton1" runat="server"></asp:RadioButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="PA_ID" HeaderText="PA_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBID" HeaderText="PA_SUBID"></asp:BoundColumn>
									<asp:BoundColumn DataField="PA_SUBDESC" HeaderText="Sub Award Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AGENTYPE_DESC" HeaderText="Sales Person Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Area" HeaderText="Wilayah Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Periode" HeaderText="Period Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Winner" HeaderText="Number of Winner">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CRITERIA_DESC" HeaderText="Criteria Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit21" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete21" runat="server" CommandName="delete">Delete</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDetail21" runat="server" CommandName="detail">Detail</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBPERIOD" HeaderText="PA_SUBPERIOD"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBTYPEPERIOD" HeaderText="PA_SUBTYPEPERIOD"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBAREATYPE" HeaderText="PA_SUBAREATYPE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBWINNER" HeaderText="PA_SUBWINNER"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_FORMULA" HeaderText="PA_FORMULA"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_FORMULATYPE" HeaderText="PA_FORMULATYPE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBCRITERIATYPE" HeaderText="PA_SUBCRITERIATYPE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_MINCRITERIA" HeaderText="PA_MINCRITERIA"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_MINCRITERIA1" HeaderText="PA_MINCRITERIA1"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBCRITEDESC" HeaderText="PA_SUBCRITEDESC"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AGENTYPE_ID" HeaderText="AGENTYPE_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="LEVEL_CODE" HeaderText="LEVEL_CODE"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="tdSmallHeader" vAlign="top" align="center" width="50%" colSpan="2">Maker 
							Sub Award Program Request</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_SUBAWARD_REQUEST" runat="server" Width="100%" AutoGenerateColumns="False"
								PageSize="5" AllowPaging="True">
								<Columns>
									<asp:TemplateColumn Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:RadioButton id="Radiobutton32" runat="server"></asp:RadioButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="PA_ID" HeaderText="PA_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBID" HeaderText="PA_SUBID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SEQ_ID" HeaderText="SEQ_ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="PA_SUBDESC" HeaderText="Sub Award Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AGENTYPE_DESC" HeaderText="Sales Person Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Area" HeaderText="Wilayah Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Periode" HeaderText="Period Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Winner" HeaderText="Number of Winner">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CRITERIA_DESC" HeaderText="Criteria Type">
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
											<asp:LinkButton id="lnk_RfEdit22" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete22" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBPERIOD" HeaderText="PA_SUBPERIOD"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBTYPEPERIOD" HeaderText="PA_SUBTYPEPERIOD"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBAREATYPE" HeaderText="PA_SUBAREATYPE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBWINNER" HeaderText="PA_SUBWINNER"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_FORMULA" HeaderText="PA_FORMULA"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_FORMULATYPE" HeaderText="PA_FORMULATYPE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBCRITERIATYPE" HeaderText="PA_SUBCRITERIATYPE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_MINCRITERIA" HeaderText="PA_MINCRITERIA"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_MINCRITERIA1" HeaderText="PA_MINCRITERIA1"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBCRITEDESC" HeaderText="PA_SUBCRITEDESC"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AGENTYPE_ID" HeaderText="AGENTYPE_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="LEVEL_CODE" HeaderText="LEVEL_CODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="STATUS" HeaderText="STATUS"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdSmallHeader" vAlign="top" align="center" width="50%" colSpan="2">Award 
							Cup</TD>
					</TR>
					<TR>
						<TD class="r" vAlign="top" align="center" width="50%" colSpan="2">
							<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 20px" width="227">Position #</TD>
									<TD style="WIDTH: 6px; HEIGHT: 20px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return numbersonly()" id="TXT_POSITION" runat="server" Width="64px"
											MaxLength="10"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px">Description</TD>
									<TD style="WIDTH: 6px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox id="TXT_DESC" runat="server" Width="100%" MaxLength="50"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 22px">Wilayah</TD>
									<TD style="WIDTH: 6px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px">JABOTABEK:
										<asp:textbox onkeypress="return numbersonly()" id="TXT_WIL_JBTK" runat="server" Width="136px"
											MaxLength="12"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										Non JABOTABEK:
										<asp:textbox onkeypress="return numbersonly()" id="TXT_WIL_NONJBTK" runat="server" Width="136px"
											MaxLength="12"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 227px; HEIGHT: 22px">Remark</TD>
									<TD style="WIDTH: 6px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox id="TXT_REMARK" runat="server" Width="100%" MaxLength="200"></asp:textbox></TD>
								</TR>
							</TABLE>
							<asp:Label id="LBL_CUPID" runat="server" Visible="False"></asp:Label>
							<asp:Label id="LBL_SEQ_NO" runat="server" Visible="False"></asp:Label>
							<asp:Label id="LBL_JENIS_CUP" runat="server" Visible="False"></asp:Label>
							<asp:Label id="LBL_SAVE_MODE3" runat="server" Visible="False">1</asp:Label>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2"><asp:button id="BTN_SAVE_AWARD" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_AWARD_Click"></asp:button><asp:button id="BTN_CANCEL_AWARD" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_AWARD_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD class="tdSmallHeader" vAlign="top" align="center" width="50%" colSpan="2">Existing 
							Award Cup Data</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_AWARD_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False"
								PageSize="5" AllowPaging="True">
								<Columns>
									<asp:BoundColumn DataField="SEQ_ID" HeaderText="Pos #">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESCRIPTION" HeaderText="Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AWARD_VALUE" HeaderText="Jabotabek">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AWARD_VALUE1" HeaderText="Non Jabotabek">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AWARD_MEMO" HeaderText="Remark">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit31" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete31" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="PA_ID" HeaderText="PA_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBID" HeaderText="PA_SUBID"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="tdSmallHeader" vAlign="top" align="center" width="50%" colSpan="2">Maker 
							Award Cup Existing</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_AWARD_REQUEST" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<Columns>
									<asp:BoundColumn DataField="SEQ_ID" HeaderText="Pos #">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESCRIPTION" HeaderText="Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AWARD_VALUE" HeaderText="Jabotabek">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AWARD_VALUE1" HeaderText="Non Jabotabek">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AWARD_MEMO" HeaderText="Remark">
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
											<asp:LinkButton id="lnk_RfEdit32" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete32" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="PA_ID" HeaderText="PA_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PA_SUBID" HeaderText="PA_SUBID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SEQ_NO" HeaderText="SEQ_NO"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="STATUS" HeaderText="STATUS"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2"></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
