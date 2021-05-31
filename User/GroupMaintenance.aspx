<%@ Page language="c#" Codebehind="GroupMaintenance.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.User.GroupMaintenance" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GroupMaintenance</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../include/cek_mandatoryOnly.html" -->
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="TableHeader">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Maker: Group 
											Maintenance</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="../Body.aspx"><IMG src="../Image/MainMenu.jpg"></A><A href="../Logout.aspx" target="_top"><IMG src="../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2" height="41"><asp:hyperlink id="HyperLink1" runat="server" NavigateUrl="User.aspx" Font-Bold="True">User Maintenance</asp:hyperlink>&nbsp;|
							<asp:hyperlink id="HyperLink2" runat="server" Font-Bold="True">Group Maintenance</asp:hyperlink></TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Module</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_MODULEID" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_MODULEID_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Group Name</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:TextBox id="TXT_FINDGROUP" runat="server"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Group Upliner</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:TextBox id="TXT_FINDUPLINER" runat="server"></asp:TextBox>
										&nbsp;
										<asp:RadioButtonList id="RDL_UPLINER" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
											Visible="False">
											<asp:ListItem Value="SG_GRPUNAME">Small</asp:ListItem>
											<asp:ListItem Value="SG_MDLUNAME">Middle</asp:ListItem>
											<asp:ListItem Value="SG_CORUNAME">Corporate</asp:ListItem>
										</asp:RadioButtonList></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 129px" align="right" colSpan="3"><asp:button id="BTN_SEARCH" runat="server" Width="100px" Text="Search" onclick="BTN_SEARCH_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD colSpan="2" align="right"><ASP:DATAGRID id="DatGrd" runat="server" Width="100%" CellPadding="1" AutoGenerateColumns="False"
								AllowPaging="True" AllowSorting="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="GROUPID" SortExpression="GROUPID" HeaderText="Group ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SG_GRPNAME" SortExpression="SG_GRPNAME" HeaderText="Group Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="175px"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LinkButton3" runat="server" CommandName="menuAccess">Menu Access</asp:LinkButton>&nbsp;
											<asp:LinkButton id="LinkButton1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID>
							<asp:Button id="BTN_GENERATE" runat="server" Text="Re-Generate All Menu" Width="175px" Visible="False" onclick="BTN_GENERATE_Click"></asp:Button></TD>
					</TR>
					<TR>
						<TD colSpan="2" align="center">
							<asp:Label id="LBL_SqlStatement" runat="server" Visible="False"></asp:Label>&nbsp;
							<asp:label id="LBL_RESULT" runat="server" Font-Bold="True" ForeColor="Red"></asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2">Detail Information</TD>
					</TR>
					<TR>
						<TD class="td" style="WIDTH: 474px" vAlign="top" width="474"><TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 17px">Group ID</TD>
									<TD style="WIDTH: 15px; HEIGHT: 17px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:textbox id="TXT_GROUPID" runat="server" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Group Description</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_SG_GRPNAME" runat="server" Width="175px" CssClass="mandatory"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Group Level</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_GRPLEVEL" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Module Access</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:checkboxlist id="CHK_MODULEID" runat="server" AutoPostBack="True" Width="100%" RepeatColumns="2" onselectedindexchanged="CHK_MODULEID_SelectedIndexChanged"></asp:checkboxlist></TD>
								</TR>
							</TABLE>
							<TABLE id="SMEUpliner" cellSpacing="0" cellPadding="0" width="100%" runat="Server">
								<TR>
									<TD style="WIDTH: 129px" align="right"><STRONG>SME :</STRONG></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Group Upliner (Small)</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_GRPUPLINER_SME" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Group Upliner (Middle)</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_MDLUPLINER" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Group Upliner (Corp)</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_CORUPLINER" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Group Upliner (CRG)</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_CRGUPLINER" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Group Upliner (Micro)</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_MCRUPLINER" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Group Unit</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_SG_GRPUNIT" runat="server" Width="125px">
											<asp:ListItem Value="">- PILIH -</asp:ListItem>
											<asp:ListItem Value="BU">BU</asp:ListItem>
											<asp:ListItem Value="CO">CO</asp:ListItem>
											<asp:ListItem Value="CRM">CRM</asp:ListItem>
											<asp:ListItem Value="BO">BO</asp:ListItem>
										</asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Business Unit</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:DropDownList id="DDL_SG_BUSSUNITID" runat="server"></asp:DropDownList></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Micro User</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_SG_GRPSUPER" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
							<TABLE id="ConsumerUpliner" cellSpacing="0" cellPadding="0" width="100%" runat="Server">
								<TR>
									<TD style="WIDTH: 129px" align="right"><STRONG>Consumer&nbsp;:</STRONG></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Group Upliner</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_GRPUPLINER_CON" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
							<TABLE id="CCUpliner" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<TR>
									<TD style="WIDTH: 129px" align="right"><STRONG>Credit Card&nbsp;:</STRONG></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Group Upliner</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_GRPUPLINER_CC" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Approval Group</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:checkbox id="CHK_SG_APPRSTA" runat="server" AutoPostBack="True" Text="(check for yes)" oncheckedchanged="CHK_SG_APPRSTA_CheckedChanged"></asp:checkbox></TD>
								</TR>
							</TABLE>
							<TABLE id="SMEApproval" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<TR>
									<TD style="WIDTH: 129px" align="right"><STRONG>SME:</STRONG></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Approval Track</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_APRVTRACK_SME" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Group Pair</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_MITRARM_SME" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Loan Review Approval Track</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_LMSAPRVTRACK_SME" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Watchlist Approval Track</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_WTCAPRVTRACK_SME" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
							<TABLE id="ConsumerApproval" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<TR>
									<TD style="WIDTH: 129px" align="right"><STRONG>Consumer :</STRONG></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Approval Track</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_APRVTRACK_CON" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Group Pair</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_MITRARM_CON" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
							<TABLE id="CCApproval" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<TR>
									<TD style="WIDTH: 129px" align="right"><STRONG>Credit Card&nbsp;:</STRONG></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Approval Group</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_APRVTRACK_CC" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Group Pair</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SG_MITRARM_CC" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2"><asp:button id="BTN_NEW" runat="server" Width="70px" CssClass="Button1" Text="New" onclick="BTN_NEW_Click"></asp:button>&nbsp;<asp:button id="BTN_SAVE" runat="server" Width="70px" CssClass="Button1" Text="Save" Visible="False" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL" runat="server" Width="70px" CssClass="Button1" Text="Cancel" Visible="False" onclick="BTN_CANCEL_Click"></asp:button><asp:checkbox id="CHK_ISNEW" runat="server" Visible="False"></asp:checkbox></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
