
Partial Class frmFindEmp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        tfName.Text = ""
        tdob.Text = ""
    End Sub

    Protected Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        lMsg.Text = ""
        dgEmpDet.DataSource = Nothing
        dgEmpDet.DataBind()
        tfName.Text = Trim(tfName.Text)
        tdob.Text = Trim(tdob.Text)
        If Not Regex.IsMatch(tfName.Text, "^[0-9]*$") Then
            lMsg.Text = "Invalid fileno no"
            Exit Sub
        End If

        If tfName.Text = "" And tdob.Text = "" Then
            lMsg.Text = "Please enter file no or dob"
            Exit Sub
        End If
        If tdob.Text <> "" Then
            If Not IsDate(tdob.Text) Then
                'tdob.Text = ""
                lMsg.Text = "Please enter dob in proper format"
                Exit Sub
            End If
        End If
        Dim OraCn As New OraDBconnection
        Dim ds As New DataSet
        Dim sql As String
        sql = ""
        sql = "select d.fileno,d.empid, firstname || ' ' || middlename || ' ' || lastname emp_name, fathername father_name, desgtext designation, locname location from pshr.empperso a, pshr.mast_desg b, pshr.mast_loc c, (select empid,fileno from pension.tmpen" & Now.Year.ToString() & Now.Month.ToString().PadLeft(2, "0") & " where fileno like '%" & tfName.Text & "%') d where a.cdesgcode=b.desgcode and a.cloccode=c.loccode and d.empid = a.empid "
        If tfName.Text <> "" Then sql = sql & " and d.fileno like '%" & tfName.Text & "%' "
        If tdob.Text <> "" Then sql = sql & " and dob = '" & tdob.Text & "'"
        sql = sql & " order by fileno,empid"
        OraCn.FillData1(sql, ds)
        dgEmpDet.DataSource = ds
        dgEmpDet.DataBind()
        lMsg.Text = "Total Records Found : " & ds.Tables(0).Rows.Count
        ds.Clear()
        ds.Dispose()
    End Sub
End Class
