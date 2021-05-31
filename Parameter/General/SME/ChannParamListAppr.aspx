<%@ Page language="c#" Codebehind="ChannParamListAppr.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.SME.ChannParamListAppr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Channeling Parameter List Approval</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tbl_MAIN" cellSpacing="2" cellPadding="2" width="100%">
				<TR>
					<TD class="tdNoBorder"><!--<img src="../Image/HeaderDetailDataEntry.jpg">-->
						<TABLE id="tbl_JUDUL">
							<TR>
								<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
										General</B></TD>
							</TR>
						</TABLE>
					</TD>
					<TD class="tdNoBorder" align="right"><asp:imagebutton id="btn_BACK" runat="server" ImageUrl="../../../image/Back.jpg" onclick="btn_BACK_Click"></asp:imagebutton><A href="../../../Body.aspx"><IMG height="25" src="../../../Image/MainMenu.jpg" width="106"></A>
						<A href="../../../Logout.aspx" target="_top"><IMG src="../../../Image/Logout.jpg"></A>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">Channeling Parameter List Approval</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
						<asp:datagrid id="dtg_CHANN" runat="server" Width="100%" PageSize="5" AutoGenerateColumns="False"
							AllowPaging="True" Height="176px" ShowFooter="True">
							<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="A" HeaderText="CH_PRM_CODE"></asp:BoundColumn>
								<asp:BoundColumn DataField="B" HeaderText="Deskripsi">
									<HeaderStyle Width="25%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="F" HeaderText="Tabel Referensi">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="E" HeaderText="Kolom Referensi">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="I" HeaderText="Reject Description">
									<HeaderStyle Width="25%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="C" HeaderText="Parameter">
									<HeaderStyle Width="8%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="G" HeaderText="Tabel Parameter">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="L" HeaderText="Status">
									<HeaderStyle Width="6%" CssClass="tdSmallHeader"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Approve">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rdb_Approve" runat="server" GroupName="Approve"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="lnb_Approve" runat="server" CommandName="approve">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Reject">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rdb_Reject" runat="server" GroupName="Reject"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="lnb_Reject" runat="server" CommandName="reject">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pending">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:RadioButton id="rdb_Pending" runat="server" GroupName="Pending"></asp:RadioButton>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<FooterTemplate>
										<asp:LinkButton id="lnb_Pending" runat="server" CommandName="pending">Select All</asp:LinkButton>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2">&nbsp;
						<asp:button id="btn_SUBMIT" CssClass="button1" Runat="server" Text="Submit" onclick="btn_SUBMIT_Click"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
