<%@ Page language="c#" Codebehind="MitraKaryaCompParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.MitraKaryaCompParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MitraKaryaCompParam</title>
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
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="101%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													General&nbsp;Maker</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="tdNoBorder" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A>
									<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter Mitra Karya Company Maker</TD>
				</TR>
				<TR>
					<TD class="td">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">(*) Company Induk</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 7px"><asp:dropdownlist id="DDL_COMP_INDUK" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_COMP_INDUK_SelectedIndexChanged"></asp:dropdownlist><asp:label id="LBL_MTKCODE" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_MKICODE" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Company Source&nbsp;Code</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_COMP_CODE" runat="server" CssClass="Mandatory"
										Columns="10" Enabled="False"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">(*) Company Name</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 23px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_NAME" runat="server" MaxLength="50" Columns="60"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px; HEIGHT: 25px">(*) Branch&nbsp;</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 25px"><asp:dropdownlist id="DDL_BRANCH" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Sub Interest</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_SUB_INT" runat="server" MaxLength="5" Columns="5">0</asp:textbox>&nbsp;%</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Expire Date</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_EXP_DAY" runat="server" Columns="3" MaxLength="2"></asp:textbox><asp:dropdownlist id="DDL_EXP_MONTH" runat="server"></asp:dropdownlist><asp:textbox onkeypress="return numbersonly()" id="TXT_EXP_YEAR" runat="server" Columns="5" MaxLength="4"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Guarantor Line</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_GRLINE" onkeyup='curr("Form1.TXT_GRLINE")' runat="server" MaxLength="15">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Realisasi</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_REAL" onkeyup='curr("Form1.TXT_REAL")' runat="server" MaxLength="15">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Remaining Guarantor's Line</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_REMAIN_GR" onkeyup='curr("Form1.TXT_REMAIN_GR")' runat="server" MaxLength="15">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Company Rating</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_COMP_RATE" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Presentase Potongan Gaji</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_PCT_SALARY" runat="server" Columns="5" MaxLength="4">0</asp:textbox>&nbsp;%</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Limit Individu (maximal)</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_LIMIT" onkeyup='curr("Form1.TXT_LIMIT")' runat="server" Columns="20" MaxLength="15">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Tenor Individu</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" runat="server" id="TXT_TENOR" MaxLength="3" Columns="5">0</asp:textbox>&nbsp;bulan</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Cover THT</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_COVER_THT" runat="server"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Blocked</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_BLOCKED" runat="server"></asp:checkbox>
									<asp:label id="LBL_SAVEMODE" runat="server" Visible="False"></asp:label>
									<asp:Label id="Label2" runat="server" Visible="False" Width="48px">Label</asp:Label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" Width="70px" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button>&nbsp;
						<asp:button id="BTN_FIND" CssClass="button1" Width="70px" Runat="server" Text="Find" ToolTip="Use field with mark (*) to find " onclick="BTN_FIND_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_EXPORT" CssClass="button1" Runat="server" Text="Export to Excel" onclick="BTN_EXPORT_Click"></asp:button>&nbsp;
						<asp:button id="BTN_DOWNLOAD" runat="server" Visible="False" CssClass="Button1" Width="124px"
							Text="Download Excel" onclick="BTN_DOWNLOAD_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Current&nbsp;Mitra Karya Company Table
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2">
						<asp:datagrid id="DGExisting" runat="server" Width="100%" PageSize="20" AutoGenerateColumns="False"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="MKI_CODE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MTK_CODE"></asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_SRCCODE" HeaderText="Company Source Code">
									<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MKI_DESC" HeaderText="Company Induk">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_DESC" HeaderText="Company Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_SUBINTEREST" HeaderText="Sub Interest">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_GRLINE" HeaderText="Guarantor Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_PLAFOND" HeaderText="Realisasi" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="REMAIN" HeaderText="Remaining Guarantor's Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfDetail" runat="server" CommandName="view">Detail</asp:LinkButton>&nbsp;
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
					<TD class="td" colSpan="2">
						<asp:datagrid id="DGRequest" runat="server" Width="100%" PageSize="5" AutoGenerateColumns="False"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="MKI_CODE"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MTK_CODE"></asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_SRCCODE" HeaderText="Company Source Code">
									<HeaderStyle CssClass="tdSmallHeader" Width="8%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MKI_DESC" HeaderText="Company Induk">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_DESC" HeaderText="Company Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_SUBINTEREST" HeaderText="Sub Interest">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_GRLINE" HeaderText="Guarantor Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MTK_PLAFOND" HeaderText="Realisasi" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="REMAIN" HeaderText="Remaining Guarantor's Line" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="STATUS" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
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
