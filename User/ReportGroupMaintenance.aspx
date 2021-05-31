<%@ Page language="c#" Codebehind="ReportGroupMaintenance.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.User.ReportGroupMaintenance" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReportGroupMaintenance</title>
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
						<TD height="41">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="60%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Module</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_MODULEID" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_MODULEID_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
						<TD height="41" align="right">
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="75%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Group Name</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:TextBox id="TXT_FINDGROUP" runat="server"></asp:TextBox>
										<asp:Button id="BTN_SEARCH" runat="server" Text="Find" onclick="BTN_SEARCH_Click"></asp:Button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2">
							<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="510px"></rsweb:ReportViewer>
                        </TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
