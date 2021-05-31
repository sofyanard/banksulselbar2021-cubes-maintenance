<%@ Page language="c#" Codebehind="JiwaServiceParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.JiwaServiceParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>JiwaServiceParam</TITLE>
		<META name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<META name="CODE_LANGUAGE" Content="C#">
		<META name="vs_defaultClientScript" content="JavaScript">
		<META name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<CENTER>
				<TABLE cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder" width="50%">
							<TABLE id="Table1">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B><asp:label id="LBL_PARAM_TITLE" runat="server"></asp:label></B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="../Body.aspx"><IMG src="../Image/MainMenu.jpg"></A>
							<A href="../Logout.aspx" target="_top"><IMG src="../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2"><asp:label id="LBL_PARAM" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD class="td" colSpan="2">
							<TABLE cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD><asp:table id="TBL_CONTENT" runat="server" Width="100%"></asp:table></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" colSpan="2"></TD>
					</TR>
				</TABLE>
			</CENTER>
		</FORM>
	</BODY>
</HTML>
