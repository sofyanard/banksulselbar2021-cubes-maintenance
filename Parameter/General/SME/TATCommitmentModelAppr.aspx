<%@ Page language="c#" Codebehind="TATCommitmentModelAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.TATCommitmentModelAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TATCommitmentModelAppr</title>
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
												<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Model TAT Commitment 
														Approval</B></TD>
											</TR>
										</TABLE>
									</TD>
									<TD class="tdNoBorder" align="right"><A href="../../GeneralParamApprovalAll.aspx?moduleID=01"><IMG src="../../../Image/Back.jpg" border="0"></A>
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
						<TD class="tdHeader1" colSpan="2"><asp:label id="LBL_TITLE" runat="server">Model TAT Commitment 
														Approval</asp:label></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="2"><asp:table id="TBL_APPR" runat="server" width="100%"></asp:table></TD>
						</TD></TR>
					<TR>
						<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SUBMIT" Width="70px" Text="Submit" Runat="server" CssClass="button1" onclick="BTN_SUBMIT_Click"></asp:button>&nbsp;<asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label><asp:label id="LBL_EDITMODE" runat="server" Visible="False">1</asp:label>
							<asp:label id="LBL_MODELSEQ" runat="server" Visible="False">NULL</asp:label></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
