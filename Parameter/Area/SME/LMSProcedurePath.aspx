<%@ Page language="c#" Codebehind="LMSProcedurePath.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.LMSProcedurePath" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LMSProcedurePath</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="Table2">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
											LMS</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
							<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter Procedure Path Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" colSpan="2">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1">Track From</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PP_TRACKFROM" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Track Next</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PP_TRACKNEXT" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Track Back</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PP_TRACKBACK" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Track Fail</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PP_TRACKFAIL" runat="server"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">0</asp:label><asp:label id="LBL_PENDINGSTATUS" runat="server" Visible="False">1</asp:label><asp:label id="LBL_AREAID_OLD" runat="server" Visible="False"></asp:label><asp:label id="LBL_PROGRAMID_OLD" runat="server" Visible="False"></asp:label><asp:label id="LBL_PRODUCTID_OLD" runat="server" Visible="False"></asp:label><asp:label id="LBL_PP_TRACKFROM_OLD" runat="server" Visible="False"></asp:label><asp:label id="LBL_PP_TRACKNEXT_OLD" runat="server" Visible="False"></asp:label><asp:label id="LBL_PP_TRACKBACK_OLD" runat="server" Visible="False"></asp:label><asp:label id="LBL_PP_TRACKFAIL_OLD" runat="server" Visible="False"></asp:label><asp:label id="Label1" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Current Data</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_CURRENT" runat="server" AllowPaging="True" PageSize="5" AutoGenerateColumns="False"
								Width="100%">
								<Columns>
									<asp:BoundColumn DataField="PP_TRACKFROM" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKFROMNAME" HeaderText="Track From">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKNEXT" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKNEXTNAME" HeaderText="Track Next">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKBACK" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKBACKNAME" HeaderText="Track Back">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKFAIL" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKFAILNAME" HeaderText="Track Fail">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Maker Request</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_MAKER" runat="server" AllowPaging="True" PageSize="5" AutoGenerateColumns="False"
								Width="100%">
								<Columns>
									<asp:BoundColumn DataField="PP_TRACKFROM" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKFROMNAME" HeaderText="Track From">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKNEXT" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKNEXTNAME" HeaderText="Track Next">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKBACK" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKBACKNAME" HeaderText="Track Back">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKFAIL" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKFAILNAME" HeaderText="Track Fail">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PENDINGSTATUS" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDINGSTATUSNAME" HeaderText="Request Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RqEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RqDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2">&nbsp;</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
