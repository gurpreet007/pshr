''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Code Module  : changepwd
'Project      : PSHR Search Engine
'Author       : Santosh Kumar
'Role         : Project Leader
'Designation  : Project Leader
'Department   : Software Engineering
'Created Date : 01-Dec-2007
'Modified by  : 
'Modified on  : 

'Description  : Change Password screen for logged in users
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data.Odbc


Namespace pshr


Partial Class changepwd
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
				If Session("AppId") <> "L" Then
						Response.Redirect("login.aspx")
				End If
		End Sub

		Private Sub bGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGo.Click
				If Len(tPwdOne.Text.Trim) < 5 Or Len(tPwdTwo.Text.Trim) < 5 Then
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
						vDr = GetDataReader("select * from netlogin where upper(eid)=upper('" & Session("EmpId") & "') and pwd='" & Replace(tPwdOld.Text, "'", "''") & "'")
						vDr.Read()
						If vDr.HasRows = False Then
								lMsg.Text = "Old password doesn't match"
								lMsg.Visible = True
								Exit Sub
						Else
								ExecuteSql("update netlogin set pwd='" & Replace(tPwdOne.Text, "'", "''") & "' where eid='" & Session("EmpId") & "'")
								Session("AppId") = "L"				'Login mode
								Session("MsgPL") = "Your password is successfully changed"
                    Response.Redirect("empdetail.aspx")
						End If

				Catch Ex As Exception
						If Ex.Message <> "Thread was being aborted." Then
								Session("IsErr") = Ex
						End If
				Finally
                objClose(vDr)

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
