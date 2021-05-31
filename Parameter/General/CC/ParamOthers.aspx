<%@ Page language="c#" Codebehind="ParamOthers.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.Others" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ParamOthers</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include  file="../../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table4" width="100%" border="0">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="Table10" style="WIDTH: 440px; HEIGHT: 25px">
								<TR>
									<TD class="TDBGColor2" align="center"><B><B>Parameter Credit Card Initial - Maker</B></B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><a id="back" runat="server"></a>
							<asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A><A href="../../../Logout.aspx"><IMG src="../../../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 25px" align="center" colSpan="2">
							<asp:placeholder id="Menu" runat="server" Visible="False"></asp:placeholder></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" style="HEIGHT: 25px" align="center" colSpan="2">
							Others</TD>
					</TR>
					<tr>
						<td vAlign="top" width="50%">
							<fieldset class="TDBGFieldset"><legend class="TDBGLegend">&nbsp;&nbsp;Phone 
									Verification&nbsp;&nbsp;</legend>
								<table id="table69" cellSpacing="0" cellPadding="0" width="98%">
									<TR>
										<TD class="tdBGColor1" width="170">Credit Card Outstand Limit</TD>
										<TD width="15"></TD>
										<TD class="tdBGColorValue">
											<asp:TextBox id="TXT_IN_VERPHNLIMIT" runat="server" Width="190px" onkeypress="return numbersonly()"
												style="TEXT-ALIGN: right" onblur="FormatCurrency(document.Form1.TXT_IN_VERPHNLIMIT)"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Percentage for Under Limit</TD>
										<TD></TD>
										<TD class="tdBGColorValue">
											<asp:TextBox id="TXT_IN_VERPHNLESS" runat="server" Width="190px" onkeypress="return numbersonly()"
												style="TEXT-ALIGN: right" onblur="FormatCurrency(document.Form1.TXT_IN_VERPHNLESS)"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Percentage for Higher Limit</TD>
										<TD></TD>
										<TD class="tdBGColorValue">
											<asp:TextBox id="TXT_IN_VERPHNMORE" runat="server" Width="190px" onkeypress="return numbersonly()"
												style="TEXT-ALIGN: right" onblur="FormatCurrency(document.Form1.TXT_IN_VERPHNMORE)"></asp:TextBox></TD>
									</TR>
								</table>
								<br>
							</fieldset>
							<br>
							<fieldset class="TDBGFieldset"><legend class="TDBGLegend">&nbsp;&nbsp;Physical 
									Verification&nbsp;</legend>
								<table id="table6" cellSpacing="0" cellPadding="0" width="98%">
									<TR>
										<TD class="tdBGColor1" width="170">Credit Card Outstand Limit</TD>
										<TD width="15"></TD>
										<TD class="tdBGColorValue">
											<asp:TextBox id="TXT_IN_VERSITLIMIT" runat="server" Width="190px" onkeypress="return numbersonly()"
												style="TEXT-ALIGN: right" onblur="FormatCurrency(document.Form1.TXT_IN_VERSITLIMIT)"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Percentage for Under Limit</TD>
										<TD></TD>
										<TD class="tdBGColorValue">
											<asp:TextBox id="TXT_IN_VERSITLESS" runat="server" Width="190px" onkeypress="return numbersonly()"
												style="TEXT-ALIGN: right" onblur="FormatCurrency(document.Form1.TXT_IN_VERSITLESS)"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Percentage for Higher Limit</TD>
										<TD></TD>
										<TD class="tdBGColorValue">
											<asp:TextBox id="TXT_IN_VERSITMORE" runat="server" Width="190px" onkeypress="return numbersonly()"
												style="TEXT-ALIGN: right" onblur="FormatCurrency(document.Form1.TXT_IN_VERSITMORE)"></asp:TextBox></TD>
									</TR>
								</table>
								<br>
							</fieldset>
						</td>
						<td vAlign="top" width="50%">
							<fieldset class="TDBGFieldset"><legend class="TDBGLegend">&nbsp;&nbsp;Job 
									Type&nbsp;&nbsp;</legend>
								<table id="table8" width="99%">
									<TR>
										<TD class="tdBGColor1" width="150">
											Employee</TD>
										<TD width="15"></TD>
										<TD class="tdBGColorValue">
											<asp:dropdownlist id="DDL_IN_JTEMPLOY" runat="server"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">
											Self-Employee</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_JTSELF" runat="server"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">
											Profesional</TD>
										<TD></TD>
										<TD class="tdBGColorValue">
											<asp:dropdownlist id="DDL_IN_JTPROF" runat="server"></asp:dropdownlist></TD>
									</TR>
								</table>
								<br>
							</fieldset>
						</td>
					</tr>
					<TR>
						<TD class="TDBGcOLOR2" vAlign="top" colSpan="2">
							<asp:Button id="BTN_SAVE" runat="server" CssClass="button1" Text="Save" onclick="BTN_SAVE_Click"></asp:Button></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
