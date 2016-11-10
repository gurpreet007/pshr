<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGPFStmt.aspx.vb" Inherits="frmGPFStmt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="border-right: #ff3333 thick solid; border-top: #ff3333 thick solid;
            left: 368px; border-left: #ff3333 thick solid; width: 456px; border-bottom: #ff3333 thick solid;
            position: relative; top: 72px; height: 144px">
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Style="border-left-color: #ff3333;
                        border-bottom-color: #ff3333; border-top-style: solid; border-top-color: #ff3333;
                        border-right-style: solid; border-left-style: solid; border-right-color: #ff3333;
                        border-bottom-style: solid" Text="GPF STATEMENT" Width="280px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 98px">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" Text="Year"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="drpGpfYr" runat="server" Font-Bold="True" Width="232px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" Width="432px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 98px">
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Ok" Width="72px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server">Back</asp:LinkButton></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
