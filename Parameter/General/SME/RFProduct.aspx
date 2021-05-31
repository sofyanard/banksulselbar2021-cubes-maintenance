<%@ Page language="c#" Codebehind="RFProduct.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFProduct" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFProduct</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_entries.html" -->
		<script language="vbscript">
			Function enableAlternateRate()
				If RBL_NEGO.disabled = false Then
					If RBL_NEGO.SelectedIndex = 1 Then
						Form1.BTN_ALERNATE_RATE.visible = true
					End If
				End If
			End Function

			Function productID()
				productID = TXT_PRODUCTID.value
			End Function
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
							<TABLE id="Table2">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter : Area</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="/SME/Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="/SME/Image/MainMenu.jpg"></A>
							<A href="../../../Logout.aspx" target="_top"><IMG src="/SME/Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter Product Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="200">Product Code</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_PRODUCTID" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">LOS Product Name</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_PRODUCTDESC" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">SIBS Product Code</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_SIBS_PRODCODE" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">SIBS Facility Code</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_SIBS_PRODID" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Currency</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CURRENCY" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Calculation Method</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CALCMETHOD" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_CURRENCY_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Interest Rest</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_INTERESTREST" runat="server">
											<asp:ListItem Selected="True">-- Pilih --</asp:ListItem>
											<asp:ListItem Value="12">12</asp:ListItem>
											<asp:ListItem Value="4">4</asp:ListItem>
											<asp:ListItem Value="2">2</asp:ListItem>
											<asp:ListItem Value="1">1</asp:ListItem>
										</asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1"></TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDO_ISCASHLOAN" runat="server" RepeatDirection="Horizontal">
											<asp:ListItem Value="1" Selected="True">Cash Loan</asp:ListItem>
											<asp:ListItem Value="0">Non-Cash Loan</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1"></TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDO_REVOLVING" runat="server" RepeatDirection="Horizontal">
											<asp:ListItem Value="1" Selected="True">Revolving</asp:ListItem>
											<asp:ListItem Value="0">Non-Revolving</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Product Type</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_JNSPRODUCT" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Support Sub-application</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDO_SUPPORTSUBAPP" runat="server" RepeatDirection="Horizontal">
											<asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
											<asp:ListItem Value="0">No</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Agunan</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDO_AGUNAN" runat="server" RepeatDirection="Horizontal">
											<asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
											<asp:ListItem Value="0">No</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">PRK</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDO_PRK" runat="server" RepeatDirection="Horizontal">
											<asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
											<asp:ListItem Value="0">No</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Sub-product</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDO_ISSUBAPPPROD" runat="server" RepeatDirection="Horizontal">
											<asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
											<asp:ListItem Value="0">No</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 8px">Interest Type</TD>
									<TD style="HEIGHT: 8px" width="17"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 8px"><asp:dropdownlist id="DDL_INTERESTTYPE" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_INTERESTTYPE_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1"></TD>
									<TD width="17"></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RBL_NEGO" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False"
											Width="233px" onselectedindexchanged="RBL_NEGO_SelectedIndexChanged">
											<asp:ListItem Value="0" Selected="True">Not Negotiable</asp:ListItem>
											<asp:ListItem Value="1">Negotiable</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Fixed&nbsp;Rate</TD>
									<TD width="17"></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_INTERESTTYPERATE" runat="server" Enabled="False"
											Columns="4"></asp:textbox>%</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Rate Number</TD>
									<TD width="17"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_RATENO" runat="server" AutoPostBack="True" Enabled="False" onselectedindexchanged="DDL_RATENO_SelectedIndexChanged"></asp:dropdownlist>&nbsp;
										<asp:textbox onkeypress="return kutip_satu()" id="TXT_RATE" runat="server" Columns="4" ReadOnly="True"></asp:textbox>%&nbsp;
										<asp:dropdownlist id="DDL_RATE" runat="server" Visible="False"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Variance Code</TD>
									<TD width="17"></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDO_VARCODE" runat="server" RepeatDirection="Horizontal" Enabled="False">
											<asp:ListItem Selected="True" Value="">&amp;nbsp;&amp;nbsp;&amp;nbsp;</asp:ListItem>
											<asp:ListItem Value="+">+</asp:ListItem>
											<asp:ListItem Value="-">-</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Variance</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_VARIANCE" runat="server" Enabled="False"
											Columns="4"></asp:textbox>%</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">SPK</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDO_SPK" runat="server" RepeatDirection="Horizontal">
											<asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
											<asp:ListItem Value="0">No</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Payment Type</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDO_ISINSTALLMENT" runat="server" RepeatDirection="Horizontal">
											<asp:ListItem Value="1" Selected="True">Installment</asp:ListItem>
											<asp:ListItem Value="0">Interest</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Installment Type</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_INSTALLMENTTYPE" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Rekening Koran</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDO_CONFIRMKORAN" runat="server" RepeatDirection="Horizontal">
											<asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
											<asp:ListItem Value="0">No</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Upload to eMAS</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDO_ISUPLOADEMAS" runat="server" RepeatDirection="Horizontal">
											<asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
											<asp:ListItem Value="0">No</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
                                <TR>
									<TD class="TDBGColor1">Limit Atas</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_LIMIT_ATAS" runat="server"></asp:textbox></TD>
								</TR>
                                <TR>
									<TD class="TDBGColor1">Limit Bawah</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_LIMIT_BAWAH" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">NCL Code</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_NCL_CODE" runat="server"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_ALTERNATE_RATE" Visible="False" CssClass="button1" Text="Set Alternate Rate"
								Runat="server" onclick="BTN_ALTERNATE_RATE_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Current Product Table</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="Datagrid1" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
								<Columns>
									<asp:BoundColumn DataField="PRODUCTID" HeaderText="Product Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTDESC" HeaderText="Product Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SIBS_PRODCODE" HeaderText="SIBS Product Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SIBS_PRODID" HeaderText="SIBS Facility Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CURRENCY"></asp:BoundColumn>
									<asp:BoundColumn DataField="CURRENCYDESC" HeaderText="Currency">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="INTERESTREST" HeaderText="Interest Rest">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CALCMETHOD"></asp:BoundColumn>
									<asp:BoundColumn DataField="CALCMETHODDESC" HeaderText="Calculation Method">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISCASHLOAN"></asp:BoundColumn>
									<asp:BoundColumn DataField="ISCASHLOANDESC" HeaderText="Cash Loan">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="REVOLVING"></asp:BoundColumn>
									<asp:BoundColumn DataField="REVOLVINGDESC" HeaderText="Revolving">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="INTERESTTYPE"></asp:BoundColumn>
									<asp:BoundColumn DataField="INTERESTTYPEDESC" HeaderText="Interest Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="INTERESTTYPERATE" HeaderText="Interest Type Rate">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RATENO" HeaderText="Rate Ref Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RATE" HeaderText="Rate">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VARCODE" HeaderText="Varcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VARIANCE" HeaderText="Variance">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SPK"></asp:BoundColumn>
									<asp:BoundColumn DataField="SPKDESC" HeaderText="SPK">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISINSTALLMENT"></asp:BoundColumn>
									<asp:BoundColumn DataField="ISINSTALLMENTDESC" HeaderText="Payment type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="INSTALMENTTYPE"></asp:BoundColumn>
									<asp:BoundColumn DataField="INSTALMENTTYPEDESC" HeaderText="Installment Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CONFIRMKORAN"></asp:BoundColumn>
									<asp:BoundColumn DataField="CONFIRMKORANDESC" HeaderText="Rekening Koran">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISUPLOADEMAS"></asp:BoundColumn>
									<asp:BoundColumn DataField="ISUPLOADEMASDESC" HeaderText="Upload to eMAS">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="IDCFLAG"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FIRSTINSTALLDATE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="JNSPRODUCT"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SUPPORTSUBAPP"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SUPPORTEBIZ"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISSUBAPPPROD"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="INTERESTMODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISCHANGEINTERESTALLOW"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISNEGORATE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ALTRATECALC"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="NCL_CODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE"></asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="IS_AGUNAN"/>
                                    <asp:BoundColumn DataField="IS_AGUNANDESC" HeaderText="Agunan">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="LIMIT_ATAS" HeaderText="Limit Atas">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="LIMIT_BAWAH" HeaderText="Limit Bawah">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="IS_PRK"/>
                                    <asp:BoundColumn DataField="IS_PRKDESC" HeaderText="PRK">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    </asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
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
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DataGrid2" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
								<Columns>
									<asp:BoundColumn DataField="PRODUCTID" HeaderText="Product Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTDESC" HeaderText="Product Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SIBS_PRODCODE" HeaderText="SIBS Product Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SIBS_PRODID" HeaderText="SIBS Facility Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CURRENCY"></asp:BoundColumn>
									<asp:BoundColumn DataField="CURRENCYDESC" HeaderText="Currency">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="INTERESTREST" HeaderText="Interest Rest">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CALCMETHOD"></asp:BoundColumn>
									<asp:BoundColumn DataField="CALCMETHODDESC" HeaderText="Calculation Method">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISCASHLOAN"></asp:BoundColumn>
									<asp:BoundColumn DataField="ISCASHLOANDESC" HeaderText="Cash Loan">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="REVOLVING"></asp:BoundColumn>
									<asp:BoundColumn DataField="REVOLVINGDESC" HeaderText="Revolving">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="INTERESTTYPE"></asp:BoundColumn>
									<asp:BoundColumn DataField="INTERESTTYPEDESC" HeaderText="Interest Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="INTERESTTYPERATE" HeaderText="Interest Type Rate">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RATENO" HeaderText="Rate Ref Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RATE" HeaderText="Rate">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VARCODE" HeaderText="Varcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VARIANCE" HeaderText="Variance">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SPK"></asp:BoundColumn>
									<asp:BoundColumn DataField="SPKDESC" HeaderText="SPK">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISINSTALLMENT"></asp:BoundColumn>
									<asp:BoundColumn DataField="ISINSTALLMENTDESC" HeaderText="Payment type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="INSTALMENTTYPE"></asp:BoundColumn>
									<asp:BoundColumn DataField="INSTALMENTTYPEDESC" HeaderText="Installment Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CONFIRMKORAN"></asp:BoundColumn>
									<asp:BoundColumn DataField="CONFIRMKORANDESC" HeaderText="Rekening Koran">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISUPLOADEMAS"></asp:BoundColumn>
									<asp:BoundColumn DataField="ISUPLOADEMASDESC" HeaderText="Upload to eMAS">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="IDCFLAG"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FIRSTINSTALLDATE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="JNSPRODUCT"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SUPPORTSUBAPP"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SUPPORTEBIZ"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISSUBAPPPROD"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="INTERESTMODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISCHANGEINTERESTALLOW"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISNEGORATE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ALTRATECALC"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="NCL_CODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE"></asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="IS_AGUNAN"/>
                                    <asp:BoundColumn DataField="IS_AGUNANDESC" HeaderText="Agunan">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="LIMIT_ATAS" HeaderText="Limit Atas">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="LIMIT_BAWAH" HeaderText="Limit Bawah">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="IS_PRK"/>
                                    <asp:BoundColumn DataField="IS_PRKDESC" HeaderText="PRK">
                                        <HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                    </asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="STATUSDESC" HeaderText="Request Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RqEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RqDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
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
