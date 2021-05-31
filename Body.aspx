<%@ Page language="c#" Codebehind="Body.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Body" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<HEAD>
		<title>Body</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="MenuAccess/menuStyle.css" type="text/css" rel="stylesheet">
        <link href="Components/General/CSS/Reset.css" rel="stylesheet" type="text/css" />
        <script src="Components/General/JavaScript/jQuery-1.10.2.js" type="text/javascript"></script>
        <link href="Components/Menus/CSS/Menus.css" rel="stylesheet" type="text/css" />
        <script src="Components/Menus/JavaScript/Menus.js" type="text/javascript"></script>
        <script type="text/javascript">

            jQuery(document).ready(function () {
                jQuery('ul.sf-menu').superfish();
            });

        </script>
        <!--[if lt IE 8]>
            <style type="text/css">
                li { display: inline-table; }
            </style>   
        <![endif]-->
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="0" leftmargin="0" rightmargin="0" bottommargin="0">
		<form id="Form1" method="post" runat="server">
			<div id="MenuDIV" runat="server"></div>
			<table height="85%" width="100%">
				<tr>
					<td align="center" vAlign="top">
						<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="right" colSpan="3">
									<asp:Label id="Label1" runat="server" Visible="False"></asp:Label>
									<asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD align="center">
									<img src="image/losnet.gif"></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<P></P>
		</form>
	</body>
</HTML>
