Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.Odbc
Partial Class frmrptvwr
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim arylist As New ArrayList()
        Dim selformula As [String] = String.Empty
        Dim oStream As MemoryStream = Nothing
        Dim rptFile As String = String.Empty
        Dim fileName As String = String.Empty
        Dim sql As String
        Dim doc As New ReportDocument()
        Dim oraCn As New OraDBconnection
        Dim ds As New Data.DataSet

        sql = ""
        'gettname()

        'objn.dbName = "pshr"
        'objn.serverName = "pshr"
        'objn.userID = "pshr"
        'objn.passWord = "CuteMines2004"
        If Not IsPostBack Then
            Dim fy As String
            fy = ""
            Session("rptNm") = "rptgpf"
            fy = Session("fyr")
            rptFile = "RptGpft.rpt"
            sql = "select * from GPF" & fy & " s where gpfno = '" & Session("EmpGpf") & "'"
            oraCn.FillData(sql, ds)
            Me.CrystalReportSource1.Report.FileName = Server.MapPath(rptFile)
            Me.CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
            Me.CrystalReportSource1.DataBind()
            Me.CrystalReportViewer1.ReportSource = CrystalReportSource1
            Me.CrystalReportViewer1.DataBind()
            Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.ServerName = "pshr"
            'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.DatabaseName = "pshr"
            Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.UserID = "gpf"
            Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.Password = "pseb"

            'objn.dbName = "pshr"
            'objn.serverName = "pshr"
            'objn.userID = "gpf"
            'objn.passWord = "pseb"
            'ElseIf Session("rptNm") = "rptsalslip" Then
            '    fy = Session("fyr")
            '    rptFile = "RptSalarySlip.rpt"
            '    sql = "select s.*,b.* from salary" & fy & " s, mast_bank b where b.bnkcode = s.bcode and s.empid = " & Session("EmpId") & ""
            '    oraCn.FillData1(sql, ds)

            '    Me.CrystalReportSource1.Report.FileName = Server.MapPath(rptFile)

            '    Me.CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
            '    Me.CrystalReportSource1.DataBind()
            '    Me.CrystalReportViewer1.ReportSource = CrystalReportSource1
            '    Me.CrystalReportViewer1.DataBind()
            '    Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.ServerName = "pshr"
            '    'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.DatabaseName = "pshr"
            '    Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.UserID = "salary"
            '    Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.Password = "10937pspcl"
            'objn.dbName = "pshr"
            'objn.serverName = "pshr"
            'objn.userID = "Salary"
            'objn.passWord = "10937pspcl"

        End If
        'Dim objn As New ApplyCRLogin()
        'objn.dbName = "pshr"
        'objn.serverName = "pshr"
        'objn.userID = "gpf"
        'objn.passWord = "pseb"
        'fileName = Server.MapPath(rptFile)
        'doc.Load(fileName)
        'doc.Refresh()
        'arylist = objclscommon.ConnectValues()

        'objn.ApplyInfo(doc)

        'doc.SetParameterValue("reporthead", desg.ToString)
        'doc.SetDataSource(ds.Tables(0))
        'doc.RecordSelectionFormula = selformula
        'doc.SaveAs("C:\\abc1.pdf", False)
        'Response.Clear()
        'Response.Buffer = True
        'If Session("Oexl") = 0 Then
        '    oStream = DirectCast((doc.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat)), MemoryStream)
        '    oStream = DirectCast(doc.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat), MemoryStream)
        '    Response.ContentType = "application/pdf"
        'Else
        '    oStream = DirectCast((doc.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.Excel)), MemoryStream)
        '    Response.ContentType = "application/vnd.ms-excel"

        'End If
        'Response.BinaryWrite(oStream.ToArray())
        'doc.Close()
        'doc.Dispose()
        'ds.Clear()
        'ds.Dispose()
        'Response.[End]()
    End Sub
End Class
