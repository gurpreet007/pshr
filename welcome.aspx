<%@ Page Language="vb" AutoEventWireup="false" Inherits="pshr.welcome" EnableSessionState="True" enableViewState="True" CodeFile="welcome.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PSPCL HR Search Engine</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="css/common.css" type="text/css" rel="stylesheet">
		<script src="JScriptOne.js" type="text/javascript"></script>
	</head>
	<body class="CssBlank" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="CssBlank" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr height="100%">
					<td align="center" height="100%">
						<table class="CssBody" height="100%" cellspacing="0" cellpadding="5" width="100%" border="0">
							<tbody>
								<tr height="57">
									<td style="BACKGROUND-IMAGE: url(img/header100.jpg); BACKGROUND-REPEAT: no-repeat; HEIGHT: 57px"
										valign="bottom" align="right">
										<table cellspacing="0" cellpadding="0" border="0">
											<tr class="CssLink">
												<td style="height: 15px">&nbsp;<a class="CssLink" href="findemp.aspx">Find</a></td>
												<td style="height: 15px">&nbsp;|&nbsp;</td>
												<td style="height: 15px">&nbsp;<a class="CssLink" href="changepwd.aspx">Change password</a>&nbsp;</td>
												<td style="height: 15px">&nbsp;|&nbsp;</td>
												<td style="height: 15px">&nbsp;<a class="CssLink" href="frmGPFStmt1.aspx">GPF</a></td>
												<td style="height: 15px">&nbsp;|&nbsp;</td>
												<td style="height: 15px"><asp:LinkButton ID="LinkButton1" runat="server">Pension Slip</asp:LinkButton></td>
												<td style="height: 15px">&nbsp;|&nbsp;</td>
												<td style="height: 15px">&nbsp;<a class="CssLink" href="frmSalSlip2.aspx">Pay Slip</a></td>
												<td style="height: 15px">&nbsp;|&nbsp;</td>
												<td style="height: 15px">&nbsp;<a class=CssLink href='empdetail.aspx?empid=<%=Session("EmpId")%>'>History</a>&nbsp;</td>
												<td style="height: 15px">&nbsp;|&nbsp;</td>
												<td style="height: 15px">&nbsp;<asp:button id="bLogout" runat="server" font-bold="True" text="Logout" borderstyle="None" borderwidth="0px"
														width="48px" cssclass="CssLink" onmouseover="this.style.color='blue';" onmouseout="this.style.color='#2f6a94';"></asp:button>&nbsp;</td>
												<td width="1" style="height: 15px"></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td style="BACKGROUND-IMAGE: url(img/upperbar.jpg); BACKGROUND-REPEAT: repeat; HEIGHT: 5px"
										align="center"></td>
								</tr>
								<tr height="20">
									<td style="BACKGROUND-IMAGE: url(img/lowerbar.jpg); height: 20px;" valign="bottom" align="right"></td>
								</tr>
								<tr>
									<td style="HEIGHT: 3px; text-align: left;" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:label id="lMsg0" runat="server" font-bold="True" forecolor="Red" 
                                            Visible="False"></asp:label></td>
								</tr>
								<tr>
									<td colspan="2" valign="top" height="100%">
										<table class="CssBody" cellspacing="0" cellpadding="5" width="100%" border="0" height="100%">
											<tr>
												<td width="20">&nbsp;</td>
												<td colspan="2"><asp:label id="lMsg" runat="server" font-bold="True" forecolor="Red" visible="False"></asp:label></td>
												<td width="20" valign="top">&nbsp;</td>
											</tr>
											<tr>
												<td width="20" valign="top" style="height: 23px">&nbsp;</td>
												<td colspan="2" valign="top" style="height: 23px">Hello&nbsp;<b><%=strconv(Session("EmpNm"),3)%></b></td>
												<td width="20" valign="top" style="height: 23px">&nbsp;</td>
											</tr>
											<tr height="100%">
												<td width="20" valign="top">&nbsp;</td>
												<td valign="top">Welcome to Human Resource Information System. The Punjab State 
													Electricity Board(PSEB) is a statutory body formed on 1-2-1959 under the 
													Electricity Supply Act.194. Subsequently with the re-organization of the 
													erstwhile State of Punjab under the Punjab Re-organization Act 1966 the present 
													form came into existence w.e.f. 1st May,1967.
													<p>Starting with the modest installed capacity of 62 MW, the PSEB has grown up by 
														leaps and bounds with generating capacity 6201 MW as on 31-3-2007. The Board's 
														gross generation during the year 2006-07 was 36412.055 MUs.</p>
													<p>PSEB operates its own Generation Power Plants and also gets power as its share 
														from BBMB. It also gets power as per allocation from the Central Sector Power 
														Projects. The PSEB also constructs and maintains its Transmission and 
														Distribution system for providing efficient services to the various categories 
														of electricity consumers in the state.</p>
													<p>Though the welnit network of Transmission and Distribution System, PSEB is proud 
														of serving more than 62.31 lacs consumers comprising of approximate 51.49 lakhs 
														General, 1.09 lakhs Industrial, 9.7 lakhs Agricultural connections.</p>
												</td>
												<td style="BACKGROUND-IMAGE: url(img/body100.jpg); WIDTH: 263px; BACKGROUND-REPEAT: no-repeat"
													valign="top"></td>
												<td width="20" valign="top">&nbsp;</td>
											</tr>
										</table>
									</td>
								</tr>
							</tbody>
						</table>
					</td>
				</tr>
				<!-- #include file=footer.inc -->
			</table>
		</form>
	</body>
</html>
