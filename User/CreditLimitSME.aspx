<%@ Page language="c#" Codebehind="CreditLimitSME.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.User.CreditLimitSME" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CreditLimitSME</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%" border="0">
					<TR>
						<TD colSpan="2"><asp:datagrid id="DataGrid1" runat="server" AllowPaging="True" PageSize="10" Width="100%" Height="32px"
								AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="tdHdrData"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="PROGRAMID"></asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="Sub Segmen Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="APPTYPEID"></asp:BoundColumn>
									<asp:BoundColumn DataField="APPTYPEDESC" HeaderText="Application Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LIMIT1" HeaderText="Limit Pertama" DataFormatString="{0:#,##0.#}">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LIMIT2" HeaderText="Limit Kedua" DataFormatString="{0:#,##0.#}">
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
									<TD class="TDBGColor1">User ID</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_USERID" runat="server" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Sub Segmen Program</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PROGRAM" runat="server" Enabled="False"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Application Type</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_APPTYPE" runat="server" Enabled="False"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="150">Limit Pertama</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_LIMIT1" onblur="FormatCurrency(this)" runat="server" Enabled="False" MaxLength="30"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="150">Limit Kedua</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_LIMIT2" onblur="FormatCurrency(this)" runat="server" Enabled="False" MaxLength="30"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" align="center" colSpan="2"><asp:button id="BTN_NEW" runat="server" Width="87px" Text="New" CssClass="Button1" onclick="BTN_NEW_Click"></asp:button>&nbsp;
							<asp:button id="Button1" runat="server" Width="87px" Text="Close" CssClass="Button1" onclick="Button1_Click"></asp:button><asp:button id="BTN_SUBMIT" runat="server" Width="87px" Text="Submit" CssClass="Button1" Visible="False" onclick="BTN_SUBMIT_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL" runat="server" Width="87px" Text="Cancel" CssClass="Button1" Visible="False" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="Label1" runat="server" Visible="False">1</asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" align="center" colSpan="2">Current Request</TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2"><asp:datagrid id="Datagrid2" runat="server" AllowPaging="True" PageSize="10" Width="100%" Height="32px"
								AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" CssClass="tdHdrData"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="PROGRAMID"></asp:BoundColumn>
									<asp:BoundColumn DataField="PROGRAMDESC" HeaderText="Sub Segmen Program">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="APPTYPEID"></asp:BoundColumn>
									<asp:BoundColumn DataField="APPTYPEDESC" HeaderText="Application Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LIMIT1" HeaderText="Limit Pertama" DataFormatString="{0:#,##0.#}">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LIMIT2" HeaderText="Limit Kedua" DataFormatString="{0:#,##0.#}">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="STATUS"></asp:BoundColumn>
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
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
