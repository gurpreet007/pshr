<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPenSlip.aspx.vb" Inherits="frmPenSlip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pension Slip</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<link href="css/common.css" type="text/css" rel="stylesheet">
</head>
<body class="CssBody" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="form1" runat="server">
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
																	<td class="Header1st" align="left" style="height: 17px"><strong>
                                                                        <asp:Label ID="Label1" runat="server" Text="Pension Slip" Width="144px" 
                                                                            Height="16px"></asp:Label></strong></td>
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
															<table class="CssBody" cellspacing="10" cellpadding="0" border="0">
																<tr>
																	<td style="height: 24px">
                                                                        <strong>
                                                                            <asp:Label ID="Label2" runat="server" Text="Year"></asp:Label></strong></td>
																	<td style="height: 24px">
                                                                        <asp:DropDownList ID="drpGpfYr" runat="server" Font-Bold="True" Width="232px">
                                                                        </asp:DropDownList></td>
																	<td style="height: 24px"></td>
																	<td style="height: 24px"></td>
																</tr>
																<tr>
																	<td>&nbsp;</td>
																	<td colspan="3">
                                                                        <asp:Button ID="Button1" runat="server" CssClass="ButSmall" Text="Ok" /><asp:Button
                                                                            ID="btnClear" runat="server" CssClass="ButSmall" Text="Back" /><!--<input type="reset" class="ButSmall" value="Clear">--></td>
																</tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="X-Small" Width="272px"></asp:Label></td>
                                                                </tr>
															</table>
														</td>
													</tr>
													<tr>
														<td>
															<table class="CssBody" cellspacing="0" cellpadding="0" width="100%">
																<tr>
																	<td class="Header1st" align="left" style="height: 17px"><strong>
                                                                        <asp:Label ID="Label3" runat="server" Text="Consolidated Pension" Width="151px"></asp:Label></strong></td>
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
															<table class="CssBody" cellspacing="10" cellpadding="0" border="0">
																<tr>
																	<td style="height: 24px">
                                                                        <strong>
                                                                            <asp:Label ID="Label4" runat="server" Text="Year"></asp:Label></strong></td>
																	<td style="height: 24px">
                                                                        <asp:DropDownList ID="drpConsmth" runat="server" Font-Bold="True" Width="232px">
                                                                            <asp:ListItem Value="2015">2015-16</asp:ListItem>
                                                                            <asp:ListItem Value="2014">2014-15</asp:ListItem>
                                                                            <asp:ListItem Value="2013">2013-14</asp:ListItem>
                                                                            <asp:ListItem Value="2012">2012-13</asp:ListItem>
                                                                            <asp:ListItem Value="2011">2011-12</asp:ListItem>
                                                                        </asp:DropDownList></td>
																	<td style="height: 24px"></td>
																	<td style="height: 24px"></td>
																</tr>
																<tr>
																	<td>&nbsp;</td>
																	<td colspan="3">
                                                                        <asp:Button ID="Button2" runat="server" CssClass="ButSmall" Text="Ok" /><!--<input type="reset" class="ButSmall" value="Clear">--></td>
																</tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td colspan="3">
                                                            <asp:GridView ID="GridView1" runat="server" Height="32px" Visible="False" Width="32px">
                                                            </asp:GridView>
                                                                    </td>
                                                                </tr>
															</table>
														</td>
													</tr>
													<tr>
														<td>
                                                            <br>
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
										<td>
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </td>
									</tr>
								</tbody>
							</table>
						</td>
					</tr>
					<!-- #include file=footer.inc --></tbody></table>
    </form>
</body>
</html>
