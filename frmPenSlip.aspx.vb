
Partial Class frmPenSlip
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Len(Session("EmpId")) = 0 Then
            Response.Redirect("login.aspx")
        End If
        If Not IsPostBack() Then
            Dim OraCn As New OraDBconnection
            Dim ds As New DataSet
            Dim sql As String
            Dim s As String
            sql = "select mths,oldid from pshr.netpenmths where empid = " & Session("EmpId") & ""
            OraCn.FillData2(sql, ds)
            HiddenField1.Value = Session("EmpId")
            s = ""
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)(0)) Then
                    s = ds.Tables(0).Rows(0)(0)
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)(1)) Then
                    If Len(ds.Tables(0).Rows(0)(1)) = 6 Then
                        HiddenField1.Value = Session("EmpId") & "," & ds.Tables(0).Rows(0)(1)
                    End If
                End If
            End If
            ds.Clear()
            ds.Dispose()
            drpGpfYr.Items.Clear()
            drpConsmth.Items.Clear()
            Dim s1 As String
            s1 = ""
            If s <> "" Then
                Dim a() As String = s.Split(",")
                Dim b() As String = {" ", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}
                Dim y As Integer
                Dim y1 As String
                y = 0
                y1 = ""
                Dim drpitm As ListItem
                For i = 0 To a.Count - 1
                    s1 = Mid(a(i), 5, 2)
                    s1 = b(s1) & "-" & Mid(a(i), 1, 4)
                    y = Mid(a(i), 1, 4)
                    drpGpfYr.Items.Add(New ListItem(s1, a(i)))
                    If Mid(a(i), 5, 2) <= "02" Then
                        y = y - 1
                    End If
                    y1 = y & "-" & Mid((y + 1), 3, 2)
                    drpitm = drpConsmth.Items.FindByText(y1)
                    If IsNothing(drpitm) Then
                        drpConsmth.Items.Add(New ListItem(y1, y & "-" & a(i) & ","))
                    Else
                        drpitm.Value = drpitm.Value & a(i) & ","
                    End If
                    'If a(i) > (y & "02") And a(i) < ((y + 1) & "04") Then
                    '    drpConsmth.Items.Add(New ListItem(y & Mid(y + 1, 3, 2), y))
                    '    y = y + 1
                    'End If
                    'If a(i) > ((y + 1) & "04") Then
                    '    y = y + 1
                    'End If
                Next i
                's = "salary.salary" & s.Replace(",", "t,salary.salary") & "t"
                'drpGpfYr.DataSource = a
                'drpGpfYr.DataBind()
            End If
            If drpGpfYr.Items.Count > 0 Then
                drpGpfYr.SelectedIndex = drpGpfYr.Items.Count - 1
                drpConsmth.SelectedIndex = drpConsmth.Items.Count - 1
            Else
                setDat()
                lblMsg.Text = "Pension Slip not available. You may contact concerned DDO for the same."
            End If
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Response.Redirect("empdetail.aspx")
    End Sub

    Private Sub setDat()
        Label2.Visible = False
        drpGpfYr.Visible = False
        Button1.Visible = False
        drpConsmth.Visible = False
        Button2.Visible = False
        Label3.Text = ""
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Session("fyr") = drpGpfYr.SelectedItem.Value
        Response.Redirect("frmrptpenslip.aspx")
    End Sub

    Protected Sub bLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bLogout.Click
        Session("EmpId") = ""
        Session.Abandon()
        Response.Redirect("login.aspx")
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        get_data()
    End Sub

    Private Sub get_data()
        GridView1.Visible = True

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=SalaryCons.xls")
        Response.Charset = ""

        ' If you want the option to open the Excel file without saving then
        ' comment out the line below
        ' Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls"
        Me.EnableViewState = False
        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim stringWrite As New System.IO.StringWriter()
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)
        Dim s1, s2 As String
        sql = "select empid,FIRSTNAME || ' ' || MIDDLENAME || ' ' || LASTNAME as empname,Panno from pshr.empperso where empid = " & Session("EmpId")
        oraCn.FillData1(sql, ds)
        If ds.Tables(0).Rows.Count = 0 Then
            ds.Clear()
            ds.Dispose()
            Exit Sub
        End If
        s1 = ""
        If Not IsDBNull(ds.Tables(0).Rows(0)(1)) Then
            s1 = ds.Tables(0).Rows(0)(1)
        End If
        s2 = ""
        If Not IsDBNull(ds.Tables(0).Rows(0)(2)) Then
            s2 = ds.Tables(0).Rows(0)(2)
        End If
        ds.Clear()
        ds.Dispose()
        htmlWrite.WriteLine("<div><tr><b><u><font size='3'> Emp Id : " & Session("EmpId") & " Name : " & s1 & " Pan : " & s2 & "</font></u></b></tr></div>")

        s1 = Mid(drpConsmth.SelectedItem.Value, 6, Len(drpConsmth.SelectedItem.Value) - 6)
        sql = ""
        Dim a() As String = s1.Split(",")

        Dim i As Integer
        i = 0
        For i = 0 To a.Length - 1
            If sql <> "" Then
                sql = sql & " union all "
            End If
            sql = sql & "select '" & Mid(a(i), 5, 2) & "' as Mth, '" & Mid(a(i), 1, 4) & "' as Yr, case when ptype = 'S' then BP when ptype = 'F' then FM when ptype = 'L' then LFM else 0 end as BasicP,OP as OldAge,DP,IR,DA,MA as Medical,DaArear,LTCAmount as LTCAll,MiscAll + MiscA2 as MiscAll,ITax,Comm_Recov,Cont_Recov,MiscRecov + MISCR2 as MiscRecov,Clubfee,NET_PEN,userid as Loc from pension.tmpen" & a(i) & " p where empid in(" & HiddenField1.Value & ")"
        Next i



        If sql = "" Then
            lblMsg.Text = "Details not available."
            Exit Sub
        End If

        ds = New DataSet()

        sql = sql & " union all select 'Total' as MTH, ' ' as YR,sum(BASICP) as BASICP, sum(OLDAGE) as OLDAGE, sum(DP) as DP, sum(IR) as IR, sum(DA) as DA, sum(MEDICAL) as MEDICAL, sum(DAAREAR) as DAAREAR, sum(LTCALL) as LTCALL, sum(MISCALL) as MISCALL, sum(ITAX) as ITAX, sum(COMM_RECOV) as COMM_RECOV, sum(CONT_RECOV) as CONT_RECOV, sum(MISCRECOV) as MISCRECOV, sum(clubfee) as clubfee,sum(NET_PEN) as NET_PEN, ' ' as LOC from (" & sql & ") b"


        oraCn.FillData2(sql, ds)

        GridView1.DataSource = ds
        GridView1.DataBind()
        GridView1.RenderControl(htmlWrite)

        ds.Clear()
        ds.Dispose()




        Response.Write(stringWrite.ToString())
        Response.[End]()

        GridView1.Visible = False
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Confirms that an HtmlForm control is rendered for the 

        ' specified ASP.NET server control at run time. 

        ' No code required here. 
    End Sub
End Class
