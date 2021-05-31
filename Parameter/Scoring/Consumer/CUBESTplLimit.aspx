<%@ Page language="c#" Codebehind="CUBESTplLimit.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Consumer.CUBESTplLimit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CUBESTplLimit</title>
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
									<TABLE id="Table5" style="WIDTH: 408px" cellSpacing="0" cellPadding="0" width="408" border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Template 
													Maintenance&nbsp;: Limit List Maker</B></TD>
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
					<TD class="tdHeader1" colSpan="2">Parameter Template Maker</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="2">
						<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Template ID</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_TPLID" runat="server" Width="152px" CssClass="mandatory2" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Template Desc</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_TPLDESC" runat="server" Width="336px" CssClass="mandatory2"
										MaxLength="50"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE_TPL" Width="70px" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_TPL_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL_TPL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_TPL_Click"></asp:button><asp:label id="LBL_SAVEMODE_TPL" runat="server" Visible="False">1</asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Existing Data</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_EXISTING_TPL" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="LMTTPLID" HeaderText="Template ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LMTTPLDESC" HeaderText="Template Desc">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit3" runat="server" CommandName="Edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete3" runat="server" CommandName="Delete">Delete</asp:LinkButton>
										<asp:Label id="lbl_Status" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="ACTIVE"></asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Maker Request</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_REQUEST_TPL" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="LMTTPLID" HeaderText="Template ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LMTTPLDESC" HeaderText="Template Desc">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit4" runat="server" CommandName="Edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete4" runat="server" CommandName="Delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter Value Maker</TD>
				</TR>
				<TR>
					<TD class="td">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 18px">Template ID</TD>
								<TD style="WIDTH: 7px; HEIGHT: 18px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 18px"><asp:dropdownlist id="DDL_TPLID" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_TPLID_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter PRM</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:radiobuttonlist id="RBL_PRM" runat="server" Width="520px" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="RBL_PRM_SelectedIndexChanged">
										<asp:ListItem Value="0">CUBES</asp:ListItem>
										<asp:ListItem Value="1">PROMPT</asp:ListItem>
									</asp:radiobuttonlist><asp:label id="LBL_SAVEMODE_VAL" runat="server" Visible="False">1</asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Limit Name</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox id="TXT_LIMIT_NAME" runat="server" Width="440px" MaxLength="50"></asp:textbox><asp:label id="LBL_SEQ_NO" runat="server" Visible="False"></asp:label><asp:label onkeypress="return kutip_satu()" id="LBL_SEQ_ID" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Limit Formula</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox id="TXT_LIMIT_FORMULA" runat="server" Width="100%" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Limit Table</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_LIMIT_TABLE" runat="server" Width="100%"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Limit Link</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox id="TXT_LIMIT_LINK" runat="server" Width="100%" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px">Limit Active</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:radiobuttonlist id="RBL_LIMIT_ACTIVE" runat="server" Width="304px" RepeatDirection="Horizontal"
										AutoPostBack="True" onselectedindexchanged="RBL_LIMIT_ACTIVE_SelectedIndexChanged">
										<asp:ListItem Value="1">Active</asp:ListItem>
										<asp:ListItem Value="0">Non Active</asp:ListItem>
									</asp:radiobuttonlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="LBL_PRM" runat="server" Visible="False"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE_VAL" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_VAL_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL_VAL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_VAL_Click"></asp:button><asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Existing Data</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_EXISTING_VAL" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="LMTTPLID" HeaderText="Template ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SEQ_NO" HeaderText="Sequential No.">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="LOAN_CODE_DESC"></asp:BoundColumn>
								<asp:BoundColumn DataField="LIMIT_NAME" HeaderText="Limit Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LIMIT_FORMULA" HeaderText="Limit Formula">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LIMIT_TABLE" HeaderText="Limit Table">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LIMIT_LINK" HeaderText="Limit Link">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="LIMIT_ACTIVE"></asp:BoundColumn>
								<asp:BoundColumn DataField="LIMAC" HeaderText="Limit Active">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRM_CODE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="LOAN_CODE"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_REQUEST_VAL" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="LMTTPLID" HeaderText="Template ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SEQ_NO" HeaderText="Sequential No.">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="LOAN_CODE_DESC"></asp:BoundColumn>
								<asp:BoundColumn DataField="LIMIT_NAME" HeaderText="Limit Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LIMIT_FORMULA" HeaderText="Limit Formula">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LIMIT_TABLE" HeaderText="Limit Table">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LIMIT_LINK" HeaderText="Limit Link">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="LIMIT_ACTIVE"></asp:BoundColumn>
								<asp:BoundColumn DataField="LIMAC" HeaderText="Limit Active">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRM_CODE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA"></asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="SEQ_ID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="LOAN_CODE"></asp:BoundColumn>
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
