''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Code Module  : empdetail
'Project      : PSHR Search Engine
'Author       : Santosh Kumar
'Role         : Project Leader
'Designation  : Project Leader
'Department   : Software Engineering
'Created Date : 26-Nov-2007
'Modified by  : 
'Modified on  : 

'Description  : To display employee details
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports System.Data.Odbc
Imports Oracle.DataAccess.Client
Imports System.DateTime
Imports System.IO

Namespace pshr


Partial Class empdetail
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
        Dim empid As String
        Private Sub Show_Message_Ndays(ByVal msg As String, ByVal dt_join_accept As System.DateTime, ByVal ndays As Integer)
            Dim dt_temp As DateTime
            'check if dt_join_accept is valid datetime
            If (DateTime.TryParse(dt_join_accept, dt_temp)) Then
                'only show message is joining acceptance date is not more than ndays old
                If dt_join_accept.AddDays(ndays) > System.DateTime.Now Then
                    lblTransferMessage.Text = msg
                End If
            End If
        End Sub
        Private Sub Handle_Transfer_Promotion(ByVal status As String, ByVal dt_join_accept As System.DateTime,
                                              ByVal isRelComments As Boolean, ByVal isJoinComments As Boolean)
            Dim defpage As String = "./ChargeReport.aspx"
            Session("ucontrol") = "uc_req_tnp.ascx"
            Session("Status") = status
            If status = "None" Then
                lblTransferMessage.Text = "There is a change in your posting. <a href='" & defpage & "'>Click here</a> to submit Relieve Request"
            ElseIf status = "RRA" Then
                lblTransferMessage.Text = "Your Relieve Request have been accepted. <a href='" & defpage & "'>Click here</a> to submit Joining Request"
            ElseIf status = "RRS" And isRelComments Then
                lblTransferMessage.Text = "Your Relieve Request is Rejected by the Officer. <a href='" & defpage & "'>Click here</a> to view the comments."
            ElseIf status = "RRS" Then
                lblTransferMessage.Text = "Your Relieve Request is Pending with the Officer. <a href='" & defpage & "'>Click here</a> to view Request"
            ElseIf status = "JRS" And isJoinComments Then
                lblTransferMessage.Text = "Your Joining Request is Rejected by the Officer. <a href='" & defpage & "'>Click here</a> to view the comments."
            ElseIf status = "JRS" Then
                lblTransferMessage.Text = "Your Joining Request is Pending with the Officer. <a href='" & defpage & "'>Click here</a> to view Request"
            ElseIf status = "JRA" Then
                'show transfer message for 10 days
                Show_Message_Ndays("Your Request is Accepted. <a href='" & defpage & "'>Click here</a> to view Request", dt_join_accept, 10)
            End If
        End Sub
        Private Sub Handle_Leave_Join(ByVal status As String, ByVal dt_join_accept As System.DateTime)
            'status types:
            'None: Will submit joining request
            'JRS: Joining request is submitted
            'JRA: Joining request accepted. Update Tables.
            Dim defpage As String = "./ChargeReport.aspx"
            Session("ucontrol") = "uc_req_leave_join.ascx"
            Session("Status") = status
            If status = "None" Then
                lblTransferMessage.Text = "Back from Leave! <a href='" & defpage & "'>Click here</a> to submit your joining request."
            ElseIf status = "JRS" Then
                lblTransferMessage.Text = "Your Joining Request is pending with the officer. <a href='" & defpage & "'>Click here</a> to view request."
            ElseIf status = "JRA" Then
                'show leave join message for 10 days
                Show_Message_Ndays("Your Joining Request is Accepted. <a href='" & defpage & "'>Click here</a> to view Request", dt_join_accept, 10)
            End If
        End Sub
        Private Sub Handle_Leave_Relieve(ByVal status As String, ByVal dt_join_accept As System.DateTime)
            'status types:
            'None: Will submit leave request
            'RRS: Relieve request is submitted
            'RRA: Relieve request accepted. Update Tables.
            Dim defpage As String = "./ChargeReport.aspx"
            Session("ucontrol") = "uc_req_leave_relieve.ascx"
            Session("Status") = status
            If status = "None" Then
                lblTransferMessage.Text = "Going for Leave! <a href='" & defpage & "'>Click here</a> to submit your Leave Request."
            ElseIf status = "RRS" Then
                lblTransferMessage.Text = "Your Joining Request is pending with the officer. <a href='" & defpage & "'>Click here</a> to view request."
            ElseIf status = "RRA" Then
                'show leave join message for 10 days
                Show_Message_Ndays("Your Leave Request is Accepted. <a href='" & defpage & "'>Click here</a> to view Request", dt_join_accept, 10)
            End If
        End Sub
        Private Sub Handle_Retirement(ByVal status As String, ByVal dt_relieve_accept As System.DateTime)
            'status types:
            'None: Will submit relieve request
            'RRS: Relieve request is submitted
            'RRA: Relieve request accepted. Update Tables.
            Dim defpage As String = "./ChargeReport.aspx"
            Session("ucontrol") = "uc_req_retire.ascx"
            Session("Status") = status
            If status = "None" Then
                lblTransferMessage.Text = "<a href='" & defpage & "'>Click here</a> to submit your Retirement Relieve Request."
            ElseIf status = "RRS" Then
                lblTransferMessage.Text = "Your Relieve Request is pending with the officer. <a href='" & defpage & "'>Click here</a> to view request."
            ElseIf status = "RRA" Then
                'show retirement accepted message for 10 days
                Show_Message_Ndays("Your Retirement Request is Accepted. <a href='" & defpage & "'>Click here</a> to view Request.", dt_relieve_accept, 10)
            End If
        End Sub
        Private Sub TransferActions()
            Dim sql As String
            Dim status As String
            Dim ds As New System.Data.DataSet
            Dim oracn As New OraDBconnection
            Dim oodate As New System.DateTime
            Dim dt_join_accept, dt_rel_accept As New System.DateTime
            Dim eventcode As Integer
            Dim arrLeaveEvents() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 62, 63, 86, 98}
            Dim arrRetdEvents() As Integer = {11, 12, 13, 14, 15, 16, 89}
            Dim isRelComments As Boolean = False
            Dim isJoinComments As Boolean = False

            empid = Session("EmpId").ToString()
            sql = String.Format("select nvl(status,'None') as status, oodate, " +
                                "date_join_accept, date_rel_accept, eventcode, " +
                                "rel_off_comment, join_off_comment from " +
                                "CADRE.chargereport where empid = {0} order by oodate desc", empid)
            oracn.FillData(sql, ds)
            If ds.Tables(0).Rows.Count < 1 Then
                Return
            End If

            status = ds.Tables(0).Rows(0)("status").ToString()
            oodate = ds.Tables(0).Rows(0)("oodate").ToString()
            eventcode = ds.Tables(0).Rows(0)("eventcode").ToString()
            isRelComments = ds.Tables(0).Rows(0)("rel_off_comment").ToString().Length > 0
            isJoinComments = ds.Tables(0).Rows(0)("join_off_comment").ToString().Length > 0

            If Not Convert.IsDBNull(ds.Tables(0).Rows(0)("date_join_accept")) Then
                dt_join_accept = ds.Tables(0).Rows(0)("date_join_accept")
            End If
            If Not Convert.IsDBNull(ds.Tables(0).Rows(0)("date_rel_accept")) Then
                dt_rel_accept = ds.Tables(0).Rows(0)("date_rel_accept")
            End If

            If (eventcode = 36 Or eventcode = 28 Or eventcode = 37) Then
                'CTRP (36) and CPRO(28)
                Handle_Transfer_Promotion(status, dt_join_accept, isRelComments, isJoinComments)
            ElseIf (eventcode = 10) Then
                'LJON(10) - ending of any kind of leave
                Handle_Leave_Join(status, dt_join_accept)
            ElseIf (arrLeaveEvents.Contains(eventcode)) Then
                '"Going to leave" events handling
                Handle_Leave_Relieve(status, dt_rel_accept)
            ElseIf (arrRetdEvents.Contains(eventcode)) Then
                'Retirement events handling
                Handle_Retirement(status, dt_rel_accept)
            End If
        End Sub
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            btnPrint.Attributes.Add("onClick", "javascript:window.print();")
            'Try
            If Not IsPostBack Then
                'Session("AppId") = "L"                               'Login mode
                'Session("EmpId") = "107036"
                'Session("EmpNm") = "Pankaj"
                'Session("EmpGpf") = "abcdef"
                'Session("EmpSt") = 10

                If Len(Session("EmpId")) = 0 Then
                    Response.Redirect("login.aspx")
                End If
                If Session("AppId") <> "L" Then
                    Response.Redirect("login.aspx")
                End If

                lMsg0.Text = Session("MsgPL")
                Session("MsgPL") = ""
                'Dim vDr As OdbcDataReader
                Dim oracn As New OraDBconnection
                Dim sql As String
                Dim ds As New System.Data.DataSet

                TransferActions()

                empid = Session("EmpId")     'EmpId = Request("empid")

                sql = "select a.empid, gpfno, firstname || ' ' || middlename || ' ' || lastname, " & _
             "panno, fathername, bloodgroup, pshr.get_desg(cdesgcode) as desgtext , to_char(dob,'dd-Mon-yy'), pshr.get_org(cloccode) as locname, to_char(doj,'dd-Mon-yy'), " & _
             "d.pdoorno, d.psteet, d.pcity, d.ppincode, d.cdoorno, d.csteet, d.ccity, d.cpincode, d.phonecell, d.mailid  " & _
             "from pshr.empperso a, pshr.empaddr d where " & _
             "a.empid=d.empid(+) and a.empid like '" & empid & "'"

                oracn.FillData1(sql, ds)

                Label1.Text = GetField(ds.Tables(0).Rows(0)(0))
                Label2.Text = GetField(ds.Tables(0).Rows(0)(1))
                Label3.Text = GetField(ds.Tables(0).Rows(0)(2))
                Label4.Text = GetField(ds.Tables(0).Rows(0)(3))
                Label5.Text = GetField(ds.Tables(0).Rows(0)(4))
                Label6.Text = GetField(ds.Tables(0).Rows(0)(5))
                Label7.Text = GetField(ds.Tables(0).Rows(0)(6))
                Label8.Text = GetField(ds.Tables(0).Rows(0)(7))
                Label9.Text = GetField(ds.Tables(0).Rows(0)(8))
                Label10.Text = GetField(ds.Tables(0).Rows(0)(9))
                Label11.Text = GetField(ds.Tables(0).Rows(0)(10))
                Label12.Text = GetField(ds.Tables(0).Rows(0)(11))
                Label13.Text = GetField(ds.Tables(0).Rows(0)(12))
                Label14.Text = GetField(ds.Tables(0).Rows(0)(13))
                Label15.Text = GetField(ds.Tables(0).Rows(0)(14))
                Label16.Text = GetField(ds.Tables(0).Rows(0)(15))
                Label17.Text = GetField(ds.Tables(0).Rows(0)(16))
                Label18.Text = GetField(ds.Tables(0).Rows(0)(17))

                Label20.Text = GetField(ds.Tables(0).Rows(0)(18))
                Label21.Text = GetField(ds.Tables(0).Rows(0)(19))

                Session("MbNo") = Trim(Label20.Text.ToString)
                Session("Email") = Trim(Label21.Text.ToString)

                GetQryFamily()

                GetQryNominee()


                'bindControl(gvFamily, GetQryFamily())

                'bindControl(gvNominee, GetQryNominee())


                'vDr = GetDataReader(GetQry1)
                'If vDr.HasRows Then
                '    If Session("EmpSt") = 10 And Session("EmpId") <> "101765" Then
                '        LinkButton1.Enabled = False
                '    End If
                '    Label1.Text = GetField(vDr.Item(0))
                '    Label2.Text = GetField(vDr.Item(1))
                '    Label3.Text = GetField(vDr.Item(2))
                '    Label4.Text = GetField(vDr.Item(3))
                '    Label5.Text = GetField(vDr.Item(4))
                '    Label6.Text = GetField(vDr.Item(5))
                '    Label7.Text = GetField(vDr.Item(6))
                '    Label8.Text = GetField(vDr.Item(7))
                '    Label9.Text = GetField(vDr.Item(8))
                '    Label10.Text = GetField(vDr.Item(9))
                '    Label11.Text = GetField(vDr.Item(10))
                '    Label12.Text = GetField(vDr.Item(11))
                '    Label13.Text = GetField(vDr.Item(12))
                '    Label14.Text = GetField(vDr.Item(13))
                '    Label15.Text = GetField(vDr.Item(14))
                '    Label16.Text = GetField(vDr.Item(15))
                '    Label17.Text = GetField(vDr.Item(16))
                '    Label18.Text = GetField(vDr.Item(17))

                GetQry2()
                'bindControl(gvEvents, GetQry2())
                'db2Control()
                imgEmpPhoto.ImageUrl = "ShowImage.ashx?eid=" & Session("EmpId").ToString

                GetPendingRequests()
                'icard_st()
            End If

            'Catch Ex As Exception
            '    If Ex.Message <> "Thread was being aborted." Then
            '        Session("IsErr") = Ex
            '    End If
            '    'Finally
            '    '    ' objClose(vDr)
            '    '    If Session("IsErr").ToString <> "" Then _
            '    '    Response.Redirect("ErrHandler.aspx")
            'End Try


            'gurpreet
            'If Len(Trim(Label2.Text)) = 12 Then
            '    cpf_sch()
            'End If
        End Sub
        Protected Sub cpf_sch()
            Dim oracn As New OraDBconnection
            Dim ds As New System.Data.DataSet
            Dim sql As String
            sql = "select count(*) from salary.salary_cpf_cons where batch_no > 0 and PRAN='" & Label2.Text & "'"
            oracn.FillData1(sql, ds)
            If ds.Tables(0).Rows(0)(0) > 0 Then
                lbcpfsch.Visible = True
            Else
                lbcpfsch.Visible = False
            End If
            ds.Clear()
            ds.Dispose()
        End Sub

        Protected Sub icard_st()
            Dim oracn As New OraDBconnection
            Dim ds As New System.Data.DataSet
            Dim sql As String
            sql = "select print_st from salary.icard_main where dt_sub >'07-Aug-2012' and (add_months(dispatch_dt,1)>=sysdate or dispatch_dt is null) and edit_id <> '999999' and empid=" & Session("EmpId")
            oracn.FillData1(sql, ds)
            If ds.Tables(0).Rows.Count > 0 Then
                lMsg1.Text = "The Status of your Icard request : " & ds.Tables(0).Rows(0)(0).ToString
            End If
            ds.Clear()
            ds.Dispose()
        End Sub

        Private Function GetQry1()
            Return _
             "select a.empid, gpfno, firstname || ' ' || middlename || ' ' || lastname, " & _
             "panno, fathername, bloodgroup, pshr.get_desg(cdesgcode) as desgtext , to_char(dob,'dd-Mon-yy'), pshr.get_org(cloccode) as locname, to_char(doj,'dd-Mon-yy'), " & _
             "d.pdoorno, d.psteet, d.pcity, d.ppincode, d.cdoorno, d.csteet, d.ccity, d.cpincode  " & _
             "from empperso a, empaddr d where " & _
             "a.empid=d.empid(+) and a.empid like '" & EmpId & "'"
        End Function

        Private Function GetQry2()
            'Return _
            ' "select event, to_char(fromdate,'dd-Mon-yy') relieving_date, to_char(todate,'dd-Mon-yy') joining_date, desgtext designation, locname location " & _
            ' "from emphistory a, mast_desg b, mast_loc c, mast_event d " & _
            ' "where a.eventcode=d.eventcode and a.desgcode=b.desgcode " & _
            ' "and a.loccode=c.loccode and empid like '" & EmpId & "' order by to_char(fromdate,'yyyymmdd')||to_char(todate,'yyyymmdd') "
            Dim oracn As New OraDBconnection
            Dim sql As String
            Dim ds As New System.Data.DataSet
            sql = "select event, to_char(fromdate,'dd-Mon-yy') relieving_date, to_char(todate,'dd-Mon-yy') joining_date, desgtext designation, locname location " & _
             "from pshr.emphistory a, pshr.mast_desg b, pshr.mast_loc c, pshr.mast_event d " & _
             "where a.eventcode=d.eventcode and a.desgcode=b.desgcode " & _
             "and a.loccode=c.loccode and empid like '" & empid & "' and a.eventcode <> 102 order by a.rowno "
            oracn.FillData(sql, ds)
            oracn.fillgrid(gvEvents, ds)
        End Function
        Private Function GetQryFamily()
            'Return _
            '"SELECT trim(firstname || ' ' || middlename || ' ' ||  lastname) as Name, trim((SELECT reltext FROM PSHR.mast_relation WHERE relcode=e.relcode)) as Relation, " & _
            '"to_char(dob,'dd-Mon-yyyy') as dob FROM empfamily e WHERE empid = '" & EmpId & "'"
            Dim oracn As New OraDBconnection
            Dim sql As String
            Dim ds As New System.Data.DataSet
            sql = "SELECT trim(firstname || ' ' || middlename || ' ' ||  lastname) as Name, trim((SELECT reltext FROM PSHR.mast_relation WHERE relcode=e.relcode)) as Relation, " & _
            "to_char(dob,'dd-Mon-yyyy') as dob FROM pshr.empfamily e WHERE empid = '" & EmpId & "'"
            oracn.FillData(sql, ds)
            oracn.fillgrid(gvFamily, ds)
        End Function
        Private Function GetQryNominee()
            'Return _
            '"SELECT trim(nname) AS Name, trim((SELECT reltext FROM PSHR.mast_relation WHERE relcode=e.relcode)) AS Relation " & _
            '"FROM emppfnom e WHERE empid = '" & EmpId & "'"
            Dim oracn As New OraDBconnection
            Dim sql As String
            Dim ds As New System.Data.DataSet
            sql = "SELECT trim(nname) AS Name, trim((SELECT reltext FROM PSHR.mast_relation WHERE relcode=e.relcode)) AS Relation " & _
            "FROM pshr.emppfnom e WHERE empid = '" & EmpId & "'"
            oracn.FillData(sql, ds)
            oracn.fillgrid(gvNominee, ds)
        End Function
        Private Sub GetPendingRequests()
            'Return _
            '"SELECT trim(nname) AS Name, trim((SELECT reltext FROM PSHR.mast_relation WHERE relcode=e.relcode)) AS Relation " & _
            '"FROM emppfnom e WHERE empid = '" & EmpId & "'"
            Dim oracn As New OraDBconnection
            Dim sql As String
            Dim ds As New System.Data.DataSet
            Dim empid = Session("EmpId").ToString()

            sql = "select  " +
                    "(select count(*) from CADRE.chargereport where rep_off_rel =" + empid + " and status = 'RRS' and eventcode in (36,28,37)) relcount, " & _
                    "(select count(*) from CADRE.chargereport where rep_off_join=" + empid + " and status = 'JRS' and eventcode in (36,28,37)) joincount, " & _
                    "(select count(*) from CADRE.chargereport where rep_off_rel=" + empid + " and status = 'RRS' and eventcode in (1, 2, 3, 4, 5, 6, 7, 8, 9, 62, 63, 86, 98)) leavereqcount, " & _
                    "(select count(*) from CADRE.chargereport where rep_off_join=" + empid + " and status = 'JRS' and eventcode = 10) leavejoincount, " & _
                    "(select count(*) from CADRE.chargereport where rep_off_rel=" + empid + " and status = 'RRS' and eventcode in (11, 12, 13, 14, 15, 16, 89)) retirementcount " & _
                    " from dual "
            oracn.FillData(sql, ds)

            If ds.Tables(0).Rows(0)("relcount") > 0 Then
                lnkPRR.Text = "Transfer/Promotion Relieve Requests (" & ds.Tables(0).Rows(0)("relcount") & ")"
                lnkPRR.Visible = True
            End If

            If ds.Tables(0).Rows(0)("joincount") > 0 Then
                lnkPJR.Text = "Transfer/Promotion Joining Requests (" & ds.Tables(0).Rows(0)("joincount") & ")"
                lnkPJR.Visible = True
            End If

            If ds.Tables(0).Rows(0)("leavereqcount") > 0 Then
                lnkLRR.Text = "Leave Requests (" & ds.Tables(0).Rows(0)("leavereqcount") & ")"
                lnkLRR.Visible = True
            End If

            If ds.Tables(0).Rows(0)("leavejoincount") > 0 Then
                lnkLJR.Text = "Leave Joining Requests (" & ds.Tables(0).Rows(0)("leavejoincount") & ")"
                lnkLJR.Visible = True
            End If

            If ds.Tables(0).Rows(0)("retirementcount") > 0 Then
                lnkRR.Text = "Retirement Requests (" & ds.Tables(0).Rows(0)("retirementcount") & ")"
                lnkRR.Visible = True
            End If
        End Sub
        Private Sub bLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bLogout.Click
            Session.Abandon()
            Response.Redirect("login.aspx")
        End Sub

        Private Sub db2Control()

            'Exit Sub

            Dim PicField As Integer = 0  'the column # of the BLOB field
            Dim dPath1 As String = "C:\\Inetpub\\wwwroot\\pshr\\emp\\empid.jpg" '& "\emp\empid.jpg"
            Dim dPath2 As String = "http://10.10.1.2/pshr/emp/empid.jpg?fake=" & Date.Now.Ticks.ToString() '& "\emp\empid.jpg"
            Dim sPath1 As String = "http://10.10.1.2/pshr/emp/frame.jpg?fake=" & Date.Now.Ticks.ToString()

            'Dim dPath As String = "C:\Inetpub\wwwroot\emp\empid.jpg" '& "\emp\empid.jpg"
            'Dim sPath As String = "C:\Inetpub\wwwroot\emp\frame.jpg" '& "\emp\frame.jpg
            'HttpContext.Current.Request.ApplicationPath
            'System.AppDomain.CurrentDomain.BaseDirectory() 
            'Server.MapPath("\emp")

            Dim vDr As OdbcDataReader
            Try
                vDr = GetDataReader("select photo from img_pshr.img where empid like '" & EmpId & "'", "I")
                If vDr.HasRows Then
                    vDr.Read()
                    Try

                        Dim vByte(vDr.GetBytes(PicField, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
                        vDr.GetBytes(PicField, 0, vByte, 0, vByte.Length)
                        Dim fs As New System.IO.FileStream(dPath1, IO.FileMode.Create, IO.FileAccess.Write)
                        fs.Write(vByte, 0, vByte.Length)
                        fs.Close()
                        imgEmpPhoto.ImageUrl = DirectCast(dPath2, String)
                    Catch ex As Exception
                        imgEmpPhoto.ImageUrl = DirectCast(sPath1, String)
                    End Try
                Else
                    imgEmpPhoto.ImageUrl = DirectCast(sPath1, String)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                objClose(vDr)
            End Try
        End Sub

        'Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        '    Response.Redirect("frmPenSlip.aspx")
        'End Sub

        Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        End Sub

        Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
            Response.Redirect("frmchangembemail.aspx")
        End Sub

        Protected Sub lbcpfsch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbcpfsch.Click
            Dim oracn As New OraDBconnection
            Dim ds1 As New DataSet
            Dim sql As String
            sql = "select Batch_No,Mth,Yr,Userid,Ledger,Empid,PRAN,emp_Share as Employer_Share,emp_share as Employee_Share,Intr_Comp as Interest_On_Employer_Share,Intr_Emp as Interest_On_Employee_Share,emp_share + intr_comp as Total_Employer,emp_share + intr_emp as Total_Employee from salary.salary_cpf_cons where pran = '" & Label2.Text & "' and batch_no > 0 and emp_share <> 0 "
            sql = sql & " union all select Batch_No,'' as Mth,'' as Yr,'Total','',null,'',sum(Employer_Share),sum(Employee_Share),sum(Interest_On_Employer_Share),sum(Interest_On_Employee_share),round(sum(total_employer)),round(sum(total_employee)) from (" & sql & ") group by batch_no order by batch_no,yr,mth"
            oracn.FillData(sql, ds1)
            If ds1.Tables(0).Rows.Count > 0 Then
                GridView1.DataSource = ds1
                GridView1.DataBind()
            Else
                Exit Sub
            End If
            ds1.Clear()
            ds1.Dispose()
            Response.AddHeader("content-disposition", "attachment;filename=FileName.xls")
            Response.Charset = ""
            Response.ContentType = "application/vnd.xls"
            Me.EnableViewState = False
            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
            htmlWrite.WriteLine("<b><u><fontsize='10'>CPF Arrear Details PRAN:" & Label2.Text & " </font></u></b>")
            GridView1.RenderControl(htmlWrite)
            Response.Write(stringWrite.ToString())
            Response.[End]()
        End Sub
        Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

            ' Confirms that an HtmlForm control is rendered for the 

            ' specified ASP.NET server control at run time. 

            ' No code required here. 
        End Sub

        Protected Sub lnkPRR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPRR.Click
            Session("Status") = "RRS"
            Session("ucontrol") = "uc_acc_tnp.ascx"
            Response.Redirect("./ChargeReport.aspx")
        End Sub

        Protected Sub lnkPJR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPJR.Click
            Session("Status") = "JRS"
            Session("ucontrol") = "uc_acc_tnp.ascx"
            Response.Redirect("./ChargeReport.aspx")
        End Sub

        Protected Sub lnkLRR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLRR.Click
            Session("Status") = "RRS"
            Session("ucontrol") = "uc_acc_leave_relieve.ascx"
            Response.Redirect("./ChargeReport.aspx")
        End Sub

        Protected Sub lnkLJR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLJR.Click
            Session("Status") = "JRS"
            Session("ucontrol") = "uc_acc_leave_join.ascx"
            Response.Redirect("./ChargeReport.aspx")
        End Sub

        Protected Sub lnkRR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRR.Click
            Session("Status") = "RRS"
            Session("ucontrol") = "uc_acc_retire.ascx"
            Response.Redirect("./ChargeReport.aspx")
        End Sub
    End Class

End Namespace


