<%@ Page Language="vb" AutoEventWireup="false" Inherits="pshr.changepwd" CodeFile="changepwd.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PSPCL HR Search Engine </title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="css/common.css" type="text/css" rel="stylesheet">
		<script src="JScriptOne.js" type="text/javascript"></script>
	</HEAD>
	<body class="CssBody" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="CssBody" cellspacing="0" cellpadding="0" width="100%" border="0" height="100%">
				<tr height="100%">
					<td align="center" height="100%">
						<table class="CssBody" cellspacing="0" cellpadding="5" width="100%" border="0" height="100%">
							<tr height="57">
								<td style="BACKGROUND-IMAGE: url(img/header100.jpg); BACKGROUND-REPEAT: no-repeat; HEIGHT: 57px"
									valign="bottom" align="right" colspan="2">
									<table cellspacing="0" cellpadding="0" border="0">
										<tr class="CssLink">
											<td>&nbsp;<a class="CssLink" href="empdetail.aspx">Home</a>&nbsp;</td>
											<td>&nbsp;|&nbsp;</td>
											<td>&nbsp;<a class="CssLink" href="findemp.aspx">Find employee</a>&nbsp;</td>
											<td>&nbsp;|&nbsp;</td>
											<td>&nbsp;<asp:button id="bLogout" cssclass="CssLink" runat="server" width="48px" borderwidth="0px" borderstyle="None"
													text="Logout" font-bold="True" onmouseover="this.style.color='blue';" onmouseout="this.style.color='#2f6a94';"></asp:button>&nbsp;</td>
											<td width="1"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td style="BACKGROUND-IMAGE: url(img/upperbar.jpg); BACKGROUND-REPEAT: repeat; HEIGHT: 5px"
									align="center" colspan="2"></td>
							</tr>
							<tr height="20">
								<td style="BACKGROUND-IMAGE: url(img/lowerbar.jpg)" valign="bottom" align="right" colspan="2">
								</td>
							</tr>
							<tr>
								<td style="HEIGHT: 3px" align="center" colspan="2">&nbsp;</td>
							</tr>
							<tr height="100%">
								<td valign="top" align="center">
									<table class="CssBody" cellspacing="0" cellpadding="0" border="0" style=" HEIGHT: 133px">
										<tr>
											<td align="left"><asp:label id="lMsg" runat="server" font-bold="True" visible="False" forecolor="Red"></asp:label>
											</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td>
												<table class="CssBody" cellspacing="0" cellpadding="0" width="100%">
													<tr>
														<td class="Header1st" align="left"><strong>Change&nbsp;Password</strong></td>
														<td class="Header2nd" width="100%">&nbsp;</td>
													</tr>
													<tr>
														<td colspan="2">&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td align="center" class="ContentBorder">
												<table class="CssBody" cellspacing="10" cellpadding="0" border="0">
													<tr>
														<td style="WIDTH: 125px">Old Password</td>
														<td>
															<asp:textbox cssclass="txtFont" id="tPwdOld" runat="server" textmode="Password"></asp:textbox></td>
													</tr>
													<tr>
														<td style="WIDTH: 125px">New&nbsp;Password</td>
														<td>
															<asp:textbox cssclass="txtFont" id="tPwdOne" runat="server" textmode="Password"></asp:textbox></td>
													</tr>
													<tr>
														<td style="WIDTH: 125px">Confirm Password</td>
														<td>
															<asp:textbox cssclass="txtFont" id="tPwdTwo" runat="server" textmode="Password"></asp:textbox></td>
													</tr>
													<tr>
														<td style="WIDTH: 125px">&nbsp;</td>
														<td><asp:button cssclass="ButSmallest" id="bGo" runat="server" text="Go"></asp:button></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<!-- #include file=footer.inc -->
			</table>
		</form>
	</body>
</HTML>
