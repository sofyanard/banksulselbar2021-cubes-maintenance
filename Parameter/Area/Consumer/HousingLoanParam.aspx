<%@ Page language="c#" Codebehind="HousingLoanParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Area.Consumer.HousingLoanParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Housing Loan Parameter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table7" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													Area Maker</B></TD>
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
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Housing Loan Project Maker</TD>
				</TR>
				<TR>
					<TD class="td">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD width="66%">
									<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%">
										<TR>
											<TD class="TDBGColor1" width="100">City</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CITY" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_CITY_SelectedIndexChanged"></asp:dropdownlist></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" width="100">Project Code</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PROJECT_CODE" runat="server" CssClass="mandatory"
													Columns="10" MaxLength="10"></asp:textbox><asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label><asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Developer</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_DEVELOPER" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Location</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_LOCATION" runat="server"></asp:dropdownlist></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Project Name</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_PROJECT_NAME" runat="server" MaxLength="50"
													Width="99%"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Type Bangunan</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:radiobuttonlist id="RBL_TYPE_BANG" runat="server" AutoPostBack="True" RepeatLayout="Flow" onselectedindexchanged="RBL_TYPE_BANG_SelectedIndexChanged">
													<asp:ListItem Value="1" Selected="True">Rumah / Ruko (Landed House)</asp:ListItem>
													<asp:ListItem Value="2">Apartemen / Kios / Mall</asp:ListItem>
												</asp:radiobuttonlist></TD>
										</TR>
										<TR id="TR_BANG" runat="server">
											<TD class="TDBGColor1"></TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue">
												<asp:checkbox id="CB_BANG_JADI_SERT_BLM" runat="server" AutoPostBack="True" Text="Bangunan jadi - Sertifikat induk" oncheckedchanged="CB_BANG_JADI_SERT_BLM_CheckedChanged"></asp:checkbox><br>
												<asp:checkbox id="CB_BANG_BLM_SERT_BLM" runat="server" AutoPostBack="True" Text="Bangunan indent - Sertifikat induk" oncheckedchanged="CB_BANG_BLM_SERT_BLM_CheckedChanged"></asp:checkbox><br>
												<asp:checkbox id="CB_BANG_JADI_SERT_JADI" runat="server" Text="Bangunan jadi - Sertifikat pecah"></asp:checkbox><br>
												<asp:checkbox id="CB_BANG_BLM_SERT_JADI" runat="server" Text="Bangunan indent - Sertifikat pecah"></asp:checkbox><br>
											</TD>
										</TR>
										<TR id="TR_SHGB" runat="server">
											<TD class="TDBGColor1"></TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:radiobuttonlist id="RBL_SHGB_GABUNG" runat="server" RepeatLayout="Flow">
													<asp:ListItem Value="0" Selected="True">SHGB tanah bersama dalam proses panggabungan</asp:ListItem>
													<asp:ListItem Value="1">SHGB tanah bersama telah digabung</asp:ListItem>
												</asp:radiobuttonlist></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Param Number</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PARAM_NUMBER" runat="server" Width="88px">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Remark</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_REMARK" runat="server" MaxLength="50" Width="99%"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Guarantor Line</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_GUARANTOR_LINE" onblur="FormatCurrency(this)" runat="server" Columns="25"
													MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Realisasi</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_REALISASI" onblur="FormatCurrency(this)" runat="server" Columns="25" MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Harga Minimal</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_HARGAMIN" onblur="FormatCurrency(this)" runat="server" Columns="25" MaxLength="15">0</asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Harga Maksimal</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_HARGAMAX" onblur="FormatCurrency(this)" runat="server" Columns="25" MaxLength="15">0</asp:textbox></TD>
										</TR>
									</TABLE>
								</TD>
								<TD width="34%">
									<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
										<TR>
											<TD class="TDBGColorValue" style="WIDTH: 50px">&nbsp;</TD>
											<TD class="TDBGColorValue" style="WIDTH: 8px">&nbsp;</TD>
											<TD class="TDBGColorValue">&nbsp;
												<asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Hospital</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:checkbox id="CB_HOSPITAL" runat="server"></asp:checkbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">School</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:checkbox id="CB_SCHOOL" runat="server"></asp:checkbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Market Place</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:checkbox id="CB_MARKETPLACE" runat="server"></asp:checkbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Lake</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:checkbox id="CB_LAKE" runat="server"></asp:checkbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Park</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:checkbox id="CB_PARK" runat="server"></asp:checkbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Sport Center</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:checkbox id="CB_SPORTCENTER" runat="server"></asp:checkbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">Marketing Source Code</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_MKT_SOURCE_CODE" runat="server" Columns="10" MaxLength="10"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1">eMAS Code</TD>
											<TD style="WIDTH: 8px">:</TD>
											<TD class="TDBGColorValue"><asp:textbox id="TXT_EMAS_CODE" runat="server" Columns="10" MaxLength="10"></asp:textbox></TD>
										</TR>
										<TR>
											<TD>&nbsp;</TD>
											<TD style="WIDTH: 8px">&nbsp;</TD>
											<TD>&nbsp;</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Current Housing Loan&nbsp;Project Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="PROYEK_ID" HeaderText="Project Code">
									<HeaderStyle Width="6%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DEVELOPER_ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PROYEK_DESCRIPTION" HeaderText="Project Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ID_KOTA">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ID_LOKASI">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LOKASI" HeaderText="Location">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DEVELOPER" HeaderText="Developer">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PH_GRLINE" HeaderText="Guarantor Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PH_PLAFOND" HeaderText="Realisasi" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PH_SRCCODE" HeaderText="Marketing Source Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_SIBS" HeaderText="eMAS Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
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
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="PROYEK_ID" HeaderText="Project Code">
									<HeaderStyle Width="6%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DEVELOPER_ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PROYEK_DESCRIPTION" HeaderText="Project Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ID_KOTA">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ID_LOKASI">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="LOKASI" HeaderText="Location">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DEVELOPER" HeaderText="Developer">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PH_GRLINE" HeaderText="Guarantor Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PH_PLAFOND" HeaderText="Realisasi" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PH_SRCCODE" HeaderText="Marketing Source Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_SIBS" HeaderText="eMAS Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="STATUS" HeaderText="Status">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="Linkbutton1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
