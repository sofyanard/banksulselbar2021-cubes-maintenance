<%@ Page language="c#" Codebehind="CreditAnalystArea.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.User.CreditAnalystArea" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CreditAnalystArea</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../include/cek_entries.html" -->
		<script language="javascript">
			function pilih(ctrlDesc)
			{
				eval('opener.document.Form1.' + ctrlDesc + '.value = document.Form1.TXT_AREA_REQUEsT.value');
				window.close();
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR>
						<TD class="tdHeader1" colSpan="2">User Area Coverage</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 17px">User ID</TD>
									<TD style="WIDTH: 15px; HEIGHT: 17px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:textbox id="TXT_USERID" runat="server" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 17px">Area</TD>
									<TD style="WIDTH: 15px; HEIGHT: 17px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px">
										<asp:ListBox id="LST_AREA" runat="server" Width="226px" Height="100px" SelectionMode="Multiple"></asp:ListBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">
										Current Area</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:textbox id="TXT_CC_AREA" runat="server" Width="300px" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Areas Requested</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:textbox id="TXT_AREA_REQUEsT" runat="server" Width="300px" ReadOnly="True"></asp:textbox>
										<asp:button id="BTN_SUBMIT" runat="server" Width="100px" Text="Submit" onclick="BTN_SUBMIT_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" align="center" colSpan="2">&nbsp;&nbsp;&nbsp; <INPUT class="Button1" id="ok" style="WIDTH: 100px" type="button" size="20" value="OK"
								name="ok" runat="server">&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" runat="server" Width="100px" Text="Cancel" CssClass="Button1" onclick="BTN_CANCEL_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
