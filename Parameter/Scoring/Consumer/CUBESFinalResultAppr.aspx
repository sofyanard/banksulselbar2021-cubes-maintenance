<%@ Page language="c#" Codebehind="CUBESFinalResultAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Consumer.CUBESFinalResultAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CUBES Final Result Approval</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px" cellSpacing="0" cellPadding="0" width="408" border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													Scoring Approval</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="tdNoBorder" align="right">
									<A href="../../ScoringParamApprovalAll.aspx?mc=9902040302&amp;moduleID=40"><IMG src="../../../Image/Back.jpg" border="0"></A>
									<A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A> <A href="../../../Logout.aspx" target="_top">
										<IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						Parameter&nbsp;CUBES Final Result Approval</TD>
				</TR>
				<TR>
					<TD class="td">
						<asp:datagrid id="DGR_APPR" runat="server" AutoGenerateColumns="False" PageSize="5" AllowPaging="True"
							Width="100%" ShowFooter="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="RESULT_ID" HeaderText="ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="RESULT_NAME" HeaderText="Result Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRODUCTID" HeaderText="PRODUCT ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRODUCT_NAME" HeaderText="Product Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MIN_RANGE" HeaderText="Min. Range">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MAX_RANGE" HeaderText="Max. Range">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA" HeaderText="Status ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="SEQ_ID" HeaderText="SEQ"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Approve">
									<HeaderStyle Width="80px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rd_Approve" runat="server" GroupName="RBGroup"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="LBTN_allAppr" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Reject">
									<HeaderStyle Width="80px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rd_Reject" runat="server" GroupName="RBGroup"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="LBTN_AllReject" runat="server" CommandName="allReject">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pending">
									<HeaderStyle Width="80px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rd_Pending" runat="server" GroupName="RBGroup" Checked="True"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="LBTN_allPend" runat="server" CommandName="allPend">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SUBMIT" Runat="server" Text="Submit" CssClass="button1" onclick="BTN_SUBMIT_Click"></asp:button>
						<asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
