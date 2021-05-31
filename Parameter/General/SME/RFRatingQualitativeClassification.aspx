<%@ Page language="c#" Codebehind="RFRatingQualitativeClassification.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFRatingQualitativeClassification" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFRatingQualitativeClassification</title>
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
					<TD class="tdHeader1" colSpan="2">Rating - Qualitative - Classification</TD>
				</TR>
				<TR>
					<TD class="td">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px">Qualitative</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_QUALITATIVEID" runat="server" AutoPostBack="True" CssClass="mandatory" onselectedindexchanged="DDL_QUALITATIVEID_SelectedIndexChanged"></asp:dropdownlist><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label><asp:label id="LBL_ACTIVE" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px">Classification Name</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_CLASSIFICATION" runat="server" CssClass="mandatory"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px">Score</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_SCORE" runat="server" CssClass="mandatory"
										MaxLength="15"></asp:textbox></TD>
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
							<TR>
								<TD class="TDBGColor1" width="20%">Downgrade Flag</TD>
								<TD style="WIDTH: 15px">:</TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_FLAG" runat="server" AutoPostBack="True" oncheckedchanged="CHK_FLAG_CheckedChanged"></asp:checkbox>&nbsp;Check 
									for Yes,&nbsp;&nbsp;to level :&nbsp;<asp:dropdownlist id="DDL_DOWNGRADELEVEL" Runat="server" Enabled="False"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Runat="server" Text="Save" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Runat="server" Text="Cancel" onclick="BTN_CANCEL_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Existing Data</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGExisting" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="QUALITATIVEID" HeaderText="ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="QUALITATIVEDESC" HeaderText="Qualitative">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CLASSIFICATION" HeaderText="Classification">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SCORE" HeaderText="Score">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MIN_RANGE" HeaderText="Min. Range">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MAX_RANGE" HeaderText="Max. Range">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DOWNGRADE_FLAG">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DOWNGRADE_FLAGDESC" HeaderText="Downgrade Flag">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DOWNGRADE_LEVEL">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DOWNGRADE_LEVELDESC" HeaderText="Downgrade to Level">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
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
					<TD class="tdHeader1" colSpan="2">Requested Data</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGRequest" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="QUALITATIVEID" HeaderText="ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="QUALITATIVEDESC" HeaderText="Qualitative">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CLASSIFICATION" HeaderText="Classification">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SCORE" HeaderText="Score">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MIN_RANGE" HeaderText="Min. Range">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MAX_RANGE" HeaderText="Max. Range">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DOWNGRADE_FLAG">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DOWNGRADE_FLAGDESC" HeaderText="Downgrade Flag">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DOWNGRADE_LEVEL">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DOWNGRADE_LEVELDESC" HeaderText="Downgrade to Level">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA"></asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
									<HeaderStyle Width="11%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										&nbsp;
										<asp:LinkButton id="lnk_RfDelete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
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
