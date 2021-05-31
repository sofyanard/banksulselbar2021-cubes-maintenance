<%@ Page language="c#" Codebehind="RFInsuranceType.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFInsuranceType" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFInsuranceType</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include  file="../../../include/cek_all.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
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
						<TD class="tdHeader1" colSpan="2">Parameter Insurance Type Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" colSpan="2">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="200">Insurance Code</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_IT_ID" onkeyup="return kutip_satu()" runat="server" CssClass="mandatory"
											Columns="10" MaxLength="10"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Insurance Type Description</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_IT_DESC" onkeyup="return kutip_satu()" runat="server" Columns="30" MaxLength="50"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Group</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_GRIT_ID" runat="server"></asp:dropdownlist><br>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Runat="server" Text="Save" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Runat="server" Text="Cancel" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Current Insurance Type Table</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_CURRENT" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<Columns>
									<asp:BoundColumn DataField="IT_ID" HeaderText="Insurance Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IT_DESC" HeaderText="Insurance Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="GRIT_ID" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="GRIT_DESC" HeaderText="Group Insurance">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
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
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_MAKER" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True" onselectedindexchanged="DGR_MAKER_SelectedIndexChanged">
								<Columns>
									<asp:BoundColumn DataField="IT_ID" HeaderText="Insurance Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="IT_DESC" HeaderText="Insurance Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="GRIT_ID" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="GRIT_DESC" HeaderText="Group Insurance">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PENDINGSTATUS" Visible="False"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDINGDESC" HeaderText="Request Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
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
