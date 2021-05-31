<%@ Page language="c#" Codebehind="SearchSalesAgent.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.User.SearchSalesAgent" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SearchSalesAgent</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../include/cek_mandatoryOnly.html" -->
		<script language="javascript">
			function pilih(ctrlID,ctrlDesc)
			{
				eval('opener.document.Form1.' + ctrlID + '.value = document.Form1.LST_RESULT.value');
				eval('opener.document.Form1.' + ctrlDesc + '.value = document.Form1.LST_RESULT.options[document.Form1.LST_RESULT.selectedIndex].text');
				window.close();
			}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="450">
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Agency</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:Label id="LBL_AGENTNAME" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Sales Agent</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:textbox id="TXT_AGENTNAME" runat="server" Width="250px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 129px"></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD>
										<asp:Button id="BTN_SEARCH" runat="server" Width="100px" Text="Search" onclick="BTN_SEARCH_Click"></asp:Button></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Search Result</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD>
										<asp:ListBox id="LST_RESULT" runat="server" Width="300px" Height="75px"></asp:ListBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 129px"></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="TDBGColor2" colSpan="3"><INPUT type="button" value="OK" id="ok" name="ok" runat="server" class="Button1" style="WIDTH: 100px"
											size="20"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
