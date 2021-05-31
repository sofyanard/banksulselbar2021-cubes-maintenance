<%@ Page language="c#" Codebehind="MitraKaryaIndukParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.MitraKaryaIndukParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MitraKaryaIndukParam</title>
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
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
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
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Parameter Mitra Karya Induk Maker</TD>
				</TR>
				<TR>
					<TD class="td">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="200">(*)&nbsp;Company Source&nbsp;Code</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_COMP_CODE" runat="server" Width="60px"
										CssClass="mandatory" MaxLength="4"></asp:textbox><asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label><asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="200">(*)&nbsp;Company Name</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_COMP_DESC" runat="server" Width="376px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Plafond</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 23px"><asp:textbox id="TXT_PLAFOND" onkeyup='curr("Form1.TXT_PLAFOND")' runat="server" Width="160px"
										MaxLength="20">0</asp:textbox><asp:label id="LBL_MKICODE" runat="server" Visible="False"></asp:label><asp:label id="LBL_SAVEMODE" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Blocked Status</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:checkbox id="CHK_BLOCKED" runat="server"></asp:checkbox><asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label><asp:label id="LBL_NB" runat="server" Visible="False">1</asp:label><asp:label id="Label2" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<!--add for LOS Consumer Enhancement 4-->
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Expire Date</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_EXP_DAY" runat="server" MaxLength="2"
										Columns="3"></asp:textbox><asp:dropdownlist id="DDL_EXP_MONTH" runat="server"></asp:dropdownlist><asp:textbox onkeypress="return numbersonly()" id="TXT_EXP_YEAR" runat="server" MaxLength="4"
										Columns="5"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 210px">Company Rating</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_COMP_RATE" runat="server"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Runat="server" Text="Save" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Runat="server" Text="Cancel" onclick="BTN_CANCEL_Click"></asp:button>&nbsp;
						<asp:button id="BTN_FIND" Width="70px" CssClass="button1" Runat="server" Text="Find" ToolTip="Use field with mark (*) to find " onclick="BTN_FIND_Click"></asp:button>&nbsp;
						<asp:button id="BTN_EXPORT" CssClass="button1" Runat="server" Text="Export to Excel" onclick="BTN_EXPORT_Click"></asp:button>&nbsp;
						<asp:button id="BTN_DOWNLOAD" runat="server" Width="124px" CssClass="Button1" Visible="False"
							Text="Download Excel" onclick="BTN_DOWNLOAD_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Current&nbsp;Mitra Karya Induk Table
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="110"
							AllowPaging="True">
							<AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="MKI_CODE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MKI_SRCCODE" HeaderText="Company Source Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MKI_DESC" HeaderText="Company Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GRLINE" HeaderText="Plafond" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PLAFOND" HeaderText="Distribusi" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SISA" HeaderText="Sisa Plafond Induk" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BLOCKED" HeaderText="Blocked Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MKI_EXPIREDATE2" HeaderText="Expire Date">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="RT_DESC" HeaderText="Company Rating">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="LinkDetail" runat="server" CommandName="view">Detail</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="MKI_BLOCKED">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MKI_EXPIREDATE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="RT_CODE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="BackGroundList" style="HEIGHT: 27px" colSpan="2"><b>Total Plafond :
							<asp:label id="LBL_TPLAFOND" Runat="server" ForeColor="IndianRed"></asp:label>&nbsp;&nbsp;&nbsp;Total 
							Distribusi :
							<asp:label id="LBL_TDISB" Runat="server" ForeColor="IndianRed"></asp:label>&nbsp;&nbsp;&nbsp;Sisa 
							:
							<asp:label id="LBL_SISA" Runat="server" ForeColor="IndianRed"></asp:label></b></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Maker Request</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True">
							<AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="MKI_CODE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MKI_SRCCODE" HeaderText="Company Source Code">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MKI_DESC" HeaderText="Company Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="GRLINE" HeaderText="Plafond" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PLAFOND" HeaderText="Distribusi" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SISA" HeaderText="Sisa Plafond Induk" DataFormatString="{0:N}">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BLOCKED" HeaderText="Blocked Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MKI_EXPIREDATE2" HeaderText="Expire Date">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="RT_DESC" HeaderText="Company Rating">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="STATUS" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="Linkbutton1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False" DataField="MKI_BLOCKED">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="MKI_EXPIREDATE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="RT_CODE">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
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
