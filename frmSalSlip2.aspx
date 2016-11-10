<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSalSlip2.aspx.vb" Inherits="frmSalSlip2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pay Slip</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<link href="css/common.css" type="text/css" rel="stylesheet">
</head>
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
										<td style="HEIGHT: 3px" align="center" colspan="2">
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </td>
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
																	<td class="Header1st" align="left" style="height: 17px; width: 73px;"><strong>
                                                                        <asp:Label ID="Label1" runat="server" Text="Pay Details" Width="88px"></asp:Label></strong></td>
																	<td class="Header2nd" width="100%" style="height: 17px">&nbsp;</td>
																</tr>
																<tr>
																	<td colspan="2">&nbsp;</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td class="ContentBorder" align="center" colspan="4" style="width: 600px">
															<table class="CssBody" cellspacing="10" cellpadding="0" border="0" style="height: 48px">
																<tr>
																	<td style="height: 24px; width: 162px;">
                                                                        <strong>
                                                                            <asp:Label ID="Label2" runat="server" Text="Pay Slip" Font-Size="Medium" Width="272px"></asp:Label></strong></td>
																	<td style="height: 24px">
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 162px">
                                                                        &nbsp;<asp:DropDownList ID="drpGpfYr" runat="server" Font-Bold="True" Width="136px">
                                                                        </asp:DropDownList></td>
                                                                    <td colspan="3" style="text-align: center">
                                                                        <!--<input type="reset" class="ButSmall" value="Clear">-->
                                                                        <asp:Button ID="Button1" runat="server" CssClass="ButSmall" Text="Ok" Width="72px" /></td>
                                                                </tr>
                                                            </table>
														</td>
													</tr>
                                                    <tr>
                                                        <td align="center" class="ContentBorder" colspan="4" style="width: 600px; height: 18px;
                                                            text-align: left">
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Consolidated Pay In Excel"
                                                                Visible="False" Width="208px"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" class="ContentBorder" colspan="4" style="width: 600px">
                                                            <table class="CssBody" cellspacing="10" cellpadding="0" border="0">
                                                                <tr>
                                                                    <td style="height: 24px; width: 142px;">
                                                                        <strong>
                                                                            <asp:Label ID="Label3" runat="server" Font-Size="Medium" Height="16px" Text="Consolidated Pay In Excel"
                                                                                Width="264px"></asp:Label></strong></td>
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                    <td style="height: 24px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 142px">
                                                                        &nbsp;<asp:DropDownList ID="drpConsmth" runat="server" Font-Bold="True" Width="136px">
                                                                            <asp:ListItem Value="2015">2015-16</asp:ListItem>
                                                                            <asp:ListItem Value="2014">2014-15</asp:ListItem>
                                                                            <asp:ListItem Value="2013">2013-14</asp:ListItem>
                                                                            <asp:ListItem Value="2012">2012-13</asp:ListItem>
                                                                            <asp:ListItem Value="2011">2011-12</asp:ListItem>
                                                                            <asp:ListItem Value="2010">2010-11</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td colspan="3" style="text-align: center">
                                                                        <asp:Button ID="Button2" runat="server" CssClass="ButSmall" Text="Ok" Width="72px" /><!--<input type="reset" class="ButSmall" value="Clear">--></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
													<tr>
														<td style="text-align: center">&nbsp;
                                                            <table class="CssBody" cellspacing="10" cellpadding="0" border="0" style="width: 368px">
                                                                <tr>
                                                                    <td colspan="1" style="text-align: center">
                                                                        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="X-Small" Width="248px"></asp:Label></td>
                                                                    <td colspan="3" style="width: 73px; text-align: center">
                                                                        <asp:Button
                                                                            ID="btnClear" runat="server" CssClass="ButSmall" Text="Back" Width="72px" /></td>
                                                                </tr>
															</table>
														</td>
													</tr>
													<tr>
														<td>
                                                            <asp:GridView ID="GridView1" runat="server" Height="32px" Visible="False" Width="32px">
                                                            </asp:GridView>
                                                            &nbsp;<br>
															<br>
															<br>
															&nbsp;<br>
															</td>
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
</html>
