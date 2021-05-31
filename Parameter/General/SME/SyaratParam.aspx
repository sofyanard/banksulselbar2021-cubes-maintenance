<%@ Page language="c#" Codebehind="SyaratParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.SyaratParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Parameter Setup</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
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
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Setup</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A>
							<asp:ImageButton id="BTN_BACK" runat="server" ImageUrl="/SME/image/Back.jpg" onclick="BTN_BACK_Click"></asp:ImageButton><A href="../../../Body.aspx"><IMG src="/SME/Image/MainMenu.jpg"></A><A href="../../../Logout.aspx" target="_top"><IMG src="/SME/Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD class="tdNoBorder" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" align="center" width="50%" colSpan="2">
							Syarat - Syarat</TD>
					</TR>
					<asp:label id="lbl_CU_CUSTTYPEID" runat="server" Visible="False"></asp:label>
					<TR id="TR_PERSONAL" runat="server">
						<TD class="td" vAlign="top" width="50%" colSpan="2">
							<TABLE id="Table20" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="129">Jenis Syarat</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_DOCTYPEID" runat="server" AutoPostBack="True" CssClass="mandatory" onselectedindexchanged="DDL_DOCTYPEID_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 20px" vAlign="top">Syarat</TD>
									<TD style="HEIGHT: 20px" vAlign="top">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:dropdownlist id="DDL_DOCUMENT" runat="server" CssClass="mandatory" Width="780px"></asp:dropdownlist></TD>
								</TR> <!-- Additional Field : Right --></TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" width="50%" colSpan="2">
							<asp:Label id="LBL_SAVEMODE" runat="server" Visible="False"></asp:Label>
							<asp:dropdownlist id="DDL_DOCTYPEDESC" runat="server" Visible="False" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_DOCTYPEDESC_SelectedIndexChanged"></asp:dropdownlist>
							<asp:Label id="LBL_DOCTYPEDESC" runat="server" Visible="False"></asp:Label>
							<asp:RadioButton id="RDO_YES" runat="server" Text="Yes" Checked="True" GroupName="ISMANDATORY" Visible="False"></asp:RadioButton>
							<asp:RadioButton id="RDO_NO" runat="server" Text="No" GroupName="ISMANDATORY" Visible="False"></asp:RadioButton><asp:button id="BTN_SAVE" runat="server" Width="100px" Text="Save" CssClass="Button1" onclick="BTN_SAVE_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2">Existing Syarat</TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2"><ASP:DATAGRID id="DGDocuments" runat="server" Width="100%" AllowPaging="True" CellPadding="1"
								AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID">
										<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESC" HeaderText="Description">
										<HeaderStyle Width="750px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="MANDATORY" HeaderText="Mandatory">
										<HeaderStyle Width="60px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Delete" CommandName="Delete">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
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
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2">Syarat Requested</TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2">
							<ASP:DATAGRID id="DGDocRequest" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID">
										<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESC" HeaderText="Description">
										<HeaderStyle Width="750px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="MANDATORY" HeaderText="Mandatory">
										<HeaderStyle Width="60px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS" HeaderText="PENDINGSTATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Pending Status">
										<HeaderStyle Width="70px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
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
