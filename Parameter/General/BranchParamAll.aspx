<%@ Page language="c#" Codebehind="BranchParamAll.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.BranchParamAll" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BranchParamAll</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../include/cek_entries.html" -->
		<script language="javascript">
		function disableControl() 
		{
			if ( Form1.RBL_BRANCH_TYPE_SME.value = '3' ) 
			{
				Form1.DDL_CBC_CODE.Enabled = false;
			}
			else 
			{
				Form1.DDL_CBC_CODE.Enabled = true;
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder" style="HEIGHT: 49px"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
							<TABLE id="Table2">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
											Host</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" style="HEIGHT: 49px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton>
							<A href="../../Body.aspx"><IMG src="../../Image/MainMenu.jpg"></A> <A href="../../Logout.aspx" target="_top">
								<IMG src="../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter Branch Maker</TD>
					</TR>
					<TR>
						<TD class="td" style="HEIGHT: 17px" vAlign="top" width="50%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="200">Module</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RBL_MODULE" runat="server" Width="248px" RepeatDirection="Horizontal" AutoPostBack="True" onselectedindexchanged="RBL_MODULE_SelectedIndexChanged">
											<asp:ListItem Value="0" Selected="True">SME</asp:ListItem>
											<asp:ListItem Value="1">Consumer/Credit Card</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="200">Branch&nbsp;Code</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_BRANCH_CODE" runat="server" MaxLength="10"
											CssClass="mandatory"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 22px">Branch&nbsp;Name</TD>
									<TD style="HEIGHT: 22px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_BRANCH_NAME" runat="server" Width="100%"
											MaxLength="50"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 22px">Address</TD>
									<TD style="HEIGHT: 22px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_BR_ADDR" runat="server" Width="100%" MaxLength="100"></asp:textbox></TD>
								</TR>
								<TR id="TR_BRANCHAREA" runat="server">
									<TD class="TDBGColor1" style="HEIGHT: 22px">Branch Area</TD>
									<TD style="HEIGHT: 22px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_BR_BRANCH_AREA" runat="server" Width="100%"
											MaxLength="100"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 17px">City</TD>
									<TD style="HEIGHT: 17px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:dropdownlist onkeypress="return kutip_satu()" id="DDL_CITYID" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_CITYID_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR id="TR_AREA" runat="server">
									<TD class="TDBGColor1" style="HEIGHT: 21px">Area</TD>
									<TD style="HEIGHT: 21px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_AREAID" runat="server" Width="208px"></asp:textbox><asp:label id="LBL_CONSAREA_ID" runat="server" Visible="False"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 20px">Zipcode</TD>
									<TD style="HEIGHT: 20px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return numbersonly()" id="TXT_BR_ZIPCODE" runat="server" MaxLength="6"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 20px"><asp:label id="LBL_REGION_CONS" runat="server">Region</asp:label><asp:label id="LBL_AREA_SME" runat="server">Area</asp:label></TD>
									<TD style="HEIGHT: 20px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:dropdownlist id="DDL_REGIONAL_ID" runat="server"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 93px">SIBS Code</TD>
									<TD style="WIDTH: 13px" width="13"></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_CD_SIBS" runat="server" MaxLength="10"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 93px"><asp:label id="LBL_PHONE" runat="server">Phone</asp:label><asp:label id="LBL_FAX" runat="server"> / Fax</asp:label></TD>
									<TD style="WIDTH: 13px" width="13"></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PHONEAREA" runat="server" Width="40px"
											MaxLength="4"></asp:textbox><asp:textbox onkeypress="return numbersonly()" id="TXT_PHONE" runat="server" Width="113px" MaxLength="10"></asp:textbox><asp:textbox onkeypress="return numbersonly()" id="TXT_PHONEEXT" runat="server" Width="51px"
											MaxLength="4"></asp:textbox><asp:textbox onkeypress="return kutip_satu()" id="TXT_BR_PHNFAX" runat="server" Width="294px"
											MaxLength="50"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 93px">Category</TD>
									<TD style="WIDTH: 13px" width="13"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist onkeypress="return kutip_satu()" id="DDL_BR_ISBRANCH" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Branch Type</TD>
									<TD width="13"></TD>
									<TD class="TDBGColorValue"><asp:radiobuttonlist id="RBL_BRANCH_TYPE_CONCC" runat="server" RepeatDirection="Horizontal">
											<asp:ListItem Value="0">Community Branch</asp:ListItem>
											<asp:ListItem Value="1">Scoring Branch</asp:ListItem>
											<asp:ListItem Value="2" Selected="True">None</asp:ListItem>
										</asp:radiobuttonlist><asp:radiobuttonlist id="RBL_BRANCH_TYPE_SME" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" onselectedindexchanged="RBL_BRANCH_TYPE_SME_SelectedIndexChanged">
											<asp:ListItem Value="3">CBC</asp:ListItem>
											<asp:ListItem Value="4">CCO</asp:ListItem>
											<asp:ListItem Value="5" Selected="True">None</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR id="TR_CBC" runat="server">
									<TD class="TDBGColor1" style="WIDTH: 93px; HEIGHT: 6px">CBC&nbsp;Code</TD>
									<TD style="WIDTH: 13px; HEIGHT: 6px" width="13"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 6px"><asp:dropdownlist onkeypress="return kutip_satu()" id="DDL_CBC_CODE" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 93px"><asp:label id="LBL_COMBRANCH" runat="server">Comm. Branch</asp:label><asp:label id="LBL_CCOBRANCH" runat="server">CCO Branch</asp:label></TD>
									<TD style="WIDTH: 13px" width="13"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist onkeypress="return kutip_satu()" id="DDL_BR_CCOBRANCH_CONCC" runat="server"></asp:dropdownlist><asp:dropdownlist onkeypress="return kutip_satu()" id="DDL_BR_CCOBRANCH_SME" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR id="TR_ISBOOKINGBRANCH" runat="server">
									<TD class="TDBGColor1" style="HEIGHT: 17px">Booking Branch</TD>
									<TD style="HEIGHT: 17px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:dropdownlist id="DDL_ISBOOKINGBRANCH" runat="server"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label><asp:label id="LBL_STA" runat="server" Visible="False"></asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							<P>Existing Data</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_EXISTING_CONNCC" runat="server" Width="100%" PageSize="200" AutoGenerateColumns="False">
								<Columns>
									<asp:BoundColumn DataField="BRANCH_CODE" HeaderText="Branch Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_NAME" HeaderText="Branch Name">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CITYID" HeaderText="CITYID"></asp:BoundColumn>
									<asp:BoundColumn DataField="CITY_NAME" HeaderText="City">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AREA_ID" HeaderText="AREA_ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="AREA_NAME" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="REGIONAL_ID" HeaderText="REGIONAL_ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="REGIONAL_NAME" HeaderText="Region">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ADDR" HeaderText="Address">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ZIPCODE" HeaderText="Zipcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PHONEAREA" HeaderText="Phone Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PHONE" HeaderText="Phone Numb">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PHONEEXT" HeaderText="Phone Ext.">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BR_ISBRANCH" HeaderText="BR_ISBRANCH"></asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ISBRANCH_DESC" HeaderText="Category">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BRANCH_TYPE" HeaderText="BRANCH_TYPE"></asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_TYPE_DESC" HeaderText="Branch Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CBC_CODE" HeaderText="CBC Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_CCOBRANCH" HeaderText="Comm. Branch">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle Width="12px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LinkView4" runat="server" Visible="False" CommandName="view">View</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_Edit4" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_Delete4" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid><asp:datagrid id="DGR_EXISTING_SME" runat="server" Width="100%" AllowPaging="True" PageSize="5"
								AutoGenerateColumns="False">
								<Columns>
									<asp:BoundColumn DataField="BRANCH_CODE" HeaderText="Branch Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_NAME" HeaderText="Branch Name">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CITYID"></asp:BoundColumn>
									<asp:BoundColumn DataField="CITY_NAME" HeaderText="City">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AREAID"></asp:BoundColumn>
									<asp:BoundColumn DataField="AREA_NAME" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ADDR" HeaderText="Address">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_BRANCHAREA" HeaderText="Branch Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ZIPCODE" HeaderText="Zipcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_PHNFAX" HeaderText="Phone / Fax">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BR_ISBRANCH" HeaderText="BR_ISBRANCH"></asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ISBRANCH_DESC" HeaderText="Category">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BRANCH_TYPE" HeaderText="BRANCH_TYPE"></asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_TYPE_DESC" HeaderText="Branch Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CBC_CODE" HeaderText="CBC Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_CCOBRANCH" HeaderText="CCO Branch">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BR_ISBOOKINGBRANCH"></asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ISBOOKINGBRANCH" HeaderText="Booking Branch">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle Width="12px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LinkView2" runat="server" Visible="False" CommandName="view">View</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_Edit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_Delete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_REQUEST_CONNCC" runat="server" Width="100%" PageSize="20" AutoGenerateColumns="False">
								<Columns>
									<asp:BoundColumn DataField="BRANCH_CODE" HeaderText="Branch Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_NAME" HeaderText="Branch Name">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CITYID" HeaderText="CITYID"></asp:BoundColumn>
									<asp:BoundColumn DataField="CITY_NAME" HeaderText="City">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AREA_NAME" HeaderText="AREA_NAME"></asp:BoundColumn>
									<asp:BoundColumn DataField="AREA_NAME" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="REGIONAL_ID" HeaderText="REGIONAL_ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="REGIONAL_NAME" HeaderText="Region">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ADDR" HeaderText="Address">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ZIPCODE" HeaderText="Zipcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PHONEAREA" HeaderText="Phone Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PHONE" HeaderText="Phone Numb">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PHONEEXT" HeaderText="Phone Ext.">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BR_ISBRANCH" HeaderText="BR_ISBRANCH"></asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ISBRANCH_DESC" HeaderText="Category">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BRANCH_TYPE" HeaderText="BRANCH_TYPE"></asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_TYPE_DESC" HeaderText="Branch Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CBC_CODE" HeaderText="CBC Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_CCOBRANCH" HeaderText="Comm. Branch">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDING_STATUS" HeaderText="PENDING_STATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BR_PHNFAX" HeaderText="BR_PHNFAX"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BR_ISBOOKINGBRANCH" HeaderText="BR_ISBOOKINGBRANCH"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BR_BRANCHAREA" HeaderText="BR_BRANCHAREA"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle Width="12px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LinkView3" runat="server" Visible="False" CommandName="view">View</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_Edit3" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_Delete3" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid><asp:datagrid id="DGR_REQUEST_SME" runat="server" Width="100%" AllowPaging="True" PageSize="5"
								AutoGenerateColumns="False">
								<Columns>
									<asp:BoundColumn DataField="BRANCH_CODE" HeaderText="Branch Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_NAME" HeaderText="Branch Name">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CITYID"></asp:BoundColumn>
									<asp:BoundColumn DataField="CITY_NAME" HeaderText="City">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AREAID" HeaderText="AREAID"></asp:BoundColumn>
									<asp:BoundColumn DataField="AREA_NAME" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ADDR" HeaderText="Address">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_BRANCHAREA" HeaderText="Branch Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ZIPCODE" HeaderText="Zipcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_PHNFAX" HeaderText="Phone / Fax">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BR_ISBRANCH" HeaderText="BR_ISBRANCH"></asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ISBRANCH_DESC" HeaderText="Category">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BRANCH_TYPE"></asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_TYPE_DESC" HeaderText="Branch Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CBC_CODE" HeaderText="CBC Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_CCOBRANCH" HeaderText="CCO Branch">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BR_ISBOOKINGBRANCH" HeaderText="BR_ISBOOKINGBRANCH"></asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ISBOOKINGBRANCH" HeaderText="Booking Branch">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDING_STATUS" HeaderText="PENDING_STATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PHONEAREA" HeaderText="PHONEAREA"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PHONE" HeaderText="PHONE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PHONEEXT" HeaderText="PHONEEXT"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="REGIONAL_ID" HeaderText="REGIONAL_ID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle Width="12px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LinkView2" runat="server" Visible="False" CommandName="view">View</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_Edit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_Delete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2"><asp:label id="LBL_BRANCH_TYPE_SME" runat="server" Visible="False"></asp:label>&nbsp;
							<asp:label id="LBL_BR_ISBOOKINGBRANCH" runat="server" Visible="False"></asp:label><asp:label id="LBL_BR_PHNFAX" runat="server" Visible="False"></asp:label><asp:label id="LBL_BR_CCOBRANCH_SME" runat="server" Visible="False"></asp:label><asp:label id="LBL_BRANCH_TYPE_CONCC" runat="server" Visible="False"></asp:label><asp:label id="LBL_BR_CCOBRANCH_CONCC" runat="server" Visible="False"></asp:label><asp:label id="LBL_BR_BRANCHAREA" runat="server" Visible="False"></asp:label><asp:label id="LBL_PHONE1" runat="server" Visible="False"></asp:label><asp:label id="LBL_PHONEAREA" runat="server" Visible="False"></asp:label><asp:label id="LBL_PHONEEXT" runat="server" Visible="False"></asp:label><asp:label id="LBL_AREAID" runat="server" Visible="False"></asp:label><asp:label id="Label4" runat="server" Visible="False"></asp:label><asp:label id="Label5" runat="server" Visible="False"></asp:label></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
