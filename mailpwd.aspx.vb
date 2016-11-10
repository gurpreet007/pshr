''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Code Module  : mailpwd
'Project      : PSHR Search Engine
'Author       : Santosh Kumar
'Role         : Project Leader
'Designation  : Project Leader
'Department   : Software Engineering
'Created Date : 27-Nov-2007
'Modified by  : 
'Modified on  : 

'Description  : This is user authentication screen for change pwd 
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data.Odbc


Namespace pshr


Partial Class mailpwd
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
        If Not IsPostBack Then
            AddList(ddQns)
        End If
    End Sub

    Private Sub bGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGo.Click
        Dim vDr As OdbcDataReader
        Try
						'vDr = GetDataReader("select empid, firstname from netlogin a, empperso b where a.eid=b.empid and upper(eid)=upper('" & tUsr.Text & "') and upper(panno)=upper('" & tPan.Text & "') and qns='" & ddQns.SelectedIndex & "' and ans='" & tAns.Text & "'")
						vDr = GetDataReader("select empid, firstname from netlogin a, empperso b where a.eid=b.empid and upper(eid)=upper('" & tUsr.Text & "') and qns='" & ddQns.SelectedIndex & "' and ans='" & tAns.Text & "'")
						vDr.Read()
            If vDr.HasRows = False Then
                lMsg.Text = "Authentication failed!"
                lMsg.Visible = True
            Else
                Session("AppId") = "C" 'Change mode
                Session("EmpId") = vDr.Item(0)
                Session("EmpNm") = vDr.Item(1)
								Response.Redirect("resetpwd.aspx")
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
