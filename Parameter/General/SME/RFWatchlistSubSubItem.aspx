<%@ Page language="c#" Codebehind="RFWatchlistSubSubItem.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFWatchlistSubSubItem" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFWatchlistSubSubItem</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
							<TABLE id="Table6">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Setup</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../image/Back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
							<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdNoBorder" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" align="center" width="50%" colSpan="2">Watchlist 
							Sub Sub Item</TD>
					</TR>
					<asp:label id="lbl_CU_CUSTTYPEID" runat="server" Visible="False"></asp:label>
					<TR id="TR_PERSONAL" runat="server">
						<TD class="td" vAlign="top" width="50%" colSpan="2">
							<TABLE id="Table20" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="20%">Business Unit</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_BUSSUNIT" AutoPostBack="True" Runat="server" onselectedindexchanged="DDL_BUSSUNIT_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="20%" style="HEIGHT: 16px">Watchlist Item</TD>
									<TD style="WIDTH: 15px; HEIGHT: 16px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 16px"><asp:dropdownlist id="DDL_WATCHID" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_WATCHID_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="20%">Watchlist Sub Item</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SUBWATCHID" runat="server" AutoPostBack="True" CssClass="mandatory" onselectedindexchanged="DDL_SUBWATCHID_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="20%">Watchlist Sub Item Code</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_ID" runat="server" CssClass="mandatory" ReadOnly="True"></asp:textbox><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label><asp:label id="LBL_ACTIVE" runat="server" Visible="False"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 20px" vAlign="top">Watchlist Sub Item Desc</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_DESC" runat="server" CssClass="mandatory" width="100%" TextMode="MultiLine"
											MaxLength="200"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="20%">Weight</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_WEIGHT" runat="server" CssClass="mandatory" onkeypress="return digitsonly()"
											MaxLength="2" Columns="3"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="20%">Mandatory Watchlist</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue"><asp:checkbox id="CHK_ISMANDATORY" runat="server" AutoPostBack="True" Text="Check for Yes"></asp:checkbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="20%">Mandatory Review Kol BI</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue"><asp:checkbox id="CHK_RVWKOLBI" runat="server" AutoPostBack="True" Text="Check for Yes"></asp:checkbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="20%">Pilar BI</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PILARBI" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="20%">Conditional</TD>
									<TD style="WIDTH: 15px">:</TD>
									<TD class="TDBGColorValue"><asp:checkbox id="CHK_ISIGNORED" runat="server" AutoPostBack="True" Text="Check for Yes"></asp:checkbox></TD>
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
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2">Existing Data</TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2"><ASP:DATAGRID id="DGExisting" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="BUSSUNITID" HeaderText="Business Unit Code" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BUSSUNITDESC" HeaderText="Business Unit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="WATCHID" HeaderText="Watchlist Item Code" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="WATCHDESC" HeaderText="Watchlist Item">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SUBWATCHID" HeaderText="Watchlist Sub Item Code" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SUBWATCHDESC" HeaderText="Watchlist Sub Item">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SUBSUBWATCHID" HeaderText="Watchlist Sub Sub Item Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SUBSUBWATCHDESC" HeaderText="Watchlist Sub Sub Item Desc">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="WEIGHT" HeaderText="Weight">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISMANDATORY" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISMANDATORYDESC" HeaderText="Mandatory Watchlist">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RVWKOLBI" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RVWKOLBIDESC" HeaderText="Mandatory Review Kol BI">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PILARBI" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PILARBIDESC" HeaderText="Pilar BI">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISIGNORED" HeaderText="Conditional" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISIGNOREDDESC" HeaderText="Conditional">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE" HeaderText="ACTIVE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2">Requested Data</TD>
					</TR>
					<TR>
						<TD vAlign="top" width="50%" colSpan="2"><ASP:DATAGRID id="DGRequest" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="BUSSUNITID" HeaderText="Business Unit Code" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BUSSUNITDESC" HeaderText="Business Unit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="WATCHID" HeaderText="Watchlist Item Code" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="WATCHDESC" HeaderText="Watchlist Item">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SUBWATCHID" HeaderText="Watchlist Sub Item Code" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SUBWATCHDESC" HeaderText="Watchlist Sub Item">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SUBSUBWATCHID" HeaderText="Watchlist Sub Sub Item Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SUBSUBWATCHDESC" HeaderText="Watchlist Sub Sub Item Desc">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="WEIGHT" HeaderText="Weight">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISMANDATORY" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISMANDATORYDESC" HeaderText="Mandatory Watchlist">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RVWKOLBI" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RVWKOLBIDESC" HeaderText="Mandatory Review Kol BI">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PILARBI" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PILARBIDESC" HeaderText="Pilar BI">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISIGNORED" HeaderText="Conditional" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISIGNOREDDESC" HeaderText="Conditional">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_STADESC" HeaderText="Pending Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CH_STA"></asp:BoundColumn>
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
							</ASP:DATAGRID></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
