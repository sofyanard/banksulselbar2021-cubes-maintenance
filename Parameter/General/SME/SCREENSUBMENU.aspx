<%@ Page language="c#" Codebehind="SCREENSUBMENU.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.SCREENSUBMENU" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SCREEN SUB MENU</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
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
						<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../image/Back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG height="25" src="../../../Image/MainMenu.jpg" width="106"></A>
							<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Financial Statement&nbsp;Sub Menu 
							Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%" colSpan="2">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1">Program</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PROGRAMID" runat="server" Width="300px" CssClass="mandatory2"></asp:dropdownlist><asp:button id="BTN_VIEW" runat="server" Width="80px" Text="View" onclick="BTN_VIEW_Click"></asp:button>&nbsp;(pilih 
										Program dulu untuk View)</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:checkbox id="CHK_PROG_BPR" runat="server" Text="Program BPR ?"></asp:checkbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 15px">Menu</TD>
									<TD style="WIDTH: 15px; HEIGHT: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_MENUCODE" runat="server" Width="300px" CssClass="mandatory2"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" Width="100px" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button><asp:button id="BTN_CANCEL" Width="100px" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							<P>Current&nbsp;&nbsp;Financial Statement Sub Menu</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_CURRENT" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								PageSize="18">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="MENUCODE" HeaderText="MENUCODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BUSSUNITID" HeaderText="BUSSUNITID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PROGRAMID" HeaderText="PROGRAMID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PROGRAMID_SEQ" HeaderText="PROGRAMID_SEQ"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SM_MENUDISPLAY" HeaderText="Menu Display">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SM_LINKNAME" HeaderText="SM_LINKNAME"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="LG_CODE" HeaderText="LG_CODE"></asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LG_CODE_DESC" HeaderText="Program BPR (Y/N)">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MENUDISPLAY" HeaderText="Menu">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit1" runat="server" Visible="False" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete1" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Pending Financial Statement Sub Menu</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
							<asp:datagrid id="DGR_PENDING" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								PageSize="18">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="MENUCODE" HeaderText="MENUCODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BUSSUNITID" HeaderText="BUSSUNITID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PROGRAMID" HeaderText="PROGRAMID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PROGRAMID_SEQ" HeaderText="PROGRAMID_SEQ"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SM_MENUDISPLAY" HeaderText="Menu Display">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SM_LINKNAME" HeaderText="SM_LINKNAME"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="LG_CODE" HeaderText="LG_CODE"></asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LG_CODE_DESC" HeaderText="Program BPR (Y/N)">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MENUDISPLAY" HeaderText="Menu">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS" HeaderText="PENDINGSTATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDINGSTATUSDESC" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit1" runat="server" Visible="False" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete1" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
