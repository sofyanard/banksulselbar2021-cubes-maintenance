<%@ Page language="c#" Codebehind="ParamFTPServerAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.ParamFTPServerAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ParamFTPServerAppr</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Setup 
						Office&nbsp;Automation&nbsp;FTP Server Approval</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="left" width="50%" colSpan="2">
						<asp:datagrid id="DGR_APPR" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="100%"
							ShowFooter="True">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="IN_IPSERVER" HeaderText="Server Name (IP)">
									<HeaderStyle HorizontalAlign="Center" Width="200px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="IN_PORTFTP" HeaderText="Port">
									<HeaderStyle HorizontalAlign="Center" Width="200px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="IN_IDFTP" HeaderText="User ID">
									<HeaderStyle HorizontalAlign="Center" Width="200px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="IN_PASSFTP" HeaderText="Password">
									<HeaderStyle HorizontalAlign="Center" Width="280px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="REJECT_NEXT1" HeaderText="Reject Next 1">
									<HeaderStyle HorizontalAlign="Center" Width="160px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="REJECT_TRACK1" HeaderText="Reject Track 1">
									<HeaderStyle HorizontalAlign="Center" Width="160px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="REJECT_NEXT2" HeaderText="Reject Next 2">
									<HeaderStyle HorizontalAlign="Center" Width="160px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="REJECT_TRACK2" HeaderText="Reject Track 2">
									<HeaderStyle HorizontalAlign="Center" Width="160px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CAP_APPROVEBY" HeaderText="Cap Approve by">
									<HeaderStyle HorizontalAlign="Center" Width="160px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CAP_TRACK" HeaderText="Cap Track">
									<HeaderStyle HorizontalAlign="Center" Width="160px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CAP" HeaderText="Cap">
									<HeaderStyle HorizontalAlign="Center" Width="160px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="IN_SEQ" HeaderText="IN_SEQ"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Approve">
									<HeaderStyle HorizontalAlign="Center" Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rd_approve" runat="server" GroupName="approval_status"></asp:RadioButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:LinkButton id="lnkAllAppr" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Reject">
									<HeaderStyle HorizontalAlign="Center" Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rd_Reject" runat="server" GroupName="approval_status"></asp:RadioButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:LinkButton id="lnkAllReject" runat="server" CommandName="allReject">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pending">
									<HeaderStyle HorizontalAlign="Center" Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rd_Pending" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:LinkButton id="lnkAllPend" runat="server" CommandName="allPend">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
				<!--
				<TR>
					<TD class="td" vAlign="top" align="left" width="50%" colSpan="2">
						<TABLE id="Table3" borderColor="silver" cellSpacing="1" cellPadding="0" width="100%" border="1">
							<TR>
								<TD class="tdSmallHeader">Server Name (IP)</TD>
								<TD class="tdSmallHeader">Port</TD>
								<TD class="tdSmallHeader">User ID</TD>
								<TD class="tdSmallHeader">Password</TD>
								<TD class="tdSmallHeader">Reject Next 1</TD>
								<TD class="tdSmallHeader">Reject Track 1</TD>
								<TD class="tdSmallHeader">Reject Next 2</TD>
								<TD class="tdSmallHeader">Reject Track 2</TD>
								<TD class="tdSmallHeader">Cap</TD>
								<TD class="tdSmallHeader">Cap Approve by</TD>
								<TD class="tdSmallHeader">Cap Track</TD>
								<TD class="tdSmallHeader" width="50">Approve</TD>
								<TD class="tdSmallHeader" width="50">Reject</TD>
								<TD class="tdSmallHeader" width="50">Pending</TD>
							</TR>
							<TR>
								<TD><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_IPSERVER2" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
										style="TEXT-ALIGN: right" runat="server" BorderStyle="None" BorderColor="Silver" ReadOnly="True"
										Width="100%"></asp:textbox></TD>
								<TD><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_PORTFTP2" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
										style="TEXT-ALIGN: right" runat="server" BorderStyle="None" BorderColor="Silver" ReadOnly="True"
										Width="100%"></asp:textbox></TD>
								<TD><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_IDFTP2" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
										style="TEXT-ALIGN: right" runat="server" BorderStyle="None" BorderColor="Silver" ReadOnly="True"
										Width="100%"></asp:textbox></TD>
								<TD><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_PASSFTP2" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
										style="TEXT-ALIGN: right" runat="server" BorderStyle="None" BorderColor="Silver" ReadOnly="True"
										Width="100%"></asp:textbox></TD>
								<TD style="WIDTH: 355px">
									<asp:TextBox id="ddl_reject_next1" style="TEXT-ALIGN: right" runat="server" Width="100%" ReadOnly="True"
										BorderColor="Silver" BorderStyle="None"></asp:TextBox></TD>
								<TD style="WIDTH: 355px">
									<asp:TextBox id="ddl_reject_track1" style="TEXT-ALIGN: right" runat="server" Width="100%" ReadOnly="True"
										BorderColor="Silver" BorderStyle="None"></asp:TextBox></TD>
								<TD style="WIDTH: 355px">
									<asp:TextBox id="ddl_reject_next2" style="TEXT-ALIGN: right" runat="server" Width="100%" ReadOnly="True"
										BorderColor="Silver" BorderStyle="None"></asp:TextBox></TD>
								<TD style="WIDTH: 355px">
									<asp:TextBox id="ddl_reject_track2" style="TEXT-ALIGN: right" runat="server" Width="100%" ReadOnly="True"
										BorderColor="Silver" BorderStyle="None"></asp:TextBox></TD>
								<TD style="WIDTH: 355px">
									<asp:TextBox id="ddl_cap_approveby" style="TEXT-ALIGN: right" runat="server" Width="100%" ReadOnly="True"
										BorderColor="Silver" BorderStyle="None"></asp:TextBox></TD>
								<TD style="WIDTH: 355px">
									<asp:TextBox id="ddl_cap_track" style="TEXT-ALIGN: right" runat="server" Width="100%" ReadOnly="True"
										BorderColor="Silver" BorderStyle="None"></asp:TextBox></TD>
								<TD style="WIDTH: 355px">
									<asp:TextBox onkeypress="return numbersonly()" id="tx_cap" onblur="FormatCurrency(document.Form1.tx_capa)"
										style="TEXT-ALIGN: right" runat="server" Width="100%" ReadOnly="True" BorderColor="Silver"
										BorderStyle="None"></asp:TextBox></TD>
								<TD align="center" width="50"><asp:radiobutton id="rdo_Approve" runat="server" GroupName="approval_status"></asp:radiobutton>&nbsp;</TD>
								<TD align="center" width="50"><asp:radiobutton id="rdo_Reject" runat="server" GroupName="approval_status"></asp:radiobutton>&nbsp;</TD>
								<TD align="center" width="50"><asp:radiobutton id="rdo_Pending" runat="server" GroupName="approval_status" Checked="True"></asp:radiobutton>&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				-->
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SUBMIT" CssClass="button1" Text="Submit" Runat="server" onclick="BTN_SUBMIT_Click"></asp:button>&nbsp;&nbsp;
						<asp:Label id="LBL_IN_PASSFTP2" runat="server" Visible="False"></asp:Label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
