<%@ Page language="c#" Codebehind="EvaluationDetail.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.Sales.EvaluationDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EvaluationDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!-- #include file="../../../include/cek_mandatory.html" -->
		<!-- #include file="../../../include/cek_entries.html" -->
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
									<TD style="HEIGHT: 41px" width="400">
										<TABLE id="Table5" style="WIDTH: 408px; HEIGHT: 17px" cellSpacing="0" cellPadding="0" width="408"
											border="0">
											<TR>
												<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>Parameter Maintenance : 
														Sales Commission</B></TD>
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
						<TD class="tdHeader1" colSpan="2">Parameter&nbsp;Evaluation Detail Maker</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 172px; HEIGHT: 20px" width="172">Evaluation</TD>
									<TD style="WIDTH: 7px; HEIGHT: 20px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 20px"><asp:dropdownlist id="DDL_EVAL" runat="server" CssClass="mandatory"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 172px">Evaluation Description</TD>
									<TD style="WIDTH: 7px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox id="TXT_EVAL_DESC" runat="server" Width="100%" CssClass="mandatory" MaxLength="300"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" style="WIDTH: 172px; HEIGHT: 22px">Evaluation Maximum</TD>
									<TD style="WIDTH: 7px; HEIGHT: 22px">:</TD>
									<TD class="TDBGColorValue" style="HEIGHT: 22px"><asp:textbox onkeypress="return numbersonly()" id="TXT_EVAL_MAX" runat="server" Width="152px"
											CssClass="mandatory" MaxLength="12"></asp:textbox>
										<asp:Label id="LBL_JENIS" runat="server" Visible="False"></asp:Label>
										<asp:Label id="LBL_TYPEID" runat="server" Visible="False"></asp:Label>
										<asp:Label id="LBL_SAVE_MODE" runat="server" Visible="False">1</asp:Label>
										<asp:Label id="LBL_SEQ_ID" runat="server" Visible="False"></asp:Label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="left" width="50%" colSpan="2"><asp:button id="BTN_SAVE" Runat="server" Text="Save" CssClass="button1" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
							<asp:button id="BTN_CANCEL" Runat="server" Text="Cancel" CssClass="button1" onclick="BTN_CANCEL_Click"></asp:button></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">
							<P>Existing Data</P>
						</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_EXISTING" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="EVALUATION_TYPESTS" HeaderText="Evaluation_typests"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description" HeaderText="Evaluation">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="EVALUATION_DESC" HeaderText="Evaluation Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="EVALUATION_MAX" HeaderText="Evaluation Maximum">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit1" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete1" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="evaluation_typeid" HeaderText="evaluation_typeid"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="EVALUATION_ID" HeaderText="Evaluation_ID"></asp:BoundColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">Maker Request</TD>
					</TR>
					<TR>
						<TD class="td" vAlign="top" align="center" width="50%" colSpan="2"><asp:datagrid id="DGR_REQUEST" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="5"
								AllowPaging="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="EVALUATION_TYPESTS" HeaderText="EVALUATION_TYPESTS"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="No">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="description" HeaderText="Evaluation">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="evaluation_desc" HeaderText="Evaluation Description">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="evaluation_max" HeaderText="Evaluation Maximum">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS1" HeaderText="Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemTemplate>
											<asp:LinkButton id="lnk_RfEdit2" runat="server" CommandName="edit">Edit</asp:LinkButton>&nbsp;
											<asp:LinkButton id="lnk_RfDelete2" runat="server" CommandName="delete">Delete</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="EVALUATION_TYPEID" HeaderText="Evaluation_TypeID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="EVALUATION_ID" HeaderText="Evaluation_id"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="SEQ_ID" HeaderText="SEQ_ID"></asp:BoundColumn>
									<asp:BoundColumn Visible="False" DataField="STATUS" HeaderText="STATUS"></asp:BoundColumn>
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
