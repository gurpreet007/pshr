Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.Odbc
Partial Class RptSalSlip
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If IsDBNull(Session("EmpPan")) And IsDBNull(Session("EmpGpf")) Then
                Label2.Visible = True
                Button2.Visible = True
                Panel1.Visible = False
                Exit Sub
            End If


            If IsDBNull(Session("EmpPan")) Then
                Session("EmpPan") = "---------------"
            End If

            If IsDBNull(Session("EmpGpf")) Then
                Session("EmpGpf") = "---------------"
            End If

            Dim oraCn As New OraDBconnection
            Dim ds As New DataSet
            Dim ds1 As New DataSet
            Dim i As Integer
            Dim sql, sql1 As String
            Dim s As String
            sql = "Select tname from tab where tname like 'SALARY%' and tname != 'SALARY012010' order by tname"
            oraCn.FillData(sql, ds)
            sql1 = ""
            s = ""
            For i = 0 To ds.Tables(0).Rows.Count - 1
                sql1 = "select count(*) from " & ds.Tables(0).Rows(i)(0) & " where pan = '" & Session("EmpPan") & "' or gpfa = '" & Session("EmpGpf") & "'"
                oraCn.FillData(sql1, ds1)
                If ds1.Tables(0).Rows.Count > 0 Then
                    s = ds.Tables(0).Rows(i)(0).ToString()
                    drpSalMth.Items.Add(Mid$(s, 11, 2) & "-" & Mid$(s, 7, 4))
                End If
            Next i

            If drpSalMth.Items.Count = 0 Then
                Label2.Visible = True
                Button2.Visible = True
                Panel1.Visible = False
            End If
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Redirect("empdetail.aspx")
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim arylist As New ArrayList()
        Dim selformula As [String] = String.Empty
        Dim oStream As MemoryStream = Nothing
        Dim rptFile As String = String.Empty
        Dim fileName As String = String.Empty
        Dim sql As String
        Dim doc As New ReportDocument()
        rptFile = "RptSalarySlip.rpt"
        sql = "select * from SALARY" & Mid$(drpSalMth.SelectedItem.Text, 4, 4) & Mid$(drpSalMth.SelectedItem.Text, 1, 2) & " where pan = '" & Session("EmpPan") & "' or gpfa = '" & Session("EmpGpf") & "'"
        fileName = Server.MapPath(rptFile)
        Dim objn As New ApplyCRLogin()
        doc.Load(fileName)
        doc.Refresh()
        'arylist = objclscommon.ConnectValues()
        objn.dbName = "pshr"
        objn.serverName = "pshr"
        objn.userID = "gpf"
        objn.passWord = "pseb"
        objn.ApplyInfo(doc)
        Dim oraCn As New OraDBconnection
        Dim ds As New Data.DataSet
        oraCn.FillData(sql, ds)
        ' doc.SetParameterValue("reporthead", desg.ToString) 
        doc.SetDataSource(ds.Tables(0))
        doc.RecordSelectionFormula = selformula
        'doc.SaveAs("C:\\abc1.pdf", False)
        Response.Clear()
        Response.Buffer = True

        oStream = DirectCast((doc.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat)), MemoryStream)
        Response.ContentType = "application/pdf"
        Response.BinaryWrite(oStream.ToArray())
        Response.[End]()


    End Sub
End Class
