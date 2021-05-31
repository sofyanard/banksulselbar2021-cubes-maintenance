<%@ Page language="c#" Codebehind="UserMaintenance.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Report.UserMaintenance" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UserMaintenance</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>User Maintenance Audit 
											Trail</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="../Body.aspx"><IMG src="../Image/MainMenu.jpg"></A><A href="../Logout.aspx" target="_top"><IMG src="../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD class="td" colSpan="2" height="41">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="70%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Date</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_AU_DATE_FROM_D" runat="server" MaxLength="2"
											Columns="4" Width="24px"></asp:textbox><asp:dropdownlist id="DDL_AU_DATE_FROM_M" runat="server"></asp:dropdownlist><asp:textbox onkeypress="return numbersonly()" id="TXT_AU_DATE_FROM_Y" runat="server" MaxLength="4"
											Columns="4" Width="36px"></asp:textbox>&nbsp; - &nbsp;
										<asp:textbox onkeypress="return numbersonly()" id="TXT_AU_DATE_TO_D" runat="server" MaxLength="2"
											Columns="4" Width="24px"></asp:textbox><asp:dropdownlist id="DDL_AU_DATE_TO_M" runat="server"></asp:dropdownlist><asp:textbox onkeypress="return numbersonly()" id="TXT_AU_DATE_TO_Y" runat="server" MaxLength="4"
											Columns="4" Width="36px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Area</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_AREAID" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_AREAID_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Branch</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_BRANCH_CODE" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Group</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_GROUPID" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 129px"></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:CheckBox id="Chk_AuditTrail" runat="server" Text="View Group Access Menu Audit Trail"></asp:CheckBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">User ID</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_USERID" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 129px"></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD>&nbsp;
										<asp:button id="BTN_SEARCH" runat="server" Width="75px" Text="Search" onclick="BTN_SEARCH_Click"></asp:button>&nbsp;
										<asp:button id="BTN_CLEAR" runat="server" Width="75px" Text="Clear" onclick="BTN_CLEAR_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD height="41"><asp:label id="Label1" runat="server" Visible="False"></asp:label></TD>
						<TD height="41"></TD>
					</TR>
					<TR>
						<TD align="right" colSpan="2"><ASP:DATAGRID id="DatGrd" runat="server" Width="100%" Visible="False" AllowSorting="True" AllowPaging="True"
								AutoGenerateColumns="False" CellPadding="1">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IDGROUP" HeaderText="ID / Group">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DATEDESC" HeaderText="Date / Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AU_FROM" HeaderText="Old Value">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AU_TO" HeaderText="New Value">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MODBY_FULLNAME" HeaderText="Modified By">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
					<TR style="width:100%">
						<TD align="center"  style="width:100%">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="510px"></rsweb:ReportViewer>
                        </TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
