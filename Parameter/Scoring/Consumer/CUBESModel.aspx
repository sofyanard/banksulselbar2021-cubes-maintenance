<%@ Page language="c#" Codebehind="CUBESModel.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Consumer.CUBESModel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CUBESModel</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include  file="../../../include/cek_entries.html" -->
		<!-- #include  file="../../../include/cek_mandatory.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD style="HEIGHT: 41px" width="50%">
										<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
											border="0">
											<TR>
												<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Model&nbsp;Maintenance :
														<asp:Label id="LBL_MAIN_TITLE" runat="server"></asp:Label></B></TD>
											</TR>
										</TABLE>
									</TD>
									<TD class="tdNoBorder" align="right"><A href="../../ScoringParamAll.aspx?mc=9902040301&amp;moduleID=40"><IMG src="../../../Image/Back.jpg" border="0"></A>
										<A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A> <A href="../../../Logout.aspx" target="_top">
											<IMG src="../../../Image/Logout.jpg"></A>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%">
					<TR>
						<TD class="tdHeader1" colSpan="2"><asp:label id="LBL_TITLE" runat="server">Model Scoring Maker</asp:label></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="2"><asp:table id="TBL_MODEL" runat="server" width="100%" cellPadding="0" cellSpacing="0"></asp:table></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE_VALUE" CssClass="button1" Runat="server" Text="Save" Width="70px" onclick="BTN_SAVE_VALUE_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL_VALUE" CssClass="button1" Runat="server" Text="Cancel" onclick="BTN_CANCEL_VALUE_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label><asp:label id="LBL_EDITMODE" runat="server" Visible="False">1</asp:label>
							<asp:label id="LBL_MODELSEQ" runat="server" Visible="False">NULL</asp:label></TD>
					</TR>
				</TABLE>
				<TABLE id="Table3" cellSpacing="1" cellPadding="2" width="100%">
					<TR>
						<TD class="tdHeader1" colSpan="2">Existing Data</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="2"><asp:table id="TBL_EXISTING" runat="server" width="100%"></asp:table></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Maker Request</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="2"><asp:table id="TBL_REQUEST" runat="server" width="100%"></asp:table></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" colSpan="2"></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
