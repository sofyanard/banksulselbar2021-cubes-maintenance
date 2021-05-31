<%@ Page language="c#" Codebehind="ProblemMaker.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.ProblemMaker" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProblemMaker</title>
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
							<TABLE id="Table31">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>PROBLEM TYPE MAKER</B></TD>
								</TR>
							</TABLE>
						</td>
						<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../image/Back.jpg"></asp:imagebutton><A href="../../../Body.aspx"><IMG height="25" src="../../../Image/MainMenu.jpg" width="106"></A>
							<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
						</TD>
					</tr>
					<TR>
						<TD class="tdHeader1" colSpan="2">PROBLEM TYPE PARAMETER</TD>
					</TR>
					<tr id="TR_CATATAN" runat="server">
						<td class="td" vAlign="top" width="60%">
							<TABLE id="Table61" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1">Code</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue" style="WIDTH: 43px"><asp:textbox id="TXT_CODE" runat="server" BorderStyle="None" ReadOnly="True" Width="352px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Description</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue" style="WIDTH: 43px"><asp:textbox id="TXT_PROBNAME" runat="server" BorderStyle="None" Width="352px" CssClass="mandatory"></asp:textbox></TD>
								</TR>
							</TABLE>
						</td>
					</tr>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="100%" colSpan="2"><asp:button id="BTN_SAVE" runat="server" CssClass="button1" Text="SAVE" onclick="BTN_SAVE_Click"></asp:button><asp:button id="BTN_CLEAR" runat="server" CssClass="button1" Text="CLEAR" onclick="BTN_CLEAR_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
					</TR>
					<tr>
					</tr>
					<tr>
					</tr>
					<TR>
						<TD class="tdHeader1" colSpan="2">CURRENT PROBLEM TYPE</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="Dgr_CurrStage" runat="server" Width="100%" PageSize="18" AutoGenerateColumns="False"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="PROB_CD" HeaderText="CODE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PROB_DESC" HeaderText="STAGE NAME">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LNK_EDIT" runat="server" CommandName="edit">Edit</asp:LinkButton>
											<asp:LinkButton id="LNK_DELETE" runat="server" CommandName="delete">Delete</asp:LinkButton>
											<asp:LinkButton id="LNK_UNDELETE" runat="server" CommandName="undelete">Undelete</asp:LinkButton>
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
						<TD class="tdHeader1" colSpan="2">REQUEST PROBLEM TYPE</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="Dgr_RequestStage" runat="server" Width="100%" PageSize="18" AutoGenerateColumns="False"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="True" DataField="PROB_CD" HeaderText="CODE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="True" DataField="PROB_DESC" HeaderText="STAGE NAME">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PENDINGSTATUSDESC" HeaderText="STATUS">
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
