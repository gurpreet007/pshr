''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Code Module  : resetpwd
'Project      : PSHR Search Engine
'Author       : Santosh Kumar
'Role         : Project Leader
'Designation  : Project Leader
'Department   : Software Engineering
'Created Date : 02-Jan-2007
'Modified by  : 
'Modified on  : 

'Description  : Change pwd for forget pwd options
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data.Odbc


Namespace pshr


Partial Class resetpwd
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
				If Session("AppId") <> "C" Then
						Response.Redirect("mailpwd.aspx")
				End If
		End Sub

		Private Sub bGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGo.Click
				If ((Len(tPwdOne.Text.Trim) < 5 Or Len(tPwdTwo.Text.Trim) < 5) AND (Len(tPwdOne.Text.Trim) > 25 Or Len(tPwdTwo.Text.Trim) > 25)) Then
						lMsg.Text = "No field can be less than 5 alphabets and not more than 25 or blank"
						lMsg.Visible = True
						Exit Sub
				ElseIf tPwdOne.Text.Trim <> tPwdTwo.Text.Trim Then
						lMsg.Text = "Password verification failed"
						lMsg.Visible = True
						Exit Sub
				End If
				lMsg.Text = "No field can be less than 5 alphabets and not more than 25 or blank"
				Dim vDr As OdbcDataReader
				Try
						vDr = GetDataReader("select empid, firstname from netlogin a, empperso b where a.eid=b.empid and upper(eid)=upper('" & Session("EmpId") & "')")
						vDr.Read()
						If vDr.HasRows = False Then
								lMsg.Text = "Login doesn't already exist!"
								lMsg.Visible = True
						Else
								ExecuteSql("update netlogin set pwd='" & Replace(tPwdOne.Text, "'", "''") & "' where eid='" & Session("EmpId") & "'")
								Session("AppId") = "L"								'Login mode
								Session("MsgPL") = "You are authenticated successfully and your password is changed"
                    Response.Redirect("empdetail.aspx")
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
