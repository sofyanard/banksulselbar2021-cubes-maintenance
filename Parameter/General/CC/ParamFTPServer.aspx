<%@ Page language="c#" Codebehind="ParamFTPServer.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.ParamFTPServer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ParamFTPServer</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
						<TD class="tdNoBorder" align="right"><a id="back" runat="server"></a><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A><A href="../../../Logout.aspx"><IMG src="../../../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 25px" align="center" colSpan="2"><asp:placeholder id="Menu" runat="server" Visible="False"></asp:placeholder></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" style="HEIGHT: 25px" align="center" colSpan="2">Setup 
							Office&nbsp;Automation&nbsp;FTP Server</TD>
					</TR>
					<TR>
						<TD class="TD" vAlign="top" colSpan="2">
							<TABLE id="table6" cellSpacing="0" cellPadding="0" width="98%">
								<TR>
									<TD class="tdBGColor1" width="250">Server Name (IP)</TD>
									<TD width="15"></TD>
									<TD class="tdBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_IN_IPSERVER" runat="server" MaxLength="50"
											Width="190px"></asp:textbox>
										<asp:label id="LBL_IN_SEQ" runat="server" Visible="False"></asp:label></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1">FTP Port</TD>
									<TD></TD>
									<TD class="tdBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_PORTFTP" runat="server" Width="190px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1">FTP User ID</TD>
									<TD></TD>
									<TD class="tdBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_IN_IDFTP" runat="server" MaxLength="50"
											Width="190px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1">FTP Password</TD>
									<TD></TD>
									<TD class="tdBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_IN_PASSFTP" runat="server" MaxLength="50"
											Width="190px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1"></TD>
									<TD></TD>
									<TD class="tdBGColorValue"></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1">Reject Next 1</TD>
									<TD></TD>
									<TD class="tdBGColorValue"><asp:dropdownlist id="ddl_reject_next1" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1">Reject Track 1</TD>
									<TD></TD>
									<TD class="tdBGColorValue"><asp:dropdownlist id="ddl_reject_track1" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1">Reject Next 2</TD>
									<TD></TD>
									<TD class="tdBGColorValue"><asp:dropdownlist id="ddl_reject_next2" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1">Reject Track 2</TD>
									<TD></TD>
									<TD class="tdBGColorValue"><asp:dropdownlist id="ddl_reject_track2" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1"></TD>
									<TD></TD>
									<TD class="tdBGColorValue"></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1">Cap</TD>
									<TD></TD>
									<TD class="tdBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="tx_cap" runat="server" MaxLength="8" Width="72px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1">Cap Approveby</TD>
									<TD></TD>
									<TD class="tdBGColorValue"><asp:dropdownlist id="ddl_cap_approveby" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="tdBGColor1">Cap Track</TD>
									<TD></TD>
									<TD class="tdBGColorValue"><asp:dropdownlist id="ddl_cap_track" runat="server"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGcOLOR2" vAlign="top" colSpan="2"><asp:button id="BTN_SAVE" runat="server" CssClass="button1" Text="Save" Enabled="False" onclick="BTN_SAVE_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" colSpan="2">Existing Data</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" PageSize="5" AutoGenerateColumns="False"
								AllowPaging="True">
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
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle HorizontalAlign="Center" Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="IN_SEQ" HeaderText="IN_SEQ"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<!--
					<TR>
						<TD class="TD" vAlign="top" colSpan="2">
							<TABLE id="Table2" borderColor="silver" cellSpacing="1" cellPadding="0" width="100%" border="1">
								<TR>
									<TD class="tdSmallHeader" style="WIDTH: 108px">Server Name (IP)</TD>
									<TD class="tdSmallHeader" style="WIDTH: 64px">Port</TD>
									<TD class="tdSmallHeader" style="WIDTH: 76px">User ID</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Password</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Reject Next 1</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Reject Track 1</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Reject Next 2</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Reject Track 2</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Cap Approve by</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Cap Track</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Cap</TD>
									<TD class="tdSmallHeader" width="50">Function</TD>
								</TR>
								<TR>
									<TD style="WIDTH: 108px"><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_IPSERVER1" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 64px"><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_PORTFTP1" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 76px"><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_IDFTP1" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_PASSFTP1" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox id="ddl_reject_next1a" style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None"
											BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox id="ddl_reject_track1a" style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None"
											BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox id="ddl_reject_next2a" style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None"
											BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox id="ddl_reject_track2a" style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None"
											BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox id="ddl_cap_approvebya" style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None"
											BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox id="ddl_cap_tracka" style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None"
											BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox onkeypress="return numbersonly()" id="tx_capa" onblur="FormatCurrency(document.Form1.tx_capa)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD borderColor="silver" align="center"><asp:linkbutton id="LinkEdit" runat="server">Edit</asp:linkbutton></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					-->
					<TR>
						<TD class="tdHeader1" vAlign="top" colSpan="2">Request Data</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" PageSize="5" AutoGenerateColumns="False"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
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
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle HorizontalAlign="Center" Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="IN_SEQ" HeaderText="IN_SEQ"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<!--
					<TR>
						<TD class="TD" vAlign="top" colSpan="2">
							<TABLE id="Table1" borderColor="silver" cellSpacing="1" cellPadding="0" width="100%" border="1">
								<TR>
									<TD class="tdSmallHeader">Server Name (IP)</TD>
									<TD class="tdSmallHeader">Port</TD>
									<TD class="tdSmallHeader">User ID</TD>
									<TD class="tdSmallHeader">Password</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Reject Next 1</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Reject Track 1</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Reject Next 2</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Reject Track 2</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Cap</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Cap Approve by</TD>
									<TD class="tdSmallHeader" style="WIDTH: 355px">Cap Track</TD>
								</TR>
								<TR>
									<TD><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_IPSERVER2" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver"
											ReadOnly="True"></asp:textbox></TD>
									<TD><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_PORTFTP2" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver"
											ReadOnly="True"></asp:textbox></TD>
									<TD><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_IDFTP2" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver"
											ReadOnly="True"></asp:textbox></TD>
									<TD><asp:textbox onkeypress="return numbersonly()" id="TXT_IN_PASSFTP2" onblur="FormatCurrency(document.Form1.TXT_IN_FEERECEIVED)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver"
											ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox id="ddl_reject_next1b" style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None"
											BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox id="ddl_reject_track1b" style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None"
											BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox id="ddl_reject_next2b" style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None"
											BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox id="ddl_reject_track2b" style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None"
											BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox id="ddl_cap_approvebyb" style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None"
											BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox id="ddl_cap_trackb" style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None"
											BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
									<TD style="WIDTH: 355px"><asp:textbox onkeypress="return numbersonly()" id="tx_capb" onblur="FormatCurrency(document.Form1.tx_capb)"
											style="TEXT-ALIGN: right" runat="server" Width="100%" BorderStyle="None" BorderColor="Silver" ReadOnly="True"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					-->
					<TR>
						<TD class="TDBGcOLOR2" vAlign="top" colSpan="2"></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
