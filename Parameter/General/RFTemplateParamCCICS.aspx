<%@ Page language="c#" Codebehind="RFTemplateParamCCICS.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.RFTemplateParamCCICS" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CustCategory</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../include/cek_entries.html" -->
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="Table6">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Credit 
											Card&nbsp;Host - Maker</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A>
							<asp:ImageButton id="BTN_BACK" runat="server" ImageUrl="../../image/Back.jpg" onclick="BTN_BACK_Click"></asp:ImageButton><A href="../../Body.aspx"><IMG src="../../Image/MainMenu.jpg"></A>
							<A href="../../Logout.aspx" target="_top"><IMG src="../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdNoBorder" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" align="center" width="50%" colSpan="2"><asp:label id="LBL_PARAMNAME" runat="server"></asp:label></TD>
					</TR>
					<asp:label id="lbl_CU_CUSTTYPEID" runat="server" Visible="False"></asp:label>
					<TR id="TR_PERSONAL" runat="server">
						<TD class="td" vAlign="top" width="50%" colSpan="2">
							<TABLE id="Table20" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 23px" width="129">ID</TD>
									<TD style="WIDTH: 15px; HEIGHT: 23px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 23px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ID" runat="server" MaxLength="50" CssClass="mandatory"></asp:textbox><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label><asp:label id="LBL_ACTIVE" runat="server" Visible="False"></asp:label><asp:label id="LBL_ID" runat="server" Visible="False"></asp:label><asp:label id="LBL_DESC" runat="server" Visible="False"></asp:label><asp:label id="LBL_SICS" runat="server" Visible="False"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 20px" vAlign="top">Description</TD>
									<TD style="HEIGHT: 20px" vAlign="top">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox id="TXT_DESC" runat="server" MaxLength="100" Width="700px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 20px" vAlign="top">SICS Code</TD>
									<TD style="HEIGHT: 20px" vAlign="top">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_SICS" runat="server" MaxLength="10" Width="136px"></asp:textbox>
										<asp:label id="Label1" runat="server" Visible="False"></asp:label></TD>
								</TR> <!-- Additional Field : Right --></TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" width="50%" colSpan="2"><asp:button id="BTN_SAVE" runat="server" CssClass="Button1" Width="100px" Text="Save" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL" runat="server" CssClass="Button1" Width="100px" Text="Cancel" onclick="BTN_CANCEL_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2">Existing Data</TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2"><ASP:DATAGRID id="DGExisting" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
								AllowPaging="True" PageSize="6">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESC" HeaderText="Description">
										<HeaderStyle Width="60%" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SICS_ID" HeaderText="SICS Code">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" CommandName="Edit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
									</asp:ButtonColumn>
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
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2">Requested Data</TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2"><ASP:DATAGRID id="DGRequest" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
								AllowPaging="True" PageSize="6">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="ID" HeaderText="ID">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESC" HeaderText="Description">
										<HeaderStyle Width="50%" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SICS_ID" HeaderText="SICS Code">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDING_STATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDING_STATUS1" HeaderText="Pending Status">
										<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" CommandName="Edit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Delete" CommandName="Delete">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
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
