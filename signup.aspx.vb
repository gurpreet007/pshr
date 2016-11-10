''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Code Module  : signup
'Project      : PSHR Search Engine
'Author       : Santosh Kumar
'Role         : Project Leader
'Designation  : Project Leader
'Department   : Software Engineering
'Created Date : 27-Nov-2007
'Modified by  : 
'Modified on  : 

'Description  : Create user screen
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data.Odbc


Namespace pshr


Partial Class signup
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

		End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session("AppId") <> "I" Then
            Response.Redirect("confirm.aspx")
        End If
        If Not IsPostBack Then
            AddList(ddQns)
        End If
    End Sub

    Private Sub bGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGo.Click
        If Len(tPwdOne.Text.Trim) < 5 Or Len(tPwdTwo.Text.Trim) < 5 Or Len(ddQns.SelectedValue.Trim) < 5 Or Len(tAns.Text.Trim) < 5 Then
            lMsg.Text = "No field can be less than 5 alphabets or blank"
            lMsg.Visible = True
            Exit Sub
        ElseIf tPwdOne.Text.Trim <> tPwdTwo.Text.Trim Then
            lMsg.Text = "Password verification failed"
            lMsg.Visible = True
            Exit Sub
        End If
        Dim vDr As OdbcDataReader
        Try
                vDr = GetDataReader("select empid, firstname,b.gpfno from netlogin a, empperso b where a.eid=b.empid and upper(eid)=upper('" & Session("EmpId") & "')")
                vDr.Read()
                Dim s As Boolean
                s = vDr.HasRows
                vDr.Close()
                If s = False Then
                    ExecuteSql("insert into netlogin values('" & Replace(Session("EmpId"), "'", "''") & "','" & Replace(tPwdOne.Text, "'", "''") & "','" & Replace(ddQns.SelectedIndex, "'", "''") & "','" & Replace(tAns.Text, "'", "''") & "')")
                    'Response.Write(SayThis("Login created\nClick here to continue"))
                    vDr = GetDataReader("select empid, firstname,b.gpfno, b.recstatus from netlogin a, empperso b where a.eid=b.empid and upper(eid)=upper('" & Session("EmpId") & "')")
                    vDr.Read()
                    Session("EmpNm") = vDr.Item(1) & ","
                    Session("EmpGpf") = vDr.Item(2)
                    Session("EmpSt") = vDr.Item(3)
                    Session("AppId") = "L"               'Login mode
                    Session("MsgPL") = "Congrates! your login information is accepted successfully"
                    vDr.Close()
                    Response.Redirect("empdetail.aspx")
                Else
                    lMsg.Text = "Login already exists!"
                    lMsg.Visible = True
                End If
            Catch Ex As Exception
                If Ex.Message <> "Thread was being aborted." Then
                    Session("IsErr") = Ex
                End If
            Finally
                If Session("IsErr").ToString <> "" Then _
                Response.Redirect("ErrHandler.aspx")
            End Try

    End Sub

		Private Sub bLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bLogout.Click
				Session.Abandon()
				Response.Redirect("login.aspx")
		End Sub
End Class

End Namespace
