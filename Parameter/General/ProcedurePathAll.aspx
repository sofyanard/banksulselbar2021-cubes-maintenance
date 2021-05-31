<%@ Page language="c#" Codebehind="ProcedurePathAll.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.ProcedurePathAll" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProcedurePathAll</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder" width="50%" style="HEIGHT: 51px">
						<TABLE id="Table6">
							<TR>
								<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
										Common</B></TD>
							</TR>
						</TABLE>
					</TD>
					<TD style="HEIGHT: 51px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../Body.aspx">
							<IMG src="../../Image/MainMenu.jpg"></A>&nbsp;<A href="../../Logout.aspx" target="_top"><IMG src="../../Image/Logout.jpg"></A><A href="../../Logout.aspx" target="_top"></A>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Path Procedure (Track Decision) 
						Maker</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="tdLabel" style="WIDTH: 205px" align="right" width="205" bgColor="#d"><STRONG><FONT color="#330099">Module</FONT></STRONG></TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue">
									<asp:radiobuttonlist id="RBL_MODULE" runat="server" Width="248px" RepeatDirection="Horizontal" AutoPostBack="True" onselectedindexchanged="RBL_MODULE_SelectedIndexChanged">
										<asp:ListItem Value="20">Credit Card</asp:ListItem>
										<asp:ListItem Value="40">Consumer</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
						</TABLE>
						<asp:Table id="tbl" runat="server"></asp:Table>
						<TABLE id="Table8" style="HEIGHT: 26px" cellSpacing="0" cellPadding="0" width="992" align="right">
							<TR id="TR_TRACK_SEQ" runat="server">
								<TD class="TDBGColor1" style="WIDTH: 200px" align="right" width="200">Track Seq.</TD>
								<TD style="WIDTH: 3px">&nbsp;&nbsp;&nbsp; &nbsp;
								</TD>
								<TD class="TDBGColorValue">
									<asp:dropdownlist id="DDL_TRACK_SEQ" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label><asp:button id="BTN_SAVE" Width="70px" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL" Width="70px" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_STA" runat="server" Visible="False"></asp:label><asp:label id="LBL_FIELDS" runat="server" Visible="False"></asp:label><asp:label id="LBL_JMLPAR" runat="server" Visible="False"></asp:label><asp:label id="LBL_NILTEMP" runat="server" Visible="False"></asp:label><asp:label id="LBL_NUM" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" vAlign="top" align="left" width="50%" colSpan="2">Existing 
						Path Data</TD>
				</TR>
				<TR>
					<TD class="td" style="HEIGHT: 21px" vAlign="top" align="left" width="50%" colSpan="2"><asp:datagrid id="DGR_EXISTING_PATH" runat="server" Width="100%" PageSize="5" AllowPaging="True"
							HorizontalAlign="Center">
							<AlternatingItemStyle CssClass="TblALternating"></AlternatingItemStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="8%"></HeaderStyle>
									<ItemTemplate>
										<asp:LinkButton id="LinkButton2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="LinkButton1" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_REQUEST_PATH" runat="server" Width="100%" PageSize="5" AllowPaging="True"
							HorizontalAlign="Center">
							<AlternatingItemStyle CssClass="TblALternating"></AlternatingItemStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="8%"></HeaderStyle>
									<ItemTemplate>
										<asp:LinkButton id="LinkButton2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="LinkButton1" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
		</form>
	</body>
</HTML>
