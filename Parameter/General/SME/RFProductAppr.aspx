<%@ Page language="c#" Codebehind="RFProductAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.RFProductAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RFProductAppr</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
							<TABLE id="Table2">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center">
										<B>Parameter Maintenance : General</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right">
							<asp:ImageButton id="BTN_BACK" runat="server" ImageUrl="/SME/Image/back.jpg" onclick="BTN_BACK_Click"></asp:ImageButton>
							<A href="../../../Body.aspx"><IMG src="/SME/Image/MainMenu.jpg"></A> <A href="../../../Logout.aspx" target="_top">
								<IMG src="/SME/Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter Product Approval</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DTG_APPR" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="15"
								AllowPaging="True" ShowFooter="True">
								<Columns>
									<asp:BoundColumn DataField="STATUSDESC" HeaderText="Request Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTID" HeaderText="Product Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTDESC" HeaderText="Product Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SIBS_PRODCODE" HeaderText="SIBS Product Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SIBS_PRODID" HeaderText="SIBS Facility Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CURRENCY"></asp:BoundColumn>
									<asp:BoundColumn DataField="CURRENCYDESC" HeaderText="Currency">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="INTERESTREST" HeaderText="Interest Rest">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CALCMETHOD"></asp:BoundColumn>
									<asp:BoundColumn DataField="CALCMETHODDESC" HeaderText="Calculation Method">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISCASHLOAN"></asp:BoundColumn>
									<asp:BoundColumn DataField="ISCASHLOANDESC" HeaderText="Cash Loan">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="REVOLVING"></asp:BoundColumn>
									<asp:BoundColumn DataField="REVOLVINGDESC" HeaderText="Revolving">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="INTERESTTYPE"></asp:BoundColumn>
									<asp:BoundColumn DataField="INTERESTTYPEDESC" HeaderText="Interest Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="INTERESTTYPERATE" HeaderText="Interest Type Rate">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RATENO" HeaderText="Rate Ref Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RATE" HeaderText="Rate">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VARCODE" HeaderText="Varcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VARIANCE" HeaderText="Variance">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SPK"></asp:BoundColumn>
									<asp:BoundColumn DataField="SPKDESC" HeaderText="SPK">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISINSTALLMENT"></asp:BoundColumn>
									<asp:BoundColumn DataField="ISINSTALLMENTDESC" HeaderText="Payment type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="INSTALMENTTYPE"></asp:BoundColumn>
									<asp:BoundColumn DataField="INSTALMENTTYPEDESC" HeaderText="Installment Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CONFIRMKORAN"></asp:BoundColumn>
									<asp:BoundColumn DataField="CONFIRMKORANDESC" HeaderText="Rekening Koran">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISUPLOADEMAS"></asp:BoundColumn>
									<asp:BoundColumn DataField="ISUPLOADEMASDESC" HeaderText="Upload to eMAS">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="IDCFLAG"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FIRSTINSTALLDATE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="JNSPRODUCT"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SUPPORTSUBAPP"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SUPPORTEBIZ"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISSUBAPPPROD"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="INTERESTMODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISCHANGEINTERESTALLOW"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ISNEGORATE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ALTRATECALC"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="NCL_CODE"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="ACTIVE"></asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="IS_AGUNAN"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IS_AGUNANDESC" HeaderText="Agunan">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn DataField="LIMIT_ATAS" HeaderText="Limit Atas">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn DataField="LIMIT_BAWAH" HeaderText="Limit Bawah">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn Visible="False" DataField="IS_PRK"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IS_PRKDESC" HeaderText="PRK">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Approve" runat="server" GroupName="rdg_Decision"></asp:RadioButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="BTN_All_Approve" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Reject" runat="server" GroupName="rdg_Decision"></asp:RadioButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="BTN_All_Reject" runat="server" CommandName="allRejc">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Pending" runat="server" GroupName="rdg_Decision" Checked="True"></asp:RadioButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="BTN_All_Pending" runat="server" CommandName="allPend">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SUBMIT" Runat="server" Text="Submit" CssClass="button1" onclick="BTN_SUBMIT_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
