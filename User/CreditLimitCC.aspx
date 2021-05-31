<%@ Page language="c#" Codebehind="CreditLimitCC.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.User.CreditLimitCC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CreditLimitCC</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR>
						<TD><asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" Height="32px" Width="100%"
								PageSize="6" AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="tdHdrData"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="PRODUCTID" HeaderText="PRODUCTID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Product">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SC_SINGLE" HeaderText="Accept Limit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LinkButton1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Add Limit Approval</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 17px">User ID</TD>
									<TD style="WIDTH: 15px; HEIGHT: 17px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:textbox id="TXT_USERID" runat="server" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 17px">Product</TD>
									<TD style="WIDTH: 15px; HEIGHT: 17px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:dropdownlist id="DDL_PRODUCTID" runat="server" Enabled="False"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">
										Accept Limit</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_ACC_LIMIT" runat="server" Enabled="False" onblur="FormatCurrency(this)"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" align="center" colSpan="2"><asp:button id="BTN_NEW" runat="server" Width="87px" CssClass="Button1" Text="New" onclick="BTN_NEW_Click"></asp:button>&nbsp;
							<asp:button id="Button1" runat="server" Width="87px" Text="Close" CssClass="Button1" onclick="Button1_Click"></asp:button>
							<asp:button id="BTN_SUBMIT" runat="server" Width="87px" CssClass="Button1" Text="Submit" Visible="False" onclick="BTN_SUBMIT_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL" runat="server" Width="87px" CssClass="Button1" Text="Cancel" Visible="False" onclick="BTN_CANCEL_Click"></asp:button>
							<asp:Label id="Label1" runat="server" Visible="False">1</asp:Label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" align="center" colSpan="2">Current Request</TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2"><asp:datagrid id="Datagrid2" runat="server" AutoGenerateColumns="False" Height="32px" Width="100%"
								PageSize="6">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="tdHdrData"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="PRODUCTID" HeaderText="PRODUCTID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Product">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ACC_LIMIT" HeaderText="Accept Limit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUSDESC" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="Linkbutton3" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="SEQ" HeaderText="SEQ"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="STATUS" HeaderText="STATUS"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
