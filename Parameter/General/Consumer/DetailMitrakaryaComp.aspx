<%@ Page language="c#" Codebehind="DetailMitrakaryaComp.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.DetailMitrakaryaComp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Detail Mitrakarya Company</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="2" width="100%" border="1">
				<TR>
					<TD class="tdHeader1" colspan="3">Detail Mitrakarya Company</TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Company Induk</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_COMP_INDUK" runat="server"></asp:Label>
						<asp:Label id="LBL_DB_IP" runat="server" Visible="False"></asp:Label>
						<asp:Label id="LBL_DB_NAME" runat="server" Visible="False"></asp:Label>&nbsp;</TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Company Source&nbsp;Code</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_COMP_CODE" runat="server"></asp:Label>
						<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label>
						<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Company Name</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_COMP_NAME" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Branch</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_BRANCH" runat="server"></asp:Label>&nbsp;</TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Sub Interest</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_SUB_IN" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Expire Date</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_EXPDATE" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Guarantor Line</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_GLINE" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Realisasi</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue"></ASP:TEXTBOX>
						<asp:Label id="LBL_REAL" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Remaining Guarantor's Line</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_REMAIN" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Company Rating</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_COMP_RATE" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Presentase Potongan Gaji</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_POT_GAJI" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Pesangon 6 th</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_PES1" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Pesangon &gt; 6 th</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_PES2" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Limit Individu (maximal)</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_LIMIT" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Tenor Individu</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_TENOR" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Blocked</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_BLOCK" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="WIDTH: 210px">Cover THT</TD>
					<TD style="WIDTH: 6px">:</TD>
					<TD class="TDBGColorValue">
						<asp:Label id="LBL_THT" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colspan="3">
						<asp:Button id="BTN_CLOSE" runat="server" Text="Close" CssClass="button1" Width="80px" onclick="BTN_CLOSE_Click"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
