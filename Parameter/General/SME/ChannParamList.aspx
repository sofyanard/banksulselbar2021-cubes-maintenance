<%@ Page language="c#" Codebehind="ChannParamList.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.ChannParamList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Channeling Parameter List Approval Maker</title>
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
					<TD class="tdNoBorder" align="right"><asp:imagebutton id="btn_BACK" runat="server" ImageUrl="../../../image/Back.jpg" onclick="btn_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG height="25" src="../../../Image/MainMenu.jpg" width="106"></A>
						<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Channeling Parameter List Maker</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" width="50%" colSpan="2">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="20%">Deskripsi</TD>
								<TD width="1%"></TD>
								<TD class="TDBGColorValue" width="79%"><asp:textbox id="txt_CH_PRM_NAME" runat="server" Columns="50" MaxLength="100" CssClass="mandatory"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Tabel Referensi</TD>
								<TD></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="ddl_CH_PRM_TABLE" runat="server" AutoPostBack="True" onselectedindexchanged="ddl_CH_PRM_TABLE_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Kolom Referensi</TD>
								<TD></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="ddl_CH_PRM_FIELD" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Reject Description</TD>
								<TD></TD>
								<TD class="TDBGColorValue"><asp:textbox id="txt_CH_PRM_REJECTDESC" runat="server" Columns="50" MaxLength="100" CssClass="mandatory"
										TextMode="MultiLine"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Parameter</TD>
								<TD></TD>
								<TD class="TDBGColorValue"><asp:radiobuttonlist id="rbl_CH_PRM_ISPARAMETER" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="rbl_CH_PRM_ISPARAMETER_SelectedIndexChanged">
										<asp:ListItem Value="1">Yes</asp:ListItem>
										<asp:ListItem Value="0" Selected="True">No</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Tabel Parameter</TD>
								<TD></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="ddl_CH_PRM_RFTABLE" runat="server"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
						<asp:radiobuttonlist id="rbl_CH_PRM_TERSEDIADATA" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
							Visible="False">
							<asp:ListItem Value="1" Selected="True">Tersedia Data</asp:ListItem>
							<asp:ListItem Value="0">Tidak</asp:ListItem>
						</asp:radiobuttonlist></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:label id="lbl_SAVEMODE" runat="server" Visible="False"></asp:label><asp:button id="btn_SAVE" CssClass="button1" Runat="server" Text="Save" onclick="btn_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="btn_CANCEL" CssClass="button1" Runat="server" Text="Cancel" onclick="btn_CANCEL_Click"></asp:button><asp:label id="lbl_CH_PRM_CODE" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>&nbsp;Current&nbsp;&nbsp;Chann&nbsp;ParamChanneling Parameter List Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="Datagrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="5"
							Width="100%">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="CH_PRM_CODE" HeaderText="CH_PRM_CODE"></asp:BoundColumn>
								<asp:BoundColumn DataField="CH_PRM_NAME" HeaderText="Deskripsi">
									<HeaderStyle Width="25%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_PRM_TABLE" HeaderText="Tabel Referensi">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_PRM_FIELD" HeaderText="Kolom Referensi">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_PRM_REJECTDESC" HeaderText="Reject Description">
									<HeaderStyle Width="25%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_PRM_ISPARAMETER" HeaderText="Parameter">
									<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_PRM_RFTABLE" HeaderText="Tabel Parameter">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_PRM_ACTIVE" HeaderText="ACTIVE">
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
					<TD class="tdHeader1" colSpan="2">Maker Request</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="5"
							Width="100%">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="CH_PRM_CODE" HeaderText="CH_PRM_CODE"></asp:BoundColumn>
								<asp:BoundColumn DataField="CH_PRM_NAME" HeaderText="Deskripsi">
									<HeaderStyle Width="25%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_PRM_TABLE" HeaderText="Tabel Referensi">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_PRM_FIELD" HeaderText="Kolom Referensi">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_PRM_REJECTDESC" HeaderText="Reject Description">
									<HeaderStyle Width="25%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_PRM_ISPARAMETER" HeaderText="Parameter">
									<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_PRM_RFTABLE" HeaderText="Tabel Parameter">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PENDINGSTATUS" HeaderText="Status">
									<HeaderStyle Width="6%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
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
		</form>
	</body>
</HTML>
