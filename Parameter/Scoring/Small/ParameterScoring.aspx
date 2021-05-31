<%@ Page language="c#" Codebehind="ParameterScoring.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Small.ParameterScoring" %>
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
						<TD class="tdNoBorder" align="left" width="50%">
							<TABLE id="Table3">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Scoring&nbsp;Parameter : 
											Cut Off</B></TD>
								</TR>
							</TABLE>
						</TD>
						<td align="right"><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A><A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A></td>
					</TR>
					<TR>
						<TD align="center" colSpan="2">&nbsp;</TD>
					</TR>
					<tr>
					</tr>
					<TR id="Tr1" runat="server">
						<TD align="center" colSpan="2">
							<TABLE class="td" id="TableEdit1" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="0"
								cellPadding="0" width="590" border="1">
								<TR>
									<TD class="tdHeader1">Item</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center">
										<TABLE id="TableEdit27" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="TDBGColor1" style="HEIGHT: 24px" width="170">Item :</TD>
												<TD style="HEIGHT: 24px" width="5"></TD>
												<TD class="TDBGColorValue" style="HEIGHT: 24px"><asp:dropdownlist id="_ddlItemCutOff" runat="server" AutoPostBack="True" Width="200px" onselectedindexchanged="_ddlItemCutOff_SelectedIndexChanged"></asp:dropdownlist>&nbsp;&nbsp;<asp:button id="_addNewItem" runat="server" Width="80px" CssClass="button1" Text="Add" onclick="_addNewItem_Click"></asp:button>&nbsp;&nbsp;&nbsp;
													<asp:button id="_btnEditItem" runat="server" Width="80px" CssClass="button1" Text="Edit" onclick="_btnEditItem_Click"></asp:button></TD>
											<tr>
												<TD></TD>
											</tr>
											<TR>
												<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnTemplate" runat="server" Width="180px" CssClass="button1" Text="Template" onclick="_btnTemplate_Click"></asp:button>&nbsp;
													<asp:button id="_btnItem" runat="server" Width="180px" CssClass="button1" Text="Item" onclick="_btnItem_Click"></asp:button></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR id="TR_ADD_NEW_ITEM" runat="server">
						<TD align="center" colSpan="2">
							<TABLE class="td" id="TableEditfgfd" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="0"
								cellPadding="0" width="590" border="1">
								<TR>
									<TD class="tdHeader1"><asp:label id="_labelAddItem" runat="server">Add Item</asp:label></TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center">
										<TABLE id="TableEdit27ere" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="TDBGColor1" width="170">Item :</TD>
												<TD width="5"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="_txtItemName" Width="333px" TextMode="MultiLine" Runat="server"></asp:textbox></TD>
											<tr>
												<TD></TD>
											</tr>
											<TR>
												<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnAddNewItem" runat="server" Width="180px" CssClass="button1" Text="Add" onclick="_btnAddNewItem_Click"></asp:button></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR id="TR_TEMPLATE" runat="server">
						<TD align="center" colSpan="2">
							<TABLE class="td" id="TableEdit271" style="WIDTH: 645px; HEIGHT: 182px" cellSpacing="0"
								cellPadding="0" width="645" border="0">
								<TR>
									<TD class="tdHeader1">Template</TD>
								</TR>
								<TR>
									<td>
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD class="TDBGColorValue" style="WIDTH: 342px" align="center">
													<table id="TableEdit24" cellSpacing="0" cellPadding="0" width="50%" border="0">
														<TR>
															<TD class="tdHeader1">Available Template</TD>
														</TR>
														<tr>
															<td><asp:listbox id="_listBoxAvailableTemplate" runat="server" Width="323px"></asp:listbox>&nbsp;
															</td>
														</tr>
														<tr>
															<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnAddTemplate" runat="server" Width="118px" CssClass="button1" Text="Add" onclick="_btnAddTemplate_Click"></asp:button></TD>
														</tr>
													</table>
												</TD>
												<TD class="TDBGColorValue" align="center">
													<table id="TableEdit2478" cellSpacing="0" cellPadding="0" width="50%" border="0">
														<TR>
															<TD class="tdHeader1">Applied Template</TD>
														</TR>
														<tr>
															<td><asp:listbox id="_listBoxAppliedTemplate" runat="server" Width="323px"></asp:listbox>&nbsp;
															</td>
														</tr>
														<tr>
															<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnRemoveTemplate" runat="server" Width="118px" CssClass="button1" Text="Remove" onclick="_btnRemoveTemplate_Click"></asp:button></TD>
														</tr>
													</table>
												</TD>
											</TR>
										</table>
									</td>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				</TD><tr>
				</tr>
				<TR id="TR_ITEM" runat="server">
					<TD colSpan="2"><ASP:DATAGRID id="DatGrd" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="ID" HeaderText="ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SCORERESULT" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PROPORSIACCOUNT" HeaderText="Proporsi">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DESCLINE" HeaderText="Desc Line">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LOWESTSCORE" HeaderText="Lowest">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HIGHESTSCORE" HeaderText="Highest">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ISACTIVE" HeaderText="Active">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LINE" HeaderText="Line">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
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
						</ASP:DATAGRID></TD>
				</TR>
				<tr>
				</tr>
				<TR id="TR_EDIT_PARAMETER" runat="server">
					<TD colSpan="2" align="center">
						<TABLE class="td" id="TableEdit1" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1"
							cellPadding="1" width="590" border="1">
							<TR>
								<TD class="tdHeader1">Edit Parameter</TD>
							</TR>
							<TR>
								<TD vAlign="top" align="center">
									<TABLE id="TableEdit2" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD class="TDBGColor1" width="170">ID&nbsp;:</TD>
											<TD width="5"></TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtEditedID" runat="server" Width="280px"
													Enabled="False" BackColor="Silver" MaxLength="20"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" width="170">Description :</TD>
											<TD width="5"></TD>
											<TD class="TDBGColorValue"><asp:textbox id="_txtEditedDesc" runat="server" Width="280px" BackColor="White" MaxLength="20"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" width="170">Proportion :</TD>
											<TD width="5"></TD>
											<TD class="TDBGColorValue"><asp:textbox id="_txtEditedProp" runat="server" Width="280px" BackColor="White" MaxLength="20"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" width="170">Lowest Score :</TD>
											<TD width="5"></TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtEditedLowestScr" runat="server" Width="280px"
													BackColor="White" MaxLength="20"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" width="170">Highest Score :</TD>
											<TD width="5"></TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtEditedHighestScr" runat="server" Width="280px"
													BackColor="White" MaxLength="20"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Line :</TD>
											<TD></TD>
											<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdEditedLine" runat="server" AutoPostBack="True" Width="200px" RepeatDirection="Vertical">
													<asp:ListItem Value="0">Uppest Line</asp:ListItem>
													<asp:ListItem Value="1" Selected="True">Middle Line</asp:ListItem>
													<asp:ListItem Value="2">Lowest Line</asp:ListItem>
												</asp:radiobuttonlist></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Status :</TD>
											<TD></TD>
											<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdEditedStatus" runat="server" AutoPostBack="True" Width="150px" RepeatDirection="Horizontal">
													<asp:ListItem Value="1" Selected="True">Enable</asp:ListItem>
													<asp:ListItem Value="0">Disable</asp:ListItem>
												</asp:radiobuttonlist></TD>
										</TR>
										<tr>
											<TD></TD>
										</tr>
										<TR>
											<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnEditedUpdate" runat="server" Width="180px" CssClass="button1" Text="Update Parameter" onclick="_btnEditedUpdate_Click"></asp:button>&nbsp;
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR id="TR_NEW_PARAMETER" runat="server">
					<TD colSpan="2" align="center">
						<TABLE class="td" id="TableNew1" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1"
							cellPadding="1" width="590" border="1">
							<TR>
								<TD class="tdHeader1">Insert New Parameter</TD>
							</TR>
							<TR>
								<TD vAlign="top" align="center">
									<TABLE id="TableNew2" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD class="TDBGColor1" width="170">Description :</TD>
											<TD width="5"></TD>
											<TD class="TDBGColorValue"><asp:textbox id="_txtNewDesc" runat="server" Width="280px" BackColor="White" MaxLength="20"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" width="170">Proportion :</TD>
											<TD width="5"></TD>
											<TD class="TDBGColorValue"><asp:textbox id="_txtNewProp" runat="server" Width="280px" BackColor="White" MaxLength="20"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" width="170">Lowest Score :</TD>
											<TD width="5"></TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtNewLowestScr" runat="server" Width="280px"
													BackColor="White" MaxLength="20"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" width="170">Highest Score :</TD>
											<TD width="5"></TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="_txtNewHighestScr" runat="server" Width="280px"
													BackColor="White" MaxLength="20"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Line :</TD>
											<TD></TD>
											<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdNewLine" runat="server" AutoPostBack="True" Width="200px" RepeatDirection="Vertical">
													<asp:ListItem Value="0">Uppest Line</asp:ListItem>
													<asp:ListItem Value="1" Selected="True">Midle Line</asp:ListItem>
													<asp:ListItem Value="2">Lowest Line</asp:ListItem>
												</asp:radiobuttonlist></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Status :</TD>
											<TD></TD>
											<TD class="TDBGColorValue"><asp:radiobuttonlist id="_rdNewStatus" runat="server" AutoPostBack="True" Width="150px" RepeatDirection="Horizontal">
													<asp:ListItem Value="1" Selected="True">Enable</asp:ListItem>
													<asp:ListItem Value="0">Disable</asp:ListItem>
												</asp:radiobuttonlist></TD>
										</TR>
										<TR>
											<TD></TD>
										</TR>
										<TR>
											<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnNewUpdate" runat="server" Width="180px" CssClass="button1" Text="Insert" onclick="_btnNewUpdate_Click"></asp:button>&nbsp;
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<br>
				<table width="100%">
					<TR>
						<TD class="tdHeader1" colSpan="3">Maker Request</TD>
					</TR>
					<tr>
						<td>
							<table width="100%">
								<TR id="Tr2" runat="server">
									<TD colSpan="2"><ASP:DATAGRID id="DatGrdItemTemplate" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
											PageSize="5">
											<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
												<asp:BoundColumn DataField="DESCRIPTION" HeaderText="Description">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ISACTIVE" HeaderText="Active">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="STATUS" HeaderText="Status">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
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
										</ASP:DATAGRID></TD>
								</TR>
								<TR id="Tr3" runat="server">
									<TD colSpan="2"><ASP:DATAGRID id="DatGrdCutOff" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
											PageSize="5">
											<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
												<asp:BoundColumn DataField="DESCRIPTION" HeaderText="Description">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="PROPORSIACCOUNT" HeaderText="Proporsi">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ISACTIVE" HeaderText="Active">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="STATUS" HeaderText="Status">
													<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
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
										</ASP:DATAGRID></TD>
								</TR>
								<tr>
								</tr>
							</table>
						</td>
					</tr>
					<tr id="TR_EDIT_REQUEST_ITEM" runat="server">
						<td>
							<center>
								<TABLE class="td" id="TableNew100" style="WIDTH: 590px; HEIGHT: 140px" height="140" cellSpacing="1"
									cellPadding="1" width="590" border="1">
									<TR>
										<TD class="tdHeader1">
											Update Parameter</TD>
									</TR>
									<TR>
										<TD vAlign="top" align="center">
											<TABLE id="TableEdit27ere" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR>
													<TD class="TDBGColor1" width="170">Item :</TD>
													<TD width="5"></TD>
													<TD class="TDBGColorValue"><asp:textbox id="_txtItemTemplate" Width="333px" TextMode="MultiLine" Runat="server"></asp:textbox></TD>
												<tr>
													<TD></TD>
												</tr>
												<TR>
													<TD vAlign="top" align="center" colSpan="3" height="10"><asp:button id="_btnUpdateRequestParam" runat="server" Width="180px" CssClass="button1" Text="Add" onclick="Button1_Click"></asp:button></TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
								</TABLE>
							</center>
						</td>
					</tr>
					<asp:Label ID="_lblIdItemTemplate" Runat="server" Visible="False"></asp:Label>
				</table>
			</center>
		</form>
		<asp:Label Runat="SERVER" ID="_lblIDDatGrdItemTemplate" Visible="False"></asp:Label>
	</body>
</HTML>
