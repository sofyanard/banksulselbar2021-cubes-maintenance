<%@ Page language="c#" Codebehind="ParamDownload.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.ParamDownload" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ParamDownload</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width="50%" align="center"><asp:Button id="Button1" runat="server" Text="Read File" Width="175px" onclick="Button1_Click"></asp:Button></td>
					<td width="50%" align="center"><asp:Button id="Button2" runat="server" Text="Update Parameter" Width="175px" onclick="Button2_Click"></asp:Button></td>
				</tr>
				<tr>
					<td align="center" class="td"><asp:Literal ID="readResult" Runat="server"></asp:Literal></td>
					<td align="center" class="td"><asp:Literal ID="updateResult" Runat="server"></asp:Literal></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
