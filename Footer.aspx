<%@ Page language="c#" Codebehind="Footer.aspx.cs" AutoEventWireup="True" Inherits="CuBES_Maintenance.Footer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Footer</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
		//var timer;
		function postback()
		{
			Form1.post_cnt.value = eval(Form1.post_cnt.value + '+1');
			Form1.submit();
		}
		function set_post()
		{
			try {
				if (Form1.post_cnt.value == '12')	window.parent.warn_timeout();
				if (Form1.post_cnt.value == '18')	window.parent.logout_now();
				else	setTimeout('postback()',100000);
			}	catch (e) { }
		}
		function reset_post()
		{
			Form1.post_cnt.value = 0;
			//clearTimeout(timer);
			//set_post();
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="0" leftmargin="0" onload="set_post()">
		<form id="Form1" method="post" runat="server">
			<table border="0" width="100%" bgcolor="#808080" height="15">
				<tr valign="top">
					<td width="35%" valign="top"><font face="tahoma" size="1" style="FONT-SIZE:9px" color="#ffffff">&nbsp;<asp:Label id="Label3" runat="server" Height="15">UserName : </asp:Label>&nbsp;
							<asp:Label id="Label2" runat="server" Height="15"></asp:Label>&nbsp;
							<asp:Label id="Label5" runat="server" Height="15"></asp:Label></font>
					</td>
					<td width="30%" align="center"><font color="#ffffff" face="tahoma" size="1" style="FONT-SIZE:9px">Copyright 
							©2013 A-Solution </font>
					</td>
					<td width="35%" align="right" valign="top"><font color="#ffffff" face="tahoma" size="1" style="FONT-SIZE:9px"><INPUT type="hidden" id="post_cnt" runat="server" NAME="post_cnt">
							<asp:Label id="Label4" runat="server" Height="15">Login Since : </asp:Label>&nbsp;
							<asp:Label id="Label1" runat="server" Height="15"></asp:Label></font>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
