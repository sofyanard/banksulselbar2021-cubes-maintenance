<%@ Page language="c#" Codebehind="TBORefCOlParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.TBORefCOlParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Consumer General Parameter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_entries.html" -->
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													General Maker</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
									<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter TBO Reference Collateral Maker</TD>
				</TR>
				<TR>
					<TD class="td">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="200">Collateral Type</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_COL_TYPE" runat="server" CssClass="mandatory"></asp:dropdownlist><asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="200">Flag</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_FLAG" runat="server" CssClass="mandatory"></asp:dropdownlist><asp:label id="LBL_SAVE" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Document</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_DOC" runat="server" CssClass="mandatory"></asp:dropdownlist>
									<asp:label id="LBL_NB" runat="server" Visible="False">1</asp:label>
									<asp:label id="LBL_SEQ" runat="server" Visible="False"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Runat="server" Text="Save" Width="70px" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Runat="server" Text="Cancel" Width="70px" onclick="BTN_CANCEL_Click"></asp:button>&nbsp;&nbsp;
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Current TBO Reference Collateral Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2">
						<asp:datagrid id="DG1" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="20">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="DOC_SEQ" HeaderText="ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="RF_DESC" HeaderText="Collateral Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FLAG_STAT" HeaderText="Flag">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TBO_DESC" HeaderText="Document">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="FLAG">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DOC_ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="COL_TYPE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Maker Request</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2">
						<asp:datagrid id="DG2" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="DOC_SEQ" HeaderText="ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="COLTY" HeaderText="Collateral Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FG" HeaderText="Flag">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TBO_DESC" HeaderText="Document">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="STATUS" HeaderText="Pending Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="link_edit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="link_del" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="FLAG">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="COL_TYPE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DOC_ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2">&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
