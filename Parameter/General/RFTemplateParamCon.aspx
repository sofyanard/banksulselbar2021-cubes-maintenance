<%@ Page language="c#" Codebehind="RFTemplateParamCon.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.RFTemplateParamCon" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFTemplateParamCon</title>
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
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table6">
							<TR>
								<TD class="tdBGColor2" style="WIDTH: 400px" align="center">
									<P><B>Parameter Consumer General - Maker&nbsp;
											<asp:label id="LBL_PRM_NM" runat="server" Visible="False"></asp:label></B></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
					<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../Body.aspx"><IMG src="../../Image/MainMenu.jpg"></A>
						<A href="../../Logout.aspx" target="_top"><IMG src="../../Image/Logout.jpg"></A>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2"><asp:label id="LBL_PARAMNAME" runat="server"></asp:label></TD>
				</TR>
				<TR id="TR_PERSONAL" runat="server">
					<TD class="td" vAlign="top" width="50%" colSpan="2">
						<TABLE id="Table20" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="129">ID</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ID" runat="server" CssClass="mandatory"
										MaxLength="5" Columns="15"></asp:textbox><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label><asp:label id="LBL_ACTIVE" runat="server" Visible="False"></asp:label><asp:label id="LBL_ID" runat="server" Visible="False"></asp:label><asp:label id="LBL_DESC" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_AUTO" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_NB" runat="server" Visible="False">1</asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Description</TD>
								<TD style="HEIGHT: 10px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_DESC" runat="server" CssClass="mandatory"
										MaxLength="100" Columns="100"></asp:textbox></TD>
							</TR>
							<TR id="TR_CD_SIBS" runat="server">
								<TD class="TDBGColor1">SIBS Code</TD>
								<TD style="HEIGHT: 10px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_CD_SIBS" runat="server" MaxLength="10"
										Columns="15"></asp:textbox></TD>
							</TR>
							<!-- Additional Field : Right --></TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" runat="server" CssClass="Button1" Width="100px" Text="Save" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL" runat="server" CssClass="Button1" Width="100px" Text="Cancel" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_DB_NAMA" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Existing Data</TD>
				</TR>
				<TR>
					<TD colSpan="2"><ASP:DATAGRID id="DGExisting" runat="server" Width="100%" PageSize="20" AllowPaging="True" CellPadding="1"
							AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="ID" HeaderText="ID">
									<HeaderStyle Width="150px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DESC" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CD_SIBS" HeaderText="SIBS Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn Text="Edit" CommandName="edit">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
								</asp:ButtonColumn>
								<asp:ButtonColumn Text="Delete" CommandName="delete">
									<HeaderStyle Width="80px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</ASP:DATAGRID></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Requested Data</TD>
				</TR>
				<TR>
					<TD colSpan="2"><ASP:DATAGRID id="DGRequest" runat="server" Width="100%" PageSize="6" AllowPaging="True" CellPadding="1"
							AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="ID" HeaderText="ID">
									<HeaderStyle Width="150px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DESC" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PENDING_STATUS"></asp:BoundColumn>
								<asp:BoundColumn DataField="PENDINGSTATUS" HeaderText="Pending Status">
									<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CD_SIBS" HeaderText="SIBS Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn Text="Edit" CommandName="edit">
									<HeaderStyle CssClass="tdSmallHeader" Width="80px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
								<asp:ButtonColumn Text="Delete" CommandName="delete">
									<HeaderStyle Width="80px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</ASP:DATAGRID></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
