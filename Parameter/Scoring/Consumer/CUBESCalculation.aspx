<%@ Page language="c#" Codebehind="CUBESCalculation.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Consumer.CUBESCalculation" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CUBES Calculation</title>
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
													Scoring Maker</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="tdNoBorder" align="right">
									<A href="../../ScoringParamAll.aspx?mc=9902040301&amp;moduleID=40"><IMG src="../../../Image/Back.jpg" border="0"></A>
									<A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A> <A href="../../../Logout.aspx" target="_top">
										<IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;CUBES Calculation&nbsp;Maker</TD>
				</TR>
				<TR>
					<TD class="TDSmallHeader" colSpan="2">Parameter List</TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px">ID</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue">
									<asp:textbox id="TXT_ID1" onkeypress="return kutip_satu()" runat="server" Width="72px" CssClass="mandatory"
										MaxLength="3"></asp:textbox>
									<asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label>
									<asp:label id="LBL_ID" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter Name</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px">
									<asp:textbox id="TXT_PARAM_NAME" onkeypress="return kutip_satu()" runat="server" Width="400px"
										CssClass="mandatory" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter Formula</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px">
									<asp:textbox id="TXT_PARAM_FORMULA" runat="server" Width="100%" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter Field</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px">
									<asp:textbox id="TXT_PARAM_FIELD" onkeypress="return kutip_satu()" runat="server" Width="336px"
										MaxLength="100"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter table</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px">
									<asp:textbox id="TXT_PARAM_TABLE" onkeypress="return kutip_satu()" runat="server" Width="336px"
										MaxLength="100"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter table Child</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px">
									<asp:textbox id="TXT_PARAM_TABLE_CHILD" onkeypress="return kutip_satu()" runat="server" Width="208px"
										MaxLength="100"></asp:textbox>
									<asp:label id="Label1" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_PRM" runat="server" Visible="False"></asp:label>
									<asp:label id="Label5" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter PRM</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px">
									<asp:radiobuttonlist id="RBL_PARAM_PRM" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" onselectedindexchanged="RBL_PARAM_PRM_SelectedIndexChanged">
										<asp:ListItem Value="0">CUBES</asp:ListItem>
										<asp:ListItem Value="1">PROMPT</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter Active</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px">
									<asp:radiobuttonlist id="RBL_PARAM_ACTIVE" runat="server" Width="160px" RepeatDirection="Horizontal">
										<asp:ListItem Value="1">Active</asp:ListItem>
										<asp:ListItem Value="0">Non Active</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 20px">Parameter Link</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px">
									<asp:textbox id="TXT_PARAM_LINK" onkeypress="return kutip_satu()" runat="server" Width="100%"
										TextMode="MultiLine"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2">
						<asp:button id="BTN_SAVE_LIST" CssClass="button1" Runat="server" Text="Save" Width="70px" onclick="BTN_SAVE_LIST_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL_LIST" CssClass="button1" Runat="server" Text="Cancel" onclick="BTN_CANCEL_LIST_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="tdSmallHeader" colSpan="2">
						<P>Current Parameter List Data</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2">
						<asp:datagrid id="DGR_EXISTING_LIST" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn>
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
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
					<TD class="tdSmallHeader" colSpan="2">Maker Parameter List Request</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_REQUEST_LIST" runat="server" Width="100%" AllowPaging="True" PageSize="5"
							AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:TemplateColumn Visible="False">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemTemplate>
										<asp:RadioButton id="RD_CHECK2" runat="server"></asp:RadioButton>
									</ItemTemplate>
								</asp:TemplateColumn>
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
			<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%">
				<TR>
					<TD class="tdSmallHeader" colSpan="2">PARAMETER VALUE</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="2">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Program Type</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PROGRAM_TYPE" runat="server" CssClass="mandatory2" AutoPostBack="True" onselectedindexchanged="DDL_PROGRAM_TYPE_SelectedIndexChanged"></asp:dropdownlist><asp:label id="Label2" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px; HEIGHT: 20px">Product&nbsp; Type</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:dropdownlist id="DDL_PRODUCT_TYPE" runat="server" CssClass="mandatory2"></asp:dropdownlist><asp:label id="Label3" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px; HEIGHT: 20px">Employee Type</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px"><asp:dropdownlist id="DDL_EMPLOYEE_TYPE" runat="server" CssClass="mandatory2"></asp:dropdownlist><asp:label id="Label4" runat="server" Visible="False"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BtnViewValue" Text="View Parameter Value" Runat="server" CssClass="button1" onclick="BtnViewValue_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="tdSmallHeader" colSpan="2">Parameter Value</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="2">
						<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Parameter Value ID</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_ID2" onkeypress="return numbersonly()" runat="server" Width="56px" CssClass="mandatory2"
										MaxLength="10"></asp:textbox>
									<asp:label id="LBL_SEQ" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_SAVEMODE2" runat="server" Visible="False">1</asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Parameter Name</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_PARAM_NAME2" onkeypress="return kutip_satu()" runat="server" Width="336px"
										CssClass="mandatory2" MaxLength="50"></asp:textbox><asp:label id="LBL_MIN" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_MAX" runat="server" Visible="False"></asp:label></TD>
							</TR>
						</TABLE>
						<TABLE id="TR_DDL_VALUE" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px"><asp:label id="LBL_VALUE" runat="server">Value</asp:label></TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:dropdownlist id="DDL_VALUE" runat="server"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
						<TABLE id="TR_MIN_VALUE" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px"><asp:label id="LBL_MIN_VALUE" runat="server">Minimum Value</asp:label></TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_MIN_VALUE" runat="server" onkeypress="return digitsonly3()" Width="136px"
										MaxLength="30"></asp:textbox></TD>
							</TR>
						</TABLE>
						<TABLE id="TR_MAX_VALUE" cellSpacing="0" cellPadding="0" width="100%" runat="server">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px"><asp:label id="LBL_MAX_VALUE" runat="server">Maximum Value</asp:label></TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_MAX_VALUE" onkeypress="return digitsonly3()" runat="server" Width="136px"
										MaxLength="30"></asp:textbox></TD>
							</TR>
						</TABLE>
						<TABLE id="TR_PRM_SCORE" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px"><asp:label id="LBL_PARAM_SCORE" runat="server">Parameter  Score</asp:label></TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_PARAM_SCORE" onkeypress="return digitsonly()" runat="server" Width="136px"
										MaxLength="15"></asp:textbox><asp:label id="LBL_REQUEST" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_PARAM_VALUE_ID" runat="server" Visible="False"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE_VALUE" Text="Save" Runat="server" CssClass="button1" Width="70px" onclick="BTN_SAVE_VALUE_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL_VALUE" Text="Cancel" Runat="server" CssClass="button1" onclick="BTN_CANCEL_VALUE_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="tdSmallHeader" colSpan="2">Current parameter value Data</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_EXISTING_VALUE" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="PARAM_VALUE_ID" HeaderText="ID">
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
					<TD class="tdSmallHeader" colSpan="2">Maker Parameter Value Request&nbsp;</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_REQUEST_VALUE" runat="server" Width="100%" AllowPaging="True" PageSize="5"
							AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="PARAM_VALUE_ID" HeaderText="ID">
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
			</TABLE>
		</form>
	</body>
</HTML>
