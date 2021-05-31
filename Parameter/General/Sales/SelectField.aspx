<%@ Page language="c#" Codebehind="SelectField.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Sales.SelectField" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SelectField</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdHeader1" colSpan="2">
							Select Field for Formula</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 90px; HEIGHT: 20px" width="90">Pilih Table</TD>
									<TD style="WIDTH: 11px; HEIGHT: 20px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px">
										<asp:DropDownList id="DDL_TABLE" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_TABLE_SelectedIndexChanged"></asp:DropDownList></TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="50%">
							<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 118px">Pilih Field</TD>
									<TD style="WIDTH: 4px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px">
										<asp:DropDownList id="DDL_FIELD" runat="server"></asp:DropDownList></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2">
							<asp:button id="BTN_OK" CssClass="button1" Text="OK" Runat="server" onclick="BTN_OK_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
