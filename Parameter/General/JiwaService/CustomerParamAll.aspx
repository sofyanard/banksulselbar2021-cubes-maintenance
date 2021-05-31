<%@ Page language="c#" Codebehind="CustomerParamAll.aspx.cs" AutoEventWireup="false" Inherits="CuBES_Maintenance.Parameter.General.JiwaService.CustomerParamAll" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CustomerParamAll</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<CENTER>
				<table width="100%" border="0">
					<tr>
						<td align="left">
							<TABLE id="Table1">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>PERTANYAAN INTERNAL 
											CUSTOMER</B></TD>
								</TR>
							</TABLE>
						</td>
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="/SME/image/Back.jpg"></asp:imagebutton><A href="../../../Body.aspx"><IMG height="25" src="../../../Image/MainMenu.jpg" width="106"></A>
							<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
						</TD>
					</tr>
					<TR>
						<TD class="tdHeader1" colSpan="2">PARAMETER SETUP</TD>
					</TR>
					<tr id="TR_GROUP" runat="server">
						<td class="td" vAlign="top" width="50%">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; BORDER-BOTTOM: gray 1px solid">Group 
										Provider :
									</TD>
									<TD class="TDBGColorValue" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; WIDTH: 43px; BORDER-BOTTOM: gray 1px solid"><asp:dropdownlist id="DDL_GRPTYPEID" runat="server" AutoPostBack="True" CssClass="Mandatory"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; BORDER-BOTTOM: gray 1px solid">Department 
										Provider :
									</TD>
									<TD class="TDBGColorValue" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; WIDTH: 43px; BORDER-BOTTOM: gray 1px solid"><asp:dropdownlist id="DDL_DEPTTYPEID" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD><asp:label id="LBL_NO" runat="server" Visible="False"></asp:label></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_NO" runat="server" Visible="False" Width="352px" BorderStyle="None"></asp:textbox></TD>
								</TR>
							</TABLE>
						</td>
						<td class="td" vAlign="top" width="50%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; BORDER-BOTTOM: gray 1px solid">Pertanyaan 
										:
									</TD>
									<TD class="TDBGColorValue" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; WIDTH: 43px; BORDER-BOTTOM: gray 1px solid"><asp:textbox id="TEXT_QUESTION" runat="server" Width="400px" CssClass="Mandatory" Height="38px"
											TextMode="MultiLine"></asp:textbox></TD>
								</TR>
							</TABLE>
						</td>
					</tr>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="100%" colSpan="2"><asp:button id="BTN_SAVE" runat="server" Text="SAVE" CssClass="button1"></asp:button><asp:button id="BTN_CLEAR" runat="server" Text="CLEAR" CssClass="button1"></asp:button></TD>
					</TR>
					<tr>
					</tr>
					<tr>
					</tr>
					<TR>
						<TD class="tdHeader1" colSpan="2">EXISTING PARAMETER</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_QUEST" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="NO" HeaderText="No">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
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
									<asp:BoundColumn DataField="D_DESC" HeaderText="Dept. Provider">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="QUESTION" HeaderText="Pertanyaan">
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
					<tr>
					</tr>
					<tr>
					</tr>
					<TR>
						<TD class="tdHeader1" colSpan="2">REQUESTED PARAMETER</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_REQUESTQUEST" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="NO" HeaderText="No">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
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
									<asp:BoundColumn DataField="D_DESC" HeaderText="Dept. Provider">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="QUESTION" HeaderText="Pertanyaan">
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
							</asp:datagrid></TD>
					</TR>
				</table>
			</CENTER>
		</form>
	</body>
</HTML>
