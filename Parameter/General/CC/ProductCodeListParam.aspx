<%@ Page language="c#" Codebehind="ProductCodeListParam.aspx.cs" AutoEventWireup="false" Inherits="CuBES_Maintenance.Parameter.General.CC.ProductListParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>General Credit Card Parameter</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<!-- #include  file="../../../include/cek_all.html" -->
		<!-- #include file="../../../include/cek_mandatory.html" --><LINK href="../../../style.css" type="text/css" rel="stylesheet">
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
									<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg"></asp:imagebutton><A href="../../../Body.aspx">
											<IMG src="../../../Image/MainMenu.jpg"></A>&nbsp;<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A><A href="../../../Logout.aspx" target="_top"></A>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter Product Code List Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>
										<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px" width="202">
													Product Code ID&nbsp;</TD>
												<TD style="WIDTH: 7px"></TD>
												<TD class="TDBGColorValue" style="WIDTH: 756px"><asp:textbox id="TXT_ID" onkeypress="return kutip_satu()" runat="server" Width="88px" MaxLength="8"
														Height="20px" CssClass="mandatory"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px" width="202">Name</TD>
												<TD style="WIDTH: 7px"></TD>
												<TD class="TDBGColorValue" style="WIDTH: 756px"><asp:textbox id="TXT_NAME" onkeypress="return kutip_satu()" runat="server" Width="100%" MaxLength="35"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="TDBGColor1" style="WIDTH: 202px"></TD>
												<TD style="WIDTH: 7px"></TD>
												<TD class="TDBGColorValue" style="WIDTH: 756px; HEIGHT: 29px">
													<asp:Label id="LBL_SAVE_MODE" runat="server" Visible="False">1</asp:Label>
													<asp:label id="LBL_CODE" runat="server" Visible="False"></asp:label></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server"></asp:button></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							<P>Existing&nbsp;Data</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								PageSize="5">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="PRODUCTID" HeaderText="Product Type ID">
										<HeaderStyle HorizontalAlign="Center" Width="200px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Name">
										<HeaderStyle HorizontalAlign="Center" Width="280px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CD_SIBS" HeaderText="Service Program ID">
										<HeaderStyle HorizontalAlign="Center" Width="160px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_FLAG" HeaderText="PR_FLAG"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle HorizontalAlign="Center" Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
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
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								PageSize="5">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="PRODUCTID" HeaderText="Product Type ID">
										<HeaderStyle HorizontalAlign="Center" Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Name">
										<HeaderStyle Width="280px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CD_SIBS" HeaderText="Service Program ID">
										<HeaderStyle HorizontalAlign="Center" Width="160px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS1" HeaderText="Status">
										<HeaderStyle HorizontalAlign="Center" Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle HorizontalAlign="Center" Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="DUMMIES" HeaderText="DUMMIES"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_TYPE" HeaderText="PR_TYPE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CCY" HeaderText="PR_CCY"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_TENOR" HeaderText="PR_TENOR"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_AMOUNT" HeaderText="PR_AMOUNT"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="NA_CODE" HeaderText="NA_CODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_PROVISI" HeaderText="PR_PROVISI"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_RATEYY" HeaderText="PR_RATEYY"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_RATEMM" HeaderText="PR_RATEMM"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWTAX1" HeaderText="PR_CWTAX1"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWTAX2" HeaderText="PR_CWTAX2"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWTAX3" HeaderText="PR_CWTAX3"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWTAX4" HeaderText="PR_CWTAX4"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWTAXIDR1" HeaderText="PR_CWTAXIDR1"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWTAXIDR2" HeaderText="PR_CWTAXIDR2"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PMIN" HeaderText="PMIN"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PMAX" HeaderText="PMAX"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWCALLIMIT" HeaderText="PR_CWCALLIMIT"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_IURANCC" HeaderText="PR_IURANCC"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_IURANCCSP" HeaderText="PR_IURANCCSP"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FLOOR_LIMIT" HeaderText="FLOOR_LIMIT"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CEILING_LIMIT" HeaderText="CEILING_LIMIT"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SEQ_ID" HeaderText="SEQ_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_FLAG" HeaderText="PR_FLAG"></asp:BoundColumn>
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
