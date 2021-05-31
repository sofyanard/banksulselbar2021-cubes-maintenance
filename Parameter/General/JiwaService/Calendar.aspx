<%@ Page language="c#" Codebehind="Calendar.aspx.cs" AutoEventWireup="false" Inherits="CuBES_Maintenance.Parameter.General.JiwaService.Calendar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Calendar</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function pilih(ctrlID,ctrlDesc)
			{
				if (document.Form1.LST_RESULT.Text != '')
				{
					eval('opener.document.Form1.' + ctrlID + '.value = document.Form1.LST_RESULT.Text');
					eval('opener.document.Form1.' + ctrlDesc + '.value = document.Form1.LST_RESULT.options[document.Form1.LST_RESULT.Text]');
				}
				else
				{
					eval('opener.document.Form1.' + ctrlID + '.Text = ""');
					eval('opener.document.Form1.' + ctrlDesc + '.Text = ""');
				}
				window.close();
			}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="380" align="center" style="WIDTH: 361px; HEIGHT: 217px">
					<TR align="center">
						<TD class="td" vAlign="top" align="center" width="100%" colSpan="2"><asp:datagrid id="DGR_CALENDAR" runat="server" Width="100%" PageSize="6" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="SENIN">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="SENIN">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LNK_SENIN" runat="server" CommandName="senin" ForeColor="Black">1</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="SELASA">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="SELASA">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LNK_SELASA" runat="server" CommandName="selasa" ForeColor="Black">2</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="RABU">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="RABU">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LNK_RABU" runat="server" CommandName="rabu" ForeColor="Black">3</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="KAMIS">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="KAMIS">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LNK_KAMIS" runat="server" CommandName="kamis" ForeColor="Black">4</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="JUMAT">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="JUMAT">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LNK_JUMAT" runat="server" CommandName="jumat" ForeColor="Black">5</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="SABTU">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="SABTU">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LNK_SABTU" runat="server" CommandName="sabtu" ForeColor="Red">6</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="MINGGU">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="MINGGU">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="LNK_MINGGU" runat="server" CommandName="minggu" ForeColor="Red">7</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" colSpan="3"><INPUT class="Button1" id="SAVE" style="WIDTH: 60px" type="button" value="SAVE" name="SAVE"
								runat="server"> <INPUT class="Button1" id="CLEAR" style="WIDTH: 60px" type="button" value="CLEAR" name="CLEAR"
								runat="server"> <INPUT type="button" value="CLOSE" id="CLOSE" name="CLOSE" runat="server" class="Button1"
								style="WIDTH: 60px"></TD>
					</TR>
					<tr>
						<td>
							<asp:TextBox Runat="server" ID="LST_RESULT" Width="338px" Visible="False"></asp:TextBox>
						</td>
					</tr>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
