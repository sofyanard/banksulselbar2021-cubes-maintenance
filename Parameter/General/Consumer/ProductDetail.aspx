<%@ Page language="c#" Codebehind="ProductDetail.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.ProductDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProductDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_entries.html" -->
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="125%">
				<tr>
					<td class="tdHeader1" style="FONT-SIZE: 12px" colSpan="6">
					</td>
				</tr>
				<tr>
					<td class="TDBGColor1" width="16%">Product Code</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue" width="25%">
						<asp:label id="LBL_PID" Runat="server"></asp:label></td>
					<td class="TDBGColor1" width="16%">NPWP Limit</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue" width="25%"><asp:label id="LBL_NPWP" Runat="server"></asp:label></td>
				</tr>
				<TR>
					<TD class="TDBGColor1" width="16%">Product Name</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" width="25%">
						<asp:label id="LBL_PRNAME" Runat="server"></asp:label></TD>
					<TD class="TDBGColor1" width="16%">Down Payment</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" width="25%"><asp:label id="LBL_DP" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" width="16%">Group Name</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" width="25%"><asp:label id="LBL_GRP_NAME" Runat="server"></asp:label></TD>
					<TD class="TDBGColor1" width="16%">SPPK Limit Time</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" width="25%"><asp:label id="LBL_SPPK" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" width="16%">Customer Type</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" width="25%"><asp:label id="LBL_CUST_TYPE" Runat="server"></asp:label></TD>
					<TD class="TDBGColor1" width="16%">AIP Limit Time</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" width="25%"><asp:label id="LBL_AIP" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" width="16%">Negative List</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" width="25%"><asp:label id="LBL_NEG_LIST" Runat="server"></asp:label></TD>
					<TD class="TDBGColor1" width="16%">Admin Fee</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" width="25%"><asp:label id="LBL_ADMIN" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" width="16%">Blacklist</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" width="25%"><asp:label id="LBL_BLACK" Runat="server"></asp:label></TD>
					<TD class="TDBGColor1" width="16%">Floor Rate</TD>
					<TD align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" width="25%"><asp:label id="LBL_FLOOR_RATE" Runat="server"></asp:label></TD>
				</TR>
				<tr>
					<td class="TDBGColor1" style="HEIGHT: 21px" width="16%">PreScreener</td>
					<td style="HEIGHT: 21px" align="center" width="1%">:</td>
					<td class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_PRES" Runat="server"></asp:label></td>
					<td class="TDBGColor1" style="HEIGHT: 21px" width="16%">Floor Limit</td>
					<td style="HEIGHT: 21px" align="center" width="1%">:</td>
					<td class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_FLOOR_LIM" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="TDBGColor1" width="16%">DHBI</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_DHBI" Runat="server"></asp:label></td>
					<td class="TDBGColor1" width="16%">Ceiling Limit</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_CEIL_LIM" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="TDBGColor1" width="16%">Promo Group</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_PROMO" Runat="server"></asp:label></td>
					<td class="TDBGColor1" width="16%">
						Provisi</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_PROV" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="TDBGColor1" width="16%">Type</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_TYPE" Runat="server"></asp:label></td>
					<td class="TDBGColor1" width="16%">
						Provition rate</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_PROV_RATE" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="TDBGColor1" width="16%">eMAS Code</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_EMAS_CODE" Runat="server"></asp:label></td>
					<td class="TDBGColor1" width="16%">
						Fiducia</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_FIDU" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="TDBGColor1" width="16%">SPK</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_SPK" Runat="server"></asp:label></td>
					<td class="TDBGColor1" width="16%">Fiducia&nbsp;Limit</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_FIDU_LIM" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="TDBGColor1" width="16%">Marketing Source Code</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_MKSC" Runat="server"></asp:label></td>
					<td class="TDBGColor1" width="16%">Bea Other</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_BEA_OTHER" Runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="TDBGColor1" width="16%">
						Kendara</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_PR_KENDARA" Runat="server"></asp:label></td>
					<td class="TDBGColor1" width="16%"></td>
					<td align="center" width="1%"></td>
					<td class="TDBGColorValue"></td>
				</tr>
				<tr>
					<td class="TDBGColor1" width="16%">
						Revolving</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue">
						<asp:label id="LBL_REVOLVING" Runat="server"></asp:label></td>
					<td class="TDBGColor1" width="16%"></td>
					<td align="center" width="1%"></td>
					<td class="TDBGColorValue"></td>
				</tr>
				<tr>
					<td class="TDBGColor1" width="16%">Second Round Approval</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue">
						<asp:label id="LBL_ROUND_APPROVAL" Runat="server"></asp:label></td>
					<td class="TDBGColor1" width="16%"></td>
					<td align="center" width="1%"></td>
					<td class="TDBGColorValue"></td>
				</tr>
				<tr>
					<td class="TDBGColor1" width="16%" style="HEIGHT: 19px">RAB Document</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue">
						<asp:label id="LBL_DOC_RAB" Runat="server"></asp:label></td>
					<td class="TDBGColor1" width="16%"></td>
					<td align="center" width="1%"></td>
					<td class="TDBGColorValue"></td>
				</tr>
				<TR>
					<TD class="TDBGColor1" style="HEIGHT: 20px" width="16%">Active</TD>
					<TD style="HEIGHT: 20px" align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 20px">
						<asp:label id="LBL_ACTIVE" Runat="server"></asp:label></TD>
					<TD class="TDBGColor1" style="HEIGHT: 20px" width="16%"></TD>
					<TD style="HEIGHT: 20px" align="center" width="1%"></TD>
					<TD class="TDBGColorValue" style="HEIGHT: 20px"></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="HEIGHT: 20px" width="16%">Allow Cardbundling</TD>
					<TD style="HEIGHT: 20px" align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 20px">
						<asp:label id="LBL_CARDBUNDLING" Runat="server"></asp:label></TD>
					<TD class="TDBGColor1" style="HEIGHT: 20px" width="16%"></TD>
					<TD style="HEIGHT: 20px" align="center" width="1%"></TD>
					<TD class="TDBGColorValue" style="HEIGHT: 20px"></TD>
				</TR>
				<tr>
					<td class="TDBGColor1" width="16%">Mitrakarya</td>
					<td align="center" width="1%">:</td>
					<td class="TDBGColorValue"><asp:label id="LBL_PR_MITRAKARYA" Runat="server"></asp:label></td>
					<td class="TDBGColor1" width="16%"></td>
					<td align="center" width="1%"></td>
					<td class="TDBGColorValue"></td>
				</tr>
				<tr>
					<td class="tdHeader1" style="FONT-SIZE: 12px" colSpan="6">
					</td>
				</tr>
				<TR>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%">Child Product Code</TD>
					<TD style="HEIGHT: 21px" align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px">
						<asp:label id="LBL_CHILDPRODUCTID" Runat="server"></asp:label></TD>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%">Minimum Ratio</TD>
					<TD style="WIDTH: 7px; HEIGHT: 21px">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px">
						<asp:label id="LBL_CHILDMINRATIO" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%">Child Product Name</TD>
					<TD style="HEIGHT: 21px" align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px">
						<asp:label id="LBL_CHILDPRODUCT" Runat="server"></asp:label></TD>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%">Maximum Ratio</TD>
					<TD style="WIDTH: 7px; HEIGHT: 21px">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_CHILDMAXRATIO" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%">Minimum&nbsp;Tenor</TD>
					<TD style="HEIGHT: 21px" align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_CHILDMINTENOR" Runat="server"></asp:label></TD>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%">Default Ratio</TD>
					<TD style="WIDTH: 7px; HEIGHT: 21px">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_CHILDDEFRATIO" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%">Maximum Tenor</TD>
					<TD style="HEIGHT: 21px" align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_CHILDMAXTENOR" Runat="server"></asp:label></TD>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%">Minimum&nbsp;Limit</TD>
					<TD style="HEIGHT: 21px" align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_CHILDMINLIMIT" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%">Default Tenor</TD>
					<TD style="HEIGHT: 21px" align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_CHILDDEFTENOR" Runat="server"></asp:label></TD>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%">Maximum Limit</TD>
					<TD style="HEIGHT: 21px" align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_CHILDMAXLIMIT" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="HEIGHT: 22px" width="16%">Minimum Interest</TD>
					<TD style="HEIGHT: 22px" align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:label id="LBL_CHILDMININTEREST" Runat="server"></asp:label>%</TD>
					<TD class="TDBGColor1" style="HEIGHT: 22px" width="16%"></TD>
					<TD style="HEIGHT: 22px" align="center" width="1%"></TD>
					<TD class="TDBGColorValue" style="HEIGHT: 22px"></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%">Maximum Interest</TD>
					<TD style="HEIGHT: 21px" align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_CHILDMAXINTEREST" Runat="server"></asp:label>%</TD>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%"></TD>
					<TD style="HEIGHT: 21px" align="center" width="1%"></TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px"></TD>
				</TR>
				<TR>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%">Default Interest</TD>
					<TD style="HEIGHT: 21px" align="center" width="1%">:</TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:label id="LBL_CHILDDEFINTEREST" Runat="server"></asp:label>%</TD>
					<TD class="TDBGColor1" style="HEIGHT: 21px" width="16%"></TD>
					<TD style="HEIGHT: 21px" align="center" width="1%"></TD>
					<TD class="TDBGColorValue" style="HEIGHT: 21px"></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="6"><INPUT onclick="javascript:window.close()" type="button" value="Close"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
