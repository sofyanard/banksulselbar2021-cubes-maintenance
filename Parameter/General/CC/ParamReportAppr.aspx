<%@ Page language="c#" Codebehind="ParamReportAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.ParamReportAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ParamReportAppr</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
						<TABLE id="Table2">
						</TABLE>
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="400">
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B><B>Parameter Credit Card 
														Initial - Approval</B></B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>&nbsp;<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A><A href="../../../Logout.aspx" target="_top"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						Parameter&nbsp;Report Approval</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="left" width="50%" colSpan="2" class="TD">
						<TABLE id="Table3" borderColor="silver" cellSpacing="1" cellPadding="0" width="100%" border="1">
							<TR>
								<TD class="tdSmallHeader">Fee Per Application</TD>
								<TD class="tdSmallHeader">Point Reward per Application</TD>
								<TD class="tdSmallHeader">Tax (PPH)</TD>
								<TD class="tdSmallHeader">Approve</TD>
								<TD class="tdSmallHeader">Reject</TD>
								<TD class="tdSmallHeader">Pending</TD>
							</TR>
							<TR>
								<TD>
									<asp:TextBox onkeypress="return numbersonly()" id="TXT_IN_FEERECEIVED1" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
										style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver"
										ReadOnly="True"></asp:TextBox></TD>
								<TD>
									<asp:TextBox onkeypress="return numbersonly()" id="TXT_IN_POINTREWARD1" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
										style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver"
										ReadOnly="True"></asp:TextBox></TD>
								<TD>
									<asp:TextBox onkeypress="return numbersonly()" id="TXT_IN_TAX1" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
										style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver"
										ReadOnly="True"></asp:TextBox></TD>
								<TD width="50" borderColor="silver" align="center">
									<asp:RadioButton id="rdo_Approve" runat="server" GroupName="approval_status"></asp:RadioButton>&nbsp;</TD>
								<TD width="50" borderColor="silver" align="center">
									<asp:RadioButton id="rdo_Reject" runat="server" GroupName="approval_status"></asp:RadioButton>&nbsp;</TD>
								<TD width="50" borderColor="silver" align="center">
									<asp:RadioButton id="rdo_Pending" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SUBMIT" Runat="server" Text="Submit" CssClass="button1" onclick="BTN_SUBMIT_Click"></asp:button>&nbsp;&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
