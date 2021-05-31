<%@ Page language="c#" Codebehind="RFCAFinstatement.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFCAFinstatement" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFCAFinstatement</title>
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
						<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Program dari Jenis Nasabah Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%" colSpan="2">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 15px">Sequence</TD>
									<TD style="WIDTH: 15px; HEIGHT: 15px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 15px"><asp:textbox id="TXT_SEQ" runat="server" ReadOnly="True" CssClass="mandatory2" Width="128px"></asp:textbox>&nbsp;(klik 
										"new" saat input data baru)</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">ProgramID</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PROGRAMID" runat="server" CssClass="mandatory2" Width="270px"></asp:dropdownlist>&nbsp;
										<asp:Button id="BTN_VIEW" runat="server" Width="53px" Text="View" onclick="BTN_VIEW_Click"></asp:Button>&nbsp;&nbsp;(pilih 
										Program dulu)</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">AreaID</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_AREAID" runat="server" CssClass="mandatory2" Width="270px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 21px">NasabahID</TD>
									<TD style="WIDTH: 15px; HEIGHT: 21px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:dropdownlist id="DDL_NASABAHID" runat="server" CssClass="mandatory2" Width="270px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 15px">LG Code</TD>
									<TD style="WIDTH: 15px; HEIGHT: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_LG_CODE" runat="server" Width="270px"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_NEW" runat="server" CssClass="button1" Text="New" onclick="BTN_NEW_Click"></asp:button>&nbsp;&nbsp;&nbsp;
							<asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							<P>Current&nbsp;&nbsp;Program dari Jenis Nasabah&nbsp;Table</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="Datagrid1" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								PageSize="18">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="SEQ" HeaderText="Sequence">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ProgramID" HeaderText="Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AreaID" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="NasabahID" HeaderText="Nasabah">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="LG_CODE" HeaderText="LG Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="PROGRAMDESC">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AREANAME" HeaderText="AREANAME">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NASABAHDESC" HeaderText="NASABAHDESC">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LG_DESC" HeaderText="LG_DESC">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
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
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="Datagrid2" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								PageSize="18">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="SEQ" HeaderText="Sequence">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ProgramID" HeaderText="Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AreaID" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="NasabahID" HeaderText="Nasabah">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="LG_CODE" HeaderText="LG Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS" HeaderText="HiddenStatus"></asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="PROGRAMDESC">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AREANAME" HeaderText="AREANAME">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NASABAHDESC" HeaderText="NASABAHDESC">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LG_DESC" HeaderText="LG_DESC">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PENDINGSTATUSDESC" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="Linkbutton1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
