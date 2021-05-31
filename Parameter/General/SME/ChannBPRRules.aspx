<%@ Page language="c#" Codebehind="ChannBPRRules.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.ChannBPRRules" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ChannBPRRules</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
							<TABLE id="Table2">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
											General</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../image/Back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG height="25" src="../../../Image/MainMenu.jpg" width="106"></A>
							<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Chann BPR Rules&nbsp;Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%" colSpan="2">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1">Company</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_COMP" runat="server" Width="190px" AutoPostBack="True" onselectedindexchanged="DDL_COMP_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 15px">Rules</TD>
									<TD style="WIDTH: 15px; HEIGHT: 15px"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_RULES" runat="server" Width="342px"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" Runat="server" Text="Save" CssClass="button1" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" Runat="server" Text="Cancel" CssClass="button1" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							<P>Current&nbsp;&nbsp;Chann BPR Rules&nbsp; Table</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="Datagrid1" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="CH_BPR_CUREF" HeaderText="CuRef">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="KANTOR" HeaderText="Company">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CH_PRM_CODE" HeaderText="Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_PRM_NAME" HeaderText="Rules">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfDelete1" runat="server" CommandName="delete">Delete</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDetail" runat="server" CommandName="detail">Detail</asp:LinkButton>
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
						<TD class="td" style="HEIGHT: 172px" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DataGrid2" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="CH_BPR_CUREF" HeaderText="CuRef">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="KANTOR" HeaderText="Company">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CH_PRM_CODE" HeaderText="Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_PRM_NAME" HeaderText="Rules">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Status" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>&nbsp;
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR id="tr_header" runat="server">
						<TD class="tdHeader1" colSpan="2">
							<P>Current&nbsp;&nbsp;Chann BPR Rules&nbsp;Detail Value</P>
						</TD>
					</TR>
					<TR id="tr_isian" runat="server">
						<TD class="td" vAlign="top" width="50%" colSpan="2">
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1">Lower Range &gt;= &nbsp;
									</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="txt_value1" onkeypress="return digitsonly()" runat="server" MaxLength="15"></asp:textbox><asp:label id="LBL_CUREFGRD" runat="server" Visible="False"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Upper Range &lt;&nbsp;
									</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="txt_value2" onkeypress="return digitsonly()" runat="server" MaxLength="15"></asp:textbox><asp:label id="LBL_RULESGRD" runat="server" Visible="False"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 15px">Parameter</TD>
									<TD style="WIDTH: 15px; HEIGHT: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="txt_value3" runat="server" MaxLength="50"></asp:textbox><asp:label id="LBL_VALUE_CODE" runat="server" Visible="False"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 15px">Score</TD>
									<TD style="WIDTH: 15px; HEIGHT: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="txt_score" onkeypress="return digitsonly()" runat="server" MaxLength="15"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR id="tr_button" runat="server">
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:label id="LBL_RULES" runat="server" Visible="False"></asp:label><asp:label id="LBL_CUREF" runat="server" Visible="False"></asp:label><asp:button id="btn_saveDetail" Runat="server" Text="Save" CssClass="button1" onclick="btn_saveDetail_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="btn_cancelDetail" Runat="server" Text="Cancel" CssClass="button1" onclick="btn_cancelDetail_Click"></asp:button><asp:label id="lbl_savemodeDetail" runat="server" Visible="False">1</asp:label></TD>
					</TR>
					<TR id="tr_headerTable" runat="server">
						<TD class="tdHeader1" colSpan="2">
							<P>Current&nbsp;&nbsp;Table</P>
						</TD>
					</TR>
					<TR id="tr_grid" runat="server">
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DtGrd_Detail" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="KANTOR" HeaderText="Company">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_PRM_NAME" HeaderText="Rules">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_VALUE1" HeaderText="Value 1">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_VALUE2" HeaderText="Value 2">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_VALUE3" HeaderText="Value 3">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_SCORE" HeaderText="Score">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_edit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="Linkbutton1" runat="server" CommandName="delete">Delete</asp:LinkButton>&nbsp;
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="CH_BPR_CU_REF" HeaderText="cuRef"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CH_PRM_CODE" HeaderText="CH_PRM_CODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CH_VALUE_CODE" HeaderText="CH_VALUE_CODE"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR id="tr_headerTablemaker" runat="server">
						<TD class="tdHeader1" colSpan="2">Maker Request</TD>
					</TR>
					<TR id="tr_grid2" runat="server">
						<TD class="td" style="HEIGHT: 172px" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DtGrd_DetailMaker" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="KANTOR" HeaderText="Company">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_PRM_NAME" HeaderText="Rules">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_VALUE1" HeaderText="Value 1">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_VALUE2" HeaderText="Value 2">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_VALUE3" HeaderText="Value 3">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CH_SCORE" HeaderText="Score">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_edit1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="Linkbutton4" runat="server" CommandName="delete">Delete</asp:LinkButton>&nbsp;
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="CH_BPR_CU_REF" HeaderText="curef"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CH_PRM_CODE" HeaderText="CH_PRM_CODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CH_VALUE_CODE" HeaderText="CH_VALUE_CODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS" HeaderText="PENDINGSTATUS"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2">&nbsp;</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
