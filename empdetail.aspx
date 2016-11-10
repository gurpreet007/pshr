<%@ Page Language="vb" AutoEventWireup="false" Inherits="pshr.empdetail" CodeFile="empdetail.aspx.vb" %>
<HTML>
	<HEAD>
		<title>PSPCL HR Search Engine</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="css/common.css" type="text/css" rel="stylesheet">
		<style> @media Print { INPUT { DISPLAY: none }}
	@media Screen { INPUT { DISPLAY: block! important }}
		    .style1
            {
                height: 2px;
            }
		    .style2
            {
                color: #FF0000;
                font-size: x-small;
            }
		</style>
	</HEAD>
	<body class="CssBody" bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="CssBody" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr height="100%">
					<td align="center" height="100%">
						<table class="CssBody" height="100%" cellspacing="0" cellpadding="5" width="100%" border="0">
							<tr height="57">
								<td style="BACKGROUND-IMAGE: url(img/header100.jpg); BACKGROUND-REPEAT: no-repeat; HEIGHT: 60px"
									valign="bottom" align="right">
									<table cellspacing="0" cellpadding="0" border="0">
										<tr class="CssLink">
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
											<td>&nbsp;<a class="CssLink" href="frmCashLess.aspx">Cashless Medical Facility</a>&nbsp;</td>
											<td>&nbsp;|&nbsp;</td>
											<td>&nbsp;<a class="CssLink" href="findemp.aspx">Find</a>&nbsp;</td>
											<td>&nbsp;|&nbsp;</td>
											<td style="WIDTH: 124px">&nbsp;<a class="CssLink" href="changepwd.aspx">Change Password</a>&nbsp;</td>
											<td>&nbsp;|&nbsp;</td>
                                            <td>&nbsp;<a class="CssLink" href="frmGpfStmt1.aspx">GPF</a>&nbsp;</td>
                                            <td style="height: 15px">&nbsp;|&nbsp;</td>
											<td>&nbsp;<a class="CssLink" href="frmPenSlip.aspx">Pension Slip</a>&nbsp;</td>
                                            <td style="height: 15px">&nbsp;|&nbsp;</td>
                                            <td>&nbsp;<a class="CssLink" href="frmSalSlip2.aspx">Pay Slip</a>&nbsp;</td>
                                            <td>&nbsp;|&nbsp;</td>
											<td>&nbsp;<asp:button id="bLogout" onmouseover="this.style.color='blue';" onmouseout="this.style.color='#2f6a94';"
													runat="server" cssclass="DivBGColor" width="48px" borderwidth="0px" borderstyle="None" text="Logout"
													font-bold="True"></asp:button>&nbsp;</td>
											<td width="1"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td style="BACKGROUND-IMAGE: url('img/upperbar.jpg'); BACKGROUND-REPEAT: repeat; HEIGHT: 5px; text-align: left;"
									align="center"></td>
							</tr>
							<tr height="20">
								<td style="BACKGROUND-IMAGE: url('img/lowerbar.jpg'); text-align: left;" 
                                    valign="bottom" align="right"></td>
							</tr>
                            <tr height="20">
								<td style="text-align: left;" 
                                    valign="bottom" align="right"><marquee>State Bank Of Patiala Provides Personal Loans To Employees of PSPCL (Having Salary Account With SBOP) At Concessional Rate Of Interest @ 12.65% Without Check-Of-Facility. Employees Willing To Take Personal Loan May Contact SBOP Branch.</marquee></td>
							</tr>
							<tr>
								<td style="text-align: left;" align="center" class="style1">
                                    <asp:label id="lblTransferMessage" runat="server" font-bold="True" 
                                        forecolor="Red"></asp:label></td>
							</tr>
							<tr>
								<td style="text-align: left;" align="center" class="style1">
                                    <asp:label id="lMsg0" runat="server" font-bold="True" forecolor="Red"></asp:label></td>
							</tr>
							<tr>
								<td style="HEIGHT: 3px; text-align: left;" align="center">
                                    <asp:label id="lMsg1" runat="server" font-bold="True" forecolor="Red">You Can Submit Request For ICard To Concerned DDO.</asp:label></td>
							</tr>
							
							<tr>
								<td style="HEIGHT: 3px; text-align: left;" align="center">
                                    <asp:LinkButton ID="lbcpfsch" runat="server" Visible="False">CPF Arrear Details</asp:LinkButton>
                                </td>
							</tr>
							
							<tr height="100%">
								<td valign="top" align="center">
									<!--style="BACKGROUND-POSITION: right top; BACKGROUND-ATTACHMENT: fixed; BACKGROUND-IMAGE: url(img/body100.jpg); BACKGROUND-REPEAT: no-repeat"-->
									<table class="CssBody" cellspacing="0" cellpadding="0" width="650" border="0">
										<tr>
											<td colspan="2" align="right">
												<asp:button id="btnPrint" runat="server" text="Print" cssclass="ButSmall"></asp:button></td>
										</tr>
										<tr>
											<td width="100%">
												<table class="CssBody" cellspacing="0" cellpadding="0" width="100%">
													<tr>
														<td class="Header1st" align="left"><strong>Employee&nbsp;Details</strong></td>
														<td class="Header2nd" width="100%">&nbsp;</td>
													</tr>
													<tr>
														<td colspan="2">&nbsp;</td>
													</tr>
													<tr>
														<td colspan="2">
                                                            <asp:label id="Label19" runat="server" 
                                                                Font-Bold="True" ForeColor="Black" Width="600px">Incase of any discrepancy get it corrected by filling <a class="CssLink" target="_blank" href="http://pspcl.in/docs/pdf/hrdata.pdf">HR Performa</a> dully attested by the DDO.</asp:label></td>
													</tr>
													<tr>
														<td colspan="2">&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td>
												<table class="CssBody ContentBorder" cellspacing="0" cellpadding="5" width="650">
													<tr class="Report_Head">
														<td align="left" colspan="5">Pending Requests</td>
													</tr>
                                                    <tr>
                                                    <td class="LeftCaption">
                                                        <asp:LinkButton ID="lnkPRR" runat="server" Visible="False">Transfer/Promotion Relieve Requests</asp:LinkButton>
                                                    </td>
                                                    <td></td>
                                                    <td class="LeftCaption">
                                                        <asp:LinkButton ID="lnkLRR" runat="server" Visible="False">Leave Requests</asp:LinkButton>
                                                    </td>
                                                    <td></td>
                                                    <td class="LeftCaption">
                                                        <asp:LinkButton ID="lnkRR" runat="server" Visible="False">Retirement Requests</asp:LinkButton>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                    <td class="LeftCaption">
                                                        <asp:LinkButton ID="lnkPJR" runat="server" Visible="False">Transfer/Promotion Join Requests</asp:LinkButton>
                                                    </td>
                                                    <td></td>
                                                    <td class="LeftCaption">
                                                        <asp:LinkButton ID="lnkLJR" runat="server" Visible="False">Leave Join Requests</asp:LinkButton>
                                                    </td>
                                                    </tr>
													<tr class="Report_Head">
														<td align="left" colspan="5"><strong>Personal Details</strong></td>
													</tr>
													<tr>
														<td class="LeftCaption">Employee&nbsp;ID</td>
														<td><asp:label id="Label1" runat="server">Label</asp:label></td>
														<td class="LeftCaption">GPF&nbsp;No</td>
														<td><asp:label id="Label2" runat="server">Label</asp:label></td>
														<td style="MARGIN: 0px" valign="top" align="left" rowspan="6">
                                                            <asp:image id="imgEmpPhoto" runat="server" borderwidth="0px" 
                                                                borderstyle="Solid" height="127px"
																imagealign="Top" width="99px"></asp:image></td>
													</tr>
													<tr>
														<td class="LeftCaption">Name</td>
														<td><asp:label id="Label3" runat="server">Label</asp:label></td>
														<td class="LeftCaption">PAN&nbsp;No</td>
														<td><asp:label id="Label4" runat="server">Label</asp:label></td>
													</tr>
													<tr>
														<td class="LeftCaption">Father's&nbsp;Name</td>
														<td><asp:label id="Label5" runat="server">Label</asp:label></td>
														<td class="LeftCaption">Blood&nbsp;Group</td>
														<td><asp:label id="Label6" runat="server">Label</asp:label></td>
													</tr>
													<tr>
														<td class="LeftCaption">Designation</td>
														<td><asp:label id="Label7" runat="server">Label</asp:label></td>
														<td class="LeftCaption">Date&nbsp;Of&nbsp;Birth</td>
														<td><asp:label id="Label8" runat="server">Label</asp:label></td>
													</tr>
													<tr>
														<td class="LeftCaption">Office</td>
														<td><asp:label id="Label9" runat="server">Label</asp:label></td>
														<td class="LeftCaption">Date&nbsp;Of&nbsp;Join</td>
														<td><asp:label id="Label10" runat="server">Label</asp:label></td>
													</tr>
                                                    <tr>
														<td class="LeftCaption">Mobile&nbsp;No.</td>
														<td><asp:label id="Label20" runat="server">Label</asp:label></td>
                                                        <td colspan="2">&nbsp;</td>
													</tr>
                                                    <tr>
														<td class="LeftCaption">Email&nbsp;ID</td>
														<td colspan="2"><asp:label id="Label21" runat="server">Label</asp:label></td>
                                                        <td colspan="2"><asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Blue" 
                                                                Font-Size="10px">Update Mobile No. / Email ID</asp:LinkButton></td>
													</tr>
													<tr>
														<td colspan="5">&nbsp;</td>
													</tr>

                                                    <tr class="Report_Head">
														<td align="left" colspan="5"><strong>Family Details</strong></td>
													</tr>
                                                    <tr>
                                                    <td align="left" colspan="5" style="padding:0px">
                                                        <asp:GridView ID="gvFamily" runat="server" CssClass="CssBody" Width="100%" BorderWidth="0px" BorderColor="#cdc5b1"
                                                        CellPadding="5" AlternatingRowStyle-BackColor="#f8f2ef" RowStyle-VerticalAlign="Top" HorizontalAlign="Left">
                                                        <AlternatingRowStyle CssClass="Report_Text" BackColor="#F8F2EF" />
                                                        <RowStyle CssClass="Report_Text" VerticalAlign="Top" />
                                                        <HeaderStyle Font-Bold="true" CssClass="Report_Head" />
                                                        </asp:GridView>
                                                    </td>
                                                    </tr>
                                                    
                                                    <tr class="Report_Head">
														<td align="left" colspan="5"><strong>GPF Nominee(s)</strong></td>
													</tr>
                                                    <tr>
                                                    <td align="left" colspan="5" style="padding:0px">
                                                        <asp:GridView ID="gvNominee" runat="server" CssClass="CssBody" Width="100%" BorderWidth="0px" BorderColor="#cdc5b1"
                                                        CellPadding="5" AlternatingRowStyle-BackColor="#f8f2ef" RowStyle-VerticalAlign="Top" HorizontalAlign="Left">
                                                        <AlternatingRowStyle CssClass="Report_Text" BackColor="#F8F2EF" />
                                                        <RowStyle CssClass="Report_Text" VerticalAlign="Top" />
                                                        <HeaderStyle Font-Bold="true" CssClass="Report_Head" />
                                                        </asp:GridView>
                                                    </td>
                                                    </tr>
                                                    <tr>
														<td colspan="5">&nbsp;</td>
													</tr>

                                                    <tr class="Report_Head">
														<td colspan="2">&nbsp;<b>Permanent Address </b></td>
														<td colspan="2">&nbsp;<b>Correspondence Address</b></td>
                                                        <td class="LeftCaption">&nbsp;</td>
                                                    </tr>
													<tr>
														
														<td colspan="2"><asp:label id="Label11" runat="server">Label</asp:label></td>
														<td colspan="2"><asp:label id="Label15" runat="server">Label</asp:label></td>
                                                        <td class="LeftCaption">&nbsp;</td>
													</tr>
													<tr>
														
														<td colspan="2"><asp:label id="Label12" runat="server">Label</asp:label></td>
														<td colspan="2"><asp:label id="Label16" runat="server">Label</asp:label></td>
                                                        <td class="LeftCaption">&nbsp;</td>
													</tr>
													<tr>
														
														<td colspan="2"><asp:label id="Label13" runat="server">Label</asp:label></td>
														<td colspan="2"><asp:label id="Label17" runat="server">Label</asp:label></td>
                                                        <td class="LeftCaption">&nbsp;</td>
													</tr>
													<tr>
														
														<td colspan="2"><asp:label id="Label14" runat="server">Label</asp:label></td>
														<td colspan="2"><asp:label id="Label18" runat="server">Label</asp:label></td>
                                                        <td class="LeftCaption">&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colspan="5"><br />
												<br />
												<asp:label id="lMsg" runat="server"></asp:label><strong>Career History</strong><br>
												<br />
												<asp:GridView ID="gvEvents" runat="server" CssClass="CssBody" Width="100%" BorderWidth="0px" BorderColor="#cdc5b1"
                                                        CellPadding="5" AlternatingRowStyle-BackColor="#f8f2ef" RowStyle-VerticalAlign="Top" HorizontalAlign="Left">
                                                        <AlternatingRowStyle CssClass="Report_Text" BackColor="#F8F2EF" />
                                                        <RowStyle CssClass="Report_Text" VerticalAlign="Top" />
                                                        <HeaderStyle Font-Bold="true" CssClass="Report_Head" />
                                                        </asp:GridView></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<!-- #include file=footer.inc --></table>
		    <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
		</form>
	</body>
</HTML>
