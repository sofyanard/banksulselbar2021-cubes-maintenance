<%@ Page language="c#" Codebehind="RFProcedurePath.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Area.SME.RFProcedurePath" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFProcedurePath</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center">
										<B>Parameter Maintenance : Common</B></TD>
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
						<TD class="td" vAlign="top" colspan="2">
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="tdLabel" style="WIDTH: 203px" align="right" width="203" bgColor="#d"><STRONG><FONT color="#330099">Module</FONT></STRONG></TD>
									<TD style="WIDTH: 5px">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
									<TD class="TDBGColorValue">
										<asp:radiobuttonlist id="RBL_MODULE" runat="server" AutoPostBack="True" Width="248px" RepeatDirection="Horizontal" onselectedindexchanged="RBL_MODULE_SelectedIndexChanged">
											<asp:ListItem Value="01" Selected="True">SME</asp:ListItem>
											<asp:ListItem Value="20">Credit Card</asp:ListItem>
											<asp:ListItem Value="40">Consumer</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
							</TABLE>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="200">Area</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:DropDownList id="DDL_AREAID" runat="server" AutoPostBack="True" CssClass="mandatory" onselectedindexchanged="DDL_AREAID_SelectedIndexChanged"></asp:DropDownList></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Program</TD>
									<TD></TD>
									<TD class="TDBGColorValue">
										<asp:DropDownList id="DDL_PROGRAMID" runat="server" AutoPostBack="True" CssClass="mandatory" onselectedindexchanged="DDL_PROGRAMID_SelectedIndexChanged"></asp:DropDownList></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Product</TD>
									<TD></TD>
									<TD class="TDBGColorValue">
										<asp:DropDownList id="DDL_PRODUCTID" runat="server" AutoPostBack="True" CssClass="mandatory" onselectedindexchanged="DDL_PRODUCTID_SelectedIndexChanged"></asp:DropDownList></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Track From</TD>
									<TD></TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_PP_TRACKFROM" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Track Next</TD>
									<TD></TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_PP_TRACKNEXT" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Track Back</TD>
									<TD></TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_PP_TRACKBACK" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Track Fail</TD>
									<TD></TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_PP_TRACKFAIL" runat="server"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button>
							<asp:label id="LBL_SAVEMODE" runat="server" Visible="False">0</asp:label>
							<asp:label id="LBL_PENDINGSTATUS" runat="server" Visible="False">1</asp:label>
							<asp:label id="LBL_AREAID_OLD" runat="server" Visible="False"></asp:label>
							<asp:label id="LBL_PROGRAMID_OLD" runat="server" Visible="False"></asp:label>
							<asp:label id="LBL_PRODUCTID_OLD" runat="server" Visible="False"></asp:label>
							<asp:label id="LBL_PP_TRACKFROM_OLD" runat="server" Visible="False"></asp:label>
							<asp:label id="LBL_PP_TRACKNEXT_OLD" runat="server" Visible="False"></asp:label>
							<asp:label id="LBL_PP_TRACKBACK_OLD" runat="server" Visible="False"></asp:label>
							<asp:label id="LBL_PP_TRACKFAIL_OLD" runat="server" Visible="False"></asp:label>
							<asp:label id="Label1" runat="server"></asp:label>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Current Product Table</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
							<asp:datagrid id="DGR_CURRENT" runat="server" AllowPaging="True" PageSize="5" AutoGenerateColumns="False"
								Width="100%">
								<Columns>
									<asp:BoundColumn DataField="AREAID" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="AREANAME" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader" HorizontalAlign="Center"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMID" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTID" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTDESC" HeaderText="Product">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
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
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
							<asp:datagrid id="DGR_MAKER" runat="server" AllowPaging="True" PageSize="5" AutoGenerateColumns="False"
								Width="100%">
								<Columns>
									<asp:BoundColumn DataField="AREAID" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="AREANAME" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader" HorizontalAlign="Center"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMID" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTID" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTDESC" HeaderText="Product">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
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
									<asp:BoundColumn DataField="PENDINGDESC" HeaderText="Request Status">
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
