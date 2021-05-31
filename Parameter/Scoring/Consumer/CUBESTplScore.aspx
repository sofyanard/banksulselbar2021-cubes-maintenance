<%@ Page language="c#" Codebehind="CUBESTplScore.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Consumer.CUBESTplScore" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CUBESTplScore</title>
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
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Template&nbsp;Maintenance 
													: Scoring Maker</B></TD>
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
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Template Maker</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%">
				<TR>
					<TD vAlign="top" align="center" colSpan="2">
						<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Template&nbsp;ID</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_TPLID" runat="server" Width="152px" CssClass="mandatory2" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Template&nbsp;Desc</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_TPLDESC" runat="server" Width="336px" CssClass="mandatory2"
										MaxLength="50"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE_VALUE" Width="70px" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_VALUE_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL_VALUE" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_VALUE_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Existing Data</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_EXISTING_VALUE" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="SCOTPLID" HeaderText="Template ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SCOTPLDESC" HeaderText="Template Desc">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit3" runat="server" CommandName="Edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete3" runat="server" CommandName="Delete">Delete</asp:LinkButton>
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
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_REQUEST_VALUE" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="SCOTPLID" HeaderText="Template ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SCOTPLDESC" HeaderText="Template Desc">
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
					<TD vAlign="top" align="center" colSpan="2">
						<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Template&nbsp;ID</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_TPLID" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_TPLID_SelectedIndexChanged"></asp:dropdownlist><asp:label id="LBL_SEQ" runat="server" Visible="False"></asp:label><asp:label id="LBL_SAVEMODE2" runat="server" Visible="False">1</asp:label><asp:label id="LBL_SAVEMODE3" runat="server" Visible="False">1</asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px; HEIGHT: 34px">Parameter Location</TD>
								<TD style="WIDTH: 7px; HEIGHT: 34px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 34px"><asp:radiobuttonlist id="RBL_PARAM_LOC" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="RBL_PARAM_LOC_SelectedIndexChanged">
										<asp:ListItem Value="0">CUBES</asp:ListItem>
										<asp:ListItem Value="1">Prompt</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px; HEIGHT: 34px">Parameter</TD>
								<TD style="WIDTH: 7px; HEIGHT: 34px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 34px"><asp:radiobuttonlist id="RBL_PARAM" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="RBL_PARAM_SelectedIndexChanged">
										<asp:ListItem Value="0">Param List</asp:ListItem>
										<asp:ListItem Value="1">Param List Other</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Parameter ID</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PARAMID" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_PARAMID_SelectedIndexChanged"></asp:dropdownlist><asp:label id="LBL_MIN" runat="server" Visible="False"></asp:label><asp:label id="LBL_MAX" runat="server" Visible="False"></asp:label><asp:label id="LBL_PRMVALNAME" runat="server" Visible="False"></asp:label></TD>
							</TR>
						</TABLE>
						<TABLE id="TR_DDL_VALUE" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px"><asp:label id="LBL_VALUE" runat="server">Value</asp:label></TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:dropdownlist id="DDL_VALUE" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px"><asp:label id="LBL_PARAMVALID" runat="server">Parameter Value ID</asp:label></TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_PARAM_VALUE_ID" runat="server" Width="136px" CssClass="mandatory" MaxLength="30"></asp:textbox></TD>
							</TR>
						</TABLE>
						<TABLE id="TR_MIN_VALUE" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px"><asp:label id="LBL_MIN_VALUE" runat="server">Minimum Value</asp:label></TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly3()" id="TXT_MIN_VALUE" runat="server" Width="136px"
										MaxLength="30"></asp:textbox></TD>
							</TR>
						</TABLE>
						<TABLE id="TR_MAX_VALUE" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px"><asp:label id="LBL_MAX_VALUE" runat="server">Maximum Value</asp:label></TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly3()" id="TXT_MAX_VALUE" runat="server" Width="136px"
										MaxLength="30"></asp:textbox></TD>
							</TR>
						</TABLE>
						<TABLE id="TR_PRM_SCORE" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px"><asp:label id="LBL_PARAM_SCORE" runat="server">Parameter  Score</asp:label></TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_PARAM_SCORE" runat="server" Width="136px"
										MaxLength="15"></asp:textbox><asp:label id="LBL_PARAM_VALUE_ID" runat="server" Visible="False"></asp:label></TD>
							</TR>
						</TABLE>
						<TABLE id="TR_REMARKS" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px"><asp:label id="LBL_REMARKS" runat="server">Value Description</asp:label></TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_REMARKS" runat="server" Width="136px" MaxLength="30"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE_LIST" Width="70px" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_LIST_Click"></asp:button>&nbsp;
						<asp:button id="Button2" CssClass="button1" Text="Cancel" Runat="server" onclick="Button2_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Existing Data</TD>
				</TR>
				<TR id="tr_grd_value_exist" runat="server">
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="Datagrid1" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="SCOTPLID" HeaderText="Template ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_ID" HeaderText="ID" Visible="False">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_VALUE_ID" HeaderText="Parameter Value ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_VALUE_NAME" HeaderText="Parameter Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
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
								<asp:BoundColumn DataField="REMARKS" HeaderText="Value Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="Linkbutton1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
										<asp:Label id="lbl_statusparam" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR id="tr_grd_other_exitst" runat="server">
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="Datagrid4" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="SCOTPLID" HeaderText="Template ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_ID" HeaderText="ID" Visible="False">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_OTHER_ID" HeaderText="Parameter Other ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_OTHER_NAME" HeaderText="Parameter Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_OTHER_VALUE" HeaderText="Parameter Score">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="Linkbutton7" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="Linkbutton8" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="Datagrid2" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="SCOTPLID" HeaderText="Template ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_ID" HeaderText="ID" Visible="False">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_VALUE_ID" HeaderText="Parameter Value ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_VALUE_NAME" HeaderText="Parameter Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
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
								<asp:BoundColumn Visible="False" DataField="CH_STA" HeaderText="Status ID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="SEQ_ID" HeaderText="Seq"></asp:BoundColumn>
								<asp:BoundColumn DataField="REMARKS" HeaderText="Value Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="Linkbutton3" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="Linkbutton4" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR id="tr_grd_other" runat="server">
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="Datagrid3" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="SCOTPLID" HeaderText="Template ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_ID" HeaderText="ID" Visible="False">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_OTHER_ID" HeaderText="Parameter Other ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_OTHER_NAME" HeaderText="Parameter Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PARAM_OTHER_VALUE" HeaderText="Parameter Score">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA" HeaderText="Status ID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="SEQ_ID" HeaderText="Seq"></asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="Linkbutton5" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="Linkbutton6" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
