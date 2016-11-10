<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptSalSlip.aspx.vb" Inherits="RptSalSlip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server" Height="50px" Style="left: 380px; position: relative;
            top: 188px" Width="125px">
            <table id="tab1" style="left: -3px; position: static; top: 7px">
                <tr>
                    <td style="width: 92px; height: 21px">
                    </td>
                    <td style="width: 149px; height: 21px">
                    </td>
                    <td style="width: 95px; height: 21px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 92px; height: 21px">
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="left: -267px; position: static;
            top: 330px" Text="Select Month/Year" Width="148px"></asp:Label></td>
                    <td style="width: 149px; height: 21px">
        <asp:DropDownList ID="drpSalMth" runat="server" Style="left: 20px; position: static;
            top: 106px" Width="200px">
        </asp:DropDownList></td>
                    <td style="width: 95px; height: 21px">
                        <asp:Button ID="Button1" runat="server" Height="24px" Style="left: -98px; position: static;
            top: 106px" Text="Go" Width="48px" /></td>
                </tr>
                <tr>
                    <td style="width: 92px">
                    </td>
                    <td style="width: 149px">
                    </td>
                    <td style="width: 95px">
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Label ID="Label2" runat="server" Style="left: 380px; position: relative; top: 69px"
            Text="Please contact HR Administrator for updation of your data." Visible="False"
            Width="366px"></asp:Label>
        &nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Height="24px" Style="left: 363px; position: relative;
            top: 69px" Text="Back" Width="48px" Visible="False" /></div>
    </form>
</body>
</html>
