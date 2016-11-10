<%@ Page Language="vb" AutoEventWireup="false" Inherits="pshr.login" EnableSessionState="True" enableViewState="True" enableViewStateMac="False" CodeFile="login.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PSPCL HR Search Engine</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="css/login.css" type="text/css" rel="stylesheet">
		<script src="JScriptOne.js" type="text/javascript"></script>
        <script language="javascript" type = "text/javascript" >            window.history.forward(1);
        </script>
	    <style type="text/css">
            .style1
            {
                color: #996633;
            }
        </style>
	</HEAD>
	<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
		<div style="FONT-SIZE: 6pt">
			<form id="Form1" method="post" runat="server">
				<table style="BACKGROUND-COLOR: #595959" height="100%" cellspacing="0" cellpadding="0"
					width="100%" border="0">
					<tr>
						<td width="33%"></td>
						<td width="33%"></td>
						<td width="33%"></td>
					</tr>
					<tr>
						<td></td>
						<td>
							<table cellspacing="0" cellpadding="0" width="627" border="0">
								<tr>
									<td style="BACKGROUND-IMAGE: url(img/login100.gif); WIDTH: 627px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 114px"></td>
								</tr>
								<tr>
                                                                    <td style="background-color: #003366">
                                                                        <asp:Label ID="Label2" runat="server" 
                                                                            Width="618px" ForeColor="White"></asp:Label>
                                        </td>
                                </tr>
								<tr>
									<td style="BACKGROUND-IMAGE: url(img/login200.jpg); WIDTH: 627px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 204px"
										align="right">
										<table cellspacing="0" cellpadding="0" border="0">
											<tr>
												<td style="WIDTH: 195px" valign="middle" align="center">
													<table style="WIDTH: 253px; HEIGHT: 166px" cellspacing="0" cellpadding="0"
														border="0">
														<tr>
															<td style="WIDTH: 334px" align="left" colspan="2"><asp:label id="lMsg" runat="server" font-bold="True" visible="False" forecolor="Red"></asp:label>&nbsp;
															</td>
															<td style="WIDTH: 334px" align="left">&nbsp;</td>
														</tr>
														<tr>
															<td style="WIDTH: 181px">&nbsp;</td>
															<td style="WIDTH: 229px">&nbsp;</td>
															<td style="WIDTH: 229px">&nbsp;</td>
														</tr>
														<tr>
															<td style="WIDTH: 181px" align="right"><img src="img/textusr.jpg"></td>
															<td style="WIDTH: 229px">
                                                                <asp:textbox id="tUsr" runat="server" borderwidth="1px" 
                                                                    borderstyle="Solid" bordercolor="DarkGray"
																	width="140px" MaxLength="6"></asp:textbox></td>
															<td style="WIDTH: 229px">&nbsp;</td>
														</tr>
														<tr>
															<td style="WIDTH: 181px" align="right"><img src="img/textpwd.jpg" alt="" /></td>
															<td style="WIDTH: 229px">
                                                                <asp:textbox id="tPwd" runat="server" borderwidth="1px" 
                                                                    borderstyle="Solid" bordercolor="DarkGray"
																	width="140px" textmode="Password" MaxLength="30"></asp:textbox></td>
															<td style="WIDTH: 229px">&nbsp;</td>
														</tr>
														<tr>
															<td style="WIDTH: 181px; HEIGHT: 3px"></td>
															<td style="WIDTH: 229px; HEIGHT: 3px"></td>
															<td style="WIDTH: 229px; HEIGHT: 3px">&nbsp;</td>
														</tr>
														<tr>
															<td style="WIDTH: 181px">&nbsp;</td>
															<td style="WIDTH: 229px"><asp:imagebutton id="bGo" runat="server" imageurl="btn/loginGo.gif"></asp:imagebutton></td>
															<td style="WIDTH: 229px">&nbsp;</td>
														</tr>
														<tr>
															<td style="WIDTH: 181px">&nbsp;</td>
															<td style="WIDTH: 229px">&nbsp;</td>
															<td style="WIDTH: 229px">&nbsp;</td>
														</tr>
														<tr>
															<td style="WIDTH: 334px" colspan="2"></td>
															<td style="WIDTH: 334px">&nbsp;</td>
														</tr>
														<tr>
															<td colspan="2">
                                                                <a style="text-align: center;" 
                                                                    href="frmFindEmp.aspx" class="style1">Find Employee Id (For Pensioners Only)</a></td>
															<td style="WIDTH: 334px">&nbsp;</td>
														</tr>
														<tr>
															<td colspan="2">&nbsp;</td>
															<td style="WIDTH: 334px">&nbsp;</td>
														</tr>
														<tr>
															<td colspan="3" style="text-align: left"> 
										<table cellspacing="0" cellpadding="0">
											<tr>
												<td><a style="COLOR: #4b4b4b" href="confirm.aspx">Registration</a>&nbsp;</td>
												<td>&nbsp;<a style="COLOR: #4b4b4b" href="frmpass_sms.aspx">Forgot Password</a>
                                               <%-- <asp:LinkButton ID="lbpass" runat="server" ForeColor="#4B4B4B">Forgot Password</asp:LinkButton>--%>
                                                    &nbsp;</td>
												<td>&nbsp;<a style="COLOR: #4b4b4b" href="mailto:hr@psebindia.org">Write Mail</a>&nbsp;&nbsp;</td>
											</tr>
										</table>
									                        </td>
														</tr>
													</table>
												</td>
												<td style="BACKGROUND-IMAGE: url(img/login201.jpg); WIDTH: 1px; BACKGROUND-REPEAT: repeat-y">
                                                    &nbsp;</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td bgcolor="#f9f1de" height="114">
										<P>
                                            &nbsp;<asp:HyperLink 
                                                ID="HyperLink2" runat="server" Font-Bold="True" Font-Size="Small"
                                                Height="16px" NavigateUrl="http://www.pspcl.in" 
                                                Width="45px" Visible="True">Home</asp:HyperLink><asp:HyperLink 
                                                ID="HyperLink1" runat="server" Font-Bold="True" Font-Size="Small"
                                                Height="16px" NavigateUrl="http://www.pspcl.in/docs/hr_information.htm" 
                                                Width="336px" Visible="False">Click For Updation of Employees Data</asp:HyperLink>&nbsp;
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Style="left: -282px;
                                                position: relative; top: -35px" Text=" For Official Use Only" Visible="False"></asp:Label>
                                        </P>
									</td>
									<!--style="BACKGROUND-IMAGE: url(img/login300.jpg);WIDTH: 627px;BACKGROUND-REPEAT: no-repeat;HEIGHT: 147px"--></tr>
								<tr>
									<td>
										<table style="COLOR: #aaaaaa; BACKGROUND-COLOR: #595959" cellspacing="0" cellpadding="0"
											width="100%" border="0">
											<tr>
												<td align="left">©2008 Punjab State Power Corporation Ltd.</td>
												<td align="right"><a style="COLOR: #aaaaaa;	TEXT-DECORATION: none" href="http://www.phoenix.co.in">Powered 
														by O/o SE/IT PSPCL Patiala</a></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
						<td></td>
					</tr>
					<tr>
						<td width="33%"></td>
						<td width="33%"></td>
						<td width="33%"></td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>
