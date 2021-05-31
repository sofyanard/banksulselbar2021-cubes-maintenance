<%@ Page language="c#" Codebehind="MenuItems.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.MenuItems" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MenuItems</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="Table4">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>General Parameter: Menu</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A><A href="../Body.aspx"><IMG src="../Image/MainMenu.jpg"></A><A href="../Logout.aspx" target="_top"><IMG src="../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">General Information</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 17px">Module</TD>
									<TD style="WIDTH: 15px; HEIGHT: 17px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:dropdownlist id="DDL_MODULEID" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_MODULEID_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 22px">Menu Header 1</TD>
									<TD style="WIDTH: 15px; HEIGHT: 22px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SUBMODULEID_ADD" runat="server" AutoPostBack="True" Enabled="False" onselectedindexchanged="DDL_SUBMODULEID_ADD_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Menu Parent</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_MENUPARENTCODE" runat="server" Enabled="False"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Menu Code</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:TextBox id="TXT_MENUCODE" runat="server" Enabled="False"></asp:TextBox></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="150">Menu Display</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:TextBox id="TXT_MENUDISPLAY" runat="server" Enabled="False" Width="250px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="150">Track Code</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_TRACKCODE" runat="server" Width="250px" Enabled="False"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="150">URL</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_TM_LINKNAME" runat="server" Enabled="False" Width="250px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="150">QueryString</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_TM_PARSINGPARAM" runat="server" Enabled="False" Width="250px"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" colSpan="2"><asp:label id="LBL_SqlQuery" runat="server" Visible="False"></asp:label><asp:button id="BTN_NEW" runat="server" Width="86px" CssClass="Button1" Text="New" onclick="BTN_NEW_Click"></asp:button>&nbsp;
							<asp:button id="Button2" runat="server" Width="86px" CssClass="Button1" Text="Submit" Visible="False" onclick="Button2_Click"></asp:button>&nbsp;
							<asp:button id="Button3" runat="server" Width="86px" CssClass="Button1" Text="Cancel" Visible="False" onclick="Button3_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD vAlign="top" colSpan="2"><asp:dropdownlist id="DDL_SUBMODULEID" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_SUBMODULEID_SelectedIndexChanged"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="2"><ASP:DATAGRID id="DatGrd" runat="server" Width="100%" CellPadding="1" AutoGenerateColumns="False"
								AllowPaging="True" AllowSorting="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="MENUCODE" HeaderText="Menu Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SUBMODULEDISPLAY" HeaderText="Menu Header 1">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MENUPARENTDISPLAY" HeaderText="Menu Header 2">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MENUDISPLAY" HeaderText="Menu Header 3">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MENUHEADER4" HeaderText="Menu Header 4">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TRACKCODE" HeaderText="Track">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="TM_PARSINGPARAM" HeaderText="QueryString">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LinkButton1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="SUBMODULEID" HeaderText="SUBMODULEID"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
