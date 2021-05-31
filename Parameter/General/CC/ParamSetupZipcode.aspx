<%@ Page language="c#" Codebehind="ParamSetupZipcode.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.ParamSetupZipcode" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ParamSetupZipcode</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<!-- #include  file="../../../include/cek_entries.html" -->
		<script language="javascript">
		function cek_mandatory(frm, alamat)
		{
			max_elm = (frm.elements.length) - 2;
			lanjut = true;
			for (var i=1; i<=max_elm; i++)
			{
				elm = frm.elements[i];
				nm_kolom = "kotak";
				if (elm.className == "mandatory" && (elm.value == "" || elm.value == "0" || elm.value == "0,00") && (elm.type == "text" || elm.type == "select-one"))
				{
					if (i==2)
						alert("Agency Name tidak boleh kosong...");
					if (i==3)
						alert("Start Zipcode tidak boleh kosong...");
					if (i==4)
						alert("End Zipcode tidak boleh kosong...");
					lanjut = false;
					elm.focus();
					return false;
				}
			}
			if (lanjut)
			{

				if (alamat != undefined && alamat != "" )
					frm.action = alamat;

				return true;
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table4" width="100%" border="0">
					<TR>
						<TD class="tdNoBorder" colSpan="2">
							<TABLE id="Table10" style="WIDTH: 440px; HEIGHT: 25px">
								<TR>
									<TD class="TDBGColor2" align="center"><B>General Information</B></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<tr>
						<td vAlign="top" colSpan="2">
							<fieldset class="TDBGFieldset"><legend class="TDBGLegend">&nbsp;&nbsp;Agency Setup 
									Table&nbsp;&nbsp;</legend>
								<TABLE cellSpacing="0" cellPadding="0" width="95%">
									<TR>
										<TD class="TDBGHeaderGrid" align="center">Agency Name
											<asp:Label id="LBL_SEQ" runat="server" Visible="False"></asp:Label></TD>
										<TD class="TDBGHeaderGrid" width="120">Start Zipcode</TD>
										<TD class="TDBGHeaderGrid" width="120">End Zipcode</TD>
										<TD class="TDBGHeaderGrid" width="100" align="center">Function</TD>
									</TR>
									<TR>
										<TD class="TDBGColorValue">
											<asp:DropDownList id="DDL_AGENCY" runat="server" CssClass="mandatory"></asp:DropDownList></TD>
										<TD class="TDBGColorValue">
											<asp:textbox onkeypress="return numbersonly()" id="TXT_STARTZIP" style="TEXT-ALIGN: right" runat="server"
												Width="120px" CssClass="mandatory" MaxLength="7"></asp:textbox></TD>
										<TD class="TDBGColorValue"><asp:textbox onkeypress="return numbersonly()" id="TXT_ENDZIP" style="TEXT-ALIGN: right" runat="server"
												Width="120px" CssClass="mandatory" MaxLength="7"></asp:textbox></TD>
										<TD class="TDBGColorValue" align="center"><asp:button id="BTN_AGENT" runat="server" Width="55px" Text="Save" CssClass="Button1" onclick="BTN_AGENT_Click"></asp:button></TD>
									</TR>
								</TABLE>
								<BR>
								<asp:datagrid id="GRD_ZIPCODE" runat="server" Width="95%" CssClass="TDBGGrid" AllowPaging="True"
									AutoGenerateColumns="False">
									<SelectedItemStyle CssClass="mandatory"></SelectedItemStyle>
									<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="AGENCYID" HeaderText="Agent ID"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="AG_SEQ" HeaderText="Agent SEQ"></asp:BoundColumn>
										<asp:BoundColumn DataField="agencyname" HeaderText="Agency Name">
											<HeaderStyle Font-Bold="True" CssClass="&quot;&quot;tdSmallHeader&quot;&quot;"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="STARTZIP" HeaderText="Start Zipcode">
											<HeaderStyle Font-Bold="True" Width="120px" CssClass="&quot;&quot;tdSmallHeader&quot;&quot;"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="ENDZIP" HeaderText="End Zipcode">
											<HeaderStyle Font-Bold="True" CssClass="&quot;&quot;tdSmallHeader&quot;&quot;"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right" Width="120px"></ItemStyle>
										</asp:BoundColumn>
										<asp:ButtonColumn Text="Edit" CommandName="edit">
											<HeaderStyle Font-Bold="True" CssClass="TDBGHeaderGrid"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
										</asp:ButtonColumn>
										<asp:ButtonColumn Text="Delete" CommandName="delete">
											<HeaderStyle Font-Bold="True" CssClass="TDBGHeaderGrid"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
										</asp:ButtonColumn>
									</Columns>
									<PagerStyle Mode="NumericPages"></PagerStyle>
								</asp:datagrid><BR>
							</fieldset>
						</td>
					</tr>
					<TR>
						<TD vAlign="top" align="center" colSpan="2"><INPUT class="button1" type="button" value="Close" onclick="javascript:window.close();"></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
