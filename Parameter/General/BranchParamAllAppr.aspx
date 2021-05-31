<%@ Page language="c#" Codebehind="BranchParamAllAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.BranchParamAllAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BranchParamAllAppr</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../include/cek_entries.html" -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="tdNoBorder" style="HEIGHT: 49px"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
							<TABLE id="Table2">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
											Host</B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" style="HEIGHT: 49px" align="right"><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="../../Image/back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton>
						<A href="../../Body.aspx"><IMG src="../../Image/MainMenu.jpg"></A>
							<A href="../../Logout.aspx" target="_top"><IMG src="../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Parameter Branch Approval</TD>
					</TR>
					<TR>
						<TD class="TD" colSpan="2">
							<TABLE id="Table3" cellSpacing="1" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="TDBGColor1">Module
									</TD>
									<TD style="WIDTH: 8px">:</TD>
									<TD class="TDBGColorValue" vAlign="middle" align="left"><asp:radiobuttonlist id="RBL_MODULE" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="RBL_MODULE_SelectedIndexChanged">
											<asp:ListItem Value="0" Selected="True">SME</asp:ListItem>
											<asp:ListItem Value="1">Consumer/Credit Card</asp:ListItem>
										</asp:radiobuttonlist></TD>
								</TR>
							</TABLE>
							<asp:datagrid id="DGR_REQUEST_SME" runat="server" ShowFooter="True" AutoGenerateColumns="False"
								AllowPaging="True" Width="100%">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="BRANCH_CODE" HeaderText="Branch Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_NAME" HeaderText="Branch Name">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CITYID"></asp:BoundColumn>
									<asp:BoundColumn DataField="CITY_NAME" HeaderText="City">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AREAID" HeaderText="AREAID"></asp:BoundColumn>
									<asp:BoundColumn DataField="AREA_NAME" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ADDR" HeaderText="Address">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_BRANCHAREA" HeaderText="Branch Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ZIPCODE" HeaderText="Zipcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_PHNFAX" HeaderText="Phone / Fax">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" HeaderText="BR_ISBRANCH"></asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ISBRANCH_DESC" HeaderText="Category">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BRANCH_TYPE"></asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_TYPE_DESC" HeaderText="Branch Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CBC_CODE" HeaderText="CBC Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_CCOBRANCH" HeaderText="CCO Branch">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BR_ISBOOKINGBRANCH"></asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ISBOOKINGBRANCH" HeaderText="Booking Branch">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDING_STATUS" HeaderText="PENDING_STATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Approve2" runat="server" GroupName="approval_status2"></asp:RadioButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton1" runat="server" CommandName="allAppr2">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Reject2" runat="server" GroupName="approval_status2"></asp:RadioButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton2" runat="server" CommandName="allReject2">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Pending2" runat="server" GroupName="approval_status2" Checked="True"></asp:RadioButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton3" runat="server" CommandName="allPend2">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid><asp:datagrid id="DGR_REQUEST_CONNCC" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								ShowFooter="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="BRANCH_CODE" HeaderText="Branch Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CD_SIBS" HeaderText="SIBS Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_NAME" HeaderText="Branch Name">
										<HeaderStyle Width="15%" CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CITYID" HeaderText="CITYID"></asp:BoundColumn>
									<asp:BoundColumn DataField="CITY_NAME" HeaderText="City">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="AREA_NAME" HeaderText="AREA_NAME"></asp:BoundColumn>
									<asp:BoundColumn DataField="AREA_NAME" HeaderText="Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="REGIONAL_ID" HeaderText="REGIONAL_ID"></asp:BoundColumn>
									<asp:BoundColumn DataField="REGIONAL_NAME" HeaderText="Region">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ADDR" HeaderText="Address">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ZIPCODE" HeaderText="Zipcode">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PHONEAREA" HeaderText="Phone Area">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PHONE" HeaderText="Phone Numb">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PHONEEXT" HeaderText="Phone Ext.">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BR_ISBRANCH" HeaderText="BR_ISBRANCH"></asp:BoundColumn>
									<asp:BoundColumn DataField="BR_ISBRANCH_DESC" HeaderText="Category">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="BRANCH_TYPE" HeaderText="BRANCH_TYPE"></asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH_TYPE_DESC" HeaderText="Branch Type">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="CBC_CODE" HeaderText="CBC Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BR_CCOBRANCH" HeaderText="Comm. Branch">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="PENDING_STATUS" HeaderText="PENDING_STATUS"></asp:BoundColumn>
									<asp:BoundColumn DataField="PENDING_STATUS" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Approve1" runat="server" GroupName="approval_status1"></asp:RadioButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton4" runat="server" CommandName="allAppr1">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Reject1" runat="server" GroupName="approval_status1"></asp:RadioButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton5" runat="server" CommandName="allReject1">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="rdo_Pending1" runat="server" GroupName="approval_status1" Checked="True"></asp:RadioButton>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton id="Linkbutton6" runat="server" CommandName="allPend1">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2">
							<asp:button id="BTN_SUBMIT" CssClass="button1" Text="Submit" Runat="server" onclick="BTN_SUBMIT_Click"></asp:button>&nbsp;
							<asp:label id="LBL_STA" runat="server" Visible="False"></asp:label></TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
