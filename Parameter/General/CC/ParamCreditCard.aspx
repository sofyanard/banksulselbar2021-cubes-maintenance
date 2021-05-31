<%@ Page language="c#" Codebehind="ParamCreditCard.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.ParamCreditCard" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ParamCreditCard</title>
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
						<TD style="HEIGHT: 25px" align="center" colSpan="2"><asp:placeholder id="Menu" runat="server" Visible="False"></asp:placeholder></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" style="HEIGHT: 25px" align="center" colSpan="2">Others</TD>
					</TR>
					<tr>
						<td vAlign="top" colSpan="2">
							<fieldset class="TDBGFieldset"><legend class="TDBGLegend">&nbsp;&nbsp;Agent&nbsp;&nbsp;</legend>
								<TABLE cellSpacing="0" cellPadding="0" width="95%">
									<TR>
										<TD class="TDBGHeaderGrid" width="120">Agent ID</TD>
										<TD class="TDBGHeaderGrid">Agent Name</TD>
										<TD class="TDBGHeaderGrid" width="120">Max Application</TD>
										<TD class="TDBGHeaderGrid" width="100">Function</TD>
									</TR>
									<TR>
										<TD class="TDBGColorValue" align="center"><asp:label id="LBL_AGENTID" runat="server"></asp:label>&nbsp;</TD>
										<TD class="TDBGColorValue"><asp:label id="LBL_AGENTNAME" runat="server"></asp:label>&nbsp;</TD>
										<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_MAXAPP" style="TEXT-ALIGN: right" runat="server"
												Enabled="False" Width="120px"></asp:textbox></TD>
										<TD class="TDBGColorValue" align="center"><asp:button id="BTN_AGENT" runat="server" Width="55px" Text="Save" CssClass="Button1" onclick="BTN_AGENT_Click"></asp:button></TD>
									</TR>
								</TABLE>
								<BR>
								<asp:datagrid id="GRD_AGENT" runat="server" Width="95%" CssClass="TDBGGrid" AllowPaging="True"
									PageSize="5" AutoGenerateColumns="False">
									<SelectedItemStyle CssClass="mandatory"></SelectedItemStyle>
									<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="agencyid" HeaderText="Agent ID">
											<HeaderStyle Font-Bold="True" Width="120px" CssClass="TDBGHeaderGrid"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="agencyname" HeaderText="Agent Name">
											<HeaderStyle Font-Bold="True" CssClass="TDBGHeaderGrid"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="maxapp" HeaderText="Max Applicaiton">
											<HeaderStyle Font-Bold="True" CssClass="TDBGHeaderGrid"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right" Width="120px"></ItemStyle>
										</asp:BoundColumn>
										<asp:ButtonColumn Text="Edit" CommandName="edit">
											<HeaderStyle Font-Bold="True" CssClass="TDBGHeaderGrid"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
										</asp:ButtonColumn>
									</Columns>
									<PagerStyle Mode="NumericPages"></PagerStyle>
								</asp:datagrid><BR>
								<TABLE cellSpacing="0" cellPadding="0" width="95%">
									<TR>
										<TD class="tdBGColor1" width="250">Max day for agent (Jabotabek)</TD>
										<TD width="15"></TD>
										<TD class="tdBGColorValue">
											<asp:textbox onkeypress="return numbersonly()" id="TXT_IN_MAXDAYAGENTJB" style="TEXT-ALIGN: right"
												runat="server" Width="120px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Max day for agent (Luar Jabotabek)</TD>
										<TD width="15"></TD>
										<TD class="tdBGColorValue">
											<asp:textbox onkeypress="return numbersonly()" id="TXT_IN_MAXDAYAGENTLJB" style="TEXT-ALIGN: right"
												runat="server" Width="120px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">&nbsp;</TD>
										<TD width="15"></TD>
										<TD class="tdBGColorValue">
											<asp:button id="BTN_SAVEAGENT" runat="server" Width="55px" Text="Save" CssClass="Button1" onclick="BTN_SAVEAGENT_Click"></asp:button></TD>
									</TR>
									<TR>
										<TD align="right" colSpan="3">
											<asp:button id="Button1" runat="server" Text="Setup Zipcode" onclick="Button1_Click"></asp:button></TD>
									</TR>
								</TABLE>
								<BR>
							</fieldset>
							<br>
							<fieldset class="TDBGFieldset"><legend class="TDBGLegend">&nbsp;&nbsp;Stage&nbsp;</legend>
								<TABLE cellSpacing="0" cellPadding="0" width="95%">
									<TR>
										<TD class="TDBGHeaderGrid" width="120">Track Code</TD>
										<TD class="TDBGHeaderGrid">Track Desc</TD>
										<TD class="TDBGHeaderGrid" width="120">Max day in stage</TD>
										<TD class="TDBGHeaderGrid" width="100">Function</TD>
									</TR>
									<TR>
										<TD class="TDBGColorValue" align="center"><asp:label id="LBL_TR_CODE" runat="server"></asp:label>&nbsp;</TD>
										<TD class="TDBGColorValue"><asp:label id="LBL_TR_DESC" runat="server"></asp:label>&nbsp;</TD>
										<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_MAXDAY" style="TEXT-ALIGN: right" runat="server"
												Enabled="False" Width="120px"></asp:textbox></TD>
										<TD class="TDBGColorValue" align="center"><asp:button id="BTN_SAVESTAGE" runat="server" Width="55px" Text="Save" CssClass="Button1" onclick="BTN_SAVESTAGE_Click"></asp:button></TD>
									</TR>
								</TABLE>
								<BR>
								<asp:datagrid id="GRD_STAGE" runat="server" Width="95%" CssClass="TDBGGrid" AllowPaging="True"
									PageSize="5" AutoGenerateColumns="False">
									<SelectedItemStyle CssClass="mandatory"></SelectedItemStyle>
									<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="tr_code" HeaderText="Track Code">
											<HeaderStyle Font-Bold="True" CssClass="TDBGHeaderGrid"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="tr_desc" HeaderText="Track Desc">
											<HeaderStyle Font-Bold="True" CssClass="TDBGHeaderGrid"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="maxday" HeaderText="Max day in stage">
											<HeaderStyle Font-Bold="True" CssClass="TDBGHeaderGrid"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right" Width="120px"></ItemStyle>
										</asp:BoundColumn>
										<asp:ButtonColumn Text="Edit" CommandName="edit">
											<HeaderStyle Font-Bold="True" CssClass="TDBGHeaderGrid"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
										</asp:ButtonColumn>
									</Columns>
									<PagerStyle Mode="NumericPages"></PagerStyle>
								</asp:datagrid><BR>
							</fieldset>
							<br>
							<fieldset class="TDBGFieldset"><legend class="TDBGLegend">&nbsp;&nbsp;Other&nbsp;&nbsp;</legend>
								<table id="table8" width="99%">
									<TR>
										<TD class="tdBGColor1" width="250">Group Administrator</TD>
										<TD width="15"></TD>
										<TD class="tdBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_IN_GROUPADMIN" runat="server" Width="190px"
												MaxLength="20"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Group Verification</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_IN_GROUPVER" runat="server" Width="190px"
												MaxLength="20"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">Group Analyst</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_IN_GROUPANL" runat="server" Width="190px"
												MaxLength="20"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="tdBGColor1">&nbsp;</TD>
										<TD></TD>
										<TD class="tdBGColorValue"><asp:button id="BTN_SAVEOTHER" runat="server" Width="55px" Text="Save" CssClass="Button1" onclick="BTN_SAVEOTHER_Click"></asp:button></TD>
									</TR>
								</table>
								<br>
							</fieldset>
						</td>
					</tr>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
