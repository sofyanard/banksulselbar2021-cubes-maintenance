<%@ Page language="c#" Codebehind="ScoringParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.CC.ScoringParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ScoringParam</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder" width="50%">
							<TABLE id="Table1">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter : Scoring</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A><A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colspan="2">Scoring Parameter</TD>
					</TR>
					<TR>
						<TD colspan="2">
							<OBJECT classid="clsid:5220cb21-c88d-11cf-b347-00aa00a28331" VIEWASTEXT>
								<PARAM NAME="LPKPath" VALUE="../../../Include/CCScoringParm.lpk">
							</OBJECT>
							<OBJECT id="scanner" codeBase="../../../Include/CCScoringParm.ocx#version=1,0,6,0" height="485" hspace="0"
								width="100%" vspace="0" classid="clsid:40C3DEF8-FC38-452A-AAFB-E38AC94EC777" VIEWASTEXT>
								<PARAM NAME="ConnString" VALUE="<%=ViewState["connstring"]%>">
							</OBJECT>
						</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
