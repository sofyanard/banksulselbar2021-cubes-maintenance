<%@ Page language="c#" Codebehind="ParamCalculationAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.ParamCalculationAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ParamCalculationAppr</title>
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
									<TD class="TDBGColor2" align="center"><B>Parameter Credit Card Initial - Approval</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><a id="back" runat="server"></a>
							<asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A><A href="../../../Logout.aspx"><IMG src="../../../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 25px" align="center" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" style="HEIGHT: 25px" align="center" colSpan="2">Credit 
							Calculation Items</TD>
					</TR>
					<tr>
						<td vAlign="top" colspan="2">
							<fieldset class="TDBGFieldset"><legend class="TDBGLegend">&nbsp; Operation&nbsp;&nbsp;</legend>
								<table id="table61" cellSpacing="0" cellPadding="0" width="98%">
									<TR>
										<TD class="tdBGColor1" width="200">Citizenship</TD>
										<TD width="15"></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWOPRCODE1" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Age + Tenor</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWOPRCODE2" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1" style="HEIGHT: 20px">MOB - Credit Card</TD>
										<TD style="HEIGHT: 20px"></TD>
										<TD class="tdBGColorValue" style="HEIGHT: 20px"><asp:dropdownlist id="DDL_IN_CWOPRCODE3" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1" style="HEIGHT: 17px">Blacklist Checking</TD>
										<TD style="HEIGHT: 17px"></TD>
										<TD class="tdBGColorValue" style="HEIGHT: 17px"><asp:dropdownlist id="DDL_IN_CWOPRCODE4" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Reject List</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWOPRCODE5" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Employment</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWOPRCODE6" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
								</table>
								<br>
							</fieldset>
							<br>
							<fieldset class="TDBGFieldset"><legend class="TDBGLegend">&nbsp;&nbsp;Consumer&nbsp;Credit&nbsp;&nbsp;</legend>
								<table id="table6" cellSpacing="0" cellPadding="0" width="98%">
									<TR>
										<TD class="tdBGColor1" width="200">Annual Gross Income</TD>
										<TD width="15"></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCCRCODE2" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Annual Net Income</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCCRCODE3" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Phone Verification</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCCRCODE4" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Employment Verification</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCCRCODE5" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
								</table>
								<br>
							</fieldset>
							<br>
							<fieldset class="TDBGFieldset"><legend class="TDBGLegend">&nbsp;&nbsp;Calculation&nbsp;&nbsp;</legend>
								<table id="table8" width="99%">
									<TR>
										<TD class="tdBGColor1" width="200" style="HEIGHT: 11px">Monthly Gross Income</TD>
										<TD width="15" style="HEIGHT: 11px"></TD>
										<TD class="tdBGColorValue" style="HEIGHT: 11px"><asp:dropdownlist id="DDL_IN_CWCALCODE1" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Monthly Net Income</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCALCODE11" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Credit Card Outsand</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCALCODE2" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Credit Card Limit</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCALCODE3" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Utilization rate (%)</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCALCODE4" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">n% of credit limit</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCALCODE5" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">nX Monthly Gross Income</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCALCODE6" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">nX Monthly&nbsp;Net Income</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCALCODE12" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Loan Amount</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCALCODE7" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1" style="HEIGHT: 21px">Tenor (months)</TD>
										<TD style="HEIGHT: 21px"></TD>
										<TD class="tdBGColorValue" style="HEIGHT: 21px"><asp:dropdownlist id="DDL_IN_CWCALCODE8" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Monthly Installment</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCALCODE9" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">DBR Ratio (gross)</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCALCODE10" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">DBR Ratio (net)</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:dropdownlist id="DDL_IN_CWCALCODE13" runat="server" Enabled="False"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">FixnFast Limit</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_LIMIT" style="TEXT-ALIGN: right" runat="server"
												Width="190px" onblur="FormatCurrency(document.Form1.TXT_IN_LIMIT)" ReadOnly="True"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Target Incoming Regular</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_TINCREG" style="TEXT-ALIGN: right"
												runat="server" Width="190px" ReadOnly="True"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Target Account Regular</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_TACCREG" style="TEXT-ALIGN: right"
												runat="server" Width="190px" ReadOnly="True"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Service Indicator 10 days</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_SERVIND10" style="TEXT-ALIGN: right"
												runat="server" Width="190px" ReadOnly="True"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Service Indicator 12 days</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_SERVIND12" style="TEXT-ALIGN: right"
												runat="server" Width="190px" ReadOnly="True"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Max Application Open</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_MAXOPEN" style="TEXT-ALIGN: right"
												runat="server" Width="190px" ReadOnly="True"></asp:textbox></TD>
									</TR>
								</table>
								<br>
							</fieldset>
						</td>
					</tr>
					<TR>
						<TD class="TDBGcOLOR2" vAlign="top" colSpan="2">
							<asp:Button id="BTN_SAVE" runat="server" CssClass="button1" Text="Submit" onclick="BTN_SAVE_Click"></asp:Button></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
