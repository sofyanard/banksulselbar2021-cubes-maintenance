<%@ Page language="c#" Codebehind="ScoringOther.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Consumer.ScoringOther" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ScoringOther</title>
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
													Scoring&nbsp;Item Other Maker</B></TD>
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
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Scoring Item Other</TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px">ID</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ID1" runat="server" MaxLength="3" CssClass="mandatory"
										Width="72px"></asp:textbox><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter Name</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_PARAM_NAME" runat="server" MaxLength="50"
										CssClass="mandatory" Width="400px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter Formula</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox id="TXT_PARAM_FORMULA" runat="server" Width="100%" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter Field</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_PARAM_FIELD" runat="server" MaxLength="100"
										Width="336px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter table</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_PARAM_TABLE" runat="server" MaxLength="100"
										Width="336px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter table Child</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_PARAM_TABLE_CHILD" runat="server" MaxLength="100"
										Width="208px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter PRM</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:radiobuttonlist id="RBL_PARAM_PRM" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="RBL_PARAM_PRM_SelectedIndexChanged">
										<asp:ListItem Value="0">CUBES</asp:ListItem>
										<asp:ListItem Value="1">PROMPT</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter Active</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:radiobuttonlist id="RBL_PARAM_ACTIVE" runat="server" Width="160px" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="RBL_PARAM_ACTIVE_SelectedIndexChanged">
										<asp:ListItem Value="1">Active</asp:ListItem>
										<asp:ListItem Value="0">Non Active</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter Link</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_PARAM_LINK" runat="server" Width="100%"
										TextMode="MultiLine"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE_LIST" CssClass="button1" Width="70px" Text="Save" Runat="server" onclick="BTN_SAVE_LIST_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL_LIST" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_LIST_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						Existing Data
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_EXISTING_LIST" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="PARAM_ID" HeaderText="ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_NAME" HeaderText="Parameter Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_FORMULA" HeaderText="Parameter Formula">
									<HeaderStyle Width="30%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_FIELD" HeaderText="Parameter Field">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_TABLE" HeaderText="Parameter Table">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_TABLE_CHILD" HeaderText="Parameter Table Child">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_LINK" HeaderText="Parameter Link">
									<HeaderStyle Width="30%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_PRM" HeaderText="Parameter PRM">
									<HeaderStyle Width="3%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_ACTIVE" HeaderText="Parameter Active">
									<HeaderStyle Width="3%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
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
					<TD class="tdHeader1" colSpan="2">
						Maker Request</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_REQUEST_LIST" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="PARAM_ID" HeaderText="ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_NAME" HeaderText="Parameter Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_FORMULA" HeaderText="Parameter Formula">
									<HeaderStyle Width="30%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_FIELD" HeaderText="Parameter Field">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_TABLE" HeaderText="Parameter Table">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_TABLE_CHILD" HeaderText="Parameter Table Child">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_LINK" HeaderText="Parameter Link">
									<HeaderStyle Width="30%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_PRM" HeaderText="Parameter PRM">
									<HeaderStyle Width="3%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_ACTIVE" HeaderText="Parameter Active">
									<HeaderStyle Width="3%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA" HeaderText="Status ID"></asp:BoundColumn>
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
				<TR>
					<TD class="TDBGColor2" colSpan="2">&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
