<%@ Page language="c#" Codebehind="FTPSetupParamAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.FTPSetupParamAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>General Credit Card Parameter</title>
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
								<TD width="400" style="HEIGHT: 41px">
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Maintenance : 
													Credit Card General</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx">
										<IMG src="../../../Image/MainMenu.jpg"></A>&nbsp;<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A><A href="../../../Logout.aspx" target="_top"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						Parameter&nbsp;FTP Setup&nbsp;Approval</TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<asp:Panel id="PNL_REQUEST" runat="server">
							<TABLE id="Table8" style="WIDTH: 948px; HEIGHT: 152px" cellSpacing="2" cellPadding="0"
								width="948" border="0">
								<TR>
									<TD class="td">
										<TABLE id="Table9" cellSpacing="0" cellPadding="0" width="100%">
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px" width="202">&nbsp;FTP IP Address</TD>
												<TD style="WIDTH: 7px">:</TD>
												<TD class="TDBGColorValue">
													<asp:Label id="LBL_REQ_F_IP" runat="server"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px" width="202">FTP Username</TD>
												<TD style="WIDTH: 7px">:</TD>
												<TD class="TDBGColorValue">
													<asp:Label id="LBL_REQ_F_USER" runat="server"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px">FTP Password</TD>
												<TD style="WIDTH: 7px">:</TD>
												<TD class="TDBGColorValue">
													<asp:Label id="LBL_REQ_F_PASS" runat="server"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px">FTP Schedule</TD>
												<TD style="WIDTH: 7px">:</TD>
												<TD class="TDBGColorValue">
													<asp:Label id="LBL_REQ_F_SCDL" runat="server"></asp:Label>&nbsp;(hh:mm)</TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px">Maintenance</TD>
												<TD style="WIDTH: 7px">:</TD>
												<TD class="TDBGColorValue">
													<asp:Label id="LBL_REQ_F_MTNT" runat="server"></asp:Label>&nbsp;(hh:mm)</TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px">Hours</TD>
												<TD style="WIDTH: 7px">:</TD>
												<TD class="TDBGColorValue">
													<asp:Label id="LBL_REQ_F_HOURS" runat="server"></asp:Label></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 202px"></TD>
												<TD style="WIDTH: 7px"></TD>
												<TD></TD>
											</TR>
										</TABLE>
									</TD>
									<TD class="TD" style="HEIGHT: 167px" width="50%">
										<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px" width="202">FTP Business Intelligence 
													(FBI)</TD>
												<TD style="WIDTH: 7px">:</TD>
												<TD class="TDBGColorValue">
													<asp:Label id="LBL_REQ_F_BI" runat="server"></asp:Label>&nbsp;(hh:mm)</TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px" width="202">FBI IP Address</TD>
												<TD style="WIDTH: 7px">:</TD>
												<TD class="TDBGColorValue">
													<asp:Label id="LBL_REQ_FBI_IP" runat="server"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px">FBI Username</TD>
												<TD style="WIDTH: 7px">:</TD>
												<TD class="TDBGColorValue">
													<asp:Label id="LBL_REQ_FBI_USER" runat="server"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px; HEIGHT: 22px">FBI Password</TD>
												<TD style="WIDTH: 7px; HEIGHT: 22px">:</TD>
												<TD class="TDBGColorValue" style="HEIGHT: 22px">
													<asp:Label id="LBL_REQ_FBI_PASS" runat="server"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px">FBI Directory</TD>
												<TD style="WIDTH: 7px">:</TD>
												<TD class="TDBGColorValue">
													<asp:Label id="LBL_REQ_FBI_DIR" runat="server"></asp:Label></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px">CIF Schedule</TD>
												<TD style="WIDTH: 7px">:</TD>
												<TD class="TDBGColorValue">
													<asp:Label id="LBL_REQ_CIF_SHEC" runat="server"></asp:Label>&nbsp;(hh:mm)</TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px">Server Schedule</TD>
												<TD style="WIDTH: 7px">:</TD>
												<TD class="TDBGColorValue">
													<asp:Label id="LBL_REQ_SERV_SHEC" runat="server"></asp:Label>&nbsp;(hh:mm)</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</asp:Panel></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2">
						<asp:button id="BTN_SUBMIT" CssClass="button1" Text="Submit" Runat="server" onclick="BTN_SUBMIT_Click"></asp:button></TD>
				</TR>
			</TABLE>
			</TD></TR>
			<TR>
				<TD vAlign="top" align="left" width="50%" colSpan="2">
			</TR>
			</TABLE></TR></TABLE>
		</form>
	</body>
</HTML>
