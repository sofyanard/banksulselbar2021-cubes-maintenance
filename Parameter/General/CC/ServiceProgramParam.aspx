<%@ Page language="c#" Codebehind="ServiceProgramParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.ServiceProgramParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServiceProgramParam</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!-- #include  file="../../../include/cek_all.html" -->
		<!-- #include file="../../../include/cek_mandatory.html" -->
		<!-- #include file="../../../include/cek_entries.html" --><LINK href="../../../style.css" type="text/css" rel="stylesheet">
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
									<TD style="HEIGHT: 41px" width="400">
										<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
											border="0">
											<TR>
												<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
														Credit Card General</B></TD>
											</TR>
										</TABLE>
									</TD>
									<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>&nbsp;<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A><A href="../../../Logout.aspx" target="_top"></A>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Service Program&nbsp;Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>
										<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px" width="202">Product Type&nbsp;ID</TD>
												<TD style="WIDTH: 7px"></TD>
												<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PROD_ID" runat="server" Width="104px" CssClass="mandatory"></asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px" width="202">Type Description</TD>
												<TD style="WIDTH: 7px"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="TXT_TYPE" onkeypress="return kutip_satu()" runat="server" Width="288px" MaxLength="50"></asp:textbox><asp:label id="LBL_SEQ" runat="server" Visible="False"></asp:label><asp:label id="LBL_SEQ_ID" runat="server" Visible="False"></asp:label><asp:label id="LBL_SAVE_MODE" runat="server" Visible="False">1</asp:label></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px" width="202">Default Limit</TD>
												<TD style="WIDTH: 7px"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_DEF_LIMIT" runat="server" Width="184px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px">Service&nbsp;Program&nbsp;ID</TD>
												<TD style="WIDTH: 7px"></TD>
												<TD class="TDBGColorValue"><asp:textbox id="TXT_SICS_ID" onkeypress="return kutip_satu()" runat="server" Width="184px" MaxLength="6"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px">Max. Limit</TD>
												<TD style="WIDTH: 7px"></TD>
												<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_MAX_LIMIT" runat="server" Width="184px"></asp:textbox></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							<P>Existing&nbsp;Data</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SEQ" HeaderText="Seq">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTID" HeaderText="Product Type ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TYPE_DESC" HeaderText="Type Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DEFAULT_LIMIT" HeaderText="Default Limit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SICS_ID" HeaderText="Service Program ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MAX_LIMIT" HeaderText="Max. Limit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
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
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SEQ" HeaderText="Seq">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTID" HeaderText="Product Type ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="TYPE_DESC" HeaderText="Type Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DEFAULT_LIMIT" HeaderText="Default Limit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SICS_ID" HeaderText="Service Program ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="MAX_LIMIT" HeaderText="Max. Limit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS1" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="TYPE_CODE" HeaderText="Type_code"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ORG" HeaderText="ORG"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="TYPE_EMBOSS" HeaderText="TYPE_EMBOSS"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CCNUM" HeaderText="CCNUM"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CCIURANYY" HeaderText="CCIURANYY"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CCIURANYYSP" HeaderText="CCIURANYYSP"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="REMARK" HeaderText="REMARK"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="EXPIRE_DATE" HeaderText="EXPIRE_DATE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SEQ_ID" HeaderText="SEQ_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="STATUS" HeaderText="STATUS"></asp:BoundColumn>
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
