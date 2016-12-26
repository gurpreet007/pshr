<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_acc_tnp.ascx.cs" Inherits="uc_acc_tnp" %>
<table class="CssBody" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
			<tr height="100%">
				<td align="center" height="100%">
					<table class="CssBody" height="100%" cellspacing="0" cellpadding="5" width="100%" border="0">
						<tr height="57">
							<td style="BACKGROUND-IMAGE: url(img/header100.jpg); BACKGROUND-REPEAT: no-repeat; HEIGHT: 60px"
								valign="bottom" align="right">
								<table cellspacing="0" cellpadding="0" border="0">
									<tr class="CssLink">
										<td>&nbsp;<a class="CssLink" href="empdetail.aspx">Home</a>&nbsp;</td>
										<td>&nbsp;|&nbsp;</td>
										<td>&nbsp;<asp:button id="bLogout" onmouseover="this.style.color='blue';" onmouseout="this.style.color='#2f6a94';"
												runat="server" cssclass="CssLink" width="48px" borderwidth="0px" borderstyle="None" text="Logout"
												font-bold="True" onclick="bLogout_Click"></asp:button>&nbsp;</td>
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
                                valign="bottom" align="right">&nbsp;</td>
						</tr>
						<tr>
							<td style="text-align: left;" align="center" class="style1">
                                <asp:label id="lMsg0" runat="server" font-bold="True" forecolor="Red"></asp:label></td>
						</tr>
						<tr>
							<td valign="top" align="center">
								<!--style="BACKGROUND-POSITION: right top; BACKGROUND-ATTACHMENT: fixed; BACKGROUND-IMAGE: url(img/body100.jpg); BACKGROUND-REPEAT: no-repeat"-->
								<table class="CssBody" cellspacing="0" cellpadding="0" width="650" border="0">
									<tr>
                                        <td align="center" style="text-align:center;width:100%">
                                            <asp:Panel ID="panAccept" runat="server">
                                        	    <asp:GridView ID="gvRequests" runat="server" style="width:100%" 
                                                    AutoGenerateSelectButton="True" 
                                                    onselectedindexchanged="gvRequests_SelectedIndexChanged">
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
									</tr>
                                    <tr>
                                    <td>
                                        <br />
                                        <br />
                                        <asp:Panel ID="panRelReq" runat="server" Visible="False">
                                            <table style="width:100%; background-color:#F0E3C6" cellpadding="4px">
                                                <tr>
                                                    <th style="text-align:center; background-color:#847963; color:#FFFFFF;">
                                                        <asp:Label ID="lblRequest" runat="server"></asp:Label>
                                                    </th>
                                                </tr>
                                            </table>
                                            <table cellpadding="4px" style="width:100%; background-color:#F0E3C6">
                                                <tr>
                                                    <th class="style3" 
                                                        style="text-align:right; background-color:#C4BDB0; color:#000000;">
                                                        <asp:Label ID="Label8" runat="server" Text="EmpID"></asp:Label>
                                                    </th>
                                                    <td class="style7" style="text-align:left">
                                                        <asp:Label ID="lblEmpID" runat="server"></asp:Label>
                                                    </td>
                                                    <td rowspan="8" style="text-align:center; vertical-align: top;">
                                                        <asp:Image ID="imgEmpPhoto" runat="server" borderstyle="Solid" 
                                                            borderwidth="0px" height="170px" imagealign="Top" 
                                                            width="138px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th class="style3" 
                                                        style="text-align:right; background-color:#C4BDB0; color:#000000;">
                                                        <asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
                                                    </th>
                                                    <td class="style7" style="text-align:left">
                                                        <asp:Label ID="lblRRName" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="text-align:right; background-color:#C4BDB0; color:#000000;" 
                                                        class="style6">
                                                        <asp:Label ID="Label3" runat="server" Text="desg"></asp:Label>
                                                    </th>
                                                    <td class="style9" style="text-align:left">
                                                        <asp:Label ID="lblRRDesg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="text-align:right; background-color:#C4BDB0; color:#000000;" 
                                                        class="style6">
                                                        <asp:Label ID="Label4" runat="server" Text="Loc"></asp:Label>
                                                    </th>
                                                    <td class="style9" style="text-align:left">
                                                        <asp:Label ID="lblRRLoc" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="text-align:right; background-color:#C4BDB0; color:#000000;" 
                                                        class="style6">
                                                        <asp:Label ID="Label5" runat="server" Text="Mobile"></asp:Label>
                                                    </th>
                                                    <td class="style9" style="text-align:left">
                                                        <asp:Label ID="lblRRMob" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th class="style6" 
                                                        style="text-align:right; background-color:#C4BDB0; color:#000000;">
                                                        <asp:Label ID="Label7" runat="server" Text="Action"></asp:Label>
                                                    </th>
                                                    <td class="style9" style="text-align:left">
                                                        <asp:DropDownList ID="drpAccept" runat="server" Width="200px" 
                                                            onselectedindexchanged="drpAccept_SelectedIndexChanged" 
                                                            AutoPostBack="True">
                                                            <asp:ListItem Value="CURR">Accept at Current Date</asp:ListItem>
                                                            <asp:ListItem Value="BACK" >Accept at Back Date</asp:ListItem>
                                                            <asp:ListItem Value="NO">Do not Accept</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="trDate" runat="server">
                                                    <th class="style6" 
                                                        style="text-align:right; background-color:#C4BDB0; color:#000000;">
                                                        <asp:Label ID="lblSelDate" runat="server" Text="Select Date"></asp:Label>
                                                    </th>
                                                    <td class="style9" style="text-align:left">
                                                        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                                                    </td>
                                                </tr>
                                                <tr id="trTime" runat="server">
                                                    <th class="style6" 
                                                        style="text-align:right; background-color:#C4BDB0; color:#000000;">
                                                        <asp:Label ID="Label1" runat="server" Text="Select Time"></asp:Label>
                                                    </th>
                                                    <td class="style9" style="text-align:left">
                                                        <asp:DropDownList ID="drpTime" runat="server" Width="80px">
                                                            <asp:ListItem Value="-1">Select</asp:ListItem>
                                                            <asp:ListItem Value="0">00:00 AM</asp:ListItem>
                                                            <asp:ListItem Value="1">01:00 AM</asp:ListItem>
                                                            <asp:ListItem Value="2">02:00 AM</asp:ListItem>
                                                            <asp:ListItem Value="3">03:00 AM</asp:ListItem>
                                                            <asp:ListItem Value="4">04:00 AM</asp:ListItem>
                                                            <asp:ListItem Value="5">05:00 AM</asp:ListItem>
                                                            <asp:ListItem Value="6">06:00 AM</asp:ListItem>
                                                            <asp:ListItem Value="7">07:00 AM</asp:ListItem>
                                                            <asp:ListItem Value="8">08:00 AM</asp:ListItem>
                                                            <asp:ListItem Value="9">09:00 AM</asp:ListItem>
                                                            <asp:ListItem Value="10">10:00 AM</asp:ListItem>
                                                            <asp:ListItem Value="11">11:00 AM</asp:ListItem>
                                                            <asp:ListItem Value="12">12:00 PM</asp:ListItem>
                                                            <asp:ListItem Value="13">01:00 PM</asp:ListItem>
                                                            <asp:ListItem Value="14">02:00 PM</asp:ListItem>
                                                            <asp:ListItem Value="15">03:00 PM</asp:ListItem>
                                                            <asp:ListItem Value="16">04:00 PM</asp:ListItem>
                                                            <asp:ListItem Value="17">05:00 PM</asp:ListItem>
                                                            <asp:ListItem Value="18">06:00 PM</asp:ListItem>
                                                            <asp:ListItem Value="19">07:00 PM</asp:ListItem>
                                                            <asp:ListItem Value="20">08:00 PM</asp:ListItem>
                                                            <asp:ListItem Value="21">09:00 PM</asp:ListItem>
                                                            <asp:ListItem Value="22">10:00 PM</asp:ListItem>
                                                            <asp:ListItem Value="23">11:00 PM</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th class="style6" 
                                                        style="text-align:right; background-color:#C4BDB0; color:#000000;">
                                                        <asp:Label ID="lblROComment" runat="server" Text="Relieving Officer Comment"></asp:Label>
                                                    </th>
                                                    <td class="style9" style="text-align:left">
                                                        <asp:TextBox ID="txtROComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th class="style6" 
                                                        style="text-align:right; background-color:#C4BDB0; color:#000000;">
                                                        <asp:Label ID="lblJOComment" runat="server" Text="Joining Officer Comment"></asp:Label>
                                                    </th>
                                                    <td class="style9" style="text-align:left">
                                                        <asp:TextBox ID="txtJOComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="text-align:right" class="style6">
                                                        &nbsp;</td>
                                                    <td colspan="2" style="text-align:left">
                                                        <asp:Button ID="btnAcceptReq" runat="server" Height="25px" 
                                                            onclick="btnAcceptReq_Click" text="Submit" 
                                                             />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="text-align:center">
                                                        <asp:Label ID="lblMsg" runat="server" Font-Size="Medium" ForeColor="#FF3300"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>

                                    </td>
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
            </table>