<%@ Page language="c#" Codebehind="TypeEmploymentParamAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.TypeEmploymentParamAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TypeEmploymentParamAppr</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
														Credit Card General</B></TD>
											</TR>
										</TABLE>
									</TD>
									<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx">
											<IMG src="../../../Image/MainMenu.jpg"></A>&nbsp;<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A><A href="../../../Logout.aspx" target="_top"></A>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							Parameter&nbsp;Type of Employment Approval</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="left" width="50%" colSpan="2">
							<asp:datagrid id="DGR_APPR" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="100%"
								ShowFooter="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="JOB_TYPE_ID" HeaderText="Code">
										<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DES" HeaderText="Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="GR_CODE" HeaderText="Group">
										<HeaderStyle Width="20%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Status">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDING_STATUS">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Approve" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton1" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Reject" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton2" runat="server" CommandName="allReject">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Pending" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
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
