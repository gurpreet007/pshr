<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmpass_sms.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        th
        {
            text-align: left;
        }
        .style1
        {
            text-decoration: underline;
        }
        .style2
        {
            font-weight: bold;
            font-size: medium;
        }
        .style3
        {
            color: #000066;
        }
        .style4
        {
            text-align: left;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <div style="border: thick solid #808080; width:350px ; margin-left:auto; margin-right:auto">
   <noscript align="center" style="font-size:x-large;color:Red">Please Enable Javascript</noscript>
       <table align="center" style="width: 348px">
           <tr style="text-align:center; font-size:x-large;color:Blue">
        <td colspan="2" class="style1"><strong>Get Password Via SMS</strong></td>
        </tr>
        <tr>
        <td colspan="2" style="height:20px; text-align: right;">
        <a href="login.aspx">back</a>
            <%--<asp:LinkButton ID="LinkButton1" runat="server">back</asp:LinkButton>--%>
            </td>
        </tr>
        <tr>
        <th class="style3">Employee ID</th><td style="width:230px" class="style4"><asp:TextBox id ="txtid" runat="server" Text="" MaxLength="6" BorderStyle="Double" CssClass="style2" style="font-family: Arial; text-align: left" Width="115px" Height="25px" ></asp:TextBox></td>
        </tr>
       <tr>
        <th><span class="style3">DOB</span> </th><td class="style4"><asp:TextBox ID="txtdob" runat="server" Text="" MaxLength="11" BorderStyle="Double" CssClass="style2" style="font-family: Arial; text-align: left;" Width="115px" Height="25px"></asp:TextBox></td>
        </tr>
       <tr>
       <td style="text-align: right">Example-</td>
       <td>(02-Apr-1970)</td>
       </tr>
       
       <tr>
       <td>&nbsp;</td>
       <td>&nbsp;</td>
       </tr>
       
       <tr style="text-align:center"> 
       <td colspan="2"><asp:Button ID="btnmob" runat="server" Text="Check Registered Mobile No" style="font-weight: 700" /></td>
       </tr>      
       <tr>
       <td>&nbsp;</td>
       <td>&nbsp;</td>
       </tr>
       <tr>
       <th class="style3">&nbsp;Mobile No.</th>
       <td><asp:Label ID="lblmob" runat="server" text="" style="font-weight: 700; font-size: x-large; color: #660033"/></td>
       </tr>
       <tr>
       <th class="style3">&nbsp;</th>
       <td>&nbsp;</td>
       </tr>
       <tr>
       <td colspan="2"><asp:Label ID="lblmsg" runat="server" text="" style="color: #CC3300; font-weight: 700"/></td>
       </tr>
        <tr style="text-align:center">
       <td colspan="2">
        <asp:Button ID="btnreq" runat="server" Text="Send Request" Enabled="False" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" style="font-weight: 700" /></td>
   </tr>
    </table>
    </div>
    </form>
</body>
</html>
