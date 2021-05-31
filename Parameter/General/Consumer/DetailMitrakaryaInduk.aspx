<%@ Page language="c#" Codebehind="DetailMitrakaryaInduk.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.DetailMitrakaryaInduk" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Detail Mitrakarya Induk</title>
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
					<TD class="TDBGColor1">Company System Code</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" width="80%"><asp:label id="LBL_MKI_CODE" runat="server"></asp:label>
						<asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1">Company Marketing Code</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue"><asp:label id="LBL_MKI_SRCCODE" runat="server"></asp:label>
						<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1">Company Name</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue"><asp:label id="LBL_COMP_NAME" runat="server"></asp:label>
						<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1">Expire Date</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue"><asp:label id="LBL_EXP_DATE" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1">Company Rating</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue"><asp:label id="LBL_RT_CODE" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="td" colSpan="3"><asp:datagrid id="DGR_DETAIL" runat="server" AutoGenerateColumns="False" Width="100%">
							<AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="MTK_SRCCODE" HeaderText="Company Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_DESC" HeaderText="Company Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_SUBINTEREST" HeaderText="Sub Interest">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_GRLINE" HeaderText="Guarantor Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_PLAFOND" HeaderText="Realisasi" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SISA" HeaderText="Remaining Guarantor's Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_EXPIREDATE" HeaderText="Expire Date">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="RT_DESC" HeaderText="Company Rating">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MKT_POTGAJI" HeaderText="Persentase Pot. Gaji">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MKT_PESENAM" HeaderText="Pesangon 6 th" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MKT_PESATENAM" HeaderText="Pesangon &gt; 6 th" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_INDLIMIT" HeaderText="Limit Individu(Maximal)" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_INDTENOR" HeaderText="Tenor Individu(Bulan)">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="COVER" HeaderText="Cover THT">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
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
