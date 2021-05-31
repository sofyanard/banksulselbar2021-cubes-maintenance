<%@ Page language="c#" Codebehind="Template.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Scoring.Small.Template" %>
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
    <TD class=tdNoBorder style="WIDTH: 929px" align=left width=929 
    >
      <TABLE id=Table3>
        <TR>
          <TD class=tdBGColor2 style="WIDTH: 400px" align=center 
          ><B>Scoring : 
          Template</B></TD></TR></TABLE></TD>
    <td align=right><A href="../../../Body.aspx" ><IMG src="../../../Image/MainMenu.jpg" ></A><A href="../../../Logout.aspx" target=_top ><IMG src="../../../Image/Logout.jpg" ></A></TD></TR>
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
                <TD class=TDBGColor1 width=170>ID :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtID runat="server" MaxLength="20" Width="232px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170>Name :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtName runat="server" MaxLength="20" Width="232px"></asp:textbox></TD></TR>
              <tr>
                <TD></TD></TR>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                ><asp:button id=_btnFindTemplate runat="server" Width="180px" Text="Find Template" CssClass="button1"></asp:button>&nbsp; 
<asp:button id=_btnNewTemplate runat="server" Width="180px" Text="New Template" CssClass="button1" onclick="_btnNewTemplate_Click"></asp:button>&nbsp; 
													<!-- <asp:button id="_btnNew" runat="server" Width="180px" Text="New Rule" CssClass="button1"></asp:button>&nbsp;--></TD></TR></TABLE></TD></TR></TABLE></TD></TR>
  <TR>
    <TD align=center colSpan=2>&nbsp;</TD></TR>
  <TR align=center>
    <TD colSpan=2><ASP:DATAGRID id=DatGrd runat="server" Width="90%" CellPadding="1" AutoGenerateColumns="False" AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="ID" HeaderText="ID">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESC" HeaderText="TEMPLATE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="30%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BASEPOINT" HeaderText="BASE POINT">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISACTIVE" HeaderText="STATUS">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="ViewAttribute" HeaderText="Attribute" CommandName="ViewAttribute">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="ViewRuleReason" HeaderText="RuleReason" CommandName="ViewRuleReason">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="11%"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="ViewCutOff" HeaderText="CutOff" CommandName="ViewCutOff">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="11%"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Enable" HeaderText="ENABLE" CommandName="Enable">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Disable" HeaderText="DISABLE" CommandName="Disable">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Edit" HeaderText="EDIT" CommandName="Edit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD></TR>
  <TR id=TR_NEW_TEMPLATE align=center runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td id=Table6 style="WIDTH: 590px; HEIGHT: 90px" height=140 
      cellSpacing=1 cellPadding=1 width=590 border=1>
        <TR>
          <TD class=tdHeader1>New Template</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE id=Table5 cellSpacing=0 cellPadding=0 width="100%" border=0 
            >
              <TR>
                <TD class=TDBGColor1 width=170 
                  >Description :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtNewTemplateDesc runat="server" MaxLength="20" Width="306px" Height="73px" TextMode="MultiLine" BackColor="Transparent" ForeColor="Transparent"></asp:textbox></TD></TR>
              <tr>
                <td></TD></TR>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                ><asp:button id=_btnNewTemplateSave runat="server" Width="180px" Text="Save" CssClass="button1" onclick="_btnNewTemplateSave_Click"></asp:button></TD></TR></TABLE></TD></TR></TABLE></TD></TR>
  <TR id=TR_EDIT_TEMPLATE align=center runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td id=Table10 style="WIDTH: 590px; HEIGHT: 140px" height=140 
      cellSpacing=1 cellPadding=1 width=590 border=1>
        <TR>
          <TD class=tdHeader1>Edit Template</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE id=Table11 cellSpacing=0 cellPadding=0 width="100%" border=0 
            >
              <TR>
                <TD class=TDBGColor1 width=170>ID :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtEditTempID runat="server" MaxLength="20" Width="232px" BackColor="Silver" ForeColor="Transparent" Enabled="False"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170 
                  >Description :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtEditTempDesc runat="server" MaxLength="20" Width="306px" Height="62px" TextMode="MultiLine" BackColor="Transparent" ForeColor="Transparent"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Status :</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:radiobuttonlist id=_rdoEditTempStats runat="server" Width="150px" AutoPostBack="True" RepeatDirection="Horizontal">
														<asp:ListItem Value="1" Selected="True">Enable</asp:ListItem>
														<asp:ListItem Value="0">Disable</asp:ListItem>
													</asp:radiobuttonlist></TD></TR>
              <tr>
                <TD></TD></TR>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                ><asp:button id=_btnEditedTemplateSave runat="server" Width="180px" Text="Save Changed" CssClass="button1" onclick="_btnEditedTemplateSave_Click"></asp:button>&nbsp;</TD></TR></TABLE></TD></TR></TABLE></TD></TR>
  <TR></TR>
  <TR id=TR_GRID_ATTRIBUTE align=center runat="server">
    <TD colSpan=2><ASP:DATAGRID id=DatGrdAttribute runat="server" Width="80%" CellPadding="1" AutoGenerateColumns="False" AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="DESC" HeaderText="Template">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="30%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESCRIPT" HeaderText="ATTRIBUTE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="50%"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" HeaderText="Edit Value" CommandName="EditValue">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Delete" HeaderText="DELETE" CommandName="Delete">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD></TR>
  <tr></TR>
  <TR id=TR_ADD_ATTRIBUTE align=center runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td id=Table1 style="WIDTH: 590px; HEIGHT: 140px" height=140 
      cellSpacing=1 cellPadding=1 width=590 border=1>
        <TR>
          <TD class=tdHeader1>Add Attribute</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE id=Table21 cellSpacing=0 cellPadding=0 width="100%" border=0 
            >
              <TR>
                <TD class=TDBGColor1 width=170>Template 
                :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtAddAttributeTemplate runat="server" MaxLength="20" Width="272px" BackColor="Silver" Enabled="False"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170>Attribute 
                :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:dropdownlist id=DDL_ATTRIBUTE runat="server" Width="272px"></asp:dropdownlist></TD></TR>
              <tr>
                <TD></TD></TR>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                >&nbsp; <asp:button id=_btnAddAttribute runat="server" Width="180px" Text="Add Attribute" CssClass="button1" onclick="_btnAddAttribute_Click"></asp:button>&nbsp; 
                </TD></TR></TABLE></TD></TR></TABLE></TD></TR>
  <TR id=TR_ATTRIBUTE_NONRANGE runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td style="WIDTH: 590px; HEIGHT: 140px" height=140 
      cellSpacing=1 cellPadding=1 width=590 border=1>
        <TR>
          <TD class=tdHeader1><asp:label id=LabelHeaderAttributeNonRange runat="server">Attribute Non Range</asp:label></TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0 
            >
              <TR>
                <TD class=TDBGColor1 width=170 
                  >ATTRIBUTE&nbsp;:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox onkeypress="return digitsonly()" id=_txtEditedAttributeNonRangeID runat="server" MaxLength="20" Width="376px" Height="22px" TextMode="MultiLine" BackColor="Silver" Enabled="False"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 style="HEIGHT: 15px" width=170 
                >DESCRIPTION :</TD>
                <TD style="HEIGHT: 15px" width=5></TD>
                <TD class=TDBGColorValue style="HEIGHT: 15px" 
                ><asp:dropdownlist id=DDL_EDITATTNONRANGE runat="server" Width="280px" AutoPostBack="True" onselectedindexchanged="DDL_EDITATTNONRANGE_SelectedIndexChanged"></asp:dropdownlist></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170>WEIGHT 
:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox onkeypress="return digitsonly()" id=_txtNewAttributeNonRangeWeight runat="server" MaxLength="200" Width="280px"></asp:textbox></TD></TR>
              <tr>
                <TD></TD></TR>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                ><asp:button id=_btnNewAttributeNonRange runat="server" Width="180px" Text="Update Attribute" CssClass="button1" onclick="_btnNewAttributeNonRange_Click"></asp:button>&nbsp; 
                </TD></TR></TABLE></TD></TR></TABLE></TD></TR>
  <TR id=TR_ATTRIBUTE_RANGE runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td style="WIDTH: 590px; HEIGHT: 140px" height=140 
      cellSpacing=1 cellPadding=1 width=590 border=1>
        <TR>
          <TD class=tdHeader1><asp:label id=Label1 runat="server">Attribute Range</asp:label></TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0 
            >
              <TR>
                <TD class=TDBGColor1 style="HEIGHT: 23px" width=170 
                >ATTRIBUTE :</TD>
                <TD style="HEIGHT: 23px" width=5></TD>
                <TD class=TDBGColorValue style="HEIGHT: 23px" 
                ><asp:textbox onkeypress="return digitsonly()" id=TXT_EditedAttributeRangeID runat="server" MaxLength="20" Width="376px" Height="20px" TextMode="MultiLine" BackColor="Silver" Enabled="False"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170 
                  >DESCRIPTION :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:dropdownlist id=DDL_EDITATTRANGE runat="server" Width="280px" AutoPostBack="True" onselectedindexchanged="DDL_EDITATTRANGE_SelectedIndexChanged"></asp:dropdownlist></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170>LOWEST 
:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox onkeypress="return digitsonly()" id=TXT_EditedAttributeRangeLowest runat="server" MaxLength="200" Width="280px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170>HIGHEST 
                :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox onkeypress="return digitsonly()" id=TXT_EditedAttributeRangeHighest runat="server" MaxLength="200" Width="280px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170>WEIGHT 
:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox onkeypress="return digitsonly()" id=TXT_EditedAttributeRangeWeight runat="server" MaxLength="200" Width="280px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170>CONDITION 
                :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:radiobuttonlist id=RDO_EditedStatus runat="server" Width="352px" Enabled="False" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="RDO_EditedStatus_SelectedIndexChanged">
														<asp:ListItem Value="3" Selected="True">BELOW</asp:ListItem>
														<asp:ListItem Value="2">HIGH</asp:ListItem>
														<asp:ListItem Value="1">NO INFORMATION</asp:ListItem>
														<asp:ListItem Value="0">NORMAL</asp:ListItem>
													</asp:radiobuttonlist></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170>ACTION 
:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:radiobuttonlist id=RDO_Action runat="server" Width="336px" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="RDO_Action_SelectedIndexChanged">
														<asp:ListItem Value="2">Delete Attribute</asp:ListItem>
														<asp:ListItem Value="1" Selected="True">Update Attribute</asp:ListItem>
														<asp:ListItem Value="0">Add Attribute</asp:ListItem>
													</asp:radiobuttonlist></TD></TR>
              <tr>
                <TD></TD></TR>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                ><asp:button id=_btnEditedAttributeRangeWeight runat="server" Width="180px" Text="Update Attribute" CssClass="button1" onclick="_btnEditedAttributeRangeWeight_Click"></asp:button>&nbsp; 
                </TD></TR></TABLE></TD></TR></TABLE></TD></TR>
  <TR id=TR_GRID_RULEREASON align=center runat="server">
    <TD colSpan=2><ASP:DATAGRID id=DatGridRuleReason runat="server" Width="80%" AutoGenerateColumns="False" AllowPaging="True" PageSize="10">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="DESC" HeaderText="Template">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESCRIPTION" HeaderText="Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="40%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="REASON_CODE" HeaderText="Rule Reason Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="QUERYCOMPARATION" HeaderText="Result">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" HeaderText="Edit" CommandName="Edit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Delete" HeaderText="Delete" CommandName="Delete">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD></TR>
  <TR></TR>
  <TR id=TR_EDIT_RULE_REASON runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td id=TableEdit111 style="WIDTH: 590px; HEIGHT: 140px" 
      height=140 cellSpacing=1 cellPadding=1 width=590 border=1 
      >
        <TR>
          <TD class=tdHeader1>Edit Rule Reason</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE id=TableEdit222 cellSpacing=0 cellPadding=0 width="100%" 
            border=0>
              <TR>
                <TD class=TDBGColor1 width=170 
                  >Description&nbsp;:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtEditedRRDesc runat="server" MaxLength="20" Width="376px" Height="42px" TextMode="MultiLine" BackColor="Silver" Enabled="False"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170>Rule 
                  Reason Code :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtEditedRRCode runat="server" MaxLength="20" Width="336px" BackColor="Silver" ForeColor="Transparent" Enabled="False"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Result :</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtEditedRRResult runat="server" MaxLength="50" Width="336px"></asp:textbox></TD></TR>
              <tr>
                <TD></TD></TR>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                ><asp:button id=_btnEditedUpdate runat="server" Width="180px" Text="Update Parameter" CssClass="button1" onclick="_btnEditedUpdate_Click"></asp:button>&nbsp; 
                </TD></TR></TABLE></TD></TR></TABLE></TD></TR>
  <tr></TR>
  <TR id=TR_ADD_RULE_REASON align=center runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td id=Table150 style="WIDTH: 590px; HEIGHT: 140px" height=140 
      cellSpacing=1 cellPadding=1 width=590 border=1>
        <TR>
          <TD class=tdHeader1>Add Rule Reason</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE id=Table220 cellSpacing=0 cellPadding=0 width="100%" border=0 
            >
              <TR>
                <TD class=TDBGColor1 width=170>Template 
                :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=TXT_TEMPLATE_R runat="server" MaxLength="20" Width="336px" BackColor="Silver" Enabled="False"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 style="HEIGHT: 16px" width=170 
                >Rule Reason :</TD>
                <TD style="HEIGHT: 16px" width=5></TD>
                <TD class=TDBGColorValue style="HEIGHT: 16px" 
                ><asp:dropdownlist id=DDL_RULE_REASON runat="server" Width="336px"></asp:dropdownlist></TD></TR>
              <tr>
                <TD></TD></TR>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                >&nbsp; <asp:button id=BTN_ADD_RULEREASON runat="server" Width="180px" Text="Add Rule Reason" CssClass="button1" onclick="BTN_ADD_RULEREASON_Click"></asp:button>&nbsp; 
                </TD></TR></TABLE></TD></TR></TABLE></TD></TR>
  <tr></TR>
  <TR id=TR_DDL_ITEMPERTEMPLATE runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td id=TableEdit13453 style="WIDTH: 590px" height=100 
      cellSpacing=1 cellPadding=1 width=590 border=1>
        <TR>
          <TD class=tdHeader1>Item Cut Off</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE id=TableEdit234543 cellSpacing=0 cellPadding=0 width="100%" 
            border=0>
              <TR id=Tr2 runat="server">
                <TD class=TDBGColor1 style="HEIGHT: 6px" width=170 
                >Item :</TD>
                <TD style="HEIGHT: 6px" width=5></TD>
                <TD class=TDBGColorValue style="HEIGHT: 6px" 
                ><asp:dropdownlist id=DDL_ITEMPERTEMPLATE runat="server" Width="288px" AutoPostBack="True" onselectedindexchanged="DDL_ITEMPERTEMPLATE_SelectedIndexChanged"></asp:dropdownlist></TD></TR>
              <TR>
                <TD class=TDBGColor1 style="HEIGHT: 49px" width=170 
                >Condition&nbsp;:</TD>
                <TD style="HEIGHT: 49px" width=5></TD>
                <TD class=TDBGColorValue style="HEIGHT: 49px" 
                ><asp:textbox id=_txtCondition runat="server" MaxLength="20" Width="376px" Height="66px" TextMode="MultiLine" BackColor="Transparent"></asp:textbox></TD></TR>
              <TR id=Tr1 runat="server">
                <TD class=TDBGColor1 style="HEIGHT: 6px" width=170 
                >Column Name&nbsp;:</TD>
                <TD style="HEIGHT: 6px" width=5></TD>
                <TD class=TDBGColorValue style="HEIGHT: 6px" 
                ><asp:textbox id=_txtColumnName runat="server" MaxLength="20" Width="376px" TextMode="SingleLine" BackColor="Transparent"></asp:textbox></TD></TR>
              <TR id=Tr3 runat="server">
                <TD class=TDBGColor1 style="HEIGHT: 6px" width=170 
                >Parameter&nbsp;:</TD>
                <TD style="HEIGHT: 6px" width=5></TD>
                <TD class=TDBGColorValue style="HEIGHT: 6px" 
                ><asp:textbox id=_txtParameter runat="server" MaxLength="20" Width="376px" TextMode="SingleLine" BackColor="Transparent"></asp:textbox></TD></TR>
              <TR id=Tr4 runat="server">
                <TD class=TDBGColor1 style="HEIGHT: 6px" width=170 
                >Operator&nbsp;:</TD>
                <TD style="HEIGHT: 6px" width=5></TD>
                <TD class=TDBGColorValue style="HEIGHT: 6px" 
                ><asp:dropdownlist id=_ddlOperatorItem Width="288px" Runat="Server"></asp:dropdownlist></TD></TR>
              <TR id=Tr5 runat="server">
                <TD class=TDBGColor1 style="HEIGHT: 6px" width=170 
                >Result&nbsp;:</TD>
                <TD style="HEIGHT: 6px" width=5></TD>
                <TD class=TDBGColorValue style="HEIGHT: 6px" 
                ><asp:textbox id=_txtResult runat="server" MaxLength="20" Width="376px" TextMode="SingleLine" BackColor="Transparent"></asp:textbox></TD></TR></TABLE></TD></TR>
        <TR>
          <TD vAlign=top align=center colSpan=3 height=10 
            >&nbsp; <asp:button id=_btnUpdateCondition runat="server" Width="180px" Text="Update Condition" CssClass="button1" onclick="_btnUpdateCondition_Click"></asp:button>&nbsp;&nbsp; 
<asp:button id=_btnViewItem runat="server" Width="180px" Text="View Item" CssClass="button1" onclick="_btnViewItem_Click"></asp:button></TD></TR></TABLE></TD></TR>
  <tr></TR>
  <TR id=TR_CUTOFF align=center runat="server">
    <TD colSpan=2><ASP:DATAGRID id=DatGridCutOff runat="server" Width="100%" CellPadding="1" AutoGenerateColumns="False" AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="DESCITEMP" HeaderText="ITEM">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SCORERESULT" HeaderText="DESCRIPTION">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="DESCLINE" HeaderText="LINE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PROPORSIACCOUNT" HeaderText="PROPORSI">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LOWESTSCORE" HeaderText="LOWESTSCORE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="HIGHESTSCORE" HeaderText="HIGHESTSCORE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ISHIGHEST" HeaderText="POSITION">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" HeaderText="EDIT" CommandName="EditValue">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Delete" HeaderText="DELETE" CommandName="Delete">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD></TR>
  <TR></TR>
  <TR id=TR_EDIT_CUTOFF_SCORE runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td id=TableEdit1 style="WIDTH: 590px; HEIGHT: 140px" 
      height=140 cellSpacing=1 cellPadding=1 width=590 border=1 
      >
        <TR>
          <TD class=tdHeader1>Cut Off Score</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE id=TableEdit2 cellSpacing=0 cellPadding=0 width="100%" 
            border=0>
              <TR>
                <TD class=TDBGColor1 width=170 
                  >Description&nbsp;:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=TXT_DESC_SCORE_CUTOFF_EDIT runat="server" MaxLength="20" Width="376px" Height="42px" TextMode="MultiLine" BackColor="Transparent"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170 
                  >Proporsi&nbsp;:</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=TXT_PROPORSI_CUTOFF_EDIT runat="server" MaxLength="20" Width="336px" BackColor="Transparent" ForeColor="Transparent"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Lowest Score :</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox onkeypress="return digitsonly()" id=TXT_LOWESTSCORE_CUTOFFEDIT runat="server" MaxLength="50" Width="336px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Highest Score :</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:textbox onkeypress="return digitsonly()" id=TXT_HIGHESTSCORE_CUTOFFEDIT runat="server" MaxLength="50" Width="336px"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Position :</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:radiobuttonlist id=RDO_POSITION_CUTOFF_EDIT runat="server" Width="112px" AutoPostBack="True" RepeatDirection="Horizontal">
														<asp:ListItem Value="2" Selected="True">Lowest</asp:ListItem>
														<asp:ListItem Value="1">Middle</asp:ListItem>
														<asp:ListItem Value="0">Highest</asp:ListItem>
													</asp:radiobuttonlist></TD></TR>
              <tr>
                <TD></TD></TR>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                ><asp:button id=BTN_EDIT_CUTOFF runat="server" Width="180px" Text="Add Parameter" CssClass="button1" onclick="BTN_EDIT_CUTOFF_Click"></asp:button>&nbsp; 
                </TD></TR></TABLE></TD></TR></TABLE></TD></TR>
  <tr></TR></TABLE>
<table width="100%">
  <TBODY>
  <TR>
    <TD class=tdHeader1 colSpan=3>Maker Request</TD></TR>
  <TR>
    <TD class=td vAlign=top align=center colSpan=3><asp:datagrid id=DataGridTemplateTemp runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" PageSize="5">
									<Columns>
										<asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
										<asp:BoundColumn DataField="DESC" HeaderText="Deskripsi">
											<HeaderStyle Width="50%" CssClass="tdSmallHeader"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="ISACTIVE" HeaderText="Active">
											<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="STATUS" HeaderText="Status">
											<HeaderStyle Width="10%" CssClass="tdSmallHeader"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
										<asp:ButtonColumn Text="Edit" HeaderText="Function" CommandName="Edit">
											<HeaderStyle CssClass="tdSmallHeader" Width="10%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
										</asp:ButtonColumn>
										<asp:ButtonColumn Text="Delete" HeaderText="Function" CommandName="Delete">
											<HeaderStyle CssClass="tdSmallHeader" Width="10%"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
										</asp:ButtonColumn>
									</Columns>
									<PagerStyle Mode="NumericPages"></PagerStyle>
								</asp:datagrid></TD></TR>
  <TR id=TR_TEMPLATE_TEMP align=center runat="server">
    <TD align=center colSpan=2>
      <TABLE class=td id=Table10 style="WIDTH: 590px; HEIGHT: 140px" height=140 
      cellSpacing=1 cellPadding=1 width=590 border=1>
        <TR>
          <TD class=tdHeader1>Edit Template</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <TABLE id=Table11 cellSpacing=0 cellPadding=0 width="100%" border=0 
            >
              <TR>
                <TD class=TDBGColor1 width=170>ID :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtIdTemplateTemp runat="server" MaxLength="20" Width="232px" BackColor="Silver" ForeColor="Transparent" Enabled="False"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1 width=170 
                  >Description :</TD>
                <TD width=5></TD>
                <TD class=TDBGColorValue><asp:textbox id=_txtIdDescTemplateTemp runat="server" MaxLength="20" Width="306px" Height="62px" TextMode="MultiLine" BackColor="Transparent" ForeColor="Transparent"></asp:textbox></TD></TR>
              <TR>
                <TD class=TDBGColor1>Status :</TD>
                <TD></TD>
                <TD class=TDBGColorValue><asp:radiobuttonlist id=_rdoStatusTemplateTemp runat="server" Width="150px" AutoPostBack="True" RepeatDirection="Horizontal">
					<asp:ListItem Value="1" Selected="True">Enable</asp:ListItem>
					<asp:ListItem Value="0">Disable</asp:ListItem>
				</asp:radiobuttonlist></TD></TR>
              <tr>
                <TD></TD></TR>
              <TR>
                <TD vAlign=top align=center colSpan=3 height=10 
                ><asp:button id=_btnTemplateTemp runat="server" Width="180px" Text="Save Changed" CssClass="button1" onclick="Button1_Click"></asp:button>&nbsp;</TD></TR></TABLE></TD></TR></TABLE></TD></TR></TBODY></TABLE></CENTER><asp:textbox id=TXT_ID_TEMPLATE runat="server" MaxLength="20" Width="336px" BackColor="Silver" Visible="False"></asp:textbox><asp:textbox id=idRuleReason runat="server" MaxLength="20" Width="336px" BackColor="Silver" Visible="False"></asp:textbox><asp:textbox id=idAttributeNonRange runat="server" MaxLength="20" Width="336px" BackColor="Silver" Visible="False"></asp:textbox><asp:textbox id=idAttributeRange runat="server" MaxLength="20" Width="336px" BackColor="Silver" Visible="False"></asp:textbox><asp:textbox id=idAttributeCutOff runat="server" MaxLength="20" Width="336px" BackColor="Silver" Visible="False"></asp:textbox></FORM></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE>
<CENTER></CENTER>
	</body>
</HTML>
