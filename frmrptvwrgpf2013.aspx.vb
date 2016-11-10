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

Partial Class frmrptvwrgpf2013
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

        If Not IsPostBack Then
            Dim fy As String
            fy = ""
            fy = Session("fyr")
            rptFile = "RptGpft2013.rpt"
            sql = "select * from GPF" & fy & " s where gpfno = '" & Session("EmpGpf") & "'"
            oraCn.FillData(sql, ds)
            Me.CrystalReportSource1.Report.FileName = Server.MapPath(rptFile)
            Me.CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
            Me.CrystalReportSource1.DataBind()
            Me.CrystalReportViewer1.ReportSource = CrystalReportSource1
            Me.CrystalReportViewer1.DataBind()
            Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.ServerName = "pshr1"
            Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.UserID = "gpf"
            Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.Password = "pspcl"
        End If
    End Sub
End Class
