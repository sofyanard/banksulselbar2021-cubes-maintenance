<%@ Page language="c#" Codebehind="AssessmentParamAll.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Parameter.General.JiwaService.AssessmentParamAll" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>AssessmentParamAll</TITLE>
		<META content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>SELF ASSESSMENT</B>
									</TD>
								</TR>
							</TABLE>
						</TD>
						<TD class="tdNoBorder" align="right"><A href="ListCustomer.aspx?si="></A><asp:imagebutton id="BTN_BACK" runat="server" ImageUrl="/SME/image/Back.jpg" onclick="BTN_BACK_Click"></asp:imagebutton><A href="../../../../Body.aspx"><IMG height="25" src="../../../../Image/MainMenu.jpg" width="106"></A>
							<A href="../../../../Logout.aspx" target="_top"><IMG src="../../../../Image/Logout.jpg"></A>
						</TD>
					</TR>
					<TR>
						<TD class="tdHeader1" colSpan="2">PARAMETER SETUP</TD>
					</TR>
					<TR>
						<TD class="td" style="HEIGHT: 7px" vAlign="top" width="50%">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%">
								<TBODY>
									<TR>
										<TD class="TDBGColor1" width="50%"><asp:label id="LBL_TXT_GROUPTYPE" runat="server">Group Provider :</asp:label></TD>
										<TD class='A"TDBGColorValue"'><asp:dropdownlist id="DDL_GRPTYPEID" runat="server" AutoPostBack="True" CssClass="Mandatory" Width="100%" onselectedindexchanged="DDL_GRPTYPEID_SelectedIndexChanged"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="TDBGColor1" width="50%"><asp:label id="LBL_TXT_DEPTTYPEID" runat="server">Department Provider :</asp:label></TD>
										<TD class='A"TDBGColorValue"'><asp:dropdownlist id="DDL_DEPTTYPEID" runat="server" Width="100%"></asp:dropdownlist></TD>
									</TR>
									<TR>
										<TD class="TDBGColor1" width="50%"><asp:label id="LBL_TXT_ACTION" runat="server">Action :</asp:label></TD>
										<TD class="TDBGColorValue"><asp:textbox id="TXT_ACTION" runat="server" CssClass="mandatory" BorderStyle="None" Width="100%"
												Height="70px" TextMode="MultiLine"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="TDBGColor1" width="50%"><asp:label id="LBL_TXT_UKURAN" runat="server">Ukuran :</asp:label></TD>
										<TD class="TDBGColorValue"><asp:textbox id="TXT_UKURAN" runat="server" CssClass="mandatory" BorderStyle="None" Width="100%"
												Height="70px" TextMode="MultiLine"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="TDBGColor1" width="50%"><asp:label id="LBL_TXT_PIC_UNIT" runat="server">PIC :</asp:label></TD>
										<TD class="TDBGColorValue"><asp:textbox id="TXT_PIC_UNIT" runat="server" BorderStyle="None" Width="100%"></asp:textbox>
										</TD>
									</TR>
									<TR>
										<TD class="TDBGColor1" width="50%"><asp:label id="LBL_TXT_SERVICE" runat="server">Jiwa Service :</asp:label></TD>
										<!--<TD class="TDBGColorValue"><asp:radiobuttonlist id="RDO_SERVICE" runat="server" Width="100%" RepeatDirection="Vertical">
											<asp:ListItem Value="P">Proactive</asp:ListItem>
											<asp:ListItem Value="R">Reliable</asp:ListItem>
											<asp:ListItem Value="T">Timely Solution</asp:ListItem>
											<asp:ListItem Value="C">Convenient</asp:ListItem>
											<asp:ListItem Value="F">Friendly</asp:ListItem>
										</asp:radiobuttonlist>-->
										<TD class="TDBGColorValue">
											<asp:checkbox id="CHK_PROACTIVE" runat="server" Text="Proactive" Width="100%"></asp:checkbox>
											<asp:checkbox id="CHK_TIMELY_SOLUTION" runat="server" Text="Timely Solution" Width="100%"></asp:checkbox>
											<asp:checkbox id="CHK_RELIABLE" runat="server" Text="Reliable" Width="100%"></asp:checkbox>
											<asp:checkbox id="CHK_FRIENDLY" runat="server" Text="Friendly" Width="100%"></asp:checkbox>
											<asp:checkbox id="CHK_CONVENIENT" runat="server" Text="Convenient" Width="100%"></asp:checkbox>
										</TD>
						</TD>
					</TR>
					<TR>
						<TD><asp:label id="LBL_ID" runat="server" Visible="False"></asp:label></TD>
						<TD><asp:label id="TXT_ID" runat="server" Visible="False"></asp:label></TD>
					</TR>
				</TABLE>
				</TD>
				<TD class="td" vAlign="top" width="50%">
					<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%">
						<TR>
							<TD class="td" vAlign="top" align="center" width="50%" colSpan="3">
								<asp:datagrid id="DGR_LANGKAH" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
									<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
									<Columns>
										<asp:BoundColumn DataField="SEQ_ID" Visible="False">
											<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="SEQ" HeaderText="No">
											<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="G_CODE" Visible="False">
											<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="ACTION" Visible="False">
											<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="UKURAN" Visible="False">
											<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="LANGKAH" HeaderText="Langkah-langkah">
											<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Function">
											<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<asp:LinkButton id="Linkbutton1" runat="server" CommandName="edit_langkah">Edit</asp:LinkButton>
												<asp:LinkButton id="Linkbutton2" runat="server" CommandName="delete_langkah">Delete</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateColumn>
									</Columns>
									<PagerStyle Mode="NumericPages"></PagerStyle>
								</asp:datagrid>
							</TD>
						</TR>
						<TR>
							<TD class="TDBGColor1" width="50%">Langkah-langkah :</TD>
							<TD class="TDBGColorValue">
								<asp:textbox onkeypress="return signeddigitsonly()" id="TEXT_LANGKAH" runat="server" BorderStyle="None"
									Width="350px" Height="70px" TextMode="MultiLine"></asp:textbox></TD>
							<TD>&nbsp; &nbsp; &nbsp;
								<asp:button id="BTN_INSERT" runat="server" Width="70px" Text="INSERT" onclick="BTN_INSERT_Click"></asp:button>
							</TD>
						</TR>
						<TR>
							<TD><asp:label id="LBL_NO" runat="server" Visible="False"></asp:label></TD>
							<TD><asp:label id="TXT_NO" runat="server" Visible="False"></asp:label></TD>
						</TR>
					</TABLE>
				</TD>
				</TR>
				<TR>
					<TD class="TDBGColor2" vAlign="top" align="center" width="100%" colSpan="2">
						<asp:button id="BTN_SAVE" runat="server" CssClass="button1" Text="SAVE" Width="76px" onclick="BTN_SAVE_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="BTN_CLEAR" runat="server" CssClass="button1" Text="CLEAR" Width="76px" onclick="BTN_CLEAR_Click"></asp:button>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">EXISTING PARAMETER</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
						<asp:datagrid id="DGR_SELF" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
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
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="LNK_EDIT" runat="server" CommandName="edit">Edit</asp:LinkButton>
										<asp:LinkButton id="LNK_DELETE" runat="server" CommandName="delete">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
				<TR>
					<TD class="tdHeader1" colSpan="2">REQUESTED PARAMETER</TD>
				</TR>
				<TR>
					<TD class="td" vAlign="top" align="center" width="50%" colSpan="2">
						<asp:datagrid id="DGR_REQUESTSELF" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
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
								<asp:TemplateColumn HeaderText="Function">
									<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="LNK_EDIT_REQ" runat="server" CommandName="edit_req">Edit</asp:LinkButton>
										<asp:LinkButton id="LNK_DELETE_REQ" runat="server" CommandName="delete_req">Delete</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
					</TD>
				</TR>
				</TBODY></TABLE>
			</CENTER>
		</FORM>
	</BODY>
</HTML>
