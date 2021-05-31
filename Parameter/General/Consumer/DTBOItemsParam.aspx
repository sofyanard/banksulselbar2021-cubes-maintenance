<%@ Page language="c#" Codebehind="DTBOItemsParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.DTBOItemsParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DTBOItemsParam</title>
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
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance 
													:&nbsp;General Maker</B></TD>
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
						Parameter DTBO Items Maker</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" width="50%">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="200">
									Code</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue">
									<asp:textbox id="TXT_CODE" runat="server" CssClass="mandatory" MaxLength="4" Columns="10" onkeypress="return numbersonly()"></asp:textbox>
									<asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="200">Description</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue">
									<asp:textbox id="TXT_DESC" runat="server" Width="376px" CssClass="mandatory" onkeypress="return kutip_satu()"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="HEIGHT: 23px">
									Upload</TD>
								<TD style="WIDTH: 7px; HEIGHT: 23px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 23px">
									<asp:DropDownList id="DDL_UPLOAD" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_UPLOAD_SelectedIndexChanged">
										<asp:ListItem Value="1">Yes</asp:ListItem>
										<asp:ListItem Value="0">No</asp:ListItem>
									</asp:DropDownList></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Print</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue">
									<asp:DropDownList id="DDL_PRINT" runat="server">
										<asp:ListItem Value="1">Yes</asp:ListItem>
										<asp:ListItem Value="0">No</asp:ListItem>
									</asp:DropDownList></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">SIBS Code</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_SBC" runat="server" CssClass="mandatory" Columns="10"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" Runat="server" Text="Save" CssClass="button1" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" Runat="server" Text="Cancel" CssClass="button1" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Current&nbsp;DTBO Items&nbsp; Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="Datagrid1" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="20"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="SIBS_FLAG"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CW_PRINT"></asp:BoundColumn>
								<asp:BoundColumn DataField="CW_CODE" HeaderText="Code">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CW_DESC" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FLAG" HeaderText="Upload">
									<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PRINTT" HeaderText="Print">
									<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
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
						<asp:datagrid id="DataGrid2" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="SIBS_FLAG"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CW_PRINT"></asp:BoundColumn>
								<asp:BoundColumn DataField="CW_CODE" HeaderText="Code">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CW_DESC" HeaderText="Description">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FLAG" HeaderText="Upload">
									<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PRINTT" HeaderText="Print">
									<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA" HeaderText="Status">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
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
					<TD class="TDBGColor2" colSpan="2">&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
