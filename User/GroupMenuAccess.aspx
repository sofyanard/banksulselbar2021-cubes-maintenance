<%@ Page language="c#" Codebehind="GroupMenuAccess.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.User.GroupMenuAccess" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GroupMenuAccess</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD vAlign="top" align="center">
						<asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<asp:Table id="TBL_MENU" runat="server" Width="475px" CellPadding="0"></asp:Table></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="center">
						<asp:Label id="Label1" runat="server" Visible="False"></asp:Label>&nbsp;<asp:button id="BTN_SAVE" runat="server" Text="Save" CssClass="Button1" Width="70px" onclick="BTN_SAVE_Click"></asp:button>
						<asp:CheckBox id="CHK_ISNEW" runat="server" Visible="False"></asp:CheckBox>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
