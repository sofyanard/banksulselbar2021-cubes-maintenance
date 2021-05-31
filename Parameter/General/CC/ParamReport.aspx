<%@ Page language="c#" Codebehind="ParamReport.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.ParamReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ParamReport</title>
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
							Parameter Report</TD>
					</TR>
					<TR>
						<TD class="TD" vAlign="top" colSpan="2">
							<TABLE id="table6" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="tdBGColor1" width="250">Fee Per Application</TD>
									<TD width="15"></TD>
									<TD class="tdBGColorValue">
										<asp:TextBox onkeypress="return numbersonly()" id="TXT_IN_FEERECEIVED" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
											style="TEXT-ALIGN: right" runat="server" Width="190px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1">Point Reward per Application</TD>
									<TD></TD>
									<TD class="tdBGColorValue">
										<asp:TextBox onkeypress="return numbersonly()" id="TXT_IN_POINTREWARD" onblur="FormatCurrency(document.Form1.TXT_IN_POINTREWARD)"
											style="TEXT-ALIGN: right" runat="server" Width="190px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1">Tax (PPH)</TD>
									<TD></TD>
									<TD class="tdBGColorValue">
										<asp:TextBox onkeypress="return digitsonly()" id="TXT_IN_TAX" onblur="FormatCurrency(document.Form1.TXT_IN_TAX)"
											style="TEXT-ALIGN: right" runat="server" Width="190px"></asp:TextBox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGcOLOR2" vAlign="top" colSpan="2">
							<asp:Button id="BTN_SAVE" runat="server" CssClass="button1" Text="Save" Enabled="False" onclick="BTN_SAVE_Click"></asp:Button></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" colSpan="2">Existing Data</TD>
					</TR>
					<TR>
						<TD class="TD" vAlign="top" colSpan="2">
							<TABLE id="Table2" cellSpacing="1" cellPadding="0" width="100%" border="1" borderColor="silver">
								<TR>
									<TD class="tdSmallHeader">Fee Per Application</TD>
									<TD class="tdSmallHeader">Point Reward per Application</TD>
									<TD class="tdSmallHeader">Tax (PPH)</TD>
									<TD class="tdSmallHeader" width="50">Function</TD>
								</TR>
								<TR>
									<TD>
										<asp:TextBox onkeypress="return numbersonly()" id="TXT_IN_FEERECEIVED2" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver"
											ReadOnly="True"></asp:TextBox></TD>
									<TD>
										<asp:TextBox onkeypress="return numbersonly()" id="TXT_IN_POINTREWARD2" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver"
											ReadOnly="True"></asp:TextBox></TD>
									<TD>
										<asp:TextBox onkeypress="return numbersonly()" id="TXT_IN_TAX2" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver"
											ReadOnly="True"></asp:TextBox></TD>
									<TD borderColor="silver" align="center">
										<asp:LinkButton id="LinkEdit" runat="server" onclick="LinkEdit_Click">Edit</asp:LinkButton></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" style="HEIGHT: 20px" vAlign="top" colSpan="2">Request Data</TD>
					</TR>
					<TR>
						<TD class="TD" vAlign="top" colSpan="2">
							<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="100%" border="1" borderColor="silver">
								<TR>
									<TD class="tdSmallHeader">Fee Per Application</TD>
									<TD class="tdSmallHeader">Point Reward per Application</TD>
									<TD class="tdSmallHeader">Tax (PPH)</TD>
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
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGcOLOR2" vAlign="top" colSpan="2">&nbsp;</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
