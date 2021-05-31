<%@ Page language="c#" Codebehind="TATCommitment.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.TATCommitment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TATCommitment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include  file="../../../include/cek_entries.html" -->
		<!-- #include  file="../../../include/cek_mandatory.html" -->
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
											<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>TAT&nbsp;Commitment&nbsp;:&nbsp;Parameter 
													Maker</B></TD>
										</TR>
									</TABLE>
								</TD>
								<TD class="tdNoBorder" align="right"><A href="../../GeneralParamAll.aspx?moduleID=01"><IMG src="../../../Image/Back.jpg" border="0"></A>
									<A href="../../../Body.aspx"><IMG src="../../../Image/MainMenu.jpg"></A> <A href="../../../Logout.aspx" target="_top">
										<IMG src="../../../Image/Logout.jpg"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">TAT Commitment&nbsp;Maker</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="2" width="100%">
				<TR>
					<TD vAlign="top" align="center" colSpan="2">
						<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Dimention&nbsp;ID</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox id="TXT_DIMID" runat="server" ReadOnly="True" MaxLength="10" CssClass="mandatory2"
										Width="56px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Dimention&nbsp;Desc</TD>
								<TD style="WIDTH: 7px">:</TD>
								<TD class="TDBGColorValue"><asp:textbox onkeypress="return kutip_satu()" id="TXT_DIMDESC" runat="server" MaxLength="50"
										CssClass="mandatory2" Width="336px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Reference Table</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_REFTABLE" runat="server" MaxLength="100"
										Width="336px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Reference Field</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_REFFIELDID" runat="server" MaxLength="100"
										Width="336px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">Reference Field Desc</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_REFFIELDDESC" runat="server" MaxLength="100"
										Width="336px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">CUBESTable</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_CBSTABLE" runat="server" MaxLength="100"
										Width="336px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">CUBES Field</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_CBSFIELD" runat="server" MaxLength="100"
										Width="336px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="TDBGColor1" style="WIDTH: 300px">CUBES&nbsp;Link</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_CBSLINK" runat="server" MaxLength="100"
										Width="336px" TextMode="MultiLine" Height="100px"></asp:textbox></TD>
							</TR>
							<TR id="PromptTable" runat="server">
								<TD class="TDBGColor1" style="WIDTH: 300px">Prompt Table</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_PRMTABLE" runat="server" MaxLength="100"
										Width="336px"></asp:textbox></TD>
							</TR>
							<TR id="PromptField" runat="server">
								<TD class="TDBGColor1" style="WIDTH: 300px">Prompt&nbsp;Field</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_PRMFIELD" runat="server" MaxLength="100"
										Width="336px"></asp:textbox></TD>
							</TR>
							<TR id="PromptLink" runat="server">
								<TD class="TDBGColor1" style="WIDTH: 300px">Prompt&nbsp;Link</TD>
								<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
								<TD class="TDBGColorValue" style="HEIGHT: 2px"><asp:textbox onkeypress="return kutip_satu()" id="TXT_PRMLINK" runat="server" MaxLength="100"
										Width="336px" TextMode="MultiLine" Height="100px"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" colSpan="2"><asp:button id="BTN_SAVE" CssClass="button1" Width="70px" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button>&nbsp;
						<asp:button id="BTN_CANCEL" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Existing Data</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="DIMID" HeaderText="Dimention ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DIMDESC" HeaderText="Dimention Desc">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="REFTABLE" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="REFFIELDID" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="REFFIELDDESC" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="CBSTABLE" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="CBSFIELD" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="CBSLINK" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRMTABLE" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRMFIELD" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRMLINK" Visible="False"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit3" runat="server" CommandName="Edit" Visible="False">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete3" runat="server" CommandName="Delete">Delete</asp:LinkButton>
										<asp:Label id="lbl_Status" runat="server"></asp:Label>
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
					<TD vAlign="top" align="center" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
							PageSize="5">
							<AlternatingItemStyle CssClass="tblalternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="DIMID" HeaderText="Dimention ID">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DIMDESC" HeaderText="Dimention Desc">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="REFTABLE" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="REFFIELDID" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="REFFIELDDESC" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="CBSTABLE" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="CBSFIELD" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="CBSLINK" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRMTABLE" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRMFIELD" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRMLINK" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA1" HeaderText="Status">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CH_STA" Visible="False"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="lnk_RfEdit4" runat="server" CommandName="Edit">Edit</asp:LinkButton>&nbsp;
										<asp:LinkButton id="lnk_RfDelete4" runat="server" CommandName="Delete">Delete</asp:LinkButton>
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
