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
Partial Class frmrptvwrsal1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim arylist As New ArrayList()
            Dim selformula As [String] = String.Empty
            ' Dim oStream As MemoryStream = Nothing
            Dim rptFile As String = String.Empty
            Dim fileName As String = String.Empty
            Dim sql As String
            'Dim doc As New ReportDocument()
            Dim oraCn As New OraDBconnection
            Dim ds As New Data.DataSet

            sql = ""
            'gettname()
            Dim objn As New ApplyCRLogin()
            'objn.dbName = "pshr"
            'objn.serverName = "pshr"
            'objn.userID = "pshr"
            'objn.passWord = "CuteMines2004"
            Dim fy As String
            fy = ""
            fy = Session("fyr")
            rptFile = "RptSalarySlip3.rpt"
            sql = "select s.*,GET_BNK(S.BCODE),nvl((select msgdis from salmsgmonthly where mthyr = s.yr || s.mth and userid = s.userid), ' ') as msgd,salary.get_org_name(userid) as orgname from salary" & fy & "T s  where s.empid in (" & Session("EmpIdtot") & ") and netpay >= 0"
            oraCn.FillData1(sql, ds)
            Me.CrystalReportSource1.Report.FileName = Server.MapPath(rptFile)
            Me.CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
            ds.Clear()
            ds.Dispose()
            'Me.CrystalReportSource1.DataBind()
            'Me.CrystalReportViewer1.ReportSource = CrystalReportSource1
            'Me.CrystalReportViewer1.DataBind()
            'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.ServerName = "pshr"
            ''Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.DatabaseName = "pshr"
            'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.UserID = "salary"
            'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.Password = "10937pspcl"
        End If
    End Sub
End Class
