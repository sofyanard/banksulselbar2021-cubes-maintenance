<%@ Page language="c#" Codebehind="RFTemplateParamConAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.RFTemplateParamConAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFTemplateParamConAppr</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../include/cek_all.html" -->
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
							<TABLE id="Table6">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Consumer 
											General&nbsp; - Approval</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../Body.aspx"><IMG src="../../Image/MainMenu.jpg"></A>
							<A href="../../Logout.aspx" target="_top"><IMG src="../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdNoBorder" colspan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2" align="center">
							<asp:Label id="LBL_PARAMNAME" runat="server"></asp:Label></TD>
					</TR>
					<asp:label id="lbl_CU_CUSTTYPEID" runat="server" Visible="False"></asp:label>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2">
							<ASP:DATAGRID id="DGRequest" runat="server" Width="100%" AllowPaging="True" CellPadding="1" AutoGenerateColumns="False"
								ShowFooter="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID">
										<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESC" HeaderText="Description">
										<HeaderStyle Width="500px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CD_SIBS" HeaderText="SIBS Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Pending Status">
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
											<asp:LinkButton id="BTN_All_Reject" runat="server" CommandName="allRejc">Select All</asp:LinkButton>
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
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" width="50%" colSpan="2">
							<asp:Label id="LBL_DB_NAMA" runat="server" Visible="False"></asp:Label>
							<asp:Label id="LBL_DB_IP" runat="server" Visible="False"></asp:Label>
							<asp:button id="BTN_SUBMIT" runat="server" CssClass="Button1" Width="100px" Text="Submit" onclick="BTN_SUBMIT_Click"></asp:button>
							<asp:Label id="LBL_ACTIVE" runat="server" Visible="False"></asp:Label>
							<asp:Label id="LBL_ID" runat="server" Visible="False"></asp:Label>
							<asp:Label id="LBL_DESC" runat="server" Visible="False"></asp:Label></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
