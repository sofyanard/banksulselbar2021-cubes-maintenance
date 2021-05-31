<%@ Page language="c#" Codebehind="TypeEmploymentParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.TypeEmploymentParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TypeEmploymentParam</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="Table2">
							</TABLE>
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD width="400" style="HEIGHT: 41px">
										<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
											border="0">
											<TR>
												<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Maintenance : 
														Credit Card General</B></TD>
											</TR>
										</TABLE>
									</TD>
									<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx">
											<IMG src="../../../Image/MainMenu.jpg"></A>&nbsp;<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A><A href="../../../Logout.aspx" target="_top"></A>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							Parameter&nbsp;Type of Employment&nbsp;Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="202" style="WIDTH: 202px">Code</TD>
									<TD style="WIDTH: 7px"></TD>
									<TD class="TDBGColorValue">
										<asp:TextBox id="TXT_JOB_TYPE_ID" onkeypress="return kutip_satu()" runat="server" Width="80px"
											MaxLength="5" CssClass="mandatory"></asp:TextBox>
										<asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label>
										<asp:label id="LBL_ACTIVE" runat="server" Visible="False"></asp:label>
										<asp:label id="LBL_ID" runat="server" Visible="False"></asp:label>
										<asp:label id="LBL_DESC" runat="server" Visible="False"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="202" style="WIDTH: 202px">
										Description</TD>
									<TD style="WIDTH: 7px"></TD>
									<TD class="TDBGColorValue">
										<asp:TextBox id="TXT_DESC" onkeypress="return kutip_satu()" runat="server" Width="360px" MaxLength="35"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 202px">
										Group</TD>
									<TD style="WIDTH: 7px"></TD>
									<TD class="TDBGColorValue">
										<asp:DropDownList id="DDL_GR_CODE" runat="server"></asp:DropDownList></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" Runat="server" Text="Save" CssClass="button1" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" Runat="server" Text="Cancel" CssClass="button1" onclick="BTN_CANCEL_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							<P>
								Existing&nbsp;Data</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="JOB_TYPE_ID" HeaderText="Code">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DES" HeaderText="Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="GR_CODE" HeaderText="Group">
										<HeaderStyle Width="20%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
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
						<TD class="tdHeader1" colSpan="2">Maker Request</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
							<asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="JOB_TYPE_ID" HeaderText="Code">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DES" HeaderText="Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="GR_CODE" HeaderText="Group">
										<HeaderStyle Width="20%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Status">
										<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDING_STATUS"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
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
			</center>
		</form>
	</body>
</HTML>
