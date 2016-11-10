Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Data.OracleClient
Imports System
Imports System.IO

Public Class OraDBconnection
    Dim connString As New String("user id=pshr;password=123;data source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=xe)))")
    Dim connString1 As New String("user id=pshr;password=123;data source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST= 127.0.0.1)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=xe)))")
    Dim connString2 As New String("user id=pshr;password=123;data source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST= 127.0.0.1)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=xe)))")
    Dim connString_pshr As New String("user id=pshr;password=123;data source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST= 127.0.0.1)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=xe)))")
    'Dim connString As New String("user id=salary;password=pseb;data source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=your-a9279112e3)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=pshr1)))")


    'Public Sub OraDBConOpen(ByRef conn As Oracle.DataAccess.Client.OracleConnection)
    Public Sub OraDBConOpen(ByRef conn As System.Data.OracleClient.OracleConnection)
        'If conn.State = ConnectionState.Open Then
        ' conn.Close()
        ' End If
        'conn = New Oracle.DataAccess.Client.OracleConnection
        conn.ConnectionString = connString
        On Error GoTo er
        conn.Open()
er:
        If Err.Number > 0 Then
            '     MsgBox(Err.Description)
            Exit Sub
        End If

    End Sub
    
    Public Sub OraDBConOpen1(ByRef conn As System.Data.OracleClient.OracleConnection)
        'Public Sub OraDBConOpen1(ByRef conn As Oracle.DataAccess.Client.OracleConnection)
        'If conn.State = ConnectionState.Open Then
        ' conn.Close()
        ' End If
        'conn = New Oracle.DataAccess.Client.OracleConnection
        conn.ConnectionString = connString1
        On Error GoTo er
        conn.Open()
er:
        If Err.Number > 0 Then
            '     MsgBox(Err.Description)
            Exit Sub
        End If
    End Sub

    Public Sub OraDBConOpen2(ByRef conn As System.Data.OracleClient.OracleConnection)
        'Public Sub OraDBConOpen1(ByRef conn As Oracle.DataAccess.Client.OracleConnection)
        'If conn.State = ConnectionState.Open Then
        ' conn.Close()
        ' End If
        'conn = New Oracle.DataAccess.Client.OracleConnection
        conn.ConnectionString = connString2
        On Error GoTo er
        conn.Open()
er:
        If Err.Number > 0 Then
            '     MsgBox(Err.Description)
            Exit Sub
        End If
    End Sub

    Public Sub conclose(ByRef conn As System.Data.OracleClient.OracleConnection)
        'Public Sub conclose(ByRef conn As Oracle.DataAccess.Client.OracleConnection)
        If conn.State = ConnectionState.Open Then
            conn.Close()
            conn.Dispose()
        End If
    End Sub
    Public Sub FillData(ByVal sqlString As String, ByRef dataset As System.Data.DataSet)
        Dim cmd As New System.Data.OracleClient.OracleCommand
        Dim con As New System.Data.OracleClient.OracleConnection
        OraDBConOpen(con)
        Dim dbadapter As New System.Data.OracleClient.OracleDataAdapter(sqlString, con)
        dbadapter.Fill(dataset)
        cmd.Dispose()
        conclose(con)
    End Sub

    Public Function GetData(ByVal sql As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As New System.Data.OracleClient.OracleCommand
        Dim con As New System.Data.OracleClient.OracleConnection
        OraDBConOpen(con)
        Dim dbadapter As New System.Data.OracleClient.OracleDataAdapter(sql, con)
        dbadapter.Fill(ds)
        cmd.Dispose()
        conclose(con)
        Return ds
    End Function

    Public Function GetScalar(ByVal sql As String) As String
        Dim ds As New DataSet
        Dim cmd As New System.Data.OracleClient.OracleCommand
        Dim con As New System.Data.OracleClient.OracleConnection
        Dim retStr As String

        OraDBConOpen(con)
        Dim dbadapter As New System.Data.OracleClient.OracleDataAdapter(sql, con)
        dbadapter.Fill(ds)
        cmd.Dispose()
        conclose(con)
        retStr = ds.Tables(0).Rows(0)(0).ToString()
        ds.Clear()
        ds.Dispose()
        Return retStr
    End Function

    Public Sub FillData1(ByVal sqlString As String, ByRef dataset As System.Data.DataSet)
        Dim cmd As New System.Data.OracleClient.OracleCommand
        Dim con As New System.Data.OracleClient.OracleConnection
        OraDBConOpen1(con)
        Dim dbadapter As New System.Data.OracleClient.OracleDataAdapter(sqlString, con)
        dbadapter.Fill(dataset)
        cmd.Dispose()
        conclose(con)
    End Sub

    Public Sub FillData2(ByVal sqlString As String, ByRef dataset As System.Data.DataSet)
        Dim cmd As New System.Data.OracleClient.OracleCommand
        Dim con As New System.Data.OracleClient.OracleConnection
        OraDBConOpen2(con)
        Dim dbadapter As New System.Data.OracleClient.OracleDataAdapter(sqlString, con)
        dbadapter.Fill(dataset)
        cmd.Dispose()
        conclose(con)
    End Sub

    Public Sub FillDrp(ByRef ds As System.Data.DataSet, ByRef drp As DropDownList)
        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1
            drp.Items.Add(New ListItem(ds.Tables(0).Rows(i)(0).ToString(), ds.Tables(0).Rows(i)(1).ToString()))
        Next i
    End Sub

    Public Sub ExecQry(ByVal sql As String)
        Dim cmd As New System.Data.OracleClient.OracleCommand
        Dim con As New System.Data.OracleClient.OracleConnection
        OraDBConOpen(con)
        cmd.CommandText = sql
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        cmd.Dispose()
        conclose(con)
    End Sub

    Public Function getimage(ByVal eid As String) As Stream
        Dim con As New System.Data.OracleClient.OracleConnection
        OraDBConOpen(con)
        Dim sql As String = "select photo2 from img_pshr.img where empid = '" & eid & "'"
        Dim cmd As New System.Data.OracleClient.OracleCommand
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = sql
        'cmd.Parameters.Add("@ID", rollno)
        Dim img As Object = cmd.ExecuteScalar()
        Try
            Return New MemoryStream(DirectCast(img, Byte()))
        Catch
            Return Nothing
        Finally
            con.Close()
        End Try
    End Function

    Public Function getimage1(ByVal eid As String, ByVal fid As String) As Stream
        Dim con As New System.Data.OracleClient.OracleConnection
        OraDBConOpen(con)
        Dim sql As String = "select photo2 from img_pshr.familyimg where empid = '" & eid & "' and familyid = '" & fid & "'"
        Dim cmd As New System.Data.OracleClient.OracleCommand
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = sql
        'cmd.Parameters.Add("@ID", rollno)
        Dim img As Object = cmd.ExecuteScalar()
        Try
            Return New MemoryStream(DirectCast(img, Byte()))
        Catch
            Return Nothing
        Finally
            con.Close()
        End Try
    End Function

    Public Sub fillgrid(ByRef gv As System.Web.UI.WebControls.GridView, ByRef ds As System.Data.DataSet)
        gv.DataSource = ds
        gv.DataBind()
    End Sub

    Public Sub GetConnString(ByRef con As String)
        con = connString
    End Sub
    Public Sub ExecUpdateRec(ByVal oldid As String, ByVal newid As String)
        Dim con As New OracleConnection
        Dim cmd As New OracleCommand("UPDATEREC")

        cmd.CommandType = CommandType.StoredProcedure
        'OraDBConOpen(con)
        con.ConnectionString = connString_pshr
        con.Open()
        cmd.Connection = con
        cmd.Parameters.Add(New OracleParameter("m_empid", oldid))
        cmd.Parameters.Add(New OracleParameter("retain_empid", newid))
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub

    'Private Sub OraDBConOpen(ByVal con As OracleConnection)
    '    Throw New NotImplementedException
    'End Sub

    'Private Sub OraDBConOpen1(ByVal con As OracleConnection)
    '    Throw New NotImplementedException
    'End Sub

    'Private Sub conclose(ByVal con As OracleConnection)
    '    Throw New NotImplementedException
    'End Sub

End Class

