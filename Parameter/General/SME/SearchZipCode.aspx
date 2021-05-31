<%@ Page language="c#" Codebehind="SearchZipCode.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.SearchZipCode" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SearchZipCode</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="tdH" colSpan="2"></TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" width="50%">
						<TABLE id="Table2" cellSpacing="2" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="tdLabel">City</TD>
								<TD>:</TD>
								<TD>
									<asp:dropdownlist id="DDL_CITYID" runat="server" Width="200px" AutoPostBack="True" onselectedindexchanged="DDL_CITYID_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="tdLabel">Kabupaten</TD>
								<TD>:</TD>
								<TD><asp:dropdownlist id="DDL_REGIONID" runat="server" AutoPostBack="True" Width="200px" onselectedindexchanged="DDL_REGIONID_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="tdLabel">Zipcode</TD>
								<TD>:</TD>
								<TD><asp:textbox id="txt_ZIPCODE" runat="server" ReadOnly="True" Columns="5"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
				</tr>
				<TR>
					<TD class="tdH" colSpan="2"><asp:label id="Label1" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:button id="btn_OK" runat="server" Text="OK" onclick="btn_OK_Click"></asp:button>&nbsp;<asp:button id="btn_Cancel" runat="server" Text="Cancel" onclick="btn_Cancel_Click"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
