
Partial Class frmSalSlip
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim OraCn As New OraDBconnection
            Dim ds As New DataSet
            Dim sql As String
            ds = New DataSet()
            sql = "select case when substr(tname,11,2) = '01' then 'Jan' when substr(tname,11,2) = '02' then 'Feb' when substr(tname,11,2) = '03' then 'Mar' when substr(tname,11,2) = '04' then 'Apr' when substr(tname,11,2) = '05' then 'May' when substr(tname,11,2) = '06' then 'Jun' when substr(tname,11,2) = '07' then 'Jul' when substr(tname,11,2) = '08' then 'Aug' when substr(tname,11,2) = '09' then 'Sep' when substr(tname,11,2) = '10' then 'Oct' when substr(tname,11,2) = '11' then 'Nov' when substr(tname,11,2) = '12' then 'Dec' end  || '-' || substr(tname,7,4) as tname,substr(tname,7,6) tname1 from tab where tname like 'SALARY20%' and length(tname) = 12 order by tname1"
            OraCn.FillData1(sql, ds)
            Dim i As Integer
            For i = 0 To ds.Tables(0).Rows.Count - 2
                Dim ds1 As New DataSet()
                sql = "select count(*) from SALARY" & ds.Tables(0).Rows(i)(1) & " where empid = " & Session("EmpId") & ""
                OraCn.FillData1(sql, ds1)
                If ds1.Tables(0).Rows(0)(0) > 0 Then
                    drpGpfYr.Items.Add(New ListItem(ds.Tables(0).Rows(i)(0), ds.Tables(0).Rows(i)(1)))
                End If
                ds1.Clear()
                ds1.Dispose()
            Next i
            ds.Clear()
            ds.Dispose()
            If drpGpfYr.Items.Count > 0 Then
                drpGpfYr.SelectedIndex = drpGpfYr.Items.Count - 1
            Else
                setDat()
                lblMsg.Text = "Pay Slip not available. You may contact concerned DDO for the same."
            End If
        End If
    End Sub

    Private Sub setDat()
        Label2.Visible = False
        drpGpfYr.Visible = False
        Button1.Visible = False
        LinkButton2.Visible = False
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("empdetail.aspx")
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Session("rptNm") = "rptsalslip"
        Session("fyr") = drpGpfYr.SelectedItem.Value
        Response.Redirect("frmrptvwr.aspx")
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        GridView1.Visible = True

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=FileName.xls")
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
        htmlWrite.WriteLine("<div><tr><b><u><font size='5'> Allowances </font></u></b></tr></div>")
        Dim ScanData As New DataTable("ScanData")
        Dim sdrow As DataRow = ScanData.NewRow()
        Dim col As New DataColumn("abc")
        Dim i As Integer
        Dim s As String
        s = ""
        For i = 0 To 22
            s = "Col" & i
            If i = 0 Then
                s = "MONTH"
            ElseIf i = 1 Then
                s = "YEAR"
            ElseIf i = 2 Then
                s = "BscPay"
            ElseIf i = 3 Then
                s = "GrdPay"
            ElseIf i = 4 Then
                s = "DPay"
            ElseIf i = 5 Then
                s = "IntrmR"
            ElseIf i = 6 Then
                s = "NonPA"
            ElseIf i = 7 Then
                s = "PersnP"
            ElseIf i = 8 Then
                s = "SpclPa"
            ElseIf i = 9 Then
                s = "Medicl"
            ElseIf i = 10 Then
                s = "DAll"
            ElseIf i = 11 Then
                s = "HRAll"
            ElseIf i = 12 Then
                s = "ShiftA"
            ElseIf i = 13 Then
                s = "GenAll"
            ElseIf i = 14 Then
                s = "CityCA"
            ElseIf i = 15 Then
                s = "LODAL"
            ElseIf i = 16 Then
                s = "ELECA"
            ElseIf i = 17 Then
                s = "RuralA"
            ElseIf i = 18 Then
                s = "Police"
            ElseIf i = 19 Then
                s = "Pprotc"
            ElseIf i = 20 Then
                s = "C/EPF"
            ElseIf i = 21 Then
                s = "MISCA"
            ElseIf i = 22 Then
                s = "GrossP"
            End If
            Dim col1 As New DataColumn(s)
            col1.DataType = GetType(String)
            col1.MaxLength = 15
            'empId.Unique = true;
            col1.AllowDBNull = True
            col1.Caption = s
            ScanData.Columns.Add(col1)
        Next i

        Dim yr, yr1 As Integer
        Dim fm, tm As Integer
        Dim empid As String
        Dim j, k As Integer
        Dim sql1, tmp As String

        yr = Now.Year
        fm = 3
        tm = 3
        If tm < 4 Then
            tm = tm + 12
        End If
        tmp = ""


        i = Convert.ToInt16(fm)
        j = Convert.ToInt16(tm)
        k = i
        sql = ""
        sql1 = ""

        empid = Session("EmpId")

        For k = i To j
            yr1 = yr
            tmp = k
            If Val(tmp) > 12 Then
                tmp = Convert.ToInt16(Val(tmp) - 12)
                yr1 = Convert.ToInt16(Val(yr) + 1)
            End If
            If Len(tmp) < 2 Then
                tmp = "0" & tmp
            End If
            ds = New DataSet
            sql1 = "select count(*) from tab where tname = 'SALARY" & yr1 & tmp & "'"
            oraCn.FillData1(sql1, ds)

            If ds.Tables(0).Rows(0)(0) > 0 Then
                If sql <> "" Then
                    sql = sql & " union all "
                End If
                'sql = sql & "select * from salary" & yr1 & tmp & " where loccode = " & Session("ULOC").ToString() & " and empid = " & empid & " and userid = '" & Session("UID").ToString() & "'"
                sql = sql & "select * from salary" & yr1 & tmp & " where empid = " & empid & ""
                If k = 3 Then
                    sql = sql & " and ledger <> 'Arrear' "
                ElseIf k = 15 Then
                    sql = sql & " and ledger = 'Arrear' "
                End If
            End If

            ds.Clear()
            ds.Dispose()
        Next k
        ds = New DataSet()
        sql = "select * from (" & sql & ") a order by  a.yr,a.mth"
        oraCn.FillData1(sql, ds)


        Dim r As Integer
        Dim tot As Double
        Dim a(25), d(25) As Double
        tot = 0
        For r = 0 To ds.Tables(0).Rows.Count - 1
            sdrow = ScanData.NewRow()
            sdrow(0) = ds.Tables(0).Rows(r)(135).ToString()
            sdrow(1) = ds.Tables(0).Rows(r)(136).ToString()
            tot = ds.Tables(0).Rows(r)(20) + ds.Tables(0).Rows(r)(21)
            sdrow(2) = tot
            sdrow(3) = "    " & ds.Tables(0).Rows(r)(22).ToString()
            tot = ds.Tables(0).Rows(r)(23) + ds.Tables(0).Rows(r)(24)
            sdrow(4) = tot
            tot = ds.Tables(0).Rows(r)(26) + ds.Tables(0).Rows(r)(27)
            sdrow(5) = tot
            tot = ds.Tables(0).Rows(r)(26) + ds.Tables(0).Rows(r)(27)
            sdrow(6) = tot
            tot = ds.Tables(0).Rows(r)(137) + ds.Tables(0).Rows(r)(138)
            sdrow(7) = tot
            tot = ds.Tables(0).Rows(r)(31) + ds.Tables(0).Rows(r)(32)
            sdrow(8) = tot
            tot = ds.Tables(0).Rows(r)(33) + ds.Tables(0).Rows(r)(34)
            sdrow(9) = tot
            tot = ds.Tables(0).Rows(r)(35) + ds.Tables(0).Rows(r)(36)
            sdrow(10) = tot
            tot = ds.Tables(0).Rows(r)(38) + ds.Tables(0).Rows(r)(39)
            sdrow(11) = tot
            tot = ds.Tables(0).Rows(r)(41) + ds.Tables(0).Rows(r)(42)
            sdrow(12) = tot
            tot = ds.Tables(0).Rows(r)(43) + ds.Tables(0).Rows(r)(44)
            sdrow(13) = tot
            sdrow(14) = ds.Tables(0).Rows(r)(45).ToString()
            tot = ds.Tables(0).Rows(r)(46) + ds.Tables(0).Rows(r)(47)
            sdrow(15) = tot
            sdrow(16) = ds.Tables(0).Rows(r)(48).ToString()
            tot = ds.Tables(0).Rows(r)(49) + ds.Tables(0).Rows(r)(50)
            sdrow(17) = tot
            tot = ds.Tables(0).Rows(r)(51) + ds.Tables(0).Rows(r)(52) + ds.Tables(0).Rows(r)(53)
            sdrow(18) = tot
            sdrow(19) = ds.Tables(0).Rows(r)(54).ToString()
            sdrow(20) = ds.Tables(0).Rows(r)(55).ToString()
            sdrow(21) = ds.Tables(0).Rows(r)(56).ToString()
            sdrow(22) = ds.Tables(0).Rows(r)(126).ToString()
            For i = 0 To 22
                a(i) = a(i) + Val(sdrow(i))
            Next i
            ScanData.Rows.Add(sdrow)
        Next r
        Dim sdrow1 As DataRow = ScanData.NewRow()
        For i = 0 To 22
            sdrow1(i) = ""
            If i = 0 Then
                sdrow1(i) = "Total"
            End If
            If i > 1 Then
                sdrow1(i) = a(i)
            End If
        Next i
        ScanData.Rows.Add(sdrow1)

        GridView1.DataSource = ScanData
        GridView1.DataBind()


        'htmlWrite.WriteLine("<b><u><fontsize='5'>TDS FOR THE MONTH " & drpMth.SelectedItem.Value & "  /  " & drpYr.SelectedItem.Value & "</font></u></b>")
        GridView1.RenderControl(htmlWrite)

        ScanData.Clear()
        ScanData.Dispose()

        ScanData = New DataTable()

        htmlWrite.WriteLine("<b><u><font size='5'> Recoveries </font></u></b>")
        s = ""
        For i = 0 To 22
            s = "Col" & i
            If i = 0 Then
                s = "MONTH"
            ElseIf i = 1 Then
                s = "YEAR"
            ElseIf i = 2 Then
                s = "Subscr"
            ElseIf i = 3 Then
                s = "Recovr"
            ElseIf i = 4 Then
                s = "IncTax"
            ElseIf i = 5 Then
                s = "HRecov"
            ElseIf i = 6 Then
                s = "WtrFan"
            ElseIf i = 7 Then
                s = "ClubFe"
            ElseIf i = 8 Then
                s = "SalAdv"
            ElseIf i = 9 Then
                s = "CarAdv"
            ElseIf i = 10 Then
                s = "SctADV"
            ElseIf i = 11 Then
                s = "Comput"
            ElseIf i = 12 Then
                s = "Instal"
            ElseIf i = 13 Then
                s = "PUseV"
            ElseIf i = 14 Then
                s = "HBAdv"
            ElseIf i = 15 Then
                s = "MiscR"
            ElseIf i = 16 Then
                s = "LICSub"
            ElseIf i = 17 Then
                s = "GICSub"
            ElseIf i = 18 Then
                s = "Benove"
            ElseIf i = 19 Then
                s = "Mobile"
            ElseIf i = 20 Then
                s = "Deduct"
            ElseIf i = 21 Then
                s = "   "
            ElseIf i = 22 Then
                s = "NetPay"
            End If
            col = New DataColumn(s)
            col.DataType = GetType(String)
            col.MaxLength = 15
            'empId.Unique = true;
            col.AllowDBNull = True
            col.Caption = s
            ScanData.Columns.Add(col)
        Next i

        tot = 0

        For i = 0 To 22
            a(i) = 0
        Next i

        For r = 0 To ds.Tables(0).Rows.Count - 1
            sdrow = ScanData.NewRow()
            sdrow(0) = ds.Tables(0).Rows(r)(135).ToString()
            sdrow(1) = ds.Tables(0).Rows(r)(136).ToString()
            tot = ds.Tables(0).Rows(r)(57) + ds.Tables(0).Rows(r)(58)
            sdrow(2) = tot
            sdrow(3) = ds.Tables(0).Rows(r)(59).ToString()
            sdrow(4) = ds.Tables(0).Rows(r)(62).ToString()
            sdrow(5) = ds.Tables(0).Rows(r)(63).ToString()
            sdrow(6) = ds.Tables(0).Rows(r)(65).ToString()
            sdrow(7) = ds.Tables(0).Rows(r)(66).ToString()
            sdrow(8) = ds.Tables(0).Rows(r)(67).ToString()
            sdrow(9) = ds.Tables(0).Rows(r)(70).ToString()
            sdrow(10) = ds.Tables(0).Rows(r)(73).ToString()
            sdrow(11) = ds.Tables(0).Rows(r)(76).ToString()
            sdrow(12) = ds.Tables(0).Rows(r)(70).ToString()
            sdrow(13) = ds.Tables(0).Rows(r)(82).ToString()
            tot = ds.Tables(0).Rows(r)(85) + ds.Tables(0).Rows(r)(88) + ds.Tables(0).Rows(r)(91) + ds.Tables(0).Rows(r)(94) + ds.Tables(0).Rows(r)(97)
            sdrow(14) = tot
            sdrow(15) = ds.Tables(0).Rows(r)(98).ToString()
            tot = ds.Tables(0).Rows(r)(100) + ds.Tables(0).Rows(r)(102) + ds.Tables(0).Rows(r)(104) + ds.Tables(0).Rows(r)(106) + ds.Tables(0).Rows(r)(108)
            sdrow(16) = tot
            sdrow(17) = ds.Tables(0).Rows(r)(110).ToString()
            tot = ds.Tables(0).Rows(r)(113) + ds.Tables(0).Rows(r)(116) + ds.Tables(0).Rows(r)(119)
            sdrow(18) = tot
            tot = ds.Tables(0).Rows(r)(122) + ds.Tables(0).Rows(r)(125)
            sdrow(19) = tot
            sdrow(20) = ds.Tables(0).Rows(r)(127).ToString()
            sdrow(21) = " "
            sdrow(22) = ds.Tables(0).Rows(r)(128).ToString()
            For i = 0 To 22
                If a(i) <> 21 Then
                    a(i) = a(i) + Val(sdrow(i))
                End If
            Next i
            ScanData.Rows.Add(sdrow)
        Next r

        sdrow1 = ScanData.NewRow()
        For i = 0 To 22
            sdrow1(i) = ""
            If i = 0 Then
                sdrow1(i) = "Total"
            End If
            If i > 1 And i <> 21 Then
                sdrow1(i) = a(i)
            End If
        Next i
        ScanData.Rows.Add(sdrow1)

        GridView1.DataSource = ScanData
        GridView1.DataBind()

        GridView1.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.[End]()

        ScanData.Clear()
        ScanData.Dispose()
        GridView1.Visible = False
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Confirms that an HtmlForm control is rendered for the 

        ' specified ASP.NET server control at run time. 

        ' No code required here. 
    End Sub

End Class
