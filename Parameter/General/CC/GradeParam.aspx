<%@ Page language="c#" Codebehind="GradeParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.GradeParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GradeParam</title>
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
								<TD align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx">
										<IMG src="../../../Image/MainMenu.jpg"></A>&nbsp;<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Grade Maker</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" width="50%">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 202px" width="202">Category ID</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue">
									<asp:DropDownList id="DDL_CATEGORY_ID" runat="server" CssClass="mandatory"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 202px; HEIGHT: 20px" width="202">Grade&nbsp;ID</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px"></TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox id="TXT_GOL_ID" onkeypress="return kutip_satu()" runat="server" Width="208px" MaxLength="20"
										CssClass="mandatory"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 202px">Grade&nbsp;Desc.</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_GOL_DESC" onkeypress="return kutip_satu()" runat="server" Width="100%" MaxLength="60"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 202px">Grade Short&nbsp;Desc.</TD>
								<TD style="WIDTH: 7px"></TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_GOL_SHORTDESC" onkeypress="return kutip_satu()" runat="server" Width="208px"
										MaxLength="15"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" Width="57px" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button>
						<asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>
							Existing&nbsp;Data</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblALternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="CATEGORY_ID" HeaderText="CATEGORY_ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="CATEGORY_DESC" HeaderText="Category ID">
									<HeaderStyle Width="20%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GOL_ID" HeaderText="Grade ID">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GOL_DESC" HeaderText="Description">
									<HeaderStyle Width="35%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GOL_SHORTDESC" HeaderText="Short Description">
									<HeaderStyle Width="25%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle HorizontalAlign="Center" Width="2px" CssClass="tdSmallHeader"></HeaderStyle>
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
							<AlternatingItemStyle CssClass="TblALternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="CATEGORY_ID" HeaderText="CATEGORY_ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="CATEGORY_DESC" HeaderText="Category ID">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GOL_ID" HeaderText="Grade ID">
									<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GOL_DESC" HeaderText="Description">
									<HeaderStyle Width="30%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GOL_SHORTDESC" HeaderText="Short Description">
									<HeaderStyle Width="25%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PENDING_STATUS">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Status">
									<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="9%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
