<%@ Page language="c#" Codebehind="ScoringModel.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.SME.ScoringModel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ScoringModel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
													Scoring Template Maker</B></TD>
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
					<TD class="tdHeader1" colSpan="2">Parameter Scoring Template</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%">
				<TBODY>
					<TR>
						<TD vAlign="top" align="center" colSpan="2">
							<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Sub Segment Program</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PROGRAM" runat="server" CssClass="mandatory" AutoPostBack="True"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Template</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_TEMPLATE" runat="server" CssClass="mandatory"></asp:dropdownlist><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" Width="70px" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Existing Data</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								PageSize="5">
								<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="PROGRAM_ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAM_DESC" HeaderText="Sub Segment Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="TEMPLATE_ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="TEMPLATE_DESC" HeaderText="Template">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
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
						<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								PageSize="5">
								<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="PROGRAM_ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAM_DESC" HeaderText="Sub Segment Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="TEMPLATE_ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="TEMPLATE_DESC" HeaderText="Template">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
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
