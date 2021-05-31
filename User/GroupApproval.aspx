<%@ Page language="c#" Codebehind="GroupApproval.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.User.GroupApproval" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GroupApproval</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="TableHeader">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Maker: Group  
											Maintenance</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="../Body.aspx"><IMG src="../Image/MainMenu.jpg"></A><A href="../Logout.aspx" target="_top"><IMG src="../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD colspan="2" align="center">
							<asp:HyperLink id="HyperLink1" runat="server" Font-Bold="True" NavigateUrl="User.aspx">User Maintenance</asp:HyperLink>
							|
							<asp:HyperLink id="HyperLink2" runat="server" Font-Bold="True">Group Maintenance</asp:HyperLink></TD>
					</TR>
					<TR>
						<TD colspan="2"><asp:datagrid id="DataGrid1" runat="server" Width="100%" Height="32px" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="tdHdrData"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="GROUPID" HeaderText="Group ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SG_GRPNAME" HeaderText="Group Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="STATUSID" HeaderText="STATUSID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Accept">
										<HeaderStyle Width="75px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_APPROVE" runat="server" GroupName="function"></asp:RadioButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle Width="75px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_REJECT" runat="server" GroupName="function"></asp:RadioButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle Width="75px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_PENDING" runat="server" GroupName="function" Checked="True"></asp:RadioButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="MODULEID" HeaderText="moduleid"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD align="center" colspan="2"></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" align="center" colspan="2">
							<asp:button id="BTN_SUBMIT" runat="server" Width="87px" Text="Submit" CssClass="Button1" onclick="BTN_SUBMIT_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
