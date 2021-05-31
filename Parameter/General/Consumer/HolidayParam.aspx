<%@ Page language="c#" Codebehind="HolidayParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.HolidayParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HolidayParam</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
         function ch_jenis(vstatus1, vstatus2)
         {
			frm = document.Form1;
	        frm.DDL_LIBURDATE.disabled = vstatus1;
	        frm.DDL_LIBURMONTH.disabled = vstatus1;
	        frm.DDL_LIBURYEAR.disabled = vstatus1;
	        frm.TXT_LIBURDESC.readOnly = vstatus1;
	        frm.DDL_PEKANYEAR.disabled = vstatus2;
	        frm.CB_HARI.disabled = vstatus2;	        
        }
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder">
						<TABLE id="Table2" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408">
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
					<TD class="tdHeader1" colSpan="2">Parameter Holiday Maker</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:radiobutton id="RB_LIBURNAS" runat="server" Text="Libur Nasional" GroupName="RBLIBUR" AutoPostBack="True"
							Checked="True" oncheckedchanged="RB_LIBURNAS_CheckedChanged"></asp:radiobutton>
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 128px; HEIGHT: 15px">Tanggal</TD>
								<TD style="WIDTH: 9px; HEIGHT: 15px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 15px"><asp:dropdownlist id="DDL_LIBURDATE" runat="server"></asp:dropdownlist>&nbsp;
									<asp:dropdownlist id="DDL_LIBURMONTH" runat="server"></asp:dropdownlist>&nbsp;
									<asp:dropdownlist id="DDL_LIBURYEAR" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 128px">Deskripsi Libur</TD>
								<TD style="WIDTH: 9px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_LIBURDESC" runat="server" Width="248px"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:radiobutton id="RB_AHIRPEKAN" runat="server" Text="Akhir Pekan" GroupName="RBLIBUR" AutoPostBack="True" oncheckedchanged="RB_AHIRPEKAN_CheckedChanged"></asp:radiobutton>
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 128px">Tahun</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_PEKANYEAR" runat="server" Enabled="False"></asp:dropdownlist><asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 128px">Hari</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue">
									<asp:RadioButtonList id="CB_HARI" runat="server" Enabled="False" RepeatDirection="Horizontal">
										<asp:ListItem Value="Sabtu" Selected="True">Sabtu</asp:ListItem>
										<asp:ListItem Value="Minggu">Minggu</asp:ListItem>
									</asp:RadioButtonList></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" Text="Save" Runat="server" CssClass="button1" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" Text="Cancel" Runat="server" CssClass="button1" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label><asp:textbox id="TXT_HL_TYPE" runat="server" Visible="False">01</asp:textbox><asp:textbox id="TXT_HL_CODE" runat="server" Visible="False"></asp:textbox>
						<asp:TextBox id="TXT_HL_DATE_LAMA" runat="server" Visible="False"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Current Holiday Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="100%" colSpan="2"><asp:datagrid id="DG_HOLIDAY" runat="server" Width="100%" AllowPaging="True" PageSize="30" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="LBL_NO" runat="server"></asp:Label>&nbsp;
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="HL_DATE" HeaderText="Tanggal Libur">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HL_DESC" HeaderText="Deskripsi Libur">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HL_TYPE" HeaderText="Tipe Libur">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="LB_EDIT" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="LB_DELETE" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="HL_TYPE" Visible="False">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HL_CODE" Visible="False">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Maker Request</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DG_THOLIDAY" runat="server" Width="100%" AllowPaging="True" PageSize="30" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="No">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="LBL_TNO" runat="server"></asp:Label>&nbsp;
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="HL_DATE" HeaderText="Tanggal Libur">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HL_DESC" HeaderText="Deskripsi Libur">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HL_TYPE" HeaderText="Tipe Libur">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HL_TYPE" Visible="False">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="HL_CODE" Visible="False">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2">&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
