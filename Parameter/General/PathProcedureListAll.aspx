<%@ Page language="c#" Codebehind="PathProcedureListAll.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.PathProcedureListAll" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PathProcedureListAll</title>
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
					<TD colSpan="2">
						<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD>
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													Common</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../Body.aspx"><IMG src="../../Image/MainMenu.jpg"></A><A href="../../Logout.aspx" target="_top"><IMG src="../../Image/Logout.jpg"></A>&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Path Procedure Maker</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" colSpan="2">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="tdLabel" style="WIDTH: 205px" align="right" width="205" bgColor="#d"><STRONG><FONT color="#330099">Module</FONT></STRONG></TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:radiobuttonlist id="RBL_MODULE" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
										Width="248px" onselectedindexchanged="RBL_MODULE_SelectedIndexChanged">
										<asp:ListItem Value="01" Selected="True">SME</asp:ListItem>
										<asp:ListItem Value="20">Credit Card</asp:ListItem>
										<asp:ListItem Value="40">Consumer</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
						</TABLE>
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR id="TR_CC" runat="server">
								<TD class="TDBGColor1" style="WIDTH: 202px" width="202">Track</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_TRACK_SEQ" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 202px">Current Track</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_TRACK_FROM" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 202px">Next Success Track</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_TRACK_NEXT" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 202px">Next Back Track</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_TRACK_BACK" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 202px">Next Fail Track</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_TRACK_FAIL" runat="server"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" Width="70px" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" Width="70px" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_STA" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Existing&nbsp;Data</P>
					</TD>
				</TR>
				<TR>
					<TD colSpan="2"><STRONG>Filter by :</STRONG>
						<asp:dropdownlist id="DDL_TRACK_SEQ_FILTER" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_TRACK_SEQ_FILTER_SelectedIndexChanged"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblALternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="TRACK_SEQ" HeaderText="TRACK_SEQ">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TRACK_DESC" HeaderText="Track">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="TRACK_FROM" HeaderText="Code">
									<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TR_FROM_DESC" HeaderText="Current Track">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="TRACK_NEXT" HeaderText="Code">
									<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TR_NEXT_DESC" HeaderText="Next Success Track">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="TRACK_BACK" HeaderText="Code">
									<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TR_BACK_DESC" HeaderText="Next Back Track">
									<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="TRACK_FAIL" HeaderText="Code">
									<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TR_FAIL_DESC" HeaderText="Next Fail Track">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete1" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblALternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="TRACK_SEQ" HeaderText="TRACK_SEQ">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TRACK_DESC" HeaderText="Track">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="TRACK_FROM" HeaderText="Code">
									<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TR_FROM_DESC" HeaderText="Current Track">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="TRACK_NEXT" HeaderText="Code">
									<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TR_NEXT_DESC" HeaderText="Next Success Track">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="TRACK_BACK" HeaderText="Code">
									<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TR_BACK_DESC" HeaderText="Next Back Track">
									<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="TRACK_FAIL" HeaderText="Code">
									<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TR_FAIL_DESC" HeaderText="Next Fail Track">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PENDING_STATUS" HeaderText="PENDING_STATUS">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Status">
									<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
