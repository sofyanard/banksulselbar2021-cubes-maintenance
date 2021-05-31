<%@ Page language="c#" Codebehind="RFTemplateParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.RFTemplateParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFTemplateParam</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../include/cek_entries.html" -->
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
							<asp:ImageButton id="BTN_BACK" runat="server" ImageUrl="../../image/Back.jpg" onclick="BTN_BACK_Click"></asp:ImageButton><A href="../../Body.aspx"><IMG src="../../Image/MainMenu.jpg"></A>
							<A href="../../Logout.aspx" target="_top"><IMG src="../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdNoBorder" colspan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2" align="center">
							<asp:Label id="LBL_PARAMNAME" runat="server"></asp:Label></TD>
					</TR>
					<asp:label id="lbl_CU_CUSTTYPEID" runat="server" Visible="False"></asp:label>
					<TR id="TR_PERSONAL" runat="server">
						<TD class="td" vAlign="top" width="50%" colspan="2">
							<TABLE id="Table20" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="129">ID</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue">
										<asp:textbox onkeypress="return kutip_satu()" id="TXT_ID" runat="server" MaxLength="50" CssClass="mandatory"></asp:textbox>
										<asp:Label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:Label>
										<asp:Label id="LBL_ACTIVE" runat="server" Visible="False"></asp:Label>
										<asp:Label id="LBL_ID" runat="server" Visible="False"></asp:Label>
										<asp:Label id="LBL_DESC" runat="server" Visible="False"></asp:Label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 20px" vAlign="top">Description</TD>
									<TD style="HEIGHT: 20px" vAlign="top">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px">
										<asp:textbox onkeypress="return kutip_satu()" id="TXT_DESC" runat="server" MaxLength="500" Width="700px"
											CssClass="mandatory"></asp:textbox></TD>
								</TR> <!-- Additional Field : Right --></TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" width="50%" colspan="2">
							<asp:button id="BTN_SAVE" runat="server" CssClass="Button1" Text="Save" Width="100px" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL" runat="server" CssClass="Button1" Width="100px" Text="Cancel" onclick="BTN_CANCEL_Click"></asp:button>
							<asp:Label id="LBL_DB_IP" runat="server" Visible="False"></asp:Label>
							<asp:Label id="LBL_DB_NAMA" runat="server" Visible="False"></asp:Label></TD>
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
									<asp:BoundColumn DataField="ID" HeaderText="ID">
										<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESC" HeaderText="Description">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE" HeaderText="ACTIVE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
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
									<asp:BoundColumn DataField="ID" HeaderText="ID">
										<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESC" HeaderText="Description">
										<HeaderStyle Width="600px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Pending Status">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
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
