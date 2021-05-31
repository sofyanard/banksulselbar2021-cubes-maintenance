<%@ Page language="c#" Codebehind="GeneralParamApprovalAll.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.GeneralParamApprovalAll" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GeneralParamApprovalAll</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder" width="50%">
							<TABLE id="Table1">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter&nbsp;
											<asp:Label id="LBL_MODULENAME" runat="server"></asp:Label>
											: General</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../Image/back.jpg"></asp:imagebutton><A href="../Body.aspx"><IMG src="../Image/MainMenu.jpg"></A>
							<A href="../Logout.aspx" target="_top"><IMG src="../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colspan="2">General Parameter - Approval</TD>
					</TR>
					<TR>
						<TD colspan="2" class="td">
							<TABLE cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD>
										<asp:Table id="Table2" runat="server" Width="100%"></asp:Table></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" colSpan="2">&nbsp;</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
