<%@ Page language="c#" Codebehind="ScoreParamAll.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.JiwaService.ScoreParamAll" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>ScoreParamAll</TITLE>
		<META content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>SCORE RULE</B></TD>
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
									<TD class="TDBGColor1" width="50%"><asp:label id="LBL_TXT_VARIABLE" runat="server">Variable :</asp:label></TD>
									<TD class='A"TDBGColorValue"' colspan="2"><asp:dropdownlist id="DDL_VARIABLE" Runat="server" CssClass="Mandatory" Width="100%"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="50%"><asp:label id="LBL_TXT_BOBOT" runat="server">Bobot :</asp:label></TD>
									<TD class="TDBGColorValue" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; WIDTH: 7.02%; BORDER-BOTTOM: gray 1px solid"
										valign="middle">
										<asp:textbox onkeypress="return signeddigitsonly()" id="TXT_BOBOT" runat="server" BorderStyle="None"
											Width="100%"></asp:textbox>
									</TD>
									<TD>&nbsp; (%)</TD>
								</TR>
								<TR>
									<TD><asp:label id="LBL_SCOREID" runat="server" Visible="False"></asp:label></TD>
									<TD colspan="2"><asp:label id="LBL_CODE" runat="server" Visible="False"></asp:label></TD>
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
							<asp:datagrid id="DGR_SCORE" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="CODE" HeaderText="Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VARIABLE_ID" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VARIABLE" HeaderText="Variable">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BOBOT" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BOBOT_DESC" HeaderText="Bobot">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
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
							<asp:datagrid id="DGR_REQUESTSCORE" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="CODE" HeaderText="Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VARIABLE_ID" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VARIABLE" HeaderText="Variable">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BOBOT" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BOBOT_DESC" HeaderText="Bobot">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
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
