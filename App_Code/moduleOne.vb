''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Code Module  : moduleOne
'Project      : PSHR Search Engine
'Author       : Santosh Kumar
'Role         : Project Leader
'Designation  : Project Leader
'Department   : Software Engineering
'Created Date : 23-Nov-2007
'Modified by  : 
'Modified on  : 

'Description  : This contains global methods required from all corners
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports System
Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Data.Odbc
Imports System.Web.Mail
Imports Microsoft.VisualBasic

Namespace pshr

Public Module moduleOne
		Public Function dbConnect(Optional ByVal UFlg As String = "D") As OdbcConnection

            Dim vCon As OdbcConnection
            Try
                Dim vStr As String = System.Configuration.ConfigurationManager.AppSettings("_OCon")
                If UFlg = "D" Then
                    'vStr = vStr & System.Configuration.ConfigurationManager.AppSettings("_UDat")
                    vStr = "dsn=dsn_xe;uid=pshr;pwd=123"
                Else
                    'vStr = vStr & System.Configuration.ConfigurationManager.AppSettings("_UImg")
                    'vStr = "dsn=img1;uid=img_pshr;pwd=pshr"
                    vStr = "dsn=dsn_xe;uid=pshr;pwd=123"
                End If
                vCon = New OdbcConnection(vStr)
                vCon.Open()
            Catch ex As Exception
                Throw ex
            End Try
				Return vCon
		End Function

		Public Sub objClose(ByRef vObj As Object)
				Try
						If Not vObj Is Nothing Then
								If TypeOf vObj Is OdbcCommand Then
										vObj.Dispose()
								Else
                        vObj.Close()
                        vObj.dispose()
								End If
						End If
				Catch ex As Exception
						Throw ex
				End Try
		End Sub

		Public Function ExecuteSql(ByVal vSql As String) As Boolean
				Dim vRet As Boolean
				Dim vCon As OdbcConnection
				Dim vCmd As OdbcCommand

				Try
						vCon = dbConnect()
						vCmd = vCon.CreateCommand
						vCmd.CommandText = vSql
						vCmd.CommandType = CommandType.Text
						vCmd.ExecuteScalar()
						vRet = True
				Catch ex As Exception
						vRet = False
				Finally
						objClose(vCmd)
						objClose(vCon)
				End Try
				Return vRet
		End Function

		Public Function GetDataReader(ByVal vSql As String, Optional ByVal UFlg As String = "D") As OdbcDataReader
				Dim vCon As OdbcConnection
				Dim vDr As OdbcDataReader
				Dim vCmd As OdbcCommand

				Try
						vCon = dbConnect(UFlg)
						vCmd = vCon.CreateCommand
						vCmd.CommandText = vSql
						vCmd.CommandType = CommandType.Text
						vDr = vCmd.ExecuteReader
						Return vDr

				Catch ex As Exception
						Throw ex
				Finally
                'objClose(vDr)
                'objClose(vCmd)
                'objClose(vCon)
				End Try
		End Function

		Public Function GetField(ByVal vFld As Object) As String
				If IsDBNull(vFld) Or IsNothing(vFld) Then vFld = ""
				Return vFld
		End Function

		Public Sub bindControl(ByRef vObj As Object, ByVal vSql As String)
				Dim vCon As OdbcConnection
				Dim vDr As OdbcDataReader
				Dim vCmd As OdbcCommand

				Try
						vCon = dbConnect()
						vCmd = vCon.CreateCommand
						vCmd.CommandText = vSql
						vCmd.CommandType = CommandType.Text
						vDr = vCmd.ExecuteReader

						vObj.DataSource = vDr

						If TypeOf vObj Is DropDownList Then
								With vObj
										.DataValueField = vDr.GetName(0)
										If vDr.FieldCount > 1 Then
												.DataTextField = vDr.GetName(1)
										Else
												.DataTextField = vDr.GetName(0)
										End If
								End With
						End If

						vObj.DataBind()

						If TypeOf vObj Is DropDownList Then
								With vObj
										.SelectedItem.Text = ""
										.SelectedItem.Value = ""
								End With
						End If

				Catch ex As Exception
						Throw ex
				Finally
						objClose(vDr)
						objClose(vCmd)
						objClose(vCon)

				End Try

		End Sub

		Public Function SendMail(ByVal eMailId As String, ByVal vMsg As String) As Boolean
				Dim vAct As Boolean
				Try
						If Trim(eMailId) = "" Then
								vAct = False
						Else
								Dim mObj As MailMessage
								mObj = New MailMessage
								mObj.From = "automated@pshr.com"
								mObj.Subject = "Site Error"
								mObj.To = eMailId
								mObj.Body = vMsg
								mObj.BodyFormat = MailFormat.Html
								mObj.Priority = MailPriority.Normal
								SmtpMail.Send(mObj)
								vAct = True
						End If
				Catch ex As Exception
						vAct = False
				Finally

				End Try

				Return vAct

		End Function

		Public Function AddList(ByRef ddList As DropDownList)
				'This order and code must not get deleted
				ddList.Items.Add("")
				ddList.Items.Add("What is your pet’s name?")
				ddList.Items.Add("What is your favorite color?")
				ddList.Items.Add("Who was your childhood hero? ")
				ddList.Items.Add("What is your first school’s name?")
				ddList.Items.Add("What is your favorite pastime?")
		End Function

		Public Function SayThis(ByVal vMsg As String) As String
				Return "<script language=javascript>alert('" & vMsg & "')</script>"
		End Function
End Module

End Namespace

