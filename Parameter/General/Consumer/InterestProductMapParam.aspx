<%@ Page language="c#" Codebehind="InterestProductMapParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.InterestProductMapParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InterestProductMapParam</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table2">
							<TR>
								<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance 
										Consumer : Host Maker</B></TD>
							</TR>
						</TABLE>
					</TD>
					<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
						<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						Parameter Interest Product Mapping Maker</TD>
				</TR>
				<TR>
					<TD class="td" colspan="2">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="td" vAlign="top" width="50%">
									<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" runat="server">
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 250px">
												Product Code</TD>
											<TD style="WIDTH: 10px">:</TD>
											<TD class="TDBGColorValue" style="HEIGHT: 8px">
												<asp:DropDownList id="DDL_PROD" runat="server" CssClass="Mandatory"></asp:DropDownList></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 250px">Interest Type</TD>
											<TD style="WIDTH: 10px">:</TD>
											<TD class="TDBGColorValue">
												<asp:DropDownList id="DDL_ASSC" runat="server" CssClass="Mandatory"></asp:DropDownList></TD>
										</TR>
										<TR>
											<TD class="TDBGColor1" style="WIDTH: 250px">SIBS Code</TD>
											<TD style="WIDTH: 10px">:</TD>
											<TD class="TDBGColorValue">
												<asp:TextBox id="TXT_SIBS" runat="server" CssClass="Mandatory" MaxLength="15" Columns="20"></asp:TextBox></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" Runat="server" Text="Save" CssClass="button1" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
									<asp:button id="BTN_CANCEL" Runat="server" Text="Cancel" CssClass="button1" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label>
									<asp:label id="LBL_PR" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_AS" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_ID" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="tdHeader1" colSpan="2">
									<P>
										Current&nbsp;Insurance Based On Product Table</P>
								</TD>
							</TR>
							<TR>
								<TD class="td" colSpan="2"><asp:datagrid id="DGExisting" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
										<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
										<Columns>
											<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Product Code">
												<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="IN_DESC" HeaderText="Insurance Type">
												<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="PRODUCTID"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="IN_TYPE"></asp:BoundColumn>
											<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
												<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Function">
												<HeaderStyle Width="80px" CssClass="tdSmallHeader"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="Linkbutton3" runat="server" CommandName="edit">Edit</asp:LinkButton>
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
								<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
									<asp:datagrid id="DGRequest" runat="server" Width="100%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False">
										<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
										<Columns>
											<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Product Code ">
												<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="IN_DESC" HeaderText="Insurance Type">
												<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="PRODUCTID"></asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="IN_TYPE"></asp:BoundColumn>
											<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
												<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn Visible="False" DataField="CH_STA"></asp:BoundColumn>
											<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
												<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Function">
												<HeaderStyle Width="80px" CssClass="tdSmallHeader"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:LinkButton id="Linkbutton1" runat="server" CommandName="edit">Edit</asp:LinkButton>
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
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
