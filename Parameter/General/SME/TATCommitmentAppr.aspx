<%@ Page language="c#" Codebehind="TATCommitmentAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.TATCommitmentAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TATCommitmentAppr</title>
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
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B><B>TAT&nbsp;Commitment&nbsp;:&nbsp;Parameter 
														Approval</B></B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="tdNoBorder" align="right"><A href="../../GeneralParamApprovalAll.aspx?moduleID=01"><IMG src="../../../Image/Back.jpg" border="0"></A>
									<A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A> <A href="../../../Logout.aspx" target="_top">
										<IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2"><B><B>TAT&nbsp;Commitment&nbsp;</B></B> Approval</TD>
				</TR>
				<TR>
					<TD class="td"><ASP:DATAGRID id="DGR_REQUEST" runat="server" Width="100%" AllowPaging="True" CellPadding="1"
							AutoGenerateColumns="False" ShowFooter="True">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="DIMID" HeaderText="Dimention ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DIMDESC" HeaderText="Dimention Desc">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="REFTABLE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="REFFIELDID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="REFFIELDDESC"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CBSTABLE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CBSFIELD"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CBSLINK"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRMTABLE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRMFIELD"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRMLINK"></asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
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
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SUBMIT_TPL" Runat="server" Text="Submit" CssClass="button1" onclick="BTN_SUBMIT_TPL_Click"></asp:button><asp:label id="lbl_rda" runat="server" Visible="False"></asp:label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
