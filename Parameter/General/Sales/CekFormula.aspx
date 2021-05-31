<%@ Page language="c#" Codebehind="CekFormula.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Sales.CekFormula" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CekFormula</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdHeader1" colSpan="2">Formula Test</TD>
					</TR>
					<TR>
						<TD class="td" style="HEIGHT: 118px" vAlign="top" width="50%"><asp:textbox id="TXT_FORMULA" runat="server" Height="120px" TextMode="MultiLine" Width="100%"></asp:textbox></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_TEST" Runat="server" Text="Test" CssClass="button1" onclick="BTN_TEST_Click"></asp:button>&nbsp;&nbsp;</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="left" width="50%" colSpan="2">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
								</TR>
							</TABLE>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 90px; HEIGHT: 20px" width="90">Result</TD>
									<TD style="WIDTH: 11px; HEIGHT: 20px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox id="TXT_RESULT" runat="server" Width="240px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 90px">Description</TD>
									<TD style="WIDTH: 11px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox id="TXT_DESC" runat="server" Height="80px" TextMode="MultiLine" Width="100%"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"></TD>
					</TR>
				</TABLE>
				</TD></TR><TR>
					<TD class="td" colSpan="2" width="50%" align="center" vAlign="top"></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2" width="50%" align="center" vAlign="top">&nbsp;</TD>
				</TR>
				</TABLE></center>
		</form>
	</body>
</HTML>
