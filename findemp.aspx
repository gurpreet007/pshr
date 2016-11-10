<%@ Page Language="vb" AutoEventWireup="false" Inherits="pshr.findemp" CodeFile="findemp.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PSPCL HR Search Engine </title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="css/common.css" type="text/css" rel="stylesheet">
		<script src="JScriptOne.js" type="text/javascript"></script>
	</HEAD>
	<body class="CssBody" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="CssBody" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tbody>
					<tr height="100%">
						<td align="center" height="100%">
							<table class="CssBody" height="100%" cellspacing="0" cellpadding="5" width="100%" border="0">
								<tbody>
									<tr height="57">
										<td style="BACKGROUND-IMAGE: url(img/header100.jpg); BACKGROUND-REPEAT: no-repeat; HEIGHT: 57px"
											valign="bottom" align="right" colspan="2">
											<table cellspacing="0" cellpadding="0" border="0">
												<tr class="CssLink">
													<td>&nbsp;<a class="CssLink" href="empdetail.aspx">Home</a>&nbsp;</td>
													<td>&nbsp;|&nbsp;</td>
													<td>&nbsp;<a class="CssLink" href="changepwd.aspx">Change password</a>&nbsp;</td>
													<td>&nbsp;|&nbsp;</td>
													<td>&nbsp;<asp:button id="bLogout" onmouseover="this.style.color='blue';" onmouseout="this.style.color='#2f6a94';"
															runat="server" borderwidth="0px" borderstyle="None" font-bold="True" text="Logout" width="48px"
															cssclass="CssLink"></asp:button>&nbsp;</td>
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
										<td style="BACKGROUND-IMAGE: url(img/lowerbar.jpg)" valign="bottom" align="right" colspan="2"></td>
									</tr>
									<tr>
										<td style="HEIGHT: 3px" align="center" colspan="2">&nbsp;</td>
									</tr>
									<tr height="100%">
										<td valign="top" align="center">
											<!--style="BACKGROUND-POSITION: right top; BACKGROUND-ATTACHMENT: fixed; BACKGROUND-IMAGE: url(img/body100.jpg); BACKGROUND-REPEAT: no-repeat"-->
											<table class="CssBody" cellspacing="0" cellpadding="0" width="600" border="0">
												<tbody>
													<tr>
														<td>
															<table class="CssBody" cellspacing="0" cellpadding="0" width="100%">
																<tr>
																	<td class="Header1st" align="left"><strong>Find&nbsp;Employee</strong></td>
																	<td class="Header2nd" width="100%">&nbsp;</td>
																</tr>
																<tr>
																	<td colspan="2">&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="ContentBorder" align="center" colspan="4">
															<table class="CssBody" cellspacing="10" cellpadding="0" border="0">
																<tr>
																	<td>First&nbsp;Name</td>
																	<td><asp:textbox id="tfName" runat="server" cssclass="txtFont"></asp:textbox></td>
																	<td>PAN&nbsp;No</td>
																	<td><asp:textbox id="tPan" runat="server" cssclass="txtFont"></asp:textbox></td>
																</tr>
																<tr>
																	<td>Middle&nbsp;Name</td>
																	<td><asp:textbox id="tmName" runat="server" cssclass="txtFont"></asp:textbox></td>
																	<td>Blood Group</td>
																	<td><asp:dropdownlist id="drpBloodGrp" runat="server" cssclass="DropFont">
                                                                        <asp:ListItem>All</asp:ListItem>
                                                                        <asp:ListItem>A+</asp:ListItem>
                                                                        <asp:ListItem>A-</asp:ListItem>
                                                                        <asp:ListItem>AB+</asp:ListItem>
                                                                        <asp:ListItem>AB-</asp:ListItem>
                                                                        <asp:ListItem>B+</asp:ListItem>
                                                                        <asp:ListItem>B-</asp:ListItem>
                                                                        <asp:ListItem>O+</asp:ListItem>
                                                                        <asp:ListItem>O-</asp:ListItem>
                                                                        </asp:dropdownlist></td>
																</tr>
																<tr>
																	<td>Last&nbsp;Name</td>
																	<td><asp:textbox id="tlName" runat="server" cssclass="txtFont"></asp:textbox></td>
																	<td>&nbsp;</td>
																	<td>&nbsp;</td>
																</tr>
																<tr>
																	<td style="HEIGHT: 18px">Designation</td>
																	<td colspan="3" style="HEIGHT: 18px"><asp:dropdownlist id="ddDesignation" runat="server" width="355px" cssclass="DropFont"></asp:dropdownlist></td>
																</tr>
																<tr>
																	<td>Location</td>
																	<td colspan="3"><asp:dropdownlist id="ddLocation" runat="server" width="355px" 
                                                                            cssclass="DropFont"></asp:dropdownlist></td>
																</tr>
																<tr>
																	<td>&nbsp;</td>
																	<td colspan="3"><asp:button id="btnFind" runat="server" text="Find" cssclass="ButSmall"></asp:button><asp:button id="btnClear" runat="server" text="Clear" cssclass="ButSmall"></asp:button><!--<input type="reset" class="ButSmall" value="Clear">--></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td>&nbsp;</td>
													</tr>
													<tr>
														<td><br>
															<asp:label id="lMsg" runat="server" font-bold="True"></asp:label><br>
															<br>
															&nbsp;<br>
															<asp:datagrid id="dgEmpDet" runat="server" borderwidth="1px" width="100%" cssclass="CssBody" cellpadding="5"
																bordercolor="#cdc5b1" alternatingitemstyle-backcolor="#f8f2ef" headerstyle-font-bold="True">
																<alternatingitemstyle cssclass="Report_Text" backcolor="#F8F2EF"></alternatingitemstyle>
																<itemstyle cssclass="Report_Text"></itemstyle>
																<headerstyle font-bold="True" cssclass="Report_Head"></headerstyle>
															</asp:datagrid></td>
													</tr>
												</tbody>
											</table>
										</td>
									</tr>
									<tr>
										<td>&nbsp;</td>
									</tr>
								</tbody>
							</table>
						</td>
					</tr>
					<!-- #include file=footer.inc --></tbody></table>
		</form>
	</body>
</HTML>
