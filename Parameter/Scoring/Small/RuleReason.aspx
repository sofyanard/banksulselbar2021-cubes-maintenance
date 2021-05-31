<%@ Page language="c#" Codebehind="RuleReason.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Small.RuleReason" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SearchParameter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table4" width="100%" border="0">
					<TR>
						<TD class="tdNoBorder" style="WIDTH: 929px" align="left" width="929">
							<TABLE id="Table3">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Attribute</B></TD>
								</TR>
							</TABLE>
						</TD>
						<td align="right"><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A><A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A></td>
					</TR>
					<TR>
						<TD align="center" colSpan="2">
							<TABLE class="td" id="Table1" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1"
								cellPadding="1" width="590" border="1">
								<TR>
									<TD class="tdHeader1">Search Criteria</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center">
										<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="TDBGColor1" width="170">ID</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_TxtIDParam" runat="server" MaxLength="20"
														Width="200px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1">Rule Name</TD>
												<TD></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="_TxtRuleName" runat="server" MaxLength="50"
														Width="200px"></asp:textbox></TD>
											</TR>
											<tr>
												<TD></TD>
											</tr>
											<TR>
												<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnFind" runat="server" Width="180px" Text="Find Rule" CssClass="button1" onclick="_btnFind_Click"></asp:button>&nbsp;
													<asp:button id="_btnNew" runat="server" Width="180px" Text="New Rule" CssClass="button1" onclick="_btnNew_Click"></asp:button></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2">&nbsp;</TD>
					</TR>
					<TR>
						<TD colSpan="2"><ASP:DATAGRID id="DatGrd" runat="server" Width="100%" CellPadding="1" AutoGenerateColumns="False"
								AllowPaging="True" PageSize="15">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="ID" HeaderText="ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESCRIPT" HeaderText="Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="QUERYTXT" HeaderText="Query">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="COLUMNNAME" HeaderText="Column">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PARAMNAME" HeaderText="Parameter">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISRANGE" HeaderText="Isrange">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" HeaderText="Function" CommandName="Edit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
					<TR id="TR_EDIT_PARAMETER" runat="server">
						<TD align="center" colSpan="2">
							<TABLE class="td" id="TableEdit1" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1"
								cellPadding="1" width="590" border="1">
								<TR>
									<TD class="tdHeader1">Edit Attribute</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center">
										<TABLE id="TableEdit2" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="TDBGColor1" width="170">ID :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtEditedID" runat="server" MaxLength="20"
														Width="304px" Enabled="False" BackColor="Silver"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">DESCRIPTION :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtEditedDesc" runat="server" MaxLength="200" Width="304px" Height="58px" TextMode="MultiLine"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">QUERY :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtEditedQuery" runat="server" MaxLength="200" Width="304px" Height="58px"
														TextMode="MultiLine"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">PARAMETER&nbsp;:</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtEditedParameter" runat="server" Width="304px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">COLUMN&nbsp;:</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtEditedColumn" runat="server" Width="304px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1">TYPE :</TD>
												<TD></TD>
												<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdoEditedType" runat="server" Width="150px" RepeatDirection="Horizontal" AutoPostBack="True">
														<asp:ListItem Value="1" Selected="True">Range</asp:ListItem>
														<asp:ListItem Value="0">Non Range</asp:ListItem>
													</asp:radiobuttonlist></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1">STATUS :</TD>
												<TD></TD>
												<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdoEditedStatus" runat="server" Width="150px" RepeatDirection="Horizontal"
														AutoPostBack="True">
														<asp:ListItem Value="1" Selected="True">Enable</asp:ListItem>
														<asp:ListItem Value="0">Disable</asp:ListItem>
													</asp:radiobuttonlist></TD>
											</TR>
											<tr>
												<TD></TD>
											</tr>
											<TR>
												<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnUpdateRule" runat="server" Width="180px" Text="Update Rule" CssClass="button1" onclick="_btnUpdateRule_Click"></asp:button>&nbsp;
													<asp:button id="_btnViewDetail" runat="server" Width="180px" Text="View Detail" CssClass="button1" onclick="_btnViewDetail_Click"></asp:button></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR id="TR_NEW_PARAMETER" runat="server">
						<TD align="center" colSpan="2">
							<TABLE class="td" id="TableNew1" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1"
								cellPadding="1" width="590" border="1">
								<TR>
									<TD class="tdHeader1">Insert New Attribute</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center">
										<TABLE id="TableEdit3" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="TDBGColor1" width="170">DESCRIPTION :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtNewDesc" runat="server" MaxLength="200" Width="304px" Height="58px" TextMode="MultiLine"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">QUERY :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtNewQuery" runat="server" MaxLength="200" Width="304px" Height="58px" TextMode="MultiLine"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">PARAMETER&nbsp;:</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtNewParameter" runat="server" Width="304px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">COLUMN&nbsp;:</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtNewColumn" runat="server" Width="304px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1">TYPE :</TD>
												<TD></TD>
												<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdoNewType" runat="server" Width="150px" RepeatDirection="Horizontal" AutoPostBack="True">
														<asp:ListItem Value="1" Selected="True">Range</asp:ListItem>
														<asp:ListItem Value="0">Non Range</asp:ListItem>
													</asp:radiobuttonlist></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1">Status :</TD>
												<TD></TD>
												<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdoNewStatus" runat="server" Width="150px" RepeatDirection="Horizontal" AutoPostBack="True">
														<asp:ListItem Value="1" Selected="True">Enable</asp:ListItem>
														<asp:ListItem Value="0">Disable</asp:ListItem>
													</asp:radiobuttonlist></TD>
											</TR>
											<tr>
												<TD></TD>
											</tr>
											<TR>
												<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnInsertRule" runat="server" Width="180px" Text="Insert Rule" CssClass="button1" onclick="_btnInsertRule_Click"></asp:button>&nbsp;
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
					</TR>
					<TR id="TR_ATTRIBUTE_RANGE" runat="server">
						<TD align="center" colSpan="2">
							<TABLE width="100%" border="0">
								<TR>
									<TD class="tdHeader1">Attribute Range</TD>
								</TR>
								<tr>
									<td><ASP:DATAGRID id="DatGridAttributeRange" runat="server" Width="100%" CellPadding="1" AutoGenerateColumns="False"
											AllowPaging="True">
											<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
												<asp:BoundColumn DataField="ID" HeaderText="ID">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="LOWESTSCORE" HeaderText="Lowest">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="HIGHESTSCORE" HeaderText="Highest">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="WEIGHT" HeaderText="Weight">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:ButtonColumn Text="Edit" HeaderText="Function" CommandName="Edit">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:ButtonColumn>
												<asp:ButtonColumn Text="Delete" HeaderText="Function" CommandName="Edits">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:ButtonColumn>
											</Columns>
											<PagerStyle Mode="NumericPages"></PagerStyle>
										</ASP:DATAGRID></td>
								</tr>
								<TR id="TR_EDIT_ATTRIBUTE_RANGE" runat="server">
									<TD align="center" colSpan="2">
										<TABLE class="td" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1" cellPadding="1"
											width="590" border="1">
											<TR>
												<TD class="tdHeader1">Edit Attibute Range</TD>
											</TR>
											<TR>
												<TD vAlign="top" align="center">
													<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD class="TDBGColor1" width="170">ID :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtEditedAttributeRangeID" runat="server"
																	MaxLength="20" Width="280px" Enabled="False" BackColor="Silver"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="TDBGColor1" width="170">LOWEST :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtEditedAttributeRangeLowest" runat="server"
																	MaxLength="200" Width="280px"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="TDBGColor1" width="170">HIGHEST :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtEditedAttributeRangeHighest" runat="server"
																	MaxLength="200" Width="280px"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="TDBGColor1" width="170">WEIGHT :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtEditedAttributeRangeWeight" runat="server"
																	MaxLength="200" Width="280px"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="TDBGColor1" width="170">CONDITION :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdEditedStatus" runat="server" Width="352px" RepeatDirection="Horizontal" AutoPostBack="True">
																	<asp:ListItem Value="3" Selected="True">BELOW</asp:ListItem>
																	<asp:ListItem Value="2">HIGH</asp:ListItem>
																	<asp:ListItem Value="1">NO INFORMATION</asp:ListItem>
																	<asp:ListItem Value="0">NORMAL</asp:ListItem>
																</asp:radiobuttonlist></TD>
														</TR>
														<tr>
															<TD></TD>
														</tr>
														<TR>
															<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnEditedAttributeRangeWeight" runat="server" Width="180px" Text="Update Attribute"
																	CssClass="button1" onclick="_btnEditedAttributeRangeWeight_Click"></asp:button>&nbsp;
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR id="TR_NEW_ATTRIBUTE_RANGE" runat="server">
									<TD align="center" colSpan="2">
										<TABLE class="td" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1" cellPadding="1"
											width="590" border="1">
											<TR>
												<TD class="tdHeader1">New Attibute Range</TD>
											</TR>
											<TR>
												<TD vAlign="top" align="center">
													<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD class="TDBGColor1" width="170">LOWEST :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtNewRangeLowest" runat="server" MaxLength="200"
																	Width="280px"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="TDBGColor1" width="170">HIGHEST :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtNewRangeHighest" runat="server" MaxLength="200"
																	Width="280px"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="TDBGColor1" width="170">WEIGHT :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtNewRangeWeight" runat="server" MaxLength="200"
																	Width="280px"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="TDBGColor1" width="170">CONDITION :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdNewStatus" runat="server" Width="352px" RepeatDirection="Horizontal" AutoPostBack="True">
																	<asp:ListItem Value="3" Selected="True">BELOW</asp:ListItem>
																	<asp:ListItem Value="2">HIGH</asp:ListItem>
																	<asp:ListItem Value="1">NO INFORMATION</asp:ListItem>
																	<asp:ListItem Value="0">NORMAL</asp:ListItem>
																</asp:radiobuttonlist></TD>
														</TR>
														<tr>
															<TD></TD>
														</tr>
														<TR>
															<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnUpdateAttributeRange" runat="server" Width="180px" Text="Insert Attribute"
																	CssClass="button1" onclick="_btnUpdateAttributeRange_Click"></asp:button>&nbsp;
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
					</TR>
					<TR id="TR_ATTRIBUTE_NONRANGE" runat="server">
						<TD align="center" colSpan="2">
							<TABLE width="100%" border="0">
								<TR>
									<TD class="tdHeader1">Attribute Non Range</TD>
								</TR>
								<tr>
									<td><ASP:DATAGRID id="DatGridAttributeNonRange" runat="server" Width="100%" CellPadding="1" AutoGenerateColumns="False"
											AllowPaging="True">
											<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
												<asp:BoundColumn DataField="ID" HeaderText="ID Attribute">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="QUERYTXT" HeaderText="Description">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="40%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="VALUE" HeaderText="Value">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="WEIGHT" HeaderText="Weight">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:ButtonColumn Text="Edit" HeaderText="Function" CommandName="Edit">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:ButtonColumn>
												<asp:ButtonColumn Text="Delete" HeaderText="Function" CommandName="Delete">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:ButtonColumn>
											</Columns>
											<PagerStyle Mode="NumericPages"></PagerStyle>
										</ASP:DATAGRID></td>
								</tr>
								<TR id="TR_EDIT_ATTRIBUTE_NONRANGE" runat="server">
									<TD align="center" colSpan="2">
										<TABLE class="td" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1" cellPadding="1"
											width="590" border="1">
											<TR>
												<TD class="tdHeader1">Edit Attribute Non Range</TD>
											</TR>
											<TR>
												<TD vAlign="top" align="center">
													<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD class="TDBGColor1" width="170">ID :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtEditedAttributeNonRangeID" runat="server"
																	MaxLength="20" Width="280px" Enabled="False" BackColor="Silver"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="TDBGColor1" width="170">DESCRIPTION :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox id="_txtEditedAttributeNonRangeDesc" runat="server" MaxLength="200" Width="280px"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="TDBGColor1" width="170">VALUE :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtEditedAttributeNonRangeValue" runat="server"
																	MaxLength="200" Width="280px"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="TDBGColor1" width="170">WEIGHT :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtEditedAttributeNonRangeWeight" runat="server"
																	MaxLength="200" Width="280px"></asp:textbox></TD>
														</TR>
														<tr>
															<TD></TD>
														</tr>
														<TR>
															<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnEditedAttributeNonRange" runat="server" Width="180px" Text="Update Attribute"
																	CssClass="button1" onclick="_btnEditedAttributeNonRange_Click"></asp:button>&nbsp;
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR id="TR_NEW_ATTRIBUTE_NONRANGE" runat="server">
									<TD align="center" colSpan="2">
										<TABLE class="td" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1" cellPadding="1"
											width="590" border="1">
											<TR>
												<TD class="tdHeader1">New Attribute Non Range</TD>
											</TR>
											<TR>
												<TD vAlign="top" align="center">
													<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD class="TDBGColor1" width="170">DESCRIPTION :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox id="_txtNewAttributeNonRangeDesc" runat="server" MaxLength="200" Width="280px"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="TDBGColor1" width="170">VALUE :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtNewAttributeNonRangeValue" runat="server"
																	MaxLength="200" Width="280px"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="TDBGColor1" width="170">WEIGHT :</TD>
															<TD width="5"></TD>
															<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtNewAttributeNonRangeWeight" runat="server"
																	MaxLength="200" Width="280px"></asp:textbox></TD>
														</TR>
														<tr>
															<TD></TD>
														</tr>
														<TR>
															<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnNewAttributeNonRange" runat="server" Width="180px" Text="Insert Attribute"
																	CssClass="button1" onclick="_btnNewAttributeNonRange_Click"></asp:button>&nbsp;
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<asp:textbox id="_txtIDStatus" runat="server" MaxLength="200" Width="280px " Visible="False"></asp:textbox></TABLE>
				<table class="td" width="100%" border="1">
					<TR>
						<TD class="tdHeader1" colSpan="3">Maker Request</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="3">Attribute</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" colSpan="3"><asp:datagrid id="DatGridAttributeReq" runat="server" Width="100%" AutoGenerateColumns="False"
								AllowPaging="True" PageSize="5">
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="DESCRIPT" HeaderText="Deskripsi">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" HeaderText="Function" CommandName="Edit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Delete" HeaderText="Function" CommandName="Delete">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR id="TR_ATTRIBUTE_TEMP" runat="server">
						<TD align="center" colSpan="2">
							<TABLE class="td" id="TableEdit1" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1"
								cellPadding="1" width="590" border="1">
								<TR>
									<TD class="tdHeader1">Edit Attribute</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center">
										<TABLE id="TableEdit2" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="TDBGColor1" width="170">ID :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtIDAttTemp" runat="server" MaxLength="20"
														Width="304px" Enabled="False" BackColor="Silver"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">DESCRIPTION :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtDescAttTemp" runat="server" MaxLength="200" Width="304px" Height="58px"
														TextMode="MultiLine"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">QUERY :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtQueryAttTemp" runat="server" MaxLength="200" Width="304px" Height="58px"
														TextMode="MultiLine"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">PARAMETER&nbsp;:</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtParameterAttTemp" runat="server" Width="304px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">COLUMN&nbsp;:</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtColumnAttTemp" runat="server" Width="304px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1">TYPE :</TD>
												<TD></TD>
												<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdoTypeAttTemp" runat="server" Width="150px" RepeatDirection="Horizontal" AutoPostBack="True">
														<asp:ListItem Value="1" Selected="True">Range</asp:ListItem>
														<asp:ListItem Value="0">Non Range</asp:ListItem>
													</asp:radiobuttonlist></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1">STATUS :</TD>
												<TD></TD>
												<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdoStatusAttTemp" runat="server" Width="150px" RepeatDirection="Horizontal"
														AutoPostBack="True">
														<asp:ListItem Value="1" Selected="True">Enable</asp:ListItem>
														<asp:ListItem Value="0">Disable</asp:ListItem>
													</asp:radiobuttonlist></TD>
											</TR>
											<tr>
												<TD></TD>
											</tr>
											<TR>
												<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnUpdateAttribute" runat="server" Width="180px" Text="Update Attribute" CssClass="button1" onclick="_btnUpdateAttribute_Click"></asp:button>&nbsp;</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="3">Attribute Item Range</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" colSpan="3"><asp:datagrid id="DatGridAttRangeReq" runat="server" Width="100%" AutoGenerateColumns="False"
								AllowPaging="True" PageSize="5">
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="DESCRIPT" HeaderText="Deskripsi">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LOWESTSCORE" HeaderText="Lowest Score">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="HIGHESTSCORE" HeaderText="Highest Score">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="WEIGHT" HeaderText="Weight">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" HeaderText="Function" CommandName="Edit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Delete" HeaderText="Function" CommandName="Delete">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR id="TR_ATTRANGE_TEMP" runat="server">
						<TD align="center" colSpan="2">
							<TABLE class="td" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1" cellPadding="1"
								width="590" border="1">
								<TR>
									<TD class="tdHeader1">Edit Attibute Range</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center">
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="TDBGColor1" width="170">ID :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtIdAttRangeTemp" runat="server" MaxLength="20"
														Width="280px" Enabled="False" BackColor="Silver"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">LOWEST :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtLowestAttRangeTemp" runat="server" MaxLength="200"
														Width="280px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">HIGHEST :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtHighestAttRangeTemp" runat="server" MaxLength="200"
														Width="280px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">WEIGHT :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtWeightAttRangeTemp" runat="server" MaxLength="200"
														Width="280px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">CONDITION :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdoConditionAttRangeTemp" runat="server" Width="352px" RepeatDirection="Horizontal"
														AutoPostBack="True">
														<asp:ListItem Value="3" Selected="True">BELOW</asp:ListItem>
														<asp:ListItem Value="2">HIGH</asp:ListItem>
														<asp:ListItem Value="1">NO INFORMATION</asp:ListItem>
														<asp:ListItem Value="0">NORMAL</asp:ListItem>
													</asp:radiobuttonlist></TD>
											</TR>
											<tr>
												<TD></TD>
											</tr>
											<TR>
												<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnUpdateAttRangeTemp" runat="server" Width="180px" Text="Update Attribute"
														CssClass="button1" onclick="_btnUpdateAttRangeTemp_Click"></asp:button>&nbsp;
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="3">Attribute Item Non Range</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" colSpan="3"><asp:datagrid id="DatGridAttNonRangeReq" runat="server" Width="100%" AutoGenerateColumns="False"
								AllowPaging="True" PageSize="5">
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="DESCRIPT" HeaderText="Deskripsi">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="QUERYTXT" HeaderText="Attribute">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="WEIGHT" HeaderText="Weight">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" HeaderText="Function" CommandName="Edit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Delete" HeaderText="Function" CommandName="Delete">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR id="TR_ATTNONRANGE_TEMP" runat="server">
						<TD align="center" colSpan="2">
							<TABLE class="td" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1" cellPadding="1"
								width="590" border="1">
								<TR>
									<TD class="tdHeader1">Edit Attribute Non Range</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center">
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="TDBGColor1" width="170">ID :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtIdAttNonRangeTemp" runat="server" MaxLength="20"
														Width="280px" Enabled="False" BackColor="Silver"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">DESCRIPTION :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtDescAttNonRangeTemp" runat="server" MaxLength="200" Width="280px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">VALUE :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtValueAttNonRangeTemp" runat="server" MaxLength="200"
														Width="280px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" width="170">WEIGHT :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtWeightAttNonRangeTemp" runat="server" MaxLength="200"
														Width="280px"></asp:textbox></TD>
											</TR>
											<tr>
												<TD></TD>
											</tr>
											<TR>
												<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnUpdateAttNonRangeTemp" runat="server" Width="180px" Text="Update Attribute"
														CssClass="button1" onclick="_btnUpdateAttNonRangeTemp_Click"></asp:button>&nbsp;
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</table>
				<asp:textbox id="_txtIDAttNonRange" runat="server" MaxLength="200" Width="280px" Visible="False"></asp:textbox><asp:textbox id="_txtIDAttRange" runat="server" MaxLength="200" Width="280px" Visible="False"></asp:textbox><asp:textbox id="_txtIDAttribute" runat="server" MaxLength="200" Width="280px" Visible="False"></asp:textbox><asp:textbox id="_txtIDAttributeTemp" runat="server" MaxLength="200" Width="280px" Visible="False"></asp:textbox><asp:textbox id="_txtIDAttributeRangeTemp" runat="server" MaxLength="200" Width="280px" Visible="False"></asp:textbox><asp:textbox id="_txtIDAttributeNonRangeTemp" runat="server" MaxLength="200" Width="280px" Visible="False"></asp:textbox></center>
		</form>
	</body>
</HTML>
