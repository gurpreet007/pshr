<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_req_tnp.ascx.cs" Inherits="tnp_request" %>
  <%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
  <style type="text/css">
      .style1
      {
      }
      .style3
      {
          width: 22px;
      }
      .style4
      {
          width: 194px;
      }
  </style>
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
							<td valign="top" align="center">
								<!--style="BACKGROUND-POSITION: right top; BACKGROUND-ATTACHMENT: fixed; BACKGROUND-IMAGE: url(img/body100.jpg); BACKGROUND-REPEAT: no-repeat"-->
								<table class="CssBody" cellspacing="0" cellpadding="0" width="650" border="0">
									<tr>
                                        <td align="center" style="text-align:center;width:100%">
                                        	<asp:GridView ID="gvPosting" runat="server" style="width:100%" 
                                                AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="Office Order" HeaderText="Office Order" />
                                                    <asp:BoundField DataField="Present Loc" HeaderText="Present Loc" />
                                                    <asp:BoundField DataField="New Loc" HeaderText="New Loc" />
                                                    <asp:BoundField DataField="Relieving Officer" HeaderText="Relieving Officer" />
                                                    <asp:BoundField DataField="Relieving Request Date" 
                                                        HeaderText="Relieving Request Date" />
                                                    <asp:BoundField DataField="Relieving Accept Date" 
                                                        HeaderText="Relieving Accept Date" />
                                                    <asp:BoundField DataField="Joining Officer" HeaderText="Joining Officer" />
                                                    <asp:BoundField DataField="Joining Request Date" 
                                                        HeaderText="Joining Request Date" />
                                                    <asp:BoundField DataField="Joining Accept Date" 
                                                        HeaderText="Joining Accept Date" />
                                                    <asp:TemplateField HeaderText="Relieving Report">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibtnRelieveRep" runat="server" Height="32px" 
                                                                ImageUrl="~/img/imgDownload2.png" onclick="ibtnRelieveRep_Click" Width="32px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Joining Report">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibtnJoiningRep" runat="server" Height="32px" 
                                                                ImageUrl="~/img/imgDownload2.png" Width="32px" 
                                                                onclick="ibtnJoiningRep_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
									</tr>
                                    
                                    <tr><td align="left">
                                        <asp:LinkButton ID="lnkShowAll" runat="server" onclick="lnkShowAll_Click">Show All Charge Reports</asp:LinkButton>
                                        </td></tr>
                                    
                                    <tr><td>&nbsp;</td></tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMobile" runat="server" ForeColor="Red"></asp:Label>
                                            <asp:LinkButton ID="linkupdateMobile" runat="server" ForeColor="Blue" 
                                                onclick="linkupdateMobile_Click">Click here</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr><td>
                                        <asp:LinkButton ID="lb_ChangeRepOfficer" runat="server" 
                                            onclick="lb_ChangeRepOfficer_Click" style="text-align: left" Visible="False"></asp:LinkButton>
                                        </td></tr>
                                    <tr>
                                        <td align="center" style="text-align:center;width:100%">
                                            &nbsp;</td>
									</tr>
                                    <tr>
                                        <td align="center" style="text-align:center;width:100%">
                                            <asp:Panel ID="panRelReq0" runat="server">
                                                <table style="width:100%; background-color:#F0E3C6" cellpadding="4px">
                                                    <tr>
                                                        <th style="text-align:center; background-color:#847963; color:#FFFFFF;" 
                                                    colspan="2">
                                                            <asp:Label ID="lblRequest0" runat="server">Officer Comments</asp:Label>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th class="style6" 
                                                    style="text-align:right; background-color:#C4BDB0; color:#000000;">
                                                            <asp:Label ID="lblROComment" runat="server" Text="Relieving Officer Comments"></asp:Label>
                                                        </th>
                                                        <td style="text-align:left">
                                                            <asp:TextBox ID="txtROComment" runat="server" AutoPostBack="True" MaxLength="6" 
                                                        ontextchanged="txtRREmpid_TextChanged" Width="313px" Enabled="False" Height="47px" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th class="style6" 
                                                            style="text-align:right; background-color:#C4BDB0; color:#000000;">
                                                            <asp:Label ID="Label7" runat="server" Text="Joining Officer Comments"></asp:Label>
                                                        </th>
                                                        <td style="text-align:left">
                                                            <asp:TextBox ID="txtJOComment" runat="server" AutoPostBack="True" 
                                                                Enabled="False" Height="47px" MaxLength="6" 
                                                                ontextchanged="txtRREmpid_TextChanged" TextMode="MultiLine" Width="313px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
									</tr>
                                    <tr>
                                        <td align="center" style="text-align:center;width:100%">
                                            &nbsp;</td>
									</tr>
                                    <tr>
                                    <td>
                                        <asp:Panel ID="PanChangeEmpid" runat="server" Visible="False">
                                            <table style="width:100%; background-color:#F0E3C6" cellpadding="4px">
                                                <tr>
                                                    <th style="text-align:center; background-color:#847963; color:#FFFFFF;">
                                                        <asp:Label ID="Label6" runat="server" Font-Size="Medium">Change EmpID</asp:Label>
                                                    </th>
                                                </tr>
                                            </table>
                                            <table cellpadding="4px" style="width:100%; background-color:#F0E3C6">
                                                <tr>
                                                    <th class="style4" 
                                                        style="text-align:right; background-color:#C4BDB0; color:#000000;" 
                                                        width="50%">
                                                        <asp:Label ID="Label9" runat="server" Text="Old EmpID" Font-Size="Medium"></asp:Label>
                                                    </th>
                                                    <td class="style7" style="text-align:left">
                                                        <asp:Label ID="lblOldEmpid" runat="server" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="text-align:right; background-color:#C4BDB0; color:#000000;" 
                                                        class="style4" width="50%">
                                                        <asp:Label ID="Label11" runat="server" Text="New EmpID" Font-Size="Medium"></asp:Label>
                                                    </th>
                                                    <td class="style9" style="text-align:left">
                                                        <asp:Label ID="lblNewEmpid" runat="server" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align:left" align="right">
                                                    <b>
                                                        Please Note:<br /> <br /> 1. Clicking &#39;Change EmpID&#39; button will change your 
                                                        Employee ID from old non-gazetted
                                                        <br />
                                                        &nbsp;&nbsp;&nbsp; EmpID to new gazetted EmpID.<br /> <br /> 2. Once EmpID is changed your old 
                                                        EmpID will be disabled and you will not be able to access<br /> &nbsp;&nbsp;&nbsp; your 
                                                        payslips. Hence, you are adviced to take backup of the payslips.<br /> <br /> 3. 
                                                        After your EmpID is changed you will be logged out of the HR Package, to Login 
                                                        again to<br /> &nbsp;&nbsp;&nbsp; HR 
                                                        Package use your new EmpID. Password will be same as your EmpID.<br />
                                                        <br />
                                                    </td>
                                                    </b>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="2" style="text-align:center">
                                                        <asp:CheckBox ID="chkUnderstandChange" runat="server" AutoPostBack="True" 
                                                            Font-Bold="True" oncheckedchanged="chkUnderstandChange_CheckedChanged" 
                                                            Text="I understand the above and I want to change my EmpID" 
                                                            Font-Size="Medium" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1" style="text-align:center" colspan="2">
                                                        <asp:Button ID="btnChangeEmpid" runat="server" Enabled="False" Font-Bold="True" 
                                                            Height="26px" onclick="btnChangeEmpid_Click" Text="Change EmpID" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:center" colspan="2">
                                                        <asp:Label ID="lblChangeEmpIDMsg" runat="server" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>
                                        <br />
                                        <br />
                                        <asp:Panel ID="panRelReq" runat="server">
                                            <table style="width:100%; background-color:#F0E3C6" cellpadding="4px">
                                                <tr>
                                                    <th style="text-align:center; background-color:#847963; color:#FFFFFF;" colspan="2">
                                                        <asp:Label ID="lblRequest" runat="server"></asp:Label>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <th class="style6" 
                                                        style="text-align:right; background-color:#C4BDB0; color:#000000;">
                                                        <asp:Label ID="Label1" runat="server" Text="EmpID of reporting officer"></asp:Label>
                                                    </th>
                                                    <td style="text-align:left">
                                                        <asp:TextBox ID="txtRREmpid" runat="server" AutoPostBack="True" MaxLength="6" 
                                                            ontextchanged="txtRREmpid_TextChanged" Width="103px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table cellpadding="4px" style="width:100%; background-color:#F0E3C6">
                                                <tr>
                                                    <th class="style3" 
                                                        style="text-align:right; background-color:#C4BDB0; color:#000000;">
                                                        <asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
                                                    </th>
                                                    <td class="style7" style="text-align:left">
                                                        <asp:Label ID="lblRRName" runat="server"></asp:Label>
                                                    </td>
                                                    <td rowspan="4" style="text-align:left">
                                                        <asp:Image ID="imgEmpPhoto" runat="server" borderstyle="Solid" 
                                                            borderwidth="0px" height="114px" imagealign="Top" 
                                                            width="86px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th style="text-align:right; background-color:#C4BDB0; color:#000000;" 
                                                        class="style6">
                                                        <asp:Label ID="Label3" runat="server" Text="Desg"></asp:Label>
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
                                                    <td style="text-align:right" class="style6">
                                                        &nbsp;</td>
                                                    <td colspan="2" style="text-align:left">
                                                        <asp:Button ID="btnSubReq" runat="server" Height="26px" 
                                                            onclick="btnSubReq_Click" />
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
            </table>