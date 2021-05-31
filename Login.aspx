<%@ Page language="c#" Codebehind="Login.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Login</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="style.css" type="text/css" rel="stylesheet">
		<script language="JavaScript">
			if (top != self) { top.location = self.location; }
		</script>
	</HEAD>
	<body onload="document.Form1.TXT_USERNAME.focus()" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td height="100">&nbsp;</td>
				</tr>
				<tr bgcolor="#f5f5f5">
					<td vAlign="middle" align="center">
						<table cellSpacing="0" cellPadding="0" border="0" width="328" height="200">
							<tr>
								<td colspan="3" height="71"><IMG height="71" width="328" src="image/log01.jpg"></td>
							</tr>
							<tr>
								<td width="25">&nbsp;</td>
								<td height="15"><font style="FONT-SIZE: 8pt" face="Verdana,arial" color="navy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>User 
											ID</b></font></td>
								<td><asp:textbox id="TXT_USERNAME" style="COLOR: #46468e; BACKGROUND-COLOR: #fcffe1"
										runat="server"></asp:textbox></td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td height="15"><font style="FONT-SIZE: 8pt" face="Verdana,arial" color="navy">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Password</b></font></td>
								<td><asp:textbox id="TXT_PASSWORD" style="COLOR: #46468e; BACKGROUND-COLOR: #fcffe1" runat="server"
										TextMode="Password"></asp:textbox></td>
							</tr>
							<tr>
								<td colspan="2">&nbsp;</td>
								<td valign="middle">
									<asp:button id="BTN_SUBMIT" style="BORDER-RIGHT: whitesmoke 1px solid; BORDER-TOP: whitesmoke 1px solid; FONT-WEIGHT: 700; FONT-SIZE: 11px; BORDER-LEFT: whitesmoke 1px solid; COLOR: white; BORDER-BOTTOM: whitesmoke 1px solid; FONT-FAMILY: tahoma; BACKGROUND-COLOR: navy"
										runat="server" Width="88px" Text="L o g i n"></asp:button>
								</td>
							</tr>
							<TR>
								<TD colSpan="3"><asp:label id="Label1" runat="server" ForeColor="Red" Font-Bold="True"></asp:label></TD>
							</TR>
							<tr>
								<td colspan="3" height="26"><IMG height="26" width="328" src="image/log02.jpg"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="100"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
