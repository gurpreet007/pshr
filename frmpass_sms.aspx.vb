Imports System.Net
Imports System.IO
Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page

    Private Function get_pass() As String
        Dim s As String = "ABCDEFGHJKMNOPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz23456789"
        Dim r As New Random
        Dim sb As New StringBuilder
        Dim idx As Integer
        For i As Integer = 1 To 8
            'idx = r.Next(0, 65)
            'sb.Append(s.Substring(idx, 1))
            sb.Append(s(r.Next(0, 54)))
        Next
        Return sb.ToString()
    End Function
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreq.Click
        Dim pass As String = get_pass()
        Dim orcn As New OraDBconnection
        Dim sql As String

         Dim strUrl As String = "http://sms6.routesms.com:8080/bulksms/bulksms?username=pspclinternal&password=psc35int&type=0&dlr=1&destination=" & lblmob.Text & "&source=PSPCLP&message=Your Password is:" & pass & ""
        Dim request As WebRequest = HttpWebRequest.Create(strUrl)
        request.Timeout = 25000
        Dim dataString As String
        Try
            Dim webresponse As HttpWebResponse = DirectCast(request.GetResponse, HttpWebResponse)
            Dim s As Stream = DirectCast(webresponse.GetResponseStream(), Stream)
            Dim readStream As New StreamReader(s)
            dataString = readStream.ReadToEnd()
            If Mid(dataString, 1, 4) = "1701" Then
                sql = "begin "
                sql &= "insert into pshr.pass_req values(" & txtid.Text & ",sysdate,'" & lblmob.Text & "','" & pass & "','password');"
                sql &= "merge into pshr.netlogin p using (select " & txtid.Text & " as empid from dual) d on (p.eid=d.empid)  WHEN MATCHED THEN update set p.pwd='" & pass & "' where p.eid =" & txtid.Text & " WHEN NOT MATCHED THEN insert(p.eid,p.pwd,p.qns,p.ans) values(" & txtid.Text & ",'" & pass & "',1,'" & pass & "');"
                sql &= " end;"
                orcn.ExecQry(sql)
                lblmsg.Text = "Your Password Will Be Sent To You Shortly"
                lblmob.Text = ""
                btnreq.Enabled = False
            Else
                lblmsg.Text = "Request Failed. " & dataString
                lblmob.Text = ""
                'btnreq.Enabled = True
            End If
            s.Close()
            readStream.Close()
            webresponse.Close()
        Catch ex As Exception
            lblmsg.Text = "Request Time OUT"
        End Try
    End Sub

    Protected Sub btnmob_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmob.Click
        lblmsg.Text = ""
        lblmob.Text = ""
        btnreq.Enabled = False
        Dim orcn As New OraDBconnection
        Dim ds As New DataSet
        Dim sql As String
        Dim regempid As New Regex("^[0-9]{6}$")
        Dim regdob As New Regex("^[0-9]{2}-[a-zA-Z]{3}-[0-9]{4}$")
        If Not regempid.IsMatch(txtid.Text) Then
            lblmsg.Text = "Invalid Employee ID"
            Exit Sub
        End If
        If Not regdob.IsMatch(txtdob.Text) Or Not IsDate(txtdob.Text) Then
            lblmsg.Text = "Invalid DOB "
            Exit Sub
        End If
        sql = "select count(*) from pshr.empperso where empid=" & txtid.Text & " and trunc(dob)='" & txtdob.Text & "'"
        orcn.FillData(sql, ds)
        If ds.Tables(0).Rows(0)(0) <> 1 Then
            lblmsg.Text = "Empid and DOB Not Matched With HR Data"
            Exit Sub
        End If
        ds.Clear()
        ds.Dispose()
        ds = New DataSet
        sql = "select phonecell from pshr.empaddr where empid=" & txtid.Text
        orcn.FillData(sql, ds)
        If ds.Tables(0).Rows.Count = 0 Then
            lblmob.Text = ""
            lblmsg.Text = "Mobile Number Not Registered.Request Concerned DDO"
            Exit Sub
        End If
        If IsDBNull(ds.Tables(0).Rows(0)(0)) Then
            lblmob.Text = ""
            lblmsg.Text = "Mobile Number Not Registered.Request Concerned DDO"
            Exit Sub
        End If
        If Len(ds.Tables(0).Rows(0)(0)) <> 10 Then
            lblmob.Text = ""
            lblmsg.Text = "Mobile Number Not Registered.Request Concerned DDO"
            Exit Sub
        End If
        lblmob.Text = ds.Tables(0).Rows(0)(0)
        ds.Clear()
        ds.Dispose()
        ds = New DataSet
        sql = "select count(*) from pshr.pass_req where empid=" & txtid.Text & " and usr='password'"
        orcn.FillData(sql, ds)
        If ds.Tables(0).Rows(0)(0) > 0 Then
            lblmsg.Text = "You have already submitted the request"
            Exit Sub
        End If
        ds.Clear()
        ds.Dispose()
        btnreq.Enabled = True
    End Sub

    'Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
    '    Response.Redirect("http://202.164.52.148/pshr")
    'End Sub
End Class
