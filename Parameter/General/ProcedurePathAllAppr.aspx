<%@ Page language="c#" Codebehind="ProcedurePathAllAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.ProcedurePathAllAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProcedurePathAllAppr</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder" width="50%" style="HEIGHT: 51px">
						<TABLE id="Table6">
							<TR>
								<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
										Common</B></TD>
							</TR>
						</TABLE>
					</TD>
					<TD style="HEIGHT: 51px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../Body.aspx">
							<IMG src="../../Image/MainMenu.jpg"></A>&nbsp;<A href="../../Logout.aspx" target="_top"><IMG src="../../Image/Logout.jpg"></A><A href="../../Logout.aspx" target="_top"></A>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Path Procedure (Track Decision) 
						Approval</TD>
				</TR>
				<TR>
					<TD class="td" style="HEIGHT: 50px" vAlign="top" colSpan="2"><TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="tdLabel" style="WIDTH: 205px" align="right" width="205" bgColor="#d"><STRONG><FONT color="#330099">Module</FONT></STRONG></TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:radiobuttonlist id="RBL_MODULE" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
										Width="248px" onselectedindexchanged="RBL_MODULE_SelectedIndexChanged">
										<asp:ListItem Value="20">Credit Card</asp:ListItem>
										<asp:ListItem Value="40">Consumer</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
						</TABLE>
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<TR id="TR_INT" runat="server">
							</TR>
						</TABLE>
						<asp:datagrid id="DGR_APPR" runat="server" Width="100%" PageSize="20" AllowPaging="True" HorizontalAlign="Center"
							ShowFooter="True">
							<AlternatingItemStyle CssClass="TblALternating"></AlternatingItemStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Approve">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rdo_Approve" runat="server" GroupName="approval_status"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="Linkbutton3" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Reject">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rdo_Reject" runat="server" GroupName="approval_status"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="Linkbutton4" runat="server" CommandName="allReject">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pending">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rdo_Pending" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="Linkbutton5" runat="server" CommandName="allPend">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
					</TD></TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2"><asp:button id="BTN_SUBMIT" Width="70px" Runat="server" Text="Submit" CssClass="button1" onclick="BTN_SUBMIT_Click"></asp:button>&nbsp;<asp:label id="LBL_STA" runat="server" Visible="False"></asp:label>
						<asp:label id="Label1" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_KEY" runat="server" Visible="False"></asp:label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
