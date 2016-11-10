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
Namespace pshr
    Partial Class Reports
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then

                Dim vDr As OdbcDataReader

                Dim c1 As Integer

                vDr = GetDataReader("select count(*) from gpf.gpfinfo where gpfno = '" & Session("EmpGpf") & "'")
                vDr.Read()
                c1 = vDr.Item(0)
                objClose(vDr)
                If c1 = 0 Then
                    Response.Redirect("GPFErr.aspx")
                Else
                    Dim arylist As New ArrayList()
                    Dim selformula As [String] = String.Empty
                    Dim oStream As MemoryStream = Nothing
                    Dim rptFile As String = String.Empty
                    Dim fileName As String = String.Empty
                    Dim doc As New ReportDocument()
                    'rptFile = Session("RPTFILE").ToString()
                    rptFile = "RptGpft.rpt"
                    'selformula = Session("SELFORMULA").ToString()
                    selformula = "{GPFINFO.GPFNO}='" + Session("EmpGpf") + "'"
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
                    ' doc.SetParameterValue("reporthead", desg.ToString) 
                    doc.RecordSelectionFormula = selformula
                    'doc.SaveAs("C:\\abc1.pdf", False)
                    Response.Clear()
                    Response.Buffer = True
                    oStream = DirectCast((doc.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat)), MemoryStream)
                    Response.ContentType = "application/pdf"
                    Response.BinaryWrite(oStream.ToArray())
                    Response.[End]()
                End If
            End If
        End Sub
    End Class
End Namespace
