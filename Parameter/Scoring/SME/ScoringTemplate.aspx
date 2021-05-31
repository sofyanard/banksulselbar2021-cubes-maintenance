<%@ Page language="c#" Codebehind="ScoringTemplate.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.SME.ScoringTemplate" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ScoringTemplate</title>
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
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Conversion&nbsp;Formula</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_TPLFORMULA" runat="server" MaxLength="250" Width="336px"></asp:textbox></TD>
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
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2">&nbsp;</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter Template Value</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="2">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Template ID</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SCOTPL_ID" runat="server" CssClass="mandatory"></asp:dropdownlist><asp:label id="LBL_SAVEMODE2" runat="server" Visible="False">1</asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Parameter ID</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PARAM_ID" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_PARAM_ID_SelectedIndexChanged"></asp:dropdownlist><asp:label id="LBL_MIN" runat="server" Visible="False"></asp:label><asp:label id="LBL_MAX" runat="server" Visible="False"></asp:label><asp:label id="LBL_PRMVALNAME" runat="server" Visible="False"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Parameter Value ID</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_PARAM_VALUE_ID" runat="server" MaxLength="10" CssClass="mandatory"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Direct Value</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:checkbox id="CHK_DIRECT_FLAG" runat="server" Text="(check if yes)" AutoPostBack="True" oncheckedchanged="CHK_DIRECT_FLAG_CheckedChanged"></asp:checkbox></TD>
								</TR>
							</TABLE>
							<TABLE id="TR_PARAM" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Value</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_REF_VALUE" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
							<TABLE id="TR_VALUE" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Minimum Value</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly3()" id="TXT_MIN_VALUE" runat="server" MaxLength="30"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Maximum Value</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly3()" id="TXT_MAX_VALUE" runat="server" MaxLength="30"></asp:textbox></TD>
								</TR>
							</TABLE>
							<TABLE id="TR_SCORE" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 300px">Parameter Score</TD>
									<TD style="WIDTH: 7px">:</TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_PARAM_SCORE" runat="server" MaxLength="15"
											Width="136px"></asp:textbox></TD>
								</TR>
							</TABLE>
							<TABLE cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<TBODY>
									<TR>
										<TD class="TDBGColor1" style="WIDTH: 300px">Parameter Weight</TD>
										<TD style="WIDTH: 7px">:</TD>
										<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_PARAM_WEIGHT" runat="server" MaxLength="15"
												Width="136px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="TDBGColor1" style="WIDTH: 300px">Remarks</TD>
										<TD style="WIDTH: 7px">:</TD>
										<TD class="TDBGColorValue"><asp:textbox id="TXT_REMARKS" runat="server" MaxLength="30" Width="300px"></asp:textbox></TD>
									</TR>
								</TBODY>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_FIND_VAL" CssClass="button1" Width="70px" Runat="server" Text="Find" onclick="BTN_FIND_VAL_Click"></asp:button>&nbsp;
							<asp:button id="BTN_SAVE_VAL" CssClass="button1" Width="70px" Runat="server" Text="Save" onclick="BTN_SAVE_VAL_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL_VAL" CssClass="button1" Runat="server" Text="Cancel" onclick="BTN_CANCEL_VAL_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Existing Data</TD>
					</TR>
					<TR id="tr_grd_value_exist" runat="server">
						<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_EXISTING_VAL" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
								<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
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
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit3" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete3" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" style="HEIGHT: 26px" colSpan="2">Maker&nbsp;Request&nbsp;</TD>
					</TR>
					<TR id="tr_grd_value" runat="server">
						<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_REQUEST_VAL" runat="server" Width="100%" PageSize="5" AutoGenerateColumns="False"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
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
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit4" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete4" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" colSpan="2">&nbsp;</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
