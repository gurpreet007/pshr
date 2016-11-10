
Partial Class frmrptpenslip
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Len(Session("EmpId")) = 0 Or Len(Session("fyr")) = 0 Then
            Response.Redirect("login.aspx")
        End If
        If Not IsPostBack Then
            Dim sql As String
            sql = ""

            Dim fy As String
            Dim empid As String

            fy = Session("fyr")
            empid = Session("EmpId")

            Me.CrystalReportSource1.Report.FileName = "rptpenslip.rpt"

            sql = "select t.*, pension.get_bank(t.bcode) as bnkname, salary.get_org(userid) as org,salary.get_org_name(userid) as orgname from tmpen" & fy & " t where empid = " & empid & ""

            Dim oracn As New OraDBconnection
            Dim ds As New Data.DataSet
            oracn.FillData2(sql, ds)
            Me.CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables(0))
            CrystalReportSource1.DataBind()
            ds.Clear()
            ds.Dispose()
            'CrystalReportViewer1.ReportSource = CrystalReportSource1
            'CrystalReportViewer1.DataBind()

            'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.ServerName = "pshr"
            'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.DatabaseName = "pshr"
            'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.UserID = "pension"
            'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.Password = "sivareddy10"
        End If
    End Sub
End Class
