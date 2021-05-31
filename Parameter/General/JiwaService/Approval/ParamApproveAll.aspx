<%@ Page language="c#" Codebehind="ParamApproveAll.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.JiwaService.Approval.ParamApproveAll" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<TITLE>ParamApproveAll</TITLE>
		<META name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<META name="CODE_LANGUAGE" Content="C#">
		<META name="vs_defaultClientScript" content="JavaScript">
		<META name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../../Style.css" type="text/css" rel="stylesheet">
  </HEAD>
	<BODY MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<CENTER>
				<TABLE width="100%" border="0">
					<TR>
						<TD align="left">
							<TABLE id="Table1">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B><asp:label id="LBL_PARAM_TITLE" runat="server"></asp:label></B></TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="/SME/image/Back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../../Body.aspx"><IMG height="25" src="../../../../Image/MainMenu.jpg" width="106"></A>
							<A href="../../../../Logout.aspx" target="_top"><IMG src="../../../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">PARAMETER APPROVAL</TD>
					</TR>
					<!--
						Page Department Provider Parameter Approval
					-->
					<TR id="TR_DEPT" runat="server">
						<TD vAlign="top" align="left" width="50%" colSpan="2"><asp:datagrid id="DGR_DEPTAPPR" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								ShowFooter="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="G_CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="GROUP" HeaderText="Group Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="D_CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="D_DESCNEW" HeaderText="Department Name">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS_DESC" HeaderText="Pending Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_APPROVE1" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_APPROVE1" runat="server" CommandName="allAccept">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_PENDING1" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_PENDING1" runat="server" CommandName="allPend">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_REJECT1" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_REJECT1" runat="server" CommandName="allReject">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<!--
						Page Self Assessment Parameter Approval
					-->
					<TR id="TR_SELF" runat="server">
						<TD vAlign="top" align="left" width="50%" colSpan="2"><asp:datagrid id="DGR_SELFAPPR" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								ShowFooter="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SEQ" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="G_CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="G_DESC" HeaderText="Group Provider">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="D_CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="D_DESC" HeaderText="Dept. Provider">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ACTION" HeaderText="Action">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="UKURAN" HeaderText="Ukuran">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LANGKAH_SEQ" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LANGKAH_DESC" HeaderText="Langkah-langkah">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SERVICE_CODE" HeaderText="Jiwa Service">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PIC_UNIT" HeaderText="PIC Unit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS_DESC" HeaderText="Pending Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_APPROVE2" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_APPROVE2" runat="server" CommandName="allAccept">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_PENDING2" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_PENDING2" runat="server" CommandName="allPend">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_REJECT2" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_REJECT2" runat="server" CommandName="allReject">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<!--
						Page Customer Rule Parameter Approval
					-->
					<TR id="TR_CUST" runat="server">
						<TD vAlign="top" align="left" width="50%" colSpan="2"><asp:datagrid id="DGR_CUSTAPPR" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								ShowFooter="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="NO" HeaderText="No">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="G_CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="G_DESC" HeaderText="Group Provider">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="D_CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="D_DESC" HeaderText="Dept. Provider">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="QUESTION" HeaderText="Pertanyaan">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS_DESC" HeaderText="Pending Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_APPROVE3" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_APPROVE3" runat="server" CommandName="allAccept">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_PENDING3" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_PENDING3" runat="server" CommandName="allPend">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_REJECT3" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_REJECT3" runat="server" CommandName="allReject">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<!--
						Page Link Provider Parameter Approval
					-->
					<TR id="TR_LINK" runat="server">
						<TD vAlign="top" align="left" width="50%" colSpan="2"><asp:datagrid id="DGR_LINKAPPR" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								ShowFooter="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SEQ" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BRANCH" HeaderText="Customer Unit">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="G_CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="G_DESC" HeaderText="Group Provider">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="D_CODE" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="D_DESC" HeaderText="Department Provider">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS_DESC" HeaderText="Pending Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_APPROVE4" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_APPROVE4" runat="server" CommandName="allAccept">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_PENDING4" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_PENDING4" runat="server" CommandName="allPend">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_REJECT4" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_REJECT4" runat="server" CommandName="allReject">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<!--
						Page Score Rule Parameter Approval
					-->
					<TR id="TR_SCORE" runat="server">
						<TD vAlign="top" align="left" width="50%" colSpan="2"><asp:datagrid id="DGR_SCOREAPPR" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								ShowFooter="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="CODE" HeaderText="Code">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VARIABLE_ID" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="VARIABLE" HeaderText="Variable">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BOBOT" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BOBOT_DESC" HeaderText="Bobot">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS_DESC" HeaderText="Pending Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_APPROVE5" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_APPROVE5" runat="server" CommandName="allAccept">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_PENDING5" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_PENDING5" runat="server" CommandName="allPend">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_REJECT5" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_REJECT5" runat="server" CommandName="allReject">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<!--
						Page Cut Date Parameter Approval
					-->
					<TR id="TR_DATE" runat="server">
						<TD vAlign="top" align="left" width="50%" colSpan="2"><asp:datagrid id="DGR_DATEAPPR" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
								ShowFooter="True">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="SEQ" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FUNCTION" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="FUNCTIONDESC" HeaderText="Function">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="BEGIN_DATE" HeaderText="Tanggal Awal">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="CUT_DATE" HeaderText="Tanggal Akhir">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS" Visible="False">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="STATUS_DESC" HeaderText="Pending Status">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Approve">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_APPROVE6" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_APPROVE6" runat="server" CommandName="allAccept">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Pending">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_PENDING6" runat="server" GroupName="approval_status" Checked="True"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_PENDING6" runat="server" CommandName="allPend">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Reject">
										<HeaderStyle Width="50px" CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:RadioButton id="RDO_REJECT6" runat="server" GroupName="approval_status"></asp:RadioButton>
										</ItemTemplate>
										<FooterTemplate>
											<asp:LinkButton id="LNK_REJECT6" runat="server" CommandName="allReject">Select All</asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="100%" colSpan="2"><asp:button id="BTN_SUB" runat="server" Text="SUBMIT" CssClass="button1" onclick="BTN_SUB_Click"></asp:button></TD>
					</TR>
				</TABLE>
			</CENTER>
		</FORM>
	</BODY>
</HTML>
