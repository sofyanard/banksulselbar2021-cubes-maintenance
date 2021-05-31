<%@ Page language="c#" Codebehind="HouseTypeParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.HouseTypeParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HouseTypeParam</title>
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
					<TD class="tdHeader1" colSpan="2">
						Parameter House Type Maker</TD>
				</TR>
				<TR>
					<TD class="td">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="200">Type Code</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue">
									<asp:TextBox id="TXT_TYPE_CODE" runat="server" Width="80px" CssClass="Mandatory" MaxLength="5"
										onkeypress="return numbersonly()"></asp:TextBox>
									<asp:Label id="LBL_CODE_LAMA" runat="server" Visible="False"></asp:Label>
									<asp:label id="LBL_ID" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="200">Type Name</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue">
									<asp:TextBox id="TXT_TYPE_NAME" runat="server" Width="232px" MaxLength="50"></asp:TextBox>
									<asp:label id="LBL_SAVEMODE" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">
									Description</TD>
								<TD></TD>
								<TD class="TDBGColorValue">
									<asp:TextBox id="TXT_DESC" runat="server" Width="232px" MaxLength="50"></asp:TextBox>
									<asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" Runat="server" Text="Save" CssClass="button1" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" Runat="server" Text="Cancel" CssClass="button1" onclick="BTN_CANCEL_Click"></asp:button>
						<asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Current&nbsp; House Type Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DG_HOUSE" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="20"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="type_code" HeaderText="Code">
									<HeaderStyle CssClass="tdSmallHeader" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="type_name" HeaderText="Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="description" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
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
					<TD class="td" colSpan="2">
						<asp:datagrid id="DG_THOUSE" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="type_code" HeaderText="Code">
									<HeaderStyle CssClass="tdSmallHeader" Width="15%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="type_name" HeaderText="Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="description" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Status">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="LBL_STATUS" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="ch_sta">
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
