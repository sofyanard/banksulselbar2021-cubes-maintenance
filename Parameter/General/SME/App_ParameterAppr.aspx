<%@ Page language="c#" Codebehind="App_ParameterAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.App_ParameterAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>App_ParameterAppr</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
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
														General</B></TD>
											</TR>
										</TABLE>
									</TD>
									<TD style="HEIGHT: 41px" align="right">
									<asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton>
									<A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
									<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
									<A href="../../../Logout.aspx" target="_top"></A>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							Parameter&nbsp;Scoring/Rating Counter&nbsp;Approval</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="left" width="50%" colSpan="2">
							<asp:datagrid id="DGR_APPR" runat="server" ShowFooter="True" Width="100%" AllowPaging="True" PageSize="5"
								AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="SEQ" HeaderText="SEQ">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MAX_PS_COUNT" HeaderText="Max. Pre Scoring Count">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MAX_FS_COUNT" HeaderText="Max. Final Scoring">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MAX_ACQ_COUNT" HeaderText="Max. Acquire Information Count">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PENDINGSTATUS" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rd_Approve" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton1" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rd_Reject" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton2" runat="server" CommandName="allReject">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rd_Pending" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton3" runat="server" CommandName="allPend">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2">
							<asp:button id="BTN_SUBMIT" CssClass="button1" Text="Submit" Runat="server" onclick="BTN_SUBMIT_Click"></asp:button>&nbsp;&nbsp;</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
