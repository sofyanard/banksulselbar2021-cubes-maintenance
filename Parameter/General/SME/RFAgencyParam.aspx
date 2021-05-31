<%@ Page language="c#" Codebehind="RFAgencyParam.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFAgencyParam" %>
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
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="/SME/image/Back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A><A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A></TD>
					</TR>
					<TR>
						<TD class="tdNoBorder"></TD>
						<TD class="tdNoBorder" align="right"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Agency</TD>
					</TR>
					<TR id="TR_COMPANY" runat="Server">
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">ID</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_AGENCY_ID" onkeypress="return kutip_satu()" runat="server" MaxLength="10"
											CssClass="mandatory"></asp:textbox><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Nama Agency</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_AGENCY_NAME" onkeypress="return kutip_satu()" runat="server" MaxLength="50"
											CssClass="mandatory" Width="312px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Alamat</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_AGENCY_ADDR1" onkeypress="return kutip_satu()" runat="server" CssClass="mandatory"
											Width="312px" MaxLength="100"></asp:textbox><BR>
										<asp:textbox id="TXT_AGENCY_ADDR2" onkeypress="return kutip_satu()" runat="server" Width="312px"
											MaxLength="100"></asp:textbox><BR>
										<asp:textbox id="TXT_AGENCY_ADDR3" onkeypress="return kutip_satu()" runat="server" Width="312px"
											MaxLength="100"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 129px">Kota</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_AGENCY_CITY" onkeypress="return kutip_satu()" runat="server" CssClass="mandatory"
											Width="175px" ReadOnly="True"></asp:textbox><asp:label id="LBL_AGENCY_CITY" runat="server" Visible="False"></asp:label></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Kode Pos</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_AGENCY_ZIPCODE" onkeypress="return kutip_satu()" runat="server" MaxLength="7"
											CssClass="mandatory" AutoPostBack="True" Columns="6" ontextchanged="TXT_AGENCY_ZIPCODE_TextChanged"></asp:textbox><asp:button id="BTN_SEARCHCOMP" runat="server" Text="Search" onclick="BTN_SEARCHCOMP_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1">No. Telepon</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly();" id="TXT_AGENCY_PHNAREA" runat="server" MaxLength="5"
											CssClass="mandatory" Columns="4"></asp:textbox><asp:textbox onkeypress="return numbersonly();" id="TXT_AGENCY_PHNNUM" runat="server" MaxLength="50"
											CssClass="mandatory" Columns="10"></asp:textbox>&nbsp;Ext.
										<asp:textbox onkeypress="return numbersonly();" id="TXT_AGENCY_PHNEXT" runat="server" MaxLength="5"
											Columns="3"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">No. Fax</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly();" id="TXT_AGENCY_FAXAREA" runat="server" MaxLength="5"
											Columns="4"></asp:textbox><asp:textbox onkeypress="return numbersonly();" id="TXT_AGENCY_FAXNUM" runat="server" MaxLength="50"
											Columns="10"></asp:textbox>&nbsp;Ext.
										<asp:textbox onkeypress="return numbersonly();" id="TXT_AGENCY_FAXEXT" runat="server" MaxLength="5"
											Columns="3"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Email</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_AGENCY_EMAIL" onkeypress="return kutip_satu()" runat="server" MaxLength="200"
											Width="320px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Tipe Agency</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_AGENCYTYPE" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Wilayah Kerja</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CITY" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2"><asp:button id="BTN_SAVE" runat="server" CssClass="Button1" Width="101" Text="Save" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
							<asp:button id="BTN_CANCEL" CssClass="button1" Width="101px" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" vAlign="top" align="center" width="50%" colSpan="2">Existing 
							Agency</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2"><ASP:DATAGRID id="DGR_AGENCY_EXIST" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="AGENCYID" HeaderText="ID">
										<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AGENCYNAME" HeaderText="Nama">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AGENCY_ADDR" HeaderText="Alamat">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AGENCY_CITY" HeaderText="Kota">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AGENCY_ZIPCODE" HeaderText="Kode Pos">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AGENCY_PHN" HeaderText="Telepon">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AGENCY_FAX" HeaderText="Fax">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AGENCY_EMAIL" HeaderText="Email">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AGENCYTYPEID" HeaderText="AGENCYTYPEID"></asp:BoundColumn>
									<asp:BoundColumn DataField="AGENCYTYPEDESC" HeaderText="Tipe Agency">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CITYID" HeaderText="CITYID"></asp:BoundColumn>
									<asp:BoundColumn DataField="CITYNAME" HeaderText="Wilayah Kerja">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE" HeaderText="ACTIVE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2"></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" style="HEIGHT: 21px" vAlign="top" align="center" width="50%" colSpan="2">Agency 
							Requested</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="50%" colSpan="2"><ASP:DATAGRID id="DGR_AGENCY_REQ" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
								AllowPaging="True" onselectedindexchanged="DGR_AGENCY_REQ_SelectedIndexChanged">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="AGENCYID" HeaderText="ID">
										<HeaderStyle Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AGENCYNAME" HeaderText="Nama">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AGENCY_ADDR" HeaderText="Alamat">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AGENCY_CITY" HeaderText="Kota">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AGENCY_ZIPCODE" HeaderText="Kode Pos">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="AGENCY_PHN" HeaderText="Telepon">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AGENCY_FAX" HeaderText="Fax">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AGENCY_EMAIL" HeaderText="Email">
										<HeaderStyle Width="700px" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AGENCYTYPEID" HeaderText="AGENCYTYPEID"></asp:BoundColumn>
									<asp:BoundColumn DataField="AGENCYTYPEDESC" HeaderText="Tipe Agency">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CITYID" HeaderText="CITYID"></asp:BoundColumn>
									<asp:BoundColumn DataField="CITYNAME" HeaderText="Wilaya Kerja">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
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
