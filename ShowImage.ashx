<%@ WebHandler Language="VB" Class="Handler" %>

Imports System
Imports System.Web
Imports System.IO

Public Class Handler : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim oracn As New OraDBconnection
        Dim eid As String
        Dim a() As String
        If context.Request.QueryString("eid") IsNot Nothing Then
            eid = context.Request.QueryString("eid")
            a = eid.Split("-")
        Else
            Throw New ArgumentException("No parameter specified")
        End If

        context.Response.ContentType = "image/jpeg"
        Dim strm As Stream
        
        If a.Length = 1 Then
            strm = oracn.getimage(eid)
        Else
            strm = oracn.getimage1(a(0), a(1))
        End If
        
        Dim buffer As Byte() = New Byte(4095) {}
        If strm Is Nothing Then
            Return
        End If
        Dim byteSeq As Integer = strm.Read(buffer, 0, 4096)
        While byteSeq > 0
            context.Response.OutputStream.Write(buffer, 0, byteSeq)
            byteSeq = strm.Read(buffer, 0, 4096)
        End While
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class