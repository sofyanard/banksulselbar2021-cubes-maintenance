<%@ Page language="c#" Codebehind="RFRatingTotalAdjustment.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFRatingTotalAdjustment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFRatingTotalAdjustment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include  file="../../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px" cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Setup</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../image/Back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
									<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Rating - Total Adjustment</TD>
				</TR>
				<TR>
					<TD class="td">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px">ID</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_ID" runat="server" CssClass="mandatory"></asp:textbox><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label><asp:label id="LBL_ACTIVE" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px">Description</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_DESC" runat="server" CssClass="mandatory"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px">Minimal Range</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_MIN_RANGE" runat="server" CssClass="mandatory"
										MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px">Maximal Range</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_MAX_RANGE" runat="server" CssClass="mandatory"
										MaxLength="15"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" width="50%" colSpan="2"><asp:button id="BTN_SAVE" runat="server" CssClass="Button1" Text="Save" Width="100px" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL" runat="server" CssClass="Button1" Text="Cancel" Width="100px" onclick="BTN_CANCEL_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD vAlign="top" width="50%" colSpan="2"></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Existing Data</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGExisting" runat="server" AllowPaging="True" AutoGenerateColumns="False" Width="100%">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="ID" HeaderText="ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DESC" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MIN_RANGE" HeaderText="Min. Range">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MAX_RANGE" HeaderText="Max. Range">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ACTIVE" HeaderText="ACTIVE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
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
					<TD class="tdHeader1" colSpan="2">Requested Data</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGRequest" runat="server" AllowPaging="True" AutoGenerateColumns="False" Width="100%">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="ID" HeaderText="ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DESC" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MIN_RANGE" HeaderText="Min. Range">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MAX_RANGE" HeaderText="Max. Range">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS"></asp:BoundColumn>
								<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Pending Status">
									<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:ButtonColumn Text="Edit" CommandName="Edit">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
								</asp:ButtonColumn>
								<asp:ButtonColumn Text="Delete" CommandName="Delete">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:ButtonColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2">&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
