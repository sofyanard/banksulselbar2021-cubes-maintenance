<%@ Page language="c#" Codebehind="ProductParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.ProductParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProductParam</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_entries.html" -->
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="125%">
				<TR>
					<TD class="tdNoBorder" colSpan="2">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													General Maker</B></TD>
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
					<TD class="tdHeader1" colSpan="2">Parameter Product Maker</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="left" width="100%">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="300">Product Code</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PR_CODE" runat="server" Width="72px" CssClass="mandatory"></asp:textbox><asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label><asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="300">Product Name</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_PRNAME" runat="server" Width="380px" CssClass="mandatory"
										MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="HEIGHT: 23px">Group Name</TD>
								<TD style="WIDTH: 7px; HEIGHT: 22px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:dropdownlist id="DDL_GID" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="HEIGHT: 22px">Customer Type</TD>
								<TD style="WIDTH: 7px; HEIGHT: 22px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 7px"><asp:dropdownlist id="DDL_CUST_TYPE" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Negative List</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_NEGLST" runat="server"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Blacklist</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_BLACK" runat="server"></asp:checkbox></TD>
							<TR>
								<TD class="TDBGColor1">Prescreneer</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_PRES" runat="server"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">DHBI</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_DHBI" runat="server"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Promo Group</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PROMO" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="HEIGHT: 22px">Type</TD>
								<TD style="WIDTH: 7px; HEIGHT: 22px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 15px"><asp:dropdownlist id="DDL_TYPE" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">NPWP Limit</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_NPWP" onblur="FormatCurrency(this)" runat="server" Width="144px">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Down Payment</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_DP" runat="server" Width="56px" MaxLength="5"></asp:textbox>&nbsp;%</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="HEIGHT: 19px">SPPK Limit Time</TD>
								<TD style="WIDTH: 7px; HEIGHT: 19px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 19px"><asp:textbox onkeypress="return digitsonly()" id="TXT_SPPK_LM" runat="server" Width="144px" MaxLength="10">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">AIP Limit Time</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_AIP_LM" runat="server" Width="144px" MaxLength="10">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Admin Fee</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_ADMIN_FEE" onblur="FormatCurrency(this)" runat="server" Width="144px">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Floor Rate</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_FLOOR_RATE" runat="server" Width="56px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="HEIGHT: 23px">Floor Limit</TD>
								<TD style="WIDTH: 7px; HEIGHT: 23px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 23px"><asp:textbox id="TXT_FLOOR_LIMIT" onblur="FormatCurrency(this)" runat="server" Width="144px">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Ceiling Limit</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_CEIL_LIMIT" onblur="FormatCurrency(this)" runat="server" Width="144px">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Provisi</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_PROVISI" onblur="FormatCurrency(this)" runat="server" Width="144px">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Provition Rate</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_PROV_RATE" runat="server" Width="56px"
										MaxLength="5"></asp:textbox>&nbsp;%</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Fiducia</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_FIDUCIA" onblur="FormatCurrency(this)" runat="server" Width="144px">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Fiducia Limit</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_FIDUCIA_LIM" onblur="FormatCurrency(this)" runat="server" Width="144px">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Bea Other</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_BEA_OTHER" onblur="FormatCurrency(this)" runat="server" Width="144px">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">eMAS Code</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_EMAS_CODE" runat="server" Width="144px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">SPK</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_SPK" runat="server"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Marketing Source Code</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_MARKET" runat="server" Width="144px"></asp:textbox><asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Kendara</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_PR_KENDARA" runat="server"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Revolving</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_REVOLVING" runat="server"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Round Approval</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_ROUND_APPROVAL" runat="server"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Active</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_ACTIVE" runat="server"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Doc RAB</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_DOC_RAB" runat="server"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Allow Cardbundling</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_CARDBUNDLING" runat="server"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Mitrakarya</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_PR_MITRAKARYA" runat="server"></asp:checkbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Child Product Data</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="left" width="100%">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="300">Child Product Code&nbsp;</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CHILDPRODUCT" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Child Minimum Tenor</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_CHILDMINTENOR" runat="server" Width="144px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="HEIGHT: 21px">Child Maximum Tenor</TD>
								<TD style="WIDTH: 7px; HEIGHT: 21px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:textbox onkeypress="return numbersonly()" id="TXT_CHILDMAXTENOR" runat="server" Width="144px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="HEIGHT: 21px">Child Default Tenor</TD>
								<TD style="WIDTH: 7px; HEIGHT: 21px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:textbox onkeypress="return numbersonly()" id="TXT_CHILDDEFTENOR" runat="server" Width="144px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Child Minimum Ratio</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_CHILDMINRATIO" runat="server" Width="144px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Child Maximum Ratio</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_CHILDMAXRATIO" runat="server" Width="144px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Child Default Ratio</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_CHILDDEFRATIO" runat="server" Width="144px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Child Minimum Interest</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_CHILDMININTEREST" runat="server" Width="56px"
										MaxLength="5"></asp:textbox>%</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Child Maximum Interest</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_CHILDMAXINTEREST" runat="server" Width="56px"
										MaxLength="5"></asp:textbox>%</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Child Default Interest</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_CHILDDEFINTEREST" runat="server" Width="56px"
										MaxLength="5"></asp:textbox>%</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="HEIGHT: 22px">Child Minimum Limit</TD>
								<TD style="WIDTH: 7px; HEIGHT: 22px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox id="TXT_CHILDMINLIMIT" onblur="FormatCurrency(this)" runat="server" Width="144px">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Child Maximum Limit</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_CHILDMAXLIMIT" onblur="FormatCurrency(this)" runat="server" Width="144px">0</asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Current&nbsp;Product Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" colSpan="2"><asp:datagrid id="DG1" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="PRODUCTID" HeaderText="Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Product Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GROUP_NAME" HeaderText="Group">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CUSTOMER_TYPE" HeaderText="Customer Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Negative List">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CHK_NG1" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Blacklist">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CHK_BL1" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pree screener">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CHK_PRE1" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="DHBI">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CHK_DHBI1" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="NAMA_PROMO" HeaderText="Promo Group">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PROD_TP" HeaderText="Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MIN_LIMIT_NPWP" HeaderText="NPWP Limit" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_ADMIN" HeaderText="Admin Fee" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DOWN_PAYMENT" HeaderText="Down Payment">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_SPPKLMTTIME" HeaderText="SPPK Limit Time">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_AIPLIMITTIME" HeaderText="AIP Limit Time">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="FLOOR_RATE" HeaderText="Floor Rate">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="FLOOR_LIMIT" HeaderText="Floor Limit" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CEILLING_LIMIT" HeaderText="Ceiling Limit" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_PROVISI" HeaderText="Provisi" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_PROVPERCENT" HeaderText="Provition Rate">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_FIDUCIA" HeaderText="Fiducia" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_LIMITFIDUCIA" HeaderText="Fiducia Limit" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PR_SRCCODE" HeaderText="Marketing Source Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_SIBS" HeaderText="eMAS Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="SPK">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CHK_SPK1" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Allow Cardbundling">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CHK_ALLOWCARDBUNDLING1" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="PARAMSTATUS" HeaderText="Active">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDetail" runat="server" CommandName="detail">Detail</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete" runat="server" CommandName="delete">Delete</asp:LinkButton><BR>
										<BR>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="NL_CHECK">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="BLACKLIST_CHECK">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRESCRE_CHECK">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DHBI_CHECK">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_SPK">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="GROUP_ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="GROUP_PROMO">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ALLOWCARDBUNDLING">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRODUCT_TYPE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_KENDARA">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CHILDPRODUCT">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CHILDMINTENOR">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CHILDMAXTENOR">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CHILDDEFTENOR">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CHILDMINRATIO">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CHILDMAXRATIO">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CHILDDEFRATIO">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CHILDDEFINTEREST">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CHILDMININTEREST">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CHILDMAXINTEREST">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CHILDMINLIMIT">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CHILDMAXLIMIT">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="REVOLVING">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ROUND_APPROVAL">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ACTIVE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DOC_RAB">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Maker Request</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" colSpan="2"><asp:datagrid id="DG2" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="PRODUCTID" HeaderText="Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Product Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GROUP_NAME" HeaderText="Group">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CUSTOMER_TYPE" HeaderText="Customer Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Negative List">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CHK_NG2" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Blacklist">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CHK_BL2" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pree screener">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CHK_PRE2" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="DHBI">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CHK_DHBI2" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="NAMA_PROMO" HeaderText="Promo Group">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PROD_TP" HeaderText="Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MIN_LIMIT_NPWP" HeaderText="NPWP Limit" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_ADMIN" HeaderText="Admin Fee" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DOWN_PAYMENT" HeaderText="Down Payment">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_SPPKLMTTIME" HeaderText="SPPK Limit Time">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_AIPLIMITTIME" HeaderText="AIP Limit Time">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="FLOOR_RATE" HeaderText="Floor Rate" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="FLOOR_LIMIT" HeaderText="Floor Limit" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CEILLING_LIMIT" HeaderText="Ceiling Limit" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_PROVISI" HeaderText="Provisi" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_PROVPERCENT" HeaderText="Provition Rate">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_FIDUCIA" HeaderText="Fiducia" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_LIMITFIDUCIA" HeaderText="Fiducia Limit" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_BEAOTH" HeaderText="Bea Other" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_SIBS" HeaderText="eMAS Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="SPK">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CHK_SPK2" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Allow Cardbundling">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CHK_ALLOWCARDBUNDLING2" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="PR_SRCCODE" HeaderText="Marketing Source Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="Lb_edit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="Lb_det" runat="server" CommandName="detail">Detail</asp:LinkButton>&nbsp;
										<asp:LinkButton id="Lb_del" runat="server" CommandName="delete">Delete</asp:LinkButton><BR>
										<BR>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="NL_CHECK">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="BLACKLIST_CHECK">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRESCRE_CHECK">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DHBI_CHECK">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_SPK">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="GROUP_ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="GROUP_PROMO">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ALLOWCARDBUNDLING">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
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
