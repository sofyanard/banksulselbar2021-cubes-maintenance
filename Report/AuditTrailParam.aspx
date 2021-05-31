<%@ Page language="c#" Codebehind="AuditTrailParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Report.AuditTrailParam" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AuditTrailParam</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../include/cek_entries.html" -->
		<!-- #include file="../include/cek_mandatoryOnly.html" -->
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="TableHeader">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>
											<asp:label id="LBL_MODULENAME" runat="server"></asp:label>
											&nbsp;Parameter Audit Trail</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="../Body.aspx"><IMG src="../Image/MainMenu.jpg"></A><A href="../Logout.aspx" target="_top"><IMG src="../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2" height="41">
							<TABLE class="td" id="Table3" cellSpacing="2" cellPadding="1" border="1">
								<TR>
									<TD align="center">
										<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%">
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 129px">Date</TD>
												<TD style="WIDTH: 15px"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_AU_DATE_FROM_D" runat="server" Width="24px"
														Columns="4" MaxLength="2"></asp:textbox><asp:dropdownlist id="DDL_AU_DATE_FROM_M" runat="server"></asp:dropdownlist><asp:textbox onkeypress="return numbersonly()" id="TXT_AU_DATE_FROM_Y" runat="server" Width="36px"
														Columns="4" MaxLength="4"></asp:textbox>&nbsp; - &nbsp;
													<asp:textbox onkeypress="return numbersonly()" id="TXT_AU_DATE_TO_D" runat="server" Width="24px"
														Columns="4" MaxLength="2"></asp:textbox><asp:dropdownlist id="DDL_AU_DATE_TO_M" runat="server"></asp:dropdownlist><asp:textbox onkeypress="return numbersonly()" id="TXT_AU_DATE_TO_Y" runat="server" Width="36px"
														Columns="4" MaxLength="4"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 15px">Parameter Class</TD>
												<TD style="WIDTH: 15px; HEIGHT: 15px"></TD>
												<TD class="TDBGColorValue" style="HEIGHT: 15px"><asp:dropdownlist id="DDL_PARAMCLASS" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_PARAMCLASS_SelectedIndexChanged"></asp:dropdownlist><asp:label id="LBL_PARAMCLASS" runat="server" Visible="False"></asp:label></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 17px">Parameter Group</TD>
												<TD style="WIDTH: 15px; HEIGHT: 17px"></TD>
												<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:dropdownlist id="DDL_PARAMGROUP" runat="server" AutoPostBack="True" Enabled="False" onselectedindexchanged="DDL_PARAMGROUP_SelectedIndexChanged"></asp:dropdownlist><asp:label id="LBL_PARAMGROUP" runat="server" Visible="False"></asp:label></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 20px">Parameter Name</TD>
												<TD style="WIDTH: 15px; HEIGHT: 20px"></TD>
												<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:dropdownlist id="DDL_AU_PARAMNAME" runat="server" Enabled="False"></asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1">Modified by</TD>
												<TD style="WIDTH: 15px"></TD>
												<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_AU_BY" runat="server"></asp:dropdownlist><asp:label id="LBL_DB_IP_SME" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAMA_SME" runat="server" Visible="False"></asp:label>
													<asp:label id="LBL_DB_NAMA_MNT" runat="server" Visible="False"></asp:label>
													<asp:label id="LBL_DB_IP_MNT" runat="server" Visible="False"></asp:label>
													<asp:label id="LBL_DB_LOGINID_MNT" runat="server" Visible="False"></asp:label>
													<asp:label id="LBL_DB_LOGINPWD_MNT" runat="server" Visible="False"></asp:label></TD>
											</TR>
										</TABLE>
										<asp:button id="BTN_SEARCH" runat="server" Width="75px" Text="Search" CssClass="Button1" onclick="BTN_SEARCH_Click"></asp:button>&nbsp;
										<asp:button id="BTN_CLEAR" runat="server" Width="75px" Text="Clear" CssClass="Button1" onclick="BTN_CLEAR_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2" height="41">
							<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="510px"></rsweb:ReportViewer>
						</TD>
					</TR>
					<TR>
						<TD height="41"><asp:label id="Label1" runat="server" Visible="False"></asp:label></TD>
						<TD height="41"><asp:label id="LBL_DB_LOGINID_SME" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_LOGINPWD_SME" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_LOGINID" runat="server" Visible="False"></asp:label></TD>
					</TR>
					<TR>
						<TD class="td" align="right" colSpan="2"><ASP:DATAGRID id="DGR_LIST" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
								AutoGenerateColumns="False" CellPadding="1">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle HorizontalAlign="Center" Width="5%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AU_PARAMNAME" HeaderText="Parameter">
										<HeaderStyle Width="20%" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AU_ID" HeaderText="ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AU_FIELD" HeaderText="Field">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AU_FROM" HeaderText="Old Value">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AU_TO" HeaderText="New Value">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AU_ACTION" HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AU_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}">
										<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SU_FULLNAME" HeaderText="Modified By">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
