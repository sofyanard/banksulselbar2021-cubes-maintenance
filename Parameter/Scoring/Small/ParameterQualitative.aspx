<%@ Page language="c#" Codebehind="ParameterQualitative.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Small.ParameterQualitative" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>SearchParameter</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="../../../style.css" type=text/css rel=stylesheet >
		<!-- #include file="../../../include/cek_entries.html" -->
  </HEAD>
<body MS_POSITIONING="GridLayout">
<form id=Form1 method=post runat="server">
<center>
<TABLE id=Table4 width="100%" border=0>
  <TR>
    <TD class=tdNoBorder align=left width="50%">
      <TABLE id=Table3>
        <TR>
          <TD class=tdBGColor2 style="WIDTH: 400px" align=center 
          ><B>Qualitative 
            Parameter</B></TD></TR></TABLE></TD>
    <td align=right><A href="../../../Body.aspx" ><IMG src="../../../Image/MainMenu.jpg" ></A><A href="../../../Logout.aspx" target=_top ><IMG src="../../../Image/Logout.jpg" ></A></td></TR>
  <TR>
    <TD align=center colSpan=2>
      <TABLE class=td id=Table1 style="WIDTH: 590px; HEIGHT: 140px" height=140 
      cellSpacing=1 cellPadding=1 width=590 border=1>
        <TR>
          <TD class=tdHeader1>Search Criteria</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE id=Table2 cellSpacing=0 cellPadding=0 width="100%" border=0 
            >
              <TR>
                <TD class=TDBGColor1 width=170>ID</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox onkeypress="return numbersonly()" id=_txtIDQualitative runat="server" MaxLength="200" Width="200px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Description</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox onkeypress="return kutip_satu()" id=_txtParamName runat="server" MaxLength="500" Width="200px"></asp:textbox></TD></TR>
              <tr>
                <TD></TD></tr>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                ><asp:button id=_btnFind runat="server" Width="180px" Text="Find Parameter" CssClass="button1"></asp:button>&nbsp; 
<asp:button id=_btnNew runat="server" Width="180px" Text="New Parameter" CssClass="button1"></asp:button>&nbsp; 
                </TD></TR></TABLE></TD></TR></TABLE></TD></TR>
  <TR>
    <TD align=center colSpan=2>&nbsp;</TD></TR>
  <TR>
    <TD colSpan=2><ASP:DATAGRID id=DatGrd runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" PageSize="10">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="ID" HeaderText="ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESCRIPTION" HeaderText="Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="REASON_CODE" HeaderText="Rule Reason Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="QUERYGETVALUE" HeaderText="Query Get">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="COLUMNNAME" HeaderText="Param Request">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISQUERY" HeaderText="Operator">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="QUERYCOMPARATION" HeaderText="Result">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISACTIVE" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" HeaderText="Function" CommandName="Edit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD></TR>
  <TR id=TR_EDIT_PARAMETER runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td id=TableEdit1 style="WIDTH: 590px; HEIGHT: 140px" 
      height=140 cellSpacing=1 cellPadding=1 width=590 border=1 
      >
        <TR>
          <TD class=tdHeader1>Edit Parameter</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE id=TableEdit2 cellSpacing=0 cellPadding=0 width="100%" 
            border=0>
              <TR>
                <TD class=TDBGColor1 width=170 
                >ID&nbsp;:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox onkeypress="return numbersonly()" id=_txtEditedID runat="server" MaxLength="20" Width="280px" Enabled="False" BackColor="Silver"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170 
                  >Description&nbsp;:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtEditedDesc runat="server" MaxLength="100" Width="280px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Rule Reason Code</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtEditedRRC runat="server" MaxLength="500" Width="200px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Query Get Value</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtEditedQueryGetValue runat="server" MaxLength="5000" Width="282px" Height="92px" TextMode="MultiLine"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Param Request</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtEditedParamRequest runat="server" MaxLength="500" Width="200px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Operator</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:dropdownlist id=_ddlEditedOperator runat="server" Width="200px" onselectedindexchanged="_ddlEditedOperator_SelectedIndexChanged"></asp:dropdownlist></TD></TR>
              <TR>
                <TD class=TDBGColor1>Result</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtEditedResult runat="server" MaxLength="500" Width="200px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Status :</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:radiobuttonlist id=_rdBtnEditedStatus Width="128px" Height="0px" RepeatDirection="Horizontal" Runat="server">
														<asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
														<asp:ListItem Value="0" Selected="False">Disabled</asp:ListItem>
													</asp:radiobuttonlist></TD></TR>
              <tr>
                <TD></TD></tr>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                ><asp:button id=_btnEditedUpdate runat="server" Width="180px" Text="Update Parameter" CssClass="button1" onclick="_btnEditedUpdate_Click"></asp:button>&nbsp; 
                </TD></TR></TABLE></TD></TR></TABLE></TD></TR>
  <TR id=TR_NEW_PARAMETER runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td id=TableNew1 style="WIDTH: 590px; HEIGHT: 140px" 
      height=140 cellSpacing=1 cellPadding=1 width=590 border=1 
      >
        <TR>
          <TD class=tdHeader1>Insert New Parameter</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE id=TableNew2 cellSpacing=0 cellPadding=0 width="100%" 
            border=0>
              <TR>
                <TD class=TDBGColor1 width=170 
                  >Description&nbsp;:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtNewDesc runat="server" MaxLength="100" Width="280px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Rule Reason Code</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtNewRuleReasonCode runat="server" MaxLength="500" Width="200px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Query Get Value</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtNewQueryGetValue runat="server" MaxLength="5000" Width="282px" Height="92px" TextMode="MultiLine"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Param Request</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtNewParamRequest runat="server" MaxLength="500" Width="200px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Operator</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:dropdownlist id=_ddlNewOperator runat="server" Width="200px"></asp:dropdownlist></TD></TR>
              <TR>
                <TD class=TDBGColor1>Result</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtNewResult runat="server" MaxLength="500" Width="200px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Status :</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:radiobuttonlist id=_rdBtnNewStatus Width="128px" Height="0px" RepeatDirection="Horizontal" Runat="server">
														<asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
														<asp:ListItem Value="0" Selected="False">Disabled</asp:ListItem>
													</asp:radiobuttonlist></TD></TR>
              <TR>
                <TD></TD></TR>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                ><asp:button id=_btnNewUpdate runat="server" Width="180px" Text="Insert" CssClass="button1" onclick="_btnNewUpdate_Click"></asp:button>&nbsp; 
                </TD></TR></TABLE></TD></TR></TABLE></TD></TR></TABLE>
<table width="100%">
  <TR>
    <TD class=tdHeader1 colSpan=3>Maker Request</TD></TR>
  <TR>
    <TD class=td vAlign=top align=center colSpan=3><asp:datagrid id=DatGridRuleReason runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" PageSize="5">
												<Columns>
													<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
													<asp:BoundColumn DataField="DESCRIPTION" HeaderText="Deskripsi" HeaderStyle-Width="30%">
														<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="REASON_CODE" HeaderText="Code" HeaderStyle-Width="10%">
														<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="QUERYGETVALUE" HeaderText="Query" HeaderStyle-Width="40%">
														<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="STATUS" HeaderText="Status" HeaderStyle-Width="15%">
														<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:BoundColumn>
													<asp:ButtonColumn Text="Edit" HeaderText="Function" CommandName="Edit">
														<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
													</asp:ButtonColumn>
													<asp:ButtonColumn Text="Delete" HeaderText="Function" CommandName="Delete">
														<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
													</asp:ButtonColumn>
												</Columns>
												<PagerStyle Mode="NumericPages"></PagerStyle>
											</asp:datagrid></TD></TR>
  <TR id=TR_RULEREASON_TEMP runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td height=140 cellSpacing=1 cellPadding=1 border=1 
       140px? HEIGHT: 100%;>
        <TR>
          <TD class=tdHeader1>Edit Rule Reason</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0 
            >
              <TR>
                <TD class=TDBGColor1 width=170 
                >ID&nbsp;:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox onkeypress="return numbersonly()" id=_txtIDRRTemp runat="server" MaxLength="20" Width="280px" Enabled="False" BackColor="Silver"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170 
                  >Description&nbsp;:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtDescRRTemp runat="server" MaxLength="100" Width="280px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Rule Reason Code</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtCodeRRTemp runat="server" MaxLength="500" Width="200px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Query Get Value</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtQueryRRTemp runat="server" MaxLength="5000" Width="282px" Height="92px" TextMode="MultiLine"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Param Request</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtParamRRTemp runat="server" MaxLength="500" Width="200px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Operator</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:dropdownlist id=_ddlOperatorTemp runat="server" Width="200px"></asp:dropdownlist></TD></TR>
              <TR>
                <TD class=TDBGColor1>Result</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtResultRRTemp runat="server" MaxLength="500" Width="200px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Status :</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:radiobuttonlist id=_rdoStatusRRTemp Width="128px" Height="0px" RepeatDirection="Horizontal" Runat="server">
										<asp:ListItem Value="1" Selected="True">Enabled</asp:ListItem>
										<asp:ListItem Value="0" Selected="False">Disabled</asp:ListItem>
									</asp:radiobuttonlist></TD></TR>
              <tr>
                <TD></TD></tr>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 >
                <asp:button id=_btnSaveRRTemp runat="server" Width="180px" Text="Update Parameter" CssClass="button1" onclick="_btnSaveRRTemp_Click"></asp:button>&nbsp; 
                </TD>
              </TR>
          </TABLE>
       </TD>
     </TR></TABLE></TD></TR></table><asp:textbox id=_txtRRID Runat="server" Visible="False"></asp:textbox></center></form>
	</body>
</HTML>
