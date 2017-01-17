using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System.Data;

public partial class tnp_request : System.Web.UI.UserControl
{
    string dtformat = "dd-MON-yyyy hh:mi AM";
    private void show_posting_to_user(bool showAll=false)
    {
        string empid = Session["EmpId"].ToString();
        string status = Session["Status"].ToString();
        string sql = string.Empty;
        OraDBconnection oracn = new OraDBconnection();

        sql = "SELECT * FROM (SELECT cr.oonum || ' / ' || to_char(cr.oodate,'dd-Mon-yyyy') as \"Office Order\", " +
                "pshr.get_desg(cr.olddesgcode) || ' at ' || cadre.get_org_plants(cr.oldloccode) \"Present Loc\"," +
                "pshr.get_desg(cr.desgcode)  || ' at ' || cadre.get_org_plants(cr.loccode) \"NEW Loc\"," +
                //"nvl2(cr.postjoin, " +
                //"pshr.get_desg(c2.desgcode)    || ' at '    || cadre.get_org_plants(c2.loccode), " +
                //"pshr.get_desg(cr.desgcode)    || ' at '    || cadre.get_org_plants(cr.loccode) " +
                //") \"NEW Loc\", " +
                
                "CASE WHEN rel_skip='1' then 'N/A' ELSE to_char(date_rel_req,'" + dtformat + "') END as \"Relieving Request Date\", " +
                "CASE WHEN rel_skip='1' then 'N/A' ELSE to_char(date_rel_accept,'" + dtformat + "') END as \"Relieving Accept Date\", " +

                //"to_char(date_rel_req,'" + dtformat + "') as \"Relieving Request Date\", " +
                //"to_char(date_rel_accept,'" + dtformat + "') as \"Relieving Accept Date\", " +
                "to_char(date_join_req,'" + dtformat + "') as \"Joining Request Date\", " +
                "to_char(date_join_accept,'" + dtformat + "') as \"Joining Accept Date\", " +
                "CASE WHEN rel_skip='1' then 'N/A' ELSE pshr.get_fullname(REP_OFF_REL) || ' (' || REP_OFF_REL || ')' END as \"Relieving Officer\", " +
               // "pshr.get_fullname(REP_OFF_REL) || ' (' || REP_OFF_REL || ')' as \"Relieving Officer\", " +
                "pshr.get_fullname(REP_OFF_JOIN) || ' (' || REP_OFF_JOIN || ')' as \"Joining Officer\", " +
                "rel_off_comment, join_off_comment, cr.newempid as newemp " +
                "FROM CADRE.chargereport cr LEFT OUTER JOIN cadre.cadr c2 ON c2.rowno = cr.postjoin " +
                "LEFT OUTER JOIN pshr.empperso e1 ON cr.empid = e1.empid " +
                "WHERE eventcode in (28,36,37) and ";
        if (status == "None")
        {
            lblRequest.Text = "Relieve Request";
            btnSubReq.Text = "Submit Relieve Request";
            panRelReq0.Visible = false;

            sql += "(status is null or status = 'RRS') and ";
        }
        else if (status == "RRA")
        {
            lblRequest.Text = "Joining Request";
            btnSubReq.Text = "Submit Joining Request";
            panRelReq0.Visible = false;

            sql += "(status = 'RRA' or status = 'JRS' or status is null) and ";
        }
        else if (status == "RRS")
        {
            lb_ChangeRepOfficer.Visible = true;
            lb_ChangeRepOfficer.Text = "Change Relieving Officer";
            btnSubReq.Text = "Submit Relieve Request";
            panRelReq0.Visible = true;
            panRelReq.Visible = false;
        }
        else if (status == "JRS")
        {
            lb_ChangeRepOfficer.Visible = true;
            lb_ChangeRepOfficer.Text = "Change Joining Officer";
            btnSubReq.Text = "Submit Joining Request";
            panRelReq0.Visible = true;
            panRelReq.Visible = false;
        }
        else if (status == "JRA")
        {
            panRelReq.Visible = false;
            panRelReq0.Visible = false;
        }
        sql += " cr.empid = " + empid + " ORDER BY cr.oodate DESC) ";
        if (! showAll)
        {
            sql += " WHERE rownum = 1";
        }
        System.Data.DataSet ds = new System.Data.DataSet();
        oracn.FillData(sql, ref ds);

        //if JE is being promoted to AE, then show panel to change empid...
        //and stop proceeding, we will pick up the process with new empid later
        if (status == "RRA" && !empid.StartsWith("1"))
        {
            lblOldEmpid.Text = empid;
            lblNewEmpid.Text = ds.Tables[0].Rows[0]["newemp"].ToString();
            PanChangeEmpid.Visible = true;
            panRelReq.Visible = false;
            return;
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtROComment.Text = ds.Tables[0].Rows[0]["rel_off_comment"].ToString();
            txtJOComment.Text = ds.Tables[0].Rows[0]["join_off_comment"].ToString();
            oracn.fillgrid(ref gvPosting, ref ds);
        }
    }
    private void show_details(string empid)
    {
        string sql;

        System.Data.DataSet ds = new System.Data.DataSet();
        OraDBconnection orcn = new OraDBconnection();

        if (empid == "" || empid.Length != 6)
        {
            return;
        }

        sql = "select pshr.get_fullname(e.empid) as name, pshr.get_desg(e.cdesgcode) as desg, " +
            "cadre.get_org_plants(e.cloccode) as loc,ea.phonecell as cell, i.photo2 as photo from " +
            "pshr.empperso e left outer join pshr.empaddr ea on e.empid =ea.empid " +
            "left outer join img_pshr.img i on e.empid=i.empid " +
            "where recstatus=10 and e.empid=" + empid;
        orcn.FillData(sql, ref ds);
        if (ds.Tables[0].Rows.Count != 1)
        {
            return;
        }
        txtRREmpid.Text = empid;
        lblRRName.Text = ds.Tables[0].Rows[0]["name"].ToString();
        lblRRLoc.Text = ds.Tables[0].Rows[0]["loc"].ToString();
        lblRRDesg.Text = ds.Tables[0].Rows[0]["desg"].ToString();
        lblRRMob.Text = ds.Tables[0].Rows[0]["cell"].ToString();

        //load image
        if (!Convert.IsDBNull(ds.Tables[0].Rows[0]["photo"]))
        {
            byte[] barr = (byte[])ds.Tables[0].Rows[0]["photo"];
            string base64str = Convert.ToBase64String(barr);
            imgEmpPhoto.ImageUrl = string.Format("data:image/gif;base64,{0}", base64str);
        }

        ds.Clear();
        ds.Dispose();
    }
    protected void Clear_Repofficer_Detail()
    {
        txtRREmpid.Text = "";
        lblRRName.Text = "";
        lblRRLoc.Text = "";
        lblRRDesg.Text = "";
        lblRRMob.Text = "";
        lblMsg.Text = string.Empty;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Status"].ToString() == "None")
            {
                string mobileNo = Session["MbNo"].ToString();
                lblMobile.Text = "SMS regarding relieving/joining will be sent to mobile no " + mobileNo + "\n"+ ".To update number ";
            }
            else
            {
                lblMobile.Visible = false;
                linkupdateMobile.Visible = false;
            }

            show_posting_to_user();
        }
    }
    protected void txtRREmpid_TextChanged(object sender, EventArgs e)
    {
        string empid = Session["EmpId"].ToString();
        string offEmpid = txtRREmpid.Text.Trim();
        if (offEmpid.Length!=6)
        {
            Clear_Repofficer_Detail();
            lblMsg.Text = "Invalid Relieving/Joining Officer";
            txtRREmpid.Text = string.Empty;
            return;
        }
        string sql;
        int offHeCode;
        int empHeCode;
        OraDBconnection orcn = new OraDBconnection();

        //get hecode of empid
        empHeCode = int.Parse(orcn.GetScalar("select hecode from pshr.mast_desg where desgcode = (select cdesgcode from pshr.empperso "+
            "where empid = " + empid + ")"));

        //check if offEmpid exists and get its hecode
        sql = "select hecode from pshr.mast_desg where desgcode = (select cdesgcode from pshr.empperso where empid = " + offEmpid + ")";
        DataSet ds = orcn.GetData(sql);
        if (ds.Tables[0].Rows.Count != 1)
        {
            lblMsg.Text = "Invalid Relieving Officer";
            txtRREmpid.Text = string.Empty;
            return;
        }
        offHeCode = int.Parse(ds.Tables[0].Rows[0][0].ToString());

        //compare hecodes
        //TESTING
        if (offHeCode >= empHeCode)
        {
            lblMsg.Text = "Relieving Officer must be of higher rank than yourself.";
            txtRREmpid.Text = string.Empty;
            return;
        }

        lblMsg.Text = string.Empty;
        show_details(txtRREmpid.Text);
    }
    protected void btnSubReq_Click(object sender, EventArgs e)
    {
        string sql;
        string empid = Session["EmpId"].ToString();
        string repofficer = txtRREmpid.Text;
        OraDBconnection oracn = new OraDBconnection();
        string status = Session["Status"].ToString();
        System.Data.DataSet ds = new System.Data.DataSet();
        string phonecell;
        string msg;

        if (empid == "" || empid.Length != 6) return;
        
        if (string.IsNullOrWhiteSpace(repofficer) || 
            repofficer.Length != 6 || 
            !(repofficer.StartsWith("10") || repofficer.StartsWith("11")||repofficer.StartsWith("19")))
        {
            lblMsg.Text = "Invalid EmpID";
            return;
        }

        if (status == "None")
        {
            sql = string.Format("UPDATE cadre.chargereport SET "+
                "rep_off_rel={0}, status='RRS', date_rel_req=sysdate, "+
                "rep_off_rel_desg='{2}', rep_off_rel_loc='{3}' " +
                "WHERE (status IS NULL OR status = 'RRS') AND eventcode IN (28,36,37) AND empid = {1} "+
                "AND oodate = (SELECT max(oodate) FROM cadre.chargereport WHERE empid = {1} AND "+
                "eventcode IN (28,36,37) AND (status IS NULL OR status = 'RRS'))", 
                repofficer, empid, lblRRDesg.Text, lblRRLoc.Text);
            //sql = "update cadre.chargereport set " +
            //" rep_off_rel = " + repofficer + ", " +
            //" status = 'RRS', " +
            //" date_rel_req = sysdate" +
            //" where (status is null or status = 'RRS') and eventcode in (28,36,37) and empid = " + empid;
            oracn.ExecQry(sql);
            lblMsg.Text = "Relieve Request Submitted Successfully";
        }
        else if (status == "RRS")
        {
            //To change relieving officer
            //Insert values into change_rep_officer table
            sql = string.Format(" insert into CADRE.change_rep_officer "+
                "(oodate,oonum,propno,empid,old_rep_officer,old_date_rel_req,entrydate,eventcode,REQ_STATUS)"+
                "select * from (select oodate,oonum,propno,empid,rep_off_rel,date_rel_req,sysdate,eventcode,"+
                "STATUS from cadre.chargereport "+
                "where empid = {0} and status='RRS' and oodate = (select max(oodate) from cadre.chargereport "+
                "where empid = {0} AND STATUS='RRS'))", empid);
            oracn.ExecQry(sql);

            // send message regarding cancel relieving request
           sql=string.Format("select rep_off_rel,nvl(ea.phonecell,'0') as MobileNo from cadre.chargereport cr , PSHR.empaddr ea "+
                     "where cr.empid = {0} and status='RRS' and ea.empid=cr.rep_off_rel and oodate = (select max(oodate) from cadre.chargereport " +
                      "where empid = {0} AND STATUS='RRS')", empid);
           System.Data.DataSet ds_rel = new System.Data.DataSet();
           oracn.FillData(sql, ref ds_rel);
           string OldRepOfficer = ds_rel.Tables[0].Rows[0][0].ToString();
           phonecell = ds_rel.Tables[0].Rows[0][1].ToString();
            msg = "The relieving request submitted by the officer with id " + empid + "  has been cancelled.";

            if (System.Environment.MachineName.ToUpper().Contains("SERVER") && phonecell != "0" && libSMSPbGovt.SMS.SendSMS(phonecell, msg))
            {
                oracn.ExecQry(string.Format("insert into cadre.smslog values('{0}','{1}',sysdate)", phonecell, msg));
            }

            
            //Update chargereport table
            sql = string.Format("UPDATE cadre.chargereport SET " +
                "rep_off_rel={0}, date_rel_req=sysdate, " +
                "rep_off_rel_desg='{2}', rep_off_rel_loc='{3}' " +
                "WHERE empid = {1} " +
                "AND oodate = (SELECT max(oodate) FROM cadre.chargereport WHERE empid = {1} AND " +
                "eventcode IN (28,36,37) AND (status = 'RRS' ))",
                repofficer, empid, lblRRDesg.Text, lblRRLoc.Text);
            oracn.ExecQry(sql);
            lblMsg.Text = "New Releiving Request Submitted Successfully";
            
            //send messages
            
            oracn.FillData("select nvl(phonecell,'0') from pshr.EMPADDR where empid = " + repofficer, ref ds);
            phonecell = ds.Tables[0].Rows[0][0].ToString();
            msg = "You have received a relieving/joining request. Please visit HR Portal";
            if (System.Environment.MachineName.ToUpper().Contains("SERVER") && phonecell != "0" && libSMSPbGovt.SMS.SendSMS(phonecell, msg))
            {
                oracn.ExecQry(string.Format("insert into cadre.smslog values('{0}','{1}',sysdate)", phonecell, msg));
            }
        }
        else if (status == "JRS")
        {
            //To change joining officer
            //Insert values into change_rep_officer table
            sql = string.Format(" insert into CADRE.change_rep_officer " +
              "(oodate,oonum,propno,empid,old_rep_officer,old_date_rel_req,entrydate,eventcode,REQ_STATUS)" +
               "select * from (select oodate,oonum,propno,empid,rep_off_join,date_join_req,sysdate,eventcode,STATUS "+
               "from cadre.chargereport " +
              "where empid = {0} and status='JRS' and oodate = (select max(oodate) from cadre.chargereport where "+
              "empid = {0} AND STATUS='JRS'))", empid);
            oracn.ExecQry(sql);


            // send message regarding cancel joining request
            sql = string.Format("select rep_off_join,nvl(ea.phonecell,'0') as MobileNo from cadre.chargereport cr , PSHR.empaddr ea " +
                      "where cr.empid = {0} and status='JRS' and ea.empid=cr.rep_off_join and oodate = (select max(oodate) from cadre.chargereport " +
                       "where empid = {0} AND STATUS='JRS')", empid);

            System.Data.DataSet ds_join = new System.Data.DataSet();
            oracn.FillData(sql, ref ds_join);
            string OldRepOfficer = ds_join.Tables[0].Rows[0][0].ToString();
            phonecell = ds_join.Tables[0].Rows[0][1].ToString();
            msg = "The joining request submitted by the officer with id " + empid + "  has been cancelled.";

            if (System.Environment.MachineName.ToUpper().Contains("SERVER") && phonecell != "0" && libSMSPbGovt.SMS.SendSMS(phonecell, msg))
            {
                oracn.ExecQry(string.Format("insert into cadre.smslog values('{0}','{1}',sysdate)", phonecell, msg));
            }

            //Update chargereport table
            sql = string.Format("UPDATE cadre.chargereport SET " +
                "rep_off_join={0}, date_join_req=sysdate, " +
                "rep_off_join_desg='{2}', rep_off_join_loc='{3}' " +
                "WHERE empid = {1} " +
                "AND oodate = (SELECT max(oodate) FROM cadre.chargereport WHERE empid = {1} AND " +
                "eventcode IN (28,36,37) AND (status = 'JRS' ))", 
                repofficer, empid, lblRRDesg.Text, lblRRLoc.Text);
            oracn.ExecQry(sql);
            lblMsg.Text = "New Joining Request Submitted Successfully";

            // send messages
            oracn.FillData("select nvl(phonecell,'0') from pshr.EMPADDR where empid = " + repofficer, ref ds);
            phonecell = ds.Tables[0].Rows[0][0].ToString();
            msg = "You have received a relieving/joining request. Please visit HR Portal";
          
            if (System.Environment.MachineName.ToUpper().Contains("SERVER") && phonecell != "0" && libSMSPbGovt.SMS.SendSMS(phonecell, msg))
            {
               oracn.ExecQry(string.Format("insert into cadre.smslog values('{0}','{1}',sysdate)", phonecell, msg));
            }
        }
        else if (status == "RRA")
        {
            //check for status is null in case of LJON (10)
            sql = string.Format("UPDATE cadre.chargereport SET " +
                "rep_off_join={0}, status='JRS', date_join_req=sysdate, " +
                "rep_off_join_desg='{2}', rep_off_join_loc='{3}' " +
                "WHERE (status = 'RRA' or status = 'JRS' or status is null) AND eventcode IN (28,36,37) AND empid = {1} " +
                "AND oodate = (SELECT max(oodate) FROM cadre.chargereport WHERE empid = {1} AND " +
                "eventcode IN (28,36,37) AND (status = 'RRA' or status = 'JRS' or status is null))",
                repofficer, empid, lblRRDesg.Text, lblRRLoc.Text);

            //sql = "update cadre.chargereport set " +
            //" rep_off_join = " + repofficer + ", " +
            //" status = 'JRS', " +
            //" date_join_req = sysdate" +
            //" where (status = 'RRA' or status = 'JRS' or status is null) and eventcode in (28,36,37) and empid = " + empid;
            oracn.ExecQry(sql);
            lblMsg.Text = "Joining Request Submitted Successfully";
        }
        if (status == "None" || status == "RRA")
        {
            oracn.FillData("select nvl(phonecell,'0') from pshr.EMPADDR where empid = " + repofficer, ref ds);
            phonecell = ds.Tables[0].Rows[0][0].ToString();
            msg = "You have received a relieving/joining request. Please visit HR Portal";
            //TESTING
            if (System.Environment.MachineName.ToUpper().Contains("SERVER") && phonecell != "0" && libSMSPbGovt.SMS.SendSMS(phonecell, msg))
            {
                oracn.ExecQry(string.Format("insert into cadre.smslog values('{0}','{1}',sysdate)", phonecell, msg));
            }
        }
        btnSubReq.Enabled = false;
        show_posting_to_user();
    }
    protected void bLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
    protected void chkUnderstandChange_CheckedChanged(object sender, EventArgs e)
    {
        btnChangeEmpid.Enabled = chkUnderstandChange.Checked;
    }
    protected void btnChangeEmpid_Click(object sender, EventArgs e)
    {
        OraDBconnection oracn = new OraDBconnection();
        string oldempid, newempid;
        //string sql;

        oldempid = lblOldEmpid.Text;
        newempid = lblNewEmpid.Text;

        //run oracle procedure UpdateRec
        oracn.ExecUpdateRec(oldempid, newempid);

        //change entry in chargereport table so that 
        //on next login we can show 'Request Join' message
        //sql = "update cadre.chargereport set empid = " + newempid + 
        //        " WHERE eventcode = 28 " +
        //        " and status = 'RRA' " +
        //        " and  empid = " + oldempid;
        //oracn.ExecQry(sql);
        Response.Redirect("./login.aspx");
        //lblChangeEmpIDMsg.Text = 
        //    string.Format("Empid Changed. Login with UserID: {0}, Password: {0}", newempid);
    }
    public void DownloadFile(String pdfPath, bool autoDelete = true, string content_type = "application/pdf")
    {
        System.IO.FileInfo objFi = new System.IO.FileInfo(pdfPath);
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + objFi.Name);
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.AddHeader("Content-Length", objFi.Length.ToString());
        HttpContext.Current.Response.ContentType = content_type;
        HttpContext.Current.Response.WriteFile(objFi.FullName);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.Close();
        if (autoDelete)
        {
            System.IO.File.Delete(pdfPath);
        }
    }
    protected void ibtnRelieveRep_Click(object sender, ImageClickEventArgs e)
    {
        string empid = Session["EmpId"].ToString();
        string rel_skip = Session["Rel_Skip"].ToString();

        if (rel_skip == "1")
        {
            lblMsg.Text = "Relieving report is not available";
            return;
        }
        string relAcceptDate = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).Cells[5].Text;
        string oonum_date = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).Cells[0].Text;
        
        if (! oonum_date.Contains('/'))
        {
            lblMsg.Text = "Invalid O/o Number";
            return;
        }
        else if (String.IsNullOrWhiteSpace(relAcceptDate) || relAcceptDate=="&nbsp;")
        {
            lblMsg.Text = "Relieving Report only available after acceptance of Relieve Request.";
            return;
        }
        string oonum = oonum_date.Substring(0, oonum_date.LastIndexOf('/')).Trim();
        OraDBconnection oracn = new OraDBconnection();
        System.Data.DataSet ds = new System.Data.DataSet();
        string sql = "select empid, pshr.get_fullname(empid) as name, pshr.get_org(oldloccode) as loc, " +
            "pshr.get_desg(olddesgcode) as desg, OONUM, to_char(OODATE,'dd-mm-yyyy') as oodate, " +
            "to_char(DATE_REL_ACCEPT,'dd-mm-yyyy \"at\" hh:mi AM') as date_rel_accept , " +
            "REP_OFF_REL, pshr.get_fullname(rep_off_rel) repOffName,"+
            "rep_off_rel_desg, rep_off_rel_loc "+
            "from cadre.chargereport " +
            "where oonum = '" + oonum + "' and empid = " + empid;
        string pdfPath = Server.MapPath("Relieving_Report_" + empid + ".pdf");
        oracn.FillData(sql, ref ds);

        CrystalReportSource CrystalReportSource1 = new CrystalReportSource();
        CrystalReportSource1.EnableCaching = false;
        CrystalReportSource1.Report.FileName = Server.MapPath("RepRelieving.rpt");
        CrystalReportSource1.ReportDocument.Refresh();
        CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables[0]);
        CrystalReportSource1.DataBind();
        CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath);
        CrystalReportSource1.ReportDocument.Dispose();
        DownloadFile(pdfPath);
    }  
    protected void ibtnJoiningRep_Click(object sender, ImageClickEventArgs e)
    {
        string empid = Session["EmpId"].ToString();
        string joinAcceptDate = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).Cells[8].Text;
        string oonum_date = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).Cells[0].Text;
        if (!oonum_date.Contains('/'))
        {
            lblMsg.Text = "Invalid O/o Number";
            return;
        }
        else if (String.IsNullOrWhiteSpace(joinAcceptDate) || joinAcceptDate == "&nbsp;")
        {
            lblMsg.Text = "Joining Report only available after acceptance of Joining Request.";
            return;
        }
        string oonum = oonum_date.Substring(0, oonum_date.LastIndexOf('/')).Trim();
        OraDBconnection oracn = new OraDBconnection();
        System.Data.DataSet ds = new System.Data.DataSet();
        string sql = "select empid, pshr.get_fullname(empid) as name, pshr.get_org(loccode) as loc, " +
            "cadre.get_mapping_text_from_rowno(postjoin) as pcloc, loccode, cadre.loccode_from_rowno(postjoin) as pcloccode," +
            "pshr.get_desg(desgcode) as desg, OONUM, to_char(OODATE,'dd-mm-yyyy') as oodate, " +
            "to_char(DATE_JOIN_ACCEPT,'dd-mm-yyyy \"at\" hh:mi AM') as date_join_accept , " +
            "REP_OFF_JOIN, pshr.get_fullname(rep_off_join) repOffName, "+
            "rep_off_join_desg, rep_off_join_loc " +
            "from cadre.chargereport " +
            "where oonum = '" + oonum + "' and empid = " + empid;
        string pdfPath = Server.MapPath("Joining_Report_" + empid + ".pdf");
        oracn.FillData(sql, ref ds);

        CrystalReportSource CrystalReportSource1 = new CrystalReportSource();
        CrystalReportSource1.EnableCaching = false;
        CrystalReportSource1.Report.FileName = Server.MapPath("RepJoining.rpt");
        CrystalReportSource1.ReportDocument.Refresh();
        CrystalReportSource1.ReportDocument.SetDataSource(ds.Tables[0]);
        CrystalReportSource1.DataBind();
        CrystalReportSource1.ReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath);
        CrystalReportSource1.ReportDocument.Dispose();
        DownloadFile(pdfPath);
    }
    protected void lb_ChangeRepOfficer_Click(object sender, EventArgs e)
    {
        Clear_Repofficer_Detail();
        string status = Session["Status"].ToString();
        if (status == "RRS")
        {
            panRelReq.Visible = true;
            panRelReq0.Visible = false;
            lblRequest.Text = "Change Relieving officer";
        }
        else if (status == "JRS")
        {
            panRelReq.Visible = true;
            panRelReq0.Visible = false;
            lblRequest.Text = "Change Joining officer";
        }
    }
    protected void linkupdateMobile_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmchangembemail.aspx");
    }
    protected void lnkShowAll_Click(object sender, EventArgs e)
    {
        show_posting_to_user(true);
    }
}