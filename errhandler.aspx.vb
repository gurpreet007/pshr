''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Code Module  : errhandler
'Project      : PSHR Search Engine
'Author       : Santosh Kumar
'Role         : Project Leader
'Designation  : Project Leader
'Department   : Software Engineering
'Created Date : 16-Jan-2007
'Modified by  : 
'Modified on  : 

'Description  : Common error handler page
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Web.Mail


Namespace pshr


Partial Class errhandler
		Inherits System.Web.UI.Page
		Dim vErr As String

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
				If Session("IsErr") Is Nothing Then Response.Redirect("login.aspx")
				vErr = Session("IsErr").ToString()
				If Trim(ConfigurationSettings.AppSettings("_ErrMailId")) <> "" Then
						If SendMail(ConfigurationSettings.AppSettings("_ErrMailId"), vErr) = True Then
								vErr = vErr & "<br><br>An alert to the administrator is sent</br></br>"
						Else
								vErr = vErr & "<br><br>An alert to the administrator could not be sent</br></br>"
						End If
				End If
				lMsg.Text = vErr
				Session.Abandon()
				'Session("IsErr") = Nothing
		End Sub

End Class

End Namespace
