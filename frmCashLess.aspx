<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCashLess.aspx.vb" Inherits="pshr.frmCashLess" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Cashless Medical Facility</title>
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
								<td style="background-image: url(img/header100.jpg); background-repeat: no-repeat; height: 57px" valign="bottom" align="right" colspan="2">
									<table cellspacing="0" cellpadding="0" border="0">
										<tr class="CssLink">
											<td>
												&nbsp;<a class="CssLink" href="empdetail.aspx">Home</a>&nbsp;
											</td>
											<td>
												&nbsp;|&nbsp;
											</td>
											<td>
												&nbsp;<a class="CssLink" href="changepwd.aspx">Change password</a>&nbsp;
											</td>
											<td>
												&nbsp;|&nbsp;
											</td>
											<td>
												&nbsp;<asp:Button ID="bLogout" onmouseover="this.style.color='blue';" onmouseout="this.style.color='#2f6a94';" runat="server" BorderWidth="0px" BorderStyle="None" Font-Bold="True" Text="Logout" Width="48px" CssClass="CssLink"></asp:Button>&nbsp;
											</td>
											<td width="1">
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td style="background-image: url(img/upperbar.jpg); background-repeat: repeat; height: 5px" align="center" colspan="2">
								</td>
							</tr>
							<tr height="20">
								<td style="background-image: url(img/lowerbar.jpg)" valign="bottom" align="right" colspan="2">
								</td>
							</tr>
							<tr>
								<td style="height: 3px" align="center" colspan="2">
									&nbsp;
								</td>
							</tr>
							<tr height="100%">
								<td valign="top" align="center">
									<!--style="BACKGROUND-POSITION: right top; BACKGROUND-ATTACHMENT: fixed; BACKGROUND-IMAGE: url(img/body100.jpg); BACKGROUND-REPEAT: no-repeat"-->
									<table class="CssBody" cellspacing="0" cellpadding="0" width="600" border="0">
										<tbody>
											<tr>
												<td colspan="2" align="right">
													<input type="button" id="btnPrint" value="Print" class="ButSmall" onclick="cashless_printPage()" />
												</td>
											</tr>
											<tr>
												<td>
													<table class="CssBody" cellspacing="0" cellpadding="0" width="100%">
														<tr>
															<td class="Header1st" align="left" style="height: 17px; width: 250px;">
																<strong>
																	<asp:Label ID="Label0" runat="server" Text="Details for Cashless Medical Policy" Width="250px"></asp:Label></strong>
															</td>
															<td class="Header2nd" style="height: 17px;">
																&nbsp;
															</td>
														</tr>
														<tr>
															<td colspan="2">
																&nbsp;
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td id="printid" class="ContentBorder" align="center" colspan="4" style="width: 600px">
													<table class="CssBody" cellspacing="10" cellpadding="0" border="0" style="height: 48px">
														<tr class="Report_Head">
															<td align="left" colspan="3">
																<strong>Personal Details</strong>
															</td>
														</tr>
														<tr>
															<td class="LeftCaption">
																Employee&nbsp;ID
															</td>
															<td>
																<asp:Label ID="Label1" runat="server">Label</asp:Label>
															</td>
															<td style="margin: 0px" valign="top" align="left" rowspan="6">
																<asp:Image ID="imgEmpPhoto" runat="server" BorderWidth="0px" BorderStyle="Solid" Height="127px" ImageAlign="Top" Width="99px"></asp:Image>
															</td>
														</tr>
														<tr>
															<td class="LeftCaption">
																Name of Employee/Pensioner
															</td>
															<td>
																<asp:Label ID="Label3" runat="server">Label</asp:Label>
															</td>
														</tr>
														<tr>
															<td class="LeftCaption">
																Father's&nbsp;Name
															</td>
															<td>
																<asp:Label ID="Label5" runat="server">Label</asp:Label>
															</td>
														</tr>
														<tr>
															<td class="LeftCaption">
																Designation
															</td>
															<td>
																<asp:Label ID="Label7" runat="server">Label</asp:Label>
															</td>
														</tr>
														<tr>
															<td class="LeftCaption">
																Office
															</td>
															<td>
																<asp:Label ID="Label9" runat="server">Label</asp:Label>
															</td>
														</tr>
														<tr>
														<td>&nbsp;</td>
														</tr>
                                                        <tr class="Report_Head">
															<td align="left" colspan="3">
																<strong>Medical Entitlement</strong>
															</td>
														</tr>
                                                        <tr>
															<td class="LeftCaption">
																<asp:Label ID="lblMedEnt" runat="server"></asp:Label>
															</td>
															<td>
																&nbsp;</td>
														</tr>
                                                        <tr>
															<td class="LeftCaption">
																&nbsp;</td>
															<td>
																&nbsp;</td>
														</tr>
														<tr class="Report_Head">
															<td align="left" colspan="3">
																<strong>Registered Family Members</strong>
															</td>
														</tr>
														<tr>
															<td align="left" colspan="3" style="padding: 0px">
																<asp:Label ID="RegMembers_Lbl" runat="server" Text="Label"></asp:Label>
																<asp:GridView ID="gvFamily" runat="server" CssClass="CssBody" Width="100%" BorderWidth="0px" BorderColor="#CDC5B1" CellPadding="5" AlternatingRowStyle-BackColor="#f8f2ef" RowStyle-VerticalAlign="Top" HorizontalAlign="Left" AutoGenerateColumns="False">
																	<AlternatingRowStyle CssClass="Report_Text" BackColor="#F8F2EF" />
																	<RowStyle CssClass="Report_Text" VerticalAlign="Top" />
																	<Columns>
																		<asp:BoundField DataField="Name" HeaderText="Name" />
																		<asp:BoundField DataField="Relation" HeaderText="Relation" />
																		<asp:BoundField DataField="DOB" HeaderText="DOB" />
																		<asp:ImageField DataImageUrlField="photo2" HeaderText="Image" ItemStyle-HorizontalAlign="Center">
																			<ControlStyle Height="128px" Width="100px" />
																		</asp:ImageField>
																	</Columns>
																	<HeaderStyle Font-Bold="true" CssClass="Report_Head" />
																</asp:GridView>
															</td>
														</tr>
														<tr>
															<td colspan="3">
																&nbsp;
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td>
													<table class="CssBody" cellspacing="0" cellpadding="0" width="100%">
														<tr>
															<td class="Header1st" align="left" style="height: 17px; width: 250px;"><br />
																<strong>
																	<asp:Label ID="Label2" runat="server" Text="List Of Empanneled Hospitals" Width="250px"></asp:Label></strong>
															</td>
															<td class="Header2nd" style="height: 17px;">
																&nbsp;
															</td>
														</tr>
														<tr><td style="padding:10px;font-weight:bold">Select City &nbsp;&nbsp;&nbsp;&nbsp;
																<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
																</asp:DropDownList></td></tr>
														<tr>
															<td colspan="2">

																<asp:GridView ID="gvHospital" runat="server" CssClass="CssBody" Width="100%" BorderWidth="0px" BorderColor="#CDC5B1" CellPadding="5" AlternatingRowStyle-BackColor="#f8f2ef" RowStyle-VerticalAlign="Top" HorizontalAlign="Left" AutoGenerateColumns="False">
																	<AlternatingRowStyle CssClass="Report_Text" BackColor="#F8F2EF" />
																	<RowStyle CssClass="Report_Text" VerticalAlign="Top" />
																	<Columns>
																		<asp:BoundField DataField="hname" HeaderText="Hospital Name" />
																		<asp:BoundField DataField="haddr" HeaderText="Address" />
																		<asp:BoundField DataField="hcity" HeaderText="City" />
																		<asp:BoundField DataField="hcontact" HeaderText="Contact Person" />
																		<asp:BoundField DataField="hmobile" HeaderText="Mobile/Phone No" />
																		<asp:BoundField DataField="hcontract" HeaderText="Contract Upto" />
																		<asp:TemplateField HeaderText="Attachment">
																			<ItemTemplate>
																				<asp:HyperLink ID="HyperLink1" runat="server" 
                                                                                    NavigateUrl='<%# "http://115.249.65.148//CMT/procedure_uploaded/" + Eval("Link") %>' 
                                                                                    Text='<%# Eval("attach") %>' Target="_blank"></asp:HyperLink>
																			</ItemTemplate>
																		</asp:TemplateField>
																	</Columns>
																	<HeaderStyle Font-Bold="true" CssClass="Report_Head" />
																</asp:GridView>
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</tbody>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									&nbsp;
								</td>
							</tr>
						</tbody>
					</table>
				</td>
			</tr>
			<!-- #include file=footer.inc -->
		</tbody>
	</table>
	</form>
</body>
<script type="text/javascript">
	function cashless_printPage() {
		var srchtml = document.getElementById('printid').innerHTML.replace(/(\r\n|\n|\r|\t)/gm, "")

		var url = document.location.href

		var html = '<html><head><link rel="stylesheet" type="text/css" href="css/common.css" />';
		html += "<style>@page { size:8.27in 11.69in; margin:55px 20px 20px 55px;} </style></head><body>";
		html +="<div style='width:100%;text-align:center;font-weight:bold'>Details for Cashless Medical Policy</div></br>"
		var printWin = window.open('', '', 'left=0,top=0,width=screen.Width,height=screen.height,toolbar=0,scrollbars=0,status=0');
		printWin.document.write(html + srchtml + '</body></html>');

		printWin.document.close();
		printWin.focus();
		setTimeout(function () {
			printWin.print();
			printWin.close();
		}, 1000);
	}
</script>
</html>
