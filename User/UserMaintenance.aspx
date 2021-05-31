<%@ Page language="c#" Codebehind="UserMaintenance.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.User.UserMaintenance" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UserMaintenance</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../include/OpenWindow.html" -->
		<!-- #include file="../include/cek_mandatoryonly.html" -->
		<script language="javascript">
		function showpwdmsg()
		{
			if (!Form1.TXT_SU_PWD.disabled && Form1.TXT_SU_PWD.dataFld=='')
			{
				Form1.TXT_SU_PWD.dataFld = '1';
				alert(Form1.pwdmsg.value);
			}
		}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="TableHeader">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maker: User 
											Maintenance</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="../Body.aspx"><IMG src="../Image/MainMenu.jpg"></A><A href="../Logout.aspx" target="_top"><IMG src="../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2" height="41"><asp:hyperlink id="HyperLink1" runat="server" Font-Bold="True" NavigateUrl="User.aspx">User Maintenance</asp:hyperlink>&nbsp;|
							<asp:hyperlink id="HyperLink2" runat="server" Font-Bold="True">Group Maintenance</asp:hyperlink></TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Module</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_RFMODULE" runat="server"></asp:dropdownlist><INPUT id="pwdmsg" type="hidden" name="pwdmsg" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Area</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_RFAREA" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Branch</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_RFBRANCH" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Group</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_RFGROUP" runat="server" Width="250px"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top" align="center" width="50%">
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">User ID</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_SEARCH_USERID" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">User Name</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_SEARCH_USERNAME" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Related Personel UserID</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_SEARCH_UPLINER" runat="server"></asp:textbox>&nbsp;
										<asp:radiobuttonlist id="RDL_UPLINER" runat="server" Visible="False" RepeatDirection="Horizontal" RepeatLayout="Flow">
											<asp:ListItem Value="SU_UPLINER+' '+SU_UNAME">Small</asp:ListItem>
											<asp:ListItem Value="SU_MDLUPLINER+' '+SU_MDLUNAME">Middle</asp:ListItem>
											<asp:ListItem Value="SU_CORUPLINER+' '+SU_CORUNAME">Corporate</asp:ListItem>
											<asp:ListItem Value="SU_CRGUPLINER+' '+SU_CRGUNAME">Credit Recovery</asp:ListItem>
											<asp:ListItem Value="SU_MCRUPLINER+' '+SU_MCRUNAME">Mikro</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Officer Code</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_SEARCH_OFFICERCODE" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 129px" align="right"><asp:button id="BTN_SEARCH" runat="server" Width="100px" Text="Search" onclick="BTN_SEARCH_Click"></asp:button></TD>
									<TD></TD>
									<TD><asp:button id="BTN_CLEAR" runat="server" Width="100px" Text="Clear" onclick="BTN_CLEAR_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD colSpan="2"><ASP:DATAGRID id="DatGrd" runat="server" Width="100%" 
                                AllowSorting="True" AutoGenerateColumns="False"
								CellPadding="1" AllowPaging="True" onselectedindexchanged="DatGrd_SelectedIndexChanged">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="MODULEID" HeaderText="ModuleID"></asp:BoundColumn>
									<asp:BoundColumn DataField="MODULENAME" SortExpression="MODULENAME" HeaderText="Module">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="USERID" SortExpression="USERID" HeaderText="User ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SU_FULLNAME" SortExpression="SU_FULLNAME" HeaderText="Full Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="GROUPID" HeaderText="Group ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SG_GRPNAME" SortExpression="SG_GRPNAME" HeaderText="Group">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SU_LOGON" HeaderText="Logon">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SU_REVOKE" HeaderText="Revoke">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SU_ACTIVE" HeaderText="Active"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Logon">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="CheckBox1" runat="server" Enabled="False"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Revoke">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="CheckBox2" runat="server" Enabled="False"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Active">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="Checkbox3" runat="server" Enabled="False"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnkEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnkDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnkUndelete" runat="server" CommandName="undelete">UnDelete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE" HeaderText="Active"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
					<TR>
						<TD align="center" colSpan="2">&nbsp;
							<asp:label id="LBL_RESULT" runat="server" Font-Bold="True" ForeColor="Red"></asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" width="50%" colSpan="2">Detail Information</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 17px">Group</TD>
									<TD style="WIDTH: 15px; HEIGHT: 17px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:dropdownlist id="DDL_GROUPID" runat="server" Width="250px" AutoPostBack="True" CssClass="mandatory" onselectedindexchanged="DDL_GROUPID_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px; HEIGHT: 17px">UserID</TD>
									<TD style="WIDTH: 15px; HEIGHT: 17px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:textbox id="TXT_USERID" runat="server" CssClass="mandatory"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Nama Lengkap</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_SU_FULLNAME" runat="server" Width="250px" CssClass="mandatory"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Password</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_SU_PWD" onfocus="showpwdmsg()" runat="server" CssClass="mandatory" TextMode="Password"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Verify</TD>
									<TD></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:textbox id="TXT_VERIFYPWD" runat="server" CssClass="mandatory" TextMode="Password"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">NIP</TD>
									<TD></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:textbox id="TXT_SU_NIP" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">HP No.</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_SU_HPNUM" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Email</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_SU_EMAIL" runat="server" Width="250px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Officer Code</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_OFFICER_CODE" runat="server"></asp:textbox></TD>
								</TR>
								<tr>
									<td class="TDBGColor1">Jabatan Code</td>
									<td></td>
									<td class="TDBGColorValue"><asp:dropdownlist id="DDL_JB_CODE" runat="server"></asp:dropdownlist></td>
								</tr>
								<TR>
									<TD class="TDBGColor1">Cabang&nbsp;/ CBC</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_BRANCH" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SEARCH_BRANCH" onclick="openSetWindow('SearchBranch.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SU_BRANCH&amp;targetObjectDesc=TXT_BRANCH', '460', '232');"
											type="button" size="20" value="..." name="BTN_SEARCH_BRANCH" runat="server"></TD>
								</TR>
								<TR>
									<TD colSpan="3"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Logon status</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:checkbox id="cb_logon" runat="server" Text="(clear to reset)" AutoPostBack="True" oncheckedchanged="cb_logon_CheckedChanged"></asp:checkbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Revoke</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:checkbox id="cb_revoke" runat="server" Text="(check for yes)"></asp:checkbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Active</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:checkbox id="CHK_SU_ACTIVE" runat="server" Text="(check for yes)"></asp:checkbox><asp:textbox id="TXT_SU_TEAMLEADER" runat="server" Width="1px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None" ontextchanged="TXT_SU_TEAMLEADER_TextChanged"></asp:textbox><asp:textbox id="TXT_SU_MIDUPLINER" runat="server" Width="1px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None" ontextchanged="TXT_SU_MIDUPLINER_TextChanged"></asp:textbox><asp:textbox id="TXT_SU_UPLINER" runat="server" Width="1px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None" ontextchanged="TXT_SU_UPLINER_TextChanged"></asp:textbox><asp:textbox id="TXT_SU_MITRARM" runat="server" Width="1px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None" ontextchanged="TXT_SU_MITRARM_TextChanged"></asp:textbox><asp:textbox id="TXT_SU_BRANCH" runat="server" Width="1px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None"></asp:textbox><asp:textbox id="TXT_SU_OWNUNIT" runat="server" Width="10px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None"></asp:textbox><asp:textbox id="TXT_SU_SYSUNIT" runat="server" Width="10px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None"></asp:textbox><asp:textbox id="TXT_SU_UPLINER_CON" runat="server" Width="10px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None"></asp:textbox><asp:textbox id="TXT_SU_MITRARM_CON" runat="server" Width="10px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None"></asp:textbox><asp:textbox id="TXT_SU_UPLINER_CC" runat="server" Width="10px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None"></asp:textbox><asp:textbox id="TXT_SU_MITRARM_CC" runat="server" Width="10px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None"></asp:textbox><asp:textbox id="TXT_SU_CORUPLINER" runat="server" Width="10px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None" ontextchanged="TXT_SU_CORUPLINER_TextChanged"></asp:textbox><asp:textbox id="TXT_SU_CRGUPLINER" runat="server" Width="10px" ForeColor="White" 
											BackColor="Transparent" BorderStyle="None" ontextchanged="TXT_SU_CRGUPLINER_TextChanged"></asp:textbox><asp:textbox id="TXT_SU_MCRUPLINER" runat="server" Width="10px" ForeColor="White" 
											BackColor="Transparent" BorderStyle="None" ontextchanged="TXT_SU_MCRUPLINER_TextChanged"></asp:textbox><asp:textbox id="TXT_AGENTID" runat="server" Width="10px" ForeColor="White" BackColor="Transparent"
											BorderStyle="None"></asp:textbox><asp:textbox id="TXT_SALES_ID" runat="server" Width="10px" ForeColor="White"
											BackColor="Transparent" BorderStyle="None"></asp:textbox></TD>
								</TR>
								<TR>
									<TD colSpan="3"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Password Reset</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:checkbox id="cb_resetpwd" runat="server" Text="(check for yes)"></asp:checkbox></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="TableSME" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<TR>
									<TD style="WIDTH: 129px" align="right"><STRONG>SME&nbsp;:</STRONG></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD style="HEIGHT: 17px"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Teamleader</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_TEAMLEADER" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SEARCH_TL" onclick="openSetWindow('SearchUser.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SU_TEAMLEADER&amp;targetObjectDesc=TXT_TEAMLEADER', '460', '360');"
											type="button" size="20" value="..." name="BTN_SEARCH_TL" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Upliner Micro</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_UPLINER" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SEARCH_UPSMALL" onclick="openSetWindow('SearchUser.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SU_UPLINER&amp;targetObjectDesc=TXT_UPLINER', '460', '360');"
											type="button" size="20" value="..." name="BTN_SEARCH_UPSMALL" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Upliner SME</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_MIDUPLINER" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SEARCH_UPMIDDLE" onclick="openSetWindow('SearchUser.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SU_MIDUPLINER&amp;targetObjectDesc=TXT_MIDUPLINER', '460', '360');"
											type="button" size="20" value="..." name="BTN_SEARCH_UPMIDDLE" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Upliner Corporate</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CORUPLINER" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SEARCH_UPCORP" onclick="openSetWindow('SearchUser.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SU_CORUPLINER&amp;targetObjectDesc=TXT_CORUPLINER', '460', '360');"
											type="button" size="20" value="..." name="BTN_SEARCH_UPCORP" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Upliner Credit Recovery</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CRGUPLINER" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SEARCH_UPCRG" onclick="openSetWindow('SearchUser.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SU_CRGUPLINER&amp;targetObjectDesc=TXT_CRGUPLINER', '460', '360');"
											type="button" size="20" value="..." name="BTN_SEARCH_UPCRG" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Upliner Consumer</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_MCRUPLINER" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SEARCH_UPMCR" onclick="openSetWindow('SearchUser.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SU_MCRUPLINER&amp;targetObjectDesc=TXT_MCRUPLINER', '460', '360');"
											type="button" size="20" value="..." name="BTN_SEARCH_UPMCR" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">User Pair</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_MITRARM" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SEARCH_PAIR" onclick="openSetWindow('SearchUser.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SU_MITRARM&amp;targetObjectDesc=TXT_MITRARM', '460', '360');"
											type="button" size="20" value="..." name="BTN_SEARCH_PAIR" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Department Code</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_DEPT_CODE" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Approval Limit (old)</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_SU_APRVLIMIT" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">eMas Limit (old)</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_SU_EMASLIMIT" runat="server"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Approval Limit</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><INPUT id="BTN_SME_APRVLIMIT" onclick="openSetWindow('CreditLimitSME.aspx?userid=' + document.Form1.TXT_USERID.value, '800', '700');"
											type="button" size="20" value="..." name="BTN_SEARCH_PAIR" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1"></TD>
									<TD></TD>
									<TD class="TDBGColorValue"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Owner Unit</TD>
									<TD></TD>
									<TD class="TDBGColorValue">
										<asp:textbox id="TXT_OWNUNIT" runat="server" Width="226px" ReadOnly="True"></asp:textbox>
										<INPUT id="BTN_SEARCH_OWNUNIT" onclick="openSetWindow('SearchUnit.aspx?unit=1&amp;targetFormID=Form1&amp;targetObjectID=TXT_SU_OWNUNIT&amp;targetObjectDesc=TXT_OWNUNIT', '460', '260');"
											type="button" size="20" value="..." name="BTN_SEARCH_OWNUNIT" runat="server">
									</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">System Unit</TD>
									<TD></TD>
									<TD class="TDBGColorValue">
										<asp:textbox id="TXT_SYSUNIT" runat="server" Width="226px" ReadOnly="True"></asp:textbox>
										<INPUT id="BTN_SEARCH_SYSUNIT" onclick="openSetWindow('SearchUnit.aspx?unit=1&amp;targetFormID=Form1&amp;targetObjectID=TXT_SU_SYSUNIT&amp;targetObjectDesc=TXT_SYSUNIT', '460', '260');"
											type="button" size="20" value="..." name="BTN_SEARCH_SYSUNIT" runat="server">
									</TD>
								</TR>
							</TABLE>
							<TABLE id="TableConsumer" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<TR>
									<TD style="WIDTH: 129px" align="right"><STRONG>Consumer&nbsp;:</STRONG></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD style="HEIGHT: 17px"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Area</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:label id="LBL_AREANAME" runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Upliner</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_UPLINER_CON" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SRCH_CON_UPLINER" onclick="openSetWindow('SearchUser.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SU_UPLINER_CON&amp;targetObjectDesc=TXT_UPLINER_CON', '460', '300');"
											type="button" size="20" value="..." name="BTN_SEARCH_UPSMALL" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">User Pair</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_MITRARM_CON" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SRCH_CON_USRPAIR" onclick="openSetWindow('SearchUser.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SU_MITRARM_CON&amp;targetObjectDesc=TXT_MITRARM_CON', '460', '300');"
											type="button" size="20" value="..." name="BTN_SEARCH_PAIR" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Approval Limit</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><INPUT id="BTN_CON_APRVLIMIT" onclick="openSetWindow('CreditLimit.aspx?userid=' + document.Form1.TXT_USERID.value, '800', '400');"
											type="button" size="20" value="..." name="BTN_SEARCH_PAIR" runat="server"></TD>
								</TR>
							</TABLE>
							<TABLE id="TableCC" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<TR>
									<TD style="WIDTH: 129px" align="right"><STRONG>Credit Card&nbsp;:</STRONG></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD style="HEIGHT: 17px"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Upliner</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_UPLINER_CC" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SRCH_CC_UPLINER" onclick="openSetWindow('SearchUser.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SU_UPLINER_CC&amp;targetObjectDesc=TXT_UPLINER_CC', '460', '300');"
											type="button" size="20" value="..." name="BTN_SEARCH_UPSMALL" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">User Pair</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_MITRARM_CC" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SRCH_CC_USRPAIR" onclick="openSetWindow('SearchUser.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SU_MITRARM_CC&amp;targetObjectDesc=TXT_MITRARM_CC', '460', '300');"
											type="button" size="20" value="..." name="BTN_SEARCH_PAIR" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Area</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CC_AREA" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SEARCH_CC_AREA" onclick="openSetWindow('CreditAnalystArea.aspx?uid=' + document.Form1.TXT_USERID.value + '&amp;targetFormID=Form1&amp;targetObjectDesc=TXT_CC_AREA', '500', '300');"
											type="button" size="20" value="..." name="BTN_SEARCH_CC_AREA" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Approval Limit</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><INPUT id="BTN_CC_APRVLIMIT" onclick="openSetWindow('CreditLimitCC.aspx?userid=' + document.Form1.TXT_USERID.value, '600', '500');"
											type="button" size="20" value="..." name="BTN_SEARCH_PAIR" runat="server"></TD>
								</TR>
							</TABLE>
							<TABLE id="TableSales" cellSpacing="0" cellPadding="0" width="100%" runat="server">
								<TR>
									<TD style="WIDTH: 129px" align="right"><STRONG>Sales Commission&nbsp;:</STRONG></TD>
									<TD style="WIDTH: 15px"></TD>
									<TD style="HEIGHT: 17px"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Agency</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_AGENTNAME" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SRCH_SC_AGENCY" onclick="openSetWindow('SearchAgency.aspx?targetFormID=Form1&amp;targetObjectID=TXT_AGENTID&amp;targetObjectDesc=TXT_AGENTNAME', '460', '230');"
											type="button" size="20" value="..." name="BTN_SEARCH_UPSMALL" runat="server"></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Sales Agent</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_SALES_NAME" runat="server" Width="226px" ReadOnly="True"></asp:textbox><INPUT id="BTN_SRCH_SC_SAGENT" onclick="openSetWindow('SearchSalesAgent.aspx?targetFormID=Form1&amp;targetObjectID=TXT_SALES_ID&amp;targetObjectDesc=TXT_SALES_NAME&amp;agentid=' + document.Form1.TXT_AGENTID.value + '&amp;agentname=' + document.Form1.TXT_AGENTNAME.value, '460', '230');"
											type="button" size="20" value="..." name="BTN_SEARCH_PAIR" runat="server"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2"><asp:button id="BTN_NEW" runat="server" Width="70px" Text="New" CssClass="Button1" onclick="BTN_NEW_Click"></asp:button>&nbsp;<asp:button id="BTN_SAVE" runat="server" Width="70px" Visible="False" Text="Save" CssClass="Button1" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL" runat="server" Width="70px" Visible="False" Text="Cancel" CssClass="Button1" onclick="BTN_CANCEL_Click"></asp:button><asp:checkbox id="CHK_ISNEW" runat="server" Visible="False"></asp:checkbox><asp:label id="LBL_SqlStatement" runat="server" Visible="False"></asp:label></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
