Imports System.Text.RegularExpressions
Partial Class frmchangembemail
    Inherits System.Web.UI.Page

    Dim EmpId, MbNo, Email As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            MbNo = Session("MbNo")
            Email = Session("Email")

            TextBox1.Text = MbNo
            TextBox2.Text = Email
            If Email.Equals("Not Specified") Then
                TextBox2.Text = ""
            End If
            Label1.Visible = False
        End If
        
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("empdetail.aspx")
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Dim oracn As New OraDBconnection
        Dim sql As String
        Dim ds As New System.Data.DataSet
        Dim ok As Int32
        Dim rgxMbNo, rgxEmail As Regex
        'Dim connString1 As New String("user id=pshr;password=pspcl123;data source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST= localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=iut)))")
        Dim connString1 As New String("user id=salary;password=10937pspcl;data source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST= 10.10.1.99)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=pshr)))")
        EmpId = Session("EmpId")
        MbNo = TextBox1.Text
        Email = TextBox2.Text

        rgxMbNo = New Regex("^[1-9][0-9]{9}$")
        rgxEmail = New Regex("^[a-zA-Z][a-zA-Z0-9\._-]*@[a-zA-Z][a-zA-Z0-9_]*(\.[a-zA-Z]{3}|\.[a-zA-Z]{2}|(\.[a-zA-Z]{2}){2})$")

        Label1.Visible = True

        If rgxMbNo.IsMatch(MbNo) = False Then
            Label1.Text = "Mobile No. is not correct !!!"
            Return
        ElseIf Not Email.Equals("") And rgxEmail.IsMatch(Email) = False Then
            Label1.Text = "Email ID is not correct !!!"
            Return
            'Else
            '    Label1.Text = "correct !!!"
            '    Return
        End If
        If Email.Equals("") Then
            Email = "Not Specified"
        End If

        Try
            sql = "UPDATE PSHR.empaddr SET phonecell = '" & Replace(MbNo, "'", "") & "', mailid = '" & Replace(Email, "'", "") & "' WHERE empid = to_number('" & EmpId & "')"
            Dim cmd_s As New OracleClient.OracleCommand
            Dim cn As New OracleClient.OracleConnection
            cn.ConnectionString = connString1
            cn.Open()
            cmd_s.Connection = cn
            cmd_s.CommandText = sql
            cmd_s.CommandType = CommandType.Text
            ok = cmd_s.ExecuteNonQuery()
            cmd_s.Dispose()
            cn.Close()

        Catch ex As Exception
            Label1.Visible = True
            Return
        End Try
        If ok = 1 Then
            Response.Redirect("empdetail.aspx")
        Else
            Label1.Visible = True
        End If
    End Sub
End Class
