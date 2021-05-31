<%@ Page language="c#" Codebehind="ScoringHistory.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Small.ScoringHistory" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>SearchParameter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_entries.html" -->
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table4" width="100%" border="0">
					<TR>
						<TD class="tdNoBorder" style="WIDTH: 929px" align="left" width="929">
							<TABLE id="Table3">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Scoring History</B></TD>
								</TR>
							</TABLE>
						</TD>
						<td align="right"><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A><A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A></td>
					</TR>
					<TR>
						<TD align="center" colSpan="2">
							<TABLE class="td" id="Table1" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1"
								cellPadding="1" width="590" border="1">
								<TR>
									<TD class="tdHeader1">Search Criteria</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center">
										<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="TDBGColor1" width="170">AP_REGNO</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtApREGNO" runat="server" Width="200px" MaxLength="20"></asp:textbox><asp:textbox id="_txtId" runat="server" Width="200px" MaxLength="20" Visible="False"></asp:textbox></TD>
											</TR>
											<tr>
												<TD></TD>
											</tr>
											<TR>
												<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnFind" runat="server" Width="180px" CssClass="button1" Text="Find" onclick="_btnFind_Click"></asp:button>&nbsp; 
													<!-- <asp:button id="_btnNew" runat="server" Width="180px" Text="New Rule" CssClass="button1"></asp:button>&nbsp;--></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2">&nbsp;</TD>
					</TR>
					<TR align="center">
						<TD colSpan="2"><ASP:DATAGRID id="DatGrd" runat="server" Width="90%" AllowPaging="True" AutoGenerateColumns="False"
								CellPadding="1">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="TANGGAL" HeaderText="Tanggal">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SEQ" HeaderText="Seq">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SCORERESULT" HeaderText="Accept">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESCRIPTION" HeaderText="Result">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TOTAL" HeaderText="Total">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TEMPLATE" HeaderText="Template">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SUMBERDATA" HeaderText="Sumberdata">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="View" HeaderText="Composition" CommandName="Composition">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="View" HeaderText="Rule" CommandName="Rule">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
					<TR>
					</TR>
					<TR id="TR_RULE" align="center" runat="server">
						<TD colSpan="2"><ASP:DATAGRID id="DatGrdRule" runat="server" Width="50%" AllowPaging="True" AutoGenerateColumns="False"
								CellPadding="1">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SUMBERDATA" HeaderText="Sumber Data">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SEQ" HeaderText="Seq">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESCRIPTION" HeaderText="Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="70%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID>
						</TD>
					</TR>
					<TR id="TR_COMPOSITION" align="center" runat="server">
						<TD colSpan="2"><ASP:DATAGRID id="DatGrdComposition" runat="server" Width="50%" AllowPaging="True" AutoGenerateColumns="False"
								CellPadding="1">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SEQ" HeaderText="Seq">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESC" HeaderText="Desc">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="70%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VALUE" HeaderText="Value">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
				</TABLE>
				<asp:TextBox ID="_txtApRegnoHidden" Runat="Server" Visible="False"></asp:TextBox>
				<asp:TextBox ID="_txtSeqComposition" Runat="Server" Visible="False"></asp:TextBox>
				<asp:TextBox ID="_txtSeqRuleReason" Runat="Server" Visible="False"></asp:TextBox>
			</center>
		</form>
	</body>
</HTML>
