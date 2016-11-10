''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Code Module  : welcome
'Project      : PSHR Search Engine
'Author       : Santosh Kumar
'Role         : Project Leader
'Designation  : Project Leader
'Department   : Software Engineering
'Created Date : 29-Nov-2007
'Modified by  : 
'Modified on  : 

'Description  : Home page after logged in
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Namespace pshr

Partial Class welcome
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
				If Trim(Session("MsgPL")) <> "" Then
						lMsg.Visible = True
				Else
						lMsg.Visible = False
            End If
            lMsg.Text = Session("MsgPL")
            Session("MsgPL") = ""
            If Not IsPostBack Then
                If Session("EmpSt") = 10 And Session("EmpId") <> "101765" Then
                    LinkButton1.Enabled = False
                End If
            End If
		End Sub

		Private Sub bLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bLogout.Click
				Session.Abandon()
				Response.Redirect("login.aspx")
		End Sub

        Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
            Response.Redirect("frmPenSlip.aspx")
        End Sub
    End Class

End Namespace
