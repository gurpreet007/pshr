
Partial Class frmSalSlip2
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
            sql = "select mths,oldid from pshr.netsalmths where empid = " & Session("EmpId") & ""
            OraCn.FillData1(sql, ds)
            HiddenField1.Value = Session("EmpId")
            s = ""
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)(0)) Then
                    s = ds.Tables(0).Rows(0)(0)
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)(1)) Then
                    If Len(ds.Tables(0).Rows(0)(1).ToString()) = 6 Then
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
                lblMsg.Text = "Pay Slip not available. You may contact concerned DDO for the same."
            End If
        End If
    End Sub

    Private Sub setDat()
        Label2.Visible = False
        drpGpfYr.Visible = False
        Button1.Visible = False
        Label3.Visible = False
        drpConsmth.Visible = False
        Button2.Visible = False
        'LinkButton2.Visible = False
    End Sub

    Protected Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Session("fyr") = drpGpfYr.SelectedItem.Value
        Session("EmpIdtot") = HiddenField1.Value
        'Session("rptNm") = "RptSalarySlip3.rpt"
        'If drpGpfYr.SelectedItem.Value <= "201111" Then
        '    Session("rptNm") = "RptSalarySlip.rpt"
        'End If
        If drpGpfYr.SelectedItem.Value > "201111" Then
            Response.Redirect("frmrptvwrsal1.aspx")
        Else
            Response.Redirect("frmrptvwrsal.aspx")
        End If
    End Sub

    'Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click

    '    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Confirms that an HtmlForm control is rendered for the 

        ' specified ASP.NET server control at run time. 

        ' No code required here. 
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Response.Redirect("empdetail.aspx")
    End Sub

    Protected Sub bLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bLogout.Click
        Session.Abandon()
        Response.Redirect("login.aspx")
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        GridView1.Visible = True

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=SalaryCons.xls")
        Response.Charset = ""

        
        Response.ContentType = "application/vnd.xls"
        Me.EnableViewState = False
        Dim empid As String
        empid = Session("EmpId")

        Dim oraCn As New OraDBconnection
        Dim ds As New System.Data.DataSet
        Dim sql As String
        Dim sql2 As String
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

        s1 = Mid(drpConsmth.SelectedItem.Value, 6, Len(drpConsmth.SelectedItem.Value) - 6)
        sql = ""
        sql2 = ""

        Dim a() As String = s1.Split(",")
        Dim y As Integer
        y = 0
        Dim i As Integer
        i = 0
        For i = 0 To a.Length - 1
            If sql <> "" Then
                sql = sql & " union all "
                sql2 = sql2 & " union all "
            End If
            sql = sql & "select '" & Mid(a(i), 5, 2) & "' as Mth, '" & Mid(a(i), 1, 4) & "' as Yr,BP + BP1 as BscPay,GP as GrdPay,DP + DP1 as DPay,IR + IR1 as IntrmR,NPA + NPA1 as NonPA,PERPAY +PERPAY1 as PersnP,SPLPAY + SPLPAY1 as SpclPay,MA + MA1 as MedicalA,CDA + CDA1 as DAAll, HRA + HRA1 as HRAAll,Shiftal + shiftal1 as ShiftA, PrjGen + PrjGen1 as GenAll, CCA as CityCA, Loda + Loda1 as Loda, ElecAL,RuralAl + RuralAl1 as RuralA,PolKit + PolClth + Poleqp as PoliceAll, Protal, EnvAl,DustAl,SectAl,RiskAl,DeputAl,WashAl,ChowAl,MobAl, Pay13, GIAlw + GDAlw + GIArr + GDArr as GenAl, PFComp as C_EPF, MiscAl + MiscA2 + MiscA3 + MiscA4 + MiscA5 as MiscAl, Gpay as GrossP,UserId,Ledger from salary.salary" & a(i) & "T where empid in(" & HiddenField1.Value & ")  and netpay >= 0 "
            sql2 = sql2 & "select '" & Mid(a(i), 5, 2) & "' as Mth, '" & Mid(a(i), 1, 4) & "' as Yr,pfsub + pfsub1 as PFSubs,pfrec as Recovr,itax + GITax as IncTax,hrent as HRecov,wfchg as WtrFan,clubfee,SalAdv,CarAdv,SctAdv,CompAdv,Interst,PUV,Hba1 + hba2 + hba3 + hba4 + hba5 as HBAAdv,MiscRec + MiscRec2 + MiscRec3 + MiscRec4 + MiscRec5 as MiscR,UPaidRec,Rec44,IUTRec,Licamt1 + licamt2 + licamt3 + licamt4 + licamt5 + Licamt6 + licamt7 + licamt8 + licamt9 + licamt10 as LICSub,Ginsamt + Ginsub2 as GICSub,bfamt1 + bfamt2 + bfamt3 as Benov,mobamt1 + mobamt2 as MobAmt,gded as Deduc,' ' as Col1,' ' as Col2,' ' as Col3,' ' as Col4,' ' as Col5,' ' as Col6,' ' as Col7,' ' as Col8,NetPay,UserId,Ledger from salary.salary" & a(i) & "T where empid in(" & HiddenField1.Value & ") and netpay >= 0 "
            y = Mid(drpConsmth.SelectedItem.Text, 1, 4)
            If Mid(a(i), 5, 2) = "03" And Mid(a(i), 1, 4) = y Then
                sql = sql & " and ledger not in('Arrear','Arrear15') "
                sql2 = sql2 & " and ledger not in('Arrear','Arrear15') "
            ElseIf Mid(a(i), 5, 2) = "03" And Mid(a(i), 1, 4) = (y + 1) Then
                sql = sql & " and ledger in('Arrear','Arrear15') "
                sql2 = sql2 & " and ledger in('Arrear','Arrear15') "
            End If
        Next i


        If sql = "" Then
            lblMsg.Text = "Details not available."
            Exit Sub
        End If

        ds = New DataSet()

        sql = "select * from (" & sql & ") a order by  Yr,Mth,ledger"

        sql = "select * from (" & sql & ") d union all " & "select 'Total' as Mth,' ' as Yr,Sum(BscPay),Sum(GrdPay),Sum(DPay),Sum(IntrmR),Sum(NonPA),Sum(PersnP), Sum(SpclPay), Sum(MedicalA),Sum(DAAll), Sum(HRAAll), Sum(ShiftA), Sum(GenAll), Sum(CityCA), Sum(Loda), Sum(ElecAL), Sum(RuralA), Sum(PoliceAll), Sum(Protal), Sum(EnvAl),Sum(DustAl), Sum(SectAl), Sum(RiskAl),Sum(DeputAl),Sum(WashAl), Sum(ChowAl), Sum(MobAl), Sum(Pay13), Sum(GenAl),    Sum(C_EPF), Sum(MiscAl), Sum(GrossP), ' ' as Userid,' ' as Ledger from (" & sql & ") e"
        oraCn.FillData1(sql, ds)

        GridView1.DataSource = ds
        GridView1.DataBind()
        GridView1.RenderControl(htmlWrite)

        ds.Clear()
        ds.Dispose()


        htmlWrite.WriteLine("<b><u><font size='5'> Recoveries </font></u></b>")

        sql = "select * from (" & sql2 & ") a order by  yr,mth,ledger"

        ds = New DataSet()

        sql = "select * from (" & sql & ") d union all " & "select 'Total' as Mth,' ' as Yr,sum(PFSubs),sum(Recovr),sum(IncTax),sum(HRecov),sum(WtrFan),sum(clubfee),sum(SalAdv),sum(CarAdv),sum(SctAdv),sum(CompAdv),sum(Interst),sum(PUV),sum(HBAAdv),sum(MiscR),sum(UPaidRec),sum(Rec44),sum(IUTRec), sum(LICSub),sum(GICSub),sum(Benov),sum(MobAmt),sum(Deduc),' ' as Col1,' ' as Col2,' ' as Col3,' ' as Col4,' ' as Col5,' ' as Col6,' ' as Col7,' ' as Col8,sum(NetPay),' ' as UserId, ' ' as Ledger from (" & sql & ") e"
        oraCn.FillData1(sql, ds)

        GridView1.DataSource = ds
        GridView1.DataBind()
        GridView1.HeaderRow.Cells(24).Text = " "
        GridView1.HeaderRow.Cells(25).Text = " "
        GridView1.HeaderRow.Cells(26).Text = " "
        GridView1.HeaderRow.Cells(27).Text = " "
        GridView1.HeaderRow.Cells(28).Text = " "
        GridView1.HeaderRow.Cells(29).Text = " "
        GridView1.HeaderRow.Cells(30).Text = " "
        GridView1.HeaderRow.Cells(31).Text = " "
        GridView1.RenderControl(htmlWrite)

        ds.Clear()
        ds.Dispose()


        Response.Write(stringWrite.ToString())
        Response.[End]()

        GridView1.Visible = False


    End Sub
End Class
