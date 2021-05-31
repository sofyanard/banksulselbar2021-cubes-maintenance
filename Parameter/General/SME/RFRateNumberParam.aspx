<%@ Page language="c#" Codebehind="RFRateNumberParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFRateNumberParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Parameter Setup</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_all.html" -->
		<!-- #include file="../../../include/cek_mandatory.html" -->
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
							<TABLE id="Table6">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Setup</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A>
							<asp:ImageButton id="BTN_BACK" runat="server" ImageUrl="/SME/image/Back.jpg" onclick="BTN_BACK_Click"></asp:ImageButton><A href="../../../Body.aspx"><IMG src="/SME/Image/MainMenu.jpg"></A><A href="../../../Logout.aspx" target="_top"><IMG src="/SME/Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD class="tdNoBorder" colspan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2" align="center">
							Rate Number Parameter</TD>
					</TR>
					<asp:label id="lbl_CU_CUSTTYPEID" runat="server" Visible="False"></asp:label>
					<TR id="TR_PERSONAL" runat="server">
						<TD class="td" vAlign="top" width="50%" colspan="2">
							<TABLE id="Table20" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="129">Rate No.</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue">
										<asp:textbox onkeypress="return kutip_satu()" id="TXT_RATENO" runat="server" MaxLength="10" CssClass="mandatory"
											Width="137"></asp:textbox>
										<asp:Label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:Label>
										<asp:DropDownList id="DDL_SIBS_PRODCODE" runat="server" Visible="False" AutoPostBack="True" onselectedindexchanged="DDL_SIBS_PRODCODE_SelectedIndexChanged"></asp:DropDownList></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="129">SIBS Product Code</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue">
										<asp:textbox onkeypress="return kutip_satu()" id="TXT_SIBS_PRODCODE" runat="server" Width="137"
											CssClass="mandatory" MaxLength="10"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="129">
										Currency</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue">
										<asp:DropDownList id="DDL_CURRENCY" runat="server" CssClass="mandatory"></asp:DropDownList></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 20px" vAlign="top">Rate</TD>
									<TD style="HEIGHT: 20px" vAlign="top">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px">
										<asp:textbox onkeypress="return kutip_satu()" id="TXT_RATE" runat="server" MaxLength="8" CssClass="mandatory" ontextchanged="TXT_RATE_TextChanged"></asp:textbox></TD>
								</TR> <!-- Additional Field : Right --></TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" width="50%" colspan="2">
							<asp:button id="BTN_SAVE" runat="server" CssClass="Button1" Text="Save" Width="101" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL" Width="101px" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2">Existing Data</TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2">
							<ASP:DATAGRID id="DGExisting" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="RATENO" HeaderText="Rate No.">
										<HeaderStyle Width="70px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CURRENCY" HeaderText="Currency">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CURRENCYDESC" HeaderText="Currency Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SIBS_PRODCODE" HeaderText="SIBS Product Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RATE" HeaderText="Rate">
										<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" CommandName="Edit">
										<HeaderStyle Width="70px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Delete" CommandName="Delete">
										<HeaderStyle Width="70px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2">Requested Data</TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2">
							<ASP:DATAGRID id="DGRequest" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="RATENO" HeaderText="Rate No.">
										<HeaderStyle Width="70px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CURRENCY" HeaderText="Currency">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CURRENCYDESC" HeaderText="Currency Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SIBS_PRODCODE" HeaderText="SIBS Product Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RATE" HeaderText="Rate">
										<HeaderStyle Width="90px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Pending Status">
										<HeaderStyle Width="70px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" CommandName="Edit">
										<HeaderStyle Width="70px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Delete" CommandName="Delete">
										<HeaderStyle Width="70px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
