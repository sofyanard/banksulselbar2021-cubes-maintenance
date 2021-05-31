<%@ Page language="c#" Codebehind="Branch.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Area.Branch" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Branch</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../include/OpenWindow.html" -->
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="Table4">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Area&nbsp;Parameter: 
											Branch</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A><A href="../Body.aspx"><IMG src="../../Image/MainMenu.jpg"></A><A href="../Logout.aspx" target="_top"><IMG src="../../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">General Information</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 17px">Branch Code</TD>
									<TD style="WIDTH: 15px; HEIGHT: 17px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:textbox id="TXT_BRANCH_CODE" runat="server" Columns="6"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 22px">Branch Name</TD>
									<TD style="WIDTH: 15px; HEIGHT: 22px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_BRANCH_NAME" runat="server" Enabled="False" Width="250px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Address</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_BR_ADDR" runat="server" Enabled="False" Width="250px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 129px"></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_BR_BRANCHAREA" runat="server" Enabled="False" Width="250px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Area</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_AREAID" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_AREAID_SelectedIndexChanged"></asp:dropdownlist>, 
										City:
										<asp:dropdownlist id="DDL_CITYID" runat="server"></asp:dropdownlist>
										<asp:textbox id="TXT_CBC_CODE" runat="server" Width="1px" ReadOnly="True" BackColor="Transparent"
											BorderStyle="None" ForeColor="White"></asp:textbox>
										<asp:textbox id="TXT_BR_CCOBRANCH" runat="server" Width="1px" ReadOnly="True" BackColor="Transparent"
											BorderStyle="None" ForeColor="White"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="129">Branch</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:checkbox id="CHK_ISBRANCH" runat="server" Text="(check if yes)"></asp:checkbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="129">Booking Branch</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:checkbox id="CHK_BOOKINGBRANCH" runat="server" Text="(check if yes)"></asp:checkbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="129">Branch Type</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_BRANCH_TYPE" runat="server" Enabled="False"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="129">CBC</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:textbox id="TXT_CBC" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SEARCH_CBC" onclick="openSetWindow('../../User/SearchBranch.aspx?targetFormID=Form1&amp;targetObjectID=TXT_CBC_CODE&amp;targetObjectDesc=TXT_CBC', '460', '232');"
											type="button" size="20" value="..." name="BTN_SEARCH_BRANCH" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="129">CCO Branch</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CCOBRANCH" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SEARCH_CCOBRANCH" onclick="openSetWindow('../../User/SearchBranch.aspx?targetFormID=Form1&amp;targetObjectID=TXT_BR_CCOBRANCH&amp;targetObjectDesc=TXT_CCOBRANCH', '460', '232');"
											type="button" size="20" value="..." name="BTN_SEARCH_CCOBRANCH" runat="server"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" colSpan="2">
							<asp:CheckBox id="CHK_ISNEW" runat="server" Visible="False"></asp:CheckBox><asp:button id="BTN_NEW" runat="server" Width="86px" Text="New" CssClass="Button1" onclick="BTN_NEW_Click"></asp:button>&nbsp;
							<asp:button id="BTN_SUBMIT" runat="server" Width="86px" Visible="False" Text="Submit" CssClass="Button1" onclick="BTN_SUBMIT_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL" runat="server" Width="86px" Visible="False" Text="Cancel" CssClass="Button1" onclick="BTN_CANCEL_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="2"><ASP:DATAGRID id="DatGrd" runat="server" Width="100%" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False"
								CellPadding="1">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="BRANCH_CODE" HeaderText="Branch Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_NAME" HeaderText="Branch Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CITYNAME" HeaderText="City">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AREANAME" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="REGIONDESC" HeaderText="Region">
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
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
