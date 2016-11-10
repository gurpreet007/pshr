<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GPFErr.aspx.vb" Inherits="GPFErr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Height="67px" Text="Your GPF details are not upto date in HR Database. Please Contact HR Department to update the details."
            Width="377px"></asp:Label><br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:LinkButton ID="LinkButton1" runat="server">Back</asp:LinkButton></div>
    </form>
</body>
</html>
