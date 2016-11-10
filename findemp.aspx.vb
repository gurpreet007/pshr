''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Code Module  : findemp
'Project      : PSHR Search Engine
'Author       : Santosh Kumar
'Role         : Project Leader
'Designation  : Project Leader
'Department   : Software Engineering
'Created Date : 23-Nov-2007
'Modified by  : 
'Modified on  : 

'Description  : Search screen to accept criteria and list records
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Namespace pshr

Partial Class findemp
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
            'Page.RegisterHiddenField("__EVENTTARGET", "btnFind")
            If Len(Session("EmpId")) = 0 Then
                Response.Redirect("login.aspx")
            End If
            If Session("AppId") <> "L" Then
                Response.Redirect("login.aspx")
            End If
            If Not IsPostBack Then
                bindControl(ddDesignation, "select desgcode, desgtext from pshr.mast_desg order by desgtext")
                bindControl(ddLocation, "select loccode,locname from pshr.mast_loc  order by locname")
                '"select tname from tab where tname like '%LOC%'"
            End If
        End Sub

        Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
            Try
                'If Len(tfName.Text.Trim & tmName.Text.Trim & tlName.Text.Trim) < 3 Then
                If Len(tfName.Text.Trim & tmName.Text.Trim & tlName.Text.Trim & tPan.Text & ddLocation.SelectedValue.Trim & ddDesignation.SelectedValue.Trim) = 0 And drpBloodGrp.SelectedValue = "All" Then
                    'lMsg.Text = "Full name can not be less than 3 alphabets"
                    lMsg.Text = "All fields can not be empty"
                    lMsg.ForeColor = Color.Red
                    dgEmpDet.Visible = False
                    Exit Sub
                End If
                lMsg.ForeColor = Color.Black
                dgEmpDet.Visible = True
                bindControl(dgEmpDet, getQuery)
                If dgEmpDet.Items.Count > 1 Then
                    lMsg.Text = dgEmpDet.Items.Count.ToString & " Matches found"
                Else
                    lMsg.Text = dgEmpDet.Items.Count.ToString & " Match found"
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

        Private Function getQuery() As String
            Dim vQry As String = ""
            vQry = vQry & "select empid, firstname || ' ' || middlename || ' ' || lastname emp_name, fathername father_name, desgtext designation, locname as location "
            If drpBloodGrp.SelectedValue <> "All" Then
                vQry = vQry & ", (select max(phonecell) from pshr.empaddr where empid = a.empid) as ContactNo "
            End If
            vQry = vQry & " from pshr.empperso a, pshr.mast_desg b, pshr.mast_loc c where a.recstatus = 10 and a.cdesgcode=b.desgcode and a.cloccode=c.loccode "
            If tfName.Text <> "" Then vQry = vQry & "and upper(firstname) like upper('" & tfName.Text & "%') "
            If tmName.Text <> "" Then vQry = vQry & "and upper(middlename) like upper('" & tmName.Text & "%') "
            If tlName.Text <> "" Then vQry = vQry & "and upper(lastname) like upper('" & tlName.Text & "%') "
            If tPan.Text <> "" Then vQry = vQry & "and upper(panno) like upper('" & tPan.Text & "%') "
            If ddDesignation.SelectedValue <> "" Then vQry = vQry & "and cdesgcode like '" & ddDesignation.SelectedValue & "%' "
            If ddLocation.SelectedValue <> "" Then vQry = vQry & "and cloccode like '" & ddLocation.SelectedValue & "%' "
            If drpBloodGrp.SelectedValue <> "All" Then vQry = vQry & " and bloodgroup = '" & drpBloodGrp.SelectedValue & "' "

            vQry = vQry & " order by a.empid "
            Return vQry

            '"select empid, firstname, middlename, lastname, sex, panno, '<a href=''empdetail.aspx?empid='||empid||'''>Show</a>' Click from empperso where " & _
            '"upper(firstname) like upper('%" & tfName.Text & "%') " & _
            '"and upper(middlename) like upper('%" & tmName.Text & "%') " & _
            '"and upper(lastname) like upper('%" & tlName.Text & "%') " & _
            '"and upper(panno) like upper('%" & tPan.Text & "%') " & _
            '"and cdesgcode like '%" & ddDesignation.SelectedValue & "%' and cloccode like '%" & ddLocation.SelectedValue & "%'"
        End Function

		Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload

		End Sub

		Private Sub Page_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed

		End Sub

		Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
				'Response.Redirect("findemp.aspx")
				tfName.Text = ""
				tmName.Text = ""
				tlName.Text = ""
            tPan.Text = ""
            drpBloodGrp.SelectedIndex = 0
				ddDesignation.SelectedIndex = 0
            ddLocation.SelectedIndex = 0
            dgEmpDet.Visible = False
		End Sub

        Private Sub bLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bLogout.Click
            Session("EmpId") = ""
            Session.Abandon()
            Response.Redirect("login.aspx")
        End Sub
End Class

End Namespace
