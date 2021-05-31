<%@ Page language="c#" Codebehind="RFBRANCH.aspx.cs" AutoEventWireup="True" Inherits="SME.Maintenance.Parameters.General.RFBRANCH" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFBRANCH</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
							<TABLE id="Table2">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
											General</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="/SME/Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="/SME/Image/MainMenu.jpg"></A>
							<A href="../../../Logout.aspx" target="_top"><IMG src="/SME/Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter Branch Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="200">Branch&nbsp;Code</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_BRANCH_CODE"   onkeypress="return kutip_satu()" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Branch&nbsp;Name</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_BRANCH_NAME"   onkeypress="return kutip_satu()" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">CBC&nbsp;Code</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CBC_CODE"   onkeypress="return kutip_satu()" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">City</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CITYID"   onkeypress="return kutip_satu()" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Zipcode</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_BR_ZIPCODE"   onkeypress="return kutip_satu()" runat="server"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1">Address</TD>
									<TD width="17"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_BR_ADDR"   onkeypress="return kutip_satu()" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Branch Area</TD>
									<TD width="17"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_BR_BRANCHAREA"  onkeypress="return kutip_satu()"  runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Phone / Fax</TD>
									<TD width="17"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_BR_PHNFAX"  onkeypress="return kutip_satu()"  runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">CCO Branch</TD>
									<TD width="17"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_BR_CCOBRANCH"  onkeypress="return kutip_satu()"  runat="server"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" Runat="server" Text="Save" CssClass="button1" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" Runat="server" Text="Cancel" CssClass="button1" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							<P>Current Branch Table</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="Datagrid1" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<Columns>
									<asp:BoundColumn DataField="BRANCH_CODE" HeaderText="Branch Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_NAME" HeaderText="Branch Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CBC_CODE" HeaderText="CBC Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CITYNAME" HeaderText="City">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ZIPCODE" HeaderText="Zipcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ADDR" HeaderText="Address">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_BRANCHAREA" HeaderText="Branch Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_PHNFAX" HeaderText="Phone / Fax">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_CCOBRANCH" HeaderText="CCO Branch">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CITYID"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
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
						<TD class="tdHeader1" colSpan="2">Maker Request</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DataGrid2" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<Columns>
									<asp:BoundColumn DataField="BRANCH_CODE" HeaderText="Branch Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_NAME" HeaderText="Branch Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CBC_CODE" HeaderText="CBC Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CITYNAME" HeaderText="City">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ZIPCODE" HeaderText="Zipcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ADDR" HeaderText="Address">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_BRANCHAREA" HeaderText="Branch Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_PHNFAX" HeaderText="Phone / Fax">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_CCOBRANCH" HeaderText="CCO Branch">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CITYID"></asp:BoundColumn>
									<asp:BoundColumn DataField="STATUSDESC" HeaderText="Request Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RqEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RqDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
			</center>
		</form>
	</body>
</HTML>
