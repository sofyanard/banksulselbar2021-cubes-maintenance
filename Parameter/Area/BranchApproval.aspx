<%@ Page language="c#" Codebehind="BranchApproval.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Area.BranchApproval" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BranchApproval</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="TableHeader">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Approval: 
											Branch</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="../Body.aspx"><IMG src="../../Image/MainMenu.jpg"></A><A href="../Logout.aspx" target="_top"><IMG src="../../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD colSpan="2"><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" Height="32px" Width="100%">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="BRANCH_CODE" HeaderText="Branch Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_NAME" HeaderText="Branch Name">
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
