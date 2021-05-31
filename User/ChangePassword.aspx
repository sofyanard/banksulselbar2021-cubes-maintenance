<%@ Page language="c#" Codebehind="ChangePassword.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.User.ChangePassword" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ChangePassword</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table7" border="0" width="100%" cellSpacing="2" cellPadding="2">
					<TR>
						<TD class="tdNoBorder" width="421"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
							<TABLE id="Table6">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Change Password</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A><A href="../Body.aspx"><IMG src="../Image/MainMenu.jpg"></A><A href="../Logout.aspx" target="_top"><IMG src="../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD colSpan="2">
							<P>
								<TABLE id="Table1" style="HEIGHT: 125px" height="125" cellSpacing="2" cellPadding="2" width="400"
									align="center" border="0" class="td">
									<TR>
										<TD class="tdHeader1">Change Password</TD>
									</TR>
									<TR>
										<TD align="center">
											<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0" width="375">
												<TR>
													<TD class="TDBGColor1" width="150">Old Password</TD>
													<TD width="17"></TD>
													<TD class="TDBGColorValue">
														<asp:textbox onkeypress="return kutip_satu()" id="txt_OldPwd" runat="server" TextMode="Password"></asp:textbox></TD>
												</TR>
												<TR>
													<TD class="TDBGColor1">New Password</TD>
													<TD style="WIDTH: 7px; HEIGHT: 3px"></TD>
													<TD class="TDBGColorValue">
														<asp:textbox onkeypress="return kutip_satu()" id="txt_NewPwd" runat="server" TextMode="Password"></asp:textbox></TD>
												</TR>
												<TR>
													<TD class="TDBGColor1">Confirm Password</TD>
													<TD style="WIDTH: 7px; HEIGHT: 1px"></TD>
													<TD class="TDBGColorValue">
														<asp:textbox onkeypress="return kutip_satu()" id="txt_ConfirmPwd" runat="server" TextMode="Password"></asp:textbox></TD>
												</TR>
											</TABLE>
											<asp:Label id="Message" runat="server" ForeColor="Red" Width="150px"></asp:Label>
										</TD>
									</TR>
									<TR>
										<TD align="center">
											<asp:button id="btn_Change" runat="server" Text="Change" Width="75px" CssClass="button1" onclick="btn_Change_Click"></asp:button></TD>
									</TR>
								</TABLE>
							</P>
						</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
