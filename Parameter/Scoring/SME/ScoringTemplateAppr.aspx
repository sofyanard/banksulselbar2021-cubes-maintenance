<%@ Page language="c#" Codebehind="ScoringTemplateAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.SME.ScoringTemplateAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ScoringTemplateAppr</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include  file="../../../include/cek_entries.html" -->
		<!-- #include  file="../../../include/cek_mandatory.html" -->
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TBODY>
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD style="HEIGHT: 41px" width="50%">
										<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
											border="0">
											<TR>
												<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
														Scoring Template Approval</B></TD>
											</TR>
										</TABLE>
									</TD>
									<TD class="tdNoBorder" align="right"><A href="../../ScoringParamApprovalAll.aspx?mc=9902040301&amp;moduleID=40"><IMG src="../../../Image/Back.jpg" border="0"></A>
										<A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A> <A href="../../../Logout.aspx" target="_top">
											<IMG src="../../../Image/Logout.jpg"></A>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" align="center" width="50%" colSpan="2">Parameter 
							Scoring Template</TD>
					</TR>
					<asp:label id="lbl_CU_CUSTTYPEID" runat="server" Visible="False"></asp:label>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2"><ASP:DATAGRID id="DGR_REQUEST_TPL" runat="server" Width="100%" AllowPaging="True" CellPadding="1"
								AutoGenerateColumns="False" ShowFooter="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SCOTPL_ID" HeaderText="Template ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SCOTPL_DESC" HeaderText="Template Desc">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SCORE_FORMULA" HeaderText="Conversion Formula">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CH_STA"></asp:BoundColumn>
									<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rd_Approve" runat="server" GroupName="rdg_Decision"></asp:RadioButton>
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
											<asp:RadioButton id="rd_Reject" runat="server" GroupName="rdg_Decision"></asp:RadioButton>
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
											<asp:RadioButton id="rd_Pending" runat="server" GroupName="rdg_Decision" Checked="True"></asp:RadioButton>
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
						<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SUBMIT_TPL" runat="server" Width="100px" CssClass="Button1" Text="Submit" onclick="BTN_SUBMIT_TPL_Click"></asp:button></TD>
					</TR>
					<tr id="tr_grd_value" runat="server">
						<td>
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="tdHeader1" vAlign="top" align="center" width="50%" colSpan="2">Parameter 
										Template Value</TD>
								</TR>
								<TR>
									<TD vAlign="top" width="50%" colSpan="2"><ASP:DATAGRID id="DGR_REQUEST_VAL" runat="server" Width="100%" AllowPaging="True" CellPadding="1"
											AutoGenerateColumns="False" ShowFooter="True">
											<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
											<Columns>
												<asp:BoundColumn DataField="SCOTPL_ID" HeaderText="Template ID">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PARAM_ID" HeaderText="Param ID" Visible="False">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PARAM_NAME" HeaderText="Parameter Name">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PARAM_VALUE_ID" HeaderText="Parameter Value ID">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DIRECT_FLAG" HeaderText="Direct">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="MIN_VALUE" HeaderText="Min Value">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="MAX_VALUE" HeaderText="Max Value">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PARAM_SCORE" HeaderText="Parameter Score">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PARAM_WEIGHT" HeaderText="Parameter Weight">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="REMARKS" HeaderText="Value Description">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="CH_STA" HeaderText="Status ID"></asp:BoundColumn>
												<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Approve">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:RadioButton id="rd_Approve1" runat="server" GroupName="rdg_Decision"></asp:RadioButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="BTN_All_Approve1" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reject">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:RadioButton id="rd_Reject1" runat="server" GroupName="rdg_Decision"></asp:RadioButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="BTN_All_Reject1" runat="server" CommandName="allRejc">Select All</asp:LinkButton>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Pending">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:RadioButton id="rd_Pending1" runat="server" GroupName="rdg_Decision" Checked="True"></asp:RadioButton>
													</ItemTemplate>
													<FooterStyle HorizontalAlign="Center"></FooterStyle>
													<FooterTemplate>
														<asp:LinkButton id="BTN_All_Pending1" runat="server" CommandName="allPend">Select All</asp:LinkButton>
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle Mode="NumericPages"></PagerStyle>
										</ASP:DATAGRID></TD>
								</TR>
							</table>
						</td>
					</tr>
					<TR>
						<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SUBMIT_VAL" runat="server" Width="100px" CssClass="Button1" Text="Submit" onclick="BTN_SUBMIT_VAL_Click"></asp:button></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></FORM>
	</body>
</HTML>
