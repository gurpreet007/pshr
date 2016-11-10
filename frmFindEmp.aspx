<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmFindEmp.aspx.vb" Inherits="frmFindEmp" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PSPCL HR Search Engine </title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="css/common.css" type="text/css" rel="stylesheet">
		<script src="JScriptOne.js" type="text/javascript"></script>
</head>
<body  class="CssBody" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="form1" runat="server" method="post">
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
													<td>&nbsp;|&nbsp;</td>
													<td>&nbsp;<a class="CssLink" href="login.aspx">Login</a>&nbsp;</td>
													<td>&nbsp;|&nbsp;</td>
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
                                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                                            </asp:ScriptManager>
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
																	<td>File No</td>
																	<td><asp:textbox id="tfName" runat="server" cssclass="txtFont" MaxLength="8"></asp:textbox></td>
																	<td>(Numeric Part Only)</td>
																</tr>
																<tr>
																	<td>DOB</td>
																	<td><asp:textbox id="tdob" runat="server" cssclass="txtFont"></asp:textbox>
                                                                        <cc1:CalendarExtender ID="tdob_CalendarExtender" runat="server" Enabled="True" 
                                                                            Format="dd-MMM-yyyy" TargetControlID="tdob">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
																	<td>e.g. 05-Aug-1980</td>
																</tr>
																<tr>
																	<td>&nbsp;</td>
																	<td colspan="2"><asp:button id="btnFind" runat="server" text="Find" cssclass="ButSmall"></asp:button><asp:button id="btnClear" runat="server" text="Clear" cssclass="ButSmall"></asp:button><!--<input type="reset" class="ButSmall" value="Clear">--></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td>&nbsp;</td>
													</tr>
													<tr>
														<td style="text-align: left"><br>
															<asp:label id="lMsg" runat="server" font-bold="True" ForeColor="#CC0000"></asp:label><br>
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
</html>
