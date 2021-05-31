<%@ Page language="c#" Codebehind="MappingConsCC.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.MappingConsCC.MappingConcCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MappingConcCC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table6">
							<TR>
								<TD class="tdBGColor2" style="WIDTH: 400px" align="center">
									<P><B>Mapping Consumer-Credit Card&nbsp;: Maker</B></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
					<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
						<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2"><asp:label id="LBL_PARAMNAME" runat="server"></asp:label></TD>
				</TR>
				<TR id="TR_PERSONAL" runat="server">
					<TD class="td" vAlign="top" width="50%" colSpan="2">
						<TABLE id="Table20" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1">ID (Consumer)</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue">
									<asp:textbox onkeypress="return kutip_satu()" id="TXT_ConsID" runat="server" Columns="15" MaxLength="5"
										ReadOnly="True"></asp:textbox>
								</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Description (Consumer)</TD>
								<TD style="HEIGHT: 10px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ConsDESC" runat="server" Columns="100"
										MaxLength="100" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR id="TR_CD_SIBS" runat="server">
								<TD class="TDBGColor1">Credit Card Mapping</TD>
								<TD style="HEIGHT: 10px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:dropdownlist id="DDL_CCMAP" runat="server"></asp:dropdownlist></TD>
							</TR>
							<!-- Additional Field : Right --></TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" runat="server" CssClass="Button1" Text="Save" Width="100px" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL" runat="server" CssClass="Button1" Text="Cancel" Width="100px" onclick="BTN_CANCEL_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Existing Data</TD>
				</TR>
				<TR>
					<TD colSpan="2"><ASP:DATAGRID id="DGExisting" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
							AllowPaging="True" PageSize="20">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="ConsID" HeaderText="ID (Consumer)">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ConsDESC" HeaderText="Description (Consumer)">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ConsCCMAPID" HeaderText="ConsCCMAPID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CCID" HeaderText="Map ID (Credit Card)">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CCDESC" HeaderText="Description (Credit Card)">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn Text="Edit" CommandName="edit">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
								</asp:ButtonColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</ASP:DATAGRID></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Requested Data</TD>
				</TR>
				<TR>
					<TD colSpan="2"><ASP:DATAGRID id="DGRequest" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
							AllowPaging="True" PageSize="6">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="ConsID" HeaderText="ID (Consumer)">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ConsDESC" HeaderText="Description (Consumer)">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ConsCCMAPID" HeaderText=" Map ID (Credit Card)">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CCDESC" HeaderText="Description (Credit Card)">
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
