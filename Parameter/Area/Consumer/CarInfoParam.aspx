<%@ Page language="c#" Codebehind="CarInfoParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.Area.Consumer.CarInfoParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Car Info Parameter</title>
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
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
										border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													Area Maker</B></TD>
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
					<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Car Information Maker</TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" width="200">City</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 7px"><asp:dropdownlist id="DDL_CITY" runat="server" CssClass="mandatory" AutoPostBack="True" onselectedindexchanged="DDL_CITY_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="200">Car Code</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_CAR_CODE" runat="server" CssClass="Mandatory" Width="80px" MaxLength="11"></asp:textbox><asp:label id="LBL_ID" runat="server" Visible="False"></asp:label><asp:label id="LBL_ACTIVE" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="200">(*) Year</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_YEAR" runat="server" Width="80px" MaxLength="4"></asp:textbox><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" width="200">(*) Dealer</TD>
								<TD style="WIDTH: 10px">:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_DEALER" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">(*) Car Type</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CAR_TYPE" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">(*) Brand</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_BRAND" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_BRAND_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Model</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_MODEL" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_MODEL_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Seri</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_SERI" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Price</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_PRICE" onkeyup='curr("Form1.TXT_PRICE")' runat="server" Width="136px" MaxLength="15">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Min. Down Payment</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_MIN_PAYMENT" onkeyup='curr("Form1.TXT_MIN_PAYMENT")' runat="server" Width="48px"
										MaxLength="5">0</asp:textbox>&nbsp;%</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="HEIGHT: 19px">Quantity</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 19px"><asp:textbox id="TXT_QUANTITY" onkeyup='curr("Form1.TXT_QUANTITY")' runat="server" Width="80px">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Manufacture Year</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_MAN_YEAR" runat="server" Width="48px"
										MaxLength="4"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Number Of Door</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_NUM_DOOR" runat="server" Width="80px"
										MaxLength="5"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Transmission</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_TRANSMISSION" runat="server" Width="160px" MaxLength="30"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Production Type</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_PRODUCTION_TYPE" runat="server" Width="160px" MaxLength="30"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Description</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_DESC" runat="server" Width="416px" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Picture</TD>
								<TD>:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_PICTURE" runat="server" Width="416px" MaxLength="50"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button>&nbsp;
						<asp:button id="BTN_FIND" CssClass="button1" Width="70px" Text="Find" Runat="server" ToolTip="Use field with mark (*) to find " onclick="BTN_FIND_Click"></asp:button></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						<P>Current&nbsp;Car Information Table</P>
					</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGExisting" runat="server" Width="100%" AllowPaging="True" PageSize="20" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="ID_MOBILBARU" HeaderText="Code">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NM_JENIS" HeaderText="Car Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_kota"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_tahun"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_dealer"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_jns"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_merek"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_model"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_tipe"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="hargajual"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="min_dp"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="qty"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="yearofmade"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="jml_pintu"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="transmission"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="jns_produksi"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="keterangan"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="dir_gambar"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
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
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGRequest" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="ID_MOBILBARU" HeaderText="Code">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NM_JENIS" HeaderText="Car Type">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_kota"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_tahun"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_dealer"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_jns"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_merek"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_model"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="id_tipe"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="hargajual"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="min_dp"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="qty"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="yearofmade"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="jml_pintu"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="transmission"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="jns_produksi"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="keterangan"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="dir_gambar"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="ch_sta"></asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
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
