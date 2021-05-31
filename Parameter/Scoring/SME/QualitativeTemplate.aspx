<%@ Page language="c#" Codebehind="QualitativeTemplate.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.SME.QualitativeTemplate" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QualitativeTemplate</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include  file="../../../include/cek_entries.html" -->
		<!-- #include  file="../../../include/cek_mandatory.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													Qualitative Template Maker</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="tdNoBorder" align="right"><A href="../../ScoringParamAll.aspx?mc=9902040301&amp;moduleID=40"><IMG src="../../../Image/Back.jpg" border="0"></A>
									<A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A> <A href="../../../Logout.aspx" target="_top">
										<IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter Qualitative Template</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%">
				<TBODY>
					<TR>
						<TD vAlign="top" align="center" colSpan="2">
							<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Template&nbsp;ID</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_TPLID" runat="server" MaxLength="20" CssClass="mandatory2" Width="152px"></asp:textbox><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Template&nbsp;Desc</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_TPLDESC" runat="server" MaxLength="50"
											CssClass="mandatory2" Width="336px"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE_TPL" CssClass="button1" Width="70px" Runat="server" Text="Save" onclick="BTN_SAVE_TPL_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL_TPL" CssClass="button1" Runat="server" Text="Cancel" onclick="BTN_CANCEL_TPL_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Existing Data</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_EXISTING_TPL" runat="server" Width="100%" PageSize="5" AutoGenerateColumns="False"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="QUALTPL_ID" HeaderText="Template ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="QUALTPL_DESC" HeaderText="Template Desc">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete1" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Maker Request</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_REQUEST_TPL" runat="server" Width="100%" PageSize="5" AutoGenerateColumns="False"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="QUALTPL_ID" HeaderText="Template ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="QUALTPL_DESC" HeaderText="Template Desc">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CH_STA"></asp:BoundColumn>
									<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
