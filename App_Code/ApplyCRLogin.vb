Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class ApplyCRLogin
    Public dbName As String

    Public serverName As String

    Public userID As String

    Public passWord As String

    Public Sub ApplyInfo(ByRef _oRpt As CrystalDecisions.CrystalReports.Engine.ReportDocument)
        Dim oCRDb As CrystalDecisions.CrystalReports.Engine.Database = _oRpt.Database
        Dim oCRTables As CrystalDecisions.CrystalReports.Engine.Tables = oCRDb.Tables
        ' CrystalDecisions.CrystalReports.Engine.Table oCRTable; 
        Dim oCRTableLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim oCRConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo()
        oCRConnectionInfo.DatabaseName = dbName
        oCRConnectionInfo.ServerName = serverName
        oCRConnectionInfo.UserID = userID
        oCRConnectionInfo.Password = passWord
        For Each oCRTable As CrystalDecisions.CrystalReports.Engine.Table In oCRTables
            oCRTableLogonInfo = oCRTable.LogOnInfo
            oCRTableLogonInfo.ConnectionInfo = oCRConnectionInfo
            oCRTable.ApplyLogOnInfo(oCRTableLogonInfo)
            'oCRTable.Location = (oCRConnectionInfo.DatabaseName + (".dbo." + oCRTable.Location.Substring((oCRTable.Location.LastIndexOf(".") + 1)))); 
            'crTable.Location = "ims" + "." + crTable.Location.Substring(crTable.Location.LastIndexOf(".") + 1); 

            oCRTable.Location = oCRTable.Location.Substring((oCRTable.Location.LastIndexOf(".") + 1))
        Next
        'Set the sections collection with report sections 
        Dim crSections As Sections = _oRpt.ReportDefinition.Sections

        'Loop through each section and find all the report objects 
        'Loop through all the report objects to find all subreport objects, then set the 
        'logoninfo to the subreport 
        For Each crSection As Section In crSections
            Dim crReportObjects As ReportObjects = crSection.ReportObjects
            For Each crReportObject As ReportObject In crReportObjects
                If crReportObject.Kind = ReportObjectKind.SubreportObject Then
                    'If you find a subreport, typecast the reportobject to a subreport object 
                    Dim crSubreportObject As SubreportObject = DirectCast(crReportObject, SubreportObject)

                    'Open the subreport 
                    Dim subRepDoc As ReportDocument = crSubreportObject.OpenSubreport(crSubreportObject.SubreportName)

                    oCRDb = subRepDoc.Database
                    oCRTables = oCRDb.Tables

                    'Loop through each table and set the connection info 
                    'Pass the connection info to the logoninfo object then apply the 
                    'logoninfo to the subreport 

                    For Each crTable As CrystalDecisions.CrystalReports.Engine.Table In oCRTables
                        Dim crLogOnInfo As TableLogOnInfo = crTable.LogOnInfo
                        crLogOnInfo.ConnectionInfo = oCRConnectionInfo
                        crTable.ApplyLogOnInfo(crLogOnInfo)
                        crTable.Location = crTable.Location.Substring(crTable.Location.LastIndexOf(".") + 1)
                    Next
                End If
            Next
        Next
    End Sub

End Class
