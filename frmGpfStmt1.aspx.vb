
Partial Class frmGpfStmt1
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
            getGPFPayments()
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

    Private Sub getGPFPayments()
        GridView2.Visible = False
        Dim c1 As Integer
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        sql = "select to_char(PAID,'dd-Mon-yyyy') as PaidOn,userid as Loc,NAMEP as PaidTo,RELP as Relation,ACNOP as AccountNo,BNKP as Bank,GPFREF as RefAmt,GPFNREF as NonRefAmt,REASON from salary.gpfdeb where empid = " & Session("EmpId") & " and paid is not null order by paid"
        oraCn.FillData1(sql, ds)
        GridView2.DataSource = ds
        GridView2.DataBind()
        c1 = ds.Tables(0).Rows.Count
        ds.Clear()
        ds.Dispose()
        If c1 > 0 Then
            GridView2.Visible = True
        End If
    End Sub

    Private Sub setDat()
        Label2.Visible = False
        drpGpfYr.Visible = False
        Button1.Visible = False
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Session("rptNm") = "rptgpf"
        Session("fyr") = drpGpfYr.SelectedItem.Value
        If drpGpfYr.SelectedItem.Value < "201011" Then
            Response.Redirect("frmrptvwr.aspx")
        ElseIf drpGpfYr.SelectedItem.Value < "201314" Then
            Response.Redirect("frmrptvwrgpf2010.aspx")
        Else
            Response.Redirect("frmrptvwrgpf2013.aspx")
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Response.Redirect("empdetail.aspx")
    End Sub

    Protected Sub bLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bLogout.Click
        Session.Abandon()
        Response.Redirect("login.aspx")
    End Sub
End Class
