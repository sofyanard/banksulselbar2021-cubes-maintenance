<%@ Page language="c#" Codebehind="NotaryDocParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.NotaryDocParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NotaryDocParam</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder" style="WIDTH: 461px">
						<TABLE id="Table2">
							<TR>
								<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
										General Maker</B></TD>
							</TR>
						</TABLE>
					</TD>
					<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
						<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter Notary Document Maker</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" colspan="2">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="200" style="HEIGHT: 8px">Program</TD>
								<TD style="WIDTH: 7px; HEIGHT: 8px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 8px"><asp:dropdownlist id="DDL_PROGRAM" runat="server" AutoPostBack="True" CssClass="mandatory" onselectedindexchanged="DDL_PROGRAM_SelectedIndexChanged"></asp:dropdownlist><asp:label id="LBL_PROGRAM" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="200" style="HEIGHT: 4px">Product</TD>
								<TD style="WIDTH: 7px; HEIGHT: 4px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 4px"><asp:dropdownlist id="DDL_PRODUCT" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_PRODUCT_SelectedIndexChanged"></asp:dropdownlist><asp:label id="LBL_PRODUCT" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="HEIGHT: 23px">Type</TD>
								<TD style="WIDTH: 7px; HEIGHT: 23px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 23px"><asp:dropdownlist id="DDL_TYPE" runat="server" AutoPostBack="True" CssClass="mandatory" onselectedindexchanged="DDL_TYPE_SelectedIndexChanged"></asp:dropdownlist><asp:label id="LBL_TYPE" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Document</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_DOC" runat="server" CssClass="mandatory"></asp:dropdownlist><asp:label id="LBL_DOC" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Mandatory</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CXB_MANDATORY" runat="server"></asp:checkbox></TD>
							</TR>
						</TABLE>
						<STRONG></STRONG>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label><asp:label id="LBL_ACTIVE" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Current&nbsp;Notary Document Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" colSpan="2"><asp:datagrid id="DGExisting" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="PR_CODE"></asp:BoundColumn>
								<asp:BoundColumn DataField="PR_DESC" HeaderText="Program">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRODUCTID"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Product">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DNM_CODE"></asp:BoundColumn>
								<asp:BoundColumn DataField="DNM_DESC" HeaderText="Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DND_CODE"></asp:BoundColumn>
								<asp:BoundColumn DataField="DND_DESC" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DN_MANDATORY"></asp:BoundColumn>
								<asp:BoundColumn DataField="MANDATORY" HeaderText="Mandatory">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
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
					<TD class="td" vAlign="top" colSpan="2"><asp:datagrid id="DGRequest" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="PR_DESC" HeaderText="Program">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Product">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DNM_DESC" HeaderText="Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DND_DESC" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MANDATORY1" HeaderText="Mandatory">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_CODE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRODUCTID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DNM_CODE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DND_CODE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DN_MANDATORY"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader" Width="10%"></HeaderStyle>
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
					<TD class="TDBGColor2" vAlign="top" colSpan="2">&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
