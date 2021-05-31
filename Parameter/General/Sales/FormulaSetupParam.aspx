<%@ Page language="c#" Codebehind="FormulaSetupParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Sales.FormulaSetupParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FormulaSetupParam</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!-- #include file="../../../include/cek_mandatory.html" --><LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="Table2">
							</TABLE>
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD style="HEIGHT: 41px" width="400">
										<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
											border="0">
											<TR>
												<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Maintenance : 
														Sales Commission</B></TD>
											</TR>
										</TABLE>
									</TD>
									<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
										<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							Parameter&nbsp;Formula Setup Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 147px; HEIGHT: 20px" width="147">Number</TD>
									<TD style="WIDTH: 11px; HEIGHT: 20px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox id="TXT_NUM" runat="server" Width="152px" MaxLength="10"></asp:textbox>&nbsp;&nbsp;
										<asp:button id="BTN_KURANG1" runat="server" Width="20px" Text="-" ForeColor="Red" BackColor="White"
											BorderStyle="Groove" onclick="BTN_KURANG1_Click"></asp:button>&nbsp;
										<asp:button id="BTN_TAMBAH1" runat="server" Width="20px" Text="+" ForeColor="Red" BackColor="White"
											BorderStyle="Groove" onclick="BTN_TAMBAH1_Click"></asp:button></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 147px; HEIGHT: 25px">Calculation&nbsp; Type</TD>
									<TD style="WIDTH: 11px; HEIGHT: 25px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 25px"><asp:dropdownlist id="DDL_CALC_TYPE" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 147px; HEIGHT: 22px">Agent Type</TD>
									<TD style="WIDTH: 11px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:dropdownlist id="DDL_AGENT_TYPE" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 147px; HEIGHT: 29px">Calculation Name</TD>
									<TD style="WIDTH: 11px; HEIGHT: 29px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 29px"><asp:textbox id="TXT_CALC_NAME" runat="server" Width="432px" MaxLength="40"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 147px; HEIGHT: 84px" colSpan="1">
										<P>Calculation Formula</P>
									</TD>
									<TD style="WIDTH: 11px; HEIGHT: 84px">:</TD>
									<TD style="HEIGHT: 84px">
										<P>
											<asp:Label id="Label1" runat="server" Font-Bold="True" Visible="False">Select Operator :</asp:Label>&nbsp;
											<asp:button id="BTN_KALI2" runat="server" Width="20px" Text="*" ForeColor="Red" BackColor="White"
												BorderStyle="Groove" onclick="BTN_KALI2_Click"></asp:button>&nbsp;
											<asp:button id="BTN_BAGI2" runat="server" Width="20px" Text="/" ForeColor="Red" BackColor="White"
												BorderStyle="Groove" onclick="BTN_BAGI2_Click"></asp:button>&nbsp;
											<asp:button id="BTN_TAMBAH2" runat="server" Width="20px" Text="+" ForeColor="Red" BackColor="White"
												BorderStyle="Groove" onclick="BTN_TAMBAH2_Click"></asp:button>&nbsp;
											<asp:button id="BTN_KURANG2" runat="server" Width="20px" Text="-" ForeColor="Red" BackColor="White"
												BorderStyle="Groove" onclick="BTN_KURANG2_Click"></asp:button>&nbsp;
											<asp:button id="BTN_KURBUKA" runat="server" Width="20px" Text="(" ForeColor="Red" BackColor="White"
												BorderStyle="Groove" onclick="BTN_KURBUKA_Click"></asp:button>&nbsp;
											<asp:button id="BTN_KURTUTUP" runat="server" Width="20px" Text=")" ForeColor="Red" BackColor="White"
												BorderStyle="Groove" onclick="BTN_KURTUTUP_Click"></asp:button>&nbsp;&nbsp;&nbsp;
											<asp:button id="Button9" Text="Insert Value" ForeColor="MidnightBlue" BackColor="#C0FFFF" Runat="server"
												Font-Names="Arial" Font-Bold="True" BorderColor="Navy" onclick="Button9_Click"></asp:button></P>
										<P><asp:textbox id="TXT_CALC_FORMULA" runat="server" Width="100%" TextMode="MultiLine" Height="65px"></asp:textbox></P>
									</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 147px; HEIGHT: 22px">Calculation Table</TD>
									<TD style="WIDTH: 11px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox id="TXT_CALC_TABLE" runat="server" Width="100%" TextMode="MultiLine" Height="65px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 147px; HEIGHT: 22px">Calculation Link</TD>
									<TD style="WIDTH: 11px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox id="TXT_CALC_LINK" runat="server" Width="100%" TextMode="MultiLine" Height="65px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 147px; HEIGHT: 22px">Calculation Group</TD>
									<TD style="WIDTH: 11px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox id="TXT_CALC_GROUP" runat="server" Width="100%" TextMode="MultiLine" Height="65px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 147px; HEIGHT: 22px">Result Type</TD>
									<TD style="WIDTH: 11px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:radiobuttonlist id="RBL_RESULT_TYPE" runat="server" Width="232px" RepeatDirection="Horizontal">
											<asp:ListItem Value="1">Point</asp:ListItem>
											<asp:ListItem Value="0">Value</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 147px; HEIGHT: 22px">Active</TD>
									<TD style="WIDTH: 11px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:radiobuttonlist id="RBL_ACTIVE" runat="server" Width="272px" RepeatDirection="Horizontal">
											<asp:ListItem Value="1">Active</asp:ListItem>
											<asp:ListItem Value="0">Non Active</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
							</TABLE>
							<asp:label id="LBL_JENIS" runat="server" Visible="False"></asp:label><asp:label id="LBL_SEQ_NO" runat="server" Visible="False"></asp:label><asp:label id="LBL_SEQ_ID" runat="server" Visible="False"></asp:label><asp:label id="LBL_SAVE_MODE" runat="server" Visible="False">1</asp:label></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_NEW" Text="New" CssClass="button1" Runat="server" onclick="BTN_NEW_Click"></asp:button>&nbsp;
							<asp:button id="BTN_SAVE" Text="Save" CssClass="button1" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" Text="Reset" CssClass="button1" Runat="server" onclick="BTN_CANCEL_Click"></asp:button>&nbsp;
							<asp:button id="BTN_TEST" Text="Formula Test" CssClass="button1" Runat="server" onclick="BTN_TEST_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							<P>Existing Data</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SEQ_NO" HeaderText="No">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CALCULATION_DESC" HeaderText="Calculation Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FORMULA_NAME" HeaderText="Calculation Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Result_Desc" HeaderText="Limit Result Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Formula_Desc" HeaderText="Limit Active">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete1" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="CALCULATION_ID" HeaderText="CALCULATION_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AGENTYPE_ID" HeaderText="AGENTYPE_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FORMULA_ACTIVE" HeaderText="FORMULA_ACTIVE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="RESULT_TYPE" HeaderText="RESULT_TYPE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FORMULA" HeaderText="FORMULA"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FORMULA_TABLE" HeaderText="FORMULA_TABLE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FORMULA_LINK" HeaderText="FORMULA_LINK"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FORMULA_GROUP" HeaderText="FORMULA_GROUP"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Maker Request</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="SEQ_ID" HeaderText="SEQ_ID"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CALCULATION_DESC" HeaderText="Calculation Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FORMULA_NAME" HeaderText="Calculation Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Result_Desc" HeaderText="Limit Result Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Formula_Desc" HeaderText="Limit Active">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS1" HeaderText="Status">
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
									<asp:BoundColumn Visible="False" DataField="AGENTYPE_ID" HeaderText="AGENTYPE_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FORMULA_ACTIVE" HeaderText="FORMULA_ACTIVE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="RESULT_TYPE" HeaderText="RESULT_TYPE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SEQ_NO" HeaderText="SEQ_NO"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CALCULATION_ID" HeaderText="CALCULATION_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="STATUS" HeaderText="STATUS"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2">&nbsp;</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
