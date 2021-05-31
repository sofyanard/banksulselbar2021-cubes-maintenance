<%@ Page language="c#" Codebehind="RFRacScoredCardLink.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFRacScoredCardLink" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RAC - Score Card Model Link (Maker)</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../../include/cek_mandatoryOnly.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
		<script language="javascript">
		  function fillText(sTXT)
		  {
		    objTXT = eval('document.Form1.TXT_' + sTXT)
		    objDDL = eval('document.Form1.DDL_' + sTXT)
		    objTXT.value = objDDL.options[objDDL.selectedIndex].text;
		  }
		</script>
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
						<TD class="tdHeader1" colSpan="2">RAC - Score Card Link</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%" colSpan="2">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 17px">Loan Purpose</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 17px"><asp:dropdownlist id="DDL_PROGRAMID" runat="server" Width="300px" CssClass="mandatory2"></asp:dropdownlist>&nbsp;</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 16px">Product</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 18px"><asp:dropdownlist id="DDL_CA_ASPEK" runat="server" Width="300px" CssClass="mandatory2"></asp:dropdownlist><asp:button id="BTN_VIEW" runat="server" Width="80px" Text="View" onclick="BTN_VIEW_Click"></asp:button>(pilih 
										Loan Purpose &amp; Program dulu)</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="HEIGHT: 15px">RAC</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue">
										<asp:dropdownlist id="DDL_RCA" runat="server" Width="80%" CssClass="mandatory" onchange="fillText('RCA');"></asp:dropdownlist>
										<asp:textBox id="TXT_RCA" Runat="server" Width="100%" TextMode="MultiLine" ReadOnly="True" Rows="5"></asp:textBox>
									</TD>
								</TR>
                                <TR>
									<TD class="TDBGColor1" style="HEIGHT: 16px">Deviasi</TD>
									<TD style="WIDTH: 15px"></TD>
									<TD class="TDBGColorValue" style="HEIGHT: 18px">
                                        <asp:checkbox id="CHK_DEVIASI_FLAG" runat="server" AutoPostBack="True" oncheckedchanged="CHK_DEVIASI_FLAG_CheckedChanged"></asp:checkbox>&nbsp;&nbsp;
                                        <asp:dropdownlist id="DDL_DEVIASI_ACTION" runat="server" AutoPostBack="True" onselectedindexchanged="DDL_DEVIASI_ACTION_SelectedIndexChanged"></asp:dropdownlist>&nbsp;&nbsp;
                                        <asp:dropdownlist id="DDL_DEVIASI_ROUTE" runat="server"></asp:dropdownlist>
                                    </TD>
								</TR>
							</TABLE>
							<asp:Label id="LBL_SEQ_ID" runat="server" Visible="False">Label</asp:Label></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" Width="100px" CssClass="button1" Text="Save" Runat="server" onclick="BTN_SAVE_Click"></asp:button><asp:button id="BTN_CANCEL" Width="100px" CssClass="button1" Text="Cancel" Runat="server" onclick="BTN_CANCEL_Click"></asp:button><asp:label id="LBL_SAVEMODE" runat="server" Visible="False">1</asp:label></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							<P>Current&nbsp;&nbsp;RAC - Score Card Link</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_CURRENT" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								PageSize="18">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="loanpurpid" HeaderText="loanpurpid">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="loanpurpdesc" HeaderText="Loan Purpose">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="productid" HeaderText="productid">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="productdesc" HeaderText="Product">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="rcaid" HeaderText="rcaid"></asp:BoundColumn>
									<asp:BoundColumn DataField="rcadesc" HeaderText="RAC">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="deviasi_flag" HeaderText="deviasi_flag"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="deviasi_flagdesc" HeaderText="Deviasi">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="deviasi_action" HeaderText="deviasi_action"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="deviasi_actiondesc" HeaderText="Deviasi Action">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="deviasi_route" HeaderText="deviasi_route"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="deviasi_routedesc" HeaderText="Deviasi Route">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit1" runat="server" Visible="False" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete1" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Pending RAC - Score Card Link</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
							<asp:datagrid id="DGR_PENDING" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								PageSize="18">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="loanpurpid" HeaderText="loanpurpid">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="loanpurpdesc" HeaderText="Loan Purpose">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="productid" HeaderText="productid">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="productdesc" HeaderText="Product">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="rcaid" HeaderText="rcaid">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="rcadesc" HeaderText="RAC">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="deviasi_flag" HeaderText="deviasi_flag"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="deviasi_flagdesc" HeaderText="Deviasi">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="deviasi_action" HeaderText="deviasi_action"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="deviasi_actiondesc" HeaderText="Deviasi Action">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="deviasi_route" HeaderText="deviasi_route"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="deviasi_routedesc" HeaderText="Deviasi Route">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDINGSTATUS" HeaderText="PENDINGSTATUS">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PENDINGSTATUSDESC" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SEQ" HeaderText="SEQ"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit2" runat="server" Visible="False" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
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
			</center>
		</form>
	</body>
</HTML>
