
Partial Class frmGPFStmt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim OraCn As New OraDBconnection
            Dim ds As New DataSet
            Dim sql As String
            Dim s As String
            s = ""
            sql = "select empid,gpftype,gpfno from pshr.empperso where empid = " & Session("EmpId")
            OraCn.FillData(sql, ds)
            If Not IsDBNull(ds.Tables(0).Rows(0)(1)) Then
                s = ds.Tables(0).Rows(0)(1)
            End If
            ds.Clear()
            ds.Dispose()
            If s = "C" Or s = "E" Then
                setDat()
                lblMsg.Text = "GPF statement not available. You may contact GPF section for the same"
                Exit Sub
            End If
            If s = "G" Then
                ds = New DataSet()
                sql = "select substr(tname,4,4) || '-' || substr(tname,8,2) as tname,substr(tname,4,6) tname1 from tab where tname like 'GPF20%' and length(tname) = 9 order by tname"
                OraCn.FillData(sql, ds)
                Dim i As Integer
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    Dim ds1 As New DataSet()
                    sql = "select count(*) from GPF" & ds.Tables(0).Rows(i)(1) & " where gpfno = '" & Session("EmpGpf") & "'"
                    OraCn.FillData(sql, ds1)
                    If ds1.Tables(0).Rows(0)(0) > 0 Then
                        drpGpfYr.Items.Add(New ListItem(ds.Tables(0).Rows(i)(0), ds.Tables(0).Rows(i)(1)))
                    End If
                    ds1.Clear()
                    ds1.Dispose()
                Next i
                ds.Clear()
                ds.Dispose()
                If drpGpfYr.Items.Count > 0 Then
                    drpGpfYr.SelectedIndex = drpGpfYr.Items.Count - 1
                Else
                    setDat()
                    lblMsg.Text = "GPF statement not available. You may contact GPF section for the same"
                End If

            Else
                setDat()
                lblMsg.Text = "GPF statement not available. You may contact GPF section for the same"
            End If
        End If
    End Sub

    Private Sub setDat()
        Label2.Visible = False
        drpGpfYr.Visible = False
        Button1.Visible = False
    End Sub
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("empdetail.aspx")
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Session("rptNm") = "rptgpf"
        Session("fyr") = drpGpfYr.SelectedItem.Value
        Response.Redirect("frmrptvwr.aspx")
    End Sub
End Class
