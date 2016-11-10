
Namespace pshr

	Partial Class frmCashLess
		Inherits System.Web.UI.Page

		Private Sub bLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bLogout.Click
			Session.Abandon()
			Response.Redirect("login.aspx")
		End Sub

		Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
			'btnPrint.Attributes.Add("onClick", "javascript:cashless_printPage();")
			'Try
			If Not IsPostBack Then
				'Session("AppId") = "L"								 'Login mode
				'Session("EmpId") = "204023"
				'Session("EmpNm") = "204023"
				'Session("EmpGpf") = "abcdef"
				'Session("EmpSt") = 10

				If Len(Session("EmpId")) = 0 Then
					Response.Redirect("login.aspx")
				End If
				If Session("AppId") <> "L" Then
					Response.Redirect("login.aspx")
				End If

				Session("MsgPL") = ""
				'Dim vDr As OdbcDataReader
				Dim oracn As New OraDBconnection
				Dim sql As String
				Dim ds As New System.Data.DataSet

				Dim empid As String
				empid = Session("EmpId")	 'EmpId = Request("empid")

                sql = "select a.empid, gpfno, firstname || ' ' || middlename || ' ' || lastname, " & _
                "panno, fathername, bloodgroup, pshr.get_desg(cdesgcode) as desgtext , to_char(dob,'dd-Mon-yy'), pshr.get_org(cloccode) as locname, to_char(doj,'dd-Mon-yy'), " & _
                "d.pdoorno, d.psteet, d.pcity, d.ppincode, d.cdoorno, d.csteet, d.ccity, d.cpincode, d.phonecell, d.mailid, a.entitlement  " & _
                "from pshr.empperso a, pshr.empaddr d where " & _
                "a.empid=d.empid(+) and a.empid like '" & empid & "'"

				oracn.FillData1(sql, ds)

				Label1.Text = GetField(ds.Tables(0).Rows(0)(0))
				Label3.Text = GetField(ds.Tables(0).Rows(0)(2))
				Label5.Text = GetField(ds.Tables(0).Rows(0)(4))
				Label7.Text = GetField(ds.Tables(0).Rows(0)(6))
				Label9.Text = GetField(ds.Tables(0).Rows(0)(8))
                lblMedEnt.Text = GetField(ds.Tables(0).Rows(0)(20))

				imgEmpPhoto.ImageUrl = "ShowImage.ashx?eid=" & Session("EmpId").ToString

				ds = New System.Data.DataSet
				RegMembers_Lbl.Text = ""
				sql = "SELECT trim(firstname || ' ' || middlename || ' ' ||  lastname) as Name, trim((SELECT reltext FROM PSHR.mast_relation WHERE relcode=e.relcode)) as Relation, " & _
			"to_char(dob,'dd-Mon-yyyy') as dob, 'ShowImage.ashx?eid=' || empid ||'-'|| familyid AS photo2 FROM pshr.empfamily e WHERE empid = '" & empid & "' and dependent='Yes'"
				oracn.FillData(sql, ds)
				oracn.fillgrid(gvFamily, ds)

				If ds.Tables(0).Rows.Count < 1 Then
					RegMembers_Lbl.Text = "No Family Member Registered"
				End If
				ds.Clear()
				ds.Dispose()

				filldropdown()

				fillhdetails()
			End If

		End Sub

Private Sub filldropdown()
Dim oracn As New OraDBconnection
			Dim sql As String
			Dim ds As New System.Data.DataSet

			sql = "SELECT 'ALL' AS hcity FROM dual UNION ALL SELECT hcity FROM (select distinct city as hcity from pshr.cmt_user_details where user_status='E' and user_type='USER' and to_date(valid_upto,'DD-MM-YYYY') >= to_date('" & DateTime.Today.ToString("dd-MM-yyyy") & "','DD-MM-YYYY') order by hcity)"
			oracn.FillData(sql, ds)
			DropDownList1.DataSource = ds
			DropDownList1.DataValueField = ds.Tables(0).Columns("hcity").ColumnName
			DropDownList1.DataTextField = ds.Tables(0).Columns("hcity").ColumnName
			DropDownList1.DataBind()
			ds.Clear()
			ds.Dispose()
End Sub

		Private Sub fillhdetails()
			Dim oracn As New OraDBconnection
			Dim sql As String
			Dim ds As New System.Data.DataSet


			If DropDownList1.SelectedValue = "ALL" Then
			sql = "select case when File_Uploaded is not null then 'PDF' else '' end as attach, File_Uploaded as Link, name as hname,address as haddr, cont_person as hcontact, city as hcity, cont_numb as hmobile, valid_upto as hcontract from pshr.cmt_user_details where user_status='E' and user_type='USER' and to_date(valid_upto,'DD-MM-YYYY') >= to_date('" & DateTime.Today.ToString("dd-MM-yyyy") & "','DD-MM-YYYY') order by name"
			Else
			sql = "select case when File_Uploaded is not null then 'PDF' else '' end as attach, File_Uploaded as Link, name as hname,address as haddr, cont_person as hcontact, city as hcity, cont_numb as hmobile, valid_upto as hcontract from pshr.cmt_user_details where city='" + DropDownList1.SelectedValue + "' and user_status='E' and user_type='USER' and to_date(valid_upto,'DD-MM-YYYY') >= to_date('" & DateTime.Today.ToString("dd-MM-yyyy") & "','DD-MM-YYYY') order by name"
			End If
			oracn.FillData(sql, ds)
			gvHospital.DataSource = ds
			gvHospital.DataBind()
			
			ds.Clear()
			ds.Dispose()

			


		End Sub

	Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
			fillhdetails()
End Sub
End Class
End Namespace
