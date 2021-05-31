<%@ Page language="c#" Codebehind="DeleteConfirmation.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Consumer.DeleteConfirmation" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DeleteConfirmation</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_entries.html" -->
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdHeader1">Delete Confirmation</TD>
				</TR>
				<TR>
					<td align="left">
						<P>&nbsp;Delete data&nbsp;in dimention,&nbsp;will reset&nbsp;any data in model that 
							related with&nbsp;dimention&nbsp;:</P>
					</td>
				</TR>
				<TR id="tr_modelscoring" runat="server">
					<td align="left">&nbsp;<b>Scoring Model</b>
						<asp:table id="TBL_SCORING" runat="server" width="100%"></asp:table></td>
				</TR>
				<TR>
					<td></td>
				</TR>
				<TR id="tr_modelrange" runat="server">
					<td align="left">&nbsp;<b>Range Model</b>
						<asp:table id="TBL_RANGE" runat="server" width="100%"></asp:table></td>
				</TR>
				<TR>
					<td></td>
				</TR>
				<TR id="tr_modellimit" runat="server">
					<td align="left">&nbsp;<b>Limit Model</b>
						<asp:table id="TBL_LIMIT" runat="server" width="100%"></asp:table></td>
				</TR>
				<TR>
					<td></td>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="6">
						<INPUT onclick="javascript:window.close()" type="button" value="Close">&nbsp;&nbsp;
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
