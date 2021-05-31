<%@ Page language="c#" Codebehind="UserApproval.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.User.UserApproval" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UserApproval</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Approval: User  
											Maintenance</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="../Body.aspx"><IMG src="../Image/MainMenu.jpg"></A><A href="../Logout.aspx" target="_top"><IMG src="../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2"><asp:hyperlink id="HyperLink1" runat="server" NavigateUrl="User.aspx" Font-Bold="True">User Maintenance</asp:hyperlink>|
							<asp:hyperlink id="HyperLink2" runat="server" Font-Bold="True">Group Maintenance</asp:hyperlink></TD>
					</TR>
					<TR>
						<TD colSpan="2"><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" Height="32px" Width="100%">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="USERID" HeaderText="User ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SU_FULLNAME" HeaderText="Full Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SG_GRPNAME" HeaderText="Group">
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
									<asp:BoundColumn Visible="False" DataField="GROUPID" HeaderText="groupid"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" align="center" colSpan="2"><asp:button id="BTN_SUBMIT" runat="server" Width="87px" CssClass="Button1" Text="Submit" onclick="BTN_SUBMIT_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
