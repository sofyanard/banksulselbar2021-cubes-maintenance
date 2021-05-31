<%@ Page language="c#" Codebehind="DateParamAll.aspx.cs" AutoEventWireup="false" Inherits="CuBES_Maintenance.Parameter.General.JiwaService.DateParamAll" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>DateParamAll</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>CUT OFF DATE</B></TD>
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
					<TR>
						<TD class="td" vAlign="top" width="60%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="167%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 246px; HEIGHT: 15px">Function</TD>
									<TD style="WIDTH: 15px; HEIGHT: 11px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 11px"><asp:dropdownlist id="DDL_FUNCTION" Runat="server" CssClass="Mandatory"></asp:dropdownlist>&nbsp;</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 246px; HEIGHT: 15px">Begin Date</TD>
									<TD style="WIDTH: 15px; HEIGHT: 11px"></TD>
									<TD class="TDBGColorValue">
										<asp:textbox onkeypress="return numbersonly()" id="TXT_TGL1" runat="server" MaxLength="2" Columns="2"
											CssClass="Mandatory"></asp:textbox>
										<asp:dropdownlist id="DDL_BLN1" runat="server" CssClass="Mandatory"></asp:dropdownlist>
										<asp:textbox onkeypress="return numbersonly()" id="TXT_THN1" runat="server" MaxLength="4" Columns="4"
											CssClass="Mandatory"></asp:textbox>
									</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 246px; HEIGHT: 15px">Cut Date</TD>
									<TD style="WIDTH: 15px; HEIGHT: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:textbox onkeypress="return numbersonly()" id="TXT_TGL2" runat="server" MaxLength="2" Columns="2"
											CssClass="Mandatory"></asp:textbox>
										<asp:dropdownlist id="DDL_BLN2" runat="server" CssClass="Mandatory"></asp:dropdownlist>
										<asp:textbox onkeypress="return numbersonly()" id="TXT_THN2" runat="server" MaxLength="4" Columns="4"
											CssClass="Mandatory"></asp:textbox>
									</TD>
									<TD><asp:label id="LBL_NO" runat="server" Visible="False"></asp:label></TD>
									<TD><asp:label id="LBL_ID" runat="server" Visible="False"></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="100%" colSpan="2">
							<asp:button id="BTN_SAVE" runat="server" CssClass="button1" Text="SAVE"></asp:button>
							<asp:button id="BTN_CLEAR" runat="server" CssClass="button1" Text="CLEAR"></asp:button>
						</TD>
					</TR>
					<tr>
					</tr>
					<tr>
					</tr>
					<TR>
						<TD class="tdHeader1" colSpan="2">EXISTING PARAMTER</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2" style="HEIGHT: 220px">
							<asp:datagrid id="DGR_DATE" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SEQ" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FUNCTION" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FUNCTIONDESC" HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BEGIN_DATE" HeaderText="Tanggal Awal">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CUT_DATE" HeaderText="Tanggal Akhir">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
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
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
							<asp:datagrid id="DGR_REQUESTDATE" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SEQ" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FUNCTION" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FUNCTIONDESC" HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BEGIN_DATE" HeaderText="Tanggal Awal">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CUT_DATE" HeaderText="Tanggal Akhir">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
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
				</table>
			</CENTER>
		</form>
	</body>
</HTML>
