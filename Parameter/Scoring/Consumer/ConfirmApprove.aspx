<%@ Page language="c#" Codebehind="ConfirmApprove.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Consumer.ConfirmApprove" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ConfirmApprove</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_entries.html" -->
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdHeader1" colSpan="2">Delete Confirmation</TD>
				</TR>
				<TR>
					<td align="left">
						<P>&nbsp;Delete data&nbsp;in dimention,&nbsp;will reset&nbsp;any data in model :</P>
					</td>
				</TR>
				<TR id="tr_modelscoring" runat="server">
					<td align="left">&nbsp;Scoring Model
						<asp:table id="Table2" runat="server" width="100%"></asp:table></td>
				</TR>
				<TR id="tr_modelrange" runat="server">
					<td align="left">&nbsp;Range Model
						<asp:table id="TBL_RANGE" runat="server" width="100%"></asp:table></td>
				</TR>
				<TR id="tr_modellimit" runat="server">
					<td align="left">&nbsp;Limit Model
						<asp:table id="TBL_LIMIT" runat="server" width="100%"></asp:table></td>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="6"><INPUT onclick="javascript:window.close()" type="button" value="Close"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
