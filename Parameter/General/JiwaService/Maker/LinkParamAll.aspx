<%@ Page language="c#" Codebehind="LinkParamAll.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.JiwaService.LinkParamAll" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>LinkParamAll</TITLE>
		<META name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<META name="CODE_LANGUAGE" Content="C#">
		<META name="vs_defaultClientScript" content="JavaScript">
		<META name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../../Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<CENTER>
				<TABLE width="100%" border="0">
					<TR>
						<TD align="left">
							<TABLE id="Table1">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>CUSTOMER-PROVIDER LINK</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="/SME/image/Back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../../Body.aspx"><IMG height="25" src="../../../../Image/MainMenu.jpg" width="106"></A>
							<A href="../../../../Logout.aspx" target="_top"><IMG src="../../../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">PARAMETER SETUP</TD>
					</TR>
					<TR>
						<TD class="td" style="HEIGHT: 7px" vAlign="top" width="50%">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1"><asp:label id="LBL_TXT_BCHTYPEID" runat="server">Customer Unit :</asp:label></TD>
									<TD class='A"TDBGColorValue"' width="50%"><asp:dropdownlist id="DDL_BCHTYPEID" runat="server" CssClass="Mandatory" Width="100%"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1"><asp:label id="LBL_TXT_GRPTYPEID" runat="server">Group Provider :</asp:label></TD>
									<TD class='A"TDBGColorValue"' width="50%"><asp:dropdownlist id="DDL_GRPTYPEID" runat="server" AutoPostBack="True" CssClass="Mandatory" Width="100%" onselectedindexchanged="DDL_GRPTYPEID_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1"><asp:label id="LBL_TXT_DEPTTYPEID" runat="server">Department Provider :</asp:label></TD>
									<TD class='A"TDBGColorValue"' width="50%"><asp:dropdownlist id="DDL_DEPTTYPEID" runat="server" Width="100%"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD><asp:label id="LBL_NO" runat="server" Visible="False"></asp:label></TD>
									<TD class="TDBGColorValue" width="50%"><asp:textbox id="TXT_NO" runat="server" Visible="False" Width="100%" BorderStyle="None"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="100%" colSpan="2">
							<asp:button id="BTN_SAVE" runat="server" CssClass="button1" Text="SAVE" Width="76px" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CLEAR" runat="server" CssClass="button1" Text="CLEAR" Width="76px" onclick="BTN_CLEAR_Click"></asp:button>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">EXISTING PARAMETER</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
							<asp:datagrid id="DGR_LINK" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SEQ" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH" HeaderText="Customer Unit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="G_CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="G_DESC" HeaderText="Group Provider">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="D_CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="D_DESC" HeaderText="Department Provider">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LNK_EDIT" runat="server" CommandName="edit">Edit</asp:LinkButton>
											<asp:LinkButton id="LNK_DELETE" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">REQUESTED PARAMETER</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
							<asp:datagrid id="DGR_REQUESTLINK" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SEQ" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH" HeaderText="Customer Unit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="G_CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="G_DESC" HeaderText="Group Provider">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="D_CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="D_DESC" HeaderText="Department Provider">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS_DESC" HeaderText="Pending Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LNK_EDIT_REQ" runat="server" CommandName="edit_req">Edit</asp:LinkButton>
											<asp:LinkButton id="LNK_DELETE_REQ" runat="server" CommandName="delete_req">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</TD>
					</TR>
				</TABLE>
			</CENTER>
		</FORM>
	</BODY>
</HTML>
