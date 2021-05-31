<%@ Page language="c#" Codebehind="FTPSetupParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.FTPSetupParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>General Credit Card Parameter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TBODY>
					<TR>
						<TD colSpan="2">
							<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 411px; HEIGHT: 41px" vAlign="middle" align="left">
										<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
											border="0">
											<TR>
												<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
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
							<P>Existing&nbsp;Data</P>
						</TD>
					<TR>
						<TD class="td" style="HEIGHT: 152px" vAlign="top" width="50%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px" width="202">&nbsp;FTP IP Address</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_F_IP" runat="server"></asp:label>
										<asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px" width="202">FTP Username</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_F_USER" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px">FTP Password</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_F_PASS" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px">FTP Schedule</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_F_SCDL" runat="server"></asp:label>&nbsp;(hh:mm)</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px">Maintenance</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_F_MTNT" runat="server"></asp:label>&nbsp; 
										(hh:mm)</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px">Hours</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_F_HOURS" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 202px"></TD>
									<TD style="WIDTH: 7px"></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="TD" style="HEIGHT: 152px" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="470" style="WIDTH: 470px; HEIGHT: 136px">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px" width="202">FTP Business Intelligence 
										(FBI)</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_F_BI" runat="server"></asp:label>&nbsp;(hh:mm)</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px" width="202">Business 
										Intelligence&nbsp;&nbsp;IP Address</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_FBI_IP" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px">Business 
										Intelligence&nbsp;&nbsp;Username</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_FBI_USER" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px">Business 
										Intelligence&nbsp;&nbsp;Password</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_FBI_PASS" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px">Business 
										Intelligence&nbsp;&nbsp;Directory</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_FBI_DIR" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<td class="TDBGColor1" style="WIDTH: 202px">CIF Schedule</td>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_CIF_SHEC" runat="server"></asp:label>&nbsp;(hh:mm)</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px">Server Schedule</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_SERV_SHEC" runat="server"></asp:label>&nbsp;(hh:mm)</TD>
								</TR>
							</TABLE>
					<TR>
						<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_EDIT" Width="70px" CssClass="button1" Text="Edit" Runat="server" onclick="BTN_EDIT_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD colSpan="2"><asp:panel id="PNP_REQUEST" runat="server">
								<TABLE id="Table4" cellSpacing="2" cellPadding="0" width="100%" border="0">
									<TR>
										<TD class="TDHeader1">Maker Request</TD>
									</TR>
								</TABLE>
								<TABLE id="Table8" cellSpacing="2" cellPadding="0" width="100%" border="0">
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
													<TD class="TDBGColor1" style="WIDTH: 202px" width="202">Business 
														Intelligence&nbsp;&nbsp;IP Address</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:Label id="LBL_REQ_FBI_IP" runat="server"></asp:Label></TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px">Business 
														Intelligence&nbsp;&nbsp;Username</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:Label id="LBL_REQ_FBI_USER" runat="server"></asp:Label></TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px; HEIGHT: 22px">Business 
														Intelligence&nbsp;&nbsp;Password</TD>
													<TD style="WIDTH: 7px; HEIGHT: 22px">:</TD>
													<TD class="TDBGColorValue" style="HEIGHT: 22px">
														<asp:Label id="LBL_REQ_FBI_PASS" runat="server"></asp:Label></TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px">Business 
														Intelligence&nbsp;&nbsp;Directory</TD>
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
								<TABLE id="Table2" cellSpacing="2" cellPadding="0" width="100%" border="0">
									<TR>
										<TD class="TDBGColor2">&nbsp;</TD>
									</TR>
								</TABLE>
							</asp:panel></TD>
					</TR>
					<TR>
						<TD colSpan="2"><asp:panel id="PNL_SETUP" runat="server" DESIGNTIMEDRAGDROP="5039">
								<TABLE id="Table10" cellSpacing="2" cellPadding="0" width="100%" border="0">
									<TR>
										<TD class="TDHeader1">Parameter&nbsp;FTP Setup&nbsp;Maker</TD>
									</TR>
								</TABLE>
								<TABLE id="Table11" cellSpacing="2" cellPadding="0" width="100%" border="0">
									<TR>
										<TD class="td">
											<TABLE id="Table12" cellSpacing="0" cellPadding="0" width="100%">
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px" width="202">&nbsp;FTP IP Address</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return kutip_satu()" id="TXT_F_IP" runat="server" CssClass="mandatory"
															Width="152px" MaxLength="15"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px" width="202">FTP Username</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return kutip_satu()" id="TXT_F_USER" runat="server" Width="256px" MaxLength="20"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px">FTP Password</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return kutip_satu()" id="TXT_F_PASS" runat="server" Width="256px" MaxLength="20"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px">FTP Schedule</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return kutip_satu()" id="TXT_F_SCDL_YY" runat="server" Width="96px"
															MaxLength="5"></asp:TextBox>&nbsp;(hh:mm)</TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px">Maintenance</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return kutip_satu()" id="TXT_F_MTNT_YY" runat="server" Width="96px"
															MaxLength="5"></asp:TextBox>&nbsp;(hh:mm)</TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px">Hours</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return numbersonly()" id="TXT_F_HOURS" runat="server" Width="54px" MaxLength="4"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 202px"></TD>
													<TD style="WIDTH: 7px"></TD>
													<TD></TD>
												</TR>
											</TABLE>
										</TD>
										<TD class="TD" width="50%">
											<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px" width="202">FTP Business Intelligence 
														(FBI)</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return kutip_satu()" id="TXT_F_BI_YY" runat="server" Width="88px" MaxLength="5"></asp:TextBox>&nbsp;(hh:mm)</TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px" width="202">Business Intelligence 
														Address</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return kutip_satu()" id="TXT_FBI_IP" runat="server" Width="158px" MaxLength="15"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px">Business 
														Intelligence&nbsp;&nbsp;Username</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return kutip_satu()" id="TXT_FBI_USER" runat="server" Width="256px"
															MaxLength="20"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px">Business 
														Intelligence&nbsp;&nbsp;Password</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return kutip_satu()" id="TXT_FBI_PASS" runat="server" Width="256px"
															MaxLength="20"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px">Business 
														Intelligence&nbsp;&nbsp;Directory</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return kutip_satu()" id="TXT_FBI_DIR" runat="server" Width="256px" MaxLength="20"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px">CIF Schedule</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return kutip_satu()" id="TXT_CIF_SHEC_YY" runat="server" Width="88px"
															MaxLength="5"></asp:TextBox>&nbsp;(hh:mm)</TD>
												</TR>
												<TR>
													<TD class="TDBGColor1" style="WIDTH: 202px">Server Schedule</TD>
													<TD style="WIDTH: 7px">:</TD>
													<TD class="TDBGColorValue">
														<asp:TextBox onkeypress="return kutip_satu()" id="TXT_SERV_SHEC_YY" runat="server" Width="88px"
															MaxLength="5"></asp:TextBox>&nbsp;(hh:mm)</TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
								</TABLE>
								<TABLE id="Table14" cellSpacing="2" cellPadding="0" width="100%" border="0">
									<TR>
										<TD class="TDBGColor2">
											<asp:button id="BTN_SAVE" Runat="server" Text="Save" CssClass="button1" Width="59px" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
											<asp:button id="BTN_CANCEL" Runat="server" Text="Cancel" CssClass="button1" onclick="BTN_CANCEL_Click"></asp:button></TD>
									</TR>
								</TABLE>
							</asp:panel></TD>
					</TR>
				</TBODY>
			</TABLE>
			</TR></TBODY></TABLE></form>
	</body>
</HTML>
