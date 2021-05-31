<%@ Page language="c#" Codebehind="RFProgram.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFProgram" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFProgram</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<td colSpan="3">
							<table cellSpacing="2" cellPadding="2" width="100%">
								<tr>
									<TD class="tdNoBorder"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
										<TABLE id="Table2">
											<TR>
												<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
														General</B></TD>
											</TR>
										</TABLE>
									</TD>
									<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="/SME/Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="/SME/Image/MainMenu.jpg"></A>
										<A href="../../../Logout.aspx" target="_top"><IMG src="/SME/Image/Logout.jpg"></A>
									</TD>
								</tr>
							</table>
						</td>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="3">Parameter Program Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="25%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" vAlign="top">Area</TD>
									<td vAlign="top">:</td>
									<TD><ASP:LISTBOX id="LIST_AREA" runat="server" SelectionMode="Multiple" AutoPostBack="True" Rows="10" onselectedindexchanged="LIST_AREA_SelectedIndexChanged"></ASP:LISTBOX></TD>
								</TR>
								<TR>
									<TD class="TDBGColor2" vAlign="top" colSpan="3"><asp:checkbox id="CHK_CheckAll" runat="server" AutoPostBack="True" Text="Check All" oncheckedchanged="CHK_CheckAll_CheckedChanged"></asp:checkbox></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" vAlign="top">Program</TD>
									<td vAlign="top">:</td>
									<TD><ASP:LISTBOX id="LIST_PROGRAM" runat="server" AutoPostBack="True" Rows="10" onselectedindexchanged="LIST_PROGRAM_SelectedIndexChanged"></ASP:LISTBOX></TD>
								</TR>
								<TR>
									<TD class="TDBGColor2" vAlign="top" colSpan="3"><asp:button id="BTN_EDITLISTPROGRAM" Text="Edit" Runat="server" CssClass="button1" onclick="BTN_EDITLISTPROGRAM_Click"></asp:button>&nbsp;&nbsp;
										<asp:button id="BTN_DELETELISTPROGRAM" Text="Delete" Runat="server" CssClass="button1" onclick="BTN_DELETELISTPROGRAM_Click"></asp:button></TD>
								</TR>
								<TR>
									<TD class="tdHeader1" colSpan="3">Program Detail</TD>
								</TR>
								<tr>
									<td align="center" colSpan="3">
										<table id="Table6" cellSpacing="0" cellPadding="0" width="100%">
											<TR>
												<TD class="TDBGColor1" vAlign="top">Program Code</TD>
												<TD vAlign="top">:</TD>
												<TD><asp:textbox onkeypress="return kutip_satu()" id="TXT_PROGRAMID" runat="server"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" vAlign="top">Program Name</TD>
												<TD vAlign="top">:</TD>
												<TD><asp:textbox onkeypress="return kutip_satu()" id="TXT_PROGRAMDESC" runat="server" Width="250px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" vAlign="top">Pre-Scoring</TD>
												<TD vAlign="top">:</TD>
												<TD><asp:radiobuttonlist id="RDO_WITHFAIRISAAC" runat="server" RepeatDirection="Horizontal">
														<asp:ListItem Value="1">Yes</asp:ListItem>
														<asp:ListItem Value="0" Selected="True">No</asp:ListItem>
													</asp:radiobuttonlist></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" vAlign="top">Scoring Type</TD>
												<TD vAlign="top">:</TD>
												<TD><asp:dropdownlist id="DDL_SCRID" runat="server"></asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" vAlign="top"></TD>
												<TD vAlign="top"></TD>
												<TD><asp:radiobuttonlist id="RDO_APRVFOUREYES" runat="server" RepeatDirection="Horizontal">
														<asp:ListItem Value="0" Selected="True">Two Eyes</asp:ListItem>
														<asp:ListItem Value="1">Four Eyes</asp:ListItem>
													</asp:radiobuttonlist></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" vAlign="top">Business Unit</TD>
												<TD vAlign="top"></TD>
												<TD><asp:dropdownlist id="DDL_BUSINESSUNIT" runat="server"></asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" vAlign="top">Support withdrawal</TD>
												<TD vAlign="top"></TD>
												<TD><asp:radiobuttonlist id="RDO_WITHDRAWL" runat="server" RepeatDirection="Horizontal">
														<asp:ListItem Value="1">Yes</asp:ListItem>
														<asp:ListItem Value="0" Selected="True">No</asp:ListItem>
													</asp:radiobuttonlist></TD>
											</TR>
										</table>
									</td>
								</tr>
								<TR>
									<TD class="TDBGColor2" vAlign="top" colSpan="3"><asp:button id="BTN_SAVE" Text="Save" Runat="server" CssClass="button1" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
										<asp:button id="BTN_CANCEL" Text="Cancel" Runat="server" CssClass="button1" onclick="BTN_CANCEL_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top" width="25%">
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" vAlign="top">Product</TD>
									<TD vAlign="top">:</TD>
									<TD class="TDBGColorValue"><asp:listbox id="LIST_PRODUCT" runat="server" Rows="10"></asp:listbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor2" vAlign="top" align="left" colSpan="3"><asp:button id="BTN_DELETELISTPRODUCT" Text="Delete" Runat="server" CssClass="button1" onclick="BTN_DELETELISTPRODUCT_Click"></asp:button></TD>
								</TR>
								<TR>
									<TD class="tdHeader1" colSpan="3">Add Product</TD>
								</TR>
								<TR>
									<td align="center" colSpan="3">
										<table id="Table7" cellSpacing="0" cellPadding="0" width="100%">
											<TR>
												<TD class="TDBGColor1" vAlign="top"><asp:dropdownlist id="ddl_PRODUCTID" runat="server"></asp:dropdownlist></TD>
												<TD vAlign="top"></TD>
												<TD><asp:button id="BTN_ADDPROD" Text="Add" Runat="server" CssClass="button1" onclick="BTN_ADDPROD_Click"></asp:button></TD>
											</TR>
										</table>
									</td>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" colSpan="3"><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label><asp:dropdownlist id="DDL_H_WITHFAIRISAAC" runat="server" Visible="False"></asp:dropdownlist><asp:dropdownlist id="DDL_H_SCRID" runat="server" Visible="False"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="3">Maker Request</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="3">Program</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" colSpan="3"><asp:datagrid id="Datagrid2" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
								<Columns>
									<asp:BoundColumn DataField="AREANAME" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMID" HeaderText="Program Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="Program Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="WITHFAIRISAAC" HeaderText="Pre-Scoring">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SCRDESC" HeaderText="Scoring Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AREAID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SCRID"></asp:BoundColumn>
									<asp:BoundColumn DataField="APRVFOUREYES" HeaderText="Four eyes">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="WITHDRAWL" HeaderText="Support Withdrawal">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BUSSUNITDESC" HeaderText="Business Unit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BUSINESSUNIT"></asp:BoundColumn>
									<asp:BoundColumn DataField="STATUSDESC" HeaderText="Request Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS"></asp:BoundColumn>
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
						<TD class="tdHeader1" colSpan="3">Product in Program</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" colSpan="3"><asp:datagrid id="Datagrid3" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True" onselectedindexchanged="Datagrid3_SelectedIndexChanged">
								<Columns>
									<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="Program Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTDESC" HeaderText="Product Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PROGRAMID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PRODUCTID"></asp:BoundColumn>
									<asp:BoundColumn DataField="STATUSDESC" HeaderText="Request Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_Rq2Delete" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
