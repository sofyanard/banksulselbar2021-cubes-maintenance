<%@ Page language="c#" Codebehind="ProductListParamAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.CC.ProductListParamAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>General Credit Card Parameter</title>
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
							</TABLE>
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD width="400" style="HEIGHT: 41px">
										<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
											border="0">
											<TR>
												<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B> Parameter Maintenance : 
														Credit Card General</B></TD>
											</TR>
										</TABLE>
									</TD>
									<TD style="HEIGHT: 41px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx">
											<IMG src="../../../Image/MainMenu.jpg"></A>&nbsp;<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A><A href="../../../Logout.aspx" target="_top"></A>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							Parameter&nbsp; Product List Approval</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="left" width="50%" colSpan="2">
							<asp:datagrid id="DGR_APPR" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="100%"
								ShowFooter="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="PRODUCTID" HeaderText="Product Type ID">
										<HeaderStyle HorizontalAlign="Center" Width="125px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NETWORKDESC" HeaderText="Network">
										<HeaderStyle HorizontalAlign="Center" Width="125px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CLASSICDESC" HeaderText="Classic Type">
										<HeaderStyle HorizontalAlign="Center" Width="125px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PRODUCTNAME" HeaderText="Name">
										<HeaderStyle HorizontalAlign="Center" Width="125px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CARD_TYPE" HeaderText="Type">
										<HeaderStyle HorizontalAlign="Center" Width="120px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CD_SIBS" HeaderText="Service Program ID">
										<HeaderStyle HorizontalAlign="Center" Width="120px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PR_FLAG_NM" HeaderText="Category">
										<HeaderStyle HorizontalAlign="Center" Width="120px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS1" HeaderText="Status">
										<HeaderStyle HorizontalAlign="Center" Width="100px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="DUMMIES" HeaderText="Dummies">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_TYPE" HeaderText="PR_TYPE">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CCY" HeaderText="PR_CCY">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_TENOR" HeaderText="Tenor">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_AMOUNT" HeaderText="Amount">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="NA_CODE" HeaderText="Nationality">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_PROVISI" HeaderText="Provisi">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_RATEYY" HeaderText="Rate(YY)">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_RATEMM" HeaderText="Rate(MM)">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWTAX1" HeaderText="Tax 1">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWTAX2" HeaderText="tax 2">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWTAX3" HeaderText="Tax 3">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWTAX4" HeaderText="Tax 4">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWTAXIDR1" HeaderText="Tax IDR 1">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWTAXIDR2" HeaderText="Tax IDR 2">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PMIN" HeaderText="PMIN">
										<HeaderStyle HorizontalAlign="Center" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PMAX" HeaderText="PMAX">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_CWCALLIMIT" HeaderText="PR_CWCALLIMIT">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_IURANCC" HeaderText="PR_IURANCC">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_IURANCCSP" HeaderText="PR_IURANCCSP">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="FLOOR_LIMIT" HeaderText="FLOOR_LIMIT">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CEILING_LIMIT" HeaderText="CEILING_LIMIT">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PR_FLAG" HeaderText="PR_FLAG"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SEQ_ID" HeaderText="SEQ_ID">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle HorizontalAlign="Center" Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rd_approve" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="lnkAllAppr" runat="server" CommandName="allAppr">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle HorizontalAlign="Center" Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rd_Reject" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="lnkAllReject" runat="server" CommandName="allReject">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle HorizontalAlign="Center" Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rd_Pending" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="lnkAllPend" runat="server" CommandName="allPend">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2">
							<asp:button id="BTN_SUBMIT" CssClass="button1" Text="Submit" Runat="server" onclick="BTN_SUBMIT_Click"></asp:button>&nbsp;&nbsp;</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
