<%@ Page language="c#" Codebehind="AreaParam.aspx.cs" AutoEventWireup="True" Inherits="SME.MaintainanceAll.ParametersAll.GeneralAll.Consumer.AreaParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Area Parameter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table3" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
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
					<TD class="tdHeader1" colSpan="2">Parameter 
						Area Maker</TD>
				</TR>
				<TR runat="server">
					<TD class="td" colSpan="2">
						<TABLE id="Table20" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 200px">Code</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_CODE" runat="server" MaxLength="4" CssClass="mandatory" Columns="7"></asp:textbox>
									<asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 200px">Area</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_AREA1" runat="server" MaxLength="30" Columns="40"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 200px">Address</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_ADDR" runat="server" MaxLength="40" Columns="40"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 200px">City</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_CITY" runat="server" MaxLength="30" Columns="40"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 200px">Zip Code</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_ZIPCODE" onkeypress="return numbersonly()" runat="server" MaxLength="6"
										Columns="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 200px">Marketing Area Code</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_AREACODE" runat="server" MaxLength="10" Columns="10"></asp:textbox></TD>
							</TR> <!-- Additional Field : Right --></TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" runat="server" Width="100px" Text="Save" CssClass="Button1" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL" runat="server" Width="100px" Text="Cancel" CssClass="Button1" onclick="BTN_CANCEL_Click"></asp:button>
						<asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Existing Data</TD>
				</TR>
				<TR>
					<TD colSpan="2"><ASP:DATAGRID id="Datagrid1" runat="server" Width="100%" AllowPaging="True" CellPadding="1" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="AREA_ID" HeaderText="Code">
									<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AREA_NAME" HeaderText="Area">
									<HeaderStyle Width="500px" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AR_ADDR1" HeaderText="Address">
									<HeaderStyle Width="800px" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AR_CITY" HeaderText="City">
									<HeaderStyle Width="500px" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AR_ZIPCODE" HeaderText="Zip Code">
									<HeaderStyle Width="200px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="AR_SRCCODE" HeaderText="Mac"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="350px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete1" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</ASP:DATAGRID></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Requested Data</TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="2">
						<ASP:DATAGRID id="DataGrid2" runat="server" Width="100%" AllowPaging="True" CellPadding="1" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="AREA_ID" HeaderText="Code">
									<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AREA_NAME" HeaderText="Area">
									<HeaderStyle Width="400px" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AR_ADDR1" HeaderText="Address">
									<HeaderStyle Width="800px" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AR_CITY" HeaderText="City">
									<HeaderStyle Width="400px" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AR_ZIPCODE" HeaderText="Zip Code">
									<HeaderStyle Width="200px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA" HeaderText="Status">
									<HeaderStyle Width="200px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="AR_SRCCODE" HeaderText="CODE"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="350px" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="Linkbutton1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</ASP:DATAGRID></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2">&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
