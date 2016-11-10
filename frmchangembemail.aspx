<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmchangembemail.aspx.vb" Inherits="frmchangembemail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PSPCL HR Search Engine</title>
</head>
<body class="CssBody" style="margin:150px 150px 150px 150px">
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <table class="CssBody" cellspacing="0px" cellpadding="5px" width="400px" border="1">
                        <tr>
                           <td style="BACKGROUND-IMAGE: url('img/upperbar.jpg'); BACKGROUND-REPEAT: repeat; HEIGHT: 5px; text-align: left;"
									align="center" colspan="3">
                           </td>
                        </tr>
                        <tr>
							<td colspan="3" align="center" style="color:blue;"> Update Mobile No. / Email ID</td>
			            </tr>
                        <tr>
							<td class="LeftCaption">Mobile&nbsp;No.</td>
							<td colspan="2">
                                <asp:TextBox ID="TextBox1" runat="server" MaxLength="10" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp;(only 10 digits)
                            </td>
			            </tr>
                        <tr>
						    <td class="LeftCaption">Email&nbsp;ID</td>
							<td colspan="2">
                                <asp:TextBox ID="TextBox2" runat="server" Width="95%"></asp:TextBox>
                            </td>
			            </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td colspan="2" style="text-align:center;"> 
                                <asp:Button ID="Button1" runat="server" Text="Back" />&nbsp;&nbsp;
                                <asp:Button ID="Button2" runat="server" Text="Update" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red" BackColor="Yellow" Visible="true"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
