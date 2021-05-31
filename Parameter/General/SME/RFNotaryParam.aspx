<%@ Page language="c#" Codebehind="RFNotaryParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFNotaryParam" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Parameter Setup - Notary</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!-- #include file="../../../include/cek_mandatory.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder">
							<TABLE id="Table4">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Setup</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A>
							<asp:ImageButton id="BTN_BACK" runat="server" ImageUrl="/SME/image/Back.jpg" onclick="BTN_BACK_Click"></asp:ImageButton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A><A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD class="tdNoBorder"></TD>
						<TD class="tdNoBorder" align="right"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Notary</TD>
					</TR>
					<TR id="TR_COMPANY" runat="Server">
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">ID</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_NT_ID" runat="server" CssClass="mandatory"
											MaxLength="10"></asp:textbox>
										<asp:Label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:Label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Nama Notary</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_NT_NAME" runat="server" CssClass="mandatory"
											Width="312px" MaxLength="35"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Alamat</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_NT_ADDR1" runat="server" CssClass="mandatory"
											Width="312px"></asp:textbox><BR>
										<asp:textbox onkeypress="return kutip_satu()" id="TXT_NT_ADDR2" runat="server" Width="312px"></asp:textbox><BR>
										<asp:textbox onkeypress="return kutip_satu()" id="TXT_NT_ADDR3" runat="server" Width="312px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Kota</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_NT_CITY" runat="server" CssClass="mandatory"
											ReadOnly="True" Width="175px"></asp:textbox><asp:label id="LBL_NT_CITY" runat="server" Visible="False"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Kode Pos</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_NT_ZIPCODE" runat="server" CssClass="mandatory"
											Columns="6" AutoPostBack="True" MaxLength="6" ontextchanged="TXT_NT_ZIPCODE_TextChanged"></asp:textbox><asp:button id="BTN_SEARCHCOMP" runat="server" Text="Search" onclick="BTN_SEARCHCOMP_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1">No. Telepon</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:textbox id="TXT_NT_PHNAREA" runat="server" CssClass="mandatory" Columns="4" MaxLength="5"
											onkeypress="return numbersonly();"></asp:textbox>
										<asp:textbox id="TXT_NT_PHNNUM" runat="server" CssClass="mandatory" Columns="10" MaxLength="15"
											onkeypress="return numbersonly();"></asp:textbox>&nbsp;Ext.
										<asp:textbox id="TXT_NT_PHNEXT" runat="server" Columns="3" MaxLength="5" onkeypress="return numbersonly();"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">No. Fax</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:textbox id="TXT_NT_FAXAREA" runat="server" Columns="4" MaxLength="3" onkeypress="return numbersonly();"></asp:textbox>
										<asp:textbox id="TXT_NT_FAXNUM" runat="server" Columns="10" MaxLength="15" onkeypress="return numbersonly();"></asp:textbox>&nbsp;Ext.
										<asp:textbox id="TXT_NT_FAXEXT" runat="server" Columns="3" MaxLength="5" onkeypress="return numbersonly();"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Email</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_NT_EMAIL" runat="server" Width="320px"
											MaxLength="50"></asp:textbox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2"><asp:button id="BTN_SAVE" runat="server" CssClass="Button1" Width="100px" Text="Save" onclick="BTN_SAVE_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" align="center" width="50%" colSpan="2">Existing 
							Notary</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2"><ASP:DATAGRID id="DGR_NOTARY_EXIST" runat="server" Width="100%" AllowPaging="True" CellPadding="1"
								AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="NTID" HeaderText="ID">
										<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_NAME" HeaderText="Nama">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_ADDR" HeaderText="Alamat">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_CITY" HeaderText="Kota">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_ZIPCODE" HeaderText="Kode Pos">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_PHN" HeaderText="Telepon">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_FAX" HeaderText="Fax">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_EMAIL" HeaderText="Email">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" CommandName="Edit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Delete" CommandName="Delete">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" style="HEIGHT: 21px" vAlign="top" align="center" width="50%" colSpan="2">Notary 
							Requested</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2"><ASP:DATAGRID id="DGR_NOTARY_REQ" runat="server" Width="100%" AllowPaging="True" CellPadding="1"
								AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="NTID" HeaderText="ID">
										<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_NAME" HeaderText="Nama">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_ADDR" HeaderText="Alamat">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_CITY" HeaderText="Kota">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_ZIPCODE" HeaderText="Kode Pos">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_PHN" HeaderText="Telepon">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_FAX" HeaderText="Fax">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NT_EMAIL" HeaderText="Email">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Pending Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:ButtonColumn Text="Edit" CommandName="Edit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
									</asp:ButtonColumn>
									<asp:ButtonColumn Text="Delete" CommandName="Delete">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:ButtonColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
