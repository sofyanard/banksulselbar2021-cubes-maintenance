<%@ Page language="c#" Codebehind="HolidayParamAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.HolidayParamAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HolidayParamAppr</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_all.html" -->
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table6">
							<TR>
								<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><STRONG>Parameter Approval : 
										General Approval</STRONG></TD>
							</TR>
						</TABLE>
					</TD>
					<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
						<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
					</TD>
				</TR>
				<TR>
					<TD class="tdNoBorder" colSpan="2"></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter Holiday Approval</TD>
				</TR>
				<TR>
					<TD vAlign="top" width="50%" colSpan="2"><ASP:DATAGRID id="DGRequest" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="1"
							AllowPaging="True" Width="100%">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="LBL_NO" runat="server"></asp:Label>&nbsp;
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="HL_DATE" HeaderText="Tanggal Libur">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HL_DESC" HeaderText="Deskripsi Libur">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HL_TYPE" HeaderText="Tipe Libur">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="HL_TYPE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="HL_CODE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Approve">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rdo_Approve" runat="server" GroupName="rdg_Decision"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="BTN_All_Approve" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Reject">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rdo_Reject" runat="server" GroupName="rdg_Decision"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="BTN_All_Reject" runat="server" CommandName="allReject">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pending">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rdo_Pending" runat="server" GroupName="rdg_Decision" Checked="True"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="BTN_All_Pending" runat="server" CommandName="allPend">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</ASP:DATAGRID></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" width="50%" colSpan="2">
						<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label><asp:button id="BTN_SUBMIT" runat="server" Width="100px" Text="Submit" CssClass="Button1" onclick="BTN_SUBMIT_Click"></asp:button>
						<asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label></TD>
				</TR>
			</TABLE>
			<CENTER></CENTER>
		</form>
	</body>
</HTML>
