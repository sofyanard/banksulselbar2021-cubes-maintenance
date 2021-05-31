<%@ Page language="c#" Codebehind="JobTitleParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.JobTitleParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Job Title Parameter Approval</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px" cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter 
													Consumer&nbsp;Maintenance : Host&nbsp;Maker</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
									<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Job Title&nbsp;Maker</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2">
						<TABLE id="Table10" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="150">Code</TD>
								<TD style="WIDTH: 9px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ID" runat="server" Columns="10" MaxLength="3"
										CssClass="mandatory"></asp:textbox><asp:label id="LBL_SAVEMODE" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="HEIGHT: 22px">Description</TD>
								<TD style="HEIGHT: 22px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_DESC" runat="server" Columns="50" MaxLength="30"
										CssClass="mandatory"></asp:textbox>
									<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">SIBS Code</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_CD_SIBS" runat="server" Columns="10" MaxLength="10"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" runat="server" CssClass="Button1" Text="Save" Width="100px" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL" runat="server" CssClass="Button1" Text="Cancel" Width="100px"></asp:button><asp:label id="LBL_JT_SEQ" runat="server" Visible="False"></asp:label><asp:label id="LBL_CODE_LAMA" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Existing Data</TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="2"><ASP:DATAGRID id="DGEXISTING" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="JOB_TYPE_ID" HeaderText="Code">
									<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="JT_DESC" HeaderText="Description">
									<HeaderStyle Width="500px" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
									<HeaderStyle Width="130px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn Text="Edit" CommandName="edit">
									<HeaderStyle Width="80px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
								<asp:ButtonColumn Text="Delete" CommandName="delete">
									<HeaderStyle Width="80px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
								<asp:BoundColumn Visible="False" DataField="JT_CODE">
									<HeaderStyle Width="130px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</ASP:DATAGRID></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Requested Data</TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="2"><ASP:DATAGRID id="DGREQUEST" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="JOB_TYPE_ID" HeaderText="Code">
									<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="JT_DESC" HeaderText="Description">
									<HeaderStyle Width="500px" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
									<HeaderStyle Width="130px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA" HeaderText="Pending Status">
									<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="JT_CODE">
									<HeaderStyle Width="130px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</ASP:DATAGRID></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
