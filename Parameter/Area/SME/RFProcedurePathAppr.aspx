<%@ Page language="c#" Codebehind="RFProcedurePathAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Area.SME.RFProcedurePathAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFProcedurePathAppr</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder" style="HEIGHT: 47px">
							<TABLE id="Table2">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center">
										<B>Parameter Maintenance : Common</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right" style="HEIGHT: 47px"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
							<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter Procedure Path Approval</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
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
							<asp:datagrid id="DGR_APPR" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="15"
								AllowPaging="True" ShowFooter="True">
								<AlternatingItemStyle CssClass="TblALternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDINGDESC" HeaderText="Request Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AREAID"></asp:BoundColumn>
									<asp:BoundColumn DataField="AREANAME" HeaderText="Area">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PROGRAMID"></asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PRODUCTID"></asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTDESC" HeaderText="Product">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PP_TRACKFROM"></asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKFROMNAME" HeaderText="Track From">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PP_TRACKNEXT"></asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKNEXTNAME" HeaderText="Track Next">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PP_TRACKBACK"></asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKBACKNAME" HeaderText="Track Back">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PP_TRACKFAIL"></asp:BoundColumn>
									<asp:BoundColumn DataField="PP_TRACKFAILNAME" HeaderText="Track Fail">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Approve" runat="server" GroupName="rdg_Decision"></asp:RadioButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="BTN_All_Approve" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Reject" runat="server" GroupName="rdg_Decision"></asp:RadioButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="BTN_All_Reject" runat="server" CommandName="allRejc">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Pending" runat="server" GroupName="rdg_Decision" Checked="True"></asp:RadioButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="BTN_All_Pending" runat="server" CommandName="allPend">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SUBMIT" Runat="server" Text="Submit" CssClass="button1" onclick="BTN_SUBMIT_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
