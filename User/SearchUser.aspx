<%@ Page language="c#" Codebehind="SearchUser.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.User.SearchUser" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SearchUser</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../include/cek_mandatoryOnly.html" -->
		<script language="javascript">
			function pilih(ctrlID,ctrlDesc)
			{
				if (document.getElementById('LST_RESULT').value != '')
				{
				    eval('opener.document.getElementById("' + ctrlID + '").value = document.getElementById("LST_RESULT").value');
				    eval('opener.document.getElementById("' + ctrlDesc + '").value = document.getElementById("LST_RESULT").options[document.getElementById("LST_RESULT").selectedIndex].text');
				}
				else
				{
					eval('opener.document.getElementById("' + ctrlID + '").value = ""');
					eval('opener.document.getElementById("' + ctrlDesc + '").value = ""');
				}
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
									<TD class="TDBGColor1" style="WIDTH: 129px">Module</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_RFMODULE" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Area</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_RFAREA" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Branch</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_RFBRANCH" runat="server" Width="300px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Group</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_RFGROUP" runat="server" Width="300px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Name</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:TextBox id="TXT_SEARCH_USERNAME" runat="server"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">UserID</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:TextBox id="TXT_SEARCH_USERID" runat="server"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Officer Code</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:TextBox id="TXT_SEARCH_OFFICERCODE" runat="server"></asp:TextBox>
										<asp:Button id="BTN_SEARCH" runat="server" Width="100px" Text="Search" onclick="BTN_SEARCH_Click"></asp:Button></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 129px"></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Search Result</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:ListBox id="LST_RESULT" runat="server" Width="300px"></asp:ListBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 129px"></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD class="TDBGColor2" colSpan="3"><INPUT type="button" value="OK" id="ok" name="ok" runat="server" class="Button1" style="WIDTH: 100px"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
