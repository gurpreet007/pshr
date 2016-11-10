''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Code Module  : confirm
'Project      : PSHR Search Engine
'Author       : Santosh Kumar
'Role         : Project Leader
'Designation  : Project Leader
'Department   : Software Engineering
'Created Date : 29-Nov-2007
'Modified by  : 
'Modified on  : 

'Description  : Employee confirmation before allowing to create user
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data.Odbc


Namespace pshr


Partial Class confirm
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
        Session("AppId") = ""
        Session("EmpId") = ""
        Session("EmpNm") = ""
				If Session("AppId") <> "L" Then
						'Response.Redirect("login.aspx")'No login is required
				End If
		End Sub

		Private Sub bGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGo.Click
				Dim vDr As OdbcDataReader
				Try
						'cheking existing user
						vDr = GetDataReader("select eid from netlogin where upper(eid)=upper('" & tEid.Text & "')")
						vDr.Read()
						If vDr.HasRows = True Then
								lMsg.Text = "Login already exists!"
								lMsg.Visible = True
								Exit Sub
						End If
						objClose(vDr)

						'authenticating
                vDr = GetDataReader("select empid, firstname from empperso where empid='" & tEid.Text & "' and to_char(dob,'dd-mm-yyyy')='" & tDob.Text & "'")
						'vDr = GetDataReader("select empid, firstname from empperso where empid='" & tEid.Text & "' and panno='" & tPan.Text & "'")
						'vDr = GetDataReader("select firstname from empperso where empid='" & tEid.Text & "' and to_char(dob,'yyyymmdd')=to_char(to_date('" & tDob.Text & "'),'yyyymmdd') and panno='" & tPan.Text & "'")
						vDr.Read()
						If vDr.HasRows = False Then
								lMsg.Text = "Authentication failed !"
								lMsg.Visible = True
						Else
								Session("AppId") = "I"				 'Initiation mode 
								Session("EmpId") = vDr.Item(0)
                    Session("EmpNm") = vDr.Item(1)
                    Response.Redirect("signup.aspx")
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
