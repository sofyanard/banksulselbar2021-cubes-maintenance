<%@ Page language="c#" Codebehind="RejReasonParamAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.RejReasonParamAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Reject Reason Parameter Approval</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													General Approval</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
									<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Reject Reason&nbsp;Approval</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="left" width="50%" colSpan="2"><ASP:DATAGRID id="DG_APPR" runat="server" AutoGenerateColumns="False" CellPadding="1" AllowPaging="True"
							Width="100%" ShowFooter="True">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="RJ_CODE" HeaderText="Code">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="RJ_DESC" HeaderText="Description">
									<HeaderStyle Width="40%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_TMP" HeaderText="Sibs Code">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="STATUS" HeaderText="Status">
									<HeaderStyle Width="13%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA" Visible="False">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Approve">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="Rdb1" runat="server" GroupName="approval_status"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="Lb1" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Reject">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="Rdb2" runat="server" GroupName="approval_status"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="Lb2" runat="server" CommandName="allReject">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pending">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="Rdb3" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="Lb3" runat="server" CommandName="allPend">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</ASP:DATAGRID></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2">
						<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label><asp:button id="BTN_SUBMIT" CssClass="button1" Text="Submit" Runat="server" onclick="BTN_SUBMIT_Click"></asp:button><asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
