<%@ Page language="c#" Codebehind="FeatureProductParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.FeatureProductParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FeatureProductParam</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table2">
							<TR>
								<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
										General Maker</B></TD>
							</TR>
						</TABLE>
					</TD>
					<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
						<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter Feature Product Maker</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px" width="206">Parameter PRM</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:radiobutton id="RDB_PROMPT" runat="server" Text="Prompt" Checked="True"></asp:radiobutton></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px" width="206">Feature Name&nbsp;</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_ID" runat="server" CssClass="mandatory" Width="174px" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px" width="206">Feature Formula</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_FORMULA" runat="server" Width="536px" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px" width="206">Feature Table</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_TABLE" runat="server" Width="176px" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px" width="206">Feature Field</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_FIELD" runat="server" Width="176px" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px" width="206">Feature Link</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_LINK" runat="server" Width="536px" MaxLength="400"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px" width="206">Result Description</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_DESC" runat="server" Width="310px" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px; HEIGHT: 3px" width="206">Feature 
									Program</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 3px"><asp:dropdownlist id="DDL_PROGRAM" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_PROGRAM_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px; HEIGHT: 2px" width="206">Product Type</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px"><asp:dropdownlist id="DDL_PRODUCT" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_PRODUCT_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px; HEIGHT: 6px" width="206">Job Type</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 6px"><asp:dropdownlist id="DDL_JOB_TYPE" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_JOB_TYPE_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px" width="206">Min Value</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_MIN" runat="server" Width="176px" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px" width="206">Max value</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_MAX" runat="server" Width="176px" MaxLength="50"></asp:textbox><asp:label id="lbl_max" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 206px"></TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDB_COL_TYPE" runat="server" Width="350px" Height="8px" RepeatDirection="Horizontal">
										<asp:ListItem Value="1">Active</asp:ListItem>
										<asp:ListItem Value="0">Non Active</asp:ListItem>
									</asp:radiobuttonlist></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" Text="Save" CssClass="button1" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" Text="Cancel" CssClass="button1" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Current&nbsp;Feature Product Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="Datagrid1" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="FEATURE_ID" HeaderText="No">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FEATURE_DESC" HeaderText="Feature Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FORMULA" HeaderText="Feature Formula">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FEATURE_TABLE" HeaderText="Feature Table">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FEATURE_LINK" HeaderText="Feature Link">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PR_DESC" HeaderText="Program">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Product Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FEATURE_JOBTYPE" HeaderText="Job Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ACTIVE" HeaderText="Active">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRM_CODE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_CODE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRODUCTID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="JOB_TYPE_ID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="FEATURE_ACTIVE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MIN_VALUE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MAX_VALUE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DESCRIPTION"></asp:BoundColumn>
								<asp:BoundColumn DataField="FEATURE_FIELD" HeaderText="Feature Field">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Maker Request</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="Datagrid3" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="FEATURE_ID" HeaderText="No">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FEATURE_DESC" HeaderText="Feature Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FORMULA" HeaderText="Feature Formula">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FEATURE_TABLE" HeaderText="Feature Table">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FEATURE_LINK" HeaderText="Feature Link">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PR_DESC" HeaderText="Program">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Product Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FEATURE_JOBTYPE" HeaderText="Job Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ACTIVE" HeaderText="Active">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRM_CODE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PR_CODE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PRODUCTID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="JOB_TYPE_ID"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="FEATURE_ACTIVE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MIN_VALUE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MAX_VALUE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA"></asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="DESCRIPTION"></asp:BoundColumn>
								<asp:BoundColumn DataField="FEATURE_FIELD" HeaderText="Feature Field">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="Linkbutton1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2">&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
