<%@ Page language="c#" Codebehind="InitialParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Consumer.InitialParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InitialParam</title>
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
						<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD style="HEIGHT: 41px" width="50%">
									<TABLE id="Table5" style="WIDTH: 408px" cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
													Maker</B></TD>
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
					<TD class="tdHeader1" colSpan="2">Parameter Initial&nbsp;Maker</TD>
				</TR>
				<TR>
					<TD class="td">
						<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px"><STRONG>Sequence Number</STRONG></TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_SEQ_NO" runat="server" Width="78px" CssClass="mandatory"
										MaxLength="10"></asp:textbox><asp:label id="LBL_DB_IP" runat="server" Visible="False"></asp:label><asp:label id="LBL_DB_NAME" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_PWD" runat="server" Visible="False"></asp:label>
									<asp:label id="LBL_LOG_ID" runat="server" Visible="False"></asp:label></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Password Life</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PASS_LIFE" runat="server" Width="78px"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Password Warning</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PASS_WARNING" runat="server" Width="78px"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Password Revoke Count</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PASS_REVOKE" runat="server" Width="78px"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Password Uniq</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PASS_UNIQ" runat="server" Width="78px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Password Digit</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PASS_DIGIT" runat="server" Width="78px"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Password Change</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PASS_CHANGE" runat="server" Width="78px"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Phone Verification Limit</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PH_VERLIM" runat="server" Width="78px"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Phone Verification More</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PH_VERMORE" runat="server" Width="78px"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1">Phone Verification Less</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PH_VERLESS" runat="server" Width="78px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px" width="217">Physical Verification Limit</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PY_VERLIM" runat="server" Width="78px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px" width="217">&nbsp;Physical Verification 
									More</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PY_VERMORE" runat="server" Width="78px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px" width="217">&nbsp;Physical Verification 
									Less</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PY_VERLESS" runat="server" Width="78px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px" width="217">Warm Application Age</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_WAA" runat="server" Width="78px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px" width="217">Caw Gross 1</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_CAW_GR1" runat="server" Width="78px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px" width="217">Caw Tax IDR 1</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_CAWIDR_TAX1" onkeyup='curr("Form1.TXT_CAWIDR_TAX1")' runat="server" Width="128px"
										MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px" width="217">Caw Tax IDR 2</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_CAWIDR_TAX2" onkeyup='curr("Form1.TXT_CAWIDR_TAX2")' runat="server" Width="128px"
										MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px" width="217">Caw Tax 1</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_CAW_TAX1" runat="server" Width="78px" MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px" width="217">Caw Tax 2</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_CAW_TAX2" runat="server" Width="78px" MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Caw Tax 3</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_CAW_TAX3" runat="server" Width="78px" MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Caw Tax 4</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_CAW_TAX4" runat="server" Width="78px" MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Cost Living</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_COST_LIVE" runat="server" Width="78px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Maximum Cost Living</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_MAX_COST" onkeyup='curr("Form1.TXT_MAX_COST")' runat="server" Width="128px"
										MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Maximal DTBO</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_MAX_DTBO" onkeyup='curr("Form1.TXT_MAX_DTBO")'
										runat="server" Width="78px" MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Expired DTBO</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_EXP_DTBO" runat="server" Width="78px"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Bank Name</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_BANK_NAME" runat="server" Width="286px"
										MaxLength="30"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Nama Gedung</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_BUILD_NAME" runat="server" Width="286px"
										MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Alamat</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_ADDR1" runat="server" Width="160px" MaxLength="50"></asp:textbox><asp:textbox onkeypress="return kutip_satu()" id="TXT_ADDR2" runat="server" Width="160px" MaxLength="50"></asp:textbox><asp:textbox onkeypress="return kutip_satu()" id="TXT_ADDR3" runat="server" Width="160px" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Kota</TD>
								<TD style="WIDTH: 8px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_CITY" runat="server" Width="198px" MaxLength="20"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Kode Pos</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_ZIPCODE" runat="server" Width="78px" MaxLength="6"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Nomor Telepon</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_PHONE" runat="server" Width="198px" MaxLength="25"></asp:textbox>&nbsp;</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">CCRA Approved</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_CCRA_APPR" runat="server" Width="198px"
										MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">CCRA Disetujui Oleh</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_CCRA_AGREE" runat="server" Width="198px"
										MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Bea materai</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_BEA_MATERAI" onkeyup='curr("Form1.TXT_BEA_MATERAI")' runat="server" Width="78px"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Limit Red</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_LIM_RED" runat="server" Width="78px" MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Limit Green</TD>
								<TD style="WIDTH: 8px; HEIGHT: 21px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 21px"><asp:textbox onkeypress="return digitsonly()" id="TXT_LIM_GREEN" runat="server" Width="78px"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Limit Yellow</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_LIM_YELLOW" runat="server" Width="78px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Aging Maximum</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_MAX_AGING" runat="server" Width="78px"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Maximum Count Aging</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_MAX_CTAG" runat="server" Width="78px"
										MaxLength="10"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Margin Rate for Different Currency</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_MG_RATE1" runat="server" Width="78px" MaxLength="5"></asp:textbox>&nbsp;%</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Margin Rate for Some Currency</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_MG_RATE2" runat="server" Width="78px" MaxLength="5"></asp:textbox>&nbsp;%</TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 217px">Foreign Interest Margin</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return digitsonly()" id="TXT_FG_IM" runat="server" Width="78px" MaxLength="5"></asp:textbox>&nbsp;%</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="td">
						<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 560px">Denda keterlambatan pembayaran 
									angsuran/per hari dihitung dari jml angsuran tertunggak</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_DENDA" onkeyup='curr("Form1.TXT_DENDA")' runat="server" Width="100px" MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 560px">Minimal denda keterlambatan pembayaran 
									angsuran/per hari</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_MIN_DENDA" onkeyup='curr("Form1.TXT_MIN_DENDA")' runat="server" Width="100px"
										MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 560px">Biaya adm untuk pelunasan sebelum jatuh 
									tempo dlm persen</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_BEA_ADM_LUNAS1" runat="server" Width="100px" MaxLength="5"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 560px">Minimal biaya adm untuk pelunasan 
									sebelum jatuh tempo</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_MINBEA_ADM1" onkeyup='curr("Form1.TXT_MINBEA_ADM1")' runat="server" Width="100px"
										MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 560px">Biaya adm untuk pelunasan sebagian 
									sebelum jatuh tempo dlm persen</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_BEA_ADM_LUNAS2" runat="server" Width="100px" MaxLength="5"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 560px">Minimal biaya adm untuk pelunasan 
									sebagian sebelum jatuh tempo</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_MINBEA_ADM2" onkeyup='curr("Form1.TXT_MINBEA_ADM2")' runat="server" Width="100px"
										MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 560px">Besarnya nilai jaminan dalam bentuk % 
									untuk fiducia</TD>
								<TD style="WIDTH: 8px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_NILAI_JAM" runat="server" Width="100px" MaxLength="5"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" Width="80px" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CANCEL" Width="80px" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button>
						<asp:label id="LBL_SAVE" runat="server" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">
						Current&nbsp; Initial Table
					</TD>
				</TR>
				<TR>
					<TD class="td" colSpan="2"><asp:datagrid id="DG" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="1">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="IN_SEQ" HeaderText="Sequence Number">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="IN_SCPWDLIVE" HeaderText="Password Live">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BANK_NAME" HeaderText="Bank Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>
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
					<TD class="td" colSpan="2"><asp:datagrid id="DG2" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5">
							<AlternatingItemStyle CssClass="tblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="IN_SEQ" HeaderText="Sequence Number">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="IN_SCPWDLIVE" HeaderText="Password Live">
									<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BANK_NAME" HeaderText="Bank Name">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="STATUS" HeaderText="Pending Status">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="CH_STA">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle Width="12%" CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
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
