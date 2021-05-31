<%@ Page language="c#" Codebehind="CUBESSimModel.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Consumer.CUBESSimModel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CUBESSimModel</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include  file="../../../include/cek_entries.html" -->
		<!-- #include  file="../../../include/cek_mandatory.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px" cellSpacing="0" cellPadding="0" width="408" border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Model Maintenance : Limit 
													List Simulasi Maker</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="tdNoBorder" align="right"><A href="../../ScoringParamAll.aspx?mc=9902040301&amp;moduleID=40"><IMG src="../../../Image/Back.jpg" border="0"></A>
									<A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A> <A href="../../../Logout.aspx" target="_top">
										<IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Model Limit List Simulasi Maker</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="2">
						<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 10px">PRODUCT</TD>
								<TD style="WIDTH: 7px; HEIGHT: 10px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 10px"><asp:dropdownlist id="DDL_PRODUCT" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_PRODUCT_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 17px">PROGRAM</TD>
								<TD style="WIDTH: 7px; HEIGHT: 17px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:dropdownlist id="DDL_PROGRAM" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_PROGRAM_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 17px">JOBTYPE</TD>
								<TD style="WIDTH: 7px; HEIGHT: 17px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:dropdownlist id="DDL_JOBTYPE" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_JOBTYPE_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 250px; HEIGHT: 18px">Template ID</TD>
								<TD style="WIDTH: 7px; HEIGHT: 18px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 18px"><asp:dropdownlist id="DDL_TPLID" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_TPLID_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Width="70px" Runat="server" Text="Save" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Runat="server" Text="Cancel" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Existing Data</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="PROGRAMID"></asp:BoundColumn>
								<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="Program">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRODUCTID"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRODUCTDESC" HeaderText="Product">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="JOBTYPEID"></asp:BoundColumn>
								<asp:BoundColumn DataField="JOBTYPEDESC" HeaderText="Job Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="TEMPLATEID"></asp:BoundColumn>
								<asp:BoundColumn DataField="TEMPLATEDESC" HeaderText="Template">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit3" runat="server" CommandName="Edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete3" runat="server" CommandName="Delete">Delete</asp:LinkButton>
										<asp:Label id="lbl_Status" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False"></asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Maker Request</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" PageSize="5" AutoGenerateColumns="False"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="PROGRAMID"></asp:BoundColumn>
								<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="Program">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRODUCTID"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRODUCTDESC" HeaderText="Product">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="JOBTYPEID"></asp:BoundColumn>
								<asp:BoundColumn DataField="JOBTYPEDESC" HeaderText="Job Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="TEMPLATEID"></asp:BoundColumn>
								<asp:BoundColumn DataField="TEMPLATEDESC" HeaderText="Template">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit4" runat="server" CommandName="Edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete4" runat="server" CommandName="Delete">Delete</asp:LinkButton>
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
