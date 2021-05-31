<%@ Page language="c#" Codebehind="PathProcedureListAllAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.PathProcedureListAllAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PathProcedureListAllAppr</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder" style="HEIGHT: 44px"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
						<TABLE id="Table2">
						</TABLE>
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD width="400" style="HEIGHT: 41px">
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Maintenance : 
													Common</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../Body.aspx">
										<IMG src="../../Image/MainMenu.jpg"></A>&nbsp;<A href="../../Logout.aspx" target="_top"><IMG src="../../Image/Logout.jpg"></A><A href="../../Logout.aspx" target="_top"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						Parameter Path Procedure Approval</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="left" width="50%" colSpan="2" class="TD">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="tdLabel" style="WIDTH: 205px" align="right" width="205" bgColor="#d"><STRONG><FONT color="#330099">Module</FONT></STRONG></TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue">
									<asp:radiobuttonlist id="RBL_MODULE" runat="server" Width="248px" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="RBL_MODULE_SelectedIndexChanged">
										<asp:ListItem Value="01" Selected="True">SME</asp:ListItem>
										<asp:ListItem Value="20">Credit Card</asp:ListItem>
										<asp:ListItem Value="40">Consumer</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
						</TABLE>
						<asp:datagrid id="DGR_APPR" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							ShowFooter="True" PageSize="20">
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
								<asp:TemplateColumn HeaderText="Approve">
									<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rdo_Approve" runat="server" GroupName="approval_status"></asp:RadioButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:LinkButton id="Linkbutton1" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Reject">
									<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rdo_Reject" runat="server" GroupName="approval_status"></asp:RadioButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:LinkButton id="Linkbutton2" runat="server" CommandName="allReject">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pending">
									<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rdo_Pending" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:LinkButton id="Linkbutton3" runat="server" CommandName="allPend">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2">
						<asp:button id="BTN_SUBMIT" CssClass="button1" Text="Submit" Runat="server" onclick="BTN_SUBMIT_Click"></asp:button>&nbsp;&nbsp;
						<asp:label id="LBL_STA" runat="server" Visible="False"></asp:label></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
