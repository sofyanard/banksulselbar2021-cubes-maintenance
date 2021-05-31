<%@ Page language="c#" Codebehind="DetailDeveloperParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Area.Consumer.DetailDeveloperParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Detail Developer Parameter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="130%" border="0">
				<TR>
					<TD style="HEIGHT: 25px" colSpan="3">&nbsp;</TD>
				</TR>
				<TR>
					<TD class="TDBGColor1">Developer Name</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" width="85%"><asp:label id="LBL_DEV_NAME" runat="server"></asp:label>
						<asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="td" colSpan="3"><asp:datagrid id="DGR_DETAIL" runat="server" AutoGenerateColumns="False" Width="100%">
							<AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="h0" HeaderText="Kode Proyek">
									<HeaderStyle CssClass="tdSmallHeader" Width="6%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h1" HeaderText="Nama Proyek">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h2" HeaderText="Lokasi">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h3" HeaderText="Kota">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h4" HeaderText="Guarantor Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h5" HeaderText="Remaining Guarantor's Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h6" HeaderText="Marketing Source Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h7" HeaderText="Rumah Sakit">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h8" HeaderText="Sekolah">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h9" HeaderText="Pusat Belanja">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h10" HeaderText="Danau">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h11" HeaderText="Taman">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h12" HeaderText="Sport Center">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="h13" HeaderText="Keterangan">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="3"><asp:button id="BTN_PRINT" CssClass="button1" Text="Print" Runat="server" onclick="BTN_PRINT_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CLOSE" CssClass="button1" Text="Close" Runat="server" onclick="BTN_CLOSE_Click"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
